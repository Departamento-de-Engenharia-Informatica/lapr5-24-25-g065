using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.IRepos;
using DDDSample1.Domain.OperationType;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.OperationTypes
{
    public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeID>, IOperationTypeRepository
    {
        private readonly DDDSample1DbContext _context;

        public OperationTypeRepository(DDDSample1DbContext context) : base(context.OperationTypes)
        {
            _context = context;
        }

        public new async Task<OperationType> GetByIdAsync(OperationTypeID id)
        {
            return await _context.OperationTypes
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
        }

        public new async Task<List<OperationType>> GetAllAsync()
        {
            return await _context.OperationTypes
                .ToListAsync();
        }

        public new async Task AddAsync(OperationType operationType)
        {
            await _context.OperationTypes.AddAsync(operationType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OperationType operationType)
        {
            _context.OperationTypes.Update(operationType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OperationType operationType)
        {
            _context.OperationTypes.Remove(operationType);
            await _context.SaveChangesAsync();
        }
    }
}
