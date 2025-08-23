using Microsoft.EntityFrameworkCore;
using NLog;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;
using CommonUtils.Settings.Attributes;


namespace CommonUtils.Database
{
    public abstract class AppDbContext : DbContext
    {
        protected readonly string _connectionString;
        protected readonly ILogger _logger;
        private static readonly Logger _nullLogger = LogManager.CreateNullLogger(); // Пустой логгер

        protected AppDbContext(string connectionString, ILogger logger)
            : base(GetOptions(connectionString, logger))
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        // Конструктор для времени разработки
        protected AppDbContext() : this(GlobalConstant.GetConnectionString(), _nullLogger) { }


        private static DbContextOptions GetOptions(string connectionString, ILogger logger)
        {
            return new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .Options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

                
                
            }
        }

        public abstract void SeedDatabase();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveConversion<EncryptedValueConverter>();
                //.Where(p => p.PropertyInfo?.GetCustomAttribute<CryptAttribute>()?.IsCrypt == true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Автоматическое применение конфигураций
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(GetType()) ?? Assembly.GetExecutingAssembly());
        }

        public void EnsureDatabaseCreated()
        {
            try
            {
                if (Database.EnsureCreated())
                {
                    _logger.Info($"База данных создана: {Database.GetDbConnection().Database}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка создания базы данных");
                throw;
            }
        }

        public bool IsModelCompatible()
        {
            try
            {
                return !Database.GetPendingMigrations().Any();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    

}
