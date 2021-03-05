using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class ServidoresController : Controller
    {

        ModelServidor server = new ModelServidor();
        ViewModelServidor vwserver = new ViewModelServidor();

        public IActionResult Servidores()
        {
            vwserver.listaServidores = server.listaServer();
            return View(vwserver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addServer(ModelServidor server)
        {
            server.addServer(server);
            return RedirectToAction("Servidores", "Servidores");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateServer(ModelServidor server)
        {
            server.updateServer(server, server.nome, server.funcao);
            return RedirectToAction("Servidores", "Servidores");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeServer(string ridserver)
        {
            server.removeServer(Int32.Parse(ridserver));
            return RedirectToAction("Servidores", "Servidores");
        }
    }
}
