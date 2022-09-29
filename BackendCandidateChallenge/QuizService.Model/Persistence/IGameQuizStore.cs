using System.Threading.Tasks;

namespace QuizService.Model.Persistence
{
    public interface IGameQuizStore
    {
        Task<Question> GetQuestion(int id)
    }
}
