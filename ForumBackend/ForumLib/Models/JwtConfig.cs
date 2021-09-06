using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Models
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string SignKey { get; set; }
    }
}
