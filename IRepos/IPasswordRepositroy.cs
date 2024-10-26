using DDDSample1.Domain.Passwords;

namespace DDDNetCore.IRepos
{
    public interface IPasswordRepository : IRepository<Password, PasswordId>
    {
    }
}