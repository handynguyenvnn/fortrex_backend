using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Realtime.Host.Entities;
using System.IO;

namespace Realtime.Host
{
    public class RealTimeChartDBContextFactory : IDesignTimeDbContextFactory<CoreDatabaseContext>
    {
        public CoreDatabaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<CoreDatabaseContext>();

            var connectionString = configuration.GetConnectionString("DbConnection");

            builder.UseSqlServer(connectionString);
            
            return new CoreDatabaseContext(builder.Options);
        }
    }
}
