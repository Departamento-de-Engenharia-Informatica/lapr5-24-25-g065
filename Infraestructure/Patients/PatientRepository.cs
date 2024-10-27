using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Patients
{
    public class PatientRepository : BaseRepository<Patient, PatientId>, IPatientRepository
    {
        private readonly DDDSample1DbContext _context;

        public PatientRepository(DDDSample1DbContext context) : base(context.Patients)
        {
            _context = context;
        }

        public async Task<Patient> GetByIdAsync(PatientId id)
        {
            return await _context.Patients
                .Include(p => p.AppointmentHistory) // Include related appointments if necessary
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.AppointmentHistory) // Include related appointments if necessary
                .ToListAsync();
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync(); // Ensure changes are saved to the database
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync(); // Ensure changes are saved to the database
        }

        public async Task DeleteAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync(); // Ensure changes are saved to the database
        }

        public async Task<Patient> GetByUserIDAsync(Guid userId)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
