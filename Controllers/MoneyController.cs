using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MoneyTrack.Models;

namespace MoneyTrack.Controllers
{
    public class MoneyController : Controller
    {

        private readonly MoneyTrackRepository _repository;

        public MoneyController(MoneyTrackRepository repository)
        {
            _repository = repository;            
        }

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

            _repository.InserirContato(contato);
            ViewBag.Mensagem = "Cadastro de contato realizado com sucesso!";
            return View();
        }

public IActionResult CadastroFinancas()
{            
    try
    {
        // Obtenha a lista de todos os contatos
        var usuarios = _repository.ObterTodosContatos();

        // Inicialize uma nova instância de FinancasViewModel
        var viewModel = new FinancasViewModel
        {
            // Inicialize Financas conforme necessário
            Financas = new Financas(),

            // Atribua a lista de usuários ao ViewModel
            Usuarios = usuarios
        };

        return View(viewModel);
    }
    catch (Exception ex)
    {
        ViewData["ErroUsuarios"] = ex.Message;
 
        return View();
    }
}

      [HttpPost]
public IActionResult CadastroFinancas(Financas financas)
{
    try
    {
        if (!ModelState.IsValid)
        {            
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {         
                Console.WriteLine(error.ErrorMessage);
            }
            
            return View();
        }

        new MoneyTrackRepository().InserirFinancas(financas);        
        ViewBag.Mensagem = "Cadastro de finanças realizado com sucesso!";
        return View();
    }
    catch (Exception ex)
    {        
        Console.WriteLine(ex.Message);
        ViewBag.Mensagem = "Erro ao cadastrar as finanças. Por favor, tente novamente.";
        return View();
    }
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

    public IActionResult Editar(int Id)
{
    MoneyTrackRepository repository = new MoneyTrackRepository();
    Contato contato = repository.BuscarPorId(Id);    

    if (contato == null)
    {
        ViewBag.Mensagem = "Contato não encontrado.";
        return RedirectToAction("Lista", "Money");
    }

    return View(contato);
}

[HttpPost]
public IActionResult Editar(Contato contato)
{
    if (!ModelState.IsValid)
    {
        return View(contato);
    }

    MoneyTrackRepository repository = new MoneyTrackRepository();
    repository.AtualizarContato(contato);

    ViewBag.Mensagem = "Contato atualizado com sucesso!";
    return View(contato);
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

            return RedirectToAction("BTC", "Crypto"); 
        }
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return View("Login");
    }  

    public IActionResult ListarFinancas()
    {
        MoneyTrackRepository ur = new MoneyTrackRepository();
        List<MoneyTrack.Models.Financas> listagemFinancas = _repository.ListarFinancas();

        return View(listagemFinancas);
    }

     public IActionResult Lista()
    {
        MoneyTrackRepository ur = new MoneyTrackRepository();
        List<MoneyTrack.Models.Contato> listagemContatos = ur.Listar();        
        
        return View(listagemContatos);
    }
 }
    }