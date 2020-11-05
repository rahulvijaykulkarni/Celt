using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

public partial class User_Management : System.Web.UI.Page
{
    DAL d = new DAL();
    string login_id = null;
    protected void Page_Load(object sender, EventArgs e)
    {
		
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Create User", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create User", Session["COMP_CODE"].ToString()) == "R")
        {
            btnupdate.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create User", Session["COMP_CODE"].ToString()) == "U")
        {
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create User", Session["COMP_CODE"].ToString()) == "C")
        {
        }


        if (!this.IsPostBack)
        {
            d.con1.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT role_name FROM pay_role_master where comp_code='"+Session["comp_code"].ToString()+"'", d.con1);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataTextField = "role_name";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Role--", ""));

                ds.Clear();
                MySqlDataAdapter adp = new MySqlDataAdapter("select login_id, concat(user_name,' - ', login_id) as login_name from pay_user_master", d.con1);
                adp.Fill(ds);
                ddl_loginid.DataSource = ds.Tables[0];
                ddl_loginid.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con1.Close();
            }
        }
    }
   
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {

        String login_id = ddl_loginid.SelectedValue;
        String password = txt_password.Text;
        String role = DropDownList1.SelectedItem.Text;
        String flag;
        int counter = 0;
        if (CheckBox1.Checked == true)
        {
            flag = "A";
        }
        else
        {
            flag = "P";
            counter = 3;

        }

        int passlen = txt_password.Text.Length;
        if (password.Equals(txt_con_password.Text.ToString()))
        {
                d.operation("UPDATE pay_user_master SET USER_PASSWORD ='" + d.Encrypt(password) + "', ROLE ='" + role + "', flag ='" + flag + "',COUNTER = " + int.Parse(counter.ToString()) + ", Modify_user = '" + Session["USERID"].ToString() + "', modify_date= now()  WHERE LOGIN_ID = '" + login_id + "' and comp_code='"+Session["comp_code"].ToString()+"'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password should have min. 8 bytes...!!!');", true);
        }
    }
   
    protected void ddl_loginid_SelectedIndexChanged(object sender, EventArgs e)
    {
        string role = "";
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select ROLE from pay_user_master where login_id='"+ddl_loginid.SelectedValue+"' and comp_code='"+Session["comp_code"].ToString()+"'", d.con);
        try
        {
            role = (string)cmd.ExecuteScalar();
        }
        catch
        {
            throw;
        }
        finally
        {
            d.con.Close();
            cmd.Dispose();
        }

        if (!role.Equals(""))
        {
            DropDownList1.SelectedValue = role;
        }

    }
}