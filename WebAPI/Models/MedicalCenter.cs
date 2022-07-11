using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class MedicalCenter
    {
        public MedicalCenter()
        {
            RecordEmergencyemtries = new HashSet<RecordEmergencyEntry>();
            RecordInterments = new HashSet<RecordInterment>();
            RecordLastMedicalcenterUpdateNavigations = new HashSet<Record>();
            RecordMedicalcenterCreatorNavigations = new HashSet<Record>();
        }

        public int IdmedicalCenter { get; set; }
        public string? Description { get; set; }
        public string? Rnc { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? NameContact { get; set; }
        public int? NvstatusCenter { get; set; }
        public string? Token { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Namevalue? NvstatusCenterNavigation { get; set; }
        public virtual ICollection<RecordEmergencyEntry> RecordEmergencyemtries { get; set; }
        public virtual ICollection<RecordInterment> RecordInterments { get; set; }
        public virtual ICollection<Record> RecordLastMedicalcenterUpdateNavigations { get; set; }
        public virtual ICollection<Record> RecordMedicalcenterCreatorNavigations { get; set; }

        public bool MedicalCenterValid(string MedicalCenterToken)
        {
            bool isValid = false;
            var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
            var mcList = db.MedicalCenters.Where(m => m.Token == MedicalCenterToken && m.NvstatusCenter == 6).ToList();
            if (mcList.Count == 1)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
