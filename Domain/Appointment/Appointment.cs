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
        public StaffId StaffId { get; private set; } // Added

        public Appointment(string requestId, string roomId, DateTime date, string status, PatientId patientId, StaffId staffId)
        {
            Id = new AppointmentId(Guid.NewGuid());
            RequestId = requestId;
            RoomId = roomId;
            Date = date;
            Status = status;
            PatientId = patientId;
            StaffId = staffId; // Assign the associated StaffId
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

        public void ChangePatientId(PatientId patientId)
        {
            this.PatientId = patientId;
        }

        public void ChangeStaffId(StaffId staffId)
        {
            this.StaffId = staffId; // Method to change associated staff
        }
    }
}
