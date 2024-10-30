using DDDSample1.Domain.OperationType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDNetCore.IRepos
{
    public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeID>
    {
        new Task<List<OperationType>> GetAllAsync();
        new Task<OperationType> GetByIdAsync(OperationTypeID id);
        new Task AddAsync(OperationType operationType);
        new void Remove(OperationType operationType);
        Task UpdateAsync(OperationType operationType);
    }
}