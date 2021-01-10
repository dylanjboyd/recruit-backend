using System;

namespace RecruitBackend.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }

    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}