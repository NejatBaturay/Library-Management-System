using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using System.Net.Http;
using LibraryMVC.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;



namespace Kutuphanetaslak.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public ActionResult Giris()
        {
            return View();

        }
        
        [HttpPost]
        public ActionResult Giris(YetkiliBilgi yetkiliBilgi)
        {
            byte[] bytes;
            StringBuilder hashsifre = new StringBuilder();
            using (var hash = SHA256Managed.Create())
            {

               bytes = hash.ComputeHash(Encoding.Default.GetBytes(yetkiliBilgi.sifre));
                foreach(var b in bytes)
                {

                    hashsifre.Append(b.ToString("x2"));

                }

                yetkiliBilgi.sifre = hashsifre.ToString();

            }


            using (var client = new HttpClient())
            {



                var response = client.PostAsJsonAsync<YetkiliBilgi>("http://localhost:52197/api/GirisYap", yetkiliBilgi);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View("Giris");
            }
           

        }


   
        public ActionResult KitapListele()



        {
            IEnumerable<KitapBilgi> kitaplar = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/KitapList";

                var response = client.GetAsync(url);


                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {


                    kitaplar = responseResult.Content.ReadAsAsync<IEnumerable<KitapBilgi>>().Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View(kitaplar);
            }
        }

        [HttpPost]
        public ActionResult KitapListele(string searchstring)
        {


            IEnumerable<KitapBilgi> kitapbilgiaranan = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/KitapAraListele";

                var response = client.GetAsync(url +"/"+searchstring);

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<IEnumerable<KitapBilgi>>();

                    ReadResponse.Wait();

                    kitapbilgiaranan = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }
               

                return View(kitapbilgiaranan);
            }
        }







        [HttpGet]
        public ActionResult KitapEkle()
        {


            List<System.Web.Mvc.SelectListItem> dinamik_degerler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/KitapEkle";

                var response = client.GetAsync(url);

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<List<System.Web.Mvc.SelectListItem>>();

                    ReadResponse.Wait();

                    dinamik_degerler = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }
                ViewBag.degerlerim = dinamik_degerler;

                return View();
            }
        }









        [HttpPost]
        public ActionResult KitapEkle(KitapBilgi kitapBilgi)
        {


            using (var client = new HttpClient())
            {



                var response = client.PostAsJsonAsync<KitapBilgi>("http://localhost:52197/api/KitapEkle", kitapBilgi);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("KitapListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View(kitapBilgi);
            }

        }



        public ActionResult KitapSil(int id)
        {


            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/KitapSil";

                var response = client.DeleteAsync(url + "/" + id.ToString());
             

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("KitapListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return RedirectToAction("KitapListele");
            }

        }


        [HttpGet]
        public ActionResult KitapGuncelle(int id)
        {

            UptadeModel updateModel = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/KitapGuncelle";

                var response = client.GetAsync(url + "/" + id.ToString());

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsStringAsync();

                    ReadResponse.Wait();

                    var jsonString = JsonConvert.DeserializeObject<String>(ReadResponse.Result);



                    updateModel = JsonConvert.DeserializeObject<UptadeModel>(jsonString);

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                ViewBag.degerlerim = updateModel.dinamikdeger;

                return View(updateModel.kitapBilgi);
            }

        }

        [HttpPost]
        public ActionResult KitapGuncelle(KitapBilgi kitapBilgi)
        {



            using (var client = new HttpClient())
            {



                var response = client.PutAsJsonAsync<KitapBilgi>("http://localhost:52197/api/KitapGuncelle/", kitapBilgi);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("KitapListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View(kitapBilgi);
            }

        }










        public ActionResult UyeListele()
        {
            IEnumerable<UyeBilgi> uyeler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/UyeList";

                var response = client.GetAsync(url);

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<IEnumerable<UyeBilgi>>();

                    ReadResponse.Wait();

                    uyeler = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View(uyeler);
            }
        }



        [HttpPost]
        public ActionResult UyeListele(int ?SicilNo,string searchstring ="")
            {
            IEnumerable<UyeBilgi> uyeler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/UyeAraList";


                if(SicilNo == null)
                {

                    url += "SearchString/" + searchstring;

                }
                else if(SicilNo != null && searchstring == "")
                {
                    url += "SicilNo/" + SicilNo.ToString();

                }
                else {

                    url += "Together/" + SicilNo.ToString() + "/" + searchstring;

                }


                var response = client.GetAsync(url);

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<IEnumerable<UyeBilgi>>();

                    ReadResponse.Wait();

                    uyeler = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View(uyeler);
            }
        }


        public ActionResult UyeEkle()
        {



            return View("UyeEkle");
        }




        [HttpPost]
        public ActionResult UyeEkle(UyeBilgi uyeBilgi)
        {


            using (var client = new HttpClient())
            {



                var response = client.PostAsJsonAsync<UyeBilgi>("http://localhost:52197/api/UyeEkle", uyeBilgi);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("UyeListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View(uyeBilgi);
            }

        }



        public ActionResult UyeSil(int id)
        {


            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/UyeSil";

                var response = client.DeleteAsync(url + "/" + id.ToString());
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("UyeSil");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return RedirectToAction("KitapListele");
            }

        }
        [HttpGet]
        public ActionResult UyeGuncelle(int id)
        {

            UyeBilgi uyeler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/UyeGuncelle";

                var response = client.GetAsync(url + "/" + id.ToString());

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsStringAsync();


                    var jsonString = JsonConvert.DeserializeObject<String>(ReadResponse.Result);

                    ReadResponse.Wait();

                    uyeler = JsonConvert.DeserializeObject<UyeBilgi>(jsonString);

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View(uyeler);
            }
        }


        [HttpPost]
        public ActionResult UyeGuncelle(UyeBilgi uyeBilgi)
        {



            using (var client = new HttpClient())
            {



                var response = client.PutAsJsonAsync<UyeBilgi>("http://localhost:52197/api/UyeGuncelle/", uyeBilgi);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("UyeListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View(uyeBilgi);
            }

        }



        public ActionResult OduncListele()
        {
            IEnumerable<OduncBilgi> oduncler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/OduncList";

                var response = client.GetAsync(url);

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<IEnumerable<OduncBilgi>>();

                    ReadResponse.Wait();

                    oduncler = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View(oduncler);
            }
        }


        public ActionResult OduncVer()
        {


            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(int id, int SicilNo, string teslimedilecektarih)
        {
            OduncTut oduncTut = new OduncTut();
            oduncTut.id = id;
            oduncTut.SicilNo = SicilNo;
            oduncTut.TeslimEdilecekTarih = teslimedilecektarih;



            using (var client = new HttpClient())
            {



                var response = client.PostAsJsonAsync<OduncTut>("http://localhost:52197/api/OduncVer", oduncTut);
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("UyeListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return View("Index");
            }
        }


        public ActionResult OduncIade(int id)
        {
            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/Odunciade";

                var response = client.DeleteAsync(url + "/" + id.ToString());
                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    return RedirectToAction("OduncListele");

                }

                ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                return RedirectToAction("OduncListele");
            }
        }

        [HttpGet]
        public ActionResult OduncGecmisListele()
        {


            return View();

        }




        [HttpPost]
        public ActionResult OduncGecmisListele(int aranandeger)
        {

            IEnumerable<OduncBilgi> oduncler = null;

            using (var client = new HttpClient())
            {

                var url = "http://localhost:52197/api/OduncGecmis";

                var response = client.GetAsync(url+"/"+aranandeger.ToString());

                response.Wait();

                var responseResult = response.Result;

                if (responseResult.IsSuccessStatusCode)
                {

                    var ReadResponse = responseResult.Content.ReadAsAsync<IEnumerable<OduncBilgi>>();

                    ReadResponse.Wait();

                    oduncler = ReadResponse.Result;

                }

                else
                {

                    ModelState.AddModelError(string.Empty, "Bir Hata Oluştu");

                }

                return View("OduncGecmisListele",oduncler);
            }
        }
    }
}


   

    









        

    



