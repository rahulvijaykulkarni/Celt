using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Globalization;


public partial class account_reports : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    BillingSalary bs = new BillingSalary();
    string aa = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument crystalReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (d.getaccess(Session["ROLE"].ToString(), "Account", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Account", Session["COMP_CODE"].ToString()) == "R")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Account", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Account", Session["COMP_CODE"].ToString()) == "C")
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
            seles();
            btn_update_receipt.Visible = false;
            
           client_name();
            payment_type_selection();
            btn_add_others.Visible = false;
            txt_description.Visible = false;
            Panel6.Visible = false;
            Panel_gv_pmt.Visible = false;
            head_transction.Visible = false;
            btn_update.Visible = false;
            panel_add_other.Visible = false;
            Panel_other_desc.Visible = false;
            panel_mode.Visible = false;
            //panel2.Visible = true;
            load_gv_payment("");
            load_gv_debit_pmt_details("1");
            ddl_batch_no.Items.Insert(0, new ListItem("Select"));
            submit_btn.Visible = false;
            cheque.Visible = false;
            ddl_mode_transfer.SelectedValue = "Select";
             for_other.Visible=false;
            for_client.Visible = false;
            for_other1.Visible = false;
            desc.Visible = false;
            div_invoice_list.Visible = false;
            account_link_details.Visible=false;
           
        }

    }
    //vikas
    protected void client_code()
    {

        // insert();
        ddl_minibank_client.Items.Clear();
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

                ddl_minibank_client.DataSource = dt_item;
                ddl_minibank_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_minibank_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_minibank_client.DataBind();

                ddl_pmt_client.DataSource = dt_item;
                ddl_pmt_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_pmt_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_pmt_client.DataBind();

                ddl_upload_lg_client.DataSource = dt_item;
                ddl_upload_lg_client.DataTextField = dt_item.Columns[0].ToString();
                ddl_upload_lg_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_upload_lg_client.DataBind();

            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client.Items.Insert(0, "Select");
            ddl_minibank_client.Items.Insert(0, "Select");
            ddl_pmt_client.Items.Insert(0, "Select");
            ddl_upload_lg_client.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }



    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
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
        //e.Row.Cells[7].Visible = false;
    }
    //vikas
    //private void load_grdview()
    //{
    //    gv_fullmonthot.Visible = false;
    //    d.con1.Open();
    //    try
    //    {
    //        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,comp_code,client_code,unit_code,month,year,(SELECT emp_name FROM pay_employee_master WHERE uploaded_by = pay_employee_master.emp_code) AS uploaded_by, uploaded_date,description,concat('~/Attendance_Images/',file_name) as Value FROM pay_files_timesheet where comp_Code = '" + Session["COMP_CODE"].ToString() + "' ", d.con1);

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
    double final_double_amount;
    protected int result = 0;
    protected double billing_amount = 0, recived_amt = 0;


    protected void btn_close_click(object sender, object e)
    {
        Response.Redirect("Home.aspx");
    }
    //Receipt details

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //receving_amount.Visible = false;
        //load_client_amount();
        div_invoice_list.Visible = true;
        try
        {
           
            txt_total_invoice.Text = "";
            txt_deducted.Text = "";
            DataSet ds = new DataSet();
            gv_invoice_list.DataSource = null;
            gv_invoice_list.DataBind();
           
            //ds = d.select_data("SELECT payment_history.Invoice_No AS 'Invoice_no', CONCAT(payment_history.month, '/', payment_history.year) AS 'bill_month', ROUND(payment_history.billing_amt) as 'billing_amt',(ROUND(payment_history.billing_amt) - IFNULL(ROUND(SUM(pay_report_gst.received_amt + tds_amount)), 0)) AS 'Balanced Amount',payment_history.Id FROM payment_history  LEFT JOIN pay_report_gst ON payment_history.Invoice_No = pay_report_gst.Invoice_No WHERE payment_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and  payment_history.client_code = '" + ddl_client.SelectedValue + "' AND payment_history.invoice_flag = 2 and payment_history.invoice_no  not in (SELECT Invoice_No FROM (SELECT payment_history.Invoice_No, CASE adjustment_sign != '' WHEN adjustment_sign = 1 THEN ROUND(IFNULL((payment_history.billing_amt - SUM(pay_report_gst.received_amt + tds_amount + adjustment_amt)), 0), 2) WHEN adjustment_sign = 2 THEN ROUND(IFNULL((payment_history.billing_amt - SUM(pay_report_gst.received_amt + tds_amount - adjustment_amt)), 0), 2) ELSE payment_history.billing_amt END AS 'Balanced_Amount' FROM payment_history LEFT JOIN pay_report_gst ON payment_history.Invoice_No = pay_report_gst.Invoice_No AND payment_history.client_code = pay_report_gst.client_code AND payment_history.month = pay_report_gst.month AND payment_history.year = pay_report_gst.year WHERE payment_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' and payment_history.client_code = '" + ddl_client.SelectedValue + "' AND payment_history.invoice_flag = 2 GROUP BY payment_history.Invoice_No) AS t1 WHERE t1.Balanced_Amount <= 0) ");
           //chaitali commented query for all type invoice
            //ds = d.select_data("SELECT Invoice_No,amount ,month, year,Balanced_Amount FROM((SELECT pay_report_gst.Invoice_No,pay_report_gst.amount,pay_report_gst.month, pay_report_gst.year,CASE adjustment_sign != '' WHEN adjustment_sign = 1 THEN ROUND(IFNULL((pay_report_gst.amount - SUM(pay_report_gst.received_amt + tds_amount + adjustment_amt)), 0), 2) WHEN adjustment_sign = 2 THEN ROUND(IFNULL((pay_report_gst.amount - SUM(pay_report_gst.received_amt + tds_amount - adjustment_amt)), 0), 2) ELSE pay_report_gst.amount END AS 'Balanced_Amount' FROM pay_report_gst  LEFT JOIN pay_report_gst ON pay_report_gst.Invoice_No = pay_report_gst.Invoice_No AND pay_report_gst.client_code = pay_report_gst.client_code AND pay_report_gst.month = pay_report_gst.month AND pay_report_gst.year = pay_report_gst.year INNER JOIN `pay_billing_unit_rate_history` ON `pay_report_gst`.`Invoice_No` = `pay_billing_unit_rate_history`.`Invoice_No` AND `pay_report_gst`.`client_code` = `pay_billing_unit_rate_history`.`client_code` WHERE pay_report_gst.comp_code='" + Session["comp_code"] + "' and pay_report_gst.client_code = '" + ddl_client.SelectedValue + "' AND invoice_flag = 2  GROUP BY pay_report_gst.Invoice_No) AS t1) WHERE t1.Balanced_Amount != 0 &&  `t1`.`Balanced_Amount` > 0");
            d1.con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_minibank_master`.`ID`, ROUND(IFNULL(SUM(`pay_report_gst`.`received_amt`), 0), 2) AS ' SETTLED_AMOUNT', ROUND(pay_minibank_master.`Amount` - (IFNULL(SUM(`pay_report_gst`.`received_amt`), 0)), 2) AS 'REMANING_AMOUNT' FROM `pay_minibank_master`  LEFT JOIN `pay_report_gst` ON `pay_report_gst`.`payment_id` = `pay_minibank_master`.`id` WHERE `pay_minibank_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' and pay_minibank_master.receive_date=str_to_date('" + txt_date.Text + "','%d-%m-%Y') and pay_minibank_master.client_code='" + ddl_client.SelectedValue + "' and pay_minibank_master.amount='" + ddl_client_resive_amt.SelectedItem + "'", d1.con1);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txt_total_invoice.Text = dr.GetValue(1).ToString();
                txt_deducted.Text = dr.GetValue(2).ToString();
                d1.con1.Close();
            }



            ds = d.select_data("SELECT Invoice_No,ROUND(amount, 2) AS 'amount',month,year,ROUND(Balanced_Amount, 2) AS 'Balanced_Amount', type FROM((SELECT pay_report_gst . Invoice_No ,( amount +  cgst +  sgst +  igst ) AS 'amount',pay_report_gst . month ,pay_report_gst . year , CASE  pay_report_gst.adjustment_sign  = '0' WHEN  pay_report_gst.adjustment_sign  = 1  THEN ROUND(IFNULL((( amount +  cgst +  sgst +  igst )  - SUM( pay_report_gst . received_amt  +  pay_report_gst.tds_amount  +  pay_report_gst.adjustment_amt )), 0), 2) WHEN  pay_report_gst.adjustment_sign  = 2 THEN ROUND(IFNULL((( amount +  cgst +  sgst +  igst ) - SUM( pay_report_gst . received_amt  +  pay_report_gst.tds_amount  -  pay_report_gst.adjustment_amt )), 0), 2)   ELSE round((sum( amount +  cgst +  sgst +  igst ) - sum(pay_report_gst.received_amt + pay_report_gst.tds_amount)),2) END AS 'Balanced_Amount', type  FROM pay_report_gst  WHERE pay_report_gst . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  pay_report_gst . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_report_gst . flag_invoice  = 2 GROUP BY pay_report_gst . Invoice_No ) AS t1)WHERE t1 . Balanced_Amount  != 0 &&  t1 . Balanced_Amount  > 0.99 ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_invoice_list.DataSource = ds;
                gv_invoice_list.DataBind();
                Panel6.Visible = true;
                // panel2.Visible = false;
              

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('No Matching Records Found !!!')", true);
                gv_invoice_list.DataSource = null;
                gv_invoice_list.DataBind();
                btn_process.Visible = false;
            }
            ds.Dispose();
           
        }
        catch (Exception ex) { throw ex; }
        finally
        { 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            Panel_gv_pmt.Visible = false;
        }

    }
    protected void gv_payment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        string invoice_no = gv_payment.SelectedRow.Cells[7].Text;


        try
        {
            payment_details(invoice_no);
            // panel_payment_detail.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); 
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);       
        }

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
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["receipt_de_approve"].ToString() != "0" && dr["receipt_de_approve"].ToString() != "3")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;


                //  e.Row.Cells[14].Visible = false;
                // e.Row.Cells[15].Visible = false;

                LinkButton lb1 = e.Row.FindControl("lnk_remove_manual_other") as LinkButton;
                lb1.Visible = false;


            }
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_payment, "Select$" + e.Row.RowIndex);

        }
       // e.Row.Cells[0].Visible = false;


        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[10].Visible = false;
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gv_links.DataSource = null;
            gv_links.DataBind();
            if (ddl_client.SelectedValue != "Select")
            {
                account_link_details.Visible = true;
                gv_links.DataSource = null;
                gv_links.DataBind();
                load_gv_payment("and payment_history.client_code = '" + ddl_client.SelectedValue + "'");
                DataTable dt_item = new DataTable();
                //ddl_client_resive_amt.Items.Clear();
                //MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct DATE_FORMAT(`receive_date`, '%d-%m-%Y') FROM pay_minibank_master   LEFT OUTER JOIN pay_report_gst ON pay_minibank_master.comp_code = pay_report_gst.comp_code AND pay_minibank_master.client_code = pay_report_gst.client_code WHERE pay_minibank_master.client_code='" + ddl_client.SelectedValue + "' ", d.con);
                MySqlDataAdapter cmd_item = new MySqlDataAdapter("select   DATE_FORMAT(`receive_date`, '%d-%m-%Y') from(SELECT pay_minibank_master.ID,receive_date, ROUND(pay_minibank_master.Amount - (IFNULL(SUM(pay_report_gst.received_amt), 0)), 2) AS 'REMANING_AMOUNT' FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_report_gst.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_minibank_master.client_code='" + ddl_client.SelectedValue + "' AND `receipt_approve` != '0'  GROUP BY pay_minibank_master.receive_date) as t1 where REMANING_AMOUNT >0.99 ", d.con);
                d.con.Open();

                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    txt_date.DataSource = dt_item;

                    txt_date.DataValueField = dt_item.Columns[0].ToString();

                    txt_date.DataBind();
                }
                txt_date.Items.Insert(0, "Select");
                dt_item.Dispose();
                d.con.Close();
                display_close_date();
            
                // load client date wise amount
               


            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }

    }
    protected void load_client_amount()
    {

        try
        {
            DataTable dt_item = new DataTable();
            ddl_client_resive_amt.Items.Clear();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select Id,amount from ( SELECT pay_minibank_master.id AS 'Id', ROUND((Amount - IFNULL(SUM(received_amt), 0)), 2) AS 'amount'   FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_minibank_master.id = pay_report_gst.payment_id AND pay_minibank_master.CLIENT_CODE = pay_report_gst.CLIENT_CODE WHERE pay_minibank_master.receive_date = date_format('" + txt_date.Text + "', '%Y-%m-%d') AND pay_minibank_master.client_code = '" + ddl_client.SelectedValue + "' GROUP BY pay_minibank_master.id, pay_report_gst.payment_id)  as t1 where  amount > 0 ORDER BY amount  ", d.con);
            d.con.Open();

            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_resive_amt.DataSource = dt_item;

                ddl_client_resive_amt.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_resive_amt.DataTextField = dt_item.Columns[1].ToString();
                ddl_client_resive_amt.DataBind();
            }
            ddl_client_resive_amt.Items.Insert(0, "Select");
            dt_item.Dispose();
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
    //protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_state.SelectedValue != "ALL")
    //    {
    //        ddl_branch.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "' ORDER BY UNIT_CODE", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_branch.DataSource = dt_item;
    //                ddl_branch.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_branch.DataValueField = dt_item.Columns[1].ToString();
    //                ddl_branch.DataBind();
    //            }
    //            ddl_branch.Items.Insert(0, "ALL");
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

    protected void payment_details(string invoice_no)
    {
        head_transction.Visible = true;
        d.con1.Open();
        try
        {
            gv_payment_detail.DataSource = null;
            gv_payment_detail.DataBind();
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("select ID,Invoice_No as 'Invoice No',   `receipt_de_approve`,CASE  WHEN `receipt_de_approve` = '0' THEN 'Pending'  WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc'   WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc'  WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc'  END AS 'Status' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE pay_client_master.client_code = pay_report_gst.client_code AND pay_client_master.comp_code = pay_report_gst.comp_code) AS 'Client Name',Round(billing_amt,2) as 'Bill Amount',Round(received_amt,2) as 'Received Amount',Round(tds_amount,2) as 'TDS Amount' , CASE  WHEN (adjustment_sign = 1 && adjustment_amt > 0) THEN CONCAT('+', ROUND(adjustment_amt, 2)) WHEN (adjustment_sign = 2 && adjustment_amt > 0) THEN CONCAT('-', ROUND(adjustment_amt, 2)) ELSE ROUND(adjustment_amt, 2) END AS 'Adj Amt',date_format(received_date,'%d/%m/%Y') as 'Received Date',receipt_de_reasons as 'Reject_Reason' from pay_report_gst where comp_code = '" + Session["COMP_CODE"].ToString() + "' and Invoice_No='" + invoice_no + "'  order by Id", d.con1);
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
        //txt_invoice_no.Text = "";
        //ViewState["client_code"] = "";
        //ViewState["state_name"] = "";
        //ViewState["unit_code"] = "";
        //ViewState["taxable_amount"] = 0;
        //gv_payment_detail.DataSource = null;
        //gv_payment_detail.DataBind();

        //txt_bill_amount.Text = "";
        //txt_receive_amount.Text = "0";
        //txt_receive_date.Text = "";
        //ddl_tds_amount.SelectedIndex = 0;
        //txt_tds_amt.Text = "0";
        //ddl_adjustment.SelectedIndex = 0;
        //txt_adment_amt.Text = "0";
    }
    protected void btn_process_Click(object sender, EventArgs e)
    {
        string invoice_list = "";
        string b_amt = "";
        div_invoice_list.Visible = false;
        try
        {
            double sum = 0;
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            foreach (GridViewRow gvrow in gv_invoice_list.Rows)
            {
                string Invoice_no = (string)gv_invoice_list.DataKeys[gvrow.RowIndex].Value;
                //string Balanced_amount = gv_invoice_list.Rows[gvrow.RowIndex].Cells[6].Text;
                var checkbox = gvrow.FindControl("chk_invoice") as CheckBox;

                if (checkbox.Checked == true)
                {
                    invoice_list = invoice_list + "'" + Invoice_no + "',";
                   // b_amt = b_amt + "'" + Balanced_amount + "'";
                    
                }
            }
            if (invoice_list == "") { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select Invoice !!!')", true); return; }
            if (invoice_list.Length > 0)
            {
                invoice_list = invoice_list.Substring(0, invoice_list.Length - 1);
            }
            else { invoice_list = "''"; }

         
            d1.con1.Open();

            //MySqlCommand cmd = new MySqlCommand("SELECT sum(billing_amt),((sum(billing_amt))-('" + ddl_client_resive_amt.SelectedItem.Text + "')) as deducted FROM payment_history WHERE payment_history.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND payment_history.client_code = '" + ddl_client.SelectedValue + "' and invoice_no in(" + invoice_list + ")", d1.con1);
          // MySqlCommand cmd = new MySqlCommand("SELECT  billing_amt,if(deducted >=0,deducted,0) as 'deducted' FROM (SELECT sum(billing_amt)as 'billing_amt',(('" + ddl_client_resive_amt.SelectedItem.Text + "')-(SUM(`payment_history`.`billing_amt`)) ) AS 'deducted' FROM `payment_history`  WHERE payment_history.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND payment_history.client_code = '" + ddl_client.SelectedValue + "' and invoice_no in(" + invoice_list + ")) AS t1", d1.con1);
           // MySqlCommand cmd = new MySqlCommand("SELECT  sum(balanced_amount), (('" + ddl_client_resive_amt.SelectedItem.Text + "') - sum( balanced_amount)) AS 'deducted' FROM (SELECT (`payment_history`.`billing_amt` - (`pay_report_gst`.`received_amt` + `tds_amount`)) AS 'balanced_amount' FROM payment_history payment_history LEFT JOIN `pay_report_gst` ON `payment_history`.`Invoice_No` = `pay_report_gst`.`Invoice_No` AND `payment_history`.`client_code` = `pay_report_gst`.`client_code`  WHERE payment_history.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND payment_history.client_code = '" + ddl_client.SelectedValue + "' and payment_history.invoice_no in(" + invoice_list + ") GROUP BY `payment_history`.`Invoice_No`) AS t1", d1.con1);

           // d1.con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_minibank_master`.`ID`, ROUND(IFNULL(SUM(`pay_report_gst`.`received_amt`), 0), 2) AS ' SETTLED_AMOUNT', ROUND(pay_minibank_master.`Amount` - (IFNULL(SUM(`pay_report_gst`.`received_amt`), 0)), 2) AS 'REMANING_AMOUNT' FROM `pay_minibank_master`  LEFT JOIN `pay_report_gst` ON `pay_report_gst`.`payment_id` = `pay_minibank_master`.`id` WHERE `pay_minibank_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' and pay_minibank_master.receive_date=str_to_date('" + txt_date.Text + "','%d-%m-%Y') and pay_minibank_master.client_code='" + ddl_client.SelectedValue + "' and pay_minibank_master.amount='" + ddl_client_resive_amt.SelectedItem + "'", d1.con1);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txt_total_invoice.Text = dr.GetValue(1).ToString();
                txt_deducted.Text = dr.GetValue(2).ToString();
                d1.con1.Close();
            }

            DataSet ds = new DataSet();
            gv_invoice_pmt.DataSource = null;
            gv_invoice_pmt.DataBind();

            gv_invoice_pmt.Columns[5].HeaderText = d.getsinglestring("SELECT CASE  WHEN tds_applicable = 1 AND tds_percentage = 1 AND tds_on = 1 THEN 'TDS 1% ON TAXABLE AMOUNT' WHEN tds_applicable = 1 AND tds_percentage = 1 AND tds_on = 2 THEN 'TDS 1% ON BILLING AMOUNT' WHEN tds_applicable = 1 AND tds_percentage = 2 AND tds_on = 1 THEN 'TDS 2% ON TAXABLE AMOUNT' WHEN tds_applicable = 1 AND tds_percentage = 2 AND tds_on = 2 THEN 'TDS 2% ON BILLING AMOUNT'  WHEN `tds_applicable` = 1 AND `tds_percentage` = 0.75 AND `tds_on` = 1 THEN 'TDS 0.75% ON BILLING AMOUNT' WHEN `tds_applicable` = 1 AND `tds_percentage` = 0.75 AND `tds_on` = 2 THEN 'TDS 0.75% ON TAXABLE AMOUNT' WHEN `tds_applicable` = 1 AND `tds_percentage` = 1.5 AND `tds_on` = 1 THEN 'TDS 1.5% ON  BILLING AMOUNT' WHEN `tds_applicable` = 1 AND `tds_percentage` = 1.5 AND `tds_on` = 2 THEN 'TDS 1.5% ON TAXABLE AMOUNT'  ELSE 'TDS NOT APPLICABLE' END AS 'TDS STATUS' FROM pay_client_master WHERE comp_Code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "'");
            //chaitali comment for all type invoice
            //ds = d.select_data("select Invoice_no,billing_amt,(billing_amt - tds_amt ) as receving_amt,receving_date,tds_amt,adj_amt,tds,tds_on,adjustment_sign from(SELECT payment_history.Invoice_No AS 'Invoice_no', (payment_history.billing_amt) AS 'billing_amt',0 as 'receving_amt','" + txt_date.Text + "' as receving_date,  CASE WHEN `pay_report_gst`.`tds_amount` != '' THEN `pay_report_gst`.`tds_amount` = '0' WHEN tds_applicable = 1 AND pay_client_master.tds_on = 1 THEN ROUND(((payment_history.taxable_amount * tds_percentage) / 100), 2) WHEN tds_applicable = 1 AND pay_client_master.tds_on = 2 THEN ROUND(((payment_history.billing_amt * tds_percentage) / 100), 2) ELSE 0 END AS 'tds_amt', 0 as 'adj_amt','Amount' AS 'tds',0 AS 'tds_on',0 AS 'adjustment_sign'  FROM payment_history INNER JOIN pay_client_master ON payment_history.comp_code = pay_client_master.comp_Code AND payment_history.client_code = pay_client_master.client_code LEFT JOIN pay_report_gst ON payment_history.Invoice_No = pay_report_gst.Invoice_No AND payment_history.client_code = pay_report_gst.client_code AND payment_history.month = pay_report_gst.month AND payment_history.year = pay_report_gst.year WHERE payment_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND payment_history.client_code = '" + ddl_client.SelectedValue + "' AND payment_history.Invoice_No in (" + invoice_list + ") AND payment_history.invoice_flag = 2 GROUP BY payment_history.Invoice_No, payment_history.client_code ORDER BY payment_history.Id) as t1");
            //ds = d.select_data("SELECT payment_history.Invoice_No AS 'Invoice_no', ROUND(payment_history.billing_amt) AS 'billing_amt',0 as 'receving_amt','" + txt_date.Text + "' as receving_date, CASE WHEN `pay_report_gst`.`tds_amount` != '' THEN `pay_report_gst`.`tds_amount` = '0' WHEN `pay_report_gst`.`tds_amount` != '' THEN `pay_report_gst`.`tds_amount` = '0' WHEN `pay_report_gst`.`tds_amount` = '' THEN (CASE WHEN `tds_applicable` = 1 AND `pay_client_master`.`tds_on` = 1 THEN ROUND(((`payment_history`.`taxable_amount` * `tds_percentage`) / 100), 2) WHEN `tds_applicable` = 1 AND `pay_client_master`.`tds_on` = 2 THEN ROUND(((`payment_history`.`billing_amt` * `tds_percentage`) / 100), 2) END) END AS 'tds_amt', 0 as 'adj_amt','Amount' AS 'tds',0 AS 'tds_on',0 AS 'adjustment_sign'  FROM payment_history INNER JOIN pay_client_master ON payment_history.comp_code = pay_client_master.comp_Code AND payment_history.client_code = pay_client_master.client_code LEFT JOIN pay_report_gst ON payment_history.Invoice_No = pay_report_gst.Invoice_No AND payment_history.client_code = pay_report_gst.client_code AND payment_history.month = pay_report_gst.month AND payment_history.year = pay_report_gst.year WHERE payment_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND payment_history.client_code = '" + ddl_client.SelectedValue + "' AND payment_history.Invoice_No in (" + invoice_list + ") AND payment_history.invoice_flag = 2 GROUP BY payment_history.Invoice_No, payment_history.client_code ORDER BY payment_history.Id");

            ds = d.select_data("SELECT Invoice_no ,ROUND(amount, 2) AS 'billing_amt',ROUND((`amount` - `tds_amt`), 2) AS 'receving_amt',`receving_date`, ROUND(`tds_amt`,2) AS 'tds_amt' ,ROUND(`adj_amt`,2) AS 'adj_amt',tds ,tds_on , adjustment_sign FROM(SELECT pay_report_gst . Invoice_No  AS 'Invoice_no',( amount +  cgst +  sgst +  igst ) AS 'amount', 0 AS 'receving_amt', '" + txt_date.Text + "' AS 'receving_date',CASE WHEN  pay_report_gst . tds_amount  != '' THEN  pay_report_gst . tds_amount  = '0' WHEN  tds_applicable  = 1 AND  pay_client_master . tds_on  = 1  THEN ROUND(((( amount  + cgst + sgst+ igst ) *  tds_percentage ) / 100), 2)WHEN  tds_applicable  = 1 AND  pay_client_master . tds_on  = 2 THEN ROUND((((amount) *  tds_percentage ) / 100), 2)   ELSE 0 END AS 'tds_amt',0 AS 'adj_amt','Amount' AS 'tds', 0 AS 'tds_on', 0 AS 'adjustment_sign' FROM  pay_report_gst  INNER JOIN  pay_client_master  ON  pay_report_gst . comp_code  =  pay_client_master . comp_Code  AND  pay_report_gst . client_code  =  pay_client_master . client_code   WHERE pay_report_gst . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  pay_report_gst . client_code  = '" + ddl_client.SelectedValue + "' AND  pay_report_gst .Invoice_No in (" + invoice_list + ") AND  pay_report_gst . flag_invoice  = 2 GROUP BY pay_report_gst . Invoice_No ,  pay_report_gst . client_code ORDER BY  pay_report_gst . Id ) AS t1");
            gv_invoice_pmt.DataSource = ds;
            gv_invoice_pmt.DataBind();

            Panel6.Visible = false;
            Panel_gv_pmt.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }

    }
    protected void gv_invoice_pmt_RowDataBound(object sender, GridViewRowEventArgs e)
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
            DropDownList ddl_tds_amount = e.Row.FindControl("ddl_tds_amount") as DropDownList;
            DropDownList ddl_tds_on = e.Row.FindControl("ddl_tds_on") as DropDownList;
            DropDownList ddl_adjustment = e.Row.FindControl("ddl_adjustment") as DropDownList;

        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //try { ScriptManager.RegisterStartupScript(this, this.GetType(), "callmyfunction", "unblock()", true); }
        //catch { }
        string invoice_list = null;
        if (check_validation("Insert"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callmyfunction", "unblock()", true);

            return;
        }
        d.con1.Open();
        MySqlTransaction mtran = d.con1.BeginTransaction();
        try
        {

            int result = 0;
            double t_amt = 0;
            foreach (GridViewRow row in gv_invoice_pmt.Rows)
            {
                
                if (row != null)
                {
                    double txt_amount = double.Parse(((TextBox)row.FindControl("txt_recive_amt")).Text);
                    t_amt = (t_amt + txt_amount);
                }
                if (t_amt > Convert.ToDouble(ddl_client_resive_amt.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Receiveing  Amount is Not Greater Than Receipt Amount !!!')", true);
                    return;
                }
                
            }
            foreach (GridViewRow row in gv_invoice_pmt.Rows)
            {
                if (row != null)
                {

                    result = 0;
                    string invoice_no = row.Cells[1].Text;

                    double txt_bill_amount = double.Parse(row.Cells[2].Text);

                    double txt_receive_amount = double.Parse(((TextBox)row.FindControl("txt_recive_amt")).Text);

                    string txt_reciving_date = ((TextBox)row.FindControl("txt_reciving_date")).Text;

                    //DropDownList ddl_tds_amount = (DropDownList)row.FindControl("ddl_tds_amount");

                    //DropDownList ddl_tds_on = (DropDownList)row.FindControl("ddl_tds_on");

                    double txt_tds_amt = double.Parse(((TextBox)row.FindControl("txt_tds_amt")).Text);

                    int adj_selectedvalue = int.Parse(((DropDownList)row.FindControl("ddl_adjustment")).SelectedValue);

                    double txt_adjustment_amt = double.Parse(((TextBox)row.FindControl("txt_adjustment_amt")).Text);



                    int month = int.Parse(return_fields1("month", invoice_no));
                    int year = int.Parse(return_fields1("year", invoice_no));

                    double tds_amount = 0;

                    tds_amount = txt_tds_amt;
                    //tds amount
                    //if (ddl_tds_amount.SelectedValue != "Amount")
                    //{
                    //    int tds_persent = int.Parse(ddl_tds_amount.SelectedValue);
                    //    tds_amount = ddl_tds_on.SelectedValue == "0" ? (double.Parse(return_fields("taxable_amount", invoice_no)) * tds_persent) / 100 : (txt_bill_amount * tds_persent) / 100;
                    //}
                    //else
                    //{

                    //}

                    double received_amt = double.Parse(d.getsinglestring("select ifnull(sum(received_amt + tds_amount), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "'  order by id"));

                    //adjustment amount
                    string adj_sign = "";

                    if (adj_selectedvalue == 1) { received_amt = received_amt + txt_adjustment_amt; }

                    if (adj_selectedvalue == 2) { received_amt = received_amt - txt_adjustment_amt; }

                    string matching = String.Format("{0:0.00}", (received_amt + txt_receive_amount + tds_amount));
                    Double balance_amt = Convert.ToDouble(matching);

                    if (txt_bill_amount >= (balance_amt))
                    {
                        try
                        {
                            string query = null,temp="";
                            //   query = "insert into pay_report_gst (comp_code,invoice_no,client_code,state,unit_code,billing_amt,received_amt,tds,tds_on,tds_amount,adjustment_amt,adjustment_sign,received_date,month,year,total_received_amt,payment_id,uploaded_by,uploaded_date) values('" + Session["comp_code"].ToString() + "','" + invoice_no + "','" + return_fields("client_code", invoice_no) + "','" + return_fields("state_name", invoice_no) + "','" + return_fields("unit_code", invoice_no) + "','" + txt_bill_amount + "','" + txt_receive_amount + "','" + ddl_tds_amount.SelectedValue + "','" + ddl_tds_on.SelectedValue + "','" + tds_amount + "','" + txt_adjustment_amt + "','" + adj_selectedvalue + "',str_to_date('" + txt_reciving_date + "','%d/%m/%Y'),'" + month + "','" + year + "','" + (txt_receive_amount + tds_amount) + "','" + ddl_client_resive_amt.SelectedValue + "','" + Session["login_id"].ToString() + "',now())";
                           // query = "insert into pay_report_gst (comp_code,invoice_no,client_code,state,unit_code,billing_amt,received_amt,tds_amount,adjustment_amt,adjustment_sign,received_date,month,year,total_received_amt,payment_id,uploaded_by,uploaded_date) values('" + Session["comp_code"].ToString() + "','" + invoice_no + "','" + return_fields1("client_code", invoice_no) + "','" + return_fields1("state_name", invoice_no) + "','" + return_fields1("unit_code", invoice_no) + "','" + txt_bill_amount + "','" + txt_receive_amount + "','" + tds_amount + "','" + txt_adjustment_amt + "','" + adj_selectedvalue + "',str_to_date('" + txt_reciving_date + "','%d-%m-%Y'),'" + month + "','" + year + "','" + (received_amt + txt_receive_amount + tds_amount) + "','" + ddl_client_resive_amt.SelectedValue + "','" + Session["login_id"].ToString() + "',now())";
                            temp = d.getsinglestring("select received_date from pay_report_gst where invoice_no = '" + invoice_no + "'");
                            if (temp == "")
                            {
                                query = " update pay_report_gst set `received_original_amount`='" + ddl_client_resive_amt.SelectedItem + "', billing_amt='" + txt_bill_amount + "', received_amt ='" + txt_receive_amount + "',tds_amount ='" + tds_amount + "',adjustment_amt ='" + txt_adjustment_amt + "',adjustment_sign='" + adj_selectedvalue + "',received_date= str_to_date('" + txt_reciving_date + "','%d-%m-%Y'),total_received_amt='" + (received_amt + txt_receive_amount + tds_amount) + "',payment_id='" + ddl_client_resive_amt.SelectedValue + "',uploaded_by='" + Session["login_id"].ToString() + "',uploaded_date=now() where invoice_no = '" + invoice_no + "'";
                            }
                            else
                            {
                                query = "insert into pay_report_gst (received_original_amount,comp_code,invoice_no,client_code,state_name,unit_code,billing_amt,received_amt,tds_amount,adjustment_amt,adjustment_sign,received_date,month,year,total_received_amt,payment_id,uploaded_by,uploaded_date,flag_invoice) values('" + ddl_client_resive_amt.SelectedItem + "','" + Session["comp_code"].ToString() + "','" + invoice_no + "','" + return_fields1("client_code", invoice_no) + "','" + return_fields1("state_name", invoice_no) + "','" + return_fields1("unit_code", invoice_no) + "','" + txt_bill_amount + "','" + txt_receive_amount + "','" + tds_amount + "','" + txt_adjustment_amt + "','" + adj_selectedvalue + "',str_to_date('" + txt_reciving_date + "','%d-%m-%Y'),'" + month + "','" + year + "','" + (received_amt + txt_receive_amount + tds_amount) + "','" + ddl_client_resive_amt.SelectedValue + "','" + Session["login_id"].ToString() + "',now(),'2')";
                            }
                              MySqlCommand cmd = new MySqlCommand(query, d.con1);
                            result = 1;
                            cmd.ExecuteNonQuery();

                            invoice_list = invoice_list + "'" + invoice_no + "',";

                        }
                        catch (Exception ex)
                        {
                            mtran.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "callmyfunction", "unblock()", true);
                        
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount exceed Billing Amount !!!')", true);
                        result = 0;
                        mtran.Rollback();
                        Panel_gv_pmt.Visible = true;
                        return;
                    }

                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Saved Successfully Please Approve Record !!!')", true);
                mtran.Commit();
                d.con1.Close();
                //tran_clear();
                ////panel2.Visible = true;
                //Panel_gv_pmt.Visible = false;

                invoice_list = invoice_list.Length > 0 ? invoice_list.Substring(0, invoice_list.Length - 1) : "''";
                load_gv_payment("and payment_history.invoice_no in (" + invoice_list + ")");

            }

        }
        catch (Exception ex)
        {
            mtran.Rollback();
            throw ex;
        }
        finally
        {
            d.con1.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callmyfunction", "unblock()", true);

        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        //try { ScriptManager.RegisterStartupScript(this, this.GetType(), "callmyfunction", "unblock()", true); }
        //catch { }
        string invoice_list = null;
        if (check_validation("Update"))
        {

            return;
        }
        d.con1.Open();
        MySqlTransaction mtran = d.con1.BeginTransaction();
        try
        {

            int result = 0;

            foreach (GridViewRow row in gv_invoice_pmt.Rows)
            {
                if (row != null)
                {

                    result = 0;
                    string invoice_no = row.Cells[1].Text;

                    double txt_bill_amount = double.Parse(row.Cells[2].Text);

                    double txt_receive_amount = double.Parse(((TextBox)row.FindControl("txt_recive_amt")).Text);

                    string txt_reciving_date = ((TextBox)row.FindControl("txt_reciving_date")).Text;

                    DropDownList ddl_tds_amount = (DropDownList)row.FindControl("ddl_tds_amount");

                    DropDownList ddl_tds_on = (DropDownList)row.FindControl("ddl_tds_on");

                    double txt_tds_amt = double.Parse(((TextBox)row.FindControl("txt_tds_amt")).Text);

                    int adj_selectedvalue = int.Parse(((DropDownList)row.FindControl("ddl_adjustment")).SelectedValue);

                    double txt_adjustment_amt = double.Parse(((TextBox)row.FindControl("txt_adjustment_amt")).Text);



                    int month = int.Parse(return_fields1("month", invoice_no));
                    int year = int.Parse(return_fields1("year", invoice_no));

                    double tds_amount = 0;


                    //tds amount
                    if (ddl_tds_amount.SelectedValue != "Amount")
                    {
                        int tds_persent = int.Parse(ddl_tds_amount.SelectedValue);
                        tds_amount = ddl_tds_on.SelectedValue == "0" ? (double.Parse(return_fields1("taxable_amount", invoice_no)) * tds_persent) / 100 : (txt_bill_amount * tds_persent) / 100;
                    }
                    else
                    {
                        tds_amount = txt_tds_amt;
                    }


                    double received_amt = double.Parse(d.getsinglestring("select ifnull(Round(sum(total_received_amt),2), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "' and id != '" + ViewState["row_id"] + "'  order by id"));

                    //adjustment amount
                    string adj_sign = "";

                    if (adj_selectedvalue == 1) { received_amt = received_amt + txt_adjustment_amt; }

                    if (adj_selectedvalue == 2) { received_amt = received_amt - txt_adjustment_amt; }

                    if (txt_bill_amount >= (received_amt + txt_receive_amount + tds_amount))
                    {
                        try
                        {
                            string query = null;
                            query = "update pay_report_gst set received_amt = '" + txt_receive_amount + "', tds = '" + ddl_tds_amount.SelectedValue + "',tds_on =  '" + ddl_tds_on.SelectedValue + "',tds_amount = '" + tds_amount + "',adjustment_amt = '" + txt_adjustment_amt + "',total_received_amt = '" + (txt_receive_amount + tds_amount) + "',adjustment_sign = '" + adj_selectedvalue + "' ,uploaded_by = '" + Session["login_id"].ToString() + "',uploaded_date = now() where comp_code =  '" + Session["comp_code"].ToString() + "' and Id = '" + ViewState["row_id"] + "'";
                            MySqlCommand cmd = new MySqlCommand(query, d.con1);
                            result = 1;
                            cmd.ExecuteNonQuery();

                            invoice_list = invoice_list + "'" + invoice_no + "',";

                        }
                        catch (Exception ex)
                        {
                            mtran.Rollback();
                            throw ex;
                        }
                        finally
                        { }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount exceed Billing Amount !!!')", true);
                        result = 0;
                        mtran.Rollback();
                        Panel_gv_pmt.Visible = true;
                        return;
                    }

                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Updated Successfully !!!')", true);
                mtran.Commit();
                d.con1.Close();
                tran_clear();
                //panel2.Visible = true;
                Panel_gv_pmt.Visible = false;

                invoice_list = invoice_list.Length > 0 ? invoice_list.Substring(0, invoice_list.Length - 1) : "''";
                load_gv_payment("and payment_history.invoice_no in (" + invoice_list + ")");

            }

        }
        catch (Exception ex)
        {
            mtran.Rollback();
            throw ex;
        }
        finally
        {
            d.con1.Close();
        }
    }
    protected string return_fields(string str, string invoice_no)
    {

        return d.getsinglestring("select ifnull(" + str + ",0) from payment_history where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "'");
    }
    // from pay_report_gst all type invoice
    protected string return_fields1(string str, string invoice_no)
    {

        return d.getsinglestring("select ifnull(" + str + ",0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "'");
    }

    protected void btn_pmt_close_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void tran_clear()
    {

        foreach (GridViewRow row in gv_invoice_pmt.Rows)
        {
            if (row != null)
            {


                row.Cells[1].Text = "";

                row.Cells[2].Text = "";

                ((TextBox)row.FindControl("txt_recive_amt")).Text = "0";
                ((TextBox)row.FindControl("txt_reciving_date")).Text = "";

                //((DropDownList)row.FindControl("ddl_tds_amount")).SelectedValue = "Amount";
                //((DropDownList)row.FindControl("ddl_tds_on")).SelectedValue = "0";

                ((TextBox)row.FindControl("txt_tds_amt")).Text = "0";

                ((DropDownList)row.FindControl("ddl_adjustment")).SelectedValue = "0";

                ((TextBox)row.FindControl("txt_adjustment_amt")).Text = "0";
            }
        }

    }

    protected void gv_payment_detail_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        ViewState["row_id"] = gv_payment_detail.SelectedRow.Cells[0].Text;


        try
        {
            DataSet ds = new DataSet();
            gv_invoice_pmt.DataSource = null;
            gv_invoice_pmt.DataBind();
            ds = d.select_data("SELECT pay_report_gst.Invoice_No AS 'Invoice_no', ROUND(pay_report_gst.billing_amt, 2) AS 'billing_amt',received_amt as 'receving_amt', DATE_FORMAT(received_date, '%d/%m/%Y') AS 'receving_date',tds,tds_on,tds_amount as 'tds_amt',adjustment_sign,adjustment_amt as 'adj_amt',payment_id FROM pay_report_gst WHERE pay_report_gst.comp_code = '" + Session["COMP_CODE"].ToString() + "' and id = '" + ViewState["row_id"] + "'");
            gv_invoice_pmt.DataSource = ds;
            gv_invoice_pmt.DataBind();
            Panel6.Visible = false;
            Panel_gv_pmt.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
            load_client_details();
            btn_update.Visible = true;
            btn_save.Visible = false;
        }

    }
    protected void load_client_details()
    {
        try
        {
            d1.con1.Open();
            MySqlCommand cmd = new MySqlCommand("select date_format(received_date,'%d/%m/%Y'),client_code,payment_id from pay_report_gst where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id = '" + ViewState["row_id"] + "'", d1.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_date.Text = dr.GetValue(0).ToString();
                ddl_client.SelectedValue = dr.GetValue(1).ToString();
                ddl_client_SelectedIndexChanged(null, null);
                ddl_client_resive_amt.SelectedValue = dr.GetValue(2).ToString();
                d1.con1.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { d1.con1.Close(); }

    }
    protected void gv_payment_detail_RowDataBound(object sender, GridViewRowEventArgs e)
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
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["receipt_de_approve"].ToString() != "0" && dr["receipt_de_approve"].ToString() != "3")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;


                //  e.Row.Cells[14].Visible = false;
                // e.Row.Cells[15].Visible = false;

                LinkButton lb1 = e.Row.FindControl("lnk_remove_product") as LinkButton;
                lb1.Visible = false;


            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_payment_detail, "Select$" + e.Row.RowIndex);

        }
        //e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[4].Visible = false;
        //e.Row.Cells[2].Visible = false;
    }
    protected void load_gv_payment(string where)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //receving_amount.Visible = false;
        d.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = null;
            //MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT payment_history.Id, payment_history.client_code, payment_history.comp_code, DATE_FORMAT(payment_history.billing_date, '%d/%m/%Y') AS 'Bill Date', payment_history.Invoice_No AS 'Invoice No', payment_history.client_name AS 'Client Name', payment_history.state_name AS 'State', payment_history.unit_name AS 'Branch', CONCAT(payment_history.month, '/', payment_history.year) AS 'MONTH', ROUND(payment_history.taxable_amount, 2) AS 'Taxable Amount', ROUND(payment_history.GST_Amount, 2) AS 'GST', ROUND(payment_history.billing_amt) AS 'Bill Amount', IFNULL(SUM(ROUND(pay_report_gst.received_amt + tds_amount)), 0) AS 'Received Amount', pay_report_gst.tds_amount,(ROUND(payment_history.billing_amt) - IFNULL(ROUND(SUM(pay_report_gst.received_amt + tds_amount)), 0)) AS 'Balanced Amount',DATE_FORMAT( `pay_report_gst`.`received_date`, '%d/%m/%Y') AS 'Received date'  FROM payment_history  LEFT JOIN pay_report_gst ON payment_history.Invoice_No = pay_report_gst.Invoice_No  WHERE payment_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + where + " AND payment_history.invoice_flag = 2 GROUP BY payment_history.Invoice_No, payment_history.client_code ORDER BY Id", d.con1);
            if (ddl_client_gv.SelectedValue == "ALL")
            {
                if (ddl_type.SelectedValue == "1")
                {
                    adp1 = new MySqlDataAdapter("select Id,client_code,Bill_Date,  receipt_de_approve,Status,Invoice_No,Client_Name,State,Branch,MONTH,Taxable_Amount,GST,Bill_Amount,Received_Amount,tds_amount,Balanced_Amount,Received_date,Reject_Reason from (SELECT pay_report_gst . Id ,pay_report_gst . client_code , receipt_de_reasons as 'Reject_Reason', `receipt_de_approve` , CASE WHEN `receipt_de_approve` = '0' THEN 'Pending' WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc' WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc' WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc' END AS 'Status', DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill_Date',  pay_report_gst . Invoice_No  AS 'Invoice_No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH',  ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount', IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   pay_report_gst.tds_amount  )), 0) AS 'Received_Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + pay_report_gst.`tds_amount`)),0),2) AS 'Balanced_Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received_date' FROM pay_report_gst where   pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id ) as t1 where Balanced_Amount <= 0.99 ", d.con1);
                }
                else if (ddl_type.SelectedValue == "2")
                {
                    adp1 = new MySqlDataAdapter("select Id,client_code,Bill_Date,receipt_de_approve,Status,Invoice_No,Client_Name,State,Branch,MONTH,Taxable_Amount,GST,Bill_Amount,Received_Amount,tds_amount,Balanced_Amount,Received_date,Reject_Reason from(SELECT pay_report_gst . Id ,pay_report_gst . client_code , receipt_de_reasons as 'Reject_Reason', `receipt_de_approve` , CASE WHEN `receipt_de_approve` = '0' THEN 'Pending' WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc' WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc' WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc' END AS 'Status', DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill_Date',  pay_report_gst . Invoice_No  AS 'Invoice_No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH',  ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount', IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received_Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + `tds_amount`)),0),2) AS 'Balanced_Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received_date' FROM pay_report_gst   WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id ) as t1 where Balanced_Amount > 0.99 ", d.con1);
                }
                else if (ddl_type.SelectedValue == "ALL")
                {
                    adp1 = new MySqlDataAdapter("SELECT pay_report_gst . Id ,pay_report_gst . client_code ,   `receipt_de_reasons` AS 'receipt_de_reasons', `receipt_de_approve`,CASE  WHEN `receipt_de_approve` = '0' THEN 'Pending'  WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc'   WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc'  WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc'  END AS 'Status', DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill Date',  pay_report_gst . Invoice_No  AS 'Invoice No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH', ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount', IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + `tds_amount`)),0),2) AS 'Balanced Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received date' FROM pay_report_gst    WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id  ", d.con1);
                }
            }
            else if (ddl_client_gv.SelectedValue != "ALL")
            {
                if (ddl_type.SelectedValue == "1")
                {
                    adp1 = new MySqlDataAdapter("select Id,client_code,Bill_Date, receipt_de_approve,Status,Invoice_No,Client_Name,State,Branch,MONTH,Taxable_Amount,GST,Bill_Amount,Received_Amount,tds_amount,Balanced_Amount,Received_date,Reject_Reason from (SELECT pay_report_gst . Id ,pay_report_gst . client_code ,receipt_de_reasons as 'Reject_Reason', `receipt_de_approve`, CASE WHEN `receipt_de_approve` = '0' THEN 'Pending' WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc' WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc'  WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc'  END AS 'Status',  DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill_Date',  pay_report_gst . Invoice_No  AS 'Invoice_No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH',   ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount', IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received_Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + pay_report_gst.`tds_amount`)),0),2) AS 'Balanced_Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received_date' FROM pay_report_gst   WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' and pay_report_gst  .  client_code   = '" + ddl_client_gv.SelectedValue + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id ) as t1 where Balanced_Amount <= 0.99  ", d.con1);
                }
                else if (ddl_type.SelectedValue == "2")
                {
                    adp1 = new MySqlDataAdapter("select Id,client_code,Bill_Date, receipt_de_approve,Status,Invoice_No,Client_Name,State,Branch,MONTH,Taxable_Amount,GST,Bill_Amount,Received_Amount,tds_amount,Balanced_Amount,Received_date,Reject_Reason from (SELECT pay_report_gst . Id ,pay_report_gst . client_code , receipt_de_reasons as 'Reject_Reason', `receipt_de_approve`, CASE WHEN `receipt_de_approve` = '0' THEN 'Pending' WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc' WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc'  WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc'  END AS 'Status',  DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill_Date',  pay_report_gst . Invoice_No  AS 'Invoice_No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH',  ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount',IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received_Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + `tds_amount`)),0),2) AS 'Balanced_Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received_date' FROM pay_report_gst    WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' and pay_report_gst  .  client_code   = '" + ddl_client_gv.SelectedValue + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id) as t1 where Balanced_Amount > 0.99  ", d.con1);
                }
                else if (ddl_type.SelectedValue == "ALL")
                {
                    adp1 = new MySqlDataAdapter("SELECT pay_report_gst . Id ,pay_report_gst . client_code , `receipt_de_approve`,CASE  WHEN `receipt_de_approve` = '0' THEN 'Pending'  WHEN `receipt_de_approve` = '1' THEN 'Approve By Jr Acc'   WHEN `receipt_de_approve` = '2' THEN 'Approve By Sr Acc'  WHEN `receipt_de_approve` = '3' THEN 'Rejected By Sr Acc'  END AS 'Status', DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill_Date',  pay_report_gst . Invoice_No  AS 'Invoice_No',pay_report_gst . client_name  AS 'Client_Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH',  ROUND((`amount`), 2) AS 'Taxable_Amount',ROUND((`cgst` + `sgst` + `igst`), 2) AS 'GST',ROUND((`amount` + `cgst` + `sgst` + `igst`), 2) AS 'Bill_Amount',  IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received_Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + `tds_amount`)),0),2) AS 'Balanced_Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received_date', `receipt_de_reasons`  as 'Reject Reasons' FROM pay_report_gst   WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' and pay_report_gst  .  client_code   = '" + ddl_client_gv.SelectedValue + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id  ", d.con1);
                }
                //adp1 = new MySqlDataAdapter("SELECT pay_report_gst . Id ,pay_report_gst . client_code ,DATE_FORMAT( pay_report_gst . invoice_date , '%d/%m/%Y') AS 'Bill Date',  pay_report_gst . Invoice_No  AS 'Invoice No',pay_report_gst . client_name  AS 'Client Name',pay_report_gst . state_name  AS 'State', pay_report_gst . unit_code  AS 'Branch',  CONCAT( pay_report_gst . month , '/',  pay_report_gst . year ) AS 'MONTH', ROUND(( amount  +  cgst  +  sgst  +   igst  ), 2) AS 'Taxable Amount', ROUND((  cgst   +   sgst   +   igst  ), 2) AS 'GST', ROUND((  amount  ), 2) AS 'Bill Amount', IFNULL(SUM(ROUND(  pay_report_gst  .  received_amt   +   tds_amount  )), 0) AS 'Received Amount', pay_report_gst  .  tds_amount  ,  ROUND(((`amount` + `cgst`+ `sgst`+ `igst`)) - IFNULL(ROUND(SUM(`pay_report_gst`.`received_amt` + `tds_amount`)),0),2) AS 'Balanced Amount', DATE_FORMAT(  pay_report_gst  .  received_date  , '%d/%m/%Y') AS 'Received date' FROM pay_report_gst   LEFT JOIN   pay_report_gst   ON   pay_report_gst  .  Invoice_No   =   pay_report_gst  .  Invoice_No WHERE  pay_report_gst  .  comp_code   = '" + Session["COMP_CODE"].ToString() + "' and pay_report_gst  .  client_code   = '" + ddl_client_gv.SelectedValue + "' AND   pay_report_gst  .  flag_invoice   = 2 GROUP BY pay_report_gst  .  Invoice_No  ,   pay_report_gst  .  client_code  ORDER BY   Id  ", d.con1);
            }
            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                gv_payment.DataSource = ds1.Tables[0];
                gv_payment.DataBind();
            }
            else
            {
                
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('No Matching Records Found !!!')", true);
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

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        string sql = "";
        try
        {
            string row_id = e.CommandArgument.ToString();
            sql = "SELECT comp_code, Invoice_No, CLIENT_CODE, state_name, unit_code, billing_amt, received_amt, received_date, tds, tds_on, tds_amount, adjustment_sign, adjustment_amt, total_received_amt, month, year, '" + Session["LOGIN_ID"].ToString() + "', now(), payment_id FROM pay_report_gst WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "'";

            result = d.operation("insert into payment_delete_history_details (comp_code, Invoice_No, CLIENT_CODE, state, unit_code, billing_amt, received_amt, received_date, tds, tds_on, tds_amount, adjustment_sign, adjustment_amt, total_received_amt, month, year,deleted_by,deleted_date, payment_id ) " + sql);
            if (result > 0)
            {
                result = d.operation("UPDATE pay_report_gst SET billing_amt = 0, received_amt = 0, tds_amount = 0, adjustment_amt = 0, `receipt_de_reasons`='',`receipt_de_approve`='0' ,adjustment_sign = 0, received_date = NULL, total_received_amt = 0, payment_id = 0, uploaded_by = NULL, uploaded_date = NULL WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "'");
                d.operation("delete  from pay_report_gst WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "' and amount = 0 and (igst= 0 || cgst=0 || igst= 0 )");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deleted Succsefully !!!')", true);
                payment_details(d.getsinglestring("select Invoice_No  FROM pay_report_gst WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "'"));
                load_gv_payment("");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deletion Failed !!!')", true);

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    }
    protected void seles()
    {
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT pay_minibank_master.ID,(SELECT COMPANY_NAME FROM pay_company_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'COMPANY NAME', IFNULL((SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_minibank_master.client_code), (SELECT client_name FROM pay_other_client_master WHERE client_code = pay_minibank_master.client_code)) AS 'CLIENT',description as 'Payment Description', pay_minibank_master.Amount as 'Received Amount', DATE_FORMAT(receive_date, '%D - %M - %Y') AS 'Received Date', ROUND(IFNULL(SUM(pay_report_gst.received_amt), 0), 2) AS ' SETTLED AMOUNT', Round(pay_minibank_master.Amount - (IFNULL(SUM(pay_report_gst.received_amt), 0)) ,2) as 'REMANING AMOUNT',`Bank_name` as 'Debit' ,pay_minibank_master.Amount as 'Debit Amount',IFNULL((SELECT `client_name` FROM `pay_client_master` WHERE `comp_code` = 'C01' AND `client_code` = `pay_minibank_master`.`client_code`), (SELECT `client_name` FROM `pay_other_client_master` WHERE `client_code` = `pay_minibank_master`.`client_code`)) as 'Credit ' ,  pay_minibank_master.Amount as 'Credit Amount' FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_report_gst.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' GROUP BY pay_minibank_master.id ", d.con1);
        adp1.Fill(ds1);
        grv_saleentery.DataSource = ds1.Tables[0];
        grv_saleentery.DataBind();
        d.con1.Close();

    }
    //MiniBank Receipt
    public void comp_data()
    {
        try
        {
            txt_comp_name.Text = d.getsinglestring("Select Company_name from pay_company_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' ");

            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT pay_minibank_master.ID,receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' when receipt_approve = '3' then 'Rejected By Sr Acc' end as 'Status', (SELECT COMPANY_NAME FROM pay_company_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'COMPANY NAME',pay_minibank_master. client_name as 'client Name/Other',description as 'Payment Description', DATE_FORMAT(receive_date, '%D - %M - %Y') AS 'Received Date', Amount as 'Credit Amount', Mode_of_transfer as 'Mode of Transfer',Utr_no as 'UTR_NO',Cheque as 'Cheque NO',Upload_file ,ROUND(IFNULL(SUM(`payment_history_details`.`received_amt`), 0), 2) AS ' SETTLED AMOUNT',ROUND(`Amount` - (IFNULL(SUM(`payment_history_details`.`received_amt`), 0)), 2) AS 'REMANING AMOUNT',`receipt_reasons` as 'Rejected Reason' FROM pay_minibank_master LEFT JOIN payment_history_details ON payment_history_details.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' GROUP BY pay_minibank_master.id ", d.con1);
            //MySqlDataAdapter adp1 = new MySqlDataAdapter("SELECT pay_minibank_master.ID,(SELECT COMPANY_NAME FROM pay_company_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'COMPANY NAME', IFNULL((SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_minibank_master.client_code), (SELECT client_name FROM pay_other_client_master WHERE client_code = pay_minibank_master.client_code)) AS 'CLIENT',description as 'Payment Description', Amount as 'Received Amount', DATE_FORMAT(receive_date, '%D - %M - %Y') AS 'Received Date', ROUND(IFNULL(SUM(payment_history_details.received_amt), 0), 2) AS ' SETTLED AMOUNT', Round(Amount - (IFNULL(SUM(payment_history_details.received_amt), 0)) ,2) as 'REMANING AMOUNT',`Bank_name` as 'Debit' ,Amount as 'Debit Amount',IFNULL((SELECT `client_name` FROM `pay_client_master` WHERE `comp_code` = 'C01' AND `client_code` = `pay_minibank_master`.`client_code`), (SELECT `client_name` FROM `pay_other_client_master` WHERE `client_code` = `pay_minibank_master`.`client_code`)) as 'Credit ' ,  Amount as 'Credit Amount' FROM pay_minibank_master LEFT JOIN payment_history_details ON payment_history_details.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' GROUP BY pay_minibank_master.id ", d.con1);
            adp1.Fill(ds1);
            gv_minibank.DataSource = ds1.Tables[0];
            gv_minibank.DataBind();
            d.con1.Close();


            
            //System.Data.DataTable dt_item = new System.Data.DataTable();
            //MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE is null", d.con);
            //d.con.Open();

     
            //cmd_item.Fill(dt_item);
            //if (dt_item.Rows.Count > 0)
            //{

            //    ddl_bank_name.DataSource = dt_item;
            //    ddl_bank_name.DataTextField = dt_item.Columns[0].ToString();
            //    ddl_bank_name.DataValueField = dt_item.Columns[0].ToString();
            //    ddl_bank_name.DataBind();

            //}
            //ddl_bank_name.Items.Insert(0, "Select");
            //dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }

    protected void btn_minibank_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string record_save = null;


            if (ddl_pmt_recived.SelectedValue == "0")
            {
                //record_save = d.getsinglestring("select client_code,`Bank_name`,`Account_number`,`Amount`,client_bank_name,client_ac_number,Mode_of_transfer,Utr_no,uploaded_by,payment_type from pay_minibank_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "' and bank_name = '" + ddl_bank_name.SelectedValue + "' and `Account_number` = '" + ddl_comp_ac_number.Text + "' and `client_bank_name` = '" + ddl_client_bank.SelectedValue + "' and `client_ac_number` ='" + ddl_client_ac_number.SelectedValue + "' and `Amount` ='" + txt_minibank_amount.Text + "' and `receive_date`= str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') and `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "' and `Utr_no`='" + txt_minibank_utr_no.Text + "' and uploaded_by = '" + Session["LOGIN_ID"].ToString() + "' and `payment_type` ='" + ddl_payment_type.SelectedValue + "' ");

                record_save = d.getsinglestring("select client_code,`Bank_name`,`Account_number`,`Amount`,client_bank_name,client_ac_number,Mode_of_transfer,Utr_no,uploaded_by,payment_type from pay_minibank_master where comp_code = '" + Session["comp_code"].ToString() + "' and `Utr_no`='" + txt_minibank_utr_no.Text + "' and `received_from` = '0' ");

                if (record_save != "")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Record All Ready Added')", true);
                    return;


                }
            }
            else

                if (ddl_pmt_recived.SelectedValue == "1")
                {
                    record_save = d.getsinglestring("select `Account_number`,`Amount`,`receive_date`,`description`,`uploaded_by`,client_name,Mode_of_transfer  from pay_minibank_master where comp_code = '" + Session["comp_code"].ToString() + "'  and `Utr_no`='" + txt_minibank_utr_no.Text + "' and`received_from` = '1'  ");

                    if (record_save != "")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Record All Ready Added')", true);
                        return;


                    }

                }



          //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
          
            int res = 0;
            if (ddl_pmt_recived.SelectedValue == "1")
            {
               
                if (txt_description.Text == "") { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Enter description to Payment Related !!!')", true); txt_description.Focus(); return; }
                res = d.operation("Insert Into pay_minibank_master(comp_code,client_name,Bank_name,Account_number,receive_date,description,Amount,uploaded_by,uploaded_date,Mode_of_transfer,Cheque,Utr_no,received_from) values('" + Session["COMP_CODE"].ToString() + "','" + ddl_other.SelectedValue + "','" + ddl_bank_name.SelectedValue + "','" + ddl_comp_ac_number.Text + "',str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y'),'" + txt_description.Text + "','" + txt_minibank_amount.Text + "','" + Session["LOGIN_ID"].ToString() + "', now(),'" + ddl_mode_transfer.SelectedValue + "','" + txt_cheque.Text + "','" + txt_minibank_utr_no.Text + "','" + ddl_pmt_recived.SelectedValue + "')");
                string id = d.getsinglestring("select max(id) from pay_minibank_master ");
                upload_file(id);
            }
            else if (ddl_pmt_recived.SelectedValue == "0")
            {
                
                if (ddl_payment_type.SelectedValue == "Select") { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Select Payment Against  !!!')", true); ddl_payment_type.Focus(); return; }

                res = d.operation("Insert Into pay_minibank_master(comp_code,client_name,Bank_name,Account_number,client_bank_name,client_ac_number,receive_date,payment_type,description,Amount,uploaded_by,uploaded_date,Mode_of_transfer,Cheque,Utr_no,client_code,received_from) values('" + Session["COMP_CODE"].ToString() + "','" + ddl_minibank_client.SelectedItem.Text + "','" + ddl_bank_name.SelectedValue + "','" + ddl_comp_ac_number.Text + "','" + ddl_client_bank.SelectedValue + "','" + ddl_client_ac_number.SelectedValue + "',str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y'),'" + ddl_payment_type.SelectedValue + "','" + ddl_payment_type.SelectedItem.Text + "','" + txt_minibank_amount.Text + "','" + Session["LOGIN_ID"].ToString() + "', now(),'" + ddl_mode_transfer.SelectedValue + "','" + txt_cheque.Text + "','" + txt_minibank_utr_no.Text + "','" + ddl_minibank_client.SelectedValue + "','" + ddl_pmt_recived.SelectedValue + "')");
                string id = d.getsinglestring("select max(id) from pay_minibank_master ");
                upload_file(id);
            }
            
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Saved Successfully Approve This Record !!!')", true);
                //mini_text_clear();
                for_client.Visible = false;
                for_other.Visible = false;
              //  ddl_bank_name.Items.Clear();
              //  ddl_other_bank.Items.Clear();
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Transaction Failed !!!')", true); }
        }
        catch (Exception ex)
        {

            throw ex;

        }
        finally
        {
            comp_data();
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }
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
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["receipt_approve"].ToString() != "0" && dr["receipt_approve"].ToString() != "3")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;


                //  e.Row.Cells[14].Visible = false;
                // e.Row.Cells[15].Visible = false;

                LinkButton lb1 = e.Row.FindControl("LinkButton2") as LinkButton;
                lb1.Visible = false;

                LinkButton lb2 = e.Row.FindControl("btn_edit_other1") as LinkButton;
                lb2.Visible = false;


            }
        }




        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[11].Visible = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_minibank, "Select$" + e.Row.RowIndex);

        }

    }
    protected void gv_minibank_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddl_bank_name_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddl_comp_ac_number.Items.Clear();
    //    System.Data.DataTable dt_item = new System.Data.DataTable();
    //    MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_bank_name.SelectedValue + "'", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        cmd_item.Fill(dt_item);
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ddl_comp_ac_number.DataSource = dt_item;
    //            ddl_comp_ac_number.DataTextField = dt_item.Columns[0].ToString();
    //            ddl_comp_ac_number.DataValueField = dt_item.Columns[0].ToString();
    //            ddl_comp_ac_number.DataBind();

    //        }
    //        //ddl_comp_ac_number.Items.Insert(0, "Select");
    //        dt_item.Dispose();
    //    }
    //    catch (Exception ex) { }
    //    finally { d.con.Close(); }
    //}

    // minibank client
    protected void ddl_minibank_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_client_bank.Items.Clear();
        ddl_client_ac_number.Items.Clear();
        ddl_bank_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;

        if (ddl_pmt_recived.SelectedValue == "1")
        {
            cmd_item = new MySqlDataAdapter("select client_bank_name from pay_other_client_master where client_code='" + ddl_minibank_client.SelectedValue + "' ", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE ='" + ddl_minibank_client.SelectedValue + "' ", d.con);
        }
        d.con.Open();
        try
        {
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
        //    ddl_bank_name.Readonly=true;
            ddl_comp_ac_number.ReadOnly = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        comp_bank_details();

        //komal change 23-04-2020
        //comp_bank details
        //try
        //{
        //    d.con.Open();
        //   //ddl_bank_name.Text = "";
        //    ddl_comp_ac_number.Text = "";
        //    MySqlCommand cmd = null;
        //    if (ddl_pmt_recived.SelectedValue == "1")
        //    {
        //        cmd = new MySqlCommand("Select comp_bank_name,comp_ac_no  from pay_other_client_master where  client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);
        //    }
        //    else
        //    {
        //        cmd = new MySqlCommand("Select comp_bank_name,comp_acc_no  from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);
        //    }
        //    MySqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //       // ddl_bank_name.Text = dr.GetValue(0).ToString();
        //        ddl_comp_ac_number.Text = dr.GetValue(1).ToString();
        //    }

        //}
        //catch (Exception ex) { throw ex; }
        //finally { d.con.Close(); }

    }
    protected void ddl_client_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_client_ac_number.Items.Clear();

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;
        if (ddl_pmt_recived.SelectedValue == "1")
        {
            cmd_item = new MySqlDataAdapter("select client_ac_no from pay_other_client_master where client_code='" + ddl_minibank_client.SelectedValue + "' and client_bank_name = '" + ddl_client_bank.SelectedValue + "' ", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_client_bank.SelectedValue + "' and client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);

        }
        d.con.Open();
        try
        {
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
            bank_name_ac_no();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        
        }
       
    }
    protected void bank_name_ac_no()
    {
        try
        {
            d4.con.Open();
            ddl_bank_name.DataSource = null;

            //d.con.Open();
            //ddl_bank_name.SelectedValue = "";
            ddl_comp_ac_number.Text = "";
            MySqlCommand cmd = null;
            if (ddl_pmt_recived.SelectedValue == "0")
            {
                cmd = new MySqlCommand("Select comp_bank_name,comp_acc_no  from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "'", d4.con);
                //cmd = new MySqlCommand("Select comp_bank_name,comp_ac_no  from pay_other_client_master where  client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);
            }
            //else
            //{
            //    cmd = new MySqlCommand("Select comp_bank_name,comp_acc_no  from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "'", d.con);
            //}
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //ddl_bank_name.Text = dr.GetValue(0).ToString();
                ddl_bank_name.SelectedValue = dr.GetValue(0).ToString();
                ddl_comp_ac_number.Text = dr.GetValue(1).ToString();
            }

        }
        catch (Exception ex) { throw ex; }
        finally { //d.con.Close(); 
            d4.con.Close();

        }
    
    }
    protected void mini_text_clear()
    {
    //    ddl_minibank_client.SelectedValue = "Select";
    //ddl_bank_name.SelectedValue = "";
        ddl_comp_ac_number.Text = "";
        ddl_client_bank.Items.Clear();
        ddl_client_ac_number.Items.Clear();
        ddl_mode_transfer.SelectedValue = "Select";
        txt_minibank_received_date.Text = "";
        txt_description.Text = "";

        // komal -25/06/2020
     //   ddl_payment_type.SelectedIndex = 0;
     //   ddl_payment_type.SelectedValue = "Select";
        txt_minibank_amount.Text = "";
        txt_cheque.Text = "";
        txt_minibank_utr_no.Text = "";
        //ddl_other.SelectedValue = "Select";
       // ddl_other_bank.SelectedValue=""

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

    protected void gv_minibank_menu4(object sender, EventArgs e)
    {
        try
        {
            grv_saleentery.UseAccessibleHeader = false;
            grv_saleentery.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void lnkminiDelete_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        string sql = "";
        try
        {

            string row_id = e.CommandArgument.ToString();

            sql = "SELECT comp_code,client_code,Bank_name,Account_number,Account_balance,client_bank_name,client_ac_number,month,year,receive_date,payment_type,Amount,description,'" + Session["LOGIN_ID"].ToString() + "', now() FROM pay_minibank_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "'";
            string filename = d.getsinglestring("select upload_file FROM pay_minibank_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + row_id + "' ");
            File.Delete(Server.MapPath("~/Account_images/") + filename);
            d.operation("delete FROM pay_minibank_master WHERE  Id = '" + row_id + "'");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deleted Succsefully !!!')", true);
            comp_data();
            result = d.operation("insert into pay_delete_minibank_history (comp_code,client_code,Bank_name,Account_number,Account_balance,client_bank_name,client_ac_number,month,year,receive_date,payment_type,Amount,description,deleted_by,deleted_date ) " + sql);
            if (result > 0)
            {
               // d.operation("delete FROM pay_minibank_master WHERE  Id = '" + row_id + "'");

               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deleted Succsefully !!!')", true);
               // comp_data();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deletion Failed !!!')", true);

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    }

    protected void gv_payment_detail_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_payment_detail.UseAccessibleHeader = false;
            gv_payment_detail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void ddl_pmt_recived_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           mini_text_clear();
            client_bank.Visible = true;
            client_ac_no.Visible = true;
            bank_name.Visible = true;
            desc.Visible = false;
            if (ddl_pmt_recived.SelectedValue == "1")
            {
             //   ddl_bank_name.SelectedValue = "";
                ddl_comp_ac_number.Text = "";
                for_client.Visible = false;
                bank_name.Visible = false;
                for_other.Visible = true;
                for_other1.Visible = true;
                client_bank.Visible = false;
                client_ac_no.Visible = false;
                other_client_code();
               // btn_add_others.Visible = true;
                ddl_payment_type.Visible = false;
                lbl_payment_type.Visible = false;
                txt_description.Visible = true;
                Panel1.Visible = false;
                pnl_desc.Visible = true;
                desc.Visible = true;
            }
            else
            {
              //  ddl_bank_name.SelectedValue = "";
                ddl_comp_ac_number.Text = "";
                bank_name.Visible = true;
                for_client.Visible = true;
                for_other.Visible = false;
                for_other1.Visible = false;
                client_bank.Visible = true;
                client_ac_no.Visible = true;
                client_code();
                btn_add_others.Visible = false;
                ddl_payment_type.Visible = true;
                lbl_payment_type.Visible = true;
                txt_description.Visible = false;
                Panel1.Visible = true;
                pnl_desc.Visible = false;
                desc.Visible = false;
            }
        }
        catch (Exception ex) { throw ex; }
        finally { //mini_text_clear(); 
        }
    }

    //protected void btn_add_others_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (ddl_pmt_recived.SelectedValue == "1")
    //        {
    //            string client_code = create_client_code();

    //            d.operation("insert into pay_other_client_master(client_code,client_name,created_by,created_date)values('"+client_code+"','"+txt_client_name.Text+"','"+Session["LOGIN_ID"].ToString()+"',now()) ");
    //            other_client_code();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Added successfully !!!')", true);
    //        }

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { }
    //}

    protected string create_client_code()
    {
        string client_code_series = "C";
        try
        {
            string other_client = d.getsinglestring("select Max(substring(client_code,2)+1) from pay_other_client_master");
            if (other_client == "")
            {
                client_code_series = client_code_series + "001";
            }

            else
            {
                int number = int.Parse(other_client);

                if (number < 10)
                {
                    client_code_series = client_code_series + "00" + other_client;
                }
                else if (number > 9 && number < 100)
                {
                    client_code_series = client_code_series + "0" + other_client;
                }
                else if (number > 99)
                {
                    client_code_series = client_code_series + other_client;
                }
            }
            return client_code_series;
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }

    protected void other_client_code()
    {

        ddl_minibank_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and type='minibank' ORDER BY Field1", d.con);
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_code,client_name from pay_other_client_master  ORDER BY client_name", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_other.DataSource = dt_item;

                ddl_other.DataValueField = dt_item.Columns[0].ToString();
                //ddl_minibank_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_other.DataBind();


            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();

            ddl_other.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //for bank name
        System.Data.DataTable dt_item1 = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and type='bank_details'  and client_code is null ORDER BY Field1", d.con);
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_code,client_name from pay_other_client_master  ORDER BY client_name", d.con);
        d.con.Open();
        try
        {
            cmd_item1.Fill(dt_item1);
            if (dt_item1.Rows.Count > 0)
            {

                ddl_other_bank.DataSource = dt_item1;

                ddl_other_bank.DataValueField = dt_item1.Columns[0].ToString();
                //ddl_minibank_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_other_bank.DataBind();
            }
            dt_item1.Dispose();
            // hide_controls();
            d.con.Close();

           // ddl_other_bank.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        Bank_name();
    }

    protected bool check_validation(string process)
    {

        double total_amount = 0;
        double received_amt = 0;
        double update_total_amount = 0;
       
        foreach (GridViewRow row in gv_invoice_pmt.Rows)
        {
            if (row != null)
            {

                result = 0;
                string invoice_no = row.Cells[1].Text;

                double txt_bill_amount = double.Parse(row.Cells[2].Text);

                double txt_receive_amount = double.Parse(((TextBox)row.FindControl("txt_recive_amt")).Text);

                string txt_reciving_date = ((TextBox)row.FindControl("txt_reciving_date")).Text;

                //DropDownList ddl_tds_amount = (DropDownList)row.FindControl("ddl_tds_amount");

                //DropDownList ddl_tds_on = (DropDownList)row.FindControl("ddl_tds_on");

                double txt_tds_amt = double.Parse(((TextBox)row.FindControl("txt_tds_amt")).Text);

                int adj_selectedvalue = int.Parse(((DropDownList)row.FindControl("ddl_adjustment")).SelectedValue);

                double txt_adjustment_amt = double.Parse(((TextBox)row.FindControl("txt_adjustment_amt")).Text);



                int month = int.Parse(return_fields1("month", invoice_no));
                int year = int.Parse(return_fields1("year", invoice_no));

                double tds_amount = 0;

                //check receving amount not zero
                if (txt_receive_amount.Equals(0) || txt_receive_amount.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount Must be Greater Than Zero!!!')", true);
                    row.FindControl("txt_recive_amt").Focus();
                    return true;
                }
                //tds amount
                //if (ddl_tds_amount.SelectedValue != "Amount")
                //{
                //    int tds_persent = int.Parse(ddl_tds_amount.SelectedValue);
                //    tds_amount = ddl_tds_on.SelectedValue == "0" ? (double.Parse(return_fields("taxable_amount", invoice_no)) * tds_persent) / 100 : (txt_bill_amount * tds_persent) / 100;
                //}
                //else
                //{
                tds_amount = txt_tds_amt;
                //}
                if (process == "Insert")
                {
                    update_total_amount = double.Parse(d.getsinglestring("select ifnull(Round(sum(total_received_amt),2), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "' and id = '" + ViewState["row_id"] + "'  order by id"));
                    received_amt = double.Parse(d.getsinglestring("select ifnull(Round(sum(total_received_amt),2), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "'  order by id"));
                }
                else if (process == "Update")
                {
                    received_amt = double.Parse(d.getsinglestring("select ifnull(Round(sum(total_received_amt),2), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "' and id != '" + ViewState["row_id"] + "'  order by id"));
                    update_total_amount = double.Parse(d.getsinglestring("select ifnull(Round(sum(total_received_amt),2), 0) from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and invoice_no='" + invoice_no + "' and client_code='" + ddl_client.SelectedValue + "' and id = '" + ViewState["row_id"] + "'  order by id"));
                }
                //adjustment amount
                string adj_sign = "";

                if (adj_selectedvalue == 1)
                {

                    if ((txt_bill_amount - (received_amt + txt_adjustment_amt + txt_receive_amount + tds_amount)) != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Your Adjustment Amount is : " + (txt_bill_amount - (received_amt + txt_adjustment_amt + txt_receive_amount + tds_amount)) + " You Can Enter Wrong Adjustment Amount !!!')", true);
                        row.FindControl("txt_adjustment_amt").Focus();
                        return true;
                    }
                    received_amt = received_amt + txt_adjustment_amt;

                }

                if (adj_selectedvalue == 2)
                {

                    if ((txt_bill_amount - ((received_amt - txt_adjustment_amt) + txt_receive_amount + tds_amount)) != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Your Adjustment Amount is : " + (txt_bill_amount - ((received_amt - txt_adjustment_amt) + txt_receive_amount + tds_amount)) + " You Can Enter Wrong Adjustment Amount !!!')", true);
                        row.FindControl("txt_adjustment_amt").Focus();
                        return true;
                    }
                    received_amt = received_amt - txt_adjustment_amt;

                }
                string matching = String.Format("{0:0.00}", (received_amt + txt_receive_amount + tds_amount));
                Double balance_amt = Convert.ToDouble(matching);

                if (txt_bill_amount >= (balance_amt))
                {
                    try
                    {

                        total_amount = total_amount + (received_amt + txt_receive_amount + tds_amount);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    { }

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount Exceed Client Billing Amount!!!')", true);
                    //row.FindControl("txt_recive_amt").Focus();
                    //return true;
                }

            }
        }

        //temprory comment 29/07/2019
        if (total_amount > (double.Parse(ddl_client_resive_amt.SelectedItem.Text) + update_total_amount))
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Received Amount Exceed Client Payment Amount !!!')", true);
            //ddl_client_resive_amt.Focus();
           // return true;////temprory comment 29/07/2019
            return false;
        }
        else { return false; }
    }



    //payment
    public void company_bank_load()
    {

        try
        {
            ddl_company_bank.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;
            if (ddl_pmt_paid.SelectedValue == "2")
            {
                cmd_item = new MySqlDataAdapter("Select Field1 , Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE is null", d.con);
            }
            else if (ddl_pmt_paid.SelectedValue == "3")
            {
                cmd_item = new MySqlDataAdapter("Select Field1 , Field2 from pay_zone_master where Field2 != '" + ddl_pmt_client.SelectedValue + "' and Type = 'bank_details' and CLIENT_CODE is null", d.con);
            }
            d.con.Open();



            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_company_bank.DataSource = dt_item;
                ddl_company_bank.DataTextField = dt_item.Columns[0].ToString();
                ddl_company_bank.DataValueField = dt_item.Columns[1].ToString();
                ddl_company_bank.DataBind();
                d.con.Close();
            }

            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_company_bank.Items.Insert(0, new ListItem("Select"));
            ddl_batch_no.Items.Clear();
            ddl_batch_no.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }


    }

    protected void ddl_pmt_paid_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        pmt_text_clear(0);
        payment_type_selection();
        load_gv_debit_pmt_details(ddl_pmt_paid.SelectedValue);
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
    }

    protected void ddl_company_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";

        try
        {
            if (ddl_pmt_paid.SelectedValue == "3")
            {
                txt_pmt_desc.Text = "";

                txt_pmt_desc.Text = ddl_company_bank.SelectedValue;
                txt_pmt_desc.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
        }
    }

    protected void ddl_pmt_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            //client payment
            if (ddl_pmt_paid.SelectedValue == "1")
            {
                DataSet ds = new DataSet();
                ddl_batch_no.Items.Clear();
                ds = d.select_data("SELECT batch_no FROM (SELECT batch_no, (amount_payable - paid_Amount) AS 'remaining_amount', amount_payable FROM (SELECT batch_no, SUM(REPLACE(amount_payable, ',', '')) AS 'amount_payable', SUM(CAST(IFNULL(Amount, 0) AS signed)) AS 'paid_Amount' FROM paypro_uploaded_data INNER JOIN pay_pro_master ON paypro_uploaded_data.batch_no = pay_pro_master.paypro_batch_id LEFT JOIN pay_debit_master ON pay_debit_master.annuxure_no = paypro_uploaded_data.batch_no WHERE pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_pro_master.client_code = '" + ddl_pmt_client.SelectedValue + "' AND transaction_status = 'Paid' GROUP BY batch_no) AS t1) AS t2 WHERE amount_payable > 0 AND remaining_amount > 0");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_batch_no.DataSource = ds.Tables[0];
                    ddl_batch_no.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddl_batch_no.DataTextField = ds.Tables[0].Columns[0].ToString();
                    ddl_batch_no.DataBind();


                }

                ds.Dispose();
                ddl_batch_no.Items.Insert(0, "Select");
                load_gv_debit_pmt_details(1);
            }
            //vendor payment
            else if (ddl_pmt_paid.SelectedValue == "2")
            {
                DataSet ds = new DataSet();
                ddl_batch_no.Items.Clear();
                ds = d.select_data("SELECT DOC_NO FROM (SELECT doc_no, (FINAL_PRICE - ifnull(amount,0)) AS 'amount' FROM pay_transactionp LEFT JOIN pay_debit_master ON pay_debit_master.client_code = pay_transactionp.CUST_CODE AND pay_debit_master.annuxure_no = pay_transactionp.DOC_NO WHERE CUST_CODE = '" + ddl_pmt_client.SelectedValue + "') AS t1 WHERE amount > 0   ");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_batch_no.DataSource = ds.Tables[0];
                    ddl_batch_no.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddl_batch_no.DataTextField = ds.Tables[0].Columns[0].ToString();
                    ddl_batch_no.DataBind();


                }

                ds.Dispose();
                ddl_batch_no.Items.Insert(0, "Select");
                load_gv_debit_pmt_details(2);
            }
            //Internal transfer
            else if (ddl_pmt_paid.SelectedValue == "3")
            {
                txt_pmt_ac_no.Text = "";
                txt_pmt_ac_no.Text = ddl_pmt_client.SelectedValue;
                company_bank_load();
                load_gv_debit_pmt_details(3);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }

    protected void ddl_batch_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {
//ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            //Employee payemnt
            if (ddl_pmt_paid.SelectedValue == "1")
            {
                txt_pmt_amount.Text = "";
                txt_pmt_amount.Text = d.getsinglestring("SELECT SUM(REPLACE(amount_payable, ',', '')) AS 'amount_payable' FROM paypro_uploaded_data INNER JOIN pay_pro_master ON paypro_uploaded_data.batch_no = pay_pro_master.paypro_batch_id WHERE pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_pmt_client.SelectedValue + "' AND transaction_status = 'Paid' and batch_no = '" + ddl_batch_no.SelectedValue + "' GROUP BY batch_no");
                txt_pmt_amount.ReadOnly = true;

                txt_comp_bank_name.Text = d.getsinglestring("select  Field1  from pay_zone_master inner join paypro_uploaded_data ON pay_zone_master.comp_code =  paypro_uploaded_data.comp_code and pay_zone_master.Field2 =  paypro_uploaded_data.debit_ac_no  where  paypro_uploaded_data.comp_code = '" + Session["COMP_CODE"].ToString() + "' and paypro_uploaded_data.batch_no =  '" + ddl_batch_no.SelectedValue + "' limit 1");
                //ddl_company_bank.ReadOnly = true;
            }

            //vendor payment
            else if (ddl_pmt_paid.SelectedValue == "2")
            {
                txt_pmt_amount.Text = d.getsinglestring("select FINAL_PRICE from pay_transactionp where DOC_NO = '" + ddl_batch_no.SelectedValue + "'");
                txt_pmt_amount.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }

    protected void btn_pmt_submit_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        int result = 0;
        string insert_field = null, select_field = null, txt_bank_no = null;

        try
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            check_pmt_validation();

            if (ddl_pmt_mode.SelectedValue != "Select")
            {
                if (ddl_pmt_mode.SelectedValue == "Cheque")
                {
                    insert_field = ",pmt_mode_transfer,ChequeNo,ChequeReceivedDate,DipositeDate";
                    select_field = ",'" + ddl_pmt_mode.SelectedValue + "','" + txt_cheque_no.Text + "' ,str_to_date('" + txt_cheque_receive_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_cheque_deposite_date.Text + "','%d/%m/%Y') ";
                }
                else
                {
                    insert_field = ",pmt_mode_transfer,UTR_No";
                    select_field = ",'" + ddl_pmt_mode.SelectedValue + "','" + txt_utr_no.Text + "'";

                }
            }
            txt_bank_no = d.getsinglestring("select  debit_ac_no  from  paypro_uploaded_data  where  paypro_uploaded_data.comp_code = '" + Session["COMP_CODE"].ToString() + "' and paypro_uploaded_data.batch_no =  '" + ddl_batch_no.SelectedValue + "' limit 1");
            //client payment insert 
            if (ddl_pmt_paid.SelectedValue == "1")
            {
                result = d.operation(" insert into pay_debit_master (comp_code,client_code,annuxure_no,Comp_Bank_name,Comp_Account_number,payment_type,description,Amount,payment_date,uploaded_by,uploaded_date " + insert_field + ") values ('" + Session["COMP_CODE"].ToString() + "' , '" + ddl_pmt_client.SelectedValue + "' , '" + ddl_batch_no.SelectedValue + "' , '" + txt_comp_bank_name.Text + "' ,'" + txt_bank_no + "', '" + ddl_pmt_paid.SelectedValue + "','" + ddl_pmt_desc.SelectedItem.Text + "' , '" + txt_pmt_amount.Text + "' , str_to_date('" + txt_pmt_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now() " + select_field + ") ");
                string id = d.getsinglestring("select max(id) from pay_debit_master ");
                upload_Click(id);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Payment Succsefully!!');", true);
                    load_gv_debit_pmt_details(1);
                    pmt_text_clear(1);
                }
            }

            else if (ddl_pmt_paid.SelectedValue == "2")
            {
                result = d.operation(" insert into pay_debit_master (comp_code,client_code,annuxure_no,Comp_Bank_name,payment_type,description,Amount,payment_date,uploaded_by,uploaded_date " + insert_field + ") values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_pmt_client.SelectedValue + "' , '" + ddl_batch_no.SelectedValue + "' , '" + ddl_company_bank.SelectedValue + "' , '" + ddl_pmt_paid.SelectedValue + "', '" + txt_pmt_desc.Text + "', '" + txt_pmt_amount.Text + "' , str_to_date('" + txt_pmt_date.Text + "','%d/%m/%Y')'" + Session["LOGIN_ID"].ToString() + "',now()  " + select_field + ") ");
                string id = d.getsinglestring("select max(id) from pay_debit_master ");
                upload_Click(id);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vendor Payment  Succsefully!!');", true);
                    load_gv_debit_pmt_details(2);
                    pmt_text_clear(1);
                }

            }
            else if (ddl_pmt_paid.SelectedValue == "3")
            {
                result = d.operation(" insert into pay_debit_master (payment_type ,transfer_to_bank_name,transfer_to_ac_no ,transfer_from_bank_name,transfer_from_ac_no ,Amount,payment_date,uploaded_by,uploaded_date " + insert_field + ") values ('" + ddl_pmt_paid.SelectedValue + "', '" + ddl_pmt_client.SelectedItem.Text + "' , '" + txt_pmt_ac_no.Text + "' , '" + ddl_company_bank.SelectedItem.Text + "' ,'" + txt_pmt_desc.Text + "', '" + txt_pmt_amount.Text + "' , str_to_date('" + txt_pmt_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now() " + select_field + ")");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Internal Transfer Succsefully!!');", true);
                    load_gv_debit_pmt_details(3);
                    pmt_text_clear(1);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }

    protected void ddl_pmt_mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";

        try
        {
            if (ddl_pmt_mode.SelectedValue == "Cheque")
            {
                panel_mode.Visible = false;
                panel_mode_cheque.Visible = true;
                txt_utr_no.Text = "";
            }
            else
            {
                panel_mode.Visible = true;
                panel_mode_cheque.Visible = false;
                txt_cheque_no.Text = "";
                txt_cheque_receive_date.Text = "";
                txt_cheque_deposite_date.Text = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }

    protected void upload_Click(string id)
    {


        if (document1_file.HasFile)
        {

            string fileExt = System.IO.Path.GetExtension(document1_file.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP" || fileExt.ToUpper() == ".XLSX" || fileExt.ToUpper() == ".XLS")
            {
                string fileName = Path.GetFileName(document1_file.PostedFile.FileName);
                document1_file.PostedFile.SaveAs(Server.MapPath("~/Annuxure_upload/") + fileName);
               // string id = d.getsinglestring("select ifnull(max(id),0) from pay_debit_master ");

                string file_name = ddl_pmt_client.SelectedValue + ddl_batch_no.SelectedValue + id + fileExt;

                File.Copy(Server.MapPath("~/Annuxure_upload/") + fileName, Server.MapPath("~/Annuxure_upload/") + file_name, true);
                File.Delete(Server.MapPath("~/Annuxure_upload/") + fileName);



                d.operation("update   pay_debit_master set  annuxure_file='" + file_name + "', uploaded_by='" + Session["LOGIN_ID"].ToString() + "', uploaded_date=now()  where comp_code='" + Session["COMP_CODE"].ToString() + "' and annuxure_no = '" + ddl_batch_no.SelectedValue + "'");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Uploaded Succsefully!!');", true);


            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select only JPG, PNG , XLSX, XLS and PDF  Files  !!');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
                return;
            }


        }
    }

    protected void check_pmt_validation()
    {
        try
        {
            double payble_amount = double.Parse(txt_pmt_amount.Text);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    protected void payment_type_selection()
    {
        try
        {
            //employee payment
            if (ddl_pmt_paid.SelectedValue == "1")
            {
                client_code();
                panel_add_other.Visible = false;
                ddl_payment_type.Visible = true;
                lbl_payment_type.Visible = true;
                panel_annexure_id.Visible = true;
                Panel_client_desc.Visible = true;
                Panel_other_desc.Visible = false;
                btn_add_others.Visible = false;
                panel_ddl_bank_name.Visible = false;
                panel_txt_bank_name.Visible = true;
                panel_ac_no.Visible = false;

                lable_client.Text = "Client :";
                lable_bank.Text = " Company Bank Name :";
                label_ac_no.Text = "";
            }

             //vendor payment
            else if (ddl_pmt_paid.SelectedValue == "2")
            {

                vendor_load();
                company_bank_load();
                ddl_payment_type.Visible = true;
                lbl_payment_type.Visible = true;
                panel_annexure_id.Visible = true;
                Panel_client_desc.Visible = false;
                Panel_other_desc.Visible = true;
                panel_add_other.Visible = false;
                panel_ddl_bank_name.Visible = true;
                panel_txt_bank_name.Visible = false;
                panel_ac_no.Visible = false;

                lable_client.Text = "Vendor :";
                lable_bank.Text = " Company Bank Name :";
                label_ac_no.Text = "Description";
            }

            //Internal transfer
            else
            {

                internal_transfer();
                panel_add_other.Visible = false;
                ddl_payment_type.Visible = true;
                lbl_payment_type.Visible = true;
                panel_annexure_id.Visible = false;
                Panel_client_desc.Visible = false;
                Panel_other_desc.Visible = true;
                panel_ddl_bank_name.Visible = true;
                btn_add_others.Visible = false;
                panel_txt_bank_name.Visible = false;
                panel_ac_no.Visible = true;
                att_upload_panel.Visible = false;
                lable_client.Text = "Transfer To :";
                lable_bank.Text = "Transfer From :";
                label_ac_no.Text = " A/C No :";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }

    protected void vendor_load()
    {

        try
        {
            DataSet ds = new DataSet();
            ddl_pmt_client.Items.Clear();
            ds = d.select_data("select VEND_ID,VEND_NAME from pay_vendor_master where comp_code = '" + Session["COMP_CODE"].ToString() + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_pmt_client.DataSource = ds.Tables[0];
                ddl_pmt_client.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddl_pmt_client.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddl_pmt_client.DataBind();

                ddl_pmt_client.Items.Insert(0, "Select");
            }

            ds.Dispose();



        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    protected void internal_transfer()
    {

        try
        {
            DataSet ds = new DataSet();
            ddl_pmt_client.Items.Clear();
            ds = d.select_data("Select field2,Field1 from pay_zone_master where Type = 'bank_details' and CLIENT_CODE is null");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_pmt_client.DataSource = ds.Tables[0];
                ddl_pmt_client.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddl_pmt_client.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddl_pmt_client.DataBind();

                ddl_pmt_client.Items.Insert(0, "Select");
            }

            ds.Dispose();



        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            load_gv_debit_pmt_details(3);
        }
    }

    protected void load_gv_debit_pmt_details(int i)
    {
        try
        {
            DataSet ds = new DataSet();
            gv_debit_pmt_details.DataSource = null;
            gv_debit_pmt_details.DataBind();

            //Employee payment
            if (i == 1)
            {
                ds = d.select_data("SELECT distinct pay_debit_master.Id,pay_debit_master.Comp_Bank_name, Comp_Account_number, pay_client_master.client_name, annuxure_no, Amount, description, date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_client_master ON pay_debit_master.comp_code = pay_client_master.comp_code AND pay_debit_master.client_code = pay_client_master.client_code where pay_debit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_debit_master.client_Code = '" + ddl_pmt_client.SelectedValue + "' ");
            }
            //Vendor payment
            else if (i == 2)
            {
                ds = d.select_data("SELECT  distinct pay_debit_master.Id, pay_debit_master.Comp_Bank_name, Comp_Account_number, VEND_NAME, annuxure_no, Amount, description,  date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_vendor_master ON pay_debit_master.client_code = pay_vendor_master.VEND_ID Where pay_debit_master.client_Code = '" + ddl_pmt_client.SelectedValue + "' ");
            }
            //Internal Transfer
            else if (i == 3)
            {
                ds = d.select_data("SELECT pay_debit_master.Comp_Bank_name, Comp_Account_number, pay_client_master.client_name, annuxure_no, Amount, description,  date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_client_master ON pay_debit_master.comp_code = pay_client_master.comp_code AND pay_debit_master.client_code = pay_client_master.client_code where pay_debit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_debit_master.client_Code = '" + ddl_pmt_client.SelectedValue + "' ");

            }


            gv_debit_pmt_details.DataSource = ds;
            gv_debit_pmt_details.DataBind();
            ds.Dispose();


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }
    protected void load_gv_debit_pmt_details(string i)
    {
        try
        {
            DataSet ds = new DataSet();
            gv_debit_pmt_details.DataSource = null;
            gv_debit_pmt_details.DataBind();

            //Employee payment
            if (i == "1")
            {
                ds = d.select_data("SELECT distinct pay_debit_master.Id,pay_debit_master.Comp_Bank_name, Comp_Account_number, pay_client_master.client_name, annuxure_no, Amount, description, date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_client_master ON pay_debit_master.comp_code = pay_client_master.comp_code AND pay_debit_master.client_code = pay_client_master.client_code where pay_debit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_debit_master.payment_type = '" + ddl_pmt_paid.SelectedValue + "' ");
            }
            //Vendor payment
            else if (i == "2")
            {
                ds = d.select_data("SELECT  distinct pay_debit_master.Id, pay_debit_master.Comp_Bank_name, Comp_Account_number, VEND_NAME, annuxure_no, Amount, description,  date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_vendor_master ON pay_debit_master.client_code = pay_vendor_master.VEND_ID Where pay_debit_master.payment_type = '" + ddl_pmt_paid.SelectedValue + "' ");
            }
            //Internal Transfer
            else if (i == "3")
            {
                ds = d.select_data("SELECT pay_debit_master.Comp_Bank_name, Comp_Account_number, pay_client_master.client_name, annuxure_no, Amount, description,  date_format(payment_date,'%d/%m/%Y') as 'payment_date', annuxure_file FROM pay_debit_master INNER JOIN pay_client_master ON pay_debit_master.comp_code = pay_client_master.comp_code AND pay_debit_master.client_code = pay_client_master.client_code where pay_debit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_debit_master.client_Code = '" + ddl_pmt_client.SelectedValue + "' ");

            }


            gv_debit_pmt_details.DataSource = ds;
            gv_debit_pmt_details.DataBind();
            ds.Dispose();


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }

    }
    protected void gv_debit_pmt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        //Employee payment
        if (ddl_pmt_paid.SelectedValue == "1")
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        //Vendor payment
        else if (ddl_pmt_paid.SelectedValue == "2")
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        //Internal Transfer
        else if (ddl_pmt_paid.SelectedValue == "3")
        {

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_minibank, "Select$" + e.Row.RowIndex);

        }
    }
    protected void pmt_text_clear(int text_clear_mode)
    {
        if (text_clear_mode.Equals(1))
        {
            ddl_pmt_client.SelectedValue = "Select";
        }
        else
        {
            ddl_pmt_client.Items.Clear();
        }
        ddl_batch_no.Items.Clear();
        ddl_company_bank.Items.Clear();
        txt_pmt_desc.Text = "";
        txt_pmt_amount.Text = "";
        txt_pmt_date.Text = "";
        txt_pmt_ac_no.Text = "";
        ddl_pmt_mode.SelectedIndex = 0;
        txt_utr_no.Text = "";
        txt_cheque_no.Text = "";
        txt_cheque_receive_date.Text = "";
        txt_cheque_deposite_date.Text = "";
        txt_comp_bank_name.Text = "";
        ddl_pmt_desc.SelectedValue = "Select";

    }

    protected void lnkpmtDownload_Command(object sender, CommandEventArgs e)
    {
        string filename = e.CommandArgument.ToString();


        if (filename != "")
        {
            downloadfile(filename);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }
    }

    protected void downloadfile(string filename)
    {

        //var result = filename.Substring(filename.Length - 4);
        //if (result.Contains("jpeg"))
        //{
        //    result = ".jpeg";
        //}
        try
        {


            string path2 = Server.MapPath("~\\Annuxure_upload\\" + filename);

            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip/xls/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile("~\\Annuxure_upload\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();





        }
        catch (Exception ex) { }


    }
    protected void gv_invoice_list_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_invoice_list.UseAccessibleHeader = false;
            gv_invoice_list.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void lnkpmtDelete_Command(object sender, CommandEventArgs e)
    {
        int result = 0;

        try
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            string Id = commandArgs[0];

            string filename = commandArgs[1];

            if (!filename.Equals(""))
            {
                string delete_file = System.IO.Path.Combine(@"~/Annuxure_upload/" + filename);
                if (File.Exists(delete_file))
                {
                    File.Delete(delete_file);
                }
            }
            result = d.operation("delete FROM pay_debit_master WHERE  Id = '" + Id + "'");

            if (result > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deleted Succsefully !!!')", true);
                load_gv_debit_pmt_details(ddl_pmt_paid.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deletion Failed !!!')", true);

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    }
    protected void gv_debit_pmt_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_debit_pmt_details.UseAccessibleHeader = false;
            gv_debit_pmt_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    string FolderPath = "~/Temp_images/";
                    FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    btn_Import_Click(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Ledger File Uploaded Successfully...');", true);
                    File.Delete(FilePath);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload a valid excel file.');", true);
                }
            }
            catch (Exception ee)
            {
                throw ee;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('System Error - Please Try again....');", true);
            }
            finally
            {
                File.Delete(FilePath);
            }
        }
    }
    public void btn_Import_Click(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension.ToUpper())
        {
            case ".XLS":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".XLSX":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        //   OleDbCommand cmdExcel1 = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        // OleDbDataAdapter oda1 = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        //System.Data.DataTable dt1 = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;
        //cmdExcel1.Connection = connExcel;

        // Get The Name of First Sheet
        connExcel.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet

        connExcel.Open();
        cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);

        connExcel.Close();

        //check file format

        //Push Datatable to database
        DataTable table2 = new DataTable("ledger");
        if (ddl_upload_lg_client.SelectedValue == "8")//Kotak
        {
            table2.Columns.Add("Invoice_Number");
            table2.Columns.Add("Invoice_Date");
            table2.Columns.Add("GL_Date");
            table2.Columns.Add("Batch_No");
            table2.Columns.Add("Invoice_Type");
            table2.Columns.Add("Payment_Voucher");
            table2.Columns.Add("Check_No");
            table2.Columns.Add("Description");
            table2.Columns.Add("Account");
            table2.Columns.Add("Amount_Dr");
            table2.Columns.Add("Amount_Cr");
            table2.Columns.Add("Comments");
        }
        else if (ddl_upload_lg_client.SelectedValue == "4")//BFL
        {
            table2.Columns.Add("Account");
            table2.Columns.Add("Business_Area");
            table2.Columns.Add("Offsetting_acct_no");
            table2.Columns.Add("Assignment");
            table2.Columns.Add("Reference");
            table2.Columns.Add("Document_Number");
            table2.Columns.Add("Document_Type");
            table2.Columns.Add("Tax_code");
            table2.Columns.Add("Posting_Date");
            table2.Columns.Add("Document_Date");
            table2.Columns.Add("Clearing_Document");
            table2.Columns.Add("Special_GL_ind");
            table2.Columns.Add("Amount_in_local_currency");
            table2.Columns.Add("Text");
            table2.Columns.Add("Withholding_tax_amnt");
            table2.Columns.Add("Withhldg_tax_base_amount");
            table2.Columns.Add("Payment_reference");
            table2.Columns.Add("Comments");
            //cmdExcel1.CommandText = "SELECT * FROM [" + sheetName + "]";
            //oda1.SelectCommand = cmdExcel1;
            //oda1.Fill(dt1);
        }
        else if (ddl_upload_lg_client.SelectedValue == "RLIC HK")
        {
            table2.Columns.Add("Clearing_Date");
            table2.Columns.Add("Clearing_Document");
            table2.Columns.Add("Name_of_posting_key");
            table2.Columns.Add("Doc_No");
            table2.Columns.Add("Document_Date");
            table2.Columns.Add("Due_On");
            table2.Columns.Add("Discount");
            table2.Columns.Add("Amount");
            table2.Columns.Add("Reference_Number");
            table2.Columns.Add("Assignment_Number");
            table2.Columns.Add("Comments");
        }
        else if (ddl_upload_lg_client.SelectedValue == "HDFC")
        {
            table2.Columns.Add("Assignment");
            table2.Columns.Add("Document Number");
            table2.Columns.Add("Document Type");
            table2.Columns.Add("Document Date");
            table2.Columns.Add("Special GL ind");
            table2.Columns.Add("Posting Date");
            table2.Columns.Add("Withholding tax amnt");
            table2.Columns.Add("Withhldg tax base amount");
            table2.Columns.Add("Amount in local currency");
            table2.Columns.Add("Clearing Document");
            table2.Columns.Add("Text");
            table2.Columns.Add("Reference");
            table2.Columns.Add("Parked by");
            table2.Columns.Add("Account");
            table2.Columns.Add("Comments");
        }
        else if (ddl_upload_lg_client.SelectedValue == "RBL")
        {
            table2.Columns.Add("BAZ_CLAIM_NO");
            table2.Columns.Add("CLAIM_TYPE");
            table2.Columns.Add("STATUS_DATE");
            table2.Columns.Add("STATUS");
            table2.Columns.Add("APPROVED_AMOUNT");
            table2.Columns.Add("ADJUSTED_AMOUNT");
            table2.Columns.Add("NET_PAYABLE_AMOUNT");
            table2.Columns.Add("CLAIM_FOR_USER_NAME_VENDOR_NAME");
            table2.Columns.Add("PAYREFNO_PAYREFDATE");
            table2.Columns.Add("PAYMENT_SEQUENCE_NO_DATE");
            table2.Columns.Add("TDS_AMOUNT");
            table2.Columns.Add("INVOICE_NO");
            table2.Columns.Add("INVOICE_DATE");
            table2.Columns.Add("COMMENTS");
        }
        else if (ddl_upload_lg_client.SelectedValue.Contains("BAG") || ddl_upload_lg_client.SelectedValue == "BG")
        {
            table2.Columns.Add("Account_Code");
            table2.Columns.Add("Accounting_Period");
            table2.Columns.Add("Base_Amount");
            table2.Columns.Add("Debit_Credit_marker");
            table2.Columns.Add("Transaction_Date");
            table2.Columns.Add("Journal_No");
            table2.Columns.Add("Journal_Line");
            table2.Columns.Add("Journal_Type");
            table2.Columns.Add("Journal_Source");
            table2.Columns.Add("Transaction_Reference");
            table2.Columns.Add("Description");
            table2.Columns.Add("COMMENTS");
        }
        else if (ddl_upload_lg_client.SelectedValue == "DHFL")
        {
            table2.Columns.Add("cluster_ref_no");
            table2.Columns.Add("circle");
            table2.Columns.Add("cluster");
            table2.Columns.Add("branch");
            table2.Columns.Add("service_centre");
            table2.Columns.Add("invoice_no");
            table2.Columns.Add("invoice_dt");
            table2.Columns.Add("vendor_name");
            table2.Columns.Add("head");
            table2.Columns.Add("gross_amt");
            table2.Columns.Add("period_for_which_payment_is_due");
            table2.Columns.Add("pan_no");
            table2.Columns.Add("service_tax_no");
            table2.Columns.Add("inward_date");
            table2.Columns.Add("received_by");
            table2.Columns.Add("paid_on");
            table2.Columns.Add("mode_of_payment");
            table2.Columns.Add("dispatch_date");
            table2.Columns.Add("dispatched_to");
            table2.Columns.Add("COMMENTS");
        }
        else if (ddl_upload_lg_client.SelectedValue == "7")
        {
            table2.Columns.Add("Doc_Chq_Date");
            table2.Columns.Add("Amount_in_Local_Currency");
            table2.Columns.Add("Text");
            table2.Columns.Add("UTR_No");
            table2.Columns.Add("Clearing_Date");
            table2.Columns.Add("Hdr_Text_Bank");
            table2.Columns.Add("COMMENTS");
        }
        else if (ddl_upload_lg_client.SelectedValue == "BRLI")
        {
            table2.Columns.Add("No");
            table2.Columns.Add("Transaction_Number");
            table2.Columns.Add("Vendor_Invoice_Date");
            table2.Columns.Add("Date");
            table2.Columns.Add("Due_Date");
            table2.Columns.Add("Age");
            table2.Columns.Add("Amount_Gross");
            table2.Columns.Add("CGST");
            table2.Columns.Add("SGST");
            table2.Columns.Add("IGST");
            table2.Columns.Add("Tds");
            table2.Columns.Add("Net_Payable");
            table2.Columns.Add("COMMENTS");
        }
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    int res = 0;

                    if (ddl_upload_lg_client.SelectedValue == "8")//Kotak
                    {
                        if (r[0].ToString().Trim() != "" && !r[0].ToString().ToUpper().Contains("REF") && !r[0].ToString().ToUpper().Contains("DATE") && !r[0].ToString().ToUpper().Contains("VENDOR") && !r[0].ToString().ToUpper().Contains("INVOICE") && !r[0].ToString().ToUpper().Contains("FROM") && !r[0].ToString().ToUpper().Contains("LEDGER"))
                        {
                            try
                            {
                                if (r[0].ToString().ToUpper().Contains("TDS"))
                                {
                                    res = d.operation("update pay_report_gst set tds_deducted=" + r[9].ToString().Trim() + " where INSTR('" + r[0].ToString().Trim() + "', invoice_no) > 0");
                                }
                                else
                                {
                                    string payment_date = r[2].ToString().Trim();
                                    if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                    
                                    //CHANGES BY VINOD TO CHECK SERVER DATETIME FORMAT
                                    //res = d.operation("update pay_report_gst set payment=" + r[9].ToString().Trim() + ",payment_date=str_to_date('" + payment_date + "','%m/%d/%Y'), flag=1 where invoice_no = '" + r[0].ToString().Trim() + "'");
                                    res = d.operation("update pay_report_gst set payment=" + r[9].ToString().Trim() + ",payment_date='" + payment_date + "', flag=1 where invoice_no = '" + r[0].ToString().Trim() + "'");
                                }
                                if (res == 0)
                                {
                                    //if (!d.getsinglestring("select payment_status from pay_pro_master where BANK_EMP_AC_CODE = '" + bank_code.Trim() + "' and floor(Payment) = " + amount + " and comp_code='" + comp_code + "' and MONTH = " + txt_month_year.Text.Substring(0, 2) + " AND YEAR=" + txt_month_year.Text.Substring(3) + "").Equals("1"))
                                    //  {
                                    table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), "Invoice number not Matching");
                                    //}
                                }

                            }
                            catch (Exception ex)
                            {
                                table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), ex.Message.ToString());
                            }
                        }
                    }
                    else if (ddl_upload_lg_client.SelectedValue == "RLIC HK")//Reliance
                    {
                        try
                        {
                            if (r[8].ToString().Trim() != "" && !r[8].ToString().ToUpper().Contains("REFER"))   
                            {
                                string payment_date = r[5].ToString().Trim();
                                double amount = double.Parse(r[7].ToString().Trim()) * -1;
                                foreach (DataRow m in dt.Rows)
                                {
                                    if (m[1].ToString().Trim() == r[1].ToString().Trim() && m[7].ToString().Trim() == amount.ToString())
                                    {
                                        payment_date = m[5].ToString().Trim();
                                        break;
                                    }
                                }
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                               // res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1, tds_deducted = 0  where invoice_no = '" + r[8].ToString().Trim() + "'");
                                res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date='" + payment_date + "', flag=1, tds_deducted = 0  where invoice_no = '" + r[8].ToString().Trim() + "'");

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), "Invoice number not Matching");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), ex.Message.ToString());
                        }

                    }
                    else if (ddl_upload_lg_client.SelectedValue == "4")//BFL
                    {
                        if (r[6].ToString().Trim() != "" && !r[6].ToString().ToUpper().Contains("REF"))
                        {
                            try
                            {
                                string payment_date = "";
                                double amount = double.Parse(r[14].ToString().Trim()) * -1;//converting to positive number
                                double tds = double.Parse(r[16].ToString().Trim()) * -1;//converting to positive number
                                foreach (DataRow m in dt.Rows)
                                {
                                    if (m[12].ToString().Trim() == r[12].ToString().Trim() && m[14].ToString().Trim() == amount.ToString())
                                    {
                                        payment_date = m[11].ToString().Trim();
                                        break;
                                    }
                                }

                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                                if (!payment_date.Equals(""))
                                {
                                    //res = d.operation("update pay_report_gst set transaction_ref='" + r[18].ToString().Trim() + "', batchid = '" + r[12].ToString().Trim() + "', tds_deducted=" + tds + ", payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1 where invoice_no like '%" + r[6].ToString().Trim() + "%'");
                                    res = d.operation("update pay_report_gst set transaction_ref='" + r[18].ToString().Trim() + "', batchid = '" + r[12].ToString().Trim() + "', tds_deducted=" + tds + ", payment=" + amount + ",payment_date='" + payment_date + "', flag=1 where invoice_no like '%" + r[6].ToString().Trim() + "%'");
                                }

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), "Invoice number not Matching");
                                }
                            }
                            catch (Exception ex)
                            {
                                table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), ex.Message.ToString());
                            }
                        }
                    }
                    else if (ddl_upload_lg_client.SelectedValue == "HDFC")//HDFC
                    {
                        if (r[12].ToString().Trim() != "" && !r[12].ToString().ToUpper().Contains("REF"))
                        {
                            try
                            {
                                string payment_date = "";
                                double amount = double.Parse(r[9].ToString().Trim()) * -1;//converting to positive number
                                double tds = double.Parse(r[7].ToString().Trim()) * -1;//converting to positive number
                                foreach (DataRow m in dt.Rows)
                                {
                                    if (m[10].ToString().Trim() == r[10].ToString().Trim() && m[9].ToString().Trim() == amount.ToString())
                                    {
                                        payment_date = m[6].ToString().Trim();
                                        break;
                                    }
                                }

                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                                if (!payment_date.Equals(""))
                                {
                                    //res = d.operation("update pay_report_gst set batchid = '" + r[10].ToString().Trim() + "', tds_deducted=" + tds + ", payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1 where invoice_no like '%" + r[12].ToString().Trim() + "%'");
                                    res = d.operation("update pay_report_gst set batchid = '" + r[10].ToString().Trim() + "', tds_deducted=" + tds + ", payment=" + amount + ",payment_date='" + payment_date + "', flag=1 where invoice_no like '%" + r[12].ToString().Trim() + "%'");
                                }

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), "Invoice number not Matching");
                                }
                            }
                            catch (Exception ex)
                            {
                                table2.Rows.Add(r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), ex.Message.ToString());
                            }
                        }
                    }
                    else if (ddl_upload_lg_client.SelectedValue.Contains("BAG") || ddl_upload_lg_client.SelectedValue == "BG")//BAG
                    {
                        if (r[12].ToString().Trim() != "" && !r[12].ToString().ToUpper().Contains("REF"))
                        {
                            try
                            {
                                string payment_date = "";
                                double amount = double.Parse(r[5].ToString().Trim()) * -1;//converting to positive number
                                foreach (DataRow m in dt.Rows)
                                {
                                    if (m[13].ToString().Trim().Contains(r[13].ToString().Trim()) && m[5].ToString().Trim() == amount.ToString())
                                    {
                                        payment_date = m[7].ToString().Trim();
                                        break;
                                    }
                                }

                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                                if (!payment_date.Equals(""))
                                {
                                    //res = d.operation("update pay_report_gst set batchid = '" + r[13].ToString().Trim() + "', payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1 where invoice_no like '%" + r[12].ToString().Trim() + "%'");
                                    res = d.operation("update pay_report_gst set batchid = '" + r[13].ToString().Trim() + "', payment=" + amount + ",payment_date='" + payment_date + "', flag=1 where invoice_no like '%" + r[12].ToString().Trim() + "%'");
                                }

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), "Invoice number not Matching");
                                }
                            }
                            catch (Exception ex)
                            {
                                table2.Rows.Add(r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), ex.Message.ToString());
                            }
                        }
                    }
                    else if (ddl_upload_lg_client.SelectedValue == "RBL")//RBL BANK Ltd.s
                    {
                        try
                        {
                            if (r[11].ToString().Trim() != "" && !r[11].ToString().ToUpper().Contains("INVOI"))
                            {
                                string payment_date = r[2].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                double amount = double.Parse(r[6].ToString().Trim());
                                //res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1, tds_deducted = " + double.Parse(r[10].ToString().Trim()) + " where invoice_no = '" + r[11].ToString().Trim() + "'");
                                res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date='" + payment_date + "', flag=1, tds_deducted = " + double.Parse(r[10].ToString().Trim()) + " where invoice_no = '" + r[11].ToString().Trim() + "'");

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), "Invoice number not Matching");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), ex.Message.ToString());
                        }

                    }
                    else if (ddl_upload_lg_client.SelectedValue == "DHFL")
                    {
                        try
                        {
                            if (r[5].ToString().Trim() != "" && !r[5].ToString().ToUpper().Contains("INVOI"))
                            {
                                string payment_date = r[15].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                double amount = double.Parse(r[9].ToString().Trim());
                                //res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date=str_to_date('" + payment_date + "','%d/%m/%Y'), flag=1 where invoice_no = '" + r[5].ToString().Trim() + "'");
                                res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date='" + payment_date + "', flag=1 where invoice_no = '" + r[5].ToString().Trim() + "'");

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), "Invoice number not Matching");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), ex.Message.ToString());
                        }

                    }
                    else if (ddl_upload_lg_client.SelectedValue == "7")
                    {
                        try
                        {
                            if (r[1].ToString().Trim() != "" && !r[1].ToString().ToUpper().Contains("DOCUM"))
                            {

                                string payment_date = "";
                                double amount = double.Parse(r[3].ToString().Trim()) * -1;//converting to positive number
                                foreach (DataRow m in dt.Rows)
                                {
                                    if (m[5].ToString().Trim().Contains(r[5].ToString().Trim()) && m[3].ToString().Trim() == amount.ToString())
                                    {
                                        payment_date = m[6].ToString().Trim();
                                        break;
                                    }
                                }

                                if (!payment_date.Equals(""))
                                {
                                    if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                    res = d.operation("update pay_report_gst set payment=" + amount + ",payment_date= '" + payment_date + "', flag=1, transaction_ref = '" + r[5].ToString().Trim() + "' where invoice_no = '" + r[7].ToString().Replace("BN:", "").Trim() + "'");
                                }
                                if (res == 0)
                                {
                                    table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), "Invoice number not Matching");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), ex.Message.ToString());
                        }

                    }
                    else if (ddl_upload_lg_client.SelectedValue == "BRLI")
                    {
                        try
                        {
                            if (r[2].ToString().Trim() != "" && !r[2].ToString().ToUpper().Contains("NO"))
                            {
                                string payment_date = r[6].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                double amount = double.Parse(r[14].ToString().Trim());
                                res = d.operation("update pay_report_gst set payment=" + amount + ",transaction_ref = '" + r[3].ToString().Trim() + "', payment_date='" + payment_date + "', flag=1 where invoice_no = '" + r[2].ToString().Trim() + "'");

                                if (res == 0)
                                {
                                    table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[14].ToString(), "Invoice number not Matching");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[14].ToString(), ex.Message.ToString());
                        }

                    }
                }
                catch (Exception ex)
                {
                    // comments = ex.Message;
                    throw ex;
                }
            }
            if (table2.Rows.Count > 0)
            {
                DataSet ds = new DataSet("ledger");
                ds.Tables.Add(table2);
                send_file(ds, ddl_upload_lg_client.SelectedValue);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully !!!');", true);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            connExcel.Close();
        }
    }
    private void send_file(DataSet ds, string client_code)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Ledger_issue.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, client_code);
            Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, client_code);
            Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, null);
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
        string client_code = "";
        public MyTemplate(ListItemType type, DataSet ds, string client_code)
        {
            this.type = type;
            this.ds = ds;
            this.client_code = client_code;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    if (client_code == "8")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>Invoice Date</th><th>GL Date</th><th>Batch No</th><th>Invoice Type</th><th>Payment Voucher</th><th>Check No</th><th>Description</th><th>Account</th><th>Amount Dr</th><th>Amount Cr</th></tr>");
                    }
                    else if (client_code == "4")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>Account</th><th>Business Area</th><th>Offsetting acct no</th><th>Assignment</th><th>Reference</th><th>Document Number</th><th>Document Type</th><th>Tax code</th><th>Posting Date</th><th>Document Date</th><th>Clearing Document</th><th>Special GL ind</th><th>Amount in local currency</th><th>Text</th><th>Withholding tax amnt</th><th>Withhldg tax base amount</th><th>Payment reference</th></tr>");
                    }
                    else if (client_code == "RLIC HK")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>Clearing Date</th><th>Clearing Document</th><th>Name of posting key</th><th>Doc. No.</th><th>Document Date</th><th>Due On</th><th>Discount</th><th>Amount</th><th>Reference Number</th><th>Assignment Number</th><th>Comments</th></tr>");
                    }
                    else if (client_code == "HDFC")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>ASSIGNMENT</th><th>DOCUMENT NUMBER</th><th>DOCUMENT TYPE</th><th>DOCUMENT DATE</th><th>SPECIAL GL INDA</th><th>POSTING DATE</th><th>WITHHOLDING TAX AMNT</th><th>WITHHLDG TAX BASE AMOUNT</th><th>AMOUNT IN LOCAL CURRENCY</th><th>CLEARING DOCUMENT</th><th>TEXT</th><th>REFERENCE</th><th>PARKED BY</th><th>ACCOUNT</th><th>COMMENTS</th></tr>");
                    }
                    else if (client_code == "RBL")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>BAZ_CLAIM_NO</TH><TH>CLAIM_TYPE</TH><TH>STATUS_DATE</TH><TH>STATUS</TH><TH>APPROVED_AMOUNT</TH><TH>ADJUSTED_AMOUNT</TH><TH>NET_PAYABLE_AMOUNT</TH><TH>CLAIM_FOR_USER_NAME_VENDOR_NAME</TH><TH>PAYREFNO_PAYREFDATE</TH><TH>PAYMENT_SEQUENCE_NO_DATE</TH><TH>TDS_AMOUNT</TH><TH>INVOICE_NO</TH><TH>INVOICE_DATE</TH><TH>COMMENTS</th></tr>");
                    }
                    else if (client_code.Contains("BAG") || client_code == "BG")//BAGIC
                    {
                        lc = new LiteralControl("<table border=1><tr><th>ACCOUNT_CODE</TH><TH>ACCOUNTING_PERIOD</TH><TH>BASE_AMOUNT</TH><TH>DEBIT_CREDIT_MARKER</TH><TH>TRANSACTION_DATE</TH><TH>JOURNAL_NO</TH><TH>JOURNAL_LINE</TH><TH>JOURNAL_TYPE</TH><TH>JOURNAL_SOURCE</TH><TH>TRANSACTION_REFERENCE</TH><TH>DESCRIPTION</TH><TH>COMMENTS</th></tr>");
                    }
                    else if (client_code == "DHFL")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>cluster_ref_no</TH><TH>circle</TH><TH>cluster</TH><TH>branch</TH><TH>service_centre</TH><TH>invoice_no</TH><TH>invoice_dt</TH><TH>vendor_name</TH><TH>head</TH><TH>gross_amt</TH><TH>period_for_which_payment_is_due</TH><TH>pan_no</TH><TH>service_tax_no</TH><TH>inward_date</TH><TH>received_by</TH><TH>paid_on</TH><TH>mode_of_payment</TH><TH>dispatch_date</TH><TH>dispatched_to</TH><TH>comments</TH></tr>");
                    }
                    else if (client_code == "7")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>Doc_Chq_Date</th><th>Amount_in_Local_Currency</th><th>Text</th><th>UTR_No</th><th>Clearing_Date</th><th>Hdr_Text_Bank</th><TH>comments</TH></tr>");
                    }
                    else if (client_code == "BRLI")
                    {
                        lc = new LiteralControl("<table border=1><tr><th>No</th><th>Transaction_Number</th><th>Vendor_Invoice_Date</th><th>Date</th><th>Due_Date</th><th>Age</th><th>Amount_Gross</th><th>CGST</th><th>SGST</th><th>IGST</th><th>Tds</th><th>Net_Payable</th><TH>comments</TH></tr>");
                    }
                    break;
                case ListItemType.Item:
                    if (client_code == "8")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td></tr>");
                    }
                    else if (client_code == "4")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td><td>" + ds.Tables[0].Rows[ctr][12] + " </td><td>" + ds.Tables[0].Rows[ctr][13] + " </td><td>" + ds.Tables[0].Rows[ctr][14] + " </td><td>" + ds.Tables[0].Rows[ctr][15] + " </td><td>" + ds.Tables[0].Rows[ctr][16] + " </td></tr>");
                    }
                    else if (client_code == "RLIC HK")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td></tr>");
                    }
                    else if (client_code == "HDFC")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td><td>" + ds.Tables[0].Rows[ctr][12] + " </td><td>" + ds.Tables[0].Rows[ctr][13] + " </td><td>" + ds.Tables[0].Rows[ctr][14] + " </td></tr>");
                    }
                    else if (client_code == "RBL")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td><td>" + ds.Tables[0].Rows[ctr][12] + " </td><td>" + ds.Tables[0].Rows[ctr][13] + " </td></tr>");
                    }
                    else if (client_code.Contains("BAG") || client_code == "BG")//BAGIC
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td></tr>");
                    }
                    else if (client_code == "DHFL")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td><td>" + ds.Tables[0].Rows[ctr][12] + " </td><td>" + ds.Tables[0].Rows[ctr][13] + " </td><td>" + ds.Tables[0].Rows[ctr][14] + " </td><td>" + ds.Tables[0].Rows[ctr][15] + " </td><td>" + ds.Tables[0].Rows[ctr][16] + " </td><td>" + ds.Tables[0].Rows[ctr][17] + " </td><td>" + ds.Tables[0].Rows[ctr][18] + " </td><td>" + ds.Tables[0].Rows[ctr][19] + " </td></tr>");
                    }
                    else if (client_code == "7")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td></tr>");
                    }
                    else if (client_code == "BRLI")
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr][0] + " </td><td>" + ds.Tables[0].Rows[ctr][1] + " </td><td>" + ds.Tables[0].Rows[ctr][2] + " </td><td>" + ds.Tables[0].Rows[ctr][3] + " </td><td>" + ds.Tables[0].Rows[ctr][4] + " </td><td>" + ds.Tables[0].Rows[ctr][5] + " </td><td>" + ds.Tables[0].Rows[ctr][6] + " </td><td>" + ds.Tables[0].Rows[ctr][7] + " </td><td>" + ds.Tables[0].Rows[ctr][8] + " </td><td>" + ds.Tables[0].Rows[ctr][9] + " </td><td>" + ds.Tables[0].Rows[ctr][10] + " </td><td>" + ds.Tables[0].Rows[ctr][11] + " </td><td>" + ds.Tables[0].Rows[ctr][12] + " </td></tr>");
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
    protected void btn_report_Click(object sender, EventArgs e)
    {
        export_xl(1);
    }

    private void export_xl(int i)
    {

        string sql = null;

        if (i == 1)
        {
            string where = " WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_upload_lg_client.SelectedValue + "' ";
            if (ddl_upload_lg_client.SelectedValue.Contains("BAG") || ddl_upload_lg_client.SelectedValue == "BG")//BAGIC
            {
                where = " WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code like '%BAG%' or client_code = 'BG'";
            }
            if (ddl_upload_lg_client.SelectedValue.Equals("4"))
            {
                sql = "SELECT client_name, state_name, month, year, invoice_no, DATE_FORMAT(invoice_date, '%d/%m/%Y'), '', type, ROUND(amount, 2) AS 'amount', ROUND(cgst, 2) AS 'cgst', ROUND(sgst, 2) AS 'sgst', ROUND(igst, 2) AS 'igst', ROUND((cgst + sgst + igst), 2) AS 'total_gst', ROUND((cgst + sgst + igst + amount), 2) AS 'total_amount', ROUND((cgst + sgst + igst + amount)-(amount * (if(comp_code='C02',0.01,0.02))), 2) AS 'total_payment', DATE_FORMAT(payment_date, '%d/%m/%Y') AS 'Payment_date', ROUND(payment, 2) AS 'payment', ROUND(IF(payment = 0, 0, ((cgst + sgst + igst + amount) - payment-(amount * (if(comp_code='C02',0.01,0.02))))), 2) AS 'query_amount', ROUND(IF(payment != 0, 0, ((cgst + sgst + igst + amount) - payment)), 2) AS 'outstanding_amount', ROUND(tds_deduction, 2) AS 'tds_deduction', ROUND(tds_deducted, 2) AS 'tds_deducted', ROUND((tds_deduction - tds_deducted), 2) AS 'tds_differance' FROM pay_report_gst " + where + " ORDER BY 5,3 ";
            }
            else
            {
                sql = "SELECT client_name, state_name, month, year, invoice_no, DATE_FORMAT(invoice_date, '%d/%m/%Y'), '', type, ROUND(amount, 2) AS 'amount', ROUND(cgst, 2) AS 'cgst', ROUND(sgst, 2) AS 'sgst', ROUND(igst, 2) AS 'igst', ROUND((cgst + sgst + igst), 2) AS 'total_gst', ROUND((cgst + sgst + igst + amount), 2) AS 'total_amount', ROUND((cgst + sgst + igst + amount) - (amount * (if(comp_code='C02',0.01,0.02))), 2) AS 'total_payment', DATE_FORMAT(payment_date, '%d/%m/%Y') AS 'Payment_date', ROUND(payment, 2) AS 'payment', ROUND(IF(payment = 0, 0, ((cgst + sgst + igst + amount) - payment - (amount * (if(comp_code='C02',0.01,0.02))))), 2) AS 'query_amount', ROUND(IF(payment != 0, 0, ((cgst + sgst + igst + amount) - payment- (amount * (if(comp_code='C02',0.01,0.02))))), 2) AS 'outstanding_amount', ROUND(tds_deduction, 2) AS 'tds_deduction', ROUND(tds_deducted, 2) AS 'tds_deducted', ROUND((tds_deduction - tds_deducted), 2) AS 'tds_differance' FROM pay_report_gst " + where + " ORDER BY 5,3 ";
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
                Response.AddHeader("content-disposition", "attachment;filename=LEDGER_REPORT.xls");
            }

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate12(ListItemType.Header, ds, i);
            Repeater1.ItemTemplate = new MyTemplate12(ListItemType.Item, ds, i);
            Repeater1.FooterTemplate = new MyTemplate12(ListItemType.Footer, ds, i);
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

    public class MyTemplate12 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        static int ctr1;
        int i;
        string emp_type;
        int i3 = 1;
        private ListItemType listItemType;

        public MyTemplate12(ListItemType type, DataSet ds, int i)
        {
            // TODO: Complete member initialization
            this.type = type;
            this.ds = ds;
            this.i = i;

        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:

                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=23 align=center> LEDGER REPORT </th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>MONTH</th><th>YEAR</th><th>INVOICE NO</th><th>INVOICE DATE</th><th>INVOICE PERIOD</th><th>SERVICE CATEGORY</th><th>SUBTOTAL</th><th>CGST</th><th>SGST</th><th>IGST</th><th>TOTAL GST</th><th>GRAND TOTAL AMOUNT</th><th>TOTAL PAYMENT</th><th>PAYMENT DATE</th><th>PAYMENT</th><th>QUERY AMOUNT</th><th>OUTSTANDING AMOUNT</th><th>TDS DEDUCTION</th><th>TDS DEDUCTED</th><th>TDS DIFFERANCE</th></tr>");
                    }

                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr][0].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][1].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][2].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][3].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][4].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][5].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][6].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][7].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][8].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][9].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][10].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][11].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][12].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][13].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][14].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][15].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][16].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][17].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][18].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][19].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][20].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr][21].ToString().ToUpper() + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 9>Total</td><td>=ROUND(SUM(J3:J" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(K3:K" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(L3:L" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(M3:M" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(N3:N" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(O3:O" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(P3:P" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(Q3:Q" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(R3:R" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(S3:S" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(T3:T" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(U3:U" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(V3:V" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(W3:W" + (ctr + 3) + "),2)</td></b></tr>";
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
    protected void payment_gv()
    {
        hidtab.Value = "5";
        d.con.Open();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        try
        {
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = new MySqlDataAdapter("select ID,batchid,state_name,invoice_no,invoice_date,Amount,payment,payment_date,transaction_ref,tds_deducted,remarks from pay_report_gst where client_code='" + ddl_upload_lg_client.SelectedValue + "' ORDER BY id DESC ", d.con);
            // MySqlDataAdapter dt_status = new MySqlDataAdapter("select ID,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', answer1) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', answer2) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', answer3) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', answer4) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', answer5) AS 'que_5_ans', CONCAT('Deep Cleaning og office on every Saturday ? :-', answer6) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', answer7) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', answer8) AS 'que_8_ans',CONCAT('Compliance Management ? :-', answer9) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dusy Bins Condition/Cleaning Material supply ? :-', answer10) AS 'que_10_ans',pay_service_rating.remark,(CASE flag   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating.emp_code = '" + dd1_super.SelectedValue + "' AND pay_service_rating.client_code = '" + ddl_client.SelectedValue + "' AND pay_service_rating.unit_code = '" + ddl_unit.SelectedValue + "'  ORDER BY id DESC", d.con);
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                payment_gridview.DataSource = ds_status;
                payment_gridview.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }

    protected void payment_gridview_PreRender(object sender, EventArgs e)
    {
        try

        {
            payment_gridview.UseAccessibleHeader = false;
            payment_gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
       // payment_gv();
    }
    protected void ddl_upload_lg_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        payment_gv();
    }
    protected void payment_gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        submit_btn.Visible = true;
        d.con.Open();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        try
        {
            ViewState["invoice_no"] = payment_gridview.SelectedRow.Cells[3].Text;
            string batchid = payment_gridview.SelectedRow.Cells[1].Text;
            string state = payment_gridview.SelectedRow.Cells[2].Text;
           // string invoice = payment_gridview.SelectedRow.Cells[3].Text;
            string date_invoice = payment_gridview.SelectedRow.Cells[4].Text;
            string payment = payment_gridview.SelectedRow.Cells[5].Text;
            string date_payment = payment_gridview.SelectedRow.Cells[6].Text;
            string ref_no = payment_gridview.SelectedRow.Cells[7].Text;
            string deduction = payment_gridview.SelectedRow.Cells[8].Text;
            string remark = payment_gridview.SelectedRow.Cells[9].Text;

            MySqlCommand cmd = new MySqlCommand("select batchid,state_name,invoice_no,invoice_date,payment,payment_date,transaction_ref,tds_deducted,remarks from pay_report_gst where invoice_no= '" + ViewState["invoice_no"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_batchid.Text = dr.GetValue(0).ToString();
                txt_state.Text = dr.GetValue(1).ToString();
                txt_invoice.Text = dr.GetValue(2).ToString();
                txt_date_invoice.Text = dr.GetValue(3).ToString();
                txt_payment.Text = dr.GetValue(4).ToString();
                  txt_date_payment.Text = dr.GetValue(5).ToString();
                  txt_ref.Text = dr.GetValue(6).ToString();
                  txt_deduction.Text = dr.GetValue(7).ToString();
                  txt_remark.Text = dr.GetValue(8).ToString();
            }
            dr.Dispose();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void payment_gridview_RowDataBound1(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.payment_gridview, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
    }
    protected void payment_gridview_PreRender1(object sender, EventArgs e)
    {

        try
        {
            payment_gridview.UseAccessibleHeader = false;
            payment_gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void submit_btn_Click(object sender, EventArgs e)
    {
        try
        {
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result = 0;
            // string invoice = payment_gridview.SelectedRow.Cells[3].Text;
            result = d.operation("update pay_report_gst set payment='" + txt_payment.Text + "' , remarks='" + txt_remark.Text + "', batchid='" + txt_batchid.Text + "', transaction_ref='" + txt_ref.Text + "' where invoice_no='" + ViewState["invoice_no"].ToString() + "'");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record update successfully');", true);
            }
            payment_gv();
            txt_batchid.Text = "";
            txt_state.Text = "";
            txt_invoice.Text = "";
            txt_date_invoice.Text = "";
            txt_payment.Text = "";
            txt_date_payment.Text = "";
            txt_ref.Text = "";
            txt_deduction.Text = "";
            txt_remark.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            
        }
    }
    protected void close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void txt_date_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load_client_amount();
        ddl_client_resive_amt.Items.Clear();
        try
        {
            DataTable dt_item = new DataTable();
            ddl_client_resive_amt.Items.Clear();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select Id,amount from ( SELECT pay_minibank_master.id AS 'Id',pay_minibank_master.amount   FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_minibank_master.id = pay_report_gst.payment_id AND pay_minibank_master.CLIENT_CODE = pay_report_gst.CLIENT_CODE WHERE pay_minibank_master.receive_date = str_to_date('" + txt_date.Text + "', '%d-%m-%Y') AND pay_minibank_master.client_code = '" + ddl_client.SelectedValue + "' AND `receipt_approve` != '0' GROUP BY pay_minibank_master.id, pay_report_gst.payment_id)  as t1 where  amount > 0 ORDER BY amount  ", d.con);
            d.con.Open();

            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_resive_amt.DataSource = dt_item;

                ddl_client_resive_amt.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_resive_amt.DataTextField = dt_item.Columns[1].ToString();
                ddl_client_resive_amt.DataBind();
            }
            //ddl_client_resive_amt.Items.Insert(0, "Select");
            dt_item.Dispose();
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
    protected void ddl_mode_transfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pmt_recived.SelectedValue == "1")
        { Bank_name(); }
        else
        {
            d.con.Open();
            bank_name_ac_no();
            d.con.Close();
        }
        d.con.Open();
        try
        {
            if (ddl_mode_transfer.SelectedValue == "Cheque")
            {
                cheque.Visible = true;
                utr_no.Visible = false;
            }
            else
            {
                cheque.Visible = false;
                utr_no.Visible = true;
            }
        }
        catch (Exception ex)
        {
            d.con.Close();
            throw ex;
        }
        finally
        {

        }

    }
    protected void upload_file(string id)
    {


        if (photo_upload.HasFile)
        {

            string fileExt = System.IO.Path.GetExtension(photo_upload.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP" || fileExt.ToUpper() == ".XLSX" || fileExt.ToUpper() == ".XLS")
            {
                string fileName = Path.GetFileName(photo_upload.PostedFile.FileName);
                photo_upload.PostedFile.SaveAs(Server.MapPath("~/Account_images/") + fileName);
                // string id = d.getsinglestring("select ifnull(max(id),0) from pay_debit_master ");

                string file_name = ddl_minibank_client.SelectedValue + id + fileExt;

                File.Copy(Server.MapPath("~/Account_images/") + fileName, Server.MapPath("~/Account_images/") + file_name, true);
                File.Delete(Server.MapPath("~/Account_images/") + fileName);



                d.operation("update pay_minibank_master set  Upload_file='" + file_name + "', uploaded_by='" + Session["LOGIN_ID"].ToString() + "', uploaded_date=now()  where comp_code='" + Session["COMP_CODE"].ToString() + "' and id='"+id+"' ");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Uploaded Succsefully!!');", true);


            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select only JPG, PNG , XLSX, XLS and PDF  Files  !!');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
                return;
            }


        }
    }
    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {
        //string filePath = (sender as LinkButton).CommandArgument;
        //Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        //Response.WriteFile(filePath);
        //Response.End();

        string filename = e.CommandArgument.ToString();


        if (filename != "")
        {
            string path2 = Server.MapPath("~\\Account_images\\" + filename);

            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip/xls/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile("~\\Account_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }

    }
    protected void ddl_payment_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        d.con.Open();
        try
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
          //  bank_name_ac_no();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }
       
    }
    protected void Bank_name()
    {
        try
        {
        d.con.Open();
        MySqlCommand cmd_item1 = new MySqlCommand("Select Field1, Field2 from pay_zone_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and Type = 'bank_details' and Field1='" + ddl_other_bank .SelectedValue+ "'", d.con);
       // MySqlCommand cmd_item1 = new MySqlCommand("Select comp_bank_name, comp_acc_no from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and comp_bank_name='" + ddl_other_bank.SelectedValue + "'", d.con);
        MySqlDataReader dr = cmd_item1.ExecuteReader();
        while (dr.Read())
        { 
            ddl_bank_name.SelectedValue=dr.GetValue(0).ToString();
            ddl_comp_ac_number.Text = dr.GetValue(1).ToString();
        
        }
            d.con.Close();
            //ddl_other_bank.Items.Insert(0, "Select");
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_other_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddl_bank_name.Text = "";
        ddl_comp_ac_number.Text = "";
        Bank_name();
        
    }

    // company bank details komal 23-04-2020
    protected void comp_bank_details() 
    {

        MySqlDataAdapter comp_bank = null;
        System.Data.DataTable dt_item = new System.Data.DataTable();

        comp_bank = new MySqlDataAdapter("select distinct payment_ag_bank from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_minibank_client.SelectedValue + "'", d.con);
        d.con.Open();

        try
        {
            comp_bank.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_bank_name.DataSource = dt_item;
                ddl_bank_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_bank_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_bank_name.DataBind();
            }
            //   ddl_bank_name.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        account_no();
   
    
    }
    protected void ddl_bank_name_SelectedIndexChanged(object sender, EventArgs e)
    {

      //  ddl_bank_name.SelectedValue = "";
        ddl_comp_ac_number.Text = "";
        account_no();
    }

    protected void account_no() 
    {
        try
        {
            d.con.Open();
            //ddl_bank_name.Text = "";
            ddl_comp_ac_number.Text = "";
            MySqlCommand cmd = null;
            if (ddl_pmt_recived.SelectedValue == "0")
            {
                cmd = new MySqlCommand("Select payment_ag_bank, company_ac_no from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "' and payment_ag_bank = '" + ddl_bank_name.SelectedValue + "'", d.con);
            }
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ddl_bank_name.SelectedValue = dr.GetValue(0).ToString();
                ddl_comp_ac_number.Text = dr.GetValue(1).ToString();
            }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    }
    //protected void gv_minibank_SelectedIndexChanged1(object sender, EventArgs e)
  //  {
    
   // }
    protected void lnk_remove_manual_other_Click(object sender, EventArgs e)
    {
        int result = 0, result1 = 0;
        try
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

            result = d.operation("UPDATE pay_report_gst SET billing_amt = 0, received_amt = 0, `receipt_de_reasons`='',`receipt_de_approve`='0', tds_amount = 0, adjustment_amt = 0, adjustment_sign = 0, received_date = NULL, total_received_amt = 0, payment_id = 0, uploaded_by = NULL, uploaded_date = NULL,received_original_amount= 0 WHERE  Invoice_No = '" + grdrow.Cells[7].Text + "'");
            result1 = d.operation("delete  from pay_report_gst where Invoice_No = '" + grdrow.Cells[7].Text + "' and id= '" + grdrow.Cells[2].Text + "' and amount = 0 and (igst= 0 || cgst=0 || igst= 0 )");
            if (result > 0 || result1 > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deleted Succsefully !!!')", true);
                load_gv_payment("");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Deletion Failed !!!')", true);
                load_gv_payment("");
              }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    }

    protected void client_name()
    {

        ddl_client_gv.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_gv.DataSource = dt_item;
                ddl_client_gv.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_gv.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_gv.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_client_gv.Items.Insert(0, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_client_gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        load_gv_payment("");
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        load_gv_payment("");
    }
    protected void display_close_date()
    {
        d.con.Open();
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        try
        {
            DataSet ds_status = new DataSet();
           // MySqlDataAdapter dt_status = new MySqlDataAdapter("SELECT client_name  ,receive_date,   REMANING_AMOUNT  FROM (SELECT pay_minibank_master  .  ID  , pay_minibank_master  .  client_name   AS 'client_Name', DATE_FORMAT(  receive_date  , '%d-%m-%Y') AS 'receive_date', Amount   AS 'Credit Amount', ROUND(IFNULL(SUM(  pay_report_gst  .  received_amt  ), 0), 2) AS ' SETTLED_AMOUNT', ROUND(  Amount   - (IFNULL(SUM(  pay_report_gst  .  received_amt  ), 0)), 2) AS 'REMANING_AMOUNT' FROM pay_minibank_master   LEFT JOIN   pay_report_gst  ON  pay_report_gst . payment_id  =  pay_minibank_master . id  WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and `pay_minibank_master`.`client_code`='" + ddl_client.SelectedValue + "' GROUP BY pay_minibank_master . id ) AS t1 WHERE REMANING_AMOUNT  < 0.99", d.con);
            MySqlDataAdapter dt_status = new MySqlDataAdapter("select  client_name, DATE_FORMAT(`receive_date`, '%d-%m-%Y') as 'receive_date',REMANING_AMOUNT from(SELECT pay_minibank_master.ID,pay_minibank_master.client_name,receive_date, ROUND(pay_minibank_master.Amount - (IFNULL(SUM(pay_report_gst.received_amt), 0)), 2) AS 'REMANING_AMOUNT' FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_report_gst.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and `pay_minibank_master`.`client_code`='" + ddl_client.SelectedValue + "' GROUP BY pay_minibank_master.receive_date) as t1 where REMANING_AMOUNT <0.99 ", d.con);
			dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                gv_links.DataSource = ds_status;
                gv_links.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void btn_approve_minibank_Click(object sender, EventArgs e)
    {
        try {
            string record_save = null;


            if (ddl_pmt_recived.SelectedValue == "0")
            {
                record_save = d.getsinglestring("select client_code,`Bank_name`,`Account_number`,`Amount` from pay_minibank_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "' and bank_name = '" + ddl_bank_name.SelectedValue + "' and `Account_number` = '" + ddl_comp_ac_number.Text + "' and `client_bank_name` = '" + ddl_client_bank.SelectedValue + "' and `client_ac_number` ='" + ddl_client_ac_number.SelectedValue + "' and `Amount` ='" + txt_minibank_amount.Text + "' and `receive_date`= str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') and `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "' and `Utr_no`='" + txt_minibank_utr_no.Text + "' and uploaded_by = '" + Session["LOGIN_ID"].ToString() + "' and `payment_type` ='" + ddl_payment_type.SelectedValue + "' and `received_from` = '0' ");

                if (record_save == "")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit Record First')", true);
                    return;


                }
            }
            else
                
            if (ddl_pmt_recived.SelectedValue=="1")
            
            {
                record_save = d.getsinglestring("select `Account_number`,`Amount`,`receive_date`,`description`,`uploaded_by`  from pay_minibank_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_name = '" + ddl_other.SelectedValue + "'  and `Account_number` = '" + ddl_comp_ac_number.Text + "'  and `Amount` ='" + txt_minibank_amount.Text + "' and `receive_date`=str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') and `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "' and `Utr_no`='" + txt_minibank_utr_no.Text + "' and uploaded_by = '" + Session["LOGIN_ID"].ToString() + "' and `received_from` = '1' ");

                if (record_save == "")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit Record First')", true);
                    return;


                }

            }



            int result = 0;

            if (ddl_pmt_recived.SelectedValue=="0")
            {
                result = d.operation("update pay_minibank_master set receipt_approve = '1' , receipt_reasons ='' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_minibank_client.SelectedValue + "' and bank_name = '" + ddl_bank_name.SelectedValue + "' and Account_number = '" + ddl_comp_ac_number.Text + "' and client_bank_name = '" + ddl_client_bank.SelectedValue + "' and client_ac_number ='" + ddl_client_ac_number.SelectedValue + "' and `Amount` ='" + txt_minibank_amount.Text + "' and `receive_date`= str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') and `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "' and `Utr_no`='" + txt_minibank_utr_no.Text + "' and uploaded_by = '" + Session["LOGIN_ID"].ToString() + "' and payment_type ='" + ddl_payment_type.SelectedValue + "' ");
            }

            else if (ddl_pmt_recived.SelectedValue == "1")
            {
                //comp_code,client_name,Bank_name,Account_number,receive_date,description,Amount,uploaded_by,uploaded_date,Mode_of_transfer,Cheque,Utr_no

                result = d.operation("update pay_minibank_master set receipt_approve = '1' ,receipt_reasons = '' where comp_code = '" + Session["comp_code"].ToString() + "' and client_name = '" + ddl_other.SelectedValue + "'  and Account_number = '" + ddl_comp_ac_number.Text + "'  and Amount ='" + txt_minibank_amount.Text + "' and receive_date =str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') and Mode_of_transfer ='" + ddl_mode_transfer.SelectedValue + "' and Utr_no ='" + txt_minibank_utr_no.Text + "' and uploaded_by = '" + Session["LOGIN_ID"].ToString() + "'  ");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved Succesfully !!');", true);

            comp_data();
            mini_text_clear();

            ddl_pmt_recived.SelectedValue = "Select";
            ddl_minibank_client.SelectedValue = "Select";
            ddl_bank_name.Items.Clear();
         ddl_other_bank.Items.Clear();
            ddl_payment_type.SelectedValue = "Select";
        }
        catch (Exception ex) { throw ex; }
        finally{}
    }
    protected void btn_approve_receipt_de_Click(object sender, EventArgs e)
    {
        try {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string inlist = "";

            foreach (GridViewRow gvrow in gv_invoice_pmt.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string invoice_no = gv_invoice_pmt.Rows[gvrow.RowIndex].Cells[1].Text;

                TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_recive_amt");
                string receive_amt = (txt_returnqty.Text);

                TextBox receive_date1 = (TextBox)gvrow.FindControl("txt_reciving_date");
                string receive_date = (receive_date1.Text);

                

                //var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                //if (checkbox.Checked == true)
                //{

                //    inlist = "" + invoice_no+ "";
                //}


                string receipt_details = d.getsinglestring("select distinct invoice_no from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and `Invoice_No`='" + invoice_no + "' and `received_amt` = '" + receive_amt + "'");


                if (receipt_details=="") {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit Record First')", true);

                    return;
                
                
                }


                int result = 0;

             //  
                result = d.operation("update pay_report_gst set receipt_de_approve = '1',`receipt_de_reasons`='' where comp_code='" + Session["comp_code"].ToString() + "' and Invoice_No ='" + invoice_no + "' and received_amt = '" + receive_amt + "' and `received_date` = str_to_date('" + txt_date.SelectedValue + "','%d-%m-%Y') ");

              

              
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Approve  Successfully !!!')", true);
        
        }
        catch (Exception ex) { throw ex; }
       finally{
           tran_clear();
           //panel2.Visible = true;
           Panel_gv_pmt.Visible = false;
        
        
        
        
        }


    }

    protected void client_bank_name()
    {

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;

        if (ddl_pmt_recived.SelectedValue == "1")
        {
            cmd_item = new MySqlDataAdapter("select client_bank_name from pay_other_client_master where client_code='" + ddl_minibank_client.SelectedValue + "' ", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE ='" + ddl_minibank_client.SelectedValue + "' ", d.con);
        }
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_bank.DataSource = dt_item;
                ddl_client_bank.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_bank.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_bank.DataBind();
            }
           
            dt_item.Dispose();
            //    ddl_bank_name.Readonly=true;
            ddl_comp_ac_number.ReadOnly = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        comp_bank_details();

    
    
    
    
    
    }

    protected void btn_edit_other1_Click(object sender, EventArgs e)
    
    {
        MySqlDataReader dr2 = null;
         GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[4].Text;

        btn_update_receipt.Visible = true;
        Button1.Visible = false;

        string received_from_to = d.getsinglestring("select received_from from pay_minibank_master where id = '"+id+"'");

        // for other edit 
        if(received_from_to=="1"){
       
        
        MySqlCommand cmd2 = new MySqlCommand("SELECT `received_from`, `client_name`, `description`,`amount`, DATE_FORMAT( receive_date , '%d/%m/%Y') as 'receive_date', `Mode_of_transfer`, `Utr_no`, id FROM `pay_minibank_master` WHERE  `id` = '" + id + "'", d3.con);

        d3.con.Open();
        try
        {
          dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {


                ddl_pmt_recived.SelectedValue = dr2.GetValue(0).ToString();
                ddl_pmt_recived_SelectedIndexChanged(null, null);

                ddl_other.SelectedValue = dr2.GetValue(1).ToString();
                txt_description.Text = dr2.GetValue(2).ToString();
                txt_minibank_amount.Text = dr2.GetValue(3).ToString();
                txt_minibank_received_date.Text = dr2.GetValue(4).ToString();
                ddl_mode_transfer.SelectedValue = dr2.GetValue(5).ToString();
                txt_minibank_utr_no.Text = dr2.GetValue(6).ToString();
                txt_id.Text = dr2.GetValue(7).ToString();
              
            }
            dr2.Close();

         

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally { }
        }
            // for client edit
        else if (received_from_to == "0")
        {

            MySqlCommand cmd2 = new MySqlCommand("select distinct `received_from` ,client_code,`payment_type`,`client_bank_name`,`client_ac_number`,amount, DATE_FORMAT( receive_date , '%d/%m/%Y') as 'receive_date',`Mode_of_transfer`,`Utr_no`,id from pay_minibank_master  WHERE  `id` = '" + id + "'", d3.con);

            d3.con.Open();
            try
            {
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {


                    ddl_pmt_recived.SelectedValue = dr2.GetValue(0).ToString();
                    ddl_pmt_recived_SelectedIndexChanged(null, null);

                    ddl_minibank_client.SelectedValue = dr2.GetValue(1).ToString();
                   // ddl_minibank_client_SelectedIndexChanged(null,null);

                   // comp_bank_details();
                   
                 ddl_payment_type.SelectedValue = dr2.GetValue(2).ToString();       
                ddl_payment_type_SelectedIndexChanged(null, null);
                client_bank_name();
                 ddl_client_bank_SelectedIndexChanged(null,null);
                    //ddl_client_bank.SelectedValue = dr2.GetValue(3).ToString();

                    //ddl_client_ac_number.SelectedValue= dr2.GetValue(4).ToString();
                // bank_name_ac_no();
                    txt_minibank_amount.Text = dr2.GetValue(5).ToString();
                    txt_minibank_received_date.Text = dr2.GetValue(6).ToString();

                    ddl_mode_transfer.SelectedValue = dr2.GetValue(7).ToString();
                    txt_minibank_utr_no.Text = dr2.GetValue(8).ToString();
                    txt_id.Text = dr2.GetValue(9).ToString();
                }
                dr2.Close();


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally { }

        }

    
    }
    protected void btn_update_receipt_Click(object sender, EventArgs e)
    {
        try 
        {
            int result = 0;

            if (ddl_pmt_recived.SelectedValue == "0")
            {
                result = d.operation("update pay_minibank_master set received_from = '" + ddl_pmt_recived.SelectedValue + "', client_code='" + ddl_minibank_client.SelectedValue + "' , `Bank_name` = '" + ddl_bank_name.SelectedValue + "', `Account_number`='" + ddl_comp_ac_number.Text + "',`payment_type` = '" + ddl_payment_type.SelectedValue + "',client_bank_name ='" + ddl_client_bank.SelectedValue + "',`client_ac_number`='" + ddl_client_ac_number.SelectedValue + "', amount = '" + txt_minibank_amount.Text + "',`receive_date` = str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y') , `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "', `Utr_no`='" + txt_minibank_utr_no.Text + "', uploaded_by = '" + Session["LOGIN_ID"].ToString() + "'  where id = '"+txt_id.Text+"' ");
            }

            else if (ddl_pmt_recived.SelectedValue == "1")
            {
                //comp_code,client_name,Bank_name,Account_number,receive_date,description,Amount,uploaded_by,uploaded_date,Mode_of_transfer,Cheque,Utr_no

                result = d.operation("update pay_minibank_master set client_name = '" + ddl_other.SelectedValue + "' , `Account_number` = '" + ddl_comp_ac_number.Text + "' , `Amount` ='" + txt_minibank_amount.Text + "', `receive_date`=str_to_date('" + txt_minibank_received_date.Text + "','%d/%m/%Y'), `Mode_of_transfer` ='" + ddl_mode_transfer.SelectedValue + "' ,`Utr_no`='" + txt_minibank_utr_no.Text + "', uploaded_by = '" + Session["LOGIN_ID"].ToString() + "'  where id = '" + txt_id.Text + "' ");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Succesfully !!');", true);

            comp_data();
           // mini_text_clear();
        
        
        }
        catch (Exception ex) { throw ex; }
        finally{}
    }
}


