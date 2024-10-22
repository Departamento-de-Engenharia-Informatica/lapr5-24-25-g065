using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using System.Runtime.ConstrainedExecution;
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
            
            List<PatientDto> listDto = list.ConvertAll<PatientDto>(patient => 
                new PatientDto(patient.Id.AsGuid(),patient.Firstname, patient.LastName,patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber ));

            return listDto;
        }

        public async Task<PatientDto> AddAsync(CreatingPatientDto dto)
        {   
            var patient = new Patient(dto.Firstname, dto.LastName,dto.FullName, dto.Gender, dto.Allergies, dto.EmergencyContact, dto.DateOfBirth, dto.MedicalRecordNumber );

            if(patient.Allergies == null){
                await this._repo.AddAsync(patient);

                await this._unitOfWork.CommitAsync();

                return new PatientDto(patient.Id.AsGuid(),patient.Firstname, patient.LastName,patient.FullName, patient.Gender, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber );
            }else{
                await this._repo.AddAsync(patient);

                await this._unitOfWork.CommitAsync();

                return new PatientDto(patient.Id.AsGuid(),patient.Firstname, patient.LastName,patient.FullName, patient.Gender, patient.Allergies, patient.EmergencyContact, patient.DateOfBirth, patient.MedicalRecordNumber );
            }
        }

        /*public async Task<BackOfficeUserDto> UpdateAsync(BackOfficeUserDto dto)
        {
            await checkCategoryIdAsync(dto.CategoryId);
            var product = await this._repo.GetByIdAsync(new ProductId(dto.Id)); 

            if (product == null)
                return null;   

            // change all fields
            product.ChangeDescription(dto.Description);
            product.ChangeCategoryId(dto.CategoryId);
            
            await this._unitOfWork.CommitAsync();

            return new ProductDto(product.Id.AsGuid(),product.Description,product.CategoryId);
        }

        public async Task<ProductDto> DeleteAsync(ProductId id)
        {
            var product = await this._repo.GetByIdAsync(id); 

            if (product == null)
                return null;   

            this._repo.Remove(product);
            await this._unitOfWork.CommitAsync();

            return new BackOfficeUserDto(product.Id.AsGuid(),product.Description,product.CategoryId);
        }*/
    }
}