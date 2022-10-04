using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aspnet_simple_restapi.Models
{
    public partial class Album
    {
        public Album()
        {
            Photos = new HashSet<Photo>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual User? User { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Photo>? Photos { get; set; }
    }
}
