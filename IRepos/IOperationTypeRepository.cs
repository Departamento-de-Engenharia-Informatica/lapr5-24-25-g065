using DDDSample1.Domain.OperationType;

namespace DDDNetCore.IRepos
{
    public interface IOperationTypeRepository : IRepository<OperationType, OperationTypeID>
    {
    }
}