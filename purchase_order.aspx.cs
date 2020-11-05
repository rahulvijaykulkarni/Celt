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

public partial class purchase_order : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d_sum = new DAL();
    DAL d1 = new DAL();
    DAL d3 = new DAL();
    TransactionBAL tbl = new TransactionBAL();
    double a = 0, b = 0, c = 0;

    System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
   
        
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
        //    btn_save_send.Visible = false;
            txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txt_docdate.Text = txt_docdate.Text;
        //    Session["DISCOUNT_BY"] = "RS";

        //    //   sales_person_list();
            btn_budget.Visible = false;
           btn_Save.Visible = true;
           btn_update.Visible = false;
           btn_delete.Visible = false;

           gendocno();

        //    txt_docno.Enabled = true;
        //    ddl_customerlist.Enabled = true;
        //    txt_docdate.Enabled = true;
        //    txt_narration.Enabled = true;

        //   gv_itemslist.Visible = false;
            btn_update.Visible = false;
            btn_delete.Visible = false;

          // btn_save_send.Visible = false;
            txt_docno.ReadOnly = true;

             ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' ORDER BY client_code", d.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
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
            catch (Exception ex)
            { throw ex; }
           
        }


    }

    protected void txt_docno_number_TextChanged(object sender, EventArgs e)
    {
        string doc_no = txt_docno.Text;
        if (doc_no != "")
        {
            txt_customername.Text = "";
        }
    }
    protected void txt_customername_TextChanged(object sender, EventArgs e)
    {
        string customername = txt_customername.Text;
        if (customername != "")
        {
            txt_docno.Text = "";
        }
    }
    protected void ddl_customerlist_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (ddl_vendortype.SelectedValue == "Regular")
        {
            ddl_customerlist.Items.Clear();
            MySqlCommand cmd_cust = new MySqlCommand("SELECT CONCAT(VEND_ID,' - ',VEND_NAME)  from PAY_VENDOR_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and ACTIVE_STATUS='A' ORDER BY VEND_ID", d1.con);

            d1.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_cust = cmd_cust.ExecuteReader();
                while (dr_cust.Read())
                {
                    ddl_customerlist.Items.Add(dr_cust[0].ToString());
                }
                dr_cust.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
                customer_details();
                //gst_counter(txt_customer_gst.Text.Substring(0, 2));
            }
           
           
            ddl_customerlist.Visible = true;
          

        }
        else
        {
           
            ddl_customerlist.Visible = false;
            txt_customer_gst.ReadOnly = false;
          



        }
    }



    protected void customer_details(object sender, EventArgs e)
    {


        if (ddl_customerlist.SelectedValue != "Select")
        {
            MySqlCommand cmd = new MySqlCommand("select GST from pay_vendor_master where vend_id='" + ddl_customerlist.SelectedItem.ToString().Substring(0, 4) + "'", d.con);
            d.con.Open();
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
                d.con.Close();
                customer_details();
                txt_customer_gst.ReadOnly = true;
                txt_address.ReadOnly = true;
             
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

            MySqlCommand cmd = new MySqlCommand("select GST,concat_ws('\n',txtbillattention,txtbilladdress) as 'Address' from pay_vendor_master where VEND_ID='" + ddl_customerlist.SelectedItem.ToString().Substring(0, 4) + "'", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_item = cmd.ExecuteReader();
                while (dr_item.Read())
                {
                    txt_customer_gst.Text = dr_item.GetValue(0).ToString();
                    //txt_bill_add.Text = dr_item.GetValue(1).ToString();
                    txt_address.Text = dr_item.GetValue(1).ToString();
                    //txt_bank_ac.Text = dr_item.GetValue(3).ToString();
                    //txt_bank_no.Text = dr_item.GetValue(4).ToString();
                    //txt_ifc_code.Text = dr_item.GetValue(5).ToString();
                    //txt_credit_perod.Text = dr_item.GetValue(6).ToString();
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        }

    protected void ddl_vendortype_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        if (ddl_vendortype.SelectedValue == "Regular")
        {
            ddl_customerlist.Items.Clear();
            DataTable dt = new DataTable();
            MySqlCommand cmd_cust = new MySqlCommand("SELECT VEND_ID,CONCAT(VEND_ID,' - ',VEND_NAME)  from PAY_VENDOR_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and ACTIVE_STATUS='A' and vendor_type = '" + ddl_vendor_categories.SelectedValue + "' ORDER BY VEND_ID", d1.con);

            d1.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataAdapter dr = new MySqlDataAdapter(cmd_cust);
                dr.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    ddl_customerlist.DataSource = dt;
                    ddl_customerlist.DataValueField = dt.Columns[0].ToString();
                    ddl_customerlist.DataTextField = dt.Columns[1].ToString();
                    ddl_customerlist.DataBind();
                    
                }
                dt.Dispose();
                ddl_customerlist.Items.Insert(0, "Select");
                //dr_cust.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
            
           
            ddl_customerlist.Visible = true;
           

        }
        else
        {

            txt_address.ReadOnly = false;
            
            ddl_customerlist.Visible = false;
            txt_customer_gst.ReadOnly = false;
          


        }
    }

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
    private void gendocno()
        {
            //  MySqlCommand cmd1 = new MySqlCommand("Select COUNT(*) from pay_transactionp  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND TASK_CODE='STP'", d.con);
            string series = d.getsinglestring("SELECT MAX(IFNULL(SUBSTRING( PO_NO ,3)+1, 0)) FROM pay_transaction_po  WHERE COMP_CODE  = '" + Session["COMP_CODE"].ToString() + "'  ORDER BY  PO_NO  DESC LIMIT 1");

          int total_count = series =="" ? 1:int.Parse(series);
        
            try
            {
              
                if (total_count >= 0 && total_count <= 9)
                {
                    string docno = "PO" + "0000" + total_count;
                    txt_docno.Text = docno.ToString();
                }
                if (total_count >= 10 && total_count <= 99)
                {
                    string docno = "PO" + "000" + total_count;
                    txt_docno.Text = docno.ToString();
                }
                if (total_count >= 100 && total_count <= 999)
                {
                    string docno = "PO" + "00" + total_count;
                    txt_docno.Text = docno.ToString();
                }
                if (total_count >= 1000 && total_count <= 9999)
                {
                    string docno = "PO" + "0" + total_count;
                    txt_docno.Text = docno.ToString();
                } 
                if (total_count >= 10000 && total_count <= 99999)
                {
                    string docno = "PO" + total_count.ToString();
                    txt_docno.Text = docno.ToString();
                }
                
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
         //gv_itemslist.DataSource = null;
        // gv_itemslist.DataBind();
         
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
       // dt.Columns.Add("Designation");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Rate");
        //dt.Columns.Add("DISCOUNT");
       // dt.Columns.Add("DISCOUNT_AMT");
        dt.Columns.Add("Amount");
        //dt.Columns.Add("START_DATE");
        //dt.Columns.Add("END_DATE");
        //dt.Columns.Add("VENDOR");
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

                //TextBox lbl_designation = (TextBox)gv_itemslist.Rows[rownum].Cells[11].FindControl("lbl_designation");
                //dr["Designation"] = lbl_designation.Text;
                TextBox lbl_quantity = (TextBox)gv_itemslist.Rows[rownum].Cells[11].FindControl("lbl_quantity");
                dr["Quantity"] = Convert.ToDouble(lbl_quantity.Text);
                TextBox lbl_rate = (TextBox)gv_itemslist.Rows[rownum].Cells[12].FindControl("lbl_rate");
                dr["Rate"] = Convert.ToDouble(lbl_rate.Text);
                //TextBox lbl_discount = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_discount");
                //dr["DISCOUNT"] = Convert.ToDouble(lbl_discount.Text);
                //TextBox lbl_discount_amt = (TextBox)gv_itemslist.Rows[rownum].Cells[15].FindControl("lbl_discount_amt");
                //dr["DISCOUNT_AMT"] = Convert.ToDouble(lbl_discount_amt.Text);
                TextBox lbl_amount = (TextBox)gv_itemslist.Rows[rownum].Cells[13].FindControl("lbl_amount");
                dr["Amount"] = Convert.ToDouble(lbl_amount.Text);
                //TextBox lbl_start_date = (TextBox)gv_itemslist.Rows[rownum].Cells[16].FindControl("lbl_start_date");
                //dr["START_DATE"] = (lbl_start_date.Text); vikas comment 22/11
                //TextBox lbl_end_date = (TextBox)gv_itemslist.Rows[rownum].Cells[18].FindControl("lbl_end_date");
                //dr["END_DATE"] = (lbl_end_date.Text);
                //TextBox lbl_vendor = (TextBox)gv_itemslist.Rows[rownum].Cells[19].FindControl("lbl_vendor");
                //dr["VENDOR"] = (lbl_vendor.Text);

                TextBox lbl_apronsize = (TextBox)gv_itemslist.Rows[rownum].Cells[14].FindControl("lbl_apronsize");
                dr["size_apron"] = lbl_apronsize.Text.ToString();

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["ITEM_CODE"] = ddl_item_name.SelectedValue.ToString();
        dr["item_type"] = ddl_item_type.SelectedValue.ToString();
        dr["Particular"] = ddl_item_name.SelectedItem.ToString();
        dr["size_uniform"] = ddl_uniform_size.SelectedItem.ToString();
        dr["size_shoes"] = ddl_shoes_size.SelectedItem.ToString();
        dr["size_pantry"] = ddl_pantry_jacket.SelectedItem.ToString();
        dr["DESCRIPTION"] = txt_description.Text.ToString();
        dr["VAT"] = txt_vat.Text.ToString();
        dr["HSN_Code"] = txt_hsn_code.Text.ToString();
        //dr["Designation"] = txt_designation.Text;
        dr["Quantity"] = txt_qty.Text;
        dr["Rate"] = txt_per_unit_rate.Text;
        //dr["DISCOUNT"] = txt_discount_rate.Text;
        //dr["DISCOUNT_AMT"] = txt_discount_price.Text;
        dr["Amount"] = txt_amount.Text;
        //dr["START_DATE"] = txt_start_date.Text;
        //dr["END_DATE"] = txt_end_date.Text;
        //dr["VENDOR"] = ddl_vendor.SelectedItem.Text.ToString();
        dr["size_apron"] = ddl_apron_size.SelectedItem.Text.ToString();
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        gst_counter(txt_customer_gst.Text.Substring(0, 2));
        discount_calculate(dt, 1);
        
        ViewState["CurrentTable"] = dt;
        //txt_rate.Text = "0";
        //txt_particular.SelectedIndex = 0;
        txt_per_unit_rate.Text = "0";
        //txt_designation.SelectedIndex = 0;
        txt_qty.Text = "0";
        txt_amount.Text = "0";
        txt_vat.Text = "0";
        //txt_desc.Text = "";
        ////txt_vendorname.Text = "";
        txt_description.Text = "0";
        txt_hsn_code.Text = "";
        //txt_igst.Text = "";
        //txt_description.Text = "0";
        //txt_discount_price.Text = "0";
        ddl_item_name.Focus();
        txt_per_unit_rate.Text = "0";
        //txt_discount_rate.Text = "0";
        ////txt_tot_discount_percent.Text = "0";
        ////  txt_end_date.Text = "";
        //ddl_vendor.SelectedIndex = 0;
        ////lbl_qty.Visible = false;
        //Panel4.Visible = true;
        //Panel2.Visible = true;
        //lbl_qty.Text = "";
        //lbl_rete1.Visible = false;
        //txt_quantity1.Visible = false;
        ddl_item_type.SelectedValue = "Select";
        ddl_uniform_size.SelectedValue = "Select";
        ddl_shoes_size.SelectedValue = "Select";
        ddl_item_name.Items.Clear();
       // ddl_item_name.SelectedValue = "Select";
        //lbl_rete1.Visible = false;
        //lbl_raten.Visible = false;
    }
    protected void size()
     {
         if (ddl_item_type.SelectedValue == "Uniform")
         {
             try
             {
                 MySqlCommand cmd = new MySqlCommand("SELECT distinct( size ) FROM  pay_item_master  WHERE  product_service  = 'Uniform' and  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'", d.con);
                 d.con.Open();
                 MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 dr.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     ddl_uniform_size.DataSource = dt;
                     ddl_uniform_size.DataTextField = dt.Columns[0].ToString();
                     ddl_uniform_size.DataBind();
                     ddl_uniform_size.Items.Insert(0, "Select");
                 }
                 d.con.Close();
             }
             catch (Exception ex)
             { }
             finally { d.con.Close(); }
         }
         else if (ddl_item_type.SelectedValue == "pantry_jacket")
         {
             try
             {
                 MySqlCommand cmd = new MySqlCommand("SELECT distinct( size ) FROM  pay_item_master  WHERE  product_service  = 'pantry_jacket' and  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'", d.con);
                 d.con.Open();
                 MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 dr.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     ddl_pantry_jacket.DataSource = dt;
                     ddl_pantry_jacket.DataTextField = dt.Columns[0].ToString();
                     ddl_pantry_jacket.DataBind();
                     ddl_pantry_jacket.Items.Insert(0, "Select");
                 }
                 d.con.Close();
             }
             catch (Exception ex)
             { }
             finally { d.con.Close(); }
         }
         else if (ddl_item_type.SelectedValue == "Shoes")
         {
             try
             {
                 MySqlCommand cmd = new MySqlCommand("SELECT distinct( size ) FROM  pay_item_master  WHERE  product_service  = 'Shoes' and  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "' AND  size  IS NOT NULL", d.con);
                 d.con.Open();
                 MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 dr.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     ddl_shoes_size.DataSource = dt;
                     ddl_shoes_size.DataTextField = dt.Columns[0].ToString();
                     ddl_shoes_size.DataBind();
                     ddl_shoes_size.Items.Insert(0, "Select");
                 }
                 d.con.Close();
             }
             catch (Exception ex)
             { }
             finally { d.con.Close(); }
         }
         else if (ddl_item_type.SelectedValue == "Apron")
         {
             try
             {
                 MySqlCommand cmd = new MySqlCommand("SELECT distinct( size ) FROM  pay_item_master  WHERE  product_service  = 'Apron' and  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'", d.con);
                 d.con.Open();
                 MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 dr.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     ddl_apron_size.DataSource = dt;
                     ddl_apron_size.DataTextField = dt.Columns[0].ToString();
                     ddl_apron_size.DataBind();
                     ddl_apron_size.Items.Insert(0, "Select");
                 }
                 d.con.Close();
             }
             catch (Exception ex)
             { }
             finally { d.con.Close(); }
         }
     }

     

     protected void ddl_item_type_SelectedIndexChanged(object sender, EventArgs e)
     {
         ddl_item_name.Items.Clear();
         MySqlCommand cmd_item = new MySqlCommand("SELECT item_name , item_code  from PAY_ITEM_MASTER WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and product_service='" + ddl_item_type.SelectedValue + "' and size is not null ORDER BY ITEM_NAME", d.con);
         d.con.Open();
         try
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
             int i = 0;
             MySqlDataReader dr_item = cmd_item.ExecuteReader();
             while (dr_item.Read())
             {
                 ddl_item_name.Items.Insert(i++, new ListItem(dr_item[0].ToString(), dr_item[1].ToString()));
             }
             dr_item.Close();
         }
         catch (Exception ex) { throw ex; }
         finally
         {
             d.con.Close();
             ddl_item_name.Items.Insert(0, new ListItem("Select", "0"));

         }
         
         size();
         //lbl_qty.Visible = false;
         //txt_quantity1.Visible = false;
         //lbl_raten.Visible = false;
         //lbl_rete1.Visible = false;
         //txt_description.Text = "";
         //txt_hsn.Text = "";
         //txt_desc.Text = "";
         ////txt_designation.text = "Select";
         //txt_per_unit.Text = "0";
         //txt_quantity.Text = "0";
         //txt_rate.Text = "0";
         //txt_discount_rate.Text = "0";
         //txt_discount_price.Text = "0";
         //txt_amount.Text = "0";

     }
     protected void particular_hsn_sac_code(object sender, EventArgs e)
     {
        
         if (ddl_item_name.SelectedItem.ToString() != "Select")
         {
             // MySqlCommand cmd = new MySqlCommand("Select item_description,PURCHASE_RATE,unit,VAT,case When hsn_number <> '' then hsn_number else sac_number END as hsn_sac_no , case When hsn_number <> '' then 'H' else 'S' END as hsn,unit_per_piece,Stock from PAY_ITEM_MASTER where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ITEM_NAME = '" + txt_particular.SelectedItem.Text.ToString() + "'", d.con);
             MySqlCommand cmd = new MySqlCommand("Select item_description,PURCHASE_RATE,unit,VAT,case When hsn_number <> '' then hsn_number else sac_number END as hsn_sac_no , case When hsn_number <> '' then 'H' else 'S' END as hsn,unit_per_piece,Stock,(select PURCHASE_RATE from pay_transactionp where comp_code='" + Session["COMP_CODE"].ToString() + "'AND ITEM_CODE = '" + ddl_item_name.SelectedValue + "' ORDER BY  ITEM_CODE   DESC LIMIT 1),product_service,size from PAY_ITEM_MASTER where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ITEM_CODE = '" + ddl_item_name.SelectedValue + "'", d.con);
             d.con.Open();
             try
             {
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                 MySqlDataReader dr_item = cmd.ExecuteReader();
                 while (dr_item.Read())
                 {
                    //txt_desc.Text = dr_item.GetValue(0).ToString();
                     txt_per_unit_rate.Text = dr_item.GetValue(1).ToString();
                     // txt_designation.SelectedValue = dr_item.GetValue(2).ToString();      
                     string unit = dr_item.GetValue(2).ToString();
                     //(txt_designation, unit);
                     txt_vat.Text = dr_item.GetValue(3).ToString(); //Passing gst value 
                     txt_hsn_code.Text = dr_item.GetValue(4).ToString();
                     if (dr_item[5].ToString() == "H")
                     {
                         //lbl_hsn_code.Text = "HSN Code";
                     }
                     else
                     {
                        // lbl_hsn_code.Text = "SAC Code";
                     }
                     txt_per_unit_rate.Text = dr_item.GetValue(6).ToString();
                     //lbl_qty.Text = dr_item.GetValue(7).ToString();
                     //lbl_raten.Text = dr_item.GetValue(8).ToString();
                     if (dr_item[9].ToString() == "Shoes")
                     {
                         ddl_shoes_size.SelectedValue = dr_item.GetValue(10).ToString();
                         ddl_uniform_size.SelectedValue = "Select";
                         ddl_apron_size.SelectedValue = "Select";
                         ddl_apron_size.SelectedValue = "Select";
                     }
                     else if (dr_item[9].ToString() == "Uniform")
                     {
                         ddl_uniform_size.SelectedValue = dr_item.GetValue(10).ToString();
                         ddl_shoes_size.SelectedValue = "Select";
                         ddl_apron_size.SelectedValue = "Select";
                         ddl_apron_size.SelectedValue = "Select";
                     }
                     else if (dr_item[9].ToString() == "pantry_jacket")
                     {
                         ddl_apron_size.SelectedValue = dr_item.GetValue(10).ToString();
                         ddl_uniform_size.SelectedValue = "Select";
                         ddl_shoes_size.SelectedValue = "Select";
                         ddl_apron_size.SelectedValue = "Select";
                     }
                     else if (dr_item[9].ToString() == "Apron" || dr_item[9].ToString() == "apron")
                     {
                         ddl_apron_size.SelectedValue = dr_item.GetValue(10).ToString();
                         ddl_apron_size.SelectedValue = "Select";
                         ddl_uniform_size.SelectedValue = "Select";
                         ddl_shoes_size.SelectedValue = "Select";
                     }

                 }
                 dr_item.Close();
             }
             catch (Exception ex) { throw ex; }
             finally
             {
                 //d.con.Close();
                 //txt_hsn.Enabled = false;
                 //lbl_raten.Visible = true;
                 //lbl_rete1.Visible = true;
                 ////vikas16/11
                 //txt_quantity1.Visible = true;
                 //lbl_qty.Visible = true;
                 //txt_quantity1.Visible = true;
                 //// txt_designation.Enabled = false;
                 //// txt_rate.Enabled = false;
                 //// txt_description.Enabled = false;
                 //tooltrip();

                 ////MySqlCommand cmdrate = new MySqlCommand("select ITEM_CODE from  pay_transactionp_details where  ITEM_CODE='" + txt_particular.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
                 //get_purchase_rate();
             }

         }
      }

    
     protected void txt_quantity_TextChanged(object sender, EventArgs e)
     {
         float rate = float.Parse(txt_per_unit_rate.Text);
         float quantity = float.Parse(txt_qty.Text);
         if (quantity > 0)
         {
             double amount = rate * quantity;
             txt_amount.Text = Math.Round(amount, 2).ToString();
             //if (Session["DISCOUNT_BY"].ToString() == "PER")
             //{
             //    txt_discount_rate_TextChanged(sender, e);
             //}
             //else
             //{
             //    txt_discount_price_TextChanged(sender, e);
             //}
         }
         else
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Quantity must be greater than 0');", true);
             txt_qty.Text = "0";
             //txt_quantity.Focus();
         }
        //tooltrip();
     }

     public void gst_calculate(System.Data.DataTable dt_gst)
     {
         System.Web.UI.WebControls.Table Table1 = new System.Web.UI.WebControls.Table();
        // Panel1.Visible = true;
        // Panel1.Controls.Clear();
       //  Panel1.Controls.Add(Table1);
         TableRow tRow = new TableRow();
         TableCell tCell = new TableCell();
         Table1.HorizontalAlign = HorizontalAlign.Right;
         double sub_total = 0;
         tCell.HorizontalAlign = HorizontalAlign.Right;
         //tCell.Width = 1000;
         double gst1_total = 0;
         double cgst1_total = 0;
         double total_cgst = 0;
         double total_sgst = 0;
         double total_igst = 0;
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
                             sub_total = sub_total + Convert.ToDouble(dr_gst["Amount"].ToString());
                         }
                     }
                     unique_gst["total"] = cgst1_total.ToString();
                     unique_gst["title_c"] = "CGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
                     unique_gst["title_s"] = "SGST @ " + Convert.ToDouble(unique_gst["VAT"].ToString()) / 2 + " %";
                     total_cgst = total_cgst + cgst1_total;
                     total_sgst = total_sgst + cgst1_total;
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
                             sub_total = sub_total + Convert.ToDouble(dr_gst["Amount"].ToString());
                         }
                     }
                     unique_gst["total"] = gst1_total.ToString();
                     unique_gst["title_i"] = "IGST @ " + unique_gst["VAT"].ToString() + " %";
                     total_igst = total_igst + gst1_total;
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
             //double grosstotal = Convert.ToDouble(txt_taxable_amt.Text);

           
             txt_sub_total.Text = sub_total.ToString();
             txt_igst.Text = total_igst.ToString();
             txt_CGST.Text = total_cgst.ToString();
             txt_sgst.Text = total_sgst.ToString();

             txt_grand_total.Text = (sub_total + total_cgst + total_sgst + total_igst).ToString();
             txt_advacamt.Text = ((Convert.ToDouble(txt_grand_total.Text)) * (Convert.ToDouble(txt_advance.Text)) / 100).ToString();
             txt_remainamt.Text = ((Convert.ToDouble(txt_grand_total.Text)) * (Convert.ToDouble(txt_remain.Text)) / 100).ToString();
             //final_total();
         }
         catch (Exception gst)
         {
             throw gst;
         }

     }

     public void discount_calculate(System.Data.DataTable dt_discount, int discount_choice)
     {
         //double discount_percent = 0, discount_price = 0, final_price = 0, gross_total_discount = 0, gross_total_no_discount = 0; ;

         //foreach (DataRow dr_discount in dt_discount.Rows)
         //{
         //    if (Convert.ToDouble(dr_discount["DISCOUNT"].ToString()) <= 0)
         //    {
         //        gross_total_discount = gross_total_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
         //    }
         //    else
         //    {
         //        gross_total_no_discount = gross_total_no_discount + Convert.ToDouble(dr_discount["Amount"].ToString());
         //    }
         //    c = c + Convert.ToDouble(dr_discount["Amount"].ToString());
         //}

         //if (c == gross_total_no_discount)
         //{
         //    txt_tot_discount_percent.Text = "0";
         //    txt_tot_discount_amt.Text = "0";
         //    txt_tot_discount_percent.Enabled = false;
         //    txt_tot_discount_amt.Enabled = false;
         //}
         //else
         //{
         //    txt_tot_discount_percent.Enabled = true;
         //    txt_tot_discount_amt.Enabled = true;
         //    if (discount_choice == 0)
         //    {
         //        discount_percent = Convert.ToDouble(txt_tot_discount_percent.Text);
         //        discount_price = Math.Round(((discount_percent * gross_total_discount) / 100), 2);
         //        final_price = Math.Round((gross_total_discount - discount_price), 2);
         //        txt_tot_discount_amt.Text = Convert.ToString(discount_price);
         //    }
         //    else if (discount_choice == 1)
         //    {
         //        discount_price = Convert.ToDouble(txt_tot_discount_amt.Text);
         //        discount_percent = Math.Round(((discount_price * 100) / gross_total_discount), 2);
         //        final_price = Math.Round((gross_total_discount - discount_price), 2);
         //        txt_tot_discount_percent.Text = Convert.ToString(discount_percent);
         //    }
         //}
         //txt_grossamt.Text = Convert.ToString(Math.Round(c, 2));
         //txt_taxable_amt.Text = Convert.ToString(final_price + gross_total_no_discount);
         gst_calculate(dt_discount);

     }

     protected void btn_Save_Click(object sender, EventArgs e)
     {

         int invoice_result = 0;
         int item_result = 0;
        // budget();
         //if (budget())
         //{
         //    btn_budget.Visible = true;
         //    return;
         //}

         ///// for client assign komal 06-04-2020




         string client_check = d.getsinglestring("SELECT GROUP_CONCAT(distinct(`pay_vendor_client_assign`.`client_code`)) FROM `pay_vendor_client_assign`  INNER JOIN `pay_client_billing_details` ON `pay_vendor_client_assign`.`comp_code` = `pay_client_billing_details`.`comp_code` AND `pay_vendor_client_assign`.`client_code` = `pay_client_billing_details`.`client_code` WHERE pay_vendor_client_assign.`comp_code` = '" + Session["comp_code"].ToString() + "'  AND `billing_id` = '2' AND `VEND_ID` = '" + ddl_customerlist.SelectedValue + "'");

         client_check = client_check.Replace(",", "','");

         string billing_client = d.getsinglestring("SELECT  group_concat(DISTINCT(`client_code`)) FROM `pay_billing_material_history` WHERE `pay_billing_material_history`.`comp_code` = '" + Session["comp_code"].ToString() + "'  and client_code in('" + client_check + "')  AND `month` = '" + txt_docdate.Text.Substring(3, 2) + "' AND `year` = '" + txt_docdate.Text.Substring(6) + "' ");

         billing_client = billing_client.Replace(",", "','");

         string client_name = d.getsinglestring(" select group_concat(distinct (pay_client_master.client_name)) from pay_vendor_client_assign  INNER JOIN `pay_client_billing_details` ON `pay_vendor_client_assign`.`comp_code` = `pay_client_billing_details`.`comp_code` AND `pay_vendor_client_assign`.`client_code` = `pay_client_billing_details`.`client_code` inner join pay_client_master on pay_vendor_client_assign.comp_code = pay_client_master.comp_code and pay_vendor_client_assign.client_code = pay_client_master.client_code where pay_vendor_client_assign.comp_code = '" + Session["comp_code"].ToString() + "' and pay_vendor_client_assign.client_code not in('" + billing_client + "')  AND `billing_id` = '2' and `VEND_ID` = '" + ddl_customerlist.SelectedValue + "'");

        if (client_name!="") 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Final The Bill For this Client " + client_name + "');", true);
            return;
        
        }

         ///////
         btn_budget.Visible = false;

         string final = d.getsinglestring(" select PO_NO from pay_transaction_po where PO_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_po_flag = 1");
         if (final != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This PO IS Final YOU Can Not Save  !!');", true);

                return;
         
            }
         MySqlCommand cmd = new MySqlCommand("select PO_NO from pay_transaction_po  where PO_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
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

                 if (ddl_vendortype.SelectedValue == "Regular")
                 {

                     invoice_result = d.operation("INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,ADDRESS,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PERCENTAGE,REMANING_PERCENTAGE,ADVANCE_PAYMENT,REMAINING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO,client_code,unit_code,month,year,vendor_categorie,CUST_CODE) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + txt_address.Text + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_advacamt.Text + "','" + txt_remainamt.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "','" + ddl_client.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + txt_docdate.Text.Substring(3, 2).ToString() + "','" + txt_docdate.Text.Substring(6).ToString() + "','" + ddl_vendor_categories.SelectedValue + "','" + ddl_customerlist.SelectedValue + "')");
                 }
                 else
                 {
                    
                    //invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "')");
                     invoice_result = d.operation("INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,ADDRESS,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PERCENTAGE,REMANING_PERCENTAGE,ADVANCE_PAYMENT,REMAINING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO,,client_code,unit_code,month,year,vendor_categorie,CUST_CODE) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + txt_address.Text + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_advacamt.Text + "','" + txt_remainamt.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "','" + ddl_client.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + txt_docdate.Text.Substring(3, 2).ToString() + "','" + txt_docdate.Text.Substring(6).ToString() + "',,'" + ddl_vendor_categories.SelectedValue + "','" + ddl_customerlist.SelectedValue + "')");
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
                     //TextBox lbl_designation = (TextBox)row.FindControl("lbl_designation");
                     //string designation = lbl_designation.Text;
                     TextBox txt_qty = (TextBox)row.FindControl("lbl_quantity");
                     float quantity = float.Parse(txt_qty.Text);
                     TextBox txt_rate = (TextBox)row.FindControl("lbl_rate");
                     float rate = float.Parse(txt_rate.Text);
                     //TextBox txt_product_discount = (TextBox)row.FindControl("lbl_discount");
                     //float product_discount = float.Parse(txt_product_discount.Text);
                     //TextBox txt_product_discount_amt = (TextBox)row.FindControl("lbl_discount_amt");
                     //float product_discount_amt = float.Parse(txt_product_discount_amt.Text);
                     TextBox lbl_amount = (TextBox)row.FindControl("lbl_amount");
                     float amount = float.Parse(lbl_amount.Text);
                     //TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                     //string start_date = lbl_start_date.Text.ToString(); vikas comment 22/11
                     //TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                     //string end_date = lbl_end_date.Text.ToString();
                     //TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                     //string vendor = lbl_vendor.Text.ToString();

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
                     //int insert_result = d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,END_DATE,VENDOR,item_type,size_uniform,size_shoes, size_pantry ,size_apron) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(amount) + ",'" + item_type + "','" + uniformsize + "','" + shoes + "','" + pantrysize + "','" + apronsize + "')");
                     int insert_result = d.operation("INSERT INTO pay_transaction_po_details(COMP_CODE,TASK_CODE,PO_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,QUANTITY,RATE,AMOUNT,item_type,size_uniform,size_shoes, size_pantry ,size_apron, CUST_CODE ) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(amount) + ",'" + item_type + "','" + uniformsize + "','" + shoes + "','" + pantrysize + "','" + apronsize + "','" + ddl_customerlist.SelectedValue.Substring(0,4).ToString()+ "')");                                                     
                     //INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PAYMENT,REMANING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "'                                     
                     if (insert_result > 0)
                     {
                         try
                         {
                             item_result = item_result + insert_result;
                             string sum_inward = (" select sum(QUANTITY) from pay_transaction_po_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
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

                         catch(Exception ex) { throw ex;  }
                     }
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:swal('Record Added Successfully !!!')", true);

                 }
                 if (invoice_result > 0)
                 {
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice (" + txt_docno.Text + ") added successfully!')", true);

                     // attached_doc();
                     //   lbl_print_quote.Text = txt_docno.Text;

                 }
                 else
                 {
                     d.operation("Delete from pay_transaction_po Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                     d.operation("Delete from pay_transaction_po_details Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
                 }

             }
             dr.Close();
             d.con.Close();
         }
         catch (Exception ee)
         {
             d.operation("Delete from pay_transaction_po Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
             d.operation("Delete from pay_transaction_po_details Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
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
        
     }
     protected void btn_delete_Click(object sender, EventArgs e)
     {
         try
         {

             string final = d.getsinglestring(" select PO_NO from pay_transaction_po where PO_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_po_flag = 1");
             if (final != "")
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This PO IS Final YOU Can Not Delete  !!');", true);
                 return;
             }

             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
             int result = 0;

             string po_delele = d.getsinglestring("SELECT GROUP_CONCAT( DOC_NO ) FROM  pay_transactionp  WHERE  comp_code  = '" + Session["comp_code"].ToString() + "' AND  pur_order_no  = '" + txt_docno.Text + "'");
             if (po_delele!="")
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please first delete all invoices (" + po_delele + ")');", true);
               return;
           }
             string l_docnumber = txt_docno.Text;

             d.operation("DELETE FROM pay_transaction_po WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND PO_NO='" + l_docnumber + "'");

             foreach (GridViewRow row in gv_itemslist.Rows)
             {
                 Label lbl_item_code = (Label)row.FindControl("lbl_item_code");
                 string item_code = (lbl_item_code.Text);
                 TextBox txt_quantity = (TextBox)row.FindControl("lbl_quantity");
                 float quantity = float.Parse(txt_quantity.Text);

                 d.operation("DELETE FROM pay_transaction_po_details WHERE Item_Code = '" + item_code + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND PO_NO='" + l_docnumber + "'");
                 string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_inward from pay_transaction_po_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
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
             } ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ddl_customerlist.Text + " Deleted Successfully');", true);
             //   Panel1.Visible = false;

             //Panel8.Visible = true;
             //Panel6.Visible = true;
             //Panel5.Visible = false;

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
     protected void btn_update_Click(object sender, EventArgs e)
     {

         try
         {
             string final = d.getsinglestring(" select PO_NO from pay_transaction_po where PO_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_po_flag = 1");
             if (final != "")
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This PO IS Final YOU Can Not update  !!');", true);
                 return;
             }

             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
             //if (budget())
             //{
             //    btn_budget.Visible = false;
             //    return;
             //}
             btn_budget.Visible = false;
             int result = 0;
             int invoice_result = 0;
             int item_result = 0;
             int TotalRows = gv_itemslist.Rows.Count;


             string l_docnumber = txt_docno.Text;
             lbl_print_quote.Text = l_docnumber;
             d.operation("DELETE FROM pay_transaction_po WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND PO_NO='" + l_docnumber + "' ");

             d.operation("DELETE FROM pay_transaction_po_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND PO_NO='" + l_docnumber + "' ");

            
             if (ddl_vendortype.SelectedValue == "Regular")
             {

                 
                 //invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.Text.Substring(0, 4) + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "')");
                 invoice_result = d.operation("INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,ADDRESS,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PERCENTAGE,REMANING_PERCENTAGE,ADVANCE_PAYMENT,REMAINING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO,vendor_categorie,client_code,unit_code,CUST_CODE) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + txt_address.Text + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_advacamt.Text + "','" + txt_remainamt.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "','" + ddl_vendor_categories.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + ddl_customerlist.SelectedValue + "')");
             }
             else
             {
                // invoice_result = d.operation("INSERT into pay_transactionp(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE,CUST_NAME,customer_gst_no,EXPIRY_DATE,BILL_ADDRESS,SHIPPING_ADDRESS,SALES_PERSON,NARRATION,REF_NO2,GROSS_AMOUNT,DISCOUNT,DISCOUNTED_PRICE,TAXABLE_AMT,NET_TOTAL,EXTRA_CHRGS,EXTRA_CHRGS_AMT,EXTRA_CHRGS_TAX,EXTRA_CHRGS_TAX_AMT,FINAL_PRICE,CUSTOMER_NOTES,TERMS_CONDITIONS,CUST_CODE,p_o_no,TRANSPORT,FREIGHT,VEHICLE_NO,SALES_MOBILE_NO,BILL_MONTH,BILL_YEAR,Category,vendor_type,BANK_AC_NUM,BANK_AC_NAME,IFC_CODE,CREDIT_PERIOD) values('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + txt_vendorname.Text + "','" + txt_customer_gst.Text + "',str_to_date('" + txt_expiry_date.Text + "','%d/%m/%Y'),'" + txt_bill_add.Text + "','" + txt_ship_add.Text + "','" + ddl_sales_person.Text + "','" + txt_narration.Text + "','" + txt_referenceno2.Text + "','" + txt_grossamt.Text + "','" + txt_tot_discount_percent.Text + "','" + txt_tot_discount_amt.Text + "','" + txt_taxable_amt.Text + "','" + txt_sub_total1.Text + "','" + txt_extra_chrgs.Text + "','" + txt_extra_chrgs_amt.Text + "','" + txt_extra_chrgs_tax.Text + "','" + txt_extra_chrgs_tax_amt.Text + "','" + txt_final_total.Text + "','" + txt_customer_notes.Text + "','" + txt_terms_conditions.Text + "','" + ddl_customerlist.SelectedValue + "','" + txt_p_o_no.Text + "','" + ddl_transport.SelectedItem.Text + "','" + txt_freight.Text + "','" + txt_vehicle.Text + "','" + txtsalesmobileno.Text + "','" + ddlcalmonth.Text + "','" + txt_year.Text + "','" + ddlcategory.SelectedItem.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_bank_no.Text + "','" + txt_bank_ac.Text + "','" + txt_ifc_code.Text + "','" + txt_credit_perod.Text + "')");
                 invoice_result = d.operation("INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PERCENTAGE,REMANING_PERCENTAGE,ADVANCE_PAYMENT,REMAINING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO,vendor_categorie,client_code,unit_code,CUST_CODE) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.SelectedItem.ToString() + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_advacamt.Text + "','" + txt_remainamt.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "','" + ddl_vendor_categories.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + ddl_customerlist.SelectedValue+ "')");
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
                     //TextBox lbl_designation = (TextBox)row.FindControl("lbl_designation");
                     //string designation = lbl_designation.Text;
                     TextBox txt_qty = (TextBox)row.FindControl("lbl_quantity");
                     float quantity = float.Parse(txt_qty.Text);
                     TextBox txt_rate = (TextBox)row.FindControl("lbl_rate");
                     float rate = float.Parse(txt_rate.Text);
                     //TextBox txt_product_discount = (TextBox)row.FindControl("lbl_discount");
                     //float product_discount = float.Parse(txt_product_discount.Text);
                     //TextBox txt_product_discount_amt = (TextBox)row.FindControl("lbl_discount_amt");
                     //float product_discount_amt = float.Parse(txt_product_discount_amt.Text);
                     TextBox lbl_amount = (TextBox)row.FindControl("lbl_amount");
                     float amount = float.Parse(lbl_amount.Text);
                     //TextBox lbl_start_date = (TextBox)row.FindControl("lbl_start_date");
                     //string start_date = lbl_start_date.Text.ToString(); vikas comment 22/11
                     //TextBox lbl_end_date = (TextBox)row.FindControl("lbl_end_date");
                     //string end_date = lbl_end_date.Text.ToString();
                     //TextBox lbl_vendor = (TextBox)row.FindControl("lbl_vendor");
                     //string vendor = lbl_vendor.Text.ToString();

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
                     //int insert_result = d.operation("INSERT INTO pay_transactionp_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,DESIGNATION,QUANTITY,RATE,DISCOUNT,DISCOUNT_AMT,AMOUNT,END_DATE,VENDOR,item_type,size_uniform,size_shoes, size_pantry ,size_apron) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(amount) + ",'" + item_type + "','" + uniformsize + "','" + shoes + "','" + pantrysize + "','" + apronsize + "')");
                     int insert_result = d.operation("INSERT INTO pay_transaction_po_details(COMP_CODE,TASK_CODE,PO_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,QUANTITY,RATE,AMOUNT,item_type,size_uniform,size_shoes, size_pantry ,size_apron) VALUES('" + Session["COMP_CODE"].ToString() + "','STP','" + txt_docno.Text + "'," + Convert.ToInt32(sr_number) + ",'" + item_code.ToString() + "','" + particular.ToString() + "','" + lbl_Description_final1 + "','" + Convert.ToDouble(vat) + "','" + lbl_hsc_code + "'," + Convert.ToDouble(quantity) + "," + Convert.ToDouble(rate) + "," + Convert.ToDouble(amount) + ",'" + item_type + "','" + uniformsize + "','" + shoes + "','" + pantrysize + "','" + apronsize + "')");                                                     
                    //INSERT into pay_transaction_po(COMP_CODE, PO_NO, PO_DATE,CUST_NAME,PO_GENRATED_BY,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PAYMENT,REMANING_PAYMENT,customer_gst_no,vendor_type,MOBILE_NO) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_docno.Text + "',str_to_date('" + txt_docdate.Text + "','%d/%m/%Y'),'" + ddl_customerlist.Text + "','" + ddl_sales_person.Text + "','" + txt_sub_total.Text + "','" + txt_CGST.Text + "','" + txt_sgst.Text + "','" + txt_igst.Text + "','" + txt_grand_total.Text + "','" + txt_advance.Text + "','" + txt_remain.Text + "','" + txt_customer_gst.Text + "','" + ddl_vendortype.SelectedValue + "','" + txt_mobileno.Text + "'                                     
                     if (insert_result > 0)
                     {
                         try
                         {
                             item_result = item_result + insert_result;
                             string sum_inward = (" select sum(QUANTITY) from pay_transaction_po_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
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
                         catch (Exception ex) { throw ex; }

                 }
                 insert_result = 0;


             }
             if (invoice_result > 0)
             {
                 //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "swal('Invoice (" + txt_docno.Text + ") Updated successfully!')", true);
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Invoice (" + txt_docno.Text + ") Updated successfully!');", true);
                 //Panel5.Visible = false;
                 //gv_bynumber_name.Visible = false;
                 //attached_doc();
                 //lbl_print_quote.Text = txt_docno.Text;

             }
             else
             {
                 d.operation("Delete from pay_transaction_po Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                 d.operation("Delete from pay_transaction_po_details Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice ('" + txt_docno.Text + "') does not saved successfully!')", true);
             }

         }

         catch (Exception ee)
         {
             d.operation("Delete from pay_transaction_po Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
             d.operation("Delete from pay_transaction_po_details Where PO_NO ='" + txt_docno.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"].ToString() + "'");
             throw ee;
         }
         finally
         {
             d.con.Close();
             text_clear();
             gendocno();
            // tooltrip();
             btn_Save.Visible = true; ;
             btn_delete.Visible = false;
             btn_update.Visible = false;
         }
     }

    public void text_clear()
     {
      
         //   txt_year.Text = "";
         ddl_vendor_categories.SelectedValue = "0";
         txt_docdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
         txt_address.Text = "";
         ddl_customerlist.SelectedValue = "Select";
         ddl_client.SelectedValue = "Select";
         ddl_branch.SelectedValue = "ALL";
         txt_amount.Text = "";
         ddl_sales_person.Text = "";
         txt_mobileno.Text = "";
        
         txt_customer_gst.Text = "";
         ddl_sales_person.Text = "";
         // txt_particular.SelectedValue = "Select";
       
         txt_description.Text = "0";
        
         ddl_sales_person.Text = "";
         txt_sub_total.Text = "0";
         txt_CGST.Text = "0";
         txt_sgst.Text = "0";
         txt_igst.Text = "0";
         txt_grand_total.Text = "0";
         txt_advacamt.Text = "0";
         txt_remainamt.Text = "0";
       
         gv_itemslist.DataSource = null;
         gv_itemslist.DataBind();
         gv_itemslist.Visible = false;
         Panel4.Visible = false;
        
         ddl_vendortype.SelectedValue = "Select";
       
     }
    protected void btn_searchvendor_Click(object sender, EventArgs e)
    {
        // hide.Visible = false;
        if (txt_docno_number.Text != "")
        {
            MySqlCommand cmd_pono = new MySqlCommand("SELECT  pay_transaction_po.PO_NO,date_format(pay_transaction_po.PO_DATE,'%d/%m/%Y') AS PO_DATE,CUST_NAME,GRAND_TOTAL FROM pay_transaction_po WHERE (pay_transaction_po.PO_NO LIKE '%" + txt_docno_number.Text + "%' AND pay_transaction_po.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transaction_po.PO_DATE DESC ", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_pono = cmd_pono.ExecuteReader();
                if (dr_pono.HasRows)
                {
                    Panel5.Visible = true;
                    //Panel6.Visible = true;
                    gv_bynumber_name.Visible = true;
                    gv_bynumber_name.DataSource = dr_pono;
                    gv_bynumber_name.DataBind();
                }
                else
                {
                    gv_bynumber_name.Visible = false;
                    Panel5.Visible = false;
                   // Panel6.Visible = false;

                }
                dr_pono.Close();
                //vikas24/11 for read only
                txt_customer_gst.ReadOnly = true;
                txt_address.ReadOnly = true;
                //txt_bank_ac.ReadOnly = true;
                //txt_bank_no.ReadOnly = true;
                //txt_ifc_code.ReadOnly = true;
                //txt_credit_perod.ReadOnly = true;
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
             //MySqlCommand cmd_docno = new MySqlCommand("SELECT  pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM pay_transactionp WHERE (pay_transactionp. CUST_NAME  LIKE '%" + txt_customername.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC ", d.con);
             MySqlCommand cmd_docno = new MySqlCommand("SELECT  pay_transaction_po.PO_NO,date_format(pay_transaction_po.PO_DATE,'%d/%m/%Y') AS PO_DATE,CUST_NAME,GRAND_TOTAL FROM pay_transaction_po WHERE (pay_transaction_po.cust_name LIKE '%" + txt_customername.Text + "%' AND pay_transaction_po.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transaction_po.PO_DATE DESC ", d.con);
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
        //if (gv_bynumber_name.Rows.Count > 0)
        //{
        //    if (txt_customername.Text != "")
        //    {
        //        // MySqlCommand cmd_customername = new MySqlCommand("SELECT pay_transactionp.CUST_NAME,pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') as DOC_DATE,NET_TOTAL FROM pay_transactionp WHERE (pay_transactionp.CUST_NAME LIKE '%" + txt_customername.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC", d.con);
        //        MySqlCommand cmd_customername = new MySqlCommand("SELECT  pay_transactionp.DOC_NO,date_format(pay_transactionp.DOC_DATE,'%d/%m/%Y') AS DOC_DATE,CUST_NAME,FINAL_PRICE FROM pay_transactionp WHERE (pay_transactionp. CUST_NAME  LIKE '%" + txt_docno_number.Text + "%' AND pay_transactionp.COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY pay_transactionp.DOC_DATE DESC ", d.con);
        //        d.con.Open();
        //        try
        //        {
        //            MySqlDataReader dr_customername = cmd_customername.ExecuteReader();
        //            if (dr_customername.HasRows)
        //            {
        //                Panel5.Visible = true;
        //               // Panel6.Visible = true;//vikas22/11
        //                gv_bynumber_name.Visible = true;
        //                gv_bynumber_name.DataSource = dr_customername;
        //                gv_bynumber_name.DataBind();
        //            }
        //            else
        //            {
        //                gv_bynumber_name.Visible = false;
        //                Panel5.Visible = false;
        //                //Panel6.Visible = false;

        //            }
        //            dr_customername.Close();
        //        }
        //        catch (Exception ex) { throw ex; }
        //        finally
        //        {
        //            d.con.Close();
        //        }
        //    }
        //}
        //else
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found for')", true);
        //}
        // SELECT  PAY_TRANSACTIONQ.DOC_NO,PAY_TRANSACTIONQ.DOC_DATE AS 'DOC_DATE',CUST_NAME FROM PAY_TRANSACTIONQ   ORDER BY PAY_TRANSACTIONQ.DOC_DATE DESC 
    }
    protected void lnk_button_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
       // Panel6.Visible = true;
        Panel4.Visible = true;
        //Panel8.Visible = true;
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
        MySqlCommand cmd2 = new MySqlCommand("select PO_NO,DATE_FORMAT(PO_DATE,'%d/%m/%Y'),vendor_type,CUST_CODE,customer_gst_no,ADDRESS,PO_GENRATED_BY,MOBILE_NO,ADVANCE_PERCENTAGE,REMANING_PERCENTAGE,SUB_TOTAL,CGST,SGST,IGST,GRAND_TOTAL,ADVANCE_PAYMENT,REMAINING_PAYMENT,vendor_categorie FROM pay_transaction_po WHERE (PO_NO = '" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "')", d3.con);

        d3.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_docno.Text = dr2[0].ToString();
                txt_docdate.Text = dr2[1].ToString();
                ddl_vendortype.SelectedValue = dr2[2].ToString();
                ddl_vendor_categories.SelectedValue = dr2[17].ToString();
                ddl_vendortype_SelectedIndexChanged(null, null);
                //vikas23/11
               
                if (ddl_vendortype.SelectedValue == "Regular")
                {
                   
                    ddl_customerlist.SelectedValue = dr2[3].ToString();
                }
                else {  }
                //ddl_client.SelectedValue = dr2[18].ToString();
                //ddl_client_SelectedIndexChanged(null, null);
                //ddl_branch.SelectedValue = dr2[19].ToString();
                txt_customer_gst.Text = dr2["customer_gst_no"].ToString();
                txt_address.Text = dr2["ADDRESS"].ToString();
                ddl_sales_person.Text = dr2["PO_GENRATED_BY"].ToString();
                txt_mobileno.Text = dr2["MOBILE_NO"].ToString();
                txt_advance.Text = dr2["ADVANCE_PERCENTAGE"].ToString();
                txt_remain.Text = dr2["REMANING_PERCENTAGE"].ToString();
                txt_sub_total.Text = dr2["SUB_TOTAL"].ToString();
                txt_CGST.Text = dr2["CGST"].ToString();
                txt_sgst.Text = dr2["SGST"].ToString();
                txt_igst.Text = dr2["IGST"].ToString();
                txt_grand_total.Text = dr2["GRAND_TOTAL"].ToString();
                txt_advacamt.Text = dr2["ADVANCE_PAYMENT"].ToString();
                txt_remainamt.Text = dr2["REMAINING_PAYMENT"].ToString();
              
            }
            dr2.Close();
            d3.con.Close();
            gst_counter(txt_customer_gst.Text.Substring(0, 2));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();

        }



        MySqlCommand cmd1 = new MySqlCommand("SELECT ITEM_CODE,item_type,PARTICULAR,size_uniform,size_shoes,DESCRIPTION,VAT,hsn_number as HSN_Code,DESIGNATION,QUANTITY,RATE,AMOUNT, size_pantry ,size_apron FROM pay_transaction_po_details WHERE (PO_NO ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d.con);
        //COMP_CODE,TASK_CODE,PO_NO,SR_NO,ITEM_CODE,PARTICULAR,DESCRIPTION,VAT,hsn_number,QUANTITY,RATE,AMOUNT,item_type,size_uniform,size_shoes, size_pantry ,size_apron
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
               // Panel2.Visible = true;
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
           //Panel2.Visible = true;
        }
      //  attached_doc();
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

    protected void gv_bynumber_name_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_bynumber_name.UseAccessibleHeader = false;
            gv_bynumber_name.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        string final = d.getsinglestring(" select PO_NO from pay_transaction_po where PO_NO='" + txt_docno.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and final_po_flag = 1");
        if (final != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('This PO IS Final YOU Can Not Remove  !!');", true);
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
                    int delete_item = d.operation("Delete from pay_transaction_po_details Where Item_Code = '" + item_code + "' AND PO_NO = '" + txt_docno.Text + "'  and COMP_CODE = '" + Session["COMP_CODE"] + "' ");
                    string sum_inward = (" select case When isnull(sum(QUANTITY)) then 0 Else sum(QUANTITY) END AS sum_inward from pay_transaction_po_details  where Item_Code='" + item_code + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' ");
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

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        lbl_print_quote.Text = "";
        Response.Redirect("Home.aspx");
    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

        string headerpath = null;
            ReportDocument crystalReport = new ReportDocument();
            System.Data.DataTable dt = new System.Data.DataTable();
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();

            string query1 = "SELECT pay_transaction_po_details.po_no as 'zone', CUST_NAME  as 'UNIT_ADD2', ADDRESS  as 'UNIT_ADD1', customer_gst_no  AS 'unit_gst_no',date_format(pay_transaction_po.PO_DATE,'%d-%m-%Y') as other, MOBILE_NO  AS 'designation',pay_transaction_po.ADVANCE_PERCENTAGE as 'month',pay_transaction_po.REMANING_PERCENTAGE as 'year', DESCRIPTION  as 'TOT_DAYS_PRESENT', hsn_number  AS 'Expr1', QUANTITY  as 'hrs_12_ot', RATE as 'month_days',pay_transaction_po_details.Amount as 'tool_unit',`pay_transaction_po_details`.`size_uniform` AS 'type', PAY_COMPANY_MASTER . COMPANY_NAME , PAY_COMPANY_MASTER . ADDRESS1 , PAY_COMPANY_MASTER . ADDRESS2 , PAY_COMPANY_MASTER . CITY , PAY_COMPANY_MASTER . STATE , PAY_COMPANY_MASTER . PIN ,  PAY_COMPANY_MASTER . PF_REG_NO , PAY_COMPANY_MASTER . ESIC_REG_NO ,   PAY_COMPANY_MASTER . COMPANY_PAN_NO ,   PAY_COMPANY_MASTER . COMPANY_TAN_NO , PAY_COMPANY_MASTER . COMPANY_CIN_NO , PAY_COMPANY_MASTER . COMPANY_CONTACT_NO , PAY_COMPANY_MASTER . COMPANY_WEBSITE ,  PAY_COMPANY_MASTER . SERVICE_TAX_REG_NO , pay_transaction_po.comp_code, VAT  as 'femina_unit', CASE WHEN `IGST` = '' THEN `VAT` / 2 ELSE '0' END AS 'client_code', CASE WHEN `IGST` != '' THEN `VAT` ELSE '0' END AS 'EMP_CODE', CASE WHEN `IGST` = '' THEN ROUND((`VAT` / 2 * (`Amount`)) / 100,2) ELSE '0' END AS 'UNIT_CITY', CASE WHEN `IGST` != '' THEN ROUND((`VAT` * (`Amount`)) / 100, 2) END AS 'EMP_NAME', CGST  AS 'aerosol_unit', SGST  AS 'aerosol_dispenser_rate', IGST  AS 'aerosol_handling_applicable', GRAND_TOTAL  AS 'tool_applicable',PO_GENRATED_BY as 'aerosol_handling_percent' FROM   pay_transaction_po_details   inner join pay_transaction_po on pay_transaction_po_details.po_no=pay_transaction_po.po_no  inner join  PAY_COMPANY_MASTER  on PAY_COMPANY_MASTER.comp_code=pay_transaction_po_details.comp_code WHERE  pay_transaction_po_details.PO_NO = '" + txt_docno.Text + "' and pay_transaction_po_details.comp_code='" + Session["COMP_CODE"].ToString() + "'";
                MySqlCommand cmd = new MySqlCommand(query1, d.con);
                MySqlDataReader sda = null;
                d.con.Open();
                try
                {
                    sda = cmd.ExecuteReader();
                    dt.Load(sda);

                    crystalReport.Load(Server.MapPath("~/purchase_order1.rpt"));
                    if (Session["COMP_CODE"].ToString() == "C02")
                    {
                        headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header.png");
                        //footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                        crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerpath + "'";
                        //crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
                    }
                    else
                    {
                        headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header.png");
                        // footerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                        crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerpath + "'";
                        // crystalReport.DataDefinition.FormulaFields["footerimagepath"].Text = @"'" + footerpath + "'";
                    }
                    crystalReport.SetDataSource(dt);
                    crystalReport.Refresh();
                    crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "PurchaseOrder");
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    //dt.Close();
                }
               
            }
       
        //catch (Exception ee)
        //{
        //    throw ee;
        //}
        //finally
        //{
        //    d.con.Close();
           
        //}

    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
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
       // e.Row.Cells[10].Visible = false;
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
         ddl_branch.Items.Clear();
         d.con.Close();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select unit_name,unit_code from pay_unit_master where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' ", d.con);
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
                dt_item.Dispose();

                d.con.Close();
                ddl_branch.Items.Insert(0, "ALL");
            }
            catch (Exception ex)
            { throw ex; }
            finally { d.con.Close();
            
            }
        
    }
    //protected Boolean budget()
    //{
        
    //        string client_b = d.getsinglestring("select IFNULL( Budget_Materials , 0) from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "' ");
    //        if (client_b == "")
    //        {
    //            client_b = "0";
    //        }
    //        string vendor_p = d.getsinglestring("select IFNULL(SUM( GRAND_TOTAL ), 0) from pay_transaction_po where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_client.SelectedValue + "'and month='" + txt_docdate.Text.Substring(3, 2).ToString() + "'and year='" + txt_docdate.Text.Substring(6).ToString() + "' ");
    //        if (vendor_p == "")
    //        {
    //            vendor_p = "0";
    //        }
    //        double buget = Convert.ToDouble(vendor_p) + Convert.ToDouble(txt_grand_total.Text);
    //        if (buget > Convert.ToDouble(client_b))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('P.O value Not Grater than budget value  ...');", true);
    //            btn_budget.Visible = true;
    //            return true;
    //        }
        
    //    return false;
    //}
    protected void btn_budget_Click(object sender, EventArgs e)
    {

    }
    protected void btn_final_Click(object sender, EventArgs e)
    {
        int  result = 0;
        result = d.operation("update pay_transaction_po set final_po_flag = 1 WHERE  PO_NO = '" + txt_docno.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "' ");
        btn_Print_Click(null,null);
    }
}

