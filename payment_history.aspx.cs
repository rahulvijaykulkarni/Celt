using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
public partial class payment_history : System.Web.UI.Page
{
    DAL d = new DAL();
   // double rec_amount;
   // double balance_amount;
    double final_double_amount;
    protected int result = 0;
    protected double billing_amount=0,recived_amt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        gv_payment_history();
           
        if (!IsPostBack)
        {
            ddl_client_list();
        }
    }
    protected void ddl_client_list()
    {
        ddl_client.Items.Clear();
        DataTable dt_item = new DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', CLIENT_CODE from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')  ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_client.DataSource = dt_item;
                ddl_client_amount.DataSource = dt_item;

                ddl_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_amount.DataTextField = dt_item.Columns[0].ToString();
                ddl_client.DataValueField = dt_item.Columns[1].ToString();
               
                ddl_client_amount.DataValueField = dt_item.Columns[1].ToString();

                ddl_client.DataBind();
                ddl_client_amount.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();

            ddl_client.Items.Insert(0, "Select");
            ddl_client_amount.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            receving_amount.Visible = false;
            d.con.Close();
        }
    }
    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_payment, "Select$" + e.Row.RowIndex);

            //  e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "Select$" + e.Row.RowIndex);
            // e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "select$" + e.Row.RowIndex);

        }
    }


    protected void btn_add1(object sender, EventArgs e) 
    {

        final_billing();
        if (billing_amount == 0.0)
        {
            txt_date.Text = "";
            ddl_client.SelectedValue = "Select";
            ddl_state.Items.Clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('First Complete Billing Process !!!');", true);
           
        }
        else
        {
           

            try
            {
                double recived_amt = 0;
                double balance_amt = 0;

            result = d.operation("INSERT INTO payment_history(CLIENT_CODE,client_name,state,billing_amt,month_year,recived_amt,balance_amt,comp_code) VALUES('" + ddl_client.SelectedItem.Value + "','" + ddl_client.SelectedItem.Text + "','" + ddl_state.SelectedItem.Value + "','" + billing_amount + "','" + txt_date.Text + "','" + recived_amt + "','" + billing_amount + "','" + Session["comp_code"].ToString() + "')");
            
            if (result > 0)
            {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Save Succsefully !!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment NOT save !!!');", true);
                }

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                gv_payment_history();
                d.con.Close();
            }
        }
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and  state_name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "') order by 1", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_state.DataSource = dt_item;
                    ddl_state_amount.DataSource = dt_item;
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
      
    }

    protected void ddl_client_amount_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_amount.SelectedValue + "' order by 1", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_state_amount.DataSource = dt_item;
                    ddl_state_amount.DataTextField = dt_item.Columns[0].ToString();
                    ddl_state_amount.DataValueField = dt_item.Columns[0].ToString();
                    ddl_state_amount.DataBind();

                }
                dt_item.Dispose();
                d.con.Close();
                ddl_state_amount.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        

    }

    protected void btn_close_click(object sender, object e)
    { 
     Response.Redirect("Home.aspx");
    }
   
   
    
    protected void final_billing()
    {

        if (ddl_state.SelectedValue.ToString() == "ALL")
        {

            d.con.Open();
           
                String sql = "SELECT sum(Total + pf + esic) AS 'Amount', sum(((Total + pf + esic + (ot_rate * ot_hours)) * bill_service_charge) / 100) AS 'Service_charge', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform + (ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'CGST9', sum(CASE WHEN company_state != state_name THEN ROUND(((((Total + pf + esic+ operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 18) / 100), 2) ELSE 0 END) AS 'IGST18', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'SGST9',sum(uniform),sum(operational_cost), bill_service_charge, NH, hours,case when emp_cca = 0 then (sub_total_c -ot_rate) else (bill_gross + ((bill_gross * esic_percent) / 100) + bill_pf + bill_uniform ) end AS 'sub_total_c',ot_rate,ot_hours,(ot_rate * ot_hours) as 'ot_amount' FROM (SELECT client, company_state,unit_name,state_name, unit_city, client_branch_code, emp_name, grade_desc, emp_basic_vda, hra, bonus_gross, leave_gross, gratuity_gross, washing, travelling, education, allowances, cca_billing, other_allow, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) AS 'gross', bonus_after_gross, leave_after_gross, gratuity_after_gross, (((emp_basic_vda) / 100) * pf_percent) AS 'pf', (((emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) / 100) * esic_percent) AS 'esic', hrs_12_ot, (((hrs_12_ot) * esic_percent) / 100) AS 'esic_ot', lwf, CASE WHEN bill_ser_uniform = 1 THEN 0 ELSE uniform END AS 'uniform',relieving_charg, CASE WHEN bill_ser_operations = 1 THEN 0 ELSE operational_cost END AS 'operational_cost', tot_days_present, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross + bonus_after_gross + leave_after_gross + gratuity_after_gross + lwf + CASE WHEN bill_ser_uniform = 0 THEN 0 ELSE uniform END + relieving_charg + CASE WHEN bill_ser_operations = 0 THEN 0 ELSE operational_cost END + NH+ hrs_12_ot) AS 'Total', bill_service_charge, NH,hours,(bill_gross + emp_cca ) as 'bill_gross',sub_total_c,bill_ser_uniform,bill_ser_operations,(ot_rate+esi_on_ot_amount) as 'ot_rate',ot_hours,esic_amount,esi_on_ot_amount,emp_cca,bill_pf,bill_uniform,esic_percent FROM (SELECT (SELECT client_name FROM pay_client_master WHERE client_code = pay_unit_master.client_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'client', pay_company_master.state as 'company_state',pay_unit_master.unit_name, pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.client_branch_code, pay_employee_master.emp_name, pay_grade_master.grade_desc, (((pay_billing_master_history.basic + pay_billing_master_history.vda) / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_billing_unit_rate.hra / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra', CASE WHEN bonus_taxable = '1' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_gross', CASE WHEN leave_taxable = '0' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_after_gross', CASE WHEN gratuity_taxable = '1' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_gross', CASE WHEN gratuity_taxable = '0' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_after_gross', ((pay_billing_unit_rate.washing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing', ((pay_billing_unit_rate.traveling / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling', ((pay_billing_unit_rate.education / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education', ((pay_billing_unit_rate.national_holiday_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'NH', ((pay_billing_unit_rate.allowances / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_billing_unit_rate.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE ((pay_employee_master.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'cca_billing', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_billing_master_history.ot_policy_billing ='1' THEN ((pay_billing_master_history.ot_amount_billing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'hrs_12_ot', pay_billing_master_history.bill_esic_percent AS 'esic_percent', pay_billing_master_history.bill_pf_percent AS 'pf_percent', ((pay_billing_unit_rate.lwf / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'lwf', ((pay_billing_unit_rate.uniform / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'uniform', ((pay_billing_unit_rate.relieving_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'relieving_charg', ((pay_billing_unit_rate.operational_cost / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'operational_cost', pay_attendance_muster.tot_days_present, ROUND(((pay_billing_unit_rate.sub_total_c / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present), 2) AS 'baseamount', bill_service_charge,	pay_billing_master_history.hours,pay_billing_unit_rate.sub_total_c,	pay_billing_master_history.bill_ser_operations,	pay_billing_master_history.bill_ser_uniform,pay_billing_unit_rate.ot_1_hr_amount as 'ot_rate' ,pay_attendance_muster.ot_hours, pay_billing_unit_rate.esic_amount,pay_billing_unit_rate.esi_on_ot_amount,pay_employee_master.cca as 'emp_cca',      pay_billing_unit_rate.gross as 'bill_gross',pay_billing_unit_rate.pf_amount as 'bill_pf',pay_billing_unit_rate.uniform as 'bill_uniform'  FROM pay_employee_master INNER JOIN  pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND  pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE WHERE pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_attendance_muster.month = '" + txt_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0) AS billing_table) as Final_billing";

                MySqlCommand cmd_item = new MySqlCommand(sql, d.con);
                MySqlDataReader dr = cmd_item.ExecuteReader();


                if (dr.Read())
                {
                   string amount=(dr.GetValue(0).ToString());
                    string Service_charge = ((dr.GetValue(1).ToString()));
                    string CGST9 = ((dr.GetValue(2).ToString()));
                    string IGST18 = ((dr.GetValue(3).ToString()));
                    string SGST8 = ((dr.GetValue(4).ToString()));
                    string baseamount = ((dr.GetValue(5).ToString()));
                    string emp_cca = ((dr.GetValue(6).ToString()));

                    if (amount == "") { amount = "0"; }
                    if (Service_charge == "") { Service_charge = "0"; }
                    if (CGST9 == "") { CGST9 = "0"; }
                    if (IGST18 == "") { IGST18 = "0"; }
                    if (SGST8 == "") { SGST8 = "0"; }
                    if (baseamount == "") { baseamount = "0"; }
                    if (emp_cca == "") { emp_cca = "0"; }

                    billing_amount = (double.Parse(amount) + double.Parse(Service_charge) + double.Parse(CGST9) + double.Parse(IGST18) + double.Parse(SGST8) + double.Parse(baseamount) + double.Parse(emp_cca));

                }
                dr.Close();
                d.con.Close();
            }
        else
        {
            d.con.Open();
           
                String sql = "SELECT sum(Total + pf + esic) AS 'Amount', sum(((Total + pf + esic + (ot_rate * ot_hours)) * bill_service_charge) / 100) AS 'Service_charge', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform + (ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'CGST9', sum(CASE WHEN company_state != state_name THEN ROUND(((((Total + pf + esic+ operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 18) / 100), 2) ELSE 0 END) AS 'IGST18', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'SGST9',sum(uniform),sum(operational_cost), bill_service_charge, NH, hours,case when emp_cca = 0 then (sub_total_c -ot_rate) else (bill_gross + ((bill_gross * esic_percent) / 100) + bill_pf + bill_uniform ) end AS 'sub_total_c',ot_rate,ot_hours,(ot_rate * ot_hours) as 'ot_amount' FROM (SELECT client, company_state,unit_name,state_name, unit_city, client_branch_code, emp_name, grade_desc, emp_basic_vda, hra, bonus_gross, leave_gross, gratuity_gross, washing, travelling, education, allowances, cca_billing, other_allow, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) AS 'gross', bonus_after_gross, leave_after_gross, gratuity_after_gross, (((emp_basic_vda) / 100) * pf_percent) AS 'pf', (((emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) / 100) * esic_percent) AS 'esic', hrs_12_ot, (((hrs_12_ot) * esic_percent) / 100) AS 'esic_ot', lwf, CASE WHEN bill_ser_uniform = 1 THEN 0 ELSE uniform END AS 'uniform',relieving_charg, CASE WHEN bill_ser_operations = 1 THEN 0 ELSE operational_cost END AS 'operational_cost', tot_days_present, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross + bonus_after_gross + leave_after_gross + gratuity_after_gross + lwf + CASE WHEN bill_ser_uniform = 0 THEN 0 ELSE uniform END + relieving_charg + CASE WHEN bill_ser_operations = 0 THEN 0 ELSE operational_cost END + NH+ hrs_12_ot) AS 'Total', bill_service_charge, NH,hours,(bill_gross + emp_cca ) as 'bill_gross',sub_total_c,bill_ser_uniform,bill_ser_operations,(ot_rate+esi_on_ot_amount) as 'ot_rate',ot_hours,esic_amount,esi_on_ot_amount,emp_cca,bill_pf,bill_uniform,esic_percent FROM (SELECT (SELECT client_name FROM pay_client_master WHERE client_code = pay_unit_master.client_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'client', pay_company_master.state as 'company_state',pay_unit_master.unit_name, pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.client_branch_code, pay_employee_master.emp_name, pay_grade_master.grade_desc, (((pay_billing_master_history.basic + pay_billing_master_history.vda) / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_billing_unit_rate.hra / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra', CASE WHEN bonus_taxable = '1' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_gross', CASE WHEN leave_taxable = '0' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_after_gross', CASE WHEN gratuity_taxable = '1' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_gross', CASE WHEN gratuity_taxable = '0' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_after_gross', ((pay_billing_unit_rate.washing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing', ((pay_billing_unit_rate.traveling / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling', ((pay_billing_unit_rate.education / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education', ((pay_billing_unit_rate.national_holiday_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'NH', ((pay_billing_unit_rate.allowances / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_billing_unit_rate.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE ((pay_employee_master.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'cca_billing', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_billing_master_history.ot_policy_billing ='1' THEN ((pay_billing_master_history.ot_amount_billing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'hrs_12_ot', pay_billing_master_history.bill_esic_percent AS 'esic_percent', pay_billing_master_history.bill_pf_percent AS 'pf_percent', ((pay_billing_unit_rate.lwf / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'lwf', ((pay_billing_unit_rate.uniform / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'uniform', ((pay_billing_unit_rate.relieving_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'relieving_charg', ((pay_billing_unit_rate.operational_cost / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'operational_cost', pay_attendance_muster.tot_days_present, ROUND(((pay_billing_unit_rate.sub_total_c / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present), 2) AS 'baseamount', bill_service_charge,	pay_billing_master_history.hours,pay_billing_unit_rate.sub_total_c,	pay_billing_master_history.bill_ser_operations,	pay_billing_master_history.bill_ser_uniform,pay_billing_unit_rate.ot_1_hr_amount as 'ot_rate' ,pay_attendance_muster.ot_hours, pay_billing_unit_rate.esic_amount,pay_billing_unit_rate.esi_on_ot_amount,pay_employee_master.cca as 'emp_cca',      pay_billing_unit_rate.gross as 'bill_gross',pay_billing_unit_rate.pf_amount as 'bill_pf',pay_billing_unit_rate.uniform as 'bill_uniform'  FROM pay_employee_master INNER JOIN  pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND  pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE WHERE pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and pay_attendance_muster.month = '" + txt_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0) AS billing_table) as Final_billing";

                MySqlCommand cmd_item = new MySqlCommand(sql, d.con);
                MySqlDataReader dr = cmd_item.ExecuteReader();


                if (dr.Read())
                {
                   
                    string amount=(dr.GetValue(0).ToString());
                    string Service_charge = ((dr.GetValue(1).ToString()));
                    string CGST9 = ((dr.GetValue(2).ToString()));
                    string IGST18 = ((dr.GetValue(3).ToString()));
                    string SGST8 = ((dr.GetValue(4).ToString()));
                    string baseamount = ((dr.GetValue(5).ToString()));
                    string emp_cca = ((dr.GetValue(6).ToString()));

                    if (amount == "") { amount = "0"; }
                    if (Service_charge == "") { Service_charge = "0"; }
                    if (CGST9 == "") { CGST9 = "0"; }
                    if (IGST18 == "") { IGST18 = "0"; }
                    if (SGST8 == "") { SGST8 = "0"; }
                    if (baseamount == "") { baseamount = "0"; }
                    if (emp_cca == "") { emp_cca = "0"; }

                    billing_amount = (double.Parse(amount) + double.Parse(Service_charge) + double.Parse(CGST9) + double.Parse(IGST18) + double.Parse(SGST8) + double.Parse(baseamount) + double.Parse(emp_cca));
                   // billing_amount = Math.Round(((double.Parse(dr.GetValue(0).ToString())) + (double.Parse(dr.GetValue(1).ToString())) + (double.Parse(dr.GetValue(2).ToString())) + (double.Parse(dr.GetValue(3).ToString())) + (double.Parse(dr.GetValue(4).ToString())) + (double.Parse(dr.GetValue(5).ToString())) + (double.Parse(dr.GetValue(6).ToString()))), 2);
                    
                
                }
                dr.Close();
                d.con.Close();
        }
    
    }
    protected void gv_payment_history() {

        DataTable dt_item = new DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  ID,client_name,state_name,billing_amt,concat(month,'_',year) as 'month_year',recived_amt,balance_amt   from payment_history  ", d.con);
        try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    gv_payment.DataSource = dt_item;
                    gv_payment.DataBind();
                }
                dt_item.Dispose();

                d.con.Close();
               
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }

    protected void lnk_remove_head_Click(object sender, EventArgs e)
    {

        System.Web.UI.WebControls.Label lbl_sr_CODE = (System.Web.UI.WebControls.Label)gv_payment.SelectedRow.FindControl("lbl_GRADE_CODE");
        string lbl_sr = lbl_sr_CODE.Text;

        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            DataTable dt = (DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_payment_detail.DataSource = dt;
            gv_payment_detail.DataBind();
        }


        //double rec_amount = Convert.ToDouble(txt_receive_amount.Text);
        //double balance_amount = Convert.ToDouble(txt_bal_amount.Text);

        //final_double_amount = balance_amount + rec_amount;
        //txt_bal_amount.Text = Convert.ToString(final_double_amount);

        //Session["bal_amount"] = final_double_amount;
        //foreach (GridViewRow row1 in gv_payment_detail.Rows)
        //{
        //    int res = d.operation("update payment_history SET recived_amt='" + row1.Cells[5].Text + "', balance_amt='" + row1.Cells[7].Text + "' where comp_code='" + Session["comp_code"].ToString() + "' and sr_no='" + lbl_sr + "' ");
        //}
    }
    protected void lnk_zone_add_Click(object sender, EventArgs e)
    {

        gv_payment_detail.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("Sr_No");
        dt.Columns.Add("Client_name");
        dt.Columns.Add("State");
        dt.Columns.Add("Billing_amount");
        dt.Columns.Add("Reciving_amount");
        dt.Columns.Add("Recived_date");
        dt.Columns.Add("Balance_amount");
        int rownum = 0;
        for (rownum = 0; rownum < gv_payment_detail.Rows.Count; rownum++)
        {
            if (gv_payment_detail.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["Sr_No"] = gv_payment_detail.Rows[rownum].Cells[1].Text;
                dr["Client_name"] = gv_payment_detail.Rows[rownum].Cells[2].Text;
                dr["State"] = gv_payment_detail.Rows[rownum].Cells[3].Text;
                dr["Billing_amount"] = gv_payment_detail.Rows[rownum].Cells[4].Text;
                dr["Reciving_amount"] = gv_payment_detail.Rows[rownum].Cells[5].Text;
                dr["Recived_date"] = gv_payment_detail.Rows[rownum].Cells[6].Text;
                dr["Balance_amount"] = gv_payment_detail.Rows[rownum].Cells[7].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["Sr_No"] = rownum + 1;
        dr["Client_name"] = ddl_client_amount.SelectedItem.Text;
        dr["State"] = ddl_state_amount.SelectedValue;

        dr["Billing_amount"] = txt_bill_amount.Text;

        double receving_amt = Convert.ToDouble(Session["recived_amt"].ToString());
        double txt_received = double.Parse(txt_receive_amount.Text) + receving_amt;

        dr["Reciving_amount"] = txt_received;
        dr["Recived_date"] = txt_receive_date.Text;


        dr["Balance_amount"] = Session["bal_amount"].ToString();
        dt.Rows.Add(dr);
        gv_payment_detail.DataSource = dt;
        gv_payment_detail.DataBind();

        ViewState["headtable"] = dt;

        panel_payment_detail.Visible = true;

        ddl_client_amount.Items.Clear();
        ddl_state_amount.Items.Clear();
        txt_bill_amount.Text = "0";
        txt_receive_date.Text = "";
        btn_recived_amt.Text = "0";
        txt_bal_amount.Text = "0";
        txt_receive_amount.Text = "0";
    }

    protected void final_balance_amount(object sender,EventArgs e)
    {
        double rec_amount = Convert.ToDouble(txt_receive_amount.Text);
        double balance_amount = Convert.ToDouble(txt_bal_amount.Text);

         final_double_amount = balance_amount - rec_amount;
        txt_bal_amount.Text= Convert.ToString(final_double_amount);

        Session["bal_amount"] = final_double_amount;
    }

    protected void onSelected_IndexChange(object sender, EventArgs e)
    {
       
        receving_amount.Visible = true;

        Label lbl_GRADE_CODE = (Label)gv_payment.SelectedRow.FindControl("lbl_GRADE_CODE");
        string l_GRADE_CODE = lbl_GRADE_CODE.Text;

        d.con1.Open();
        try
        {

            MySqlCommand cmd = new MySqlCommand("SELECT client_name,state,billing_amt,recived_amt,balance_amt from payment_history where sr_no = '" + l_GRADE_CODE + "'  ", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ddl_client_list();
                ddl_client_amount.SelectedItem.Text = dr.GetValue(0).ToString();
                ddl_client_amount_SelectedIndexChanged(null, null);
                ddl_state_amount.SelectedItem.Text = dr.GetValue(1).ToString();
               
                //ddl_client_amount.SelectedValue = dr.GetValue(0).ToString();

                //ddl_client_SelectedIndexChanged(null, null);
                //ddl_state.SelectedValue = dr.GetValue(1).ToString();
                txt_bill_amount.Text = dr.GetValue(2).ToString();
                Session["recived_amt"] = double.Parse(dr.GetValue(3).ToString());

                txt_bal_amount.Text = (dr.GetValue(4).ToString()); 

             }

           }
       catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Dispose();
            d.con.Close();
            receving_amount.Visible = true;
        
        
        //    btn_submit.Visible = true;
        //    btn_Update.Visible = true;
        }


    }

    protected void add_payment_details(object sender, EventArgs e)
    {
        int result = 0;
        int res = 0;
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        System.Web.UI.WebControls.Label lbl_sr_CODE = (System.Web.UI.WebControls.Label)gv_payment.SelectedRow.FindControl("lbl_GRADE_CODE");
        string lbl_sr = lbl_sr_CODE.Text;
        foreach (GridViewRow row in gv_payment_detail.Rows)
        {
          result=  d.operation("INSERT INTO  payment_history_details (comp_code,client_name,state,billing_amt,month_year,recived_amt,balance_amt)VALUES('" + Session["COMP_CODE"].ToString() + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[6].Text + "','" + row.Cells[5].Text + "','" + row.Cells[7].Text + "')");
          res = d.operation("update payment_history SET recived_amt='" + row.Cells[5].Text + "', balance_amt='" + row.Cells[7].Text + "' where comp_code='" + Session["comp_code"].ToString() + "' and sr_no='" + lbl_sr + "' ");
        }
        if (res > 0 && result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Save successfully!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Save failed!!');", true);
        }
        gv_payment_history();

        gv_payment_detail.DataSource = null;
        gv_payment_detail.DataBind();

        receving_amount.Visible = false;
    }

    protected void gv_payment_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_payment.UseAccessibleHeader = false;
            gv_payment.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}
