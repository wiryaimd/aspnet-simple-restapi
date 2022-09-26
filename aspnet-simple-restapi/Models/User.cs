using System;
using System.Collections.Generic;

namespace aspnet_simple_restapi.Models
{
    public partial class User
    {
        public User()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Payments = new HashSet<Payment>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? Gender { get; set; }
        public int Role { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
