using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class ChamadosDAL : BaseDAL
    {
        private const string ANSI_DATE_FORMAT = "yyyy-MM-dd";

        public IEnumerable<Chamado> ListarChamados()
        {
            IList<Chamado> lstChamados = new List<Chamado>();

            DataTable dtChamados = new DataTable();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {

                    dbCommand.CommandText = 
                        "SELECT chamados.ID, " + 
                        "       Assunto, " +
                        "       Solicitante, " +
                        "       IdDepartamento, " +
                        "       departamentos.Descricao AS Departamento, " + 
                        "       DataAbertura " + 
                        "FROM chamados " + 
                        "INNER JOIN departamentos " +
                        "   ON chamados.IdDepartamento = departamentos.ID ";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        var chamado = Chamado.Empty;

                        while (dataReader.Read())
                        {
                            chamado = MapChamadoFromDataReader(dataReader);

                            lstChamados.Add(chamado);
                        }

                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return lstChamados;
        }

        public Chamado ObterChamado(int idChamado)
        {
            var chamado = Chamado.Empty;

            DataTable dtChamados = new DataTable();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText =
                        "SELECT chamados.ID, " +
                        "       Assunto, " +
                        "       Solicitante, " +
                        "       IdDepartamento, " +
                        "       departamentos.Descricao AS Departamento, " +
                        "       DataAbertura " +
                        "FROM chamados " +
                        "INNER JOIN departamentos " +
                        "   ON chamados.IdDepartamento = departamentos.ID " +
                        $"WHERE chamados.ID = {idChamado}";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            chamado = MapChamadoFromDataReader(dataReader);
                        }

                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return chamado;
        }

        public bool VerificarSeChamadoPossuiDepartamento(int idDepartamento)
        {
            bool possuiChamado = false;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText =
                        "SELECT COUNT(*) " +
                        "FROM chamados " +
                        $"WHERE chamados.IdDepartamento = {idDepartamento}";

                    dbConnection.Open();

                    int count = Convert.ToInt32(dbCommand.ExecuteScalar());

                    possuiChamado = count > 0;

                    dbConnection.Close();
                }
            }

            return possuiChamado;
        }


        public bool GravarChamado(int ID, string Assunto, string Solicitante, int IdDepartamento, DateTime DataAbertura)
        {
            int regsAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    if (ID == 0)
                    {
                        dbCommand.CommandText = 
                            "INSERT INTO chamados (Assunto,Solicitante,IdDepartamento,DataAbertura)" +
                            "VALUES (@Assunto,@Solicitante,@IdDepartamento,@DataAbertura)";
                    }
                    else
                    {
                        dbCommand.CommandText = 
                            "UPDATE chamados " + 
                            "SET Assunto=@Assunto, " + 
                            "    Solicitante=@Solicitante, " +
                            "    IdDepartamento=@IdDepartamento, " + 
                            "    DataAbertura=@DataAbertura " + 
                            "WHERE ID=@ID ";
                    }

                    dbCommand.Parameters.AddWithValue("@Assunto", Assunto);
                    dbCommand.Parameters.AddWithValue("@Solicitante", Solicitante);
                    dbCommand.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
                    dbCommand.Parameters.AddWithValue("@DataAbertura", DataAbertura.ToString(ANSI_DATE_FORMAT));
                    dbCommand.Parameters.AddWithValue("@ID", ID);

                    dbConnection.Open();
                    regsAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }

            }

            return (regsAfetados > 0);

        }

        public bool ExcluirChamado(int idChamado)
        {
            int regsAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = $"DELETE FROM chamados WHERE ID = {idChamado}";

                    dbConnection.Open();
                    regsAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();

                }

            }

            return (regsAfetados > 0);
        }

        private Chamado MapChamadoFromDataReader(SQLiteDataReader dataReader)
        {
            Chamado chamado = new Chamado();

            chamado.ID = dataReader.GetInt32(0);
            chamado.Assunto = dataReader.GetString(1);
            chamado.Solicitante = dataReader.GetString(2);
            chamado.IdDepartamento = dataReader.GetInt32(3);
            chamado.Departamento = dataReader.GetString(4);
            chamado.DataAbertura = DateTime.Parse(dataReader.GetString(5));

            return chamado;
        }
    }
}