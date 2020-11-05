using System;
using System.IO;
using System.Management.Automation;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Backup_Page : System.Web.UI.Page
{
    DAL d1 = new DAL();
   
    string MySql = "";
    string BackupData = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }		
        if (d1.getaccess(Session["ROLE"].ToString(), "System Backup",Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "System Backup",Session["COMP_CODE"].ToString()) == "R")
        {
            btn_backup.Visible = false;
            //btn_edit.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "System Backup",Session["COMP_CODE"].ToString()) == "U")
        {
            btn_backup.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "System Backup", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_backup.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
       // d1.con1.Close();
    }

    protected void btn_BackUp_Click(object sender, EventArgs e) 
    {
        try
        {
            var shell = PowerShell.Create();

            MySql = Server.MapPath("~\\Images\\backup.zip");
            string db_backUp = Server.MapPath("~\\EMP_Images\\celtpayroll.sql");

            BackupData = "mysqldump -u " + txt_user_id.Text + " -p " + txt_password.Text + " celtpayroll > " + db_backUp + "";
            // BackupData = "mysqldump -u root -proot celtpay > " + MySql + "";
            shell.Commands.AddScript(BackupData);
            shell.Invoke();
            if (File.Exists(MySql))
            {
                File.Delete(MySql);
            }
           //// ZipFile.CreateFromDirectory(Server.MapPath("~\\EMP_Images"), MySql);


            Download_File(MySql);
            File.Delete(MySql);
            string backup = "BackUp Done Successfully!";
            d1.logs(backup);
        }
        catch(Exception error_backup)
        {
            d1.logs(error_backup.Message);
        }

     //   MessageBox.Show("Successfully Database Backup Completed");
    }

    private void Download_File(string FilePath)
    {
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
        Response.WriteFile(FilePath);
        Response.End();
    }

    protected void btn_PasswordGenerat_Click(object sender, EventArgs e)
    {

        string left_date = d1.getsinglestring("select left_date from pay_employee_master where emp_code='" + txt_emp_login_id.Text.ToString() + "'");

        if (!left_date.Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee already left for system !!!')", true);
           // return;
        }
        else {
            string newPassword = GetSha256FromString(txtPassword.Text.ToString());

            txt_generatesha256.Text = newPassword;
            txt_orignalpassword.Text = txtPassword.Text.ToString();

            int result = d1.operation("update pay_user_master set user_password='" + newPassword + "',flag='A',password_changed_date='2020-05-14' where Login_id='" + txt_emp_login_id.Text.ToString() + "'");

            txtPassword.Text = "";
            txt_emp_login_id.Text = "";
            txt_orignalpassword.Text = "";
            txt_generatesha256.Text = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee password change successfully !!!')", true);

        }

        
    }

    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}
