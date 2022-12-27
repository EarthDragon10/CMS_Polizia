using Antlr.Runtime.Tree;
using CMS_Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        // Aggiunta nuovo Trasgressore
        public ActionResult Trasgressori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Trasgressori(Anagrafica trasgressore)
        {
            SqlConnection connetcionDB = new SqlConnection();
            try
            {
                connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
                connetcionDB.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Cognome", trasgressore.Cognome);
                command.Parameters.AddWithValue("@Nome", trasgressore.Nome);
                command.Parameters.AddWithValue("@Indirizzo", trasgressore.Indirizzo);
                command.Parameters.AddWithValue("Citta", trasgressore.Citta);
                command.Parameters.AddWithValue("CAP", trasgressore.CAP);
                command.Parameters.AddWithValue("@Cod_Fisc", trasgressore.Cod_Fisc);
                command.CommandText = "INSERT INTO ANAGRAFICA VALUES (@Cognome, @Nome, @Indirizzo, @CItta, @CAP, @Cod_Fisc)";
                command.Connection = connetcionDB;

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                connetcionDB.Close();
            }
            
            connetcionDB.Close();
            return RedirectToAction("Index");
        }

        // Mostra e Aggiungi Violazioni
        public ActionResult Violazioni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Violazioni(TipoViolazione violazione)
        {
            SqlConnection connetcionDB = new SqlConnection();
            try
            {
                connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
                connetcionDB.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Descrizione", violazione.descrizione);
                command.CommandText = "INSERT INTO TIPO_VIOLAZIONE VALUES (@Descrizione)";
                command.Connection = connetcionDB;

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                connetcionDB.Close();
            }

            connetcionDB.Close();
            return RedirectToAction("Index");
        }

        // Aggiungi Verbali
        public ActionResult AggiungiVerbale()
        {
            ViewBag.ListaTrasgressori = Anagrafica.AnagragicaDropDownList();
            ViewBag.ListaViolazioni = Verbale.VerbaleDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiVerbale(Verbale verbale)
        {
            SqlConnection connetcionDB = new SqlConnection();
            try
            {
                connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
                connetcionDB.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NomeAgente", verbale.Nominativo_Agente);
                command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizione);
                command.Parameters.AddWithValue("@Importo", verbale.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IdAnagrafica", verbale.IdAnagrafica);
                command.Parameters.AddWithValue("@IdViolazione", verbale.IdViolazione);
                
                command.CommandText = "INSERT INTO VERBALE VALUES (@Descrizione, @IndirizzoViolazione, @NomeAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IdAnagrafica, @IdViolazione)";
                command.Connection = connetcionDB;

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                connetcionDB.Close();
            }

            connetcionDB.Close();

            return View();
        }

        public ActionResult ListaVerbali()
        {
            List<Verbale> ListaVerbali = Verbale.GetAllVerbali();
            return View(ListaVerbali);
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