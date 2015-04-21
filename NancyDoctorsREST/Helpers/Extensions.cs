using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace NancyDoctorsREST.Helpers
{
    public static class Extensions
    {
        public static string ToJson(this object obj)
        {
            return new Nancy.Json.JavaScriptSerializer().Serialize(obj);
        }

        public static string ToXml<T>(this T obj)
        {
            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, obj);
                return stringWriter.ToString();
            }
        }
    }
}