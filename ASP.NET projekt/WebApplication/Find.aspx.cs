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
    public partial class Find : System.Web.UI.Page
    {
        string undantag = string.Empty;

        //Om användaren inte är en premium användare så skickas han tillbaka till hem sidan
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!(HttpContext.Current.User.Identity.IsAuthenticated))
                {
                    Response.Redirect("signIn.aspx");
                }
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                undantag = "Ohanterat undantag i hittasidan: " + ex.Message;
            }
        }  
    }
}