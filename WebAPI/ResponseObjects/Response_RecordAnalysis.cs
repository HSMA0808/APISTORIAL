using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordAnalysis
    {
        public int IdrecordAnalysis { get; set; }
        public string? NombreAnalisis { get; set; }
        public string? CodigoAnalisis { get; set; }
        public string TipoAnalisis { get; set; }
        public string Codigo_TipoAnalisis { get; set; }
        public bool? ResultadosPublicos { get; set; }
        public string? Resultados { get; set; }
        public string? Resultados_Observaciones { get; set; }
        public DateTime? FechaAnalisis { get; set; }
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
                foreach (var rA in recordsAnalysis)
                {
                    analysis = db.Analyses.Where(a=>a.Idanalysis == rA.Idanalysis).Include(ta=>ta.IdanalysisTypeNavigation).ToList()[0];
                    list.Add(new Response_RecordAnalysis()
                    {
                        IdrecordAnalysis = rA.IdrecordAnalysis,
                        NombreAnalisis = analysis.Description,
                        CodigoAnalisis = analysis.Code,
                        TipoAnalisis = analysis.IdanalysisTypeNavigation.Description,
                        Codigo_TipoAnalisis = analysis.IdanalysisTypeNavigation.Code,
                        Resultados = rA.Result,
                        Resultados_Observaciones = rA.ResultsObservations,
                        FechaAnalisis = rA.AnalysisDate,
                        CreateUser = rA.CreateUser,
                        CreateDate = rA.CreateDate,
                    });
                }
                return list;
            }
        }
    }
}
