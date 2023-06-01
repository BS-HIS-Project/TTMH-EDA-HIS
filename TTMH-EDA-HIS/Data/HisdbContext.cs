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
            new Employee { EmployeeId = "D11201001", EmployeeName = "YuDaLu", Account = "D1001", Password = "D1001" },
            new Employee { EmployeeId = "P11201001", EmployeeName = "鍾伊惠", Account = "P1002", Password = "P1002" },
            new Employee { EmployeeId = "D11201002", EmployeeName = "連智健", Account = "D1003", Password = "D1003" },
            new Employee { EmployeeId = "C11201001", EmployeeName = "盧昱達", Account = "C1004", Password = "C1004" },
            new Employee { EmployeeId = "P11201002", EmployeeName = "林廣學", Account = "P1005", Password = "P1005" },
            new Employee { EmployeeId = "C11201002", EmployeeName = "Althea", Account = "C1006", Password = "C1006" }
        );
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { DoctorId="D11201001", DepartmentName="心臟內科"},    
            new Doctor { DoctorId="D11201002", DepartmentName="心臟內科"}     
        );
        modelBuilder.Entity<Pharmacist>().HasData(
            new Pharmacist { PhaId="P11201001" },
            new Pharmacist { PhaId="P11201002" }
        );
        modelBuilder.Entity<Cashier>().HasData(
            new Cashier { CasId="C11201001" },
            new Cashier { CasId="C11201002" }
        );
        modelBuilder.Entity<DoctorsPatientsChart>().HasData(
            new DoctorsPatientsChart { DoctorId="D11201001", PatientId= "A118992634", ChaId= "CHA20230530001001" },
            new DoctorsPatientsChart { DoctorId="D11201001", PatientId= "O101929955", ChaId= "CHA20230530001002" },
            new DoctorsPatientsChart { DoctorId="D11201002", PatientId= "H255590997", ChaId= "CHA20230530001003" },
            new DoctorsPatientsChart { DoctorId="D11201002", PatientId= "L198058112", ChaId= "CHA20230530001004" },
            new DoctorsPatientsChart { DoctorId="D11201002", PatientId= "S257920071", ChaId= "CHA20230530001005" }
        );
        modelBuilder.Entity<Patient>().HasData(
            // PAT + NOWDATE + 診間號 + (病患)序號
            new Patient { PatientId="A118992634", Nhicard="000012345678", CaseHistory="PAT20160610001001", PatientName= "水戶黃門", BirthDate= new DateTime(1999,06,07), Gender="1", Blood="A", Address="台北市", Mobile="0912345678" },
            new Patient { PatientId="O101929955", Nhicard="080009699912", CaseHistory="PAT20160610001002", PatientName="海綿寶寶", BirthDate= new DateTime(1997,05,26), Gender="1", Blood="A", Address="新竹市", Mobile="0965478932" },
            new Patient { PatientId="H255590997", Nhicard="123456789011", CaseHistory="PAT20161225001003", PatientName="晨曦", BirthDate= new DateTime(1997,08,13), Gender="2", Blood="AB", Address="桃園市", Mobile="0955664477" },
            new Patient { PatientId="L198058112", Nhicard="647519785134", CaseHistory="PAT20170526001004", PatientName="野原新之助", BirthDate= new DateTime(1992,12,22), Gender="1", Blood="O", Address="台中市", Mobile="0964973125" },
            new Patient { PatientId="S257920071", Nhicard="715687493157", CaseHistory="PAT20170811001005", PatientName= "橘子", BirthDate= new DateTime(2000,02,29), Gender="2", Blood="O", Address="高雄市", Mobile="0997919395" }
        );
        modelBuilder.Entity<Chart>().HasData(
            // CHA + NOWDATE + 診間號 + (就診號)序號
            new Chart { ChaId="CHA20230530001001", DepartmentName= "心臟內科", Vdate=new DateTime(2023,05,30), Subject="胸悶", Object="心臟病" },
            new Chart { ChaId="CHA20230530001002", DepartmentName= "心臟內科", Vdate=new DateTime(2023,05,30), Subject="心跳好像一直不規律，呼吸不過來", Object="心臟病" },
            new Chart { ChaId="CHA20230530001003", DepartmentName= "心臟內科", Vdate=new DateTime(2023,05,30), Subject="頭痛、噁心、冒冷汗", Object="心臟病" },
            new Chart { ChaId="CHA20230530001004", DepartmentName= "心臟內科", Vdate=new DateTime(2023,05,30), Subject="呼吸急促、胸悶", Object="心臟病" },
            new Chart { ChaId="CHA20230530001005", DepartmentName= "心臟內科", Vdate=new DateTime(2023,05,30), Subject="心臟很痛", Object="心臟病" }
        );
        modelBuilder.Entity<Prescription>().HasData(
            // PRE + NOWDATE + 診間號 + (領藥號)序號
            new Prescription { PresNo= "PRE20230530001001", DrugDate=new DateTime(2023,05,30), PhaId= "P11201001", PatientId= "A118992634" },
            new Prescription { PresNo= "PRE20230530001002", DrugDate=new DateTime(2023,05,30), PhaId= "P11201001", PatientId= "O101929955" },
            new Prescription { PresNo= "PRE20230530001003", DrugDate=new DateTime(2023,05,30), PhaId= "P11201001", PatientId= "H255590997" },
            new Prescription { PresNo= "PRE20230530001004", DrugDate=new DateTime(2023,05,30), PhaId= "P11201001", PatientId= "L198058112" },
            new Prescription { PresNo= "PRE20230530001005", DrugDate=new DateTime(2023,05,30), PhaId= "P11201001", PatientId= "S257920071" }
        );
        modelBuilder.Entity<Dosage>().HasData(
            new Dosage { DosId = "BIAH", Direction = "每天早,晚(飯前)及睡前1次" },
            new Dosage { DosId = "BIDP", Direction= "需要時早,晚(飯後)1次" },
            new Dosage { DosId = "QN", Direction= "每晚1次" },
            new Dosage { DosId = "PRN", Direction= "需要時使用" },
            new Dosage { DosId = "ORDER", Direction= "依照醫師指示" }
        );
        modelBuilder.Entity<Drug>().HasData(
            //參照童綜合醫院心臟相關藥品查詢
            new Drug { DrugId= "046404", Atccode= "C07AG02", Nhicode= "AC46404100", DrugName= "Syntrend", GenericName= "Carvedilol", UnitPrice=240, Dcontent= "25mg/tab", Appearance= "白色圓形錠,Syntrend,S｜Y", ClinicalUses= "高血壓、鬱血性心臟衰竭", SuggestedUsage= "治療的期間︰服用 Carvedilol需長期性的治療。不應突然停止治療而應以星期為間隔逐漸減少治療，此對併有冠狀心臟疾病的病人尤其重要", AdverseReactions= "暈眩、頭痛，通常是輕微的且尤其於治療的初期", WarningPrecautions= "無資料", PointOfHealthEducation= "1.懷孕或計劃懷孕、氣喘或其他肺臟疾病、糖尿病、甲狀腺機能亢進，請先告訴醫師。 2.不要自行改變劑量或停藥。 3.請定時測量脈搏，低於醫師所訂標準請即就醫。 4.服用後可能發生頭暈、低血壓，小心跌倒。 5.糖尿病病人請注意，此藥可能會影響血糖值，掩蓋低血糖現象，應小心監測血糖。", StorageConditions= "室溫保存", OtherInstructions= "隨餐或餐後立即服用", ProcessingMethod= "無資料", Roaid= "PO" },
            new Drug { DrugId= "009554", Atccode= "C01AA05", Nhicode= "BC09554100", DrugName= "Digoxin", GenericName= "Digoxin", UnitPrice=240, Dcontent= "0.25mg/tab", Appearance= "白色圓扁形錠, |D025", ClinicalUses= "心臟衰竭、心律不整", SuggestedUsage= "對於輕度心衰竭的病人，應每天給藥 0.25~0.75mg，共一星期，接著給予一適當的維持劑量，達到毛地黃化的速度可和緩些", AdverseReactions= "疲憊、頭痛、顏面神經痛、精神沮喪、幻覺、困倦、低血壓、視力障礙、厭食、嘔吐、腹瀉、發汗、吞嚥困難", WarningPrecautions= "無資料", PointOfHealthEducation= "1.不要自行改變劑量或停藥。 2.定期量血壓和心跳。 3.服用後可能發生頭暈、低血壓，小心跌倒。 4.定期監測藥物血中濃度與血鉀濃度。 5.服藥期間若發生虛弱無力、心跳太慢，意識混亂等應即刻就醫。 6.避免高麩飲食。", StorageConditions= "室溫陰涼乾燥處保存", OtherInstructions= "無資料", ProcessingMethod= "無資料", Roaid= "PO" },
            new Drug { DrugId= "021757", Atccode= "L04AA06", Nhicode= "BC20999100", DrugName= "Cellcept", GenericName= "Mycophenolate Mofetil", UnitPrice=240, Dcontent= "250mg/cap", Appearance= "橘 | 淺藍膠囊,CellCept 250,Roche", ClinicalUses= "預防或緩解腎臟移植之急性器官排斥、預防心臟和肝臟移植之急性器官排斥", SuggestedUsage= "口服給藥在腎臟、心臟和肝臟移植後應儘速給予 CellCept的起始劑量", AdverseReactions= "便秘、腹瀉、頭痛、噁心、無力等", WarningPrecautions= "整粒吞服, 勿嚼碎；孕婦禁用；服藥後可能嗜睡或眩暈", PointOfHealthEducation= "1.服用本藥會使您抵抗力較弱，請經常洗手並避免接觸有傳染性疾病如感冒的親友。 2.接種任何疫苗之前請先諮詢你的醫生。 3.此藥可能使您皮膚對光較敏感，外出時請做好防曬工作。 4.告訴牙醫、外科醫生和其他醫生您在使用本藥物。 5.不要同時與含有鎂或鋁的制酸劑一起服用。 6.若有高血壓，每日定時測量血壓、脈搏並列入記錄，提供醫師參考。 7.如果膠囊已經打開或破裂，不要接觸內含的藥物。如果已經接觸到藥物或藥物跑進眼睛，請立即洗手或洗眼睛。 8.建議具有生育能力的女性在服用期間及停藥6周內採取有效避孕措施且請勿授乳。有性行為的男性在服用期間及停藥90天內採取有效避孕措施且不應捐獻精液。", StorageConditions= "儲存於30℃以下避光處", OtherInstructions= "空腹時服用。不應膠囊打開或弄碎", ProcessingMethod= "無資料", Roaid= "PO" },
            new Drug { DrugId= "026672", Atccode= "C09DX04", Nhicode= "BC26671100", DrugName= "Entresto", GenericName= "Sacubitril and Valsartan", UnitPrice=240, Dcontent= "100mg/tab", Appearance= "淺黃橢圓形錠,NVR,L1", ClinicalUses= "治療慢性心臟衰竭", SuggestedUsage= "不建議重度肝功能不全患者使用此藥物" , AdverseReactions= "低血壓、高血鉀、頭暈、腎衰竭、血管性水腫等", WarningPrecautions= "孕婦禁用；服藥後可能有姿態性低血壓；", PointOfHealthEducation= "1.預備懷孕、已懷孕或授乳應先告訴醫師。 2.不要自行改變劑量或停藥。 3.定期量血壓和心跳。 4.服用後可能發生頭暈、低血壓，小心跌倒。 5.如長期使用含高鉀的飲食(如低鈉鹽、香蕉、柑橘類高鉀水果），須先請教醫師。", StorageConditions= "儲存於30℃以下避光處", OtherInstructions= "可與食物併服或是空腹服用", ProcessingMethod= "無資料", Roaid= "PO" },
            new Drug { DrugId= "026173", Atccode= "C02KX01", Nhicode= "BC26173100", DrugName= "Tracleer", GenericName= "Bosentan Monohydrate", UnitPrice=240, Dcontent= "62.5mg/tab", Appearance= "橘白色圓凸錠,62.5", ClinicalUses= "治療因先天性心臟病續發WHO Class III 肺動脈高血壓", SuggestedUsage= "腎功能受損者不需要調整劑量。接受血液透析治療之病人，亦無需調整劑量", AdverseReactions= "呼吸道感染、頭痛、水腫、昏厥、低血壓、心悸、貧血等", WarningPrecautions= "無資料", PointOfHealthEducation= "1.除非醫師指示，請勿任意停藥。 2.漏服一劑，請在想起時立即服用，切勿依次服用 2劑藥物。", StorageConditions= "儲存於30℃以下避光處", OtherInstructions= "每日服用1-2次，請於每日固定時間服用", ProcessingMethod= "無資料", Roaid= "PO" }
        );
        modelBuilder.Entity<ChartsDrugsDosage>().HasData(
            new ChartsDrugsDosage { ChaId= "CHA20230530001001", DrugId= "046404", DosId= "ORDER", Days=3, Total=9, Remark="無" },
            new ChartsDrugsDosage { ChaId= "CHA20230530001002", DrugId= "046404", DosId= "ORDER", Days=3, Total=9, Remark="無" },
            new ChartsDrugsDosage { ChaId= "CHA20230530001003", DrugId= "046404", DosId= "ORDER", Days=3, Total=9, Remark="無" },
            new ChartsDrugsDosage { ChaId= "CHA20230530001004", DrugId= "046404", DosId= "ORDER", Days=3, Total=9, Remark="無" },
            new ChartsDrugsDosage { ChaId= "CHA20230530001005", DrugId= "046404", DosId= "ORDER", Days=3, Total=9, Remark="無" }
        );
        modelBuilder.Entity<RoutesOfAdminstration>().HasData(
            new RoutesOfAdminstration { Roaid="PO", BodyParts="口服" }
        );
        modelBuilder.Entity<Detail>().HasData(
            // DET + NOWDATE + 診間號 + (繳費條碼)序號
            new Detail { DetId="DET20230530001001", Registration=150, MedicalCost=500, Payable=650, CasId= "C11201001", PatientId= "A118992634" },
            new Detail { DetId= "DET20230530001002", Registration=150, MedicalCost=500, Payable=650, CasId= "C11201001", PatientId= "O101929955" },
            new Detail { DetId= "DET20230530001003", Registration=150, MedicalCost=500, Payable=650, CasId= "C11201001", PatientId= "H255590997" },
            new Detail { DetId= "DET20230530001004", Registration=150, MedicalCost=500, Payable=650, CasId= "C11201001", PatientId= "L198058112" },
            new Detail { DetId= "DET20230530001005", Registration=150, MedicalCost=500, Payable=650, CasId= "C11201001", PatientId= "S257920071" }
        );

        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
