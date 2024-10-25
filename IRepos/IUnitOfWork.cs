using System.Threading.Tasks;

namespace DDDNetCore.IRepos
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}