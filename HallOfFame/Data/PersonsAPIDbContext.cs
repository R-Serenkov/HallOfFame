using HallOfFame.Models;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Data
{
    public class PersonsAPIDbContext : DbContext
    {
        private static DbContextOptions<PersonsAPIDbContext> _options;

        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public PersonsAPIDbContext(DbContextOptions<PersonsAPIDbContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // Настраиваем основные ключи и зависимости
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().HasMany(x => x.Skills).WithOne();
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Skill>().HasKey(x => x.Id);
        }
    }
}