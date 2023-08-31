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

// todo
// unit tests
// docker