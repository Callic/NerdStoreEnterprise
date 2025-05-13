namespace NSE.WebAPI.Core.Identidade
{
    public class AppSettings
    {
        public JwtSettings JwtSettings { get; set; } = new JwtSettings();
    }
    public class JwtSettings
    {
        public string? Secret { get; set; }
        public string? ExpiracaoHoras { get; set; }
        public string? Emissor { get; set; }
        public string? ValidoEm { get; set; }
    }
}
