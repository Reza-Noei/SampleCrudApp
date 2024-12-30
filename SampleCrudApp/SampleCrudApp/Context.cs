using Microsoft.EntityFrameworkCore;
using SampleCrudApp.Domain;

namespace SampleCrudApp
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            
        }

        public DbSet<Person> People { get; set; }       
    }
}
