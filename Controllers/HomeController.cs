using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyTrack.Models;

namespace MoneyTrack.Controllers
{
   public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MoneyTrackRepository _repository;

    public HomeController(ILogger<HomeController> logger, MoneyTrackRepository repository)
    {
        _logger = logger;
        _repository = repository; 
    }

    public IActionResult Index()
    {
        _repository.TesteConexao();
        return View();
    }
    
    public IActionResult Listar()
    {
        List<Contato> listaContatos = _repository.Listar();
        return View(listaContatos);
    }

     public IActionResult ListarFinancas()
    {
        List<Financas> listaFinancas = _repository.ListarFinancas(); 
        return View(listaFinancas);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Contato contato)
    {
        Contato contatoLocalizado = _repository.ValidarLogin(contato);

        if (contatoLocalizado == null)
        {
            ViewBag.Mensagem = "Login ou Senha estão incorretos.";
            return View();
        }
        else
        {
            return RedirectToAction("Lista", "Login");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}