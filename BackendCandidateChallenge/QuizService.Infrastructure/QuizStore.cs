using Dapper;
using QuizService.Model.Persistence;
using QuizService.Model.Persistence.Exceptions;
using QuizService.Model.WriteModel;
using System.Data;

namespace QuizService.Infrastructure
{
    public class QuizStore : IQuizStore
    {
        readonly IDbConnection _connection;

        public QuizStore(IDbConnection conn)
            => _connection = conn;

        public async Task<object> CreateAnswer(AnswerCreate cmd)
        {
            const string sql = "INSERT INTO Answer (Text, QuestionId) VALUES(@Text, @QuestionId); SELECT LAST_INSERT_ROWID();";
            var id = await _connection.ExecuteScalarAsync(sql, new { Text = cmd.Text, QuestionId = cmd.QuestionId });
            return id;
        }

        public async Task<object> CreateQuestion(QuestionCreate cmd)
        {
            const string sql = "INSERT INTO Question (Text, QuizId) VALUES(@Text, @QuizId); SELECT LAST_INSERT_ROWID();";
            var id = await _connection.ExecuteScalarAsync(sql, new { Text = cmd.Text, QuizId = cmd.QuizId });
            return id;
        }

        public async Task<object> CreateQuiz(QuizCreate cmd)
        {
            var sql = $"INSERT INTO Quiz (Title) VALUES('{cmd.Title}'); SELECT LAST_INSERT_ROWID();";
            var id = await _connection.ExecuteScalarAsync(sql);
            return id;
        }

        public async Task DeleteAnswer(int id)
        {
            const string sql = "DELETE FROM Answer WHERE Id = @AnswerId";
            await _connection.ExecuteScalarAsync(sql, new { AnswerId = id });
        }

        public async Task DeleteQuestion(int id)
        {
            const string sql = "DELETE FROM Question WHERE Id = @QuestionId";
            await _connection.ExecuteScalarAsync(sql, new { QuestionId = id });
        }

        public async Task DeleteQuiz(int id)
        {
            const string sql = "DELETE FROM Quiz WHERE Id = @Id";
            int rowsDeleted = await _connection.ExecuteAsync(sql, new { Id = id });
            if (rowsDeleted == 0)
                throw new NotFound();
        }

        public async Task<Model.ReadModel.Quiz> GetQuiz(int id)
        {
            const string quizSql = "SELECT * FROM Quiz WHERE Id = @Id;";
            var quiz = await _connection.QuerySingleAsync<Quiz>(quizSql, new { Id = id });
            if (quiz == null)
                throw new NotFound();
            const string questionsSql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
            var questions = _connection.Query<Question>(questionsSql, new { QuizId = id });
            const string answersSql = "SELECT a.Id, a.Text, a.QuestionId FROM Answer a INNER JOIN Question q ON a.QuestionId = q.Id WHERE q.QuizId = @QuizId;";
            var answers = _connection.Query<Answer>(answersSql, new { QuizId = id })
                .Aggregate(new Dictionary<int, IList<Answer>>(), (dict, answer) => {
                    if (!dict.ContainsKey(answer.QuestionId))
                        dict.Add(answer.QuestionId, new List<Answer>());
                    dict[answer.QuestionId].Add(answer);
                    return dict;
                });
            return new Model.ReadModel.Quiz
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = questions.Select(question => new Model.ReadModel.Quiz.Question
                {
                    Id = question.Id,
                    Text = question.Text,
                    Answers = answers.ContainsKey(question.Id)
                        ? answers[question.Id].Select(answer => new Model.ReadModel.Quiz.Answer
                        {
                            Id = answer.Id,
                            Text = answer.Text
                        })
                        : new Model.ReadModel.Quiz.Answer[0],
                }),
                Links = new Dictionary<string, string>
                {
                    {"self", $"/api/quizzes/{id}"},
                    {"questions", $"/api/quizzes/{id}/questions"}
                }
            };
        }

        public async Task<IEnumerable<Model.ReadModel.QuizMenuItem>> GetQuizMenuItems()
        {
            const string sql = "SELECT * FROM Quiz;";
            var quizzes = await _connection.QueryAsync<Quiz>(sql);
            return quizzes.Select(quiz =>
                new Model.ReadModel.QuizMenuItem
                {
                    Id = quiz.Id,
                    Title = quiz.Title
                });
        }

        public async Task UpdateAnswer(AnswerUpdate cmd)
        {
            const string sql = "UPDATE Answer SET Text = @Text WHERE Id = @AnswerId";
            int rowsUpdated = await _connection.ExecuteAsync(sql, new { AnswerId = cmd.Id, Text = cmd.Text });
            if (rowsUpdated == 0)
                throw new NotFound();
        }

        public async Task SetCorrectAsnwerInQuestion(QuestionUpdate cmd)
        {
            const string sql = "UPDATE Question SET CorrectAnswerId = @CorrectAnswerId WHERE Id = @QuestionId";
            int rowsUpdated = await _connection.ExecuteAsync(sql, new 
            { 
                QuestionId = cmd.Id, 
                CorrectAnswerId = cmd.CorrectAnswerId
            });
            if (rowsUpdated == 0)
                throw new NotFound();
        }
        
        public async Task UpdateQuiz(QuizUpdate cmd)
        {
            const string sql = "UPDATE Quiz SET Title = @Title WHERE Id = @Id";
            int rowsUpdated = await _connection.ExecuteAsync(sql, new { Id = cmd.Id, Title = cmd.Title });
            if (rowsUpdated == 0)
                throw new NotFound();
        }
    }
}