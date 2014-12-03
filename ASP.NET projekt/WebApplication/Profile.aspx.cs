using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Enteties;


namespace WebApplication
{
    public partial class Profile : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();

        string undantag = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserInfo();
        }
        

        // Metod som körs när programmet går igång.
        public void GetUserInfo()
        {
            try
            {
                //Om en användare är inloggad så hämtas info.
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    SetUserInfo();
                    getWallMessage();
                }

                else
                {
                    Response.Redirect("SignIn.aspx"); //annars visas signin sidan.
                }
            }

             //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i profil sidan: " + ex.Message;
            }
        }


        //Sätter användarens information på profilsidan.
        public void SetUserInfo()
        {
            try
            {
                //Anropar Datalagretoch får tillbaka en lista med information.
                List<USerEntities> listUserInfo = d.GetUSerInfo(HttpContext.Current.User.Identity.Name);

                //Om listan är inte tom så sätt användarens information ut.
                if (listUserInfo != null)
                {
                    lblPNamn2.Text = listUserInfo[0].FNamn + " " + listUserInfo[0].ENamn;
                    lblPEmail2.Text = listUserInfo[0].EMail;
                    lblPAlder2.Text = listUserInfo[0].Ålder.ToShortDateString();
                    lblPSysselsattning2.Text = listUserInfo[0].Sysselsättning;
                    lblPKon2.Text = listUserInfo[0].Kön;
                    lblPBor2.Text = listUserInfo[0].Bor;
                    lblPOmmig2.Text = listUserInfo[0].OmMig;

                    //Byter ut bild källan.
                    PPicture.ImageUrl = "~/images/" + listUserInfo[0].Bild;
                    DateTime x = new DateTime(1010, 10, 10);

                    if (listUserInfo[0].Pre != x)
                    {
                        lblPPermium2.Text = "Premium";
                    }

                    else
                    {
                        lblPPermium2.Text = "Standard";
                    }
                }
            }

             //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i profil sidan: " + ex.Message;
            }
        }


        // Hämtar väggmeddelandet och skriver ut det i textboxen
        public void getWallMessage()
        {
            try
            {
                List<WallEntities> listWallMessages = d.getWallMessages(HttpContext.Current.User.Identity.Name);

                if (listWallMessages != null)
                {
                    WalltxtBox.Text = "";

                    for (int i = 0; i < listWallMessages.Count; i++)
                    {
                        WalltxtBox.Text += "Från: " + d.GetNameByID(listWallMessages[i].AnvändarID) + Environment.NewLine + "Meddelande :" + listWallMessages[i].Meddelande + Environment.NewLine + "Tid :" + listWallMessages[i].Tid + Environment.NewLine + Environment.NewLine;
                    }
                }
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i profil sidan: " + ex.Message;
            }
        }


        // Denna metod är till för att få in texten i väggmeddelandet
        public void SaveWallMessage()
        {
            try
            {
                string till = HttpContext.Current.User.Identity.Name;
                string meddelande = WalltxtMeddelande.Text;
                WalltxtMeddelande.Text = "";
                d.saveWallMessages(HttpContext.Current.User.Identity.Name, till, meddelande);
                getWallMessage();
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i profil sidan: " + ex.Message;
            }
        }


        // Denna knapp sparar väggmeddelandet
        protected void btnSaveWallMsg_Click(object sender, EventArgs e)
        {
            SaveWallMessage();
        }


        // Denna knapp hänvisar dig så du kan redigera din profil
        protected void btnPRedigera_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");  
        }
    }
}