using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aspnet_simple_restapi.Models
{
    public partial class OrderProduct
    {
        public Guid Id { get; set; }
        public Guid OrderDetailId { get; set; }
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public virtual OrderDetail? OrderDetail { get; set; } = null!;

        [JsonIgnore]
        public virtual Product? Product { get; set; } = null!;
    }
}
