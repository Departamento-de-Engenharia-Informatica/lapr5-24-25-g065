using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;
        public UserService(IUnitOfWork unitOfWork, IUserRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<UserDto> listDto = list.ConvertAll<UserDto>(user => 
                new UserDto(user.Id.AsGuid(),user.UserName,user.Email,user.Role));

            return listDto;
        }

        public async Task<UserDto> AddAsync(CreateUserDto dto)
        {
            var user = new User(dto.UserName,dto.Email,dto.Role);
            await this._repo.AddAsync(user);

            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(),user.UserName,user.Email,user.Role);
        }

        internal async Task<ActionResult<UserDto>> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);
            
            if(user == null)
                return null;

            return new UserDto(user.Id.AsGuid(),user.UserName,user.Email,user.Role);
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id)); 

            if (user == null)
                return null;   

            // change all fields
            user.ChangeUserName(user.UserName);
            user.ChangeEmail(user.Email);
            user.ChangeRole(user.Role);

            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(),user.UserName,user.Email,user.Role);
       }

        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id); 

            if (user == null)
                return null;   

            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(),user.UserName,user.Email,user.Role);
       }
    }
}