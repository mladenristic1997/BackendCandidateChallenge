namespace QuizService.Model.Persistence;

public record Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; }
}