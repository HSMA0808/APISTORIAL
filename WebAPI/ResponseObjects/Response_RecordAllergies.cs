using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordAllergies
    {
        public string? Codigo_Vacuna { get; set; }
        public string? Descripcion { get; set; }

        public List<Response_RecordAllergies> ToResponseList(List<RecordAllergy> recordVaccineList)
        {
            var list = new List<Response_RecordAllergies>();
            foreach (var ras in recordVaccineList)
            {
                list.Add(new Response_RecordAllergies()
                {
                    Codigo_Vacuna = ras.NvallergieNavigation.Customstring1,
                    Descripcion = ras.NvallergieNavigation.Description
                });
            }
            return list;
        }
    }
}
