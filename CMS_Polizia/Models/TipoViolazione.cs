using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMS_Polizia.Models
{
    public class TipoViolazione
    {
        public int IdViolazione { get; set; }
        public string descrizione { get; set; }

        public static List<TipoViolazione> ListaViolazioni = new List<TipoViolazione>();

        public static List<TipoViolazione> GetAllViolazioni()
        {
            SqlConnection connetcionDB = new SqlConnection();
            connetcionDB.ConnectionString = ConfigurationManager.ConnectionStrings["DB_Polizia"].ToString();
            connetcionDB.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Violazioni";
            command.Connection = connetcionDB;

            SqlDataReader reader = command.ExecuteReader();

            ListaViolazioni.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TipoViolazione violazione = new TipoViolazione ();
                    violazione.descrizione = reader["descrizione"].ToString();
                    ListaViolazioni.Add(violazione);
                }
            }

            connetcionDB.Close();
            return TipoViolazione.ListaViolazioni;
        }

    }
}