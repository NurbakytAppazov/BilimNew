﻿using System;

namespace Bilim.DbFolder
{
    public class VideoComment
    {
        public int Id { get; set; }


        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }


        public int KursVideoId { get; set; }
        public KursVideo KursVideo { get; set; }
    }
}
