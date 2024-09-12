namespace COLOR.Services.Interfaces;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpiresHours { get; set; }
}