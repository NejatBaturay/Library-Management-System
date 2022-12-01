using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using LibraryMVC.Models;


namespace LibraryMVC.Models
{
    public class UptadeModel {

        public KitapBilgi kitapBilgi { get; set; }
        public List<System.Web.Mvc.SelectListItem> dinamikdeger { get; set; }



    }
}