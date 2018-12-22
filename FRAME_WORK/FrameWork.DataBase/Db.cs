using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using FrameWork.Core;
using Dapper;
using System.Reflection;

namespace FrameWork.DataBase
{
	public class Db
	{
		public static string GetMainDbName()
		{
			string DbName = Localizer.MainDatabaseName;

			return DbName;
		}

		public static string GetConnectionString(string strDatabaseName = "")
		{
			strDatabaseName = strDatabaseName.Trim();

            string connectionString = "";

            connectionString = @"Data Source=" + Localizer.DatabaseServerName + ";Initial Catalog=" + (strDatabaseName == "" ? GetMainDbName() : strDatabaseName) + ";Persist Security Info=True;User ID=" + Localizer.DatabaseServerUserName + ";Password=" + Localizer.DatabaseServerPassword;
            
            return connectionString;
		}

		public static SqlConnection GetConnection(string strDatabaseName)
		{
			SqlConnection sqlConnection = new SqlConnection();

			sqlConnection.ConnectionString = GetConnectionString(strDatabaseName);

			return sqlConnection;
		}

		public static DataTable GetDataTable(string procedureName, List<SqlParameter> parameters, string strDatabaseName = "")
		{
			DataTable dataTable = new DataTable();
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(procedureName, connection);
				command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;

                for (int i = 0; i < parameters.Count; i++)
				{
					command.Parameters.Add(new SqlParameter(parameters[i].ParameterName, parameters[i].DbType)).Value = parameters[i].Value;
				}

				connection.Open();
				SqlDataReader dataReader = command.ExecuteReader();

				dataTable.Load(dataReader);
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return dataTable;
		}

        public static void ExecuteProcedure(string procedureName, List<SqlParameter> parameters, string strDatabaseName = "")
		{
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(procedureName, connection);
				command.CommandType = CommandType.StoredProcedure;

				for (int i = 0; i < parameters.Count; i++)
				{
					command.Parameters.Add(new SqlParameter(parameters[i].ParameterName, parameters[i].DbType)).Value = parameters[i].Value;
				}

				connection.Open();
				SqlDataReader dataReader = command.ExecuteReader();
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}
		}

		public static DataTable GetDataTableFromQuery(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
		{
			DataTable dataTable = new DataTable();
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(strQuery, connection);
				
				connection.Open();
				SqlDataReader dataReader = command.ExecuteReader();

				dataTable.Load(dataReader);
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return dataTable;
		}

		public static int ExecuteDeleteQuery(string strQuery, string strDatabaseName = "")
		{
			int rowsAffected = 0;
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(strQuery, connection);

				connection.Open();
				rowsAffected = command.ExecuteNonQuery();

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return rowsAffected;
		}

		public static int ExecuteQuery(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
		{
			int rowsAffected = 0;
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(strQuery, connection);

				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
				
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return rowsAffected;
		}

		public static int ExecuteReportQuery(string strQuery, string strDatabaseName = "")
		{
			int rowsAffected = 0;
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(strQuery, connection);

				connection.Open();
				rowsAffected = command.ExecuteNonQuery();

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return rowsAffected;
		}

		public static void ExecuteInsertQuery(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
		{
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlCommand command = new SqlCommand(strQuery, connection);

				connection.Open();
                command.ExecuteNonQuery();
            }
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}
		}

		public static DataSet GetDataSet(string procedureName, List<SqlParameter> parameters, string strDatabaseName = "")
		{
			DataSet dataSet = new DataSet();
			SqlConnection connection = null;

			try
			{
				connection = GetConnection(strDatabaseName);

				SqlDataAdapter command = new SqlDataAdapter(procedureName, connection);
				command.SelectCommand.CommandType = CommandType.StoredProcedure;

				for (int i = 0; i < parameters.Count; i++)
				{
					command.SelectCommand.Parameters.Add(new SqlParameter(parameters[i].ParameterName, parameters[i].DbType)).Value = parameters[i].Value;
				}

				connection.Open();
				command.Fill(dataSet);
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return dataSet;
		}
		
		public static bool SaveAmendment(int Id, string TableName, string DatabaseName = "", string ColumnName = "ID", bool IsChildQuery = false)
		{
			bool isDone = false;
			string strQuery = "";

			strQuery += System.Environment.NewLine + "SELECT        [NAME] AS COLUMN_NAME";
			strQuery += System.Environment.NewLine + "FROM          SYS.COLUMNS";
			strQuery += System.Environment.NewLine + "WHERE         OBJECT_ID = OBJECT_ID('" + TableName + "')";
			strQuery += System.Environment.NewLine + "AND           [NAME] != '" + ColumnName + "'";
			strQuery += System.Environment.NewLine + "AND           [NAME] != 'HAS_SYNCED'";

			DataTable dataTable = new DataTable();
			dataTable = GetDataTableFromQuery(strQuery, DatabaseName);

			if (dataTable.Rows.Count > 0)
			{
				string Columns = "ID";

				for (int IntI = 0; IntI < dataTable.Rows.Count; IntI++)
					Columns += "," + System.Environment.NewLine + dataTable.Rows[IntI][0];


				strQuery = "";
				strQuery += System.Environment.NewLine + "INSERT INTO " + TableName + "_AMENDMENT   (   " + Columns + ",";
				strQuery += System.Environment.NewLine + "                                              HAS_SYNCED";
				strQuery += System.Environment.NewLine + "                                          )   ";
				strQuery += System.Environment.NewLine + "                                  SELECT      " + Columns + ",";
				strQuery += System.Environment.NewLine + "                                              0";
				strQuery += System.Environment.NewLine + "                                  FROM        " + TableName;
				strQuery += System.Environment.NewLine + "                                  WHERE       " + ColumnName + " = " + Id;

				if (ExecuteQuery(strQuery, DatabaseName, IsChildQuery) == 1)
					isDone = true;
			}

			return isDone;
		}

		public static int DapperInsert(string query, object obj = null, string strDatabaseName = "", bool isChildQuery = false)
		{
			int newId = 0;
			SqlConnection connection = null;

			ValidateQuery(query, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				connection.Open();
				if (obj != null)
				{
					newId = connection.ExecuteScalar(query, obj).ToInteger();
				}
				else
				{
					newId = connection.ExecuteScalar(query).ToInteger();
				}

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return newId;
		}



		public static int DapperUpdate(string query, object obj = null, string strDatabaseName = "", bool isChildQuery = false)
		{
			int newId = 0;
			SqlConnection connection = null;

			ValidateQuery(query, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				connection.Open();
				if (obj != null)
				{
					newId = connection.Execute(query, obj);
				}
				else
				{
					newId = connection.Execute(query);
				}

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return newId;
		}

		public static T DapperGet<T>(string strQuery, object obj, string strDatabaseName = "", bool isChildQuery = false)
		{
			T objData;
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);
				connection.Open();
				objData = (T)connection.Query<T>(strQuery, obj).SingleOrDefault();                

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return objData;
		}

        public static List<T> DapperGetList<T>(string strQuery, object obj, string strDatabaseName = "", bool isChildQuery = false)
		{
			List<T> objData;
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				connection.Open();
				objData = (List<T>)connection.Query<T>(strQuery, obj).ToList();

			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return objData;
		}

        public static List<T> DapperGetListFromProcedure<T>(string strProcedure, object obj, string strDatabaseName = "")
        {
            List<T> objData;
            SqlConnection connection = null;

            try
            {
                connection = GetConnection(strDatabaseName);
                connection.Open();

                objData = (List<T>)connection.Query<T>(strProcedure, obj, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            finally
            {
                connection.Close();
            }

            return objData;
        }

        public static T DapperGet<T>(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
		{
			T objData;
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);

				connection.Open();
				objData = (T)connection.Query<T>(strQuery).SingleOrDefault();                
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return objData;
		}

		public static List<T> DapperGetList<T>(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
		{
			List<T> objData;
			SqlConnection connection = null;

			ValidateQuery(strQuery, isChildQuery);

			try
			{
				connection = GetConnection(strDatabaseName);
				connection.Open();
				objData = (List<T>)connection.Query<T>(strQuery).ToList();                
			}
			catch (SqlException ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message, null);
			}
			finally
			{
				connection.Close();
			}

			return objData;
		}

		private static bool ValidateQuery(string strQuery, bool isChildQuery)
		{
			strQuery = strQuery.ToUpper().TrimEnd().TrimStart().Trim();

			//if (strQuery.IndexOf("DELETE") >= 0 && Localizer.CurrentUser.IsSeniorAdmin == false)
			//	throw new ApplicationException("Cannot execute 'DELETE' Statement");

			//if (strQuery.IndexOf("INSERT") >= 0)
			//	throw new ApplicationException("Cannot execute 'INSERT' Statement");

			//if (strQuery.IndexOf("UPDATE") >= 0)
			//	throw new ApplicationException("Cannot execute 'UPDATE' Statement");

			/*
			if (strQuery.IndexOf("INSERT") >= 0 && strQuery.IndexOf("HAS_SYNCED") < 0)
				throw new ApplicationException("'HAS_SYNCED' is missing in 'INSERT STATEMENT'");

			if (strQuery.IndexOf("UPDATE") >= 0 && strQuery.IndexOf("HAS_SYNCED = 0") < 0)
				throw new ApplicationException("'HAS_SYNCED' is missing in 'UPDATE STATEMENT'");

			if (!isChildQuery)
			{
				if (strQuery.IndexOf("INSERT") >= 0 && strQuery.IndexOf("SUPERADMIN_USERNAME") < 0)
					throw new ApplicationException("'SuperAdmin UserName' is missing in 'INSERT STATEMENT'");

				if (strQuery.IndexOf("UPDATE") >= 0 && strQuery.IndexOf("SUPERADMIN_USERNAME") < 0)
					throw new ApplicationException("'SuperAdmin UserName' is missing in 'UPDATE STATEMENT'");

				if (strQuery.IndexOf("UPDATE") >= 0 && strQuery.IndexOf("MODIFIED_BY") < 0)
					throw new ApplicationException("'Modified By' is missing in 'UPDATE STATEMENT'");

				if (strQuery.IndexOf("UPDATE") >= 0 && strQuery.IndexOf("MODIFY_DATE") < 0)
					throw new ApplicationException("'Modify Date' is missing in 'UPDATE STATEMENT'");

				if (strQuery.IndexOf("INSERT") >= 0 && strQuery.IndexOf("CREATED_BY") < 0)
					throw new ApplicationException("'Created By' is missing in 'INSERT STATEMENT'");

				if (strQuery.IndexOf("INSERT") >= 0 && strQuery.IndexOf("CREATE_DATE") < 0)
					throw new ApplicationException("'Create Date' is missing in 'INSERT STATEMENT'");
			}
			*/

			return true;
		}

		public static DataTable GetUserDefinedDataTable()
		{
			DataTable dataTable = new DataTable();

			dataTable.Columns.Add("DATE1", typeof(DateTime));
			dataTable.Columns.Add("DATE2", typeof(DateTime));
			dataTable.Columns.Add("DATE3", typeof(DateTime));
			dataTable.Columns.Add("DATE4", typeof(DateTime));
			dataTable.Columns.Add("DATE5", typeof(DateTime));
			dataTable.Columns.Add("DATE6", typeof(DateTime));
			dataTable.Columns.Add("DATE7", typeof(DateTime));
			dataTable.Columns.Add("DATE8", typeof(DateTime));
			dataTable.Columns.Add("DATE9", typeof(DateTime));
			dataTable.Columns.Add("DATE10", typeof(DateTime));
			dataTable.Columns.Add("DESC1", typeof(string));
			dataTable.Columns.Add("DESC2", typeof(string));
			dataTable.Columns.Add("DESC3", typeof(string));
			dataTable.Columns.Add("DESC4", typeof(string));
			dataTable.Columns.Add("DESC5", typeof(string));
			dataTable.Columns.Add("DESC6", typeof(string));
			dataTable.Columns.Add("DESC7", typeof(string));
			dataTable.Columns.Add("DESC8", typeof(string));
			dataTable.Columns.Add("DESC9", typeof(string));
			dataTable.Columns.Add("DESC10", typeof(string));
			dataTable.Columns.Add("DESC11", typeof(string));
			dataTable.Columns.Add("DESC12", typeof(string));
			dataTable.Columns.Add("DESC13", typeof(string));
			dataTable.Columns.Add("DESC14", typeof(string));
			dataTable.Columns.Add("DESC15", typeof(string));
			dataTable.Columns.Add("DESC16", typeof(string));
			dataTable.Columns.Add("DESC17", typeof(string));
			dataTable.Columns.Add("DESC18", typeof(string));
			dataTable.Columns.Add("DESC19", typeof(string));
			dataTable.Columns.Add("DESC20", typeof(string));
			dataTable.Columns.Add("AMOUNT1", typeof(decimal));
			dataTable.Columns.Add("AMOUNT2", typeof(decimal));
			dataTable.Columns.Add("AMOUNT3", typeof(decimal));
			dataTable.Columns.Add("AMOUNT4", typeof(decimal));
			dataTable.Columns.Add("AMOUNT5", typeof(decimal));
			dataTable.Columns.Add("AMOUNT6", typeof(decimal));
			dataTable.Columns.Add("AMOUNT7", typeof(decimal));
			dataTable.Columns.Add("AMOUNT8", typeof(decimal));
			dataTable.Columns.Add("AMOUNT9", typeof(decimal));
			dataTable.Columns.Add("AMOUNT10", typeof(decimal));
			dataTable.Columns.Add("AMOUNT11", typeof(decimal));
			dataTable.Columns.Add("AMOUNT12", typeof(decimal));
			dataTable.Columns.Add("AMOUNT13", typeof(decimal));
			dataTable.Columns.Add("AMOUNT14", typeof(decimal));
			dataTable.Columns.Add("AMOUNT15", typeof(decimal));
			dataTable.Columns.Add("AMOUNT16", typeof(decimal));
			dataTable.Columns.Add("AMOUNT17", typeof(decimal));
			dataTable.Columns.Add("AMOUNT18", typeof(decimal));
			dataTable.Columns.Add("AMOUNT19", typeof(decimal));
			dataTable.Columns.Add("AMOUNT20", typeof(decimal));
			dataTable.Columns.Add("BIT1", typeof(bool));
			dataTable.Columns.Add("BIT2", typeof(bool));
			dataTable.Columns.Add("BIT3", typeof(bool));
			dataTable.Columns.Add("BIT4", typeof(bool));
			dataTable.Columns.Add("BIT5", typeof(bool));
			dataTable.Columns.Add("BIT6", typeof(bool));
			dataTable.Columns.Add("BIT7", typeof(bool));
			dataTable.Columns.Add("BIT8", typeof(bool));
			dataTable.Columns.Add("BIT9", typeof(bool));
			dataTable.Columns.Add("BIT10", typeof(bool));

			return dataTable;
		}

        public static DataTable GetUserDefinedReportTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("DATE1", typeof(DateTime));
            dataTable.Columns.Add("DATE2", typeof(DateTime));
            dataTable.Columns.Add("DATE3", typeof(DateTime));
            dataTable.Columns.Add("DATE4", typeof(DateTime));
            dataTable.Columns.Add("DATE5", typeof(DateTime));
            dataTable.Columns.Add("DATE6", typeof(DateTime));
            dataTable.Columns.Add("DATE7", typeof(DateTime));
            dataTable.Columns.Add("DATE8", typeof(DateTime));
            dataTable.Columns.Add("DATE9", typeof(DateTime));
            dataTable.Columns.Add("DATE10", typeof(DateTime));
            dataTable.Columns.Add("DESC1", typeof(string));
            dataTable.Columns.Add("DESC2", typeof(string));
            dataTable.Columns.Add("DESC3", typeof(string));
            dataTable.Columns.Add("DESC4", typeof(string));
            dataTable.Columns.Add("DESC5", typeof(string));
            dataTable.Columns.Add("DESC6", typeof(string));
            dataTable.Columns.Add("DESC7", typeof(string));
            dataTable.Columns.Add("DESC8", typeof(string));
            dataTable.Columns.Add("DESC9", typeof(string));
            dataTable.Columns.Add("DESC10", typeof(string));
            dataTable.Columns.Add("DESC11", typeof(string));
            dataTable.Columns.Add("DESC12", typeof(string));
            dataTable.Columns.Add("DESC13", typeof(string));
            dataTable.Columns.Add("DESC14", typeof(string));
            dataTable.Columns.Add("DESC15", typeof(string));
            dataTable.Columns.Add("DESC16", typeof(string));
            dataTable.Columns.Add("DESC17", typeof(string));
            dataTable.Columns.Add("DESC18", typeof(string));
            dataTable.Columns.Add("DESC19", typeof(string));
            dataTable.Columns.Add("DESC20", typeof(string));
            dataTable.Columns.Add("AMOUNT1", typeof(decimal));
            dataTable.Columns.Add("AMOUNT2", typeof(decimal));
            dataTable.Columns.Add("AMOUNT3", typeof(decimal));
            dataTable.Columns.Add("AMOUNT4", typeof(decimal));
            dataTable.Columns.Add("AMOUNT5", typeof(decimal));
            dataTable.Columns.Add("AMOUNT6", typeof(decimal));
            dataTable.Columns.Add("AMOUNT7", typeof(decimal));
            dataTable.Columns.Add("AMOUNT8", typeof(decimal));
            dataTable.Columns.Add("AMOUNT9", typeof(decimal));
            dataTable.Columns.Add("AMOUNT10", typeof(decimal));
            dataTable.Columns.Add("AMOUNT11", typeof(decimal));
            dataTable.Columns.Add("AMOUNT12", typeof(decimal));
            dataTable.Columns.Add("AMOUNT13", typeof(decimal));
            dataTable.Columns.Add("AMOUNT14", typeof(decimal));
            dataTable.Columns.Add("AMOUNT15", typeof(decimal));
            dataTable.Columns.Add("AMOUNT16", typeof(decimal));
            dataTable.Columns.Add("AMOUNT17", typeof(decimal));
            dataTable.Columns.Add("AMOUNT18", typeof(decimal));
            dataTable.Columns.Add("AMOUNT19", typeof(decimal));
            dataTable.Columns.Add("AMOUNT20", typeof(decimal));
            dataTable.Columns.Add("BIT1", typeof(bool));
            dataTable.Columns.Add("BIT2", typeof(bool));
            dataTable.Columns.Add("BIT3", typeof(bool));
            dataTable.Columns.Add("BIT4", typeof(bool));
            dataTable.Columns.Add("BIT5", typeof(bool));
            dataTable.Columns.Add("BIT6", typeof(bool));
            dataTable.Columns.Add("BIT7", typeof(bool));
            dataTable.Columns.Add("BIT8", typeof(bool));
            dataTable.Columns.Add("BIT9", typeof(bool));
            dataTable.Columns.Add("BIT10", typeof(bool));

            return dataTable;
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private static void ValidateUserSession()
        {
            string strQuery = "";

            strQuery = strQuery + System.Environment.NewLine + "SELECT      1";
            strQuery = strQuery + System.Environment.NewLine + "FROM        USER_LOGIN_HISTORY";
            strQuery = strQuery + System.Environment.NewLine + "WHERE       ID = " + Localizer.CurrentUser.LoginHistoryId;
            strQuery = strQuery + System.Environment.NewLine + "AND         ISNULL(LOGOUT_DATE, 'Jan 01, 1900') = 'Jan 01, 1900'";

            DataTable dt = GetDataTableFromQuery(strQuery);

            if (dt.Rows.Count <= 0)
                throw new ApplicationException("Sorry! Your session has been expired. Please Login again.");
        }

        public static DataSet GetDataSetFromQuery(string strQuery, string strDatabaseName = "", bool isChildQuery = false)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = null;

            ValidateQuery(strQuery, isChildQuery);

            try
            {
                connection = GetConnection(strDatabaseName);

                SqlDataAdapter command = new SqlDataAdapter(strQuery, connection);

                connection.Open();
                command.Fill(dataSet);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            finally
            {
                connection.Close();
            }

            return dataSet;
        }

        public static DataSet GetDataSet(string query, List<SqlParameter> parameters, CommandType commandType, string strDatabaseName = "")
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = null;

            try
            {
                connection = GetConnection(strDatabaseName);

                SqlDataAdapter command = new SqlDataAdapter(query, connection);
                command.SelectCommand.CommandType = commandType;

                for (int i = 0; i < parameters.Count; i++)
                {
                    command.SelectCommand.Parameters.Add(new SqlParameter(parameters[i].ParameterName, parameters[i].DbType)).Value = parameters[i].Value;
                }

                connection.Open();
                command.Fill(dataSet);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, null);
            }
            finally
            {
                connection.Close();
            }

            return dataSet;
        }
    }
}
