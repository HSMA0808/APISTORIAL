namespace WebAPI.RequestObjects
{
    public class Request_RecordOperation
    {
        public int? idRecord { get; set; } 
        public string? MedicalCenter_Token { get; set; }
        public string? Doctor_Identification { get; set; }
        public string? OperationCode { get; set; }
        public string? OperationDate { get; set; }
    }
}
