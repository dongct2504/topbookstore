using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Infrastructure.Persistence;

public partial class TopBookStoreContext : DbContext
{
    public TopBookStoreContext()
    {
    }

    public TopBookStoreContext(DbContextOptions<TopBookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TopBookStore;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC3464B683CC");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2079FDD2A18");

            entity.Property(e => e.BookId).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasConstraintName("FK_Books_Authors");

            entity.HasOne(d => d.Cart).WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Carts");

            entity.HasOne(d => d.Order).WithMany(p => p.Books).HasConstraintName("FK_Books_Orders");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasConstraintName("FK_Books_Publishers");

            entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookCategories_Categories"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BookCategories_Books"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("PK__BookCate__9C7051A70F2D3EFF");
                        j.ToTable("BookCategories");
                        j.IndexerProperty<string>("CategoryId")
                            .HasMaxLength(30)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Carts__51BCD7B738B10524");

            entity.Property(e => e.CartId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B50550405");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D895200631");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.HasOne(d => d.Cart).WithMany(p => p.Customers).HasConstraintName("FK_Customers_Carts");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF520E5FFA");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.State).HasDefaultValueSql("('awaiting')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FAB828A8CE3");

            entity.Property(e => e.PublisherId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__Receipts__CC08C42068B1FEDC");

            entity.Property(e => e.ReceiptId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Receipts).HasConstraintName("FK_Receipts_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
