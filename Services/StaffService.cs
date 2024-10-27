using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;
using DDDNetCore.DTOs.Staff;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.Staffs
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly ISpecializationRepository _specRepo;

        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo, ISpecializationRepository specRepo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._specRepo = specRepo; 
        }

        public async Task<List<StaffDto>> GetAllAsync()
        {
            var staffList = await this._repo.GetAllAsync();

            var staffDtoList = staffList.ConvertAll(staff => 
                new StaffDto(
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
                    staff.Email));

            return staffDtoList;
        }

        public async Task<StaffDto> AddAsync(CreatingStaffDto dto)
        {
            var staff = new Staff(
                dto.Firstname,
                dto.LastName,
                dto.FullName,
                dto.Gender,
                dto.SpecializationId,
                dto.Type,
                dto.LicenseNumber,
                dto.UserId,
                dto.AvailabilitySlot,
                dto.PhoneNumber,
                dto.Email);

            await this._repo.AddAsync(staff);
            await this._unitOfWork.CommitAsync();

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

        public async Task<StaffDto> GetByIdAsync(StaffId id) // Made this method public instead of internal
        {
            var staff = await this._repo.GetByIdAsync(id);
            
            if (staff == null)
                return null;

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

        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            var staff = await this._repo.GetByIdAsync(new StaffId(dto.Id)); 

            if (staff == null)
                return null;   

            // Update fields
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

            await this._unitOfWork.CommitAsync();

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

        public async Task<StaffDto> DeleteAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id); 

            if (staff == null)
                return null;   

            this._repo.Remove(staff);
            await this._unitOfWork.CommitAsync();

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
    }
}
