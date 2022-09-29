namespace QuizService.Model.WriteModel;

public class QuestionUpdate
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int CorrectAnswerId { get; set; }
}