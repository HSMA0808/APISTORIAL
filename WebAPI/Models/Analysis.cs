using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Analysis
    {
        public Analysis()
        {
            RecordAnalyses = new HashSet<RecordAnalysis>();
        }

        public int Idanalysis { get; set; }
        public int? IdanalysisType { get; set; }
        public int? IdresultType { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual AnalysisType? IdanalysisTypeNavigation { get; set; }
        public virtual ResultType? IdresultTypeNavigation { get; set; }
        public virtual ICollection<RecordAnalysis> RecordAnalyses { get; set; }
    }
}
