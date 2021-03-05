using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Services;

namespace WebAppManager.Models
{
    public class ModelTarefa
    {
        public int idtarefa { get; set; }
        public string nome { get; set; }
        public int fk_idservidor { get; set; }
        public string nomeServidor { get; set; }
        public int fk_idcomando { get; set; }
        public string nomeComando { get; set; }
        public DateTime data { get; set; }

        ServiceTarefa taskService = new ServiceTarefa();

        public void addTarefa(ModelTarefa task)
        {
            taskService.addTarefa(task);
        }

        public void removeTarefa(int idtarefa)
        {
            taskService.removeTarefa(idtarefa);
        }

        public void updateTarefa(ModelTarefa task)
        {
            taskService.updateTarefa(task);
        }

        public ModelTarefa buscaTarefa(int idtarefa)
        {
            return taskService.buscaTarefa(idtarefa);
        }

        public List<ModelTarefa> listaTarefa()
        {
            return taskService.listaTarefa();
        }
    }

    public class ViewModelTarefa
    {
        public List<ModelTarefa> listaTarefas { get; set; }

    }
}
