using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using DAL;
using Enteties;

namespace WebApplication
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer D = new DataLayer();

        string undantag = string.Empty;


        //
        protected void page_init(object sender, EventArgs e)
        {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Home.aspx");
                }

                else
                {
                    SetExample();
                }

        }

        //Om det är fler än 2 användare i databasen så ska en användare inte visas mer än 1 gång.
        public void SetExample()
        {
            try
            {
                List<USerEntities> allUsers = D.GetAllUsers();

                if (allUsers != null && allUsers.Count >0)
                {
                    Random numbers = new Random();

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

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i Loggin sidan: " + ex.Message;
            }
        }


        public bool connectionToDAL()
        {
            return D.SignIn(UserNametxb.Text, Passwordtxb.Text);
        }


        protected override void InitializeCulture()
        {
            try
            {
                Culture = Session["Culture"] as string ?? "sv-SE";
                UICulture = Session["Culture"] as string ?? "sv-SE";
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Felmeddelande kastas.
                undantag = "Ohanterat undantag i Loggin sidan: " + ex.Message;
            }
        }


        protected void regbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }


        protected void Loginbtn_Click(object sender, EventArgs e)
        {
                if (connectionToDAL())
                {
                    FormsAuthentication.SetAuthCookie(UserNametxb.Text, false);
                    Response.Redirect("Home.aspx");
                }

                else
                {
                    lblWrongPass.Text = "Fel användarnamn eller Lösenord";
                }

        }
    }
}