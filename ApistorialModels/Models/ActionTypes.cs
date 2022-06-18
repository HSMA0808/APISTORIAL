using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public abstract class ActionTypes : EntityBase
    {
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
