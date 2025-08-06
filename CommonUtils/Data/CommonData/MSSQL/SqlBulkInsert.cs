using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CommonUtils.MSSQL
{
    public static class SqlBulkInsert
    {

        public static void WriteToServer(string connectionString, string destinationTableName, IDataReader reader)
        {
            var options = SqlBulkCopyOptions.Default;// SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers;// | SqlBulkCopyOptions.KeepNulls; //| SqlBulkCopyOptions.UseInternalTransaction; //  | SqlBulkCopyOptions.CheckConstraints;
            using (SqlBulkCopy bulkCopy =
                new SqlBulkCopy(connectionString,options))
            {
                bulkCopy.DestinationTableName = destinationTableName;
                //"dbo.BulkCopyDemoMatchingColumns";

                try
                {
                    bulkCopy.WriteToServer(reader);
                }
                finally
                {
                    reader.Close();
                }
            }
        }
    }
}