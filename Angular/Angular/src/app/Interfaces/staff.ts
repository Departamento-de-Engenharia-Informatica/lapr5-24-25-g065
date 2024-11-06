import { UserId } from './userId';        // Adjust path as necessary
import { Appointment } from './appointment';  // Adjust path as necessary
import { StaffId } from './staffId';      // Adjust path as necessary

export interface Staff {
    Id: StaffId;
    Gender: string;
    Type: string;
    Specialization: string;
    FirstName: string;
    LastName: string;
    FullName: string;
    LicenseNumber: string;
    UserId: UserId;
    AvailabilitySlot: string;
    PhoneNumber: string;
    Email: string;
    Appointments: Appointment[];  // Array of Appointment objects
}
