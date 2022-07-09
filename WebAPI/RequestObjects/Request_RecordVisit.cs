namespace WebAPI.RequestObjects
{
    public class Request_RecordVisit
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; } 
        public string? Doctor_Identification { get; set; } 
        public string? SpecialtyCode { get; set; }
        public string? Observations { get; set; } 
        public string? Indications { get; set; } 
        public string? VisitDate { get; set; }
    }
}
