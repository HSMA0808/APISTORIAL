using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordEmergencyEntry
    {
        public int IdrecordInterment { get; set; }
        public string? MedicalCenter { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? Reason { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public List<Response_RecordEmergencyEntry> ToResponseList(List<RecordEmergencyEntry> recordsEmergencyEntry)
        {
            var list = new List<Response_RecordEmergencyEntry>();
            if (recordsEmergencyEntry.Count <= 0)
            {
                return new List<Response_RecordEmergencyEntry>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var rI in recordsEmergencyEntry)
                {
                    var medicalCenter = db.MedicalCenters.Find(rI.IdmedicalCenter);
                    list.Add(new Response_RecordEmergencyEntry()
                    {
                        IdrecordInterment = rI.IdrecordEmergencyentry,
                        Reason = rI.Reason,
                        EntryDate = rI.Intermentdate,
                        MedicalCenter = medicalCenter.Description,
                    });
                }
                return list;
            }
        }
    }
}
