using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.DataAccess.DatabaseContext;

namespace Model.DataAccess.Migrations
{
    public class ModelArchContextFactory : IDesignTimeDbContextFactory<ModelArchContext>
    {
        public ModelArchContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModelArchContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Work\\Architecture-Model\\Model.DataAccess\\Database1.mdf;Integrated Security=True");

            return new ModelArchContext(optionsBuilder.Options);
        }
    }
}
