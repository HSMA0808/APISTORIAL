using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordVisit
    {
        public int IdrecordVisits { get; set; }
        public int? Idrecord { get; set; }
        public int? Iddoctor { get; set; }
        public int? Idspecialty { get; set; }
        public string? Observations { get; set; }
        public string? Indications { get; set; }
        public int? IdMedicalCenter { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Doctor? IddoctorNavigation { get; set; }
        public virtual Record? IdrecordNavigation { get; set; }
        public virtual Specialty? IdspecialtyNavigation { get; set; }
    }
}
