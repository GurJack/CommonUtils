//using System;
//using Microsoft.EntityFrameworkCore;

//namespace CommonData.MySQL
//{
//    public abstract class MySQLContext:DbContext
//    {
//        private readonly string _connectionString;
//        private readonly int _majorMySql;
//        private readonly int _minorMySql;
//        private readonly int _buildMySql;
//        public MySQLContext(string connecionString, int majorMySql, int minorMySql, int buildMySql)
//        {
//            _connectionString = connecionString;
//            _majorMySql = majorMySql;
//            _minorMySql = minorMySql;
//            _buildMySql = buildMySql;
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseMySql(
//                _connectionString,
//                new MySqlServerVersion(new Version(_majorMySql, _minorMySql, _buildMySql))
//            );
//        }
//    }
//}