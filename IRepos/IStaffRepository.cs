using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.IRepos
{
    public interface IStaffRepository:  IRepository<Staff, StaffId>
    {
      
    }
}
