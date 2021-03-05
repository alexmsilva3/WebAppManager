using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAppManager.Models;
using static WebAppManager.Services.GeneralServices;

namespace WebAppManager.Services
{
    public class ServiceTarefa
    {
        string connectionString = buscaConexao.GetConnectionString();

        public void addTarefa(ModelTarefa tarefa)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Tarefas (nome,fk_idservidor,fk_idcomando,data) VALUES ('"+tarefa.nome+"','"+tarefa.fk_idservidor+"','"+tarefa.fk_idcomando+"','"+tarefa.data+"')";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void removeTarefa(int idtarefa)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Tarefas WHERE idtarefa = " + idtarefa + ";";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public void updateTarefa(ModelTarefa task)
        {
            if (task.data <= DateTime.MinValue)
            {
                task.data = DateTime.Now;
            }

            using SqlConnection con = new SqlConnection(connectionString);
            string SQL = "UPDATE Tarefas SET nome =  '" + task.nome + "', fk_idservidor = '" + task.fk_idservidor + "', fk_idcomando = '"+ task.fk_idcomando + "', data = '"+task.data+"' " +
                "  WHERE idtarefa = " + task.idtarefa + " ;";

            con.Open();
            SqlCommand command = new SqlCommand(SQL, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public ModelTarefa buscaTarefa(int idtarefa)
        {
            ModelTarefa task = new ModelTarefa();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT idtarefa, Tarefas.nome, fk_idservidor, Servidores.nome as nomeServidor, fk_idcomando, Comandos.nome as nomeComando, data " +
                              "FROM Tarefas " +
                              "INNER JOIN Servidores ON Tarefas.fk_idservidor = Servidores.idservidor " +
                              "INNER JOIN Comandos ON Tarefas.fk_idcomando = Comandos.idcomando " +
                              "WHERE idtarefa = '"+idtarefa+"' ";
                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    task.idtarefa = TratarConversaoDeDados.TrataInt(reader["idtarefa"]);
                    task.nome = TratarConversaoDeDados.TrataString(reader["nome"]);
                    task.fk_idservidor = TratarConversaoDeDados.TrataInt(reader["fk_idservidor"]);
                    task.nomeServidor = TratarConversaoDeDados.TrataString(reader["nomeServidor"]);
                    task.fk_idcomando = TratarConversaoDeDados.TrataInt(reader["fk_idcomando"]);
                    task.nomeComando = TratarConversaoDeDados.TrataString(reader["nomeComando"]);
                    task.data = TratarConversaoDeDados.TrataDateTime(reader["data"]);
                }
                reader.Close();
                con.Close();
            }
            return task;
        }

        public List<ModelTarefa> listaTarefa()
        {
            List<ModelTarefa> listaTask = new List<ModelTarefa>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string SQL = "SELECT idtarefa, Tarefas.nome, fk_idservidor, Servidores.nome as nomeServidor, fk_idcomando, Comandos.nome as nomeComando, data " +
                              "FROM Tarefas " +
                              "INNER JOIN Servidores ON Tarefas.fk_idservidor = Servidores.idservidor " +
                              "INNER JOIN Comandos ON Tarefas.fk_idcomando = Comandos.idcomando " +
                              "ORDER BY 1 ASC ";
                con.Open();
                SqlCommand command = new SqlCommand(SQL, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ModelTarefa taf = new ModelTarefa
                    {
                        idtarefa = TratarConversaoDeDados.TrataInt(reader["idtarefa"]),
                        nome = TratarConversaoDeDados.TrataString(reader["nome"]),
                        fk_idservidor = TratarConversaoDeDados.TrataInt(reader["fk_idservidor"]),
                        nomeServidor = TratarConversaoDeDados.TrataString(reader["nomeServidor"]),
                        fk_idcomando = TratarConversaoDeDados.TrataInt(reader["fk_idcomando"]),
                        nomeComando = TratarConversaoDeDados.TrataString(reader["nomeComando"]),
                        data = TratarConversaoDeDados.TrataDateTime(reader["data"])
                    };
                    listaTask.Add(taf);
                }
                reader.Close();
                con.Close();
            }
            return listaTask;
        }

    }
}
