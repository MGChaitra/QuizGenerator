using EducationAPI.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelDLL.Models;
using System.Threading.Tasks;

namespace EducationAPI.Controllers
{
    [EnableCors("cors")]
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private readonly IQuizService _quizService;

        public QuizController(ILogger<QuizController> logger, IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }

        /// <summary>
        /// Generates a multiple-choice quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-mcq")]
        public async Task<IActionResult> GenerateMCQQuiz([FromBody] RequestModel request)
        {
            try
            {
                var result = await _quizService.GenerateQuizContentAsync(request, "MCQ").ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating quiz content for type MCQ");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Generates a short-answer quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-short-answer")]
        public async Task<IActionResult> GenerateShortAnswerQuiz([FromBody] RequestModel request)
        {
            try
            {
                var result = await _quizService.GenerateQuizContentAsync(request, "Short Answer").ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating quiz content for type Short Answer");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Generates a long-answer quiz.
        /// </summary>
        /// <param name="request">The quiz generation request.</param>
        /// <returns>The generated quiz content.</returns>
        [HttpPost("generate-long-answer")]
        public async Task<IActionResult> GenerateLongAnswerQuiz([FromBody] RequestModel request)
        {
            try
            {
                var result = await _quizService.GenerateQuizContentAsync(request, "Long Answer").ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating quiz content for type Long Answer");
                return BadRequest(ex);
            }
        }
    }
}
