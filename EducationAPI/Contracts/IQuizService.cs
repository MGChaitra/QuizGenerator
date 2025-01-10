using ModelDLL.Models;
namespace EducationAPI.Contracts
{
    public interface IQuizService
    {
        Task<string> GenerateQuizContentAsync(RequestModel request, string questionType);
    }
}
