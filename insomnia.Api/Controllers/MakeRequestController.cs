using insomnia.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace insomnia.Api.Controllers
{
    public class MakeRequestController : ApiController
    {
        // TODO: this is just dummy atm.
        public List<RequestModel> requests;

        public MakeRequestController()
        {
            requests = new List<RequestModel>();
        }

        [HttpGet]
        public HttpResponseMessage GetList()
        {
            RequestListModel list = new RequestListModel();
            list.Count = requests.Count;
            list.Requests = requests;
            return Request.CreateResponse(list);
        }

        [HttpPost]
        public HttpResponseMessage PostNew(String url)
        {
            requests.Add(new RequestModel { Url = url, Created = DateTime.Now });
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}
