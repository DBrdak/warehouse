using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.PalletSpaces.UpdatePalletSpace;

public sealed record UpdatePalletSpaceCommand() : ICommand<PalletSpaceModel>;
