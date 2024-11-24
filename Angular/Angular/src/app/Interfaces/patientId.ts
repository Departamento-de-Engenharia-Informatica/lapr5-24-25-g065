export interface CreatePatientDTO {
  firstname: string;
  lastname: string;
  fullName: string;  // Updated to include fullName
  gender: string;
  allergies?: string[];  // Nullable list, to match the C# List<string>?
  emergencyContact: string;
  dateOfBirth?: string;  // DateTime? in C# will map to string or Date in TypeScript
  medicalRecordNumber: string;
  phoneNumber: string;
  email: string;
}

export interface PatientDTO {
  id: string;
  firstname: string;
  lastname: string;
  fullName: string;
  gender: string;
  allergies?: string[];  // Nullable list, same as CreatePatientDTO
  emergencyContact: string;
  dateOfBirth?: string;  // Nullable, corresponds to DateTime? in C#
  medicalRecordNumber: string;
  phoneNumber: string;
  email: string;
  userName: string;  // Corresponding field from the User class in C#
}
