using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPI;

namespace WebAPI.Models
{
    public partial class APISTORIAL_v1Context : DbContext
    {
        public APISTORIAL_v1Context()
        {
        }

        public APISTORIAL_v1Context(DbContextOptions<APISTORIAL_v1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Analysis> Analyses { get; set; } = null!;
        public virtual DbSet<AnalysisType> AnalysisTypes { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<MedicalCenter> MedicalCenters { get; set; } = null!;
        public virtual DbSet<Namevalue> Namevalues { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<OperationType> OperationTypes { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<RecordAllergy> RecordAllergies { get; set; } = null!;
        public virtual DbSet<RecordAnalysis> RecordAnalyses { get; set; } = null!;
        public virtual DbSet<RecordEmergencyEntry> RecordEmergencyemtries { get; set; } = null!;
        public virtual DbSet<RecordInterment> RecordInterments { get; set; } = null!;
        public virtual DbSet<RecordOperation> RecordOperations { get; set; } = null!;
        public virtual DbSet<RecordVaccine> RecordVaccines { get; set; } = null!;
        public virtual DbSet<RecordVisit> RecordVisits { get; set; } = null!;
        public virtual DbSet<ResultType> ResultTypes { get; set; } = null!;
        public virtual DbSet<Specialty> Specialtys { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(StaticsOperations.getConfiguration().GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.HasKey(e => e.Idanalysis)
                    .HasName("PK__ANALYSIS__7E992C364071AA05");

                entity.ToTable("ANALYSIS");

                entity.Property(e => e.Idanalysis).HasColumnName("IDANALYSIS");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdanalysisType).HasColumnName("IDANALYSIS_TYPE");

                entity.Property(e => e.IdresultType).HasColumnName("IDRESULT_TYPE");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdanalysisTypeNavigation)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.IdanalysisType)
                    .HasConstraintName("FK_IDANALYSISTYPE_ANALYSIS_ANALYSISTYPE");

                entity.HasOne(d => d.IdresultTypeNavigation)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.IdresultType)
                    .HasConstraintName("FK_IDRESULTTYPE_ANALYSIS_RESULTTYPE");
            });

            modelBuilder.Entity<AnalysisType>(entity =>
            {
                entity.HasKey(e => e.IdanalysisType)
                    .HasName("PK__ANALYSIS__3C82E0B49BAF2D53");

                entity.ToTable("ANALYSIS_TYPE");

                entity.Property(e => e.IdanalysisType).HasColumnName("IDANALYSIS_TYPE");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Iddoctor)
                    .HasName("PK__DOCTOR__2D8713F5E8BB847E");

                entity.ToTable("DOCTOR");

                entity.Property(e => e.Iddoctor).HasColumnName("IDDOCTOR");

                entity.Property(e => e.Address1)
                    .HasMaxLength(500)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(75)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.IdentificationNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICATION_NUMBER");

                entity.Property(e => e.Idspecialty).HasColumnName("IDSPECIALTY");

                entity.Property(e => e.LastName)
                    .HasMaxLength(75)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(75)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.NvbloodType).HasColumnName("NVBLOOD_TYPE");

                entity.Property(e => e.NvidentificationType).HasColumnName("NVIDENTIFICATION_TYPE");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasColumnName("SEX");

                entity.Property(e => e.Tel1)
                    .HasMaxLength(15)
                    .HasColumnName("TEL1");

                entity.Property(e => e.Tel2)
                    .HasMaxLength(15)
                    .HasColumnName("TEL2");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdspecialtyNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Idspecialty)
                    .HasConstraintName("FK_IDSPECIALTY_DOCTOR_SPECIALTYS");

                entity.HasOne(d => d.NvbloodTypeNavigation)
                    .WithMany(p => p.DoctorNvbloodTypeNavigations)
                    .HasForeignKey(d => d.NvbloodType)
                    .HasConstraintName("FK_NVBLOOD_DOCTOR_NAMEVALUE");

                entity.HasOne(d => d.NvidentificationTypeNavigation)
                    .WithMany(p => p.DoctorNvidentificationTypeNavigations)
                    .HasForeignKey(d => d.NvidentificationType)
                    .HasConstraintName("FK_IDNAMEVALUE_DOCTOR_NAMEVALUE");
            });

            modelBuilder.Entity<MedicalCenter>(entity =>
            {
                entity.HasKey(e => e.IdmedicalCenter)
                    .HasName("PK__MEDICAL___F0CE7C68D1A700CF");

                entity.ToTable("MEDICAL_CENTER");

                entity.Property(e => e.IdmedicalCenter).HasColumnName("IDMEDICAL_CENTER");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Email1)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL1");

                entity.Property(e => e.Email2)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL2");

                entity.Property(e => e.NameContact)
                    .HasMaxLength(500)
                    .HasColumnName("NAME_CONTACT");

                entity.Property(e => e.NvstatusCenter).HasColumnName("NVSTATUS_CENTER");

                entity.Property(e => e.Rnc)
                    .HasMaxLength(25)
                    .HasColumnName("RNC");

                entity.Property(e => e.Tel1)
                    .HasMaxLength(15)
                    .HasColumnName("TEL1");

                entity.Property(e => e.Tel2)
                    .HasMaxLength(15)
                    .HasColumnName("TEL2");

                entity.Property(e => e.Token)
                    .HasMaxLength(150)
                    .HasColumnName("TOKEN");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.NvstatusCenterNavigation)
                    .WithMany(p => p.MedicalCenters)
                    .HasForeignKey(d => d.NvstatusCenter)
                    .HasConstraintName("FK_NVSTATUSCENTER_MEDICALCENTER_NAMEVALUE");
            });

            modelBuilder.Entity<Namevalue>(entity =>
            {
                entity.HasKey(e => e.Idnamevalue)
                    .HasName("PK__NAMEVALU__DD427A27880EC46A");

                entity.ToTable("NAMEVALUE");

                entity.Property(e => e.Idnamevalue).HasColumnName("IDNAMEVALUE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Customint1).HasColumnName("CUSTOMINT1");

                entity.Property(e => e.Customint2).HasColumnName("CUSTOMINT2");

                entity.Property(e => e.Customstring1)
                    .HasMaxLength(75)
                    .HasColumnName("CUSTOMSTRING1");

                entity.Property(e => e.Customstring2)
                    .HasMaxLength(75)
                    .HasColumnName("CUSTOMSTRING2");

                entity.Property(e => e.Description)
                    .HasMaxLength(75)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(150)
                    .HasColumnName("GROUP_NAME");

                entity.Property(e => e.Idgroup).HasColumnName("IDGROUP");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Idoperation)
                    .HasName("PK__OPERATIO__7C5ADBF09962FD1A");

                entity.ToTable("OPERATION");

                entity.Property(e => e.Idoperation).HasColumnName("IDOPERATION");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdoperationType).HasColumnName("IDOPERATION_TYPE");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdoperationTypeNavigation)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.IdoperationType)
                    .HasConstraintName("FK_IDOPERATIONTYPE_OPERATION_OPERATIONTYPE");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.HasKey(e => e.IdoperationType)
                    .HasName("PK__OPERATIO__F28A95BB970764EF");

                entity.ToTable("OPERATION_TYPE");

                entity.Property(e => e.IdoperationType).HasColumnName("IDOPERATION_TYPE");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Idpatient)
                    .HasName("PK__PATIENT__C603C53AC61DC76F");

                entity.ToTable("PATIENT");

                entity.Property(e => e.Idpatient).HasColumnName("IDPATIENT");

                entity.Property(e => e.Address1)
                    .HasMaxLength(500)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(75)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.IdentificationNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICATION_NUMBER");

                entity.Property(e => e.LastName)
                    .HasMaxLength(75)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(75)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.NvbloodType).HasColumnName("NVBLOOD_TYPE");

                entity.Property(e => e.NvidentificationType).HasColumnName("NVIDENTIFICATION_TYPE");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasColumnName("SEX");

                entity.Property(e => e.Tel1)
                    .HasMaxLength(15)
                    .HasColumnName("TEL1");

                entity.Property(e => e.Tel2)
                    .HasMaxLength(15)
                    .HasColumnName("TEL2");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.NvbloodTypeNavigation)
                    .WithMany(p => p.PatientNvbloodTypeNavigations)
                    .HasForeignKey(d => d.NvbloodType)
                    .HasConstraintName("FK_NVBLOOD_PATIENT_NAMEVALUE");

                entity.HasOne(d => d.NvidentificationTypeNavigation)
                    .WithMany(p => p.PatientNvidentificationTypeNavigations)
                    .HasForeignKey(d => d.NvidentificationType)
                    .HasConstraintName("FK_IDNAMEVALUE_PATIENT_NAMEVALUE");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.Idrecord)
                    .HasName("PK__RECORD__7A77D9E3EAEB66EA");

                entity.ToTable("RECORD");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Idpatient).HasColumnName("IDPATIENT");

                entity.Property(e => e.LastMedicalcenterUpdate).HasColumnName("LAST_MEDICALCENTER_UPDATE");

                entity.Property(e => e.MedicalcenterCreator).HasColumnName("MEDICALCENTER_CREATOR");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdpatientNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.Idpatient)
                    .HasConstraintName("FK_IDPATIENT_RECORD_PATIENT");

                entity.HasOne(d => d.LastMedicalcenterUpdateNavigation)
                    .WithMany(p => p.RecordLastMedicalcenterUpdateNavigations)
                    .HasForeignKey(d => d.LastMedicalcenterUpdate)
                    .HasConstraintName("FK_LAST_MEDICALCENTERUPDATE_RECORD_MEDICALCENTER");

                entity.HasOne(d => d.MedicalcenterCreatorNavigation)
                    .WithMany(p => p.RecordMedicalcenterCreatorNavigations)
                    .HasForeignKey(d => d.MedicalcenterCreator)
                    .HasConstraintName("FK_LAST_MEDICALCENTERCREATOR_RECORD_MEDICALCENTER");
            });

            modelBuilder.Entity<RecordAllergy>(entity =>
            {
                entity.HasKey(e => e.IdrecordAllergies)
                    .HasName("PK__RECORD_A__5BDFE41C7FACD919");

                entity.ToTable("RECORD_ALLERGIES");

                entity.Property(e => e.IdrecordAllergies).HasColumnName("IDRECORD_ALLERGIES");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Nvallergie).HasColumnName("NVALLERGIE");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordAllergies)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDALLERGIES_RECORD");

                entity.HasOne(d => d.NvallergieNavigation)
                    .WithMany(p => p.RecordAllergies)
                    .HasForeignKey(d => d.Nvallergie)
                    .HasConstraintName("FK_NVALLERGIE_RECORDALLERGIES_NAMEVALUE");
            });

            modelBuilder.Entity<RecordAnalysis>(entity =>
            {
                entity.HasKey(e => e.IdrecordAnalysis)
                    .HasName("PK__RECORD_A__35B5EB3ABA2C2516");

                entity.ToTable("RECORD_ANALYSIS");

                entity.Property(e => e.IdrecordAnalysis).HasColumnName("IDRECORD_ANALYSIS");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Idanalysis).HasColumnName("IDANALYSIS");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.PublicResults).HasColumnName("PUBLIC_RESULTS");

                entity.Property(e => e.ResultsObservations)
                    .HasMaxLength(500)
                    .HasColumnName("RESULTSOBSERVATIONS");

                entity.Property(e => e.Result)
                    .HasMaxLength(500)
                    .HasColumnName("RESULT");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdanalysisNavigation)
                    .WithMany(p => p.RecordAnalyses)
                    .HasForeignKey(d => d.Idanalysis)
                    .HasConstraintName("FK_IDANALYSIS_RECORDANALYSIS_ANALYSIS");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordAnalyses)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDANALYSIS_RECORD");
            });

            modelBuilder.Entity<RecordEmergencyEntry>(entity =>
            {
                entity.HasKey(e => e.IdrecordEmergencyentry)
                    .HasName("PK__RECORD_E__46A2FABA696D0035");

                entity.ToTable("RECORD_EMERGENCYEMTRY");

                entity.Property(e => e.IdrecordEmergencyentry).HasColumnName("IDRECORD_EMERGENCYENTRY");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.IdmedicalCenter).HasColumnName("IDMEDICAL_CENTER");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Intermentdate)
                    .HasColumnType("datetime")
                    .HasColumnName("INTERMENTDATE");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("REASON");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdmedicalCenterNavigation)
                    .WithMany(p => p.RecordEmergencyemtries)
                    .HasForeignKey(d => d.IdmedicalCenter)
                    .HasConstraintName("FK_IDMEDICALCENTER_RECORDEMERGENCYENTRY_MEDICALCENTER");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordEmergencyemtries)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDEMERGENCYENTRY_RECORD");
            });

            modelBuilder.Entity<RecordInterment>(entity =>
            {
                entity.HasKey(e => e.IdrecordInterment)
                    .HasName("PK__RECORD_I__2573BFAAAD6DD927");

                entity.ToTable("RECORD_INTERMENTS");

                entity.Property(e => e.IdrecordInterment).HasColumnName("IDRECORD_INTERMENT");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.IdmedicalCenter).HasColumnName("IDMEDICAL_CENTER");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Intermentdate)
                    .HasColumnType("datetime")
                    .HasColumnName("INTERMENTDATE");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("REASON");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdmedicalCenterNavigation)
                    .WithMany(p => p.RecordInterments)
                    .HasForeignKey(d => d.IdmedicalCenter)
                    .HasConstraintName("FK_IDMEDICALCENTER_RECORDINTERMENTS_MEDICALCENTER");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordInterments)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDINTERMENTS_RECORD");
            });

            modelBuilder.Entity<RecordOperation>(entity =>
            {
                entity.HasKey(e => e.IdrecordOperations)
                    .HasName("PK__RECORD_O__D9E344D802994A63");

                entity.ToTable("RECORD_OPERATIONS");

                entity.Property(e => e.IdrecordOperations).HasColumnName("IDRECORD_OPERATIONS");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Iddoctor).HasColumnName("IDDOCTOR");

                entity.Property(e => e.Idoperation).HasColumnName("IDOPERATION");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Operationdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OPERATIONDATE");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IddoctorNavigation)
                    .WithMany(p => p.RecordOperations)
                    .HasForeignKey(d => d.Iddoctor)
                    .HasConstraintName("FK_IDDOCTOR_RECORDOPERATION_DOCTOR");

                entity.HasOne(d => d.IdoperationNavigation)
                    .WithMany(p => p.RecordOperations)
                    .HasForeignKey(d => d.Idoperation)
                    .HasConstraintName("FK_IDOPERATION_RECORDOPERATION_OPERATION");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordOperations)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDOPERATIONS_RECORD");
            });

            modelBuilder.Entity<RecordVaccine>(entity =>
            {
                entity.HasKey(e => e.IdrecordVaccines)
                    .HasName("PK__RECORD_V__F1A095B5AB1ED86E");

                entity.ToTable("RECORD_VACCINES");

                entity.Property(e => e.IdrecordVaccines).HasColumnName("IDRECORD_VACCINES");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Nvvaccine).HasColumnName("NVVACCINE");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordVaccines)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDVACCINES_RECORD");

                entity.HasOne(d => d.NvvaccineNavigation)
                    .WithMany(p => p.RecordVaccines)
                    .HasForeignKey(d => d.Nvvaccine)
                    .HasConstraintName("FK_NVVACCINE_RECORDVACCINE_NAMEVALUE");
            });

            modelBuilder.Entity<RecordVisit>(entity =>
            {
                entity.HasKey(e => e.IdrecordVisits)
                    .HasName("PK__RECORD_V__AF4F9F26FA6E03BC");

                entity.ToTable("RECORD_VISITS");

                entity.Property(e => e.IdrecordVisits).HasColumnName("IDRECORD_VISITS");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Iddoctor).HasColumnName("IDDOCTOR");

                entity.Property(e => e.Idrecord).HasColumnName("IDRECORD");

                entity.Property(e => e.Idspecialty).HasColumnName("IDSPECIALTY");

                entity.Property(e => e.Indications)
                    .HasMaxLength(500)
                    .HasColumnName("INDICATIONS");

                entity.Property(e => e.Observations)
                    .HasMaxLength(500)
                    .HasColumnName("OBSERVATIONS");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");

                entity.Property(e => e.VisitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("VISIT_DATE");

                entity.HasOne(d => d.IddoctorNavigation)
                    .WithMany(p => p.RecordVisits)
                    .HasForeignKey(d => d.Iddoctor)
                    .HasConstraintName("FK_IDOCTOR_RECORDVISITS_DOCTOR");

                entity.HasOne(d => d.IdrecordNavigation)
                    .WithMany(p => p.RecordVisits)
                    .HasForeignKey(d => d.Idrecord)
                    .HasConstraintName("FK_LAST_IDRECORD_RECORDVISITS_RECORD");

                entity.HasOne(d => d.IdspecialtyNavigation)
                    .WithMany(p => p.RecordVisits)
                    .HasForeignKey(d => d.Idspecialty)
                    .HasConstraintName("FK_IDSPECIALTY_RECORDVISITS_SPECIALTYS");
            });

            modelBuilder.Entity<ResultType>(entity =>
            {
                entity.HasKey(e => e.IdresultType)
                    .HasName("PK__RESULT_T__E06C17ACA867186C");

                entity.ToTable("RESULT_TYPE");

                entity.Property(e => e.IdresultType).HasColumnName("IDRESULT_TYPE");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => e.Idspecialty)
                    .HasName("PK__SPECIALT__F49F691DF55C9810");

                entity.ToTable("SPECIALTYS");

                entity.Property(e => e.Idspecialty).HasColumnName("IDSPECIALTY");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("CODE");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .HasColumnName("UPDATE_USER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
