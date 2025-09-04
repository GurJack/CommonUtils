using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Database
{
    public class TemporaryDbContext : AppDbContext
    {
        public TemporaryDbContext(string connectionString, ILogger logger)
            : base(connectionString, logger) { }

        public override void SeedDatabase()
        {
            
        }
    }
}
