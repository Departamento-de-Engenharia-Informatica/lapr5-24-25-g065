using DDDSample1.Domain.Specializations;

namespace DDDNetCore.IRepos
{
    public interface ISpecializationRepository : IRepository<Specialization, SpecializationId>
    {
    }
}