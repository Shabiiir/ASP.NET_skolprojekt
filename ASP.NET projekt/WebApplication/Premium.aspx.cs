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
    public partial class Premium : System.Web.UI.Page
    {
        // skapar en instans av datalagret för att kunna använda det i andra metoder
        DataLayer d = new DataLayer();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && IsUserPremium())
            {
                lblPremium.Text = "Du har ett premium konto. Grattis!!!";
            }
            
            else
            {
                Response.Redirect("SignIn.aspx"); //annars hänvisas man till signin sidan.
            }

        }


        public bool IsUserPremium()
        {
            //Anropar Datalagretoch får tillbaka en lista med information.
            List<USerEntities> listUserInfo = d.GetUSerInfo(HttpContext.Current.User.Identity.Name);

            bool resault = false;

            if (listUserInfo != null)
            {
                DateTime x = new DateTime(2000, 01, 01);

                if (listUserInfo[0].Pre != x)
                {
                    resault= true;
                }
            }
            return resault;
        }
    }
}