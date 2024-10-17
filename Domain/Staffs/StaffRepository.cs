using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs
{
    public interface StaffRepository: IRepository<Staff,StaffId>
    {
    }
}