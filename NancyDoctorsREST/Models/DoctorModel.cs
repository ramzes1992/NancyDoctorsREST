using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Models
{

    public class DoctorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Specialization { get; set; }

        public string City { get; set; }

        public List<CommentModel> Comments { get; set; }

        public List<TimeSlotModel> TimeSlots { get; set; }

    }
}