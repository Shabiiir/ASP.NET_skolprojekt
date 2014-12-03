using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Web.Configuration;

namespace WebApplication
{
    public class DAL
    {
  //       <connectionStrings>
  //  <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=aspnet-WebApplication-20131219151719;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\aspnet-WebApplication-20131219151719.mdf" providerName="System.Data.SqlClient"/>
  //  <add name="DatabaseEntities" connectionString="metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string='data source=(LocalDB)\v11.0;attachdbfilename=&quot;|DataDirectory|\Database.mdf&quot;;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework'" providerName="System.Data.EntityClient"/>
  //  <add name="DatabaseConnecstionString" connectionString="Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" providerName="System.Data.SqlClient"/>
  //</connectionStrings>

        string SqlConnectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnecstionString"].ToString();
        
        public int GetUserID(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int resultat;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select ID from användare where användarnamn = '"+user+"'";
                cmd = new SqlCommand(fragan, con);
                resultat = Convert.ToInt32(cmd.ExecuteScalar());
                return resultat;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public string GetNameByID(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            string resultat;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select Användarnamn from användare where ID = '" + id + "'";
                cmd = new SqlCommand(fragan, con);
                resultat = cmd.ExecuteScalar().ToString();
                return resultat;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //Hämtar alla olästa meddeladen för den in loggade användaren.
        public string ShowMessages(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select sum(Läst) from Meddelanden where till = " + GetUserID(user);
                cmd = new SqlCommand(fragan, con);
                return cmd.ExecuteScalar().ToString();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int SignIn(string user, string password)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int resultat;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select count(*) from användare where användarnamn like @UserName and Password like @Password";  // and CAST(Password AS varbinary(25)) like CAST(@Password AS varbinary(25))";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserName";
                param1.Value = user;
                cmd.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@Password";
                param2.Value = password;
                cmd.Parameters.Add(param2);

                resultat = Convert.ToInt32(cmd.ExecuteScalar());
                return resultat;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public void RegisterUser(string user, string pass, string fname, string ename, string mail, DateTime age, string sex, string area, string job, string about, string pic, DateTime pre)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Användare anv = new Användare();

            anv.Användarnamn = user;
            anv.Password = pass;
            anv.FNamn = fname;
            anv.ENamn = ename;
            anv.Email = mail;
            anv.Ålder = age;
            anv.Kön = sex;
            anv.Bor = area;
            anv.Sysselsättning = job;
            anv.OmMig = about;
            anv.Pre = pre;
            anv.Bild = pic;
            db.Användares.InsertOnSubmit(anv);
            db.SubmitChanges();
        }

        //Hämtar användar information och skickar tillbaka.
        public List<USerEntities> SetProfileInfo(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            USerEntities E = new USerEntities();
            List<USerEntities> list = new List<USerEntities>();
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from användare where användarnamn like @UserName";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserName";
                param1.Value = user;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    E.ID = (int)rdr["ID"];
                    E.FNamn = rdr["FNamn"].ToString();
                    E.ENamn = rdr["ENamn"].ToString();
                    E.EMail = rdr["EMail"].ToString();
                    E.Ålder = (DateTime)rdr["Ålder"];
                    E.Kön = rdr["Kön"].ToString();
                    E.Bor = rdr["Bor"].ToString();
                    E.Sysselsättning = rdr["Sysselsättning"].ToString();
                    E.OmMig = rdr["OmMig"].ToString();
                    E.Bild = rdr["Bild"].ToString();
                    E.Pre = (DateTime)rdr["Pre"];
                    list.Add(E);
                }
                return list;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if(rdr != null)
                {
                    rdr.Close();
                }

            }
        }

        //Hämtar alla meddelanden för en specifik användare.
        public List<MessageEntities> Messeges(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            MessageEntities M = new MessageEntities();
            List<MessageEntities> ml = new List<MessageEntities>();
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from Meddelanden where Till = @UserID order by Tid desc";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = GetUserID(user);
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    ml.Add(new MessageEntities() { ID = (int)rdr["ID"], Från = (int)rdr["Från"], Till = (int)rdr["Till"], Meddelande = rdr["Meddelande"].ToString(), Läst = (int)rdr["Läst"], Tid = (DateTime)rdr["Tid"] });
                }
                return ml;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }

            }
        }

        //Hämtar en meddelande Med hjälp av ID.
        public MessageEntities MessageByID(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            MessageEntities M = new MessageEntities();
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from Meddelanden where ID = @MID";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@MID";
                param1.Value = id;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    M.ID = (int)rdr["ID"];
                    M.Från = (int)rdr["Från"];
                    M.Till = (int)rdr["Till"];
                    M.Meddelande = rdr["Meddelande"].ToString();
                    M.Läst = (int)rdr["Läst"];
                    M.Tid = (DateTime)rdr["Tid"];
                }
                return M;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }

            }
        }

        //Sparar skickat meddelande
        public void SaveMessage(int from, string message, int to)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Meddelanden M = new Meddelanden();
            M.Från = from;
            M.Meddelande = message;
            M.Till = to;
            M.Tid = DateTime.Now;
            M.Läst = 1;
            db.Meddelandens.InsertOnSubmit(M);
            db.SubmitChanges();
        }

        //Sätter läst till 0, vilket betyder att meddelandet är läst.
        public void MessageRead(int id)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"Update Meddelanden set läst = 0 where ID = @MID";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@MID";
                param1.Value = id;
                cmd.Parameters.Add(param1);
                cmd.ExecuteScalar();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
                
        }

        //Hämtar Väggmeddelande för en specefik användare.
        public List<WallEntities> getWallMessages(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            WallEntities W = new WallEntities();
            List<WallEntities> wl = new List<WallEntities>();
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"SELECT a2.id as Mottagare, Väggmeddelande.ID, Väggmeddelande.Meddelande, A1.id as Från , Väggmeddelande.Tid FROM Användare as A1, Användare AS A2, Väggmeddelande
                                      Where A1.ID = Väggmeddelande.AnvändarID 
                                      and a2.Användarnamn = @UserName order by tid desc";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserName";
                param1.Value = user;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wl.Add(new WallEntities() { ID = (int)rdr["ID"], AnvändarID = (int)rdr["Från"], Meddelande = rdr["Meddelande"].ToString(), MottagarID = (int)rdr["Mottagare"], Tid =(DateTime)rdr["Tid"]});
                    
                }
                return wl;



            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }

            }

        }

        //Sparar den skickade väggmeddelandet i databasen.
        public void saveWallMessages(string to, string meddelande)
        {

            DataClasses1DataContext db = new DataClasses1DataContext();

            Väggmeddelande vm = new Väggmeddelande();

            DAL d = new DAL();
            vm.MottagarID = d.GetUserID(to);
            vm.AnvändarID = d.GetUserID(HttpContext.Current.User.Identity.Name);
            vm.Meddelande = meddelande;
            vm.Tid = DateTime.Now;

            db.Väggmeddelandes.InsertOnSubmit(vm);
            db.SubmitChanges();
            
        }

        //Hämtar den loggade användarens vänner.
        public List<USerEntities> GetFriends(string user)
        {

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            USerEntities E = new USerEntities();
            List<USerEntities> list = new List<USerEntities>();
            int userID = GetUserID(user);
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select A1.* from Användare AS A1 join Vänner on Vänner.ID2 = A1.ID where ID1 = @UserID 
                                    union
                                    select A1.* from Användare AS A1 join Vänner on Vänner.ID1 = A1.ID where ID2 = @UserID";
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = userID;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new USerEntities() { ID = (int)rdr["ID"], Användarnamn = rdr["Användarnamn"].ToString(), FNamn = rdr["FNamn"].ToString(), ENamn = rdr["ENamn"].ToString(), Bild = rdr["Bild"].ToString() });
                }
                return list;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if(rdr != null)
                {
                    rdr.Close();
                }

            }
        
 
        }


        //uppdaterar basic info om användaren.
        public void UpdateUserInfo(int userID, string fname, string ename, string mail, DateTime age, string area, string job, string about)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Användare anv = db.Användares.Single(c => c.ID == userID);
            
                anv.FNamn = fname;
                anv.ENamn = ename;
                anv.Email = mail;
                anv.Ålder = age;
                anv.Bor = area;
                anv.Sysselsättning = job;
                anv.OmMig = about;
            
            // Spara ändringar till databasen. 
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                // för exceptions.
                Console.WriteLine(e);
                
            }

        }

        //uppdatera bild.
        public void UpdateProfilePicture(int userID, string image)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Användare anv = db.Användares.Single(c => c.ID == userID);

            anv.Bild = image;

            db.SubmitChanges();
        }

        //get user pass
        public string getPassword(string user)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            string resultat;
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select Password from användare where användarnamn = '" + user + "'";
                cmd = new SqlCommand(fragan, con);
                resultat = cmd.ExecuteScalar().ToString();
                return resultat;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
 
        }

        //set user pass
        public void UpdatePassword(int userID, string pass)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Användare anv = db.Användares.Single(c => c.ID == userID);

            anv.Password = pass;

            db.SubmitChanges();
        }


        //Hämtar alla användare
        public List<USerEntities> GetAllUsers()
        {
            List<USerEntities> listAll = new List<USerEntities>();
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            USerEntities U = new USerEntities();
            try
            {
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from Användare";
                cmd = new SqlCommand(fragan, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listAll.Add(new USerEntities() { ID = (int)rdr["ID"], Användarnamn = rdr["Användarnamn"].ToString(), FNamn = rdr["FNamn"].ToString(), ENamn = rdr["ENamn"].ToString(), Bild = rdr["Bild"].ToString() });
                }
                return listAll;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }

            }

        }
    }
    
}