using EducationWebApp.Models;

namespace EducationWebApp.Contracts
{
    /// <summary>
    /// Interface for OpenAIService.
    /// </summary>
    public interface IOpenAIService
    {
        /// <summary>
        /// Generates multiple-choice quiz content based on the provided request model.
        /// </summary>
        /// <param name="request">The request model containing the necessary parameters for generating the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated multiple-choice quiz content as a string.</returns>
        Task<string> GetMCQQuizContentAsync(RequestModel request);

        /// <summary>
        /// Generates short answer quiz content based on the provided request model.
        /// </summary>
        /// <param name="request">The request model containing the necessary parameters for generating the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated short answer quiz content as a string.</returns>
        Task<string> GetShortAnswerQuizContentAsync(RequestModel request);

        /// <summary>
        /// Generates long answer quiz content based on the provided request model.
        /// </summary>
        /// <param name="request">The request model containing the necessary parameters for generating the quiz.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated long answer quiz content as a string.</returns>
        Task<string> GetLongAnswerQuizContentAsync(RequestModel request);
    }
}
