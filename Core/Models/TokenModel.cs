namespace Core.Models
{
    public class TokenModel
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string[] ValidIssuers { get; set; }
        public string[] ValidAudiences { get; set; }
        public string Secret { get; set; }
        public string EncryptKey { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
    }
}
