using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data.Database
{
    class MySqlDatabaseConnection : IDisposable
    {
        #region "Fields"

        private MySqlConnection connection;

        #endregion

        #region "Constructors"

        public MySqlDatabaseConnection(string host, string database, string username, string password, int port = 3306)
        {
            connection = new MySqlConnection("Server=" + host + ";Port=" + port + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + "; ");
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private MySqlDbType ConvertType(Type obj_type)
        {
            if (obj_type == typeof(double))
                return MySqlDbType.Double;

            else if (obj_type == typeof(long))
                return MySqlDbType.Int64;
            else if (obj_type == typeof(int))
                return MySqlDbType.Int32;
            else if (obj_type == typeof(short))
                return MySqlDbType.Int32;
            else if (obj_type == typeof(ulong))
                return MySqlDbType.Int64;
            else if (obj_type == typeof(uint))
                return MySqlDbType.Int64;
            else if (obj_type == typeof(ushort))
                return MySqlDbType.Int32;

            else if (obj_type == typeof(bool))
                return MySqlDbType.Bit;
            else if (obj_type == typeof(string))
                return MySqlDbType.LongText;
            else if (obj_type == typeof(DateTime))
                return MySqlDbType.DateTime;
            else if (obj_type == typeof(byte[]))
                return MySqlDbType.LongBlob;
            else
                return MySqlDbType.LongBlob;
        }

        private void InsertSqlParameter(MySqlCommand command, object value)
        {
            int index = command.CommandText.IndexOf("?");
            command.CommandText = command.CommandText.Remove(index, 1).Insert(index, "@" + index);

            MySqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@" + index;
            parameter.MySqlDbType = ConvertType(value.GetType());
            parameter.Value = value;

            command.Parameters.Add(parameter);
        }

        public void ExecuteNonQuery(string instruction, params object[] values)
        {
            MySqlCommand command = connection.CreateCommand();
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
            MySqlCommand command = connection.CreateCommand();
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

        public void Dispose()
        {
            Close();

            connection = null;
        }

        #endregion

        #region "Static Methods"

        public static MySqlDatabaseConnection GetConnection()
        {
            return new MySqlDatabaseConnection("kdullens.com", "mistkingdoms", "mist", "e8ECRUS9xcBDJdqm&");
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
