using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class TurBilgi
    {
        public int ID { get; set; }
        public string TurAdi { get; set; }

        public virtual List<KitapBilgi> Kitaplar { get; set; }
       
    }
}