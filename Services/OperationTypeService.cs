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

        public async Task<OperationTypeDTO> GetByIdAsync(OperationTypeID id)
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
        }


        public async Task<OperationTypeDTO> UpdateAsync(OperationTypeDTO dto)
        {
            if (dto == null) throw new ArgumentException("Invalid Operation Type data");

            var operationType = await operationTypeRepository.GetByIdAsync(new OperationTypeID(dto.ID));
            if (operationType == null) throw new BusinessRuleValidationException("Operation Type not found");

            if (!TimeSpan.TryParse(dto.EstimatedDuration, out TimeSpan t1))
            {
                throw new ArgumentException("Invalid format for EstimatedDuration");
            }

            operationType.Update(
                dto.Name,
                dto.RequiredStaffBySpecialization,
                t1
            );

            await unitOfWork.CommitAsync();

            return new OperationTypeDTO(
                operationType.Id.AsGuid(),
                operationType.Name,
                operationType.RequiredStaffBySpecialization,
                operationType.EstimatedDuration.ToString(),
                operationType.IsActive
            );
        }

        public async Task<OperationTypeDTO> DeleteAsync(OperationTypeID id)
        {
            var operationType = await operationTypeRepository.GetByIdAsync(id);
            if (operationType == null) throw new BusinessRuleValidationException("Operation Type not found");

            operationTypeRepository.Remove(operationType);
            await unitOfWork.CommitAsync();

            return new OperationTypeDTO(
                operationType.Id.AsGuid(),
                operationType.Name,
                operationType.RequiredStaffBySpecialization,
                operationType.EstimatedDuration.ToString(),
                operationType.IsActive
            );
        }
    }
}
