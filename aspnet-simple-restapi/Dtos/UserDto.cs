using aspnet_simple_restapi.Models;

namespace aspnet_simple_restapi.Dtos
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public User.GenderUser? Gender { get; set; }

        public User.RoleUser Role { get; set; }

    }
}
