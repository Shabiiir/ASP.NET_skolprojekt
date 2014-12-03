using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Enteties;

namespace WebApplication
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();

        string undantag = string.Empty;


        //Om användaren är inloggad och klickar på "Loggan" ska hänvisas till Home.
        //Om användaren inte är inloggad ska personen hänvisas till SignIn
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserFunctionality();

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    HeaderText.NavigateUrl = "Home.aspx";
                    NewMessages();
                }

                else
                {
                    HeaderText.NavigateUrl = "SignIn.aspx";
                }
            }

            catch (Exception ex)
            {
                undantag = "följande fel har inträffat " + ex.Message;
            }
        }

        // När användaren är inloggad så visas knapparna SignOut och Meddelanden
        // Är användaren inte inloggad syns bara en Bli medlem text som hänvisar den till registrerings sidan
        public void LoadUserFunctionality()
        {
            try
            {   
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ltrUserName.Text = "User: " + HttpContext.Current.User.Identity.Name;
                    btnSignOut.Text = "Sign Out";
                    btnSignOut.Visible = true;
                    btnMessage.Visible = true;
                }

                else
                {
                    ltrUserName.Text = "<a class='Links' href='SignUp.aspx?'> Bli medlem! </a>";
                    btnSignOut.Visible = false;
                    btnMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                undantag = "följande fel har inträffat " + ex.Message;
            }
        }


        // Denna metod loggar ut användaren, sätter ut värdet Bli medlem och gör knappen logga ut osynlig
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            try
            {
                LogOutFunction();
                ltrUserName.Text = "<a class='Links' href='SignUp.aspx?'> Bli medlem! </a>";
                btnSignOut.Visible = false;
                Response.Redirect("SignIn.aspx");
            }

            catch (Exception ex)
            {
                undantag = "följande fel har inträffat " + ex.Message;
            }
        }


        //Detta är en logga ut funktion
        public void LogOutFunction()
        {
            try
            {
                FormsAuthentication.SignOut();
            }

            catch (Exception ex)
            {
                undantag = "följande fel har inträffat " + ex.Message;
            }
        }


        // Denna funktion visar på en knapp hur många meddelanden användaren har
        // Skulle man läsa ett av meddelanden så räknar den ner, har man inga meddelanden så är den annars 0;
        public void NewMessages()
        {
            try
            {
                string number = "0";
                if (d.ShowMessages(HttpContext.Current.User.Identity.Name).ToString() != null || d.ShowMessages(HttpContext.Current.User.Identity.Name).ToString() != "" || d.ShowMessages(HttpContext.Current.User.Identity.Name).ToString() != "0")
                {
                    number = d.ShowMessages(HttpContext.Current.User.Identity.Name).ToString();
                }
                btnMessage.Text = "Du har (" + number + ") nya meddelanden";
            }

            catch (Exception ex)
            {
                undantag = "följande fel har inträffat " + ex.Message;
            }
        }


        // Denna session är en rersursknapp för Svenska språket
        protected void LinkButtonSV_Click(object sender, EventArgs e)
        {
            Session["Culture"] = "sv-SE";
            Response.Redirect(Request.RawUrl);
        }


        // Denna session är en resursknapp för Engelska språket
        protected void LinkButtonEN_Click(object sender, EventArgs e)
        {
            Session["Culture"] = "en-GB";
            Response.Redirect(Request.RawUrl);
        }
           

        // Denna knapp tar dig till Meddelande sidan
        protected void btnMessage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Message.aspx");
        }
    }
}