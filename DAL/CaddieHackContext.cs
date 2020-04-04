using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL
{
    public partial class CaddieHackContext : DbContext
    {
        public CaddieHackContext()
        {
        }

        public CaddieHackContext(DbContextOptions<CaddieHackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Locality> Locality { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonRole> PersonRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShoppingList> ShoppingList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=91.211.152.22;database=caddieHackDB;User id=stfahhadmin;Password=cwS8yYN7JLjhGPgC;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.Person, e.Shop })
                    .HasName("PK__Favorite__41EC061B250FC2AB");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.Favorite)
                    .HasForeignKey(d => d.Person)
                    .HasConstraintName("FKFavorite868523");

                entity.HasOne(d => d.ShopNavigation)
                    .WithMany(p => p.Favorite)
                    .HasForeignKey(d => d.Shop)
                    .HasConstraintName("FKFavorite434994");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnName("label")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ShoppingListNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ShoppingList)
                    .HasConstraintName("FKItem405484");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.Property(e => e.LocalityId).HasColumnName("localityId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode).HasColumnName("zipCode");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Person__AB6E616491E42083")
                    .IsUnique();

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocalityNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Locality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPerson955365");
            });

            modelBuilder.Entity<PersonRole>(entity =>
            {
                entity.HasKey(e => new { e.Person, e.Role })
                    .HasName("PK__Person_R__C6630D6FE21002F8");

                entity.ToTable("Person_Role");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('customer')");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.PersonRole)
                    .HasForeignKey(d => d.Person)
                    .HasConstraintName("FKPerson_Rol250937");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.PersonRole)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPerson_Rol767352");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Role__72E12F1A726F0EF3");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PicturePath)
                    .IsRequired()
                    .HasColumnName("picturePath")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocalityNavigation)
                    .WithMany(p => p.Shop)
                    .HasForeignKey(d => d.Locality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShop472049");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.Property(e => e.ShoppingListId).HasColumnName("shoppingListId");

                entity.Property(e => e.Delivered)
                    .IsRequired()
                    .HasColumnName("delivered")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.DelivererNavigation)
                    .WithMany(p => p.ShoppingListDelivererNavigation)
                    .HasForeignKey(d => d.Deliverer)
                    .HasConstraintName("deliverer");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.ShoppingListOwnerNavigation)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("owner");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
