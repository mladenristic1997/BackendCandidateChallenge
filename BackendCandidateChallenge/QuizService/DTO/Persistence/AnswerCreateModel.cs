namespace QuizService.Api;

public class AnswerCreateModel
{
    public AnswerCreateModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}