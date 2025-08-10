namespace Foodify.Data.DTOs
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
