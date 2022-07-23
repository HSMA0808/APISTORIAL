namespace WebAPI.RequestObjects
{
    public class Request_RecordAllergies
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public List<Codigo_Alergia> Codigos_Alergias { get; set; }

        public class Codigo_Alergia
        { 
            public string? Codigo { get; set; }
        }
    }
}
