using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;
using Enteties;

namespace DAL
{
    public class DataLayer
    {
        string undantag = string.Empty;

        //ConectionString, pekar på databasen
        string SqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;


        //Hämtar ID med hjälp av användarnamnet.
        public int GetUserID(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                //kopplar mot databasen
                con = new SqlConnection(SqlConnectionString);

                //Öppnar koppling
                con.Open();

                //Ställer frågan.
                string fragan = @"select ID from användare where användarnamn = '" + user + "'";
                cmd = new SqlCommand(fragan, con);

                //returnerar svaret.
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }

            finally
            {
                // Om kopplingen har öppnats tidigare så ska den stängas nu.

                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Hämtar Användarnamnet med hjälp av ID.
        public string GetNameByID(int id)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                //kopplar mot databasen.
                con = new SqlConnection(SqlConnectionString);

                //Öppnar koppling.
                con.Open();

                //Ställer frågan.
                string fragan = @"select Användarnamn from användare where ID = '" + id + "'";
                cmd = new SqlCommand(fragan, con);

                //returnerar svaret.
                return cmd.ExecuteScalar().ToString();
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }

            finally
            {
                // Om kopplingen har öppnats tidigare så ska den stängas nu.
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Hämtar antalet olästa meddeladen för användaren 'user'.
        public string ShowMessages(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                //kopplar mot databasen.
                con = new SqlConnection(SqlConnectionString);

                //Öppnar koppling.
                con.Open();

                //Ställer frågan (antal olästa meddelanden)
                string fragan = @"select sum(Läst) from Meddelanden where till = " + GetUserID(user);
                cmd = new SqlCommand(fragan, con);
                return cmd.ExecuteScalar().ToString();
            }
            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }

            finally
            {
                // Om kopplingen har öppnats tidigare så ska den stängas nu.
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Metoden checkar om användarnamnet och lösenordet existerar i databasen.
        public bool SignIn(string user, string password)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            
            bool res = false;
            int resultat;

            try
            {
                //kopplar mot databasen.
                con = new SqlConnection(SqlConnectionString);
                //Öppnar koppling.
                con.Open();

                //Ställer frågan för att kontrollera om användarnamn och lösenord matchar
                string fragan = @"select count(*) from användare where användarnamn = @UserName
	                            AND Password = @Password COLLATE SQL_Latin1_General_CP1_CS_AS
	                            AND Password = @Password";

                // Parameter för Användare
                cmd = new SqlCommand(fragan, con);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserName";
                param1.Value = user;

                // Parameter för Lösenord
                cmd.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@Password";
                param2.Value = password;
                cmd.Parameters.Add(param2);

                // Om matchningen passade returnerar en etta som i sin tur släpper in användaren i sidan
                resultat = Convert.ToInt32(cmd.ExecuteScalar());
                if (resultat == 1)
                {
                    res = true;
                }

                return res;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        // Registrerar en ny användare i databasen
        public void RegisterUser(string user, string pass, string fname, string ename, string mail, DateTime age, string sex, string area, string job, string about, string pic, DateTime pre)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Insert Into användare values(@User, @Password, @FName, @EName, @EMail, @Ålder, @Kön, @Bor, @Sysselsätning, @OmMig, @Bild, @Pre)";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@User", user);
                    SqlParameter p2 = new SqlParameter("@Password", pass);
                    SqlParameter p3 = new SqlParameter("@FName", fname);
                    SqlParameter p4 = new SqlParameter("@EName", ename);
                    SqlParameter p5 = new SqlParameter("@EMail", mail);
                    SqlParameter p6 = new SqlParameter("@Ålder", age);
                    SqlParameter p7 = new SqlParameter("@Kön", sex);
                    SqlParameter p8 = new SqlParameter("@Bor", area);
                    SqlParameter p9 = new SqlParameter("@OmMig", about);
                    SqlParameter p10 = new SqlParameter("@Bild", pic);
                    SqlParameter p11 = new SqlParameter("@Pre", pre);
                    SqlParameter p12 = new SqlParameter("@Sysselsätning", job);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p8);
                    cmd.Parameters.Add(p9);
                    cmd.Parameters.Add(p10);
                    cmd.Parameters.Add(p11);
                    cmd.Parameters.Add(p12);
                    //Sql frågan körs och ingen svar väntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        //Hämtar användar information från en lista av användare och skickar tillbaka den.
        public List<USerEntities> GetUSerInfo(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader SqlDre = null;

            List<USerEntities> UsEnli = new List<USerEntities>();

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from användare where ID = @UserID";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = GetUserID(user);
                cmd.Parameters.Add(param1);
                SqlDre = cmd.ExecuteReader();

                // Loopar igenom listan och tar emot attributen
                while (SqlDre.Read())
                {
                    UsEnli.Add(new USerEntities
                    {
                        ID = (int)SqlDre["ID"],
                        FNamn = SqlDre["FNamn"].ToString(),
                        ENamn = SqlDre["ENamn"].ToString(),
                        EMail = SqlDre["EMail"].ToString(),
                        Ålder = (DateTime)SqlDre["Ålder"],
                        Kön = SqlDre["Kön"].ToString(),
                        Bor = SqlDre["Bor"].ToString(),
                        Sysselsättning = SqlDre["Sysselsättning"].ToString(),
                        OmMig = SqlDre["OmMig"].ToString(),
                        Bild = SqlDre["Bild"].ToString(),
                        Pre = (DateTime)SqlDre["Pre"]
                    });
                }
                return UsEnli;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (SqlDre != null)
                {
                    SqlDre.Close();
                }
            }
        }


        //Hämtar alla meddelanden för en specifik användare.
        public List<MessageEntities> Messeges(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // Skapar nya objekt och en lista av meddelande entiteten
            MessageEntities MessEn = new MessageEntities();
            List<MessageEntities> MessEnLi = new List<MessageEntities>();

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from Meddelanden where Till = @UserID order by Tid desc";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = GetUserID(user);
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MessEnLi.Add(new MessageEntities() { ID = (int)rdr["ID"], Från = (int)rdr["Från"], Till = (int)rdr["Till"], Meddelande = rdr["Meddelande"].ToString(), Läst = (int)rdr["Läst"], Tid = (DateTime)rdr["Tid"] });
                }
                return MessEnLi;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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


        //Hämtar dom fem senaste meddelanden för en specifik användare.
        public List<MessageEntities> GetLast5Messeges(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // Skapar nya objekt och en lista av meddelande entiteten
            MessageEntities MessEn = new MessageEntities();
            List<MessageEntities> MessEnLi = new List<MessageEntities>();

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select top 5 * from Meddelanden where Till = @UserID order by Tid desc";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = GetUserID(user);
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    MessEnLi.Add(new MessageEntities() { ID = (int)rdr["ID"], Från = (int)rdr["Från"], Till = (int)rdr["Till"], Meddelande = rdr["Meddelande"].ToString(), Läst = (int)rdr["Läst"], Tid = (DateTime)rdr["Tid"] });
                }
                return MessEnLi;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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


        //Hämtar ett meddelande Med hjälp av ID.
        public MessageEntities MessageByID(int id)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            //Skapar ett nytt objekt av klassen
            MessageEntities MessEn = new MessageEntities();

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select * from Meddelanden where ID = @MID";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@MID";
                param1.Value = id;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();

                // Lyckas den läsa så tilldelar den värdena
                if (rdr.Read())
                {
                    MessEn.ID = (int)rdr["ID"];
                    MessEn.Från = (int)rdr["Från"];
                    MessEn.Till = (int)rdr["Till"];
                    MessEn.Meddelande = rdr["Meddelande"].ToString();
                    MessEn.Läst = (int)rdr["Läst"];
                    MessEn.Tid = (DateTime)rdr["Tid"];
                }

                return MessEn;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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
        public void SaveMessage(string from, string message, int to)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Insert Into Meddelanden values(@Från, @Till, @Meddelande, @Läst, @Tid)";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sqlparametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@Från", GetUserID(from));
                    SqlParameter p2 = new SqlParameter("@Till", to);
                    SqlParameter p3 = new SqlParameter("@Meddelande", message);
                    SqlParameter p4 = new SqlParameter("@Läst", 1);
                    SqlParameter p5 = new SqlParameter("@Tid", DateTime.Now);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    //sql frågan körs och ingen svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

           //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        //Sätter läst till 0, vilket betyder att meddelandet är läst.
        public void MessageRead(int id)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"Update Meddelanden set läst = 0 where ID = @MID";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@MID";
                param1.Value = id;
                cmd.Parameters.Add(param1);
                cmd.ExecuteScalar();
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            //Skapar ett nytt objekt av klassen
            WallEntities WallEn = new WallEntities();
            List<WallEntities> WallEnLi = new List<WallEntities>();

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"SELECT * from Väggmeddelande where  MottagarID = @UserID order by tid desc";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = GetUserID(user);
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    WallEnLi.Add(new WallEntities() { ID = (int)rdr["ID"], AnvändarID = (int)rdr["AnvändarID"], Meddelande = rdr["Meddelande"].ToString(), MottagarID = (int)rdr["MottagarID"], Tid = (DateTime)rdr["Tid"] });
                }
                return WallEnLi;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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
        public void saveWallMessages(string from, string to, string message)
        {
            try
            {
                DateTime date = DateTime.Now;
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Insert Into Väggmeddelande values(@Från, @Meddelande, @Mottagare, @Tid)";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@Från", GetUserID(from));
                    SqlParameter p2 = new SqlParameter("@Meddelande", message);
                    SqlParameter p3 = new SqlParameter("@Mottagare", GetUserID(to));
                    SqlParameter p4 = new SqlParameter("@Tid", date);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    //sql frågan körs, ingen svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

          //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        //Hämtar den loggade användarens vänner.
        public List<USerEntities> GetFriends(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // skapar nya objekt och lista från klassen
            USerEntities UsEn = new USerEntities();
            List<USerEntities> UsEnli = new List<USerEntities>();
            int userID = GetUserID(user);

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select A1.* from Användare AS A1 join Vänner on Vänner.ID2 = A1.ID where ID1 = @UserID 
                                    union
                                    select A1.* from Användare AS A1 join Vänner on Vänner.ID1 = A1.ID where ID2 = @UserID";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = userID;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    UsEnli.Add(new USerEntities() { ID = (int)rdr["ID"], Användarnamn = rdr["Användarnamn"].ToString(), FNamn = rdr["FNamn"].ToString(), ENamn = rdr["ENamn"].ToString(), Bild = rdr["Bild"].ToString() });
                }
                return UsEnli;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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


        //uppdaterar basic informationen om användaren.
        public void UpdateUserInfo(string user, string fname, string ename, string mail, DateTime age, string area, string job, string about)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Update användare set FNamn = @FName, Enamn = @EName, Email = @EMail, Ålder = @Ålder, Bor = @Bor, Sysselsättning = @Sysselsätning, Ommig = @OmMig where ID = @userID";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@UserID", GetUserID(user));
                    SqlParameter p2 = new SqlParameter("@FName", fname);
                    SqlParameter p3 = new SqlParameter("@EName", ename);
                    SqlParameter p4 = new SqlParameter("@EMail", mail);
                    SqlParameter p5 = new SqlParameter("@Ålder", age);
                    SqlParameter p6 = new SqlParameter("@Bor", area);
                    SqlParameter p7 = new SqlParameter("@OmMig", about);
                    SqlParameter p8 = new SqlParameter("@Sysselsätning", job);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p8);
                    //sql frågan körs ingen svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        //uppdaterar användarens bild.
        public void UpdateProfilePicture(string user, string image)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Update användare set  Bild = @Bild where ID = @userID";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@UserID", GetUserID(user));
                    SqlParameter p2 = new SqlParameter("@Bild", image);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    //sql frågan körs ingen svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        // Hämtar lösenordet för att kunna ändra det 
        public string getPassword(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            string resultat;

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select Password from användare where användarnamn = '" + user + "'";
                cmd = new SqlCommand(fragan, con);
                resultat = cmd.ExecuteScalar().ToString();
                return resultat;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        // Uppdaterar lösenordet på användaren
        public void UpdatePassword(string user, string pass)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                //kopplar mot databasen.
                con = new SqlConnection(SqlConnectionString);

                //Öppnar koppling.
                con.Open();

                //Ställer frågan.
                string fragan = @"Update Användare Set Password = '" + pass + "' where ID = " + GetUserID(user);

                cmd = new SqlCommand(fragan, con);
                cmd.ExecuteNonQuery();
            }

            //om någont går fel undantag kastas.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }

            finally
            {
                // Om kopplingen har öppnats tidigare så ska den stängas nu.
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Hämtar alla användare
        public List<USerEntities> GetAllUsers()
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // Skapar nya objekt av klassen och listan
            USerEntities UseEnt = new USerEntities();
            List<USerEntities> listAll = new List<USerEntities>();
            
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
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

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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


        //Visar upp alla vänförfrågan som user har fått.
        public List<FriendRequests> showFriendRequest(string user)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // skapar nya objekt av klassen och listan
            FriendRequests friReq = new FriendRequests();
            List<FriendRequests> friendRequestList = new List<FriendRequests>();
            int userID = GetUserID(user);

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"SELECT * FROM VänFörfrågan WHERE Tillfrågad = @UserID";
                cmd = new SqlCommand(fragan, con);

                // Tar in en parameter för att läsa av den
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@UserID";
                param1.Value = userID;
                cmd.Parameters.Add(param1);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    friendRequestList.Add(new FriendRequests() { ID = (int)rdr["ID"], Avskickare = (int)rdr["Avskickare"], TillFrågad = (int)rdr["TillFrågad"] });
                }
                return friendRequestList;
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
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


        //Sparar en ny vänförfrågan i databasen
        public void saveFriendRequest(int tillfrågad, string avskickare)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "Insert Into Vänförfrågan values(@avskickare, @Tillfrågad)";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@avskickare", GetUserID(avskickare));
                    SqlParameter p2 = new SqlParameter("@TillFrågad", tillfrågad);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    //sql frågan körs ingen svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }

             //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }


        //Returnerar sant om userID1 och UserID2 är vänner.
        public bool Friends(string user1, int userID2)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            int resultat;
            int userID1 = GetUserID(user1);

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select count(ID) from vänner where (ID1=" + userID1 + " and ID2=" + userID2 + ") or (ID1=" + userID2 + " and ID2=" + userID1 + ")";
                cmd = new SqlCommand(fragan, con);
                resultat = Convert.ToInt32(cmd.ExecuteScalar());

                if (resultat > 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Returnerar Sant om userID1 har fått förfrågan från userID2
        public bool GotFriendRequest(int userID1, int userID2)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            int resultat;

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select count(ID) from VänFörfrågan where TillFrågad =" + userID1 + " and Avskickare  =" + userID2;
                cmd = new SqlCommand(fragan, con);
                resultat = Convert.ToInt32(cmd.ExecuteScalar());

                if (resultat > 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Returnerar Sant om userID1 har skickat förfrågan till userID2
        public bool SentFriendRequest(int userID1, int userID2)
        {
            //Sätter SqlConnection och SqlCommand till null innan man öppnar dom
            SqlConnection con = null;
            SqlCommand cmd = null;
            int resultat;

            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                con = new SqlConnection(SqlConnectionString);
                con.Open();
                string fragan = @"select Count(id) from VänFörfrågan where TillFrågad =" + userID2 + " and Avskickare  =" + userID1;
                cmd = new SqlCommand(fragan, con);
                resultat = Convert.ToInt32(cmd.ExecuteScalar());

                if (resultat > 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            // Om kopplingen har öppnats tidigare så ska den stängas nu.
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Raderar från Friendrequest och lägger till i friends om användaren har klickat på acceptera.
        public void AcceptOrDenyFriendRequest(int userID1, int userID2, int acceptOrDeny)
        {
            try
            {
                // Öppnar uppkopplingen för att ställa frågan
                // Tar bort användaren från vänförfrågnings listan om användaren nekats
                var connection = new SqlConnection(SqlConnectionString);
                string commandString1 = "delete from VänFörfrågan where tillfrågad = @TillFrågad and avskickare = @Avskickare";
                using (SqlCommand cmd = new SqlCommand(commandString1, connection))
                {
                    //Öppnar koopling
                    connection.Open();
                    //Sql parametrar blir till delade värden.
                    SqlParameter p1 = new SqlParameter("@TillFrågad", userID1);
                    SqlParameter p2 = new SqlParameter("@Avskickare", userID2);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    //sql frågan körs inget svar förväntas.
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                // Om användaren accepterar vänförfrågan så läggs personen till i vännlistan
                if (acceptOrDeny == 1)
                {
                    string commandString2 = "insert into vänner values(@ID1, @ID2)";
                    using (SqlCommand cmd = new SqlCommand(commandString2, connection))
                    {
                        connection.Open();
                        SqlParameter p1 = new SqlParameter("@ID1", userID1);
                        SqlParameter p2 = new SqlParameter("@ID2", userID2);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            //om någont går fel kastas ett undantag.
            catch (Exception ex)
            {
                //Fel meddelande kastas.
                throw new Exception(undantag = "Ohanterat undantag i datalagret: " + ex.Message.ToString());
            }
        }
    }
}