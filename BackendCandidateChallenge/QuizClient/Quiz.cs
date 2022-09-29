namespace QuizClient;

public record Quiz
{
    public int Id;
    public string Title;
    public static Quiz NotFound => default(Quiz);
}