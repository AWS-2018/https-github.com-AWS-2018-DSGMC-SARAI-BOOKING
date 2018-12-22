using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.ComponentModel;

namespace FrameWork.DataBase
{
    public class ObjectMapper<T> where T : new()
    {
        protected static T Map(DataRow rows, DataColumnCollection columns)
        {
            T instance = new T();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    if (property.Name == columns[i].ColumnName)
                    {
                        var type = (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType);

                        property.SetValue(instance, Convert.ChangeType(rows[i], type), null);
                        break;
                    }
                }
            }

            return instance;
        }

        public static List<T> MapDataToListObject(DataTable dataTable)
        {
            List<T> collection = new List<T>();

            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                collection.Add(Map(dataTable.Rows[i], dataTable.Columns));
            }

            return collection;

        }

        public static T MapDataToObject(DataTable dataTable)
        {
            T objData = new T();

            objData = Map(dataTable.Rows[0], dataTable.Columns);

            return objData;

        }

        public static DataTable CreateTable<TT>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            
            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                tbl.Columns.Add(prop.Name, prop.PropertyType);
            }
            return tbl;
        }
 

        public static DataTable ConvertToDataTable(IList<T> lst)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            foreach (T item in lst)
            {
                DataRow row = tbl.NewRow( );
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }

            return tbl;
        }
    }
}
