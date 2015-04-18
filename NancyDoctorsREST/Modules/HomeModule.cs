using Nancy;
using NancyDoctorsREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyDoctorsREST.Modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {
            Get["/"] = param =>
            {
                return View["Index"];
            };
        } 
    }
}