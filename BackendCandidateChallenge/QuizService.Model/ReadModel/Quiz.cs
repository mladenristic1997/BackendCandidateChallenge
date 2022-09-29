using System.Collections.Generic;

namespace QuizService.Model.ReadModel;

public class Quiz
{
    public long Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    public IDictionary<string, string> Links { get; set; }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}