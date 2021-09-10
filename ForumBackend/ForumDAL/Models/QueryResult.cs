using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Models
{
    public class QueryResult<T> where T : class
    {
        public int StatusCode { get; set; }

        public T Result { get; set; }
    }
}
