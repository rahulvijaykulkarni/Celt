using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class salary_paid_report : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    public static string month_name = "";
    public int arrears_invoice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }
        if (!IsPostBack)
        {
            txt_month_year.Text = d.getCurrentMonthYear();
            client_name();
            // gridview();
            ddl_client_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client_name.DataSource = dt_item;
                    ddl_client_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_client_name.DataValueField = dt_item.Columns[1].ToString();
                    ddl_client_name.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_client_name.Items.Insert(0, "ALL");
                ddl_state.Items.Insert(0, "ALL");
                ddl_unit.Items.Insert(0, "ALL");

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }
    protected void client_name()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "'  ORDER BY client_code", d.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
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
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  ORDER BY UNIT_CODE", d.con);//and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" +ddl_billing_state.SelectedValue + "') 
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
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unitcode.Items.Insert(0, "ALL");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
           
            //State
            ddl_billing_state.Items.Clear();
            dt_item = new System.Data.DataTable();
            cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  order by 1", d.con);//and state_Name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "')
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
                ddl_billing_state.Items.Insert(0, "ALL");
                ddl_billing_state_SelectedIndexChanged(null, null);
                gridview();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
            
    }
    protected void ddl_billing_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        if (ddl_billing_state.SelectedValue != "ALL")
        {
            cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "' AND branch_status = 0 ORDER BY UNIT_NAME", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND branch_status = 0 ORDER BY UNIT_NAME", d.con);
        }
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
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_unitcode.Items.Insert(0, "ALL");
            gridview();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    
    protected void gridview()
    {
        try
        {
            salary_gridview.DataSource = null;
            salary_gridview.DataBind();

            d.con.Open();
            //MySqlDataAdapter cd_cmd = null;

            //cd_cmd = new MySqlDataAdapter("SELECT  pay_pro_master.state_name, pay_pro_master.unit_city, pay_pro_master.unit_name,CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, CASE designation WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', pay_pro_master.fine, EMP_ADVANCE_PAYMENT,pay_pro_master.DEDUCTION, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no,  pay_pro_master.date, (payment - pay_pro_master.fine -deduction) as payment, pay_pro_master.month, pay_pro_master.year,case when payment_Status =1 then 'Paid' when payment_Status =2 then 'Returned' else 'Process' END as Status FROM pay_pro_master LEFT OUTER JOIN pay_employee_salary_details ON  pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year WHERE   (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And (salary_type is null || salary_type ='') order by 1,2,3,4 ", d.con1);

            //if (ddl_billing_state.SelectedValue != "ALL")
            //{
            //    cd_cmd = new MySqlDataAdapter("SELECT  pay_pro_master.state_name, pay_pro_master.unit_city, pay_pro_master.unit_name,CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, CASE designation WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', pay_pro_master.fine, EMP_ADVANCE_PAYMENT,pay_pro_master.DEDUCTION, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no,  pay_pro_master.date, (payment - pay_pro_master.fine -deduction) as payment, pay_pro_master.month, pay_pro_master.year,case when payment_Status =1 then 'Paid' when payment_Status =2 then 'Returned' else 'Process' END as Status FROM pay_pro_master LEFT OUTER JOIN pay_employee_salary_details ON  pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year WHERE   (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And pay_pro_master.state_name='" + ddl_billing_state.SelectedValue + "' and  (salary_type is null || salary_type ='') order by 1,2,3,4 ", d.con1);
            //}
            //if (ddl_unitcode.SelectedValue != "ALL")
            //{
            //    cd_cmd = new MySqlDataAdapter("SELECT  pay_pro_master.state_name, pay_pro_master.unit_city, pay_pro_master.unit_name,CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, CASE designation WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', pay_pro_master.fine, EMP_ADVANCE_PAYMENT,pay_pro_master.DEDUCTION, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no,  pay_pro_master.date, (payment - pay_pro_master.fine -deduction) as payment, pay_pro_master.month, pay_pro_master.year,case when payment_Status =1 then 'Paid' when payment_Status =2 then 'Returned' else 'Process' END as Status FROM pay_pro_master LEFT OUTER JOIN pay_employee_salary_details ON  pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year WHERE   (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And pay_pro_master.state_name='" + ddl_billing_state.SelectedValue + "' and pay_pro_master.unit_code='" + ddl_unitcode.SelectedValue + "' and  (salary_type is null || salary_type ='') order by 1,2,3,4 ", d.con1);
            //}


            string where = "  (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And (salary_type is null || salary_type ='') order by 1,2,3,4";
            if (ddl_billing_state.SelectedValue != "ALL")
            {
                where = " (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And pay_pro_master.state_name='" + ddl_billing_state.SelectedValue + "' and  (salary_type is null || salary_type ='') order by 1,2,3,4";
            }
            if (ddl_unitcode.SelectedValue != "ALL")
            {
                where = " (BANK_EMP_AC_CODE IS NOT NULL && trim(BANK_EMP_AC_CODE) != '') AND (Bank_holder_name IS NOT NULL && trim(Bank_holder_name) != '') AND (PF_IFSC_CODE IS NOT NULL && trim(PF_IFSC_CODE) != '') and pay_pro_master.STATUS != 'LEFT'  And pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' And pay_pro_master.state_name='" + ddl_billing_state.SelectedValue + "' and pay_pro_master.unit_code='" + ddl_unitcode.SelectedValue + "' and  (salary_type is null || salary_type ='') order by 1,2,3,4 ";
            }

            MySqlDataAdapter cd_cmd = new MySqlDataAdapter("SELECT pay_pro_master.state_name, pay_pro_master.unit_city, pay_pro_master.unit_name,CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, CASE designation WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', pay_pro_master.fine, EMP_ADVANCE_PAYMENT,pay_pro_master.DEDUCTION, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no,  pay_pro_master.date, (payment - pay_pro_master.fine -deduction) as payment, pay_pro_master.month, pay_pro_master.year,case when payment_Status =1 then 'Paid' when payment_Status =2 then 'Returned' else 'Process' END as Status, date_format(salary_date,'%d/%m/%Y') as salary_date FROM pay_pro_master LEFT OUTER JOIN pay_employee_salary_details ON  pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year WHERE " + where, d.con1);


            DataTable dt_cmd = new DataTable();
            cd_cmd.Fill(dt_cmd);
            if (dt_cmd.Rows.Count > 0)
            {
                salary_gridview.DataSource = dt_cmd;
                salary_gridview.DataBind();
            }
        }

        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridview();
    }
    protected void salary_gridview_PreRender(object sender, EventArgs e)
    {
        try
        {
            // ClientGridView.UseAccessibleHeader = false;
            salary_gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
    protected string get_start_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and  pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1),(SELECT start_date_common FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1))");
    }
    protected string get_end_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and  pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1),(SELECT end_date_common FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1))");

    }
    protected void btn_attendance_Click(object sender, EventArgs e)
    {
        
        try
        {
            int month_days = 0;
            int i = 3;
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            month_name = mfi.GetMonthName(int.Parse(txt_month_year.Text.Substring(0, 2))).ToString();
            month_name = month_name + " " + txt_month_year.Text.Substring(3).ToUpper();

            // month_days = DateTime.DaysInMonth(int.Parse(txt_month_year.Text.Substring(3)), int.Parse(txt_month_year.Text.Substring(0, 2)));
            string where = "";
            string order_by_clause = "   ORDER BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name,pay_billing_unit_rate_history.emp_name";
            string grade = "";
            string pay_attendance_muster = " pay_attendance_muster ", pay_billing_master_history = "pay_billing_master_history", pay_billing_unit_rate = "pay_billing_unit_rate";
            string start_date_common = get_start_date();
            string sql = null, flag = "and pay_attendance_muster.flag != 0 ";
            string bill_date = "", billing_type = "And (bill_type is null || bill_type ='')";
          
            //string sql = null, flag = "and pay_attendance_muster.flag != 0 ";
            {
                if (start_date_common != "" && start_date_common != "1")
                {
                    //d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text, int.Parse(start_date_common));
                    where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  " + flag + " " + grade ;
                    if (ddl_billing_state.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0 " + flag + " " + grade;
                    }
                    else if (ddl_unitcode.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + flag + " " + grade;
                    }
                    string getdays = "";
                        getdays = d.get_calendar_days(int.Parse(start_date_common), txt_month_year.Text, 1, 2);
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
                    if (!getdays.Contains("DAY28"))
                    {
                        getdays = getdays + " 0 as 'DAY28',";
                    }
                    sql = "select pay_billing_unit_rate_history.state_name,branch_type, pay_billing_unit_rate_history.unit_city,pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.client_branch_code, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc,pay_attendance_muster.ot_hours ," + getdays + " pay_attendance_muster.tot_days_present, pay_attendance_muster.tot_days_absent as absent, pay_attendance_muster.tot_working_days as 'total days',IF(pay_employee_master.LEFT_DATE IS NULL, 'CONTINUE', 'LEFT') AS 'STATUS' from pay_billing_unit_rate_history INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code and pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_code AND   pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code   AND  pay_attendance_muster . month  =  pay_billing_unit_rate_history . month  AND  pay_attendance_muster . year  =  pay_billing_unit_rate_history . year INNER JOIN pay_employee_master ON pay_employee_master.COMP_CODE = pay_attendance_muster.COMP_CODE AND pay_employee_master.UNIT_CODE = pay_attendance_muster.UNIT_CODE AND pay_employee_master.EMP_CODE = pay_attendance_muster.EMP_CODE  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(txt_month_year.Text.Substring(0, 2)) == 1 ? int.Parse(txt_month_year.Text.Substring(3)) - 1 : int.Parse(txt_month_year.Text.Substring(3))) + " and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(txt_month_year.Text.Substring(0, 2)) == 1 ? 12 : int.Parse(txt_month_year.Text.Substring(0, 2)) - 1) + " where " + where + " " + order_by_clause;

                }
                else
                {
                    where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  " + flag + "  " + grade ;
                    if (ddl_billing_state.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  " + flag + "  " + grade ;
                    }
                    else if (ddl_unitcode.SelectedValue == "ALL")
                    {
                        where = "pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0 " + flag + "  " + grade ;
                    }
                    sql = "select pay_billing_unit_rate_history.state_name, branch_type, pay_billing_unit_rate_history.unit_city,pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.client_branch_code, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc,pay_attendance_muster.ot_hours , case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster.tot_days_present, CASE WHEN (pay_attendance_muster.tot_working_days - pay_attendance_muster.tot_days_present) < 0 THEN 0 ELSE pay_attendance_muster.tot_working_days - pay_attendance_muster.tot_days_present END AS 'absent',DAY(LAST_DAY('" + txt_month_year.Text.Substring(3) + "-" + txt_month_year.Text.Substring(0, 2) + "-1')) AS 'total days', IF(pay_employee_master.LEFT_DATE IS NULL, 'CONTINUE', 'LEFT') AS 'STATUS' from pay_billing_unit_rate_history INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code and pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_code  and pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate_history.month AND pay_attendance_muster.year = pay_billing_unit_rate_history.year  INNER JOIN pay_employee_master ON pay_employee_master.COMP_CODE = pay_attendance_muster.COMP_CODE AND pay_employee_master.UNIT_CODE = pay_attendance_muster.UNIT_CODE AND pay_employee_master.EMP_CODE = pay_attendance_muster.EMP_CODE where " + where + " " + order_by_clause;

                }
            }
                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=CLIENT_ATTENDANCE_" + ddl_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");

                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Repeater Repeater1 = new Repeater();
                    Repeater1.DataSource = ds;
                    Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, i, start_date_common, month_days, d.get_calendar_days(int.Parse(start_date_common), txt_month_year.Text, 0, 2));
                    Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, i, start_date_common, month_days,"");
                    Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, i,  start_date_common, month_days,"");
                    //(ListItemType type, DataSet ds, int i, string start_date_common, int month_days)
                    Repeater1.DataBind();

                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    Repeater1.RenderControl(htmlWrite);

                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(stringWrite.ToString());
                    Response.Flush();
                    Response.End();
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
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr = 0;

        //i3  is use to row data
        int i, i3 = 2, state_change = 0, month_days = 0;
        string invoice = "";
        string bill_date = "";
        double rate = 0, paid_days = 0, service_charge = 0, grand_tot = 0, cgst = 0, sgst = 0, igst = 0, gst = 0, ctc = 0, present_days = 0, absent_days = 0, total_days = 0, ot_hrs = 0, ot_rate = 0, ot_amount = 0, sub_total = 0, total_emp_count = 0, no_of_duties = 0;

        double rate1 = 0, paid_days1 = 0, service_charge1 = 0, grand_tot1 = 0, cgst1 = 0, sgst1 = 0, igst1 = 0, gst1 = 0, ctc1 = 2, present_days1 = 0, absent_days1 = 0, total_days1 = 0, ot_hrs1 = 0, ot_rate1 = 0, ot_amount1 = 0, sub_total1 = 0, total_emp_count1 = 0, no_of_duties1 = 0;

        //ADD MD 
        string DUTY_HOURS = null, RATE = null, NO_OF_PAID_DAYS = null, BASE_AMOUNT = null, OT_HOURS = null, OT_RATE = null, OT_AMOUNT = null, TOTAL_BASE_AMT_OT_AMT = null, SERVICE_CHARGE = null, GRAND_TOTAL = null, CGST = null, SGST = null, IGST = null, TOTAL_GST = null, TOTAL_CTC = null;
        string header = "", header1 = "", state_name = "";
        string bodystr = "", start_date_common = "", branch_type = "";
        public MyTemplate(ListItemType type, DataSet ds, int i, string start_date_common, int month_days, string header1)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            
            this.start_date_common = start_date_common;
            this.header1 = header1;
            this.month_days = month_days;
            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {

                case ListItemType.Header:
                    if (i == 3)
                    {
                        header = "<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th>";
                        int daysadd = 0;
                        int colspan = 39;
                        int days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                        if (month_days > 0)
                        {
                            days = month_days;
                        }
                        if (days == 29)
                        { header = header + "<th>29</th>"; daysadd = 1; colspan = 40; }
                        else if (days == 30)
                        {
                            header = header + "<th>29</th><th>30</th>"; daysadd = 2;
                            colspan = 41;
                        }
                        else if (days == 31)
                        {
                            header = header + "<th>29</th><th>30</th><th>31</th>";
                            daysadd = 3;
                            colspan = 42;
                        }
                        if (start_date_common != "" && start_date_common != "1")
                        {
                            if (month_days == 0)
                            {
                                header = header1;
                            }
                        }
                        
                        //lc = new LiteralControl("<table border=1><tr><th colspan=" + colspan + " bgcolor=yellow align=center>CLIENT ATTENDANCE</th></tr><tr></tr><tr><th>SL. <br style=\"mso-data-placement:same-cell;\">NO.</th><th>STATE</th><th>LOCATION</th><th>BRANCH<br style=\"mso-data-placement:same-cell;\"> CODE</th><th>NAME</th><th>DEG.</th><th>OT <br style=\"mso-data-placement:same-cell;\">HRS.</th>" + header + "<th>TOTAL <br style=\"mso-data-placement:same-cell;\">P/DAY</th><th>ABSENT<br style=\"mso-data-placement:same-cell;\"> DAY</th><th>TOTAL DAYS</th><th>STATUS</th></tr>");
                        lc = new LiteralControl("<table border=1><tr><th colspan=" + colspan + " bgcolor=yellow align=center>ATTENDANCE COPY  FOR " + salary_paid_report.month_name.ToUpper() + "</th></tr><tr></tr><tr><th>SL. <br style=\"mso-data-placement:same-cell;\">NO.</th><th>STATE</th><th>LOCATION</th><th>BRANCH<br style=\"mso-data-placement:same-cell;\"> CODE</th><th>NAME</th><th>DEG.</th><th>OT <br style=\"mso-data-placement:same-cell;\">HRS.</th>" + header + "<th>TOTAL <br style=\"mso-data-placement:same-cell;\">P/DAY</th><th>ABSENT<br style=\"mso-data-placement:same-cell;\"> DAY</th><th>TOTAL DAYS</th><th>STATUS</th></tr>");
                        header = "";

                    }
                    break;
                case ListItemType.Item:
                    if (i == 3)
                    {
                        string color = "";
                        bodystr = "";
                        int start_first_row = 3;
                        if (ds.Tables[0].Rows[ctr]["DAY01"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY01"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY02"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY02"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY03"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY03"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY04"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY04"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY05"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY05"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY06"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY06"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY07"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY07"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY08"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY08"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY09"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY09"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY10"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY10"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY11"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY11"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY12"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY12"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY13"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY13"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY14"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY14"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY15"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY15"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY16"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY16"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY17"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY17"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY18"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY18"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY19"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY19"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY20"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY20"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY21"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY21"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY22"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY22"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY23"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY23"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY24"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY24"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY25"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY25"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY26"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY26"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY27"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY27"] + "</td>";
                        if (ds.Tables[0].Rows[ctr]["DAY28"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY28"] + "</td>";

                        int days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                        if (month_days > 0)
                        {
                            days = month_days;
                        }

                        if (days == 29)
                        {
                            if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                            //bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>"; 
                        }
                        else if (days == 30)
                        {
                            if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                            if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";

                            // bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                        }
                        else if (days == 31)
                        {
                            if (ds.Tables[0].Rows[ctr]["DAY29"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>";
                            if (ds.Tables[0].Rows[ctr]["DAY30"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>";
                            if (ds.Tables[0].Rows[ctr]["DAY31"].ToString() == "A") { color = "red"; } else { color = "white"; } bodystr = bodystr + "<td bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>";

                            //  bodystr = "<td>" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>";
                        }
                        int count = bodystr.Split('A').Length - 1;
                        string present_days1 = (days == 31 ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AL" + (ctr + start_first_row) + ",\"P\")+COUNTIF(H" + (ctr + start_first_row) + ":AL" + (ctr + start_first_row) + ",\"PH\")+COUNTIF(H" + (ctr + start_first_row) + ":AL" + (ctr + start_first_row) + ",\"HD\")/2)" : (days == 28) ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AI" + (ctr + start_first_row) + ",\"P\")+COUNTIF(H" + (ctr + start_first_row) + ":AI" + (ctr + start_first_row) + ",\"PH\")+COUNTIF(H" + (ctr + start_first_row) + ":AI" + (ctr + start_first_row) + ",\"HD\")/2)" : (days == 29) ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AJ" + (ctr + start_first_row) + ",\"P\")+COUNTIF(H" + (ctr + start_first_row) + ":AJ" + (ctr + start_first_row) + ",\"PH\")+COUNTIF(H" + (ctr + start_first_row) + ":AJ" + (ctr + start_first_row) + ",\"HD\")/2)" : "=SUM(COUNTIF(H" + (ctr + start_first_row) + ":AK" + (ctr + start_first_row) + ",\"P\")+COUNTIF(H" + (ctr + start_first_row) + ":AK" + (ctr + start_first_row) + ",\"PH\")+COUNTIF(H" + (ctr + start_first_row) + ":AK" + (ctr + start_first_row) + ",\"HD\")/2)");
                        string absent_days1 = (days == 31 ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AL" + (ctr + start_first_row) + ",\"A\")+COUNTIF(H" + (ctr + start_first_row) + ":AL" + (ctr + start_first_row) + ",\"HD\")/2)" : (days == 28) ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AI" + (ctr + start_first_row) + ",\"A\")+COUNTIF(H" + (ctr + start_first_row) + ":AI" + (ctr + start_first_row) + ",\"HD\")/2)" : (days == 29) ? " = SUM(COUNTIF(H" + (ctr + start_first_row) + ":AJ" + (ctr + start_first_row) + ",\"A\")+COUNTIF(H" + (ctr + start_first_row) + ":AJ" + (ctr + start_first_row) + ",\"HD\")/2)" : "=SUM(COUNTIF(H" + (ctr + start_first_row) + ":AK" + (ctr + start_first_row) + ",\"A\")+COUNTIF(H" + (ctr + start_first_row) + ":AK" + (ctr + start_first_row) + ",\"HD\")/2)");

                        //int absent = Convert.ToInt32 (bodystr.Contains("A"));
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"].ToString().ToUpper() + "</td>" + bodystr + "<td>" + (month_days == 0 ? ds.Tables[0].Rows[ctr]["tot_days_present"] : present_days1) + "</td><td>" + (month_days == 0 ? count.ToString() : absent_days1) + "</td><td>" + (month_days == 0 ? ds.Tables[0].Rows[ctr]["total days"].ToString() : month_days.ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["STATUS"].ToString() + "</td></tr>");
                        if (month_days == 0)
                        {
                            present_days = present_days + double.Parse(ds.Tables[0].Rows[ctr]["tot_days_present"].ToString());
                            absent_days = absent_days + count;
                            total_days = total_days + double.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());

                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                int col_span = (int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString()) + 7);
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = " + col_span + ">Total</td><td>" + present_days + "</td><td>" + absent_days + "</td><td>" + total_days + "</td></b></tr>";
                            }
                        }

                        bodystr = "";
                    }
                    
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
    

    protected void btn_paid_Click(object sender, EventArgs e)
    {

    }
    protected void btn_close_Click(object sender, EventArgs e)
    {

    }


    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        if (ddl_client_name.SelectedValue != "ALL")
        {

            d1.con1.Open();
            ddl_state.Items.Clear();
            try
            {
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client_name.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_name.SelectedValue + "')  ORDER BY STATE", d1.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddl_state.DataSource = DS;
                ddl_state.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                ddl_state.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con1.Close();
            }


            ddl_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND state_name ='" + ddl_state.SelectedValue + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unit.DataSource = dt_item;
                    ddl_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unit.Items.Insert(0, "ALL");
                ddl_unit.SelectedIndex = 0;
                ddl_state_SelectedIndexChanged1(null, null);

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
        if (ddl_client_name.SelectedValue != "ALL")
        {
            ddl_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_name.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "')   ORDER BY pay_unit_master.state_name", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unit.DataSource = dt_item;
                    ddl_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unit.Items.Insert(0, "ALL");
                ddl_unit.SelectedIndex = 0;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void btn_getxl_report_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";

        if (ddl_report.SelectedValue == "Branch Head Contact Details")
        {
            export_xl(1);
        }

        if (ddl_report.SelectedValue == "Joining Letter Sending Details")
        {
            export_xl(2);
        }

    }

    private void export_xl(int i)
    {
        try
        {
            string sql = null;
            string where_head = "";
            string where_salary = "";
            string where_joining = "";
            string where_billing = "";
            string client = "";

            where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client_name.SelectedValue + "' and unit_code='" + ddl_unit.SelectedValue + "'  and branch_status != 1 ";

            where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client_name.SelectedValue + "' and pay_employee_master.unit_code = '" + ddl_unit.SelectedValue + "' ";

            if (ddl_state.SelectedValue == "ALL")
            {
                where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client_name.SelectedValue + "' and branch_status != 1  ";

                where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client_name.SelectedValue + "' ";

            }
            else if (ddl_unit.SelectedValue == "ALL")
            {
                where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' and branch_status != 1  ";

                where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client_name.SelectedValue + "' and client_wise_state = '" + ddl_state.SelectedValue + "' ";

            }

            if (i == 1)
            {
                sql = "SELECT client_name, state_name, unit_name, LocationHead_Name, LocationHead_mobileno, LocationHead_Emailid, OperationHead_Name, OperationHead_Mobileno, OperationHead_EmailId, FinanceHead_Name, FinanceHead_Mobileno, FinanceHead_EmailId, adminhead_name, adminhead_mobile, adminhead_email FROM pay_unit_master inner join pay_client_master on pay_unit_master.comp_code = pay_client_master.comp_code and pay_unit_master.client_code = pay_client_master.client_code " + where_head;
            }

            if (i == 2)
            {
                sql = "  SELECT client_name, client_wise_state AS 'state_name', unit_name, emp_name, GRADE_DESC as 'designation', date_format(joining_date, '%d/%m/%y') AS 'joining_date',  (CASE WHEN joining_letter_email = 0 THEN 'Not Send' WHEN joining_letter_email = 1 THEN 'Send' ELSE 'Not Send' END) AS 'status' FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_unit_master.client_code = pay_employee_master.client_code AND pay_unit_master.unit_code = pay_employee_master.unit_code INNER JOIN  pay_grade_master  ON  pay_employee_master . comp_code  =  pay_grade_master . comp_code  and  pay_employee_master . GRADE_CODE  =  pay_grade_master . GRADE_CODE " + where_joining + " and left_date is null  ORDER BY 3";
            }


            MySqlCommand cmd = new MySqlCommand(sql, d.con);

            MySqlDataAdapter dscmd = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                
                string date1 = txt_date.Text;

                Response.Clear();
                Response.Buffer = true;
            
                if (i == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename= HEAD_CONTACT_DETAILS.xls");
                }
                else if (i == 2)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=JOINING_LETTER_DETAILS.xls");
                }
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate12(ListItemType.Header, ds, i,date1);
                Repeater1.ItemTemplate = new MyTemplate12(ListItemType.Item, ds, i,date1);
                Repeater1.FooterTemplate = new MyTemplate12(ListItemType.Footer, null, i,date1);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(stringWrite.ToString());
                Response.Flush();
                Response.End();

               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally{
            d.con.Close();
        }
    }
    public class MyTemplate12 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        //static int ctr1;
        int i;
        //string emp_type;
       string date1;
        //string t;
        //double emp_esic = 0, empr_esic = 0, total = 0;
        //string client_name = "";
        //int i3 = 1;
        private ListItemType listItemType;
        //double amount = 0, gst = 0, grand_total = 0, amount1 = 0, gst1 = 0, grand_total1 = 0;

        private string getmonth(string month)
        {
            month = (int.Parse(month)).ToString();
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
        public MyTemplate12(ListItemType type, DataSet ds, int i, string date1)
        {
            // TODO: Complete member initialization
            this.type = type;
            this.ds = ds;
            this.i = i;
            //this.date1 = date1;
             ctr=0;
          

        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            { //Original Bank A/C Number ,PF_IFSC_CODE,BANK_HOLDER_NAME
                case ListItemType.Header:


                    // var today = DateTime.Now;
                 //  var current_date = date1;

                  
                     if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=16 align=center> HEAD CONTACT DETAILS</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>LOCATION HEAD NAME</th><th>LOCATION HEAD MOBILE NO</th><th>LOCATION HEAD E-MAIL</th><th>OPERTION HEAD NAME</th><th>OPERTION HEAD MOBILE NO</th><th>OPERTION HEAD E-MAIL</th><th>FINANCE HEAD NAME</th><th>FINANCE HEAD MOBILE NO</th><th>FINANCE HEAD E-MAIL</th><th>ADMIN HEAD NAME</th><th>ADMIN HEAD MOBILE NO</th><th>ADMIN HEAD E-MAIL</th></tr>");
                    }
                   
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=8 align=center> JOINING LETTER DETAILS </th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>EMPLOYEE NAME</th><th>DESIGNATION</th><th>JOINING DATE</th><th>STATUS</th></tr>");
                    }
                    break;
                case ListItemType.Item:

                     if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_Emailid"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_Mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_EmailId"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_Mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_EmailId"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_mobile"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_email"].ToString().ToUpper() + "</td></tr>");

                    }
                    else if (i == 2)
                    {
                        string bg = "";
                        if (ds.Tables[0].Rows[ctr]["status"].ToString() == "Send")
                        {
                            bg = "bgcolor=green";
                        }
                        else
                        {
                            bg = "bgcolor=red";
                        }
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"].ToString().ToUpper() + "</td><td " + bg + ">" + ds.Tables[0].Rows[ctr]["status"].ToString().ToUpper() + "</td></tr>");
                    }
                   
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
}