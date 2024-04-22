using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IGS.Models
{
    public partial class IGSCompanyContext : DbContext
    {
        public IGSCompanyContext()
        {
        }

        public IGSCompanyContext(DbContextOptions<IGSCompanyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryDetailes> CategoryDetailes { get; set; }
        public virtual DbSet<Medias> Medias { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.CategoryImageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryImagePath).IsRequired();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_Users");
            });

            modelBuilder.Entity<CategoryDetailes>(entity =>
            {
                entity.Property(e => e.DetailesName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.CategoryDetailes)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryDetailes_Categories");
            });

            modelBuilder.Entity<Medias>(entity =>
            {
                entity.Property(e => e.AdvertiserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MedConteact)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MedDate).HasColumnType("date");

                entity.Property(e => e.MedDele).HasColumnType("date");

                entity.Property(e => e.MedDescription).IsRequired();

                entity.Property(e => e.MedImageAname)
                    .HasColumnName("MedImageAName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageApath).HasColumnName("MedImageAPath");

                entity.Property(e => e.MedImageBname)
                    .HasColumnName("MedImageBName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageBpath).HasColumnName("MedImageBPath");

                entity.Property(e => e.MedImageCname)
                    .HasColumnName("MedImageCName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageCpath).HasColumnName("MedImageCPath");

                entity.Property(e => e.MedImageDname)
                    .HasColumnName("MedImageDName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageDpath).HasColumnName("MedImageDPath");

                entity.Property(e => e.MedImageEname)
                    .HasColumnName("MedImageEName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageEpath).HasColumnName("MedImageEPath");

                entity.Property(e => e.MedImageFname)
                    .HasColumnName("MedImageFName")
                    .HasMaxLength(100);

                entity.Property(e => e.MedImageFpath).HasColumnName("MedImageFPath");

                entity.Property(e => e.MedLcoationname)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.MedName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MedPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MedStuation)
                    .HasMaxLength(150);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Medias)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medias_Categories");

                entity.HasOne(d => d.Catdet)
                    .WithMany(p => p.Medias)
                    .HasForeignKey(d => d.CatdetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medias_CategoryDetailes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Medias)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medias_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
