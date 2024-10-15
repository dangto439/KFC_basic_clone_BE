using Microsoft.AspNetCore.Identity;
using KFC.Core.Utils;

namespace KFC.Entity
{
    public class Role : IdentityRole<Guid>
    {
        protected Role()
        {
            Id = Guid.NewGuid();
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        // Relationship
        public List<User> Users { get; set; }
    }
}
