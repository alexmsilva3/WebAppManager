using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Services;

namespace WebAppManager.Models
{
    public class ModelGrupo
    {
        public int idgrupo { get; set; }
        public string nome { get; set; }

        ServiceGrupo grupoService = new ServiceGrupo();

        public void addGrupo(ModelGrupo grupo)
        {    
            grupoService.addGrupo(grupo);
        }

        public void removeGrupo(int idgrupo)
        {
            grupoService.removeGrupo(idgrupo);
        }

        public void updateGrupo(ModelGrupo grupo, string nome)
        {
            grupoService.updateGrupo(grupo, nome);
        }

        public ModelGrupo buscaGrupo(int idgrupo)
        {
            return grupoService.buscaGrupo(idgrupo);
        }

        public List<ModelGrupo> listaGrupo()
        {
            return grupoService.listaGrupo();
        }
    }

    public class ViewModelGrupo
    {
        public ModelGrupo grupo { get; set; }
        public List<ModelGrupo> listaGrupos { get; set; }
    }
}
