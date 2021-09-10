using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWebApi.Models
{
    public class LoginInfo
    {
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        [RegularExpression("^.[A-Za-z0-9]+$")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string Pwd { get; set; }
    }
}
