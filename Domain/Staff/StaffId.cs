using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Staffs
{
    public class StaffId : EntityId
    {
        [JsonConstructor]
        public StaffId(Guid value) : base(value) { }
        public StaffId(String value) : base(value)
        {
        }

        override protected Object createFromString(String text) => new Guid(text);
        
        override public String AsString() => ((Guid)base.ObjValue).ToString();

        public Guid AsGuid() => (Guid)base.ObjValue;
    }
}
