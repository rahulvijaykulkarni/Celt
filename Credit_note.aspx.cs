using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;
using System.IO;


public partial class Credit_note : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    DAL d5 = new DAL();
	//komal
	//CrystalDecisions.CrystalReports.Engine.ReportDocument crystalReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    ReportDocument crystalReport = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
		//komal start
            gv_other_invoice.DataSource = null;
            gv_other_invoice.DataBind();

          
            other_state_name();
            sac_code();
            //btn_excel_clientwise.Visible = false;
            //btn_final_invoice.Visible = false;
            panel_other.Visible = false;
            panel4.Visible = false;
         
          //  panel_invoice_date.Visible = false;
            //btn_clientwise_other_invoice.Visible = false;
            btn_update_other.Visible = false;
		//komal end
  			client_name();
            txt_date.Text = d.getCurrentMonthYear();
            ddl_client_manual.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client_manual.DataSource = dt_item;
                    ddl_client_manual.DataTextField = dt_item.Columns[0].ToString();
                    ddl_client_manual.DataValueField = dt_item.Columns[1].ToString();
                    ddl_client_manual.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_client_manual.Items.Insert(0, "ALL");
                ddl_state.Items.Insert(0, "Select");


            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        status_gv();
       // manual_invoice_details();
        btn_submit.Visible = false;
        button.Visible = false;
        gv_show.Visible = false;
    }
    protected void client_name()
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
            d.con.Close();
            ddl_client.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        status_gv();
    }
    protected void btn_process_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        gv_check_invoice.Visible = true;
        btn_submit.Visible = true;
        load_gv();
    }

    protected void load_gv()
    {
        d.con.Open();
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT invoice_no as 'Invoice_no',state_name,type FROM `pay_report_gst` WHERE `client_code` = '" + ddl_client.SelectedValue + "' AND `COMP_CODE` = '" + Session["comp_code"].ToString() + "' AND `month` = '" + txt_date.Text.Substring(0, 2) + "' AND `year` = '" + txt_date.Text.Substring(3) + "'", d.con);
            cmd_item.Fill(ds);
            gv_check_invoice.DataSource = ds.Tables[0];
            gv_check_invoice.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        btn_submit.Visible = true;
        button.Visible = true;
        gv_show.Visible = true;
        string invoice_list = "";
        MySqlDataAdapter ads1 = null;
        foreach (GridViewRow gvrow in gv_check_invoice.Rows)
        {
            string Invoice_no = (string)gv_check_invoice.DataKeys[gvrow.RowIndex].Value;
            var checkbox = gvrow.FindControl("chk_invoice") as CheckBox;

            if (checkbox.Checked == true)
            {
                invoice_list = invoice_list + "'" + Invoice_no + "',";
               
                //invoice_list = invoice_list.Replace("'", "  ");
                
                // b_amt = b_amt + "'" + Balanced_amount + "'";

            }
            //string No_invoice=""+Invoice_no+"";
            //string show_date = d.getsinglestring("select date_for mat(credit_note_date,'%d/%m/%Y') from credit_debit_note where `client_name` = '" + ddl_client.SelectedValue + "' and `original_bill_no` IN ('" + Invoice_no + "') ");
            //if (show_date != "")
            //{
            //    //string txt_credit_not_date = ((TextBox)gvrow.FindControl("txt_credit_not_date")).Text;
            //    //TextBox txt_credit_not_date = (TextBox)gvrow.FindControl("txt_credit_not_date");
            //    //txt_credit_not_date.Text = show_date.ToString();
            //}
            //ads1 = new MySqlDataAdapter("select credit_note_date,original_bill_no,bill_date,client_name,gst_no,taxable_amt,cgst,sgst,Igst,Total credit_debit_note where original_bill_no in(" + No_invoice + ")",d.con);
        }
       

        if (invoice_list == "") 
        { 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select atlest one Invoice !!!')", true);
        }
        load_gv();
        if (invoice_list.Length > 0)
        {
            invoice_list = invoice_list.Substring(0, invoice_list.Length - 1);
        }
        else { invoice_list = "''"; }

        string[] abc = invoice_list.Split(',');

        d.con.Open();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //string check_record_db = "SELECT original_bill_no , DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'invoice_date',client_name as 'cl', gst_no ,ROUND( taxable_amt , 2) 'amount',ROUND( cgst , 2) 'cgst',ROUND( sgst , 2) 'sgst', ROUND( igst , 2) 'igst',( taxable_amt +  cgst  +  sgst  +  igst ) AS 'Total' FROM credit_debit_note  WHERE client_name  = '" + ddl_client.SelectedItem + "' AND  comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  original_bill_no  IN ('"+invoice_list+"')";
        //string check_date = d.getsinglestring("select group_concat(credit_note_date) from credit_debit_note where client_name='" + ddl_client.SelectedItem + "' and type='" + ddl_note_type.SelectedValue + "' and original_bill_no in(" + invoice_list + ")");
        //if (check_date != "")
        //{
        //    ads1 = new MySqlDataAdapter("SELECT original_bill_no as 'Invoice_no',DATE_FORMAT(credit_note_date, '%d/%m/%Y') as 'CREDIT NOTE DATE',DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'invoice_date',client_name as 'client_code' ,gst_no , ROUND( taxable_amt , 2) 'amount',ROUND( cgst , 2) 'cgst',ROUND( sgst , 2) 'sgst', ROUND( igst , 2) 'igst', ( taxable_amt  +  cgst  +  sgst  +  igst ) AS 'Total' FROM credit_debit_note WHERE client_name  = '" + ddl_client.SelectedItem + "' AND  comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  month  = '" + txt_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_date.Text.Substring(3) + "' and original_bill_no in(" + invoice_list + ")   ", d.con);
        //}
        //else
        //{
        //union query
       // ads1 = new MySqlDataAdapter("SELECT DATE_FORMAT(credit_note_date, '%d/%m/%Y') as 'CREDIT NOTE DATE',original_bill_no as 'Invoice_no',DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'invoice_date',client_name as 'client_code' ,gst_no , ROUND( taxable_amt , 2) 'amount',ROUND( cgst , 2) 'cgst',ROUND( sgst , 2) 'sgst', ROUND( igst , 2) 'igst', ( taxable_amt  +  cgst  +  sgst  +  igst ) AS 'Total' FROM credit_debit_note WHERE client_name  = '" + ddl_client.SelectedItem + "' AND  comp_code  = '" + Session["COMP_CODE"].ToString() + "' and original_bill_no in(" + invoice_list + ") UNION SELECT  '' as  'CREDIT NOTE DATE',invoice_no,DATE_FORMAT(`invoice_date`, '%d/%m/%Y') as 'invoice_date',client_name as 'client_code', gst_no,ROUND(`amount`, 2)'amount',ROUND(`cgst`, 2) 'cgst', ROUND(`sgst`, 2)'sgst', ROUND(`igst`, 2)'igst',(amount+cgst+sgst+igst) as 'Total' FROM  pay_report_gst  WHERE client_code  = '" + ddl_client.SelectedValue + "' AND  comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  month  = '" + txt_date.Text.Substring(0, 2) + "' AND  year  = '" + txt_date.Text.Substring(3) + "' and invoice_no in(" + invoice_list + ")", d.con);
       //}

        string type =d.getsinglestring( " select type from credit_debit_note where `original_bill_no` IN (" + invoice_list + ") and client_name='" + ddl_client.SelectedItem + "' and comp_code  = '" + Session["COMP_CODE"].ToString() + "' and type='"+ddl_note_type.SelectedValue+"'");

        if (type == "1" || type=="2")
        {
            ads1 = new MySqlDataAdapter("SELECT invoice_no , DATE_FORMAT( invoice_date , '%d/%m/%Y') AS 'invoice_date', if(credit_debit_note.type='" + ddl_note_type.SelectedValue + "',DATE_FORMAT(`credit_note_date`, '%d/%m/%Y'),'') as 'CREDIT NOTE DATE', pay_report_gst.`client_name`,pay_report_gst.`state_name`, `pay_report_gst`.`type`,pay_report_gst . gst_no ,ROUND( pay_report_gst . amount , 2) 'amount',  ROUND( pay_report_gst . cgst , 2) 'cgst',ROUND( pay_report_gst . sgst , 2) 'sgst',ROUND( pay_report_gst . igst , 2) 'igst',( amount +  pay_report_gst . cgst +  pay_report_gst . sgst +  pay_report_gst . igst ) AS 'Total' FROM pay_report_gst  LEFT OUTER JOIN  credit_debit_note  ON  pay_report_gst . comp_code  =  credit_debit_note . comp_code  AND  pay_report_gst . invoice_no  =  credit_debit_note . original_bill_no WHERE client_code  = '" + ddl_client.SelectedValue + "' AND  pay_report_gst . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  pay_report_gst . month  = '" + txt_date.Text.Substring(0, 2) + "' AND  pay_report_gst . year  = '" + txt_date.Text.Substring(3) + "' AND  pay_report_gst . invoice_no  IN (" + invoice_list + ")  group by invoice_no", d.con);
        }
        else
        {
            ads1 = new MySqlDataAdapter("SELECT invoice_no , DATE_FORMAT( invoice_date , '%d/%m/%Y') AS 'invoice_date',''  AS 'CREDIT NOTE DATE', pay_report_gst.`client_name`,pay_report_gst.`state_name`, `pay_report_gst`.`type`,pay_report_gst . gst_no ,ROUND( pay_report_gst . amount , 2) 'amount',  ROUND( pay_report_gst . cgst , 2) 'cgst',ROUND( pay_report_gst . sgst , 2) 'sgst',ROUND( pay_report_gst . igst , 2) 'igst',( amount +  pay_report_gst . cgst +  pay_report_gst . sgst +  pay_report_gst . igst ) AS 'Total' FROM pay_report_gst  LEFT OUTER JOIN  credit_debit_note  ON  pay_report_gst . comp_code  =  credit_debit_note . comp_code  AND  pay_report_gst . invoice_no  =  credit_debit_note . original_bill_no WHERE client_code  = '" + ddl_client.SelectedValue + "' AND  pay_report_gst . comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  pay_report_gst . month  = '" + txt_date.Text.Substring(0, 2) + "' AND  pay_report_gst . year  = '" + txt_date.Text.Substring(3) + "' AND  pay_report_gst . invoice_no  IN (" + invoice_list + ")  ", d.con);
        }
            ads1.Fill(dt_item);
        if (dt_item.Rows.Count > 0)
        {
            gv_invoice_details.DataSource = dt_item;
            gv_invoice_details.DataBind();
            btn_submit.Visible = false;
            gv_check_invoice.Visible = false;
        }
        else
        {
             button.Visible = false;
            gv_show.Visible = false;
        }
        d.con.Close();
    }


    protected void gv_check_invoice_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_check_invoice.UseAccessibleHeader = false;
            gv_check_invoice.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_check_invoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "1")
            {
                //Find the TextBox control.
                CheckBox txtName = (e.Row.FindControl("chk_invoice") as CheckBox);
                txtName.Checked = true;
                txtName.Enabled = false;
            }
        }
    }
    protected void gv_invoice_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_invoice_details.UseAccessibleHeader = false;
            gv_invoice_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_invoice_details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "1")
            {
                //Find the TextBox control.
                CheckBox txtName = (e.Row.FindControl("chk_invoice1") as CheckBox);
                txtName.Checked = true;
                txtName.Enabled = false;
            }
        }
    }
    protected void gv_status_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_status.UseAccessibleHeader = false;
            gv_status.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_status_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }
    protected void txt_amount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();

                    GridViewRow row = (GridViewRow)(((TextBox)sender)).NamingContainer;
                    double txt_amount = double.Parse(((TextBox)row.FindControl("txt_amount")).Text);

              
                    string txt_taxable_amt =  (row.FindControl("txt_amount") as System.Web.UI.WebControls.TextBox).Text;
                    Double txt_taxable_amt1 = Convert.ToDouble(txt_taxable_amt);
                  

                    string txt_cgst_txt = (row.FindControl("txt_cgst") as System.Web.UI.WebControls.TextBox).Text;
                    Double txt_cgst_txt1 = Convert.ToDouble(txt_cgst_txt);

                    string txt_sgst_txt = (row.FindControl("txt_sgst") as System.Web.UI.WebControls.TextBox).Text;
                    Double txt_sgst_txt1 = Convert.ToDouble(txt_sgst_txt);

                    string txt_igst_txt = (row.FindControl("txt_igst") as System.Web.UI.WebControls.TextBox).Text;
                    Double txt_igst_txt1 = Convert.ToDouble(txt_igst_txt);

                    Double cgst=0;
                        Double sgst=0;
                        Double igst = 0;
                        if (txt_cgst_txt1 == 0)
                        {
                            cgst = 0;
                        }
                        else
                        {
                            cgst = ((txt_taxable_amt1) * (0.09));
                        }
                        if (txt_sgst_txt1 == 0)
                        {
                            sgst = 0;
                        }
                        else
                        { sgst = ((txt_taxable_amt1) * (0.09)); }
                        if (txt_igst_txt1 == 0)
                        {
                            igst = 0;
                        }
                        else
                        { igst = ((txt_taxable_amt1) * (0.18)); }


                        Double All_total = (cgst + sgst + igst + txt_amount);
                        
                    TextBox txt_cgst = (TextBox)row.FindControl("txt_cgst");
                    txt_cgst.Text = cgst.ToString();

                    TextBox txt_sgst = (TextBox)row.FindControl("txt_sgst");
                    txt_sgst.Text = sgst.ToString();

                    TextBox txt_igst = (TextBox)row.FindControl("txt_igst");
                    txt_igst.Text = igst.ToString();

                    TextBox txt_total = (TextBox)row.FindControl("txt_total");
                    txt_total.Text = All_total.ToString();

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            gv_show.Visible = true;
            button.Visible = true;
            d.con.Close();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            hidtab.Value = "0";
            int res = 0;
            string invoice_list = "";
            foreach (GridViewRow row in gv_invoice_details.Rows)
            {
                string Invoice_no = (string)gv_invoice_details.DataKeys[row.RowIndex].Value;
                var checkbox = row.FindControl("chk_invoice1") as CheckBox;

                if (checkbox.Checked == true)
                {
                    invoice_list = invoice_list + "" + Invoice_no + "";

                }
                else
                {
                    Invoice_no = "";
                }
                string check_final_flag = "";
                string txt_credit_not_date = ((TextBox)row.FindControl("txt_credit_not_date")).Text;
                    if (txt_credit_not_date != "")
                    {
                        check_final_flag = d.getsinglestring("select Final_invoice_flag from credit_debit_note where client_name = '" + ddl_client.SelectedItem + "' AND `month` = '" + txt_credit_not_date.Substring(3, 2) + "' AND `year` = '" + txt_credit_not_date.Substring(6) + "' AND `original_bill_no` IN ('" + Invoice_no + "') and type='" + ddl_note_type.SelectedValue + "'");
                    }
                    if (check_final_flag == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('This Invoice Number Already Final !!!')", true);
                        gv_show.Visible = true;
                        button.Visible = true;
                        return;
                    }
                string bill_date = row.Cells[4].Text;
                string client_name = row.Cells[5].Text;
                string state_name = row.Cells[6].Text;
                string TYPE = row.Cells[7].Text;
                string gst_no = row.Cells[8].Text;
                string txt_amount1 = String.Format("{0:0.00}", ((TextBox)row.FindControl("txt_amount")).Text).Replace(",", "");
                string txt_cgst = String.Format("{0:0.00}", ((TextBox)row.FindControl("txt_cgst")).Text).Replace(",", "");
                string txt_sgst = String.Format("{0:0.00}", ((TextBox)row.FindControl("txt_sgst")).Text).Replace(",", "");
                string txt_igst = String.Format("{0:0.00}", ((TextBox)row.FindControl("txt_igst")).Text).Replace(",", "");
                string txt_total = String.Format("{0:0.00}", ((TextBox)row.FindControl("txt_total")).Text).Replace(",", "");
                if (invoice_list != "")
                {
                    if (txt_credit_not_date == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Date... !!!');", true);
                        button.Visible = true;
                        gv_show.Visible = true;
                        return;
                    }
                    string check_invoice_entry = "";
                    check_invoice_entry = d.getsinglestring("select original_bill_no from credit_debit_note where client_name='" + client_name + "' and type='" + ddl_note_type.SelectedValue + "' and month='" + txt_credit_not_date.Substring(3,2) + "' and year='"+txt_credit_not_date .Substring(6)+"'and original_bill_no='"+invoice_list+"'");
                    if (check_invoice_entry != "")
                    {
                        d.operation("delete from credit_debit_note where client_name='" + client_name + "' and type='" + ddl_note_type.SelectedValue + "' and month='" + txt_credit_not_date.Substring(3, 2) + "' and year='" + txt_credit_not_date.Substring(6) + "'and original_bill_no='" + invoice_list + "'");
                    
                    }
                    string number = null;
                    MySqlCommand cmdmax = new MySqlCommand();
                    string initcode = "";
                    if (ddl_note_type.SelectedValue == "1")
                    {
                        cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(credit_note_no, 4, length(credit_note_no)-3) AS UNSIGNED)) FROM credit_debit_note WHERE comp_code='" + Session["comp_code"].ToString() + "' and Type='1'", d.con);
                        initcode = "CRN";
                    }
                    else if (ddl_note_type.SelectedValue == "2")
                    {
                        cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(credit_note_no, 4, length(credit_note_no)-3) AS UNSIGNED)) FROM credit_debit_note WHERE comp_code='" + Session["comp_code"].ToString() + "' and Type='2'", d.con);
                        initcode = "DRN";
                    }
                    d.con.Open();
                    MySqlDataReader drmax = cmdmax.ExecuteReader();



                    if (drmax.Read())
                    {

                        string str = drmax.GetValue(0).ToString();
                        if (str == "")
                        {
                            number = initcode.ToString() + "0001";
                        }
                        else
                        {
                            //number = (drmax.GetValue(0).ToString()) + 1;
                            int max_empcode = int.Parse(drmax.GetValue(0).ToString()) + 1;
                            if (max_empcode < 10)
                            {
                                number = initcode.ToString() + "000" + max_empcode;
                            }
                            else if (max_empcode >= 10 && max_empcode < 100)
                            {
                                number = initcode.ToString() + "00" + max_empcode;
                            }
                            else if (max_empcode >= 100 && max_empcode < 1000)
                            {
                                number = initcode.ToString() + "0" + max_empcode;
                            }
                            else if (max_empcode >= 1000 && max_empcode < 10000)
                            {
                                number = initcode.ToString() + "0" + max_empcode;
                            }
                            else if (max_empcode == 10000)
                            {
                                number = initcode.ToString() + "" + max_empcode;
                            }

                        }
                    }
                    string sql = "";
                    string set_field = "";
                    if (TYPE == "manpower")
                    {
                        sql = "SELECT COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2, COMP_CITY,COMP_STATE,PF_REG_NO,COMPANY_PAN_NO, COMPANY_TAN_NO,COMPANY_CIN_NO,SERVICE_TAX_REG_NO,UNIT_full_ADD1,unit_city,unit_gst_no,grade_desc,fromtodate,sum(tot_days_present) as 'tot_days_present',month_days ,housekeeiing_sac_code,Security_sac_code,grade_code FROM `pay_billing_unit_rate_history` WHERE `auto_invoice_no` = '"+invoice_list+"'  and `client_code`='"+ddl_client.SelectedValue+"' ";
                        set_field = " credit_debit_note. COMPANY_NAME = t1.COMPANY_NAME,credit_debit_note.COMP_ADDRESS1=t1.COMP_ADDRESS1, credit_debit_note.COMP_ADDRESS2=t1.COMP_ADDRESS2, credit_debit_note.COMP_CITY=t1.COMP_CITY, credit_debit_note.COMP_STATE=t1.COMP_STATE, credit_debit_note.PF_REG_NO=t1.PF_REG_NO,  credit_debit_note.COMPANY_PAN_NO=t1.COMPANY_PAN_NO,  credit_debit_note.COMPANY_TAN_NO=t1.COMPANY_TAN_NO,credit_debit_note.COMPANY_CIN_NO=t1.COMPANY_CIN_NO,credit_debit_note.SERVICE_TAX_REG_NO=t1.SERVICE_TAX_REG_NO,  credit_debit_note.UNIT_full_ADD1=t1.UNIT_full_ADD1, credit_debit_note.unit_city=t1.unit_city, credit_debit_note.unit_gst_no=t1.unit_gst_no, credit_debit_note.grade_desc=t1.grade_desc, credit_debit_note.fromtodate=t1.fromtodate,credit_debit_note.tot_days_present=t1.tot_days_present,credit_debit_note.month_days=t1.month_days,credit_debit_note.housekeeiing_sac_code=t1.housekeeiing_sac_code,credit_debit_note.Security_sac_code=t1.Security_sac_code,  credit_debit_note.grade_code=t1.grade_code";
                    }
                    else if (TYPE != "manpower")
                    {
                        if (TYPE == "conveyance")
                        {
                            sql = "SELECT COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2, COMP_CITY,COMP_STATE,PF_REG_NO,COMPANY_PAN_NO, COMPANY_TAN_NO,COMPANY_CIN_NO,SERVICE_TAX_REG_NO,UNIT_full_ADD1,unit_city,unit_gst_no,grade_desc,fromtodate, count(`emp_code`) AS 'tot_days_present',month_days ,housekeeiing_sac_code,Security_sac_code,grade_code FROM `pay_billing_material_history` WHERE `auto_invoice_no` = '" + invoice_list + "'  and `client_code`='" + ddl_client.SelectedValue + "'";
                        }
                        else
                        {
                            sql = "SELECT COMPANY_NAME,COMP_ADDRESS1,COMP_ADDRESS2, COMP_CITY,COMP_STATE,PF_REG_NO,COMPANY_PAN_NO, COMPANY_TAN_NO,COMPANY_CIN_NO,SERVICE_TAX_REG_NO,UNIT_full_ADD1,unit_city,unit_gst_no,grade_desc,fromtodate,sum(tot_days_present) as 'tot_days_present',month_days ,housekeeiing_sac_code,Security_sac_code,grade_code FROM `pay_billing_material_history` WHERE `auto_invoice_no` = '" + invoice_list + "'  and `client_code`='" + ddl_client.SelectedValue + "'";
                        }
                       set_field = " credit_debit_note. COMPANY_NAME = t1.COMPANY_NAME,credit_debit_note.COMP_ADDRESS1=t1.COMP_ADDRESS1, credit_debit_note.COMP_ADDRESS2=t1.COMP_ADDRESS2, credit_debit_note.COMP_CITY=t1.COMP_CITY, credit_debit_note.COMP_STATE=t1.COMP_STATE, credit_debit_note.PF_REG_NO=t1.PF_REG_NO,  credit_debit_note.COMPANY_PAN_NO=t1.COMPANY_PAN_NO,  credit_debit_note.COMPANY_TAN_NO=t1.COMPANY_TAN_NO,credit_debit_note.COMPANY_CIN_NO=t1.COMPANY_CIN_NO,credit_debit_note.SERVICE_TAX_REG_NO=t1.SERVICE_TAX_REG_NO,  credit_debit_note.UNIT_full_ADD1=t1.UNIT_full_ADD1, credit_debit_note.unit_city=t1.unit_city, credit_debit_note.unit_gst_no=t1.unit_gst_no, credit_debit_note.grade_desc=t1.grade_desc, credit_debit_note.fromtodate=t1.fromtodate,credit_debit_note.tot_days_present=t1.tot_days_present,credit_debit_note.month_days=t1.month_days,credit_debit_note.housekeeiing_sac_code=t1.housekeeiing_sac_code,credit_debit_note.Security_sac_code=t1.Security_sac_code,  credit_debit_note.grade_code=t1.grade_code";
                    }
                    res= d.operation("insert into credit_debit_note (comp_code,credit_note_no,credit_note_date,original_bill_no,bill_date,client_name,gst_no,taxable_amt,cgst,sgst,Igst,Total,Type,month,year) values('" + Session["comp_code"].ToString() + "','" + number + "',str_to_date('" + txt_credit_not_date + "','%d/%m/%Y'),'" + invoice_list + "',str_to_date('" + bill_date + "','%d/%m/%Y'),'" + client_name + "','" + gst_no + "','" + txt_amount1 + "','" + txt_cgst + "','" + txt_sgst + "','" + txt_igst + "','" + txt_total + "','" + ddl_note_type.SelectedValue + "','" + txt_credit_not_date.Substring(3, 2) + "','" + txt_credit_not_date.Substring(6) + "')");
                    string crn_no = d.getsinglestring("select credit_note_no from credit_debit_note where original_bill_no='" + invoice_list + "' and type='" + ddl_note_type.SelectedValue + "'");
                    d.operation("update credit_debit_note INNER JOIN (" + sql + " ) t1 SET "+set_field+" where credit_note_no='" + crn_no + "' and original_bill_no='"+invoice_list+"' ");
                    invoice_list = "";
                }
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save Successfully... !!!');", true);
                    button.Visible = true;
                    gv_show.Visible = true;
                    status_gv(); 
                   
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Atleast One Record... !!!');", true);
                //    button.Visible = true;
                //    gv_show.Visible = true;
                //}
            }
            if (invoice_list == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Atleast One Record... !!!');", true);
                button.Visible = true;
                gv_show.Visible = true;
            }
           
        }
        catch (Exception ex) { throw ex; }
         finally
         {
             //button.Visible = false;
             d.con.Close();
         }
    }

    protected void btn_cr_exl_Click(object sender, EventArgs e)
    {
        try
        {
            hidtab.Value = "0";
            string sql = "";
           
            string status = "" + ddl_note_type.SelectedValue + "";
            string invoice_list = "";
            foreach (GridViewRow gvrow in gv_invoice_details.Rows)
            {
                string Invoice_no = (string)gv_invoice_details.DataKeys[gvrow.RowIndex].Value;
                var checkbox = gvrow.FindControl("chk_invoice1") as CheckBox;

                if (checkbox.Checked == true)
                {
                    invoice_list = invoice_list + "'" + Invoice_no + "',";

                    //invoice_list = invoice_list.Replace("'", "  ");

                    // b_amt = b_amt + "'" + Balanced_amount + "'";
                }
            }
            if (invoice_list == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select Invoice !!!')", true);
                gv_show.Visible = true;
                button.Visible = true;
                return;
            }
            if (invoice_list.Length > 0)
            {
                invoice_list = invoice_list.Substring(0, invoice_list.Length - 1);
            }
            else { invoice_list = "''"; }

            string[] invoice_no = invoice_list.Split(',');
            string manual_invoice_type = "";
            sql = "SELECT credit_debit_note . comp_code ,pay_report_gst . type ,credit_note_no ,DATE_FORMAT( credit_note_date , '%d/%m/%Y') AS 'credit_note_date',credit_debit_note.month,credit_debit_note.year,original_bill_no , DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'bill_date', credit_debit_note . client_name ,credit_debit_note . gst_no ,pay_report_gst . state_name ,taxable_amt ,credit_debit_note . cgst ,credit_debit_note . sgst ,credit_debit_note . Igst , Total  from credit_debit_note INNER JOIN  pay_report_gst  ON  credit_debit_note . comp_code  =  pay_report_gst . comp_code AND  credit_debit_note . original_bill_no  =  pay_report_gst . invoice_no  where credit_debit_note.Type='" + status + "' and `original_bill_no` in(" + invoice_list + ")";
            
            MySqlDataAdapter adp1 = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            adp1.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                if (ddl_note_type.SelectedValue == "1")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=CREDIT_NOTE_REPORT_"+ ddl_client.SelectedItem.Text.Replace(" ","_") + ".xls");
                }
                else if (ddl_note_type.SelectedValue == "2")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=DEBIT_NOTE_REPORT_" + ddl_client.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds, status, manual_invoice_type);
                Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds, status, manual_invoice_type);
                Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null, status, manual_invoice_type);
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
                gv_show.Visible = true;
                button.Visible = true;
                return;
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

     // for clientwise excel 01-06-2020 komal

    protected void btn_excel_clientwise_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try
        {
            //string invoice_date = "" + txt_invoice_date.Text + "";

            //if (invoice_date == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Invoice Date !!');", true);
            //    btn_final_invoice.Focus();
            //    return;
            //}



            string sql = null;
            string status = " ";

            string manual_invoice_type = "" + ddl_manual_invoice_type.SelectedValue + "";
            if (ddl_manual_invoice_type.SelectedValue=="0")
            {

                if (ddl_client_manual.SelectedValue=="DHFL")
                {
                
                     sql = " select client_name,state_name, invoice_no,DATE_FORMAT(invoice_date, '%d/%m/%Y') AS 'invoice_date',amount,cgst,sgst,igst,manual_grand_total,gst_no from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' and manual_region = '"+ddlregion_manual.SelectedValue+"'";
                
                }
                else
                {

            //sql = " select client_name,state_name, invoice_no,invoice_date,amount,cgst,sgst,igst,(cgst+sgst+igst) as 'total',gst_no from pay_report_gst where comp_code = '"+Session["comp_code"].ToString()+"' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual'";
                sql = " select client_name,state_name, invoice_no,DATE_FORMAT(invoice_date, '%d/%m/%Y') AS 'invoice_date',amount,cgst,sgst,igst,manual_grand_total,gst_no from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "'";
                }
            }
           
            else if (ddl_manual_invoice_type.SelectedValue=="1")
            {
                sql = " select state_name,invoice_no,DATE_FORMAT(invoice_date, '%d/%m/%Y') AS 'invoice_date', invoice_category,bill_shipping_add,amount,cgst,sgst,igst,manual_grand_total,gst_no,gst_address,client_name from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and state_name = '" + ddl_state_name_other.SelectedValue + "'  and `manual_month` = '" + txt_manual_date.Text + "' ";
            
            }
            MySqlDataAdapter adp1 = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            adp1.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                if (ddl_manual_invoice_type.SelectedValue == "0")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=CLIENTWISE_MANUAL_EXCEL_" + ddl_client.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }
                else if (ddl_manual_invoice_type.SelectedValue == "1")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=OTHER_MANUAL_EXCEL_" + ddl_state_name_other.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds,status, manual_invoice_type);
                Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds,status, manual_invoice_type);
                Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null,status, manual_invoice_type);
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
        catch(Exception ex){throw ex;}
        finally
        {
            d.con1.Close();
        }

    }


    //end

    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string status;
        string manual_invoice_type;
        int i = 0;
        double txt_amt = 0, cgst = 0, sgst = 0 ,igst=0,manual_grand_total=0,Total=0;
        static int ctr;
        public MyTemplate1(ListItemType type, DataSet ds, string status ,string manual_invoice_type )
        {
            this.type = type;
            this.ds = ds;
            this.status = status;
            this.manual_invoice_type = manual_invoice_type;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:

                    // manual clientwise and other excel 01-06-2020 komal

                    if (manual_invoice_type!="")
                    {
                         if (manual_invoice_type=="0")
                         {

                             lc = new LiteralControl("<table border=1><table border=1><th bgcolor=yellow colspan=11 align=center> Clientwise Manual Invoice </th><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>STATE NAME</th><th>INVOICE NO</th><th>INVOICE DATE</th><th>GST NO</th><th>AMOUNT</th><th>CGST</th><th>SGST</th><th>IGST</th><th>TOTAL</th></tr>");

                         }
                         else if (manual_invoice_type == "1")
                         {

                             lc = new LiteralControl("<table border=1><table border=1><th bgcolor=yellow colspan=12 align=center> Other Manual Invoice </th><tr><th>SR. NO.</th><th>INVOICE NO</th><th>INVOICE DATE</th><th>BILL TO PARTY</th><th>STATE NAME</th><th>GST ADDRESS</th><th>GST NO</th><th>AMOUNT</th><th>CGST</th><th>SGST</th><th>IGST</th><th>TOTAL</th></tr>");
                             
                          }

                    
                    }

                         /// end 01-06-2020 komal

                    else if (manual_invoice_type == "")
                    
                    {

                    if (status == "1")
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=16 align=center>" + ds.Tables[0].Rows[ctr]["client_name"] + "</th><table border=1><th bgcolor=yellow colspan=16 align=center> CREDIT NOTE </th><tr><th>SR. NO.</th><th>BILLING TYPE</th><th>CREDIT NOTE NO</th><th>CREDIT NOTE DATE</th><th>CREDIT NOTE MONTH</th><th>CREDIT NOTE YEAR</th><th>ORIGINAL BILL NO</th><th>ORIGINAL BILL DATE</th><th>CLIENT NAME</th><th>GST NO</th><th>STATE NAME</th><th>TAXABLE AMOUNT</th><th>CGST 9%</th><th>SGST 9%</th><th>IGST 18%</th><th>TOTAL</th></tr>");
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=16 align=center>" + ds.Tables[0].Rows[ctr]["client_name"] + "</th><table border=1><th bgcolor=yellow colspan=16 align=center> DEBIT NOTE </th><tr><th>SR. NO.</th><th>BILLING TYPE</th><th>DEBIT NOTE NO</th><th>DEBIT NOTE DATE</th><th>DEBIT NOTE YEAR</th><th>DEBIT NOTE YEAR</th><th>ORIGINAL BILL NO</th><th>ORIGINAL BILL DATE</th><th>CLIENT NAME</th><th>GST NO</th><th>STATE NAME</th><th>TAXABLE AMOUNT</th><th>CGST 9%</th><th>SGST 9%</th><th>IGST 18%</th><th>TOTAL</th></tr>");
                    }


                    }
                    break;
                case ListItemType.Item:

                    // manual clientwise and other excel 01-06-2020 komal

                    if (manual_invoice_type != "")

                    {
                        if (manual_invoice_type == "0")
                        {

                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["manual_grand_total"] + "</td></tr>");

                        }
                        else if (manual_invoice_type == "1")

                        {
                            lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst_address"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["manual_grand_total"] + "</td></tr>");
                        }
                    }

                    /// end 01-06-2020 komal
                    /// 
                    if (manual_invoice_type == "")
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["credit_note_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["credit_note_date"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["month"] + "</td><td>" + ds.Tables[0].Rows[ctr]["year"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["original_bill_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bill_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["taxable_amt"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total"] + "</td></tr>");

                        txt_amt = txt_amt + double.Parse(ds.Tables[0].Rows[ctr]["taxable_amt"].ToString());
                        cgst = cgst + double.Parse(ds.Tables[0].Rows[ctr]["cgst"].ToString());
                        sgst = sgst + double.Parse(ds.Tables[0].Rows[ctr]["sgst"].ToString());
                        igst = igst + double.Parse(ds.Tables[0].Rows[ctr]["igst"].ToString());
                        Total = Total + double.Parse(ds.Tables[0].Rows[ctr]["Total"].ToString());
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 11>Total</td></b><td>" + Math.Round((txt_amt), 2) + "</td><td>" + Math.Round((cgst), 2) + "</td><td>" + Math.Round((sgst), 2) + "</td><td>" + Math.Round((igst), 2) + "</td><td>" + Math.Round((Total), 2) + "</td></tr>";
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
    protected void status_gv()
    {
        MySqlCommand cmd1 = null;
        if (ddl_client.SelectedValue != "Select")
        {
            cmd1 = new MySqlCommand("SELECT  case when credit_debit_note . type=1 then 'Credit' when credit_debit_note.type='2' then 'Debit' end as 'Type'  ,credit_note_no ,credit_debit_note.`client_name`,credit_debit_note . month ,credit_debit_note . year ,original_bill_no , DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'bill_date',credit_debit_note . gst_no , pay_report_gst . state_name ,taxable_amt ,credit_debit_note . cgst ,credit_debit_note . sgst , credit_debit_note . Igst , Total FROM credit_debit_note INNER JOIN  pay_report_gst  ON  credit_debit_note . comp_code  =  pay_report_gst . comp_code  AND  credit_debit_note . original_bill_no  =  pay_report_gst . invoice_no  where credit_debit_note.client_name='" + ddl_client.SelectedItem + "' and credit_debit_note.comp_code='" + Session["COMP_CODE"].ToString() + "' ", d1.con);
        }
        else
        {
            cmd1 = new MySqlCommand("SELECT  case when credit_debit_note . type=1 then 'Credit' when credit_debit_note.type='2' then 'Debit' end as 'Type'  ,credit_note_no ,credit_debit_note.`client_name`,credit_debit_note . month ,credit_debit_note . year ,original_bill_no , DATE_FORMAT( bill_date , '%d/%m/%Y') AS 'bill_date',credit_debit_note . gst_no , pay_report_gst . state_name ,taxable_amt ,credit_debit_note . cgst ,credit_debit_note . sgst , credit_debit_note . Igst , Total FROM credit_debit_note INNER JOIN  pay_report_gst  ON  credit_debit_note . comp_code  =  pay_report_gst . comp_code  AND  credit_debit_note . original_bill_no  =  pay_report_gst . invoice_no  where credit_debit_note.comp_code='" + Session["COMP_CODE"].ToString() + "'  ", d1.con);
        }
        d1.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {

                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_status.DataSource = dt;
                gv_status.DataBind();
                gv_status.Visible = true;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_status.DataSource = dt;
                gv_status.DataBind();
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }

    }
    protected void btn_cr_invoice_Click(object sender, EventArgs e)
    {
        try
        {
            hidtab.Value = "0";
            read_the_rpt_data(1);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            d.con1.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            // File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "client_bill_invoice_club_bajaj.pdf");
        }
    }
   
    protected void btn_cr_final_invoice_Click(object sender, EventArgs e)
    {
        try
        {
            hidtab.Value = "0";
            read_the_rpt_data(2);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            d.con1.Close();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            // File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "client_bill_invoice_club_bajaj.pdf");
        }
    }

    protected void read_the_rpt_data(int counter)
    {
        try
        {
            string headerpath = null;
            string footerpath = null;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataAdapter cmd_item = null;

            //d.con.Open();
            string invoice_list = "";
            foreach (GridViewRow gvrow in gv_invoice_details.Rows)
            {
                string Invoice_no = (string)gv_invoice_details.DataKeys[gvrow.RowIndex].Value;
                var checkbox = gvrow.FindControl("chk_invoice1") as CheckBox;
              
                if (checkbox.Checked == true)
                {
                    invoice_list = invoice_list + "'" + Invoice_no + "',";

                    //invoice_list = invoice_list.Replace("'", "  ");

                    // b_amt = b_amt + "'" + Balanced_amount + "'";
                }
                else
                {
                    Invoice_no = "";
                }
                string check_final_flag = "";

                if (Invoice_no != "")
                {
                    string txt_credit_not_date = ((TextBox)gvrow.FindControl("txt_credit_not_date")).Text;
                    if (counter == 2)
                    {
                        if (txt_credit_not_date != "")
                        {
                            check_final_flag = d.getsinglestring("select Final_invoice_flag from credit_debit_note where client_name = '" + ddl_client.SelectedValue + "' AND `month` = '" + txt_credit_not_date.Substring(3, 2) + "' AND `year` = '" + txt_credit_not_date.Substring(6) + "' AND `original_bill_no` IN ('" + Invoice_no + "') and type='" + ddl_note_type.SelectedValue + "'");
                        }
                       if(check_final_flag !="1")
                       {
                           d.operation(" UPDATE `credit_debit_note` SET `Final_invoice_flag` = '1' WHERE `client_name` = '" + ddl_client.SelectedItem + "' AND `month` = '" + txt_credit_not_date.Substring(3, 2) + "' AND `year` = '" + txt_credit_not_date.Substring(6) + "' AND `original_bill_no` in ('" + Invoice_no + "') and type='" + ddl_note_type.SelectedValue + "'");
                        
                       }
                      
                    }
                  
                }
            }
            if (invoice_list == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select Invoice !!!')", true);
                gv_show.Visible = true;
                button.Visible = true;
                return;
            }
            if (invoice_list.Length > 0)
            {
                invoice_list = invoice_list.Substring(0, invoice_list.Length - 1);
            }
            else { invoice_list = "''"; }

            string[] invoice_no = invoice_list.Split(',');
            d.con1.Open();
            string check_no_db = d.getsinglestring("SELECT original_bill_no FROM  credit_debit_note  WHERE  client_name  = '" + ddl_client.SelectedItem + "'  and original_bill_no in(" + invoice_list + ")");
            if (check_no_db == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found... !!!');", true);
                gv_show.Visible = true;
                button.Visible = true;
                return;
            }
            ReportDocument crystalReport = new ReportDocument();
            string query = null;
            string path1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
            if (ddl_note_type.SelectedValue == "1")
            {
                crystalReport.Load(Server.MapPath("~/credit_note.rpt"));
            }
            else if (ddl_note_type.SelectedValue == "2")
            {
                crystalReport.Load(Server.MapPath("~/debit_note.rpt"));
            }
            string downloadname = "Invoice";
            //for state
            if (counter == 1)
            {
               // query = "(SELECT credit_debit_note . comp_code ,credit_debit_note . client_name ,credit_debit_note . original_bill_no  AS 'Expr1', DATE_FORMAT( credit_debit_note . bill_date , '%d/%m/%Y') AS 'unit_city', COMPANY_NAME ,COMP_ADDRESS1  AS 'ADDRESS1',COMP_ADDRESS2  AS 'ADDRESS2',COMP_CITY  AS 'CITY',COMP_STATE  AS 'STATE',PF_REG_NO ,COMPANY_PAN_NO ,COMPANY_TAN_NO ,COMPANY_CIN_NO ,SERVICE_TAX_REG_NO ,ESIC_REG_NO , `pay_report_gst`.`state_name` AS 'STATE_NAME',UNIT_full_ADD1  AS 'UNIT_ADD1','' AS 'UNIT_ADD2',unit_city  AS 'UNIT_CITY',unit_gst_no ,grade_desc  AS 'Expr1',fromtodate  AS 'start_end_date',SUM( tot_days_present ) AS 'TOT_DAYS_PRESENT',month_days ,(ROUND( credit_debit_note . CGST , 2)) AS 'CGST',(ROUND( credit_debit_note . SGST , 2)) AS 'SGST',(ROUND( credit_debit_note . IGST , 2)) AS 'IGST',((ROUND(( credit_debit_note . taxable_amt )+ ( credit_debit_note . CGST ) + ( credit_debit_note . SGST )+ ( credit_debit_note . igst )))) AS 'total', CONCAT('January', ' ', '2020') AS 'month',housekeeiing_sac_code ,Security_sac_code ,pay_billing_unit_rate_history . grade_code  AS 'designation'FROM credit_debit_note  INNER JOIN  pay_billing_unit_rate_history  ON  credit_debit_note . comp_code  =  pay_billing_unit_rate_history . comp_code   INNER JOIN `pay_report_gst` ON `credit_debit_note`.`original_bill_no` = `pay_report_gst`.`invoice_no`  WHERE credit_debit_note . original_bill_no  in (" + invoice_list + ") and credit_debit_note.type='" + ddl_note_type.SelectedValue + "'   group by original_bill_no)";
                query = "(SELECT credit_debit_note . comp_code ,credit_debit_note . client_name ,credit_debit_note . original_bill_no  AS 'Expr1', DATE_FORMAT( credit_debit_note . bill_date , '%d/%m/%Y') AS 'unit_city', COMPANY_NAME ,COMP_ADDRESS1  AS 'ADDRESS1',COMP_ADDRESS2  AS 'ADDRESS2',COMP_CITY  AS 'CITY',COMP_STATE  AS 'STATE',PF_REG_NO ,COMPANY_PAN_NO ,COMPANY_TAN_NO ,COMPANY_CIN_NO ,SERVICE_TAX_REG_NO , `state_name` AS 'STATE_NAME',UNIT_full_ADD1  AS 'UNIT_ADD1','' AS 'UNIT_ADD2',unit_city  AS 'UNIT_CITY',unit_gst_no ,grade_desc  AS 'Expr1',fromtodate  AS 'start_end_date', tot_days_present  AS 'TOT_DAYS_PRESENT',month_days ,(ROUND( credit_debit_note . CGST , 2)) AS 'month',(ROUND( credit_debit_note . SGST , 2)) AS 'year', (ROUND(`credit_debit_note`.`IGST`, 2)) AS 'ESIC_REG_NO',(ROUND(`credit_debit_note`.`taxable_amt`,2)) AS 'grand_total',(ROUND(`credit_debit_note`.`total`,2)) AS 'total',housekeeiing_sac_code ,Security_sac_code ,if(pay_report_gst.type='conveyance','No Of Manpower','No Of Paid Days') as 'designation' FROM credit_debit_note  INNER JOIN `pay_report_gst` ON `credit_debit_note`.`original_bill_no` = `pay_report_gst`.`invoice_no`  WHERE credit_debit_note . original_bill_no  in (" + invoice_list + ") and credit_debit_note.type='" + ddl_note_type.SelectedValue + "'   group by original_bill_no)";
            }
            else if (counter == 2)
            {
                query = "(SELECT credit_debit_note . comp_code ,credit_debit_note . client_name ,credit_debit_note . original_bill_no  AS 'Expr1', DATE_FORMAT( credit_debit_note . bill_date , '%d/%m/%Y') AS 'unit_city',credit_debit_note . credit_note_no  AS 'zone',DATE_FORMAT( credit_debit_note . credit_note_date , '%d/%m/%Y') AS 'type', COMPANY_NAME ,COMP_ADDRESS1  AS 'ADDRESS1',COMP_ADDRESS2  AS 'ADDRESS2',COMP_CITY  AS 'CITY',COMP_STATE  AS 'STATE',PF_REG_NO ,COMPANY_PAN_NO ,COMPANY_TAN_NO ,COMPANY_CIN_NO ,SERVICE_TAX_REG_NO , `state_name` AS 'STATE_NAME',UNIT_full_ADD1  AS 'UNIT_ADD1','' AS 'UNIT_ADD2',unit_city  AS 'UNIT_CITY',unit_gst_no ,grade_desc  AS 'Expr1',fromtodate  AS 'start_end_date', tot_days_present AS 'TOT_DAYS_PRESENT',month_days ,(ROUND( credit_debit_note . CGST , 2)) AS 'month',(ROUND( credit_debit_note . SGST , 2)) AS 'year', (ROUND(`credit_debit_note`.`IGST`, 2)) AS 'ESIC_REG_NO',(ROUND(`credit_debit_note`.`taxable_amt`,2)) AS 'grand_total',(ROUND(`credit_debit_note`.`total`,2)) AS 'total', CONCAT('January', ' ', '2020') AS 'month',housekeeiing_sac_code ,Security_sac_code  ,if(pay_report_gst.type='conveyance','No Of Manpower','No Of Paid Days') as 'designation' FROM credit_debit_note     INNER JOIN `pay_report_gst` ON `credit_debit_note`.`original_bill_no` = `pay_report_gst`.`invoice_no`  WHERE credit_debit_note . original_bill_no  in (" + invoice_list + ") and credit_debit_note.type='" + ddl_note_type.SelectedValue + "' group by original_bill_no)";
            }
            //query = "(SELECT credit_debit_note . comp_code ,credit_debit_note . client_name ,credit_debit_note . credit_note_no  AS 'zone',DATE_FORMAT( credit_debit_note . credit_note_date , '%d/%m/%Y') AS 'type',credit_debit_note . original_bill_no  AS 'Expr1', DATE_FORMAT( credit_debit_note . bill_date , '%d/%m/%Y') AS 'unit_city', COMPANY_NAME ,COMP_ADDRESS1  AS 'ADDRESS1',COMP_ADDRESS2  AS 'ADDRESS2',COMP_CITY  AS 'CITY',COMP_STATE  AS 'STATE',PF_REG_NO ,COMPANY_PAN_NO ,COMPANY_TAN_NO ,COMPANY_CIN_NO ,SERVICE_TAX_REG_NO ,ESIC_REG_NO , UNIT_full_ADD1  AS 'UNIT_ADD1','' AS 'UNIT_ADD2',unit_city  AS 'UNIT_CITY',unit_gst_no ,grade_desc  AS 'Expr1',fromtodate  AS 'start_end_date',SUM( tot_days_present ) AS 'TOT_DAYS_PRESENT',month_days ,(ROUND( credit_debit_note . CGST , 2)) AS 'CGST',(ROUND( credit_debit_note . SGST , 2)) AS 'SGST',(ROUND( credit_debit_note . IGST , 2)) AS 'IGST',((ROUND(( credit_debit_note . taxable_amt )+ ( credit_debit_note . CGST ) + ( credit_debit_note . SGST )+ ( credit_debit_note . igst )))) AS 'total', CONCAT('January', ' ', '2020') AS 'month',housekeeiing_sac_code ,Security_sac_code ,pay_billing_unit_rate_history . grade_code  AS 'designation'FROM credit_debit_note  INNER JOIN  pay_billing_unit_rate_history  ON  credit_debit_note . comp_code  =  pay_billing_unit_rate_history . comp_code    WHERE credit_debit_note . original_bill_no  in (" + invoice_list + ") and credit_debit_note.type='" + ddl_note_type.SelectedValue + "'   group by original_bill_no)";
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            sda = cmd.ExecuteReader();
            dt.Load(sda);

            if (Session["COMP_CODE"].ToString() == "C02")
            {
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header.png");
                footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath"].Text = @"'" + headerpath + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
            }
            else
            {
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header.png");
                footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath"].Text = @"'" + headerpath + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
            }



            PageMargins margins;
            if (dt.Rows.Count > 0)
            {

                // Get the PageMargins structure and set the 
                // margins for the report.
                margins = crystalReport.PrintOptions.PageMargins;
                margins.bottomMargin = 0;
                margins.leftMargin = 350;
                margins.rightMargin = 0;
                margins.topMargin = 0;
                // Apply the page margins.
                crystalReport.PrintOptions.ApplyPageMargins(margins);
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, downloadname);
                crystalReport.Close();
                crystalReport.Clone();
                crystalReport.Dispose();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
                gv_show.Visible = true;
                button.Visible = true;
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

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
  
    //manual invoice code by jyosna
    protected void ddl_client_manual_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {

            MySqlDataAdapter grd_client = null;
            if (ddl_client_manual.SelectedValue == "DHFL")
            {
                grd_client = new MySqlDataAdapter("SELECT DISTINCT( STATE_NAME ) FROM  pay_unit_master  INNER JOIN  pay_zone_master  ON  pay_unit_master . comp_code  =  pay_zone_master . comp_code  AND  pay_unit_master . client_code  =  pay_zone_master . client_code  WHERE pay_zone_master. comp_code  = '" + Session["comp_code"].ToString() + "' AND pay_zone_master. client_code  = '" + Session["client_code"].ToString() + "' AND pay_zone_master. type  = 'region' ORDER BY 1", d.con);
            }

            else
            {
               grd_client = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client_manual.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_manual.SelectedValue + "')  ORDER BY STATE", d.con);

            }
                DataTable dt_client = new DataTable();
            grd_client.Fill(dt_client);
            if (dt_client.Rows.Count > 0)
            {

                ddl_state.DataSource = dt_client;
                ddl_state.DataTextField = dt_client.Columns[0].ToString();
                ddl_state.DataValueField = dt_client.Columns[0].ToString();
                ddl_state.DataBind();
            }

            region();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_state.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    }

    // region for dhfl komal changes 28-07-2020

    protected void region()
    {
        if (ddl_client_manual.SelectedValue != "Select")
        {
            ddlregion_manual.Items.Clear();
            System.Data.DataTable dt_item2 = new System.Data.DataTable();

            MySqlDataAdapter cmd_item2 = new MySqlDataAdapter("SELECT DISTINCT  pay_zone_master.region FROM pay_client_billing_details INNER JOIN pay_zone_master  ON  pay_client_billing_details . comp_code  =  pay_zone_master . comp_code  AND  pay_client_billing_details . client_code  =  pay_zone_master . client_code  WHERE  pay_client_billing_details . client_code  = '" + ddl_client_manual.SelectedValue + "' and type = 'Region' ", d5.con);
            //MySqlDataAdapter cmd_item1 = new MySqlDataAdapter(" Select DISTINCT region from pay_zone_master where comp_code ='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' ", d.con);

            d5.con.Open();
            try
            {
                cmd_item2.Fill(dt_item2);
                if (dt_item2.Rows.Count > 0)
                {
                    ddlregion_manual.DataSource = dt_item2;
                    ddlregion_manual.DataTextField = dt_item2.Columns[0].ToString();
                    ddlregion_manual.DataValueField = dt_item2.Columns[0].ToString();
                    ddlregion_manual.DataBind();
                }
                dt_item2.Dispose();
                d.con.Close();
                ddlregion_manual.Items.Insert(0, "Select");
                ddlregion_manual.Items.Insert(1, "ALL");
                //  ddl_state_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d5.con.Close();
            }

        }
    }

    // end 
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";

        try
        {
            d.con.Open();
            txt_gst_no.Text = "";
            MySqlCommand cmd_item = new MySqlCommand("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and type ='GST' and client_code = '" + ddl_client_manual.SelectedValue + "' and `REGION` = '" + ddl_state.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();
            if (dr.Read())
            {
                txt_gst_no.Text = dr.GetValue(0).ToString();
            }


        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }

    protected void btn_save1_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";


        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        try
        {
           string same_record =null;

            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                if (ddl_client_manual.SelectedValue=="DHFL")
                {

                    same_record = d.getsinglestring("select client_code, state_name,`manual_invoice_type`,manual_month from pay_report_gst where  comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' and manual_region = '" + ddlregion_manual.SelectedValue + "' ");
                }
                else
                {

                same_record = d.getsinglestring("select client_code, state_name,`manual_invoice_type`,manual_month from pay_report_gst where  comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' ");
                }
                if (same_record!="") 
                
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Record All ready Added.. !!!');", true);
                    return;
                }
            
            }
            else if (ddl_manual_invoice_type.SelectedValue == "1")
            {
                same_record = d.getsinglestring("select client_code, state_name,`manual_invoice_type`,manual_month from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and state_name = '" + ddl_state_name_other.SelectedValue + "'  and `manual_month` = '" + txt_manual_date.Text + "' ");

                if (same_record != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Record All ready Added.. !!!');", true);
                    return;
                }
 

            }

            if (ddl_manual_invoice_type.SelectedValue=="0")
            {
                
                //int result = d.operation("insert into pay_report_gst (comp_code,client_code,client_name,state_name,invoice_no,invoice_date,gst_no,amount,cgst,sgst,igst,sac_code,type,manual_invoice_type,gst_to_be,manual_grand_total,month,year)values('" + Session["comp_code"].ToString() + "','" + ddl_client_manual.SelectedValue + "','" + ddl_client_manual.SelectedItem + "','" + ddl_state.SelectedValue + "' ,'" + txt_invoice_no.Text + "',str_to_date('" + txt_invoice_date.Text + "','%d/%m/%Y') ,'" + txt_gst_no.Text + "','" + txt_amount.Text + "','" + txt_cgst.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_sac_code.Text + "','manual','" + ddl_manual_invoice_type.SelectedValue + "','" + ddl_gst_payed.SelectedValue + "','" + txt_grand_total.Text + "','" + txt_manual_date.Text.Substring(0, 2) + "', '" + txt_manual_date.Text.Substring(3) + "')");
                int result = d.operation("insert into pay_report_gst (comp_code,client_code,client_name,state_name,invoice_no,gst_no,amount,cgst,sgst,igst,sac_code,type,manual_invoice_type,gst_to_be,manual_grand_total,month,year,manual_month,manual_region)values('" + Session["comp_code"].ToString() + "','" + ddl_client_manual.SelectedValue + "','" + ddl_client_manual.SelectedItem + "','" + ddl_state.SelectedValue + "' ,'" + txt_invoice_no.Text + "','" + txt_gst_no.Text + "','" + txt_amount.Text + "','" + txt_cgst.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_sac_code.Text + "','manual','" + ddl_manual_invoice_type.SelectedValue + "','" + ddl_gst_payed.SelectedValue + "','" + txt_grand_total.Text + "','" + txt_manual_date.Text.Substring(0, 2) + "', '" + txt_manual_date.Text.Substring(3) + "','" + txt_manual_date.Text + "','" + ddlregion_manual.SelectedValue + "')");
       
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Manual Invoice Clientwise Details Successfully Added... !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Manual Invoice Clientwise Details Addition Failed... !!!');", true);
            }
         }
            else if (ddl_manual_invoice_type.SelectedValue == "1")
            {
                //int result = d.operation("insert into pay_report_gst (comp_code,client_code,client_name,state_name,invoice_no,invoice_date,gst_no,amount,cgst,sgst,igst,sac_code,type)values('" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_client.SelectedItem + "','" + ddl_state.SelectedValue + "','" + txt_invoice_no.Text + "', date_format('" + txt_invoice_date.Text + "','%d-%m-%Y') ,'" + txt_gst_no.Text + "','" + txt_amount.Text + "','" + txt_cgst.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_sac_code.Text + "','manual')");
                int result = d.operation("insert into pay_report_gst (comp_code,state_name,gst_no,amount,cgst,sgst,igst,sac_code,type,invoice_category,gst_address,bill_shipping_add,client_name,manual_invoice_type,`invoice_no`,gst_to_be,manual_grand_total,month,year,manual_month)values('" + Session["comp_code"].ToString() + "','" + ddl_state_name_other.SelectedValue + "','" + txt_gst_no_other.Text + "' ,'" + txt_amount.Text + "','" + txt_cgst.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_sac_code.Text + "','manual','" + txt_invoice_category.Text + "','" + txt_gst_add.Text + "','" + txt_bill_add.Text + "','" + txt_bill_party.Text + "','" + ddl_manual_invoice_type.SelectedValue + "','" + txt_invoice_no.Text + "','" + ddl_gst_payed.SelectedValue + "','" + txt_grand_total.Text + "','" + txt_manual_date.Text.Substring(0, 2) + "', '" + txt_manual_date.Text.Substring(3) + "','" + txt_manual_date.Text + "' )");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Manual Invoice Other Details Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Manual Invoice Other Details Addition Failed... !!!');", true);
                }

                other_client_code();
            }

           // text_clear();
        }
        catch (Exception ee) { throw ee; }
        finally
        {
            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                manual_invoice_details();
            }else
                if (ddl_manual_invoice_type.SelectedValue == "1")
                {
                    other_invoice_details();
                }
            text_clear();
        }


    }

    protected void manual_invoice_details()
    {
        gv_other_invoice.DataSource = null;
        gv_other_invoice.DataBind();

        gv_manual_invoice.DataSource = null;
        gv_manual_invoice.DataBind();

        // for clientwise invoice type
        if (ddl_manual_invoice_type.SelectedValue == "0")
        {
            //  MySqlCommand cmd1 = new MySqlCommand("select id,client_name,state_name,invoice_no,invoice_date,amount from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name ='" + ddl_state.SelectedValue + "' and gst_no = '" + txt_gst_no.Text + "' and invoice_no = '" + txt_invoice_no.Text + "'  ", d1.con);
            MySqlCommand cmd1 = new MySqlCommand("select id,client_name,state_name,invoice_no,date_format(invoice_date,'%d-%m-%Y') as 'invoice_date',amount,manual_grand_total,igst,cgst,sgst,manual_month, case when final_invoice = '0' then 'Pending' when final_invoice = '1' then 'Final Invoice' when final_invoice ='2' then 'Approve By MD' end as 'final_invoice_check' ,final_invoice,CASE WHEN `manual_invoice_type` = '0' THEN 'Clientwise' WHEN `manual_invoice_type` = '1' THEN 'Other' END AS 'manual_invoice_type', CASE WHEN `gst_to_be` = 'R' THEN 'Regular' WHEN `gst_to_be` = 'SEWP' THEN 'SEZ supplies with payment'  WHEN `gst_to_be` = 'SEWOP' THEN 'SEZ supplies without payment'  WHEN `gst_to_be` = 'DE' THEN 'Deemed Exp' WHEN `gst_to_be` = 'SCU' THEN 'Supplies covered under section 7 of IGST Acts' END AS 'gst_to_be', manual_region  from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and type='manual' and manual_invoice_type = '0' and manual_month = '" + txt_manual_date.Text + "' order by id desc ", d1.con);


        d1.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {

                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_manual_invoice.DataSource = dt;
                gv_manual_invoice.DataBind();
                gv_manual_invoice.Visible = true;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_manual_invoice.DataSource = dt;
                gv_manual_invoice.DataBind();
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }
	}

         

    }

    protected void other_invoice_details() 
    {
         gv_manual_invoice.DataSource = null;
         gv_manual_invoice.DataBind();


         //  MySqlCommand cmd1 = new MySqlCommand("select id,client_name,state_name,invoice_no,invoice_date,amount from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name ='" + ddl_state.SelectedValue + "' and gst_no = '" + txt_gst_no.Text + "' and invoice_no = '" + txt_invoice_no.Text + "'  ", d1.con);
         MySqlCommand cmd1 = new MySqlCommand("select id,state_name,invoice_category,gst_address,bill_shipping_add,client_name,gst_no,gst_address,amount,manual_grand_total,invoice_no,date_format(invoice_date,'%d-%m-%Y') as 'invoice_date',igst,cgst,sgst,manual_month , case when final_invoice = '0' then 'Pending' when final_invoice = '1' then 'Final Invoice' when final_invoice ='2' then 'Approve By MD' end as 'final_invoice_check' ,final_invoice ,CASE WHEN `manual_invoice_type` = '0' THEN 'Clientwise' WHEN `manual_invoice_type` = '1' THEN 'Other' END AS 'manual_invoice_type', CASE WHEN `gst_to_be` = 'R' THEN 'Regular' WHEN `gst_to_be` = 'SEWP' THEN 'SEZ supplies with payment'  WHEN `gst_to_be` = 'SEWOP' THEN 'SEZ supplies without payment'  WHEN `gst_to_be` = 'DE' THEN 'Deemed Exp' WHEN `gst_to_be` = 'SCU' THEN 'Supplies covered under section 7 of IGST Acts' END AS 'gst_to_be' from pay_report_gst where comp_code='" + Session["comp_code"].ToString() + "' and type='manual' and manual_invoice_type = '1' and manual_month = '" + txt_manual_date.Text + "' order by id desc ", d1.con);

         d1.con.Open();
         try
         {
             MySqlDataReader dr1 = cmd1.ExecuteReader();
             if (dr1.HasRows)
             {

                 DataTable dt = new DataTable();
                 dt.Load(dr1);
                 gv_other_invoice.DataSource = dt;
                 gv_other_invoice.DataBind();
                 gv_other_invoice.Visible = true;
             }
             else
             {
                 DataTable dt = new DataTable();
                 dt.Load(dr1);
                 gv_other_invoice.DataSource = dt;
                 gv_other_invoice.DataBind();
             }
             dr1.Close();



         }

         catch (Exception ex) { throw ex; }
         finally { d1.con.Close(); }
        
    
    }
    protected void gv_manual_invoice_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (dr["final_invoice"].ToString() != "0")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;


              //  e.Row.Cells[14].Visible = false;
               // e.Row.Cells[15].Visible = false;

                LinkButton lb1 = e.Row.FindControl("btn_edit") as LinkButton;
                lb1.Visible = false;

                LinkButton lb2 = e.Row.FindControl("lnk_remove_manual_invoice") as LinkButton;
                lb2.Visible = false;


            

            }
        }
      

        e.Row.Cells[0].Visible = false;
        e.Row.Cells[17].Visible = false;
       
    }
    protected void gv_manual_invoice_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_manual_invoice.UseAccessibleHeader = false;
            gv_manual_invoice.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void gv_manual_invoice_SelectedIndexChanged(object sender, EventArgs e)
    {

        // MySqlCommand cmd2 = new MySqlCommand("SELECT id,client_code,client_name,invoice_no,invoice_date,amount from pay_report_gst WHERE  ID='" + gv_manual_invoice.SelectedRow.Cells[0].Text+"'", d.con);
        //d.con.Open();
        // try
        // {
        //     MySqlDataReader dr2 = cmd2.ExecuteReader();
        //   if (dr2.Read())
        //     {
        //         ViewState["ID"] = dr2[0].ToString();
        //         ddl_client.SelectedValue = dr2[1].ToString();
        //         ddl_client.SelectedValue = dr2[2].ToString();
        //         ddl_state.SelectedValue = dr2[3].ToString();
        //         txt_invoice_no.Text = dr2[4].ToString();
        //         txt_invoice_date.Text = dr2[5].ToString();
        //         txt_amount.Text = dr2[6].ToString();

        //     }
        //     dr2.Close();
        //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        // }
        // catch (Exception ex) { throw ex; }
        // finally
        // {
        //     d.con.Close();
        //     btn_update.Visible = true;

        // }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        //  int res = 0;
        try
        {


            //  int dispatch2 = d.operation("update pay_report_gst set  invoice_date='" + txt_invoice_date.Text + "',gst_no='" + txt_gst_no.Text + "',amount='" + txt_amount.Text.ToString() + "',cgst='" + txt_cgst.Text.ToString() + "',sgst='" + txt_sgst.Text.ToString() + "',igst='" + txt_igst.Text.ToString() + "',sac_code='" + txt_sac_code.Text.ToString() + "'  where id = " + ViewState["id"].ToString());


            int dispatch2 = d.operation("update pay_report_gst set  gst_no='" + txt_gst_no.Text + "',amount='" + txt_amount.Text + "',cgst='" + txt_cgst.Text + "',sgst='" + txt_sgst.Text + "',igst='" + txt_igst.Text + "',sac_code='" + txt_sac_code.Text + "',manual_region = '" + ddlregion_manual.SelectedValue + "'  where id = '" + txt_id.Text + "'");


            if (dispatch2 > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successfully !!');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('record Updated UpdatedFailed. !!');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            btn_save.Visible = true;
            btn_update.Visible = false;
            manual_invoice_details();
            text_clear();

        }

    }
    protected void lnk_remove_manual_invoice_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_report_gst where id = '" + grdrow.Cells[0].Text + "'");

        manual_invoice_details();
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;
        //MySqlCommand cmd2 = new MySqlCommand("SELECT client_code,invoice_no,date_format(invoice_date,'%d-%m-%Y') as 'invoice_date',state_name, amount,cgst,sgst,igst,sac_code,id,gst_to_be,manual_grand_total from pay_report_gst WHERE  ID='" + id + "'", d3.con);
        MySqlCommand cmd2 = new MySqlCommand("SELECT client_code,invoice_no,state_name, amount,cgst,sgst,igst,sac_code,id,gst_to_be,manual_grand_total,manual_month,manual_invoice_type,manual_region from pay_report_gst WHERE  ID='" + id + "'", d3.con);
        
        d3.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                //  id = dr2.GetValue(0).ToString();

                ddl_client_manual.SelectedValue = dr2.GetValue(0).ToString();
                ddl_client_manual_SelectedIndexChanged(null, null);

                ddlregion_manual.SelectedValue = dr2.GetValue(13).ToString();
                ddlregion_manual_SelectedIndexChanged(null,null);

                ddl_state.SelectedValue = dr2.GetValue(2).ToString();
                ddl_state_SelectedIndexChanged(null, null);

                txt_invoice_no.Text = dr2.GetValue(1).ToString();
               // txt_invoice_date.Text = dr2.GetValue(2).ToString();


                txt_amount.Text = dr2.GetValue(3).ToString();
                txt_cgst.Text = dr2.GetValue(4).ToString();
                txt_sgst.Text = dr2.GetValue(5).ToString();
                txt_igst.Text = dr2.GetValue(6).ToString();
                txt_sac_code.Text = dr2.GetValue(7).ToString();
                txt_id.Text = dr2.GetValue(8).ToString();
                ddl_gst_payed.SelectedValue = dr2.GetValue(9).ToString();
                txt_grand_total.Text = dr2.GetValue(10).ToString();
                txt_manual_date.Text = dr2.GetValue(11).ToString();
                ddl_manual_invoice_type.SelectedValue = dr2.GetValue(12).ToString();
            }
            dr2.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
            btn_update.Visible = true;
            btn_save.Visible = false;
            btn_save1.Visible = false;
        //    btn_excel_clientwise.Visible = true;
            //panel_invoice_date.Visible = true;
            //nel_save.Visible = false;
           // btn_clientwise_other_invoice.Visible = true;
           // btn_final_invoice.Visible = true;
            
        }

    }

    /// komal code for manual invoice 04-06-2020


    protected void text_clear()
    {
        ddl_client_manual.SelectedValue = "ALL";
        ddl_state.SelectedValue = "Select";
        txt_invoice_no.Text = "";
        txt_invoice_date.Text = "";
        txt_gst_no.Text = "";
        txt_amount.Text = "0";
        txt_cgst.Text = "0";
        txt_sgst.Text = "0";
        txt_igst.Text = "0";
        txt_sac_code.Text = "";

        ddl_state_name_other.SelectedValue = "Select";
        ddl_manual_invoice_type.SelectedValue = "2";
        ddl_gst_payed.SelectedValue = "Select";
        txt_bill_add.Text = "";
        txt_gst_add.Text = "";
        txt_gst_no_other.Text = "";
        txt_invoice_category.Text = "";
        txt_bill_party.Text = "";
        txt_grand_total.Text = "";
        txt_manual_date.Text = "";
        ddl_gst_payed.SelectedValue = "SEL";
    }
    protected void ddl_manual_invoice_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        hidtab.Value = "1";

        if (ddl_manual_invoice_type.SelectedValue == "0")
        {

            manual_invoice_details();
            panel_clientwise.Visible = true;
         //   panel_clientwise1.Visible = true;

            panel_other.Visible = false;

        }
        else
        if (ddl_manual_invoice_type.SelectedValue=="1") 
        {


            panel_clientwise.Visible = false;
           // panel_clientwise1.Visible = false;

            other_invoice_details();
            panel_other.Visible = true;
        
        }
    }
    
    //protected void gv_other_invoice_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    for (int i = 0; i < e.Row.Cells.Count; i++)
    //    {
    //        if (e.Row.Cells[i].Text == "&nbsp;")
    //        {
    //            e.Row.Cells[i].Text = "";
    //        }
    //    }



    //}
    protected void lnk_remove_manual_other_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_report_gst where id = '" + grdrow.Cells[0].Text + "'");

        other_invoice_details();
    }

    protected void other_state_name() 
    {
        hidtab.Value = "1";
        d.con.Open();
        try
        {
            MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where comp_code ='"+Session["comp_code"].ToString()+"'and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' )  ORDER BY STATE", d.con);
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
  
 
    protected void btn_edit_other1_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        string id = grdrow.Cells[0].Text;
        MySqlCommand cmd2 = new MySqlCommand("SELECT state_name,`gst_address`,`bill_shipping_add`,`client_name`,`gst_no`, `invoice_category`,amount,cgst,sgst,igst,sac_code,invoice_no,id,gst_to_be,manual_grand_total,manual_month,`manual_invoice_type` from pay_report_gst WHERE  ID='" + id + "'", d3.con);
        d3.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                //  id = dr2.GetValue(0).ToString();


                ddl_state_name_other.SelectedValue = dr2.GetValue(0).ToString();
                 //ddl_state_SelectedIndexChanged(null, null);

                txt_gst_add.Text = dr2.GetValue(1).ToString();
                txt_bill_add.Text = dr2.GetValue(2).ToString();
                txt_bill_party.Text = dr2.GetValue(3).ToString();
                txt_gst_no_other.Text = dr2.GetValue(4).ToString();
                txt_invoice_category.Text = dr2.GetValue(5).ToString();

                txt_amount.Text = dr2.GetValue(6).ToString();
                txt_cgst.Text = dr2.GetValue(7).ToString();
                txt_sgst.Text = dr2.GetValue(8).ToString();
                txt_igst.Text = dr2.GetValue(9).ToString();
                txt_sac_code.Text = dr2.GetValue(10).ToString();

                txt_invoice_no.Text = dr2.GetValue(11).ToString();
               // txt_invoice_date.Text = dr2.GetValue(12).ToString();
                txt_id.Text = dr2.GetValue(12).ToString();
                ddl_gst_payed.SelectedValue = dr2.GetValue(13).ToString();
                txt_grand_total.Text = dr2.GetValue(14).ToString();
                txt_manual_date.Text = dr2.GetValue(15).ToString();
                ddl_manual_invoice_type.SelectedValue = dr2.GetValue(16).ToString();
             
               
              
                //  txt_id.Text = dr2.GetValue(9).ToString();
            }
            dr2.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
            btn_save1.Visible = false;
            btn_update_other.Visible = true;
           // btn_excel_clientwise.Visible = true;
           // btn_clientwise_other_invoice.Visible = true;
          btn_save.Visible = false;
           // panel_invoice_date.Visible = true;
           // btn_final_invoice.Visible = true;

        }
    }


    protected void btn_clientwise_other_invoice_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";

        //string invoice_date = "" + txt_invoice_date.Text + "";

        //if (invoice_date == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Invoice Date !!');", true);
        //    btn_final_invoice.Focus();
        //    return;
        //}
      

          
            string bill_date = "" + txt_invoice_date + "";

           // string id = "" + txt_id.Text + "";

            ReportLoad( bill_date);
    }

    private void ReportLoad(string bill_date)
    {
        string headerpath = null;
        string footerpath = null;
       // string downloadname = downloadfilename;
        try
        {

            string query = "";
     

            ReportDocument crystalReport = new ReportDocument();
            
            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                crystalReport.Load(Server.MapPath("~/clientwise_manual_invoice.rpt"));
            }

            else if (ddl_manual_invoice_type.SelectedValue == "1") 
            {
                crystalReport.Load(Server.MapPath("~/other_manual_invoice.rpt"));
            
            }

            string month = ""; string year = "";

            month = ""+txt_manual_date.Text+"";
          month =  month.Substring(0,2);

          year = "" + txt_manual_date.Text + "";
          year = year.Substring(3);

            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            string month_name = mfi.GetMonthName(int.Parse(month)).ToString();

            string invoice_month_name = "concat('" + month_name + "',' ' ,'" + year + "')";

            if(ddl_manual_invoice_type.SelectedValue=="0") // clientwise
            {
                if (ddl_client_manual.SelectedValue=="DHFL")
                {
                    query = "SELECT  distinct (client_name) as 'UNIT_ADD2' ,(`state_name`) AS 'state_name', (`invoice_no`) AS 'zone',  DATE_FORMAT( invoice_date , '%d/%m/%Y')  AS 'Expr1', (`gst_no`) AS 'type', (`sac_code`) AS 'Security_sac_code' , (amount) as 'CITY',  (manual_grand_total) AS 'grand_total', (cgst) as 'ADDRESS2', (sgst) as 'STATE',(igst) as 'client_code',PF_REG_NO,COMPANY_PAN_NO, SERVICE_TAX_REG_NO," + invoice_month_name + "  AS 'UNIT_ADD1',`COMPANY_NAME`,(SELECT `Field1`FROM `pay_zone_master` WHERE `client_code` = '" + ddl_client_manual.SelectedValue + "' AND `pay_zone_master`.`region` = '" + ddl_state.SelectedValue + "' AND `comp_code` = '" + Session["comp_code"].ToString() + "' AND `type` = 'GST') as 'ADDRESS1' FROM `pay_report_gst` INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` where pay_report_gst.comp_code = '" + Session["comp_code"].ToString() + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' and `manual_invoice_type` = '" + ddl_manual_invoice_type.SelectedValue + "' and manual_region = '" + ddlregion_manual.SelectedValue + "' ";
                
                
                }
                else{

            //    query = "SELECT (`client_name`) AS 'other', '' AS 'ADDRESS1', '' AS 'ADDRESS2', '' AS 'CITY', '' AS 'STATE',  `PF_REG_NO`, `COMPANY_PAN_NO`, `COMPANY_TAN_NO`, `COMPANY_CIN_NO`, `SERVICE_TAX_REG_NO`, `ESIC_REG_NO`, `state_name` AS 'STATE_NAME',  '' AS 'UNIT_ADD1', '' AS 'UNIT_ADD2', '' AS 'UNIT_CITY', (`gst_no`) AS 'unit_gst_no',  '' AS 'Expr1', '' AS 'start_end_date', '' AS 'TOT_DAYS_PRESENT',  '' AS 'month_days', (`amount` + `igst` + `cgst` + `sgst`) AS 'grand_total', (`sgst` + `cgst` + `igst`) AS 'total', (`cgst`) AS 'CGST', (`igst`) AS 'SGST', (`sgst`) AS 'IGST', (`month`) AS 'month','' AS 'housekeeiing_sac_code', (`sac_code`) AS 'Security_sac_codess', `grade_code` AS 'designation','' AS 'hrs_12_ot' FROM `pay_report_gst`  INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` where pay_report_gst.comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_manual.SelectedValue + "' and invoice_no = '" + txt_invoice_no.Text + "'";

                query = "SELECT  distinct (client_name) as 'UNIT_ADD2' ,(`state_name`) AS 'state_name', (`invoice_no`) AS 'zone',  DATE_FORMAT( invoice_date , '%d/%m/%Y')  AS 'Expr1', (`gst_no`) AS 'type', (`sac_code`) AS 'Security_sac_code' , (amount) as 'CITY',  (manual_grand_total) AS 'grand_total', (cgst) as 'ADDRESS2', (sgst) as 'STATE',(igst) as 'client_code',PF_REG_NO,COMPANY_PAN_NO, SERVICE_TAX_REG_NO," + invoice_month_name + "  AS 'UNIT_ADD1',`COMPANY_NAME`,(SELECT `Field1`FROM `pay_zone_master` WHERE `client_code` = '" + ddl_client_manual.SelectedValue + "' AND `pay_zone_master`.`region` = '" + ddl_state.SelectedValue + "' AND `comp_code` = '" + Session["comp_code"].ToString() + "' AND `type` = 'GST') as 'ADDRESS1' FROM `pay_report_gst` INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` where pay_report_gst.comp_code = '" + Session["comp_code"].ToString() + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' and `manual_invoice_type` = '" + ddl_manual_invoice_type.SelectedValue + "' ";
                }
            
            
            }

            else
                if (ddl_manual_invoice_type.SelectedValue == "1") 
            {
                query = "SELECT  distinct (`state_name`) AS 'state_name', (`invoice_no`) AS 'zone',  DATE_FORMAT( invoice_date , '%d/%m/%Y')  AS 'Expr1', (`gst_no`) AS 'type', (`sac_code`) AS 'Security_sac_code' , (amount) as 'CITY' ,(manual_grand_total) as 'grand_total',PF_REG_NO,COMPANY_PAN_NO, SERVICE_TAX_REG_NO, " + invoice_month_name + " AS 'COMPANY_TAN_NO',`COMPANY_NAME`,(gst_address) as 'ADDRESS2', (`client_name`) as 'COMP_CODE',(`invoice_category`) AS 'client_code', (`bill_shipping_add`) as 'STATE' ,(`sgst`) AS 'UNIT_ADD1', (`cgst`) AS 'UNIT_ADD2', (`igst`) AS 'UNIT_CITY'  FROM `pay_report_gst`INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` where pay_report_gst.comp_code = '" + Session["comp_code"].ToString() + "' and manual_month = '" + txt_manual_date.Text + "' and state_name = '" + ddl_state_name_other.SelectedValue + "'  and type = 'manual' and `manual_invoice_type` = '" + ddl_manual_invoice_type.SelectedValue + "' ";
            
            }

          //  query = "SELECT (`state_name`) AS 'state_name' FROM `pay_report_gst` where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_manual.SelectedValue + "' and invoice_no = '" + txt_invoice_no.Text + "'";
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query, d.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            d.con.Open();
            sda.Fill(dt);

            
   

            if (Session["COMP_CODE"].ToString() == "C02")
            {
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header.png");
                footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath"].Text = @"'" + headerpath + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
            }
            else
            {
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header.png");
                footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath"].Text = @"'" + headerpath + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
            }


            PageMargins margins;
            // Get the PageMargins structure and set the 
            // margins for the report.
            margins = crystalReport.PrintOptions.PageMargins;
            margins.bottomMargin = 0;
            margins.leftMargin = 350;
            margins.rightMargin = 0;
            margins.topMargin = 0;
            // Apply the page margins.
            crystalReport.PrintOptions.ApplyPageMargins(margins);
            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();


            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "invoice_manual_copy");
            crystalReport.Close();
            crystalReport.Clone();
            crystalReport.Dispose();
            Response.End();

        }
        catch (Exception ex) { throw ex; }
        finally {

            d.con.Close();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
    protected void gv_other_invoice_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_other_invoice.UseAccessibleHeader = false;
            gv_other_invoice.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
   
    protected void gv_other_invoice_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (dr["final_invoice"].ToString() != "0")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;


                //  e.Row.Cells[14].Visible = false;
                // e.Row.Cells[15].Visible = false;

                LinkButton lb1 = e.Row.FindControl("btn_edit_other1") as LinkButton;
                lb1.Visible = false;

                LinkButton lb2 = e.Row.FindControl("lnk_remove_manual_other") as LinkButton;
                lb2.Visible = false;




            }
        }
      



        e.Row.Cells[0].Visible = false;
        e.Row.Cells[21].Visible = false;
    }

    protected void btn_update_other_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try
        {
            d.con.Open();
            int dispatch2 = d.operation("update pay_report_gst set  gst_no='" + txt_gst_no.Text + "',amount='" + txt_amount.Text + "',cgst='" + txt_cgst.Text + "',sgst='" + txt_sgst.Text + "',igst='" + txt_igst.Text + "',sac_code='" + txt_sac_code.Text + "',`client_name` ='" + txt_bill_party.Text + "',`invoice_category` = '" + txt_invoice_category.Text + "',gst_address ='" + txt_gst_add.Text + "',`bill_shipping_add` = '" + txt_bill_add.Text + "', gst_to_be = '" + ddl_gst_payed.SelectedValue + "' where id = '" + txt_id.Text + "' ");


            if (dispatch2 > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successfully !!');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('record Updated UpdatedFailed. !!');", true);
            }

        }
        catch (Exception ex) { throw ex; }
        finally{
            d.con.Close();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_clientwise_other_invoice.Visible = false;
            other_invoice_details();
         //   text_clear();
        }

    }
    protected void txt_amount_TextChanged1(object sender, EventArgs e)
    {

        try
        {
            d.con.Open();

            string clientwise_amount = "" + txt_amount.Text + "";
            Double amount = Convert.ToDouble(clientwise_amount);

            Double cgst = 0;
            Double sgst = 0;
            Double igst = 0;
            Double total = 0;

            string state_name = "";
            if (ddl_manual_invoice_type.SelectedValue=="0") 
            {
                state_name = "" + ddl_state.SelectedValue + "";
            }
            else
          if (ddl_manual_invoice_type.SelectedValue == "1")
           {
               state_name = "" + ddl_state_name_other.SelectedValue + "";

           }

            string company_name = ""+Session["comp_code"].ToString()+"";


            if (company_name=="C01") 
            {

                if (state_name == "Maharashtra")
                {
                    cgst = ((amount) * (0.09));
                    sgst = ((amount) * (0.09));

                    txt_cgst.Text = cgst.ToString();
                    txt_sgst.Text = sgst.ToString();
                    txt_igst.Text = igst.ToString();

                    total = (cgst + sgst + igst + amount);
                     txt_grand_total.Text = total.ToString();
                }
                else
                {
                    igst = ((amount) * (0.18));
                    txt_igst.Text = igst.ToString();

                    txt_cgst.Text = cgst.ToString();
                    txt_sgst.Text = sgst.ToString();

                     total = (cgst + sgst + igst + amount);
                     txt_grand_total.Text = total.ToString();
                }


            }
            else
                if (company_name == "C02")
                {

                    if (ddl_state.SelectedValue == "Jharkhand")
                    {
                    cgst = ((amount) * (0.09));
                    sgst = ((amount) * (0.09));

                    txt_cgst.Text = cgst.ToString();
                    txt_sgst.Text = sgst.ToString();
                    txt_igst.Text = igst.ToString();

                   total = (cgst + sgst + igst + amount);
                   txt_grand_total.Text = total.ToString();
                }

                    
                    else
                    {
                        igst = ((amount) * (0.18));
                        txt_igst.Text = igst.ToString();

                        txt_cgst.Text = cgst.ToString();
                        txt_sgst.Text = sgst.ToString();

                      total = (cgst + sgst + igst + amount);
                       txt_grand_total.Text = total.ToString();
                    }
                
                }

           

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            
            d.con.Close();
        }



    }

    protected void sac_code() 
    {

        try 
        {

            string sac_code = d.getsinglestring("select `Security_sac_code` from pay_company_master where comp_code = '"+Session["comp_code"].ToString()+"'");

            txt_sac_code.Text = ""+sac_code+"";
        
        
        }
        catch (Exception ex) { throw ex; }
        finally{}
    
    }

    protected void btn_final_invoice_Click(object sender, EventArgs e)
    {
        try
        {
            string invoice_date = "" + txt_invoice_date.Text + "";

            if (invoice_date == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Invoice Date !!');", true);
                btn_final_invoice.Focus();
                return;
            }


            string number = null;
            MySqlCommand cmdmax = new MySqlCommand();
            string init_manual = "";

            string id = "" + txt_id.Text + "";

            string invoice_flag_check = "";
            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                if (ddl_client_manual.SelectedValue == "DHFL")
                {

                    invoice_flag_check = d.getsinglestring("select `final_invoice` from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' and manual_region = '" + ddlregion_manual.SelectedValue + "' ");
                }
                else
                {

                    invoice_flag_check = d.getsinglestring("select `final_invoice` from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' ");
                }

                }

            else if (ddl_manual_invoice_type.SelectedValue == "1")
            {
                invoice_flag_check = d.getsinglestring("select `final_invoice` from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and state_name = '" + ddl_state_name_other.SelectedValue + "'  and `manual_month` = '" + txt_manual_date.Text + "' ");
            
            }
            if (invoice_flag_check=="0")
            {
           
               // cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(credit_note_no, 4, length(credit_note_no)-3) AS UNSIGNED)) FROM credit_debit_note WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            cmdmax = new MySqlCommand(" SELECT MAX(CAST(SUBSTRING(`invoice_no`,10, LENGTH(`invoice_no`) - 1) AS unsigned)) FROM `pay_report_gst`WHERE comp_code='" + Session["comp_code"].ToString() + "' and type ='manual'", d.con);


           init_manual = "M";

            // for month
           init_manual = init_manual + txt_manual_date.Text.Substring(0, 2);

            // for year
           init_manual = init_manual + txt_manual_date.Text.Substring(3);

           
            d.con.Open();
            MySqlDataReader drmax = cmdmax.ExecuteReader();



            if (drmax.Read())
            {

                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    number = init_manual.ToString() + "0001";
                }
                else
                {
                    //number = (drmax.GetValue(0).ToString()) + 1;
                    int max_empcode = int.Parse(drmax.GetValue(0).ToString()) + 1;
               
                    if (max_empcode < 10)
                    {
                        number = init_manual.ToString() + "000" + max_empcode;
                    }
                    else if (max_empcode >= 10 && max_empcode < 100)
                    {
                        number = init_manual.ToString() + "00" + max_empcode;
                    }
                    else if (max_empcode >= 100 && max_empcode < 1000)
                    {
                        number = init_manual.ToString() + "0" + max_empcode;
                    }
                    else if (max_empcode >= 1000 && max_empcode < 10000)
                    {
                        number = init_manual.ToString() + "0" + max_empcode;
                    }
                    else if (max_empcode == 10000)
                    {
                        number = init_manual.ToString() + "" + max_empcode;
                    }

                }
            }



            int dispatch2 = 0;
            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                if (ddl_client_manual.SelectedValue=="DHFL")
                {
                    dispatch2 = d.operation("update pay_report_gst set  final_invoice = '1', invoice_no = '" + number + "' , invoice_date = str_to_date('" + txt_invoice_date.Text + "','%d/%m/%Y') where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "' AND manual_region = '" + ddlregion_manual.SelectedValue + "'");
                }
                else
                {
                
                 dispatch2 = d.operation("update pay_report_gst set  final_invoice = '1', invoice_no = '" + number + "' , invoice_date = str_to_date('" + txt_invoice_date.Text + "','%d/%m/%Y') where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "'");
                }
            }
            else
                if (ddl_manual_invoice_type.SelectedValue == "1") // other
                {
                    dispatch2 = d.operation("update pay_report_gst set  final_invoice = '1', invoice_no = '" + number + "' , invoice_date = str_to_date('" + txt_invoice_date.Text + "','%d/%m/%Y') where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and state_name = '" + ddl_state_name_other.SelectedValue + "'  and `manual_month` = '" + txt_manual_date.Text + "'");
                
                }


            if (dispatch2 > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Final Invoice Genrated Successfully !!');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Final Invoice Genrated Failed. !!');", true);
            }

            }
            else if (invoice_flag_check == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Final Invoice Genrated Successfully !!');", true);

            }


            btn_update_other.Visible = false;
            btn_update.Visible = false;

            if (ddl_manual_invoice_type.SelectedValue == "0")
            {
                manual_invoice_details();

            }
            else
                if (ddl_manual_invoice_type.SelectedValue == "1")
                {
                    other_invoice_details();
                }
        

            string bill_date = "" + txt_invoice_date + "";

            // string id = "" + txt_id.Text + "";

            ReportLoad(bill_date);

        //    btn_final_invoice.Visible = false;
           
        }
        catch (Exception ex) { throw ex; }
        finally{}
        
    }

    protected void other_client_code() 
    {

        try
        {

         //   d4.con.Open();

            string number = null;
            MySqlCommand cmdmax = new MySqlCommand();
            string init_manual = "";

            cmdmax = new MySqlCommand(" SELECT MAX(CAST(SUBSTRING(`client_code`,3, LENGTH(`client_code`) - 2) AS unsigned)) FROM `pay_report_gst`WHERE comp_code='" + Session["comp_code"].ToString() + "' and type ='manual' AND `manual_invoice_type` = '1' ", d4.con);


           init_manual = "OM";

        
            d4.con.Open();
            MySqlDataReader drmax = cmdmax.ExecuteReader();



            if (drmax.Read())
            {

                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    number = init_manual.ToString() + "001";
                }
                else
                {
                    //number = (drmax.GetValue(0).ToString()) + 1;
                    int max_empcode = int.Parse(drmax.GetValue(0).ToString()) + 1;
               
                    if (max_empcode < 10)
                    {
                        number = init_manual.ToString() + "00" + max_empcode;
                    }
                    else if (max_empcode >= 10 && max_empcode < 100)
                    {
                        number = init_manual.ToString() + "00" + max_empcode;
                    }
                    else if (max_empcode >= 100 && max_empcode < 1000)
                    {
                        number = init_manual.ToString() + "0" + max_empcode;
                    }
                    else if (max_empcode >= 1000 && max_empcode < 10000)
                    {
                        number = init_manual.ToString() + "0" + max_empcode;
                    }
                    else if (max_empcode == 10000)
                    {
                        number = init_manual.ToString() + "" + max_empcode;
                    }

                }
            }



            int dispatch2 = 0;
            //if (ddl_manual_invoice_type.SelectedValue == "0")
            //{
            //    dispatch2 = d.operation("update pay_report_gst set  final_invoice = '1', invoice_no = '" + number + "' , invoice_date = str_to_date('" + txt_invoice_date.Text + "','%d/%m/%Y') where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '" + ddl_manual_invoice_type.SelectedValue + "' and type = 'manual' and `client_code` = '" + ddl_client_manual.SelectedValue + "' and `state_name` = '" + ddl_state.SelectedValue + "' and `manual_month` = '" + txt_manual_date.Text + "'");

            //}
            //else
                if (ddl_manual_invoice_type.SelectedValue == "1")
                {
                    dispatch2 = d.operation("update pay_report_gst set   client_code = '" + number + "'  where comp_code = '" + Session["comp_code"].ToString() + "' and manual_invoice_type = '1' and type = 'manual' and state_name = '" + ddl_state_name_other.SelectedValue + "' and client_name = '" + txt_bill_party.Text + "' and `manual_month` = '" + txt_manual_date.Text + "'");
                
                }


            if (dispatch2 > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' other client code updated !!');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('other client code not updated. !!');", true);
            }

            }
        



            //////////
        
        catch (Exception ex) { throw ex; }
        finally { d4.con.Close(); }
    
    
    
    }
    protected void ddlregion_manual_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlregion_manual.SelectedValue != "Select")
        {
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = null;
            if (ddlregion_manual.SelectedValue != "ALL")
            {
                cmd_item = new MySqlDataAdapter("SELECT DISTINCT (STATE_NAME) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"] + "' AND client_code = '" + ddl_client_manual.SelectedValue + "' AND ZONE = '" + ddlregion_manual.SelectedValue + "' ORDER BY 1", d.con);
            }
            else
            {
                cmd_item = new MySqlDataAdapter("SELECT DISTINCT (STATE_NAME) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"] + "' AND client_code = '" + ddl_client_manual.SelectedValue + "' ORDER BY 1", d.con);
            }
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
                ddl_state.Items.Insert(0, "Select");
                ddl_state.Items.Insert(1, "ALL");
                //ddl_unitcode.Items.Clear();
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
}