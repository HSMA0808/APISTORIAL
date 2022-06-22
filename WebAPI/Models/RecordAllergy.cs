using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordAllergy
    {
        public int IdrecordAllergies { get; set; }
        public int? Idrecord { get; set; }
        public int? Nvallergie { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Record? IdrecordNavigation { get; set; }
        public virtual Namevalue? NvallergieNavigation { get; set; }
    }
}
