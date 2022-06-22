using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordVaccine
    {
        public int IdrecordVaccines { get; set; }
        public int? Idrecord { get; set; }
        public int? Nvvaccine { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Record? IdrecordNavigation { get; set; }
        public virtual Namevalue? NvvaccineNavigation { get; set; }
    }
}
