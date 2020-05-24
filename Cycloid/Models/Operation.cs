using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycloid.Models
{
    public class Operation<T> where T : class
    {
        public T Payload { get; set; }
        public List<string> ErrorMessages { get; set; }
        public bool IsValid { get; set; }
    }
}
