using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspnet_simple_restapi.Models
{
    public partial class Payment
    {
        public Guid Id { get; set; }
        [Required]
        public Guid? UserId { get; set; }
        public int PaymentType { get; set; }

        [Required]
        public double Balance { get; set; }

        public virtual User? User { get; set; }
    }
}
