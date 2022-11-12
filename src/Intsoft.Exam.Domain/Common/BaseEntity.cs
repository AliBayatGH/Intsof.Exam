

namespace Intsoft.Exam.Domain.Common
{
    public interface IEntity
    {
    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
