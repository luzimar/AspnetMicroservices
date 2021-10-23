using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.ViewModels
{
    public class ResponseViewModelBasic
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
