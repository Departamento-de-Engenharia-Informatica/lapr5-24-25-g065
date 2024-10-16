using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specializations
{
    public interface SpecializationRepository: IRepository<Specialization,SpecializationId>
    {
    }
}