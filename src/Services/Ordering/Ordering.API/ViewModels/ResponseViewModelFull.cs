using Ordering.Domain.Common;
using System.Collections.Generic;

namespace Ordering.API.ViewModels
{
    public class ResponseViewModelFull<T> where T : class
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public List<T> Results { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
