using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }
        

        // Construtor para criação de um novo usuário
        public UserDto(Guid id,string userName, string email,Role r){
            Id = id;
            UserName = userName;
            Email = email;
            Role = r;
        }
    }
}