using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TTMH_EDA_HIS.Models;

namespace TTMH_EDA_HIS.Data;

public partial class HisdbContext : DbContext
{
    public HisdbContext()
    {
    }

    public HisdbContext(DbContextOptions<HisdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cashier> Cashiers { get; set; }

    public virtual DbSet<Chart> Charts { get; set; }

    public virtual DbSet<ChartsDrugsDosage> ChartsDrugsDosages { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorsPatientsChart> DoctorsPatientsCharts { get; set; }

    public virtual DbSet<Dosage> Dosages { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Pharmacist> Pharmacists { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<RoutesOfAdminstration> RoutesOfAdminstrations { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HISDB;TrustServerCertificate=true;MultipleActiveResultSets=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.HasKey(e => e.CasId).HasName("PK__Cashiers__6B6EF2C78E8D5CE5");

            entity.Property(e => e.CasId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CasID");

            entity.HasOne(d => d.Cas).WithOne(p => p.Cashier)
                .HasForeignKey<Cashier>(d => d.CasId)
                .HasConstraintName("FK_Cashiers_Employees");
        });

        modelBuilder.Entity<Chart>(entity =>
        {
            entity.HasKey(e => e.ChaId).HasName("PK__Charts__97A59C75CBFB1D4C");

            entity.Property(e => e.ChaId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ChaID");
            entity.Property(e => e.DepartmentName).HasMaxLength(20);
            entity.Property(e => e.Vdate)
                .HasColumnType("datetime")
                .HasColumnName("VDate");
        });

        modelBuilder.Entity<ChartsDrugsDosage>(entity =>
        {
            entity.HasKey(e => new { e.ChaId, e.DrugId, e.DosId }).HasName("PK__Charts_D__43E0B46C2967DE43");

            entity.ToTable("Charts_Drugs_Dosages");

            entity.Property(e => e.ChaId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ChaID");
            entity.Property(e => e.DrugId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DrugID");
            entity.Property(e => e.DosId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DosID");

            entity.HasOne(d => d.Cha).WithMany(p => p.ChartsDrugsDosages)
                .HasForeignKey(d => d.ChaId)
                .HasConstraintName("FK_Charts_Drugs_Dosages_Charts");

            entity.HasOne(d => d.Dos).WithMany(p => p.ChartsDrugsDosages)
                .HasForeignKey(d => d.DosId)
                .HasConstraintName("FK_Charts_Drugs_Dosages_Dosages");

            entity.HasOne(d => d.Drug).WithMany(p => p.ChartsDrugsDosages)
                .HasForeignKey(d => d.DrugId)
                .HasConstraintName("FK_Charts_Drugs_Dosages_Drugs");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.DetId).HasName("PK__Details__D8957AFCE2D73766");

            entity.Property(e => e.DetId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DetID");
            entity.Property(e => e.CasId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CasID");
            entity.Property(e => e.MedicalCost).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.PatientId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PatientID");
            entity.Property(e => e.Payable).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.Registration).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.Cas).WithMany(p => p.Details)
                .HasForeignKey(d => d.CasId)
                .HasConstraintName("FK_Details_Cashiers");

            entity.HasOne(d => d.Patient).WithMany(p => p.Details)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Details_Patients");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EDF9BB9663C");

            entity.Property(e => e.DoctorId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DoctorID");
            entity.Property(e => e.DepartmentName).HasMaxLength(20);

            entity.HasOne(d => d.DoctorNavigation).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.DoctorId)
                .HasConstraintName("FK_Doctors_Employees");
        });

        modelBuilder.Entity<DoctorsPatientsChart>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.PatientId, e.ChaId }).HasName("PK__Doctors___20274777D9BDA64F");

            entity.ToTable("Doctors_Patients_Charts");

            entity.Property(e => e.DoctorId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DoctorID");
            entity.Property(e => e.PatientId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PatientID");
            entity.Property(e => e.ChaId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ChaID");

            entity.HasOne(d => d.Cha).WithMany(p => p.DoctorsPatientsCharts)
                .HasForeignKey(d => d.ChaId)
                .HasConstraintName("FK_Doctors_Patients_Charts_Charts");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorsPatientsCharts)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Doctors_Patients_Charts_Doctors");

            entity.HasOne(d => d.Patient).WithMany(p => p.DoctorsPatientsCharts)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Doctors_Patients_Charts_Patients");
        });

        modelBuilder.Entity<Dosage>(entity =>
        {
            entity.HasKey(e => e.DosId).HasName("PK__Dosages__4DFE76ACB6EE094D");

            entity.Property(e => e.DosId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DosID");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.DrugId).HasName("PK__Drugs__908D66F6408F425E");

            entity.Property(e => e.DrugId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DrugID");
            entity.Property(e => e.Atccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ATCCode");
            entity.Property(e => e.Dcontent)
                .IsUnicode(false)
                .HasColumnName("DContent");
            entity.Property(e => e.DrugName).IsUnicode(false);
            entity.Property(e => e.GenericName).IsUnicode(false);
            entity.Property(e => e.Nhicode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NHICode");
            entity.Property(e => e.Roaid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROAID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.WarningPrecautions).HasColumnName("Warning_Precautions");

            entity.HasOne(d => d.Roa).WithMany(p => p.Drugs)
                .HasForeignKey(d => d.Roaid)
                .HasConstraintName("FK_Drugs_RoutesOfAdminstrations");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1CB77EC3F");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC346C8B25736");

            entity.Property(e => e.PatientId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PatientID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Blood)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CaseHistory)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nhicard)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NHICard");
            entity.Property(e => e.PatientName).HasMaxLength(50);
        });

        modelBuilder.Entity<Pharmacist>(entity =>
        {
            entity.HasKey(e => e.PhaId).HasName("PK__Pharmaci__5D18076B51E8930B");

            entity.Property(e => e.PhaId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PhaID");

            entity.HasOne(d => d.Pha).WithOne(p => p.Pharmacist)
                .HasForeignKey<Pharmacist>(d => d.PhaId)
                .HasConstraintName("FK_Pharmacists_Employees");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PresNo).HasName("PK__Prescrip__1401F7AF47170057");

            entity.Property(e => e.PresNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DrugDate).HasColumnType("datetime");
            entity.Property(e => e.PatientId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PatientID");
            entity.Property(e => e.PhaId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PhaID");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Prescriptions_Patients");

            entity.HasOne(d => d.Pha).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PhaId)
                .HasConstraintName("FK_Prescriptions_Pharmacists");
        });

        modelBuilder.Entity<RoutesOfAdminstration>(entity =>
        {
            entity.HasKey(e => e.Roaid).HasName("PK__RoutesOf__84E1649C98E0E0F3");

            entity.Property(e => e.Roaid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROAID");
            entity.Property(e => e.BodyParts).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = "D1001", EmployeeName = "YuDaLu", Account = "D1001", Password = "D1001" },
            new Employee { EmployeeId = "D1002", EmployeeName = "鍾伊惠", Account = "D1002", Password = "D1002" },
            new Employee { EmployeeId = "D1003", EmployeeName = "連智健", Account = "D1003", Password = "D1003" },
            new Employee { EmployeeId = "D1004", EmployeeName = "盧昱達", Account = "D1004", Password = "D1004" },
            new Employee { EmployeeId = "D1005", EmployeeName = "林廣學", Account = "D1005", Password = "D1005" }
        );
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { DoctorId="", DepartmentName=""}    
        );
        modelBuilder.Entity<Pharmacist>().HasData(
            new Pharmacist { PhaId="" }
        );
        modelBuilder.Entity<Cashier>().HasData(
            new Cashier { CasId="" }
        );
        modelBuilder.Entity<DoctorsPatientsChart>().HasData(
            new DoctorsPatientsChart { DoctorId="", PatientId="", ChaId="" }
        );
        modelBuilder.Entity<Patient>().HasData(
            new Patient { PatientId="", Nhicard="", CaseHistory="", PatientName="", BirthDate= new DateTime(1999/06/07), Gender="", Blood="", Address="", Mobile="" }
        );
        modelBuilder.Entity<Chart>().HasData(
            new Chart { ChaId="", DepartmentName="", Vdate=new DateTime(2023/05/30), Subject="", Object="" }
        );
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { PresNo="", DrugDate=new DateTime(2023/05/30), PhaId="", PatientId="" }
        );
        modelBuilder.Entity<Dosage>().HasData(
            new Dosage { DosId="", Direction="" }
        );
        modelBuilder.Entity<Drug>().HasData(
            new Drug { DrugId="", Atccode="", Nhicode="", DrugName="", GenericName="", UnitPrice=240, Dcontent="", Appearance="", ClinicalUses="", SuggestedUsage="", AdverseReactions="", WarningPrecautions="", PointOfHealthEducation="", StorageConditions="", OtherInstructions="", ProcessingMethod="", Roaid=""  }
        );
        modelBuilder.Entity<ChartsDrugsDosage>().HasData(
            new ChartsDrugsDosage { ChaId="", DrugId="", DosId="", Days=3, Total=9, Remark="" }
        );
        modelBuilder.Entity<RoutesOfAdminstration>().HasData(
            new RoutesOfAdminstration { Roaid="", BodyParts="" }
        );
        modelBuilder.Entity<Detail>().HasData(
            new Detail { DetId="", Registration=150, MedicalCost=500, Payable=650, CasId="", PatientId="" }
        );

        // // // //
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
