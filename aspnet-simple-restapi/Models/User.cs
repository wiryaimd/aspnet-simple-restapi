using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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


        // string diawali dengan @ untuk mengignore escape sequence / symbol, ex: symbol backslash pada string \ perlu dibuat double, namun ketika menggunakan @"", bisa menggunakan satu \
        // ex: @"\bisa tanpa double \backslash"
        // ex: "\\perlu double backslash \\ gitu"
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")] // ini work sihh kalau request body nya pake User bukan UserDto
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegisterDate { get; set; }


        public GenderUser? Gender { get; set; }

        public RoleUser Role { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RoleUser { 
            Admin,
            User
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GenderUser { 
            Male, Female
        }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


        public virtual ICollection<Payment> Payments { get; set; }
    }
}
