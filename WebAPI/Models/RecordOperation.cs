using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RecordOperation
    {
        public int IdrecordOperations { get; set; }
        public int? Idrecord { get; set; }
        public int? Idoperation { get; set; }
        public int? Iddoctor { get; set; }
        public DateTime? Operationdate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Doctor? IddoctorNavigation { get; set; }
        public virtual Operation? IdoperationNavigation { get; set; }
        public virtual Record? IdrecordNavigation { get; set; }
    }
}
