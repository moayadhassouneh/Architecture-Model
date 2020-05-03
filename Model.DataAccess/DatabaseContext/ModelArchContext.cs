
using Microsoft.EntityFrameworkCore;
using Model.DataAccess.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace Model.DataAccess.DatabaseContext
{
    public partial class ModelArchContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {    
        public ModelArchContext(DbContextOptions<ModelArchContext> options) : base(options)
        {           

        }

        #region Tables
        public virtual DbSet<Channel> Channel { get; set; }        

        #endregion
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users", schema: "Security");
                entity.Property(e => e.Id).HasColumnName("UserId");
            });

            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Roles", schema: "Security"); //name schema what ever you want
                entity.Property(e => e.Id).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims", "Security");
                entity.Property(e => e.Id).HasColumnName("UserClaimId");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins", "Security");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims", "Security");
                entity.Property(e => e.Id).HasColumnName("RoleClaimId");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles", "Security");
            });


            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens", "Security");
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);

            // modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value)

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
