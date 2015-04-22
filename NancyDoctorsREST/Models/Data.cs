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
                           Date = DateTime.Now,
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Date = DateTime.Now,
                           Content = "Polecam tego lekarza"
                       },
                    },
                    TimeSlots = new List<TimeSlot>() {
                        new TimeSlot(){
                            Id = 1,
                            Date = DateTime.Now,
                        },
                        new TimeSlot(){
                            Id = 2,
                            Date = DateTime.Now.AddHours(1),
                        },
                        new TimeSlot(){
                            Id = 3,
                            Date = DateTime.Now.AddHours(2),
                        },
                        new TimeSlot(){
                            Id = 4,
                            Date = DateTime.Now.AddHours(3),
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
                           Date = DateTime.Now,
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Date = DateTime.Now,
                           Content = "Polecam tego lekarza"
                       },
                    },
                    TimeSlots = new List<TimeSlot>() {
                        new TimeSlot(){
                            Id = 5,
                            Date = DateTime.Now,
                        },
                        new TimeSlot(){
                            Id = 6,
                            Date = DateTime.Now.AddHours(1),
                        },
                        new TimeSlot(){
                            Id = 7,
                            Date = DateTime.Now.AddHours(2),
                        },
                        new TimeSlot(){
                            Id = 8,
                            Date = DateTime.Now.AddHours(3),
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
                           Date = DateTime.Now,
                           Content = "Bardzo fajny lekarz! polecam!"
                       },
                       new CommentModel(){
                           Author = "Bogdan",
                           Date = DateTime.Now,
                           Content = "Polecam tego lekarza"
                       },
                    },
                    TimeSlots = new List<TimeSlot>() {
                        new TimeSlot(){
                            Id = 9,
                            Date = DateTime.Now,
                        },
                        new TimeSlot(){
                            Id = 10,
                            Date = DateTime.Now.AddHours(1),
                        },
                        new TimeSlot(){
                            Id = 11,
                            Date = DateTime.Now.AddHours(2),
                        },
                        new TimeSlot(){
                            Id = 12,
                            Date = DateTime.Now.AddHours(3),
                        },
                    },
                },
            };

        public static List<DoctorModel> GetAllDoctorModels()
        {
            return _data;
        }
    }
}