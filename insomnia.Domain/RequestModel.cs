using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insomnia.Domain
{
    public class RequestModel
    {
        public String Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
        public int StatusCode { get; set; }
    }
}
