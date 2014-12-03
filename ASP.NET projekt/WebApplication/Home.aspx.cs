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
    public partial class Home : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer D = new DataLayer();
        
        string CurrentUSer = "";
        string undantag = string.Empty;

        
        // Är man inte inloggade så hänvisas man till startsidan eller så så kommer man in och ser välkomst texten
        protected void Page_Init(object sender, EventArgs e)
        {
                if (!(HttpContext.Current.User.Identity.IsAuthenticated))
                {
                    Response.Redirect("signIn.aspx");
                }

                SetExample();
                CurrentUSer = HttpContext.Current.User.Identity.Name;
                lblHome1.Text = "Välkommen till den heta dejting sidan " + lblUserName.Text + "!!! <br />";
                lblHome1.Text += "Börja träffa andra singlar redan nu... <br /> Vi erbjuder den perfekta matchningen för <br /> just dig!!!";
        }

        //sätter ut exempel användare.
        public void SetExample()
        {
            try
            {
                List<USerEntities> allUsers = D.GetAllUsers();

                if (allUsers != null)
                {
                    Random numbers = new Random();

                    //Om det är fler än 2 användare i databasen så ska en användare inte visas mer än 1 gång.
                    if (allUsers.Count > 2)
                    {
                        int user1 = numbers.Next(allUsers.Count);
                        Image1.ImageUrl = "~/images/" + allUsers[user1].Bild;
                        Image1Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user1].ID;
                        allUsers.RemoveAt(user1);

                        int user2 = numbers.Next(allUsers.Count);
                        Image2.ImageUrl = "~/images/" + allUsers[user2].Bild;
                        Image2Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user2].ID;
                        allUsers.RemoveAt(user2);

                        int user3 = numbers.Next(allUsers.Count);
                        Image3.ImageUrl = "~/images/" + allUsers[user3].Bild;
                        Image3Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user3].ID;
                        allUsers.RemoveAt(user3);
                    }

                    else
                    {
                        int user1 = numbers.Next(allUsers.Count);
                        Image1.ImageUrl = "~/images/" + allUsers[user1].Bild;
                        Image1Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user1].ID;

                        int user2 = numbers.Next(allUsers.Count);
                        Image2.ImageUrl = "~/images/" + allUsers[user2].Bild;
                        Image2Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user2].ID;

                        int user3 = numbers.Next(allUsers.Count);
                        Image3.ImageUrl = "~/images/" + allUsers[user3].Bild;
                        Image3Src.NavigateUrl = "~/ProfileByID.aspx?AnvID=" + allUsers[user3].ID;
                    }
                }
            }

             //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i Hem sidan: " + ex.Message;
            }
        }
    }
}