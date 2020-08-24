namespace Cadastro.Services.Configs
{    public class TokenConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
