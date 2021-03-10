using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;
using static WebAppManager.Services.GeneralServices;

namespace WebAppManager.Services
{
    public class ServiceComando
    { 
        string connectionString = buscaConexao.GetConnectionString();

        public void addComando(ModelComandos comand)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Comandos (nome,fk_idgrupo, arquivo) VALUES ('" + comand.nome + "', '" + comand.fk_idgrupo + "', '"+comand.arquivo.FileName+"');";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        #region removeComando
        public void removeComando(int idcomando)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Comandos WHERE idcomando = " + idcomando + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        public void updateComando(ModelComandos comand, string nome, int fk_idgrupo)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "UPDATE Comandos SET nome =  '" + nome + "', fk_idgrupo = '" + fk_idgrupo + "', arquivo = '"+comand.arquivo.FileName+"' WHERE idcomando = " + comand.idcomando + " ;";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public ModelComandos buscaComando(int idcomando)
        {
            ModelComandos comand = new ModelComandos();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT idcomando, arquivo, Comandos.nome, Grupos.nome as grupo, Grupos.idgrupo as fk_idgrupo FROM Comandos" +
                    " INNER JOIN Grupos" +
                    " ON Comandos.fk_idgrupo = Grupos.idgrupo" +
                    " WHERE idcomando = " + idcomando + ";";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comand.idcomando = TratarConversaoDeDados.TrataInt(reader["idcomando"]);
                    comand.arquivoNome = TratarConversaoDeDados.TrataString(reader["arquivo"]);
                    comand.nome = TratarConversaoDeDados.TrataString(reader["nome"]);
                    comand.grupo = TratarConversaoDeDados.TrataString(reader["grupo"]);
                    comand.fk_idgrupo = TratarConversaoDeDados.TrataInt(reader["fk_idgrupo"]);
                }
                reader.Close();
                con.Close();
            }
            return comand;
        }

        public List<ModelComandos> listaComandos()
        {
            List<ModelComandos> listaComando = new List<ModelComandos>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT idcomando, arquivo, Comandos.nome, Grupos.nome as grupo, Grupos.idgrupo as fk_idgrupo FROM Comandos" +
                    " INNER JOIN Grupos" +
                    " ON Comandos.fk_idgrupo = Grupos.idgrupo" +
                    " ORDER BY 1 ASC;";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelComandos cm = new ModelComandos
                    {
                        idcomando = TratarConversaoDeDados.TrataInt(reader["idcomando"]),
                        nome = TratarConversaoDeDados.TrataString(reader["nome"]),
                        arquivoNome = TratarConversaoDeDados.TrataString(reader["arquivo"]),
                        fk_idgrupo = TratarConversaoDeDados.TrataInt(reader["fk_idgrupo"]),
                        grupo = TratarConversaoDeDados.TrataString(reader["grupo"])
                    };

                    listaComando.Add(cm);
                }
                reader.Close();
                con.Close();
            }
            return listaComando;
        }

        public async Task<bool> salvarArquivo(IFormFile arquivo, string caminho)
        {

            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            //verifica se existem arquivos 
            if (arquivo == null || arquivo.Length == 0)
            {
                //retorna a viewdata com erro
                return false;
            }
            //define a pasta onde vamos salvar os arquivos
            string pasta = "CommandRepo";
            // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
            string nomeArquivo = arquivo.FileName;
            //< obtém o caminho físico da pasta wwwroot >
            string caminho_WebRoot = caminho;
            // monta o caminho onde vamos salvar o arquivo : 
            string caminhoDestinoArquivo = Path.Combine(caminho_WebRoot, pasta, nomeArquivo);

            //Verifica se a pasta existe, caso não, cria
            if (!Directory.Exists(Path.GetDirectoryName(caminhoDestinoArquivo)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoDestinoArquivo));
            }

            //copia o arquivo para o local de destino original
            using (var stream = new FileStream(caminhoDestinoArquivo, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }
            return true;
        }
    }
}
