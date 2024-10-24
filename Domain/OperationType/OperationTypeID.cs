using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.OperationType{
public class  OperationTypeID : EntityId{
    [JsonConstructor]
        public OperationTypeID(Guid value) : base(value)
        {
        }

        public OperationTypeID(object value) : base(value)
        {
        }

        override
        public String AsString(){
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }

        protected override object createFromString(string text)
        {
            throw new NotImplementedException();
        }
    }
}