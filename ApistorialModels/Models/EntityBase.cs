using System;
using System.Collections.Generic;
using System.Text;

namespace ApistorialModels.Models
{
    public abstract class EntityBase
    {
        public int ID { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
