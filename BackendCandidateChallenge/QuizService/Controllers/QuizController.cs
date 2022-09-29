using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuizService.Model.Persistence;
using QuizService.Model.ReadModel;
using System.Threading.Tasks;
using QuizService.Api;

namespace QuizService.Controllers;

[Route("api/quizzes")]
public class QuizController : Controller
{
    readonly IQuizStore _quizStore;

    public QuizController(IQuizStore store)
    {
        _quizStore = store;
    }

    // GET api/quizzes
    [HttpGet]
    public async Task<IEnumerable<QuizMenuItem>> GetQuizMenuItems()
        => await _quizStore.GetQuizMenuItems().ConfigureAwait(false);

    // GET api/quizzes/5
    [HttpGet("{id}")]
    public async Task<Model.ReadModel.Quiz> GetQuiz(int id)
        => await _quizStore.GetQuiz(id).ConfigureAwait(false);

    // POST api/quizzes
    [HttpPost]
    public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateModel value)
    {
        var id = await _quizStore.CreateQuiz(new Model.WriteModel.QuizCreate
        {
            Title = value.Title
        }).ConfigureAwait(false);
        return Created($"/api/quizzes/{id}", null);
    }

    // PUT api/quizzes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuiz(int id, [FromBody]QuizUpdateModel value)
    {
        await _quizStore.UpdateQuiz(new Model.WriteModel.QuizUpdate
        {
            Id = id,
            Title = value.Title
        }).ConfigureAwait(false);
        return Ok();
    }

    // DELETE api/quizzes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        await _quizStore.DeleteQuiz(id).ConfigureAwait(false);
        return Ok();
    }

    // POST api/quizzes/5/questions
    [HttpPost]
    [Route("{id}/questions")]
    public async Task<IActionResult> CreateQuestion(int id, [FromBody]QuestionCreateModel value)
    {
        await _quizStore.CreateQuestion(new Model.WriteModel.QuestionCreate
        {
            QuizId = id,
            Text = value.Text
        }).ConfigureAwait(false);
        return Ok();
    }

    // PUT api/quizzes/questions/6
    [HttpPut("/questions/{qid}")]
    public async Task<IActionResult> SetCorrectAnswerInQuestionAsync(int qid, [FromBody]QuestionSetCorrectAnswerModel value)
    {
        await _quizStore.SetCorrectAsnwerInQuestion(new Model.WriteModel.QuestionUpdate
        {
            Id = qid,
            CorrectAnswerId = value.CorrectAnswerId
        }).ConfigureAwait(false);
        return Ok();
    }

    // DELETE api/quizzes/5/questions/6
    [HttpDelete]
    [Route("questions/{qid}")]
    public async Task<IActionResult> DeleteQuestion(int qid)
    {
        await _quizStore.DeleteQuestion(qid).ConfigureAwait(false);
        return Ok();
    }

    // POST api/quizzes/questions/6/answers
    [HttpPost]
    [Route("questions/{qid}/answers")]
    public async Task<IActionResult> CreateAnswer(int qid, [FromBody]AnswerCreateModel value)
    {
        await _quizStore.CreateAnswer(new Model.WriteModel.AnswerCreate
        {
            QuestionId = qid,
            Text = value.Text,
        }).ConfigureAwait(false);
        return Ok();
    }

    // PUT api/quizzes/questions/answers/7
    [HttpPut("questions/answers/{aid}")]
    public async Task<IActionResult> UpdateAnswer(int aid, [FromBody]AnswerUpdateModel value)
    {
        await _quizStore.UpdateAnswer(new Model.WriteModel.AnswerUpdate
        {
            Id = aid,
            Text = value.Text
        }).ConfigureAwait(false);
        return Ok();
    }

    // DELETE api/quizzes/questions/answers/7
    [HttpDelete]
    [Route("questions/answers/{aid}")]
    public async Task<IActionResult> DeleteAnswer(int aid)
    {
        await _quizStore.DeleteAnswer(aid).ConfigureAwait(false);
        return Ok();
    }
}