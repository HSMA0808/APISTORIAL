using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Doctors = new HashSet<Doctor>();
            RecordVisits = new HashSet<RecordVisit>();
        }

        public int Idspecialty { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<RecordVisit> RecordVisits { get; set; }
    }
}
