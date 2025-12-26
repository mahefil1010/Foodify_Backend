using System;
using System.Collections.Generic;

namespace Foodify.Data.Entities
{
    public class Menu
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
