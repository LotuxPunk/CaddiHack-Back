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
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShoppingList> ShoppingList { get; set; }
        public virtual DbSet<ShoppingListItem> ShoppingListItem { get; set; }

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
                entity.Property(e => e.FavoriteId).HasColumnName("favoriteId");

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

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ShopNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.Shop)
                    .HasConstraintName("FKItem711087");
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
                    .HasName("UQ__Person__AB6E6164495CEF9B")
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

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocalityNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.Locality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPerson955365");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
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

                entity.Property(e => e.Delivered).HasColumnName("delivered");

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
                    .HasConstraintName("owner");

                entity.HasOne(d => d.ShopNavigation)
                    .WithMany(p => p.ShoppingList)
                    .HasForeignKey(d => d.Shop)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKShoppingLi519458");
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(e => new { e.ShoppingList, e.Item })
                    .HasName("PK__Shopping__1649D2AF861CBE6F");

                entity.ToTable("ShoppingList_Item");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.ShoppingListItem)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShoppingLi447487");

                entity.HasOne(d => d.ShoppingListNavigation)
                    .WithMany(p => p.ShoppingListItem)
                    .HasForeignKey(d => d.ShoppingList)
                    .HasConstraintName("FKShoppingLi913741");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
