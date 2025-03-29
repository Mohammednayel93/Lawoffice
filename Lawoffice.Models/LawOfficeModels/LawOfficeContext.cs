using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lawoffice.Models.LawOfficeModels;

public partial class LawOfficeContext : DbContext
{
    public LawOfficeContext()
    {
    }

    public LawOfficeContext(DbContextOptions<LawOfficeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseType> CaseTypes { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<FileType> FileTypes { get; set; }

    public virtual DbSet<Procedure> Procedures { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Case>(entity =>
        {
            entity.ToTable("Case");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseTypeId).HasColumnName("case_type_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CourtName)
                .HasMaxLength(500)
                .HasColumnName("court_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fees)
                .HasColumnType("money")
                .HasColumnName("fees");
            entity.Property(e => e.FilingLawsuitDate)
                .HasColumnType("datetime")
                .HasColumnName("filing_lawsuit_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LawsuitNumber)
                .HasMaxLength(250)
                .HasColumnName("lawsuit_number");
            entity.Property(e => e.OpponentId).HasColumnName("opponent_id");
            entity.Property(e => e.PaymentOfFees)
                .HasColumnType("money")
                .HasColumnName("payment_of_fees");
            entity.Property(e => e.PowerOfAttorneyNumber)
                .HasMaxLength(50)
                .HasColumnName("power_of_attorney_number");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CaseType).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CaseTypeId)
                .HasConstraintName("FK_Case_CaseType");

            entity.HasOne(d => d.Client).WithMany(p => p.CaseClients)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Case_Users1");

            entity.HasOne(d => d.Opponent).WithMany(p => p.CaseOpponents)
                .HasForeignKey(d => d.OpponentId)
                .HasConstraintName("FK_Case_Users");
        });

        modelBuilder.Entity<CaseType>(entity =>
        {
            entity.ToTable("CaseType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(500)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("File");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.CretaedAt)
                .HasColumnType("datetime")
                .HasColumnName("cretaed_at");
            entity.Property(e => e.FileTypeId).HasColumnName("file_type_id");
            entity.Property(e => e.FileUrl).HasColumnName("file_url");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Case).WithMany(p => p.Files)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_File_Case");

            entity.HasOne(d => d.FileType).WithMany(p => p.Files)
                .HasForeignKey(d => d.FileTypeId)
                .HasConstraintName("FK_File_FileType");
        });

        modelBuilder.Entity<FileType>(entity =>
        {
            entity.ToTable("FileType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(500)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Procedure>(entity =>
        {
            entity.ToTable("Procedure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ProcedureDate)
                .HasColumnType("datetime")
                .HasColumnName("procedure_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Case).WithMany(p => p.Procedures)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Procedure_Case");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Descision).HasColumnName("descision");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SessionDate)
                .HasColumnType("datetime")
                .HasColumnName("session_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Case).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("FK_Session_Case");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(50)
                .HasColumnName("identity_number");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber1)
                .HasMaxLength(50)
                .HasColumnName("phone_number_1");
            entity.Property(e => e.PhoneNumber2)
                .HasMaxLength(50)
                .HasColumnName("phone_number_2");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
