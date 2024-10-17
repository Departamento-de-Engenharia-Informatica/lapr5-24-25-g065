using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public interface PatientRepository: IRepository<Patient,PatientId>
    {
    }
}