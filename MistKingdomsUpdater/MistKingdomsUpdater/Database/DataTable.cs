using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Database
{
    public class DataTable
    {

        #region "Attributes"

        private List<string> fields;
        private List<object[]> data;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor for a DataTable object
        /// </summary>
        /// <param name="reader">A DataReader object containing the results of a database query. Cannot be null</param>
        public DataTable(DbDataReader reader)
        {
            // Check the parameters
            if (reader == null)
                throw new ArgumentNullException("Parameter reader cannot be null or empty", "reader");

            // Initialize attributes
            this.fields = new List<string>();
            this.data = new List<object[]>();

            // Set the column names
            for (int i = 0; i < reader.FieldCount; i++)
            {
                fields.Add(reader.GetName(i));
            }

            // Read the data from the rows
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                for (int i = 0; i < fields.Count; i++)
                {
                    values[i] = reader.GetValue(reader.GetOrdinal(fields[i]));
                }
                // Add the data to the List<>
                data.Add(values);
            }

            reader.Dispose();
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Gets the amount of rows in the result set
        /// </summary>
        /// <returns>Returns an integer with the amount of rows in the result set</returns>
        public int GetRowCount()
        {
            return this.data.Count;
        }

        /// <summary>
        /// Gets a row from the DataTable
        /// </summary>
        /// <param name="row">The index of the row to fetch. Must be bigger or equal to 0 and smaller than the amount of rows</param>
        /// <returns>An array of objects containing the data from the specified rows</returns>
        public object[] GetRow(int row)
        {
            // Check the parameters
            if (row < 0 || row > data.Count - 1)
                throw new IndexOutOfRangeException("Parameter row must be bigger or equal to 0 and smaller than the amount of rows");

            return data[row];
        }

        /// <summary>
        /// Gets a DataRow object from a specified row index
        /// </summary>
        /// <param name="row">The index number of the row to fetch. Must be bigger or equal to 0 and smaller than the amount of rows</param>
        /// <returns>Returns a DataRow object containing the data from the row</returns>
        public DataRow GetDataRow(int row)
        {
            // Check the parameters
            if (row < 0 || row > data.Count - 1)
                throw new IndexOutOfRangeException("Parameter row must be bigger or equal to 0 and smaller than the amount of rows");

            return new DataRow(fields, data[row]);
        }

        public object[] GetDataFromColumn(string column)
        {
            int index = fields.IndexOf(column);
            return data.Select(x => x[index]).ToArray();
        }

        /// <summary>
        /// Gets the data from a single column of the data result set
        /// </summary>
        /// <param name="row">The index of the row to fetch data from. Must be bigger or equal to 0 and smaller than the amount of rows</param>
        /// <param name="column">The name of the column to fetch data from. Cannot be null or empty</param>
        /// <returns>Returns a single object with the data of the given row and column</returns>
        public object GetDataFromRow(int row, string column)
        {
            // Check the parameters
            if (row < 0 || row > data.Count - 1)
                throw new IndexOutOfRangeException("Parameter row must be bigger or equal to 0 and smaller than the amount of rows");
            if (column == null || column == "")
                throw new ArgumentException("Parameter column cannot be null or empty");

            // Loop through the columns and fetch the correct value
            int index = fields.IndexOf(column);
            return data[row][index];
        }

        /// <summary>
        /// Gets the data from a single column of the data result set
        /// </summary>
        /// <param name="row">The index of the row to fetch data from. Must be bigger or equal to 0 and smaller than the amount of rows</param>
        /// <param name="columns">An array of strings containing multiple names of columns to fetch. Cannot be null or empty</param>
        /// <returns>Returns an array with the data of the given columns from the given row</returns>
        public object[] GetDataFromRow(int row, string[] columns)
        {
            // Check the parameters
            if (row < 0 || row > data.Count - 1)
                throw new IndexOutOfRangeException("Parameter row must be bigger or equal to 0 and smaller than the amount of rows");
            if (columns == null || columns.Length == 0)
                throw new ArgumentException("Parameter columns cannot be null or empty", "columns");

            // Loop through the columns and fetch a value for each column
            object[] values = new object[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                int index = fields.IndexOf(columns[i]);
                values[i] = data[row][index];
            }

            // Return the found values
            return values;
        }

        /// <summary>
        /// Gets an enumerator to loop through the row in the DataTable
        /// </summary>
        /// <returns>Return an enumerator containing DataRow objects containing the information of the rows in the current data set</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            // Loop through the data and create DataRows from them
            for (int i = 0; i < data.Count; i++)
            {
                yield return new DataRow(fields, data[i]);
            }
        }

        #endregion

        #region "Static Methods"
        #endregion

        #region "Inherited Methods"
        #endregion

        public static implicit operator DataTable(SqlDataReader reader)
        {
            return new DataTable(reader);
        }

        public static implicit operator DataTable(MySqlDataReader reader)
        {
            return new DataTable(reader);
        }
    }
}
