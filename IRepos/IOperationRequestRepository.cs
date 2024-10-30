using DDDNetCore.Domain;
using DDDNetCore.Domain.OperationRequestDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDNetCore.IRepos
{
    public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestID>
    {
        new Task<IEnumerable<OperationRequest>> GetAllAsync();
        new Task<OperationRequest> GetByIdAsync(OperationRequestID operationRequestID);
        new Task AddAsync(OperationRequest operationRequest);
        new Task Remove(OperationRequest operationRequest);
        Task UpdateAsync(OperationRequest operationRequest);
    }
}
