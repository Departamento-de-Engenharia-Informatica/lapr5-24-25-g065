using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            var userDtos = users.ConvertAll(user => 
                new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password));

            return userDtos;
        }

       public async Task<UserDto> AddAsync(CreateUserDto dto)
{
    // Add email uniqueness check here
    var existingUser = await _repo.GetByEmailAsync(dto.Email);
    if (existingUser != null)
    {
        throw new BusinessRuleValidationException("A user with this email already exists.");
    }

    var password = new Password(dto.Password); // Assuming Password has a constructor
    var user = new User(dto.UserName, dto.Email, dto.Role, password);

    await _repo.AddAsync(user);
    await _unitOfWork.CommitAsync();

    return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
}


        internal async Task<ActionResult<UserDto>> GetByIdAsync(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null)
                return null;

            return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var user = await _repo.GetByIdAsync(new UserId(dto.Id)); 
            if (user == null)
                return null;   

            // Update fields - use incoming DTO properties
            user.ChangeUserName(dto.UserName);
            user.ChangeEmail(dto.Email);
            user.ChangeRole(dto.Role);
            
            // If the password needs to be changed, handle it here
            // For instance, if your DTO has a new password, you would verify and change it
            // if (!string.IsNullOrEmpty(dto.Password)) {
            //     user.ChangePassword(new Password(dto.Password));
            // }

            await _unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
        }

        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await _repo.GetByIdAsync(id); 
            if (user == null)
                return null;   

            _repo.Remove(user);
            await _unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
        }

        // Method for authenticating a user
        public async Task<UserDto> AuthenticateAsync(string email, string password)
        {
            // Find user by email
            var user = await _repo.GetByEmailAsync(email);
            if (user == null)
                return null;

            // Check passwords
            if (user.Password.Verify(password))
            {
                // Map User to UserDto before returning
                return new UserDto(user.Id.AsGuid(), user.UserName, user.Email, user.Role, user.Password);
            }

            return null;
        }
    }
}
