using Microsoft.AspNetCore.Mvc;
using NSE.Core.Models.ViewModels;

namespace NSE.WebApp.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        protected bool PossuiErros(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach (var item in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return true;
            }
            return false;
        }
    }
}
