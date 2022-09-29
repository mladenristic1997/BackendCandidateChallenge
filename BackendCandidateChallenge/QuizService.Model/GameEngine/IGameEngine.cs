using System.Threading.Tasks;

namespace QuizService.Model.GameEngine
{
    public interface IGameEngine
    {
        Task<QuizResponse> AttemptAnswer(AnswerAttempt attempt);
    }
}
