using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        IHostingEnvironment _appEnvironment;
        public ComandosController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

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
            comando.addComando(comando, _appEnvironment.WebRootPath);
            return RedirectToAction("Comandos", "Comandos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult updateComando(ModelComandos comando)
        {
            comando.updateComando(comando, comando.nome, comando.fk_idgrupo, _appEnvironment.WebRootPath);
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

        //public async Task<IActionResult> EnviarArquivo(IFormFile arquivo)
        //{

        //    // caminho completo do arquivo na localização temporária
        //    var caminhoArquivo = Path.GetTempFileName();

        //    //verifica se existem arquivos 
        //    if (arquivo == null || arquivo.Length == 0)
        //    {
        //        //retorna a viewdata com erro
        //        ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
        //        return View(ViewData);
        //    }
        //    //define a pasta onde vamos salvar os arquivos
        //    string pasta = "CommandRepo";
        //    // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
        //    string nomeArquivo = arquivo.FileName;
        //    //< obtém o caminho físico da pasta wwwroot >
        //    string caminho_WebRoot = _appEnvironment.WebRootPath;
        //    // monta o caminho onde vamos salvar o arquivo : 
        //    string caminhoDestinoArquivo = Path.Combine(caminho_WebRoot, pasta, nomeArquivo);

        //    //Verifica se a pasta existe, caso não, cria
        //    if (!Directory.Exists(Path.GetDirectoryName(caminhoDestinoArquivo)))
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(caminhoDestinoArquivo));
        //    }

        //    //copia o arquivo para o local de destino original
        //    using (var stream = new FileStream(caminhoDestinoArquivo, FileMode.Create))
        //    {
        //        await arquivo.CopyToAsync(stream);
        //    }
        //    //monta a ViewData que será exibida na view como resultado do envio 
        //    ViewData["Resultado"] = $"{arquivo} arquivos foram enviados ao servidor";
        //    //retorna a viewdata
        //    return View(ViewData);
        //}
    }
}
