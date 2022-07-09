namespace WebAPI.RequestObjects
{
    public class Request_RecordAnalysis
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public string? AnalysisCode { get; set; }
        public bool? PublicResults { get; set; }
        public string? ResultCode { get; set; }
        public string? ResultsObservations { get; set; }
        public string? AnalysisDate { get; set; }
    }
}
