using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;
using static WebAppManager.Services.GeneralServices;

namespace WebAppManager.Services
{
    public class ServiceServidor
    {
        string connectionString = buscaConexao.GetConnectionString();

        public void addServidor(ModelServidor server)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Servidores (nome,funcao) VALUES ('" + server.nome + "', '" + server.funcao + "');";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void removeServidor(int idservidor)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Servidores WHERE idservidor = " + idservidor + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void updateServidor(ModelServidor server, string nome, string funcao)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "UPDATE Servidores SET nome =  '" + nome + "', funcao = '" + funcao + "' WHERE idservidor = " + server.idservidor + " ;";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public ModelServidor buscaServidor(int idservidor)
        {
            ModelServidor server = new ModelServidor();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Servidores WHERE idservidor = " + idservidor + ";";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    server.idservidor = TratarConversaoDeDados.TrataInt(reader["idservidor"]);
                    server.nome = TratarConversaoDeDados.TrataString(reader["nome"]);
                    server.funcao = TratarConversaoDeDados.TrataString(reader["funcao"]);
                }
                reader.Close();
                con.Close();
            }
            return server;
        }

        public List<ModelServidor> listaServidor()
        {
            List<ModelServidor> listaServer = new List<ModelServidor>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Servidores ORDER BY 1 ASC;";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelServidor gr = new ModelServidor
                    {
                        idservidor = TratarConversaoDeDados.TrataInt(reader["idservidor"]),
                        nome = TratarConversaoDeDados.TrataString(reader["nome"]),
                        funcao = TratarConversaoDeDados.TrataString(reader["funcao"])
                    };

                    listaServer.Add(gr);
                }
                reader.Close();
                con.Close();
            }
            return listaServer;
        }
    }
}
