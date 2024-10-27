using System.Collections.Generic;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.Specializations
{
    public class Specialization : Entity<SpecializationId>, IAggregateRoot
    {
        public string Type { get; private set; }
        public string Description { get; private set; }

        // Navigation property for the related Staff entities
        public virtual ICollection<Staff> StaffMembers { get; private set; }

        public Specialization(string type, string description)
        {
            this.Id = new SpecializationId();
            Type = type;
            Description = description;
            StaffMembers = new List<Staff>(); // Initialize the collection
        }
    }
}
