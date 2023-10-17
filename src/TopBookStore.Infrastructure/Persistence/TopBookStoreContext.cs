using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TopBookStore.Domain.Entities;
using TopBookStore.Infrastructure.Identity;

namespace TopBookStore.Infrastructure.Persistence;

public partial class TopBookStoreContext : IdentityTopBookStoreDbContext
{
    public TopBookStoreContext(DbContextOptions<TopBookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2AFB6B17D448");

            entity.HasOne(d => d.Customer).WithMany(p => p.Addresses).HasConstraintName("FK_Addresses_Customers");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC342D4F3D88");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207393D586C");

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
                        .HasConstraintName("FK_BookCategory_Categories"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BookCategory_Books"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("PK__BookCate__9C7051A7CA4C337C");
                        j.ToTable("BookCategory");
                    });
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Carts__51BCD7B7BF79390F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasConstraintName("FK_Carts_Customers");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B0AD51B588C");

            entity.HasOne(d => d.Book).WithMany(p => p.CartItems).HasConstraintName("FK_CartItems_Books");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK_CartItems_Carts");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BA345D279");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8F46771C4");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF3D594098");

            entity.Property(e => e.State).HasDefaultValueSql("('awaiting')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CCFD7C0F5");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetails_Books");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_OrderDetails_Orders");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FABE00C6E8A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
