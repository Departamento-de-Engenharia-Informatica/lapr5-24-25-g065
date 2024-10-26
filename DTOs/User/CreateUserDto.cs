using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public class CreateUserDto
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }
        public PasswordId PasswordId { get; private set; }
        

        // Construtor para criação de um novo usuário
        public CreateUserDto(string userName, string email,Role r, PasswordId passwordId){
            UserName = userName;
            Email = email;
            Role = r;
            PasswordId = passwordId;
        }
    }
}