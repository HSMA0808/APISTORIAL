using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordAnalysis
    {
        public int IdrecordAnalysis { get; set; }
        public int? Idrecord { get; set; }
        public int? Idanalysis { get; set; }
        public bool? PublicResults { get; set; }
        public string? Results { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Analysis? IdanalysisNavigation { get; set; }
        public virtual Record? IdrecordNavigation { get; set; }
    }
}
