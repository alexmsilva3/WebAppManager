using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppManager.Views
{
    public class LogController : Controller
    {
        public IActionResult Log()
        {
            return View();
        }
    }
}
