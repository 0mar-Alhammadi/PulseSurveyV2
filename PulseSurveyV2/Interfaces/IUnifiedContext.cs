using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Models;

namespace PulseSurveyV2.Interfaces;

    public interface IUnifiedContext
    {
        DbSet<Survey> Surveys { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }