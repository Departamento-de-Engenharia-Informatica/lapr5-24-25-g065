using DDDNetCore.IRepos;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Infrastructure.Shared;

namespace DDDSample1.Infrastructure.OperationTypes
{
    public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeID>,IOperationTypeRepository
    {
        public OperationTypeRepository(DDDSample1DbContext context):base(context.OperationTypes)
        {
           
        }
    }
}