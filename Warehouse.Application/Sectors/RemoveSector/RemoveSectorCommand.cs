using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.RemoveSector;

public sealed record RemoveSectorCommand(Guid Id) : ICommand;
