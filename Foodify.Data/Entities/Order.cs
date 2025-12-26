using System;
using System.Collections.Generic;

namespace Foodify.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<DeliveryAssignment> DeliveryAssignments { get; set; }
    }
}
