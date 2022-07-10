using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordVisit
    {
        public int IdrecordVisits { get; set; }
        public int? Iddoctor { get; set; }
        public string? Doctor_Nombre { get; set; }
        public string? Especialidad_Medica { get; set; }
        public string? Observaciones { get; set; }
        public string? Indicaciones { get; set; }
        public string? CentroMedico { get; set; }
        public DateTime? Fecha_Visita { get; set; }

        public List<Response_RecordVisit> ToList(List<RecordVisit> recordVisits)
        {
            var list = new List<Response_RecordVisit>();
            if (recordVisits.Count <= 0)
            {
                return new List<Response_RecordVisit>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var rv in recordVisits)
                {
                    var EspecialidadMedica = db.Specialtys.Find(rv.Idspecialty);
                    var Doctor = db.Doctors.Find(rv.Iddoctor);
                    var centroMedico = db.MedicalCenters.Where(m => m.IdmedicalCenter == rv.IdMedicalCenter).First().Description;
                    list.Add(new Response_RecordVisit()
                    {
                        IdrecordVisits = rv.IdrecordVisits,
                        Iddoctor = rv.Iddoctor,
                        Doctor_Nombre = Doctor.FirstName + " " + Doctor.LastName,
                        Especialidad_Medica = EspecialidadMedica.Description,
                        CentroMedico = centroMedico,
                        Observaciones = rv.Observations,
                        Indicaciones = rv.Indications,
                        Fecha_Visita = rv.VisitDate
                    });
                }
                return list;
            }
        }
    }
}
