using Nancy;
using Nancy.ModelBinding;
using NancyDoctorsREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyDoctorsREST.Helpers;

namespace NancyDoctorsREST.Modules
{
    public class DoctorsModule : NancyModule
    {
        public DoctorsModule()
        {
            #region cshtml
            Get["/Doctor/{Id:int}"] = param =>
            {
                var id = param.Id;

                var model = Data.GetAllDoctorModels().FirstOrDefault(d => d.Id.Equals(id));

                return View["Doctor", model];
            };

            Get["/Doctors/{Specialization?All}"] =
            Get["/Doctors/{Specialization?All}/{City?}"] = param =>
            {
                var model = Data.GetAllDoctorModels()
                    .Where(d => (param.Specialization.Value.Equals("All")
                             || d.Specialization.Equals(param.Specialization))
                           && (!param.City.HasValue
                             || d.City.Equals(param.City))).ToList();

                return View["DoctorsList", model];
            };

            Post["/Doctor/{Id:int}"] = param =>
            {
                var form = this.Request.Form;

                var model = Data.GetAllDoctorModels()
                    .First(d => d.Id.Equals(param.Id));

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
                var model = Data.GetAllDoctorModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId));

                ViewBag.DoctorId = param.Id;

                return View["TimeSlot", model];
            };

            Post["/Doctor/{Id:int}/TimeSlot/{TimeSlotId:int}"] = param =>
            {
                var form = this.Request.Form;

                Data.GetAllDoctorModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId)).Visitor = form.Visitor;

                return "OK!";
            };
            #endregion

            //JSONs
            #region JSON
            Get["/DoctorJSON/{Id:int}"] = param =>
            {
                var id = param.Id;

                var model = Data.GetAllDoctorModels().FirstOrDefault(d => d.Id.Equals(id));

                return model.ToJson();
            };

            Get["/DoctorsJSON"] = param =>
            {
                var model = Data.GetAllDoctorModels();

                return model.ToJson();
            };
            #endregion

            //XMLs
            #region XML
            Get["/DoctorXML/{Id}"] = param =>
            {
                var id = (int)param.Id;

                var model = Data.GetAllDoctorModels().FirstOrDefault(d => d.Id.Equals(id));

                return model.ToXml();
            };

            Get["/DoctorsXML/{Specialization?All}"] =
            Get["/DoctorsXML/{Specialization?All}/{City?}"] = param =>
            {
                var model = Data.GetAllDoctorModels()
                    .Where(d => (param.Specialization.Value.Equals("All")
                             || d.Specialization.Equals(param.Specialization))
                           && (!param.City.HasValue
                             || d.City.Equals(param.City))).ToList();

                return model.ToXml();
            };

            Post["/DoctorXML/{Id:int}/TimeSlot/{TimeSlotId:int}"] = param =>
            {
                string bodyString = GetBodyString(this.Request.Body);

                Data.GetAllDoctorModels()
                    .First(d => d.Id.Equals(param.Id))
                    .TimeSlots.First(ts => ts.Id.Equals(param.TimeSlotId)).Visitor = bodyString;

                return "OK!";
            };
            #endregion
        }

        private static string GetBodyString(Nancy.IO.RequestStream body)
        {
            int length = (int)body.Length; //this is a dynamic variable.
            byte[] data = new byte[length];
            body.Read(data, 0, length);
            return System.Text.Encoding.Default.GetString(data);
        }
    }
}