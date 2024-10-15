using DocumentFormat.OpenXml.Drawing.Charts;
using KFC.Core.Base;
using KFC.Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace KFC.Entity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        // Relationship
        public string RoleId { get; set; }
        public Role Role { get; set; }  
        public List<Order> Orders { get; set; }
    }
}
