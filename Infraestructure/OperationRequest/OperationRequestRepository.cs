using DDDNetCore.IRepos;
using DDDSample1.Infrastructure.Shared;
using DDDSample1.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain;
using DDDNetCore.Domain.OperationRequestDomain;
using Microsoft.EntityFrameworkCore;

public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestID>, IOperationRequestRepository
{

    private readonly DDDSample1DbContext context;

    public OperationRequestRepository(DDDSample1DbContext context) : base(context.OperationRequests)
    {
        this.context = context;
    }

    public new async Task<OperationRequest> GetByIdAsync(OperationRequestID id)
    {
        return await context.OperationRequests
        .FirstOrDefaultAsync(or => or.Id.Equals(id));

    }

    public new async Task<IEnumerable<OperationRequest>> GetAllAsync()
    {
        return await context.OperationRequests.ToListAsync();
    }


    public new async Task AddAsync(OperationRequest operationRequest)
    {
        await context.OperationRequests.AddAsync(operationRequest);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OperationRequest operationRequest)
    {
        context.OperationRequests.Update(operationRequest);
        await context.SaveChangesAsync();
    }

    public new async Task Remove(OperationRequest operationRequest)
    {
        context.OperationRequests.Remove(operationRequest);
        await context.SaveChangesAsync();
    }
}

