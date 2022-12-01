using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class KutuphaneContext : DbContext
    {

            public KutuphaneContext() : base("KutuphaneConnection2")
            {

            }

            public DbSet<KitapBilgi> Kitaplar { get; set; }
            public DbSet<OduncBilgi> Oduncler { get; set; }
            public DbSet<TurBilgi> Turler { get; set; }
            public DbSet<UyeBilgi> Uyeler { get; set; }
            public DbSet<YetkiliBilgi> Yetkililer { get; set; }
           

    }
}


