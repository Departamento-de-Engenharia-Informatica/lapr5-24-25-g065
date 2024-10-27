using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;
using DDDNetCore.DTOs.Staff;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staffs
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly ISpecializationRepository _specRepo;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo, ISpecializationRepository specRepo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _specRepo = specRepo; 
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
        new SpecializationId(dto.SpecializationId),
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
                string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new BusinessRuleValidationException("Firstname, LastName, and Email are required fields.");
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
                staff.SpecializationId,
                staff.Type,
                staff.LicenseNumber,
                staff.UserId,
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
            staff.ChangeSpecialization(dto.SpecializationId);
            staff.ChangeAvailabilitySlot(dto.AvailabilitySlot);
            staff.ChangePhoneNumber(dto.PhoneNumber);
            staff.ChangeEmail(dto.Email);
        }
    }
}
