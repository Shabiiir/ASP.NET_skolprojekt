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
    public partial class Friends : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer D = new DataLayer();
        
        string undantag = string.Empty;


        //Skulle man inte vara behörig för att komma in så hänvisas man till startsidan
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.Identity.IsAuthenticated))
            {
                Response.Redirect("signIn.aspx");
            }
        }


        // kör två metoder när sidan laddas. Alla vänner och vännförfrågningar syns direkt
        protected void Page_Load(object sender, EventArgs e)
        {
            setFriends();
            showFriendRequest();
        }


        //Hämtar användarens vänner.
        public void setFriends()
        {
            try
            {
                lblFriends.Text = "Dina vänlista är tom :(... ";
                List<USerEntities> listFriends = D.GetFriends(HttpContext.Current.User.Identity.Name);
                
                //Om listan är inte tom så sätt användarens information ut.
                //Listan skall aldrig vara tom(om nu det händer).
                if (listFriends != null)
                {
                    lblFriends.Text = "Dina Vänner: <br />";
                    for (int i = 0; i < listFriends.Count; i++)
                    {
                        lblFriends.Text += "<img src='/images/" + listFriends[i].Bild + "' id='Picture' width='50' height='50'/>" + "Namn: " + listFriends[i].FNamn + " " + listFriends[i].ENamn + ". Användare: " + listFriends[i].Användarnamn + "<a class='Links' id='HyperLink" + (i) + "' href='ProfileByID.aspx?AnvID=" + listFriends[i].ID + "'>           - Visa </a>" + "<br />" + "<br />";
                    }
                }
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i vänsidan: " + ex.Message;
            }
        }


        //Hämtar alla vänförfrågningar till användaren.
        public void showFriendRequest()
        {
            try
            {
                List<FriendRequests> listFriendRequests = D.showFriendRequest(HttpContext.Current.User.Identity.Name);
                lblFriendRequests.Text = "Inga nya vänförfrågningar!";

                if (listFriendRequests != null)
                {
                    lblFriendRequests.Text = "Dina vänförfrågningar! <br /> <br /> ";
                    for (int i = 0; i < listFriendRequests.Count; i++)
                    {
                        lblFriendRequests.Text += "Användare: " + D.GetNameByID(listFriendRequests[i].Avskickare) + "<a id='hyperLink2" + (i) + "' Class='Links' href='ProfileByID.aspx?AnvID=" + listFriendRequests[i].Avskickare + "'> - Visa profil </a> <br /> <br />";
                    }
                }
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i vänsidan: " + ex.Message;
            }
        }
    }
}