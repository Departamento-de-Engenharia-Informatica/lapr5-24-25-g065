using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;

namespace DDDNetCore.IRepos
{
    public interface IPatientRepository : IRepository<Patient, PatientId>
    {
        Task DeleteAsync(Patient patient);
        
        // Add method signatures for CRUD operations if not already in IRepository
        Task<Patient> GetByIdAsync(PatientId id);
        Task<List<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        // If you need to update, you could have something like this
        Task UpdateAsync(Patient patient);
    }
}
