using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userDashboard : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            d.operation("Insert into pay_dashboard (COMP_CODE,CLIENT_CODE,STATE,UNIT_CODE,EMP_CODE,DESIGNATION,Problem_Area,date_time,type) select pay_employee_master.comp_code, pay_unit_master.client_code, pay_unit_master.state_name, pay_unit_master.unit_code, pay_employee_master.emp_code, grade_code, concat('Attendance not Received ',pay_employee_master.emp_name), now(), 1 from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and pay_employee_master.comp_code = pay_unit_master.comp_code where in_time != 'Flexible' and emp_code not in (select emp_code from pay_dashboard where date_format(pay_dashboard.date_time,'%d/%m/%Y') = date_format(now(),'%d/%m/%Y') and pay_dashboard.comp_code = '"+Session["COMP_CODE"].ToString()+"') and emp_code not in (select emp_code from pay_android_attendance_logs where date_format(pay_android_attendance_logs.date_time,'%d/%m/%Y') = date_format(now(),'%d/%m/%Y') and pay_android_attendance_logs.comp_code = '" + Session["COMP_CODE"].ToString() + "') and pay_employee_master.comp_code = 'C01' and emp_code not in (select emp_code from pay_employee_master where hour(now()) < time_format(in_time, '%H:%i') and comp_code = '" + Session["COMP_CODE"].ToString() + "')");
            d.con1.Open();
            try
            {
                MySqlDataAdapter cmd = new MySqlDataAdapter("Select distinct(client_code),(select client_name from pay_client_master where client_code = pay_client_state_role_grade.client_code and COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' ) as 'client name' from pay_client_state_role_grade where comp_code='" + Session["comp_code"] + "' and role_name = '" + Session["ROLE"].ToString() + "' ORDER BY client_code", d.con1);
                System.Data.DataTable dt = new DataTable();
                cmd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_client.DataSource = dt;
                    ddl_client.DataTextField = dt.Columns[1].ToString();
                    ddl_client.DataValueField = dt.Columns[0].ToString();
                    ddl_client.DataBind();
                }
                dt.Dispose();
                d.con1.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con1.Close();
            }
        }
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_state.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state.DataSource = dt_item;
                ddl_state.DataTextField = dt_item.Columns[0].ToString();
                ddl_state.DataValueField = dt_item.Columns[0].ToString();
                ddl_state.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state.Items.Insert(0, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        gv_dash_board.Visible = true;
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1 = new MySqlDataAdapter("select pay_dashboard.id, CLIENT_name as 'CLIENT NAME',pay_dashboard.STATE as STATE,UNIT_Name as 'BRANCH NAME',EMP_Name as 'EMPLOYEE NAME',Grade_desc AS DESIGNATION,Problem_Area as DESCRIPTION,date_format(pay_dashboard.date_time,'%d/%m/%Y %h:%i:%s %p') as 'LOGIN DATE TIME' from pay_dashboard inner join pay_client_master on pay_client_master.client_code = pay_dashboard.client_code and pay_client_master.comp_code = pay_dashboard.comp_code inner join pay_unit_master on pay_unit_master.unit_code = pay_dashboard.unit_code and pay_unit_master.comp_code = pay_dashboard.comp_code inner join pay_employee_master on pay_employee_master.emp_code = pay_dashboard.emp_code and pay_employee_master.comp_code = pay_dashboard.comp_code inner join pay_grade_master on pay_grade_master.grade_code = pay_dashboard.DESIGNATION and pay_grade_master.comp_code = pay_dashboard.comp_code where pay_dashboard.status = 0", d.con);
        adp1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            gv_dash_board.DataSource = ds1.Tables[0];
            gv_dash_board.DataBind();
            d.con.Close();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records Found !!!');", true);
        }
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        d.operation("Update pay_dashboard set Status= 1 where id = " + grdrow.Cells[1].Text);
        btn_show_Click(null,null);
    }
    protected void gv_dash_board_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = true;
    }
    protected void gv_dash_board_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_dash_board.UseAccessibleHeader = false;
            gv_dash_board.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}