using MediatR;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.BLL.Abstractions.Messaging;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;