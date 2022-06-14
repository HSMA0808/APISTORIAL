using System;
using System.Collections.Generic;
using System.Text;

namespace ApistorialModels.Models
{
    public class Analysis : EntityBase
    {
        public AnalysisType analysisType { get; set; }
        public ResultType resultType { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

    }
}
