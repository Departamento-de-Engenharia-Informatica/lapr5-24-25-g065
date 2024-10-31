using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Users
{
    public class UserId : EntityId
    {
        [JsonConstructor]
        public UserId(Guid value) : base(value) { }
        public UserId() : base(Guid.NewGuid()) { }

        protected override object createFromString(string text)
        {
            return new Guid(text);
        }

        public override string AsString()
        {
            return ((Guid)base.ObjValue).ToString();
        }

        public Guid AsGuid()
        {
            return (Guid)base.ObjValue;
        }
    }
}



