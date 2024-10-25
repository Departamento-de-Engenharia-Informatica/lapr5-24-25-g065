using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Specializations
{
    public class SpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _repo;

        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<SpecializationDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<SpecializationDto> listDto = list.ConvertAll<SpecializationDto>(s => 
                new SpecializationDto(s.Id.AsGuid(),s.Type,s.Description));

            return listDto;
        }

        public async Task<SpecializationDto> AddAsync(CreatingSpecializationDto dto)
        {
            var spec = new Specialization(dto.Type, dto.Description);

            await this._repo.AddAsync(spec);

            await this._unitOfWork.CommitAsync();

            return new SpecializationDto(spec.Id.AsGuid(),spec.Type,spec.Description);
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