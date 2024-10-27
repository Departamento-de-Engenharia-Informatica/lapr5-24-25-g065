using System;
using DDDSample1.Domain.Passwords;

namespace DDDSample1.Domain.Users
{
    public class UserDto
    {
        public Guid Id { get; set; } // Set to allow binding
        public string UserName { get; set; } // Changed to set; to allow binding
        public string Email { get; set; } // Changed to set; to allow binding
        public Role Role { get; set; } // Changed to set; to allow binding
        public Password Password { get; set; } // Changed to set; to allow binding
        
        public UserDto(Guid id, string userName, string email, Role role, Password password)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Role = role;
            Password = password;
        }
    }
}
