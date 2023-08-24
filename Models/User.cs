namespace PulseSurveyV2.Models;

public class User
{
    public long UserId { get; set; }
    public bool IsCreator { get; set; }
    public string UserName { get; set; } = null!;
}