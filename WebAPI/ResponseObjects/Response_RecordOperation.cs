using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordOperation
    {
        public int IdrecordOperation { get; set; }
        public int? Iddoctor { get; set; }
        public string? Doctor_Nombre { get; set; }
        public string? Doctor_Identificacion { get; set; }
        public string? Operacion { get; set; }
        public string Codigo_Operacion { get; set; }
        public string TipoOperacion { get; set; }
        public string Codigo_TipoOperacion { get; set; }
        public int idCentroMedico { get; set; }
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
                    var Operacion = db.Operations.Where(o => o.Idoperation == ro.Idoperation).Include(ot => ot.IdoperationTypeNavigation).ToList()[0];
                    var Doctor = db.Doctors.Find(ro.Iddoctor);
                    var centroMedico = db.MedicalCenters.Where(m => m.IdmedicalCenter == ro.IdMedicalCenter).ToList()[0];
                    list.Add(new Response_RecordOperation()
                    {
                        IdrecordOperation = ro.IdrecordOperations,
                        Iddoctor = ro.Iddoctor,
                        Doctor_Nombre = Doctor.FirstName + " " + Doctor.LastName,
                        Doctor_Identificacion = Doctor.IdentificationNumber,
                        Operacion = Operacion.Description,
                        Codigo_Operacion = Operacion.Code,
                        TipoOperacion = Operacion.IdoperationTypeNavigation.Description,
                        Codigo_TipoOperacion = Operacion.IdoperationTypeNavigation.Code,
                        idCentroMedico = centroMedico.IdmedicalCenter,
                        CentroMedico = centroMedico.Description,
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
