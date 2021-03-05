using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            string SQL = "INSERT INTO Comandos (nome,fk_idgrupo) VALUES ('" + comand.nome + "', '" + comand.fk_idgrupo + "');";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void removeComando(int idcomando)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Comandos WHERE idcomando = " + idcomando + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void updateComando(ModelComandos comand, string nome, int fk_idgrupo)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "UPDATE Comandos SET nome =  '" + nome + "', fk_idgrupo = '" + fk_idgrupo + "' WHERE idcomando = " + comand.idcomando + " ;";

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
                string SQL = "SELECT idcomando, Comandos.nome, Grupos.nome as grupo, Grupos.idgrupo as fk_idgrupo FROM Comandos" +
                    " INNER JOIN Grupos" +
                    " ON Comandos.fk_idgrupo = Grupos.idgrupo" +
                    " WHERE idcomando = " + idcomando + ";";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comand.idcomando = TratarConversaoDeDados.TrataInt(reader["idcomando"]);
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
                string SQL = "SELECT idcomando, Comandos.nome, Grupos.nome as grupo, Grupos.idgrupo as fk_idgrupo FROM Comandos" +
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
    }
}
