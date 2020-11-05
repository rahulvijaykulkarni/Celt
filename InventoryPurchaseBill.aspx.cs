using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class InventoryPurchaseBill : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d_sum = new DAL();
    DAL ac = new DAL();
    DAL d1 = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    TransactionBAL tbl = new TransactionBAL();
    double a = 0, b = 0, c = 0;
    System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
    int update_flag = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            state();
            btn_save_send.Visible = false;
            txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txt_start_date.Text = txt_docdate.Text;
            Session["DISCOUNT_BY"] = "RS";

            //   sales_person_list();

            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;



            gendocno();

            txt_docno.Enabled = true;
            ddl_customerlist.Enabled = true;
            txt_docdate.Enabled = true;
            txt_narration.Enabled = true;

            gv_itemslist.Visible = false;
            btn_update.Visible = false;
            btn_delete.Visible = false;

            btn_save_send.Visible = false;
            txt_docno.ReadOnly = true;
            ddlcalmonth.SelectedIndex = DateTime.Now.Month - 1;

            txt_extra_chrgs_tax.Text = "0";


            txt_docno.ReadOnly = true;
            ddl_vendor.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT CUST_NAME , CUST_ID  from pay_customer_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            d.con.Open();
            try
            {
                int i = 0;
                MySqlDataReader dr_item = cmd_item1.ExecuteReader();
                while (dr_item.Read())
                {
                    ddl_vendor.Items.Insert(i++, new ListItem(dr_item[0].ToString(), dr_item[1].ToString()));
                }
                dr_item.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_vendor.Items.Insert(0, new ListItem("Select", "0"));
                ddl_vendor.ToolTip = ddl_vendor.SelectedItem.ToString();
            }
            string unit = "";
            update_listbox(txt_designation, unit);
            tooltrip();
            //vikas for report
            report_vendor();

        }


    }


    private void gendocno()
    {
        //  MySqlCommand cmd1 = new MySqlCommand("Select COUNT(*) from pay_transactionp  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND TASK_CODE='STP'", d.con);
        MySqlCommand cmd1 = new MySqlCommand("Select DOC_NO from pay_transactionp  where COMP_CODE='"+Session["comp_code"].ToString()+"' AND TASK_CODE='STP' ORDER BY DOC_NO DESC  limit 1", d.con);

        d.con.Open();
        int total_count = 1;
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                string row = dr1[0].ToString().Substring(1, 4);
                if (row == "")
                {
                    total_count = 1;
                }
                else
                {
                    total_count =
                   Convert.ToInt32(row);
                    total_count++;
                }
            }

            if (total_count >= 0 && total_count <= 9)
            {
                string docno = "P" + "000" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 10 && total_count <= 99)
            {
                string docno = "P" + "00" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 100 && total_count <= 999)
            {
                string docno = "P" + "0" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 1000 && total_count <= 9999)
            {
                string docno = "P" + total_count.ToString();
                txt_docno.Text = docno.ToString();
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_itemslist.Visible = true;
        //  gv_itemslist.DataSource = null;
        //  gv_itemslist.DataBind();
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("item_type");

        dt.Columns.Add("Particular");
        dt.Columns.Add("size_uniform");
        dt.Columns.Add("size_shoes");
        dt.Columns.Add("size_pantry");

        dt.Columns.Add("DESCRIPTION");
        dt.Columns.Add("VAT");
        dt.Columns.Add("HSN_Code");
        dt.Columns.Add("Designation");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Rate");
        dt.Columns.Add("DISCOUNT");
        dt.Columns.Add("DISCOUNT_AMT");
        dt.Columns.Add("Amount");
        dt.Columns.Add("START_DATE");
        dt.Columns.Add("END_DATE");
        dt.Columns.Add("VENDOR");
        dt.Columns.Add("size_apron");

        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                Label lbl_item_code = (Label)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_item_code");
                dr["ITEM_CODE"] = lbl_item_code.Text.ToString();
                Label lbl_item_type = (Label)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_item_type");
                dr["item_type"] = lbl_item_type.Text.ToString();
                TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_particular");
                dr["Particular"] = lbl_particular.Text.ToString();

                TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_uniformsize");
                dr["size_uniform"] = lbl_shoessize.Text.ToString();
                TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_shoessize");
                dr["size_shoes"] = lbl_uniformsize.Text.ToString();
                TextBox lbl_pantrysize = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_shoessize");
                dr["size_pantry"] = lbl_pantrysize.Text.ToString();

                TextBox lbl_Description_final = (TextBox)gv_itemslist.Rows[rownum].Cells[8].FindControl("lbl_Description_final");
                dr["DESCRIPTION"] = lbl_Description_final.Text.ToString();
                TextBox lbl_vat = (TextBox)gv_itemslist.Rows[rownum].Cells[9].FindControl("lbl_vat");
                dr["VAT"] = lbl_vat.Text.ToString();

                TextBox lbl_hsn_number = (TextBox)gv_itemslist.Rows[rownum].Cells[10].FindControl("lbl_hsn_code");
                dr["HSN_Code"] = lbl_hsn_number.Text.ToString();

                TextBox lbl_designation = (TextBox)gv_itemslist.Rows[rownum].Cells[11].FindControl("lbl_designation");
                dr["Designation"] = lbl_designation.Text;
                TextBox lbl_quantity = (TextBox)gv_itemslist.Rows[rownum].Cells[12].FindControl("lbl_quantity");
                dr["Quantity"] = Convert.ToDouble(lbl_quantity.Text);
                TextBox lbl_rate = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_rate");
                dr["Rate"] = Convert.ToDouble(lbl_rate.Text);
                TextBox lbl_discount = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_discount");
                dr["DISCOUNT"] = Convert.ToDouble(lbl_discount.Text);
                TextBox lbl_discount_amt = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_discount_amt");
                dr["DISCOUNT_AMT"] = Convert.ToDouble(lbl_discount_amt.Text);
                TextBox lbl_amount = (TextBox)gv_itemslist.Rows[rownum].Cells[16].FindControl("lbl_amount");
                dr["Amount"] = Convert.ToDouble(lbl_amount.Text);
                //TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[16].FindControl("lbl_start_date");
                //dr["START_DATE"] = (lbl_start_date.Text); vikas comment 22/11
                TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_end_date");
                dr["END_DATE"] = (lbl_end_date.Text);
                TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[19].FindControl("lbl_vendor");
                dr["VENDOR"] = (lbl_vendor.Text);

                TextBox lbl_apronsize = (TextBox)gv_itemslist.Rows[rownum].Cells[20].FindControl("lbl_apronsize");
                dr["size_apron"] = lbl_apronsize.Text.ToString();

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["ITEM_CODE"] = txt_particular.SelectedValue.ToString();
        dr["item_type"] = ddl_product.SelectedValue.ToString();
        dr["Particular"] = txt_particular.SelectedItem.ToString();
        dr["size_uniform"] = ddl_uniformsize.SelectedItem.ToString();
        dr["size_shoes"] = ddl_shoosesize.SelectedItem.ToString();
        dr["size_pantry"] = ddl_pantry_size.SelectedItem.ToString();
        dr["DESCRIPTION"] = txt_desc.Text.ToString();
        dr["VAT"] = txt_description.Text.ToString();
        dr["HSN_Code"] = txt_hsn.Text.ToString();
        dr["Designation"] = txt_designation.Text;
        dr["Quantity"] = txt_quantity.Text;
        dr["Rate"] = txt_rate.Text;
        dr["DISCOUNT"] = txt_discount_rate.Text;
        dr["DISCOUNT_AMT"] = txt_discount_price.Text;
        dr["Amount"] = txt_amount.Text;
        dr["START_DATE"] = txt_start_date.Text;
        dr["END_DATE"] = txt_end_date.Text;
        dr["VENDOR"] = ddl_vendor.SelectedItem.Text.ToString();
        dr["size_apron"] = ddl_apron_size1.SelectedItem.Text.ToString();
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        gst_counter(txt_customer_gst.Text.Substring(0, 2));
        discount_calculate(dt, 1);
        ViewState["CurrentTable"] = dt;
        txt_rate.Text = "0";
        txt_particular.SelectedIndex = 0;
        txt_per_unit.Text = "0";
        txt_designation.SelectedIndex = 0;
        txt_quantity.Text = "0";
        txt_amount.Text = "0";
        txt_desc.Text = "";
        //txt_vendorname.Text = "";
        txt_description.Text = "0";
        txt_hsn.Text = "";
        txt_igst.Text = "";
        txt_description.Text = "0";
        txt_discount_price.Text = "0";
        txt_particular.Focus();
        txt_rate.Text = "0";
        txt_discount_rate.Text = "0";
        //txt_tot_discount_percent.Text = "0";
        //  txt_end_date.Text = "";
        ddl_vendor.SelectedIndex = 0;
        //lbl_qty.Visible = false;
        Panel4.Visible = true;
        Panel2.Visible = true;
        lbl_qty.Text = "";
        lbl_rete1.Visible = false;
        txt_quantity1.Visible = false;
        ddl_product.SelectedValue = "Select";
        ddl_uniformsize.SelectedValue = "Select";
        ddl_shoosesize.SelectedValue = "Select";
        lbl_rete1.Visible = false;
        lbl_raten.Visible = false;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string final1 = d.getsinglestring(" select DOC_NO from pay_transactionp where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_invoice_flag = 1");
        if (final1 != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This Invoice IS Final YOU Can Not Save  !!');", true);

            return;

        }
        update_flag = 1;
        if (budget())
        {
            return;
        }
        int invoice_result = 0;
        int item_result = 0;
        int TotalRows = gv_itemslist.Rows.Count;
        MySqlCommand cmd = new MySqlCommand("select DOC_NO from pay_transactionp  where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and TASK_CODE='STP'", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Invoice Number is already exist, Please rechange the number.')", true);
                text_clear();
            }
            else
            {
                string cust_code = "C001";
                string txt_billmonth = ddlcalmonth.SelectedValue.ToString();
                if (txt_extra_chrgs_amt.Text == "")
                {
                    txt_extra_chrgs_amt.Text = "0";
                }
                final_total();
                string final = txt_final_total.Text;
                string l_docnumber = txt_docno.Text;
                lbl_print_quote.Text = l_docnumber;
                d.operation("DELETE FROM pay_transactionp WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");

                d.operation("DELETE FROM pay_transactionp_DETAILS WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");


                if (ddl_vendortype.SelectedValue == "Regular")
                {

                    //invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.Text.Substring(0, 4) + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','"+ddlcategory.SelectedItem.Text+"','"+ddl_vendortype.SelectedValue+"')");
                    // invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.Text.Substring(0, 4) + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "')");
                    invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD,pur_order_no,month,year,company_shiping_name,company_shiping_address,company_shiping_gst_no,company_shiping_state,vendor_invoice_no) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "','" + ddl_po_num.SelectedValue + "','" + txt_docdate.Text.Substring(3, 2) + "','" + txt_docdate.Text.Substring(6) + "','" + txt_ship.Text + "','" + txt_ship_address.Text + "','" + txt_ship_gst_no.Text + "','" + ddl_shiping_state.SelectedValue + "','" + txt_vendor_no.Text + "')");
                }
                else
                {
                    //  invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "')");
                    //  invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "')");
                    invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD,pur_order_no,,month,year,company_shiping_name,company_shiping_address,company_shiping_gst_no,company_shiping_state,vendor_invoice_no) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "','" + ddl_po_num.SelectedValue + "','" + txt_docdate.Text.Substring(3, 2) + "','" + txt_docdate.Text.Substring(6) + "','" + txt_ship.Text + "','" + txt_ship_address.Text + "','" + txt_ship_gst_no.Text + "','" + ddl_shiping_state.SelectedValue + "','" + txt_vendor_no.Text + "')");

                }

                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    Label lbl_srnumber = (Label)row.FindControl("lbl_srnumber");
                    int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                    Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                    string item_code = (lbl_item_code.Text);
                    Label lbl_item_type = (Label)row.FindControl("lbl_item_type");
                    string item_type = (lbl_item_type.Text);
                    TextBox lbl_particular = (TextBox)row.FindControl("lbl_particular");
                    string particular = lbl_particular.Text.ToString();
                    TextBox lbl_Description_final = (TextBox)row.FindControl("lbl_Description_final");
                    string lbl_Description_final1 = lbl_Description_final.Text.ToString();
                    TextBox lbl_vat = (TextBox)row.FindControl("lbl_vat");
                    string vat = lbl_vat.Text.ToString();
                    TextBox lbl_hsc_no = (TextBox)row.FindControl("lbl_hsn_code");
                    string lbl_hsc_code = lbl_hsc_no.Text.ToString();
                    TextBox lbl_designation = (TextBox)row.FindControl("lbl_designation");
                    string designation = lbl_designation.Text;
                    TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                    float quantity = float.Parse(txt_quantity.Text);
                    TextBox txt_rate = (TextBox)row.FindControl("lbl_rate");
                    float rate = float.Parse(txt_rate.Text);
                    TextBox txt_product_discount = (TextBox)row.FindControl("lbl_discount");
                    float product_discount = float.Parse(txt_product_discount.Text);
                    TextBox txt_product_discount_amt = (TextBox)row.FindControl("lbl_discount_amt");
                    float product_discount_amt = float.Parse(txt_product_discount_amt.Text);
                    TextBox lbl_amount = (TextBox)row.FindControl("lbl_amount");
                    float amount = float.Parse(lbl_amount.Text);
                    //TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                    //string start_date = lbl_start_date.Text.ToString(); vikas comment 22/11
                    TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                    string end_date = lbl_end_date.Text.ToString();
                    TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                    string vendor = lbl_vendor.Text.ToString();

                    TextBox lbl_uniformsize = (TextBox)row.FindControl("lbl_uniformsize");
                    string uniformsize = lbl_uniformsize.Text.ToString();
                    TextBox lbl_shoessixe = (TextBox)row.FindControl("lbl_shoessize");
                    string shoes = lbl_shoessixe.Text.ToString();
                    TextBox lbl_pantrysize = (TextBox)row.FindControl("lbl_pantrysize");
                    string pantrysize = lbl_pantrysize.Text.ToString();

                    TextBox lbl_apronsize = (TextBox)row.FindControl("lbl_apronsize");
                    string apronsize = lbl_apronsize.Text.ToString();
                    //    string query = "INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "'";

                    // int insert_result =  d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,END_DATE,VENDOR,item_type,size_uniform,size_shoes) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "')");
                    int insert_result = d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,END_DATE,VENDOR,item_type,size_uniform,size_shoes,`size_pantry`,size_apron) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "','" + pantrysize + "','" + apronsize + "')");
                    if (insert_result > 0)
                    {
                        try
                        {
                            item_result = item_result + insert_result;
                            string sum_inward = (" select sum(QUANTITY) from pay_transactionp_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                            MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                            d_sum.con1.Open();
                            MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                            if (dr_sum_inward.Read())
                            {
                                double doubleoinword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                                int updaterecord = d.operation("update pay_item_master SET inward ='" + doubleoinword + "' where Item_Code='" + item_code + "'");
                            }
                            dr_sum_inward.Close();
                            cmd_sum.Dispose();
                            d_sum.con1.Close();
                            string query1 = (" select inward,outward,Stock from pay_item_master  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");

                            MySqlCommand cmd1 = new MySqlCommand(query1, d.con);
                            d.con.Open();
                            MySqlDataReader sda = cmd1.ExecuteReader();

                            if (sda.Read())
                            {
                                double doubleoinword = Convert.ToDouble(sda.GetValue(0).ToString());
                                double doubleoutword = Convert.ToDouble(sda.GetValue(1).ToString());
                                double doublestock = Convert.ToDouble(sda.GetValue(2).ToString());
                                doublestock = doubleoinword - doubleoutword;
                                int updaterecord = d.operation("update pay_item_master SET inward ='" + doubleoinword + "',outward='" + doubleoutword + "' ,Stock= '" + doublestock + "'  where Item_Code='" + item_code + "'");
                            }
                        }

                        catch { }
                    }


                }
                if (invoice_result > 0)
                {
                    d.operation("update pay_transaction_po set pur_invoice_flag='1' where comp_code='" + Session["comp_code"].ToString() + "' and po_no='"+ddl_po_num.SelectedValue+"'");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Invoice (" + txt_docno.Text + ") added successfully!')", true);

                    attached_doc();
                    lbl_print_quote.Text = txt_docno.Text;

                }
                else
                {
                    d.operation("Delete from pay_transactionp Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    d.operation("Delete from pay_transactionp_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                }

            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ee)
        {
            d.operation("Delete from pay_transactionp Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete from pay_transactionp_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {
            d.con.Close();
            text_clear();
            gendocno();
            btn_Save.Visible = true; ;
            btn_delete.Visible = false;
            btn_update.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:swal('Record Added Successfully !!!')", true);
    }


    protected void btn_save_send_mail(object sender, EventArgs e)
    {

        btn_Save_Click(sender, e);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email Send successfully!')", true);

    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        lbl_print_quote.Text = "";
        Response.Redirect("Home.aspx");
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        string final1 = d.getsinglestring(" select DOC_NO from pay_transactionp where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_invoice_flag = 1");
        if (final1 != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This Invoice IS Final YOU Can Not Remove  !!');", true);

            return;

        }

        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
        string item_code = (lbl_item_code.Text);
        TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
        float quantity = float.Parse(txt_quantity.Text);

        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    int delete_item = d.operation("Delete from pay_transactionp_details Where Item_Code = '" + item_code + "' AND DOC_NO = '" + txt_docno.Text + "'  and COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                    string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_inward from pay_transactionp_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                    MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                    d_sum.con1.Open();
                    MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                    if (dr_sum_inward.Read())
                    {
                        double doubleoinword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                        int updaterecord = d.operation("update pay_item_master SET inward ='" + doubleoinword + "' where Item_Code='" + item_code + "'");
                    }
                    dr_sum_inward.Close();
                    cmd_sum.Dispose();
                    d_sum.con1.Close();
                    string query1 = (" select inward,outward,Stock from pay_item_master  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                    MySqlCommand cmd1 = new MySqlCommand(query1, d.con);
                    d.con.Open();
                    MySqlDataReader sda = cmd1.ExecuteReader();
                    if (sda.Read())
                    {
                        double doubleoinword = Convert.ToDouble(sda.GetValue(0).ToString());
                        double doubleoutword = Convert.ToDouble(sda.GetValue(1).ToString());
                        double doublestock = Convert.ToDouble(sda.GetValue(2).ToString());

                        doublestock = doubleoinword - doubleoutword;

                        //  int updaterecord = d.operation("update pay_item_master inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code='" + item_code + "'");

                        int updaterecord = d.operation("update pay_item_master set outward='" + doubleoutword + "' ,Stock= '" + doublestock + "'  where Item_Code = '" + item_code + "' ");
                    }
                    sda.Close();
                    d.con.Close();

                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
            discount_calculate(dt, 1);
            //gst_calculate(dt);
        }
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        try
        {
            string headerpath = null;
            string discount_counter = "";
            ReportDocument crystalReport = new ReportDocument();
            string compcd = Session["COMP_CODE"].ToString();
            DataTable dt = new DataTable();
            if (txt_docno.Text != "")
            {
                
                //string query = "    SELECT  PAY_COMPANY_MASTER.COMP_CODE, PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO, PAY_COMPANY_MASTER.ECC_CODE_NO,  PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, PAY_TRANSACTIONQ.DOC_NO, PAY_TRANSACTIONQ.DOC_DATE,PAY_TRANSACTIONQ.CUST_NAME, PAY_TRANSACTIONQ.CUST_CODE, PAY_TRANSACTIONQ.REF_NO1, PAY_TRANSACTIONQ.REF_NO2, PAY_TRANSACTIONQ.NARRATION, PAY_TRANSACTIONQ.BILL_MONTH, PAY_TRANSACTIONQ.GROSS_AMOUNT, PAY_TRANSACTIONQ.SER_PER_REC, PAY_TRANSACTIONQ.SER_TAXABLE_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_CESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_HCESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_REC_TOT, PAY_TRANSACTIONQ.SER_PER_PRO, PAY_TRANSACTIONQ.SER_TAXABLE_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_CESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_HCESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_PRO_TOT, PAY_TRANSACTIONQ.NET_TOTAL, PAY_TRANSACTIONQ.DEDUCTION, PAY_TRANSACTIONQ.TOTAL, PAY_TRANSACTIONQ.BILL_YEAR, PAY_TRANSACTIONQ_DETAILS.SR_NO, PAY_TRANSACTIONQ_DETAILS.PARTICULAR, PAY_TRANSACTIONQ_DETAILS.DESIGNATION, PAY_TRANSACTIONQ_DETAILS.QUANTITY, PAY_TRANSACTIONQ_DETAILS.RATE, PAY_TRANSACTIONQ_DETAILS.AMOUNT, PAY_CUSTOMER_MASTER.CUST_NAME, PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE, PAY_CUSTOMER_MASTER.CITY, PAY_CUSTOMER_MASTER.PIN, PAY_CUSTOMER_MASTER.CONTACT_PERSON,PAY_TRANSACTIONQ.customer_gst_no,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO_AMT,PAY_TRANSACTIONQ_DETAILS.hsn_code,PAY_TRANSACTIONQ_DETAILS.sac_code FROM  PAY_TRANSACTIONQ INNER JOIN  PAY_TRANSACTIONQ_DETAILS ON PAY_TRANSACTIONQ.COMP_CODE = PAY_TRANSACTIONQ_DETAILS.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE = PAY_TRANSACTIONQ_DETAILS.TASK_CODE AND PAY_TRANSACTIONQ.DOC_NO = PAY_TRANSACTIONQ_DETAILS.DOC_NO INNER JOIN PAY_CUSTOMER_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_CUSTOMER_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.CUST_CODE = PAY_CUSTOMER_MASTER.CUST_ID INNER JOIN PAY_COMPANY_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_COMPANY_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE='INV' AND  PAY_TRANSACTIONQ.DOC_NO >='" + txt_docno.Text + "' AND PAY_TRANSACTIONQ.DOC_NO <='" + txt_docno.Text + "'  AND PAY_TRANSACTIONQ.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  PAY_TRANSACTIONQ.DOC_NO,PAY_TRANSACTIONQ_DETAILS.SR_NO ";
                if (ddl_vendortype.SelectedValue == "Regular")
                {
                    string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, pay_vendor_master.VEND_ID As CUST_ID, pay_vendor_master.VEND_NAME As CUST_NAME,pay_vendor_master.VEND_ADD1 As CUST_ADD1, pay_vendor_master.VEND_ADD2 As CUST_ADD2  , pay_vendor_master.txtbillcity AS Expr1, pay_vendor_master.CITY AS Expr2, (select  STATE_CODE  from pay_state_master inner join  PAY_COMPANY_MASTER  on  pay_state_master .STATE_NAME=  PAY_COMPANY_MASTER . STATE    limit 1) AS 'Expr3',pay_transactionp.DOC_NO , DATE_FORMAT(pay_transactionp.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transactionp.BILL_MONTH,pay_transactionp.DISCOUNT,pay_transactionp.DISCOUNTED_PRICE,pay_transactionp.TAXABLE_AMT,pay_transactionp.NET_TOTAL,pay_transactionp.EXTRA_CHRGS,pay_transactionp.EXTRA_CHRGS_AMT,pay_transactionp.EXTRA_CHRGS_TAX,pay_transactionp.EXTRA_CHRGS_TAX_AMT,pay_transactionp.FINAL_PRICE,pay_transactionp.BILL_YEAR,pay_transactionp.customer_gst_no,DATE_FORMAT(pay_transactionp.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transactionp.BILL_ADDRESS,pay_transactionp.SHIPPING_ADDRESS,pay_transactionp.SALES_PERSON,pay_transactionp.CUSTOMER_NOTES,pay_transactionp.TERMS_CONDITIONS, pay_transactionp . pur_order_no  as 'p_o_no',pay_transactionp.vendor_invoice_no as 'TRANSPORT',pay_transactionp.VEHICLE_NO,pay_transactionp_DETAILS.SR_NO,pay_transactionp_DETAILS.PARTICULAR,pay_transactionp_DETAILS.DESCRIPTION,pay_transactionp_DETAILS.VAT,pay_transactionp_DETAILS.hsn_number,pay_transactionp_DETAILS.DESIGNATION,pay_transactionp_DETAILS.QUANTITY,pay_transactionp_DETAILS.RATE,pay_transactionp_DETAILS.DISCOUNT,pay_transactionp_DETAILS.DISCOUNT_AMT,pay_transactionp_DETAILS.AMOUNT,  pay_transactionp. company_shiping_name  as 'narration',pay_transactionp. company_shiping_address  as 'ecc_code_no',pay_transactionp. company_shiping_gst_no  as 'freight',pay_transactionp. company_shiping_state  as 'cust_admin',(SELECT  STATE_CODE  FROM   pay_state_master  INNER JOIN  pay_transactionp  ON  pay_state_master . STATE_NAME  =  pay_transactionp . company_shiping_state  limit 1) AS 'SALES_MOBILE_NO' FROM pay_vendor_master, PAY_COMPANY_MASTER, pay_transactionp_DETAILS, pay_transactionp WHERE   pay_vendor_master . COMP_CODE  =  pay_transactionp . COMP_CODE  AND  pay_vendor_master . VEND_ID  =  pay_transactionp . CUST_CODE  AND  pay_transactionp_DETAILS . COMP_CODE  =  pay_transactionp . COMP_CODE  AND  pay_transactionp_DETAILS . TASK_CODE  =  pay_transactionp . TASK_CODE   AND  pay_transactionp_DETAILS . DOC_NO  =  pay_transactionp . DOC_NO 	AND  pay_vendor_master . COMP_CODE  =  PAY_COMPANY_MASTER . COMP_CODE  AND pay_transactionp_details.TASK_CODE='STP'   AND pay_transactionp_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transactionp_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transactionp_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionp_details.DOC_NO,pay_transactionp_details.SR_NO ";
                    MySqlCommand cmd = new MySqlCommand(query, d.con1);
                    MySqlDataReader sda = null;
                    d.con1.Open();
                    try
                    {
                        sda = cmd.ExecuteReader();
                        dt.Load(sda);

                        foreach (DataRow dr_dis_col in dt.Rows)
                        {
                            if (Convert.ToDouble(dr_dis_col["DISCOUNT1"].ToString()) > 0)
                            {
                                discount_counter = "discount";
                            }
                        }
                    }
                    catch (Exception ex) { throw ex; }
                }
                // vikas24/11 string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, pay_vendor_master.VEND_ID As CUST_ID, pay_vendor_master.VEND_NAME As CUST_NAME,pay_vendor_master.VEND_ADD1 As CUST_ADD1, pay_vendor_master.VEND_ADD2 As CUST_ADD2  , pay_vendor_master.txtbillcity AS Expr1, pay_vendor_master.CITY AS Expr2, (select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transactionp.DOC_NO , DATE_FORMAT(pay_transactionp.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transactionp.NARRATION,pay_transactionp.BILL_MONTH,pay_transactionp.DISCOUNT,pay_transactionp.DISCOUNTED_PRICE,pay_transactionp.TAXABLE_AMT,pay_transactionp.NET_TOTAL,pay_transactionp.EXTRA_CHRGS,pay_transactionp.EXTRA_CHRGS_AMT,pay_transactionp.EXTRA_CHRGS_TAX,pay_transactionp.EXTRA_CHRGS_TAX_AMT,pay_transactionp.SALES_MOBILE_NO,pay_transactionp.FINAL_PRICE,pay_transactionp.BILL_YEAR,pay_transactionp.customer_gst_no,DATE_FORMAT(pay_transactionp.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transactionp.BILL_ADDRESS,pay_transactionp.SHIPPING_ADDRESS,pay_transactionp.SALES_PERSON,pay_transactionp.CUSTOMER_NOTES,pay_transactionp.TERMS_CONDITIONS,pay_transactionp.p_o_no,pay_transactionp.TRANSPORT,pay_transactionp.FREIGHT,pay_transactionp.VEHICLE_NO,pay_transactionp_DETAILS.SR_NO,pay_transactionp_DETAILS.PARTICULAR,pay_transactionp_DETAILS.DESCRIPTION,pay_transactionp_DETAILS.VAT,pay_transactionp_DETAILS.hsn_number,pay_transactionp_DETAILS.DESIGNATION,pay_transactionp_DETAILS.QUANTITY,pay_transactionp_DETAILS.RATE,pay_transactionp_DETAILS.DISCOUNT,pay_transactionp_DETAILS.DISCOUNT_AMT,pay_transactionp_DETAILS.AMOUNT FROM pay_vendor_master, PAY_COMPANY_MASTER, pay_transactionp_DETAILS, pay_transactionp WHERE  `pay_vendor_master`.`COMP_CODE` = `pay_transactionp`.`COMP_CODE` AND `pay_vendor_master`.`VEND_ID` = `pay_transactionp`.`CUST_CODE` AND `pay_transactionp_DETAILS`.`COMP_CODE` = `pay_transactionp`.`COMP_CODE` AND `pay_transactionp_DETAILS`.`TASK_CODE` = `pay_transactionp`.`TASK_CODE`  AND `pay_transactionp_DETAILS`.`DOC_NO` = `pay_transactionp`.`DOC_NO`	AND `pay_vendor_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND pay_transactionp_details.TASK_CODE='STP'   AND pay_transactionp_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transactionp_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transactionp_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionp_details.DOC_NO,pay_transactionp_details.SR_NO";
                else
                {
                    string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  pay_transactionp.CUST_NAME, (select  STATE_CODE  from pay_state_master inner join  PAY_COMPANY_MASTER  on  pay_state_master .STATE_NAME=  PAY_COMPANY_MASTER . STATE    limit 1) AS 'Expr3',pay_transactionp.DOC_NO , DATE_FORMAT(pay_transactionp.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transactionp.BILL_MONTH,pay_transactionp.DISCOUNT,pay_transactionp.DISCOUNTED_PRICE,pay_transactionp.TAXABLE_AMT,pay_transactionp.NET_TOTAL,pay_transactionp.EXTRA_CHRGS,pay_transactionp.EXTRA_CHRGS_AMT,pay_transactionp.EXTRA_CHRGS_TAX,pay_transactionp.EXTRA_CHRGS_TAX_AMT,pay_transactionp.SALES_MOBILE_NO,pay_transactionp.FINAL_PRICE,pay_transactionp.BILL_YEAR,pay_transactionp.customer_gst_no,DATE_FORMAT(pay_transactionp.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transactionp.BILL_ADDRESS,pay_transactionp.SHIPPING_ADDRESS,pay_transactionp.SALES_PERSON,pay_transactionp.CUSTOMER_NOTES,pay_transactionp.TERMS_CONDITIONS, pay_transactionp . pur_order_no  as 'p_o_no',pay_transactionp.vendor_invoice_no as 'TRANSPORT',pay_transactionp.VEHICLE_NO,pay_transactionp_DETAILS.SR_NO,pay_transactionp_DETAILS.PARTICULAR,pay_transactionp_DETAILS.DESCRIPTION,pay_transactionp_DETAILS.VAT,pay_transactionp_DETAILS.hsn_number,pay_transactionp_DETAILS.DESIGNATION,pay_transactionp_DETAILS.QUANTITY,pay_transactionp_DETAILS.RATE,pay_transactionp_DETAILS.DISCOUNT,pay_transactionp_DETAILS.DISCOUNT_AMT,pay_transactionp_DETAILS.AMOUNT,  pay_transactionp. company_shiping_name  as 'narration',pay_transactionp. company_shiping_address  as 'ecc_code_no',pay_transactionp. company_shiping_gst_no  as 'freight',pay_transactionp. company_shiping_state  as 'cust_admin',vendor_invoice_no as '' FROM  PAY_COMPANY_MASTER, pay_transactionp_DETAILS, pay_transactionp WHERE    pay_transactionp_DETAILS . COMP_CODE  =  pay_transactionp . COMP_CODE  AND  pay_transactionp_DETAILS . TASK_CODE  =  pay_transactionp . TASK_CODE   AND  pay_transactionp_DETAILS . DOC_NO  =  pay_transactionp . DOC_NO 	 AND pay_transactionp_details.TASK_CODE='STP'   AND pay_transactionp_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transactionp_details.DOC_NO <='" + lbl_print_quote.Text + "' AND PAY_COMPANY_MASTER.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionp_details.DOC_NO,pay_transactionp_details.SR_NO";
                    MySqlCommand cmd = new MySqlCommand(query, d.con1);
                    MySqlDataReader sda = null;
                    d.con1.Open();
                    try
                    {
                        sda = cmd.ExecuteReader();
                        dt.Load(sda);

                        foreach (DataRow dr_dis_col in dt.Rows)
                        {
                            if (Convert.ToDouble(dr_dis_col["DISCOUNT1"].ToString()) > 0)
                            {
                                discount_counter = "discount";
                            }
                        }
                    }
                    catch (Exception ex) { throw ex; }
                }


                if (txt_year.Text == "C")
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoice_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoice.rpt"));
                    }
                }
                else
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoiceI_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoiceI.rpt"));
                    }

                }
                if (Session["COMP_CODE"].ToString() == "C02")
                {
                    headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_1.png");
                    //footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                    crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerpath + "'";
                    //crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
                }
                else
                {
                    headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\logo.png");
                    // footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                    crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerpath + "'";
                    // crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
                }
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                //CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "TaxInvoice");
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }

    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }

    public void text_clear()
    {
        txt_tot_discount_percent.Text = "0";
        txt_tot_discount_amt.Text = "0";
        txt_freight.Text = "0";
        txt_vehicle.Text = "";
        txt_p_o_no.Text = "";
        //   txt_year.Text = "";
        ddl_customerlist.SelectedValue = "Select";
        txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        txt_vendorname.Text = "";
        //ddlcalmonth.SelectedValue = "0";
        //txt_designation.SelectedIndex = 0;
        txt_quantity.Text = "";
        txt_rate.Text = "";
        txt_amount.Text = "";
        txt_narration.Text = "";
        txt_grossamt.Text = "0";
        txt_net_total_1.Text = "";
        txt_expiry_date.Text = "";
        txt_bill_add.Text = "";
        txt_ship_add.Text = "";
        txt_referenceno2.Text = "";
        txt_customer_notes.Text = "";
        txt_terms_conditions.Text = "";
        txt_customer_gst.Text = "";
        ddl_sales_person.Text = "";
        // txt_particular.SelectedValue = "Select";
        txt_desc.Text = "";
        txt_description.Text = "0";
        txt_hsn.Text = "";
        txt_per_unit.Text = "0";
        ddl_sales_person.Text = "";
        txt_discount_price.Text = "0";
        txt_discount_rate.Text = "0";
        txt_start_date.Text = txt_docdate.Text;
        txt_end_date.Text = txt_docdate.Text;
        ddl_vendor.SelectedIndex = 0;
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        gv_itemslist.Visible = false;
        Panel4.Visible = false;
        txt_taxable_amt.Text = "0";
        txt_sub_total1.Text = "0";
        txt_extra_chrgs.Text = "";
        txt_extra_chrgs_amt.Text = "0";
        txt_extra_chrgs_tax.Text = "0";
        txt_extra_chrgs_tax_amt.Text = "0";
        txt_sub_total2.Text = "";
        txt_final_total.Text = "";
        txtsalesmobileno.Text = "";
        ddl_vendortype.SelectedValue = "Select";
        txt_bank_ac.Text = "";
        txt_bank_no.Text = "";
        txt_ifc_code.Text = "";
        txt_credit_perod.Text = "";
        ddl_transport.SelectedValue = "Select";
        txt_vendor_no.Text = "";
        //Panel1.Visible = false;
        //d.operation("truncate table '" + Table1 + "'");

    }
    protected void txt_quantity_TextChanged(object sender, EventArgs e)
    {
        float rate = float.Parse(txt_rate.Text);
        float quantity = float.Parse(txt_quantity.Text);
        if (quantity > 0)
        {
            double amount = rate * quantity;
            txt_amount.Text = Math.Round(amount, 2).ToString();
            if (Session["DISCOUNT_BY"].ToString() == "PER")
            {
                txt_discount_rate_TextChanged(sender, e);
            }
            else
            {
                txt_discount_price_TextChanged(sender, e);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Quantity must be greater than 0');", true);
            txt_quantity.Text = "0";
            //txt_quantity.Focus();
        }
        tooltrip();
    }
    protected void txt_rate_TextChanged(object sender, EventArgs e)
    {
        //    if (txt_rate.Text != "" && txt_quantity.Text != "")
        //    {

        //        float rate = float.Parse(txt_rate.Text);
        //        float quantity = float.Parse(txt_quantity.Text);
        //        if (quantity > 0)
        //        {
        //            double amount = rate * quantity;
        //            txt_amount.Text = Math.Round(amount, 2).ToString();
        //            lnkbtn_addmoreitem.Focus();
        //        }
        //        else
        //        {
        //        }
        //    }

        if (txt_rate.Text != "" && txt_quantity.Text != "")
        {

            float rate = float.Parse(txt_rate.Text);
            float quantity = float.Parse(txt_quantity.Text);
            if (quantity > 0)
            {
                double amount = rate * quantity;
                txt_amount.Text = Math.Round(amount, 2).ToString();
                if (Session["DISCOUNT_BY"].ToString() == "PER")
                {
                    txt_discount_rate_TextChanged(sender, e);
                }
                else
                {
                    txt_discount_price_TextChanged(sender, e);
                }
            }
            else
            {
            }
        }
        txt_rate.ToolTip = txt_rate.Text;
    }

    protected void btn_ShowST_Click(object sender, EventArgs e)
    {

        //btn_ShowST_ModalPopupExtender.Show();
        //Panel4.Visible = true;
    }

    protected void txt_ser_tax_per_pro_TextChanged(object sender, EventArgs e)
    {
        //btn_ShowST_ModalPopupExtender.Show();
    }
    private void tooltrip()
    {
        //txt_bharat_education.ToolTip = txt_bharat_education.Text;
        //txt_doc_number.ToolTip = txt_doc_number.Text;
        txt_customername.ToolTip = txt_customername.Text;
        txt_docno.ToolTip = txt_docno.Text;
        // ddl_customerlist.ToolTip = ddl_customerlist.SelectedItem.ToString();
        txt_docdate.ToolTip = txt_docdate.Text;
        //ddlcalmonth.ToolTip = ddlcalmonth.Text;
        txt_year.ToolTip = txt_year.Text;
        txt_referenceno1.ToolTip = txt_referenceno1.Text;
        txt_narration.ToolTip = txt_narration.Text;
        txt_p_o_no.ToolTip = txt_p_o_no.Text;
        ddl_transport.ToolTip = ddl_transport.SelectedValue.ToString();
        txt_freight.ToolTip = txt_freight.Text;
        txt_vehicle.ToolTip = txt_vehicle.Text;
        //txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
        txt_designation.ToolTip = txt_designation.Text;
        txt_quantity.ToolTip = txt_quantity.Text;
        txt_rate.ToolTip = txt_rate.Text;
        ///tabel particular
        ///

        //  txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
        txt_desc.ToolTip = txt_desc.Text;
        txt_description.ToolTip = txt_description.Text;
        txt_hsn.ToolTip = txt_hsn.Text;

        //txt_per_unit.ToolTip = txt_per_unit.Text;
        txt_quantity.ToolTip = txt_quantity.Text;
        txt_rate.ToolTip = txt_rate.Text;
        txt_discount_rate.ToolTip = txt_discount_rate.Text;
        txt_discount_price.ToolTip = txt_discount_price.Text;
        txt_amount.ToolTip = txt_amount.Text;
        txt_start_date.ToolTip = txt_start_date.Text;
        txt_end_date.ToolTip = txt_end_date.Text;


    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

        string final1 = d.getsinglestring(" select DOC_NO from pay_transactionp where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_invoice_flag = 1");
        if (final1 != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This Invoice IS Final YOU Can Not Update  !!');", true);

            return;

        }
        update_flag = 2;

        if (budget())
        {
            return;
        }
     
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result = 0;
            int invoice_result = 0;
            int item_result = 0;
            int TotalRows = gv_itemslist.Rows.Count;



            string l_docnumber = txt_docno.Text;
            lbl_print_quote.Text = l_docnumber;
            d.operation("DELETE FROM pay_transactionp WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");

            d.operation("DELETE FROM pay_transactionp_DETAILS WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");

            final_total();
            string final = txt_final_total.Text;
            if (ddl_vendortype.SelectedValue == "Regular")
            {

                invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD,month,year,pur_order_no,company_shiping_name,company_shiping_address,company_shiping_gst_no,company_shiping_state,vendor_invoice_no) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "','" + txt_docdate.Text.Substring(3, 2) + "','" + txt_docdate.Text.Substring(6) + "','" + ddl_po_num.SelectedValue + "','" + txt_ship.Text + "','" + txt_ship_address.Text + "','" + txt_ship_gst_no.Text + "','" + ddl_shiping_state.SelectedValue + "','" + txt_vendor_no.Text + "')");
            }
            else
            {
                invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD,month,year,pur_order_no,company_shiping_name,company_shiping_address,company_shiping_gst_no,company_shiping_state,vendor_invoice_no) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "','" + txt_docdate.Text.Substring(3, 2) + "','" + txt_docdate.Text.Substring(6) + "','" + ddl_po_num.SelectedValue + "','" + txt_ship.Text + "','" + txt_ship_address.Text + "','" + txt_ship_gst_no.Text + "','" + ddl_shiping_state.SelectedValue + "','" + txt_vendor_no.Text + "')");
            }


            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                Label lbl_srnumber = (Label)row.FindControl("lbl_srnumber");
                int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                string item_code = (lbl_item_code.Text);
                Label lbl_item_type = (Label)row.FindControl("lbl_item_type");
                string item_type = (lbl_item_type.Text);
                TextBox lbl_particular = (TextBox)row.FindControl("lbl_particular");
                string particular = lbl_particular.Text.ToString();
                TextBox lbl_Description_final = (TextBox)row.FindControl("lbl_Description_final");
                string lbl_Description_final1 = lbl_Description_final.Text.ToString();
                TextBox lbl_vat = (TextBox)row.FindControl("lbl_vat");
                string vat = lbl_vat.Text.ToString();
                TextBox lbl_hsc_no = (TextBox)row.FindControl("lbl_hsn_code");
                string lbl_hsc_code = lbl_hsc_no.Text.ToString();
                TextBox lbl_designation = (TextBox)row.FindControl("lbl_designation");
                string designation = lbl_designation.Text;
                TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                float quantity = float.Parse(txt_quantity.Text);
                TextBox txt_rate = (TextBox)row.FindControl("lbl_rate");
                float rate = float.Parse(txt_rate.Text);
                TextBox txt_product_discount = (TextBox)row.FindControl("lbl_discount");
                float product_discount = float.Parse(txt_product_discount.Text);
                TextBox txt_product_discount_amt = (TextBox)row.FindControl("lbl_discount_amt");
                float product_discount_amt = float.Parse(txt_product_discount_amt.Text);
                TextBox lbl_amount = (TextBox)row.FindControl("lbl_amount");
                float amount = float.Parse(lbl_amount.Text);
                //TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                //string start_date = lbl_start_date.Text.ToString(); vikas comment 22/11
                TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                string end_date = lbl_end_date.Text.ToString();
                TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                string vendor = lbl_vendor.Text.ToString();

                TextBox lbl_uniformsize = (TextBox)row.FindControl("lbl_uniformsize");
                string uniformsize = lbl_uniformsize.Text.ToString();
                TextBox lbl_shoessixe = (TextBox)row.FindControl("lbl_shoessize");
                string shoes = lbl_shoessixe.Text.ToString();
                //    string query = "INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "'";

                // int insert_result =  d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR,item_type,size_uniform,size_shoes) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "')");
                int insert_result = d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,END_DATE,VENDOR,item_type,size_uniform,size_shoes) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "')");
                if (insert_result > 0)
                {
                    try
                    {
                        item_result = item_result + insert_result;
                        string sum_inward = (" select sum(QUANTITY) from pay_transactionp_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                        MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                        d_sum.con1.Open();
                        MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                        if (dr_sum_inward.Read())
                        {
                            double doubleoinword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                            int updaterecord = d.operation("update pay_item_master SET inward ='" + doubleoinword + "' where Item_Code='" + item_code + "'");
                        }
                        dr_sum_inward.Close();
                        cmd_sum.Dispose();
                        d_sum.con1.Close();
                        string query1 = (" select inward,outward,Stock from pay_item_master  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");

                        MySqlCommand cmd1 = new MySqlCommand(query1, d.con);
                        d.con.Open();
                        MySqlDataReader sda = cmd1.ExecuteReader();

                        if (sda.Read())
                        {
                            double doubleoinword = Convert.ToDouble(sda.GetValue(0).ToString());
                            double doubleoutword = Convert.ToDouble(sda.GetValue(1).ToString());
                            double doublestock = Convert.ToDouble(sda.GetValue(2).ToString());

                            // double iquantity = Convert.ToDouble(txt_quantity.Text);
                            // doubleoinword = doubleoinword + iquantity;

                            doublestock = doubleoinword - doubleoutword;

                            int updaterecord = d.operation("update pay_item_master set outward='" + doubleoutword + "' ,Stock= '" + doublestock + "'  where Item_Code='" + item_code + "'");
                        }
                        sda.Close();
                        d.con.Close();
                    }

                    catch { }
                }
                insert_result = 0;


            }
            if (invoice_result > 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Invoice (" + txt_docno.Text + ") Updated successfully!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Invoice (" + txt_docno.Text + ") Updated successfully!');", true);
                Panel5.Visible = false;
                gv_bynumber_name.Visible = false;
                attached_doc();
                lbl_print_quote.Text = txt_docno.Text;

            }
            else
            {
                d.operation("Delete from pay_transactionp Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                d.operation("Delete from pay_transactionp_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
            }

        }

        catch (Exception ee)
        {
            d.operation("Delete from pay_transactionp Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete from pay_transactionp_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
            text_clear();
        }
        finally
        {
            d.con.Close();
           
            gendocno();
            tooltrip();
            btn_Save.Visible = true; ;
            btn_delete.Visible = false;
            btn_update.Visible = false;
        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {

        string final1 = d.getsinglestring(" select DOC_NO from pay_transactionp where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_invoice_flag = 1");
        if (final1 != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This Invoice IS Final YOU Can Not Delete  !!');", true);
            return;

        }
        
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result = 0;


            string l_docnumber = txt_docno.Text;

            d.operation("DELETE FROM pay_transactionp WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");

            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                string item_code = (lbl_item_code.Text);
                TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                float quantity = float.Parse(txt_quantity.Text);

                d.operation("DELETE FROM pay_transactionp_details WHERE Item_Code = '" + item_code + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");
                string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_inward from pay_transactionp_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                d_sum.con1.Open();
                MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                if (dr_sum_inward.Read())
                {
                    double doubleoinword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                    int updaterecord = d.operation("update pay_item_master SET inward ='" + doubleoinword + "' where Item_Code='" + item_code + "'");
                }
                dr_sum_inward.Close();
                cmd_sum.Dispose();
                d_sum.con1.Close();
                string query1 = (" select inward,outward,Stock from pay_item_master  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                MySqlCommand cmd1 = new MySqlCommand(query1, d.con);
                d.con.Open();
                MySqlDataReader sda = cmd1.ExecuteReader();
                if (sda.Read())
                {
                    double doubleoinword = Convert.ToDouble(sda.GetValue(0).ToString());
                    double doubleoutword = Convert.ToDouble(sda.GetValue(1).ToString());
                    double doublestock = Convert.ToDouble(sda.GetValue(2).ToString());

                    doublestock = doubleoinword - doubleoutword;

                    //  int updaterecord = d.operation("update pay_item_master inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code='" + item_code + "'");

                    int updaterecord = d.operation("update pay_item_master set outward='" + doubleoutword + "' ,Stock= '" + doublestock + "'  where Item_Code = '" + item_code + "' ");
                }
                sda.Close();
                d.con.Close();
            } ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('" + ddl_customerlist.Text + " Deleted Successfully');", true);
            //   Panel1.Visible = false;

            Panel8.Visible = true;
            Panel6.Visible = true;
            Panel5.Visible = false;

        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            text_clear();
            gendocno();
            btn_Save.Visible = true;
            btn_delete.Visible = false;
            btn_update.Visible = false;
        }

    }

    protected void btn_searchvendor_Click(object sender, EventArgs e)
    {
        // hide.Visible = false;
        hidtab.Value = "0";
        if (txt_docno_number.Text != "")
        {
            MySqlCommand cmd_docno = new MySqlCommand("SELECT  pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM pay_transactionp WHERE (pay_transactionp.DOC_NO LIKE '%" + txt_docno_number.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC ", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_docno = cmd_docno.ExecuteReader();
                if (dr_docno.HasRows)
                {
                    Panel5.Visible = true;
                    Panel6.Visible = true;
                    gv_bynumber_name.Visible = true;
                    gv_bynumber_name.DataSource = dr_docno;
                    gv_bynumber_name.DataBind();
                }
                else
                {
                    gv_bynumber_name.Visible = false;
                    Panel5.Visible = false;
                    Panel6.Visible = false;

                }
                dr_docno.Close();
                //vikas24/11 for read only
                txt_customer_gst.ReadOnly = true;
                txt_bill_add.ReadOnly = true;
                txt_bank_ac.ReadOnly = true;
                txt_bank_no.ReadOnly = true;
                txt_ifc_code.ReadOnly = true;
                txt_credit_perod.ReadOnly = true;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        if (txt_customername.Text != "")
        {
            gv_bynumber_name.DataSource = null;
            gv_bynumber_name.DataBind();
            MySqlCommand cmd_docno = new MySqlCommand("SELECT  pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM pay_transactionp WHERE (pay_transactionp.`CUST_NAME` LIKE '%" + txt_customername.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC ", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_docno = cmd_docno.ExecuteReader();
                if (dr_docno.HasRows)
                {
                    Panel5.Visible = true;
                    gv_bynumber_name.Visible = true;
                    gv_bynumber_name.DataSource = dr_docno;
                    gv_bynumber_name.DataBind();
                }
                else
                {
                    gv_bynumber_name.Visible = false;
                    Panel5.Visible = false;

                }
                dr_docno.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        if (gv_bynumber_name.Rows.Count > 0)
        {
            if (txt_customername.Text != "")
            {
                // MySqlCommand cmd_customername = new MySqlCommand("SELECT pay_transactionp.CUST_NAME,pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') as DOC_DATE,NET_TOTAL FROM pay_transactionp WHERE (pay_transactionp.CUST_NAME LIKE '%" + txt_customername.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC", d.con);
                MySqlCommand cmd_customername = new MySqlCommand("SELECT  pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM pay_transactionp WHERE (pay_transactionp.`CUST_NAME` LIKE '%" + txt_customername.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC ", d.con);
                d.con.Open();
                try
                {
                    MySqlDataReader dr_customername = cmd_customername.ExecuteReader();
                    if (dr_customername.HasRows)
                    {
                        Panel5.Visible = true;
                        Panel6.Visible = true;//vikas22/11
                        gv_bynumber_name.Visible = true;
                        gv_bynumber_name.DataSource = dr_customername;
                        gv_bynumber_name.DataBind();
                    }
                    else
                    {
                        gv_bynumber_name.Visible = false;
                        Panel5.Visible = false;
                        Panel6.Visible = false;

                    }
                    dr_customername.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con.Close();
                }
            }
        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found for')", true);
        }
        // SELECT  PAY_TRANSACTIONQ.DOC_NO,PAY_TRANSACTIONQ.DOC_DATE AS 'DOC_DATE',CUST_NAME FROM PAY_TRANSACTIONQ   ORDER BY PAY_TRANSACTIONQ.DOC_DATE DESC 
    }

    protected void txt_customername_TextChanged(object sender, EventArgs e)
    {
        string customername = txt_customername.Text;
        if (customername != "")
        {
            txt_docno.Text = "";
        }
        Panel2.Visible = false;
    }

    protected void txt_docno_TextChanged(object sender, EventArgs e)
    {
        string doc_no = txt_docno.Text;
        if (doc_no != "")
        {
            txt_customername.Text = "";
        }
        // Panel6.Visible = false; vikas comments22/11
    }


    protected void particular_hsn_sac_code(object sender, EventArgs e)
    {
        txt_designation.Visible = true;
        if (txt_particular.SelectedItem.ToString() != "Select")
        {
                       MySqlCommand cmd = new MySqlCommand("Select item_description,PURCHASE_RATE,unit,VAT,case When hsn_number <> '' then hsn_number else sac_number END as hsn_sac_no , case When hsn_number <> '' then 'H' else 'S' END as hsn,unit_per_piece,Stock,(select PURCHASE_RATE from pay_transactionp where comp_code='" + Session["COMP_CODE"].ToString() + "'AND ITEM_CODE = '" + txt_particular.SelectedValue + "' ORDER BY `ITEM_CODE`  DESC LIMIT 1),product_service,size from PAY_ITEM_MASTER where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ITEM_CODE = '" + txt_particular.SelectedValue + "'", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item = cmd.ExecuteReader();
                while (dr_item.Read())
                {
                    txt_desc.Text = dr_item.GetValue(0).ToString();
                      
                    string unit = dr_item.GetValue(2).ToString();
                    update_listbox(txt_designation, unit);
                    txt_description.Text = dr_item.GetValue(3).ToString();
                    txt_hsn.Text = dr_item.GetValue(4).ToString();
                    if (dr_item[5].ToString() == "H")
                    {
                        lbl_hsn.Text = "HSN Code";
                    }
                    else
                    {
                        lbl_hsn.Text = "SAC Code";
                    }
                    txt_per_unit.Text = dr_item.GetValue(6).ToString();
                    lbl_qty.Text = dr_item.GetValue(7).ToString();
                    lbl_raten.Text = dr_item.GetValue(8).ToString();
                    if (dr_item[9].ToString() == "Shoes")
                    {
                        ddl_shoosesize.SelectedValue = dr_item.GetValue(10).ToString();
                        ddl_uniformsize.SelectedValue = "Select";
                        ddl_pantry_size.SelectedValue = "Select";
                        ddl_apron_size1.SelectedValue = "Select";
                    }
                    else if (dr_item[9].ToString() == "Uniform")
                    {
                        ddl_uniformsize.SelectedValue = dr_item.GetValue(10).ToString();
                        ddl_shoosesize.SelectedValue ="Select";
                        ddl_pantry_size.SelectedValue = "Select";
                        ddl_apron_size1.SelectedValue = "Select";
                    }
                    else if (dr_item[9].ToString() == "pantry_jacket")
                    {
                        ddl_pantry_size.SelectedValue = dr_item.GetValue(10).ToString();
                        ddl_uniformsize.SelectedValue = "Select";
                        ddl_shoosesize.SelectedValue = "Select";
                        ddl_apron_size1.SelectedValue = "Select";
                    }
                    else if (dr_item[9].ToString() == "Apron" || dr_item[9].ToString() == "apron")
                    {
                        ddl_apron_size1.SelectedValue = dr_item.GetValue(10).ToString();
                        ddl_pantry_size.SelectedValue = "Select";
                        ddl_uniformsize.SelectedValue = "Select";
                        ddl_shoosesize.SelectedValue = "Select";
                    }
                    
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
               
                
                d.con.Close();
                txt_hsn.Enabled = false;
                lbl_raten.Visible = true;
                lbl_rete1.Visible = true;
                //vikas16/11
                txt_quantity1.Visible = true;
                lbl_qty.Visible = true;
                txt_quantity1.Visible = true;
                // txt_designation.Enabled = false;
                // txt_rate.Enabled = false;
                // txt_description.Enabled = false;
                tooltrip();

                //MySqlCommand cmdrate = new MySqlCommand("select ITEM_CODE from  pay_transactionp_details where  ITEM_CODE='" + txt_particular.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
                
                po_qty();
                txt_quantity_TextChanged(null, null);
                get_purchase_rate();
                txt_rate_TextChanged(null, null);
            }

        }

    }
    protected void po_qty()
    {
        MySqlCommand cmdrate = new MySqlCommand("select `QUANTITY` from pay_transaction_po_details where po_no='" + ddl_po_num.SelectedValue + "'  and `ITEM_CODE`='" + txt_particular.SelectedValue+ "'", d.con);
        d.con.Open();
        MySqlDataReader dr = cmdrate.ExecuteReader();
        if (dr.Read())
        {
            txt_quantity.Text = dr.GetValue(0).ToString();
           
            dr.Close();
            d.con.Close();
        }

    }
    protected void get_purchase_rate()
    {
        MySqlCommand cmdrate = new MySqlCommand("select ITEM_CODE,RATE from  pay_transactionp_details where  ITEM_CODE='" + txt_particular.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' ORDER BY `DOC_NO` DESC LIMIT 1 ", d.con);
        d.con.Open();
        MySqlDataReader dr = cmdrate.ExecuteReader();
        if (dr.Read())
        {
            txt_rate.Text = dr.GetValue(1).ToString();
            lbl_raten.Text = dr.GetValue(1).ToString();
            dr.Close();
            d.con.Close();
        }
        else
        {
            MySqlCommand cmdrate1 = new MySqlCommand("select ITEM_CODE,PURCHASE_RATE from  pay_item_master where  ITEM_CODE='" + txt_particular.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' ", d1.con);
            d1.con.Open();
            MySqlDataReader dr1 = cmdrate1.ExecuteReader();
            if (dr1.Read())
            {
                txt_rate.Text = dr1.GetValue(1).ToString();
                lbl_raten.Text = dr1.GetValue(1).ToString();
                dr1.Close();
                d1.con.Close();
            }

            d.con.Close();
            d1.con.Close();

        }


    }
    protected void customer_details(object sender, EventArgs e)
    {
        //vikas add for po num 
        if (ddl_customerlist.SelectedValue != "Select")
        {
          
            ddl_po_num.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select po_no from pay_transaction_po where comp_code='" + Session["comp_code"].ToString() + "' and CUST_CODE='" + ddl_customerlist.SelectedValue + "' ", d3.con);
            d3.con.Open();
           
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_po_num.DataSource = dt_item;
                    ddl_po_num.DataTextField = dt_item.Columns[0].ToString();
                    ddl_po_num.DataValueField = dt_item.Columns[0].ToString();
                    ddl_po_num.DataBind();
                }
                dt_item.Dispose();

                d3.con.Close();
                ddl_po_num.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                //d.con.Close();
                d3.con.Close();
            }

        }
        if (ddl_customerlist.SelectedValue != "Select")
        {
            MySqlCommand cmd = new MySqlCommand("select GST from pay_vendor_master where vend_id='" + ddl_customerlist.SelectedValue + "'", d4.con);
            d4.con.Open();
            try
            {
                MySqlDataReader dr_item = cmd.ExecuteReader();
                while (dr_item.Read())
                {
                    txt_customer_gst.Text = dr_item.GetValue(0).ToString();

                }
                dr_item.Close();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d4.con.Close();
                customer_details();
                txt_customer_gst.ReadOnly = true;
                txt_bill_add.ReadOnly = true;
                txt_bank_ac.ReadOnly = true;
                txt_bank_no.ReadOnly = true;
                txt_ifc_code.ReadOnly = true;
                txt_credit_perod.ReadOnly = true;
                // txt_bill_add.ReadOnly = true;
                gst_counter(txt_customer_gst.Text.Substring(0, 2));
            }

        }
        else
        {

            txt_customer_gst.Text = "";
            txt_customer_gst.ReadOnly = false;
        }


    }

    protected void customer_details()
    {


        if (ddl_customerlist.SelectedItem.ToString() != "Select")
        {

            MySqlCommand cmd = new MySqlCommand("select GST,concat_ws('\n',txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillcountry,txtbillzipcode) as 'Bill_Address',concat_ws('\n',txtsattention,txtsaddress,txtscity,txtssstate,txtscountry,txtszipcode) as 'Shipping_Address',`bank_account_name`,`account_number`,`ifsc_code`,`CREDIT_PERIOD` from pay_vendor_master where VEND_ID='" + ddl_customerlist.SelectedValue + "'", d4.con);
            d4.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_item = cmd.ExecuteReader();
                while (dr_item.Read())
                {
                    txt_customer_gst.Text = dr_item.GetValue(0).ToString();
                    txt_bill_add.Text = dr_item.GetValue(1).ToString();
                    txt_ship_add.Text = dr_item.GetValue(2).ToString();
                    txt_bank_ac.Text = dr_item.GetValue(3).ToString();
                    txt_bank_no.Text = dr_item.GetValue(4).ToString();
                    txt_ifc_code.Text = dr_item.GetValue(5).ToString();
                    txt_credit_perod.Text = dr_item.GetValue(6).ToString();
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d4.con.Close();
            }
        }

    }

    protected void ddl_vendortype_SelectedIndexchange(object sender, EventArgs e)
    {
        if (ddl_vendortype.SelectedValue == "Regular")
        {
            ddl_customerlist.Items.Clear();
            DataTable dt =new DataTable();
            MySqlCommand cmd_cust = new MySqlCommand("SELECT VEND_ID,CONCAT(VEND_ID,' - ',VEND_NAME)  from PAY_VENDOR_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and ACTIVE_STATUS='A' ORDER BY VEND_ID", d1.con);

            d1.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataAdapter dr_cust = new MySqlDataAdapter(cmd_cust);
                dr_cust.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    ddl_customerlist.DataSource = dt;
                   ddl_customerlist.DataTextField=dt.Columns[1].ToString();
                   ddl_customerlist.DataValueField = dt.Columns[0].ToString();
                   ddl_customerlist.DataBind();
                }
                dt.Dispose();
               // dr_cust.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
            ddl_customerlist.Items.Insert(0, "Select");
            Panel8.Visible = true;
            ddl_customerlist.Visible = true;
            txt_vendorname.Visible = false;

        }
        else
        {
            txt_vendorname.Visible = true;
            ddl_customerlist.Visible = false;
            txt_customer_gst.ReadOnly = false;
            txt_bill_add.ReadOnly = false;
            txt_bank_ac.ReadOnly = false;
            txt_bank_no.ReadOnly = false;
            txt_ifc_code.ReadOnly = false;
            txt_credit_perod.ReadOnly = false;
            txt_bank_ac.Text = "";
            txt_customer_gst.Text = "";
            txt_bank_no.Text = "";
            txt_ifc_code.Text = "";
            txt_credit_perod.Text = "";
            txt_bill_add.Text = "";



        }
    }


    public void discount_calculate(System.Data.DataTable dt_discount, int discount_choice)
    {
        double discount_percent = 0, discount_price = 0, final_price = 0, gross_total_discount = 0, gross_total_no_discount = 0; ;

        foreach (DataRow dr_discount in dt_discount.Rows)
        {
            if (Convert.ToDouble(dr_discount["DISCOUNT"].ToString()) <= 0)
            {
                gross_total_discount = gross_total_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
            }
            else
            {
                gross_total_no_discount = gross_total_no_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
            }
            c = c + Convert.ToDouble(dr_discount["Amount"].ToString());
        }

        if (c == gross_total_no_discount)
        {
            txt_tot_discount_percent.Text = "0";
            txt_tot_discount_amt.Text = "0";
            txt_tot_discount_percent.Enabled = false;
            txt_tot_discount_amt.Enabled = false;
        }
        else
        {
            txt_tot_discount_percent.Enabled = true;
            txt_tot_discount_amt.Enabled = true;
            if (discount_choice == 0)
            {
                discount_percent = Convert.ToDouble(txt_tot_discount_percent.Text);
                discount_price = Math.Round(((discount_percent * gross_total_discount) / 100), 2);
                final_price = Math.Round((gross_total_discount - discount_price), 2);
                txt_tot_discount_amt.Text = Convert.ToString(discount_price);
            }
            else if (discount_choice == 1)
            {
                discount_price = Convert.ToDouble(txt_tot_discount_amt.Text);
                discount_percent = Math.Round(((discount_price * 100) / gross_total_discount), 2);
                final_price = Math.Round((gross_total_discount - discount_price), 2);
                txt_tot_discount_percent.Text = Convert.ToString(discount_percent);
            }
        }
        txt_grossamt.Text = Convert.ToString(Math.Round(c, 2));
        txt_taxable_amt.Text = Convert.ToString(final_price + gross_total_no_discount);
        gst_calculate(dt_discount);

    }
    public void gst_calculate(System.Data.DataTable dt_gst)
    {
        System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
        Panel1.Visible = true;
        Panel1.Controls.Clear();
        Panel1.Controls.Add(Table1);
        TableRow tRow = new TableRow();
        TableCell tCell = new TableCell();
        Table1.HorizontalAlign = HorizontalAlign.Right;
        double c = 0;
        tCell.HorizontalAlign = HorizontalAlign.Right;
        //tCell.Width = 1000;
        double gst1_total = 0;
        double cgst1_total = 0;
        double total_gst = 0;
        double net_total = 0;
        double[] gst_group = { };
        DataView gst_view = new DataView(dt_gst);
        DataTable distinct_gst = gst_view.ToTable(true, "VAT");
        distinct_gst.Columns.Add("total", typeof(double));


        try
        {
            if (txt_year.Text == "C")
            {
                distinct_gst.Columns.Add("title_c", typeof(string));
                distinct_gst.Columns.Add("title_s", typeof(string));
                foreach (DataRow unique_gst in distinct_gst.Rows)
                {

                    foreach (DataRow dr_gst in dt_gst.Rows)
                    {
                        if (Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 == Convert.ToDouble(dr_gst["VAT"].ToString()) / 2)
                        {
                            cgst1_total = cgst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 200) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
                            double current_row_gst = (Convert.ToDouble(dr_gst["VAT"].ToString()) / 100) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
                            total_gst = total_gst + current_row_gst;
                            c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
                        }
                    }
                    unique_gst["total"] = cgst1_total.ToString();
                    unique_gst["title_c"] = "CGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
                    unique_gst["title_s"] = "SGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
                    cgst1_total = 0;

                    tRow = new TableRow();
                    tCell = new TableCell();
                    tCell.BorderWidth = 1;
                    tCell.Text = unique_gst[2].ToString();
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.BorderWidth = 1;
                    tCell.Text = unique_gst[1].ToString();
                    tCell.HorizontalAlign = HorizontalAlign.Left;
                    tRow.Cells.Add(tCell);

                    Table1.Rows.Add(tRow);

                    tRow = new TableRow();
                    tCell = new TableCell();
                    tCell.BorderWidth = 1;
                    tCell.Text = unique_gst[3].ToString();
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.BorderWidth = 1;
                    tCell.Text = unique_gst[1].ToString();
                    tCell.HorizontalAlign = HorizontalAlign.Left;
                    tRow.Cells.Add(tCell);

                    Table1.Rows.Add(tRow);


                }

            }
            else
            {
                distinct_gst.Columns.Add("title_i", typeof(string));
                foreach (DataRow unique_gst in distinct_gst.Rows)
                {

                    foreach (DataRow dr_gst in dt_gst.Rows)
                    {
                        if (Convert.ToDouble(unique_gst["VAT"].ToString()) == Convert.ToDouble(dr_gst["VAT"].ToString()))
                        {
                            gst1_total = gst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 100) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
                            double current_row_gst = (Convert.ToDouble(dr_gst["VAT"].ToString()) / 100) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
                            total_gst = total_gst + current_row_gst;
                            c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
                        }
                    }
                    unique_gst["total"] = gst1_total.ToString();
                    unique_gst["title_i"] = "IGST @ " + unique_gst["VAT"].ToString() + " %";
                    gst1_total = 0;

                    tRow = new TableRow();
                    for (int i = 2; i >= 1; i--)
                    {
                        tCell = new TableCell();
                        tCell.BorderWidth = 1;
                        tCell.Text = unique_gst[i].ToString();
                        if (i == 1)
                        {
                            tCell.HorizontalAlign = HorizontalAlign.Left;
                        }
                        tRow.Cells.Add(tCell);
                    }
                    Table1.Rows.Add(tRow);
                }
            }

            //txt_grossamt.Text = Math.Round(c, 2).ToString();
            double grosstotal = Convert.ToDouble(txt_taxable_amt.Text);

            net_total = total_gst + grosstotal;
            txt_sub_total1.Text = net_total.ToString();
            final_total();
        }
        catch (Exception gst)
        {
            throw gst;
        }

    }


    //public void gst_calculate(System.Data.DataTable dt_gst)
    //{
    //    Panel1.Visible = true;

    //    Panel1.Controls.Add(Table1);
    //    TableRow tRow = new TableRow();
    //    TableCell tCell = new TableCell();
    //    Table1.HorizontalAlign = HorizontalAlign.Right;

    //    tCell.HorizontalAlign = HorizontalAlign.Right;
    //    //tCell.Width = 1000;
    //    double gst1_total = 0;
    //    double cgst1_total = 0;
    //    double total_gst = 0;
    //    double net_total = 0;
    //    double[] gst_group = { };
    //    DataView gst_view = new DataView(dt_gst);
    //    DataTable distinct_gst = gst_view.ToTable(true, "VAT");
    //    distinct_gst.Columns.Add("total", typeof(double));
    //    c = 0;

    //    try
    //    {
    //        if (txt_year.Text == "C")
    //        {
    //            distinct_gst.Columns.Add("title_c", typeof(string));
    //            distinct_gst.Columns.Add("title_s", typeof(string));
    //            foreach (DataRow unique_gst in distinct_gst.Rows)
    //            {

    //                foreach (DataRow dr_gst in dt_gst.Rows)
    //                {
    //                    if (Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 == Convert.ToDouble(dr_gst["VAT"].ToString()) / 2)
    //                    {
    //                        cgst1_total = cgst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 200) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
    //                        total_gst = total_gst + cgst1_total * 2;
    //                        c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
    //                    }
    //                }
    //                unique_gst["total"] = cgst1_total.ToString();
    //                unique_gst["title_c"] = "CGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
    //                unique_gst["title_s"] = "SGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
    //                cgst1_total = 0;

    //                tRow = new TableRow();
    //                tCell = new TableCell();
    //                tCell.BorderWidth = 1;
    //                tCell.Text = unique_gst[2].ToString();
    //                tRow.Cells.Add(tCell);

    //                tCell = new TableCell();
    //                tCell.BorderWidth = 1;
    //                tCell.Text = unique_gst[1].ToString();
    //                tCell.HorizontalAlign = HorizontalAlign.Left;
    //                tRow.Cells.Add(tCell);

    //                Table1.Rows.Add(tRow);

    //                tRow = new TableRow();
    //                tCell = new TableCell();
    //                tCell.BorderWidth = 1;
    //                tCell.Text = unique_gst[3].ToString();
    //                tRow.Cells.Add(tCell);

    //                tCell = new TableCell();
    //                tCell.BorderWidth = 1;
    //                tCell.Text = unique_gst[1].ToString();
    //                tCell.HorizontalAlign = HorizontalAlign.Left;
    //                tRow.Cells.Add(tCell);

    //                Table1.Rows.Add(tRow);   


    //            }

    //        }
    //        else
    //        {
    //            distinct_gst.Columns.Add("title_i", typeof(string));
    //            foreach (DataRow unique_gst in distinct_gst.Rows)
    //            {

    //                foreach (DataRow dr_gst in dt_gst.Rows)
    //                {
    //                    if (Convert.ToDouble(unique_gst["VAT"].ToString()) == Convert.ToDouble(dr_gst["VAT"].ToString()))
    //                    {
    //                        gst1_total = gst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 100) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
    //                        total_gst = total_gst + gst1_total;
    //                        c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
    //                    }
    //                }
    //                unique_gst["total"] = gst1_total.ToString();
    //                unique_gst["title_i"] = "IGST @ " + unique_gst["VAT"].ToString() + " %";
    //                cgst1_total = 0;

    //                tRow = new TableRow();
    //                for (int i = 2; i >=1; i--)
    //                {
    //                    tCell = new TableCell();
    //                    tCell.BorderWidth = 1;
    //                    tCell.Text = unique_gst[i].ToString();
    //                    if(i==1)
    //                    {
    //                        tCell.HorizontalAlign = HorizontalAlign.Left;
    //                    }
    //                    tRow.Cells.Add(tCell);
    //                }
    //                Table1.Rows.Add(tRow);
    //            }
    //        }

    //        //ViewState["gstTable"] = Table1;
    //        //txt_grossamt.Text = Math.Round(c, 2).ToString();
    //        double grosstotal = Convert.ToDouble(txt_taxable_amt.Text);
    //        net_total = total_gst + grosstotal;
    //        txt_sub_total1.Text = net_total.ToString();
    //        final_total();

    //    }

    //    catch (Exception gst)
    //    {
    //        throw gst;
    //    }

    //}

    public void gst_counter(string state)
    {
        string company_state = "";
        MySqlCommand cmd_state = new MySqlCommand("select SERVICE_TAX_REG_NO from pay_company_master Where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'", d.con);
        d.con.Open();
        MySqlDataReader dr_state = cmd_state.ExecuteReader();
        while (dr_state.Read())
        {
            company_state = dr_state[0].ToString().Substring(0, 2);
        }
        dr_state.Close();
        cmd_state.Dispose();
        d.con.Close();

        if (state.Equals(company_state))
        {
            txt_year.Text = "C";
        }
        else
        {
            txt_year.Text = "I";
        }

        //  txt_year.Text = "2017";
    }
    protected void txt_discount_rate_TextChanged(object sender, EventArgs e)
    {
        double discount_percent = 0, discount_price = 0, gross_total_discount = 0, final_price = 0;

        discount_price = Convert.ToDouble(txt_discount_price.Text);
        discount_percent = Convert.ToDouble(txt_discount_rate.Text);
        gross_total_discount = Convert.ToDouble(txt_quantity.Text) * Convert.ToDouble(txt_rate.Text);
        discount_price = Math.Round(((discount_percent * gross_total_discount) / 100), 2);
        final_price = Math.Round((gross_total_discount - discount_price), 2);
        txt_discount_price.Text = Convert.ToString(discount_price);
        txt_amount.Text = final_price.ToString();
        if (Session["DISCOUNT_BY"].ToString() != "PER")
        {
            Session["DISCOUNT_BY"] = "PER";
        }
        txt_discount_rate.ToolTip = txt_discount_rate.Text;
        txt_discount_price.ToolTip = txt_discount_price.Text;
        txt_amount.ToolTip = txt_amount.Text;
    }
    protected void txt_discount_price_TextChanged(object sender, EventArgs e)
    {
        double discount_percent = 0, discount_price = 0, gross_total_discount = 0, final_price = 0;

        discount_price = Convert.ToDouble(txt_discount_price.Text);
        discount_percent = Convert.ToDouble(txt_discount_rate.Text);
        gross_total_discount = Convert.ToDouble(txt_quantity.Text) * Convert.ToDouble(txt_rate.Text);
        discount_percent = Math.Round(((discount_price * 100) / gross_total_discount), 2);
        final_price = Math.Round((gross_total_discount - discount_price), 2);
        txt_discount_rate.Text = discount_percent.ToString();
        txt_amount.Text = final_price.ToString();
        if (Session["DISCOUNT_BY"].ToString() != "RS")
        {
            Session["DISCOUNT_BY"] = "RS";
        }
        txt_discount_rate.ToolTip = txt_discount_rate.Text;
        txt_discount_price.ToolTip = txt_discount_price.Text;
        txt_amount.ToolTip = txt_amount.Text;

        //txt_discount_price.Text = "0";
        //double discount_percent = 0, discount_price = 0, gross_total_discount = 0, final_price = 0;

        //discount_price = Convert.ToDouble(txt_discount_price.Text);
        //discount_percent = Convert.ToDouble(txt_discount_rate.Text);
        //gross_total_discount = Convert.ToDouble(txt_quantity.Text) * Convert.ToDouble(txt_rate.Text);
        //discount_percent = Math.Round(((discount_price * 100) / gross_total_discount), 2);
        //final_price = Math.Round((gross_total_discount - discount_price), 2);
        //txt_discount_rate.Text = discount_percent.ToString();
        //txt_amount.Text = final_price.ToString();
        //if (Session["DISCOUNT_BY"].ToString() != "RS")
        //{
        //    Session["DISCOUNT_BY"] = "RS";
        //}
        //txt_discount_rate.ToolTip = txt_discount_rate.Text;
        //txt_discount_price.ToolTip = txt_discount_price.Text;
        //txt_amount.ToolTip = txt_amount.Text;
    }
    protected void txt_tot_discount_percent_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("item_type");

        dt.Columns.Add("Particular");
        dt.Columns.Add("size_uniform");
        dt.Columns.Add("size_shoes");

        dt.Columns.Add("DESCRIPTION");
        dt.Columns.Add("VAT");
        dt.Columns.Add("HSN_Code");
        dt.Columns.Add("Designation");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Rate");
        dt.Columns.Add("DISCOUNT");
        dt.Columns.Add("DISCOUNT_AMT");
        dt.Columns.Add("Amount");
        dt.Columns.Add("START_DATE");
        dt.Columns.Add("END_DATE");
        dt.Columns.Add("VENDOR");

        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                Label lbl_item_code = (Label)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_item_code");
                dr["ITEM_CODE"] = lbl_item_code.Text.ToString();
                Label lbl_item_type = (Label)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_item_type");
                dr["item_type"] = lbl_item_type.Text.ToString();
                TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_particular");
                dr["Particular"] = lbl_particular.Text.ToString();

                TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_uniformsize");
                dr["size_uniform"] = lbl_shoessize.Text.ToString();
                TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_shoessize");
                dr["size_shoes"] = lbl_uniformsize.Text.ToString();

                TextBox lbl_Description_final = (TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_Description_final");
                dr["DESCRIPTION"] = lbl_Description_final.Text.ToString();
                TextBox lbl_vat = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_vat");
                dr["VAT"] = lbl_vat.Text.ToString();

                TextBox lbl_hsn_number = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_hsn_code");
                dr["HSN_Code"] = lbl_hsn_number.Text.ToString();

                TextBox lbl_designation = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_designation");
                dr["Designation"] = lbl_designation.Text;
                TextBox lbl_quantity = (TextBox)gv_itemslist.Rows[rownum].Cells[8].FindControl("lbl_quantity");
                dr["Quantity"] = Convert.ToDouble(lbl_quantity.Text);
                TextBox lbl_rate = (TextBox)gv_itemslist.Rows[rownum].Cells[9].FindControl("lbl_rate");
                dr["Rate"] = Convert.ToDouble(lbl_rate.Text);
                TextBox lbl_discount = (TextBox)gv_itemslist.Rows[rownum].Cells[10].FindControl("lbl_discount");
                dr["DISCOUNT"] = Convert.ToDouble(lbl_discount.Text);
                TextBox lbl_discount_amt = (TextBox)gv_itemslist.Rows[rownum].Cells[11].FindControl("lbl_discount_amt");
                dr["DISCOUNT_AMT"] = Convert.ToDouble(lbl_discount_amt.Text);
                TextBox lbl_amount = (TextBox)gv_itemslist.Rows[rownum].Cells[12].FindControl("lbl_amount");
                dr["Amount"] = Convert.ToDouble(lbl_amount.Text);
                // TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("g"); vikas comment 22/11
                //dr["START_DATE"] = (lbl_start_date.Text);
                TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_end_date");
                dr["END_DATE"] = (lbl_end_date.Text);
                TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_vendor");
                dr["VENDOR"] = (lbl_vendor.Text);
                dt.Rows.Add(dr);

            }
        }
        discount_calculate(dt, 0);
    }
    protected void txt_tot_discount_amt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("ITEM_CODE");
            dt.Columns.Add("item_type");

            dt.Columns.Add("Particular");
            dt.Columns.Add("size_uniform");
            dt.Columns.Add("size_shoes");

            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("VAT");
            dt.Columns.Add("HSN_Code");
            dt.Columns.Add("Designation");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Rate");
            dt.Columns.Add("DISCOUNT");
            dt.Columns.Add("DISCOUNT_AMT");
            dt.Columns.Add("Amount");
            dt.Columns.Add("START_DATE");
            dt.Columns.Add("END_DATE");
            dt.Columns.Add("VENDOR");

            int rownum = 0;
            for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
            {
                if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();

                    Label lbl_item_code = (Label)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_item_code");
                    dr["ITEM_CODE"] = lbl_item_code.Text.ToString();
                    Label lbl_item_type = (Label)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_item_type");
                    dr["item_type"] = lbl_item_type.Text.ToString();
                    TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_particular");
                    dr["Particular"] = lbl_particular.Text.ToString();

                    TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_uniformsize");
                    dr["size_uniform"] = lbl_shoessize.Text.ToString();
                    TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_shoessize");
                    dr["size_shoes"] = lbl_uniformsize.Text.ToString();


                    TextBox lbl_Description_final = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_Description_final");
                    dr["DESCRIPTION"] = lbl_Description_final.Text.ToString();
                    TextBox lbl_vat = (TextBox)gv_itemslist.Rows[rownum].Cells[8].FindControl("lbl_vat");
                    dr["VAT"] = lbl_vat.Text.ToString();

                    TextBox lbl_hsn_number = (TextBox)gv_itemslist.Rows[rownum].Cells[9].FindControl("lbl_hsn_code");
                    dr["HSN_Code"] = lbl_hsn_number.Text.ToString();

                    TextBox lbl_designation = (TextBox)gv_itemslist.Rows[rownum].Cells[10].FindControl("lbl_designation");
                    dr["Designation"] = lbl_designation.Text;
                    TextBox lbl_quantity = (TextBox)gv_itemslist.Rows[rownum].Cells[11].FindControl("lbl_quantity");
                    dr["Quantity"] = Convert.ToDouble(lbl_quantity.Text);
                    TextBox lbl_rate = (TextBox)gv_itemslist.Rows[rownum].Cells[12].FindControl("lbl_rate");
                    dr["Rate"] = Convert.ToDouble(lbl_rate.Text);
                    TextBox lbl_discount = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_discount");
                    dr["DISCOUNT"] = Convert.ToDouble(lbl_discount.Text);
                    TextBox lbl_discount_amt = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_discount_amt");
                    dr["DISCOUNT_AMT"] = Convert.ToDouble(lbl_discount_amt.Text);
                    TextBox lbl_amount = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_amount");
                    dr["Amount"] = Convert.ToDouble(lbl_amount.Text);
                    //TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[16].FindControl("lbl_start_date");
                    //dr["START_DATE"] = (lbl_start_date.Text); vikas comment 22/11
                    TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[17].FindControl("lbl_end_date");
                    dr["END_DATE"] = (lbl_end_date.Text);
                    TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_vendor");
                    dr["VENDOR"] = (lbl_vendor.Text);
                    dt.Rows.Add(dr);

                }
            }
            discount_calculate(dt, 1);
        }
        catch (Exception dis_amt)
        {

        }
    }
    protected void txt_extra_chrgs_amt_TextChanged(object sender, EventArgs e)
    {
        double extra_amt = 0, extra_tax = 0, extra_tax_amt = 0, total_b = 0;
        if (txt_extra_chrgs_amt.Text == "")
        {
            extra_amt = 0;
        }
        else
        {
            extra_amt = Convert.ToDouble(txt_extra_chrgs_amt.Text.ToString());
        }
        if (txt_extra_chrgs_tax.Text == "")
        {
            extra_tax = 0;
        }
        else
        {
            extra_tax = Convert.ToDouble(txt_extra_chrgs_tax.Text.ToString());
        }
        //if (txt_extra_chrgs_tax_amt.Text == "")
        //{
        //    extra_tax_amt = 0;
        //}
        //else
        //{
        //    extra_amt = Convert.ToDouble(txt_extra_chrgs_tax_amt.Text.ToString());
        //}
        extra_tax_amt = (extra_amt * extra_tax) / 100;
        total_b = extra_amt + extra_tax_amt;
        txt_extra_chrgs_tax_amt.Text = Convert.ToString(extra_tax_amt);
        txt_sub_total2.Text = Convert.ToString(total_b);
        final_total();
    }
    protected void txt_extra_chrgs_tax_TextChanged(object sender, EventArgs e)
    {
        double extra_amt = 0, extra_tax = 0, extra_tax_amt = 0, total_b = 0;
        if (txt_extra_chrgs_amt.Text == "")
        {
            extra_amt = 0;
        }
        else
        {
            extra_amt = Convert.ToDouble(txt_extra_chrgs_amt.Text.ToString());
        }
        if (txt_extra_chrgs_tax.Text == "")
        {
            extra_tax = 0;
        }
        else
        {
            extra_tax = Convert.ToDouble(txt_extra_chrgs_tax.Text.ToString());
        }
        //if (txt_extra_chrgs_tax_amt.Text == "")
        //{
        //    extra_tax_amt = 0;
        //}
        //else
        //{
        //    extra_amt = Convert.ToDouble(txt_extra_chrgs_tax_amt.Text.ToString());
        //}
        extra_tax_amt = (extra_amt * extra_tax) / 100;
        total_b = extra_amt + extra_tax_amt;
        txt_extra_chrgs_tax_amt.Text = Convert.ToString(extra_tax_amt);
        txt_sub_total2.Text = Convert.ToString(total_b);
        Panel1.Visible = true;
        final_total();
    }

    public void final_total()
    {
        double sub_a = 0, sub_b = 0, total = 0;
        double extra_amt = 0, extra_tax = 0, extra_tax_amt = 0, total_b = 0;
        if (txt_extra_chrgs_amt.Text == "")
        {
            extra_amt = 0;
        }
        else
        {
            extra_amt = Convert.ToDouble(txt_extra_chrgs_amt.Text.ToString());
        }
        if (txt_extra_chrgs_tax.Text == "")
        {
            extra_tax = 0;
        }
        else
        {
            extra_tax = Convert.ToDouble(txt_extra_chrgs_tax.Text.ToString());
        }
        extra_tax_amt = (extra_amt * extra_tax) / 100;
        total_b = extra_amt + extra_tax_amt;
        txt_extra_chrgs_tax_amt.Text = Convert.ToString(extra_tax_amt);
        txt_sub_total2.Text = Convert.ToString(total_b);
        if (txt_sub_total2.Text.Equals(""))
        { txt_sub_total2.Text = "0"; }
        total = Convert.ToDouble(txt_sub_total1.Text.ToString()) + Convert.ToDouble(txt_sub_total2.Text.ToString());
        txt_final_total.Text = Convert.ToString(total);
    }
    //public void final_total()
    //{
    //    double sub_a = 0, sub_b = 0, total = 0;
    //    total = Convert.ToDouble(txt_sub_total1.Text.ToString()) + Convert.ToDouble(txt_sub_total2.Text.ToString());
    //    txt_final_total.Text = Convert.ToString(total);
    //}

    protected void btn_searchvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            MySqlCommand cmd2 = new MySqlCommand("select EMP_MOBILE_NO from pay_employee_master where Emp_Name='" + ddl_sales_person.Text + "'", d.con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txtsalesmobileno.Text = dr2[0].ToString();

            }
        }
        catch
        { }

    }

    public void attached_doc()
    {

        try
        {
            string discount_counter = "";
            ReportDocument crystalReport = new ReportDocument();
            string compcd = Session["COMP_CODE"].ToString();
            DataTable dt = new DataTable();
            if (txt_docno.Text != "")
            {
                // string query = "    SELECT  PAY_COMPANY_MASTER.COMP_CODE, PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO, PAY_COMPANY_MASTER.ECC_CODE_NO,  PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, PAY_TRANSACTIONQ.DOC_NO, PAY_TRANSACTIONQ.DOC_DATE,PAY_TRANSACTIONQ.CUST_NAME, PAY_TRANSACTIONQ.CUST_CODE, PAY_TRANSACTIONQ.REF_NO1, PAY_TRANSACTIONQ.REF_NO2, PAY_TRANSACTIONQ.NARRATION, PAY_TRANSACTIONQ.BILL_MONTH, PAY_TRANSACTIONQ.GROSS_AMOUNT, PAY_TRANSACTIONQ.SER_PER_REC, PAY_TRANSACTIONQ.SER_TAXABLE_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_CESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_HCESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_REC_TOT, PAY_TRANSACTIONQ.SER_PER_PRO, PAY_TRANSACTIONQ.SER_TAXABLE_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_CESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_HCESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_PRO_TOT, PAY_TRANSACTIONQ.NET_TOTAL, PAY_TRANSACTIONQ.DEDUCTION, PAY_TRANSACTIONQ.TOTAL, PAY_TRANSACTIONQ.BILL_YEAR, PAY_TRANSACTIONQ_DETAILS.SR_NO, PAY_TRANSACTIONQ_DETAILS.PARTICULAR, PAY_TRANSACTIONQ_DETAILS.DESIGNATION, PAY_TRANSACTIONQ_DETAILS.QUANTITY, PAY_TRANSACTIONQ_DETAILS.RATE, PAY_TRANSACTIONQ_DETAILS.AMOUNT, PAY_CUSTOMER_MASTER.CUST_NAME, PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE, PAY_CUSTOMER_MASTER.CITY, PAY_CUSTOMER_MASTER.PIN, PAY_CUSTOMER_MASTER.CONTACT_PERSON,PAY_TRANSACTIONQ.customer_gst_no,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO_AMT,PAY_TRANSACTIONQ_DETAILS.hsn_code,PAY_TRANSACTIONQ_DETAILS.sac_code FROM  PAY_TRANSACTIONQ INNER JOIN  PAY_TRANSACTIONQ_DETAILS ON PAY_TRANSACTIONQ.COMP_CODE = PAY_TRANSACTIONQ_DETAILS.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE = PAY_TRANSACTIONQ_DETAILS.TASK_CODE AND PAY_TRANSACTIONQ.DOC_NO = PAY_TRANSACTIONQ_DETAILS.DOC_NO INNER JOIN PAY_CUSTOMER_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_CUSTOMER_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.CUST_CODE = PAY_CUSTOMER_MASTER.CUST_ID INNER JOIN PAY_COMPANY_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_COMPANY_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE='INV' AND  PAY_TRANSACTIONQ.DOC_NO >='" + txt_docno.Text + "' AND PAY_TRANSACTIONQ.DOC_NO <='" + txt_docno.Text + "'  AND PAY_TRANSACTIONQ.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  PAY_TRANSACTIONQ.DOC_NO,PAY_TRANSACTIONQ_DETAILS.SR_NO ";
                string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, pay_vendor_master.VEND_ID As CUST_ID, pay_vendor_master.VEND_NAME As CUST_NAME,pay_vendor_master.VEND_ADD1 As CUST_ADD1, pay_vendor_master.VEND_ADD2 As CUST_ADD2  , pay_vendor_master.STATE AS Expr1, pay_vendor_master.CITY AS Expr2, (select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transactionp.DOC_NO , DATE_FORMAT(pay_transactionp.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transactionp.NARRATION,pay_transactionp.BILL_MONTH,pay_transactionp.DISCOUNT,pay_transactionp.DISCOUNTED_PRICE,pay_transactionp.TAXABLE_AMT,pay_transactionp.NET_TOTAL,pay_transactionp.EXTRA_CHRGS,pay_transactionp.EXTRA_CHRGS_AMT,pay_transactionp.EXTRA_CHRGS_TAX,pay_transactionp.EXTRA_CHRGS_TAX_AMT,pay_transactionp.FINAL_PRICE,pay_transactionp.BILL_YEAR,pay_transactionp.customer_gst_no,DATE_FORMAT(pay_transactionp.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transactionp.BILL_ADDRESS,pay_transactionp.SHIPPING_ADDRESS,pay_transactionp.SALES_PERSON,pay_transactionp.CUSTOMER_NOTES,pay_transactionp.TERMS_CONDITIONS,pay_transactionp.p_o_no,pay_transactionp.TRANSPORT,pay_transactionp.FREIGHT,pay_transactionp.VEHICLE_NO,pay_transactionp_DETAILS.SR_NO,pay_transactionp_DETAILS.PARTICULAR,pay_transactionp_DETAILS.DESCRIPTION,pay_transactionp_DETAILS.VAT,pay_transactionp_DETAILS.hsn_number,pay_transactionp_DETAILS.DESIGNATION,pay_transactionp_DETAILS.QUANTITY,pay_transactionp_DETAILS.RATE,pay_transactionp_DETAILS.DISCOUNT,pay_transactionp_DETAILS.DISCOUNT_AMT,pay_transactionp_DETAILS.AMOUNT FROM pay_vendor_master, PAY_COMPANY_MASTER, pay_transactionp_DETAILS, pay_transactionp WHERE pay_vendor_master.COMP_CODE = pay_transactionp.COMP_CODE AND pay_vendor_master.VEND_ID = pay_transactionp.CUST_CODE AND pay_vendor_master.COMP_CODE = pay_transactionp.COMP_CODE AND pay_transactionp_DETAILS.COMP_CODE = pay_transactionp.COMP_CODE AND pay_transactionp_DETAILS.TASK_CODE = pay_transactionp.TASK_CODE AND pay_transactionp_DETAILS.DOC_NO = pay_transactionp.DOC_NO AND pay_transactionp_details.TASK_CODE='STP'   AND pay_transactionp_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transactionp_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transactionp_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionp_details.DOC_NO,pay_transactionp_details.SR_NO";
                MySqlCommand cmd = new MySqlCommand(query, d.con1);
                MySqlDataReader sda = null;
                d.con1.Open();
                try
                {
                    sda = cmd.ExecuteReader();
                    dt.Load(sda);

                    foreach (DataRow dr_dis_col in dt.Rows)
                    {
                        if (Convert.ToDouble(dr_dis_col["DISCOUNT1"].ToString()) > 0)
                        {
                            discount_counter = "discount";
                        }
                    }
                }
                catch (Exception ex) { throw ex; }

                if (txt_year.Text == "C")
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoice_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoice.rpt"));
                    }
                }
                else
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoiceI_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_PInvoiceI.rpt"));
                    }

                }
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                //CrystalReportViewer1.ReportSource = crystalReport;
                string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/."), "html\\");
                path = path.Replace("\\", "\\\\");
                string proposal_path = path;
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = path + lbl_print_quote.Text + ".pdf";
                Session["DOC_NO"] = lbl_print_quote.Text + ".pdf";

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
                ///crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "Quotation");

            }
        }

        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            lbl_print_quote.Text = "";
            d.con1.Close();
        }
    }
    private void update_listbox(DropDownList txt_designation, string unit)
    {
        //listarray1 = listarray1.Replace(",", ""); 
        List<string> tagIds = unit.Split(',').ToList();
        //txt_designation.ClearSelection();
        txt_designation.Items.Clear();
        foreach (var item in tagIds)
        {
            // listbox1.SelectedValue = item;
            txt_designation.Items.Add(item.ToString());
        }

        txt_designation.Items.Insert(0, new ListItem("Select Unit", ""));
        txt_designation.ToolTip = txt_designation.SelectedItem.ToString();
    }

    protected void unit_per_price_changes(object sender, EventArgs e)
    {

        string unit_name = txt_designation.SelectedItem.ToString();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT item_pieces FROM item_unit_master where item_unit_name='" + txt_designation.SelectedValue.ToString() + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_per_unit.Text = dr_item1.GetValue(0).ToString();

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }

    public void unit_select()
    {

        txt_designation.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT item_unit_name FROM item_unit_master ORDER BY item_unit_name", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                // txt_designation.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

        // txt_particular.Items.Insert(0, "Select");customer_details
        txt_designation.Items.Insert(0, new ListItem("Select Unit", ""));


    }

    protected void fill_txt_particular(object sender, EventArgs e)
    {
        //----- Item List ------------------

        //if (ddl_product.SelectedValue != "Uniform" || ddl_product.SelectedValue != "pantry_jacket" || ddl_product.SelectedValue != "Apron")
        //{
        //    unitform.Visible = false;
        //}
        //else
        //{
        //    unitform.Visible = true;
        //}



        txt_particular.Items.Clear();
       
        MySqlCommand cmd_item = new MySqlCommand("SELECT PARTICULAR , item_code  from pay_transaction_po_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and item_type='" + ddl_product.SelectedValue + "'  and po_no='" + ddl_po_num.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            int i = 0;
            MySqlDataReader dr_item = cmd_item.ExecuteReader();
            while (dr_item.Read())
            {
                txt_particular.Items.Insert(i++, new ListItem(dr_item[0].ToString(), dr_item[1].ToString()));
            }
            dr_item.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            txt_particular.Items.Insert(0, new ListItem("Select", "0"));

        }
        size();
        lbl_qty.Visible = false;
        txt_quantity1.Visible = false;
        lbl_raten.Visible = false;
        lbl_rete1.Visible = false;
        txt_description.Text = "";
        txt_hsn.Text = "";
        txt_desc.Text = "";
        //txt_designation.text = "Select";
        txt_per_unit.Text = "0";
        txt_quantity.Text = "0";
        txt_rate.Text = "0";
        txt_discount_rate.Text = "0";
        txt_discount_price.Text = "0";
        txt_amount.Text = "0";

    }

    protected void size()
    {
        if (ddl_product.SelectedValue == "Uniform")
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT distinct(`size`) FROM `pay_item_master` WHERE `product_service` = 'Uniform' and `COMP_CODE`='" + Session["COMP_CODE"].ToString() + "'", d.con);
               d.con.Open();
               MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_uniformsize.DataSource = dt;
                    ddl_uniformsize.DataTextField = dt.Columns[0].ToString();
                    ddl_uniformsize.DataBind();
                  ddl_uniformsize.Items.Insert(0, "Select");
                }
                 d.con.Close();
            }
            catch (Exception ex)
            { }
            finally { d.con.Close(); }
        }
        else if (ddl_product.SelectedValue == "pantry_jacket")
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT distinct(`size`) FROM `pay_item_master` WHERE `product_service` = 'pantry_jacket' and `COMP_CODE`='" + Session["COMP_CODE"].ToString() + "'", d.con);
                d.con.Open();
                MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_pantry_size.DataSource = dt;
                    ddl_pantry_size.DataTextField = dt.Columns[0].ToString();
                    ddl_pantry_size.DataBind();
                    ddl_pantry_size.Items.Insert(0, "Select");
                }
                d.con.Close();
            }
            catch (Exception ex)
            { }
            finally {  d.con.Close();}
        }
        else if (ddl_product.SelectedValue == "Shoes")
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT distinct(`size`) FROM `pay_item_master` WHERE `product_service` = 'Shoes' and `COMP_CODE`='" + Session["COMP_CODE"].ToString() + "'", d.con);
                d.con.Open();
                MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_shoosesize.DataSource = dt;
                    ddl_shoosesize.DataTextField = dt.Columns[0].ToString();
                    ddl_shoosesize.DataBind();
                    ddl_shoosesize.Items.Insert(0, "Select");
                }
                d.con.Close();
            }
            catch (Exception ex)
            { }
            finally { d.con.Close();}
        }
        else if (ddl_product.SelectedValue == "Apron")
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT distinct(`size`) FROM `pay_item_master` WHERE `product_service` = 'Apron' and `COMP_CODE`='" + Session["COMP_CODE"].ToString() + "'", d.con);
                d.con.Open();
                MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddl_apron_size1.DataSource = dt;
                    ddl_apron_size1.DataTextField = dt.Columns[0].ToString();
                    ddl_apron_size1.DataBind();
                    ddl_apron_size1.Items.Insert(0, "Select");
                }
                d.con.Close();
            }
            catch (Exception ex)
            { }
            finally { d.con.Close(); }
        }
    }

    protected void gv_bynumber_name_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_bynumber_name.UseAccessibleHeader = false;
            gv_bynumber_name.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void lnk_button_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        Panel6.Visible = true;
        Panel4.Visible = true;
        Panel8.Visible = true;
        btn_Save.Visible = false;
        btn_final.Visible = true;


        gv_itemslist.Visible = true;
        btn_update.Visible = true;
        btn_delete.Visible = true;
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string lbl_doccode = row.Cells[0].Text;
 
        string doc_no = lbl_doccode;
        lbl_print_quote.Text = doc_no;

        // MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),vendor_type,CUST_NAME,NARRATION,REF_NO2,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,GROSS_AMOUNT,BILL_YEAR,BILL_MONTH,Category  FROM pay_transactionp WHERE (DOC_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d.con);
        //vikas23/11
        MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),vendor_type,CUST_CODE,NARRATION,REF_NO2,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,GROSS_AMOUNT,BILL_YEAR,BILL_MONTH,Category, BANK_AC_NUM , BANK_AC_NAME , IFC_CODE , CREDIT_PERIOD , TRANSPORT ,pur_order_no,company_shiping_name,company_shiping_address,company_shiping_gst_no,company_shiping_state,vendor_invoice_no FROM pay_transactionp WHERE (DOC_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", ac.con);

        ac.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_docno.Text = dr2[0].ToString();
                txt_docdate.Text = dr2[1].ToString();
                ddl_vendortype.SelectedValue = dr2[2].ToString();
                ddl_vendortype_SelectedIndexchange(null, null);
                //vikas23/11
                txt_bank_ac.Text = dr2[31].ToString();
                txt_bank_no.Text = dr2[30].ToString();
                txt_ifc_code.Text = dr2[32].ToString();
                txt_credit_perod.Text = dr2[33].ToString();
                ddl_transport.SelectedValue = dr2[34].ToString();
                if (ddl_vendortype.SelectedValue == "Regular")
                {
                    ddl_customerlist.SelectedValue = dr2[3].ToString();
                }
                else { txt_vendorname.Text = dr2[3].ToString(); }

                customer_details(null, null);
                ddl_po_num.SelectedValue = dr2[35].ToString();
                txt_narration.Text = dr2[4].ToString();
                txt_customer_gst.Text = dr2["customer_gst_no"].ToString();
                txt_expiry_date.Text = dr2["EXPIRY_DATE"].ToString();
                txt_bill_add.Text = dr2["BILL_ADDRESS"].ToString();
                txt_ship_add.Text = dr2["SHIPPING_ADDRESS"].ToString();
                ddl_sales_person.Text = dr2["SALES_PERSON"].ToString();
                txt_referenceno2.Text = dr2["REF_NO2"].ToString();
                txt_tot_discount_percent.Text = dr2["DISCOUNT"].ToString();
                txt_tot_discount_amt.Text = dr2["DISCOUNTED_PRICE"].ToString();
                txt_taxable_amt.Text = dr2["TAXABLE_AMT"].ToString();
                txt_sub_total1.Text = dr2["NET_TOTAL"].ToString();
                txt_extra_chrgs.Text = dr2["EXTRA_CHRGS"].ToString();
                txt_extra_chrgs_amt.Text = dr2["EXTRA_CHRGS_AMT"].ToString();
                txt_extra_chrgs_tax.Text = dr2["EXTRA_CHRGS_TAX"].ToString();
                txt_extra_chrgs_tax_amt.Text = dr2["EXTRA_CHRGS_TAX_AMT"].ToString();
                txt_sub_total2.Text = Convert.ToString(double.Parse(txt_extra_chrgs_amt.Text) + double.Parse(txt_extra_chrgs_tax_amt.Text));
                txt_final_total.Text = dr2["FINAL_PRICE"].ToString();
                txt_customer_notes.Text = dr2["CUSTOMER_NOTES"].ToString();
                txt_terms_conditions.Text = dr2["TERMS_CONDITIONS"].ToString();
                // ,p_o_no,FREIGHT,VEHICLE_NO
                txt_p_o_no.Text = dr2["p_o_no"].ToString();
                txt_freight.Text = dr2["FREIGHT"].ToString();
                txt_vehicle.Text = dr2["VEHICLE_NO"].ToString();
                txtsalesmobileno.Text = dr2["SALES_MOBILE_NO"].ToString();

                txt_grossamt.Text = dr2["GROSS_AMOUNT"].ToString();
                txt_year.Text = dr2["BILL_YEAR"].ToString();
                ddlcalmonth.Text = dr2["BILL_MONTH"].ToString();

                ddlcategory.Text = dr2["Category"].ToString();
                txt_ship.Text = dr2["company_shiping_name"].ToString();
                txt_ship_address.Text = dr2["company_shiping_address"].ToString();
                txt_ship_gst_no.Text = dr2["company_shiping_gst_no"].ToString();
                if(dr2["company_shiping_state"].ToString()!="")
                {
                ddl_shiping_state.SelectedValue = dr2["company_shiping_state"].ToString();
                }
                 txt_vendor_no.Text =dr2["vendor_invoice_no"].ToString();
            }
            dr2.Close();
            ac.con.Close();
            gst_counter(txt_customer_gst.Text.Substring(0, 2));
            ddl_po_num_SelectedIndexChanged(null, null);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ac.con.Close();

        }



        MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,item_type,PARTICULAR,size_uniform,size_shoes,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,DATE_FORMAT(START_DATE,'%d/%m/%Y') As START_DATE,DATE_FORMAT(END_DATE,'%d/%m/%Y') As END_DATE,VENDOR,`size_pantry`,size_apron FROM pay_transactionp_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                gv_itemslist.DataSource = null;
                gv_itemslist.DataBind();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt;
                }
                gv_itemslist.DataSource = dt;
                gv_itemslist.DataBind();
                // discount_calculate(dt, 1);
                gst_calculate(dt);
                Panel2.Visible = true;
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            Panel2.Visible = true;
        }
        attached_doc();
        lbl_print_quote.Text = txt_docno.Text;
        gv_bynumber_name.Visible = true;

        // txt_year.Text = "2017";
    }
    protected void gv_bynumber_name_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;
    }

    protected void ddl_po_num_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_product.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct item_type FROM `pay_transaction_po_details` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `po_no` = '" + ddl_po_num.SelectedValue + "'", d.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_product.DataSource = dt_item;
                ddl_product.DataTextField = dt_item.Columns[0].ToString();
                ddl_product.DataValueField = dt_item.Columns[0].ToString();
                ddl_product.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_product.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        { throw ex; }
        finally { d.con.Close(); }

        ddl_invoice_no.Items.Clear();
        System.Data.DataTable dt_item1 = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT `DOC_NO` FROM `pay_transactionp`  WHERE  comp_code  = '" + Session["comp_code"].ToString() + "' AND  pur_order_no  = '" + ddl_po_num.SelectedValue + "'", d1.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
        d1.con.Open();
        try
        {
            cmd_item1.Fill(dt_item1);
            if (dt_item1.Rows.Count > 0)
            {
                ddl_invoice_no.DataSource = dt_item1;
                ddl_invoice_no.DataTextField = dt_item1.Columns[0].ToString();
                ddl_invoice_no.DataValueField = dt_item1.Columns[0].ToString();
                ddl_invoice_no.DataBind();

            }
            dt_item1.Dispose();

            d1.con.Close();
            ddl_invoice_no.Items.Insert(0, "Select");
        }
        catch (Exception ex)
        { throw ex; }
        finally { d1.con.Close(); }

        string po_value = d.getsinglestring("SELECT ( GRAND_TOTAL ) - IFNULL(SUM( FINAL_PRICE ), 0) FROM  pay_transaction_po    LEFT JOIN  pay_transactionp  ON  pay_transaction_po . po_no  = pay_transactionp. pur_order_no   AND  pay_transaction_po . comp_code  = pay_transactionp. comp_code  WHERE  pay_transaction_po. comp_code  = '" + Session["comp_code"].ToString() + "' AND  po_no  = '" + ddl_po_num.SelectedValue + "'");
        txt_balance_op.Text = po_value;
    }

    protected Boolean budget()
    {
        string po_value = d.getsinglestring("select grand_total from pay_transaction_po  where comp_code='" + Session["comp_code"].ToString() + "' and po_no='" + ddl_po_num.SelectedValue + "'");
        string final_txt = "0";
        string pur_invoive = d.getsinglestring("SELECT sum(FINAL_PRICE) FROM `pay_transactionp` WHERE `comp_code` = '"+Session["comp_code"].ToString()+"' AND `pur_order_no` = '" + ddl_po_num.SelectedValue + "'and month = '" + txt_docdate.Text.Substring(3, 2) + "' and year = '" + txt_docdate.Text.Substring(6) + "'");
        if (pur_invoive == "")
        {
            pur_invoive = "0";
        }
        if (update_flag==1)
        {
         final_txt = txt_final_total.Text;
        }
       double invoive = (Convert.ToDouble(pur_invoive)) + (Convert.ToDouble(final_txt));
        if (po_value=="")
        {
            po_value = "0";
        }

        if ((Convert.ToDouble(po_value)) < (Convert.ToDouble(invoive)))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Purchase invoice value Not Greater than " + po_value + " (PO value)  ...');", true);
            return true;
        }
     return false;
    }
    //vikas for report
    protected void report_vendor()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT VEND_ID,CONCAT(VEND_ID,' - ',VEND_NAME)  from PAY_VENDOR_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and ACTIVE_STATUS='A' ORDER BY VEND_ID", d1.con);
        d1.con.Open();
        try
        {
            cmd_item1.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_report_vendor.DataSource = dt_item;
                ddl_report_vendor.DataTextField = dt_item.Columns[1].ToString();
                ddl_report_vendor.DataValueField = dt_item.Columns[0].ToString();
                ddl_report_vendor.DataBind();
            }
            dt_item.Dispose();
            d1.con.Close();
            ddl_report_vendor.Items.Insert(0, "Select");
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            ddl_report_vendor.Items.Insert(0, new ListItem("Select"));
        }

    }
    protected void btn_report_Click(object sender, EventArgs e)
    {
        string sql = "";
        string where = "";
        if (ddl_report_vendor_po.SelectedValue !="ALL")
        {
            where = " and  pur_order_no ='" + ddl_report_vendor_po.SelectedValue+ "'";
        }
        if (ddl_invoice_report.SelectedValue!="ALL")
        {
            where = where + " and  DOC_NO ='" + ddl_invoice_report.SelectedValue + "'";
        }
        sql = "SELECT  pay_transactionp . CUST_CODE , pay_transactionp . CUST_NAME , pur_order_no , GRAND_TOTAL  AS 'po_amount', DOC_NO , TAXABLE_AMT as 'FINAL_PRICE' ,( NET_TOTAL  -  TAXABLE_AMT ) AS 'gst', NET_TOTAL,CONCAT( pay_transactionp . month , '/',  pay_transactionp . year ) AS 'month'   FROM  pay_transactionp  INNER JOIN  pay_transaction_po  ON  pay_transactionp . pur_order_no  =  pay_transaction_po . PO_NO  WHERE  pay_transactionp . comp_code  = '" + Session["comp_code"].ToString() + "' AND  pay_transactionp . cust_code  = '" + ddl_report_vendor.SelectedValue + "' AND  DOC_DATE  BETWEEN STR_TO_DATE('01/" + txt_report_month.Text + "', '%d/%m/%Y') AND STR_TO_DATE('31/" + txt_report_month1.Text + "', '%d/%m/%Y') " + where + " ";
        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=INVOICE_REPORT" + ddl_report_vendor.SelectedItem.Text.Replace(" ", "_") + ".xls");

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
    protected void ddl_report_vendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        ddl_report_vendor_po.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  distinct pur_order_no  FROM  pay_transactionp  WHERE  CUST_CODE  = '" + ddl_report_vendor.SelectedValue + "' AND  DOC_DATE  BETWEEN STR_TO_DATE('01/" + txt_report_month.Text + "', '%d/%m/%Y') AND STR_TO_DATE('31/" + txt_report_month1.Text + "', '%d/%m/%Y') and  COMP_CODE ='" + Session["comp_code"].ToString() + "' ", d3.con);
        d3.con.Open();

        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_report_vendor_po.DataSource = dt_item;
                ddl_report_vendor_po.DataTextField = dt_item.Columns[0].ToString();
                ddl_report_vendor_po.DataValueField = dt_item.Columns[0].ToString();
                ddl_report_vendor_po.DataBind();
            }
            dt_item.Dispose();

            d3.con.Close();
            ddl_report_vendor_po.Items.Insert(0, "ALL");
            ddl_invoice_report.Items.Insert(0, "ALL");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            //d.con.Close();
            d3.con.Close();
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
            //paid_days = 0;
            //rate = 0;
        }

        public void InstantiateIn(Control container)
        {


            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr ><th colspan=10>PURCHASE INVOICE BILL DETAILS</th></tr><tr><th>SR NO.</th><th>VENDOR CODE</th><th>VENDOR NAME</th><th>MONTH_YEAR</th> <th>PO NO</th><th>INVOICE NO</th><th>PO AMOUNT</th><th>INVOICE AMOUNT</th><th>GST</th><th>Total</th></tr> ");


                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CUST_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["CUST_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["month"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pur_order_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DOC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["po_amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["FINAL_PRICE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["NET_TOTAL"] + "</td></tr>");

                    if (ds.Tables[0].Rows.Count == ctr + 1)
                    {
                        lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>=SUM(H3:H" + (ctr + 3) + ")</td><td>=SUM(I3:I" + (ctr + 3) + ")</td><td>=SUM(J3:J" + (ctr + 3) + ")</td>";
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
    protected void ddl_report_vendor_po_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_invoice_report.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select  DOC_NO  from pay_transactionp  where  CUST_CODE ='" + ddl_report_vendor.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' and  pur_order_no ='" + ddl_report_vendor_po.SelectedValue+ "'", d3.con);
        d3.con.Open();

        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_invoice_report.DataSource = dt_item;
                ddl_invoice_report.DataTextField = dt_item.Columns[0].ToString();
                ddl_invoice_report.DataValueField = dt_item.Columns[0].ToString();
                ddl_invoice_report.DataBind();
            }
            dt_item.Dispose();

            d3.con.Close();
            //ddl_invoice_report.Items.Insert(0, "Select");
            ddl_invoice_report.Items.Insert(0, "ALL");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            //d.con.Close();
            d3.con.Close();
        }
    }
    protected void state()
    {
        try
        {
            ddl_shiping_state.Items.Clear();
            d.con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter dr = new MySqlDataAdapter("select  distinct STATE_NAME from pay_state_master", d.con);
            dr.Fill(dt);
            if(dt.Rows.Count>0)
            {
                ddl_shiping_state.DataSource = dt;
                ddl_shiping_state.DataTextField=dt.Columns[0].ToString();
                ddl_shiping_state.DataValueField=dt.Columns[0].ToString();
                ddl_shiping_state.DataBind();
            }
            dt.Dispose();
            d.con.Close();
            ddl_shiping_state.Items.Insert(0,"Select");
        }
        catch (Exception ex)
        {
            d.con.Close();
        }
        finally
        {
        }
    }
    protected void btn_final_Click(object sender, EventArgs e)
    {
         int  result = 0;
         result = d.operation("update pay_transactionp set final_invoice_flag = 1 WHERE  DOC_NO = '" + txt_docno.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "' ");
         vendor_CRN();
        btn_Print_Click(null,null);

    }
    protected void vendor_CRN()
    {
        string Sql = "select DOC_NO, month,year,3,0,@i:=@i+1 from pay_transactionp where comp_code='" + Session["COMP_CODE"].ToString() + "' and month='" + txt_docdate.Text.Substring(3, 2) + "' and year='" + txt_docdate.Text.Substring(6) + "' and CUST_CODE='" + ddl_customerlist.SelectedValue + "' and DOC_NO not in (select emp_code from pay_emp_paypro where month='" + txt_docdate.Text.Substring(3, 2) + "' and year='" + txt_docdate.Text.Substring(6) + "' and  type = '3' AND `emp_code` IS NOT NULL)";
        d.operation("set @i:= (select ifnull(max(pay_pro_no),21000) from pay_emp_paypro where pay_pro_no < 98000);insert into pay_emp_paypro(emp_code,month,year,type,status,pay_pro_no)" + Sql);
    }
}