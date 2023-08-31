namespace PulseSurveyV2.Models;

public class Answer
{
    public long AnswerId { get; set; }
    public long SurveyId { get; set; }
    public long UserId { get; set; }
    public int AnswerRating { get; set; }
    public DateTime AnswerDate { get; set; }
}