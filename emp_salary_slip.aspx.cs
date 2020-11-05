using System;
using System.Data;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;

/// <summary>
/// Summary description for Payslip
/// </summary>
public partial class Payslip : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    //ReportDocument crystalReport = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Salary Slip", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Salary Slip", Session["COMP_CODE"].ToString()) == "R")
        {
            //btn_delete.Visible = false;
            //btn_edit.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Salary Slip", Session["COMP_CODE"].ToString()) == "U")
        {
            //btn_delete.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Salary Slip", Session["COMP_CODE"].ToString()) == "C")
        {
            //btn_delete.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }



        if (!IsPostBack)
        {
            txttoempcode.Visible = false;
            Label3.Visible = false;
            txtfrmempcode.Visible = false;
            Label2.Visible = false;
            //txtcurrentyr.Text = Session["system_curr_date"].ToString().Substring(6);
           // ddl_currmon.SelectedValue = Session["system_curr_date"].ToString().Substring(3,2);
        }
       


    }



    protected void btnclose_Click(object sender, EventArgs e)
    {
        //crystalReport.Dispose();
        //crystalReport.Close();
        ReportDocument crystalReport = new ReportDocument();
        //CrystalReportViewer1.ReportSource = null;
        //CrystalReportViewer1.RefreshReport();
        crystalReport.Dispose();
        crystalReport.Close();
        Response.Redirect("Home.aspx");
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        ReportDocument crystalReport = new ReportDocument();
        //CrystalReportViewer1.ReportSource = null;
        //CrystalReportViewer1.RefreshReport();
        string query = null;
        string now = DateTime.Today.ToString("MM");
       string cuur_date = "" + DateTime.Now.Day + "/" + now + "/" + DateTime.Now.Year + "";

       DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string month_name = mfi.GetMonthName(int.Parse(ddl_currmon.SelectedValue.ToString()));
      

      // string month_name = ddl_currmon.SelectedValue.ToString();

        string month_year = month_name + " " + txtcurrentyr.Text;
        crystalReport.Load(Server.MapPath("~/Salary_Slip_peremp.rpt"));

        string thisMonth = ddl_currmon.SelectedItem.Text;
        crystalReport.DataDefinition.FormulaFields["salary_monthyear"].Text = @"'" + month_year + "'";
    
        query = "SELECT comp_code, COMPANY_NAME, ADDRESS1, ADDRESS2, CITY, STATE, UnitState, Unit_City, Client, grade, Unitcode, ihms_code, Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, UAN_No, PF_No, PAN_No, ESI_No, PerDayRate, Basic, Vda, emp_basic_vda as 'basic_vda', hra_amount as 'hra', sal_bonus_gross as 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross as 'leaveDays', washing_salary as 'washing', travelling_salary as 'travelling', education_salary as 'education', allowances_salary as 'special_allo', cca_salary as 'cca', other_allow as 'other_allo', gratuity_gross as 'Gratuity_taxable', gratuity_after_gross as 'Gratuity', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'PF', (((emp_basic_vda + hra_amount + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_esic_percent) AS 'ESIC', sal_ot as 'ot_amount_salary', lwf_salary as 'lwf', sal_uniform_rate as 'Uniform', CASE WHEN `F_PT` = 'Y' THEN CASE WHEN (`emp_basic_vda` + `hra_amount` + `sal_bonus_gross` + `leave_sal_gross` + `washing_salary` + `travelling_salary` + `education_salary` + `allowances_salary` + `cca_salary` + `other_allow` + `gratuity_gross` + `sal_ot`) < 10001 THEN 0 END ELSE `PT` END AS 'pt', fine, EMP_ADVANCE_PAYMENT as 'advance', Total_Days_Present, Payment, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, Working_Days, Bonus_Policy FROM (SELECT pay_company_master.comp_code, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_unit_master.state_name AS 'UnitState', unit_city AS 'Unit_City', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'Client', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_unit_master.unit_code AS 'Unitcode', pay_employee_master.ihmscode AS 'ihms_code', pay_employee_master.emp_name AS 'Emp_Name', pay_employee_master.emp_code AS 'Emp_Code', pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'UAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_billing_master.per_rate_salary AS 'PerDayRate', pay_billing_master.basic_salary as 'Basic' , pay_billing_master.vda_salary as 'Vda', (((pay_billing_master.basic_salary + pay_billing_master.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra_amount', CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable_salary = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable_salary = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education_salary', ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END AS 'cca_salary', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_billing_master.sal_esic AS 'sal_esic_percent', pay_billing_master.sal_pf AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'sal_ot', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_billing_master.basic_salary + pay_billing_master.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + ddl_currmon.SelectedValue + "/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/" + ddl_currmon.SelectedValue + "/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/" + ddl_currmon.SelectedValue + "/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', CASE WHEN FLOOR(((pay_salary_unit_rate.total_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) IS NULL THEN 0 ELSE FLOOR(((pay_salary_unit_rate.total_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'Payment', pay_employee_master.Bank_holder_name, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.month_days AS 'Working_Days', pay_billing_master.bonus_policy_salary AS 'Bonus_Policy', pay_billing_master.bonus_taxable_salary AS 'Bonus_taxable', pay_billing_master.leave_taxable_salary AS 'leave_taxable', pay_billing_master.gratuity_taxable_salary AS 'Gratuity_taxable', pay_billing_master.gratuity_salary AS 'Gratuity_BM', pay_salary_unit_rate.gratuity_salary AS 'Gratuity', pay_salary_unit_rate.leave_days AS 'leaveDays', pay_billing_master.leave_days_percent AS 'Leave_Percent' FROM `pay_employee_master` INNER JOIN `pay_attendance_muster` ON `pay_attendance_muster`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_attendance_muster`.`comp_code` = `pay_employee_master`.`comp_code` INNER JOIN `pay_unit_master` ON `pay_attendance_muster`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_attendance_muster`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_salary_unit_rate` ON `pay_attendance_muster`.`unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_attendance_muster`.`month` = `pay_salary_unit_rate`.`month` AND `pay_attendance_muster`.`year` = `pay_salary_unit_rate`.`year` INNER JOIN `pay_billing_master` ON `pay_billing_master`.`comp_code` = `pay_salary_unit_rate`.`comp_code` AND `pay_billing_master`.`billing_client_code` = `pay_salary_unit_rate`.`client_code` AND `pay_billing_master`.`billing_unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_employee_master`.`grade_code` = `pay_billing_master`.`designation` AND `pay_billing_master`.`designation` = `pay_salary_unit_rate`.`designation` AND `pay_billing_master`.`hours` = `pay_salary_unit_rate`.`hours` INNER JOIN `pay_company_master` ON `pay_employee_master`.`comp_code` = `pay_company_master`.`comp_code`  WHERE pay_attendance_muster.month = '" + ddl_currmon.SelectedValue.ToString() + "' AND pay_attendance_muster.year = '" + txtcurrentyr.Text + "' AND pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_attendance_muster.emp_code = '" + Session["LOGIN_ID"].ToString() + "' AND pay_attendance_muster.tot_days_present > 0) AS payslip";
        DataTable dt = new DataTable();
        MySqlCommand cmd = new MySqlCommand(query);
        MySqlDataReader sda = null;
        cmd.Connection = d.con;
        d.con.Open();
        sda = cmd.ExecuteReader();
        dt.Load(sda);
        d.con.Close();
        crystalReport.Refresh();
        crystalReport.SetDataSource(dt);
        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Salary_" + ddl_currmon.SelectedItem.Text + "_" + txtcurrentyr.Text);
        ddl_currmon.SelectedValue = "Select Month";
    }

    protected void ddl_currmon_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnshow_Click(sender, e);
    }
   
    
}
