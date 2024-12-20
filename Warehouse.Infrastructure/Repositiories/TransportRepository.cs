﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal sealed class TransportRepository : Repository<Transport, TransportId>, ITransportRepository
{
    public TransportRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Result<List<Transport>>> GetAllExportsAsync(CancellationToken cancellationToken) =>
        await Table
            .Include(t => t.Client)
            .Include(t => t.Driver)
            .Include(t => t.Warehouseman)
            .Where(t => t.Type == TransportType.Export)
            .ToListAsync(cancellationToken);

    public async Task<Result<List<Transport>>> GetAllImportsAsync(CancellationToken cancellationToken) =>
        await Table
            .Include(t => t.Client)
            .Include(t => t.Driver)
            .Include(t => t.Warehouseman)
            .Where(t => t.Type == TransportType.Import)
            .ToListAsync(cancellationToken);

    public async Task<Result<int>> GetLastTransportNumberAsync(CancellationToken cancellationToken) =>
        (await Table
            .OrderByDescending(t => t.Number)
            .Select(t => t.Number)
            .FirstOrDefaultAsync(cancellationToken))?.Value ?? 0;

    public async Task<Result<Transport>> GetDetailedByIdAsync(TransportId transportId, CancellationToken cancellationToken) =>
        await Table
            .Include(t => t.Client)
            .Include(t => t.Driver)
            .Include(t => t.Warehouseman)
            .Include(t => t.DeliveredFreights)
            .ThenInclude(f => f.Export)
            .Include(t => t.ReceivedFreights)
            .ThenInclude(f => f.Import)
            .Include(t => t.DeliveredFreights)
            .ThenInclude(f => f.PalletSpace)
            .ThenInclude(ps => ps.Sector)
            .Include(t => t.ReceivedFreights)
            .ThenInclude(f => f.PalletSpace)
            .ThenInclude(ps => ps.Sector)
            .FirstOrDefaultAsync(t => t.Id == transportId, cancellationToken);
}