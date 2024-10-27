using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.DTOs.Patient;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients;
using DDDNetCore.IRepos;
using Microsoft.IdentityModel.Tokens;

namespace DDDSample1.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository; // User repository for user management
        private readonly IPatientRepository patientRepository; // Patient repository for patient management

        public PatientService(IUnitOfWork unitOfWork, IUserRepository userRepo, IPatientRepository patientRepo)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepo;
            this.patientRepository = patientRepo; // Initialize the patient repository
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var patients = await patientRepository.GetAllAsync(); // Fetch patients from repository
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
                patient.UserId // Include UserId
            )).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id); // Fetch from repository
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
                patient.UserId // Include UserId
            );
        }

       public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
{
    if (dto == null) throw new ArgumentException("Invalid patient data");

    // Convert IReadOnlyList<string> to List<string>
    var allergies = dto.Allergies?.ToList(); // Assuming dto.Allergies is IReadOnlyList<string>

    // Create the patient with the UserId
    var patient = new Patient(
        dto.Firstname,
        dto.LastName,
        dto.FullName,
        dto.Gender,
        allergies, // Pass the converted allergies
        dto.EmergencyContact,
        dto.DateOfBirth,
        dto.MedicalRecordNumber,
        dto.UserId // Include UserId
    );

    await patientRepository.AddAsync(patient); // Use the repository to add
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
        patient.UserId // Include UserId
    );
}


       public async Task<PatientDto> UpdateAsync(UpdatePatientDTO dto)
{
    if (dto == null) throw new ArgumentException("Invalid patient data");

    var patient = await patientRepository.GetByIdAsync(new PatientId(dto.Id)); // Fetch patient from repository
    if (patient == null) throw new BusinessRuleValidationException("Patient not found");

    // Update patient details
    patient.Update(
        dto.Firstname,
        dto.LastName,
        dto.FullName,
        dto.Gender,
        dto.Allergies,
        dto.EmergencyContact,
        dto.DateOfBirth,
        dto.MedicalRecordNumber,
        dto.UserId // Pass UserId from the DTO
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
        patient.UserId // Include UserId
    );
}


        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id); // Fetch patient from repository
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            await patientRepository.DeleteAsync(patient); // Use the repository to delete
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
                patient.UserId // Include UserId
            );
        }
    
    
    public async Task<List<PatientDto>> SearchPatientsAsync(string name, DateTime? dateOfBirth, string medicalRecordNumber, int pageNumber, int pageSize)
{
    var patients = await patientRepository.GetAllAsync();

    // Apply filters
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

    // Implement pagination
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
            patient.UserId
        ))
        .ToList();

    return paginatedPatients;
}

}

}