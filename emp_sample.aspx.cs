using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Configuration;
using System.Data.OleDb;

public partial class emp_sample : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    DAL d4 = new DAL();
    DAL d5 = new DAL();

    public string Message = "0", reject_attendance = "0", appro_attendannce = "0", appro_attendannce_finanace = "0", deployment="0",closed_branch="0";
    public string Emp_Message = "0", Emp_reject_attendance = "0", Emp_appro_attendannce = "0", Emp_appro_attendannce_finanace = "0", Emp_deployment = "0",Emp_closed_branch="0";
    
    public string emp_con_remaing = "0", driver_con_remaing = "0", emp_con_approve_finance = "0", driver_con_approve_finance = "0", emp_con_approve_admin = "0", driver_con_approve_admin = "0", emp_con_reject_finance = "0", driver_con_reject_finance = "0", Conv_deployment = "0", Conv_closed_branch = "0";
    public string Conv_Message_emp = "0", Conv_reject_attendance_emp = "0", Conv_appro_attendannce_emp = "0", Conv_appro_attendannce_finanace_emp = "0", Conv_deployment_emp = "0", Conv_closed_branch_emp = "0";

    public string Material_Message = "0", Material_reject_attendance = "0", Material_appro_attendannce = "0", Material_appro_attendannce_finanace = "0", Material_deployment = "0", Material_closed_branch = "0";
    public string Material_Message_emp = "0", Material_reject_attendance_emp = "0", Material_appro_attendannce_emp = "0", Material_appro_attendannce_finanace_emp = "0", Material_deployment_emp = "0", Material_closed_branch_emp = "0";
    
    
    public int file_uploaded  = 0,ot_flag1=0;
    //public int required_count = 0;
    //public double actual_count = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (d.getaccess(Session["ROLE"].ToString(), "Attendance Sheet", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Attendance Sheet", Session["COMP_CODE"].ToString()) == "R")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Attendance Sheet", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Attendance Sheet", Session["COMP_CODE"].ToString()) == "C")
        {

        }
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            client_code();
            client_material();
            Panel_driver_conv.Visible = false;
            Panel21.Visible = false;

            ////for conveyance
            Panel_notification_conv.Visible = false;
              Panel_notification_conv_driver.Visible = false;
            // Panel_not_approve_conv_driver.Visible = false;
            Panel_appro_con_driver.Visible = false;
            Panel_reject_con_driver.Visible = false;
            Panel_appro_finance_con_driver.Visible = false;

            Panel_not_approve_conv.Visible = false;
            Panel_appro_con.Visible = false;
            Panel_appro_finance_con.Visible = false;
            Panel_reject_con.Visible = false;

            // for material
            Panel_notification_material.Visible = false;
            Panel_not_appr_material.Visible = false;
            Panel_appro_atte_material.Visible = false;
            Panel_reject_Material.Visible = false;
            Panel_approv_finance_material.Visible = false;

            // con_button.Visible = false;

            /////////////////
            Notification_panel.Visible = false;
            btn_reports.Visible = false;
            Panel1.Visible = false;
            pnl_branch.Visible = false;
            btn_attendance.Visible = false;
            approval_panel.Visible = false;
            reject_panel.Visible = false;
            panel_deployment.Visible = false;
            approval_finance_panel.Visible = false;
            Panel6.Visible = false;
            Panel10.Visible = false;
            //btn_add_emp.Visible = false;//vikas
            btn_save.Visible = false;//vikas
            btn_approve.Visible = false;
            panel_clo_branch.Visible = false;
            ViewState["Message"] = 0;
            ViewState["reject_attendance"] = 0;
            ViewState["appro_attendannce"] = 0;
            ViewState["appro_attendannce_finanace"] = 0;
            ViewState["deployment"] = 0;
            ViewState["closed_branch"] = 0;

            ViewState["Emp_Message"] = 0;
            ViewState["Emp_reject_attendance"] = 0;
            ViewState["Emp_appro_attendannce"] = 0;
            ViewState["Emp_appro_attendannce_finanace"] = 0;
            ViewState["Emp_deployment"] = 0;
            ViewState["Emp_closed_branch"] = 0;

            ViewState["emp_con_remaing"] = 0;
            ViewState["emp_con_approve_finance"] = 0;
            ViewState["emp_con_approve_admin"] = 0;
            ViewState["emp_con_reject_finance"] = 0;
            ViewState["Conv_deployment"] = 0;
            ViewState["Conv_closed_branch"] = 0;

            ViewState["driver_con_remaing"] = 0;
            ViewState["driver_con_approve_finance"] = 0;
            ViewState["driver_con_approve_admin"] = 0;
            ViewState["driver_con_reject_finance"] = 0;


            ViewState["Conv_Message_emp"] = 0;
            ViewState["Conv_reject_attendance_emp"] = 0;
            ViewState["Conv_appro_attendannce_emp"] = 0;
            ViewState["Conv_appro_attendannce_finanace_emp"] = 0;
            ViewState["Conv_deployment_emp"] = 0;
            ViewState["Conv_closed_branch_emp"] = 0;


            ViewState["Material_Message"] = 0;
            ViewState["Material_reject_attendance"] = 0;
            ViewState["Material_appro_attendannce"] = 0;
            ViewState["Material_appro_attendannce_finanace"] = 0;
            ViewState["Material_deployment"] = 0;
            ViewState["Material_closed_branch"] = 0;



            gv_reject_panel.Visible = false;
            att_upload_panel.Visible = false;
            gv_approve_panel.Visible = false;
            gv_deployment_panel.Visible = false;


            RandM_panel.Visible = false;
            administrative_panel.Visible = false;
           // show_upload.Visible = false;
            //show_upload_adm.Visible = false;
            gv_r_m.Visible = false;
            btn_r_m.Visible = false;
            admin_gv_ap.Visible = false;
            btn_admin_ap.Visible = false;
            client_name_r_m();
            //conveyance

            con_ddl_client.Items.Clear();
            txt_date_conveyance.Text = d.getCurrentMonthYear();
            txt_month_material.Text = d.getCurrentMonthYear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    con_ddl_client.DataSource = dt_item;
                    con_ddl_client.DataTextField = dt_item.Columns[0].ToString();
                    con_ddl_client.DataValueField = dt_item.Columns[1].ToString();
                    con_ddl_client.DataBind();
                }
                dt_item.Dispose();

                d.con.Close();
                con_ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }

        Message = ViewState["Message"].ToString();
        reject_attendance = ViewState["reject_attendance"].ToString();
        appro_attendannce = ViewState["appro_attendannce"].ToString();
        appro_attendannce_finanace = ViewState["appro_attendannce_finanace"].ToString();
        deployment = ViewState["deployment"].ToString();
        closed_branch = ViewState["closed_branch"].ToString();

        Emp_Message = ViewState["Emp_Message"].ToString();
        Emp_reject_attendance = ViewState["Emp_reject_attendance"].ToString();
        Emp_appro_attendannce = ViewState["Emp_appro_attendannce"].ToString();
        Emp_appro_attendannce_finanace = ViewState["Emp_appro_attendannce_finanace"].ToString();
        Emp_deployment = ViewState["Emp_deployment"].ToString();
        Emp_closed_branch = ViewState["Emp_closed_branch"].ToString();
        // for conveyance


        emp_con_remaing = ViewState["emp_con_remaing"].ToString();
        emp_con_approve_finance = ViewState["emp_con_approve_finance"].ToString();
        emp_con_approve_admin = ViewState["emp_con_approve_admin"].ToString();
        emp_con_reject_finance = ViewState["emp_con_reject_finance"].ToString();
        Conv_deployment = ViewState["Conv_deployment"].ToString();
        Conv_closed_branch = ViewState["Conv_closed_branch"].ToString();

        driver_con_remaing = ViewState["driver_con_remaing"].ToString();
        emp_con_approve_finance = ViewState["driver_con_approve_finance"].ToString();
        driver_con_approve_admin = ViewState["driver_con_approve_admin"].ToString();
        driver_con_reject_finance = ViewState["driver_con_reject_finance"].ToString();
        Panel_notification_conv_driver.Visible = false; Panel_appro_con_driver.Visible = false; Panel_reject_con_driver.Visible = false; Panel_appro_finance_con_driver.Visible = false;

        Conv_Message_emp = ViewState["Conv_Message_emp"].ToString();
        Conv_reject_attendance_emp = ViewState["Conv_reject_attendance_emp"].ToString();
        Conv_appro_attendannce_emp = ViewState["Conv_appro_attendannce_emp"].ToString();
        Conv_appro_attendannce_finanace_emp = ViewState["Conv_appro_attendannce_finanace_emp"].ToString();
        Conv_deployment_emp = ViewState["Conv_deployment_emp"].ToString();
        Conv_closed_branch_emp = ViewState["Conv_closed_branch_emp"].ToString();
        Panel_notification_conv.Visible = false; Panel_not_approve_conv.Visible = false; Panel_appro_con.Visible = false; Panel_reject_con.Visible = false; Panel_appro_finance_con.Visible = false;

        // for material

        Material_Message = ViewState["Material_Message"].ToString();
        Material_reject_attendance = ViewState["Material_reject_attendance"].ToString();
        Material_appro_attendannce = ViewState["Material_appro_attendannce"].ToString();
        Material_appro_attendannce_finanace = ViewState["Material_appro_attendannce_finanace"].ToString();
        Conv_deployment = ViewState["Material_deployment"].ToString();
        Conv_closed_branch = ViewState["Material_closed_branch"].ToString();
        Panel_not_appr_material.Visible = false; Panel_appro_atte_material.Visible = false; Panel_reject_Material.Visible = false; Panel_approv_finance_material.Visible = false; Panel_notification_material.Visible = false;

        gv_reject_panel.Visible = false;
        gv_approve_panel.Visible = false;
        gv_deployment_panel.Visible = false;
    }

    protected void btn_process_Click(object sender, EventArgs e)
    {
        hidden_month.Value = txttodate.Text.Substring(0, 2);
        hidden_year.Value = txttodate.Text.Substring(3);
        string function = where_function();
        gv_attendace_load(function);
        gv_approve_attendace_load(function);

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Submit Successfully!!');", true);
        if (!gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 1, function))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add Left Date for Unchecked Employees !!!');", true);
            return;
        }

        Panel21.Visible = true;
        btn_reports.Visible = true;
        btn_save.Visible = true;
        btn_approve.Visible = true;
        Panel1.Visible = true;
        //btn_attendance.Visible = true;
        att_upload_panel.Visible = true;
        ////panel_deployment.Visible = true;
        //if (checkcount())
        //{
        Session["client_code"] = ddl_client.SelectedValue;
        Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_");
        Session["unit_code1"] = ddl_unitcode.SelectedValue;
        Session["client_code1"] = ddl_client.SelectedItem.Text;
        Session["year"] = txttodate.Text;

        Panel6.Visible = false;
        Panel10.Visible = false;
        Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Attendance(Present Days) !!!');", true);
        //}

    }

    private string chkday(string day)
    {
        if (day == "1") { return "01"; }
        else if (day == "2") { return "02"; }
        else if (day == "3") { return "03"; }
        else if (day == "4") { return "04"; }
        else if (day == "5") { return "05"; }
        else if (day == "6") { return "06"; }
        else if (day == "7") { return "07"; }
        else if (day == "8") { return "08"; }
        else if (day == "9") { return "09"; }
        return day;

    }
    protected void client_material() 
    {
        ddl_client_material.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_material.DataSource = dt_item;
                ddl_client_material.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_material.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_material.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client_material.Items.Insert(0, "Select");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    
    }
    protected void client_code()
    {// and client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "')
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client.DataSource = dt_item;
                ddl_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_client.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client.Items.Insert(0, "Select");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }
    private string chkmonth(string month)
    {
        if (month == "1") { return "01"; }
        else if (month == "2") { return "02"; }
        else if (month == "3") { return "03"; }
        else if (month == "4") { return "04"; }
        else if (month == "5") { return "05"; }
        else if (month == "6") { return "06"; }
        else if (month == "7") { return "07"; }
        else if (month == "8") { return "08"; }
        else if (month == "9") { return "09"; }

        return month;

    }
    protected void updatesunday()
    {
        int count = CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2);

        for (int i = 1; i < count; i++)
        {
            string countnew = Convert.ToString(i);
            string countnew1 = chkday(countnew);
            string chkmonth1 = chkmonth(hidden_month.Value);
            string date = string.Concat(countnew1 + "/" + chkmonth1 + "/" + hidden_year.Value);

            DateTime date1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            string days = date1.DayOfWeek.ToString();

            string dateupdate = countnew1;
            if (days == "Sunday")
            {
                int res = d.operation("update pay_attendance_muster set DAY" + dateupdate.ToString() + "='W' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' and DAY" + dateupdate.ToString() + "='0'  ");
            }

        }




    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    private void fill_cell(GridViewRowEventArgs e, DropDownList tb, int location)
    {

        if (tb.SelectedValue == "P")
        {
            tb.BackColor = Color.LimeGreen;
        }
        else if (tb.SelectedValue == "A")
        {
            tb.BackColor = Color.Red;
        }
        else if (tb.SelectedValue == "HD")
        {
            tb.BackColor = Color.Orange;
        }
        else if (tb.SelectedValue == "L")
        {
            tb.BackColor = Color.Yellow;
        }
        else if (tb.SelectedValue == "W")
        {
            tb.BackColor = Color.Violet;
        }
        else if (tb.SelectedValue == "H")
        {
            tb.BackColor = Color.Pink;
        }
        else if (tb.SelectedValue == "CL")
        {
            tb.BackColor = Color.Yellow;
        }
        else if (tb.SelectedValue == "PL")
        {
            tb.BackColor = Color.YellowGreen;
        }
        else if (tb.SelectedValue == "ML")
        {
            tb.BackColor = Color.Wheat;
        }
        else if (tb.SelectedValue == "PH")
        {
            tb.BackColor = Color.Aqua;
        }
        else if (tb.SelectedValue == "CO")
        {
            tb.BackColor = Color.Peru;
        }
    }
    protected void gv_attendance_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_attendance.UseAccessibleHeader = false;
            gv_attendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
    protected void shiftcalendar_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0'");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }




        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");

        //string start_date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN( " + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN( " + unit_code + ") limit 1))");
        string start_date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_unit_master INNER JOIN pay_billing_master_history ON  pay_unit_master.unit_code = pay_billing_master_history.billing_unit_code AND  pay_unit_master.comp_code = pay_billing_master_history.comp_code  WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN( " + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_unit_master  INNER JOIN  pay_billing_master  ON   pay_unit_master.unit_code = pay_billing_master.billing_unit_code    AND    pay_unit_master.comp_code = pay_billing_master.comp_code   WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN( " + unit_code + ") limit 1))");
        //string end_date = get_end_date();

        //vikas add 07/05/2019

        string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and client_code='" + ddl_client.SelectedValue.ToString() + "'");


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ot_applicable == "2")
            {
                TextBox ot_text1 = (TextBox)e.Row.FindControl("txt_ot_hours") as TextBox;
                   // ot_text1.Visible = false; //rahul changes
                    ot_text1.Visible = true;
            }
            else if (ot_applicable == "3")
            {
                if (tempflag=="yes")
                {
                    TextBox ot_text1 = (TextBox)e.Row.FindControl("txt_ot_hours") as TextBox;
                     //ot_text1.Visible = false; //rahul changes
                    ot_text1.Visible = true;
                }else{
                    int i = 31;
                    for (i = 1; i <= 31; i++)
                    {
                        var txtname = "txt_ot_" + i.ToString();
                        TextBox ot_text = (TextBox)e.Row.FindControl(txtname) as TextBox;
                        ot_text.Visible = false;
                    }
                    TextBox ot_text1 = (TextBox)e.Row.FindControl("txt_ot_hours") as TextBox;
                    ot_text1.Visible = true;
                }

                
            }
            else
            {
                int i = 31;
                for (i = 1; i <= 31; i++)
                {
                    var txtname = "txt_ot_" + i.ToString();
                    TextBox ot_text = (TextBox)e.Row.FindControl(txtname) as TextBox;
                    ot_text.Visible = false;
                }
                TextBox ot_text1 = (TextBox)e.Row.FindControl("txt_ot_hours") as TextBox;
                    //ot_text1.Visible = false;//rahul changes
                    ot_text1.Visible = true;
            }

            DataRowView drv = e.Row.DataItem as DataRowView;
            DropDownList ddlCategories1 = e.Row.FindControl("DropDownList1") as DropDownList;
            DropDownList ddlCategories2 = e.Row.FindControl("DropDownList2") as DropDownList;
            DropDownList ddlCategories3 = e.Row.FindControl("DropDownList3") as DropDownList;
            DropDownList ddlCategories4 = e.Row.FindControl("DropDownList4") as DropDownList;
            DropDownList ddlCategories5 = e.Row.FindControl("DropDownList5") as DropDownList;
            DropDownList ddlCategories6 = e.Row.FindControl("DropDownList6") as DropDownList;
            DropDownList ddlCategories7 = e.Row.FindControl("DropDownList7") as DropDownList;
            DropDownList ddlCategories8 = e.Row.FindControl("DropDownList8") as DropDownList;
            DropDownList ddlCategories9 = e.Row.FindControl("DropDownList9") as DropDownList;
            DropDownList ddlCategories10 = e.Row.FindControl("DropDownList10") as DropDownList;
            DropDownList ddlCategories11 = e.Row.FindControl("DropDownList11") as DropDownList;
            DropDownList ddlCategories12 = e.Row.FindControl("DropDownList12") as DropDownList;
            DropDownList ddlCategories13 = e.Row.FindControl("DropDownList13") as DropDownList;
            DropDownList ddlCategories14 = e.Row.FindControl("DropDownList14") as DropDownList;
            DropDownList ddlCategories15 = e.Row.FindControl("DropDownList15") as DropDownList;
            DropDownList ddlCategories16 = e.Row.FindControl("DropDownList16") as DropDownList;
            DropDownList ddlCategories17 = e.Row.FindControl("DropDownList17") as DropDownList;
            DropDownList ddlCategories18 = e.Row.FindControl("DropDownList18") as DropDownList;
            DropDownList ddlCategories19 = e.Row.FindControl("DropDownList19") as DropDownList;
            DropDownList ddlCategories20 = e.Row.FindControl("DropDownList20") as DropDownList;
            DropDownList ddlCategories21 = e.Row.FindControl("DropDownList21") as DropDownList;
            DropDownList ddlCategories22 = e.Row.FindControl("DropDownList22") as DropDownList;
            DropDownList ddlCategories23 = e.Row.FindControl("DropDownList23") as DropDownList;
            DropDownList ddlCategories24 = e.Row.FindControl("DropDownList24") as DropDownList;
            DropDownList ddlCategories25 = e.Row.FindControl("DropDownList25") as DropDownList;
            DropDownList ddlCategories26 = e.Row.FindControl("DropDownList26") as DropDownList;
            DropDownList ddlCategories27 = e.Row.FindControl("DropDownList27") as DropDownList;
            DropDownList ddlCategories28 = e.Row.FindControl("DropDownList28") as DropDownList;
            DropDownList ddlCategories29 = e.Row.FindControl("DropDownList29") as DropDownList;
            DropDownList ddlCategories30 = e.Row.FindControl("DropDownList30") as DropDownList;
            DropDownList ddlCategories31 = e.Row.FindControl("DropDownList31") as DropDownList;

            //ddlCategories1.Items.Insert(0, new ListItem("Select Shift", "0"));
            //string ddlCategories = "", finalddl = "";
            //hidden_month.Value = txttodate.Text.Substring(0, 2);
            //hidden_year.Value = txttodate.Text.Substring(3);
            //if (start_date_common != "" && start_date_common != "1")
            //{
            //    for (int i = (int.Parse(end_date) + 1); (int.Parse(end_date)) <= i && i <= CountDay(int.Parse(hidden_month.Value),int.Parse(hidden_year.Value),2); i++)
            //    {
            //        string cntrlname = "DropDownList" + i.ToString();
            //        DropDownList txt_day1 = (DropDownList)e.Row.FindControl(cntrlname);
            //        txt_day1.Attributes["disabled"] = "disabled";

            //        cntrlname = "OT_DropDownList" + i.ToString();
            //        txt_day1 = (DropDownList)e.Row.FindControl(cntrlname);
            //        txt_day1.Attributes["disabled"] = "disabled";

            //    }

            //}

            if (ddlCategories1 != null)
            {
                ddlCategories1.SelectedValue = drv["DAY01"].ToString();
                //ddlCategories1.Enabled = false;
            }
            else { ddlCategories1.SelectedValue = "0"; }
            fill_cell(e, ddlCategories1, 41);
            if (ddlCategories2 != null)
            {
                ddlCategories2.SelectedValue = drv["DAY02"].ToString();
            }
            else { ddlCategories2.SelectedValue = "0"; } fill_cell(e, ddlCategories2, 42);
            if (ddlCategories3 != null)
            {
                ddlCategories3.SelectedValue = drv["DAY03"].ToString();
            }
            else { ddlCategories3.SelectedValue = "0"; } fill_cell(e, ddlCategories3, 43);
            if (ddlCategories4 != null)
            {
                ddlCategories4.SelectedValue = drv["DAY04"].ToString();
            }
            else { ddlCategories4.SelectedValue = "0"; } fill_cell(e, ddlCategories4, 44);
            if (ddlCategories5 != null)
            {
                ddlCategories5.SelectedValue = drv["DAY05"].ToString();
            }
            else { ddlCategories5.SelectedValue = "0"; } fill_cell(e, ddlCategories5, 45);
            if (ddlCategories6 != null)
            {
                ddlCategories6.SelectedValue = drv["DAY06"].ToString();
            }
            else { ddlCategories6.SelectedValue = "0"; } fill_cell(e, ddlCategories6, 46);
            if (ddlCategories7 != null)
            {
                ddlCategories7.SelectedValue = drv["DAY07"].ToString();
            }
            else { ddlCategories7.SelectedValue = "0"; } fill_cell(e, ddlCategories7, 47);
            if (ddlCategories8 != null)
            {
                ddlCategories8.SelectedValue = drv["DAY08"].ToString();
            }
            else { ddlCategories8.SelectedValue = "0"; } fill_cell(e, ddlCategories8, 48);
            if (ddlCategories9 != null)
            {
                ddlCategories9.SelectedValue = drv["DAY09"].ToString();
            }
            else { ddlCategories9.SelectedValue = "0"; } fill_cell(e, ddlCategories9, 49);
            if (ddlCategories10 != null)
            {
                ddlCategories10.SelectedValue = drv["DAY10"].ToString();
            }
            else { ddlCategories10.SelectedValue = "0"; } fill_cell(e, ddlCategories10, 50);
            if (ddlCategories11 != null)
            {
                ddlCategories11.SelectedValue = drv["DAY11"].ToString();
            }
            else { ddlCategories11.SelectedValue = "0"; } fill_cell(e, ddlCategories11, 51);
            if (ddlCategories2 != null)
            {
                ddlCategories12.SelectedValue = drv["DAY12"].ToString();
            }
            else { ddlCategories12.SelectedValue = "0"; } fill_cell(e, ddlCategories12, 52);
            if (ddlCategories13 != null)
            {
                ddlCategories13.SelectedValue = drv["DAY13"].ToString();
            }
            else { ddlCategories13.SelectedValue = "0"; } fill_cell(e, ddlCategories13, 53);
            if (ddlCategories14 != null)
            {
                ddlCategories14.SelectedValue = drv["DAY14"].ToString();
            }
            else { ddlCategories14.SelectedValue = "0"; } fill_cell(e, ddlCategories14, 54);
            if (ddlCategories15 != null)
            {
                ddlCategories15.SelectedValue = drv["DAY15"].ToString();
            }
            else { ddlCategories15.SelectedValue = "0"; } fill_cell(e, ddlCategories15, 55);
            if (ddlCategories16 != null)
            {
                ddlCategories16.SelectedValue = drv["DAY16"].ToString();
            }
            else { ddlCategories16.SelectedValue = "0"; } fill_cell(e, ddlCategories16, 56);
            if (ddlCategories17 != null)
            {
                ddlCategories17.SelectedValue = drv["DAY17"].ToString();
            }
            else { ddlCategories17.SelectedValue = "0"; } fill_cell(e, ddlCategories17, 57);
            if (ddlCategories18 != null)
            {
                ddlCategories18.SelectedValue = drv["DAY18"].ToString();
            }
            else { ddlCategories18.SelectedValue = "0"; } fill_cell(e, ddlCategories18, 58);
            if (ddlCategories19 != null)
            {
                ddlCategories19.SelectedValue = drv["DAY19"].ToString();
            }
            else { ddlCategories19.SelectedValue = "0"; } fill_cell(e, ddlCategories19, 59);
            if (ddlCategories20 != null)
            {
                ddlCategories20.SelectedValue = drv["DAY20"].ToString();
            }
            else { ddlCategories20.SelectedValue = "0"; } fill_cell(e, ddlCategories20, 60);
            if (ddlCategories21 != null)
            {
                ddlCategories21.SelectedValue = drv["DAY21"].ToString();
            }
            else { ddlCategories21.SelectedValue = "0"; } fill_cell(e, ddlCategories21, 61);
            if (ddlCategories22 != null)
            {
                ddlCategories22.SelectedValue = drv["DAY22"].ToString();
            }
            else { ddlCategories22.SelectedValue = "0"; } fill_cell(e, ddlCategories22, 62);
            if (ddlCategories23 != null)
            {
                ddlCategories23.SelectedValue = drv["DAY23"].ToString();
            }
            else { ddlCategories23.SelectedValue = "0"; } fill_cell(e, ddlCategories23, 63);
            if (ddlCategories24 != null)
            {
                ddlCategories24.SelectedValue = drv["DAY24"].ToString();
            }
            else { ddlCategories24.SelectedValue = "0"; } fill_cell(e, ddlCategories24, 64);
            if (ddlCategories25 != null)
            {
                ddlCategories25.SelectedValue = drv["DAY25"].ToString();
            }
            else { ddlCategories25.SelectedValue = "0"; } fill_cell(e, ddlCategories25, 65);
            if (ddlCategories26 != null)
            {
                ddlCategories26.SelectedValue = drv["DAY26"].ToString();
            }
            else { ddlCategories26.SelectedValue = "0"; } fill_cell(e, ddlCategories26, 66);
            if (ddlCategories27 != null)
            {
                ddlCategories27.SelectedValue = drv["DAY27"].ToString();
            }
            else { ddlCategories27.SelectedValue = "0"; } fill_cell(e, ddlCategories27, 67);
            if (ddlCategories28 != null)
            {
                ddlCategories28.SelectedValue = drv["DAY28"].ToString();
            }
            else { ddlCategories28.SelectedValue = "0"; } fill_cell(e, ddlCategories28, 68);
            if (ddlCategories29 != null)
            {
                ddlCategories29.SelectedValue = drv["DAY29"].ToString();
            }
            else { ddlCategories29.SelectedValue = "0"; } fill_cell(e, ddlCategories29, 69);
            if (ddlCategories30 != null)
            {
                ddlCategories30.SelectedValue = drv["DAY30"].ToString();
            }
            else { ddlCategories30.SelectedValue = "0"; } fill_cell(e, ddlCategories30, 70);
            if (ddlCategories31 != null)
            {
                ddlCategories31.SelectedValue = drv["DAY31"].ToString();
            }
            else { ddlCategories31.SelectedValue = "0"; } fill_cell(e, ddlCategories31, 71);

            //string temp = d1.getsinglestring("select OT from pay_client_master where client_code = '" + ddl_client.SelectedValue + "'");
            //if (temp == "0")
            //{
            //    TextBox txtothours = e.Row.FindControl("txt_ot_hours") as TextBox;
            //    txtothours.ReadOnly = true;
            //}
            //else
            //{
            //    TextBox txtothours = e.Row.FindControl("txt_ot_hours") as TextBox;
            //    txtothours.ReadOnly = false;
            //}
            //vikas 
            TextBox text1 = e.Row.FindControl("txt_ot_1") as TextBox;
            TextBox text2 = e.Row.FindControl("txt_ot_2") as TextBox;
            TextBox text3 = e.Row.FindControl("txt_ot_3") as TextBox;
            TextBox text4 = e.Row.FindControl("txt_ot_4") as TextBox;
            TextBox text5 = e.Row.FindControl("txt_ot_5") as TextBox;
            TextBox text6 = e.Row.FindControl("txt_ot_6") as TextBox;
            TextBox text7 = e.Row.FindControl("txt_ot_7") as TextBox;
            TextBox text8 = e.Row.FindControl("txt_ot_8") as TextBox;
            TextBox text9 = e.Row.FindControl("txt_ot_9") as TextBox;
            TextBox text10 = e.Row.FindControl("txt_ot_10") as TextBox;
            TextBox text11 = e.Row.FindControl("txt_ot_11") as TextBox;
            TextBox text12 = e.Row.FindControl("txt_ot_12") as TextBox;
            TextBox text13 = e.Row.FindControl("txt_ot_13") as TextBox;
            TextBox text14 = e.Row.FindControl("txt_ot_14") as TextBox;
            TextBox text15 = e.Row.FindControl("txt_ot_15") as TextBox;
            TextBox text16 = e.Row.FindControl("txt_ot_16") as TextBox;
            TextBox text17 = e.Row.FindControl("txt_ot_17") as TextBox;
            TextBox text18 = e.Row.FindControl("txt_ot_18") as TextBox;
            TextBox text19 = e.Row.FindControl("txt_ot_19") as TextBox;
            TextBox text20 = e.Row.FindControl("txt_ot_20") as TextBox;
            TextBox text21 = e.Row.FindControl("txt_ot_21") as TextBox;
            TextBox text22 = e.Row.FindControl("txt_ot_22") as TextBox;
            TextBox text23 = e.Row.FindControl("txt_ot_23") as TextBox;
            TextBox text24 = e.Row.FindControl("txt_ot_24") as TextBox;
            TextBox text25 = e.Row.FindControl("txt_ot_25") as TextBox;
            TextBox text26 = e.Row.FindControl("txt_ot_26") as TextBox;
            TextBox text27 = e.Row.FindControl("txt_ot_27") as TextBox;
            TextBox text28 = e.Row.FindControl("txt_ot_28") as TextBox;
            TextBox text29 = e.Row.FindControl("txt_ot_29") as TextBox;
            TextBox text30 = e.Row.FindControl("txt_ot_30") as TextBox;
            TextBox text31 = e.Row.FindControl("txt_ot_31") as TextBox;
            if (ot_applicable == "2")
            {
                if (text1 != null)
                {
                    //ot_ddlCategories1.Visible = false;
                    text1.Text = drv["OT_DAILY_DAY01"].ToString();
                }
                else { text1.Text = "0"; }
                if (text2 != null)
                {
                    //ot_ddlCategories1.Visible = false;
                    text2.Text = drv["OT_DAILY_DAY02"].ToString();
                }
                else { text2.Text = "0"; }
                if (text3 != null)
                {
                    //ot_ddlCategories1.Visible = false;
                    text3.Text = drv["OT_DAILY_DAY03"].ToString();
                }
                else { text3.Text = "0"; }

                if (text4 != null)
                {
                    //ot_ddlCategories1.Visible = false;
                    text4.Text = drv["OT_DAILY_DAY04"].ToString();
                }
                else { text4.Text = "0"; }
                if (text5 != null)
                {
                    text5.Text = drv["OT_DAILY_DAY05"].ToString();
                }
                else { text5.Text = "0"; }

                if (text6 != null)
                {
                    text6.Text = drv["OT_DAILY_DAY06"].ToString();
                }
                else { text6.Text = "0"; }
                if (text7 != null)
                {
                    text7.Text = drv["OT_DAILY_DAY07"].ToString();
                }
                else { text7.Text = "0"; }
                if (text8 != null)
                {
                    text8.Text = drv["OT_DAILY_DAY08"].ToString();
                }
                else { text8.Text = "0"; }
                if (text9 != null)
                {
                    text9.Text = drv["OT_DAILY_DAY09"].ToString();
                }
                else { text9.Text = "0"; }
                if (text10 != null)
                {
                    text10.Text = drv["OT_DAILY_DAY10"].ToString();
                }
                else { text10.Text = "0"; }
                if (text11 != null)
                {
                    text11.Text = drv["OT_DAILY_DAY11"].ToString();
                }
                else { text11.Text = "0"; }
                if (text12 != null)
                {
                    text12.Text = drv["OT_DAILY_DAY12"].ToString();
                }
                else { text12.Text = "0"; }
                if (text13 != null)
                {
                    text13.Text = drv["OT_DAILY_DAY13"].ToString();
                }
                else { text13.Text = "0"; }
                if (text14 != null)
                {
                    text14.Text = drv["OT_DAILY_DAY14"].ToString();
                }
                else { text14.Text = "0"; }
                if (text15 != null)
                {
                    text15.Text = drv["OT_DAILY_DAY15"].ToString();
                }
                else { text15.Text = "0"; }
                if (text16 != null)
                {
                    text16.Text = drv["OT_DAILY_DAY16"].ToString();
                }
                else { text16.Text = "0"; }
                if (text17 != null)
                {
                    text17.Text = drv["OT_DAILY_DAY17"].ToString();
                }
                else { text17.Text = "0"; }
                if (text18 != null)
                {
                    text18.Text = drv["OT_DAILY_DAY18"].ToString();
                }
                else { text18.Text = "0"; }
                if (text19 != null)
                {
                    text19.Text = drv["OT_DAILY_DAY19"].ToString();
                }
                else { text19.Text = "0"; }
                if (text20 != null)
                {
                    text20.Text = drv["OT_DAILY_DAY20"].ToString();
                }
                else { text20.Text = "0"; }
                if (text21 != null)
                {
                    text21.Text = drv["OT_DAILY_DAY21"].ToString();
                }
                else { text21.Text = "0"; }
                if (text22 != null)
                {
                    text22.Text = drv["OT_DAILY_DAY22"].ToString();
                }
                else { text22.Text = "0"; }
                if (text23 != null)
                {
                    text23.Text = drv["OT_DAILY_DAY23"].ToString();
                }
                else { text23.Text = "0"; }
                if (text24 != null)
                {
                    text24.Text = drv["OT_DAILY_DAY24"].ToString();
                }
                else { text24.Text = "0"; }
                if (text25 != null)
                {
                    text25.Text = drv["OT_DAILY_DAY25"].ToString();
                }
                else { text25.Text = "0"; }
                if (text26 != null)
                {
                    text26.Text = drv["OT_DAILY_DAY26"].ToString();
                }
                else { text26.Text = "0"; }
                if (text27 != null)
                {
                    text27.Text = drv["OT_DAILY_DAY27"].ToString();
                }
                else { text27.Text = "0"; }
                if (text28 != null)
                {
                    text28.Text = drv["OT_DAILY_DAY28"].ToString();
                }
                else { text28.Text = "0"; }
                if (text29 != null)
                {
                    text29.Text = drv["OT_DAILY_DAY29"].ToString();
                }
                else { text29.Text = "0"; }
                if (text30 != null)
                {
                    text30.Text = drv["OT_DAILY_DAY30"].ToString();
                }
                else { text30.Text = "0"; }
                if (text31 != null)
                {
                    text31.Text = drv["OT_DAILY_DAY31"].ToString();
                }
                else { text31.Text = "0"; }
            }

            //vikas end 

            DropDownList ot_ddlCategories1 = e.Row.FindControl("OT_DropDownList1") as DropDownList;
            DropDownList ot_ddlCategories2 = e.Row.FindControl("OT_DropDownList2") as DropDownList;
            DropDownList ot_ddlCategories3 = e.Row.FindControl("OT_DropDownList3") as DropDownList;
            DropDownList ot_ddlCategories4 = e.Row.FindControl("OT_DropDownList4") as DropDownList;
            DropDownList ot_ddlCategories5 = e.Row.FindControl("OT_DropDownList5") as DropDownList;
            DropDownList ot_ddlCategories6 = e.Row.FindControl("OT_DropDownList6") as DropDownList;
            DropDownList ot_ddlCategories7 = e.Row.FindControl("OT_DropDownList7") as DropDownList;
            DropDownList ot_ddlCategories8 = e.Row.FindControl("OT_DropDownList8") as DropDownList;
            DropDownList ot_ddlCategories9 = e.Row.FindControl("OT_DropDownList9") as DropDownList;
            DropDownList ot_ddlCategories10 = e.Row.FindControl("OT_DropDownList10") as DropDownList;
            DropDownList ot_ddlCategories11 = e.Row.FindControl("OT_DropDownList11") as DropDownList;
            DropDownList ot_ddlCategories12 = e.Row.FindControl("OT_DropDownList12") as DropDownList;
            DropDownList ot_ddlCategories13 = e.Row.FindControl("OT_DropDownList13") as DropDownList;
            DropDownList ot_ddlCategories14 = e.Row.FindControl("OT_DropDownList14") as DropDownList;
            DropDownList ot_ddlCategories15 = e.Row.FindControl("OT_DropDownList15") as DropDownList;
            DropDownList ot_ddlCategories16 = e.Row.FindControl("OT_DropDownList16") as DropDownList;
            DropDownList ot_ddlCategories17 = e.Row.FindControl("OT_DropDownList17") as DropDownList;
            DropDownList ot_ddlCategories18 = e.Row.FindControl("OT_DropDownList18") as DropDownList;
            DropDownList ot_ddlCategories19 = e.Row.FindControl("OT_DropDownList19") as DropDownList;
            DropDownList ot_ddlCategories20 = e.Row.FindControl("OT_DropDownList20") as DropDownList;
            DropDownList ot_ddlCategories21 = e.Row.FindControl("OT_DropDownList21") as DropDownList;
            DropDownList ot_ddlCategories22 = e.Row.FindControl("OT_DropDownList22") as DropDownList;
            DropDownList ot_ddlCategories23 = e.Row.FindControl("OT_DropDownList23") as DropDownList;
            DropDownList ot_ddlCategories24 = e.Row.FindControl("OT_DropDownList24") as DropDownList;
            DropDownList ot_ddlCategories25 = e.Row.FindControl("OT_DropDownList25") as DropDownList;
            DropDownList ot_ddlCategories26 = e.Row.FindControl("OT_DropDownList26") as DropDownList;
            DropDownList ot_ddlCategories27 = e.Row.FindControl("OT_DropDownList27") as DropDownList;
            DropDownList ot_ddlCategories28 = e.Row.FindControl("OT_DropDownList28") as DropDownList;
            DropDownList ot_ddlCategories29 = e.Row.FindControl("OT_DropDownList29") as DropDownList;
            DropDownList ot_ddlCategories30 = e.Row.FindControl("OT_DropDownList30") as DropDownList;
            DropDownList ot_ddlCategories31 = e.Row.FindControl("OT_DropDownList31") as DropDownList;
            if (ot_applicable == "1")
            {
                if (ot_ddlCategories1 != null)
                {
                    //ot_ddlCategories1.Visible = false;
                    ot_ddlCategories1.SelectedValue = drv["OT_DAY01"].ToString();
                }
                else { ot_ddlCategories1.SelectedValue = "0"; }

                if (ot_ddlCategories2 != null)
                {
                    ot_ddlCategories2.SelectedValue = drv["OT_DAY02"].ToString();
                }
                else { ot_ddlCategories2.SelectedValue = "0"; }

                if (ot_ddlCategories3 != null)
                {
                    ot_ddlCategories3.SelectedValue = drv["OT_DAY03"].ToString();
                }
                else { ot_ddlCategories3.SelectedValue = "0"; }

                if (ot_ddlCategories4 != null)
                {
                    ot_ddlCategories4.SelectedValue = drv["OT_DAY04"].ToString();
                }
                else { ot_ddlCategories4.SelectedValue = "0"; }
                if (ot_ddlCategories5 != null)
                {
                    ot_ddlCategories5.SelectedValue = drv["OT_DAY05"].ToString();
                }
                else { ot_ddlCategories5.SelectedValue = "0"; }
                if (ot_ddlCategories6 != null)
                {
                    ot_ddlCategories6.SelectedValue = drv["OT_DAY06"].ToString();
                }
                else { ot_ddlCategories6.SelectedValue = "0"; }
                if (ot_ddlCategories7 != null)
                {
                    ot_ddlCategories7.SelectedValue = drv["OT_DAY07"].ToString();
                }
                else { ot_ddlCategories7.SelectedValue = "0"; }
                if (ot_ddlCategories8 != null)
                {
                    ot_ddlCategories8.SelectedValue = drv["OT_DAY08"].ToString();
                }
                else { ot_ddlCategories8.SelectedValue = "0"; }
                if (ot_ddlCategories9 != null)
                {
                    ot_ddlCategories9.SelectedValue = drv["OT_DAY09"].ToString();
                }
                else { ot_ddlCategories9.SelectedValue = "0"; }

                if (ot_ddlCategories10 != null)
                {
                    ot_ddlCategories10.SelectedValue = drv["OT_DAY10"].ToString();
                }
                else { ot_ddlCategories10.SelectedValue = "0"; }

                if (ot_ddlCategories11 != null)
                {
                    ot_ddlCategories11.SelectedValue = drv["OT_DAY11"].ToString();
                }
                else { ot_ddlCategories11.SelectedValue = "0"; }

                if (ot_ddlCategories2 != null)
                {
                    ot_ddlCategories12.SelectedValue = drv["OT_DAY12"].ToString();
                }
                else { ot_ddlCategories12.SelectedValue = "0"; }

                if (ot_ddlCategories13 != null)
                {
                    ot_ddlCategories13.SelectedValue = drv["OT_DAY13"].ToString();
                }
                else { ot_ddlCategories13.SelectedValue = "0"; }
                if (ot_ddlCategories14 != null)
                {
                    ot_ddlCategories14.SelectedValue = drv["OT_DAY14"].ToString();
                }
                else { ot_ddlCategories14.SelectedValue = "0"; }
                if (ot_ddlCategories15 != null)
                {
                    ot_ddlCategories15.SelectedValue = drv["OT_DAY15"].ToString();
                }
                else { ot_ddlCategories15.SelectedValue = "0"; }
                if (ot_ddlCategories16 != null)
                {
                    ot_ddlCategories16.SelectedValue = drv["OT_DAY16"].ToString();
                }
                else { ot_ddlCategories16.SelectedValue = "0"; }
                if (ot_ddlCategories17 != null)
                {
                    ot_ddlCategories17.SelectedValue = drv["OT_DAY17"].ToString();
                }
                else { ot_ddlCategories17.SelectedValue = "0"; }

                if (ot_ddlCategories18 != null)
                {
                    ot_ddlCategories18.SelectedValue = drv["OT_DAY18"].ToString();
                }
                else { ot_ddlCategories18.SelectedValue = "0"; }

                if (ot_ddlCategories19 != null)
                {
                    ot_ddlCategories19.SelectedValue = drv["OT_DAY19"].ToString();
                }
                else { ot_ddlCategories19.SelectedValue = "0"; }
                if (ot_ddlCategories20 != null)
                {
                    ot_ddlCategories20.SelectedValue = drv["OT_DAY20"].ToString();
                }
                else { ot_ddlCategories20.SelectedValue = "0"; }

                if (ot_ddlCategories21 != null)
                {
                    ot_ddlCategories21.SelectedValue = drv["OT_DAY21"].ToString();
                }
                else { ot_ddlCategories21.SelectedValue = "0"; }

                if (ot_ddlCategories22 != null)
                {
                    ot_ddlCategories22.SelectedValue = drv["OT_DAY22"].ToString();
                }
                else { ot_ddlCategories22.SelectedValue = "0"; }

                if (ot_ddlCategories23 != null)
                {
                    ot_ddlCategories23.SelectedValue = drv["OT_DAY23"].ToString();
                }
                else { ot_ddlCategories23.SelectedValue = "0"; }

                if (ot_ddlCategories24 != null)
                {
                    ot_ddlCategories24.SelectedValue = drv["OT_DAY24"].ToString();
                }
                else { ot_ddlCategories24.SelectedValue = "0"; }

                if (ot_ddlCategories25 != null)
                {
                    ot_ddlCategories25.SelectedValue = drv["OT_DAY25"].ToString();
                }
                else { ot_ddlCategories25.SelectedValue = "0"; }

                if (ot_ddlCategories26 != null)
                {
                    ot_ddlCategories26.SelectedValue = drv["OT_DAY26"].ToString();
                }
                else { ot_ddlCategories26.SelectedValue = "0"; }
                if (ot_ddlCategories27 != null)
                {
                    ot_ddlCategories27.SelectedValue = drv["OT_DAY27"].ToString();
                }
                else { ot_ddlCategories27.SelectedValue = "0"; }
                if (ot_ddlCategories28 != null)
                {
                    ot_ddlCategories28.SelectedValue = drv["OT_DAY28"].ToString();
                }
                else { ot_ddlCategories28.SelectedValue = "0"; }
                if (ot_ddlCategories29 != null)
                {
                    ot_ddlCategories29.SelectedValue = drv["OT_DAY29"].ToString();
                }
                else { ot_ddlCategories29.SelectedValue = "0"; }
                if (ot_ddlCategories30 != null)
                {
                    ot_ddlCategories30.SelectedValue = drv["OT_DAY30"].ToString();
                }
                else { ot_ddlCategories30.SelectedValue = "0"; }
                if (ot_ddlCategories31 != null)
                {
                    ot_ddlCategories31.SelectedValue = drv["OT_DAY31"].ToString();
                }
                else { ot_ddlCategories31.SelectedValue = "0"; }

            }
            else
            {
                ot_ddlCategories1.Visible = false;
                ot_ddlCategories2.Visible = false;
                ot_ddlCategories3.Visible = false;
                ot_ddlCategories4.Visible = false;
                ot_ddlCategories5.Visible = false;
                ot_ddlCategories6.Visible = false;
                ot_ddlCategories7.Visible = false;
                ot_ddlCategories8.Visible = false;
                ot_ddlCategories9.Visible = false;
                ot_ddlCategories10.Visible = false;
                ot_ddlCategories11.Visible = false;
                ot_ddlCategories12.Visible = false;
                ot_ddlCategories13.Visible = false;
                ot_ddlCategories14.Visible = false;
                ot_ddlCategories15.Visible = false;
                ot_ddlCategories16.Visible = false;
                ot_ddlCategories17.Visible = false;
                ot_ddlCategories18.Visible = false;
                ot_ddlCategories19.Visible = false;
                ot_ddlCategories20.Visible = false;
                ot_ddlCategories21.Visible = false;
                ot_ddlCategories22.Visible = false;
                ot_ddlCategories23.Visible = false;
                ot_ddlCategories24.Visible = false;
                ot_ddlCategories25.Visible = false;
                ot_ddlCategories26.Visible = false;
                ot_ddlCategories27.Visible = false;
                ot_ddlCategories28.Visible = false;
                ot_ddlCategories29.Visible = false;
                ot_ddlCategories30.Visible = false;
                ot_ddlCategories31.Visible = false;

            }
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[10].Visible = false;

        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;

        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
        e.Row.Cells[24].Visible = false;
        e.Row.Cells[25].Visible = false;
        e.Row.Cells[26].Visible = false;
        e.Row.Cells[27].Visible = false;
        e.Row.Cells[28].Visible = false;
        e.Row.Cells[29].Visible = false;
        e.Row.Cells[30].Visible = false;
        e.Row.Cells[31].Visible = false;
        e.Row.Cells[32].Visible = false;
        e.Row.Cells[33].Visible = false;
        e.Row.Cells[34].Visible = false;
        e.Row.Cells[35].Visible = false;
        e.Row.Cells[36].Visible = false;
        e.Row.Cells[37].Visible = false;
        e.Row.Cells[38].Visible = false;
        e.Row.Cells[39].Visible = false;
        e.Row.Cells[40].Visible = false;

        int days = 0;
        if (start_date_common == "1")
        {
            days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value));
        }
        else
        {
            if ((int.Parse(hidden_month.Value) - 1) == 0)
            {
                days = DateTime.DaysInMonth(int.Parse(hidden_year.Value) - 1, 12);
            }
            else
            {
                days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value) - 1);
            }
        }

        if (days == 30)
        {
            e.Row.Cells[71].Visible = false;
        }
        else if (days == 29)
        {
            e.Row.Cells[70].Visible = false;
            e.Row.Cells[71].Visible = false;
        }
        else if (days == 28)
        {
            e.Row.Cells[69].Visible = false;
            e.Row.Cells[70].Visible = false;
            e.Row.Cells[71].Visible = false;
        }


    }
    private void loadheaders(int month, int year, int datecommon)
    {
        if (datecommon == 1)
        {
            DateTime dt = new DateTime(year, month, 1);
            gv_attendance.HeaderRow.Cells[41].Text = "1 " + dt.AddDays(0).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[42].Text = "2 " + dt.AddDays(1).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[43].Text = "3 " + dt.AddDays(2).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[44].Text = "4 " + dt.AddDays(3).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[45].Text = "5 " + dt.AddDays(4).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[46].Text = "6 " + dt.AddDays(5).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[47].Text = "7 " + dt.AddDays(6).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[48].Text = "8 " + dt.AddDays(7).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[49].Text = "9 " + dt.AddDays(8).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[50].Text = "10 " + dt.AddDays(9).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[51].Text = "11 " + dt.AddDays(10).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[52].Text = "12 " + dt.AddDays(11).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[53].Text = "13 " + dt.AddDays(12).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[54].Text = "14 " + dt.AddDays(13).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[55].Text = "15 " + dt.AddDays(14).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[56].Text = "16 " + dt.AddDays(15).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[57].Text = "17 " + dt.AddDays(16).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[58].Text = "18 " + dt.AddDays(17).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[59].Text = "19 " + dt.AddDays(18).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[60].Text = "20 " + dt.AddDays(19).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[61].Text = "21 " + dt.AddDays(20).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[62].Text = "22 " + dt.AddDays(21).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[63].Text = "23 " + dt.AddDays(22).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[64].Text = "24 " + dt.AddDays(23).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[65].Text = "25 " + dt.AddDays(24).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[66].Text = "26 " + dt.AddDays(25).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[67].Text = "27 " + dt.AddDays(26).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[68].Text = "28 " + dt.AddDays(27).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[69].Text = "29 " + dt.AddDays(28).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[70].Text = "30 " + dt.AddDays(29).DayOfWeek.ToString().Substring(0, 3).ToUpper();
            gv_attendance.HeaderRow.Cells[71].Text = "31 " + dt.AddDays(30).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        }
        else
        {
            int month1 = 0, year1 = 0, j = 41, k = datecommon;
            month1 = month;
            year1 = year;
            --month1;
            if (month1 == 0) { month1 = 12; year1 = year1 - 1; }
            int daysinmonth = DateTime.DaysInMonth(year1, month1);
            DateTime dt = new DateTime(year1, month1, 1);
            for (int i = 0; daysinmonth >= (i + datecommon); i++)
            {
                gv_attendance.HeaderRow.Cells[j++].Text = (k) + " " + dt.AddDays(k - 1).DayOfWeek.ToString().Substring(0, 3).ToUpper();
                k = k + 1;
            }
            k = 1;
            dt = new DateTime(year, month, 1);
            for (int i = 1; (datecommon) > i; i++)
            {
                gv_attendance.HeaderRow.Cells[j++].Text = k + " " + dt.AddDays(k - 1).DayOfWeek.ToString().Substring(0, 3).ToUpper();
                k = k + 1;
            }
        }
    }
    //protected void shiftcalendar_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //GridViewRow row = ((Control)sender).NamingContainer as GridViewRow;
    //    //ViewState["EMP_CODE"] = ViewState["EMP_CODE"].ToString() + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + ",";
    //    //DropDownList ddl = (DropDownList)sender;
    //    //string id = ddl.ID;
    //}
    int CountDay(int month, int year, int counter)
    {
        //d1.getsinglestring("SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '"+ddl_unitcode.SelectedValue+"' AND month = '"+hidden_month.Value+"' AND year = '"+hidden_year.Value+"'  ");
        //string start_date_common = get_start_date();
        //string end_date_common = get_end_date();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status ='0'";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }





        string start_date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master . billing_unit_code  =  pay_unit_master . unit_code  AND  pay_billing_master . comp_code  =  pay_unit_master . comp_code  WHERE pay_unit_master . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");
        string end_date_common = d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1), (SELECT end_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master . billing_unit_code  =  pay_unit_master . unit_code  AND  pay_billing_master . comp_code  =  pay_unit_master . comp_code  WHERE pay_unit_master . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code IN(" + unit_code + ") limit 1))");

        int NoOfSunday = 0;

        var firstDay = (dynamic)null;

        if (start_date_common != "1")
        {
            // if ((month - 1) == 0) {
            firstDay = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            //}
            //else{
            //firstDay = new DateTime(year, (month-1), int.Parse(start_date_common));
            //}
        }
        else
        {

            firstDay = new DateTime(year, month, 1);
        }

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


        int NumOfDay = 0;
        if (start_date_common != "1")
        {
            //int year1 = year;
            //if (month == 12)
            //{
            //    year1 = year - 1;
            //}
            //else { year1 = year; }
            //for (int j = 0; j <= (d.CountDay((month - 1), year1, 1) - 1); j++)
            //{
            //    var date = new DateTime(year1,(month - 1),int.Parse(start_date_common));

            //    var dayscount = date.AddDays(j);
            //}

            var start_date = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            var end_date = new DateTime(year, (month), int.Parse(end_date_common));
            if ((end_date.Date - start_date.Date).Days == 28)
            {
                day31 = day30;
            }
            if ((end_date.Date - start_date.Date).Days + 1 == 30)
            {
                day31 = day30;
            }
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

            NumOfDay = (end_date.Date - start_date.Date).Days;
            NumOfDay = ++NumOfDay;
        }
        else
        {
            NumOfDay = DateTime.DaysInMonth(year, month);
        }

        //if (counter == 2)
        //{
        //    if (NumOfDay.Equals(32))
        //    {
        //        NumOfDay = 31;
        //    }
        //    return NumOfDay;
        //}
        //if (counter == 1)
        //{
        //    return NumOfDay - NoOfSunday;
        //}
        //else
        //{ return NoOfSunday; }
        if (counter == 1)
        {//calendar days
            return NumOfDay;
        }
        else
        { //working days
            return NumOfDay - NoOfSunday;
        }
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

        //txttodate.Text = "";
        if (ddl_client.SelectedValue != "Select")
        {

            //State
            ddl_state.Items.Clear();
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            //comment 30/09 for branch close MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='"+Session["COMP_CODE"].ToString()+"' and client_code = '"+ddl_client.SelectedValue+"' and EMP_CODE in ("+Session["REPORTING_EMP_SERIES"].ToString()+") ", d.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") and unit_code in(select unit_code from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y'))) order by state_name ", d.con);
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
                ddl_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_unitcode.Items.Clear();
            }
            Notification_panel.Visible = false;
            Panel1.Visible = false;

        }

        // ddl_state_SelectedIndexChanged(null, null);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        Session["SalarySteps"] = "";
        Session["SalaryProcessMonth"] = "";
        Session["SalaryProcessYear"] = "";
        Session["SalaryProcessUnit"] = "";
        Response.Redirect("Home.aspx");
    }
    protected void btn_attendance_Click(object sender, EventArgs e)
    {
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' AND  branch_status  = '0'");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }



        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");


        string flag = d.getsinglestring("select flag from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and  unit_code IN (" + unit_code + ") and client_code='" + ddl_client.SelectedValue + "'");

        if (flag == "0" || flag == "1" || flag == "")
        {
            string sql = "", pay_attendance_muster = "pay_attendance_muster";
            string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-01','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-01'), '%D %b %Y'))) as fromtodate";
            string start_date_common = get_start_date();
            string btn_attendance = ""+2+"";
            int month1 = int.Parse(hidden_month.Value);
            int year1 = int.Parse(hidden_year.Value);
            int monthdays = DateTime.DaysInMonth(year1, month1);
            int end_date1 = monthdays;
            int i = 0;
            //rahul  start
            //string attendance = " and pay_attendance_muster.tot_days_present > 0";

            
            //string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_client.SelectedValue.ToString() + "'");
            //string tempflag12 = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");

            string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "'");


            //if (tempflag == "yes")
            //{
            //    attendance = "";
            //}
            //end 
            if (ot_applicable == "1")
            {
                if (start_date_common != "" && start_date_common != "1")
                {
                    d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(start_date_common), ddl_state.SelectedValue);
                    //pay_attendance_muster = "pay_attendance_muster_diff pay_attendance_muster";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + ((int.Parse(hidden_month.Value) - 1) == 0 ? (int.Parse(hidden_year.Value) - 1).ToString() : hidden_year.Value) + "-" + ((int.Parse(hidden_month.Value) - 1) == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    //string ot_emp_code = d.getsinglestring("select emp_code from pay_ot_muster where emp_CODE='" + emp_code + "' AND MONTH = " + (int.Parse(hidden_month.Value) - 1) + " AND YEAR= " + hidden_year.Value);

                    int month = int.Parse(hidden_month.Value) - 1;
                    int year = int.Parse(hidden_year.Value);
                    if (month == 0) { month = 12; year = year - 1; }

                    // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 3) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code INNER JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR left join pay_attendance_muster t2 on pay_attendance_muster.Year = t2.year and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) - 1) + " left outer join pay_ot_muster t3 on pay_ot_muster.YEAR = t3.YEAR and pay_ot_muster.UNIT_CODE = t3.UNIT_CODE and pay_ot_muster.EMP_CODE = t3.EMP_CODE and pay_ot_muster.COMP_CODE = t3.COMP_CODE AND t3.month = " + (int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_ot_muster.month = '" + hidden_month.Value + "'  and pay_attendance_muster.tot_days_present > 0  and (str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d') >= pay_employee_master.joining_date and if(left_date is null,now(),left_date) >= str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value) - 1) + "-" + start_date_common + "','%Y-%m-%d')) ORDER BY pay_employee_master.EMP_CODE";

                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 3, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master LEFT JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code  AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "'   AND pay_attendance_muster.tot_days_present > 0  INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code   left JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE  INNER JOIN pay_company_master ON pay_unit_master.comp_code = pay_company_master.comp_code  LEFT JOIN pay_ot_muster ON  pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code  AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE  AND pay_attendance_muster.MONTH = pay_ot_muster.MONTH  AND pay_attendance_muster.YEAR = pay_ot_muster.YEAR  AND pay_ot_muster.month =  '" + hidden_month.Value + "'  LEFT JOIN pay_attendance_muster t2   ON " + year + "= t2.year AND pay_company_master.COMP_CODE = t2.COMP_CODE  AND pay_unit_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE  AND t2.month ='" + month + "'  AND t2.tot_days_present > 0 LEFT OUTER JOIN pay_ot_muster t3   ON " + year + " = t3.YEAR  AND pay_unit_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_company_master.COMP_CODE = t3.COMP_CODE   AND t3.month = '" + month + "' WHERE pay_company_master.comp_code =  '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.unit_code IN( " + unit_code + ") AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                }
                else
                {
                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31,OT_DAY01 , OT_DAY02 , OT_DAY03 , OT_DAY04 , OT_DAY05 , OT_DAY06 , OT_DAY07 , OT_DAY08 , OT_DAY09 , OT_DAY10 , OT_DAY11 , OT_DAY12 , OT_DAY13 , OT_DAY14 , OT_DAY15 , OT_DAY16 , OT_DAY17 , OT_DAY18 , OT_DAY19 , OT_DAY20 , OT_DAY21 , OT_DAY22 , OT_DAY23 , OT_DAY24 , OT_DAY25 , OT_DAY26 , OT_DAY27 , OT_DAY28 , OT_DAY29 , OT_DAY30 , OT_DAY31,TOT_OT, pay_attendance_muster.tot_days_present,pay_attendance_muster.tot_days_absent AS 'absent', DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) AS 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code LEFT JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN( " + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                }

                i = 1;
            }
            else
            {

                if (start_date_common != "" && start_date_common != "1")
                {
                    d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(start_date_common), ddl_state.SelectedValue);
                    //pay_attendance_muster = "pay_attendance_muster_diff pay_attendance_muster";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value) - 1) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    if (tempflag == "yes")
                    {
                        sql = "";
                        ot_flag1 = 1;
                        string get_otday = d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 4, 1);
                        if (!get_otday.Contains("OT_DAILY_DAY31"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY31',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY30"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAY30',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY29"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY29',";
                        }
                        // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date," + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " " + get_otday + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno,  IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0)  AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " LEFT JOIN `pay_daily_ot_muster` t4 ON `t4`.`year` = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `t4`.`month` = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date," + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " " + get_otday + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno, (IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0)+IFNULL(`t4`.`TOTAL_OT`, 0))  AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " LEFT JOIN `pay_daily_ot_muster` t4 ON `t4`.`year` = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `t4`.`month` = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' group by pay_employee_master.emp_code ORDER BY pay_employee_master.emp_name";
                    }
                    else
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + " and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                    
                    }
               
                
                } 
                    // attendaces 21-20 cycle end and 1-31 days satrt
                else
                {
                    //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + end_date1 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                   
                   // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        //if (((Session["comp_code"].ToString() == "C01") && (ddl_unitcode.SelectedValue == "U951")) || ((Session["comp_code"].ToString() == "C02") && (ddl_unitcode.SelectedValue == "U1036")))
                    if (tempflag == "yes")
                    {
                        sql = "";
                        ot_flag1 = 1;
                        //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno,SUBSTR(sec_to_time( OT_DAILY_DAY01 ),1,8) as ot_daily_day01,SUBSTR(sec_to_time( OT_DAILY_DAY02 ),1,8) as ot_daily_day02,SUBSTR(sec_to_time( OT_DAILY_DAY03 ),1,8) as ot_daily_day03,SUBSTR(sec_to_time( OT_DAILY_DAY04 ),1,8) as ot_daily_day04,SUBSTR(sec_to_time( OT_DAILY_DAY05 ),1,8) as ot_daily_day05,SUBSTR(sec_to_time( OT_DAILY_DAY06 ),1,8) as ot_daily_day06,SUBSTR(sec_to_time( OT_DAILY_DAY07 ),1,8) as ot_daily_day07,SUBSTR(sec_to_time( OT_DAILY_DAY08 ),1,8) as ot_daily_day08,SUBSTR(sec_to_time( OT_DAILY_DAY09 ),1,8) as ot_daily_day09,SUBSTR(sec_to_time( OT_DAILY_DAY10 ),1,8) as ot_daily_day10,SUBSTR(sec_to_time( OT_DAILY_DAY11 ),1,8) as ot_daily_day11,SUBSTR(sec_to_time( OT_DAILY_DAY12 ),1,8) as ot_daily_day12,SUBSTR(sec_to_time( OT_DAILY_DAY13 ),1,8) as ot_daily_day13,SUBSTR(sec_to_time( OT_DAILY_DAY14 ),1,8) as ot_daily_day14,SUBSTR(sec_to_time( OT_DAILY_DAY15 ),1,8) as ot_daily_day15,SUBSTR(sec_to_time( OT_DAILY_DAY16 ),1,8) as ot_daily_day16,SUBSTR(sec_to_time( OT_DAILY_DAY17 ),1,8) as ot_daily_day17,SUBSTR(sec_to_time( OT_DAILY_DAY18 ),1,8) as ot_daily_day18,SUBSTR(sec_to_time( OT_DAILY_DAY19 ),1,8) as ot_daily_day19,SUBSTR(sec_to_time( OT_DAILY_DAY20 ),1,8) as ot_daily_day20,SUBSTR(sec_to_time( OT_DAILY_DAY21 ),1,8) as ot_daily_day21,SUBSTR(sec_to_time( OT_DAILY_DAY22 ),1,8) as ot_daily_day22,SUBSTR(sec_to_time( OT_DAILY_DAY23 ),1,8) as ot_daily_day23,SUBSTR(sec_to_time( OT_DAILY_DAY24 ),1,8) as ot_daily_day24,SUBSTR(sec_to_time( OT_DAILY_DAY25 ),1,8) as ot_daily_day25,SUBSTR(sec_to_time( OT_DAILY_DAY26 ),1,8) as ot_daily_day26,SUBSTR(sec_to_time( OT_DAILY_DAY27 ),1,8) as ot_daily_day27,SUBSTR(sec_to_time( OT_DAILY_DAY28 ),1,8) as ot_daily_day28,SUBSTR(sec_to_time( OT_DAILY_DAY29 ),1,8) as ot_daily_day29,SUBSTR(sec_to_time( OT_DAILY_DAY30 ),1,8) as ot_daily_day30,SUBSTR(sec_to_time( OT_DAILY_DAY31 ),1,8) as ot_daily_day31,SUBSTR(sec_to_time(  TOTAL_OT  ),1,8) as  TOTAL_OT   from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        sql = "SELECT (SELECT `client_name` FROM `pay_client_master` WHERE `pay_client_master`.`client_code` = `pay_unit_master`.`client_code`) AS 'client_name', `pay_unit_master`.`client_code`, `pay_unit_master`.`comp_code`, CONCAT(`pay_unit_master`.`unit_add1`, ' , ', `pay_unit_master`.`unit_city`, ' , ', `pay_unit_master`.`state_name`) AS 'location', `pay_unit_master`.`client_branch_code`, `pay_unit_master`.`unit_add2`, " + daterange + ", `pay_company_master`.`company_name`, `pay_company_master`.`address1`, `pay_company_master`.`PF_REG_NO`, `pay_company_master`.`company_pan_no`, (SELECT `FIELD3` FROM `pay_zone_master` WHERE `comp_code` = `pay_company_master`.`comp_code` AND `type` = 'ESIC' AND `Field1` = `pay_company_master`.`state`) AS 'ESIC', (SELECT `FIELD2` FROM `pay_zone_master` WHERE `comp_code` = `pay_company_master`.`comp_code` AND `type` = 'GST' AND `client_Code` IS NULL) AS 'GST', `pay_employee_master`.`emp_name`, `pay_grade_master`.`grade_desc`, DATE_FORMAT(`pay_employee_master`.`joining_date`, '%d-%m-%Y') AS 'joining_date', CASE WHEN `DAY01` = '0' THEN 'A' ELSE `DAY01` END AS 'DAY01', CASE WHEN `DAY02` = '0' THEN 'A' ELSE `DAY02` END AS 'DAY02', CASE WHEN `DAY03` = '0' THEN 'A' ELSE `DAY03` END AS 'DAY03', CASE WHEN `DAY04` = '0' THEN 'A' ELSE `DAY04` END AS 'DAY04', CASE WHEN `DAY05` = '0' THEN 'A' ELSE `DAY05` END AS 'DAY05', CASE WHEN `DAY06` = '0' THEN 'A' ELSE `DAY06` END AS 'DAY06', CASE WHEN `DAY07` = '0' THEN 'A' ELSE `DAY07` END AS 'DAY07', CASE WHEN `DAY08` = '0' THEN 'A' ELSE `DAY08` END AS 'DAY08', CASE WHEN `DAY09` = '0' THEN 'A' ELSE `DAY09` END AS 'DAY09', CASE WHEN `DAY10` = '0' THEN 'A' ELSE `DAY10` END AS 'DAY10', CASE WHEN `DAY11` = '0' THEN 'A' ELSE `DAY11` END AS 'DAY11', CASE WHEN `DAY12` = '0' THEN 'A' ELSE `DAY12` END AS 'DAY12', CASE WHEN `DAY13` = '0' THEN 'A' ELSE `DAY13` END AS 'DAY13', CASE WHEN `DAY14` = '0' THEN 'A' ELSE `DAY14` END AS 'DAY14', CASE WHEN `DAY15` = '0' THEN 'A' ELSE `DAY15` END AS 'DAY15', CASE WHEN `DAY16` = '0' THEN 'A' ELSE `DAY16` END AS 'DAY16', CASE WHEN `DAY17` = '0' THEN 'A' ELSE `DAY17` END AS 'DAY17', CASE WHEN `DAY18` = '0' THEN 'A' ELSE `DAY18` END AS 'DAY18', CASE WHEN `DAY19` = '0' THEN 'A' ELSE `DAY19` END AS 'DAY19', CASE WHEN `DAY20` = '0' THEN 'A' ELSE `DAY20` END AS 'DAY20', CASE WHEN `DAY21` = '0' THEN 'A' ELSE `DAY21` END AS 'DAY21', CASE WHEN `DAY22` = '0' THEN 'A' ELSE `DAY22` END AS 'DAY22', CASE WHEN `DAY23` = '0' THEN 'A' ELSE `DAY23` END AS 'DAY23', CASE WHEN `DAY24` = '0' THEN 'A' ELSE `DAY24` END AS 'DAY24', CASE WHEN `DAY25` = '0' THEN 'A' ELSE `DAY25` END AS 'DAY25', CASE WHEN `DAY26` = '0' THEN 'A' ELSE `DAY26` END AS 'DAY26', CASE WHEN `DAY27` = '0' THEN 'A' ELSE `DAY27` END AS 'DAY27', CASE WHEN `DAY28` = '0' THEN 'A' ELSE `DAY28` END AS 'DAY28', CASE WHEN `DAY29` = '0' THEN 'A' ELSE `DAY29` END AS 'DAY29', CASE WHEN `DAY30` = '0' THEN 'A' ELSE `DAY30` END AS 'DAY30', CASE WHEN `DAY31` = '0' THEN 'A' ELSE `DAY31` END AS 'DAY31', `pay_attendance_muster`.`tot_days_present`, `pay_attendance_muster`.`tot_days_absent` AS 'absent', DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) AS 'total days', `LocationHead_Name`, `LocationHead_mobileno`, `OT_DAILY_DAY01` AS 'ot_daily_day01', `OT_DAILY_DAY02` AS 'ot_daily_day02', `OT_DAILY_DAY03` AS 'ot_daily_day03', `OT_DAILY_DAY04` AS 'ot_daily_day04', `OT_DAILY_DAY05` AS 'ot_daily_day05', `OT_DAILY_DAY06` AS 'ot_daily_day06', `OT_DAILY_DAY07` AS 'ot_daily_day07', `OT_DAILY_DAY08` AS 'ot_daily_day08', `OT_DAILY_DAY09` AS 'ot_daily_day09', `OT_DAILY_DAY10` AS 'ot_daily_day10', `OT_DAILY_DAY11` AS 'ot_daily_day11', `OT_DAILY_DAY12` AS 'ot_daily_day12', `OT_DAILY_DAY13` AS 'ot_daily_day13', `OT_DAILY_DAY14` AS 'ot_daily_day14', `OT_DAILY_DAY15` AS 'ot_daily_day15', `OT_DAILY_DAY16` AS 'ot_daily_day16', `OT_DAILY_DAY17` AS 'ot_daily_day17', `OT_DAILY_DAY18` AS 'ot_daily_day18', `OT_DAILY_DAY19` AS 'ot_daily_day19', `OT_DAILY_DAY20` AS 'ot_daily_day20', `OT_DAILY_DAY21` AS 'ot_daily_day21', `OT_DAILY_DAY22` AS 'ot_daily_day22', `OT_DAILY_DAY23` AS 'ot_daily_day23', `OT_DAILY_DAY24` AS 'ot_daily_day24', `OT_DAILY_DAY25` AS 'ot_daily_day25', `OT_DAILY_DAY26` AS 'ot_daily_day26', `OT_DAILY_DAY27` AS 'ot_daily_day27', `OT_DAILY_DAY28` AS 'ot_daily_day28', `OT_DAILY_DAY29` AS 'ot_daily_day29', `OT_DAILY_DAY30` AS 'ot_daily_day30', `OT_DAILY_DAY31` AS 'ot_daily_day31', `TOTAL_OT` AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                    
                    }
                    else
                    {
                        // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                    
                    }
                }
                i = 2;
            }
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Attendance_Copy_" + ddl_unitcode.SelectedItem.Text.Replace(" ", "_") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, i, ds, start_date_common, d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 0, 1), ot_flag1, btn_attendance);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, i, ds, start_date_common, "", ot_flag1, btn_attendance);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, i, ds, start_date_common, "", ot_flag1, btn_attendance);
                    Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(stringWrite.ToString());


                string abc = stringWrite.ToString();

                string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
                string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                string filename1 = path + "Attendance_Copy_" + unitcode + ".xls";



                System.IO.File.WriteAllText(filename1, abc);
                Response.Flush();
                Response.End();

                //   Session["month"] = txttodate.Text.Substring(0, 2);
                Session["year"] = txttodate.Text;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }
        }
    }
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;

        int i;
        DAL d = new DAL();
        static int ctr;
        static int att_ctr;
        int daysadd = 0;
        string header = "", header1 = "";
        string bodystr = "", start_date_common = "", btn_attendance;
        int ot_flag1; 
        public MyTemplate(ListItemType type, int i, DataSet ds, string start_date_common, string header1, int ot_flag1, string btn_attendance)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.start_date_common = start_date_common;
            this.header1 = header1;
            this.ot_flag1 = ot_flag1;
            this.btn_attendance = btn_attendance;
            ctr = 0;
            att_ctr = 0;
            header = "<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th>";
            int days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
            if (days == 29)
            { header = header + "<th>29</th><th>OT</th>"; daysadd = 1; }
            else if (days == 30)
            {
                header = header + "<th>29</th><th>30</th><th>OT</th>"; daysadd = 2;
            }
            else if (days == 31)
            {
                header = header + "<th>29</th><th>30</th><th>31</th><th>OT</th>"; daysadd = 3;
            }
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:

                    if (start_date_common != "" && start_date_common != "1")
                    {
                        header = header1;

                        //12-03-2020 for report upload
                        if (btn_attendance == "2")
                        {
                            header = header + "<th>OT</th>";
                        }
                    }
                    //if (i == 1)
                    //{
                    // lc = new LiteralControl("<table border=1 style=font-size:10;text-align:center;vertical-align:middle;><tr><th colspan=" + (36 + daysadd) + " style=font-size:10;text-align:center;vertical-align:middle;><b>Attendance Sheet</b></th></tr><tr><th colspan=3 font-size=8>Name of the Principal Employer:</th><th width=300 colspan=15>" + ds.Tables[0].Rows[ctr]["client_name"].ToString() + "</th><th width=110 text-size=8 colspan=6>Name of the Service Provider:</th><th width=350 colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_name"].ToString() + "</th></tr><tr><th colspan=3>Location:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["location"].ToString() + "</th><th colspan=6>Address of the Service Provider:</th><th width=380  colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["address1"].ToString() + "</th></tr><tr><th colspan=3>Location Code:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString() + "</th><th colspan=6>PF Registration No:</th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["pf_reg_no"].ToString() + "</th></tr><tr><th colspan=3 rowspan=2>Complete Address of the Location:</th><th colspan=15 rowspan=2>" + ds.Tables[0].Rows[ctr]["unit_add2"].ToString() + "</th><th colspan=6>ESIC Registration No:</th><th colspan=" + (12 + daysadd) + ">'" + ds.Tables[0].Rows[ctr]["esic"].ToString() + "</th></tr><tr><th colspan=6>PAN No.</th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_pan_no"].ToString() + "</th></tr><tr><th colspan=3>Attendance for the Month:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["fromtodate"].ToString() + "</th><th colspan=6>GSTIN/ UIN: </th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["gst"].ToString() + "</th></tr><tr style=text-align:center;vertical-align:middle;><th style=text-align:center;vertical-align:middle; width=20>SL. NO.</th><th width=130>NAME</th><th width=70>DOJ (DD-MM-YYYY)</th><th width=90>Designation</th><th width=70>Particulars</th>" + header + "<th width=30>PRESENT DAYS</th><th width=30>ABSENT DAYS</th><th width=40 style=text-align:center;vertical-align:middle;>TOTAL MONTH DAYS (P+WO)</th></tr>");

                    if (btn_attendance == "2")
                    {
                        lc = new LiteralControl("<table border=1 style=font-size:10;text-align:center;vertical-align:middle;><tr><th colspan=" + (37 + daysadd) + " style=font-size:10;text-align:center;vertical-align:middle;><b>Attendance Sheet</b></th></tr><tr><th colspan=3 font-size=8>Name of the Principal Employer:</th><th width=300 colspan=15>" + ds.Tables[0].Rows[ctr]["client_name"].ToString() + "</th><th width=110 text-size=8 colspan=6>Name of the Service Provider:</th><th width=350 colspan=" + (13 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_name"].ToString() + "</th></tr><tr><th colspan=3>Location:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["location"].ToString() + "</th><th colspan=6>Address of the Service Provider:</th><th width=380  colspan=" + (13 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["address1"].ToString() + "</th></tr><tr><th colspan=3>Location Code:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString() + "</th><th colspan=6>PF Registration No:</th><th colspan=" + (13 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["pf_reg_no"].ToString() + "</th></tr><tr><th colspan=3 rowspan=2>Complete Address of the Location:</th><th colspan=15 rowspan=2>" + ds.Tables[0].Rows[ctr]["unit_add2"].ToString() + "</th><th colspan=6>ESIC Registration No:</th><th colspan=" + (13 + daysadd) + ">'" + ds.Tables[0].Rows[ctr]["esic"].ToString() + "</th></tr><tr><th colspan=6>PAN No.</th><th colspan=" + (13 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_pan_no"].ToString() + "</th></tr><tr><th colspan=3>Attendance for the Month:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["fromtodate"].ToString() + "</th><th colspan=6>GSTIN/ UIN: </th><th colspan=" + (13 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["gst"].ToString() + "</th></tr><tr style=text-align:center;vertical-align:middle;><th style=text-align:center;vertical-align:middle; width=20>SL. NO.</th><th width=130>NAME</th><th width=70>DOJ (DD-MM-YYYY)</th><th width=90>Designation</th><th width=70>Particulars</th>" + header + "<th width=30>PRESENT DAYS</th><th width=30>ABSENT DAYS</th><th width=40 style=text-align:center;vertical-align:middle;>TOTAL MONTH DAYS (P+WO)</th></tr>");
                    
                    }


                    if (btn_attendance=="1")
                    {
                    // change for report upload 12-03-2020
                    lc = new LiteralControl("<table border=1><tr><th colspan=" + (30 + daysadd) + " style=font-size:10;text-align:center;vertical-align:middle;></th></tr><tr style=text-align:center;vertical-align:middle;><th style=text-align:center;vertical-align:middle; width=20>SL. NO.</th><th width=130>emp code</th>" + header + "</tr>");
                    }

                //}
                    //else {
                    //    lc = new LiteralControl("<table border=1><tr><th colspan=" + (36 + daysadd) + ">Attendance Sheet</th></tr><tr><th colspan=3>Name of the Principal Employer:</th><th colspan=9>" + ds.Tables[0].Rows[ctr]["client_name"].ToString() + "</th><th colspan=6>Name of the Service Provider:</th><th colspan=21>" + ds.Tables[0].Rows[ctr]["company_name"].ToString() + "</th></tr><tr><th colspan=3>Location:</th><th colspan=9>" + ds.Tables[0].Rows[ctr]["location"].ToString() + "</th><th colspan=6>Address of the Service Provider:</th><th colspan=21>" + ds.Tables[0].Rows[ctr]["address1"].ToString() + "</th></tr><tr><th colspan=3>Location Code:</th><th colspan=9>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString() + "</th><th colspan=6>PF Registration No:</th><th colspan=21>" + ds.Tables[0].Rows[ctr]["pf_reg_no"].ToString() + "</th></tr><tr><th colspan=3 rowspan=2>Complete Address of the Location:</th><th colspan=9 rowspan=2>" + ds.Tables[0].Rows[ctr]["unit_add2"].ToString() + "</th><th colspan=6>ESIC Registration No:</th><th colspan=21>'" + ds.Tables[0].Rows[ctr]["esic"].ToString() + "</th></tr><tr><th colspan=6>PAN No.</th><th colspan=21>" + ds.Tables[0].Rows[ctr]["company_pan_no"].ToString() + "</th></tr><tr><th colspan=3>Attendance for the Month:</th><th colspan=9>" + ds.Tables[0].Rows[ctr]["fromtodate"].ToString() + "</th><th colspan=6>GSTIN/ UIN: </th><th colspan=21>" + ds.Tables[0].Rows[ctr]["gst"].ToString() + "</th></tr><tr><th>SL. NO.</th><th>NAME</th><th>DOJ (DD-MM-YYYY)</th><th>Designation</th>" + header + "<th>PRESENT DAYS</th><th>ABSENT DAYS</th><th>TOTAL MONTH DAYS (P+WO)</th><th>STATUS</th></tr>");
                    //}
                    header = "";
                    break;
                case ListItemType.Item:
                    string iot_applicable = d.getsinglestring("Select iot_applicable from pay_client_master where client_code = '" + ds.Tables[0].Rows[ctr]["client_code"].ToString() + "' and comp_code ='" + ds.Tables[0].Rows[ctr]["comp_code"].ToString() + "'");

                    string color = "";

                    bodystr = "";
                    //if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY01"].ToString()=="" ? "A" : ds.Tables[0].Rows[ctr]["DAY01"].ToString() + "</td>";
                    //if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY02"].ToString()=="" ? "A" : ds.Tables[0].Rows[ctr]["DAY02"].ToString() + "</td>";
                    //if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY03"].ToString()=="" ? "A" : ds.Tables[0].Rows[ctr]["DAY03"].ToString() + "</td>";
                    //if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY04"].ToString()=="" ? "A" : ds.Tables[0].Rows[ctr]["DAY04"].ToString() + "</td>";
                    //if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY05"].ToString()=="" ? "A" : ds.Tables[0].Rows[ctr]["DAY05"].ToString() + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY01"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY02"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY03"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY04"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY05"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY06"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY06"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY06"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY07"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY07"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY07"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY08"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY08"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY08"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY09"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY09"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY09"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY10"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY10"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY10"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY11"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY11"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY11"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY12"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY12"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY12"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY13"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY13"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY13"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY14"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY14"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY14"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY15"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY15"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY15"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY16"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY16"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY16"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY17"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY17"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY17"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY18"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY18"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY18"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY19"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY19"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY19"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY20"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY20"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY20"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY21"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY21"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY21"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY22"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY22"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY22"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY23"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY23"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY23"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY24"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY24"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY24"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY25"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY25"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY25"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY26"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY26"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY26"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY27"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY27"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY27"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY28"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY28"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY28"] + "</td>";
                    //vikas change for excel ot 23/10/2019
                   string ot_bodystr = "";
                   if (ot_flag1 == 1)
                   {
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY01"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY02"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY03"] + "</td>";
                      // ot_bodystr = ot_bodystr + "<td>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY03"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY04"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY05"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY06"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY07"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY08"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY09"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY10"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY11"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY12"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY13"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY14"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY15"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY16"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY17"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY18"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY19"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY20"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY21"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY22"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY23"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY24"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY25"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY26"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY27"] + "</td>";
                       ot_bodystr = ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY28"] + "</td>";
                   }
                    string iot_body = "";
                    int days1 = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                    string present_days = "";
                    string absent_days = "";
                    string ot_hrs = "";

                    if (days1 == 29)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        //vikas change for ot 23/10
                        if (ot_flag1 == 1)
                        {
                            ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY29"] + "</td>";
                        }
                        //bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>"; 
                    }
                    else if (days1 == 30)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        //vikas change for ot 23/10
                        if (ot_flag1==1)
                        {
                        ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY29"] + "</td>";
                        ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY30"] + "</td>";
                        }
                        // bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                    }
                    else if (days1 == 31)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY31"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY31"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        //vikas change for ot 23/10
                        if (ot_flag1 == 1)
                        {
                            ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY29"] + "</td>";
                            ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY30"] + "</td>";
                            ot_bodystr = ot_bodystr + "<td>" + ds.Tables[0].Rows[ctr]["OT_DAILY_DAY31"] + "</td>";
                        }
                        //  bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>";
                    }
                    else
                    {
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";

                    }
                    if (i == 1)
                    {
                        string ot_body = "";

                        if (days1 == 29)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td> ";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
                        else if (days1 == 30)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY30"] + "</td>";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
                        else if (days1 == 31)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY30"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY31"] + "</td> ";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
                        else
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td>";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }

                        present_days = (days1 == 31 ? " = SUM(COUNTIF(F" + ((ctr * 2) + 9) + ":AJ" + ((ctr * 2) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AJ" + ((ctr * 2) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AJ" + ((ctr * 2) + 9) + ",\"HD\")/2)" : (days1 == 28) ? " = SUM(COUNTIF(F" + ((ctr * 2) + 9) + ":AG" + ((ctr * 2) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AG" + ((ctr * 2) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AG" + ((ctr * 2) + 9) + ",\"HD\")/2)" : (days1 == 29) ? " = SUM(COUNTIF(F" + ((ctr * 2) + 9) + ":AH" + ((ctr * 2) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AH" + ((ctr * 2) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AH" + ((ctr * 2) + 9) + ",\"HD\")/2)" : "=SUM(COUNTIF(F" + ((ctr * 2) + 9) + ":AI" + ((ctr * 2) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AI" + ((ctr * 2) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 2) + 9) + ":AI" + ((ctr * 2) + 9) + ",\"HD\")/2)");
                        absent_days = (days1 == 31 ? " = COUNTIF(F" + ((ctr * 2) + 9) + ":AJ" + ((ctr * 2) + 9) + ",\"A\")" : (days1 == 28) ? " = COUNTIF(F" + ((ctr * 2) + 9) + ":AG" + ((ctr * 2) + 9) + ",\"A\")" : (days1 == 29) ? " = COUNTIF(F" + ((ctr * 2) + 9) + ":AH" + ((ctr * 2) + 9) + ",\"A\")" : "=COUNTIF(F" + ((ctr * 2) + 9) + ":AI" + ((ctr * 2) + 9) + ",\"A\")");
                        ot_hrs = (days1 == 31 ? "=SUM(F" + ((ctr * 2) + 10) + ":AJ" + ((ctr * 2) + 10) + ")" : (days1 == 28) ? "=SUM(F" + ((ctr * 2) + 10) + ":AG" + ((ctr * 2) + 10) + ")" : (days1 == 29) ? "=SUM(F" + ((ctr * 2) + 10) + ":AH" + ((ctr * 2) + 10) + ")" : "=SUM(F" + ((ctr * 2) + 10) + ":AI" + ((ctr * 2) + 10) + ")");

                        if (iot_applicable == "1")
                        {
                            present_days = (days1 == 31 ? "=SUM(COUNTIF(F" + ((ctr * 4) + 9) + ":AJ" + ((ctr * 4) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AJ" + ((ctr * 4) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AJ" + ((ctr * 4) + 9) + ",\"HD\")/2)" : (days1 == 28) ? "=SUM(COUNTIF(F" + ((ctr * 4) + 9) + ":AG" + ((ctr * 4) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AG" + ((ctr * 4) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AG" + ((ctr * 4) + 9) + ",\"HD\")/2)" : (days1 == 29) ? "=SUM(COUNTIF(F" + ((ctr * 4) + 9) + ":AH" + ((ctr * 4) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AH" + ((ctr * 4) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AH" + ((ctr * 4) + 9) + ",\"HD\")/2)" : "=SUM(COUNTIF(F" + ((ctr * 4) + 9) + ":AI" + ((ctr * 4) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AI" + ((ctr * 4) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 4) + 9) + ":AI" + ((ctr * 4) + 9) + ",\"HD\")/2)");
                            absent_days = (days1 == 31 ? "=COUNTIF(F" + ((ctr * 4) + 9) + ":AJ" + ((ctr * 4) + 9) + ",\"A\")" : (days1 == 28) ? "=COUNTIF(F" + ((ctr * 4) + 9) + ":AG" + ((ctr * 4) + 9) + ",\"A\")" : (days1 == 29) ? "=COUNTIF(F" + ((ctr * 4) + 9) + ":AH" + ((ctr * 4) + 9) + ",\"A\")" : "=COUNTIF(F" + ((ctr * 4) + 9) + ":AI" + ((ctr * 4) + 9) + ",\"A\")");
                            ot_hrs = (days1 == 31 ? "=SUM(F" + ((ctr * 4) + 10) + ":AJ" + ((ctr * 4) + 10) + ")" : (days1 == 28) ? "=SUM(F" + ((ctr * 4) + 10) + ":AG" + ((ctr * 4) + 10) + ")" : (days1 == 29) ? "=SUM(F" + ((ctr * 4) + 10) + ":AH" + ((ctr * 4) + 10) + ")" : "=SUM(F" + ((ctr * 4) + 10) + ":AI" + ((ctr * 4) + 10) + ")");

                          
                            if (btn_attendance == "2")
                            {
                                lc = new LiteralControl("<tr><td  style=text-align:center;vertical-align:middle;>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td  style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td  style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>Extra Hours</td>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                            }

                            if(btn_attendance=="1"){

                                // 12-03-2020 for report upload
                            lc = new LiteralControl("<tr><td  style=text-align:center;vertical-align:middle;>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td>" + bodystr + "</tr><tr>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                            }

                        }
                        else
                        {
                            if (btn_attendance == "2")
                            {

                                lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>Extra Hours</td>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr>");
                            }
                            if (btn_attendance == "1")
                            {
                                // 12-03-2020 for report upload
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td>" + bodystr + "</tr><tr>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr>");
                            }
                           
                        }

                    }
                    else
                    {
                        present_days = (days1 == 31 ? "=SUM(COUNTIF(F" + (ctr + 9) + ":AJ" + (ctr + 9) + ",\"P\")+COUNTIF(F" + (ctr + 9) + ":AJ" + (ctr + 9) + ",\"PH\")+COUNTIF(F" + (ctr + 9) + ":AJ" + (ctr + 9) + ",\"HD\")/2)" : (days1 == 28) ? " =SUM(COUNTIF(F" + (ctr + 9) + ":AG" + (ctr + 9) + ",\"P\")+COUNTIF(F" + (ctr + 9) + ":AG" + (ctr + 9) + ",\"PH\")+COUNTIF(F" + (ctr + 9) + ":AG" + (ctr + 9) + ",\"HD\")/2) " : (days1 == 29) ? " =SUM(COUNTIF(F" + (ctr + 9) + ":AH" + (ctr + 9) + ",\"P\")+COUNTIF(F" + (ctr + 9) + ":AH" + (ctr + 9) + ",\"PH\")+COUNTIF(F" + (ctr + 9) + ":AH" + (ctr + 9) + ",\"HD\")/2) " : "=SUM(COUNTIF(F" + (ctr + 9) + ":AI" + (ctr + 9) + ",\"P\")+COUNTIF(F" + (ctr + 9) + ":AI" + (ctr + 9) + ",\"PH\")+COUNTIF(F" + (ctr + 9) + ":AI" + (ctr + 9) + ",\"HD\")/2)");
                        absent_days = (days1 == 31 ? "=COUNTIF(F" + (ctr + 9) + ":AJ" + (ctr + 9) + ",\"A\")" : (days1 == 28) ? "=COUNTIF(F" + (ctr + 9) + ":AG" + (ctr + 9) + ",\"A\")" : (days1 == 29) ? "=COUNTIF(F" + (ctr + 9) + ":AH" + (ctr + 9) + ",\"A\")" : "=COUNTIF(F" + (ctr + 9) + ":AI" + (ctr + 9) + ",\"A\")");

                        if (iot_applicable == "1")
                        {
                            present_days = (days1 == 31 ? "=SUM(COUNTIF(F" + ((ctr * 3) + 9) + ":AJ" + ((ctr * 3) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AJ" + ((ctr * 3) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AJ" + ((ctr * 3) + 9) + ",\"HD\")/2)" : (days1 == 28) ? "=SUM(COUNTIF(F" + ((ctr * 3) + 9) + ":AG" + ((ctr * 3) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AG" + ((ctr * 3) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AG" + ((ctr * 3) + 9) + ",\"HD\")/2)" : (days1 == 29) ? "=SUM(COUNTIF(F" + ((ctr * 3) + 9) + ":AH" + ((ctr * 3) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AH" + ((ctr * 3) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AH" + ((ctr * 3) + 9) + ",\"HD\")/2)" : "=SUM(COUNTIF(F" + ((ctr * 3) + 9) + ":AI" + ((ctr * 3) + 9) + ",\"P\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AI" + ((ctr * 3) + 9) + ",\"PH\")+COUNTIF(F" + ((ctr * 3) + 9) + ":AI" + ((ctr * 3) + 9) + ",\"HD\")/2)");
                            absent_days = (days1 == 31 ? "=COUNTIF(F" + ((ctr * 3) + 9) + ":AJ" + ((ctr * 3) + 9) + ",\"A\")" : (days1 == 28) ? "=COUNTIF(F" + ((ctr * 3) + 9) + ":AG" + ((ctr * 3) + 9) + ",\"A\")" : (days1 == 29) ? "=COUNTIF(F" + ((ctr * 3) + 9) + ":AH" + ((ctr * 3) + 9) + ",\"A\")" : "=COUNTIF(F" + ((ctr * 3) + 9) + ":AI" + ((ctr * 3) + 9) + ",\"A\")");

                            if (btn_attendance == "2")
                            {
                               // lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                                lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                           
                            }

                            if (btn_attendance == "1")
                            {
                                // 12-03-2020 for report upload
                                lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td>" + bodystr + "</tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");

                            }
                        }
                        else
                        {
                            // android ot applicable flag
                            if (ot_flag1 == 1)
                            {
                                present_days = (days1 == 31 ? "=SUM(COUNTIF(F" + (att_ctr + 9) + ":AJ" + (att_ctr + 9) + ",\"P\")+COUNTIF(F" + (att_ctr + 9) + ":AJ" + (att_ctr + 9) + ",\"PH\")+COUNTIF(F" + (att_ctr + 9) + ":AJ" + (att_ctr + 9) + ",\"HD\")/2)" : (days1 == 28) ? " =SUM(COUNTIF(F" + (att_ctr + 9) + ":AG" + (att_ctr + 9) + ",\"P\")+COUNTIF(F" + (att_ctr + 9) + ":AG" + (att_ctr + 9) + ",\"PH\")+COUNTIF(F" + (att_ctr + 9) + ":AG" + (att_ctr + 9) + ",\"HD\")/2) " : (days1 == 29) ? " =SUM(COUNTIF(F" + (att_ctr + 9) + ":AH" + (att_ctr + 9) + ",\"P\")+COUNTIF(F" + (att_ctr + 9) + ":AH" + (att_ctr + 9) + ",\"PH\")+COUNTIF(F" + (att_ctr + 9) + ":AH" + (att_ctr + 9) + ",\"HD\")/2) " : "=SUM(COUNTIF(F" + (att_ctr + 9) + ":AI" + (att_ctr + 9) + ",\"P\")+COUNTIF(F" + (att_ctr + 9) + ":AI" + (att_ctr + 9) + ",\"PH\")+COUNTIF(F" + (att_ctr + 9) + ":AI" + (att_ctr + 9) + ",\"HD\")/2)");
                                absent_days = (days1 == 31 ? "=COUNTIF(F" + (att_ctr + 9) + ":AJ" + (att_ctr + 9) + ",\"A\")" : (days1 == 28) ? "=COUNTIF(F" + (att_ctr + 9) + ":AG" + (att_ctr + 9) + ",\"A\")" : (days1 == 29) ? "=COUNTIF(F" + (att_ctr + 9) + ":AH" + (att_ctr + 9) + ",\"A\")" : "=COUNTIF(F" + (att_ctr + 9) + ":AI" + (att_ctr + 9) + ",\"A\")");

                               // lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td></td><td></td><td></td><td></td><td>OT</td>" + ot_bodystr + "<td></td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["TOTAL_OT"] + "</td><td></td></tr>");
                              
                                //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td></td><td></td><td></td><td></td><td>OT</td>" + ot_bodystr + "<td></td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["TOTAL_OT"] + "</td></tr>");


                                if (btn_attendance == "2")
                                {
                                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td></td><td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td></td><td></td><td></td><td></td><td>OT</td>" + ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["TOTAL_OT"] + "</td><td>0</td><td>0</td><td>0</td></tr>");

                                }
                                if (btn_attendance == "1")
                                {
                                    // 12-03-2020 for report upload
                                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td>" + bodystr + "<td></td></tr><tr><td></td><td></td><td></td><td></td><td>OT</td>" + ot_bodystr + "<td style=text-align:center;vertical-align:middle;>'" + ds.Tables[0].Rows[ctr]["TOTAL_OT"] + "</td><td>0</td><td>0</td><td>0</td></tr>");

                                }
                                  }
                            else
                            {
                                if (btn_attendance == "2")
                                {
                                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td>0</td><td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr>");

                                }

                                if (btn_attendance == "1")
                                {
                                    // 12-03-2020 for report upload
                                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td>" + bodystr + "</tr>");

                                }
                            }
                        }
                    }
                    bodystr = "";
                    ctr++;
                    //att_ctr++;
                    att_ctr = att_ctr+2;
                    break;

                case ListItemType.Footer:

                    // 12-02-2020 FOR REPORT UPLOAD

                    if (btn_attendance == "1")
                    {
                        lc = new LiteralControl("<tr></tr>");
                    }

                    if (btn_attendance == "2")
                    {
                        lc = new LiteralControl("<tr><td rowspan=6></td><td><b>P = Present</b></td><td colspan =4>NAME OF THE MANAGER</td><td style=text-align:center;vertical-align:middle; colspan=14><b>" + ds.Tables[0].Rows[0]["LocationHead_Name"].ToString() + "</b></td><td height=120 colspan=" + (17 + daysadd) + " rowspan=6 align=center><b>Stamp and Signature of the Service Provider</b></td></tr><tr><td><b>A = Absent</b></td><td colspan =4>&nbsp;<p> SIGNATURE OF THE MANAGER</td><td colspan=14></td></tr><tr><td ><b>W = Weekly Off</b></td><td colspan =4>DATE OF SIGNATURE</td><td colspan=14></td></tr><tr><td><b>PH = Paid Holiday</b></td><td colspan =4>MOBILE NO.</td><td style=text-align:center;vertical-align:middle; colspan=14><b>" + ds.Tables[0].Rows[0]["LocationHead_mobileno"].ToString() + "</b></td></tr><tr><td><b>L = Leave</b></td><td colspan =4> LANDLINE NO.</td><td colspan=14></td></tr><tr><td></td><td colspan =4>STAMP</td><td colspan=14></td></tr></table>");
                    
                    }
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
    protected string get_start_date()
    {

        //  string function = where_function();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' ";
            //unit_code = unit_code + "";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }

        //return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");

       // return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN (" + unit_code + ") limit 1))");
       //rahul changes query 08-11-2019
        return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_unit_master INNER JOIN pay_billing_master_history ON  pay_unit_master.unit_code = pay_billing_master_history.billing_unit_code AND  pay_unit_master.comp_code = pay_billing_master_history.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_unit_master  INNER JOIN  pay_billing_master  ON    pay_unit_master.unit_code = pay_billing_master.billing_unit_code  AND   pay_unit_master.comp_code = pay_billing_master.comp_code   WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN (" + unit_code + ") limit 1))");
    }
    protected string get_start_date_material()
    {

        //  string function = where_function();
        string unit_code = "";

        if (ddl_branch_material.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and  STATE_NAME  = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ";
            //unit_code = unit_code + "";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }

        //return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");

        // return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN (" + unit_code + ") limit 1))");
        //rahul changes query 08-11-2019
        return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_unit_master INNER JOIN pay_billing_master_history ON  pay_unit_master.unit_code = pay_billing_master_history.billing_unit_code AND  pay_unit_master.comp_code = pay_billing_master_history.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_unit_master  INNER JOIN  pay_billing_master  ON    pay_unit_master.unit_code = pay_billing_master.billing_unit_code  AND   pay_unit_master.comp_code = pay_billing_master.comp_code   WHERE pay_unit_master.client_code  = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN (" + unit_code + ") limit 1))");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string function = where_function();
        if (hidden_month.Value.Equals("1"))
        {
            gridcalender_update(12, (int.Parse(hidden_year.Value) - 1), 0, function);
            //hidden_month.Value = "12"; hidden_year.Value = (int.Parse(hidden_year.Value) - 1).ToString();
        }
        else
        {
            gridcalender_update((int.Parse(hidden_month.Value) - 1), int.Parse(hidden_year.Value), 0, function);
            // hidden_month.Value = (int.Parse(hidden_month.Value) - 1).ToString(); 
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string function = where_function();
        if (hidden_month.Value.Equals("12"))
        {
            gridcalender_update(1, (int.Parse(hidden_year.Value) + 1), 0, function);
            // hidden_month.Value = "1"; hidden_year.Value = (int.Parse(hidden_year.Value) + 1).ToString();
        }
        else
        {
            gridcalender_update((int.Parse(hidden_month.Value) + 1), int.Parse(hidden_year.Value), 0, function);
            //hidden_month.Value = (int.Parse(hidden_month.Value) - 1).ToString();
        }
    }
    private void update_label(int month, int year, int date_common)
    {
        if (month.Equals(0) && year.Equals(0))
        {
            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT MONTH(now()), YEAR(now())", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (date_common == 1)
                    {
                        lbl_month_year.Text = getmonth(dr.GetValue(0).ToString()) + " " + dr.GetValue(1).ToString();
                    }
                    else
                    {
                        lbl_month_year.Text = getmonth((int.Parse(dr.GetValue(0).ToString()) - 1).ToString()) + " " + dr.GetValue(1).ToString() + " - " + getmonth(dr.GetValue(0).ToString()) + " " + dr.GetValue(1).ToString();
                    }
                    hidden_month.Value = (dr.GetValue(0).ToString().Length == 2 ? dr.GetValue(0).ToString() : "0" + dr.GetValue(0).ToString());
                    hidden_year.Value = dr.GetValue(1).ToString();
                }
                dr.Close();
                cmd.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }
        else
        {
            if (date_common == 1)
            {
                lbl_month_year.Text = getmonth(month.ToString()) + " " + year;
            }
            else
            {
                lbl_month_year.Text = getmonth((month - 1).ToString()) + " " + ((month - 1) == 0 ? year - 1 : year) + " - " + getmonth(month.ToString()) + " " + year;
            }
            hidden_month.Value = (month.ToString().Length == 2 ? month.ToString() : "0" + month.ToString());
            hidden_year.Value = year.ToString();

        }

        //Session["YEAR_MONTH"] = lbl_month_year.Text;
    }
    private string getmonth(string month)
    {
        if (month == "0")
        {
            month = "12";
        }

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


    private bool gridcalender_update(int mnth, int year, int counter,string where_function)
    {
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        try
        {
            string date_common = get_start_date();
            string where_unit = "";
            //string function = where_function();
            string function = where_function;
            if (ddl_unitcode.SelectedValue == "ALL") { where_unit = " and pay_employee_master.unit_code in (select unit_code from pay_unit_master where client_code = '" + ddl_client.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " ) )"; }
            else { where_unit = " and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "'"; }

            string where = "";

            if (counter == 1)
            {
                // int flag = 0;
                d.con.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT flag, ot_applicable FROM pay_unit_master  INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and  unit_code='" + ddl_unitcode.SelectedValue + "' and pay_unit_master.client_code='" + ddl_client.SelectedValue + "'", d.con);
                //MySqlDataReader dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    flag = int.Parse(dr.GetValue(0).ToString());

                //}
                ViewState["EMP_CODE"] = "";
                string insrt_where = "";
                btn_attendance.Visible = true;
                int mnth1 = mnth, year1 = year;
                if (date_common != "" && date_common != "1")
                {
                    mnth1 = --mnth1;
                    if (mnth1 == 0) { mnth1 = 12; year1 = --year1; }
                    insrt_where = " and (left_date >= str_to_date('" + date_common + "/" + mnth1 + "/" + year1 + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + (int.Parse(date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }
                else
                {
                    date_common = "1";

                    insrt_where = " and (left_date >= str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }

                string inlist = "", outlist = "", reliverlist = "";
                // int counter1 = 0;

                foreach (GridViewRow gvrow in gv_fullmonthot.Rows)
                {
                    System.Web.UI.WebControls.TextBox left_date_date = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("left_date_date");

                    string emp_code = (string)gv_fullmonthot.DataKeys[gvrow.RowIndex].Value;

                    if (left_date_date.Text != "")
                    {
                        d.operation("update pay_employee_master set LEFT_DATE= str_to_date('" + left_date_date.Text + "','%d/%m/%Y'), LEFT_REASON='" + (((System.Web.UI.WebControls.TextBox)gvrow.FindControl("txt_emp_sample_left_reson")).Text.Trim() == "" ? "LEFT" : ((System.Web.UI.WebControls.TextBox)gvrow.FindControl("txt_emp_sample_left_reson")).Text) + "' where emp_code='" + emp_code + "' ");
                    }

                    var checkbox = gvrow.FindControl("chk_client") as CheckBox;
                    if (checkbox.Checked == true)
                    {
                        //counter1 = 1;
                        try
                        {
                            inlist = inlist + "'" + emp_code + "',";

                            if (gvrow.Cells[3].Text.ToUpper() == "RELIEVER")
                            {
                                reliverlist = reliverlist + "'" + emp_code + "',";

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                        }
                    }
                    else
                    {
                        outlist = outlist + "'" + emp_code + "',";
                        if (left_date_date.Text == "")
                        {
                            return false;
                        }
                    }
                }
                //if (counter1 == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Atleast One Employee.');", true);
                //    d.con.Close();
                //    emp_btn_process_Click(null, null);
                //    return;

                //}
                if (inlist.Length > 0)
                {
                    inlist = inlist.Substring(0, inlist.Length - 1);
                }
                else { inlist = "''"; }
                if (outlist.Length > 0)
                {
                    outlist = outlist.Substring(0, outlist.Length - 1);
                }
                else { outlist = "''"; }
                if (reliverlist.Length > 0)
                {
                    reliverlist = reliverlist.Substring(0, reliverlist.Length - 1);
                }
                else { reliverlist = "''"; }
                try
                {
                    //string function1 = where_function();
                    string function1 = where_function;
                    string unit_code = "";

                    if (ddl_unitcode.SelectedValue == "ALL")
                    {
                        unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function1 + ")  ";
                    }
                    else
                    {
                        Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                        //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                        unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
                    }


                    //d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintSundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + ") and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and STR_TO_DATE(concat('1/',date_format(joining_date, '%m/%Y')), '%d/%m/%Y') <=  str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))");
                    d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintSundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + ")  " + insrt_where + ")");
                    if (date_common != "" && date_common != "1")
                    {
                        d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintSundays(mnth1, year1, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + ") " + insrt_where + ")");
                    }

                    if (mnth == 1)
                    {
                        d.operation("update pay_attendance_muster set DAY26='PH' WHERE DAY26='P' and emp_CODE in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    }
                    else if (mnth == 5)
                    {
                        d.operation("update pay_attendance_muster set DAY01='PH' WHERE DAY01='P' and emp_CODE in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    }
                    else if (mnth == 8)
                    {
                        d.operation("update pay_attendance_muster set DAY15='PH' WHERE DAY15='P' and emp_CODE in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    }
                    else if (mnth == 10)
                    {
                        d.operation("update pay_attendance_muster set DAY02='PH' WHERE DAY02='P' and emp_CODE in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    }
                    if (ot_applicable == "1")
                    {
                        d.operation("INSERT INTO pay_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTSundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintOTSundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_codeIN(" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + ") and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and STR_TO_DATE(concat('1/',date_format(joining_date, '%m/%Y')), '%d/%m/%Y') <=  str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))");

                        if (date_common != "" && date_common != "1")
                        {
                            d.operation("INSERT INTO pay_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTSundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintOTSundays(mnth1, year1, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + ") and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and STR_TO_DATE(concat('1/',date_format(joining_date, '%m/%Y')), '%d/%m/%Y') <=  str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))");

                        }
                    }
                    ViewState["EMP_CODE"] = inlist;
                    //vikas add code 04/05/2019
                    if (ot_applicable == "2")
                    {
                        // d.operation("INSERT INTO pay_daily_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTDaySundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintOTDaySundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_daily_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + ")  " + insrt_where + ")");
                        d.operation("INSERT INTO pay_daily_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTDaySundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintOTDaySundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_daily_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN (" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + ") and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and STR_TO_DATE(concat('1/',date_format(joining_date, '%m/%Y')), '%d/%m/%Y') <=  str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))");
                        if (date_common != "" && date_common != "1")
                        {
                            //  d.operation("INSERT INTO pay_daily_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTDaySundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintOTDaySundays(mnth1, year1, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_daily_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + ") " + insrt_where + ")");
                            d.operation("INSERT INTO pay_daily_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTDaySundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintOTDaySundays(mnth1, year1, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and emp_code not in (" + outlist + ") and EMP_CODE not in (select emp_code from pay_daily_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN (" + unit_code + ") and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + ") and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and STR_TO_DATE(concat('1/',date_format(joining_date, '%m/%Y')), '%d/%m/%Y') <=  str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))");
                        }
                    }
                    //vikas end code 04/05/2019
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    d.operation("delete from pay_attendance_muster where emp_CODE in (" + outlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    d.operation("delete from pay_attendance_muster where emp_CODE in (" + outlist + ") AND FLAG = 0 AND MONTH = " + mnth1 + " AND YEAR= " + year1);
                    d.operation("delete from pay_ot_muster where emp_CODE in (" + outlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                    //vikas add code 04/05/2019
                    d.operation("delete from pay_daily_ot_muster where emp_CODE in (" + outlist + ") AND MONTH = " + mnth + " AND YEAR= " + year);
                }

                //Rahul Sir Code *** commenting

                //            string joining_emp_code = "";
                //            string joining_emp_name = "";
                //            string joiningDay = "";
                //            int joinintgmonth = 0;
                //            int joiningyear = 0;
                //            string leftDay = "";
                //            int leftmonth = 0;
                //            int leftyear = 0;

                //            int select_month = int.Parse(hidden_month.Value);
                //            int select_year = int.Parse(hidden_year.Value);
                //            MySqlCommand cmd_item = new MySqlCommand("select emp_code,emp_name,date_format(joining_date,'%d/%m/%Y') as joining_date from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and joining_date between str_to_date('01/" + select_month + "/" + select_year + "', '%d/%m/%Y') and str_to_date('31/" + select_month + "/" + select_year + "', '%d/%m/%Y') and emp_CODE in (" + inlist + ")", d1.con1);
                //            d1.con1.Open();
                //            MySqlDataReader dr2 = cmd_item.ExecuteReader();
                //            try
                //            {
                //                while (dr2.Read())
                //                {
                //                    joining_emp_code = dr2.GetValue(0).ToString();
                //                    joining_emp_name = dr2.GetValue(1).ToString();
                //                    joiningDay = dr2.GetValue(2).ToString().Substring(0, 2);
                //                    joinintgmonth = int.Parse(dr2.GetValue(2).ToString().Substring(3, 2));
                //                    joiningyear = int.Parse(dr2.GetValue(2).ToString().Substring(6, 4));

                //                    string h_day2 = "";

                //                    if (joinintgmonth == select_month && joiningyear == select_year)
                //                    {
                //                        for (int i = int.Parse(joiningDay) - 1; i > 0; i--)
                //                        {
                //                            if (i < 10)
                //                            {
                //                                h_day2 = "DAY0" + i;
                //                            }
                //                            else
                //                            {
                //                                h_day2 = "DAY" + i;
                //                            }
                //                            int res_att = d.operation("UPDATE pay_attendance_muster  SET " + h_day2 + "='A' WHERE EMP_CODE = '" + joining_emp_code + "' AND MONTH = '" + select_month + "' AND YEAR='" + select_year + "'");
                //                        }
                //                        //d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txttodate.Text, int.Parse(date_common), ddl_state.SelectedValue);
                //                        //int sundays = Countsundays(DateTime.ParseExact("01/" + dr2.GetValue(2).ToString().Substring(3) + "", "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(dr2.GetValue(2).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                //                        //d.operation("UPDATE pay_attendance_muster set TOT_DAYS_PRESENT = (TOT_DAYS_PRESENT + " + sundays + "), WEEKLY_OFF = (WEEKLY_OFF - "+sundays+") WHERE EMP_CODE = '" + joining_emp_code + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue.ToString() + "' AND MONTH = '" + select_month + "' AND YEAR='" + select_year + "'");
                //                    }

                //                }
                //            }
                //            catch (Exception ex) { throw ex; }
                //            finally
                //            {
                //                dr2.Close();
                //                cmd_item.Dispose();
                //                d1.con1.Close();
                //            }

                //            ///////////////
                //            // Client billing cycle wise code ex 26 to 25 new Employee Joining Date
                //            string start_date_common = get_start_date();
                //            string end_date = get_end_date();
                //            string h_day12 = "";
                //            int selectmonthsub = 0;
                //            int ddl_select_month = 0;
                //            if (select_month == 1)
                //            {
                //                //selectmonthsub = 12;
                //                selectmonthsub = 12;
                //                select_month = 12;
                //                select_year = select_year - 1;
                //            }
                //            else if (select_month == 12)
                //            {
                //                selectmonthsub = 12;
                //                select_month = 1;
                //                select_year = select_year + 1;

                //            }
                //            else
                //            {
                //                selectmonthsub = select_month - 1;
                //                ddl_select_month = select_month;
                //                select_month = select_month + 1;


                //            }
                //            MySqlCommand cmd_item12 = new MySqlCommand("select emp_code,emp_name,date_format(joining_date,'%d/%m/%Y') as joining_date from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and joining_date between str_to_date('01/" + select_month + "/" + select_year + "', '%d/%m/%Y') and str_to_date('31/" + select_month + "/" + select_year + "', '%d/%m/%Y') and emp_CODE in (" + inlist + ")", d1.con1);
                //            d1.con1.Open();
                //            MySqlDataReader dr12 = cmd_item12.ExecuteReader();
                //            try
                //            {
                //                while (dr12.Read())
                //                {
                //                    joining_emp_code = dr12.GetValue(0).ToString();
                //                    joining_emp_name = dr12.GetValue(1).ToString();
                //                    joiningDay = dr12.GetValue(2).ToString().Substring(0, 2);
                //                    joinintgmonth = int.Parse(dr12.GetValue(2).ToString().Substring(3, 2));
                //                    joiningyear = int.Parse(dr12.GetValue(2).ToString().Substring(6, 4));


                //                    string h_day13 = "";


                //                    if (start_date_common != "" && start_date_common != "1")
                //                    {
                //                        // if (joinintgmonth == select_month + 1 && joiningyear == select_year)
                //                        if (joinintgmonth == select_month && joiningyear == select_year)
                //                        {
                //                            for (int j = int.Parse(start_date_common); j <= DateTime.DaysInMonth(select_year, selectmonthsub); j++)
                //                            {

                //                                if (j < 10)
                //                                {
                //                                    h_day13 = "DAY0" + j;
                //                                }
                //                                else
                //                                {
                //                                    h_day13 = "DAY" + j;
                //                                }
                //                                int res_att = d.operation("UPDATE pay_attendance_muster  SET " + h_day13 + "='A' WHERE EMP_CODE = '" + joining_emp_code + "' AND MONTH = '" + ddl_select_month + "' AND YEAR='" + select_year + "'");

                //                            }
                ////                            d.update_attendance_left_joined(selectmonthsub, joiningyear, joining_emp_code);
                //                            //d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txttodate.Text, int.Parse(date_common), ddl_state.SelectedValue);
                //                        }

                //                    }

                //                }
                //            }
                //            catch (Exception ex) { throw ex; }
                //            finally
                //            {
                //                dr12.Close();
                //                cmd_item.Dispose();
                //                d1.con1.Close();
                //            }

                //            // client billing cycle wise left employee Date

                //            int select_month3 = int.Parse(hidden_month.Value);
                //            int select_year3 = int.Parse(hidden_year.Value);
                //            int selectmonthsub3 = 0;
                //            int ddl_select_month3 = 0;
                //            if (select_month3 == 1)
                //            {
                //                //selectmonthsub = 12;
                //                selectmonthsub3 = 12;
                //                select_month3 = 12;
                //                select_year3 = select_year3 - 1;
                //            }
                //            else if (select_month3 == 12)
                //            {
                //                selectmonthsub3 = 12;
                //                select_month3 = 1;
                //                select_year3 = select_year3 + 1;

                //            }
                //            else
                //            {
                //                selectmonthsub3 = select_month3 - 1;
                //                ddl_select_month3 = select_month3 + 1;
                //                // select_month3 = select_month3 - 1;

                //            }
                //            MySqlCommand cmd_item3 = new MySqlCommand("select emp_code,emp_name, date_format(left_date,'%d/%m/%Y') as left_date from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and left_date between str_to_date('01/" + select_month3 + "/" + select_year3 + "', '%d/%m/%Y') and str_to_date('31/" + select_month3 + "/" + select_year3 + "', '%d/%m/%Y') and emp_CODE in (" + inlist + ")", d1.con1);
                //            d1.con1.Open();
                //            MySqlDataReader dr4 = cmd_item3.ExecuteReader();
                //            try
                //            {
                //                while (dr4.Read())
                //                {
                //                    joining_emp_code = dr4.GetValue(0).ToString();
                //                    joining_emp_name = dr4.GetValue(1).ToString();

                //                    leftDay = dr4.GetValue(2).ToString().Substring(0, 2);
                //                    leftmonth = int.Parse(dr4.GetValue(2).ToString().Substring(3, 2));
                //                    leftyear = int.Parse(dr4.GetValue(2).ToString().Substring(6, 4));

                //                    string h_day4 = "";
                //                    if (start_date_common != "" && start_date_common != "1")
                //                    {
                //                        if (leftmonth == select_month3 && leftyear == select_year3 && leftDay != "")
                //                        {

                //                            // for (int j = int.Parse(start_date_common); j <= DateTime.DaysInMonth(leftyear, leftmonth); j++)
                //                            for (int j = int.Parse(end_date); j > 0; j--)
                //                            {

                //                                if (j < 10)
                //                                {
                //                                    h_day4 = "DAY0" + j;
                //                                }
                //                                else
                //                                {
                //                                    h_day4 = "DAY" + j;
                //                                }
                //                                int res_att = d.operation("UPDATE pay_attendance_muster SET " + h_day4 + "='A' WHERE EMP_CODE = '" + joining_emp_code + "' AND MONTH = '" + ddl_select_month3 + "' AND YEAR='" + select_year3 + "'");

                //                            }
                //                            //d.update_attendance_left_joined(ddl_select_month3, leftyear, joining_emp_code);
                //                           // d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txttodate.Text, int.Parse(date_common), ddl_state.SelectedValue);

                //                        }
                //                    }
                //                }
                //            }
                //            catch (Exception ex) { throw ex; }
                //            finally
                //            {
                //                dr4.Close();
                //                cmd_item3.Dispose();
                //                d1.con1.Close();
                //            }

                //            // left employee code

                //            int select_month2 = int.Parse(hidden_month.Value);
                //            int select_year2 = int.Parse(hidden_year.Value);


                //            MySqlCommand cmd_item1 = new MySqlCommand("select emp_code,emp_name, date_format(left_date,'%d/%m/%Y') as left_date from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and left_date between str_to_date('01/" + select_month2 + "/" + select_year2 + "', '%d/%m/%Y') and str_to_date('31/" + select_month2 + "/" + select_year2 + "', '%d/%m/%Y') and emp_CODE in (" + inlist + ")", d1.con1);
                //            d1.con1.Open();
                //            MySqlDataReader dr3 = cmd_item1.ExecuteReader();
                //            try
                //            {
                //                while (dr3.Read())
                //                {
                //                    joining_emp_code = dr3.GetValue(0).ToString();
                //                    joining_emp_name = dr3.GetValue(1).ToString();

                //                    leftDay = dr3.GetValue(2).ToString().Substring(0, 2);
                //                    leftmonth = int.Parse(dr3.GetValue(2).ToString().Substring(3, 2));
                //                    leftyear = int.Parse(dr3.GetValue(2).ToString().Substring(6, 4));

                //                    string h_day3 = "";

                //                    if (leftmonth == select_month2 && leftyear == select_year2 && leftDay != "")
                //                    {

                //                        for (int j = int.Parse(leftDay) + 1; j <= DateTime.DaysInMonth(leftyear, leftmonth); j++)
                //                        {

                //                            if (j < 10)
                //                            {
                //                                h_day3 = "DAY0" + j;
                //                            }
                //                            else
                //                            {
                //                                h_day3 = "DAY" + j;
                //                            }
                //                            int res_att = d.operation("UPDATE pay_attendance_muster SET " + h_day3 + "='A' WHERE EMP_CODE = '" + joining_emp_code + "' AND MONTH = '" + select_month2 + "' AND YEAR='" + select_year2 + "'");

                //                        }
                //                       // d.update_attendance_left_joined(leftmonth, leftyear, joining_emp_code);


                //                    }

                //                }
                //            }
                //            catch (Exception ex) { throw ex; }
                //            finally
                //            {
                //                dr3.Close();
                //                cmd_item1.Dispose();
                //                d1.con1.Close();
                //               // d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(date_common), ddl_state.SelectedValue);
                //            }


                // Rahul Sir Code *** commenting

            }

            update_joining_left_date(function);
            if (!ddl_unitcode.SelectedValue.Equals("ALL"))
            {
                Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            
            }
            d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(date_common), ddl_state.SelectedValue);
                string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='"+ddl_unitcode.SelectedValue.ToString()+"' and client_code='"+ddl_client.SelectedValue.ToString()+"'");

            Message = ViewState["Message"].ToString();
            MySqlDataAdapter adp = null;
            update_label(mnth, year, int.Parse(date_common));
            if (date_common == "1")
            {
                where = " and pay_attendance_muster.MONTH = '" + mnth + "' and pay_attendance_muster.YEAR = '" + year + "'";
                //d.con1.Open();

                where_unit = where_unit.Replace("and unit_code", "and shift_calendar.unit_code");
                if (ot_applicable == "1")
                {
                    // adp = new MySqlDataAdapter("SELECT pay_attendance_muster.*,IFNULL(pay_ot_muster.OT_DAY01, 0) AS 'OT_DAY01',IFNULL(pay_ot_muster.OT_DAY01, 0) AS 'OT_DAY01',  IFNULL(pay_ot_muster.OT_DAY02, 0) AS 'OT_DAY02',  IFNULL(pay_ot_muster.OT_DAY03, 0) AS 'OT_DAY03',  IFNULL(pay_ot_muster.OT_DAY04, 0) AS 'OT_DAY04',  IFNULL(pay_ot_muster.OT_DAY05, 0) AS 'OT_DAY05',  IFNULL(pay_ot_muster.OT_DAY06, 0) AS 'OT_DAY06',  IFNULL(pay_ot_muster.OT_DAY07, 0) AS 'OT_DAY07',  IFNULL(pay_ot_muster.OT_DAY08, 0) AS 'OT_DAY08',  IFNULL(pay_ot_muster.OT_DAY09, 0) AS 'OT_DAY09',  IFNULL(pay_ot_muster.OT_DAY10, 0) AS 'OT_DAY10',  IFNULL(pay_ot_muster.OT_DAY11, 0) AS 'OT_DAY11',  IFNULL(pay_ot_muster.OT_DAY12, 0) AS 'OT_DAY12',  IFNULL(pay_ot_muster.OT_DAY13, 0) AS 'OT_DAY13',  IFNULL(pay_ot_muster.OT_DAY14, 0) AS 'OT_DAY14',  IFNULL(pay_ot_muster.OT_DAY15, 0) AS 'OT_DAY15',  IFNULL(pay_ot_muster.OT_DAY16, 0) AS 'OT_DAY16',  IFNULL(pay_ot_muster.OT_DAY17, 0) AS 'OT_DAY17',  IFNULL(pay_ot_muster.OT_DAY18, 0) AS 'OT_DAY18',  IFNULL(pay_ot_muster.OT_DAY19, 0) AS 'OT_DAY19',  IFNULL(pay_ot_muster.OT_DAY20, 0) AS 'OT_DAY20',  IFNULL(pay_ot_muster.OT_DAY21, 0) AS 'OT_DAY21',  IFNULL(pay_ot_muster.OT_DAY22, 0) AS 'OT_DAY22',  IFNULL(pay_ot_muster.OT_DAY23, 0) AS 'OT_DAY23',  IFNULL(pay_ot_muster.OT_DAY24, 0) AS 'OT_DAY24',  IFNULL(pay_ot_muster.OT_DAY25, 0) AS 'OT_DAY25',  IFNULL(pay_ot_muster.OT_DAY26, 0) AS 'OT_DAY26',  IFNULL(pay_ot_muster.OT_DAY27, 0) AS 'OT_DAY27',  IFNULL(pay_ot_muster.OT_DAY28, 0) AS 'OT_DAY28',  IFNULL(pay_ot_muster.OT_DAY29, 0) AS 'OT_DAY29',  IFNULL(pay_ot_muster.OT_DAY30, 0) AS 'OT_DAY30',  IFNULL(pay_ot_muster.OT_DAY31, 0) AS 'OT_DAY31',(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name' FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                       // adp = new MySqlDataAdapter("SELECT pay_attendance_muster.*,IFNULL(pay_ot_muster.OT_DAY01, 0) AS 'OT_DAY01',IFNULL(pay_ot_muster.OT_DAY01, 0) AS 'OT_DAY01',  IFNULL(pay_ot_muster.OT_DAY02, 0) AS 'OT_DAY02',  IFNULL(pay_ot_muster.OT_DAY03, 0) AS 'OT_DAY03',  IFNULL(pay_ot_muster.OT_DAY04, 0) AS 'OT_DAY04',  IFNULL(pay_ot_muster.OT_DAY05, 0) AS 'OT_DAY05',  IFNULL(pay_ot_muster.OT_DAY06, 0) AS 'OT_DAY06',  IFNULL(pay_ot_muster.OT_DAY07, 0) AS 'OT_DAY07',  IFNULL(pay_ot_muster.OT_DAY08, 0) AS 'OT_DAY08',  IFNULL(pay_ot_muster.OT_DAY09, 0) AS 'OT_DAY09',  IFNULL(pay_ot_muster.OT_DAY10, 0) AS 'OT_DAY10',  IFNULL(pay_ot_muster.OT_DAY11, 0) AS 'OT_DAY11',  IFNULL(pay_ot_muster.OT_DAY12, 0) AS 'OT_DAY12',  IFNULL(pay_ot_muster.OT_DAY13, 0) AS 'OT_DAY13',  IFNULL(pay_ot_muster.OT_DAY14, 0) AS 'OT_DAY14',  IFNULL(pay_ot_muster.OT_DAY15, 0) AS 'OT_DAY15',  IFNULL(pay_ot_muster.OT_DAY16, 0) AS 'OT_DAY16',  IFNULL(pay_ot_muster.OT_DAY17, 0) AS 'OT_DAY17',  IFNULL(pay_ot_muster.OT_DAY18, 0) AS 'OT_DAY18',  IFNULL(pay_ot_muster.OT_DAY19, 0) AS 'OT_DAY19',  IFNULL(pay_ot_muster.OT_DAY20, 0) AS 'OT_DAY20',  IFNULL(pay_ot_muster.OT_DAY21, 0) AS 'OT_DAY21',  IFNULL(pay_ot_muster.OT_DAY22, 0) AS 'OT_DAY22',  IFNULL(pay_ot_muster.OT_DAY23, 0) AS 'OT_DAY23',  IFNULL(pay_ot_muster.OT_DAY24, 0) AS 'OT_DAY24',  IFNULL(pay_ot_muster.OT_DAY25, 0) AS 'OT_DAY25',  IFNULL(pay_ot_muster.OT_DAY26, 0) AS 'OT_DAY26',  IFNULL(pay_ot_muster.OT_DAY27, 0) AS 'OT_DAY27',  IFNULL(pay_ot_muster.OT_DAY28, 0) AS 'OT_DAY28',  IFNULL(pay_ot_muster.OT_DAY29, 0) AS 'OT_DAY29',  IFNULL(pay_ot_muster.OT_DAY30, 0) AS 'OT_DAY30',  IFNULL(pay_ot_muster.OT_DAY31, 0) AS 'OT_DAY31',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY01, 0) AS 'OT_DAILY_DAY01',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY02, 0) AS 'OT_DAILY_DAY02',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY03, 0) AS 'OT_DAILY_DAY03',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY04, 0) AS 'OT_DAILY_DAY04',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY05, 0) AS 'OT_DAILY_DAY05',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY06, 0) AS 'OT_DAILY_DAY06',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY07, 0) AS 'OT_DAILY_DAY07',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY08, 0) AS 'OT_DAILY_DAY08',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY09, 0) AS 'OT_DAILY_DAY09',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY10, 0) AS 'OT_DAILY_DAY10',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY11, 0) AS 'OT_DAILY_DAY11',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY12, 0) AS 'OT_DAILY_DAY12',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY13, 0) AS 'OT_DAILY_DAY13',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY14, 0) AS 'OT_DAILY_DAY14',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY15, 0) AS 'OT_DAILY_DAY15',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY16, 0) AS 'OT_DAILY_DAY16',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY17, 0) AS 'OT_DAILY_DAY17',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY18, 0) AS 'OT_DAILY_DAY18',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY19, 0) AS 'OT_DAILY_DAY19',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY20, 0) AS 'OT_DAILY_DAY20',IFNULL( pay_daily_ot_muster . OT_DAILY_DAY21 , 0) AS 'OT_DAILY_DAY21',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY22, 0) AS 'OT_DAILY_DAY22',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY22, 0) AS 'OT_DAILY_DAY22',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY23, 0) AS 'OT_DAILY_DAY23',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY24, 0) AS 'OT_DAILY_DAY24',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY25, 0) AS 'OT_DAILY_DAY25',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY26, 0) AS 'OT_DAILY_DAY26',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY27, 0) AS 'OT_DAILY_DAY27',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY28, 0) AS 'OT_DAILY_DAY28',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY29, 0) AS 'OT_DAILY_DAY29',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY30, 0) AS 'OT_DAILY_DAY30',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY31, 0) AS 'OT_DAILY_DAY31',(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name' FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR left outer join pay_daily_ot_muster on pay_attendance_muster.EMP_CODE =pay_daily_ot_muster.EMP_CODE  and pay_attendance_muster.COMP_CODE = pay_daily_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_daily_ot_muster.UNIT_CODE  and pay_attendance_muster.MONTH = pay_daily_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_daily_ot_muster.YEAR  WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                        //rahul changes 16-10-2019 ot time format
                        //old query
                        //adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY02`, 0) AS 'OT_DAY02', IFNULL(`pay_ot_muster`.`OT_DAY03`, 0) AS 'OT_DAY03', IFNULL(`pay_ot_muster`.`OT_DAY04`, 0) AS 'OT_DAY04', IFNULL(`pay_ot_muster`.`OT_DAY05`, 0) AS 'OT_DAY05', IFNULL(`pay_ot_muster`.`OT_DAY06`, 0) AS 'OT_DAY06', IFNULL(`pay_ot_muster`.`OT_DAY07`, 0) AS 'OT_DAY07', IFNULL(`pay_ot_muster`.`OT_DAY08`, 0) AS 'OT_DAY08',               IFNULL(`pay_ot_muster`.`OT_DAY09`, 0) AS 'OT_DAY09', IFNULL(`pay_ot_muster`.`OT_DAY10`, 0) AS 'OT_DAY10', IFNULL(`pay_ot_muster`.`OT_DAY11`, 0) AS 'OT_DAY11', IFNULL(`pay_ot_muster`.`OT_DAY12`, 0) AS 'OT_DAY12', IFNULL(`pay_ot_muster`.`OT_DAY13`, 0) AS 'OT_DAY13', IFNULL(`pay_ot_muster`.`OT_DAY14`, 0) AS 'OT_DAY14', IFNULL(`pay_ot_muster`.`OT_DAY15`, 0) AS 'OT_DAY15', IFNULL(`pay_ot_muster`.`OT_DAY16`, 0) AS 'OT_DAY16', IFNULL(`pay_ot_muster`.`OT_DAY17`, 0) AS 'OT_DAY17', IFNULL(`pay_ot_muster`.`OT_DAY18`, 0) AS 'OT_DAY18', IFNULL(`pay_ot_muster`.`OT_DAY19`, 0) AS 'OT_DAY19', IFNULL(`pay_ot_muster`.`OT_DAY20`, 0) AS 'OT_DAY20', IFNULL(`pay_ot_muster`.`OT_DAY21`, 0) AS 'OT_DAY21', IFNULL(`pay_ot_muster`.`OT_DAY22`, 0) AS 'OT_DAY22', IFNULL(`pay_ot_muster`.`OT_DAY23`, 0) AS 'OT_DAY23', IFNULL(`pay_ot_muster`.`OT_DAY24`, 0) AS 'OT_DAY24', IFNULL(`pay_ot_muster`.`OT_DAY25`, 0) AS 'OT_DAY25', IFNULL(`pay_ot_muster`.`OT_DAY26`, 0) AS 'OT_DAY26', IFNULL(`pay_ot_muster`.`OT_DAY27`, 0) AS 'OT_DAY27', IFNULL(`pay_ot_muster`.`OT_DAY28`, 0) AS 'OT_DAY28', IFNULL(`pay_ot_muster`.`OT_DAY29`, 0) AS 'OT_DAY29', IFNULL(`pay_ot_muster`.`OT_DAY30`, 0) AS 'OT_DAY30', IFNULL(`pay_ot_muster`.`OT_DAY31`, 0) AS 'OT_DAY31', CASE `OT_DAILY_DAY01` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY01`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY01', CASE `OT_DAILY_DAY02` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY02`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY02', CASE `OT_DAILY_DAY03` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY03`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY03', CASE `OT_DAILY_DAY04` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY04`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY04', CASE `OT_DAILY_DAY05` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY05`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY05', CASE `OT_DAILY_DAY06` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY06`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY06', CASE `OT_DAILY_DAY07` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY07`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY07', CASE `OT_DAILY_DAY08` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY08`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY08', CASE `OT_DAILY_DAY09` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY09`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY09', CASE `OT_DAILY_DAY10` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY10`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY10', CASE `OT_DAILY_DAY11` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY11`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY11', CASE `OT_DAILY_DAY12` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY12`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY12', CASE `OT_DAILY_DAY13` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY13`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY13', CASE `OT_DAILY_DAY14` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY14`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY14', CASE `OT_DAILY_DAY15` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY15`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY15', CASE `OT_DAILY_DAY16` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY16`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY16', CASE `OT_DAILY_DAY17` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY17`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY17', CASE `OT_DAILY_DAY18` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY18`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY18', CASE `OT_DAILY_DAY19` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY19`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY19', CASE `OT_DAILY_DAY20` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY20`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY20', CASE `OT_DAILY_DAY21` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY21`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY21', CASE `OT_DAILY_DAY22` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY22`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY22', CASE `OT_DAILY_DAY23` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY23`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY23', CASE `OT_DAILY_DAY24` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY24`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY24', CASE `OT_DAILY_DAY25` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY25`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY25', CASE `OT_DAILY_DAY26` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY26`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY26', CASE `OT_DAILY_DAY27` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY27`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY27', CASE `OT_DAILY_DAY28` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY28`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY28', CASE `OT_DAILY_DAY29` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY29`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY29', CASE `OT_DAILY_DAY30` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY30`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY30', CASE `OT_DAILY_DAY31` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY31`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY31', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name' FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR`   WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);

                            //time_to_sec convert
                        //adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY02`, 0) AS 'OT_DAY02', IFNULL(`pay_ot_muster`.`OT_DAY03`, 0) AS 'OT_DAY03', IFNULL(`pay_ot_muster`.`OT_DAY04`, 0) AS 'OT_DAY04', IFNULL(`pay_ot_muster`.`OT_DAY05`, 0) AS 'OT_DAY05', IFNULL(`pay_ot_muster`.`OT_DAY06`, 0) AS 'OT_DAY06', IFNULL(`pay_ot_muster`.`OT_DAY07`, 0) AS 'OT_DAY07', IFNULL(`pay_ot_muster`.`OT_DAY08`, 0) AS 'OT_DAY08',IFNULL(`pay_ot_muster`.`OT_DAY09`, 0) AS 'OT_DAY09', IFNULL(`pay_ot_muster`.`OT_DAY10`, 0) AS 'OT_DAY10', IFNULL(`pay_ot_muster`.`OT_DAY11`, 0) AS 'OT_DAY11', IFNULL(`pay_ot_muster`.`OT_DAY12`, 0) AS 'OT_DAY12', IFNULL(`pay_ot_muster`.`OT_DAY13`, 0) AS 'OT_DAY13', IFNULL(`pay_ot_muster`.`OT_DAY14`, 0) AS 'OT_DAY14', IFNULL(`pay_ot_muster`.`OT_DAY15`, 0) AS 'OT_DAY15', IFNULL(`pay_ot_muster`.`OT_DAY16`, 0) AS 'OT_DAY16', IFNULL(`pay_ot_muster`.`OT_DAY17`, 0) AS 'OT_DAY17', IFNULL(`pay_ot_muster`.`OT_DAY18`, 0) AS 'OT_DAY18', IFNULL(`pay_ot_muster`.`OT_DAY19`, 0) AS 'OT_DAY19', IFNULL(`pay_ot_muster`.`OT_DAY20`, 0) AS 'OT_DAY20', IFNULL(`pay_ot_muster`.`OT_DAY21`, 0) AS 'OT_DAY21', IFNULL(`pay_ot_muster`.`OT_DAY22`, 0) AS 'OT_DAY22', IFNULL(`pay_ot_muster`.`OT_DAY23`, 0) AS 'OT_DAY23', IFNULL(`pay_ot_muster`.`OT_DAY24`, 0) AS 'OT_DAY24', IFNULL(`pay_ot_muster`.`OT_DAY25`, 0) AS 'OT_DAY25', IFNULL(`pay_ot_muster`.`OT_DAY26`, 0) AS 'OT_DAY26', IFNULL(`pay_ot_muster`.`OT_DAY27`, 0) AS 'OT_DAY27', IFNULL(`pay_ot_muster`.`OT_DAY28`, 0) AS 'OT_DAY28', IFNULL(`pay_ot_muster`.`OT_DAY29`, 0) AS 'OT_DAY29', IFNULL(`pay_ot_muster`.`OT_DAY30`, 0) AS 'OT_DAY30', IFNULL(`pay_ot_muster`.`OT_DAY31`, 0) AS 'OT_DAY31',CASE `OT_DAILY_DAY01` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY01`),0) END AS 'OT_DAILY_DAY01', CASE `OT_DAILY_DAY02` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY02`) ,0) END AS 'OT_DAILY_DAY02', CASE `OT_DAILY_DAY03` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY03`) ,0) END AS 'OT_DAILY_DAY03', CASE `OT_DAILY_DAY04` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY04`) ,0) END AS 'OT_DAILY_DAY04', CASE `OT_DAILY_DAY05` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY05`) ,0) END AS 'OT_DAILY_DAY05', CASE `OT_DAILY_DAY06` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY06`) ,0) END AS 'OT_DAILY_DAY06', CASE `OT_DAILY_DAY07` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY07`) ,0) END AS 'OT_DAILY_DAY07', CASE `OT_DAILY_DAY08` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY08`) ,0) END AS 'OT_DAILY_DAY08', CASE `OT_DAILY_DAY09` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY09`) ,0) END AS 'OT_DAILY_DAY09', CASE `OT_DAILY_DAY10` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY10`) ,0) END AS 'OT_DAILY_DAY10', CASE `OT_DAILY_DAY11` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY11`) ,0) END AS 'OT_DAILY_DAY11', CASE `OT_DAILY_DAY12` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY12`) ,0) END AS 'OT_DAILY_DAY12', CASE `OT_DAILY_DAY13` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY13`) ,0) END AS 'OT_DAILY_DAY13', CASE `OT_DAILY_DAY14` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY14`) ,0) END AS 'OT_DAILY_DAY14', CASE `OT_DAILY_DAY15` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY15`) ,0) END AS 'OT_DAILY_DAY15', CASE `OT_DAILY_DAY16` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY16`) ,0) END AS 'OT_DAILY_DAY16', CASE `OT_DAILY_DAY17` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY17`) ,0) END AS 'OT_DAILY_DAY17', CASE `OT_DAILY_DAY18` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY18`) ,0) END AS 'OT_DAILY_DAY18', CASE `OT_DAILY_DAY19` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY19`) ,0) END AS 'OT_DAILY_DAY19', CASE `OT_DAILY_DAY20` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY20`) ,0) END AS 'OT_DAILY_DAY20', CASE `OT_DAILY_DAY21` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY21`) ,0) END AS 'OT_DAILY_DAY21', CASE `OT_DAILY_DAY22` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY22`) ,0) END AS 'OT_DAILY_DAY22', CASE `OT_DAILY_DAY23` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY23`) ,0) END AS 'OT_DAILY_DAY23', CASE `OT_DAILY_DAY24` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY24`) ,0) END AS 'OT_DAILY_DAY24', CASE `OT_DAILY_DAY25` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY25`) ,0) END AS 'OT_DAILY_DAY25', CASE `OT_DAILY_DAY26` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY26`) ,0) END AS 'OT_DAILY_DAY26', CASE `OT_DAILY_DAY27` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY27`) ,0) END AS 'OT_DAILY_DAY27', CASE `OT_DAILY_DAY28` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY28`) ,0) END AS 'OT_DAILY_DAY28', CASE `OT_DAILY_DAY29` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY29`) ,0) END AS 'OT_DAILY_DAY29', CASE `OT_DAILY_DAY30` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY30`) ,0), 0) END AS 'OT_DAILY_DAY30', CASE `OT_DAILY_DAY31` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY31`) ,0) END AS 'OT_DAILY_DAY31',(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name',IFNULL(TIME_FORMAT(sec_to_time(`pay_daily_ot_muster`.`TOTAL_OT`),'%H:%i:%s') ,0) as TOTAL_OT  FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR`   WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);

                    adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY01`, 0) AS 'OT_DAY01', IFNULL(`pay_ot_muster`.`OT_DAY02`, 0) AS 'OT_DAY02', IFNULL(`pay_ot_muster`.`OT_DAY03`, 0) AS 'OT_DAY03', IFNULL(`pay_ot_muster`.`OT_DAY04`, 0) AS 'OT_DAY04', IFNULL(`pay_ot_muster`.`OT_DAY05`, 0) AS 'OT_DAY05', IFNULL(`pay_ot_muster`.`OT_DAY06`, 0) AS 'OT_DAY06', IFNULL(`pay_ot_muster`.`OT_DAY07`, 0) AS 'OT_DAY07', IFNULL(`pay_ot_muster`.`OT_DAY08`, 0) AS 'OT_DAY08', IFNULL(`pay_ot_muster`.`OT_DAY09`, 0) AS 'OT_DAY09', IFNULL(`pay_ot_muster`.`OT_DAY10`, 0) AS 'OT_DAY10', IFNULL(`pay_ot_muster`.`OT_DAY11`, 0) AS 'OT_DAY11', IFNULL(`pay_ot_muster`.`OT_DAY12`, 0) AS 'OT_DAY12', IFNULL(`pay_ot_muster`.`OT_DAY13`, 0) AS 'OT_DAY13', IFNULL(`pay_ot_muster`.`OT_DAY14`, 0) AS 'OT_DAY14', IFNULL(`pay_ot_muster`.`OT_DAY15`, 0) AS 'OT_DAY15', IFNULL(`pay_ot_muster`.`OT_DAY16`, 0) AS 'OT_DAY16', IFNULL(`pay_ot_muster`.`OT_DAY17`, 0) AS 'OT_DAY17', IFNULL(`pay_ot_muster`.`OT_DAY18`, 0) AS 'OT_DAY18', IFNULL(`pay_ot_muster`.`OT_DAY19`, 0) AS 'OT_DAY19', IFNULL(`pay_ot_muster`.`OT_DAY20`, 0) AS 'OT_DAY20', IFNULL(`pay_ot_muster`.`OT_DAY21`, 0) AS 'OT_DAY21', IFNULL(`pay_ot_muster`.`OT_DAY22`, 0) AS 'OT_DAY22', IFNULL(`pay_ot_muster`.`OT_DAY23`, 0) AS 'OT_DAY23', IFNULL(`pay_ot_muster`.`OT_DAY24`, 0) AS 'OT_DAY24', IFNULL(`pay_ot_muster`.`OT_DAY25`, 0) AS 'OT_DAY25', IFNULL(`pay_ot_muster`.`OT_DAY26`, 0) AS 'OT_DAY26', IFNULL(`pay_ot_muster`.`OT_DAY27`, 0) AS 'OT_DAY27', IFNULL(`pay_ot_muster`.`OT_DAY28`, 0) AS 'OT_DAY28', IFNULL(`pay_ot_muster`.`OT_DAY29`, 0) AS 'OT_DAY29', IFNULL(`pay_ot_muster`.`OT_DAY30`, 0) AS 'OT_DAY30', IFNULL(`pay_ot_muster`.`OT_DAY31`, 0) AS 'OT_DAY31', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY01`, 0) AS 'OT_DAILY_DAY01', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY02`, 0) AS 'OT_DAILY_DAY02', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY03`, 0) AS 'OT_DAILY_DAY03', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY04`, 0) AS 'OT_DAILY_DAY04', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY05`, 0) AS 'OT_DAILY_DAY05', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY06`, 0) AS 'OT_DAILY_DAY06', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY07`, 0) AS 'OT_DAILY_DAY07', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY08`, 0) AS 'OT_DAILY_DAY08', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY09`, 0) AS 'OT_DAILY_DAY09', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY10`, 0) AS 'OT_DAILY_DAY10', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY11`, 0) AS 'OT_DAILY_DAY11', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY12`, 0) AS 'OT_DAILY_DAY12', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY13`, 0) AS 'OT_DAILY_DAY13', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY14`, 0) AS 'OT_DAILY_DAY14', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY15`, 0) AS 'OT_DAILY_DAY15', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY16`, 0) AS 'OT_DAILY_DAY16', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY17`, 0) AS 'OT_DAILY_DAY17', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY18`, 0) AS 'OT_DAILY_DAY18', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY19`, 0) AS 'OT_DAILY_DAY19', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY20`, 0) AS 'OT_DAILY_DAY20', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY21`, 0) AS 'OT_DAILY_DAY21', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY22`, 0) AS 'OT_DAILY_DAY22', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY23`, 0) AS 'OT_DAILY_DAY23', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY24`, 0) AS 'OT_DAILY_DAY24', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY25`, 0) AS 'OT_DAILY_DAY25', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY26`, 0) AS 'OT_DAILY_DAY26', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY27`, 0) AS 'OT_DAILY_DAY27', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY28`, 0) AS 'OT_DAILY_DAY28', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY29`, 0) AS 'OT_DAILY_DAY29', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY30`, 0) AS 'OT_DAILY_DAY30', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY31`, 0) AS 'OT_DAILY_DAY31', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name', IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0) AS 'TOTAL_OT' FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR` WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                    
                }
                    else
                    {
                        //adp = new MySqlDataAdapter("SELECT pay_attendance_muster.*,0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31',pay_employee_master.EMP_NAME FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR WHERE pay_attendance_muster.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.EMP_CODE", d.con1);//vv
                       // adp = new MySqlDataAdapter("SELECT pay_attendance_muster.*,IFNULL(pay_daily_ot_muster.OT_DAILY_DAY01, 0) AS 'OT_DAILY_DAY01',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY02, 0) AS 'OT_DAILY_DAY02',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY03, 0) AS 'OT_DAILY_DAY03',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY04, 0) AS 'OT_DAILY_DAY04',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY05, 0) AS 'OT_DAILY_DAY05',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY06, 0) AS 'OT_DAILY_DAY06',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY07, 0) AS 'OT_DAILY_DAY07',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY08, 0) AS 'OT_DAILY_DAY08',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY09, 0) AS 'OT_DAILY_DAY09',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY10, 0) AS 'OT_DAILY_DAY10',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY11, 0) AS 'OT_DAILY_DAY11',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY12, 0) AS 'OT_DAILY_DAY12',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY13, 0) AS 'OT_DAILY_DAY13',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY14, 0) AS 'OT_DAILY_DAY14',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY15, 0) AS 'OT_DAILY_DAY15',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY16, 0) AS 'OT_DAILY_DAY16',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY17, 0) AS 'OT_DAILY_DAY17',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY18, 0) AS 'OT_DAILY_DAY18',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY19, 0) AS 'OT_DAILY_DAY19',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY20, 0) AS 'OT_DAILY_DAY20',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY21, 0) AS 'OT_DAILY_DAY21',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY22, 0) AS 'OT_DAILY_DAY22',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY23, 0) AS 'OT_DAILY_DAY23',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY24, 0) AS 'OT_DAILY_DAY24',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY25, 0) AS 'OT_DAILY_DAY25',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY26, 0) AS 'OT_DAILY_DAY26',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY27, 0) AS 'OT_DAILY_DAY27',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY28, 0) AS 'OT_DAILY_DAY28',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY29, 0) AS 'OT_DAILY_DAY29',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY30, 0) AS 'OT_DAILY_DAY30',IFNULL(pay_daily_ot_muster.OT_DAILY_DAY31, 0) AS 'OT_DAILY_DAY31',0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31',(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name' FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_employee_master.EMP_CODE =pay_ot_muster.EMP_CODE and pay_employee_master.COMP_CODE = pay_ot_muster.COMP_CODE and pay_employee_master.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR left outer join pay_daily_ot_muster on pay_employee_master.EMP_CODE =pay_daily_ot_muster.EMP_CODE and pay_employee_master.COMP_CODE = pay_daily_ot_muster.COMP_CODE and pay_employee_master.UNIT_CODE = pay_daily_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_daily_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_daily_ot_muster.YEAR  WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.emp_name", d.con1);
                        
                        //rahul changes 16-10-2019 ot time format
                       //old query
                       // adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, CASE `OT_DAILY_DAY01` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY01`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY01', CASE `OT_DAILY_DAY02` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY02`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY02', CASE `OT_DAILY_DAY03` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY03`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY03', CASE `OT_DAILY_DAY04` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY04`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY04', CASE `OT_DAILY_DAY05` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY05`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY05', CASE `OT_DAILY_DAY06` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY06`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY06', CASE `OT_DAILY_DAY07` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY07`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY07', CASE `OT_DAILY_DAY08` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY08`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY08', CASE `OT_DAILY_DAY09` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY09`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY09', CASE `OT_DAILY_DAY10` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY10`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY10', CASE `OT_DAILY_DAY11` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY11`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY11', CASE `OT_DAILY_DAY12` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY12`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY12', CASE `OT_DAILY_DAY13` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY13`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY13', CASE `OT_DAILY_DAY14` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY14`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY14', CASE `OT_DAILY_DAY15` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY15`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY15', CASE `OT_DAILY_DAY16` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY16`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY16', CASE `OT_DAILY_DAY17` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY17`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY17', CASE `OT_DAILY_DAY18` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY18`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY18', CASE `OT_DAILY_DAY19` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY19`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY19', CASE `OT_DAILY_DAY20` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY20`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY20', CASE `OT_DAILY_DAY21` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY21`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY21', CASE `OT_DAILY_DAY22` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY22`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY22', CASE `OT_DAILY_DAY23` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY23`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY23', CASE `OT_DAILY_DAY24` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY24`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY24', CASE `OT_DAILY_DAY25` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY25`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY25', CASE `OT_DAILY_DAY26` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY26`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY26', CASE `OT_DAILY_DAY27` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY27`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY27', CASE `OT_DAILY_DAY28` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY28`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY28', CASE `OT_DAILY_DAY29` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY29`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY29', CASE `OT_DAILY_DAY30` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY30`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY30', CASE `OT_DAILY_DAY31` WHEN 0 THEN 0 ELSE IFNULL(SUBSTRING(TIME(FROM_UNIXTIME((`pay_daily_ot_muster`.`OT_DAILY_DAY31`) / 1000)),1, 8), 0) END AS 'OT_DAILY_DAY31', 0 AS 'OT_DAY01', 0 AS 'OT_DAY02', 0 AS 'OT_DAY03', 0 AS 'OT_DAY04', 0 AS 'OT_DAY05', 0 AS 'OT_DAY06', 0 AS 'OT_DAY07', 0 AS 'OT_DAY08', 0 AS 'OT_DAY09', 0 AS 'OT_DAY10', 0 AS 'OT_DAY11', 0 AS 'OT_DAY12', 0 AS 'OT_DAY13', 0 AS 'OT_DAY14', 0 AS 'OT_DAY15', 0 AS 'OT_DAY16', 0 AS 'OT_DAY17', 0 AS 'OT_DAY18', 0 AS 'OT_DAY19', 0 AS 'OT_DAY20', 0 AS 'OT_DAY21', 0 AS 'OT_DAY22', 0 AS 'OT_DAY23', 0 AS 'OT_DAY24', 0 AS 'OT_DAY25', 0 AS 'OT_DAY26', 0 AS 'OT_DAY27', 0 AS 'OT_DAY28', 0 AS 'OT_DAY29', 0 AS 'OT_DAY30', 0 AS 'OT_DAY31', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name' FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR`  WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.emp_name", d.con1);

                        // time to sec convert 
                      //  adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, CASE `OT_DAILY_DAY01` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY01`) ,0) END AS 'OT_DAILY_DAY01', CASE `OT_DAILY_DAY02` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY02`) ,0) END AS 'OT_DAILY_DAY02', CASE `OT_DAILY_DAY03` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY03`) ,0) END AS 'OT_DAILY_DAY03', CASE `OT_DAILY_DAY04` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY04`) ,0) END AS 'OT_DAILY_DAY04', CASE `OT_DAILY_DAY05` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY05`) ,0) END AS 'OT_DAILY_DAY05', CASE `OT_DAILY_DAY06` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY06`) ,0) END AS 'OT_DAILY_DAY06', CASE `OT_DAILY_DAY07` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY07`) ,0) END AS 'OT_DAILY_DAY07', CASE `OT_DAILY_DAY08` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY08`) ,0) END AS 'OT_DAILY_DAY08', CASE `OT_DAILY_DAY09` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY09`) ,0) END AS 'OT_DAILY_DAY09', CASE `OT_DAILY_DAY10` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY10`) ,0) END AS 'OT_DAILY_DAY10', CASE `OT_DAILY_DAY11` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY11`) ,0) END AS 'OT_DAILY_DAY11', CASE `OT_DAILY_DAY12` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY12`) ,0) END AS 'OT_DAILY_DAY12', CASE `OT_DAILY_DAY13` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY13`) ,0) END AS 'OT_DAILY_DAY13', CASE `OT_DAILY_DAY14` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY14`) ,0) END AS 'OT_DAILY_DAY14', CASE `OT_DAILY_DAY15` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY15`) ,0) END AS 'OT_DAILY_DAY15', CASE `OT_DAILY_DAY16` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY16`) ,0) END AS 'OT_DAILY_DAY16', CASE `OT_DAILY_DAY17` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY17`) ,0) END AS 'OT_DAILY_DAY17', CASE `OT_DAILY_DAY18` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY18`) ,0) END AS 'OT_DAILY_DAY18', CASE `OT_DAILY_DAY19` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY19`) ,0) END AS 'OT_DAILY_DAY19', CASE `OT_DAILY_DAY20` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY20`) ,0) END AS 'OT_DAILY_DAY20', CASE `OT_DAILY_DAY21` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY21`) ,0) END AS 'OT_DAILY_DAY21', CASE `OT_DAILY_DAY22` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY22`) ,0) END AS 'OT_DAILY_DAY22', CASE `OT_DAILY_DAY23` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY23`) ,0) END AS 'OT_DAILY_DAY23', CASE `OT_DAILY_DAY24` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY24`) ,0) END AS 'OT_DAILY_DAY24', CASE `OT_DAILY_DAY25` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY25`) ,0) END AS 'OT_DAILY_DAY25', CASE `OT_DAILY_DAY26` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY26`) ,0) END AS 'OT_DAILY_DAY26', CASE `OT_DAILY_DAY27` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY27`) ,0) END AS 'OT_DAILY_DAY27', CASE `OT_DAILY_DAY28` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY28`) ,0) END AS 'OT_DAILY_DAY28', CASE `OT_DAILY_DAY29` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY29`) ,0) END AS 'OT_DAILY_DAY29', CASE `OT_DAILY_DAY30` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY30`) ,0) END AS 'OT_DAILY_DAY30', CASE `OT_DAILY_DAY31` WHEN 0 THEN 0 ELSE IFNULL(sec_to_time(`pay_daily_ot_muster`.`OT_DAILY_DAY31`) ,0) END AS 'OT_DAILY_DAY31', 0 AS 'OT_DAY01', 0 AS 'OT_DAY02', 0 AS 'OT_DAY03', 0 AS 'OT_DAY04', 0 AS 'OT_DAY05', 0 AS 'OT_DAY06', 0 AS 'OT_DAY07', 0 AS 'OT_DAY08', 0 AS 'OT_DAY09', 0 AS 'OT_DAY10', 0 AS 'OT_DAY11', 0 AS 'OT_DAY12', 0 AS 'OT_DAY13', 0 AS 'OT_DAY14', 0 AS 'OT_DAY15', 0 AS 'OT_DAY16', 0 AS 'OT_DAY17', 0 AS 'OT_DAY18', 0 AS 'OT_DAY19', 0 AS 'OT_DAY20', 0 AS 'OT_DAY21', 0 AS 'OT_DAY22', 0 AS 'OT_DAY23', 0 AS 'OT_DAY24', 0 AS 'OT_DAY25', 0 AS 'OT_DAY26', 0 AS 'OT_DAY27', 0 AS 'OT_DAY28', 0 AS 'OT_DAY29', 0 AS 'OT_DAY30', 0 AS 'OT_DAY31', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name',IFNULL(TIME_FORMAT(sec_to_time(`pay_daily_ot_muster`.`TOTAL_OT`),'%H:%i:%s') ,0) as TOTAL_OT FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR`  WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.emp_name", d.con1);

                        adp = new MySqlDataAdapter("SELECT `pay_attendance_muster`.*, IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY01`, 0) AS 'OT_DAILY_DAY01', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY02`, 0) AS 'OT_DAILY_DAY02', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY03`, 0) AS 'OT_DAILY_DAY03', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY04`, 0) AS 'OT_DAILY_DAY04', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY05`, 0) AS 'OT_DAILY_DAY05', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY06`, 0) AS 'OT_DAILY_DAY06', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY07`, 0) AS 'OT_DAILY_DAY07', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY08`, 0) AS 'OT_DAILY_DAY08', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY09`, 0) AS 'OT_DAILY_DAY09', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY10`, 0) AS 'OT_DAILY_DAY10', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY11`, 0) AS 'OT_DAILY_DAY11', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY12`, 0) AS 'OT_DAILY_DAY12', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY13`, 0) AS 'OT_DAILY_DAY13', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY14`, 0) AS 'OT_DAILY_DAY14', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY15`, 0) AS 'OT_DAILY_DAY15', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY16`, 0) AS 'OT_DAILY_DAY16', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY17`, 0) AS 'OT_DAILY_DAY17', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY18`, 0) AS 'OT_DAILY_DAY18', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY19`, 0) AS 'OT_DAILY_DAY19', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY20`, 0) AS 'OT_DAILY_DAY20', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY21`, 0) AS 'OT_DAILY_DAY21', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY22`, 0) AS 'OT_DAILY_DAY22', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY23`, 0) AS 'OT_DAILY_DAY23', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY24`, 0) AS 'OT_DAILY_DAY24', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY25`, 0) AS 'OT_DAILY_DAY25', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY26`, 0) AS 'OT_DAILY_DAY26', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY27`, 0) AS 'OT_DAILY_DAY27', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY28`, 0) AS 'OT_DAILY_DAY28', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY29`, 0) AS 'OT_DAILY_DAY29', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY30`, 0) AS 'OT_DAILY_DAY30', IFNULL(`pay_daily_ot_muster`.`OT_DAILY_DAY31`, 0) AS 'OT_DAILY_DAY31', 0 AS 'OT_DAY01', 0 AS 'OT_DAY02', 0 AS 'OT_DAY03', 0 AS 'OT_DAY04', 0 AS 'OT_DAY05', 0 AS 'OT_DAY06', 0 AS 'OT_DAY07', 0 AS 'OT_DAY08', 0 AS 'OT_DAY09', 0 AS 'OT_DAY10', 0 AS 'OT_DAY11', 0 AS 'OT_DAY12', 0 AS 'OT_DAY13', 0 AS 'OT_DAY14', 0 AS 'OT_DAY15', 0 AS 'OT_DAY16', 0 AS 'OT_DAY17', 0 AS 'OT_DAY18', 0 AS 'OT_DAY19', 0 AS 'OT_DAY20', 0 AS 'OT_DAY21', 0 AS 'OT_DAY22', 0 AS 'OT_DAY23', 0 AS 'OT_DAY24', 0 AS 'OT_DAY25', 0 AS 'OT_DAY26', 0 AS 'OT_DAY27', 0 AS 'OT_DAY28', 0 AS 'OT_DAY29', 0 AS 'OT_DAY30', 0 AS 'OT_DAY31', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'emp_name', IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0) AS 'TOTAL_OT' FROM `pay_attendance_muster` INNER JOIN `pay_employee_master` ON `pay_attendance_muster`.`EMP_CODE` = `pay_employee_master`.`EMP_CODE` AND `pay_attendance_muster`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` AND `pay_attendance_muster`.`UNIT_CODE` = `pay_employee_master`.`UNIT_CODE` LEFT OUTER JOIN `pay_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_ot_muster`.`YEAR` LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_attendance_muster`.`MONTH` = `pay_daily_ot_muster`.`MONTH` AND `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR` WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " " + where + " ORDER BY pay_employee_master.emp_name", d.con1);
                    
                    }
            }
            else
            {
                //for 2  months timehseet start
                string getdays = d.get_calendar_days(int.Parse(date_common), (mnth.ToString().Length == 2 ? mnth.ToString() : "0" + mnth.ToString()) + "/" + year, 1, 1);

                if (!getdays.Contains("DAY31"))
                {
                    getdays = getdays + " 0 as 'DAY31',";
                }
                if (!getdays.Contains("DAY30"))
                {
                    getdays = getdays + " 0 as 'DAY30',";
                }
                if (!getdays.Contains("DAY29"))
                {
                    getdays = getdays + " 0 as 'DAY29',";
                }
                //vikas add 06/05/2019
                if (ot_applicable == "2" || ot_applicable == "1")
                {
                    if (ot_applicable == "2")
                    {
                        string get_otday = d.get_calendar_days(int.Parse(date_common), (mnth.ToString().Length == 2 ? mnth.ToString() : "0" + mnth.ToString()) + "/" + year, 4, 1);
                        if (!get_otday.Contains("OT_DAILY_DAY31"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY31',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY30"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAY30',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY29"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY29',";
                        }
                            //changes query rahul
                            adp = new MySqlDataAdapter("SELECT " + getdays + "" + get_otday + " pay_attendance_muster.TOT_DAYS_PRESENT,0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31', pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES, pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS, pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE,IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`,0) as TOTAL_OT FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN pay_ot_muster t3 ON  " + (mnth == 1 ? year - 1 : year) + " = t3.YEAR AND pay_employee_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_employee_master.COMP_CODE = t3.COMP_CODE AND t3.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` t4  ON  " + (mnth == 1 ? year - 1 : year) + " = `t4`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `t4`.`month` = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_daily_ot_muster`.`month` = `pay_attendance_muster`.`MONTH` WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " and pay_attendance_muster.month = '" + hidden_month.Value + "' and  pay_attendance_muster.year = '" + hidden_year.Value + "' " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                        }
                        //end code 06/05/2019

                    else if (ot_applicable == "1")
                    {
                        string get_otdays = d.get_calendar_days(int.Parse(date_common), (mnth.ToString().Length == 2 ? mnth.ToString() : "0" + mnth.ToString()) + "/" + year, 3, 1);
                        if (!get_otdays.Contains("OT_DAY31"))
                        {
                            get_otdays = get_otdays + " 0 as 'OT_DAY31',";
                        }
                        if (!get_otdays.Contains("OT_DAY30"))
                        {
                            get_otdays = get_otdays + " 0 as 'OT_DAY30',";
                        }
                        if (!get_otdays.Contains("OT_DAY29"))
                        {
                            get_otdays = get_otdays + " 0 as 'OT_DAY29',";
                        }
                        //  adp = new MySqlDataAdapter("SELECT " + getdays + "" + get_otdays + "" + get_otday + " pay_attendance_muster.TOT_DAYS_PRESENT, pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES, pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS, pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN pay_ot_muster t3 ON  " + (mnth == 1 ? year - 1 : year) + " = t3.YEAR AND pay_employee_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_employee_master.COMP_CODE = t3.COMP_CODE AND t3.month = " + (mnth == 1 ? 12 : mnth - 1) + " WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " and pay_attendance_muster.month = '" + hidden_month.Value + "' and  pay_attendance_muster.year = '" + hidden_year.Value + "' " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                            adp = new MySqlDataAdapter("SELECT " + getdays + "" + get_otdays + "0 AS 'OT_DAILY_DAY01',0 AS 'OT_DAILY_DAY02',0 AS 'OT_DAILY_DAY03',0 AS 'OT_DAILY_DAY04',0 AS 'OT_DAILY_DAY05',0 AS 'OT_DAILY_DAY06',0 AS 'OT_DAILY_DAY07',0 AS 'OT_DAILY_DAY08',0 AS 'OT_DAILY_DAY09',0 AS 'OT_DAILY_DAY10',0 AS 'OT_DAILY_DAY11',0 AS 'OT_DAILY_DAY12',0 AS 'OT_DAILY_DAY13',0 AS 'OT_DAILY_DAY14',0 AS 'OT_DAILY_DAY15',0 AS 'OT_DAILY_DAY16',0 AS 'OT_DAILY_DAY17',0 AS 'OT_DAILY_DAY18',0 AS 'OT_DAILY_DAY19',0 AS 'OT_DAILY_DAY20',0 AS 'OT_DAILY_DAY21',0 AS 'OT_DAILY_DAY22',0 AS 'OT_DAILY_DAY23',0 AS 'OT_DAILY_DAY24',0 AS 'OT_DAILY_DAY25',0 AS 'OT_DAILY_DAY26',0 AS 'OT_DAILY_DAY27',0 AS 'OT_DAILY_DAY28',0 AS 'OT_DAILY_DAY29',0 AS 'OT_DAILY_DAY30',0 AS 'OT_DAILY_DAY31', pay_attendance_muster.TOT_DAYS_PRESENT, pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES, pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS, pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE,IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`,0) as TOTAL_OT FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN pay_ot_muster t3 ON  " + (mnth == 1 ? year - 1 : year) + " = t3.YEAR AND pay_employee_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_employee_master.COMP_CODE = t3.COMP_CODE AND t3.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` t4  ON  " + (mnth == 1 ? year - 1 : year) + " = `t4`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `t4`.`month` = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_daily_ot_muster`.`month` = `pay_attendance_muster`.`MONTH` WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " and pay_attendance_muster.month = '" + hidden_month.Value + "' and  pay_attendance_muster.year = '" + hidden_year.Value + "' " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                    }
                }
                else
                {
                    //string function1 = where_function();
                    string function1 = where_function;
                    string unit_code = "";

                    if (ddl_unitcode.SelectedValue == "ALL")
                    {
                        unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function1 + ")  ";
                    }
                    else
                    {
                        Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                        //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                        unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
                    }

                    if (tempflag == "yes")
                    {
                        string get_otday = d.get_calendar_days(int.Parse(date_common), (mnth.ToString().Length == 2 ? mnth.ToString() : "0" + mnth.ToString()) + "/" + year, 4, 1);
                        if (!get_otday.Contains("OT_DAILY_DAY31"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY31',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY30"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAY30',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY29"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY29',";
                        }
                        //changes query rahul
                        adp = new MySqlDataAdapter("SELECT " + getdays + "" + get_otday + " pay_attendance_muster.TOT_DAYS_PRESENT,0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31', pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES, pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS, pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE,IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`,0) as TOTAL_OT FROM pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.EMP_CODE =pay_employee_master.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE left outer join pay_ot_muster on pay_attendance_muster.EMP_CODE =pay_ot_muster.EMP_CODE and pay_attendance_muster.COMP_CODE = pay_ot_muster.COMP_CODE and pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN pay_ot_muster t3 ON  " + (mnth == 1 ? year - 1 : year) + " = t3.YEAR AND pay_employee_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_employee_master.COMP_CODE = t3.COMP_CODE AND t3.month = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` t4  ON  " + (mnth == 1 ? year - 1 : year) + " = `t4`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `t4`.`month` = " + (mnth == 1 ? 12 : mnth - 1) + " LEFT OUTER JOIN `pay_daily_ot_muster` ON `pay_attendance_muster`.`YEAR` = `pay_daily_ot_muster`.`YEAR` AND `pay_employee_master`.`UNIT_CODE` = `pay_daily_ot_muster`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_daily_ot_muster`.`EMP_CODE` AND `pay_employee_master`.`COMP_CODE` = `pay_daily_ot_muster`.`COMP_CODE` AND `pay_daily_ot_muster`.`month` = `pay_attendance_muster`.`MONTH` WHERE pay_employee_master.COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'" + where_unit + " and pay_attendance_muster.month = '" + hidden_month.Value + "' and  pay_attendance_muster.year = '" + hidden_year.Value + "' " + where + " ORDER BY pay_employee_master.EMP_name", d.con1);
                 
                    }else {
                        adp = new MySqlDataAdapter("SELECT " + getdays + " pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE,0 AS 'OT_DAILY_DAY01',0 AS 'OT_DAILY_DAY02',0 AS 'OT_DAILY_DAY03',0 AS 'OT_DAILY_DAY04',0 AS 'OT_DAILY_DAY05',0 AS 'OT_DAILY_DAY06',0 AS 'OT_DAILY_DAY07',0 AS 'OT_DAILY_DAY08',0 AS 'OT_DAILY_DAY09',0 AS 'OT_DAILY_DAY10',0 AS 'OT_DAILY_DAY11',0 AS 'OT_DAILY_DAY12',0 AS 'OT_DAILY_DAY13',0 AS 'OT_DAILY_DAY14',0 AS 'OT_DAILY_DAY15',0 AS 'OT_DAILY_DAY16',0 AS 'OT_DAILY_DAY17',0 AS 'OT_DAILY_DAY18',0 AS 'OT_DAILY_DAY19',0 AS 'OT_DAILY_DAY20',0 AS 'OT_DAILY_DAY21',0 AS 'OT_DAILY_DAY22',0 AS 'OT_DAILY_DAY23',0 AS 'OT_DAILY_DAY24',0 AS 'OT_DAILY_DAY25',0 AS 'OT_DAILY_DAY26',0 AS 'OT_DAILY_DAY27',0 AS 'OT_DAILY_DAY28',0 AS 'OT_DAILY_DAY29',0 AS 'OT_DAILY_DAY30',0 AS 'OT_DAILY_DAY31',0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31',pay_attendance_muster.TOT_DAYS_PRESENT, pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES,pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS,pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',0 as TOTAL_OT FROM pay_attendance_muster LEFT JOIN pay_employee_master ON " + year + " = pay_attendance_muster.year AND pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE AND pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance_muster.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance_muster.month = " + mnth + " LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " WHERE pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.unit_code IN(" + unit_code + ") order by emp_name", d.con1);
                        //adp = new MySqlDataAdapter("SELECT Distinct " + getdays + " pay_employee_master.emp_CODE, pay_employee_master.UNIT_CODE,0 AS 'OT_DAILY_DAY01',0 AS 'OT_DAILY_DAY02',0 AS 'OT_DAILY_DAY03',0 AS 'OT_DAILY_DAY04',0 AS 'OT_DAILY_DAY05',0 AS 'OT_DAILY_DAY06',0 AS 'OT_DAILY_DAY07',0 AS 'OT_DAILY_DAY08',0 AS 'OT_DAILY_DAY09',0 AS 'OT_DAILY_DAY10',0 AS 'OT_DAILY_DAY11',0 AS 'OT_DAILY_DAY12',0 AS 'OT_DAILY_DAY13',0 AS 'OT_DAILY_DAY14',0 AS 'OT_DAILY_DAY15',0 AS 'OT_DAILY_DAY16',0 AS 'OT_DAILY_DAY17',0 AS 'OT_DAILY_DAY18',0 AS 'OT_DAILY_DAY19',0 AS 'OT_DAILY_DAY20',0 AS 'OT_DAILY_DAY21',0 AS 'OT_DAILY_DAY22',0 AS 'OT_DAILY_DAY23',0 AS 'OT_DAILY_DAY24',0 AS 'OT_DAILY_DAY25',0 AS 'OT_DAILY_DAY26',0 AS 'OT_DAILY_DAY27',0 AS 'OT_DAILY_DAY28',0 AS 'OT_DAILY_DAY29',0 AS 'OT_DAILY_DAY30',0 AS 'OT_DAILY_DAY31',0 AS 'OT_DAY01',0 AS 'OT_DAY02',0 AS 'OT_DAY03',0 AS 'OT_DAY04',0 AS 'OT_DAY05',0 AS 'OT_DAY06',0 AS 'OT_DAY07',0 AS 'OT_DAY08',0 AS 'OT_DAY09',0 AS 'OT_DAY10',0 AS 'OT_DAY11',0 AS 'OT_DAY12',0 AS 'OT_DAY13',0 AS 'OT_DAY14',0 AS 'OT_DAY15',0 AS 'OT_DAY16',0 AS 'OT_DAY17',0 AS 'OT_DAY18',0 AS 'OT_DAY19',0 AS 'OT_DAY20',0 AS 'OT_DAY21',0 AS 'OT_DAY22',0 AS 'OT_DAY23',0 AS 'OT_DAY24',0 AS 'OT_DAY25',0 AS 'OT_DAY26',0 AS 'OT_DAY27',0 AS 'OT_DAY28',0 AS 'OT_DAY29',0 AS 'OT_DAY30',0 AS 'OT_DAY31',pay_attendance_muster.TOT_DAYS_PRESENT, pay_attendance_muster.TOT_DAYS_ABSENT,pay_attendance_muster.TOT_LEAVES,pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_WORKING_DAYS,pay_attendance_muster.ot_hours,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME' FROM pay_attendance_muster LEFT JOIN pay_employee_master ON " + year + " = pay_attendance_muster.year AND pay_attendance_muster.COMP_CODE = pay_employee_master.COMP_CODE AND pay_attendance_muster.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance_muster.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance_muster.month = " + mnth + " LEFT JOIN pay_attendance_muster t2 ON t2.Year = " + (mnth == 1 ? year - 1 : year) + " AND pay_employee_master.COMP_CODE = t2.COMP_CODE AND pay_employee_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE AND t2.month = " + (mnth == 1 ? 12 : mnth - 1) + " WHERE pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.unit_code IN(" + unit_code + ") order by emp_name", d.con1);

                    }

                    
                }
            }
            //for 2  months timehseet end
            DataSet ds = new DataSet();
            adp.Fill(ds);
           // DataTable dt = new DataTable();
            //adp.Fill(dt);

            gv_attendance.DataSource = null;
            gv_attendance.DataBind();
            gv_fullmonthot.DataSource = null;
            gv_fullmonthot.DataBind();
            gv_attendance.DataSource = ds.Tables[0];
            //gv_attendance.DataSource = dt;
            gv_attendance.DataBind();

           // if (dt.Rows.Count > 0)
            if (ds.Tables[0].Rows.Count > 0)
            {
                loadheaders(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), int.Parse(date_common));
            }
            return true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }


    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string function = where_function();
        gv_attendace_load(function);
        gv_approve_attendace_load(function);

        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + ") ";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }


        if (d1.getsinglestring("select arrears_flag from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' AND unit_code IN(" + unit_code + ")").Equals("1"))
        {

        }

        else
        {
            if (d1.getsinglestring("select flag from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'").Equals("1"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Timesheet already approved, You cannot make changes !!')", true);
                return;
            }
        }
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "' and client_code='" + ddl_client.SelectedValue.ToString() + "'");

        //GridViewRow row = ((Control)sender).NamingContainer as GridViewRow;
        //if (checkcount() >= 50)
        //{
        int days = (CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2));
        int mnth1 = int.Parse(hidden_month.Value), year1 = int.Parse(hidden_year.Value);
        // string date_common = d1.getsinglestring("select start_date_common FROM pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_billing_master_history.billing_unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code ='" + ddl_client.SelectedValue + "' and pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' limit 1");
        string date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");

        if (date_common != "" && date_common != "1")
        {
            mnth1 = --mnth1;
            if (mnth1 == 0) { mnth1 = 12; year1 = --year1; }

        }
        else
        { date_common = "1"; }

        foreach (GridViewRow row in gv_attendance.Rows)
        {
            if (row != null)
            {
                DropDownList ddl1 = (DropDownList)row.FindControl("DropDownList1");
                DropDownList ddl2 = (DropDownList)row.FindControl("DropDownList2");
                DropDownList ddl3 = (DropDownList)row.FindControl("DropDownList3");
                DropDownList ddl4 = (DropDownList)row.FindControl("DropDownList4");
                DropDownList ddl5 = (DropDownList)row.FindControl("DropDownList5");
                DropDownList ddl6 = (DropDownList)row.FindControl("DropDownList6");
                DropDownList ddl7 = (DropDownList)row.FindControl("DropDownList7");
                DropDownList ddl8 = (DropDownList)row.FindControl("DropDownList8");
                DropDownList ddl9 = (DropDownList)row.FindControl("DropDownList9");
                DropDownList ddl10 = (DropDownList)row.FindControl("DropDownList10");
                DropDownList ddl11 = (DropDownList)row.FindControl("DropDownList11");
                DropDownList ddl12 = (DropDownList)row.FindControl("DropDownList12");
                DropDownList ddl13 = (DropDownList)row.FindControl("DropDownList13");
                DropDownList ddl14 = (DropDownList)row.FindControl("DropDownList14");
                DropDownList ddl15 = (DropDownList)row.FindControl("DropDownList15");
                DropDownList ddl16 = (DropDownList)row.FindControl("DropDownList16");
                DropDownList ddl17 = (DropDownList)row.FindControl("DropDownList17");
                DropDownList ddl18 = (DropDownList)row.FindControl("DropDownList18");
                DropDownList ddl19 = (DropDownList)row.FindControl("DropDownList19");
                DropDownList ddl20 = (DropDownList)row.FindControl("DropDownList20");
                DropDownList ddl21 = (DropDownList)row.FindControl("DropDownList21");
                DropDownList ddl22 = (DropDownList)row.FindControl("DropDownList22");
                DropDownList ddl23 = (DropDownList)row.FindControl("DropDownList23");
                DropDownList ddl24 = (DropDownList)row.FindControl("DropDownList24");
                DropDownList ddl25 = (DropDownList)row.FindControl("DropDownList25");
                DropDownList ddl26 = (DropDownList)row.FindControl("DropDownList26");
                DropDownList ddl27 = (DropDownList)row.FindControl("DropDownList27");
                DropDownList ddl28 = (DropDownList)row.FindControl("DropDownList28");
                DropDownList ddl29 = (DropDownList)row.FindControl("DropDownList29");
                DropDownList ddl30 = (DropDownList)row.FindControl("DropDownList30");
                DropDownList ddl31 = (DropDownList)row.FindControl("DropDownList31");
                TextBox txt_ot_hour = (TextBox)row.FindControl("txt_ot_hours");
                txt_ot_hour.Text = txt_ot_hour.Text.Replace(",", "");
                //double pcount = 0;
                //double acount = 0;
                //double halfdaycount = 0;
                //double leavescount = 0;
                //double holidaycount = 0;
                //double weeklyoff = 0;


                // double cocount = 0;

                //for (int j = 1; j <= days; j++)
                //{
                //    string cntrlname = "DropDownList" + j.ToString();
                //    DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                //    if (txt_day1.SelectedValue == "P")
                //    {
                //        pcount++;
                //    }
                //    else if (txt_day1.SelectedValue == "A")
                //    {
                //        acount++;
                //    }
                //    else if (txt_day1.SelectedValue == "HD")
                //    {
                //        halfdaycount++;
                //    }
                //    else if (txt_day1.SelectedValue == "L")
                //    {
                //        leavescount++;
                //    }
                //    else if (txt_day1.SelectedValue == "W")
                //    {
                //        weeklyoff++;
                //    }
                //    else if (txt_day1.SelectedValue == "H")
                //    {
                //        holidaycount++;
                //    }
                //    else if (txt_day1.SelectedValue == "CL")
                //    {
                //        leavescount++;
                //    }
                //    else if (txt_day1.SelectedValue == "PL")
                //    {
                //        leavescount++;
                //    }
                //    else if (txt_day1.SelectedValue == "ML")
                //    {
                //        leavescount++;
                //    }
                //    else if (txt_day1.SelectedValue == "PH")
                //    {
                //        //leavescount++;
                //        pcount++;
                //    }
                //    //else if (txt_day1.SelectedValue == "CO")
                //    //{
                //    //    cocount++;
                //    //}
                //}
                //halfdaycount = halfdaycount / 2;
                //pcount = halfdaycount + pcount;

                //" + (txt_ot_hour.Text.Trim() == "" ? "0" : txt_ot_hour.Text) + "otnew"


                if (date_common == "1")
                {
                    d.operation("Update pay_attendance_muster set DAY01='" + ddl1.SelectedValue + "', DAY02='" + ddl2.SelectedValue + "', DAY03='" + ddl3.SelectedValue + "', DAY04='" + ddl4.SelectedValue + "', DAY05='" + ddl5.SelectedValue + "', DAY06='" + ddl6.SelectedValue + "', DAY07='" + ddl7.SelectedValue + "', DAY08='" + ddl8.SelectedValue + "', DAY09='" + ddl9.SelectedValue + "', DAY10='" + ddl10.SelectedValue + "', DAY11='" + ddl11.SelectedValue + "', DAY12='" + ddl12.SelectedValue + "', DAY13='" + ddl13.SelectedValue + "', DAY14='" + ddl14.SelectedValue + "', DAY15='" + ddl15.SelectedValue + "', DAY16='" + ddl16.SelectedValue + "', DAY17='" + ddl17.SelectedValue + "', DAY18='" + ddl18.SelectedValue + "', DAY19='" + ddl19.SelectedValue + "', DAY20='" + ddl20.SelectedValue + "', DAY21='" + ddl21.SelectedValue + "', DAY22='" + ddl22.SelectedValue + "', DAY23='" + ddl23.SelectedValue + "', DAY24='" + ddl24.SelectedValue + "', DAY25='" + ddl25.SelectedValue + "', DAY26='" + ddl26.SelectedValue + "', DAY27='" + ddl27.SelectedValue + "', DAY28='" + ddl28.SelectedValue + "', DAY29='" + ddl29.SelectedValue + "', DAY30='" + ddl30.SelectedValue + "', DAY31='" + ddl31.SelectedValue + "', ot_hours=" + (txt_ot_hour.Text.Trim() == "" ? "0" : txt_ot_hour.Text) + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");
                }
                else
                {
                    int datecommon = int.Parse(date_common);
                    int month1 = 0; int j = 41, k = datecommon, l = 1;
                    month1 = mnth1;
                    //year1 = year1;
                    string query = "";
                    //--month1;
                    if (month1 == 0) { month1 = 12; year1 = year1 - 1; }
                    int daysinmonth = DateTime.DaysInMonth(year1, month1);
                    DateTime dt = new DateTime(year1, month1, 1);
                    for (int i = 0; daysinmonth >= (i + datecommon); i++)
                    {
                        if (datecommon < 10)
                        {
                            string cntrlname = "DropDownList" + l.ToString();
                            DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                            query = query + " day0" + (datecommon + i) + "= '" + txt_day1.SelectedValue + "',";
                            ++l;
                        }
                        else
                        {
                            string cntrlname = "DropDownList" + l.ToString();
                            DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                            query = query + " day" + (datecommon + i) + "= '" + txt_day1.SelectedValue + "',";
                            ++l;
                        }
                    }
                    d.operation("Update pay_attendance_muster set " + query.Substring(0, query.Length - 1) + "  where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + month1 + " and year = " + year1 + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                    k = 1;
                    query = "";
                    dt = new DateTime(year1, mnth1, 1);
                    for (int i = 1; (datecommon) > i; i++)
                    {
                        if (i < 10)
                        {
                            string cntrlname = "DropDownList" + l.ToString();
                            DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                            query = query + " day0" + i + "= '" + txt_day1.SelectedValue + "',";
                            ++l;
                        }
                        else
                        {
                            string cntrlname = "DropDownList" + l.ToString();
                            DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                            query = query + " day" + i + "= '" + txt_day1.SelectedValue + "',";
                            ++l;
                        }
                    }
                    d.operation("Update pay_attendance_muster set " + query + " ot_hours=" + (txt_ot_hour.Text.Trim() == "" ? "0" : txt_ot_hour.Text) + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                }
                //vikas starrt code 04/05/2019

                if (ot_applicable == "2" || tempflag=="yes")
                {
                    TextBox ot_txt1 = (TextBox)row.FindControl("txt_ot_1");
                    TextBox ot_txt2 = (TextBox)row.FindControl("txt_ot_2");
                    TextBox ot_txt3 = (TextBox)row.FindControl("txt_ot_3");
                    TextBox ot_txt4 = (TextBox)row.FindControl("txt_ot_4");
                    TextBox ot_txt5 = (TextBox)row.FindControl("txt_ot_5");
                    TextBox ot_txt6 = (TextBox)row.FindControl("txt_ot_6");
                    TextBox ot_txt7 = (TextBox)row.FindControl("txt_ot_7");
                    TextBox ot_txt8 = (TextBox)row.FindControl("txt_ot_8");
                    TextBox ot_txt9 = (TextBox)row.FindControl("txt_ot_9");
                    TextBox ot_txt10 = (TextBox)row.FindControl("txt_ot_10");
                    TextBox ot_txt11 = (TextBox)row.FindControl("txt_ot_11");
                    TextBox ot_txt12 = (TextBox)row.FindControl("txt_ot_12");
                    TextBox ot_txt13 = (TextBox)row.FindControl("txt_ot_13");
                    TextBox ot_txt14 = (TextBox)row.FindControl("txt_ot_14");
                    TextBox ot_txt15 = (TextBox)row.FindControl("txt_ot_15");
                    TextBox ot_txt16 = (TextBox)row.FindControl("txt_ot_16");
                    TextBox ot_txt17 = (TextBox)row.FindControl("txt_ot_17");
                    TextBox ot_txt18 = (TextBox)row.FindControl("txt_ot_18");
                    TextBox ot_txt19 = (TextBox)row.FindControl("txt_ot_19");
                    TextBox ot_txt20 = (TextBox)row.FindControl("txt_ot_20");
                    TextBox ot_txt21 = (TextBox)row.FindControl("txt_ot_21");
                    TextBox ot_txt22 = (TextBox)row.FindControl("txt_ot_22");
                    TextBox ot_txt23 = (TextBox)row.FindControl("txt_ot_23");
                    TextBox ot_txt24 = (TextBox)row.FindControl("txt_ot_24");
                    TextBox ot_txt25 = (TextBox)row.FindControl("txt_ot_25");
                    TextBox ot_txt26 = (TextBox)row.FindControl("txt_ot_26");
                    TextBox ot_txt27 = (TextBox)row.FindControl("txt_ot_27");
                    TextBox ot_txt28 = (TextBox)row.FindControl("txt_ot_28");
                    TextBox ot_txt29 = (TextBox)row.FindControl("txt_ot_29");
                    TextBox ot_txt30 = (TextBox)row.FindControl("txt_ot_30");
                    TextBox ot_txt31 = (TextBox)row.FindControl("txt_ot_31");

                    if (date_common == "1")
                    {
                        int i = 31, l = 1; string query = "";
                        int datecommon = int.Parse(date_common);
                        Double total_otday = 0;
                        for (i = 1; i <= 31; i++)
                        {
                            if (i < 10)
                            {
                                string cntrlname = "txt_ot_" + l.ToString();
                                TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                   // query = query + " OT_DAILY_DAY0" + (i) + "= '" + txt_day1.Text + "',";
                                    // rahul changes 18-10-2019
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY0" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    else
                                    {
                                        //string aa = +year1 + "-" + mnth1 + "-0" + (i) + " " + txt_day1.Text.ToString();
                                        //query = query + " OT_DAILY_DAY0" + (i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";

                                        //string aa = +year1 + "-" + mnth1 + "-0" + (i) + " " + txt_day1.Text.ToString();
                                        //query = query + " OT_DAILY_DAY0" + (i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";

                                        query = query + " OT_DAILY_DAY0" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    
                                    ++l;
                                }
                                else
                                {
                                    string cntrlname = "txt_ot_" + l.ToString();
                                    TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                    //query = query + " OT_DAILY_DAY" + (i) + "= '" + txt_day1.Text + "',";//DropDownList.SelectedValue
                                    // rahul changes 18-10-2019
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    else
                                    {

                                        string aa = +year1 + "-" + mnth1 + "-" + (i) + " " + txt_day1.Text.ToString();
                                        //query = query + " OT_DAILY_DAY" + (i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";//DropDownList.SelectedValue
                                        //query = query + " OT_DAILY_DAY" + (i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";//DropDownList.SelectedValue
                                        query = query + " OT_DAILY_DAY" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                   // total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    ++l;
                                }
                            }
                            //old query
                            //d.operation("Update pay_daily_ot_muster set OT_DAILY_DAY01='" + ot_txt1.Text + "', OT_DAILY_DAY02='" + ot_txt2.Text + "', OT_DAILY_DAY03='" + ot_txt3.Text + "', OT_DAILY_DAY04='" + ot_txt4.Text + "', OT_DAILY_DAY05='" + ot_txt5.Text + "', OT_DAILY_DAY06='" + ot_txt6.Text + "', OT_DAILY_DAY07='" + ot_txt7.Text + "', OT_DAILY_DAY08='" + ot_txt8.Text + "', OT_DAILY_DAY09='" + ot_txt9.Text + "', OT_DAILY_DAY10='" + ot_txt10.Text + "', OT_DAILY_DAY11='" + ot_txt11.Text + "', OT_DAILY_DAY12='" + ot_txt12.Text + "', OT_DAILY_DAY13='" + ot_txt13.Text + "', OT_DAILY_DAY14='" + ot_txt14.Text + "', OT_DAILY_DAY15='" + ot_txt15.Text + "', OT_DAILY_DAY16='" + ot_txt16.Text + "', OT_DAILY_DAY17='" + ot_txt17.Text + "', OT_DAILY_DAY18='" + ot_txt18.Text + "', OT_DAILY_DAY19='" + ot_txt19.Text + "', OT_DAILY_DAY20='" + ot_txt20.Text + "', OT_DAILY_DAY21='" + ot_txt21.Text + "', OT_DAILY_DAY22='" + ot_txt22.Text + "',  OT_DAILY_DAY23='" + ot_txt23.Text + "', OT_DAILY_DAY24='" + ot_txt24.Text + "', OT_DAILY_DAY25='" + ot_txt25.Text + "', OT_DAILY_DAY26='" + ot_txt26.Text + "', OT_DAILY_DAY27='" + ot_txt27.Text + "', OT_DAILY_DAY28='" + ot_txt28.Text + "', OT_DAILY_DAY29='" + ot_txt29.Text + "', OT_DAILY_DAY30='" + ot_txt30.Text + "', OT_DAILY_DAY31='" + ot_txt31.Text + "',TOTAL_OT='" + total_otday + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");

                           // d.operation("Update pay_daily_ot_muster set OT_DAILY_DAY01=time_to_sec('" + ot_txt1.Text + "'), OT_DAILY_DAY02=time_to_sec('" + ot_txt2.Text + "'), OT_DAILY_DAY03=time_to_sec('" + ot_txt3.Text + "'), OT_DAILY_DAY04=time_to_sec('" + ot_txt4.Text + "'), OT_DAILY_DAY05=time_to_sec('" + ot_txt5.Text + "'), OT_DAILY_DAY06=time_to_sec('" + ot_txt6.Text + "'), OT_DAILY_DAY07=time_to_sec('" + ot_txt7.Text + "'), OT_DAILY_DAY08=time_to_sec('" + ot_txt8.Text + "'), OT_DAILY_DAY09=time_to_sec('" + ot_txt9.Text + "'), OT_DAILY_DAY10=time_to_sec('" + ot_txt10.Text + "'), OT_DAILY_DAY11=time_to_sec('" + ot_txt11.Text + "'), OT_DAILY_DAY12=time_to_sec('" + ot_txt12.Text + "'), OT_DAILY_DAY13=time_to_sec('" + ot_txt13.Text + "'), OT_DAILY_DAY14=time_to_sec('" + ot_txt14.Text + "'), OT_DAILY_DAY15=time_to_sec('" + ot_txt15.Text + "'), OT_DAILY_DAY16=time_to_sec('" + ot_txt16.Text + "'), OT_DAILY_DAY17=time_to_sec('" + ot_txt17.Text + "'), OT_DAILY_DAY18=time_to_sec('" + ot_txt18.Text + "'), OT_DAILY_DAY19=time_to_sec('" + ot_txt19.Text + "'), OT_DAILY_DAY20=time_to_sec('" + ot_txt20.Text + "'), OT_DAILY_DAY21=time_to_sec('" + ot_txt21.Text + "'), OT_DAILY_DAY22=time_to_sec('" + ot_txt22.Text + "'),  OT_DAILY_DAY23=time_to_sec('" + ot_txt23.Text + "'), OT_DAILY_DAY24=time_to_sec('" + ot_txt24.Text + "'), OT_DAILY_DAY25=time_to_sec('" + ot_txt25.Text + "'), OT_DAILY_DAY26=time_to_sec('" + ot_txt26.Text + "'), OT_DAILY_DAY27=time_to_sec('" + ot_txt27.Text + "'), OT_DAILY_DAY28=time_to_sec('" + ot_txt28.Text + "'), OT_DAILY_DAY29=time_to_sec('" + ot_txt29.Text + "'), OT_DAILY_DAY30=time_to_sec('" + ot_txt30.Text + "'), OT_DAILY_DAY31=time_to_sec('" + ot_txt31.Text + "'),TOTAL_OT='" + total_otday + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");

                            d.operation("Update pay_daily_ot_muster set `OT_DAILY_DAY01` = '" + ot_txt1.Text + "',`OT_DAILY_DAY02` = '" + ot_txt2.Text + "',`OT_DAILY_DAY03` = '" + ot_txt3.Text + "',`OT_DAILY_DAY04` = '" + ot_txt4.Text + "',`OT_DAILY_DAY05` = '" + ot_txt5.Text + "',`OT_DAILY_DAY06` = '" + ot_txt6.Text + "',`OT_DAILY_DAY07` = '" + ot_txt7.Text + "',`OT_DAILY_DAY08` = '" + ot_txt8.Text + "',`OT_DAILY_DAY09` = '" + ot_txt9.Text + "',`OT_DAILY_DAY10` = '" + ot_txt10.Text + "',`OT_DAILY_DAY11` = '" + ot_txt11.Text + "',`OT_DAILY_DAY12` = '" + ot_txt12.Text + "',`OT_DAILY_DAY13` = '" + ot_txt13.Text + "',`OT_DAILY_DAY14` = '" + ot_txt14.Text + "',`OT_DAILY_DAY15` = '" + ot_txt15.Text + "',`OT_DAILY_DAY16` = '" + ot_txt16.Text + "',`OT_DAILY_DAY17` = '" + ot_txt17.Text + "',`OT_DAILY_DAY18` = '" + ot_txt18.Text + "',`OT_DAILY_DAY19` = '" + ot_txt19.Text + "',`OT_DAILY_DAY20` = '" + ot_txt20.Text + "',`OT_DAILY_DAY21` = '" + ot_txt21.Text + "',`OT_DAILY_DAY22` = '" + ot_txt22.Text + "',`OT_DAILY_DAY23` = '" + ot_txt23.Text + "',`OT_DAILY_DAY24` = '" + ot_txt24.Text + "',`OT_DAILY_DAY25` = '" + ot_txt25.Text + "',`OT_DAILY_DAY26` = '" + ot_txt26.Text + "',`OT_DAILY_DAY27` = '" + ot_txt27.Text + "',`OT_DAILY_DAY28` = '" + ot_txt28.Text + "',`OT_DAILY_DAY29` = '" + ot_txt29.Text + "',`OT_DAILY_DAY30` = '" + ot_txt30.Text + "',`OT_DAILY_DAY31` = '" + ot_txt31.Text + "',TOTAL_OT='" + total_otday + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");
                            

                            //string total_ot_second = d.getsinglestring("  select  (OT_DAILY_DAY01+OT_DAILY_DAY02+OT_DAILY_DAY03+OT_DAILY_DAY04+OT_DAILY_DAY05+OT_DAILY_DAY06+OT_DAILY_DAY07+OT_DAILY_DAY08+OT_DAILY_DAY09+OT_DAILY_DAY10+OT_DAILY_DAY11+OT_DAILY_DAY12+OT_DAILY_DAY13+OT_DAILY_DAY14+OT_DAILY_DAY15+OT_DAILY_DAY16+OT_DAILY_DAY17+OT_DAILY_DAY18+OT_DAILY_DAY19+OT_DAILY_DAY20+OT_DAILY_DAY21+OT_DAILY_DAY22+OT_DAILY_DAY23+OT_DAILY_DAY24+OT_DAILY_DAY25+OT_DAILY_DAY26+OT_DAILY_DAY27+OT_DAILY_DAY28+OT_DAILY_DAY29+OT_DAILY_DAY30+OT_DAILY_DAY31) as total_ot_hours from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");

                            //  Update pay_daily_ot_muster total OT Hours
                            //d.operation("Update pay_daily_ot_muster set TOTAL_OT='" + total_ot_second + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");

                        //string attendance_muster_ot_hours = d.getsinglestring("  select  replace (TIME_FORMAT( CASE `pay_daily_ot_muster`.`TOTAL_OT` WHEN 0 THEN 0 ELSE IFNULL(SEC_TO_TIME(`pay_daily_ot_muster`.`TOTAL_OT`), 0) END, '%H.%i'),'00.00','0') AS 'TOTAL_OT' from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");
                        string attendance_muster_ot_hours = d.getsinglestring("  select  IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0)  AS 'TOTAL_OT' from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");
                        if (attendance_muster_ot_hours == "") { attendance_muster_ot_hours = "0"; }
                        // update pay_attendance_muster ot_hours 
                        d.operation("Update pay_attendance_muster set ot_hours ='" + attendance_muster_ot_hours + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");

                    }
                    else
                    {
                        int datecommon = int.Parse(date_common);
                        int month1 = 0; int j = 41, k = datecommon, l = 1;
                        month1 = mnth1;
                        //year1 = year1;
                        string query = "";
                        Double total_otday = 0;
                        //--month1;
                        if (month1 == 0) { month1 = 12; year1 = year1 - 1; }
                        int daysinmonth = DateTime.DaysInMonth(year1, month1);//
                        DateTime dt = new DateTime(year1, month1, 1);
                        for (int i = 0; daysinmonth >= (i + datecommon); i++)
                        {
                            if (datecommon < 10)
                            {
                                string cntrlname = "txt_ot_" + l.ToString();
                                TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                   // query = query + " OT_DAILY_DAY0" + (datecommon + i) + "= '" + txt_day1.Text + "',";
                                    // rahul changes 18-10-2019
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY0" + (datecommon + i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    else
                                    {
                                       // string aa = +year1 + "-" + month1 + "-0" + (datecommon + i) + " " + txt_day1.Text.ToString();
                                        //query = query + " OT_DAILY_DAY0" + (datecommon + i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";//DropDownList.SelectedValue
                                       // query = query + " OT_DAILY_DAY0" + (datecommon + i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";//DropDownList.SelectedValue
                                        query = query + " OT_DAILY_DAY0" + (datecommon + i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                   // total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    ++l;
                                }
                                else
                                {
                                    string cntrlname = "txt_ot_" + l.ToString();
                                    TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                    
                                   // query = query + " OT_DAILY_DAY" + (datecommon + i) + "= '" + txt_day1.Text + "',";

                                    // rahul changes 18-10-2019 8 pm
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY" + (datecommon + i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }else {

                                       // string aa = +year1 + "-" + month1 + "-" + (datecommon + i) + " " + txt_day1.Text.ToString();
                                        //query = query + " OT_DAILY_DAY" + (datecommon + i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";//DropDownList.SelectedValue
                                        //query = query + " OT_DAILY_DAY" + (datecommon + i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";//DropDownList.SelectedValue
                                        query = query + " OT_DAILY_DAY" + (datecommon + i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    
                                   // total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    ++l;
                                }
                            }
                            d.operation("Update pay_daily_ot_muster set " + query.Substring(0, query.Length - 1) + ",TOTAL_OT='" + total_otday + "'  where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + month1 + " and year = " + year1 + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                        string attendance_muster_ot_hours_old_month = d.getsinglestring("  select  IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0) AS 'TOTAL_OT' from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + month1 + " and year = " + year1 + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + month1 + "/" + year1 + "','%d/%m/%Y') OR left_date IS NULL)) ");
                         if (attendance_muster_ot_hours_old_month == "") { attendance_muster_ot_hours_old_month = "0"; }
                            k = 1;
                            query = "";
                            total_otday = 0;
                            dt = new DateTime(year1, mnth1, 1);
                            for (int i = 1; (datecommon) > i; i++)
                            {
                                if (i < 10)
                                {
                                    string cntrlname = "txt_ot_" + l.ToString();
                                    TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                    //query = query + " OT_DAILY_DAY0" + i + "= '" + txt_day1.Text + "',";
                                    // rahul changes 18-10-2019
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY0" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }else {

                                    //string aa = +year1 + "-" + mnth1 + "-0" + (i) + " " + txt_day1.Text.ToString();
                                    //query = query + " OT_DAILY_DAY0" + (i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";//DropDownList.SelectedValue
                                    //query = query + " OT_DAILY_DAY0" + (i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";//DropDownList.SelectedValue
                                    query = query + " OT_DAILY_DAY0" + (i) + "= '" + txt_day1.Text + "',";
                                    total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                   // total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    ++l;
                                }
                                else
                                {
                                    string cntrlname = "txt_ot_" + l.ToString();
                                    TextBox txt_day1 = (TextBox)row.FindControl(cntrlname);
                                   // query = query + " OT_DAILY_DAY" + i + "= '" + txt_day1.Text + "',";
                                    // rahul changes 18-10-2019
                                    if (txt_day1.Text.ToString() == "0")
                                    {
                                        query = query + " OT_DAILY_DAY" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                    else
                                    {
                                        //string aa = +year1 + "-" + mnth1 + "-" + (i) + " " + txt_day1.Text.ToString();
                                       // query = query + " OT_DAILY_DAY" + (i) + "= (UNIX_TIMESTAMP('" + aa + "')*1000),";//DropDownList.SelectedValue
                                        //query = query + " OT_DAILY_DAY" + (i) + "= time_to_sec('" + txt_day1.Text.ToString() + "'),";//DropDownList.SelectedValue
                                        query = query + " OT_DAILY_DAY" + (i) + "= '" + txt_day1.Text + "',";
                                        total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    }
                                   // total_otday = (total_otday) + double.Parse(txt_day1.Text.ToString());
                                    ++l;
                            }
                        }



                       // d.operation("Update pay_daily_ot_muster set " + query + "TOTAL_OT='" + total_otday + "'  where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                       
                       // string total_ot_second = d.getsinglestring("  select  (OT_DAILY_DAY01+OT_DAILY_DAY02+OT_DAILY_DAY03+OT_DAILY_DAY04+OT_DAILY_DAY05+OT_DAILY_DAY06+OT_DAILY_DAY07+OT_DAILY_DAY08+OT_DAILY_DAY09+OT_DAILY_DAY10+OT_DAILY_DAY11+OT_DAILY_DAY12+OT_DAILY_DAY13+OT_DAILY_DAY14+OT_DAILY_DAY15+OT_DAILY_DAY16+OT_DAILY_DAY17+OT_DAILY_DAY18+OT_DAILY_DAY19+OT_DAILY_DAY20+OT_DAILY_DAY21+OT_DAILY_DAY22+OT_DAILY_DAY23+OT_DAILY_DAY24+OT_DAILY_DAY25+OT_DAILY_DAY26+OT_DAILY_DAY27+OT_DAILY_DAY28+OT_DAILY_DAY29+OT_DAILY_DAY30+OT_DAILY_DAY31) as total_ot_hours from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");

                        d.operation("Update pay_daily_ot_muster set " + query + "TOTAL_OT='" + total_otday + "'  where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");

                       // string attendance_muster_ot_hours = d.getsinglestring("  select  replace (TIME_FORMAT( CASE `pay_daily_ot_muster`.`TOTAL_OT` WHEN 0 THEN 0 ELSE IFNULL(SEC_TO_TIME(`pay_daily_ot_muster`.`TOTAL_OT`), 0) END, '%H.%i'),'00.00','0') AS 'TOTAL_OT' from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");

                        string attendance_muster_ot_hours = d.getsinglestring("  select  IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0) AS 'TOTAL_OT' from pay_daily_ot_muster where  comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL)) ");
                        if (attendance_muster_ot_hours == "") { attendance_muster_ot_hours = "0"; }

                        double total_ot_hours_cal = (double.Parse(attendance_muster_ot_hours_old_month) + double.Parse(attendance_muster_ot_hours));

                        // update pay_attendance_muster ot_hours 
                        d.operation("Update pay_attendance_muster set ot_hours ='" + total_ot_hours_cal + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code in (select emp_code from pay_employee_master where emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "' and ( left_date > str_to_date('01/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') OR left_date IS NULL))");


                    }
                }
                //vikas end code 04//05/2019
                else if (ot_applicable == "1")
                {
                    DropDownList ot_ddl1 = (DropDownList)row.FindControl("OT_DropDownList1");
                    DropDownList ot_ddl2 = (DropDownList)row.FindControl("OT_DropDownList2");
                    DropDownList ot_ddl3 = (DropDownList)row.FindControl("OT_DropDownList3");
                    DropDownList ot_ddl4 = (DropDownList)row.FindControl("OT_DropDownList4");
                    DropDownList ot_ddl5 = (DropDownList)row.FindControl("OT_DropDownList5");
                    DropDownList ot_ddl6 = (DropDownList)row.FindControl("OT_DropDownList6");
                    DropDownList ot_ddl7 = (DropDownList)row.FindControl("OT_DropDownList7");
                    DropDownList ot_ddl8 = (DropDownList)row.FindControl("OT_DropDownList8");
                    DropDownList ot_ddl9 = (DropDownList)row.FindControl("OT_DropDownList9");
                    DropDownList ot_ddl10 = (DropDownList)row.FindControl("OT_DropDownList10");
                    DropDownList ot_ddl11 = (DropDownList)row.FindControl("OT_DropDownList11");
                    DropDownList ot_ddl12 = (DropDownList)row.FindControl("OT_DropDownList12");
                    DropDownList ot_ddl13 = (DropDownList)row.FindControl("OT_DropDownList13");
                    DropDownList ot_ddl14 = (DropDownList)row.FindControl("OT_DropDownList14");
                    DropDownList ot_ddl15 = (DropDownList)row.FindControl("OT_DropDownList15");
                    DropDownList ot_ddl16 = (DropDownList)row.FindControl("OT_DropDownList16");
                    DropDownList ot_ddl17 = (DropDownList)row.FindControl("OT_DropDownList17");
                    DropDownList ot_ddl18 = (DropDownList)row.FindControl("OT_DropDownList18");
                    DropDownList ot_ddl19 = (DropDownList)row.FindControl("OT_DropDownList19");
                    DropDownList ot_ddl20 = (DropDownList)row.FindControl("OT_DropDownList20");
                    DropDownList ot_ddl21 = (DropDownList)row.FindControl("OT_DropDownList21");
                    DropDownList ot_ddl22 = (DropDownList)row.FindControl("OT_DropDownList22");
                    DropDownList ot_ddl23 = (DropDownList)row.FindControl("OT_DropDownList23");
                    DropDownList ot_ddl24 = (DropDownList)row.FindControl("OT_DropDownList24");
                    DropDownList ot_ddl25 = (DropDownList)row.FindControl("OT_DropDownList25");
                    DropDownList ot_ddl26 = (DropDownList)row.FindControl("OT_DropDownList26");
                    DropDownList ot_ddl27 = (DropDownList)row.FindControl("OT_DropDownList27");
                    DropDownList ot_ddl28 = (DropDownList)row.FindControl("OT_DropDownList28");
                    DropDownList ot_ddl29 = (DropDownList)row.FindControl("OT_DropDownList29");
                    DropDownList ot_ddl30 = (DropDownList)row.FindControl("OT_DropDownList30");
                    DropDownList ot_ddl31 = (DropDownList)row.FindControl("OT_DropDownList31");

                    double zerocount = 0;
                    double twocount = 0;
                    double eightcount = 0;
                    int datecommon = int.Parse(date_common);
                    int days_ot = CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2);
                    for (int j = 1; j <= days_ot; j++)
                    {
                        string cntrlname = "OT_DropDownList" + j.ToString();
                        DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                        if (txt_day1.SelectedValue == "0")
                        {
                            zerocount = zerocount + 0;
                        }
                        else if (txt_day1.SelectedValue == "2")
                        {
                            twocount = twocount + 2;
                        }
                        else if (txt_day1.SelectedValue == "8")
                        {
                            eightcount = eightcount + 8;
                        }
                    }
                    if (datecommon == 1)
                    {
                        d.operation("Update pay_ot_muster set OT_DAY01='" + ot_ddl1.SelectedValue + "', OT_DAY02='" + ot_ddl2.SelectedValue + "', OT_DAY03='" + ot_ddl3.SelectedValue + "', OT_DAY04='" + ot_ddl4.SelectedValue + "', OT_DAY05='" + ot_ddl5.SelectedValue + "', OT_DAY06='" + ot_ddl6.SelectedValue + "', OT_DAY07='" + ot_ddl7.SelectedValue + "', OT_DAY08='" + ot_ddl8.SelectedValue + "', OT_DAY09='" + ot_ddl9.SelectedValue + "', OT_DAY10='" + ot_ddl10.SelectedValue + "', OT_DAY11='" + ot_ddl11.SelectedValue + "', OT_DAY12='" + ot_ddl12.SelectedValue + "', OT_DAY13='" + ot_ddl13.SelectedValue + "', OT_DAY14='" + ot_ddl14.SelectedValue + "', OT_DAY15='" + ot_ddl15.SelectedValue + "', OT_DAY16='" + ot_ddl16.SelectedValue + "', OT_DAY17='" + ot_ddl17.SelectedValue + "', OT_DAY18='" + ot_ddl18.SelectedValue + "', OT_DAY19='" + ot_ddl19.SelectedValue + "', OT_DAY20='" + ot_ddl20.SelectedValue + "', OT_DAY21='" + ot_ddl21.SelectedValue + "', OT_DAY22='" + ot_ddl22.SelectedValue + "', OT_DAY23='" + ot_ddl23.SelectedValue + "', OT_DAY24='" + ot_ddl24.SelectedValue + "', OT_DAY25='" + ot_ddl25.SelectedValue + "', OT_DAY26='" + ot_ddl26.SelectedValue + "', OT_DAY27='" + ot_ddl27.SelectedValue + "', OT_DAY28='" + ot_ddl28.SelectedValue + "', OT_DAY29='" + ot_ddl29.SelectedValue + "',OT_DAY30='" + ot_ddl30.SelectedValue + "',OT_DAY31='" + ot_ddl31.SelectedValue + "',TOT_OT = '" + (twocount + eightcount) + "' where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                    }
                    else
                    {

                        int month1 = 0; int j = 41, k = datecommon, l = 1;
                        month1 = mnth1;
                        //year1 = year1;
                        string query = "";
                        //--month1;
                        if (month1 == 0) { month1 = 12; year1 = year1 - 1; }
                        int daysinmonth = DateTime.DaysInMonth(year1, month1);
                        DateTime dt = new DateTime(year1, month1, 1);
                        for (int i = 0; daysinmonth >= (i + datecommon); i++)
                        {
                            if (datecommon < 10)
                            {
                                string cntrlname = "OT_DropDownList" + l.ToString();
                                DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                                query = query + " ot_day0" + (datecommon + i) + "= '" + txt_day1.SelectedValue + "',";
                                ++l;
                            }
                            else
                            {
                                string cntrlname = "OT_DropDownList" + l.ToString();
                                DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                                query = query + " ot_day" + (datecommon + i) + "= '" + txt_day1.SelectedValue + "',";
                                ++l;
                            }
                        }
                        d.operation("Update pay_ot_muster set " + query.Substring(0, query.Length - 1) + "  where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + month1 + " and year = " + year1 + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                        k = 1;
                        query = "";
                        dt = new DateTime(year1, mnth1, 1);
                        for (int i = 1; (datecommon) > i; i++)
                        {
                            if (i < 10)
                            {
                                string cntrlname = "OT_DropDownList" + l.ToString();
                                DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                                query = query + " ot_day0" + i + "= '" + txt_day1.SelectedValue + "',";
                                ++l;
                            }
                            else
                            {
                                string cntrlname = "OT_DropDownList" + l.ToString();
                                DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                                query = query + " ot_day" + i + "= '" + txt_day1.SelectedValue + "',";
                                ++l;
                            }
                        }
                        d.operation("Update pay_ot_muster set " + query + " TOT_OT = '" + (twocount + eightcount) + "', ot_hours=" + (txt_ot_hour.Text.Trim() == "" ? "0" : txt_ot_hour.Text) + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = " + hidden_month.Value + " and year = " + hidden_year.Value + " and emp_code = '" + Convert.ToString(gv_attendance.DataKeys[row.RowIndex].Value) + "'");
                    }
                    twocount = 0;
                    eightcount = 0;
                }

            }
        }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Exceeds beyond the limits( "+ checkcount() +" Present Days) !!!');", true);
        //}
        gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 0,function);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Saved Successfully!!!');", true);
    }

    protected string where_function()
    {

        string where = "";
        hidden_month.Value = txttodate.Text.Substring(0, 2);
        hidden_year.Value = txttodate.Text.Substring(3);
        string start_date_common = get_start_date();
        string end_date_common = get_end_date();

        int month = int.Parse(hidden_month.Value);
        int year = int.Parse(hidden_year.Value);
        if (start_date_common != "" && start_date_common != "1")
        {
            month = --month;
            if (month == 0) { month = 12; year = --year; }
            where = " and (left_date >= str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
        }
        else
        {
            start_date_common = "1";
            where = " and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
        }

        return where;




    }
    protected void emp_btn_process_Click(object sender, EventArgs e)
    {
        //MD change
        //  attendance_status();

        Panel21.Visible = false;
        d.con1.Open();
        string unit_code = "";
        string function = where_function();
        try
        {

            if (ddl_unitcode.SelectedValue == "ALL")
            {
                unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " ) ";
            }
            else
            {
                Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }



            string grade_name = "";
            //old query
            // MySqlCommand cmd = new MySqlCommand("SELECT (SELECT group_concat( grade_code) FROM pay_grade_master WHERE COMP_CODE = pay_designation_count.comp_code AND GRADE_DESC = pay_designation_count.designation) AS 'grade' FROM pay_designation_count WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN ("+unit_code+")", d.con1);
            MySqlCommand cmd = new MySqlCommand("select cast(GROUP_CONCAT(DISTINCT ( grade_code )) as char) as grade FROM  pay_designation_count  inner join pay_grade_master on pay_grade_master. COMP_CODE  =  pay_designation_count . comp_code  AND pay_grade_master. GRADE_DESC  =  pay_designation_count . designation  inner join pay_unit_master on pay_unit_master. COMP_CODE  =  pay_designation_count . comp_code  AND pay_unit_master. unit_code  =  pay_designation_count . unit_code  WHERE pay_unit_master. comp_code  = 'C01' AND pay_unit_master. client_code  = '" + ddl_client.SelectedValue + "' AND pay_unit_master. STATE_NAME  = '" + ddl_state.SelectedValue + "' and pay_unit_master.unit_code IN (" + unit_code + ")", d.con1);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string grade = "";
                if (ddl_client.SelectedValue.Equals("Staff") || ddl_client.SelectedValue.Equals("IHMS")) { }

                else
                {
                    string designation = "";
                    designation = "'" + dr.GetValue(0).ToString() + "'";
                    designation = designation.Replace(",", "','");

                    //    grade = d.getsinglestring("select designation from pay_billing_master where billing_unit_code IN (" +unit_code+ ") and designation='" + dr.GetValue(0).ToString() + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                    grade = d.getsinglestring("select GROUP_CONCAT(distinct pay_billing_master.designation) from pay_billing_master inner join pay_unit_master on pay_billing_master.comp_code = pay_unit_master.comp_code and pay_billing_master.billing_unit_code = pay_unit_master.unit_code  where pay_billing_master.billing_unit_code IN (" + unit_code + ") and pay_billing_master.designation IN (" + designation + ") and pay_billing_master.comp_code='" + Session["COMP_CODE"].ToString() + "'");
                    grade = "'" + grade + "'";
                    grade = grade.Replace(",", "','");

                    if (grade.Equals(""))
                    {
                        grade_name = grade_name + "," + dr.GetValue(0).ToString();
                    }
                }
            }
            grade_name = grade_name.Equals("") ? "" : grade_name.Substring(1);
            if (!grade_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please create  Policy for this :" + grade_name + " designation ..!!');", true);
                Panel6.Visible = false;
                //dr.Dispose();
                // d.con1.Close();
                return;
            }

            dr.Dispose();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
        if (d.getsinglestring("select distinct pay_billing_master.billing_unit_code from pay_billing_master where pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_billing_master.billing_client_code = '" + ddl_client.SelectedValue + "' and pay_billing_master.billing_unit_code IN (" + unit_code + ")") == "")
        {
            btn_save.Visible = false;
            btn_approve.Visible = false;

            Panel1.Visible = false;

            btn_attendance.Visible = false;
            Panel6.Visible = false;
            Panel10.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch Having No Policy!!!');", true); return;
        }
        hidden_month.Value = txttodate.Text.Substring(0, 2);
        hidden_year.Value = txttodate.Text.Substring(3);
        gv_attendace_load(function);
        gv_approve_attendace_load(function);
        att_upload_panel.Visible = false;
        btn_process.Visible = true;
        //  btn_add_emp.Visible = true;//vikas30/10
        gv_fullmonthot.DataSource = null;
        gv_fullmonthot.DataBind();
        gv_attendance.DataSource = null;
        gv_attendance.DataBind();

        if (d1.getsinglestring("select arrears_flag from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' AND unit_code IN (" + unit_code + ")").Equals("0"))
        {
            string temp = d1.getsinglestring("SELECT distinct(month_end) FROM pay_billing_master_history where month_end =1 and comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' AND billing_unit_code IN(" + unit_code + ")");
            string temp1 = d1.getsinglestring("select flag from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'");
            if (temp == "1" || temp1 == "1" || temp1 == "2")
            {
                if (temp == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Month End Process completed, You cannot make changes !!')", true);
                    return;
                }
                else if (temp1 == "1" || temp1 == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Timesheet already approved, You cannot make changes !!')", true);
                    return;
                }
                btn_save.Visible = false;
                btn_approve.Visible = false;

                Panel1.Visible = false;

                btn_attendance.Visible = false;
                Panel6.Visible = false;
                Panel10.Visible = false;
                return;
            }

        }


        btn_save.Visible = false;
        btn_approve.Visible = false;

        d.con1.Open();
        DataSet ds1 = new DataSet();
        try
        {
            string where = "";
            string start_date_common = get_start_date();
            string end_date_common = get_end_date();
            int month = int.Parse(hidden_month.Value);
            int year = int.Parse(hidden_year.Value);
            if (start_date_common != "" && start_date_common != "1")
            {
                month = --month;
                if (month == 0) { month = 12; year = --year; }
                where = " and (left_date >= str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
            }
            else
            {
                start_date_common = "1";
                where = " and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            // MySqlDataAdapter adp1 = new MySqlDataAdapter("select * from (select pay_employee_master.emp_code, (SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME' ,(SELECT  GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code and GRADE_CODE = pay_employee_master.grade_code) as 'designation', Employee_type,LEFT_REASON,date_format(LEFT_DATE,'%d/%m/%Y') as left_date, pay_attendance_muster.emp_code as emp_code1 from pay_employee_master Left JOIN pay_attendance_muster ON pay_employee_master.emp_code = pay_attendance_muster.emp_code AND pay_attendance_muster.month = " + hidden_month.Value + " AND pay_attendance_muster.year = " + hidden_year.Value + " AND pay_attendance_muster.unit_code IN ("+unit_code +") where pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "' " + where + " union select pay_employee_master.emp_code, pay_employee_master.emp_name ,  (SELECT  GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code and GRADE_CODE = pay_employee_master.grade_code) as 'designation', pay_employee_master.Employee_type,pay_employee_master.LEFT_REASON, date_format(pay_employee_master.LEFT_DATE,'%d/%m/%Y') as left_date, '' as emp_code1 from pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code = pay_attendance_muster.emp_code where pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year = " + hidden_year.Value + ") t group by emp_code order by emp_name", d.con1);
            MySqlDataAdapter adp1 = new MySqlDataAdapter("select * from (select   pay_unit_master . unit_code , pay_unit_master . unit_name ,pay_employee_master.emp_code, (SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME' ,(SELECT  GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code and GRADE_CODE = pay_employee_master.grade_code) as 'designation', Employee_type,LEFT_REASON,date_format(LEFT_DATE,'%d/%m/%Y') as left_date, pay_attendance_muster.emp_code as emp_code1 from pay_employee_master INNER JOIN  pay_unit_master  ON  pay_employee_master . unit_code  =  pay_unit_master . unit_code  AND  pay_employee_master . client_code  =  pay_unit_master . client_code  Left JOIN pay_attendance_muster ON pay_employee_master.emp_code = pay_attendance_muster.emp_code AND pay_attendance_muster.month = " + hidden_month.Value + " AND pay_attendance_muster.year = " + hidden_year.Value + " AND pay_attendance_muster.unit_code IN (" + unit_code + ") where pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code IN ( " + unit_code + ") " + where + " union select  pay_unit_master . unit_code ,  pay_unit_master . unit_name , pay_employee_master.emp_code, pay_employee_master.emp_name ,  (SELECT  GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code and GRADE_CODE = pay_employee_master.grade_code) as 'designation', pay_employee_master.Employee_type,pay_employee_master.LEFT_REASON, date_format(pay_employee_master.LEFT_DATE,'%d/%m/%Y') as left_date, '' as emp_code1 from pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code = pay_attendance_muster.emp_code INNER JOIN  pay_unit_master  ON  pay_employee_master . unit_code  =  pay_unit_master . unit_code  AND  pay_employee_master . client_code  =  pay_unit_master . client_code  where pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code IN (" + unit_code + ") and pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year = " + hidden_year.Value + ") t group by emp_code order by unit_name", d.con1);


            adp1.Fill(ds1);
            gv_fullmonthot.DataSource = ds1.Tables[0];
            gv_fullmonthot.DataBind();

            if (ds1.Tables[0].Rows.Count <= 0)
            {

                btn_save.Visible = false;
                btn_process.Visible = false;
                btn_approve.Visible = false;
                gv_load_deployment();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('No Employee for This Location !!')", true);

            }
            d.con1.Close();
            Panel6.Visible = true;
            Panel10.Visible = true;
            Panel1.Visible = false;
            btn_attendance.Visible = false;
            System.Data.DataTable dt_item = new System.Data.DataTable();


            d.con1.Close();
            if (d.con.State == ConnectionState.Open) { d.con.Close(); }
            if (d.con1.State == ConnectionState.Open) { d.con1.Close(); }


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            Session["client_code"] = ddl_client.SelectedValue;//vikas30/10
            Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;//vikas0/10
            Session["state_new_emp"] = ddl_state.SelectedValue;
        }
    }
    protected void Material_status() 
    {

        try
        {
            Panel_notification_material.Visible = true;

            ViewState["Material_Message"] = 0;
            ViewState["Material_reject_attendance"] = 0;
            ViewState["Material_appro_attendannce"] = 0;
            ViewState["Material_appro_attendannce_finanace"] = 0;
            ViewState["Material_deployment"] = 0;
            ViewState["Material_closed_branch"] = 0;
            Material_emp_Count();

            DataTable dt_item = new DataTable();
            //Remaining Approve By Admin
            dt_item = d.chk_material(Session["COMP_CODE"].ToString(), ddl_client_material.SelectedValue, ddl_state_material.SelectedValue, ddl_branch_material.SelectedValue, txt_month_material.Text.Substring(0, 2), txt_month_material.Text.Substring(3), 0);
            Message = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["Material_Message"] = dt_item.Rows.Count.ToString();
                Material_Message = ViewState["Material_Message"].ToString();
                gv_branches_no_material.DataSource = dt_item;
                gv_branches_no_material.DataBind();
                Panel_not_approve_conv.Visible = true;
            }
            dt_item.Dispose();

            //approve by admin
            gv_approve_by_admin_material.DataSource = null;
            gv_approve_by_admin_material.DataBind();
            dt_item = d.chk_material(Session["COMP_CODE"].ToString(), ddl_client_material.SelectedValue, ddl_state_material.SelectedValue, ddl_branch_material.SelectedValue, txt_month_material.Text.Substring(0, 2), txt_month_material.Text.Substring(3), 1);
            Material_appro_attendannce = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["Material_appro_attendannce"] = dt_item.Rows.Count.ToString();
                Material_appro_attendannce = ViewState["Material_appro_attendannce"].ToString();

                gv_approve_by_admin_material.DataSource = dt_item;
                gv_approve_by_admin_material.DataBind();
                Panel_appro_con.Visible = true;
            }
            dt_item.Dispose();


            gv_branches_reject_material.DataSource = null;
            gv_branches_reject_material.DataBind();

            //Approve by Finance

            dt_item = d.chk_material(Session["COMP_CODE"].ToString(), ddl_client_material.SelectedValue, ddl_state_material.SelectedValue, ddl_branch_material.SelectedValue, txt_month_material.Text.Substring(0, 2), txt_month_material.Text.Substring(3), 2);
            Material_reject_attendance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["Material_reject_attendance"] = dt_item.Rows.Count.ToString();
                Material_reject_attendance = ViewState["Material_reject_attendance"].ToString();

                gv_branches_reject_material.DataSource = dt_item;
                gv_branches_reject_material.DataBind();

            }
            dt_item.Dispose();


            gv_approve_by_finance_material.DataSource = null;
            gv_approve_by_finance_material.DataBind();

            //Reject by finance

            dt_item = d.chk_material(Session["COMP_CODE"].ToString(), ddl_client_material.SelectedValue, ddl_state_material.SelectedValue, ddl_branch_material.SelectedValue, txt_month_material.Text.Substring(0, 2), txt_month_material.Text.Substring(3), 3);
            Material_appro_attendannce_finanace = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["Material_appro_attendannce_finanace"] = dt_item.Rows.Count.ToString();
                Material_appro_attendannce_finanace = ViewState["Material_appro_attendannce_finanace"].ToString();

                gv_approve_by_finance_material.DataSource = dt_item;
                gv_approve_by_finance_material.DataBind();

            }
            dt_item.Dispose();

            gv_no_deployment_material.DataSource = null;
            gv_no_deployment_material.DataBind();

            ////branch having no deployment
            //dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 5);
            //deployment = "0";
            //if (dt_item.Rows.Count > 0)
            //{
            //    ViewState["Material_deployment"] = dt_item.Rows.Count.ToString();
            //    deployment = ViewState["Material_deployment"].ToString();
            //    gv_no_deployment_material.DataSource = dt_item;
            //    gv_no_deployment_material.DataBind();

            //}
            //dt_item.Dispose();

            //gv_branch_close_conv.DataSource = null;
            //gv_branch_close_conv.DataBind();

            ////branch have closed
            //dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 6);
            //closed_branch = "0";
            //if (dt_item.Rows.Count > 0)
            //{
            //    ViewState["Material_closed_branch"] = dt_item.Rows.Count.ToString();
            //    closed_branch = ViewState["Material_closed_branch"].ToString();
            //    gv_branch_close_material.DataSource = dt_item;
            //    gv_branch_close_material.DataBind();

            //}
            //dt_item.Dispose();
            Panel_not_appr_material.Visible = true; Panel_appro_atte_material.Visible = true; Panel_reject_Material.Visible = true; Panel_approv_finance_material.Visible = true;



        }
        catch (Exception ex) { throw ex; }
        finally { }
    
   
    }
	
	 protected void conveyance_status()
    {
        try
        {
            Panel_notification_conv.Visible = true;
            ViewState["emp_con_remaing"] = 0;
            ViewState["emp_con_approve_admin"] = 0;
            ViewState["emp_con_approve_finance"] = 0;
            ViewState["emp_con_reject_finance"] = 0;

            DataTable dt_item = new DataTable();
            //Remaining Approve By Admin
            dt_item = d.chk_emp_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 0);
            emp_con_remaing = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["emp_con_remaing"] = dt_item.Rows.Count.ToString();
                emp_con_remaing = ViewState["emp_con_remaing"].ToString();
                gridview_conv_1.DataSource = dt_item;
                gridview_conv_1.DataBind();
                Panel_not_approve_conv.Visible = true;
            }


            //Approve By Admin
            gv_approved_conveyance.DataSource = null;
            gv_approved_conveyance.DataBind();
            dt_item = d.chk_emp_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 1);
            emp_con_approve_admin = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["emp_con_approve_admin"] = dt_item.Rows.Count.ToString();
                emp_con_approve_admin = ViewState["emp_con_approve_admin"].ToString();

            gv_approved_conveyance.DataSource = dt_item;
            gv_approved_conveyance.DataBind();
            Panel_appro_con.Visible = true;
        }
        dt_item.Dispose();


        gv_reject_conveyance.DataSource = null;
        gv_reject_conveyance.DataBind();

            //Approve By Finance
            dt_item = d.chk_emp_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 2);
            emp_con_approve_finance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["emp_con_approve_finance"] = dt_item.Rows.Count.ToString();
                emp_con_approve_finance = ViewState["emp_con_approve_finance"].ToString();

            gv_reject_conveyance.DataSource = dt_item;
            gv_reject_conveyance.DataBind();
           
        }
        dt_item.Dispose();


        gv_app_att_finance_conv.DataSource = null;
        gv_app_att_finance_conv.DataBind();

            //Reject By Finance
            dt_item = d.chk_emp_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 3);
            emp_con_reject_finance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["emp_con_reject_finance"] = dt_item.Rows.Count.ToString();
                emp_con_reject_finance = ViewState["emp_con_reject_finance"].ToString();

            gv_app_att_finance_conv.DataSource = dt_item;
            gv_app_att_finance_conv.DataBind();
           
        }
        dt_item.Dispose();

            //gv_branch_deployement_conv.DataSource = null;
            //gv_branch_deployement_conv.DataBind();

            Panel_not_approve_conv.Visible = true; Panel_appro_con.Visible = true; Panel_reject_con.Visible = true; Panel_appro_finance_con.Visible = true;



    }
     catch (Exception ex) { throw ex; }
        finally { }
    
    
    
    }



	
	

    protected void attendance_status()
    {

        try
        {
            Notification_panel.Visible = true;
            ViewState["Message"] = 0;
            ViewState["reject_attendance"] = 0;
            ViewState["appro_attendannce"] = 0;
            ViewState["appro_attendannce_finanace"] = 0;
            ViewState["deployment"] = 0;
            ViewState["closed_branch"] = 0;
            employee_count();

            gridService.DataSource = null;
            gridService.DataBind();

            DataTable dt_item = new DataTable();
            //policy
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 0,"");
            Message = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["Message"] = dt_item.Rows.Count.ToString();
                Message = ViewState["Message"].ToString();
                gridService.DataSource = dt_item;
                gridService.DataBind();
                pnl_branch.Visible = true;
            }
            //approve by admin
            gv_approved_attendance.DataSource = null;
            gv_approved_attendance.DataBind();
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 2,"");
            appro_attendannce = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_attendannce"] = dt_item.Rows.Count.ToString();
                appro_attendannce = ViewState["appro_attendannce"].ToString();

                gv_approved_attendance.DataSource = dt_item;
                gv_approved_attendance.DataBind();
                approval_panel.Visible = true;
            }
            dt_item.Dispose();

            //reject by finance

            gv_reject_attendance.DataSource = null;
            gv_reject_attendance.DataBind();
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 3,"");
            reject_attendance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["reject_attendance"] = dt_item.Rows.Count.ToString();
                reject_attendance = ViewState["reject_attendance"].ToString();

                gv_reject_attendance.DataSource = dt_item;
                gv_reject_attendance.DataBind();
                reject_panel.Visible = true;
            }
            dt_item.Dispose();

            gv_appr_att_finance.DataSource = null;
            gv_appr_att_finance.DataBind();

            //approve by finance
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 4,"");
            appro_attendannce_finanace = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_attendannce_finanace"] = dt_item.Rows.Count.ToString();
                appro_attendannce_finanace = ViewState["appro_attendannce_finanace"].ToString();

                gv_appr_att_finance.DataSource = dt_item;
                gv_appr_att_finance.DataBind();

            }
            dt_item.Dispose();

            gv_branch_deployment.DataSource = null;
            gv_branch_deployment.DataBind();
            //branch having no deployment
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 5,"");
            deployment = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["deployment"] = dt_item.Rows.Count.ToString();
                deployment = ViewState["deployment"].ToString();
                gv_branch_deployment.DataSource = dt_item;
                gv_branch_deployment.DataBind();

            }
            dt_item.Dispose();

            gv_branch_close.DataSource = null;
            gv_branch_close.DataBind();
            //branch have closed
            dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 6,"");
            closed_branch = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["closed_branch"] = dt_item.Rows.Count.ToString();
                closed_branch = ViewState["closed_branch"].ToString();
                gv_branch_close.DataSource = dt_item;
                gv_branch_close.DataBind();

            }
            dt_item.Dispose();
            pnl_branch.Visible = true; approval_panel.Visible = true; reject_panel.Visible = true; approval_finance_panel.Visible = true; panel_deployment.Visible = true; panel_clo_branch.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { }

    }


 protected void conveyance_count() 
    {
        try
        {


            ViewState["Conv_closed_branch_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 0);
            ViewState["Conv_Message_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 1);
            ViewState["Conv_appro_attendannce_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 2);
            ViewState["Conv_reject_attendance_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 3);

            ViewState["Conv_appro_attendannce_finanace_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 4);
            ViewState["Conv_deployment_emp"] = 0;


            Conv_closed_branch_emp = ViewState["Conv_closed_branch_emp"].ToString();
            Conv_Message_emp = ViewState["Conv_Message_emp"].ToString();
            Conv_reject_attendance_emp = ViewState["Conv_reject_attendance_emp"].ToString();
            Conv_appro_attendannce_emp = ViewState["Conv_appro_attendannce_emp"].ToString();
            Conv_appro_attendannce_finanace_emp = ViewState["Conv_appro_attendannce_finanace_emp"].ToString();
            Conv_deployment_emp = ViewState["Conv_deployment_emp"].ToString();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    
    
    
    }

protected void Material_emp_Count() 
   {
       try
       {


           ViewState["Material_closed_branch_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 0);
           ViewState["Material_Message_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 1);
           ViewState["Material_appro_attendannce_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 2);
           ViewState["Material_reject_attendance_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 3);

           ViewState["Material_appro_attendannce_finanace_emp"] = d.emp_count(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, txt_date_conveyance.Text, 4);
           ViewState["Material_deployment_emp"] = 0;


           Material_closed_branch_emp = ViewState["Material_closed_branch_emp"].ToString();
           Material_Message_emp = ViewState["Material_Message_emp"].ToString();
           Material_reject_attendance_emp = ViewState["Material_reject_attendance_emp"].ToString();
           Material_appro_attendannce_emp = ViewState["Material_appro_attendannce_emp"].ToString();
           Material_appro_attendannce_finanace_emp = ViewState["Material_appro_attendannce_finanace_emp"].ToString();
           Material_deployment_emp = ViewState["Material_deployment_emp"].ToString();
       }
       catch (Exception ex)
       {

       }
       finally
       {

       }
    
    }

    protected void employee_count()
    {

        try
        {
            ViewState["Emp_closed_branch"] = d.emp_count(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 0);
            ViewState["Emp_Message"] = d.emp_count(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 1);
            ViewState["Emp_appro_attendannce"] = d.emp_count(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 2);
            ViewState["Emp_reject_attendance"] = d.emp_count(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 3);

            ViewState["Emp_appro_attendannce_finanace"] = d.emp_count(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 4);
            ViewState["Emp_deployment"] = 0;

            Emp_closed_branch = ViewState["Emp_closed_branch"].ToString();
            Emp_Message = ViewState["Emp_Message"].ToString();
            Emp_reject_attendance = ViewState["Emp_reject_attendance"].ToString();
            Emp_appro_attendannce = ViewState["Emp_appro_attendannce"].ToString();
            Emp_appro_attendannce_finanace = ViewState["Emp_appro_attendannce_finanace"].ToString();
            Emp_deployment = ViewState["Emp_deployment"].ToString();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }
    protected void gv_fullmonthot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[10].Text).Trim() != "")
            {
                CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);
                txtName.Checked = true;
            }
        }
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[10].Visible = false;

    }
    public bool checkcount()
    {
        int days = 0;
        //string datediff = "";
        //string start_date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");
        //string end_date_common = d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1), (SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");

        //int mnth1 = int.Parse(hidden_month.Value), year1 = int.Parse(hidden_year.Value);
        //if (start_date_common != "" && start_date_common != "1")
        //{
        //    mnth1 = --mnth1;

        //    if (mnth1 == 0) { mnth1 = 12; year1 = --year1; }

        //    datediff = "DATEDIFF('" + ((mnth1 + 1) == 13 ? (year1 + 1) : year1) + "-" + int.Parse(hidden_month.Value) + "-" + end_date_common + "', '" + year1 + "-" + mnth1 + "-" + (int.Parse(start_date_common) - 1) + "')";
        //}
        //else {
        //    datediff = "DATEDIFF('" + ((mnth1 + 1) == 13 ? (year1 + 1) : year1) + "-" + ((mnth1 + 1) == 13 ? 1 : (mnth1 + 1)) + "-01', '" + year1 + "-" + mnth1 + "-01')";
        //}
        string function = where_function();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {

            //select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " 
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " ) ");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }

        double total_count = 0;
        //string total_present_days = d.getsinglestring("Select sum(TOT_DAYS_PRESENT) from pay_attendance_muster where  UNIT_CODE='" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + txttodate.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txttodate.Text.Substring(3) + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        // string countdays = "";
        d.con1.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Pay_designation_count.DESIGNATION, SUM(Pay_designation_count.count), pay_billing_master.designation, pay_billing_master.month_calc FROM Pay_designation_count inner join pay_grade_master on Pay_designation_count.DESIGNATION = pay_grade_master.grade_desc inner join pay_billing_master on Pay_designation_count.comp_code =  pay_billing_master.comp_code  and Pay_designation_count.unit_code =  pay_billing_master.billing_unit_code and pay_billing_master.DESIGNATION = pay_grade_master.graDE_code WHERE Pay_designation_count.client_code = '" + ddl_client.SelectedValue + "' AND Pay_designation_count.unit_code IN (" + unit_code + ") AND Pay_designation_count.comp_code ='" + Session["COMP_CODE"].ToString() + "' GROUP BY Pay_designation_count.designation  ", d.con1);
            MySqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                if (dr1.GetValue(3).ToString() == "1")
                {
                    // countdays = d.getsinglestring("(SELECT " + datediff + "  FROM  (SELECT ADDDATE('" + hidden_year.Value + "-" + hidden_month.Value + "-" + start_date_common + "', INTERVAL @i:=@i+1 DAY) AS DAY FROM ( SELECT a.a FROM (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS a CROSS JOIN (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS b CROSS JOIN (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS c ) a JOIN (SELECT @i := -1) r1 WHERE  @i < " + datediff + "  ) AS dateTable WHERE WEEKDAY(dateTable.Day) IN (6) ORDER BY dateTable.Day)");

                    days = CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2);
                    // days = int.Parse(countdays);
                }
                else
                {
                    days = CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 1);
                    //countdays = d.getsinglestring("(SELECT (" + datediff + " -  count(1))  FROM  (SELECT ADDDATE('" + hidden_year.Value + "-" + hidden_month.Value + "-" + start_date_common + "', INTERVAL @i:=@i+1 DAY) AS DAY FROM ( SELECT a.a FROM (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS a CROSS JOIN (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS b CROSS JOIN (SELECT 0 AS a UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) AS c ) a JOIN (SELECT @i := -1) r1 WHERE  @i < " + datediff + "  ) AS dateTable WHERE WEEKDAY(dateTable.Day) IN (6) ORDER BY dateTable.Day)");
                    //days = int.Parse(countdays);
                }

                if (ddl_unitcode.SelectedValue == "ALL")
                {

                    double count_days1 = double.Parse(d.getsinglestring("select ifnull(sum(tot_days_present),0) from pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.emp_code = pay_employee_master.emp_code  AND pay_attendance_muster.unit_code = pay_employee_master.unit_code and grade_code='" + dr1.GetValue(2).ToString() + "' inner join pay_unit_master on  pay_unit_master . unit_code  =  pay_employee_master . unit_code  AND  pay_unit_master . comp_code  =  pay_employee_master . comp_code  where pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year=" + hidden_year.Value + " and pay_attendance_muster.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.client_code ='" + ddl_client.SelectedValue + "' and pay_unit_master.state_name='" + ddl_state.SelectedValue + "'"));

                    total_count = double.Parse(dr1.GetValue(1).ToString()) * days;
                    if (total_count < count_days1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance (" + count_days1 + ") Exceeds beyond the limits( " + total_count + " Present Days) for Designation " + dr1.GetValue(0).ToString() + " !!!');", true);
                        return false;
                    }
                    string emp_name = d.getsinglestring("select pay_employee_master.emp_name from pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.emp_code = pay_employee_master.emp_code  AND pay_attendance_muster.unit_code = pay_employee_master.unit_code and grade_code='" + dr1.GetValue(2).ToString() + "' where pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year=" + hidden_year.Value + " and pay_attendance_muster.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.unit_code IN(" + unit_code + ") and tot_days_present > " + days);
                    if (!emp_name.Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance for " + emp_name + " Exceeds beyond the limits( " + days + " Present Days) !!!');", true);
                        return false;
                    }
                }
                else
                {
                    double count_days1 = double.Parse(d.getsinglestring("select ifnull(sum(tot_days_present),0) from pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.emp_code = pay_employee_master.emp_code  AND pay_attendance_muster.unit_code = pay_employee_master.unit_code and grade_code='" + dr1.GetValue(2).ToString() + "' where pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year=" + hidden_year.Value + " and pay_attendance_muster.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.unit_code = '" + ddl_unitcode.SelectedValue + "'"));

                    total_count = double.Parse(dr1.GetValue(1).ToString()) * days;
                    if (total_count < count_days1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance (" + count_days1 + ") Exceeds beyond the limits( " + total_count + " Present Days) for Designation " + dr1.GetValue(0).ToString() + " !!!');", true);
                        return false;
                    }
                    string emp_name = d.getsinglestring("select pay_employee_master.emp_name from pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.emp_code = pay_employee_master.emp_code  AND pay_attendance_muster.unit_code = pay_employee_master.unit_code and grade_code='" + dr1.GetValue(2).ToString() + "' where pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year=" + hidden_year.Value + " and pay_attendance_muster.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.unit_code IN(" + unit_code + ") and tot_days_present > " + days);
                    if (!emp_name.Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance for " + emp_name + " Exceeds beyond the limits( " + days + " Present Days) !!!');", true);
                        return false;
                    }
                }
            }
            dr1.Close();
            d.con1.Close();

            //if (total_present_days == "") { total_present_days = "0"; }
            //if (int.Parse(total_present_days) > total_count)
            //{
            //    return total_count;
            //}

            //double pcount = 0;
            //double halfdaycount = 0;
            //int count_days = CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2);
            //foreach (GridViewRow row in gv_attendance.Rows)
            //{
            //    if (row != null)
            //    {
            //        for (int j = 1; j <= count_days; j++)
            //        {
            //            string cntrlname = "DropDownList" + j.ToString();
            //            DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
            //            if (txt_day1.SelectedValue == "P")
            //            {
            //                pcount++;
            //            }
            //            else if (txt_day1.SelectedValue == "HD")
            //            {
            //                halfdaycount++;
            //            }

            //            else if (txt_day1.SelectedValue == "PH")
            //            {
            //                //leavescount++;
            //                pcount++;
            //            }
            //            //else if (txt_day1.SelectedValue == "CO")
            //            //{
            //            //    cocount++;
            //            //}
            //        }
            //        halfdaycount = halfdaycount / 2;
            //        pcount = halfdaycount + pcount;
            //    }

            //}
            //required_count = total_count;
            //actual_count = pcount;
            //if(total_count >= pcount)
            //{
            //    return true;
            //}
            return true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    private double validate()
    {
        double pcount = 0;
        double halfdaycount = 0;

        foreach (GridViewRow row in gv_attendance.Rows)
        {
            if (row != null)
            {
                for (int j = 1; j <= (CountDay(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 2)); j++)
                {
                    string cntrlname = "DropDownList" + j.ToString();
                    DropDownList txt_day1 = (DropDownList)row.FindControl(cntrlname);
                    if (txt_day1.SelectedValue == "P")
                    {
                        pcount++;
                    }
                    else if (txt_day1.SelectedValue == "HD")
                    {
                        halfdaycount++;
                    }

                    else if (txt_day1.SelectedValue == "PH")
                    {
                        //leavescount++;
                        pcount++;
                    }
                    //else if (txt_day1.SelectedValue == "CO")
                    //{
                    //    cocount++;
                    //}
                }
                halfdaycount = halfdaycount / 2;
                pcount = halfdaycount + pcount;
            }

        }
        return pcount;
    }
    protected string get_end_date()
    {
        // string function = where_function();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' ";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }
        //  return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");

       // return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN (" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");
    
        // rahul change query
        return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_unit_master INNER JOIN pay_billing_master_history ON  pay_unit_master.unit_code = pay_billing_master_history.billing_unit_code  AND  pay_unit_master.comp_code = pay_billing_master_history.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN (" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_unit_master  INNER JOIN  pay_billing_master  ON  pay_unit_master.unit_code = pay_billing_master.billing_unit_code  AND  pay_unit_master.comp_code =  pay_billing_master.comp_code   WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");
    }
    protected string get_end_date_material()
    {
        // string function = where_function();
        string unit_code = "";

        if (ddl_branch_material.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and  STATE_NAME  = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }
        //  return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + ddl_unitcode.SelectedValue + "' limit 1))");

        // return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN (" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");

        // rahul change query
        return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_unit_master INNER JOIN pay_billing_master_history ON  pay_unit_master.unit_code = pay_billing_master_history.billing_unit_code  AND  pay_unit_master.comp_code = pay_billing_master_history.comp_code WHERE pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code IN (" + unit_code + ") AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_unit_master  INNER JOIN  pay_billing_master  ON  pay_unit_master.unit_code = pay_billing_master.billing_unit_code  AND  pay_unit_master.comp_code =  pay_billing_master.comp_code   WHERE pay_unit_master.client_code  = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  IN(" + unit_code + ") limit 1))");
    }
    //vikas01/11/18
    protected void btn_add_emp_Click(object sender, EventArgs e)
    {
        Session["client_code"] = ddl_client.SelectedValue;
        Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;
        ModalPopupExtender2.Show();
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        // txttodate.Text = "";
        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //comment 30/09 branch_closing  MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y'))  ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unitcode.DataSource = dt_item;
                ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                ddl_unitcode.DataBind();
                ddl_unitcode.Items.Insert(0, "ALL");
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            if (txttodate.Text.Length > 0)
            {
                // hidden_month.Value = txttodate.Text.Substring(0, 2);
                //hidden_year.Value = txttodate.Text.Substring(3);
                // gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 0);
            }

            else
            {
                Panel1.Visible = false;
                pnl_branch.Visible = false;
                btn_attendance.Visible = false;
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            Session["state_new_emp"] = ddl_state.SelectedValue;
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
    }

    protected void gridService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
    }
    protected void gridService_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt_branch = new DataTable();
        string unit = (gridService.SelectedRow.Cells[1].Text);
        d.con1.Open();
        try
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT emp_code ,  emp_name  FROM pay_employee_master  WHERE comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + unit + "' AND  emp_code  Not IN (SELECT emp_code  FROM pay_attendance_muster  WHERE comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code  = '" + unit + "' AND  month  = '" + hidden_month.Value + "' AND  year  = '" + hidden_year.Value + "')", d.con1);
            adp.Fill(dt_branch);
            if (dt_branch.Rows.Count > 0)
            {
                gv_emp_details.DataSource = dt_branch;
                gv_emp_details.DataBind();
            }
            dt_branch.Clear();
            d.con1.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    private void update_joining_left_date(string where_function)
    {
        //string function = where_function();
        string function = where_function;
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " )  ";
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }



        string where = "";
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        string start_date_common = get_start_date();
        //string end_date_common = get_end_date();
        DateTime start_date, end_date;
        int month = int.Parse(hidden_month.Value);
        int year = int.Parse(hidden_year.Value);
        if (start_date_common != "" && start_date_common != "1")
        {
            if (start_date_common.Length == 1) { start_date_common = "0" + start_date_common; }
            month = --month;
            if (month == 0) { month = 12; year = --year; }
            where = "and (left_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') or joining_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact(start_date_common + "/" + (month >= 10 ? month.ToString() : "0" + month.ToString()) + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = DateTime.ParseExact(((int.Parse(start_date_common) - 1) >= 10 ? (int.Parse(start_date_common) - 1).ToString() : "0" + (int.Parse(start_date_common) - 1).ToString()) + "/" + hidden_month.Value + "/" + hidden_year.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            start_date_common = "1";

            where = "and (left_date between str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') or joining_date between str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact("01/" + hidden_month.Value + "/" + hidden_year.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = start_date.AddMonths(1).AddDays(-1);
        }

        MySqlCommand cmd_item = new MySqlCommand("SELECT emp_code, date_format(joining_date,'%d/%m/%Y'), ifnull(DATE_FORMAT(left_date, '%d/%m/%Y'),'1') FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") " + where, d1.con1);
        d1.con1.Open();
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
                        d.operation("UPDATE pay_attendance_muster SET " + start_date_common_query.Substring(0, start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        if (ot_applicable == "1")
                        {
                            d.operation("UPDATE pay_ot_muster SET " + Ot_start_date_common_query.Substring(0, Ot_start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        }
                    }
                    if (End_date_common_query != "")
                    {
                        d.operation("UPDATE pay_attendance_muster SET " + End_date_common_query.Substring(0, End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
                        if (ot_applicable == "1")
                        {
                            d.operation("UPDATE pay_ot_muster SET " + Ot_End_date_common_query.Substring(0, Ot_End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
                        }
                    }
                }
                else
                {
                    if (query != "")
                    {
                        d.operation("UPDATE pay_attendance_muster SET " + query.Substring(0, query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
                        if (ot_applicable == "1")
                        {
                            d.operation("UPDATE pay_ot_muster SET " + ot_query.Substring(0, ot_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
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
            d1.con1.Close();
        }

    }

    private Boolean checkreliever()
    {
        string function = where_function();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " ) ");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }



        string emp_name = d.getsinglestring("select group_concat(emp_name) from pay_attendance_muster inner join pay_employee_master on pay_attendance_muster.emp_code = pay_employee_master.emp_code where pay_attendance_muster.month = " + hidden_month.Value + " and pay_attendance_muster.year=" + hidden_year.Value + " and pay_attendance_muster.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.unit_code IN(" + unit_code + ") and employee_type = 'Reliever' and tot_days_present > 14");

        if (emp_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Releiver (" + emp_name + ") have attendance more than 15 days, Please make them Permanent !!!');", true);
            return false;

        }
        return true;
    }

    //MD CHANGE
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        Notification_panel.Visible = false;
        try
        { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string function = where_function();
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0'  AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " ) ");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }



        if (checkcount() && checkreliever())
        {

            if (d1.getsinglestring("select arrears_flag from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' AND unit_code IN(" + unit_code + ")").Equals("0"))
            {
                if (d.getsinglestring("select flag from pay_attendance_muster where comp_code='" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'") != "0")
                {
                    // attendance_status();
                    txt_document1.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Already Approved !!');", true);
                    return;

                }

                if (d.getsinglestring("select unit_code from pay_files_timesheet where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client.SelectedValue + "' and   month='" + txttodate.Text.Substring(0, 2) + "' and year='" + txttodate.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and flag = 0 ") == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Attendance Sheet  !!');", true);
                    return;
                }
                // upload_attendance_copy(document1_file);

               
                ///start vinod Merged 3 update queries 30/3/2020
                /// d.operation("Update pay_attendance_muster set flag = 1 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and  emp_code in (" + ViewState["EMP_CODE"].ToString() + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'");
               // d.operation("Update pay_attendance_reject_master set flag = 1 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'");
              //  d.operation("update   pay_files_timesheet set  uploaded_by='" + Session["LOGIN_ID"].ToString() + "', uploaded_date=now() , flag = 0 , status = 'Approve By Admin', reject_status='0' where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client.SelectedValue + "' and   month='" + txttodate.Text.Substring(0, 2) + "' and year='" + txttodate.Text.Substring(3) + "' and unit_code IN(" + unit_code + ")");
               // d.operation("delete from pay_attendance_muster where tot_days_present=0 and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'");

                d.operation("Update pay_attendance_muster set flag = 1 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and  emp_code in (" + ViewState["EMP_CODE"].ToString() + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "';Update pay_attendance_reject_master set flag = 1 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "';update pay_files_timesheet set uploaded_by='" + Session["LOGIN_ID"].ToString() + "', uploaded_date=now(), flag = 0, status = 'Approve By Admin', reject_status='0' where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client.SelectedValue + "' and   month=" + txttodate.Text.Substring(0, 2) + " and year=" + txttodate.Text.Substring(3) + " and unit_code IN(" + unit_code + ")");
                ///end vinod Merged 3 update queries 30/3/2020
            }
            else
            {
                d.operation("Update pay_attendance_muster set arrears_flag = 0 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN(" + unit_code + ") and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "'");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Approved Successfully !!');", true);
            // attendance_status();
            gv_attendace_load(function);
            txt_document1.Text = "";

            att_upload_panel.Visible = false;
            // Panel1.Visible = false;
            Panel6.Visible = false;
            Panel10.Visible = false;
            btn_save.Visible = false;
            btn_approve.Visible = false;
        }

    }




    protected void gv_attendace_load(string where_function)
    {

        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //apporve_attendace gridview
            grid_reject_attendace.DataSource = null;
            grid_reject_attendace.DataBind();
            d.con.Open();

            //string function = where_function();
            string function = where_function;
            string unit_code = "";


            if (ddl_unitcode.SelectedValue == "ALL")
            {
                unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " )  ";


            }
            else
            {

                Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }


            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_attendance_reject_master.unit_code, (SELECT client_name FROM pay_client_master WHERE pay_client_master.client_code = pay_attendance_reject_master.client_code) AS 'client_name', pay_files_timesheet_history.state AS 'state_name', (SELECT unit_name FROM pay_unit_master WHERE pay_unit_master.COMP_CODE = pay_attendance_reject_master.COMP_CODE AND pay_unit_master.unit_code = pay_attendance_reject_master.unit_code) AS 'branch_name', CONCAT(pay_attendance_reject_master.month, '/', pay_attendance_reject_master.year) AS 'month_year', file_name , rejected_reason FROM pay_files_timesheet_history INNER JOIN pay_attendance_reject_master ON pay_files_timesheet_history.comp_code = pay_attendance_reject_master.comp_code AND pay_files_timesheet_history.client_code = pay_attendance_reject_master.client_code AND pay_files_timesheet_history.unit_code = pay_attendance_reject_master.unit_code AND pay_files_timesheet_history.month = pay_attendance_reject_master.month AND pay_files_timesheet_history.year = pay_attendance_reject_master.year WHERE pay_attendance_reject_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_attendance_reject_master.client_code = '" + ddl_client.SelectedValue + "' and  pay_attendance_reject_master.unit_code IN(" + unit_code + ")  AND pay_attendance_reject_master.month = '" + txttodate.Text.Substring(0, 2).ToString() + "' AND pay_attendance_reject_master.year = '" + txttodate.Text.Substring(3).ToString() + "' AND pay_attendance_reject_master.flag = '0'     AND pay_files_timesheet_history.flag = '0' ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grid_reject_attendace.DataSource = dt;
                grid_reject_attendace.DataBind();
                gv_reject_panel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { d.con.Close(); }

    }

    protected void gv_approve_attendace_load(string where_function)
    {

        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //apporve_attendace gridview
            grid_approve_attendace.DataSource = null;
            grid_approve_attendace.DataBind();
            d.con.Open();
           // string function = where_function();
            string function = where_function;
            string unit_code = "";


            if (ddl_unitcode.SelectedValue == "ALL")
            {
                unit_code = "select unit_code from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0' AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + " )  ";


            }
            else
            {

                Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }
            // MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_attendance_reject_master.unit_code, (SELECT client_name FROM pay_client_master WHERE pay_client_master.client_code = pay_attendance_reject_master.client_code) AS 'client_name', pay_files_timesheet_history.state AS 'state_name', (SELECT unit_name FROM pay_unit_master WHERE pay_unit_master.COMP_CODE = pay_attendance_reject_master.COMP_CODE AND pay_unit_master.unit_code = pay_attendance_reject_master.unit_code) AS 'branch_name', CONCAT(pay_attendance_reject_master.month, '/', pay_attendance_reject_master.year) AS 'month_year', file_name FROM pay_files_timesheet_history INNER JOIN pay_attendance_reject_master ON pay_files_timesheet_history.comp_code = pay_attendance_reject_master.comp_code AND pay_files_timesheet_history.client_code = pay_attendance_reject_master.client_code and pay_files_timesheet_history.unit_code = pay_attendance_reject_master.unit_code WHERE pay_attendance_reject_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_attendance_reject_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_attendance_reject_master.month = '" + txttodate.Text.Substring(0, 2).ToString() + "' AND pay_attendance_reject_master.year = '" + txttodate.Text.Substring(3).ToString() + "' AND pay_attendance_reject_master.flag = '2'     AND pay_files_timesheet_history.flag = '2' ", d.con);

            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pay_files_timesheet.unit_code, (SELECT client_name FROM pay_client_master WHERE pay_client_master.client_code = pay_files_timesheet.client_code) AS 'client_name', pay_files_timesheet.state AS 'state_name', (SELECT unit_name FROM pay_unit_master WHERE pay_unit_master.COMP_CODE = pay_files_timesheet.COMP_CODE AND pay_unit_master.unit_code = pay_files_timesheet.unit_code) AS 'branch_name', CONCAT(pay_files_timesheet.month, '/', pay_files_timesheet.year) AS 'month_year', file_name FROM pay_files_timesheet INNER JOIN pay_attendance_muster ON pay_files_timesheet.comp_code = pay_attendance_muster.comp_code  AND pay_files_timesheet.unit_code = pay_attendance_muster.unit_code WHERE pay_files_timesheet.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_files_timesheet.client_code = '" + ddl_client.SelectedValue + "' AND pay_files_timesheet.unit_code IN (" + unit_code + ") AND pay_files_timesheet.month = '" + txttodate.Text.Substring(0, 2).ToString() + "' AND pay_files_timesheet.year = '" + txttodate.Text.Substring(3).ToString() + "' AND pay_files_timesheet.flag = '2'  AND pay_attendance_muster.flag = '2' ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grid_approve_attendace.DataSource = dt;
                grid_approve_attendace.DataBind();
                gv_approve_panel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { d.con.Close(); }

    }
    protected void downloadfile_material(string filename, string unit_name) 
    {
        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\material_images\\" + filename);
            string unitName = unit_name + "-material_images" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\material_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();

        }
        catch (Exception ex) { }

    }
    protected void downloadfile(string filename, string unit_name)
    {

        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\approved_attendance_images\\" + filename);
            string unitName = unit_name + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\approved_attendance_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();





        }
        catch (Exception ex) { }


    }

    protected void lnkDownload_Command(object sender, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }
    }

    protected void grid_reject_attendace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {

            }
        }
        e.Row.Cells[4].Visible = false;
    }
    protected void grid_reject_attendace_PreRender(object sender, EventArgs e)
    {
        try
        {
            grid_reject_attendace.UseAccessibleHeader = false;
            grid_reject_attendace.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    public string GenerateFileName(string unit_code)
    {
        return unit_code + "_" + Guid.NewGuid();
    }

    protected void upload_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }

        if (document1_file.HasFile)
        {

            string fileExt = System.IO.Path.GetExtension(document1_file.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(document1_file.PostedFile.FileName);
                document1_file.PostedFile.SaveAs(Server.MapPath("~/approved_attendance_images/") + fileName);
                string id = d.getsinglestring("select ifnull(max(id),0) from pay_files_timesheet");

                string file_name = GenerateFileName(ddl_unitcode.SelectedValue);
                string new_file_name = ddl_client.SelectedValue + "_" + file_name.Substring(0, 10) + id + fileExt;

                File.Copy(Server.MapPath("~/approved_attendance_images/") + fileName, Server.MapPath("~/approved_attendance_images/") + new_file_name, true);
                File.Delete(Server.MapPath("~/approved_attendance_images/") + fileName);

                string function = where_function();
                string unit_code = "";

                if (ddl_unitcode.SelectedValue == "ALL")
                {
                    unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' and branch_status = '0'  AND  unit_code  IN (SELECT DISTINCT ( unit_code ) FROM  pay_employee_master  WHERE '" + Session["comp_code"].ToString() + "' =  pay_employee_master . comp_code  AND  pay_employee_master . client_code  = '" + ddl_client.SelectedValue + "' and  client_wise_state  = '" + ddl_state.SelectedValue + "' " + function + ")");
                    unit_code = "'" + unit_code + "'";
                    unit_code = unit_code.Replace(",", "','");
                }
                else
                {
                    Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                    //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                    unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
                }

                string[] abc = unit_code.Split(',');


                if (d.getsinglestring("select  pay_files_timesheet. unit_code  from pay_files_timesheet   where pay_files_timesheet.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_files_timesheet.client_Code='" + ddl_client.SelectedValue + "' and   month='" + txttodate.Text.Substring(0, 2) + "' and year='" + txttodate.Text.Substring(3) + "' and pay_files_timesheet.unit_code IN(" + unit_code + ") ") == "")
                {
                    foreach (object obj in abc)
                    {
                        d.operation("insert into pay_files_timesheet (comp_code, client_code, unit_code, file_name, description, month, year, uploaded_by, uploaded_date,state) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client.SelectedValue + "'," + obj + ",'" + new_file_name + "','" + txt_document1.Text + "'," + int.Parse(txttodate.Text.Substring(0, 2)) + "," + int.Parse(txttodate.Text.Substring(3)) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),'" + ddl_state.SelectedValue + "')");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Attendance Sheet Upload Succsefully Please  Click on Approve to Approve Attendance !!');", true);
                    }
                }
                else
                {
                    foreach (object obj in abc)
                    {

                        d.operation("update pay_files_timesheet set file_name='" + new_file_name + "', description='" + txt_document1.Text + "', uploaded_by='" + Session["LOGIN_ID"].ToString() + "', uploaded_date=now() , flag = 0 ,status = NULL where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client.SelectedValue + "' and   month='" + txttodate.Text.Substring(0, 2) + "' and year='" + txttodate.Text.Substring(3) + "' and unit_code= " + obj + " ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Attendance Sheet Upload Succsefully ..!!  Please  Click on Approve to Approve Attendance!!');", true);
                    }
                }


            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select only JPG, PNG and PDF Files  !!');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
                return;
            }





        }
    }

    //deployment gv

    protected void gv_load_deployment()
    {
        try
        {
            DataSet ds = new DataSet();
            string where = "", where1 = "";
            where = "comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  client_code  = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "'";
            //  where1 = "comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  client_code  = '" + ddl_client.SelectedValue + "'";

            ds = d.select_data("SELECT (select client_name from pay_client_master where client_code= '" + ddl_client.SelectedValue + "') as 'client_name', pay_unit_master . STATE_NAME ,  UNIT_NAME,unit_code   FROM  pay_unit_master  WHERE   " + where + " AND  unit_code  NOT IN (SELECT DISTINCT  unit_code  FROM  pay_employee_master  WHERE  " + where + " AND (LEFT_DATE >= '" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text) + "' || left_date IS NULL) AND joining_date <= '" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text) + "') and deployment_flag = 0");

            gv_deployment.DataSource = null;
            gv_deployment.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_deployment.DataSource = ds.Tables[0];
                gv_deployment.DataBind();
                gv_deployment_panel.Visible = true;
            }
            ds.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }

    protected void lnk_deployment_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        string unit_code = e.CommandArgument.ToString();

        result = d.operation("update pay_unit_master set deployment_flag = '1' , unit_month='" + txttodate.Text.Substring(0, 2) + "' , unit_year ='" + txttodate.Text.Substring(3) + "' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + unit_code + "'");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch  Approved Successfully !!');", true);

        gv_load_deployment();
        attendance_status();

    }
    protected void lnk_hold_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        string unit_code = e.CommandArgument.ToString();

        result = d.operation("update pay_unit_master set deployment_flag = '0' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + unit_code + "'");


        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch  Are Hold !!');", true);
        gv_load_deployment();
        attendance_status();

    }

    protected void btn_reports_Click(object sender, EventArgs e)
    {
        attendance_status();
    }

    //Conveyance

    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        if (con_ddl_client.SelectedValue != "Select")
        {
            d1.con1.Open();
            con_ddl_state.Items.Clear();
            driver_text_clear();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                // comment 30/09 MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + con_ddl_client.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + con_ddl_client.SelectedValue + "')  ORDER BY STATE", d1.con1);
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(" SELECT DISTINCT  state  FROM  pay_designation_count  WHERE  CLIENT_CODE  = '" + con_ddl_client.SelectedValue + "' AND  state  IN (SELECT  pay_unit_master . state_name  FROM  pay_client_state_role_grade  INNER JOIN  pay_unit_master  ON  pay_client_state_role_grade . client_code  =  pay_unit_master . client_code  AND  pay_client_state_role_grade . state_name  =  pay_unit_master . state_name  WHERE  pay_client_state_role_grade . COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ( branch_close_date  IS NULL ||  branch_close_date  = '' ||  branch_close_date  >= STR_TO_DATE('01/" + txt_date_conveyance.Text + "', '%d/%m/%Y')) AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND pay_client_state_role_grade.client_code='" + con_ddl_client.SelectedValue + "')  ORDER BY STATE", d1.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                con_ddl_state.DataSource = DS;
                con_ddl_state.DataBind();
                con_ddl_state.Items.Insert(0, "Select");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con1.Close();
            }


            con_ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + con_ddl_client.SelectedValue + "' AND state_name ='" + con_ddl_state.SelectedValue + "' and UNIT_CODE in(SELECT  pay_client_state_role_grade . UNIT_CODE  FROM  pay_client_state_role_grade  INNER JOIN  pay_unit_master  ON  pay_client_state_role_grade . COMP_CODE  =  pay_unit_master . COMP_CODE  AND  pay_client_state_role_grade . client_code  =  pay_unit_master . client_code  where  pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND pay_client_state_role_grade.client_code='" + con_ddl_client.SelectedValue + "' AND pay_client_state_role_grade.state_name='" + con_ddl_state.SelectedValue + "'  AND ( branch_close_date  IS NULL ||  branch_close_date  = '' ||  branch_close_date  >= STR_TO_DATE('01/" + txt_date_conveyance.Text + "', '%d/%m/%Y'))) ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    con_ddl_unitcode.DataSource = dt_item;
                    con_ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    con_ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    con_ddl_unitcode.DataBind();
                    con_ddl_unitcode.Items.Insert(0, "Select");
                }
                dt_item.Dispose();
                d.con.Close();

                //ddl_unitcode.SelectedIndex = 0;
                ddl_state_SelectedIndexChanged1(null, null);
                ddlunitselect_SelectedIndexChanged(null, null);
                //ddlemployee_SelectedIndexChanged(null,null);
                ddl_employee_type.SelectedIndex = 0;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
    }
    protected void ddl_state_SelectedIndexChanged1(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        if (con_ddl_client.SelectedValue != "Select")
        {
            con_ddl_unitcode.Items.Clear();
            driver_text_clear();

            System.Data.DataTable dt_item = new System.Data.DataTable();
            //comment 30/09 branch_closing    MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + con_ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + con_ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + con_ddl_client.SelectedValue + "' AND state_name='" + con_ddl_state.SelectedValue + "') AND pay_unit_master.branch_status = 0   ORDER BY pay_unit_master.state_name", d.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + con_ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + con_ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + con_ddl_client.SelectedValue + "' AND state_name='" + con_ddl_state.SelectedValue + "') AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txt_date_conveyance.Text + "', '%d/%m/%Y'))  ORDER BY pay_unit_master.state_name", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    con_ddl_unitcode.DataSource = dt_item;
                    con_ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    con_ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    con_ddl_unitcode.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                con_ddl_unitcode.Items.Insert(0, "Select");
                //ddl_unitcode.SelectedIndex = 0;
                ddlunitselect_SelectedIndexChanged(null, null);
                ddlemployee_SelectedIndexChanged(null, null);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }
    protected void ddlunitselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        grd_convayance.DataSource = null;
        grd_convayance.DataBind();

        hidtab.Value = "1";
        ddl_employee.Items.Clear();
        driver_text_clear();
        if (ddl_employee_type.SelectedValue != "Select")
        {
            // con_button.Visible = true;
            btn_conv_save.Visible = true;
            Button1.Visible = true;
            btn_approve_conveyance.Visible = true;
            btn_conv_link.Visible = true;
        }
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item;
        string left = " employee_type = '" + ddl_employee_type.SelectedValue + "' and  (left_date = '' or left_date is null)";
        if (ddl_employee_type.SelectedValue == "Left")
        {
            left = " left_date is not null";
        }
        string where = " where  comp_code='" + Session["comp_code"] + "' and client_code = '" + con_ddl_client.SelectedValue + "' and unit_code='" + con_ddl_unitcode.SelectedValue + "' and " + left + " ORDER BY emp_name";
        if (con_ddl_unitcode.SelectedValue == "ALL")
        {
            where = " where comp_code='" + Session["comp_code"] + "' and client_code = '" + con_ddl_client.SelectedValue + "' and EMP_CURRENT_STATE='" + con_ddl_state.SelectedValue + "' and " + left + " ORDER BY emp_name";
        }
        //vikas 08-01-19
        cmd_item = new MySqlDataAdapter("Select (SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',EMP_CODE from pay_employee_master " + where, d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_employee.DataSource = dt_item;
                ddl_employee.DataTextField = dt_item.Columns[0].ToString();
                ddl_employee.DataValueField = dt_item.Columns[1].ToString();
                ddl_employee.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_employee.Items.Insert(0, "ALL");
            ddl_employee.SelectedIndex = 0;
            ddlemployee_SelectedIndexChanged(null, null);

            //komal 12-06-19
            if (ddl_employee_type.SelectedValue == "Permanent")
            {
                convayance_details();
                conveyance_location();
                

            }
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        conveyance_location();

    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try
        {
            driver.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d2.con.Open();
            //MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,fine,cca_desc,advance_desc ,fine_desc,conveyance_rate,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d.con);
            MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,cca_desc,advance_desc,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d2.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                select_driver_record();
            }
            dr.Close();
            d2.con.Close();
        }
        catch (Exception ex)
        {
        }
        finally { d2.con.Close(); }
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
        driver_conveyance_location();

    }
    protected void btn_upload_Click1(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        hidtab.Value = "1";
        if (bill_upload.HasFile)
        {
            conveyance_images();
            conveyance_location();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select File to upload !!')", true);
        }
    }

    //komal 12-06-19
    protected void conveyance_location()
    {
        try
        {
            grd_convayance_location.Visible = true;
            gv_bill_list_upload.Visible = true;
            System.Data.DataTable dt = new System.Data.DataTable();

            //apporve_attendace gridview
            grd_convayance_location.DataSource = null;
            grd_convayance_location.DataBind();
            //gv_bill_list_upload.DataSource = null;
            //gv_bill_list_upload.DataBind();

            d4.con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT client_name, pay_unit_master.state_name, unit_name, CAST(CONCAT(pay_conveyance_upload.month, '/', pay_conveyance_upload.year) AS char) AS 'month', pay_conveyance_upload.conveyance_images FROM pay_conveyance_upload INNER JOIN pay_client_master ON pay_conveyance_upload.comp_code = pay_client_master.comp_code AND pay_conveyance_upload.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_conveyance_upload.unit_code = pay_unit_master.unit_code AND pay_conveyance_upload.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND pay_unit_master.client_code = '" + con_ddl_client.SelectedValue + "' AND pay_unit_master.UNIT_CODE = '" + con_ddl_unitcode.SelectedValue + "'  AND pay_unit_master.state_name = '" + con_ddl_state.SelectedValue + "'   AND pay_conveyance_upload.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_upload.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'  AND conveyance_type is null ", d.con);

            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grd_convayance_location.DataSource = dt;
                grd_convayance_location.DataBind();
                //gv_approve_panel.Visible = true;
            }
            d4.con.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close(); d.con1.Close(); d4.con.Close();
        }


    }
    protected void conveyance_images()
    {

        try
        {
            d.con.Open();
            string fileExt = "";
            string bill_upload1 = "";

            fileExt = System.IO.Path.GetExtension(bill_upload.FileName);
            bill_upload1 = Path.GetFileName(bill_upload.PostedFile.FileName);


            string fname = null;
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = bill_upload1;
                bill_upload.PostedFile.SaveAs(Server.MapPath("~/approved_attendance_images/") + fileName);
                fname = Session["COMP_CODE"].ToString() + "_" + con_ddl_client.SelectedValue + "_" + con_ddl_unitcode.SelectedValue + "_" + txt_date_conveyance.Text.Substring(0, 2) + txt_date_conveyance.Text.Substring(3) + fileExt;
                File.Copy(Server.MapPath("~/approved_attendance_images/") + fileName, Server.MapPath("~/approved_attendance_images/") + fname, true);
                File.Delete(Server.MapPath("~/approved_attendance_images/") + fileName);

                d.operation("delete from pay_conveyance_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and client_code = '" + con_ddl_client.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'");

                int result = d.operation("insert into pay_conveyance_upload(comp_code,unit_code,client_code,state,employee_type,month,year,conveyance_images)values('" + Session["COMP_CODE"].ToString() + "','" + con_ddl_unitcode.SelectedValue + "','" + con_ddl_client.SelectedValue + "','" + con_ddl_state.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + fname + "')");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploaded Successfully... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploading Failed... !!!');", true);
                }
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void downloadfile1(string filename, string unit_name)
    {

        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\approved_attendance_images\\" + filename);
            string unitName = unit_name + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\approved_attendance_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();


        }
        catch (Exception ex) { }

    }

    protected void grd_convayance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        e.Row.Cells[1].Visible = false;
    }
    protected void lnk_conveyance_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Downloaded !!!')", true);
        }

    }
    protected void btn_conv_save_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string check_con = "";
        check_con = d.getsinglestring(" select billing_unit_code from pay_billing_master where billing_unit_code = '" + con_ddl_unitcode.SelectedValue + "' and conveyance_applicable = 1");
        if (check_con != "")
        {
            try
            {
                foreach (GridViewRow gr in grd_convayance.Rows)
                {
                    string cell_1_Value = grd_convayance.Rows[gr.RowIndex].Cells[1].Text;
                    string cell_2_Value = grd_convayance.Rows[gr.RowIndex].Cells[2].Text;
                    System.Web.UI.WebControls.TextBox lbl_material_name = (System.Web.UI.WebControls.TextBox)gr.FindControl("txt_conveyance_amount");
                    string material_name = lbl_material_name.Text;
                    System.Web.UI.WebControls.TextBox lbl_con_deduction = (System.Web.UI.WebControls.TextBox)gr.FindControl("txt_conveyance_deduction");
                    string lbl_material_name_1 = (lbl_material_name.Text == "" ? "0" : lbl_material_name.Text);
                    string lbl_con_deduction1 = (lbl_con_deduction.Text == "" ? "0" : lbl_con_deduction.Text);

                d.operation("delete from pay_conveyance_amount_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and client_code = '" + con_ddl_client.SelectedValue + "' and emp_code = '" + cell_1_Value + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "' and conveyance ='emp_conveyance'");
                if (lbl_material_name_1 != "" && lbl_material_name_1 != "0")
                {
                    d.operation("insert into pay_conveyance_amount_history (comp_code,unit_code,client_code,employee_type,state,month,year,EMP_CODE,emp_name,conveyance_rate,emp_con_deduction,conveyance)value('" + Session["COMP_CODE"] + "','" + con_ddl_unitcode.SelectedValue + "','" + con_ddl_client.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + con_ddl_state.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + cell_1_Value + "','" + cell_2_Value + "','" + lbl_material_name_1 + "','" + lbl_con_deduction1 + "','emp_conveyance')");
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally { }
        if (!conveyance_upload())
        {
            convayance_details();
            return;
        }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Save successfully!! Please  Click on Approve to Approve Conveyance!!');", true);
        }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('For This Branch Conveyance Policy Not Created !!!');", true); }

    }
    //komal 12-06-19 

    protected void convayance_details()
    {
        string approve_conv = "";
        string where = "";
        string left = " pay_employee_master.employee_type = '" + ddl_employee_type.SelectedValue + "' and  (pay_employee_master.left_date = '' or pay_employee_master.left_date is null)";
        if (ddl_employee_type.SelectedValue == "Left")
        {
            left = " pay_employee_master.left_date is not null";
        }
        where = " where  pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + con_ddl_client.SelectedValue + "' and pay_employee_master.unit_code='" + con_ddl_unitcode.SelectedValue + "'  and " + left + " ORDER BY pay_employee_master.emp_name";
        if (con_ddl_unitcode.SelectedValue == "ALL")
        {
            where = " where pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + con_ddl_client.SelectedValue + "' and pay_employee_master.EMP_CURRENT_STATE='" + con_ddl_state.SelectedValue + "'  and " + left + " ORDER BY pay_employee_master.emp_name";
            }


            approve_conv = d.getsinglestring("select conveyance_flag from pay_conveyance_amount_history where comp_code='" + Session["COMP_CODE"].ToString() + "'and client_code = '" + con_ddl_client.SelectedValue + "'and state = '" + con_ddl_state.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "'  and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and conveyance_rate != 0");
            if (approve_conv=="")
        {
            approve_conv = "0";
        }
        MySqlCommand cmd11 = null;
        if (approve_conv == "0" || approve_conv == "3")
 {
     cmd11 = new MySqlCommand("select pay_employee_master.EMP_NAME,pay_employee_master.EMP_CODE,pay_conveyance_amount_history.conveyance_rate,pay_conveyance_amount_history.emp_con_deduction,pay_conveyance_amount_history.month, pay_conveyance_amount_history.year, CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS 'month' FROM  pay_employee_master LEFT OUTER JOIN pay_conveyance_amount_history  ON pay_employee_master.comp_code = pay_conveyance_amount_history.comp_code  AND pay_employee_master.client_code = pay_conveyance_amount_history.client_code  AND pay_employee_master.unit_code = pay_conveyance_amount_history.unit_code  AND pay_employee_master.emp_code = pay_conveyance_amount_history.emp_code AND  (pay_conveyance_amount_history.conveyance_rate IS not NULL || pay_conveyance_amount_history.conveyance_rate='') AND pay_conveyance_amount_history.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_amount_history.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'" + where, d.con);

   
        }
        else if (approve_conv == "1" || approve_conv == "2")
         {
     btn_conv_save.Visible = false;
     btn_approve_conveyance.Visible = false;
      //cmd11 = new MySqlCommand("select pay_employee_master.EMP_NAME,pay_employee_master.EMP_CODE,pay_conveyance_amount_history.conveyance_rate,pay_conveyance_amount_history.month, pay_conveyance_amount_history.year, CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS 'month' FROM  pay_employee_master LEFT OUTER JOIN pay_conveyance_amount_history  ON pay_employee_master.comp_code = pay_conveyance_amount_history.comp_code  AND pay_employee_master.client_code = pay_conveyance_amount_history.client_code  AND pay_employee_master.unit_code = pay_conveyance_amount_history.unit_code  AND pay_employee_master.emp_code = pay_conveyance_amount_history.emp_code AND  (pay_conveyance_amount_history.conveyance_rate IS not NULL || pay_conveyance_amount_history.conveyance_rate='') AND pay_conveyance_amount_history.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_amount_history.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'" + where, d.con);
     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Approved by Admin you can not make changes !!');", true);
     return;
         }      
            
            d.con.Open();

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr1 = cmd11.ExecuteReader();
            if (dr1.HasRows)
            {

                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2.Load(dr1);
                if (dt2.Rows.Count > 0)
                {

                    ViewState["grd_convayance"] = dt2;
                }
                grd_convayance.DataSource = dt2;
                grd_convayance.DataBind();

            }
            dr1.Close();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    //komal 12-06-19


    protected bool conveyance_upload()
    {


        if (d.getsinglestring("select conveyance_images from pay_conveyance_upload where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + con_ddl_client.SelectedValue + "' and unit_code= '" + con_ddl_unitcode.SelectedValue + "' and employee_type in ('Permanent','Temporary') and state = '" + con_ddl_state.SelectedValue + "'and month ='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' ") == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Upload Conveyance Sheet  !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_driver_convence_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        hidtab.Value = "1";

        if (con_bill_upload.HasFile)
        {
            driver_conveyance_images();
            driver_conveyance_location();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select File to upload !!')", true);
        }
    }
    protected void btn_drive_save_Click(object sender, EventArgs e)
    {
        HiddenField2.Value = "1";
        if (d.getsinglestring("select comp_code from pay_conveyance_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and client_code = '" + con_ddl_client.SelectedValue + "' and emp_code = '" + ddl_employee.SelectedValue + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'").Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload conveyance file !!');", true);
            return;
        }

        try
        {


            if (txt_food_days.Text == "")
            { txt_food_days.Text = "0"; }
            if (txt_oc_days.Text == "") { txt_oc_days.Text = "0"; }
            if (txt_os_days.Text == "") { txt_os_days.Text = "0"; }
            if (txt_nh_days.Text == "") { txt_nh_days.Text = "0"; }
            if (txt_total_km.Text == "") { txt_total_km.Text = "0"; }
            if (txt_deduction_amount.Text == "") { txt_deduction_amount.Text = "0"; }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string check_deg = d.getsinglestring(" select grade_code from pay_employee_master where emp_code= '" + ddl_employee.SelectedValue + "'");
            if (check_deg == "DR")
            {
                MySqlDataAdapter cmd = new MySqlDataAdapter("select food_allowance_days,outstation_allowance_days,outstation_food_allowance_days,night_halt_days,total_km,driver_con_deduction from pay_conveyance_amount_history where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "' and EMP_CODE='" + ddl_employee.SelectedValue + "' and conveyance='driver_conveyance' ", d.con);
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    d.operation("update pay_conveyance_amount_history set food_allowance_days = '" + txt_food_days.Text + "',outstation_allowance_days = '" + txt_oc_days.Text + "',outstation_food_allowance_days = '" + txt_os_days.Text + "',night_halt_days = '" + txt_nh_days.Text + "',total_km = '" + txt_total_km.Text + "',driver_con_deduction = '" + txt_deduction_amount.Text + "',conveyance='driver_conveyance' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "' and EMP_CODE='" + ddl_employee.SelectedValue + "' and conveyance='driver_conveyance'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Update successfully!!');", true);
                }
                else
                {
                    d.operation("insert into pay_conveyance_amount_history (comp_code,unit_code,client_code,employee_type,state,month,year,EMP_CODE,emp_name,food_allowance_days,outstation_allowance_days,outstation_food_allowance_days,night_halt_days,total_km,driver_con_deduction,conveyance)value('" + Session["COMP_CODE"] + "','" + con_ddl_unitcode.SelectedValue + "','" + con_ddl_client.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + con_ddl_state.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + ddl_employee.SelectedValue + "','" + ddl_employee.SelectedItem + "','" + txt_food_days.Text + "','" + txt_oc_days.Text + "','" + txt_os_days.Text + "','" + txt_nh_days.Text + "','" + txt_total_km.Text + "','" + txt_deduction_amount.Text + "','driver_conveyance')");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save successfully Please Approve This Employee!!');", true);
                }
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Driver Empployee!!');", true); }

        }
        catch (Exception ex) { throw ex; }
        finally
        {

           // driver_text_clear();
            //ddl_employee.SelectedValue = "ALL";
        }


    }
    protected void lnk_driver_conveyance_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile2(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Downloaded !!!')", true);
        }

    }
    protected void driver_text_clear()
    {

        txt_food_days.Text = "0";

        txt_oc_days.Text = "0";

        txt_os_days.Text = "0";

        txt_nh_days.Text = "0";

        txt_total_km.Text = "0";

        txt_deduction_amount.Text = "0";
      
        //ddlemployee_SelectedIndexChanged(null, null);
    }
    protected void select_driver_record()
    {
        try
        {
            string approve_conv_driver = "";
            d5.con.Open();

            //MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,fine,cca_desc,advance_desc ,fine_desc,conveyance_rate,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d.con);
            approve_conv_driver = d.getsinglestring("select driver_conv_flag from pay_conveyance_amount_history where comp_code='" + Session["COMP_CODE"].ToString() + "'and client_code = '" + con_ddl_client.SelectedValue + "'and state = '" + con_ddl_state.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "'  and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "'  and emp_code = '" + ddl_employee.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'");
            if (approve_conv_driver == "")
            {
                approve_conv_driver = "0";
            }
            MySqlCommand cmd = null;
            if (approve_conv_driver == "0" || approve_conv_driver == "3")
            {

                cmd = new MySqlCommand("select food_allowance_days,outstation_allowance_days,outstation_food_allowance_days,night_halt_days,total_km,driver_con_deduction from pay_conveyance_amount_history where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "' and EMP_CODE='" + ddl_employee.SelectedValue + "' AND (pay_conveyance_amount_history.conveyance_rate IS  NULL || pay_conveyance_amount_history.conveyance_rate='') ", d5.con);
                Panel_driver_conv.Visible = true;
                btn_drive_save.Visible = true;
                btn_drive_approve.Visible = true;
                btn_report.Visible = true;
            }
            else if (approve_conv_driver == "1" || approve_conv_driver == "2")
            {
                btn_drive_save.Visible = false;
                btn_drive_approve.Visible = false;
                btn_report.Visible = false;
                Panel_driver_conv.Visible = false;
                //cmd11 = new MySqlCommand("select pay_employee_master.EMP_NAME,pay_employee_master.EMP_CODE,pay_conveyance_amount_history.conveyance_rate,pay_conveyance_amount_history.month, pay_conveyance_amount_history.year, CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS 'month' FROM  pay_employee_master LEFT OUTER JOIN pay_conveyance_amount_history  ON pay_employee_master.comp_code = pay_conveyance_amount_history.comp_code  AND pay_employee_master.client_code = pay_conveyance_amount_history.client_code  AND pay_employee_master.unit_code = pay_conveyance_amount_history.unit_code  AND pay_employee_master.emp_code = pay_conveyance_amount_history.emp_code AND  (pay_conveyance_amount_history.conveyance_rate IS not NULL || pay_conveyance_amount_history.conveyance_rate='') AND pay_conveyance_amount_history.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_amount_history.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'" + where, d.con);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved by Admin you can not make changes !!');", true);
                return;
            
            
            }
            
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {


                txt_food_days.Text = dr.GetValue(0).ToString();

                txt_oc_days.Text = dr.GetValue(1).ToString();

                txt_os_days.Text = dr.GetValue(2).ToString();

                txt_nh_days.Text = dr.GetValue(3).ToString();

                txt_total_km.Text = dr.GetValue(4).ToString();

                txt_deduction_amount.Text = dr.GetValue(5).ToString();

            }
            dr.Close();
            d5.con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { d5.con.Close(); }


    }

    protected void driver_conveyance_location()
    {
        try
        {
            grd_convayance_location.Visible = true;
            gv_bill_list_upload.Visible = true;
            System.Data.DataTable dt = new System.Data.DataTable();

            //apporve_attendace gridview

            gv_bill_list_upload.DataSource = null;
            gv_bill_list_upload.DataBind();


            d.con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT client_name, pay_unit_master.state_name, unit_name, CAST(CONCAT(pay_conveyance_upload.month, '/', pay_conveyance_upload.year) AS char) AS 'month', pay_conveyance_upload.conveyance_images FROM pay_conveyance_upload INNER JOIN pay_client_master ON pay_conveyance_upload.comp_code = pay_client_master.comp_code AND pay_conveyance_upload.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_conveyance_upload.unit_code = pay_unit_master.unit_code AND pay_conveyance_upload.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND pay_unit_master.client_code = '" + con_ddl_client.SelectedValue + "' AND pay_unit_master.UNIT_CODE = '" + con_ddl_unitcode.SelectedValue + "'  AND pay_unit_master.state_name = '" + con_ddl_state.SelectedValue + "'  AND pay_conveyance_upload.emp_code = '" + ddl_employee.SelectedValue + "'  AND pay_conveyance_upload.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_upload.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'", d.con1);

            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gv_bill_list_upload.DataSource = dt;
                gv_bill_list_upload.DataBind();
                //gv_approve_panel.Visible = true;
            }
            d.con1.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close(); d.con1.Close(); d4.con.Close();
        }


    }
    protected void driver_conveyance_images()
    {

        try
        {
            d.con.Open();
            string fileExt = "";
            string bill_upload1 = "";



            //con_bill_upload as bill_upload
            fileExt = System.IO.Path.GetExtension(con_bill_upload.FileName);
            bill_upload1 = Path.GetFileName(con_bill_upload.PostedFile.FileName);

            string fname = null;
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = bill_upload1;
                bill_upload.PostedFile.SaveAs(Server.MapPath("~/approved_attendance_images/") + fileName);
                fname = Session["COMP_CODE"].ToString() + "_" + con_ddl_client.SelectedValue + "_" + con_ddl_unitcode.SelectedValue + "_" + txt_date_conveyance.Text.Substring(0, 2) + txt_date_conveyance.Text.Substring(3) + fileExt;
                File.Copy(Server.MapPath("~/approved_attendance_images/") + fileName, Server.MapPath("~/approved_attendance_images/") + fname, true);
                File.Delete(Server.MapPath("~/approved_attendance_images/") + fileName);

                d.operation("delete from pay_conveyance_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and client_code = '" + con_ddl_client.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and emp_code = '" + ddl_employee.SelectedValue + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'");
                string conveyance = "", emp_code = "";


                conveyance = "driver_conveyance";
                emp_code = ddl_employee.SelectedValue;

                int result = d.operation("insert into pay_conveyance_upload(comp_code,unit_code,client_code,state,employee_type,month,year,conveyance_images,conveyance_type,emp_code)values('" + Session["COMP_CODE"].ToString() + "','" + con_ddl_unitcode.SelectedValue + "','" + con_ddl_client.SelectedValue + "','" + con_ddl_state.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + fname + "','" + conveyance + "','" + emp_code + "')");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploaded Successfully... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploading Failed... !!!');", true);
                }
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void downloadfile2(string filename, string unit_name)
    {

        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\approved_attendance_images\\" + filename);
            string unitName = unit_name + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\approved_attendance_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();


        }
        catch (Exception ex) { }

    }

    protected void ddl_client_material_SelectedIndexChanged(object sender, EventArgs e)
    {
        //State
        hidtab.Value = "2";
        ddl_state_material.Items.Clear();
       // ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);

        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_material.DataSource = dt_item;
                ddl_state_material.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_material.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_material.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_material.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
           // ddl_unitcode.Items.Clear();
        }
        Notification_panel.Visible = false;
        //Panel1.Visible = false;
    }

    protected void ddl_state_material_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
       txttodate.Text = "";
       ddl_branch_material.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state_material.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_material.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_material.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_material.DataSource = dt_item;
                ddl_branch_material.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_material.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_material.DataBind();
                ddl_branch_material.Items.Insert(0, "Select");
                ddl_branch_material.Items.Insert(1, "ALL");
            }
            dt_item.Dispose();
            d.con.Close();
            ////vikas
            //if (txttodate.Text.Length > 0)
            //{
            //    // hidden_month.Value = txttodate.Text.Substring(0, 2);
            //    //hidden_year.Value = txttodate.Text.Substring(3);
            //    // gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 0);
            //}

            //else
            //{
            //    Panel1.Visible = false;
            //    pnl_branch.Visible = false;
            //    btn_attendance.Visible = false;
            //}
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ////Session["state_new_emp"] = ddl_state.SelectedValue;
            ////d.con.Close();
            ////ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
           

    }
    protected void btn_save_material_Click(object sender, EventArgs e)
    {
       

        if (ddl_material_amount_type.SelectedValue=="0")
        {
            hidtab.Value = "2";

            int result = 0;
            string unit_code = "";
            if (ddl_branch_material.SelectedValue == "ALL")
            {
                unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
                unit_code = "'" + unit_code + "'";
                unit_code = unit_code.Replace(",", "','");
            }
            else
            {
                Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }



        string check_material_type = "";
        check_material_type = d.getsinglestring(" select billing_unit_code from pay_billing_master where billing_unit_code in(" + unit_code + ") and contract_type = 4 ");
        if (check_material_type != "")
        {
            try
            {
             


            //if (d.getsinglestring("select unit_code from pay_material_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and   month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and material_flag = 0 ") == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Attendance Sheet  !!');", true);
            //    return;
            //}


            foreach (GridViewRow row in grid_material_amount.Rows)
            {
                string emp_code = row.Cells[4].Text;
                string unit_code_grv = row.Cells[2].Text;

                TextBox txt_returnqty = (TextBox)row.FindControl("txt_material_amount");
                string amount_material = (txt_returnqty.Text);

                TextBox txt_returnqtyd = (TextBox)row.FindControl("txt_material_deduction");
                string material_deduction = (txt_returnqtyd.Text);

                d.operation("delete from pay_material_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code_grv+ "' and client_code = '" + ddl_client_material.SelectedValue + "' and emp_code = '" + emp_code + "' and month  = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3, 4) + "'");
              
                if (amount_material != "" && amount_material != "0")
                {

                    result = d.operation(" insert into pay_material_details(comp_code,client_code,state_name,month,year,emp_code,unit_code,material_amount,material_amount_type,material_deduction)value ('" + Session["comp_code"].ToString() + "','" + ddl_client_material.SelectedValue + "','" + ddl_state_material.SelectedValue + "','" + txt_month_material.Text.Substring(0, 2) + "','" + txt_month_material.Text.Substring(3) + "', '" + emp_code + "','" + unit_code_grv + "','" + amount_material + "','" + ddl_material_amount_type.SelectedValue + "','" + material_deduction + "')");
                }
                //if (dispatch1 > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Amount Added Successfully... !!! Please  Click on Approve to Approve Attendance!! ');", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Added Failed... !!!');", true);
                //}


                }
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Amount Added Successfully... !!! Please  Click on Approve to Approve Material!! ');", true);
                }
                else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Enter Amount to Material Does not insert 0 Value!! ');", true); }
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('For This Branch EmployeeWise Contract Material Policy  Not Created !!!');", true); }

    }
    // locationwise material komal 15-05-2020
        if (ddl_material_amount_type.SelectedValue=="1") 
        {
            HiddenField2.Value = "1";

            int result = 0;
            string unit_code = "";
            if (ddl_branch_material.SelectedValue == "ALL")
            {
                unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
                unit_code = "'" + unit_code + "'";
                unit_code = unit_code.Replace(",", "','");
            }
            else
            {
                Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }


            string check_material_type = "";
            check_material_type = d.getsinglestring(" select distinct`billing_client_code` from pay_billing_master where `billing_client_code` = '" + ddl_client_material.SelectedValue + "' and contract_type = 1 ");
            if (check_material_type != "")
            {
                try
                {
                

                    //if (d.getsinglestring("select unit_code from pay_material_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and   month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and material_flag = 0 ") == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Attendance Sheet  !!');", true);
                    //    return;
                    //}


                    foreach (GridViewRow row in gv_branchwise_material.Rows)
                    {
                       // string emp_code = row.Cells[4].Text;
                        string unit_code_grv = row.Cells[2].Text;

                        TextBox txt_returnqty = (TextBox)row.FindControl("txt_material_amount");
                        string amount_material = (txt_returnqty.Text);

                        d.operation("delete from pay_material_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code_grv + "' and client_code = '" + ddl_client_material.SelectedValue + "'  and month  = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3, 4) + "'");

                        if (amount_material != "" && amount_material != "0")
                        {

                            result = d.operation(" insert into pay_material_details(comp_code,client_code,state_name,month,year,unit_code,material_amount,material_amount_type)value ('" + Session["comp_code"].ToString() + "','" + ddl_client_material.SelectedValue + "','" + ddl_state_material.SelectedValue + "','" + txt_month_material.Text.Substring(0, 2) + "','" + txt_month_material.Text.Substring(3) + "','" + unit_code_grv + "','" + amount_material + "','" + ddl_material_amount_type.SelectedValue + "')");
                        }
                        //if (dispatch1 > 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Amount Added Successfully... !!! Please  Click on Approve to Approve Attendance!! ');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Added Failed... !!!');", true);
                        //}


                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Amount Added Successfully... !!! Please  Click on Approve to Approve Material!! ');", true);
                    }
                    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Enter Amount to Material Does not insert 0 Value!! ');", true); }
                }
                catch (Exception ex) { throw ex; }
                finally { }
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('For This Client Fix Contract Material Policy  Not Created !!!');", true); }
        
        
        
        
        }

    }
    protected void ddl_branch_material_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {
            d.con.Open();
            

            string unit_code = "";
            string approve_material = "";

            if (ddl_branch_material.SelectedValue == "ALL")
            {
                unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
                unit_code = "'" + unit_code + "'";
                unit_code = unit_code.Replace(",", "','");
            }
            else
            {
                Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }

          // string material_amount_type = d.getsinglestring("select distinct `material_amount_type` from pay_material_details where comp_code= '" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client_material.SelectedValue + "' and IN (" + unit_code + ") ");

           


            if (ddl_material_amount_type.SelectedValue == "0")
            {
                HiddenField1.Value = "0";

                gv_branchwise_material.DataSource = null;
                gv_branchwise_material.DataBind();

                // komal comment 

                string material_amount_type = d.getsinglestring("select distinct `material_amount_type` from pay_material_details where comp_code= '" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client_material.SelectedValue + "'and state_name='" + ddl_state_material.SelectedValue + "' and unit_code IN (" + unit_code + ") AND month = '" + txt_month_material.Text.Substring(0, 2) + "' AND year = '" + txt_month_material.Text.Substring(3) + "' ");

                if (material_amount_type!="")
                {
                if (material_amount_type != ddl_material_amount_type.SelectedValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This record all ready added by Branchwise !!');", true);
                    return;

                }
                }

                string approve_material1 = "";

            if(ddl_employee_type1.SelectedValue=="0")
            {
            approve_material = d.getsinglestring("select  material_flag from pay_material_details  INNER JOIN pay_employee_master ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.emp_code = pay_employee_master.emp_code where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "' and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");
            }
            else if (ddl_employee_type1.SelectedValue == "1") 
            {
                approve_material = d.getsinglestring("select  material_flag from pay_material_details  INNER JOIN pay_employee_master ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.emp_code = pay_employee_master.emp_code where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "' and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");
            }


            if (ddl_branch_material.SelectedValue == "ALL")
            {
                if (ddl_employee_type1.SelectedValue == "0")
                {
                    approve_material1 = d.getsinglestring("select  GROUP_CONCAT(material_flag) from pay_material_details  INNER JOIN pay_employee_master ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.emp_code = pay_employee_master.emp_code where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "' and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");
                }
                else
                    if (ddl_employee_type1.SelectedValue == "1")
                    {
                        approve_material1 = d.getsinglestring("select  GROUP_CONCAT(material_flag) from pay_material_details  INNER JOIN pay_employee_master ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.emp_code = pay_employee_master.emp_code where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "' and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");
                    }

                string[] flag = approve_material1.Split(',');
                foreach (object obc in flag)
                {

                    if (obc.Equals("1"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved by Admin you can not make changes !!');", true);
                        return;
                    }
                    if (obc.Equals("2"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved by Finance you can not make changes !!');", true);
                        return;
                    }
                }
            }


            if (approve_material == "")
            {
                approve_material = "0";
            }
            MySqlDataAdapter adp1 = null;
            if (approve_material == "0" || approve_material == "3")
            {
                //  MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT unit_name,unit_code,'' as 'LEFT_DATE' FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client_material.SelectedValue + "' AND STATE_NAME = '" + ddl_state_material.SelectedValue + "'", d.con1);
                hidden_month.Value = txt_month_material.Text.Substring(0, 2);
                hidden_year.Value = txt_month_material.Text.Substring(3);
        
                string where = "";
                string start_date_common = get_start_date_material();
                string end_date_common = get_end_date_material();
                int month = int.Parse(hidden_month.Value);
                int year = int.Parse(hidden_year.Value);
                string lef = "";
                if (ddl_employee_type1.SelectedValue == "0")
                {
                    lef = " or left_date is null";
                }
                if (start_date_common != "" && start_date_common != "1")
                {
                    month = --month;
                    if (month == 0) { month = 12; year = --year; }
                    where = " and ((left_date >= str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y')) "+lef+") and joining_date <=  str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }
                else
                {
                    start_date_common = "1";
                    where = " and ((left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') ) "+lef+") and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }
                if (ddl_employee_type1.SelectedValue=="0")
                {
                    adp1 = new MySqlDataAdapter("SELECT DISTINCT unit_name,pay_unit_master.unit_code, pay_employee_master.emp_code, emp_name, ifnull(material_amount,0) AS 'LEFT_DATE',ifnull(material_deduction,0) AS 'LEFT_DATE1' FROM pay_employee_master INNER JOIN pay_unit_master ON  pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.client_code = pay_unit_master.client_code AND pay_employee_master.unit_code = pay_unit_master.unit_code LEFT JOIN pay_material_details ON pay_employee_master.emp_code = pay_material_details.emp_code AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client_material.SelectedValue + "'   AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "' " + where + " AND pay_unit_master.unit_code IN(" + unit_code + ") GROUP BY emp_code", d.con1);
                }
                else
                    if (ddl_employee_type1.SelectedValue == "1")
                    {
                        adp1 = new MySqlDataAdapter("SELECT DISTINCT unit_name,pay_unit_master.unit_code, pay_employee_master.emp_code, emp_name, ifnull(material_amount,0) AS 'LEFT_DATE',ifnull(material_deduction,0) AS 'LEFT_DATE1' FROM pay_employee_master INNER JOIN pay_unit_master ON  pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.client_code = pay_unit_master.client_code AND pay_employee_master.unit_code = pay_unit_master.unit_code LEFT JOIN pay_material_details ON pay_employee_master.emp_code = pay_material_details.emp_code AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client_material.SelectedValue + "'   AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "' " + where + " AND pay_unit_master.unit_code IN(" + unit_code + ") GROUP BY emp_code", d.con1);
                    }

                btn_save_material.Visible = true;
                btn_approve_material.Visible = true;
                btn_material_link.Visible = true;
            }
            else if (approve_material == "1" || approve_material == "2")
            {

                btn_save_material.Visible = false;
                btn_approve_material.Visible = false;
                btn_material_link.Visible = false;
                Panel11.Visible = false;
                material_approve_upload();
                Panel14.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Approved by Admin you can not make changes !!');", true);
               return;
            }
            Panel14.Visible = true;
            Panel11.Visible = true;
            DataSet ds = new DataSet();
            adp1.Fill(ds);

            grid_material_amount.DataSource = ds.Tables[0];
            grid_material_amount.DataBind();

               
            }

            if (ddl_material_amount_type.SelectedValue=="1")
            {

                HiddenField1.Value = "1";

                grid_material_amount.DataSource = null;
                grid_material_amount.DataBind();

                // komal comment 18-05-2020

                string material_amount_type = d.getsinglestring("select distinct `material_amount_type` from pay_material_details where comp_code= '" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client_material.SelectedValue + "' and state_name='" + ddl_state_material.SelectedValue + "' and unit_code IN (" + unit_code + ") AND month = '" + txt_month_material.Text.Substring(0, 2) + "' AND year = '" + txt_month_material.Text.Substring(3) + "' ");

                if (material_amount_type != "")
                {
                    if (material_amount_type != ddl_material_amount_type.SelectedValue)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This record all ready added by Employeewise !!');", true);
                        return;

                    }

                }
            string approve_material1 = "";

            approve_material = d.getsinglestring("select  material_flag from pay_material_details  where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "' and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");

              

                if (ddl_branch_material.SelectedValue == "ALL")
                {
                        approve_material1 = d.getsinglestring("select  GROUP_CONCAT(material_flag) from pay_material_details  where pay_material_details.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_material_details.client_code = '" + ddl_client_material.SelectedValue + "'  AND state_name = '" + ddl_state_material.SelectedValue + "'  and pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "'  and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  AND pay_material_details.unit_code IN (" + unit_code + ")");
                    
                    string[] flag = approve_material1.Split(',');
                    foreach (object obc in flag)
                    {

                        if (obc.Equals("1"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved by Admin you can not make changes !!');", true);
                            return;
                        }
                        if (obc.Equals("2"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved by Finance you can not make changes !!');", true);
                            return;
                        }
                    }
                }


                if (approve_material == "")
                {
                    approve_material = "0";
                }
                MySqlDataAdapter adp1 = null;
                if (approve_material == "0" || approve_material == "3")
                {
                    //  MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT unit_name,unit_code,'' as 'LEFT_DATE' FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client_material.SelectedValue + "' AND STATE_NAME = '" + ddl_state_material.SelectedValue + "'", d.con1);

                  
                        //adp1 = new MySqlDataAdapter("SELECT DISTINCT unit_name,pay_unit_master.unit_code ifnull(material_amount,0) AS 'LEFT_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON  pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.client_code = pay_unit_master.client_code AND pay_employee_master.unit_code = pay_unit_master.unit_code LEFT JOIN pay_material_details ON pay_employee_master.emp_code = pay_material_details.emp_code AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client_material.SelectedValue + "' AND LOCATION = '" + ddl_state_material.SelectedValue + "'  AND pay_employee_master.unit_code IN(" + unit_code + ") ", d.con1);

                    adp1 = new MySqlDataAdapter(" select unit_name,pay_unit_master.unit_code, IFNULL(`material_amount`, 0) AS 'LEFT_DATE' from pay_unit_master LEFT JOIN `pay_material_details` ON `pay_unit_master`.`unit_code` = `pay_material_details`.`unit_code`AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' and pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "' where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client_material.SelectedValue + "' and pay_unit_master.`STATE_NAME` ='" + ddl_state_material.SelectedValue + "' and pay_unit_master.`unit_code` IN (" + unit_code + ") ", d.con1);

                    btn_save_material.Visible = true;
                    btn_approve_material.Visible = true;
                    btn_material_link.Visible = true;
                }
                else if (approve_material == "1" || approve_material == "2")
                {

                    btn_save_material.Visible = false;
                    btn_approve_material.Visible = false;
                    btn_material_link.Visible = false;
                    Panel11.Visible = false;


                  material_approve_upload();
                    
                    
                    Panel14.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Location Approved by Admin you can not make changes !!');", true);
                    return;
                }
                Panel14.Visible = true;
                Panel11.Visible = true;
                DataSet ds = new DataSet();
                adp1.Fill(ds);

                gv_branchwise_material.DataSource = ds.Tables[0];
                gv_branchwise_material.DataBind();

            
            }
           
            
            

            material_location();
            //if (d.getsinglestring("select unit_code from pay_material_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and   month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and material_flag = 0 ") == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Attendance Sheet  !!');", true);
            //    return;
            //}

            if (d.getsinglestring("select state_name,client_code,unit_code,month,year,emp_code  from pay_material_details where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and state_name= '" + ddl_state_material.SelectedValue + "' and unit_code in("+unit_code+") and month = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3) + "' ") == "") 

            {
                btn_save_material.Visible = true;

              //btn_save_material.Visible = false;
                btn_approve_material.Visible = true;
                btn_material_link.Visible = true;
                Panel11.Visible = true;
                Panel14.Visible = true;
                //btn_update_material.Visible = false;
            }
            else{
               // btn_save_material.Visible = false;
            //    btn_update_material.Visible = true;
            
            
            }
          
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
 
    //protected void btn_update_material_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow row in grid_material_amount.Rows)
    //        {
    //            string emp_code = row.Cells[4].Text;

    //            TextBox txt_returnqty = (TextBox)row.FindControl("txt_material_amount");
    //            string amount_material = (txt_returnqty.Text);

    //            int dispatch2 = d.operation("update pay_material_details set material_amount = '" + amount_material + "' where emp_code = '" + emp_code + "' and month = '" + txt_month_material.Text.Substring(0,2) + "' and year = '" + txt_month_material.Text.Substring(3) + "' and comp_code = '" + Session["comp_code"].ToString() + "'");

    //            if (dispatch2 > 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Amount Updated Successfully... !!!');", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('  Material Amount UpdatedFailed.. !!!');", true);
    //            }


    //        }
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { }


    //}

    protected void btn_material_upload_Click(object sender, EventArgs e)
    {

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        if (document_file_material.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(document_file_material.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(document_file_material.PostedFile.FileName);
                document_file_material.PostedFile.SaveAs(Server.MapPath("~/material_images/") + fileName);
                string id = d.getsinglestring("select ifnull(max(id),0) from pay_material_details");

                string file_name = GenerateFileName(ddl_state_material.SelectedValue);
                string new_file_name = ddl_client_material.SelectedValue + "_" + file_name.Substring(0, 10) + id + fileExt;

                File.Copy(Server.MapPath("~/material_images/") + fileName, Server.MapPath("~/material_images/") + new_file_name, true);
                File.Delete(Server.MapPath("~/material_images/") + fileName);

                //  string unit_code = "";

                //if (ddl_branch_material.SelectedValue == "ALL")
                //{
                //    unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' AND branch_status = '0'");
                //    unit_code = "'" + unit_code + "'";
                //    unit_code = unit_code.Replace(",", "','");
                //}
                //else
                //{
                //    Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                //    //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                //    unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
                //}

                //string[] abc = unit_code.Split(',');

                if (ddl_material_amount_type.SelectedValue=="0")
                {
                foreach (GridViewRow row in grid_material_amount.Rows)
                //foreach (object obj in abc)
                {
                    string emp_code = row.Cells[4].Text;
                    string ddl_unitcode = row.Cells[2].Text;

                    //d.operation("delete from pay_material_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode + "' and client_code = '" + ddl_client_material.SelectedValue + "' and emp_code = '" + emp_code + "' and month  = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3, 4) + "'");

                    if(ddl_material_amount_type.SelectedValue=="0")
                    {

                    d.operation("update pay_material_details set material_upload ='" + new_file_name + "', description='" + des_material.Text + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and unit_code = '" + ddl_unitcode + "' and month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and state_name = '" + ddl_state_material.SelectedValue + "'  ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Sheet Upload Succsefully ..!!');", true);
                 
                    }
                    else if (ddl_material_amount_type.SelectedValue == "1") 
                    {
                        d.operation("update pay_material_details set material_upload ='" + new_file_name + "', description='" + des_material.Text + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and unit_code = '" + ddl_unitcode + "' and month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and state_name = '" + ddl_state_material.SelectedValue + "' ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Sheet Upload Succsefully ..!!');", true);
                 
                    
                    }

                }

                }
                else if (ddl_material_amount_type.SelectedValue == "1")
                {
                    HiddenField1.Value = "1";

                    foreach (GridViewRow row in gv_branchwise_material.Rows)
                    //foreach (object obj in abc)
                    {
                       // string emp_code = row.Cells[4].Text;
                        string ddl_unitcode = row.Cells[2].Text;

                        //d.operation("delete from pay_material_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode + "' and client_code = '" + ddl_client_material.SelectedValue + "' and emp_code = '" + emp_code + "' and month  = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3, 4) + "'");

                       
                            d.operation("update pay_material_details set material_upload ='" + new_file_name + "', description='" + des_material.Text + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and unit_code = '" + ddl_unitcode + "' and month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and state_name = '" + ddl_state_material.SelectedValue + "' ");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Sheet Upload Succsefully ..!!');", true);

                        

                    }

                }
                material_location();


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select JPG, PNG and PDF file.');", true);
                return;
            }
        }
        else { 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
            return;
        }


    }
    protected void btn_approve_material_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        string unit_code = "";

        if (ddl_branch_material.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }

        //for employeewise  komal 16-05-2020 
        if (ddl_material_amount_type.SelectedValue=="0")
        {

        foreach (GridViewRow row in grid_material_amount.Rows)
        //foreach (object obj in abc)
        {
            string emp_code = row.Cells[4].Text;
            //   string unit_code1 = row.Cells[2].Text;
            TextBox txt_returnqty = (TextBox)row.FindControl("txt_material_amount");
            string amount_material = (txt_returnqty.Text);

            if (amount_material != "" && amount_material != "0")
            {
                HiddenField1.Value = "0";

                if (d.getsinglestring("SELECT emp_code FROM pay_material_details WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND client_code = '" + ddl_client_material.SelectedValue + "' AND unit_code IN(" + unit_code + ") and emp_code IN( '" + emp_code + "') AND state_name = '" + ddl_state_material.SelectedValue + "'  AND month = '" + txt_month_material.Text.Substring(0, 2) + "' AND year = '" + txt_month_material.Text.Substring(3) + "'") == "")
                {// d.getsinglestring(" ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Save Material Amount  !!');", true);
                    return;
                }
            }
        }
        }
        else
            //for branchwise  komal 16-05-2020 
            if (ddl_material_amount_type.SelectedValue == "1")
            {

                HiddenField1.Value = "1";

                foreach (GridViewRow row in gv_branchwise_material.Rows)
                //foreach (object obj in abc)
                {
                  //  string emp_code = row.Cells[4].Text;
                    //   string unit_code1 = row.Cells[2].Text;
                    TextBox txt_returnqty = (TextBox)row.FindControl("txt_material_amount");
                    string amount_material = (txt_returnqty.Text);

                    if (amount_material != "" && amount_material != "0")
                    {
                        HiddenField1.Value = "1";
                        if (d.getsinglestring("SELECT unit_code FROM pay_material_details WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND client_code = '" + ddl_client_material.SelectedValue + "' AND unit_code IN(" + unit_code + ")  AND state_name = '" + ddl_state_material.SelectedValue + "'  AND month = '" + txt_month_material.Text.Substring(0, 2) + "' AND year = '" + txt_month_material.Text.Substring(3) + "'") == "")
                        {// d.getsinglestring(" ");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Save Material Amount  !!');", true);
                            return;
                        }
                    }
                }
            }





        if (d.getsinglestring("select Distinct material_upload from pay_material_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and   month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and material_flag in (0,3) ") == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Material Sheet  !!');", true);
            return;
        }




        string check = d.getsinglestring("select material_flag from pay_material_details where comp_code ='" + Session["COMP_CODE"].ToString() + "' AND unit_code IN(" + unit_code + ") and month = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3) + "'  AND ( material_flag  = '1' || material_flag = '2') ");
         if (check == "1" || check == "2")
        {   // attendance_status();
            // txt_document1.Text = "";

            if (ddl_material_amount_type.SelectedValue == "0")
            {
                HiddenField1.Value = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Already Approved !!');", true);
                return;
            }else
                if (ddl_material_amount_type.SelectedValue == "1")
                {
                    HiddenField1.Value = "1";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Already Approved !!');", true);
                    return;
                }

        }
   
        //if (d.getsinglestring("select unit_code from pay_material_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client_material.SelectedValue + "' and   month='" + txt_month_material.Text.Substring(0, 2) + "' and year='" + txt_month_material.Text.Substring(3) + "' and unit_code IN(" + unit_code + ") and material_flag = 0 ") == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Attendance Sheet  !!');", true);
        //    return;
        //}
         if (ddl_material_amount_type.SelectedValue == "0")
         {

             foreach (GridViewRow row in grid_material_amount.Rows)
             //foreach (object obj in abc)
             {
                 string emp_code = row.Cells[4].Text;
                 string unit_code1 = row.Cells[2].Text;


                 d.operation("Update pay_material_details set material_flag = '1' ,material_status = 'Approve By Admin',rejected_reason = '' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code ='" + ddl_client_material.SelectedValue + "' and state_name = '" + ddl_state_material.SelectedValue + "' and month = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3) + "'  and unit_code = '" + unit_code1 + "' and material_amount_type='0' ");

             }
         }
         else
             if (ddl_material_amount_type.SelectedValue == "1")
             {

                 foreach (GridViewRow row in gv_branchwise_material.Rows)
                 //foreach (object obj in abc)
                 {
                   //  string emp_code = row.Cells[4].Text;
                     string unit_code1 = row.Cells[2].Text;


                     d.operation("Update pay_material_details set material_flag = '1' ,material_status = 'Approve By Admin',rejected_reason = '' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code ='" + ddl_client_material.SelectedValue + "' and state_name = '" + ddl_state_material.SelectedValue + "' and month = '" + txt_month_material.Text.Substring(0, 2) + "' and year = '" + txt_month_material.Text.Substring(3) + "' and material_amount_type='1' and unit_code = '" + unit_code1 + "' ");

                 }
             }

        btn_save_material.Visible = false;
        //grid_material_amount.Visible = false;
        if (ddl_material_amount_type.SelectedValue == "0")
        {
            grid_material_amount.DataSource = null;
            grid_material_amount.DataBind();
        }else
            if (ddl_material_amount_type.SelectedValue == "1")
            {
                gv_branchwise_material.DataSource = null;
                gv_branchwise_material.DataBind();
            }
        btn_material_link.Visible = false;
        Panel11.Visible = false;
        Panel14.Visible = false;
        btn_approve_material.Visible = false;

        if (ddl_material_amount_type.SelectedValue == "0")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Approved Successfully!!');", true);
            return;
        }else
            if (ddl_material_amount_type.SelectedValue == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved Successfully!!');", true);
                return;
            }
    }

    protected void btn_approve_conveyance_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        foreach (GridViewRow row in grd_convayance.Rows)
        //foreach (object obj in abc)
        {
            string emp_code = row.Cells[1].Text;
            //   string unit_code1 = row.Cells[2].Text;
            TextBox txt_returnqty = (TextBox)row.FindControl("txt_conveyance_amount");
            string amount_material = (txt_returnqty.Text);

            if (amount_material != "" && amount_material != "0")
            {

                if (d.getsinglestring("SELECT emp_code FROM pay_conveyance_amount_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND client_code = '" + con_ddl_client.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "' and emp_code IN( '" + emp_code + "') AND state = '" + con_ddl_state.SelectedValue + "'  AND month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' AND year = '" + txt_date_conveyance.Text.Substring(3) + "' AND employee_type = '" + ddl_employee_type.SelectedValue + "' and conveyance='emp_conveyance'") == "")
                {// d.getsinglestring(" ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Save Coneyance  !!');", true);
                    return;
                }
            }
        }

        if (d.getsinglestring("select Distinct conveyance_images from pay_conveyance_upload where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code ='" + con_ddl_client.SelectedValue + "' and   month='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year='" + txt_date_conveyance.Text.Substring(3) + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and state = '" + con_ddl_state.SelectedValue + "'") == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Conveyance Sheet  !!');", true);
            return;
        }



        string check = d.getsinglestring("select conveyance_flag from pay_conveyance_amount_history where comp_code='" + Session["COMP_CODE"].ToString() + "'and client_code = '" + con_ddl_client.SelectedValue + "'and state = '" + con_ddl_state.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "' and emp_code = '" + ddl_employee.SelectedValue + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'  AND ( conveyance_flag  = '1' || conveyance_flag='2')  AND  conveyance_rate  != 0 and conveyance='emp_conveyance' ");

        if (check == "1" || check == "2")
        {
            // attendance_status();
            // txt_document1.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('  Coneyance Already Approved !!');", true);
            return;

        }

        //if (d.getsinglestring("select conveyance_flag from pay_conveyance_amount_history where comp_code='" + Session["COMP_CODE"].ToString() + "'and client_code = '" + con_ddl_client.SelectedValue + "'and state = '" + con_ddl_state.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "'  and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'") == "1")
        //{
        //    // attendance_status();
        //    // txt_document1.Text = "";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Coneyance Already Approved !!');", true);
        //    return;

        //}
   

        foreach (GridViewRow row in grd_convayance.Rows)
        //foreach (object obj in abc)
        {
            string emp_code = row.Cells[1].Text;
         //   string unit_code1 = row.Cells[2].Text;


            d.operation("Update pay_conveyance_amount_history set conveyance_flag = '1',con_emp_status = 'Approve By Admin',con_emp_rejected_reason = '' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code ='" + con_ddl_client.SelectedValue + "' and state = '" + con_ddl_state.SelectedValue + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and emp_code = '" + emp_code + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and conveyance='emp_conveyance' ");
        
        }
        btn_conv_save.Visible = false;
        grd_convayance.Visible = false;
        btn_approve_conveyance.Visible = false;
        btn_conv_link.Visible = false;
        grd_convayance_location.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Approved Successfully!!');", true);
        return;
    


    }


    protected void report_conveyance()
    {
        try {
        
        
        
        
        
        
        }
        catch (Exception ex) { throw ex; }
        finally { }
    
    
    
    
    }
    protected void gridview_conv_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
    }
    protected void btn_conv_link_Click(object sender, EventArgs e)
    {
        HiddenField2.Value = "0";
        conveyance_status();
    }

    protected void btn_material_link_Click(object sender, EventArgs e)
    {
        Material_status();
    }

    protected void btn_drive_approve_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

         try {
        if (d.getsinglestring("SELECT emp_code FROM pay_conveyance_amount_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND  client_code  = '" + con_ddl_client.SelectedValue + "' AND  unit_code  = '" + con_ddl_unitcode.SelectedValue + "' and emp_code = '" + ddl_employee .SelectedValue+ "' AND  state  = '" + con_ddl_state.SelectedValue + "'  AND  month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' AND  year  = '" + txt_date_conveyance.Text.Substring(3) + "' AND  employee_type  = '" + ddl_employee_type.SelectedValue + "' and conveyance='driver_conveyance'") == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Save Driver Conveyance  !!');", true);
            return;
        }

        //if (d.getsinglestring("select Distinct conveyance_images from pay_conveyance_upload where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code ='" + con_ddl_client.SelectedValue + "' and   month='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year='" + txt_date_conveyance.Text.Substring(3) + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and state = '" + con_ddl_state.SelectedValue + "'") == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Upload Driver Conveyance Sheet  !!');", true);
        //    return;
        //}

        if (d.getsinglestring("select comp_code from pay_conveyance_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and client_code = '" + con_ddl_client.SelectedValue + "' and emp_code = '" + ddl_employee.SelectedValue + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'").Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload conveyance file !!');", true);
            return;
        }





        string check = d.getsinglestring("select driver_conv_flag from pay_conveyance_amount_history where comp_code='" + Session["COMP_CODE"].ToString() + "'and client_code = '" + con_ddl_client.SelectedValue + "'and state = '" + con_ddl_state.SelectedValue + "' AND unit_code = '" + con_ddl_unitcode.SelectedValue + "' and emp_code = '" + ddl_employee.SelectedValue + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'  AND ( driver_conv_flag  = '1' || driver_conv_flag='2')  AND   conveyance='driver_conveyance' ");

        if (check=="1"||check=="2")
        {
            // attendance_status();
            // txt_document1.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Driver Coneyance Already Approved !!');", true);
            return;

        }


        //foreach (GridViewRow row in grd_convayance.Rows)
        ////foreach (object obj in abc)
        //{
        //    string emp_code = row.Cells[1].Text;
        //    //   string unit_code1 = row.Cells[2].Text;


        d.operation("Update pay_conveyance_amount_history set driver_conv_flag = '1',con_driver_status = 'Approve By Admin',con_driver_rejected_reason = '' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code ='" + con_ddl_client.SelectedValue + "' and state = '" + con_ddl_state.SelectedValue + "' and month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' and emp_code = '" + ddl_employee.SelectedValue + "' and unit_code = '" + con_ddl_unitcode.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and conveyance='driver_conveyance' ");

            btn_drive_save.Visible = false;
            btn_drive_approve.Visible = false;
            Panel_driver_conv.Visible = false;
            btn_report.Visible = false;
            Panel12.Visible = false;
            driver_text_clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Driver Conveyance Approved Successfully!!');", true);
            return;
        }
        catch (Exception ex) { throw ex; }
        finally { }


    }
    protected void grid_material_amount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[4].Visible = false;

    }


    protected void material_location() 
     {
         try
         {
             if (ddl_material_amount_type.SelectedValue=="0")
             {

             gv_material_location.Visible = true;
             }
             //gv_bill_list_upload.Visible = true;
             System.Data.DataTable dt = new System.Data.DataTable();

             //apporve_attendace gridview
             gv_material_location.DataSource = null;
             gv_material_location.DataBind();
             //gv_bill_list_upload.DataSource = null;
             //gv_bill_list_upload.DataBind();

             string unit_code = "";

             if (ddl_branch_material.SelectedValue == "ALL")
             {
                 unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
                 unit_code = "'" + unit_code + "'";
                 unit_code = unit_code.Replace(",", "','");
             }
             else
             {
                 Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                 //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                 unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
             }

             string unit_name = ddl_branch_material.SelectedItem.Text;

             d4.con.Open();

             //employeewise komal 15-05-2020
             
           string emp_code1 = "";
           MySqlCommand cmd = null;
             // employeewise material komal 15-05-2020
           if (ddl_material_amount_type.SelectedValue == "0")
           {
           foreach (GridViewRow gvrow in grid_material_amount.Rows)
           {
               emp_code1 = grid_material_amount.Rows[gvrow.RowIndex].Cells[4].Text;
               emp_code1 = "'" + emp_code1 + "'";
               emp_code1 = emp_code1.Replace(",", "','");
           }
           if (emp_code1 != "")
           {
             cmd = new MySqlCommand(" SELECT distinct  pay_client_master.client_name, pay_unit_master.state_name, '" + unit_name + "' as 'unit_name', CAST(CONCAT(pay_material_details.month, '/', pay_material_details.year) AS char) AS 'month', pay_material_details.material_upload FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_code AND pay_material_details.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' and emp_code in(" + emp_code1 + ") AND pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.UNIT_CODE IN ( " + unit_code + " ) AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' AND pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'", d.con);
           }
           else
           {
               cmd = new MySqlCommand(" SELECT distinct  pay_client_master.client_name, pay_unit_master.state_name, '" + unit_name + "' as 'unit_name', CAST(CONCAT(pay_material_details.month, '/', pay_material_details.year) AS char) AS 'month', pay_material_details.material_upload FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_code AND pay_material_details.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.UNIT_CODE IN ( " + unit_code + " ) AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' AND pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'", d.con);
           }

           }

                  // branchwise material komal 15-05-2020
           else if (ddl_material_amount_type.SelectedValue == "1") 
           {
             //  cmd = new MySqlCommand(" SELECT distinct  pay_client_master.client_name, pay_unit_master.state_name, '" + unit_name + "' as 'unit_name', CAST(CONCAT(pay_material_details.month, '/', pay_material_details.year) AS char) AS 'month', pay_material_details.material_upload FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_code AND pay_material_details.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.UNIT_CODE IN ( " + unit_code + " ) AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' AND pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'  group by 'UNIT_CODE' ", d.con);
               cmd = new MySqlCommand(" SELECT distinct  pay_client_master.client_name, pay_unit_master.state_name,  pay_unit_master.unit_name, CAST(CONCAT(pay_material_details.month, '/', pay_material_details.year) AS char) AS 'month', pay_material_details.material_upload FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_code AND pay_material_details.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.UNIT_CODE IN ( " + unit_code + " ) AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' AND pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "' ", d.con);
           
           }



             MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
             dt_item.Fill(dt);
             if (dt.Rows.Count > 0)
             {
                 if (ddl_material_amount_type.SelectedValue=="0")
                 {
                 gv_material_location.DataSource = dt;
                 gv_material_location.DataBind();
                 //gv_approve_panel.Visible = true;
                 gv_material_location.Visible = true;

                 Panel_materil.Visible = true;

                 }
                 else
                     if (ddl_material_amount_type.SelectedValue == "1")
                     {
                         gv_locationwise_material_upload.DataSource = dt;
                         gv_locationwise_material_upload.DataBind();
                         //gv_approve_panel.Visible = true;
                         gv_locationwise_material_upload.Visible = true;

                         Panel_upload_branchwise.Visible = true;

                     }
             }
             d4.con.Close();

         }
         catch (Exception ex)
         {
             throw ex;
         }
         finally
         {
             d.con.Close(); d.con1.Close(); d4.con.Close();
         }
   
    }



    protected void material_approve_upload()
    {

        try {
            d.con.Open();
            string unit_code = "";

            if (ddl_branch_material.SelectedValue == "ALL")
            {
                unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_material.SelectedValue + "' and STATE_NAME = '" + ddl_state_material.SelectedValue + "' and branch_status = '0' ");
                unit_code = "'" + unit_code + "'";
                unit_code = unit_code.Replace(",", "','");
            }
            else
            {
                Session["UNIT_CODE"] = ddl_branch_material.SelectedValue;
                //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
            }


            gv_material_location.Visible = true;

            string unit_name = ddl_branch_material.SelectedItem.Text;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd3 = new MySqlCommand(" SELECT distinct  pay_client_master.client_name, pay_unit_master.state_name, '" + unit_name + "' as 'unit_name', CAST(CONCAT(pay_material_details.month, '/', pay_material_details.year) AS char) AS 'month', pay_material_details.material_upload FROM pay_material_details INNER JOIN pay_client_master ON pay_material_details.comp_code = pay_client_master.comp_code AND pay_material_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_material_details.unit_code = pay_unit_master.unit_code AND pay_material_details.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_material_details.material_flag ='1' AND pay_unit_master.client_code = '" + ddl_client_material.SelectedValue + "' AND  pay_unit_master.UNIT_CODE IN ( " + unit_code + " ) AND pay_unit_master.state_name = '" + ddl_state_material.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_material.Text.Substring(0, 2) + "' AND pay_material_details.year = '" + txt_month_material.Text.Substring(3) + "'", d.con);

            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd3);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gv_material_location.DataSource = dt;
                gv_material_location.DataBind();
                //gv_approve_panel.Visible = true;
            }
            d.con.Close();
        
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    
    }




    protected void lnk_material_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Downloaded !!!')", true);
        }
    }
    protected void lnk_material_Command1(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile_material(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }

    }




    protected void btn_report_update_Click(object sender, EventArgs e)
    {
       // upload_emp(int days);

        upload_emp();
    }



    private void upload_emp()
    {

        try
        {

            

            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);


            string fname = null;

            string FilePath = "";
            if (FileUpload1.HasFile)
            {

                try
                {
                    if (fileExt.ToUpper() == ".XLS" || fileExt.ToUpper() == ".XLSX")
                    {
                        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Downloads/") + FileName);
                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        if (Extension == ".xls" || Extension == ".xlsx")
                        {

                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                         
                            fname = ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + fileExt;
                            //fname = ddlunitclient1.SelectedValue + "_" + ddl_gv_statewise.SelectedValue + "_" + ddl_gv_branchwise.SelectedValue + "_" + fileExt;
                            File.Copy(Server.MapPath("~/Downloads/") + FileName, Server.MapPath("~/Downloads/") + fname, true);

                            string FolderPath = "~/Downloads/";
                            FilePath = Server.MapPath(FolderPath + fname);
                            //FileUpload1.SaveAs(FilePath);
                            btn_Import_Click(FilePath, Extension, "Yes", FileName, fname);

                            btn_process_Click(null,null);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Report file Uploaded Successfully...');", true);
                            // File.Delete(FilePath);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload a valid excel file.');", true);
                        }
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('System Error - Please Try again....');", true);
                }
                finally
                {
                    File.Delete(FilePath);
                }
            }


        }
        catch (Exception ex) { throw ex; }
        finally { }

    }


    public void btn_Import_Click(string FilePath, string Extension, string IsHDR, string filename, string fname)
    {
        
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        //   OleDbCommand cmdExcel1 = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        // OleDbDataAdapter oda1 = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        //System.Data.DataTable dt1 = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;
        //cmdExcel1.Connection = connExcel;

        // Get The Name of First Sheet
        connExcel.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

       // string sheetName =  ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + ddl_state.SelectedValue + "_" + ".xlsx";
        connExcel.Close();

        //Read Data from First Sheet

        connExcel.Open();
        cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);

        connExcel.Close();


        foreach (DataRow r in dt.Rows)
        {

            if(!r[0].ToString().Trim().ToUpper().Contains("SL"))
            {
            if (r[0].ToString().Trim() != "" && r[1].ToString().Trim() != "" && r[2].ToString().Trim() != "" && r[3].ToString().Trim() != "" && r[4].ToString().Trim() != "" && r[5].ToString().Trim() != "" && r[6].ToString().Trim() != "" && r[7].ToString().Trim() != "" && r[8].ToString().Trim() != "" && r[9].ToString().Trim() != "" && r[10].ToString().Trim() != "" && r[11].ToString().Trim() != "" && r[12].ToString().Trim() != "" && r[13].ToString().Trim() != "" && r[14].ToString().Trim() != "" && r[15].ToString().Trim() != "" && r[16].ToString().Trim() != "" && r[17].ToString().Trim() != "" && r[18].ToString().Trim() != "" && r[19].ToString().Trim() != "" && r[20].ToString().Trim() != "" && r[21].ToString().Trim() != "" && r[22].ToString().Trim() != "" && r[23].ToString().Trim() != "" && r[24].ToString().Trim() != "" && r[25].ToString().Trim() != "" && r[26].ToString().Trim() != "" && r[27].ToString().Trim() != "" && r[28].ToString().Trim() != "" && r[29].ToString().Trim() != "" && r[30].ToString().Trim() != "" && r[31].ToString().Trim() != "")
            {
                string unit_code = null;
                if (ddl_unitcode.SelectedValue == "ALL")
                {
                    
                    unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' AND  branch_status  = '0'");
                    unit_code = "'" + unit_code + "'";
                    unit_code = unit_code.Replace(",", "','");
                }
                else
                {
                    Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
                    //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                    unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
                }


                string emp_code = ""+r[1].ToString().Trim()+"";

                string tot_working_days = d.getsinglestring("select distinct `TOT_WORKING_DAYS` from pay_attendance_muster where comp_code = '"+Session["comp_code"].ToString()+"' and unit_code in("+ unit_code +") and emp_code = '"+ emp_code +"' and month = '" + txttodate.Text.Substring(0, 2) + "' and year = '" + txttodate.Text.Substring(3) + "' ");


                          string header = "";
                          string header1 = "";

                         // header = "DAY01 = '" + r[8].ToString().Trim() + "',DAY02='" + r[9].ToString().Trim() + "',DAY03 = '" + r[10].ToString().Trim() + "',DAY04='" + r[11].ToString().Trim() + "',DAY05='" + r[12].ToString().Trim() + "',DAY06='" + r[13].ToString().Trim() + "',DAY07='" + r[14].ToString().Trim() + "',DAY08='" + r[15].ToString().Trim() + "',DAY09='" + r[16].ToString().Trim() + "',DAY10='" + r[17].ToString().Trim() + "',DAY11='" + r[18].ToString().Trim() + "',DAY12='" + r[19].ToString().Trim() + "',DAY13='" + r[20].ToString().Trim() + "',DAY14='" + r[21].ToString().Trim() + "',DAY15='" + r[22].ToString().Trim() + "',DAY16='" + r[23].ToString().Trim() + "',DAY17='" + r[24].ToString().Trim() + "',DAY18='" + r[25].ToString().Trim() + "',DAY19='" + r[26].ToString().Trim() + "',DAY20='" + r[27].ToString().Trim() + "',DAY21='" + r[28].ToString().Trim() + "',DAY22='" + r[29].ToString().Trim() + "',DAY23='" + r[30].ToString().Trim() + "',DAY24='" + r[31].ToString().Trim() + "',DAY25='" + r[32].ToString().Trim() + "' ,DAY26='" + r[2].ToString().Trim() + "',DAY27='" + r[3].ToString().Trim() + "',DAY28='" + r[4].ToString().Trim() + "'";

                          if (tot_working_days == "28")
                          {
                              header1 = "";
                          }
                
                if(tot_working_days=="29")
                      {
                          header1 =  ",DAY29='" + r[5].ToString().Trim() + "'";
                        }


                          if (tot_working_days == "30")
                          {
                              header1 =  ",DAY29='" + r[5].ToString().Trim() + "',DAY30='" + r[6].ToString().Trim() + "'";
                          }


                          if (tot_working_days == "31")
                          {
                              header1 =  ",DAY29='" + r[5].ToString().Trim() + "',DAY30='" + r[6].ToString().Trim() + "', DAY31='" + r[7].ToString().Trim() + "'";
                          }


                int res = 0;
                res = d.operation("update pay_attendance_muster set DAY01 = '" + r[8].ToString().Trim() + "',DAY02='" + r[9].ToString().Trim() + "',DAY03 = '" + r[10].ToString().Trim() + "',DAY04='" + r[11].ToString().Trim() + "',DAY05='" + r[12].ToString().Trim() + "',DAY06='" + r[13].ToString().Trim() + "',DAY07='" + r[14].ToString().Trim() + "',DAY08='" + r[15].ToString().Trim() + "',DAY09='" + r[16].ToString().Trim() + "',DAY10='" + r[17].ToString().Trim() + "',DAY11='" + r[18].ToString().Trim() + "',DAY12='" + r[19].ToString().Trim() + "',DAY13='" + r[20].ToString().Trim() + "',DAY14='" + r[21].ToString().Trim() + "',DAY15='" + r[22].ToString().Trim() + "',DAY16='" + r[23].ToString().Trim() + "',DAY17='" + r[24].ToString().Trim() + "',DAY18='" + r[25].ToString().Trim() + "',DAY19='" + r[26].ToString().Trim() + "',DAY20='" + r[27].ToString().Trim() + "',DAY21='" + r[28].ToString().Trim() + "',DAY22='" + r[29].ToString().Trim() + "',DAY23='" + r[30].ToString().Trim() + "',DAY24='" + r[31].ToString().Trim() + "',DAY25='" + r[32].ToString().Trim() + "' ,DAY26='" + r[2].ToString().Trim() + "',DAY27='" + r[3].ToString().Trim() + "',DAY28='" + r[4].ToString().Trim() + "' " + header1 + "   where comp_code = '" + Session["comp_code"].ToString() + "' and unit_code  in (" + unit_code + ") and emp_code = '" + emp_code + "' and month = '" + txttodate.Text.Substring(0, 2) + "' and year = '" + txttodate.Text.Substring(3) + "' and flag = '0' ");            
            
            }
            }
        
        
        }

      
    
    
    
    }


    // 12-03-2020 attendace upload
    protected void btn_attendance_upload_Click(object sender,  EventArgs e )
    {
        string unit_code = "";

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.getsinglestring("select group_concat(unit_code) from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and  STATE_NAME  = '" + ddl_state.SelectedValue + "' AND  branch_status  = '0'");
            unit_code = "'" + unit_code + "'";
            unit_code = unit_code.Replace(",", "','");
        }
        else
        {
            Session["UNIT_CODE"] = ddl_unitcode.SelectedValue;
            //unit_code = "pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            unit_code = "'" + Session["UNIT_CODE"].ToString() + "'";
        }



        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");


        string flag = d.getsinglestring("select flag from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and  unit_code IN (" + unit_code + ") and client_code='" + ddl_client.SelectedValue + "'");

        if (flag == "0" || flag == "1" || flag == "")
        {
            string sql = "", pay_attendance_muster = "pay_attendance_muster";
            string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-01','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-01'), '%D %b %Y'))) as fromtodate";
            string start_date_common = get_start_date();
           
            string btn_attendance = ""+1+"";
            int month1 = int.Parse(hidden_month.Value);
            int year1 = int.Parse(hidden_year.Value);
            int monthdays = DateTime.DaysInMonth(year1, month1);
            int end_date1 = monthdays;
            int i = 0;
            //rahul  start
          //  string attendance = "";


            //string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_client.SelectedValue.ToString() + "'");
            //string tempflag12 = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");

            string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue.ToString() + "'");


            //if (tempflag == "yes")
            //{
            //    attendance = "";
            //}
            //end 
            if (ot_applicable == "1")
            {
                if (start_date_common != "" && start_date_common != "1")
                {
                    d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(start_date_common), ddl_state.SelectedValue);
                    //pay_attendance_muster = "pay_attendance_muster_diff pay_attendance_muster";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + ((int.Parse(hidden_month.Value) - 1) == 0 ? (int.Parse(hidden_year.Value) - 1).ToString() : hidden_year.Value) + "-" + ((int.Parse(hidden_month.Value) - 1) == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    //string ot_emp_code = d.getsinglestring("select emp_code from pay_ot_muster where emp_CODE='" + emp_code + "' AND MONTH = " + (int.Parse(hidden_month.Value) - 1) + " AND YEAR= " + hidden_year.Value);

                    int month = int.Parse(hidden_month.Value) - 1;
                    int year = int.Parse(hidden_year.Value);
                    if (month == 0) { month = 12; year = year - 1; }

                    // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 3) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code INNER JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR left join pay_attendance_muster t2 on pay_attendance_muster.Year = t2.year and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) - 1) + " left outer join pay_ot_muster t3 on pay_ot_muster.YEAR = t3.YEAR and pay_ot_muster.UNIT_CODE = t3.UNIT_CODE and pay_ot_muster.EMP_CODE = t3.EMP_CODE and pay_ot_muster.COMP_CODE = t3.COMP_CODE AND t3.month = " + (int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_ot_muster.month = '" + hidden_month.Value + "'  and pay_attendance_muster.tot_days_present > 0  and (str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d') >= pay_employee_master.joining_date and if(left_date is null,now(),left_date) >= str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value) - 1) + "-" + start_date_common + "','%Y-%m-%d')) ORDER BY pay_employee_master.EMP_CODE";

                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 3, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master LEFT JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code  AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "'   AND pay_attendance_muster.tot_days_present > 0  INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code   left JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE  INNER JOIN pay_company_master ON pay_unit_master.comp_code = pay_company_master.comp_code  LEFT JOIN pay_ot_muster ON  pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code  AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE  AND pay_attendance_muster.MONTH = pay_ot_muster.MONTH  AND pay_attendance_muster.YEAR = pay_ot_muster.YEAR  AND pay_ot_muster.month =  '" + hidden_month.Value + "'  LEFT JOIN pay_attendance_muster t2   ON " + year + "= t2.year AND pay_company_master.COMP_CODE = t2.COMP_CODE  AND pay_unit_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE  AND t2.month ='" + month + "'  AND t2.tot_days_present > 0 LEFT OUTER JOIN pay_ot_muster t3   ON " + year + " = t3.YEAR  AND pay_unit_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_company_master.COMP_CODE = t3.COMP_CODE   AND t3.month = '" + month + "' WHERE pay_company_master.comp_code =  '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.unit_code IN( " + unit_code + ") AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                }
                else
                {
                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31,OT_DAY01 , OT_DAY02 , OT_DAY03 , OT_DAY04 , OT_DAY05 , OT_DAY06 , OT_DAY07 , OT_DAY08 , OT_DAY09 , OT_DAY10 , OT_DAY11 , OT_DAY12 , OT_DAY13 , OT_DAY14 , OT_DAY15 , OT_DAY16 , OT_DAY17 , OT_DAY18 , OT_DAY19 , OT_DAY20 , OT_DAY21 , OT_DAY22 , OT_DAY23 , OT_DAY24 , OT_DAY25 , OT_DAY26 , OT_DAY27 , OT_DAY28 , OT_DAY29 , OT_DAY30 , OT_DAY31,TOT_OT, pay_attendance_muster.tot_days_present,pay_attendance_muster.tot_days_absent AS 'absent', DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) AS 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code LEFT JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN( " + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                }

                i = 1;
            }
            else
            {

                if (start_date_common != "" && start_date_common != "1")
                {
                    d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(start_date_common), ddl_state.SelectedValue);
                    //pay_attendance_muster = "pay_attendance_muster_diff pay_attendance_muster";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value) - 1) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    if (tempflag == "yes")
                    {
                        sql = "";
                        ot_flag1 = 1;
                        string get_otday = d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 4, 1);
                        if (!get_otday.Contains("OT_DAILY_DAY31"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY31',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY30"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAY30',";
                        }
                        if (!get_otday.Contains("OT_DAILY_DAY29"))
                        {
                            get_otday = get_otday + " 0 as 'OT_DAILY_DAY29',";
                        }
                        // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date," + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " " + get_otday + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno,  IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0)  AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " LEFT JOIN `pay_daily_ot_muster` t4 ON `t4`.`year` = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `t4`.`month` = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date," + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " " + get_otday + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno, (IFNULL(`pay_daily_ot_muster`.`TOTAL_OT`, 0)+IFNULL(`t4`.`TOTAL_OT`, 0))  AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " LEFT JOIN `pay_daily_ot_muster` t4 ON `t4`.`year` = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + "  AND `pay_employee_master`.`COMP_CODE` = `t4`.`COMP_CODE` AND `pay_employee_master`.`UNIT_CODE` = `t4`.`UNIT_CODE` AND `pay_employee_master`.`EMP_CODE` = `t4`.`EMP_CODE` AND `t4`.`month` = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' group by pay_employee_master.emp_code ORDER BY pay_employee_master.emp_name";
                    }
                    else
                    {

                        //555555
                       
                       sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST,pay_employee_master.emp_code, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,pay_attendance_muster.tot_working_days as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + " and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";

                       // sql = "select  date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + " pay_attendance_muster.tot_days_present, from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + " and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.emp_name";
                    }


                }
                // attendaces 21-20 cycle end and 1-31 days satrt
                else
                {
                    //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code IN (" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                    daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + end_date1 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                    //if (((Session["comp_code"].ToString() == "C01") && (ddl_unitcode.SelectedValue == "U951")) || ((Session["comp_code"].ToString() == "C02") && (ddl_unitcode.SelectedValue == "U1036")))
                    if (tempflag == "yes")
                    {
                        sql = "";
                        ot_flag1 = 1;
                        //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno,SUBSTR(sec_to_time( OT_DAILY_DAY01 ),1,8) as ot_daily_day01,SUBSTR(sec_to_time( OT_DAILY_DAY02 ),1,8) as ot_daily_day02,SUBSTR(sec_to_time( OT_DAILY_DAY03 ),1,8) as ot_daily_day03,SUBSTR(sec_to_time( OT_DAILY_DAY04 ),1,8) as ot_daily_day04,SUBSTR(sec_to_time( OT_DAILY_DAY05 ),1,8) as ot_daily_day05,SUBSTR(sec_to_time( OT_DAILY_DAY06 ),1,8) as ot_daily_day06,SUBSTR(sec_to_time( OT_DAILY_DAY07 ),1,8) as ot_daily_day07,SUBSTR(sec_to_time( OT_DAILY_DAY08 ),1,8) as ot_daily_day08,SUBSTR(sec_to_time( OT_DAILY_DAY09 ),1,8) as ot_daily_day09,SUBSTR(sec_to_time( OT_DAILY_DAY10 ),1,8) as ot_daily_day10,SUBSTR(sec_to_time( OT_DAILY_DAY11 ),1,8) as ot_daily_day11,SUBSTR(sec_to_time( OT_DAILY_DAY12 ),1,8) as ot_daily_day12,SUBSTR(sec_to_time( OT_DAILY_DAY13 ),1,8) as ot_daily_day13,SUBSTR(sec_to_time( OT_DAILY_DAY14 ),1,8) as ot_daily_day14,SUBSTR(sec_to_time( OT_DAILY_DAY15 ),1,8) as ot_daily_day15,SUBSTR(sec_to_time( OT_DAILY_DAY16 ),1,8) as ot_daily_day16,SUBSTR(sec_to_time( OT_DAILY_DAY17 ),1,8) as ot_daily_day17,SUBSTR(sec_to_time( OT_DAILY_DAY18 ),1,8) as ot_daily_day18,SUBSTR(sec_to_time( OT_DAILY_DAY19 ),1,8) as ot_daily_day19,SUBSTR(sec_to_time( OT_DAILY_DAY20 ),1,8) as ot_daily_day20,SUBSTR(sec_to_time( OT_DAILY_DAY21 ),1,8) as ot_daily_day21,SUBSTR(sec_to_time( OT_DAILY_DAY22 ),1,8) as ot_daily_day22,SUBSTR(sec_to_time( OT_DAILY_DAY23 ),1,8) as ot_daily_day23,SUBSTR(sec_to_time( OT_DAILY_DAY24 ),1,8) as ot_daily_day24,SUBSTR(sec_to_time( OT_DAILY_DAY25 ),1,8) as ot_daily_day25,SUBSTR(sec_to_time( OT_DAILY_DAY26 ),1,8) as ot_daily_day26,SUBSTR(sec_to_time( OT_DAILY_DAY27 ),1,8) as ot_daily_day27,SUBSTR(sec_to_time( OT_DAILY_DAY28 ),1,8) as ot_daily_day28,SUBSTR(sec_to_time( OT_DAILY_DAY29 ),1,8) as ot_daily_day29,SUBSTR(sec_to_time( OT_DAILY_DAY30 ),1,8) as ot_daily_day30,SUBSTR(sec_to_time( OT_DAILY_DAY31 ),1,8) as ot_daily_day31,SUBSTR(sec_to_time(  TOTAL_OT  ),1,8) as  TOTAL_OT   from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        sql = "SELECT pay_employee_master.emp_code, CASE WHEN `DAY01` = '0' THEN 'A' ELSE `DAY01` END AS 'DAY01', CASE WHEN `DAY02` = '0' THEN 'A' ELSE `DAY02` END AS 'DAY02', CASE WHEN `DAY03` = '0' THEN 'A' ELSE `DAY03` END AS 'DAY03', CASE WHEN `DAY04` = '0' THEN 'A' ELSE `DAY04` END AS 'DAY04', CASE WHEN `DAY05` = '0' THEN 'A' ELSE `DAY05` END AS 'DAY05', CASE WHEN `DAY06` = '0' THEN 'A' ELSE `DAY06` END AS 'DAY06', CASE WHEN `DAY07` = '0' THEN 'A' ELSE `DAY07` END AS 'DAY07', CASE WHEN `DAY08` = '0' THEN 'A' ELSE `DAY08` END AS 'DAY08', CASE WHEN `DAY09` = '0' THEN 'A' ELSE `DAY09` END AS 'DAY09', CASE WHEN `DAY10` = '0' THEN 'A' ELSE `DAY10` END AS 'DAY10', CASE WHEN `DAY11` = '0' THEN 'A' ELSE `DAY11` END AS 'DAY11', CASE WHEN `DAY12` = '0' THEN 'A' ELSE `DAY12` END AS 'DAY12', CASE WHEN `DAY13` = '0' THEN 'A' ELSE `DAY13` END AS 'DAY13', CASE WHEN `DAY14` = '0' THEN 'A' ELSE `DAY14` END AS 'DAY14', CASE WHEN `DAY15` = '0' THEN 'A' ELSE `DAY15` END AS 'DAY15', CASE WHEN `DAY16` = '0' THEN 'A' ELSE `DAY16` END AS 'DAY16', CASE WHEN `DAY17` = '0' THEN 'A' ELSE `DAY17` END AS 'DAY17', CASE WHEN `DAY18` = '0' THEN 'A' ELSE `DAY18` END AS 'DAY18', CASE WHEN `DAY19` = '0' THEN 'A' ELSE `DAY19` END AS 'DAY19', CASE WHEN `DAY20` = '0' THEN 'A' ELSE `DAY20` END AS 'DAY20', CASE WHEN `DAY21` = '0' THEN 'A' ELSE `DAY21` END AS 'DAY21', CASE WHEN `DAY22` = '0' THEN 'A' ELSE `DAY22` END AS 'DAY22', CASE WHEN `DAY23` = '0' THEN 'A' ELSE `DAY23` END AS 'DAY23', CASE WHEN `DAY24` = '0' THEN 'A' ELSE `DAY24` END AS 'DAY24', CASE WHEN `DAY25` = '0' THEN 'A' ELSE `DAY25` END AS 'DAY25', CASE WHEN `DAY26` = '0' THEN 'A' ELSE `DAY26` END AS 'DAY26', CASE WHEN `DAY27` = '0' THEN 'A' ELSE `DAY27` END AS 'DAY27', CASE WHEN `DAY28` = '0' THEN 'A' ELSE `DAY28` END AS 'DAY28', CASE WHEN `DAY29` = '0' THEN 'A' ELSE `DAY29` END AS 'DAY29', CASE WHEN `DAY30` = '0' THEN 'A' ELSE `DAY30` END AS 'DAY30', CASE WHEN `DAY31` = '0' THEN 'A' ELSE `DAY31` END AS 'DAY31', `pay_attendance_muster`.`tot_days_present`, `pay_attendance_muster`.`tot_days_absent` AS 'absent', DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) AS 'total days', `LocationHead_Name`, `LocationHead_mobileno`, `OT_DAILY_DAY01` AS 'ot_daily_day01', `OT_DAILY_DAY02` AS 'ot_daily_day02', `OT_DAILY_DAY03` AS 'ot_daily_day03', `OT_DAILY_DAY04` AS 'ot_daily_day04', `OT_DAILY_DAY05` AS 'ot_daily_day05', `OT_DAILY_DAY06` AS 'ot_daily_day06', `OT_DAILY_DAY07` AS 'ot_daily_day07', `OT_DAILY_DAY08` AS 'ot_daily_day08', `OT_DAILY_DAY09` AS 'ot_daily_day09', `OT_DAILY_DAY10` AS 'ot_daily_day10', `OT_DAILY_DAY11` AS 'ot_daily_day11', `OT_DAILY_DAY12` AS 'ot_daily_day12', `OT_DAILY_DAY13` AS 'ot_daily_day13', `OT_DAILY_DAY14` AS 'ot_daily_day14', `OT_DAILY_DAY15` AS 'ot_daily_day15', `OT_DAILY_DAY16` AS 'ot_daily_day16', `OT_DAILY_DAY17` AS 'ot_daily_day17', `OT_DAILY_DAY18` AS 'ot_daily_day18', `OT_DAILY_DAY19` AS 'ot_daily_day19', `OT_DAILY_DAY20` AS 'ot_daily_day20', `OT_DAILY_DAY21` AS 'ot_daily_day21', `OT_DAILY_DAY22` AS 'ot_daily_day22', `OT_DAILY_DAY23` AS 'ot_daily_day23', `OT_DAILY_DAY24` AS 'ot_daily_day24', `OT_DAILY_DAY25` AS 'ot_daily_day25', `OT_DAILY_DAY26` AS 'ot_daily_day26', `OT_DAILY_DAY27` AS 'ot_daily_day27', `OT_DAILY_DAY28` AS 'ot_daily_day28', `OT_DAILY_DAY29` AS 'ot_daily_day29', `OT_DAILY_DAY30` AS 'ot_daily_day30', `OT_DAILY_DAY31` AS 'ot_daily_day31', `TOTAL_OT` AS 'TOTAL_OT' from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";

                    }
                    else
                    {
                        // sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code inner join pay_daily_ot_muster on  pay_daily_ot_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_daily_ot_muster . comp_code  =  pay_employee_master . comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.EMP_CODE";
                        
                       // origial query chage query 12-03-2020
                        //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster. tot_days_present ,pay_attendance_muster. tot_days_absent  AS 'absent',DAY(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' " + attendance + " ORDER BY pay_employee_master.emp_name";


                        sql = "select pay_employee_master.emp_code, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31 from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code  where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' ORDER BY pay_employee_master.emp_name";
                    }
                }
                i = 2;
            }
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename= " + ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + ddl_state.SelectedValue + "_" +".xlsx");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;

                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, i, ds, start_date_common, d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 0, 1), ot_flag1, btn_attendance);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, i, ds, start_date_common, "", ot_flag1, btn_attendance);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, i, ds, start_date_common, "", ot_flag1, btn_attendance);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(stringWrite.ToString());


                string abc = stringWrite.ToString();

                string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
                string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                string fname = null; 
                fname = ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + ddl_state.SelectedValue + "_" + ".xlsx";
              //  string filename1 = path + "emp_Attendance_Copy_" + unitcode + ".xls";
                string filename1 = path + fname;


                System.IO.File.WriteAllText(filename1, abc);
                Response.Flush();
                Response.End();

                //   Session["month"] = txttodate.Text.Substring(0, 2);
                Session["year"] = txttodate.Text;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }
        }


    }
	    protected void btn_report_Click(object sender, EventArgs e)
    {
        HiddenField2.Value = "1";
        driver_conveyance_status();
    }
    protected void driver_conveyance_status()
    {
       
        try
        {
            Panel_notification_conv_driver.Visible = true;
            ViewState["driver_con_remaing"] = 0;
            ViewState["driver_con_approve_admin"] = 0;
            ViewState["driver_con_approve_finance"] = 0;
            ViewState["driver_con_reject_finance"] = 0;


            //ViewState["Conv_Message"] = 0;

            //ViewState["Conv_reject_attendance"] = 0;
            //ViewState["Conv_appro_attendannce"] = 0;
            //ViewState["Conv_reject_attendance"] = 0;
            //ViewState["Conv_appro_attendannce_finanace"] = 0;
            //ViewState["Conv_deployment"] = 0;
            //ViewState["Conv_closed_branch"] = 0;
            //conveyance_count();

            DataTable dt_item = new DataTable();
            //Remaining Approve By Admin
            driver_con_notapprove_admin.DataSource = null;
            driver_con_notapprove_admin.DataBind();
            dt_item = d.chk_driver_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 0);
            driver_con_remaing = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["driver_con_remaing"] = dt_item.Rows.Count.ToString();
                driver_con_remaing = ViewState["driver_con_remaing"].ToString();
                driver_con_notapprove_admin.DataSource = dt_item;
                driver_con_notapprove_admin.DataBind();
                // Panel_not_approve_conv_driver.Visible = true;
            }


            //Approve By Admin
            gv_approved_driver_conveyance.DataSource = null;
            gv_approved_driver_conveyance.DataBind();
            dt_item = d.chk_driver_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 1);
            driver_con_approve_admin = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["driver_con_approve_admin"] = dt_item.Rows.Count.ToString();
                driver_con_approve_admin = ViewState["driver_con_approve_admin"].ToString();

                gv_approved_driver_conveyance.DataSource = dt_item;
                gv_approved_driver_conveyance.DataBind();
                Panel_appro_con_driver.Visible = true;
            }
            dt_item.Dispose();

            //Approve By Finance
            driver_con_appro_finance.DataSource = null;
            driver_con_appro_finance.DataBind();

            dt_item = d.chk_driver_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 2);
            driver_con_approve_finance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["driver_con_approve_finance"] = dt_item.Rows.Count.ToString();
                driver_con_approve_finance = ViewState["driver_con_approve_finance"].ToString();

                driver_con_appro_finance.DataSource = dt_item;
                driver_con_appro_finance.DataBind();

            }
            dt_item.Dispose();

            //Reject By Finance
            driver_con_notapprove_fiance.DataSource = null;
            driver_con_notapprove_fiance.DataBind();
            dt_item = d.chk_driver_con(Session["COMP_CODE"].ToString(), con_ddl_client.SelectedValue, con_ddl_state.SelectedValue, con_ddl_unitcode.SelectedValue, txt_date_conveyance.Text.Substring(0, 2), txt_date_conveyance.Text.Substring(3), 3);
            driver_con_reject_finance = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["driver_con_reject_finance"] = dt_item.Rows.Count.ToString();
                driver_con_reject_finance = ViewState["driver_con_reject_finance"].ToString();

                driver_con_notapprove_fiance.DataSource = dt_item;
                driver_con_notapprove_fiance.DataBind();

            }
            dt_item.Dispose();

            // Panel_not_approve_conv_driver.Visible = true;
            Panel_appro_con_driver.Visible = true; Panel_reject_con_driver.Visible = true; Panel_appro_finance_con_driver.Visible = true;



        }
        catch (Exception ex) { throw ex; }
        finally { }



    }


    protected void gv_branchwise_material_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[2].Visible = false;
        

    }

    protected void btn_process_service_Click(object sender, EventArgs e)
    {
        RandM_panel.Visible = true;
        process_gv.Visible = false;
        txt_party_name.Text = "";
        txt_help_req_no.Text = "";
        txt_amount.Text = "";
        txt_bank_acc_no.Text = "";
        txt_ifsc_code.Text = "";
        string database_service_check = "";
        database_service_check = d.getsinglestring("select ID,client_code,state_name,unit_code,month,year from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' and (approve_flag='2' || approve_flag='1')");
        if (database_service_check != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Already Approve You Can Not Make Changes ');", true);
            RandM_panel.Visible = false;
            process_gv.Visible = true;
            gv_r_m.Visible = false;
            btn_r_m.Visible = false;
            process_r_m_gv();
            return;
        }
        else
        {
            process_gv.Visible = false;
            string check_reject_rec = d.getsinglestring("select ID ,approve_flag from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' ");
            if (check_reject_rec != "")
            {
                r_m_gv();
                gv_r_m.Visible = true;
                btn_r_m.Visible = true;
            }
            //MySqlCommand cmd = new MySqlCommand("select ID,party_name,help_req_number,amount,bank_acc_no,ifsc_code from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "'", d.con);
            //d.con.Open();
            //try
            //{
            //    MySqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {

            //        txt_party_name.Text = dr.GetValue(1).ToString();
            //        txt_help_req_no.Text = dr.GetValue(2).ToString();
            //        txt_amount.Text = dr.GetValue(3).ToString();
            //        txt_bank_acc_no.Text = dr.GetValue(4).ToString();
            //        txt_ifsc_code.Text = dr.GetValue(5).ToString();
            //        dr.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    d.con.Close();
            //}
        }
    }
    protected void btn_process_administrative_Click(object sender, EventArgs e)
    {
        administrative_panel.Visible = true;
        process_admin_panel.Visible = false;
        txt_party_adm.Text = "";
        txt_req_no_adm.Text = "";
        txt_amount_req.Text = "";
        txt_bank_account_adm.Text = "";
        txt_ifsc_adm.Text = "";
        string database_service_check = "";
        database_service_check = d.getsinglestring("select ID,client_code,state_name,unit_code,month,year from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' and (approve_flag='2'||approve_flag='1')");
        if (database_service_check != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Already Approve You Can Not Make Changes ');", true);
            administrative_panel.Visible = false;
            process_admin_panel.Visible = true;
            admin_gv_ap.Visible = false;
            btn_admin_ap.Visible = false;
            process_admin_gv();
            return;
        }
        else
        {
            process_admin_panel.Visible = false;
            string check_reject_rec = d.getsinglestring("select ID ,approve_flag from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' ");
            if (check_reject_rec != "")
            {
                admin_gv();
                admin_gv_ap.Visible = true;
                btn_admin_ap.Visible = true;
            }
            //MySqlCommand cmd = new MySqlCommand("select ID,party_name,days,amount,bank_acc_no,ifsc_code from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "'", d.con);
            //d.con.Open();
            //try
            //{
            //    MySqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {

                    //txt_party_adm.Text = dr.GetValue(1).ToString();
                    //txt_req_no_adm.Text = dr.GetValue(2).ToString();
                    //txt_amount_req.Text = dr.GetValue(3).ToString();
                    //txt_bank_account_adm.Text = dr.GetValue(4).ToString();
                    //txt_ifsc_adm.Text = dr.GetValue(5).ToString();
        //            dr.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        d.con.Close();
        //    }
        }
    }
    protected void client_name_r_m()
    {
        //R&M service
        ddl_client_service.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_service.DataSource = dt_item;
                ddl_client_service.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_service.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_service.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_client_service.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //Administative expenses
        ddl_client_adm.Items.Clear();
        System.Data.DataTable dt_item1 = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1= new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item1.Fill(dt_item1);
            if (dt_item1.Rows.Count > 0)
            {
                ddl_client_adm.DataSource = dt_item1;
                ddl_client_adm.DataTextField = dt_item1.Columns[0].ToString();
                ddl_client_adm.DataValueField = dt_item1.Columns[1].ToString();
                ddl_client_adm.DataBind();
            }
            dt_item1.Dispose();

            d.con.Close();
            ddl_client_adm.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    }
    protected void ddl_client_service_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        ddl_state_service.Items.Clear();
        // ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client_service.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_service.DataSource = dt_item;
                ddl_state_service.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_service.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_service.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_service.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            // ddl_unitcode.Items.Clear();
        }
        //Notification_panel.Visible = false;
    }
    protected void ddl_state_service_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        ddl_branch_service.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //comment 30/09 branch_closing  MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state_service.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_service.SelectedValue + "' AND branch_status = 0 AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_service.SelectedValue + "') AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) and r_m_applicable = 1   ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_service.DataSource = dt_item;
                ddl_branch_service.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_service.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_service.DataBind();
                ddl_branch_service.Items.Insert(0, "Select");
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            RandM_panel.Visible = false;
            gv_r_m.Visible = false;
            btn_r_m_aprrove.Visible = false;
        }
    }
    protected void btn_save_service_Click(object sender, EventArgs e)
    {
        //show_upload.Visible = true;
        gv_r_m.Visible = true;
        btn_r_m.Visible = true;
        string database_service_check = "";
        int res = 0;
        string new_file_name = "";
        if (r_m_upload.HasFile)
        {
            
            string fileExt = System.IO.Path.GetExtension(r_m_upload.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(r_m_upload.PostedFile.FileName);
                r_m_upload.PostedFile.SaveAs(Server.MapPath("~/r_m_images/") + fileName);
                // string id = d.getsinglestring("select ifnull(max(id),0) from pay_material_details");

                string file_name = GenerateFileName(ddl_state_service.SelectedValue + "_" + ddl_branch_service.SelectedItem.Text + "_" + txt_party_name.Text);
                 new_file_name = ddl_client_service.SelectedValue + "_" + file_name.Substring(0, 50) + fileExt;

                File.Copy(Server.MapPath("~/r_m_images/") + fileName, Server.MapPath("~/r_m_images/") + new_file_name, true);
                File.Delete(Server.MapPath("~/r_m_images/") + fileName);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
                return;
            }
        }
        //database_service_check = d.getsinglestring("select ID,client_code,state_name,unit_code,month,year from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' and party_name='" + txt_party_name.Text + "' and help_req_number='" + txt_help_req_no.Text + "' and amount='" + txt_amount.Text + "' and bank_acc_no='" + txt_bank_acc_no.Text + "' and  ifsc_code='" + txt_ifsc_code.Text + "'");
        //if (database_service_check != "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Already Save');", true);
        //    return;
        //}
        int result = 0;
        //d.operation("delete from pay_r_and_m_service where client_code='" + ddl_client_service.SelectedValue + "' AND state_name = '" + ddl_state_service.SelectedValue + "' AND unit_code= '" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "'");
        result = d.operation("insert into pay_r_and_m_service(comp_code,client_code,state_name,unit_code,month,year,party_name,help_req_number,amount,bank_acc_no,ifsc_code)value ('" + Session["comp_code"].ToString() + "','" + ddl_client_service.SelectedValue + "','" + ddl_state_service.SelectedValue + "','" + ddl_branch_service.SelectedValue + "','" + txt_date.Text.Substring(0, 2) + "','" + txt_date.Text.Substring(3) + "','" + txt_party_name.Text + "','" + txt_help_req_no.Text + "','" + txt_amount.Text + "','" + txt_bank_acc_no.Text + "','" + txt_ifsc_code.Text + "')");
       res=d.operation("update pay_r_and_m_service set image_name='" + new_file_name + "' where client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "'and unit_code='" + ddl_branch_service.SelectedValue + "'and month='" + txt_date.Text.Substring(0, 2) + "'and year='" + txt_date.Text.Substring(3) + "'and party_name='" + txt_party_name.Text + "'and help_req_number='" + txt_help_req_no.Text + "'and amount='" + txt_amount.Text + "'and bank_acc_no='" + txt_bank_acc_no.Text + "'and ifsc_code='" + txt_ifsc_code.Text + "'");
       if (result > 0)
       {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save successfully ');", true);
           btn_r_m_aprrove.Visible = true;
           txt_party_name.Text = "";
           txt_help_req_no.Text = "";
           txt_amount.Text = "";
           txt_bank_acc_no.Text = "";
           txt_ifsc_code.Text = "";
       }
       if (res > 0)
       {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded successfully ');", true);
       }
       r_m_gv();

    }
    protected void ddl_client_adm_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        ddl_state_adm.Items.Clear();
        // ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client_adm.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_adm.DataSource = dt_item;
                ddl_state_adm.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_adm.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_adm.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_adm.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            // ddl_unitcode.Items.Clear();
        }
        //Notification_panel.Visible = false;


        
    }
    //protected void btn_upload_r_m_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    //    catch { }
    //    string database_service_check = "";
    //    database_service_check = d.getsinglestring("select ID,client_code,state_name,unit_code,month,year from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' and party_name='" + txt_party_name.Text + "' and help_req_number='" + txt_help_req_no.Text + "' and amount='" + txt_amount.Text + "' and bank_acc_no='" + txt_bank_acc_no.Text + "' and  ifsc_code='" + txt_ifsc_code .Text+ "'");
    //    if (database_service_check == "")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Save Record First  ');", true);
    //        RandM_panel.Visible = false;
    //        return;
    //    }
    //    if (r_m_upload.HasFile)
    //    {
    //        int res = 0;
    //        string fileExt = System.IO.Path.GetExtension(r_m_upload.FileName);
    //        if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
    //        {
    //            string fileName = Path.GetFileName(r_m_upload.PostedFile.FileName);
    //            r_m_upload.PostedFile.SaveAs(Server.MapPath("~/r_m_images/") + fileName);
    //           // string id = d.getsinglestring("select ifnull(max(id),0) from pay_material_details");

    //            string file_name = GenerateFileName(ddl_state_service.SelectedValue+"_"+ddl_branch_service.SelectedItem.Text);
    //            string new_file_name = ddl_client_service.SelectedValue + "_" + file_name.Substring(0, 20) + fileExt;

    //            File.Copy(Server.MapPath("~/r_m_images/") + fileName, Server.MapPath("~/r_m_images/") + new_file_name, true);
    //            File.Delete(Server.MapPath("~/r_m_images/") + fileName);
    //            res=d.operation("update pay_r_and_m_service set image_name='" + new_file_name + "' where client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "'and unit_code='" + ddl_branch_service.SelectedValue + "'and month='" + txt_date.Text.Substring(0, 2) + "'and year='" + txt_date.Text.Substring(3) + "'and party_name='" + txt_party_name.Text + "'and help_req_number='" + txt_help_req_no.Text + "'and amount='" + txt_amount.Text + "'and bank_acc_no='" + txt_bank_acc_no.Text + "'and ifsc_code='" + txt_ifsc_code.Text + "'");
    //            if (res > 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded successfully ');", true);
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
    //            return;
    //        }
    //    }
    //}
    protected void ddl_state_adm_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        ddl_branch_adm.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //comment 30/09 branch_closing  MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state_adm.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_adm.SelectedValue + "' AND branch_status = 0 AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_adm.SelectedValue + "') AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) and administrative_applicable = 1 ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_adm.DataSource = dt_item;
                ddl_branch_adm.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_adm.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_adm.DataBind();
                ddl_branch_adm.Items.Insert(0, "Select");
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            administrative_panel.Visible = false;
            admin_gv_ap.Visible = false;
            btn_admin_aprrove.Visible = false;
           
        }
    }
    //protected void btn_upload_admini_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    //    catch { }
    //    if (administrative_upload.HasFile)
    //    {
    //        int res = 0;
    //        string fileExt = System.IO.Path.GetExtension(administrative_upload.FileName);
    //        if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
    //        {
    //            string fileName = Path.GetFileName(administrative_upload.PostedFile.FileName);
    //            administrative_upload.PostedFile.SaveAs(Server.MapPath("~/administrative_images/") + fileName);


    //            string file_name = GenerateFileName(ddl_state_adm.SelectedValue+"_"+ddl_branch_adm.SelectedValue);
    //            string new_file_name = ddl_client_adm.SelectedValue + "_" + file_name.Substring(0, 10) + fileExt;

    //            File.Copy(Server.MapPath("~/administrative_images/") + fileName, Server.MapPath("~/administrative_images/") + new_file_name, true);
    //            File.Delete(Server.MapPath("~/administrative_images/") + fileName);
    //            res = d.operation("update pay_administrative_expense set image_name='" + new_file_name + "' where client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "'and unit_code='" + ddl_branch_adm.SelectedValue + "'and month='" + txt_date_adm.Text.Substring(0, 2) + "'and year='" + txt_date_adm.Text.Substring(3) + "'and party_name='" + txt_party_adm.Text + "'and days='" + txt_req_no_adm.Text + "'and amount='" + txt_amount_req.Text + "'and bank_acc_no='" + txt_bank_account_adm.Text + "'and ifsc_code='" + txt_ifsc_adm.Text + "'");
    //            if (res > 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded successfully ');", true);
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
    //            return;
    //        }
    //    }
    //}
    protected void btn_save_administrative_Click(object sender, EventArgs e)
    {
       // show_upload_adm.Visible = true;
        string database_service_check = "";
        int res = 0;
        string new_file_name = "";
        admin_gv_ap.Visible = true;
        btn_admin_ap.Visible = true;
        btn_admin_aprrove.Visible = true;
        database_service_check = d.getsinglestring("select ID,client_code,state_name,unit_code,month,year from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' and party_name='" + txt_party_adm.Text + "' and days='" + txt_req_no_adm.Text + "' and amount='" + txt_amount_req.Text + "' and bank_acc_no='" + txt_bank_account_adm.Text + "' and  ifsc_code='" + txt_ifsc_adm.Text + "'");
        if (database_service_check != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Already Save');", true);
            return;
        }
        if (administrative_upload.HasFile)
        {
            
            string fileExt = System.IO.Path.GetExtension(administrative_upload.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(administrative_upload.PostedFile.FileName);
                administrative_upload.PostedFile.SaveAs(Server.MapPath("~/administrative_images/") + fileName);


                string file_name = GenerateFileName(ddl_state_adm.SelectedValue + "_" + ddl_branch_adm.SelectedValue + "_" + txt_party_adm.Text);
                 new_file_name = ddl_client_adm.SelectedValue + "_" + file_name.Substring(0, 50) + fileExt;

                File.Copy(Server.MapPath("~/administrative_images/") + fileName, Server.MapPath("~/administrative_images/") + new_file_name, true);
                File.Delete(Server.MapPath("~/administrative_images/") + fileName);
            }
        }
        int result = 0;
        //d.operation("delete from pay_administrative_expense where client_code='" + ddl_client_adm.SelectedValue + "' AND state_name = '" + ddl_state_adm.SelectedValue + "' AND unit_code= '" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "'");
        result = d.operation(" insert into pay_administrative_expense(comp_code,client_code,state_name,unit_code,month,year,party_name,days,amount,bank_acc_no,ifsc_code)value ('" + Session["comp_code"].ToString() + "','" + ddl_client_adm.SelectedValue + "','" + ddl_state_adm.SelectedValue + "','" + ddl_branch_adm.SelectedValue + "','" + txt_date_adm.Text.Substring(0, 2) + "','" + txt_date_adm.Text.Substring(3) + "','" + txt_party_adm.Text + "','" + txt_req_no_adm.Text + "','" + txt_amount_req.Text + "','" + txt_bank_account_adm.Text + "','" + txt_ifsc_adm.Text + "')");
        res = d.operation("update pay_administrative_expense set image_name='" + new_file_name + "' where client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "'and unit_code='" + ddl_branch_adm.SelectedValue + "'and month='" + txt_date_adm.Text.Substring(0, 2) + "'and year='" + txt_date_adm.Text.Substring(3) + "'and party_name='" + txt_party_adm.Text + "'and days='" + txt_req_no_adm.Text + "'and amount='" + txt_amount_req.Text + "'and bank_acc_no='" + txt_bank_account_adm.Text + "'and ifsc_code='" + txt_ifsc_adm.Text + "'");
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Save successfully ');", true);
            btn_admin_aprrove.Visible = true;

        }
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded successfully ');", true);
            txt_party_adm.Text = "";
            txt_req_no_adm.Text = "";
            txt_amount_req.Text = "";
            txt_bank_account_adm.Text = "";
            txt_ifsc_adm.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
            return;
        }
        admin_gv();
    }
    protected void btn_approve_service_Click(object sender, EventArgs e)
    {
        int result = 0;
        string database_service_check = "";
        database_service_check = d.getsinglestring("select image_name from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' and party_name='" + txt_party_name.Text + "' and help_req_number='" + txt_help_req_no.Text + "' and amount='" + txt_amount.Text + "' and bank_acc_no='" + txt_bank_acc_no.Text + "' and  ifsc_code='" + txt_ifsc_code .Text+ "'");
        if (database_service_check == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Upload Image first');", true);
            return;
        }
        result = d.operation("update pay_r_and_m_service set  approve_flag='1',r_m_status='Approve By Admin' where client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "'and unit_code='" + ddl_branch_service.SelectedValue + "'and month='" + txt_date.Text.Substring(0, 2) + "'and year='" + txt_date.Text.Substring(3) + "'and party_name='" + txt_party_name.Text + "'and help_req_number='" + txt_help_req_no.Text + "'and amount='" + txt_amount.Text + "'and bank_acc_no='" + txt_bank_acc_no.Text + "'and ifsc_code='" + txt_ifsc_code.Text + "'");
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved Successfully');", true);
            RandM_panel.Visible = false;
            // show_upload.Visible = false;
            ddl_client_service.SelectedValue = "Select";
            ddl_state_service.Items.Clear();
            ddl_branch_service.Items.Clear();
            txt_date.Text = "";
            btn_approve.Visible = false;
            btn_save_service.Visible = false;
            btn_close_service_rm.Visible = false;
        }
       
    }
    protected void btn_approve_administrative_Click(object sender, EventArgs e)
    {
        int res = 0;
        string database_service_check = "";
        database_service_check = d.getsinglestring("select image_name from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' and party_name='" + txt_party_adm.Text + "' and days='" + txt_req_no_adm.Text + "' and amount='" + txt_amount_req.Text + "' and bank_acc_no='" + txt_bank_account_adm.Text + "' and  ifsc_code='" + txt_ifsc_adm.Text + "'");
        if (database_service_check == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Upload Image first');", true);
            return;
        }
        res = d.operation("update pay_administrative_expense set approve_flag='1',admini_status='Approve By Admin' where client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "'and unit_code='" + ddl_branch_adm.SelectedValue + "'and month='" + txt_date_adm.Text.Substring(0, 2) + "'and year='" + txt_date_adm.Text.Substring(3) + "'and party_name='" + txt_party_adm.Text + "'and days='" + txt_req_no_adm.Text + "'and amount='" + txt_amount_req.Text + "'and bank_acc_no='" + txt_bank_account_adm.Text + "'and ifsc_code='" + txt_ifsc_adm.Text + "'");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Approved Successfully');", true);
        }
        administrative_panel.Visible = false;
       // show_upload_adm.Visible = false;
        ddl_client_adm.SelectedValue = "Select";
        ddl_state_adm.Items.Clear();
        ddl_branch_adm.Items.Clear();
        txt_date_adm.Text = "";
    }
    protected void r_m_gv()
    {
        try
        {
            
            System.Data.DataTable dt = new System.Data.DataTable();
            gv_rm.DataSource = null;
            gv_rm.DataBind();

            d4.con.Open();
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("select ID,party_name,help_req_number,amount,bank_acc_no,ifsc_code,image_name,r_m_status from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gv_rm.DataSource = dt;
                gv_rm.DataBind();
            }
                        d4.con.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d4.con.Close();
        }
    }
    protected void gv_rm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            if (e.Row.Cells[1].Text.ToUpper().Equals("APPROVE BY FINANCE"))
            {
               
            }
            if (e.Row.Cells[8].Text.ToUpper().Equals("APPROVE BY ADMIN"))
            {
                e.Row.Cells[10].Text = "";
                e.Row.Cells[2].Text = "";
               
            }
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void btn_r_m_aprrove_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int result = 0;
        foreach (GridViewRow gvrow_check in gv_rm.Rows)
        {
            var checkbox = gvrow_check.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select All Record !!');", true);
                return;
            }
        }
        foreach (GridViewRow gvrow in gv_rm.Rows)
        {
            var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            string cell_1_Value = gv_rm.Rows[gvrow.RowIndex].Cells[0].Text;
            string cell_2_Value = gv_rm.Rows[gvrow.RowIndex].Cells[3].Text;
            string cell_3_Value = gv_rm.Rows[gvrow.RowIndex].Cells[4].Text;
            string cell_4_Value = gv_rm.Rows[gvrow.RowIndex].Cells[5].Text;
            string cell_5_Value = gv_rm.Rows[gvrow.RowIndex].Cells[6].Text;
            string cell_6_Value = gv_rm.Rows[gvrow.RowIndex].Cells[7].Text;
          
            if (checkbox.Checked == true)
            {
                    result = d.operation("update pay_r_and_m_service set approve_flag = '1', r_m_status = 'Approve By Admin',reject_reason='' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and state_name='" + ddl_state_service.SelectedValue + "' and client_code='" + ddl_client_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month = '" + txt_date.Text.Substring(0, 2) + "' and year = '" + txt_date.Text.Substring(3) + "' and ID='"+cell_1_Value+"' and party_name='" + cell_2_Value + "' and help_req_number='" + cell_3_Value + "' and amount='" + cell_4_Value + "' and bank_acc_no='" + cell_5_Value + "' and ifsc_code='"+cell_6_Value+"' ");
            }

        }
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' R&M Approved Successfully !!');", true);
            //process_r_m_gv();
           // r_m_gv();
            RandM_panel.Visible = false;
            btn_r_m_aprrove.Visible = false;
        }
        
    }
    protected void lnk_R_M_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        d.operation("delete from pay_r_and_m_service where ID='"+filename+"' ");
        r_m_gv();
    }
    protected void admin_gv()
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            gv_admin.DataSource = null;
            gv_admin.DataBind();

            d4.con.Open();
            MySqlCommand cmd = null;

            cmd = new MySqlCommand("select ID,party_name,days,amount,bank_acc_no,ifsc_code,image_name,admini_status from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' ", d.con);

            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gv_admin.DataSource = dt;
                gv_admin.DataBind();
            }
            d4.con.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d4.con.Close();
        }
    }
    protected void btn_admin_aprrove_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int result = 0;
        foreach (GridViewRow gvrow_check in gv_admin.Rows)
        {
            var checkbox = gvrow_check.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select All Record !!');", true);
                return;
            }
        }
        foreach (GridViewRow gvrow in gv_admin.Rows)
        {
            var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            string cell_1_Value = gv_admin.Rows[gvrow.RowIndex].Cells[0].Text;
            string cell_2_Value = gv_admin.Rows[gvrow.RowIndex].Cells[3].Text;
            string cell_3_Value = gv_admin.Rows[gvrow.RowIndex].Cells[4].Text;
            string cell_4_Value = gv_admin.Rows[gvrow.RowIndex].Cells[5].Text;
            string cell_5_Value = gv_admin.Rows[gvrow.RowIndex].Cells[6].Text;
            string cell_6_Value = gv_admin.Rows[gvrow.RowIndex].Cells[7].Text;

            if (checkbox.Checked == true)
            {
                    result = d.operation("update pay_administrative_expense set approve_flag='1',admini_status='Approve By Admin' where client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "'and unit_code='" + ddl_branch_adm.SelectedValue + "'and month='" + txt_date_adm.Text.Substring(0, 2) + "'and year='" + txt_date_adm.Text.Substring(3) + "' and ID='"+cell_1_Value+"' and party_name='" + cell_2_Value + "'and days='" + cell_3_Value + "'and amount='" + cell_4_Value + "'and bank_acc_no='" + cell_5_Value + "'and ifsc_code='" + cell_6_Value + "'");
            }

        }
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Administrative Expeses Approved Successfully !!');", true);
            administrative_panel.Visible = false;
            btn_admin_aprrove.Visible = false;
            admin_gv();
        }
    }
    protected void lnk_admin_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        d.operation("delete from pay_administrative_expense where ID='" + filename + "' ");
        admin_gv();
    }
    protected void lnk_R_M_download_gv_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[0];
        if (filename != "")
        {
            downloadfile_r_m(filename, unit_name);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }
    }
    protected void downloadfile_r_m(string filename, string unit_name)
    {
        var result = filename.Substring(filename.Length - 4);
        if (result.Contains("jpeg"))
        {
            result = ".jpeg";
        }
        try
        {
            string path2 = Server.MapPath("~\\r_m_images\\" + filename);
            string unitName = unit_name + "-Billing_type" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\r_m_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }
        catch (Exception ex) { }
    }
    protected void lnk_admin_download_gv_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[0];
        if (filename != "")
        {
            downloadfile_admin(filename, unit_name);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }
    }
    protected void downloadfile_admin(string filename, string unit_name)
    {
        var result = filename.Substring(filename.Length - 4);
        if (result.Contains("jpeg"))
        {
            result = ".jpeg";
        }
        try
        {
            string path2 = Server.MapPath("~\\administrative_images\\" + filename);
            string unitName = unit_name + "-Billing_type" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\administrative_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }
        catch (Exception ex) { }
    }
    protected void process_r_m_gv()
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            process_gv_r_m.DataSource = null;
            process_gv_r_m.DataBind();

            d4.con.Open();
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("select ID,party_name,help_req_number,amount,bank_acc_no,ifsc_code,image_name,r_m_status from pay_r_and_m_service where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_service.SelectedValue + "' and state_name='" + ddl_state_service.SelectedValue + "' and unit_code='" + ddl_branch_service.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' and (approve_flag='2' || approve_flag='1') ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                process_gv_r_m.DataSource = dt;
                process_gv_r_m.DataBind();
            }
            d4.con.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d4.con.Close();
        }
    }
    protected void process_gv_r_m_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void process_admin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void lnk_admin_download_gv_Command1(object sender, CommandEventArgs e)
    {

    }
    protected void process_admin_gv()
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            process_admin.DataSource = null;
            process_admin.DataBind();

            d4.con.Open();
            MySqlCommand cmd = null;
            cmd = new MySqlCommand("select ID,party_name,days,amount,bank_acc_no,ifsc_code,image_name,admini_status from pay_administrative_expense where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + ddl_client_adm.SelectedValue + "' and state_name='" + ddl_state_adm.SelectedValue + "' and unit_code='" + ddl_branch_adm.SelectedValue + "' and month='" + txt_date_adm.Text.Substring(0, 2) + "' and year='" + txt_date_adm.Text.Substring(3) + "' and (approve_flag='2' || approve_flag='1') ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                process_admin.DataSource = dt;
                process_admin.DataBind();
            }
            d4.con.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d4.con.Close();
        }
    }
    protected void gv_admin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            if (e.Row.Cells[1].Text.ToUpper().Equals("APPROVE BY FINANCE"))
            {

            }
            if (e.Row.Cells[8].Text.ToUpper().Equals("APPROVE BY ADMIN"))
            {
                e.Row.Cells[10].Text = "";
                e.Row.Cells[2].Text = "";

            }
        }
        e.Row.Cells[0].Visible = false;
    }
}