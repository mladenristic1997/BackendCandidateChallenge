namespace QuizClient;

public record Answer
{
    public int Id;
    public string Text;
    public int QuestionId;
}