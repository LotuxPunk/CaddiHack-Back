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
                optionsBuilder.UseSqlServer("Server=localhost;database=caddieHackDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .HasName("UQ__Person__AB6E61644AEEB32F")
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
                    .HasName("PK__Person_R__C6630D6FAAE66181");

                entity.ToTable("Person_Role");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.PersonRole)
                    .HasForeignKey(d => d.Person)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .HasName("PK__Role__72E12F1A595A5B7B");

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

                entity.HasOne(d => d.LocalityNavigation)
                    .WithMany(p => p.Shop)
                    .HasForeignKey(d => d.Locality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShop472049");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.Property(e => e.ShoppingListId).HasColumnName("shoppingListId");

                entity.Property(e => e.Delivery).HasColumnName("delivery");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.ShoppingList)
                    .HasForeignKey(d => d.Person)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShoppingLi18603");

                entity.HasOne(d => d.ShopNavigation)
                    .WithMany(p => p.ShoppingList)
                    .HasForeignKey(d => d.Shop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShoppingLi519458");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
