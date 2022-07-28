using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordEmergencyEntry
    {
        public int idRecordEmergencyEntries { get; set; }
        public string? centroMedico { get; set; }
        public DateTime? fecha_Entrada { get; set; }
        public string? razon { get; set; }
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
                foreach (var rE in recordsEmergencyEntry)
                {
                    var medicalCenter = db.MedicalCenters.Find(rE.IdmedicalCenter);
                    list.Add(new Response_RecordEmergencyEntry()
                    {
                        idRecordEmergencyEntries = rE.IdrecordEmergencyentry,
                        razon = rE.Reason,
                        fecha_Entrada = rE.Intermentdate,
                        centroMedico = medicalCenter.Description,
                        CreateUser = rE.CreateUser,
                        CreateDate = rE.CreateDate,
                    });
                }
                return list;
            }
        }
    }
}
