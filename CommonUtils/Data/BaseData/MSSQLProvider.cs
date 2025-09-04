//using CommonUtils.Settings;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
//using System.Collections.Specialized;

//namespace BaseData
//{
//    public class MSSQLProvider : DataProvider
//    {
//        private const string _createBaseTables = @"IF NOT EXISTS
//(
//    SELECT 1
//        FROM sys.objects
//        WHERE object_id = OBJECT_ID (N'dbo.Users') AND type IN
// (N'U')
//)
//BEGIN
//    CREATE TABLE dbo.Users
//    (
//        _id UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL
//       ,_user_name NVARCHAR(1000) NOT NULL
//       ,_user_login NVARCHAR(100) NOT NULL
//    ) ON [PRIMARY];

//    ALTER TABLE dbo.Users
//    ADD CONSTRAINT DF_Users__id
//        DEFAULT (NEWID ()) FOR _id;

//    ALTER TABLE dbo.Users
//    ADD CONSTRAINT PK_Users
//        PRIMARY KEY CLUSTERED (_id)
//        WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];


//    CREATE UNIQUE NONCLUSTERED INDEX IX_Users
//    ON dbo.Users (_user_login)
//    WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
//    ON [PRIMARY];
//END;

//IF NOT EXISTS
//(
//    SELECT 1
//        FROM sys.objects
//        WHERE object_id = OBJECT_ID (N'dbo.DatabaseParams') AND type IN
// (N'U')
//)
//BEGIN
//    CREATE TABLE dbo.DatabaseParams
//    (
//        _id UNIQUEIDENTIFIER NOT NULL ROWGUIDCOL
//       ,_param_name NVARCHAR(100) NOT NULL
//       ,_param_value NVARCHAR(4000) NOT NULL
//    ) ON [PRIMARY];

//    ALTER TABLE dbo.DatabaseParams
//    ADD CONSTRAINT DF_DatabaseParams__id
//        DEFAULT (NEWID ()) FOR _id;

//    ALTER TABLE dbo.DatabaseParams
//    ADD CONSTRAINT PK_DatabaseParams
//        PRIMARY KEY CLUSTERED (_id)
//        WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];


//    CREATE UNIQUE NONCLUSTERED INDEX IX_DatabaseParams
//    ON dbo.DatabaseParams (_param_name)
//    WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
//    ON [PRIMARY];
//END;

//";
//        private const string _getDatabeseVersion = "SELECT _param_value FROM dbo.DatabaseParams WHERE _param_name = 'DatabaseVersion'";
//        public bool Connected { get; protected set; }
//        public string DataBaseName { get; protected set; }
//        public string UserName { get; protected set; }
//        public string DataBasePath { get; protected set; }
//        public string ServerName { get; protected set; }
//        public string ConnectionString { get; protected set; }
//        public string ConnectionStringToMaster { get; protected set; }
//        public string ProgramPath { get; protected set; }
//        public MSSQLProvider(global::CommonUtils.Loggers.LogUtils logger) : base(logger)
//        {
//            ProviderName = "MSSQLProvider";
//            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//        }

//        public override List<ParamItem> GetParamList()
//        {
//            return new List<ParamItem>();
//            //throw new NotImplementedException();
//        }

//        protected void ExecuteSQL(SqlConnection conn, string query)
//        {

//            var sqls = Regex.Split(query, @"[\n|\r|\s]+GO[\n|\r|\s]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
//            //var sqls = query.Split(new[] {"go","GO","Go", "gO"}, StringSplitOptions.None);
//            SqlCommand myCommand;
//            if (conn.State != ConnectionState.Open)
//                conn.Open();
//            foreach (var sql in sqls)
//            {
//                if (!String.IsNullOrWhiteSpace(sql))
//                {
//                    myCommand = new SqlCommand(sql, conn);
//                    myCommand.ExecuteNonQuery();
//                }
//            }

//        }

//        public bool CheckDataBaseFiles()
//        {
//            return File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}.mdf")) &&
//                                      File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf"));
//        }

//        protected override void ConnectToDataBase(Dictionary<string, string> connectionParams)
//        {
//            DataBaseName = connectionParams["DataBaseName"];
//            DataBasePath = connectionParams["DataBasePath"];
//            UserName = connectionParams["UserName"];
//            if(String.IsNullOrWhiteSpace(DataBasePath)) { throw new Exception("Не указан путь к базе данных"); }    
//            if (!Directory.Exists(DataBasePath))
//                Directory.CreateDirectory(DataBasePath);
//            ServerName = connectionParams["ServerName"];
//            ProgramPath = connectionParams["ProgramPath"];
//            ConnectionString = $@"Data Source={ServerName};Initial Catalog={DataBaseName};Integrated Security=SSPI";
//            ConnectionStringToMaster = $@"Data Source={ServerName};Initial Catalog=master;Integrated Security=SSPI";
//            bool checkFiles = true;
//            bool findFiles = checkFiles && CheckDataBaseFiles();
//            using (SqlConnection sqlconn = new SqlConnection(ConnectionStringToMaster))
//            {
//                var server = new Server(new ServerConnection(sqlconn));
//                bool findBase = server.Databases.Contains(DataBaseName);
//                if (findFiles && !findBase)
//                {
//                    AttachLocalDataBase(server);
//                    return;
//                }
//                if (checkFiles && findBase && !findFiles)
//                    throw new ApplicationException($"База данных уже существует: {Path.Combine(DataBasePath, $"{DataBaseName}.mdf")}, а файлов базы данных нет.");
//                if (!findBase)
//                {
                    
//                    if (sqlconn.State != ConnectionState.Open)
//                        sqlconn.Open();
//                    try
//                    {
                        
//                        string str = checkFiles ?
//                            $"CREATE DATABASE {DataBaseName} ON PRIMARY (NAME = {DataBaseName}_Data, FILENAME = '{Path.Combine(DataBasePath, $"{DataBaseName}.mdf")}', SIZE = 2MB, FILEGROWTH = 10%) LOG ON (NAME = {DataBaseName}_Log, FILENAME = '{Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf")}', SIZE = 1MB, FILEGROWTH = 10%) COLLATE Cyrillic_General_CI_AS" :
//                            $"CREATE DATABASE {DataBaseName} ON PRIMARY (NAME = {DataBaseName}_Data, SIZE = 2MB, FILEGROWTH = 10%) LOG ON (NAME = {DataBaseName}_Log, SIZE = 1MB, FILEGROWTH = 10%) COLLATE Cyrillic_General_CI_AS";
//                        SqlCommand myCommand = new SqlCommand(str, sqlconn);
//                        myCommand.ExecuteNonQuery();
//                        myCommand = new SqlCommand($"USE {DataBaseName}; "+ _createBaseTables, sqlconn);
//                        myCommand.ExecuteNonQuery();
//                        str = @$"USE {DataBaseName}
//INSERT INTO dbo.Users
//(_id,_user_name,_user_login)
//VALUES
//(NEWID(),N'{UserName}',N'{UserName}')
//INSERT INTO dbo.DatabaseParams
//(_id,_param_name,_param_value)
//VALUES
//(NEWID(),N'DatabaseVersion',N'0')";
//                        myCommand = new SqlCommand(str, sqlconn);
//                        myCommand.ExecuteNonQuery();

//                        //ExecuteSQL(sqlconn, sqlText);
//                        //str =
//                        //    $"UPDATE [dbo].[DataBaseVersion] SET[CreateScript] = '{sqlText.Replace("'", "''")}' WHERE[Version] = '01'";
//                        //myCommand = new SqlCommand(str, sqlconn);
//                        //myCommand.ExecuteNonQuery();
//                        //ExexuteSqlOnCreateBaseAfter(sqlconn);
//                    }
//                    catch( Exception ex )
//                    {
//                        throw ex;
//                    }
//                    finally
//                    {
//                        if (sqlconn.State == ConnectionState.Open)
//                            sqlconn.Close();

//                    }

//                }
//                using (SqlConnection connection = new SqlConnection(ConnectionString))
//                {
//                    if (connection.State != ConnectionState.Open)
//                        connection.Open();
//                    try
//                    {
//                        SqlCommand command = new SqlCommand($"USE {DataBaseName}; " + _createBaseTables, sqlconn);
//                        command.ExecuteNonQuery();
//                        string str = @$"USE {DataBaseName}
//IF NOT EXISTS
//(
//    SELECT 1
//        FROM dbo.Users
//        WHERE _user_login = N'{UserName}'
//)
//INSERT INTO dbo.Users
//(_id,_user_name,_user_login)
//VALUES
//(NEWID(),N'{UserName}',N'{UserName}')
//IF NOT EXISTS
//(
//    SELECT 1
//        FROM dbo.DatabaseParams
//        WHERE _param_name = 'DatabaseVersion'
//)

//INSERT INTO dbo.DatabaseParams
//(_id,_param_name,_param_value)
//VALUES
//(NEWID(),N'DatabaseVersion',N'0');";
//                        command = new SqlCommand(str, sqlconn);
//                        command.ExecuteNonQuery();
//                        command = new SqlCommand(_getDatabeseVersion, connection);
//                        SqlDataReader reader = command.ExecuteReader();
//                        int dbVersion = 0;
//                        if (reader.HasRows) // если есть данные
//                        {
//                            while (reader.Read()) // построчно считываем данные
//                            {
//                                int.TryParse(reader.GetString(0), out dbVersion);
//                                break;
                                
//                            }
//                        }
//                        reader.Close();
//                        string[] scriptList = Directory.GetFiles(Path.Combine(ProgramPath, "Scripts\\"), "DataBaseCreate*.sql");
//                        var scripts = new Dictionary<int, string>();
//                        foreach ( string script in scriptList )
//                        {
//                            int vers = 0;
//                            if (!int.TryParse(Path.GetFileName(script).Replace("DataBaseCreate", "").Replace(".sql", ""), out vers))
//                                throw new Exception($"Неверное имя файла: {script}") ;
//                            // todo: При ошибке ничего не происходит Пользователь ее не видит
//                            scripts.Add(vers, script) ;
//                        }
//                        for (int i = dbVersion+1;i <= scripts.Keys.Max();i++)
//                        {
//                            string sqlText = null;
//                            using (StreamReader sr = new StreamReader(scripts[i], Encoding.GetEncoding(1251)))
//                            {
//                                sqlText = sr.ReadToEnd();
//                            }
//                            if (String.IsNullOrWhiteSpace(sqlText))
//                                continue;
//                            ExecuteSQL(connection, sqlText);
//                            command = new SqlCommand($"UPDATE dbo.DatabaseParams SET _param_value = {i} WHERE _param_name = 'DatabaseVersion'", connection);
//                            command.ExecuteNonQuery();
                            
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        throw ex;
//                    }
//                    finally
//                    {
//                        if (connection.State == ConnectionState.Open)
//                            connection.Close();

//                    }
//                }
                

//            }
//        }

//        private void AttachLocalDataBase(Server server)
//        {

//            try
//            {
//                if (!File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}.mdf")) ||
//                                  !File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf")))
//                    throw new ApplicationException($"Не найдена база данных: {Path.Combine(DataBasePath, $"{DataBaseName}.mdf")}");

//                server.AttachDatabase(DataBaseName,
//                new StringCollection
//                {
//                                    Path.Combine(DataBasePath, $"{DataBaseName}.mdf"), Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf")
//                }, AttachOptions.None);



//            }
//            catch (Exception e)
//            {
//                throw new ApplicationException($"Невозможно подключить базу данных. {e.Message}");
//            }




//        }



//        public void DetachBase()
//        {
//            using (SqlConnection sqlconn = new SqlConnection(ConnectionStringToMaster))
//            {
//                try
//                {
//                    var server = new Server(new ServerConnection(sqlconn));
//                    if (server.Databases.Contains(DataBaseName))
//                        server.DetachDatabase(DataBaseName, true);
//                }
//                catch (Exception e)
//                {
//                    throw new ApplicationException($"Невозможно отключить базу данных. {e.Message}");
//                }


//            }
//        }

//        protected override void CreateDataBase(string dataBasePath)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
