using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MoneyTrack.Models;

namespace MoneyTrack.Controllers
{
    public class MoneyController : Controller
    {
        public IActionResult CadastroContato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroContato(Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            new MoneyTrackRepository().InserirContato(contato);
            ViewBag.Mensagem = "Cadastro de contato realizado com sucesso!";
            return View();
        }

        public IActionResult CadastroFinancas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroFinancas(Financas financas)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            new MoneyTrackRepository().InserirFinancas(financas);
            ViewBag.Mensagem = "Cadastro de finanças realizado com sucesso!";
            return View();
        }

        public IActionResult Remover (int Id)
    {
        MoneyTrackRepository contato = new MoneyTrackRepository();
        Contato contatoLocalizado = contato.BuscarPorId(Id);    
        contato.Remover(contatoLocalizado);
        
        return RedirectToAction("Lista", "Money");
        }

        public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Contato contato)
    {
        MoneyTrackRepository repository = new MoneyTrackRepository();
        Contato contatoEncontrado = repository.ValidarLogin(contato);

        if (contatoEncontrado == null)
        {
            ViewBag.Mensagem = "Login ou Senha estão incorretos.";
            return View();
        }
        else
        {
            
            HttpContext.Session.SetInt32("IdContato", contatoEncontrado.IdContato);
            HttpContext.Session.SetString("NomeUsuario", contatoEncontrado.Nome);

            return RedirectToAction("Lista", "Money"); 
        }
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return View("Login");
    }

      public IActionResult Lista()
    {
        MoneyTrackRepository ur = new MoneyTrackRepository();
        List<MoneyTrack.Models.Contato> listagemContatos = ur.Listar();        
        
        return View(listagemContatos);
    }
    }
    }