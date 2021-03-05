using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;
using static WebAppManager.Services.GeneralServices;

namespace WebAppManager.Services
{
    public class ServiceVariavel
    {
        string connectionString = buscaConexao.GetConnectionString();

        public void addVariavel(int fkidcomando,string variavel)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Variaveis (nome,fk_idcomando) VALUES ('" + variavel + "', '" + fkidcomando + "');";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void removeVariavel(int idvariavel)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Variaveis WHERE idvariavel = " + idvariavel + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public List<ModelVariavel> listaVariavel()
        {
            List<ModelVariavel> listaVariavel = new List<ModelVariavel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT * FROM Variaveis";

                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelVariavel vr = new ModelVariavel
                    {
                        idvariavel = TratarConversaoDeDados.TrataInt(reader["idvariavel"]),
                        fk_idcomando = TratarConversaoDeDados.TrataInt(reader["fk_idcomando"]),
                        nome = TratarConversaoDeDados.TrataString(reader["nome"]),
                        valor = TratarConversaoDeDados.TrataString(reader["valor"])
                    };

                    listaVariavel.Add(vr);
                }
                reader.Close();
                con.Close();
            }
            return listaVariavel;
        }
    }
}
