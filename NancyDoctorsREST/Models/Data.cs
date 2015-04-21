using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Models
{
    public static class Data
    {
        private static List<DoctorModel> _data =
            new List<DoctorModel>()
            {
                new DoctorModel(){
                    Id = 1,
                    FirstName = "Adrian",
                    LastName = "Karalus",
                    Specialization = "Neurology",
                    City = "Poznań",
                    Comments = new List<CommentModel>(){
                       new CommentModel(){
                           Author = "Adrian",
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Content = "Polecam tego lekarza"
                       },
                    }, 
                },
                new DoctorModel(){
                    Id = 2,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Specialization = "Cardiology",
                    City = "Warszwa",
                    Comments = new List<CommentModel>(){
                       new CommentModel(){
                           Author = "Adrian",
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Content = "Polecam tego lekarza"
                       },
                    }, 
                },
                new DoctorModel(){
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Nowak",
                    Specialization = "Cardiology",
                    City = "Wrocław",
                    Comments = new List<CommentModel>(){
                       new CommentModel(){
                           Author = "Adrian",
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Content = "Polecam tego lekarza"
                       },
                    }, 
                },
            };

        public static IEnumerable<DoctorModel> GetAllDoctorModels()
        {
            return _data;
        }
    }
}