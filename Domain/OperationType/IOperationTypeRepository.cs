using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationTypes
{
    public interface IOperationTypeRepository: IRepository<OperationType,OperationTypeID>
    {
    }
}