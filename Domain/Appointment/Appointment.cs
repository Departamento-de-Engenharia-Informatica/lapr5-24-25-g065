using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;

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
        public List<StaffId> StaffIds { get; private set; } = new List<StaffId>();

        // Parameterless constructor for EF Core
        private Appointment() { }

        // Constructor with required fields
        public Appointment(string requestId, string roomId, DateTime date, string status, PatientId patientId, List<StaffId> staffIds)
        {
            Id = new AppointmentId(Guid.NewGuid());
            RequestId = requestId ?? throw new ArgumentNullException(nameof(requestId));
            RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
            Date = date;
            Status = status ?? throw new ArgumentNullException(nameof(status));
            PatientId = patientId ?? throw new ArgumentNullException(nameof(patientId));
            if (staffIds == null || staffIds.Count == 0) throw new ArgumentException("Appointment must include at least one staff member.");
            StaffIds = staffIds;
        }

        // Method to add staff to the appointment
        public void AddStaff(StaffId staffId)
        {
            if (staffId == null) throw new ArgumentNullException(nameof(staffId));
            if (!StaffIds.Contains(staffId))
            {
                StaffIds.Add(staffId);
            }
        }

        // Method to remove a staff member from the appointment
        public void RemoveStaff(StaffId staffId)
        {
            if (staffId == null) throw new ArgumentNullException(nameof(staffId));
            StaffIds.Remove(staffId);
        }

        // Update methods
        public void ChangeRequestId(string requestId) => RequestId = requestId ?? throw new ArgumentNullException(nameof(requestId));
        public void ChangeRoomId(string roomId) => RoomId = roomId ?? throw new ArgumentNullException(nameof(roomId));
        public void ChangeDate(DateTime date) => Date = date;
        public void ChangeStatus(string status) => Status = status ?? throw new ArgumentNullException(nameof(status));

        // Validation to ensure staff specialization matches appointment type
        public async Task<bool> ValidateStaffSpecializationsAsync(IStaffRepository staffRepository, List<string> requiredSpecializations)
        {
            foreach (var staffId in StaffIds)
            {
                var staff = await staffRepository.GetByIdAsync(staffId);
                if (staff == null) throw new ArgumentException($"Staff member with ID {staffId.AsGuid()} not found.");

                var staffSpecialization = staff.Specialization; // Get staff specialization
                if (!requiredSpecializations.Contains(staffSpecialization))
                {
                    return false; // Specialization does not match
                }
            }
            return true; // All staff specializations are valid
        }

        // Check room and staff availability (stub method, to be implemented)
        public bool CheckAvailability(DateTime appointmentDate, string roomId, List<StaffId> staffIds)
        {
            // Implement the check to verify room and staff availability for the given date and time.
            return true; // Placeholder, should return actual availability status
        }
    }
}
