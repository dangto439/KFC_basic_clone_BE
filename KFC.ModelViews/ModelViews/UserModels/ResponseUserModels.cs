using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.ModelViews.ModelViews.UserModels
{
    public class ResponseUserModels
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleId { get; set; }

        public DateTimeOffset CreatedTime { get; set; }
        public string CreatedBy { get; set; }
    }
}
