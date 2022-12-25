using CMS_Polizia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Polizia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
 
            return View();
        }

        public ActionResult VerbaliPerTrasgressore()
        {
            List<Anagrafica> ListaVerbaliPerTrasgressore = Anagrafica.GetVerbaliGroupByTrasgressori();
            return PartialView(ListaVerbaliPerTrasgressore);
        }

        public ActionResult PuntiDecurtatiPerTrasgressore() {
            List<Anagrafica> ListaPuntiToltiPerTrasgressore = Anagrafica.GetPuntiDecurtatiGroupByTrasgressore();
            return PartialView(ListaPuntiToltiPerTrasgressore);
        }
        
        public ActionResult ViolazioniOverTenPnt()
        {
            List<Anagrafica> ListaViolazioniOver10pnt = Anagrafica.ViolazioniOver10();
            return PartialView(ListaViolazioniOver10pnt);
        }

        public ActionResult ViolazioniOver400money()
        {
            List<Anagrafica> ListaViolazioniOver400Mny = Anagrafica.GetViolazioniOver400Mny();
            return PartialView(ListaViolazioniOver400Mny);
        }
    }
}