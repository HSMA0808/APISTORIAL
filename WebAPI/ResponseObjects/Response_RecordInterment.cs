﻿using WebAPI.Models;

namespace WebAPI.ResponseObjects
{
    public class Response_RecordInterment
    {
        public int IdrecordInterment { get; set; }
        public string? MedicalCenter { get; set; }
        public DateTime? Intermentdate { get; set; }
        public string? Reason { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public List<Response_RecordInterment> ToResponseList(List<RecordInterment> recordsInterments)
        {
            var list = new List<Response_RecordInterment>();
            if (recordsInterments.Count <= 0)
            {
                return new List<Response_RecordInterment>();
            }
            else
            {
                var db = new APISTORIAL_v1Context(new Microsoft.EntityFrameworkCore.DbContextOptions<APISTORIAL_v1Context>());
                foreach (var rI in recordsInterments)
                {
                    var medicalCenter = db.MedicalCenters.Find(rI.IdmedicalCenter);
                    list.Add(new Response_RecordInterment()
                    {
                        IdrecordInterment = rI.IdrecordInterment,
                        Reason = rI.Reason,
                        Intermentdate = rI.Intermentdate,
                        MedicalCenter = medicalCenter.Description,
                    });
                }
                return list;
            }
        }
    }
}
