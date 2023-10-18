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

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK_AUTHORS");

            entity.Property(e => e.PhoneNumber).IsFixedLength();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK_BOOKS");

            entity.Property(e => e.Isbn13).IsFixedLength();

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasConstraintName("FK_BOOKS_BOOKAUTHO_AUTHORS");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasConstraintName("FK_BOOKS_BOOKPUBLI_PUBLISHE");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK_CARTS");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasConstraintName("FK_CARTS_CARTCUSTO_CUSTOMER");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK_CARTITEMS");

            entity.HasOne(d => d.Book).WithMany(p => p.CartItems).HasConstraintName("FK_CARTITEM_CARTITEMB_BOOKS");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK_CARTITEM_CARTITEMC_CARTS");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_CATEGORIES");

            entity.HasMany(d => d.Books).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_BOOKCATE_BOOKCATEG_BOOKS"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_BOOKCATE_BOOKCATEG_CATEGORI"),
                    j =>
                    {
                        j.HasKey("CategoryId", "BookId").HasName("PK_BOOKCATEGORY");
                        j.ToTable("BookCategory");
                        j.HasIndex(new[] { "BookId" }, "BOOKCATEGORY2_FK");
                        j.HasIndex(new[] { "CategoryId" }, "BOOKCATEGORY_FK");
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_CUSTOMERS");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_ORDERS");

            entity.Property(e => e.PhoneNumber).IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_ORDERS_ORDERCUST_CUSTOMER");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK_ORDERDETAILS");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails).HasConstraintName("FK_ORDERDET_ORDERDETA_BOOKS");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_ORDERDET_ORDERDETA_ORDERS");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK_PUBLISHERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
