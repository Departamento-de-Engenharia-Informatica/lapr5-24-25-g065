using DDDNetCore.Domain;
using DDDNetCore.Domain.OperationRequestDomain;
using DDDNetCore.DTOs.OperationRequest;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDNetCore.Services
{
    public class OperationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationRequestRepository operationRequestRepository;

        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepo)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepo; // Initialize the patient repository
        }

        public async Task<List<OperationRequestDTO>> GetAllAsync()
        {
            var operationRequests = await operationRequestRepository.GetAllAsync();
            return operationRequests.ConvertAll(CreateOperationRequestDto);
        }

        public async Task<OperationRequestDTO> GetByIdAsync(OperationRequestID id)
        {
            var operationRequest = await operationRequestRepository.GetByIdAsync(id);
            return new OperationRequestDTO(operationRequest.Id.AsGuid(), operationRequest.patientID, operationRequest.doctorID, operationRequest.operationTypeID, operationRequest.operationDateTime.ToString(), operationRequest.deadline.ToString(), operationRequest.priority);
        }

        public async Task<OperationRequestDTO> AddAsync(OperationRequestDTO operationRequestDTO)
        {
            var dateTimeAux = DateTime.Parse(operationRequestDTO.operationDateTime);
            var deadlineAux = DateTime.Parse(operationRequestDTO.deadline);
            var operationRequest = new OperationRequest(operationRequestDTO.patientID, operationRequestDTO.doctorID, operationRequestDTO.operationTypeID, dateTimeAux, deadlineAux, operationRequestDTO.priority);

            await operationRequestRepository.AddAsync(operationRequest);
            await unitOfWork.CommitAsync();
            return operationRequestDTO;
        }

        public async Task<OperationRequestDTO> UpdateAsync(OperationRequestDTO operationRequestDTO)
        {
            if (operationRequestDTO == null) throw new ArgumentException("Invalid Operation Request data");

            var operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(operationRequestDTO.ID));
            if (operationRequest == null) throw new BusinessRuleValidationException("Operation Request not found");

            operationRequest.Update(
                operationRequestDTO.patientID,
                operationRequestDTO.doctorID,
                operationRequestDTO.operationTypeID,
                operationRequestDTO.operationDateTime,
                operationRequestDTO.deadline,
                operationRequestDTO.priority
            );

            await unitOfWork.CommitAsync();

            return new OperationRequestDTO(
                operationRequest.Id.AsGuid(),
                operationRequest.patientID,
                operationRequest.doctorID,
                operationRequest.operationTypeID,
                operationRequest.operationDateTime.ToString(),
                operationRequest.deadline.ToString(),
                operationRequest.priority
            );
        }



        public async Task<OperationRequestDTO> RemoveAsync(OperationRequestID id)
        {
            var operationRequest = await operationRequestRepository.GetByIdAsync(id);
            if (operationRequest == null) throw new BusinessRuleValidationException("Operation Request not found");

            operationRequestRepository.Remove(operationRequest); // Use the repository to delete
            await unitOfWork.CommitAsync();

            return new OperationRequestDTO(
                operationRequest.Id.AsGuid(),
                operationRequest.patientID,
                operationRequest.doctorID,
                operationRequest.operationTypeID,
                operationRequest.operationDateTime.ToString(),
                operationRequest.deadline.ToString(),
                operationRequest.priority
            );
        }

        private OperationRequestDTO CreateOperationRequestDto(OperationRequest operationRequest)
        {
            return new OperationRequestDTO(
                operationRequest.Id.AsGuid(),
                operationRequest.patientID,
                operationRequest.doctorID,
                operationRequest.operationTypeID,
                operationRequest.operationDateTime.ToString(),
                operationRequest.deadline.ToString(),
                operationRequest.priority
                );
        }

    }
}