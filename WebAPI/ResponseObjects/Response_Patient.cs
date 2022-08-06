using WebAPI.Models;
namespace WebAPI.ResponseObjects
{
    public class Response_Patient
    {
        public int Idpatient { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public string? TipoIdentificacion { get; set; }
        public int? nvIdTipoIdentificacion { get; set; }
        public string? Codigo_TipoIdentificacion { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? Direccion1 { get; set; }
        public string? Direccion2 { get; set; }
        public int? nvIdTipoSangre { get; set; }
        public string? TipoSangre { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Email { get; set; }

        public List<Response_Patient> ToResponseList(List<Patient> patientList)
        {
            var listaPaciente = new List<Response_Patient>();
            foreach (var patient in patientList)
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                var nameValueTipoIdentificacion = db.Namevalues.Where(nv => nv.GroupName == "IDENTIFICATIONTYPE_GROUP" && nv.Idnamevalue == patient.NvidentificationType).ToList()[0];
                var codigo_TipoIdentificacion = nameValueTipoIdentificacion.Customstring1;
                var tipoIdentificacion = nameValueTipoIdentificacion.Description;
                var TipoSangre = db.Namevalues.Where(nv => nv.GroupName == "BLOODTYPE_GROUP" && nv.Idnamevalue == patient.NvbloodType).ToList()[0].Customstring1;
                listaPaciente.Add(new Response_Patient()
                {
                    Idpatient = patient.Idpatient,
                    PrimerNombre = patient.FirstName,
                    SegundoNombre = patient.MiddleName,
                    Apellidos = patient.LastName,
                    Sexo = patient.Sex,
                    nvIdTipoIdentificacion = patient.NvidentificationType,
                    Codigo_TipoIdentificacion = codigo_TipoIdentificacion,
                    TipoIdentificacion = tipoIdentificacion,
                    NumeroIdentificacion = patient.IdentificationNumber,
                    Direccion1 = patient.Address1,
                    Direccion2 = patient.Address2,
                    nvIdTipoSangre = patient.NvbloodType,
                    TipoSangre = TipoSangre,
                    Telefono1 = patient.Tel1,
                    Telefono2 = patient.Tel2,
                    Email = patient.Email
                });
            }
            return listaPaciente;
        }

        public Response_Patient ToResponseEntity(Patient patient)
        {
            var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
            var codigo_TipoIdentificacion = db.Namevalues.Where(nv => nv.GroupName == "IDENTIFICATIONTYPE_GROUP" && nv.Idnamevalue == patient.NvidentificationType).ToList()[0].Customstring1;
            var TipoSangre = db.Namevalues.Where(nv => nv.GroupName == "BLOODTYPE_GROUP" && nv.Idnamevalue == patient.NvbloodType).ToList()[0].Customstring1;
            var paciente = new Response_Patient()
            {
                Idpatient = patient.Idpatient,
                PrimerNombre = patient.FirstName,
                SegundoNombre = patient.MiddleName,
                Apellidos = patient.LastName,
                Sexo = patient.Sex,
                nvIdTipoIdentificacion = patient.NvidentificationType,
                Codigo_TipoIdentificacion = codigo_TipoIdentificacion,
                NumeroIdentificacion = patient.IdentificationNumber,
                Direccion1 = patient.Address1,
                Direccion2 = patient.Address2,
                nvIdTipoSangre = patient.NvbloodType,
                TipoSangre = TipoSangre,
                Telefono1 = patient.Tel1,
                Telefono2 = patient.Tel2,
                Email = patient.Email
            };
            return paciente;
        }
    }

}
