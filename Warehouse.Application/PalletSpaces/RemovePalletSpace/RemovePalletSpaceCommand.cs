using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.PalletSpaces.RemovePalletSpace;

public sealed record RemovePalletSpaceCommand(Guid Id) : ICommand;
