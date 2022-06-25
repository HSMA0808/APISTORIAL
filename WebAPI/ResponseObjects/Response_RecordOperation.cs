using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordOperation
    {
        public int IdrecordVisits { get; set; }
        public int? Iddoctor { get; set; }
        public string? Doctor_Nombre { get; set; }
        public string? Operacion { get; set; }
        public DateTime? Fecha_Operacion { get; set; }

        public List<Response_RecordOperation> ToList(List<RecordOperation> recordVisits)
        {
            var list = new List<Response_RecordOperation>();
            if (recordVisits.Count <= 0)
            {
                return new List<Response_RecordOperation>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var ro in recordVisits)
                {
                    var Operacion = db.Operations.Find(ro.Idoperation);
                    var Doctor = db.Doctors.Find(ro.Iddoctor);
                    list.Add(new Response_RecordOperation()
                    {
                        IdrecordVisits = ro.IdrecordOperations,
                        Iddoctor = ro.Iddoctor,
                        Doctor_Nombre = Doctor.FirstName + " " + Doctor.LastName,
                        Operacion = Operacion.Description,
                        Fecha_Operacion = ro.Operationdate
                    });
                }
                return list;
            }
        }
    }
}
