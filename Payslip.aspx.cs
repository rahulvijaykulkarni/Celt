using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Globalization;

/// <summary>
/// Summary description for Payslip
/// </summary>
public partial class Payslip : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    //ReportDocument crystalReport = new ReportDocument();
    //public string rem_emp_count = "0";//vikas add 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "For Company Salary Slips", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "For Company Salary Slips", Session["COMP_CODE"].ToString()) == "R")
        {
            //btn_delete.Visible = false;
            //btn_edit.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "For Company Salary Slips", Session["COMP_CODE"].ToString()) == "U")
        {
            //btn_delete.Visible = false;
            //btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "For Company Salary Slips", Session["COMP_CODE"].ToString()) == "C")
        {
            //btn_delete.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }

        //ViewState["rem_emp_count"] = 0;
        //rem_emp_count = ViewState["rem_emp_count"].ToString();

        if (!IsPostBack)
        {


            //d1.con1.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT CONCAT(UNIT_CODE,'-',UNIT_NAME,'-',REPORT_CATEGORY) FROM pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' ORDER BY UNIT_CODE", d1.con1);
            //MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    ddlunitselect.Items.Add(dr[0].ToString());//ddl_banklist0.Items.Add(dr_banks[0].ToString());
            //}
            //ddlunitselect.Items.Insert(0, "ALL");
            //d1.con1.Close();
            txtcurrentyr.Text = Session["system_curr_date"].ToString().Substring(6);
            ddl_currmon.SelectedValue = Session["system_curr_date"].ToString().Substring(3, 2);

            client_code();
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        ReportDocument crystalReport = new ReportDocument();

        string query = null;
        string now = DateTime.Today.ToString("MM");
        string cuur_date = "" + DateTime.Now.Day + "/" + now + "/" + DateTime.Now.Year + "";

        int length = ddlunitselect.SelectedValue.Length;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string month_name = mfi.GetMonthName(int.Parse(select_payslip_date.Text.Substring(0, 2))).ToString();

        string month_year = month_name + " " + select_payslip_date.Text.Substring(3);


        string thisMonth = ddl_currmon.SelectedItem.Text;
       
        //if (Session["COMP_CODE"].ToString().Equals("C02"))
        //{
        //    crystalReport.Load(Server.MapPath("~/Salary_Slip_C02.rpt"));
        //}
        //else
        //{
        //    crystalReport.Load(Server.MapPath("~/Salary_Slip.rpt"));
        //}

        string path1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
                crystalReport.Load(Server.MapPath("~/Salary_Slip.rpt"));
		       

       //string where = " where pay_pro_master.month = '" + select_payslip_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + select_payslip_date.Text.Substring(3) + "' AND  pay_pro_master.client_code = '" + ddl_client.SelectedValue + "'  and pay_pro_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_pro_master.Total_Days_Present > 0  AND PAN_No != '' AND pf_no != ''  AND ESI_No != '' order by pay_pro_master.state_name,pay_pro_master.unit_code,pay_pro_master.emp_code";
       // if (ddlunitselect.Text == "ALL")
       // {
       //     where = "where  pay_pro_master.month ='" + select_payslip_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + select_payslip_date.Text.Substring(3) + "' AND pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.state_name = '" + ddl_state_name.SelectedValue + "'  and pay_pro_master.comp_code= '" + Session["comp_code"].ToString() + "' and pay_pro_master.Total_Days_Present > 0  AND PAN_No != '' AND pf_no != ''  AND ESI_No != '' order by pay_pro_master.state_name,pay_pro_master.unit_code,pay_pro_master.emp_code";
       // }

                string where = " where pay_pro_master.month = '" + select_payslip_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + select_payslip_date.Text.Substring(3) + "' AND  pay_pro_master.client_code = '" + ddl_client.SelectedValue + "'  and pay_pro_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND payment_status= '1' and employee_type IN ('Temporary', 'Permanent') order by pay_pro_master.state_name,pay_pro_master.unit_code,pay_pro_master.emp_code ";
        if (ddlunitselect.Text == "ALL")
        {
            where = "where  pay_pro_master.month ='" + select_payslip_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + select_payslip_date.Text.Substring(3) + "' AND pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' and pay_pro_master.state_name = '" + ddl_state_name.SelectedValue + "'  and pay_pro_master.comp_code= '" + Session["comp_code"].ToString() + "' and  payment_status= '1' and employee_type IN ('Temporary', 'Permanent')  order by pay_pro_master.state_name,pay_pro_master.unit_code,pay_pro_master.emp_code";
        }

        string ot_applicable = d.getsinglestring("SELECT comp_logo from pay_company_master where comp_code = '" + Session["COMP_CODE"].ToString() + "'");
        string companyimagepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\" + ot_applicable);

       // query = "SELECT pay_pro_master.comp_code, COMPANY_NAME, COMP_ADDRESS1 as 'ADDRESS1', COMP_ADDRESS2 As 'ADDRESS2', COMP_CITY AS  'CITY', COMP_STATE as 'STATE', state_name as 'UnitState', unit_city as 'Unit_City', client as 'Client', grade, unit_code as 'Unitcode', ihms as 'ihms_code', Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, PAN_No as 'UAN_No', PF_No,EMP_NEW_PAN_No as  'PAN_No', ESI_No, PerDayRate, Basic, Vda, emp_basic_vda AS 'basic_vda', hra_amount_salary AS 'hra', sal_bonus_gross AS 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross AS 'leaveDays', washing_salary AS 'washing', travelling_salary AS 'travelling', education_salary AS 'education', allowances_salary AS 'special_allo', cca_salary AS 'cca', other_allow AS 'other_allo', gratuity_gross AS 'Gratuity_taxable', gratuity_after_gross AS 'Gratuity', sal_pf AS 'PF', sal_esic AS 'ESIC', sal_ot AS 'ot_amount_salary', lwf_salary AS 'lwf', sal_uniform_rate AS 'Uniform', PT_AMOUNT AS 'pt', fine, advance_payment_mode AS 'advance', Total_Days_Present, Payment, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, Total_Days_Present as 'Working_Days', Bonus_Policy, ot_rate , esic_ot_applicable AS 'ESIC_OT', ot_hours, common_allow AS 'EMP_specialallow' FROM pay_pro_master " + where;

        query = "SELECT pay_pro_master.comp_code, COMPANY_NAME, COMP_ADDRESS1 as 'ADDRESS1', COMP_ADDRESS2 As 'ADDRESS2', COMP_CITY AS  'CITY', COMP_STATE as 'STATE', state_name as 'UnitState', unit_city as 'Unit_City', client as 'Client', grade, unit_code as 'Unitcode', ihms as 'ihms_code', Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, if(PAN_No is null or PAN_No='','IN PROCESS',PAN_No) AS 'UAN_No', if(PF_No is null or PF_No='','IN PROCESS',PF_No) AS PF_No,date_format(salary_date,'%d/%m/%Y') as  'PAN_No', if(ESI_No is null or ESI_No='','IN PROCESS',PAN_No) AS ESI_No, PerDayRate, Basic, Vda, emp_basic_vda AS 'basic_vda', hra_amount_salary AS 'hra', sal_bonus_gross AS 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross AS 'leaveDays', washing_salary AS 'washing', travelling_salary AS 'travelling', education_salary AS 'education', allowances_salary AS 'special_allo', cca_salary AS 'cca', other_allow AS 'other_allo', gratuity_gross AS 'Gratuity_taxable', gratuity_after_gross AS 'Gratuity', sal_pf AS 'PF', sal_esic AS 'ESIC', sal_ot AS 'ot_amount_salary', lwf_salary AS 'lwf', sal_uniform_rate AS 'Uniform', PT_AMOUNT AS 'pt', fine, advance_payment_mode AS 'advance', Total_Days_Present, Payment, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, month_days as 'Working_Days', Bonus_Policy, ot_rate , esic_ot_applicable AS 'ESIC_OT', ot_hours, common_allow AS 'EMP_specialallow'  FROM pay_pro_master " + where;
        // }

        DataTable dt = new DataTable();
        MySqlCommand cmd = new MySqlCommand(query);
        MySqlDataReader sda = null;
        cmd.Connection = d.con;
        d.con.Open();
        sda = cmd.ExecuteReader();
        dt.Load(sda);

        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Can not proceed !!');", true);
            return;
        }
        d.con.Close();
      //  employee_status();//vikas 03/05/2019
        //MySqlCommand cmd_item1 = new MySqlCommand("SELECT COMP_LOGO from pay_company_master where comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
        //d.con.Open();
        //MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
        //if (dr_item1.Read())
        //{
        //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\" + dr_item1.GetValue(0).ToString());
        //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
        //    PictureObject TAddress1 = (PictureObject)crystalReport.ReportDefinition.Sections[0].ReportObjects["Picture1"];
        //    TAddress1.Width = 550;
        //    TAddress1.Height = 220;
        //}
        //else
        //{
        //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\logo.png");
        //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
        //}
        //dr_item1.Close();
        //d.con.Close();
        crystalReport.DataDefinition.FormulaFields["company_image_path"].Text = @"'" + companyimagepath + "'";
        crystalReport.DataDefinition.FormulaFields["salary_monthyear"].Text = @"'" + month_year + "'";
        if (Session["COMP_CODE"].ToString().Equals("C01"))
        { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path1 + "C01_stamp.jpg" + "'"; }
        else
        { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path1 + "C02_stamp.png" + "'"; }

        crystalReport.Refresh();
        crystalReport.SetDataSource(dt);
        //CrystalReportViewer1.ReportSource = crystalReport;
        //CrystalReportViewer1.DataBind();
        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Salary_" + month_name + "_" + txtcurrentyr.Text);
        ddl_currmon.SelectedValue = "Select Month";
        crystalReport.Close();
        crystalReport.Clone();
        crystalReport.Dispose();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    //protected void ddl_currmon_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //btnshow_Click(sender, e);
    //}
    protected void btn_bank_Click(object sender, EventArgs e)
    {
        d1.con1.Open();
        //SqlCommand cmd = new SqlCommand(); 

        string query = "";
        if (ddlunitselect.Text == "ALL")
        {
            query = "SELECT '" + ddl_currmon.SelectedItem.ToString() + " " + txtcurrentyr.Text + " Salary' as CUS_REF, pay_employee_master.EMP_NAME, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, '10' as ACCT_TYPE, ((pay_attendance.L_HEAD01+pay_attendance.L_HEAD02+pay_attendance.L_HEAD03+pay_attendance.L_HEAD04+pay_attendance.L_HEAD05+pay_attendance.C_HEAD01+pay_attendance.C_HEAD02+pay_attendance.C_HEAD03+pay_attendance.C_HEAD04+pay_attendance.C_HEAD05+pay_attendance.C_HEAD06+pay_attendance.C_HEAD07+pay_attendance.C_HEAD08+pay_attendance.C_HEAD09+pay_attendance.C_HEAD10+pay_attendance.C_HEAD11+pay_attendance.C_HEAD12+pay_attendance.C_HEAD13+pay_attendance.C_HEAD14+pay_attendance.C_HEAD15+pay_attendance.OT_GROSS) - (pay_attendance.D_HEAD01+pay_attendance.D_HEAD02+pay_attendance.D_HEAD03+pay_attendance.D_HEAD04+pay_attendance.D_HEAD05+pay_attendance.PF+pay_attendance.ESIC_TOT+pay_attendance.PTAX+pay_attendance.D_HEAD06+pay_attendance.D_HEAD07+pay_attendance.D_HEAD08+pay_attendance.D_HEAD09 +pay_attendance.D_LOAN)) As 'Net_Salary', Date_format(now(),'%Y%m%d') as VALUE_DATE FROM pay_attendance INNER JOIN pay_employee_master ON pay_attendance.comp_code = pay_employee_master.comp_code AND pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE INNER JOIN pay_company_master ON pay_attendance.comp_code = pay_company_master.comp_code INNER JOIN pay_unit_master ON pay_attendance.comp_code = pay_unit_master.comp_code AND pay_attendance.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN pay_grade_master ON pay_attendance.CGRADE_CODE = pay_grade_master.GRADE_CODE AND pay_attendance.comp_code = pay_grade_master.comp_code  WHERE pay_attendance.PRESENT_DAYS>0 AND pay_company_master.comp_code='" + Session["comp_code"].ToString() + "'  AND  pay_attendance.MONTH = '" + ddl_currmon.SelectedValue.ToString() + "' AND pay_attendance.YEAR = '" + txtcurrentyr.Text + "' ";
        }
        else
        {
            query = "SELECT '" + ddl_currmon.SelectedItem.ToString() + " " + txtcurrentyr.Text + " Salary' as CUS_REF, pay_employee_master.EMP_NAME, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, '10' as ACCT_TYPE, ((pay_attendance.L_HEAD01+pay_attendance.L_HEAD02+pay_attendance.L_HEAD03+pay_attendance.L_HEAD04+pay_attendance.L_HEAD05+pay_attendance.C_HEAD01+pay_attendance.C_HEAD02+pay_attendance.C_HEAD03+pay_attendance.C_HEAD04+pay_attendance.C_HEAD05+pay_attendance.C_HEAD06+pay_attendance.C_HEAD07+pay_attendance.C_HEAD08+pay_attendance.C_HEAD09+pay_attendance.C_HEAD10+pay_attendance.C_HEAD11+pay_attendance.C_HEAD12+pay_attendance.C_HEAD13+pay_attendance.C_HEAD14+pay_attendance.C_HEAD15+pay_attendance.OT_GROSS) - (pay_attendance.D_HEAD01+pay_attendance.D_HEAD02+pay_attendance.D_HEAD03+pay_attendance.D_HEAD04+pay_attendance.D_HEAD05+pay_attendance.PF+pay_attendance.ESIC_TOT+pay_attendance.PTAX+pay_attendance.D_HEAD06+pay_attendance.D_HEAD07+pay_attendance.D_HEAD08+pay_attendance.D_HEAD09 +pay_attendance.D_LOAN)) As 'Net_Salary', Date_format(now(),'%Y%m%d') as VALUE_DATE FROM pay_attendance INNER JOIN pay_employee_master ON pay_attendance.comp_code = pay_employee_master.comp_code AND pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE INNER JOIN pay_company_master ON pay_attendance.comp_code = pay_company_master.comp_code INNER JOIN pay_unit_master ON pay_attendance.comp_code = pay_unit_master.comp_code AND pay_attendance.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN pay_grade_master ON pay_attendance.CGRADE_CODE = pay_grade_master.GRADE_CODE AND pay_attendance.comp_code = pay_grade_master.comp_code  WHERE pay_attendance.PRESENT_DAYS>0 AND pay_company_master.comp_code='" + Session["comp_code"].ToString() + "'  AND pay_attendance.UNIT_CODE='" + ddlunitselect.SelectedValue + "' AND pay_attendance.MONTH = '" + ddl_currmon.SelectedValue.ToString() + "' AND pay_attendance.YEAR = '" + txtcurrentyr.Text + "'";
        }
        DataTable dt = new DataTable();
        MySqlDataAdapter adp;

        adp = new MySqlDataAdapter(query, d1.con1);

        adp.Fill(dt);
        string csv = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ToString() == "VALUE_DATE")
                {
                    csv += row[column.ColumnName].ToString();
                }
                else
                {

                    //Add the Data rows.

                    csv += row[column.ColumnName].ToString() + ",";
                }
            }
            //Add new line.
            csv += "\r\n";
        }


        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BANK_EXCEL_" + ddl_currmon.SelectedItem.Text + "_" + txtcurrentyr.Text + ".cvs");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(csv);
        Response.Flush();
        Response.End();
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
          //comment 30/09  ds = d.select_data("select distinct state_name from pay_unit_master where comp_code = '"+Session["COMP_CODE"].ToString()+"' and client_code = '"+ddl_client.SelectedValue+"'");
ds = d.select_data("select distinct state_name from pay_unit_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + select_payslip_date.Text + "', '%d/%m/%Y'))");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_state_name.DataSource = ds.Tables[0];
                ddl_state_name.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddl_state_name.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddl_state_name.DataBind();
                ds.Dispose();
            }


            ddl_state_name.Items.Insert(0, "Select");
           
        }
        catch (Exception ex) { throw ex; }
        finally {  }

        //try
        //{
        //    d1.con1.Open();
        //     System.Data.DataTable dt_item = new System.Data.DataTable();
        //     MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code FROM pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY STATE_NAME", d1.con1);
           
        //        cmd_item.Fill(dt_item);
        //        if (dt_item.Rows.Count > 0)
        //        {
        //            ddlunitselect.DataSource = dt_item;
        //            ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
        //            ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
        //            ddlunitselect.DataBind();
        //        }
        //        dt_item.Dispose();
        //        ddlunitselect.Items.Insert(0, "ALL");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //    }
        //    catch (Exception ex) { throw ex; }
        //    finally { d1.con1.Close(); }



        //try
        //{
        //    DataTable dt_emp = new DataTable();
        //    ddl_employee.Items.Clear();
        //    MySqlDataAdapter da_emp = new MySqlDataAdapter("Select pay_attendance.emp_code, pay_employee_master.EMP_NAME from pay_attendance INNER JOIN pay_employee_master ON pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance.comp_code = pay_employee_master.comp_code Where (C_HEAD01+C_HEAD02+C_HEAD03+C_HEAD04+C_HEAD05+C_HEAD06+C_HEAD07+C_HEAD08+C_HEAD09+C_HEAD10+C_HEAD11+C_HEAD12+C_HEAD13+C_HEAD14+C_HEAD15) > 0 AND pay_attendance.UNIT_CODE in (select unit_Code from pay_unit_master where client_code = '"+ddl_client.SelectedValue+"') AND pay_attendance.comp_code ='" + Session["comp_code"].ToString() + "' AND MONTH ='" + ddl_currmon.SelectedValue.ToString() + "' AND YEAR = '" + txtcurrentyr.Text + "' Order by pay_employee_master.EMP_NAME", d.con);
        //    d.con.Open();
        //    da_emp.Fill(dt_emp);
        //    if (dt_emp.Rows.Count > 0)
        //    {
        //        ddl_employee.DataSource = dt_emp;
        //        ddl_employee.DataValueField = dt_emp.Columns[0].ToString();
        //        ddl_employee.DataTextField = dt_emp.Columns[1].ToString();
        //        ddl_employee.DataBind();
        //    }
        //    ddl_employee.Items.Insert(0, "ALL");
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //}
        //txtcurren
    }

    protected void client_code()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
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
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }

    protected void ddlunitselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt_emp = new DataTable();
            ddl_employee.Items.Clear();
            MySqlDataAdapter da_emp = new MySqlDataAdapter("Select pay_attendance.emp_code, pay_employee_master.EMP_NAME from pay_attendance INNER JOIN pay_employee_master ON pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE AND pay_attendance.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_attendance.comp_code = pay_employee_master.comp_code Where (C_HEAD01+C_HEAD02+C_HEAD03+C_HEAD04+C_HEAD05+C_HEAD06+C_HEAD07+C_HEAD08+C_HEAD09+C_HEAD10+C_HEAD11+C_HEAD12+C_HEAD13+C_HEAD14+C_HEAD15) > 0 AND pay_attendance.UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND pay_attendance.comp_code ='" + Session["comp_code"].ToString() + "' AND pay_attendance.MONTH ='" + ddl_currmon.SelectedValue.ToString() + "' AND pay_attendance.YEAR = '" + txtcurrentyr.Text + "' Order by pay_employee_master.EMP_NAME", d.con);
            d.con.Open();
            da_emp.Fill(dt_emp);
            if (dt_emp.Rows.Count > 0)
            {
                ddl_employee.DataSource = dt_emp;
                ddl_employee.DataValueField = dt_emp.Columns[0].ToString();
                ddl_employee.DataTextField = dt_emp.Columns[1].ToString();
                ddl_employee.DataBind();
            }
            ddl_employee.Items.Insert(0, "ALL");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_send_email_Click(object sender, EventArgs e)
    {
        IEnumerable<string> selectedValues = from item in ddl_employee.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Value;
        string listvalues_ddl_utility_cost = string.Join(",", selectedValues);


        while (listvalues_ddl_utility_cost != "")
        {
            if (listvalues_ddl_utility_cost.Length >= 6)
            {
                if (getemail(listvalues_ddl_utility_cost.Substring(0, 6)).Contains("@"))
                {

                    ReportDocument crystalReport = new ReportDocument();
                    crystalReport.Load(Server.MapPath("~/MonthlySalary_PaySlip_all.rpt"));
                    string thisMonth = ddl_currmon.SelectedItem.Text;
                    crystalReport.DataDefinition.FormulaFields["current_month"].Text = @"'" + thisMonth + "'";
                    crystalReport.DataDefinition.FormulaFields["current_year"].Text = @"'" + txtcurrentyr.Text + "'";
                    string filepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "EMP_Images\\SALARY_SLIP_" + thisMonth + "_" + txtcurrentyr.Text + ".pdf");
                    DataTable dt = new DataTable();
                    MySqlCommand cmd = new MySqlCommand("SELECT pay_company_master.comp_code, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2,  pay_attendance_muster.HOLIDAYS As CITY, pay_company_master.STATE, pay_company_master.PIN, pay_company_master.PF_REG_NO, pay_company_master.ESIC_REG_NO, pay_company_master.E_HEAD01 AS HEAD01, pay_company_master.E_HEAD02 AS HEAD02, pay_company_master.E_HEAD03 AS HEAD03, pay_company_master.E_HEAD04 AS HEAD04, pay_company_master.E_HEAD05 AS HEAD05, pay_company_master.E_HEAD06 AS HEAD06, pay_company_master.E_HEAD07 AS HEAD07, pay_company_master.E_HEAD08 AS HEAD08, pay_company_master.E_HEAD09 AS HEAD09, pay_company_master.E_HEAD10 AS HEAD10, pay_company_master.E_HEAD11 AS HEAD11, pay_company_master.E_HEAD12 AS HEAD12, pay_company_master.L_HEAD01 AS LHEAD01, pay_company_master.L_HEAD02 AS LHEAD02, pay_company_master.L_HEAD03 AS LHEAD03, pay_company_master.L_HEAD04 AS LHEAD04, pay_company_master.D_HEAD01 AS DHEAD01, pay_company_master.D_HEAD02 AS DHEAD02, pay_company_master.D_HEAD03 AS DHEAD03, pay_company_master.D_HEAD04 AS DHEAD04, pay_company_master.D_HEAD05 AS DHEAD05, pay_attendance.MONTH, pay_attendance.Year, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE,'%d/%m/%Y') As JOINING_DATE, pay_employee_master.GENDER, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_BANK_NAME As BANK_CODE, pay_employee_master.STATUS, pay_employee_master.PF_NUMBER, pay_employee_master.E_HEAD01, pay_employee_master.E_HEAD02, pay_employee_master.E_HEAD03, pay_employee_master.E_HEAD04, pay_employee_master.E_HEAD05, pay_employee_master.E_HEAD06, pay_employee_master.E_HEAD07, pay_employee_master.E_HEAD08, pay_employee_master.E_HEAD09, pay_employee_master.E_HEAD10, pay_employee_master.E_HEAD11, pay_employee_master.E_HEAD12, pay_employee_master.PF_SHEET, pay_employee_master.EARN_TOTAL, pay_employee_master.LEFT_REASON, pay_unit_master.UNIT_NAME, pay_unit_master.UNIT_ADD1, pay_unit_master.UNIT_ADD2, pay_unit_master.UNIT_CITY, pay_attendance.PRESENT_DAYS, pay_attendance.LEAVE_DAYS, pay_attendance.PAYABLE_DAYS, pay_attendance.OT_HRS, pay_attendance.L_HEAD01, pay_attendance.L_HEAD02, pay_attendance.L_HEAD03, pay_attendance.L_HEAD04, pay_attendance.D_HEAD01, pay_attendance.D_LOAN, pay_attendance.D_HEAD02, pay_attendance.D_HEAD03, pay_attendance.D_HEAD04, pay_attendance.D_HEAD05, pay_attendance.INCOMETAX, pay_attendance.C_HEAD01, pay_attendance.C_HEAD02, pay_attendance.C_HEAD03, pay_attendance.C_HEAD04, pay_attendance.C_HEAD05, pay_attendance.C_HEAD06, pay_attendance.C_HEAD07, pay_attendance.C_HEAD08, pay_attendance.C_HEAD09, pay_attendance.C_HEAD10, pay_attendance.C_HEAD11, pay_attendance.C_HEAD12, pay_attendance.PF, pay_attendance.ESIC_TOT, pay_attendance.OT_GROSS, pay_attendance.PTAX, pay_grade_master.GRADE_DESC, pay_employee_master.ESIC_NUMBER,pay_attendance.TOT_ESIC_GROSS, pay_attendance.ESIC_COMP_CONTRI, pay_attendance.MLWF, pay_company_master.D_HEAD06 AS DHEAD06, pay_company_master.D_HEAD07 AS DHEAD07, pay_company_master.D_HEAD08 AS DHEAD08, pay_company_master.D_HEAD09 AS DHEAD09, pay_attendance.D_HEAD06, pay_attendance.D_HEAD07, pay_attendance.D_HEAD08, pay_attendance.D_HEAD09, pay_attendance.CGRADE_CODE, pay_attendance_muster.TOT_CO As EXTRA_DAYS, pay_attendance.EXTRA_GROSS, pay_attendance.UNIT_CODE, pay_attendance.CPF_SHEET, pay_department_master.dept_name,pay_attendance.ABSENT_DAYS,pay_employee_master.EMP_NEW_PAN_NO As UAN,pay_attendance.COMP_PF_PEN,pay_attendance.ESIC_COMP_CONTRI,  0, 0 FROM pay_attendance INNER JOIN pay_employee_master ON pay_attendance.comp_code = pay_employee_master.comp_code AND pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE INNER JOIN pay_company_master ON pay_attendance.comp_code = pay_company_master.comp_code INNER JOIN pay_unit_master ON pay_attendance.comp_code = pay_unit_master.comp_code AND pay_attendance.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN pay_grade_master ON pay_attendance.CGRADE_CODE = pay_grade_master.GRADE_CODE AND pay_attendance.comp_code = pay_grade_master.comp_code INNER JOIN pay_department_master ON pay_employee_master.dept_code = pay_department_master.dept_code INNER JOIN pay_attendance_muster ON pay_attendance.emp_code = pay_attendance_muster.emp_code and  pay_attendance.month = pay_attendance_muster.month and pay_attendance.year = pay_attendance_muster.year WHERE pay_attendance.PRESENT_DAYS>0 AND pay_company_master.comp_code='" + Session["comp_code"].ToString() + "'  AND pay_attendance.MONTH = '" + ddl_currmon.SelectedValue.ToString() + "' AND pay_attendance.YEAR = '" + txtcurrentyr.Text + "' and pay_attendance.emp_code = '" + listvalues_ddl_utility_cost.Substring(0, 6) + "'");
                    MySqlDataReader sda = null;
                    cmd.Connection = d.con;
                    d.con.Open();
                    sda = cmd.ExecuteReader();
                    dt.Load(sda);
                    d.con.Close();
                    crystalReport.Refresh();
                    crystalReport.SetDataSource(dt);

                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = filepath;
                    CrExportOptions = crystalReport.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    crystalReport.Export();
                    CrDiskFileDestinationOptions = null;
                    CrExportOptions = null;
                    CrFormatTypeOptions = null;
                    crystalReport.Dispose();

                    send_email(filepath, listvalues_ddl_utility_cost.Substring(0, 6));

                    File.Delete(filepath);
                }

                if (listvalues_ddl_utility_cost.Length >= 6)
                {
                    if (listvalues_ddl_utility_cost.Length == 6)
                    {
                        listvalues_ddl_utility_cost = "";
                    }
                    else
                    {
                        listvalues_ddl_utility_cost = listvalues_ddl_utility_cost.Substring(7, listvalues_ddl_utility_cost.Length - 7);
                    }
                }
            }
            else
            {
                listvalues_ddl_utility_cost = "";
            }
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email Sent Successfully !!')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }




    }

    private void send_email(string attachments, string emp_code)
    {
        using (MailMessage mm = new MailMessage("vinod_pol@yahoo.com", getemail(emp_code)))
        {
            mm.Subject = "Salary Slip for the month of " + ddl_currmon.SelectedItem.Text + " and Year " + txtcurrentyr.Text;
            mm.Body = "\r\n Please find the attached Salary Slip for the month of " + ddl_currmon.SelectedItem.Text + " and Year " + txtcurrentyr.Text + ".\r\n \r\n NOTE : This is an automated e-mail to help you. Hence, please do not reply to this e-mail.";


            // string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            mm.Attachments.Add(new Attachment(attachments));

            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp = new SmtpClient("smtp.mail.yahoo.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential("vinod_pol@yahoo.com", ""); // password Put 
            smtp.Credentials = NetworkCred;
            smtp.Send(mm);
        }
    }

    public string getemail(string emp_code)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select emp_email_id from pay_employee_master where emp_code = '" + emp_code + "'", d.con);
        try
        {
            return (string)cmd.ExecuteScalar();
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
    }
    public void get_form16()
    {
        ReportDocument crystalReport = new ReportDocument();
        try
        {
            crystalReport.Load(Server.MapPath("~/Form16.rpt"));

            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Form16");

            Response.End();
        }
        catch (Exception ee)
        {


        }
    }


    protected void btn_form_16_Click(object sender, EventArgs e)
    {
        get_form16();
    }
    //start vikas 03/05/2019
    //protected void employee_status()
    //{

    //    try
    //    {
    //        ViewState["rem_emp_count"] = 0;

    //        panel_link.Visible = true;

    //        gv_rem_emp_count.DataSource = null;
    //        gv_rem_emp_count.DataBind();

    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter(" SELECT emp_code,`emp_name` FROM `pay_pro_master` where pay_pro_master.month = '" + select_payslip_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + select_payslip_date.Text.Substring(3) + "' AND  pay_pro_master.client_code = '" + ddl_client.SelectedValue + "'  and pay_pro_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_pro_master.Total_Days_Present > 0  AND   (`PAN_No`= '' or PAN_No is  null ) AND (`PF_No`  = '' or PF_No is null) AND (`ESI_No` = '' or ESI_No is null) order by pay_pro_master.state_name,pay_pro_master.unit_code,pay_pro_master.emp_name ", d.con);
    //        cmd_item.Fill(dt_item);
    //        rem_emp_count = "0";
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ViewState["rem_emp_count"] = dt_item.Rows.Count.ToString();
    //            rem_emp_count = ViewState["rem_emp_count"].ToString();
    //            gv_rem_emp_count.DataSource = dt_item;
    //            gv_rem_emp_count.DataBind();

    //        }

    //        rem_emp_count = ViewState["rem_emp_count"].ToString();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { }

    //}

    //end vikas 05/05/2019

	 protected void ddl_state_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ddlunitselect.Items.Clear();
        //comment 30/09    ds = d.select_data("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code FROM pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '"+ddl_state_name.SelectedValue+"' ORDER BY STATE_NAME");
ds = d.select_data("SELECT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code FROM pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state_name.SelectedValue + "' AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + select_payslip_date.Text + "', '%d/%m/%Y')) ORDER BY STATE_NAME");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlunitselect.DataSource = ds.Tables[0];
                ddlunitselect.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddlunitselect.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlunitselect.DataBind();
                ds.Dispose();
            }


            ddlunitselect.Items.Insert(0, "ALL");
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { }

    }
    //vikas add 
     protected void Button1_Click(object sender, EventArgs e)
     {
         try
         {
             string sql = "SELECT  `client_name` AS 'client_code',`emp_code`,emp_name,`unit_name` AS 'UNIT_CODE',client_wise_state, `PAN_NUMBER`, `ESIC_NUMBER`, `PF_NUMBER` FROM `pay_employee_master` INNER JOIN `pay_client_master` ON `pay_employee_master`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_client_master`.`client_code` INNER JOIN `pay_unit_master` ON `pay_employee_master`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_unit_master`.`client_code` and `pay_employee_master`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND (`pay_employee_master`.`LEFT_REASON` = '' || `pay_employee_master`.`LEFT_REASON` IS NULL)  and ((`PAN_NUMBER` = '' OR `pay_employee_master`.`PAN_NUMBER` IS NULL)or (`pay_employee_master`.`ESIC_NUMBER` = '' OR `pay_employee_master`.`ESIC_NUMBER` IS NULL ) or (`pay_employee_master`.`PF_NUMBER` = '' OR `pay_employee_master`.`PF_NUMBER` IS NULL )) group by emp_code";
             MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
             DataSet ds = new DataSet();
             dscmd.Fill(ds);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 Response.Clear();
                 Response.Buffer = true;
                 Response.AddHeader("content-disposition", "attachment;filename=Employee" + ddl_client.SelectedItem.Text + ".xls");

                 Response.Charset = "";
                 Response.ContentType = "application/vnd.ms-excel";
                 Repeater Repeater1 = new Repeater();
                 Repeater1.DataSource = ds;
                 Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds, 1);
                 Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds, 1);
                 Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null, 1);
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
     public class MyTemplate1 : ITemplate
     {
         ListItemType type;
         LiteralControl lc;
         DataSet ds;
         static int ctr;
         int i;


         public MyTemplate1(ListItemType type, DataSet ds, int i)
         {
             this.type = type;
             this.ds = ds;

             ctr = 0;
             //paid_days = 0;
             //rate = 0;
         }

         public void InstantiateIn(Control container)
         {

             switch (type)
             {
                 case ListItemType.Header:

                     lc = new LiteralControl("<table border=1><tr ><th>SR No.</th><th>Client Name</th><th>State Name</th><th>Location Name</th><th>Employee Name</th><th>UAN No</th><th>PF No</th><th>ESIC No</th></tr> ");
                     break;
                 case ListItemType.Item:

                     lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_wise_state"] + "</td><td>" + ds.Tables[0].Rows[ctr]["UNIT_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PAN_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ESIC_NUMBER"] + "</td></tr>");

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
