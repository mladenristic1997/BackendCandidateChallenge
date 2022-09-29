using System.Threading.Tasks;

namespace QuizService.Model.Persistence
{
    public interface IQuizStore
    {
        Task<IEnumerable<ReadModel.QuizMenuItem>> GetQuizMenuItems();
        Task<ReadModel.Quiz> GetQuiz(int id);
        Task<object> CreateQuiz(WriteModel.QuizCreate cmd);
        Task UpdateQuiz(WriteModel.QuizUpdate cmd);
        Task DeleteQuiz(int id);
        Task<object> CreateQuestion(WriteModel.QuestionCreate cmd);
        Task SetCorrectAsnwerInQuestion(WriteModel.QuestionUpdate cmd);
        Task DeleteQuestion(int id);
        Task<object> CreateAnswer(WriteModel.AnswerCreate cmd);
        Task UpdateAnswer(WriteModel.AnswerUpdate cmd);
        Task DeleteAnswer(int id);
    }
}
