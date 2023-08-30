using Microsoft.EntityFrameworkCore;

namespace VotingSystemBigBrotherBrasil.Consumer.Data
{
    public class VotingSystemContext : DbContext
    {
        public DbSet<Vote> Votes { get; set; }

        private readonly string _connection = "Server=(localdb)\\mssqllocaldb;Database=VotingSystemDB;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>()
                .HaveMaxLength(50);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vote>()
                .Property(v => v.ParticipantName).IsRequired();
        }
    }
}