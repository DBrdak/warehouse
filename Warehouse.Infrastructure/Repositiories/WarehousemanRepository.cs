﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class WarehousemanRepository : Repository<Warehouseman, WarehousemanId>, IWarehousemanRepository
{
    public WarehousemanRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<List<Warehouseman>>> GetAllDetailedAsync(
        CancellationToken cancellationToken) =>
        Result.Create(await Table
            .Include(w => w.Sector)
            .Include(w => w.Transports)
            .OrderBy(w => w.IdentificationNumber)
            .ToListAsync(cancellationToken));

    public async Task<Result<Warehouseman>> GetByIdNumberAsync(
        IdentificationNumber idNumber,
        CancellationToken cancellationToken) =>
        await Table.FirstOrDefaultAsync(w => w.IdentificationNumber == idNumber, cancellationToken);
}