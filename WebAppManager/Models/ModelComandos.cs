using Microsoft.AspNetCore.Http;
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
        public string grupo { get; set; }
        public IFormFile arquivo { get; set; }
        public string arquivoNome { get; set; }

        ServiceComando comandService = new ServiceComando();

        public bool addComando(ModelComandos comand, string caminho)
        {
            comandService.addComando(comand);
            return comandService.salvarArquivo(comand.arquivo, caminho).Result;
        }

        public void removeComando(int idcomando)
        {
            comandService.removeComando(idcomando);

        }

        public bool updateComando(ModelComandos com, string nome, int fk_grupo, string caminho)
        {
            comandService.updateComando(com, nome, fk_grupo);
            return comandService.salvarArquivo(com.arquivo, caminho).Result;
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
