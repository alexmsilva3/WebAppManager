using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Services;

namespace WebAppManager.Models
{
    public class ModelComandos
    {
        public int idcomando { get; set; }
        public string nome { get; set; }
        public int fk_idgrupo { get; set; }
        public string  grupo { get; set; }

        ServiceComando comandService = new ServiceComando();

        public void addComando(ModelComandos comand)
        {
            comandService.addComando(comand);
        }

        public void removeComando(int idcomando)
        {
            comandService.removeComando(idcomando);
        }

        public void updateComando(ModelComandos com, string nome, int fk_grupo)
        {
            comandService.updateComando(com, nome, fk_grupo);
        }

        public ModelComandos buscaComando(int idcomando)
        {
            return comandService.buscaComando(idcomando);
        }

        public List<ModelComandos> listaComandos()
        {
            return comandService.listaComandos();
        }
    }

    public class ViewModelComandos
    {
        public ModelComandos comandos { get; set; }
        public List<ModelComandos> listaComandos { get; set; }
        public List<ModelVariavel> listaVariaveis { get; set; }
    }
}
