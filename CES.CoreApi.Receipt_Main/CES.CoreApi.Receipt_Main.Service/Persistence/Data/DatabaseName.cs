using CES.Data.Sql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CES.CoreApi.Shared.Persistence.Data
{
	/// <summary>
	///This is to register connection strings.
	///If you don’t use main and  ReadOnlyTransactional please remove them or replace them
	/// </summary>
	public class DatabaseName
	{
        public static readonly string Main = "main";
        public static readonly string ReadOnlyTransactional = "readonlyTransactional";


        internal static SqlMapper CreateSqlMapper()
        {
            var sqlMapper = new SqlMapper();

            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                sqlMapper.AddConnectionSetting(item.Name, item.ConnectionString, 30);
            }
            return sqlMapper;
        }
        internal static PersistenceSqlMapper CreatePersistenceSqlMapper()
        {
            var sqlMapper = new SqlMapper();

            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                sqlMapper.AddConnectionSetting(item.Name, item.ConnectionString, 30);
            }
            return new PersistenceSqlMapper(sqlMapper);
        }


        internal static ConnectionStringSettings GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];

        }

        #region LogSPCall
        /// <summary>
        /// Log the call to the SP with input values.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="cmd"></param>
        public static string LogSPCall(string spName, ICollection<SqlParameter> paramCollect)
        {
            var log = string.Empty;
            try
            {
                //if (ConfigSettings.LogStoredProcedureCalls())
                if (true)
                {
                    //Write out a SQL exec statement for use in testing:
                    string fullExec = "";
                    string sqlCmd = "exec " + spName + " ";
                    string declareStmt = "";
                    string paramList = "";

                    IEnumerator e = paramCollect.GetEnumerator();
                    while (e.MoveNext())
                    {
                        SqlParameter p = (SqlParameter)e.Current;
                        if (p.Direction == ParameterDirection.Output)
                        {
                            if (p.DbType == DbType.Int32)
                            {
                                declareStmt += "declare " + p + " int|";
                            }
                            else if (p.DbType == DbType.String || p.DbType == DbType.AnsiString)
                            {
                                declareStmt += "declare " + p + " varchar(100)|";
                            }
                            else if (p.DbType == DbType.DateTime)
                            {
                                declareStmt += "declare " + p + " datetime|";
                            }
                            else if (p.DbType == DbType.Boolean)
                            {
                                declareStmt += "declare " + p + " bit|";
                            }
                            else
                            {
                                declareStmt += "declare " + p + " " + p.DbType + "|";
                            }
                        }
                    }
                    //Write out the parameter list:
                    e = paramCollect.GetEnumerator();
                    while (e.MoveNext())
                    {
                        SqlParameter p = (SqlParameter)e.Current;
                        if (p.Direction == ParameterDirection.Output)
                        {
                            paramList += p + " = " + p + " output,";
                        }
                        else if (p.DbType == DbType.String
                            || p.DbType == DbType.AnsiString
                            || p.DbType == DbType.DateTime
                            || p.DbType == DbType.Boolean)
                        {
                            paramList += p + " = '" + p.Value + "',";
                        }
                        else
                        {
                            paramList += p + " = " + (p.Value ?? "null") + ",";
                        }
                    }
                    //Compose the statement:
                    fullExec = declareStmt + sqlCmd + paramList.Remove(paramList.LastIndexOf(","), 1);
                    log = fullExec.Replace("|", Environment.NewLine);

                }
            }
            catch (Exception e)
            {
                //Don't want to stop the application process if the SP call cannot be parsed, just log it:
                log = "Cannot parse and write out SP call logging." + e.Message;
                Logging.Log.Debug(log);
                return log;
            }

            //Logging.Log.Info(log);
            return log;
        }


        #endregion

    }
}