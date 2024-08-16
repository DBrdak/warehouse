using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Freights.RemoveFreight;

public sealed record RemoveFreightCommand(Guid Id) : ICommand;
