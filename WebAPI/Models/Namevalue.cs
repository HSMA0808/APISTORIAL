using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Namevalue
    {
        public Namevalue()
        {
            DoctorNvbloodTypeNavigations = new HashSet<Doctor>();
            DoctorNvidentificationTypeNavigations = new HashSet<Doctor>();
            MedicalCenters = new HashSet<MedicalCenter>();
            PatientNvbloodTypeNavigations = new HashSet<Patient>();
            PatientNvidentificationTypeNavigations = new HashSet<Patient>();
            RecordAllergies = new HashSet<RecordAllergy>();
            RecordVaccines = new HashSet<RecordVaccine>();
        }

        public int Idnamevalue { get; set; }
        public int? Idgroup { get; set; }
        public string? Description { get; set; }
        public string? Customstring1 { get; set; }
        public string? Customstring2 { get; set; }
        public int? Customint1 { get; set; }
        public int? Customint2 { get; set; }
        public string? GroupName { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Doctor> DoctorNvbloodTypeNavigations { get; set; }
        public virtual ICollection<Doctor> DoctorNvidentificationTypeNavigations { get; set; }
        public virtual ICollection<MedicalCenter> MedicalCenters { get; set; }
        public virtual ICollection<Patient> PatientNvbloodTypeNavigations { get; set; }
        public virtual ICollection<Patient> PatientNvidentificationTypeNavigations { get; set; }
        public virtual ICollection<RecordAllergy> RecordAllergies { get; set; }
        public virtual ICollection<RecordVaccine> RecordVaccines { get; set; }
    }
}
