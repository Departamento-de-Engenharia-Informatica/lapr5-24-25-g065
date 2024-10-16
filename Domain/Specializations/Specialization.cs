using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specializations
{
    public class Specialization : Entity<SpecializationId>, IAggregateRoot
    {
        public string Type { get;  private set; }
        public string Description{ get;  private set; }

        public Specialization(string type, string description){
            Type=type;
            Description=description;
        }
    }
}