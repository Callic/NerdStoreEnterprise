using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace NSE.Identidade.API.Controllers
{
    
    [ApiController]
    public abstract class MainController : ControllerBase
    {

        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(HttpStatusCode statusCode, object? obj = null)
        {
            if (!OperacaoInvalida())
            {
                return StatusCode((int) statusCode, obj);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErro(erro.ErrorMessage);
            }
            return CustomResponse(HttpStatusCode.BadRequest);
        }

        protected bool OperacaoInvalida()
        {
            return Erros.Any();
        }

        protected void AdicionarErro(string erro)
        {
            Erros.Add(erro);
        }

    }
}
