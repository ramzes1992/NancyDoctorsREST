using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleRestClient.Helpers
{
    public class XmlSerializationHelper
    {
        public static T Deserialize<T>(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            object result;

            using (TextReader reader = new StringReader(xmlString))
            {
                result = serializer.Deserialize(reader);
            }

            return (T)result;
        }
    }
}
