using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class DepartamentosDAL : BaseDAL
    {
        public IEnumerable<Departamento> ListarDepartamentos()
        {
            IList<Departamento> lstDepartamentos = new List<Departamento>();
            var xxx = AppDomain.CurrentDomain.BaseDirectory;
            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {

                    dbCommand.CommandText = "SELECT * FROM departamentos";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        var departamento = Departamento.Empty;

                        while (dataReader.Read())
                        {
                            departamento = MapDepartamentoFromDataReader(dataReader);

                            lstDepartamentos.Add(departamento);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return lstDepartamentos;
        }

        public Departamento ObterDepartamento(int idDepartamento)
        {
            var departamento = Departamento.Empty;

            DataTable dtDepartamentos = new DataTable();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText =
                        "SELECT departamentos.Id, " +
                        "       departamentos.Descricao " +
                        "FROM departamentos " +
                        $"WHERE departamentos.Id = {idDepartamento}";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            departamento = MapDepartamentoFromDataReader(dataReader);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return departamento;
        }

        public bool GravarDepartamento(int Id, string Descricao)
        {
            int regsAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    if (Id == 0)
                    {
                        dbCommand.CommandText =
                            "INSERT INTO departamentos (Descricao)" +
                            "VALUES (@Descricao)";
                    }
                    else
                    {
                        dbCommand.CommandText =
                            "UPDATE departamentos " +
                            "SET Descricao=@Descricao " +
                            "WHERE ID=@ID ";
                    }

                    dbCommand.Parameters.AddWithValue("@Descricao", Descricao);
                    dbCommand.Parameters.AddWithValue("@ID", Id);

                    dbConnection.Open();
                    regsAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }

            }

            return regsAfetados > 0;

        }

        public bool ExcluirDepartamento(int idDepartamento)
        {
            int regsAfetados = -1;

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = $"DELETE FROM departamentos WHERE ID = {idDepartamento}";

                    dbConnection.Open();
                    regsAfetados = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();

                }

            }

            return regsAfetados > 0;
        }

        private Departamento MapDepartamentoFromDataReader(SQLiteDataReader dataReader)
        {
            Departamento departamento = new Departamento();

            departamento.ID = dataReader.GetInt32(0);
            departamento.Descricao = dataReader.GetString(1);

            return departamento;
        }
    }
}