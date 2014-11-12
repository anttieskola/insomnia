using insomnia.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
        public HttpResponseMessage Get()
        {
            RequestListModel list = new RequestListModel();
            list.Count = requests.Count;
            list.Requests = requests;
            return Request.CreateResponse(list);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]RequestPostModel req)
        {
            if (req != null)
            {
                if (req.Url != null)
                {
                    requests.Add(new RequestModel { Url = req.Url, Created = DateTime.Now });
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
            }
            return Request.CreateResponse((HttpStatusCode)422); // unprocessable entity
        }
    }
}
