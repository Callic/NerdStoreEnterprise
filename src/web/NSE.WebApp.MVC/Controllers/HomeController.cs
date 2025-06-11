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


        [HttpGet("sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            ErrorViewModel model = new();
            model.ErrorCode = 503;
            model.Mensagem = "O sistema está temporariamente indisponível. Tente novamente mais tarde ou entre em contato com nosso suporte";
            model.Titulo = "Sistema Indisponível";
            return View("Error", model);
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
                model.Mensagem = "A página que está você está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                model.Titulo = "Página não encontrada.";
            }
            if(id == 403)
            {
                model.Mensagem = "Você não tem permissão para fazer isto";
                model.Titulo = "Acesso negado!";
            }

            return View("Error", model);
        }
    }
}
