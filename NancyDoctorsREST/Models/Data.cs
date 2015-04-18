using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Models
{
    public static class Data
    {
        public static IEnumerable<DoctorModel> GetAllDoctorModels()
        {
            return new List<DoctorModel>()
            {
                new DoctorModel(){
                    Id = 1,
                    FirstName = "Adrian",
                    LastName = "Karalus",
                    Specialization = "Neurology",
                    City = "Poznań"
                },
                new DoctorModel(){
                    Id = 2,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Specialization = "Cardiology",
                    City = "Warszwa"
                },
                new DoctorModel(){
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Nowak",
                    Specialization = "Cardiology",
                    City = "Wrocław"
                },

            };
        }
    }
}