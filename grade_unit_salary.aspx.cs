using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System.Web;
using System.Linq;
using System.Globalization;


public partial class Billing_rates : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    CompanyBAL cbal = new CompanyBAL();
    BillingSalary bs = new BillingSalary();
    public int arrears_flag = 0, ot_payment = 0;
    public int without_ac_no = 0;
    public static int payment_approve_flag = 0;
    public static int provisional_payment_flag = 0;
    public static int vendor_annuxture = 0;//vikas add for vendor payment
    public int left_employee = 0;
    public string appro_attendannce_finanace = "0", pending_attendance = "0", payment_approve = "0", finance_approve = "0", finance_not_approve = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }

        if (!IsPostBack)
        {
            // vendor_bank();
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_active_close='0'  ORDER BY client_code", d.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
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
                hide_controls();
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
                ddl_client.Items.Insert(1, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            ViewState["pending_attendance"] = 0;
            ViewState["appro_attendannce_finanace"] = 0;
            desigpanel.Visible = false;
            ViewState["payment_approve"] = 0;
            ViewState["finance_approve"] = 0;
            ViewState["finance_not_approve"] = 0;

            vendor_payment();
        }
        appro_attendannce_finanace = ViewState["appro_attendannce_finanace"].ToString();
        pending_attendance = ViewState["pending_attendance"].ToString();
        payment_approve = ViewState["payment_approve"].ToString();
        finance_approve = ViewState["finance_approve"].ToString();
        finance_not_approve = ViewState["finance_not_approve"].ToString();

        pending_finance_panel.Visible = false; approval_finance_panel.Visible = false; payment_approve_panel.Visible = false; approval_by_finance_panel.Visible = false; not_approval_by_finance_panel.Visible = false;
        btn_save.Visible = true;
        arrears_flag = 0;
        without_ac_no = 0;
        left_employee = 0;
        payment_approve_flag = 0;
    }

    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        // attendance_status();

        string flag = "and pay_attendance_muster.flag = 2";
        string where = " and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name='" + ddl_billing_state.SelectedValue + "'";
        string from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
        //lessAndroidAttendancesMarksEmployeeList(ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text);
       
        if (ddl_billing_state.SelectedValue == "ALL")
        {
            from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
            where = " and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'";
        }
        else if (ddl_unitcode.SelectedValue != "ALL")
        {
            from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
            where = " and pay_billing_master_history.billing_unit_code  = '" + ddl_unitcode.SelectedValue + "'";
        }
        string temp = d1.getsinglestring("SELECT distinct(month_end) FROM pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_client_code = pay_unit_master.client_code and pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code where pay_unit_master.branch_status=0 and pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month_year.Text.Substring(0, 2) + "' AND YEAR='" + txt_month_year.Text.Substring(3, 4) + "'" + where);
        if (temp == "0" || temp == "")
        {
            string unit = d.chk_payment_approve(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text.Substring(0, 2), txt_month_year.Text.Substring(3), 1, "0");
            if (unit != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This : " + unit + "  Branch Payment Already Approve You Can not Proceed ...');", true);
                return;

            }
            if (int.Parse(ddl_process_data.SelectedValue).Equals(0))
            {
                int counter = 0;
                if (!ddl_start_date_common.SelectedValue.Equals("0"))
                {
                    int last_date = DateTime.DaysInMonth(int.Parse(txt_month_year.Text.Substring(3)), int.Parse(txt_month_year.Text.Substring(0, 2)));

                    if (last_date < int.Parse(ddl_end_date_common.SelectedValue)) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('End Date Not Valid In this Month !!!');", true); return; }

                    string from_to = d1.getsinglestring("select start_date from pay_billing_unit_rate_history where " + from_to_where + " limit  1");
                    if (from_to.Equals("0") || from_to.Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' The start_date : " + ddl_start_date_common.SelectedValue + "  To   end_date  : " + ddl_end_date_common.SelectedValue + "  Bill  Can not Proceed ...');", true);


                        return;
                    }
                    counter = 1;
                }

                bs.Salary(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text, Session["LOGIN_ID"].ToString(), counter, ddl_start_date_common.SelectedValue, ddl_end_date_common.SelectedValue);

            }
            insert_salary_histry(1);
            string start_date_common = get_start_date();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You Cannot Make Changes as Month End Process have Completed !!!');", true);
        }
        gv_fullmonthot.Visible = false;
        paypro_no(0);
        d1.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1;

            if (ddl_billing_state.SelectedValue == "ALL")
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT +common_allowance) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT',CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and  pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0  " + flag + "  order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT',CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' and  pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0 " + flag + " order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            else
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT +common_allowance) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable  * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT', CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND  pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0  " + flag + " order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            adp1.Fill(ds1);
            gv_fullmonthot.DataSource = ds1.Tables[0];
            gv_fullmonthot.DataBind();
            show_controls();
            d1.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
            //paypro_no(1);
        }

        //rahul loop start
        if (ddl_client.SelectedValue == "RLIC HK")
        {
            string unit_namevalidation = "";

            if (ddl_billing_state.SelectedValue == "ALL") {
               unit_namevalidation = d.get_group_concat("select distinct(unit_name) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag = 0");
                if (unit_namevalidation != null && unit_namevalidation !="" )
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_namevalidation + " billing  not approve so you can not process payment');", true);
                    return;
                }
                
            }

            string unit_code = "";
            string unit_name = "";
            String state_name = "";

            if (ddl_billing_state.SelectedValue == "ALL" && ddl_unitcode.SelectedValue == "ALL")
            {
                unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in('1','2')");
                unit_name = ddl_unitcode.SelectedItem.ToString();
                state_name = ddl_billing_state.SelectedItem.ToString();
            }
            else if (ddl_billing_state.SelectedValue != "ALL" && ddl_unitcode.SelectedValue == "ALL")
            {
                unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in ('1','2')");
                unit_name = ddl_unitcode.SelectedItem.ToString();
                state_name = ddl_billing_state.SelectedItem.ToString();
            }
            else if (ddl_unitcode.SelectedValue != "ALL")
            {
                unit_name = ddl_unitcode.SelectedItem.ToString();
                state_name = ddl_billing_state.SelectedItem.ToString();
                unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "'  and unit_code='" + ddl_unitcode.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in ('1','2')");
            
            }

            if (unit_code == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Client : " + ddl_client.SelectedItem + " and state Name:"+state_name+" and unit name: "+unit_name+" billing  not approve this month: "+txt_month_year.Text+" so you can not process payment');", true);
                return;
            }
            //else if (unit_code != "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + " billing  not approve so you can not process payment');", true);
            //    return;
                
            // }
            else {
                   // lessAndroidAttendancesMarksEmployeeList(ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text);
                lessAndroidAttendancesMarksEmployeeList(ddl_client.SelectedValue, unit_code, txt_month_year.Text);

                 }

        }
        //rahul loop end
    }

    public void lessAndroidAttendancesMarksEmployeeList(string client_code,string unit_code, string billing_date) {
        
        string function = where_function();
       
        d1.con1.Open();
        try
        {

            DataSet ds2 = new DataSet();
            MySqlDataAdapter adp2 = new MySqlDataAdapter();
            int days = bs.CountDay(int.Parse(txt_month_year.Text.Substring(0, 2)), int.Parse(txt_month_year.Text.Substring(3)), 2, ddl_client.SelectedValue, Session["COMP_CODE"].ToString(), 0);
            string groupconcatunit_code = "";
            if (ddl_billing_state.SelectedValue=="ALL" && ddl_unitcode.SelectedValue == "ALL")
            {
                 //groupconcatunit_code = d.get_group_concat("select  unit_code from pay_unit_master where client_code='" + ddl_client.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
               // groupconcatunit_code = groupconcatunit_code.Replace(",", "','");
                groupconcatunit_code = unit_code.Replace(",", "','");
                
                //adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', '" + ddl_client.SelectedItem + "' as 'Client Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code in ('" + groupconcatunit_code + "') " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < " + days, d1.con1);
                adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', 'RELIANCE NIPPON LIFE INSURANCE CO. LTD.' as 'Client Name',pay_unit_master.STATE_NAME as 'State Name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code in ('" + groupconcatunit_code + "') " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < '" + days+"'  union select EMP_CODE,(SELECT CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'NAME',CLIENT_NAME,pay_unit_master.STATE_NAME as 'State Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',0 as 'present day'  from pay_employee_master inner join pay_unit_master on pay_employee_master.COMP_CODE=pay_unit_master.COMP_CODE and  pay_employee_master.UNIT_CODE=pay_unit_master.UNIT_CODE inner join pay_client_master on pay_employee_master.COMP_CODE=pay_client_master.COMP_CODE and  pay_employee_master.client_code=pay_client_master.CLIENT_CODE where  pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code in ('" + groupconcatunit_code + "') and employee_type='Reliever' and left_date is null", d1.con1);
          
           }
           else if (ddl_billing_state.SelectedValue != "ALL" &&ddl_unitcode.SelectedValue == "ALL")
            {
                // groupconcatunit_code = d.get_group_concat("select  unit_code from pay_unit_master where client_code='" + ddl_client.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "' and state_name='" + ddl_billing_state.SelectedValue + "'");
                //groupconcatunit_code = groupconcatunit_code.Replace(",", "','");

                groupconcatunit_code = unit_code.Replace(",", "','");

               // adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', '" + ddl_client.SelectedItem + "' as 'Client Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code in ('" + groupconcatunit_code + "') " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < " + days, d1.con1);
                adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', '" + ddl_client.SelectedItem + "' as 'Client Name',pay_unit_master.STATE_NAME as 'State Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code in ('" + groupconcatunit_code + "') " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < '" + days + "' union select EMP_CODE,(SELECT CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'NAME',CLIENT_NAME,pay_unit_master.STATE_NAME as 'State Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',0 as 'present day'  from pay_employee_master inner join pay_unit_master on pay_employee_master.COMP_CODE=pay_unit_master.COMP_CODE and  pay_employee_master.UNIT_CODE=pay_unit_master.UNIT_CODE inner join pay_client_master on pay_employee_master.COMP_CODE=pay_client_master.COMP_CODE and  pay_employee_master.client_code=pay_client_master.CLIENT_CODE where  pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code in ('" + groupconcatunit_code + "') and employee_type='Reliever' and left_date is null", d1.con1);
            
           }
            else
            {
                groupconcatunit_code = unit_code.Replace(",", "','");
                //adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', '" + ddl_client.SelectedItem + "' as 'Client Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code='" + ddl_unitcode.SelectedValue + "' " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < " + days, d1.con1);
                adp2 = new MySqlDataAdapter("select  pay_android_attendance_logs.EMP_CODE as 'Emp Code',pay_android_attendance_logs.EMP_NAME as 'Employee Name', '" + ddl_client.SelectedItem + "' as 'Client Name',pay_unit_master.STATE_NAME as 'State Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',count(EMP_CODE) as 'present day'  from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.COMP_CODE=pay_unit_master.COMP_CODE and pay_android_attendance_logs.UNIT_CODE=pay_unit_master.unit_CODE where pay_android_attendance_logs.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_android_attendance_logs.unit_code='" + ddl_unitcode.SelectedValue + "' " + function + " and lessattendancesflag='0' group by emp_code having count(EMP_CODE) < '" + days + "' union select EMP_CODE,(SELECT CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'NAME',CLIENT_NAME,pay_unit_master.STATE_NAME as 'State Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'Unit Name',0 as 'present day'  from pay_employee_master inner join pay_unit_master on pay_employee_master.COMP_CODE=pay_unit_master.COMP_CODE and  pay_employee_master.UNIT_CODE=pay_unit_master.UNIT_CODE inner join pay_client_master on pay_employee_master.COMP_CODE=pay_client_master.COMP_CODE and  pay_employee_master.client_code=pay_client_master.CLIENT_CODE where  pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.unit_code='" + ddl_unitcode.SelectedValue + "' and employee_type='Reliever' and left_date is null", d1.con1);
           
           }

            adp2.Fill(ds2);
            gv_lessEmployeeAttendances.DataSource = ds2.Tables[0];
            gv_lessEmployeeAttendances.DataBind();
            d1.con1.Close();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string inlist = "", outlist = "";string res12;

            foreach (GridViewRow gvrow in gv_lessEmployeeAttendances.Rows)
            {
               // var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                //string emp_code = (string)gv_lessEmployeeAttendances.DataKeys[gvrow.RowIndex].Value;
                string emp_code = gvrow.Cells[1].Text;
                res12 = d.getsinglestring("select concat(less_attendances_flag,'-',less_attendances_flag_reliver) from pay_pro_master where emp_code ='" + gvrow.Cells[1].Text + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");

                if (res12 !="")
                {
                
                if (res12.Substring(0, 1).ToString() == "0" && res12.Substring(2, 1).ToString() == "0")
                {
                    inlist = inlist + "'" + emp_code + "',";
                }else {
                    outlist = outlist + "'" + emp_code + "',";
                }
                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (res12.Substring(2, 1).ToString() == "1")
                {
                    checkbox.Checked = true;
                }
                else
                {
                    checkbox.Checked = false;
                }
                }
                              
            }

            if (inlist.Length > 0)
            {
                int res1 = d.operation("update pay_pro_master set less_attendances_flag='1' where emp_code in (" + inlist.Substring(0, inlist.Length - 1) + ") and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
                lessEmployeeAttendances.Visible = true;
            }
            else {
                lessEmployeeAttendances.Visible = true;
            }
    
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
           
            
        }

    
    }

    protected string where_function()
    {

        string where = "";
        hidden_month.Value = txt_month_year.Text.Substring(0, 2);
        hidden_year.Value = txt_month_year.Text.Substring(3);
        string start_date_common = get_start_date();
        string end_date_common = get_end_date();

        int month = int.Parse(hidden_month.Value);
        int year = int.Parse(hidden_year.Value);
        if (start_date_common != "" && start_date_common != "1")
        {
            month = --month;
            if (month == 0) { month = 12; year = --year; }
            where = " and (date_time >= str_to_date('" + start_date_common + "/" + month + "/" + year + "','%d/%m/%Y') || date_time is null) and date_time <=  str_to_date('" + (int.Parse(start_date_common) - 1) + "/" + hidden_month.Value + "/" + hidden_year.Value + " 23:59:59','%d/%m/%Y %H:%i:%s')";
        }
        else
        {
            start_date_common = "1";
            where = " and (date_time >= str_to_date('1/" + hidden_month.Value + "/" + hidden_year.Value + "','%d/%m/%Y') || date_time is null) and date_time <=  str_to_date('" + DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value)) + "/" + hidden_month.Value + "/" + hidden_year.Value + " 23:59:59','%d/%m/%Y %H:%i:%s')";
        }

        return where;

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
                System.Web.UI.WebControls.CheckBox txtName = (e.Row.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox);
                txtName.Checked = true;
            }
        }
        e.Row.Cells[1].Visible = false;
        //e.Row.Cells[10].Visible = false;

    }

    protected void lessEmployeeAttendancesFalgUpdate(object e, EventArgs ar) {
        string inlist = "", outlist = "";

        foreach (GridViewRow gvrow in gv_lessEmployeeAttendances.Rows)
        {
            var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            //string emp_code = (string)gv_lessEmployeeAttendances.DataKeys[gvrow.RowIndex].Value;
            string emp_code = gvrow.Cells[1].Text;
            if (checkbox.Checked == true)
            {
               
                try
                {
                    inlist = inlist + "'" + emp_code + "',";

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

            }


        }

        int res1 = d.operation("update pay_pro_master set less_attendances_flag='0',less_attendances_flag_reliver='1' where emp_code in (" + inlist.Substring(0, inlist.Length - 1) + ") and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
        string function = where_function();
        int res2 = d.operation("update pay_android_attendance_logs set lessattendancesflag='1' where emp_code in (" + inlist.Substring(0, inlist.Length - 1) + ") " + function + "");


        string unit_code = "";
        string unit_name = "";
        String state_name = "";

        if (ddl_billing_state.SelectedValue == "ALL" && ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in('1','2')");
            unit_name = ddl_unitcode.SelectedItem.ToString();
            state_name = ddl_billing_state.SelectedItem.ToString();
        }
        else if (ddl_billing_state.SelectedValue != "ALL" && ddl_unitcode.SelectedValue == "ALL")
        {
            unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in ('1','2')");
            unit_name = ddl_unitcode.SelectedItem.ToString();
            state_name = ddl_billing_state.SelectedItem.ToString();
        }
        else if (ddl_unitcode.SelectedValue != "ALL")
        {
            unit_name = ddl_unitcode.SelectedItem.ToString();
            state_name = ddl_billing_state.SelectedItem.ToString();
            unit_code = d.get_group_concat("select distinct(unit_code) from pay_billing_unit_rate_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "'  and unit_code='" + ddl_unitcode.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag in ('1','2')");

        }


        lessAndroidAttendancesMarksEmployeeList(ddl_client.SelectedValue, unit_code, txt_month_year.Text);
        
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            // comment 30/09  MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  ORDER BY UNIT_CODE", d.con);//and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" +ddl_billing_state.SelectedValue + "') 
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  ORDER BY UNIT_CODE", d.con);//and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" +ddl_billing_state.SelectedValue + "') 
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
                show_controls();
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
            cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND (branch_close_date is null ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) order by 1", d.con);//and state_Name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "')
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
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

            // Bank
            ddl_bank.Items.Clear();
            System.Data.DataTable dt_bank = new System.Data.DataTable();
            MySqlDataAdapter cmd_bank = new MySqlDataAdapter("select distinct(bank) from pay_client_master where comp_code='" + Session["comp_code"] + "' and CLIENT_CODE ='" + ddl_client.SelectedValue + "' ", d.con);
            d.con.Open();
            try
            {
                cmd_bank.Fill(dt_bank);
                if (dt_bank.Rows.Count > 0)
                {
                    ddl_bank.DataSource = dt_bank;
                    ddl_bank.DataTextField = dt_bank.Columns[0].ToString();
                    ddl_bank.DataValueField = dt_bank.Columns[0].ToString();
                    ddl_bank.DataBind();
                }
                dt_bank.Dispose();
                d.con.Close();

            }
            catch (Exception ex) { }
            finally { }

        }
        else
        {
            ddl_unitcode.Items.Clear();
            hide_controls();
        }
    }

    private void show_controls()
    {
        unit_panel.Visible = true;
        unit_panel1.Visible = true;
        btn_save.Visible = true;
    }

    private void hide_controls()
    {
        unit_panel.Visible = false;
        unit_panel1.Visible = false;
        btn_save.Visible = false;
    }


    int CountDay(int month, int year, int counter)
    {
        string start_date_common = get_start_date();
        string end_date_common = get_end_date();

        int NoOfSunday = 0;

        var firstDay = (dynamic)null;

        if (start_date_common != "1")
        {
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


        if (counter == 1)
        {//calendar days
            return NumOfDay;
        }
        else
        { //working days
            return NumOfDay - NoOfSunday;
        }
    }

    private void export_xl(int i)
    {
        string start_end_date = "AND (pay_pro_master.start_date = 0 AND  pay_pro_master.end_date = 0) and (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) ", payment_approve = null;
        if (ddl_start_date_common.SelectedValue != "0" && ddl_end_date_common.SelectedValue != "0")
        {
            start_end_date = "AND (pay_pro_master.start_date = " + ddl_start_date_common.SelectedValue + " AND pay_pro_master.end_date = " + ddl_end_date_common.SelectedValue + ") AND (pay_billing_unit_rate_history.start_date = " + ddl_start_date_common.SelectedValue + " AND pay_billing_unit_rate_history.end_date = " + ddl_end_date_common.SelectedValue + ") ";
        }
        string where = "", grade = "";
        string ot_type = "", hdfc = "", ot_where = "", ot_inner = "";

        ot_type = d.getsinglestring("SELECT billing_ot FROM pay_client_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ");
        if (ot_type == "With OT" && ddl_client.SelectedValue == "HDFC")
        {
            hdfc = " and pay_pro_master.hdfc_type='manpower_bill'";

        }
        else if (ot_type == "Without OT" && ddl_client.SelectedValue == "HDFC" && (ot_payment == 1 || ot_payment == 2))
        {
            hdfc = " and pay_pro_master.hdfc_type='ot_bill'";
        }

        if (ot_payment != 1 && ot_payment != 2 && (ddl_client.SelectedValue == "HDFC"))
        { hdfc = " and pay_pro_master.hdfc_type='manpower_bill'"; }
        if (ddl_invoice_type.SelectedValue == "2")
        {
            grade = " and designation = '" + ddl_designation.SelectedValue + "'";
            if (i == 2)
            {
                grade = " and pay_salary_unit_rate.designation = '" + ddl_designation.SelectedValue + "'";
            }
        }
        string pay_attendance_muster = " pay_attendance_muster ";
        string reliver = " and pay_pro_master.employee_type != 'Reliever'";
        string salary_type = "And (salary_type is null || salary_type ='')", leftemp = " and pay_pro_master.STATUS != 'LEFT' ";
        string bank_ac_no = " (pay_pro_master.BANK_EMP_AC_CODE IS NOT NULL && trim(pay_pro_master.BANK_EMP_AC_CODE) != '') AND (pay_pro_master.Bank_holder_name IS NOT NULL && trim(pay_pro_master.Bank_holder_name) != '') AND (pay_pro_master.PF_IFSC_CODE IS NOT NULL && trim(pay_pro_master.PF_IFSC_CODE) != '') ";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string sql = null;
        d.con.Open();
        if (i == 1 || i == 3)
        {
            if (payment_approve_flag == 1)
            {
                payment_approve = " and payment_approve > 0";
            }
            if (arrears_flag == 1) { salary_type = "and salary_type = 'Arrears_salary'"; }
            if (ddl_emp_xl_type.SelectedValue == "1") { bank_ac_no = "(pay_pro_master.BANK_EMP_AC_CODE ='' or pay_pro_master.BANK_EMP_AC_CODE IS NULL || pay_pro_master.Bank_holder_name ='' or pay_pro_master.Bank_holder_name IS NULL || pay_pro_master.PF_IFSC_CODE ='' or pay_pro_master.PF_IFSC_CODE IS NULL    )"; }
            if (ddl_emp_xl_type.SelectedValue == "2") { bank_ac_no = ""; leftemp = "pay_pro_master.STATUS = 'LEFT' "; }
            if (ddl_emp_xl_type.SelectedValue == "3")
            {
                bank_ac_no = "";
                leftemp = "";
                reliver = "  pay_pro_master.employee_type = 'Reliever'";
            }

            string less_attendances_flag = "";
            if (ddl_client.SelectedValue == "RLIC HK") {
                less_attendances_flag = "and less_attendances_flag='0'";
                }

            if (ddl_client.SelectedValue != "ALL")
            {
                where = "pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_master.unit_code = '" + ddl_unitcode.SelectedValue + "'  " + grade + hdfc + " " + start_end_date + "  " + salary_type+ " " + less_attendances_flag;
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {
                where = "pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade + hdfc + " " + start_end_date + "  " + salary_type+ " " + less_attendances_flag;
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {

                where = "pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_master.state_name = '" + ddl_billing_state.SelectedValue + "' " + grade + hdfc + " " + start_end_date + "  " + salary_type+ " " + less_attendances_flag;
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = "pay_pro_master.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade + hdfc + " " + start_end_date + "  " + salary_type+ " " + less_attendances_flag;
            }

            if (i == 1)
            {
                where = where + payment_approve + " and payment_status in (0,2)  group by pay_pro_master.emp_code order by pay_pro_master.client,pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name";
            }
            else
            {
                where = where + payment_approve + " and payment_status=1 group by pay_pro_master.emp_code  order by pay_pro_master.client,pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name";
            }

            //sql = "SELECT pay_emp_paypro.pay_pro_no, pay_pro_master.comp_code, pay_pro_master.unit_code,pay_pro_master.deduction as 'uni_deduct', pay_pro_master.client_code, pay_pro_master.state_name, pay_pro_master.unit_city, CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, CASE designation WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', actual_basic_vda, pay_pro_master.emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, pay_pro_master.other_allow, pay_pro_master.gratuity_gross, pay_pro_master.gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, pay_pro_master.gratuity_after_gross, pay_pro_master.fine, EMP_ADVANCE_PAYMENT,ifnull( pay_employee_salary_details.fine,0) AS 'DEDUCTION', Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, pay_pro_master.STATUS, NI, pay_pro_master.date, pay_pro_master.client, IF(pay_pro_master.ot_hours > 0, pay_pro_master.ot_rate, 0) AS 'ot_rate', pay_pro_master.ot_hours, pay_pro_master.ot_amount, common_allow, esic_allowances_salary, pay_pro_master.EMP_CODE, advance_payment_mode, Installment, advance, salary_status, pay_pro_master.unit_name, payment, pay_pro_master.month, pay_pro_master.year, paypro_batch_id, invoice_no, '' AS 'tran_status',pay_pro_master.reliver_advances,pay_pro_master.emp_advance FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_employee_salary_details ON  pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year left outer JOIN pay_emp_paypro ON pay_pro_master.emp_code = pay_emp_paypro.emp_code AND pay_pro_master.month = pay_emp_paypro.month AND pay_pro_master.year = pay_emp_paypro.year WHERE  " + bank_ac_no + "  " + leftemp + " And " + where;
            sql = "SELECT pay_pro_master.advance_deduction, pay_emp_paypro.pay_pro_no, pay_pro_master.comp_code, pay_pro_master.unit_code, pay_pro_master.deduction AS 'uni_deduct', pay_pro_master.client_code,pay_pro_master.client,pay_pro_master.state_name, pay_pro_master.unit_city, CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, pay_employee_master.Id_as_per_dob, CASE designation WHEN 'OB' THEN CASE pay_pro_master.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', actual_basic_vda, pay_pro_master.emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, pay_pro_master.other_allow, pay_pro_master.gratuity_gross, pay_pro_master.gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, pay_pro_master.gratuity_after_gross, pay_pro_master.fine, '0' as EMP_ADVANCE_PAYMENT, IFNULL(pay_employee_salary_details.fine, 0) AS 'DEDUCTION', Total_Days_Present, pay_pro_master.Bank_holder_name, pay_pro_master.BANK_EMP_AC_CODE, pay_pro_master.PF_IFSC_CODE, pay_pro_master.Account_no, pay_pro_master.STATUS, NI, pay_pro_master.date, pay_pro_master.client, IF(pay_pro_master.ot_hours > 0, pay_pro_master.ot_rate, 0) AS 'ot_rate', pay_pro_master.ot_hours, pay_pro_master.ot_amount, common_allow, esic_allowances_salary, pay_pro_master.EMP_CODE, pay_pro_master.advance_payment_mode, pay_pro_master.Installment, advance, pay_pro_master.salary_status, pay_pro_master.unit_name, payment, pay_pro_master.month, pay_pro_master.year, paypro_batch_id, invoice_no, '' AS 'tran_status',pay_pro_master.reliver_advances,pay_pro_master.emp_advance, (pay_billing_unit_rate_history.conveyance_amount/ pay_billing_unit_rate_history.month_days * tot_days_present ) AS 'conveyance_rate' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code and pay_pro_master.comp_code = pay_employee_master.comp_code  LEFT OUTER JOIN pay_employee_salary_details ON pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year LEFT OUTER JOIN pay_emp_paypro ON pay_pro_master.emp_code = pay_emp_paypro.emp_code AND pay_pro_master.month = pay_emp_paypro.month AND pay_pro_master.year = pay_emp_paypro.year and bank ='" + ddl_bank.SelectedValue + "' and type= 0  WHERE  " + bank_ac_no + "  " + leftemp + " " + reliver + " And " + where;

            if (ddl_client.SelectedValue == "HDFC" && (ot_payment == 1 || ot_payment == 2))
            {

                sql = "SELECT   pay_emp_paypro.pay_pro_no, pay_pro_master.comp_code, pay_pro_master.unit_code,  pay_pro_master.client_code, pay_pro_master.client, pay_pro_master.state_name, pay_pro_master.unit_city, CASE pay_pro_master.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master.emp_name, '-', 'Reliever') ELSE pay_pro_master.emp_name END AS 'emp_name', pay_pro_master.employee_type, pay_employee_master.Id_as_per_dob, CASE designation WHEN 'OB' THEN CASE pay_pro_master.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', sal_ot, Total_Days_Present, pay_pro_master.Bank_holder_name, pay_pro_master.BANK_EMP_AC_CODE, pay_pro_master.PF_IFSC_CODE, pay_pro_master.Account_no, pay_pro_master.STATUS, NI, pay_pro_master.date, pay_pro_master.client, IF(pay_pro_master.ot_hours > 0, pay_pro_master.ot_rate, 0) AS 'ot_rate', pay_pro_master.ot_hours, pay_pro_master.ot_amount, pay_pro_master.EMP_CODE, pay_pro_master.salary_status, pay_pro_master.unit_name, payment, pay_pro_master.month, pay_pro_master.year, paypro_batch_id, invoice_no, '' AS 'tran_status' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code and pay_pro_master.comp_code = pay_employee_master.comp_code  LEFT OUTER JOIN pay_employee_salary_details ON pay_pro_master.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master.month = pay_employee_salary_details.month AND pay_pro_master.year = pay_employee_salary_details.year LEFT OUTER JOIN pay_emp_paypro ON pay_pro_master.emp_code = pay_emp_paypro.emp_code AND pay_pro_master.month = pay_emp_paypro.month AND pay_pro_master.year = pay_emp_paypro.year and bank ='" + ddl_bank.SelectedValue + "' and type= '6'  WHERE  " + bank_ac_no + "  " + leftemp + " " + reliver + " And " + where;

            }


        }
        //provisional payment
        else if (i == 2)
        {
            if (ddl_client.SelectedValue != "ALL")
            {
                where = "pay_attendance_muster.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_unit_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.tot_days_present > 0 AND  IF(pay_unit_master.client_code = 'BAGIC TM' ,pay_employee_master.Employee_type = 'Temporary' OR pay_employee_master.Employee_type = 'Permanent',pay_employee_master.Employee_type = 'Permanent')  " + grade + " order by pay_unit_master.state_name,pay_unit_master.unit_name,pay_employee_master.emp_name) as table_salary";
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {
                where = "pay_attendance_muster.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.tot_days_present > 0 AND  IF(pay_unit_master.client_code = 'BAGIC TM' ,pay_employee_master.Employee_type = 'Temporary' OR pay_employee_master.Employee_type = 'Permanent',pay_employee_master.Employee_type = 'Permanent')  " + grade + " order by  pay_unit_master.state_name,pay_unit_master.unit_name,pay_employee_master.emp_name) as table_salary";
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {
                where = "pay_attendance_muster.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_unit_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.tot_days_present > 0 AND IF(pay_unit_master.client_code = 'BAGIC TM', pay_employee_master.Employee_type = 'Temporary' OR pay_employee_master.Employee_type = 'Permanent', pay_employee_master.Employee_type = 'Permanent') " + grade + " order by pay_unit_master.state_name,pay_unit_master.unit_name,pay_employee_master.emp_name) as table_salary";
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = "pay_attendance_muster.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.year = '" + txt_month_year.Text.Substring(3) + "'  and pay_unit_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_attendance_muster.tot_days_present > 0 AND  IF(pay_unit_master.client_code = 'BAGIC TM' ,pay_employee_master.Employee_type = 'Temporary' OR pay_employee_master.Employee_type = 'Permanent',pay_employee_master.Employee_type = 'Permanent')  " + grade + " order by  pay_unit_master.state_name,pay_unit_master.unit_name,pay_employee_master.emp_name) as table_salary";
            }
            //sql = "SELECT state_name, unit_city, emp_name, grade, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary,IF(esic_oa_salary=1,allowances_salary,0) as 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot+(ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type= 'Permanent',lwf_salary,0) as 'lwf_salary', sal_uniform_rate, IF(pt_applicable=1,CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END,0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode= 1,(EMP_ADVANCE_PAYMENT/Installment),EMP_ADVANCE_PAYMENT) as 'EMP_ADVANCE_PAYMENT',month_days as 'Total_Days_Present', Bank_holder_name,original_bank_account_no as 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client,(ot_applicable) as 'ot_rate',ot_hours,(ot_applicable * ot_hours) as 'ot_amount',IF(esic_common_allow = 0, common_allow, 0) AS 'common_allow',	IF(esic_oa_salary=0,allowances_salary,0) as 'esic_allowances_salary',EMP_CODE,advance_payment_mode,Installment,EMP_ADVANCE_PAYMENT as 'advance',IF(salary_status=1,'Hold','Clear') as 'salary_status',unit_name,month_days FROM (SELECT pay_unit_master.state_name, unit_city, pay_employee_master.emp_name, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' and  type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', pay_salary_unit_rate.basic_vda AS 'emp_basic_vda',pay_salary_unit_rate.hra_amount_salary,CASE WHEN bonus_taxable_salary = '1' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END AS 'sal_bonus_gross',CASE WHEN bonus_taxable_salary = '0' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END AS 'sal_bonus_after_gross',CASE WHEN leave_taxable_salary = '1' THEN pay_salary_unit_rate.leave_days ELSE 0 END AS 'leave_sal_gross',CASE WHEN leave_taxable_salary = '0' THEN pay_salary_unit_rate.leave_days ELSE 0 END AS 'leave_sal_after_gross',pay_salary_unit_rate.washing_salary,pay_salary_unit_rate.travelling_salary,pay_salary_unit_rate.education_salary,pay_billing_master_history.allowances_salary,CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.cca_salary ELSE pay_employee_master.cca END AS 'cca_salary',CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.other_allow ELSE pay_employee_master.special_allow END AS 'other_allow',CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross',CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross',pay_billing_master_history.sal_esic AS 'sal_esic_percent',pay_billing_master_history.sal_pf AS 'sal_pf_percent',pay_salary_unit_rate.ot_amount AS 'sal_ot',pay_salary_unit_rate.lwf_salary,pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND pay_salary_unit_rate.basic_vda+ pay_salary_unit_rate.hra_amount_salary+ CASE WHEN bonus_taxable_salary = '1' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END+ CASE WHEN leave_taxable_salary = '1' THEN pay_salary_unit_rate.leave_days ELSE 0 END+ pay_salary_unit_rate.washing_salary+ pay_salary_unit_rate.travelling_salary+ pay_salary_unit_rate.education_salary+ pay_salary_unit_rate.allowances_salary+ CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.cca_salary ELSE pay_employee_master.cca END+ CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.other_allow ELSE pay_employee_master.special_allow END+ CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END+ pay_salary_unit_rate.ot_amount BETWEEN FROM_AMOUNT AND TO_AMOUNT AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT,pay_salary_unit_rate.ot_applicable,pay_salary_unit_rate.esic_ot_applicable,pay_attendance_muster.ot_hours,	pay_billing_master_history.esic_oa_salary,	pay_billing_master_history.esic_common_allow,CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.common_allowance ELSE pay_employee_master.special_allow END AS 'common_allow',pay_billing_master_history.pt_applicable,pay_employee_master.employee_type,pay_employee_master.advance_payment_mode,pay_employee_master.Installment,pay_employee_master.EMP_CODE,pay_employee_master.salary_status,unit_name,pay_salary_unit_rate.month_days FROM pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code and pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours AND pay_billing_master_history.type = 'salary' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code WHERE  " + where;
            // rahul add oofice lady
            sql = "SELECT state_name, unit_city, emp_name, grade, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary,IF(esic_oa_salary=1,allowances_salary,0) as 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot+(ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type= 'Permanent',lwf_salary,0) as 'lwf_salary', sal_uniform_rate, IF(pt_applicable=1,CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary=1,allowances_salary,0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END,0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode= 1,(EMP_ADVANCE_PAYMENT/Installment),EMP_ADVANCE_PAYMENT) as 'EMP_ADVANCE_PAYMENT',month_days as 'Total_Days_Present', Bank_holder_name,original_bank_account_no as 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client,(ot_applicable) as 'ot_rate',ot_hours,(ot_applicable * ot_hours) as 'ot_amount',IF(esic_common_allow = 0, common_allow, 0) AS 'common_allow',	IF(esic_oa_salary=0,allowances_salary,0) as 'esic_allowances_salary',EMP_CODE,advance_payment_mode,Installment,EMP_ADVANCE_PAYMENT as 'advance',IF(salary_status=1,'Hold','Clear') as 'salary_status',unit_name,month_days FROM (SELECT pay_unit_master.state_name, unit_city, pay_employee_master.emp_name, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', CASE grade_code WHEN 'OB' THEN CASE gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) END AS 'grade', pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' and  type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', pay_salary_unit_rate.basic_vda AS 'emp_basic_vda',pay_salary_unit_rate.hra_amount_salary,CASE WHEN bonus_taxable_salary = '1' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END AS 'sal_bonus_gross',CASE WHEN bonus_taxable_salary = '0' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END AS 'sal_bonus_after_gross',CASE WHEN leave_taxable_salary = '1' THEN pay_salary_unit_rate.leave_days ELSE 0 END AS 'leave_sal_gross',CASE WHEN leave_taxable_salary = '0' THEN pay_salary_unit_rate.leave_days ELSE 0 END AS 'leave_sal_after_gross',pay_salary_unit_rate.washing_salary,pay_salary_unit_rate.travelling_salary,pay_salary_unit_rate.education_salary,pay_billing_master_history.allowances_salary,CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.cca_salary ELSE pay_employee_master.cca END AS 'cca_salary',CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.other_allow ELSE pay_employee_master.special_allow END AS 'other_allow',CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross',CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross',pay_billing_master_history.sal_esic AS 'sal_esic_percent',pay_billing_master_history.sal_pf AS 'sal_pf_percent',pay_salary_unit_rate.ot_amount AS 'sal_ot',pay_salary_unit_rate.lwf_salary,pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND pay_salary_unit_rate.basic_vda+ pay_salary_unit_rate.hra_amount_salary+ CASE WHEN bonus_taxable_salary = '1' THEN pay_salary_unit_rate.sal_bonus ELSE 0 END+ CASE WHEN leave_taxable_salary = '1' THEN pay_salary_unit_rate.leave_days ELSE 0 END+ pay_salary_unit_rate.washing_salary+ pay_salary_unit_rate.travelling_salary+ pay_salary_unit_rate.education_salary+ pay_salary_unit_rate.allowances_salary+ CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.cca_salary ELSE pay_employee_master.cca END+ CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.other_allow ELSE pay_employee_master.special_allow END+ CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN pay_salary_unit_rate.gratuity_salary ELSE 0 END ELSE pay_employee_master.gratuity END+ pay_salary_unit_rate.ot_amount BETWEEN FROM_AMOUNT AND TO_AMOUNT AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT,pay_salary_unit_rate.ot_applicable,pay_salary_unit_rate.esic_ot_applicable,pay_attendance_muster.ot_hours,	pay_billing_master_history.esic_oa_salary,	pay_billing_master_history.esic_common_allow,CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.common_allowance ELSE pay_employee_master.special_allow END AS 'common_allow',pay_billing_master_history.pt_applicable,pay_employee_master.employee_type,pay_employee_master.advance_payment_mode,pay_employee_master.Installment,pay_employee_master.EMP_CODE,pay_employee_master.salary_status,unit_name,pay_salary_unit_rate.month_days FROM pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code and pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours AND pay_billing_master_history.type = 'salary' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code WHERE  " + where;

        }
        else if (i == 4 || i == 5)
        {
            reliver = " and pay_pro_master_arrears.employee_type != 'Reliever'";
            leftemp = " and pay_pro_master_arrears.STATUS != 'LEFT' ";
            bank_ac_no = " (pay_pro_master_arrears.BANK_EMP_AC_CODE IS NOT NULL && trim(pay_pro_master_arrears.BANK_EMP_AC_CODE) != '') AND (pay_pro_master_arrears.Bank_holder_name IS NOT NULL && trim(pay_pro_master_arrears.Bank_holder_name) != '') AND (pay_pro_master_arrears.PF_IFSC_CODE IS NOT NULL && trim(pay_pro_master_arrears.PF_IFSC_CODE) != '') ";
            if (i == 4)
            {
                i = 1;
            }
            else
            {
                i = 3;
            }
            if (ddl_emp_xl_type_arrear.SelectedValue == "1") { bank_ac_no = "(pay_pro_master_arrears.BANK_EMP_AC_CODE ='' or pay_pro_master_arrears.BANK_EMP_AC_CODE IS NULL || pay_pro_master_arrears.Bank_holder_name ='' or pay_pro_master_arrears.Bank_holder_name IS NULL || pay_pro_master_arrears.PF_IFSC_CODE ='' or pay_pro_master_arrears.PF_IFSC_CODE IS NULL    )"; }
            if (ddl_emp_xl_type_arrear.SelectedValue == "2") { bank_ac_no = ""; leftemp = "pay_pro_master_arrears.STATUS = 'LEFT' "; }
            if (ddl_emp_xl_type_arrear.SelectedValue == "3")
            {
                bank_ac_no = "";
                leftemp = "";
                reliver = "  pay_pro_master_arrears.employee_type = 'Reliever'";
            }

            string month_year = "pay_pro_master_arrears.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_master_arrears.Year = '" + txt_month_year.Text.Substring(3) + "'";
            if (ddl_arrears_type.SelectedValue.Equals("policy"))
            {
                month_year = "pay_pro_master_arrears.month='" + txt_arrear_month_year.Text.Substring(3, 2) + "' and pay_pro_master_arrears.Year = '" + txt_arrear_month_year.Text.Substring(6) + "'";
            }
            if (ddl_client.SelectedValue != "ALL")
            {
                where = month_year + " and pay_pro_master_arrears.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master_arrears.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_master_arrears.unit_code = '" + ddl_unitcode.SelectedValue + "'  " + grade;
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {
                where = month_year + " and pay_pro_master_arrears.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master_arrears.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade;
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {

                where = month_year + " and pay_pro_master_arrears.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master_arrears.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_master_arrears.state_name = '" + ddl_billing_state.SelectedValue + "' " + grade;
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = month_year + " and pay_pro_master_arrears.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade;
            }

            if (i == 1)
            {
                where = where + payment_approve + " and payment_status in (0,2)  group by pay_pro_master_arrears.emp_code order by pay_pro_master_arrears.client,pay_pro_master_arrears.state_name,pay_pro_master_arrears.unit_name,pay_pro_master_arrears.emp_name";
            }
            else
            {
                where = where + payment_approve + " and payment_status=1 group by pay_pro_master_arrears.emp_code  order by pay_pro_master_arrears.client,pay_pro_master_arrears.state_name,pay_pro_master_arrears.unit_name,pay_pro_master_arrears.emp_name";
            }
            sql = "SELECT pay_pro_master_arrears.advance_deduction, pay_emp_paypro.pay_pro_no, pay_pro_master_arrears.comp_code, pay_pro_master_arrears.unit_code, pay_pro_master_arrears.deduction AS 'uni_deduct', pay_pro_master_arrears.client_code,pay_pro_master_arrears.client,pay_pro_master_arrears.state_name, pay_pro_master_arrears.unit_city, CASE pay_pro_master_arrears.employee_type WHEN 'Reliever' THEN CONCAT(pay_pro_master_arrears.emp_name, '-', 'Reliever') ELSE pay_pro_master_arrears.emp_name END AS 'emp_name', pay_pro_master_arrears.employee_type, pay_employee_master.Id_as_per_dob, CASE designation WHEN 'OB' THEN CASE pay_pro_master_arrears.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE '' END ELSE grade END AS 'grade', actual_basic_vda, pay_pro_master_arrears.emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, pay_pro_master_arrears.other_allow, pay_pro_master_arrears.gratuity_gross, pay_pro_master_arrears.gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, pay_pro_master_arrears.gratuity_after_gross, pay_pro_master_arrears.fine, '0' as EMP_ADVANCE_PAYMENT, IFNULL(pay_employee_salary_details.fine, 0) AS 'DEDUCTION', Total_Days_Present, pay_pro_master_arrears.Bank_holder_name, pay_pro_master_arrears.BANK_EMP_AC_CODE, pay_pro_master_arrears.PF_IFSC_CODE, pay_pro_master_arrears.Account_no, pay_pro_master_arrears.STATUS, NI, pay_pro_master_arrears.date, pay_pro_master_arrears.client, IF(pay_pro_master_arrears.ot_hours > 0, pay_pro_master_arrears.ot_rate, 0) AS 'ot_rate', pay_pro_master_arrears.ot_hours, pay_pro_master_arrears.ot_amount, common_allow, esic_allowances_salary, pay_pro_master_arrears.EMP_CODE, pay_pro_master_arrears.advance_payment_mode, pay_pro_master_arrears.Installment, advance, pay_pro_master_arrears.salary_status, pay_pro_master_arrears.unit_name, payment, pay_pro_master_arrears.month, pay_pro_master_arrears.year, paypro_batch_id, invoice_no, '' AS 'tran_status',pay_pro_master_arrears.reliver_advances,pay_pro_master_arrears.emp_advance, (pay_billing_unit_rate_history_arrears.conveyance_amount/ pay_billing_unit_rate_history_arrears.month_days * tot_days_present ) AS 'conveyance_rate' FROM pay_pro_master_arrears INNER JOIN pay_billing_unit_rate_history_arrears ON pay_pro_master_arrears.emp_code = pay_billing_unit_rate_history_arrears.emp_code AND pay_pro_master_arrears.month = pay_billing_unit_rate_history_arrears.month AND pay_pro_master_arrears.year = pay_billing_unit_rate_history_arrears.year INNER JOIN pay_employee_master ON pay_pro_master_arrears.emp_code = pay_employee_master.emp_code and pay_pro_master_arrears.comp_code = pay_employee_master.comp_code  LEFT OUTER JOIN pay_employee_salary_details ON pay_pro_master_arrears.emp_code = pay_employee_salary_details.emp_code AND pay_pro_master_arrears.month = pay_employee_salary_details.month AND pay_pro_master_arrears.year = pay_employee_salary_details.year LEFT OUTER JOIN pay_emp_paypro ON pay_pro_master_arrears.emp_code = pay_emp_paypro.emp_code AND pay_pro_master_arrears.month = pay_emp_paypro.month AND pay_pro_master_arrears.year = pay_emp_paypro.year and type = 4  WHERE  " + bank_ac_no + "  " + leftemp + " " + reliver + " And " + where;
        }
        MySqlCommand cmd = new MySqlCommand(sql, d.con);
        cmd.CommandTimeout = 200;
        MySqlDataAdapter dscmd = new MySqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (i == 1)
            {
                if (payment_approve_flag.Equals(1))
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ANNUXURE_UPLOAD.xls");
                }
                else if (ot_payment.Equals(1))
                {
                    Response.AddHeader("content-disposition", "attachment;filename=OT_EMPLOYEE_SALARY.xls");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=EMPLOYEE_SALARY.xls");
                }
            }
            else if (i == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=PROVISIONAL_SALARY.xls");
            }
            else if (i == 3)
            {
                if (ot_payment.Equals(1))
                {
                    Response.AddHeader("content-disposition", "attachment;filename=OT_EMPLOYEE_SALARY.xls");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=PAID_SALARY.xls");
                }
            }
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, i, ddl_bank.SelectedValue, ot_payment);
            Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, i, ddl_bank.SelectedValue, ot_payment);
            Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, i, ddl_bank.SelectedValue, ot_payment);
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        attendance_status();
        payment_approve_flag = 0;
        provisional_payment_flag = 0;
        export_xl(1);
    }
    public class MyTemplate : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i;
        int ot_format;
        string bank;
        static int ctr;
        public MyTemplate(ListItemType type, DataSet ds, int i, string bank, int ot_format)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.bank = bank;
            this.ot_format = ot_format;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    if (Billing_rates.payment_approve_flag == 1)
                    {
                        lc = new LiteralControl("<table border=1>");
                        break;
                    }
                    else
                    {
                        if (Billing_rates.provisional_payment_flag == 1)
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=45 align=center>SALARY RATE BREAKUP FOR " + ds.Tables[0].Rows[ctr]["client"] + "</th></tr><table border=1><tr><th>Sr. No.</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>DEG</th><th>BASIC</th><th>HRA</th><th>BONUS</th><th>LEAVE</th><th>WASHING</th><th>TRAVELLING</th><th>EDUCATION</th><th>OTHER ALLOWANCES</th><th>CCA</th><th>ALLOWANCES</th><th>GRATUITY</th><th>SPECIAL ALLOWANCES</th><th>TOTAL</th><th>OT RATE</th><th>OT HOURS</th><th>OT AMOUNT</th><th>GROSS</th><th>PF</th><th>ESIC</th><th>LWF</th><th>UNIFORM RATE</th><th>PT</th><th>BONUS</th><th>LEAVE</th><th>GRATUITY</th><th>ALLOWANCES</th><th>OTHER ALLOWANCES</th><th>UNIFORM ADVANCE DEDUCTION</th><th>PRESENT DAYS</th><th>SALARY STATUS</th><th bgcolor=silver>N / I</th><th bgcolor=silver>AMOUNT</th><th bgcolor=silver>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th> <th bgcolor=silver>ADVANCE DEDUCTION</th><th></th>" + (i == 3 ? "<th>BATCH ID</th>" : "") + "" + (i == 1 ? "<th>SALARY STATUS</th>" : "") + "</tr>");
                        }
                        else
                        {
                            string deduc_uniform = "";
                            if ((ot_format != 1 && ot_format != 2))
                            {
                                if (ds.Tables[0].Rows[ctr]["advance_deduction"].ToString() != "0")
                                {
                                    deduc_uniform = " <td>UNIFORM ADVANCE DEDUCTION </td>";
                                }
                                else
                                {
                                    deduc_uniform = " <td> DEDUCTION UNIFORM/ID/SHOES/ETC </td>";
                                }
                            }
                            if (bank == "INDUSIND BANK")
                            {
                                lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "73" : "70") + " align=center>SALARY RATE BREAKUP FOR " + ds.Tables[0].Rows[ctr]["client"] + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DEG</th><th>ACTUAL BASIC + VDA</th><th>BASIC</th><th>HRA</th><th>BONUS</th><th>LEAVE</th><th>WASHING</th><th>TRAVELLING</th><th>EDUCATION</th><th>OTHER ALLOWANCES</th><th>CCA</th><th>ALLOWANCES</th><th>GRATUITY</th><th>SPECIAL ALLOWANCES</th><th>TOTAL</th><th>OT RATE</th><th>OT HOURS</th><th>OT AMOUNT</th><th>GROSS</th><th>PF</th><th>ESIC</th><th>LWF</th><th>UNIFORM RATE</th><th>PT</th><th>BONUS</th><th>LEAVE</th><th>GRATUITY</th><th>ALLOWANCES</th><th>OTHER ALLOWANCES</th><th>FINE</th>" + deduc_uniform + "<th>Advance</th><th>Reliver Deduction</th><th>PRESENT DAYS</th><th>SALARY STATUS</th>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "<th>TAKE HOME</th><th>TRAVEL ALLOWANCE</th>" : "") + "<th bgcolor=silver>Transaction Type</th><th bgcolor=silver>Beneficiary Code</th> <th bgcolor=silver>Beneficiary A/c No.</th><th bgcolor=silver>Transaction Amount</th><th bgcolor=silver>Beneficiary Name</th><th bgcolor=silver>Drawee Location</th><th bgcolor=silver>Print Location</th><th bgcolor=silver>Beneficiary add line1</th><th bgcolor=silver>Beneficiary add line2</th><th bgcolor=silver>Beneficiary add line3</th><th bgcolor=silver>Beneficiary add line4</th><th bgcolor=silver>Zipcode</th><th bgcolor=silver>Instrument Ref No.</th><th bgcolor=silver>Customer Ref No.</th><th bgcolor=silver>Advising Detail1</th><th bgcolor=silver>Advising Detail2</th><th bgcolor=silver>Advising Detail3</th><th bgcolor=silver>Advising Detail4</th><th bgcolor=silver>Advising Detail5</th><th bgcolor=silver>Advising Detail6</th><th bgcolor=silver>Advising Detail7</th><th bgcolor=silver>Cheque No.</th><th bgcolor=silver>Instrument Date</th><th bgcolor=silver>MICR No</th><th bgcolor=silver>IFSC Code</th><th bgcolor=silver>Bene Bank Name</th><th bgcolor=silver>Bene Bank Branch</th><th bgcolor=silver>Bene Email ID</th><th bgcolor=silver>Debit A/c Number</th></tr> ");
                            }
                            else
                            {
                                lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "55" : "53") + " align=center>SALARY RATE BREAKUP FOR " + ds.Tables[0].Rows[ctr]["client"] + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DEG</th><th>ACTUAL BASIC + VDA</th><th>BASIC</th><th>HRA</th><th>BONUS</th><th>LEAVE</th><th>WASHING</th><th>TRAVELLING</th><th>EDUCATION</th><th>OTHER ALLOWANCES</th><th>CCA</th><th>ALLOWANCES</th><th>GRATUITY</th><th>SPECIAL ALLOWANCES</th><th>TOTAL</th><th>OT RATE</th><th>OT HOURS</th><th>OT AMOUNT</th><th>GROSS</th><th>PF</th><th>ESIC</th><th>LWF</th><th>UNIFORM RATE</th><th>PT</th><th>BONUS</th><th>LEAVE</th><th>GRATUITY</th><th>ALLOWANCES</th><th>OTHER ALLOWANCES</th><th>FINE</th>" + deduc_uniform + "<th>Advance</th><th>Reliver Deduction</th><th>PRESENT DAYS</th><th>SALARY STATUS</th>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "<th>TAKE HOME</th><th>TRAVEL ALLOWANCE</th>" : "") + "<th bgcolor=silver>N / I</th> <th bgcolor=silver>AMOUNT</th><th bgcolor=silver>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th> " + (i == 3 ? "<th>BATCH ID</th>" : "") + "</tr> ");
                                if ((ot_format == 1 || ot_format == 2) && (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("HDFC")))
                                {
                                    lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=25 align=center>SALARY RATE BREAKUP FOR " + ds.Tables[0].Rows[ctr]["client"] + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DEG</th><th>OT RATE</th><th>OT HOURS</th><th>OT AMOUNT</th><th>PRESENT DAYS</th><th>SALARY STATUS</th><th bgcolor=silver>N / I</th> <th bgcolor=silver>AMOUNT</th><th bgcolor=silver>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th> " + (i == 3 ? "<th>BATCH ID</th>" : "") + "</tr> ");

                                }
                            }
                        }

                        break;
                    }

                case ListItemType.Item:
                    if (Billing_rates.payment_approve_flag == 1)
                    {
                        double Payment = Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString() + Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"].ToString())) - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"].ToString())) + (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())));

                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_advance"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["reliver_advances"].ToString()))) + "</td><td>'" + ds.Tables[0].Rows[ctr]["date"].ToString().Trim('\'', '$') + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>11</td></tr>");


                        ctr++;
                        break;
                    }
                    else if ((ot_format == 1 || ot_format == 2) && (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("HDFC")))
                    {
                        string color = "";
                        if (ds.Tables[0].Rows[ctr]["salary_status"].ToString() == "Hold") { color = "red"; }


                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_rate"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + " </td><td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["salary_status"] + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ds.Tables[0].Rows[ctr]["payment"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td>" + (i == 3 ? "<td>'" + ds.Tables[0].Rows[ctr]["paypro_batch_id"] + "</td>" : "") + " </tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td><td>=SUM(K3:K" + (ctr + 3) + ")</td><td></td><td></td><td>=SUM(N3:N" + (ctr + 3) + ")</td><td></td><td></td><td></td><td></td>";
                        }


                        ctr++;
                        break;
                    }
                    else
                    {
                        string color = "";
                        if (ds.Tables[0].Rows[ctr]["salary_status"].ToString() == "Hold") { color = "red"; }
                        double Payment = Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"].ToString()) - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"].ToString())) + (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())));

                        if (Billing_rates.provisional_payment_flag == 1)
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["hra_amount_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["washing_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["travelling_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["education_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["allowances_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["cca_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["other_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_ot"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_rate"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()), 2) + "</td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"]), 2) + " </td ><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"]), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + " </td><td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["salary_status"] + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()))) + "</td><td>'" + ds.Tables[0].Rows[ctr]["date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td>" + (i == 3 ? "<th>'" + ds.Tables[0].Rows[ctr]["paypro_batch_id"] + "</th>" : "") + "" + (i == 1 ? "<th>" + ds.Tables[0].Rows[ctr]["tran_Status"] + "</th>" : "") + "</tr>");

                        }
                        else
                        {
                            string deduc = "";
                            if (ds.Tables[0].Rows[ctr]["client_code"].ToString() == "BAGICTM") { Payment = Payment + Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["conveyance_rate"].ToString())) - Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["advance_deduction"].ToString())); }
                            // string Convert.ToDouble(ds.Tables[0].Rows[ctr]["deduc"].ToString())
                            string deduc1 = "";
                            if (ds.Tables[0].Rows[ctr]["advance_deduction"].ToString() != "0")
                            {
                                deduc = " <td>" + ds.Tables[0].Rows[ctr]["advance_deduction"] + " </td>";
                                deduc1 = "<td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["advance_deduction"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_advance"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["reliver_advances"].ToString()))) + "</td>";
                            }
                            else
                            {
                                deduc = " <td>" + ds.Tables[0].Rows[ctr]["uni_deduct"] + " </td>";
                                deduc1 = "<td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["uni_deduct"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_advance"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["reliver_advances"].ToString()))) + "</td>"; ;
                            }
                            if (bank == "INDUSIND BANK")
                            {
                                lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["actual_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["hra_amount_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["washing_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["travelling_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["education_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["allowances_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["cca_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["other_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_ot"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_rate"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()), 2) + "</td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"]), 2) + " </td ><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"]), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["fine"] + " </td> " + deduc + " <td>" + ds.Tables[0].Rows[ctr]["emp_advance"] + " </td><td>" + ds.Tables[0].Rows[ctr]["reliver_advances"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + " </td><td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["salary_status"] + " </td>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "<td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["conveyance_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["uni_deduct"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_advance"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["reliver_advances"].ToString()))) + "</td><td>" + Convert.ToDouble(ds.Tables[0].Rows[ctr]["conveyance_rate"].ToString()) + "</td>" : "") + "<td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td></td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td>" + deduc1 + "<td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td> </tr>");
                            }
                            else
                            {
                                lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["actual_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["hra_amount_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["washing_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["travelling_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["education_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["allowances_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["cca_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["other_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_ot"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_rate"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()), 2) + "</td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"]), 2) + " </td ><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"]), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["fine"] + " </td> " + deduc + " <td>" + ds.Tables[0].Rows[ctr]["emp_advance"] + " </td><td>" + ds.Tables[0].Rows[ctr]["reliver_advances"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + " </td><td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["salary_status"] + " </td>" + (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM") ? "<td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["conveyance_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["uni_deduct"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_advance"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["reliver_advances"].ToString()))) + "</td><td>" + Convert.ToDouble(ds.Tables[0].Rows[ctr]["conveyance_rate"].ToString()) + "</td>" : "") + "<td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td>" + deduc1 + "<td>'" + ds.Tables[0].Rows[ctr]["date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td>" + (i == 3 ? "<td>'" + ds.Tables[0].Rows[ctr]["paypro_batch_id"] + "</td>" : "") + " </tr>");
                            }
                        }

                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            if (Billing_rates.provisional_payment_flag == 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=SUM(F3:F" + (ctr + 3) + ")</td><td>=SUM(G3:G" + (ctr + 3) + ")</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td><td>=SUM(K3:K" + (ctr + 3) + ")</td><td>=SUM(L3:L" + (ctr + 3) + ")</td><td>=SUM(M3:M" + (ctr + 3) + ")</td><td>=SUM(N3:N" + (ctr + 3) + ")</td><td>=SUM(O3:O" + (ctr + 3) + ")</td><td>=SUM(P3:P" + (ctr + 3) + ")</td><td>=SUM(Q3:Q" + (ctr + 3) + ")</td><td>=SUM(R3:R" + (ctr + 3) + ")</td><td>=SUM(S3:S" + (ctr + 3) + ")</td><td>=SUM(T3:T" + (ctr + 3) + ")</td><td>=SUM(U3:U" + (ctr + 3) + ")</td><td>=SUM(V3:V" + (ctr + 3) + ")</td><td>=SUM(W3:W" + (ctr + 3) + ")</td><td>=SUM(X3:X" + (ctr + 3) + ")</td><td>=SUM(Y3:Y" + (ctr + 3) + ")</td><td>=SUM(Z3:Z" + (ctr + 3) + ")</td><td>=SUM(AA3:AA" + (ctr + 3) + ")</td><td>=SUM(AB3:AB" + (ctr + 3) + ")</td><td>=SUM(AC3:AC" + (ctr + 3) + ")</td><td>=SUM(AD3:AD" + (ctr + 3) + ")</td><td>=SUM(AE3:AE" + (ctr + 3) + ")</td><td>=SUM(AF3:AF" + (ctr + 3) + ")</td><td>=SUM(AG3:AG" + (ctr + 3) + ")</td><td></td><td></td><td>=SUM(AJ3:AJ" + (ctr + 3) + ")</td><td></td><td></td><td></td><td></td><td></td><td></td>";
                            }
                            else
                            {
                                string bag_tot = "";
                                if (bank == "INDUSIND BANK")
                                {
                                    bag_tot = "</td><td></td><td></td><td><td>=SUM(AS3:AS" + (ctr + 3) + ")</td><td></td>";
                                    if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM"))
                                    {
                                        bag_tot = "<td>=SUM(AP3:AP" + (ctr + 3) + ")</td><td>=SUM(AQ3:AQ" + (ctr + 3) + ")</td><td></td><td></td><td></td><td>=SUM(AU3:AU" + (ctr + 3) + ")</td>";
                                    }

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td><td>=SUM(K3:K" + (ctr + 3) + ")</td><td>=SUM(L3:L" + (ctr + 3) + ")</td><td>=SUM(M3:M" + (ctr + 3) + ")</td><td>=SUM(N3:N" + (ctr + 3) + ")</td><td>=SUM(O3:O" + (ctr + 3) + ")</td><td>=SUM(P3:P" + (ctr + 3) + ")</td><td>=SUM(Q3:Q" + (ctr + 3) + ")</td><td>=SUM(R3:R" + (ctr + 3) + ")</td><td>=SUM(S3:S" + (ctr + 3) + ")</td><td>=SUM(T3:T" + (ctr + 3) + ")</td><td>=SUM(U3:U" + (ctr + 3) + ")</td><td>=SUM(V3:V" + (ctr + 3) + ")</td><td>=SUM(W3:W" + (ctr + 3) + ")</td><td>=SUM(X3:X" + (ctr + 3) + ")</td><td>=SUM(Y3:Y" + (ctr + 3) + ")</td><td>=SUM(Z3:Z" + (ctr + 3) + ")</td><td>=SUM(AA3:AA" + (ctr + 3) + ")</td><td>=SUM(AB3:AB" + (ctr + 3) + ")</td><td>=SUM(AC3:AC" + (ctr + 3) + ")</td><td>=SUM(AD3:AD" + (ctr + 3) + ")</td><td>=SUM(AE3:AE" + (ctr + 3) + ")</td><td>=SUM(AF3:AF" + (ctr + 3) + ")</td><td>=SUM(AG3:AG" + (ctr + 3) + ")</td><td>=SUM(AH3:AH" + (ctr + 3) + ")</td><td>=SUM(AI3:AI" + (ctr + 3) + ")</td><td>=SUM(AJ3:AJ" + (ctr + 3) + ")</td><td>=SUM(AK3:AK" + (ctr + 3) + ")</td><td>=SUM(AL3:AL" + (ctr + 3) + ")</td><td>=SUM(AM3:AM" + (ctr + 3) + ")</td><td>=SUM(AN3:AN" + (ctr + 3) + ")</td><td></td>" + bag_tot + "<td></td>";
                                }
                                else
                                {

                                    bag_tot = "</td><td><td>=SUM(AQ3:AQ" + (ctr + 3) + ")</td><td></td>";
                                    if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAGICTM"))
                                    {
                                        bag_tot = "<td>=SUM(AP3:AP" + (ctr + 3) + ")</td><td>=SUM(AQ3:AQ" + (ctr + 3) + ")</td><td></td><td>=SUM(AS3:AS" + (ctr + 3) + ")</td>";
                                    }

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td><td>=SUM(K3:K" + (ctr + 3) + ")</td><td>=SUM(L3:L" + (ctr + 3) + ")</td><td>=SUM(M3:M" + (ctr + 3) + ")</td><td>=SUM(N3:N" + (ctr + 3) + ")</td><td>=SUM(O3:O" + (ctr + 3) + ")</td><td>=SUM(P3:P" + (ctr + 3) + ")</td><td>=SUM(Q3:Q" + (ctr + 3) + ")</td><td>=SUM(R3:R" + (ctr + 3) + ")</td><td>=SUM(S3:S" + (ctr + 3) + ")</td><td>=SUM(T3:T" + (ctr + 3) + ")</td><td>=SUM(U3:U" + (ctr + 3) + ")</td><td>=SUM(V3:V" + (ctr + 3) + ")</td><td>=SUM(W3:W" + (ctr + 3) + ")</td><td>=SUM(X3:X" + (ctr + 3) + ")</td><td>=SUM(Y3:Y" + (ctr + 3) + ")</td><td>=SUM(Z3:Z" + (ctr + 3) + ")</td><td>=SUM(AA3:AA" + (ctr + 3) + ")</td><td>=SUM(AB3:AB" + (ctr + 3) + ")</td><td>=SUM(AC3:AC" + (ctr + 3) + ")</td><td>=SUM(AD3:AD" + (ctr + 3) + ")</td><td>=SUM(AE3:AE" + (ctr + 3) + ")</td><td>=SUM(AF3:AF" + (ctr + 3) + ")</td><td>=SUM(AG3:AG" + (ctr + 3) + ")</td><td>=SUM(AH3:AH" + (ctr + 3) + ")</td><td>=SUM(AI3:AI" + (ctr + 3) + ")</td><td>=SUM(AJ3:AJ" + (ctr + 3) + ")</td><td>=SUM(AK3:AK" + (ctr + 3) + ")</td><td>=SUM(AL3:AL" + (ctr + 3) + ")</td><td>=SUM(AM3:AM" + (ctr + 3) + ")</td><td>=SUM(AN3:AN" + (ctr + 3) + ")</td><td></td>" + bag_tot + "</td><td></td><td></td><td></td><td></td>";
                                }
                            }
                        }
                        ctr++;
                        break;
                    }
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }


    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate1(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    lc = new LiteralControl("<table border=1>");
                    break;
                case ListItemType.Item:
                    double Payment = Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()) - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString())) + (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"].ToString())));
                    string acc_type = "";
                    string receiver_type = "";
                    if (Payment > 50000)
                    {
                        acc_type = "R";
                    }
                    else
                    {
                        acc_type = ds.Tables[0].Rows[ctr]["NI"].ToString();
                    }
                    if (acc_type == "R")
                    {
                        receiver_type = "10";
                    }
                    else
                    {

                        receiver_type = "11";
                    }

                    lc = new LiteralControl("<tr><td>" + acc_type + " </td><td>" + Payment + "</td><td>'" + ds.Tables[0].Rows[ctr]["date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["EMP_EMAIL_ID"] + "</td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + receiver_type + "</td></tr>");
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


    protected void gridService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
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
            cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "' AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) ORDER BY UNIT_NAME", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) ORDER BY UNIT_NAME", d.con);
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
            show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }
    protected string get_start_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and  pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1),(SELECT start_date_common FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1))");
    }
    protected string get_end_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and  pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1),(SELECT end_date_common FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1))");

    }

    protected void btn_provisional_payment_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        attendance_status();
        provisional_payment_flag = 1;
        export_xl(2);
    }

    //MD change start
    protected void attendance_status()
    {

        try
        {

            ViewState["pending_attendance"] = 0;
            ViewState["appro_attendannce_finanace"] = 0;
            ViewState["payment_approve"] = 0;
            ViewState["finance_approve"] = 0;
            ViewState["finance_not_approve"] = 0;

            System.Data.DataTable dt_item = new System.Data.DataTable();


            pending_finance_panel.Visible = true; approval_finance_panel.Visible = true; payment_approve_panel.Visible = true; approval_by_finance_panel.Visible = true; not_approval_by_finance_panel.Visible = true;
            if (txt_month_year.Text.Length > 0)
            {

                //pending  attendance by finance

                gv_pend_att_finance.DataSource = null;
                gv_pend_att_finance.DataBind();
                dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text, 2, "");
                pending_attendance = "0";

                if (dt_item.Rows.Count > 0)
                {
                    ViewState["pending_attendance"] = dt_item.Rows.Count.ToString();
                    pending_attendance = ViewState["pending_attendance"].ToString();

                    gv_pend_att_finance.DataSource = dt_item;
                    gv_pend_att_finance.DataBind();
                    pending_finance_panel.Visible = true;
                }
                dt_item.Dispose();



                // final bill approve by finance


                gv_appr_att_finance.DataSource = null;
                gv_appr_att_finance.DataBind();

                dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text, 10, "");
                appro_attendannce_finanace = "0";
                if (dt_item.Rows.Count > 0)
                {
                    ViewState["appro_attendannce_finanace"] = dt_item.Rows.Count.ToString();
                    appro_attendannce_finanace = ViewState["appro_attendannce_finanace"].ToString();

                    gv_appr_att_finance.DataSource = dt_item;
                    gv_appr_att_finance.DataBind();

                }
                dt_item.Dispose();

                //payment appprove 


                gv_payment_approve.DataSource = null;
                gv_payment_approve.DataBind();

                dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text, 7, "");
                payment_approve = "0";
                if (dt_item.Rows.Count > 0)
                {
                    ViewState["payment_approve"] = dt_item.Rows.Count.ToString();
                    payment_approve = ViewState["payment_approve"].ToString();

                    gv_payment_approve.DataSource = dt_item;
                    gv_payment_approve.DataBind();

                }
                dt_item.Dispose();


                // approve by finance komal 07-05-2020

                string billing = d.getsinglestring("SELECT distinct billing_wise FROM `pay_client_billing_details` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_code` = '" + ddl_client.SelectedValue + "' AND `billing_name` = 'Manpower Billing'");

                gv_approve_finance.DataSource = null;
                gv_approve_finance.DataBind();



                dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text, 11, billing);
                finance_approve = "0";
                if (dt_item.Rows.Count > 0)
                {
                    ViewState["finance_approve"] = dt_item.Rows.Count.ToString();
                    finance_approve = ViewState["finance_approve"].ToString();

                    gv_approve_finance.DataSource = dt_item;
                    gv_approve_finance.DataBind();

                }
                dt_item.Dispose();

                // not approve by finance komal 07-05-2020
                gv_not_approve_finance.DataSource = null;
                gv_not_approve_finance.DataBind();


                dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text, 12, billing);
                finance_not_approve = "0";
                if (dt_item.Rows.Count > 0)
                {
                    ViewState["finance_not_approve"] = dt_item.Rows.Count.ToString();
                    finance_not_approve = ViewState["finance_not_approve"].ToString();

                    gv_not_approve_finance.DataSource = dt_item;
                    gv_not_approve_finance.DataBind();

                }
                dt_item.Dispose();

                //end
            }
            else
            { pending_finance_panel.Visible = false; approval_finance_panel.Visible = false; }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            //hide_controls();
        }

    }

    protected void insert_salary_histry(int i)
    {
        //check to bill approve then payment proceed
        string flag = "  pay_billing_unit_rate_history.invoice_flag != 0 and";
        string where = "", where_from_to_date = "";
        string delete_where = "", check_finace_flag = "";
        string salary_type = "And (salary_type is null || salary_type ='')";
        string billing_type = "And (bill_type is null || bill_type ='')";
        string insert_date = "'" + ddl_start_date_common.SelectedValue + "' as 'start_date','" + ddl_end_date_common.SelectedValue + "' as 'end_date' ";

        //check paid unpaid employee in pay_pro_master
        string paid_employee = " and pay_billing_unit_rate_history.emp_code not in (select distinct emp_code from pay_pro_master where client_code = '" + ddl_client.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' and paypro_batch_id != '')";
        string unpaid_emp = "  and payment_status != 1";
        //  unpaid_emp = "";
        paid_employee = "";
        string pay_attendance_muster = " pay_billing_unit_rate_history ", pay_billing_master_history = "pay_billing_master_history", pay_salary_unit_rate = "pay_salary_unit_rate";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string sql = null, sql1 = null;

        //arrears payment insert data
        if (i == 2)
        {
            pay_attendance_muster = " pay_billing_unit_rate_history ";
            if (ddl_arrears_type.SelectedValue.Equals("policy"))
            {
                pay_attendance_muster = " pay_billing_unit_rate_history_arrears ";
            }
            salary_type = "";
            billing_type = "";
            pay_billing_master_history = "pay_billing_master_history_arrears";
            pay_salary_unit_rate = "pay_salary_unit_rate_arrears  as pay_salary_unit_rate";

        }
        if (ddl_start_date_common.SelectedValue != "0" && ddl_end_date_common.SelectedValue != "0")
        {

            pay_billing_master_history = "pay_billing_from_to_history as pay_billing_master_history";
            pay_salary_unit_rate = "pay_salary_from_to_unit_rate as pay_salary_unit_rate";
        }
        //  d.con.Open();


        string start_end_date = "AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0)";
        string delete_start_end_date = "AND (start_date = 0 AND end_date = 0) ";
        if (ddl_start_date_common.SelectedValue != "0" && ddl_end_date_common.SelectedValue != "0")
        {
            start_end_date = "AND (pay_billing_unit_rate_history.start_date = " + ddl_start_date_common.SelectedValue + " AND pay_billing_unit_rate_history.end_date = " + ddl_end_date_common.SelectedValue + ")";
            delete_start_end_date = "AND (start_date = " + ddl_start_date_common.SelectedValue + " AND end_date = " + ddl_end_date_common.SelectedValue + ")";
        }

        where = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null)  order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
        where_from_to_date = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_billing_unit_rate_history.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_billing_unit_rate_history.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) and pay_billing_unit_rate_history.start_date=" + ddl_start_date_common.SelectedValue + " and pay_billing_unit_rate_history.end_date=" + ddl_end_date_common.SelectedValue + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name) as table_salary) AS final_salary";
        check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' " + delete_start_end_date + "";

        if (ddl_arrears_type.SelectedValue.Equals("policy"))
        {
            where = "pay_billing_unit_rate_history.month ='" + txt_arrear_month_year.Text.Substring(3, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_arrear_month_year.Text.Substring(6) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
            delete_where = "month='" + txt_arrear_month_year.Text.Substring(3, 2) + "' and Year = '" + txt_arrear_month_year.Text.Substring(6) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        }
        if (ddl_billing_state.SelectedValue == "ALL")
        {
            where = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
            where_from_to_date = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) and pay_billing_unit_rate_history.start_date=" + ddl_start_date_common.SelectedValue + " and pay_billing_unit_rate_history.end_date=" + ddl_end_date_common.SelectedValue + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name) as table_salary) AS final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' " + delete_start_end_date + "";
            check_finace_flag = "  comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'";
            if (ddl_arrears_type.SelectedValue.Equals("policy"))
            {
                where = "pay_billing_unit_rate_history.month ='" + txt_arrear_month_year.Text.Substring(3, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(6) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
                delete_where = "month='" + txt_arrear_month_year.Text.Substring(3, 2) + "' and Year = '" + txt_arrear_month_year.Text.Substring(6) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' ";
            }
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null)  order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
            where_from_to_date = "pay_billing_unit_rate_history.month ='" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_billing_unit_rate_history.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null) and pay_billing_unit_rate_history.start_date=" + ddl_start_date_common.SelectedValue + " and pay_billing_unit_rate_history.end_date=" + ddl_end_date_common.SelectedValue + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name) as table_salary) AS final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "' " + delete_start_end_date + "";
            check_finace_flag = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            if (ddl_arrears_type.SelectedValue.Equals("policy"))
            {
                where = "pay_billing_unit_rate_history.month ='" + txt_arrear_month_year.Text.Substring(3, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_arrear_month_year.Text.Substring(6) + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' and pay_company_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_billing_unit_rate_history.tot_days_present > 0    AND (payment_status != 1 || payment_status is null)  order by pay_unit_master.state_name,pay_unit_master.unit_name) as table_salary) AS final_salary";
                delete_where = "month='" + txt_arrear_month_year.Text.Substring(3, 2) + "' and Year = '" + txt_arrear_month_year.Text.Substring(6) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            }
        }

        string unit_name = d.get_group_concat("select distinct(unit_name) from pay_billing_unit_rate_history where " + check_finace_flag + billing_type + delete_start_end_date + " and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag = 0");

        if (unit_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + " billing  not approve so you can not process payment');", true);
            return;
        }

        if (i == 1)
        {
            string ot_type = "", hdfc = "", ot_where = "", ot_inner = "", hdfc1 = "", ot_where1 = "", ot_inner1 = "";
            ot_type = d.getsinglestring("SELECT billing_ot FROM pay_client_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ");
            if (ot_type == "With OT" && ddl_client.SelectedValue == "HDFC")
            {
                hdfc = " and hdfc_type='manpower_bill'"; ot_where = "  (pay_pro_master.hdfc_type = 'manpower_bill' || pay_pro_master.hdfc_type IS NULL)  and pay_billing_unit_rate_history.hdfc_type = 'manpower_bill'  and ";
                ot_inner = " and (pay_pro_master.hdfc_type = 'manpower_bill' || pay_pro_master.hdfc_type IS NULL)  ";
            }
            else if (ot_type == "Without OT" && ddl_client.SelectedValue == "HDFC")
            {
                hdfc1 = " and hdfc_type='manpower_bill'"; ot_where1 = "  (pay_pro_master.hdfc_type = 'manpower_bill' || pay_pro_master.hdfc_type IS NULL)  and pay_billing_unit_rate_history.hdfc_type = 'manpower_bill'  and ";
                ot_inner1 = " and (pay_pro_master.hdfc_type = 'manpower_bill' || pay_pro_master.hdfc_type IS NULL)  ";
                hdfc = " and hdfc_type='ot_bill'"; ot_where = "  (pay_pro_master.hdfc_type = 'ot_bill' || pay_pro_master.hdfc_type IS NULL) AND pay_billing_unit_rate_history.ot_hours != 0  and pay_billing_unit_rate_history.hdfc_type = 'ot_bill' and ";
                ot_inner = " and (pay_pro_master.hdfc_type = 'ot_bill' || pay_pro_master.hdfc_type IS NULL)  ";
            }

            if (ddl_start_date_common.SelectedValue.Equals("0"))
            {
                if (ot_type == "With OT" && ddl_client.SelectedValue == "HDFC")
                {
                    sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount + common_allow) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances','manpower_bill' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', common_allow, IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.gross ELSE (pay_salary_unit_rate.gross - pay_salary_unit_rate.cca_salary) + pay_employee_master.cca END AS 'total_gross', ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent AS 'sal_esic_percent', pay_salary_unit_rate.sal_pf_percent AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_employee_master.employee_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', IFNULL(pay_employee_salary_details.fine, 0) AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.common_allowance ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_employee_master.employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_employee_master.EMP_CODE, pay_salary_hold.salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year " + ot_inner + "  LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_employee_master.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + ot_where + " " + where;
                }
                else if (ot_type == "Without OT" && ddl_client.SelectedValue == "HDFC")
                {
                    //Without Ot payment 
                    sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount + common_allow) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances','manpower_bill' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', common_allow, IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT  pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT  grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT  Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= STR_TO_DATE('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT  client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.gross ELSE (pay_salary_unit_rate.gross - pay_salary_unit_rate.cca_salary) + pay_employee_master.cca END AS 'total_gross', ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent AS 'sal_esic_percent', pay_salary_unit_rate.sal_pf_percent AS 'sal_pf_percent', ((0 / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_employee_master.employee_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT  MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', IFNULL(pay_employee_salary_details.fine, 0) AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, '0' as 'ot_applicable', '0' as 'esic_ot_applicable', '0' as 'ot_hours', pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.common_allowance ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_employee_master.employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_employee_master.EMP_CODE, pay_salary_hold.salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT  LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', '0' as 'ot_amount_salary', '0' as 'esic_ot_percent', pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender, IFNULL(emp_advance, 0) AS 'emp_advance', IFNULL(reliver_advances, 0) AS 'reliver_advances' FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year " + ot_inner1 + "  LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_employee_master.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + ot_where1 + " " + where;
                    d.operation("delete from pay_pro_master where  " + delete_where + " " + salary_type + unpaid_emp + hdfc1);
                    d.operation("Insert into pay_pro_master(comp_code,unit_code, client_code, state_name, unit_city, emp_name,employee_type, grade,EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ot_rate, ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name,payment,month,year,Emp_Father,Emp_City,Joining_Date,PAN_No,PF_No,EMP_NEW_PAN_No,ESI_No,PerDayRate,Basic,Vda,PF_BANK_NAME,BANK_BRANCH,Bonus_Policy,esic_ot_applicable,COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2,COMP_CITY,COMP_STATE,ihms,Others,working_days,LICENSE_NO,special_allow,esic_ot_percent,esic_ot_amount,total_gross,month_days, bill_esic_percent, sal_esic_percent,designation,actual_basic_vda,start_date,end_date,gender,emp_advance,reliver_advances,hdfc_type) " + sql + "");


                    //OT Payment
                    sql1 = "SELECT  comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, sal_ot, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, EMP_CODE, salary_status, unit_name, (ot_amount) AS 'Payment','" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0) AS 'esic_ot_amount', month_days, bill_esic_percent, designation," + insert_date + ", gender,'ot_bill' FROM (SELECT  state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, sal_ot, Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', EMP_CODE, IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, month_days, bill_esic_percent, designation, pf_cmn_on, gender FROM (SELECT  pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT  grade_desc FROM pay_grade_master WHERE comp_code ='" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT  Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no',  CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y')  THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT  client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_employee_master.employee_type, pay_employee_master.EMP_CODE, pay_salary_hold.salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT  LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year " + ot_inner + "  LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_employee_master.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + ot_where + "" + where;
                    d.operation("delete from pay_pro_master where  " + delete_where + " " + salary_type + unpaid_emp + hdfc);
                    d.operation("Insert into pay_pro_master(comp_code,unit_code, client_code, state_name, unit_city, emp_name,employee_type, grade,EMP_EMAIL_ID,  sal_ot, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ot_rate, ot_hours, ot_amount,  EMP_CODE, salary_status, unit_name,payment,month,year,Emp_Father,Emp_City,Joining_Date,PAN_No,PF_No,EMP_NEW_PAN_No,ESI_No,PerDayRate,PF_BANK_NAME,BANK_BRANCH,Bonus_Policy,esic_ot_applicable,COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2,COMP_CITY,COMP_STATE,ihms,working_days,LICENSE_NO,special_allow,esic_ot_percent,esic_ot_amount,month_days, bill_esic_percent,designation,start_date,end_date,gender,hdfc_type) " + sql1 + "");

                }
                else
                {
                    //sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', IF(esic_common_allow = 0, common_allow, 0) AS 'common_allow', IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.gross ELSE (pay_salary_unit_rate.gross - pay_salary_unit_rate.cca_salary) + pay_employee_master.cca END AS 'total_gross', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent AS 'sal_esic_percent', pay_salary_unit_rate.sal_pf_percent AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_employee_master.employee_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', IFNULL(pay_employee_salary_details.fine, 0) AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.common_allowance / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_employee_master.employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_employee_master.EMP_CODE, pay_salary_hold.salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM pay_employee_master INNER JOIN pay_attendance_muster AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_employee_master.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + " " + where;
                    sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount + common_allow) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances','manpower_bill' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', common_allow, IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.gross ELSE (pay_salary_unit_rate.gross - pay_salary_unit_rate.cca_salary) + pay_employee_master.cca END AS 'total_gross', ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent AS 'sal_esic_percent', pay_salary_unit_rate.sal_pf_percent AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_employee_master.employee_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', IFNULL(pay_employee_salary_details.fine, 0) AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN pay_salary_unit_rate.common_allowance ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_employee_master.employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_employee_master.EMP_CODE, pay_salary_hold.salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year " + ot_inner + "  LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_employee_master.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + ot_where + " " + where;
                }
            }
            else
            {
                //start Changes Vinod - 27/04/2020 from to date
                sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + txt_month_year.Text.Substring(0, 2) + "' AS 'month', '" + txt_month_year.Text.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', IF(esic_common_allow = 0, common_allow, 0) AS 'common_allow', IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city, pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.emp_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_billing_unit_rate_history.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.cca = 0 THEN pay_salary_unit_rate.gross ELSE (pay_salary_unit_rate.gross - pay_salary_unit_rate.cca_salary) + pay_employee_master.cca END AS 'total_gross', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent, pay_salary_unit_rate.sal_pf_percent, ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_billing_unit_rate_history.emp_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_billing_unit_rate_history.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_billing_unit_rate_history.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', IFNULL(pay_employee_salary_details.fine, 0) AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.common_allowance / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_billing_unit_rate_history.emp_type as employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_billing_unit_rate_history.EMP_CODE, pay_salary_hold.salary_status, pay_billing_unit_rate_history.unit_code, pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_billing_unit_rate_history.COMPANY_NAME, pay_billing_unit_rate_history.COMP_ADDRESS1 AS 'COMP_ADDRESS1', pay_billing_unit_rate_history.COMP_ADDRESS2 AS 'COMP_ADDRESS2', pay_billing_unit_rate_history.comp_CITY AS 'COMP_CITY', pay_billing_unit_rate_history.comp_STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_billing_unit_rate_history.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender, 0 'emp_advance', 0 as 'reliver_advances' FROM pay_billing_unit_rate_history inner join pay_employee_master on pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_billing_unit_rate_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation AND pay_billing_unit_rate_history.start_date = pay_salary_unit_rate.start_date and pay_billing_unit_rate_history.end_date = pay_salary_unit_rate.end_date LEFT OUTER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year and pay_billing_unit_rate_history.start_date = pay_pro_master.start_date and pay_billing_unit_rate_history.end_date = pay_pro_master.end_date LEFT OUTER JOIN pay_employee_salary_details ON pay_employee_salary_details.emp_code = pay_billing_unit_rate_history.emp_code AND pay_employee_salary_details.month = pay_billing_unit_rate_history.month AND pay_employee_salary_details.year = pay_billing_unit_rate_history.year LEFT OUTER JOIN pay_salary_hold ON pay_salary_hold.emp_code = pay_billing_unit_rate_history.emp_code AND pay_salary_hold.month = pay_billing_unit_rate_history.month AND pay_salary_hold.year = pay_billing_unit_rate_history.year where " + flag + " " + where_from_to_date;
                //end Changes Vinod - 27/04/2020 from to date
            }
            if (ot_type == "With OT" || ot_type == "" || ot_type == "Select")
            {
                d.operation("delete from pay_pro_master where  " + delete_where + " " + salary_type + unpaid_emp + hdfc);
                d.operation("Insert into pay_pro_master(comp_code,unit_code, client_code, state_name, unit_city, emp_name,employee_type, grade,EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ot_rate, ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name,payment,month,year,Emp_Father,Emp_City,Joining_Date,PAN_No,PF_No,EMP_NEW_PAN_No,ESI_No,PerDayRate,Basic,Vda,PF_BANK_NAME,BANK_BRANCH,Bonus_Policy,esic_ot_applicable,COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2,COMP_CITY,COMP_STATE,ihms,Others,working_days,LICENSE_NO,special_allow,esic_ot_percent,esic_ot_amount,total_gross,month_days, bill_esic_percent, sal_esic_percent,designation,actual_basic_vda,start_date,end_date,gender,emp_advance,reliver_advances,hdfc_type) " + sql + "");
            }
            //vikas add for duction left employee uniform and etc
            //deduction_shoes_uni();
            if (ddl_start_date_common.SelectedValue.Equals("0") || ddl_start_date_common.SelectedValue.Equals("1"))
            {
                empadvance_deduction();
            }
        }
        //arrears payment
        else if (i == 2)
        {
            string month_year = txt_month_year.Text;
            if (ddl_arrears_type.SelectedValue.Equals("policy"))
            {
                month_year = txt_arrear_month_year.Text.Substring(3);//policy wise arrears
            }
            sql = "SELECT comp_code, unit_code, client_code, state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, '0', other_allow, gratuity_gross, gross, sal_pf, IF(gross > 21000, 0, sal_esic) AS 'sal_esic', lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, IFNULL(ot_rate, 0) AS 'ot_rate', ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name, ((gross + sal_bonus_after_gross + leave_sal_after_gross + gratuity_after_gross + esic_allowances_salary + ot_amount) - (sal_pf + IF(gross > 21000, 0, sal_esic) + lwf_salary + sal_uniform_rate + PT_AMOUNT)) AS 'Payment', '" + month_year.Substring(0, 2) + "' AS 'month', '" + month_year.Substring(3) + "' AS 'year', Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, ((emp_basic_vda / Working_days) * 4) AS 'other', Working_days, LICENSE_NO, ot_amount_salary AS 'special_allow', esic_ot_percent, IF(gross > 21000, 0, IF(esic_ot_applicable = 1, ROUND(((ot_amount * esic_ot_percent) / 100), 2), 0)) AS 'esic_ot_amount', total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, " + insert_date + ", gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT state_name, unit_city, emp_name, emp_type, grade, EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, IF(esic_oa_salary = 1, allowances_salary, 0) AS 'allowances_salary', cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', CASE WHEN pf_cmn_on = 0 THEN (((emp_basic_vda) / 100) * sal_pf_percent) WHEN pf_cmn_on = 1 THEN ((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 2 THEN ((emp_basic_vda + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_pf_percent WHEN pf_cmn_on = 3 THEN ((emp_basic_vda + cca_salary + other_allow) / 100) * sal_pf_percent END AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'sal_esic', IF(employee_type = 'Permanent', lwf_salary, rel_lwf) AS 'lwf_salary', sal_uniform_rate, IF(pt_applicable = 1, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE PT END ELSE PT END, 0) AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, IF(advance_payment_mode = 1, (EMP_ADVANCE_PAYMENT / Installment), EMP_ADVANCE_PAYMENT) AS 'EMP_ADVANCE_PAYMENT', Total_Days_Present, Bank_holder_name, original_bank_account_no AS 'BANK_EMP_AC_CODE', PF_IFSC_CODE, Account_no, STATUS, NI, date, client, (ot_applicable) AS 'ot_rate', ot_hours, (ot_applicable * ot_hours) AS 'ot_amount', IF(esic_common_allow = 0, common_allow, 0) AS 'common_allow', IF(esic_oa_salary = 0, allowances_salary, 0) AS 'esic_allowances_salary', EMP_CODE, advance_payment_mode, Installment, EMP_ADVANCE_PAYMENT AS 'advance', IF(salary_status = 1, 'Hold', 'Clear') AS 'salary_status', unit_name, unit_code, client_code, comp_code, Emp_Father, Emp_City, Joining_Date, PAN_No, PF_No, EMP_NEW_PAN_No, ESI_No, PerDayRate, Basic, Vda, PF_BANK_NAME, BANK_BRANCH, Bonus_Policy, esic_ot_applicable, COMPANY_NAME, COMP_ADDRESS1, COMP_ADDRESS2, COMP_CITY, COMP_STATE, ihms, Working_days, LICENSE_NO, ot_amount_salary, esic_ot_percent, total_gross, month_days, bill_esic_percent, sal_esic_percent, designation, actual_basic_vda, pf_cmn_on, gender,ifnull(  emp_advance  ,0) as 'emp_advance',IFNULL(  reliver_advances  ,0) as 'reliver_advances' FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.unit_name, pay_employee_master.emp_name, pay_employee_master.employee_type AS 'emp_type', tot_days_present AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, pay_employee_master.original_bank_account_no, pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'education_salary', ((allowances_salary_original / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'allowances_salary', '0' AS 'cca_salary',  pay_salary_unit_rate.gross AS 'total_gross', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_salary_unit_rate.sal_esic_percent AS 'sal_esic_percent', pay_salary_unit_rate.sal_pf_percent AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) AS 'sal_ot', CASE WHEN pay_salary_unit_rate.pf_cmn_on = 0 THEN ((pay_salary_unit_rate.lwf_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) WHEN pay_employee_master.employee_type = 'Permanent' THEN pay_salary_unit_rate.lwf_salary ELSE 0 END AS 'rel_lwf', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Tamil Nadu' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_salary_unit_rate.basic_vda) / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN bonus_taxable = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE ((pay_employee_master.cca / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + txt_month_year.Text.Substring(0, 2) + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/ " + txt_month_year.Text.Substring(0, 2) + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + txt_month_year.Text.Substring(0, 2) + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', 0 AS 'fine', pay_employee_master.EMP_ADVANCE_PAYMENT, pay_salary_unit_rate.ot_applicable, pay_salary_unit_rate.esic_ot_applicable, pay_billing_unit_rate_history.ot_hours, pay_salary_unit_rate.esic_oa_salary, pay_salary_unit_rate.esic_common_allow, CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.common_allowance / pay_salary_unit_rate.month_days) * pay_billing_unit_rate_history.tot_days_present) ELSE pay_employee_master.special_allow END AS 'common_allow', pay_salary_unit_rate.pt_applicable, pay_employee_master.employee_type, pay_employee_master.advance_payment_mode, pay_employee_master.Installment, pay_employee_master.EMP_CODE, '' as salary_status, pay_unit_master.unit_code, pay_unit_master.client_code, pay_company_master.comp_code, pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'PAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'EMP_NEW_PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_salary_unit_rate.per_rate_salary AS 'PerDayRate', pay_salary_unit_rate.basic_salary AS 'Basic', pay_salary_unit_rate.vda_salary AS 'Vda', pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.bonus_policy_salary AS 'Bonus_Policy', pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1 AS 'COMP_ADDRESS1', pay_company_master.ADDRESS2 AS 'COMP_ADDRESS2', pay_company_master.CITY AS 'COMP_CITY', pay_company_master.STATE AS 'COMP_STATE', pay_employee_master.id_as_per_dob AS 'ihms', pay_billing_unit_rate_history.TOT_WORKING_DAYS AS 'Working_days', (SELECT LICENSE_NO FROM pay_client_master WHERE pay_client_master.client_code = pay_unit_master.client_code) AS 'LICENSE_NO', pay_salary_unit_rate.ot_amount_salary, pay_salary_unit_rate.esic_ot_percent, pay_salary_unit_rate.month_days, pay_salary_unit_rate.bill_esic_percent, pay_salary_unit_rate.designation, (pay_salary_unit_rate.basic_vda) AS 'actual_basic_vda', pay_salary_unit_rate.pf_cmn_on, pay_employee_master.gender, 0 as emp_advance, 0 AS reliver_advances FROM pay_employee_master INNER JOIN " + pay_attendance_muster + " AS pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_billing_unit_rate_history.unit_code = pay_unit_master.unit_code AND pay_billing_unit_rate_history.comp_code = pay_unit_master.comp_code INNER JOIN " + pay_salary_unit_rate + " ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code LEFT OUTER JOIN pay_pro_master_arrears as pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year where " + flag + " " + where;
            d.operation("delete from pay_pro_master_arrears where  " + delete_where + " " + salary_type + unpaid_emp);
            d.operation("Insert into pay_pro_master_arrears(comp_code,unit_code, client_code, state_name, unit_city, emp_name,employee_type, grade,EMP_EMAIL_ID, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ot_rate, ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name,payment,month,year,Emp_Father,Emp_City,Joining_Date,PAN_No,PF_No,EMP_NEW_PAN_No,ESI_No,PerDayRate,Basic,Vda,PF_BANK_NAME,BANK_BRANCH,Bonus_Policy,esic_ot_applicable,COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2,COMP_CITY,COMP_STATE,ihms,Others,working_days,LICENSE_NO,special_allow,esic_ot_percent,esic_ot_amount,total_gross,month_days, bill_esic_percent, sal_esic_percent,designation,actual_basic_vda,start_date,end_date,gender,emp_advance,reliver_advances) " + sql + "");
        }
    }

    protected void btn_paid_salary_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        export_xl(3);
    }

    protected void ddl_invoice_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_invoice_type.SelectedValue == "1")
        {
            ddl_designation.Items.Clear();
            desigpanel.Visible = false;
        }
        else if (ddl_invoice_type.SelectedValue == "2")
        {
            if (txt_month_year.Text != "")
            {
                ddl_designation.Items.Clear();
                desigpanel.Visible = true; int i = 0; string temp = "";
                if (ddl_billing_state.SelectedValue == "ALL")
                {
                    temp = d1.getsinglestring("select group_concat(distinct(designation)) from pay_salary_unit_rate where client_code='" + ddl_client.SelectedValue + "'  and year='" + txt_month_year.Text.Substring(3) + "'and month='" + txt_month_year.Text.Substring(0, 2) + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "')");
                }
                else if (ddl_unitcode.SelectedValue == "ALL")
                {
                    temp = d1.getsinglestring("select group_concat(distinct(designation)) from pay_salary_unit_rate where client_code='" + ddl_client.SelectedValue + "'  and year='" + txt_month_year.Text.Substring(3) + "'and month='" + txt_month_year.Text.Substring(0, 2) + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "')");
                }
                else
                {
                    temp = d1.getsinglestring("select group_concat(distinct(designation)) from pay_salary_unit_rate where client_code='" + ddl_client.SelectedValue + "'  and year='" + txt_month_year.Text.Substring(3) + "'and month='" + txt_month_year.Text.Substring(0, 2) + "' and unit_code = '" + ddl_unitcode.SelectedValue + "'");
                }
                var designationlist = temp.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string designation in designationlist)
                {
                    ddl_designation.Items.Insert(i++, designation);
                }
            }
            else
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Month and try again.');", true); }
        }
    }

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        string where = "", c_type = "", type = "";
        provisional_payment_flag = 0;
        int result = 0;
        string pay_pro_master = "pay_pro_master";
        payment_approve_flag = 1;
        if (sender == "conveyance") { pay_pro_master = " pay_pro_material_history"; type = " and type = 'Conveyance'"; }
        if (sender == "Material") { pay_pro_master = " pay_pro_material_history"; type = " and type = 'Material' "; }


        if (ddl_con_type.SelectedValue == "2")
        {
            c_type = " and conveyance_type='100'";
        }
        if (ddl_con_type.SelectedValue == "1")
        {
            c_type = " and conveyance_type !='100'";
        }
        where = "month ='" + txt_month_year.Text.Substring(0, 2) + "' AND year = '" + txt_month_year.Text.Substring(3) + "' AND client_code = '" + ddl_client.SelectedValue + "'  AND unit_code = '" + ddl_unitcode.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";


        if (ddl_billing_state.SelectedValue == "ALL")
        {
            where = "month ='" + txt_month_year.Text.Substring(0, 2) + "' AND year = '" + txt_month_year.Text.Substring(3) + "' AND client_code = '" + ddl_client.SelectedValue + "'  and comp_code= '" + Session["COMP_CODE"].ToString() + "'";

        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = "month ='" + txt_month_year.Text.Substring(0, 2) + "' AND year = '" + txt_month_year.Text.Substring(3) + "' AND client_code = '" + ddl_client.SelectedValue + "'  and state_name = '" + ddl_billing_state.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";


        }

        result = d.operation("update  " + pay_pro_master + "  set payment_approve ='1' where   " + where + " " + c_type + " " + type + "");
        attendance_status();
        if (result > 0)
        {
            if (sender == "conveyance")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Approve Successfully !!!');", true);
                ddl_con_type.Items.Clear();
            }
            else if (sender == "Material")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Approve Successfully !!!');", true);

            }
            else
            {
                export_xl(1);
            }
        }
    }


    protected void btn_con_process_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";

        string unit = d.chk_payment_approve(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text.Substring(0, 2), txt_month_year.Text.Substring(3), 2, ddl_con_type.SelectedValue);
        if (unit != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This : " + unit + "  Branch Payment Already Approve You Can not Proceed ...');", true);
            return;

        }
        string sql = null, where = null, driver_where = null, delete_where = null, type = " and type = 'Conveyance'";

        where = " WHERE pay_billing_master_history.conveyance_applicable = 1 AND pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' AND pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL) AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) AND pay_attendance_muster.tot_days_present > 0 ORDER BY 4, 3) AS salary_table) AS Final_salary";
        driver_where = "WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' AND pay_conveyance_amount_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_conveyance_amount_history.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL) AND pay_conveyance_amount_history.conveyance_rate = '0' ORDER BY 4, 3) AS salary_table) AS Final_salary GROUP BY emp_code";
        delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and payment_status != 1 " + type;
        string check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        if (ddl_billing_state.SelectedValue == "ALL")
        {

            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  ";
            where = " WHERE pay_billing_master_history.conveyance_applicable = 1 AND pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  AND pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL)  AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) AND pay_attendance_muster.tot_days_present > 0 ORDER BY 4, 3) AS salary_table) AS Final_salary";
            driver_where = "WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  AND pay_conveyance_amount_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_conveyance_amount_history.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL) AND pay_conveyance_amount_history.conveyance_rate = '0' ORDER BY 4, 3) AS salary_table) AS Final_salary GROUP BY emp_code";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and payment_status != 1 " + type;
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {

            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            where = " WHERE pay_billing_master_history.conveyance_applicable = 1 AND pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'  AND pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL)  AND (branch_close_date is null  ||branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y')) AND pay_attendance_muster.tot_days_present > 0 ORDER BY 4, 3) AS salary_table) AS Final_salary";
            driver_where = "WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'  AND pay_conveyance_amount_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_conveyance_amount_history.Year = '" + txt_month_year.Text.Substring(3) + "' AND (payment_status != 1 || payment_status IS NULL) AND pay_conveyance_amount_history.conveyance_rate = '0' ORDER BY 4, 3) AS salary_table) AS Final_salary GROUP BY emp_code";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and payment_status != 1 " + type;
        }
        string c_type = "";
        string deduction = "";

        if (ddl_con_type.SelectedValue == "2")
        {
            c_type = " and conveyance_type='100'";
            deduction = "";
        }
        else
        {
            c_type = " and conveyance_type !='100'";
            deduction = "";
        }
        string unit_name = d.getsinglestring("select group_concat(distinct unit_name) from pay_billing_material_history where " + check_finace_flag + type + " and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag = 0 " + c_type + "");

        if (unit_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + " billing  not approve so you can not process payment');", true);
            return;
        }

        if (ddl_con_type.SelectedValue == "1")
        {
            sql = "SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, conveyance_amount, conveyance_applicable, conveyance_type, Conveyance_PerKmRate, conveyance_km, 'Conveyance',emp_con_deduction,STATUS ,'" + Session["LOGIN_ID"].ToString() + "', date , NI FROM (SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, IF(conveyance_type = 2, ROUND(Conveyance_PerKmRate * conveyance_km), conveyance_rate) AS 'conveyance_amount', conveyance_applicable, conveyance_type, Conveyance_PerKmRate, conveyance_km, 'Conveyance',emp_con_deduction,STATUS ,NI,date FROM (SELECT pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_employee_master.emp_code, client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', pay_employee_master.original_bank_account_no AS 'BANK_EMP_AC_CODE', pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'COMP_AC_NO', (CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name', pay_grade_master.grade_desc, pay_billing_unit_rate.month_days, pay_attendance_muster.month, pay_attendance_muster.year, pay_employee_master.Employee_type AS 'EMP_TYPE', pay_company_master.STATE AS 'COMP_STATE', pay_grade_master.grade_code AS 'GRADE_CODE', pay_conveyance_amount_history.conveyance_rate, pay_billing_master_history.conveyance_applicable, pay_billing_master_history.conveyance_type, pay_billing_master_history.conveyance_rate AS 'Conveyance_PerKmRate', pay_billing_master_history.conveyance_km,pay_conveyance_amount_history.emp_con_deduction,CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours AND pay_billing_master_history.type = 'Conveyance' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code  INNER JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.comp_code = pay_employee_master.comp_code AND pay_conveyance_amount_history.client_code = pay_employee_master.client_code AND pay_conveyance_amount_history.emp_code = pay_employee_master.emp_code AND pay_conveyance_amount_history.month = pay_billing_unit_rate.month AND pay_conveyance_amount_history.year = pay_billing_unit_rate.year AND IF(pay_billing_master_history.conveyance_type != 2, pay_conveyance_amount_history.conveyance_rate, 1) > 0  LEFT OUTER JOIN pay_pro_material_history ON pay_attendance_muster.emp_code = pay_pro_material_history.emp_code AND pay_attendance_muster.month = pay_pro_material_history.month AND pay_attendance_muster.year = pay_pro_material_history.year" + where;
            d.operation("delete from pay_pro_material_history where " + delete_where + " and conveyance_type !='100'");
            d.operation("INSERT INTO pay_pro_material_history (comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, conveyance_amount, conveyance_applicable, conveyance_type, Conveyance_PerKmRate, conveyance_km, type,emp_con_deduction,STATUS,Uploaded_by,uploaded_date,NI)" + sql);
            paypro_no(1);
        }
        if (ddl_con_type.SelectedValue == "2")
        {
            sql = "SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, '100', conveyance_amount, 'Conveyance', driver_con_deduction, STATUS, '" + Session["LOGIN_ID"].ToString() + "', date, NI FROM (SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, '100' , conveyance_amount, 'Conveyance', driver_con_deduction, STATUS, NI, date FROM (SELECT pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_employee_master.emp_code, client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name,CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', pay_employee_master.original_bank_account_no AS 'BANK_EMP_AC_CODE', pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'COMP_AC_NO', (CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name', pay_grade_master.grade_desc, pay_billing_unit_rate.month_days, pay_attendance_muster.month, pay_attendance_muster.year, pay_employee_master.Employee_type AS 'EMP_TYPE', pay_company_master.STATE AS 'COMP_STATE', pay_grade_master.grade_code AS 'GRADE_CODE', '100' , (pay_conveyance_amount_history.conveyance_rate + (food_allowance_rate * food_allowance_days) + (outstation_allowance_rate * outstation_allowance_days) + (outstation_food_allowance_rate * outstation_food_allowance_days) + (night_halt_rate * night_halt_days) + (total_km)) AS 'conveyance_amount', pay_conveyance_amount_history.driver_con_deduction, CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.GRADE_CODE INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code INNER JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.comp_code = pay_employee_master.comp_code AND pay_conveyance_amount_history.client_code = pay_employee_master.client_code AND pay_conveyance_amount_history.emp_code = pay_employee_master.emp_code AND pay_conveyance_amount_history.month = pay_billing_unit_rate.month AND pay_conveyance_amount_history.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master ON pay_billing_master.billing_unit_code = pay_employee_master.unit_code AND pay_billing_master.comp_code = pay_employee_master.comp_code AND pay_billing_master.designation = pay_employee_master.GRADE_CODE  LEFT OUTER JOIN pay_pro_material_history ON pay_attendance_muster.emp_code = pay_pro_material_history.emp_code AND pay_attendance_muster.month = pay_pro_material_history.month AND pay_attendance_muster.year = pay_pro_material_history.year " + driver_where;

            d.operation("delete from pay_pro_material_history where " + delete_where + " and conveyance_type = '100'");
            d.operation("INSERT INTO pay_pro_material_history (comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE,conveyance_type,conveyance_amount,type,driver_con_deduction,STATUS,Uploaded_by,uploaded_date,NI)" + sql);
            paypro_no(1);
        }

    }
    protected void btn_con_xl_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        attendance_status();

        payment_approve_flag = 0;
        get_excel(1);




    }
    protected void btn_con_approve_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        attendance_status();
        object obj = new object();
        obj = "conveyance";
        btn_approve_Click(obj, null);

    }

    protected void get_excel(int i)
    {

        string where = "", grade = "", c_type = "", deduction = "";

        if (ddl_con_type.SelectedValue == "2")
        {
            c_type = " and pay_pro_material_history.conveyance_type='100'";
            deduction = "driver_con_deduction ,";
        }
        if (ddl_con_type.SelectedValue == "1")
        {
            c_type = " and pay_pro_material_history.conveyance_type !='100'";
            deduction = "emp_con_deduction ,";
        }

        if (ddl_invoice_type.SelectedValue == "2")
        {
            grade = " and GRADE_CODE = '" + ddl_designation.SelectedValue + "'";

        }

        string salary_type = "", left_join = "", pay_pro_no = "";
        string pay_attendance_muster = " pay_attendance_muster ";
        if (i == 3) { salary_type = "and pay_pro_material_history.type = 'Material' "; } else { salary_type = "and pay_pro_material_history.type = 'Conveyance' "; }
        string leftemp = leftemp = " and pay_pro_material_history.STATUS != 'LEFT' ";
        string bank_ac_no = " (pay_pro_material_history.BANK_EMP_AC_CODE != ''  and (pay_pro_material_history.BANK_EMP_AC_CODE IS NOT NULL || pay_pro_material_history.BANK_EMP_AC_CODE !=''))";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string sql = null;
        d.con.Open();
        if (i == 1 || i == 2 || i == 3 || i == 4)
        {
            if (ddl_con_xl.SelectedValue == "1" || ddl_material_xl.SelectedValue == "1") { bank_ac_no = "(pay_pro_material_history.BANK_EMP_AC_CODE ='' or pay_pro_material_history.BANK_EMP_AC_CODE IS NULL || pay_pro_material_history.Bank_holder_name ='' or pay_pro_material_history.Bank_holder_name IS NULL|| pay_pro_material_history.PF_IFSC_CODE ='' or pay_pro_material_history.PF_IFSC_CODE IS NULL)"; }
            if (ddl_con_xl.SelectedValue == "2" || ddl_material_xl.SelectedValue == "2") { bank_ac_no = ""; leftemp = "pay_pro_material_history.STATUS = 'LEFT' "; }

            if (ddl_client.SelectedValue != "ALL")
            {
                where = " pay_pro_material_history.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_material_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_material_history.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_material_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_material_history.unit_code = '" + ddl_unitcode.SelectedValue + "'  " + grade + "   " + salary_type + "  ";
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {

                where = " pay_pro_material_history.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_material_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_material_history.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_material_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade + "  " + salary_type;
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {

                where = " pay_pro_material_history.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_material_history.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_material_history.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_material_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_material_history.state_name = '" + ddl_billing_state.SelectedValue + "' " + grade + "  " + salary_type;
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = " pay_pro_material_history.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_material_history.Year = '" + txt_month_year.Text.Substring(3) + "'  and pay_pro_material_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + grade + "  " + salary_type;
            }
            if (i == 1 || i == 3) { where = where + " and payment_Status in (0,2)"; }
            else if (i == 2 || i == 4) { where = where + " and payment_Status = 1"; }
            string amount_type = "";
            if (i == 1 || i == 2) { amount_type = "conveyance_amount"; pay_pro_no = ",pay_emp_paypro.pay_pro_no"; left_join = "LEFT OUTER JOIN pay_emp_paypro ON pay_pro_material_history.emp_code = pay_emp_paypro.emp_code AND pay_pro_material_history.month = pay_emp_paypro.month AND pay_pro_material_history.year = pay_emp_paypro.year AND bank = '" + ddl_bank.SelectedValue + "' AND pay_emp_paypro.type = 1"; }
            else if (i == 3 || i == 4) { amount_type = "material_amount,material_deduction,(material_amount- material_deduction) as 'total_amount'"; }

            // sql = "SELECT client, state_name, unit_name, pay_pro_material_history. emp_name, EMP_TYPE, pay_employee_master.Id_as_per_dob, grade_desc, pay_pro_material_history.status, pay_pro_material_history.Bank_holder_name, pay_pro_material_history.BANK_EMP_AC_CODE, pay_pro_material_history.PF_IFSC_CODE, COMP_AC_NO, " + amount_type + ","+ deduction +", NI, DATE_FORMAT(uploaded_date, '%d-%m-%Y') AS 'uploaded_date', id FROM pay_pro_material_history INNER JOIN pay_employee_master ON pay_pro_material_history.emp_code = pay_employee_master.emp_code and pay_pro_material_history.comp_code = pay_employee_master.comp_code and pay_pro_material_history.unit_code = pay_employee_master.unit_code WHERE " + bank_ac_no + "  " + leftemp + " And " + where + " " + c_type + " order by state_name,unit_name,emp_name";
            sql = "SELECT pay_pro_material_history.client, pay_pro_material_history.state_name, pay_pro_material_history.unit_name, pay_pro_material_history.emp_name, pay_pro_material_history.EMP_TYPE, pay_employee_master.Id_as_per_dob, pay_pro_material_history.grade_desc, pay_pro_material_history.status, pay_pro_material_history.Bank_holder_name, pay_pro_material_history.BANK_EMP_AC_CODE, pay_pro_material_history.PF_IFSC_CODE, COMP_AC_NO, " + amount_type + "," + deduction + " NI, DATE_FORMAT(uploaded_date, '%d-%m-%Y') AS 'uploaded_date', invoice_no, pay_pro_material_history.id " + pay_pro_no + "  FROM pay_pro_material_history INNER JOIN pay_employee_master ON pay_pro_material_history.emp_code = pay_employee_master.emp_code AND pay_pro_material_history.comp_code = pay_employee_master.comp_code AND pay_pro_material_history.unit_code = pay_employee_master.unit_code INNER JOIN pay_billing_material_history ON pay_pro_material_history.emp_code = pay_billing_material_history.emp_code AND pay_pro_material_history.month = pay_billing_material_history.month AND pay_pro_material_history.year = pay_billing_material_history.year AND pay_pro_material_history.type = pay_billing_material_history.type " + left_join + " WHERE " + bank_ac_no + "  " + leftemp + " And " + where + " " + c_type + " order by state_name,unit_name,emp_name";

            // for report table 
            //if (ddl_billing_state.SelectedValue != "ALL")
            //{
            //    MySqlCommand cmd = new MySqlCommand("SELECT  sum(" + amount_type + "), invoice_no FROM pay_pro_material_history INNER JOIN pay_employee_master ON pay_pro_material_history.emp_code = pay_employee_master.emp_code AND pay_pro_material_history.comp_code = pay_employee_master.comp_code AND pay_pro_material_history.unit_code = pay_employee_master.unit_code INNER JOIN pay_billing_material_history ON pay_pro_material_history.emp_code = pay_billing_material_history.emp_code AND pay_pro_material_history.month = pay_billing_material_history.month AND pay_pro_material_history.year = pay_billing_material_history.year AND pay_pro_material_history.type = pay_billing_material_history.type WHERE " + bank_ac_no + "  " + leftemp + " And " + where + " " + c_type + " group by pay_pro_material_history.state_name ", d.con1);
            //    d.con1.Open();
            //    try
            //    {
            //        MySqlDataReader dr1 = cmd.ExecuteReader();

            //        while (dr1.Read())
            //        {
            //            d.operation("update pay_report_gst set payment = " + double.Parse(dr1.GetValue(0).ToString()) + " where invoice_no = '" + dr1.GetValue(1).ToString() + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year ='" + txt_month_year.Text.Substring(3) + "' ");
            //        }
            //    }
            //    catch (Exception ex) { throw ex; }
            //    finally { d.con1.Close(); }

            //}

        }
        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (i == 1)
            {
                Response.AddHeader("content-disposition", "attachment;filename=CONVEYANCE_EMPLOYEE_SALARY.xls");
            }
            if (i == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=CONVEYANCE_PAID_SALARY.xls");
            }
            if (i == 3)
            {
                Response.AddHeader("content-disposition", "attachment;filename=MATERIAL_EMPLOYEE_SALARY.xls");
            }
            if (i == 4)
            {
                Response.AddHeader("content-disposition", "attachment;filename=MATERIAL_PAID_SALARY.xls");
            }
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            string month_d = txt_month_year.Text.Substring(0, 2).ToString();
            string month_name = mfi.GetMonthName(int.Parse(month_d)).ToString();
            string year = txt_month_year.Text.Substring(3).ToString();

            string month_year = month_name.ToUpper() + " " + year;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate3(ListItemType.Header, ds, i, null, month_year, ddl_bank.SelectedValue);
            Repeater1.ItemTemplate = new MyTemplate3(ListItemType.Item, ds, i, ddl_con_type.SelectedValue, null, ddl_bank.SelectedValue);
            Repeater1.FooterTemplate = new MyTemplate3(ListItemType.Footer, null, i, null, null, ddl_bank.SelectedValue);
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


    public class MyTemplate3 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i;
        string con_type;
        string bank;
        string month_year;
        static int ctr;
        public MyTemplate3(ListItemType type, DataSet ds, int i, string con_type, string month_year, string bank)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.con_type = con_type;
            this.month_year = month_year;
            this.bank = bank;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    if (i == 1 || i == 2)
                    {
                        if (bank == "INDUSIND BANK")
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=38 align=center>CONVEYANCE PAYMENT FOR " + ds.Tables[0].Rows[ctr]["client"] + " " + month_year + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DESIGNATION</th><th>CONVEYANCE AMOUNT</th><th>DEDUCTION</th><th bgcolor=silver>Transaction Type</th><th bgcolor=silver>Beneficiary Code</th> <th bgcolor=silver>Beneficiary A/c No.</th><th bgcolor=silver>Transaction Amount</th><th bgcolor=silver>Beneficiary Name</th><th bgcolor=silver>Drawee Location</th><th bgcolor=silver>Print Location</th><th bgcolor=silver>Beneficiary add line1</th><th bgcolor=silver>Beneficiary add line2</th><th bgcolor=silver>Beneficiary add line3</th><th bgcolor=silver>Beneficiary add line4</th><th bgcolor=silver>Zipcode</th><th bgcolor=silver>Instrument Ref No.</th><th bgcolor=silver>Customer Ref No.</th><th bgcolor=silver>Advising Detail1</th><th bgcolor=silver>Advising Detail2</th><th bgcolor=silver>Advising Detail3</th><th bgcolor=silver>Advising Detail4</th><th bgcolor=silver>Advising Detail5</th><th bgcolor=silver>Advising Detail6</th><th bgcolor=silver>Advising Detail7</th><th bgcolor=silver>Cheque No.</th><th bgcolor=silver>Instrument Date</th><th bgcolor=silver>MICR No</th><th bgcolor=silver>IFSC Code</th><th bgcolor=silver>Bene Bank Name</th><th bgcolor=silver>Bene Bank Branch</th><th bgcolor=silver>Bene Email ID</th><th bgcolor=silver>Debit A/c Number</th></tr> ");
                        }
                        else
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=22 align=center>CONVEYANCE PAYMENT FOR " + ds.Tables[0].Rows[ctr]["client"] + " " + month_year + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DESIGNATION</th><th>CONVEYANCE AMOUNT</th><th>DEDUCTION</th><th>NI</th><th>AMOUNT</th><th>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th></th></tr>");
                        }
                    }
                    if (i == 3 || i == 4)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=22 align=center>MATERIAL PAYMENT FOR " + ds.Tables[0].Rows[ctr]["client"] + " " + month_year + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>ID</th><th>DESIGNATION</th><th>MATERIAL AMOUNT</th><th>DEDUCTION AMOUNT</th><th>TOTAL MATERIAL AMOUNT</th><th>NI</th><th>AMOUNT</th><th>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                    string color = "";

                    if ((i == 1 || i == 2) && con_type == "1")
                    {
                        if (bank == "INDUSIND BANK")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_con_deduction"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td></td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td>" + ((Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString()), 2)) - (Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["emp_con_deduction"].ToString()), 2))) + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td> </tr>");
                        }
                        else
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_con_deduction"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ((Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString()), 2)) - (Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["emp_con_deduction"].ToString()), 2))) + " </td><td>'" + ds.Tables[0].Rows[ctr]["uploaded_date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + " </td></tr>");
                        }
                    }

                    if ((i == 1 || i == 2) && con_type == "2")
                    {
                        if (bank == "INDUSIND BANK")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["driver_con_deduction"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td></td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td>" + ((Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString()), 2)) - (Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["driver_con_deduction"].ToString()), 2))) + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td> </tr>");
                        }
                        else
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["driver_con_deduction"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ((Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["conveyance_amount"].ToString()), 2)) - (Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["driver_con_deduction"].ToString()), 2))) + " </td><td>'" + ds.Tables[0].Rows[ctr]["uploaded_date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + " </td></tr>");
                        }
                    }

                    if (i == 3 || i == 4)
                    {

                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Id_as_per_dob"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["material_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["material_deduction"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["total_amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ds.Tables[0].Rows[ctr]["total_amount"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["uploaded_date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ID"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + " </td></tr>");
                    }

                    if (i == 1 || i == 2)
                    {
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            if (bank == "INDUSIND BANK")
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td></td><td></td><td></td><td>=ROUND(SUM(M3:M" + (ctr + 3) + "),0)</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                            }
                            else
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td></td><td>=ROUND(SUM(k3:k" + (ctr + 3) + "),0)</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                            }
                        }
                    }

                    if (i == 3 || i == 4)
                    {
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(L3:L" + (ctr + 3) + ")</td><td></td><td>=ROUND(SUM(J3:J" + (ctr + 3) + "),0)</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
                        }
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
    //chaitali(vendor payment)
    protected void vendor_payment()
    {
        try
        {
            System.Data.DataTable dt_add = new System.Data.DataTable();
            MySqlDataAdapter cmd_add = new MySqlDataAdapter("SELECT cust_name,cust_code FROM pay_transactionp where comp_code='" + Session["comp_code"].ToString() + "'  group by cust_code ORDER BY cust_code", d.con);

            d.con.Open();
            cmd_add.Fill(dt_add);

            if (dt_add.Rows.Count > 0)
            {
                ddl_vendorname.DataSource = dt_add;
                ddl_vendorname.DataTextField = dt_add.Columns[0].ToString();
                ddl_vendorname.DataValueField = dt_add.Columns[1].ToString();
                ddl_vendorname.DataBind();
            }
            ddl_vendorname.Items.Insert(0, "Select");
            d.con.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            d.con.Close();
        }
    }
    protected void vendor_paymentSelectedIndexChanged(object sender, EventArgs e)
    {

        hidtab.Value = "4";

        //purchase invoice no pur_order_no
        try
        {
            ddl_purchase_onum.Items.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlDataAdapter dr = new MySqlDataAdapter("SELECT pur_order_no FROM pay_transactionp where comp_code='" + Session["comp_code"].ToString() + "' and cust_code='" + ddl_vendorname.SelectedValue + "' and month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' group by pur_order_no", d1.con);
            d1.con.Open();
            dr.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_purchase_onum.DataSource = dt;
                ddl_purchase_onum.DataTextField = dt.Columns[0].ToString();
                ddl_purchase_onum.DataValueField = dt.Columns[0].ToString();
                ddl_purchase_onum.DataBind();
            }
            dt.Dispose();

            d1.con.Close();
            ddl_purchase_onum.Items.Insert(0, "Select");
            ddl_purchase_onum.Items.Insert(1, "ALL");

        }
        catch (Exception ex)
        {
        }
        finally
        {
            vendor_bank();
        }


    }
    protected void ddl_purchase_inv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_vendor_save_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        string where = null;
        string where1 = null;
        string sql = null;
        if (ddl_purchase_inv.SelectedValue == "ALL")
        {
            where = " where pay_transactionp.comp_code='" + Session["comp_code"].ToString() + "' and month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' and cust_code='" + ddl_vendorname.SelectedValue + "' and pur_order_no='" + ddl_purchase_onum.SelectedValue + "' ";
            where1 = " where comp_code='" + Session["comp_code"].ToString() + "' and vendor_code='" + ddl_vendorname.SelectedValue + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and po_num='" + ddl_purchase_onum.SelectedValue + "'";
        }
        else
        {
            where = " where pay_transactionp.comp_code='" + Session["comp_code"].ToString() + "' and month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' and cust_code='" + ddl_vendorname.SelectedValue + "' and pur_order_no='" + ddl_purchase_onum.SelectedValue + "' and doc_no='" + ddl_purchase_inv.SelectedValue + "'";
            where1 = " where comp_code='" + Session["comp_code"].ToString() + "' and vendor_code='" + ddl_vendorname.SelectedValue + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and purch_invoice_no='" + ddl_purchase_inv.SelectedValue + "'";
        }
        if (ddl_purchase_onum.SelectedValue == "ALL")
        {
            where = " where pay_transactionp.comp_code='" + Session["comp_code"].ToString() + "' and month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' and cust_code='" + ddl_vendorname.SelectedValue + "'";
            where1 = " where comp_code='" + Session["comp_code"].ToString() + "' and vendor_code='" + ddl_vendorname.SelectedValue + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and vendor_code ='" + ddl_vendorname.SelectedValue + "'";
        }
        string temp1 = d1.getsinglestring("select count(cust_code) from pay_transactionp " + where + " ");
        string temp = d1.getsinglestring("select count(vendor_code) from pay_pro_vendor " + where1 + " and payment_status = 1 ");
        if (temp == temp1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Already Approve You Can not Proceed ...');", true);
            return;
        }

        d.operation("delete from pay_pro_vendor " + where1 + " and payment_status != 1 ");
        sql = "SELECT '" + Session["COMP_CODE"].ToString() + "' as 'comp_code' ,cust_name,cust_code,doc_no,CONCAT(month, '/', year) AS 'month','0' as 'sub_total','0' as 's_gst','0' as 'c_gst','0' as 'i_gst','0' as 'taxable_amt',NET_TOTAL,BANK_AC_NAME,BANK_AC_NUM,IFC_CODE,(SELECT Field2 FROM pay_zone_master WHERE Field1 = '" + ddl_vendor_bank.SelectedValue + "' AND type = 'bank_details' AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'Account no','YES' AS 'STATUS', CASE WHEN (SELECT bank_name FROM pay_vendor_master WHERE VEND_ID = '" + ddl_vendorname.SelectedValue + "') = '" + ddl_vendor_bank.SelectedValue + "' THEN 'I' ELSE 'N' END AS 'NI',DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date','" + ddl_modeofpayment.SelectedValue + "','" + txt_check_number.Text + "','" + txt_check_date.Text + "',pur_order_no FROM pay_transactionp   left JOIN pay_pro_vendor ON pay_pro_vendor.comp_code = pay_transactionp.comp_code  AND pay_pro_vendor.vendor_code = pay_transactionp.cust_code AND pay_transactionp.doc_no = pay_pro_vendor.purch_invoice_no   AND pay_transactionp.month = CASE WHEN LENGTH(month_year) = 6 THEN SUBSTRING(pay_pro_vendor.month_year,1, 1) ELSE SUBSTRING(pay_pro_vendor.month_year,1, 2) END AND pay_transactionp.year = CASE WHEN LENGTH(month_year) = 6 THEN SUBSTRING(pay_pro_vendor.month_year,3) ELSE SUBSTRING(pay_pro_vendor.month_year,4) END  " + where + "  and pay_transactionp.payment_status != 1 group by doc_no";
        int result = d.operation("insert into pay_pro_vendor (comp_code,vendor_id,vendor_code,purch_invoice_no,month_year,sub_total,s_gst,c_gst,i_gst,taxable_amt,grand_total,Bank_holder_name,BANK_EMP_AC_CODE,PF_IFSC_CODE,Account_no,STATUS,NI,date,payment_mode,cheque_num,cheque_date,po_num)" + sql);
        ven_advance_deduction();
        if (result > 0)
        {
            vendor_CRN();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vendor Payment Process sucessfuly.');", true);
        }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found');", true); }
    }
    protected void btn_vendor_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        try
        {
            vendor_annuxture = 0;
            export_vendor_xl(2);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    public class MyTemplate11 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        string bank;
        int i;


        public MyTemplate11(ListItemType type, DataSet ds, int i, string bank)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.bank = bank;
            ctr = 0;

        }

        public void InstantiateIn(Control container)
        {


            switch (type)
            {
                case ListItemType.Header:
                    if (vendor_annuxture == 1)
                    {
                        lc = new LiteralControl("<table border=1>");
                        break;
                    }
                    else
                    {
                        if (bank == "INDUSIND BANK")
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=36 align=center> VENDOR PAYMENT</th></tr><table border=1><tr><th>SR No.</th><th>VENDOR NAME</th><th>VENDOR CODE</th><th>PO NUM</th><th>INVOICE NO</th><th>GRAND TOTAL</th><th>VENDOR ADVANCE</th><th bgcolor=silver>Transaction Type</th><th bgcolor=silver>Beneficiary Code</th> <th bgcolor=silver>Beneficiary A/c No.</th><th bgcolor=silver>Transaction Amount</th><th bgcolor=silver>Beneficiary Name</th><th bgcolor=silver>Drawee Location</th><th bgcolor=silver>Print Location</th><th bgcolor=silver>Beneficiary add line1</th><th bgcolor=silver>Beneficiary add line2</th><th bgcolor=silver>Beneficiary add line3</th><th bgcolor=silver>Beneficiary add line4</th><th bgcolor=silver>Zipcode</th><th bgcolor=silver>Instrument Ref No.</th><th bgcolor=silver>Customer Ref No.</th><th bgcolor=silver>Advising Detail1</th><th bgcolor=silver>Advising Detail2</th><th bgcolor=silver>Advising Detail3</th><th bgcolor=silver>Advising Detail4</th><th bgcolor=silver>Advising Detail5</th><th bgcolor=silver>Advising Detail6</th><th bgcolor=silver>Advising Detail7</th><th bgcolor=silver>Cheque No.</th><th bgcolor=silver>Instrument Date</th><th bgcolor=silver>MICR No</th><th bgcolor=silver>IFSC Code</th><th bgcolor=silver>Bene Bank Name</th><th bgcolor=silver>Bene Bank Branch</th><th bgcolor=silver>Bene Email ID</th><th bgcolor=silver>Debit A/c Number</th></tr>");
                        }
                        else if (bank == "AXIS BANK")
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=20 align=center> VENDOR PAYMENT</th></tr><table border=1><tr><th>SR No.</th><th>VENDOR NAME</th><th>VENDOR CODE</th><th>PO NUM</th><th>INVOICE NO</th><th>GRAND TOTAL</th><th>VENDOR ADVANCE</th><th bgcolor=silver>N / I</th><th bgcolor=silver>AMOUNT</th><th bgcolor=silver>DATE</th><th bgcolor=silver>A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th  bgcolor=silver>INVOICE NO</th>" + (i == 1 ? "<th>BATCH ID</th>" : "") + "</tr>");
                        }
                        break;
                    }


                case ListItemType.Item:
                    if (vendor_annuxture == 1)
                    {

                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["ni"] + "</td><td>" + ((Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["grand_total"].ToString()), 2)) - (Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["vendor_advance"].ToString()), 2))) + " </td><td>" + ds.Tables[0].Rows[ctr]["date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bank_holder_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["bank_emp_ac_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank2"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank3"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pf_ifsc_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["status"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank4"] + "</td><td>" + ds.Tables[0].Rows[ctr]["purch_invoice_no"] + "</td></tr>");//double.Parse(ds.Tables[0].Rows[ctr]["handling_charge"].ToString()
                        ctr++;

                        break;
                    }
                    else
                    {
                        if (bank == "INDUSIND BANK")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td> <td>" + ds.Tables[0].Rows[ctr]["vendor_id"] + "</td><td>" + ds.Tables[0].Rows[ctr]["vendor_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["po_num"] + "</td><td>" + ds.Tables[0].Rows[ctr]["purch_invoice_no"] + "</td><td>" + (Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["grand_total"].ToString()), 2)) + "</td><td>" + ds.Tables[0].Rows[ctr]["vendor_advance"] + "</td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td></td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td>" + ((Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["grand_total"].ToString()))) - (Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["vendor_advance"].ToString())))) + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + " </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ds.Tables[0].Rows[ctr]["pf_ifsc_code"] + " </td><td></td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["account_no"] + "</td></tr>");//double.Parse(ds.Tables[0].Rows[ctr]["handling_charge"].ToString()
                        }
                        else if (bank == "AXIS BANK")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td> <td>" + ds.Tables[0].Rows[ctr]["vendor_id"] + "</td><td>" + ds.Tables[0].Rows[ctr]["vendor_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["po_num"] + "</td><td>" + ds.Tables[0].Rows[ctr]["purch_invoice_no"] + "</td><td>" + (Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["grand_total"].ToString()), 2)) + "</td><td>" + ds.Tables[0].Rows[ctr]["vendor_advance"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ni"] + "</td><td>" + ((Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["grand_total"].ToString()))) - (Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["vendor_advance"].ToString())))) + " </td><td>" + ds.Tables[0].Rows[ctr]["date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bank_holder_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["bank_emp_ac_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank2"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pay_pro_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pf_ifsc_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["status"] + "</td><td>" + ds.Tables[0].Rows[ctr]["blank4"] + "</td><td>" + ds.Tables[0].Rows[ctr]["vendor_invoice_no"] + "</td>" + (i == 1 ? "<td>'" + ds.Tables[0].Rows[ctr]["paypro_batch_id"] + "</td>" : "") + "</tr>");//double.Parse(ds.Tables[0].Rows[ctr]["handling_charge"].ToString()
                        }
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            if (bank == "INDUSIND BANK")
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=Round(SUM(F3:F" + (ctr + 3) + "),2)</td></td><td>=Round(SUM(G3:G" + (ctr + 3) + "),2)</td><td></td><td></td><td></td><td>=Round(SUM(K3:K" + (ctr + 3) + "),2)</td></b></tr>";
                            }
                            else if (bank == "AXIS BANK")
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=Round(SUM(F3:F" + (ctr + 3) + "),2)</td></b></tr>";
                            }
                        }
                        ctr++;

                        break;
                    }


                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }


    }
    protected void btn_paid_vendor_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        try
        {
            vendor_annuxture = 0;
            export_vendor_xl(1);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_ven_annux_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        int result = 0;
        vendor_annuxture = 1;
        string where = "";
        where = "month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "'  AND purch_invoice_no = '" + ddl_purchase_inv.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";
        if (ddl_purchase_inv.SelectedValue == "ALL")
        {
            where = "month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";
        }
        result = d.operation("update  pay_pro_vendor  set payment_approve ='1' where   " + where);
        export_vendor_xl(3);

    }
    private void export_vendor_xl(int i)
    {
        string where = "";
        if (ddl_purchase_inv.SelectedValue == "ALL")
        {
            where = " WHERE  pay_pro_vendor.comp_code = '" + Session["comp_code"].ToString() + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "' AND month_year = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and po_num='" + ddl_purchase_onum.SelectedValue + "'";

        }
        else
        {
            where = " WHERE  pay_pro_vendor.comp_code = '" + Session["comp_code"].ToString() + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "' AND month_year = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and purch_invoice_no='" + ddl_purchase_inv.SelectedValue + "'";

            //`purch_invoice_no`
        }

        if (ddl_purchase_onum.SelectedValue == "ALL")
        {
            where = " WHERE  pay_pro_vendor.comp_code = '" + Session["comp_code"].ToString() + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "' AND month_year = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "'";
            where = " WHERE  pay_pro_vendor.comp_code = '" + Session["comp_code"].ToString() + "' AND vendor_code = '" + ddl_vendorname.SelectedValue + "' AND month_year = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "'";

        }
        string sql = "";
        string payment_status = "";
        string payment_approve = "";
        string payment_mode = "";
        if (i == 1)
        {
            payment_status = "AND pay_pro_vendor.payment_status = 1";
        }
        if (i == 2)
        {
            payment_status = "AND pay_pro_vendor.payment_status != 1";

        }
        if (i == 3)
        {
            payment_status = " AND payment_status = 0";
            payment_approve = " AND payment_approve = 1";
            payment_mode = " and payment_mode='1'";
        }

        sql = "SELECT pay_emp_paypro.pay_pro_no,vendor_id,vendor_code,grand_total,ni,grand_total,date,bank_holder_name,bank_emp_ac_code,'' as 'blank1','' as 'blank2',account_no,'' as 'blank3',pf_ifsc_code,pay_pro_vendor.status,'11' as 'blank4',purch_invoice_no,po_num, vendor_advance,vendor_invoice_no,paypro_batch_id  FROM pay_pro_vendor INNER JOIN pay_transactionp ON pay_transactionp.comp_code = pay_pro_vendor.comp_code AND pay_transactionp.DOC_NO = pay_pro_vendor.purch_invoice_no INNER JOIN `pay_emp_paypro` ON `pay_pro_vendor`.`purch_invoice_no` = `pay_emp_paypro`.emp_code and `pay_pro_vendor`.`comp_code` = `pay_emp_paypro`.comp_code and bank ='" + ddl_vendor_bank.SelectedValue + "' and type=3 " + where + payment_mode + payment_approve + payment_status + " GROUP BY vendor_code";

        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (i == 1)
            {
                Response.AddHeader("content-disposition", "attachment;filename=Vendor_Payment" + ddl_vendorname.SelectedItem.Text.Replace(" ", "_") + ".xls");
            }
            else if (i == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=Vendor_Payment" + ddl_vendorname.SelectedItem.Text.Replace(" ", "_") + ".xls");
            }
            else if (i == 3)
            {
                Response.AddHeader("content-disposition", "attachment;filename=Vendor_annuxture" + ddl_vendorname.SelectedItem.Text.Replace(" ", "_") + ".xls");
            }

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate11(ListItemType.Header, ds, i, ddl_vendor_bank.SelectedValue);
            Repeater1.ItemTemplate = new MyTemplate11(ListItemType.Item, ds, i, ddl_vendor_bank.SelectedValue);
            Repeater1.FooterTemplate = new MyTemplate11(ListItemType.Footer, null, i, null);
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
    protected void vendor_bank()
    {
        ddl_vendor_bank.Items.Clear();
        System.Data.DataTable dt_bank = new System.Data.DataTable();
        MySqlDataAdapter cmd_bank = new MySqlDataAdapter("select payment_ag_bank from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and vendor_id = '" + ddl_vendorname.Text + "' ", d.con);
        d.con.Open();
        try
        {
            cmd_bank.Fill(dt_bank);
            if (dt_bank.Rows.Count > 0)
            {
                ddl_vendor_bank.DataSource = dt_bank;
                ddl_vendor_bank.DataTextField = dt_bank.Columns[0].ToString();
                ddl_vendor_bank.DataValueField = dt_bank.Columns[0].ToString();
                ddl_vendor_bank.DataBind();
            }
            ddl_vendor_bank.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { }
        finally { }
    }
    protected void ddl_purchase_onum_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string purchase_no = "";
            ddl_purchase_inv.Items.Clear();
            if (ddl_purchase_onum.SelectedValue != "ALL")
            {
                purchase_no = "and pur_order_no='" + ddl_purchase_onum.SelectedValue + "'";

            }

            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlDataAdapter dr = new MySqlDataAdapter("SELECT doc_no FROM pay_transactionp where comp_code='" + Session["comp_code"].ToString() + "' and cust_code='" + ddl_vendorname.SelectedValue + "' and month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' " + purchase_no + " ", d1.con);
            d1.con.Open();
            dr.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_purchase_inv.DataSource = dt;
                ddl_purchase_inv.DataTextField = dt.Columns[0].ToString();
                ddl_purchase_inv.DataValueField = dt.Columns[0].ToString();
                ddl_purchase_inv.DataBind();
            }
            dt.Dispose();

            d1.con.Close();
            ddl_purchase_inv.Items.Insert(0, "ALL");

        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    //
    protected void btn_conveyance_paid_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        attendance_status();

        payment_approve_flag = 0;
        get_excel(2);
    }
    private void deduction_shoes_uni()
    {

        // for client wise advance deduction query 13-11-19 without left

        string emp_code = "";
        string where = " comp_code = '" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'and month='" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "'";

        if (ddl_billing_state.SelectedValue != "ALL")
        {

            where = where + " and state_name ='" + ddl_billing_state.SelectedValue + "' ";
        }
        if (ddl_unitcode.SelectedValue != "ALL")
        {

            where = where + " and unit_code = '" + ddl_unitcode.SelectedValue + "'";

        }


        emp_code = d.getsinglestring("select group_concat(emp_code) from pay_pro_master where " + where + "");
        emp_code = "'" + emp_code + "'";
        emp_code = emp_code.Replace(",", "','");

        string[] abc1 = emp_code.Split(',');


        // for pod no and second pod no

        foreach (object obj in abc1)
        {
            string where1 = null;
            // string pod_no_value = "";
            string advance_deduct_field = "";

            advance_deduct_field = d.getsinglestring("select advance_deduction from pay_pro_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and emp_code = " + obj + " and advance_deduction !='0' ");

            if (advance_deduct_field == "")
            {


                where1 = "((`pod_no` IS NOT NULL && `pod_no` != ''|| (`dispatch_by` = '1') || (`dispatch_by` = '2')) && `receiving_date`!='')";
            }
            else if (advance_deduct_field != "")
            {

                where1 = "(`pod_no` IS NOT NULL && `pod_no` != ''|| (`dispatch_by` = '1') || (`dispatch_by` = '2')) and ((`sec_pod_no` IS NOT NULL && `sec_pod_no` != ''|| (`dispatch_by` = '1') || (`dispatch_by` = '2'))&& `sec_receiver_name`!='')";
            }


            d.operation("UPDATE `pay_pro_master` g INNER JOIN (SELECT SUM(CASE WHEN `pay_document_details_history`.`no_of_set` >= `pay_document_details_history`.`no_of_deduction` THEN `pay_document_details_history`.`no_of_deduction` WHEN `pay_document_details_history`.`no_of_set` = `pay_document_details_history`.`no_of_deduction` THEN `pay_document_details_history`.`no_of_deduction` ELSE `pay_document_details_history`.`no_of_set` END * `pay_deduction`.`deduction_amount`) AS 'DED_AMT',`pay_document_details_history`.`emp_code` FROM `pay_document_details_history` INNER JOIN `pay_deduction` ON `pay_document_details_history`.`document_type` = `pay_deduction`.`deduction_item` AND `pay_document_details_history`.`client_code` = `pay_deduction`.`client_code` INNER JOIN `pay_employee_master` ON `pay_document_details_history`.`emp_Code` = `pay_employee_master`.`emp_Code`  INNER JOIN `pay_dispatch_billing` ON `pay_document_details_history`.`comp_code` = `pay_dispatch_billing`.`comp_code` AND `pay_document_details_history`.`emp_code` = `pay_dispatch_billing`.`emp_code` WHERE `pay_document_details_history`.`month` = '" + txt_month_year.Text + "' AND `employee_type` = 'Permanent'  AND `pay_document_details_history`.`client_code` = '" + ddl_client.SelectedValue + "' and pay_document_details_history.comp_code = '" + Session["comp_code"].ToString() + "'and " + where1 + " and pay_document_details_history.emp_code= " + obj + " and `receiver_name_invoice`!='' and `receiving_date`!='' GROUP BY `pay_document_details_history`.`emp_code`) t1 ON `g`.`emp_code` = `t1`.`emp_code` SET `g`.`advance_deduction` = `t1`.`DED_AMT` WHERE `g`.`month` = '" + txt_month_year.Text.Substring(0, 2) + "'  AND `g`.`year` = '" + txt_month_year.Text.Substring(3) + "' AND g.`comp_code` = '" + Session["comp_code"].ToString() + "' AND g.`client_code` = '" + ddl_client.SelectedValue + "' and employee_type='Permanent'  and state_name = '" + ddl_billing_state.SelectedValue + "'");
        }
        // for left employee query 30-12-19

        string daterange = " pay_employee_master.left_date between str_to_Date('" + txt_month_year.Text.Substring(3) + "-" + txt_month_year.Text.Substring(0, 2) + "-01','%Y-%m-%d') and str_to_Date('" + txt_month_year.Text.Substring(3) + "-" + txt_month_year.Text.Substring(0, 2) + "-31','%Y-%m-%d')";
        string start_date_common = get_start_date();
        if (start_date_common != "" && start_date_common != "1")
        {
            daterange = " pay_employee_master.left_date between str_to_Date('" + (int.Parse(txt_month_year.Text.Substring(0, 2)) == 1 ? int.Parse(txt_month_year.Text.Substring(3)) - 1 : int.Parse(txt_month_year.Text.Substring(3))) + "-" + (int.Parse(txt_month_year.Text.Substring(0, 2)) == 1 ? 12 : int.Parse(txt_month_year.Text.Substring(0, 2)) - 1) + "-" + start_date_common + "','%Y-%m-%d') and str_to_date('" + txt_month_year.Text.Substring(3) + "-" + txt_month_year.Text.Substring(0, 2) + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d')";
        }


        string emp_name = d.getsinglestring("SELECT pay_pro_master.`emp_code` FROM `pay_pro_master` INNER JOIN `pay_employee_master` ON `pay_pro_master`.`comp_code` = `pay_employee_master`.`comp_code`  AND `pay_pro_master`.`client_code` = `pay_employee_master`.`client_code` and `pay_pro_master`.`emp_code` = `pay_employee_master`.`emp_code` INNER JOIN `pay_dispatch_billing` ON `pay_pro_master`.`comp_code` = `pay_dispatch_billing`.`comp_code` AND `pay_pro_master`.`emp_code` = `pay_dispatch_billing`.`emp_code` where " + daterange + "  and pay_pro_master.client_code ='" + ddl_client.SelectedValue + "' AND ((`pod_no` IS NOT NULL && `pod_no` != '') || (`dispatch_by` = '1')||(`dispatch_by` = '2')) group by pay_pro_master.emp_code");
        if (emp_name != "")
        {
            string[] abc = emp_name.Split(',');

            foreach (object obj in abc)
            {
                string total_advance_pro = d.getsinglestring("select sum(advance_deduction) from pay_pro_master where emp_code='" + obj + "'");
                if (total_advance_pro == null || total_advance_pro == "")
                {
                    total_advance_pro = "0";
                }
                string total_deduction = d.getsinglestring("SELECT SUM(`pay_document_details`.`admin_no_of_set` * `pay_deduction`.`deduction_amount`) AS 'DED_AMT' FROM `pay_document_details` INNER JOIN `pay_deduction` ON `pay_document_details`.`document_type` = `pay_deduction`.`deduction_item` AND `pay_document_details`.`client_code` = `pay_deduction`.`client_code` WHERE `EMP_CODE` = '" + obj + "'");
                if (total_deduction == null || total_deduction == "")
                {
                    total_deduction = "0";
                }
                double total = (double.Parse(total_deduction) - double.Parse(total_advance_pro));
                if (total == null)
                {
                    total = 0;
                }

                d.operation("update pay_pro_master set deduction='" + total + "' where comp_code='c01' and emp_code='" + obj + "' and client_code='" + ddl_client.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ");
            }

        }





    }


    protected void empadvance_deduction()
    {

        d.operation("UPDATE pay_pro_master INNER JOIN (SELECT deduction_amount, advance_service, pay_to_emp_code, g.month, g.year FROM pay_advance_policy g INNER JOIN pay_pro_master ON g.pay_to_emp_code = pay_pro_master.emp_code AND g.month = pay_pro_master.month AND g.year = pay_pro_master.year AND  g . month = '" + txt_month_year.Text.Substring(0, 2) + "' AND  g . year = '" + txt_month_year.Text.Substring(3) + "' AND g.client_code='" + ddl_client.SelectedValue + "' and  g.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND advance_service = 'Self Advance') t1 ON t1.pay_to_emp_code = pay_pro_master.emp_code AND t1.month = pay_pro_master.month AND t1.year = pay_pro_master.year SET emp_advance = t1.deduction_amount WHERE  pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Text.Substring(3) + "'");

        d.operation(" UPDATE pay_advance_policy INNER JOIN  pay_pro_master  ON  pay_advance_policy . pay_to_emp_code  =  pay_pro_master . EMP_CODE  AND  pay_advance_policy . month  =  pay_pro_master . month  AND  pay_advance_policy . year  =  pay_pro_master . year SET pay_flag  = '1' WHERE pay_pro_master .month = '" + txt_month_year.Text.Substring(0, 2) + "' AND  pay_pro_master . year = '" + txt_month_year.Text.Substring(3) + "' AND  advance_service  = 'Self Advance' AND  emp_advance  != '0' ");

        d.operation("UPDATE pay_pro_master INNER JOIN (SELECT deduction_amount, advance_service, pay_to_emp_code, g.month, g.year FROM pay_advance_policy g INNER JOIN pay_pro_master ON g.pay_to_emp_code = pay_pro_master.emp_code AND g.month = pay_pro_master.month AND g.year = pay_pro_master.year AND  g . month = '" + txt_month_year.Text.Substring(0, 2) + "' AND  g . year = '" + txt_month_year.Text.Substring(3) + "' AND g.client_code='" + ddl_client.SelectedValue + "' and  g.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND advance_service  = 'Reliver Payment') t1 ON t1.pay_to_emp_code = pay_pro_master.emp_code AND t1.month = pay_pro_master.month AND t1.year = pay_pro_master.year SET reliver_advances  =  t1 . deduction_amount WHERE  pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Text.Substring(3) + "'");

        d.operation(" UPDATE pay_advance_policy INNER JOIN  pay_pro_master  ON  pay_advance_policy . pay_to_emp_code  =  pay_pro_master . EMP_CODE  AND  pay_advance_policy . month  =  pay_pro_master . month  AND  pay_advance_policy . year  =  pay_pro_master . year SET pay_flag  = '1' WHERE pay_pro_master .month = '" + txt_month_year.Text.Substring(0, 2) + "' AND  pay_pro_master . year = '" + txt_month_year.Text.Substring(3) + "' AND  advance_service  = 'reliver_advances' AND  reliver_advances  != '0' ");



    }

    //Material process

    protected void btn_material_process_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        string sql = null, where = null, delete_where = null, type = " and type = 'Material'";

        string unit = d.chk_payment_approve(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, txt_month_year.Text.Substring(0, 2), txt_month_year.Text.Substring(3), 2, "0");
        if (unit != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This : " + unit + "  Branch Payment Already Approve You Can not Proceed ...');", true);
            return;

        }

        where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' AND pay_material_details.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_material_details.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null  || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_material_details.material_amount != '0' AND contract_type = 4  group by emp_code ORDER BY 4, 3) AS salary_table) AS Final_salary";

        delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' " + type;
        string check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        if (ddl_billing_state.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  ";
            where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_material_details.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_material_details.material_amount != '0' AND contract_type = 4  group by emp_code ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' " + type;
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            where = " WHERE pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'  AND pay_material_details.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_material_details.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_material_details.material_amount != '0' AND contract_type = 4  group by emp_code ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "' " + type;
        }



        string unit_name = d.getsinglestring("select group_concat(distinct unit_name) from pay_billing_material_history where " + check_finace_flag + type + " and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and invoice_flag = 0 ");

        if (unit_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + " billing  not approve so you can not process payment');", true);
            return;
        }


        sql = "SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, material_amount,material_deduction, 'Material', STATUS, '" + Session["LOGIN_ID"].ToString() + "', date, NI FROM (SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, material_amount,material_deduction, 'Material', STATUS, NI, date FROM (SELECT pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_employee_master.emp_code, pay_client_master.client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y')  AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', pay_employee_master.original_bank_account_no AS 'BANK_EMP_AC_CODE', pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'COMP_AC_NO', (CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name', pay_grade_master.grade_desc, pay_billing_unit_rate.month_days, pay_attendance_muster.month, pay_attendance_muster.year, pay_employee_master.Employee_type AS 'EMP_TYPE', pay_company_master.STATE AS 'COMP_STATE', pay_grade_master.grade_code AS 'GRADE_CODE', material_amount,material_deduction, CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.GRADE_CODE INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code INNER JOIN pay_material_details ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.client_code = pay_employee_master.client_code AND pay_material_details.emp_code = pay_employee_master.emp_code AND pay_material_details.month = pay_billing_unit_rate.month AND pay_material_details.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master ON pay_billing_master.billing_unit_code = pay_employee_master.unit_code AND pay_billing_master.comp_code = pay_employee_master.comp_code AND pay_billing_master.designation = pay_employee_master.GRADE_CODE " + where;

        d.operation("delete from pay_pro_material_history where " + delete_where + "");
        d.operation("INSERT INTO pay_pro_material_history (comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, material_amount,material_deduction, type,STATUS,Uploaded_by,uploaded_date,NI)" + sql);
        // paypro_no(2);

    }
    protected void btn_material_xl_Click(object sender, EventArgs e)
    {
        payment_approve_flag = 0;
        get_excel(3);
    }
    protected void btn_material_paid_Click(object sender, EventArgs e)
    {
        payment_approve_flag = 0;
        get_excel(4);
    }
    protected void btn_material_approve_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        attendance_status();
        object obj = new object();
        obj = "Material";
        btn_approve_Click(obj, null);
    }
    protected void btn_material_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    //vikas add from vendor advance
    protected void ven_advance_deduction()
    {
        d.operation("update pay_pro_vendor g inner join ( select  amount,invoice_no  from pay_vendor_advance inner join pay_pro_vendor on pay_vendor_advance. po_no  = pay_pro_vendor.po_num  and pay_vendor_advance. invoice_no  = pay_pro_vendor. purch_invoice_no  where month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and po_no='" + ddl_purchase_onum.SelectedValue + "' ) t1 set  vendor_advance = t1.amount where comp_code='" + Session["comp_code"].ToString() + "' and vendor_code='" + ddl_vendorname.SelectedValue + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' and po_num='" + ddl_purchase_onum.SelectedValue + "' and  purch_invoice_no = t1.invoice_no   ");
        d.operation("UPDATE  pay_vendor_advance  g INNER JOIN (SELECT month_year,comp_code,po_num,invoice_no FROM  pay_pro_vendor  INNER JOIN  pay_vendor_advance  ON  pay_pro_vendor . po_num  = pay_vendor_advance . po_no    AND   pay_pro_vendor . purch_invoice_no  = pay_vendor_advance . invoice_no  WHERE   po_no  = '" + ddl_purchase_onum.SelectedValue + "' AND  month_year  = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "' ) t1 SET  g. pay_flag  = '1' WHERE  t1. comp_code  = '" + Session["comp_code"].ToString() + "' AND  vendor_code  = '" + ddl_vendorname.SelectedValue + "' AND t1. month_year  = '" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "'    AND t1. po_num  = '" + ddl_purchase_onum.SelectedValue + "'    and g. invoice_no =t1. invoice_no ");

    }
    protected void btn_save_arrear_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string where_state = "", where_check = "";
        if (!ddl_billing_state.Equals("ALL")) { where_state = " and state='" + ddl_billing_state.SelectedValue + "'"; }

        else
        { where_state = ""; }
        where_check = " and client_code = '" + ddl_client.SelectedValue + "'";

        if (ddl_billing_state.SelectedValue != "ALL")
        {


            where_check = " and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "'";
        }
        if (ddl_unitcode.SelectedValue != "ALL")
        {

            where_check = " and client_code = '" + ddl_client.SelectedValue + "'  and unit_code  = '" + ddl_unitcode.SelectedValue + "'";
        }
        string unit_name = "";
        if (ddl_arrears_type.SelectedValue.Equals("policy"))
        {
            unit_name = d1.getsinglestring("SELECT group_concat(distinct unit_name) FROM pay_billing_unit_rate_history_arrears where client_Code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_arrear_monthend.Text.Substring(3, 2) + "' AND YEAR='" + txt_arrear_monthend.Text.Substring(6) + "' and invoice_flag = 0  ");
        }
        else if (ddl_arrears_type.SelectedValue.Equals("month"))
        {
            unit_name = d1.getsinglestring("SELECT group_concat(distinct unit_name) FROM pay_billing_unit_rate_history_arrears where client_Code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month_year.Text.Substring(0, 2) + "' AND YEAR='" + txt_month_year.Text.Substring(3) + "' and invoice_flag = 0 ");
        }
        if (!unit_name.Equals("")) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bill Of This Branches : " + unit_name + "  Are Not Approved So You Can Not Proceed Arrears Payment!!!');", true); return; }



        string flag = "and pay_attendance_muster.flag = 2";
        string where = " and billing_unit_code in (select unit_Code from pay_unit_master where client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "')";
        string from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_billing_state.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
        if (ddl_billing_state.SelectedValue == "ALL")
        {
            from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
            where = " and billing_unit_code in (select unit_Code from pay_unit_master where client_code = '" + ddl_client.SelectedValue + "')";
        }
        else if (ddl_unitcode.SelectedValue != "ALL")
        {
            from_to_where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_unitcode.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and start_date = '" + ddl_start_date_common.SelectedValue + "'  and  end_date  = '" + ddl_end_date_common.SelectedValue + "'";
            where = " and billing_unit_code  = '" + ddl_unitcode.SelectedValue + "'";
        }
        string temp = d1.getsinglestring("SELECT distinct(month_end) FROM pay_billing_master_history_arrears where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month_year.Text.Substring(0, 2) + "' AND YEAR='" + txt_month_year.Text.Substring(3, 4) + "'" + where);
        if (temp == "0" || temp == "")
        {


            if (int.Parse(ddl_process_data.SelectedValue).Equals(0))
            {
                int i = 3;//month wise arrears
                string month_year = txt_month_year.Text;
                if (ddl_arrears_type.SelectedValue.Equals("policy"))
                {
                    month_year = txt_arrear_month_year.Text.Substring(3); i = 2;//policy wise arrears
                }
                else { txt_arrear_month_year.Text = "000"; txt_arrear_monthend.Text = "000"; }
                bs.Salary(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_billing_state.SelectedValue, ddl_unitcode.SelectedValue, month_year, Session["LOGIN_ID"].ToString(), i, txt_arrear_month_year.Text.Substring(0, 2), txt_arrear_monthend.Text.Substring(0, 2));

            }
            insert_salary_histry(2);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You Cannot Make Changes as Month End Process have Completed !!!');", true);
        }
        gv_fullmonthot.Visible = false;
        paypro_no(4);
        d1.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1;

            if (ddl_billing_state.SelectedValue == "ALL")
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT +common_allowance) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT',CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and  pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0  " + flag + "  order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT',CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' and  pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0 " + flag + " order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            else
            {
                adp1 = new MySqlDataAdapter("SELECT distinct STATE_NAME AS 'STATE NAME', UNIT_CITY AS 'UNIT CITY', EMPLOYEE_NAME AS 'EMPLOYEE NAME', DESIGNATION, (( gross+common_allowance ) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) as 'TOTAL SALARY', OT_AMOUNT as 'OT AMOUNT', TOTAL_DAYS_PRESENT as 'TOTAL DAYS PRESENT',FLOOR(((( gross  +  OT_AMOUNT +common_allowance) - ( total_deduction  - IF( OT_AMOUNT  > 0, ( sal_esic  -  esic_amount ), 0))) /  month_days ) *  TOTAL_DAYS_PRESENT ) as 'PAYMENT', BANK_HOLDER_NAME as 'BANK HOLDER NAME', ACCOUNT_NO as 'ACCOUNT NO', IFSC_CODE as 'IFSC CODE', BENE_NO as 'BENE NO', STATUS, NI, DATE, CLIENT FROM (SELECT pay_unit_master.state_name as 'STATE_NAME', unit_city AS 'UNIT_CITY', pay_employee_master.emp_name AS 'EMPLOYEE_NAME', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and grade_code = pay_salary_unit_rate.designation) AS 'DESIGNATION',(pay_salary_unit_rate.ot_applicable  * pay_attendance_muster.ot_hours) AS 'OT_AMOUNT', CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'TOTAL_DAYS_PRESENT', pay_employee_master.Bank_holder_name AS 'BANK_HOLDER_NAME', pay_employee_master.original_bank_account_no AS 'ACCOUNT_NO', pay_employee_master.PF_IFSC_CODE AS 'IFSC_CODE', (SELECT Field2 FROM pay_zone_master WHERE type = 'bank_details' and comp_code = pay_employee_master.comp_code AND Field1 = '" + ddl_bank.SelectedValue + "') AS 'BENE_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'DATE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = 	pay_unit_master.client_code) AS 'CLIENT', pay_salary_unit_rate.gross, pay_salary_unit_rate.sal_pf AS 'pf_amount', (((pay_salary_unit_rate.gross + (pay_salary_unit_rate.ot_applicable * pay_attendance_muster.ot_hours)) * pay_billing_master_history.sal_esic) / 100) AS 'esic_amount', pay_salary_unit_rate.total_deduction, pay_salary_unit_rate.sal_esic, pay_salary_unit_rate.month_days,pay_salary_unit_rate.common_allowance FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_employee_master.grade_code = pay_salary_unit_rate.designation AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year 	AND pay_attendance_muster.comp_code = pay_salary_unit_rate.comp_code INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours WHERE pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code =  '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_attendance_muster.month = " + txt_month_year.Text.Substring(0, 2) + " AND  pay_attendance_muster.year = " + txt_month_year.Text.Substring(3) + " AND  pay_attendance_muster.tot_days_present > 0  " + flag + " order by pay_unit_master.unit_code,pay_employee_master.emp_code) AS salary_grid", d1.con1);
            }
            adp1.Fill(ds1);
            gv_fullmonthot.DataSource = ds1.Tables[0];
            gv_fullmonthot.DataBind();
            show_controls();
            d1.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
            //paypro_no(1);
        }
    }
    protected void btn_paid_salary_arrear_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        export_xl(5);
    }
    protected void btn_approve_arrear_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        string where = "", c_type = "", type = "";
        provisional_payment_flag = 0;
        int result = 0;
        string pay_pro_master = "pay_pro_master_arrears";
        payment_approve_flag = 1;

        string month_year = "month ='" + txt_month_year.Text.Substring(0, 2) + "' AND year = '" + txt_month_year.Text.Substring(3) + "'";

        if (ddl_arrears_type.SelectedValue.Equals("policy"))
        {
            month_year = "month ='" + txt_arrear_month_year.Text.Substring(3, 2) + "' AND year = '" + txt_arrear_month_year.Text.Substring(6) + "'";
        }

        where = month_year + " AND client_code = '" + ddl_client.SelectedValue + "'  AND unit_code = '" + ddl_unitcode.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";
        if (ddl_billing_state.SelectedValue == "ALL")
        {
            where = month_year + " AND client_code = '" + ddl_client.SelectedValue + "'  and comp_code= '" + Session["COMP_CODE"].ToString() + "'";

        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = month_year + " AND client_code = '" + ddl_client.SelectedValue + "'  and state_name = '" + ddl_billing_state.SelectedValue + "' and comp_code= '" + Session["COMP_CODE"].ToString() + "'";
        }

        result = d.operation("update  " + pay_pro_master + "  set payment_approve ='1' where   " + where + " " + c_type + " " + type + "");
        if (result > 0)
        {
            export_xl(4);
        }

    }
    protected void btn_breakup_arrear_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        export_xl(4);
    }
    protected void paypro_no(int i)
    {
        //pay_pro_no
        string where = "";
        string table = "", month = "", year = "";
        //vinod for region
        string where_state = "", c_type = "";
        month = " and month='" + txt_month_year.Text.Substring(0, 2) + "'";
        year = "and year = '" + txt_month_year.Text.Substring(3) + "'";


        if (i == 1)
        {
            table = "pay_pro_material_history as ";
            if (ddl_con_type.SelectedValue == "2")
            {
                c_type = " and conveyance_type='100'";

            }
            else if (ddl_con_type.SelectedValue == "1")
            {
                c_type = " and conveyance_type !='100'";
            }
        }
        else if (i == 4)
        {
            table = "pay_pro_master_arrears as ";
            if (ddl_arrears_type.SelectedValue == "policy")
            {
                month = " and month='" + txt_arrear_month_year.Text.Substring(3, 2) + "'";
                year = "and year = '" + txt_arrear_month_year.Text.Substring(6) + "'";
            }
            else
            {
                month = " and month='" + txt_month_year.Text.Substring(0, 2) + "'";
                year = "and year = '" + txt_month_year.Text.Substring(3) + "'";
            }
        }
        //if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + ddl_client.SelectedValue + "' " + where_state).Equals("5"))
        //{
        //    where_state = " and pay_billing_unit_rate_history.zone = '" + ddlregion.SelectedValue + "'";
        //}
        //else
        //{ where_state = ""; }
        if (d.getsinglestring("select paypro_no from pay_company_master where comp_code='" + Session["COMP_CODE"].ToString() + "'").Equals("Enable"))
        {
            if (ddl_unitcode.SelectedValue == "ALL")
            {
                where = " where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code= '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' " + c_type + "  " + month + " " + year;
            }
            else
            {
                where = " where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code= '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "'  " + c_type + " " + month + " " + year;
            }
            // d.operation("delete from pay_emp_paypro  where comp_code='" + Session["COMP_CODE"].ToString() + "' and type = " + i + " and month ='" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and emp_code is not null and emp_code in(select emp_code from " + arrears + "pay_pro_master " + where + " and payment_status != '1') ");
            if (ddl_bank.SelectedValue == "AXIS BANK")
            {
                string Sql = "select emp_code, month,year," + i + ",0,@i:=@i+1,comp_code,'" + ddl_bank.SelectedValue + "' from  " + table + "pay_pro_master " + where + " and emp_code not in (select emp_code from pay_emp_paypro where type = " + i + " and month ='" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and bank = 'AXIS BANK' and emp_code is not null)";
                d.operation("set @i:= (select ifnull(max(pay_pro_no),21000) from pay_emp_paypro where pay_pro_no < 98000 and bank = 'AXIS BANK');insert into pay_emp_paypro(emp_code,month,year,type,status,pay_pro_no,comp_code,bank)" + Sql);
            }
            else if (ddl_bank.SelectedValue == "INDUSIND BANK")
            {
                string Sql = "select emp_code, month,year," + i + ",0,@i:=@i+1,comp_code,'" + ddl_bank.SelectedValue + "' from  " + table + "pay_pro_master " + where + " and emp_code not in (select emp_code from pay_emp_paypro where type = " + i + " and month ='" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and bank = 'INDUSIND BANK' and emp_code is not null)";
                d.operation("set @i:= (select ifnull(max(pay_pro_no),0) from pay_emp_paypro where pay_pro_no < 98000 and bank = 'INDUSIND BANK');insert into pay_emp_paypro(emp_code,month,year,type,status,pay_pro_no,comp_code,bank)" + Sql);
            }
        }
    }

    protected void btn_report_Click(object sender, EventArgs e)
    {
        attendance_status();
    }
    protected void vendor_CRN()
    {
        if (ddl_vendor_bank.SelectedValue == "AXIS BANK")
        {
            string Sql = "select purch_invoice_no, '" + txt_vendor_month.Text.Substring(0, 2) + "','" + txt_vendor_month.Text.Substring(3) + "',3,0,@i:=@i+1,'" + Session["COMP_CODE"].ToString() + "','" + ddl_vendor_bank.SelectedValue + "' from pay_pro_vendor where comp_code='" + Session["COMP_CODE"].ToString() + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "'  and vendor_code='" + ddl_vendorname.SelectedValue + "' and purch_invoice_no not in (select emp_code from pay_emp_paypro where month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' and  type = '3'and bank = 'AXIS BANK' AND emp_code IS NOT NULL) GROUP BY purch_invoice_no";
            d.operation("set @i:= (select ifnull(max(pay_pro_no),21000) from pay_emp_paypro where pay_pro_no < 98000 and bank = 'AXIS BANK');insert into pay_emp_paypro(emp_code,month,year,type,status,pay_pro_no,comp_code,bank)" + Sql);
        }
        else if (ddl_vendor_bank.SelectedValue == "INDUSIND BANK")
        {
            string Sql = "select purch_invoice_no, '" + txt_vendor_month.Text.Substring(0, 2) + "','" + txt_vendor_month.Text.Substring(3) + "',3,0,@i:=@i+1,'" + Session["COMP_CODE"].ToString() + "','" + ddl_vendor_bank.SelectedValue + "' from pay_pro_vendor where comp_code='" + Session["COMP_CODE"].ToString() + "' and month_year='" + (txt_vendor_month.Text.Substring(0, 1).Equals("0") ? txt_vendor_month.Text.Substring(1, txt_vendor_month.Text.Length - 1) : txt_vendor_month.Text) + "'  and vendor_code='" + ddl_vendorname.SelectedValue + "' and purch_invoice_no not in (select emp_code from pay_emp_paypro where month='" + txt_vendor_month.Text.Substring(0, 2) + "' and year='" + txt_vendor_month.Text.Substring(3) + "' and  type = '3' and bank = 'INDUSIND BANK' AND emp_code IS NOT NULL) GROUP BY purch_invoice_no";
            d.operation("set @i:= (select ifnull(max(pay_pro_no),0) from pay_emp_paypro where pay_pro_no < 98000 and bank = 'INDUSIND BANK');insert into pay_emp_paypro(emp_code,month,year,type,status,pay_pro_no,comp_code,bank)" + Sql);
        }
    }
    protected void ot_btn_paid_salary_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        attendance_status();
        ot_payment = 1;
        payment_approve_flag = 0;
        provisional_payment_flag = 0;
        export_xl(3);

    }
    protected void ot_btn_breakup_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        ot_payment = 1;
        export_xl(1);

    }
    protected void btn_rm_process_Click(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        string sql = null, where = null, delete_where = null;


        where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' AND pay_billing_r_m.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null  || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_r_m.amount != '0'   and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2  group by pay_billing_r_m.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
        string check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "'  and payment_Status != 1";

        if (ddl_billing_state.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  ";
            where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  AND pay_billing_r_m.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_r_m.amount != '0'  and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2 group by pay_billing_r_m.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'  and payment_Status != 1";
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            where = " WHERE pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'  AND pay_billing_r_m.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_r_m.amount != '0'   and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2 group by pay_billing_r_m.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "'  and payment_Status != 1";
        }



        string unit_name = d.getsinglestring("SELECT  GROUP_CONCAT(DISTINCT unit_name) FROM pay_r_and_m_service inner join pay_unit_master on pay_unit_master.comp_code=pay_r_and_m_service.comp_code and pay_unit_master.client_code=pay_r_and_m_service.client_code and pay_unit_master.unit_code=pay_r_and_m_service.unit_code WHERE   pay_r_and_m_service.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_r_and_m_service.client_code = '" + ddl_client.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and approve_flag = 1 ");

        if (unit_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + "  not approve finance so you can not process payment');", true);
            return;
        }


        //sql = "SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, material_amount,material_deduction, 'Material', STATUS, '" + Session["LOGIN_ID"].ToString() + "', date, NI FROM (SELECT comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_code, emp_name, state_name, EMP_TYPE, grade_desc, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month_days, month, year, GRADE_CODE, material_amount,material_deduction, 'Material', STATUS, NI, date FROM (SELECT pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_employee_master.emp_code, pay_client_master.client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_employee_master.EMP_EMAIL_ID, pay_employee_master.Bank_holder_name, CASE WHEN left_date >= str_to_date('" + d.get_start_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y')  AND left_date <= str_to_date('" + d.get_end_date(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_month_year.Text) + "','%d/%m/%Y') THEN 'LEFT' ELSE 'YES' END AS 'STATUS', pay_employee_master.original_bank_account_no AS 'BANK_EMP_AC_CODE', pay_employee_master.PF_IFSC_CODE, (SELECT Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'COMP_AC_NO', (CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name', pay_grade_master.grade_desc, pay_billing_unit_rate.month_days, pay_attendance_muster.month, pay_attendance_muster.year, pay_employee_master.Employee_type AS 'EMP_TYPE', pay_company_master.STATE AS 'COMP_STATE', pay_grade_master.grade_code AS 'GRADE_CODE', material_amount,material_deduction, CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.GRADE_CODE INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code INNER JOIN pay_material_details ON pay_material_details.comp_code = pay_employee_master.comp_code AND pay_material_details.client_code = pay_employee_master.client_code AND pay_material_details.emp_code = pay_employee_master.emp_code AND pay_material_details.month = pay_billing_unit_rate.month AND pay_material_details.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master ON pay_billing_master.billing_unit_code = pay_employee_master.unit_code AND pay_billing_master.comp_code = pay_employee_master.comp_code AND pay_billing_master.designation = pay_employee_master.GRADE_CODE " + where;
        sql = "SELECT  comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name, Bank_holder_name, BANK_EMP_AC_CODE, IFSC_CODE, month, year, amount, date, NI FROM (SELECT  comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name, Bank_holder_name, BANK_EMP_AC_CODE, IFSC_CODE, month, year, amount, date,NI FROM (SELECT  pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_client_master.client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_billing_r_m.emp_name AS 'Bank_holder_name', bank_acc_no AS 'BANK_EMP_AC_CODE', ifsc_code, (SELECT  Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_billing_r_m.comp_code) AS 'COMP_AC_NO', pay_billing_r_m.emp_name, pay_billing_r_m.month, pay_billing_r_m.year, pay_company_master.STATE AS 'COMP_STATE', pay_billing_r_m.amount, CASE WHEN INSTR(pay_r_and_m_service.IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date' FROM pay_billing_r_m INNER JOIN pay_unit_master ON pay_billing_r_m.unit_code = pay_unit_master.unit_code AND pay_billing_r_m.comp_code = pay_unit_master.comp_code INNER JOIN pay_company_master ON pay_billing_r_m.comp_code = pay_company_master.comp_code INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code INNER JOIN pay_r_and_m_service ON pay_r_and_m_service.comp_code = pay_billing_r_m.comp_code AND pay_r_and_m_service.client_code = pay_billing_r_m.client_code AND pay_r_and_m_service.party_name = pay_billing_r_m.emp_name AND pay_r_and_m_service.month = pay_billing_r_m.month AND pay_r_and_m_service.year = pay_billing_r_m.year left JOIN pay_pro_r_m ON pay_pro_r_m.comp_code = pay_billing_r_m.comp_code AND pay_pro_r_m.client_code = pay_billing_r_m.client_code AND pay_pro_r_m.emp_name = pay_billing_r_m.emp_name AND pay_pro_r_m.month = pay_billing_r_m.month AND pay_pro_r_m.year = pay_billing_r_m.year" + where; ;
        d.operation("delete from pay_pro_r_m where " + delete_where + "");
        d.operation("INSERT INTO pay_pro_r_m (comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name,  Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month, year, amount,uploaded_date,NI)" + sql);
        // paypro_no(2);


    }
    protected void btn_rm_get_Click(object sender, EventArgs e)
    {
        get_r_m_excel(1);
    }
    protected void btn_rm_paid_Click(object sender, EventArgs e)
    {
        get_r_m_excel(2);
    }
    protected void get_r_m_excel(int i)
    {

        string where = "";

        string sql = null;
        d.con.Open();
        if (i == 1 || i == 2)
        {
            if (ddl_client.SelectedValue != "ALL")
            {
                where = " pay_pro_r_m.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_r_m.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_r_m.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_r_m.unit_code = '" + ddl_unitcode.SelectedValue + "'    ";
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {
                where = " pay_pro_r_m.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_r_m.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_r_m.comp_code = '" + Session["COMP_CODE"].ToString() + "'  ";
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {
                where = " pay_pro_r_m.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_r_m.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_r_m.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_r_m.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_r_m.state_name = '" + ddl_billing_state.SelectedValue + "'";
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = " pay_pro_r_m.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_r_m.Year = '" + txt_month_year.Text.Substring(3) + "'  and pay_pro_r_m.comp_code = '" + Session["COMP_CODE"].ToString() + "' ";
            }
            if (i == 1) { where = where + " and payment_Status in (0,2)"; }
            else if (i == 2) { where = where + " and payment_Status = 1"; }
            sql = "SELECT  pay_pro_r_m.client, pay_pro_r_m.state_name, pay_pro_r_m.unit_name, pay_pro_r_m.emp_name,'YES' as status, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, COMP_AC_NO, pay_pro_r_m.amount, NI, DATE_FORMAT(uploaded_date, '%d-%m-%Y') AS 'uploaded_date', invoice_no FROM pay_pro_r_m INNER JOIN pay_billing_r_m ON pay_pro_r_m.comp_code = pay_billing_r_m.comp_code AND pay_pro_r_m.client_code = pay_billing_r_m.client_code AND pay_pro_r_m.emp_name = pay_billing_r_m.emp_name AND pay_pro_r_m.month = pay_billing_r_m.month AND pay_pro_r_m.year = pay_billing_r_m.year WHERE  " + where + "  ORDER BY pay_pro_r_m.state_name , pay_pro_r_m.unit_name , pay_pro_r_m.emp_name";


        }
       else if (i == 3 || i == 4)
        {
            if (ddl_client.SelectedValue != "ALL")
            {
                where = " pay_pro_admini_expense.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_admini_expense.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_admini_expense.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_admini_expense.unit_code = '" + ddl_unitcode.SelectedValue + "'    ";
            }
            if (ddl_billing_state.SelectedValue == "ALL")
            {
                where = " pay_pro_admini_expense.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_admini_expense.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_admini_expense.comp_code = '" + Session["COMP_CODE"].ToString() + "'  ";
            }
            else if (ddl_unitcode.SelectedValue == "ALL")
            {
                where = " pay_pro_admini_expense.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_pro_admini_expense.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_admini_expense.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_pro_admini_expense.state_name = '" + ddl_billing_state.SelectedValue + "'";
            }
            if (ddl_client.SelectedValue == "ALL")
            {
                where = " pay_pro_admini_expense.month='" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "'  and pay_pro_admini_expense.comp_code = '" + Session["COMP_CODE"].ToString() + "' ";
            }
            if (i == 3) { where = where + " and payment_Status in (0,2)"; }
            else if (i == 4) { where = where + " and payment_Status = 1"; }
            sql = "SELECT  pay_pro_admini_expense.client, pay_pro_admini_expense.state_name, pay_pro_admini_expense.unit_name, pay_pro_admini_expense.emp_name,'YES' as  status, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, COMP_AC_NO, pay_pro_admini_expense.amount, NI, DATE_FORMAT(uploaded_date, '%d-%m-%Y') AS 'uploaded_date', invoice_no,pay_pro_admini_expense.days FROM pay_pro_admini_expense INNER JOIN pay_billing_admini_expense ON pay_billing_admini_expense.comp_code = pay_pro_admini_expense.comp_code AND pay_billing_admini_expense.client_code = pay_pro_admini_expense.client_code AND pay_billing_admini_expense.emp_name = pay_pro_admini_expense.emp_name AND pay_billing_admini_expense.month = pay_pro_admini_expense.month AND pay_billing_admini_expense.year = pay_pro_admini_expense.year WHERE  " + where + "  ORDER BY pay_pro_admini_expense.state_name , pay_pro_admini_expense.unit_name , pay_pro_admini_expense.emp_name";


        }
       
        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (i == 1 || i==3)
            {
                Response.AddHeader("content-disposition", "attachment;filename=R_AND_M_VENDOR_PAYMENT_SHEET.xls");
            }
            if (i == 2 || i==4)
            {
                Response.AddHeader("content-disposition", "attachment;filename=ADMINISTRATIVE_EXPENSE_VENDOR_PAYMENT_SHEET.xls");
            }

            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            string month_d = txt_month_year.Text.Substring(0, 2).ToString();
            string month_name = mfi.GetMonthName(int.Parse(month_d)).ToString();
            string year = txt_month_year.Text.Substring(3).ToString();

            string month_year = month_name.ToUpper() + " " + year;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate4(ListItemType.Header, ds, i, null, month_year, ddl_bank.SelectedValue);
            Repeater1.ItemTemplate = new MyTemplate4(ListItemType.Item, ds, i, ddl_con_type.SelectedValue, null, ddl_bank.SelectedValue);
            Repeater1.FooterTemplate = new MyTemplate4(ListItemType.Footer, null, i, null, null, ddl_bank.SelectedValue);
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
    public class MyTemplate4 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i;
        string con_type;
        string bank;
        string month_year;
        static int ctr;
        public MyTemplate4(ListItemType type, DataSet ds, int i, string con_type, string month_year, string bank)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.con_type = con_type;
            this.month_year = month_year;
            this.bank = bank;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:

                    if (i == 1 || i == 2)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=19 align=center>R&M PAYMENT FOR " + ds.Tables[0].Rows[ctr]["client"] + " " + month_year + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>PARTY NAME</th><th>AMOUNT</th><th>NI</th><th>AMOUNT</th><th>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th></tr>");
                    }
                    else if (i == 3 || i == 4)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=20 align=center>ADMINISTRATIVE EXPENSE PAYMENT FOR " + ds.Tables[0].Rows[ctr]["client"] + " " + month_year + "</th></tr><table border=1><tr><th>Sr. No.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>PARTY NAME</th><th>DAYS</th><th>AMOUNT</th><th>NI</th><th>AMOUNT</th><th>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th><th bgcolor=silver>INVOICE NO</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                    string color = "";

                    if (i == 1 || i == 2)
                    {

                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ds.Tables[0].Rows[ctr]["amount"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["uploaded_date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td></td><td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + " </td></tr>");
                    }
                    else if (i == 3 || i == 4)
                    {

                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["days"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["amount"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + ds.Tables[0].Rows[ctr]["amount"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["uploaded_date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["COMP_AC_NO"] + "</td></td><td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + " </td></tr>");
                    }


                    if (i == 1 || i == 2)
                    {
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=SUM(F3:F" + (ctr + 3) + ")</td><td></td><td>=SUM(H3:H" + (ctr + 3) + ")</td>";
                        }
                    }
                    else if (i == 3 || i == 4)
                    {
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=SUM(F3:F" + (ctr + 3) + ")</td><td>=SUM(G3:G" + (ctr + 3) + ")</td><td></td><td>=SUM(I3:I" + (ctr + 3) + ")</td>";
                        }
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
    protected void btn_admini_process_Click(object sender, EventArgs e)
    {
        hidtab.Value = "6";
        string sql = null, where = null, delete_where = null;


        where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "' AND pay_billing_admini_expense.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null  || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_admini_expense.amount != '0' and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2  group by pay_billing_admini_expense.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
        string check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and payment_Status != 1 ";

        if (ddl_billing_state.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  ";
            where = " WHERE  pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  AND pay_billing_admini_expense.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_admini_expense.amount != '0' and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2  group by pay_billing_admini_expense.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'  and payment_Status != 1";
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            check_finace_flag = " comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_billing_state.SelectedValue + "' ";
            where = " WHERE pay_company_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_billing_state.SelectedValue + "'  AND pay_billing_admini_expense.month = '" + txt_month_year.Text.Substring(0, 2) + "' AND pay_billing_admini_expense.Year = '" + txt_month_year.Text.Substring(3) + "' AND (branch_close_date is null || branch_close_date  >= STR_TO_DATE('01/" + txt_month_year.Text + "', '%d/%m/%Y'))  AND pay_billing_admini_expense.amount != '0' and (payment_Status is null || payment_Status != 1)  AND approve_flag = 2 group by pay_billing_admini_expense.emp_name ORDER BY 4, 3) AS salary_table) AS Final_salary";
            delete_where = "month='" + txt_month_year.Text.Substring(0, 2) + "' and Year = '" + txt_month_year.Text.Substring(3) + "' and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_billing_state.SelectedValue + "'  and payment_Status != 1";
        }

        string unit_name = d.getsinglestring("SELECT  GROUP_CONCAT(DISTINCT unit_name) FROM pay_administrative_expense inner join pay_unit_master on pay_unit_master.comp_code=pay_administrative_expense.comp_code and pay_unit_master.client_code=pay_administrative_expense.client_code and pay_unit_master.unit_code=pay_administrative_expense.unit_code WHERE   pay_administrative_expense.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_administrative_expense.client_code = '" + ddl_client.SelectedValue + "' and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and approve_flag = 1 ");

        if (unit_name != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch : " + unit_name + "  not approve finance so you can not process payment');", true);
            return;
        }


        sql = "SELECT  comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name, Bank_holder_name, BANK_EMP_AC_CODE, IFSC_CODE, month, year, amount, date, NI,days FROM (SELECT  comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name, Bank_holder_name, BANK_EMP_AC_CODE, IFSC_CODE, month, year, amount, date,NI,days FROM (SELECT  pay_unit_master.comp_code, pay_unit_master.client_code, pay_unit_master.unit_code, pay_client_master.client_name AS 'client', pay_company_master.state AS 'company_state', pay_unit_master.unit_name, pay_unit_master.state_name, pay_billing_admini_expense.emp_name AS 'Bank_holder_name', bank_acc_no AS 'BANK_EMP_AC_CODE', ifsc_code, (SELECT  Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_billing_admini_expense.comp_code) AS 'COMP_AC_NO', pay_billing_admini_expense.emp_name, pay_billing_admini_expense.month, pay_billing_admini_expense.year, pay_company_master.STATE AS 'COMP_STATE', pay_billing_admini_expense.amount, CASE WHEN INSTR(pay_administrative_expense.IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', NOW() AS 'date',pay_billing_admini_expense.days FROM pay_billing_admini_expense INNER JOIN pay_unit_master ON pay_billing_admini_expense.unit_code = pay_unit_master.unit_code AND pay_billing_admini_expense.comp_code = pay_unit_master.comp_code INNER JOIN pay_company_master ON pay_billing_admini_expense.comp_code = pay_company_master.comp_code INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code INNER JOIN pay_administrative_expense ON pay_administrative_expense.comp_code = pay_billing_admini_expense.comp_code AND pay_administrative_expense.client_code = pay_billing_admini_expense.client_code AND pay_administrative_expense.party_name = pay_billing_admini_expense.emp_name AND pay_administrative_expense.month = pay_billing_admini_expense.month AND pay_administrative_expense.year = pay_billing_admini_expense.year left JOIN pay_pro_admini_expense ON pay_pro_admini_expense.comp_code = pay_billing_admini_expense.comp_code AND pay_pro_admini_expense.client_code = pay_billing_admini_expense.client_code AND pay_pro_admini_expense.emp_name = pay_billing_admini_expense.emp_name AND pay_pro_admini_expense.month = pay_billing_admini_expense.month AND pay_pro_admini_expense.year = pay_billing_admini_expense.year " + where; ;
        d.operation("delete from pay_pro_admini_expense where " + delete_where + "");
        d.operation("INSERT INTO pay_pro_admini_expense (comp_code, COMP_AC_NO, client_code, client, unit_code, unit_name, emp_name, state_name,  Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, month, year, amount,uploaded_date,NI,days)" + sql);
       

    }
    protected void btn_admini_get_Click(object sender, EventArgs e)
    {
        get_r_m_excel(3);
    }
    protected void btn_admini_paid_Click(object sender, EventArgs e)
    {
        get_r_m_excel(4);
    }
    protected void gv_minibank_menu4(object sender, EventArgs e)
    {
        try
        {
            gv_lessEmployeeAttendances.UseAccessibleHeader = false;
            gv_lessEmployeeAttendances.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
}