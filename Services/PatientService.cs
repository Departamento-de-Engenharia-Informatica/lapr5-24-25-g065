using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.DTOs.Patient;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPatientRepository patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = repo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this.patientRepository.GetAllAsync();

            List<PatientDto> listDto = list.ConvertAll(patient =>
                new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender,
                    patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber));

            return listDto;
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await this.patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender,
                patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber);
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
{
    // Validate DTO
    if (dto == null || string.IsNullOrEmpty(dto.Firstname) || string.IsNullOrEmpty(dto.LastName) ||
        string.IsNullOrEmpty(dto.FullName) || string.IsNullOrEmpty(dto.Gender) ||
        string.IsNullOrEmpty(dto.EmergencyContact) || string.IsNullOrEmpty(dto.MedicalRecordNumber))
    {
        throw new ArgumentException("Invalid patient data");
    }

    // Create a new patient from the DTO
    var patient = new Patient(
        dto.Firstname,
        dto.LastName,
        dto.FullName,
        dto.Gender,
        dto.Allergies, // Directly use the List<string> allergies
        dto.EmergencyContact,
        dto.DateOfBirth, // Assuming DateOfBirth is already a DateTime?
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
        patient.DateOfBirth, // Ensure this is of type DateTime?
        patient.MedicalRecordNumber
    );
}

     public async Task<PatientDto> UpdateAsync(UpdatePatientDTO dto)
{
    // Validate DTO
    if (dto == null || string.IsNullOrEmpty(dto.Firstname) || string.IsNullOrEmpty(dto.LastName) ||
        string.IsNullOrEmpty(dto.FullName) || string.IsNullOrEmpty(dto.Gender) ||
        string.IsNullOrEmpty(dto.EmergencyContact) || string.IsNullOrEmpty(dto.MedicalRecordNumber))
    {
        throw new ArgumentException("Invalid patient data");
    }

    // Fetch the patient by ID
    var patient = await this.patientRepository.GetByIdAsync(new PatientId(dto.Id));

    if (patient == null)
    {
        return null;
    }

    // Update all fields
    patient.ChangeFirstName(dto.Firstname);
    patient.ChangeLastName(dto.LastName);
    patient.ChangeFullName(dto.FullName);
    patient.ChangeGender(dto.Gender);
    patient.ChangeEmergencyContact(dto.EmergencyContact);

    // Use the List<string> directly for allergies
    patient.ChangeAllergies(dto.Allergies); // Ensure dto.Allergies is of type List<string>

    // Assign DateOfBirth directly
    patient.ChangeDateOfBirth(dto.DateOfBirth); // Assuming this is already of type DateTime?

    patient.ChangeMedicalRecordNumber(dto.MedicalRecordNumber);

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

            if (patient == null)
            {
                return null;
            }

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
