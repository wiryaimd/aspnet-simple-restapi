using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aspnet_simple_restapi.Models
{
    public partial class User
    {
        public User()
        {
            Albums = new HashSet<Album>();
            OrderDetails = new HashSet<OrderDetail>();
            Payments = new HashSet<Payment>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public GenderUser? Gender { get; set; }
        public RoleUser Role { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RoleUser { 
            Admin, User
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GenderUser { 
            Male, Female
        }

        public virtual ICollection<Album> Albums { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
