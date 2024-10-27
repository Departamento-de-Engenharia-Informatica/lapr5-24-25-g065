using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.DTOs.Patient;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPatientRepository patientRepository;
        private readonly IUserRepository userRepository; // New dependency for User management

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo, IUserRepository userRepo)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = repo;
            this.userRepository = userRepo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this.patientRepository.GetAllAsync();
            return list.ConvertAll(patient => new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber
            ));
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await this.patientRepository.GetByIdAsync(id);
            return patient == null ? null : new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber
            );
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data");

            var patient = new Patient(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.Allergies,
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber
            );

            await this.patientRepository.AddAsync(patient);
            await this.unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber
            );
        }

        public async Task<PatientDto> UpdateAsync(UpdatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data");

            var patient = await this.patientRepository.GetByIdAsync(new PatientId(dto.Id));
            if (patient == null) return null;

            patient.ChangeFirstName(dto.Firstname);
            patient.ChangeLastName(dto.LastName);
            patient.ChangeFullName(dto.FullName);
            patient.ChangeGender(dto.Gender);
            patient.ChangeEmergencyContact(dto.EmergencyContact);
            patient.ChangeAllergies(dto.Allergies);
            patient.ChangeDateOfBirth(dto.DateOfBirth);
            patient.ChangeMedicalRecordNumber(dto.MedicalRecordNumber);

            // Update related User fields if applicable
            var user = await this.userRepository.GetByIdAsync(new UserId(dto.Id));
            if (user != null)
            {
                user.ChangeUserName(dto.UserName);
                user.ChangeEmail(dto.Email);
                await this.unitOfWork.CommitAsync(); // Commit user changes
            }

            await this.unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber
            );
        }

        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await this.patientRepository.GetByIdAsync(id);
            if (patient == null) return null;

            this.patientRepository.Remove(patient);
            await this.unitOfWork.CommitAsync();

            return new PatientDto(
                patient.Id.AsGuid(),
                patient.Firstname,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber
            );
        }
    }
}
