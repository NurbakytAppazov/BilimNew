﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilim.DbFolder
{
    public class KursComment
    {
        public int Id { get; set; }


        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }


        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
