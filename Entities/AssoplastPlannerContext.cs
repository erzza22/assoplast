using Microsoft.EntityFrameworkCore;

namespace MVC.Entities
{
    public class AssoplastPlannerContext : DbContext
    {
        public AssoplastPlannerContext()
        {
        }

        public AssoplastPlannerContext(DbContextOptions<AssoplastPlannerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplianceCategory> ApplianceCategory { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<PickupRequest> PickupRequest { get; set; }
        public virtual DbSet<ProduttoreDetentore> ProduttoreDetentore { get; set; }
        public virtual DbSet<ProduttoreDetentoreCategory> ProduttoreDetentoreCategory { get; set; }
        public virtual DbSet<TransportatoreCategory> TransportatoreCategory { get; set; }
        public virtual DbSet<DestinatarioCategory> DestinatarioCategory { get; set; }
        public virtual DbSet<Transportatore> Transportatore { get; set; }
        public virtual DbSet<RequestCategory> RequestCategory { get; set; }

        public virtual DbSet<UserProfileUploads> UserProfileUploads { get; set; }

        public virtual DbSet<Email> Email { get; set; }
    }
}
