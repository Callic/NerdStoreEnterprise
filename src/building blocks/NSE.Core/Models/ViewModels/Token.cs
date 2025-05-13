using System.Text.Json.Serialization;

namespace NSE.Core.Models.ViewModels
{
    public class Token
    {
        [JsonPropertyName("acessToken")]
        public string AcessToken { get; set; }

        [JsonPropertyName("expireIn")]
        public int ExpireIn { get; set; }

        [JsonPropertyName("usuarioToken")]
        public UsuarioToken UsuarioToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

   


}
