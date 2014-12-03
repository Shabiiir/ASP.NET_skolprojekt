using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DAL;
using Enteties;

namespace WebApplication
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();

        string undantag = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Home.aspx");
                }

                RadioRegMale.Checked = true;
                RadioRegNo.Checked = true;

        }


        public bool CheckUserName(string name)
        {
            List<USerEntities> U = d.GetAllUsers();
            //true om användarnamnet är tillgänglig.
            
            bool ret = true;

            for (int i = 0; i < U.Count; i++)
            {
                if (U[i].Användarnamn == name) 
                {
                    //returnerar false och bryter loopen om användarnmanet är upptagen.
                    ret = false;
                    break;
                }
            }
            return ret;
        }


        protected void SubmitReg_Click(object sender, EventArgs e)
        {
                if (CheckUserName(txtRegUsername.Text))
                {
                    SaveAndSend();
                    Response.Redirect("Home.aspx");
                }

                else
                {
                    UserExists.Text = "Användarnamnet är upptagen, var god och välj en annan.";
                }
        }


        //Skickar all data från textboxarna vidare till DAL(Datalagret).
        public void  SaveAndSend()
        {
            string age = txtRegAge.Text;

            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

            DateTime alder = DateTime.ParseExact(age, "yyyyMMdd", culture);

            string gender;

            if (RadioRegMale.Checked)
            {
                gender = RadioRegMale.Text;
            }

            else if (RadioRegFemale.Checked)
            {
                gender = RadioRegFemale.Text;
            }

            else
            {
                gender = null;
            }

            string imageName = null;
            if (FileUpload1.HasFile)
            {
                try
                {
                    //Sparar bildens namn
                    imageName = Path.GetFileName(FileUpload1.FileName);
                    
                    // Sparar bilden i mappen Images
                    FileUpload1.SaveAs(Server.MapPath("~/Images/" + imageName));
                    undantag = "Upload status: File uploaded!"; 
                }

                //om någont går fel kastas ett undantag.
                catch (Exception ex)
                {
                    //Felmeddelande kastas.
                    undantag = "Ohanterat undantag i registrerings sidan: " + ex.Message;
                }
            }

            DateTime pre;
            if (RadioRegYes.Checked)
            {
                pre = DateTime.Now;
            }

            else
            {
                pre = new DateTime(2000, 01, 01);
            }

            d.RegisterUser(txtRegUsername.Text, txtRegPassword.Text, txtRegFirstname.Text, txtRegLastname.Text, txtRegEmail.Text, alder, gender, DropDownListCountry.Text, DropDownListRegEmploy.Text, txtAboutMe.Text, imageName, pre);

        }
    }
}



