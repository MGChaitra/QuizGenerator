using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace EducationAPI.Plugins
{
    public class QuizPlugin
    {
        // Add description to the parameter as provided for kernel function, even provide return descrption - completed
        // Improve the prompt, for example you can add like Follow the below few shots examples and also provide few more examples, - completed
        // for note you can update as Rules - completed

        private readonly IConfiguration _configuration;

        public QuizPlugin(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        /// <summary>
        /// Generates an MCQ quiz prompt using input from the request model.
        /// </summary>
        /// <param name="subject">The subject for the quiz questions.</param>
        /// <param name="difficulty">The difficulty level of the quiz questions.</param>
        /// <param name="numberOfQuestions">The number of quiz questions to generate.</param>
        /// <returns>A string containing the generated MCQ quiz questions.</returns>
        [KernelFunction("generate_mcq_quiz")]
        [Description("Generate MCQ quiz questions based on the subject, difficulty, and number of questions.")]
        public async Task<string> GenerateMCQQuiz(string subject, string difficulty, int numberOfQuestions)
        {
            string? promptPath = _configuration["Prompts"];

            if (string.IsNullOrWhiteSpace(promptPath))
            {
                throw new ArgumentNullException(nameof(promptPath));
            }
            // Load the example and rules prompts
            string examplePath = Path.Combine(promptPath, "MCQPrompt.txt");
            if (!File.Exists(examplePath))
            {
                throw new FileNotFoundException($"The example prompt file path is not found at path:{examplePath}");
            }

            string rulesPath = Path.Combine(promptPath, "RulesPrompt.txt");
            if (!File.Exists(rulesPath))
            {
                throw new FileNotFoundException($"The rules prompt file path is not found at path:{rulesPath}");
            }
            string exampleDescription = await File.ReadAllTextAsync(examplePath).ConfigureAwait(false);
            string rulesDescription = await File.ReadAllTextAsync(rulesPath).ConfigureAwait(false);

            // Construct the final prompt
            string finalPrompt = $"""
        You are an AI assistant skilled at generating high-quality quiz content. 
        Your task is to create multiple-choice questions (MCQs) as described below.

        ### Instructions:
        - Subject: {subject}
        - Difficulty: {difficulty}
        - Number of Questions: {numberOfQuestions}
       
        ### Examples:
        {exampleDescription}

        ### Rules:
        {rulesDescription}

        Follow the examples closely to generate accurate and relevant MCQs.
        Ensure the output only contains the questions and answers as per the examples provided.
        """;

            return finalPrompt;
        }

        /// <summary>
        /// Generates short-answer quiz questions based on the subject, difficulty, and number of questions.
        /// </summary>
        /// <param name="subject">The subject for the quiz questions.</param>
        /// <param name="difficulty">The difficulty level of the quiz questions.</param>
        /// <param name="numberOfQuestions">The number of quiz questions to generate.</param>
        /// <returns>A string containing the generated short-answer quiz questions.</returns>
        [KernelFunction("generate_short_answer_quiz")]
        [Description("Generate short-answer quiz questions based on the subject, difficulty, and number of questions.")]
        public async Task<string> GenerateShortAnswerQuiz(string subject, string difficulty, int numberOfQuestions)
        {
            string? promptPath = _configuration["Prompts"];

            if (string.IsNullOrWhiteSpace(promptPath))
            {
                throw new ArgumentNullException(nameof(promptPath));
            }
            // Load the example and rules prompts
            string examplePath = Path.Combine(promptPath, "ShortAnswerPrompt.txt");
            if (!File.Exists(examplePath))
            {
                throw new FileNotFoundException($"The example prompt file path is not found at path:{examplePath}");
            }

            string rulesPath = Path.Combine(promptPath, "RulesPrompt.txt");
            if (!File.Exists(rulesPath))
            {
                throw new FileNotFoundException($"The rules prompt file path is not found at path:{rulesPath}");
            }

            string exampleDescription = await File.ReadAllTextAsync(examplePath).ConfigureAwait(false);          
            string rulesDescription = await File.ReadAllTextAsync(rulesPath).ConfigureAwait(false);

            // Construct the final prompt
            string finalPrompt = $"""
        You are an AI assistant skilled at generating high-quality quiz content. 
        Your task is to create short answer questions as described below.

        ### Instructions:
        - Subject: {subject}
        - Difficulty: {difficulty}
        - Number of Questions: {numberOfQuestions}
       
        ### Examples:
        {exampleDescription}

        ### Rules:
        {rulesDescription}

        Follow the examples closely to generate accurate and relevant MCQs.
        Ensure the output only contains the questions and answers as per the examples provided.
        """;
            return finalPrompt;
        }

        /// <summary>
        /// Generates long-answer quiz questions based on the subject, difficulty, and number of questions.
        /// </summary>
        /// <param name="subject">The subject for the quiz questions.</param>
        /// <param name="difficulty">The difficulty level of the quiz questions.</param>
        /// <param name="numberOfQuestions">The number of quiz questions to generate.</param>
        /// <returns>A string containing the generated long-answer quiz questions.</returns>
        [KernelFunction("generate_long_answer_quiz")]
        [Description("Generate long-answer quiz questions based on the subject, difficulty, and number of questions.")]
        public async Task<string> GenerateLongAnswerQuiz(string subject, string difficulty, int numberOfQuestions)
        {

            string? promptPath = _configuration["Prompts"];

            if (string.IsNullOrWhiteSpace(promptPath))
            {
                throw new ArgumentNullException(nameof(promptPath));
            }
            // Load the example and rules prompts
            string examplePath = Path.Combine(promptPath, "LongAnswerPrompt.txt");
            if (!File.Exists(examplePath))
            {
                throw new FileNotFoundException($"The example prompt file path is not found at path:{examplePath}");
            }

            string rulesPath = Path.Combine(promptPath, "RulesPrompt.txt");
            if (!File.Exists(rulesPath))
            {
                throw new FileNotFoundException($"The rules prompt file path is not found at path:{rulesPath}");
            }

            string exampleDescription = await File.ReadAllTextAsync(examplePath).ConfigureAwait(false);
            string rulesDescription = await File.ReadAllTextAsync(rulesPath).ConfigureAwait(false);

            // Construct the final prompt
            string finalPrompt = $"""
         You are an AI assistant skilled at generating high-quality quiz content. 
         Your task is to create long answer questions as described below.

        ### Instructions:
        - Subject: {subject}
        - Difficulty: {difficulty}
        - Number of Questions: {numberOfQuestions}
       
        ### Examples:
        {exampleDescription}

        ### Rules:
        {rulesDescription}

        Follow the examples closely to generate accurate and relevant MCQs.
        Ensure the output only contains the questions and answers as per the examples provided.
        """;
            return finalPrompt;
        }
    }
}
