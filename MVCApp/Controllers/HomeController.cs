using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
            con.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM DipendenteTab";
            command.Connection = con;

            SqlDataReader reader = command.ExecuteReader();
            List<Dipendente> EmployeeList = new List<Dipendente>();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    Dipendente employee = new Dipendente();
                    employee.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    employee.Nome = reader["Nome"].ToString();
                    employee.Cognome = reader["Cognome"].ToString();
                    employee.Stipendio = Convert.ToDouble(reader["Stipendio"]);
                    employee.Coniugato = Convert.ToBoolean(reader["Coniugato"]);
                    EmployeeList.Add(employee);
                }
            }

            con.Close();

            
            return View(EmployeeList);
        }

        // Aggiunta dipendente

        
        public ActionResult CreateEmployee() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Dipendente employee)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Nome", employee.Nome);
                command.Parameters.AddWithValue("@Cognome", employee.Cognome);
                command.Parameters.AddWithValue("@Stipendio", employee.Stipendio);
                command.Parameters.AddWithValue("@Coniugato", employee.Coniugato);

                command.CommandText = "INSERT INTO DipendenteTab VALUES (@Nome , @Cognome , @Stipendio, @Coniugato)";
                command.Connection = con;


                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

                con.Close();
            }

            con.Close();
            return RedirectToAction("Index");
        }

        // Update dipendente
        public ActionResult EditEmployee(int id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
            con.Open();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@ID", id);

            command.CommandText = "SELECT * FROM DipendenteTab WHERE IDDipendente = @ID";
            command.Connection = con;

            SqlDataReader reader = command.ExecuteReader();
            Dipendente employee = new Dipendente();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    employee.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    employee.Nome = reader["Nome"].ToString();
                    employee.Cognome = reader["Cognome"].ToString();
                    employee.Stipendio = Convert.ToDouble(reader["Stipendio"]);
                    employee.Coniugato = Convert.ToBoolean(reader["Coniugato"]);

                }
            }

            con.Close();
            return View(employee); 
        }

        [HttpPost]
        public ActionResult EditEmployee(Dipendente employee)
        {
            SqlConnection con = new SqlConnection();
            try { 
            
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
            con.Open();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@ID", employee.IDDipendente);
            command.Parameters.AddWithValue("@Nome", employee.Nome);
            command.Parameters.AddWithValue("@Cognome", employee.Cognome);
            command.Parameters.AddWithValue("@Stipendio", employee.Stipendio);
            command.Parameters.AddWithValue("@Coniugato", employee.Coniugato);

            command.CommandText = "UPDATE DipendenteTab SET Nome = @Nome , Cognome = @Cognome , Stipendio = @Stipendio , Coniugato = @Coniugato WHERE IDDipendente = @ID";
            command.Connection = con;

            command.ExecuteNonQuery();


            }catch(Exception ex)
            {
                con.Close();
            }

            con.Close();

            return RedirectToAction("Index");
        }

        // Eliminazione dipendente 

        //public ActionResult DeleteEmployee(int id) // in modalità GET
        //{
        //    SqlConnection con = new SqlConnection();
        //    try
        //    {
        //        con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
        //        con.Open();

        //        SqlCommand command = new SqlCommand();
        //        command.Parameters.AddWithValue("@ID", id);

        //        command.CommandText = "DELETE FROM DipendenteTab WHERE IDDipendente = @ID";
        //        command.Connection= con;

        //        command.ExecuteNonQuery();

        //    }
        //    catch(Exception ex) 
        //    {
        //        con.Close();
        //    }

        //    con.Close();
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ID", id);

            cmd.CommandText = "SELECT Nome, Cognome FROM DipendenteTab WHERE IDDipendente = @ID";
            cmd.Connection = con;

            SqlDataReader reader = cmd.ExecuteReader();
            Dipendente d = new Dipendente();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    d.IDDipendente = id;
                    d.Nome = reader["Nome"].ToString();
                    d.Cognome = reader["Cognome"].ToString();
                }
            }

            con.Close();
            return View(d);
        }

        [HttpPost]
        [ActionName("DeleteEmployee")]
        public ActionResult ConfirmDeleteEmployee(int IDDipendente, string Nome, string Cognome)
        {

            // AZIONI PER LA CANCELLAZIONE DAL DB

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToDipendentiDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@ID", IDDipendente);

                command.CommandText = "DELETE FROM DipendenteTab WHERE IDDipendente = @ID";
                command.Connection = con;

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                con.Close();
            }

            con.Close();


            return RedirectToAction("Index");
        }

    }
}