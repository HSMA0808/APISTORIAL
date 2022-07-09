namespace WebAPI.RequestObjects
{
    public class Request_RecordVaccines
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public List<VaccineCode> VaccineCodes { get; set; }

        public class VaccineCode
        { 
            public static string? Code { get; set; }
        }
    }
}
