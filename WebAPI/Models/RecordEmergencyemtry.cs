using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordEmergencyemtry
    {
        public int IdrecordEmergencyentry { get; set; }
        public int? Idrecord { get; set; }
        public int? IdmedicalCenter { get; set; }
        public DateTime? Intermentdate { get; set; }
        public string? Reason { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual MedicalCenter? IdmedicalCenterNavigation { get; set; }
        public virtual Record? IdrecordNavigation { get; set; }
    }
}
