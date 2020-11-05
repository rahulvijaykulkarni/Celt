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


public partial class Transaction_New : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d_sum = new DAL();
    DAL d_profit = new DAL();
    TransactionBAL tbl = new TransactionBAL();
    double a = 0, b = 0, c = 0;
    System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
    protected void Page_Load(object sender, EventArgs e)

    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
       
      
        //if (ViewState["gstTable"] != null)
        //{
        //    Table1 = (System.Web.UI.WebControls.Table)ViewState["gstTable"];
        //}
        if (!IsPostBack)
        {
            btn_save_send.Visible = false;
            txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txt_start_date.Text = txt_docdate.Text;

            Session["DISCOUNT_BY"] = "RS";
            //----- Item List ------------------
            txt_particular.Items.Clear();
            MySqlCommand cmd_item = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY ITEM_NAME", d.con);
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
            sales_person_list();

            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;

            gendocno();
           
            txt_docno.Enabled = true;
          //  ddl_customerlist.Enabled = true;
            txt_docdate.Enabled = true;
            txt_narration.Enabled = true;
            gv_itemslist.Visible = false;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            btn_save_send.Visible = false;
            //Session["DOC_NO"] = "";
            //text_clear();
            txt_docno.ReadOnly = true;
            ddlcalmonth.SelectedIndex = DateTime.Now.Month - 1;

            ddl_vendor.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT VEND_NAME , VEND_ID  from pay_vendor_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
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
            txt_docno.ReadOnly = true;
            tooltrip();
        }

    }

    private void gendocno()
    {
        MySqlCommand cmd1 = new MySqlCommand("Select COUNT(*) from PAY_TRANSACTION  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND TASK_CODE='INV'", d.con);
        d.con.Open();
        int total_count = 0;
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                string row = dr1[0].ToString();
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
                string docno = "I" + "000" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 10 && total_count <= 99)
            {
                string docno = "I" + "00" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 100 && total_count <= 999)
            {
                string docno = "I" + "0" + total_count;
                txt_docno.Text = docno.ToString();
            }
            if (total_count >= 1000 && total_count <= 9999)
            {
                string docno = "I" + total_count.ToString();
                txt_docno.Text = docno.ToString();
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        tooltrip();
    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        //if (txt_description.Text == "")
        //{
        //    txt_description.Text = "0";
        //}

        gv_itemslist.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("Particular");
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
                TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_particular");
                dr["Particular"] = lbl_particular.Text.ToString();
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
                TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_start_date");
                dr["START_DATE"] = (lbl_start_date.Text);
                TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_end_date");
                dr["END_DATE"] = (lbl_end_date.Text);
                TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_vendor");
                dr["VENDOR"] = (lbl_vendor.Text);
                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["ITEM_CODE"] = txt_particular.SelectedValue.ToString();
        dr["Particular"] = txt_particular.SelectedItem.ToString();
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
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        discount_calculate(dt , 1);
        //gst_calculate(dt);
        ViewState["CurrentTable"] = dt;
        txt_rate.Text = "0";
        txt_quantity.Text = "0";
        txt_amount.Text = "";
       // txt_designation.Text = "";
     txt_particular.SelectedIndex = 0;
        txt_description.Text = "0";
        txt_hsn.Text = "";
        txt_igst.Text = "";
        txt_description.Text = "0";
       txt_discount_price.Text = "0";
        txt_particular.Focus();
        txt_desc.Text = "";
       // txt_tot_discount_percent.Text = "0";
        Panel4.Visible = true;
        Panel2.Visible = true;
      //  txt_extra_chrgs_tax.Text = "0";
        txt_rate.Text = "0";
        txt_discount_rate.Text = "0";
        txt_designation.SelectedIndex = 0;
        txt_per_unit.Text = "0";
        ddl_vendor.SelectedIndex = 0;
        txt_end_date.Text = "";
       // lbl_qty.Visible = false;
        tooltrip();

        lbl_qty.Text = "";
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int invoice_result =0;
        int item_result = 0;
        int TotalRows = gv_itemslist.Rows.Count;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select DOC_NO from PAY_TRANSACTION  where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and TASK_CODE='INV'", d.con);
            d.con.Open();
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

                if (txt_extra_chrgs_tax.Text == "")
                {
                    txt_extra_chrgs_tax.Text = "0";
                }
                final_total();
                string final = txt_final_total.Text;
                //d.operation("INSERT INTO PAY_TRANSACTIONQ(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL, DEDUCTION, TOTAL,OUTSTANDING,BILL_YEAR,customer_gst_no,IGST_TAX_PER_PRO,IGST_TAX_PER_PRO_AMT,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + cust_code.ToString() + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_narration.Text + "','" + ddlcalmonth.Text + "','" + txt_grossamt.Text + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble(txt_ser_tax_per_pro.Text) + "," + Convert.ToDouble(txt_ser_tax_pro_tot_1.Text) + "," + Convert.ToDouble(txt_bharat_education.Text) + "," + Convert.ToDouble(txt_swachh_bharat.Text) + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_net_total_1.Text + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_year.Text + "','" + txt_customer_gst.Text + "','" + Convert.ToDouble(txt_igst.Text) + "','" + Convert.ToDouble(txt_igst_ugst.Text) + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'"+txt_bill_add.Text+"','"+txt_ship_add.Text+"','"+ddl_sales_person.SelectedItem.ToString()+"','"+txt_customer_notes.Text+"','"+txt_terms_conditions.Text+"')");
                 invoice_result = d.operation("INSERT INTO PAY_TRANSACTION(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,BILL_ADDRESS,SALES_PERSON,NARRATION,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_YEAR,CUST_MOBILE_NO,REF_NO1) values('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_customer_name.Text + "','" + txt_customer_gst.Text + "','" + txt_address.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txt_sales_mobile_no.Text + "','" + txt_year.Text + "','" + txt_mobile_no.Text +  "','NEW')");
                tooltrip();
                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    Label lbl_srnumber = (Label)row.FindControl("lbl_srnumber");
                    int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                    Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                    string item_code = (lbl_item_code.Text);
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
                    TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                    string start_date = lbl_start_date.Text.ToString();
                    TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                    string end_date = lbl_end_date.Text.ToString();
                    TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                    string vendor = lbl_vendor.Text.ToString();

                    string query = "INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "'";
                    int insert_result = d.operation("INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "')");
                    if (insert_result > 0)
                    {
                        try
                        {
                            item_result = item_result + insert_result;
                            string sum_inward = (" select sum(QUANTITY) from pay_transaction_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                            MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                            d_sum.con1.Open();
                            MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                            if (dr_sum_inward.Read())
                            {
                                double doubleoutword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                                int updaterecord = d.operation("update pay_item_master SET outward ='" + doubleoutword + "' where Item_Code='" + item_code + "'");
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
                        d.con.Close();
                        try
                        {

                            string query1 = (" select sum(pay_transaction_details.RATE),sum(pay_transactionp_details.RATE) from pay_transaction_details,pay_transactionp_details  where pay_transaction_details.Item_Code = pay_transactionp_details.Item_Code AND pay_transaction_details.COMP_CODE = '" + Session["COMP_CODE"] + "' ");

                            MySqlCommand cmd1 = new MySqlCommand(query1, d.con);
                            d.con.Open();
                            MySqlDataReader sda = cmd1.ExecuteReader();

                            if (sda.Read())
                            {
                                double Sales_rate = Convert.ToDouble(sda.GetValue(0).ToString());
                                double Purchase_rate = Convert.ToDouble(sda.GetValue(1).ToString());

                                double sum_profit = Sales_rate - Purchase_rate;
                                double sum_loss = Purchase_rate - Sales_rate;

                                if (sum_profit > 0)
                                {
                                  //  int insert = d.operation(" Insert into pay_profit_loss (PURCHASE_RATE,SALES_RATE,Profit,COMP_CODE,ITEM_CODE,loss) value('" + Purchase_rate + "','" + Sales_rate + "','" + sum_profit + "','" + Session["COMP_CODE"] + "','" + item_code + "','0')");
                                    int updaterecord = d.operation("update pay_profit_loss SET PURCHASE_RATE ='" + Purchase_rate + "',SALES_RATE='" + Sales_rate + "',Profit='" + sum_profit + "',loss ='0'  where Item_Code='" + item_code + "'");
                                }
                                else 
                                {
                                  //  int insert = d.operation(" Insert into pay_profit_loss (PURCHASE_RATE,SALES_RATE,Profit,COMP_CODE,ITEM_CODE,loss) value('" + Purchase_rate + "','" + Sales_rate + "','0','" + Session["COMP_CODE"] + "','" + item_code + "','" + sum_loss + "')");
                                    int updaterecord = d.operation("update pay_profit_loss SET PURCHASE_RATE ='" + Purchase_rate + "',SALES_RATE='" + Sales_rate + "',Profit='0',loss='" + sum_loss + "'  where Item_Code='" + item_code + "'");
                                }
                              
                              

                            }
                            d.con.Close();                          
                        }

                        catch (Exception ex) { throw ex; }
                        finally { }
                    }
                    insert_result = 0;
                }
                    if (invoice_result > 0 )
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice (" + txt_docno.Text + ") added successfully!')", true);
                      
                        attached_doc();
                        lbl_print_quote.Text = txt_docno.Text;
                       
                    }
                    else
                    {
                        d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                        d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                    }
                
                dr.Close();

            }
        }
        catch (Exception ee)
        {
            d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {
            d.con.Close();
            text_clear();
            gendocno();
            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            tooltrip();
        }
    }
    


    protected void btn_save_send_mail(object sender, EventArgs e) {

        btn_Save_Click(sender, e);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email Send successfully!')", true);
    
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        lbl_print_quote.Text = "";
        Session["DOC_NO"] = "";
        Response.Redirect("Sales.aspx");
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
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

                        int delete_item = d.operation("Delete from pay_transaction_details Where Item_Code = '" + item_code + "' AND DOC_NO = '" + txt_docno.Text + "'  and COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                        string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_outward from pay_transaction_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                        MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                        d_sum.con1.Open();
                        MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                        if (dr_sum_inward.Read())
                        {
                            double doubleoutword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                            int updaterecord = d.operation("update pay_item_master SET outward ='" + doubleoutword + "' where Item_Code='" + item_code + "'");
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

                            int updaterecord = d.operation("update pay_item_master set inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code = '" + item_code + "' ");
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
            System.Web.UI.WebControls.Label lbl_docnumber = (System.Web.UI.WebControls.Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
            string l_docnumber = lbl_docnumber.Text;
            string discount_counter = "";
            ReportDocument crystalReport = new ReportDocument();
            string compcd = Session["COMP_CODE"].ToString();
            DataTable dt = new DataTable();
            if (txt_docno.Text != "")
            {
                // string query = "    SELECT  PAY_COMPANY_MASTER.COMP_CODE, PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO, PAY_COMPANY_MASTER.ECC_CODE_NO,  PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, PAY_TRANSACTIONQ.DOC_NO, PAY_TRANSACTIONQ.DOC_DATE,PAY_TRANSACTIONQ.CUST_NAME, PAY_TRANSACTIONQ.CUST_CODE, PAY_TRANSACTIONQ.REF_NO1, PAY_TRANSACTIONQ.REF_NO2, PAY_TRANSACTIONQ.NARRATION, PAY_TRANSACTIONQ.BILL_MONTH, PAY_TRANSACTIONQ.GROSS_AMOUNT, PAY_TRANSACTIONQ.SER_PER_REC, PAY_TRANSACTIONQ.SER_TAXABLE_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC, PAY_TRANSACTIONQ.SER_TAX_PER_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_CESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_REC, PAY_TRANSACTIONQ.SER_TAX_HCESS_REC_AMT, PAY_TRANSACTIONQ.SER_TAX_REC_TOT, PAY_TRANSACTIONQ.SER_PER_PRO, PAY_TRANSACTIONQ.SER_TAXABLE_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_PER_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_CESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_CESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_HCESS_PER_PRO, PAY_TRANSACTIONQ.SER_TAX_HCESS_PRO_AMT, PAY_TRANSACTIONQ.SER_TAX_PRO_TOT, PAY_TRANSACTIONQ.NET_TOTAL, PAY_TRANSACTIONQ.DEDUCTION, PAY_TRANSACTIONQ.TOTAL, PAY_TRANSACTIONQ.BILL_YEAR, PAY_TRANSACTIONQ_DETAILS.SR_NO, PAY_TRANSACTIONQ_DETAILS.PARTICULAR, PAY_TRANSACTIONQ_DETAILS.DESIGNATION, PAY_TRANSACTIONQ_DETAILS.QUANTITY, PAY_TRANSACTIONQ_DETAILS.RATE, PAY_TRANSACTIONQ_DETAILS.AMOUNT, PAY_CUSTOMER_MASTER.CUST_NAME, PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE, PAY_CUSTOMER_MASTER.CITY, PAY_CUSTOMER_MASTER.PIN, PAY_CUSTOMER_MASTER.CONTACT_PERSON,PAY_TRANSACTIONQ.customer_gst_no,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO,PAY_TRANSACTIONQ.IGST_TAX_PER_PRO_AMT,PAY_TRANSACTIONQ_DETAILS.hsn_code,PAY_TRANSACTIONQ_DETAILS.sac_code FROM  PAY_TRANSACTIONQ INNER JOIN  PAY_TRANSACTIONQ_DETAILS ON PAY_TRANSACTIONQ.COMP_CODE = PAY_TRANSACTIONQ_DETAILS.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE = PAY_TRANSACTIONQ_DETAILS.TASK_CODE AND PAY_TRANSACTIONQ.DOC_NO = PAY_TRANSACTIONQ_DETAILS.DOC_NO INNER JOIN PAY_CUSTOMER_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_CUSTOMER_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.CUST_CODE = PAY_CUSTOMER_MASTER.CUST_ID INNER JOIN PAY_COMPANY_MASTER ON PAY_TRANSACTIONQ.COMP_CODE = PAY_COMPANY_MASTER.COMP_CODE AND PAY_TRANSACTIONQ.TASK_CODE='INV' AND  PAY_TRANSACTIONQ.DOC_NO >='" + txt_docno.Text + "' AND PAY_TRANSACTIONQ.DOC_NO <='" + txt_docno.Text + "'  AND PAY_TRANSACTIONQ.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  PAY_TRANSACTIONQ.DOC_NO,PAY_TRANSACTIONQ_DETAILS.SR_NO ";
                string query = "SELECT `PAY_COMPANY_MASTER`.`COMPANY_NAME`, `PAY_COMPANY_MASTER`.`ADDRESS1`, `PAY_COMPANY_MASTER`.`ADDRESS2`, `PAY_COMPANY_MASTER`.`CITY`, `PAY_COMPANY_MASTER`.`STATE`, `PAY_COMPANY_MASTER`.`PIN`, `PAY_COMPANY_MASTER`.`PF_REG_NO`, `PAY_COMPANY_MASTER`.`ESIC_REG_NO`, `PAY_COMPANY_MASTER`.`COMPANY_PAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_TAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CIN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CONTACT_NO`, `PAY_COMPANY_MASTER`.`ECC_CODE_NO`, `PAY_COMPANY_MASTER`.`COMPANY_WEBSITE`, `PAY_COMPANY_MASTER`.`SERVICE_TAX_REG_NO`, `PAY_TRANSACTION`.`DOC_NO`,`PAY_TRANSACTION`.`CUST_NAME`, DATE_FORMAT(`PAY_TRANSACTION`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', `PAY_TRANSACTION`.`NARRATION`, `PAY_TRANSACTION`.`BILL_MONTH`, `PAY_TRANSACTION`.`DISCOUNT`, `PAY_TRANSACTION`.`DISCOUNTED_PRICE`, `PAY_TRANSACTION`.`TAXABLE_AMT`, `PAY_TRANSACTION`.`NET_TOTAL`, `PAY_TRANSACTION`.`EXTRA_CHRGS`, `PAY_TRANSACTION`.`EXTRA_CHRGS_AMT`, `PAY_TRANSACTION`.`EXTRA_CHRGS_TAX`, `PAY_TRANSACTION`.`EXTRA_CHRGS_TAX_AMT`, `PAY_TRANSACTION`.`FINAL_PRICE`, `PAY_TRANSACTION`.`BILL_YEAR`, `PAY_TRANSACTION`.`customer_gst_no`, DATE_FORMAT(`PAY_TRANSACTION`.`EXPIRY_DATE`, '%d/%m/%Y') AS 'EXPIRY_DATE', `PAY_TRANSACTION`.`BILL_ADDRESS`, `PAY_TRANSACTION`.`SHIPPING_ADDRESS`, `PAY_TRANSACTION`.`SALES_PERSON`, `PAY_TRANSACTION`.`CUSTOMER_NOTES`, `PAY_TRANSACTION`.`TERMS_CONDITIONS`, `PAY_TRANSACTION`.`p_o_no`, `PAY_TRANSACTION`.`TRANSPORT`, `PAY_TRANSACTION`.`FREIGHT`, `PAY_TRANSACTION`.`VEHICLE_NO`, `PAY_TRANSACTION_DETAILS`.`SR_NO`, `PAY_TRANSACTION_DETAILS`.`PARTICULAR`, `PAY_TRANSACTION_DETAILS`.`DESCRIPTION`, `PAY_TRANSACTION_DETAILS`.`VAT`, `PAY_TRANSACTION_DETAILS`.`hsn_number`, `PAY_TRANSACTION_DETAILS`.`DESIGNATION`, `PAY_TRANSACTION_DETAILS`.`QUANTITY`, `PAY_TRANSACTION_DETAILS`.`RATE`, `PAY_TRANSACTION_DETAILS`.`DISCOUNT`, `PAY_TRANSACTION_DETAILS`.`DISCOUNT_AMT`, `PAY_TRANSACTION_DETAILS`.`AMOUNT`, `PAY_TRANSACTION`.`SALES_MOBILE_NO` FROM `PAY_COMPANY_MASTER`, `PAY_TRANSACTION_DETAILS`, `PAY_TRANSACTION` WHERE `PAY_COMPANY_MASTER`.`COMP_CODE` = `PAY_TRANSACTION`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = `PAY_TRANSACTION`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`TASK_CODE` = `PAY_TRANSACTION`.`TASK_CODE` AND `PAY_TRANSACTION_DETAILS`.`DOC_NO` = `PAY_TRANSACTION`.`DOC_NO` AND `pay_transaction_details`.`TASK_CODE` = 'INV' AND `pay_transaction_details`.`DOC_NO` = '" + l_docnumber + "' AND `pay_transaction_details`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' ORDER BY `pay_transaction_details`.`DOC_NO`, `pay_transaction_details`.`SR_NO`";
                MySqlCommand cmd = new MySqlCommand(query, d.con);
                MySqlDataReader sda = null;
                d.con.Open();
                try
                {
                    sda = cmd.ExecuteReader();
                    dt.Load(sda);

                    foreach(DataRow dr_dis_col in dt.Rows)
                    {
                        if(Convert.ToDouble(dr_dis_col["DISCOUNT1"].ToString()) > 0)
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
                        crystalReport.Load(Server.MapPath("~/Rpt_Invoice_Discount_New.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_Invoice_New.rpt"));
                    }
                }
                else
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_InvoiceI_Discount_New.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_InvoiceI_New.rpt"));
                    }

                }
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "TaxInvoice");
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con.Close();
            lbl_print_quote.Text = "";
           
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
        txt_vehicle.Text = "";
        txt_discount_rate.Text = "0";
        txt_discount_price.Text = "0";
        txt_customer_name.Text = "";
        txt_docdate.Text = DateTime.Now.ToString("d/M/yyyy");
        txt_taxable_amt.Text = "0";
        txt_sub_total1.Text = "0";
        txt_extra_chrgs_amt.Text ="0";
        txt_sub_total2.Text = "0";
        txt_final_total.Text = "0";
        txt_extra_chrgs.Text = "";
        txt_extra_chrgs_tax_amt.Text="0";
        //txt_designation.Text = "";
        txt_quantity.Text = "";
        txt_extra_chrgs_tax.Text = "0";
        txt_rate.Text = "";
        txt_amount.Text = "";
        txt_narration.Text = "";
        txt_grossamt.Text = "";
        txt_net_total_1.Text = "";
        txt_expiry_date.Text = "";
        txt_address.Text = "";
        txt_mobile_no.Text = "";
        //txt_referenceno2.Text = "";
        txt_customer_notes.Text = "";
        txt_terms_conditions.Text = "";
        txt_customer_gst.Text = "";
        ddl_sales_person.Text = "";
        txt_sales_mobile_no.Text = "";
        txt_particular.SelectedIndex = 0;
        txt_desc.Text = "";
        txt_description.Text = "0";
        txt_hsn.Text = "";
        txt_particular.SelectedIndex = 0;
        ddl_sales_person.Text = "";
        txt_discount_price.Text = "0";
        txt_discount_rate.Text = "0";
        txt_p_o_no.Text = "";
        txt_start_date.Text = txt_docdate.Text;
        txt_end_date.Text = "";
        ddl_vendor.SelectedIndex = 0;
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        gv_itemslist.Visible = false;
        Panel4.Visible = false;
        //txt_taxable_amt.Text = "0";
        //txt_sub_total1.Text = "0";
        //txt_extra_chrgs.Text = "";
        //txt_extra_chrgs_amt.Text = "0";
        //txt_extra_chrgs_tax.Text = "0";
        //txt_extra_chrgs_tax_amt.Text = "0";
        //txt_sub_total2.Text = "";
        //txt_final_total.Text = "0";
        txt_sales_mobile_no.Text = "";

        //Panel1.Visible = false;
        //d.operation("truncate table '" + Table1 + "'");
        tooltrip();
    }
    protected void txt_quantity_TextChanged(object sender, EventArgs e)
    {
        txt_discount_price.Text = "0";
        float rate = float.Parse(txt_rate.Text);
        float quantity = float.Parse(txt_quantity.Text);
         float available_qty = float.Parse(lbl_qty.Text);
        if (available_qty > quantity)
        {
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity must be Less Than  (" + lbl_qty.Text + ") ')", true);
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
      //  ddlcalmonth.ToolTip = ddlcalmonth.Text;
        txt_year.ToolTip = txt_year.Text;
        txt_referenceno1.ToolTip = txt_referenceno1.Text;
        txt_narration.ToolTip = txt_narration.Text;
        txt_p_o_no.ToolTip = txt_p_o_no.Text;
        ddl_transport.ToolTip = ddl_transport.SelectedValue.ToString();
        txt_freight.ToolTip = txt_freight.Text;
        txt_vehicle.ToolTip = txt_vehicle.Text;
        txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
        txt_designation.ToolTip = txt_designation.Text;
        txt_quantity.ToolTip = txt_quantity.Text;
        txt_rate.ToolTip = txt_rate.Text;
        ///tabel particular
        ///

        txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
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
        try
        {
            int result = 0;
            int invoice_result = 0;
            int item_result = 0;
            int TotalRows = gv_itemslist.Rows.Count;

            System.Web.UI.WebControls.Label lbl_docnumber = (System.Web.UI.WebControls.Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
            string l_docnumber = lbl_docnumber.Text;
            lbl_print_quote.Text = l_docnumber;
            d.operation("DELETE FROM PAY_TRANSACTION WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");

            d.operation("DELETE FROM PAY_TRANSACTION_DETAILS WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");
            if (txt_extra_chrgs_tax.Text == "")
            {
                txt_extra_chrgs_tax.Text = "0";
            }

            final_total();
            string final = txt_final_total.Text;
            invoice_result = d.operation("INSERT INTO PAY_TRANSACTION(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,BILL_ADDRESS,SALES_PERSON,NARRATION,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_YEAR,CUST_MOBILE_NO,REF_NO1) values('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_customer_name.Text + "','" + txt_customer_gst.Text + "','" + txt_address.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txt_sales_mobile_no.Text + "','" + txt_year.Text + "','" + txt_mobile_no.Text +  "','NEW')");

            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                Label lbl_srnumber = (Label)row.FindControl("lbl_srnumber");
                int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                string item_code = (lbl_item_code.Text);
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
                TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                string start_date = lbl_start_date.Text.ToString();
                TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                string end_date = lbl_end_date.Text.ToString();
                TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                string vendor = lbl_vendor.Text.ToString();

                string query = "INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "'";


                int insert_result = d.operation("INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "')");
                if (insert_result > 0)
                {
                    try
                    {
                        item_result = insert_result + insert_result;
                        string sum_inward = (" select sum(QUANTITY) from pay_transaction_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                        MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                        d_sum.con1.Open();
                        MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                        if (dr_sum_inward.Read())
                        {
                            double doubleoutword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                            int updaterecord = d.operation("update pay_item_master SET outward ='" + doubleoutword + "' where Item_Code='" + item_code + "'");
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
                            //doubleoutword = doubleoutword + iquantity;

                            doublestock = doubleoinword - doubleoutword;

                          //  int updaterecord = d.operation("update pay_item_master inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code='" + item_code + "'");

                            int updaterecord = d.operation("update pay_item_master set inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code = '" + item_code + "' ");
                        }
                        sda.Close();
                        d.con.Close();
                    }

                    catch { }
                }
                insert_result = 0;
                if (invoice_result > 0 )
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice (" + txt_docno.Text + ") Updated successfully!')", true);
                    Panel5.Visible = false;
                    gv_bynumber_name.Visible = false;
                     attached_doc();
                    lbl_print_quote.Text = txt_docno.Text;
                  
                }
                else
                {
                    d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                }

            }
        }

        catch (Exception ee)
        {
            d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {
            d.con.Close();
            text_clear();
            gendocno();
            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            tooltrip();
        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;

            System.Web.UI.WebControls.Label lbl_docnumber = (System.Web.UI.WebControls.Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
            string l_docnumber = lbl_docnumber.Text;

            d.operation("DELETE FROM PAY_TRANSACTION WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                string item_code = (lbl_item_code.Text);
                TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                float quantity = float.Parse(txt_quantity.Text);

                d.operation("DELETE FROM PAY_TRANSACTION_DETAILS WHERE Item_Code = '" + item_code + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");
                string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_outward from pay_transaction_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                MySqlCommand cmd_sum = new MySqlCommand(sum_inward, d_sum.con1);
                d_sum.con1.Open();
                MySqlDataReader dr_sum_inward = cmd_sum.ExecuteReader();
                if (dr_sum_inward.Read())
                {
                    double doubleoutword = Convert.ToDouble(dr_sum_inward.GetValue(0).ToString());
                    int updaterecord = d.operation("update pay_item_master SET outward ='" + doubleoutword + "' where Item_Code='" + item_code + "'");
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

                    int updaterecord = d.operation("update pay_item_master set inward='" + doubleoinword + "' ,Stock= '" + doublestock + "'  where Item_Code = '" + item_code + "' ");
                }
                sda.Close();
                d.con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + txt_customer_name.Text + " Deleted Successfully');", true);
            //   Panel1.Visible = false;
            Panel5.Visible = false;
            gv_bynumber_name.Visible = false;
            Panel8.Visible = true;
            Panel6.Visible = true;
            Panel5.Visible = false;

        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally {
            text_clear();
            gendocno();
            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            tooltrip();
        }

    }

    protected void btn_searchvendor_Click(object sender, EventArgs e)
    {
       // hide.Visible = false;
            if (txt_docno_number.Text != "")
            {
                MySqlCommand cmd_docno = new MySqlCommand("SELECT  PAY_TRANSACTION.DOC_NO,date_format(PAY_TRANSACTION.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM PAY_TRANSACTION WHERE (PAY_TRANSACTION.DOC_NO LIKE '%" + txt_docno_number.Text + "%' AND REF_NO1='NEW' AND PAY_TRANSACTION.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY PAY_TRANSACTION.DOC_DATE DESC ", d.con);
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
                MySqlCommand cmd_customername = new MySqlCommand("SELECT PAY_TRANSACTION.CUST_NAME,PAY_TRANSACTION.DOC_NO,date_format(PAY_TRANSACTION.DOC_DATE,'%d/%m/%Y') as DOC_DATE,NET_TOTAL FROM PAY_TRANSACTION WHERE (PAY_TRANSACTION.CUST_NAME LIKE '%" + txt_customername.Text + "%' AND REF_NO1='NEW' AND PAY_TRANSACTION.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY PAY_TRANSACTION.DOC_DATE DESC", d.con);
                d.con.Open();
                try
                {
                    MySqlDataReader dr_customername = cmd_customername.ExecuteReader();
                    if (dr_customername.HasRows)
                    {
                        Panel5.Visible = true;
                        gv_bynumber_name.Visible = true;
                        gv_bynumber_name.DataSource = dr_customername;
                        gv_bynumber_name.DataBind();
                    }
                    else
                    {
                        gv_bynumber_name.Visible = false;
                        Panel5.Visible = false;

                    }
                    dr_customername.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con.Close();
                    tooltrip();
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

    protected void gv_bynumber_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_save_send.Visible = false;
        Panel6.Visible = true;
        Panel4.Visible = true;
        Panel8.Visible = true;
        btn_Save.Visible = false;
        
        gv_itemslist.Visible = true;
        btn_update.Visible = true;
        btn_delete.Visible = true;

        Label lbl_doccode = (Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
        string doc_no = lbl_doccode.Text;
        lbl_print_quote.Text = doc_no;

        MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),CUST_NAME,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO , SALES_MOBILE_NO,BILL_YEAR,BILL_MONTH,Category,CUST_MOBILE_NO FROM PAY_TRANSACTION WHERE (DOC_NO = '" + doc_no + "' AND REF_NO1='NEW' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_docno.Text = dr2[0].ToString();
                txt_docdate.Text = dr2[1].ToString();
                txt_customer_name.Text = dr2[2].ToString();
                //get_client_contact(ddl_customerlist.SelectedValue.ToString());
                txt_narration.Text = dr2[3].ToString();
                txt_customer_gst.Text = dr2["customer_gst_no"].ToString();
                txt_expiry_date.Text = dr2["EXPIRY_DATE"].ToString();
                txt_address.Text = dr2["BILL_ADDRESS"].ToString();
                //txt_ship_add.Text = dr2["SHIPPING_ADDRESS"].ToString();
                ddl_sales_person.Text = dr2["SALES_PERSON"].ToString();
                //txt_referenceno2.Text = dr2["REF_NO2"].ToString();
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
                txt_p_o_no.Text = dr2["p_o_no"].ToString();
                ddl_transport.SelectedValue = dr2["TRANSPORT"].ToString();
                txt_freight.Text = dr2["FREIGHT"].ToString();
                txt_vehicle.Text = dr2["VEHICLE_NO"].ToString();
                txt_sales_mobile_no.Text = dr2["SALES_MOBILE_NO"].ToString();
                txt_grossamt.Text = dr2["GROSS_AMOUNT"].ToString();
                txt_year.Text = dr2["BILL_YEAR"].ToString();
                ddlcalmonth.Text = dr2["BILL_MONTH"].ToString();
                txt_mobile_no.Text = dr2["CUST_MOBILE_NO"].ToString();
                ddlcategory.Text = dr2["Category"].ToString();
            }
            dr2.Close();
            d.con.Close();
            gst_counter(txt_customer_gst.Text.Substring(0,2));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            tooltrip();
        }

        
       // MySqlCommand cmd1 = new MySqlCommand("SELECT PARTICULAR,DESCRIPTION,hsn_code,sac_code, DESIGNATION, ROUND(QUANTITY,6) AS QUANTITY, ROUND(RATE,6) AS RATE, ROUND(AMOUNT,2) AS AMOUNT FROM PAY_TRANSACTIONQ_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);

        MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,DATE_FORMAT(START_DATE,'%d/%m/%Y') As START_DATE,DATE_FORMAT(END_DATE,'%d/%m/%Y') As END_DATE,VENDOR FROM PAY_TRANSACTION_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
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
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            tooltrip();
        }
        attached_doc();
        lbl_print_quote.Text = txt_docno.Text;
        gv_bynumber_name.Visible = true;
    }

    protected void txt_docno_TextChanged(object sender, EventArgs e)
    {
        string doc_no = txt_docno.Text;
        if (doc_no != "")
        {
            txt_customername.Text = "";
        }
    }
    
    protected void particular_hsn_sac_code(object sender, EventArgs e)
    {
       txt_designation.Visible = true;
        if (txt_particular.SelectedItem.ToString() != "Select")
        {
            MySqlCommand cmd = new MySqlCommand("Select item_description,SALES_RATE,unit,VAT,case When hsn_number <> '' then hsn_number else sac_number END as hsn_sac_no , case When hsn_number <> '' then 'H' else 'S' END as hsn , unit_per_piece,Stock from pay_item_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ITEM_NAME = '" + txt_particular.SelectedItem.Text.ToString() + "'", d.con);
            //MySqlCommand cmd = new MySqlCommand("select hsn_number,sac_number,unit,sales_rate,item_description from pay_item_master where ITEM_NAME='" + txt_particular.SelectedItem.ToString() + "'", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item = cmd.ExecuteReader();
                while (dr_item.Read())
                {
                    txt_desc.Text = dr_item.GetValue(0).ToString();
                    txt_rate.Text = dr_item.GetValue(1).ToString();
                   // txt_designation.SelectedValue = dr_item.GetValue(2).ToString();
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
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                txt_hsn.Enabled=false;
               // txt_designation.Enabled = false;
               // txt_rate.Enabled = false;
               // txt_description.Enabled = false;
                tooltrip();
            }

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

    //protected void update_listbox(ListBox listbox1, string listarray1)
    //{

    //    //listarray1 = listarray1.Replace(",", ""); 
    //    List<string> tagIds = listarray1.Split(',').ToList();
    //    listbox1.ClearSelection();

    //    //foreach (var item in tagIds)
    //    //{
    //    //    listbox1.SelectedValue = item;
    //    //}

    //    for (int i = 0; i < listbox1.Items.Count; i++)
    //    {
    //        foreach (var item in tagIds)
    //        {
    //            if (item.Equals(listbox1.Items[i].Value))
    //            {
    //                listbox1.Items[i].Selected = true;
    //            }
    //        }
    //    }

    //    //for (int i = 0; i <= listarray1.Length - 1; i++)
    //    //{
    //    //    string selectvalue = listarray1.Substring(i, 1);
    //    //    //listbox1.Items[int.Parse(listarray1.Substring(i, 1))].Selected = true;

    //    //    listbox1.SelectedValue = selectvalue;

    //    //}


    //}

    

    public void get_client_contact(string client_name)
    {

        MySqlCommand cmd = new MySqlCommand("SELECT `GST`, CONCAT_WS('\n', `Bills_attention`, `Bill_Address`, `Bill_City`, `Bill_State`, `Bill_Country`, `Bill_Zipcode`) AS 'Bill_Address', CONCAT_WS('\n', `Shipping_attention`, `Shipping_Address`, `Shipplng_city`, `Shipping_state`, `Shipping_country`, `Shipping_Zipcode`) AS 'Shipping_Address', CONCAT(`pay_contact_person`.`first_name`, ' ', `pay_contact_person`.`last_name`,'-',pay_contact_person.Mobile_no) AS 'contact_person' FROM `pay_customer_master` INNER JOIN `pay_contact_person` ON `pay_customer_master`.`comp_code` = `pay_contact_person`.`comp_code` AND `pay_customer_master`.`cust_id` = `pay_contact_person`.`cust_id` WHERE `pay_customer_master`.`cust_id` ='" + client_name.Substring(0, 4) + "'", d.con1);
        d.con1.Open();
        try
        {
            MySqlDataReader dr_item = cmd.ExecuteReader();
            while (dr_item.Read())
            {
                txt_customer_gst.Text = dr_item.GetValue(0).ToString();
                txt_address.Text = dr_item.GetValue(1).ToString();
                //txt_ship_add.Text = dr_item.GetValue(2).ToString();
                //ddl_contact_person.SelectedValue = dr_item.GetValue(3).ToString();
                //ddl_contact_person.Items.Add(dr_item[3].ToString());

            }
            dr_item.Close();
            cmd.Dispose();
            d.con1.Close();
            gst_counter(txt_customer_gst.Text.Substring(0, 2));
            //ddl_contact_person.Items.Insert(0, new ListItem("Select person", "0"));
          
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            tooltrip();
            txt_customer_gst.ReadOnly = true;
        }
      
    }

    public void sales_person_list()
    {

        //ddl_sales_person.Items.Clear();
        //MySqlCommand cmd_item = new MySqlCommand("SELECT EMP_NAME from PAY_EMPLOYEE_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'    ORDER BY EMP_NAME", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataReader dr_item = cmd_item.ExecuteReader();
        //    while (dr_item.Read())
        //    {
        //        ddl_sales_person.Items.Add(dr_item[0].ToString());
        //        ddl_sales_person.DataValueField = dr_item[0].ToString();
        //        ddl_sales_person.DataTextField = dr_item[0].ToString();
        //    }
        //    ddl_sales_person.Items.Insert(0, new ListItem("Select", "Select"));
        //    dr_item.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //    tooltrip();
        //}

        //txt_particular.Items.Insert(0, "Select");




      //  txt_particular.Items.Insert(0, "Select");
    }

    public void discount_calculate(System.Data.DataTable dt_discount , int discount_choice)
    {
        double discount_percent = 0, discount_price = 0, final_price = 0, gross_total_discount = 0, gross_total_no_discount = 0; ;
        double c = 0;
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
       txt_grossamt.Text = Convert.ToString(Math.Round(c,2));
       txt_taxable_amt.Text = Convert.ToString(final_price + gross_total_no_discount);
       gst_counter(txt_customer_gst.Text.Substring(0, 2));
       gst_calculate(dt_discount);
       tooltrip();
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
        tooltrip();
    }


    //public void gst_calculate(System.Data.DataTable dt_gst)
    //{
    //    if (txt_particular.SelectedIndex == '0')
    //    {
    //        txt_particular.Focus();
    //    }
    //    else
    //    {
    //        Panel1.Visible = true;

    //        Panel1.Controls.Add(Table1);
    //        TableRow tRow = new TableRow();
    //        TableCell tCell = new TableCell();
    //        Table1.HorizontalAlign = HorizontalAlign.Right;

    //        tCell.HorizontalAlign = HorizontalAlign.Right;
    //        //tCell.Width = 1000;
    //        double gst1_total = 0;
    //        double cgst1_total = 0;
    //        double total_gst = 0;
    //        double net_total = 0;
    //        double[] gst_group = { };
    //        DataView gst_view = new DataView(dt_gst);
    //        DataTable distinct_gst = gst_view.ToTable(true, "VAT");
    //        distinct_gst.Columns.Add("total", typeof(double));
    //        c = 0;

    //        try
    //        {
    //            if (txt_year.Text == "C")
    //            {
    //                distinct_gst.Columns.Add("title_c", typeof(string));
    //                distinct_gst.Columns.Add("title_s", typeof(string));
    //                foreach (DataRow unique_gst in distinct_gst.Rows)
    //                {

    //                    foreach (DataRow     dr_gst in dt_gst.Rows)
    //                    {
    //                        if (Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 == Convert.ToDouble(dr_gst["VAT"].ToString()) / 2)
    //                        {
    //                            cgst1_total = cgst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 200) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
    //                            total_gst = total_gst + cgst1_total * 2;
    //                            c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
    //                        }
    //                    }
    //                    unique_gst["total"] = cgst1_total.ToString();
    //                    unique_gst["title_c"] = "CGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
    //                    unique_gst["title_s"] = "SGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
    //                    cgst1_total = 0;

    //                    tRow = new TableRow();
    //                    tCell = new TableCell();
    //                    tCell.BorderWidth = 1;
    //                    tCell.Text = unique_gst[2].ToString();
    //                    tRow.Cells.Add(tCell);

    //                    tCell = new TableCell();
    //                    tCell.BorderWidth = 1;
    //                    tCell.Text = unique_gst[1].ToString();
    //                    tCell.HorizontalAlign = HorizontalAlign.Left;
    //                    tRow.Cells.Add(tCell);

    //                    Table1.Rows.Add(tRow);

    //                    tRow = new TableRow();
    //                    tCell = new TableCell();
    //                    tCell.BorderWidth = 1;
    //                    tCell.Text = unique_gst[3].ToString();
    //                    tRow.Cells.Add(tCell);

    //                    tCell = new TableCell();
    //                    tCell.BorderWidth = 1;
    //                    tCell.Text = unique_gst[1].ToString();
    //                    tCell.HorizontalAlign = HorizontalAlign.Left;
    //                    tRow.Cells.Add(tCell);

    //                    Table1.Rows.Add(tRow);


    //                }

    //            }
    //            else
    //            {
    //                distinct_gst.Columns.Add("title_i", typeof(string));
    //                foreach (DataRow unique_gst in distinct_gst.Rows)
    //                {

    //                    foreach (DataRow dr_gst in dt_gst.Rows)
    //                    {
    //                        if (Convert.ToDouble(unique_gst["VAT"].ToString()) == Convert.ToDouble(dr_gst["VAT"].ToString()))
    //                        {
    //                            gst1_total = gst1_total + (Convert.ToDouble(dr_gst["VAT"].ToString()) / 100) * (Convert.ToDouble(dr_gst["Amount"].ToString()));
    //                            total_gst = total_gst + gst1_total;
    //                            c = c + Convert.ToDouble(dr_gst["Amount"].ToString());
    //                        }
    //                    }
    //                    unique_gst["total"] = gst1_total.ToString();
    //                    unique_gst["title_i"] = "IGST @ " + unique_gst["VAT"].ToString() + " %";
    //                    cgst1_total = 0;

    //                    tRow = new TableRow();
    //                    for (int i = 2; i >= 1; i--)
    //                    {
    //                        tCell = new TableCell();
    //                        tCell.BorderWidth = 1;
    //                        tCell.Text = unique_gst[i].ToString();
    //                        if (i == 1)
    //                        {
    //                            tCell.HorizontalAlign = HorizontalAlign.Left;
    //                        }
    //                        tRow.Cells.Add(tCell);
    //                    }
    //                    Table1.Rows.Add(tRow);
    //                }
    //            }

    //            //txt_grossamt.Text = Math.Round(c, 2).ToString();
    //            double grosstotal = Convert.ToDouble(txt_taxable_amt.Text);
    //            net_total = total_gst + grosstotal;
    //            txt_sub_total1.Text = net_total.ToString();
    //            final_total();
    //            //ViewState["gstTable"] = Table1;
    //        }

    //        catch (Exception gst)
    //        {
    //            throw gst;
    //        }
    //    }
    //}

    public void gst_counter(string state)
    {
        string company_state = "";
        MySqlCommand cmd_state = new MySqlCommand("select SERVICE_TAX_REG_NO from pay_company_master Where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'", d.con1);
        d.con1.Open();
        MySqlDataReader dr_state = cmd_state.ExecuteReader();
        while (dr_state.Read())
        {
            company_state = dr_state[0].ToString().Substring(0,2);
        }
        dr_state.Close();
        cmd_state.Dispose();
        d.con1.Close();

        if (state.Equals(company_state))
        {
            txt_year.Text = "C";
        }
        else
        {
            txt_year.Text = "I";
        }
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
    }
    protected void txt_tot_discount_percent_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("Particular");
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
                TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_particular");
                dr["Particular"] = lbl_particular.Text.ToString();
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
                TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_start_date");
                dr["START_DATE"] = (lbl_start_date.Text);
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
            dt.Columns.Add("Particular");
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
                    TextBox lbl_particular = (TextBox)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_particular");
                    dr["Particular"] = lbl_particular.Text.ToString();
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
                    TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_start_date");
                    dr["START_DATE"] = (lbl_start_date.Text);
                    TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_end_date");
                    dr["END_DATE"] = (lbl_end_date.Text);
                    TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_vendor");
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
        if(txt_extra_chrgs_amt.Text == "")
        {
            extra_amt = 0;
        }
        else
        {
            extra_amt = Convert.ToDouble(txt_extra_chrgs_amt.Text.ToString());
        }
        if(txt_extra_chrgs_tax.Text =="")
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
                string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.ECC_CODE_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, PAY_CUSTOMER_MASTER.CUST_ID, PAY_CUSTOMER_MASTER.CUST_NAME,PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE AS Expr1, PAY_CUSTOMER_MASTER.CITY AS Expr2,PAY_CUSTOMER_MASTER.PIN AS Expr3,PAY_TRANSACTION.DOC_NO , DATE_FORMAT(PAY_TRANSACTION.DOC_DATE,'%d/%m/%Y') As DOC_DATE,PAY_TRANSACTION.NARRATION,PAY_TRANSACTION.BILL_MONTH,PAY_TRANSACTION.DISCOUNT,PAY_TRANSACTION.DISCOUNTED_PRICE,PAY_TRANSACTION.TAXABLE_AMT,PAY_TRANSACTION.NET_TOTAL,PAY_TRANSACTION.EXTRA_CHRGS,PAY_TRANSACTION.EXTRA_CHRGS_AMT,PAY_TRANSACTION.EXTRA_CHRGS_TAX,PAY_TRANSACTION.EXTRA_CHRGS_TAX_AMT,PAY_TRANSACTION.FINAL_PRICE,PAY_TRANSACTION.BILL_YEAR,PAY_TRANSACTION.customer_gst_no,DATE_FORMAT(PAY_TRANSACTION.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,PAY_TRANSACTION.BILL_ADDRESS,PAY_TRANSACTION.SHIPPING_ADDRESS,PAY_TRANSACTION.SALES_PERSON,PAY_TRANSACTION.CUSTOMER_NOTES,PAY_TRANSACTION.TERMS_CONDITIONS,PAY_TRANSACTION.p_o_no,PAY_TRANSACTION.TRANSPORT,PAY_TRANSACTION.FREIGHT,PAY_TRANSACTION.VEHICLE_NO,PAY_TRANSACTION_DETAILS.SR_NO,PAY_TRANSACTION_DETAILS.PARTICULAR,PAY_TRANSACTION_DETAILS.DESCRIPTION,PAY_TRANSACTION_DETAILS.VAT,PAY_TRANSACTION_DETAILS.hsn_number,PAY_TRANSACTION_DETAILS.DESIGNATION,PAY_TRANSACTION_DETAILS.QUANTITY,PAY_TRANSACTION_DETAILS.RATE,PAY_TRANSACTION_DETAILS.DISCOUNT,PAY_TRANSACTION_DETAILS.DISCOUNT_AMT,PAY_TRANSACTION_DETAILS.AMOUNT,PAY_CUSTOMER_MASTER.CUST_seniormanager,PAY_CUSTOMER_MASTER.CUST_admin , PAY_TRANSACTION.SALES_MOBILE_NO FROM PAY_CUSTOMER_MASTER, PAY_COMPANY_MASTER, PAY_TRANSACTION_DETAILS, PAY_TRANSACTION WHERE PAY_CUSTOMER_MASTER.COMP_CODE = PAY_TRANSACTION.COMP_CODE AND PAY_CUSTOMER_MASTER.CUST_ID = PAY_TRANSACTION.CUST_CODE AND PAY_COMPANY_MASTER.COMP_CODE = PAY_TRANSACTION.COMP_CODE AND PAY_TRANSACTION_DETAILS.COMP_CODE = PAY_TRANSACTION.COMP_CODE AND PAY_TRANSACTION_DETAILS.TASK_CODE = PAY_TRANSACTION.TASK_CODE AND PAY_TRANSACTION_DETAILS.DOC_NO = PAY_TRANSACTION.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
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
                        crystalReport.Load(Server.MapPath("~/Rpt_Invoice_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_Invoice.rpt"));
                    }
                }
                else
                {
                    if (discount_counter == "discount")
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_InvoiceI_Discount.rpt"));
                    }
                    else
                    {
                        crystalReport.Load(Server.MapPath("~/Rpt_InvoiceI.rpt"));
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
    //protected void ddl_sales_person_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        MySqlCommand cmd_mobile = new MySqlCommand("Select emp_mobile_no from pay_employee_master Where EMP_NAME = '" + ddl_sales_person.SelectedValue.ToString() + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'", d.con);
    //        d.con.Open();
    //        MySqlDataReader dr_mobile = cmd_mobile.ExecuteReader();
    //        while (dr_mobile.Read())
    //        {
    //            txt_sales_mobile_no.Text = dr_mobile[0].ToString();
    //        }
    //        dr_mobile.Close();
    //        d.con.Close();
    //        cmd_mobile.Dispose();
    //    }

    //    catch { }

    //    finally
    //    {
    //        d.con.Close();
    //    }
    //}

    public void unit_select() {

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

        // txt_particular.Items.Insert(0, "Select");
        txt_designation.Items.Insert(0, new ListItem("Select Unit", ""));
            
    }

    protected void unit_per_price_changes(object sender, EventArgs e) {

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
}