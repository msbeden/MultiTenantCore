using System.ComponentModel.DataAnnotations.Schema;

namespace Multiple.Models.Abstractions
{
    public abstract class BaseEntity
    {
        [NotMapped]
        public DateTime UsedTime => DateTime.Now;

        public short? Status { get; set; }
    }
}
