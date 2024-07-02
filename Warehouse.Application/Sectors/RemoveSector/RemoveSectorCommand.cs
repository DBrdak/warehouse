using Warehouse.Application.Sectors.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Sectors.RemoveSector;

public sealed record RemoveSectorCommand() : ICommand;
