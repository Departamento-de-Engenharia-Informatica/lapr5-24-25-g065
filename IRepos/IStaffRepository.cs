using DDDSample1.Domain.Staffs;

namespace DDDNetCore.IRepos
{
    public interface IStaffRepository : IRepository<Staff, StaffId>
    {
    }
}