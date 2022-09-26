using System;
using System.Collections.Generic;

namespace aspnet_simple_restapi.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Category { get; set; }
        public int Unit { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
