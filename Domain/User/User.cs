using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Passwords;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }
        public Password Password { get; private set; }

        protected User() { }

        public User(string userName, string email, Role role, Password password)
        {
            ValidateUserName(userName);
            ValidateEmail(email);
            this.Id = new UserId(Guid.NewGuid());
            UserName = userName;
            Email = email;
            Role = role;
            Password = password;
        }
        private void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Contains(" "))
            {
                throw new ArgumentException("Username cannot be empty or contain spaces.");
            }
        }

        private void ValidateEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
        }
        public void ChangeUserName(string username) => UserName = username;

        public void ChangeEmail(string email) => Email = email;

        public void ChangeRole(Role role) => Role = role;

        public void ChangePassword(Password password) => Password = password;
    }
}
