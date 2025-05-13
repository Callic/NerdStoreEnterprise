using Microsoft.Extensions.Options;
using NSE.Core.Models.ViewModels;
using NSE.WebApp.MVC.Extensions;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AutenticacaoService> _logger;
        private readonly AppSettings _appSettings;
        public AutenticacaoService(IHttpClientFactory httpClientFactory,
                                   ILogger<AutenticacaoService> logger,
                                   IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<Token> LoginAsync(UsuarioLogin usuarioLogin)
        {
            var _ = JsonSerializer.Serialize(usuarioLogin);
            var content = new StringContent(_, Encoding.UTF8, "application/json");

            using var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync($"{_appSettings.AutenticacaoUrl}/api/Auth/login", content);

            var contentResp = await response.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!TratarRespostaErro(response))
            {
                return new Token
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(contentResp, opt)!
                };
            }

            _logger.LogInformation(contentResp);

            return JsonSerializer.Deserialize<Token>(contentResp, opt)!;
        }

        public async Task<Token> RegistroAsync(UsuarioRegistro usuarioRegistro)
        {
            var _ = JsonSerializer.Serialize(usuarioRegistro);
            var content = new StringContent(_, Encoding.UTF8, "application/json");

            using var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync($"{_appSettings.AutenticacaoUrl}/api/Auth/cadastrar", content);

            var contentResp = await response.Content.ReadAsStringAsync();
            if (!TratarRespostaErro(response))
            {
                return new Token
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(contentResp)!
                };
            }

            _logger.LogInformation(contentResp);

            return JsonSerializer.Deserialize<Token>(contentResp)!;

        }
    }
}
