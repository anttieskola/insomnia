using insomnia.Domain;
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

    public class RequestPostResponseModel
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public String Error { get; set; }
    }

    public class RequestListViewModel
    {
        public List<RequestModel> Requests { get; set; }
        public int Count { get; set; }
    }
}
