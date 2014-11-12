using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insomnia.Domain
{
    public interface IRequestEngine
    {
        Task<int> Create(String url);
        IEnumerable<RequestModel> List();
    }
}
