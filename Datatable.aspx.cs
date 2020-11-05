using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

public partial class Datatable : System.Web.UI.Page
{
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_name.Visible = false;

        if (!IsPostBack)
        {
            ddl_technician_wise.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.emp_code,pay_employee_master.emp_name FROM  pay_department_master,  pay_employee_master WHERE pay_employee_master.dept_code = pay_department_master.dept_code AND  pay_department_master.dept_name LIKE '%support%' and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
               // ddl_technician_wise.DataSource = ds.Tables[0];
                //ddl_technician_wise.DataBind();

                ddl_technician_wise.DataSource = ds.Tables[0];
                ddl_technician_wise.DataValueField = "emp_code";
                ddl_technician_wise.DataTextField = "emp_name";
                ddl_technician_wise.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
                ddl_technician_wise.Items.Insert(0, new ListItem("Select", "Select"));
            }


           //  ddl_technician.Items.Clear();
            MySqlCommand cmd1 = new MySqlCommand("SELECT Domain_Name from  pay_assign_domain where comp_code='"+Session["comp_code"]+"'",d1.con);
            d1.con.Open();
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                ddl_domain_wise.DataSource = ds.Tables[0];
                ddl_domain_wise.DataValueField = "Domain_Name";
                ddl_domain_wise.DataTextField = "Domain_Name";
                ddl_domain_wise.DataBind();


            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
                ddl_domain_wise.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        string com_code = Session["comp_code"].ToString();
        d1.con1.Open();
        MySqlCommand cmdnew = new MySqlCommand("	SELECT pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID, pay_assign_domain.Domain_Name FROM pay_employee_master INNER JOIN pay_assign_domain on pay_employee_master.EMP_CODE = pay_assign_domain.EMP_CODE WHERE pay_employee_master.comp_code = pay_assign_domain.comp_code AND pay_employee_master.Dept_Code = (SELECT Dept_Code FROM pay_department_master WHERE Dept_Name LIKE '%support%')", d1.con);
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmdnew);
        adp1.Fill(ds1);
        GradeGridView.DataSource = ds1.Tables[0];
        GradeGridView.DataBind();
        d1.con1.Close();
       // technician_details();
      }
    
    protected void ddl_tables_SelectedIndexChanged(object sender, EventArgs e)
    {
        GradeGridView.Visible = false;
        lbl_name.Visible = true;
        lbl_name.Text = ddl_tables.SelectedItem.Text;
    //------ WORKSHEET --------------------------
        if (ddl_tables.SelectedValue.ToString() == "daily")
        {
            DataTable dt = new DataTable();
            string query = "";
            if(txt_from_date.Text != "" && txt_to_date.Text !="")
            {
                query = "Select DATE_FORMAT(date_assigned,'%d/%m/%Y') As 'Assigned Date',(Select agency_name from agency_master Where agency_master.Id = ticket_master.agency_code) As Agency,(Select team_name from team_master Where team_master.Id = ticket_master.team_code) As Team,branch,system,patient_name As 'Patient Name',insurance_code As Insurance,episode_id As 'Episode Id',visit_code As 'Visit Code',DATE_FORMAT(visit_date,'%d/%m/%Y') As 'Visit Date',(Select coder_name from coder_master Where coder_master.Id = ticket_master.coder_id) As Coder,status_code as Status,on_hold_reason As 'On Hold Reason',priority_record As 'Record Priority',maturity_days As 'Maturity Days',DATE_FORMAT(on_hold_date,'%d/%m/%Y') As 'On Hold Date',DATE_FORMAT(date_complete,'%d/%m/%Y') As 'Completed Date',record_type_code As 'Record Type',do_not_bill As 'Do not Bill' from ticket_master Where date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
            }
            else
            {
                query = "Select DATE_FORMAT(date_assigned,'%d/%m/%Y') As 'Assigned Date',(Select agency_name from agency_master Where agency_master.Id = ticket_master.agency_code) As Agency,(Select team_name from team_master Where team_master.Id = ticket_master.team_code) As Team,branch,system,patient_name As 'Patient Name',insurance_code As Insurance,episode_id As 'Episode Id',visit_code As 'Visit Code',DATE_FORMAT(visit_date,'%d/%m/%Y') As 'Visit Date',(Select coder_name from coder_master Where coder_master.Id = ticket_master.coder_id) As Coder,status_code as Status,on_hold_reason As 'On Hold Reason',priority_record As 'Record Priority',maturity_days As 'Maturity Days',DATE_FORMAT(on_hold_date,'%d/%m/%Y') As 'On Hold Date',DATE_FORMAT(date_complete,'%d/%m/%Y') As 'Completed Date',record_type_code As 'Record Type',do_not_bill As 'Do not Bill' from ticket_master";
            }
            //MySqlCommand cmd = new MySqlCommand("select Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17 from costing_detail_new", d1.con1);
            MySqlCommand cmd = new MySqlCommand(query, d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
           
                    // DataTable.Load automatically advances the reader to the next result set
                   da.Fill(dt);
                    //da.Fill(dt);
             
                    //Populating a DataTable from database.
                    // DataTable dt = this.GetData();

                    //Building an HTML string.
                    StringBuilder html = new StringBuilder();

                    //Table start.
                    // html.Append("<table style=\"width: 100%;\" aria-describedby=\"example_info\" role=\"grid\" id=\"example\" class=\"table table-striped table-bordered dataTable no-footer\" width=\"100%\" cellspacing=\"0\">");

                    html.Append("<thead>");
                    //Building the Header row.
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    html.Append("</tr>");
                    html.Append("</thead>");
                    html.Append("<tbody>");
                    //Building the Data rows.
                    foreach (DataRow row in dt.Rows)
                    {
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                        html.Append("</tr>");
                    }
                    html.Append("</tbody>");

                    BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
                    //Table end.
                    // 
                    //Append the HTML string to Placeholder.
                    //BodyContent.Controls.Add(new Literal { Text = html.ToString() });
                    //d1.con1.Close();
                    ddl_tables.SelectedValue = "0";
           
        }
   
    //-------- PAYMENT DETAILS -----------------------------------
       
        else if (ddl_tables.SelectedValue.ToString() == "team")
        {
            GradeGridView.Visible = false;
            //MySqlCommand cmd = new MySqlCommand("select Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17 from costing_detail_new", d1.con1);
            string query = "";
            if (txt_from_date.Text != "" && txt_to_date.Text != "")
            {

                query = "Select team_master.team_name,count(ticket_master.Id) As Assigned , (Select count(status_code) from ticket_master Where status_code='on-hold' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'On Hold' , (Select  count(status_code) from ticket_master Where status_code='Completed' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'Completed' , (Select  count(status_code) from ticket_master Where status_code='Assigned' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'In Complete' from ticket_master INNER JOIN team_master ON ticket_master.team_code = team_master.id  Where date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
            }
            else 
            {
                query = "Select team_master.team_name,count(ticket_master.Id) As Assigned , (Select count(status_code) from ticket_master Where status_code='on-hold') As 'On Hold' , (Select  count(status_code) from ticket_master Where status_code='Completed') As 'Completed' , (Select  count(status_code) from ticket_master Where status_code='Assigned') As ' In Complete' from ticket_master INNER JOIN team_master ON ticket_master.team_code = team_master.id";
            }
                MySqlCommand cmd = new MySqlCommand(query, d1.con1);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            //Populating a DataTable from database.
            // DataTable dt = this.GetData();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            // html.Append("<table style=\"width: 100%;\" aria-describedby=\"example_info\" role=\"grid\" id=\"example\" class=\"table table-striped table-bordered dataTable no-footer\" width=\"100%\" cellspacing=\"0\">");

            html.Append("<thead>");
            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
            //Table end.
            // 
            //Append the HTML string to Placeholder.
            //BodyContent.Controls.Add(new Literal { Text = html.ToString() });
            //d1.con1.Close();
            ddl_tables.SelectedValue = "0";
        }
     
 
    //------ holidays_details  ------------------------------
        else if (ddl_tables.SelectedValue.ToString() == "auditor")
        {
            GradeGridView.Visible = false;
            //MySqlCommand cmd = new MySqlCommand("select Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17 from costing_detail_new", d1.con1);
             string query = "";
            if (txt_from_date.Text != "" && txt_to_date.Text != "")
            {
                query = "Select coder_master.coder_name As Coder , '' As Attendance , count(ticket_master.Id) As Assigned , (Select count(ticket_master.Id) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where date_assigned < STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y')) As 'Carry Forward', count(ticket_master.Id + (Select count(ticket_master.Id) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where date_assigned < STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y'))) As 'Total Assigned' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='On-hold' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'On-hold' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='Completed' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'complete' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='Assigned' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'In complete'  from ticket_master INNER JOIN coder_master ON ticket_master.coder_id = coder_master.Id  Where date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
            }
            else
            {
                query = "Select coder_master.coder_name As Coder , '' As Attendance , count(ticket_master.Id) As Assigned , (Select count(ticket_master.Id) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where date_assigned < STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y')) As 'Carry Forward', count(ticket_master.Id + (Select count(ticket_master.Id) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where date_assigned < STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y'))) As 'Total Assigned' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='On-hold' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'On-hold' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='Completed' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'complete' , (Select count(status_code) from ticket_master INNER Join coder_master ON ticket_master.coder_id = coder_master.Id Where status_code='Assigned' AND date_assigned between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')) As 'In complete'  from ticket_master INNER JOIN coder_master ON ticket_master.coder_id = coder_master.Id ";//query="select Sr_No,holidays_details.Membership_No,enquiry_form.Husband_Name As Name,From_Date_Holiday_Used As FromDate,To_Date_Holiday_Used As ToDate,holidays_details.Balance,AMC_Amount As Destination,Mode_Of_Payment As Hotel,TID_CHEQUE_DD_APPR As Costing,Remark from holidays_details INNER JOIN enquiry_form ON enquiry_form.Membership_No = holidays_details.Membership_No";
            }
                MySqlCommand cmd = new MySqlCommand(query, d1.con1);
           
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            //Populating a DataTable from database.
            // DataTable dt = this.GetData();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            // html.Append("<table style=\"width: 100%;\" aria-describedby=\"example_info\" role=\"grid\" id=\"example\" class=\"table table-striped table-bordered dataTable no-footer\" width=\"100%\" cellspacing=\"0\">");

            html.Append("<thead>");
            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
            //Table end.
            // 
            //Append the HTML string to Placeholder.
            //BodyContent.Controls.Add(new Literal { Text = html.ToString() });
            ddl_tables.SelectedValue = "0";
        }

    //------------ Service Details ---------------------
        else if (ddl_tables.SelectedValue.ToString() == "services_details")
        {
            //MySqlCommand cmd = new MySqlCommand("select Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17 from costing_detail_new", d1.con1);
             string query = "";
            if (txt_from_date.Text != "" && txt_to_date.Text != "")
            {
                query= "select SR_NO,DATE_FORMAT(SERVICE_DATE,'%d/%m/%Y') As Date,Membership_No,Member_Name ,Voucher_Name As Name ,Voucher_qty As Quantity,SERVICE_AMOUNT As Amount,REMARK from services_details Where SERVICE_DATE between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
            }
            else
            {
               query="select SR_NO,DATE_FORMAT(SERVICE_DATE,'%d/%m/%Y') As Date,Membership_No,Member_Name ,Voucher_Name As Name ,Voucher_qty As Quantity,SERVICE_AMOUNT As Amount,REMARK from services_details";
            }
            MySqlCommand cmd = new MySqlCommand("select SR_NO,DATE_FORMAT(SERVICE_DATE,'%d/%m/%Y') As Date,Membership_No,Member_Name ,Voucher_Name As Name ,Voucher_qty As Quantity,SERVICE_AMOUNT As Amount,REMARK from services_details Where SERVICE_DATE between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')", d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            //Populating a DataTable from database.
            // DataTable dt = this.GetData();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            // html.Append("<table style=\"width: 100%;\" aria-describedby=\"example_info\" role=\"grid\" id=\"example\" class=\"table table-striped table-bordered dataTable no-footer\" width=\"100%\" cellspacing=\"0\">");

            html.Append("<thead>");
            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");

            BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
            //Table end.
            // 
            //Append the HTML string to Placeholder.
            //BodyContent.Controls.Add(new Literal { Text = html.ToString() });
            ddl_tables.SelectedValue = "0";
        }
    }
    protected void ddl_technician_wise_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_technician_wise.SelectedValue == "Select")
        {
            string com_code = Session["comp_code"].ToString();
            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID, pay_assign_domain.Domain_Name FROM pay_employee_master INNER JOIN pay_assign_domain on pay_employee_master.EMP_CODE = pay_assign_domain.EMP_CODE WHERE pay_employee_master.comp_code = pay_assign_domain.comp_code AND pay_employee_master.Dept_Code = (SELECT Dept_Code FROM pay_department_master WHERE Dept_Name LIKE '%support%')", d1.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            GradeGridView.DataSource = ds1.Tables[0];
            GradeGridView.DataBind();
            d1.con1.Close();

        }
        else
        {
            string com_code = Session["comp_code"].ToString();
            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID, pay_assign_domain.Domain_Name FROM pay_employee_master INNER JOIN pay_assign_domain on pay_employee_master.EMP_CODE = pay_assign_domain.EMP_CODE WHERE pay_employee_master.comp_code = pay_assign_domain.comp_code AND pay_employee_master.Dept_Code = (SELECT Dept_Code FROM pay_department_master WHERE Dept_Name LIKE '%support%') And pay_employee_master.EMP_CODE like '%" + ddl_technician_wise.SelectedValue.ToString() + "%'  ", d1.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            GradeGridView.DataSource = ds1.Tables[0];
            GradeGridView.DataBind();
            d1.con1.Close();
        }
      //ddl_tables.SelectedValue = "0";
        }
    protected void ddl_domain_wise_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_domain_wise.SelectedValue == "Select")
        {
            string com_code = Session["comp_code"].ToString();
            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID, pay_assign_domain.Domain_Name FROM pay_employee_master INNER JOIN pay_assign_domain on pay_employee_master.EMP_CODE = pay_assign_domain.EMP_CODE WHERE pay_employee_master.comp_code = pay_assign_domain.comp_code AND pay_employee_master.Dept_Code = (SELECT Dept_Code FROM pay_department_master WHERE Dept_Name LIKE '%support%')", d1.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            GradeGridView.DataSource = ds1.Tables[0];
            GradeGridView.DataBind();
            d1.con1.Close();
        }
        else
        {
            string com_code = Session["comp_code"].ToString();
            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID, pay_assign_domain.Domain_Name FROM pay_employee_master INNER JOIN pay_assign_domain on pay_employee_master.EMP_CODE = pay_assign_domain.EMP_CODE WHERE pay_employee_master.comp_code = pay_assign_domain.comp_code AND pay_employee_master.Dept_Code = (SELECT Dept_Code FROM pay_department_master WHERE Dept_Name LIKE '%support%') And pay_assign_domain.Domain_Name like '%" + ddl_domain_wise.SelectedValue.ToString() + "%'  ", d1.con);
            
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            GradeGridView.DataSource = ds1.Tables[0];
            GradeGridView.DataBind();
            d1.con1.Close();
        }
    }    
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void technician_details()
    {
        string query = "";
        if (txt_from_date.Text != "" && txt_to_date.Text != "")
        {
            // query= "select SR_NO,DATE_FORMAT(SERVICE_DATE,'%d/%m/%Y') As Date,Membership_No,Member_Name ,Voucher_Name As Name ,Voucher_qty As Quantity,SERVICE_AMOUNT As Amount,REMARK from services_details Where SERVICE_DATE between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
           // query = "Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name from pay_employee_master inner join pay_assign_domain where pay_employee_master.EMP_CODE=pay_assign_domain.comp_code And  pay_employee_master.comp_code='" + ddl_technician_wise.SelectedValue.ToString() + "' And SERVICE_DATE between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
            query = "Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name from pay_employee_master inner join pay_assign_domain where pay_employee_master.EMP_CODE=pay_assign_domain.comp_code And SERVICE_DATE between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_to_date.Text + "','%d/%m/%Y')";
        }
        else
        {
           // query = "Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name from pay_employee_master inner join pay_assign_domain where pay_employee_master.comp_code=pay_assign_domain.comp_code And pay_employee_master.EMP_CODE='" + ddl_technician_wise.SelectedValue.ToString() + "'";
            query = "Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name from pay_employee_master inner join pay_assign_domain where pay_employee_master.comp_code=pay_assign_domain.comp_code ";
        }
    //    MySqlCommand cmd = new MySqlCommand("Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name  from pay_employee_master inner join pay_assign_domain where pay_employee_master.comp_code=pay_assign_domain.comp_code And pay_employee_master.EMP_CODE='" + ddl_technician_wise.SelectedValue.ToString() + "'", d1.con1);
        MySqlCommand cmd = new MySqlCommand("Select pay_employee_master.EMP_NAME As Technician_Name ,pay_employee_master.EMP_MOBILE_NO AS Mobile_No,pay_employee_master.EMP_EMAIL_ID As Email_Id,pay_assign_domain.Domain_Name  from pay_employee_master inner join pay_assign_domain where pay_employee_master.comp_code=pay_assign_domain.comp_code ", d1.con1);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);


        StringBuilder html = new StringBuilder();


        html.Append("<thead>");
        //Building the Header row.
        html.Append("<tr>");
        foreach (DataColumn column in dt.Columns)
        {
            html.Append("<th>");
            html.Append(column.ColumnName);
            html.Append("</th>");
        }
        html.Append("</tr>");
        html.Append("</thead>");
        html.Append("<tbody>");
        //Building the Data rows.
        foreach (DataRow row in dt.Rows)
        {
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<td>");
                html.Append(row[column.ColumnName]);
                html.Append("</td>");
            }
            html.Append("</tr>");
        }
        html.Append("</tbody>");

        BodyContent1.Controls.Add(new Literal { Text = html.ToString() });

        ddl_tables.SelectedValue = "0";
    
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "Select$" + e.Row.RowIndex);
        }
    }

    protected void gv_expeness_PreRender(object sender, EventArgs e)
    {
        try
        {
            GradeGridView.UseAccessibleHeader = false;
            GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
    protected void lnkView_Click_details(object sender, EventArgs e)
    {

        modelpopup.Show();
    }

}