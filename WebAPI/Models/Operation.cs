using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Operation
    {
        public Operation()
        {
            RecordOperations = new HashSet<RecordOperation>();
        }

        public int Idoperation { get; set; }
        public int? IdoperationType { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual OperationType? IdoperationTypeNavigation { get; set; }
        public virtual ICollection<RecordOperation> RecordOperations { get; set; }
    }
}
