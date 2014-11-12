using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace insomnia.Domain
{
    public class RequestDaemon
    {
        #region lazy singleton
        private static readonly Lazy<RequestDaemon> lazy =
            new Lazy<RequestDaemon>(() => new RequestDaemon());
        public static RequestDaemon Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private RequestDaemon()
        {
            requests = new List<RequestModel>();
        }
        #endregion

        private List<RequestModel> requests;

        public async Task<int> Create(String url)
        {
            RequestModel rm = new RequestModel { Url = url };
            requests.Add(rm);
            rm.Created = DateTime.Now;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(rm.Url);
                HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();
                rm.StatusCode = (int)res.StatusCode;
            } catch (WebException)
            {
                // server does not exist
                rm.StatusCode = (int)HttpStatusCode.NotFound;
            } catch (UriFormatException)
            {
                // url not valid
                rm.StatusCode = (int)HttpStatusCode.NotFound;
            }
            rm.Completed = DateTime.Now;
            return rm.StatusCode;
        }

        public IEnumerable<RequestModel> List()
        {
            return requests;
        }
    }
}
