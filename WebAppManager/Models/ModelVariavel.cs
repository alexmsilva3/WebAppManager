using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Services;

namespace WebAppManager.Models
{
    public class ModelVariavel
    {
        public int idvariavel { get; set; }
        public int fk_idcomando { get; set; }
        public string nome { get; set; }
        public string valor { get; set; }

        ServiceVariavel variavelService = new ServiceVariavel();

        public void addVariavel(int fkidcomando,string variavel)
        {
            variavelService.addVariavel(fkidcomando, variavel);
        }

        public void removeVariavel(int idvariavel)
        {
            variavelService.removeVariavel(idvariavel);
        }

        public List<ModelVariavel> listaVariavel()
        {
            return variavelService.listaVariavel();
             
        }
    }
}
