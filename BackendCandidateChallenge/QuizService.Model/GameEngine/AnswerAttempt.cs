namespace QuizService.Model.GameEngine
{
    public record AnswerAttempt
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
