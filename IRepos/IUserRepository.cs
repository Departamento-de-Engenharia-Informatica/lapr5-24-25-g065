using DDDSample1.Domain.Users;

namespace DDDNetCore.IRepos
{
    public interface IUserRepository : IRepository<User, UserId>
    {
    }
}