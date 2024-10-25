using DDDSample1.Domain.Patients;

namespace DDDNetCore.IRepos
{
    public interface IPatientRepository : IRepository<Patient, PatientId>
    {
    }
}