using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.DTOs.Patient;
using DDDSample1.Domain.Shared;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPatientRepository patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepo)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = patientRepo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var patients = await patientRepository.GetAllAsync();
            return patients.Select(patient => new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.PhoneNumber,
                patient.Email // Removed UserId
            )).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            return patient == null ? null : new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.PhoneNumber,
                patient.Email // Removed UserId
            );
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data");

            var allergies = dto.Allergies?.ToList();

            var patient = new Patient(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                allergies,
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber,
                dto.PhoneNumber,
                dto.Email // Removed UserId
            );

            await patientRepository.AddAsync(patient);
            await unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.PhoneNumber,
                patient.Email // Removed UserId
            );
        }

        public async Task<PatientDto> UpdateAsync(UpdatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data");

            var patient = await patientRepository.GetByIdAsync(new PatientId(dto.Id));
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            patient.Update(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.Allergies,
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber,
                dto.PhoneNumber,
                dto.Email // Removed UserId
            );

            await unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.PhoneNumber,
                patient.Email // Removed UserId
            );
        }

        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            await patientRepository.DeleteAsync(patient);
            await unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.PhoneNumber,
                patient.Email // Removed UserId
            );
        }

        public async Task<List<PatientDto>> SearchPatientsAsync(string name, DateTime? dateOfBirth, string medicalRecordNumber, string phoneNumber, string email, int pageNumber, int pageSize)
        {
            var patients = await patientRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                patients = patients.Where(p => 
                    p.Firstname.Contains(name, StringComparison.OrdinalIgnoreCase) || 
                    p.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (dateOfBirth.HasValue)
            {
                patients = patients.Where(p => p.DateOfBirth?.Date == dateOfBirth.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(medicalRecordNumber))
            {
                patients = patients.Where(p => p.MedicalRecordNumber == medicalRecordNumber).ToList();
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                patients = patients.Where(p => p.PhoneNumber == phoneNumber).ToList();
            }

            // Filter by email if provided
            if (!string.IsNullOrEmpty(email))
            {
                patients = patients.Where(p => p.Email == email).ToList();
            }

            var paginatedPatients = patients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(patient => new PatientDto(
                    patient.Id.AsGuid(),
                    patient.Firstname,
                    patient.LastName,
                    patient.FullName,
                    patient.Gender,
                    patient.Allergies,
                    patient.EmergencyContact,
                    patient.DateOfBirth,
                    patient.MedicalRecordNumber,
                    patient.PhoneNumber,
                    patient.Email // Removed UserId
                ))
                .ToList();

            return paginatedPatients;
        }
    }
}
