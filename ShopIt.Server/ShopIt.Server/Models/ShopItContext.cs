using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopIt.Server.Models
{
    public partial class ShopItContext : DbContext
    {
        public virtual DbSet<AdProduct> AdProduct { get; set; }
        public virtual DbSet<AdRetailer> AdRetailer { get; set; }
        public virtual DbSet<Advert> Advert { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Image> Image { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=ShopIt;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdProduct>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Advert)
                    .WithMany(p => p.AdProducts)
                    .HasForeignKey(d => d.AdvertId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AdProduct_Advert");

                entity.HasOne(d => d.ProductImage)
                    .WithMany(p => p.AdProduct)
                    .HasForeignKey(d => d.ProductImageId)
                    .HasConstraintName("FK_AdProduct_Image");
            });

            modelBuilder.Entity<AdRetailer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AdProduct)
                    .WithMany(p => p.AdRetailers)
                    .HasForeignKey(d => d.AdProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AdRetailer_AdProduct");

                entity.HasOne(d => d.RetailerLogo)
                    .WithMany(p => p.AdRetailer)
                    .HasForeignKey(d => d.RetailerLogoId)
                    .HasConstraintName("FK_AdRetailer_Image");
            });

            modelBuilder.Entity<Advert>(entity =>
            {
                entity.Property(e => e.AdvertId).ValueGeneratedNever();

                entity.HasOne(d => d.AdvertNavigation)
                    .WithOne(p => p.InverseAdvertNavigation)
                    .HasForeignKey<Advert>(d => d.AdvertId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Advert_Advert");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Advert)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Advert_Company");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.ImageId).ValueGeneratedNever();

                entity.Property(e => e.ImageData).IsRequired();
            });
        }
    }
}