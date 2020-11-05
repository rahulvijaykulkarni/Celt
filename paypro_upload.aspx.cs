using System;
using System.Web.UI;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Threading;
using System.Net.Mail;
using System.Collections.Generic;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;

public partial class Billing_rates : System.Web.UI.Page
{
    DAL d = new DAL();
    string comp_code = null, user_name = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }
        if (!IsPostBack)
        {
            // fill_gridview();
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
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
           
           
        }
        comp_code = Session["COMP_CODE"].ToString();
        user_name = Session["USERNAME"].ToString();
        chalan_ESIC.Visible = false;
        state_ddl.Visible = false;
        client_ddl.Visible = false;
        chalan_PF.Visible = false;
    }
   
    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //need to work
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    string FolderPath = "~/Temp_images/";
                    FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    btn_Import_Click(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Paypro File Uploaded Successfully...');", true);
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
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;

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

        //if (!chk_excel(dt))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PayPro file does not match with payment Annexure !!!');", true);
        //    return;
        //}

        //Push Datatable to database
        DataTable table2 = new DataTable("emp");
        table2.Columns.Add("bene_name");
        table2.Columns.Add("payee_name");
        table2.Columns.Add("bene_account_no");
        table2.Columns.Add("amount_payable");
        table2.Columns.Add("paid_batch_no");
        table2.Columns.Add("Comments");
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                string bank_code = "", comments = "Not Found";

                try
                {
                    //0 == HOLD; 1 == Paid; 2 == Returned
                    int status = 0;
                    int res = 0;
                    // 0 == unpaid , 1 == Paid;
                    int branch_email_status = 0;
                    if (r[11].ToString() != "") { bank_code = r[11].ToString(); }

                    if (r[1].ToString().Trim() == "Corporate Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This is new Paypro file format use new process button.');", true);
                        return;
                    }
                    if (bank_code.Trim() != "" && !bank_code.ToUpper().Contains("BENE"))
                    {

                        if (r[14].ToString() != "")
                        {
                            if (r[14].ToString().ToUpper() == "PAID")
                            { status = 1; branch_email_status = 1; }
                            if (r[14].ToString().ToUpper() == "RETURNED" || r[14].ToString().ToUpper() == "UNPAID")
                            { status = 2; }
                        }
                        try
                        {
                            string amount = "0";
                            if (r[13].ToString().Contains("."))
                            {
                                amount = r[13].ToString().Substring(0, r[13].ToString().IndexOf("."));
                            }
                            else
                            {
                                amount = r[13].ToString();
                            }
                            amount = amount.Replace(",", "");
                            //if (d.getsinglestring("select payment_status from pay_pro_master where BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and floor(Payment) = " + amount + " and comp_code='" + Session["COMP_CODE"].ToString() + "' and MONTH = " + txt_month_year.Text.Substring(0, 2) + " AND YEAR=" + txt_month_year.Text.Substring(3) + "").Equals("1"))
                            //{
                            //    res = 1;
                            //}
                            //else
                            //{
                            if (ddl_upload_type.SelectedValue == "Payment")
                            {
                                //manpower
                                res = d.operation("update pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year set payment_status=" + status + ", paypro_batch_id = '" + r[1].ToString() + "', salary_date= str_to_date('" + r[17].ToString() + "','%d-%b-%y'), branch_email = '" + branch_email_status + "'  where pay_pro_master.branch_email != 2 and pay_pro_master.BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and (FLOOR(pay_pro_master.Payment) - (pay_pro_master.fine + pay_pro_master.EMP_ADVANCE_PAYMENT)) + (pay_billing_unit_rate_history.conveyance_amount / pay_billing_unit_rate_history.month_days * tot_days_present) = " + amount + " and pay_pro_master.comp_code='" + comp_code + "' and pay_pro_master.MONTH = " + txt_month_year.Text.Substring(0, 2) + " AND pay_pro_master.YEAR=" + txt_month_year.Text.Substring(3));
                                // }
                            }
                            //vendor
                            else if (ddl_upload_type.SelectedValue == "Vendor")
                            {
                                res = d.operation("update pay_pro_vendor  set payment_status=" + status + ", paypro_batch_id = '" + r[1].ToString() + "', salary_date= str_to_date('" + r[17].ToString() + "','%d-%b-%y')  where  BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and FLOOR(grand_total) = " + amount + " and comp_code='" + comp_code + "' AND month_year = substring('" + txt_month_year.Text + "',2,6)");
                            }
                            //Conveyance
                            else if (ddl_upload_type.SelectedValue == "Conveyance")
                            {
                                res = d.operation("update pay_pro_material_history  set payment_status=" + status + ", paypro_batch_id = '" + r[1].ToString() + "',  payment_date= str_to_date('" + r[17].ToString() + "','%d-%b-%y')  where  BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and FLOOR(conveyance_amount) = " + amount + "  AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' and type='Conveyance' ");
                            }
                            //Material
                            else if (ddl_upload_type.SelectedValue == "Material")
                            {
                                res = d.operation("update pay_pro_material_history inner join  pay_material_details on pay_pro_material_history.month=pay_material_details.month and pay_pro_material_history.year=pay_material_details.year  set payment_status=" + status + ", paypro_batch_id = '" + r[1].ToString() + "',  payment_date= str_to_date('" + r[17].ToString() + "','%d-%b-%y')  where  BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and FLOOR(pay_pro_material_history.material_amount) = " + amount + "  AND pay_pro_material_history.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_pro_material_history.year='" + txt_month_year.Text.Substring(3) + "' and pay_pro_material_history.type='Material' ");
                            }
                            if (res > 0)
                            {
                                comments = "Success";
                            }
                            else
                            {
                                if (ddl_upload_type.SelectedValue == "Payment")
                                {
                                    if (!d.getsinglestring("select payment_status from pay_pro_master where BANK_EMP_AC_CODE = '" + bank_code.Trim() + "' and floor(Payment) = " + amount + " and comp_code='" + comp_code + "' and MONTH = " + txt_month_year.Text.Substring(0, 2) + " AND YEAR=" + txt_month_year.Text.Substring(3) + "").Equals("1"))
                                    {
                                        table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), "Salary Amount / Account Number not matching in system");
                                    }
                                }
                                if (ddl_upload_type.SelectedValue == "Vendor")
                                {
                                    if (!d.getsinglestring("select payment_status from pay_pro_vendor where BANK_EMP_AC_CODE = '" + bank_code.Trim() + "' and floor(grand_total) = " + amount + " and comp_code='" + comp_code + "' and month_year = '" + txt_month_year.Text + "'").Equals("1"))
                                    {
                                        table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), "Salary Amount / Account Number not matching in system");
                                    }
                                }
                                if (ddl_upload_type.SelectedValue == "Conveyance")
                                {
                                    if (!d.getsinglestring("select payment_status from pay_pro_material_history where BANK_EMP_AC_CODE = '" + bank_code.Trim() + "' and floor(conveyance_amount) = " + amount + "  and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' and type='Conveyance'").Equals("1"))
                                    {
                                        table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), "Salary Amount / Account Number not matching in system");
                                    }
                                }
                                if (ddl_upload_type.SelectedValue == "Material")
                                {
                                    if (!d.getsinglestring("select payment_status from pay_pro_material_history where BANK_EMP_AC_CODE = '" + bank_code.Trim() + "' and floor(material_amount) = " + amount + "  and month = '" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "' and type='Material'").Equals("1"))
                                    {
                                        table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), "Salary Amount / Account Number not matching in system");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), ex.Message.ToString());
                        }
                        try
                        {
                            d.operation("insert into paypro_uploaded_data (comp_code, month, year, batch_no, corporate_ref_no, next_working_day_date, corporate_product_name, payment_method, debit_ac_no, corporate_account_description, bene_code, bene_name, payee_name, bene_ac_no, amount_payable_currency, amount_payable, transaction_status, instrumentno, instrument_date, paid_date, payee_location, paid_location,paid_batch_no, activation_date, bene_mobile_no, payable_branch, identifications_documents, document_id, payout_mode, finacle_cheque_no, comments, uploaded_by, datetime) values ('" + comp_code + "','" + txt_month_year.Text.Substring(0, 2) + "','" + txt_month_year.Text.Substring(3) + "','" + r[1].ToString() + "','" + r[2].ToString() + "','" + r[3].ToString() + "','" + r[4].ToString() + "','" + r[5].ToString() + "','" + r[6].ToString() + "','" + r[7].ToString() + "','" + r[8].ToString() + "','" + r[9].ToString() + "','" + r[10].ToString() + "','" + r[11].ToString() + "','" + r[12].ToString() + "','" + r[13].ToString() + "','" + r[14].ToString() + "','" + r[15].ToString() + "','" + r[16].ToString() + "','" + r[17].ToString() + "','" + r[18].ToString() + "','" + r[19].ToString() + "','" + r[20].ToString() + "','" + r[21].ToString() + "','" + r[22].ToString() + "','" + r[23].ToString() + "','" + r[24].ToString() + "','" + r[25].ToString() + "','" + r[26].ToString() + "','" + r[27].ToString() + "','" + comments + "','" + user_name + "',now())");
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), "Data Error : " + ex.Message);
                        }
                        finally { }
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
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file(ds);
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
        fill_gridview();
    }
    private void send_file(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Paypro_issue.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, 2);
            Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, 2);
            Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, 2);
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
    private void fill_gridview()
    {
        d.con1.Open();
        try
        {
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1;
            adp1 = new MySqlDataAdapter("select MONTH, YEAR, bene_name as 'EMP NAME', batch_no as 'BATCH NO', bene_ac_no as 'EMP ACCT', transaction_status as STATUS, Amount_payable as AMOUNT, paid_date as 'PAID DATE', comments as COMMENTS, uploaded_by as 'UPLOADED BY', date_format(datetime,'%d/%m/%Y %h:%i:%s %p') as 'DATE TIME' from paypro_uploaded_data where comp_code = '" +comp_code + "' AND MONTH = '" + txt_month_year.Text.Substring(0, 2) + "' AND YEAR = '" + txt_month_year.Text.Substring(3) + "'", d.con1);
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
    protected void gv_fullmonthot_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_fullmonthot.UseAccessibleHeader = false;
            gv_fullmonthot.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void btn_excel_Click(object sender, EventArgs e)
    {

        MySqlDataAdapter adp;

        try
        {
            d.con1.Open();
            adp = new MySqlDataAdapter("SELECT client as client_name, UNIT_NAME AS 'branch_name', EMP_NAME AS 'EMP_NAME', case when payment_status = 2 then 'Returned' else case when payment_status = 1 then 'Paid' else 'In Process' end end as 'Salary Status' FROM pay_pro_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND MONTH = '" + txt_month_year.Text.Substring(0, 2) + "' AND YEAR = '" + txt_month_year.Text.Substring(3) + "' ORDER BY 1", d.con1);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Employee_Salary_Status_" + txt_month_year.Text.Substring(0, 2) + "_" + txt_month_year.Text.Substring(3) + ".xls");
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
                d.con1.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
        }

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
        return "MONTH";

    }

    public class MyTemplate : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i = 0;
        static int ctr;
        public MyTemplate(ListItemType type, DataSet ds, int i)
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
                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th> CLIENT_NAME </th><th> BRANCH_NAME</th><th>EMPLOYEE NAME</th><th>SALARY_STATUS</th>");
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BENE NAME</th><th>PAYEE NAME</th><th>BENE ACCOUNT NO.</th><th>AMOUNT PAYABLE</th><th>PAID BATCH NO.</th><th>Comments</th></tr>");

                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["branch_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Salary Status"] + " </td>");
                    }
                    else
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["bene_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["payee_name"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["bene_account_no"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["amount_payable"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["paid_batch_no"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Comments"].ToString().ToUpper() + "</td></tr>");
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


    protected void gv_fullmonthot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Text = getmonth(e.Row.Cells[0].Text);
    }
    //protected void btn_save_history_Click(object sender, EventArgs e)
    //{
    //    d.operation("insert into paypro_uploaded_files (comp_code,datetime,request,amount) values('"+Session["COMP_CODE"].ToString()+"',str_to_date('"+txt_activation_date.Text+"','%d/%m/%Y'),'"+txt_request.Text+"','"+txt_amount.Text+"')");
    //    //txt_amount.Text = "0";
    //    //txt_request.Text = "0";
    //    //txt_activation_date.Text = "";
    //}
    private bool chk_excel(System.Data.DataTable dt)
    {
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                string bank_code = "";

                try
                {
                    if (r[11].ToString() != "") { bank_code = r[11].ToString(); }

                    if (bank_code.Trim() != "" && !bank_code.ToUpper().Contains("BENE"))
                    {
                        try
                        {
                            string amount = "0";
                            if (r[13].ToString().Contains("."))
                            {
                                amount = r[13].ToString().Substring(0, r[13].ToString().IndexOf("."));
                            }
                            else
                            {
                                amount = r[13].ToString();
                            }
                            amount = amount.Replace(",", "");

                            if (ddl_upload_type.SelectedValue == "Vendor")
                            {
                                string vendor_id = d.getsinglestring("select vendor_id from pay_pro_vendor where BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and floor(grand_total) = " + amount + " and comp_code='" + Session["COMP_CODE"].ToString() + "' and month_year = '" + txt_month_year.Text + "'");
                                if (vendor_id == "")
                                {
                                    return false;

                                }
                            }
                            else
                            {
                                string emp_code = d.getsinglestring("select emp_code from pay_pro_master where BANK_EMP_AC_CODE= '" + bank_code.Trim() + "' and floor(Payment) = " + amount + " and comp_code='" + Session["COMP_CODE"].ToString() + "' and MONTH = " + txt_month_year.Text.Substring(0, 2) + " AND YEAR=" + txt_month_year.Text.Substring(3));
                                if (emp_code == "")
                                {
                                    return false;

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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

        return true;
    }
    protected void btn_send_email_Click(object sender, EventArgs e)
    {
        create_salary_pdf("", txttodate.Text.Substring(0, 2), txttodate.Text.Substring(3), 1);
    }
    private void create_salary_pdf(string unit_code, string month, string year, int counter1)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            string where = "client_code = '" + ddl_client.SelectedValue + "' and month = " + month + " and year = " + year;
            if (unit_code == "")
            {
                if (ddl_state.SelectedValue != "ALL") { where = where + " and state_name = '" + ddl_state.SelectedValue + "'"; }
                if (ddlunitselect.SelectedValue != "ALL") { where = where + " and unit_code = '" + ddlunitselect.SelectedValue + "'"; }
            }
            else
            {
                where = "comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code + "' and month = " + month + " and year = " + year;
            }
            d.con1.Open();
            string branch_email = "branch_email = 1";
            if (counter1 == 3 || counter1 == 4)
            {
                branch_email = "branch_email in (1,2)";
            }
            MySqlCommand cmd = new MySqlCommand("SELECT comp_code, unit_code, month, year, unit_city,state_name FROM pay_pro_master WHERE " + where + "  AND paypro_batch_id IS NOT NULL AND "+branch_email+" AND employee_type IN ('Temporary', 'Permanent') group BY unit_code", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows && !counter1.Equals(3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Salary Slip already sent !!');", true);
                return;
            }
            while (dr.Read())
            {
                ReportDocument crystalReport = new ReportDocument();
                string query = null;

                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                string month_name = mfi.GetMonthName(int.Parse(dr.GetValue(2).ToString())).ToString();

                string month_year = month_name + " " + dr.GetValue(3).ToString();

                // string thisMonth = ddl_currmon.SelectedItem.Text;
                crystalReport.Load(Server.MapPath("~/Salary_Slip.rpt"));
                crystalReport.DataDefinition.FormulaFields["salary_monthyear"].Text = @"'" + month_year + "'";

                string unit_code1 = "unit_code = '" + dr.GetValue(1).ToString() + "'";
                if(ddlunitselect.SelectedValue=="ALL" && counter1==3)
                {
                    unit_code1 = "state_name = '" + ddl_state.SelectedValue + "'";
                }


                query = "SELECT pay_pro_master.comp_code, COMPANY_NAME, COMP_ADDRESS1 as 'ADDRESS1', COMP_ADDRESS2 As 'ADDRESS2', COMP_CITY AS  'CITY', COMP_STATE as 'STATE', state_name as 'UnitState', unit_city as 'Unit_City', client as 'Client', grade, unit_code as 'Unitcode', ihms as 'ihms_code', Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, if(PAN_No is null or PAN_No='','IN PROCESS',PAN_No) AS 'UAN_No', if(PF_No is null or PF_No='','IN PROCESS',PF_No) AS PF_No,date_format(salary_date,'%d/%m/%Y') as  'PAN_No', if(ESI_No is null or ESI_No='','IN PROCESS',ESI_No) AS ESI_No, PerDayRate, Basic, Vda, emp_basic_vda AS 'basic_vda', hra_amount_salary AS 'hra', sal_bonus_gross AS 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross AS 'leaveDays', washing_salary AS 'washing', travelling_salary AS 'travelling', education_salary AS 'education', allowances_salary AS 'special_allo', cca_salary AS 'cca', other_allow AS 'other_allo', gratuity_gross AS 'Gratuity_taxable', gratuity_after_gross AS 'Gratuity', sal_pf AS 'PF', sal_esic AS 'ESIC', sal_ot AS 'ot_amount_salary', lwf_salary AS 'lwf', sal_uniform_rate AS 'Uniform', PT_AMOUNT AS 'pt', fine, advance_payment_mode AS 'advance', Total_Days_Present, Payment, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, month_days as 'Working_Days', Bonus_Policy, ot_rate , esic_ot_applicable AS 'ESIC_OT', ot_hours, common_allow AS 'EMP_specialallow' FROM pay_pro_master where client_code='" + ddl_client.SelectedValue + "' and comp_code = '" + dr.GetValue(0).ToString() + "' and " + unit_code1 + " and month = '" + dr.GetValue(2).ToString() + "' and year = '" + dr.GetValue(3).ToString() + "' and " + branch_email + " AND employee_type IN ('Temporary', 'Permanent')";

                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlCommand cmd1 = new MySqlCommand(query);
                MySqlDataReader sda = null;
                cmd1.Connection = d.con;
                d.con.Open();
                sda = cmd1.ExecuteReader();
                dt.Load(sda);
                d.con.Close();
                string body = "";
                string body1 = "";
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir/Madam, <p>Please find the attached salary slip for the following employees. </FONT></FONT></FONT></B><p><Table border =1><tr><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>SR. No.</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>EMPLOYEE NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>DESIGNATION</FONT></FONT></FONT></th></tr>";
                    foreach (DataRow row in dt.Rows)
                    {
                        body = body + "<tr><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + ++i + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["EMP_NAME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["grade"].ToString() + "</FONT></FONT></FONT></td></tr>";
                    }
                    body1 = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Note:- IH&MS also provide salary slip on android app of an employee. </FONT></FONT></FONT></B><p>";
                    body = body + "</Table> <p>" + body1;
                    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
                    string companyimagepath = path + d.getsinglestring("SELECT comp_logo from pay_company_master where comp_code = '" + dr.GetValue(0).ToString() + "'");
                    crystalReport.Refresh();
                    crystalReport.SetDataSource(dt);
                    crystalReport.DataDefinition.FormulaFields["company_image_path"].Text = @"'" + companyimagepath + "'";
                    if (Session["COMP_CODE"].ToString().Equals("C01")) { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path + "C01_stamp.jpg" + "'"; }
                    else { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path + "C02_stamp.png" + "'"; }
                    if (counter1 == 3)
                    {
                        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "Salary_slip.pdf");
                          return;
                    }
                    
                    crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Salary_slip.pdf");
                    crystalReport.Close();
                    crystalReport.Clone();
                    crystalReport.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    try
                    {
                        d.con.Open();
                        MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;select cast(group_concat(distinct head_email_id) as char), head_name, client_code,comp_code,state from pay_branch_mail_details where comp_code = '" + dr.GetValue(0).ToString() + "' and unit_code = '" + dr.GetValue(1).ToString() + "'", d.con);
                        MySqlDataReader drnew = cmdnew.ExecuteReader();
                        System.Data.DataTable DataTable1 = new System.Data.DataTable();
                        DataTable1.Load(drnew);
                        d.con.Close();
                        foreach (DataRow row in DataTable1.Rows)
                        {
                            if (!row[0].ToString().Equals(""))
                            {
                                mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "IH&MS - Salary Slip of " + dr.GetValue(4).ToString() + " for the month of " + month_year, dr.GetValue(5).ToString(), dr.GetValue(1).ToString(), month, year, counter1, body);
                            }
                            else
                            {
                                insert_email_issue(ddl_client.SelectedValue, dr.GetValue(5).ToString(), dr.GetValue(1).ToString(), "NO Branch Email ID.");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch email id not Present !!');", true);

                            }
                        }

                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con.Close(); }
                }
            }
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            d.con1.Close();
            File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Salary_slip.pdf");
        }
    }
    protected void mail_send(string head_email_id, string head_email_name, string client_name, string comp_code, int counter, string subject, string state_name, string unit_code, string month, string year, int counter1, string body1)
    {
        List<string> list1 = new List<string>();
        string from_emailid = "", password = "";
        try
        {
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
                string body = string.Empty;
                if (counter == 1)
                {
                    using (StreamReader reader = new StreamReader("morning_email.htm"))
                    {
                        body = reader.ReadToEnd();
                    }
                    if (comp_code.Equals("C02"))
                    {
                        body = body.Replace("{company}", "INTERNATIONAL HOUSEKEEPING & MAINTENANCE SERVICES");
                    }
                    else
                    {
                        body = body.Replace("{company}", "IH&MS INTEGRATED SOLUTIONS INDIA PVT. LTD.");
                    }
                }
                else if (counter == 3)
                {
                    body = body1;
                }
				string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Account' and client_code='" + client_name + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />" + name + "";

                //if (client_name == "BALIC")
               // {
               //     body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />Santosh Ghurade<br />Admin and OPS<br />Mobile - 9325431471<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "BAGIC")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />Samiksha<br />Admin and OPS<br />Mobile - 9067159872<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                ///}
                //else if (client_name == "MAX" || client_name == "AEG" || client_name == "5" || client_name == "7" || client_name == "8" || client_name == "ICICI HK" || client_name == "ESFB" || client_name == "TBZ")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />SNEHAL GHADGE<br />Admin and OPS<br />Mobile - 8308925811<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                ///}
                //else if (client_name == "RLIC HK" || client_name == "RCFL" || client_name == "RCPL")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />CHAITALI<br />Admin and OPS<br />Mobile - 8805814003<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "SUD" || client_name == "UTKARSH" || client_name == "HDFC" || client_name == "TAVISKA" || client_name == "SUN" || client_name == "DAF" || client_name == "TBML" || client_name == "BRLI")
                //{
                 //   body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />SNEHAL GHADGE<br />Admin and OPS<br />Mobile - 8308925811<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}
                //else if (client_name == "4" || client_name == "RBL")
                //{
                //    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />RAHUL<br />Admin and OPS<br />Mobile - 7057919614<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //}

                //string body = "<tr><td style = \"font-family:verdana;font-size:10pt;\">Respected " + head_email_name + ",</p></tr>";
                using (MailMessage mailMessage = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mailMessage.From = new MailAddress(from_emailid);


                    //list1 = head_email_id.Split(',').ToList();
                    //foreach (string mail in list1)
                    //{
                    if (head_email_id != "")
                    {
                        if (counter == 2)
                        {
                            mailMessage.Bcc.Add(head_email_id);
                            mailMessage.Subject = " GOOD MORNING EMAIL NOT SENT TODAY ";
                            mailMessage.Body = "Hi, <p> Good Morning Email not sent due to some technical issue. <p>";
                        }
                        else if (counter == 3)
                        {
                            mailMessage.To.Add(head_email_id);
                            mailMessage.Subject = subject;
                            mailMessage.Body = body;
                            mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Salary_slip.pdf"));
                        }
                        else
                        {
                            mailMessage.Bcc.Add(head_email_id);
                            mailMessage.CC.Add("aksingh@ihms.co.in");
                            mailMessage.Subject = " GOOD MORNING ";
                            mailMessage.Body = body;
                        }
                        mailMessage.IsBodyHtml = true;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                        SmtpServer.EnableSsl = true;
                        try
                        {
                            SmtpServer.Send(mailMessage);
                            d.operation("update pay_pro_master set branch_email =2 where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code + "' and month = '" + month + "' and year = '" + year + "' and branch_email = 1 AND employee_type IN ('Temporary', 'Permanent')");
                            d.operation("delete from send_email_issue where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unit_code + "' and month = '" + month + "' and year = '" + year + "'");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Sent successfully!!');", true);

                        }
                        catch
                        {
                            if (counter1 == 1)
                            {
                                insert_email_issue(ddl_client.SelectedValue, state_name, unit_code, "No Internet Connection.");
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Not Sent!!');", true);

                        }
                        Thread.Sleep(500);
                    }
                    //}
                    // }
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
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_client.SelectedValue != "Select")
        {
            ddlunitselect.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
         //comment 30/09   MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  AND pay_unit_master.branch_status = 0  ORDER BY UNIT_CODE", d.con);
             MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y'))  ORDER BY UNIT_CODE", d.con);
			d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddlunitselect.DataSource = dt_item;
                    ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                    ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                    ddlunitselect.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddlunitselect.Items.Insert(0, "ALL");
                ddlunitselect.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }


            //State
            ddl_state.Items.Clear();
            dt_item = new System.Data.DataTable();
          //comment 30/09  cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY state_name", d.con);
          cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) ORDER BY state_name", d.con);
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
                dt_item.Dispose();
                d.con.Close();
                ddl_state.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_state_SelectedIndexChanged(null, null);
            }
        }
        else
        {
            ddlunitselect.Items.Clear();
            ddl_state.Items.Clear();
        }
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlunitselect.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
  //comment 30/09      MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
     MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) ORDER BY UNIT_CODE", d.con);
	    d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddlunitselect.DataSource = dt_item;
                ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                ddlunitselect.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddlunitselect.Items.Insert(0, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    private void insert_email_issue(string client_code, string state_name, string unit_code, string reason)
    {
        d.operation("insert into send_email_issue (comp_code, client_code, state_name, unit_code, month, year, reason, type,modified_by,modif_datetime) values ('" + Session["COMP_CODE"].ToString() + "','" + client_code + "','" + state_name + "','" + unit_code + "','" + txttodate.Text.Substring(0, 2) + "','" + txttodate.Text.Substring(3) + "','" + reason + "','SALARY','" + Session["LOGIN_ID"].ToString() + "',now())");
    }
    protected void btn_emails_not_sent_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        string where = "client_code = '" + ddl_client.SelectedValue + "' and month = " + txttodate.Text.Substring(0, 2) + " and year = " + txttodate.Text.Substring(3);
        if (ddl_state.SelectedValue != "ALL") { where = where + " and state_name = '" + ddl_state.SelectedValue + "'"; }
        if (ddlunitselect.SelectedValue != "ALL") { where = where + " and unit_code = '" + ddlunitselect.SelectedValue + "'"; }

        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select id, (select client_name from pay_client_master where pay_client_master.client_code=send_email_issue.client_code) as client_code, upper(state_name) as state_name, unit_code as unit_code1, (select unit_name from pay_unit_master where pay_unit_master.unit_code=send_email_issue.unit_code and pay_unit_master.comp_code=send_email_issue.comp_code) as unit_code,  month, year, reason from send_email_issue where " + where + " order by id desc", d.con1);
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
    protected void lnk_send_email_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        create_salary_pdf(grdrow.Cells[7].Text, grdrow.Cells[8].Text, grdrow.Cells[9].Text, 2);
        btn_emails_not_sent_Click(null, null);
    }
    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        d.operation("delete from send_email_issue where id = " + grdrow.Cells[2].Text);
        btn_emails_not_sent_Click(null, null);
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
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[6].Text = getmonth(e.Row.Cells[6].Text);
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
    protected void btn_download_salary_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        create_salary_pdf("", txttodate.Text.Substring(0, 2), txttodate.Text.Substring(3), 3);
    }
    protected void btn_new_save_Click(object sender, EventArgs e)
    {
        //need to work
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    string FolderPath = "~/Temp_images/";
                    FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    if (ddl_bank.SelectedValue == "AXIS BANK")
                    {
                        btn_Import_Click_new(FilePath, Extension, "Yes", FileName);
                    }
                    else if (ddl_bank.SelectedValue == "INDUSIND BANK")
                    {
                        indusind_Import_Click_new(FilePath, Extension, "Yes", FileName);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Paypro File Uploaded Successfully...');", true);
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
    public void btn_Import_Click_new(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;

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

        //if (!chk_excel(dt))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PayPro file does not match with payment Annexure !!!');", true);
        //    return;
        //}

        //Push Datatable to database
        DataTable table2 = new DataTable("emp");
        table2.Columns.Add("bene_name");
        table2.Columns.Add("payee_name");
       
        table2.Columns.Add("bene_account_no");
        table2.Columns.Add("amount_payable");
        table2.Columns.Add("paid_batch_no");
        table2.Columns.Add("Comments");
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                string bank_code = "", comments = "Not Found";

                try
                {
                    //0 == HOLD; 1 == Paid; 2 == Returned
                    int status = 0;
                    int res = 0;
                    // 0 == unpaid , 1 == Paid;
                    int branch_email_status = 0;
                    if (r[3].ToString() != "") { bank_code = r[3].ToString(); }

                    if (r[1].ToString().Trim() == "Batch No")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This is old Paypro file format use process button.');", true);
                        return;
                    }
                    if (bank_code.Trim() != "" && !bank_code.ToUpper().Contains("CORPORATE"))
                    {

                        if (r[15].ToString() != "")
                        {
                            if (r[15].ToString().ToUpper() == "PAID")
                            { status = 1; branch_email_status = 1; }
                            if (r[15].ToString().ToUpper() == "RETURN" || r[15].ToString().ToUpper() == "UNPAID")
                            { status = 2; }
                        }
                        try
                        {
                            string table_name = "";
                            if (ddl_upload_type.SelectedValue == "Payment")
                            {
                                table_name = "pay_pro_master";
                                res = d.operation("update pay_pro_master inner join pay_emp_paypro on pay_pro_master.month=pay_emp_paypro.month and pay_pro_master.year=pay_emp_paypro.year and pay_pro_master.emp_code=pay_emp_paypro.emp_code and pay_pro_no = " + bank_code+" set utr_number='" + r[14].ToString() + "', payment_status=" + status + ", paypro_batch_id = '" + r[4].ToString() + "', salary_date= str_to_date('" + r[16].ToString() + "','%d-%b-%y'), branch_email = '" + branch_email_status + "'  where branch_email != 2");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 0);
                                }
                            }
                            else if (ddl_upload_type.SelectedValue == "Conveyance")
                            {
                                table_name = "pay_pro_material_history";
                                res = d.operation("update pay_pro_material_history inner join pay_emp_paypro on pay_pro_material_history.month=pay_emp_paypro.month and pay_pro_material_history.year=pay_emp_paypro.year and pay_pro_material_history.emp_code=pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " set utr_number='" + r[14].ToString() + "', payment_status=" + status + ", paypro_batch_id = '" + r[4].ToString() + "', payment_date= str_to_date('" + r[16].ToString() + "','%d-%b-%y')");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 1);
                                }
                            }
                            else if (ddl_upload_type.SelectedValue == "Vendor")
                            {
                                table_name = "pay_pro_vendor";
                                res = d.operation("UPDATE pay_pro_vendor INNER JOIN pay_emp_paypro ON  pay_pro_vendor.purch_invoice_no = pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " SET payment_status = " + status + " ,paypro_batch_id = '" + r[4].ToString() + "', SALARY_DATE= str_to_date('" + r[16].ToString() + "','%d-%b-%y')");
                                res = d.operation("UPDATE pay_transactionp INNER JOIN pay_emp_paypro ON pay_transactionp.comp_code = pay_emp_paypro.comp_code and pay_transactionp.doc_no = pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " SET payment_status = " + status + "");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 3);
                                }
                            }


                            if (res > 0 && (status == 1 || status ==2))
                            {
                                comments = "Success";
                                if (status == 1)
                                {
                                    d.operation("update pay_emp_paypro set status=1 where pay_pro_no = " + bank_code);
                                }
                            }
                            else
                            {
                                //suraj update query
                                if (!d.getsinglestring("select payment_status from "+table_name+" where id=" + bank_code).Equals("1"))
                                {
                                    //Issues excel input
                                    
                                    table2.Rows.Add(r[9].ToString(), r[8].ToString(), r[10].ToString(), r[11].ToString(),  r[4].ToString(), "Pay Pro CRN Number not matching in system");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            table2.Rows.Add(r[9].ToString(), r[10].ToString(), r[11].ToString(), r[13].ToString(), r[20].ToString(), ex.Message.ToString());
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
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file(ds);
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
        fill_gridview();
    }
    protected void btn_resend_Click(object sender, EventArgs e)
    {
        create_salary_pdf("", txttodate.Text.Substring(0, 2), txttodate.Text.Substring(3), 4);
    }
  
    private void update_pay_pro_number(int status, string code, int i)
    {
        //string id = d.getsinglestring("SELECT IFNULL(MAX(pay_pro_no)+1 , 21000) FROM pay_emp_paypro");
        //manpower
        if (i == 0)
        {
            if (ddl_bank.SelectedValue == "AXIS BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 21000) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'AXIS BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_master ON pay_pro_master.emp_code = pay_emp_paypro.emp_code AND pay_pro_master.month = pay_emp_paypro.month AND pay_pro_master.year = pay_emp_paypro.year AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and  pay_emp_paypro.emp_code is not null and pay_emp_paypro.type = '" + i + "' and bank = 'AXIS BANK' ");
            }
            else if (ddl_bank.SelectedValue == "INDUSIND BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 0) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'INDUSIND BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_master ON pay_pro_master.emp_code = pay_emp_paypro.emp_code AND pay_pro_master.month = pay_emp_paypro.month AND pay_pro_master.year = pay_emp_paypro.year AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and  pay_emp_paypro.emp_code is not null and pay_emp_paypro.type = '" + i + "' and bank = 'INDUSIND BANK'");
            }
        }
            //convenyence
        else if(i==1)
        {
            if (ddl_bank.SelectedValue == "AXIS BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 21000) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'AXIS BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_material_history ON pay_pro_material_history.emp_code = pay_emp_paypro.emp_code AND pay_pro_material_history.month = pay_emp_paypro.month AND pay_pro_material_history.year = pay_emp_paypro.year AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and pay_emp_paypro.emp_code is not null  and pay_emp_paypro.type = '" + i + "' and bank = 'AXIS BANK' ");
            }
            else if (ddl_bank.SelectedValue == "INDUSIND BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 0) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'INDUSIND BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_material_history ON pay_pro_material_history.emp_code = pay_emp_paypro.emp_code AND pay_pro_material_history.month = pay_emp_paypro.month AND pay_pro_material_history.year = pay_emp_paypro.year AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and pay_emp_paypro.emp_code is not null  and pay_emp_paypro.type = '" + i + "' and bank = 'INDUSIND BANK' ");
            }
         }
            //vendor
        else if (i == 3)
        {
            if (ddl_bank.SelectedValue == "AXIS BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 21000) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'AXIS BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_vendor ON pay_pro_vendor.purch_invoice_no = pay_emp_paypro.emp_code  AND pay_emp_paypro.month = case when length(month_year)=6 then SUBSTRING(pay_pro_vendor.month_year,1, 1)else SUBSTRING(pay_pro_vendor.month_year,1, 2) END AND pay_emp_paypro.year = case when length(month_year)=6 then SUBSTRING(pay_pro_vendor.month_year,3)else SUBSTRING(pay_pro_vendor.month_year,4) END AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and pay_emp_paypro.type = '" + i + "' and bank = 'AXIS BANK' ");
            }
            else if (ddl_bank.SelectedValue == "INDUSIND BANK")
            {
                d.operation("SET @i := (SELECT IFNULL(MAX(pay_pro_no), 0) FROM pay_emp_paypro where pay_pro_no < 98000 and bank = 'INDUSIND BANK'); UPDATE pay_emp_paypro INNER JOIN pay_pro_vendor ON pay_pro_vendor.purch_invoice_no = pay_emp_paypro.emp_code  AND pay_emp_paypro.month = case when length(month_year)=6 then SUBSTRING(pay_pro_vendor.month_year,1, 1)else SUBSTRING(pay_pro_vendor.month_year,1, 2) END AND pay_emp_paypro.year = case when length(month_year)=6 then SUBSTRING(pay_pro_vendor.month_year,3)else SUBSTRING(pay_pro_vendor.month_year,4) END AND payment_status = 2 SET pay_pro_no = @i := @i + 1, pay_emp_paypro.status = 0 WHERE pay_emp_paypro.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_emp_paypro.year = '" + txt_month_year.Text.Substring(3) + "' AND pay_emp_paypro.status = 0 AND pay_pro_no=" + code + " and pay_emp_paypro.type = '" + i + "' and bank = 'INDUSIND BANK'");
            }
        }

    }
    protected void ddl_upload_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        bank_name.Visible = true;
        button.Visible = true;
        client_ddl.Visible = false;
        if (ddl_upload_type.SelectedValue == "ESIC Payment")
        {
            bank_name.Visible = false;
            chalan_ESIC.Visible = true;
            state_ddl.Visible = true;
            button.Visible = false;
            state_name();
        }
        else if (ddl_upload_type.SelectedValue == "PF Payment")
        {
            bank_name.Visible = false;
            client_ddl.Visible = true;
            chalan_PF.Visible = true;
            button.Visible = false;
            client_name();
        }
    }
    protected void client_name()
    {
        ddl_client_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
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
    
    protected void state_name()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = null;

        cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "'  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txttodate.Text + "', '%d/%m/%Y')) ORDER BY state_name", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_payment.DataSource = dt_item;
                ddl_state_payment.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_payment.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_payment.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_payment.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_PF_chalan_Click(object sender, EventArgs e)
   {
       string chk_month = d.getsinglestring("select distinct CONCAT(month,'/',year)as date from pay_emp_compliance WHERE client_code = '" + ddl_client_name.SelectedValue + "' and comp_code='" + Session["comp_code"] + "'  and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
       string txtbox_month_year = "" + txt_month_year.Text + "";
       if (chk_month == txtbox_month_year)
       {
           d.operation("delete from pay_emp_compliance where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_client_name.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
       }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = "~/Temp_images/";
                FilePath = Server.MapPath(FolderPath + FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    // string FolderPath = "~/Temp_images/";
                    //FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    btn_Challan_Click_PF(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded Successfully...');", true);
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
                client_ddl.Visible = true;
                chalan_PF.Visible = true;
            }
        }
    }
    public void btn_Challan_Click_PF(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
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
        int res = 0;
        //Push Datatable to database
        DataTable table2 = new DataTable("PF");

       
        table2.Columns.Add("comp_code");
        table2.Columns.Add("client_code");
        //table2.Columns.Add("unit_name");
        table2.Columns.Add("emp_name");
        table2.Columns.Add("UAN");
        table2.Columns.Add("EPF_CR");
        table2.Columns.Add("EPS_CR");
        table2.Columns.Add("ER");
        table2.Columns.Add("diff_EPF");
        table2.Columns.Add("diff_EPC");
        table2.Columns.Add("diff_ER");
        table2.Columns.Add("Comment");

        try
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row[1].ToString() != "" && row[1].ToString()!="UAN" )
               {
                   string UAN ="";
                   string emp_name ="";
                    string EPF ="";
                   string EPC ="";
                   string ER="";
                   string start_date = "";
                   string end_date = "";
                   Double diff_EPF = 0;
                   Double diff_EPC = 0;
                   Double diff_ER = 0;
                   d.con.Open();
                   MySqlCommand adp2 = new MySqlCommand("SELECT DISTINCT `client`,`unit_name`,`UAN`,`emp_name`,`EPF_CR`,`EPS_CR`,(`EPF_CR` - `EPS_CR`) AS 'ER',start_date,end_date FROM (SELECT CONCAT( pay_pro_master . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client',pay_pro_master.start_date,pay_pro_master.end_date,  pay_pro_master . state_name ,  pay_pro_master . unit_name ,  pay_pro_master . PF_NO ,  pay_pro_master . ESI_No  AS 'ESIC_NO',  pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name ,  pay_pro_master . grade , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda',  pay_pro_master . STATUS ,  pay_pro_master . month_days ,  pay_pro_master . Total_Days_Present , ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR', ( pay_pro_master . month_days  -  pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  `pay_pro_master`.`month` = '" + txt_month_year.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_month_year.Text.Substring(3) + "' AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`client_code` = '" + ddl_client_name.SelectedValue + "'  AND `pay_pro_master`.`PAN_No` = '" + row[1].ToString() + "' AND (`PAN_No` IS NOT NULL AND `PAN_No` != '') AND (`ESI_No` IS NOT NULL AND `ESI_No` != '') ORDER BY `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_name`) AS a WHERE a.EPF_CR = '" + row[8].ToString() + "'", d.con);
                  
                   MySqlDataReader dr = adp2.ExecuteReader();
                   if (dr.Read())
                   {
                        UAN = dr.GetValue(2).ToString();
                        emp_name = dr.GetValue(3).ToString();
                        EPF = dr.GetValue(4).ToString();
                        EPC = dr.GetValue(5).ToString();
                        ER = dr.GetValue(6).ToString();
                        start_date = dr.GetValue(7).ToString();
                        end_date = dr.GetValue(8).ToString();
                   }

                   if (row[1].ToString() == UAN && row[8].ToString() == EPF && row[9].ToString() == EPC && row[10].ToString() == ER)
                   {
                       res = d.operation("insert into pay_emp_compliance (comp_code,client_code,emp_name,UAN, month, year,EPF_CR,EPS_CR,ER,Flag_PF,start_date,end_date) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client_name.SelectedValue + "','" + row[2].ToString() + "','" + row[1].ToString() + "','" + txt_month_year.Text.Substring(0, 2) + "','" + txt_month_year.Text.Substring(3) + "','" + row[8].ToString() + "','" + row[9].ToString() + "','" + row[10].ToString() + "','1','"+start_date+"','"+end_date+"')");
                       d.operation("UPDATE `pay_pro_master` SET `pf_status` = 1 WHERE `pay_pro_master`.`PAN_No` = '" + row[1].ToString() + "'   AND `pay_pro_master`.`month` = '" + txt_month_year.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_month_year.Text.Substring(3) + "'  AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`client_code` = '" + ddl_client_name.SelectedValue + "' AND start_date='" + start_date + "' and end_date='" + end_date + "' and  (`PAN_No` IS NOT NULL AND `PAN_No` != '')");
                   
                   }
                   else
                   {
                       //res = d.operation("insert into pay_emp_compliance (comp_code,client_code,emp_name,UAN, month, year,EPF_CR,EPS_CR,ER,Flag_PF) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client_name.SelectedValue + "','" + row[2].ToString() + "','" + row[1].ToString() + "','" + txt_month_year.Text.Substring(0, 2) + "','" + txt_month_year.Text.Substring(3) + "','" + row[8].ToString() + "','" + row[9].ToString() + "','" + row[10].ToString() + "','0')");
                       if (UAN == "")
                       {
                           EPF = "" + row[8].ToString() + "";
                           EPC = "" + row[9].ToString() + "";
                           ER = "" + row[10].ToString() + "";
                            diff_EPF = 0;
                            diff_EPC = 0;
                            diff_ER = 0;
                       }
                       else
                       {
                           Double uploaded_EPF = Convert.ToDouble(row[8].ToString());
                           Double software_EPF = Convert.ToDouble(EPF);

                           Double uploaded_EPC = Convert.ToDouble(row[9].ToString());
                           Double software_EPC = Convert.ToDouble(EPC);

                           Double uploaded_ER = Convert.ToDouble(row[10].ToString());
                           Double software_ER = Convert.ToDouble(ER);

                            diff_EPF = software_EPF - uploaded_EPF;
                            diff_EPC = software_EPC - uploaded_EPC;
                            diff_ER = software_ER - uploaded_ER;
                       }
                       table2.Rows.Add(Session["COMP_CODE"].ToString(), ddl_client_name.SelectedValue, row[2].ToString(), row[1].ToString(), EPF, EPC, ER, diff_EPF, diff_EPC, diff_ER, "UAN_NO / Amount Not matching");
                       
                   }
                   d.con.Close();
               }
            }
            if (table2.Rows.Count > 0)
            {
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file_pf_esic(ds, 2);
            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select correct state !!!');", true);
                return;
            }
            
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
    protected void btn_ESIC_challan_Click(object sender, EventArgs e)
    {
        string chk_month = d.getsinglestring("select distinct CONCAT(month,'/',year)as date from pay_emp_compliance WHERE state_name = '" + ddl_state_payment.SelectedValue + "' and comp_code='" + Session["comp_code"] + "'  and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
        string txtbox_month_year = "" + txt_month_year.Text + "";
        if (chk_month == txtbox_month_year)
        {
            d.operation("delete from pay_emp_compliance where comp_code = '" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_state_payment.SelectedValue + "' and month='" + txt_month_year.Text.Substring(0, 2) + "' and year='" + txt_month_year.Text.Substring(3) + "'");
        }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = "~/Temp_images/";
                FilePath = Server.MapPath(FolderPath + FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    // string FolderPath = "~/Temp_images/";
                    //FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    btn_Challan_Click_ESIC(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded Successfully...');", true);
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
                state_ddl.Visible = true;
                chalan_ESIC.Visible = true;
            }
        }
    }
    public void btn_Challan_Click_ESIC(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
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
        int res = 0;
        //Push Datatable to database
        DataTable table2 = new DataTable("PF");


        table2.Columns.Add("comp_code");
        table2.Columns.Add("state_name");
        table2.Columns.Add("emp_name");
        table2.Columns.Add("ESIC_NO");
        table2.Columns.Add("esic_salary");
        table2.Columns.Add("sal_esic");
        table2.Columns.Add("diff_salary");
        table2.Columns.Add("diff_sal_esic");
        table2.Columns.Add("Comment");
        
        try
        {
            foreach (DataRow row in dt.Rows)
            
            {
                //if (row[2].ToString() != "" && !row[2].ToString().ToUpper().Contains("TOTAL") || row[2].ToString().Length == 10 || !row[2].ToString().ToUpper().Contains("Printed"))
                if (row[2].ToString() != "" && row[2].ToString() != "Total Contribution" && row[2].ToString().Length == 10 && row[2].ToString()!="Printed")
                {
                        string ESIC_NO = "";
                        string esic_salary = "";
                        string sal_esic = "";
                        string start_date = "";
                        string end_date = "";
                        Double uploaded_esic_sal = 0;
                        Double software_esic_sal = 0;
                     Double uploaded_sal_esic = 0;
                     Double software_sal_esic = 0;
                     Double diff_salary = 0;
                     Double diff_sal_esic = 0;
                     
                        d.con.Open();
                        MySqlCommand adp2 = new MySqlCommand("SELECT DISTINCT `ESIC_NO`, `emp_name`, `esic_salary`, `sal_esic`,start_date,end_date FROM(SELECT  `pay_pro_master`.`ESI_No` AS 'ESIC_NO',  `pay_pro_master`.`emp_name`,ROUND((`pay_pro_master`.`gross`), 2) AS 'esic_salary',CAST(CEILING(((`pay_pro_master`.`gross` * `sal_esic_percent`) / 100)) AS decimal(16,2)) AS 'sal_esic', pay_pro_master.start_date,pay_pro_master.end_date FROM `pay_pro_master` INNER JOIN `pay_billing_unit_rate_history` ON  `pay_pro_master`.`month` = `pay_billing_unit_rate_history`.`month`   AND `pay_pro_master`.`year` = `pay_billing_unit_rate_history`.`year`  AND `pay_pro_master`.`comp_code` = `pay_billing_unit_rate_history`.`comp_code`   AND `pay_pro_master`.`state_name` = `pay_billing_unit_rate_history`.`state_name`   AND `pay_pro_master`.`emp_code` = `pay_billing_unit_rate_history`.`emp_code` AND IF(`pay_billing_unit_rate_history`.`client_code` = 'RCPL', `invoice_no` IS NULL, `invoice_no` IS NOT NULL) WHERE  `pay_pro_master`.`month` = '" + txt_month_year.Text.Substring(0, 2) + "'  AND `pay_pro_master`.`year` = '" + txt_month_year.Text.Substring(3) + "' AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "'  AND `pay_pro_master`.`state_name` = '" + ddl_state_payment.SelectedValue + "'  AND `pay_pro_master`.`ESI_No`='" + row[2].ToString() + "'   AND (`pay_pro_master`.`Employee_type` = 'Temporary' OR `pay_pro_master`.`Employee_type` = 'Permanent')  AND (`PAN_No` IS NOT NULL AND `PAN_No` != '') AND (`ESI_No` IS NOT NULL AND `ESI_No` != '') ORDER BY `pay_pro_master`.`client`, `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_name`) AS a WHERE `a`.`esic_salary` = '" + row[5].ToString() + "'", d.con);

                        MySqlDataReader dr = adp2.ExecuteReader();
                        if (dr.Read())
                        {
                            ESIC_NO = dr.GetValue(0).ToString();
                            esic_salary = dr.GetValue(2).ToString();
                            sal_esic = dr.GetValue(3).ToString();
                            start_date = dr.GetValue(4).ToString();
                            end_date = dr.GetValue(5).ToString();
                        }
                        //if (ESIC_NO != "")
                        //{
                            //if (row[2].ToString() == ESIC_NO)
                            //{
                                if (row[2].ToString() == ESIC_NO && row[5].ToString() == esic_salary && row[6].ToString() == sal_esic)
                                {
                                    res = d.operation("insert into pay_emp_compliance (comp_code,state_name,emp_name,ESIC_NO, month, year,esic_salary,sal_esic,Flag_ESIC) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_state_payment.SelectedValue + "','" + row[3].ToString() + "','" + row[2].ToString() + "','" + txt_month_year.Text.Substring(0, 2) + "','" + txt_month_year.Text.Substring(3) + "','" + row[5].ToString() + "','" + row[6].ToString() + "','1')");
                                    d.operation("UPDATE `pay_pro_master` SET `esic_status` =1  WHERE `pay_pro_master`.`ESI_No` = '" + row[2].ToString() + "'   AND `pay_pro_master`.`month` = '" + txt_month_year.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_month_year.Text.Substring(3) + "'  AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`state_name` = '" + ddl_state_payment.SelectedValue + "' AND start_date='" + start_date + "' and end_date='" + end_date + "' and  (`ESI_No` IS NOT NULL AND `ESI_No` != '')");
                                }
                            
                            else
                            {
                                if (ESIC_NO == "")
                                {
                                    esic_salary = "" + row[5].ToString() + "";
                                    sal_esic = "" + row[6].ToString() + "";
                                    diff_salary = 0; ;
                                    diff_sal_esic = 0;
                                }
                                else
                                {
                                    uploaded_esic_sal = Convert.ToDouble(row[5].ToString());
                                    software_esic_sal = Convert.ToDouble(esic_salary);

                                    uploaded_sal_esic = Convert.ToDouble(row[6].ToString());
                                    software_sal_esic = Convert.ToDouble(sal_esic);

                                    diff_salary = software_esic_sal - uploaded_esic_sal;
                                    diff_sal_esic = software_sal_esic - uploaded_sal_esic;
                                }
                                table2.Rows.Add(Session["COMP_CODE"].ToString(), ddl_state_payment.SelectedValue, row[3].ToString(), row[2].ToString(), esic_salary, sal_esic, diff_salary, diff_sal_esic, "ESIC_NO / ESIC_SALARY Amount Not Matching");
                            }
                            
                        //}
                        d.con.Close();
                    }
            }
            if (table2.Rows.Count > 0)
            {
                d.con1.Open();
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file_pf_esic(ds, 1);
            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select correct state !!!');", true);
                return;
            }
            
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
	protected void send_file_pf_esic(DataSet ds,int i)
    {
        //d.con1.Open();
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                if (i == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ESIC_ISSUE.xls");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=PF_ISSUE.xls");
                }
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate5(ListItemType.Header, ds, i);
                Repeater1.ItemTemplate = new MyTemplate5(ListItemType.Item, ds, i);
                Repeater1.FooterTemplate = new MyTemplate5(ListItemType.Footer, null, i);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(stringWrite.ToString());
                Response.Flush();
                Response.End();
                d.con1.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' File Uploaded Successfully...');", true);
        }
    }
    public class MyTemplate5 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i = 0;
        static int ctr;
        public MyTemplate5(ListItemType type, DataSet ds, int i)
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
                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=9 align=center> ESIC ISSUE SHEET </th><tr><th>SR. NO.</th><th>STATE NAME</th><th>EMP NAME</th><th>ESIC_NO</th><th>ESIC SALARY</th><th>SAL ESIC</th><th>DIFFERANCE ESIC SALARY(Actual - ECR Challan)</th>><th>DIFFERANCE SAL ESIC(Actual - ECR Challan)</th><th>Comment</th></tr>");
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=11 align=center> PF ISSUE REPORT </th><tr><th>SR. NO.</th><th>CLIENT CODE</th><th>EMP NAME</th><th>UAN</th><th>EPF</th><th>EPS</th><th>ER</th><th>DIFFERANCE EPF(Actual - ECR Challan)</th><th>DIFFERANCE EPS(Actual - ECR Challan)</th><th>DIFFERANCE ER(Actual - ECR Challan)</th><th>Comment</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["esic_salary"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sal_esic"] + "</td><td>" + ds.Tables[0].Rows[ctr]["diff_salary"] + "</td><td>" + ds.Tables[0].Rows[ctr]["diff_sal_esic"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Comment"] + "</td></tr>");
                    }
                    else
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["UAN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EPF_CR"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EPS_CR"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["diff_EPF"] + "</td><td>" + ds.Tables[0].Rows[ctr]["diff_EPC"] + "</td><td>" + ds.Tables[0].Rows[ctr]["diff_ER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Comment"] + "</td></tr>");
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
	  public void indusind_Import_Click_new(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;

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

        //if (!chk_excel(dt))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PayPro file does not match with payment Annexure !!!');", true);
        //    return;
        //}

        //Push Datatable to database
        DataTable table2 = new DataTable("emp");
        table2.Columns.Add("bene_name");
        table2.Columns.Add("payee_name");

        table2.Columns.Add("bene_account_no");
        table2.Columns.Add("amount_payable");
       // table2.Columns.Add("paid_batch_no");
        table2.Columns.Add("Comments");
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                string bank_code = "", comments = "Not Found";

                try
                {
                    //0 == HOLD; 1 == Paid; 2 == Returned
                    int status = 0;
                    int res = 0;
                    // 0 == unpaid , 1 == Paid;
                    int branch_email_status = 0;
                    if (r[2].ToString() != "") { bank_code = r[2].ToString(); }

                    if (r[1].ToString().Trim() == "Batch No")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This is old Paypro file format use process button.');", true);
                        return;
                    }
                    if (bank_code.Trim() != "" && !bank_code.ToUpper().Contains("CORPORATE"))
                    {

                        if (r[13].ToString() != "")
                        {
                            if (r[13].ToString() == "Successful")
                            { status = 1; branch_email_status = 1; }
                            if (r[13].ToString() == "Failed" || r[13].ToString() == "Pending")
                            { status = 2; }
                        }
                        try
                        {
                            string table_name = "";
                            if (ddl_upload_type.SelectedValue == "Payment")
                            {
                                table_name = "pay_pro_master";
                                string payment_date = r[8].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                                res = d.operation("update pay_pro_master inner join pay_emp_paypro on pay_pro_master.comp_code = pay_emp_paypro.comp_code and pay_pro_master.month=pay_emp_paypro.month and pay_pro_master.year=pay_emp_paypro.year and pay_pro_master.emp_code=pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " set utr_number='" + r[0].ToString() + "', payment_status=" + status + ", salary_date= str_to_date('" + payment_date + "','%d/%m/%Y'), branch_email = '" + branch_email_status + "'  where branch_email != 2");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 0);
                                }
                            }
                            else if (ddl_upload_type.SelectedValue == "Conveyance")
                            {
                                table_name = "pay_pro_material_history";
                                string payment_date = r[8].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                                res = d.operation("update pay_pro_material_history inner join pay_emp_paypro on pay_pro_material_history.comp_code = pay_emp_paypro.comp_code and pay_pro_material_history.month=pay_emp_paypro.month and pay_pro_material_history.year=pay_emp_paypro.year and pay_pro_material_history.emp_code=pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " set utr_number='" + r[0].ToString() + "', payment_status=" + status + ",  payment_date= str_to_date('" + payment_date + "','%d/%m/%Y')");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 1);
                                }
                            }
                            else if (ddl_upload_type.SelectedValue == "Vendor")
                            {
                                table_name = "pay_pro_vendor";
                                string payment_date = r[8].ToString().Trim();
                                if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                                res = d.operation("UPDATE pay_pro_vendor INNER JOIN pay_emp_paypro ON pay_pro_vendor.comp_code = pay_emp_paypro.comp_code and pay_pro_vendor.purch_invoice_no = pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " SET payment_status = " + status + " , SALARY_DATE= str_to_date('" + payment_date + "','%d/%m/%Y')");
                                res = d.operation("UPDATE pay_transactionp INNER JOIN pay_emp_paypro ON pay_transactionp.comp_code = pay_emp_paypro.comp_code and pay_transactionp.doc_no = pay_emp_paypro.emp_code and pay_pro_no = " + bank_code + " SET payment_status = " + status + "");
                                if (status == 2)
                                {
                                    update_pay_pro_number(status, bank_code, 3);
                                }
                            }


                            if (res > 0 && (status == 1 || status == 2))
                            {
                                comments = "Success";
                                if (status == 1)
                                {
                                    d.operation("update pay_emp_paypro set status=1 where pay_pro_no = " + bank_code);
                                }
                            }
                            else
                            {
                                //suraj update query
                                if (!d.getsinglestring("select payment_status from " + table_name + " where id=" + bank_code).Equals("1"))
                                {
                                    //Issues excel input

                                    table2.Rows.Add(r[4].ToString(), r[11].ToString(), r[5].ToString(), r[9].ToString(), "Pay Pro CRN Number not matching in system");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                             //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Uploaded File Formate...Please Try again....');", true);
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
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_inds_file(ds);
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
        fill_gridview();
    }
     private void send_inds_file(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Paypro_issue.xls");
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
    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        int i = 0;
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
                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BENE NAME</th><th>PAYEE NAME</th><th>BENE ACCOUNT NO.</th><th>AMOUNT PAYABLE</th><th>Comments</th></tr>");
                       // lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th> CLIENT_NAME </th><th> BRANCH_NAME</th><th>EMPLOYEE NAME</th><th>SALARY_STATUS</th>");
                    }
                    //else
                    //{
                    //    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>BENE NAME</th><th>PAYEE NAME</th><th>BENE ACCOUNT NO.</th><th>AMOUNT PAYABLE</th><th>PAID BATCH NO.</th><th>Comments</th></tr>");

                    //}
                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["bene_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["payee_name"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["bene_account_no"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["amount_payable"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Comments"].ToString().ToUpper() + "</td></tr>");
                        //lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["branch_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Salary Status"] + " </td>");
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




