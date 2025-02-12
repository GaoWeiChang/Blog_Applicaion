namespace backend.Models.Dto
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles {  get; set; }
    }

    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
