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


public partial class Transaction : System.Web.UI.Page
{
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    DAL client_d = new DAL();
    DAL d = new DAL();
    DAL d_sum = new DAL();
    DAL client = new DAL();
    TransactionBAL tbl = new TransactionBAL();
    ProfitLoss profitloss = new ProfitLoss();
    double a = 0, b = 0, c = 0;
    System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
    protected void Page_Load(object sender, EventArgs e)
    {
        reson.Visible = false;
        hold_materl.Visible = false;
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
            txt_comment.Enabled = true;
            client_name();//add vikas 05/04/2019
            count();//add vikas 05/04/2019
            btn_save_send.Visible = false;
            txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txt_start_date.Text = txt_docdate.Text;

            Session["DISCOUNT_BY"] = "RS";
            //----- Item List ------------------

            sales_person_list();

            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;

            gendocno();
            //ddl_emp.Enabled = false; //employee namae 
            //ddl_shoosesize.Enabled = false;// shoose size
            //ddl_uniformsize.Enabled = false;//uniform size

            txt_docno.Enabled = true;
            ddl_customerlist.Enabled = true;
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
    //vikas 05/04/2019
    protected void client_name()
    {
        ddl_client_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(pay_client_state_role_grade.`client_code`),`client_name` FROM `pay_client_master` INNER JOIN  pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_state_role_grade.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") and client_active_close='0' order by `client_code`", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_name.DataSource = dt_item;
                ddl_client_name.DataTextField = dt_item.Columns[1].ToString();
                ddl_client_name.DataValueField = dt_item.Columns[0].ToString();
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
    private void gendocno()
    {
        MySqlCommand cmd1 = new MySqlCommand("Select `DOC_NO` from pay_transaction  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND TASK_CODE='INV' ORDER BY DOC_NO DESC  limit 1", d.con);
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
        hidtab.Value = "1";
          try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
          catch { }
        gv_itemslist.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ITEM_CODE");
        dt.Columns.Add("item_type");

        dt.Columns.Add("Particular");

        dt.Columns.Add("emp_name");
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
        dt.Columns.Add("emp_code");
        dt.Columns.Add("POD_NUM");//vikas13-12
        dt.Columns.Add("size_pantry");//vikas 02-04-2019 ddl_pantry_size
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
                // ddl_employee_termination(null, null);
                TextBox lbl_emp_name = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_emp_name");
                dr["emp_name"] = lbl_emp_name.Text.ToString();
                TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_uniformsize");
                dr["size_uniform"] = lbl_uniformsize.Text.ToString();
                TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_shoessize");
                dr["size_shoes"] = lbl_shoessize.Text.ToString();

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
                TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[17].FindControl("lbl_start_date");
                dr["START_DATE"] = (lbl_start_date.Text);
                TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_end_date");
                dr["END_DATE"] = (lbl_end_date.Text);
                TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[19].FindControl("lbl_vendor");
                dr["VENDOR"] = (lbl_vendor.Text);
                TextBox lbl_empcode = (TextBox)gv_itemslist.Rows[rownum].Cells[20].FindControl("lbl_empcode");
                dr["emp_code"] = (lbl_empcode.Text);
                //vikas 13-12
                TextBox lbl_pod_num = (TextBox)gv_itemslist.Rows[rownum].Cells[20].FindControl("lbl_pod_num");
                dr["POD_NUM"] = (lbl_pod_num.Text);

                TextBox lbl_pantrysize = (TextBox)gv_itemslist.Rows[rownum].Cells[20].FindControl("lbl_pantrysize");
                dr["size_pantry"] = (lbl_pantrysize.Text);

                TextBox lbl_apronsize = (TextBox)gv_itemslist.Rows[rownum].Cells[20].FindControl("lbl_apronsize");
                dr["size_apron"] = (lbl_apronsize.Text);

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["ITEM_CODE"] = txt_particular.SelectedValue.ToString();
        dr["item_type"] = ddl_product.SelectedValue.ToString();
        dr["Particular"] = txt_particular.SelectedItem.ToString();
        //
        dr["emp_name"] = ddl_emp.SelectedItem.ToString();
        dr["size_uniform"] = ddl_uniformsize.Text;
        dr["size_shoes"] = ddl_shoosesize.Text;
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
        dr["emp_code"] = ddl_emp.SelectedValue;
        dr["POD_NUM"] = txt_pod_num.Text;//vikas 13-12
        dr["size_pantry"] = ddl_pantry_size.Text;
        dr["size_apron"] = ddl_apron.Text;
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        discount_calculate(dt, 1);
        //gst_calculate(dt);
        ViewState["CurrentTable"] = dt;
        txt_rate.Text = "0";
        ddl_product.SelectedValue = "Select";
        ddl_emp.SelectedValue = "Select";
        ddl_shoosesize.Text = "";
        ddl_uniformsize.Text = "";
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
        // discount_calculate(dt, 1);//vikas22/11
        // txt_tot_discount_percent.Text = "0";
        Panel4.Visible = true;
        Panel2.Visible = true;
        //  txt_extra_chrgs_tax.Text = "0";
        txt_rate.Text = "0";
        txt_discount_rate.Text = "0";
        txt_designation.SelectedIndex = 0;
        txt_per_unit.Text = "0";
        ddl_vendor.SelectedIndex = 0;
        // txt_end_date.Text = "";
        // lbl_qty.Visible = false;
        //   txt_quantity1.Visible = false; vikas
        tooltrip();
        lbl_qty.Text = "";
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        int invoice_result = 0;
        int item_result = 0;
        int TotalRows = gv_itemslist.Rows.Count;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); 
            MySqlCommand cmd = new MySqlCommand("select DOC_NO from pay_transaction  where DOC_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and TASK_CODE='INV'", d.con);
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
                int month = int.Parse(txt_docdate.Text.Substring(3, 2));
                int year = int.Parse(txt_docdate.Text.Substring(6));

                if (txt_extra_chrgs_tax.Text == "")
                {
                    txt_extra_chrgs_tax.Text = "0";
                }
                final_total();
                string final = txt_final_total.Text;
                //d.operation("INSERT INTO pay_transactionQ(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL, DEDUCTION, TOTAL,OUTSTANDING,BILL_YEAR,customer_gst_no,IGST_TAX_PER_PRO,IGST_TAX_PER_PRO_AMT,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + cust_code.ToString() + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_narration.Text + "','" + ddlcalmonth.Text + "','" + txt_grossamt.Text + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble(txt_ser_tax_per_pro.Text) + "," + Convert.ToDouble(txt_ser_tax_pro_tot_1.Text) + "," + Convert.ToDouble(txt_bharat_education.Text) + "," + Convert.ToDouble(txt_swachh_bharat.Text) + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_net_total_1.Text + "'," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + "," + Convert.ToDouble("0") + ",'" + txt_year.Text + "','" + txt_customer_gst.Text + "','" + Convert.ToDouble(txt_igst.Text) + "','" + Convert.ToDouble(txt_igst_ugst.Text) + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'"+txt_bill_add.Text+"','"+txt_ship_add.Text+"','"+ddl_sales_person.SelectedItem.ToString()+"','"+txt_customer_notes.Text+"','"+txt_terms_conditions.Text+"')");
                invoice_result = d.operation("INSERT into pay_transaction(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,REF_NO1,state,branch_name,MONTH,YEAR) values('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txt_sales_mobile_no.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','Existing','" + ddl_state.SelectedValue + "','" + ddlunitselect.SelectedValue + "','" + month + "','" + year + "')");
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
                    TextBox lbl_quantity = (TextBox)row.FindControl("lbl_quantity");
                    float quantity = float.Parse(lbl_quantity.Text);
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
                    Label lbl_item_type = (Label)row.FindControl("lbl_item_type");
                    string item_type = (lbl_item_type.Text);

                    TextBox lbl_uniformsize = (TextBox)row.FindControl("lbl_uniformsize");
                    string uniformsize = lbl_uniformsize.Text.ToString();
                    TextBox lbl_shoessixe = (TextBox)row.FindControl("lbl_shoessize");
                    string shoes = lbl_shoessixe.Text.ToString();

                    double sales_rate = Convert.ToDouble(rate);

                    TextBox lbl_emp_name = (TextBox)row.FindControl("lbl_emp_name");
                    string emp_name = lbl_emp_name.Text.ToString();

                    TextBox lbl_emp_code = (TextBox)row.FindControl("lbl_empcode");
                    string emp_code = lbl_emp_code.Text.ToString();

                    TextBox lbl_pod_num = (TextBox)row.FindControl("lbl_pod_num");
                    string POD_NUM = lbl_pod_num.Text.ToString();

                    TextBox lbl_pantrysize = (TextBox)row.FindControl("lbl_pantrysize");
                    string pantry_size = lbl_pantrysize.Text.ToString();

                    TextBox lbl_apronsize = (TextBox)row.FindControl("lbl_apronsize");
                    string apron_size = lbl_apronsize.Text.ToString();

                    //string query = "INSERT INTO pay_transaction_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR,item_type,size_uniform,size_shoes,emp_name,emp_code) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "','" + emp_name + "','" + emp_code + "'";
                    int insert_result = d.operation("INSERT INTO pay_transaction_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR,item_type,size_uniform,size_shoes,emp_name,emp_code,MONTH,YEAR,POD_NUM,size_pantry,size_apron) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "','" + emp_name + "','" + emp_code + "','" + month + "','" + year + "','" + POD_NUM + "','" + pantry_size + "','" + apron_size + "')");

                    d.operation("update pay_document_details set dispatch_flag='1' where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "' and `document_type`='" + item_type + "' ");
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
                            int udateprofitloss = profitloss.calculateProfitLoss("+", item_code, sales_rate, Convert.ToDouble(quantity));
                        }

                        catch { }
                        d.con.Close();

                    }
                    insert_result = 0;
                }
                if (invoice_result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invoice (" + txt_docno.Text + ") added successfully!');", true);
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice (" + txt_docno.Text + ") added successfully!')", true);

                    attached_doc();
                    lbl_print_quote.Text = txt_docno.Text;

                }
                else
                {

                    d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    profitloss.getItemCode(txt_docno.Text, Session["COMP_CODE"].ToString(), "pay_transaction_details");
                    d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                }

                dr.Close();

            }
        }
        catch (Exception ee)
        {
            d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            profitloss.getItemCode(txt_docno.Text, Session["COMP_CODE"].ToString(), "pay_transaction_details");
            d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {
            d.con.Close();
            text_clear();
            btn_btn_clear(null, null);
            gendocno();
            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            tooltrip();

        }
    }



    protected void btn_save_send_mail(object sender, EventArgs e)
    {

        btn_Save_Click(sender, e);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email Send successfully!')", true);

    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        lbl_print_quote.Text = "";
        Session["DOC_NO"] = "";
        Response.Redirect("Home.aspx");
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
        string item_code = (lbl_item_code.Text);
        TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
        float quantity = float.Parse(txt_quantity.Text);
        TextBox lbl_rate = (TextBox)row.FindControl("lbl_rate");
        float sales_rate = float.Parse(lbl_rate.Text);

        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    profitloss.calculateProfitLoss("-", item_code, Convert.ToDouble(sales_rate), Convert.ToDouble(quantity));
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

    protected void generate_report(int i)
    {
        if (i == 1)
        {
            try
            {
                string sql = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1,(SELECT `unit_add2` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_address', PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXPIRY_DATE,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,pay_transaction_DETAILS.item_type,pay_transaction_DETAILS.emp_code,pay_transaction_DETAILS.emp_name,	(SELECT `EMP_MOBILE_NO` FROM `pay_employee_master`  WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = '" + Session["comp_code"].ToString() + "') AS 'EMP_MOBILE_NO',	(SELECT `GRADE_DESC` FROM `pay_employee_master` INNER JOIN pay_grade_master ON pay_grade_master.GRADE_CODE=pay_employee_master. GRADE_CODE AND pay_grade_master.COMP_CODE=pay_employee_master.COMP_CODE WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND pay_employee_master.`comp_code` = '" + Session["comp_code"].ToString() + "') AS 'GRADE_CODE', `size_uniform` ,`size_shoes`,`size_pantry`,size_apron FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Delivery_Challan" + ddlunitselect.SelectedItem.Text.Replace(" ", "_") + ".xls");

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
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else if (i == 2)
        {

            try
            {
                //string sql = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.EXPIRY_DATE,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,pay_transaction_DETAILS.item_type,pay_transaction_DETAILS.emp_code,pay_transaction_DETAILS.emp_name,	(SELECT `EMP_MOBILE_NO` FROM `pay_employee_master`  WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',	(SELECT `GRADE_DESC` FROM `pay_employee_master` INNER JOIN pay_grade_master ON pay_grade_master.GRADE_CODE=pay_employee_master. GRADE_CODE AND pay_grade_master.COMP_CODE=pay_employee_master.COMP_CODE WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND pay_employee_master.`comp_code` = 'C01') AS 'GRADE_CODE',size_uniform,size_shoes FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='"`size_pantry` + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                string sql = "SELECT `PAY_COMPANY_MASTER`.`COMPANY_NAME`,`PAY_COMPANY_MASTER`.`ADDRESS1`,(SELECT `unit_add2` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_address',`PAY_COMPANY_MASTER`.`ADDRESS2`,`PAY_COMPANY_MASTER`.`CITY`, `PAY_COMPANY_MASTER`.`STATE`,`PAY_COMPANY_MASTER`.`PIN`,`PAY_COMPANY_MASTER`.`PF_REG_NO`,`PAY_COMPANY_MASTER`.`ESIC_REG_NO`, `PAY_COMPANY_MASTER`.`COMPANY_PAN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_TAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CIN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_CONTACT_NO`,`PAY_COMPANY_MASTER`.`COMPANY_WEBSITE`,`PAY_COMPANY_MASTER`.`SERVICE_TAX_REG_NO`,`pay_client_master`.`Client_code`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`ADDRESS1`,`pay_client_master`.`CITY` AS 'Expr1',`pay_client_master`.`STATE` AS 'Expr2',(SELECT `STATE_CODE` FROM `pay_state_master` INNER JOIN `PAY_COMPANY_MASTER` ON `pay_state_master`.`STATE_NAME` = `PAY_COMPANY_MASTER`.`STATE` LIMIT 1) AS 'Expr3',`pay_transaction`.`DOC_NO`,DATE_FORMAT(`pay_transaction`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE',`pay_transaction`.`NARRATION`,`pay_transaction`.`BILL_MONTH`,`pay_transaction`.`DISCOUNT`,`pay_transaction`.`DISCOUNTED_PRICE`,`pay_transaction`.`TAXABLE_AMT`,`pay_transaction`.`EXPIRY_DATE`,`pay_transaction`.`NET_TOTAL`,`pay_transaction`.`EXTRA_CHRGS`,`pay_transaction`.`EXTRA_CHRGS_AMT`,`pay_transaction`.`EXTRA_CHRGS_TAX`,`pay_transaction`.`EXTRA_CHRGS_TAX_AMT`,`pay_transaction`.`FINAL_PRICE`,`pay_transaction`.`BILL_YEAR`,`pay_transaction`.`customer_gst_no`,DATE_FORMAT(`pay_transaction`.`EXPIRY_DATE`, '%d/%m/%Y') AS 'EXPIRY_DATE',`pay_transaction`.`BILL_ADDRESS`, `pay_transaction`.`SHIPPING_ADDRESS`,  `pay_transaction`.`SALES_PERSON`,`pay_transaction`.`CUSTOMER_NOTES`,`pay_transaction`.`TERMS_CONDITIONS`,`pay_transaction`.`p_o_no`,`pay_transaction`.`TRANSPORT`,`pay_transaction`.`FREIGHT`, `pay_transaction`.`VEHICLE_NO`, pay_transaction.state as 'branch_state',`pay_transaction_DETAILS`.`SR_NO`,`pay_transaction_DETAILS`.`PARTICULAR`,`pay_transaction_DETAILS`.`DESCRIPTION`,`pay_transaction_DETAILS`.`VAT`,`pay_transaction_DETAILS`.`hsn_number`,`pay_transaction_DETAILS`.`DESIGNATION`,`pay_transaction_DETAILS`.`QUANTITY`,`pay_transaction_DETAILS`.`RATE`,`pay_transaction_DETAILS`.`DISCOUNT`,`pay_transaction_DETAILS`.`DISCOUNT_AMT`,`pay_transaction_DETAILS`.`AMOUNT`,`pay_transaction`.`SALES_MOBILE_NO`,`pay_transaction_DETAILS`.`item_type`,IF(instr(`pay_transaction_DETAILS`.`emp_code`,'Select'),'-',`pay_transaction_DETAILS`.`emp_code`) as 'emp_code', IF(instr(`pay_transaction_DETAILS`.`emp_name`,'Select'),'-',`pay_transaction_DETAILS`.`emp_name`) as 'emp_name', (SELECT `Unit_name` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_name', (SELECT `EMP_MOBILE_NO` FROM `pay_employee_master` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',(SELECT `GRADE_DESC` FROM   `pay_employee_master`   INNER JOIN `pay_grade_master` ON `pay_grade_master`.`GRADE_CODE` = `pay_employee_master`.`GRADE_CODE` AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `pay_employee_master`.`comp_code` = 'C01') AS 'GRADE_CODE',IF(instr(size_uniform,'Select'),'-',size_uniform) as 'size_uniform',IF(instr(size_shoes,'Select'),'-',size_shoes) as 'size_shoes',IF(instr(size_pantry,'Select'),'-',size_pantry) as 'size_pantry',IF(INSTR(`size_apron`, 'Select'), '-', `size_apron`) AS 'size_apron' FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";

                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Delivery_Challan" + ddlunitselect.SelectedItem.Text.Replace(" ", "_") + ".xls");

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
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }

        //vikas 
        else if (i == 3)
        {

            try
            {
                //string sql = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.EXPIRY_DATE,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,pay_transaction_DETAILS.item_type,pay_transaction_DETAILS.emp_code,pay_transaction_DETAILS.emp_name,	(SELECT `EMP_MOBILE_NO` FROM `pay_employee_master`  WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',	(SELECT `GRADE_DESC` FROM `pay_employee_master` INNER JOIN pay_grade_master ON pay_grade_master.GRADE_CODE=pay_employee_master. GRADE_CODE AND pay_grade_master.COMP_CODE=pay_employee_master.COMP_CODE WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND pay_employee_master.`comp_code` = 'C01') AS 'GRADE_CODE',size_uniform,size_shoes FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                string sql = "SELECT `PAY_COMPANY_MASTER`.`COMPANY_NAME`,`PAY_COMPANY_MASTER`.`ADDRESS1`,`PAY_COMPANY_MASTER`.`ADDRESS2`,(SELECT `unit_add2` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_address',`PAY_COMPANY_MASTER`.`CITY`, `PAY_COMPANY_MASTER`.`STATE`,`PAY_COMPANY_MASTER`.`PIN`,`PAY_COMPANY_MASTER`.`PF_REG_NO`,`PAY_COMPANY_MASTER`.`ESIC_REG_NO`, `PAY_COMPANY_MASTER`.`COMPANY_PAN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_TAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CIN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_CONTACT_NO`,`PAY_COMPANY_MASTER`.`COMPANY_WEBSITE`,`PAY_COMPANY_MASTER`.`SERVICE_TAX_REG_NO`,`pay_client_master`.`Client_code`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`ADDRESS1`,`pay_client_master`.`CITY` AS 'Expr1',`pay_client_master`.`STATE` AS 'Expr2',(SELECT `STATE_CODE` FROM `pay_state_master` INNER JOIN `PAY_COMPANY_MASTER` ON `pay_state_master`.`STATE_NAME` = `PAY_COMPANY_MASTER`.`STATE` LIMIT 1) AS 'Expr3',`pay_transaction`.`DOC_NO`,DATE_FORMAT(`pay_transaction`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE',`pay_transaction`.`NARRATION`,`pay_transaction`.`BILL_MONTH`,`pay_transaction`.`DISCOUNT`,`pay_transaction`.`DISCOUNTED_PRICE`,`pay_transaction`.`TAXABLE_AMT`,`pay_transaction`.`EXPIRY_DATE`,`pay_transaction`.`NET_TOTAL`,`pay_transaction`.`EXTRA_CHRGS`,`pay_transaction`.`EXTRA_CHRGS_AMT`,`pay_transaction`.`EXTRA_CHRGS_TAX`,`pay_transaction`.`EXTRA_CHRGS_TAX_AMT`,`pay_transaction`.`FINAL_PRICE`,`pay_transaction`.`BILL_YEAR`,`pay_transaction`.`customer_gst_no`,DATE_FORMAT(`pay_transaction`.`EXPIRY_DATE`, '%d/%m/%Y') AS 'EXPIRY_DATE',`pay_transaction`.`BILL_ADDRESS`, `pay_transaction`.`SHIPPING_ADDRESS`,  `pay_transaction`.`SALES_PERSON`,`pay_transaction`.`CUSTOMER_NOTES`,`pay_transaction`.`TERMS_CONDITIONS`,`pay_transaction`.`p_o_no`,`pay_transaction`.`TRANSPORT`,`pay_transaction`.`FREIGHT`, `pay_transaction`.`VEHICLE_NO`, pay_transaction.state as 'branch_state',`pay_transaction_DETAILS`.`SR_NO`,`pay_transaction_DETAILS`.`PARTICULAR`,`pay_transaction_DETAILS`.`DESCRIPTION`,`pay_transaction_DETAILS`.`VAT`,`pay_transaction_DETAILS`.`hsn_number`,`pay_transaction_DETAILS`.`DESIGNATION`,`pay_transaction_DETAILS`.`QUANTITY`,`pay_transaction_DETAILS`.`RATE`,`pay_transaction_DETAILS`.`DISCOUNT`,`pay_transaction_DETAILS`.`DISCOUNT_AMT`,`pay_transaction_DETAILS`.`AMOUNT`,`pay_transaction`.`SALES_MOBILE_NO`,`pay_transaction_DETAILS`.`item_type`,IF(instr(`pay_transaction_DETAILS`.`emp_code`,'Select'),'-',`pay_transaction_DETAILS`.`emp_code`) as 'emp_code', IF(instr(`pay_transaction_DETAILS`.`emp_name`,'Select'),'-',`pay_transaction_DETAILS`.`emp_name`) as 'emp_name', (SELECT `Unit_name` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_name', (SELECT `EMP_MOBILE_NO` FROM `pay_employee_master` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',(SELECT `GRADE_DESC` FROM   `pay_employee_master`   INNER JOIN `pay_grade_master` ON `pay_grade_master`.`GRADE_CODE` = `pay_employee_master`.`GRADE_CODE` AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `pay_employee_master`.`comp_code` = 'C01') AS 'GRADE_CODE',IF(instr(size_uniform,'Select'),'-',size_uniform) as 'size_uniform',IF(instr(size_shoes,'Select'),'-',size_shoes) as 'size_shoes',IF(INSTR(`size_pantry`, 'Select'), '-', `size_pantry`) AS 'size_pantry',IF(INSTR(`size_apron`, 'Select'), '-', `size_apron`) AS 'size_apron' FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";

                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Delivery_Challan" + ddlunitselect.SelectedItem.Text.Replace(" ", "_") + ".xls");

                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Repeater Repeater1 = new Repeater();
                    Repeater1.DataSource = ds;
                    Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds, 2);
                    Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds, 2);
                    Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, null, 2);
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
        //id card

        else if (i == 4)
        {

            try
            {
                //string sql = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.EXPIRY_DATE,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,pay_transaction_DETAILS.item_type,pay_transaction_DETAILS.emp_code,pay_transaction_DETAILS.emp_name,	(SELECT `EMP_MOBILE_NO` FROM `pay_employee_master`  WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',	(SELECT `GRADE_DESC` FROM `pay_employee_master` INNER JOIN pay_grade_master ON pay_grade_master.GRADE_CODE=pay_employee_master. GRADE_CODE AND pay_grade_master.COMP_CODE=pay_employee_master.COMP_CODE WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND pay_employee_master.`comp_code` = 'C01') AS 'GRADE_CODE',size_uniform,size_shoes FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                string sql = "SELECT `PAY_COMPANY_MASTER`.`COMPANY_NAME`,`PAY_COMPANY_MASTER`.`ADDRESS1`,`PAY_COMPANY_MASTER`.`ADDRESS2`,(SELECT `unit_add2` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_address',`PAY_COMPANY_MASTER`.`CITY`, `PAY_COMPANY_MASTER`.`STATE`,`PAY_COMPANY_MASTER`.`PIN`,`PAY_COMPANY_MASTER`.`PF_REG_NO`,`PAY_COMPANY_MASTER`.`ESIC_REG_NO`, `PAY_COMPANY_MASTER`.`COMPANY_PAN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_TAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CIN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_CONTACT_NO`,`PAY_COMPANY_MASTER`.`COMPANY_WEBSITE`,`PAY_COMPANY_MASTER`.`SERVICE_TAX_REG_NO`,`pay_client_master`.`Client_code`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`ADDRESS1`,`pay_client_master`.`CITY` AS 'Expr1',`pay_client_master`.`STATE` AS 'Expr2',(SELECT `STATE_CODE` FROM `pay_state_master` INNER JOIN `PAY_COMPANY_MASTER` ON `pay_state_master`.`STATE_NAME` = `PAY_COMPANY_MASTER`.`STATE` LIMIT 1) AS 'Expr3',`pay_transaction`.`DOC_NO`,DATE_FORMAT(`pay_transaction`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE',`pay_transaction`.`NARRATION`,`pay_transaction`.`BILL_MONTH`,`pay_transaction`.`DISCOUNT`,`pay_transaction`.`DISCOUNTED_PRICE`,`pay_transaction`.`TAXABLE_AMT`,`pay_transaction`.`EXPIRY_DATE`,`pay_transaction`.`NET_TOTAL`,`pay_transaction`.`EXTRA_CHRGS`,`pay_transaction`.`EXTRA_CHRGS_AMT`,`pay_transaction`.`EXTRA_CHRGS_TAX`,`pay_transaction`.`EXTRA_CHRGS_TAX_AMT`,`pay_transaction`.`FINAL_PRICE`,`pay_transaction`.`BILL_YEAR`,`pay_transaction`.`customer_gst_no`,DATE_FORMAT(`pay_transaction`.`EXPIRY_DATE`, '%d/%m/%Y') AS 'EXPIRY_DATE',`pay_transaction`.`BILL_ADDRESS`, `pay_transaction`.`SHIPPING_ADDRESS`,  `pay_transaction`.`SALES_PERSON`,`pay_transaction`.`CUSTOMER_NOTES`,`pay_transaction`.`TERMS_CONDITIONS`,`pay_transaction`.`p_o_no`,`pay_transaction`.`TRANSPORT`,`pay_transaction`.`FREIGHT`, `pay_transaction`.`VEHICLE_NO`, pay_transaction.state as 'branch_state',`pay_transaction_DETAILS`.`SR_NO`,`pay_transaction_DETAILS`.`PARTICULAR`,`pay_transaction_DETAILS`.`DESCRIPTION`,`pay_transaction_DETAILS`.`VAT`,`pay_transaction_DETAILS`.`hsn_number`,`pay_transaction_DETAILS`.`DESIGNATION`,`pay_transaction_DETAILS`.`QUANTITY`,`pay_transaction_DETAILS`.`RATE`,`pay_transaction_DETAILS`.`DISCOUNT`,`pay_transaction_DETAILS`.`DISCOUNT_AMT`,`pay_transaction_DETAILS`.`AMOUNT`,`pay_transaction`.`SALES_MOBILE_NO`,`pay_transaction_DETAILS`.`item_type`,IF(instr(`pay_transaction_DETAILS`.`emp_code`,'Select'),'-',`pay_transaction_DETAILS`.`emp_code`) as 'emp_code', IF(instr(`pay_transaction_DETAILS`.`emp_name`,'Select'),'-',`pay_transaction_DETAILS`.`emp_name`) as 'emp_name', (SELECT `Unit_name` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_name', (SELECT `EMP_MOBILE_NO` FROM `pay_employee_master` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',(SELECT `GRADE_DESC` FROM   `pay_employee_master`   INNER JOIN `pay_grade_master` ON `pay_grade_master`.`GRADE_CODE` = `pay_employee_master`.`GRADE_CODE` AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `pay_employee_master`.`comp_code` = 'C01') AS 'GRADE_CODE',IF(instr(size_uniform,'Select'),'-',size_uniform) as 'size_uniform',IF(instr(size_shoes,'Select'),'-',size_shoes) as 'size_shoes',IF(INSTR(`size_pantry`, 'Select'), '-', `size_pantry`) AS 'size_pantry',IF(INSTR(`size_apron`, 'Select'), '-', `size_apron`) AS 'size_apron' FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";

                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Delivery_Challan" + ddlunitselect.SelectedItem.Text.Replace(" ", "_") + ".xls");

                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Repeater Repeater1 = new Repeater();
                    Repeater1.DataSource = ds;
                    Repeater1.HeaderTemplate = new MyTemplate4(ListItemType.Header, ds, 2);
                    Repeater1.ItemTemplate = new MyTemplate4(ListItemType.Item, ds, 2);
                    Repeater1.FooterTemplate = new MyTemplate4(ListItemType.Footer, null, 2);
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

            // apron size
        else if (i == 5)
        {

            try
            {
                //string sql = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.EXPIRY_DATE,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,pay_transaction_DETAILS.item_type,pay_transaction_DETAILS.emp_code,pay_transaction_DETAILS.emp_name,	(SELECT `EMP_MOBILE_NO` FROM `pay_employee_master`  WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',	(SELECT `GRADE_DESC` FROM `pay_employee_master` INNER JOIN pay_grade_master ON pay_grade_master.GRADE_CODE=pay_employee_master. GRADE_CODE AND pay_grade_master.COMP_CODE=pay_employee_master.COMP_CODE WHERE  `emp_code` = `pay_transaction_details`.`emp_code` AND pay_employee_master.`comp_code` = 'C01') AS 'GRADE_CODE',size_uniform,size_shoes FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                string sql = "SELECT `PAY_COMPANY_MASTER`.`COMPANY_NAME`,`PAY_COMPANY_MASTER`.`ADDRESS1`,`PAY_COMPANY_MASTER`.`ADDRESS2`,(SELECT `unit_add2` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_address',`PAY_COMPANY_MASTER`.`CITY`, `PAY_COMPANY_MASTER`.`STATE`,`PAY_COMPANY_MASTER`.`PIN`,`PAY_COMPANY_MASTER`.`PF_REG_NO`,`PAY_COMPANY_MASTER`.`ESIC_REG_NO`, `PAY_COMPANY_MASTER`.`COMPANY_PAN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_TAN_NO`, `PAY_COMPANY_MASTER`.`COMPANY_CIN_NO`,`PAY_COMPANY_MASTER`.`COMPANY_CONTACT_NO`,`PAY_COMPANY_MASTER`.`COMPANY_WEBSITE`,`PAY_COMPANY_MASTER`.`SERVICE_TAX_REG_NO`,`pay_client_master`.`Client_code`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`CLIENT_NAME`,`pay_client_master`.`ADDRESS1`,`pay_client_master`.`CITY` AS 'Expr1',`pay_client_master`.`STATE` AS 'Expr2',(SELECT `STATE_CODE` FROM `pay_state_master` INNER JOIN `PAY_COMPANY_MASTER` ON `pay_state_master`.`STATE_NAME` = `PAY_COMPANY_MASTER`.`STATE` LIMIT 1) AS 'Expr3',`pay_transaction`.`DOC_NO`,DATE_FORMAT(`pay_transaction`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE',`pay_transaction`.`NARRATION`,`pay_transaction`.`BILL_MONTH`,`pay_transaction`.`DISCOUNT`,`pay_transaction`.`DISCOUNTED_PRICE`,`pay_transaction`.`TAXABLE_AMT`,`pay_transaction`.`EXPIRY_DATE`,`pay_transaction`.`NET_TOTAL`,`pay_transaction`.`EXTRA_CHRGS`,`pay_transaction`.`EXTRA_CHRGS_AMT`,`pay_transaction`.`EXTRA_CHRGS_TAX`,`pay_transaction`.`EXTRA_CHRGS_TAX_AMT`,`pay_transaction`.`FINAL_PRICE`,`pay_transaction`.`BILL_YEAR`,`pay_transaction`.`customer_gst_no`,DATE_FORMAT(`pay_transaction`.`EXPIRY_DATE`, '%d/%m/%Y') AS 'EXPIRY_DATE',`pay_transaction`.`BILL_ADDRESS`, `pay_transaction`.`SHIPPING_ADDRESS`,  `pay_transaction`.`SALES_PERSON`,`pay_transaction`.`CUSTOMER_NOTES`,`pay_transaction`.`TERMS_CONDITIONS`,`pay_transaction`.`p_o_no`,`pay_transaction`.`TRANSPORT`,`pay_transaction`.`FREIGHT`, `pay_transaction`.`VEHICLE_NO`, pay_transaction.state as 'branch_state',`pay_transaction_DETAILS`.`SR_NO`,`pay_transaction_DETAILS`.`PARTICULAR`,`pay_transaction_DETAILS`.`DESCRIPTION`,`pay_transaction_DETAILS`.`VAT`,`pay_transaction_DETAILS`.`hsn_number`,`pay_transaction_DETAILS`.`DESIGNATION`,`pay_transaction_DETAILS`.`QUANTITY`,`pay_transaction_DETAILS`.`RATE`,`pay_transaction_DETAILS`.`DISCOUNT`,`pay_transaction_DETAILS`.`DISCOUNT_AMT`,`pay_transaction_DETAILS`.`AMOUNT`,`pay_transaction`.`SALES_MOBILE_NO`,`pay_transaction_DETAILS`.`item_type`,IF(instr(`pay_transaction_DETAILS`.`emp_code`,'Select'),'-',`pay_transaction_DETAILS`.`emp_code`) as 'emp_code', IF(instr(`pay_transaction_DETAILS`.`emp_name`,'Select'),'-',`pay_transaction_DETAILS`.`emp_name`) as 'emp_name', (SELECT `Unit_name` FROM `pay_unit_master` WHERE `unit_code` = `branch_name` AND `comp_code` = 'C01') AS 'branch_name', (SELECT `EMP_MOBILE_NO` FROM `pay_employee_master` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `comp_code` = 'C01') AS 'EMP_MOBILE_NO',(SELECT `GRADE_DESC` FROM   `pay_employee_master`   INNER JOIN `pay_grade_master` ON `pay_grade_master`.`GRADE_CODE` = `pay_employee_master`.`GRADE_CODE` AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` WHERE `emp_code` = `pay_transaction_details`.`emp_code` AND `pay_employee_master`.`comp_code` = 'C01') AS 'GRADE_CODE',IF(instr(size_uniform,'Select'),'-',size_uniform) as 'size_uniform',IF(instr(size_shoes,'Select'),'-',size_shoes) as 'size_shoes',IF(INSTR(`size_pantry`, 'Select'), '-', `size_pantry`) AS 'size_pantry',IF(INSTR(`size_apron`, 'Select'), '-', `size_apron`) AS 'size_apron' FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";

                MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Delivery_Challan" + ddlunitselect.SelectedItem.Text.Replace(" ", "_") + ".xls");

                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Repeater Repeater1 = new Repeater();
                    Repeater1.DataSource = ds;
                    Repeater1.HeaderTemplate = new MyTemplate3(ListItemType.Header, ds, 2);
                    Repeater1.ItemTemplate = new MyTemplate3(ListItemType.Item, ds, 2);
                    Repeater1.FooterTemplate = new MyTemplate3(ListItemType.Footer, null, 2);
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

                    lc = new LiteralControl("<table border=1><tr ><th colspan=13>UNIFORM / SHOES DELIVERY RECEIPT</th></tr><tr><th>RECEIPT NO.</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_NO"] + "</th></tr><tr><th>DATE OF APPROVAL</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr><th>DATE OF DESPATCH</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr></tr><tr><th>SR NO.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>CONTACT NO.</th><th>DESIGNATION</th><th>MATERIAL TYPE</th><th>UNIFORM SIZE</th><th>SHOES SIZE</th><th>PANTRY JACKET SIZE</th><th>APRON SIZE</th><th>QUANTITY</th><th>POD NO</th><th>REMARK</th></tr> ");
                    break;
                case ListItemType.Item:


                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_state"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_address"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["GRADE_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_uniform"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_shoes"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_pantry"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_apron"] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td></tr>");

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
    public class MyTemplate1 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;


        public MyTemplate1(ListItemType type, DataSet ds, int i)
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

                    lc = new LiteralControl("<table border=1><tr ><th colspan=13>UNIFORM / SHOES DELIVERY RECEIPT</th></tr><tr><th>RECEIPT NO.</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_NO"] + "</th></tr><tr><th>DATE OF APPROVAL</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr><th>DATE OF DESPATCH</th><th colspan=12>" + ds.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr></tr><tr><th>SR NO.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>CONTACT NO.</th><th>DESIGNATION</th><th>MATERIAL TYPE</th><th>UNIFORM SIZE</th><th>SHOES SIZE</th><th>PANTRY JACKET SIZE</th><th>APRON SIZE</th><th>QUANTITY</th><th>POD NO</th><th>REMARK</th></tr> ");
                    break;
                case ListItemType.Item:


                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_address"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["GRADE_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_uniform"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_shoes"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_pantry"] + "</td><td>" + ds.Tables[0].Rows[ctr]["size_apron"] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td></tr>");

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

    //vikas excel for pantry

    public class MyTemplate2 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds2;
        static int ctr;
        int i;


        public MyTemplate2(ListItemType type, DataSet ds, int i)
        {
            this.type = type;
            this.ds2 = ds;

            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }

        public void InstantiateIn(Control container)
        {


            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr ><th colspan=13>UNIFORM / SHOES DELIVERY RECEIPT</th></tr><tr><th>RECEIPT NO.</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_NO"] + "</th></tr><tr><th>DATE OF APPROVAL</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr><th>DATE OF DESPATCH</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr></tr><tr><th>SR NO.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>CONTACT NO.</th><th>DESIGNATION</th><th>MATERIAL TYPE</th><th>UNIFORM SIZE</th><th>SHOES SIZE</th><th>PANTRY SIZE</th><th>QUANTITY</th><th>POD NO</th><th>REMARK</th></tr> ");
                    break;
                case ListItemType.Item:


                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds2.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["branch_address"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["GRADE_CODE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_uniform"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_shoes"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_pantry"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td></tr>");

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
    //excel end

    //id card start
    public class MyTemplate4 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds2;
        static int ctr;
        int i;


        public MyTemplate4(ListItemType type, DataSet ds, int i)
        {
            this.type = type;
            this.ds2 = ds;

            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }

        public void InstantiateIn(Control container)
        {


            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr ><th colspan=13>UNIFORM / SHOES DELIVERY RECEIPT</th></tr><tr><th>RECEIPT NO.</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_NO"] + "</th></tr><tr><th>DATE OF APPROVAL</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr><th>DATE OF DESPATCH</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr></tr><tr><th>SR NO.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>CONTACT NO.</th><th>DESIGNATION</th><th>MATERIAL TYPE</th><th>UNIFORM SIZE</th><th>SHOES SIZE</th><th>PANTRY SIZE</th><th>QUANTITY</th><th>POD NO</th><th>REMARK</th></tr> ");
                    break;
                case ListItemType.Item:


                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds2.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["branch_address"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["GRADE_CODE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_uniform"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_shoes"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_pantry"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td></tr>");

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

    //id card end 
    //apron start
    public class MyTemplate3 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds2;
        static int ctr;
        int i;


        public MyTemplate3(ListItemType type, DataSet ds, int i)
        {
            this.type = type;
            this.ds2 = ds;

            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }

        public void InstantiateIn(Control container)
        {


            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr ><th colspan=13>UNIFORM / SHOES DELIVERY RECEIPT</th></tr><tr><th>RECEIPT NO.</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_NO"] + "</th></tr><tr><th>DATE OF APPROVAL</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr><th>DATE OF DESPATCH</th><th colspan=13>" + ds2.Tables[0].Rows[ctr]["DOC_DATE"] + "</th></tr><tr></tr><tr><th>SR NO.</th><th>CLIENT NAME</th><th>STATE</th><th>LOCATION</th><th>EMPLOYEE NAME</th><th>CONTACT NO.</th><th>DESIGNATION</th><th>MATERIAL TYPE</th><th>UNIFORM SIZE</th><th>SHOES SIZE</th><th>PANTRY JACKET SIZE</th><th>APRON SIZE</th><th>QUANTITY</th><th>POD NO</th><th>REMARK</th></tr> ");
                    break;
                case ListItemType.Item:


                    //  lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ADDRESS1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds.Tables[0].Rows[ctr][""] + "</td><td>" + ds.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + "</td><td>" + ds.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td><td>" + "</td></tr>");
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds2.Tables[0].Rows[ctr]["CLIENT_NAME"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["STATE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["branch_address"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["GRADE_CODE"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["item_type"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_uniform"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_shoes"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_pantry"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["size_apron"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["QUANTITY"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["p_o_no"] + "</td><td>" + ds2.Tables[0].Rows[ctr]["DESCRIPTION"] + "</td></tr>");

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

    //end excel for apron

    protected void btn_Print_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_itemslist.Rows)
        {

            Label lbl_item_type1 = (Label)row.FindControl("lbl_item_type");
            string lbl_item_type = lbl_item_type1.Text.ToString();
            if (lbl_item_type == "Shoes" || lbl_item_type == "SHOES")
            {
                generate_report(1);
            }
            else if (lbl_item_type == "Uniform" || lbl_item_type == "UNIFORM")
            {
                generate_report(2);
            }
            else if (lbl_item_type == "Pantry_Jacket" || lbl_item_type == "PANTRY_JACKET")
            {
                generate_report(3);
            }
            else if (lbl_item_type == "ID_Card" || lbl_item_type == "ID_CARD" || lbl_item_type == " id_card")
            {
                generate_report(4);
            }
            else if (lbl_item_type == "Apron" || lbl_item_type == "APRON")
            {
                generate_report(5);
            }

            else
            {
                try
                {
                    string discount_counter = "";
                    ReportDocument crystalReport = new ReportDocument();
                    string compcd = Session["COMP_CODE"].ToString();
                    DataTable dt = new DataTable();
                    if (txt_docno.Text != "")
                    {
                        // string query = "    SELECT  PAY_COMPANY_MASTER.COMP_CODE, PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO, PAY_COMPANY_MASTER.ECC_CODE_NO,  PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, pay_transactionQ.DOC_NO, pay_transactionQ.DOC_DATE,pay_transactionQ.CUST_NAME, pay_transactionQ.CUST_CODE, pay_transactionQ.REF_NO1, pay_transactionQ.REF_NO2, pay_transactionQ.NARRATION, pay_transactionQ.BILL_MONTH, pay_transactionQ.GROSS_AMOUNT, pay_transactionQ.SER_PER_REC, pay_transactionQ.SER_TAXABLE_REC, pay_transactionQ.SER_TAX_PER_REC, pay_transactionQ.SER_TAX_PER_REC_AMT, pay_transactionQ.SER_TAX_CESS_PER_REC, pay_transactionQ.SER_TAX_CESS_REC_AMT, pay_transactionQ.SER_TAX_HCESS_PER_REC, pay_transactionQ.SER_TAX_HCESS_REC_AMT, pay_transactionQ.SER_TAX_REC_TOT, pay_transactionQ.SER_PER_PRO, pay_transactionQ.SER_TAXABLE_PRO, pay_transactionQ.SER_TAX_PER_PRO, pay_transactionQ.SER_TAX_PER_PRO_AMT, pay_transactionQ.SER_TAX_CESS_PER_PRO, pay_transactionQ.SER_TAX_CESS_PRO_AMT, pay_transactionQ.SER_TAX_HCESS_PER_PRO, pay_transactionQ.SER_TAX_HCESS_PRO_AMT, pay_transactionQ.SER_TAX_PRO_TOT, pay_transactionQ.NET_TOTAL, pay_transactionQ.DEDUCTION, pay_transactionQ.TOTAL, pay_transactionQ.BILL_YEAR, pay_transactionQ_DETAILS.SR_NO, pay_transactionQ_DETAILS.PARTICULAR, pay_transactionQ_DETAILS.DESIGNATION, pay_transactionQ_DETAILS.QUANTITY, pay_transactionQ_DETAILS.RATE, pay_transactionQ_DETAILS.AMOUNT, PAY_CUSTOMER_MASTER.CUST_NAME, PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE, PAY_CUSTOMER_MASTER.CITY, PAY_CUSTOMER_MASTER.PIN, PAY_CUSTOMER_MASTER.CONTACT_PERSON,pay_transactionQ.customer_gst_no,pay_transactionQ.IGST_TAX_PER_PRO,pay_transactionQ.IGST_TAX_PER_PRO_AMT,pay_transactionQ_DETAILS.hsn_code,pay_transactionQ_DETAILS.sac_code FROM  pay_transactionQ INNER JOIN  pay_transactionQ_DETAILS ON pay_transactionQ.COMP_CODE = pay_transactionQ_DETAILS.COMP_CODE AND pay_transactionQ.TASK_CODE = pay_transactionQ_DETAILS.TASK_CODE AND pay_transactionQ.DOC_NO = pay_transactionQ_DETAILS.DOC_NO INNER JOIN PAY_CUSTOMER_MASTER ON pay_transactionQ.COMP_CODE = PAY_CUSTOMER_MASTER.COMP_CODE AND pay_transactionQ.CUST_CODE = PAY_CUSTOMER_MASTER.CUST_ID INNER JOIN PAY_COMPANY_MASTER ON pay_transactionQ.COMP_CODE = PAY_COMPANY_MASTER.COMP_CODE AND pay_transactionQ.TASK_CODE='INV' AND  pay_transactionQ.DOC_NO >='" + txt_docno.Text + "' AND pay_transactionQ.DOC_NO <='" + txt_docno.Text + "'  AND pay_transactionQ.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionQ.DOC_NO,pay_transactionQ_DETAILS.SR_NO ";
                        string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',(select `STATE_CODE` from pay_state_master inner join `PAY_COMPANY_MASTER` on `pay_state_master`.STATE_NAME= `PAY_COMPANY_MASTER`.`STATE`   limit 1) AS 'Expr3',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT, pay_transaction.SALES_MOBILE_NO,	pay_transaction_DETAILS.emp_name FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE    AND `pay_client_master`.`COMP_CODE` = `PAY_COMPANY_MASTER`.`COMP_CODE` AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                        MySqlCommand cmd = new MySqlCommand(query, d.con);
                        MySqlDataReader sda = null;
                        d.con.Open();
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
                                crystalReport.Load(Server.MapPath("~/Rpt_InvoiceI_Discount.rpt"));
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
                    // txt_year.Text = "2017";
                }
            }
        }
    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[20].Visible = false;
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
        ddl_customerlist.SelectedIndex = 0;
        txt_docdate.Text = DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
        txt_taxable_amt.Text = "0";
        txt_sub_total1.Text = "0";
        txt_extra_chrgs_amt.Text = "0";
        txt_sub_total2.Text = "0";
        txt_final_total.Text = "0";
        txt_extra_chrgs.Text = "";
        txt_extra_chrgs_tax_amt.Text = "0";
        //txt_designation.Text = "";
        txt_quantity.Text = "0";
        txt_extra_chrgs_tax.Text = "0";
        txt_rate.Text = "";
        txt_amount.Text = "";
        txt_narration.Text = "";
        txt_grossamt.Text = "";
        txt_net_total_1.Text = "";
        txt_expiry_date.Text = "";
        txt_bill_add.Text = "";
        txt_ship_add.Text = "";
        txt_referenceno2.Text = "";
        txt_customer_notes.Text = "";
        txt_terms_conditions.Text = "";
        txt_customer_gst.Text = "";
        ddl_sales_person.Text = "";
        txt_sales_mobile_no.Text = "";
        //txt_particular.SelectedIndex = 0;
        txt_desc.Text = "";
        txt_description.Text = "0";
        txt_hsn.Text = "";
        ddl_sales_person.Text = "";
        txt_discount_price.Text = "0";
        txt_discount_rate.Text = "0";
        txt_p_o_no.Text = "";
        txt_start_date.Text = txt_docdate.Text;
        txt_end_date.Text = txt_docdate.Text; ;
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        txt_discount_price.Text = "0";
        float rate = float.Parse(txt_rate.Text);
        float quantity = float.Parse(txt_quantity.Text);
        if (lbl_qty.Text == "")
        {
            lbl_qty.Text = "0";
        }
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
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
        //   txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
        txt_designation.ToolTip = txt_designation.Text;
        txt_quantity.ToolTip = txt_quantity.Text;
        txt_rate.ToolTip = txt_rate.Text;
        ///tabel particular
        ///

        //   txt_particular.ToolTip = txt_particular.SelectedItem.ToString();
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
        hidtab.Value = "1";
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); 
            int result = 0;
            int invoice_result = 0;
            int item_result = 0;
            int TotalRows = gv_itemslist.Rows.Count;

            int month = int.Parse(txt_docdate.Text.Substring(3, 2));
            int year = int.Parse(txt_docdate.Text.Substring(6));

            //System.Web.UI.WebControls.Label lbl_docnumber = (System.Web.UI.WebControls.Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
            //string l_docnumber = lbl_docnumber.Text;
            string l_docnumber = txt_docno.Text;
            //txt_docno
            lbl_print_quote.Text = l_docnumber;
            d.operation("DELETE FROM pay_transaction WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");
            profitloss.getItemCode(l_docnumber, Session["COMP_CODE"].ToString(), "pay_transaction_DETAILS");
            d.operation("DELETE FROM pay_transaction_DETAILS WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "' ");
            if (txt_extra_chrgs_tax.Text == "")
            {
                txt_extra_chrgs_tax.Text = "0";
            }

            final_total();
            string final = txt_final_total.Text;
            //invoice_result = d.operation("INSERT into pay_transaction(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category) values('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.Text.Substring(0, 4) + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txt_sales_mobile_no.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','"+ddlcategory.SelectedItem.Text+"')");
            invoice_result = d.operation("INSERT into pay_transaction(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,`CUST_CODE`,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,`CUST_NAME`,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,REF_NO1,state,branch_name,MONTH,YEAR) values('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedValue + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedItem + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txt_sales_mobile_no.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','Existing','" + ddl_state.SelectedValue + "','" + ddlunitselect.SelectedValue + "','" + month + "','" + year + "')");

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
                Label lbl_item_type = (Label)row.FindControl("lbl_item_type");
                string item_type = (lbl_item_type.Text);

                TextBox lbl_uniformsize = (TextBox)row.FindControl("lbl_uniformsize");
                string uniformsize = lbl_uniformsize.Text.ToString();
                TextBox lbl_shoessixe = (TextBox)row.FindControl("lbl_shoessize");
                string shoes = lbl_shoessixe.Text.ToString();

                double sales_rate = Convert.ToDouble(rate);

                TextBox lbl_emp_name = (TextBox)row.FindControl("lbl_emp_name");
                string emp_name = lbl_emp_name.Text.ToString();

                TextBox lbl_emp_code = (TextBox)row.FindControl("lbl_empcode");
                string emp_code = lbl_emp_code.Text.ToString();
                //vikas13-12
                TextBox lbl_pod_num = (TextBox)row.FindControl("lbl_pod_num");
                string POD_NUM = lbl_pod_num.Text.ToString();

                //string query = "INSERT INTO pay_transaction_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR,item_type,size_uniform,size_shoes,emp_name,emp_code) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "','" + emp_name + "','" + emp_code + "'";
                int insert_result = d.operation("INSERT INTO pay_transaction_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,START_DATE,END_DATE,VENDOR,item_type,size_uniform,size_shoes,emp_name,emp_code,MONTH,YEAR,POD_NUM) VALUES('" + Session["COMP_CODE"].ToString() + "','INV','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "','" + designation.ToString() + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(product_discount) + "," + Convert.ToDouble(product_discount_amt) + "," + Convert.ToDouble(amount) + ",STR_TO_DATE('" + start_date + "','%d/%m/%Y'),STR_TO_DATE('" + end_date + "','%d/%m/%Y'),'" + vendor + "','" + item_type + "','" + uniformsize + "','" + shoes + "','" + emp_name + "','" + emp_code + "','" + month + "','" + year + "','" + POD_NUM + "')");
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
                        int updateProfitLoss = profitloss.calculateProfitLoss("+", item_code, Convert.ToDouble(rate), Convert.ToDouble(quantity));
                    }

                    catch { }
                }
                insert_result = 0;
                if (invoice_result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invoice (" + txt_docno.Text + ") Updated successfully!');", true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice (" + txt_docno.Text + ") Updated successfully!')", true);
                    Panel5.Visible = false;
                    gv_bynumber_name.Visible = false;
                    attached_doc();
                    text_clear();

                    lbl_print_quote.Text = txt_docno.Text;

                }
                else
                {
                    d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    profitloss.getItemCode(txt_docno.Text, Session["COMP_CODE"].ToString(), "pay_transaction_details");
                    d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                }

            }
        }

        catch (Exception ee)
        {
            d.operation("Delete from pay_transaction Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            profitloss.getItemCode(txt_docno.Text, Session["COMP_CODE"].ToString(), "pay_transaction_details");
            d.operation("Delete from pay_transaction_details Where DOC_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {
            d.con.Close();
            btn_Save.Visible = true;
            btn_update.Visible = false;
            btn_delete.Visible = false;
            tooltrip();
            btn_btn_clear(null, null);
            gendocno();
        }


    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); 
            int result = 0;

            //System.Web.UI.WebControls.Label lbl_docnumber = (System.Web.UI.WebControls.Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
            //string l_docnumber = lbl_docnumber.Text;
            string l_docnumber = txt_docno.Text;
            d.operation("DELETE FROM pay_transaction WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                string item_code = (lbl_item_code.Text);
                TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                double quantity = double.Parse(txt_quantity.Text);
                TextBox lbl_rate = (TextBox)row.FindControl("lbl_rate");
                double sales_rate = double.Parse(lbl_rate.Text);

                profitloss.calculateProfitLoss("-", item_code, sales_rate, quantity);
                d.operation("DELETE FROM pay_transaction_DETAILS WHERE Item_Code = '" + item_code + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND DOC_NO='" + l_docnumber + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ddl_customerlist.SelectedItem + " Deleted Successfully');", true);
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
        finally
        {
            text_clear();
            btn_btn_clear(null, null);
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
        hidtab.Value = "1";
        if (txt_docno_number.Text != "")
        {
            MySqlCommand cmd_docno = new MySqlCommand("SELECT pay_transaction.DOC_NO,date_format(pay_transaction.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,pay_client_master.`CLIENT_NAME` AS 'CUST_NAME',FINAL_PRICE FROM `pay_transaction` inner join pay_client_master on pay_transaction.`CUST_CODE`=pay_client_master.CLIENT_CODE and  pay_transaction.`COMP_CODE`=pay_client_master.COMP_CODE WHERE (pay_transaction.DOC_NO LIKE '%" + txt_docno_number.Text + "%' AND pay_transaction.REF_NO1 Not in('NEW') AND pay_transaction.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transaction.DOC_DATE DESC ", d.con);
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
                d.con.Close();
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
                MySqlCommand cmd_customername = new MySqlCommand("SELECT pay_transaction.CUST_NAME,pay_transaction.DOC_NO,date_format(pay_transaction.DOC_DATE,'%d/%m/%Y') as DOC_DATE,FINAL_PRICE FROM pay_transaction WHERE (pay_transaction.CUST_NAME LIKE '%" + txt_customername.Text + "%' AND pay_transaction.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transaction.DOC_DATE DESC", d.con);
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
        // SELECT  pay_transactionQ.DOC_NO,pay_transactionQ.DOC_DATE AS 'DOC_DATE',CUST_NAME FROM pay_transactionQ   ORDER BY pay_transactionQ.DOC_DATE DESC 
    }

    protected void txt_customername_TextChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string customername = txt_customername.Text;
        if (customername != "")
        {
            txt_docno.Text = "";
        }
        //Panel2.Visible = false;
    }

    //protected void gv_bynumber_name_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hidtab.Value = "1";
    //    Panel6.Visible = true;
    //    Panel4.Visible = true;
    //    Panel8.Visible = true;
    //    btn_Save.Visible = false;

    //    gv_itemslist.Visible = true;
    //    btn_update.Visible = true;
    //    btn_delete.Visible = true;

    //    Label lbl_doccode = (Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
    //    string doc_no = lbl_doccode.Text;
    //    lbl_print_quote.Text = doc_no;

    //    string employee_name = d.getsinglestring("select `emp_name` from pay_transaction_details where `DOC_NO`='" + doc_no + "'");



    //    MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),`CUST_CODE`,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO , SALES_MOBILE_NO,BILL_YEAR,BILL_MONTH,Category,state,branch_name FROM pay_transaction WHERE (DOC_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr2 = cmd2.ExecuteReader();
    //        if (dr2.Read())
    //        {
    //            txt_docno.Text = dr2[0].ToString();
    //            txt_docdate.Text = dr2[1].ToString();
    //            if (employee_name == "Select")
    //            {
    //                ddl_customerlist.SelectedValue = dr2[2].ToString();
    //            }
    //            else
    //            {
    //                client_code();
    //                ddl_customerlist.SelectedValue = dr2[2].ToString();
    //            }
    //            get_client_contact(ddl_customerlist.SelectedValue.ToString());
    //            txt_narration.Text = dr2[3].ToString();
    //            txt_customer_gst.Text = dr2["customer_gst_no"].ToString();
    //            txt_expiry_date.Text = dr2["EXPIRY_DATE"].ToString();
    //            txt_bill_add.Text = dr2["BILL_ADDRESS"].ToString();
    //            txt_ship_add.Text = dr2["SHIPPING_ADDRESS"].ToString();
    //            ddl_sales_person.Text = dr2["SALES_PERSON"].ToString();
    //            txt_referenceno2.Text = dr2["REF_NO2"].ToString();
    //            txt_tot_discount_percent.Text = dr2["DISCOUNT"].ToString();
    //            txt_tot_discount_amt.Text = dr2["DISCOUNTED_PRICE"].ToString();
    //            txt_taxable_amt.Text = dr2["TAXABLE_AMT"].ToString();
    //            txt_sub_total1.Text = dr2["NET_TOTAL"].ToString();
    //            txt_extra_chrgs.Text = dr2["EXTRA_CHRGS"].ToString();
    //            txt_extra_chrgs_amt.Text = dr2["EXTRA_CHRGS_AMT"].ToString();
    //            txt_extra_chrgs_tax.Text = dr2["EXTRA_CHRGS_TAX"].ToString();
    //            txt_extra_chrgs_tax_amt.Text = dr2["EXTRA_CHRGS_TAX_AMT"].ToString();
    //            txt_sub_total2.Text = Convert.ToString(double.Parse(txt_extra_chrgs_amt.Text) + double.Parse(txt_extra_chrgs_tax_amt.Text));
    //            txt_final_total.Text = dr2["FINAL_PRICE"].ToString();
    //            txt_customer_notes.Text = dr2["CUSTOMER_NOTES"].ToString();
    //            txt_terms_conditions.Text = dr2["TERMS_CONDITIONS"].ToString();
    //            txt_p_o_no.Text = dr2["p_o_no"].ToString();
    //            ddl_transport.SelectedValue = dr2["TRANSPORT"].ToString();
    //            txt_freight.Text = dr2["FREIGHT"].ToString();
    //            txt_vehicle.Text = dr2["VEHICLE_NO"].ToString();
    //            txt_sales_mobile_no.Text = dr2["SALES_MOBILE_NO"].ToString();
    //            txt_grossamt.Text = dr2["GROSS_AMOUNT"].ToString();
    //            txt_year.Text = dr2["BILL_YEAR"].ToString();
    //            ddlcalmonth.Text = dr2["BILL_MONTH"].ToString();

    //            ddlcategory.SelectedValue = dr2["Category"].ToString();
    //            if (employee_name == "Select")
    //            {
    //                client_state();
    //                ddl_state.SelectedValue = dr2["state"].ToString();
    //                ddl_state_SelectedIndexChanged(null, null);
    //                ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
    //            }
    //            else
    //            {
    //                branch_fill();
    //                ddl_state.SelectedValue = dr2["state"].ToString();
    //                state();
    //                ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
    //            }

    //        }
    //        dr2.Close();
    //        d.con.Close();
    //        gst_counter(txt_customer_gst.Text.Substring(0, 2));
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //        tooltrip();
    //        ddl_employee_termination(null, null);
    //    }


    //    // MySqlCommand cmd1 = new MySqlCommand("SELECT PARTICULAR,DESCRIPTION,hsn_code,sac_code, DESIGNATION, ROUND(QUANTITY,6) AS QUANTITY, ROUND(RATE,6) AS RATE, ROUND(AMOUNT,2) AS AMOUNT FROM pay_transactionQ_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);

    //    MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,item_type,PARTICULAR,emp_name,size_uniform,size_shoes,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,DATE_FORMAT(START_DATE,'%d/%m/%Y') As START_DATE,DATE_FORMAT(END_DATE,'%d/%m/%Y') As END_DATE,VENDOR,emp_code,POD_NUM,size_pantry,size_apron FROM pay_transaction_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr1 = cmd1.ExecuteReader();
    //        if (dr1.HasRows)
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Load(dr1);
    //            if (dt.Rows.Count > 0)
    //            {
    //                ViewState["CurrentTable"] = dt;
    //            }
    //            gv_itemslist.DataSource = dt;
    //            gv_itemslist.DataBind();
    //            // discount_calculate(dt, 1);
    //            gst_calculate(dt);
    //        }
    //        dr1.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //        tooltrip();
    //    }
    //    attached_doc();
    //    lbl_print_quote.Text = txt_docno.Text;
    //    gv_bynumber_name.Visible = true;
    //}

    protected void txt_docno_TextChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string doc_no = txt_docno.Text;
        if (doc_no != "")
        {
            txt_customername.Text = "";
        }
    }

    protected void particular_hsn_sac_code(object sender, EventArgs e)
    {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
           catch { }
        txt_designation.Visible = true;
        if (txt_particular.SelectedItem.ToString() != "Select")
        {
            MySqlCommand cmd = new MySqlCommand("Select item_description,SALES_RATE,unit,VAT,case When hsn_number <> '' then hsn_number else sac_number END as hsn_sac_no , case When hsn_number <> '' then 'H' else 'S' END as hsn , unit_per_piece,Stock from pay_item_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ITEM_NAME = '" + txt_particular.SelectedItem.ToString() + "' and ITEM_CODE='" + txt_particular.SelectedValue + "' ", d.con);
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
                    //vikas 08/04/2019
                    txt_quantity_TextChanged(null, null);
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                txt_hsn.Enabled = false;
                lbl_qty.Visible = true;
                // txt_quantity1.Visible = true;
                // txt_designation.Enabled = false;
                // txt_rate.Enabled = false;
                //   txt_description.Enabled = false;
                tooltrip();
            }
            if (ddl_emp.SelectedValue == "Select")
            {
                txt_quantity.ReadOnly = false;
            }
            else
            {
                txt_quantity.ReadOnly = true;
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

    protected void customer_details(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        client_state();
    }

    protected void client_state()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //State
        ddl_state.Items.Clear();
        dt_item = new System.Data.DataTable();
        //  MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_customerlist.SelectedValue + "' ORDER BY state_name", d4.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select distinct `billing_state` from pay_billing_master where comp_code='" + Session["comp_code"] + "' and `billing_client_code` = '" + ddl_customerlist.SelectedValue + "' and material_contract='1' ORDER BY `billing_state`", d4.con);
        d4.con.Open();
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
            d4.con.Close();
            ddl_state.Items.Insert(0, "Select");
            //ddl_state.SelectedIndex = 0;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d4.con.Close();
        }
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        int month = int.Parse(txt_docdate.Text.Substring(3, 2));
        int year = int.Parse(txt_docdate.Text.Substring(6));
        ddlunitselect.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_customerlist.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' ORDER BY UNIT_CODE", d4.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select distinct (SELECT `UNIT_NAME` FROM `pay_unit_master` WHERE `unit_code` = `pay_billing_master_history`.`billing_unit_code` AND `comp_code` = `pay_billing_master_history`.`COMP_CODE`) as`billing_unit_code`,billing_unit_code from pay_billing_master_history  INNER JOIN `pay_unit_master` ON `pay_unit_master`.`unit_code` = `pay_billing_master_history`.`billing_unit_code` AND `pay_billing_master_history`.`COMP_CODE` = `pay_unit_master`.`comp_code` where pay_billing_master_history.comp_code='" + Session["comp_code"] + "' and `billing_client_code` = '" + ddl_customerlist.SelectedValue + "' and `billing_state`='" + ddl_state.SelectedValue + "' and material_contract='1' and month= '" + month + "' and year = '" + year + "' AND `pay_unit_master`.`branch_status` = 0 ", d4.con);
        d4.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddlunitselect.DataSource = dt_item;
                ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                ddlunitselect.DataBind();
            }
            dt_item.Dispose();
            d4.con.Close();
            ddlunitselect.Items.Insert(0, "Select");
            // show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d4.con.Close();
        }

        if (ddl_customerlist.SelectedItem.ToString() != "Select")
        {

            get_client_contact(ddl_customerlist.SelectedItem.ToString());
        }
        else
        {

            txt_customer_gst.Text = "";
            txt_customer_gst.ReadOnly = false;
        }
    }


    public void get_client_contact(string client_name)
    {
        //   MySqlCommand cmd = new MySqlCommand("SELECT `GST_NO`, CONCAT_WS('\n', `ADDRESS1`, `CITY`, `STATE`) AS 'ADDRESS1' FROM `pay_client_master`  WHERE `pay_client_master`.`CLIENT_CODE` ='" + ddl_customerlist.SelectedValue + "'", d.con1);
        MySqlCommand cmd = new MySqlCommand("SELECT `Field2`,Field1 AS 'ADDRESS1' FROM  pay_zone_master  WHERE  `CLIENT_CODE` ='" + ddl_customerlist.SelectedValue + "' and `REGION`='" + ddl_state.SelectedValue + "'", d.con1);
        d.con1.Open();
        try
        {
            MySqlDataReader dr_item = cmd.ExecuteReader();
            while (dr_item.Read())
            {
                txt_customer_gst.Text = dr_item.GetValue(0).ToString();
                txt_bill_add.Text = dr_item.GetValue(1).ToString();
                // txt_ship_add.Text = dr_item.GetValue(2).ToString();
                //ddl_contact_person.SelectedValue = dr_item.GetValue(3).ToString();
                //ddl_contact_person.Items.Add(dr_item[3].ToString());

            }
            dr_item.Close();
            cmd.Dispose();
            d.con1.Close();
            try
            {
                gst_counter(txt_customer_gst.Text.Substring(0, 2));
                dr_item.Close();
                cmd.Dispose();
                d.con1.Close();
                gst_counter(txt_customer_gst.Text.Substring(0, 2));
            }
            catch { }
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


        ddl_customerlist.Items.Clear();
        DataTable dt = new DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT  distinct(SELECT `CLIENT_NAME` FROM `pay_client_master` WHERE `CLIENT_CODE` = `pay_billing_master`.`billing_client_code` AND `comp_code` = `pay_billing_master`.`COMP_CODE`) as `CLIENT_NAME`,billing_client_code from pay_billing_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'and material_contract='1' order by CLIENT_NAME  ", d.con);
        d.con.Open();
        try
        {
            cmd_item1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_customerlist.DataSource = dt;
                ddl_customerlist.DataTextField = dt.Columns[0].ToString();
                ddl_customerlist.DataValueField = dt.Columns[1].ToString();
                ddl_customerlist.DataBind();
            }
            //   dr_item1.Close();
            ddl_customerlist.Items.Insert(0, new ListItem("Select", "Select"));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            tooltrip();
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
        // double discount_percent = 0, discount_price = 0, final_price = 0, gross_total_discount = 0, gross_total_no_discount = 0; ;
        // double c = 0;
        // foreach (DataRow dr_discount in dt_discount.Rows)
        // {
        //     if (Convert.ToDouble(dr_discount["DISCOUNT"].ToString()) <= 0)
        //     {
        //         gross_total_discount = gross_total_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
        //     }
        //     else
        //     {
        //         gross_total_no_discount = gross_total_no_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
        //     }
        //     c = c + Convert.ToDouble(dr_discount["Amount"].ToString());
        // }
        // if (c == gross_total_no_discount)
        // {
        //     txt_tot_discount_percent.Text = "0";
        //     txt_tot_discount_amt.Text = "0";
        //     txt_tot_discount_percent.Enabled = false;
        //     txt_tot_discount_amt.Enabled = false;
        // }
        // else
        // {
        //     txt_tot_discount_percent.Enabled = true;
        //     txt_tot_discount_amt.Enabled = true;
        //     if (discount_choice == 0)
        //     {
        //         discount_percent = Convert.ToDouble(txt_tot_discount_percent.Text);
        //         discount_price = Math.Round(((discount_percent * gross_total_discount) / 100), 2);
        //         final_price = Math.Round((gross_total_discount - discount_price), 2);
        //         txt_tot_discount_amt.Text = Convert.ToString(discount_price);
        //     }
        //     else if (discount_choice == 1)
        //     {
        //         discount_price = Convert.ToDouble(txt_tot_discount_amt.Text);
        //         discount_percent = Math.Round(((discount_price * 100) / gross_total_discount), 2);
        //         final_price = Math.Round((gross_total_discount - discount_price), 2);
        //         txt_tot_discount_percent.Text = Convert.ToString(discount_percent);
        //     }
        // }
        //txt_grossamt.Text = Convert.ToString(Math.Round(c,2));
        //txt_taxable_amt.Text = Convert.ToString(final_price + gross_total_no_discount);
        //gst_calculate(dt_discount);
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
            company_state = dr_state[0].ToString().Substring(0, 2);
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
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
        dt.Columns.Add("item_type");

        dt.Columns.Add("Particular");
        dt.Columns.Add("emp_name");
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
                TextBox lbl_emp_name = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_emp_name");
                dr["emp_name"] = lbl_emp_name.Text.ToString();

                TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_uniformsize");
                dr["size_uniform"] = lbl_shoessize.Text.ToString();
                TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_shoessize");
                dr["size_shoes"] = lbl_uniformsize.Text.ToString();
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
                TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[17].FindControl("lbl_start_date");
                dr["START_DATE"] = (lbl_start_date.Text);
                TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_end_date");
                dr["END_DATE"] = (lbl_end_date.Text);
                TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[19].FindControl("lbl_vendor");
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
            dt.Columns.Add("emp_name");
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
                    TextBox lbl_emp_name = (TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_emp_name");
                    dr["emp_name"] = lbl_emp_name.Text.ToString();

                    TextBox lbl_shoessize = (TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_uniformsize");
                    dr["size_uniform"] = lbl_shoessize.Text.ToString();
                    TextBox lbl_uniformsize = (TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbl_shoessize");
                    dr["size_shoes"] = lbl_uniformsize.Text.ToString();
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
                    TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[17].FindControl("lbl_start_date");
                    dr["START_DATE"] = (lbl_start_date.Text);
                    TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_end_date");
                    dr["END_DATE"] = (lbl_end_date.Text);
                    TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[19].FindControl("lbl_vendor");
                    dr["VENDOR"] = (lbl_vendor.Text);
                    dt.Rows.Add(dr);

                }
            }
            discount_calculate(dt, 1);
            // discount_calculate(dt, 0); 
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
                //string query = "    SELECT  PAY_COMPANY_MASTER.COMP_CODE, PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO, PAY_COMPANY_MASTER.ECC_CODE_NO,  PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO, pay_transactionQ.DOC_NO, pay_transactionQ.DOC_DATE,pay_transactionQ.CUST_NAME, pay_transactionQ.CUST_CODE, pay_transactionQ.REF_NO1, pay_transactionQ.REF_NO2, pay_transactionQ.NARRATION, pay_transactionQ.BILL_MONTH, pay_transactionQ.GROSS_AMOUNT, pay_transactionQ.SER_PER_REC, pay_transactionQ.SER_TAXABLE_REC, pay_transactionQ.SER_TAX_PER_REC, pay_transactionQ.SER_TAX_PER_REC_AMT, pay_transactionQ.SER_TAX_CESS_PER_REC, pay_transactionQ.SER_TAX_CESS_REC_AMT, pay_transactionQ.SER_TAX_HCESS_PER_REC, pay_transactionQ.SER_TAX_HCESS_REC_AMT, pay_transactionQ.SER_TAX_REC_TOT, pay_transactionQ.SER_PER_PRO, pay_transactionQ.SER_TAXABLE_PRO, pay_transactionQ.SER_TAX_PER_PRO, pay_transactionQ.SER_TAX_PER_PRO_AMT, pay_transactionQ.SER_TAX_CESS_PER_PRO, pay_transactionQ.SER_TAX_CESS_PRO_AMT, pay_transactionQ.SER_TAX_HCESS_PER_PRO, pay_transactionQ.SER_TAX_HCESS_PRO_AMT, pay_transactionQ.SER_TAX_PRO_TOT, pay_transactionQ.NET_TOTAL, pay_transactionQ.DEDUCTION, pay_transactionQ.TOTAL, pay_transactionQ.BILL_YEAR, pay_transactionQ_DETAILS.SR_NO, pay_transactionQ_DETAILS.PARTICULAR, pay_transactionQ_DETAILS.DESIGNATION, pay_transactionQ_DETAILS.QUANTITY, pay_transactionQ_DETAILS.RATE, pay_transactionQ_DETAILS.AMOUNT, PAY_CUSTOMER_MASTER.CUST_NAME, PAY_CUSTOMER_MASTER.CUST_ADD1, PAY_CUSTOMER_MASTER.CUST_ADD2, PAY_CUSTOMER_MASTER.STATE, PAY_CUSTOMER_MASTER.CITY, PAY_CUSTOMER_MASTER.PIN, PAY_CUSTOMER_MASTER.CONTACT_PERSON,pay_transactionQ.customer_gst_no,pay_transactionQ.IGST_TAX_PER_PRO,pay_transactionQ.IGST_TAX_PER_PRO_AMT,pay_transactionQ_DETAILS.hsn_code,pay_transactionQ_DETAILS.sac_code FROM  pay_transactionQ INNER JOIN  pay_transactionQ_DETAILS ON pay_transactionQ.COMP_CODE = pay_transactionQ_DETAILS.COMP_CODE AND pay_transactionQ.TASK_CODE = pay_transactionQ_DETAILS.TASK_CODE AND pay_transactionQ.DOC_NO = pay_transactionQ_DETAILS.DOC_NO INNER JOIN PAY_CUSTOMER_MASTER ON pay_transactionQ.COMP_CODE = PAY_CUSTOMER_MASTER.COMP_CODE AND pay_transactionQ.CUST_CODE = PAY_CUSTOMER_MASTER.CUST_ID INNER JOIN PAY_COMPANY_MASTER ON pay_transactionQ.COMP_CODE = PAY_COMPANY_MASTER.COMP_CODE AND pay_transactionQ.TASK_CODE='INV' AND  pay_transactionQ.DOC_NO >='" + txt_docno.Text + "' AND pay_transactionQ.DOC_NO <='" + txt_docno.Text + "'  AND pay_transactionQ.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transactionQ.DOC_NO,pay_transactionQ_DETAILS.SR_NO ";
                // 28/11  string query = "SELECT PAY_COMPANY_MASTER.COMP_CODE,PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT , pay_transaction.SALES_MOBILE_NO FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
                string query = "SELECT PAY_COMPANY_MASTER.COMPANY_NAME, PAY_COMPANY_MASTER.ADDRESS1, PAY_COMPANY_MASTER.ADDRESS2, PAY_COMPANY_MASTER.CITY, PAY_COMPANY_MASTER.STATE,PAY_COMPANY_MASTER.PIN, PAY_COMPANY_MASTER.PF_REG_NO, PAY_COMPANY_MASTER.ESIC_REG_NO, PAY_COMPANY_MASTER.COMPANY_PAN_NO, PAY_COMPANY_MASTER.COMPANY_TAN_NO,PAY_COMPANY_MASTER.COMPANY_CIN_NO, PAY_COMPANY_MASTER.COMPANY_CONTACT_NO, PAY_COMPANY_MASTER.COMPANY_WEBSITE, PAY_COMPANY_MASTER.SERVICE_TAX_REG_NO,  `pay_client_master`.`Client_code`, `pay_client_master`.`CLIENT_NAME`, `pay_client_master`.`CLIENT_NAME`,	`pay_client_master`.`ADDRESS1`, `pay_client_master`.`CITY` AS 'Expr1', `pay_client_master`.`STATE` AS 'Expr2',pay_transaction.DOC_NO , DATE_FORMAT(pay_transaction.DOC_DATE,'%d/%m/%Y') As DOC_DATE,pay_transaction.NARRATION,pay_transaction.BILL_MONTH,pay_transaction.DISCOUNT,pay_transaction.DISCOUNTED_PRICE,pay_transaction.TAXABLE_AMT,pay_transaction.NET_TOTAL,pay_transaction.EXTRA_CHRGS,pay_transaction.EXTRA_CHRGS_AMT,pay_transaction.EXTRA_CHRGS_TAX,pay_transaction.EXTRA_CHRGS_TAX_AMT,pay_transaction.FINAL_PRICE,pay_transaction.BILL_YEAR,pay_transaction.customer_gst_no,DATE_FORMAT(pay_transaction.EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,pay_transaction.BILL_ADDRESS,pay_transaction.SHIPPING_ADDRESS,pay_transaction.SALES_PERSON,pay_transaction.CUSTOMER_NOTES,pay_transaction.TERMS_CONDITIONS,pay_transaction.p_o_no,pay_transaction.TRANSPORT,pay_transaction.FREIGHT,pay_transaction.VEHICLE_NO,pay_transaction_DETAILS.SR_NO,pay_transaction_DETAILS.PARTICULAR,pay_transaction_DETAILS.DESCRIPTION,pay_transaction_DETAILS.VAT,pay_transaction_DETAILS.hsn_number,pay_transaction_DETAILS.DESIGNATION,pay_transaction_DETAILS.QUANTITY,pay_transaction_DETAILS.RATE,pay_transaction_DETAILS.DISCOUNT,pay_transaction_DETAILS.DISCOUNT_AMT,pay_transaction_DETAILS.AMOUNT , pay_transaction.SALES_MOBILE_NO FROM pay_client_master, PAY_COMPANY_MASTER, pay_transaction_DETAILS, pay_transaction WHERE pay_client_master.COMP_CODE = pay_transaction.COMP_CODE AND pay_client_master.client_code = pay_transaction.CUST_CODE AND PAY_COMPANY_MASTER.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.COMP_CODE = pay_transaction.COMP_CODE AND pay_transaction_DETAILS.TASK_CODE = pay_transaction.TASK_CODE AND pay_transaction_DETAILS.DOC_NO = pay_transaction.DOC_NO AND pay_transaction_details.TASK_CODE='INV'   AND pay_transaction_details.DOC_NO >='" + lbl_print_quote.Text + "' and  pay_transaction_details.DOC_NO <='" + lbl_print_quote.Text + "' AND pay_transaction_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_transaction_details.DOC_NO,pay_transaction_details.SR_NO";
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

        // txt_particular.Items.Insert(0, "Select");
        txt_designation.Items.Insert(0, new ListItem("Select Unit", ""));

    }

    protected void unit_per_price_changes(object sender, EventArgs e)
    {

        string unit_name = txt_designation.SelectedItem.ToString();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT item_pieces FROM item_unit_master where item_unit_name='" + txt_designation.SelectedValue.ToString() + "'", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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

    protected void ddl_employee_termination(object sender, EventArgs e)
    {
        ddl_emp.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct `pay_employee_master`.`EMP_CODE`,emp_name FROM `pay_employee_master` INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` WHERE `pay_document_details`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_document_details`.`unit_code` = '" + ddlunitselect.SelectedValue + "'", d3.con);
        d3.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_emp.DataSource = dt_item;
                ddl_emp.DataTextField = dt_item.Columns[1].ToString();
                ddl_emp.DataValueField = dt_item.Columns[0].ToString();

                ddl_emp.DataBind();

            }
            dt_item.Dispose();
            d3.con.Close();
            ddl_emp.Items.Insert(0, "Select");

            //  ddl_emp_warn.Items.Insert(1, "ALL");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
        }


    }
    protected void fill_txt_particular(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        //----- Item List ------------------
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
        txt_particular.Items.Clear();
        MySqlCommand cmd_item = new MySqlCommand("SELECT distinct item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' ORDER BY ITEM_NAME", d.con);
        // MySqlCommand cmd_item = new MySqlCommand("SELECT distinct(`pay_client_item_list`.`item_name`),pay_item_master.item_name FROM `pay_client_item_list` INNER JOIN `pay_item_master` ON `pay_client_item_list`.`comp_code` = `pay_item_master`.`comp_code` AND 	`pay_client_item_list`.`item_name` = `pay_item_master`.`item_code` WHERE `pay_client_item_list`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "'  AND `pay_item_master`.`product_service` = '" + ddl_product.SelectedValue + "' AND  client_code='" + ddl_customerlist.SelectedValue + "'", d.con);
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
        if (ddl_emp.SelectedValue == "Select")
        {
            txt_quantity.ReadOnly = false;
        }
        else
        {
            txt_quantity.ReadOnly = true;
        }

        ddl_emp.SelectedValue = "Select";
        ddl_uniformsize.Text = "0";
        ddl_apron.Text = "0";
        ddl_shoosesize.Text = "0";
        txt_desc.Text = "";
        txt_description.Text = "";
        txt_hsn.Text = "";
        // txt_designation.SelectedValue = "0";
        txt_per_unit.Text = "";
        txt_quantity.Text = "0";
        txt_rate.Text = "0";
        txt_discount_rate.Text = "0";
        txt_discount_price.Text = "0";

       //txt_amount.Text = "0";

        lbl_qty.Visible = false;
        //   txt_quantity1.Visible = false; vikas



    }
    protected void btn_btn_clear(object sender, EventArgs e)
    {
        // txt_docno.Text = "";
        txt_docdate.Text = "";
        ddl_customerlist.SelectedValue = "Select";
        // get_client_contact(ddl_customerlist.SelectedValue.ToString());
        txt_narration.Text = "";
        txt_customer_gst.Text = "";
        txt_expiry_date.Text = "";
        txt_bill_add.Text = "";
        txt_ship_add.Text = "";
        ddl_sales_person.Text = "";
        txt_referenceno2.Text = "";
        txt_tot_discount_percent.Text = "0";
        txt_tot_discount_amt.Text = "0";
        txt_taxable_amt.Text = "0";
        txt_sub_total1.Text = "0";
        txt_extra_chrgs.Text = "0";
        txt_extra_chrgs_amt.Text = "0";
        txt_extra_chrgs_tax.Text = "0";
        txt_extra_chrgs_tax_amt.Text = "0";
        txt_sub_total2.Text = "0";
        txt_final_total.Text = "0";
        txt_customer_notes.Text = "";
        txt_terms_conditions.Text = "";
        txt_p_o_no.Text = "";
        ddl_transport.SelectedValue = "Select";
        txt_freight.Text = "";
        txt_vehicle.Text = "";
        txt_sales_mobile_no.Text = "";
        txt_grossamt.Text = "0";
        txt_year.Text = "";
        ddlcalmonth.Text = "Select";

        ddlcategory.Text = "Select";
        // client_state();
        ddl_state.SelectedValue = "Select";

        //ddlunitselect.SelectedValue = "";
        gv_itemslist.Visible = false;
    }
    protected void client_code()
    {
        ddl_customerlist.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(`client_code`),`client_name` FROM `pay_client_master`  WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "'  order by `client_code`", client.con);
        client.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_customerlist.DataSource = dt_item;
                ddl_customerlist.DataTextField = dt_item.Columns[1].ToString();
                ddl_customerlist.DataValueField = dt_item.Columns[0].ToString();
                ddl_customerlist.DataBind();
            }
            dt_item.Dispose();

            client.con.Close();
            ddl_customerlist.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            client.con.Close();
        }
    }

    protected void ddlbeabchwise1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee_termination(null, null);
    }

    protected void ddl_emp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (ddl_emp.SelectedValue == "Select")
            {
                txt_quantity.ReadOnly = false;
            }
            else
            {
                txt_quantity.ReadOnly = true;
            }
            ddl_uniformsize.Text = "0";
            txt_quantity.Text = "0";
            ddl_shoosesize.Text = "0";
            ddl_pantry_size.Text = "0";
            ddl_apron.Text = "0";
            d.con.Open();
            if (ddl_product.SelectedValue == "Uniform")
            {
                MySqlCommand dt_item = new MySqlCommand("select size,No_of_set from pay_document_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + ddl_emp.SelectedValue + "' AND `document_type`='Uniform'", d.con);

                MySqlDataReader dr = dt_item.ExecuteReader();


                if (dr.Read())
                {
                    ddl_uniformsize.ReadOnly = true;
                    //  txt_quantity.ReadOnly = true;
                    ddl_uniformsize.Text = dr.GetValue(0).ToString();
                    txt_quantity.Text = dr.GetValue(1).ToString();
                    d.con.Close();
                    dr.Dispose();

                }
                txt_particular.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' and `size`='" + ddl_uniformsize.Text + "' ORDER BY ITEM_NAME", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                int i = 0;

                while (dr1.Read())
                {
                    txt_particular.Items.Insert(i++, new ListItem(dr1[0].ToString(), dr1[1].ToString()));

                }
                dr1.Dispose();
                d.con.Close();
                txt_particular.Items.Insert(0, new ListItem("Select", "0"));


            }
            else if (ddl_product.SelectedValue == "pantry_jacket")
            {

                MySqlCommand dt_item = new MySqlCommand("select size,No_of_set from pay_document_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + ddl_emp.SelectedValue + "' AND `document_type`='pantry_jacket'", d.con);

                MySqlDataReader dr = dt_item.ExecuteReader();


                if (dr.Read())
                {
                    ddl_pantry_size.ReadOnly = true;
                    // txt_quantity.ReadOnly = true;
                    ddl_pantry_size.Text = dr.GetValue(0).ToString();
                    txt_quantity.Text = dr.GetValue(1).ToString();
                    d.con.Close();
                    dr.Dispose();

                }

                txt_particular.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' and `size`='" + ddl_pantry_size.Text + "' ORDER BY ITEM_NAME", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                int i = 0;

                while (dr1.Read())
                {
                    txt_particular.Items.Insert(i++, new ListItem(dr1[0].ToString(), dr1[1].ToString()));

                }
                dr1.Dispose();
                d.con.Close();
                txt_particular.Items.Insert(0, new ListItem("Select", "0"));
            }
            //add apron 

            else if (ddl_product.SelectedValue == "Apron")
            {

                MySqlCommand dt_item = new MySqlCommand("select size,No_of_set from pay_document_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + ddl_emp.SelectedValue + "' AND `document_type`='Apron'", d.con);

                MySqlDataReader dr = dt_item.ExecuteReader();


                if (dr.Read())
                {
                    ddl_apron.ReadOnly = true;
                    // txt_quantity.ReadOnly = true;
                    ddl_apron.Text = dr.GetValue(0).ToString();
                    txt_quantity.Text = dr.GetValue(1).ToString();
                    d.con.Close();
                    dr.Dispose();

                }

                txt_particular.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' and `size`='" + ddl_apron.Text + "' ORDER BY ITEM_NAME", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                int i = 0;

                while (dr1.Read())
                {
                    txt_particular.Items.Insert(i++, new ListItem(dr1[0].ToString(), dr1[1].ToString()));

                }
                dr1.Dispose();
                d.con.Close();
                txt_particular.Items.Insert(0, new ListItem("Select", "0"));
            }
// add id_card
            else if (ddl_product.SelectedValue == "ID_Card")
            {

                MySqlCommand dt_item = new MySqlCommand("select No_of_set from pay_document_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + ddl_emp.SelectedValue + "' AND `document_type`='ID_Card'", d.con);

                MySqlDataReader dr = dt_item.ExecuteReader();


                if (dr.Read())
                {
                  
                    txt_quantity.Text = dr.GetValue(0).ToString();
                    d.con.Close();
                    dr.Dispose();

                }

                txt_particular.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' ' ORDER BY ITEM_NAME", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                int i = 0;

                while (dr1.Read())
                {
                    txt_particular.Items.Insert(i++, new ListItem(dr1[0].ToString(), dr1[1].ToString()));

                }
                dr1.Dispose();
                d.con.Close();
                txt_particular.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {


                MySqlCommand dt_item = new MySqlCommand("select size,No_of_set from pay_document_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + ddl_emp.SelectedValue + "' AND `document_type`='Shoes'", d.con);

                MySqlDataReader dr = dt_item.ExecuteReader();


                if (dr.Read())
                {
                    ddl_shoosesize.ReadOnly = true;
                    // txt_quantity.ReadOnly = true;
                    ddl_shoosesize.Text = dr.GetValue(0).ToString();
                    txt_quantity.Text = dr.GetValue(1).ToString();
                    d.con.Close();
                    dr.Dispose();

                }

                txt_particular.Items.Clear();
                MySqlCommand cmd1 = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_product.SelectedValue + "' and `size`='" + ddl_shoosesize.Text + "' ORDER BY ITEM_NAME", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                int i = 0;

                while (dr1.Read())
                {
                    txt_particular.Items.Insert(i++, new ListItem(dr1[0].ToString(), dr1[1].ToString()));

                }
                dr1.Dispose();
                d.con.Close();
                txt_particular.Items.Insert(0, new ListItem("Select", "0"));
            }
            txt_quantity_TextChanged(null, null);
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }


    protected void ddl_client_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        ddl_state1.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state1.DataSource = dt_item;
                ddl_state1.DataTextField = dt_item.Columns[0].ToString();
                ddl_state1.DataValueField = dt_item.Columns[0].ToString();
                ddl_state1.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state1.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }



    }
    protected void ddl_state1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branch_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state1.SelectedValue + "'   ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_name.DataSource = dt_item;
                ddl_branch_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_name.DataValueField = dt_item.Columns[1].ToString();

                ddl_branch_name.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_branch_name.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void count()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT `client_name` as client_code,STATE_NAME, `UNIT_NAME` as unit_code FROm  `pay_document_details` INNER JOIN `pay_client_master` ON `pay_document_details`.`client_code` = `pay_client_master`.`client_code`    INNER JOIN `pay_unit_master` ON `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code`WHERE  `pay_document_details`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' and dispatch_flag='0'  group by unit_name ,client_name", d3.con);

        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT  `emp_code`,pay_document_details. unit_code,`unit_name`,`STATE_NAME`, `CLIENT_NAME` as client_code  FROM `pay_document_details` INNER JOIN `pay_unit_master` ON `pay_document_details`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code` inner join pay_client_master on  `pay_document_details`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_document_details`.`client_code` = `pay_client_master`.`client_code` WHERE pay_document_details.`comp_code` =  '" + Session["COMP_CODE"].ToString() + "'  AND `dispatch_flag` !=  '1'  group by  pay_document_details. unit_code and client_active_close='0'", d3.con);
        d3.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                gv_idcard.DataSource = dt_item;
                gv_idcard.DataBind();

            }
            dt_item.Dispose();
            d3.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
        }
    }

    protected void ddl_branch_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  pay_document_details.emp_code as 'emp_code1',  pay_unit_master.`UNIT_NAME` as 'unit_code',`pay_employee_master`.`emp_name` as 'emp_code',(SELECT `document_type` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'ID_Card' ) AS 'ID_CARD',(SELECT `document_type` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Uniform' ) AS 'UNIFORM', (SELECT `document_type` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Shoes') AS 'Shoes',(SELECT `document_type` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Apron' ) AS 'Apron',(SELECT `document_type` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Pantry_Jacket') AS 'Pantry_Jacket',(SELECT `No_of_set` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'ID_Card' ) AS 'ID_CARD_set',(SELECT `No_of_set` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Uniform' ) AS 'UNIFORM_set',(SELECT `No_of_set` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Shoes' ) AS 'Shoes_set',(SELECT `No_of_set` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Apron' ) AS 'Apron_set',(SELECT `No_of_set` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Pantry_Jacket' ) AS 'Pantry_Jacket_set',(SELECT `size` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Shoes' ) AS 'Shoes_size',(SELECT `size` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Uniform' ) AS 'UNIFORM_size',(SELECT `size` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Pantry_Jacket' ) AS 'Pantry_Jacket_size',(SELECT  case  `size`  when 'Select' then '' else size end as 'size' FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'ID_Card' ) AS 'ID_Card_size',(SELECT `size` FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Apron' ) AS 'Apron_size',(SELECT CASE `dispatch_flag`WHEN '1' THEN 'dispatch' WHEN '0' THEN 'New Request' WHEN '2' THEN 'Hold' ELSE `dispatch_flag` END FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'ID_Card') AS 'dispatch_flag1' ,(SELECT CASE `dispatch_flag`WHEN '1' THEN 'dispatch' WHEN '0' THEN 'New Request' WHEN '2' THEN 'Hold' ELSE `dispatch_flag` END FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Uniform') AS 'dispatch_flag2',(SELECT CASE `dispatch_flag`WHEN '1' THEN 'dispatch' WHEN '0' THEN 'New Request' WHEN '2' THEN 'Hold' ELSE `dispatch_flag` END FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Shoes') AS 'dispatch_flag3' ,(SELECT CASE `dispatch_flag`WHEN '1' THEN 'dispatch' WHEN '0' THEN 'New Request' WHEN '2' THEN 'Hold' ELSE `dispatch_flag` END FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Apron') AS 'dispatch_flag4',(SELECT CASE `dispatch_flag`WHEN '1' THEN 'dispatch' WHEN '0' THEN 'New Request' WHEN '2' THEN 'Hold' ELSE `dispatch_flag` END FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Pantry_Jacket') AS 'dispatch_flag5',(SELECT comment FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'ID_Card' and dispatch_flag='2' ) AS 'comment1',(SELECT comment FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Uniform' and dispatch_flag='2' ) AS 'comment2',(SELECT comment FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Shoes' and dispatch_flag='2' ) AS 'comment3',(SELECT comment FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Apron' and dispatch_flag='2' ) AS 'comment5',(SELECT comment FROM `pay_document_details` WHERE `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` AND `document_type` = 'Pantry_Jacket' and dispatch_flag='2') AS 'comment4' FROM `pay_document_details` INNER JOIN `pay_employee_master` ON `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` inner join  pay_unit_master  on pay_document_details.unit_code= pay_unit_master.unit_code and pay_document_details.comp_code= pay_unit_master.comp_code WHERE  `pay_document_details`.`client_code` = '" + ddl_client_name.SelectedValue + "' and  `pay_document_details`.`unit_code` = '" + ddl_branch_name.SelectedValue + "'  GROUP BY `emp_code`", d3.con);
        d3.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                gv_details.DataSource = dt_item;
                gv_details.DataBind();
                Panel7.Visible = false;
                Panel10.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record !!!')", true);
                dt_item.Clear();
                Panel7.Visible = true;
                Panel10.Visible = false;
            }

            dt_item.Dispose();
            d3.con.Close();
            //   Panel7.Visible = false;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
            hidtab.Value = "0";
        }
    }
    protected void lnk_uniform_Click(object sender, EventArgs e)
    {
        //   Button btn = (Button)sender;fill_txt_particular
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;

        string emplyee_id = row.Cells[0].Text;
        try
        {
           
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_document_details`.`client_code`,`STATE_NAME`,`pay_unit_master`.`unit_code`,`document_type`,item_code,`No_of_set`,  `emp_code`, pay_document_details. `size`, PAY_ITEM_MASTER.`item_description`,`unit` FROM `pay_document_details` INNER JOIN `pay_unit_master` ON `pay_document_details`.`client_code` = `pay_unit_master`.`client_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code`  INNER JOIN PAY_ITEM_MASTER ON `pay_document_details`.comp_code = `PAY_ITEM_MASTER`.comp_code AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` AND `pay_document_details`.`size` = `PAY_ITEM_MASTER`.`size` where document_type='Uniform' and emp_code ='" + emplyee_id + "'", d_sum.con);
            d_sum.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code();
                ddl_customerlist.SelectedValue = dr.GetValue(0).ToString();
                branch_fill();
                ddl_state.SelectedValue = dr.GetValue(1).ToString();

                state();
                ddlunitselect.SelectedValue = dr.GetValue(2).ToString();

                ddl_employee_termination(null, null);
                ddl_product.SelectedValue = dr.GetValue(3).ToString();
                fill_txt_particular(null, null);
                txt_particular.SelectedValue = dr.GetValue(4).ToString();
            
                txt_quantity.Text = dr.GetValue(5).ToString();
               
                ddl_emp.SelectedValue = dr.GetValue(6).ToString();
                ddl_uniformsize.Text = dr.GetValue(7).ToString();
                particular_hsn_sac_code(null, null);
                txt_desc.Text = dr.GetValue(8).ToString();
                txt_designation.SelectedValue = dr.GetValue(9).ToString();
            }
       
            txt_quantity_TextChanged(null, null);

            d_sum.con.Close();


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_sum.con.Close();
            hidtab.Value = "1";
        }
    }
    protected void lnk_idcard_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;

        string emplyee_id = row.Cells[0].Text;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select pay_document_details.client_code,STATE_NAME,pay_unit_master.unit_code,document_type,No_of_set,emp_code,item_code,PAY_ITEM_MASTER.`item_description`,PAY_ITEM_MASTER.`VAT`,`unit` from pay_document_details  inner join pay_unit_master on pay_document_details.client_code=pay_unit_master.client_code and  pay_document_details.unit_code=pay_unit_master.unit_code INNER JOIN `PAY_ITEM_MASTER` ON `pay_document_details`.`comp_code` = `PAY_ITEM_MASTER`.`comp_code` AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` where document_type='ID_Card' and emp_code ='" + emplyee_id + "'", d_sum.con);
            d_sum.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code();
                ddl_customerlist.SelectedValue = dr.GetValue(0).ToString();
                branch_fill();
                ddl_state.SelectedValue = dr.GetValue(1).ToString();

                state();
                ddlunitselect.SelectedValue = dr.GetValue(2).ToString();
                ddl_employee_termination(null, null);
                ddl_product.SelectedValue = dr.GetValue(3).ToString();
                fill_txt_particular(null, null);
                txt_quantity.Text = dr.GetValue(4).ToString();
                ddl_emp.SelectedValue = dr.GetValue(5).ToString();
                txt_particular.SelectedValue = dr.GetValue(6).ToString();
                particular_hsn_sac_code(null, null);
                txt_desc.Text = dr.GetValue(7).ToString();
                txt_designation.SelectedValue = dr.GetValue(9).ToString();


            }
            txt_quantity_TextChanged(null, null);

            d_sum.con.Close();


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_sum.con.Close();
            hidtab.Value = "1";
        }

    }
    protected void lnk_shoes_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;

        string emplyee_id = row.Cells[0].Text;
        try
        {
           // MySqlCommand cmd = new MySqlCommand("select pay_document_details.client_code,STATE_NAME,pay_unit_master.unit_code,document_type,No_of_set,emp_code,pay_document_details.size,`item_code`,`PAY_ITEM_MASTER`.`item_description`,`PAY_ITEM_MASTER`.`unit` from pay_document_details  inner join pay_unit_master on pay_document_details.client_code=pay_unit_master.client_code and  pay_document_details.unit_code=pay_unit_master.unit_code  INNER JOIN `PAY_ITEM_MASTER` ON `pay_document_details`.`comp_code` = `PAY_ITEM_MASTER`.`comp_code` AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` AND `pay_document_details`.`size` = `PAY_ITEM_MASTER`.`size` where document_type='Shoes' and emp_code ='" + emplyee_id + "' GROUP BY  `emp_code`", d_sum.con);

            MySqlCommand cmd = new MySqlCommand("SELECT `pay_document_details`.`client_code`,`STATE_NAME`,`pay_unit_master`.`unit_code`,`document_type`,item_code,`No_of_set`,  `emp_code`, pay_document_details. `size`, PAY_ITEM_MASTER.`item_description`,`unit` FROM `pay_document_details` INNER JOIN `pay_unit_master` ON `pay_document_details`.`client_code` = `pay_unit_master`.`client_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code`  INNER JOIN PAY_ITEM_MASTER ON `pay_document_details`.comp_code = `PAY_ITEM_MASTER`.comp_code AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` AND `pay_document_details`.`size` = `PAY_ITEM_MASTER`.`size` where document_type='Shoes' and emp_code ='" + emplyee_id + "'", d_sum.con);
            d_sum.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code();
                ddl_customerlist.SelectedValue = dr.GetValue(0).ToString();
                branch_fill();
                ddl_state.SelectedValue = dr.GetValue(1).ToString();

                state();
                ddlunitselect.SelectedValue = dr.GetValue(2).ToString();

                ddl_employee_termination(null, null);
                ddl_product.SelectedValue = dr.GetValue(3).ToString();
                fill_txt_particular(null, null);
                txt_particular.SelectedValue = dr.GetValue(4).ToString();

                txt_quantity.Text = dr.GetValue(5).ToString();
                ddl_emp.SelectedValue = dr.GetValue(6).ToString();
                ddl_shoosesize.Text = dr.GetValue(7).ToString();
                particular_hsn_sac_code(null, null);
                txt_desc.Text = dr.GetValue(8).ToString();
                txt_designation.SelectedValue = dr.GetValue(9).ToString();
 


            }
            else
            {
                //  txt_quantity_TextChanged(null,null);
                txt_quantity_TextChanged(null, null);
            }
            d_sum.con.Close();
            txt_quantity_TextChanged(null, null);

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_sum.con.Close();
            hidtab.Value = "1";
        }
    }
    protected void lnk_pantry_j_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
        try
        {
        //    MySqlCommand cmd = new MySqlCommand("select pay_document_details.client_code,STATE_NAME,pay_unit_master.unit_code,document_type,No_of_set,emp_code,size from pay_document_details  inner join pay_unit_master on pay_document_details.client_code=pay_unit_master.client_code and  pay_document_details.unit_code=pay_unit_master.unit_code where document_type='Pantry_Jacket' and emp_code ='" + emplyee_id + "'", d_sum.con);
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_document_details`.`client_code`,`STATE_NAME`,`pay_unit_master`.`unit_code`,`document_type`,item_code,`No_of_set`,  `emp_code`, pay_document_details. `size`, PAY_ITEM_MASTER.`item_description`,`unit` FROM `pay_document_details` INNER JOIN `pay_unit_master` ON `pay_document_details`.`client_code` = `pay_unit_master`.`client_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code`  INNER JOIN PAY_ITEM_MASTER ON `pay_document_details`.comp_code = `PAY_ITEM_MASTER`.comp_code AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` AND `pay_document_details`.`size` = `PAY_ITEM_MASTER`.`size` where document_type='Pantry_Jacket' and emp_code ='" + emplyee_id + "'", d_sum.con);
            
            d_sum.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code();
                ddl_customerlist.SelectedValue = dr.GetValue(0).ToString();
                branch_fill();
                ddl_state.SelectedValue = dr.GetValue(1).ToString();

                state();
                ddlunitselect.SelectedValue = dr.GetValue(2).ToString();

                ddl_employee_termination(null, null);
                ddl_product.SelectedValue = dr.GetValue(3).ToString();
                fill_txt_particular(null, null);
                txt_particular.SelectedValue = dr.GetValue(4).ToString();

                txt_quantity.Text = dr.GetValue(5).ToString();
                ddl_emp.SelectedValue = dr.GetValue(6).ToString();
                ddl_pantry_size.Text = dr.GetValue(7).ToString();
                particular_hsn_sac_code(null, null);
                txt_desc.Text = dr.GetValue(8).ToString();
                txt_designation.SelectedValue = dr.GetValue(9).ToString();

            }
           
            txt_quantity_TextChanged(null, null);

            d_sum.con.Close();


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_sum.con.Close();
            hidtab.Value = "1";
        }
    }
    protected void lnk_apron_Click(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
        try
        {
          //  MySqlCommand cmd = new MySqlCommand("select pay_document_details.client_code,STATE_NAME,pay_unit_master.unit_code,document_type,No_of_set,emp_code,size from pay_document_details  inner join pay_unit_master on pay_document_details.client_code=pay_unit_master.client_code and  pay_document_details.unit_code=pay_unit_master.unit_code where document_type='Apron' and emp_code ='" + emplyee_id + "'", d_sum.con);
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_document_details`.`client_code`,`STATE_NAME`,`pay_unit_master`.`unit_code`,`document_type`,item_code,`No_of_set`,  `emp_code`, pay_document_details. `size`, PAY_ITEM_MASTER.`item_description`,`unit` FROM `pay_document_details` INNER JOIN `pay_unit_master` ON `pay_document_details`.`client_code` = `pay_unit_master`.`client_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code`  INNER JOIN PAY_ITEM_MASTER ON `pay_document_details`.comp_code = `PAY_ITEM_MASTER`.comp_code AND `pay_document_details`.`document_type` = `PAY_ITEM_MASTER`.`product_service` AND `pay_document_details`.`size` = `PAY_ITEM_MASTER`.`size` where document_type='Apron' and emp_code ='" + emplyee_id + "'", d_sum.con);
            d_sum.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code();
                ddl_customerlist.SelectedValue = dr.GetValue(0).ToString();
                branch_fill();
                ddl_state.SelectedValue = dr.GetValue(1).ToString();

                state();
                ddlunitselect.SelectedValue = dr.GetValue(2).ToString();

                ddl_employee_termination(null, null);
                ddl_product.SelectedValue = dr.GetValue(3).ToString();
                fill_txt_particular(null, null);
                txt_particular.SelectedValue = dr.GetValue(4).ToString();

                txt_quantity.Text = dr.GetValue(5).ToString();
                ddl_emp.SelectedValue = dr.GetValue(6).ToString();
                ddl_apron.Text = dr.GetValue(7).ToString();
                particular_hsn_sac_code(null, null);
                txt_desc.Text = dr.GetValue(8).ToString();
                txt_designation.SelectedValue = dr.GetValue(9).ToString();

            }
            //  txt_quantity_TextChanged(null,null);
            txt_quantity_TextChanged(null, null);

            d_sum.con.Close();


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_sum.con.Close();
            hidtab.Value = "1";
        }


    }

    protected void lnk_idcard_hold_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        

        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
        txt_emp_id.Text = emplyee_id;
        txt_particul.Text = "ID_Card";
        string falg = d.getsinglestring("SELECT dispatch_flag FROM `pay_document_details` WHERE `emp_code` = '" + emplyee_id + "' AND `document_type` = 'ID_Card'");
        if (falg == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('ID Card Alredy Diapatch');", true);
        }
        else
        {
            reson.Visible = true;
            hold_materl.Visible = true;
            txt_comment.Enabled = true;
            txt_comment.Text = "";
            hold_material.Visible = true;
        }
    }
    protected void lnk_uniform_hold_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
     

        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
        txt_emp_id.Text = emplyee_id;
        txt_particul.Text = "Uniform";
        string falg = d.getsinglestring("SELECT dispatch_flag FROM `pay_document_details` WHERE `emp_code` = '" + emplyee_id + "' AND `document_type` = 'Uniform'");
        if (falg == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Uniform Alredy Diapatch');", true);
        }
        else
        {
            txt_comment.Visible = true;
            txt_comment.Text = "";
            reson.Visible = true;
            hold_materl.Visible = true;
        }
    }
    protected void lnk_shoes_hold_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
       

        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;


        txt_emp_id.Text = emplyee_id;
        txt_particul.Text = "Shoes";
        string falg = d.getsinglestring("SELECT dispatch_flag FROM `pay_document_details` WHERE `emp_code` = '" + emplyee_id + "' AND `document_type` = 'Shoes'");
        if (falg == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Shoes Alredy Diapatch');", true);
        }
        else
        {
            txt_comment.Visible = true;
            txt_comment.Text = "";
            reson.Visible = true;
            hold_materl.Visible = true;
        }

    }
    protected void lnk_pantry_j_hold_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
       

        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
         txt_emp_id.Text = emplyee_id;
        txt_particul.Text = "Pantry_Jacket";
        string falg = d.getsinglestring("SELECT dispatch_flag FROM `pay_document_details` WHERE `emp_code` = '" + emplyee_id + "' AND `document_type` = 'Pantry_Jacket'");
        if (falg == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pantry_Jacket Alredy Diapatch');", true);
        }
        else
        {
            txt_comment.Visible = true;
            reson.Visible = true;
            txt_comment.Text = "";
            hold_materl.Visible = true;
        }
    }
    protected void lnk_apron_hold_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        txt_comment.Visible = true;

        reson.Visible = true;
        hold_materl.Visible = true;
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string emplyee_id = row.Cells[0].Text;
        txt_emp_id.Text = emplyee_id;
        txt_particul.Text = "Apron";
        string falg = d.getsinglestring("SELECT dispatch_flag FROM `pay_document_details` WHERE `emp_code` = '" + emplyee_id + "' AND `document_type` = 'Apron'");
        if (falg == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Apron Alredy Diapatch');", true);
        }
        else
        {
            txt_comment.Visible = true;
            reson.Visible = true;
            txt_comment.Text = "";
            hold_materl.Visible = true;
        }


    }


    protected void state()
    {

        ddlunitselect.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select  UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_customerlist.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "'  and UNIT_CODE  in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_customerlist.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND branch_status = 0 ORDER BY UNIT_CODE", client.con);
        client.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddlunitselect.DataSource = dt_item;
                ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                ddlunitselect.DataValueField = dt_item.Columns[1].ToString();

                ddlunitselect.DataBind();
            }
            dt_item.Dispose();
            client.con.Close();
            ddlunitselect.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            client.con.Close();
        }

        if (ddl_customerlist.SelectedItem.ToString() != "Select")
        {

            get_client_contact(ddl_customerlist.SelectedItem.ToString());
        }
        else
        {

            txt_customer_gst.Text = "";
            txt_customer_gst.ReadOnly = false;
        }
    }

    protected void gv_details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["ID_CARD_set"].ToString() == "" )
            {
                LinkButton lb1 = e.Row.FindControl("lnk_idcard") as LinkButton;
                lb1.Visible = false;
                LinkButton lb11 = e.Row.FindControl("lnk_idcard_hold") as LinkButton;
                lb11.Visible = false;
            }
            if(dr["UNIFORM_set"].ToString() == "")
            {
                LinkButton lb2 = e.Row.FindControl("lnk_uniform") as LinkButton;
                lb2.Visible = false;
                LinkButton lb12 = e.Row.FindControl("lnk_uniform_hold") as LinkButton;
                lb12.Visible = false;
            }
             if (dr["Shoes_set"].ToString() == "")
            {
                LinkButton lb3 = e.Row.FindControl("lnk_shoes") as LinkButton;
                lb3.Visible = false;
                LinkButton lb13 = e.Row.FindControl("lnk_shoes_hold") as LinkButton;
                lb13.Visible = false;
            }
             if (dr["Pantry_Jacket_set"].ToString() == "")
            {
                LinkButton lb4 = e.Row.FindControl("lnk_pantry_j") as LinkButton;
                lb4.Visible = false;
                LinkButton lb14 = e.Row.FindControl("lnk_pantry_j_hold") as LinkButton;
                lb14.Visible = false;
            }
             if (dr["Apron_set"].ToString() == "")
            {
                LinkButton lb5 = e.Row.FindControl("lnk_apron") as LinkButton;
                lb5.Visible = false;
                LinkButton lb15 = e.Row.FindControl("lnk_apron_hold") as LinkButton;
                lb15.Visible = false;
            }
             
        }

    }
    protected void hold_material_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        string flag = d.getsinglestring("select `dispatch_flag` from pay_document_details where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_emp_id.Text + "' and `document_type`='" + txt_particul.Text + "' ");
        if (flag == "0")
        {
            d.operation("update pay_document_details set dispatch_flag='2',comment='" + txt_comment.Text + "' where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_emp_id.Text + "' and `document_type`='" + txt_particul.Text + "' ");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Material Hold Successfully ')", true);
        }
        else if (flag == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Material Disptch ')", true);
        }
        else if (flag == "2")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Material Hold ')", true);
        }
        ddl_branch_name_SelectedIndexChanged(null, null);
    }

    protected void branch_fill()
    {

        if (ddl_customerlist.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_customerlist.SelectedValue + "'  order by 1", client.con);
            client.con.Open();
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
                client.con.Close();
                ddl_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                client.con.Close();
            }

        }
    }


    protected void gv_idcard_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_idcard.UseAccessibleHeader = false;
            gv_idcard.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_bynumber_name_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hidtab.Value = "1";
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

        //Label lbl_doccode = (Label)gv_bynumber_name.Rows[e.RowIndex].FindControl("lbl_docnumber");
        //string lbl_doccode = gv_bynumber_name.Rows[e.RowIndex].Cells[0];
         //string lbl_doccode = row.Cells[0].Text;
       // string doc_no = lbl_doccode.Text;
        //Label lbl_doccode = (Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
        //string doc_no = lbl_doccode.Text;
        lbl_print_quote.Text = doc_no;

        string employee_name = d.getsinglestring("select `emp_name` from pay_transaction_details where `DOC_NO`='" + doc_no + "'");



        MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),`CUST_CODE`,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO , SALES_MOBILE_NO,BILL_YEAR,BILL_MONTH,Category,state,branch_name FROM pay_transaction WHERE (DOC_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_docno.Text = dr2[0].ToString();
                txt_docdate.Text = dr2[1].ToString();
                if (employee_name == "Select")
                {
                    ddl_customerlist.SelectedValue = dr2[2].ToString();
                }
                else
                {
                    client_code();
                    ddl_customerlist.SelectedValue = dr2[2].ToString();
                }
                get_client_contact(ddl_customerlist.SelectedValue.ToString());
                txt_narration.Text = dr2[3].ToString();
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
                txt_p_o_no.Text = dr2["p_o_no"].ToString();
                ddl_transport.SelectedValue = dr2["TRANSPORT"].ToString();
                txt_freight.Text = dr2["FREIGHT"].ToString();
                txt_vehicle.Text = dr2["VEHICLE_NO"].ToString();
                txt_sales_mobile_no.Text = dr2["SALES_MOBILE_NO"].ToString();
                txt_grossamt.Text = dr2["GROSS_AMOUNT"].ToString();
                txt_year.Text = dr2["BILL_YEAR"].ToString();
                ddlcalmonth.Text = dr2["BILL_MONTH"].ToString();

                ddlcategory.SelectedValue = dr2["Category"].ToString();
                if (employee_name == "Select")
                {
                    client_state();
                    ddl_state.SelectedValue = dr2["state"].ToString();
                    ddl_state_SelectedIndexChanged(null, null);
                    ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
                }
                else
                {
                    branch_fill();
                    ddl_state.SelectedValue = dr2["state"].ToString();
                    state();
                    ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
                }

            }
            dr2.Close();
            d.con.Close();
            gst_counter(txt_customer_gst.Text.Substring(0, 2));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            tooltrip();
            ddl_employee_termination(null, null);
        }


        // MySqlCommand cmd1 = new MySqlCommand("SELECT PARTICULAR,DESCRIPTION,hsn_code,sac_code, DESIGNATION, ROUND(QUANTITY,6) AS QUANTITY, ROUND(RATE,6) AS RATE, ROUND(AMOUNT,2) AS AMOUNT FROM pay_transactionQ_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);

        MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,item_type,PARTICULAR,emp_name,size_uniform,size_shoes,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,DATE_FORMAT(START_DATE,'%d/%m/%Y') As START_DATE,DATE_FORMAT(END_DATE,'%d/%m/%Y') As END_DATE,VENDOR,emp_code,POD_NUM,size_pantry,size_apron FROM pay_transaction_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
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
    protected void lnk_button_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        Panel6.Visible = true;
        Panel4.Visible = true;
        Panel8.Visible = true;
        btn_Save.Visible = false;

        gv_itemslist.Visible = true;
        btn_update.Visible = true;
        btn_delete.Visible = true;

       


        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string lbl_doccode = row.Cells[0].Text;

       // Label lbl_doccode = (Label)gv_bynumber_name.SelectedRow.FindControl("lbl_docnumber");
        string doc_no = lbl_doccode;
        lbl_print_quote.Text = doc_no;

       
        lbl_print_quote.Text = doc_no;

        string employee_name = d.getsinglestring("select `emp_name` from pay_transaction_details where `DOC_NO`='" + doc_no + "'");



        MySqlCommand cmd2 = new MySqlCommand("select DOC_NO,DATE_FORMAT(DOC_DATE,'%d/%m/%Y'),`CUST_CODE`,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,customer_gst_no,DATE_FORMAT(EXPIRY_DATE,'%d/%m/%Y') As EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,CUSTOMER_NOTES,TERMS_CONDITIONS,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO , SALES_MOBILE_NO,BILL_YEAR,BILL_MONTH,Category,state,branch_name FROM pay_transaction WHERE (DOC_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_docno.Text = dr2[0].ToString();
                txt_docdate.Text = dr2[1].ToString();
                if (employee_name == "Select")
                {
                    ddl_customerlist.SelectedValue = dr2[2].ToString();
                }
                else
                {
                    client_code();
                    ddl_customerlist.SelectedValue = dr2[2].ToString();
                }
                get_client_contact(ddl_customerlist.SelectedValue.ToString());
                txt_narration.Text = dr2[3].ToString();
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
                txt_p_o_no.Text = dr2["p_o_no"].ToString();
                ddl_transport.SelectedValue = dr2["TRANSPORT"].ToString();
                txt_freight.Text = dr2["FREIGHT"].ToString();
                txt_vehicle.Text = dr2["VEHICLE_NO"].ToString();
                txt_sales_mobile_no.Text = dr2["SALES_MOBILE_NO"].ToString();
                txt_grossamt.Text = dr2["GROSS_AMOUNT"].ToString();
                txt_year.Text = dr2["BILL_YEAR"].ToString();
                ddlcalmonth.Text = dr2["BILL_MONTH"].ToString();

                ddlcategory.SelectedValue = dr2["Category"].ToString();
                if (employee_name == "Select")
                {
                    client_state();
                    ddl_state.SelectedValue = dr2["state"].ToString();
                    ddl_state_SelectedIndexChanged(null, null);
                    ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
                }
                else
                {
                    branch_fill();
                    ddl_state.SelectedValue = dr2["state"].ToString();
                    state();
                    ddlunitselect.SelectedValue = dr2["branch_name"].ToString();
                }

            }
            dr2.Close();
            d.con.Close();
            gst_counter(txt_customer_gst.Text.Substring(0, 2));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            tooltrip();
            ddl_employee_termination(null, null);
        }


        // MySqlCommand cmd1 = new MySqlCommand("SELECT PARTICULAR,DESCRIPTION,hsn_code,sac_code, DESIGNATION, ROUND(QUANTITY,6) AS QUANTITY, ROUND(RATE,6) AS RATE, ROUND(AMOUNT,2) AS AMOUNT FROM pay_transactionQ_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);

        MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,item_type,PARTICULAR,emp_name,size_uniform,size_shoes,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,DATE_FORMAT(START_DATE,'%d/%m/%Y') As START_DATE,DATE_FORMAT(END_DATE,'%d/%m/%Y') As END_DATE,VENDOR,emp_code,POD_NUM,size_pantry,size_apron FROM pay_transaction_DETAILS WHERE (DOC_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
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
    protected void gv_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_details.UseAccessibleHeader = false;
            gv_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
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
}