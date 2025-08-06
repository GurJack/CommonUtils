//using Microsoft.EntityFrameworkCore;

//namespace CommonData.MSSQL
//{
//    public abstract class MSSQLContext:DbContext
//    {
//        private readonly string _connectionString;
//        public MSSQLContext(string connecionString)
//        {
//            _connectionString = connecionString;
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(_connectionString);
//        }
//    }
//}