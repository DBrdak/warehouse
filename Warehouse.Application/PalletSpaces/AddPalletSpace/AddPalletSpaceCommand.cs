using Warehouse.Application.PalletSpaces.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.PalletSpaces.AddPalletSpace;

public sealed record AddPalletSpaceCommand(int Number, int Shelf, int Rack, int SectorNumber) : ICommand<PalletSpaceModel>;
