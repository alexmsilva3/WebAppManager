using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Services;

namespace WebAppManager.Models
{
    public class ModelServidor
    {
        public int idservidor { get; set; }
        public string nome { get; set; }
        public string funcao { get; set; }

        ServiceServidor serverService = new ServiceServidor();

        public void addServer(ModelServidor server)
        {
            serverService.addServidor(server);
        }

        public void removeServer(int idservidor)
        {
            serverService.removeServidor(idservidor);
        }

        public void updateServer(ModelServidor server, string nome, string funcao)
        {
            serverService.updateServidor(server, nome, funcao);
        }

        public ModelServidor buscaServer(int idservidor)
        {
            return serverService.buscaServidor(idservidor);
        }

        public List<ModelServidor> listaServer()
        {
            return serverService.listaServidor();
        }
    }

    public class ViewModelServidor
    {
        public ModelServidor server { get; set; }
        public List<ModelServidor> listaServidores { get; set; }
    }
}
