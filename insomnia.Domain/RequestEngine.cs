using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insomnia.Domain
{
    public class RequestEngine : IRequestEngine
    {
        public async Task<int> Create(String url)
        {
            return await RequestDaemon.Instance.Create(url);
        }

        public IEnumerable<RequestModel> List()
        {
            return RequestDaemon.Instance.List();
        }
    }
}
