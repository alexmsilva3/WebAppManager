using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class ComandosController : Controller
    {
        ModelComandos comando = new ModelComandos();
        ModelVariavel variavel = new ModelVariavel();
        ViewModelComandos vwcomando = new ViewModelComandos();
        ModelGrupo grupo = new ModelGrupo();

        public IActionResult Comandos()
        {
            vwcomando.listaComandos = comando.listaComandos();
            vwcomando.listaVariaveis = variavel.listaVariavel();
            ViewBag.listaGrupos = grupo.listaGrupo();
            return View(vwcomando);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addComando(ModelComandos comando)
        {
            comando.addComando(comando);
            return RedirectToAction("Comandos", "Comandos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateComando(ModelComandos comando)
        {
            comando.updateComando(comando, comando.nome, comando.fk_idgrupo);
            return RedirectToAction("Comandos", "Comandos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeComando(string ridcomando)
        {
            comando.removeComando(Int32.Parse(ridcomando));
            return RedirectToAction("Comandos", "Comandos");
        }

        //VARIAVEIS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addVariavel(string vidcomando, string nome)
        {
            variavel.addVariavel(Int32.Parse(vidcomando),nome);
            return RedirectToAction("Comandos", "Comandos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeVariavel(string ridvar)
        {
            variavel.removeVariavel(Int32.Parse(ridvar));
            return RedirectToAction("Comandos", "Comandos");
        }
    }
}
