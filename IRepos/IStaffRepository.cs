using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.IRepos
{
    public interface IStaffRepository:  IRepository<Staff, StaffId>
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff> GetByIdAsync(StaffId id);
        Task AddAsync(Staff staff);
        void Remove(Staff staff);
        // Add other relevant method signatures
    }
}
