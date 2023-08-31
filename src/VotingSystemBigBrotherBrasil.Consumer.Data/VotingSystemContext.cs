using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(50)");

            modelBuilder.Entity<Vote>()
                .Property(v => v.ParticipantName).IsRequired();

            modelBuilder.Entity<Vote>()
                .Property(v => v.Votes).IsRequired();

        }

        public bool Commit(string participantName)
        {
            var participantExist = Votes.Any(v => v.ParticipantName.Equals(participantName));

            if (!participantExist)
            {
                Votes.Add(new Vote()
                {
                    ParticipantName = participantName,
                    Votes = 1
                });

                return SaveChanges() > 0;
            }

            return false;
        }
    }
}