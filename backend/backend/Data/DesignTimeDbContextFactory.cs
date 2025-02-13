//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace backend.Data
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            var builder = new DbContextOptionsBuilder();
//            builder.UseSqlServer("Server=DESKTOP-MR5OABM\\SQLEXPRESS; Database=BlogAppDb; TrustServerCertificate=True; Trusted_Connection=True;");
            
//            return new ApplicationDbContext(builder.Options);
//        }
//    }
//}
