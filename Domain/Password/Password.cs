using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using System.Collections.Generic;
namespace DDDSample1.Domain.Passwords

{
    public class Password : ValueObject
    {
        public string Pass { get; private set; }
        
        public Password(string pass){
            Pass = pass;
        }
        public bool Verify(string password)
        {
            return Pass == password;
        }

         protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Pass;
        }
    }
}