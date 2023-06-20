namespace OnlineExamSystem.Domain.Identity;
public class TokenInfo
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}
