using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<WeaponModel> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }


    }
}
