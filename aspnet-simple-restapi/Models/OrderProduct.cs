using System;
using System.Collections.Generic;

namespace aspnet_simple_restapi.Models
{
    public partial class OrderProduct
    {
        public Guid OrderDetailId { get; set; }
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }

        public virtual OrderDetail OrderDetail { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
