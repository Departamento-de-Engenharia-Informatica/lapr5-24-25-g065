using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.BackOfficeUsers;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.BackOfficeUsers
{
    public class BackOfficeUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BackOfficeUserRepository _repo;

        public BackOfficeUserService(IUnitOfWork unitOfWork, BackOfficeUserRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<BackOfficeUserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<BackOfficeUserDto> listDto = list.ConvertAll<BackOfficeUserDto>(bouser => 
                new BackOfficeUserDto(bouser.Firstname,bouser.LastName,bouser.Gender,bouser.Specialization,bouser.Type,bouser.LicenseNumber));

            return listDto;
        }

        public async Task<BackOfficeUserDto> AddAsync(CreatingBackOfficeUserDto dto)
        {
            var backOfficeUser = new BackOfficeUser(dto.Firstname,dto.LastName,dto.Gender,dto.Specialization,dto.Type,dto.LicenseNumber);

            await this._repo.AddAsync(backOfficeUser);

            await this._unitOfWork.CommitAsync();

            return new BackOfficeUserDto(backOfficeUser.Firstname,backOfficeUser.LastName,backOfficeUser.Gender,backOfficeUser.Specialization,backOfficeUser.Type,backOfficeUser.LicenseNumber);
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