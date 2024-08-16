using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Reports.Drivers;

public sealed record GenerateDriverReportCommand(Guid DriverId) : ICommand;