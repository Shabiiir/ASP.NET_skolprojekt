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
    public partial class MessageAswer : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer D = new DataLayer();
        string undantag = string.Empty;
        int MessageFrom = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Om en användare är inloggad så hämtas info.
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    SetMessage();
                }

                //annars visas signin sidan.
                else
                {
                    Response.Redirect("SignIn.aspx");
                }
            }

             //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i meddelande sidan: " + ex.Message.ToString());
            }
        }


        //Hämtar information för den valda meddelandet.
        public void SetMessage()
        {
            try
            {
                MessageEntities MessEn = D.MessageByID(int.Parse(Request.QueryString["MID"]));
                MessageFrom = MessEn.Från;

                //ändrar meddelandets status till läst.
                D.MessageRead(MessEn.ID);
                lblMessageAnswer.Text = "Från: " + D.GetNameByID(MessEn.Från) + "<br /> <br /> Tid: " + MessEn.Tid + "<br /> <br /> Meddelande: " + MessEn.Meddelande + "<br /> <br />";
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i meddelande sidan: " + ex.Message.ToString());
            }
        }


        //Datalagret anropas och information skickas in för lagring.
        protected void btnMessageAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                D.SaveMessage(HttpContext.Current.User.Identity.Name, tbxMessageAnswer.Text, MessageFrom);
                Response.Redirect("Message.aspx");
            }

             //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i meddelande sidan: " + ex.Message.ToString());
            }
        }
    }
}