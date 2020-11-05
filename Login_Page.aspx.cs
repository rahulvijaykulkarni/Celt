using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Globalization;
using System.Net;
using System.Net.Mail;

//using log4net;

public partial class Login_Page : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d_unit = new DAL();
    // link_url lu = new link_url();
    //private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Login_Page));
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        //{
        //    Response.Redirect("Login_Page.aspx");
        //}
        //Log.Info("Info Message.");
        //Log.Error("Error Message.");
        //Log.Debug("Debug Message.");

        string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        string months = System.DateTime.Today.ToShortDateString();
        DateTime month = DateTime.ParseExact(months, sysFormat, CultureInfo.InvariantCulture);
        string curr_month = month.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        Session["system_curr_date"] = curr_month;

        if (!Page.IsPostBack)
        {
            string comp_code = "";
            d.con.Open();
            MySqlCommand cmd2 = new MySqlCommand("select comp_code from pay_company_master limit 1", d.con);
            try
            {
                comp_code = (string)cmd2.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd2.Dispose();
                d.con.Close();
            }

            if (comp_code == "" || comp_code==null) {
                Session["comp_code"] = "0";
                Session["CHANGE_PASS"] = "0";
                Response.Redirect("CompanyMaster.aspx");
            }

            if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                // This is an unauthorized, authenticated request...
                Response.Redirect("~/unauthorised_access.aspx");
        }

    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        string uname = txt_login_id.Text;
        int userlen = txt_login_id.Text.Length;
        string pswd = txt_password.Text;
        int passlen = txt_password.Text.Length;
        int Count = 1;

        try
        {
            d1.con1.Open();

            if (uname == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter Login ID!!!');", true);
                txt_login_id.Text = "";
                txt_login_id.Focus();
                return;
            }
            else if (userlen >= 1)
            {
                MySqlCommand cmd = new MySqlCommand("select LOGIN_ID from pay_user_master", d1.con1);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr1 in dt.Rows)
                {
                    if (!Count.Equals(2))
                    {
                        if (uname == dr1["LOGIN_ID"].ToString())
                        {
                            Count = 2;
                            break;
                        }
                    }
                }
                if (Count == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Username/Password...!!!');", true);
                    txt_login_id.Text = "";
                    txt_password.Text = "";
                    txt_login_id.Focus();
                    return;
                }
            }
            if (Count == 2)
            {
                if (passlen >= 5)
                {
                    MySqlCommand cmd = new MySqlCommand("select LOGIN_ID,USER_PASSWORD,flag,admin_login from pay_user_master where login_id='" + txt_login_id.Text.ToString() + "'", d1.con1);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int count = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //if (dr["LOGIN_ID"].ToString() == "M02564") {
                        //    String flagpassword = dr["USER_PASSWORD"].ToString();
                        //}

                        if (uname == dr["LOGIN_ID"].ToString() && pswd == dr["USER_PASSWORD"].ToString())
                        {
                            count = 2;
                            String flag = "A";

                            flag = dr["flag"].ToString();
                            if (flag == "P")
                            {
                                count = 3;
                            }
                            if (flag == "L")
                            {
                                count = 3;
                            }
                            if (dr["admin_login"].ToString() == "1")
                            {
                                count = 4;
                            }
                            break;
                        }
                    }
                    if (count == 1 || count ==3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Username/Password...!!!');", true);

                        //MySqlCommand update = new MySqlCommand("UPDATE pay_user_master SET flag = 'I' WHERE LOGIN_ID='" + uname + "'", d1.con1);
                        //update.ExecuteNonQuery();

                        //MySqlCommand menu = new MySqlCommand("UPDATE pay_user_master SET COUNTER = COUNTER + 1 WHERE LOGIN_ID='" + uname + "'", d1.con1);
                        //menu.ExecuteNonQuery();
                        //MySqlCommand menu1 = new MySqlCommand("SELECT COUNTER FROM pay_user_master WHERE LOGIN_ID='" + uname + "'", d1.con1);
                        //MySqlDataReader dr1 = menu1.ExecuteReader();
                        //int cnt = 0;
                        //if (dr1.Read())
                        //{
                        //    cnt = int.Parse(dr1.GetValue(0).ToString());
                        //}
                        //dr1.Close();
                        //dr1.Dispose();

                        //if (cnt >= 3)
                        //{
                        //    MySqlCommand update1 = new MySqlCommand("UPDATE pay_user_master SET flag = 'P' WHERE LOGIN_ID='" + uname + "'", d1.con1);
                        //    update1.ExecuteNonQuery();
                        //    count = 3;
                        //}
                        txt_password.Text = "";
                        txt_login_id.Text = "";
                        txt_login_id.Focus();
                        return;
                    }
                    if (count == 2)
                    {
                        MySqlCommand cmd_unit = new MySqlCommand("SELECT UNIT_FLAG FROM pay_user_master WHERE LOGIN_ID='" + uname + "'", d_unit.con1);
                         d_unit.con1.Open();
                         MySqlDataReader dr_unit = cmd_unit.ExecuteReader();
                         if (dr_unit.Read())
                         {
                             if (dr_unit.GetValue(0).ToString() == "1")
                             {
                                 MySqlCommand cmd1 = new MySqlCommand("SELECT d.UNIT_NAME, 30 - (DATEDIFF(NOW(), password_changed_date)), a.first_login, d.comp_code, d.unit_code, (SELECT company_name FROM pay_company_master c WHERE c.comp_code = d.comp_code), a.UNIT_FLAG,a. client_zone ,a.client_code FROM pay_user_master a INNER JOIN pay_unit_master d  ON a.unit_code = d.unit_code and a.comp_code = d.comp_code WHERE a.LOGIN_ID = '" + uname + "' AND a.USER_PASSWORD='" + pswd + "'", d1.con1);
                                 MySqlDataReader dr1 = cmd1.ExecuteReader();
                                 if (dr1.Read())
                                 {
                                     Session["USERID"] = txt_login_id.Text;
                                     Session["LOGIN_ID"] = txt_login_id.Text;
                                     Session["USERNAME"] = dr1.GetValue(0).ToString();
                                     Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                                     //Session["ROLE"] = dr1.GetValue(3).ToString();
                                     Session["comp_code"] = dr1.GetValue(3).ToString();
                                     Session["UNIT_CODE"] = dr1.GetValue(4).ToString();
                                     Session["COMP_NAME"] = dr1.GetValue(5).ToString();
                                     Session["CLIENT_CODE"] = dr1.GetValue(8).ToString();

                                     if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(2).ToString().Equals("0"))
                                     {
                                         Session["CHANGE_PASS"] = "0";
                                         Response.Redirect("Change_password.aspx",false);
                                     }
                                     d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                                 }
                                 dr1.Close();
                                 d1.con1.Close();

                                 Response.Redirect("Unit_Login.aspx",false);


                             } else if (dr_unit.GetValue(0).ToString() == "2")
                             {
                                 MySqlCommand cmd1 = new MySqlCommand("SELECT d. client_name ,30 - (DATEDIFF(NOW(), password_changed_date)),a.first_login,d.comp_code,(SELECT company_name FROM pay_company_master c WHERE c.comp_code = d.comp_code),a.UNIT_FLAG,a.client_zone,d.client_code,a.zone_region FROM  pay_user_master  a INNER JOIN  pay_client_master  d ON  a . client_code  =  d . client_code  WHERE a.LOGIN_ID = '" + uname + "' AND a.USER_PASSWORD='" + pswd + "'", d1.con1);
                              //   d1.con1.Open();
                                 MySqlDataReader dr1 = cmd1.ExecuteReader();
                                 if (dr1.Read())
                                 {
                                     Session["USERID"] = txt_login_id.Text;
                                     Session["LOGIN_ID"] = txt_login_id.Text;
                                     Session["USERNAME"] = dr1.GetValue(0).ToString();
                                     Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                                     //Session["ROLE"] = dr1.GetValue(3).ToString();
                                     Session["comp_code"] = dr1.GetValue(3).ToString();
                                     Session["ZONE_NAME"] = dr1.GetValue(6).ToString();
                                     Session["CLIENT_CODE"] = dr1.GetValue(7).ToString();
                                     Session["REGION_NAME"] = dr1.GetValue(8).ToString();
                                     //chaitali
                                     Session["login_type"] = "Zone";
                                     if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(1).ToString().Equals("0"))
                                     {
                                         Session["CHANGE_PASS"] = "0";
                                         Response.Redirect("Change_password.aspx", false);
                                     }
                                     d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                                 }
                                 dr1.Close();
                                 d1.con1.Close();

                                 Response.Redirect("HO_Login.aspx", false);

                             }
                             else if (dr_unit.GetValue(0).ToString() == "3")
                             {
                                 MySqlCommand cmd1 = new MySqlCommand("SELECT d.client_name, 30 - (DATEDIFF(NOW(), password_changed_date)), a.first_login, d.comp_code, d.client_code, (SELECT company_name FROM pay_company_master c WHERE c.comp_code = d.comp_code), a.UNIT_FLAG,a.client_zone,a.zone_region FROM pay_user_master a INNER JOIN pay_client_master d  ON a.client_code = d.client_code and a.comp_code=d.comp_code WHERE a.LOGIN_ID = '" + uname + "' AND a.USER_PASSWORD='" + pswd + "'", d1.con1);
                                // d1.con1.Open();
                                 MySqlDataReader dr1 = cmd1.ExecuteReader();
                                 if (dr1.Read())
                                 {
                                     Session["USERID"] = txt_login_id.Text;
                                     Session["LOGIN_ID"] = txt_login_id.Text;
                                     Session["USERNAME"] = dr1.GetValue(0).ToString();
                                     Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                                     //Session["ROLE"] = dr1.GetValue(3).ToString();
                                     Session["comp_code"] = dr1.GetValue(3).ToString();
                                     Session["CLIENT_CODE"] = dr1.GetValue(4).ToString();
                                     Session["COMP_NAME"] = dr1.GetValue(5).ToString();
                                     //MD Change
                                     Session["ZONE_NAME"] = dr1.GetValue(7).ToString();
                                     Session["REGION_NAME"] = dr1.GetValue(8).ToString();
                                     //chaitali
                                     Session["login_type"] = "Region";
                                     if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(2).ToString().Equals("0"))
                                     {
                                         Session["CHANGE_PASS"] = "0";
                                         Response.Redirect("Change_password.aspx", false);
                                     }
                                     d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                                 }
                                 dr1.Close();
                                 d1.con1.Close();

                                 Response.Redirect("HO_Login.aspx", false);

                             }
                             else if (dr_unit.GetValue(0).ToString() == "4")
                             {
                                 MySqlCommand cmd1 = new MySqlCommand("SELECT d.client_name,30 - (DATEDIFF(NOW(), password_changed_date)),a.first_login,d.comp_code,(SELECT company_name FROM pay_company_master c WHERE c.comp_code = d.comp_code),a.UNIT_FLAG,a.client_zone,d.client_code,a.zone_region FROM  pay_user_master  a INNER JOIN  pay_client_master  d ON  a . client_code  =  d . client_code  WHERE a.LOGIN_ID = '" + uname + "' AND a.USER_PASSWORD='" + pswd + "'", d1.con1);
                                 //   d1.con1.Open();
                                 MySqlDataReader dr1 = cmd1.ExecuteReader();
                                 if (dr1.Read())
                                 {
                                     Session["USERID"] = txt_login_id.Text;
                                     Session["LOGIN_ID"] = txt_login_id.Text;
                                     Session["USERNAME"] = dr1.GetValue(0).ToString();
                                     Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                                     //Session["ROLE"] = dr1.GetValue(3).ToString();
                                     Session["comp_code"] = dr1.GetValue(3).ToString();
                                     Session["ZONE_NAME"] = dr1.GetValue(6).ToString();
                                     Session["CLIENT_CODE"] = dr1.GetValue(7).ToString();
                                     Session["REGION_NAME"] = dr1.GetValue(8).ToString();
                                     //chaitali
                                     Session["login_type"] = "HO";
                                     if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(1).ToString().Equals("0"))
                                     {
                                         Session["CHANGE_PASS"] = "0";
                                         Response.Redirect("Change_password.aspx", false);
                                     }
                                     d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                                 }
                                 dr1.Close();
                                 d1.con1.Close();

                                 Response.Redirect("HO_Login.aspx", false);

                             }

                             else
                             {

                                 MySqlCommand cmd1 = new MySqlCommand("SELECT b.EMP_NAME,90 -(datediff(now(), password_changed_date)),a.first_login, a.role,b.comp_code,b.unit_code,(select company_name from pay_company_master c where c.comp_code=b.comp_code limit 1),(SELECT Concat(UNIT_NAME,'-',UNIT_ADD1,'-',STATE_NAME) as UNIT_NAME FROM  pay_unit_master  c WHERE  c . unit_code  =  b . unit_code   and  c . comp_code  =  b . comp_code  LIMIT 1) as 'unit_name'  FROM pay_user_master a inner join pay_employee_master b on a.Login_id = b.emp_code WHERE a.LOGIN_ID = '" + uname + "' AND a.USER_PASSWORD='" + pswd + "'", d1.con1);
                                 MySqlDataReader dr1 = cmd1.ExecuteReader();
                                 if (dr1.Read())
                                 {
                                     Session["USERID"] = txt_login_id.Text;
                                     Session["LOGIN_ID"] = txt_login_id.Text;
                                     Session["USERNAME"] = dr1.GetValue(0).ToString();
                                     Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                                     Session["ROLE"] = dr1.GetValue(3).ToString();
                                     Session["comp_code"] = dr1.GetValue(4).ToString();
                                     Session["UNIT_CODE"] = dr1.GetValue(5).ToString();
                                     
                                     Session["COMP_NAME"] = dr1.GetValue(6).ToString();
                                     Session["UNIT_NAME"] = dr1.GetValue(7).ToString();
                                     Session["REPORTING_EMP_SERIES"] = d.reporting_emp_series(Session["COMP_CODE"].ToString(), Session["LOGIN_ID"].ToString());
                                     if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(2).ToString().Equals("0"))
                                     {
                                         Session["CHANGE_PASS"] = "0";
                                         Response.Redirect("Change_password.aspx");
                                     }
                                     d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                                     //if (dr1.GetValue(3).ToString() == "HR Department")
                                     //{
                                     //    Response.Redirect("CS.aspx");
                                     //}

                                 }
                                 dr1.Close();
                                 d1.con1.Close();
                                 //update_notification();
                                 Response.Redirect("Home.aspx");

                             }
                             dr_unit.Close();
                             d_unit.con1.Close();
                         }
                    }

                    //if (count == 3)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Account has been blocked !!!');", true);
                    //    txt_password.Text = "";
                    //    txt_login_id.Text = "";
                    //    txt_login_id.Focus();
                    //    return;
                    //}
                    if (count == 4)
                    {
                        MySqlCommand cmd1 = new MySqlCommand("SELECT b.USER_NAME,30 -(datediff(now(), password_changed_date)),b.first_login, b.role, b.comp_code, b.unit_code, (select company_name from pay_company_master c where c.comp_code=b.comp_code limit 1) FROM pay_user_master b WHERE b.LOGIN_ID = '" + uname + "' AND b.USER_PASSWORD='" + pswd + "'", d1.con1);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            Session["USERID"] = txt_login_id.Text;
                            Session["LOGIN_ID"] = txt_login_id.Text;
                            Session["USERNAME"] = dr1.GetValue(0).ToString();
                            Session["CHANGE_PASS"] = dr1.GetValue(1).ToString();
                            Session["ROLE"] = dr1.GetValue(3).ToString();
                            Session["comp_code"] = dr1.GetValue(4).ToString();
                            Session["UNIT_CODE"] = dr1.GetValue(5).ToString();
                            Session["COMP_NAME"] = dr1.GetValue(6).ToString();
                            if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0 || dr1.GetValue(2).ToString().Equals("0"))
                            {
                                Session["CHANGE_PASS"] = "1";
                                Response.Redirect("Change_password.aspx",false);
                            }
                            d1.operation("update pay_user_master set flag='A', counter=0 WHERE LOGIN_ID = '" + uname + "'");

                        }
                        dr1.Close();
                        d1.con1.Close();
                       // update_notification();
                       // d.logs("Login Successfully!!");
                        
                            Response.Redirect("Home.aspx", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
           // throw ex;
            //Log.Error("ERROR : " + ex.Message);
            //Log.Error("ERROR : " + ex.StackTrace);
        }
        finally
        {
            d1.con1.Close();
        }

    }

    private void update_notification()
    {
        d1.con1.Open();
        try
        {
            if (Application["BIRTHDAY"] == null || Application["BIRTHDAY"].ToString() == "")
            {
                Application["BIRTHDAY"] = Session["system_curr_date"].ToString();
                MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_NAME,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["BIRTHDAY"].ToString() + "','%d/%m'),'%d %b')) as noti from pay_employee_master WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["BIRTHDAY"].ToString() + "',1,5)", d1.con1);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    d1.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select '" + dr1.GetValue(0).ToString() + "','0',EMP_CODE,'birthday.aspx' from pay_employee_master");
                }
            }
            else
            {
                DateTime birthday = DateTime.ParseExact(Application["BIRTHDAY"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime systemdate = DateTime.ParseExact(Session["system_curr_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (birthday <= systemdate)
                {
                    for (int i = 1; birthday != systemdate; i++)
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_NAME,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["BIRTHDAY"].ToString() + "','%d/%m'),'%d %b')) as noti from pay_employee_master WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["BIRTHDAY"].ToString() + "',1,5)", d1.con1);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            d1.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select '" + dr1.GetValue(0).ToString() + "','0',EMP_CODE,'birthday.aspx' from pay_employee_master");
                        }
                        dr1.Close();
                        birthday = birthday.AddDays(1);
                        Application["BIRTHDAY"] = birthday.ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d1.con1.Close();
        }
        //insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select (select concat('Its ',EMP_NAME,' birthday today - ', DATE_FORMAT(NOW(),'%d %b %y')) from pay_employee_master WHERE str_to_date(BIRTH_DATE,'%d/%m/%Y') = date(now())),'0',EMP_CODE,'birthday.aspx' from pay_employee_master

    }

    protected void btn_email_Click(object sender, EventArgs e)
    {
      //  d.con.Open();
        try
        {
            MySqlCommand cmdlog = new MySqlCommand("select EMP_EMAIL_ID,DATE_FORMAT(BIRTH_DATE,'%d/%m/%Y'),EMP_MOBILE_NO from pay_employee_master where EMP_CODE ='" + txt_login.Text + "'", d.con);
            // string emailid = (string)cmdlog.ExecuteScalar();
            d.con.Open();
           // try
           // {
                MySqlDataReader dr = cmdlog.ExecuteReader();
                if (dr.Read())
                {
                    string emailid = dr[0].ToString();
                    string bitthday = dr[1].ToString();
                    string mobno = dr[2].ToString();


                    if (emailid == txt_emailid.Text && bitthday == txtbirthday.Text && mobno == txt_mobileno.Text)
                    {
                        d.operation("update pay_user_master set user_password = '" + d.Encrypt("Welcome@123") + "', first_login='0' where login_id = '" + txt_login.Text + "'");
                        SendEmail(emailid, "HRMS - Password Changed", "Your HRMS password have been updated to Welcome@123");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Password have been sent to " + emailid + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Provided Correct Write Information!!');", true);
                    }

                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }

    public void SendEmail(string address, string subject, string message)
    {
        string email = "celtsoft1919@gmail.com";
        string password = "celtsoft@123";

        var loginInfo = new NetworkCredential(email, password);
        var msg = new MailMessage();
        var smtpClient = new SmtpClient("smtp.gmail.com", 587);

        msg.From = new MailAddress(email);
        msg.To.Add(new MailAddress(address));
        msg.Subject = subject;
        msg.Body = message;
        msg.IsBodyHtml = true;

        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = loginInfo;
        smtpClient.Send(msg);
    }

    protected void document_details()
    {

        MySqlCommand cmd2 = new MySqlCommand("SELECT ID,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  ", d.con);
        
        d.con.Open();
        
        try
        {
            MySqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {

                string txt_Sr_No = dr.GetValue(0).ToString();
                string ddl_employeename = dr.GetValue(1).ToString();
                string txt_reporting_to = dr.GetValue(2).ToString();
                string ddl_document_list = dr.GetValue(3).ToString();
                string txt_start_date = dr.GetValue(4).ToString();
                string txt_end_date = dr.GetValue(5).ToString();
                string enter_message1 = "Uniform Is Exipre on Date ";
                string enter_message2 = "Shoes Is Exipre on Date ";
                string enter_message5 = "Swetor Is Exipre on Date ";
                string enter_message6 = "ID Card Is Exipre on Date ";
                string enter_message7 = "Raincoat Is Exipre on Date ";
                string enter_message8 = "Tourch Is Exipre on Date ";
                string enter_message9 = "Whistel Is Exipre on Date ";
                string enter_message10 = "Baton Is Exipre on Date ";
                string enter_message11 = "Belt Is Exipre on Date ";
                DateTime joinindgdate = Convert.ToDateTime(txt_start_date);
                DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
                TimeSpan count = newdate1 - joinindgdate;
                int Days = Convert.ToInt32(count.TotalDays);

                int differance = Convert.ToInt32(Days);

                if (ddl_document_list == "Uniform")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message1 + txt_end_date + "')");
                        

                    }
                }
                else if (ddl_document_list == "Shoes")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message2 + txt_end_date + "')");
                        //return false;

                    }
                }

                else if (ddl_document_list == "Swetor")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message5 + txt_end_date + "')");
                        //return false;

                    }
                }
                else if (ddl_document_list == "ID_Card")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message6 + txt_end_date + "')");

                    }
                }
                else if (ddl_document_list == "Raincoat")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message7 + txt_end_date + "')");

                    }
                }
                else if (ddl_document_list == "Tourch")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message8 + txt_end_date + "')");

                    }
                }
                else if (ddl_document_list == "Whistel")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message9 + txt_end_date + "')");

                    }
                }
                else if (ddl_document_list == "Baton")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message10 + txt_end_date + "')");

                    }
                }
                else if (ddl_document_list == "Belt")
                {
                    if (differance >= 172)
                    {
                        d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + txt_reporting_to + "','Message from " + ddl_employeename + " - " + enter_message11 + txt_end_date + "')");

                    }
                }
            }
        }
        catch(Exception ex) { throw ex; }
        
        finally
        {
            
            d.con.Close();
        }

    }
}
