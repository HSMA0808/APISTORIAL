using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class ResultType
    {
        public ResultType()
        {
            Analyses = new HashSet<Analysis>();
        }

        public int IdresultType { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Analysis> Analyses { get; set; }
    }
}
