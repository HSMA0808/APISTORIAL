﻿namespace WebAPI.RequestObjects
{
    public class Request_RecordVaccines
    {
        public int? idRecord { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public List<Codigo_Vacuna> Codigos_Vacunas { get; set; }

        public class Codigo_Vacuna
        { 
            public string? Codigo { get; set; }
        }
    }
}
