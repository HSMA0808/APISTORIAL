using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordVaccines
    {
        public string? Codigo_Vacuna { get; set; }
        public string? Descripcion { get; set; }

        public List<Response_RecordVaccines> ToResponseList(List<RecordVaccine> recordVaccineList)
        { 
            var list = new List<Response_RecordVaccines>();
            foreach (var rvs in recordVaccineList)
            {
                list.Add(new Response_RecordVaccines()
                {
                    Codigo_Vacuna = rvs.NvvaccineNavigation.Customstring1,
                    Descripcion = rvs.NvvaccineNavigation.Description
                });
            }
            return list;
        }
    }
}
