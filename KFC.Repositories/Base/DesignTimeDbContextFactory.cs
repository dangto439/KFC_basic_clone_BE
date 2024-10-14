using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KFC.Repositories.Base
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KFCDBContext>
    {
        public KFCDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KFCDBContext>();

            // Load configuration from appsettings.json
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var connectionString = config.GetConnectionString("DefaultConnection");

            builder.UseSqlServer("server=(local);database=KFC;uid=sa;pwd=12345;Encrypt=false;TrustServerCertificate=true;Integrated Security=false;Timeout=30;");

            return new KFCDBContext(builder.Options);
        }
    }
}
