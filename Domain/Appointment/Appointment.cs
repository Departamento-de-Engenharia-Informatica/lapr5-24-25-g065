using System;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients; // Import Patient domain
using DDDSample1.Domain.Staffs; // Import Staff domain

namespace DDDSample1.Domain.Appointments
{
    public class Appointment : Entity<AppointmentId>, IAggregateRoot
    {
        public AppointmentId Id { get; private set; }
        public string RequestId { get; private set; }
        public string RoomId { get; private set; }
        public DateTime Date { get; private set; }
        public string Status { get; private set; }
        public PatientId PatientId { get; private set; }
        public StaffId StaffId { get; private set; }

        // Constructor
        public Appointment(string requestId, string roomId, DateTime date, string status, PatientId patientId, StaffId staffId)
        {
            Id = new AppointmentId(Guid.NewGuid());
            RequestId = requestId ?? throw new ArgumentNullException(nameof(requestId));
            RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
            Date = date;
            Status = status ?? throw new ArgumentNullException(nameof(status));
            PatientId = patientId;
            StaffId = staffId; // Assign the associated StaffId
        }

        // Change methods
        public void ChangeRequestId(string requestId)
        {
            RequestId = requestId ?? throw new ArgumentNullException(nameof(requestId));
        }

        public void ChangeRoomId(string roomId)
        {
            RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
        }

        public void ChangeDate(DateTime date)
        {
            Date = date;
        }

        public void ChangeStatus(string status)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }

        public void ChangePatientId(PatientId patientId)
        {
            PatientId = patientId; 
        }

        public void ChangeStaffId(StaffId staffId)
        {
            StaffId = staffId; // Method to change associated staff
        }
    }
}
