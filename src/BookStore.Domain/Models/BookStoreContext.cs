//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace BookStore.Domain.Models;

//public partial class BookStoreContext : DbContext
//{
//    public BookStoreContext()
//    {
//    }

//    public BookStoreContext(DbContextOptions<BookStoreContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Author> Authors { get; set; }

//    public virtual DbSet<AuthorContact> AuthorContacts { get; set; }

//    public virtual DbSet<Book> Books { get; set; }

//    public virtual DbSet<BookCategory> BookCategories { get; set; }

//    public virtual DbSet<Publisher> Publishers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BookStoreDB;Trusted_Connection=true;Encrypt=false;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Author>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC071F9B42C7");

//            entity.ToTable("Author");

//            entity.Property(e => e.Name)
//                .HasMaxLength(50)
//                .HasColumnName("NAME");
//        });

//        modelBuilder.Entity<AuthorContact>(entity =>
//        {
//            entity.HasKey(e => e.AuthorId).HasName("PK__AuthorCo__70DAFC34200D134B");

//            entity.ToTable("AuthorContact");

//            entity.Property(e => e.AuthorId).ValueGeneratedNever();
//            entity.Property(e => e.Address).HasMaxLength(100);
//            entity.Property(e => e.ContactNumber).HasMaxLength(15);

//            entity.HasOne(d => d.Author).WithOne(p => p.AuthorContact)
//                .HasForeignKey<AuthorContact>(d => d.AuthorId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__AuthorCon__Autho__3B75D760");
//        });

//        modelBuilder.Entity<Book>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC07581928AE");

//            entity.ToTable("Book");

//            entity.Property(e => e.).HasMaxLength(100);

//            entity.HasOne(d => d.Category).WithMany(p => p.Books)
//                .HasForeignKey(d => d.CategoryId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Book__CategoryId__5070F446");

//            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
//                .HasForeignKey(d => d.PublisherId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Book__PublisherI__5165187F");

//            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
//                .UsingEntity<Dictionary<string, object>>(
//                    "BookAuthor",
//                    r => r.HasOne<Author>().WithMany()
//                        .HasForeignKey("AuthorId")
//                        .OnDelete(DeleteBehavior.ClientSetNull)
//                        .HasConstraintName("FK__BookAutho__Autho__5535A963"),
//                    l => l.HasOne<Book>().WithMany()
//                        .HasForeignKey("BookId")
//                        .OnDelete(DeleteBehavior.ClientSetNull)
//                        .HasConstraintName("FK__BookAutho__BookI__5441852A"),
//                    j =>
//                    {
//                        j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DC4A3D6A362");
//                        j.ToTable("BookAuthors");
//                    });
//        });

//        modelBuilder.Entity<BookCategory>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__BookCate__3214EC072C7032B3");

//            entity.ToTable("BookCategory");

//            entity.Property(e => e.Name)
//                .HasMaxLength(50)
//                .HasColumnName("NAME");
//        });

//        modelBuilder.Entity<Publisher>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC07EAFCCCE9");

//            entity.ToTable("Publisher");

//            entity.Property(e => e.Name)
//                .HasMaxLength(50)
//                .HasColumnName("NAME");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
