using DocumentFormat.OpenXml.Drawing.Charts;
using KFC.Core.Base;

namespace KFC.Entity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Relationship
        public string RoleId { get; set; }
        public Role Role { get; set; }  
        public List<Order> Orders { get; set; }
    }
}
