using QuizService.Model.GameEngine;
using QuizService.Model.Persistence;

namespace QuizService.Game
{
    public class GameEngine : IGameEngine
    {
        readonly IGameQuizStore _store;

        public GameEngine(IGameQuizStore store)
        {
            _store = store;
        }

        public async Task<QuizResponse> AttemptAnswer(AnswerAttempt attempt)
        {
            var answer = await _store.GetQuestion(attempt.QuestionId).ConfigureAwait(false);
            return new QuizResponse
            {
                IsAnswerCorrect = answer.CorrectAnswerId == attempt.AnswerId
            };
        }
    }
}