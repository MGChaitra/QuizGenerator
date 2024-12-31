using EducationWebApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;
namespace EducationAPI.Controllers
{
    [EnableCors("cors")]
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : Controller
    {
        // Add summary to methods and properties - completed
        // Add exception handling using try catch - completed
        // Add Ilogger for logging - completed
        // Create new folder Services - Not using any services here

        private readonly Kernel _kernel;
        private readonly ILogger<QuizController> _logger;
        private readonly ChatHistory _history;
        private readonly IChatCompletionService _chatCompletionService;
        private readonly IConfiguration _configuration;
        public QuizController(Kernel kernel, ILogger<QuizController> logger, IConfiguration configuration)
        {
            _kernel = kernel;
            _history = new ChatHistory();
            _configuration = configuration;

            // Improve the prompt by adding some rules to it - Rules I have added in the main prompt
            _history.AddSystemMessage("You are an Educational Content Generator and a helpful assistant that specializes in generating quiz questions. Your goal is to create valid, meaningful, and accurate questions for quizzes and exams, tailored to various Question Types, Subjects, Number of Questions and Difficulty Levels. Strictly remove the escaping characters like \"\\\" included in the response");
            _chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
            _logger = logger;

        }
        /// <summary>
        /// Generates a multiple-choice quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-mcq")]
        public async Task<IActionResult> GenerateMCQQuiz([FromBody] RequestModel request)
        {
            return await GenerateQuizContentAsync(request, "MCQ");
        }
        /// <summary>
        /// Generates a short-answer quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-short-answer")]
        public async Task<IActionResult> GenerateShortAnswerQuiz([FromBody] RequestModel request)
        {
            return await GenerateQuizContentAsync(request, "Short Answer");
        }
        /// <summary>
        /// Generates a long-answer quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-long-answer")]
        public async Task<IActionResult> GenerateLongAnswerQuiz([FromBody] RequestModel request)
        {
            return await GenerateQuizContentAsync(request, "Long Answer");
        }
        /// <summary>
        /// Generates quiz content based on the provided request and question type.
        /// </summary>
        /// <param name="request">The request model containing the subject, difficulty, and number of questions.</param>
        /// <param name="questionType">The type of questions to generate (e.g., "MCQ", "Short Answer", "Long Answer").</param>
        /// <returns>An IActionResult containing the generated quiz content or an error status.</returns>
        private async Task<IActionResult> GenerateQuizContentAsync(RequestModel request, string questionType)
        {
            try
            {
                _history.AddUserMessage(request.Subject);
                _history.AddUserMessage(request.Difficulty);
                _history.AddUserMessage(request.Number.ToString());
                _history.AddUserMessage(questionType);

                var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings
                {
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                };

                var result = await _chatCompletionService.GetChatMessageContentAsync(
                    _history,
                    executionSettings: openAIPromptExecutionSettings,
                    kernel: _kernel);

                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating quiz content");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
