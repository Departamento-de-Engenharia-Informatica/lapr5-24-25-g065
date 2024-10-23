using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public class CreatingUserDto
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }

        // Construtor para criação de um novo usuário
        public CreatingUserDto(string userName, string email,Role r){
            UserName = userName;
            Email = email;
            Role = r;
        }
    }
}