using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsoleRestClient.Models
{
    public class TimeSlotModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Visitor { get; set; }

    }
}