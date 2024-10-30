using DDDNetCore.IRepos;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDDSample1.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User, UserId>,IUserRepository
    {
        private readonly DDDSample1DbContext _context;
        public UserRepository(DDDSample1DbContext context):base(context.Users)
        {
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}