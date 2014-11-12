using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insomnia.Api.Models
{
    public class RequestPostModel
    {
        public String Url { get; set; }
    }

    public class RequestModel
    {
        public String Url { get; set; }
        public DateTime Created { get; set; }
    }

    public class RequestListModel
    {
        public List<RequestModel> Requests { get; set; }
        public int Count { get; set; }
    }
}