using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using BaseData;
using CommonUtils.Loggers;
using CommonUtils.Settings;
using Microsoft.Data.SqlClient;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;


namespace SqlData
{
    public class SqlProvider:DataProvider
    {
        private string _connectionString = "";
        public SqlProvider(LogUtils logger) : base(logger)
        {
            ProviderName = "MSSQLLocalDBProvider";
        }

        public override List<ParamItem> GetParamList()
        {
            throw new NotImplementedException();
        }

        protected override void ConnectToDataBase(Dictionary<string, string> connectionParams)
        {
            //_connectionString = $@"Data Source={ServerName};Initial Catalog={DataBaseName};Integrated Security=SSPI";
            //AttachBase(CreateDataBase, programPath);
        }

        protected override void CreateDataBase(string dataBasePath)
        {
            throw new NotImplementedException();
        }

        

        protected void ExecuteSQL(SqlConnection conn, string query)
        {
            var sqls = query.Split(new[] { "go", "GO", "Go", "gO" }, StringSplitOptions.None);
            SqlCommand myCommand;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            foreach (var sql in sqls)
            {
                myCommand = new SqlCommand(sql, conn);
                myCommand.ExecuteNonQuery();
            }
        }

        //public abstract void ExexuteSqlOnCreateBaseAfter(SqlConnection conn);

        private void AttachBase(bool createDataBase, string programPath)
        {
            //bool findDateBasePath = File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}.mdf")) &&
            //                          File.Exists(Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf"));
            ////throw new ApplicationException($"Не найдена база данных: {Path.Combine(DataBasePath, $"{DataBaseName}.mdf")}");
            //string sqlText = null;
            //using (StreamReader sr = new StreamReader(Path.Combine(programPath, "DataBaseCreate.sql"), Encoding.GetEncoding(1251)))
            //{
            //    sqlText = sr.ReadToEnd();
            //}
            //if (createDataBase && String.IsNullOrWhiteSpace(sqlText))
            //    throw new ApplicationException($"Невозможно создать базу данных. Нет скрипта для создания.");
            //if (createDataBase && findDateBasePath)
            //    throw new ApplicationException($"Невозможно создать базу данных, так как найдены файлы базы данных.");
            ////using (SqlConnection sqlconn =
            ////    new SqlConnection($@"Data Source={ServerName};Initial Catalog=master;Integrated Security=SSPI"))
            ////{
            ////    try
            ////    {
            ////        var server = new Server(new ServerConnection(sqlconn));
            ////        bool findBase = server.Databases.Contains(DataBaseName);


            ////        //if (findBase)
            ////        //{
            ////        //    server.DetachDatabase(DataBaseName,true);
            ////        //    findBase = false;
            ////        //}


            ////        if (findBase && findDateBasePath && !createDataBase)
            ////            return;
            ////        if (findBase && createDataBase)
            ////            throw new ApplicationException($"Невозможно создать базу. База уже существует {DataBaseName}");
            ////        if (!findBase && findDateBasePath)
            ////        {
            ////            server.AttachDatabase(DataBaseName,
            ////                new StringCollection
            ////                {
            ////                    Path.Combine(DataBasePath, $"{DataBaseName}.mdf"), Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf")
            ////                }, AttachOptions.None);
            ////        }

            ////        if (createDataBase)
            ////        {
            ////            if (sqlconn.State != ConnectionState.Open)
            ////                sqlconn.Open();
            ////            try
            ////            {
            ////                string str =
            ////                    $"CREATE DATABASE {DataBaseName} ON PRIMARY (NAME = {DataBaseName}_Data, FILENAME = '{Path.Combine(DataBasePath, $"{DataBaseName}.mdf")}', SIZE = 2MB, FILEGROWTH = 10%) LOG ON (NAME = {DataBaseName}_Log, FILENAME = '{Path.Combine(DataBasePath, $"{DataBaseName}_log.ldf")}', SIZE = 1MB, FILEGROWTH = 10%) COLLATE Cyrillic_General_CI_AS";
            ////                SqlCommand myCommand = new SqlCommand(str, sqlconn);
            ////                myCommand.ExecuteNonQuery();
            ////                ExecuteSQL(sqlconn, sqlText);
            ////                str =
            ////                    $"UPDATE [dbo].[DataBaseVersion] SET[CreateScript] = '{sqlText.Replace("'", "''")}' WHERE[Version] = '01'";
            ////                myCommand = new SqlCommand(str, sqlconn);
            ////                myCommand.ExecuteNonQuery();
            ////                //ExexuteSqlOnCreateBaseAfter(sqlconn);
            ////            }
            ////            finally
            ////            {
            ////                if (sqlconn.State == ConnectionState.Open)
            ////                    sqlconn.Close();

            ////            }


            ////        }


            ////    }
            ////    catch (Exception e)
            ////    {
            ////        throw new ApplicationException($"Невозможно подключить базу данных. {e.Message}");
            ////    }



            ////}
        }
    }
}
