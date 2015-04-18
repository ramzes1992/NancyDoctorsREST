using Nancy;
using NancyDoctorsREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Modules
{
    public class DoctorsModule : NancyModule
    {
        public DoctorsModule()
        {
            Get["/Doctor/{Id}"] = param =>
            {
                var id = (int)param.Id;

                var model = Data.GetAllDoctorModels().FirstOrDefault(d => d.Id.Equals(id));

                return View["Doctor", model];
            };

            Get["/Doctors"] = param =>
            {
                var model = Data.GetAllDoctorModels();

                return View["DoctorsList", model];
            };
        }
    }
}