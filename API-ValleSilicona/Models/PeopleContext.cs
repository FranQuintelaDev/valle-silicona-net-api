using Microsoft.EntityFrameworkCore;

namespace API_ValleSilicona.Models
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options)
            : base(options)
        {
        }

        public DbSet<People> PeopleItems { get; set; } = null!;
    }
}
