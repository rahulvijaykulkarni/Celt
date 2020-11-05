using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Threading;
using System.Net.Mail;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;

public partial class Employee_salary_details : System.Web.UI.Page
{
    ReportDocument crystalReport = new ReportDocument();
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    string where11 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_download.Visible = false;
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
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
                ddl_state.Items.Insert(0, "ALL");
                ddl_unitcode.Items.Insert(0, "ALL");
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                left_employee_reliving_latter(1);
            }

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
                ddl_unitcode.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                left_employee_reliving_latter(2);
            }
        }
    }
    protected void btn_send_email_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        create_letter_pdf("", 1);
    }
    private void create_letter_pdf(string unit_code, int counter1)
    {
        try
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            string unit_code_join1 = null;
            string unit_code1 = "";
            string where1 = " client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "'";
            if (ddl_unitcode.SelectedValue != "ALL")
            {
                unit_code_join1 = "" + ddl_unitcode.SelectedValue + "";
            }
            else
            {
                unit_code_join1 = d.getsinglestring("select group_concat( unit_code) from pay_unit_master where " + where1 + " ");
                unit_code_join1 = unit_code_join1.Replace(",", "','");
                //where = where + " and unit_code in('" + unit + "')";
            }

            string emp1 = d.getsinglestring("select count(emp_code) from pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code in('" + unit_code_join1 + "') AND (`left_date` IS NULL) and joining_letter_email='1' and legal_flag='2'");

            string emp2 = d.getsinglestring("select count(emp_code) from pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code in('" + unit_code_join1 + "')  AND (`left_date` IS NULL)");

            if (emp1 == emp2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('joining Letter is Already Send!!');", true);
                return;
            }

            string where = "client_code = '" + ddl_client.SelectedValue + "'";
            if (unit_code == "")
            {
                if (ddl_state.SelectedValue != "ALL") { where = where + " and state_name = '" + ddl_state.SelectedValue + "'"; }
                if (ddl_unitcode.SelectedValue != "ALL") 
                { 
                    //where = where + " and unit_code = '" + ddl_unitcode.SelectedValue + "'"; 
                    unit_code1= "'" + ddl_unitcode.SelectedValue + "'";
                
                }

                if (ddl_unitcode.SelectedValue == "ALL")
                {
                    string unit = null;
                    unit = d.getsinglestring("select group_concat( unit_code) from pay_unit_master where " + where + " ");
                    unit = unit.Replace(",", "','");

                    // comment 24/12 where = where + " and unit_code in('" + unit + "')";
                    unit_code1 = "'" + unit + "'";
                }

            }
            else
            {
               // comment 24/12  where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code + "'";
                where = "comp_code = '" + Session["COMP_CODE"].ToString() + "'";
                unit_code1 = "'" + unit_code + "'";
            }
            // var foos = "Foo1,Foo2,Foo3";
            var unit_Array = unit_code1.Split(',');
            //string[] all_unit = unit_code1;
            //all_unit = all_unit.splite(',');
            foreach (string obj in unit_Array)
            {


                d.con1.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT comp_code, unit_code FROM pay_unit_master WHERE branch_status = 0 and  unit_code=" + obj + " and "  + where, d.con1);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ReportDocument crystalReport = new ReportDocument();
                    string query = null;

                    string leftfooterpath = null;
                    string stamppath = null;
                    if (Session["COMP_CODE"].ToString() == "C02")
                    {
                        crystalReport.Load(Server.MapPath("~/joining_letter.rpt"));
                        leftfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer1.png");
                        stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
                    }
                    else
                    {
                        if (ddl_client.SelectedValue == "BAGIC_OC")
                        {
                        crystalReport.Load(Server.MapPath("~/joining_letter_oc1.rpt"));
                        leftfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer1.png");
                        stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
                        }
                        else
                        {
                        crystalReport.Load(Server.MapPath("~/joining_letter1.rpt"));
                        leftfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer1.png");
                        stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
                        }
                    }
                    leftfooterpath = leftfooterpath.Replace("\\", "\\\\");
                    stamppath = stamppath.Replace("\\", "\\\\");
                    string companyimagepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\") + d.getsinglestring("SELECT comp_logo from pay_company_master where comp_code = '" + dr.GetValue(0).ToString() + "'");
                    crystalReport.DataDefinition.FormulaFields["company_image_path"].Text = @"'" + companyimagepath + "'";
                    //comment 24/12
                    //string unit_code_join = null;
                    //if (ddl_unitcode.SelectedValue != "ALL")
                    //{
                    //    unit_code_join = "" + ddl_unitcode.SelectedValue + "";
                    //}
                    //else
                    //{
                    //    unit_code_join = d.getsinglestring("select group_concat( unit_code) from pay_unit_master where " + where + " ");
                    //    unit_code_join = unit_code_join.Replace(",", "','");
                    //    //where = where + " and unit_code in('" + unit + "')";
                    //}
                    query = "SELECT concat('THIS IS TO BE INFORM YOU THAT WE ARE ',pay_company_master.company_name,', @@@@@@@@EMPLOYEE NAME - ',pay_employee_master.emp_name,'  @@@@@@@@EMPLOYEE ID - ',pay_employee_master.id_as_per_dob,' @@@@@@@@DESIGNATION - ',pay_grade_master.grade_desc,' @@@@@@@@DEPLOYED CLIENT & ADDRESS - ',pay_client_master.client_name,' ',pay_unit_master.unit_add2,'. @@@@@@@@JOINING DATE -  ',upper(DATE_FORMAT(pay_employee_master.joining_date, '%D %b %Y'))) as 'rightfooterpath', pay_employee_master.emp_name,pay_grade_master.grade_desc, pay_client_master.client_name, pay_unit_master.unit_name, pay_unit_master.unit_add2, pay_employee_master.id_as_per_dob AS 'ihmscode', DATE_FORMAT(pay_employee_master.joining_date, '%d-%m-%Y') AS 'joining_date', upper(pay_company_master.company_name) as company_name, upper(pay_company_master.address1) as address1, pay_company_master.city, pay_company_master.state, pay_company_master.company_contact_no, pay_company_master . address1  as  address ,'" + leftfooterpath + "' as 'leftfooterpath' ,'" + stamppath + "' as 'stappath',CONCAT(UPPER( pay_company_master . company_name ), ',',  pay_company_master . address1 , ',',  pay_company_master . city , ',',  pay_company_master . state ) AS 'emp_name' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code where  pay_unit_master.unit_code in(" + obj + ") and pay_employee_master.comp_code='" + dr.GetValue(0).ToString() + "' and pay_employee_master.employee_type IN ('Permanent') and joining_letter_email =0 and left_date is null and pay_employee_master.legal_flag=2";

                    System.Data.DataTable dt = new System.Data.DataTable();
                    MySqlCommand cmd1 = new MySqlCommand(query);
                    MySqlDataReader sda = null;
                    cmd1.Connection = d.con;
                    d.con.Open();
                    sda = cmd1.ExecuteReader();
                    dt.Load(sda);
                    d.con.Close();
                    string body = "";
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Hi, <p>Please find the attached Joining Letter for the following employees. </FONT></FONT></FONT></B><p><Table border =1><tr><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>SR. No.</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>EMPLOYEE NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>DESIGNATION</FONT></FONT></FONT></th></tr>";
                        foreach (DataRow row in dt.Rows)
                        {
                            body = body + "<tr><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + ++i + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["EMP_NAME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["grade_desc"].ToString() + "</FONT></FONT></FONT></td></tr>";
                        }
                        body = body + "</Table> <p>";
                        crystalReport.Refresh();
                        crystalReport.SetDataSource(dt);
                        if (ddl_client.SelectedValue == "BAGIC_OC")
                        {
                            crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter_oc.pdf");

                        }
                        else 
                        {
                            crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf");
                        
                        }

                        crystalReport.Close();
                        crystalReport.Clone();
                        crystalReport.Dispose();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        try
                        {
                            d.con.Open();
                            MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;SELECT cast(group_concat(distinct head_email_id) as char), head_name, pay_branch_mail_details.client_code, pay_branch_mail_details.comp_code, pay_branch_mail_details.state,pay_unit_master.zone,pay_zone_master.field5 FROM pay_branch_mail_details inner join pay_unit_master on pay_unit_master.comp_code = pay_branch_mail_details.comp_code and pay_unit_master.unit_code = pay_branch_mail_details.unit_code left outer join pay_zone_master on pay_zone_master.comp_code = pay_unit_master.comp_code and pay_zone_master.client_code = pay_unit_master.client_code and pay_zone_master.Region = pay_unit_master.zone and type = 'REGION' where pay_branch_mail_details.comp_code = '" + dr.GetValue(0).ToString() + "' and pay_branch_mail_details.unit_code = '" + dr.GetValue(1).ToString() + "'", d.con);
                            MySqlDataReader drnew = cmdnew.ExecuteReader();
                            System.Data.DataTable DataTable1 = new System.Data.DataTable();
                            DataTable1.Load(drnew);
                            d.con.Close();
                            if (!IsEmptyGrid(DataTable1))
                            {
                                foreach (DataRow row in DataTable1.Rows)
                                {
                                    mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "IH&MS - Joining Letters", ddl_state.SelectedValue, dr.GetValue(1).ToString(), counter1, body, row[6].ToString());
                                }
                            }
                        }
                        catch (Exception ex) { throw ex; }
                        finally { d.con.Close(); }
                    }
                }
                d.con1.Close();
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            d.con1.Close();
            if (ddl_client.SelectedValue == "BAGIC_OC")
            {
                  File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter_oc.pdf");
            
            }
            else
            {
            File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf");
            
            }
        }
    }
    private Boolean IsEmptyGrid(DataTable datatable)
    {
        for (int i = 0; i < datatable.Rows.Count; i++)
        {
            for (int j = 0; j < datatable.Columns.Count; j++)
            {
                if (!string.IsNullOrEmpty(datatable.Rows[i][j].ToString()))
                    return false;
            }
        }
        return true;
    }
    protected void mail_send(string head_email_id, string head_email_name, string client_name, string comp_code, int counter, string subject, string state_name, string unit_code, int counter1, string body1, string h_email_id)
    {
        List<string> list1 = new List<string>();
        string from_emailid = "", password = "";
        try
        {

            where11 = "";
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where client_code = '" + client_name + "' ", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                from_emailid = dr.GetValue(0).ToString();
                password = dr.GetValue(1).ToString();
            }
            dr.Close();
            d.con.Close();
            if (!(from_emailid == "") || !(password == ""))
            {
                string body = body1;
                string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Admin' and client_code='" + client_name + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />" + name + "";

                //if (client_name == "BALIC")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />Santosh Ghurade<br />Admin and OPS<br />Mobile - 9325431471<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "BAGIC")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />Samiksha<br />Admin and OPS<br />Mobile - 9067159872<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "MAX" || client_name == "AEG" || client_name == "5" || client_name == "7" || client_name == "8" || client_name == "ICICI HK" || client_name == "ESFB" || client_name == "TBZ")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />SNEHAL GHADGE<br />Admin and OPS<br />Mobile - 8308925811<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "RLIC HK" || client_name == "RCFL" || client_name == "RCPL")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />CHAITALI<br />Admin and OPS<br />Mobile - 8805814003<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "SUD" || client_name == "UTKARSH" || client_name == "HDFC" || client_name == "TAVISKA" || client_name == "SUN" || client_name == "DAF" || client_name == "TBML" || client_name == "BRLI")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />SNEHAL GHADGE<br />Admin and OPS<br />Mobile - 8308925811<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "4" || client_name == "RBL")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />RAHUL<br />Admin and OPS<br />Mobile - 7057919614<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                using (MailMessage mailMessage = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mailMessage.From = new MailAddress(from_emailid);

                    if (head_email_id != "")
                    {

                        mailMessage.To.Add(head_email_id);

                        if (!h_email_id.Equals(""))
                        {
                            mailMessage.CC.Add(h_email_id);
                        }
                        mailMessage.CC.Add("kpatel@ihms.co.in");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        if (counter1 == 1)
                        {
                            if(ddl_client.SelectedValue=="BAGIC_OC")
                            {
                                mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter_oc.pdf"));
                            }
                            else
                            {
                                 mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf"));
                            }
                        }

                        mailMessage.IsBodyHtml = true;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                        SmtpServer.EnableSsl = true;
                        try
                        {
                            SmtpServer.Send(mailMessage);
                            if (counter1 == 1)
                            {
                                //string unit_code1 = null;
                                //string where1 = " client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "'";
                                //if (ddl_unitcode.SelectedValue != "ALL")
                                //{
                                //    unit_code1 = "" + ddl_unitcode.SelectedValue + "";
                                //}
                                //else
                                //{
                                //    unit_code1 = d.getsinglestring("select group_concat( unit_code) from pay_unit_master where " + where1 + " ");
                                //    unit_code1 = unit_code1.Replace(",", "','");
                                //    //where = where + " and unit_code in('" + unit + "')";
                                //}
                                d.operation("update pay_employee_master set joining_letter_email =1, joining_letter_sent_date = now() where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code='"+unit_code+"'  AND employee_type IN ('Permanent') AND `pay_employee_master`.`legal_flag` = '2' and left_date is null ");
                            }
                            else if (counter1 == 2)
                            {
                                d.operation("insert into client_feedback (comp_code, client_code, unit_code, month, year) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client.SelectedValue + "','" + unit_code + "','" + txt_monthyear.Text.Substring(0, 2) + "','" + txt_monthyear.Text.Substring(3) + "')");
                            }

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Sent successfully!!');", true);
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error in Sending Email !!');", true);

                        }
                        Thread.Sleep(500);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
        }

    }

    protected void btn_emails_not_sent_Click(object sender, EventArgs e)
    {
        
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        string where = "pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'";
        string unit_code1 = "";
        if (ddl_state.SelectedValue != "ALL") { where = where + " and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "'"; }
        //if (ddl_unitcode.SelectedValue != "ALL") { where = where + " and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'"; }

        if (ddl_unitcode.SelectedValue == "ALL")
        {
            string unit = null;
            unit = d.getsinglestring("select group_concat( unit_code) from pay_unit_master where " + where + " ");
            unit = unit.Replace(",", "','");

            // comment 24/12 where = where + " and unit_code in('" + unit + "')";
            unit_code1 = "" + unit + "";
        }
        else
        {
            unit_code1 =  "" + ddl_unitcode.SelectedValue + "";
            //unit_code1 = where;
        }
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT (SELECT client_name FROM pay_client_master WHERE pay_client_master.client_code = pay_employee_master.client_code ) as 'client_code', pay_unit_master.state_name, unit_name, emp_name , IF( joining_letter_email = 0, 'NO', 'YES') AS 'joining_letter_email', DATE_FORMAT( joining_letter_sent_date , '%d/%m/%Y') AS 'joining_letter_sent_date' FROM pay_employee_master inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code and pay_unit_master.comp_code = pay_employee_master.comp_code WHERE " + where + "   AND `pay_employee_master`.`unit_code` IN ('"+unit_code1+"') and left_date is null and pay_employee_master.legal_flag=2  AND `employee_type` IN ('Permanent') order by 1,2,3,4", d.con1);
        try
        {
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            gv_itemslist.DataSource = DS1;
            gv_itemslist.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }
    protected void gv_itemslist_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemslist.UseAccessibleHeader = false;
            gv_itemslist.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void btn_send_feedback_link_Click(object sender, System.EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            string where = "client_code = '" + ddl_client.SelectedValue + "'";
            string where1 = where;
            if (ddl_state.SelectedValue != "ALL") { where = where + " and state_name = '" + ddl_state.SelectedValue + "'"; }
            if (ddl_unitcode.SelectedValue != "ALL")
            {
                where = where + " and unit_code = '" + ddl_unitcode.SelectedValue + "'";
                where1 = where1 + " and unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }
            d.con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT comp_code, unit_code FROM pay_unit_master WHERE unit_code not in (select unit_code from client_feedback where " + where1 + " and month=" + txt_monthyear.Text.Substring(0, 2) + " and year='" + txt_monthyear.Text.Substring(3) + "' ) and " + where, d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    string body = "";

                    d.con.Open();
                    MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;select cast(group_concat(distinct head_email_id) as char), head_name, client_code,comp_code,state from pay_branch_mail_details where comp_code = '" + dr.GetValue(0).ToString() + "' and unit_code = '" + dr.GetValue(1).ToString() + "'", d.con);
                    MySqlDataReader drnew = cmdnew.ExecuteReader();
                    System.Data.DataTable DataTable1 = new System.Data.DataTable();
                    DataTable1.Load(drnew);
                    d.con.Close();
                    if (!IsEmptyGrid(DataTable1))
                    {
                        foreach (DataRow row in DataTable1.Rows)
                        {
                            //body = "Respected <b>" + row[1].ToString() + "</b>,<p>Thank you for using our services. We would like it if you could take two minutes to give us some feedback and share your input. <p>Please click <b><button><a href=http://ihms.biz/branch_feedback.aspx?A=" + dr.GetValue(0).ToString() + "&B=" + dr.GetValue(1).ToString() + "><span>here</span></a></button></b> for feedback.<p>";
                            body = "Respected <b>" + row[1].ToString() + "</b>,<p>Thank you for using our services. We would like it if you could take two minutes to give us some feedback and share your input. <p>Please click <b><button><a href=http://ihms.biz/branch_feedback.aspx?A=" + dr.GetValue(0).ToString() + "&B=" + dr.GetValue(1).ToString() + "&C=" + txt_monthyear.Text.Substring(0, 2) + "&D=" + txt_monthyear.Text.Substring(3) + "><span>here</span></a></button></b> for feedback.<p>";
                            //body = "Respected <b>" + row[1].ToString() + "</b>,<p>Thank you for using our services. We would like it if you could take two minutes to give us some feedback and share your input. <p>Please click <b><button><a href=http://localhost:52207/CeltPayroll/branch_feedback.aspx?A=" + dr.GetValue(0).ToString() + "&B=" + dr.GetValue(1).ToString() + "&C=" + txt_monthyear.Text.Substring(0, 2) + "&D=" + txt_monthyear.Text.Substring(3) + "><span>here</span></a></button></b> for feedback.<p>";

                            mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "IH&MS - Feedback Request", ddl_state.SelectedValue, dr.GetValue(1).ToString(), 2, body, "");
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }

            }
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            d.con1.Close();
        }

    }
    protected void btn_get_report_Click(object sender, System.EventArgs e)
    {
        hidtab.Value = "1";
        btn_download.Visible = true;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        string where = " and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'";
        if (ddl_state.SelectedValue != "ALL") { where = where + " and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "'"; }
        if (ddl_unitcode.SelectedValue != "ALL") { where = where + " and pay_unit_master.unit_code = '" + ddl_unitcode.SelectedValue + "'"; }

        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select pay_unit_master.state_name, client_feedback.month,client_feedback.year, pay_unit_master.unit_name, if(client_feedback.unit_code is null,'NO','YES') as email_sent, round((feedback1+ feedback2+ feedback3+ feedback4+ feedback5)/5,0) as percent from pay_unit_master left join client_feedback on pay_unit_master.unit_code = client_feedback.unit_code and pay_unit_master.comp_code = client_feedback.comp_code where client_feedback.month = '" + txt_monthyear.Text + "'" + where, d.con1);
        try
        {
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            grd_feedback.DataSource = DS1;
            grd_feedback.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    protected void grd_feedback_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[2].Text = getmonth(e.Row.Cells[2].Text);

    }
    protected void grd_feedback_PreRender(object sender, System.EventArgs e)
    {
        try
        {
            grd_feedback.UseAccessibleHeader = false;
            grd_feedback.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
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
        return month;

    }
    protected void left_employee_reliving_latter(int i)
    {
       // hidtab.Value = "2";
        d1.con1.Open();
        ddl_left_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        MySqlDataAdapter cmd_item = null;
        if (i == 1)
        {
            cmd_item = new MySqlDataAdapter("select  emp_code,emp_name  from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' and ( LEFT_DATE  !='' or  LEFT_DATE  is not null) and employee_type='Permanent'  ", d1.con1);
        }
        else if (i == 2)
        {
            cmd_item = new MySqlDataAdapter("select  emp_code,emp_name  from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' and ( LEFT_DATE  !='' or  LEFT_DATE  is not null) and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent' ", d1.con1);
        }
        else if (i == 3)
        {
            cmd_item = new MySqlDataAdapter("select  emp_code,emp_name  from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' and ( LEFT_DATE  !='' or  LEFT_DATE  is not null) and client_wise_state='" + ddl_state.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "' and employee_type='Permanent'", d1.con1);
        }

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_left_employee.DataSource = dt_item;
                ddl_left_employee.DataTextField = dt_item.Columns[1].ToString();
                ddl_left_employee.DataValueField = dt_item.Columns[0].ToString();
                ddl_left_employee.DataBind();
            }
            dt_item.Dispose();
            d1.con1.Close();
            ddl_left_employee.Items.Insert(0, "Select");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();

        }

    }
    protected void ddl_left_employee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        left_employee_reliving_latter(3);
    }
    protected void btn_releiving_letter_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {

            string unit_code1 = null;

            //Creating PDF
            string downloadname = "Relieving_Letter_" + ddl_left_employee.SelectedValue;
            crystalReport.Load(Server.MapPath("~/relive_letter.rpt"));
            string headerimagepath1 = null;
            string footerimagepath1 = null;
            string footerimagepath2 = null;
            // string stamppath = null;
            if (Session["COMP_CODE"].ToString() == "C02")
            {
                // crystalReport.Load(Server.MapPath("~/relive_letter.rpt"));  C02_stamp.png
                headerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header.png");
                footerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
                footerimagepath2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath1"].Text = @"'" + footerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath2"].Text = @"'" + footerimagepath2 + "'";
            }
            else
            {
                // crystalReport.Load(Server.MapPath("~/relive_letter.rpt")); C01_stamp.JPG 
                headerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header.png");
                footerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.JPG");
                footerimagepath2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath1"].Text = @"'" + footerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath2"].Text = @"'" + footerimagepath2 + "'";
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT a.comp_code,a.EMP_NAME,date_format(a.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',a.EMP_CODE,pay_grade_master.GRADE_DESC AS GRADE_CODE, date_format(a.LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE',(select emp_name from pay_employee_master b where b.emp_code = a.REPORTING_TO) as REPORTING_TO, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2,pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE,pay_company_master.COMPANY_CONTACT_NO as COMPANY_CONTACT_NO FROM pay_employee_master a INNER JOIN pay_company_master ON a.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON a.GRADE_CODE =pay_grade_master.GRADE_CODE WHERE a.EMP_CODE='" + ddl_left_employee.SelectedValue + "'AND a.comp_code='" + Session["comp_code"].ToString() + "' and a.employee_type = 'Permanent'");
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            try
            {
                sda = cmd.ExecuteReader();
                dt.Load(sda);
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Should be Permanent!!');", true);
                return;
            }
            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
            crystalReport.Close();
            crystalReport.Clone();
            crystalReport.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Sending Email
            string body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir/Madam, <p>Please find the attached Releiving letter of " + ddl_left_employee.SelectedItem.Text + ". </FONT></FONT></FONT></B><p>";
            
            System.Data.DataTable DataTable1 = new System.Data.DataTable();
            try
            {

                d2.con.Open();

                if (ddl_unitcode.SelectedValue=="ALL")
                {
                unit_code1 = d.getsinglestring("select group_concat(UNIT_CODE) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "'");
                }
                else
                    if (ddl_unitcode.SelectedValue != "ALL")
                {
                
                      unit_code1 = ddl_unitcode.SelectedValue;
                }

                unit_code1 = "'" + unit_code1 + "'";
                unit_code1 = unit_code1.Replace(",", "','");

                MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;select cast(group_concat(distinct head_email_id) as char), head_name, client_code,comp_code,state from pay_branch_mail_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code IN (" + unit_code1 + ")", d2.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                DataTable1.Load(drnew);
                d2.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d2.con.Close();
            }
            foreach (DataRow row in DataTable1.Rows)
            {
                if (!row[0].ToString().Equals(""))
                {
                    //  mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "IH&MS - Salary Slip of " + dr.GetValue(4).ToString() + " for the month of " + month_year, dr.GetValue(5).ToString(), dr.GetValue(1).ToString(), month, year, counter1, body);
                    string from_emailid = "";
                    string password = "";
                    d.con.Open();
                    try
                    {
                        cmd = new MySqlCommand("select email_id,password from pay_client_master where client_code = '" + row[2].ToString() + "' ", d.con);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            from_emailid = dr.GetValue(0).ToString();
                            password = dr.GetValue(1).ToString();
                        }
                        dr.Close();
                        d.con.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con.Close(); }
                    if (!(from_emailid == "") || !(password == ""))
                    {
                        string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Admin' and client_code='" + row[2].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                        body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />" + name + "";


                        using (MailMessage mailMessage = new MailMessage())
                        {
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            mailMessage.From = new MailAddress(from_emailid);

                            if (row[0].ToString() != "")
                            {
                                mailMessage.To.Add(row[0].ToString());
                                mailMessage.Subject = "IH&MS - Relieving Letter of " + ddl_left_employee.SelectedItem.Text;
                                mailMessage.Body = body;
                                mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf"));
                                mailMessage.IsBodyHtml = true;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                                SmtpServer.EnableSsl = true;
                                try
                                {
                                    SmtpServer.Send(mailMessage);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Relieving Email Sent successfully!!');", true);
                                    //reason_panel.Visible = false;
                                }
                                catch
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Relieving Email Not Sent!!');", true);
                                }
                                // File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_download_Click(object sender, EventArgs e)
    {
        try
        {

            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT `pay_client_master`.`client_name`,`pay_unit_master`.`state_name`,CONCAT(`client_feedback`.`month`, '/', `client_feedback`.`year`) AS 'Month',CONCAT(`pay_unit_master`.`unit_add1`, ',', `pay_unit_master`.`unit_city`, ',', `pay_unit_master`.`state_name`) AS 'location',ROUND((`feedback1`+`feedback2`+`feedback3`+`feedback4`+`feedback5`) / 5, 0) AS 'percent' FROM `pay_unit_master` LEFT JOIN `client_feedback` ON `pay_unit_master`.`unit_code` = `client_feedback`.`unit_code` AND `pay_unit_master`.`comp_code` = `client_feedback`.`comp_code` INNER JOIN `pay_client_master` ON `pay_unit_master`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_unit_master`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_unit_master`.`client_code` = '" + ddl_client.SelectedValue + "' AND `month` = '" + txt_monthyear.Text + "' AND `pay_unit_master`.`state_name` = '" + ddl_state.SelectedValue + "' AND `feedback1` IS NOT NULL AND `feedback2` IS NOT NULL AND `feedback3` IS NOT NULL AND `feedback4` IS NOT NULL AND `feedback5` IS NOT NULL", d.con1);
            DataTable ds1 = new DataTable();
            DataSet ds = new DataSet();
            d.con1.Open();
            MySqlDataAdapter1.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Feedback" + ddl_unitcode.SelectedItem.Text.Replace(" ", "_") + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, 1);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, 1);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, 1);
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
        catch (Exception ex)
        { }
        finally
        {
        }

    }

    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;


        public MyTemplate(ListItemType type, DataSet ds, int i)
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

                    lc = new LiteralControl("<table border=1><tr><th>SR No.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>LOCATION</th><th>Month</th><th>percent</th></tr>");
                    break;
                case ListItemType.Item:

                   
                    
                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["location"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Month"] + "</td><td>" + ds.Tables[0].Rows[ctr]["percent"] + "</td></tr>");
                    
                     if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan= 5>Total</td><td>=SUM(F2:F" + (ctr + 2) + ")</td></tr>";     
                     }

                     if (ds.Tables[0].Rows.Count == ctr + 1)
                     {
                         lc.Text = lc.Text + "<tr><b><td align=center colspan= 5>Average</td><td>=(SUM(F2:F" + (ctr + 2) + "))/" + ds.Tables[0].Rows.Count + "</td></tr>";
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