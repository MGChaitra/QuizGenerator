namespace EducationWebApp.Contracts
{
    /// <summary>
    /// Interface for GenerateQuizPDFContentService.
    /// </summary>
    public interface IGenerateQuizPDFContentService
    {
        /// <summary>
        /// Generates a PDF containing the quiz content for a specified subject.
        /// </summary>
        /// <param name="subject">The subject for which the quiz content is generated.</param>
        /// <param name="formattedQuizContent">The formatted quiz content to be included in the PDF.</param>
        /// <returns>A byte array representing the generated PDF content.</returns>
        byte[] GenerateQuizPDFContent(string subject, string formattedQuizContent);
    }
}
