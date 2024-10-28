using DDDNetCore.Domain.OperationRequest;
using DDDNetCore.DTOs.OperationRequest;
using DDDSample1.Domain.Patients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDNetCore.IRepos
{
    public interface IOperationRequestRepository{

        Task<IEnumerable<OperationRequest>> GetAllOperationRequest();
        Task<OperationRequest> GetOperationRequestByPriority(int priority);
        Task AddOperationRequest(OperationRequest operationRequest);
        Task UpdateOperationRequest(OperationRequest operationRequest);
        Task DeleteOperationRequestByPriority(int priority);
        
    }
}
