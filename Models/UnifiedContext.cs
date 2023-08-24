using Microsoft.EntityFrameworkCore;

namespace PulseSurveyV2.Models;

public class UnifiedContext : DbContext
{
    public UnifiedContext(DbContextOptions<UnifiedContext> options)
        : base(options)
    {
    }
    
    public DbSet<Survey> Surveys { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}

// dotnet aspnet-codegenerator controller -name AnswersController -async -api -m AnswerDTO -dc UnifiedContext -outDir Controllers
// dotnet aspnet-codegenerator controller -name SurveysController -async -api -m Survey -dc UnifiedContext -outDir Controllers
// dotnet aspnet-codegenerator controller -name UsersController -async -api -m User -dc UnifiedContext -outDir Controllers
