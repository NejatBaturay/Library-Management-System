﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.Web.Mvc;
using LibraryManagement.Models;


namespace WebApplication1.Models
{
    public class UptadeModel {

        public KitapBilgi kitapBilgi { get; set; }
        public List<System.Web.Mvc.SelectListItem> dinamikdeger { get; set; }



    }
}