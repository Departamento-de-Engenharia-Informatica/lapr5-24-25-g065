using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class OperationTypeDto
    {
        public Guid Id { get; set; }
       public string Name { get;  private set; }
        

        public OperationTypeDto(Guid id,string Name, string lastName,string fullName, string gender, List<string> allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            this.Id=id;
            this.Name = Name;
        }

    }
}