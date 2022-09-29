namespace QuizService.Model.Persistence;

public record Quiz
{
    public int Id { get; set; }
    public string Title { get; set; }
}