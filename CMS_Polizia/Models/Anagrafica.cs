using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMS_Polizia.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public double CAP { get; set; }
        public string Cod_Fisc { get; set; }

        [Display(Name = "Totale Multe in euro")]
        public decimal TotaleMulte { get; set; }
        public double NumVerbali { get; set; }
        public double PuntiDecurtati { get; set; }
        public DateTime DataViolazione { get; set; }
        public decimal Importo { get; set; }

        public static List<Anagrafica> ListaTrasgressori = new List<Anagrafica>();

        public static List<Anagrafica> GetAllTrasgressori()
        {
            SqlConnection connetcionDB = new SqlConnection();
            connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
            connetcionDB.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from ANAGRAFICA";
            command.Connection = connetcionDB;

            SqlDataReader reader = command.ExecuteReader();

            ListaTrasgressori.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Anagrafica tragressore = new Anagrafica();
                    tragressore.IdAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                    tragressore.Cognome = reader["Cognome"].ToString();
                    tragressore.Nome = reader["Nome"].ToString();
                    tragressore.Indirizzo = reader["Indirizzo"].ToString();
                    tragressore.Citta = reader["Citta"].ToString();
                    tragressore.CAP = Convert.ToDouble(reader["CAP"]);
                    tragressore.Cod_Fisc = reader["Cod_Fisc"].ToString();
                    ListaTrasgressori.Add(tragressore);
                }
            }

            connetcionDB.Close();
            return Anagrafica.ListaTrasgressori;
        }

        public static List<Anagrafica> GetVerbaliGroupByTrasgressori()
        {
            SqlConnection connetcionDB = new SqlConnection();
            connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
            connetcionDB.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "select Cognome, Nome,Sum(Importo) as TotaleMulte, count(*) as numVerbali from ANAGRAFICA as A inner join VERBALE as V on A.IdAnagrafica = V.IdAnagrafica group by Cognome, Nome";
            command.Connection = connetcionDB;

            SqlDataReader reader = command.ExecuteReader();

            ListaTrasgressori.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Anagrafica trasgressore = new Anagrafica();
                    trasgressore.Cognome = reader["Cognome"].ToString();
                    trasgressore.Nome = reader["Nome"].ToString();
                    trasgressore.TotaleMulte = Convert.ToDecimal(reader["TotaleMulte"]);
                    trasgressore.NumVerbali = Convert.ToDouble(reader["numVerbali"]);
                    ListaTrasgressori.Add(trasgressore);
                }
            }

            connetcionDB.Close();
            return Anagrafica.ListaTrasgressori;
        }

        public static List<Anagrafica> GetPuntiDecurtatiGroupByTrasgressore()
        {
            SqlConnection connetcionDB = new SqlConnection();
            connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
            connetcionDB.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "select Cognome, Nome, Sum(DecurtamentoPunti) as PuntiDecurtati from ANAGRAFICA as A inner join VERBALE as V on A.IdAnagrafica = V.IdAnagrafica group by Cognome, Nome";
            command.Connection = connetcionDB;

            SqlDataReader reader = command.ExecuteReader();

            ListaTrasgressori.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Anagrafica trasgressore = new Anagrafica();
                    trasgressore.Cognome = reader["Cognome"].ToString();
                    trasgressore.Nome = reader["Nome"].ToString();
                    trasgressore.PuntiDecurtati = Convert.ToDouble(reader["DecurtamentoPunti"]);
                    ListaTrasgressori.Add(trasgressore);
                }
            }

            connetcionDB.Close();
            return Anagrafica.ListaTrasgressori;
        }

        public static List<Anagrafica> ViolazioniOver10()
        {
            SqlConnection connetcionDB = new SqlConnection();
            connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
            connetcionDB.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "select Cognome, Nome, Indirizzo, DataViolazione, Importo, DecurtamentoPunti from ANAGRAFICA as A inner join VERBALE as V on A.IdAnagrafica = V.IdAnagrafica where DecurtamentoPunti > 9";
            command.Connection = connetcionDB;

            SqlDataReader reader = command.ExecuteReader();

            ListaTrasgressori.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Anagrafica trasgressore = new Anagrafica();
                    trasgressore.Cognome = reader["Cognome"].ToString();
                    trasgressore.Nome = reader["Nome"].ToString();
                    trasgressore.Indirizzo = reader["Indirizzo"].ToString();
                    trasgressore.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    trasgressore.PuntiDecurtati = Convert.ToDouble(reader["DecurtamentoPunti"]);
                    ListaTrasgressori.Add(trasgressore);
                }
            }

            connetcionDB.Close();
            return Anagrafica.ListaTrasgressori;
        }
    }
}