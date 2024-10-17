using DDDSample1.Domain.BackOfficeUsers;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public interface BackOfficeUserRepository: IRepository<BackOfficeUser,BackOfficeUserId>
    {
    }
}