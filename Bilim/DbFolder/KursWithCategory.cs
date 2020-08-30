using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.DbFolder
{
    public class KursWithCategory
    {
        public int Id { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
