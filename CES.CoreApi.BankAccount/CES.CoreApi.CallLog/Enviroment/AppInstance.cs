using System;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CES.Data.Sql;
using CES.CoreApi.CallLog.Db;
using CES.CoreApi.CallLog.Tools;

namespace CES.CoreApi.CallLog.Enviroment
{
    /// <summary>
    /// Provides some useful prooerties about the enviroment under which the application is running
    /// </summary>
    public class AppInstance
    {
        //@@2012-01-17 lb Added as per John Ashton's request
        //@@201-04-24 lb SCR# 1457311 Added properties to get the name of the machine and its domain where it is attached (if any)

        private static object _appEnvSyncObj = new object();
        private static AppInstance _appEnv = null;
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        #region private members

        private string _machineName;
        private string _machineDomain; //If any
        private string _machineFullName;

        private static bool _initializedAssemblyProps = false;
        private static object _initializedAssemblyPropsSyncObj = new object();
        private static string _appName = "";
        private static string _appTitle = "";
        private static string _appVersion = "";
        private static string _appPath = "";

        private static bool _useDirectoryBase = false; /* Sometimes it is not possible to determine the Entry Assembly correct location as it is
                                                        * the case for web services hosted by IIS or WAS. In such scenarios the first static call
                                                        * to this class must server to determine the current domain base directory. It must be done even before
                                                        * the initialize() method
                                                        * */

        private static Assembly _entryAssembly = null; /* Sometimes it is not possible to determine the Entry Assembly as it is
                                                        * the case for web services hosted by IIS or WAS. In such scenarios the first static call
                                                        * to this class must server to determine an entry assembly. It must be done even before
                                                        * the initialize() method
                                                        * */

        #endregion

        /// <summary>
        /// Establishes an initial assembly for the confirguration and defines if using the current domain's base directory to get the application's path instead of the location of the entry assembly. Intended for IIS/WAS hosted applications
        /// </summary>
        public static void Configure(Assembly asm, bool useDirectoryBase)
        {
            if (!string.IsNullOrWhiteSpace(_appName))
                return; //This method must be called only once

            _entryAssembly = asm;
            _useDirectoryBase = true;
        }

        private AppInstance()
        {
            try{ _machineName = System.Environment.MachineName; }
            catch{ _machineName = ""; }

            try { _machineDomain = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().Name; }
            catch { _machineDomain = ""; }

            _machineFullName = "";

            if (!string.IsNullOrWhiteSpace(_machineName))
            {
                if (string.IsNullOrWhiteSpace(_machineDomain))
                    _machineFullName = _machineName;
                else
                    _machineFullName = string.Format("{0}.{1}", _machineName, _machineDomain);
            }

            //Set the IP address
            setIPAddress();

            //Set the instance ID
            setAppInstanceID();
        }

        /// <summary>
        /// Retrieves an instance id for the application from database
        /// </summary>
        //private void setAppInstanceID()
        //{
        //    //@@2008-08-20 lb SCR# 541111. Created
        //    //@@2011-01-24 lb SCR# 953511 Added logic to close the previous session if it exists
        //    try
        //    {
        //        IDbOps dbOps = DbOps.GetInstance();

        //        int auxInstance = 0, retVal = 0;                

        //        SqlParameter retValP, retMsgP, instP;

        //        dbOps.Exec(new Foundation.Data.Models.DatabaseRequestBase()
        //        {
        //            DatabaseType = Common.Enumerations.DatabaseType.Main,
        //            ProcedureName = "sys_sp_App_Instance_Register",
        //            Parameters = new SqlParameter[] {
        //                    new SqlParameter("@fAppID", SqlDbType.Int) { Value = AppID },
        //                    new SqlParameter("@fApp_QueueID", SqlDbType.Int) { Value = AppQueue },
        //                    new SqlParameter("@fApp_Version", SqlDbType.VarChar, 50) { Value = ApplicationVersion },
        //                    new SqlParameter("@fApp_Description", SqlDbType.VarChar, 50) { Value = ApplicationTitle },
        //                    new SqlParameter("@fServer_Name", SqlDbType.VarChar, 50) { Value = MachineName },
        //                    new SqlParameter("@fServer_IPAddress", SqlDbType.VarChar, 50) { Value = (IPAddress ?? "").DBNullIfEmpty() },
        //                    instP = new SqlParameter("@fApp_InstanceID", SqlDbType.Int) { Value = 0, Direction = ParameterDirection.InputOutput },
        //                    retValP = new SqlParameter("@lRetVal", SqlDbType.Int) { Value = 0, Direction = ParameterDirection.InputOutput },
        //                    retMsgP = new SqlParameter("@sRetMsg", SqlDbType.VarChar, 200) { Value = DBNull.Value, Direction = ParameterDirection.InputOutput }
        //                }
        //        });

        //        auxInstance = (int)instP.Value;
        //        retVal = (int)retValP.Value;

        //        if (retVal != 1)
        //            throw new Exception(string.Format("Incorrect value for @lRetVal: {0}", retVal));
        //        else
        //            this.InstanceID = auxInstance;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception ("Unexpected error when trying to obtain an instance ID for the application: " + ex.Message);
        //    }
        //}

        private void setAppInstanceID()
        {
            //@@2008-08-20 lb SCR# 541111. Created
            //@@2011-01-24 lb SCR# 953511 Added logic to close the previous session if it exists
            try
            {
                int auxInstance = 0, retVal = 0;

                SqlParameter retValP, retMsgP, instP;
                using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, "sys_sp_App_Instance_Register"))
                {
                    sql.AddParam("@fAppID", AppID);
                    sql.AddParam("@fApp_QueueID", AppQueue);
                    sql.AddParam("@fApp_Version", ApplicationVersion);
                    sql.AddParam("@fApp_Description", ApplicationTitle);
                    sql.AddParam("@fServer_Name", MachineName);
                    sql.AddParam("@fServer_IPAddress", (IPAddress ?? "").DBNullIfEmpty());
                    instP = sql.AddOutputParam("@fApp_InstanceID", SqlDbType.Int);
                    retValP = sql.AddOutputParam("@lRetVal", SqlDbType.Int);
                    retMsgP = sql.AddOutputParam("@sRetMsg", SqlDbType.VarChar, 200);
                   
                    sql.Execute();

                    auxInstance = instP.GetSafeValue<int>();
                    retVal = retValP.GetSafeValue<int>(); ;

                    if (retVal != 1)
                        throw new Exception(string.Format("Incorrect value for @lRetVal: {0}", retVal));
                    else
                        this.InstanceID = auxInstance;                 
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error when trying to obtain an instance ID for the application: " + ex.Message);
            }
        }
        private void setIPAddress()
        {
            System.Net.IPAddress[] ipAddresses = System.Net.Dns.GetHostAddresses(Environment.MachineName);

            if (ipAddresses != null && ipAddresses.Length != 0)
            {
                Regex reg = new Regex("^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

                string addressStr;
                foreach (var address in ipAddresses)
                {
                    if (address == null) continue;

                    if (string.IsNullOrWhiteSpace(addressStr = address.ToString())) continue;

                    if (reg.IsMatch(addressStr))
                    {
                        this.IPAddress = addressStr;
                    }
                }
            }
        }

        public static AppInstance Current
        {
            get {
                lock(_appEnvSyncObj)
                {
                    if (_appEnv == null)
                        _appEnv = new AppInstance();

                    return _appEnv;
                }                
            }
        }


        /// <summary>
        /// Gets the application ID
        /// </summary>
        public int AppID
        {
            get
            {
                string auxText;
                int appID;

                if ((auxText = ConfigurationManager.AppSettings["ApplicationID"]) != null &&  int.TryParse(auxText, out appID))
                    return appID;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the application's queue
        /// </summary>
        public int AppQueue
        {
            get
            {
                string auxText;
                int queue;

                if ((auxText = ConfigurationManager.AppSettings["Queue"]) != null && int.TryParse(auxText, out queue))
                    return queue;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the name of the machine where the application is actually executing
        /// </summary>
        public string MachineName
        {
            get { return _machineName; }
        }

        /// <summary>
        /// Gets the name of the machine's domain
        /// </summary>
        public string MachineDomainName
        {
            get { return _machineDomain; }
        }

        /// <summary>
        /// Gets the full name of the machine (including the domain, if any)
        /// </summary>
        public string MachineFullName
        {
            get { return _machineFullName; }
        }

        private void InitializedAssemblyProps()
        {
            //@@2010-08-30 lb SCR#  788511 Created

            lock(_initializedAssemblyPropsSyncObj)
            {
                if (!_initializedAssemblyProps)
                {
                    Assembly asm = _entryAssembly;

                    if (asm == null)
                        asm = Assembly.GetEntryAssembly();

                    if (asm == null)
                        throw new Exception("Unable to determine the entry assembly. if hosting application is not .NET, call explicitly the AppInstance.Configure static function");

                    AssemblyName asname = new AssemblyName(asm.FullName);

                    _appName = asname.Name;
                    _appVersion = asname.Version.ToString();

                    object[] atts = asm.GetCustomAttributes(
                        typeof(AssemblyTitleAttribute), false);

                    if (atts != null && atts.Length > 0)
                        _appTitle = ((AssemblyTitleAttribute)atts[0]).Title;

                    if (!_useDirectoryBase)
                    {
                        FileInfo fileInfo = new FileInfo(asm.Location);
                        _appPath = fileInfo.DirectoryName;
                    }
                    else
                        _appPath = AppDomain.CurrentDomain.BaseDirectory;

                    _entryAssembly = null; //this is no longer necessary

                    _initializedAssemblyProps = true;
                }
            }
        }

        /// <summary>
        /// Retrieves application name
        /// </summary>
        public string ApplicationName
        {
            //@@2010-08-30 lb SCR#  788511 Created

            get
            {
                InitializedAssemblyProps();
                return _appName;
            }
        }

        /// <summary>
        /// Retrieves application name
        /// </summary>
        public string ApplicationTitle
        {
            //@@2010-08-30 lb SCR#  788511 Created
            get
            {
                InitializedAssemblyProps();
                return _appTitle;
            }
        }

        /// <summary>
        /// Retrieves application name
        /// </summary>
        public string ApplicationVersion
        {
            //@@2010-08-30 lb SCR#  788511 Created
            get
            {
                InitializedAssemblyProps();
                return _appVersion;
            }
        }

        /// <summary>
        /// Gets the application's host's local Ip
        /// </summary>
        public string IPAddress { get; private set; }

        /// <summary>
        /// Gets the application's instance ID
        /// </summary>
        public int InstanceID { get; private set; }
    }
}
