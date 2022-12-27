using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Polizia.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataTrascrizione { get; set; }
        public decimal Importo { get; set; }
        public double DecurtamentoPunti { get; set; }
        public int IdAnagrafica { get; set; }
        public int IdViolazione { get; set; }
        public string DescrizioneViolazione { get; set; }
        public string NomeTrasgressore { get; set; }
        public string CognomeTrasgressore { get; set; }

        public static List<Verbale> ListaVerbali = new List<Verbale>();
        public static List<Verbale> GetAllVerbali()
        {
            SqlConnection connetcionDB = new SqlConnection();
            try
            {
                connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
                connetcionDB.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "select DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, descrizione, Cognome, Nome from VERBALE as V inner join TIPO_VIOLAZIONE as TP on V.IdViolazione = TP.IdViolazione inner join ANAGRAFICA as A on V.IdAnagrafica = A.IdAnagrafica";
                command.Connection = connetcionDB;

                SqlDataReader reader = command.ExecuteReader();

                ListaVerbali.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale();

                        verbale.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        verbale.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                        verbale.Nominativo_Agente = reader["Nominativo_Agente"].ToString();
                        verbale.DataTrascrizione = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                        verbale.Importo = Convert.ToDecimal(reader["Importo"]);
                        verbale.DecurtamentoPunti = Convert.ToDouble(reader["DecurtamentoPunti"]);
                        verbale.DescrizioneViolazione = reader["descrizione"].ToString();
                        verbale.CognomeTrasgressore = reader["Cognome"].ToString();
                        verbale.NomeTrasgressore = reader["Nome"].ToString();
                        ListaVerbali.Add(verbale);
                    }
                }

            }
            catch (Exception)
            {
                connetcionDB.Close();
            }

            connetcionDB.Close();
            return Verbale.ListaVerbali;
        }
        public static List<SelectListItem> VerbaleDropDownList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            SqlConnection connetcionDB = new SqlConnection();
            try
            {
                connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
                connetcionDB.Open();

                SqlCommand command = new SqlCommand();


                command.CommandText = "SELECT * FROM TIPO_VIOLAZIONE";
                command.Connection = connetcionDB;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem selectedItem = new SelectListItem
                        {
                            Value = reader["IdViolazione"].ToString(),
                            Text = reader["descrizione"].ToString(),
                        };

                        selectList.Add(selectedItem);
                    }
                }
            }
            catch (Exception)
            {
                connetcionDB.Close();
            }

            connetcionDB.Close();
            return selectList;
        }
    }
}