using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Specializations
{
    public class SpecializationId : EntityId
    {
        // Parameterless constructor for EF
        public SpecializationId() : base(Guid.NewGuid()) // You can choose to generate a new Guid or leave it to be set later
        {
        }

        [JsonConstructor]
        public SpecializationId(Guid value) : base(value)
        {
        }

        public SpecializationId(string value) : base(value)
        {
        }

        override protected Object createFromString(String text)
        {
            return new Guid(text);
        }

        override public String AsString()
        {
            Guid obj = (Guid)base.ObjValue;
            return obj.ToString();
        }

        public Guid AsGuid()
        {
            return (Guid)base.ObjValue;
        }
    }
}
