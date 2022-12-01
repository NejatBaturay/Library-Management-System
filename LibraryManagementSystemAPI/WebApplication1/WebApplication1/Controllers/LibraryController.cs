using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using System.Web.WebPages.Html;
using LibraryManagement.Models;
using WebApplication1.Models;
using System.Text.Json;
using Newtonsoft.Json;
using WebApplication1;


namespace LibraryManagement.Controllers
{
    public class LibraryController : ApiController
    {
        KutuphaneContext db = new KutuphaneContext();


        //KitapBilgi ile ilişkili database işlemleri




        [Route("api/GirisYap")]
        public IHttpActionResult PostKitapEkle(YetkiliBilgi yetkiliBilgi)
        {

            return Created("",JWTmanager.GenerateToken(yetkiliBilgi));
        }


    
        [Route("api/KitapEkle")]
        public IHttpActionResult GetKitapEkle()
        {
            List<SelectListItem> dinamik_degerler = (from x2 in db.Turler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x2.TurAdi,
                                                         Value = x2.ID.ToString()
                                                     }
                                                   ).ToList();
        

            return Ok(dinamik_degerler);
        }

        [Route("api/KitapEkle")]
        public IHttpActionResult PostKitapEkle(KitapBilgi kitapBilgi)
        {

            TurBilgi turid = (from tur in db.Turler
                              where tur.ID == kitapBilgi.TurID
                              select tur).FirstOrDefault();

            kitapBilgi.TurBilgi = turid;
           


            db.Kitaplar.Add(kitapBilgi);

            db.SaveChanges();

            return Ok();
        }






        [Route("api/KitapSil/{id}")]
        public IHttpActionResult DeleteKitapSil(int id)
        {

            var silinecek = db.Kitaplar.Where(i => i.Id == id).FirstOrDefault();

            db.Kitaplar.Remove(silinecek);

            db.SaveChanges();

            return Ok();
        }

        [Route("api/KitapGuncelle/{id}")]
        public IHttpActionResult GetKitapGuncelle(int Id)
        {
            List<System.Web.Mvc.SelectListItem> dinamik_degerler = (from x2 in db.Turler.ToList()
                                                     select new System.Web.Mvc.SelectListItem
                                                     {
                                                         Text = x2.TurAdi,
                                                         Value = x2.ID.ToString()
                                                     }
                                                   ).ToList();
            
            var guncellenecek = db.Kitaplar.Where(i=>i.Id == Id).FirstOrDefault();


            TurBilgi turid = (from tur in db.Turler
                              where tur.ID == guncellenecek.TurID
                              select tur).FirstOrDefault();

            guncellenecek.TurBilgi = turid;
           

            UptadeModel uptadeModel = new UptadeModel();

            uptadeModel.kitapBilgi = guncellenecek;
            uptadeModel.dinamikdeger = dinamik_degerler;


            string jsonString = JsonConvert.SerializeObject(uptadeModel,Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            


            return Ok(jsonString); 

        }

        [Route("api/KitapGuncelle")]
        public IHttpActionResult PutKitapGuncelle(KitapBilgi entity)
        {

            TurBilgi turid = (from tur in db.Turler
                              where tur.ID == entity.TurID
                              select tur).FirstOrDefault();

            var vericek = db.Kitaplar.Where(i=>i.Id == entity.Id).FirstOrDefault();
            vericek.KitapAdi = entity.KitapAdi;
            vericek.TurID = entity.TurID;
            vericek.KitapYazari = entity.KitapYazari;
            vericek.StokDurumu = entity.StokDurumu;
            vericek.YayinEvi = entity.YayinEvi;
            vericek.TurBilgi = turid;

            db.SaveChanges();
            return Ok();
        }

        
        
        [Route("api/KitapList")]
        public IHttpActionResult GetKitapList()
        {

            return Ok(db.Kitaplar.ToList());
        }




        [Route("api/KitapAraListele/{searchstring}")]
        public IHttpActionResult GetKitapList(string searchstring)
        {


            return Ok(db.Kitaplar.Where(i => i.KitapAdi.Contains(searchstring)));        


        }






        //-------------------------------------------------


        //Odunc ile ilgili database işlemleri




        [Route("api/UyeEkle")]
        public IHttpActionResult PostUyeEkle(UyeBilgi uyeBilgi)
        {

            db.Uyeler.Add(uyeBilgi);

            db.SaveChanges();

            return Ok();
        }



        [Route("api/UyeSil/{id}")]
        public IHttpActionResult DeleteUye(int id)
        {

            var silinecek = db.Uyeler.Where(i => i.Id == id).FirstOrDefault();

            db.Uyeler.Remove(silinecek);

            db.SaveChanges();

            return Ok();
        }






        [Route("api/UyeGuncelle/{id}")]
        public IHttpActionResult GetUyeGuncelle(int Id)
        {




            UyeBilgi uyeBilgi = new UyeBilgi();

            var guncellenecek = db.Uyeler.Where(i => i.Id == Id).FirstOrDefault();


            uyeBilgi = guncellenecek;



            string jsonString = JsonConvert.SerializeObject(guncellenecek);
                


            return Ok(jsonString);


        }




        [Route("api/UyeGuncelle")]
        public IHttpActionResult PutUyeGuncelle(UyeBilgi entity)
        {


            var vericek = db.Uyeler.Find(entity.Id);
            vericek = db.Uyeler.Find(entity.Id);
            vericek.SicilNo = entity.SicilNo;
            vericek.TelefonNo = entity.TelefonNo;
            vericek.UyeAdi = entity.UyeAdi;
            vericek.UyeSoyadi = entity.UyeSoyadi;
            vericek.Cinsiyet = entity.Cinsiyet;
            vericek.Email = entity.Email;

            db.SaveChanges();


            return Ok();

        }

        







        [Route("api/UyeList")]
        public IHttpActionResult GetUyeList()
        {

            return Ok(db.Uyeler.ToList());

        }


        

        [Route("api/UyeAraListSicilNo/{SicilNo}")]
        public IHttpActionResult GetUyeList(int SicilNo)
        {


            return Ok(db.Uyeler.Where(i => i.SicilNo == SicilNo).ToList());        


        }

        [Route("api/UyeAraListSearchString/{searchstring}")]
        public IHttpActionResult GetUyeList(string searchstring)
        {


            return Ok(db.Uyeler.Where(i => i.UyeAdi.Contains(searchstring)).ToList());


        }
    
        [Route("api/UyeAraListTogether/{SicilNo}/{searchstring}")]
        public IHttpActionResult GetUyeList(int SicilNo, string searchstring)
        {


            return Ok(db.Uyeler.Where(i => i.UyeAdi.Contains(searchstring) && i.SicilNo == SicilNo).ToList());


        }



        //------------------------------------------------------------

        //Odunc ile ilgili database işlemleri



        [Route("api/OduncVer")]
        public IHttpActionResult PostOduncVer(OduncTut oduncTut)
        {


            KitapBilgi kitapId = (from kitap in db.Kitaplar
                             where kitap.Id == oduncTut.id
                             select kitap).FirstOrDefault();

            UyeBilgi uyeId = (from uye in db.Uyeler
                         where uye.SicilNo == oduncTut.SicilNo
                         select uye).FirstOrDefault();

            kitapId.StokDurumu -= 1;

            OduncBilgi yeniodunc = new OduncBilgi();

            yeniodunc.Kitap = kitapId;
            yeniodunc.Uye = uyeId;
            yeniodunc.isActive = true;
            yeniodunc.AlinanTarih = DateTime.Now.ToString();
            yeniodunc.TeslimEdilecekTarih = oduncTut.TeslimEdilecekTarih;
            db.Oduncler.Add(yeniodunc);
            db.SaveChanges();

            return Ok();

        }



        [Route("api/Odunciade/{id}")]
        public IHttpActionResult DeleteOdunc(int id)
        {

            var silinecek = db.Oduncler.Find(id);

            silinecek.isActive = false;
            silinecek.Kitap.StokDurumu += 1;
            silinecek.TeslimTarihi = DateTime.Now.ToString();
            


            db.SaveChanges();


            return Ok();
        }




        [Route("api/OduncList")]
        public IHttpActionResult GetOduncList()
        {

            return Ok(db.Oduncler.Where(i => i.isActive == true).ToList());

        }

        [Route("api/OduncGecmis/{SicilNo}")]
        public IHttpActionResult GetOduncList(int SicilNo)
        {
           
            return Ok(db.Oduncler.Where(i => i.isActive == false && i.Uye.SicilNo == SicilNo).ToList());

        }


        //[Route("api/yetkilibilgi")]
        // public IHttpActionResult PostYetkiliBilgi(YetkiliBilgi yetkiliBilgi)
        //{

        //    db.Yetkililer.Where(i => i.sifre == yetkiliBilgi.sifre && i.SicilNo == yetkiliBilgi.SicilNo);

        //    return Ok();
        //}



        //[Route("api/Token")]
        //public IHttpActionResult GetTokenOlustur()
        //{




        //    return Created("",JWTmanager.GenerateToken("merhaba"));
        //}

    }
}

