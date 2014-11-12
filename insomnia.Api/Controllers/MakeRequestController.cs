using insomnia.Api.Models;
using insomnia.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace insomnia.Api.Controllers
{
    public class MakeRequestController : ApiController
    {
        public const int HttpStatusCodeUnprocessableEntity = 422;

        private IRequestEngine re;

        public MakeRequestController(IRequestEngine e)
        {
            re = e;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<RequestModel> list = re.List();
            RequestListViewModel vm = new RequestListViewModel();
            vm.Count = list.Count();
            vm.Requests = new List<RequestModel>(list.ToArray());
            return Request.CreateResponse(vm);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]RequestPostModel req)
        {
            RequestPostResponseModel m = new RequestPostResponseModel();
            if (req != null)
            {
                if (req.Url != null)
                {
                    int ret = await re.Create(req.Url);
                    m.Success = true;
                    m.StatusCode = ret;
                }
                else
                {
                    m.Success = false;
                    m.Error = "url was not defined";
                }
            }
            else
            {
                m.Success = false;
                m.Error = "no data defined, please define url";
            }
            return Request.CreateResponse(m);
        }
    }
}
