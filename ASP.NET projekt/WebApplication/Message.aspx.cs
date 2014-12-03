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
    public partial class Message : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();
        string undantag = string.Empty;


        // Om användaren är inloggad kan han ta sig vidare till meddelande fliken, om inte så hänvisas han till inloggnings sidan
        protected void Page_init(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                getMessages();
            }

            else
            {
                Response.Redirect("SignIn.aspx"); // Hänvisar till logga in sidan annars
            }

        }

        
        //Ska hämta alla meddelanden som har skickats till användaren
        public void getMessages()
        {
            try
            {
                List<MessageEntities> MessageList = d.Messeges(HttpContext.Current.User.Identity.Name);

                //Om listan är inte tom så sätt användarens information ut.
                //Listan skall aldrig vara tom(om nu det händer).

                if (MessageList != null && MessageList.Count>0)
                {
                    lblMessagesName.Text = "Dina meddelanden: <br /> <br /> ";

                    // Hämtar meddelanden och skrivs ut i en label. därifrån kan man klicka sig vidare till meddelandet för att svara
                    for (int i = 0; i < MessageList.Count; i++)
                    {
                        string message="";
                        if ((MessageList[i].Meddelande).Length > 10)
                            {
                                message= MessageList[i].Meddelande.Substring(0, 10) + "...";
                            }
                        else{
                                 message=MessageList[i].Meddelande;
                            }
                        string info = "Från: " + d.GetNameByID(MessageList[i].Från) + "<br /> Tid: " + MessageList[i].Tid + "<br /> Meddelande: " + message;


                        lblMessagesName.Text += "<a class='Links' id='HyperLink" + (i) + "' href='MessageAswer.aspx?MID=" + MessageList[i].ID + "'>" + info + "</a> <br /> <br />";
                    }
                }

                else
                {
                    lblMessagesName.Text = "Du har tyvärr inga nya meddelanden!";
                }
            }

             //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i meddelande sidan: " + ex.Message;
            }
        }
    }
}