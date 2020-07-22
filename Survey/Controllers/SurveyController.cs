using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Models;

namespace Survey.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        public ActionResult Survey()
        {
            List<Class1> serverydata = new List<Class1>();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from suyvey order by id desc", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var servey = new Class1();
                    servey.id = Convert.ToInt32(rdr["id"]);
                    servey.name = rdr["name"].ToString();
                    servey.city = rdr["city"].ToString();
                    servey.age = rdr["age"].ToString();
                    servey.gender = rdr["gender"].ToString();
                    servey.education = rdr["education"].ToString();
                    servey.file = rdr["filename"].ToString();
                    servey.email = rdr["email"].ToString();
                    serverydata.Add(servey);
                }
            }

            return View(serverydata);
        }
    }
}