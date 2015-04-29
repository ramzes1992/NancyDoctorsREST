using Nancy;
using Nancy.ModelBinding;
using NancyDoctorsREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyDoctorsREST.Helpers;
using NancyDoctorsModel;

namespace NancyDoctorsREST.Modules
{
    public class DoctorsModule : NancyModule
    {
        private readonly DoctorsRepository _doctorsRepository = new DoctorsRepository();
        private readonly CommentsRepository _commentsRepository = new CommentsRepository();
        private readonly TimeSlotsRepository _timeSlotsRepository = new TimeSlotsRepository();

        public DoctorsModule()
        {
            #region cshtml
            Get["/Doctor/{Id:int}"] = param =>
            {
                var id = param.Id;

                var model = GetDoctorModel(id);//_doctorsRepository.GetAll().FirstOrDefault(d => d.Id.Equals(id));

                return View["Doctor", model];
            };

            Get["/Doctors/{Specialization?All}"] =
            Get["/Doctors/{Specialization?All}/{City?}"] = param =>
            {
                var model = GetDoctorsModels()
                    .Where(d => (param.Specialization.Value.Equals("All")
                             || d.Specialization.Equals(param.Specialization))
                           && (!param.City.HasValue
                             || d.City.Equals(param.City))).ToList();

                return View["DoctorsList", model];
            };

            Post["/Doctor/{Id:int}"] = param =>
            {
                var form = this.Request.Form;

                var model = GetDoctorsModels()
                    .First(d => d.Id.Equals(param.Id));

                _commentsRepository.Add(new Comment()
                {
                    DoctorId = model.Id,
                    Author = form.Author,
                    Date = DateTime.Now,
                    Content = form.Contetn
                });
                _commentsRepository.Save();
                model.Comments.Add(new CommentModel()
                {
                    Author = form.Author,
                    Date = DateTime.Now,
                    Content = form.Contetn
                });

                return "OK!";
            };

            Get["/Doctor/{Id:int}/TimeSlot/{TimeSlotId:int}"] = param =>
            {
                var model = GetDoctorsModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId));

                ViewBag.DoctorId = param.Id;

                return View["TimeSlot", model];
            };

            Post["/Doctor/{Id:int}/TimeSlot/{TimeSlotId:int}"] = param =>
            {
                var form = this.Request.Form;

                GetDoctorsModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId)).Visitor = form.Visitor;

                _timeSlotsRepository.GetAll().First(t => t.Id.Equals(param.TimeSlotId)).Visitor = form.Visitor;
                _timeSlotsRepository.Save();

                return "OK!";
            };
            #endregion

            //JSONs
            #region JSON
            Get["/DoctorJSON/{Id:int}"] = param =>
            {
                var id = param.Id;

                var model = GetDoctorsModels().FirstOrDefault(d => d.Id.Equals(id));

                return model.ToJson();
            };

            Get["/DoctorsJSON"] = param =>
            {
                var model = GetDoctorsModels();

                return model.ToJson();
            };
            #endregion

            //XMLs
            #region XML
            Get["/DoctorXML/{Id}"] = param =>
            {
                var id = (int)param.Id;

                var model = GetDoctorsModels().FirstOrDefault(d => d.Id.Equals(id));

                return model.ToXml();
            };

            Get["/DoctorsXML/{Specialization?All}"] =
            Get["/DoctorsXML/{Specialization?All}/{City?}"] = param =>
            {
                var model = GetDoctorsModels()
                    .Where(d => (param.Specialization.Value.Equals("All")
                             || d.Specialization.Equals(param.Specialization))
                           && (!param.City.HasValue
                             || d.City.Equals(param.City))).ToList();

                return model.ToXml();
            };

            Post["/DoctorXML/{Id:int}/TimeSlot/{TimeSlotId:int}"] = param =>
            {
                string bodyString = GetBodyString(this.Request.Body);

                GetDoctorsModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId)).Visitor = bodyString;

                _timeSlotsRepository.GetAll().First(t => t.Id.Equals(param.TimeSlotId)).Visitor = bodyString;
                _timeSlotsRepository.Save();

                return "OK!";
            };
            #endregion
        }

        #region Data Models

        private DoctorModel GetDoctorModel(int id)
        {
            var doctor =  _doctorsRepository.GetAll().First(d => d.Id.Equals(id));
            return new DoctorModel()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                City = doctor.City,
                //TimeSlots = ToTimeSlotsModels(_doctorsRepository.GetTimeSlots(d)).ToList(),
                //Comments = ToCommentsModels(_doctorsRepository.GetComments(d)).ToList(),
                TimeSlots = GetTimeSlotsModels(doctor.Id).ToList(),
                Comments = GetCommentsModels(doctor.Id).ToList(),
            };
        }

        private IEnumerable<DoctorModel> GetDoctorsModels()
        {
            return _doctorsRepository.GetAll().Select(d => new DoctorModel()
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                City = d.City,
                //TimeSlots = ToTimeSlotsModels(_doctorsRepository.GetTimeSlots(d)).ToList(),
                //Comments = ToCommentsModels(_doctorsRepository.GetComments(d)).ToList(),
                TimeSlots = GetTimeSlotsModels(d.Id).ToList(),
                Comments = GetCommentsModels(d.Id).ToList(),
            });
        }

        private IEnumerable<TimeSlotModel> ToTimeSlotsModels(IEnumerable<TimeSlot> timeSlots)
        {
            return _timeSlotsRepository.GetAll().Select(t => new TimeSlotModel()
            {
                Id = t.Id,
                Date = t.Date,
                Visitor = t.Visitor
            });
        }

        private IEnumerable<CommentModel> ToCommentsModels(IEnumerable<Comment> comments)
        {
            return _commentsRepository.GetAll().Select(c => new CommentModel()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                Date = c.Date,
            });
        }

        private IEnumerable<TimeSlotModel> GetTimeSlotsModels(int doctorId = -1)
        {
            if (doctorId >= 0)
            {
                return _timeSlotsRepository.GetAll()
                    .Where(ts => ts.DoctorId.Equals(doctorId))
                    .Select(t => new TimeSlotModel()
                {
                    Id = t.Id,
                    Date = t.Date,
                    Visitor = t.Visitor
                }); ;
            }

            return _timeSlotsRepository.GetAll().Select(t => new TimeSlotModel()
            {
                Id = t.Id,
                Date = t.Date,
                Visitor = t.Visitor
            });
        }

        private IEnumerable<CommentModel> GetCommentsModels(int doctorId = -1)
        {
            if (doctorId >= 0)
            {
                return _commentsRepository.GetAll()
                    .Where(c => c.DoctorId.Equals(doctorId))
                    .Select(c => new CommentModel()
                    {
                        Id = c.Id,
                        Author = c.Author,
                        Content = c.Content,
                        Date = c.Date,
                    });
            }

            return _commentsRepository.GetAll().Select(c => new CommentModel()
            {
                Id = c.Id,
                Author = c.Author,
                Content = c.Content,
                Date = c.Date,
            });
        }

        #endregion

        private static string GetBodyString(Nancy.IO.RequestStream body)
        {
            int length = (int)body.Length; //this is a dynamic variable.
            byte[] data = new byte[length];
            body.Read(data, 0, length);
            return System.Text.Encoding.Default.GetString(data);
        }
    }
}