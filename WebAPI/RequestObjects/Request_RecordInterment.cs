namespace WebAPI.RequestObjects
{
    public class Request_RecordInterment
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public string? ReasonInterment { get; set; }
        public string? IntermentDate { get; set; }
    }
}
