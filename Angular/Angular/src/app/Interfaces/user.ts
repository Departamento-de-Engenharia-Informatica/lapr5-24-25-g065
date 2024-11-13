export interface UserId {
    value: string; // The GUID as a string
  }
  
export interface User {
    Id: UserId;
    userName: string;
    email: string;
    role: string;
    password?: string;
}
  