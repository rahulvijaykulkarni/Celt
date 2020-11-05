using System;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class summary_Report : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            client_list();
        }

    }
    protected void count_details(object sender, EventArgs e)
    {
        total_attendanceemployee_count(null, null);
        total_employee_count(null, null);
    
    }
    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_clientwisestate.Items.Clear();
       
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code is null ORDER BY STATE", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_clientwisestate.Items.Add(dr_item1[0].ToString());
               
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));
        total_employee_count2(null, null);
        total_attendance_count2(null, null);
    }

    protected void get_city_list1(object sender, EventArgs e)
    {
      
        ddl_unitcode.Items.Clear();

        MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and STATE_NAME='" + ddl_clientwisestate.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
        MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
        DataSet ds1 = new DataSet();
        sda1.Fill(ds1);
        ddl_unitcode.DataSource = ds1.Tables[0];
        ddl_unitcode.DataValueField = "UNIT_CODE";
        ddl_unitcode.DataTextField = "CUNIT";
        ddl_unitcode.DataBind();
        ddl_unitcode.Items.Insert(0, new ListItem("Select"));
        total_employee_count1(null, null);
        total_attendance_count1(null, null);
    }

    public void client_list()
    {
        d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unit_client.DataSource = dt_item;
                ddl_unit_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_unit_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_unit_client.DataBind();


               
            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_unit_client.Items.Insert(0, "Select");
          
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

           // total_employee_count2(null, null);
        }

    }

    protected void total_attendance_count2(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(pay_android_attendance_logs.emp_code) from pay_android_attendance_logs inner join pay_employee_master on pay_android_attendance_logs.emp_code=pay_employee_master.emp_code where  CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
           txt_attendancecount.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }
    protected void total_employee_count2(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(emp_code) from pay_employee_master where  CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_totalemp_count.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }
    protected void total_attendance_count1(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(pay_android_attendance_logs.emp_code) from pay_android_attendance_logs inner join pay_employee_master on pay_android_attendance_logs.emp_code=pay_employee_master.emp_code  where  CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' AND client_wise_state='" + ddl_clientwisestate.SelectedValue + "' and pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        // MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and STATE_NAME='" + ddl_clientwisestate.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_attendancecount.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }
    protected void total_employee_count1(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(emp_code) from pay_employee_master where  CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' AND client_wise_state='" + ddl_clientwisestate.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
       // MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and STATE_NAME='" + ddl_clientwisestate.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_totalemp_count.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }

    protected void total_employee_count(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(emp_code) from pay_employee_master where unit_code='" + ddl_unitcode.SelectedValue + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' AND client_wise_state='" + ddl_clientwisestate.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_totalemp_count.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }
    protected void total_attendanceemployee_count(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(emp_code) from pay_android_attendance_logs where unit_code='" + ddl_unitcode.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "' and (Camera_intime!='' or Attendances_intime!='')", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_attendancecount.Text = dr.GetValue(0).ToString();
        }

        dr.Close();
        d.con.Close();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}