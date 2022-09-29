namespace QuizService.Api.DTO.Game
{
    public class AttemptAnswerModel
    {
        public AttemptAnswerModel(int questionId, int answerId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
