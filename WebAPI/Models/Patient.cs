using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Records = new HashSet<Record>();
        }

        public int Idpatient { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public int? NvidentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public int? NvbloodType { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Email { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Namevalue? NvbloodTypeNavigation { get; set; }
        public virtual Namevalue? NvidentificationTypeNavigation { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
