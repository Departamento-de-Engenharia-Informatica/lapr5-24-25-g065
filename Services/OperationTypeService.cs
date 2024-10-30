using DDDNetCore.DTOs.OperationType;
using DDDNetCore.DTOs.Patient;
using DDDNetCore.IRepos;
using DDDSample1.Domain.OperationType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddsample1.domain
{
    public class OperationTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationTypeRepository operationTypeRepository;

        public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.operationTypeRepository = operationTypeRepository;
        }

        public async Task<List<OperationTypeDTO>> GetAllAsync()
        {
            var operationTypes = await operationTypeRepository.GetAllAsync();

            return operationTypes.Select(operationType => new OperationTypeDTO(
                operationType.Id.AsGuid(),
                operationType.Name,
            operationType.RequiredStaffBySpecialization,
                operationType.EstimatedDuration.ToString(),
                operationType.IsActive
            )).ToList();
        }

        public async Task<OperationTypeDTO> GetByIdAsync(OperationTypeId id)
        {
            var operationType = await operationTypeRepository.GetByIdAsync(id);
            return operationType == null ? null : new OperationTypeDTO(
                operationType.Id.AsGuid(),
                operationType.Name,
                operationType.RequiredStaffBySpecialization,
                operationType.EstimatedDuration.ToString(),
                operationType.IsActive);
        }

        public async Task<OperationTypeDTO> AddAsync(OperationTypeDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid operation type data");

            if (!TimeSpan.TryParse(dto.EstimatedDuration, out TimeSpan t1))
            {
                throw new ArgumentException("Invalid format for EstimatedDuration");
            }

            var operationType = new OperationType(
                dto.Name,
                dto.RequiredStaffBySpecialization,
                t1,
                dto.IsActive
            );

            await operationTypeRepository.AddAsync(operationType);
            await unitOfWork.CommitAsync();

            return new OperationTypeDTO(
                operationType.Id.AsGuid(),
                operationType.Name,
                operationType.RequiredStaffBySpecialization,
                operationType.EstimatedDuration.ToString(),
                operationType.IsActive
                );
            return dto; 
        }
        }

        public async Task<PatientDTO> UpdateAsync(UpdatePatientDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid patient data");

            var patient = await patientRepository.GetByIdAsync(new PatientId(dto.Id));
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            patient.Update(
                dto.FirstName,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.Allergies,
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber,
                dto.UserId,
                dto.PhoneNumber
            );

            await unitOfWork.CommitAsync();

            return new PatientDTO(
                patient.Id.AsGuid(),
                patient.FirstName,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
            patient.DateOfBirth,
            patient.MedicalRecordNumber,
            patient.UserId,
                patient.PhoneNumber
            );
        }

        public async Task<PatientDTO> DeleteAsync(PatientId id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient == null) throw new BusinessRuleValidationException("Patient not found");

            await patientRepository.DeleteAsync(patient);
            await unitOfWork.CommitAsync();

            return new PatientDTO(
                patient.Id.AsGuid(),
                patient.FirstName,
                patient.LastName,
                patient.FullName,
                patient.Gender,
                patient.Allergies,
                patient.EmergencyContact,
                patient.DateOfBirth,
                patient.MedicalRecordNumber,
                patient.UserId,
                patient.PhoneNumber
            );
        }
    }
}
