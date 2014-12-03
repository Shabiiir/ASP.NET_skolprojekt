using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Enteties;

namespace WebApplication
{
   
    public partial class EditProfile : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer D = new DataLayer();
        
        string undantag = string.Empty;


        //först kontrolleras ifall användaren är inloggad, sedan 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUserInfo();
            }
        }


        // Metod som körs när programet går igång.
        public void GetUserInfo()
        {
            // Om en användare är inloggad så hämtas info
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                SetUserInfo();
            }

            // Annars visas signin sidan
            else
            {
                Response.Redirect("SignIn.aspx");
            }
        }


        //Sätter användarens infromation på profilsidan.
        public void SetUserInfo()
        {
            try
            {
                //Anropar Datalagretoch får tillbaka en lista med information.
                List<USerEntities> listUserInfo = D.GetUSerInfo(HttpContext.Current.User.Identity.Name);

                //Om listan är inte tom så sätt användarens information ut.
                //Listan skall aldrig vara tom(om nu det händer).

                if (listUserInfo != null && listUserInfo.Count > 0)
                {
                    for (int i = 0; i < listUserInfo.Count; i++)
                    {
                        int ResidencyIndex = 10;
                        string Residency = listUserInfo[i].Bor;
                        {
                            switch (Residency)
                            {
                                case "Sweden":
                                    ResidencyIndex = 1;
                                    break;
                                case "Norway":
                                    ResidencyIndex = 2;
                                    break;
                                case "Finland":
                                    ResidencyIndex = 3;
                                    break;
                                case "Denmark":
                                    ResidencyIndex = 4;
                                    break;
                                case "USA":
                                    ResidencyIndex = 5;
                                    break;
                                case "UK":
                                    ResidencyIndex = 6;
                                    break;
                                default:
                                    ResidencyIndex = 0;
                                    break;
                            }
                        }

                        int jobIndex = 0;
                        string job = listUserInfo[i].Sysselsättning;
                        {
                            switch (Residency)
                            {
                                case "Working":
                                    jobIndex = 1;
                                    break;
                                case "Studying":
                                    jobIndex = 2;
                                    break;
                                case "Unemployed":
                                    jobIndex = 3;
                                    break;
                                case "Philosopher":
                                    jobIndex = 4;
                                    break;
                                case "Writer":
                                    jobIndex = 5;
                                    break;
                                case "Star counter":
                                    jobIndex = 6;
                                    break;
                                default:
                                    jobIndex = 0;
                                    break;
                            }
                        }

                        tbxFNamn.Text = listUserInfo[i].FNamn;
                        tbxEnamn.Text = listUserInfo[i].ENamn;
                        tbxEmail.Text = listUserInfo[i].EMail;
                        tbxBirthday.Text = listUserInfo[i].Ålder.ToShortDateString();
                        DropDownListCountry.SelectedIndex = ResidencyIndex;
                        DropDownListRegEmploy.SelectedIndex = jobIndex;
                        tbxAboutMe.Text = listUserInfo[i].OmMig;

                        //Byter ut bild källan.
                        PPicture.ImageUrl = "~/images/" + listUserInfo[i].Bild;
                    }
                }
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i profilsidan: " + ex.Message;
            }
        }


        // Laddar upp och uppdaterar profilbilden.
        public string UpdatePic()
        {   
            string imageName = null;
            string resultat = "";
            
            if (FileUpload1.HasFile)
            {
                try
                {
                    //Sparar bildens namn.
                    imageName = Path.GetFileName(FileUpload1.FileName);

                    // Sparar bilden i mappen Images.
                    FileUpload1.SaveAs(Server.MapPath("~/Images/" + imageName));
                    resultat = "Bilden är uppladdad nu.";
                    D.UpdateProfilePicture(HttpContext.Current.User.Identity.Name, imageName);
                }

                //om någont går fel undantag kastas.
                catch (Exception ex)
                {
                    //Fel meddelande kastas.
                    undantag = "Ohanterat undantag i profilsidan: " + ex.Message;
                }
            }
            return resultat;
        }


        //lösenords byte
        public string UpdatePassword()
        {
            string resultat = "vänligen ange korrekt gammalt lösenord";
            
            try
            {
                // Användar information för en specefik användare hämtas.
                List<USerEntities> listUserInfo = D.GetUSerInfo(HttpContext.Current.User.Identity.Name);
                //Checkar om den inmatede lösenordet är desamma som användarens lösenord.
                if (tbxOld.Text == D.getPassword(HttpContext.Current.User.Identity.Name))
                {
                    //checkar om "Nyttlösenord" och "upprepa nytt lösenord" matchar.
                    if (tbxNew1.Text == tbxNew2.Text)
                    {
                        //lösenordet byts ut.
                        D.UpdatePassword(HttpContext.Current.User.Identity.Name, tbxNew1.Text);
                        resultat = "Ditt lösenord har ändrats.";
                    }

                    else
                    {
                        resultat = "Nya lösenorden matchar inte. Försök igen";
                    }
                }
            }

            catch (Exception ex)
            {
                resultat = "följande fel har inträffat " + ex.Message;
            }

            return resultat;
        }


        //hämtar informatin till textboxarna för att kunna uppdatera informationen på nytt
        public void UpdateUserInfo()
        {
            try
            {
                string age = tbxBirthday.Text;
                DateTime alder = DateTime.Parse(age);
                string fornamn = tbxFNamn.Text;
                //Uppdaterar användarens information.
                D.UpdateUserInfo(HttpContext.Current.User.Identity.Name, fornamn, tbxEnamn.Text, tbxEmail.Text, alder, DropDownListCountry.Text, DropDownListRegEmploy.Text, tbxAboutMe.Text);
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i profilsidan: " + ex.Message;
            }
        }


        // Sparar användarens information och hämtar användar informationen på nytt.
        protected void btn_saveClick(object sender, EventArgs e)
        {
            UpdateUserInfo();
            SetUserInfo();
        }


        //En knappfunktion för att uppdatera sin bild och uppdaterar sidan på nytt.
        protected void btn_SavePic_Click(object sender, EventArgs e)
        {
            UpdatePic();
            Response.Redirect(Request.RawUrl);
        }


        //En knappfunktion för att ändra på sitt nuvarande lösenord
        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }
    }
}