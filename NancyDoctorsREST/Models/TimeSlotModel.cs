using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Models
{
    public class TimeSlotModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get { return Date.ToString(); } }
        public string Visitor { get; set; }
        public int DoctorId { get; set; }

    }
}