using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRestClient.Helpers
{
    public static class WebHelper
    {
        public static string Get(string uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = WebMethod.GET.ToString();

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }

        public static string Post(string uri, string postData)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = WebMethod.POST.ToString();

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();

            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }

    public enum WebMethod
    {
        GET = 0,
        POST = 1,
        DELETE = 2,
        PUT = 3,
        HEAD = 4,
        OPTIONS = 5,
        PATCH = 6
    }
}
