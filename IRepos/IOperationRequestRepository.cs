using DDDNetCore.Domain;
using DDDNetCore.Domain.OperationRequestDomain;

namespace DDDNetCore.IRepos
{
    public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestID>
    {
    }
}
