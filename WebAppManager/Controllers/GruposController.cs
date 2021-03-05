using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class GruposController : Controller
    {
        ModelGrupo grupo = new ModelGrupo();
        ViewModelGrupo vwgrupo = new ViewModelGrupo();
        

        public IActionResult Grupos()
        { 
            vwgrupo.listaGrupos = grupo.listaGrupo();
            return View(vwgrupo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addGrupo(ModelGrupo grupo)
        {
            grupo.addGrupo(grupo);
            return RedirectToAction("Grupos", "Grupos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateGrupo(ModelGrupo grupo)
        {
            grupo.updateGrupo(grupo, grupo.nome);
            return RedirectToAction("Grupos", "Grupos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeGrupo(string ridgrupo)
        {
            grupo.removeGrupo(Int32.Parse(ridgrupo));
            return RedirectToAction("Grupos", "Grupos");
        }
    }
}
