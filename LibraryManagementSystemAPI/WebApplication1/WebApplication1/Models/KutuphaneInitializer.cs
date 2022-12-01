using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class KutuphaneInitializer : DropCreateDatabaseIfModelChanges<KutuphaneContext>
    {
        protected override void Seed(KutuphaneContext context)
        {
            List<TurBilgi> tur = new List<TurBilgi>()
            {
                new TurBilgi(){ TurAdi ="Roman"},
                new TurBilgi(){ TurAdi ="Şiir" },
                new TurBilgi(){ TurAdi= "Anı Kitapları" },
                new TurBilgi(){ TurAdi="Gezi " },
                new TurBilgi(){ TurAdi="Biyografi " },
                new TurBilgi(){ TurAdi="Bilim" },
                new TurBilgi(){ TurAdi="Din " },
                new TurBilgi(){ TurAdi="Çocuk " }
            };
            foreach (var item in tur)
            {

                context.Turler.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
