using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class OduncBilgi
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string TeslimEdilecekTarih { get; set; }
        public string TeslimTarihi { get; set; }
        public string AlinanTarih { get; set; }

        public virtual KitapBilgi Kitap { get; set; }
        public virtual UyeBilgi Uye { get; set; }
    }
}