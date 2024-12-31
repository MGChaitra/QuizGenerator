using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using EducationWebApp.Models;
using System.Net.Http;
using EducationWebApp.Contracts;


namespace EducationWebApp.Services
{
    // Add code summary for each methods and properties - completed
    // Add Ilogger for logging - completed
    
    /// <summary>
    /// Service to interact with EducationAPI for generating quiz content.
    /// </summary>
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenAIService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAIService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance.</param>
        /// <param name="logger">The logger instance.</param>
        public OpenAIService(HttpClient httpClient, ILogger<OpenAIService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Add null validation for subject,difficulty and number - completed
        // You can create the private method to perform the validations and avoid the code repetation - completed
        // Add exception handling using try catch - completed

        /// <summary>
        /// Fetches MCQ quiz content.
        /// </summary>
        /// <param name="request">The request containing quiz parameters.</param>
        /// <returns>The generated MCQ quiz content.</returns>
        public async Task<string> GetMCQQuizContentAsync(RequestModel request)
        {
            ValidateParameters(request);

            try
            {
                _logger.LogInformation("Fetching MCQ quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                var response = await _httpClient.PostAsJsonAsync("api/quiz/generate-mcq", request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching MCQ quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                throw;
            }
        }
         
        // Make it post call - completed
        // call the request in this way response = await _httpClient.PostAsJsonAsync("api/quiz/generate", request); - completed

        /// <summary>
        /// Fetches short-answer quiz content.
        /// </summary>
        /// <param name="request">The request containing quiz parameters.</param>
        /// <returns>The generated short-answer quiz content.</returns>
        public async Task<string> GetShortAnswerQuizContentAsync(RequestModel request)
        {
            ValidateParameters(request);

            try
            {
                _logger.LogInformation("Fetching short-answer quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                var response = await _httpClient.PostAsJsonAsync("api/quiz/generate-short-answer", request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching short-answer quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                throw;
            }
        }

        /// <summary>
        /// Fetches long-answer quiz content.
        /// </summary>
        /// <param name="request">The request containing quiz parameters.</param>
        /// <returns>The generated long-answer quiz content.</returns>
        public async Task<string> GetLongAnswerQuizContentAsync(RequestModel request)
        {
            ValidateParameters(request);

            try
            {
                _logger.LogInformation("Fetching long-answer quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                var response = await _httpClient.PostAsJsonAsync("api/quiz/generate-long-answer", request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching long-answer quiz content for subject: {Subject}, difficulty: {Difficulty}, number: {Number}", request.Subject, request.Difficulty, request.Number);
                throw;
            }
        }

        /// <summary>
        /// Validates the request parameters.
        /// </summary>
        /// <param name="request">The request containing quiz parameters.</param>
        private void ValidateParameters(RequestModel request)
        {
            if (string.IsNullOrEmpty(request.Subject))
                throw new ArgumentException("Subject cannot be null or empty", nameof(request.Subject));
            if (string.IsNullOrEmpty(request.Difficulty))
                throw new ArgumentException("Difficulty cannot be null or empty", nameof(request.Difficulty));
            if (request.Number <= 0)
                throw new ArgumentException("Number must be greater than zero", nameof(request.Number));
        }
    }
}
