using Microsoft.EntityFrameworkCore;
using PatientProject.Domain.Entites;
using PatientProject.Domain.Enums;
using System;

namespace PatientProject.Infrastructure.Persistance;

public class DataContext : DbContext
{
    public DbSet<Patient> Patitents { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Patient>(e =>
            {
                e.ToTable("patients");

                e.HasKey(p => p.Name.Id);

                e.Property(p => p.Gender)
                 .HasColumnName("gender")
                 .HasConversion<string>()
                 .HasMaxLength(20);

                e.Property(p => p.BirthDate)
                 .HasColumnName("birth_date")
                 .IsRequired();

                e.Property(p => p.Active)
                .HasColumnName("active")
                .HasDefaultValue(Active.True);

                e.OwnsOne(p => p.Name, name =>
                {
                    name.Property(x => x.Id)
                        .HasColumnName("id");

                    name
                        .Property(n => n.Use)
                        .HasColumnName("use");

                    name
                        .Property(n => n.Family)
                        .HasColumnName("family")
                        .HasMaxLength(255)
                        .IsRequired();

                    name.Property(n => n.Given)
                        .HasColumnName("given")
                        .HasColumnType("text[]");
                });
            });


        base.OnModelCreating(modelBuilder);
    }
}
