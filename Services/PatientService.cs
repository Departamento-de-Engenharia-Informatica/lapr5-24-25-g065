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
            return patients.Select(patient => MapToPatientDto(patient)).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            return patient == null ? null : MapToPatientDto(patient);
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
{
    if (dto == null) 
    {
        Console.WriteLine("Error: The provided patient DTO is null.");
        throw new ArgumentException("Invalid patient data", nameof(dto));
    }

    Console.WriteLine("Starting the patient addition process.");

    // Validate required fields
    if (string.IsNullOrEmpty(dto.Firstname) || string.IsNullOrEmpty(dto.LastName))
    {
        throw new ArgumentException("First name and last name are required.");
    }

    // Validate phone number and emergency contact
    if (string.IsNullOrEmpty(dto.PhoneNumber) || string.IsNullOrEmpty(dto.EmergencyContact))
    {
        throw new ArgumentException("The phone number and emergency contact should be provided.");
    }

    // Validate gender
    if (string.IsNullOrEmpty(dto.Gender))
    {
        throw new ArgumentException("The gender should be provided.");
    }


    // Create a new patient instance with the provided DTO
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
        dto.Email
    );

    try
    {
        // Log the created patient object to ensure it's constructed properly
        Console.WriteLine($"Patient object created: {patient.FullName}, {patient.Email}");

        // Add patient to the repository
        await patientRepository.AddAsync(patient);
        await unitOfWork.CommitAsync();

        // Log success
        Console.WriteLine($"Patient successfully added: {patient.FullName}");

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
            patient.Email
        );
    }
    catch (Exception ex)
    {
        // Log detailed exception information
        Console.WriteLine($"Error occurred while adding patient: {ex.Message}");
        Console.WriteLine(ex.StackTrace);

        // Re-throw the exception with more context
        throw new Exception("An error occurred while adding the patient. See inner exception for details.", ex);
    }
}

        public async Task<PatientDto> UpdateAsync(PatientId patientId, UpdatePatientDTO dto)
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
                dto.Email
            );

            await unitOfWork.CommitAsync();

            return MapToPatientDto(patient);
        }

        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            await patientRepository.DeleteAsync(patient);
            await unitOfWork.CommitAsync();

            return MapToPatientDto(patient);
        }

        public async Task<List<PatientDto>> SearchPatientsAsync(
            string name, 
            DateTime? dateOfBirth, 
            string medicalRecordNumber, 
            string phoneNumber, 
            string email, 
            int pageNumber, 
            int pageSize)
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

            if (!string.IsNullOrEmpty(email))
            {
                patients = patients.Where(p => p.Email == email).ToList();
            }

            var paginatedPatients = patients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(patient => MapToPatientDto(patient))
                .ToList();

            return paginatedPatients;
        }

        public async Task<PatientDto> GetPatientByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            var patients = await patientRepository.GetAllAsync();
            var patient = patients.FirstOrDefault(p => p.Email == email);

            return patient == null ? null : MapToPatientDto(patient);
        }

        private PatientDto MapToPatientDto(Patient patient)
        {
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
    }
}
