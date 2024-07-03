using Warehouse.Application.Clients.Models;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Clients.AddClient;

public sealed record AddClientCommand(string Name, string Nip) : ICommand<ClientModel>;