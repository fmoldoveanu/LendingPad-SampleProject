using System;
using BusinessEntities;

namespace WebApi.Models
{
    public abstract class IdObjectData
    {
        public IdObjectData(Guid id)
        {
            Id = id;
        }

        public IdObjectData(IdObject entity) : this(entity != null ? entity.Id : Guid.Empty)
        {
        }

        public Guid Id { get; set; }
    }
}