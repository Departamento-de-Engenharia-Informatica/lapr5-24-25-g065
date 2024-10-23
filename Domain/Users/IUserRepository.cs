using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public interface IUserRepository: IRepository<User,UserId>
    {
    }
}