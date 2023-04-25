using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using IBid.DAL.Models;

namespace IBid.DAL.DataContext;

public partial class IbidContext : DbContext
{
    public IbidContext()
    {
    }

    public IbidContext(DbContextOptions<IbidContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__AD0500A66ACE2B6B");

            entity.Property(e => e.AdminId).HasColumnName("adminId");
            entity.Property(e => e.Name)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.BidId).HasName("PK__Bids__48E98F585A73F169");

            entity.Property(e => e.BidId).HasColumnName("bidId");
            entity.Property(e => e.CurrentPrice).HasColumnName("currentPrice");
            entity.Property(e => e.EndTime)
                .HasColumnType("date")
                .HasColumnName("endTime");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.StartTime)
                .HasColumnType("date")
                .HasColumnName("startTime");
            entity.Property(e => e.StartingPrice).HasColumnName("startingPrice");
            entity.Property(e => e.VolunteerId).HasColumnName("volunteerId");

            entity.HasOne(d => d.Item).WithMany(p => p.Bids)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__itemId__3F466844");

            entity.HasOne(d => d.Volunteer).WithMany(p => p.Bids)
                .HasForeignKey(d => d.VolunteerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__volunteerI__403A8C7D");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__56A128AAB4D4C85E");

            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.VolunteerId).HasName("PK__Voluntee__D346069BF9F60487");

            entity.Property(e => e.VolunteerId).HasColumnName("volunteerId");
            entity.Property(e => e.Name)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(52)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
