/*using DDDNetCore.Domain.OperationRequest;
using DDDNetCore.DTOs.OperationRequest;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDNetCore.Services
{
    public class OperationRequestService{

        // O atributo de procura de operationRequest neste momento é a priority. Se se tiver de mudar mais tarde, muda-se
        // Portanto, nao podem haver 2 operationRequest com a mesma prioridade

        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationRequestRepository operationRequestRepository;

        public async Task<List<OperationRequestDTO>> GetAllOperationRequest() {
            var operationRequests = await operationRequestRepository.GetAllOperationRequest();
            return operationRequests.Select(operationRequest => new OperationRequestDTO(
                operationRequest.patientID,
                operationRequest.doctorID,
                operationRequest.operationTypeID,
                operationRequest.operationDateTime.ToString(),
                operationRequest.deadline.ToString(),
                operationRequest.priority
            )).ToList();
        }

        public async Task<OperationRequestDTO> GetOperationRequestByPriority(int priority) {
            var operationRequest = await operationRequestRepository.GetOperationRequestByPriority(priority);
            return new OperationRequestDTO(operationRequest.patientID, operationRequest.doctorID, operationRequest.operationTypeID, operationRequest.operationDateTime.ToString(), operationRequest.deadline.ToString(), operationRequest.priority);
        }


        public async Task<OperationRequestDTO> AddOperationRequest(OperationRequestDTO operationRequestDTO) {
            var dateTimeAux = DateTime.Parse(operationRequestDTO.operationDateTime);
            var deadlineAux = DateTime.Parse(operationRequestDTO.deadline);
            var operationRequest = new OperationRequest(operationRequestDTO.patientID, operationRequestDTO.doctorID, operationRequestDTO.operationTypeID, dateTimeAux, deadlineAux, operationRequestDTO.priority);

            await operationRequestRepository.AddOperationRequest(operationRequest);
            await unitOfWork.CommitAsync();
            return operationRequestDTO;
            }

        public async Task<OperationRequestDTO> UpdateOperationRequest(OperationRequestDTO operationRequestDTO){
            var operationRequest = await operationRequestRepository.GetOperationRequestByPriority(operationRequestDTO.priority); // Fetch operationRequest from repository
            
            if (operationRequest == null) throw new BusinessRuleValidationException("Operation Request not found");

            operationRequest.Update(operationRequestDTO.patientID, operationRequestDTO.doctorID, operationRequestDTO.operationTypeID, operationRequestDTO.operationDateTime, operationRequestDTO.deadline, operationRequestDTO.priority);
            
            return operationRequestDTO;
        }

        public async Task<int> DeleteOperationRequestByID(int priority){
            await operationRequestRepository.DeleteOperationRequestByPriority(priority);
            return priority;
        }
        }
    }

*/