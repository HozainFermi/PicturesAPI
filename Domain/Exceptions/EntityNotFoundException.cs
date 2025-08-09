using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityNotFoundException : AppException
    {
        public object EntityId { get; }
        public Type EntityType { get; }

        public EntityNotFoundException(object id, Type entityType)
            : base($"Entity of type {entityType.Name} with id {id} not found")
        {
            EntityId = id;
            EntityType = entityType;
        }
    }
}
