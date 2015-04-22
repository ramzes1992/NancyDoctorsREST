using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleRestClient.Helpers;
using ConsoleRestClient.Models;

namespace ConsoleRestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUri = "http://localhost:8000/";

            string city = "Poznań";
            string specialization = "Neurology";

            string visitor = "Adrian Karalus";

            
            //var response = WebHelper.GetWebResponseString(baseUri + "Doctorsxml", WebMethod.GET);
            //var doctors = XmlSerializationHelper.Deserialize<DoctorModel[]>(response);

            var response = WebHelper.Get(baseUri + string.Format("DoctorsXML/{0}/{1}", specialization, city));
            var doctors = XmlSerializationHelper.Deserialize<DoctorModel[]>(response);

            var doctor = doctors.First(d => d.TimeSlots.Any(ts => string.IsNullOrEmpty(ts.Visitor)));
            var freeTimeSlot = doctor.TimeSlots.First(ts => string.IsNullOrEmpty(ts.Visitor));


            var uri = baseUri + string.Format("DoctorXML/{0}/TimeSlot/{1}", doctor.Id, freeTimeSlot.Id);
            var postResponse = WebHelper.Post(uri, visitor);

            Console.ReadKey();
        }

    }
}
