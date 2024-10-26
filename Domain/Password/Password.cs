using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
namespace DDDSample1.Domain.Passwords
{
    public class Password : Entity<PasswordId>, IAggregateRoot
    {
        public string Pass { get; private set; }

        protected Password() { }
        
        public Password(string pass){
            this.Id = new PasswordId(Guid.NewGuid());
            Pass = pass;
        }

        public void ChangePassword(string pass){
            this.Pass = pass;
        }
        public bool Verify(string password)
        {
            return Pass == password;
        }
    }
}