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
                    Content = form.Contetn
                });

                return "Ok!";
            };

            //JSONs

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

            //XMLs

            Get["/DoctorXML/{Id}"] = param =>
            {
                var id = (int)param.Id;

                var model = Data.GetAllDoctorModels().FirstOrDefault(d => d.Id.Equals(id));

                return model.ToXml();
            };

            Get["/DoctorsXML"] = param =>
            {
                var model = Data.GetAllDoctorModels();

                return model.ToXml();
            };
        }
    }
}