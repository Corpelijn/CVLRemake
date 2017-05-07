using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data.Database
{
    class GenericDatabaseConnection
    {
        #region "Fields"

        private SqlConnection connection;

        #endregion

        #region "Constructors"

        public GenericDatabaseConnection(string host, string database, string username, string password, int port = 3306)
        {
            connection = new SqlConnection("Server=" + host + ";Port=" + port + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + "; ");
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private SqlDbType ConvertType(Type obj_type)
        {
            if (obj_type == typeof(double))
                return SqlDbType.Decimal;

            else if (obj_type == typeof(long))
                return SqlDbType.Int;
            else if (obj_type == typeof(int))
                return SqlDbType.Int;
            else if (obj_type == typeof(short))
                return SqlDbType.Int;
            else if (obj_type == typeof(ulong))
                return SqlDbType.Int;
            else if (obj_type == typeof(uint))
                return SqlDbType.Int;
            else if (obj_type == typeof(ushort))
                return SqlDbType.Int;

            else if (obj_type == typeof(bool))
                return SqlDbType.Bit;
            else if (obj_type == typeof(string))
                return SqlDbType.Text;
            else if (obj_type == typeof(DateTime))
                return SqlDbType.DateTime;
            else if (obj_type == typeof(byte[]))
                return SqlDbType.Binary;
            else
                return SqlDbType.Binary;
        }

        private void InsertSqlParameter(SqlCommand command, object value)
        {
            int index = command.CommandText.IndexOf("?");
            command.CommandText = command.CommandText.Remove(index, 1).Insert(index, "@" + index);

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@" + index;
            parameter.SqlDbType = ConvertType(value.GetType());

            command.Parameters.Add(parameter);
        }

        public void ExecuteNonQuery(string instruction, params object[] values)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = instruction;
            foreach (object obj in values)
            {
                InsertSqlParameter(command, obj);
            }

            command.ExecuteNonQuery();

            command.Dispose();
        }

        public DataTable ExecuteQuery(string query, params object[] values)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            foreach (object obj in values)
            {
                InsertSqlParameter(command, obj);
            }

            DataTable results = command.ExecuteReader();

            command.Dispose();

            return results;
        }

        public void Close()
        {
            connection.Close();
        }

        public void Open()
        {
            connection.Open();
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
