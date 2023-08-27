

using Microsoft.EntityFrameworkCore;

namespace JALUR
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=session.db");
        }
    }
}
