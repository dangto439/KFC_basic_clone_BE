using KFC.Core.Base;

namespace KFC.Entity
{
    public class Order : BaseEntity
    {
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Relationship
        public string UserId { get; set; }
        public User User { get; set; }  

        public string? VoucherId { get; set; }
        public Voucher Voucher { get; set; }  

        public List<OrderItem> OrderItems { get; set; } 
    }
}
