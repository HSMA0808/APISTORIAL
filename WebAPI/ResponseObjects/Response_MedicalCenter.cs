using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_MedicalCenter
    {
        public int idCentroMedico { get; set; }
        public string? CentroMedico { get; set; }
        public string? Rnc { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public string? NombreContacto { get; set; }
        public int? EstatusCenter { get; set; }
        public string? EstatusCenterDescripcion { get; set; }

        public List<Response_MedicalCenter> ToResponseList(List<MedicalCenter> medicalCenterList)
        {
            var list = new List<Response_MedicalCenter>();
            var estatusDescripcion = string.Empty;
            foreach (var cm in medicalCenterList)
            {
                switch (cm.NvstatusCenter) {
                    case 6: estatusDescripcion = "Autorizado"; break;
                    case 7: estatusDescripcion = "Pendiente"; break;
                    default: estatusDescripcion = "N/A"; break;
                }
                list.Add(new Response_MedicalCenter()
                {
                    idCentroMedico = cm.IdmedicalCenter,
                    CentroMedico = cm.Description,
                    Rnc = cm.Rnc,
                    Telefono1 = cm.Tel1,
                    Telefono2 = cm.Tel2,
                    Email1 = cm.Email1,
                    Email2 = cm.Email2,
                    NombreContacto = cm.NameContact,
                    EstatusCenter = cm.NvstatusCenter,
                    EstatusCenterDescripcion = estatusDescripcion
                });
            }
            return list;
        }
    }
}
