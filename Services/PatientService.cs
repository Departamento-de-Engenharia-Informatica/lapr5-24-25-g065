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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> GetAllAsync()
{
    var patients = await _patientRepository.GetAllAsync();

    // Debug log: Ensure you're getting the correct patient data
    foreach (var patient in patients)
    {
        Console.WriteLine($"Patient: {patient.Id} - {patient.Firstname} {patient.LastName}");
    }

    return patients.Select(MapToPatientDto).ToList();
}


        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return patient == null ? null : MapToPatientDto(patient);
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data", nameof(dto));

            var patient = new Patient(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.Allergies?.ToList(),
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber,
                dto.PhoneNumber,
                dto.Email
            );

            await _patientRepository.AddAsync(patient);
            await _unitOfWork.CommitAsync();

            return MapToPatientDto(patient);
        }

        public async Task<PatientDto> UpdateAsync(PatientId id, UpdatePatientDTO dto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) throw new ArgumentException("Patient not found");

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

            await _unitOfWork.CommitAsync();

            return MapToPatientDto(patient);
        }

        public async Task DeleteAsync(PatientId id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) throw new ArgumentException("Patient not found");

            await _patientRepository.DeleteAsync(patient);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PatientDto> GetPatientByEmailAsync(string email)
        {
            var patients = await _patientRepository.GetAllAsync();
            var patient = patients.FirstOrDefault(p => p.Email == email);
            return patient == null ? null : MapToPatientDto(patient);
        }

        private PatientDto MapToPatientDto(Patient patient)
{
    if (patient == null) throw new ArgumentNullException(nameof(patient));

    return new PatientDto(
        patient.Id.AsGuid(),
        patient.Firstname,
        patient.LastName,
        patient.FullName,
        patient.Gender,
        patient.Allergies ?? new List<string>(),
        patient.EmergencyContact,
        patient.DateOfBirth,
        patient.MedicalRecordNumber,
        patient.PhoneNumber,
        patient.Email
    );
}

    }
}
