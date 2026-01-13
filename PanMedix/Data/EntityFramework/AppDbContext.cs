using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PanMedix.Models;

namespace PanMedix.Data.EntityFramework
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionItem> SubscriptionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");

            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<User>()
                .HasOne(u => u.Guardian)
                .WithMany(g => g.Patients)
                .HasForeignKey(u => u.GuardianId)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
