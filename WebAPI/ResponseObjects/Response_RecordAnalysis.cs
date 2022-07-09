using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordAnalysis
    {
        public int IdrecordAnalysis { get; set; }
        public string? analysisName { get; set; }
        public string? analysisCode { get; set; }
        public bool? PublicResults { get; set; }
        public string? Result { get; set; }
        public string? ResultsObservations { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public List<Response_RecordAnalysis> ToResponseList(List<RecordAnalysis> recordsAnalysis)
        {
            var list = new List<Response_RecordAnalysis>();
            if (recordsAnalysis.Count <= 0)
            {
                return new List<Response_RecordAnalysis>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                var analysis = new Analysis();
                foreach (var rI in recordsAnalysis)
                {
                    analysis = db.Analyses.Find(rI.Idanalysis);
                    list.Add(new Response_RecordAnalysis()
                    {
                        IdrecordAnalysis = rI.IdrecordAnalysis,
                        analysisName = analysis.Description,
                        analysisCode = analysis.Code,
                        Result = rI.Result,
                        ResultsObservations = rI.ResultsObservations,
                        AnalysisDate = rI.AnalysisDate
                    });
                }
                return list;
            }
        }
    }
}
