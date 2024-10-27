using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Appointments
{
    public class AppointmentId : EntityId
    {
        // Constructor accepting a Guid value
        [JsonConstructor]
        public AppointmentId(Guid value) : base(value)
        {
        }

        // Constructor accepting a string value
        public AppointmentId(string value) : base(value)
        {
        }

        // Create a Guid from a string representation
        override
        protected object createFromString(string text)
        {
            return new Guid(text);
        }

        // Get string representation of the Guid
        override
        public string AsString()
        {
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }

        // Get the Guid value
        public Guid AsGuid()
        {
            return (Guid) base.ObjValue;
        }
    }
}
