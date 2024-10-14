using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using KFC.Core.Constants;
using KFC.Core.ExceptionCustom;
using KFC.Core.Utils;

namespace KFC.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        // Navigation properties
        public ApplicationUser()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
        public static void checkExisted(ApplicationUser user)
        {
            if( user == null || user.DeletedTime.HasValue)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Người dùng không tồn tại hoặc đã bị xoá");
            }
        }
        public static void checkIfExisted(ApplicationUser user)
        {
            if (user != null && user.DeletedTime == null)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ResponseCodeConstants.INVALID_INPUT, "Username đã tồn tại");
            }
        }
    }
}
