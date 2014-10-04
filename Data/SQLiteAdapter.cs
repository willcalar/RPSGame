using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Data
{
    public sealed class SQLiteAdapter
    {

        #region Constructor

        public SQLiteAdapter()
        {
            SQLiteConnection.CreateFile("RPSGame.sqlite");
            _conexion = new SQLiteConnection("Data Source=RPSGame.sqlite;Version=3;");

            ExecuteNonQuery("create table User (username varchar(30), wins int, points int)");
        }

        #endregion

        #region Metodos

        private SQLiteCommand CreateCommand(string query)
        {
            var comando = new SQLiteCommand(query, _conexion)
            {
                CommandType = CommandType.Text
            };
            return comando;
        }

        public DataSet ExecuteQuery(string query)
        {
            var dataSet = new DataSet();
            try
            {
                var adapter = new SQLiteDataAdapter(CreateCommand(query));
                _conexion.Open();
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                //ExceptionLogger.LogExcepcion(ex, "Error consulta ejecutando: " + pProcedureName);
                dataSet = null;
            }
            finally
            {
                if (_conexion != null && _conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                }
            }
            return dataSet;
        }

        public bool ExecuteNonQuery(string query)
        {
            try
            {
                var comando = CreateCommand(query);
                _conexion.Open();
                int resultado = comando.ExecuteNonQuery();
                _conexion.Close();
                return resultado == -1;
            }
            catch (Exception ex)
            {
                //ExceptionLogger.LogExcepcion(ex, "Error no consulta ejecutando: " + pProcedureName);
                return false;
            }
            finally
            {
                if (_conexion != null && _conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                }
            }
        }

        #endregion

        #region Atributos

        private readonly SQLiteConnection  _conexion;

        #endregion
    }
}
