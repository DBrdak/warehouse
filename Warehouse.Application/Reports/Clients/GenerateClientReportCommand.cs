using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Reports.Clients;

public sealed record GenerateClientReportCommand(Guid ClientId) : ICommand;