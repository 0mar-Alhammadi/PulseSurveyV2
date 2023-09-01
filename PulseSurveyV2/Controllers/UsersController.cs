using Microsoft.AspNetCore.Mvc;
using PulseSurveyV2.Models;

namespace PulseSurveyV2.Controllers
{
    using Threenine.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork<UnifiedContext> _uow;

        public UsersController(IUnitOfWork<UnifiedContext> uow)
        {
            _uow = uow;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers()
        {
            var repository = _uow.GetRepositoryAsync<User>();
            if (repository == null)
            {
                return NotFound();
            }

            var users = await repository.GetListAsync();

            return Ok(users.Items);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var repository = _uow.GetRepositoryAsync<User>();
            if (repository == null)
            {
                return NotFound();
            }

            var user = await repository.SingleOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDto)
        {
            var repository = _uow.GetRepositoryAsync<User>();

            if (repository == null)
            {
                return Problem("Entity set 'UnifiedContext.Users'  is null.");
            }

            var user = new User
            {
                UserName = userDto.UserName,
                IsCreator = userDto.IsCreator,
            };

            await repository.InsertAsync(user);
            await _uow.CommitAsync();

            return CreatedAtAction("GetUser", new {id = user.UserId}, user);
        }
    }
}
