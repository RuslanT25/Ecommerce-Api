namespace Ecommerce.Infrastructure.Services.Tokens;

public class TokenOption
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public int AccessTokenExpiration  { get; set; }
}
