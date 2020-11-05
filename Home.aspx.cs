using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;//vikas


public partial class Home : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();

    public MySqlDataReader drmax = null;
    public MySqlDataReader dr { get; set; }
    public string total_emp1 = "0", Absent_emp1 = "0", Present_emp = "0", birth_emp = "0", emp_profile = "0", emp_reliver="0";
    protected void Page_Load(object sender, EventArgs e)
    {//vinod
//  get_roll();//vikas
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            hide_temp.Visible = true;
            temp_hide.Visible = false;
            ddl_id.Visible = false;
            chart_show.Visible = false;
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                hide_temp . Visible = false;
                ddl_id.Visible = true;
                temp_hide.Visible = true;
                chart_show.Visible = true;
                display_emp_profile();
                display_birthday();
                client_name();
                Bindchart();
                Bindchart1();
            }
            else if (Session["ROLE"].ToString() == "New Admin")
            {
                hide_temp.Visible = false;
                ddl_id.Visible = true;
                temp_hide.Visible = true;
                chart_show.Visible = true;
                display_emp_profile();
                display_birthday();
                client_name();
                Bindchart();
                Bindchart1();
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                hide_temp.Visible = false;
                ddl_id.Visible = true;
                temp_hide.Visible = true;
                chart_show.Visible = true;
                display_emp_profile();
                display_birthday();
                client_name();
                Bindchart();
                Bindchart1();
            }
           
        }
        ////**********************
        //vinod
        //d.con1.Open();
        //MySqlCommand cmd = new MySqlCommand("select pay_employee_master.emp_name as emp_name, date_format(logdate,'%d/%m/%Y') as logdate, time(min(logdate)) as intime, time(max(logdate)) as outtime, TIMEDIFF(max(logdate),min(logdate)) as WH from device_logs LEFT join pay_employee_master on pay_employee_master.attendance_id = device_logs.userid where logdate between (NOW() - INTERVAL 15 DAY) and now() and userid in (select attendance_id from pay_employee_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "') and pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' group by date_format(logdate,'%d/%m/%Y') order by ID Desc", d.con1);
        //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //sda.Fill(ds);
        //attendance_gridview.DataSource = null;
        //attendance_gridview.DataBind();
        //attendance_gridview.DataSource = ds.Tables[0];
        //attendance_gridview.DataBind();
        //d.con1.Close();

        ////**********************
        //d.con1.Open();
        //MySqlCommand cmd1 = new MySqlCommand("SELECT Header,Message,Header_Image as 'PhotoPath' FROM pay_event_mgmt WHERE CURDATE( ) BETWEEN Start_Date AND End_Date and comp_code = '" + Session["comp_code"].ToString() + "'", d.con1);
        //dr = cmd1.ExecuteReader();
        //if (dr.Read())
        //{

        //    lbl_Event_Name.Text = dr.GetValue(0).ToString();

        //    lbl_header_description.Text = dr.GetValue(1).ToString();

        //    if (!DBNull.Value.Equals(dr.GetValue(2)))
        //    {

        //        Image1.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
        //    }
        //    else
        //    {
        //        Image1.ImageUrl = null;
        //    }

        //    if (!DBNull.Value.Equals(dr.GetValue(2)))
        //    {

        //        Image2.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
        //    }
        //    else
        //    {
        //        Image2.ImageUrl = null;
        //    }

        //    if (!DBNull.Value.Equals(dr.GetValue(2)))
        //    {

        //        Image3.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
        //    }
        //    else
        //    {
        //        Image3.ImageUrl = null;
        //    }
        //}
        //dr.Close();
        //cmd1.Dispose();
        //d.con1.Close();

        //------ B'day Gridview -----------
        //d.con1.Open();
        ////MySqlCommand cmd_bday = new MySqlCommand("SELECT EMP_NAME As Name ,BIRTH_DATE As 'Birth Day' FROM pay_employee_master Where SUBSTRING(BIRTH_DATE,1,5) = date_format(now(),'%m/%d') AND comp_code = '" + Session["comp_code"].ToString() + "'", d.con1);
        //MySqlCommand cmd_bday = new MySqlCommand("SELECT concat(pay_employee_master.EMP_NAME,' (',pay_grade_master.Grade_desc,')') as 'Name & Grade', concat(date_format(pay_employee_master.birth_date,'%d'),' ',MONTHNAME(birth_date)) as 'Birth Date'  FROM pay_employee_master  left join pay_images_master on pay_images_master.EMP_CODE = pay_employee_master.EMP_CODE inner join pay_grade_master on pay_grade_master.Grade_code = pay_employee_master.Grade_code  where date_format(BIRTH_DATE,'%d/%m') = date_format(now(),'%d/%m') and pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'", d.con1);
        //MySqlDataReader dr_bday = cmd_bday.ExecuteReader();
        //if (dr_bday.HasRows)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    dt.Load(dr_bday);
        //    //ViewState["CurrentTable"] = dt;
        //    gridview_bday.DataSource = dt;
        //    gridview_bday.DataBind();

        //}
        //else
        //{

        //    gridview_bday.Visible = false;
        //}
        //d.con1.Close();

        // ----- Policy Gridview --------

        //d.con1.Open();
        //MySqlCommand cmd_policy = new MySqlCommand("SELECT Header As 'Policy Name',Header_Image As 'Attachment' FROM pay_policy_mgmt Where now() between start_date and end_date AND comp_code = '" + Session["comp_code"].ToString() + "'", d.con1);
        //MySqlDataReader dr_policy = cmd_policy.ExecuteReader();
        //if (dr_policy.HasRows)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    dt.Load(dr_policy);
        //    //ViewState["CurrentTable"] = dt;
        //    gridview_policy.DataSource = dt;
        //    gridview_policy.DataBind();

        //}
        //else
        //{

        //    gridview_policy.Visible = false;
        //}
        //d.con1.Close();

        // Vacancy rahul

       
       //d.con1.Open();
       //// MySqlCommand cmd_vacancy = new MySqlCommand("SELECT Vacancy_Number as 'Vacancy_Number',ROLE as 'ROLE',Mandatory_Skills as 'Mandatory_Skills' ,LOCATION as 'LOCATION' ,  Start_Date as 'Start_Date', End_Date as 'End_Date' FROM pay_job_opening  where now() between  Start_Date and  End_Date and  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
       // MySqlCommand cmd_vacancy = new MySqlCommand("SELECT ROLE as 'ROLE',Mandatory_Skills as 'Mandatory_Skills' ,LOCATION as 'LOCATION' ,  Start_Date as 'Start_Date', End_Date as 'End_Date' FROM pay_job_opening  where now()  and  End_Date and  comp_code='" + Session["comp_code"].ToString() + "' order by ID desc", d.con1);
       // DataSet ds7 = new DataSet();
       // MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd_vacancy);
       // adp1.Fill(ds7);
       // JobOpeningGridView.DataSource = null;
       // JobOpeningGridView.DataBind();
       
       // JobOpeningGridView.DataSource = ds7.Tables[0];
       // JobOpeningGridView.DataBind();
       // d.con1.Close();
       // client_pending();
       // employee_compliances();//employee compliances
       // barch_location();//branch location
       // employee_pending();//employee 

    }

    //vikas 
    protected void get_roll()
    {


        //client
        if (d.getaccess(Session["ROLE"].ToString(), "Client Master", Session["COMP_CODE"].ToString()) == "D")
        {
            pnl_client.Visible = true;
        }
        else
        {
            pnl_client.Visible = false;
        }
        //branch location
        if (d.getaccess(Session["ROLE"].ToString(), "Branch Master", Session["COMP_CODE"].ToString()) == "D")
        {
            pnl_brach.Visible = true;
        }
        else
        {
            pnl_brach.Visible = false;
        }
        //employee
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) == "D")
        {
            pnl_employee.Visible = true;
        }
        else
        {
            pnl_employee.Visible = true;
        }

        //employee compiances
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Compliance", Session["COMP_CODE"].ToString()) == "D")
        {
            pnl_emp_compliances.Visible = true;
        }
        else
        {
            pnl_emp_compliances.Visible = false;

        }

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }
    }

    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    MySqlCommand cmd1 = new MySqlCommand("SELECT HEADER_IMAGE FROM pay_event_mgmt ", d.con1);
    //    dr = cmd1.ExecuteReader();
    //    if (dr.Read())
    //    {
    //        if (!DBNull.Value.Equals(dr.GetValue(0)))
    //        {

    //            Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();
    //        }
    //        else
    //        {
    //            Image4.ImageUrl = null;
    //        }
    //    }
    //    dr.Close();
    //    cmd1.Dispose();

    //}

    //protected void LeaveTypeGridView_SelectedIndexChanged(object sender, EventArgs e)
    //{ 
    //}

    protected void btn_EMPLOYEE_PROFILE_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeMaster.aspx");
    }

    protected void grdview_bday_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gridview_bday_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("birthday.aspx");
    }
    protected void gridview_policy_SelectedIndexChanged(object sender, EventArgs e)
    {
        string filePath = "~/EMP_Images/" + (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    //protected void btnadd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int result = 0;

    //      //  result = d1.operation("Insert into pay_suggestion (type,to_select,select_employe,subject,description,response,send_Employee) value('" + ddl_type_selecte.SelectedValue + "','" + radio_button_selected.SelectedValue + "','" + ddl_maneger_selecte.SelectedValue + "','" + txt_subject.Text + "','" + txt_description.Text + "','" + txt_response.Text + "','" + Session["USERNAME"].ToString() + "')");


    //        if (result > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Suggestion saved successfully!!');", true);
    //            //text_Clear();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Suggestion adding failed...');", true);
    //            //text_Clear();
    //        }

    //        //btn_edit.Visible = false;

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Suggestion adding failed...')", true);
    //    }
    //    finally
    //    {
    //    }

    //}
    protected void DashBoardGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string test = e.Row.Cells[].Text;
        //    MySqlCommand cmd_in = new MySqlCommand("Select in_time from pay_employee_master Where EMP_CODE='" + Session["LOGIN_ID"].ToString() + "'", d2.con1);
        //    d2.con1.Open();
        //    MySqlDataReader dr_in = cmd_in.ExecuteReader();
        //    if (dr_in.Read())
        //    {
        //        if (dr_in.GetValue(0).ToString() != "Flexible")
        //        {
        //            int year = Convert.ToInt32(e.Row.Cells[2].Text.Substring(6, 4));
        //            int month = Convert.ToInt32(e.Row.Cells[2].Text.Substring(3, 2));
        //            int day = Convert.ToInt32(e.Row.Cells[2].Text.Substring(0, 2));
        //            int hour = Convert.ToInt32(e.Row.Cells[2].Text.Substring(11, 2));
        //            int min = Convert.ToInt32(e.Row.Cells[2].Text.Substring(14, 2));
        //            DateTime log_in = new DateTime(year, month, day, hour, min, 0);
        //            DateTime in_time = new DateTime(year, month, day, Convert.ToInt32(dr_in.GetValue(0).ToString().Substring(0, 2)), Convert.ToInt32(dr_in.GetValue(0).ToString().Substring(3, 2)), 0);
        //            int result = DateTime.Compare(log_in, in_time);
        //            //If Salary is less than 10000 than set the row Background Color to Cyan  
        //            if (result > 0)
        //            {
        //                e.Row.BackColor = Color.Red;
        //            }
        //        }
        //    }
        //    cmd_in.Dispose();
        //    dr_in.Close();
        //    d2.con1.Close();
        //}

    }

    //protected void late_comer()
    //{
    //    foreach (GridViewRow row in DashBoardGridView.Rows)
    //    {
    //        MySqlCommand cmd_in = new MySqlCommand("Select in_time from pay_employee_master Where EMP_CODE='" + Session["LOGIN_ID"].ToString() + "'", d2.con1);
    //        d2.con1.Open();
    //        MySqlDataReader dr_in = cmd_in.ExecuteReader();
    //        if (dr_in.Read())
    //        {
    //            if (dr_in.GetValue(0).ToString() != "Flexible")
    //            {
    //                int in_hour = 0, in_min = 0;
    //                if(dr_in.GetValue(0).ToString().Length.Equals(5))
    //                {
    //                    in_hour = Convert.ToInt32(dr_in.GetValue(0).ToString().Substring(0,2));
    //                }
    //                else
    //                {
    //                    in_hour = Convert.ToInt32(dr_in.GetValue(0).ToString().Substring(0,1));
    //                }

    //                if (dr_in.GetValue(0).ToString().Length.Equals(5))
    //                {
    //                    in_min = Convert.ToInt32(dr_in.GetValue(0).ToString().Substring(3, 1));
    //                }

    //                System.Web.UI.WebControls.Label lbl_date = (System.Web.UI.WebControls.Label)row.FindControl("lbl_date");
    //                string login = lbl_date.Text;
    //                int year = Convert.ToInt32(login.Substring(6, 4));
    //                int month = Convert.ToInt32(login.Substring(3, 2));
    //                int day = Convert.ToInt32(login.Substring(0, 2));
    //                int hour = Convert.ToInt32(login.Substring(11, 2));
    //                int min = Convert.ToInt32(login.Substring(14, 2));
    //                DateTime log_in = new DateTime(year, month, day, hour, min, 0);
    //                DateTime in_time = new DateTime(year, month, day,in_hour,in_min, 0);
    //                int result = DateTime.Compare(log_in, in_time);
    //                //If Salary is less than 10000 than set the row Background Color to Cyan  
    //                if (result > 0)
    //                {
    //                    row.BackColor = Color.Red;

    //                }
    //            }
    //        }
    //        cmd_in.Dispose();
    //        dr_in.Close();
    //        d2.con1.Close();
    //    }
    //}

    //protected void OnPaging(object sender, GridViewPageEventArgs e)
    //{
    //    DashBoardGridView.PageIndex = e.NewPageIndex;
    //    DashBoardGridView.DataBind();
    //    late_comer();
    //}
    public void SendEmail(string address, string subject, string message)
    {
        string email = "celtsoft1919@gmail.com";
        string password = "celtsoft@123";
        string fileName = "";

        var loginInfo = new NetworkCredential(email, password);
        var msg = new MailMessage();
        var smtpClient = new SmtpClient("smtp.gmail.com", 587);

        msg.From = new MailAddress(email);
        msg.To.Add(new MailAddress(address));
        msg.Subject = subject;
        msg.Body = message;
        msg.IsBodyHtml = true;

        if (upload_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upload_file.FileName);
            if (fileExt == ".jpg" || fileExt == ".pdf" || fileExt == ".docx" || fileExt == ".doc" || fileExt==".txt")
            {
                if (upload_file.PostedFile.ContentLength <= 512000)
                {
                    fileName = Path.GetFileName(upload_file.PostedFile.FileName);
                    upload_file.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);
                    Attachment attachment;
                    attachment = new Attachment(Server.MapPath("~/EMP_Images/") + fileName);
                    msg.Attachments.Add(attachment);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = loginInfo;
                    smtpClient.Send(msg);
                    smtpClient.Dispose();
                    smtpClient = null;
                    msg.Dispose();
                    msg = null;
                    attachment.Dispose();
                    attachment = null;
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Email Sent to HR Team !!!')", true);
                }
                else {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('File Size greater than 500 KB. !!!')", true);
                
                }

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PDF, DOCX, DOC and txt files !!!')", true);
            }

        }
        else
        {

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Email Sent to HR Team !!!')", true);
        }
        
    }



    protected void btn_submit_Click(object sender, EventArgs e)
    {
        SendEmail("rahul.kulkarni@celtsoft.com", "HRMS Suggestions - " + ddl_type_selected.SelectedItem.ToString() + " from " + Session["USERNAME"].ToString() + " - " + txt_subject.Text, txt_description.Text);
    }
    //employee compliances vikas
    protected void employee_compliances()
    {
        try
        {
            //string emp_namee = d.getsinglestring("SELECT `pay_employee_master`.`emp_name` from pay_employee_master where comp_code='" + Session["comp_code"] + "'and `pay_employee_master`.`UNIT_CODE` = '" + ddl_brachCompliances.SelectedValue + "'");
            //if (emp_namee == "")  
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found !!!');", true);
            //}
            //else
            //{


                // System.Data.DataTable dt_item = new System.Data.DataTable();
                //  MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("select  emp_name,PAN_NUMBER,EMP_NEW_PAN_NO ,ESIC_NUMBER ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE='" + ddl_brachCompliances.SelectedValue + "' AND client_code='" + ddl_client_empCompliances.SelectedValue + "'  and LOCATION='" + ddl_sate_empCompliances.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)", d.con);
           // MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT client_name, unit_name, emp_name, EMP_NEW_PAN_NO, ESIC_NUMBER, PF_NUMBER, PF_DEDUCTION_FLAG, cca, gratuity, special_allow, original_bank_account_no, BANK_HOLDER_NAME, PF_IFSC_CODE FROM pay_employee_master inner join pay_unit_master on pay_employee_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_employee_master.unit_CODE = pay_unit_master.unit_CODE inner join pay_client_master on pay_client_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_master.client_CODE = pay_unit_master.client_CODE where pay_unit_master.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.unit_code in (select unit_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) limit 10", d.con);
            MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT client_name,unit_name,emp_name,EMP_NEW_PAN_NO,ESIC_NUMBER,PF_NUMBER,PF_DEDUCTION_FLAG,cca,gratuity,special_allow,original_bank_account_no, BANK_HOLDER_NAME,PF_IFSC_CODE FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_employee_master.unit_CODE = pay_unit_master.unit_CODE INNER JOIN pay_client_master ON pay_client_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_master.client_CODE = pay_unit_master.client_CODE inner join pay_client_state_role_grade ON pay_client_state_role_grade.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_state_role_grade.unit_CODE = pay_unit_master.unit_CODE  AND pay_client_state_role_grade. EMP_CODE = '" + Session["LOGIN_ID"].ToString() + "' WHERE pay_unit_master.COMP_CODE = '"+Session["comp_code"].ToString()+"' AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL)", d.con);
                d.con.Open();
                DataSet ds = new DataSet();
                adp_grid2.Fill(ds);
                Grid_compliances_pupop.DataSource = ds;
                Grid_compliances_pupop.DataBind();
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
    
    
    protected void Grid_compliances_pupop_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0 && i != 1 && i != 2 && i != 9 && i !=10 && i != 11)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {

                        e.Row.Cells[i].Text = "Pending";
                       // count++;
                        e.Row.Cells[i].BackColor = Color.Red;


                    }

                    else
                    {
                        e.Row.Cells[i].Text = "Done";

                        e.Row.Cells[i].BackColor = Color.GreenYellow;
                    }

                }
            }
           // txt_count.Text = count.ToString();
        }
    }
    //branch_location vikas
    protected void barch_location()
    {
        try
        {
            //change
            // MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT pay_unit_master.UNIT_CODE,pay_unit_master.UNIT_CITY,pay_designation_count.DESIGNATION, pay_designation_count.COUNT,pay_designation_count.HOURS,pay_unit_master.Emp_count FROM  pay_designation_count  INNER JOIN  pay_unit_master On pay_unit_master.COMP_CODE= pay_designation_count.COMP_CODE and pay_unit_master.CLIENT_CODE  = pay_designation_count.CLIENT_CODE and pay_unit_master.UNIT_CODE  = pay_designation_count.UNIT_CODE WHERE pay_unit_master.client_code = '" + Session["comp_code"] + "'", d.con);
            MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT (select client_name from pay_client_master where pay_client_master.client_code = pay_unit_master.client_code) as client_name, pay_unit_master.UNIT_NAME,pay_unit_master.UNIT_CITY,pay_unit_master.Client_branch_code,pay_unit_master.OPus_NO,pay_unit_master.txt_zone, pay_unit_master.ZONE,pay_unit_master.UNIT_EMAIL_ID FROM  pay_unit_master inner join pay_client_state_role_grade on pay_unit_master.comp_code = pay_client_state_role_grade.comp_code and pay_unit_master.unit_code = pay_client_state_role_grade.unit_code and EMP_CODE = '" + Session["LOGIN_ID"].ToString() + "' WHERE  pay_unit_master.comp_code = '" + Session["comp_code"] + "' ", d.con);
            d.con.Open();
            DataSet ds = new DataSet();
            adp_grid1.Fill(ds);
            location_branch_pending_home.DataSource = ds;
            location_branch_pending_home.DataBind();

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
    protected void location_branch_pending_home_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0 && i != 1 && i != 2)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {

                        e.Row.Cells[i].Text = "Pending";
                        // count++;
                         e.Row.Cells[i].BackColor = Color.Red;


                    }

                    else
                    {
                        e.Row.Cells[i].Text = "Done";

                        e.Row.Cells[i].BackColor = Color.GreenYellow;
                    }

                }
            }
            // txt_count.Text = count.ToString();
        }

    }
    //employee vikas
    protected void employee_pending()
    {
        try
        {

            //string emp_namee = d.getsinglestring("SELECT `pay_employee_master`.`emp_name` from pay_employee_master where comp_code='" + Session["comp_code"] + "'and `pay_employee_master`.`UNIT_CODE` = '" + DropDownList3.SelectedValue + "'");
            //if (emp_namee == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found !!!');", true);
            //}
            //else
            //{

                //   MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode, pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE'  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  pay_employee_master.UNIT_CODE='" + DropDownList3.SelectedValue + "' AND pay_employee_master.client_code='" + DropDownList1.SelectedValue + "'  and LOCATION='" + DropDownList2.SelectedValue + "' AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) ", d.con1);


                // MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT pay_employee_master.emp_name,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.original_bank_account_no,pay_employee_master.PF_NOMINEE_RELATION,pay_employee_master.PF_NOMINEE_NAME,pay_images_master.original_photo,pay_images_master.original_adhar_card,pay_images_master.original_policy_document,pay_images_master.original_address_proof,pay_images_master.bank_passbook,pay_images_master.emp_signature, pay_images_master.noc_form, cast(group_concat(pay_document_details.document_type) as char) FROM pay_employee_master INNER JOIN pay_images_master ON pay_employee_master.COMP_CODE = pay_images_master.COMP_CODE AND pay_employee_master.EMP_CODE = pay_images_master.EMP_CODE inner join pay_document_details on pay_employee_master.COMP_CODE= pay_document_details.COMP_CODE and pay_employee_master.EMP_CODE=pay_document_details.EMP_CODE WHERE pay_employee_master.UNIT_CODE='" + DropDownList3.SelectedValue + "' AND pay_employee_master.client_code='" + DropDownList1.SelectedValue + "'  and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) group by pay_employee_master.EMP_CODE", d.con);
           // MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECTpay_employee_master.emp_name,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.original_bank_account_no, pay_employee_master.PF_NOMINEE_RELATION,pay_employee_master.PF_NOMINEE_NAME,pay_images_master.original_photo,pay_images_master.original_adhar_card, pay_images_master.original_policy_document,pay_images_master.original_address_proof,pay_images_master.bank_passbook,pay_images_master.emp_signature, pay_images_master.noc_form, CAST(GROUP_CONCAT(pay_document_details.document_type) AS char) AS document_type  FROM pay_employee_master left outer JOIN pay_images_master ON pay_employee_master.COMP_CODE = pay_images_master.COMP_CODE AND pay_employee_master.EMP_CODE = pay_images_master.EMP_CODEleft outer join pay_document_details ON pay_employee_master.COMP_CODE = pay_document_details.COMP_CODE AND pay_employee_master.EMP_CODE = pay_document_details.EMP_CODE WHERE  pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'and pay_employee_master.client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL) GROUP BY pay_employee_master.EMP_CODE", d.con);
            MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT client_name, unit_name, pay_employee_master.emp_name, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.original_bank_account_no, pay_employee_master.PF_NOMINEE_RELATION, pay_employee_master.PF_NOMINEE_NAME, pay_images_master.original_photo, pay_images_master.original_adhar_card, pay_images_master.original_policy_document, pay_images_master.original_address_proof, pay_images_master.bank_passbook, pay_images_master.emp_signature, pay_images_master.noc_form, (SELECT IF(document_type IS NOT NULL, 'Done', 'Pending') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'ID_Card') AS 'ID CARD', (SELECT IF(document_type IS NOT NULL, 'Done', 'Pending') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Uniform') AS 'UNIFORM', (SELECT IF(document_type IS NOT NULL, 'Done', 'Pending') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Sweater') AS 'SWEATER' FROM pay_employee_master LEFT OUTER JOIN pay_images_master ON pay_employee_master.COMP_CODE = pay_images_master.COMP_CODE AND pay_employee_master.EMP_CODE = pay_images_master.EMP_CODE LEFT OUTER JOIN pay_document_details ON pay_employee_master.COMP_CODE = pay_document_details.COMP_CODE AND pay_employee_master.EMP_CODE = pay_document_details.EMP_CODE inner join pay_unit_master on pay_employee_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_employee_master.unit_CODE = pay_unit_master.unit_CODE inner join pay_client_master on pay_client_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_master.client_CODE = pay_unit_master.client_CODE 	inner join pay_client_state_role_grade ON pay_client_state_role_grade.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_state_role_grade.unit_CODE = pay_unit_master.unit_CODE 	 AND pay_client_state_role_grade. EMP_CODE = '" + Session["LOGIN_ID"].ToString() + "' WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.LEFT_date IS NULL limit 300", d.con);
          
            d.con.Open();
                DataSet ds = new DataSet();
                adp_grid2.Fill(ds);
                employee_grd_home.DataSource = ds;
                employee_grd_home.DataBind();
                d.con.Close();
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
    protected void employee_grd_home_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0 && i != 1 && i != 2 && i != 3)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {

                        e.Row.Cells[i].Text = "Pending";
                        // count++;
                         e.Row.Cells[i].BackColor = Color.Red;


                    }

                    else
                    {
                        e.Row.Cells[i].Text = "Done";

                        e.Row.Cells[i].BackColor = Color.GreenYellow;
                    }

                }
            }
            // txt_count.Text = count.ToString();
        }
    }
    protected void location_branch_pending_home_PreRender(object sender, EventArgs e)
    {

        try
        {
            location_branch_pending_home.UseAccessibleHeader = false;
            location_branch_pending_home.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    //client vikas
    protected void client_pending()
    {
        try
        {

            MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT CLIENT_NAME, ifnull((select CASE WHEN pay_zone_master.Type = 'HEAD' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is null and Region is null and pay_zone_master.Type = 'HEAD' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as HEAD_INFO,ifnull((select CASE WHEN pay_zone_master.Type = 'ZONE' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is null and pay_zone_master.Type = 'ZONE' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'ZONE_INFO',ifnull((select CASE WHEN pay_zone_master.Type = 'REGION' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is not null and pay_zone_master.Type = 'REGION' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'REGION_INFO',IFNULL((SELECT CASE WHEN pay_zone_master.Type = 'GST' THEN 'Done' ELSE 'Pending' END FROM pay_zone_master WHERE zone IS NULL AND Region IS NOT NULL AND pay_zone_master.Type = 'GST' AND pay_zone_master.client_code = pay_client_master.client_code GROUP BY client_code), 'Pending') AS 'GST_INFO',ifnull((select 'Done' from pay_images where pay_images.client_code = pay_client_master.client_code group by client_code),'Pending') as 'DOCUMENT' FROM pay_client_master  where comp_code='" + Session["comp_code"] + "' and client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "')", d.con);
            d.con.Open();
            DataSet ds = new DataSet();
            adp_grid2.Fill(ds);
           // grd_client_pending.DataSource = ds;
           // grd_client_pending.DataBind();


            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_client_pending.DataSource = ds;
                grd_client_pending.DataBind();
            }

            for (int i = 0; i < grd_client_pending.Rows.Count; i++)
            {
                
                if (grd_client_pending.Rows[i].Cells[1].Text == "Done")
                    grd_client_pending.Rows[i].Cells[1].ControlStyle.BackColor = Color.GreenYellow;

                else
                    grd_client_pending.Rows[i].Cells[1].ControlStyle.BackColor = Color.Red;

                if (grd_client_pending.Rows[i].Cells[2].Text == "Done")
                    grd_client_pending.Rows[i].Cells[2].ControlStyle.BackColor = Color.GreenYellow;

                else
                    grd_client_pending.Rows[i].Cells[2].ControlStyle.BackColor = Color.Red;

                if (grd_client_pending.Rows[i].Cells[3].Text == "Done")
                    grd_client_pending.Rows[i].Cells[3].ControlStyle.BackColor = Color.GreenYellow;

                else
                    grd_client_pending.Rows[i].Cells[3].ControlStyle.BackColor = Color.Red;
              
                if (grd_client_pending.Rows[i].Cells[4].Text == "Done")
                    grd_client_pending.Rows[i].Cells[4].ControlStyle.BackColor = Color.GreenYellow;

                else
                    grd_client_pending.Rows[i].Cells[4].ControlStyle.BackColor = Color.Red;


                if (grd_client_pending.Rows[i].Cells[5].Text == "Done")
                    grd_client_pending.Rows[i].Cells[5].ControlStyle.BackColor = Color.GreenYellow;

                else
                    grd_client_pending.Rows[i].Cells[5].ControlStyle.BackColor = Color.Red;

            }

            d.con.Close();

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
    protected void grd_client_pending_PreRender(object sender, EventArgs e)
    {

        try
        {
            grd_client_pending.UseAccessibleHeader = false;
            grd_client_pending.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void employee_grd_home_PreRender(object sender, EventArgs e)
    {
        try
        {
            employee_grd_home.UseAccessibleHeader = false;
            employee_grd_home.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void Grid_compliances_pupop_PreRender(object sender, EventArgs e)
    {
        try
        {
            Grid_compliances_pupop.UseAccessibleHeader = false;
            Grid_compliances_pupop.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }


    private void Bindchart()
    {
        d.con.Open();
        emp_count_show();
        MySqlCommand cmd = new MySqlCommand("select ID,emp_count as 'count',status from temp_employee", d.con);

        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataTable ChartData = ds.Tables[0];

        //storing total rows count to loop on each Record  
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];

        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis  
            XPointMember[count] = ChartData.Rows[count]["ID"].ToString();
            //storing values for Y Axis  
            YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["count"]);


        }
        //binding chart control  
        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line  
        Chart1.Series[0].BorderWidth = 10;
        //setting Chart type   
        Chart1.Series[0].ChartType = SeriesChartType.Bar;
        // Chart1.Series[0].ChartType = SeriesChartType.StackedBar;  
        Color[] colors = new Color[] { Color.Orange, Color.Green, Color.Red, Color.Yellow, Color.Maroon, Color.Red };
        foreach (Series series in Chart1.Series)
        {
            foreach (DataPoint point in series.Points)
            {
                //point.LabelBackColor = colors[series.Points.IndexOf(point)];
                point.Color = colors[series.Points.IndexOf(point)];
            }
        }
        //Hide or show chart back GridLines  
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        Chart1.Series["chart"].Color = System.Drawing.ColorTranslator.FromHtml("#418cf0");
        Chart1.Series["chart"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
        Chart1.Series["chart"].IsValueShownAsLabel = true;

        //Enabled 3D  
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        d.con.Close();
    }
    private void Bindchart1()
    {
        d.con.Open();
        atted_emp_count_show();
        MySqlCommand cmd = new MySqlCommand("select ID,emp_code as 'count' from temp_android_attendance", d.con);

        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataTable ChartData = ds.Tables[0];

        //storing total rows count to loop on each Record  
        string[] XPointMember = new string[ChartData.Rows.Count];
        int[] YPointMember = new int[ChartData.Rows.Count];

        for (int count = 0; count < ChartData.Rows.Count; count++)
        {
            //storing Values for X axis  
            XPointMember[count] = ChartData.Rows[count]["ID"].ToString();
            //storing values for Y Axis  

            YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["count"]);


        }
        //binding chart control  
        Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line  
        Chart2.Series[0].BorderWidth = 10;
        //setting Chart type   
        Chart2.Series[0].ChartType = SeriesChartType.Bar;
        Color[] colors = new Color[] { Color.Orange, Color.Green, Color.Red, Color.LightBlue, Color.Maroon, Color.Red };
        foreach (Series series in Chart2.Series)
        {
            foreach (DataPoint point in series.Points)
            {
                //point.LabelBackColor = colors[series.Points.IndexOf(point)];
                point.Color = colors[series.Points.IndexOf(point)];
            }
        }
        Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
        Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
        Chart2.Series["chart1"].Color = System.Drawing.ColorTranslator.FromHtml("#418cf0");
        Chart2.Series["chart1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
        Chart2.Series["chart1"].IsValueShownAsLabel = true;
        // Chart1.Series[0].ChartType = SeriesChartType.StackedBar;  

        //Hide or show chart back GridLines  
        Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
        Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;

        //Enabled 3D  
        Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
        d.con.Close();

    }
    protected void client_name()
    {

        ddl_client_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        if (Session["ROLE"].ToString() == "Managing Director")
        {
            cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) and client_active_close='0' ORDER BY client_code", d.con);
        }
        else if (Session["ROLE"].ToString() == "New Admin")
        {
            cmd_item = new MySqlDataAdapter("select distinct Client_name,pay_client_state_role_grade.client_code from pay_client_master inner join pay_client_state_role_grade on pay_client_master.client_code=pay_client_state_role_grade.client_code  where pay_client_state_role_grade.comp_code='" + Session["comp_code"] + "' and client_active_close='0' and emp_code='" + Session["LOGIN_ID"].ToString() + "' ORDER BY client_code", d.con);
        }
        else if (Session["ROLE"].ToString() == "AVP Admin")
        {
            cmd_item = new MySqlDataAdapter("select distinct Client_name,pay_client_state_role_grade.client_code from pay_client_master inner join pay_client_state_role_grade on pay_client_master.client_code=pay_client_state_role_grade.client_code  where pay_client_state_role_grade.comp_code='" + Session["comp_code"] + "' and client_active_close='0' and emp_code='" + Session["LOGIN_ID"].ToString() + "' ORDER BY client_code", d.con);
        }
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
            ddl_client_name.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_client_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindchart();
        Bindchart1();
        display_total_gv();
        display_present_gv();
        display_absent_gv();
        display_emp_profile();
        display_reliver_gv();
        display_birthday();
        DataTable dt_state = new DataTable();
        MySqlDataAdapter adp_state = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' and client_code='"+ddl_client_name.SelectedValue+"' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d1.con);
        d1.con.Open();
        try
        {
            adp_state.Fill(dt_state);
            if (dt_state.Rows.Count > 0)
            {
                ddl_state.DataSource = dt_state;
                ddl_state.DataTextField = dt_state.Columns[0].ToString();
               
                ddl_state.DataBind();
            }
            dt_state.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            //ddl_state.Items.Insert(0, new ListItem("Select"));
            ddl_state.Items.Insert(0, new ListItem("ALL", "ALL"));
            d1.con.Close();
        }

    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindchart();
        Bindchart1();
        display_total_gv();
        display_present_gv();
        display_absent_gv();
        display_emp_profile();
        ddl_unit.Items.Clear();
        display_reliver_gv();
        display_birthday();
        DataTable dt_state = new DataTable();
        string where_state = "";
        if (ddl_state.SelectedValue == "ALL")
        {
            where_state = "";
        }
        else
        {
            where_state = "and client_code='" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "'";
        }
        MySqlDataAdapter adp_state = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "'  " + where_state + "  and branch_status = 0  ORDER BY UNIT_CODE", d1.con);
        d1.con.Open();
        try
        {
            adp_state.Fill(dt_state);
            if (dt_state.Rows.Count > 0)
            {
                ddl_unit.DataSource = dt_state;
                ddl_unit.DataTextField = dt_state.Columns[0].ToString();
                ddl_unit.DataValueField = dt_state.Columns[1].ToString();
                ddl_unit.DataBind();
            }
            dt_state.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            //ddl_unit.Items.Insert(0, new ListItem("Select"));
            ddl_unit.Items.Insert(0, new ListItem("ALL", "ALL"));
            d1.con.Close();
        }
    }
    protected void emp_count_show()
    {
        string query = null;
        string query_active = null;
        string query_Left = null;
        string where_Login = "";
        string other_emp_series = "";
        if (Session["ROLE"].ToString() == "Managing Director")
        {
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = " client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                    }
                }
            }
        }
        else if (Session["ROLE"].ToString() == "New Admin")
        {
            other_emp_series = "";
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = "pay_employee_master. client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                    }
                }
            }
        }
        else if (Session["ROLE"].ToString() == "AVP Admin")
        {
            
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = "pay_employee_master. client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                    }
                }
            }
        }
        if (ddl_client_name.SelectedValue != "Select")
        {
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                query = "select count(emp_code) from pay_employee_master where    " + where_Login + "";
                query_active = "select count(emp_code) from pay_employee_master where left_date is null and  " + where_Login + "";
                query_Left = "select count(emp_code) from pay_employee_master where left_date is not null and " + where_Login + "";
            }
            else if (Session["ROLE"].ToString() == "New Admin")
            {
                query = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent'";
                query_active = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent' and left_date is null";
                query_Left = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent' and left_date is not null";
              
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                query = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent'";
                query_active = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent' and left_date is null";
                query_Left = "select count(emp_code) from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent' and left_date is not null";
            }
        }
        else
        {
            query = "0";
            query_active = "0";
            query_Left = "0";
        }
        d.operation("DROP TABLE IF EXISTS temp_employee");
        d.con.Open();
        d.operation("create table temp_employee (ID int(11),emp_count varchar(20),status varchar(20))");
        d.operation("insert into temp_employee (ID,emp_count,status) values (1,(" + query + " ),'Total')");
        d.operation("insert into temp_employee (ID,emp_count,status) values (2,(" + query_active + " ),'Active')");
        d.operation("insert into temp_employee (ID,emp_count,status) values (3,( " + query_Left + "),'Left')");
        d.con.Close();
    
    }
    protected void atted_emp_count_show()
    {
        string where_Login = "";
        if (Session["ROLE"].ToString() == "Managing Director")
        {
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    }
                }
            }
        }
        else if (Session["ROLE"].ToString() == "New Admin")
        {
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    }
                }
            }
        }
        else if (Session["ROLE"].ToString() == "AVP Admin")
        {
            if (ddl_client_name.SelectedValue != "Select")
            {
                where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                {
                    where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                    {
                        where_Login = " pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent' AND pay_employee_master.left_date IS NULL ";
                    }
                }
            }
        }
        d.operation("DROP TABLE IF EXISTS temp_android_attendance");
        d.operation("DROP TABLE IF EXISTS temp_android");
        d.operation("create table temp_android_attendance (ID int(11),emp_code varchar(20),status varchar(20))");
        d.operation("create table temp_android (ID int(11),emp_code varchar(20),status varchar(20))");
        
            if (ddl_client_name.SelectedValue != "Select")
            {
                //d.operation("create table temp_android_attendance (ID int(11),emp_code varchar(20),status varchar(20))");
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (1,(select count(emp_code) from pay_employee_master where " + where_Login + "),'Total')");
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (2,(select count(emp_code)from(select DISTINCT pay_android_attendance_logs.emp_code from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.unit_code=pay_unit_master.unit_code   INNER JOIN pay_employee_master ON pay_android_attendance_logs.unit_code = pay_employee_master.unit_code and `pay_android_attendance_logs`.`emp_code` = `pay_employee_master`.`emp_code` where `pay_android_attendance_logs`.`date_time` BETWEEN DATE(NOW()) AND NOW() and " + where_Login + " ) as t1),'Present')");
                string count1 = d.getsinglestring("select distinct group_concat( DISTINCT pay_android_attendance_logs.emp_code) from pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.unit_code=pay_unit_master.unit_code   INNER JOIN pay_employee_master ON pay_android_attendance_logs.unit_code = pay_employee_master.unit_code   and pay_android_attendance_logs.emp_code = pay_employee_master.emp_code where `pay_android_attendance_logs`.`date_time` BETWEEN DATE(NOW()) AND NOW() and " + where_Login + "");
                if (count1 != "")
                {
                    string[] emp_code_count = count1.Split(',');
                    foreach (object obj in emp_code_count)
                    {
                        d.operation("insert into temp_android (ID,emp_code,status) values (2,('" + obj + "'),'Present')");
                    }
                }
                //for count
                string count = d.getsinglestring("SELECT COUNT(pay_employee_master.emp_code) FROM pay_employee_master  INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code   WHERE pay_client_master.client_active_close = 0 AND branch_status = 0 AND pay_employee_master.left_date IS NULL  AND pay_employee_master.emp_code NOT IN (SELECT emp_code FROM temp_android) and " + where_Login + "");
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (3,(" + count + "),'Absent')");
            }
            else
            {
                where_Login = "";
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (1,('0'),'Total')");
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (2,('0'),'Present')");
                d.operation("insert into temp_android (ID,emp_code,status) values (2,('0'),'Present')");
                d.operation("insert into temp_android_attendance (ID,emp_code,status) values (3,('0'),'Absent')");
            }
    }
    protected void ddl_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindchart();
        Bindchart1();
        display_total_gv();
        display_present_gv();
        display_absent_gv();
        display_emp_profile();
        display_reliver_gv();
        display_birthday();
    }
    protected void display_total_gv()
    {
        d.con.Open();
        try
        {
            gv_total_links.DataSource = null;
            gv_total_links.DataBind();
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = null;
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "New Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else { where_Login = ""; }
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                dt_status = new MySqlDataAdapter("select emp_name,EMP_MOBILE_NO,unit_name  from pay_employee_master inner join pay_unit_master on  pay_employee_master.unit_code=pay_unit_master.unit_code  " + where_Login + "", d.con);
            }
            else
            {
                dt_status = new MySqlDataAdapter("select emp_name,EMP_MOBILE_NO,unit_name  from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code  " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent'", d.con);
            }
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_total_links.DataSource = ds_status;
                gv_total_links.DataBind();
            }
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                dt_status1 = new MySqlDataAdapter("select emp_code from pay_employee_master " + where_Login + "", d.con);
            }
            else
            {
                dt_status1 = new MySqlDataAdapter("select emp_code  from pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code  " + where_Login + " AND pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade    INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND pay_employee_master.employee_type = 'Permanent'", d.con);
            }
            dt_status1.Fill(ds_status1);
            if (ds_status1.Tables[0].Rows.Count > 0)
            {
                total_emp1 = ds_status1.Tables[0].Rows.Count.ToString();

            }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void display_present_gv()
     {
        d.con.Open();
        try
        {
            gv_present_link.DataSource = null;
            gv_present_link.DataBind();
            DataSet ds_status = new DataSet();
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status = null;
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
           else if (Session["ROLE"].ToString() == "New Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {

                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else { where_Login = ""; }
            dt_status = new MySqlDataAdapter("select distinct unit_name,pay_android_attendance_logs.emp_name,pay_employee_master.emp_mobile_no  from pay_android_attendance_logs inner join pay_employee_master on pay_android_attendance_logs.emp_code=pay_employee_master.emp_code  INNER JOIN pay_unit_master ON pay_android_attendance_logs.unit_code = pay_unit_master.unit_code  AND `pay_android_attendance_logs`.`emp_code` = `pay_employee_master`.`emp_code` where `pay_android_attendance_logs`.`date_time` BETWEEN DATE(NOW()) AND NOW() " + where_Login + "", d.con);
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_present_link.DataSource = ds_status;
                gv_present_link.DataBind();
            }

            dt_status1 = new MySqlDataAdapter("select distinct pay_android_attendance_logs.emp_name  from pay_android_attendance_logs inner join pay_employee_master on pay_android_attendance_logs.emp_code=pay_employee_master.emp_code  INNER JOIN pay_unit_master ON pay_android_attendance_logs.unit_code = pay_unit_master.unit_code  AND `pay_android_attendance_logs`.`emp_code` = `pay_employee_master`.`emp_code` where `pay_android_attendance_logs`.`date_time` BETWEEN DATE(NOW()) AND NOW()  AND branch_status = 0 AND pay_employee_master.left_date IS NULL " + where_Login + "", d.con);
            dt_status1.Fill(ds_status1);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                Present_emp = ds_status1.Tables[0].Rows.Count.ToString();
            }
           
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void display_absent_gv()
    {
        d.con.Open();
        try
        {
            gv_absent_link.DataSource = null;
            gv_absent_link.DataBind();
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = null;
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
           else if (Session["ROLE"].ToString() == "New Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else { where_Login = ""; }
            dt_status = new MySqlDataAdapter("SELECT distinct emp_name,unit_name,emp_mobile_no FROM pay_employee_master  INNER JOIN `pay_unit_master` ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code   WHERE pay_client_master.client_active_close = 0 AND branch_status = 0 AND pay_employee_master.emp_code NOT IN (SELECT emp_code FROM temp_android) AND pay_employee_master.left_date IS NULL " + where_Login + " ", d.con);

            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
               gv_absent_link.DataSource = ds_status;
                gv_absent_link.DataBind();
            }


            dt_status1 = new MySqlDataAdapter("SELECT distinct pay_employee_master.emp_code FROM pay_employee_master  INNER JOIN `pay_unit_master` ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_client_master ON pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.client_code = pay_client_master.client_code   WHERE pay_client_master.client_active_close = 0 AND branch_status = 0 AND pay_employee_master.emp_code NOT IN (SELECT emp_code FROM temp_android) AND pay_employee_master.left_date IS NULL " + where_Login + " ", d.con);
            dt_status1.Fill(ds_status1);
            if (ds_status1.Tables[0].Rows.Count > 0)
            {
                Absent_emp1 = ds_status1.Tables[0].Rows.Count.ToString();
                //late_emp = ds_status.Tables[0].Rows.Count.ToString();
            }


        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }


    protected void display_birthday()
    {
        d.con.Open();
        try
        {
            gv_birthday_link.DataSource = null;
            gv_birthday_link.DataBind();
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = null;
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
           else if (Session["ROLE"].ToString() == "New Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Permanent'";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = " and pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Permanent'";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "and  pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Permanent'";
                        }
                    }
                }
            }
            else { where_Login = ""; }
            dt_status = new MySqlDataAdapter("SELECT EMP_NAME AS 'EMP_NAME', date_format(BIRTH_DATE,'%d-%m-%Y') AS 'BIRTH_DATE',EMP_MOBILE_NO FROM pay_employee_master WHERE date_format(BIRTH_DATE,'%d-%m') = date_format(now(),'%d-%m') AND comp_code = '" + Session["comp_code"].ToString() + "'" + where_Login + " AND pay_employee_master.left_date IS NULL ", d.con);

            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_birthday_link.DataSource = ds_status;
                gv_birthday_link.DataBind();
            }


            dt_status1 = new MySqlDataAdapter("SELECT EMP_NAME AS 'EMP_NAME', date_format(BIRTH_DATE,'%d-%m-%Y') AS 'BIRTH_DATE',EMP_MOBILE_NO FROM pay_employee_master WHERE date_format(BIRTH_DATE,'%d-%m') = date_format(now(),'%d-%m') AND comp_code = '" + Session["comp_code"].ToString() + "'" + where_Login + " AND pay_employee_master.left_date IS NULL ", d.con);
            dt_status1.Fill(ds_status1);
            if (ds_status1.Tables[0].Rows.Count > 0)
            {
                birth_emp = ds_status1.Tables[0].Rows.Count.ToString();
                //late_emp = ds_status.Tables[0].Rows.Count.ToString();
            }


        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void display_emp_profile()
    {
        d.con.Open();
        try
        {
            gv_emp_pro.DataSource = null;
            gv_emp_pro.DataBind();
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = null;
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
               
                    where_Login = "and emp_code='" + Session["LOGIN_ID"].ToString() + "'";
                
            }
            else if (Session["ROLE"].ToString() == "New Admin")
            {
                where_Login = "and emp_code='" + Session["LOGIN_ID"].ToString() + "'";
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                where_Login = "and emp_code='" + Session["LOGIN_ID"].ToString() + "'";
            }
            else { where_Login = ""; }
            dt_status = new MySqlDataAdapter("select emp_name,EMP_MOBILE_NO,unit_name  from pay_employee_master inner join pay_unit_master on  pay_employee_master.unit_code=pay_unit_master.unit_code  where  pay_employee_master.left_date IS NULL  " + where_Login + " ", d.con);

            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_emp_pro.DataSource = ds_status;
                gv_emp_pro.DataBind();
            }


            dt_status1 = new MySqlDataAdapter("select emp_code from pay_employee_master where pay_employee_master.left_date IS NULL and emp_code='" + Session["LOGIN_ID"].ToString() + "' " + where_Login + " ", d.con);
            dt_status1.Fill(ds_status1);
            if (ds_status1.Tables[0].Rows.Count > 0)
            {
                emp_profile = ds_status1.Tables[0].Rows.Count.ToString();
                //late_emp = ds_status.Tables[0].Rows.Count.ToString();
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void display_reliver_gv()
    {
        d.con.Open();
        try
        {
            gv_reliver.DataSource = null;
            gv_reliver.DataBind();
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = null;
            DataSet ds_status1 = new DataSet();
            MySqlDataAdapter dt_status1 = null;
            string where_Login = "";
            if (Session["ROLE"].ToString() == "Managing Director")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "New Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        }
                    }
                }
            }
            else if (Session["ROLE"].ToString() == "AVP Admin")
            {
                if (ddl_client_name.SelectedValue != "Select")
                {
                    where_Login = " where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                    if (ddl_state.SelectedValue != "Select" && ddl_state.SelectedValue != "ALL")
                    {
                        where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        if (ddl_unit.SelectedValue != "Select" && ddl_unit.SelectedValue != "ALL" && ddl_unit.SelectedValue != "")
                        {
                            where_Login = "where pay_employee_master.client_code='" + ddl_client_name.SelectedValue + "' and pay_employee_master.client_wise_state='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unit.SelectedValue + "' and employee_type='Reliever' AND pay_employee_master.left_date IS NULL ";
                        }
                    }
                }
            }
            else { where_Login = ""; }
            dt_status = new MySqlDataAdapter("select emp_name,EMP_MOBILE_NO,unit_name  from pay_employee_master inner join pay_unit_master on  pay_employee_master.unit_code=pay_unit_master.unit_code  " + where_Login + "", d.con);
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_reliver.DataSource = ds_status;
                gv_reliver.DataBind();
            }
            dt_status1 = new MySqlDataAdapter("select emp_code from pay_employee_master " + where_Login + "", d.con);
            dt_status1.Fill(ds_status1);
            if (ds_status1.Tables[0].Rows.Count > 0)
            {
                emp_reliver = ds_status1.Tables[0].Rows.Count.ToString();

            }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
}