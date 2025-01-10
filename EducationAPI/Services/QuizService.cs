using EducationAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using EducationAPI.Controllers;
using ModelDLL.Models;
namespace EducationAPI.Services

{
    public class QuizService : IQuizService

    {

        private readonly Kernel _kernel;
        private readonly ChatHistory _history;
        private readonly IChatCompletionService _chatCompletionService;
      
        
        public QuizService(Kernel kernel, ChatHistory history, IChatCompletionService chatCompletionService) 
        {
            _kernel = kernel;
            _history = history;
            _history.AddSystemMessage("You are an Educational Content Generator and a helpful assistant that specializes in generating quiz questions. Your goal is to create valid, meaningful, and accurate questions for quizzes and exams, tailored to various Question Types, Subjects, Number of Questions and Difficulty Levels. Strictly remove the escaping characters like \"\\\" included in the response");
            _chatCompletionService = chatCompletionService;
         
        }
        /// <summary>
        /// Generates quiz content based on the provided request and question type.
        /// </summary>
        /// <param name="request">The request model containing the subject, difficulty, and number of questions.</param>
        /// <param name="questionType">The type of questions to generate (e.g., "MCQ", "Short Answer", "Long Answer").</param>
        /// <returns>An IActionResult containing the generated quiz content or an error status.</returns>
        public async Task<string> GenerateQuizContentAsync(RequestModel request, string questionType)
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

                return result.Content!;
            }
            catch
            {
                throw;
            }
        }
    }
}
