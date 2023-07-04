namespace JWT_Token_Generator.Model
{
    public class Token
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public DateTime IssuedAt { get; set; } = DateTime.Now;
        public DateTime ExpirationTime { get; set; } = DateTime.Now;
        public string Username { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string>();
        public string SecretKey { get; set; } = null!;
    }
}
