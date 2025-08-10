using System;
using System.Collections.Generic;

namespace Foodify.Data.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public User Owner { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
