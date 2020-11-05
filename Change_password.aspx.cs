using System;
using System.Web.UI;
using System.Data;
using MySql.Data.MySqlClient;

public partial class Change_password : System.Web.UI.Page
{
    DAL d1 = new DAL();
    //link_url lu = new link_url();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d1.getaccess(Session["ROLE"].ToString(), "Change Password", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Change Password", Session["COMP_CODE"].ToString()) == "R")
        {
          
          //  Button1.Visible = false;
           
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Change Password", Session["COMP_CODE"].ToString()) == "U")
        {
           
          //  Button1.Visible = false;
           
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Change Password", Session["COMP_CODE"].ToString()) == "C")
        {
           
        }
        


        //if (int.Parse(Session["CHANGE_PASS"].ToString()) <= 0)
      //  {
         //   Button4.Visible = false;
            
      //  }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        d1.con1.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("select USER_PASSWORD from pay_user_master where LOGIN_ID = '" + Session["USERID"].ToString() + "'", d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int Count = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (txt_oldpass.Text.ToString() == dr["USER_PASSWORD"].ToString())
                {
                    Count = 2;
                }
            }
            dt.Dispose();
            da.Dispose();

            if (Count == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Old Password...!!!');", true);
                txt_oldpass.Focus();
                return;
            }
            else if (password_history())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You cannot use your last 8 passwords !!!');", true);
                txt_oldpass.Focus();
                return;
            }
            else
            {
                d1.operation("UPDATE pay_user_master SET USER_PASSWORD = '" + txt_pass.Text.ToString() + "', password_changed_date = now(), first_login='1' WHERE LOGIN_ID='" + Session["USERID"].ToString() + "'");
                d1.operation("insert into password_history (login_id,password,created_date) values ('" + Session["LOGIN_ID"].ToString() + "','" + txt_pass.Text.ToString() + "',now())");

                if (Session["CHANGE_PASS"].ToString().Equals("0"))
                {
                    Response.Redirect("Login_Page.aspx",false);
                }
                else
                {
                   // Response.Redirect("Home.aspx",false);
                    Response.Redirect("Login_Page.aspx", false);
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


    }
    protected void close(object sender, EventArgs e)
    {

        Response.Redirect("Login_Page.aspx");
    }
    private bool password_history()
    {
        MySqlCommand cmd = new MySqlCommand("select password from password_history where LOGIN_ID = '" + Session["USERID"].ToString() + "' limit 8", d1.con);
        d1.con.Open();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow dr in dt.Rows)
        {
            if (txt_pass.Text.ToString() == dr["password"].ToString())
            {
                dt.Dispose();
                da.Dispose();
                d1.con.Close();
                return true;
            }
        }
        dt.Dispose();
        da.Dispose();
        d1.con.Close();
        return false;
    }
}