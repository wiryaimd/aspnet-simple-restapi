using System;
using System.Collections.Generic;

namespace aspnet_simple_restapi.Models
{
    public partial class Payment
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public int PaymentType { get; set; }
        public double Balance { get; set; }

        public virtual User? User { get; set; }
    }
}
