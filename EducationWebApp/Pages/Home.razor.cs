using EducationWebApp.Services;
using Microsoft.JSInterop;
using ModelDLL.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace EducationWebApp.Pages
{
    public partial class Home
    {
        // Create seperate razor.cs file for this code - completed
        // Try to get the list from enum rather than hardcoding - completed
        private RequestModel request = new RequestModel();
        private bool isLoading = false;
        private string formattedQuizContent = string.Empty;
      
        private async Task GenerateQuiz(string quizType)
        {
            isLoading = true;
            string jsonResponse = quizType switch
            {

                // Instead of directly invoking the GetMCQQuizContentAsync,GetShortAnswerQuizContentAsync,GetLongAnswerQuizContentAsync methods you should create the interface for IOpenAIService and implement it - completed
                // Can pass the object to the method instead of passing each parameters seperately - completed

                "MCQ" => await OpenAIService.GetMCQQuizContentAsync(request),
                "ShortAnswer" => await OpenAIService.GetShortAnswerQuizContentAsync(request),
                "LongAnswer" => await OpenAIService.GetLongAnswerQuizContentAsync(request),
                _ => throw new InvalidOperationException("Invalid quiz type")
            };

            formattedQuizContent = jsonResponse ?? "No content available."; ;
            isLoading = false;
        }
        private void HandleValidSubmit() { }
        private async Task DownloadQuizAsPDF()
        {
            var pdfContent = gen.GenerateQuizPDFContent(request.Subject, formattedQuizContent);
            var fileName = $"{request.Subject}_Quiz.pdf";
            await JSRuntime.InvokeVoidAsync("downloadFile", fileName, Convert.ToBase64String(pdfContent));
        }
        private async Task OnGenerateMCQQuiz() => await GenerateQuiz("MCQ");
        private async Task OnGenerateShortQuiz() => await GenerateQuiz("ShortAnswer");
        private async Task OnGenerateLongQuiz() => await GenerateQuiz("LongAnswer");
    }
}