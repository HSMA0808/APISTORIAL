using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordOperation
    {
        public int IdrecordOperation { get; set; }
        public int? Iddoctor { get; set; }
        public string? Doctor_Nombre { get; set; }
        public string? Operacion { get; set; }
        public string? CentroMedico { get; set; }
        public DateTime? Fecha_Operacion { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public List<Response_RecordOperation> ToList(List<RecordOperation> recordOperation)
        {
            var list = new List<Response_RecordOperation>();
            if (recordOperation.Count <= 0)
            {
                return new List<Response_RecordOperation>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var ro in recordOperation)
                {
                    var Operacion = db.Operations.Find(ro.Idoperation);
                    var Doctor = db.Doctors.Find(ro.Iddoctor);
                    var centroMedico = db.MedicalCenters.Where(m => m.IdmedicalCenter == ro.IdMedicalCenter).ToList()[0].Description;
                    list.Add(new Response_RecordOperation()
                    {
                        IdrecordOperation = ro.IdrecordOperations,
                        Iddoctor = ro.Iddoctor,
                        Doctor_Nombre = Doctor.FirstName + " " + Doctor.LastName,
                        Operacion = Operacion.Description,
                        CentroMedico = centroMedico,
                        Fecha_Operacion = ro.Operationdate,
                        CreateUser = ro.CreateUser,
                        CreateDate = ro.CreateDate
                    });
                }
                return list;
            }
        }
    }
}
