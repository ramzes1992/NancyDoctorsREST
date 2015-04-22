
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsoleRestClient.Models
{
    public class CommentModel
    {
        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }
    }
}