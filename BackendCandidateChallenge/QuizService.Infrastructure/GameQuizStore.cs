using Dapper;
using QuizService.Model.Persistence;
using QuizService.Model.Persistence.Exceptions;
using System.Data;

namespace QuizService.Infrastructure
{
    public class GameQuizStore : IGameQuizStore
    {
        readonly IDbConnection _connection;

        public GameQuizStore(IDbConnection conn)
            => _connection = conn;

        public async Task<Question> GetQuestion(int id)
        {
            const string questionSql = "SELECT * FROM Question WHERE Id = @Id";
            var question = await _connection.QuerySingleAsync<Question>(questionSql, new { Id = id });
            if (question == null)
                throw new NotFound();
            return question;
        }
    }
}