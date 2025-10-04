using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DrawHub.Data
{
    public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
    {
        public BancoContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DataBase"));

            return new BancoContext(optionsBuilder.Options);
        }
    }
}