using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using DDDNetCore.IRepos;
using DDDNetCore.DTOs.Staff;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments;

namespace DDDSample1.Domain.Staffs
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        public async Task<List<StaffDto>> GetAllAsync()
        {
            var staffList = await _repo.GetAllAsync();
            return staffList.ConvertAll(CreateStaffDto);
        }

        public async Task<StaffDto> AddAsync(CreatingStaffDto dto)
        {
            ValidateCreatingStaffDto(dto);

            var staff = new Staff(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.Specialization, // Updated to use string specialization
                dto.Type,
                dto.LicenseNumber,
                new UserId(dto.UserId),
                dto.AvailabilitySlot,
                dto.PhoneNumber,
                dto.Email);

            await _repo.AddAsync(staff);
            await _unitOfWork.CommitAsync();

            return CreateStaffDto(staff);
        }

        public async Task<StaffDto> GetByIdAsync(StaffId id)
        {
            var staff = await _repo.GetByIdAsync(id);
            return staff != null ? CreateStaffDto(staff) : null;
        }

        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var staff = await _repo.GetByIdAsync(new StaffId(dto.Id));
            if (staff == null) return null;

            UpdateStaffFields(staff, dto);
            await _unitOfWork.CommitAsync();

            return CreateStaffDto(staff);
        }

        public async Task<StaffDto> DeleteAsync(StaffId id)
        {
            var staff = await _repo.GetByIdAsync(id);
            if (staff == null) return null;

            _repo.Remove(staff);
            await _unitOfWork.CommitAsync();

            return CreateStaffDto(staff);
        }

        // Private helper methods

        private void ValidateCreatingStaffDto(CreatingStaffDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Firstname) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.LicenseNumber))
            {
                throw new BusinessRuleValidationException("Firstname, LastName, Email, and LicenseNumber are required fields.");
            }
        }

        private StaffDto CreateStaffDto(Staff staff)
        {
            return new StaffDto(
                staff.Id.AsGuid(),
                staff.Firstname,
                staff.LastName,
                staff.FullName,
                staff.Gender,
                staff.Specialization, // Updated to use string specialization
                staff.Type,
                staff.LicenseNumber,
                staff.UserId.AsGuid(),
                staff.AvailabilitySlot,
                staff.PhoneNumber,
                staff.Email);
        }

        private void UpdateStaffFields(Staff staff, StaffDto dto)
        {
            staff.ChangeFirstName(dto.Firstname);
            staff.ChangeLastName(dto.LastName);
            staff.ChangeFullName(dto.FullName);
            staff.ChangeGender(dto.Gender);
            staff.ChangeType(dto.Type);
            staff.ChangeLicenseNumber(dto.LicenseNumber);
            staff.ChangeSpecialization(dto.Specialization); // Updated to use string specialization
            staff.ChangeAvailabilitySlot(dto.AvailabilitySlot);
            staff.ChangePhoneNumber(dto.PhoneNumber);
            staff.ChangeEmail(dto.Email);
        }

        public async Task<List<StaffDto>> SearchStaffsAsync(string name, string licenseNumber, string phoneNumber, string email, int pageNumber, int pageSize)
        {
            var staffList = await _repo.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                staffList = staffList.Where(s =>
                    s.Firstname.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(licenseNumber))
            {
                staffList = staffList.Where(s => s.LicenseNumber.Equals(licenseNumber, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                staffList = staffList.Where(s => s.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Implement pagination
            var paginatedStaffs = staffList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(staff => CreateStaffDto(staff))
                .ToList();

            return paginatedStaffs;
        }
    }
}
