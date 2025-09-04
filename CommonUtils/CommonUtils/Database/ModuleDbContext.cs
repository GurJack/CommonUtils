using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace CommonUtils.Database
{
    public abstract class ModuleDbContext : AppDbContext
    {
        public string SchemaName { get; }
        public string ModuleName { get; }

        protected ModuleDbContext(string connectionString, ILogger logger, string moduleName)
            : base(connectionString, logger)
        {
            ModuleName = moduleName;
            SchemaName = $"{moduleName}Schema";
        }

        protected ModuleDbContext( string moduleName)
            : base()
        {
            ModuleName = moduleName;
            SchemaName = $"{moduleName}Schema";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            base.OnModelCreating(modelBuilder);
        }

        public void VerifySchema()
        {
            try
            {
                var q = Database.SqlQueryRaw<int>($"SELECT count(*) _count FROM sys.schemas WHERE name = '{SchemaName}'")
                    .FirstOrDefault();
                var schemaExists = (Database.SqlQueryRaw<int>(
                    $"SELECT count(*) FROM sys.schemas WHERE name = '{SchemaName}'")
                    .FirstOrDefault()) > 0;

                if (!schemaExists)
                {
                    Database.ExecuteSqlRaw($"CREATE SCHEMA {SchemaName}");
                    _logger.Info($"Создана схема: {SchemaName}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка проверки схемы {SchemaName}");
                throw;
            }
        }

        
    }
}
