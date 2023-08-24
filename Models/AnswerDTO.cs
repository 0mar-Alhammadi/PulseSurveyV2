namespace PulseSurveyV2.Models;

public class AnswerDTO
{
    public long SurveyId { get; set; }
    public long UserId { get; set; }
    public int AnswerRating { get; set; }
}