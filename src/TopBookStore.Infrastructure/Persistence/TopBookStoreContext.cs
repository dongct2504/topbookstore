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

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TopBookStore;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC344E9D6688");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C20731EB7FDD");

            entity.Property(e => e.BookId).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Authors");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Publishers");

            entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_BookCategories_Categories"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BookCategories_Books"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("PK__BookCate__9C7051A743A6A870");
                        j.ToTable("BookCategories");
                        j.IndexerProperty<string>("CategoryId")
                            .HasMaxLength(30)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Carts__51BCD7B750F73925");

            entity.Property(e => e.CartId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasConstraintName("FK_Carts_Customers");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B0AE3D2BDAB");

            entity.Property(e => e.CartItemId).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.CartItems).HasConstraintName("FK_CartItems_Books");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK_CartItems_Carts");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B883D44BA");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8E8AE22CC");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF7DD99CA3");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.State).HasDefaultValueSql("('awaiting')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CC449B292");

            entity.Property(e => e.OrderDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetails_Books");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetails_Orders");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FABA365B076");

            entity.Property(e => e.PublisherId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__Receipts__CC08C42071AD6EB1");

            entity.Property(e => e.ReceiptId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Receipts).HasConstraintName("FK_Receipts_Customers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
