using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.Staffs
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StaffRepository _repo;

        public StaffService(IUnitOfWork unitOfWork, StaffRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<StaffDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<StaffDto> listDto = list.ConvertAll<StaffDto>(bouser => 
                new StaffDto(bouser.Firstname,bouser.FullName,bouser.LastName,bouser.Gender,bouser.Specialization,bouser.Type,bouser.LicenseNumber));

            return listDto;
        }

        public async Task<StaffDto> AddAsync(StaffDto dto)
        {
            var staff = new Staff(dto.Firstname,dto.FullName,dto.LastName,dto.Gender,dto.Specialization,dto.Type,dto.LicenseNumber);

            await this._repo.AddAsync(staff);

            await this._unitOfWork.CommitAsync();

            return new Staff(staff.Firstname,staff.FullName, staff.LastName,staff.Gender,staff.Specialization,staff.Type,staff.LicenseNumber);
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