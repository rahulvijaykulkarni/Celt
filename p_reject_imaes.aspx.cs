using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Net.Mail;
using System.Web.UI;


public partial class p_reject_imaes : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btn_reject_click(object sender, EventArgs e)
    {
        string path="";
        String emp_code = "";
        String imagename = "";
        int reject_id=0;
        String newpath="";
        try {

            MySqlCommand cmd = new MySqlCommand("select emp_code,emp_name,image_name,image_path,reject from pay_document_verification where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + Session["request_id"].ToString() + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                emp_code = dr.GetValue(0).ToString();
                imagename = dr.GetValue(2).ToString();
                path = dr.GetValue(3).ToString();
                reject_id = Int32.Parse(dr.GetValue(4).ToString());
            }
            d1.con.Close();
            string FileChk = Server.MapPath("~/Temp_images/") + path;
            //System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path); 
            if (reject_id == 0)
            {
            
            if (File.Exists(FileChk))
            {
                //then file is exist
                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='"+emp_code+"' and image_name='"+imagename+"'");
                String newpath1233 = path.Replace(".png", "");
               // String newpath = path.Remove(path.Length - 3);
                newpath = newpath1233 + "_" + temp1 + ".png";
                System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                  
                int res = d.operation("update pay_document_verification set comments='" + txt_comment.Text + "',cur_date=now() , image_path='" + newpath + "',reject='1' where Id='" + Session["request_id"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Successfully.');", true);
                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path); 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Faill!!.');", true);
                }
            }
            else { }
            }
            else if (reject_id == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already Approve  !!!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already rejected !!.');", true);
            }
            update_notification();
            
            string temp = d.getsinglestring("select EMP_EMAIL_ID from pay_employee_master where emp_code=(select  emp_code from pay_document_verification where  id='" + Session["request_id"].ToString() + "')");
            if (temp != "")
            {
                send_email();
            }
            else {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee master page add employee email id..... !!.');", true);
            }

        
        //txt_comment.Text = "";
        }
        catch (Exception eq ){ }
        finally { txt_comment.Text = "";
       // Response.Redirect("Emp_Document_Verification.aspx");
        }
    }
    private void update_notification()
    {
        try
        {
            d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Document Upload Is Rejected By " + Session["USERNAME"].ToString() + "','0','" + Session["emp_code"].ToString() + "','EmployeeMaster.aspx' ");


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
        }
    }

    protected void send_email()
    {
        MySqlCommand cmd = new MySqlCommand("select EMP_EMAIL_ID  from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + Session["emp_code"].ToString() + "'", d.con);
        d.con.Open();
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            string emp_mailid = dr.GetValue(0).ToString();



           
            string body = "Hello Your Document Is Rejected";
            using (MailMessage mailMessage = new MailMessage())
            {
                SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");
                mailMessage.From = new MailAddress("info@celtsoft.com");

                mailMessage.Subject = "Documen Related";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(emp_mailid);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mailMessage);
                d.con.Close();
            }
            d.con.Close();
        }
    }
}