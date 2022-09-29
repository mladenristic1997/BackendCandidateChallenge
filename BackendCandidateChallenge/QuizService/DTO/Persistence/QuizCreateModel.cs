namespace QuizService.Api;

public class QuizCreateModel
{
    public QuizCreateModel(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}