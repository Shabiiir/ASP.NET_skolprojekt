using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication
{
    public class USerEntities
    {
        public int ID { get; set; }
        public string Användarnamn { get; set; }
        public string FNamn { get; set; }
        public string ENamn { get; set; }
        public string EMail { get; set; }
        public DateTime Ålder { get; set; }
        public string Kön { get; set; }
        public string Bor { get; set; }
        public string Sysselsättning { get; set; }
        public string OmMig { get; set; }
        public string Bild { get; set; }
        public DateTime Pre { get; set; }
    }

    public class WallEntities
    {
        public int ID { get; set; }
        public int AnvändarID { get; set; }
        public string Meddelande { get; set; }
        public int MottagarID { get; set; }
        public DateTime Tid { get; set; }
    }

    public class MessageEntities
    {
        public int ID { get; set; }
        public int Från { get; set; }
        public string Meddelande { get; set; }
        public int Till { get; set; }
        public int Läst { get; set; }
        public DateTime Tid { get; set; }
    }
}