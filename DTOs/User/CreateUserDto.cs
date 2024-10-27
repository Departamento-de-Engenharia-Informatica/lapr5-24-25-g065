using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public class CreateUserDto
    {
        public string UserName { get; set; } // Changed to set; to allow binding
        public string Email { get; set; } // Changed to set; to allow binding
        public Role Role { get; set; } // Changed to set; to allow binding
        public string Password { get; set; } // Changed to string for easier handling

        // Parameterless constructor for model binding
        public CreateUserDto() { }

        // Constructor for creating a new user
        public CreateUserDto(string userName, string email, Role role, string password)
        {
            UserName = userName;
            Email = email;
            Role = role;
            Password = password;
        }
    }
}
