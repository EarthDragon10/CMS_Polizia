using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Polizia.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizione { get; set; }
        public decimal Importo { get; set; }
        public double DecurtamentoPunti { get; set; }
        public int IdAnagrafica { get; set; }
        public int IdViolazione { get; set; }
        public static List<Verbale> ListaVerbali = new List<Verbale>();
    }
}