using Bilim.DbFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.Models
{
    public class FreeVideoModel
    {
        public List<KursVideo> KursVideos { get; set; }
        public List<FreeVideo> FreeVideos { get; set; }
    }
}
