using Survey.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Controllers
{
    public class HomeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

        public ActionResult Index()
        {
            Class1 cityname = new Class1();
            DataSet ds = new DataSet();
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from city order by city asc", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Class1> cityList = new List<Class1>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Class1 cityobj = new Class1();
                        cityobj.CityId = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                        cityobj.CityName = ds.Tables[0].Rows[i]["city"].ToString();
                      
                        cityList.Add(cityobj);
                    }
                    cityname.cityinfo = cityList;
                }
                con.Close();
           
            return View(cityname);
        
    }

            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(string age,string name,string education,string email,string city,string gender)
        {
            DataSet ds = new DataSet();
            Random r = new Random();
            int num = r.Next();
            try
                {
                   
                        HttpFileCollectionBase files = Request.Files;
                        HttpPostedFileBase file = files[0];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = num + testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = num + file.FileName;
                        }
                        int iFileSize = file.ContentLength;
                        if (iFileSize < 1000000)  // 1MB approx (actually less though)
                        {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into suyvey values('"+name+ "','" + age + "','" + email + "','" + gender + "','" + city + "','" + education + "','" + fname + "')", con))
                        {
                            con.Open();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                        }
                        con.Close();
                    }

                    fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                            file.SaveAs(fname);
                        }
                        else
                        {

                        }
                    return Json("Data Save Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            
        }
    }
}