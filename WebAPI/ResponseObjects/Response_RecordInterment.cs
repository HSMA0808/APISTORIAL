using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordInterment
    {
        public int IdrecordInterment { get; set; }
        public int idCentroMedico { get; set; }
        public string? centroMedico { get; set; }
        public DateTime? fecha_Internamiento { get; set; }
        public string? razon { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public List<Response_RecordInterment> ToResponseList(List<RecordInterment> recordsInterments)
        {
            var list = new List<Response_RecordInterment>();
            if (recordsInterments.Count <= 0)
            {
                return new List<Response_RecordInterment>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var rI in recordsInterments)
                {
                    var medicalCenter = db.MedicalCenters.Find(rI.IdmedicalCenter);
                    list.Add(new Response_RecordInterment()
                    {
                        IdrecordInterment = rI.IdrecordInterment,
                        razon = rI.Reason,
                        fecha_Internamiento = rI.Intermentdate,
                        idCentroMedico = medicalCenter.IdmedicalCenter,
                        centroMedico = medicalCenter.Description,
                        CreateUser = rI.CreateUser,
                        CreateDate = rI.CreateDate,
                    });
                }
                return list;
            }
        }
    }
}
