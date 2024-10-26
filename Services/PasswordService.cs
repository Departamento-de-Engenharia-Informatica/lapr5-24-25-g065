using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;
using DDDSample1.DTOs.Passwords;

namespace DDDSample1.Domain.Passwords
{
    public class PasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordRepository _repo;
    
        public PasswordService(IUnitOfWork unitOfWork, IPasswordRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo; 
        }
        internal async Task<ActionResult<PasswordDto>> GetByIdAsync(PasswordId id)
        {
            var password = await this._repo.GetByIdAsync(id);
            
            if(password == null)
                return null;

            return new PasswordDto(password.Id.AsGuid(),password.Pass);
        }

        public async Task<PasswordDto> AddAsync(CreatingPasswordDto dto)
        {
            var password = new Password(dto.Pass);
            
            await this._repo.AddAsync(password);

            await this._unitOfWork.CommitAsync();

            return new PasswordDto(password.Id.AsGuid(),password.Pass);
        }

        

        public async Task<PasswordDto> UpdateAsync(PasswordDto dto)
        {
            var password = await this._repo.GetByIdAsync(new PasswordId(dto.Id)); 

            if (password == null)
                return null;   

            // change all fields
            password.ChangePassword(password.Pass);
            

            await this._unitOfWork.CommitAsync();

            return new PasswordDto(password.Id.AsGuid(),password.Pass);
        }

    }
}