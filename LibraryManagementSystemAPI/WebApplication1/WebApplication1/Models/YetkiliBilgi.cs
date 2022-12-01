using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class YetkiliBilgi
    {
        public int Id { get; set; }
        public int SicilNo { get; set; }
        public string sifre { get; set; }
        public string Role { get; set; }
    }
}