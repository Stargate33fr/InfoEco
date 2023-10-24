using Infoeco.infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infoeco.infrastructure
{
    public class InfoEcoDbContext: DbContext
    {
        public InfoEcoDbContext()
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }


        public InfoEcoDbContext(DbContextOptions options)
           : base(options)
        {
            ChangeTracker.Tracked += OnEntityTracked;
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        void OnEntityTracked(object sender, EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is TrackedEntity entity && !e.FromQuery)
            {
                switch (e.Entry.State)
                {
                    case EntityState.Added:
                        entity.CreeLe = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.ModifieLe = DateTime.UtcNow;
                        break;
                }
            }
        }

        void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is TrackedEntity entity)
                entity.ModifieLe = DateTime.Now;
        }

        public virtual DbSet<UtilisateurEntite>? Utilisateur { get; set; }
        public virtual DbSet<VilleEntite>? Ville { get; set; }
        public virtual DbSet<AppartementEntite>? Appartement { get; set; }
        public virtual DbSet<AgenceImmobiliereEntite>? AgenceImmobiliere { get; set; }
        public virtual DbSet<LocataireEntite>? Locataire { get; set; }
        public virtual DbSet<LocataireAppartementEntite>? LocataireAppartement { get; set; }
        public virtual DbSet<PaiementEntite>? Paiement { get; set; }
        public virtual DbSet<ProvenancePaiementEntite>? ProvenancePaiement { get; set; }
        public virtual DbSet<QuittanceLoyerEntite>? QuittanceLoyer { get; set; }
        public virtual DbSet<EtatDesLieuxEntite>? EtatDesLieux { get; set; }
        public virtual DbSet<TypeAppartementEntite>? TypeAppartement { get; set; }
        public virtual DbSet<CiviliteEntite>? Civilite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=snuffleupagus.db.elephantsql.com;Username=pmjmvgha;Password=ktvzW58l-VY2c6fwNbff1zndMiy3qzuJ;Database=pmjmvgha");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<UtilisateurEntite>()
               .HasIndex(u => u.Courriel)
               .IsUnique();

            modelBuilder.Entity<LocataireAppartementEntite>()
                .HasAlternateKey(pt => new { pt.LocataireId, pt.AppartementId });

            modelBuilder.Entity<LocataireEntite>()
               .HasIndex(u => u.Mail)
               .IsUnique();
        }
    }
}