using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Data;

public partial class receiver : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Accept.Visible = false;
            TextBox1.Visible = false;
            Button1.Visible = false;
            Reject.Visible = false;
            div_accept.Visible = false;
            div_reject.Visible = false;

            // string Action = d.Decrypt(Request.QueryString["A"]);
            string Action = Request.QueryString["A"];
            ViewState["type_req"] = Action;
            // string emp_code = d.Decrypt(Request.QueryString["B"]);
            string emp_code = Request.QueryString["B"];
            ViewState["v_emp_code"] = emp_code;
            // string emp_name = d.Decrypt(Request.QueryString["C"]);
            string emp_name = Request.QueryString["C"];
            if (emp_name == "feedback")
            {
                if (Action == "approvef")
                {
                    d.operation("update pay_service_rating set flag=1 where id = " + emp_code);
                    //add update query
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    Accept.Text = "You have successfully Approved.";
                }
                else
                {
                    string check = d.getsinglestring("select flag from pay_service_rating where id = " + emp_code);
                    if (check == "4" || check == "0")
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        TextBox1.Visible = true;
                        Button1.Visible = true;
                        Accept.Text = "Enter the reason for rejection of Feedback";
                    }
                    else {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "You have Already Approved / Rejected the feedback request.";
                    }
                }
            }
            else if (emp_name == "audit")
            {
                if (Action == "approvea")
                {
                    d.operation("update pay_site_audit set android_flag=1 where id = " + emp_code);
                    //add update query
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    Accept.Text = "You have successfully Approved.";
                }
                else
                {
                    string check = d.getsinglestring("select android_flag from pay_site_audit where id = " + emp_code);
                    if (check == "4" || check == "0")
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        TextBox1.Visible = true;
                        Button1.Visible = true;
                        Accept.Text = "Enter the reason for rejection of Feedback";
                    }
                    else
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "You have Already Approved / Rejected the feedback request.";
                    }
                }
            }
            else if (Action.Equals("acceptfo"))
            {
                string stat = getstatus("select flag from pay_op_management_details where ID = '" + emp_code + "'");
                if (stat.Equals("1") || stat.Equals("2"))
                {
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    lnkapp.Visible = false;
                    Accept.Text = "Your status is already marked as Available / Not Available for visit of field officer " + emp_name;
                }
                else
                {
                    d.operation("update pay_op_management_details set flag =1,status='Available' where ID ='" + emp_code + "'");
                    send_mail_to_admin(emp_code, 1);
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    lnkapp.Visible = false;
                    Accept.Text = "Your status is marked as Available for visit of field officer " + emp_name;
                }
            }
            else if (Action.Equals("rejectfo"))
            {
                string stat = getstatus("select flag from pay_op_management_details where ID ='" + emp_code + "'");
                if (stat.Equals("1") || stat.Equals("2"))
                {
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    lnkapp.Visible = false;
                    Accept.Text = "Your status is already marked as Available / Not Available for visit of field officer " + emp_name; 
                }
                else
                {
                   
                    d.operation("update pay_op_management_details set flag =1,status='Not Available' where ID ='" + emp_code + "'");
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    TextBox1.Visible = true;
                    Button1.Visible = true;
                    lnkapp.Visible = false;
                    Accept.Text = "Please Mention Persons availability for field officer " + emp_name + " visit";
                  
                   
                }
            }
            else
            {
                if (Action == "" || emp_code == "" || emp_name == "" || Action == null || emp_code == null || emp_name == null)
                {
                    div_reject.Visible = true;
                    Reject.Visible = true;
                    Reject.Text = "Bad Request !!  Please try again.";
                }
                else if (Action.Equals("accept"))
                {

                    updateleaves("Approved");
                    d.operation("update pay_leave_transaction set LEAVE_STATUS ='Approved',Leave_Approved_Date=now() where LEAVE_ID= " + int.Parse(emp_code));
                    div_accept.Visible = true;
                    //d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + dr.GetValue(0).ToString() + "','Message from " + Session["USERNAME"].ToString() + " -Leave- " + txt_Status.SelectedValue + "','Leave_form.aspx')");
                    Accept.Visible = true;
                    Accept.Text = "You have successfully approved leave request of " + emp_name;
                }
                else if (Action.Equals("reject"))
                {
                    div_accept.Visible = true;
                    Accept.Visible = true;
                    TextBox1.Visible = true;
                    Button1.Visible = true;
                    Accept.Text = "Enter the reason for rejection of leave approval of " + emp_name;
                }
                else if (Action.Equals("acceptexp"))
                {
                    string stat = getstatus("select status from pay_add_expenses where expenses_id = '" + emp_code + "' limit 1");
                    if (stat.Equals("Submitted"))
                    {
                        d.operation("update pay_add_expenses set status ='Approved' where expenses_id = '" + emp_code + "'");
                        string i = Session["iddata"].ToString();
                        if (i == "1")
                        {
                            d.SendHtmlexpenseEmail1(Server.MapPath("~/travelexpenseconfirm.htm"), emp_code, 2, "Approved", "");
                        }
                        else
                        {
                            d.SendHtmlexpenseEmail(Server.MapPath("~/travelexpenseconfirm.htm"), emp_code, 2, "Approved", "");
                        }
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "You have successfully approved expense request of " + emp_name;
                    }
                    else
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "The status of expense request is already " + stat + " for " + emp_name;
                    }
                }
                else if (Action.Equals("rejectexp"))
                {
                    string stat = getstatus("select status from pay_add_expenses where expenses_id = '" + emp_code + "' limit 1");
                    if (stat.Equals("Submitted"))
                    {
                        div_accept.Visible = true; Accept.Visible = true;
                        TextBox1.Visible = true;
                        Button1.Visible = true;
                        Accept.Text = "Enter the reason for rejection of expense approval of " + emp_name;
                    }
                    else
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "The status of expense request is already " + stat + " for " + emp_name;
                    }
                }
                else if (Action.Equals("acceptme"))
                {
                    string stat = getstatus("select expense_status from apply_travel_plan where expenses_id = '" + emp_code + "' limit 1");
                    if (stat.Equals("Submitted") || stat.Equals("Pending"))
                    {
                        d.approve(emp_code, Request.QueryString["D"].ToString(), getname(Request.QueryString["D"].ToString()), Server.MapPath("~/travel_plan.htm"), Server.MapPath("~/travel_plan_confirm.htm"));
                        //d.operation("update apply_travel_plan set expense_status ='Approved', modified_by='" + getname(emp_code) + "' where expenses_id = '" + emp_code + "'");
                        //d.SendHtmltravelEmail(Server.MapPath("~/travel_plan_confirm.htm"), emp_code, 2, "Approved", "","");
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "You have successfully approved travel plan request of " + emp_name;
                    }
                    else
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "The status of expense request is already " + stat + " for " + emp_name;
                    }
                }
                else if (Action.Equals("rejectme"))
                {
                    string stat = getstatus("select expense_status from apply_travel_plan where expenses_id = '" + emp_code + "' limit 1");
                    if (stat.Equals("Submitted") || stat.Equals("Pending"))
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        TextBox1.Visible = true;
                        Button1.Visible = true;
                        Accept.Text = "Enter the reason for rejection of travel plan request of " + emp_name;
                    }
                    else
                    {
                        div_accept.Visible = true;
                        Accept.Visible = true;
                        Accept.Text = "The status of expense request is already " + stat + " for " + emp_name;
                    }
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        div_accept.Visible = false;
        div_reject.Visible = true;
        TextBox1.Visible = false;
        Button1.Visible = false;
        Reject.Visible = true;
        Accept.Visible = false;
        if (ViewState["type_req"].ToString().Equals("reject"))
        {
            updateleaves("Rejected");
            d.operation("update pay_leave_transaction set LEAVE_STATUS ='Rejected',STATUS_COMMENT='" + TextBox1.Text + "',Leave_Approved_Date=now() where LEAVE_ID= " + int.Parse(ViewState["v_emp_code"].ToString()));
            Reject.Text = "You have successfully Rejected leave request.";
        }
        else if (ViewState["type_req"].ToString().Equals("rejectexp"))
        {
            d.operation("update pay_add_expenses set status ='Rejected',comments='" + TextBox1.Text + "' where expenses_id = '" + ViewState["v_emp_code"].ToString() + "'");
            // d.SendHtmlexpenseEmail(Server.MapPath("~/travelexpenseconfirm.htm"), ViewState["v_emp_code"].ToString(), 1, "Rejected");
            d.SendHtmlexpenseEmail(Server.MapPath("~/travelexpenseconfirm.htm"), ViewState["v_emp_code"].ToString(), 2, "Rejected", "");
            Reject.Text = "You have successfully Rejected Expense request.";
        }
        else if (ViewState["type_req"].ToString().Equals("rejectme"))
        {
            d.operation("update apply_travel_plan set expense_status ='Rejected', modified_by='" + getname(ViewState["v_emp_code"].ToString()) + "', comments= concat('" + getname(ViewState["v_emp_code"].ToString()) + "', '-', '" + TextBox1.Text + "') where expenses_id = '" + ViewState["v_emp_code"].ToString() + "'");
            // d.SendHtmltravelEmail(Server.MapPath("~/travel_plan_confirm.htm"), ViewState["v_emp_code"].ToString(), 1, "Rejected");
            d.SendHtmltravelEmail(Server.MapPath("~/travel_plan_confirm.htm"), ViewState["v_emp_code"].ToString(), 2, "Rejected", "","");
            Reject.Text = "You have successfully Rejected Expense request.";
        }
        else if (ViewState["type_req"].ToString().Equals("rejectf"))
        {
            d.operation("update pay_service_rating set flag=2, comments ='" + TextBox1.Text + "' where id = " + ViewState["v_emp_code"].ToString());
            Reject.Text = "You have successfully Rejected Feedback request.";
        }
        else if (ViewState["type_req"].ToString().Equals("rejecta"))
        {
            d.operation("update pay_site_audit set android_flag=2, comment ='" + TextBox1.Text + "' where id = " + ViewState["v_emp_code"].ToString());
            Reject.Text = "You have successfully Rejected Audit Request.";
        }
        else if (ViewState["type_req"].ToString().Equals("rejectfo"))
        {
            d.operation("update pay_op_management_details set flag =2, comment ='" + TextBox1.Text + "' where id = " + ViewState["v_emp_code"].ToString());
            string emp_code = Request.QueryString["B"];
            ViewState["v_emp_code"] = emp_code;
            send_mail_to_admin(emp_code, 2);
            Reject.Text = "You have successfully submitted your non availability request";
        }
    }

    private void updateleaves(string status)
    {

        MySqlCommand cmd2 = new MySqlCommand("SELECT EMP_CODE,EMP_NAME,MANAGER,LEAVE_TYPE,FROM_DATE,TO_DATE,NO_OF_DAYS,LEAVE_REASON,LEAVE_STATUS,STATUS_COMMENT,Leave_Apply_Date,Leave_Approved_Date FROM pay_leave_transaction WHERE LEAVE_ID = " + int.Parse(ViewState["v_emp_code"].ToString()), d.con1);
        d.con1.Open();
        MySqlDataReader dr = cmd2.ExecuteReader();
        if (dr.Read())
        {
            if (dr.GetValue(3).ToString() == "CL")
            {
                if (dr.GetValue(8).ToString().Equals("Rejected"))
                {
                    if (status.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set CL = CL - " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                    }

                }
                else if (status.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set CL = CL + " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                }
            }
            else if (dr.GetValue(3).ToString() == "PL")
            {
                if (dr.GetValue(8).ToString().Equals("Rejected"))
                {
                    if (status.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set PL = PL - " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                    }

                }
                else if (status.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                }
            }
            else if (dr.GetValue(3).ToString() == "HD")
            {
                if (dr.GetValue(8).ToString().Equals("Rejected"))
                {
                    if (status.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set HD = HD - " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                    }

                }
                else if (status.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set HD = HD + " + int.Parse(dr.GetValue(6).ToString()) + " where emp_code = '" + dr.GetValue(0).ToString() + "'");
                }
            }

            d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + dr.GetValue(0).ToString() + "','Leave- " + status + "','Leave_form.aspx')");
            d.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + dr.GetValue(2).ToString() + "','Leave- " + status + " for " + dr.GetValue(1).ToString() + "','Leave_form.aspx')");

            if (status.Equals("Approved"))
            {
                // d.SendHtmlFormattedEmail(Server.MapPath("~/leaveemailconfirmation.htm"), dr.GetValue(2).ToString(), dr.GetValue(0).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(), dr.GetValue(7).ToString(), 1,"090","Approved");
                d.SendHtmlFormattedEmail(Server.MapPath("~/leaveemailconfirmation.htm"), dr.GetValue(0).ToString(), dr.GetValue(0).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(), dr.GetValue(7).ToString(), 1, "090", "Approved");
            }
            else if (status.Equals("Rejected"))
            {
                // d.SendHtmlFormattedEmail(Server.MapPath("~/leaveemailconfirmation.htm"), dr.GetValue(2).ToString(), dr.GetValue(0).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(), dr.GetValue(7).ToString(), 1,"f00","Rejected");
                d.SendHtmlFormattedEmail(Server.MapPath("~/leaveemailconfirmation.htm"), dr.GetValue(0).ToString(), dr.GetValue(0).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(), dr.GetValue(7).ToString(), 1, "f00", "Rejected");
            }
        }
        d.con1.Close();

    }

    private string getname(string id)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select emp_name from pay_employee_master where emp_code in (select reporting_to from pay_employee_master where emp_code in ( select emp_code from apply_travel_plan where expenses_id = '" + id + "'))", d.con);
        try
        {
            id = (string)cmd.ExecuteScalar();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            cmd.Dispose();
        }
        return id;
    }
    protected void lnkapp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login_Page.aspx");
    }
    private string getstatus(string query)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand(query, d.con);
        try
        {
            return (string)cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            cmd.Dispose();
        }
    }
    protected void send_mail_to_admin(string emp_code, int counter)
    {
        try
        {
         
            d.con.Open();
         
            string query = "";
            string client_code = "";
            string unit_code = "";
            client_code= d.getsinglestring("select client_code from pay_op_management_details where pay_op_management_details.ID = '" + emp_code + "'");
            unit_code = d.getsinglestring("select unit_code from pay_op_management_details where pay_op_management_details.ID = '" + emp_code + "'");

            query = "select pay_unit_master.unit_name,pay_employee_master.EMP_MOBILE_NO,field_officer_name,date_format(OPERATION_DATE,'%d-%m-%Y')as OPERATION_DATE ,START_TIME,END_TIME,pay_op_management_details.status, pay_op_management_details.comment,pay_op_management_details.id from pay_op_management_details INNER JOIN pay_unit_master ON pay_op_management_details.comp_code = pay_unit_master.comp_code AND pay_op_management_details.unit_code =pay_unit_master.unit_code INNER JOIN pay_employee_master ON pay_op_management_details.comp_code = pay_employee_master.comp_code AND pay_op_management_details.emp_code = pay_employee_master.emp_code where pay_op_management_details. ID = '" + emp_code + "'";
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
                body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir/Madam, <p>Concerns persons availabality. </FONT></FONT></FONT></B><p><Table border =1><tr><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>SR. No.</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>FIELD OFFICER NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>MOBILE NUMBER</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>LOCATION NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>VISIT ON</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>START TIME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>END TIME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>status</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Reason for not availability</FONT></FONT></FONT></th></tr>";
                foreach (DataRow row in dt.Rows)
                {
                    body = body + "<tr><td><FONT ',COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + ++i + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["field_officer_name"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["EMP_MOBILE_NO"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["UNIT_NAME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["OPERATION_DATE"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["START_TIME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["END_TIME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["status"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["comment"].ToString() + "</FONT></FONT></FONT></td></td></tr></br> ";
                }
                body = body + "</td></tr></Table> <p>";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                try
                {
                    d.con.Open();
                    MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;SELECT cast(group_concat(distinct head_email_id) as char), head_name, pay_branch_mail_details.client_code, pay_client_master.client_name, pay_branch_mail_details.comp_code,pay_unit_master.unit_name, pay_branch_mail_details.state,pay_unit_master.zone,pay_zone_master.field5 FROM pay_branch_mail_details inner join pay_unit_master on pay_unit_master.comp_code = pay_branch_mail_details.comp_code and pay_unit_master.unit_code = pay_branch_mail_details.unit_code INNER JOIN `pay_client_master` ON `pay_client_master`.`comp_code` = `pay_branch_mail_details`.`comp_code` AND `pay_client_master`.`client_code` = `pay_branch_mail_details`.`client_code` left outer join pay_zone_master on pay_zone_master.comp_code = pay_unit_master.comp_code and pay_zone_master.client_code = pay_unit_master.client_code and pay_zone_master.Region = pay_unit_master.zone and type = 'REGION' where pay_branch_mail_details.client_code = '" + client_code + "' and pay_branch_mail_details.unit_code = '" + unit_code + "'", d.con);
                    MySqlDataReader drnew = cmdnew.ExecuteReader();
                    System.Data.DataTable DataTable1 = new System.Data.DataTable();
                    DataTable1.Load(drnew);
                    d.con.Close();
                    foreach (DataRow row in DataTable1.Rows)
                    {
                        mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[4].ToString(), 3, "" + row[3].ToString() + "/ " + row[5].ToString() + " /" + row[1].ToString() + "", unit_code.ToString(), body, row[8].ToString());
                    }
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }
                
            }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    }
    protected void mail_send(string head_email_id, string head_email_name, string client_name, string comp_code, int counter, string subject,  string unit_code, string body1, string h_email_id)
    {

        List<string> list1 = new List<string>();
        string from_emailid = "", password = "";
        try
        {

            d.con.Open();
            //MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where client_code = '" + client_name + "' ", d.con);
           MySqlCommand cmd = new MySqlCommand("select field2,field3 from pay_zone_master where client_code = '" + client_name + "' and Type='client_Email' and field1='Admin' ", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                from_emailid = dr.GetValue(0).ToString();
                password = dr.GetValue(1).ToString();

            }
            //from_emailid = "manager@ihmsindia.com";
            //password = "manager@123";

            d.con.Close();
            if (!(from_emailid == "") || !(password == ""))
            {
                string body = body1;
                //string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Admin' and comp_code='" + Session["comp_code"].ToString() + "'");
                body = body +"<br><h4>Note : This is an automatically generated email</h4></br>";

                using (MailMessage mailMessage = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mailMessage.From = new MailAddress(head_email_id);

                    if (head_email_id != "")
                    {

                        mailMessage.To.Add(from_emailid);

                        if (!h_email_id.Equals(""))
                        {
                            mailMessage.CC.Add(h_email_id);
                        }
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        //if (counter1 == 1)
                        //{
                        //    mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf"));
                        //}

                        mailMessage.IsBodyHtml = true;
                        SmtpServer.UseDefaultCredentials = true;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                        SmtpServer.EnableSsl = true;

                        try
                        {
                            SmtpServer.Send(mailMessage);

                        }
                        catch
                        {

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
}