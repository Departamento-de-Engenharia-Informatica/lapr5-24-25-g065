using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
namespace DDDSample1.DTOs.Passwords
{
     public class CreatingPasswordDto
    {
    
        public string Pass { get; private set; }
        
        public CreatingPasswordDto(string pass){
            Pass= pass;
        }

    }
}