namespace PulseSurveyV2.Models;

public class Survey
{
    public long SurveyId { get; set; }
    public long UserId { get; set; }
    public string SurveyTitle { get; set; } = null!;
    public string SurveyQuestion { get; set; } = null!;
    public DateTime SurveyDate { get; set; }
}