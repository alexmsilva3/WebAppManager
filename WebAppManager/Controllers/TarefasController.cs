using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;

namespace WebAppManager.Controllers
{
    public class TarefasController : Controller
    {
        ModelTarefa task = new ModelTarefa();
        ModelComandos comando = new ModelComandos();
        ModelServidor servidor = new ModelServidor();
        ViewModelTarefa vwtarefa = new ViewModelTarefa();

        public IActionResult Tarefas()
        {
            vwtarefa.listaTarefas = task.listaTarefa();
            ViewBag.listaServidores = servidor.listaServer();
            ViewBag.listaComandos = comando.listaComandos();
            return View(vwtarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addTarefa(ModelTarefa tarefa)
        {
            task.addTarefa(tarefa);
            return RedirectToAction("Tarefas", "Tarefas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateTarefa(ModelTarefa tarefa)
        {
            task.updateTarefa(tarefa);
            return RedirectToAction("Tarefas", "Tarefas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult removeTarefa(string ridtarefa)
        {
            task.removeTarefa(Int32.Parse(ridtarefa));
            return RedirectToAction("Tarefas", "Tarefas");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult iniciaTarefa(string startidtarefa)
        {
            ModelTarefa tarefinha = task.buscaTarefa(Int32.Parse(startidtarefa));
            tarefinha.data = DateTime.Now;
            task.updateTarefa(tarefinha);
            return RedirectToAction("Tarefas", "Tarefas");
        }
    }
}
