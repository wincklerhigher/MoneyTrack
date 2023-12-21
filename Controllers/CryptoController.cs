using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MoneyTrack.Models;

namespace MoneyTrack.Controllers
{    
    public class CryptoController : Controller
    {
            public IActionResult BTC()
    {
        return View();
    }
}
    }