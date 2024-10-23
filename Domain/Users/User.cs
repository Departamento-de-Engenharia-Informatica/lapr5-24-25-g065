using System;
using DDDSample1.Domain.Shared;
namespace DDDSample1.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }

        protected User() { }

        // Construtor para criação de um novo usuário
        public User(string userName, string email,Role r){
            this.Id = new UserId(Guid.NewGuid());
            UserName = userName;
            Email = email;
            Role = r;
        }

        public void ChangeUserName(string username){
            this.UserName = username;
        }
        public void ChangeEmail(string email){
            this.Email = email;
        }
        public void ChangeRole(Role role){
            this.Role = role;
        }

    }
}