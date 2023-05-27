using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.DbEntities;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext()
    {
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminInfo> AdminInfos { get; set; }

    public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }

    public virtual DbSet<DishesInfo> DishesInfos { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<TableSeating> TableSeatings { get; set; }

    public virtual DbSet<VisitDetail> VisitDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:abdulaleemserver.database.windows.net,1433;Initial Catalog=restaurantDb;Persist Security Info=False;User ID=abdulaleem;Password=Aa@1052001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminInfo>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__AdminInf__719FE488AFB6FFD4");

            entity.ToTable("AdminInfo");

            entity.HasIndex(e => e.Email, "UQ__AdminInf__A9D10534D5CF504D").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerDetail>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8B5B291FB");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105340FC87555").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DishesInfo>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PK__DishesIn__18834F508769F42E");

            entity.ToTable("DishesInfo");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderDet__C3905BCFAD99D11D");

            entity.HasOne(d => d.Dish).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__OrderDeta__DishI__71D1E811");

            entity.HasOne(d => d.Visit).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK__OrderDeta__Visit__70DDC3D8");
        });

        modelBuilder.Entity<TableSeating>(entity =>
        {
            entity.HasKey(e => e.TableSeatingId).HasName("PK__TableSea__9FFE8D1809F222C6");

            entity.ToTable("TableSeating");

            entity.HasIndex(e => e.TableNo, "UQ__TableSea__7D5F09DC53A3816D").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VisitDetail>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__VisitDet__4D3AA1DEB0A57325");

            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.VisitDetails)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__VisitDeta__Custo__6B24EA82");

            entity.HasOne(d => d.Table).WithMany(p => p.VisitDetails)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("FK__VisitDeta__Table__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
