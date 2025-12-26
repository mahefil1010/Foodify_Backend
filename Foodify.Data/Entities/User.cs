
using Microsoft.AspNetCore.Identity;
using System;

namespace Foodify.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
