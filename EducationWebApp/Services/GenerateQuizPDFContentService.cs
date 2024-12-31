using System.IO;
using EducationWebApp.Contracts;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;

namespace EducationWebApp.Services
{

    /// <summary>
    /// Service to generate PDF content for quizzes.
    /// </summary>
    public class GenerateQuizPDFContentService: IGenerateQuizPDFContentService
    {
        // Add Ilogger for logging - Completed
        private readonly ILogger<GenerateQuizPDFContentService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateQuizPDFContentService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public GenerateQuizPDFContentService(ILogger<GenerateQuizPDFContentService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generates PDF content for a quiz.
        /// </summary>
        /// <param name="subject">The subject of the quiz.</param>
        /// <param name="formattedQuizContent">The formatted quiz content.</param>
        /// <returns>A byte array representing the PDF content.</returns>
        public byte[] GenerateQuizPDFContent(string subject, string formattedQuizContent)
        {

            // Add null check for subject and formattedQuizContent - Completed
            // Add try catch to pdf generation logic - Completed
            // Instead of using iText.Kernel.Pdf namespace try to import the namespace at the start with using iText.Kernel.Pdf;using iText.Layout; using iText.Layout.Element; - Completed
            if (string.IsNullOrEmpty(subject))
            {
                _logger.LogError("Subject is null or empty.");
                throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));
            }

            if (string.IsNullOrEmpty(formattedQuizContent))
            {
                _logger.LogError("Formatted quiz content is null or empty.");
                throw new ArgumentException("Formatted quiz content cannot be null or empty.", nameof(formattedQuizContent));
            }

            try
            {
                using var memoryStream = new MemoryStream();
                var writer = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(writer);
                var document = new Document(pdfDocument);

                document.Add(new Paragraph($"Quiz on {subject}")
                    .SetFontSize(20));

                document.Add(new Paragraph(formattedQuizContent)
                    .SetFontSize(12));

                document.Close();

                _logger.LogInformation("PDF content generated successfully for subject: {Subject}", subject);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF content for subject: {Subject}", subject);
                throw;
            }
        }
    }
}
