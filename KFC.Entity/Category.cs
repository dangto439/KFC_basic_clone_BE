using KFC.Core.Base;

namespace KFC.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        // Relationship
        public List<Product> Products { get; set; } 
    }
}
