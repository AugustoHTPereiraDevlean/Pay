using System;

namespace Pay.Models.Base
{
    public abstract class Model
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}