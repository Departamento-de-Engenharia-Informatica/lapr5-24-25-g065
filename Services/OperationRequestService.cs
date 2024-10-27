/*using DDDNetCore.Domain.OperationRequest;
using DDDNetCore.DTOs.OperationRequest;
using DDDNetCore.DTOs.Patient;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
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

        public async Task<OperationRequestDTO> GetByPriority(int priority) {
            var operationRequest = await operationRequestRepository.GetOperationRequestByPriority(priority);
            return new OperationRequestDTO(operationRequest.patientID, operationRequest.doctorID, operationRequest.operationTypeID, operationRequest.operationDateTime.ToString(), operationRequest.deadline.ToString(), operationRequest.priority);
        }


        public async Task<PatientDto> AddAsync(CreatePatientDTO dto){
            

            // Convert IReadOnlyList<string> to List<string>
            var allergies = dto.Allergies?.ToList(); // Assuming dto.Allergies is IReadOnlyList<string>

            // Create the patient with the UserId and PhoneNumber
            var patient = new Patient(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                allergies, // Pass the converted allergies
                dto.EmergencyContact,
                dto.DateOfBirth,
                dto.MedicalRecordNumber,
                dto.UserId, // Include UserId
                dto.PhoneNumber // Include PhoneNumber
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
                patient.UserId, // Include UserId
                patient.PhoneNumber // Include PhoneNumber
            );
        }

        public async Task<OperationRequestDTO> AddOperationRequest(OperationRequestDTO operationRequestDTO) {
            var operationRequest = new OperationRequest(operationRequestDTO.patientID,operationRequestDTO.doctorID,operationRequestDTO.operationTypeID,new DateTime.Parse(operationRequestDTO.operationDateTime), new DateTime.Parse())
            await operationRequestRepository.AddOperationRequest();
            }
        }
    }

*/