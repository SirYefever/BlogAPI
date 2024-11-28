namespace Infrastructure;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpirationInMinutes { get; set; }
    
}