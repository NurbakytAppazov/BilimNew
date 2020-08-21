using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.DbFolder
{
    public class FreeVideo
    {
        public int Id { get; set; }

        public string VideoName { get; set; }
        public string Info { get; set; }
        public string VideoUrl { get; set; }
        public string PhotoUrl { get; set; }

        public DateTime CreateDate { get; set; }
        public int ViewCount { get; set; }
    }
}
