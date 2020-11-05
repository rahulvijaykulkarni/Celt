using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Collections.Generic;


/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    static string conn = ConfigurationManager.ConnectionStrings["CELTPAYConnectionString"].ToString();

    public MySqlConnection con = new MySqlConnection(conn);
    public MySqlConnection con1 = new MySqlConnection(conn);
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //----------------connection open--------------------------------------------------
    public void conopen()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
            con.Dispose();
            con.ClearPoolAsync(con);
        }
        con.Open();
    }
    //------------------connection close--------------------------------------------------
    public void conclose()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();

        }
        con.Close();
        con.Dispose();
        con.ClearPoolAsync(con);
    }

    public void conclose(MySqlConnection connclose)
    {
        if (connclose.State == ConnectionState.Open)
        {
            connclose.Close();
            connclose.Dispose();
            connclose.ClearPoolAsync(con);
        }
    }


    //----------------connection1 open--------------------------------------------------
    public void conopen1()
    {
        if (con1.State == ConnectionState.Open)
        {
            con1.Close();
            con1.Dispose();
            con1.ClearPoolAsync(con1);
        }
        con1.Open();
    }
    //------------------connection1 close--------------------------------------------------
    public void conclose1()
    {
        if (con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        con1.Close();
        con1.Dispose();
        con1.ClearPoolAsync(con1);
    }
    //------------------getting query for adapter-----------------------------------------
    public DataSet getData(string query)
    {
        MySqlDataAdapter adp = new MySqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        try
        {
            adp.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            adp.Dispose();
            con.Dispose();
        }
    }
    //-------------------getting string for insert/update/delete which returns integer--------------------
    public int operation(string query)
    {
        conopen();
        MySqlCommand cmd = new MySqlCommand(query, con);
        try
        {
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conclose();
            cmd.Dispose();
        }
    }

    // Clear all controls
    public void reset(Control control)
    {
        foreach (Control x in control.Controls)
        {
            if (x is TextBox)
            {
                (x as TextBox).Text = String.Empty;
            }
            if (x is DropDownList)
            {
                if ((x as DropDownList).Items.Count > 0)
                {
                    (x as DropDownList).SelectedIndex = 0;
                }
            }
            reset(x);
        }
    }

    public MySqlDataAdapter select(string command)
    {
        con.Open();
        MySqlDataAdapter da = new MySqlDataAdapter(command, con);
        con.Close();
        con.Dispose();
        return da;
    }
    public MySqlDataReader selectReader(string command)
    {
        con.Open();
        MySqlCommand cmd = new MySqlCommand(command, con);
        MySqlDataReader dr = cmd.ExecuteReader();
        con.Close();
        con.Dispose();
        return dr;
    }
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "RANIENCRPT@#$%";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "RANIENCRPT@#$%";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public string getaccess(string rolename, string pagename, string comp_code)
    {
        conopen();
        MySqlCommand cmd = new MySqlCommand("select permissions from pay_role_master where  comp_code='" + comp_code + "' and role_name = '" + rolename + "' and menu_id = (select id from menus where menu_name= '" + pagename + "')", con);
        try
        {
            return (string)cmd.ExecuteScalar();
        }
        catch
        {
            throw;
        }
        finally
        {
            conclose();
            cmd.Dispose();
        }
        // return "";
    }

    public string getsinglestring(string query)
    {
        conopen();
        MySqlCommand cmd = new MySqlCommand(query, con);
        try
        {
            object gg = cmd.ExecuteScalar();
            return (gg == null) ? String.Empty : gg.ToString();
        }
        catch
        {
            throw;
        }
        finally
        {
            conclose();
            cmd.Dispose();
        }
        // return "";
    }

    //Sending email Vinod Pol
    private string PopulateBody(string userName, string employeename, string leavetype, string fromdate, string todate, string noofdays, string emailhtmlfile, string reason, string emp_code, int type, string color, string status)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{employeename}", employeename);
        body = body.Replace("{leavetype}", leavetype);
        body = body.Replace("{reason}", reason);
        body = body.Replace("{fromdate}", fromdate);
        body = body.Replace("{todate}", todate);
        body = body.Replace("{noofdays}", noofdays);
        if (type == 0)
        {
            //body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("accept") + "&B=" + Encrypt(getid(emp_code)) + "&C=" + Encrypt(employeename));
            //body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("reject") + "&B=" + Encrypt(getid(emp_code)) + "&C=" + Encrypt(employeename));
            body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=accept&B=" + getid(emp_code) + "&C=" + employeename);
            body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=reject&B=" + getid(emp_code) + "&C=" + employeename);

        }
        else if (type == 1)
        {
            body = body.Replace("{color}", color);
            body = body.Replace("{status}", status);
        }

        return body;
    }

    public void SendHtmlFormattedEmail(string emailhtmlfile, string boss_emp_code, string emp_code, string leavetype, string fromdate, string todate, string noofdays, string reason, int type, string color, string status)
    {
        string body = PopulateBody(getname(boss_emp_code), getname(emp_code), leavetype, fromdate, todate, noofdays, emailhtmlfile, reason, emp_code, type, color, status);
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = "Leave Approval";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(getemail(boss_emp_code, 0)));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }

    private string getemail(string emp_code, int type)
    {
        string emailid = "";
        string sql = "";
        if (type == 0)
        {
            sql = "select emp_email_id from pay_employee_master where emp_code = '" + emp_code + "'";
        }
        else if (type == 1)
        {
            sql = "select emp_email_id from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code in (select emp_code from pay_add_expenses where Expenses_id = '" + emp_code + "' )) ";
        }
        else if (type == 2)
        {
            sql = "select emp_email_id from pay_employee_master where emp_code in (select emp_code from pay_add_expenses where Expenses_id = '" + emp_code + "' ) ";
        }
        else if (type == 3)
        {
            sql = "select emp_email_id from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code in ( select emp_code from apply_travel_plan where Expenses_id = '" + emp_code + "'))";
        }
        else if (type == 4)
        {
            sql = "select emp_email_id from pay_employee_master where emp_code in ( select emp_code from apply_travel_plan where Expenses_id = '" + emp_code + "')";
        }

        MySqlCommand cmd = new MySqlCommand(sql, con);
        try
        {
            con.Open();
            emailid = (string)cmd.ExecuteScalar();
        }
        catch
        { }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        return emailid;
    }

    private string getname(string emp_code)
    {
        string emailid = "";
        string sql = "select emp_name from pay_employee_master where emp_code = '" + emp_code + "'";

        MySqlCommand cmd = new MySqlCommand(sql, con);
        try
        {
            con.Open();
            emailid = (string)cmd.ExecuteScalar();
        }
        catch
        { }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        return emailid;
    }

    private string getid(string emp_code)
    {
        int emailid = 0;
        string sql = "select max(leave_id) from pay_leave_transaction where emp_code = '" + emp_code + "'";

        MySqlCommand cmd = new MySqlCommand(sql, con);
        try
        {
            con.Open();
            emailid = (int)cmd.ExecuteScalar();
        }
        catch
        { }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        return emailid.ToString();
    }

    private string expenseBody(string emailhtmlfile, string expenseid, int type, string status, string username)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            con1.Open();
            MySqlCommand cmdmax1 = new MySqlCommand("SELECT emp_code,travel_mode,from_designation,to_designation,date_format(from_date,'%d/%m/%Y'),date_format(to_date,'%d/%m/%Y'),exception_case,adv_amount,expense_status,Comments FROM apply_travel_plan a where a.expenses_id = '" + expenseid + "'", con1);
            MySqlDataReader drmax = cmdmax1.ExecuteReader();
            if (drmax.HasRows)
            {
                body1 = "<table border=\"1\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" style = \"font-family:Arial;font-size:10pt;\"> 	<tr>     <th>Travel Mode</th>     <th>Exception</th>     <th>From Designation</th>     <th>To Designation</th>     <th>From Date</th>     <th>To Date</th>     <th>Advance Amount</th>   </tr>";
            }
            while (drmax.Read())
            {
                body1 = body1 + "<tr><td>" + drmax.GetValue(1).ToString() + "</td><td>" + drmax.GetValue(6).ToString() + "</td><td>" + drmax.GetValue(2).ToString() + "</td><td>" + drmax.GetValue(3).ToString() + "</td><td>" + drmax.GetValue(4).ToString() + "</td><td>" + drmax.GetValue(5).ToString() + "</td><td>" + drmax.GetValue(7).ToString() + "</td></tr>";
            }
            drmax.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con1.Close();
            con1.Dispose();
        }
        body1 = body1 + "</table><br />";
        body = body.Replace("{data}", body1);


        string tablebody = "";
        double total_amount = 0;
        con1.Open();
        MySqlCommand cmdmax = new MySqlCommand("SELECT Emp_code,date_format(Date,'%d/%m/%Y'), CASE WHEN `city_type` = 1 THEN 'Inside City' WHEN `city_type` = 2 THEN 'Outside City' END AS 'city_type',Merchant,description,particular, Amount, (select emp_name from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code = a.emp_code)) as reporting FROM pay_add_expenses a where a.Expenses_id = '" + expenseid + "'", con1);
        MySqlDataReader drmax1 = cmdmax.ExecuteReader();
        while (drmax1.Read())
        {
            tablebody = tablebody + "<tr><td>" + drmax1.GetValue(1).ToString() + "</td><td>" + drmax1.GetValue(2).ToString() + "</td><td>" + drmax1.GetValue(3).ToString() + "</td><td>" + drmax1.GetValue(4).ToString() + "</td><td>" + drmax1.GetValue(5).ToString() + "</td><td>" + drmax1.GetValue(6).ToString() + "</td><tr>";
            total_amount = total_amount + double.Parse(drmax1.GetValue(6).ToString());
            if (!emailhtmlfile.Contains("travelexpenseconfirm.htm"))
            {
                body = body.Replace("{UserName}", "Hello " + drmax1.GetValue(6).ToString() + ",<br /><br />");
                body = body.Replace("{empname}", "Expense claim of " + username + " for below Travel Plan");
            }
            else
            {
                body = body.Replace("{empname}", "Your Claim have been " + status);
                body = body.Replace("{UserName}", "");
            }
            if (type == 0)
            {
                //body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("acceptexp") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                //body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("rejectexp") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=acceptexp&B=" + expenseid + "&C=" + getname(drmax1.GetValue(0).ToString()));
                body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=rejectexp&B=" + expenseid + "&C=" + getname(drmax1.GetValue(0).ToString()));
            }
        }
        tablebody = tablebody + "<tr><td align=\"right\" colspan=\"5\"><b>Total : </b></td><td><b>" + total_amount + "</b></td></tr>";
        drmax1.Close();
        con1.Close();
        con1.Dispose();
        if (type == 1 || type == 2)
        {
            if (status == "Approved")
            {
                body = body.Replace("{color}", "090");
                body = body.Replace("{status}", status);
            }
            else if (status == "Rejected")
            {
                body = body.Replace("{color}", "f00");
                body = body.Replace("{status}", status);

            }
        }

        body = body.Replace("{tablebody}", tablebody);


        return body;
    }

    private string expenseBody1(string emailhtmlfile, string expenseid, int type, string status, string username)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        //try
        //{
        //    con1.Open();
        //    MySqlCommand cmdmax1 = new MySqlCommand("SELECT emp_code,travel_mode,from_designation,to_designation,date_format(from_date,'%d/%m/%Y'),date_format(to_date,'%d/%m/%Y'),exception_case,adv_amount,expense_status,Comments FROM apply_travel_plan a where a.expenses_id = '" + expenseid + "'", con1);
        //    MySqlDataReader drmax = cmdmax1.ExecuteReader();
        //    if (drmax.HasRows)
        //    {
        //        body1 = "<table border=\"1\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\" style = \"font-family:Arial;font-size:10pt;\"> 	<tr>     <th>Travel Mode</th>     <th>Exception</th>     <th>From Designation</th>     <th>To Designation</th>     <th>From Date</th>     <th>To Date</th>     <th>Advance Amount</th>   </tr>";
        //    }
        //    while (drmax.Read())
        //    {
        //        body1 = body1 + "<tr><td>" + drmax.GetValue(1).ToString() + "</td><td>" + drmax.GetValue(6).ToString() + "</td><td>" + drmax.GetValue(2).ToString() + "</td><td>" + drmax.GetValue(3).ToString() + "</td><td>" + drmax.GetValue(4).ToString() + "</td><td>" + drmax.GetValue(5).ToString() + "</td><td>" + drmax.GetValue(7).ToString() + "</td></tr>";
        //    }
        //    drmax.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    con1.Close();
        //    con1.Dispose();
        //}
        //body1 = body1 + "</table><br />";
        //body = body.Replace("{data}", body1);


        string tablebody = "";
        double total_amount = 0;
        con1.Open();
        //MySqlCommand cmdmax = new MySqlCommand("SELECT Emp_code,Date,CASE WHEN `city_type` = 1 THEN 'Inside City' WHEN `city_type` = 2 THEN 'Outside City' END AS 'city_type',Merchant, travel_type,type,description,Expness_Image,particular, Amount, (select emp_name from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code = a.emp_code)) as reporting FROM pay_add_expenses a where a.Expenses_id = '" + expenseid + "'", con1);
        MySqlCommand cmdmax = new MySqlCommand("SELECT Emp_code,date_format(Date,'%d/%m/%Y'), CASE WHEN `city_type` = 1 THEN 'Inside City' WHEN `city_type` = 2 THEN 'Outside City' END AS 'city_type',Merchant, travel_type,type,description,Expness_Image,particular, Amount, (select emp_name from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code = a.emp_code)) as reporting FROM pay_add_expenses a where a.Expenses_id = '" + expenseid + "'", con1);
        MySqlDataReader drmax1 = cmdmax.ExecuteReader();
        while (drmax1.Read())
        {
            tablebody = tablebody + "<tr><td>" + drmax1.GetValue(1).ToString() + "</td><td>" + drmax1.GetValue(2).ToString() + "</td><td>" + drmax1.GetValue(3).ToString() + "</td><td>" + drmax1.GetValue(4).ToString() + "</td><td>" + drmax1.GetValue(5).ToString() + "</td><td>" + drmax1.GetValue(6).ToString() + "</td><td>" + drmax1.GetValue(7).ToString() + "</td><td>" + drmax1.GetValue(8).ToString() + "</td><td>" + drmax1.GetValue(9).ToString() + "</td><tr>";
            total_amount = total_amount + double.Parse(drmax1.GetValue(9).ToString());
            if (!emailhtmlfile.Contains("travelexpenseconfirm.htm"))
            {
                body = body.Replace("{UserName}", "Hello " + drmax1.GetValue(10).ToString() + ",<br /><br />");
                body = body.Replace("{empname}", "Expense claim of " + username + " for below Travel Plan");
            }
            else
            {
                body = body.Replace("{empname}", "Your Claim have been " + status);
                body = body.Replace("{UserName}", "");
            }
            if (type == 0 || type == 3)
            {
                //body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("acceptexp") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                //body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("rejectexp") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=acceptexp&B=" + expenseid + "&C=" + getname(drmax1.GetValue(0).ToString()));
                body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=rejectexp&B=" + expenseid + "&C=" + getname(drmax1.GetValue(0).ToString()));
            }
        }
        tablebody = tablebody + "<tr><td align=\"right\" colspan=\"8\"><b>Total : </b></td><td><b>" + total_amount + "</b></td></tr>";
        drmax1.Close();
        con1.Close();
        con1.Dispose();
        if (type == 1 || type == 2)
        {
            if (status == "Approved")
            {
                body = body.Replace("{color}", "090");
                body = body.Replace("{status}", status);
            }
            else if (status == "Rejected")
            {
                body = body.Replace("{color}", "f00");
                body = body.Replace("{status}", status);

            }
        }

        body = body.Replace("{tablebody}", tablebody);


        return body;
    }

    public void SendHtmlexpenseEmail(string emailhtmlfile, string expense_id, int type, string status, string username)
    {
        string body = expenseBody(emailhtmlfile, expense_id, type, status, username);
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = "Expense Approval";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            if (type == 1 || type == 0)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 1)));
            }
            else if (type == 2)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 2)));
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }

    public void SendHtmlexpenseEmail1(string emailhtmlfile, string expense_id, int type, string status, string username)
    {

        string body = expenseBody1(emailhtmlfile, expense_id, type, status, username);
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = "Expense Approval";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            if (type == 1 || type == 0)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 1)));
                if (type == 0)
                {
                    MySqlCommand cmdmax = new MySqlCommand("SELECT Expness_Image FROM pay_add_expenses a where a.Expenses_id = '" + expense_id + "'", con1);
                    con1.Open();
                    MySqlDataReader drmax1 = cmdmax.ExecuteReader();
                    //if (drmax1.HasRows)
                    //{
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Load(drmax1);
                        foreach (DataRow row in dt.Rows)
                        {
                            //while (drmax1.Read())
                            //{
                            if (row["Expness_Image"].ToString() != "")
                            {
                            string path = emailhtmlfile.Replace("travelexpense.htm", "EMP_Images\\" + row["Expness_Image"].ToString());
                                mailMessage.Attachments.Add(new Attachment(path));
                            }
                            //}
                            //vinod  
                        }
                   // } con1.Close();
                }
            }
            else if (type == 2)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 2)));
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }
    public string check_url(string url)
    {
        if (!(url.EndsWith("/")))
        {
            url = url + "/";
        }
        return url;
    }

    private string travelBody(string emailhtmlfile, string expenseid, int type, string status, string Username, string to_email)
    {
        double amt = 0;
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            con1.Open();
            MySqlCommand cmdmax1 = new MySqlCommand("SELECT emp_code,case when city_type=1 then 'Inside City' when city_type=2 then 'Outside City' end as 'city_type',travel_mode,type,from_designation as 'From Destination',to_designation as 'To Destination',date_format(from_date,'%d/%m/%Y'),date_format(to_date,'%d/%m/%Y'),exception_case,adv_amount,expense_status,(select emp_name from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code = a.emp_code)) as reporting,Comments FROM apply_travel_plan a where a.expenses_id = '" + expenseid + "'", con1);
            MySqlDataReader drmax = cmdmax1.ExecuteReader();
            while (drmax.Read())
            {
                if (type == 1 || type == 0)
                {
                    body = body.Replace("{UserName}", drmax.GetValue(11).ToString());
                }
                else if (type == 3)
                {
                    body = body.Replace("{UserName}", getname(to_email));
                }
                else
                {
                    body = body.Replace("{UserName}", Username);
                }
                body = body.Replace("{employeename}", getname(drmax.GetValue(0).ToString()));
                body1 = body1 + "<tr><td>" + drmax.GetValue(1).ToString() + "</td><td>" + drmax.GetValue(2).ToString() + "</td><td>" + drmax.GetValue(3).ToString() + "</td><td>" + drmax.GetValue(8).ToString() + "</td><td>" + drmax.GetValue(4).ToString() + "</td><td>" + drmax.GetValue(5).ToString() + "</td><td>" + drmax.GetValue(6).ToString() + "</td><td>" + drmax.GetValue(7).ToString() + "</td><td>" + drmax.GetValue(9).ToString() + "</td></tr>";

                amt = amt + double.Parse(drmax.GetValue(9).ToString());


                if (type == 0 || type == 3)
                {
                    //body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("acceptme") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                    //body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=" + Encrypt("rejectme") + "&B=" + Encrypt(expenseid) + "&C=" + Encrypt(getname(drmax.GetValue(0).ToString())));
                    body = body.Replace("{accept_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=acceptme&B=" + expenseid + "&C=" + getname(drmax.GetValue(0).ToString()) + "&D=" + to_email);
                    body = body.Replace("{reject_url}", check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=rejectme&B=" + expenseid + "&C=" + getname(drmax.GetValue(0).ToString()) + "&D=" + to_email);

                }

                if (drmax.GetValue(6).ToString().Equals("Yes"))
                {
                    body = body.Replace("{exception}", "<h4 style=\"color:#f00;\"><b>This is Exception Case. He / She is not allowed in selected travel mode.</b></h4>");
                }
                else
                {
                    body = body.Replace("{exception}", "");
                }

                if (status == "Rejected")
                {
                    if (!drmax.GetValue(12).ToString().Equals(""))
                    {
                        body = body.Replace("{comments}", "<tr><td><b>Comments</b></td><td align=\"left\" colspan=\"7\">" + drmax.GetValue(12).ToString() + "</td></tr>");
                    }
                }
                body = body.Replace("{comments}", "");

            }
            drmax.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con1.Close();
            con1.Dispose();
        }
        body = body.Replace("{data}", body1);
        body = body.Replace("{amount}", amt.ToString());
        if (type == 1 || type == 2)
        {
            if (status == "Approved")
            {
                body = body.Replace("{color}", "090");
                body = body.Replace("{status}", status);
            }
            else if (status == "Rejected" || status == "Cancelled")
            {
                body = body.Replace("{color}", "f00");
                body = body.Replace("{status}", status);
            }
        }
        return body;
    }

    public void SendHtmltravelEmail(string emailhtmlfile, string expense_id, int type, string status, string Username, string to_email)
    {
        string body = travelBody(emailhtmlfile, expense_id, type, status, Username, to_email);
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = "Travel Approval";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            if (type == 1 || type == 0)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 3)));
            }
            else if (type == 2)
            {
                mailMessage.To.Add(new MailAddress(getemail(expense_id, 4)));
            }
            else if (type == 3)
            {
                mailMessage.To.Add(new MailAddress(getemail(to_email, 0)));
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }

    public void SendshiftEmail(string emailhtmlfile, string emp_code, int month, int year)
    {
        string body = shiftBody(emailhtmlfile, emp_code, month, year);
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = "Shift Schedule for month " + getmonth(month.ToString()) + " and Year " + year;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(getemail(emp_code, 0)));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }

    private string shiftBody(string emailhtmlfile, string emp_code, int month, int year)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            body = body.Replace("{empname}", "Shift Schedule for month " + getmonth(month.ToString()) + " and Year " + year);
            DateTime dt = new DateTime(year, month, 1);
            con1.Open();
            MySqlCommand cmdmax1 = new MySqlCommand("SELECT (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day01) AS 'day01', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day02) AS 'day02', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day03) AS 'day03', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day04) AS 'day04', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day05) AS 'day05', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day06) AS 'day06', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day07) AS 'day07', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day08) AS 'day08', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day09) AS 'day09', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day10) AS 'day10', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day11) AS 'day11', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day12) AS 'day12', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day13) AS 'day13', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day14) AS 'day14', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day15) AS 'day15', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day16) AS 'day16', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day17) AS 'day17', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day18) AS 'day18', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day19) AS 'day19', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day20) AS 'day20', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day21) AS 'day21', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day22) AS 'day22', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day23) AS 'day23', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day24) AS 'day24', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day25) AS 'day25', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day26) AS 'day26', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day27) AS 'day27', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day28) AS 'day28', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day29) AS 'day29', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day30) AS 'day30', (SELECT concat(shift_name,' ',DATE_FORMAT(shift_from,'%h:%i %p'),'-',DATE_FORMAT(shift_to,'%h:%i %p')) FROM pay_shift_master AS A WHERE A.id = B.day31) AS 'day31' FROM shift_calendar AS B WHERE   B.month = '" + month + "' AND B.year = '" + year + "' AND b.emp_code = '" + emp_code + "'", con1);
            MySqlDataReader drmax = cmdmax1.ExecuteReader();
            while (drmax.Read())
            {
                body1 = body1 + "<tr><td>" + dt.AddDays(0).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(0).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(0).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(1).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(1).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(1).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(2).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(2).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(2).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(3).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(3).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(3).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(4).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(4).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(4).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(5).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(5).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(5).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(6).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(6).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(6).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(7).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(7).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(7).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(8).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(8).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(8).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(9).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(9).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(9).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(10).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(10).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(10).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(11).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(11).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(11).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(12).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(12).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(12).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(13).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(13).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(13).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(14).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(14).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(14).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(15).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(15).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(15).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(16).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(16).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(16).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(17).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(17).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(17).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(18).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(18).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(18).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(19).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(19).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(19).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(20).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(20).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(20).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(21).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(21).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(21).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(22).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(22).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(22).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(23).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(23).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(23).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(24).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(24).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(24).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(25).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(25).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(25).ToString() + "</td></tr>";
                body1 = body1 + "<tr><td>" + dt.AddDays(26).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(26).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(26).ToString() + "</td></tr>";

                int days = DateTime.DaysInMonth(dt.Month, dt.Year);

                if (days >= 28)
                {
                    body1 = body1 + "<tr><td>" + dt.AddDays(27).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(27).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(27).ToString() + "</td></tr>";
                }
                if (days >= 29)
                {
                    body1 = body1 + "<tr><td>" + dt.AddDays(28).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(28).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(28).ToString() + "</td></tr>";
                }
                if (days >= 30)
                {
                    body1 = body1 + "<tr><td>" + dt.AddDays(29).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(29).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(29).ToString() + "</td></tr>";
                }
                if (days >= 31)
                {
                    body1 = body1 + "<tr><td>" + dt.AddDays(30).ToString("dd/MM/yyyy") + "</td><td>" + dt.AddDays(30).DayOfWeek.ToString().Substring(0, 3).ToUpper() + "</td><td>" + drmax.GetValue(30).ToString() + "</td></tr>";
                }
            }
            drmax.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con1.Close();
            con1.Dispose();
        }
        body = body.Replace("{tablebody}", body1);

        return body;
    }

    private string getmonth(string month)
    {
        if (month == "1")
        {
            return "JAN";
        }
        else if (month == "2")
        { return "FEB"; }
        else if (month == "3")
        { return "MAR"; }
        else if (month == "4")
        { return "APR"; }
        else if (month == "5")
        { return "MAY"; }
        else if (month == "6")
        { return "JUN"; }
        else if (month == "7")
        { return "JUL"; }
        else if (month == "8")
        { return "AUG"; }
        else if (month == "9")
        { return "SEP"; }
        else if (month == "10")
        { return "OCT"; }
        else if (month == "11")
        { return "NOV"; }
        else if (month == "12")
        { return "DEC"; }
        return "";

    }
    public void approve(string expenseid, string loginid, string username, string emailfile, string emailfile1)
    {
        int appcount = 0, count = 0;
        try
        {
            string querynew = "Select txt_approval_level from pay_travel_policy_master Where ID in (Select policy_id from pay_travel_emp_policy Where emp_code in (select emp_code from apply_travel_plan where expenses_id='" + expenseid + "')) ";
            MySqlCommand cmd_level = new MySqlCommand(querynew, con1);
            con1.Open();
            MySqlDataReader dr_level = cmd_level.ExecuteReader();
            if (dr_level.Read())
            {
                string approval_level1 = dr_level[0].ToString();
                if (approval_level1.Equals(null) || approval_level1.Trim().Equals("")) { approval_level1 = "1"; }
                appcount = Int32.Parse(approval_level1);
            }
            con1.Close();
            string app_level = "select Approved_level from apply_travel_plan where expenses_id='" + expenseid + "'";
            con1.Open();
            MySqlCommand app_cmd = new MySqlCommand(app_level, con1);

            MySqlDataReader app_dr = app_cmd.ExecuteReader();
            if (app_dr.Read())
            {
                string app_level1 = app_dr[0].ToString();
                if (app_level1.Equals(null) || app_level1.Trim().Equals("")) { app_level1 = "1"; }
                count = Int32.Parse(app_level1);
            }
            con1.Close();
            string query = "";
            if (loginid == null || loginid == "")
            {
                query = "select REPORTING_TO from pay_employee_master where EMP_code in (select substring(approval_emp,1,6) from apply_travel_plan where expenses_id='" + expenseid + "' )";
            }
            else
            {
                query = "select REPORTING_TO from pay_employee_master where EMP_code='" + loginid + "'";
            }
            string first_reporting_to = "";
            con1.Open();
            MySqlCommand cmd = new MySqlCommand(query, con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                first_reporting_to = dr[0].ToString();
            }
            con1.Close();

            count++;

            if (count == appcount)
            {
                operation("Update apply_travel_plan SET expense_status = 'Approved'  Where expenses_id = '" + expenseid + "'");
                update_notification1("Approved", first_reporting_to, expenseid, username, emailfile, emailfile1);
            }
            else
            {
                operation("Update apply_travel_plan SET expense_status = 'Pending', Approved_level='" + count + "', approval_emp = replace(approval_emp,'" + loginid + "',''), approved_emp=concat(ifnull(approved_emp,''),'" + loginid + "'),modified_by=(select emp_name from pay_employee_master where emp_code ='" + loginid + "') Where expenses_id = '" + expenseid + "'");
                //  operation("Update apply_travel_plan SET expense_status = 'Approved'  Where expenses_id = '" + expenseid + "' and approval_emp = ''");
                update_notification("Approved", first_reporting_to, expenseid, username, emailfile, emailfile1);
            }

        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            con1.Close();
        }
    }

    public void update_notification(string status, string to_email, string expenseid, string username, string emailfile, string emailfile1)
    {
        try
        {
            operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + username + "','0',EMP_CODE,'apply_expencess.aspx' from apply_travel_plan a where expenses_id = '" + expenseid + "' limit 1");

            if (!to_email.Equals(""))
            {
                operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + username + "','0','" + to_email + "','app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + expenseid + "' limit 1");
                SendHtmltravelEmail(emailfile, expenseid, 3, "", username, to_email);
            }
            SendHtmltravelEmail(emailfile1, expenseid, 2, status, "", "");

        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    public void update_notification1(string status, string to_email, string expenseid, string username, string emailfile, string emailfile1)
    {
        try
        {
            operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + username + "','0',EMP_CODE,'apply_expencess.aspx' from apply_travel_plan a where expenses_id = '" + expenseid + "' limit 1");

            if (!to_email.Equals(""))
            {
                operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + username + "','0','" + to_email + "','app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + expenseid + "' limit 1");
                //   SendHtmltravelEmail(emailfile, expenseid, 3, "", username, to_email);
            }
            SendHtmltravelEmail(emailfile1, expenseid, 2, status, "", "");

        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    public void logs(string error_message)
    {
        try
        {
            log.Error(error_message);
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    public string update_reporting_emp(string emp_code)
    {
        int app_level = 0;
        string r_emp_code = "";
        string level = getsinglestring("select distinct(approval_level) from pay_role_master where role_name in (select role from pay_user_master where login_id = '" + emp_code + "') limit 1");
        if (level != "")
        {
            app_level = int.Parse(level);
        }
        while (app_level > 0)
        {
            string temp = getsinglestring("select reporting_to from pay_employee_master where emp_code = '" + emp_code + "' limit 1");
            if (temp != "")
            {
                r_emp_code = r_emp_code + temp;
                emp_code = temp;
            }

            app_level = --app_level;
        }
        return r_emp_code;
    }

    public string get_month_year(string comp_code, string month_year, string ddl_unit, string ddl_client, int counter)
    {
        string temp_months = "";
        string months = "";
        string years = "";
        if (ddl_unit == "ALL")
        {
            temp_months = getsinglestring("SELECT * from pay_billing_master where billing_client_code = '" + ddl_client + "' and start_date_common != 1");
        }
        else
        {
            temp_months = getsinglestring("SELECT * from pay_billing_master where billing_unit_code ='" + ddl_unit + "' and comp_code='" + comp_code + "' and start_date_common != 1");
        }

        if (temp_months != "")
        {
            months = month_year.Substring(0, 2) + "," + (int.Parse(month_year.Substring(0, 2)) == 1 ? 12 : int.Parse(month_year.Substring(0, 2)) - 1);
            if (int.Parse(month_year.Substring(0, 2)) == 1)
            {
                years = month_year.Substring(3) + "," + (int.Parse(month_year.Substring(0, 2)) == 1 ? int.Parse(month_year.Substring(3)) - 1 : int.Parse(month_year.Substring(3)));
            }
            else
            { years = month_year.Substring(3); }
        }
        else
        {
            months = month_year.Substring(0, 2);
            years = month_year.Substring(3);
        }
        if (counter == 1)
        {
            return months;
        }
        else if (counter == 2)
        {
            return years;
        }
        return "";

    }

    public void update_attendance(string comp_code, string client_code, string unit_code, string month_year, int starting_day, string state)
    {
        con1.Open();
        try
        {
            ///update counts
            MySqlCommand cmd;
            if (unit_code == "ALL")
            {
                cmd = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 1, 1) + " pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE FROM pay_employee_master inner join pay_unit_master on pay_employee_master.UNIT_CODE = pay_unit_master.unit_code and pay_unit_master.comp_code = pay_employee_master.comp_code LEFT JOIN pay_attendance_muster ON " + month_year.Substring(3) + " = pay_attendance_muster.year AND pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE AND pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance_muster.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance_muster.month = " + month_year.Substring(0, 2) + " LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + ((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? int.Parse(month_year.Substring(3)) - 1 : int.Parse(month_year.Substring(3))) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + ((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? 12 : (int.Parse(month_year.Substring(0, 2)) - 1)) + " WHERE pay_unit_master.comp_code = '" + comp_code + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state + "'", con1);
                //cmd = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 1) + " pay_attendance_muster.emp_CODE, pay_attendance_muster.UNIT_CODE FROM pay_attendance_muster left join pay_attendance_muster t2 on pay_attendance_muster.Year = t2.year and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(month_year.Substring(0, 2)) - 1)+" inner join pay_unit_master on pay_attendance_muster.UNIT_CODE = pay_unit_master.unit_code and pay_unit_master.comp_code = pay_attendance_muster.comp_code WHERE pay_attendance_muster.month = " + month_year.Substring(0, 2) + " AND pay_attendance_muster.Year = " + month_year.Substring(3) + " and pay_attendance_muster.comp_code = '" + comp_code + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state + "' ", con1);
                //cmd = new MySqlCommand("select DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31,EMP_CODE,unit_code from pay_attendance_muster_diff where COMP_CODE = '" + comp_code + "' AND unit_code in (select unit_code from pay_client_master where client_code = '" + client_code + "') AND MONTH = '" + month_year.Substring(0, 2) + "' AND YEAR='" + month_year.Substring(3) + "' ", con1);
            }
            else
            {
                cmd = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 1, 1) + " pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE FROM pay_employee_master LEFT JOIN pay_attendance_muster ON " + month_year.Substring(3) + " = pay_attendance_muster.year AND pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE AND pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance_muster.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance_muster.month = " + month_year.Substring(0, 2) + " LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + ((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? int.Parse(month_year.Substring(3)) - 1 : int.Parse(month_year.Substring(3))) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + ((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? 12 : (int.Parse(month_year.Substring(0, 2)) - 1)) + " WHERE pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.unit_code = '" + unit_code + "'", con1);
                //cmd = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 1) + " pay_attendance_muster.emp_CODE, pay_attendance_muster.UNIT_CODE FROM pay_attendance_muster left join pay_attendance_muster t2 on pay_attendance_muster.Year = t2.year and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(month_year.Substring(0, 2)) - 1) + " WHERE pay_attendance_muster.month = '" + month_year.Substring(0, 2) + "' AND pay_attendance_muster.Year = '" + month_year.Substring(3) + "' and pay_attendance_muster.comp_code = '" + comp_code + "' and pay_attendance_muster.unit_code = '" + unit_code + "' ", con1);

                // cmd = new MySqlCommand("select DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31,EMP_CODE, unit_code from pay_attendance_muster_diff where COMP_CODE = '" + comp_code + "' AND UNIT_CODE='" + unit_code + "' AND MONTH = '" + month_year.Substring(0, 2) + "' AND YEAR='" + month_year.Substring(3) + "' ", con1);
            }
            MySqlDataReader drcount1 = cmd.ExecuteReader();

            while (drcount1.Read())
            {
                double pcount = 0;
                double acount = 0;
                double halfdaycount = 0;
                double leavescount = 0;
                double holidaycount = 0;
                double weeklyoffcount = 0;
                int days = 0;
                if (starting_day != 1)
                {
                    days = CountDay(((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? 12 : (int.Parse(month_year.Substring(0, 2)) - 1)), ((int.Parse(month_year.Substring(0, 2)) - 1) == 0 ? int.Parse(month_year.Substring(3)) - 1 : int.Parse(month_year.Substring(3))), 1);
                }
                else
                {
                    days = CountDay(int.Parse(month_year.Substring(0, 2)), int.Parse(month_year.Substring(3)), 1);
                }
                string emp_code = drcount1.GetValue(days).ToString(); string unit_code_org = drcount1.GetValue(days + 1).ToString();

                for (int j = 0; j <= (days - 1); j++)
                {
                    if (drcount1.GetValue(j).ToString() == "P" || drcount1.GetValue(j).ToString() == "PH")
                    {
                        pcount++;
                    }

                    else if (drcount1.GetValue(j).ToString() == "A" || drcount1.GetValue(j).ToString() == "0" || drcount1.GetValue(j).ToString() == "")
                    {
                        acount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "HD")
                    {
                        halfdaycount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "L")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "W")
                    {
                        weeklyoffcount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "H")
                    {
                        holidaycount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "CL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "PL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "EL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "SL")
                    {
                        leavescount++;
                    }

                }
                if (halfdaycount != 0)
                {
                    halfdaycount = halfdaycount / 2;
                }
                pcount = halfdaycount + pcount;

                //string employee = getsinglestring("select count(1) from pay_attendance_muster where comp_code = '" + comp_code + "' and unit_code = '" + unit_code_org + "' and month = '" + month_year.Substring(0, 2) + "' and year = '" + month_year.Substring(3) + "' and emp_code = '" + emp_code + "'");

                //if(employee=="0" || employee == "")
                //{
                //operation("insert into pay_attendance_muster (COMP_CODE, UNIT_CODE, EMP_CODE, DAY01, DAY02, DAY03, DAY04, DAY05, DAY06, DAY07, DAY08, DAY09, DAY10, DAY11, DAY12, DAY13, DAY14, DAY15, DAY16, DAY17, DAY18, DAY19, DAY20, DAY21, DAY22, DAY23, DAY24, DAY25, DAY26, DAY27, DAY28, DAY29, DAY30, DAY31, month, year) values ('" + comp_code + "','" + unit_code_org + "','" + emp_code + "','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A','A',"+month_year.Substring(0,2)+","+month_year.Substring(3)+")");
                //}
                int working_to_days = 0;
                if (starting_day != 1)
                {
                    working_to_days = CountDay(int.Parse(month_year.Substring(0, 2)) - 1, int.Parse(month_year.Substring(3)), 1);
                }
                else
                {
                    working_to_days = CountDay(int.Parse(month_year.Substring(0, 2)), int.Parse(month_year.Substring(3)), 1);
                }

                operation("Update pay_attendance_muster set TOT_DAYS_PRESENT =" + pcount + ", TOT_DAYS_ABSENT =" + acount + ", TOT_HALF_DAYS =" + halfdaycount + ",TOT_LEAVES =" + leavescount + ", HOLIDAYS =" + holidaycount + " ,WEEKLY_OFF='" + weeklyoffcount + "',TOT_WORKING_DAYS='" + working_to_days + "'  where comp_code = '" + comp_code + "' and unit_code = '" + unit_code_org + "' and month = '" + month_year.Substring(0, 2) + "' and year = '" + month_year.Substring(3) + "' and emp_code = '" + emp_code + "' and flag = 0 ");
                //operation("Update pay_billing_unit_rate set month_days =" + (pcount + acount - weeklyoffcount) + " where comp_code = '" + comp_code + "' and unit_code = '" + unit_code_org + "' and month = '" + month_year.Substring(0, 2) + "' and year = '" + month_year.Substring(3) + "'");
            }
            drcount1.Close();
            con1.Close();

            con1.Open();
            double zerocount = 0;
            double twocount = 0;
            double eightcount = 0;
            MySqlCommand cmd_ot;
            if (unit_code == "ALL")
            {
                cmd_ot = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 3, 1) + " pay_ot_muster.emp_CODE, pay_ot_muster.UNIT_CODE FROM pay_ot_muster left outer join pay_ot_muster t3 on pay_ot_muster.Year = t3.year and pay_ot_muster.COMP_CODE = t3.COMP_CODE and pay_ot_muster.UNIT_CODE = t3.UNIT_CODE and pay_ot_muster.EMP_CODE = t3.EMP_CODE and t3.month = " + (int.Parse(month_year.Substring(0, 2)) - 1) + " inner join pay_unit_master on pay_ot_muster.UNIT_CODE = pay_unit_master.unit_code and pay_unit_master.comp_code = pay_ot_muster.comp_code WHERE pay_ot_muster.month = " + month_year.Substring(0, 2) + " AND pay_ot_muster.Year = " + month_year.Substring(3) + " and pay_ot_muster.comp_code = '" + comp_code + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state + "'", con1);
                //cmd = new MySqlCommand("select DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31,EMP_CODE,unit_code from pay_attendance_muster_diff where COMP_CODE = '" + comp_code + "' AND unit_code in (select unit_code from pay_client_master where client_code = '" + client_code + "') AND MONTH = '" + month_year.Substring(0, 2) + "' AND YEAR='" + month_year.Substring(3) + "' ", con1);
            }
            else
            {
                cmd_ot = new MySqlCommand("SELECT " + get_calendar_days(starting_day, month_year, 3, 1) + " pay_ot_muster.emp_CODE, pay_ot_muster.UNIT_CODE FROM pay_ot_muster left outer join pay_ot_muster t3 on pay_ot_muster.Year = t3.year and pay_ot_muster.COMP_CODE = t3.COMP_CODE and pay_ot_muster.UNIT_CODE = t3.UNIT_CODE and pay_ot_muster.EMP_CODE = t3.EMP_CODE and t3.month = " + (int.Parse(month_year.Substring(0, 2)) - 1) + " WHERE pay_ot_muster.month = '" + month_year.Substring(0, 2) + "' AND pay_ot_muster.Year = '" + month_year.Substring(3) + "' and pay_ot_muster.comp_code = '" + comp_code + "' and pay_ot_muster.unit_code = '" + unit_code + "'  ", con1);
                // cmd = new MySqlCommand("select DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31,EMP_CODE, unit_code from pay_attendance_muster_diff where COMP_CODE = '" + comp_code + "' AND UNIT_CODE='" + unit_code + "' AND MONTH = '" + month_year.Substring(0, 2) + "' AND YEAR='" + month_year.Substring(3) + "' ", con1);
            }
            MySqlDataReader dr_ot = cmd_ot.ExecuteReader();
            while (dr_ot.Read())
            {
                int days = 0;
                if (starting_day != 1)
                {
                    days = CountDay(int.Parse(month_year.Substring(0, 2)) - 1, int.Parse(month_year.Substring(3)), 1);
                }
                else
                {
                    days = CountDay(int.Parse(month_year.Substring(0, 2)), int.Parse(month_year.Substring(3)), 1);
                }
                string emp_code = dr_ot.GetValue(days).ToString();
                string unit_code_org = dr_ot.GetValue(days + 1).ToString();

                for (int j = 0; j <= (days - 1); j++)
                {
                    if (dr_ot.GetValue(j).ToString() == "0")
                    {
                        zerocount = zerocount + 0;
                    }
                    else if (dr_ot.GetValue(j).ToString() == "2")
                    {
                        twocount = twocount + 2;
                    }
                    else if (dr_ot.GetValue(j).ToString() == "8")
                    {
                        eightcount = eightcount + 8;
                    }
                }
                operation("Update pay_ot_muster set TOT_OT = '" + (twocount + eightcount) + "' where comp_code = '" + comp_code + "' and unit_code = '" + unit_code_org + "' and month = '" + month_year.Substring(0, 2) + "' and year = '" + month_year.Substring(3) + "' and emp_code = '" + emp_code + "'");
                twocount = 0;
                eightcount = 0;
            }
            dr_ot.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con1.Close();
        }

    }
    //vikas add for arrears bill attendance update date 19/08/2019
    public void arrears_attendance_update(string comp_code1, string client_code1, string state, string unit_code1, string start_date,  string end_date, string unit)
    {
        string days = "";

        int start_date1 = int.Parse(start_date.Substring(0,2));
        int end_date1 = int.Parse(end_date.Substring(0, 2));
        int actual_date_diff = (end_date1 - start_date1) + 1;
       
        for (int i = start_date1; i <= end_date1; i++)
        {
            if (i < 10)
            {
                days = days + "DAY" + "0" + i + ",";
            }
            else
            {
                days = days + "DAY" + i + ",";
            }

        }

       string sql = "select COMP_CODE , UNIT_CODE , EMP_CODE , "+days+" TOT_DAYS_PRESENT , TOT_DAYS_ABSENT , TOT_HALF_DAYS , TOT_LEAVES , WEEKLY_OFF , HOLIDAYS , TOT_WORKING_DAYS , MONTH , YEAR , TOT_CO , ot_hours , month_end , salary , flag , invoice_flag , arrears_flag , ac_check_flag FROM  pay_attendance_muster WHERE  comp_code = '" + comp_code1 + "'   AND MONTH = '" + start_date.Substring(3, 2) + "'  AND YEAR = '" + start_date.Substring(6) + "' and flag !='0' and  invoice_flag !='0'  " + unit + "";
       operation("delete from pay_attendance_muster_arrears WHERE  comp_code = '" + comp_code1 + "'   AND MONTH = '" + start_date.Substring(3, 2) + "'  AND YEAR = '" + start_date.Substring(6) + "' " + unit + "");
        operation("insert into pay_attendance_muster_arrears(COMP_CODE , UNIT_CODE , EMP_CODE , "+days+" TOT_DAYS_PRESENT , TOT_DAYS_ABSENT , TOT_HALF_DAYS , TOT_LEAVES , WEEKLY_OFF , HOLIDAYS , TOT_WORKING_DAYS , MONTH , YEAR , TOT_CO , ot_hours , month_end , salary , flag , invoice_flag , arrears_flag , ac_check_flag) " + sql);


         con1.Open();
      //   string a = month_year.Substring(0, 2).ToString();
      //   MySqlCommand cmd;
      //int starting_day= start_date;
      //int montha = 0;
      //int year = int.Parse(month_year.Substring(3).ToString());
      
      //if (start_date != 1)
      //{
      //    montha = int.Parse(month_year.Substring(0, 2)) - 1;
      //    if (montha == 0) { montha = 12; --year; }
          
      //    if (unit_code1 == "ALL")
      //    {
      //        cmd = new MySqlCommand("select t1.EMP_CODE," + get_calendar_days1(starting_day, month_year, 5, s_date, last_day, last_month, end_date) + " t1.EMP_CODE  FROM  pay_attendance_muster_arrears AS t1  left JOIN pay_attendance_muster_arrears AS t2   ON t1.emp_code = t2.emp_code  AND t1.unit_code = t2.unit_code AND t1.comp_code = t2.comp_code  AND t2.year = '" + year + "' AND t2.month = '" + montha + "' WHERE  t1.comp_code = '" + comp_code1 + "'  " + unit_code1 + " AND t1.month = '" + month_year.Substring(0, 2).ToString() + "'  AND t1.year = '" + month_year.Substring(3).ToString() + "'", con1);
      //    }
      //    else
      //    {
      //        cmd = new MySqlCommand("select t1.EMP_CODE," + get_calendar_days1(starting_day, month_year, 5, s_date, last_day, last_month, end_date) + " t1.EMP_CODE FROM  pay_attendance_muster_arrears AS t1  left JOIN pay_attendance_muster_arrears AS t2   ON t1.emp_code = t2.emp_code  AND t1.unit_code = t2.unit_code AND t1.comp_code = t2.comp_code  AND t2.year = '" + year + "' AND t2.month = '" + montha + "' WHERE  t1.comp_code = '" + comp_code1 + "'  " + unit_code1 + " AND t1.month = '" + month_year.Substring(0, 2).ToString() + "'  AND t1.year = '" + month_year.Substring(3).ToString() + "'", con1);
      //    }
      //}
      //else {
      //    if (unit_code1 == "ALL")
      //    {
      //        cmd = new MySqlCommand("select t1.EMP_CODE," + get_calendar_days1(starting_day, month_year, 5, s_date, last_day, last_month, end_date) + " t1.EMP_CODE  FROM  pay_attendance_muster_arrears as t1  WHERE  comp_code = '" + comp_code1 + "'  " + unit_code1 + " AND month = '" + month_year.Substring(0, 2).ToString() + "'  AND year = '" + month_year.Substring(3).ToString() + "'", con1);
      //    }
      //    else
      //    {
      //        cmd = new MySqlCommand("select t1.EMP_CODE," + get_calendar_days1(starting_day, month_year, 5, s_date, last_day, last_month, end_date) + " t1.EMP_CODE FROM  pay_attendance_muster_arrears as t1  WHERE  comp_code = '" + comp_code1 + "' " + unit_code1 + " AND month = '" + month_year.Substring(0, 2).ToString() + "'  AND year = '" + month_year.Substring(3).ToString() + "'", con1);
      //    }
      //}


         MySqlCommand cmd = new MySqlCommand("select EMP_CODE, DAY01 , DAY02 , DAY03 , DAY04 , DAY05 , DAY06 , DAY07 , DAY08 , DAY09 , DAY10 , DAY11 , DAY12 , DAY13 , DAY14 , DAY15 , DAY16 , DAY17 , DAY18 , DAY19 , DAY20 , DAY21 , DAY22 , DAY23 , DAY24 , DAY25 , DAY26 , DAY27 , DAY28 , DAY29 , DAY30 , DAY31 FROM  pay_attendance_muster_arrears WHERE  comp_code = '" + comp_code1 + "'   AND MONTH = '" + start_date.Substring(3, 2) + "'  AND YEAR = '" + start_date.Substring(6) + "' " + unit, con1);

        MySqlDataReader drcount1 = cmd.ExecuteReader();
        //int count = (int)cmd.ExecuteScalar();
        while (drcount1.Read())
        {

            double pcount = 0;
            double acount = 0;
            double halfdaycount = 0;
            double leavescount = 0;
            double holidaycount = 0;
            double weeklyoffcount = 0;
            int dys = 31;
            //int days = 0;
           // int month_days = DateTime.DaysInMonth(int.Parse(start_date.Substring(6)), int.Parse(start_date.Substring(3, 2)));
            //string emp_code = drcount1.GetValue(0).ToString(); //string unit_code_org = drcount1.GetValue(days + 1).ToString();


            for (int j = 1; j <= (dys); j++)
            {

                if (drcount1.GetValue(j).ToString() == "P" || drcount1.GetValue(j).ToString() == "PH")
                {
                    pcount++;
                }

                else if (drcount1.GetValue(j).ToString() == "A" || drcount1.GetValue(j).ToString() == "0" || drcount1.GetValue(j).ToString() == "")
                {
                    acount++;
                }
                else if (drcount1.GetValue(j).ToString() == "HD")
                {
                    halfdaycount++;
                }
                else if (drcount1.GetValue(j).ToString() == "L")
                {
                    leavescount++;
                }
                else if (drcount1.GetValue(j).ToString() == "W")
                {
                    weeklyoffcount++;
                }
                else if (drcount1.GetValue(j).ToString() == "H")
                {
                    holidaycount++;
                }
                else if (drcount1.GetValue(j).ToString() == "CL")
                {
                    leavescount++;
                }
                else if (drcount1.GetValue(j).ToString() == "PL")
                {
                    leavescount++;
                }
                else if (drcount1.GetValue(j).ToString() == "EL")
                {
                    leavescount++;
                }
                else if (drcount1.GetValue(j).ToString() == "SL")
                {
                    leavescount++;
                }

            }
            if (halfdaycount != 0)
            {
                halfdaycount = halfdaycount / 2;
            }
            pcount = halfdaycount + pcount;

            int working_to_days = 0;

            //if (start_date != 1)
            //{
            //    operation("Update pay_attendance_muster_arrears set TOT_DAYS_PRESENT =" + pcount + ", TOT_DAYS_ABSENT =" + acount + ", TOT_HALF_DAYS =" + halfdaycount + ",TOT_LEAVES =" + leavescount + ", HOLIDAYS =" + holidaycount + " ,WEEKLY_OFF='" + weeklyoffcount + "',TOT_WORKING_DAYS='" + working_to_days + "'  where comp_code = '" + comp_code1 + "'  and month = '" + month_year.Substring(0, 2).ToString() + "' and year = '" + month_year.Substring(3) + "'  and EMP_CODE='" + drcount1.GetValue(0).ToString() + "'");
            //}
            //else
            //{
            operation("Update pay_attendance_muster_arrears set TOT_DAYS_PRESENT =" + pcount + ", TOT_DAYS_ABSENT =" + acount + ", TOT_HALF_DAYS =" + halfdaycount + ",TOT_LEAVES =" + leavescount + ", HOLIDAYS =" + holidaycount + " ,WEEKLY_OFF='" + weeklyoffcount + "',TOT_WORKING_DAYS='" + working_to_days + "'  where comp_code = '" + comp_code1 + "'  and month = '" + start_date.Substring(3, 2).ToString() + "' and year = '" + start_date.Substring(6) + "'  and EMP_CODE='" + drcount1.GetValue(0).ToString() + "'");
           // }
        }
        drcount1.Close();
        con1.Close();
 }
    int CountDay(int month, int year, int counter)
    {
        if (month == 0) { month = 12; year = --year; }
        int NoOfSunday = 0;
        var firstDay = new DateTime(year, month, 1);

        var day29 = firstDay.AddDays(28);
        var day30 = firstDay.AddDays(29);
        var day31 = firstDay.AddDays(30);

        if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
        || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
        || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
        {
            NoOfSunday = 5;
        }
        else
        {
            NoOfSunday = 4;
        }

        int NumOfDay = DateTime.DaysInMonth(year, month);

        if (counter == 1)
        {//calendar days
            return NumOfDay;
        }
        else
        { //working days
            return NumOfDay - NoOfSunday;
        }
    }

    public string get_calendar_days(int starting_day, string month_year, int counter, int tb_select)
    {


        string days = "", days1 = "", days2 = "", days3 = "", days4 = "", days5 = "", days6 = "", days100 = "", sunday = "", employee_type = "",day_attendance="";
        int k = 1;
        int month = int.Parse(month_year.Substring(0, 2));
        int year = int.Parse(month_year.Substring(3));

        int attendance_pdf = 0;
        //table selection
        if (tb_select == 1)
        {
            employee_type = "pay_employee_master.employee_type";
        }
        else
        {

            employee_type = "pay_billing_unit_rate_history.emp_type";
        }

        if (starting_day != 1)
        {
            month = int.Parse(month_year.Substring(0, 2)) - 1;
            if (month == 0) { month = 12; --year; }
            int monthdays = DateTime.DaysInMonth(year, month);
            for (int i = 0; monthdays >= (i + starting_day); i++)
            {
                string kstr = k.ToString();
                if (k < 10)
                { kstr = "0" + k.ToString(); }

                if (i + starting_day < 10)
                {
                    days = days + "<th>" + (i + starting_day) + "</th>";
                    //days1 = days1 + "case when t2.DAY" + "0" + (i + starting_day) + " = '0' then 'A' else  ifnull(t2.DAY" + "0" + (i + starting_day) + ",'A') end as DAY" + kstr + ",";
                    days1 = days1 + "case when t2.DAY" + "0" + (i + starting_day) + " = '0' then 'A' else  IF(SUBSTRING(DATE_FORMAT(pay_employee_master.joining_date, '%d/%m/%Y'),1, 2) <= SUBSTR('DAY" + "0" + (i + starting_day) + "',4, 2) and " + employee_type + " = 'Permanent', ifnull(t2.DAY" + "0" + (i + starting_day) + ",'P'), ifnull(t2.DAY" + "0" + (i + starting_day) + ",'A')) end as DAY" + kstr + ",";
                    days100 = days100 + "case when t2.DAY" + "0" + (i + starting_day) + " = 'A' then ''  when t2.DAY" + "0" + (i + starting_day) + " = 'P' then ''  when t2.DAY" + "0" + (i + starting_day) + " = 'W' then ''  when t2.DAY" + "0" + (i + starting_day) + " = 'PH' then '' when t2.DAY" + "0" + (i + starting_day) + " = 'HD' then ''  else  IF(SUBSTRING(DATE_FORMAT(pay_employee_master.joining_date, '%d/%m/%Y'),1, 2) <= SUBSTR('DAY" + "0" + (i + starting_day) + "',4, 2) and " + employee_type + " = 'Permanent', ifnull(t2.DAY" + "0" + (i + starting_day) + ",''), ifnull(t2.DAY" + "0" + (i + starting_day) + ",'')) end as DAY" + kstr + ",";
                    days2 = days2 + "ifnull(t3.OT_DAY" + "0" + (i + starting_day) + ",0) as OT_DAY" + kstr + ",";
                    //vikas add code 04/05/2019
                    days3 = days3 + "ifnull(t4.OT_DAILY_DAY" + "0" + (i + starting_day) + ",0) as OT_DAILY_DAY" + kstr + ",";
                    //days5 = days5 + "CASE t4.OT_DAILY_DAY0" + (i + starting_day) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(t4.OT_DAILY_DAY0" + (i + starting_day) + ") ,0) END AS OT_DAILY_DAY" + kstr + ",";
                    
                    //sec_to_time convert code
                    //days5 = days5 + "CASE t4.OT_DAILY_DAY0" + (i + starting_day) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(t4.OT_DAILY_DAY0" + (i + starting_day) + ") ,0) END AS OT_DAILY_DAY" + kstr + ",";
                    attendance_pdf++;
                  //  days5 = days5 + "ifnull(t4.OT_DAILY_DAY" + "0" + (i + starting_day) + ",0) as OT_DAILY_DAY" + kstr + ",";
                    
                    day_attendance = day_attendance + "'" + (i + starting_day) + "' as 'HDAY" + (attendance_pdf.ToString().Length == 1 ? "0" + attendance_pdf.ToString() : attendance_pdf.ToString()) + "',";
                    // days6 = days6 + "CASE t4.OT_DAILY_DAY0" + (i + starting_day) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(t4.OT_DAILY_DAY0" + (i + starting_day) + ") ,0) END AS OT_DAILY_DAY" + kstr + ",";
                   
                    days4 = days4 + " DAY" + "0" + (i + starting_day) + ",";
                }
                else
                {
                    if (new DateTime(year, month, i + starting_day).DayOfWeek == DayOfWeek.Sunday)
                    { sunday = "'W'"; }
                    else { sunday = "'P'"; }
                    days = days + "<th>" + (i + starting_day) + "</th>";
                    //days1 = days1 + "case when t2.DAY" + (i + starting_day) + " = '0' then 'A' else ifnull(t2.DAY" + (i + starting_day) + ",'A') end as DAY" + kstr + ",";
                    days1 = days1 + "case when t2.DAY" + (i + starting_day) + " = '0' then 'A' else IF(SUBSTRING(DATE_FORMAT(pay_employee_master.joining_date, '%d/%m/%Y'),1, 2) <= SUBSTR('DAY" + (i + starting_day) + "',4, 2) and " + employee_type + " = 'Permanent', ifnull(t2.DAY" + (i + starting_day) + "," + sunday + "), ifnull(t2.DAY" + (i + starting_day) + ",'A')) end as DAY" + kstr + ",";
                    days100 = days100 + "case when t2.DAY" + (i + starting_day) + " = 'A' then '' when t2.DAY" + (i + starting_day) + " = 'P' then ''  when t2.DAY" + (i + starting_day) + " = 'W' then ''  when t2.DAY" + (i + starting_day) + " = 'PH' then ''  when t2.DAY" + (i + starting_day) + " = 'HD' then '' else IF(SUBSTRING(DATE_FORMAT(pay_employee_master.joining_date, '%d/%m/%Y'),1, 2) <= SUBSTR('DAY" + (i + starting_day) + "',4, 2) and " + employee_type + " = 'Permanent', ifnull(t2.DAY" + (i + starting_day) + "," + sunday + "), ifnull(t2.DAY" + (i + starting_day) + ",'A')) end as DAY" + kstr + ",";
                    days2 = days2 + "ifnull(t3.OT_DAY" + (i + starting_day) + ",0) as OT_DAY" + kstr + ",";
                    days3 = days3 + "ifnull(t4.OT_DAILY_DAY" + (i + starting_day) + ",0) as OT_DAILY_DAY" + kstr + ",";
                    //days5 = days5 + "CASE t4.OT_DAILY_DAY" + (i + starting_day) + " WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((t4.OT_DAILY_DAY" + (i + starting_day) + ") / 1000)),1, 8), 0) END AS OT_DAILY_DAY" + kstr + ",";
                   //sec_to_time convert
                    //days5 = days5 + "CASE t4.OT_DAILY_DAY" + (i + starting_day) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(t4.OT_DAILY_DAY" + (i + starting_day) + "),0) END AS OT_DAILY_DAY" + kstr + ",";
                    attendance_pdf++;
                    day_attendance = day_attendance + "'" + (i + starting_day) + "' as 'HDAY" + (attendance_pdf.ToString().Length==1?"0"+attendance_pdf.ToString():attendance_pdf.ToString()) + "',";
                    days4 = days4 + " DAY" + (i + starting_day) + ",";
                }
                ++k;
            }
        }
        else
        { starting_day = DateTime.DaysInMonth(year, month) + 1; }
        for (int i = 1; (starting_day - 1) >= i; i++)
        {
            string kstr = k.ToString();
            if (k < 10)
            { kstr = "0" + k.ToString(); }

            if (i < 10)
            {
                days = days + "<th>" + i + "</th>";
                days1 = days1 + "case when pay_attendance_muster.DAY" + "0" + i + " = '0' then 'A' else IFNULL(pay_attendance_muster.DAY" + "0" + i + ", 'A') end as DAY" + kstr + ",";
                days100 = days100 + "case when pay_attendance_muster.DAY" + "0" + i + " = 'A' then '' when pay_attendance_muster.DAY" + "0" + i + " = 'P' then ''  when pay_attendance_muster.DAY" + "0" + i + " = 'W' then ''  when pay_attendance_muster.DAY" + "0" + i + " = 'PH' then '' when pay_attendance_muster.DAY" + "0" + i + " = 'HD' then '' else IFNULL(pay_attendance_muster.DAY" + "0" + i + ", 'A') end as DAY" + kstr + ",";
                days2 = days2 + "IFNULL(pay_ot_muster.OT_DAY" + "0" + (i) + ",0) as OT_DAY" + kstr + ",";
                //vikas add code 04/05/2019
                days3 = days3 + "IFNULL(pay_daily_ot_muster.OT_DAILY_DAY" + "0" + (i) + ",0) as OT_DAILY_DAY" + kstr + ",";
               // days5 = days5 + "CASE pay_daily_ot_muster.OT_DAILY_DAY0" + (i) + " WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((pay_daily_ot_muster.OT_DAILY_DAY0" + (i) + ") / 1000)),1, 8), 0) END AS OT_DAILY_DAY" + kstr + ",";
                //sec_to_time convert
               // days5 = days5 + "CASE pay_daily_ot_muster.OT_DAILY_DAY0" + (i) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(pay_daily_ot_muster.OT_DAILY_DAY0" + (i) + ") ,0) END AS OT_DAILY_DAY" + kstr + ",";
                attendance_pdf++;
                days4 = days4 + " DAY0" + (i) + ",";
                day_attendance = day_attendance + "'" + (i) + "' as  'HDAY" + (attendance_pdf.ToString().Length == 1 ? "0" + attendance_pdf.ToString() : attendance_pdf.ToString()) + "',";
            }
            else
            {
                days = days + "<th>" + i + "</th>";
                days1 = days1 + "case when pay_attendance_muster.DAY" + i + " = '0' then 'A' else IFNULL(pay_attendance_muster.DAY" + i + " , 'A') end as DAY" + kstr + ",";
                days100 = days100 + "case when pay_attendance_muster.DAY" + i + " = 'A' then '' when pay_attendance_muster.DAY" + i + " = 'P' then '' when pay_attendance_muster.DAY" + i + " = 'W' then ''  when pay_attendance_muster.DAY" + i + " = 'PH' then ''  when pay_attendance_muster.DAY" + i + " = 'HD' then '' else IFNULL(pay_attendance_muster.DAY" + i + " , 'A') end as DAY" + kstr + ",";
                days2 = days2 + "IFNULL(pay_ot_muster.OT_DAY" + (i) + ",0) as OT_DAY" + kstr + ",";
                //vikas add code 04/05/2019
                days3 = days3 + "IFNULL(pay_daily_ot_muster.OT_DAILY_DAY" + (i) + ",0) as OT_DAILY_DAY" + kstr + ",";
                //days5 =days5 + "CASE pay_daily_ot_muster.OT_DAILY_DAY" + (i) + " WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((pay_daily_ot_muster.OT_DAILY_DAY" + (i) + ") / 1000)),1, 8), 0) END AS OT_DAILY_DAY" + kstr + ",";
                //sec_to_time convert
                //days5 = days5 + "CASE pay_daily_ot_muster.OT_DAILY_DAY" + (i) + " WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(pay_daily_ot_muster.OT_DAILY_DAY" + (i) + "),0) END AS OT_DAILY_DAY" + kstr + ",";
                attendance_pdf++;
                days4 = days4 + " DAY" + (i) + ",";
                day_attendance = day_attendance + "'" + (i) + "' as 'HDAY" + (attendance_pdf.ToString().Length == 1 ? "0" + attendance_pdf.ToString() : attendance_pdf.ToString()) + "',";
            }
            ++k;

        }
        if (counter == 1)
        {
            return days1;
        }
        else if (counter == 3)
        {
            return days2;
        }
        //vikas add 06/05/2019t4
        else if (counter == 4)
        {
          //  string daysss = days5;
           // return days3;
            //changes to rahul 18-10-2019
            //return days5;
            return days3;
        }
        else if (counter == 5)
        {
            return days4;
        }
        else if (counter == 100)
        {
            return days100;
        }
         else if (counter == 6)
        {
            return days1 + "" + day_attendance;
        }
        else
        {
            return days;
        }
    }
    //sir changes 1-11-18
    public string PrintSundays(int month, int year, int counter)
    {
        string days = "", sun_day = "", reliever_query = "", all_days_abs = "", all_days = "";
        CultureInfo ci = new CultureInfo("en-US");
        int counter1 = ci.Calendar.GetDaysInMonth(year, month);
        for (int i = 1; i <= counter1; i++)
        {
            if (new DateTime(year, month, i).DayOfWeek == DayOfWeek.Sunday)
            {
                sun_day = sun_day + "'W',";
                if (i < 10)
                {
                    days = days + "DAY0" + i + ",";
                    reliever_query = reliever_query + "DAY0" + i + "='W',";
                    all_days = all_days + "DAY0" + i + ",";
                    all_days_abs = all_days_abs + "'W',";
                }
                else
                {
                    days = days + "DAY" + i + ",";
                    reliever_query = reliever_query + "DAY" + i + "='W',";
                    all_days = all_days + "DAY" + i + ",";
                    all_days_abs = all_days_abs + "'W',";
                }
            }
            else
            {
                if (i < 10)
                {
                    reliever_query = reliever_query + "DAY0" + i + "='A',";
                    all_days = all_days + "DAY0" + i + ",";
                    all_days_abs = all_days_abs + "'A',";
                }
                else
                {
                    reliever_query = reliever_query + "DAY" + i + "='A',";
                    all_days_abs = all_days_abs + "'A',";
                    all_days = all_days + "DAY" + i + ",";
                }
            }
        }

        if (counter == 2)
        {
            return reliever_query.Substring(0, reliever_query.Length - 1);
        }
        else if (counter == 1)
        {
            return days.Substring(0, days.Length - 1);
        }
        else if (counter == 3)
        {
            return all_days.Substring(0, all_days.Length - 1);
        }
        else if (counter == 4)
        {
            return all_days_abs.Substring(0, all_days_abs.Length - 1);
        }
        else
        { return sun_day.Substring(0, sun_day.Length - 1); }
    }
    public string PrintOTSundays(int month, int year, int counter)
    {
        string days = "", sun_day = "";
        CultureInfo ci = new CultureInfo("en-US");
        int dayss = ci.Calendar.GetDaysInMonth(year, month);
        for (int i = 1; i <= dayss; i++)
        {
            if (new DateTime(year, month, i).DayOfWeek == DayOfWeek.Sunday)
            {
                sun_day = sun_day + "'8',";
                if (i < 10)
                {
                    days = days + "OT_DAY0" + i + ",";
                }
                else
                {
                    days = days + "OT_DAY" + i + ",";
                }
            }
        }
        if (counter == 1)
        {
            return days.Substring(0, days.Length - 1);
        }
        else
        { return sun_day.Substring(0, sun_day.Length - 1); }
    }
    // vikas add 04/05/2019
    public string PrintOTDaySundays(int month, int year, int counter)
    {
        string days = "", sun_day = "";
        CultureInfo ci = new CultureInfo("en-US");
        int dayss = ci.Calendar.GetDaysInMonth(year, month);
        for (int i = 1; i <= dayss; i++)
        {
            if (new DateTime(year, month, i).DayOfWeek == DayOfWeek.Sunday)
            {
                sun_day = sun_day + "'0',";
                if (i < 10)
                {
                    days = days + "OT_DAILY_DAY0" + i + ",";
                }
                else
                {
                    days = days + "OT_DAILY_DAY" + i + ",";
                }
            }
        }
        if (counter == 1)
        {
            return days.Substring(0, days.Length - 1);
        }
        else
        { return sun_day.Substring(0, sun_day.Length - 1); }
    }
    //end 04/05/2019
    //vikas
    public DataTable chk_attendance1(string compcode, string clientcode, string state_code, string txt_date1, int counter)
    {
        //System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item;

        //if (state_code.ToUpper() == "SELECT")
        //{
        //    cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master where comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and unit_code not in ( select distinct pay_attendance_muster.unit_code from pay_attendance_muster inner join pay_unit_master on pay_attendance_muster.unit_code = pay_unit_master.unit_Code and pay_attendance_muster.comp_code = pay_unit_master.comp_code where  pay_attendance_muster.comp_code = '" + compcode + "' and pay_unit_master.client_code = '" + clientcode + "')", con);
        //}
        //else
        //{
        //    cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master where state_name = '"+state_code+"' and comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and unit_code not in ( select distinct pay_attendance_muster.unit_code from pay_attendance_muster inner join pay_unit_master on pay_attendance_muster.unit_code = pay_unit_master.unit_Code and pay_attendance_muster.comp_code = pay_unit_master.comp_code where  pay_attendance_muster.comp_code = '" + compcode + "' and pay_unit_master.client_code = '" + clientcode + "' and pay_unit_master.state_name = '" + state_code + "')", con);
        //}
        //con.Open();
        //try
        //{
        //    cmd_item.Fill(dt_item);
        //    con.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    con.Close();
        //}
        //return dt_item;
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item;

        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master where state_name = '" + state_code + "'and comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and unit_code not in ( select distinct pay_attendance_muster.unit_code from pay_attendance_muster inner join pay_unit_master on pay_attendance_muster.unit_code = pay_unit_master.unit_Code and pay_attendance_muster.comp_code = pay_unit_master.comp_code where month = " + txt_date1.Substring(0, 2) + " and year = " + txt_date1.Substring(3) + " and pay_attendance_muster.comp_code = '" + compcode + "' and pay_unit_master.client_code = '" + clientcode + "')", con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master where state_name = '" + state_code + "'and comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and unit_code not in (select distinct pay_billing_master.billing_unit_code from pay_billing_master where pay_billing_master.comp_code = '" + compcode + "' and pay_billing_master.billing_client_code = '" + clientcode + "')", con);
        }
        con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;

    }

    public DataTable chk_attendance(string compcode, string clientcode, string txttotdate, int counter,string billing)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string month = txttotdate.Substring(0, 2);
        string year = txttotdate.Substring(3);
        string start_date = get_start_date(compcode, clientcode, txttotdate);
        string end_date = get_end_date(compcode, clientcode, txttotdate);
        //remaning branch
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT distinct CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT' FROM  pay_unit_master  INNER JOIN  pay_employee_master  ON  pay_unit_master . unit_code  =  pay_employee_master . unit_code  AND  pay_unit_master . comp_code  =  pay_employee_master . comp_code and branch_status = 0  WHERE pay_employee_master. comp_code  = '" + compcode + "' AND pay_employee_master. client_code  = '" + clientcode + "' and (left_date >= str_to_date('" + start_date + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + end_date + "','%d/%m/%Y') AND pay_employee_master. unit_code  NOT IN (SELECT DISTINCT  pay_attendance_muster . unit_code  FROM  pay_attendance_muster   INNER JOIN  pay_unit_master  ON  pay_attendance_muster . unit_code  =  pay_unit_master . unit_Code  AND  pay_attendance_muster . comp_code  =  pay_unit_master . comp_code   WHERE  pay_attendance_muster.month  = '" + txttotdate.Substring(0, 2) + " ' AND  pay_attendance_muster.year  = '" + txttotdate.Substring(3) + "' AND  pay_attendance_muster . comp_code  = '" + compcode + "' AND ( pay_attendance_muster . flag  = 1 ||  pay_attendance_muster . flag  = 2) AND  pay_unit_master . client_code  = '" + clientcode + "'  ) order by pay_unit_master.state_name , pay_unit_master.unit_name", con);
        }
        // policy
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT,unit_code FROM pay_unit_master where comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and branch_status = 0 and unit_code not in (select distinct pay_billing_master.billing_unit_code from pay_billing_master where pay_billing_master.comp_code = '" + compcode + "' and pay_billing_master.billing_client_code = '" + clientcode + "'  group by pay_unit_master.state_name order by pay_unit_master.unit_name ) and unit_code  not in (SELECT  unit_code  FROM pay_unit_master   WHERE comp_code  = '" + compcode + "' AND  client_code  = '" + clientcode + "' and branch_status = 0  and  unit_code   not in(select distinct unit_code  from pay_employee_master where client_code= '" + clientcode + "' and comp_code='" + compcode + "' and (left_date >= str_to_date('" + start_date + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + end_date + "','%d/%m/%Y')))", con);
        }
        //approve by admin komal 06-05-2020
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT,unit_code FROM pay_unit_master where comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and branch_status = 0 and unit_code  in ( select distinct pay_attendance_muster.unit_code from pay_attendance_muster inner join pay_unit_master on pay_attendance_muster.unit_code = pay_unit_master.unit_Code and pay_attendance_muster.comp_code = pay_unit_master.comp_code where pay_attendance_muster.month = " + txttotdate.Substring(0, 2) + " and pay_attendance_muster.year = " + txttotdate.Substring(3) + " and pay_unit_master.comp_code = '" + compcode + "'   AND `pay_attendance_muster`.flag = 1 and pay_unit_master.client_code = '" + clientcode + "' order by pay_unit_master.state_name , pay_unit_master.unit_name)", con);
        }
        //reject
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT  CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT',    Rejected_Reason , date_format( rejected_date ,'%d/%m/%Y') as 'Rejected_Date', (select EMP_NAME from pay_employee_master where  pay_attendance_reject_master . rejected_by  =  pay_employee_master . emp_code ) as Rejected_By  FROM  pay_unit_master  INNER JOIN  pay_attendance_reject_master  ON  pay_unit_master . COMP_CODE  =  pay_attendance_reject_master . COMP_CODE  AND  pay_unit_master . unit_code  =  pay_attendance_reject_master . unit_code  WHERE  pay_attendance_reject_master.flag = 0 and  pay_unit_master . comp_code  = '" + compcode + "' AND  pay_unit_master . client_code  = '" + clientcode + "' AND pay_attendance_reject_master. month  = '" + txttotdate.Substring(0, 2) + "' AND  pay_attendance_reject_master.year  = '" + txttotdate.Substring(3) + "' order by pay_unit_master.state_name , pay_unit_master.unit_name", con);
        }
        //approve by finance
        else if (counter == 4)
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT,unit_code FROM pay_unit_master where comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and branch_status = 0 and unit_code  in ( select distinct pay_attendance_muster.unit_code from pay_attendance_muster inner join pay_unit_master on pay_attendance_muster.unit_code = pay_unit_master.unit_Code and pay_attendance_muster.comp_code = pay_unit_master.comp_code where pay_attendance_muster.month = " + txttotdate.Substring(0, 2) + " and pay_attendance_muster.year = " + txttotdate.Substring(3) + " and pay_unit_master.comp_code = '" + compcode + "' and pay_attendance_muster.flag = 2 and pay_unit_master.client_code = '" + clientcode + "' ) order by pay_unit_master.state_name , pay_unit_master.unit_name", con);
        }
        // branch having no  deployment
        else if (counter == 5)
        {
            //     cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT',  unit_code  FROM pay_unit_master   WHERE comp_code  = '" + compcode + "' AND  client_code  = '" + clientcode + "'  and  unit_code   not in(select distinct unit_code  from pay_employee_master where client_code= '" + clientcode + "' and comp_code='" + compcode + "' and (left_date >= str_to_date('1/" + txttotdate.Substring(0, 2) + "/" + txttotdate.Substring(3) + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(txttotdate.Substring(0, 2)), int.Parse(txttotdate.Substring(0, 2))) + "/" + txttotdate.Substring(0, 2) + "/" + txttotdate.Substring(3) + "','%d/%m/%Y') group by pay_unit_master.state_name order by pay_unit_master.unit_name)", con);


            cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT',  unit_code  FROM pay_unit_master   WHERE comp_code  = '" + compcode + "' AND  client_code  = '" + clientcode + "' and branch_status = 0  and  unit_code   not in(select distinct unit_code  from pay_employee_master where client_code= '" + clientcode + "' and comp_code='" + compcode + "' and (left_date >= str_to_date('" + start_date + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + end_date + "','%d/%m/%Y') order by pay_unit_master.state_name , pay_unit_master.unit_name)", con);
        }
        else if (counter == 6)
        {
            //     cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT',  unit_code  FROM pay_unit_master   WHERE comp_code  = '" + compcode + "' AND  client_code  = '" + clientcode + "'  and  unit_code   not in(select distinct unit_code  from pay_employee_master where client_code= '" + clientcode + "' and comp_code='" + compcode + "' and (left_date >= str_to_date('1/" + txttotdate.Substring(0, 2) + "/" + txttotdate.Substring(3) + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(txttotdate.Substring(0, 2)), int.Parse(txttotdate.Substring(0, 2))) + "/" + txttotdate.Substring(0, 2) + "/" + txttotdate.Substring(3) + "','%d/%m/%Y') group by pay_unit_master.state_name order by pay_unit_master.unit_name)", con);


            cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'CUNIT', unit_code FROM pay_unit_master WHERE comp_code = '" + compcode + "' AND client_code = '" + clientcode + "' AND branch_status = 1  order by pay_unit_master.state_name , pay_unit_master.unit_name", con);
        }
        //payment approve
        else if (counter == 7)
        {
            cmd_item = new MySqlDataAdapter("SELECT  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'CUNIT', unit_code  FROM  pay_unit_master  WHERE  comp_code = '" + compcode + "' AND unit_code IN (SELECT unit_code FROM pay_pro_master WHERE comp_code = '" + compcode + "' AND client_code = '" + clientcode + "'  and month='" + month + "' and year = '" + year + "' AND payment_approve != 0)  ORDER BY pay_unit_master.state_name, pay_unit_master.unit_name", con);
        }
        //payment reject by MD
        else if (counter == 8)
        {
            cmd_item = new MySqlDataAdapter("SELECT  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'CUNIT', unit_code  FROM  pay_unit_master  WHERE  comp_code = '" + compcode + "' AND unit_code IN (SELECT unit_code FROM pay_pro_master WHERE comp_code = '" + compcode + "' AND client_code = '" + clientcode + "'  and month='" + month + "' and year = '" + year + "' AND payment_approve = 0 AND reject_reason !='')  ORDER BY pay_unit_master.state_name, pay_unit_master.unit_name", con);
        }
        else if (counter == 9)
        {
            cmd_item = new MySqlDataAdapter("select CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'CUNIT', unit_code from pay_unit_master where comp_code = '" + compcode + "' and branch_status=0 and client_code = '" + clientcode + "' and state_name not in (select state_name from pay_bill_invoices where client_code = '" + clientcode + "' and comp_code = '" + compcode + "' and month=" + month + " and year = " + year + ") ORDER BY pay_unit_master.state_name, pay_unit_master.unit_name", con);
        }
        //final bill approve by finance //komal changes 06-05-2020
        else if (counter == 10)
        {
            cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT,unit_code FROM pay_unit_master where comp_code = '" + compcode + "' and client_code = '" + clientcode + "' and branch_status = 0 and unit_code  in ( select distinct pay_billing_unit_rate_history.unit_code from pay_billing_unit_rate_history inner join pay_unit_master on pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_Code and pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code where pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " and pay_billing_unit_rate_history.comp_code = '" + compcode + "' and pay_billing_unit_rate_history.invoice_flag != 0 and pay_billing_unit_rate_history.client_code = '" + clientcode + "' ) order by pay_unit_master.state_name , pay_unit_master.unit_name", con);
        }

// approve by finance  komal 07-05-2020

        else if(counter ==11)
        {
            if (billing == "Statewise")
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct (pay_billing_unit_rate_history.state_name) as 'CUNIT'  FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` != '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewise'  AND `billing_name` = 'Manpower Billing')", con);
            }
            else if (billing == "Branchwise") 
            {

                cmd_item = new MySqlDataAdapter("SELECT distinct concat((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME) ,'_',pay_unit_master.UNIT_CITY,'_',pay_unit_master.UNIT_ADD1,'_',pay_unit_master.UNIT_NAME) as 'CUNIT' FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name`  AND `pay_billing_unit_rate_history`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` != '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Branchwise'  AND `billing_name` = 'Manpower Billing' )", con);
            }
            else if (billing == "Statewisedesignation") 
            {

                cmd_item = new MySqlDataAdapter("SELECT distinct CONCAT(pay_billing_unit_rate_history.`state_name`, '-', `pay_billing_unit_rate_history`.`GRADE_CODE`) AS 'CUNIT' FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` != '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewisedesignation'  AND `billing_name` = 'Manpower Billing' )", con);
            
            }
            else if (billing == "Regionwise")
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct (pay_billing_unit_rate_history.state_name) as 'CUNIT'  FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` = '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewise'  AND `billing_name` = 'Manpower Billing')", con);
            }
        }

            // not approve by finance  komal 07-05-2020
        else if (counter == 12)
        {
            if (billing == "Statewise")
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct (pay_billing_unit_rate_history.state_name) as 'CUNIT'  FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` = '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewise'  AND `billing_name` = 'Manpower Billing')", con);
            }
            else if (billing == "Branchwise")
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct concat((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME) ,'_',pay_unit_master.UNIT_CITY,'_',pay_unit_master.UNIT_ADD1,'_',pay_unit_master.UNIT_NAME) as 'CUNIT' FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name`  AND `pay_billing_unit_rate_history`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag`= '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Branchwise'  AND `billing_name` = 'Manpower Billing' )", con);
            }
            else if (billing == "Statewisedesignation")
            {

                cmd_item = new MySqlDataAdapter("SELECT distinct CONCAT(pay_billing_unit_rate_history.`state_name`, '-', `pay_billing_unit_rate_history`.`GRADE_CODE`) AS 'CUNIT' FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` = '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewisedesignation'  AND `billing_name` = 'Manpower Billing' )", con);
            }
            else if (billing == "Regionwise")
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct (pay_billing_unit_rate_history.state_name) as 'CUNIT'  FROM `pay_billing_unit_rate_history`  INNER JOIN `pay_unit_master` ON `pay_billing_unit_rate_history`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_billing_unit_rate_history`.`client_code` = `pay_unit_master`.`client_code` AND `pay_billing_unit_rate_history`.`state_name` = `pay_unit_master`.`state_name` WHERE pay_billing_unit_rate_history.`comp_code` = '" + compcode + "'  AND pay_billing_unit_rate_history.`client_code` = '" + clientcode + "' AND `invoice_flag` = '0' and pay_billing_unit_rate_history.month = " + txttotdate.Substring(0, 2) + " and pay_billing_unit_rate_history.year = " + txttotdate.Substring(3) + " AND pay_billing_unit_rate_history.`state_name` IN (SELECT DISTINCT `state` FROM `pay_client_billing_details` WHERE `comp_code` = '" + compcode + "' AND `client_code` = '" + clientcode + "'  AND `billing_wise` = 'Statewise'  AND `billing_name` = 'Manpower Billing')", con);
            }
        }

      //  end

        // branch having no  deployment
        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }

    public DataTable chk_emp_con(string comp_code, string client_code, string state_name, string unit_code, string month, string year, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string where = "AND pay_unit_master.client_code = '" + client_code + "'";

        if (state_name != "Select")
        {
            where = where + "AND pay_unit_master.state_name = '" + state_name + "'";

        }

        where = "pay_conveyance_amount_history.month = '" + month + " ' AND pay_conveyance_amount_history.year = '" + year + "' AND pay_conveyance_amount_history.comp_code = '" + comp_code + "'  AND pay_conveyance_amount_history.client_code = '" + client_code + "'  group by pay_conveyance_amount_history.unit_code )";
        if (state_name != "Select")
        {
            where = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "'  AND pay_unit_master.state_name ='" + state_name + "' and pay_conveyance_amount_history.month = '" + month + " ' AND pay_conveyance_amount_history.year = '" + year + "' ";
        }


        //Remaining Approve By Admin
        if (counter == 0)
        {
             cmd_item = new MySqlDataAdapter(" SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_Code AND pay_billing_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_billing_master.comp_code = pay_client_master.comp_code WHERE pay_billing_master.conveyance_applicable = 1 AND pay_billing_master.comp_code = '" + comp_code + "' AND pay_billing_master.billing_client_code = '" + client_code + "' AND pay_billing_master.billing_state = '" + state_name + "' AND billing_unit_code NOT IN (SELECT unit_code FROM pay_conveyance_amount_history WHERE pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' AND pay_conveyance_amount_history.state = '" + state_name + "' AND pay_conveyance_amount_history.conveyance_flag != 0 AND pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' and conveyance_rate != 0 GROUP BY unit_code) GROUP BY pay_billing_master.billing_unit_code", con);
           // cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_Code AND pay_billing_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_billing_master.comp_code = pay_client_master.comp_code LEFT JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.comp_code = pay_billing_master.comp_code AND pay_conveyance_amount_history.unit_code = pay_billing_master.billing_unit_code AND pay_conveyance_amount_history.conveyance_flag = 0 AND pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' WHERE pay_billing_master.conveyance_applicable = 1 AND pay_billing_master.comp_code = '" + comp_code + "' AND pay_billing_master.billing_client_code = '" + client_code + "' AND pay_billing_master.billing_state = '" + state_name + "' GROUP BY pay_billing_master.billing_unit_code", con);
        }
        // Approve By Admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.conveyance_flag = 1  AND " + where + " group by pay_conveyance_amount_history.unit_code", con);
        }
        // Approve By Finance
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.conveyance_flag = 2 AND " + where + " group by pay_conveyance_amount_history.unit_code ", con);
        }
        // Reject By Finance
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.conveyance_flag = 3 AND " + where + " group by pay_conveyance_amount_history.unit_code ", con);
        }

        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }
    public DataTable chk_driver_con(string comp_code, string client_code, string state_name, string unit_code, string month, string year, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string where = "AND pay_unit_master.client_code = '" + client_code + "'";

        if (state_name != "Select")
        {
            where = where + "AND pay_unit_master.state_name = '" + state_name + "'";

        }

        where = "pay_conveyance_amount_history.month = '" + month + " ' AND pay_conveyance_amount_history.year = '" + year + "' AND pay_conveyance_amount_history.comp_code = '" + comp_code + "'  AND pay_conveyance_amount_history.client_code = '" + client_code + "'  group by pay_conveyance_amount_history.unit_code )";
        if (state_name != "Select")
        {
            where = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "'  AND pay_unit_master.state_name ='" + state_name + "' and pay_conveyance_amount_history.month = '" + month + " ' AND pay_conveyance_amount_history.year = '" + year + "'  group by pay_conveyance_amount_history.emp_code ";
        }
        if (unit_code != "ALL")
        {
            where = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "'  AND pay_unit_master.state_name ='" + state_name + "' and pay_conveyance_amount_history.unit_code = '" + unit_code + "' and pay_conveyance_amount_history.month = '" + month + " ' AND pay_conveyance_amount_history.year = '" + year + "'  group by pay_conveyance_amount_history.emp_code ";
        }

        //Remaining Approve By Admin
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name,emp_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.driver_conv_flag = 0 AND " + where, con);
        }
        // Approve By Admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name,emp_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.driver_conv_flag = 1  AND " + where, con);
        }
        // Approve By Finance
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name,emp_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.driver_conv_flag = 2 AND " + where, con);
        }
        // Reject By Finance
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_name, state_name, unit_name,emp_name FROM pay_conveyance_amount_history INNER JOIN pay_unit_master ON pay_conveyance_amount_history.unit_code = pay_unit_master.unit_Code AND pay_conveyance_amount_history.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_conveyance_amount_history.comp_code = pay_client_master.comp_code WHERE  pay_conveyance_amount_history.driver_conv_flag = 3 AND " + where, con);
        }

        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }
    public DataTable chk_material(string comp_code, string client_code, string state_name, string unit_code, string month, string year, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string where = "AND pay_unit_master.client_code = '" + client_code + "'";

        if (state_name != "Select")
        {
            where = where + "AND pay_unit_master.state_name = '" + state_name + "'";

        }

        where = "pay_material_details.month = '" + month + " ' AND pay_material_details.year = '" + year + "' AND pay_material_details.comp_code = '" + comp_code + "'  AND pay_material_details.client_code = '" + client_code + "'  group by pay_material_details.unit_code )";
        if (state_name != "Select")
        {
            where = "pay_material_details.comp_code = '" + comp_code + "' AND pay_material_details.client_code = '" + client_code + "'  AND pay_material_details.state_name ='" + state_name + "' and pay_material_details.month = '" + month + "' AND pay_material_details.year = '" + year + "' ";
        }


        //Remaining Approve By Admin
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_Code AND pay_billing_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_billing_master.comp_code = pay_client_master.comp_code and `pay_billing_master`.`billing_client_code` = `pay_client_master`.client_code WHERE pay_billing_master.material_contract != 0 AND pay_billing_master.comp_code = '" + comp_code + "' AND pay_billing_master.billing_client_code = '" + client_code + "' AND pay_billing_master.billing_state = '" + state_name + "'  AND unit_code NOT IN (SELECT unit_code FROM pay_material_details WHERE pay_material_details.comp_code = '" + comp_code + "' AND pay_material_details.client_code = '" + client_code + "' AND pay_material_details.state_name = '" + state_name + "'  AND pay_material_details.material_flag != 0 AND pay_material_details.month = '" + month + "' AND pay_material_details.year = '" + year + "' GROUP BY unit_code) GROUP BY pay_billing_master.billing_unit_code", con);
          //  cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_Code AND pay_billing_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_billing_master.comp_code = pay_client_master.comp_code LEFT JOIN pay_material_details ON pay_material_details.comp_code = pay_billing_master.comp_code AND pay_material_details.unit_code = pay_billing_master.billing_unit_code AND pay_material_details.material_flag = 0 AND pay_material_details.month = '" + month + "' AND pay_material_details.year = '2019' WHERE pay_billing_master.material_contract != 0 AND pay_billing_master.comp_code = '" + comp_code + "' AND pay_billing_master.billing_client_code = '" + client_code + "' AND pay_billing_master.billing_state = '" + state_name + "' GROUP BY pay_billing_master.billing_unit_code", con);

        }
        // Approve By Admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_material_details.state_name, pay_unit_master.unit_name FROM pay_material_details INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_Code AND pay_material_details.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND `pay_material_details`.`client_code` = `pay_client_master`.`client_code`  WHERE  pay_material_details.material_flag = 1  AND " + where + " group by pay_material_details.unit_code", con);
        }
        // Approve By Finance
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_material_details.state_name, pay_unit_master.unit_name FROM pay_material_details INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_Code AND pay_material_details.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code and `pay_material_details`.`client_code` = `pay_client_master`.`client_code` WHERE  pay_material_details.material_flag = 2  AND " + where + " group by pay_material_details.unit_code", con);
        }
        // Reject By Finance
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, pay_material_details.state_name, pay_unit_master.unit_name FROM pay_material_details INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_Code AND pay_material_details.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code  and `pay_material_details`.`client_code` = `pay_client_master`.client_code WHERE  pay_material_details.material_flag = 3  AND " + where + " group by pay_material_details.unit_code", con);
        }

        con.Open();
        try
        {
           // cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }
    
    public string emp_count(string compcode, string clientcode, string txttotdate, int counter)
    {
        try
        {
            string employee_count = "0";
            string start_date = get_start_date(compcode, clientcode, txttotdate);
            string end_date = get_end_date(compcode, clientcode, txttotdate);

            //closed branch emp_cout  
            if (counter == 0)
            {
                return getsinglestring("SELECT COUNT(pay_employee_master.emp_code) AS 'Emp_count' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' AND pay_unit_master.client_code = '" + clientcode + "' AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') AND pay_unit_master.branch_status = 1");
            }
            //remaing branch count
            if (counter == 1)
            {
                return getsinglestring("SELECT distinct COUNT(emp_code) AS 'Emp_count' FROM  pay_employee_master INNER JOIN pay_unit_master ON   pay_employee_master.comp_code = pay_unit_master.COMP_CODE AND  pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + compcode + "' AND pay_employee_master.client_code = '" + clientcode + "' AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y')  AND  pay_unit_master.branch_Status = 0 AND pay_employee_master.emp_code NOT IN (SELECT DISTINCT pay_attendance_muster.emp_code FROM pay_attendance_muster WHERE pay_attendance_muster.month = '" + txttotdate.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txttotdate.Substring(3) + "' AND pay_attendance_muster.comp_code = '" + compcode + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2))");
            }

            //approve by admin emp
            else if (counter == 2)
            {
                return getsinglestring("SELECT  count(pay_attendance_muster.emp_code) as 'EMP_COUNT' FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.comp_code  = pay_unit_master.comp_code AND   pay_attendance_muster.unit_code =  pay_unit_master.unit_code  WHERE pay_attendance_muster.month = '" + txttotdate.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txttotdate.Substring(3) + "' AND pay_attendance_muster.comp_code = '" + compcode + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2)  AND pay_unit_master.client_code = '" + clientcode + "'");
            }
            //reject
            else if (counter == 3)
            {
                return getsinglestring("SELECT COUNT(pay_attendance_muster.emp_code) AS 'Emp_count' FROM pay_attendance_muster INNER JOIN pay_attendance_reject_master ON pay_attendance_muster.comp_code = pay_attendance_reject_master.comp_code AND pay_attendance_muster.unit_code = pay_attendance_reject_master.unit_code WHERE pay_attendance_muster.month = '" + txttotdate.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txttotdate.Substring(3) + "' AND pay_attendance_muster.comp_code = '" + compcode + "' AND pay_attendance_muster.flag = 0 AND pay_attendance_reject_master.flag = 0 AND  pay_attendance_reject_master.client_code = '" + clientcode + "'");
            }
            //approve by finance
            else if (counter == 4)
            {
                return getsinglestring("SELECT COUNT(pay_attendance_muster.emp_code) AS 'Emp_count' FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.comp_code = pay_unit_master.comp_code AND pay_attendance_muster.unit_code = pay_unit_master.unit_code WHERE pay_attendance_muster.month = '" + txttotdate.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txttotdate.Substring(3) + "' AND pay_attendance_muster.comp_code = '" + compcode + "' AND pay_attendance_muster.flag = 2 AND pay_unit_master.client_code = '" + clientcode + "'");
            }
            else
            {
                return employee_count;
            }
        }

        catch (Exception ex) { throw ex; }
        finally
        {

        }

    }
    public string update_reporting_emp1(string emp_code, int counter, string comp_code)
    {
        int app_level = 0;
        string r_emp_code = "";
        if (counter == 0)
        {
            string level = getsinglestring("select distinct(approval_level) from pay_role_master where role_name in (select role from pay_user_master where login_id = '" + emp_code + "') and comp_code= '" + comp_code + "'  limit 1");
            if (level != "")
            {
                app_level = int.Parse(level);
            }
        }
        else
        {
            app_level = 0;
        }
        while (app_level > 0)
        {
            string temp = getsinglestring("select reporting_to from pay_employee_master where emp_code = '" + emp_code + "' limit 1");
            if (temp != "")
            {
                r_emp_code = r_emp_code + temp;
                emp_code = temp;
            }

            app_level = --app_level;
        }
        return r_emp_code;
    }

    public void update_attendance_left_joined(int month, int year, string emp_code)
    {
        con1.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31 FROM pay_attendance_muster WHERE pay_attendance_muster.month = " + month + " AND pay_attendance_muster.Year = " + year + " and emp_code = '" + emp_code + "'", con1);

            MySqlDataReader drcount1 = cmd.ExecuteReader();

            while (drcount1.Read())
            {
                double pcount = 0;
                double acount = 0;
                double halfdaycount = 0;
                double leavescount = 0;
                double holidaycount = 0;
                double weeklyoffcount = 0;


                for (int j = 0; j <= (CountDay(month, year, 1) - 1); j++)
                {
                    if (drcount1.GetValue(j).ToString() == "P" || drcount1.GetValue(j).ToString() == "PH")
                    {
                        pcount++;
                    }

                    else if (drcount1.GetValue(j).ToString() == "A" || drcount1.GetValue(j).ToString() == "0")
                    {
                        acount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "HD")
                    {
                        halfdaycount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "L")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "W")
                    {
                        weeklyoffcount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "H")
                    {
                        holidaycount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "CL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "PL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "EL")
                    {
                        leavescount++;
                    }
                    else if (drcount1.GetValue(j).ToString() == "SL")
                    {
                        leavescount++;
                    }

                }
                halfdaycount = halfdaycount / 2;
                pcount = halfdaycount + pcount;

                operation("Update pay_attendance_muster set TOT_DAYS_PRESENT =" + pcount + ", TOT_DAYS_ABSENT =" + acount + ", TOT_HALF_DAYS =" + halfdaycount + ",TOT_LEAVES =" + leavescount + ", HOLIDAYS =" + holidaycount + " ,WEEKLY_OFF='" + weeklyoffcount + "' where month = " + month + " and year = " + year + " and emp_code = '" + emp_code + "'");
            }
            con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con1.Close();
        }

    }

    //MD change

    public DataSet select_data(string query)
    {
        conopen1();
        try
        {
            System.Data.DataSet dt = new DataSet();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            dt_item.Dispose();
            conclose1();
            return dt;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose1();
        }
    }

    public string reporting_emp_series(string comp_code, string login_id)
    {
        string reporting_emp_code = "";
        string emp_code = login_id;
        reporting_emp_code = "'" + emp_code + "',";
        try
        {
            while (true)
            {
                if (emp_code != "")
                {
                    string code = getsinglestring("select group_concat(EMP_CODE) from pay_employee_master where comp_code = '" + comp_code + "' and REPORTING_TO in ('" + emp_code + "')");

                    if (code != "")
                    {
                        reporting_emp_code = reporting_emp_code + "'" + code.Replace(",", "','") + "',";
                        emp_code = code.Replace(",", "','");
                    }

                    else
                    {
                        return reporting_emp_code.Substring(0, reporting_emp_code.Length - 1);
                    }
                }
                //return reporting_emp_code.Substring(0, reporting_emp_code.Length - 1);
            }
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }

    public string check_attendance_approve(string comp_code, string client_code, string state_name, string branch_code, string month, string year, int counter, string region)
    {
        try
        {
            // string where1 = "";
            string emp_where1 = "", att_where2 = "" ,date = "";
            string start_date = get_start_date(comp_code, client_code, month + "/" + year);
            string end_date = get_end_date(comp_code, client_code, month + "/" + year);
            string where_state = "";
            if (!state_name.Equals("ALL")) { where_state = " and state='" + state_name + "'"; }
            if (state_name.Equals("Maharashtra") && client_code.Equals("BAGIC") && int.Parse(month+year) > 42020) { where_state = " and state='" + state_name + "' and billingwise_id = 5"; }
            if (getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + client_code + "' " + where_state).Equals("5"))
            {
                where_state = " and zone = '" + region + "' ";
            }
            else
            { where_state = ""; }

            //finance copy
            if (counter == 1)
            {


                // where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "'   AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0";
                emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "' AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') and branch_status = 0 " + where_state;
                att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' )";


                if (state_name != "ALL")
                {
                    // where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'  AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0";
                    emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND pay_employee_master.client_wise_state='" + state_name + "'  AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') and branch_status = 0 " + where_state;
                    att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state_name + "' )";

                }
                if (branch_code != "ALL")
                {
                    if (client_code == "MLL" || client_code == "8")
                    {
                        // where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "' and pay_attendance_muster.unit_code='" + branch_code + "' AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0";
                        emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND pay_employee_master.unit_code='" + branch_code + "'   AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') " + where_state;
                        att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.unit_code = '" + branch_code + "')";

                    }
                }


            }
            //invoice copy
            if (counter == 2)
            {

                //where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "'   AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag != 2";

                emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') and branch_status = 0 " + where_state;
                att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "')";

                if (state_name != "ALL")
                {
                    //  where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'  AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag != 2";
                    emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND pay_employee_master.client_wise_state='" + state_name + "'  AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') and branch_status = 0 " + where_state;
                    att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state_name + "' )";


                }

                if (branch_code != "ALL")
                {
                    if (client_code == "MLL" || client_code == "8")
                    {
                        // where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "' and pay_attendance_muster.unit_code='" + branch_code + "' AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0";
                        //emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND pay_employee_master.client_wise_state='" + state_name + "' and left date is null and pay_employee_master.unit_code='" + branch_code + "'";
                        //att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state_name + "' and pay_unit_master.unit_code='"+branch_code+"')";

                        emp_where1 = "pay_employee_master.comp_code = '" + comp_code + "' AND pay_employee_master.client_code = '" + client_code + "'  AND pay_employee_master.unit_code='" + branch_code + "'   AND (left_date >= STR_TO_DATE('" + start_date + "', '%d/%m/%Y') || left_date IS NULL) AND joining_date <= STR_TO_DATE('" + end_date + "', '%d/%m/%Y') " + where_state;
                        att_where2 = "pay_attendance_muster.month = '" + month + " ' AND pay_attendance_muster.year = '" + year + "' AND pay_attendance_muster.comp_code = '" + comp_code + "' AND (pay_attendance_muster.flag = 1 || pay_attendance_muster.flag = 2) AND pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.unit_code = '" + branch_code + "')";

                    }
                }
            }
            date = "";
            date = "'01/"+month+"/"+year+"'";
            // return getsinglestring("SELECT  group_concat(distinct(pay_unit_master.unit_name)) FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.COMP_CODE = pay_unit_master.COMP_CODE AND pay_attendance_muster.unit_code = pay_unit_master.unit_code WHERE " + where1);
            return getsinglestring("SELECT  group_concat( distinct pay_unit_master.unit_name) FROM  pay_unit_master  INNER JOIN  pay_employee_master  ON  pay_unit_master . unit_code  =  pay_employee_master . unit_code  AND  pay_unit_master . comp_code  =  pay_employee_master . comp_code INNER JOIN pay_user_master ON pay_user_master.LOGIN_ID = pay_unit_master.unit_Code AND pay_user_master.comp_code = pay_unit_master.comp_code WHERE  " + emp_where1 + " AND (STR_TO_DATE("+date+",'%d/%m/%Y') > create_date) AND pay_employee_master. unit_code  NOT IN (SELECT DISTINCT  pay_attendance_muster . unit_code  FROM  pay_attendance_muster   INNER JOIN  pay_unit_master  ON  pay_attendance_muster . unit_code  =  pay_unit_master . unit_Code  AND  pay_attendance_muster . comp_code  =  pay_unit_master . comp_code   WHERE  " + att_where2 + "");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }


    public DataTable approve_attendance_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year, int count, string region)
    {
        try
        {
            DataTable dt = new DataTable();
            string flag = " AND pay_files_timesheet.invoice_flag = 0  and pay_files_timesheet.status is not null   order by pay_files_timesheet . state, pay_unit_master.unit_name";
            string where1 = "pay_files_timesheet.comp_code = '" + comp_code + "' AND pay_files_timesheet.client_code = '" + client_code + "' AND pay_files_timesheet.state = '" + state_name + "' and pay_files_timesheet.unit_code='" + branch_code + "' AND   pay_files_timesheet.month = '" + month + "' AND pay_files_timesheet.year = '" + year + "' ";

            if (state_name == "ALL")
            {
                where1 = "pay_files_timesheet.comp_code = '" + comp_code + "' AND pay_files_timesheet.client_code = '" + client_code + "'   AND   pay_files_timesheet.month = '" + month + "' AND pay_files_timesheet.year = '" + year + "' ";
            }
            else if (branch_code == "ALL")
            {
                where1 = "pay_files_timesheet.comp_code = '" + comp_code + "' AND pay_files_timesheet.client_code = '" + client_code + "' AND pay_files_timesheet.state = '" + state_name + "'  AND   pay_files_timesheet.month = '" + month + "' AND pay_files_timesheet.year = '" + year + "' ";
            }
            if (count == 2)
            {
                where1 = " pay_files_timesheet.flag !=2 and " + where1;
            }

            if (!region.Equals("") && !region.Equals("ALL") && !region.Equals("Select"))
            {
                where1 = " pay_unit_master.zone = '" + region + "' and " + where1;
            }

            conopen();
            //comment for visible gv after final invoice button
            // MySqlCommand cmd = new MySqlCommand("SELECT  client_name  AS 'client_name',  pay_files_timesheet . state  as 'state_name',  unit_name  as 'branch_name', concat( month ,'" + "/" + "',year) as 'month_year',status,   file_name  as 'Attendance_file', pay_files_timesheet.unit_code as 'unit_code',status as 'rejected_reason' FROM  pay_files_timesheet  INNER JOIN  pay_client_master  ON  pay_files_timesheet . client_code  =  pay_client_master . client_code  INNER JOIN  pay_unit_master  ON  pay_unit_master . COMP_CODE  =  pay_files_timesheet . COMP_CODE  AND  pay_unit_master . unit_code  =  pay_files_timesheet . unit_code  WHERE " + where1 + " " + flag, con);
            MySqlCommand cmd = new MySqlCommand("SELECT  client_name  AS 'client_name',  pay_files_timesheet . state  as 'state_name',  unit_name  as 'branch_name', cast(concat( month ,'/',year) as char) as 'month_year',status,   file_name  as 'Attendance_file', pay_files_timesheet.unit_code as 'unit_code',CASE WHEN reject_status = '0' THEN '' ELSE reject_status END AS 'rejected_reason' FROM  pay_files_timesheet  INNER JOIN  pay_client_master  ON  pay_files_timesheet . client_code  =  pay_client_master . client_code  INNER JOIN  pay_unit_master  ON  pay_unit_master . COMP_CODE  =  pay_files_timesheet . COMP_CODE  AND  pay_unit_master . unit_code  =  pay_files_timesheet . unit_code  WHERE " + where1 + " " + flag, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            return dt;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }
    public DataTable approve_material_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year)
    {
        try
        {
          
                DataTable dt = new DataTable();
                string flag = "     AND (pay_material_details.material_flag = 1 ||pay_material_details.material_flag = 2 || pay_material_details.material_flag = 3)  AND pay_billing_material_history.invoice_flag = 0 AND pay_billing_material_history.contract_type = 4 AND pay_material_details.material_status IS NOT NULL group by unit_code ORDER BY pay_material_details.state_name, pay_billing_material_history.unit_name";
                string where1 = "pay_material_details.comp_code = '" + comp_code + "' AND pay_material_details.client_code = '" + client_code + "' AND pay_material_details.state_name = '" + state_name + "' and pay_material_details.unit_code='" + branch_code + "' AND   pay_material_details.month = '" + month + "' AND pay_material_details.year = '" + year + "' ";

                if (state_name == "ALL")
                {
                    where1 = "pay_material_details.comp_code = '" + comp_code + "' AND pay_material_details.client_code = '" + client_code + "'   AND   pay_material_details.month = '" + month + "' AND pay_material_details.year = '" + year + "' ";
                }
                else if (branch_code == "ALL")
                {
                    where1 = "pay_material_details.comp_code = '" + comp_code + "' AND pay_material_details.client_code = '" + client_code + "' AND pay_material_details.state_name = '" + state_name + "'  AND   pay_material_details.month = '" + month + "' AND pay_material_details.year = '" + year + "' ";
                }
               

                conopen();

                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_client_master.client_name, pay_material_details.state_name, pay_billing_material_history.unit_name AS 'branch_name', CAST(CONCAT(pay_billing_material_history.month, '/', pay_billing_material_history.year) AS char) AS 'month_year',material_status, material_upload AS 'Attendance_file',pay_material_details.unit_code,rejected_reason as 'material_reject_reason' FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_unit_master.COMP_CODE = pay_material_details.COMP_CODE AND pay_unit_master.unit_code = pay_material_details.unit_code INNER JOIN pay_billing_material_history ON pay_billing_material_history.COMP_CODE = pay_material_details.COMP_CODE AND pay_billing_material_history.unit_code = pay_material_details.unit_code AND pay_billing_material_history.month = pay_material_details.month AND pay_billing_material_history.year = pay_material_details.year  WHERE " + where1 + " " + flag, con);
                MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
                dt_item.Fill(dt);
                return dt;
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }
    public DataTable approve_r_m_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year)
    {
        try
        {

            DataTable dt = new DataTable();
            string flag = "     AND (pay_r_and_m_service.approve_flag = 1 ||pay_r_and_m_service.approve_flag = 2 || pay_r_and_m_service.approve_flag = 3)  AND pay_billing_r_m.invoice_flag = 0  AND pay_r_and_m_service.r_m_status IS NOT NULL group by unit_code ORDER BY pay_r_and_m_service.state_name, pay_billing_r_m.unit_name";
            string where1 = "pay_r_and_m_service.comp_code = '" + comp_code + "' AND pay_r_and_m_service.client_code = '" + client_code + "' AND pay_r_and_m_service.state_name = '" + state_name + "' and pay_r_and_m_service.unit_code='" + branch_code + "' AND   pay_r_and_m_service.month = '" + month + "' AND pay_r_and_m_service.year = '" + year + "' ";

            if (state_name == "ALL")
            {
                where1 = "pay_r_and_m_service.comp_code = '" + comp_code + "' AND pay_r_and_m_service.client_code = '" + client_code + "'   AND   pay_r_and_m_service.month = '" + month + "' AND pay_r_and_m_service.year = '" + year + "' ";
            }
            else if (branch_code == "ALL")
            {
                where1 = "pay_r_and_m_service.comp_code = '" + comp_code + "' AND pay_r_and_m_service.client_code = '" + client_code + "' AND pay_r_and_m_service.state_name = '" + state_name + "'  AND   pay_r_and_m_service.month = '" + month + "' AND pay_r_and_m_service.year = '" + year + "' ";
            }
            conopen();
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_client_master.client_name, pay_r_and_m_service.state_name, pay_billing_r_m.unit_name AS 'branch_name', CAST(CONCAT(pay_billing_r_m.month, '/', pay_billing_r_m.year) AS char) AS 'month_year',r_m_status, image_name AS 'Attendance_file',pay_r_and_m_service.unit_code,reject_reason,r_m_status FROM pay_r_and_m_service INNER JOIN pay_client_master ON pay_r_and_m_service.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_unit_master.COMP_CODE = pay_r_and_m_service.COMP_CODE AND pay_unit_master.unit_code = pay_r_and_m_service.unit_code INNER JOIN pay_billing_r_m ON pay_billing_r_m.COMP_CODE = pay_r_and_m_service.COMP_CODE AND pay_billing_r_m.unit_code = pay_r_and_m_service.unit_code AND pay_billing_r_m.month = pay_r_and_m_service.month AND pay_billing_r_m.year = pay_r_and_m_service.year  WHERE " + where1 + " " + flag, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            return dt;

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }
    public DataTable approve_administrative_exp_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year)
    {
        try
        {

            DataTable dt = new DataTable();
            string flag = "     AND (pay_administrative_expense.approve_flag = 1 ||pay_administrative_expense.approve_flag = 2 || pay_administrative_expense.approve_flag = 3)  AND pay_billing_admini_expense.invoice_flag = 0  AND pay_administrative_expense.admini_status IS NOT NULL group by unit_code ORDER BY pay_administrative_expense.state_name, pay_billing_admini_expense.unit_name";
            string where1 = "pay_administrative_expense.comp_code = '" + comp_code + "' AND pay_administrative_expense.client_code = '" + client_code + "' AND pay_administrative_expense.state_name = '" + state_name + "' and pay_administrative_expense.unit_code='" + branch_code + "' AND   pay_administrative_expense.month = '" + month + "'  pay_administrative_expense.year = '" + year + "' ";

            if (state_name == "ALL")
            {
                where1 = "pay_administrative_expense.comp_code = '" + comp_code + "' AND pay_administrative_expense.client_code = '" + client_code + "'   AND   pay_administrative_expense.month = '" + month + "' AND pay_administrative_expense.year = '" + year + "' ";
            }
            else if (branch_code == "ALL")
            {
                where1 = "pay_administrative_expense.comp_code = '" + comp_code + "' AND pay_administrative_expense.client_code = '" + client_code + "' AND pay_administrative_expense.state_name = '" + state_name + "'  AND   pay_administrative_expense.month = '" + month + "' AND pay_administrative_expense.year = '" + year + "' ";
            }
            conopen();
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_client_master.client_name, pay_administrative_expense.state_name, pay_billing_admini_expense.unit_name AS 'branch_name', CAST(CONCAT(pay_billing_admini_expense.month, '/', pay_billing_admini_expense.year) AS char) AS 'month_year',admini_status, image_name AS 'Attendance_file',pay_administrative_expense.unit_code,reject_reason,admini_status FROM pay_administrative_expense INNER JOIN pay_client_master ON pay_administrative_expense.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_unit_master.COMP_CODE = pay_administrative_expense.COMP_CODE AND pay_unit_master.unit_code = pay_administrative_expense.unit_code INNER JOIN pay_billing_admini_expense ON pay_billing_admini_expense.COMP_CODE = pay_administrative_expense.COMP_CODE AND pay_billing_admini_expense.unit_code = pay_administrative_expense.unit_code AND pay_billing_admini_expense.month = pay_administrative_expense.month AND pay_billing_admini_expense.year = pay_administrative_expense.year  WHERE " + where1 + " " + flag, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            return dt;

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }

    public DataTable approve_gv_con_emp_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year,string zone)
    {
        try
        {
            string region = "";
            if (zone != "" && (zone != "ALL" && zone != "Select")) { region = " AND `pay_unit_master`.`zone`= '" + zone + "'"; }
            DataTable dt = new DataTable();
            string flag = " AND (pay_conveyance_amount_history.conveyance_flag = 1 ||pay_conveyance_amount_history.conveyance_flag = 2 || pay_conveyance_amount_history.conveyance_flag = 3)  AND pay_billing_material_history.invoice_flag = 0 and  conveyance='emp_conveyance' AND pay_conveyance_amount_history.conveyance_rate != 0 AND pay_conveyance_amount_history.con_emp_status IS NOT NULL group by unit_code ORDER BY pay_conveyance_amount_history.state, pay_unit_master.unit_name";
            string where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' " + region + " AND pay_conveyance_amount_history.state = '" + state_name + "' and pay_conveyance_amount_history.unit_code='" + branch_code + "' AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";

            if (state_name == "ALL")
            {
                where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' " + region + "  AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";
            }
            else if (branch_code == "ALL")
            {
                where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' " + region + " AND pay_conveyance_amount_history.state = '" + state_name + "'  AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";
            }


            conopen();

            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_client_master.client_name, pay_conveyance_amount_history.state, pay_billing_material_history.unit_name AS 'branch_name', CAST(CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS char) AS 'month_year', con_emp_status, conveyance_images AS 'Attendance_file', pay_conveyance_amount_history.unit_code, con_emp_rejected_reason AS 'con_emp_reject_reason' FROM pay_conveyance_amount_history INNER JOIN pay_client_master ON pay_conveyance_amount_history.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_unit_master.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_unit_master.unit_code = pay_conveyance_amount_history.unit_code INNER JOIN pay_billing_material_history ON pay_billing_material_history.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_billing_material_history.unit_code = pay_conveyance_amount_history.unit_code AND pay_billing_material_history.month = pay_conveyance_amount_history.month AND pay_billing_material_history.year = pay_conveyance_amount_history.year INNER JOIN pay_conveyance_upload ON pay_conveyance_upload.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_conveyance_upload.unit_code = pay_conveyance_amount_history.unit_code AND pay_conveyance_upload.month = pay_conveyance_amount_history.month AND pay_conveyance_upload.year = pay_conveyance_amount_history.year WHERE " + where1 + " " + flag, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            return dt;

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }

    public DataTable approve_gv_driver_emp_download(string comp_code, string client_code, string state_name, string branch_code, string month, string year, string zone)
    {
        try
        {
            string region = "";
            if (zone != "" && (zone != "ALL" || zone != "Select")) { region = " AND `pay_unit_master`.`zone`= '" + zone + "'"; }
           
            DataTable dt = new DataTable();
            string flag = " AND (pay_conveyance_amount_history.driver_conv_flag = 1 ||pay_conveyance_amount_history.driver_conv_flag = 2 || pay_conveyance_amount_history.driver_conv_flag = 3)  AND pay_billing_material_history.invoice_flag = 0 AND pay_conveyance_amount_history.conveyance_rate = 0 and  conveyance='driver_conveyance' AND pay_conveyance_amount_history.con_driver_status IS NOT NULL group by pay_conveyance_amount_history.emp_code ORDER BY pay_conveyance_amount_history.state, pay_unit_master.unit_name";
            string where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' " + region + " AND pay_conveyance_amount_history.state = '" + state_name + "' and pay_conveyance_amount_history.unit_code='" + branch_code + "' AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";

            if (state_name == "ALL")
            {
                where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "'  " + region + " AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";
            }
            else if (branch_code == "ALL")
            {
                where1 = "pay_conveyance_amount_history.comp_code = '" + comp_code + "' AND pay_conveyance_amount_history.client_code = '" + client_code + "' " + region + " AND pay_conveyance_amount_history.state = '" + state_name + "'  AND   pay_conveyance_amount_history.month = '" + month + "' AND pay_conveyance_amount_history.year = '" + year + "' ";
            }


            conopen();

            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_client_master.client_name, pay_conveyance_amount_history.state, pay_billing_material_history.unit_name AS 'branch_name',pay_conveyance_amount_history.emp_name, CAST(CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS char) AS 'month_year', con_driver_status, conveyance_images AS 'Attendance_file', pay_conveyance_amount_history.unit_code, con_driver_rejected_reason,pay_conveyance_amount_history.emp_code FROM pay_conveyance_amount_history INNER JOIN pay_client_master ON pay_conveyance_amount_history.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_unit_master.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_unit_master.unit_code = pay_conveyance_amount_history.unit_code INNER JOIN pay_billing_material_history ON pay_billing_material_history.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_billing_material_history.unit_code = pay_conveyance_amount_history.unit_code AND pay_billing_material_history.month = pay_conveyance_amount_history.month AND pay_billing_material_history.year = pay_conveyance_amount_history.year INNER JOIN pay_conveyance_upload ON pay_conveyance_upload.COMP_CODE = pay_conveyance_amount_history.COMP_CODE AND pay_conveyance_upload.unit_code = pay_conveyance_amount_history.unit_code AND pay_conveyance_upload.month = pay_conveyance_amount_history.month AND pay_conveyance_upload.year = pay_conveyance_amount_history.year WHERE " + where1 + " " + flag, con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            return dt;

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }

    public string check_branch_deployment(string comp_code, string client_code, string state_name, string branch_code, string month, string year, int counter,string region)
    {
        try
        {
            string where1 = "", where2 = "", where3 = "";
            string start_date = get_start_date(comp_code, client_code, month + "/" + year);
            string end_date = get_end_date(comp_code, client_code, month + "/" + year);
            string where_state = "";
            if (getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + client_code + "' ").Equals("5"))
            {
                where_state = " and pay_unit_master.zone = '" + region + "' ";
            }
            else
            { where_state = ""; }

            //if (counter == 1)
            //{

            //    where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "' and pay_attendance_muster.unit_code='" + branch_code + "' AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0";

            //    if (state_name == "ALL")
            //    {
            //        where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "'   AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0 and branch_status = 0";
            //    }
            //    else if (branch_code == "ALL")
            //    {
            //        where1 = "pay_unit_master.comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'  AND   month = '" + month + "' AND year = '" + year + "' AND pay_attendance_muster.flag = 0 and branch_status = 0";
            //    }
            //}
            if (counter == 2)
            {

                where1 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND pay_unit_master.deployment_flag = 0 ";
                where2 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND (left_date >=str_to_date('" + start_date + "','%d/ %m/%Y') || left_date is null) AND joining_date <= str_to_date('" + end_date + "','%d/%m/%Y')";
                where3 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' and (unit_month !='" + month + "' || unit_year!='" + year + "')";

                if (state_name != "ALL")
                {
                    where1 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' and state_name='" + state_name + "' AND pay_unit_master.deployment_flag = 0 ";
                    where2 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' AND client_wise_state='" + state_name + "'  AND (left_date >=str_to_date('" + start_date + "','%d/ %m/%Y') || left_date is null) AND joining_date <= str_to_date('" + end_date + "','%d/%m/%Y')";
                    where3 = "comp_code = '" + comp_code + "' AND client_code = '" + client_code + "' and state_name='" + state_name + "' and (unit_month !='" + month + "' || unit_year!='" + year + "')";
                }
            }

            operation("update pay_unit_master set unit_month='" + month + "' , unit_year ='" + year + "' , deployment_flag  = 0 where " + where3 + (where_state==""?"":" and zone='"+region+"'"));
            return getsinglestring("SELECT group_concat( distinct  UNIT_NAME ) as unit_name FROM  pay_unit_master  WHERE  " + where1 + (where_state==""?"":" and zone='"+region+"' ") + " AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + month + "/"+year+"', '%d/%m/%Y')) AND  unit_code  NOT IN (SELECT DISTINCT  unit_code  FROM  pay_employee_master  WHERE " + where2 + ")");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }

    //MD change
    public string get_start_date(string comp_code, string client_code, string date)
    {
        string start_date = getsinglestring("select start_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "'");
        int start_month = int.Parse(date.Substring(0, 2));
        int year = int.Parse(date.Substring(3));
        if (start_date == "1")
        {
            return start_date + "/" + start_month + "/" + year;
        }

        else
        {
            if (start_month != 1 || start_month != 01) { start_month = start_month - 1; }
            else { start_month = 12; year = year - 1; }

            return start_date + "/" + start_month + "/" + year;
        }

    }

    public string get_end_date(string comp_code, string client_code, string date)
    {
        string end_date = getsinglestring("select end_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE  pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "'");

        int end_month = int.Parse(date.Substring(0, 2));

        int year = int.Parse(date.Substring(3));

        string start_date = getsinglestring("select start_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "'");

        if (start_date == "1") { end_date = DateTime.DaysInMonth(int.Parse(date.Substring(0, 2)), int.Parse(date.Substring(0, 2))).ToString(); }

        return end_date + "/" + end_month + "/" + year;
    }

    public DataTable chk_legal_document(string compcode, string client_code, string state_name, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string where = "AND pay_unit_master.client_code = '" + client_code + "'";

        if (state_name != "Select")
        {
            where = where + "AND pay_unit_master.state_name = '" + state_name + "'";
        }
        //remaning employee approve by admin
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + " and employee_type='Permanent' and left_date is null  and (legal_flag = 0 || legal_flag = 3) order by client_wise_state,unit_name,emp_name", con);
        }
        // employee approve by admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and legal_flag = 1 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee approve by legal
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and legal_flag = 2 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee reject by legal
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,reject_reason  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and legal_flag = 3 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee bank approve
        else if (counter == 4)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and bank_approve = 1 order by client_wise_state,unit_name,emp_name", con);
        }
        // remaining employee bank approve
        else if (counter == 5)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and bank_approve = 2 order by client_wise_state,unit_name,emp_name", con);
        }
        //// vakant branch
        //else if (counter == 4)
        //{
        //    //cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' AND pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.state_name = '" + state_name + "' and left_date is null  and legal_flag = 4 order by client_wise_state,unit_name,emp_name", con);
        //  cmd_item = new MySqlDataAdapter("select (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_unit_master.client_code) as 'Client_Name',state_name, unit_name as 'branch_name' from pay_unit_master WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and  (IFNULL((Emp_count -  (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE pay_unit_master.branch_status = '0' AND unit_code = pay_unit_master.unit_code AND comp_code = '" + compcode + "'  AND Employee_type != 'Reliever' " + where + " AND pay_employee_master.LEFT_DATE IS NULL)), 0)) !='0'", con);
        //}


        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }
    public DataTable chk_bank_document(string compcode, string client_code, string state_name, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        string where = "AND pay_unit_master.client_code = '" + client_code + "'";

        if (state_name != "Select")
        {
            where = where + "AND pay_unit_master.state_name = '" + state_name + "'";
        }
        //remaning employee approve by admin
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + " and employee_type='Permanent' and left_date is null  and (legal_flag = 0 || legal_flag = 3) and bank_approve = 2 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee approve by admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and legal_flag = 1 and bank_approve != 1 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee bank approve
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and bank_approve = 1 order by client_wise_state,unit_name,emp_name", con);
        }
        // reject employee bank approve
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_employee_master.client_code) as 'Client_Name',client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,bankdetail_reject_reason  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and left_date is null  and bank_approve = 2 order by client_wise_state,unit_name,emp_name", con);
        }
        //// vakant branch
        //else if (counter == 4)
        //{
        //    //cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' AND pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.state_name = '" + state_name + "' and left_date is null  and legal_flag = 4 order by client_wise_state,unit_name,emp_name", con);
        //  cmd_item = new MySqlDataAdapter("select (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_unit_master.client_code) as 'Client_Name',state_name, unit_name as 'branch_name' from pay_unit_master WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and  (IFNULL((Emp_count -  (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE pay_unit_master.branch_status = '0' AND unit_code = pay_unit_master.unit_code AND comp_code = '" + compcode + "'  AND Employee_type != 'Reliever' " + where + " AND pay_employee_master.LEFT_DATE IS NULL)), 0)) !='0'", con);
        //}


        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;
    }


    public string chk_payment_approve(string comp_code, string client_code, string state_name, string branch_code, string month, string year, int table, string con_type)
    {
        string where = "", payment_flag = "and ( payment_approve = 1 || payment_approve = 2) ";
        string table_name = null;

        table_name = table == 1 ? " pay_pro_master " : table == 2 ? " pay_pro_material_history " : "";
        try
        {
            where = " month ='" + month + "' AND  year = '" + year + "' AND  client_code = '" + client_code + "'  AND  unit_code = '" + branch_code + "' and  comp_code= '" + comp_code + "' " + payment_flag;

            if (state_name == "ALL")
            {
                where = " month ='" + month + "' AND  year = '" + year + "' AND  client_code = '" + client_code + "'  and  comp_code= '" + comp_code + "' " + payment_flag;

            }
            else if (branch_code == "ALL")
            {
                where = " month ='" + month + "' AND  year = '" + year + "' AND  client_code = '" + client_code + "'  and  state_name = '" + state_name + "' and  comp_code= '" + comp_code + "' " + payment_flag;

            }

            if (con_type == "1")
            {
                where = where + " and conveyance_type != 100";
            }
            else if (con_type == "2")
            {
                where = where + " and conveyance_type =100";
            }
            return getsinglestring("select group_concat(distinct(unit_name)) as 'unit_name' from " + table_name + " where  " + where);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            conclose();
        }
    }


    //return current month year
    public string getCurrentMonthYear()
    {

        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();

        return int.Parse(month) <= 9 ? "0" + month + "/" + year : month + "/" + year;

    }
    //vikas 30/03/2019
    public List<string> iteam_size(string u_size)
    {
        List<string> uniformsize = new List<string>();
        if (u_size == "Uniform")
        {
            uniformsize.Add("24");
            uniformsize.Add("26");
            uniformsize.Add("28");
            uniformsize.Add("30");
            uniformsize.Add("32");
            uniformsize.Add("34");
            uniformsize.Add("36");
            uniformsize.Add("38");
            uniformsize.Add("40");
            uniformsize.Add("42");
            uniformsize.Add("44");
            uniformsize.Add("46");
        }
        else if (u_size == "pantry_jacket")
        {
            uniformsize.Add("24");
            uniformsize.Add("26");
            uniformsize.Add("28");
            uniformsize.Add("30");
            uniformsize.Add("34");
            uniformsize.Add("36");
            uniformsize.Add("38");
            uniformsize.Add("40");
            uniformsize.Add("42");
            uniformsize.Add("44");
            uniformsize.Add("46");
        }
        else if (u_size == "Shoes")
        {
            uniformsize.Add("5");
            uniformsize.Add("6");
            uniformsize.Add("7");
            uniformsize.Add("8");
            uniformsize.Add("9");
            uniformsize.Add("10");
            uniformsize.Add("11");

        }
        else if (u_size == "Apron")
        {
            uniformsize.Add("SMALL");
            uniformsize.Add("MEDIUM");
            uniformsize.Add("LARGE");
            uniformsize.Add("XL");
            uniformsize.Add("XXL");



        }
        return uniformsize;
    }
    public DataTable chk_legal_document_all(string compcode, int counter)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;

        //remaning employee approve by admin
        if (counter == 0)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "'  and left_date is null  and (legal_flag = 0 || legal_flag = 3) order by client_wise_state,unit_name,emp_name", con);
        }
        // employee approve by admin
        else if (counter == 1)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "'  and left_date is null  and legal_flag = 1 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee approve by legal
        else if (counter == 2)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' and left_date is null  and legal_flag = 2 order by client_wise_state,unit_name,emp_name", con);
        }
        // employee reject by legal
        else if (counter == 3)
        {
            cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,reject_reason  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "'  and left_date is null  and legal_flag = 3 order by client_wise_state,unit_name,emp_name", con);
        }
        // vakant branch
        else if (counter == 4)
        {
            //cmd_item = new MySqlDataAdapter("SELECT client_wise_state as 'state_name', unit_name as 'branch_name', emp_name,  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + compcode + "' AND pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.state_name = '" + state_name + "' and left_date is null  and legal_flag = 4 order by client_wise_state,unit_name,emp_name", con);
            cmd_item = new MySqlDataAdapter("select state_name, unit_name as 'branch_name', '' as emp_name, '' as reject_reason from pay_unit_master WHERE pay_unit_master.comp_code = '" + compcode + "'  and  (IFNULL((Emp_count -  (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE pay_unit_master.branch_status = '0' AND unit_code = pay_unit_master.unit_code AND comp_code = '" + compcode + "'  AND Employee_type != 'Reliever'  AND pay_employee_master.LEFT_DATE IS NULL)), 0)) !='0' and pay_unit_master.branch_status != '1'", con);
        }


        con.Open();
        try
        {
            cmd_item.SelectCommand.CommandTimeout = 0;
            cmd_item.Fill(dt_item);
            con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            con.Close();
        }
        return dt_item;

    }


    /// <summary>
    /// This function can return same as group_concat data 
    /// like 1,2,3,4,5,6
    /// </summary>
    /// <param name="query"></param>
    /// <returns> like 1,2,3,4,5,6</returns>

    public string get_group_concat(string query)
    {
        try
        {
            string group_data = "";
            DataTable dt = new DataTable();
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dt_item = cmd.ExecuteReader();

            while (dt_item.Read())
            {

                group_data = group_data + dt_item.GetValue(0).ToString() + ",";


            }
            con.Close();
            dt_item.Dispose();
            return group_data == "" ? "" : group_data.Substring(0, group_data.Length - 1).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

//vikas add for arrears
    public string get_arrears_calendar_days(int starting_day, string month_year, int counter, int tb_select, int start_date, int end_date, string from_date, string end_month, string s_date)
    {


      //  string days = "", days1 = "", days2 = "", days3 = "", days4 = "", sunday = "", employee_type = "";
        string days4 = "", sunday = "", days1 = "";
        int k = 1;
        string count = k.ToString();
        int month = int.Parse(month_year.Substring(0, 2));
        int month_A = int.Parse(month_year.Substring(0, 2));
        int year = int.Parse(month_year.Substring(3));

       

        if (starting_day != 1)
        {
            int monthdays = 0;
            if (starting_day == tb_select || starting_day < tb_select)
            {

                if (month == 0) { month = 12; --year; }
                monthdays = DateTime.DaysInMonth(year, month);//5
            }
            
            else
            {
                month = int.Parse(month_year.Substring(0, 2)) - 1;
                if (month == 0) { month = 12; --year; }
                monthdays = DateTime.DaysInMonth(year, month);
            }
            
            for (int i = 0; monthdays >= (i + starting_day); i++)
            {
               
                
                if (i + starting_day < 10)
                {
                    if (month_A < counter)
                    {
                        if ((i + starting_day) < tb_select)
                        {
                            days4 = days4 + "CASE WHEN t1.DAY0" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY0" + (i + starting_day) + " END AS 'DAY0" + (i + starting_day) + "',";
                            days1=days1+ " DAY0" + (i + starting_day) + ",";
                        }
                        else
                        {
                            days4 = days4 + " t1.DAY0" + (i + starting_day) + ",";
                            days1 = days1 + " DAY0" + (i + starting_day) + ",";
                        }
                    
                    
                    }
                    else{
                    if (starting_day == tb_select)
                    {
                        if (month_A == int.Parse(end_month))
                        {
                            
                        }else
                        {
                            days4 = days4 + " t1.DAY0" + (i + starting_day) + ",";
                            days1 = days1 + " DAY0" + (i + starting_day) + ",";
                        }
                    }
                    else
                    {
                        
                            if ((i + starting_day) > tb_select)
                            {
                                days4 = days4 + "CASE WHEN t1.DAY0" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY0" + (i + starting_day) + " END AS 'DAY0" + (i + starting_day) + "',";
                                days1 = days1 + " DAY0" + (i + starting_day) + ",";
                            }
                            else if ((i + starting_day) < tb_select)
                            {
                                days4 = days4 + "CASE WHEN t1.DAY0" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY0" + (i + starting_day) + " END AS 'DAY0" + (i + starting_day) + "',";
                                days1 = days1 + " DAY0" + (i + starting_day) + ",";
                            }
                            else
                            {
                                days4 = days4 + " t1.DAY0" + (i + starting_day) + ",";
                                days1 = days1 + " DAY0" + (i + starting_day) + ",";
                            }
                       
                    }
                }
                }
                else
                {
                    if (new DateTime(year, month, i + starting_day).DayOfWeek == DayOfWeek.Sunday)
                    { sunday = "'W'"; }
                    else { sunday = "'P'"; }
                    if (month_A < counter)
                    {
                        if ((i + starting_day) < tb_select)
                        {
                            days4 = days4 + "CASE WHEN t1.DAY" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY" + (i + starting_day) + " END AS 'DAY" + (i + starting_day) + "',";
                            days1 = days1 + " DAY" + (i + starting_day) + ",";
                        }
                        else
                        {
                            days4 = days4 + " t1.DAY" + (i + starting_day) + ",";
                            days1 = days1 + " DAY" + (i + starting_day) + ",";
                        }
                    }
                    else
                    {
                        if (starting_day == tb_select)
                        {
                            days4 = days4 + " t1.DAY" + (i + starting_day) + ",";
                            days1 = days1 + " DAY" + (i + starting_day) + ",";
                        }

                        else
                        {

                            if ((i + starting_day) > tb_select)
                            {
                                days4 = days4 + "CASE WHEN t1.DAY" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY" + (i + starting_day) + " END AS 'DAY" + (i + starting_day) + "',";
                                days1 = days1 + " DAY" + (i + starting_day) + ",";
                            }
                            else if ((i + starting_day) < tb_select)
                            {
                                days4 = days4 + "CASE WHEN t1.DAY" + (i + starting_day) + " = 'p' THEN 'A' ELSE t1.DAY" + (i + starting_day) + " END AS 'DAY" + (i + starting_day) + "',";
                                days1 = days1 + " DAY" + (i + starting_day) + ",";
                            }
                            else
                            {
                                days4 = days4 + " t1.DAY" + (i + starting_day) + ",";
                                days1 = days1 + " DAY" + (i + starting_day) + ",";
                            }

                        }
                    }
                }
                ++k;
            }
        }
        else
        {

            starting_day = DateTime.DaysInMonth(year, month) + 1;

        }
        for (int i = 1; (starting_day - 1) >= i; i++)
        {
           
            if (i < 10)
            {
                if (start_date != 1)
                {
                    if (month_A < counter)
                    {
                        if ((i) > tb_select)
                        {
                            days4 = days4 + "CASE WHEN t2.DAY0" + (i) + " = 'p' THEN 'A' ELSE t2.DAY0" + (i) + " END AS 'DAY0" + (i) + "',";
                            days1 = days1 + " DAY0" + (i ) + ",";
                        }
                        else
                        {
                            days4 = days4 + " t2.DAY0" + (i) + ",";
                            days1 = days1 + " DAY0" + (i) + ",";
                        }
                    }
                    else
                    {
                        if (starting_day == tb_select)
                        {
                            days4 = days4 + " t2.DAY0" + (i) + ",";
                            days1 = days1 + " DAY0" + (i) + ",";
                        }
                        else
                        {

                            if ((i) < tb_select)
                            {
                                days4 = days4 + "CASE WHEN t2.DAY0" + (i) + " = 'p' THEN 'A' ELSE t2.DAY0" + (i) + " END AS 'DAY0" + (i) + "',";
                                days1 = days1 + " DAY0" + (i) + ",";
                            }
                            else if ((i) > tb_select)
                            {
                                days4 = days4 + "CASE WHEN t2.DAY0" + (i) + " = 'p' THEN 'A' ELSE t2.DAY0" + (i) + " END AS 'DAY0" + (i) + "',";
                                days1 = days1 + " DAY0" + (i) + ",";
                            }
                            else
                            {
                                days4 = days4 + " t2.DAY0" + (i) + ",";
                                days1 = days1 + " DAY0" + (i) + ",";
                            }

                        }
                    }
                }
                else
                {
                    if (1 == tb_select)
                    {
                        days4 = days4 + " t2.DAY0" + (i) + ",";
                        days1 = days1 + " DAY0" + (i) + ",";
                    }
                    else
                    {
                        if ((i) < tb_select)
                        {
                            days4 = days4 + "CASE WHEN t2.DAY0" + (i) + " = 'p' THEN 'A' ELSE t2.DAY0" + (i) + " END AS 'DAY0" + (i) + "',";
                            days1 = days1 + " DAY0" + (i) + ",";
                        }
                        else
                        {
                            days4 = days4 + " t2.DAY0" + (i) + ",";
                            days1 = days1 + " DAY0" + (i) + ",";
                        }
                    }
                }

            }
            else
            {
                if (start_date != 1)
                {
                    if (month_A < counter)
                    {
                        if ((i) > tb_select)
                        {
                            days4 = days4 + "CASE WHEN t2.DAY" + (i) + " = 'p' THEN 'A' ELSE t2.DAY" + (i) + " END AS 'DAY" + (i) + "',";
                            days1 = days1 + " DAY" + (i) + ",";
                        }
                        else
                        {
                            days4 = days4 + " t2.DAY" + (i) + ",";
                            days1 = days1 + " DAY" + (i) + ",";
                        }
                    }
                    else
                    {
                        if (starting_day == tb_select)
                        {
                            days4 = days4 + " t2.DAY" + (i) + ",";
                            days1 = days1 + " DAY" + (i) + ",";
                        }
                        else
                        {

                            if ((i) < tb_select)
                            {
                                days4 = days4 + "CASE WHEN t2.DAY" + (i) + " = 'p' THEN 'A' ELSE t2.DAY" + (i) + " END AS 'DAY" + (i) + "',";
                                days1 = days1 + " DAY" + (i) + ",";
                            }
                            else
                            {
                                days4 = days4 + " t2.DAY" + (i) + ",";
                                days1 = days1 + " DAY" + (i) + ",";
                            }

                        }
                    }
                }
                else
                {
                    if ((i) < tb_select)
                    {
                        days4 = days4 + "CASE WHEN t2.DAY0" + (i) + " = 'p' THEN 'A' ELSE t2.DAY0" + (i) + " END AS 'DAY0" + (i) + "',";
                        days1 = days1 + " DAY0" + (i) + ",";
                    }
                    else
                    {
                        days4 = days4 + " t2.DAY0" + (i) + ",";
                        days1 = days1 + " DAY0" + (i) + ",";
                    }
                }
                
            }
            ++k;
        }

        if (from_date == "1")
        {
            return days4;
        }
        else if (from_date == "2")
        {
            return days1;
        }
        else
        {
            return days1;
        }
       }

public string get_calendar_days1(int starting_day, string month_year, int counter, int tb_select,int last_day, int last_month,int end_cycle)
    {


        string days = "", days1 = "", days2 = "", days3 = "", days4 = "";
        int k = 1;
        int ss = 1;
        int starting_day1 = 0;
        int Dcount = 0;
        int month = int.Parse(month_year.Substring(0, 2));
        int year = int.Parse(month_year.Substring(3));
        int arrear_starting_day = starting_day;
       
        if (starting_day != 1)
        {
            int monthdays = 0;
            
                month = int.Parse(month_year.Substring(0, 2)) - 1;
                if (month == 0) { month = 12; --year; }
                monthdays = DateTime.DaysInMonth(year, month);

               
            if (last_month==int.Parse(month_year.Substring(0,2)))
            {
                if (last_day < tb_select)
                {
                    monthdays = last_day;
                }
                else
                {
                    monthdays = last_day;
                }
                if (last_day == end_cycle)
                {
                    month = int.Parse(month_year.Substring(0, 2)) - 1;
                    if (month == 0) { month = 12; --year; }
                    monthdays = DateTime.DaysInMonth(year, month);
                }
                if (last_day < end_cycle)
                {
                    month = int.Parse(month_year.Substring(0, 2)) - 1;
                    if (month == 0) { month = 12; --year; }
                    monthdays = DateTime.DaysInMonth(year, month);
                }
            }
            
            if (tb_select >= starting_day)
            {
                starting_day = tb_select;
                for (int i = 0; monthdays >= (i + starting_day); i++)
                {
                    if (i + starting_day < 10)
                    {
                        days4 = days4 + " t2.DAY0" + (i + starting_day) + ",";
                        Dcount = Dcount + 1;
                    }
                    else
                    {
                        days4 = days4 + " t2.DAY" + (i + starting_day) + ",";
                        Dcount = Dcount + 1;
                    }

                }
            }
        }
        else
        {
           
            starting_day = DateTime.DaysInMonth(year, month) + 1;
           // starting_day = tb_select;
        }
        if (starting_day != 1)
        {

            if (tb_select < starting_day)
            {
                ss = tb_select;
            }
            else if (tb_select >= arrear_starting_day)
            {
                ss = 1;
                starting_day = arrear_starting_day;
            }
            if (last_month == int.Parse(month_year.Substring(0, 2)))
            {
                if (end_cycle > tb_select)
                {
                    starting_day = last_day + 1;
                }
                else if (end_cycle == last_day)
                {
                    starting_day = arrear_starting_day;
                }
                if (end_cycle < last_day)
                {
                    starting_day = 1;
                }
                if (last_day < end_cycle)
                {
                    starting_day = last_day + 1;

                }

            }
        }
        else
        {
            ss = tb_select;
            if (last_month == int.Parse(month_year.Substring(0, 2)))
            {
                starting_day = last_day;
            }
        }
        for (int i = ss; (starting_day - 1) >= i; i++)
        {
           
            if (i < 10)
            {
                
              days4 = days4 + " t1.DAY0" + (i) + ",";
              Dcount = Dcount + 1;
            }
            else
            {
                days4 = days4 + " t1.DAY" + (i) + ",";
                Dcount = Dcount + 1;
            }         

        }
        if (counter == 1)
        {
            return days1;
        }
        else if (counter == 3)
        {
            return days2;
        }
        //vikas add 06/05/2019t4
        else if (counter == 4)
        {
            return days3;
        }
        else if (counter == 5)
        {
            days4 =" '"+Dcount+"' as 'day'," + days4;
            return days4;
        }
        else
        {
            return days;
        }
    }
    public string getmont1(string month)
    {
        string month_name1 = "" , month_n="";
        string[] month1= month.Split(',');
        foreach (string obj in month1)
        {
            if (obj == "'01'")
            {
                month_name1= "JAN";
            }
            else if (obj == "'02'")
            { month_name1= "FEB"; }
            else if (obj == "'03'")
            { month_name1= "MAR"; }
            else if (obj == "'04'")
            { month_name1= "APR"; }
            else if (obj == "'05'")
            { month_name1= "MAY"; }
            else if (obj == "'06'")
            { month_name1= "JUN"; }
            else if (obj == "'07'")
            { month_name1= "JUL"; }
            else if (obj == "'08'")
            { month_name1= "AUG"; }
            else if (obj == "'09'")
            { month_name1= "SEP"; }
            else if (obj == "'10'")
            { month_name1= "OCT"; }
            else if (obj == "'11'")
            { month_name1= "NOV"; }
            else if (obj == "'12'")
            { month_name1= "DEC"; }
             month_n = month_n + "" + month_name1 + ",";
        }
        return month_n;

    }

    public void update_joining_left_date(string where_function, string unit_code, string comp_code, string ddl_client, string ddl_state, string start_date_common, int month, int year)
    {
        //string function = where_function();
        string function = where_function;

        if (unit_code == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + comp_code + "' and client_code = '" + ddl_client + "' and  STATE_NAME  = '" + ddl_state + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + comp_code + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client + "' and  client_wise_state  = '" + ddl_state + "' " + function + " )  ";
        }
        else
        {
            //Session["UNIT_CODE"] = ddl_unit;
           // unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + unit_code + "'";
        }



        string where = "";
        string ot_applicable = getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client + "' and comp_code = '" + comp_code + "'");
        //string start_date_common = start_date1;
        //string end_date_common = get_end_date();
        DateTime start_date, end_date;
       
        if (start_date_common != "" && start_date_common != "1")
        {
            if (start_date_common.Length == 1) { start_date_common = "0" + start_date_common; }
            month = --month;
            if (month == 0) { month = 12; year = --year; }
            where = "and (left_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + month + "/" + year + "','%d/%m/%Y') or joining_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + month + "/" + year + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact(start_date_common + "/" + (month >= 10 ? month.ToString() : "0" + month.ToString()) + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = DateTime.ParseExact(((int.Parse(start_date_common) - 1) >= 10 ? (int.Parse(start_date_common) - 1).ToString() : "0" + (int.Parse(start_date_common) - 1).ToString()) + "/" + month + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            start_date_common = "1";

            where = "and (left_date between str_to_date('1/" + (month.ToString().Length == 1 ? ("0" + month.ToString()) : month.ToString()) + "/" + year + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(year, month) + "/" + month + "/" + year + "','%d/%m/%Y') or `left_date` < str_to_date('" + DateTime.DaysInMonth(year, month) + "/" + (month.ToString().Length == 1 ? ("0" + month.ToString()) : month.ToString()) + "/" + year + "','%d/%m/%Y') or joining_date between str_to_date('1/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(year, month) + "/" + (month.ToString().Length == 1 ? ("0" + month.ToString()) : month.ToString()) + "/" + year + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact("01/" + (month.ToString().Length==1?("0" + month.ToString()):month.ToString())  + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = start_date.AddMonths(1).AddDays(-1);
        }

        MySqlCommand cmd_item = new MySqlCommand("SELECT emp_code, date_format(joining_date,'%d/%m/%Y'), ifnull(DATE_FORMAT(left_date, '%d/%m/%Y'),'1') FROM pay_employee_master WHERE comp_code = '" + comp_code + "' and unit_code IN(" + unit_code + ") " + where, con1);
        con1.Open();
        MySqlDataReader dr2 = cmd_item.ExecuteReader();
        try
        {
            while (dr2.Read())
            {
                string emp_code = dr2.GetValue(0).ToString();
                DateTime joining_date = DateTime.ParseExact(dr2.GetValue(1).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime left_date;
                if (dr2.GetValue(2).ToString() == "1")
                {
                    left_date = end_date;
                }
                else
                {
                    left_date = DateTime.ParseExact(dr2.GetValue(2).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                string query = "", start_date_common_query = "", End_date_common_query = "", ot_query = "", Ot_start_date_common_query = "", Ot_End_date_common_query = "";
                DateTime start_date1 = start_date;
                while (end_date >= start_date1)
                {
                    if (joining_date > start_date1)
                    {
                        if (start_date1.Day < 10)
                        {
                            query = query + "DAY0" + start_date1.Day + "='A',";
                            ot_query = ot_query + "OT_DAY0" + start_date1.Day + "='0',";
                            if (start_date1.Day >= int.Parse(start_date_common) && start_date_common != "1")
                            {
                                start_date_common_query = start_date_common_query + "DAY0" + start_date1.Day + "='A',";
                                Ot_start_date_common_query = Ot_start_date_common_query + "OT_DAY0" + start_date1.Day + "='0',";
                            }
                            else if (start_date_common != "1")
                            {
                                End_date_common_query = End_date_common_query + "DAY0" + start_date1.Day + "='A',";
                                Ot_End_date_common_query = Ot_End_date_common_query + "OT_DAY0" + start_date1.Day + "='0',";
                            }
                        }
                        else
                        {
                            query = query + "DAY" + start_date1.Day + "='A',";
                            ot_query = ot_query + "OT_DAY" + start_date1.Day + "='0',";
                            if (start_date1.Day >= int.Parse(start_date_common) && start_date_common != "1")
                            {
                                start_date_common_query = start_date_common_query + "DAY" + start_date1.Day + "='A',";

                                Ot_start_date_common_query = Ot_start_date_common_query + "OT_DAY" + start_date1.Day + "='0',";
                            }
                            else if (start_date_common != "1")
                            {
                                End_date_common_query = End_date_common_query + "DAY" + start_date1.Day + "='A',";
                                Ot_End_date_common_query = Ot_End_date_common_query + "OT_DAY" + start_date1.Day + "='0',";
                            }
                        }
                    }
                    if (left_date < start_date1)
                    {
                        if (start_date1.Day < 10)
                        {
                            query = query + "DAY0" + start_date1.Day + "='A',";
                            ot_query = ot_query + "OT_DAY0" + start_date1.Day + "='0',";
                            if (start_date1.Day >= int.Parse(start_date_common) && start_date_common != "1")
                            {
                                start_date_common_query = start_date_common_query + "DAY0" + start_date1.Day + "='A',";
                                Ot_start_date_common_query = Ot_start_date_common_query + "OT_DAY0" + start_date1.Day + "='0',";
                            }
                            else if (start_date_common != "1")
                            {
                                End_date_common_query = End_date_common_query + "DAY0" + start_date1.Day + "='A',";
                                Ot_End_date_common_query = Ot_End_date_common_query + "OT_DAY0" + start_date1.Day + "='0',";
                            }
                        }
                        else
                        {
                            query = query + "DAY" + start_date1.Day + "='A',";
                            ot_query = ot_query + "OT_DAY" + start_date1.Day + "='0',";
                            if (start_date1.Day >= int.Parse(start_date_common) && start_date_common != "1")
                            {
                                start_date_common_query = start_date_common_query + "DAY" + start_date1.Day + "='A',";
                                Ot_start_date_common_query = Ot_start_date_common_query + "OT_DAY" + start_date1.Day + "='0',";
                            }
                            else if (start_date_common != "1")
                            {
                                End_date_common_query = End_date_common_query + "DAY" + start_date1.Day + "='A',";
                                Ot_End_date_common_query = Ot_End_date_common_query + "OT_DAY" + start_date1.Day + "='0',";
                            }
                        }
                    }

                    start_date1 = start_date1.AddDays(1);
                }
                if (start_date_common != "" && start_date_common != "1")
                {
                    if (start_date_common_query != "")
                    {
                        operation("UPDATE pay_attendance_muster SET " + start_date_common_query.Substring(0, start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        if (ot_applicable == "1")
                        {
                            operation("UPDATE pay_ot_muster SET " + Ot_start_date_common_query.Substring(0, Ot_start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        }
                    }
                    if (End_date_common_query != "")
                    {
                        operation("UPDATE pay_attendance_muster SET " + End_date_common_query.Substring(0, End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        if (ot_applicable == "1")
                        {
                            operation("UPDATE pay_ot_muster SET " + Ot_End_date_common_query.Substring(0, Ot_End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        }
                    }
                }
                else
                {
                    if (query != "")
                    {
                        operation("UPDATE pay_attendance_muster SET " + query.Substring(0, query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        if (ot_applicable == "1")
                        {
                            operation("UPDATE pay_ot_muster SET " + ot_query.Substring(0, ot_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        }
                    }
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            dr2.Close();
            cmd_item.Dispose();
            con1.Close();
        }

    }
}