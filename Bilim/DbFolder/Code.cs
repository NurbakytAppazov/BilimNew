using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.DbFolder
{
    public class Code
    {
        public int Id { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }


        public int code { get; set; }
    }
}
