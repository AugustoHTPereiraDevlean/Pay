using System;

namespace Pay.Core.Base
{
    public abstract class Model
    {
        public Model()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Model(Guid id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}