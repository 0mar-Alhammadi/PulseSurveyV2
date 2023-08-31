namespace PulseSurveyV2.Models;

public class SurveyDTO
{
    public long UserId { get; set; }
    public string SurveyTitle { get; set; } = null!;
    public string SurveyQuestion { get; set; } = null!;
}