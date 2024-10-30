using DDDSample1.Domain.Shared;
using System;

namespace DDDNetCore.Domain
{
    public class OperationRequestID : EntityId{
        public OperationRequestID(object value) : base(value){
        }

        public override string AsString(){
            Guid obj = (Guid)base.ObjValue;
            return obj.ToString();
        }

        protected override object createFromString(string text){
            return new Guid(text);
        }
        public Guid AsGuid()
        {
            return (Guid)base.ObjValue;
        }
    }
}
