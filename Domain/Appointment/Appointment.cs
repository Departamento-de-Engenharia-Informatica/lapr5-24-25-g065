using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Appointments
{
    public class Appointment : Entity<AppointmentId>, IAggregateRoot
    {
        public AppointmentId Id { get; private set; }
        public string RequestId { get; private set; }
        public string RoomId { get; private set; }
        public DateTime Date { get; private set; }
        public string Status { get; private set; }

        public Appointment(string requestId, string roomId, DateTime date, string status)
        {
            Id = new AppointmentId(Guid.NewGuid());
            RequestId = requestId;
            RoomId = roomId;
            Date = date;
            Status = status;
        }

        public void ChangeRequestId(string requestId)
        {
            this.RequestId = requestId;
        }

        public void ChangeRoomId(string roomId)
        {
            this.RoomId = roomId;
        }

        public void ChangeDate(DateTime date)
        {
            this.Date = date;
        }

        public void ChangeStatus(string status)
        {
            this.Status = status;
        }
    }
}
