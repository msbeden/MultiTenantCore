using Multiple.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multiple.Models.DatabaseModels.Product
{
    public class Products : BaseEntity, IMustHaveTenant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Products() { }
        public Products(string name, string description, int rate)
        {
            Name = name;
            Description = description;
            Rate = rate;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Rate { get; private set; }
        public string TenantId { get; set; }
    }
}
