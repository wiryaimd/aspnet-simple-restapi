using System;
using System.Collections.Generic;

namespace aspnet_simple_restapi.Models
{
    public partial class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
