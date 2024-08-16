using MediatR;
using Warehouse.Domain.Shared.Messaging;

namespace Warehouse.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;