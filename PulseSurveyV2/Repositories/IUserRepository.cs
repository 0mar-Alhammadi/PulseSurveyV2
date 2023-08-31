namespace PulseSurveyV2.Repositories;

using PulseSurveyV2.Models;

public interface IUserRepository
{
    IEnumerable<User> GetUsersAsync(CancellationToken cancellationToken = default);
    User GetUserById(long id, CancellationToken cancellationToken = default);
    User InsertUser(User user, CancellationToken cancellationToken = default);
}