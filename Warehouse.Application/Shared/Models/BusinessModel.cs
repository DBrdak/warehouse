using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Shared;

namespace Warehouse.Application.Shared.Models;

public abstract record BusinessModel<TEntity, TEntityId>(Guid Id)
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId, new();