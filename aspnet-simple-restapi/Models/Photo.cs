using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aspnet_simple_restapi.Models
{
    public partial class Photo
    {
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }

        [JsonIgnore]
        public virtual Album Album { get; set; } = null!;
    }
}
