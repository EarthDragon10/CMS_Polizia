using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Polizia.Models
{
    public class TipoViolazione
    {
        public int IdViolazione { get; set; }
        public string descrizione { get; set; }

        public List<TipoViolazione> ListaViolazioni = new List<TipoViolazione> ();
    }
}