namespace PulseSurveyV2.Repositories;

using PulseSurveyV2.Models;

public class UserRepository : IUserRepository
{
    public IEnumerable<User> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public User InsertUser(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
