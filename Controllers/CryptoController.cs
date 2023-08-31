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
    public class CryptoController : Controller
    {
            public IActionResult BTC()
    {
        return View();
    }
}
    }