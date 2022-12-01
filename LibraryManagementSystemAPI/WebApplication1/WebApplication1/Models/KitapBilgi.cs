using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class KitapBilgi
    {
        public int Id { get; set; }
        public string KitapAdi { get; set; }
        public string KitapYazari { get; set; }
        public int BaskiYil { get; set; }
        public string YayinEvi { get; set; }
        public int StokDurumu { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string Acıklama{ get; set; }

        public int TurID { get; set; }
        public TurBilgi TurBilgi { get; set; }
    }
}