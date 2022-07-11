using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Record
    {
        public Record()
        {
            RecordAllergies = new HashSet<RecordAllergy>();
            RecordAnalyses = new HashSet<RecordAnalysis>();
            RecordEmergencyemtries = new HashSet<RecordEmergencyEntry>();
            RecordInterments = new HashSet<RecordInterment>();
            RecordOperations = new HashSet<RecordOperation>();
            RecordVaccines = new HashSet<RecordVaccine>();
            RecordVisits = new HashSet<RecordVisit>();
        }

        public int Idrecord { get; set; }
        public int? Idpatient { get; set; }
        public int? MedicalcenterCreator { get; set; }
        public int? LastMedicalcenterUpdate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Patient? IdpatientNavigation { get; set; }
        public virtual MedicalCenter? LastMedicalcenterUpdateNavigation { get; set; }
        public virtual MedicalCenter? MedicalcenterCreatorNavigation { get; set; }
        public virtual ICollection<RecordAllergy> RecordAllergies { get; set; }
        public virtual ICollection<RecordAnalysis> RecordAnalyses { get; set; }
        public virtual ICollection<RecordEmergencyEntry> RecordEmergencyemtries { get; set; }
        public virtual ICollection<RecordInterment> RecordInterments { get; set; }
        public virtual ICollection<RecordOperation> RecordOperations { get; set; }
        public virtual ICollection<RecordVaccine> RecordVaccines { get; set; }
        public virtual ICollection<RecordVisit> RecordVisits { get; set; }

        public Record ReviewRecord(string Identification, string MedicalCenterToken)
        {
            var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
            var idPatient = db.Patients.Where(p => p.IdentificationNumber == Identification).First().Idpatient;
            var medicalCenter = db.MedicalCenters.Where(m => m.Token == MedicalCenterToken).First();
            if (db.Records.Where(r => r.Idpatient == idPatient).ToList().Count == 0)
            {
                db.Records.Add(new Record() {
                    Idpatient = idPatient,
                    MedicalcenterCreator = medicalCenter.IdmedicalCenter,
                    CreateUser = medicalCenter.Description,
                    CreateDate = DateTime.Now,
                });
                db.SaveChanges();
            }
            return db.Records.Where(r=>r.Idpatient == idPatient).First();
        }
    }
}
