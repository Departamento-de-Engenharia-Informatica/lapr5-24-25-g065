using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Passwords;

namespace DDDSample1.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }
        public Password Password { get; private set; }

        protected User() { }

        public User(string userName, string email, Role role, Password password)
        {
            this.Id = new UserId(Guid.NewGuid());
            UserName = userName;
            Email = email;
            Role = role;
            Password = password;
        }

        public void ChangeUserName(string username) => UserName = username;

        public void ChangeEmail(string email) => Email = email;

        public void ChangeRole(Role role) => Role = role;

        public void ChangePassword(Password password) => Password = password;
    }
}
