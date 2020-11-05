using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using CrystalDecisions.Shared;


public partial class AttendanceRegister : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    BillingSalary bs = new BillingSalary();
    string aa = "";
    public string monthend_count = "0";
    //mahendra payment_history


    double final_double_amount;
    protected int result = 0;
    protected double billing_amount = 0, recived_amt = 0;

    CrystalDecisions.CrystalReports.Engine.ReportDocument crystalReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (d.getaccess(Session["ROLE"].ToString(), "Month End Process", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Month End Process", Session["COMP_CODE"].ToString()) == "R")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Month End Process", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Month End Process", Session["COMP_CODE"].ToString()) == "C")
        {

        }
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            client_code();
            comp_data();

            // komal changes 12-06-2020
            panel_manual_invoice_type.Visible = false;
            panel_manual_other_state.Visible = false;

            // komal changes 18-06-2020
            panel_note_number.Visible = false;
            //
            //vikas
            //ddl_client_list();//vika
            //load_gridview();
            //gv_payment.Visible = true;

            //receving_amount.Visible = false;
            billing_panel.Visible = false;
            convence.Visible = false;
            bill_panel7.Visible = false;
            payment_approve.Visible = false;
            ddl_billing_type.Items.Insert(0, "Select");
            //client_state_panel.Visible = false;
            //ddl_bill_client.Visible = false;
            //ddl_bill_state.Visible = false;
            //ddl_bill_unit.Visible = false;
            desigpanel.Visible = false;

            //MD change

            txt_pmt_date.Text = d.getCurrentMonthYear();
            txt_bill_date.Text = d.getCurrentMonthYear();
            txt_date.Text = d.getCurrentMonthYear();
            txt_month.Text = d.getCurrentMonthYear();
            txt_arrears_date.Text = d.getCurrentMonthYear();
            load_arrears_gridview("");
            ViewState["monthend_count"] = "0";
        }
        string role = "";
        monthend_count = ViewState["monthend_count"].ToString();
        role = d.getsinglestring("select Role from pay_user_master where LOGIN_ID='" + Session["LOGIN_ID"].ToString() + "'");
        if (((role == "Managing Director") || (role == "AVP Admin")))
        {

            bill_panel7.Visible = true;
            payment_approve.Visible = true;
        }
    }

    /// komal 13-04-2020 from date to date

    protected void from_date_to_date()
    {
        ddl_bill_client.Items.Clear();
        MySqlDataAdapter cmd_item = null;
        System.Data.DataTable dt_item = new System.Data.DataTable();

        // komal changes credit-debit 18-06-2020




        if (ddl_billing_type.SelectedValue == "6")
        {
            //cmd_item = new MySqlDataAdapter("SELECT DISTINCT `client_name`,billing_client_code  FROM `pay_billing_from_to_history` INNER JOIN `pay_client_master` ON `pay_billing_from_to_history`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_billing_from_to_history`.`billing_client_code` = `pay_client_master`.`client_code`", d.con);

            cmd_item = new MySqlDataAdapter("SELECT distinct client,client_code FROM `pay_billing_unit_rate_history` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `start_date` != '0' AND `end_date` != '0' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and invoice_flag!='0'", d.con);
        }
        else
            if (ddl_billing_type.SelectedValue == "7")
            {
                //cmd_item = new MySqlDataAdapter("SELECT DISTINCT `client_name`,billing_client_code  FROM `pay_billing_from_to_history` INNER JOIN `pay_client_master` ON `pay_billing_from_to_history`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_billing_from_to_history`.`billing_client_code` = `pay_client_master`.`client_code`", d.con);

                cmd_item = new MySqlDataAdapter("SELECT distinct client,client_code FROM `pay_billing_unit_rate_history_arrears` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'and invoice_flag = '1' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' ", d.con);
            }

            else
                if (ddl_billing_type.SelectedValue == "9")
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct credit_debit_note.client_name, client_code FROM credit_debit_note  INNER JOIN `pay_client_master` ON `credit_debit_note`.`comp_code` = `pay_client_master`.`comp_code` AND `credit_debit_note`.`client_name` = `pay_client_master`.`client_name`WHERE credit_debit_note.comp_code = '" + Session["comp_code"].ToString() + "' and `final_invoice_flag` = '1'", d.con);

                }
                else
                {

                    cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
                }
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_bill_client.DataSource = dt_item;
                ddl_bill_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_bill_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_bill_client.DataBind();

            }
            ddl_bill_client.Items.Insert(0, "Select");
            //ddl_bill_client.SelectedValue = "Select";

            dt_item.Dispose();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }



    /////
    //vikas
    protected void client_code()
    {
        ddl_client1.Items.Clear();
        ddl_minibank_client.Items.Clear();
        MySqlDataAdapter cmd_item = null;
        System.Data.DataTable dt_item = new System.Data.DataTable();

        cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);

        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client1.DataSource = dt_item;
                ddl_client1.DataTextField = dt_item.Columns[0].ToString();
                ddl_client1.DataValueField = dt_item.Columns[1].ToString();
                ddl_client1.DataBind();

                //ddl_bill_client.DataSource = dt_item;
                //ddl_bill_client.DataTextField = dt_item.Columns[0].ToString();
                //ddl_bill_client.DataValueField = dt_item.Columns[1].ToString();
                //ddl_bill_client.DataBind();

                ddl_minibank_client.DataSource = dt_item;
                ddl_minibank_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_minibank_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_minibank_client.DataBind();

                ddl_pmt_client.DataSource = dt_item;
                ddl_pmt_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_pmt_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_pmt_client.DataBind();

                ddl_arrears_client.DataSource = dt_item;
                ddl_arrears_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_arrears_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_arrears_client.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client1.Items.Insert(0, "Select");
            ddl_minibank_client.Items.Insert(0, "Select");
            // ddl_bill_client.Items.Insert(0, "Select");
            ddl_pmt_client.Items.Insert(0, "Select");
            ddl_arrears_client.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }


    protected void ddl_client1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnk_monthend();
        if (ddl_client1.SelectedValue != "Select")
        {
            //State
            ddl_statename.Items.Clear();
            ddl_unitcode.Items.Clear();


            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (p.STATE_NAME) FROM pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.unit_code = b.unit_code and p.month = b.month and p.year = b.year WHERE p.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND p.client_code = '" + ddl_client1.SelectedValue + "' and p.month = '" + txt_month.Text.Substring(0, 2) + "' and  p.year ='" + txt_month.Text.Substring(3) + "' AND invoice_flag = 2 AND payment_approve = 2 ORDER BY STATE_NAME ", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_statename.DataSource = dt_item;
                    ddl_statename.DataTextField = dt_item.Columns[0].ToString();
                    ddl_statename.DataValueField = dt_item.Columns[0].ToString();
                    ddl_statename.DataBind();
                    ddl_statename.Items.Insert(0, "ALL");
                }

                dt_item.Dispose();
                d.con.Close();
                ddl_statename_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); load_gridview(); all_branch(); }
        }
        else
        {

        }
    }
    protected void ddl_statename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_statename.SelectedValue != "ALL")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'UNIT_NAME', unit_code, flag FROM pay_unit_master  WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client1.SelectedValue + "' and state_name = '" + ddl_statename.SelectedValue + "' AND branch_status = 0  and unit_code in (select distinct(p.unit_code) from pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.client_code = b.client_code and p.unit_code = b.unit_code where  p.comp_code = '" + Session["COMP_CODE"].ToString() + "' and p.client_code='" + ddl_client1.SelectedValue + "' and p.state_name = '" + ddl_statename.SelectedValue + "'  and p.month = '" + txt_month.Text.Substring(0, 2) + "' and  p.year ='" + txt_month.Text.Substring(3) + "' and p.payment_approve = 2 and invoice_flag = 2) ORDER BY unit_name ", d.con);
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

                    ddl_unitcode.Items.Insert(0, "ALL");
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
    }
    //v
    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void load_gridview()
    {
        d.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT distinct (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_billing_master_history.billing_client_code) AS 'CLIENT', (SELECT Unit_name FROM pay_unit_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND unit_code = pay_billing_master_history.billing_unit_code) AS 'BRANCH',billing_state as 'STATE', IF(month_end = 1, 'COMPLETED', 0) AS 'MONTH END',MONTH,YEAR FROM pay_billing_master_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND billing_client_code = '" + ddl_client1.SelectedValue + "' AND month_end = 1 and month = '" + txt_month.Text.Substring(0, 2) + "' and  year ='" + txt_month.Text.Substring(3) + "'", d.con1);
            adp1.Fill(ds1);
            gv_fullmonthot.DataSource = ds1.Tables[0];
            gv_fullmonthot.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void gv_fullmonthot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "1")
            {
                //Find the TextBox control.
                CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);
                txtName.Checked = true;
                txtName.Enabled = false;
            }
        }
    }
    //vikas
    //private void load_grdview()
    //{
    //    gv_fullmonthot.Visible = false;
    //    d.con1.Open();
    //    try
    //    {
    //        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,comp_code,client_code,unit_code,month,year,(SELECT `emp_name` FROM `pay_employee_master` WHERE `uploaded_by` = `pay_employee_master`.`emp_code`) AS uploaded_by, uploaded_date,description,concat('~/Attendance_Images/',file_name) as Value FROM pay_files_timesheet where comp_Code = '" + Session["COMP_CODE"].ToString() + "' ", d.con1);

    //        DataSet DS1 = new DataSet();
    //        MySqlDataAdapter1.Fill(DS1);
    //        grd_company_files.DataSource = null;
    //        grd_company_files.DataBind();
    //        grd_company_files.DataSource = DS1;
    //        grd_company_files.DataBind();
    //        txt_document1.Text = "";
    //        grd_company_files.Visible = true;
    //        d.con1.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        d.con1.Close();

    //    }

    //}

    //vikas
    protected void upload_Click(object sender, EventArgs e)
    {
        //upload_documents(document1_file);


    }
    //private void upload_documents(FileUpload document_file)
    //{

    //    if (document_file.HasFile)
    //    {
    //        string fileExt = System.IO.Path.GetExtension(document_file.FileName);
    //        if (fileExt == ".jpg" || fileExt == ".JPG" || fileExt == ".png" || fileExt == ".PNG" || fileExt == ".pdf" || fileExt == ".PDF" || fileExt == ".JPEG" || fileExt == ".jpeg")
    //        {
    //            string fileName = Path.GetFileName(document_file.PostedFile.FileName);
    //            document_file.PostedFile.SaveAs(Server.MapPath("~/Attendance_Images/") + fileName);

    //            string new_file_name = txt_month.Text.Replace("/", "_") + fileExt;

    //            File.Copy(Server.MapPath("~/Attendance_Images/") + fileName, Server.MapPath("~/Attendance_Images/") + new_file_name, true);
    //            File.Delete(Server.MapPath("~/Attendance_Images/") + fileName);
    //            d.operation("insert into pay_files_timesheet (comp_code, file_name, description, month, year, uploaded_by, uploaded_date) values ('" + Session["COMP_CODE"].ToString() + "','" + new_file_name + "','" + txt_document1.Text + "','" + int.Parse(txt_month.Text.Substring(0, 2)) + "','" + int.Parse(txt_month.Text.Substring(3)) + "','" + Session["LOGIN_ID"].ToString() + "',now())");
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
    //        }

    //    }
    //    //load_grdview();
    //}

    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void grd_company_files_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;

        if (e.Row.Cells[1].Text == "1")
        {
            e.Row.Cells[1].Text = "JAN";
        }
        else if (e.Row.Cells[1].Text == "2")
        {
            e.Row.Cells[1].Text = "FEB";
        }
        else if (e.Row.Cells[1].Text == "3")
        {
            e.Row.Cells[1].Text = "MAR";
        }
        else if (e.Row.Cells[1].Text == "4")
        {
            e.Row.Cells[1].Text = "APR";
        }
        else if (e.Row.Cells[1].Text == "5")
        {
            e.Row.Cells[1].Text = "MAY";
        }
        else if (e.Row.Cells[1].Text == "6")
        {
            e.Row.Cells[1].Text = "JUN";
        }
        else if (e.Row.Cells[1].Text == "7")
        {
            e.Row.Cells[1].Text = "JUL";
        }
        else if (e.Row.Cells[1].Text == "8")
        {
            e.Row.Cells[1].Text = "AUG";
        }
        else if (e.Row.Cells[1].Text == "9")
        {
            e.Row.Cells[1].Text = "SEP";
        }
        else if (e.Row.Cells[1].Text == "10")
        {
            e.Row.Cells[1].Text = "OCT";
        }
        else if (e.Row.Cells[1].Text == "11")
        {
            e.Row.Cells[1].Text = "NOV";
        }
        else if (e.Row.Cells[1].Text == "12")
        {
            e.Row.Cells[1].Text = "DEC";
        }
    }
    //mahendra payment_history

    //DAL d = new DAL();
    // double rec_amount;
    // double balance_amount;
    // double final_double_amount;
    //protected int result = 0;
    //protected double billing_amount = 0, recived_amt = 0;


    protected void btn_close_click(object sender, object e)
    {
        Response.Redirect("Home.aspx");
    }

    //protected void ddl_statename_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_statename.SelectedValue != "ALL")
    //    {
    //        ddl_unitcode.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client1.SelectedValue + "' and state_name = '"+ddl_statename.SelectedValue+"' AND branch_status = 0  ORDER BY UNIT_CODE", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_unitcode.DataSource = dt_item;
    //                ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
    //                ddl_unitcode.DataBind();
    //            }
    //            ddl_unitcode.Items.Insert(0, "ALL");
    //            dt_item.Dispose();
    //            d.con.Close();

    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }
    //    }
    //}
    protected void btn_month_end_Click(object sender, EventArgs e)
    {
        string start_date_common = get_start_date();
        string start_end_common = get_end_date();

        // if(d.getsinglestring())
        if (start_date_common != "")
        {
            update_attendance("pay_billing_master_history", "pay_billing_master_history", 1);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Month End Completed Successfully !!!')", true);
            try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
            catch { }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Month End Failed !!!')", true);
            try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
            catch { }
        }
        lnk_monthend();
        ddl_unitcode.SelectedIndex = 0;
        txt_month.Text = "";
        ddl_client1.SelectedIndex = 0;
        ddl_statename.SelectedIndex = 0;

    }
    protected void btn_month_start_Click(object sender, EventArgs e)
    {
        update_attendance("pay_billing_master_history", "pay_billing_master_history", 0);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Month Start Completed Successfully !!!')", true);
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        ddl_unitcode.SelectedIndex = 0;
        txt_month.Text = "";
        ddl_client1.SelectedIndex = 0;
        ddl_statename.SelectedIndex = 0;
    }

    private void update_attendance(string from_table, string to_table, int month_end)
    {
        string where = "where pb.comp_code = '" + Session["COMP_CODE"].ToString() + "' ";
        if (ddl_client1.SelectedValue != "Select")
        {
            where = where + " and pb.billing_client_code = '" + ddl_client1.SelectedValue + "'";
            if (ddl_statename.SelectedValue != "ALL")
            {
                where = where + " AND pb.billing_state = '" + ddl_statename.SelectedValue + "'";
            }
            if (ddl_unitcode.SelectedValue != "ALL" && ddl_unitcode.SelectedValue != "0")
            {
                where = where + " AND pb.billing_unit_code = '" + ddl_unitcode.SelectedValue + "'";
            }
            where = where + " AND pb.month = '" + txt_month.Text.Substring(0, 2) + "' AND pb.year = '" + txt_month.Text.Substring(3) + "' ";

            d.operation("UPDATE pay_billing_master_history pb inner JOIN pay_pro_master p ON pb.comp_code = p.comp_code AND pb.billing_unit_code = p.unit_code AND pb.month = p.month AND pb.year = p.year   AND payment_approve = 2 inner JOIN pay_billing_unit_rate_history b ON pb.comp_code = b.comp_code AND pb.billing_unit_code = b.unit_code    AND pb.month = b.month AND pb.year = b.year   AND invoice_flag = 2 SET pb.month_end = " + month_end + ", b.month_end = " + month_end + ", p.month_end = " + month_end + "  " + where);

            // d.operation("UPDATE pay_billing_master_history SET month_end = " + month_end + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and billing_unit_code in (SELECT DISTINCT p.unit_code FROM pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.client_code = b.client_code and p.unit_code = b.unit_code  " + where + ")");

            // d.operation("UPDATE pay_billing_unit_rate_history SET month_end = " + month_end + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and unit_code in (SELECT DISTINCT p.unit_code FROM pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.client_code = b.client_code and p.unit_code = b.unit_code  " + where + ")");

            // d.operation("UPDATE pay_pro_master SET month_end = " + month_end + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and unit_code in (SELECT DISTINCT p.unit_code FROM pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.client_code = b.client_code and p.unit_code = b.unit_code  " + where + ")");
            int month = int.Parse(txt_month.Text.Substring(0, 2));
            int year = int.Parse(txt_month.Text.Substring(3));
            string start_date_common = get_start_date();
            // Akshay chang
            //if (start_date_common != "" && start_date_common != "1")
            //{
            //    //d.operation("delete from " + to_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //   // d.operation("insert into " + to_table + " select * from " + from_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //    month = month - 1;
            //    if (month == 0)
            //    {
            //        year = year - 1;
            //        month = 12;
            //    }
            //   // d.operation("delete from " + to_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //    //d.operation("insert into " + to_table + " select * from " + from_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");

            //    if (month_end == 1)
            //    {
            //       string month_end1 = d.getsinglestring("select month_end from pay_billing_master_history where month_end = 0 and comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and billing_unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //       if (!month_end1.Equals("0"))
            //       {
            //          // d.operation("delete from " + from_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");   
            //       }
            //    }
            //}
            //else
            //{
            //    //d.operation("UPDATE pay_attendance_muster  SET month_end =0 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //    //d.operation("UPDATE pay_salary_unit_rate  SET month_end =0 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //    //d.operation("UPDATE pay_billing_unit_rate  SET month_end =0 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");



            //    //d.operation("insert into " + to_table + " select * from " + from_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //    //d.operation("delete from " + from_table + " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and MONTH = '" + month + "' AND YEAR='" + year + "' and unit_code in (select unit_Code from pay_unit_master " + where + ")");
            //}

        }
    }

    protected string get_start_date()
    {
        return d1.getsinglestring("select start_date_common FROM pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE MONTH = '" + txt_month.Text.Substring(0, 2) + "' AND YEAR='" + txt_month.Text.Substring(3, 4) + "' and pay_unit_master.client_code ='" + ddl_client1.SelectedValue + "' and pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "'");
    }
    protected string get_end_date()
    {
        return d1.getsinglestring("select end_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + ddl_client1.SelectedValue + "' and pay_billing_master.comp_code='" + Session["COMP_CODE"].ToString() + "'");
    }
    //Payment History 
    protected void payment_client_code()
    {
        ddl_client1.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' ORDER BY client_code", d.con);
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //receving_amount.Visible = false;
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT payment_history.Id, payment_history.comp_code, payment_history.Invoice_No AS 'Invoice No', (SELECT CLIENT_NAME FROM pay_client_master WHERE pay_client_master.client_code = payment_history.client_code AND pay_client_master.comp_code = payment_history.comp_code) AS 'Client Name', payment_history.state AS 'State',(SELECT UNIT_NAME FROM pay_unit_master WHERE pay_unit_master.unit_code = payment_history.unit_code AND pay_unit_master.comp_code = payment_history.comp_code) AS 'Branch', ROUND(payment_history.billing_amt, 2) AS 'Bill Amount',round( IFNULL((payment_history.billing_amt - SUM(received_amt)), 0),2) AS 'Balanced Amount', IFNULL(SUM(received_amt), 0) AS 'Received Amount', concat(payment_history.month, '/',payment_history.year) as 'MONTH', payment_history.Bill_Date AS 'Bill Date', ROUND(payment_history.GST_Amount, 2) AS 'GST' FROM payment_history LEFT JOIN payment_history_details ON payment_history.Invoice_No = payment_history_details.Invoice_No AND payment_history.client_code = payment_history_details.client_code AND payment_history.month = payment_history_details.month AND payment_history.year = payment_history_details.year WHERE payment_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' and payment_history.month='" + txt_date.Text.Substring(0, 2) + "' and payment_history.year='" + txt_date.Text.Substring(3) + "' GROUP BY payment_history.Invoice_No,payment_history.client_code order by Id", d.con1);
            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gv_payment.DataSource = ds1.Tables[0];
                gv_payment.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('No Matching Records Found !!!')", true);
                gv_payment.DataSource = null;
                gv_payment.DataBind();
            }
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void gv_payment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["id"] = gv_payment.SelectedRow.Cells[0].Text;

        d1.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd2 = new MySqlCommand("select Invoice_No,client_code,state,unit_code,round(billing_amt ,2) as 'billing_amt' FROM payment_history where id = " + ViewState["id"].ToString(), d1.con1);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_invoice_no.Text = dr2.GetValue(0).ToString();
                payment_client_code();
                ddl_client.SelectedValue = dr2.GetValue(1).ToString();
                ddl_client_SelectedIndexChanged(null, null);
                ddl_state.SelectedValue = dr2.GetValue(2).ToString();
                ddl_state_SelectedIndexChanged(null, null);
                try
                {
                    ddl_branch.SelectedValue = dr2.GetValue(3).ToString();
                }
                catch { }
                txt_bill_amount.Text = dr2.GetValue(4).ToString();
            }
            dr2.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); payment_details(); }

    }

    protected void gv_payment_RowDataBound(object sender, GridViewRowEventArgs e)
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

        }
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;

    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' order by 1", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_state.DataSource = dt_item;
                    ddl_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_state.DataBind();
                }
                ddl_state.Items.Insert(0, "ALL");
                dt_item.Dispose();
                d.con.Close();
                // ddl_statename_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }
        else
        {
            ddl_state.Items.Clear();
            //ddl_unitcode.Items.Clear();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int month = int.Parse(txt_date.Text.Substring(0, 2));
        int year = int.Parse(txt_date.Text.Substring(3));

        string received_amt = d.getsinglestring("Select IFNULL(SUM(received_amt), 0) From payment_history_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and Invoice_No='" + txt_invoice_no.Text + "' and client_code='" + ddl_client.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' order by Id");

        if (double.Parse(txt_bill_amount.Text) >= (double.Parse(received_amt) + double.Parse(txt_receive_amount.Text)))
        {
            d.operation("Insert Into payment_history_details (comp_code,invoice_no,client_code,state,unit_code,billing_amt,received_amt,received_date,month,year) Values('" + Session["COMP_CODE"].ToString() + "','" + txt_invoice_no.Text + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + txt_bill_amount.Text + "','" + txt_receive_amount.Text + "',str_to_date('" + txt_receive_date.Text + "','%d/%m/%Y'),'" + month + "','" + year + "')");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount Exceeds than Billing Amount !!!')", true);

        }
        payment_details();
        btn_submit_Click(null, null);
        text_clear();
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_state.SelectedValue != "ALL")
        {
            ddl_branch.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "' ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_branch.DataSource = dt_item;
                    ddl_branch.DataTextField = dt_item.Columns[0].ToString();
                    ddl_branch.DataValueField = dt_item.Columns[1].ToString();
                    ddl_branch.DataBind();
                }
                ddl_branch.Items.Insert(0, "ALL");
                dt_item.Dispose();
                d.con.Close();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void payment_details()
    {
        d.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("select Invoice_No as 'Invoice No',(SELECT CLIENT_NAME FROM pay_client_master WHERE pay_client_master.client_code = payment_history_details.client_code AND pay_client_master.comp_code = payment_history_details.comp_code) AS 'Client Name',state as 'State',(SELECT UNIT_NAME FROM pay_unit_master WHERE pay_unit_master.unit_code = payment_history_details.unit_code AND pay_unit_master.comp_code = payment_history_details.comp_code) as 'Branch',Round(billing_amt,2) as 'Bill Amount',Round(received_amt,2) as 'Received Amount',month,year,received_date as 'Received Date' from payment_history_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and Invoice_No='" + txt_invoice_no.Text + "' and client_code='" + ddl_client.SelectedValue + "' and month='" + txt_date.Text.Substring(0, 2) + "' and year='" + txt_date.Text.Substring(3) + "' order by Id", d.con1);
            adp1.Fill(ds1);
            gv_payment_detail.DataSource = ds1.Tables[0];
            gv_payment_detail.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void gv_payment_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_payment.UseAccessibleHeader = false;
            gv_payment.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void text_clear()
    {
        txt_invoice_no.Text = "";
        //ddl_client.DataSource = null;
        //ddl_client.DataBind();

        //ddl_state.DataSource = null;
        //ddl_state.DataBind();

        //ddl_branch.DataSource = null;
        //ddl_branch.DataBind();

        ddl_client.Items.Clear();
        ddl_state.Items.Clear();
        ddl_branch.Items.Clear();

        ddl_branch.DataSource = null;
        ddl_branch.DataBind();

        gv_payment_detail.DataSource = null;
        gv_payment_detail.DataBind();

        txt_bill_amount.Text = "";
        txt_receive_amount.Text = "0";
        txt_receive_date.Text = "";
    }

    //MiniBank
    public void comp_data()
    {
        try
        {
            txt_comp_name.Text = d.getsinglestring("Select Company_name from pay_company_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' ");

            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT (SELECT COMPANY_NAME FROM pay_company_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'COMPANY NAME', Account_balance as 'BALANCE',(SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_minibank_master.client_code) AS 'CLIENT',Amount as 'Received Amount',CONCAT(MONTH,'/',YEAR) as MONTH,receive_date as 'Received Date' FROM pay_minibank_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' ", d.con1);
            adp1.Fill(ds1);
            gv_minibank.DataSource = ds1.Tables[0];
            gv_minibank.DataBind();
            d.con1.Close();


            ddl_bank_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE is null", d.con);
            d.con.Open();



            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_bank_name.DataSource = dt_item;
                ddl_bank_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_bank_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_bank_name.DataBind();

            }
            ddl_bank_name.Items.Insert(0, "Select");
            dt_item.Dispose();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }

    protected void btn_minibank_submit_Click(object sender, EventArgs e)
    {
        int res = 0;
        res = d.operation("Insert Into pay_minibank_master(comp_code,client_code,Bank_name,Account_number,Account_balance,client_bank_name,client_ac_number,month,year,receive_date,payment_type,Amount) values('" + Session["COMP_CODE"].ToString() + "','" + ddl_minibank_client.SelectedValue + "','" + ddl_bank_name.SelectedValue + "','" + ddl_comp_ac_number.SelectedValue + "','" + txt_ac_balanced.Text + "','" + ddl_client_bank.SelectedValue + "','" + ddl_client_ac_number.SelectedValue + "','" + txt_minibank_month.Text.Substring(0, 2) + "','" + txt_minibank_month.Text.Substring(3) + "',str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y'),'" + ddl_payment_type.SelectedValue + "','" + txt_minibank_amount.Text + "')");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Saved Successfully !!!')", true);
        }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Failed !!!')", true); }
    }

    protected void gv_minibank_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_minibank, "Select$" + e.Row.RowIndex);

        }
    }
    protected void gv_minibank_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_bank_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_comp_ac_number.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_bank_name.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_comp_ac_number.DataSource = dt_item;
                ddl_comp_ac_number.DataTextField = dt_item.Columns[0].ToString();
                ddl_comp_ac_number.DataValueField = dt_item.Columns[0].ToString();
                ddl_comp_ac_number.DataBind();

            }
            //ddl_comp_ac_number.Items.Insert(0, "Select");
            dt_item.Dispose();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }

    // minibank client
    protected void ddl_minibank_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_client_bank.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE ='" + ddl_minibank_client.SelectedValue + "' ", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_bank.DataSource = dt_item;
                ddl_client_bank.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_bank.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_bank.DataBind();
            }
            ddl_client_bank.Items.Insert(0, "Select");
            dt_item.Dispose();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }
    protected void ddl_client_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_client_ac_number.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_client_bank.SelectedValue + "' and client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_ac_number.DataSource = dt_item;
                ddl_client_ac_number.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_ac_number.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_ac_number.DataBind();

            }
            //ddl_client_ac_number.Items.Insert(0, "Select");
            dt_item.Dispose();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }



    protected void gv_minibank_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_minibank.UseAccessibleHeader = false;
            gv_minibank.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }


    //billing details


    protected void ddl_billing_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        /// komal changes 12-06-2020 
        /// 
        if (ddl_billing_type.SelectedValue == "8")
        {
            panel_manual_invoice_type.Visible = true;


        }

        if (ddl_billing_type.SelectedValue == "9")
        {
            panel_state_cr_dn.Visible = false;
            panel_branch_name.Visible = false;

            panel_note_number.Visible = true;

        }



        from_date_to_date();

        if (ddl_billing_type.SelectedValue == "2")
        {
            convence.Visible = true;
        }
        else
        {
            convence.Visible = false;
            ddl_conveyance_type.SelectedValue = "0";
        }
        billing_panel.Visible = false;
        ///ddl_bill_client.SelectedValue = "Select";
        ddl_bill_state.Items.Clear();
        ddl_bill_unit.Items.Clear();
        desigpanel.Visible = false;
        gv_billing_details.DataSource = null;
        gv_billing_details.DataBind();

    }
    protected void ddl_bill_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_bill_state.Items.Clear();
        if (ddl_bill_client.SelectedValue != "Select")
        {
            //State
            ddl_bill_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;

            if (ddl_billing_type.SelectedValue.Equals("0"))
            {
                cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_billing_unit_rate_history where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_bill_client.SelectedValue + "'  and  month='" + txt_bill_date.Text.Substring(0, 2) + "' and  year ='" + txt_bill_date.Text.Substring(3) + "' and invoice_flag != 0 order by STATE_NAME", d.con);
            }
            else
            {

                string bill_type = ddl_billing_type.SelectedValue.Equals("1") ? "and type = 'Material'" : ddl_billing_type.SelectedValue.Equals("2") ? "and type = 'Conveyance'" : ddl_billing_type.SelectedValue.Equals("3") ? "and type = 'DeepClean'" : ddl_billing_type.SelectedValue.Equals("4") ? "and type = 'PestControl'" : ddl_billing_type.SelectedValue.Equals("5") ? "and type = ''" : "";

                if (ddl_billing_type.SelectedValue.Equals("6"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct(STATE_NAME) FROM `pay_billing_unit_rate_history` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `start_date` != '0' AND `end_date` != '0' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and invoice_flag != 0 order by STATE_NAME ", d.con);
                }

                    //arrears bill komal 02-05-2020
                else if (ddl_billing_type.SelectedValue.Equals("7"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct(STATE_NAME) FROM `pay_billing_unit_rate_history_arrears` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and invoice_flag = '1' order by STATE_NAME ", d.con);
                }
                //end



                else if (!ddl_billing_type.SelectedValue.Equals("5") && !ddl_billing_type.SelectedValue.Equals("8"))
                {
                    cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_billing_material_history where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_bill_client.SelectedValue + "'  and  month='" + txt_bill_date.Text.Substring(0, 2) + "' and  year ='" + txt_bill_date.Text.Substring(3) + "' and invoice_flag != 0  " + bill_type + " order by STATE_NAME", d.con);
                }
                else if ((!ddl_billing_type.SelectedValue.Equals("6") || ddl_billing_type.SelectedValue.Equals("7")) && !ddl_billing_type.SelectedValue.Equals("8"))
                {
                    cmd_item = new MySqlDataAdapter("select distinct(STATE) from pay_billing_rental_machine where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_bill_client.SelectedValue + "'  and  month='" + txt_bill_date.Text.Substring(0, 2) + "' and  year ='" + txt_bill_date.Text.Substring(3) + "' and invoice_flag != 0   order by STATE", d.con);
                }


                // komal changes 13-06-2020
                if (ddl_billing_type.SelectedValue.Equals("8") || ddl_manual_invoice_type.SelectedValue.Equals(0))
                {

                    cmd_item = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_bill_client.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_bill_client.SelectedValue + "')  ORDER BY STATE", d.con);

                }

                // end
                  if (ddl_billing_type.SelectedValue.Equals("10"))
                {
                    cmd_item = new MySqlDataAdapter("select distinct(STATE_name) from pay_billing_r_m  where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_bill_client.SelectedValue + "'  and  month='" + txt_bill_date.Text.Substring(0, 2) + "' and  year ='" + txt_bill_date.Text.Substring(3) + "' and invoice_flag != 0   order by STATE_name", d.con);
                }
               if (ddl_billing_type.SelectedValue.Equals("11"))
                {
                    cmd_item = new MySqlDataAdapter("select distinct(STATE_name) from pay_billing_admini_expense  where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_bill_client.SelectedValue + "'  and  month='" + txt_bill_date.Text.Substring(0, 2) + "' and  year ='" + txt_bill_date.Text.Substring(3) + "' and invoice_flag != 0   order by STATE_name", d.con);
                }

            }
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_bill_state.DataSource = dt_item;
                    ddl_bill_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_bill_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_bill_state.DataBind();
                }
                ddl_bill_state.Items.Insert(0, "ALL");
                dt_item.Dispose();
                d.con.Close();
                region();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); billing_panel.Visible = false; }
        }
        else
        {
            ddl_statename.Items.Clear();
            ddl_unitcode.Items.Clear();
        }
    }
    protected void ddl_bill_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_bill_unit.Items.Clear();
        if (ddl_bill_state.SelectedValue != "ALL")
        {
            //vinod for region
            string where_state = "";
            if (ddl_bill_state.SelectedValue.Equals("Maharashtra") && ddl_bill_client.SelectedValue.Equals("BAGIC") && int.Parse(txt_bill_date.Text.Replace("/", "")) > 42020 && ddl_billing_type.SelectedValue.Equals("0")) { where_state = " and state='" + ddl_bill_state.SelectedValue + "' and billingwise_id = 5"; }
            if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + ddl_bill_client.SelectedValue + "'" + where_state).Equals("5"))
            {
                where_state = " and pay_billing_unit_rate_history.zone = '" + ddlregion.SelectedValue + "' ";
                if (!ddl_billing_type.SelectedValue.Equals("0"))
                {
                    where_state = " and zone = '" + ddlregion.SelectedValue + "' ";
                }
            }
            else
            { where_state = ""; }


            ddl_bill_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;
            if (ddl_billing_type.SelectedValue.Equals("0"))
            {
                cmd_item = new MySqlDataAdapter("SELECT distinct pay_unit_master. unit_code,CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME'  FROM  pay_unit_master  inner join pay_billing_unit_rate_history on pay_unit_master.comp_code = pay_billing_unit_rate_history.comp_code and pay_unit_master.unit_code = pay_billing_unit_rate_history.unit_code WHERE pay_billing_unit_rate_history. comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history. client_code  = '" + ddl_bill_client.SelectedValue + "' AND pay_billing_unit_rate_history. state_name  = '" + ddl_bill_state.SelectedValue + "' AND  month  = '" + txt_bill_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_bill_date.Text.Substring(3) + "' AND  invoice_flag  = 1 " + where_state + " ORDER BY pay_billing_unit_rate_history. UNIT_NAME ", d.con);
            }
            else
            {
                string bill_type = ddl_billing_type.SelectedValue.Equals("1") ? "and type = 'Material'" : ddl_billing_type.SelectedValue.Equals("2") ? "and type = 'Conveyance'" : ddl_billing_type.SelectedValue.Equals("3") ? "and type = 'DeepClean'" : ddl_billing_type.SelectedValue.Equals("4") ? "and type = 'PestControl'" : ddl_billing_type.SelectedValue.Equals("5") ? "and type = ''" : "";

                //from to date  komal 02-05-2020
                if (ddl_billing_type.SelectedValue.Equals("6"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct unit_code,unit_name FROM `pay_billing_unit_rate_history` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `start_date` != '0' AND `end_date` != '0' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "' and invoice_flag!='0' ", d.con);
                }
                //end

                    //arrears bill  komal 02-05-2020
                else if (ddl_billing_type.SelectedValue.Equals("7"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct unit_code,unit_name FROM `pay_billing_unit_rate_history_arrears` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "' and invoice_flag='1' ", d.con);
                }

                    //end

                else if (!ddl_billing_type.SelectedValue.Equals("5"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct pay_unit_master. unit_code,CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME'  FROM  pay_unit_master  inner join pay_billing_material_history as pay_billing_unit_rate_history on pay_unit_master.comp_code = pay_billing_unit_rate_history.comp_code and pay_unit_master.unit_code = pay_billing_unit_rate_history.unit_code WHERE pay_billing_unit_rate_history. comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history. client_code  = '" + ddl_bill_client.SelectedValue + "' AND pay_billing_unit_rate_history. state_name  = '" + ddl_bill_state.SelectedValue + "' AND  month  = '" + txt_bill_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + " AND  invoice_flag  = 1   " + bill_type + "  ORDER BY pay_billing_unit_rate_history. UNIT_NAME ", d.con);
                }
                else
                {

                    cmd_item = new MySqlDataAdapter("SELECT distinct pay_unit_master. unit_code,CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME'  FROM  pay_unit_master  inner join pay_billing_rental_machine as pay_billing_unit_rate_history on pay_unit_master.comp_code = pay_billing_unit_rate_history.comp_code and pay_unit_master.unit_code = pay_billing_unit_rate_history.unit_code WHERE pay_billing_unit_rate_history. comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history. client_code  = '" + ddl_bill_client.SelectedValue + "' AND pay_billing_unit_rate_history. state  = '" + ddl_bill_state.SelectedValue + "' AND  month  = '" + txt_bill_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + " AND  invoice_flag  = 1   ORDER BY pay_billing_unit_rate_history. UNIT_NAME ", d.con);
                }
                 if (ddl_billing_type.SelectedValue.Equals("10"))
                {
                    cmd_item = new MySqlDataAdapter("SELECT distinct pay_unit_master. unit_code,CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME'  FROM  pay_unit_master  inner join pay_billing_r_m as pay_billing_unit_rate_history on pay_unit_master.comp_code = pay_billing_unit_rate_history.comp_code and pay_unit_master.unit_code = pay_billing_unit_rate_history.unit_code WHERE pay_billing_unit_rate_history. comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history. client_code  = '" + ddl_bill_client.SelectedValue + "' AND pay_billing_unit_rate_history. state_name  = '" + ddl_bill_state.SelectedValue + "' AND  month  = '" + txt_bill_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + " AND  invoice_flag  = 1   " + bill_type + "  ORDER BY pay_billing_unit_rate_history. UNIT_NAME ", d.con);
                }
                 if (ddl_billing_type.SelectedValue.Equals("11"))
                 {
                     cmd_item = new MySqlDataAdapter("SELECT distinct pay_unit_master. unit_code,CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME'  FROM  pay_unit_master  inner join pay_billing_admini_expense as pay_billing_unit_rate_history on pay_unit_master.comp_code = pay_billing_unit_rate_history.comp_code and pay_unit_master.unit_code = pay_billing_unit_rate_history.unit_code WHERE pay_billing_unit_rate_history. comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_billing_unit_rate_history. client_code  = '" + ddl_bill_client.SelectedValue + "' AND pay_billing_unit_rate_history. state_name  = '" + ddl_bill_state.SelectedValue + "' AND  month  = '" + txt_bill_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + " AND  invoice_flag  = 1   " + bill_type + "  ORDER BY pay_billing_unit_rate_history. UNIT_NAME ", d.con);
                 }

            }
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_bill_unit.DataSource = dt_item;
                    ddl_bill_unit.DataTextField = dt_item.Columns[1].ToString();
                    ddl_bill_unit.DataValueField = dt_item.Columns[0].ToString();
                    ddl_bill_unit.DataBind();
                }
                ddl_bill_unit.Items.Insert(0, "ALL");
                dt_item.Dispose();
                d.con.Close();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                billing_panel.Visible = false;
            }
        }
    }
    protected void btn_bill_view_Click(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            gv_billing_details.DataSource = null;
            gv_billing_details.DataBind();
            string where1 = "";
            string where = "", flag = "and invoice_flag != 0";
            DataSet ds1 = new DataSet();
            string where_state = "";
            if (ddl_bill_state.SelectedValue.Equals("Maharashtra") && ddl_bill_client.SelectedValue.Equals("BAGIC") && int.Parse(txt_bill_date.Text.Replace("/", "")) > 42020 && ddl_billing_type.SelectedValue.Equals("0")) { where_state = " and state='" + ddl_bill_state.SelectedValue + "' and billingwise_id = 5"; }
            if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + ddl_bill_client.SelectedValue + "'" + where_state).Equals("5"))
            {
                where_state = " and pay_billing_unit_rate_history.zone = '" + ddlregion.SelectedValue + "' ";
                if (!ddl_billing_type.SelectedValue.Equals("0"))
                {
                    where_state = " and zone = '" + ddlregion.SelectedValue + "' ";
                }
            }
            else
            { where_state = ""; }

            where = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + flag + " group by unit_code";
            if (ddl_bill_state.SelectedValue == "ALL")
            {
                where = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + flag + " group by unit_code";
            }
            else if (ddl_bill_unit.SelectedValue == "ALL")
            {
                if (ddl_billing_type.SelectedValue == "5")
                {
                    where = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + flag + " group by unit_code";
                }
                else
                {

                    where = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + where_state + flag + " group by unit_code";
                }
            }
            MySqlDataAdapter adp1 = null;
            if (ddl_billing_type.SelectedValue.Equals("0"))
            {
                adp1 = new MySqlDataAdapter("SELECT  client as 'CLIENT NAME' ,  state_name  as 'STATE NAME',  unit_name AS 'BRANCH NAME' , CONCAT( month , ' / ',  year ) AS ' BILLING MONTH / YEAR' FROM  pay_billing_unit_rate_history where " + where, d.con1);
            }
            else
            {
                string c_type = "";
                string bill_type = ddl_billing_type.SelectedValue.Equals("1") ? " type = 'Material'" : ddl_billing_type.SelectedValue.Equals("2") ? " type = 'Conveyance'" : ddl_billing_type.SelectedValue.Equals("3") ? " type = 'DeepClean'" : ddl_billing_type.SelectedValue.Equals("4") ? " type = 'PestControl'" : ddl_billing_type.SelectedValue.Equals("5") ? " type = ''" : "";

                if (ddl_billing_type.SelectedValue.Equals("2"))
                {
                    if (ddl_conveyance_type.SelectedValue.Equals("1"))
                    {
                        c_type = " and conveyance_type !=100";
                    }
                    if (ddl_conveyance_type.SelectedValue.Equals("2"))
                    {
                        c_type = " and conveyance_type = 100";
                    }
                }

                // clientwise and other gv 13-06-2020 komal

                if (ddl_billing_type.SelectedValue.Equals("8"))
                {
                    // for clientwise gv

                    if (ddl_manual_invoice_type.SelectedValue == "0")
                    {
                        where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name= '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and `manual_invoice_type` ='0'  and type ='manual' and final_invoice !='0' ";

                        if (ddl_bill_state.SelectedValue == "ALL")
                        {

                            where1 = " ='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and `manual_invoice_type` ='0'  and type ='manual' and final_invoice !='0'";


                        }

                        adp1 = new MySqlDataAdapter("select distinct client_name,state_name, manual_month as ' BILLING MONTH / YEAR' from   pay_report_gst  WHERE " + where1, d.con1);

                    }

                    else
                        // for other manual invoice gv
                        if (ddl_manual_invoice_type.SelectedValue == "1")
                        {

                            where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_name = '" + txt_bill_party.Text + "' and  state_name= '" + ddl_state_name_other.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and `manual_invoice_type` ='1'  and type ='manual' and final_invoice !='0' ";

                            if (ddl_state_name_other.SelectedValue == "ALL")
                            {

                                where1 = " comp_code='" + Session["comp_code"].ToString() + "'   and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and `manual_invoice_type` ='1'  and type ='manual' and final_invoice !='0' ";

                            }

                            adp1 = new MySqlDataAdapter("select distinct state_name as 'State Name',client_name as 'Bill To Party', manual_month as ' BILLING MONTH / YEAR' from   pay_report_gst  WHERE " + where1, d.con1);


                        }
                }

                // clientwise and other gv 13-06-2020 end   


                // credit debit note gv komal 18-06-2020

                if (ddl_billing_type.SelectedValue.Equals("9"))
                {

                    if (ddl_credit_debit_note.SelectedValue=="1") 
                    {

                        adp1 = new MySqlDataAdapter("select distinct `credit_note_no` as 'Credit/Debit Note No',`original_bill_no` as 'Original Bill No', CONCAT( month , ' / ',  year ) AS ' BILLING MONTH / YEAR' from  credit_debit_note where comp_code = '" + Session["comp_code"].ToString() + "' and `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and `credit_note_no`='" + ddl_credit_debit_no.SelectedValue + "' and type ='1' ", d.con1);
                    
                    }

                    else
                        if (ddl_credit_debit_note.SelectedValue == "2")
                        {

                            adp1 = new MySqlDataAdapter("select distinct credit_note_no as 'Credit/Debit Note No',`original_bill_no` as 'Original Bill No', CONCAT( month , ' / ',  year ) AS ' BILLING MONTH / YEAR' from  credit_debit_note where comp_code = '" + Session["comp_code"].ToString() + "' and `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and `credit_note_no`='" + ddl_credit_debit_no.SelectedValue + "'  and type ='2' ", d.con1);
                        
                        
                        
                        }
                
                }

                // credit debit note gv komal 18-06-2020 end 
              
                
                ///komal 15-04-2020
                if (ddl_billing_type.SelectedValue.Equals("6"))
                {
                    where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name= '" + ddl_bill_state.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' AND `start_date` != '0' AND `end_date` != '0' and invoice_flag != 0 group by unit_code";

                    if (ddl_bill_state.SelectedValue == "ALL")
                    {

                        where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'  AND `start_date` != '0' AND `end_date` != '0' and invoice_flag != 0 group by unit_code";
                    }
                    else if (ddl_bill_unit.SelectedValue == "ALL")
                    {

                        where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'  AND `start_date` != '0' AND `end_date` != '0' and invoice_flag != 0 group by unit_code";

                    }

                    adp1 = new MySqlDataAdapter("SELECT distinct `client` AS 'CLIENT NAME', `state_name` AS 'STATE NAME', `unit_name` AS 'BRANCH NAME', CONCAT(`month`, ' / ', `year`) AS ' BILLING MONTH / YEAR' FROM `pay_billing_unit_rate_history` WHERE " + where1, d.con1);


                }
                /// arrears bill komal 02-05-2020
                else

                    if (ddl_billing_type.SelectedValue.Equals("7"))
                    {
                        where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name= '" + ddl_bill_state.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and invoice_flag = 1 group by unit_code";

                        if (ddl_bill_state.SelectedValue == "ALL")
                        {

                            where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and invoice_flag = 1 group by unit_code";
                        }
                        else if (ddl_bill_unit.SelectedValue == "ALL")
                        {

                            where1 = " comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'  and invoice_flag = 1 group by unit_code";

                        }

                        adp1 = new MySqlDataAdapter("SELECT distinct `client` AS 'CLIENT NAME', `state_name` AS 'STATE NAME', `unit_name` AS 'BRANCH NAME', CONCAT(`month`, ' / ', `year`) AS ' BILLING MONTH / YEAR' FROM `pay_billing_unit_rate_history_arrears` WHERE " + where1, d.con1);


                    }
                    //end

                    else if (!ddl_billing_type.SelectedValue.Equals("5") && !ddl_billing_type.SelectedValue.Equals("8"))
                    {
                        if (!ddl_billing_type.SelectedValue.Equals("9"))
                        {
                        adp1 = new MySqlDataAdapter("SELECT  client as 'CLIENT NAME' ,  state_name  as 'STATE NAME',  unit_name AS 'BRANCH NAME' , CONCAT( month , ' / ',  year ) AS 'BILLING MONTH / YEAR' FROM  pay_billing_material_history where " + bill_type + "  " + c_type + " and " + where, d.con1);
                    
                        }
                        }
                    else if (!ddl_billing_type.SelectedValue.Equals("6") || !ddl_billing_type.SelectedValue.Equals("7"))
                    {
                        // komal chages 13-06-2020
                        if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                        {
                            adp1 = new MySqlDataAdapter("SELECT   CLIENT_NAME as 'CLIENT NAME',  state  as 'STATE NAME',  unit_name AS 'BRANCH NAME' , CONCAT( month , ' / ',  year ) AS 'BILLING MONTH / YEAR' FROM  pay_billing_rental_machine where " + where, d.con1);

                        }
                        //  // komal chages 13-06-2020 end
                    }
                if (ddl_billing_type.SelectedValue.Equals("10") )
                {
                    adp1 = new MySqlDataAdapter("SELECT   CLIENT as 'CLIENT NAME',  state_name  as 'STATE NAME',  unit_name AS 'BRANCH NAME' , CONCAT( month , ' / ',  year ) AS 'BILLING MONTH / YEAR' FROM  pay_billing_r_m where " + where, d.con1);

                }
                if (ddl_billing_type.SelectedValue.Equals("11"))
                {
                    adp1 = new MySqlDataAdapter("SELECT   CLIENT as 'CLIENT NAME',  state_name  as 'STATE NAME',  unit_name AS 'BRANCH NAME' , CONCAT( month , ' / ',  year ) AS 'BILLING MONTH / YEAR' FROM  pay_billing_admini_expense where " + where, d.con1);

                }


            }
            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gv_billing_details.DataSource = ds1.Tables[0];
                gv_billing_details.DataBind();
                d.con1.Close();
                billing_panel.Visible = true;

                btn_attendance.Visible = ddl_billing_type.SelectedValue.Equals("0") ? true : false;
                btn_finance.Visible = ddl_billing_type.SelectedValue.Equals("0") ? true : false;
                btn_invoice.Visible = ddl_billing_type.SelectedValue.Equals("0") ? true : false;

            }
            else
            {

                billing_panel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found !!');", true);

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }

    protected void btn_attendance_Click(object sender, EventArgs e)
    {
        generate_report(3);
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

    }
    protected void btn_finance_Click(object sender, EventArgs e)
    {
        generate_report(2);
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }

    protected void delete_invoice_from_gst()
    {
        string where = "";

        where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
       
        if (ddl_bill_state.SelectedValue == "ALL")
        {
            where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";

        }
        else if (ddl_bill_unit.SelectedValue == "ALL")
        {
               where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
        }

        if(ddl_billing_type.SelectedValue =="0")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'manpower'");
        }
         else if (ddl_billing_type.SelectedValue == "1")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'material'");
        } 
        else if (ddl_billing_type.SelectedValue == "2")
        {
            if (ddl_conveyance_type.SelectedValue == "1")
            {
                d.operation("delete from pay_report_gst " + where + " and type = 'conveyance'");
            }
            else if (ddl_conveyance_type.SelectedValue == "2")
            {
                d.operation("delete from pay_report_gst " + where + " and type = 'driver_conveyance'");
            }
        } 
        else if (ddl_billing_type.SelectedValue == "3")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'deepclean'");
        }
        else if (ddl_billing_type.SelectedValue == "4")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'pest_control'");
        }
        else if (ddl_billing_type.SelectedValue == "5")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'machine_rental'");
        }
        else if (ddl_billing_type.SelectedValue == "6")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'manpower' AND start_date != '0' AND end_date != '0'");
        }
        else if (ddl_billing_type.SelectedValue == "7")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'arrears_manpower'");
        }
        else if (ddl_billing_type.SelectedValue == "8")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'manual'");
        }
        else if (ddl_billing_type.SelectedValue == "10")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'r_and_m_bill'");
        }
        else if (ddl_billing_type.SelectedValue == "11")
        {
            d.operation("delete from pay_report_gst " + where + " and type = 'administrative_bill'");
        }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {


        int result = 0;
        string where = "", where1 = "", unit_code = "", emp_code = "";

        where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
        where1 = " and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'";
        if (ddl_bill_state.SelectedValue == "ALL")
        {
            where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";

        }
        else if (ddl_bill_unit.SelectedValue == "ALL")
        {
            if (ddl_billing_type.SelectedValue == "5")
            {
                where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
            }
            else
            {
                where = " where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
            }
        }

        //vinod for region
        string where_state = "";
        if (ddl_bill_state.SelectedValue.Equals("Maharashtra") && ddl_bill_client.SelectedValue.Equals("BAGIC") && int.Parse(txt_bill_date.Text.Replace("/", "")) > 42020 && ddl_billing_type.SelectedValue.Equals("0")) { where_state = " and state='" + ddl_bill_state.SelectedValue + "' and billingwise_id = 5"; }
        if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + ddl_bill_client.SelectedValue + "' " + where_state).Equals("5"))
        {
            where_state = " and pay_billing_unit_rate_history.zone = '" + ddlregion.SelectedValue + "' ";
        }
        else
        { where_state = ""; }


        if (ddl_billing_type.SelectedValue.Equals("0") || ddl_billing_type.SelectedValue.Equals("6") || ddl_billing_type.SelectedValue.Equals("7") || ddl_billing_type.SelectedValue.Equals("8") || ddl_billing_type.SelectedValue.Equals("9"))
        {
            unit_code = d.get_group_concat("select distinct(unit_code) as 'unit_code' from pay_billing_unit_rate_history  " + where + where_state);
            emp_code = d.get_group_concat("select distinct(emp_code) as 'emp_code' from pay_billing_unit_rate_history  " + where + where_state);


            unit_code = unit_code.Replace(",", "','");
            emp_code = emp_code.Replace(",", "','");



            if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
            {
                // komal changes 13-06-2020
                if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                {

                    string unit_name = d.get_group_concat("select distinct(unit_name) from pay_billing_unit_rate_history where unit_code in ('" + unit_code + "') and month_end = 1 " + where1);
                    if (unit_name != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month End Complete This : " + unit_name + "  You can not  Edit !!');", true);
                        return;
                    }
                }
            }
            if (ddl_bill_client.SelectedValue.Equals("RCPL"))
            {
                if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
                {
                    // komal changes 13-06-2020
                    if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                    {

                        result = d.operation("update pay_files_timesheet set flag = '0' , status ='Reject By MD',invoice_flag = '0'  where unit_code in ('" + unit_code + "') " + where1 + " and flag = '2' ");

                        result = d.operation("update pay_attendance_muster set flag = '1',invoice_flag = '0' where  emp_code in ('" + emp_code + "')" + where1);

                        result = d.operation("update pay_billing_unit_rate_history set flag = '1',invoice_flag = '0',status_flag = '2' where unit_code in ('" + unit_code + "')" + where1);

                    }
                }
                else if (ddl_billing_type.SelectedValue.Equals("6"))
                {


                    where = where + "AND `start_date` != '0' AND `end_date` != '0'";
                    result = d.operation("update pay_billing_unit_rate_history set invoice_flag = '0' " + where);

                }

                // arrears bill komal 02-05-2020

                else if (ddl_billing_type.SelectedValue.Equals("7"))
                {
                    result = d.operation("update pay_billing_unit_rate_history_arrears set `invoice_flag` = '0' " + where);

                }
                //end


                // komal changes manual invoice 13-06-2020
                if (ddl_billing_type.SelectedValue.Equals("8"))
                {

                    if (ddl_manual_invoice_type.SelectedValue == "0")
                    {

                        result = d.operation("update pay_report_gst set `final_invoice` = '0'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` = '" + ddl_bill_client.SelectedValue + "' AND `state_name` = '" + ddl_bill_state.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '0'  AND `type` = 'manual'   AND `final_invoice` != '0'");
                    }
                    else
                        if (ddl_manual_invoice_type.SelectedValue == "1")
                        {
                            result = d.operation("update pay_report_gst set `final_invoice` = '0'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_name` = '" + txt_bill_party.Text + "' AND `state_name` = '" + ddl_state_name_other.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '1'  AND `type` = 'manual'   AND `final_invoice` != '0'");

                        }

                }
                // end komal


                // komal changes credit debit note 18-06-2020
                if (ddl_billing_type.SelectedValue.Equals("9"))
                {
                    if (ddl_credit_debit_note.SelectedValue == "1")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '0' where  comp_code = '" + Session["comp_code"].ToString() + "'  and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='1'");
                    }
                    else if (ddl_credit_debit_note.SelectedValue == "2")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '0' where  comp_code = '" + Session["comp_code"].ToString() + "' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='2'");

                    }
                }
                // end komal  credit debit note 18-06-2020



            }

                //Max issue - if a bill of one designation is rejected all bills get rejected for a particular state.
            //else if (ddl_bill_client.SelectedValue.Equals("MAX"))
            //{

            //    result = d.operation("update pay_files_timesheet set flag = '0' , status ='Reject By MD',invoice_flag = '0'  where unit_code in ('" + unit_code + "') " + where1 + " and flag = '2' ");

            //    result = d.operation("update pay_attendance_muster set flag = '1',invoice_flag = '0' where  emp_code in ('" + emp_code + "')" + where1);

            //    result = d.operation("update pay_billing_unit_rate_history set flag = '1',invoice_flag = '0',status_flag = '2' where emp_code in ('" + emp_code + "')" + where1);
            //}
            else
            {
                if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
                {
                    // komal changes 13-06-2020
                    if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                    {
                        result = d.operation("update pay_files_timesheet set flag = '0' , status ='Reject By MD',invoice_flag = '0'  where unit_code in ('" + unit_code + "') " + where1 + " and flag = '2' ");

                        result = d.operation("update pay_attendance_muster set flag = '1',invoice_flag = '0' where  emp_code in ('" + emp_code + "')" + where1);

                        result = d.operation("update pay_billing_unit_rate_history set flag = '1',invoice_flag = '0',status_flag = '2' where emp_code in ('" + emp_code + "')" + where1);

                    }

                }
                // from to date 16-04-2020 komal
                if (ddl_billing_type.SelectedValue.Equals("6"))
                {

                    where = where + "AND `start_date` != '0' AND `end_date` != '0'";
                    result = d.operation("update pay_billing_unit_rate_history set invoice_flag ='0' " + where);

                }
                //end

                // arrears bill komal 02-05-2020
                if (ddl_billing_type.SelectedValue.Equals("7"))
                {
                    result = d.operation("update pay_billing_unit_rate_history_arrears set `invoice_flag` ='0' " + where);

                }
                //end

                // komal changes manual invoice 13-06-2020
                if (ddl_billing_type.SelectedValue.Equals("8"))
                {
                    if (ddl_manual_invoice_type.SelectedValue == "0")
                    {
                        result = d.operation("update pay_report_gst set `final_invoice` = '0'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` = '" + ddl_bill_client.SelectedValue + "' AND `state_name` = '" + ddl_bill_state.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '0'  AND `type` = 'manual'   AND `final_invoice` != '0'");
                    }
                    else
                        if (ddl_manual_invoice_type.SelectedValue == "1")
                        {
                            result = d.operation("update pay_report_gst set `final_invoice` = '0'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_name` = '" + txt_bill_party.Text + "' AND `state_name` = '" + ddl_state_name_other.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '1'  AND `type` = 'manual'   AND `final_invoice` != '0'");

                        }
                }
                // end komal


                // komal changes credit debit note 18-06-2020
                if (ddl_billing_type.SelectedValue.Equals("9"))
                {
                    if (ddl_credit_debit_note.SelectedValue == "1")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '0' where  comp_code = '" + Session["comp_code"].ToString() + "'  and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='1'");
                    }
                    else if (ddl_credit_debit_note.SelectedValue == "2")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '0' where  comp_code = '" + Session["comp_code"].ToString() + "'  and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='2'");

                    }
                }
                // end komal  credit debit note 18-06-2020





            }
        }
        else if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
        {
            // komal changes 13-06-2020 
            if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
            {


                string bill_type = ddl_billing_type.SelectedValue.Equals("1") ? " and type = 'Material'" : ddl_billing_type.SelectedValue.Equals("2") ? " and type = 'Conveyance'" : ddl_billing_type.SelectedValue.Equals("3") ? " and type = 'DeepClean'" : ddl_billing_type.SelectedValue.Equals("4") ? " and type = 'PestControl'" : ddl_billing_type.SelectedValue.Equals("5") ? " and type = ''" : "";

                if (!ddl_billing_type.SelectedValue.Equals("5"))
                {
                    unit_code = d.getsinglestring("select group_concat(distinct(unit_code)) as 'unit_code' from pay_billing_material_history  " + where + " " + bill_type);

                    unit_code = unit_code.Replace(",", "','");
                    string ctype = "";
                    if (ddl_billing_type.SelectedValue.Equals("2"))
                    {
                        if (ddl_conveyance_type.SelectedValue.Equals("2"))
                        {
                            ctype = " and conveyance_type ='100'";
                        }
                        if (ddl_conveyance_type.SelectedValue.Equals("1"))
                        {
                            ctype = " and conveyance_type !='100'";

                        }
                    }


                    string unit_name = d.getsinglestring("select distinct group_concat(unit_name) from pay_billing_material_history where unit_code in ('" + unit_code + "') and month_end = 1 " + where1 + " " + bill_type + " " + ctype + "");
                    if (unit_name != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month End Complete This : " + unit_name + "  You can not  Edit !!');", true);
                        return;
                    }
                    else
                    {

                        result = d.operation("update pay_billing_material_history set invoice_flag = '0',status_flag = '2' where   unit_code in ('" + unit_code + "') " + where1 + " " + bill_type + " " + ctype + "");
                        if (bill_type == " and type = 'Material'")
                        {
                            result = d.operation("update pay_material_details set material_flag = '2', material_status = 'Rejected By MD' where   unit_code in ('" + unit_code + "') " + where1 + " ");
                        }
                        if (ddl_conveyance_type.SelectedValue.Equals("1"))
                        {
                            result = d.operation("update pay_conveyance_amount_history set conveyance_flag = '2', con_emp_status = 'Rejected By MD' where   unit_code in ('" + unit_code + "') " + where1 + "and conveyance_rate != 0  ");
                        }
                        if (ddl_conveyance_type.SelectedValue.Equals("2"))
                        {
                            result = d.operation("update pay_conveyance_amount_history set driver_conv_flag = '2', con_driver_status = 'Rejected By MD' where   unit_code in ('" + unit_code + "') " + where1 + " and conveyance_rate = 0 ");
                        }
                    }

                }
                else
                {
                    unit_code = d.getsinglestring("select group_concat(distinct(unit_code)) as 'unit_code' from pay_billing_rental_machine  " + where + " ");

                    unit_code = unit_code.Replace(",", "','");


                    string unit_name = d.getsinglestring("select distinct group_concat(unit_name) from pay_billing_rental_machine where unit_code in ('" + unit_code + "') and month_end = 1 " + where1 + " ");
                    if (unit_name != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month End Complete This : " + unit_name + "  You can not  Edit !!');", true);
                        return;
                    }
                    else
                    {

                        result = d.operation("update pay_billing_rental_machine set invoice_flag = '0',status_flag = '2' where   unit_code in ('" + unit_code + "') and state='" + ddl_bill_state.SelectedValue + "' " + where1 + " ");
                    }
                }

            }

        } // komal change 15-06-2020
       if (ddl_billing_type.SelectedValue.Equals("10"))
        {

            result = d.operation("update pay_billing_r_m set invoice_flag = '0' where  comp_code = '" + Session["comp_code"].ToString() + "'  and client_code = '" + ddl_bill_client.SelectedValue + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' ");
           
        }
         if (ddl_billing_type.SelectedValue.Equals("11"))
        {

            result = d.operation("update pay_billing_admini_expense  set invoice_flag = '0' where  comp_code = '" + Session["comp_code"].ToString() + "'  and client_code = '" + ddl_bill_client.SelectedValue + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' ");

        }
        //Delete invoice from pay_report_gst function 
        delete_invoice_from_gst();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Editing Permission By MD !!');", true);
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {

        int result = 0;
        int result1 = 0;
        string where = "", where1 = "", unit_code = "", flag = "and invoice_flag != 0 ", invoice_no = "", emp_code;

        where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
        where1 = "and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'";
        if (ddl_bill_state.SelectedValue == "ALL")
        {
            where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";

        }
        else if (ddl_bill_unit.SelectedValue == "ALL")
        {
            where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";

        }


        if (ddl_billing_type.SelectedValue.Equals("0") || ddl_billing_type.SelectedValue.Equals("6") || ddl_billing_type.SelectedValue.Equals("7") || ddl_billing_type.SelectedValue.Equals("8") || ddl_billing_type.SelectedValue.Equals("9"))
        {
            unit_code = d.get_group_concat("select distinct(unit_code) as 'unit_code' from pay_billing_unit_rate_history  " + where + "  " + flag);
            emp_code = d.get_group_concat("select distinct(emp_code) as 'emp_code' from pay_billing_unit_rate_history  " + where + "  " + flag);


            unit_code = unit_code.Replace(",", "','");
            emp_code = emp_code.Replace(",", "','");

            invoice_no = d.getsinglestring("select group_concat(distinct(invoice_no)) as 'invoice_no' from pay_billing_unit_rate_history  " + where + "  " + flag);

            invoice_no = invoice_no.Replace(",", "','");

            if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
            {
                // komal changes 13-06-2020
                if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                {
                    string unit_name = d.getsinglestring("select group_concat(distinct(unit_name)) from pay_billing_unit_rate_history where unit_code in ('" + unit_code + "') and month_end = 1  " + where1);
                    if (unit_name != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This branch : " + unit_name + "  already Approve !!');", true);
                        return;
                    }

                } // komal changes 13-06-2020 end 
            }
            if (ddl_client.SelectedValue.Equals("RCPL"))
            {
                if (!ddl_billing_type.SelectedValue.Equals("6") || !ddl_billing_type.SelectedValue.Equals("7"))
                {
                    // komal changes 13-06-2020
                    if (!ddl_billing_type.SelectedValue.Equals("8") || !ddl_billing_type.SelectedValue.Equals("9"))
                    {
                        result = d.operation("update pay_files_timesheet set flag = '2' , status ='Approve By MD',invoice_flag = '2'  where unit_code in ('" + unit_code + "') " + where1 + " ");

                        result = d.operation("update pay_attendance_muster set flag = '2',invoice_flag = '2' where  emp_code in ('" + emp_code + "')" + where1);

                        result = d.operation("update pay_billing_unit_rate_history set flag = '2',invoice_flag = '2' where unit_code in ('" + unit_code + "')" + where1);

                    } // komal changes 13-06-2020 end 

                }

                else if (ddl_billing_type.SelectedValue.Equals("6"))
                {


                    where = where + "AND `start_date` != '0' AND `end_date` != '0'";

                    result = d.operation("update pay_billing_unit_rate_history set invoice_flag = '2' " + where);

                }

                     // arrears bill komal 02-05-2020
                else if (ddl_billing_type.SelectedValue.Equals("7"))
                {

                    result = d.operation("update pay_billing_unit_rate_history_arrears set invoice_flag = '2' " + where);

                }
                //end


                // komal changes manual invoice 13-06-2020
                if (ddl_billing_type.SelectedValue.Equals("8"))
                {
                    if (ddl_manual_invoice_type.SelectedValue == "0")
                    {

                        result = d.operation("update pay_report_gst set `final_invoice` = '2'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` = '" + ddl_bill_client.SelectedValue + "' AND `state_name` = '" + ddl_bill_state.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '0'  AND `type` = 'manual'   AND `final_invoice` = '1'");
                    }
                    else if (ddl_manual_invoice_type.SelectedValue == "1")
                    {

                        result = d.operation("update pay_report_gst set `final_invoice` = '2'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_name` = '" + txt_bill_party.Text + "' AND `state_name` = '" + ddl_bill_state.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '1'  AND `type` = 'manual'   AND `final_invoice` = '1'");

                    }
                }
                // end 
                
                // komal changes credit debit note 18-06-2020
                if (ddl_billing_type.SelectedValue.Equals("9"))
                {
                    if (ddl_credit_debit_note.SelectedValue == "1")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '2' where  comp_code = '" + Session["comp_code"].ToString() + "' and  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='1'");
                    }
                    else if (ddl_credit_debit_note.SelectedValue == "2")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '2' where  comp_code = '" + Session["comp_code"].ToString() + "' and  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='2'");

                    }
                }
                // end komal  credit debit note 18-06-2020


            }
            else
            {

                if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
                {

                    // komal changes 13-06-2020
                    if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                    {

                        result = d.operation("update pay_files_timesheet set flag = '2' , status ='Approve By MD',invoice_flag = '2'  where unit_code in ('" + unit_code + "') " + where1 + " ");

                        result = d.operation("update pay_attendance_muster set flag = '2',invoice_flag = '2' where  emp_code in ('" + emp_code + "')" + where1);

                        result = d.operation("update pay_billing_unit_rate_history set flag = '2',invoice_flag = '2' where emp_code in ('" + emp_code + "')" + where1);
                        result = d.operation("update pay_report_gst set flag_invoice = '2' where invoice_no in ('" + invoice_no + "')" + where1);

                    } //  komal changes 13-06-2020 end
                }
                else if (ddl_billing_type.SelectedValue.Equals("6"))
                {

                    where = where + "AND `start_date` != '0' AND `end_date` != '0'";
                    result = d.operation("update pay_billing_unit_rate_history set invoice_flag = '2' " + where);
                }

            //arrears bill komal 02-05-2020
                else if (ddl_billing_type.SelectedValue.Equals("7"))
                {

                    result = d.operation("update pay_billing_unit_rate_history_arrears set invoice_flag = '2' " + where);
                }


                // komal changes  manual invoice 13-06-2020
                if (ddl_billing_type.SelectedValue.Equals("8"))
                {
                    if (ddl_manual_invoice_type.SelectedValue == "0")
                    {

                        result = d.operation("update pay_report_gst set `final_invoice` = '2'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` = '" + ddl_bill_client.SelectedValue + "' AND `state_name` = '" + ddl_bill_state.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '0'  AND `type` = 'manual'   AND `final_invoice` = '1'");
                    }

                    else if (ddl_manual_invoice_type.SelectedValue == "1")
                    {

                        result = d.operation("update pay_report_gst set `final_invoice` = '2'  where `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_name` = '" + txt_bill_party.Text + "' AND `state_name` = '" + ddl_state_name_other.SelectedValue + "'  AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "'  AND `year` = '" + txt_bill_date.Text.Substring(3) + "' AND `manual_invoice_type` = '1'  AND `type` = 'manual'   AND `final_invoice` = '1'");

                    }
                }  // end komal


                // komal changes credit debit note 18-06-2020
                if (ddl_billing_type.SelectedValue.Equals("9"))
                {
                    if (ddl_credit_debit_note.SelectedValue == "1")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '2' where  comp_code = '" + Session["comp_code"].ToString() + "' and  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='1'");
                    }
                    else if (ddl_credit_debit_note.SelectedValue == "2")
                    {

                        result = d.operation("update credit_debit_note set `final_invoice_flag` = '2' where  comp_code = '" + Session["comp_code"].ToString() + "' and  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and  `credit_note_no`= '" + ddl_credit_debit_no.SelectedValue + "' and type ='2'");

                    }
                }
                // end komal  credit debit note 18-06-2020



            }
            if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
            {
                // komal changes 13-06-2020
                if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
                {
                    d.operation(" update payment_history set invoice_flag = 2 where invoice_no in ('" + invoice_no + "') and month ='" + txt_bill_date.Text.Substring(0, 2) + "' and year='" + txt_bill_date.Text.Substring(3) + "'");
                    //btn_bill_view_Click(null ,null);
                } // komal changes 13-06-2020 end

            }
        }
        else if (!ddl_billing_type.SelectedValue.Equals("6") && !ddl_billing_type.SelectedValue.Equals("7"))
        {

            // komal changes 13-06-2020
            if (!ddl_billing_type.SelectedValue.Equals("8") && !ddl_billing_type.SelectedValue.Equals("9"))
            {

                string bill_type = ddl_billing_type.SelectedValue.Equals("1") ? " and type = 'Material'" : ddl_billing_type.SelectedValue.Equals("2") ? " and type = 'Conveyance'" : ddl_billing_type.SelectedValue.Equals("3") ? " and type = 'DeepClean'" : ddl_billing_type.SelectedValue.Equals("4") ? " and type = 'PestControl'" : ddl_billing_type.SelectedValue.Equals("5") ? " and type = ''" : "";

                if (!ddl_billing_type.SelectedValue.Equals("5"))
                {
                    unit_code = d.getsinglestring("select group_concat(distinct(unit_code)) as 'unit_code' from pay_billing_material_history  " + where + "  " + flag + " " + bill_type);


                    unit_code = unit_code.Replace(",", "','");


                    string unit_name = d.getsinglestring("select distinct group_concat(unit_name) from pay_billing_material_history where unit_code in ('" + unit_code + "') and month_end = 1 " + where1 + " " + bill_type);
                    if (unit_name != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month End Complete This : " + unit_name + "  You can not  Edit !!');", true);
                        return;
                    }
                    else
                    {
                        string where2 = "";
                        if (ddl_bill_state.SelectedValue != "ALL")
                        {
                            where2 = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name= '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
                        }
                        else
                        {
                            where2 = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "'";
                        }

                        result = d.operation("update pay_billing_material_history set invoice_flag = '2' where   unit_code in ('" + unit_code + "') " + where1 + " " + bill_type);
                        result1 = d.operation("update pay_report_gst set flag_invoice = '2'  " + where2 + " " + bill_type.ToLower());
                    }
                }
                else
                {
                    unit_code = d.getsinglestring("select group_concat(distinct(unit_code)) as 'unit_code' from pay_billing_rental_machine  " + where + "  " + flag + " ");


                    unit_code = unit_code.Replace(",", "','");


                    string unit_name = d.getsinglestring("select distinct group_concat(unit_name) from pay_billing_rental_machine where unit_code in ('" + unit_code + "') and month_end = 1 " + where1 + " ");
                    if (unit_name != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month End Complete This : " + unit_name + "  You can not  Edit !!');", true);
                        return;
                    }
                    else
                    {

                        result = d.operation("update pay_billing_rental_machine set invoice_flag = '2' where   unit_code in ('" + unit_code + "') " + where1 + " ");
                    }
                }

            } //komal changes 13-06-2020 end

        }
         if (ddl_billing_type.SelectedValue.Equals("10") )
        {
            d.operation("update pay_billing_r_m set invoice_flag='2' where  comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'");
        }
        if (ddl_billing_type.SelectedValue.Equals("11"))
        {
            d.operation("update pay_billing_admini_expense set invoice_flag='2' where  comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'");
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved Succesfully !!');", true);

    }

    protected void btn_invoice_Click(object sender, EventArgs e)
    {


        //invoice and bill date 
        string invoice_bill_date = bs.get_invoice_bill_date(Session["COMP_CODE"].ToString(), ddl_bill_client.SelectedValue, ddl_bill_state.SelectedValue, ddl_bill_unit.SelectedValue, ddl_invoice_type.SelectedValue, ddl_designation.SelectedValue, txt_bill_date.Text, 0, 0, "", ddlregion.SelectedValue, 0, "", "",0);

        string invoice = "", bill_date = "";

        if (invoice_bill_date.Equals(""))
        {
            invoice = txt_invoice_no.Text;
            bill_date = txt_bill_date.Text;

        }
        else
        {

            var invoice_bill = invoice_bill_date.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            invoice = invoice_bill[0].ToString();
            bill_date = invoice_bill[1].ToString();
        }

        string dowmloadname = "Invoice";
        string query = null;
        string a = txt_bill_date.Text;
        string firstday = "01/" + txt_bill_date.Text;



        if (ddl_bill_client.SelectedItem.Text.Contains("BAJAJ") && ddl_bill_client.SelectedValue != "4")
        {
            crystalReport.Load(Server.MapPath("~/client_bill_invoice_club_bajaj.rpt"));
        }
        else if (ddl_bill_client.SelectedValue == "4")
        {
            crystalReport.Load(Server.MapPath("~/client_bill_invoice_bfl.rpt"));

        }


        else if (ddl_bill_client.SelectedValue == "HDFC")
        {
            crystalReport.Load(Server.MapPath("~/client_bill_invoice_hdfc.rpt"));
        }
        else if (ddl_bill_client.SelectedValue == "Credence")
        {

            crystalReport.Load(Server.MapPath("~/client_bill_invoice_credence.rpt"));
        }

        else if (ddl_bill_client.SelectedValue == "SUN")
        {
            crystalReport.Load(Server.MapPath("~/client_bill_invoice_sungard.rpt"));
        }
        else if (ddl_bill_client.SelectedValue == "RG")
        {
            crystalReport.Load(Server.MapPath("~/client_bill_invoice_RG.rpt"));
        }
        else
        {
            if (ddl_bill_client.SelectedValue.Equals("ESFB"))
            {
                crystalReport.Load(Server.MapPath("~/client_bill_invoice_equitas.rpt"));
            }
            else { crystalReport.Load(Server.MapPath("~/client_bill_invoice_club.rpt")); }


        }
        //arrear 
        query = bs.get_invoice_query(Session["COMP_CODE"].ToString(), ddl_bill_client.SelectedItem.Text, ddl_bill_client.SelectedValue, ddl_bill_state.SelectedValue, ddl_bill_unit.SelectedValue, ddl_invoice_type.SelectedValue, ddl_designation.SelectedValue, txt_bill_date.Text, 0, 0, "", "", "", "", "", 0, 0,0);
        Session["ReportMonthNo"] = "01";
        ReportLoad(query, dowmloadname, invoice, bill_date);

    }

    private void generate_report(int i)
    {

        string where = "";
        string grade = "";
        string pay_attendance_muster = " pay_attendance_muster ";
        string sql = null;
        string invoice = "";
        string bill_date = "";


        string start_date_common = get_start_date1();
        //invoice and bill date 
        string invoice_bill_date = bs.get_invoice_bill_date(Session["COMP_CODE"].ToString(), ddl_bill_client.SelectedValue, ddl_bill_state.SelectedValue, ddl_bill_unit.SelectedValue, ddl_invoice_type.SelectedValue, ddl_designation.SelectedValue, txt_bill_date.Text, 0, 0, "", ddlregion.SelectedValue, 0, "", "",0);



        if (invoice_bill_date.Equals(""))
        {
            invoice = txt_invoice_no.Text;
            bill_date = txt_bill_date.Text;

        }
        else
        {
            var invoice_bill = invoice_bill_date.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            invoice = invoice_bill[0].ToString();
            bill_date = invoice_bill[1].ToString();
        }




        if (ddl_invoice_type.SelectedValue == "2")
        {
            grade = " and pay_billing_unit_rate_history.grade_code = '" + ddl_designation.SelectedValue + "'";

        }


        d.con.Open();
        try
        {
            if (i == 1)
            {
                where = "pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_unit_master.unit_code = '" + ddl_bill_unit.SelectedValue + "' and pay_attendance_muster.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0  " + grade + " GROUP BY pay_employee_master.emp_code order by 4,3) AS billing_table) as Final_billing";
                if (ddl_bill_state.SelectedValue == "ALL")
                {
                    where = "pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_attendance_muster.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0  " + grade + " GROUP BY pay_employee_master.emp_code order by 4,3) AS billing_table) as Final_billing";
                }
                else if (ddl_bill_unit.SelectedValue == "ALL")
                {
                    where = "pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_bill_state.SelectedValue + "' and pay_attendance_muster.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0  " + grade + " GROUP BY pay_employee_master.emp_code order by 4,3) AS billing_table) as Final_billing";
                }
                sql = "SELECT  state_name ,  unit_name ,  unit_city ,  emp_name ,  grade_desc ,DUTYHRS ,  tot_days_present ,  basic ,  vda ,  emp_basic_vda ,  bonus_rate ,  washing ,  travelling ,  education ,  allowances_esic ,  cca_billing ,  other_allow ,  bonus_gross ,  leave_gross ,  gratuity_gross ,  hra ,  special_allowance ,  gross ,  bonus_after_gross ,  leave_after_gross ,  gratuity_after_gross ,  NH ,  pf ,  esic ,  uniform_ser ,  group_insurance_billing ,  lwf ,  operational_cost ,  allowances_no_esic , ( gross  +  bonus_after_gross  +  leave_after_gross  +  gratuity_after_gross  +  NH  +  pf  +  esic  +  lwf  +  uniform_ser  +  operational_cost  +  allowances_no_esic ) AS 'sub_total_a',  ot_pr_hr_rate ,  esi_on_ot_amount ,  ot_hours , ( ot_pr_hr_rate  +  esi_on_ot_amount ) AS 'sub_total_b', ( gross  +  bonus_after_gross  +  leave_after_gross  +  gratuity_after_gross  +  NH  +  pf  +  esic  +  lwf  +  uniform_ser  +  operational_cost  +  allowances_no_esic  +  ot_pr_hr_rate  +  esi_on_ot_amount ) AS 'sub_total_ab',  relieving_charg , CASE WHEN  emp_cca  = 0 AND  branch_cca  != 0 THEN ((baseamount-bill_ot_rate)) WHEN  emp_cca  != 0 AND  branch_cca  != 0 THEN ((baseamount-bill_ot_rate)) WHEN  emp_cca  = 0 AND  branch_cca  = 0 THEN ((baseamount-bill_ot_rate)) ELSE ( bill_gross  + (( bill_gross  *  esic_percent ) / 100) +  bill_pf +lwf +  bill_uniform  +  group_insurance_billing_ser  +  bonus_after_gross  +  leave_after_gross  +  gratuity_after_gross ) END AS 'sub_total_c',  uniform_no_ser ,  operational_cost_no_ser , IF(((( Total  +  pf  +  esic  + ( ot_rate  *  ot_hours ) +  group_insurance_billing_ser ) *  bill_service_charge ) / 100) = 0,  bill_service_charge_amount , ((( Total  +  pf  +  esic  + ( ot_rate  *  ot_hours ) +  group_insurance_billing_ser ) *  bill_service_charge ) / 100)) AS 'Service_charge', (( Total  + ( ot_rate  *  ot_hours ) +  pf  +  esic  +  group_insurance_billing_ser  +  uniform_no_ser  +  operational_cost_no_ser  +  group_insurance_billing ) + IF(((( Total  +  pf  +  esic  + ( ot_rate  *  ot_hours ) +  group_insurance_billing_ser ) *  bill_service_charge ) / 100) = 0,  bill_service_charge_amount , ((( Total  +  pf  +  esic  + ( ot_rate  *  ot_hours ) +  group_insurance_billing_ser ) *  bill_service_charge ) / 100))) AS 'Amount',  pf_percent  AS 'bill_pf_percent',  esic_percent  AS 'bill_esic_percent',  gratuity_percent ,  hra_percent ,  bill_bonus_percent ,  leave_days ,  bill_service_charge,group_insurance_billing_ser,(ot_rate * ot_hours) as 'ot_amount'  FROM (SELECT  client ,  company_state ,  unit_name ,  state_name ,  unit_city ,  client_branch_code ,  emp_name ,  grade_desc ,  emp_basic_vda ,  hra ,  bonus_gross ,  leave_gross ,  gratuity_gross ,  washing ,  travelling ,  education ,  cca_billing ,  other_allow , ( emp_basic_vda  +  hra  +  bonus_gross  +  leave_gross  +  washing  +  travelling  +  education  +  allowances  +  cca_billing  +  other_allow  +  gratuity_gross  +  hrs_12_ot ) AS 'gross',  bonus_after_gross ,  leave_after_gross ,  gratuity_after_gross , ((( emp_basic_vda ) / 100) *  pf_percent ) AS 'pf', ((( emp_basic_vda  +  hra  +  bonus_gross  +  leave_gross  +  washing  +  travelling  +  education  + IF( esic_oa_billing  = 1,  allowances , 0) +  cca_billing  +  other_allow  +  gratuity_gross  +  hrs_12_ot ) / 100) *  esic_percent ) AS 'esic',  hrs_12_ot  AS 'special_allowance', ((( hrs_12_ot ) *  esic_percent ) / 100) AS 'esic_ot',  lwf , CASE WHEN  bill_ser_uniform  = 1 THEN  uniform  ELSE 0 END AS 'uniform_ser', CASE WHEN  bill_ser_uniform  = 0 THEN  uniform  ELSE 0 END AS 'uniform_no_ser',  relieving_charg , CASE WHEN  bill_ser_operations  = 1 THEN  operational_cost  ELSE 0 END AS 'operational_cost', CASE WHEN  bill_ser_operations  = 0 THEN  operational_cost  ELSE 0 END AS 'operational_cost_no_ser',  tot_days_present , ( emp_basic_vda  +  hra  +  bonus_gross  +  leave_gross  +  washing  +  travelling  +  education  +  allowances  +  cca_billing  +  other_allow  +  gratuity_gross  +  bonus_after_gross  +  leave_after_gross  +  gratuity_after_gross  +  lwf + CASE WHEN  bill_ser_uniform  = 0 THEN 0 ELSE  uniform  END +  relieving_charg  + CASE WHEN  bill_ser_operations  = 0 THEN 0 ELSE  operational_cost  END +  NH  +  hrs_12_ot  + IF( esic_common_allow  = 0,  common_allow , 0)) AS 'Total',  bill_service_charge ,  NH ,  hours , ( bill_gross ) AS 'bill_gross',  sub_total_c ,  bill_ser_uniform ,  bill_ser_operations , (IF(ot_hours > 0,ot_rate,0) + IF(ot_hours > 0 and ot_rate > 0,esi_on_ot_amount,0)) AS 'ot_rate',(ot_rate+esi_on_ot_amount) as 'bill_ot_rate',  ot_hours ,  esic_amount ,  IF(ot_hours > 0,ot_rate,0) AS 'ot_pr_hr_rate',IF(ot_hours > 0 and ot_rate > 0,esi_on_ot_amount,0) as 'esi_on_ot_amount',  emp_cca ,  branch_cca ,  bill_pf ,  bill_uniform , CASE WHEN  service_group_insurance_billing  = 0 THEN  group_insurance_billing  ELSE 0 END AS 'group_insurance_billing', CASE WHEN  service_group_insurance_billing  = 1 THEN  group_insurance_billing  ELSE 0 END AS 'group_insurance_billing_ser',  bill_service_charge_amount ,  branch_type ,  DUTYHRS ,  basic ,  vda ,  bonus_rate , IF( esic_oa_billing  = 1,  allowances , 0) AS 'allowances_esic', IF( esic_oa_billing  = 0,  allowances , 0) AS 'allowances_no_esic',  baseamount ,  pf_percent ,  esic_percent ,  gratuity_percent ,  hra_percent ,  bill_bonus_percent ,  leave_days  FROM (SELECT (SELECT  client_name  FROM  pay_client_master  WHERE  client_code  =  pay_unit_master . client_code  AND  comp_code  =  pay_unit_master . comp_code ) AS 'client',  pay_company_master . state  AS 'company_state',  pay_unit_master . unit_name ,  pay_unit_master . state_name ,  pay_unit_master . unit_city ,  pay_unit_master . client_branch_code ,  pay_employee_master . emp_name ,  pay_grade_master . grade_desc ,  pay_billing_unit_rate . basic ,  pay_billing_unit_rate . vda ,  pay_billing_unit_rate . bonus_rate , CAST(CONCAT( pay_billing_master_history . hours , 'HRS ',  pay_billing_unit_rate . month_days , ' DAYS ') AS char) AS 'DUTYHRS', ((( pay_billing_master_history . basic  +  pay_billing_master_history . vda ) /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'emp_basic_vda', (( pay_billing_unit_rate . hra  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'hra', CASE WHEN  bonus_taxable  = '1' THEN (( pay_billing_unit_rate . bonus_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'bonus_gross', CASE WHEN  bonus_taxable  = '0' THEN (( pay_billing_unit_rate . bonus_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'bonus_after_gross', CASE WHEN  leave_taxable  = '1' THEN (( pay_billing_unit_rate . leave_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'leave_gross', CASE WHEN  leave_taxable  = '0' THEN (( pay_billing_unit_rate . leave_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'leave_after_gross', CASE WHEN  gratuity_taxable  = '1' THEN (( pay_billing_unit_rate . grauity_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'gratuity_gross', CASE WHEN  gratuity_taxable  = '0' THEN (( pay_billing_unit_rate . grauity_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'gratuity_after_gross', (( pay_billing_unit_rate . washing  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'washing', (( pay_billing_unit_rate . traveling  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'travelling', (( pay_billing_unit_rate . education  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'education', (( pay_billing_unit_rate . national_holiday_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'NH', (( pay_billing_unit_rate . allowances  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'allowances', CASE WHEN  pay_employee_master . cca  = 0 THEN (( pay_billing_unit_rate . cca  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE (( pay_employee_master . cca  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) END AS 'cca_billing', CASE WHEN  pay_employee_master . special_allow  = 0 THEN (( pay_billing_unit_rate . otherallowance  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE (( pay_employee_master . special_allow  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) END AS 'other_allow', CASE WHEN  pay_billing_master_history . ot_policy_billing  = '1' THEN (( pay_billing_master_history . ot_amount_billing  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE 0 END AS 'hrs_12_ot',  pay_billing_master_history . bill_esic_percent  AS 'esic_percent',  pay_billing_master_history . bill_pf_percent  AS 'pf_percent',  gratuity_percent ,  pay_billing_master_history . hra_percent ,  pay_billing_master_history . bill_bonus_percent ,  pay_billing_master_history . leave_days , (( pay_billing_unit_rate . lwf  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'lwf', (( pay_billing_unit_rate . uniform  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'uniform', (( pay_billing_unit_rate . relieving_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'relieving_charg', (( pay_billing_unit_rate . operational_cost  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'operational_cost',  pay_attendance_muster . tot_days_present , ROUND((( pay_billing_unit_rate . sub_total_c  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ), 2) AS 'baseamount',  bill_service_charge ,  pay_billing_master_history . hours ,  pay_billing_unit_rate . sub_total_c ,  pay_billing_master_history . bill_ser_operations ,  pay_billing_master_history . bill_ser_uniform , pay_billing_unit_rate.ot_1_hr_amount AS 'ot_rate',  pay_attendance_muster . ot_hours ,  pay_billing_unit_rate . esic_amount ,  pay_billing_unit_rate.esi_on_ot_amount as 'esi_on_ot_amount',  pay_employee_master . cca  AS 'emp_cca',  pay_billing_unit_rate . cca  AS 'branch_cca', ( pay_billing_unit_rate . gross  + (( pay_employee_master . cca  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present )) AS 'bill_gross',  pay_billing_unit_rate . pf_amount  AS 'bill_pf',  pay_billing_unit_rate . uniform  AS 'bill_uniform', (( pay_billing_master_history . group_insurance_billing  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'group_insurance_billing',  service_group_insurance_billing ,  pay_employee_master . Employee_type , (( bill_service_charge_amount  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) AS 'bill_service_charge_amount',  pay_billing_master_history . esic_common_allow , CASE WHEN  pay_employee_master . special_allow  = 0 THEN (( pay_billing_unit_rate . common_allowance  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE (( pay_employee_master . special_allow  /  pay_billing_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) END AS 'common_allow', IFNULL( branch_type , 0) AS 'branch_type',  pay_billing_master_history . esic_oa_billing  FROM pay_employee_master INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours AND pay_billing_master_history.type = 'billing' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE WHERE  " + where;
            }
            //finance copy
            else if (i == 2)
            {
                string rg_terms = "";
                if (ddl_bill_client.SelectedValue == "RG")
                {
                    rg_terms = "AND (emp_code != '' OR emp_code IS NOT NULL)";
                }
                where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + "  and flag != 0  " + rg_terms + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name ";
                if (ddl_bill_state.SelectedValue == "ALL")
                {
                    where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + " and flag != 0 " + rg_terms + "  ORDER BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                }
                else if (ddl_bill_unit.SelectedValue == "ALL")
                {
                    where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + " and flag != 0  " + rg_terms + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                }
                if (ddl_bill_client.SelectedValue == "HDFC")
                {
                    where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + " and flag != 0  group by unit_code  order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    if (ddl_bill_state.SelectedValue == "ALL")
                    {
                        where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + " and flag != 0 group by unit_code order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    }
                    else if (ddl_bill_unit.SelectedValue == "ALL")
                    {
                        where = "where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "'  and month='" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' " + grade + "  and flag != 0 group by unit_code order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    }


                    //  sql = "SELECT client_code,client,state_name,branch_type,unit_name,unit_city,client_branch_code,emp_name,grade_desc,emp_basic_vda,hra,bonus_gross,leave_gross,gratuity_gross,washing,travelling,education,allowances,cca_billing,other_allow,gross,bonus_after_gross,leave_after_gross,gratuity_after_gross,pf,esic,hrs_12_ot,esic_ot,lwf,uniform,relieving_charg,operational_cost,tot_days_present,Amount,Service_charge,CGST9,IGST18,SGST9,bill_service_charge,NH,hours,fromtodate,sub_total_c,ot_rate,ot_hours,ot_amount,group_insurance_billing,bill_service_charge_amount,txt_zone,adminhead_name,ihms,location_type,unit_add1,emp_count,emp_count1,state_per,branch_cost_centre_code,total_emp_count,no_of_duties,zone,TOT_WORKING_DAYS,GRADE_CODE,month_days from pay_billing_unit_rate_history " + where;
                    sql = "SELECT client_code, client, state_name,branch_type, unit_name, unit_city, client_branch_code, emp_name, grade_desc, emp_basic_vda, SUM(hra) as 'hra', SUM(bonus_gross) as 'bonus_gross', SUM(leave_gross) as 'leave_gross', SUM(gratuity_gross) as 'gratuity_gross', SUM(washing) as 'washing', SUM(travelling) as 'travelling', SUM(education) as 'education', SUM(allowances) as 'allowances', SUM(cca_billing) as 'cca_billing', SUM(other_allow) as 'other_allow', SUM(gross) as 'gross', SUM(bonus_after_gross) as 'bonus_after_gross', SUM(leave_after_gross) as 'leave_after_gross', SUM(gratuity_after_gross) as 'gratuity_after_gross', SUM(pf) as 'pf', SUM(esic) as 'esic', SUM(hrs_12_ot) as 'hrs_12_ot' , SUM(esic_ot) as 'esic_ot', SUM(lwf) as 'lwf', SUM(uniform) as 'uniform', SUM(relieving_charg) as 'relieving_charg', SUM(operational_cost) as 'operational_cost', SUM(tot_days_present) as 'tot_days_present',sum(Amount) as 'Amount', SUM(Service_charge) as 'Service_charge', SUM(CGST9) as 'CGST9', SUM(IGST18) as 'IGST18', SUM(SGST9) as 'SGST9', bill_service_charge , NH, hours, fromtodate,sub_total_c, max(`ot_rate`) as 'ot_rate', SUM(ot_hours) as 'ot_hours', SUM(ot_amount) as 'ot_amount', group_insurance_billing, bill_service_charge_amount, txt_zone, adminhead_name, ihms, location_type, unit_add1, emp_count2 AS 'emp_count', emp_count1, state_per, branch_cost_centre_code, SUM(IF(`EMP_TYPE` = 'Permanent', 1, 0)) as 'total_emp_count', sum(no_of_duties) as 'no_of_duties', zone, TOT_WORKING_DAYS, GRADE_CODE, month_days FROM pay_billing_unit_rate_history " + where;
                }
                else
                {

                    // sql = "SELECT client_code,client,state_name,unit_name,unit_city,client_branch_code,emp_name,grade_desc,emp_basic_vda,hra,bonus_gross,leave_gross,gratuity_gross,washing,travelling,education,allowances,cca_billing,other_allow,gross,bonus_after_gross,leave_after_gross,gratuity_after_gross,pf,esic,hrs_12_ot,esic_ot,lwf,uniform,relieving_charg,operational_cost,tot_days_present,Amount,Service_charge,CGST9,IGST18,SGST9,bill_service_charge,NH,hours,fromtodate,sub_total_c,ot_rate,ot_hours,ot_amount,group_insurance_billing,bill_service_charge_amount,bill_service_charge_amount,branch_type,month_days,gst_applicable,OPus_NO from pay_billing_unit_rate_history  " + where;
                    sql = "SELECT client_code,client,state_name,unit_name,unit_city,client_branch_code,emp_name,grade_desc,emp_basic_vda,hra,bonus_gross,leave_gross,gratuity_gross,washing,travelling,education,allowances,cca_billing,other_allow,gross,bonus_after_gross,leave_after_gross,gratuity_after_gross,pf,esic,hrs_12_ot,esic_ot,lwf,uniform,relieving_charg,operational_cost,tot_days_present,Amount,Service_charge,CGST9,IGST18,SGST9,bill_service_charge,NH,hours,fromtodate,sub_total_c,ot_rate,ot_hours,ot_amount,group_insurance_billing,bill_service_charge_amount,bill_service_charge_amount,branch_type,month_days,gst_applicable,OPus_NO,unit_code from pay_billing_unit_rate_history  " + where;
                }




            }
            //client attendance
            else if (i == 3)
            {


                if (start_date_common != "" && start_date_common != "1")
                {
                    //d.update_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_unitcode.SelectedValue, txt_bill_date.Text, int.Parse(start_date_common));
                    where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_bill_client.SelectedValue + "'  and pay_billing_unit_rate_history.unit_code = '" + ddl_bill_unit.SelectedValue + "' and pay_attendance_muster.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  and pay_billing_unit_rate_history.flag != 0 " + grade + " order by 1,2";
                    if (ddl_bill_state.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_attendance_muster.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0 and pay_billing_unit_rate_history.flag != 0" + grade + " order by 1,2";
                    }
                    else if (ddl_bill_unit.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_bill_state.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "'  and pay_billing_unit_rate_history.tot_days_present > 0  and pay_attendance_muster.flag != 0 " + grade + " order by 1,2";
                    }

                    //if (ddl_invoice_type.SelectedValue == "2") { where = " pay_billing_unit_rate_history.grade_code = '" + ddl_designation.SelectedValue + "' and " + where; }
                    string getdays = d.get_calendar_days(int.Parse(start_date_common), txt_bill_date.Text, 1, 2);
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
                    //   sql = "select pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city,pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.client_branch_code, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc,pay_attendance_muster.ot_hours ," + getdays + " pay_attendance_muster.tot_days_present, pay_attendance_muster.tot_days_absent as absent, pay_attendance_muster.tot_working_days as 'total days' from pay_billing_unit_rate_history INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code and pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_code AND   pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code   AND  pay_attendance_muster . month  =  pay_billing_unit_rate_history . month  AND  pay_attendance_muster . year  =  pay_billing_unit_rate_history . year  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(txt_bill_date.Text.Substring(0, 2)) == 1 ? int.Parse(txt_bill_date.Text.Substring(3)) - 1 : int.Parse(txt_bill_date.Text.Substring(3))) + " and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(txt_bill_date.Text.Substring(0, 2)) == 1 ? 12 : int.Parse(txt_bill_date.Text.Substring(0, 2)) - 1) + " where " + where;
                    sql = "select pay_billing_unit_rate_history.state_name,branch_type, pay_billing_unit_rate_history.unit_city,pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.client_branch_code, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc,pay_attendance_muster.ot_hours ," + getdays + " pay_attendance_muster.tot_days_present, pay_attendance_muster.tot_days_absent as absent, pay_attendance_muster.tot_working_days as 'total days' from pay_billing_unit_rate_history INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code and pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_code AND   pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code   AND  pay_attendance_muster . month  =  pay_billing_unit_rate_history . month  AND  pay_attendance_muster . year  =  pay_billing_unit_rate_history . year  left join pay_attendance_muster t2 on  t2.year = " + (int.Parse(txt_bill_date.Text.Substring(0, 2)) == 1 ? int.Parse(txt_bill_date.Text.Substring(3)) - 1 : int.Parse(txt_bill_date.Text.Substring(3))) + " and pay_attendance_muster.COMP_CODE = t2.COMP_CODE and pay_attendance_muster.UNIT_CODE = t2.UNIT_CODE and pay_attendance_muster.EMP_CODE = t2.EMP_CODE and t2.month = " + (int.Parse(txt_bill_date.Text.Substring(0, 2)) == 1 ? 12 : int.Parse(txt_bill_date.Text.Substring(0, 2)) - 1) + " inner join `pay_employee_master` on pay_attendance_muster.emp_code = pay_employee_master.emp_code where " + where;
                }
                else
                {
                    where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.unit_code = '" + ddl_bill_unit.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  and pay_attendance_muster.flag != 0  " + grade + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    if (ddl_bill_state.SelectedValue == "ALL")
                    {
                        where = " pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0  and pay_attendance_muster.flag != 0 " + grade + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    }
                    else if (ddl_bill_unit.SelectedValue == "ALL")
                    {
                        where = "pay_billing_unit_rate_history.comp_code = '" + Session["comp_code"].ToString() + "' and pay_billing_unit_rate_history.client_code = '" + ddl_bill_client.SelectedValue + "' and pay_billing_unit_rate_history.state_name = '" + ddl_bill_state.SelectedValue + "' and pay_billing_unit_rate_history.month = '" + txt_bill_date.Text.Substring(0, 2) + "' and pay_billing_unit_rate_history.Year = '" + txt_bill_date.Text.Substring(3) + "' and pay_billing_unit_rate_history.tot_days_present > 0 and  pay_attendance_muster.flag != 0 " + grade + " order by pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_name";
                    }
                    //  if (ddl_invoice_type.SelectedValue == "2") { where = " pay_billing_unit_rate_history.grade_code = '" + ddl_designation.SelectedValue + "' and " + where; }
                    sql = "select pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city,pay_billing_unit_rate_history.unit_name, pay_billing_unit_rate_history.client_branch_code, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc,pay_attendance_muster.ot_hours , case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster.tot_days_present, CASE WHEN (pay_attendance_muster.tot_working_days - pay_attendance_muster.tot_days_present) < 0 THEN 0 ELSE pay_attendance_muster.tot_working_days - pay_attendance_muster.tot_days_present END AS 'absent',DAY(LAST_DAY('" + txt_bill_date.Text.Substring(3) + "-" + txt_bill_date.Text.Substring(0, 2) + "-1')) AS 'total days' from pay_billing_unit_rate_history INNER JOIN " + pay_attendance_muster + " ON pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code and pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_code  and pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate_history.month AND pay_attendance_muster.year = pay_billing_unit_rate_history.year inner join `pay_employee_master` on pay_attendance_muster.emp_code = pay_employee_master.emp_code where " + where;
                }
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
                    Response.AddHeader("content-disposition", "attachment;filename=RATE_BREAKUP_" + ddl_bill_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");
                }
                else if (i == 2)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=FINANCE_COPY_" + ddl_bill_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");
                }
                else if (i == 3)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ATTENDANCE_COPY_" + ddl_bill_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");
                }
                else if (i == 4)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=SUPPORT_FORMAT_" + ddl_bill_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");
                }
                else if (i == 5)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=CONVEYANCE_FINANCE_COPY_" + ddl_bill_client.SelectedItem.Text.Replace(" ", "_").Replace(",", "_").Replace(".", "_") + ".xls");
                }
                //else if (i == 4)
                //{
                //    Response.AddHeader("content-disposition", "attachment;filename=Attendance_Copy_" + ddl_bill_unit.SelectedItem.Text.Replace(" ", "_") + ".xls");
                //}
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, i, invoice, bill_date, start_date_common, d.get_calendar_days(int.Parse(start_date_common), txt_bill_date.Text, 0, 1));
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, i, invoice, bill_date, start_date_common, "");
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, i, invoice, bill_date, start_date_common, "");
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
    protected string get_start_date1()
    {
        return d1.getsinglestring("select start_date_common FROM pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE MONTH = '" + txt_bill_date.Text.Substring(0, 2) + "' AND YEAR='" + txt_bill_date.Text.Substring(3, 4) + "' and pay_unit_master.client_code ='" + ddl_bill_client.SelectedValue + "' and pay_unit_master.comp_code='" + Session["COMP_CODE"].ToString() + "'");
    }
    private void ReportLoad(string query, string downloadfilename, string invoice, string bill_date)
    {
        try
        {
            //btnsendemail.Visible = true;
            double total_amount = 0, gst = 0;
            string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            cmd.Connection = d.con;
            d.con.Open();
            sda.Fill(dt);

            d.con.Close();
            crystalReport.DataDefinition.FormulaFields["invoice_no"].Text = @"'" + invoice + "'";
            crystalReport.DataDefinition.FormulaFields["bill_date"].Text = @"'" + bill_date + "'";
            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, downloadname);
            crystalReport.Close();
            crystalReport.Clone();
            crystalReport.Dispose();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            d.con.Close();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Response.End();
        }
    }

    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i, i3, state_change = 0;
        string invoice = "";
        string bill_date = "";
        double rate = 0, paid_days = 0, service_charge = 0, grand_tot = 0, cgst = 0, sgst = 0, igst = 0, gst = 0, ctc = 0, present_days = 0, absent_days = 0, total_days = 0, ot_hrs = 0, ot_rate = 0, ot_amount = 0, sub_total = 0, total_emp_count = 0, no_of_duties = 0;

        double rate1 = 0, paid_days1 = 0, service_charge1 = 0, grand_tot1 = 0, cgst1 = 0, sgst1 = 0, igst1 = 0, gst1 = 0, ctc1 = 0, present_days1 = 0, absent_days1 = 0, total_days1 = 0, ot_hrs1 = 0, ot_rate1 = 0, ot_amount1 = 0, sub_total1 = 0, total_emp_count1 = 0, no_of_duties1 = 0;

        //ADD MD 
        string DUTY_HOURS = null, RATE = null, NO_OF_PAID_DAYS = null, BASE_AMOUNT = null, OT_HOURS = null, OT_RATE = null, OT_AMOUNT = null, TOTAL_BASE_AMT_OT_AMT = null, SERVICE_CHARGE = null, GRAND_TOTAL = null, CGST = null, SGST = null, IGST = null, TOTAL_GST = null, TOTAL_CTC = null;
        string header = "", header1 = "", state_name = "";
        string bodystr = "", start_date_common = "", branch_type = "";
        public MyTemplate(ListItemType type, DataSet ds, int i, string invoice, string bill_date, string start_date_common, string header1)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.invoice = invoice;
            this.bill_date = bill_date;
            this.start_date_common = start_date_common;
            this.header1 = header1;
            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {

                case ListItemType.Header:
                    if (i == 1)
                    {
                        header = "";
                        header = header + "<th>BASIC</th>";
                        header = header + "<th>VDA</th>";
                        header = header + "<th>BASIC + VDA</th>";
                        header = header + "<th>BONUS RATE</th>";
                        header = header + "<th>WASHING</th>";
                        header = header + "<th>TRAVELLING</th>";
                        header = header + "<th>EDUCATION</th>";
                        header = header + "<th>OTHER ALLOWANCES</th>";
                        header = header + "<th>CCA</th>";
                        header = header + "<th>ALLOWANCE</th>";
                        header = header + "<th>BONUS " + ds.Tables[0].Rows[ctr]["bill_bonus_percent"] + "% ON SALARY</th>";
                        header = header + "<th>EARNED LEAVES " + ds.Tables[0].Rows[ctr]["leave_days"] + " DAYS ON SALARY</th>";
                        header = header + "<th>GRATUITY " + ds.Tables[0].Rows[ctr]["gratuity_percent"] + " % ON SALARY</th>";

                        header = header + "<th>HRA @ " + ds.Tables[0].Rows[ctr]["hra_percent"] + "%</th>";
                        header = header + "<th>SPECIAL ALLOWANCES</th>";
                        header = header + "<th>GROSS</th>";
                        header = header + "<th>BONUS " + ds.Tables[0].Rows[ctr]["bill_bonus_percent"] + "% ON SALARY</th>";

                        header = header + "<th>EARNED LEAVES " + ds.Tables[0].Rows[ctr]["leave_days"] + " DAYS ON SALARY</th>";

                        header = header + "<th>GRATUITY " + ds.Tables[0].Rows[ctr]["gratuity_percent"] + " % ON SALARY</th>";


                        header = header + "<th>NATIONAL HOLIDAY AMOUNT</th>";
                        header = header + "<th>PF " + ds.Tables[0].Rows[ctr]["bill_pf_percent"] + "% Salary</th>";
                        header = header + "<th>ESIC " + ds.Tables[0].Rows[ctr]["bill_esic_percent"] + "% on Gross</th>";
                        header = header + "<th>UNIFORM</th>";
                        header = header + "<th>GROUP INSURANCE</th>";
                        //if (ds.Tables[0].Rows[ctr]["bill_ser_uniform"].ToString().Equals("1"))
                        //{
                        //    header = header + "<th>UNIFORM</th>";
                        //}
                        header = header + "<th>LWF</th>";
                        header = header + "<th>OPERATIONAL COST</th>";

                        //if (ds.Tables[0].Rows[ctr]["bill_ser_operations"].ToString().Equals("1"))
                        //{
                        //    header = header + "<th>OPERATIONAL COST</th>";
                        //}
                        header = header + "<th>ALLOWANCE</th>";
                        header = header + "<th>SUB TOTAL A</th>";
                        header = header + "<th>OT 1 HR AMOUNT</th>";
                        header = header + "<th>ESIC ON OT AMOUNT</th>";
                        header = header + "<th>OT HOURS</th>";

                        header = header + "<th>SUB TOTAL AMOUNT B</th>";
                        header = header + "<th>SUB TOTAL AB</th>";
                        header = header + "<th>RELIEVING AMOUNT</th>";
                        header = header + "<th>SUB TOTAL C</th>";
                        header = header + "<th>UNIFORM</th>";
                        header = header + "<th>OPERATIONAL COST</th>";
                        header = header + "<th>OT AMOUNT</th>";
                        header = header + "<th>RATE</th>";
                        if (double.Parse(ds.Tables[0].Rows[ctr]["bill_service_charge"].ToString()) > 0)
                        {
                            header = header + "<th>SERVICE CHARGE @" + ds.Tables[0].Rows[ctr]["bill_service_charge"] + "%</th>";
                        }
                        else
                        {
                            header = header + "<th>SERVICE CHARGE</th>";
                        }
                        header = header + "<th>GROUP INSURANCE</th>";
                        header = header + "<th>GRAND TOTAL</th>";
                        lc = new LiteralControl("<table border=1><tr><th colspan=49 bgcolor=yellow align=center> RATE BREAKUP FOR 8/12 HRS DUTY</th></tr><tr><th>SR. NO.</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>DEG.</th><th>DUTY HRS</th><th>NO. OF PAID DAYS</th>" + header + "</tr>");
                    }
                    else if (i == 2)
                    {
                        string branch = "";
                        string opus_code = "";
                        if (double.Parse(ds.Tables[0].Rows[ctr]["bill_service_charge"].ToString()) > 0)
                        {
                            header = "<th>SERVICE CHARGE @" + ds.Tables[0].Rows[ctr]["bill_service_charge"] + "%</th>";
                        }
                        else
                        {
                            header = "<th>SERVICE CHARGE</th>";
                        }


                        if (ds.Tables[0].Rows[ctr]["client"].ToString().Contains("HDFC"))
                        {
                            lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BILL NO</th><th>ZONE AS PER HDFC LIFE</th><th>BRANCH COST CENTER CODE</th><th>REGION AS PER HDFC LIFE</th><th>CONCERN ADMIN</th><th>SECURITY COMPANY NAME</th><th>COST CENTER</th><th>BRANCH CODE</th><th>LOCATION TYPE (BRANCH / REGIONAL OFFICE / ZONAL OFFICE / HEAD OFFICE)</th><th>BRANCH NAME</th><th>" + ds.Tables[0].Rows[ctr]["GRADE_CODE"].ToString() + " SHIFT TYPE</th><th>DUTY HOURS(EACH GUARD)</th><th>APPLICABLE GAZETTE NOTIFCATION</th><th>CATEGORY (SG / SO / SS / GUNMAN)</th><th>STATE</th><th>RATE</th><th>NO.OF " + ds.Tables[0].Rows[ctr]["GRADE_CODE"].ToString() + " IN BRANCH</th><th>NO. OF DUTIES BY " + ds.Tables[0].Rows[ctr]["GRADE_CODE"].ToString() + "</th><th>DAYS IN MONTH</th><th>BASE AMOUNT</th><th>OT HOURS</th><th>OT RATE</th><th>OT AMOUNT</th><th>TOTAL BASE AMT & OT AMT</th>" + header + "<th>GRAND TOTAL</th><th>CGST @9%</th><th>SGST @9%</th><th>IGST @18%</th><th>TOTAL GST</th><th>TOTAL CTC</th></tr>");
                        }
                        else if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAG") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BG") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG") && !ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("4"))
                        {
                            if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG"))
                            {
                                opus_code = "<th>OPUS CODE</th>";
                            }
                            if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() != "0")
                            {
                                branch = "<th>BRANCH TYPE</th>";
                            }
                            lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BILL NO</th><th>BILL DATE</th><th>INVOICE PERIOD</th><th>BRANCH CODE</th>" + opus_code + "<th>BRANCH NAME</th>" + branch + "<th>STATE</th><th>EMPLOYEE NAME</th><th>DEG.</th><th>RATE</th><th>NO. OF PAID DAYS</th><th>BASE AMOUNT</th><th>OT HOURS</th><th>OT RATE</th><th>OT AMOUNT</th><th>TOTAL BASE AMT & OT AMT</th>" + header + "<th>GRAND TOTAL</th><th>CGST @9%</th><th>SGST @9%</th><th>IGST @18%</th><th>TOTAL GST</th><th>TOTAL CTC</th></tr>");
                        }
                        else
                        {

                            if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() != "0")
                            {
                                branch = "<th>BRANCH TYPE</th>";
                            }
                            lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BILL NO</th><th>BILL DATE</th><th>INVOICE PERIOD</th><th>BRANCH CODE</th>" + opus_code + "<th>BRANCH NAME</th>" + branch + "<th>STATE</th><th>CITY</th><th>EMPLOYEE NAME</th><th>DEG.</th><th>DUTY HOURS</th><th>RATE</th><th>NO. OF PAID DAYS</th><th>BASE AMOUNT</th><th>OT HOURS</th><th>OT RATE</th><th>OT AMOUNT</th><th>TOTAL BASE AMT & OT AMT</th>" + header + "<th>GRAND TOTAL</th><th>CGST @9%</th><th>SGST @9%</th><th>IGST @18%</th><th>TOTAL GST</th><th>TOTAL CTC</th></tr>");
                        }
                    }
                    else if (i == 3)
                    {
                        header = "<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th>";
                        int daysadd = 0;
                        int days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                        if (days == 29)
                        { header = header + "<th>29</th>"; daysadd = 1; }
                        else if (days == 30)
                        {
                            header = header + "<th>29</th><th>30</th>"; daysadd = 2;
                        }
                        else if (days == 31)
                        {
                            header = header + "<th>29</th><th>30</th><th>31</th>"; daysadd = 3;
                        }
                        if (start_date_common != "" && start_date_common != "1")
                        {
                            header = header1;
                        }

                        lc = new LiteralControl("<table border=1><tr><th>SL. NO.</th><th>STATE</th><th>LOCATION</th><th>BRANCH CODE</th><th>NAME</th><th>DEG.</th><th>OT HRS.</th>" + header + "<th>TOTAL P/DAY</th><th>ABSENT DAY</th><th>TOTAL DAYS</th></tr>");
                        header = "";

                    }
                    else if (i == 4)
                    {
                        if (ds.Tables[0].Rows[ctr]["client_code"].ToString() == "UTKARSH")
                        {
                            lc = new LiteralControl("<table border=1><tr><th>SL. NO.</th><th>INVOICE NO</th><th>INVOICE DATE</th><th>BRANCH CODE</th><th>BRANCH NAME</th><th>STATE GST NO.</th><th>SHIP TO PARTY NAME</th><th>CITY</th><th>BASE VALUE</th><th>CGST 9%</th><th>SGST 9%</th><th>IGST 18%</th><th>TOTAL</th><th>STATE</th></tr>");
                        }
                        else if (ds.Tables[0].Rows[ctr]["client_code"].ToString() == "MAX")
                        {
                            lc = new LiteralControl("<table border=1><tr><th>SR.NO.</th><th>LOCATION</th><th>STATE</th><th>RANK</th><th>STRENGTH</th><th>DUTY</th><th>RATE</th><th>AMOUNT</th><th>REMARKS</th><th>MONTH</th></tr>");
                        }
                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR.NO.</th><th>BILL NO</th><th>BILL DATE</th><th>PERIOD</th><th>LOCATION</th><th>STATE</th><th>EMPLOYEE NAME</th><th>DESIGNATION</th><th>RATE</th><th>SERVICE CHARGE " + ds.Tables[0].Rows[ctr]["conveyance_service_charge_per"].ToString() + "%</th><th>SUB TOTAL</th><th>CGST 9%</th><th>SGST 9%</th><th>IGST 18%</th><th>TOTAL GST</th><th>GRAND TOTAL</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        bodystr = "";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["basic"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["vda"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["bonus_rate"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["washing"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["travelling"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["education"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["allowances_esic"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["cca_billing"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["other_allow"].ToString()), 2) + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bonus_amount"] + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["leave_amount"] + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["grauity_amount"] + "</td>";

                        //if (ds.Tables[0].Rows[ctr]["bonus_taxable"].ToString().Equals("1"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["bonus_gross"].ToString()), 2) + "</td>";
                        //if (double.Parse(ds.Tables[0].Rows[ctr]["bonus_amount"].ToString()) > 0) { header = header + "<th>Bonus " + ds.Tables[0].Rows[ctr]["bill_bonus_percent"] + "% on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bonus_amount"] + "</td>"; }
                        //}
                        //else { bodystr = bodystr + "<td>0</td>"; }
                        //if (ds.Tables[0].Rows[ctr]["leave_taxable"].ToString().Equals("1"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["leave_gross"].ToString()), 2) + "</td>";
                        //if (double.Parse(ds.Tables[0].Rows[ctr]["leave_amount"].ToString()) > 0) { header = header + "<th>Earned Leaves " + ds.Tables[0].Rows[ctr]["leave_days"] + " days on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["leave_amount"] + "</td>"; }
                        //}
                        //else { bodystr = bodystr + "<td>0</td>"; }
                        //if (ds.Tables[0].Rows[ctr]["gratuity_taxable"].ToString().Equals("1"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["gratuity_gross"].ToString()), 2) + "</td>";
                        // if (double.Parse(ds.Tables[0].Rows[ctr]["grauity_amount"].ToString()) > 0) { header = header + "<th>Gratuity " + ds.Tables[0].Rows[ctr]["gratuity_percent"] + " % on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["grauity_amount"] + "</td>"; }
                        //}
                        //else { bodystr = bodystr + "<td>0</td>"; }
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["hra"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["special_allowance"].ToString()), 2) + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["gross"] + "</td>";
                        bodystr = bodystr + "<td>=ROUND(SUM(L" + (ctr + 3) + ":V" + (ctr + 3) + ",J" + (ctr + 3) + "),2)</td>";

                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["bonus_after_gross"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["leave_after_gross"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["gratuity_after_gross"].ToString()), 2) + "</td>";


                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bonus_amount"] + "</td>";

                        // if (ds.Tables[0].Rows[ctr]["bonus_taxable"].ToString().Equals("0"))
                        // {
                        //     bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bonus_amount"] + "</td>";
                        //     //if (double.Parse(ds.Tables[0].Rows[ctr]["bonus_amount"].ToString()) > 0) { header = header + "<th>Bonus " + ds.Tables[0].Rows[ctr]["bill_bonus_percent"] + "% on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bonus_amount"] + "</td>"; }
                        // }
                        // else { bodystr = bodystr + "<td>0</td>"; }

                        //// bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["leave_amount"] + "</td>";

                        // if (ds.Tables[0].Rows[ctr]["leave_taxable"].ToString().Equals("0"))
                        // {
                        //     bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["leave_amount"] + "</td>";
                        //     //if (double.Parse(ds.Tables[0].Rows[ctr]["leave_amount"].ToString()) > 0) { header = header + "<th>Earned Leaves " + ds.Tables[0].Rows[ctr]["leave_days"] + " days on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["leave_amount"] + "</td>"; }
                        // }
                        // else { bodystr = bodystr + "<td>0</td>"; }
                        //// bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["grauity_amount"] + "</td>";

                        // if (ds.Tables[0].Rows[ctr]["gratuity_taxable"].ToString().Equals("0"))
                        // {
                        //     bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["grauity_amount"] + "</td>";
                        //     // if (double.Parse(ds.Tables[0].Rows[ctr]["grauity_amount"].ToString()) > 0) { header = header + "<th>Gratuity " + ds.Tables[0].Rows[ctr]["gratuity_percent"] + " % on Salary</th>"; bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["grauity_amount"] + "</td>"; }
                        // }
                        // else { bodystr = bodystr + "<td>0</td>"; }
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["NH"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["pf"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["esic"].ToString()), 2) + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["uniform"] + "</td>";

                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["uniform_ser"].ToString()), 2) + "</td>";
                        //if (ds.Tables[0].Rows[ctr]["bill_ser_uniform"].ToString().Equals("1"))
                        //{
                        //    bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["uniform"] + "</td>";
                        //}
                        //else
                        //{
                        //    bodystr = bodystr + "<td>0</td>";
                        //}
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing_ser"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["lwf"].ToString()), 2) + "</td>";
                        //if (ds.Tables[0].Rows[ctr]["bill_ser_operations"].ToString().Equals("1"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString()), 2) + "</td>";
                        //}
                        //else
                        //{
                        //    bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["operational_cost"] + "</td>";
                        //}

                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["allowances_no_esic"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sub_total_a"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["ot_pr_hr_rate"].ToString()), 2) + "</td>";
                        //bodystr = bodystr + "<td>" + double.Parse(ds.Tables[0].Rows[ctr]["ot_pr_hr_rate"].ToString()) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["esi_on_ot_amount"].ToString()), 2) + "</td>";
                        // bodystr = bodystr + "<td>" + Math.Roudouble.Parse(ds.Tables[0].Rows[ctr]["esi_on_ot_amount"].ToString()) + "</td>";

                        bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + "</td>";
                        // bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["esic_ot_amount"] + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sub_total_b"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sub_total_ab"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["relieving_charg"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString()), 2) + "</td>";
                        //bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["uniform"] + "</td>";

                        //if (ds.Tables[0].Rows[ctr]["bill_ser_uniform"].ToString().Equals("0"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["uniform_no_ser"].ToString()), 2) + "</td>";
                        //}
                        //else
                        //{
                        //    bodystr = bodystr + "<td>0</td>";
                        //}
                        //if (ds.Tables[0].Rows[ctr]["bill_ser_operations"].ToString().Equals("0"))
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["operational_cost_no_ser"].ToString()), 2) + "</td>";
                        //}
                        //else
                        //{
                        //    bodystr = bodystr + "<td>0</td>";
                        //}

                        //if (ds.Tables[0].Rows[ctr]["bill_ser_operations"].ToString().Equals("0"))
                        //{
                        //    bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["operational_cost"] + "</td>";
                        //}
                        //if (double.Parse(ds.Tables[0].Rows[ctr]["bill_service_charge"].ToString()) > 0)
                        //{
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td>";
                        //}
                        //else { bodystr = bodystr + "<td>" + ds.Tables[0].Rows[ctr]["bill_service_charge_amount"] + "</td>"; }
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString()), 2) + "</td>";
                        bodystr = bodystr + "<td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()), 2) + "</td>";


                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DUTYHRS"] + "</td><td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td>" + bodystr + "</tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 6>Total</td><td>=SUM(G3:G" + (ctr + 3) + ")</td><td>=ROUND(SUM(H3:H" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(I3:I" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(J3:J" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(K3:K" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(L3:L" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(M3:M" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(N3:N" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(O3:O" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(P3:P" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(Q3:Q" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(R3:R" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(S3:S" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(T3:T" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(U3:U" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(V3:V" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(W3:W" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(X3:X" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(Y3:Y" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(Z3:Z" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AA3:AA" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AB3:AB" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AC3:AC" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AD3:AD" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AE3:AE" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AF3:AF" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AG3:AG" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AH3:AH" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AI3:AI" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AJ3:AJ" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AK3:AK" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AL3:AL" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AM3:AM" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AN3:AN" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AO3:AO" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AP3:AP" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AQ3:AQ" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AR3:AR" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AS3:AS" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AT3:AT" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AU3:AU" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AV3:AV" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(AW3:AW" + (ctr + 3) + "),2)</td></b></tr>";
                        }
                        header = "";
                        bodystr = "";
                    }
                    else if (i == 2)
                    {
                        string branch = "";
                        string opus_code = "";
                        string tot = "";
                        string tot_hrs = "";
                        string base_amount = "", tot_ctc = "", tot_gst = "";

                        if (ds.Tables[0].Rows[ctr]["client"].ToString().Contains("HDFC"))
                        {
                            if (state_name != ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper())
                            {
                                if (state_name != "")
                                {
                                    //code here
                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = 16>Total</td><td>" + (Math.Round(sub_total, 2) - Math.Round(sub_total1, 2)) + "</td><td>" + (total_emp_count - total_emp_count1) + "</td><td>" + (no_of_duties - no_of_duties1) + "</td><td>" + (paid_days - paid_days1) + "</td><td>" + (Math.Round(rate, 2) - Math.Round(rate1, 2)) + "</td><td>" + (ot_hrs - ot_hrs1) + "</td><td>" + (ot_rate - ot_rate1) + "</td><td>" + (ot_amount - ot_amount1) + "</td><td>" + (Math.Round((rate + ot_amount), 2) - Math.Round((rate1 + ot_amount1), 2)) + "</td><td>" + (Math.Round(service_charge, 2) - Math.Round(service_charge1, 2)) + "</td><td>" + (Math.Round(grand_tot, 2) - Math.Round(grand_tot1, 2)) + "</td><td>" + (cgst - cgst1) + "</td><td>" + (sgst - sgst1) + "</td><td>" + (igst - igst1) + "</td><td>" + (gst - gst1) + "</td><td>" + (Math.Ceiling(Math.Round(ctc, 2)) - Math.Ceiling(Math.Round(ctc1, 2))) + "</td></b></tr>";

                                    sub_total1 = sub_total;
                                    total_emp_count1 = total_emp_count;
                                    no_of_duties1 = no_of_duties;
                                    paid_days1 = paid_days;
                                    rate1 = rate;
                                    ot_hrs1 = ot_hrs;
                                    ot_rate1 = ot_rate;
                                    ot_amount1 = ot_amount;
                                    service_charge1 = service_charge;
                                    grand_tot1 = grand_tot;
                                    cgst1 = cgst;
                                    sgst1 = sgst;
                                    igst1 = igst;
                                    gst1 = gst;
                                    ctc1 = ctc;

                                }
                                state_name = ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper();
                            }

                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + ds.Tables[0].Rows[ctr]["txt_zone"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_cost_centre_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["zone"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["ihms"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_cost_centre_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["location_type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_count"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_count1"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_per"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["total_emp_count"] + "</td><td>" + ds.Tables[0].Rows[ctr]["no_of_duties"] + "</td><td>" + ds.Tables[0].Rows[ctr]["TOT_WORKING_DAYS"] + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + ((double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td></tr>");

                            sub_total = sub_total + (double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString()));
                            total_emp_count = total_emp_count + (double.Parse(ds.Tables[0].Rows[ctr]["total_emp_count"].ToString()));
                            no_of_duties = no_of_duties + (double.Parse(ds.Tables[0].Rows[ctr]["no_of_duties"].ToString()));
                            paid_days = paid_days + (double.Parse(ds.Tables[0].Rows[ctr]["TOT_WORKING_DAYS"].ToString()));
                            rate = rate + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()));
                            ot_hrs = ot_hrs + double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString());
                            ot_rate = ot_rate + double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString());
                            ot_amount = ot_amount + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString());
                            service_charge = service_charge + (double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()));
                            grand_tot = grand_tot + ((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString())));
                            cgst = cgst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()));
                            sgst = sgst + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()));
                            igst = igst + (double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            gst = gst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            ctc = ctc + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2);
                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 16>Total</td><td>" + Math.Round(sub_total, 2) + "</td><td>" + total_emp_count + "</td><td>" + no_of_duties + "</td><td>" + paid_days + "</td><td>" + Math.Round(rate, 2) + "</td><td>" + ot_hrs + "</td><td>" + ot_rate + "</td><td>" + ot_amount + "</td><td>" + Math.Round((rate + ot_amount), 2) + "</td><td>" + Math.Round(service_charge, 2) + "</td><td>" + Math.Round(grand_tot, 2) + "</td><td>" + cgst + "</td><td>" + sgst + "</td><td>" + igst + "</td><td>" + gst + "</td><td>" + Math.Ceiling(Math.Round(ctc, 2)) + "</td></b></tr>";
                            }
                        }
                        else if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BAG") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BG") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG") && !ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("4"))
                        {
                            int colsize = 9;

                            //tot_hrs = "<td>=ROUND(SUM(K2:K" + (ctr + i3 + 2) + "),2)</td>";
                            tot_ctc = "<td>=ROUND(SUM(R" + (ctr + i3 + 2) + ",V" + (ctr + i3 + 2) + "),2)</td>";
                            tot_gst = "<td>=ROUND(SUM(S" + (ctr + i3 + 2) + ":U" + (ctr + i3 + 2) + "),2)</td>";
                            base_amount = "<td>=ROUND(J" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * K" + (ctr + i3 + 2) + ",2)</td>";

                            if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG"))
                            {
                                opus_code = "<td>" + ds.Tables[0].Rows[ctr]["OPus_NO"].ToString().ToUpper() + "</td>";
                                tot = "<td>=ROUND(SUM(X2:X" + (ctr + i3 + 2) + "),2)</td>";
                                base_amount = "<td>=ROUND(K" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * L" + (ctr + i3 + 2) + ",2)</td>";
                                tot_ctc = "<td>=ROUND(SUM(S" + (ctr + i3 + 2) + ",W" + (ctr + i3 + 2) + "),2)</td>";
                                tot_gst = "<td>=ROUND(SUM(T" + (ctr + i3 + 2) + ":V" + (ctr + i3 + 2) + "),2)</td>";
                                colsize = 10;
                            }


                            if (ds.Tables[0].Rows[ctr]["gst_applicable"].ToString() == "1")
                            {
                                ds.Tables[0].Rows[ctr]["IGST18"] = "0";
                                ds.Tables[0].Rows[ctr]["CGST9"] = "0";
                                ds.Tables[0].Rows[ctr]["SGST9"] = "0";
                            }

                            if (state_name != ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper())
                            {
                                if (state_name != "")
                                {
                                    //code here 
                                    i3 = i3 + 1;


                                    if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG"))
                                    {
                                        lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + ")</td><td>=ROUND(SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td></tr>";
                                        //DUTY_HOURS = DUTY_HOURS + "," + "k" + (ctr + i3 + 1);
                                        RATE = RATE + "," + "K" + (ctr + i3 + 1);
                                        NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "L" + (ctr + i3 + 1);
                                        BASE_AMOUNT = BASE_AMOUNT + "," + "M" + (ctr + i3 + 1);
                                        OT_HOURS = OT_HOURS + "," + "N" + (ctr + i3 + 1);
                                        OT_RATE = OT_RATE + "," + "O" + (ctr + i3 + 1);
                                        OT_AMOUNT = OT_AMOUNT + "," + "P" + (ctr + i3 + 1);
                                        TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "Q" + (ctr + i3 + 1);
                                        SERVICE_CHARGE = SERVICE_CHARGE + "," + "R" + (ctr + i3 + 1);
                                        GRAND_TOTAL = GRAND_TOTAL + "," + "S" + (ctr + i3 + 1);
                                        CGST = CGST + "," + "T" + (ctr + i3 + 1);
                                        SGST = SGST + "," + "U" + (ctr + i3 + 1);
                                        IGST = IGST + "," + "V" + (ctr + i3 + 1);
                                        TOTAL_GST = TOTAL_GST + "," + "W" + (ctr + i3 + 1);
                                        TOTAL_CTC = TOTAL_CTC + "," + "X" + (ctr + i3 + 1);
                                        state_change = 1;

                                        //tot_hrs = "<td>=ROUND(SUM(K2:K" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_ctc = "<td>=ROUND(SUM(S" + (ctr + i3 + 2) + ",W" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_gst = "<td>=ROUND(SUM(T" + (ctr + i3 + 2) + ":V" + (ctr + i3 + 2) + "),2)</td>";
                                        base_amount = "<td>=ROUND(K" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * L" + (ctr + i3 + 2) + ",2)</td>";

                                    }
                                    else
                                    {
                                        lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(J" + (ctc1 + 2) + ":J" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td></tr>";

                                        //DUTY_HOURS = DUTY_HOURS + "," + "L" + (ctr + i3 + 1);
                                        RATE = RATE + "," + "J" + (ctr + i3 + 1);
                                        NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "K" + (ctr + i3 + 1);
                                        BASE_AMOUNT = BASE_AMOUNT + "," + "L" + (ctr + i3 + 1);
                                        OT_HOURS = OT_HOURS + "," + "M" + (ctr + i3 + 1);
                                        OT_RATE = OT_RATE + "," + "N" + (ctr + i3 + 1);
                                        OT_AMOUNT = OT_AMOUNT + "," + "O" + (ctr + i3 + 1);
                                        TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "P" + (ctr + i3 + 1);
                                        SERVICE_CHARGE = SERVICE_CHARGE + "," + "Q" + (ctr + i3 + 1);
                                        GRAND_TOTAL = GRAND_TOTAL + "," + "R" + (ctr + i3 + 1);
                                        CGST = CGST + "," + "S" + (ctr + i3 + 1);
                                        SGST = SGST + "," + "T" + (ctr + i3 + 1);
                                        IGST = IGST + "," + "U" + (ctr + i3 + 1);
                                        TOTAL_GST = TOTAL_GST + "," + "V" + (ctr + i3 + 1);
                                        TOTAL_CTC = TOTAL_CTC + "," + "W" + (ctr + i3 + 1);
                                        state_change = 1;

                                        //tot = "<td>=ROUND(SUM(Z2:Z" + (ctr + i3 + 2) + "),2)</td>";
                                        base_amount = "<td>=ROUND(J" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * K" + (ctr + i3 + 2) + ",2)</td>";
                                        //tot_ctc = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ",Y" + (ctr + i3 + 2) + "),2)</td>";
                                        //tot_gst = "<td>=ROUND(SUM(V" + (ctr + i3 + 2) + ":X" + (ctr + i3 + 2) + "),2)</td>";

                                        tot_ctc = "<td>=ROUND(SUM(R" + (ctr + i3 + 2) + ",V" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_gst = "<td>=ROUND(SUM(S" + (ctr + i3 + 2) + ":U" + (ctr + i3 + 2) + "),2)</td>";
                                        //colsize = 10;
                                    }
                                    ctc1 = ctr + i3;

                                }

                                state_name = ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper();
                            }
                            //lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["fromtodate"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td>"+branch+"<td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["hours"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + ((double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td></tr>");
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["fromtodate"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"].ToString().ToUpper() + "</td>" + opus_code + "<td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td>" + branch + "<td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td>" + base_amount + "<td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td>" + tot_gst + "" + tot_ctc + "</tr>");

                            //sub_total = sub_total + (double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString()));
                            //paid_days = paid_days + (double.Parse(ds.Tables[0].Rows[ctr]["tot_days_present"].ToString()));
                            //rate = rate + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()));
                            //ot_hrs = ot_hrs + double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString());
                            //ot_rate = ot_rate + double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString());
                            //ot_amount = ot_amount + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString());
                            //service_charge = service_charge + (double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()));
                            //grand_tot = grand_tot + ((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString())));
                            //cgst = cgst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()));
                            //sgst = sgst + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()));
                            //igst = igst + (double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            //gst = gst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            //ctc = ctc + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2);

                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                i3 = i3 + 2;
                                //state total
                                if (ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIK HK") || ds.Tables[0].Rows[ctr]["client_code"].ToString().Equals("BALIC SG"))
                                {

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + ")</td><td>=ROUND(SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td></tr>";

                                    RATE = RATE + "," + "K" + (ctr + i3 + 1);
                                    NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "L" + (ctr + i3 + 1);
                                    BASE_AMOUNT = BASE_AMOUNT + "," + "M" + (ctr + i3 + 1);
                                    OT_HOURS = OT_HOURS + "," + "N" + (ctr + i3 + 1);
                                    OT_RATE = OT_RATE + "," + "O" + (ctr + i3 + 1);
                                    OT_AMOUNT = OT_AMOUNT + "," + "P" + (ctr + i3 + 1);
                                    TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "Q" + (ctr + i3 + 1);
                                    SERVICE_CHARGE = SERVICE_CHARGE + "," + "R" + (ctr + i3 + 1);
                                    GRAND_TOTAL = GRAND_TOTAL + "," + "S" + (ctr + i3 + 1);
                                    CGST = CGST + "," + "T" + (ctr + i3 + 1);
                                    SGST = SGST + "," + "U" + (ctr + i3 + 1);
                                    IGST = IGST + "," + "V" + (ctr + i3 + 1);
                                    TOTAL_GST = TOTAL_GST + "," + "W" + (ctr + i3 + 1);
                                    TOTAL_CTC = TOTAL_CTC + "," + "X" + (ctr + i3 + 1);

                                    if (state_change == 1)
                                    {
                                        lc.Text = lc.Text + "<tr><b> <td align=center colspan = " + colsize + ">Total</td> <td>=ROUND(SUM(" + RATE + "),2)</td> <td>=ROUND(SUM(" + NO_OF_PAID_DAYS + "),2)</td> <td>=ROUND(SUM(" + BASE_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + OT_HOURS + "),2)</td> <td>=ROUND(SUM(" + OT_RATE + "),2)</td> <td>=ROUND(SUM(" + OT_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + TOTAL_BASE_AMT_OT_AMT + "),2)</td> <td>=ROUND(SUM(" + SERVICE_CHARGE + "),2)</td> <td>=ROUND(SUM(" + GRAND_TOTAL + "),2)</td> <td>=ROUND(SUM(" + CGST + "),2)</td> <td>=ROUND(SUM(" + SGST + "),2)</td> <td>=ROUND(SUM(" + IGST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_GST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_CTC + "),2)</td> </b></tr>";

                                    }
                                }
                                //  lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Z" + (ctc1 + 2) + ":Z" + (ctr + i3) + "),2)</td></tr>";


                                //client total
                                else
                                {

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(J" + (ctc1 + 2) + ":J" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td></tr>";


                                    RATE = RATE + "," + "J" + (ctr + i3 + 1);
                                    NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "K" + (ctr + i3 + 1);
                                    BASE_AMOUNT = BASE_AMOUNT + "," + "L" + (ctr + i3 + 1);
                                    OT_HOURS = OT_HOURS + "," + "M" + (ctr + i3 + 1);
                                    OT_RATE = OT_RATE + "," + "N" + (ctr + i3 + 1);
                                    OT_AMOUNT = OT_AMOUNT + "," + "O" + (ctr + i3 + 1);
                                    TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "P" + (ctr + i3 + 1);
                                    SERVICE_CHARGE = SERVICE_CHARGE + "," + "Q" + (ctr + i3 + 1);
                                    GRAND_TOTAL = GRAND_TOTAL + "," + "R" + (ctr + i3 + 1);
                                    CGST = CGST + "," + "S" + (ctr + i3 + 1);
                                    SGST = SGST + "," + "T" + (ctr + i3 + 1);
                                    IGST = IGST + "," + "U" + (ctr + i3 + 1);
                                    TOTAL_GST = TOTAL_GST + "," + "V" + (ctr + i3 + 1);
                                    TOTAL_CTC = TOTAL_CTC + "," + "W" + (ctr + i3 + 1);
                                    if (state_change == 1)
                                    {
                                        lc.Text = lc.Text + "<tr><b> <td align=center colspan = " + colsize + ">Total</td> <td>=ROUND(SUM(" + DUTY_HOURS + "),2)</td> <td>=ROUND(SUM(" + RATE + "),2)</td> <td>=ROUND(SUM(" + NO_OF_PAID_DAYS + "),2)</td> <td>=ROUND(SUM(" + BASE_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + OT_HOURS + "),2)</td> <td>=ROUND(SUM(" + OT_RATE + "),2)</td> <td>=ROUND(SUM(" + OT_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + TOTAL_BASE_AMT_OT_AMT + "),2)</td> <td>=ROUND(SUM(" + SERVICE_CHARGE + "),2)</td> <td>=ROUND(SUM(" + GRAND_TOTAL + "),2)</td> <td>=ROUND(SUM(" + CGST + "),2)</td> <td>=ROUND(SUM(" + SGST + "),2)</td> <td>=ROUND(SUM(" + IGST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_GST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_CTC + "),2)</td> </b></tr>";
                                    }
                                }

                                //   lc.Text = lc.Text + "<tr><b><td align=center colspan = "+colsize+">Total</td><td>" + Math.Round(sub_total, 2) + "</td><td>" + paid_days + "</td><td>=SUM(N2:N"+(ctr+2)+")</td><td>" + ot_hrs + "</td><td>" + ot_rate + "</td><td>" + ot_amount + "</td><td>" + Math.Round((rate + ot_amount), 2) + "</td><td>" + Math.Round(service_charge, 2) + "</td><td>" + Math.Round(grand_tot, 2) + "</td><td>" + cgst + "</td><td>" + sgst + "</td><td>" + igst + "</td><td>" + gst + "</td><td>" + Math.Ceiling(Math.Round(ctc, 2)) + "</td></b></tr>";
                                //lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td>" + tot_hrs + "<td>=ROUND(SUM(L2:L" + (ctr + 2) + "),2)</td><td>=SUM(M2:M" + (ctr + 2) + ")</td><td>=ROUND(SUM(N2:N" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(O2:O" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(P2:P" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(Q2:Q" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(R2:R" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(S2:S" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(T2:T" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(U2:U" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(V2:V" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(W2:W" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(X2:X" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(Y2:Y" + (ctr + 2) + "),2)</td>" + tot + "</b></tr>";
                            }

                        }
                        else
                        {


                            int colsize = 10;
                            tot_hrs = "<td>=ROUND(SUM(K2:K" + (ctr + i3 + 2) + "),2)</td>";
                            tot_ctc = "<td>=ROUND(SUM(T" + (ctr + i3 + 2) + ",X" + (ctr + i3 + 2) + "),2)</td>";
                            tot_gst = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ":W" + (ctr + i3 + 2) + "),2)</td>";
                            base_amount = "<td>=ROUND(L" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * M" + (ctr + i3 + 2) + ",2)</td>";

                            if (ds.Tables[0].Rows[ctr]["client"].ToString().Contains("BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED"))
                            {
                                opus_code = "<td>" + ds.Tables[0].Rows[ctr]["OPus_NO"].ToString().ToUpper() + "</td>";
                                tot = "<td>=ROUND(SUM(Z2:Z" + (ctr + i3 + 2) + "),2)</td>";
                                base_amount = "<td>=ROUND(M" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * N" + (ctr + i3 + 2) + ",2)</td>";
                                tot_ctc = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ",Y" + (ctr + i3 + 2) + "),2)</td>";
                                tot_gst = "<td>=ROUND(SUM(V" + (ctr + i3 + 2) + ":X" + (ctr + i3 + 2) + "),2)</td>";
                                colsize = 11;
                            }
                            if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() != "0")
                            {
                                branch = "<td>" + ds.Tables[0].Rows[ctr]["branch_type"].ToString().ToUpper() + "</td>";
                                tot = "<td>=ROUND(SUM(Z2:Z" + (ctr + i3 + 2) + "),2)</td>";
                                base_amount = "<td>=ROUND(M" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * N" + (ctr + i3 + 2) + ",2)</td>";
                                tot_ctc = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ",Y" + (ctr + i3 + 2) + "),2)</td>";
                                tot_gst = "<td>=ROUND(SUM(V" + (ctr + i3 + 2) + ":X" + (ctr + i3 + 2) + "),2)</td>";
                                colsize = 11;
                            }

                            if (ds.Tables[0].Rows[ctr]["gst_applicable"].ToString() == "1")
                            {
                                ds.Tables[0].Rows[ctr]["IGST18"] = "0";
                                ds.Tables[0].Rows[ctr]["CGST9"] = "0";
                                ds.Tables[0].Rows[ctr]["SGST9"] = "0";
                            }

                            if (state_name != ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper())
                            {
                                if (state_name != "")
                                {
                                    //code here 
                                    i3 = i3 + 1;


                                    if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() == "0" && !ds.Tables[0].Rows[ctr]["client"].ToString().Contains("BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED"))
                                    {
                                        lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + ")</td><td>=ROUND(SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td></tr>";
                                        DUTY_HOURS = DUTY_HOURS + "," + "k" + (ctr + i3 + 1);
                                        RATE = RATE + "," + "L" + (ctr + i3 + 1);
                                        NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "M" + (ctr + i3 + 1);
                                        BASE_AMOUNT = BASE_AMOUNT + "," + "N" + (ctr + i3 + 1);
                                        OT_HOURS = OT_HOURS + "," + "O" + (ctr + i3 + 1);
                                        OT_RATE = OT_RATE + "," + "P" + (ctr + i3 + 1);
                                        OT_AMOUNT = OT_AMOUNT + "," + "Q" + (ctr + i3 + 1);
                                        TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "R" + (ctr + i3 + 1);
                                        SERVICE_CHARGE = SERVICE_CHARGE + "," + "S" + (ctr + i3 + 1);
                                        GRAND_TOTAL = GRAND_TOTAL + "," + "T" + (ctr + i3 + 1);
                                        CGST = CGST + "," + "U" + (ctr + i3 + 1);
                                        SGST = SGST + "," + "V" + (ctr + i3 + 1);
                                        IGST = IGST + "," + "W" + (ctr + i3 + 1);
                                        TOTAL_GST = TOTAL_GST + "," + "X" + (ctr + i3 + 1);
                                        TOTAL_CTC = TOTAL_CTC + "," + "Y" + (ctr + i3 + 1);
                                        state_change = 1;

                                        tot_hrs = "<td>=ROUND(SUM(K2:K" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_ctc = "<td>=ROUND(SUM(T" + (ctr + i3 + 2) + ",X" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_gst = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ":W" + (ctr + i3 + 2) + "),2)</td>";
                                        base_amount = "<td>=ROUND(L" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * M" + (ctr + i3 + 2) + ",2)</td>";

                                    }
                                    else
                                    {
                                        lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Z" + (ctc1 + 2) + ":Z" + (ctr + i3) + "),2)</td></tr>";

                                        DUTY_HOURS = DUTY_HOURS + "," + "L" + (ctr + i3 + 1);
                                        RATE = RATE + "," + "M" + (ctr + i3 + 1);
                                        NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "N" + (ctr + i3 + 1);
                                        BASE_AMOUNT = BASE_AMOUNT + "," + "O" + (ctr + i3 + 1);
                                        OT_HOURS = OT_HOURS + "," + "P" + (ctr + i3 + 1);
                                        OT_RATE = OT_RATE + "," + "Q" + (ctr + i3 + 1);
                                        OT_AMOUNT = OT_AMOUNT + "," + "R" + (ctr + i3 + 1);
                                        TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "S" + (ctr + i3 + 1);
                                        SERVICE_CHARGE = SERVICE_CHARGE + "," + "T" + (ctr + i3 + 1);
                                        GRAND_TOTAL = GRAND_TOTAL + "," + "U" + (ctr + i3 + 1);
                                        CGST = CGST + "," + "V" + (ctr + i3 + 1);
                                        SGST = SGST + "," + "W" + (ctr + i3 + 1);
                                        IGST = IGST + "," + "X" + (ctr + i3 + 1);
                                        TOTAL_GST = TOTAL_GST + "," + "Y" + (ctr + i3 + 1);
                                        TOTAL_CTC = TOTAL_CTC + "," + "Z" + (ctr + i3 + 1);
                                        state_change = 1;
                                        if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() != "0")
                                        {
                                            branch = "<td>" + ds.Tables[0].Rows[ctr]["branch_type"].ToString().ToUpper() + "</td>";
                                        }
                                        tot = "<td>=ROUND(SUM(Z2:Z" + (ctr + i3 + 2) + "),2)</td>";
                                        base_amount = "<td>=ROUND(M" + (ctr + i3 + 2) + "/" + ds.Tables[0].Rows[ctr]["month_days"].ToString() + " * N" + (ctr + i3 + 2) + ",2)</td>";
                                        tot_ctc = "<td>=ROUND(SUM(U" + (ctr + i3 + 2) + ",Y" + (ctr + i3 + 2) + "),2)</td>";
                                        tot_gst = "<td>=ROUND(SUM(V" + (ctr + i3 + 2) + ":X" + (ctr + i3 + 2) + "),2)</td>";
                                        colsize = 11;
                                    }
                                    ctc1 = ctr + i3;

                                }

                                state_name = ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper();
                            }
                            //lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["fromtodate"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td>"+branch+"<td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["hours"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + ((double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td></tr>");
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["fromtodate"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"].ToString().ToUpper() + "</td>" + opus_code + "<td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td>" + branch + "<td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["hours"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td>" + base_amount + "<td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString())) + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td>" + tot_gst + "" + tot_ctc + "</tr>");

                            //sub_total = sub_total + (double.Parse(ds.Tables[0].Rows[ctr]["sub_total_c"].ToString()));
                            //paid_days = paid_days + (double.Parse(ds.Tables[0].Rows[ctr]["tot_days_present"].ToString()));
                            //rate = rate + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()));
                            //ot_hrs = ot_hrs + double.Parse(ds.Tables[0].Rows[ctr]["ot_hours"].ToString());
                            //ot_rate = ot_rate + double.Parse(ds.Tables[0].Rows[ctr]["ot_rate"].ToString());
                            //ot_amount = ot_amount + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString());
                            //service_charge = service_charge + (double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()));
                            //grand_tot = grand_tot + ((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString())));
                            //cgst = cgst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()));
                            //sgst = sgst + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()));
                            //igst = igst + (double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            //gst = gst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));
                            //ctc = ctc + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2);

                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                i3 = i3 + 2;
                                //state total
                                if (ds.Tables[0].Rows[ctr]["branch_type"].ToString() == "0" && !ds.Tables[0].Rows[ctr]["client"].ToString().Contains("BAJAJ ALLIANZ LIFE INSURANCE COMPANY LIMITED"))
                                {

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(K" + (ctc1 + 2) + ":K" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + ")</td><td>=ROUND(SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td></tr>";
                                    DUTY_HOURS = DUTY_HOURS + "," + "k" + (ctr + i3 + 1);
                                    RATE = RATE + "," + "L" + (ctr + i3 + 1);
                                    NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "M" + (ctr + i3 + 1);
                                    BASE_AMOUNT = BASE_AMOUNT + "," + "N" + (ctr + i3 + 1);
                                    OT_HOURS = OT_HOURS + "," + "O" + (ctr + i3 + 1);
                                    OT_RATE = OT_RATE + "," + "P" + (ctr + i3 + 1);
                                    OT_AMOUNT = OT_AMOUNT + "," + "Q" + (ctr + i3 + 1);
                                    TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "R" + (ctr + i3 + 1);
                                    SERVICE_CHARGE = SERVICE_CHARGE + "," + "S" + (ctr + i3 + 1);
                                    GRAND_TOTAL = GRAND_TOTAL + "," + "T" + (ctr + i3 + 1);
                                    CGST = CGST + "," + "U" + (ctr + i3 + 1);
                                    SGST = SGST + "," + "V" + (ctr + i3 + 1);
                                    IGST = IGST + "," + "W" + (ctr + i3 + 1);
                                    TOTAL_GST = TOTAL_GST + "," + "X" + (ctr + i3 + 1);
                                    TOTAL_CTC = TOTAL_CTC + "," + "Y" + (ctr + i3 + 1);

                                    if (state_change == 1)
                                    {
                                        lc.Text = lc.Text + "<tr><b> <td align=center colspan = " + colsize + ">Total</td> <td>=ROUND(SUM(" + DUTY_HOURS + "),2)</td> <td>=ROUND(SUM(" + RATE + "),2)</td> <td>=ROUND(SUM(" + NO_OF_PAID_DAYS + "),2)</td> <td>=ROUND(SUM(" + BASE_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + OT_HOURS + "),2)</td> <td>=ROUND(SUM(" + OT_RATE + "),2)</td> <td>=ROUND(SUM(" + OT_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + TOTAL_BASE_AMT_OT_AMT + "),2)</td> <td>=ROUND(SUM(" + SERVICE_CHARGE + "),2)</td> <td>=ROUND(SUM(" + GRAND_TOTAL + "),2)</td> <td>=ROUND(SUM(" + CGST + "),2)</td> <td>=ROUND(SUM(" + SGST + "),2)</td> <td>=ROUND(SUM(" + IGST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_GST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_CTC + "),2)</td> </b></tr>";

                                    }
                                }
                                //  lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Z" + (ctc1 + 2) + ":Z" + (ctr + i3) + "),2)</td></tr>";


                                //client total
                                else
                                {

                                    lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td><td>=ROUND(SUM(L" + (ctc1 + 2) + ":L" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(M" + (ctc1 + 2) + ":M" + (ctr + i3) + "),2)</td><td>=SUM(N" + (ctc1 + 2) + ":N" + (ctr + i3) + ")</td><td>=ROUND(SUM(O" + (ctc1 + 2) + ":O" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(P" + (ctc1 + 2) + ":P" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Q" + (ctc1 + 2) + ":Q" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(R" + (ctc1 + 2) + ":R" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(S" + (ctc1 + 2) + ":S" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(T" + (ctc1 + 2) + ":T" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(U" + (ctc1 + 2) + ":U" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(V" + (ctc1 + 2) + ":V" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(W" + (ctc1 + 2) + ":W" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(X" + (ctc1 + 2) + ":X" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Y" + (ctc1 + 2) + ":Y" + (ctr + i3) + "),2)</td><td>=ROUND(SUM(Z" + (ctc1 + 2) + ":Z" + (ctr + i3) + "),2)</td></tr>";


                                    DUTY_HOURS = DUTY_HOURS + "," + "L" + (ctr + i3 + 1);
                                    RATE = RATE + "," + "M" + (ctr + i3 + 1);
                                    NO_OF_PAID_DAYS = NO_OF_PAID_DAYS + "," + "N" + (ctr + i3 + 1);
                                    BASE_AMOUNT = BASE_AMOUNT + "," + "O" + (ctr + i3 + 1);
                                    OT_HOURS = OT_HOURS + "," + "P" + (ctr + i3 + 1);
                                    OT_RATE = OT_RATE + "," + "Q" + (ctr + i3 + 1);
                                    OT_AMOUNT = OT_AMOUNT + "," + "R" + (ctr + i3 + 1);
                                    TOTAL_BASE_AMT_OT_AMT = TOTAL_BASE_AMT_OT_AMT + "," + "S" + (ctr + i3 + 1);
                                    SERVICE_CHARGE = SERVICE_CHARGE + "," + "T" + (ctr + i3 + 1);
                                    GRAND_TOTAL = GRAND_TOTAL + "," + "U" + (ctr + i3 + 1);
                                    CGST = CGST + "," + "V" + (ctr + i3 + 1);
                                    SGST = SGST + "," + "W" + (ctr + i3 + 1);
                                    IGST = IGST + "," + "X" + (ctr + i3 + 1);
                                    TOTAL_GST = TOTAL_GST + "," + "Y" + (ctr + i3 + 1);
                                    TOTAL_CTC = TOTAL_CTC + "," + "Z" + (ctr + i3 + 1);
                                    if (state_change == 1)
                                    {
                                        lc.Text = lc.Text + "<tr><b> <td align=center colspan = " + colsize + ">Total</td> <td>=ROUND(SUM(" + DUTY_HOURS + "),2)</td> <td>=ROUND(SUM(" + RATE + "),2)</td> <td>=ROUND(SUM(" + NO_OF_PAID_DAYS + "),2)</td> <td>=ROUND(SUM(" + BASE_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + OT_HOURS + "),2)</td> <td>=ROUND(SUM(" + OT_RATE + "),2)</td> <td>=ROUND(SUM(" + OT_AMOUNT + "),2)</td> <td>=ROUND(SUM(" + TOTAL_BASE_AMT_OT_AMT + "),2)</td> <td>=ROUND(SUM(" + SERVICE_CHARGE + "),2)</td> <td>=ROUND(SUM(" + GRAND_TOTAL + "),2)</td> <td>=ROUND(SUM(" + CGST + "),2)</td> <td>=ROUND(SUM(" + SGST + "),2)</td> <td>=ROUND(SUM(" + IGST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_GST + "),2)</td> <td>=ROUND(SUM(" + TOTAL_CTC + "),2)</td> </b></tr>";
                                    }
                                }

                                //   lc.Text = lc.Text + "<tr><b><td align=center colspan = "+colsize+">Total</td><td>" + Math.Round(sub_total, 2) + "</td><td>" + paid_days + "</td><td>=SUM(N2:N"+(ctr+2)+")</td><td>" + ot_hrs + "</td><td>" + ot_rate + "</td><td>" + ot_amount + "</td><td>" + Math.Round((rate + ot_amount), 2) + "</td><td>" + Math.Round(service_charge, 2) + "</td><td>" + Math.Round(grand_tot, 2) + "</td><td>" + cgst + "</td><td>" + sgst + "</td><td>" + igst + "</td><td>" + gst + "</td><td>" + Math.Ceiling(Math.Round(ctc, 2)) + "</td></b></tr>";
                                //lc.Text = lc.Text + "<tr><b><td align=center colspan = " + colsize + ">Total</td>" + tot_hrs + "<td>=ROUND(SUM(L2:L" + (ctr + 2) + "),2)</td><td>=SUM(M2:M" + (ctr + 2) + ")</td><td>=ROUND(SUM(N2:N" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(O2:O" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(P2:P" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(Q2:Q" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(R2:R" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(S2:S" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(T2:T" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(U2:U" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(V2:V" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(W2:W" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(X2:X" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(Y2:Y" + (ctr + 2) + "),2)</td>" + tot + "</b></tr>";
                            }
                        }
                    }
                    else if (i == 3)
                    {
                        string color = "";
                        bodystr = "";
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

                        //int absent = Convert.ToInt32 (bodystr.Contains("A"));
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["client_branch_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"].ToString().ToUpper() + "</td>" + bodystr + "<td>" + ds.Tables[0].Rows[ctr]["tot_days_present"] + "</td><td>" + count + "</td><td>" + ds.Tables[0].Rows[ctr]["total days"] + "</td></tr>");
                        present_days = present_days + double.Parse(ds.Tables[0].Rows[ctr]["tot_days_present"].ToString());
                        absent_days = absent_days + count;
                        total_days = total_days + double.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());

                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            int col_span = (int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString()) + 7);
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = " + col_span + ">Total</td><td>" + present_days + "</td><td>" + absent_days + "</td><td>" + total_days + "</td></b></tr>";
                        }

                        bodystr = "";
                    }
                    else if (i == 4)
                    {
                        if (ds.Tables[0].Rows[ctr]["client_code"].ToString() == "UTKARSH")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_gst"].ToString().ToUpper() + "</td><td>INTERNATIONAL HOUSEKEEPING & MAINTENANCE SERVICES</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"].ToString().ToUpper() + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()), 2) + "</td><td>" + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + ((double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td></tr>");


                            grand_tot = grand_tot + ((double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString())));
                            cgst = cgst + (double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()));
                            sgst = sgst + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()));
                            igst = igst + (double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()));

                            ctc = ctc + Math.Round(((double.Parse(ds.Tables[0].Rows[ctr]["CGST9"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["SGST9"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST18"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()))) + (double.Parse(ds.Tables[0].Rows[ctr]["group_insurance_billing"].ToString())) + double.Parse(ds.Tables[0].Rows[ctr]["Service_charge"].ToString()) + (double.Parse(ds.Tables[0].Rows[ctr]["operational_cost"].ToString())) + (double.Parse(ds.Tables[0].Rows[ctr]["uniform"].ToString()))), 2);
                            if (ds.Tables[0].Rows.Count == ctr + 1)
                            {
                                lc.Text = lc.Text + "<tr><b><td align=center colspan = 8>Total</td><td>" + Math.Round(grand_tot, 2) + "</td><td>" + cgst + "</td><td>" + sgst + "</td><td>" + igst + "</td><td>" + Math.Ceiling(Math.Round(ctc, 2)) + "</td></b></tr>";
                            }
                        }
                        else if (ds.Tables[0].Rows[ctr]["client_code"].ToString() == "MAX")
                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"].ToString().ToUpper() + "</td><td>" + int.Parse(ds.Tables[0].Rows[ctr]["emp_count"].ToString()) + "</td><td>" + int.Parse(ds.Tables[0].Rows[ctr]["Present_Days"].ToString()) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["grand_total"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["Amount"].ToString()), 2) + "</td><td>" + invoice + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString() + "</td></tr>");

                        }
                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + invoice + "</td><td>" + bill_date + "</td><td>" + ds.Tables[0].Rows[ctr]["fromtodate"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"].ToString().ToUpper() + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["total"].ToString()) + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["Service_Charge"].ToString()) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["total"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["Service_Charge"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["SGST"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["IGST"].ToString()), 2) + "</td><td>" + Math.Round(double.Parse(ds.Tables[0].Rows[ctr]["CGST"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["SGST"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["IGST"].ToString()), 2) + "</td><td>=ROUND(SUM(K" + (ctr + 2) + ",O" + (ctr + 2) + "),2)</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 8>Total</td><td>=ROUND(SUM(I2:I" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(J2:J" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(K2:K" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(L2:L" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(M2:M" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(N2:N" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(O2:O" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(P2:P" + (ctr + 2) + "),2)</td></b></tr>";
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


    protected void ddl_bill_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    }


    protected void ddl_invoice_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (ddl_invoice_type.SelectedValue == "1")
        {
            ddl_designation.Items.Clear();
            desigpanel.Visible = false;
        }
        else if (ddl_invoice_type.SelectedValue == "2")
        {
            if (txt_bill_date.Text != "")
            {
                ddl_designation.Items.Clear();
                ddl_designation.Items.Insert(0, "Select");
                desigpanel.Visible = true;
                int i = 1; string temp = "";
                if (ddl_bill_state.SelectedValue == "ALL")
                {
                    temp = d1.getsinglestring("select group_concat(distinct(GRADE_CODE)) from pay_billing_unit_rate_history where client_code='" + ddl_bill_client.SelectedValue + "'  and year='" + txt_bill_date.Text.Substring(3) + "'and month='" + txt_bill_date.Text.Substring(0, 2) + "' and  invoice_flag != 0 and unit_code in (select unit_code from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_bill_client.SelectedValue + "')");
                }
                else if (ddl_bill_unit.SelectedValue == "ALL")
                {
                    temp = d1.getsinglestring("select group_concat(distinct(GRADE_CODE)) from pay_billing_unit_rate_history where client_code='" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "' and year='" + txt_bill_date.Text.Substring(3) + "'and month='" + txt_bill_date.Text.Substring(0, 2) + "' and  invoice_flag != 0 and unit_code in (select unit_code from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_bill_client.SelectedValue + "' and state_name = '" + ddl_bill_state.SelectedValue + "')");
                }
                else
                {
                    temp = d1.getsinglestring("select group_concat(distinct(GRADE_CODE)) from pay_billing_unit_rate_history where client_code='" + ddl_bill_client.SelectedValue + "'  and year='" + txt_bill_date.Text.Substring(3) + "'and month='" + txt_bill_date.Text.Substring(0, 2) + "' and unit_code='" + ddl_bill_unit.SelectedValue + "' and  invoice_flag != 0  ");
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
    protected void gv_billing_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_billing_details.UseAccessibleHeader = false;
            gv_billing_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    //payment details
    protected void btn_pmt_view_Click(object sender, EventArgs e)
    {

        hidtab.Value = "1";
        export_xl(1);

    }

    // payment_approve = 2 using for payment approve by MD 
    protected void btn_pmt_approve_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string where = "", MD_app_flag = " and payment_approve= 2 ", ac_app_flag = "and (payment_approve= 1 ||  payment_approve= 3)";
        try
        {

            where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_pmt_unit.SelectedValue + "'";
            if (ddl_pmt_state.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'";
            }
            else if (ddl_pmt_unit.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_pmt_state.SelectedValue + "'";
            }

            string unit_name = d.getsinglestring("select group_concat(distinct(unit_name)) from  pay_pro_master where " + where + " " + MD_app_flag);
            if (!unit_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This branch :" + unit_name + " already Approve');", true);
                return;

            }
            d.operation("update pay_pro_master set payment_approve='2'  where " + where + "  " + ac_app_flag);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment approve succsefully');", true);
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }

    // payment_approve = 3 using for payment rejection flag
    protected void btn_pmt_reject_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string where = "", flag = " and payment_approve = 3 ", month_flag = "and month_end = 1";
        try
        {

            where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_pmt_unit.SelectedValue + "'";
            if (ddl_pmt_state.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'";
            }
            else if (ddl_pmt_unit.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_pmt_state.SelectedValue + "'";
            }
            //month end
            string month_unit_name = d.getsinglestring("select group_concat(distinct(unit_name)) from  pay_pro_master where " + where + " " + month_flag);
            if (!month_unit_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Month end completed for this  :" + month_unit_name + " branch');", true);
                return;

            }
            //all ready reject
            string unit_name = d.getsinglestring("select group_concat(distinct(unit_name)) from  pay_pro_master where " + where + " " + flag);
            if (!unit_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This branch :" + unit_name + " already Rejected');", true);
                return;

            }
            d.operation("update pay_pro_master set payment_approve = 3 ,reject_reason='" + txt_pmt_reject_reason.Text + "' where " + where);
            text_pmt_clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Rejected succsefully');", true);
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }
    protected void ddl_pmt_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pmt_client.SelectedValue != "Select")
        {
            //State
            ddl_pmt_state.Items.Clear();
            ddl_pmt_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_pro_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pmt_client.SelectedValue + "'  and  month='" + txt_pmt_date.Text.Substring(0, 2) + "' and  year ='" + txt_pmt_date.Text.Substring(3) + "' and payment_approve != 0 order by STATE_NAME", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_pmt_state.DataSource = dt_item;
                    ddl_pmt_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pmt_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_pmt_state.DataBind();

                    ddl_pmt_state.Items.Insert(0, "ALL");
                    dt_item.Dispose();
                    d.con.Close();

                }
                dt_item.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); billing_panel.Visible = false; ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        }
        else
        {
            ddl_pmt_state.Items.Clear();
            ddl_pmt_unit.Items.Clear();
        }
    }
    protected void ddl_pmt_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pmt_state.SelectedValue != "ALL")
        {
            ddl_bill_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME', pay_unit_master. unit_code  FROM  pay_unit_master  inner join pay_pro_master on pay_unit_master.comp_code = pay_pro_master.comp_code and pay_unit_master.unit_code = pay_pro_master.unit_code WHERE pay_pro_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_pro_master.client_code  = '" + ddl_pmt_client.SelectedValue + "' AND pay_pro_master. state_name  = '" + ddl_pmt_state.SelectedValue + "' AND  month  = '" + txt_pmt_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_pmt_date.Text.Substring(3) + "' AND  payment_approve  != 0 ORDER BY pay_pro_master. UNIT_NAME ", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_pmt_unit.DataSource = dt_item;
                    ddl_pmt_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pmt_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_pmt_unit.DataBind();
                    ddl_pmt_unit.Items.Insert(0, "ALL");
                    dt_item.Dispose();
                    d.con.Close();
                }

                dt_item.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    }
    protected void ddl_pmt_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    }

    protected void text_pmt_clear()
    {

        ddl_pmt_client.SelectedValue = "Select";
        ddl_pmt_state.Items.Clear();
        ddl_pmt_unit.Items.Clear();
        txt_pmt_reject_reason.Text = "";
    }
    private void export_xl(int i)
    {
        string where = "", flag = "and payment_approve !=0";
        string pay_attendance_muster = " pay_attendance_muster ";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string sql = null;
        d.con.Open();
        if (i == 1)
        {

            where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_pmt_unit.SelectedValue + "'";
            if (ddl_pmt_state.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'";
            }
            else if (ddl_pmt_unit.SelectedValue == "ALL")
            {

                where = "month='" + txt_pmt_date.Text.Substring(0, 2) + "' and Year = '" + txt_pmt_date.Text.Substring(3) + "' and client_code = '" + ddl_pmt_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_pmt_state.SelectedValue + "'";
            }
            if (i == 1)
            {
                where = where + " and paypro_batch_id is null  " + flag;
            }
            else
            {
                where = where + " and paypro_batch_id is not null  " + flag;
            }


            sql = "SELECT comp_code,unit_code, client_code, state_name, unit_city, emp_name,employee_type, grade, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, gross, sal_pf, sal_esic, lwf_salary, sal_uniform_rate, PT_AMOUNT, sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ot_rate, ot_hours, ot_amount, common_allow, esic_allowances_salary, EMP_CODE, advance_payment_mode, Installment, advance, salary_status, unit_name,payment,month,year,paypro_batch_id from pay_pro_master WHERE  " + where;
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
                Response.AddHeader("content-disposition", "attachment;filename=EMPLOYEE_SALARY.xls");
            }
            else if (i == 2)
            {
                Response.AddHeader("content-disposition", "attachment;filename=PROVISIONAL_SALARY.xls");
            }
            else if (i == 3)
            {
                Response.AddHeader("content-disposition", "attachment;filename=PAID_SALARY.xls");
            }
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds, i);
            Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds, i);
            Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null, i);
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


    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i;
        static int ctr;
        public MyTemplate1(ListItemType type, DataSet ds, int i)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=45 align=center>SALARY RATE BREAKUP FOR " + ds.Tables[0].Rows[ctr]["client"] + "</th></tr><table border=1><tr><th>Sr. No.</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>DEG</th><th>BASIC</th><th>HRA</th><th>BONUS</th><th>LEAVE</th><th>WASHING</th><th>TRAVELLING</th><th>EDUCATION</th><th>OTHER ALLOWANCES</th><th>CCA</th><th>ALLOWANCES</th><th>GRATUITY</th><th>SPECIAL ALLOWANCES</th><th>TOTAL</th><th>OT RATE</th><th>OT HOURS</th><th>OT AMOUNT</th><th>GROSS</th><th>PF</th><th>ESIC</th><th>LWF</th><th>UNIFORM RATE</th><th>PT</th><th>BONUS</th><th>LEAVE</th><th>GRATUITY</th><th>ALLOWANCES</th><th>OTHER ALLOWANCES</th><th>PRESENT DAYS</th><th>SALARY STATUS</th><th bgcolor=silver>N / I</th><th bgcolor=silver>AMOUNT</th><th bgcolor=silver>DATE</th><th bgcolor=silver> A/C HOLDER NAME</th><th bgcolor=silver>ACCOUNT NUMBER</th><th></th><th></th><th bgcolor=silver>BENE NO</th><th></th><th bgcolor=silver>IFSC CODE</th><th bgcolor=silver>STATUS</th><th></th>" + (i == 3 ? "<th>BATCH ID</th>" : "") + "</tr>");
                    break;
                case ListItemType.Item:
                    string color = "";
                    if (ds.Tables[0].Rows[ctr]["salary_status"].ToString() == "Hold") { color = "red"; }
                    double Payment = Math.Floor(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()) - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"].ToString())) + (Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString())));

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["grade"].ToString().ToUpper() + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["emp_basic_vda"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["hra_amount_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["washing_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["travelling_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["education_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["allowances_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["cca_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["other_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_ot"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_rate"].ToString()), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["ot_amount"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["gross"].ToString()), 2) + "</td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_pf"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_esic"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["lwf_salary"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_uniform_rate"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["PT_AMOUNT"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["leave_sal_after_gross"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["gratuity_after_gross"]), 2) + " </td ><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["common_allow"]), 2) + " </td><td>" + Math.Round(Convert.ToDouble(ds.Tables[0].Rows[ctr]["esic_allowances_salary"]), 2) + " </td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + " </td><td style=text-align:center;vertical-align:middle; bgcolor=" + color + ">" + ds.Tables[0].Rows[ctr]["salary_status"] + " </td><td>" + ds.Tables[0].Rows[ctr]["NI"] + " </td><td>" + (Payment - (Convert.ToDouble(ds.Tables[0].Rows[ctr]["fine"].ToString()) + Convert.ToDouble(ds.Tables[0].Rows[ctr]["EMP_ADVANCE_PAYMENT"].ToString()))) + "</td><td>'" + ds.Tables[0].Rows[ctr]["date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Bank_holder_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + " </td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["Account_no"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + " </td><td>11</td>" + (i == 3 ? "<th>'" + ds.Tables[0].Rows[ctr]["paypro_batch_id"] + "</th>" : "") + "</tr>");

                    if (ds.Tables[0].Rows.Count == ctr + 1)
                    {
                        lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=SUM(F3:F" + (ctr + 3) + ")</td><td>=SUM(G3:G" + (ctr + 3) + ")</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td><td>=SUM(K3:K" + (ctr + 3) + ")</td><td>=SUM(L3:L" + (ctr + 3) + ")</td><td>=SUM(M3:M" + (ctr + 3) + ")</td><td>=SUM(N3:N" + (ctr + 3) + ")</td><td>=SUM(O3:O" + (ctr + 3) + ")</td><td>=SUM(P3:P" + (ctr + 3) + ")</td><td>=SUM(Q3:Q" + (ctr + 3) + ")</td><td>=SUM(R3:R" + (ctr + 3) + ")</td><td>=SUM(S3:S" + (ctr + 3) + ")</td><td>=SUM(T3:T" + (ctr + 3) + ")</td><td>=SUM(U3:U" + (ctr + 3) + ")</td><td>=SUM(V3:V" + (ctr + 3) + ")</td><td>=SUM(W3:W" + (ctr + 3) + ")</td><td>=SUM(X3:X" + (ctr + 3) + ")</td><td>=SUM(Y3:Y" + (ctr + 3) + ")</td><td>=SUM(Z3:Z" + (ctr + 3) + ")</td><td>=SUM(AA3:AA" + (ctr + 3) + ")</td><td>=SUM(AB3:AB" + (ctr + 3) + ")</td><td>=SUM(AC3:AC" + (ctr + 3) + ")</td><td>=SUM(AD3:AD" + (ctr + 3) + ")</td><td>=SUM(AE3:AE" + (ctr + 3) + ")</td><td>=SUM(AF3:AF" + (ctr + 3) + ")</td><td>=SUM(AG3:AG" + (ctr + 3) + ")</td><td></td><td></td><td>=SUM(AJ3:AJ" + (ctr + 3) + ")</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>";
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

    protected void all_branch()
    {

        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_NAME, '_', UNIT_ADD1) AS 'UNIT_NAME', unit_code, flag FROM pay_unit_master  WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client1.SelectedValue + "' AND branch_status = 0  and unit_code in (select distinct(p.unit_code) from pay_pro_master p INNER JOIN pay_billing_unit_rate_history  b on  p.comp_code = b.comp_code and p.unit_code = b.unit_code and p.month = b.month and p.year = b.year where  p.comp_code = '" + Session["COMP_CODE"].ToString() + "' and p.client_code='" + ddl_client1.SelectedValue + "' and p.month = '" + txt_month.Text.Substring(0, 2) + "' and  p.year ='" + txt_month.Text.Substring(3) + "' and p.payment_approve = 2 and invoice_flag = 2) ORDER BY unit_name ", d.con);
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
                ddl_unitcode.Items.Insert(0, "ALL");
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


    protected void ddl_arrears_Client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_arrears_client.SelectedValue != "Select")
        {
            //State
            DataSet ds = new DataSet();
            ddl_arrears_state.Items.Clear();
            ddl_arrears_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_arrears_client.SelectedValue + "'   order by STATE_NAME", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_arrears_state.DataSource = dt_item;
                    ddl_arrears_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_arrears_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_arrears_state.DataBind();

                    ddl_arrears_state.Items.Insert(0, "ALL");
                    dt_item.Dispose();
                    d.con.Close();

                }
                dt_item.Dispose();
                d.con.Close();
                load_arrears_gridview(" and client_code ='" + ddl_arrears_client.SelectedValue + "'");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();



            }
        }
        else
        {
            ddl_arrears_state.Items.Clear();
            ddl_arrears_unit.Items.Clear();
        }
    }

    protected void ddl_arrears_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_arrears_state.SelectedValue != "Select")
        {
            //State
            DataSet ds = new DataSet();
            ddl_arrears_unit.Items.Clear();

            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', pay_unit_master. UNIT_NAME , '_', pay_unit_master. UNIT_ADD1 ) AS 'UNIT_NAME', pay_unit_master. unit_code  FROM  pay_unit_master  WHERE comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND client_code  = '" + ddl_arrears_client.SelectedValue + "' AND  state_name  = '" + ddl_arrears_state.SelectedValue + "'  ORDER BY  UNIT_NAME ", d.con);

            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_arrears_unit.DataSource = dt_item;
                    ddl_arrears_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_arrears_unit.DataValueField = dt_item.Columns[0].ToString();
                    ddl_arrears_unit.DataBind();

                    ddl_arrears_unit.Items.Insert(0, "ALL");
                    dt_item.Dispose();
                    d.con.Close();

                }
                dt_item.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();

                load_arrears_gridview(" and client_code ='" + ddl_arrears_client.SelectedValue + "' AND state_name ='" + ddl_arrears_state.SelectedValue + "'");

            }
        }
        else
        {
            ddl_arrears_state.Items.Clear();
            ddl_arrears_unit.Items.Clear();
        }
    }
    protected void ddl_arrears_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_arrears_unit.SelectedValue != "Select")
        {
            //State
            DataSet ds = new DataSet();


            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                load_arrears_gridview(" and client_code ='" + ddl_arrears_client.SelectedValue + "' AND unit_code ='" + ddl_arrears_unit.SelectedValue + "'");
            }
            catch (Exception ex) { throw ex; }
            finally
            {


            }
        }

    }


    protected void lnk_approve_Command(object sender, CommandEventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string row_id = e.CommandArgument.ToString();
            string where = "";
            string client_code = "";
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select client_code,state_name,unit_code,month,year from pay_arrears_bill_request where comp_code='" + Session["comp_code"] + "' and id ='" + row_id + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr.GetValue(0).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code in (select distinct unit_code from pay_billing_unit_rate_history where comp_code='" + Session["comp_code"] + "' and client_code='" + dr.GetValue(0).ToString() + "'  and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "')";
                    client_code = " and client_code ='" + dr.GetValue(0).ToString() + "'";
                }
                if (dr.GetValue(1).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code in (select distinct unit_code from pay_billing_unit_rate_history where comp_code='" + Session["comp_code"] + "' and client_code='" + dr.GetValue(0).ToString() + "' and state_name = '" + dr.GetValue(1).ToString() + "'  and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "')";
                }
                if (dr.GetValue(2).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code ='" + dr.GetValue(2).ToString() + "'";
                }
            }
            d.con.Close();
            d.operation("update pay_attendance_muster set arrears_flag = 1 where " + where);
            d.operation("update pay_arrears_bill_request set status = 'Arrears Request Approved By MD' where comp_code='" + Session["comp_code"] + "' and id ='" + row_id + "'");
            load_arrears_gridview(client_code);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Arrears Bill Request Approve Successfully !!');", true);

        }
        catch (Exception ex) { throw ex; }
        finally { }
    }
    protected void lnk_reject_Command(object sender, CommandEventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string row_id = e.CommandArgument.ToString();
            string where = "comp_code='" + Session["comp_code"] + "'";
            string client_code = "";
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select client_code,state_name,unit_code,month,year from pay_arrears_bill_request where comp_code='" + Session["comp_code"] + "' and id ='" + row_id + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr.GetValue(0).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code in (select distinct unit_code from pay_billing_unit_rate_history where comp_code='" + Session["comp_code"] + "' and client_code='" + dr.GetValue(0).ToString() + "'  and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "')";
                    client_code = " and client_code ='" + dr.GetValue(0).ToString() + "'";
                }
                if (dr.GetValue(1).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code in (select distinct unit_code from pay_billing_unit_rate_history where comp_code='" + Session["comp_code"] + "' and client_code='" + dr.GetValue(0).ToString() + "' and state_name = '" + dr.GetValue(1).ToString() + "'  and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "')";
                }
                if (dr.GetValue(2).ToString() != "ALL")
                {
                    where = "comp_code='" + Session["comp_code"] + "' and month='" + dr.GetValue(3).ToString() + "' and year = '" + dr.GetValue(4).ToString() + "'  and unit_code ='" + dr.GetValue(2).ToString() + "'";
                }
            }
            d.con.Close();
            d.operation("update pay_attendance_muster set arrears_flag = 0 where " + where);

            d.operation("update pay_arrears_bill_request set status = 'Arrears Request Reject By MD' where comp_code='" + Session["comp_code"] + "' and id ='" + row_id + "'");
            load_arrears_gridview(client_code);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Arrears Bill Request Rejected Successfully !!');", true);

        }
        catch (Exception ex) { throw ex; }
        finally { }
    }

    protected void load_arrears_gridview(string client_code)
    {


        try
        {
            DataSet ds = new DataSet();

            ds = d.select_data("SELECT Id,IF(client_code = 'ALL', 'ALL', (SELECT client_name FROM pay_client_master WHERE pay_client_master.comp_code = pay_arrears_bill_request.comp_code AND pay_client_master.client_code = pay_arrears_bill_request.client_code)) AS 'client_name', IF(state_name = 'ALL', 'ALL', state_name) AS 'state_name', IF(unit_code = 'ALL', 'ALL', (SELECT unit_name FROM pay_unit_master WHERE pay_unit_master.comp_code = pay_arrears_bill_request.comp_code AND pay_unit_master.unit_code = pay_arrears_bill_request.unit_code)) AS 'unit_name', CONCAT(month, '/', year) AS 'monthyear', Status FROM pay_arrears_bill_request WHERE comp_code='" + Session["comp_code"] + "'  " + client_code + "  and  month='" + txt_arrears_date.Text.Substring(0, 2) + "' and  year ='" + txt_arrears_date.Text.Substring(3) + "'");
            gv_arrears_gridview.DataSource = ds;
            gv_arrears_gridview.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {


        }


    }
    protected void gv_arrears_gridview_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_arrears_gridview.UseAccessibleHeader = false;
            gv_arrears_gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    //suraj start
    public void lnk_monthend()
    {
        try
        {
            monthend_count = "0";


            panel10.Visible = true;
            // panel4.Visible = true;

            // MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT`client_name`, `billing_state` AS 'StateName',`billing_client_code`,`month`,`year`,`month_end `FROM` pay_billing_master_history INNER JOIN `pay_client_master` ON `pay_billing_master_history`.`billing_client_code` = `pay_client_master`.`client_code` AND `pay_billing_master_history`.`comp_code` = `pay_client_master`.`comp_code` WHERE `pay_billing_master_history`.`billing_client_code` = '" + ddl_client1.SelectedValue + "' AND `pay_billing_master_history`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND month ='" + txt_month.Text.Substring(0,2) + "' and year ='"+txt_month.Text.Substring(3)+"' and `month_end` = '0' GROUP BY `billing_state`", d.con);

            // MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT client_name, billing_state AS 'StateName', billing_client_code, month, year, month_end FROM pay_billing_master_history INNER JOIN pay_client_master ON pay_billing_master_history.billing_client_code = pay_client_master.client_code AND pay_billing_master_history.comp_code = pay_client_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + ddl_client1.SelectedValue + "' AND pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND month = '" + txt_month.Text.Substring(0,2) + "' AND year = '"+txt_month.Text.Substring(3)+"' AND month_end = '0' GROUP BY billing_state", d.con);
            //ViewState["monthend_count"] = "";
            gv_monthend_count.DataSource = null;
            gv_monthend_count.DataBind();
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT client as 'client_name',state_name FROM pay_billing_unit_rate_history WHERE client_code ='" + ddl_client1.SelectedValue + "' AND comp_code ='" + Session["COMP_CODE"].ToString() + "' AND month = '" + txt_month.Text.Substring(0, 2) + "'AND year = '" + txt_month.Text.Substring(3) + "'AND  invoice_flag = 2   AND month_end =  0 group by state_name ", d.con);
            d.con.Open();
            // MySqlDataReader dr1 = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            cmd1.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ViewState["monthend_count"] = dt.Rows.Count.ToString();
                monthend_count = ViewState["monthend_count"].ToString();

                gv_monthend_count.DataSource = dt;
                gv_monthend_count.DataBind();

            }
            dt.Dispose();
            d.con.Close();
            monthend_count = ViewState["monthend_count"].ToString();
            //newpanel.Visible = true;
            // panel4.Visible = true;
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
    //suraj close
    protected void gv_fullmonthot_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_fullmonthot.UseAccessibleHeader = false;
            gv_fullmonthot.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_arrears_gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

    }
    protected void btn_list_bill_upload_Click(object sender, EventArgs e)
    {
        load_grdview();
    }
    private void load_grdview()
    {

        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,(select client_name from pay_client_master where pay_client_master.client_code = pay_bill_invoices.client_code) as client_code, state_name, CASE WHEN `unit_code` = 'ALL' THEN 'ALL' ELSE (SELECT `unit_name` FROM `pay_unit_master` WHERE `pay_unit_master`.`unit_code` = `pay_bill_invoices`.`unit_code` AND `pay_unit_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "') END AS 'unit_code',month,year,bill_type,invoice_number,concat('~/approved_bills/',file_name) as Value,dispatch_date,receiving_date,pod_number ,DATE_FORMAT(sent_date, '%d/%m/%Y') AS 'mail_send_date', CASE WHEN sent_flag = 1 THEN 'Bill Send' ELSE 'Pending' END AS 'current_status' FROM pay_bill_invoices where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and client_code= '" + ddl_bill_client.SelectedValue + "' and month = '" + txt_bill_date.Text.Substring(0, 2) + "' and year = '" + txt_bill_date.Text.Substring(3) + "' ", d.con1);
        DataSet DS1 = new DataSet();
        try
        {

            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            if (DS1.Tables[0].Rows.Count.Equals(0))
            {
                gv_bill_list_upload.DataSource = null;
                gv_bill_list_upload.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('No Record Found !!!')", true);

            }
            else
            {
                gv_bill_list_upload.DataSource = DS1;
                gv_bill_list_upload.DataBind();
            }

            d.con1.Close();
            //if (txt_bill_date.Text.Length > 0)
            //{
            //    System.Data.DataTable dt_item = new System.Data.DataTable();
            //    gv_bill_list_upload.DataSource = null;
            //    gv_bill_list_upload.DataBind();
            //    dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txt_bill_date.Text, 9);

            //    if (dt_item.Rows.Count > 0)
            //    {
            //        ViewState["bill_remain"] = dt_item.Rows.Count.ToString();

            //        gv_bill_list_upload.DataSource = dt_item;
            //        gv_bill_list_upload.DataBind();
            //        // grd_bill_remaining.Visible = true;
            //    }
            //    dt_item.Dispose();
            // }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    protected void ink_edit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string invoice_no = row.Cells[6].Text;
        string month = get1_month(row.Cells[3].Text);
        string year = row.Cells[4].Text;
        string b_type = row.Cells[5].Text;

        result = d.operation("update pay_bill_invoices set sent_flag = '2' where invoice_number = '" + invoice_no + "' and month = " + month + " and year = " + year + " and bill_type = '" + b_type + "'");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bill Upload Edit Permission By MD !!');", true);

    }
    protected void gv_bill_list_upload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }


        if (e.Row.Cells[3].Text == "1")
        {
            e.Row.Cells[3].Text = "January";
        }
        else if (e.Row.Cells[3].Text == "2")
        {
            e.Row.Cells[3].Text = "February";
        }
        else if (e.Row.Cells[3].Text == "3")
        {
            e.Row.Cells[3].Text = "March";
        }
        else if (e.Row.Cells[3].Text == "4")
        {
            e.Row.Cells[3].Text = "April";
        }
        else if (e.Row.Cells[3].Text == "5")
        {
            e.Row.Cells[3].Text = "May";
        }
        else if (e.Row.Cells[3].Text == "6")
        {
            e.Row.Cells[3].Text = "June";
        }
        else if (e.Row.Cells[3].Text == "7")
        {
            e.Row.Cells[3].Text = "July";
        }
        else if (e.Row.Cells[3].Text == "8")
        {
            e.Row.Cells[3].Text = "August";
        }
        else if (e.Row.Cells[3].Text == "9")
        {
            e.Row.Cells[3].Text = "September";
        }
        else if (e.Row.Cells[3].Text == "10")
        {
            e.Row.Cells[3].Text = "October";
        }
        else if (e.Row.Cells[3].Text == "11")
        {
            e.Row.Cells[3].Text = "November";
        }
        else if (e.Row.Cells[3].Text == "12")
        {
            e.Row.Cells[3].Text = "December";
        }
    }
    private string get1_month(string month)
    {
        if (month == "January") { return "1"; }
        if (month == "February") { return "2"; }
        if (month == "March") { return "3"; }
        if (month == "April") { return "4"; }
        if (month == "May") { return "5"; }
        if (month == "June") { return "6"; }
        if (month == "July") { return "7"; }
        if (month == "August") { return "8"; }
        if (month == "September") { return "9"; }
        if (month == "October") { return "10"; }
        if (month == "November") { return "11"; }
        if (month == "December") { return "12"; }
        return month;
    }
    protected void ddl_region_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlregion.SelectedValue != "Select")
        {
            ddl_bill_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;
            if (ddlregion.SelectedValue != "ALL")
            {
                cmd_item = new MySqlDataAdapter("SELECT DISTINCT (STATE_NAME) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"] + "' AND client_code = '" + ddl_bill_client.SelectedValue + "' AND ZONE = '" + ddlregion.SelectedValue + "' ORDER BY 1", d.con);
            }
            else
            {
                cmd_item = new MySqlDataAdapter("SELECT DISTINCT (STATE_NAME) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"] + "' AND client_code = '" + ddl_bill_client.SelectedValue + "' ORDER BY 1", d.con);
            }
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_bill_state.DataSource = dt_item;
                    ddl_bill_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_bill_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_bill_state.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_bill_state.Items.Insert(0, "Select");
                ddl_bill_state.Items.Insert(1, "ALL");
                ddl_bill_unit.Items.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                d.con.Close();
            }

        }

    }
    protected void region()
    {
        if (ddl_bill_client.SelectedValue != "Select")
        {
            ddlregion.Items.Clear();
            System.Data.DataTable dt_item2 = new System.Data.DataTable();

            MySqlDataAdapter cmd_item2 = new MySqlDataAdapter("SELECT DISTINCT  pay_zone_master.region FROM pay_client_billing_details INNER JOIN pay_zone_master  ON  pay_client_billing_details . comp_code  =  pay_zone_master . comp_code  AND  pay_client_billing_details . client_code  =  pay_zone_master . client_code  WHERE  pay_client_billing_details . client_code  = '" + ddl_bill_client.SelectedValue + "' and type = 'Region' ", d.con);
            //MySqlDataAdapter cmd_item1 = new MySqlDataAdapter(" Select DISTINCT region from pay_zone_master where comp_code ='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' ", d.con);

            d.con.Open();
            try
            {
                cmd_item2.Fill(dt_item2);
                if (dt_item2.Rows.Count > 0)
                {
                    ddlregion.DataSource = dt_item2;
                    ddlregion.DataTextField = dt_item2.Columns[0].ToString();
                    ddlregion.DataValueField = dt_item2.Columns[0].ToString();
                    ddlregion.DataBind();
                }
                dt_item2.Dispose();
                d.con.Close();
                ddlregion.Items.Insert(0, "Select");
                ddlregion.Items.Insert(1, "ALL");
                //  ddl_state_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
    }
    protected void ddl_vendor_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        po_no_panel.Visible = false;
        btn_edit_vendor_invoice.Visible = false;
        invoice_no_panel.Visible = false;
        hidtab.Value = "6";
        if (ddl_vendor_type.SelectedValue != "0")
        {

            ddl_vendor_name.Items.Clear();

            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;

            if (ddl_vendor_type.SelectedValue.Equals("1"))
            {
                cmd_item = new MySqlDataAdapter("SELECT cust_name,cust_code FROM pay_transactionp where comp_code='" + Session["comp_code"].ToString() + "' and final_invoice_flag = 1  group by cust_code ORDER BY cust_code", d.con);
            }
            else
            {

                cmd_item = new MySqlDataAdapter("SELECT cust_name,cust_code FROM pay_transaction_po where comp_code='" + Session["comp_code"].ToString() + "' and final_po_flag = 1  group by cust_code ORDER BY cust_code", d.con);

            }
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_vendor_name.DataSource = dt_item;
                    ddl_vendor_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_vendor_name.DataValueField = dt_item.Columns[1].ToString();
                    ddl_vendor_name.DataBind();
                }
                ddl_vendor_name.Items.Insert(0, "Select");
                dt_item.Dispose();
                d.con.Close();

            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }
        else
        {
            ddl_vendor_name.Items.Clear();
            ddl_pi_no.Items.Clear();
            ddl_po_no.Items.Clear();
        }
    }
    protected void ddl_vendor_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        po_no_panel.Visible = false;
        invoice_no_panel.Visible = false;
        btn_edit_vendor_invoice.Visible = false;
        if (ddl_vendor_type.SelectedValue == "1")
        {
            try
            {

                ddl_pi_no.Items.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter dr = new MySqlDataAdapter("SELECT doc_no FROM pay_transactionp where comp_code='" + Session["comp_code"].ToString() + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ", d1.con);
                d1.con.Open();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_pi_no.DataSource = dt;
                    ddl_pi_no.DataTextField = dt.Columns[0].ToString();
                    ddl_pi_no.DataValueField = dt.Columns[0].ToString();
                    ddl_pi_no.DataBind();
                }
                dt.Dispose();

                d1.con.Close();
                ddl_pi_no.Items.Insert(0, "ALL");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d1.con.Close();
            }
        }
        else if (ddl_vendor_type.SelectedValue == "2")
        {

            try
            {

                ddl_po_no.Items.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlDataAdapter dr = new MySqlDataAdapter("SELECT PO_NO FROM pay_transaction_po where comp_code='" + Session["comp_code"].ToString() + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ", d1.con);
                d1.con.Open();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_po_no.DataSource = dt;
                    ddl_po_no.DataTextField = dt.Columns[0].ToString();
                    ddl_po_no.DataValueField = dt.Columns[0].ToString();
                    ddl_po_no.DataBind();
                }
                dt.Dispose();

                d1.con.Close();
                ddl_po_no.Items.Insert(0, "ALL");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d1.con.Close();
            }
        }
    }
    protected void ddl_pi_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        po_no_panel.Visible = false;
        invoice_no_panel.Visible = false;
        btn_edit_vendor_invoice.Visible = false;
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            gv_invoice_no.DataSource = null;
            gv_invoice_no.DataBind();

            DataSet ds1 = new DataSet();
            string where = "";
            where = " where comp_code='" + Session["comp_code"].ToString() + "' and DOC_NO = '" + ddl_pi_no.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'";
            if (ddl_pi_no.SelectedValue == "ALL")
            {
                where = " where  comp_code='" + Session["comp_code"].ToString() + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'";
            }

            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT DOC_NO as 'Invoice No' ,concat(month,'/',year) as 'Month Year',CUST_NAME as 'Vendor Name',FINAL_PRICE as 'Amount' FROM pay_transactionp " + where, d.con);

            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gv_invoice_no.DataSource = ds1.Tables[0];
                gv_invoice_no.DataBind();
                d.con1.Close();
                invoice_no_panel.Visible = true;
                btn_edit_vendor_invoice.Visible = true;
            }
            else
            {
                invoice_no_panel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found !!');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

    }
    protected void ddl_po_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        po_no_panel.Visible = false;
        invoice_no_panel.Visible = false;
        btn_edit_vendor_invoice.Visible = false;
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            gv_po_no.DataSource = null;
            gv_po_no.DataBind();

            DataSet ds1 = new DataSet();
            string where = "";

            where = " WHERE comp_code='" + Session["comp_code"].ToString() + "' and PO_NO = '" + ddl_po_no.SelectedValue + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ";
            if (ddl_pi_no.SelectedValue == "ALL")
            {
                where = " WHERE comp_code='" + Session["comp_code"].ToString() + "' and PO_NO = '" + ddl_po_no.SelectedValue + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ";
            }

            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT  PO_NO as 'Purchase No',concat(month,'/',year) as 'Month Year',CUST_NAME as 'Vendor Name',SUB_TOTAL as 'Amount' FROM pay_transaction_po " + where, d.con);

            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gv_po_no.DataSource = ds1.Tables[0];
                gv_po_no.DataBind();
                d.con1.Close();
                po_no_panel.Visible = true;
                btn_edit_vendor_invoice.Visible = true;
            }
            else
            {
                po_no_panel.Visible = false;
                btn_edit_vendor_invoice.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found !!');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

    }
    protected void gv_invoice_no_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_invoice_no.UseAccessibleHeader = false;
            gv_invoice_no.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void gv_po_no_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_po_no.UseAccessibleHeader = false;
            gv_po_no.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void btn_edit_vendor_invoice_Click(object sender, EventArgs e)
    {
        try
        {
            string where = "";
            if (ddl_vendor_type.SelectedValue == "1")
            {

                where = " and  DOC_NO = '" + ddl_pi_no.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'";
                if (ddl_pi_no.SelectedValue == "ALL")
                {
                    where = " and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'";
                }

                result = d.operation("update pay_transactionp set final_invoice_flag = 0 where comp_code ='" + Session["comp_code"].ToString() + "'" + where);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Editing Permission By MD !!');", true);
            }
            else
            {

                where = " WHERE comp_code='" + Session["comp_code"].ToString() + "' and PO_NO = '" + ddl_po_no.SelectedValue + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ";
                if (ddl_pi_no.SelectedValue == "ALL")
                {
                    where = " WHERE comp_code='" + Session["comp_code"].ToString() + "' and PO_NO = '" + ddl_po_no.SelectedValue + "' and cust_code='" + ddl_vendor_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' ";
                }
                result = d.operation("update pay_transaction_po set final_po_flag = 0 " + where);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Editing Permission By MD !!');", true);
            }
        }
        catch (Exception ex)
        { throw ex; }

        po_no_panel.Visible = false;
        invoice_no_panel.Visible = false;
        btn_edit_vendor_invoice.Visible = false;
    }
    protected void gv_from_date_to_date_PreRender(object sender, EventArgs e)
    {

    }
    protected void ddl_manual_invoice_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_manual_invoice_type.SelectedValue == "0")
        {
            panel_branch_name.Visible = false;

        }

        else if (ddl_manual_invoice_type.SelectedValue == "1")
        {
            panel_branch_name.Visible = false;
            panel_client_state.Visible = false;
            panel_manual_other_state.Visible = true;


            d.con.Open();
            try
            {
                MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where comp_code ='" + Session["comp_code"].ToString() + "'and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' )  ORDER BY STATE", d.con);
                DataTable dt_client = new DataTable();
                grd_client.Fill(dt_client);
                if (dt_client.Rows.Count > 0)
                {

                    ddl_state_name_other.DataSource = dt_client;
                    ddl_state_name_other.DataTextField = dt_client.Columns[0].ToString();
                    ddl_state_name_other.DataValueField = dt_client.Columns[0].ToString();
                    ddl_state_name_other.DataBind();

                }


            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ddl_state_name_other.Items.Insert(0, new ListItem("Select"));
                d.con.Close();
            }


        }

    }
    protected void ddl_state_name_other_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            //MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT distinct `client_name`FROM `pay_report_gst` WHERE `comp_code` = 'C01'  AND `state_name` = 'Bihar' AND `month` = '02'  AND `year` = '2020'  AND `manual_invoice_type` = '1' AND `type` = 'manual'  AND `final_invoice` = '1'", d.con);
            //DataTable dt_client = new DataTable();
            //grd_client.Fill(dt_client);
            //if (dt_client.Rows.Count > 0)
            //{

            //    ddl_state_name_other.DataSource = dt_client;
            //    ddl_state_name_other.DataTextField = dt_client.Columns[0].ToString();
            //    ddl_state_name_other.DataValueField = dt_client.Columns[0].ToString();
            //    ddl_state_name_other.DataBind();

            string bill_to_party = d.getsinglestring("SELECT distinct `client_name`FROM `pay_report_gst` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `state_name` = '" + ddl_state_name_other.SelectedValue + "' AND `month` = '" + txt_bill_date.Text.Substring(0, 2) + "' AND `year` = '" + txt_bill_date.Text.Substring(3) + "'  AND `manual_invoice_type` = '1' AND `type` = 'manual'  AND `final_invoice` != '0'");

            txt_bill_party.Text = "" + bill_to_party + "";

            // }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void ddl_credit_debit_note_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();

            ddl_credit_debit_no.DataSource = null;

              MySqlDataAdapter grd_client = null;
              DataTable dt_client = new DataTable();
            if (ddl_credit_debit_note.SelectedValue == "1")
            {

             grd_client = new MySqlDataAdapter("SELECT distinct `credit_note_no` FROM `credit_debit_note` WHERE  comp_code = '"+Session["comp_code"].ToString()+"' and  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and type ='1' ", d.con);
              
                grd_client.Fill(dt_client);
                if (dt_client.Rows.Count > 0)
                {

                    ddl_credit_debit_no.DataSource = dt_client;
                    ddl_credit_debit_no.DataTextField = dt_client.Columns[0].ToString();
                    ddl_credit_debit_no.DataValueField = dt_client.Columns[0].ToString();
                    ddl_credit_debit_no.DataBind();

                }

                }
                else
                    if (ddl_credit_debit_note.SelectedValue == "2")
                    {

                       grd_client = new MySqlDataAdapter("SELECT distinct `credit_note_no` FROM `credit_debit_note` WHERE  `final_invoice_flag` = '1' and client_name = '" + ddl_bill_client.SelectedItem + "' and month ='" + txt_bill_date.Text.Substring(0, 2) + "'  and year = '" + txt_bill_date.Text.Substring(3) + "' and type ='2' ", d.con);
               
                grd_client.Fill(dt_client);
                if (dt_client.Rows.Count > 0)
                {

                    ddl_credit_debit_no.DataSource = dt_client;
                    ddl_credit_debit_no.DataTextField = dt_client.Columns[0].ToString();
                    ddl_credit_debit_no.DataValueField = dt_client.Columns[0].ToString();
                    ddl_credit_debit_no.DataBind();

                   




                    }


            }
        }
        catch (Exception ex) { throw ex; }
        finally{

            ddl_credit_debit_no.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        
        }
    }
}
