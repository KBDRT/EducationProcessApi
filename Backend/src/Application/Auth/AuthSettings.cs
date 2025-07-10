namespace Application.Auth
{
    public class AuthSettings
    {
        public string SecretKey { get; set; } = string.Empty;

        public TimeSpan TokenLifeTime { get; set; }
        
        public string CookieNameForToken { get; set; }  = string.Empty;

    }
}
 