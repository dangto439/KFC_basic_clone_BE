
using KFC.Core.Base;

namespace KFC.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } 

        // Relationship
        public List<User> Users { get; set; }
    }
}
