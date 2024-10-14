using DocumentFormat.OpenXml.Drawing.Charts;
using KFC.Core.Base;

namespace KFC.Entity
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; } 
        public decimal DiscountAmount { get; set; } 
        public DateTimeOffset ExpiryDate { get; set; }

        // Relationship
        public List<Order> Orders { get; set; }
    }
}
