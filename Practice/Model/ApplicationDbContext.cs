using Microsoft.EntityFrameworkCore;

namespace Practice.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
    }
}
