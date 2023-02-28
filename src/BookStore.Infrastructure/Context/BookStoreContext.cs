using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Domain.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BookStore;Trusted_Connection=true;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC073ECEFC99");

            entity.ToTable("Author");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC0706C7A058");

            entity.ToTable("Book");

            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
           // entity.Property(e => e.DateCreated).HasColumnType("DateTime");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__CategoryId__3A81B327");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__PublisherI__3B75D760");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC071EB0E53E");

            entity.ToTable("Category");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC07FD2661A5");

            entity.ToTable("Publisher");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}