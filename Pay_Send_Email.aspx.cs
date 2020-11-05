using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Pay_Send_Email : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    public string Message = "0";
    string bajaj_state;
    string comp_name;
    string from_emailid;
    string password;
    string phon_no;
   // string filename;
    //string filename2;
    string designation;
    string record_not_found="YES";
    string filename1;
    string unit_code_email;
    string unit_name;
    string item_checklist;
     string head_email_id;
     string cc_emailid = "";
     int blank_mail = 0;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            client_code();

            attachment_gv.DataSource = null;
            attachment_gv.DataBind();

            attachment_gridview();
            code();
            pnl_branch.Visible = false;
           
            lnk_conformationmail.Visible = false;
            upload2.Visible = false;
            list_unitname.Visible = false;
           // btn_attendance.Visible = false;
            btn_add_emp.Visible = false;
            lbl_branck_emaillist.Visible = false;
            btn_process.Visible = false;
            btn_blank.Visible = false;
            add_mail.Visible = false;
        }
    }
    
    protected void client_code()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Distinct client_name, pay_client_master.client_code from pay_client_master INNER JOIN `pay_client_state_role_grade` ON `pay_client_master`.`COMP_CODE` = `pay_client_state_role_grade`.`COMP_CODE` AND `pay_client_master`.`client_code` = `pay_client_state_role_grade`.`client_code` where pay_client_master.comp_code='" + Session["comp_code"] + "' AND  pay_client_master.client_code in(select distinct(client_code) from pay_client_state_role_grade where  pay_client_master.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ") )  and client_active_close='0' ORDER BY client_code", d.con); 
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
            d.con.Close();
            ddl_client.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }
    protected void code()
    {
        ddl_client_attach.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Distinct client_name, pay_client_master.client_code from pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND pay_client_master.client_code = pay_client_state_role_grade.client_code where pay_client_master.comp_code='" + Session["comp_code"] + "'  and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_attach.DataSource = dt_item;
                ddl_client_attach.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_attach.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_attach.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_client_attach.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
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

    protected void fill_unit_emailnot_send()
    {
        d.con.Open();
        try
        {
            MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as unit_code FROM pay_branch_mail_details   left JOIN pay_attendance_muster ON pay_branch_mail_details.unit_code = pay_attendance_muster.unit_code AND pay_branch_mail_details.comp_code = pay_attendance_muster.comp_code 		inner join pay_unit_master on pay_branch_mail_details.comp_code=pay_unit_master.comp_code and pay_branch_mail_details.unit_code = pay_unit_master.unit_code WHERE pay_branch_mail_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_branch_mail_details.CLIENT_CODE='" + ddl_client.SelectedValue + "' AND pay_branch_mail_details.unit_code NOT IN (SELECT unit_code FROM pay_attendance_muster WHERE COMP_CODE='" + Session["comp_code"].ToString() + "')", d.con);
            DataTable ds = new DataTable();
            cmd.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                list_emailnotsend.DataSource = ds;
                list_emailnotsend.DataTextField = ds.Columns[0].ToString();
                list_emailnotsend.DataValueField = ds.Columns[0].ToString();
                list_emailnotsend.DataBind();

            }
            cmd.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void fill_unitname_listbox()
    {
        list_unitname.Items.Clear();
         string where = "where ";
         string where1 = "";
         if (ddl_client.SelectedValue != "Select")
         {
             where = where + "  pay_branch_mail_details.client_code = '" + ddl_client.SelectedValue + "' ";
             if (ddl_billing_state.SelectedValue != "Select")
             {
                 where = where + " AND pay_branch_mail_details.state = '" + ddl_billing_state.SelectedValue + "'";
             }
             if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0" && ddl_unitcode.SelectedValue != "")
             {
                 where = where + " AND pay_branch_mail_details.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                 where1 = "AND pay_unit_master.unit_code='" + ddl_unitcode.SelectedValue + "'";
             }

             d.con.Open();

             try
             {
                 list_unitname.Items.Clear();
                // MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT distinct(pay_unit_master.unit_code),  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'UNIT_NAME'  FROM  pay_unit_master  INNER JOIN pay_client_state_role_grade ON pay_unit_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND  pay_unit_master.client_code = pay_client_state_role_grade.client_code AND pay_unit_master.unit_code = pay_client_state_role_grade.unit_code AND pay_unit_master.state_name = pay_client_state_role_grade.state_name  WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code = '" + ddl_client.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_client_state_role_grade.EMP_CODE = '" + Session["LOGIN_ID"].ToString() + "' " + where1 + " AND pay_unit_master.unit_code NOT IN (SELECT unit_code FROM pay_branch_mail_details  " + where + " and pay_branch_mail_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') AND branch_status = 0 ", d.con);
                 MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT distinct(pay_unit_master.unit_code),  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'UNIT_NAME'  FROM  pay_unit_master  INNER JOIN pay_client_state_role_grade ON pay_unit_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND  pay_unit_master.client_code = pay_client_state_role_grade.client_code AND pay_unit_master.unit_code = pay_client_state_role_grade.unit_code AND pay_unit_master.state_name = pay_client_state_role_grade.state_name  WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code = '" + ddl_client.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_billing_state.SelectedValue + "' AND(pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "') " + where1 + " AND pay_unit_master.unit_code NOT IN (SELECT unit_code FROM pay_branch_mail_details  " + where + " and pay_branch_mail_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') AND branch_status = 0 ", d.con);
                 DataTable ds = new DataTable();
                 cmd.Fill(ds);
                 if (ds.Rows.Count > 0)
                 {
                     list_unitname.DataSource = ds;
                     list_unitname.DataTextField = ds.Columns[1].ToString();
                     list_unitname.DataValueField = ds.Columns[0].ToString();
                     list_unitname.DataBind();

                 }
                 cmd.Dispose();
                 d.con.Close();

             }
             catch (Exception ex) { throw ex; }
             finally
             {
                 d.con.Close();
             }

         }
    }
    protected void fill_branch_wise_gv()
    {
        string where = "where ";
        if (ddl_client.SelectedValue != "Select")
        {
            where = where + "pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND  pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' ";
            if (ddl_billing_state.SelectedValue != "Select")
            {
                where = where + "AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'";

                //WHERE  pay_client_state_role_grade.client_code = '4'  AND pay_client_state_role_grade.EMP_CODE = 'I00001'  AND pay_client_state_role_grade.state_name = 'Uttar Pradesh'

                //     WHERE  pay_client_state_role_grade.EMP_CODE = 'I00001'  AND pay_client_state_role_grade.client_code = '4'  AND pay_client_state_role_grade.state_name = 'Uttar Pradesh'
            }
            if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0" && ddl_unitcode.SelectedValue != "")
            {
                where = where + " AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }

            d.con1.Open();
            DataSet ds1 = new DataSet();
            try
            {
                gv_fullmonthot.DataSource = null;
                gv_fullmonthot.DataBind();
                string start_date_common = get_start_date();
                string end_date_common = get_end_date();
                int month = int.Parse(hidden_month.Value);
                int year = int.Parse(hidden_year.Value);
                if (start_date_common != "" && start_date_common != "1")
                {
                    month = --month;
                    if (month == 0) { month = 12; year = --year; }
                    where = where + " and (left_date >= str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }
                else
                {
                    start_date_common = "1";
                    where = where + " and (left_date >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || left_date is null) and joining_date <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y')";
                }

                MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'UNIT_CODE', pay_employee_master.emp_code, emp_name, pay_employee_master.Employee_type, DATE_FORMAT(LEFT_DATE, '%d/%m/%Y') AS 'left_date', LEFT_REASON, pay_attendance_muster.emp_code AS 'emp_code1' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_state_role_grade ON pay_client_state_role_grade.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_client_state_role_grade.comp_code = pay_unit_master.comp_code INNER JOIN pay_branch_mail_details ON pay_branch_mail_details.client_code = pay_unit_master.client_code AND pay_branch_mail_details.unit_code = pay_unit_master.unit_code left JOIN pay_attendance_muster ON pay_employee_master.emp_code = pay_attendance_muster.emp_code AND pay_attendance_muster.month = " + hidden_month.Value + "  AND pay_attendance_muster.year = " + hidden_year.Value + " " + where + " AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) group by pay_employee_master.emp_code ORDER BY 1", d.con1);
                
                
                adp1.Fill(ds1);
                gv_fullmonthot.DataSource = ds1.Tables[0];
                gv_fullmonthot.DataBind();
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (ddl_client.SelectedValue != "ALL")
        {
            ddl_unitcode.Items.Clear();
            list_unitname.Items.Clear();
            list_unitname.Visible = false;
            lbl_branck_emaillist.Visible = false;
            ddl_billing_state.Items.Clear();
            gv_fullmonthot.DataSource = null;
            gv_fullmonthot.DataBind();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND  STATE_NAME  in (select distinct(state_name) from pay_client_state_role_grade  INNER JOIN `pay_employee_master` ON `pay_employee_master`.`comp_code` = `pay_client_state_role_grade`.`comp_code` AND `pay_employee_master`.`emp_code` = `pay_client_state_role_grade`.`emp_code`  where pay_client_state_role_grade. COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.EMP_CODE IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND pay_client_state_role_grade.client_code='" + ddl_client.SelectedValue + "') order by 1", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_billing_state.DataSource = dt_item;
                    ddl_billing_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_billing_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_billing_state.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_billing_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_unitcode.Items.Clear();
        }
    }
    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fill_unitname_listbox();
        lbl_branck_emaillist.Visible = true;
        list_unitname.Visible = true;
        btn_process.Visible = true;
        btn_blank.Visible = true;
        hidden_month.Value = txttodate.Text.Substring(0, 2);
        hidden_year.Value = txttodate.Text.Substring(3);
        fill_branch_wise_gv();
        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "') and STATE_NAME='"+ddl_billing_state.SelectedValue+"' and branch_status = 0 ORDER BY STATE_NAME", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM pay_state_master WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_NAME`, '_', `UNIT_ADD1`) AS 'UNIT_NAME', `unit_code`, `flag` FROM `pay_unit_master` WHERE `comp_code` = '" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client.SelectedValue + "' AND pay_unit_master.`UNIT_CODE` IN (SELECT pay_unit_master.`UNIT_CODE` FROM `pay_client_state_role_grade` INNER JOIN `pay_unit_master` ON `pay_unit_master`.`comp_code` = `pay_client_state_role_grade`.`comp_code` AND `pay_client_state_role_grade`.`UNIT_CODE` = `pay_unit_master`.`UNIT_CODE` AND `pay_client_state_role_grade`.`client_code` = `pay_unit_master`.`client_code` AND `branch_status` = 0 WHERE pay_client_state_role_grade.`COMP_CODE` = '" + Session["comp_code"] + "' AND pay_client_state_role_grade.`STATE_NAME` = '" + ddl_billing_state.SelectedValue + "' AND (`pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND `branch_status` = 0 ORDER BY `STATE_NAME`", d.con);
       
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unitcode.DataSource = dt_item;
                ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                ddl_unitcode.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_unitcode.Items.Insert(0, "ALL");
            if (txttodate.Text.Length > 0)
            {
                hidden_month.Value = txttodate.Text.Substring(0, 2);
                hidden_year.Value = txttodate.Text.Substring(3);
            }
            else
            {
                pnl_branch.Visible = false;
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            
            Session["add_email_client"] = ddl_client.SelectedValue;
            Session["add_email_state"] = ddl_billing_state.SelectedValue;
            add_mail.Visible = true;
           // upload2.Visible = true;
            load_grdview();
        }
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
        string where = " and ";
        if (ddl_client.SelectedValue != "Select")
        {
            gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 1);

            where = where + " pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' ";
            if (ddl_billing_state.SelectedValue != "Select")
            {
                where = where + " AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'";
            }
            if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0")
            {
                where = where + " AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }

            d2.con.Open();

            try
            {
                string unt_code = "";
                // MySqlCommand cmdnew = new MySqlCommand("select head_email_id,pay_branch_mail_details.unit_code,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1)AS unit_name  from pay_branch_mail_details inner join  pay_unit_master on pay_branch_mail_details.unit_code=pay_unit_master.unit_code and pay_branch_mail_details.comp_code=pay_unit_master.comp_code   where pay_branch_mail_details.comp_code='" + Session["comp_code"].ToString() + "' and  pay_branch_mail_details.unit_code in (select unit_Code from pay_unit_master " + where + ")", d.con);
                MySqlCommand cmdnew = new MySqlCommand("SELECT DISTINCT(pay_attendance_muster.unit_code), head_email_id, cc_emailid, CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'unit_name' FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_branch_mail_details ON pay_attendance_muster.unit_code = pay_branch_mail_details.unit_code AND pay_attendance_muster.comp_code = pay_branch_mail_details.comp_code WHERE pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "' and pay_attendance_muster.month = " + hidden_month.Value + " and year = " + hidden_year.Value + "" + where, d2.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                System.Data.DataTable DataTable = new System.Data.DataTable();
                DataTable.Load(drnew);
                d2.con.Close();
                if (ddl_client.SelectedValue == "BAGICTM")
                {
                    foreach (DataRow row in DataTable.Rows)
                    {
                        head_email_id = row[1].ToString();
                        unit_code_email = row[0].ToString();
                        unt_code = unt_code + "'" + unit_code_email + "',";
                        unit_name = row[3].ToString();
                        unit_name.Replace(" ", "_");
                        string db_emp_code = d.getsinglestring("select unit_code from pay_send_mail_details where UNIT_CODE ='" + unit_code_email + "' AND month_year='" + txttodate.Text + "'");
                        if (db_emp_code == "")
                        {
                            d.operation("INSERT INTO pay_send_mail_details(attendance_shit,feedback_form,check_list,ot_list,flag,email_id,comp_code,client_code,unit_code,month_year) VALUES('NO','NO','NO','NO','0','','" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txttodate.Text + "')");
                        }
                    }
                    unit_code_email = unt_code.Substring(0, unt_code.Length - 1);
                    mail_all(unit_code_email);
                }
                else
                {
                    foreach (DataRow row in DataTable.Rows)
                    {
                        //string unit_code = "";
                        head_email_id = row[1].ToString();
                        unit_code_email = "'"+row[0].ToString()+"'";
                        unit_name = row[3].ToString();
                        unit_name.Replace(" ", "_");
                        hidden_month.Value = txttodate.Text.Substring(0, 2);
                        hidden_year.Value = txttodate.Text.Substring(3);
                        cc_emailid = row[2].ToString();
                        string db_emp_code = d.getsinglestring("select unit_code from pay_send_mail_details where UNIT_CODE =" + unit_code_email + " AND month_year='" + txttodate.Text + "'");

                        if (db_emp_code == "")
                    {
                        d.operation("INSERT INTO pay_send_mail_details(attendance_shit,feedback_form,check_list,ot_list,flag,email_id,comp_code,client_code,unit_code,month_year) VALUES('NO','NO','NO','NO','0','','" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txttodate.Text + "')");
                    }
                        mail_all(unit_code_email);
                }
            }
            }
            catch (Exception ex) { throw ex; }
            finally { d2.con.Close(); }
        }
    }
    protected void mail_all(string unt_code)
    {
        if (unt_code.ToString() != "")
        {


            if (ddl_client.SelectedValue == "HDFC")
            {
                attendance_sheet(unit_code_email);
                btn_otdetails_Click(null, null);
                btn_mailsend_click(null, null);
                d.operation("update pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='NO',ot_list='YES',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else if (ddl_client.SelectedValue == "UTKARSH")
            {
                attendance_sheet(unit_code_email);
                btn_checklist_Click(null, null);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='YES',ot_list='NO',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else if (ddl_client.SelectedValue == "RLIC HK")
            {
                attendance_sheet(unit_code_email);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='NO',ot_list='NO',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else if (ddl_client.SelectedValue == "SUD")
            {
                attendance_sheet(unit_code_email);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='NO',ot_list='NO',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else if (ddl_client.SelectedValue == "BAG" || ddl_client.SelectedValue == "BG" || ddl_client.SelectedValue == "BALIC SG" || ddl_client.SelectedValue == "BALIC HK")
            {
                attendance_sheet(unit_code_email);
                btn_feedbabk_Click(null, null);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='NO',ot_list='NO',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else if (ddl_client.SelectedValue == "4")
            {
                attendance_sheet(unit_code_email);
                btn_feedbabk_bfl_Click(unit_code_email);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='YES',check_list='NO',ot_list='NO',flag='1',email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
            else
            {
                attendance_sheet(unit_code_email);
                btn_mailsend_click(null, null);
                d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='NO',check_list='NO',ot_list='YES',flag='1', email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
            }
        }
    }
    protected void attendance_sheet(string unit_code)
    {

            //string flag = d.getsinglestring("select flag from pay_send_mail_details where comp_code='" + Session["comp_code"].ToString() + "' and  unit_code='" + ddl_unitcode.SelectedValue + "' and client_code='" + ddl_client.SelectedValue + "' and month_year='"+txttodate.Text+"'");

            //if (flag == "0" || flag == "1" || flag == "")
            //{
                int counter = 0;
                int counter1 = 0;
                 if (blank_mail == 1)
                 {
                     counter = 100;
                 }
                 else
                 {
                     counter = 1;
                     counter1 = 3;
                 }
                
                string sql = "", pay_attendance_muster = "pay_attendance_muster";
                string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-01','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-01'), '%D %b %Y'))) as fromtodate";
                if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
                string start_date_common = get_start_date();
                string end_date = get_end_date();
                string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
                int i = 0;

                int month = int.Parse(hidden_month.Value);
                int year = int.Parse(hidden_year.Value);
                if (start_date_common != "" && start_date_common != "1")
                {
                    month = --month;
                    if (month == 0) { month = 12; year = --year; }
                }
                else
                {
                    start_date_common = "1";
                }


                if (ot_applicable == "1")
                {
                    if (start_date_common != "" && start_date_common != "1")
                    {
                        daterange = "concat(upper(DATE_FORMAT(str_to_date('" + (int.Parse(hidden_month.Value) - 1 == 0 ? (int.Parse(hidden_year.Value) - 1).ToString():hidden_year.Value) + "-" + (int.Parse(hidden_month.Value) - 1 == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                        if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, counter, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, counter1, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code  AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "' INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code   left JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE  INNER JOIN pay_company_master ON pay_unit_master.comp_code = pay_company_master.comp_code  INNER JOIN pay_ot_muster ON  pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code  AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE  AND pay_attendance_muster.MONTH = pay_ot_muster.MONTH  AND pay_attendance_muster.YEAR = pay_ot_muster.YEAR  AND pay_ot_muster.month =  '" + hidden_month.Value + "'  LEFT JOIN pay_attendance_muster t2   ON " + year + "= t2.year AND pay_company_master.COMP_CODE = t2.COMP_CODE  AND pay_unit_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE  AND t2.month ='" + month + "' LEFT OUTER JOIN pay_ot_muster t3   ON " + year + " = t3.YEAR  AND pay_unit_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_company_master.COMP_CODE = t3.COMP_CODE   AND t3.month = '" + month + "' WHERE pay_company_master.comp_code =  '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.unit_code in( " + unit_code_email + ") group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31,OT_DAY01 , OT_DAY02 , OT_DAY03 , OT_DAY04 , OT_DAY05 , OT_DAY06 , OT_DAY07 , OT_DAY08 , OT_DAY09 , OT_DAY10 , OT_DAY11 , OT_DAY12 , OT_DAY13 , OT_DAY14 , OT_DAY15 , OT_DAY16 , OT_DAY17 , OT_DAY18 , OT_DAY19 , OT_DAY20 , OT_DAY21 , OT_DAY22 , OT_DAY23 , OT_DAY24 , OT_DAY25 , OT_DAY26 , OT_DAY27 , OT_DAY28 , OT_DAY29 , OT_DAY30 , OT_DAY31,TOT_OT, pay_attendance_muster.tot_days_present,pay_attendance_muster.tot_days_absent AS 'absent', DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) AS 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code INNER JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in (" + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }

                    i = 1;
                }
                else
                {
                    if (start_date_common != "" && start_date_common != "1")
                    {
                       // daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + (int.Parse(hidden_month.Value) - 1) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                        daterange = "concat(upper(DATE_FORMAT(str_to_date('" + (int.Parse(hidden_month.Value) - 1 == 0 ? (int.Parse(hidden_year.Value) - 1).ToString() : hidden_year.Value) + "-" + (int.Parse(hidden_month.Value) - 1 == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                        if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, counter, 1) + " pay_attendance_muster.tot_days_present,  pay_attendance_muster.tot_days_absent as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code left join pay_attendance_muster t2 on  t2.year=" + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + " and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else
                    {
                        if (blank_mail == 1)
                        {
                    int days = DateTime.DaysInMonth(year, month);

                    if (days == 28)
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code)  as client_name, pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC,  (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case  WHEN DAY01 = 'A' THEN '' WHEN DAY01 = 'P' THEN '' WHEN DAY01 = 'W' THEN '' WHEN DAY01 = 'PH' THEN ''  WHEN DAY01 = 'HD' THEN '' else DAY01 end as DAY01, case  WHEN DAY02 = 'A' THEN '' WHEN DAY02 = 'P' THEN '' WHEN DAY02 = 'W' THEN '' WHEN DAY02 = 'PH' THEN '' WHEN DAY02 = 'HD' THEN '' else DAY02 end as DAY02, case  WHEN DAY03 = 'A' THEN '' WHEN DAY03 = 'P' THEN '' WHEN DAY03 = 'W' THEN '' WHEN DAY03 = 'PH' THEN '' WHEN DAY03 = 'HD' THEN '' else DAY03 end as DAY03, case  WHEN DAY04 = 'A' THEN '' WHEN DAY04 = 'P' THEN '' WHEN DAY04 = 'W' THEN '' WHEN DAY04 = 'PH' THEN '' WHEN DAY04 = 'HD' THEN '' else DAY04 end as DAY04, case  WHEN DAY05 = 'A' THEN '' WHEN DAY05 = 'P' THEN '' WHEN DAY05 = 'W' THEN '' WHEN DAY05 = 'PH' THEN '' WHEN DAY05 = 'HD' THEN '' else DAY05 end as DAY05, case  WHEN DAY06 = 'A' THEN '' WHEN DAY06 = 'P' THEN '' WHEN DAY06 = 'W' THEN '' WHEN DAY06 = 'PH' THEN '' WHEN DAY06 = 'HD' THEN '' else DAY06 end as DAY06, case  WHEN DAY07 = 'A' THEN '' WHEN DAY07 = 'P' THEN '' WHEN DAY07 = 'W' THEN '' WHEN DAY07 = 'PH' THEN '' WHEN DAY07 = 'HD' THEN '' else DAY07 end as DAY07, case  WHEN DAY08 = 'A' THEN '' WHEN DAY08 = 'P' THEN '' WHEN DAY08 = 'W' THEN '' WHEN DAY08 = 'PH' THEN '' WHEN DAY08 = 'HD' THEN '' else DAY08 end as DAY08, case  WHEN DAY09 = 'A' THEN '' WHEN DAY09 = 'P' THEN '' WHEN DAY09 = 'W' THEN '' WHEN DAY09 = 'PH' THEN '' WHEN DAY09 = 'HD' THEN '' else DAY09 end as DAY09, case  WHEN DAY10 = 'A' THEN '' WHEN DAY10 = 'P' THEN '' WHEN DAY10 = 'W' THEN '' WHEN DAY10 = 'PH' THEN '' WHEN DAY10 = 'HD' THEN '' else DAY10 end as DAY10, case  WHEN DAY11 = 'A' THEN '' WHEN DAY11 = 'P' THEN '' WHEN DAY11 = 'W' THEN '' WHEN DAY11 = 'PH' THEN '' WHEN DAY11 = 'HD' THEN '' else DAY11 end as DAY11, case  WHEN DAY12 = 'A' THEN '' WHEN DAY12 = 'P' THEN '' WHEN DAY12 = 'W' THEN '' WHEN DAY12 = 'PH' THEN '' WHEN DAY12 = 'HD' THEN '' else DAY12 end as DAY12, case  WHEN DAY13 = 'A' THEN '' WHEN DAY13 = 'P' THEN '' WHEN DAY13 = 'W' THEN '' WHEN DAY13 = 'PH' THEN '' WHEN DAY13 = 'HD' THEN '' else DAY13 end as DAY13, case  WHEN DAY14 = 'A' THEN '' WHEN DAY14 = 'P' THEN '' WHEN DAY14 = 'W' THEN '' WHEN DAY14 = 'PH' THEN '' WHEN DAY14 = 'HD' THEN '' else DAY14 end as DAY14, case  WHEN DAY15 = 'A' THEN '' WHEN DAY15 = 'P' THEN '' WHEN DAY15 = 'W' THEN '' WHEN DAY15 = 'PH' THEN '' WHEN DAY15 = 'HD' THEN '' else DAY15 end as DAY15, case  WHEN DAY16 = 'A' THEN '' WHEN DAY16 = 'P' THEN '' WHEN DAY16 = 'W' THEN '' WHEN DAY16 = 'PH' THEN '' WHEN DAY16 = 'HD' THEN '' else DAY16 end as DAY16, case WHEN DAY17 = 'A' THEN '' WHEN DAY17 = 'P' THEN '' WHEN DAY17 = 'W' THEN '' WHEN DAY17 = 'PH' THEN ''  WHEN DAY17 = 'HD' THEN '' else DAY17 end as DAY17, case  WHEN DAY18 = 'A' THEN '' WHEN DAY18 = 'P' THEN '' WHEN DAY18 = 'W' THEN '' WHEN DAY18 = 'PH' THEN '' WHEN DAY18 = 'HD' THEN '' else DAY18 end as DAY18, case  WHEN DAY19 = 'A' THEN '' WHEN DAY19 = 'P' THEN '' WHEN DAY19 = 'W' THEN '' WHEN DAY19 = 'PH' THEN '' WHEN DAY19 = 'HD' THEN '' else DAY19 end as DAY19, case  WHEN DAY20 = 'A' THEN '' WHEN DAY20 = 'P' THEN '' WHEN DAY20 = 'W' THEN '' WHEN DAY20 = 'PH' THEN '' WHEN DAY20 = 'HD' THEN '' else DAY20 end as DAY20, case  WHEN DAY21 = 'A' THEN '' WHEN DAY21 = 'P' THEN '' WHEN DAY21 = 'W' THEN '' WHEN DAY21 = 'PH' THEN '' WHEN DAY21 = 'HD' THEN '' else DAY21 end as DAY21, case  WHEN DAY22 = 'A' THEN '' WHEN DAY22 = 'P' THEN '' WHEN DAY22 = 'W' THEN '' WHEN DAY22 = 'PH' THEN '' WHEN DAY22 = 'HD' THEN '' else DAY22 end as DAY22, case  WHEN DAY23 = 'A' THEN '' WHEN DAY23 = 'P' THEN '' WHEN DAY23 = 'W' THEN '' WHEN DAY23 = 'PH' THEN '' WHEN DAY23 = 'HD' THEN '' else DAY23 end as DAY23,  case WHEN DAY24 = 'A' THEN '' WHEN DAY24 = 'P' THEN '' WHEN DAY24 = 'W' THEN '' WHEN DAY24 = 'PH' THEN '' WHEN DAY24 = 'HD' THEN '' else DAY24 end as DAY24, case  WHEN DAY25 = 'A' THEN '' WHEN DAY25 = 'P' THEN '' WHEN DAY25 = 'W' THEN '' WHEN DAY25 = 'PH' THEN '' WHEN DAY25 = 'HD' THEN '' else DAY25 end as DAY25, case  WHEN DAY26 = 'A' THEN '' WHEN DAY26 = 'P' THEN '' WHEN DAY26 = 'W' THEN '' WHEN DAY26 = 'PH' THEN '' WHEN DAY26 = 'HD' THEN '' else DAY26 end as DAY26, case  WHEN DAY27 = 'A' THEN '' WHEN DAY27 = 'P' THEN '' WHEN DAY27 = 'W' THEN '' WHEN DAY27 = 'PH' THEN ''  WHEN DAY27 = 'HD' THEN '' else DAY27 end as DAY27, case  WHEN DAY28 = 'A' THEN '' WHEN DAY28 = 'P' THEN '' WHEN DAY28 = 'W' THEN '' WHEN DAY28 = 'PH' THEN ''  WHEN DAY28 = 'HD' THEN '' else DAY28 end as DAY28, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else if (days == 29)
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code)  as client_name, pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC,  (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case  WHEN DAY01 = 'A' THEN '' WHEN DAY01 = 'P' THEN '' WHEN DAY01 = 'W' THEN '' WHEN DAY01 = 'PH' THEN ''  WHEN DAY01 = 'HD' THEN '' else DAY01 end as DAY01, case  WHEN DAY02 = 'A' THEN '' WHEN DAY02 = 'P' THEN '' WHEN DAY02 = 'W' THEN '' WHEN DAY02 = 'PH' THEN '' WHEN DAY02 = 'HD' THEN '' else DAY02 end as DAY02, case  WHEN DAY03 = 'A' THEN '' WHEN DAY03 = 'P' THEN '' WHEN DAY03 = 'W' THEN '' WHEN DAY03 = 'PH' THEN '' WHEN DAY03 = 'HD' THEN '' else DAY03 end as DAY03, case  WHEN DAY04 = 'A' THEN '' WHEN DAY04 = 'P' THEN '' WHEN DAY04 = 'W' THEN '' WHEN DAY04 = 'PH' THEN '' WHEN DAY04 = 'HD' THEN '' else DAY04 end as DAY04, case  WHEN DAY05 = 'A' THEN '' WHEN DAY05 = 'P' THEN '' WHEN DAY05 = 'W' THEN '' WHEN DAY05 = 'PH' THEN '' WHEN DAY05 = 'HD' THEN '' else DAY05 end as DAY05, case  WHEN DAY06 = 'A' THEN '' WHEN DAY06 = 'P' THEN '' WHEN DAY06 = 'W' THEN '' WHEN DAY06 = 'PH' THEN '' WHEN DAY06 = 'HD' THEN '' else DAY06 end as DAY06, case  WHEN DAY07 = 'A' THEN '' WHEN DAY07 = 'P' THEN '' WHEN DAY07 = 'W' THEN '' WHEN DAY07 = 'PH' THEN '' WHEN DAY07 = 'HD' THEN '' else DAY07 end as DAY07, case  WHEN DAY08 = 'A' THEN '' WHEN DAY08 = 'P' THEN '' WHEN DAY08 = 'W' THEN '' WHEN DAY08 = 'PH' THEN '' WHEN DAY08 = 'HD' THEN '' else DAY08 end as DAY08, case  WHEN DAY09 = 'A' THEN '' WHEN DAY09 = 'P' THEN '' WHEN DAY09 = 'W' THEN '' WHEN DAY09 = 'PH' THEN '' WHEN DAY09 = 'HD' THEN '' else DAY09 end as DAY09, case  WHEN DAY10 = 'A' THEN '' WHEN DAY10 = 'P' THEN '' WHEN DAY10 = 'W' THEN '' WHEN DAY10 = 'PH' THEN '' WHEN DAY10 = 'HD' THEN '' else DAY10 end as DAY10, case  WHEN DAY11 = 'A' THEN '' WHEN DAY11 = 'P' THEN '' WHEN DAY11 = 'W' THEN '' WHEN DAY11 = 'PH' THEN '' WHEN DAY11 = 'HD' THEN '' else DAY11 end as DAY11, case  WHEN DAY12 = 'A' THEN '' WHEN DAY12 = 'P' THEN '' WHEN DAY12 = 'W' THEN '' WHEN DAY12 = 'PH' THEN '' WHEN DAY12 = 'HD' THEN '' else DAY12 end as DAY12, case  WHEN DAY13 = 'A' THEN '' WHEN DAY13 = 'P' THEN '' WHEN DAY13 = 'W' THEN '' WHEN DAY13 = 'PH' THEN '' WHEN DAY13 = 'HD' THEN '' else DAY13 end as DAY13, case  WHEN DAY14 = 'A' THEN '' WHEN DAY14 = 'P' THEN '' WHEN DAY14 = 'W' THEN '' WHEN DAY14 = 'PH' THEN '' WHEN DAY14 = 'HD' THEN '' else DAY14 end as DAY14, case  WHEN DAY15 = 'A' THEN '' WHEN DAY15 = 'P' THEN '' WHEN DAY15 = 'W' THEN '' WHEN DAY15 = 'PH' THEN '' WHEN DAY15 = 'HD' THEN '' else DAY15 end as DAY15, case  WHEN DAY16 = 'A' THEN '' WHEN DAY16 = 'P' THEN '' WHEN DAY16 = 'W' THEN '' WHEN DAY16 = 'PH' THEN '' WHEN DAY16 = 'HD' THEN '' else DAY16 end as DAY16, case WHEN DAY17 = 'A' THEN '' WHEN DAY17 = 'P' THEN '' WHEN DAY17 = 'W' THEN '' WHEN DAY17 = 'PH' THEN ''  WHEN DAY17 = 'HD' THEN '' else DAY17 end as DAY17, case  WHEN DAY18 = 'A' THEN '' WHEN DAY18 = 'P' THEN '' WHEN DAY18 = 'W' THEN '' WHEN DAY18 = 'PH' THEN '' WHEN DAY18 = 'HD' THEN '' else DAY18 end as DAY18, case  WHEN DAY19 = 'A' THEN '' WHEN DAY19 = 'P' THEN '' WHEN DAY19 = 'W' THEN '' WHEN DAY19 = 'PH' THEN '' WHEN DAY19 = 'HD' THEN '' else DAY19 end as DAY19, case  WHEN DAY20 = 'A' THEN '' WHEN DAY20 = 'P' THEN '' WHEN DAY20 = 'W' THEN '' WHEN DAY20 = 'PH' THEN '' WHEN DAY20 = 'HD' THEN '' else DAY20 end as DAY20, case  WHEN DAY21 = 'A' THEN '' WHEN DAY21 = 'P' THEN '' WHEN DAY21 = 'W' THEN '' WHEN DAY21 = 'PH' THEN '' WHEN DAY21 = 'HD' THEN '' else DAY21 end as DAY21, case  WHEN DAY22 = 'A' THEN '' WHEN DAY22 = 'P' THEN '' WHEN DAY22 = 'W' THEN '' WHEN DAY22 = 'PH' THEN '' WHEN DAY22 = 'HD' THEN '' else DAY22 end as DAY22, case  WHEN DAY23 = 'A' THEN '' WHEN DAY23 = 'P' THEN '' WHEN DAY23 = 'W' THEN '' WHEN DAY23 = 'PH' THEN '' WHEN DAY23 = 'HD' THEN '' else DAY23 end as DAY23,  case WHEN DAY24 = 'A' THEN '' WHEN DAY24 = 'P' THEN '' WHEN DAY24 = 'W' THEN '' WHEN DAY24 = 'PH' THEN '' WHEN DAY24 = 'HD' THEN '' else DAY24 end as DAY24, case  WHEN DAY25 = 'A' THEN '' WHEN DAY25 = 'P' THEN '' WHEN DAY25 = 'W' THEN '' WHEN DAY25 = 'PH' THEN '' WHEN DAY25 = 'HD' THEN '' else DAY25 end as DAY25, case  WHEN DAY26 = 'A' THEN '' WHEN DAY26 = 'P' THEN '' WHEN DAY26 = 'W' THEN '' WHEN DAY26 = 'PH' THEN '' WHEN DAY26 = 'HD' THEN '' else DAY26 end as DAY26, case  WHEN DAY27 = 'A' THEN '' WHEN DAY27 = 'P' THEN '' WHEN DAY27 = 'W' THEN '' WHEN DAY27 = 'PH' THEN ''  WHEN DAY27 = 'HD' THEN '' else DAY27 end as DAY27, case  WHEN DAY28 = 'A' THEN '' WHEN DAY28 = 'P' THEN '' WHEN DAY28 = 'W' THEN '' WHEN DAY28 = 'PH' THEN ''  WHEN DAY28 = 'HD' THEN '' else DAY28 end as DAY28, case  WHEN DAY29 = 'A' THEN '' WHEN DAY29 = 'P' THEN '' WHEN DAY29 = 'W' THEN '' WHEN DAY29 = 'PH' THEN ''  WHEN DAY29 = 'HD' THEN '' else DAY29 end as DAY29,  tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code)  as client_name, pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC,  (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case  WHEN DAY01 = 'A' THEN '' WHEN DAY01 = 'P' THEN '' WHEN DAY01 = 'W' THEN '' WHEN DAY01 = 'PH' THEN ''  WHEN DAY01 = 'HD' THEN '' else DAY01 end as DAY01, case  WHEN DAY02 = 'A' THEN '' WHEN DAY02 = 'P' THEN '' WHEN DAY02 = 'W' THEN '' WHEN DAY02 = 'PH' THEN '' WHEN DAY02 = 'HD' THEN '' else DAY02 end as DAY02, case  WHEN DAY03 = 'A' THEN '' WHEN DAY03 = 'P' THEN '' WHEN DAY03 = 'W' THEN '' WHEN DAY03 = 'PH' THEN '' WHEN DAY03 = 'HD' THEN '' else DAY03 end as DAY03, case  WHEN DAY04 = 'A' THEN '' WHEN DAY04 = 'P' THEN '' WHEN DAY04 = 'W' THEN '' WHEN DAY04 = 'PH' THEN '' WHEN DAY04 = 'HD' THEN '' else DAY04 end as DAY04, case  WHEN DAY05 = 'A' THEN '' WHEN DAY05 = 'P' THEN '' WHEN DAY05 = 'W' THEN '' WHEN DAY05 = 'PH' THEN '' WHEN DAY05 = 'HD' THEN '' else DAY05 end as DAY05, case  WHEN DAY06 = 'A' THEN '' WHEN DAY06 = 'P' THEN '' WHEN DAY06 = 'W' THEN '' WHEN DAY06 = 'PH' THEN '' WHEN DAY06 = 'HD' THEN '' else DAY06 end as DAY06, case  WHEN DAY07 = 'A' THEN '' WHEN DAY07 = 'P' THEN '' WHEN DAY07 = 'W' THEN '' WHEN DAY07 = 'PH' THEN '' WHEN DAY07 = 'HD' THEN '' else DAY07 end as DAY07, case  WHEN DAY08 = 'A' THEN '' WHEN DAY08 = 'P' THEN '' WHEN DAY08 = 'W' THEN '' WHEN DAY08 = 'PH' THEN '' WHEN DAY08 = 'HD' THEN '' else DAY08 end as DAY08, case  WHEN DAY09 = 'A' THEN '' WHEN DAY09 = 'P' THEN '' WHEN DAY09 = 'W' THEN '' WHEN DAY09 = 'PH' THEN '' WHEN DAY09 = 'HD' THEN '' else DAY09 end as DAY09, case  WHEN DAY10 = 'A' THEN '' WHEN DAY10 = 'P' THEN '' WHEN DAY10 = 'W' THEN '' WHEN DAY10 = 'PH' THEN '' WHEN DAY10 = 'HD' THEN '' else DAY10 end as DAY10, case  WHEN DAY11 = 'A' THEN '' WHEN DAY11 = 'P' THEN '' WHEN DAY11 = 'W' THEN '' WHEN DAY11 = 'PH' THEN '' WHEN DAY11 = 'HD' THEN '' else DAY11 end as DAY11, case  WHEN DAY12 = 'A' THEN '' WHEN DAY12 = 'P' THEN '' WHEN DAY12 = 'W' THEN '' WHEN DAY12 = 'PH' THEN '' WHEN DAY12 = 'HD' THEN '' else DAY12 end as DAY12, case  WHEN DAY13 = 'A' THEN '' WHEN DAY13 = 'P' THEN '' WHEN DAY13 = 'W' THEN '' WHEN DAY13 = 'PH' THEN '' WHEN DAY13 = 'HD' THEN '' else DAY13 end as DAY13, case  WHEN DAY14 = 'A' THEN '' WHEN DAY14 = 'P' THEN '' WHEN DAY14 = 'W' THEN '' WHEN DAY14 = 'PH' THEN '' WHEN DAY14 = 'HD' THEN '' else DAY14 end as DAY14, case  WHEN DAY15 = 'A' THEN '' WHEN DAY15 = 'P' THEN '' WHEN DAY15 = 'W' THEN '' WHEN DAY15 = 'PH' THEN '' WHEN DAY15 = 'HD' THEN '' else DAY15 end as DAY15, case  WHEN DAY16 = 'A' THEN '' WHEN DAY16 = 'P' THEN '' WHEN DAY16 = 'W' THEN '' WHEN DAY16 = 'PH' THEN '' WHEN DAY16 = 'HD' THEN '' else DAY16 end as DAY16, case WHEN DAY17 = 'A' THEN '' WHEN DAY17 = 'P' THEN '' WHEN DAY17 = 'W' THEN '' WHEN DAY17 = 'PH' THEN ''  WHEN DAY17 = 'HD' THEN '' else DAY17 end as DAY17, case  WHEN DAY18 = 'A' THEN '' WHEN DAY18 = 'P' THEN '' WHEN DAY18 = 'W' THEN '' WHEN DAY18 = 'PH' THEN '' WHEN DAY18 = 'HD' THEN '' else DAY18 end as DAY18, case  WHEN DAY19 = 'A' THEN '' WHEN DAY19 = 'P' THEN '' WHEN DAY19 = 'W' THEN '' WHEN DAY19 = 'PH' THEN '' WHEN DAY19 = 'HD' THEN '' else DAY19 end as DAY19, case  WHEN DAY20 = 'A' THEN '' WHEN DAY20 = 'P' THEN '' WHEN DAY20 = 'W' THEN '' WHEN DAY20 = 'PH' THEN '' WHEN DAY20 = 'HD' THEN '' else DAY20 end as DAY20, case  WHEN DAY21 = 'A' THEN '' WHEN DAY21 = 'P' THEN '' WHEN DAY21 = 'W' THEN '' WHEN DAY21 = 'PH' THEN '' WHEN DAY21 = 'HD' THEN '' else DAY21 end as DAY21, case  WHEN DAY22 = 'A' THEN '' WHEN DAY22 = 'P' THEN '' WHEN DAY22 = 'W' THEN '' WHEN DAY22 = 'PH' THEN '' WHEN DAY22 = 'HD' THEN '' else DAY22 end as DAY22, case  WHEN DAY23 = 'A' THEN '' WHEN DAY23 = 'P' THEN '' WHEN DAY23 = 'W' THEN '' WHEN DAY23 = 'PH' THEN '' WHEN DAY23 = 'HD' THEN '' else DAY23 end as DAY23,  case WHEN DAY24 = 'A' THEN '' WHEN DAY24 = 'P' THEN '' WHEN DAY24 = 'W' THEN '' WHEN DAY24 = 'PH' THEN '' WHEN DAY24 = 'HD' THEN '' else DAY24 end as DAY24, case  WHEN DAY25 = 'A' THEN '' WHEN DAY25 = 'P' THEN '' WHEN DAY25 = 'W' THEN '' WHEN DAY25 = 'PH' THEN '' WHEN DAY25 = 'HD' THEN '' else DAY25 end as DAY25, case  WHEN DAY26 = 'A' THEN '' WHEN DAY26 = 'P' THEN '' WHEN DAY26 = 'W' THEN '' WHEN DAY26 = 'PH' THEN '' WHEN DAY26 = 'HD' THEN '' else DAY26 end as DAY26, case  WHEN DAY27 = 'A' THEN '' WHEN DAY27 = 'P' THEN '' WHEN DAY27 = 'W' THEN '' WHEN DAY27 = 'PH' THEN ''  WHEN DAY27 = 'HD' THEN '' else DAY27 end as DAY27, case  WHEN DAY28 = 'A' THEN '' WHEN DAY28 = 'P' THEN '' WHEN DAY28 = 'W' THEN '' WHEN DAY28 = 'PH' THEN ''  WHEN DAY28 = 'HD' THEN '' else DAY28 end as DAY28, case  WHEN DAY29 = 'A' THEN '' WHEN DAY29 = 'P' THEN '' WHEN DAY29 = 'W' THEN '' WHEN DAY29 = 'PH' THEN ''  WHEN DAY29 = 'HD' THEN '' else DAY29 end as DAY29, case  WHEN DAY30 = 'A' THEN '' WHEN DAY30 = 'P' THEN '' WHEN DAY30 = 'W' THEN '' WHEN DAY30 = 'PH' THEN ''  WHEN DAY30 = 'HD' THEN '' else DAY30 end as DAY30, case  WHEN DAY31 = 'A' THEN '' WHEN DAY31 = 'P' THEN '' WHEN DAY31 = 'W' THEN '' WHEN DAY31 = 'PH' THEN ''  WHEN DAY31 = 'HD' THEN '' else DAY31 end as DAY31, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                        //sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name, pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case  WHEN DAY01 = 'A' THEN '' WHEN DAY01 = 'P' THEN '' WHEN DAY01 = 'W' THEN '' WHEN DAY01 = 'PH' THEN '' else DAY01 end as DAY01, case  WHEN DAY02 = 'A' THEN '' WHEN DAY02 = 'P' THEN '' WHEN DAY02 = 'W' THEN '' WHEN DAY02 = 'PH' THEN '' else DAY02 end as DAY02, case  WHEN DAY03 = 'A' THEN '' WHEN DAY03 = 'P' THEN '' WHEN DAY03 = 'W' THEN '' WHEN DAY03 = 'PH' THEN '' else DAY03 end as DAY03, case  WHEN DAY04 = 'A' THEN '' WHEN DAY04 = 'P' THEN '' WHEN DAY04 = 'W' THEN '' WHEN DAY04 = 'PH' THEN '' else DAY04 end as DAY04, case  WHEN DAY05 = 'A' THEN '' WHEN DAY05 = 'P' THEN '' WHEN DAY05 = 'W' THEN '' WHEN DAY05 = 'PH' THEN '' else DAY05 end as DAY05, case  WHEN DAY06 = 'A' THEN '' WHEN DAY06 = 'P' THEN '' WHEN DAY06 = 'W' THEN '' WHEN DAY06 = 'PH' THEN '' else DAY06 end as DAY06, case  WHEN DAY07 = 'A' THEN '' WHEN DAY07 = 'P' THEN '' WHEN DAY07 = 'W' THEN '' WHEN DAY07 = 'PH' THEN '' else DAY07 end as DAY07, case  WHEN DAY08 = 'A' THEN '' WHEN DAY08 = 'P' THEN '' WHEN DAY08 = 'W' THEN '' WHEN DAY08 = 'PH' THEN '' else DAY08 end as DAY08, case  WHEN DAY09 = 'A' THEN '' WHEN DAY09 = 'P' THEN '' WHEN DAY09 = 'W' THEN '' WHEN DAY09 = 'PH' THEN '' else DAY09 end as DAY09, case  WHEN DAY10 = 'A' THEN '' WHEN DAY10 = 'P' THEN '' WHEN DAY10 = 'W' THEN '' WHEN DAY10 = 'PH' THEN '' else DAY10 end as DAY10, case  WHEN DAY11 = 'A' THEN '' WHEN DAY11 = 'P' THEN '' WHEN DAY11 = 'W' THEN '' WHEN DAY11 = 'PH' THEN '' else DAY11 end as DAY11, case  WHEN DAY12 = 'A' THEN '' WHEN DAY12 = 'P' THEN '' WHEN DAY12 = 'W' THEN '' WHEN DAY12 = 'PH' THEN '' else DAY12 end as DAY12, case  WHEN DAY13 = 'A' THEN '' WHEN DAY13 = 'P' THEN '' WHEN DAY13 = 'W' THEN '' WHEN DAY13 = 'PH' THEN '' else DAY13 end as DAY13, case  WHEN DAY14 = 'A' THEN '' WHEN DAY14 = 'P' THEN '' WHEN DAY14 = 'W' THEN '' WHEN DAY14 = 'PH' THEN '' else DAY14 end as DAY14, case  WHEN DAY15 = 'A' THEN '' WHEN DAY15 = 'P' THEN '' WHEN DAY15 = 'W' THEN '' WHEN DAY15 = 'PH' THEN '' else DAY15 end as DAY15, case  WHEN DAY16 = 'A' THEN '' WHEN DAY16 = 'P' THEN '' WHEN DAY16 = 'W' THEN '' WHEN DAY16 = 'PH' THEN '' else DAY16 end as DAY16, case WHEN DAY17 = 'A' THEN '' WHEN DAY17 = 'P' THEN '' WHEN DAY17 = 'W' THEN '' WHEN DAY17 = 'PH' THEN '' else DAY17 end as DAY17, case  WHEN DAY18 = 'A' THEN '' WHEN DAY18 = 'P' THEN '' WHEN DAY18 = 'W' THEN '' WHEN DAY18 = 'PH' THEN '' else DAY18 end as DAY18, case  WHEN DAY19 = 'A' THEN '' WHEN DAY19 = 'P' THEN '' WHEN DAY19 = 'W' THEN '' WHEN DAY19 = 'PH' THEN '' else DAY19 end as DAY19, case  WHEN DAY20 = 'A' THEN '' WHEN DAY20 = 'P' THEN '' WHEN DAY20 = 'W' THEN '' WHEN DAY20 = 'PH' THEN '' else DAY20 end as DAY20, case  WHEN DAY21 = 'A' THEN '' WHEN DAY21 = 'P' THEN '' WHEN DAY21 = 'W' THEN '' WHEN DAY21 = 'PH' THEN '' else DAY21 end as DAY21, case  WHEN DAY22 = 'A' THEN '' WHEN DAY22 = 'P' THEN '' WHEN DAY22 = 'W' THEN '' WHEN DAY22 = 'PH' THEN '' else DAY22 end as DAY22, case  WHEN DAY23 = 'A' THEN '' WHEN DAY23 = 'P' THEN '' WHEN DAY23 = 'W' THEN '' WHEN DAY23 = 'PH' THEN ''else DAY23 end as DAY23, case WHEN DAY24 = 'A' THEN '' WHEN DAY24 = 'P' THEN '' WHEN DAY24 = 'W' THEN'' WHEN DAY24 = 'PH' THEN '' else DAY24 end as DAY24, case  WHEN DAY25 = 'A' THEN '' WHEN DAY25 = 'P' THEN '' WHEN DAY25 = 'W' THEN '' WHEN DAY25 = 'PH' THEN '' else DAY25 end as DAY25, case  WHEN DAY26 = 'A' THEN '' WHEN DAY26 = 'P' THEN '' WHEN DAY26 = 'W' THEN '' WHEN DAY26 = 'PH' THEN '' else DAY26 end as DAY26, case  WHEN DAY27 = 'A' THEN '' WHEN DAY27 = 'P' THEN '' WHEN DAY27 = 'W' THEN '' WHEN DAY27 = 'PH' THEN '' else DAY27 end as DAY27, case  WHEN DAY28 = 'A' THEN '' WHEN DAY28 = 'P' THEN '' WHEN DAY28 = 'W' THEN '' WHEN DAY28 = 'PH' THEN '' else DAY28 end as DAY28, case  WHEN DAY29 = 'A' THEN '' WHEN DAY29 = 'P' THEN '' WHEN DAY29 = 'W' THEN '' WHEN DAY29 = 'PH' THEN '' else DAY29 end as DAY29, case  WHEN DAY30 = 'A' THEN '' WHEN DAY30 = 'P' THEN '' WHEN DAY30 = 'W' THEN '' WHEN DAY30 = 'PH' THEN '' else DAY30 end as DAY30, case  WHEN DAY31 = 'A' THEN '' WHEN DAY31 = 'P' THEN '' WHEN DAY31 = 'W' THEN '' WHEN DAY31 = 'PH' THEN '' else DAY31 end as DAY31, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                }
                else
                {
                    int days = DateTime.DaysInMonth(year, month);

                    if (days == 28)
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else if (days == 29)
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                    else
                    {
                        sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, tot_days_present, tot_days_absent as absent,DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in( " + unit_code_email + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                    }
                }
            }
            i = 2;
        }
        try
        {
            d.con.Open();
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Repeater Repeater1 = new Repeater();
                        Repeater1.DataSource = ds;
                        Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, i, ds, start_date_common, d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 0,1));
                        Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, i, ds, start_date_common, "");
                        Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, i, ds, start_date_common, "");
                        Repeater1.DataBind();
                        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                        Repeater1.RenderControl(htmlWrite);
                        string abc = stringWrite.ToString();
                        string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
                        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        string filename1 = "";
                        if (ddl_client.SelectedValue == "BAGICTM")
                        {
                             filename1 = path + "Attendance_Copy_" + ddl_billing_state.SelectedItem.Text + ".xls";
                        }
                        else
                        {
                            filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        }
                        System.IO.File.WriteAllText(filename1, abc);
                        Session["year"] = txttodate.Text;
                    }
                    else
                    {
                        record_not_found = "NO";
                    }
                    d.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance sheet All Ready Sent For This Branch.');", true);
            //}
       }
    protected void btn_mailsend_click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select DESIGNATION ,COMPANY_NAME from pay_designation_count INNER JOIN pay_company_master on pay_designation_count.comp_code=pay_company_master.comp_code  where pay_designation_count.comp_code='" + Session["comp_code"].ToString() + "' and unit_code=" + unit_code_email + " and client_code='" + ddl_client.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                designation = (dr.GetValue(0).ToString());
                comp_name = (dr.GetValue(1).ToString());
                if (designation == "SECURITY GUARD")
                {
                    designation = "SG";
                }
                //else if (designation == "TAILOR")
                //{
                //    designation = "TAR";
                //}
                //else if (designation == "TRIAL ROOM GAURD")
                //{
                //    designation = "TRG";
                //}
                else
                {
                    designation = "HK";
                }
            }
            dr.Close();
            d.con.Close();

        }
        catch (Exception ae)
        {
        }
        finally
        {
            d.con.Close();
        }
        char[] seperator1 = { ',' };
        List<string> delete_list = new List<string>();// string head_email_id
        try
        {
            List<int> list = new List<int>();
            List<string> list1 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();
           
            string gg1 = head_email_id;
                    list1.Add(gg1);
                    d.con.Open();
                    MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'", d.con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        from_emailid = dr.GetValue(0).ToString();
                        password = dr.GetValue(1).ToString();
                    }
                    if (from_emailid == "" || password == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Mail Id and Password in Client Master.');", true);
                    }
                    else
                    {
                        dr.Close();
                        d.con.Close();
                        ddl_client_phonno();
                        string mail_body = d.getsinglestring("select group_concat(Field4,'<br>Asst. Manager- ',Field5,'<br>''" + comp_name + "''<br><span style = \"color:red\">(An ISO 9001-2008 Certified Company)</span><br>304, 3rd Floor, Nyati Millennium,Viman Nagar, Pune-411014 <br>Tel:', Field6 ) as 'aa'  from pay_zone_master  where   Field1 ='Admin' and  type='client_Email' and comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'");
                        string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam,</td></tr><tr><td style = \"font-family:Georgia;font-size:12pt;\">Greetings from IH&MS...!!!</td></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Attached herewith the attendance for the Month of " + txttodate.Text + ".</p></tr><tr><p><span style='font-family:Georgia,serif;color:#00B0F0'>Considering present situation of COVID – 19, We are unable to take scan copy of attendance. Request you to please approve attendance on mail for further process. Kindly confirm employees name and present days on mail body.</span></p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Please consider the excel file and get it corrected if required. Also it is compulsory to send  the scan copy of the register with in & out timing of the employees.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly send the same and also attach the scan copy of in and out attendance register and send ASAP with the Signature of Branch Head & Branch's Stamp.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration: underline;\">Note:-   Please take care with the below mention notes. As it is mandatory.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">1)Please mention in & out time by manually in attendance sheet. <br>2)Please use round stamp with full address stamp for <span style = \"font-family:Georgia;font-size:10pt;\">" + ddl_client.SelectedItem.Text + " </span>on  Attendance Sheet. <br> 3) Attendance is valid only by branch Manager Sign.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration:underline;\">Note :- Please send the attendance sheet with clear print if it is not clear i will not  mention the attendance sheet.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly note if there is no stamp available at the branch  Please give us the confirmation over the mail regarding the non availability of official stamp and the HK employees total working days along with the attached attendance format.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt\">Thanks & Regards</p></tr><tr><p style = \"font-size:10pt;\">" + mail_body + "</p></tr>";
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            mailMessage.From = new MailAddress(from_emailid);
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");

                            if (record_not_found != "NO")
                            {
                                if (ddl_client.SelectedValue == "BAGICTM")
                                {
                                    filename1 = path + "Attendance_Copy_" + ddl_billing_state.SelectedItem.Text + ".xls";
                                }
                                else
                                {
                                    filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                                }
                                list4.Add(filename1);
                                delete_list.Add(filename1);
                            }

                             d.con.Open();
                             MySqlCommand cmd1 = new MySqlCommand("SELECT File_name, filename_and FROM pay_feedback_files where client_code='" + ddl_client.SelectedValue + "'", d.con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                if (dr1.GetValue(1).ToString().Equals("0"))
                {
                    list4.Add(path + dr1.GetValue(0).ToString());
                }
                else if (dr1.GetValue(1).ToString().Equals("1"))
                {
                    string ext = dr1.GetValue(0).ToString().Substring(dr1.GetValue(0).ToString().IndexOf("."), dr1.GetValue(0).ToString().Length - dr1.GetValue(0).ToString().IndexOf("."));
                    list4.Add(path + dr1.GetValue(0).ToString().Replace(ext, unit_name + ext));
                    delete_list.Add(path + dr1.GetValue(0).ToString().Replace(ext, unit_name + ext));
                   if(!File.Exists(path + dr1.GetValue(0).ToString().Replace(ext, unit_name + ext)))
                    {
                        File.Copy(path + dr1.GetValue(0).ToString(), path + dr1.GetValue(0).ToString().Replace(ext, unit_name + ext));
                    }
                }
                else if (dr1.GetValue(1).ToString().Equals("2"))
                {
                    string ext = dr1.GetValue(0).ToString().Substring(dr1.GetValue(0).ToString().IndexOf("."), dr1.GetValue(0).ToString().Length - dr1.GetValue(0).ToString().IndexOf("."));
                    list4.Add(path + dr1.GetValue(0).ToString().Replace(ext, ddl_billing_state.SelectedItem.Text.Replace(" ","_") + ext));
                    delete_list.Add(path + dr1.GetValue(0).ToString().Replace(ext, ddl_billing_state.SelectedItem.Text.Replace(" ", "_") + ext));
                    if (!File.Exists(path + dr1.GetValue(0).ToString().Replace(ext, ddl_billing_state.SelectedItem.Text.Replace(" ","_") + ext)))
                    {
                        File.Copy(path + dr1.GetValue(0).ToString(), path + dr1.GetValue(0).ToString().Replace(ext, ddl_billing_state.SelectedItem.Text.Replace(" ", "_") + ext));
                    }
                }

            }

                        //if (ddl_client.SelectedValue == "HDFC")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }
                        //        string path2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        string filename = path2 + "OT_Details_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename);

                        //        string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        string filename2 = path3 + "HDFC_FEEDBACK_FORM" + ".xlsx";
                        //        list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "UTKARSH")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }
                        //    string path2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename = path2 + "CheckList_" + unit_name + ".xls";
                        //    list4.Add(filename);

                        //    string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename2 = path3 + "FEED_BACK_FORM_UTKRASH" + ".pdf";
                        //    list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "RLIC HK")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }

                        //    string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename2 = path3 + "Feedback_for_Reliance" + ".xlsx";
                        //    list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "SUD")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }

                        //    string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename2 = path3 + "FEEDBACK STAR DAICHI" + ".xls";
                        //    list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "BAG" || ddl_client.SelectedValue == "BG" || ddl_client.SelectedValue == "BALIC SG" || ddl_client.SelectedValue == "BALIC HK")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }
                        //    string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename2 = path3 + "Bajaj_Feedback_Form_" +unit_name + ".xls";
                        //    list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "4")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //    }

                        //    string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //    filename2 = path3 +"feedback_form_BFL_" + unit_name + ".xls";
                        //    list4.Add(filename2);
                        //}
                        //else if (ddl_client.SelectedValue == "ESFB")
                        //{
                        //    if (record_not_found != "NO")
                        //    {
                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        list4.Add(filename1);
                        //        string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        filename2 = path3 + "ESFB_Checklist.xls";
                        //        list4.Add(filename2);
                        //    }
                        //}
                        //else
                        //{
                        //    if (record_not_found != "NO")
                        //    {

                        //        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                        //        if (ddl_client.SelectedValue == "BAGICTM")
                        //        {
                        //            filename1 = path + "Attendance_Copy_" + ddl_billing_state.SelectedItem.Text + ".xls";
                        //        }
                        //        else
                        //        {
                        //            filename1 = path + "Attendance_Copy_" + unit_name + ".xls";
                        //        }
                        //        list4.Add(filename1);
                        //    }
                        //}
                        if (record_not_found != "NO")
                        {
                            foreach (string mail in list4)
                            {
                                mailMessage.Attachments.Add(new Attachment(mail));
                            }
                            foreach (string mail in list1)
                            {
                                if (mail != "")
                                {
                                    mailMessage.To.Add(mail.ToString());

                                    if (cc_emailid != "") { mailMessage.CC.Add(cc_emailid); }
                                    if (ddl_client.SelectedValue == "BAGICTM")
                                    {
                                        mailMessage.Subject = "Attendance sheet for " + designation + " Employee for the month of " + Convert.ToDateTime(txttodate.Text).ToString("MM/yyyy") + " for " + ddl_billing_state.SelectedValue + " ";
                                    }else{
                                    mailMessage.Subject = "Attendance sheet for " + designation + " Employee for the month of " + Convert.ToDateTime(txttodate.Text).ToString("MM/yyyy") + " for " + unit_name + " ";
                                    }
                                        mailMessage.Body = body;
                                    mailMessage.IsBodyHtml = true;
                                    SmtpServer.Port = 587;
                                    SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                                    SmtpServer.EnableSsl = true;
                                   SmtpServer.Send(mailMessage);
                                }
                            }
                        }
                        list1.Clear();
                        list3.Clear();
                        list4.Clear();
                        
                    }
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Mail Send Successfully.');", true);
                }
                }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Mail Not Send.');", true);
        }
        finally
        {
            d.con.Close();
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send Successfully.');", true);
           // list_emailnotsend.Visible = true;
            //Label1.Visible = true;
            fill_unit_emailnot_send();

            foreach (string mail in delete_list)
            {
                File.Delete(mail);
            }
            delete_list.Clear();
            //if (ddl_client.SelectedValue == "BAG" || ddl_client.SelectedValue == "BG" || ddl_client.SelectedValue == "BALIC SG" || ddl_client.SelectedValue == "BALIC HK")
            //{
            //    if (record_not_found == "YES")
            //    {
            //        System.IO.File.Delete(filename2);
            //        System.IO.File.Delete(filename1);
            //    }
            //    else
            //    {
            //        System.IO.File.Delete(filename2);
            //    }
            //}
            //else if (ddl_client.SelectedValue == "HDFC")
            //{
            //    if (record_not_found == "YES")
            //    {
            //        System.IO.File.Delete(filename1);
            //      //  System.IO.File.Delete(filename);
            //    }
            //}
            //else if (ddl_client.SelectedValue == "UTKARSH")
            //{
            //    if (record_not_found == "YES")
            //    {
            //        //System.IO.File.Delete(filename1);
            //        System.IO.File.Delete(filename1);
            //    }
            //}
            //else if (ddl_client.SelectedValue == "RLIC HK")
            //{
            //    if (record_not_found == "YES")
            //    {
            //        System.IO.File.Delete(filename1);
            //    }
            //}
            //else if (ddl_client.SelectedValue == "SUD")
            //{
            //    if (record_not_found == "YES")
            //    {
            //        System.IO.File.Delete(filename1);
            //    }
            //}
           

            record_not_found = "YES";
        }
    }
    protected void ddl_client_phonno()
    {
        d.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("select client_phonno from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + Session["CLIENT_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                phon_no = dr.GetValue(0).ToString();
            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
       
        int i;
        DAL d = new DAL();
        static int ctr;
        int daysadd = 0;
        string header = "", header1 = "";
        string bodystr = "", start_date_common = "";
        public MyTemplate(ListItemType type,int i, DataSet ds, string start_date_common, string header1)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.start_date_common = start_date_common;
            this.header1 = header1;
            ctr = 0;
            header = "<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th>";
            int days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
            if (days == 29)
            { header = header + "<th>29</th>"; daysadd = 1; }
            else if (days == 30)
            {
                header = header + "<th>29</th><th>30</th>"; daysadd = 2;
            }
            else if (days == 31)
            {
                header = header + "<th>29</th><th>30</th><th>31</th>"; daysadd = 3;
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
                    }
                    //if (i == 1)
                    //{

                    lc = new LiteralControl("<table border=1 style=font-size:10;text-align:center;vertical-align:middle;><tr><th colspan=" + (36 + daysadd) + " style=font-size:10;text-align:center;vertical-align:middle;><b>Attendance Sheet</b></th></tr><tr><th colspan=3 font-size=8>Name of the Principal Employer:</th><th width=300 colspan=15>" + ds.Tables[0].Rows[ctr]["client_name"].ToString() + "</th><th width=110 text-size=8 colspan=6>Name of the Service Provider:</th><th width=350 colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_name"].ToString() + "</th></tr><tr><th colspan=3>Location:</th><th colspan=15>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? ds.Tables[0].Rows[ctr]["state_name"].ToString() : ds.Tables[0].Rows[ctr]["location"].ToString()) + "</th><th colspan=6>Address of the Service Provider:</th><th width=380  colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["address1"].ToString() + "</th></tr><tr><th colspan=3>Location Code:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString() + "</th><th colspan=6>PF Registration No:</th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["pf_reg_no"].ToString() + "</th></tr><tr><th colspan=3 rowspan=2>Complete Address of the Location:</th><th colspan=15 rowspan=2>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? ds.Tables[0].Rows[ctr]["state_name"].ToString() : ds.Tables[0].Rows[ctr]["unit_add2"].ToString()) + "</th><th colspan=6>ESIC Registration No:</th><th colspan=" + (12 + daysadd) + ">'" + ds.Tables[0].Rows[ctr]["esic"].ToString() + "</th></tr><tr><th colspan=6>PAN No.</th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["company_pan_no"].ToString() + "</th></tr><tr><th colspan=3>Attendance for the Month:</th><th colspan=15>" + ds.Tables[0].Rows[ctr]["fromtodate"].ToString() + "</th><th colspan=6>GSTIN/ UIN: </th><th colspan=" + (12 + daysadd) + ">" + ds.Tables[0].Rows[ctr]["gst"].ToString() + "</th></tr><tr style=text-align:center;vertical-align:middle;><th style=text-align:center;vertical-align:middle; width=20>SL. NO.</th><th width=130>NAME</th><th width=70>DOJ (DD-MM-YYYY)</th><th width=90>Designation</th><th width=70>Particulars</th>" + header + "<th width=30>PRESENT DAYS</th><th width=30>ABSENT DAYS</th><th width=40 style=text-align:center;vertical-align:middle;>TOTAL MONTH DAYS (P+WO)</th></tr>");

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
                    if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY01"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY02"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY03"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY04"] + "</td>";
                    if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY05"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY06"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY06"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY06"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY07"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY07"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY07"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY08"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY08"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY08"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY09"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY09"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY09"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY10"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY10"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY10"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY11"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY11"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY11"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY12"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY12"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY12"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY13"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY13"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY13"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY14"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY14"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY14"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY15"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY15"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY15"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY16"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY16"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY16"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY17"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY17"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY17"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY18"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY18"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY18"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY19"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY19"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY19"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY20"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY20"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY20"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY21"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY21"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY21"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY22"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY22"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY22"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY23"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY23"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY23"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY24"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY24"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY24"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY25"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY25"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY25"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY26"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY26"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY26"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY27"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY27"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY27"] + "</td>";
                   if (ds.Tables[0].Rows[ctr]["DAY28"].ToString() == "A") { color = "red"; }      else if (ds.Tables[0].Rows[ctr]["DAY28"].ToString() == "W"){ color = "pink"; } 	else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY28"] + "</td>";



                   string iot_body = "";
                    int days1 = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                    string present_days = "";
                    string absent_days = "";
                    string ot_hrs = "";
                    if (days1 == 29)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                    }
                    else if (days1 == 30)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                    }
                    else if (days1 == 31)
                    {
                        if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY31"].ToString() == "A") { color = "red"; } else if (ds.Tables[0].Rows[ctr]["DAY31"].ToString() == "W") { color = "pink"; } else { color = "white"; } bodystr = bodystr + "<td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>";
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                    }
                    else
                    {
                        iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                    }
                    if (i == 1) 
                    {
                         string ot_body  = "";
                         
                        if (days1 == 29)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td> ";
                             iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
                       else if (days1 == 30)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY30"] + "</td>";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
                        else if (days1 == 31)
                        {
                            ot_body = "<td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY01"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY02"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY03"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY04"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY05"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY06"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY07"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY08"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY09"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY10"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY11"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY12"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY13"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY14"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY15"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY16"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY17"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY18"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY19"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY20"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY21"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY22"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY23"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY24"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY25"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY26"] + "</td>  <td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY27"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY28"] + "</td><td	style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY29"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY30"] + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["OT_DAY31"] + "</td> ";
                            iot_body = "<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
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

                            lc = new LiteralControl("<tr><td  style=text-align:center;vertical-align:middle;>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td  style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td  style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>Extra Hours</td>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                        }
                        else
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>Extra Hours</td>" + ot_body + "<td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td><td></td><td style=text-align:center;vertical-align:middle;>" + ot_hrs + "</td></tr>");
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

                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr><tr><td colspan = 4></td><td>IN TIME</td>" + iot_body + "<td></td><td></td><td></td></tr><tr><td colspan = 4></td><td>OUT TIME</td>" + iot_body + "<td></td><td></td><td></td></tr>");
                        }
                        else
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>Attendance</td>" + bodystr + "<td style=text-align:center;vertical-align:middle;>" + present_days + "</td><td style=text-align:center;vertical-align:middle;>" + absent_days + "</td><td style=text-align:center;vertical-align:middle;>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr>");

                        }
                    }
                    bodystr = "";
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("<tr><td rowspan=6></td><td><b>P = Present</b></td><td colspan =4>NAME OF THE MANAGER</td><td style=text-align:center;vertical-align:middle; colspan=14><b>" + ds.Tables[0].Rows[0]["LocationHead_Name"].ToString() + "</b></td><td height=120 colspan=" + (16 + daysadd) + " rowspan=6 align=center><b>Stamp and Signature of the Service Provider</b></td></tr><tr><td><b>A = Absent</b></td><td colspan =4>&nbsp;<p> SIGNATURE OF THE MANAGER</td><td colspan=14></td></tr><tr><td ><b>W = Weekly Off</b></td><td colspan =4>DATE OF SIGNATURE</td><td colspan=14></td></tr><tr><td><b>PH = Paid Holiday</b></td><td colspan =4>MOBILE NO.</td><td style=text-align:center;vertical-align:middle; colspan=14><b>" + ds.Tables[0].Rows[0]["LocationHead_mobileno"].ToString() + "</b></td></tr><tr><td><b>L = Leave</b></td><td colspan =4> LANDLINE NO.</td><td colspan=14></td></tr><tr><td></td><td colspan =4>STAMP</td><td colspan=14></td></tr></table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
    protected string get_start_date()
    {
        return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' limit 1))");
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
    protected void lnk_conformation(object sender,EventArgs e)
{
      //d.con.Open();
      //MySqlCommand cmd = new MySqlCommand("select flag from pay_send_mail_details where comp_code='" + Session["comp_code"].ToString() + "' and  unit_code='" + ddl_unitcode.SelectedValue + "' and client_code='" + ddl_client.SelectedValue + "' and month_year='" + txttodate .Text+ "'", d.con);
      //  MySqlDataReader dr = cmd.ExecuteReader();
      //  if (dr.Read())
      //  {
      //      int flag = int.Parse(dr.GetValue(0).ToString());
      //      if (flag == 0)
      //      {
      //         // btn_attendance.Visible = false;
              
      //      }
      //      else
      //      {
      //         // btn_attendance.Visible = false;
      //        //  btn_attendance1.Visible = true;
      //         // btn_sendmail.Visible = true;
      //      }
      //  }
}
    protected void btn_sendmail_Click(object sender,EventArgs e)
   {
       Session["client_code"] = ddl_client.SelectedValue;
       Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_");
   }
    protected void ddl_unitcode_click(object sender, EventArgs e)
    { 
        // d.con.Open();
        // MySqlCommand cmd = new MySqlCommand("select flag from pay_send_mail_details where comp_code='" + Session["comp_code"].ToString() + "' and month_year='" + txttodate.Text + "' and  unit_code='" + ddl_unitcode.SelectedValue + "' and client_code='" + ddl_client.SelectedValue + "'", d.con);
        //MySqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{
        //    int flag = int.Parse(dr.GetValue(0).ToString());
        //    if (flag == 0)
        //    {
        //        btn_attendance.Visible = false;
        //       // btn_attendance1.Visible = false;
        //    }
        //    else
        //    {
        //        btn_attendance.Visible = false;
        //       // btn_attendance1.Visible = true;
        //    }
        //}
        
    }
   
    //vikas
    private void load_grdview()
    {
        d.con1.Open();
        try
        {
            string where = " comp_Code = '" + Session["COMP_CODE"].ToString() + "'  and unit_code = '" + ddl_unitcode.SelectedValue + "' and month = '" + hidden_month.Value + "' and year='" + hidden_year.Value + "'";
            if (ddl_unitcode.SelectedValue == "ALL")
            {
                where = " comp_Code = '" + Session["COMP_CODE"].ToString() + "'  and client_code = '" + ddl_client.SelectedValue + "' and state = '"+ddl_billing_state.SelectedValue+"' and month = '" + hidden_month.Value + "' and year='" + hidden_year.Value + "'";
            }
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,comp_code,client_code,unit_code,month,year,(SELECT emp_name FROM pay_employee_master WHERE uploaded_by = pay_employee_master.emp_code) AS uploaded_by, uploaded_date,description,concat('~/approved_attendance_images/',file_name) as Value FROM pay_files_timesheet where "+where, d.con1);

            DataSet DS1 = new DataSet();
            MySqlDataAdapter1.Fill(DS1);
            grd_company_files.DataSource = null;
            grd_company_files.DataBind();
            if (DS1.Tables[0].Rows.Count > 0)
            {
                grd_company_files.DataSource = DS1;
                grd_company_files.DataBind();
            }
            txt_document1.Text = "";
            grd_company_files.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally {
            d.con1.Close();
        }

    }
    protected void upload_Click(object sender, EventArgs e)
    {
        upload_documents(document1_file);
             
    }
    private void upload_documents(FileUpload document_file)
    {

        if (document_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(document_file.FileName);
            if (fileExt == ".jpg" || fileExt == ".JPG" || fileExt == ".PNG" || fileExt == ".png" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt == ".JPEG" || fileExt == ".jpeg")
            {
                string fileName = Path.GetFileName(document_file.PostedFile.FileName);
                document_file.PostedFile.SaveAs(Server.MapPath("~/approved_attendance_images/") + fileName);

                string new_file_name = ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + txttodate.Text.Replace("/", "_") + fileExt;

                File.Copy(Server.MapPath("~/approved_attendance_images/") + fileName, Server.MapPath("~/approved_attendance_images/") + new_file_name, true);
                File.Delete(Server.MapPath("~/approved_attendance_images/") + fileName);
                d.operation("insert into pay_files_timesheet (comp_code, client_code, unit_code, file_name, description, month, year, uploaded_by, uploaded_date,state) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + new_file_name + "','" + txt_document1.Text + "'," + int.Parse(txttodate.Text.Substring(0, 2)) + "," + int.Parse(txttodate.Text.Substring(3)) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),'"+ddl_billing_state.SelectedValue+"')");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }

        }
        load_grdview();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void grd_company_files_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;

        if (e.Row.Cells[1].Text == "1")
        { e.Row.Cells[1].Text = "JAN";
        }
        else if (e.Row.Cells[1].Text == "2")
        {
            e.Row.Cells[1].Text = "FEB";
        }
        else if (e.Row.Cells[1].Text == "3")
        {
            e.Row.Cells[1].Text = "MAR";
        }
        else if (e.Row.Cells[1].Text == "4")
        {
            e.Row.Cells[1].Text = "APR";
        }
        else if (e.Row.Cells[1].Text == "5")
        {
            e.Row.Cells[1].Text = "MAY";
        }
        else if (e.Row.Cells[1].Text == "6")
        {
            e.Row.Cells[1].Text = "JUN";
        }
        else if (e.Row.Cells[1].Text == "7")
        {
            e.Row.Cells[1].Text = "JUL";
        }
        else if (e.Row.Cells[1].Text == "8")
        {
            e.Row.Cells[1].Text = "AUG";
        }
        else if (e.Row.Cells[1].Text == "9")
        {
            e.Row.Cells[1].Text = "SEP";
        }
        else if (e.Row.Cells[1].Text == "10")
        {
            e.Row.Cells[1].Text = "OCT";
        }
        else if (e.Row.Cells[1].Text == "11")
        {
            e.Row.Cells[1].Text = "NOV";
        }
        else if (e.Row.Cells[1].Text == "12")
        {
            e.Row.Cells[1].Text = "DEC";
        }
    }
    protected void btn_checklist_Click(object sender, EventArgs e)
    {
        int MONTH = Convert.ToInt32(txttodate.Text.Substring(0, 2));
        int YEAR = Convert.ToInt32(txttodate.Text.Substring(3));
        var firstDayOfMonth = new DateTime(YEAR, MONTH, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        string header = "<table border=1 style=border-collapse:collapse;><tr> <th colspan=5><h3>Checklist-Housekeeping</h3></th><th colspan=9><h3>Period-" + firstDayOfMonth.ToString("dd/MM/yyyy") + " to " + lastDayOfMonth.ToString("dd/MM/yyyy") + "</h3></th></tr> <tr><th colspan=1>DATE</th><th style=width:60px; colspan=1>Housekeeping Incoming Time (Before 9' O Clock)</th><th colspan=1 style=width:60px;>In Uniform  Yes/ No</th><th colspan=1 style=width:60px;>Dustbin Cleaning Yes/ No</th><th colspan=1 style=width:60px;>Dusting of Work Stations Yes/ No</th><th colspan=1 style=width:60px;>Brooming/ Mopping Yes/ No </th><th colspan=1 style=width:60px;>Toilet Cleaning Yes/ No</th><th colspan=1 style=width:60px;>Store-room Cleaning Yes/ No</th><th colspan=1 style=width:60px;>Pantry Cleaning Yes/ No </th><th colspan=1 style=width:60px;>Fill Water Bottle Yes/ No</th><th colspan=1 style=width:60px;>Cleaning Material Yes/No</th><th colspan=1 style=width:60px;>Remark</th><th colspan=1 >BM/Ops IC Signature</th><th colspan=1 style=width:330px;><h4>Instruction</h4></th></tr>";
        string instruction = "";
        string days = "", sun_day = "";
        CultureInfo ci = new CultureInfo("en-US");
        for (int i = 1; i <= ci.Calendar.GetDaysInMonth(YEAR, MONTH); i++)
        {
            if (new DateTime(YEAR, MONTH, i).DayOfWeek == DayOfWeek.Sunday)
            {
                if (i == 1)
                {
                    instruction = "1. Office should be clean before 8:00 am";
                     
                 }
                else if (i == 2)
                {
                    instruction = "2. HK Boy should be in uniform & well grooming";
                 }
                else if (i == 3)
                {
                    instruction = "3. Dustbin Clearance - 2 time";
                 }
                else if (i == 4)
                {
                    instruction = "4. Dusting of Work Station - 1 time";
                 }
                else if (i == 5)
                {
                    instruction = "5. Mopping - 3 to 4 time";
                    
                 }
                else if (i == 6)
                {
                    instruction = "6. Toilet Cleaning - 4 time";
                    
                 }
                else if (i == 7)
                {
                    instruction = "7. Store room Cleaning - 2 time";
                    
                 }
                else if (i == 8)
                {
                    instruction = "8. Pantry Cleaning - 2 time";
                    
                 }
                else if (i == 9)
                {
                    instruction = "9. Water Bottel Refilling - 2 time";
                    
                 }
                else
                {
                    instruction = "";
                }
                item_checklist = item_checklist + "<tr><th>" + i + "</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th style=background-color:yellow>WO</th><th></th><th></th><th style=text-align:left;vertical-align:left;>" + instruction + "</th></tr>";

            }
            else
            {
                if (i == 1)
                {
                    instruction = "1. Office should be clean before 8:00 am";
                }
                else if (i == 2)
                {
                    instruction = "2. HK Boy should be in uniform & well grooming";
                }
                else if (i == 3)
                {
                    instruction = "3. Dustbin Clearance - 2 time";
                }
                 else if (i == 4)
                {
                    instruction = "4. Dusting of Work Station - 1 time";
                }
                else if (i == 5)
                {
                    instruction = "5. Mopping - 3 to 4 time";

                }
                else if (i == 6)
                {
                    instruction = "6. Toilet Cleaning - 4 time";

                }
                else if (i == 7)
                {
                    instruction = "7. Store room Cleaning - 2 time";

                }
                else if (i == 8)
                {
                    instruction = "8. Pantry Cleaning - 2 time";

                }
                else if (i == 9)
                {
                    instruction = "9. Water Bottel Refilling - 2 time";

                }
                else
                {
                    instruction = "";
                }
                item_checklist = item_checklist + "<tr><th>" + i + "</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th style=text-align:left;vertical-align:left;>" + instruction + "</th> </tr>";
            }
        }
       
        

        string footer = "<tr><th style=text-align:left;vertical-align:left; colspan=4>BM/Ops IC Name :</th><th style=text-align:center;vertical-align:center;border:none; colspan=6></th></tr><tr><th style=text-align:left;vertical-align:left; colspan=4>Mobile No. : </th><th style=text-align:center;vertical-align:center;border:none; colspan=6></th></tr><tr><th style=text-align:left;vertical-align:left; colspan=4>Email ID : </th><th style=text-align:center;vertical-align:center;border:none; colspan=6>Stamp and Signature of the Service Provider</th></tr><tr><th style=text-align:left;vertical-align:left; colspan=4>Location & Branch code & State - </th><th style=text-align:center;vertical-align:center;border:none; colspan=6></th></tr></table>";
        Repeater Repeater1 = new Repeater();
        Repeater1.DataSource = null;
        Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header);
        Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item);
        Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer);
        Repeater1.DataBind();
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string style = @"" + header + "" + item_checklist + "" + footer + "<style> .textmode { } </style>";
        string abc = stringWrite.ToString();
        string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
        string filename1 = path + "CheckList_" + unit_name + ".xls";
        System.IO.File.WriteAllText(filename1, style);
    }
    protected void get_state()
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select STATE_NAME from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "'  and client_code='" + Session["client_code"].ToString() + "' and unit_code='" + Session["unit_code1"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            bajaj_state = dr.GetValue(0).ToString();
        }
        dr.Close();
        d.con.Close();
    
    }
    protected void btn_feedbabk_Click(object sender, EventArgs e)
    {
        int MONTH = Convert.ToInt32(txttodate.Text.Substring(0, 2));
        int YEAR = Convert.ToInt32(txttodate.Text.Substring(3));
        var firstDayOfMonth = new DateTime(YEAR, MONTH, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
        get_state();
        string logo_bajaj = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
        string logo = logo_bajaj + "Bajaj_Logo.png";
        string header = "<table border=1><tr><th style=border:none; colspan=3><img  src=" + logo + "></th><th colspan=1 style=font-size:17;font-weight:bold;>Vendor Name:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Services:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Branch Name:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Branch Code:</th><th colspan=2></th></tr><tr><th colspan=6></th></tr><tr><th style=font-size:30;font-weight:bold;background-color:blue; colspan=6>SECURITY GUARD FEEDBACK FORM</th></tr><tr><th colspan=6></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;</th><th colspan=2></th><th colspan=1 style=font-size:17;font-weight:bold;>state:</th><th colspan=2>" + bajaj_state + "</th></tr><tr><th colspan=6></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;background-color:blue; width=100>Sl.No.</th><th colspan=2 style=font-size:17;font-weight:bold;background-color:blue;>Description of service quality</th><th colspan=1 style=font-size:17;font-weight:bold;background-color:blue;>Service Efficiency</th><th colspan=2 style=font-size:17;font-weight:bold;background-color:blue;>Scores by the Company</th></tr>";
        string item = "<tr><th colspan=1>1</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Uniform Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>2</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Hygiene Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>3</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Daily Register Maintenance</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>4</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Visitors controlling Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>5</th><th colspan=2 style=font-size:17;font-weight:bold;>Manpower Deployment</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>6</th><th colspan=2 style=font-size:17;font-weight:bold;>Daily Manpower Dept. Liaison</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>7</th><th colspan=2 style=font-size:17;font-weight:bold;>Reliever Deployment Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>8</th><th colspan=2 style=font-size:17;font-weight:bold;>Task Delivery Quality</th><th colspan=1>100</th><th colspan=2></th></tr>";
        string footer = "<tr><th colspan=1 style=font-size:17;font-weight:bold;>Sign of Authorize Signatory with Branch Stamp:</th><th colspan=5></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;>Designation:</th><th colspan=5></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;>Contact No:</th><th colspan=5></th></tr>";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        string style = @"" + header + "" + item + "" + footer + "<style> .textmode { } </style>";
        string abc = stringWrite.ToString();
        string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
        string filename1 = path + "Bajaj_Feedback_Form_" + unit_name + ".xls";
        System.IO.File.WriteAllText(filename1, style);
    }
    protected void btn_otdetails_Click(object sender, EventArgs e)
    {
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string month_name = mfi.GetMonthName(int.Parse(txttodate.Text.Substring(0, 2))).ToString();
        string month_year = month_name + " " + txttodate.Text.Substring(3);
        string header = "<table border=1><tr><th colspan=15>OT Sheet</th></tr><tr><th colspan=10>Monthly Over Time Approval Tracker For Outsource Manpower ( MOAT )</th><th colspan=5> AGENCY NAME : IHMS</th></tr><tr><th colspan=1>Emp Name</th><th colspan=4></th><th colspan=2>Branch Code</th><th colspan=2></th><th colspan=2>Branch Name</th><th colspan=2>Month / Year</th><th colspan=2>" + month_year + "</th></tr><tr  style=background-color:yellow;><th colspan=1>Date</th><th colspan=1>DAY</th><th colspan=1>Branch Opening Time</th><th colspan=1>Branch Closing Time</th><th colspan=1>S / G In Time</th><th colspan=1>S / G Out Time</th><th colspan=1>OT Hours ( Duty Hours Are 8.30 Hours )</th><th colspan=1>Business Purpose ( Reason) For OT</th><th colspan=1>Value Of Bebefit</th><th colspan=1>Requester Name</th><th colspan=1>Requester Designation</th><th colspan=1>Approver Name</th><th colspan=1>Approver Designation</th><th colspan=1>Department/Channel/Unit Name / Sister co. Name</th><th colspan=1>Remark</th></tr>";
        string item = "<tr><th>1</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>2</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>3</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>4</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>5</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>6</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>7</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>8</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>9</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>10</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>11</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>12</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>13</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>14</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>15</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>16</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>17</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>18</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>19</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>20</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>21</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>22</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>23</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>24</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>25</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>26</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>27</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>28</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>29</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>30</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>31</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr style=background-color:yellow;><th></th><th></th><th></th><th></th><th></th><th>Total</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr>";
        string footer = "<tr><th style=text-align:left;vertical-align:left; colspan=2>IMPORTANT Note :</th><th colspan=5></th> <th colspan=1></th><th style=text-align:left;vertical-align:left; colspan=4>PSO / Approver Name</th><th colspan=3></th></tr><tr><th style=text-align:left;vertical-align:left; colspan=7>1. OT Means Over Time & OT Hours Are Only For Approval Work Beyond Duty Shift Of 8.00 Hours</th><th></th><th style=text-align:left;vertical-align:left; colspan=4>Designation</th><th colspan=3></th></tr><tr><th style=text-align:left;vertical-align:left; colspan=7>2. PSO / Branch Head / Zonal Opration Manager / Regional Admin / Zonal Admin Is Only Authried To Sign The OT Sheet</th><th colspan=1>Branch Address Seal</th><th style=text-align:left;vertical-align:left; colspan=4>Employee Code</th><th colspan=3></th></tr><tr><th style=text-align:left;vertical-align:left; colspan=7>3. Original MOAT needs to be Sumit along Manpower Attendance Sheet to the Manpower Agency & They Will Submit The Original To Head Office Along With Billing</th><th colspan=1></th><th style=text-align:left;vertical-align:left; colspan=4>Employee Signature / Date</th><th colspan=3></th></tr></table>";
        Repeater Repeater1 = new Repeater();
        Repeater1.DataSource = null;
        Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header);
        Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item);
        Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer);
        Repeater1.DataBind();
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Repeater1.RenderControl(htmlWrite);
        string style = @"" + header + "" + item + "" + footer + "<style> .textmode { } </style>";
        string abc = stringWrite.ToString();
        string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
        string filename1 = path + "OT_Details_Copy_" + unit_name + ".xls";
        System.IO.File.WriteAllText(filename1, style);
    }
    public class MyTemplate1 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        string header = "", header1 = "";
        string bodystr = "";
        public MyTemplate1(ListItemType type)
        {
            this.type = type;
           
           
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr><th colspan=16  >OT Sheet</th></tr><tr><th colspan=10>Monthly Over Time Approval Tracker For Outsource Manpower ( MOAT )</th><th colspan=6> AGENCY NAME : IHMS</th><th colspan=6>Name of the Service Provider:</th></tr><tr><th colspan=1>Emp Name</th><th colspan=4></th><th colspan=2>Branch Code</th><th colspan=2></th><th colspan=2>Month / Year</th><th colspan=2>SEPTEMBER-2018</th></tr><tr><th colspan=1>Date</th><th colspan=1>DAY</th><th colspan=1>Branch Opening Time</th><th colspan=1>Branch Closing Time</th><th colspan=1>S / G In Time</th><th colspan=1>S / G Out Time</th><th colspan=1>OT Hours ( Duty Hours Are 8.30 Hours )</th><th colspan=1>Business Purpose ( Reason) For OT</th><th colspan=1>Value Of Bebefit</th><th colspan=1>Requester Name</th><th colspan=1>Requester Designation</th><th colspan=1>Approver Name</th><th colspan=1>Approver Designation</th><th colspan=1>Department/Channel/Unit Name / Sister co. Name</th><th colspan=1>Remark</th></tr>");
                    header = "";
                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><th>1</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>2</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>1</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>3</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>1</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>4</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>5</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>6</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>7</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>8</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>9</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>1</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>10</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>11</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>12</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>13</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>14</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>15</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>16</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>17</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>18</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>19</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>20</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>21</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>22</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>23</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>24</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>25</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>26</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>27</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>28</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>29</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>30</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr><tr><th>31</th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr>");
                    bodystr = "";
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("<tr><th colspan=2>IMPORTANT Note :</th><th colspan=7></th></tr><th colspan=4></th><th colspan=4>PSO / Approver Name</th><th colspan=4></th></tr><tr><th colspan=8>1. OT Means Over Time & OT Hours Are Only For Approval Work Beyond Duty Shift Of 8.00 Hours</th><th></th><th style=text-align:left;vertical-align:left; colspan=8>Designation</th><th colspan=4></th></tr><tr><th colspan=8>2. PSO / Branch Head / Zonal Opration Manager / Regional Admin / Zonal Admin Is Only Authried To Sign The OT Sheet</th><th colspan=1>Branch Address Seal</th><th colspan=1>Employee Code</th><th colspan=3></th></tr><tr><th colspan=7>3. Original MOAT needs to be Sumit along Manpower Attendance Sheet to the Manpower Agency & They Will Submit The Original To Head Office Along With Billing</th><th colspan=1></th><th colspan=4>Employee Signature / Date</th><th colspan=4></th></tr></table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
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
            if ((e.Row.Cells[7].Text).Trim() != "")
            {
                CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);
                txtName.Checked = true;
            }
        }
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[7].Visible = false;

    }
    protected string get_end_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "'  limit 1))");
    }
    //vias01/11/18
    protected void btn_add_emp_Click(object sender, EventArgs e)
    {
        Session["client_code"] = ddl_client.SelectedValue;
        Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;
        ModalPopupExtender1.Show();


    }
    protected void ddl_unit_click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        load_grdview();
        fill_branch_wise_gv();
        fill_unitname_listbox();
        Session["client_code"] = ddl_client.SelectedValue;//vikas30/10
        Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;//vikas0/10
        if (ddl_unitcode.SelectedValue == "ALL")
        {
            btn_add_emp.Visible = false;
        }
        else
        {
            btn_add_emp.Visible = true;
        }
        
            }
    protected void add_mail_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    //protected void gv_fullmonthot_PreRender(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        gv_fullmonthot.UseAccessibleHeader = false;
    //        gv_fullmonthot.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //    catch { }
    //}
    protected void btn_process_blank_Click(object sender, EventArgs e)
    {
        blank_mail = 1;
        btn_process_Click(null,null);
    }
    protected void btn_process_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //upload2.Visible = true;
      //  btn_attendance.Visible = false;
        //btn_attendance.Visible = false;
        hidden_month.Value = txttodate.Text.Substring(0, 2);
        hidden_year.Value = txttodate.Text.Substring(3);
        Session["client_code"] = ddl_client.SelectedValue;
        Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_");
        Session["unit_code1"] = ddl_unitcode.SelectedValue;
        Session["client_code1"] = ddl_client.SelectedItem.Text;
        Session["year"] = txttodate.Text;
        load_grdview();
        Session["unit_code_addemp"] = ddl_unitcode.SelectedValue;

        btn_attendance_Click(null, null);
    }
    int CountDay(int month, int year, int counter)
    {
        string branch ="unit_code  = '" + ddl_unitcode.SelectedValue + "'";
        if(ddl_unitcode.SelectedValue == "ALL")
        {
            branch = "unit_code in (Select unit_code from pay_unit_master where client_code='" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "')";
        }
        string start_date_common = d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND "+branch+" AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master . billing_unit_code  =  pay_unit_master . unit_code  AND  pay_billing_master . comp_code  =  pay_unit_master . comp_code  WHERE pay_unit_master . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  "+branch+" limit 1))");
        string end_date_common = d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND " + branch + " AND month = '" + hidden_month.Value + "' AND year = '" + hidden_year.Value + "' limit 1), (SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master . billing_unit_code  =  pay_unit_master . unit_code  AND  pay_billing_master . comp_code  =  pay_unit_master . comp_code  WHERE pay_unit_master . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  " + branch + " limit 1))");
        //string start_date_common = get_start_date();
        //string end_date_common = get_end_date();
        int NoOfSunday = 0;
        var firstDay = (dynamic)null;
        if (start_date_common != "1")
        {
            if ((month - 1) == 0) { month = 12; --year; }
            firstDay = new DateTime(year, (month - 1), int.Parse(start_date_common));
        }
        else { firstDay = new DateTime(year, month, 1); }
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
            int year1 = year;
            if (month == 12)
            {
                year1 = year - 1;
            }
            else { year1 = year; }
           
            var start_date = new DateTime(year1, (month - 1), int.Parse(start_date_common));
            var end_date = new DateTime(year1, (month), int.Parse(end_date_common));
            if ((end_date.Date - start_date.Date).Days == 29)
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


        if (counter == 2)
        {
            return NumOfDay;
        }
        if (counter == 1)
        {
            return NumOfDay - NoOfSunday;
        }
        else
        { return NoOfSunday; }
    }
    private void gridcalender_update(int mnth, int year, int counter)
    {
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        try
        {
            string date_common = get_start_date();
            string where_unit = "";
            if (ddl_unitcode.SelectedValue == "ALL") { where_unit = " and pay_employee_master.unit_code in (select unit_code from pay_unit_master where client_code = '" + ddl_client.SelectedValue + "')"; }
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
                string insrt_where = "";
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
                int counter1 = 0;

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
                        counter1 = 1;
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
                        if (d.getsinglestring("select flag from pay_attendance_muster where comp_Code = '"+Session["COMP_CODE"].ToString()+"' and emp_code = '"+emp_code+"' and month = "+mnth+" and year = "+year+" ").Equals("0"))
                        {
                            d.operation("delete from pay_attendance_muster where emp_CODE='" + emp_code + "' AND MONTH = " + mnth + " AND YEAR= " + year);
                            d.operation("delete from pay_ot_muster where emp_CODE='" + emp_code + "' AND MONTH = " + mnth + " AND YEAR= " + year);
                        }
                    }
                }
                if (counter1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Atleast One Employee.');", true);
                    return;

                }
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


                    d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintSundays(mnth, year, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") and EMP_CODE not in (select emp_code from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + "))");
                    d.operation("update pay_attendance_muster set " + d.PrintSundays(mnth, year, 2) + ", TOT_DAYS_PRESENT=0,TOT_DAYS_ABSENT= " + CountDay(mnth, year, 1) + " WHERE emp_code in (" + reliverlist + ") and MONTH = " + mnth + " AND YEAR= " + year);
                    if (date_common != "" && date_common != "1")
                    {
                        d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintSundays(mnth1, year1, 0) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") and EMP_CODE not in (select emp_code from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + "))");
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
                        d.operation("INSERT INTO pay_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTSundays(mnth, year, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth + "','" + year + "'," + CountDay(mnth, year, 2) + "," + CountDay(mnth, year, 3) + "," + CountDay(mnth, year, 1) + "," + d.PrintOTSundays(mnth, year, 2) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") and EMP_CODE not in (select emp_code from pay_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth + " AND YEAR= " + year + "))");

                        if (date_common != "" && date_common != "1")
                        {
                            d.operation("INSERT INTO pay_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTSundays(mnth1, year1, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE,'" + mnth1 + "','" + year1 + "'," + CountDay(mnth1, year1, 2) + "," + CountDay(mnth1, year1, 3) + "," + CountDay(mnth1, year1, 1) + "," + d.PrintOTSundays(mnth1, year1, 2) + " FROM pay_employee_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") and EMP_CODE not in (select emp_code from pay_ot_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in (" + inlist + ") AND MONTH = " + mnth1 + " AND YEAR= " + year1 + "))");

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    string empcode = d.getsinglestring(" SET SESSION group_concat_max_len = 100000000; select cast(group_concat(emp_code) as char) from pay_attendance_muster where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and emp_code in( " + outlist + ") and month = " + mnth + " and year = " + year + " and flag = 0 ");

                   d.operation("delete from pay_attendance_muster where emp_CODE in ('" + empcode + "') AND MONTH = " + mnth + " AND YEAR= " + year);
                   d.operation("delete from pay_ot_muster where emp_CODE in ('" + empcode + "') AND MONTH = " + mnth + " AND YEAR= " + year);
                    
                }
            }
            update_joining_left_date();
            d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, hidden_month.Value + "/" + hidden_year.Value, int.Parse(date_common), ddl_billing_state.SelectedValue);

        }
        catch (Exception ex) { throw ex; }
        finally
        { }
    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        ddl_client.SelectedValue = "Select";
        ddl_unitcode.Items.Clear();
        list_unitname.Items.Clear();
        list_unitname.Visible = false;
        lbl_branck_emaillist.Visible = false;
        ddl_billing_state.Items.Clear();
        gv_fullmonthot.DataSource = null;
        gv_fullmonthot.DataBind();
    }
   

    protected void btn_feedbabk_bfl_Click(string unit_code)
    {
        string new_unit_code = "";
        try
        {
            string comp_code = Session["comp_code"].ToString();

            d.con.Open();
            MySqlCommand cmd=new MySqlCommand("SELECT UNIT_CODE, CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE unit_code= "+unit_code_email+" and comp_code='"+comp_code+"' ", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                new_unit_code = dr.GetValue(1).ToString();
            }
            dr.Close();
            d.con.Close();
            d.con.Open();
            int MONTH = Convert.ToInt32(txttodate.Text.Substring(0, 2));
            int YEAR = Convert.ToInt32(txttodate.Text.Substring(3));
            var firstDayOfMonth = new DateTime(YEAR, MONTH, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
             string logo_BFL = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\bfl_Logo.png");
        
            int month = int.Parse(hidden_month.Value) - 1;
            int year = int.Parse(hidden_year.Value);
            if (month == 0) { month = 12; year = year - 1; }
             string month_year = month+"/"+year;
            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
            string filename1 = path + "BFL_Feedback_Form_" + unit_name + ".xls";
          
            MySqlDataAdapter adp2;


            adp2 = new MySqlDataAdapter("select distinct emp_code,emp_name as 'EMP_NAME',GRADE_DESC as 'DESIGNATION' from pay_employee_master inner join pay_grade_master on pay_employee_master. GRADE_CODE  =  pay_grade_master. GRADE_CODE   where pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.client_code='" + ddl_client.SelectedValue + "' and unit_code= " + unit_code + " and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null || (left_date > str_to_date('" + 1 + '/' + MONTH + '/' + YEAR + "','%d/%m/%Y')) || LEFT_DATE='') ", d.con1);


                DataSet ds = new DataSet();
                adp2.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds, logo_BFL, ddl_client.SelectedItem.Text, unit_code_email, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, month_year, comp_code, new_unit_code);
                Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, null);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string abc = stringWrite.ToString();
                string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
                string pathnew = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                string filename2 = pathnew + "feedback_form_BFL_" + unit_name + ".xls";

                System.IO.File.WriteAllText(filename2, abc);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    public class MyTemplate2 : ITemplate
    {
      
       
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string logo_BFL, client_name, unit_name, state_name, unit_code, month_year, comp_code, new_unit_code;
        static int ctr;
        public MyTemplate2(ListItemType type, DataSet ds, string logo_BFL, string client_name, string unit_name, string state_name, string unit_code, string month_year, string comp_code, string new_unit_code)
        {
            this.type = type;
            this.logo_BFL = logo_BFL;
            this.client_name = client_name;
            this.unit_name = unit_name;
            this.state_name = state_name;
            this.unit_code = unit_code;
            this.month_year = Convert.ToDateTime(month_year).ToString("MM/yyyy");
            this.comp_code = comp_code;
            this.new_unit_code = new_unit_code;
            this.ds = ds;
             
            ctr = 0;
        }
        public MyTemplate2(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
       

        public void InstantiateIn(Control container)
        {
            DateTime now = DateTime.Now;

            switch (type)
            {
                case ListItemType.Header:
                    //lc = new LiteralControl("<table cellspacing= 0  border= 1 ><tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 rowspan=3  align= center  valign=middle><b><font face= Tahoma  color= #000000;><img  src=" + logo_BFL + " width=170></font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=9 rowspan=3 align= center  valign=middle><b><font face= Tahoma  size=6 color= #000000 >IH&amp;MS INTEGRATED SOLUTIONS INDIA PVT. LTD</font></b></td> </tr>  <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 37  align= center  valign=middle bgcolor= #003366 ><b><font face= Tahoma  size=4 color= #FFFFFF >FEEDBACK FROM</font></b></td> </tr>#FFFFFF ><br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >CLIENT NAME :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=6 align= left  valign=middle><font face= Tahoma  color= #000000 >" + client_name + "</font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >STATE :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 align= center  valign=middle><b><font face= Tahoma >" + state_name + "<br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >BRANCH NAME :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=6 align= left  valign=middle><font face= Tahoma  color= #000000 >" +new_unit_code + "</font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >BRANCH CODE : </font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 align= center  valign=middle><b><font face= Tahoma >" + unit_name+ "<br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >MONTH / YEAR :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000  colspan=6 align= center  valign=middle><font face= Tahoma  color= #000000 >" + month_year + "<br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >DATE :</font></b><b ></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-right: 1px solid #000000  colspan=2 align= center  valign=middle><b><font face= Tahoma >" + now + " <br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 24  align= center  valign=middle><b><font face= Tahoma  size=4 color= #FFFFFF ><br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 height= 97  align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >NO.</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 align= left  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >EMPLOYEE NAME</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >EMPLOYEE<br>DESIGNATION</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=8 align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  size=4 color= #000000 >OVER ALL HOUSEKEEPING SERVICES</font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >HYGENIE MAINTAINED <br>AT THE BRANCH</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >HK PERSON WAS IN PROPER UNIFORM</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >ATTITUDE OF EMPLOYEE</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >REGULARITY OF HK PERSON</font></b></td> <td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >EMPLOYEE  <br>GROOMING</font></b></td> <td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >OFFICE ASSISTANT <br>SERVICES</font></b></td> <td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >MANPOWER DEPLOYMENT <br>TAT MAINTAINED</font></b></td> <td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >OTHER SERVICES <br>(IF ANY)</font></b></td> </tr>");
                    lc = new LiteralControl("<table cellspacing= 0  border= 1 > 	<colgroup width= 54 ></colgroup> 	<colgroup width= 262 ></colgroup> 	<colgroup width= 167 ></colgroup> 	<colgroup span= 6  width= 205 ></colgroup> 	<colgroup width= 226 ></colgroup> 	<colgroup width= 205 ></colgroup> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=1 rowspan=3  align= center  valign=middle><b><font face= Tahoma  color= #000000 ><img  src=" + logo_BFL + " width=140></font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=10 rowspan=3 align= center  valign=middle><b><font face= Tahoma  size=6 color= #000000 >IH&amp;MS INTEGRATED SOLUTIONS INDIA PVT. LTD</font></b></td> 		</tr> 	<tr> 		</tr> 	<tr> 		</tr> 	<tr> </tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 37  align= center  valign=middle bgcolor= #003366 ><b><font face= Tahoma  size=4 color= #FFFFFF >FEEDBACK FROM</font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 24  align= center  valign=middle><b><font face= Tahoma  size=4 color= #FFFFFF ><br></font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >CLIENT NAME :</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000;font-weight:bold  colspan=6 align= left  valign=middle><font style=font-weight:bold>" + client_name + "</font></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >STATE :</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 align= left  valign=middle><b><font > " + state_name + "<br></font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >BRANCH NAME</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=6 align= left  valign=middle><font style=font-weight:bold >" + new_unit_code + "</font></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >BRANCH CODE : </font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000;font-weight:bold  colspan=2 align= left  valign=middle><b><font style=font-weight:bold >" + unit_name + "<br></font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 height= 51  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >MONTH / YEAR :</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000;font-weight:bold  colspan=6 align= left  valign=middle><font style=font-weight:bold ><br>" + month_year + "</font></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle bgcolor= #CCCCFF ><b><font face= Tahoma >DATE :</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-right: 1px solid #000000  colspan=2 align= left  valign=middle><b><font >" + now + "<br></font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 24  align= center  valign=middle><b><font face= Tahoma  size=4 color= #FFFFFF ><br></font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 height= 97  align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >NO.</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 align= left  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >EMPLOYEE NAME</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  rowspan=2 align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  color= #000000 >EMPLOYEE<br>DESIGNATION</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=8 align= center  valign=middle bgcolor= #99CCFF ><b><font face= Tahoma  size=4 color= #000000 >OVER ALL HOUSEKEEPING SERVICES</font></b></td> 		</tr> 	<tr> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >HYGENIE MAINTAINED <br>AT THE BRANCH</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >HK PERSON WAS IN PROPER UNIFORM</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >ATTITUDE OF EMPLOYEE</font></b></td> 		<td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >REGULARITY OF HK PERSON</font></b></td> 		<td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >EMPLOYEE  <br>GROOMING</font></b></td> 		<td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >OFFICE ASSISTANT <br>SERVICES</font></b></td> 		<td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >MANPOWER DEPLOYMENT <br>TAT MAINTAINED</font></b></td> 		<td style= border-top: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= center  valign=middle><b><font face= Tahoma  color= #000000 >OTHER SERVICES <br>(IF ANY)</font></b></td> 	</tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  height= 32  align= center  valign=middle sdval= 1  sdnum= 1033; ><font face= Tahoma  color= #000000 >" + ctr + 1 + "</font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 >" + ds.Tables[0].Rows[ctr]["EMP_NAME"] + "</font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 >" + ds.Tables[0].Rows[ctr]["DESIGNATION"] + "</font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  align= left  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> </tr>");
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("<tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 32  align= center  valign=middle><font face= Tahoma  color= #000000 ></font></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000  colspan=3 height= 41  align= left  valign=middle><b><font face= Tahoma  size=3 color= #000000 >SERVICES DISCRIPEANCE (IF ANY) :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-right: 1px solid #000000  colspan=8 align= center  valign=middle><b><font face= Tahoma  size=3 color= #000000 ><br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 41  align= center  valign=middle><b><font face= Tahoma  color= #000000 ><br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 41  align= center  valign=middle><b><font face= Tahoma  color= #000000 ><br></font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 36  align= center  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 height= 37  align= center  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=5 height= 44  align= left  valign=middle><b><font face= Tahoma  size=3 color= #000000 >AUTHORIZE SIGNATORY :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=2 rowspan=7 align= center  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=4 align= center  valign=middle><b><font face= Tahoma  size=3 color= #000000 >BRANCH STAMP :</font></b></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=5 height= 42  align= left  valign=middle><b><font face= Tahoma  size=3 color= #000000 >DESIGNATION :</font></b></td> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=4 rowspan=6 align= center  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=5 rowspan=5 height= 140  align= center  valign=middle><font face= Tahoma  color= #000000 ><br></font></td> </tr> <tr> </tr> <tr> </tr> <tr> </tr> <tr> </tr> <tr> <td style= border-top: 1px solid #000000; border-bottom: 1px solid #000000; border-left: 1px solid #000000; border-right: 1px solid #000000  colspan=11 rowspan=2 height= 14  align= center  valign=left><font face= Tahoma  color= #000000 ><b>Rating : Excellent - 10, Good - 6, Very Good - 8, Average - 4, Poor -2</b></font></td> </tr>  </table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
    private void update_joining_left_date()
    {
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");
        string start_date_common = get_start_date();
        //string end_date_common = get_end_date();
        DateTime start_date, end_date;
        int month = int.Parse(hidden_month.Value);
        int year = int.Parse(hidden_year.Value);
        string where = " pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' ";
        if (ddl_billing_state.SelectedValue != "Select")
        {
            where = where + " AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'";
        }
        if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0")
        {
            where = where + " AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
        }
        if (start_date_common != "" && start_date_common != "1")
        {
            month = --month;
            if (month == 0) { month = 12; year = --year; }
            where = where + " and (left_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') or joining_date between str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') and str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact(start_date_common + "/" + (month >= 10 ? month.ToString() : "0" + month.ToString()) + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = DateTime.ParseExact((int.Parse(start_date_common) - 1).ToString() + "/" + hidden_month.Value + "/" + hidden_year.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            start_date_common = "1";

            where = where + " and (left_date between str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') or joining_date between str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') and str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y'))";
            start_date = DateTime.ParseExact("01/" + hidden_month.Value + "/" + hidden_year.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            end_date = start_date.AddMonths(1).AddDays(-1);
        }


        MySqlCommand cmd_item = new MySqlCommand("SELECT emp_code, date_format(joining_date,'%d/%m/%Y'), ifnull(DATE_FORMAT(left_date, '%d/%m/%Y'),'1') FROM pay_employee_master inner join pay_unit_master on pay_unit_master.comp_code =  pay_employee_master.comp_code and pay_unit_master.unit_code =  pay_employee_master.unit_code WHERE " + where, d1.con1);
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
                        int result = 0;

                       result =  d.operation("UPDATE pay_attendance_muster SET " + start_date_common_query.Substring(0, start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "' and flag = 0");

                       if (ot_applicable == "1" && result > 0)
                        {
                            d.operation("UPDATE pay_ot_muster SET " + Ot_start_date_common_query.Substring(0, Ot_start_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + month + "' AND YEAR='" + year + "'");
                        }
                    }
                    if (End_date_common_query != "")
                    {
                        int result = 0;

                        result = d.operation("UPDATE pay_attendance_muster SET " + End_date_common_query.Substring(0, End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'  and flag = 0");
                        if (ot_applicable == "1" && result > 0)
                        {
                            d.operation("UPDATE pay_ot_muster SET " + Ot_End_date_common_query.Substring(0, Ot_End_date_common_query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
                        }
                    }
                }
                else
                {
                    if (query != "")
                    {
                        int result = 0; 
                       result  =  d.operation("UPDATE pay_attendance_muster SET " + query.Substring(0, query.Length - 1) + " WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "' and flag = 0 ");
                       if (ot_applicable == "1" && result > 0)
                        {
                            d.operation("UPDATE pay_ot_muster SET " + ot_query.Substring(0, ot_query.Length - 1) + "  WHERE EMP_CODE = '" + emp_code + "' AND MONTH = '" + hidden_month.Value + "' AND YEAR='" + hidden_year.Value + "'");
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

    protected void btn_morning_emailprocess_Click(object sender, EventArgs e)
    {
        List<string> list1 = new List<string>();
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                from_emailid = dr.GetValue(0).ToString();
                password = dr.GetValue(1).ToString();
            }
            if(from_emailid ==""||password =="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Mail Id and Password in Client Master.');", true);
           
            }
            else{
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Mail Id and Password in Client Master.');", true);
                dr.Close();
                d.con.Close();
                ddl_client_phonno();
                //    string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam,</td></tr><tr><td style = \"font-family:Georgia;font-size:12pt;\">Greetings from IH&MS...!!!</td></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Attached herewith the attendance for the Month of " + txttodate.Text + ".</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Please consider the excel file and get it corrected if required. Also it is compulsory to send  the scan copy of the register with in & out timing of the employees.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly send the same and also attach the scan copy of in and out attendance register and send ASAP with the Signature of Branch Head & Branch's Stamp.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration: underline;\">Note:-   Please take care with the below mention notes. As it is mandatory.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">1)Please mention in & out time by manually in attendance sheet. <br>2)Please use round stamp with full address stamp for <span style = \"font-family:Georgia;font-size:10pt;\">" + ddl_client.SelectedItem.Text + " </span>on  Attendance Sheet. <br> 3) Attendance is valid only by branch Manager Sign.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration:underline;\">Note :- Please send the attendance sheet with clear print if it is not clear i will not  mention the attendance sheet.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly note if there is no stamp available at the branch  Please give us the confirmation over the mail regarding the non availability of official stamp and the HK employees total working days along with the attached attendance format.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt\">Thanks & Regards</p></tr><tr><p style = \"font-size:10pt;\">" + Session["USERNAME"].ToString() + "<br>Asst. Manager- Administration<br>'" + comp_name + "'<br><span style = \"color:red\">(An ISO 9001-2008 Certified Company)</span><br>304, 3rd Floor, Nyati Millennium,Viman Nagar, Pune-411014 <br>Tel: " + phon_no + "</p></tr>";


            //  string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam,</td></tr><tr><td style = \"font-family:Georgia;font-size:12pt;\">Greetings from IH&MS...!!!</td></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Attached herewith the attendance for the Month of " + txttodate.Text + ".</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Please consider the excel file and get it corrected if required. Also it is compulsory to send  the scan copy of the register with in & out timing of the employees.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly send the same and also attach the scan copy of in and out attendance register and send ASAP with the Signature of Branch Head & Branch's Stamp.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration: underline;\">Note:-   Please take care with the below mention notes. As it is mandatory.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">1)Please mention in & out time by manually in attendance sheet. <br>2)Please use round stamp with full address stamp for <span style = \"font-family:Georgia;font-size:10pt;\">" + ddl_client.SelectedItem.Text + " </span>on  Attendance Sheet. <br> 3) Attendance is valid only by branch Manager Sign.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration:underline;\">Note :- Please send the attendance sheet with clear print if it is not clear i will not  mention the attendance sheet.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly note if there is no stamp available at the branch  Please give us the confirmation over the mail regarding the non availability of official stamp and the HK employees total working days along with the attached attendance format.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt\">Thanks & Regards</p></tr><tr><p style = \"font-size:10pt;\">" + Session["USERNAME"].ToString() + "<br>Asst. Manager- Administration<br>'" + comp_name + "'<br><span style = \"color:red\">(An ISO 9001-2008 Certified Company)</span><br>304, 3rd Floor, Nyati Millennium,Viman Nagar, Pune-411014 <br>Tel: " + phon_no + "</p></tr>";
            string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam, Good Morning</p></tr>";
            using (MailMessage mailMessage = new MailMessage())
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mailMessage.From = new MailAddress(from_emailid);


                if (record_not_found != "NO")
                {
                   // string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                    //filename1 = path + "MorningEmail_" + unit_name + ".xls";
                    //   list4.Add(filename1);
                    //}
                    string gg1 = head_email_id;
                    list1.Add(gg1);
                    foreach (string mail in list1)
                    {
                        if (mail != "")
                        {

                                mailMessage.To.Add(mail.ToString());

                                //  mailMessage.Attachments.Add(new Attachment(filename1));
                                mailMessage.Subject = " for " + designation + " Employee for the month of " + Convert.ToDateTime(txttodate.Text).ToString("MM/yyyy") + " for " + unit_name + " ";
                                mailMessage.Body = body;
                                mailMessage.IsBodyHtml = true;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                                SmtpServer.EnableSsl = true;
                                SmtpServer.Send(mailMessage);
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send Successfully.');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            
         
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Not Send.');", true);
        }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send Successfully.');", true);
            if (record_not_found == "NO")
            {
                System.IO.File.Delete(filename1);
            }

            record_not_found = "YES";
        }
    }

    protected void btn_morningemail_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string where = "where ";
        if (ddl_client.SelectedValue != "Select")
        {
          //  gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 1);

            where = where + " pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' ";
            if (ddl_billing_state.SelectedValue != "Select")
            {
                where = where + " AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'";
            }
            if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0")
            {
                where = where + " AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }

            d.con.Open();

            try
            {
                 MySqlCommand cmdnew = new MySqlCommand("select head_email_id,pay_branch_mail_details.unit_code,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1)AS unit_name  from pay_branch_mail_details inner join  pay_unit_master on pay_branch_mail_details.unit_code=pay_unit_master.unit_code and pay_branch_mail_details.comp_code=pay_unit_master.comp_code   where pay_branch_mail_details.comp_code='" + Session["comp_code"].ToString() + "' and  pay_branch_mail_details.unit_code in (select unit_Code from pay_unit_master " + where + ")", d.con);
              //  MySqlCommand cmdnew = new MySqlCommand("SELECT DISTINCT(pay_attendance_muster.unit_code), head_email_id, cc_emailid, CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'unit_name' FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_branch_mail_details ON pay_attendance_muster.unit_code = pay_branch_mail_details.unit_code AND pay_attendance_muster.comp_code = pay_branch_mail_details.comp_code WHERE pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "' and pay_attendance_muster.month = " + hidden_month.Value + " and year = " + hidden_year.Value + "" + where, d.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                System.Data.DataTable DataTable = new System.Data.DataTable();
                DataTable.Load(drnew);
                d.con.Close();

                foreach (DataRow row in DataTable.Rows)
                {
                    head_email_id = row[0].ToString();
                    unit_code_email = row[1].ToString();
                   unit_name = row[2].ToString();
                    unit_name.Replace(" ", "_");
                    hidden_month.Value = txttodate.Text.Substring(0, 2);
                    hidden_year.Value = txttodate.Text.Substring(3);
                 
                    btn_morning_emailprocess_Click(null, null);
                        d.operation("update  pay_send_mail_details set attendance_shit='YES',feedback_form='NO',check_list='NO',ot_list='YES',flag='1', email_id='" + head_email_id + "' where month_year='" + txttodate.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + ddl_client.SelectedValue + "'and unit_code='" + ddl_unitcode.SelectedValue + "'");
                    
                }
            }
            catch (Exception ex) { ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Mail Not Send.');", true); }
            finally { d.con.Close(); }
        }
    }       

    //protected void btn_morningemailbody_Click(object sender, EventArgs e)
    //{
    //    int MONTH = Convert.ToInt32(txttodate.Text.Substring(0, 2));
    //    int YEAR = Convert.ToInt32(txttodate.Text.Substring(3));
    //    var firstDayOfMonth = new DateTime(YEAR, MONTH, 1);
    //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
    //    get_state();
    //    string logo_bajaj = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
    //    string logo = logo_bajaj + "Bajaj_Logo.png";
    //    string header = "<table border=1><tr><th style=border:none; colspan=3><img  src=" + logo + "></th><th colspan=1 style=font-size:17;font-weight:bold;>Vendor Name:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Services:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Branch Name:</th><th colspan=2></th></tr><tr><th colspan=3 style=border:none;></th><th colspan=1 style=font-size:17;font-weight:bold;>Branch Code:</th><th colspan=2></th></tr><tr><th colspan=6></th></tr><tr><th style=font-size:30;font-weight:bold;background-color:blue; colspan=6>SECURITY GUARD FEEDBACK FORM</th></tr><tr><th colspan=6></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;</th><th colspan=2></th><th colspan=1 style=font-size:17;font-weight:bold;>state:</th><th colspan=2>" + bajaj_state + "</th></tr><tr><th colspan=6></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;background-color:blue; width=100>Sl.No.</th><th colspan=2 style=font-size:17;font-weight:bold;background-color:blue;>Description of service quality</th><th colspan=1 style=font-size:17;font-weight:bold;background-color:blue;>Service Efficiency</th><th colspan=2 style=font-size:17;font-weight:bold;background-color:blue;>Scores by the Company</th></tr>";
    //    string item = "<tr><th colspan=1>1</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Uniform Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>2</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Hygiene Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>3</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Daily Register Maintenance</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>4</th><th colspan=2 style=font-size:17;font-weight:bold;>Personnel Visitors controlling Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>5</th><th colspan=2 style=font-size:17;font-weight:bold;>Manpower Deployment</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>6</th><th colspan=2 style=font-size:17;font-weight:bold;>Daily Manpower Dept. Liaison</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>7</th><th colspan=2 style=font-size:17;font-weight:bold;>Reliever Deployment Status</th><th colspan=1>100</th><th colspan=2></th></tr><tr><th colspan=1>8</th><th colspan=2 style=font-size:17;font-weight:bold;>Task Delivery Quality</th><th colspan=1>100</th><th colspan=2></th></tr>";
    //    string footer = "<tr><th colspan=1 style=font-size:17;font-weight:bold;>Sign of Authorize Signatory with Branch Stamp:</th><th colspan=5></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;>Designation:</th><th colspan=5></th></tr><tr><th colspan=1 style=font-size:17;font-weight:bold;>Contact No:</th><th colspan=5></th></tr>";
    //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //    string style = @"" + header + "" + item + "" + footer + "<style> .textmode { } </style>";
    //    string abc = stringWrite.ToString();
    //    string unitcode = Convert.ToString(Session["unit_code"] = ddl_unitcode.SelectedItem.Text.Replace(" ", "_"));
    //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
    //    string filename1 = path + "MorningEmail_" + unit_name + ".xls";
    //    System.IO.File.WriteAllText(filename1, style);
    //}

    protected void grd_company_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_company_files.UseAccessibleHeader = false;
            grd_company_files.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_attendace_pdf_Click(object sender, EventArgs e)
    {

        string where = " and ";
        if (ddl_client.SelectedValue != "Select")
        {
            hidden_month.Value = txttodate.Text.Substring(0, 2);
            hidden_year.Value = txttodate.Text.Substring(3);
            gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value), 1);

            where = where + " pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' ";
            if (ddl_billing_state.SelectedValue != "Select")
            {
                where = where + " AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'";
            }
            if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0")
            {
                where = where + " AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }

            d2.con.Open();

            try
            {
                string unt_code = "";
                // MySqlCommand cmdnew = new MySqlCommand("select head_email_id,pay_branch_mail_details.unit_code,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1)AS unit_name  from pay_branch_mail_details inner join  pay_unit_master on pay_branch_mail_details.unit_code=pay_unit_master.unit_code and pay_branch_mail_details.comp_code=pay_unit_master.comp_code   where pay_branch_mail_details.comp_code='" + Session["comp_code"].ToString() + "' and  pay_branch_mail_details.unit_code in (select unit_Code from pay_unit_master " + where + ")", d.con);
                MySqlCommand cmdnew = new MySqlCommand("SELECT DISTINCT(pay_attendance_muster.unit_code), head_email_id, cc_emailid, CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'unit_name' FROM pay_attendance_muster INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_branch_mail_details ON pay_attendance_muster.unit_code = pay_branch_mail_details.unit_code AND pay_attendance_muster.comp_code = pay_branch_mail_details.comp_code WHERE pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "'" + where, d2.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                System.Data.DataTable DataTable = new System.Data.DataTable();
                DataTable.Load(drnew);
                d2.con.Close();
                if (ddl_client.SelectedValue == "BAGICTM")
                {
                    foreach (DataRow row in DataTable.Rows)
                    {
                        head_email_id = row[1].ToString();
                        unit_code_email = row[0].ToString();
                        unt_code = unt_code + "'" + unit_code_email + "',";
                        unit_name = row[3].ToString();
                        unit_name.Replace(" ", "_");
                        //string db_emp_code = d.getsinglestring("select unit_code from pay_send_mail_details where UNIT_CODE ='" + unit_code_email + "' AND month_year='" + txttodate.Text + "'");
                        //if (db_emp_code == "")
                        //{
                        //    d.operation("INSERT INTO pay_send_mail_details(attendance_shit,feedback_form,check_list,ot_list,flag,email_id,comp_code,client_code,unit_code,month_year) VALUES('NO','NO','NO','NO','0','','" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txttodate.Text + "')");
                        //}
                    }
                    unit_code_email = unt_code.Substring(0, unt_code.Length - 1);
                    pdf_attendance(unit_code_email);
                }
                else
                {
                    foreach (DataRow row in DataTable.Rows)
                    {
                        //string unit_code = "";
                        head_email_id = row[1].ToString();
                        unit_code_email = "'" + row[0].ToString() + "'";
                        unit_name = row[3].ToString();
                        unit_name.Replace(" ", "_");
                        hidden_month.Value = txttodate.Text.Substring(0, 2);
                        hidden_year.Value = txttodate.Text.Substring(3);
                        cc_emailid = row[2].ToString();
                        //string db_emp_code = d.getsinglestring("select unit_code from pay_send_mail_details where UNIT_CODE =" + unit_code_email + " AND month_year='" + txttodate.Text + "'");

                        //if (db_emp_code == "")
                        //{
                        //    d.operation("INSERT INTO pay_send_mail_details(attendance_shit,feedback_form,check_list,ot_list,flag,email_id,comp_code,client_code,unit_code,month_year) VALUES('NO','NO','NO','NO','0','','" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txttodate.Text + "')");
                        //}
                        pdf_attendance(unit_code_email);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { d2.con.Close(); }
        }
        
    }
    protected void pdf_attendance( string unit_code)
    {
        string sql = "", pay_attendance_muster = "pay_attendance_muster";
        string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-01','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + hidden_year.Value + "-" + hidden_month.Value + "-01'), '%D %b %Y'))) as fromtodate";
        if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
        string start_date_common = get_start_date();
        string end_date = get_end_date();
        string ot_applicable = d.getsinglestring("Select ot_applicable from pay_client_master where client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["comp_code"].ToString() + "'");

        string downloadname = "Attendance_" + unit_name;
        int month = int.Parse(hidden_month.Value);
        int year = int.Parse(hidden_year.Value);
        if (start_date_common != "" && start_date_common != "1")
        {
            month = --month;
            if (month == 0) { month = 12; year = --year; }
        }
        else
        {
            start_date_common = "1";
        }


        if (ot_applicable == "1")
        {
            if (start_date_common != "" && start_date_common != "1")
            {
                daterange = "concat(upper(DATE_FORMAT(str_to_date('" + (int.Parse(hidden_month.Value) - 1 == 0 ? (int.Parse(hidden_year.Value) - 1).ToString() : hidden_year.Value) + "-" + (int.Parse(hidden_month.Value) - 1 == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
                sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 1, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 3, 1) + " round(pay_attendance_muster.tot_days_present,0) as 'tot_days_present',  round(pay_attendance_muster.tot_days_absent,0) as absent,day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days',LocationHead_Name,LocationHead_mobileno,pay_ot_muster.TOT_OT from pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code  AND pay_attendance_muster.month =  '" + hidden_month.Value + "'   AND pay_attendance_muster.Year = '" + hidden_year.Value + "' INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code   left JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE  INNER JOIN pay_company_master ON pay_unit_master.comp_code = pay_company_master.comp_code  INNER JOIN pay_ot_muster ON  pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code  AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE  AND pay_attendance_muster.MONTH = pay_ot_muster.MONTH  AND pay_attendance_muster.YEAR = pay_ot_muster.YEAR  AND pay_ot_muster.month =  '" + hidden_month.Value + "'  LEFT JOIN pay_attendance_muster t2   ON " + year + "= t2.year AND pay_company_master.COMP_CODE = t2.COMP_CODE  AND pay_unit_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE  AND t2.month ='" + month + "' LEFT OUTER JOIN pay_ot_muster t3   ON " + year + " = t3.YEAR  AND pay_unit_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_company_master.COMP_CODE = t3.COMP_CODE   AND t3.month = '" + month + "' WHERE pay_company_master.comp_code =  '" + Session["comp_code"].ToString() + "'  AND pay_unit_master.unit_code in("+unit_code+") group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
            }
            else
            {
                sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31,OT_DAY01 , OT_DAY02 , OT_DAY03 , OT_DAY04 , OT_DAY05 , OT_DAY06 , OT_DAY07 , OT_DAY08 , OT_DAY09 , OT_DAY10 , OT_DAY11 , OT_DAY12 , OT_DAY13 , OT_DAY14 , OT_DAY15 , OT_DAY16 , OT_DAY17 , OT_DAY18 , OT_DAY19 , OT_DAY20 , OT_DAY21 , OT_DAY22 , OT_DAY23 , OT_DAY24 , OT_DAY25 , OT_DAY26 , OT_DAY27 , OT_DAY28 , OT_DAY29 , OT_DAY30 , OT_DAY31,'1' as 'HDAY01','2' as 'HDAY02','3' as 'HDAY03','4' as 'HDAY04','5' as 'HDAY05','6' as 'HDAY06','7' as 'HDAY07','8' as 'HDAY08','9' as 'HDAY09','10' as 'HDAY10','11' as 'HDAY11','12' as 'HDAY12','13' as 'HDAY13','14' as 'HDAY14','15' as 'HDAY15','16' as 'HDAY16','17' as 'HDAY17','18' as 'HDAY18','19' as 'HDAY19','20' as 'HDAY20','21' as 'HDAY21','22' as 'HDAY22','23' as 'HDAY23','24' as 'HDAY24','25' as 'HDAY25','26' as 'HDAY26','27' as 'HDAY27','28' as 'HDAY28','29' as 'HDAY29','30' as 'HDAY30','31' as 'HDAY31',TOT_OT,  round(pay_attendance_muster.tot_days_present,0) as 'tot_days_present',round(pay_attendance_muster.tot_days_absent,0) AS 'absent', DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) AS 'total days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code INNER JOIN pay_ot_muster ON pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE and pay_attendance_muster.MONTH = pay_ot_muster.MONTH and pay_attendance_muster.YEAR = pay_ot_muster.YEAR where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in (" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "' group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
            }

        }
        else
        {
            if (start_date_common != "" && start_date_common != "1")
            {
                daterange = "concat(upper(DATE_FORMAT(str_to_date('" + (int.Parse(hidden_month.Value) - 1 == 0 ? (int.Parse(hidden_year.Value) - 1).ToString() : hidden_year.Value) + "-" + (int.Parse(hidden_month.Value) - 1 == 0 ? 12 : (int.Parse(hidden_month.Value) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";
                if (ddl_client.SelectedValue == "RLIC HK" && int.Parse(hidden_month.Value) == 1 && int.Parse(hidden_year.Value) == 2019) { daterange = "concat(upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 1 + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + hidden_year.Value + "-" + hidden_month.Value + "-" + 20 + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate"; }
                sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location , pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, " + d.get_calendar_days(int.Parse(start_date_common), hidden_month.Value + "/" + hidden_year.Value, 6, 1) + " CAST( round(pay_attendance_muster.tot_days_present,0)AS char) as 'tot_days_present',  CAST( round(pay_attendance_muster.tot_days_absent,0) AS char) as absent,CAST(day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y')))AS char) as 'total_days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code left join pay_attendance_muster t2 on  t2.year=" + (int.Parse(hidden_month.Value) == 1 ? int.Parse(hidden_year.Value) - 1 : int.Parse(hidden_year.Value)) + " and pay_employee_master.COMP_CODE = t2.COMP_CODE and pay_employee_master.UNIT_CODE = t2.UNIT_CODE and pay_employee_master.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(hidden_month.Value) == 1 ? 12 : int.Parse(hidden_month.Value) - 1) + " where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in(" + unit_code + ")  and  pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
            }
            else
            {
                int days = DateTime.DaysInMonth(year, month);

                if (days == 28)
                {
                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28,'1' as 'HDAY01','2' as 'HDAY02','3' as 'HDAY03','4' as 'HDAY04','5' as 'HDAY05','6' as 'HDAY06','7' as 'HDAY07','8' as 'HDAY08','9' as 'HDAY09','10' as 'HDAY10','11' as 'HDAY11','12' as 'HDAY12','13' as 'HDAY13','14' as 'HDAY14','15' as 'HDAY15','16' as 'HDAY16','17' as 'HDAY17','18' as 'HDAY18','19' as 'HDAY19','20' as 'HDAY20','21' as 'HDAY21','22' as 'HDAY22','23' as 'HDAY23','24' as 'HDAY24','25' as 'HDAY25','26' as 'HDAY26','27' as 'HDAY27','28' as 'HDAY28', cast(round(tot_days_present,0) as char) as 'tot_days_present', cast(round(tot_days_absent,0) as char) as absent,cast(DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as char) as 'total_days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in(" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                }
                else if (days == 29)
                {
                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29,'1' as 'HDAY01','2' as 'HDAY02','3' as 'HDAY03','4' as 'HDAY04','5' as 'HDAY05','6' as 'HDAY06','7' as 'HDAY07','8' as 'HDAY08','9' as 'HDAY09','10' as 'HDAY10','11' as 'HDAY11','12' as 'HDAY12','13' as 'HDAY13','14' as 'HDAY14','15' as 'HDAY15','16' as 'HDAY16','17' as 'HDAY17','18' as 'HDAY18','19' as 'HDAY19','20' as 'HDAY20','21' as 'HDAY21','22' as 'HDAY22','23' as 'HDAY23','24' as 'HDAY24','25' as 'HDAY25','26' as 'HDAY26','27' as 'HDAY27','28' as 'HDAY28','29' as 'HDAY29', cast(round(tot_days_present,0) as char) as 'tot_days_present', cast(round(tot_days_absent,0) as char) as absent,cast(DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as char) as 'total_days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in(" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                }
                else
                {
                    sql = "select (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name,pay_unit_master.client_code,pay_unit_master.comp_code,concat(pay_unit_master.unit_add1,' , ',pay_unit_master.unit_city,' , ',pay_unit_master.state_name) as location ,pay_unit_master.state_name, pay_unit_master.client_branch_code, pay_unit_master.unit_add2, " + daterange + ", pay_company_master.company_name, pay_company_master.address1, pay_company_master.PF_REG_NO, pay_company_master.company_pan_no, (select FIELD3 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'ESIC' and Field1=pay_company_master.state) as ESIC, (select FIELD2 from pay_zone_master where comp_code = pay_company_master.comp_code and type = 'GST' and client_Code is null) as GST, pay_employee_master.emp_name, pay_grade_master.grade_desc, date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30,(CASE WHEN cast(DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as char) = '31' THEN (CASE WHEN DAY31 = '1' THEN 'A' ELSE DAY31 END) ELSE '' END) AS DAY31,'1' as 'HDAY01','2' as 'HDAY02','3' as 'HDAY03','4' as 'HDAY04','5' as 'HDAY05','6' as 'HDAY06','7' as 'HDAY07','8' as 'HDAY08','9' as 'HDAY09','10' as 'HDAY10','11' as 'HDAY11','12' as 'HDAY12','13' as 'HDAY13','14' as 'HDAY14','15' as 'HDAY15','16' as 'HDAY16','17' as 'HDAY17','18' as 'HDAY18','19' as 'HDAY19','20' as 'HDAY20','21' as 'HDAY21','22' as 'HDAY22','23' as 'HDAY23','24' as 'HDAY24','25' as 'HDAY25','26' as 'HDAY26','27' as 'HDAY27','28' as 'HDAY28','29' as 'HDAY29','30' as 'HDAY30',(CASE WHEN cast(DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as char) = '31' THEN '31' ELSE '' END) AS 'HDAY31', cast(round(tot_days_present,0) as char) as 'tot_days_present', cast(round(tot_days_absent,0) as char) as absent,cast(DAY(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-1')) as char) as 'total_days',LocationHead_Name,LocationHead_mobileno from pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE inner join pay_company_master on pay_unit_master.comp_code=pay_company_master.comp_code where pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.unit_code in(" + unit_code + ") and pay_attendance_muster.month = '" + hidden_month.Value + "' and pay_attendance_muster.Year = '" + hidden_year.Value + "'  and pay_attendance_muster.flag = 0 group by pay_employee_master.emp_code ORDER BY pay_employee_master.EMP_CODE";
                }
            }
           

        }
        ReportDocument crystalReport = new ReportDocument();
        System.Data.DataTable dt = new System.Data.DataTable();
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        MySqlCommand cmd = new MySqlCommand(sql, d.con);
        MySqlDataReader sda = null;
        d.con.Open();
        try
        {
            sda = cmd.ExecuteReader();
            dt.Load(sda);

            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Email Not Sent!!');", true);
                return;
            }

            crystalReport.Load(Server.MapPath("~/emp_sample_attendance.rpt"));
            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
            crystalReport.Close();
            crystalReport.Clone();
            crystalReport.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string from_emailid = "";
            string password = "";
            d1.con.Open();
            System.Data.DataTable DataTable1 = new System.Data.DataTable();
            try
            {
                MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;select cast(group_concat(distinct head_email_id) as char), head_name, client_code,comp_code,state from pay_branch_mail_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = " + unit_code + "", d1.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                DataTable1.Load(drnew);
                d1.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }
            foreach (DataRow row in DataTable1.Rows)
            {
                d1.con.Open();
                try
                {
                    MySqlCommand cmd1 = new MySqlCommand("select email_id,password from pay_client_master where  client_code='" + ddl_client.SelectedValue + "' ", d1.con);
                    MySqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        from_emailid = dr.GetValue(0).ToString();
                        password = dr.GetValue(1).ToString();
                    }
                    dr.Close();
                    d1.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally { d1.con.Close(); }
                if (!(from_emailid == "") || !(password == ""))
                {
                    string mail_body = d.getsinglestring("select group_concat(Field4,'<br>Asst. Manager- ',Field5,'<br>''" + comp_name + "''<br><span style = \"color:red\">(An ISO 9001-2008 Certified Company)</span><br>304, 3rd Floor, Nyati Millennium,Viman Nagar, Pune-411014 <br>Tel:', Field6 ) as 'aa'  from pay_zone_master  where   Field1 ='Admin' and  type='client_Email' and comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'");
                    string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam,</td></tr><tr><td style = \"font-family:Georgia;font-size:12pt;\">Greetings from IH&MS...!!!</td></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Attached herewith the attendance for the Month of " + txttodate.Text + ".</p></tr><tr><p><span style='font-family:Georgia,serif;color:#00B0F0'>Considering present situation of COVID – 19, We are unable to take scan copy of attendance. Request you to please approve attendance on mail for further process. Kindly confirm employees name and present days on mail body.</span></p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Please consider the excel file and get it corrected if required. Also it is compulsory to send  the scan copy of the register with in & out timing of the employees.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly send the same and also attach the scan copy of in and out attendance register and send ASAP with the Signature of Branch Head & Branch's Stamp.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration: underline;\">Note:-   Please take care with the below mention notes. As it is mandatory.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">1)Please mention in & out time by manually in attendance sheet. <br>2)Please use round stamp with full address stamp for <span style = \"font-family:Georgia;font-size:10pt;\">" + ddl_client.SelectedItem.Text + " </span>on  Attendance Sheet. <br> 3) Attendance is valid only by branch Manager Sign.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration:underline;\">Note :- Please send the attendance sheet with clear print if it is not clear i will not  mention the attendance sheet.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly note if there is no stamp available at the branch  Please give us the confirmation over the mail regarding the non availability of official stamp and the HK employees total working days along with the attached attendance format.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt\">Thanks & Regards</p></tr><tr><p style = \"font-size:10pt;\">" + mail_body + "</p></tr>";


                    using (MailMessage mailMessage = new MailMessage())
                    {
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mailMessage.From = new MailAddress(from_emailid);

                        if (row[0].ToString() != "")
                        {
                            mailMessage.To.Add(row[0].ToString());
                            mailMessage.Subject = "IH&MS - Attendance for ";
                            mailMessage.Body = body;
                            mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf"));
                            mailMessage.IsBodyHtml = true;
                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                            SmtpServer.EnableSsl = true;
                            try
                            {
                                SmtpServer.Send(mailMessage);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Email Sent successfully!!');", true);

                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Email Not Sent!!');", true);
                            }

                        }
                    }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            //dt.Close();
        }
    }
   
    protected void attachment_gv_PreRender(object sender, EventArgs e)
    {
        try
        {
            attachment_gv.UseAccessibleHeader = false;
            attachment_gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try {

            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            //vinod

            if (fileExt.ToUpper() == ".XLS" || fileExt.ToUpper() == ".XLSX" || fileExt.ToUpper() == ".PDF" ) 
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Downloads/") + fileName);
               // File.Copy(Server.MapPath("~/Downloads/") + fileName, Server.MapPath("~/Downloads/"), true);
                //File.Delete(Server.MapPath("~/Downloads/") + fileName);

                int res = d.operation("insert into pay_feedback_files(client_code,File_name,Type,Uploaded_date,Uploaded_by,filename_and)values('" + ddl_client_attach.SelectedValue + "','" + fileName + "','Feedback',now(),'" + Session["USERNAME"].ToString() + "','" + ddl_filename_and.SelectedValue + "' ) ");
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File upload Successfully... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File upload Failed... !!!');", true);
                }
                attachment_gridview();
                
            }
        }
        catch (Exception ex) { throw ex; }
        finally { }
        

    }

    protected void attachment_gridview() 
    {

        try
        {
            MySqlDataAdapter attachment = null;
            attachment = new MySqlDataAdapter("SELECT  pay_feedback_files.Id,client_name,File_name,concat('~/Downloads/',file_name) as Value,Type,date_format(Uploaded_date,'%d/%m/%Y') as Uploaded_date, Uploaded_by, filename_and FROM pay_feedback_files INNER JOIN pay_client_master ON pay_feedback_files.client_code = pay_client_master.client_code where comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con);

            d.con.Open();
            DataTable dt_attach = new DataTable();
            attachment.Fill(dt_attach);
            if (dt_attach.Rows.Count > 0)
            {
                ViewState["Billing_Type"] = dt_attach;
                attachment_gv.DataSource = dt_attach;
                attachment_gv.DataBind();

            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    }
    protected void lnk_remove_attach_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_feedback_files where Id = '" + grdrow.Cells[0].Text + "'");
        try
        {
            File.Delete(Server.MapPath("~/Downloads/") + grdrow.Cells[3].Text);
        }
        catch { }

        attachment_gridview();

        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow row = (GridViewRow)lb.NamingContainer;
        //int rowID = row.RowIndex;
        //if (ViewState["Billing_Type"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["Billing_Type"];
        //    if (dt.Rows.Count >= 1)
        //    {
        //        if (row.RowIndex < dt.Rows.Count)
        //        {
        //            dt.Rows.Remove(dt.Rows[rowID]);
        //        }
        //    }
        //    ViewState["Billing_Type"] = dt;
        //    attachment_gv.DataSource = dt;
        //    attachment_gv.DataBind();
       // }
    }
    protected void attachment_gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }

            if (e.Row.Cells[5].Text == "0") { e.Row.Cells[5].Text = "Nothing"; }
            else if (e.Row.Cells[5].Text == "1") { e.Row.Cells[5].Text = "Branch"; }
            else if (e.Row.Cells[5].Text == "2") { e.Row.Cells[5].Text = "State"; }
        }
        

        e.Row.Cells[0].Visible = false;
    }
}

