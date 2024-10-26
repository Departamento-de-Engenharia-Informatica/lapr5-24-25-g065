using DDDSample1.Domain.Users;
using System.Threading.Tasks;


namespace DDDNetCore.IRepos
{
    public interface IUserRepository : IRepository<User, UserId>
    {
       Task<User> GetByEmailAsync(string email);
    }
}