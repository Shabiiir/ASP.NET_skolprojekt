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
    public partial class ProfileByID : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();

        string undantag = string.Empty;
        int userID;
        

        protected void Page_Init(object sender, EventArgs e)
        {
            userID = int.Parse(Request.QueryString["AnvID"]);
            GetUserInfo();
        }


        //Metod som körs när programet går igång.
        public void GetUserInfo()
        {
            
            //Om en användare är inloggad så hämtas info.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                setUserInfo();
                getWallMessage();
            }

            //annars visas signin sidan.
            else
            {
                Response.Redirect("SignIn.aspx");
            }
        }


        //Sätter användarens infromation på profilsidan.
        public void setUserInfo()
        {
            try
            {
                if (!(d.Friends(HttpContext.Current.User.Identity.Name, userID)))
                {
                    if (d.GotFriendRequest(d.GetUserID(HttpContext.Current.User.Identity.Name), userID))
                    {
                        Accept.Visible = true;
                        Deny.Visible = true;
                        FriendStatus.Visible = false;
                    }

                    else if (d.SentFriendRequest(d.GetUserID(HttpContext.Current.User.Identity.Name), userID))
                    {
                        FriendStatus.Text = "Vänförfrågan har skickats.";
                    }

                    else
                    {
                        SendFriendRequest.Visible = true;
                        FriendStatus.Visible = false;
                    }
                }

                //Anropar Datalagretoch får tillbaka en lista med information.
                List<USerEntities> listUserInfo = d.GetUSerInfo(d.GetNameByID(userID));

                //Om listan är inte tom så sätt användarens information ut.
                //Listan skall aldrig vara tom(om nu det händer).
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
                    DateTime x = new DateTime(2000, 01, 01);

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

             //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i profil sidan: " + ex.Message;
            }
        }


        public void getWallMessage()
        {
            try
            {
                List<WallEntities> listWallMessages = d.getWallMessages(d.GetNameByID(userID));

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


        public void saveWallMessage()
        {
            try
            {
                string till = d.GetNameByID(int.Parse(Request.QueryString["AnvID"]));
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


        protected void btnSaveWallMessage_Click(object sender, EventArgs e)
        {
            saveWallMessage();
        }


        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            d.SaveMessage(HttpContext.Current.User.Identity.Name, tbxSkickaMedd.Text, userID);
            lblSkickaMedd.Text = "Meddelandet har skickats!";
            tbxSkickaMedd.Text = "";
        }


        protected void SendFriendRequest_Click(object sender, EventArgs e)
        {
            d.saveFriendRequest(userID, HttpContext.Current.User.Identity.Name);
            Response.Redirect(Request.RawUrl);
        }


        protected void Accept_Click(object sender, EventArgs e)
        {
            d.AcceptOrDenyFriendRequest(d.GetUserID(HttpContext.Current.User.Identity.Name), userID, 1);
            Response.Redirect(Request.RawUrl);
        }


        protected void Deny_Click(object sender, EventArgs e)
        {
            d.AcceptOrDenyFriendRequest(d.GetUserID(HttpContext.Current.User.Identity.Name), userID, 0);
            Response.Redirect(Request.RawUrl);
        }
    }
}