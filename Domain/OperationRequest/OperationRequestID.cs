using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationRequest
{
    public class OperationRequestID : EntityId
    {
        public OperationRequestID(object value) : base(value)
        {
        }

        public override string AsString()
        {
            throw new System.NotImplementedException();
        }

        protected override object createFromString(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
