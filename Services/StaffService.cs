using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using DDDNetCore.IRepos;
using DDDNetCore.DTOs.Staff;

namespace DDDSample1.Domain.Staffs
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly ISpecializationRepository _specRepo;
        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo,ISpecializationRepository specRepo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._specRepo = specRepo; 
        }

        public async Task<List<StaffDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<StaffDto> listDto = list.ConvertAll<StaffDto>(staff => 
                new StaffDto(staff.Id.AsGuid(),staff.Firstname,staff.LastName,staff.FullName,staff.Gender,staff.SpecializationId,staff.Type,staff.LicenseNumber, staff.UserId, staff.AvailabilitySlot, staff.PhoneNumber,staff.Email));

            return listDto;
        }

        public async Task<StaffDto> AddAsync(CreatingStaffDto dto)
        {
            var staff = new Staff(dto.Firstname,dto.LastName,dto.FullName,dto.Gender,dto.SpecializationId,dto.Type,dto.LicenseNumber,dto.UserId, dto.AvailabilitySlot, dto.PhoneNumber,dto.Email);

            await this._repo.AddAsync(staff);

            await this._unitOfWork.CommitAsync();

            return new StaffDto(staff.Id.AsGuid(),staff.Firstname,staff.LastName,staff.FullName,staff.Gender,staff.SpecializationId,staff.Type,staff.LicenseNumber,dto.UserId, staff.AvailabilitySlot, staff.PhoneNumber,staff.Email);
        }

        internal async Task<ActionResult<StaffDto>> GetByIdAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id);
            
            if(staff == null)
                return null;

            return new StaffDto(staff.Id.AsGuid(),staff.Firstname,staff.LastName,staff.FullName,staff.Gender,staff.SpecializationId,staff.Type,staff.LicenseNumber,staff.UserId, staff.AvailabilitySlot, staff.PhoneNumber,staff.Email);
        }

        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            var staff = await this._repo.GetByIdAsync(new StaffId(dto.Id)); 

            if (staff == null)
                return null;   

            // change all fields
            staff.ChangeFirstName(staff.Firstname);
            staff.ChangeLastName(staff.LastName);
            staff.ChangeFullName(staff.FullName);
            staff.ChangeGender(staff.Gender);
            staff.ChangeType(staff.Type);
            staff.ChangeLicenseNumber(staff.LicenseNumber);
            staff.ChangeSpecialization(staff.SpecializationId);
            staff.ChangeAvailabilitySlot(staff.AvailabilitySlot);
            staff.ChangePhoneNumber(staff.PhoneNumber);
            staff.ChangeEmail(staff.Email);

            await this._unitOfWork.CommitAsync();

            return new StaffDto(staff.Id.AsGuid(),staff.Firstname,staff.LastName,staff.FullName,staff.Gender,staff.SpecializationId,staff.Type,staff.LicenseNumber,dto.UserId, dto.AvailabilitySlot, dto.PhoneNumber,dto.Email);
        }

        public async Task<StaffDto> DeleteAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id); 

            if (staff == null)
                return null;   

            this._repo.Remove(staff);
            await this._unitOfWork.CommitAsync();

            return new StaffDto(staff.Id.AsGuid(),staff.Firstname,staff.LastName,staff.FullName,staff.Gender,staff.SpecializationId,staff.Type,staff.LicenseNumber,staff.UserId, staff.AvailabilitySlot, staff.PhoneNumber,staff.Email);
        }
    }
}