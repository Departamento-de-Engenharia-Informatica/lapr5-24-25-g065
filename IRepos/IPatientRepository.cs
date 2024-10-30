using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;

namespace DDDNetCore.IRepos
{
    public interface IPatientRepository : IRepository<Patient, PatientId>
    {
        Task DeleteAsync(Patient patient);
        new Task<Patient> GetByIdAsync(PatientId id);
        new Task<List<Patient>> GetAllAsync();
        new Task AddAsync(Patient patient);
        // If you need to update, you could have something like this
        Task UpdateAsync(Patient patient);
    }
}
