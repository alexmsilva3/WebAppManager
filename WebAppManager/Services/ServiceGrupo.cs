using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static WebAppManager.Services.GeneralServices;

namespace WebAppManager.Services
{
    public class ServiceGrupo
    {
        string connectionString = buscaConexao.GetConnectionString();

        public void addGrupo(ModelGrupo grupo)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Grupos (nome) VALUES ('" + grupo.nome + "');";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void removeGrupo(int idgrupo)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Grupos WHERE idgrupo = " + idgrupo + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void updateGrupo(ModelGrupo grupo, string nome)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "UPDATE Grupos SET nome =  '" + nome + "' WHERE idgrupo = " + grupo.idgrupo + " ;";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public ModelGrupo buscaGrupo(int idgrupo)
        {
            ModelGrupo grupo = new ModelGrupo();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Grupos idgrupo = " + idgrupo + ";";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    grupo.idgrupo = TratarConversaoDeDados.TrataInt(reader["idgrupo"]);
                    grupo.nome = TratarConversaoDeDados.TrataString(reader["nome"]);
                }
                reader.Close();
                con.Close();
            }
            return grupo;
        }

        public List<ModelGrupo> listaGrupo()
        {
            List<ModelGrupo> listaGrupo = new List<ModelGrupo>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Grupos ORDER BY 1 ASC;";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelGrupo gr = new ModelGrupo
                    {
                        idgrupo = TratarConversaoDeDados.TrataInt(reader["idgrupo"]),
                        nome = TratarConversaoDeDados.TrataString(reader["nome"])
                    };

                    listaGrupo.Add(gr);
                }
                reader.Close();
                con.Close();
            }
            return listaGrupo;
        }
    }
}
