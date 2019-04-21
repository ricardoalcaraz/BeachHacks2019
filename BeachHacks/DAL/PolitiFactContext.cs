using System;
using BeachHacks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeachHacks.DAL
{
    public partial class PolitiFactContext : DbContext
    {
        public PolitiFactContext()
        {
        }

        public PolitiFactContext(DbContextOptions<PolitiFactContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Politicalparty> Politicalparty { get; set; }
        public virtual DbSet<Presidentialcandidate> Presidentialcandidate { get; set; }
        public virtual DbSet<Tweet> Tweet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=PolitiFact;Username=cinna;Password=ToMang0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Politicalparty>(entity =>
            {
                entity.ToTable("politicalparty");

                entity.Property(e => e.PoliticalPartyId).HasColumnName("political_party_id");

                entity.Property(e => e.PartyName)
                    .HasColumnName("party_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Presidentialcandidate>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("presidentialcandidate_pkey");

                entity.ToTable("presidentialcandidate");

                entity.HasIndex(e => e.Name)
                    .HasName("presidentialcandidate_name_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PoliticalPartyId).HasColumnName("political_party_id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(2);

                entity.HasOne(d => d.PoliticalParty)
                    .WithMany(p => p.Presidentialcandidate)
                    .HasForeignKey(d => d.PoliticalPartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("presidentialcandidate_political_party_id_fkey");
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.ToTable("tweet");

                entity.Property(e => e.TweetId).HasColumnName("tweet_id");

                entity.Property(e => e.PoliticalCandidate).HasColumnName("political_candidate");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(500);

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.TwitterName)
                    .IsRequired()
                    .HasColumnName("twitter_name")
                    .HasMaxLength(50);

                entity.Property(e => e.TwitterUserId).HasColumnName("twitter_user_id");

                entity.HasOne(d => d.PoliticalCandidateNavigation)
                    .WithMany(p => p.Tweet)
                    .HasForeignKey(d => d.PoliticalCandidate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tweet_political_candidate_fkey");
            });
        }
    }
}
