﻿
namespace Bilim.DbFolder
{
    public class KursVideo
    {
        public int Id { get; set; }

        public string VideoName { get; set; }
        public string Info { get; set; }
        public string VideoUrl { get; set; }
        public string PhotoUrl { get; set; }
        public bool Free { get; set; } = false;

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
