using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
namespace DDDSample1.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _repo;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<PatientDto> listDto = list.ConvertAll(patient => 
                new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber));

            return listDto;
        }

        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber);
        }

        public async Task<PatientDto> AddAsync(CreatePatientDTO dto)
        {   
            var patient = new Patient(dto.Firstname, dto.LastName, dto.FullName, dto.Gender, dto.Allergies, dto.EmergencyContact, dto.DateOfBirth, dto.MedicalRecordNumber);

            await this._repo.AddAsync(patient);
            await this._unitOfWork.CommitAsync();

            return new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber);
        }

        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            var patient = await this._repo.GetByIdAsync(new PatientId(dto.Id));

            if (patient == null)
                return null;   

            // Update all fields
            patient.ChangeFirstName(dto.Firstname);
            patient.ChangeLastName(dto.LastName);
            patient.ChangeFullName(dto.FullName);
            patient.ChangeGender(dto.Gender);
            patient.ChangeDateOfBirth(dto.DateOfBirth);
            patient.ChangeAllergies(dto.Allergies);
            patient.ChangeMedicalRecordNumber(dto.MedicalRecordNumber);

            await this._unitOfWork.CommitAsync();

            return new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber);
        }

        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
                return null;   

            this._repo.Remove(patient);
            await this._unitOfWork.CommitAsync();

            return new PatientDto(patient.Id.AsGuid(), patient.Firstname, patient.LastName, patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber);
        }
        
    }
    
}