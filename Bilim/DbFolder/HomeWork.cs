using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.DbFolder
{
    public class HomeWork
    {
        public int Id { get; set; }



        public string WorkText { get; set; }
        public string FilePath { get; set; }
        public DateTime Data { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
    }
}
