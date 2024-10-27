using System;
using System.Linq;
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
            if (!IsValidPassword(password)){
                throw new ArgumentException("Password does not meet the required strength criteria.");
            };

            return Pass == password;
        }

         protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Pass;
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            { 
                return false;
            };
            if (password.Length < 8){ 
                return false;
            };

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }
}