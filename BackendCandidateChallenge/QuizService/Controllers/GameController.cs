using Microsoft.AspNetCore.Mvc;
using QuizService.Api.DTO.Game;
using QuizService.Model.GameEngine;
using System.Threading.Tasks;

namespace QuizService.Api.Controllers
{
    [Route("api/quizzes/game")]
    public class GameController : Controller
    {
        readonly IGameEngine _gameEngine;

        public GameController(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        [HttpPost]
        public async Task<QuizResponse> AttemptQuizResponse([FromBody]AttemptAnswerModel attempt)
        {
            var resp = await _gameEngine.AttemptAnswer(new AnswerAttempt
            {
                AnswerId = attempt.AnswerId,
                QuestionId = attempt.QuestionId
            }).ConfigureAwait(false);
            return resp;
        }
    }
}
