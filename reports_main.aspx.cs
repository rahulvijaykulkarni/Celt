using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

public partial class Employee_salary_details : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_report_gst where comp_code='" + Session["comp_code"] + "' GROUP BY client_code ORDER BY client_code", d.con);
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
                ddl_client.Items.Insert(0, "ALL");
                ddl_state.Items.Insert(0, "ALL");
                ddl_unitcode.Items.Insert(0, "ALL");
                ddl_employee.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "ALL")
        {

            d1.con1.Open();
            ddl_state.Items.Clear();
            try
            {
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "')  ORDER BY STATE", d1.con1);
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


            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND state_name ='" + ddl_state.SelectedValue + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
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
                ddl_unitcode.SelectedIndex = 0;
                ddl_state_SelectedIndexChanged(null, null);
                ddlunitselect_SelectedIndexChanged(null, null);
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

        ddl_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item;
        string left = " employee_type = '" + ddl_employee_type.SelectedValue + "' and  (left_date = '' or left_date is null)";
        if (ddl_employee_type.SelectedValue == "Left")
        {
            left = " left_date is not null";
        }
        string where = " where  comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "' and " + left + " ORDER BY emp_name";
        if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = " where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' and " + left + " ORDER BY emp_name";
        }
        //vikas 08-01-19
        cmd_item = new MySqlDataAdapter("Select (SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',EMP_CODE from pay_employee_master " + where, d.con);
        d.con.Open();
        try
        {
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
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "ALL")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "')   ORDER BY pay_unit_master.state_name", d.con);
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
                ddl_unitcode.SelectedIndex = 0;
                ddlunitselect_SelectedIndexChanged(null, null);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    private void send_file(int count)
    {
        string sql = "";
        d.con.Open();
        if (count == 1)
        {
            string where = "";
            if (ddl_emp_diff.SelectedValue.Equals("0"))
            {
                where = " where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and employee_type = 'Permanent' and left_date is null order by 1,2,4,6";
            }
            else if (ddl_emp_diff.SelectedValue.Equals("1"))
            {
                d.operation("create table pay_number_update (num_ber varchar(100));insert into pay_number_update SELECT " + ddl_emp_type.SelectedValue + " FROM pay_employee_master where employee_type = 'Permanent' and " + ddl_emp_type.SelectedValue + " is not null and " + ddl_emp_type.SelectedValue + " != '' GROUP BY " + ddl_emp_type.SelectedValue + " HAVING COUNT(*) > 1;");
                where = " inner join pay_number_update on num_ber =" + ddl_emp_type.SelectedValue + " where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and employee_type = 'Permanent' order by 7";
            }
            else if (ddl_emp_diff.SelectedValue.Equals("2"))
            {
                where = " where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and employee_type = 'Permanent' and (" + ddl_emp_type.SelectedValue + " is null or " + ddl_emp_type.SelectedValue + " = '') order by 1,2,4,6";
            }
            sql = "select client_name as 'CLIENT NAME',state_name as STATE, unit_name as 'BRANCH NAME',unit_city as 'BRANCH CITY',EMP_NAME,EMP_FATHER_NAME, " + ddl_emp_type.SelectedValue.ToUpper() + " as Number, date_format(left_date,'%d/%m/%Y') as 'LEFT DATE',date_format(joining_date,'%d/%m/%Y') as 'JOINING DATE' from pay_employee_master inner join pay_unit_master on pay_unit_master.comp_code = pay_employee_master.comp_code and pay_unit_master.unit_code = pay_employee_master.unit_code inner join pay_client_master on pay_client_master.comp_code = pay_employee_master.comp_code and pay_unit_master.client_code = pay_client_master.client_code " + where;
        }
        else if (count == 2)
        {
            string where = " where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'";
            if (!ddl_client.SelectedValue.Equals("ALL"))
            {
                where = where + " and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'";
            }
            if (!ddl_state.SelectedValue.Equals("ALL"))
            {
                where = where + " and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "'";
            }
            if (!ddl_unitcode.SelectedValue.Equals("ALL"))
            {
                where = where + " and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }
            if (!ddl_employee_type.SelectedValue.Equals("ALL"))
            {
                where = where + " and pay_employee_master.employee_type = '" + ddl_employee_type.SelectedValue + "'";
            }
            if (!ddl_employee.SelectedValue.Equals("ALL"))
            {
                where = where + " and pay_employee_master.emp_code = '" + ddl_employee.SelectedValue + "'";
            }
            sql = "SELECT client_name AS 'CLIENT NAME', pay_billing_unit_Rate_history. state_name AS 'STATE', pay_billing_unit_Rate_history.unit_name AS 'BRANCH', pay_employee_master.emp_name AS 'EMPLOYEE NAME', pay_employee_master.employee_type AS 'EMPLOYEE TYPE', pay_employee_master.p_tax_number as 'AADHAR NUMBER', pay_employee_master.pan_number as 'UAN NUMBER', pay_employee_master.pf_number as 'PF NUMBER', pay_employee_master.esic_number as 'ESIC NUMBER', pay_billing_unit_Rate_history.tot_days_present as 'DAYS PRESENT', round(pay_billing_unit_Rate_history.Amount,2) as 'BILLING AMOUNT', round(pay_pro_master.payment,2) as 'PAYMENT AMOUNT', MONTHNAME(STR_TO_DATE(pay_billing_unit_Rate_history.month, '%m')) as 'MONTH', pay_billing_unit_Rate_history.year as 'YEAR' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_client_master.client_code = pay_unit_master.client_code AND pay_client_master.comp_code = pay_unit_master.comp_code inner join pay_billing_unit_Rate_history on pay_employee_master.emp_code = pay_billing_unit_Rate_history.emp_code inner join pay_pro_master on pay_employee_master.emp_code = pay_pro_master.emp_code AND pay_pro_master.month = pay_billing_unit_Rate_history.month and pay_pro_master.year = pay_billing_unit_Rate_history.year " + where + " AND pay_billing_unit_Rate_history.month = " + txt_month_year.Text.Substring(0, 2) + " and pay_billing_unit_Rate_history.year = " + txt_month_year.Text.Substring(3) + " GROUP BY client_name, pay_billing_unit_Rate_history.state_name, pay_billing_unit_Rate_history.unit_name, pay_employee_master.emp_name ORDER BY 1, 2, 3,4";
        }
        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);
        d.operation("DROP TABLE IF EXISTS pay_number_update;");
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (count == 1)
            {
                Response.AddHeader("content-disposition", "attachment;filename=Employees_Documents.xls");
            }
            else if (count == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=Employees_Details.xls");
            }
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, count, ddl_emp_type.SelectedItem.ToString());
            Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, count, ddl_emp_type.SelectedItem.ToString());
            Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, count, ddl_emp_type.SelectedItem.ToString());
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
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;
        string emp_type;
        public MyTemplate(ListItemType type, DataSet ds, int i, string emp_type)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.emp_type = emp_type;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            { //Original Bank A/C Number ,PF_IFSC_CODE,BANK_HOLDER_NAME
                case ListItemType.Header:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>Client Name</th><th>State Name</th><th>Branch Name</th><th>Branch City</th><th>EMPLOYEE NAME</th><th>EMPLOYEE FATHER NAME</th><th>" + emp_type + " NUMBER</th><th>LEFT DATE</th><th>JOINING DATE</th></tr>");
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<TABLE BORDER=1><TR><TH>SR. NO.</TH><TH>CLIENT NAME</TH><TH>STATE NAME</TH><TH>BRANCH NAME</TH><TH>EMPLOYEE NAME</TH><TH>EMPLOYEE TYPE</TH><TH>AADHAR NUMBER</TH><TH>UAN NUMBER</TH><TH>PF NUMBER</TH><TH>ESIC NUMBER</TH><TH>DAYS PRESENT</TH><TH>BILLING AMOUNT</TH><TH>PAYMENT AMOUNT</TH><th>MONTH</th><th>YEAR</th></TR>");
                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["BRANCH NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["BRANCH CITY"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_FATHER_NAME"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["Number"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LEFT DATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["JOINING DATE"].ToString().ToUpper() + "</td></tr>");
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["BRANCH"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMPLOYEE NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMPLOYEE TYPE"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["AADHAR NUMBER"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["UAN NUMBER"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PF NUMBER"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC NUMBER"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["DAYS PRESENT"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["BILLING AMOUNT"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PAYMENT AMOUNT"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["MONTH"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["YEAR"].ToString().ToUpper() + "</td></tr>");
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
    protected void btn_emp_report_Click(object sender, EventArgs e)
    {
        send_file(1);
    }
    protected void btn_employee_report_Click(object sender, EventArgs e)
    {
        send_file(2);
    }
    protected void btn_getxl_report_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        if (ddl_report.SelectedValue == "PF XL")
        {
            export_xl(1);
        }
        if (ddl_report.SelectedValue == "LWF XL")
        {
            export_xl(2);
        }
        if (ddl_report.SelectedValue == "PT XL")
        {
            export_xl(3);
        }
        if (ddl_report.SelectedValue == "ESIC XL")
        {
            export_xl(4);
        }
        if (ddl_report.SelectedValue == "GST XL")
        {
            export_xl(5);
        }
        if (ddl_report.SelectedValue == "Branch Head Contact Details")
        {
            export_xl(6);
        }
        if (ddl_report.SelectedValue == "Salary Slip Sending Details")
        {
            export_xl(7);
        }
        if (ddl_report.SelectedValue == "Joining Letter Sending Details")
        {
            export_xl(8);
        }
        if (ddl_report.SelectedValue == "Monthwise Billing Details")
        {
            export_xl(9);
        }



    }
    private void export_xl(int i)
    {
        string t = ddl_bill_type.SelectedValue;
        string sql = null;
        string where_head = "";
        string where_salary = "";
        string where_joining = "";
        string where_billing = "";
        string client = "";

        where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "'  and branch_status != 1 ";
        where_salary = "where pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_pro_master.unit_code = '" + ddl_unitcode.SelectedValue + "'  ";
        where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        where_billing = " WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        if (ddl_state.SelectedValue == "ALL")
        {
            where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client.SelectedValue + "' and branch_status != 1  ";
            where_salary = " where pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' ";
            where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' ";
            where_billing = " WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ";
        }
        else if (ddl_unitcode.SelectedValue == "ALL")
        {
            where_head = "where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code= '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' and branch_status != 1  ";
            where_salary = " where pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_pro_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_pro_master.state_name = '" + ddl_state.SelectedValue + "' ";
            where_joining = " WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and client_wise_state = '" + ddl_state.SelectedValue + "' ";
            where_billing = " WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "' ";
        }

        if (ddl_client.SelectedValue == "ALL")
        {
            where_billing = " WHERE comp_code = '" + Session["comp_code"].ToString() + "' ";
        }
        if (i == 1)
        {
            sql = "SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, ROUND(pf, 2) AS 'PF_EmployerAmount', ROUND(sal_pf, 2) AS 'PF_EmployeeAmount' FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code AND pay_billing_unit_rate_history.emp_type IN ('Permanent', 'Temporary') INNER JOIN pay_pro_master ON pay_pro_master.emp_code = pay_employee_master.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND payment_status = 1 WHERE pay_billing_unit_rate_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_unit_rate_history.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_date.Text.Substring(3) + "' ORDER BY 1, 2, 3, 4 ";
            //sql = "SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.month, ROUND(pf, 2) AS 'PF_EmployerAmount', ROUND(sal_pf, 2) AS 'PF_EmployeeAmount' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.comp_code = pay_pro_master.comp_code   AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name   AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code   AND invoice_no IS NOT NULL INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code  WHERE pay_billing_unit_rate_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_unit_rate_history.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_date.Text.Substring(3) + "' ORDER BY pay_billing_unit_rate_history.state_name";
            //sql = "SELECT client, state_name, unit_name, emp_name, grade_desc, month, ROUND(pf,2) as 'PF_Amount' FROM pay_billing_unit_rate_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' ORDER BY state_name";

        }
        if (i == 2)
        {
            sql = "SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, ROUND(lwf, 2) AS 'LWF_EmployerAmount', ROUND(lwf_salary, 2) AS 'LWF_EmployeeAmount' FROM pay_employee_master INNER JOIN pay_billing_unit_rate_history ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code AND pay_billing_unit_rate_history.emp_type IN ('Permanent', 'Temporary') INNER JOIN pay_pro_master ON pay_pro_master.emp_code = pay_employee_master.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND payment_status = 1 WHERE pay_billing_unit_rate_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' AND pay_billing_unit_rate_history.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_billing_unit_rate_history.year = '" + txt_date.Text.Substring(3) + "' ORDER BY 1, 2, 3, 4 ";
            // sql = "SELECT client, state_name, unit_name, emp_name, grade_desc, month, ROUND(lwf,2) as 'LWF_Amount' FROM pay_billing_unit_rate_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' ORDER BY state_name";

        }
        if (i == 3)
        {

            sql = "SELECT client, state_name, unit_name, emp_name, grade, month,year,ROUND(PT_AMOUNT,2) as 'PT_Amount' FROM pay_pro_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "'  AND payment_status = 1 ORDER BY 1, 2, 3, 4";

        }
        if (i == 4)
        {

            sql = "SELECT CONCAT(pay_pro_master.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, CASE grade WHEN 'OFFICE BOY' THEN CASE pay_employee_master.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade', pay_pro_master.emp_name, ROUND(((pay_pro_master.gross * 1.75) / 100), 2) AS 'sal_esic', ROUND(((pay_pro_master.gross * 4.75) / 100), 2) AS 'bill_esic' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND invoice_no IS NOT NULL INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code WHERE pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "' AND pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_pro_master.state_name = '" + ddl_state.SelectedValue + "' AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent') AND (PAN_No IS NOT NULL AND PAN_No != '') AND (ESI_No IS NOT NULL AND ESI_No != '') GROUP BY pay_pro_master.client, pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.emp_name";

        }
        if (i == 5)
        {

            sql = "SELECT client, state_name, IFNULL( auto_invoice_no ,  invoice_no ) AS 'invoice_no',month,CGST9,SGST9,IGST18,(CGST9+SGST9+IGST18) as 'TOTAL GST' FROM pay_billing_unit_rate_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' AND month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' AND ( emp_code  != '' OR  emp_code  IS NOT NULL) ORDER BY 1,2,3";

        }
        if (i == 6)
        {

            sql = "SELECT client_name, state_name, unit_name, LocationHead_Name, LocationHead_mobileno, LocationHead_Emailid, OperationHead_Name, OperationHead_Mobileno, OperationHead_EmailId, FinanceHead_Name, FinanceHead_Mobileno, FinanceHead_EmailId, adminhead_name, adminhead_mobile, adminhead_email FROM pay_unit_master inner join pay_client_master on pay_unit_master.comp_code = pay_client_master.comp_code and pay_unit_master.client_code = pay_client_master.client_code " + where_head;

        }
        if (i == 7)
        {

            sql = "SELECT client_name, state_name, unit_name, emp_name, grade,month, year, (CASE WHEN branch_email = 0 THEN 'Not Send' WHEN branch_email = 2 THEN 'Send' ELSE 'Not Send' END) AS 'status' FROM pay_pro_master INNER JOIN pay_client_master ON pay_pro_master.comp_code = pay_client_master.comp_code AND pay_pro_master.client_code = pay_client_master.client_code " + where_salary + " and  pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "' and employee_type != 'Reliever' ";

        }
        if (i == 8)
        {

            sql = "  SELECT client_name, client_wise_state AS 'state_name', unit_name, emp_name, GRADE_DESC as 'designation', date_format(joining_date, '%d/%m/%y') AS 'joining_date',  (CASE WHEN joining_letter_email = 0 THEN 'Not Send' WHEN joining_letter_email = 1 THEN 'Send' ELSE 'Not Send' END) AS 'status' FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_unit_master.client_code = pay_employee_master.client_code AND pay_unit_master.unit_code = pay_employee_master.unit_code INNER JOIN  pay_grade_master  ON  pay_employee_master . comp_code  =  pay_grade_master . comp_code  and  pay_employee_master . GRADE_CODE  =  pay_grade_master . GRADE_CODE " + where_joining + " and left_date is null  ORDER BY 3";

        }
        if (i == 9)
        {
            if (ddl_bill_type.SelectedValue == "1")
            {
                client = ",client";
            }

            if ((ddl_bill_type.SelectedValue == "1") || (ddl_bill_type.SelectedValue == "2"))
            {
                sql = "SELECT client, state_name,month,year, sum(Amount + uniform + operational_cost + conveyance_rate + group_insurance_billing + ot_amount + service_charge) AS 'Amount', sum((CGST9) + (SGST9) + (IGST18)) AS 'GST', sum((Amount) + (uniform) + (operational_cost) + (Service_charge) + (ot_amount) + (group_insurance_billing) + (conveyance_rate) + (CGST9) + (SGST9) + (IGST18)) AS 'Grand Total'  FROM pay_billing_unit_rate_history " + where_billing + "and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' and invoice_flag = 1 and  EMP_CODE != '' GROUP BY pay_billing_unit_rate_history.state_name " + client + " ORDER BY 1, 2, 3, 4";
            }
            if (ddl_bill_type.SelectedValue == "3")
            {
                sql = "SELECT client, state_name, unit_name,month,year,  sum(Amount + uniform + operational_cost + conveyance_rate + group_insurance_billing + ot_amount + service_charge) AS 'Amount', sum((CGST9) + (SGST9) + (IGST18)) AS 'GST', sum((Amount) + (uniform) + (operational_cost) + (Service_charge) + (ot_amount) + (group_insurance_billing) + (conveyance_rate) + (CGST9) + (SGST9) + (IGST18)) AS 'Grand Total' FROM pay_billing_unit_rate_history " + where_billing + "and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' and invoice_flag = 1 and  EMP_CODE != '' GROUP BY pay_billing_unit_rate_history.unit_name ORDER BY 1, 2, 3, 4";
            }
        }

        MySqlCommand cmd = new MySqlCommand(sql, d.con);

        MySqlDataAdapter dscmd = new MySqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            if (i == 1)
            {
                Response.AddHeader("content-disposition", "attachment;filename=PF_REPORT.xls");
            }
            else if (i == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=LWF_REPORT.xls");
            }

            else if (i == 3)
            {
                Response.AddHeader("content-disposition", "attachment;filename=PT_REPORT.xls");
            }
            else if (i == 4)
            {
                Response.AddHeader("content-disposition", "attachment;filename=ESIC_REPORT.xls");
            }
            else if (i == 5)
            {
                Response.AddHeader("content-disposition", "attachment;filename=GST_REPORT.xls");
            }
            else if (i == 6)
            {
                Response.AddHeader("content-disposition", "attachment;filename= HEAD_CONTACT_DETAILS.xls");
            }
            else if (i == 7)
            {
                Response.AddHeader("content-disposition", "attachment;filename=SALARY_SLIP_SENDING_DETAILS.xls");
            }
            else if (i == 8)
            {
                Response.AddHeader("content-disposition", "attachment;filename=JOINING_LETTER_DETAILS.xls");
            }
            else if (i == 9)
            {
                if (t == "1")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=BILLING_DETAILS_CLIENTWISE.xls");
                }
                if (t == "2")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=BILLING_DETAILS_STATEWISE.xls");
                }
                if (t == "3")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=BILLING_DETAILS_BRANCHWISE.xls");
                }
            }
            string date1 = txt_date.Text;

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate12(ListItemType.Header, ds, i, date1, t, 1);
            Repeater1.ItemTemplate = new MyTemplate12(ListItemType.Item, ds, i, date1, t, 1);
            Repeater1.FooterTemplate = new MyTemplate12(ListItemType.Footer, null, i, date1, t, 1);
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
    //ada 
    public class MyTemplate12 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        static int ctr1;
        int i;
        string emp_type;
        string date1;
        string t;
        double emp_esic = 0, empr_esic = 0, total = 0;
        string client_name = "";
        int i3 = 1;
        private ListItemType listItemType;
        double amount = 0, gst = 0, grand_total = 0, amount1 = 0, gst1 = 0, grand_total1 = 0;

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
        public MyTemplate12(ListItemType type, DataSet ds, int i, string date1, string t, int i3)
        {
            // TODO: Complete member initialization
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.date1 = date1;
            this.t = t;

            this.i3 = i3;

        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            { //Original Bank A/C Number ,PF_IFSC_CODE,BANK_HOLDER_NAME
                case ListItemType.Header:


                    // var today = DateTime.Now;
                    var current_date = date1;

                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=11 align=center> PF REPORT MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>DESIGNATION</th><th>EMPLOYEE NAME</th><th>MONTH</th><th>YEAR</th><th>EMPLOYER CONTRIBUTION</th><th>EMPLOYEE CONTRIBUTION</th><th>TOTAL AMOUNT</th></tr>");
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=8 align=center> LWF REPORT MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>DESIGNATION</th><th>EMPLOYEE NAME</th><th>MONTH</th><th>YEAR</th><th>EMPLOYER CONTRIBUTION</th><th>EMPLOYEE CONTRIBUTION</th><th>TOTAL AMOUNT</th></tr>");
                    }
                    else if (i == 3)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=9 align=center> PT REPORT MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>DESIGNATION</th><th>EMPLOYEE NAME</th><th>MONTH</th><th>YEAR</th><th>AMOUNT</th></tr>");
                    }
                    else if (i == 4)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=9 align=center> ESIC REPORT MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>STATE NAME</th><th>CLIENT NAME</th><th>BRANCH NAME</th><th>DESIGNATION</th><th>EMPLOYEE NAME</th><th>EMPLOYEE CONTRIBUTION</th><th>EMPLOYER CONTRIBUTION</th><th>TOTAL AMOUNT</th></tr>");

                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=9 align=center> GST REPORT MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>INVOICE NO</th><th>MONTH</th><th>CGST</th><th>SGST</th><th>IGST</th><th>TOTAL GST</th></tr>");
                    }
                    else if (i == 6)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=16 align=center> HEAD CONTACT DETAILS</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>LOCATION HEAD NAME</th><th>LOCATION HEAD MOBILE NO</th><th>LOCATION HEAD E-MAIL</th><th>OPERTION HEAD NAME</th><th>OPERTION HEAD MOBILE NO</th><th>OPERTION HEAD E-MAIL</th><th>FINANCE HEAD NAME</th><th>FINANCE HEAD MOBILE NO</th><th>FINANCE HEAD E-MAIL</th><th>ADMIN HEAD NAME</th><th>ADMIN HEAD MOBILE NO</th><th>ADMIN HEAD E-MAIL</th></tr>");
                    }
                    else if (i == 7)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=9 align=center> SALARY SLIP SENDING DETAILS MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>EMPLOYEE NAME</th><th>DESIGNATION</th><th>MONTH</th><th>YEAR</th><th>STATUS</th></tr>");
                    }
                    else if (i == 8)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=8 align=center> JOINING LETTER DETAILS </th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>EMPLOYEE NAME</th><th>DESIGNATION</th><th>JOINING DATE</th><th>STATUS</th></tr>");
                    }
                    else if (i == 9)
                    {
                        if ((t == "1") || (t == "2"))
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=8 align=center> BILLING DETAILS MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>MONTH</th><th>YEAR</th><th>AMOUNT</th><th>GST</th><th>GRAND TOTAL</th></tr>");
                        }
                        if (t == "3")
                        {
                            lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=9 align=center> BILLING DETAILS MONTH " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>BRANCH NAME</th><th>MONTH</th><th>YEAR</th><th>AMOUNT</th><th>GST</th><th>GRAND TOTAL</th></tr>");
                        }

                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_EmployerAmount"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_EmployeeAmount"].ToString().ToUpper() + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["PF_EmployerAmount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["PF_EmployeeAmount"].ToString()), 2) + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 10>Total</td><td>=ROUND(SUM(k3:k" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LWF_EmployerAmount"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LWF_EmployeeAmount"].ToString().ToUpper() + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["LWF_EmployerAmount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["LWF_EmployeeAmount"].ToString()), 2) + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 10>Total</td><td>=ROUND(SUM(k3:k" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 3)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PT_Amount"].ToString().ToUpper() + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 8>Total</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 4)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()), 2) + "</td></tr>");
                        emp_esic = emp_esic + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2);
                        empr_esic = empr_esic + (double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));
                        total = total + (double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));

                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 8>Total</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),0)</td></b></tr>";
                        }

                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["CGST9"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["SGST9"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["IGST18"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["TOTAL GST"].ToString().ToUpper() + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 8>Total</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 6)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LocationHead_Emailid"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_Mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["OperationHead_EmailId"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_Mobileno"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FinanceHead_EmailId"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_mobile"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["adminhead_email"].ToString().ToUpper() + "</td></tr>");

                    }
                    else if (i == 7)
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
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["GRADE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td " + bg + ">" + ds.Tables[0].Rows[ctr]["status"].ToString().ToUpper() + "</td></tr>");
                    }
                    else if (i == 8)
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
                    else if (i == 9)
                    {
                        int set_start_row = 3;
                        if (t == "2")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Amount"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["GST"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Grand Total"].ToString().ToUpper() + "</td></tr>");
                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td></b></tr>";
                            }
                        }

                        if (t == "1")
                        {
                            if (client_name != ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper())
                            {
                                if (client_name != "")
                                {
                                    i3 = i3 + 1;

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>" + amount + "</td><td>" + gst + "</td><td>" + grand_total + "</td></b></tr>";

                                    ctr1 = ctr + i3 + 1;
                                    amount = 0; gst = 0; grand_total = 0;

                                }
                            }
                            amount = amount + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString());
                            gst = gst + double.Parse(ds.Tables[0].Rows[ctr]["GST"].ToString());
                            grand_total = grand_total + double.Parse(ds.Tables[0].Rows[ctr]["Grand Total"].ToString());

                            amount1 = amount1 + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString());
                            gst1 = gst1 + double.Parse(ds.Tables[0].Rows[ctr]["GST"].ToString());
                            grand_total1 = grand_total1 + double.Parse(ds.Tables[0].Rows[ctr]["Grand Total"].ToString());

                            client_name = ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper();
                            // lc = new LiteralControl( "<tr><b><td align=center colspan = 5>Total</td><td>=ROUND(SUM(F3:F" + (ctr + i) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + i) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + i) + "),2)</td></b></tr>");
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Amount"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["GST"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Grand Total"].ToString().ToUpper() + "</td></tr>");
                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>" + amount + "</td><td>" + gst + "</td><td>" + grand_total + "</td></b></tr>";

                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>GRAND TOTAL</td><td>" + amount1 + "</td><td>" + gst1 + "</td><td>" + grand_total1 + "</td></b></tr>";
                            }
                        }


                        if (t == "3")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Amount"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["GST"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Grand Total"].ToString().ToUpper() + "</td></tr>");
                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 6>Total</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),2)</td></b></tr>";
                            }
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
    protected void gst_report_Click(object sender, EventArgs e)
    {
        if (ddl_gst_type.SelectedValue == "ALL")
        {
            all_gst_report("ALL");
        }
        else if (ddl_gst_type.SelectedValue == "1")
        {
            all_gst_report("manpower");
        }
        else if (ddl_gst_type.SelectedValue == "2")
        {
            all_gst_report("conveyance");
        }
        else if (ddl_gst_type.SelectedValue == "3")
        {
            all_gst_report("driver_conveyance");
        }
        else if (ddl_gst_type.SelectedValue == "4")
        {
            all_gst_report("material");
        }
        else if (ddl_gst_type.SelectedValue == "5")
        {
            all_gst_report("deepclean");
        }
        else if (ddl_gst_type.SelectedValue == "6")
        {
            all_gst_report("machine_rental");
        }
        else if (ddl_gst_type.SelectedValue == "7")
        {
            all_gst_report("arrears_manpower");
        }
        else if (ddl_gst_type.SelectedValue == "8")
        {
            all_gst_report("manual");
        }

    }
    protected void all_gst_report(string type)
    {
        hidtab.Value = "5";
        try
        {
            string where = "";
            string query = "",invoice_flag="";
            string billing_type = "";
            if (type != "ALL")
            {
                billing_type = " and type = '" + type + "'";
            }
            if (ddl_client.SelectedValue != "ALL")
            {
                where = "  and client_code='" + ddl_client.SelectedValue + "' ";

            }
            else if (ddl_state.SelectedValue != "ALL")
            {
                where = where + " and state_name ='" + ddl_state.SelectedValue + "'";

            }
            if (type=="manual")
            {
                invoice_flag = " and final_invoice !='0'";
               
                //if (ddl_client.SelectedValue != "ALL")
                //{
                //    where = " and client_name='" + ddl_client.SelectedItem + "'";

                //}
                //else if (ddl_state.SelectedValue != "ALL")
                //{
                //    where = where + " and state_name ='" + ddl_state.SelectedValue + "'";

                //}
            }

            query = "SELECT DATE_FORMAT(invoice_date, '%d/%m/%Y') AS 'billing_date',type, month, year, invoice_no, client_name, state_name, gst_no,ROUND(amount, 2) AS 'amount', ROUND(cgst, 2) AS 'cgst', ROUND(sgst, 2) AS 'sgst', ROUND(igst, 2)AS 'igst', ROUND(cgst + igst + sgst, 2) AS 'gst', ROUND(cgst + igst + sgst + amount, 2) AS 'Total_BILL' FROM pay_report_gst WHERE  (invoice_no IS NOT NULL  AND invoice_no !='') and comp_code = '" + Session["comp_code"].ToString() + "' and invoice_date between str_to_date('" + gst_from_date.Text + "','%d/%m/%Y') and str_to_date('" + gst_to_date.Text + "','%d/%m/%Y')  " + billing_type + "" + where + invoice_flag + " and (amount is not null ||amount != 0)  order by type";

            MySqlDataAdapter dscmd = new MySqlDataAdapter(query, d.con);
            DataSet ds = new DataSet();

            dscmd.SelectCommand.CommandTimeout = 200;
            dscmd.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GST_Report" + ddl_client.SelectedItem.Text.Replace(" ", "_") + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds);
                Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null);
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

                    lc = new LiteralControl("<table border=1><tr ><th bgcolor=yellow colspan=15>GST Reports</th></tr><tr><th>SR NO.</th><th>Billing Date</th><th>Billing Type</th><th>Month</th><th>Year</th><th>Invoice No</th><th>Client</th><th>State Name</th><th>GST NO.</th><th>Bill Amount</th><th>CGST</th><th>SGST</th><th>IGST</th><th>Total GST</th><th>Total Bill</th></tr> ");

                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["billing_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["month"] + "</td><td>" + ds.Tables[0].Rows[ctr]["year"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_BILL"] + "</td></tr>");
                    if (ds.Tables[0].Rows.Count == ctr + 1)
                    {
                        lc.Text = lc.Text + "<tr><b><td align=center colspan = 9>Total</td><td>=ROUND(SUM(J3:J" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(K3:K" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(L3:L" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(M3:M" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(N3:N" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(O3:O" + (ctr + 3) + "),2)</td></b></tr>";
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
    protected void ddl_get_report_Click(object sender, EventArgs e)
    {
        hidtab.Value = "6";
        if (dll_type.SelectedValue == "1")
        {
            export_account_xl(1);
        }
        if (dll_type.SelectedValue == "2")
        {
            export_account_xl(2);
        }
        if (dll_type.SelectedValue == "3")
        {
            export_account_xl(3);
        }
        if (dll_type.SelectedValue == "4")
        {
            export_account_xl(4);
        }
        if (dll_type.SelectedValue == "5")
        {
            export_account_xl(5);
        }
    }
    protected void export_account_xl(int i)
    {
        hidtab.Value = "6";
        try
        {
            string sql = "";
            string where = "";
            string where1 = "";
            if (ddl_client.SelectedValue != "ALL")
            {
                where = "  and client_code='" + ddl_client.SelectedValue + "' ";
                where1 = " and pay_pro_master.client_code='" + ddl_client.SelectedValue + "' ";
            }
            if (ddl_state.SelectedValue != "ALL")
            {
                where = where + " and state_name ='" + ddl_state.SelectedValue + "'";
                where1 = where1 + " and pay_pro_master.state_name ='" + ddl_state.SelectedValue + "'";
            }
            if (i == 1)
            {
                if (ddl_client.SelectedValue.Equals("RCPL"))
                {
                    sql = "SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, CASE WHEN invoice_flag != 0 AND pay_billing_unit_rate_history.month <= 3 AND pay_billing_unit_rate_history.year <= 2019 THEN IFNULL(auto_invoice_no, invoice_no) ELSE auto_invoice_no END AS 'invoice_no', Amount AS 'Billing_Amount', ROUND(CGST9 + igst18 + sgst9, 2) AS 'gst', ROUND(((SUM(FLOOR(payment - (fine) + (emp_advance_payment) + (emp_advance) + (reliver_advances)))) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'payment', ROUND(((pay_billing_unit_rate_history.pf + (SUM(pay_pro_master.sal_pf)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'PF', ROUND(((pay_billing_unit_rate_history.esic + (SUM(pay_pro_master.sal_esic)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'ESIC', ROUND(((pay_billing_unit_rate_history.lwf + (SUM(pay_pro_master.lwf_salary)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'LWF', ROUND((SUM(pay_pro_master.pt_amount) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'PT', ROUND(((SUM(pay_billing_unit_rate_history.bonus_after_gross) + SUM(pay_billing_unit_rate_history.leave_after_gross) + SUM(pay_billing_unit_rate_history.gratuity_after_gross)) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'Others' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.client_code = pay_pro_master.client_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year AND payment_status = 1 WHERE pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' AND billing_date BETWEEN STR_TO_DATE('" + acc_from_date.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + acc_to_date.Text + "', '%d/%m/%Y') AND (auto_invoice_no IS NOT NULL OR invoice_no IS NOT NULL) GROUP BY invoice_no, auto_invoice_no order by billing_date";
                }
                else
                {
                    sql = "SELECT pay_pro_master.client,  pay_pro_master.state_name,CASE WHEN invoice_flag != 0 AND pay_billing_unit_rate_history.month <= 3 AND pay_billing_unit_rate_history.year <= 2019 THEN IFNULL(auto_invoice_no, invoice_no) ELSE auto_invoice_no END AS 'invoice_no', floor((sum(pay_billing_unit_rate_history.amount) + IF(bill_ser_uniform = 0, ((sum(pay_billing_unit_rate.uniform) / sum(pay_billing_unit_rate.month_days)) * sum(pay_billing_unit_rate_history.tot_days_present)), 0) + IF(bill_ser_operations = 0, ((sum(pay_billing_unit_rate.operational_cost) / sum(pay_billing_unit_rate.month_days)) * sum(pay_billing_unit_rate_history.tot_days_present)), 0) + sum(Service_charge) + SUM(IFNULL(pay_conveyance_amount_history.conveyance_rate, pay_billing_unit_rate_history.conveyance_amount) * tot_days_present / pay_billing_unit_rate_history.month_days) + sum(pay_billing_unit_rate_history.ot_amount))) AS 'Billing_Amount', (case when  pay_client_master.gst_applicable = 1 then ROUND((SUM(CGST9) + SUM(igst18) + SUM(sgst9)), 2) else 0 end) AS 'gst', (SUM(FLOOR(payment - (fine)  + (emp_advance_payment) + (emp_advance) + (reliver_advances)))) AS 'payment', round((sum(pay_billing_unit_rate_history.pf) + sum(pay_pro_master.sal_pf)),2) as PF, round((sum(pay_billing_unit_rate_history.esic) + sum(pay_pro_master.sal_esic)),2) as ESIC, round((sum(pay_billing_unit_rate_history.lwf) + sum(pay_pro_master.lwf_salary)),2) as lwf, round(sum(pay_pro_master.pt_amount),2) as PT, round((sum(pay_billing_unit_rate_history.bonus_after_gross)+sum(pay_billing_unit_rate_history.leave_after_gross)+sum(pay_billing_unit_rate_history.gratuity_after_gross)),2) as Others FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year INNER JOIN pay_billing_unit_rate ON pay_billing_unit_rate_history.comp_code = pay_billing_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_billing_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_billing_unit_rate.month AND pay_billing_unit_rate_history.year = pay_billing_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_billing_unit_rate.designation INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate_history.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate_history.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate_history.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate_history.month AND pay_billing_master_history.year = pay_billing_unit_rate_history.year AND pay_billing_master_history.designation = pay_billing_unit_rate_history.grade_code AND pay_billing_master_history.hours = pay_billing_unit_rate_history.hours AND pay_billing_master_history.type = 'billing' INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_master.client_code = pay_billing_unit_rate_history.client_code LEFT OUTER JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.emp_code = pay_billing_unit_rate_history.emp_code AND pay_conveyance_amount_history.comp_code = pay_billing_unit_rate_history.comp_code AND pay_conveyance_amount_history.unit_code = pay_billing_unit_rate_history.unit_code AND pay_conveyance_amount_history.month = pay_billing_unit_rate_history.month AND pay_conveyance_amount_history.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' AND billing_date BETWEEN STR_TO_DATE('" + acc_from_date.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + acc_to_date .Text+ "', '%d/%m/%Y') " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) group by auto_invoice_no order by billing_date ";
                    if (ddl_client.SelectedValue.Equals("ALL"))
                    {
                        sql = sql + " union SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, CASE WHEN invoice_flag != 0 AND pay_billing_unit_rate_history.month <= 3 AND pay_billing_unit_rate_history.year <= 2019 THEN IFNULL(auto_invoice_no, invoice_no) ELSE auto_invoice_no END AS 'invoice_no', Amount AS 'Billing_Amount', ROUND(CGST9 + igst18 + sgst9, 2) AS 'gst', ROUND(((SUM(FLOOR(payment - (fine) + (emp_advance_payment) + (emp_advance) + (reliver_advances)))) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'payment', ROUND(((pay_billing_unit_rate_history.pf + (SUM(pay_pro_master.sal_pf)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'PF', ROUND(((pay_billing_unit_rate_history.esic + (SUM(pay_pro_master.sal_esic)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'ESIC', ROUND(((pay_billing_unit_rate_history.lwf + (SUM(pay_pro_master.lwf_salary)) * pay_billing_unit_rate_history.bill_amount / 100)), 2) AS 'LWF', ROUND((SUM(pay_pro_master.pt_amount) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'PT', ROUND(((SUM(pay_billing_unit_rate_history.bonus_after_gross) + SUM(pay_billing_unit_rate_history.leave_after_gross) + SUM(pay_billing_unit_rate_history.gratuity_after_gross)) * pay_billing_unit_rate_history.bill_amount / 100), 2) AS 'Others' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.client_code = pay_pro_master.client_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year AND payment_status = 1 WHERE pay_billing_unit_rate_history.client_code = '" + ddl_client.SelectedValue + "' AND billing_date BETWEEN STR_TO_DATE('" + acc_from_date.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + acc_to_date.Text + "', '%d/%m/%Y') AND (auto_invoice_no IS NOT NULL OR invoice_no IS NOT NULL) GROUP BY invoice_no, auto_invoice_no order by billing_date";
                    }

                }
            }
            else if (i == 2)
            {
                sql = "SELECT client_name, state_name, invoice_no, amount, gst,(amount + gst) AS 'grand_total', payment FROM pay_report_gst WHERE  comp_code  = '" + Session["comp_code"].ToString() + "' and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "'  AND  type = 'conveyance' " + where;
            }
            else if (i == 3)
            {
                sql = "SELECT client_name, state_name, invoice_no, amount, gst,(amount + gst) AS 'grand_total', payment FROM pay_report_gst WHERE   comp_code  = '" + Session["comp_code"].ToString() + "' and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "'  AND  type  = 'driver_conveyance' " + where;
            }
            else if (i == 4)
            {
                sql = "SELECT client_name, state_name, invoice_no, amount, gst,(amount + gst) AS 'grand_total', payment FROM pay_report_gst WHERE  comp_code  = '" + Session["comp_code"].ToString() + "' and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' AND  type  = 'material'  " + where;
            }
            else if (i == 5)
            {
                sql = "SELECT client_name, state_name, invoice_no, amount, gst,(amount + gst) AS 'grand_total', payment FROM pay_report_gst WHERE  comp_code  = '" + Session["comp_code"].ToString() + "' and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "' AND  type  = 'deep clean'  " + where;
            }

            //double Payment = 0, pf = 0, esic = 0, lwf = 0,pt = 0,Others = 0;

            //      MySqlDataAdapter dscmd1 = new MySqlDataAdapter("SELECT pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.state_name, CASE WHEN invoice_flag != 0 AND pay_billing_unit_rate_history.month <= 3 AND pay_billing_unit_rate_history.year <= 2019 THEN IFNULL(auto_invoice_no, invoice_no) ELSE auto_invoice_no END AS 'invoice_no',  SUM(Amount + uniform + operational_cost + conveyance_rate + group_insurance_billing + ot_amount + service_charge) AS 'Billing_Amount', ROUND((SUM(CGST9) + SUM(igst18) + SUM(sgst9)), 2) AS 'gst' FROM pay_billing_unit_rate_history WHERE comp_code  = '" + Session["comp_code"].ToString() + "' and month = '" + txt_date.Text.Substring(0, 2) + "' AND year = '" + txt_date.Text.Substring(3) + "'  AND auto_invoice_no IS NOT NULL " + where + " GROUP BY auto_invoice_no ORDER BY auto_invoice_no", d.con1);

            //DataTable dt = new DataTable();
            //dscmd1.Fill(dt);
            //dt.Columns.Add("payment");
            //dt.Columns.Add("pf");
            //dt.Columns.Add("ESIC");
            //dt.Columns.Add("LWF");
            //dt.Columns.Add("PT");
            //dt.Columns.Add("others");

            //d.con1.Open();

            //    string payment = d.getsinglestring("SELECT (SUM(FLOOR(payment - (fine) + (emp_advance_payment) + (emp_advance) + (reliver_advances)))) AS 'payment' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");
            //    string PF = d.getsinglestring("SELECT ROUND((SUM(pay_billing_unit_rate_history.pf) + SUM(pay_pro_master.sal_pf)), 2) AS 'PF' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");
            //    string ESIC = d.getsinglestring("SELECT ROUND((SUM(pay_billing_unit_rate_history.esic) + SUM(pay_pro_master.sal_esic)), 2) AS 'ESIC' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");
            //    string LWF = d.getsinglestring("SELECT  ROUND((SUM(pay_billing_unit_rate_history.lwf) + SUM(pay_pro_master.lwf_salary)), 2) AS 'lwf' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");
            //    string PT = d.getsinglestring("SELECT  ROUND(SUM(pay_pro_master.pt_amount), 2) AS 'PT' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");
            //    string others = d.getsinglestring("SELECT ROUND((SUM(pay_billing_unit_rate_history.bonus_after_gross) + SUM(pay_billing_unit_rate_history.leave_after_gross) + SUM(pay_billing_unit_rate_history.gratuity_after_gross)), 2) AS 'Others' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year WHERE  pay_pro_master.comp_code  = '" + Session["comp_code"].ToString() + "' and pay_pro_master.month = '" + txt_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_date.Text.Substring(3) + "'  " + where1 + " AND (pay_pro_master.start_date = 0 AND pay_pro_master.end_date = 0) AND (pay_billing_unit_rate_history.start_date = 0 AND pay_billing_unit_rate_history.end_date = 0) GROUP BY auto_invoice_no ORDER BY auto_invoice_no");

            //    //data will not found then return
            //    if (payment.Equals(""))
            //    {
            //        d.con1.Close();
            //        return;
            //    }
            //    MySqlCommand cmd_cg = new MySqlCommand("Select comp_name,IFNULL(percent,0) as percent,Companyname_gst_no,gst_address from pay_company_group where comp_code ='" + Session["COMP_CODE"].ToString() + "' " + where, d1.con1);
            //    d1.con1.Open();
            //    MySqlDataReader dr_cg = cmd_cg.ExecuteReader();
            //    while (dr_cg.Read())
            //    {
            //        Payment = (double.Parse(payment) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;
            //        pf = (double.Parse(PF) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;
            //        esic = (double.Parse(ESIC) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;
            //        lwf = (double.Parse(LWF) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;
            //        pt = (double.Parse(PT) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;
            //        Others = (double.Parse(others) * double.Parse(dr_cg.GetValue(1).ToString())) / 100;

            //        // dt.Tables[0].Rows.Add(dscmd1);
            //        //DataRow dr = callsTable.NewRow(); //Create New Row
            //        //dr["Call"] = "Legs";              // Set Column Value
            //        //callsTable.Rows.InsertAt(dr, 11)
            //        DataRow _pay = dt.NewRow();
            //        _pay["payment"] = Payment;
            //        _pay["pf"] = pf;
            //        _pay["ESIC"] = esic;
            //        _pay["LWF"] = lwf;
            //        _pay["PT"] = pt;
            //        _pay["others"] = Others;
            //      //  dt.[0].Rows.Add(_pay);
            //        dt.Rows.InsertAt(_pay, 0);
            //    }
            //    dr_cg.Dispose();
            //    dr_cg.Close();
            //    d1.con1.Close();

            MySqlCommand cmd_cg = new MySqlCommand(sql, d.con);
            cmd_cg.CommandTimeout = 300;
            MySqlDataAdapter dscmd = new MySqlDataAdapter(cmd_cg);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;

                if (i == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=MANPOWER_BALANCE_SHEET.xls");
                }
                else if (i == 2)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=CONVEYANCE_BALANCE_SHEET.xls");
                }

                else if (i == 3)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=DRIVER_CONVEYANCE_BALANCE_SHEET.xls");
                }
                else if (i == 4)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=MATERIAL_BALANCE_SHEET.xls");
                }
                else if (i == 5)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=DEEP_CLEAN_BALANCE_SHEET.xls");
                }
                string date1 = txt_date.Text;
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds, i, date1);
                Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds, i, date1);
                Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, ds, i, date1);
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
    public class MyTemplate2 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;
        string date1;


        public MyTemplate2(ListItemType type, DataSet ds, int i, string date1)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.date1 = date1;
            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }
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
        public void InstantiateIn(Control container)
        {
            switch (type)
            {

                case ListItemType.Header:

                    var current_date = date1;
                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=13>MANPOWER BALANCE SHEET</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Invoice No</th><th>BASE AMOUNT</th><th>GST</th><th>BILLING AMOUNT </th><th>EMPLOYEE Payment</th><th>PF (EMPLOYER + EMPLOYEE)</th><th>ESIC (EMPLOYER + EMPLOYEE)</th><th>LWF</th><th>PT</th><th>OTHER(BONUS + LEAVE + GRADUITY)</th></tr> ");

                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=8> CONVEYANCE BALANCE SHEET  " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Invoice No</th><th>SUB TOTAL</th><th>Total GST</th><th>GRAND TOTAL</th><th>Total Payment</th></tr> ");

                    }
                    else if (i == 3)
                    {
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=8>DRIVER CONVEYANCE BALANCE SHEET  " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Invoice No</th><th>SUB TOTAL</th><th>Total GST</th><th>GRAND TOTAL</th><th>Total Payment</th></tr> ");

                    }
                    else if (i == 4)
                    {
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=8>MATERIAL BALANCE SHEET  " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Invoice No</th><th>SUB TOTAL</th><th>Total GST</th><th>GRAND TOTAL</th><th>Total Payment</th></tr> ");

                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=8>DEEP CLEAN BALANCE SHEET  " + getmonth(current_date.Substring(0, 2)) + " " + current_date.Substring(3) + "</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Invoice No</th><th>SUB TOTAL</th><th>Total GST</th><th>GRAND TOTAL</th><th>Total Payment</th></tr> ");

                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {

                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr][0].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][1].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][2].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][3].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][4].ToString().ToUpper() + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr][3].ToString()) + double.Parse(ds.Tables[0].Rows[ctr][4].ToString()), 2) + "</td><td>" + ds.Tables[0].Rows[ctr][5].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][6].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][7].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][8].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][9].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][10].ToString().ToUpper() + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td><td>=ROUND(SUM(E3:E" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(J3:J" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(K3:K" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(L3:L" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(M3:M" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grand_total"] + "</td><td>" + ds.Tables[0].Rows[ctr]["payment"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td><td>=ROUND(SUM(E3:E" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 3)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grand_total"] + "</td><td>" + ds.Tables[0].Rows[ctr]["payment"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td><td>=ROUND(SUM(E3:E" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 4)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grand_total"] + "</td><td>" + ds.Tables[0].Rows[ctr]["payment"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td><td>=ROUND(SUM(E3:E" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grand_total"] + "</td><td>" + ds.Tables[0].Rows[ctr]["payment"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td><td>=ROUND(SUM(E3:E" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td></b></tr>";
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
}