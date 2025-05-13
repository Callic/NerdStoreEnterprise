using Microsoft.AspNetCore.Mvc;
using NSE.Core.Models.ViewModels;
using System.Diagnostics;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("erro/{id}")]
        public IActionResult Error(int id)
        {
            ErrorViewModel model = new();
            model.ErrorCode = id;
            if(id >= 500)
            {
                model.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou entre em contato com nosso suporte";
                model.Titulo = "Ocorreu um erro!";
            }
            if(id == 404)
            {
                model.Mensagem = "A p�gina que est� voc� est� procurando n�o existe! <br />Em caso de d�vidas entre em contato com nosso suporte";
                model.Titulo = "P�gina n�o encontrada.";
            }
            if(id == 403)
            {
                model.Mensagem = "Voc� n�o tem permiss�o para fazer isto";
                model.Titulo = "Acesso negado!";
            }

            return View("Error", model);
        }
    }
}
