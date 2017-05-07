using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Database
{
    public class DataRow
    {

        #region "Attributes"

        private List<string> fields;
        private object[] data;

        #endregion

        #region "Constructors"

        /// <summary>
        /// The constructor of a DataRow
        /// </summary>
        /// <param name="fields">List of strings objects that contain the names of the columns of the database. Cannot be null or empty</param>
        /// <param name="data">An array of objects that contain the data of the current row. Cannot be null or empty and has to contain the same amount of items as the fields parameter</param>
        public DataRow(List<string> fields, object[] data)
        {
            this.fields = fields;
            this.data = data;
        }

        #endregion

        #region "Properties"

        public object this[string s]
        {
            get { return GetObject(s); }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Gets an data object from the row by the given column name
        /// </summary>
        /// <param name="column">The name of the column to retrieve the value from. Cannot be null or empty</param>
        /// <returns>Returns an object with the data from the specified column</returns>
        public object GetObject(string column)
        {
            // Check the parameters
            if (column == null || column == "")
                throw new ArgumentException("Parameter column cannot be null or empty", "column");

            return data[fields.IndexOf(column)];
        }

        /// <summary>
        /// Gets a field from the row by the index of the column
        /// </summary>
        /// <param name="index">The index of the column to find</param>
        /// <returns>Returns the name of the field a the specified index</returns>
        public string GetField(int index)
        {
            return fields[index];
        }

        /// <summary>
        /// Gets the amount of fields in the row
        /// </summary>
        /// <returns>Returns an integer with the amount of columns</returns>
        public int GetFieldCount()
        {
            return fields.Count;
        }

        #endregion

        #region "Static Methods"
        #endregion

        #region "Inherited Methods"
        #endregion
    }
}
