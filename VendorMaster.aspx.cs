using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.IO;
using System.Web;


public partial class VendorMaster : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    DAL d = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            attachment_gridview();
            rate_gridview();
            not_assign_client_new();
            btnadd.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;

            btnnew_Click();
            text_Clear();
            company_bank_details();

            gridview_wait_master.DataSource = null;
            gridview_wait_master.DataBind();


            gv_rate_master.DataSource = null;
            gv_rate_master.DataBind();

            ddltype.Items.Clear();
            MySqlCommand vend_type = new MySqlCommand("SELECT VEND_TYPE FROM pay_vendor_type", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_vend_type = vend_type.ExecuteReader();
                while (dr_vend_type.Read())
                {
                    ddltype.Items.Add(dr_vend_type[0].ToString());
                    ddltype.DataValueField = dr_vend_type[0].ToString();
                    ddltype.DataTextField = dr_vend_type[0].ToString();
                }
                dr_vend_type.Close();
                d1.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }

            company_bank_load();

            //--- Vendor Table ----
            vendor_table();

            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT distinct STATE_NAME,state_code FROM PAY_STATE_MASTER ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                cmd_item1.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    txt_state.DataSource = dt_item;
                    txt_state.DataTextField = dt_item.Columns[0].ToString();
                    txt_state.DataValueField = dt_item.Columns[1].ToString();
                    txt_state.DataBind();
                }
                dt_item.Dispose();
                d1.con.Close();
                txtbillstate.Items.Insert(0, "Select");
                // txtssstate.Items.Insert(0, "Select");

                //MySqlDataReader dr_item1 = cmd_item.ExecuteReader();
                //while (dr_item1.Read())
                //{
                //    txt_state.Items.Add(dr_item1[0].ToString());
                //    txt_state.Items.Add(dr_item1[1].ToString());
                //}
                //dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
                txt_state.Items.Insert(0, new ListItem("Select"));
            }

            billing_city();
            // komal chages 24-04-2020 client assign
           // vendor_name();
            //end
            copy_city();
        }
        
    }
    protected void ddl_state_click(object sender, EventArgs e)
    {
       ////txt_city.Items.Clear();
       // MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_code='" + ddl_state.SelectedValue + "' order by city", d1.con);
       // d1.con.Open();
       // try
       // {
       //     MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
       //     while (dr_item1.Read())
       //     {
       //         ddl_notassign_city.Items.Add(dr_item1[0].ToString());
       //     }
       //     dr_item1.Close();
       // }
       // catch (Exception ex) { throw ex; }
       // finally
       // {
       //     d1.con.Close();
       //    // ddl_city.Items.Insert(0, new ListItem("Select", ""));

       // }

        d.con.Open();
        try
        {
          
            MySqlDataAdapter cad1;
            if (ddl_state.SelectedValue == "ALL")
            {
                cad1 = new MySqlDataAdapter("select city from pay_state_master where city NOT IN(select city form pay_vendor_city_details where comp_code='" + Session["comp_code"].ToString() + "' and Vendor_Id='" + txtvendorid.Text + "') ", d.con);
            }
            else
            {
                cad1 = new MySqlDataAdapter("select city from pay_state_master where  `STATE_CODE`='" + ddl_state.SelectedValue + "' and city NOT IN(select city from pay_vendor_city_details where comp_code='" + Session["comp_code"].ToString() + "' and Vendor_Id='" + txtvendorid.Text + "') ", d.con);
            }

            DataSet ds1 = new DataSet();

            cad1.Fill(ds1);
            ddl_notassign_city.DataSource = ds1.Tables[0];
            ddl_notassign_city.DataValueField = "city";
            ddl_notassign_city.DataTextField = "city";
            ddl_notassign_city.DataBind();

            cad1.Dispose();
            d.con.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    }

    // komal chages 24-04-2020 client assign

    //public void vendor_name() 
    //{
    //    System.Data.DataTable dt_ve = new System.Data.DataTable();
    //    MySqlDataAdapter cmd_ve = new MySqlDataAdapter("select `VEND_NAME`,`VEND_ID` from pay_vendor_master where comp_code= '"+Session["comp_code"].ToString()+"'",d3.con);
    //    d3.con.Open();
    //    try {
    //        cmd_ve.Fill(dt_ve);
    //        if(dt_ve.Rows.Count>0)
    //        {
    //            ddl_vendor_name.DataSource = dt_ve;
    //            ddl_vendor_name.DataTextField = dt_ve.Columns[0].ToString();
    //            ddl_vendor_name.DataValueField = dt_ve.Columns[1].ToString();
    //            ddl_vendor_name.DataBind();
            
    //        }
    //        ddl_vendor_name.Items.Insert(0, "Select");

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { d3.con.Close(); }


    //}

    // end
    public void billing_city()
    {
        txtbillstate.Items.Clear();

        ddl_state.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM PAY_STATE_MASTER ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            cmd_item1.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txtbillstate.DataSource = dt_item;
                txtbillstate.DataTextField = dt_item.Columns[0].ToString();
                txtbillstate.DataValueField = dt_item.Columns[1].ToString();
                txtbillstate.DataBind();

                ddl_state.DataSource = dt_item;
                ddl_state.DataTextField = dt_item.Columns[0].ToString();
                ddl_state.DataValueField = dt_item.Columns[1].ToString();
                ddl_state.DataBind();
                txtbillstate.SelectedValue = null;
            }
            dt_item.Dispose();
            d1.con.Close();
            txtbillstate.Items.Insert(0, "Select");
             ddl_state.Items.Insert(0, "Select");
             ddl_state.Items.Insert(1, "ALL");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
    }

    public void copy_city()
    {
        txtssstate.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM PAY_STATE_MASTER ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            cmd_item1.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txtssstate.DataSource = dt_item;
                txtssstate.DataTextField = dt_item.Columns[0].ToString();
                txtssstate.DataValueField = dt_item.Columns[1].ToString();
                txtssstate.SelectedValue = null;

                txtssstate.DataBind();
            }
            dt_item.Dispose();
            d1.con.Close();
            txtssstate.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
    }

    public void get_city(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        txt_city.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_code='" + txt_state.SelectedValue + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_city.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            txt_city.Items.Insert(0, new ListItem("Select", ""));

        }

        DataSet ds_vend_gv = new DataSet();

        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT COMP_CODE, VEND_ID, VEND_NAME, PHONE1,txtbillstate,txtbillcity,  GST FROM pay_vendor_master WHERE(COMP_CODE = '" + Session["COMP_CODE"].ToString() + "') and txtbillstate='" + txt_state.SelectedValue + "' and vendor_type='" + ddl_typevendor.SelectedItem.Text + "' ORDER BY VEND_ID", d1.con1);
        if (d1.con1.State == ConnectionState.Open)
        {
            d1.con1.Close();
            d1.con1.Dispose();
            d1.con1.ClearPoolAsync(d1.con1);
        }
        d1.con1.Open();
        try
        {
            MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
            adp_vend_gv.Fill(ds_vend_gv);
            VendorGridView.DataSource = ds_vend_gv;
            VendorGridView.DataBind();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }

    }
    protected void txt_city_Selected_Index(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        DataSet ds_vend_gv = new DataSet();
        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT COMP_CODE, VEND_ID, VEND_NAME, PHONE1,txtbillstate,txtbillcity, GST FROM pay_vendor_master WHERE(COMP_CODE = '" + Session["COMP_CODE"].ToString() + "') and txtbillstate='" + txt_state.SelectedValue + "' and txtbillcity='" + txt_city.SelectedValue + "' and vendor_type='" + ddl_typevendor.SelectedItem.Text + "' ORDER BY VEND_ID", d1.con1);
        if (d1.con1.State == ConnectionState.Open)
        {
            d1.con1.Close();
            d1.con1.Dispose();
            d1.con1.ClearPoolAsync(d1.con1);
        }
        d1.con1.Open();
        try
        {
            MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
            adp_vend_gv.Fill(ds_vend_gv);
            VendorGridView.DataSource = ds_vend_gv;
            VendorGridView.DataBind();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnnewclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnnew_Click()
    {
        btnadd.Visible = true;
        btnupdate.Visible = false;
        btndelete.Visible = false;
        d1.con1.Open();
        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(VEND_ID, 2, 5) AS UNSIGNED))+1 FROM  pay_vendor_master", d1.con1);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {
            }
            else if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txtvendorid.Text = "V001";
                }
                else
                {
                    int max_vendcode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_vendcode < 10)
                    {
                        txtvendorid.Text = "V00" + max_vendcode;
                    }
                    else if (max_vendcode > 9 && max_vendcode < 100)
                    {
                        txtvendorid.Text = "V0" + max_vendcode;
                    }
                    else if (max_vendcode > 99 && max_vendcode < 1000)
                    {
                        txtvendorid.Text = "V" + max_vendcode;
                    }
                    else
                    {
                    }
                }
            }
            drmax.Close();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

      
        string compcode = Session["COMP_CODE"].ToString();
        int resins = 0;
        int resins1 = 0;
        int TotalRows = gv_itemslist.Rows.Count;
        VendorBAL cb1 = new VendorBAL();
        VendorBAL cb2 = new VendorBAL();
        int rownum;
        VendorBAL vb1 = new VendorBAL();
        try
        {



            if (gv_itemslist.Rows.Count > 0)
            {
                //resins = vb1.VendorInsert(compcode, txtvendorid.Text, txtvendorname.Text, txtvendoradd1.Text,txtvendorphone1.Text, txtvendorphone2.Text, txtvendordesignation.Text, txt_gst.Text,txtvendorpan.Text, txtvendorlst.Text, txtvendorcst.Text,txtvendortin.Text, txtvendorlbt.Text, txtvendorservictax.Text, txtvendoradd2.Text, ddlstatus.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), txtvendortotaldues.Text, txtopeningbalance.Text, txtbillattention.Text, txtbilladdress.Text, txtbillcity.SelectedValue.ToString(), txtbillstate.SelectedValue.ToString(), txtbillzipcode.Text, txtbillcountry.Text, txtbillfax.Text, txtsattention.Text, txtsaddress.Text, txtscity.SelectedValue.ToString(), txtssstate.SelectedValue.ToString(), txtszipcode.Text, txtscountry.Text, txtsfax.Text,txt_v_type.SelectedItem.Text, txt_c_1.Text, txt_c_2.Text,txt_email.Text, txt_area.Text, txt_start.Text, txt_end.Text, txt_saccode.Text, txt_hsmcode.Text, txt_vendor_bank_acc_name.Text, txt_acc_no.Text, txt_ifsc_code.Text, txt_bank_name.Text, txt_credit_period.Text,txt_regi_no.Text);
                resins = d1.operation("INSERT INTO pay_vendor_master(COMP_CODE,VEND_ID,VEND_NAME,VEND_ADD1,PHONE1,PHONE2,DESIGNATION,GST,PAN_NO,LST_NO,CST_NO,TIN_NO,LBT_NO,SERVICE_TAX_NO,VEND_ADD2,ACTIVE_STATUS,TYPE,TOTAL_DUES,OPENING_BALANCE,txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillzipcode,txtbillcountry,txtbillfax,txtsattention,txtsaddress,txtscity,txtssstate,txtszipcode,txtscountry,txtsfax,vendor_type ,contact_person_1,contact_person_2,vendor_email_id,area_to_served,agrement_start_date,agrement_end_date,sac_code,hsm_code,bank_account_name,account_number,ifsc_code,bank_name,CREDIT_PERIOD,comp_registration_num,`vandor_type_nation`) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + txtvendorname.Text + "','" + txtvendoradd1.Text + "','" + txtvendorphone1.Text + "','" + txtvendorphone2.Text + "','" + txtvendordesignation.Text + "','" + txt_gst.Text + "','" + txtvendorpan.Text + "','" + txtvendorlst.Text + "','" + txtvendorcst.Text + "','" + txtvendortin.Text + "','" + txtvendorlbt.Text + "','" + txtvendorservictax.Text + "','" + txtvendoradd2.Text + "','" + ddlstatus.SelectedValue.ToString() + "','" + ddltype.SelectedValue.ToString() + "','" + txtvendortotaldues.Text + "','" + txtopeningbalance.Text + "','" + txtbillattention.Text + "','" + txtbilladdress.Text + "','" + txtbillcity.SelectedValue.ToString() + "','" + txtbillstate.SelectedValue.ToString() + "','" + txtbillzipcode.Text + "','" + txtbillcountry.Text + "','" + txtbillfax.Text + "','" + txtsattention.Text + "','" + txtsaddress.Text + "','" + txtscity.SelectedValue.ToString() + "','" + txtssstate.SelectedValue.ToString() + "','" + txtszipcode.Text + "','" + txtscountry.Text + "','" + txtsfax.Text + "','" + ddl_vendor_type.SelectedValue + "','" + txt_c_1.Text + "','" + txt_c_2.Text + "','" + txt_email.Text + "','" + txt_area.Text + "', str_to_date('" + txt_start.Text + "','%d/%m/%Y'),str_to_date('" + txt_end.Text + "','%d/%m/%Y'),'" + txt_saccode.Text + "','" + txt_hsmcode.Text + "','" + txt_vendor_bank_acc_name.Text + "','" + txt_acc_no.Text + "','" + txt_ifsc_code.Text + "','" + txt_bank_name.Text + "','" + txt_credit_period.Text + "','" + txt_regi_no.Text + "','" + ddl_vendor_nation.SelectedValue + "')");
                d1.operation("DELETE FROM pay_vendor_contact_person WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND VEND_ID='" + txtvendorid.Text + "' ");
                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    System.Web.UI.WebControls.Label lbl_srnumber = (System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber");
                    int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                    System.Web.UI.WebControls.DropDownList lbl_samulation = (System.Web.UI.WebControls.DropDownList)row.FindControl("lbl_samulation");
                    string lbl_samulation1 = lbl_samulation.Text.ToString();
                    System.Web.UI.WebControls.TextBox lbltxtfirstname = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtfirstname");
                    string lbltxtfirstname1 = lbltxtfirstname.Text.ToString();
                    System.Web.UI.WebControls.TextBox lbltxtlastname = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtlastname");
                    string lbltxtlastname1 = lbltxtlastname.Text.ToString();
                    System.Web.UI.WebControls.TextBox lbltxteaddress = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxteaddress");
                    string lbltxteaddress1 = lbltxteaddress.Text;
                    System.Web.UI.WebControls.TextBox lbltxtworkphonno = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtworkphonno");
                    string lbltxtworkphonno1 = lbltxtworkphonno.Text;
                    System.Web.UI.WebControls.TextBox lbltxtmobileno = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtmobileno");
                    string lbltxtmobileno1 = lbltxtmobileno.Text;
                    System.Web.UI.WebControls.TextBox lbltxtdesignation1 = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtdesignation1");
                    string lbltxtdesignation11 = lbltxtdesignation1.Text;
                    System.Web.UI.WebControls.TextBox lbldepartment = (System.Web.UI.WebControls.TextBox)row.FindControl("lbldepartment");
                    string lbldepartment1 = lbldepartment.Text.ToString();
                    resins1 = resins1 + d1.operation("INSERT INTO pay_vendor_contact_person(COMP_CODE,VEND_ID,SR_NO,salutation,txtfirstname,txtlastname,txteaddress,txtworkphonno,txtmobileno,txtdesignation1,txtdept) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + sr_number + "','" + lbl_samulation1 + "','" + lbltxtfirstname1 + "','" + lbltxtlastname1 + "','" + lbltxteaddress1 + "','" + lbltxtworkphonno1 + "','" + lbltxtmobileno1 + "','" + lbltxtdesignation11 + "','" + lbldepartment1 + "')");
                    //resins1 = resins1 + d1.operation("INSERT INTO pay_vendor_contact_person(COMP_CODE,VEND_ID,SR_NO,salutation,txtfirstname,txtlastname,txteaddress,txtworkphonno,txtmobileno,txtdesignation1,txtdept) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + sr_number + "','" + lbl_samulation1 + "','" + lbltxtfirstname1 + "','" + lbltxtlastname1 + "','" + lbltxteaddress1 + "','" + lbltxtworkphonno1 + "','" + lbltxtmobileno1 + "','" + lbltxtdesignation11 + "','" + lbldepartment1 + "')");
                }
             

                IEnumerable<string> selectedValues = from item in ddl_notassign_city.Items.Cast<ListItem>()
                                                     where item.Selected
                                                     select item.Value;
                string listvalues_ddl_unitclient = string.Join(",", selectedValues);

                var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);


                foreach (string city in elements)
                {
                    d1.operation("INSERT INTO pay_vendor_city_details (COMP_CODE,Vendor_Id,state,city,flag)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','"+ddl_state.SelectedValue+"','" + city + "','0')");
                }

                /// for client assign to po komal 07-04-2020

                IEnumerable<string> selectedValues2 = from item in list_client_not_assign.Items.Cast<ListItem>()
                                                      where item.Selected
                                                      select item.Value;
                string listvalues_client1 = string.Join(",", selectedValues2);

                var elements2 = listvalues_client1.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);


                foreach (string client1 in elements2)
                {
                    string client_name = d.getsinglestring("select DISTINCT client_name from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + client1 + "'");
                    d1.operation("INSERT INTO pay_vendor_client_assign (COMP_CODE,VEND_ID,VEND_NAME,client_name,client_code)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + txtvendorname.Text + "','" + client_name + "','" + client1 + "')");
                }

                assign_client();
                not_assign_client();

                ///////

                if (resins > 0 && (TotalRows == resins1))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Added Successfully...');", true);//vikas16/11

                    text_Clear();
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Adding Failed...');", true);//vikas16/11
                    text_Clear();

                }
            }

            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter One Contact Person Details...')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter At Least One Contact Person Details...');", true);//vikas16/11
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ///komal 25-04-2020  client assign
            list_client_not_assign.Items.Clear();
            list_client_assign.Items.Clear();
            //
            not_assign_client_new(); 

            vendor_table();
            btnnew_Click();
            personal();
        }
    }

    protected void gv_statewise_gst_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
    protected void VendorGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.VendorGridView, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
        // e.Row.Cells[2].Visible = false;

    }

    protected void VendorGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        gv_company_bank.DataSource = null;
        gv_company_bank.DataBind();

        System.Web.UI.WebControls.Label lbl_doccode = (System.Web.UI.WebControls.Label)VendorGridView.SelectedRow.FindControl("lbl_vender_id");
        string doc_no = lbl_doccode.Text;
        MySqlCommand cmd1 = new MySqlCommand("SELECT VEND_ID,Salutation, txtfirstname,txtlastname,txteaddress,txtworkphonno ,txtmobileno, txtdesignation1,txtdept FROM pay_vendor_contact_person WHERE (VEND_ID ='" + doc_no + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "') ORDER BY SR_NO ", d1.con);

        d1.conopen();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt;
                    gv_itemslist.DataSource = dt;
                    gv_itemslist.DataBind();
                    Panel3.Visible = true;
                    Panel4.Visible = true;
                    gv_itemslist.Visible = true;
                }

            }
            dr1.Close();
            d1.conclose();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.conclose(); }

        d2.con.Open();
        //try
        //{
            System.Web.UI.WebControls.Label lbl_doccode1 = (System.Web.UI.WebControls.Label)VendorGridView.SelectedRow.FindControl("lbl_vender_id");
            string doc_no1 = lbl_doccode1.Text;

            MySqlCommand cmd = new MySqlCommand("select VEND_ID,VEND_NAME,VEND_ADD1,PHONE1,PHONE2,DESIGNATION,GST,PAN_NO,LST_NO,CST_NO,TIN_NO,LBT_NO,SERVICE_TAX_NO,VEND_ADD2,ACTIVE_STATUS,TYPE,TOTAL_DUES,OPENING_BALANCE,txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillzipcode,txtbillcountry,txtbillfax,txtsattention,txtsaddress,txtssstate,txtszipcode,txtscountry,txtsfax,vendor_type ,contact_person_1,contact_person_2,vendor_email_id,area_to_served,agrement_start_date,agrement_end_date,sac_code,hsm_code,bank_account_name,account_number,ifsc_code,bank_name,CREDIT_PERIOD,comp_registration_num,vandor_type_nation,`vendor_type` from pay_vendor_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND VEND_ID='" + doc_no1 + "'", d2.con);

            MySqlDataReader adp = cmd.ExecuteReader();
            if (adp.Read())
            {

                txtvendorid.Text = lbl_doccode1.Text;
                txtvendorname.Text = adp.GetValue(1).ToString();
                txtvendoradd1.Text = adp.GetValue(2).ToString();
                txtvendorphone1.Text = adp.GetValue(3).ToString();
                txtvendorphone2.Text = adp.GetValue(4).ToString();
                //txtvendoradd2.Text = VendorGridView.SelectedRow.Cells[7].Text;
                txtvendordesignation.Text = adp.GetValue(5).ToString();
                txt_gst.Text = adp.GetValue(6).ToString();
                txtvendorpan.Text = adp.GetValue(7).ToString();
                txtvendorlst.Text = adp.GetValue(8).ToString();
                txtvendorcst.Text = adp.GetValue(9).ToString();
                txtvendortin.Text = adp.GetValue(10).ToString();
                txtvendorlbt.Text = adp.GetValue(11).ToString();
                txtvendorservictax.Text = adp.GetValue(12).ToString();
                txtvendoradd2.Text = adp.GetValue(13).ToString();
                ddlstatus.Text = adp.GetValue(14).ToString();
                ddltype.SelectedValue = adp.GetValue(15).ToString();
                txtvendortotaldues.Text = adp.GetValue(16).ToString();
                txtopeningbalance.Text = adp.GetValue(17).ToString();
                txtbillattention.Text = adp.GetValue(18).ToString();
                txtbilladdress.Text = adp.GetValue(19).ToString();
                txtbillstate.SelectedValue = adp.GetValue(21).ToString();
            get_city_list(null, null);
                txtbillcity.SelectedValue = adp.GetValue(20).ToString();

   
         
            txtbillzipcode.Text = adp.GetValue(22).ToString();
            txtbillcountry.Text = adp.GetValue(23).ToString();
            txtbillfax.Text = adp.GetValue(24).ToString();
            txtsattention.Text = adp.GetValue(25).ToString();
            txtsaddress.Text = adp.GetValue(26).ToString();
            //txtscity.SelectedValue = adp.GetValue(27).ToString();
            txtssstate.SelectedValue = adp.GetValue(27).ToString();
         get_city_list_shipping(null, null);
           
           
            txtszipcode.Text = adp.GetValue(28).ToString();
            txtscountry.Text = adp.GetValue(29).ToString();
            txtsfax.Text = adp.GetValue(30).ToString();
            ddl_vendor_type.SelectedValue = adp.GetValue(31).ToString();
            txt_c_1.Text = adp.GetValue(32).ToString();
            txt_c_2.Text = adp.GetValue(33).ToString();
            txt_email.Text = adp.GetValue(34).ToString();
            txt_area.Text = adp.GetValue(35).ToString();
            txt_start.Text = adp.GetValue(36).ToString();
            txt_end.Text = adp.GetValue(37).ToString();
            txt_saccode.Text = adp.GetValue(38).ToString();
            txt_hsmcode.Text = adp.GetValue(39).ToString();
            txt_vendor_bank_acc_name.Text = adp.GetValue(40).ToString();
            txt_acc_no.Text = adp.GetValue(41).ToString();
            txt_ifsc_code.Text = adp.GetValue(42).ToString();
            txt_bank_name.Text = adp.GetValue(43).ToString();
            txt_credit_period.Text = adp.GetValue(44).ToString();
            txt_regi_no.Text = adp.GetValue(45).ToString();
            ddl_vendor_nation.SelectedValue= adp.GetValue(46).ToString();

            txt_v_type.SelectedValue = adp.GetValue(47).ToString();
            
        btnadd.Visible = false;
        btnupdate.Visible = true;
        btndelete.Visible = true;
        adp.Close();
        d2.con.Close();


        MySqlDataAdapter cad1;
       
            cad1 = new MySqlDataAdapter("select city from pay_vendor_city_details where comp_code='" + Session["comp_code"].ToString() + "' and Vendor_Id='" + txtvendorid.Text + "' and flag='0' ", d.con);
       

        DataSet ds1 = new DataSet();

        cad1.Fill(ds1);
        ddl_assign_city.DataSource = ds1.Tables[0];
        ddl_assign_city.DataValueField = "city";
        ddl_assign_city.DataTextField = "city";
        ddl_assign_city.DataBind();

        cad1.Dispose();
        d.con.Close();

        assign_client();
        not_assign_client();
    }
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        company_bank_details();
        rate_gridview();
        attachment_gridview();
        
  }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string compcode = Session["COMP_CODE"].ToString();
        int resupdt = 0;
        int resins1 = 0;
        int TotalRows = gv_itemslist.Rows.Count;
        VendorBAL vb2 = new VendorBAL();
        try
        {
            resupdt = vb2.VendorUpdate(compcode, txtvendorid.Text, txtvendorname.Text, txtvendoradd1.Text, txtvendorphone1.Text, txtvendorphone2.Text, txtvendordesignation.Text, txt_gst.Text, txtvendorpan.Text, txtvendorlst.Text, txtvendorcst.Text, txtvendortin.Text, txtvendorlbt.Text, txtvendorservictax.Text, txtvendoradd2.Text, ddlstatus.SelectedValue.ToString(), ddltype.SelectedValue.ToString(), txtvendortotaldues.Text, txtopeningbalance.Text, txtbillattention.Text, txtbilladdress.Text, txtbillcity.SelectedValue.ToString(), txtbillstate.SelectedValue.ToString(), txtbillzipcode.Text, txtbillcountry.Text, txtbillfax.Text, txtsattention.Text, txtsaddress.Text, txtscity.Text, txtssstate.Text, txtszipcode.Text, txtscountry.Text, txtsfax.Text, ddl_vendor_type.SelectedValue, txt_c_1.Text, txt_c_2.Text, txt_email.Text, txt_area.Text, txt_start.Text, txt_end.Text, txt_saccode.Text, txt_hsmcode.Text, txt_vendor_bank_acc_name.Text, txt_acc_no.Text, txt_ifsc_code.Text, txt_bank_name.Text, txt_credit_period.Text, txt_regi_no.Text, ddl_vendor_nation.SelectedValue);
            int result_del = d1.operation("DELETE FROM pay_vendor_contact_person WHERE COMP_CODE='" + compcode + "' AND VEND_ID='" + txtvendorid.Text + "' ");//delete command
            foreach (GridViewRow row in gv_itemslist.Rows)
            // for (rownum = 0; rownum < maintable.Rows.Count; rownum++)
            {
                System.Web.UI.WebControls.Label lbl_srnumber = (System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber");
                int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                System.Web.UI.WebControls.DropDownList lbl_samulation = (System.Web.UI.WebControls.DropDownList)row.FindControl("lbl_samulation");
                string lbl_samulation1 = lbl_samulation.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxtfirstname = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtfirstname");
                string lbltxtfirstname1 = lbltxtfirstname.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxtlastname = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtlastname");
                string lbltxtlastname1 = lbltxtlastname.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxteaddress = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxteaddress");
                string lbltxteaddress1 = lbltxteaddress.Text;
                System.Web.UI.WebControls.TextBox lbltxtworkphonno = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtworkphonno");
                string lbltxtworkphonno1 = lbltxtworkphonno.Text;
                System.Web.UI.WebControls.TextBox lbltxtmobileno = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtmobileno");
                string lbltxtmobileno1 = lbltxtmobileno.Text;
                System.Web.UI.WebControls.TextBox lbltxtdesignation1 = (System.Web.UI.WebControls.TextBox)row.FindControl("lbltxtdesignation1");
                string lbltxtdesignation11 = lbltxtdesignation1.Text;
                System.Web.UI.WebControls.TextBox lbldepartment = (System.Web.UI.WebControls.TextBox)row.FindControl("lbldepartment");
                string lbldepartment1 = lbldepartment.Text.ToString();
                resins1 = resins1 + d1.operation("INSERT INTO pay_vendor_contact_person(COMP_CODE,VEND_ID,SR_NO,salutation,txtfirstname,txtlastname,txteaddress,txtworkphonno,txtmobileno,txtdesignation1,txtdept) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + sr_number + "','" + lbl_samulation1 + "','" + lbltxtfirstname1 + "','" + lbltxtlastname1 + "','" + lbltxteaddress1 + "','" + lbltxtworkphonno1 + "','" + lbltxtmobileno1 + "','" + lbltxtdesignation11 + "','" + lbldepartment1 + "')");
            }

           

            IEnumerable<string> selectedValues = from item in ddl_notassign_city.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_unitclient = string.Join(",", selectedValues);

            var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);


            foreach (string city in elements)
            {
                d1.operation("INSERT INTO pay_vendor_city_details (COMP_CODE,Vendor_Id,state,city,flag)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + ddl_state.SelectedValue + "','" + city + "','0')");
            }


            //// client assign for vendor komal 06-04-2020

            IEnumerable<string> selectedValues1 = from item in list_client_not_assign.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_client = string.Join(",", selectedValues1);

            var elements1 = listvalues_ddl_client.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string client1 in elements1)
            {
                string client_name = d.getsinglestring("select DISTINCT client_name from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + client1 + "'");
                d1.operation("INSERT INTO pay_vendor_client_assign (COMP_CODE,VEND_ID,VEND_NAME,client_name,client_code)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txtvendorid.Text + "','" + txtvendorname.Text + "','" + client_name + "','" + client1 + "')");
            }

            assign_client();
            not_assign_client();

            /////////

            if (resupdt > 0 && (TotalRows == resins1))
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Vendor (" + txtvendorname.Text + ") Updated Successfully...')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vendor (" + txtvendorname.Text + ") Updated Successfully...');", true);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Updated Successfully...')", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            vendor_table();
            text_Clear();
            ddl_notassign_city.Items.Clear();
            ddl_assign_city.Items.Clear();

            ///komal 25-04-2020  client assign
            list_client_not_assign.Items.Clear();
            list_client_assign.Items.Clear();
            //
            not_assign_client_new();

            btnnew_Click();
            Panel3.Visible = false;
            btnadd.Visible = true;
            gv_client_state.DataSource = null;
            gv_client_state.DataBind();
        }

    }
    protected void btn_shipping_address_Click(object sender, EventArgs e)
    {
        txtsattention.Text = txtbillattention.Text;
        txtsaddress.Text = txtbilladdress.Text;
        txtscity.SelectedValue = txtbillcity.SelectedValue;
        txtssstate.SelectedValue = txtbillstate.SelectedValue;
        txtszipcode.Text = txtbillzipcode.Text;
        txtscountry.Text = txtbillcountry.Text;
        txtsfax.Text = txtbillfax.Text;

    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string compcode = Session["COMP_CODE"].ToString();
        int resdel = 0;
        int resins1 = 0;
        VendorBAL vb3 = new VendorBAL();
        try
        {
            //////// komal 08-04-2020 client assign

            //IEnumerable<string> selectedValues = from item in list_client_assign.Items.Cast<ListItem>()
            //                                     where item.Selected
            //                                     select item.Value;
            //string listvalues_ddl_unitclient = string.Join(",", selectedValues);
            //var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            //if (elements.Length != 0)
            //{
            //    //string reporting_emp_series = d.reporting_emp_series(Session["COMP_CODE"].ToString(),ddl_employee_type.SelectedValue);
            //    foreach (string client in elements)
            //    {
            //        int res = 0;
            //        // string state_name = d.getsinglestring("select state_name from pay_unit_master where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND client_code='"+ddl_client_name.SelectedValue+"' AND UNIT_CODE= '" + branch + "'");
            //        res = d.operation("delete from pay_vendor_client_assign where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + client + "' and `VEND_ID` = '" + txtvendorid.Text + "'");

            //        if (res > 0)
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Deleted Successfully...')", true);
            //            text_Clear();
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Deletion Failed !!')", true);
            //            text_Clear();
            //        }

            //    }


            //}

            ////////
            MySqlCommand cmd_1 = new MySqlCommand("select * from  PAY_TRANSACTIONP WHERE CUST_CODE='" + txtvendorid.Text + "'", d1.con1);
            d1.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vendor(" + txtvendorname.Text + ") cannot delete due to Purchase Transaction Exist.');", true);
                text_Clear();
            }
            else
            {
                resdel = vb3.VendorDelete(compcode, txtvendorid.Text);
                resins1 = d1.operation("DELETE FROM pay_vendor_contact_person WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND VEND_ID='" + txtvendorid.Text + "' ");
                if (resdel > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Deleted Successfully...')", true);
                    text_Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Deletion Failed !!')", true);
                    text_Clear();
                }
            }
            dr_1.Close();
            d1.con1.Close();
        }
        catch (Exception ee)
        {
            //throw ee;
            //string error = ee.Message.ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vendor Deletion Failed : (" + error + ") ...');", true);
        }
        finally
        {
            d1.con1.Close();
            Panel3.Visible = false;

            vendor_table();
            d1.con1.Close();
            text_Clear();
            btnnew_Click();
            personal();
            btnadd.Visible = true;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        text_Clear();
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }



    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 15;
        ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        ws.Cells[2, 5] = "Vendor Details";
        //ws.Cells[3, 5] = "Monthly Bonus Statement For Month " + Session["CURRENT_PERIOD"].ToString();
        ws.Cells[5, 1] = "VEND_ID";
        ws.Cells[5, 2] = "VEND_NAME ";
        ws.Cells[5, 3] = "VEND_ADD1 ";
        ws.Cells[5, 4] = "PHONE1 ";
        ws.Cells[5, 5] = "PHONE2";
        //  ws.Cells[5, 6] = "DESIGNATION";
        ws.Cells[5, 6] = "GST";
        ws.Cells[5, 7] = "PAN_NO";
        ws.Cells[5, 8] = "LST_NO";
        ws.Cells[5, 9] = "CST_NO";
        ws.Cells[5, 10] = "TIN_NO";
        ws.Cells[5, 11] = "LBT_NO";
        ws.Cells[5, 12] = "SERVICE_TAX_NO";
        ws.Cells[5, 13] = "VEND_ADD2";
        ws.Cells[5, 14] = "ACTIVE_STATUS";
        ws.Cells[5, 15] = "TYPE";
        ws.Cells[5, 16] = "TOTAL_DUES";
        ws.Cells[5, 17] = "OPENING_BALANCE";
        ws.Cells[5, 18] = "Attentation";
        ws.Cells[5, 19] = "Bill Address";
        ws.Cells[5, 20] = "Bill City";
        ws.Cells[5, 21] = "Bill State";
        ws.Cells[5, 22] = "Bill Pincode";
        ws.Cells[5, 23] = "Bill Country";
        ws.Cells[5, 24] = "Bill FaxNO";
        ws.Cells[5, 25] = "Shipping Attentation";
        ws.Cells[5, 26] = "Shippingv Address";
        ws.Cells[5, 27] = "Shipping City";
        ws.Cells[5, 28] = "Shipping Satet";
        ws.Cells[5, 29] = "Shipping ZipCode";
        ws.Cells[5, 30] = "Shipping Country";
        ws.Cells[5, 31] = "Shipping Tax";

        try
        {
            d1.con1.Open();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("Select VEND_ID,VEND_NAME,VEND_ADD1,PHONE1,PHONE2,GST,PAN_NO,LST_NO,CST_NO,TIN_NO,LBT_NO,SERVICE_TAX_NO,VEND_ADD2,ACTIVE_STATUS,TYPE,TOTAL_DUES,OPENING_BALANCE,txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillzipcode,txtbillcountry,txtbillfax,txtsattention,txtsaddress,txtscity,txtssstate,txtszipcode,txtscountry,txtsfax from pay_vendor_master Where COMP_CODE ='" + Session["COMP_CODE"] + "'", d1.con1);
            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 6;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[j, i + 1] = row[i].ToString();
                }
                j++;
            }

            xla.Visible = true;
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d1.con1.Close();
            text_Clear();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void text_Clear()
    {
        txtvendorid.Text = "";
        txt_credit_period.Text = "";
        txtvendorname.Text = "";
        txtvendoradd1.Text = "";
        txtvendorphone1.Text = "";
        txtvendorphone2.Text = "";
        txtvendordesignation.Text = "";
        txt_gst.Text = "";
        txtvendorpan.Text = "";
        txtvendorlst.Text = "";
        txtvendorcst.Text = "";
        txtvendortin.Text = "";
        txtvendorlbt.Text = "";
        txtvendorservictax.Text = "";
        txtvendoradd2.Text = "";
        ddlstatus.SelectedIndex = 0;
        //ddltype.SelectedIndex = 0;
        txtvendortotaldues.Text = "0";
        txtopeningbalance.Text = "0";
        txtbillattention.Text = "";
        txtbilladdress.Text = "";
        txtbillcity.SelectedIndex = 0;
        txtbillstate.SelectedIndex = 0;
        txtbillzipcode.Text = "";
        txtbillcountry.Text = "";
        txtbillfax.Text = "";
        txtsattention.Text = "";
        txtsaddress.Text = "";
        // txtscity.Text = "";
        //txtssstate.SelectedValue = "";
        txtszipcode.Text = "";
        txtscountry.Text = "";
        txtsfax.Text = "";
        txt_email.Text = "";
        txt_start.Text = "";
        txt_end.Text = "";
        txt_saccode.Text = "";
        txt_vendor_bank_acc_name.Text = "";
        txt_acc_no.Text = "";
        txt_ifsc_code.Text = "";
        txt_bank_name.Text = "";
        txt_area.Text = "";
        txt_c_1.Text = "";
        txt_c_2.Text = "";
        txt_hsmcode.Text = "";
        txt_regi_no.Text = "";
        ddl_vendor_type.SelectedValue = "0";
        gv_itemslist.Visible = false;
        btnnew_Click();
        ddl_vendor_nation.SelectedValue = "0";
        gv_company_bank.DataSource = null;
        gv_company_bank.DataBind();
        
        /// komal 08-04-2020 client assign

        //ddl_vendor_name.SelectedValue = "Select";
        //list_client_assign.Text = "";
        //list_client_not_assign.Text = "";



        // for client assign komal 06-04-2020

      //  ddl_vendor_name.SelectedValue = "Select";
       // list_client_not_assign.Text = "";
    }
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
    }
    public void personal()
    {
        lst_simu.SelectedValue = "Select Salutation";
        txtfirstname.Text = "";
        txtlastname.Text = "";
        txteaddress.Text = "";
        txtworkphonno.Text = "";
        txtmobileno.Text = "";
        txtdesignation1.Text = "";
        txtdept.Text = "";
        //txtvendorid.Text = "";
        Panel4.Visible = false;
        Panel3.Visible = false;
        //ddl_vendor_nation.SelectedValue = "0";
    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {

        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        System.Data.DataTable dt = new System.Data.DataTable();
        //DataColumn dt, Columns;
        // DataRow dr1;
        DataRow dr;
        dt.Columns.Add("salutation");
        dt.Columns.Add("txtfirstname");
        dt.Columns.Add("txtlastname");
        dt.Columns.Add("txteaddress");
        dt.Columns.Add("sac_code");//vikas remove comment
        dt.Columns.Add("txtworkphonno");
        dt.Columns.Add("txtmobileno");
        dt.Columns.Add("txtdesignation1");
        dt.Columns.Add("txtdept");

        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                System.Web.UI.WebControls.DropDownList lblsalutation = (System.Web.UI.WebControls.DropDownList)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_samulation");
                dr["Salutation"] = lblsalutation.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxtfirstname = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbltxtfirstname");
                dr["txtfirstname"] = lbltxtfirstname.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxtlastname = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbltxtlastname");
                dr["txtlastname"] = lbltxtlastname.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxteaddress = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbltxteaddress");
                dr["txteaddress"] = lbltxteaddress.Text.ToString();
                System.Web.UI.WebControls.TextBox lbltxtworkphonno = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbltxtworkphonno");
                dr["txtworkphonno"] = lbltxtworkphonno.Text;
                System.Web.UI.WebControls.TextBox lbltxtmobileno = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[7].FindControl("lbltxtmobileno");
                dr["txtmobileno"] = lbltxtmobileno.Text;
                System.Web.UI.WebControls.TextBox lbltxtdesignation1 = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[8].FindControl("lbltxtdesignation1");
                dr["txtdesignation1"] = lbltxtdesignation1.Text;
                System.Web.UI.WebControls.TextBox lbltxtdept = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[9].FindControl("lbldepartment");
                dr["txtdept"] = lbltxtdept.Text;
                dt.Rows.Add(dr);

            }
        }

        dr = dt.NewRow();
        dr["salutation"] = lst_simu.Text.ToString();
        dr["txtfirstname"] = txtfirstname.Text.ToString();
        dr["txtlastname"] = txtlastname.Text.ToString();
        dr["txteaddress"] = txteaddress.Text.ToString();
        dr["sac_code"] = txt_saccode.Text.ToString();//vik cc
        dr["txtworkphonno"] = txtworkphonno.Text;
        dr["txtmobileno"] = txtmobileno.Text;
        dr["txtdesignation1"] = txtdesignation1.Text;
        dr["txtdept"] = txtdept.Text;
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        ViewState["CurrentTable"] = dt;

        //Panel3.Visible = false;
        if (gv_itemslist.Rows.Count > 0)
        {
            // btnadd.Visible = true;
            // btnupdate.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = true;
            gv_itemslist.Visible = true;
        }
        else
        {
            // btnadd.Visible = true;
            //btnupdate.Visible = false;
        }

        gv_itemslist.Visible = true;
        lst_simu.SelectedIndex = 0;
        txtfirstname.Text = "";
        txtlastname.Text = "";
        txteaddress.Text = "";
        txtworkphonno.Text = "";
        txtmobileno.Text = "";
        txtdesignation1.Text = "";
        txtdept.Text = "";
        //btnadd.Visible = true;
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
        }


    }

    protected void get_city_list(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        get_city();

    }
    public void get_city()
    {

        txtbillcity.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT city from pay_state_master where STATE_CODE='" + txtbillstate.SelectedValue + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txtbillcity.DataSource = dt_item;
                txtbillcity.DataTextField = dt_item.Columns[0].ToString();
                // txtbillcity.SelectedValue = null;
                txtbillcity.DataBind();
            }
            dt_item.Dispose();
            d1.con.Close();
            txtbillcity.Items.Insert(0, "Select");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

    }
    protected void get_city_list_shipping(object sender, EventArgs e)
    {

        get_city_shipping();
    }
    public void get_city_shipping()
    {
        txtscity.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT city from pay_state_master where STATE_CODE='" + txtssstate.SelectedValue + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txtscity.DataSource = dt_item;
                txtscity.DataTextField = dt_item.Columns[0].ToString();

                txtscity.DataBind();
            }
            dt_item.Dispose();
            d1.con.Close();
            txtscity.Items.Insert(0, "Select");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

    }
    protected void VendorGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            VendorGridView.UseAccessibleHeader = false;
            VendorGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    public void vendor_table()
    {
        DataSet ds_vend_gv = new DataSet();

        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT distinct `VEND_ID`,COMP_CODE, VEND_NAME, PHONE1,`STATE_NAME` as 'txtbillstate',txtbillcity, GST,`sac_code` FROM `pay_vendor_master`  INNER JOIN `pay_state_master` ON `pay_vendor_master`.`txtbillstate` = `pay_state_master`.`STATE_CODE` WHERE(COMP_CODE = '" + Session["COMP_CODE"].ToString() + "') ORDER BY VEND_ID desc", d1.con1);
        if (d1.con1.State == ConnectionState.Open)
        {
            d1.con1.Close();
            d1.con1.Dispose();
            d1.con1.ClearPoolAsync(d1.con1);
        }
        d1.con1.Open();
        try
        {
            MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
            adp_vend_gv.Fill(ds_vend_gv);
            VendorGridView.DataSource = ds_vend_gv;
            VendorGridView.DataBind();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }
    }

    protected void ddl_typevendor_SelectedIndex(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        DataSet ds_vend_gv = new DataSet();

        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT COMP_CODE, VEND_ID, VEND_NAME, PHONE1,txtbillstate,txtbillcity,  GST FROM pay_vendor_master WHERE(COMP_CODE = '" + Session["COMP_CODE"].ToString() + "') and vendor_type='" + ddl_typevendor.SelectedItem.Text + "' ORDER BY VEND_ID", d1.con1);
        if (d1.con1.State == ConnectionState.Open)
        {
            d1.con1.Close();
            d1.con1.Dispose();
            d1.con1.ClearPoolAsync(d1.con1);
        }
        d1.con1.Open();
       
            MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
            adp_vend_gv.Fill(ds_vend_gv);
            VendorGridView.DataSource = ds_vend_gv;
            VendorGridView.DataBind();
            d1.con1.Close();
        }
    
    protected void lnk_add_state_Click(object sender, EventArgs e)
    {
       // gv_client_state.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("state");
        dt.Columns.Add("city");
      
        int rownum = 0;
        for (rownum = 0; rownum < gv_client_state.Rows.Count; rownum++)
        {
            if (gv_client_state.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["state"] = gv_client_state.Rows[rownum].Cells[2].Text;
                dr["city"] = gv_client_state.Rows[rownum].Cells[3].Text;
              

                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["state"] = ddl_state.SelectedItem.Text;
        dr["city"] = ddl_city.SelectedValue;

        dt.Rows.Add(dr);
        gv_client_state.DataSource = dt;
        gv_client_state.DataBind();

        ViewState["emailtable"] = dt;

        ddl_state.SelectedValue = "Select";
        ddl_city.SelectedValue = "Select";
       
    }

    protected void lnk_remove_mail_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["emailtable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["emailtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["emailtable"] = dt;
            gv_client_state.DataSource = dt;
            gv_client_state.DataBind();
        }
    }

    protected void rate_gridview() 
    {
        try
        {
            gv_rate_master.DataSource = null;
            gv_rate_master.DataBind();

            MySqlDataAdapter attachment = null;
            attachment = new MySqlDataAdapter("select id,item_type,item_code,item_size,rate from pay_rate_chart_master ", d.con);

            d.con.Open();
            System.Data.DataTable dt_rate = new System.Data.DataTable();
            attachment.Fill(dt_rate);
            if (dt_rate.Rows.Count > 0)
            {

                ViewState["rate_Type"] = dt_rate;


                gv_rate_master.DataSource = dt_rate;
                gv_rate_master.DataBind();


            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    
    
    }




    protected void attachment_gridview()
    {

        try

        {
            gridview_wait_master.DataSource = null;
            gridview_wait_master.DataBind();

            MySqlDataAdapter attachment = null;
            attachment = new MySqlDataAdapter("select id,courier_type,wait,from_wait,to_wait,from_km,to_km,rate from pay_wait_chart_master ", d.con);

            d.con.Open();
            System.Data.DataTable dt_attach = new System.Data.DataTable();
            attachment.Fill(dt_attach);
            if (dt_attach.Rows.Count > 0)
            {

                ViewState["wait_Type"] = dt_attach;


                gridview_wait_master.DataSource = dt_attach;
                gridview_wait_master.DataBind();


            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }

    protected void link_wait_master_Click(object sender, EventArgs e)
    {
        try {

            hidtab.Value = "6";
            int res = d.operation("insert into pay_wait_chart_master(courier_type,wait,from_wait,to_wait,from_km,to_km,rate)values('" + ddl_courier_type.SelectedValue + "','" + ddl_wait_type.SelectedValue + "','" + ddl_from.SelectedValue + "','" + ddl_to.SelectedValue + "','" + ddl_from_km.SelectedValue + "','" + ddl_to_km.SelectedValue + "','" + txt_rate.Text + "')  ");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Courier weight Successfully Added.. !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Courier weight Added Failed... !!!');", true);
            }

            attachment_gridview();

            ddl_courier_type.SelectedValue = "Select";
            ddl_wait_type.SelectedValue = "Select";
            ddl_from.SelectedValue = "Select";
            ddl_to.SelectedValue = "Select";
            ddl_from_km.SelectedValue = "Select";
            ddl_to_km.SelectedValue = "Select";
            txt_rate.Text = "";

        }
        catch (Exception ex) { throw ex; }

        finally { }

    }
  
    protected void gridview_wait_master_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[2].Visible = false;

    }
    protected void ddl_item_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value="7";
        System.Data.DataTable dt_item = new System.Data.DataTable();
        dt_item = new System.Data.DataTable();

        MySqlDataAdapter cmd_item =
        cmd_item = new MySqlDataAdapter("select ITEM_CODE from pay_item_master where `product_service` = '" + ddl_item_type.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_item_code.DataSource = dt_item;
                ddl_item_code.DataTextField = dt_item.Columns[0].ToString();
                ddl_item_code.DataValueField = dt_item.Columns[0].ToString();
                ddl_item_code.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_item_code.Items.Insert(0, "Select");

        
        
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            //ddl_state_SelectedIndexChanged(null, null);
        }




    }
    protected void ddl_item_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "7";
        
      //cmd_item = new MySqlDataAdapter("select ITEM_CODE from pay_item_master where ITEM_NAME = '" + ddl_item_type.SelectedValue + "'", d.con);
     
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select size from pay_item_master where product_service = '" + ddl_item_type.SelectedValue + "' and `ITEM_CODE`= '" + ddl_item_code.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_itemsize.Text = dr.GetValue(0).ToString();
               
            }
          
            dr.Close();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            //ddl_state_SelectedIndexChanged(null, null);
        }
    }
    protected void lnk_rate_master_Click(object sender, EventArgs e)
    {
        try
        {

            hidtab.Value = "7";
            int res = d.operation("insert into pay_rate_chart_master(item_type,item_code,item_size,rate)values('" + ddl_item_type.SelectedValue + "','" + ddl_item_code.SelectedValue + "','" + txt_itemsize.Text + "','" + txt_rate_mas.Text + "')");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Courier rate Successfully Added.. !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Courier rate Added Failed... !!!');", true);
            }

            rate_gridview();


            ddl_item_type.SelectedValue = "Select";
            ddl_item_code.SelectedValue = "Select";
            txt_itemsize.Text = "";
            txt_rate_mas.Text = "";
        }
        catch (Exception ex) { throw ex; }

        finally { }
    }
    protected void gv_rate_master_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        e.Row.Cells[2].Visible = false;
    }
    protected void lnk_remove_wait_Click(object sender, EventArgs e)
    {
        hidtab.Value = "6";

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_wait_chart_master where id = '" + grdrow.Cells[2].Text + "'");
        attachment_gridview();
    }
    protected void lnk_remove_rate_Click(object sender, EventArgs e)
    {
        hidtab.Value = "7";
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_rate_chart_master where id = '" + grdrow.Cells[2].Text + "'");
        rate_gridview();
    }
    protected void gridview_wait_master_PreRender(object sender, EventArgs e)
    {
        try
        {
            gridview_wait_master.UseAccessibleHeader = false;
            gridview_wait_master.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_rate_master_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_rate_master.UseAccessibleHeader = false;
            gv_rate_master.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
//	komal changes 24-04-2020 client assign

    protected void assign_client()
{
    MySqlDataAdapter cad2;

    cad2 = new MySqlDataAdapter("select client_name,client_code from pay_vendor_client_assign where comp_code = '" + Session["comp_code"].ToString() + "' and `VEND_ID`= '" + txtvendorid.Text + "' ", d.con);

    System.Data.DataTable dt_ve1 = new System.Data.DataTable();
    cad2.Fill(dt_ve1);
    if (dt_ve1.Rows.Count > 0)
    {
        list_client_assign.DataSource = dt_ve1;
        list_client_assign.DataTextField = dt_ve1.Columns[0].ToString();
        list_client_assign.DataValueField = dt_ve1.Columns[1].ToString();
        list_client_assign.DataBind();

    }


    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);


}



    //protected void ddl_vendor_name_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hidtab.Value = "8";
    //    d.con.Open();
    //    try
    //    {
    //        assign_client();
    //        MySqlDataAdapter cad1;

    //        cad1 = new MySqlDataAdapter("select distinct pay_client_master.client_name,pay_client_master.client_code from pay_client_master INNER JOIN `pay_client_billing_details` ON `pay_client_master`.`comp_code` = `pay_client_billing_details`.`comp_code` AND `pay_client_master`.`client_code` = `pay_client_billing_details`.`client_code` where pay_client_master.comp_code = '" + Session["comp_code"].ToString() + "' and `billing_id` = '2' and pay_client_master.client_code NOT IN(select client_code from pay_vendor_client_assign where comp_code='" + Session["comp_code"].ToString() + "' ) ", d.con);

    //        System.Data.DataTable dt_ve = new System.Data.DataTable();
    //        cad1.Fill(dt_ve);
    //        if (dt_ve.Rows.Count > 0)
    //        {
    //            list_client_not_assign.DataSource = dt_ve;
    //            list_client_not_assign.DataTextField = dt_ve.Columns[0].ToString();
    //            list_client_not_assign.DataValueField = dt_ve.Columns[1].ToString();
    //            list_client_not_assign.DataBind();

    //        }

           
           
           
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    
    //}

    

    protected void not_assign_client() 
    {
       // hidtab.Value = "8";
        d.con.Open();
        try
        {
           // assign_client();
            MySqlDataAdapter cad1;

            cad1 = new MySqlDataAdapter("select distinct pay_client_master.client_name,pay_client_master.client_code from pay_client_master INNER JOIN `pay_client_billing_details` ON `pay_client_master`.`comp_code` = `pay_client_billing_details`.`comp_code` AND `pay_client_master`.`client_code` = `pay_client_billing_details`.`client_code` where pay_client_master.comp_code = '" + Session["comp_code"].ToString() + "' and `billing_id` = '2' and pay_client_master.client_code NOT IN(select client_code from pay_vendor_client_assign where comp_code='" + Session["comp_code"].ToString() + "' ) ", d.con);

            System.Data.DataTable dt_ve = new System.Data.DataTable();
            cad1.Fill(dt_ve);
            if (dt_ve.Rows.Count > 0)
            {
                list_client_not_assign.DataSource = dt_ve;
                list_client_not_assign.DataTextField = dt_ve.Columns[0].ToString();
                list_client_not_assign.DataValueField = dt_ve.Columns[1].ToString();
                list_client_not_assign.DataBind();

            }

           
           
           
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    }

    /// 
    protected void not_assign_client_new()
    {
        d.con.Open();
        try
        {
            // assign_client();
            MySqlDataAdapter cad1;

            cad1 = new MySqlDataAdapter("select distinct pay_client_master.client_name,pay_client_master.client_code from pay_client_master INNER JOIN `pay_client_billing_details` ON `pay_client_master`.`comp_code` = `pay_client_billing_details`.`comp_code` AND `pay_client_master`.`client_code` = `pay_client_billing_details`.`client_code` where pay_client_master.comp_code = '" + Session["comp_code"].ToString() + "' and `billing_id` = '2' ", d.con);

            System.Data.DataTable dt_ve = new System.Data.DataTable();
            cad1.Fill(dt_ve);
            if (dt_ve.Rows.Count > 0)
            {
                list_client_not_assign.DataSource = dt_ve;
                list_client_not_assign.DataTextField = dt_ve.Columns[0].ToString();
                list_client_not_assign.DataValueField = dt_ve.Columns[1].ToString();
                list_client_not_assign.DataBind();

            }




            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
        }



    }




    // end 
    protected void btn_remove_client_assign_Click(object sender, EventArgs e)
    {
        //////// komal 08-04-2020 client assign

        IEnumerable<string> selectedValues = from item in list_client_assign.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Value;
        string listvalues_ddl_unitclient = string.Join(",", selectedValues);
        var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (elements.Length != 0)
        {
            //string reporting_emp_series = d.reporting_emp_series(Session["COMP_CODE"].ToString(),ddl_employee_type.SelectedValue);
            foreach (string client in elements)
            {
                int res = 0;
                // string state_name = d.getsinglestring("select state_name from pay_unit_master where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND client_code='"+ddl_client_name.SelectedValue+"' AND UNIT_CODE= '" + branch + "'");
                res = d.operation("delete from pay_vendor_client_assign where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + client + "' and `VEND_ID` = '" + txtvendorid.Text + "'");

                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Client Assign Removed Successfully...')", true);
                   // text_Clear();
                    //list_client_not_assign.Items.Clear();
                   // list_client_assign.Items.Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Client Assign Removed Failed !!')", true);
                    text_Clear();
                }

               
            }

            assign_client();
        }

        ////////
           

    }
    

 protected void ddl_payment_bank_SelectedIndexChanged(object sender, EventArgs e)
{
    hidtab.Value = "9";
    try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
    catch { }
   
    
    try
    {
        
        d.con.Open();
        txt_comp_ac_no.Text = "";
        
        MySqlCommand cmd_item = new MySqlCommand("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_payment_bank.SelectedValue + "'", d.con);
        MySqlDataReader dr = cmd_item.ExecuteReader();
        if (dr.Read())
        {
            txt_comp_ac_no.Text = dr.GetValue(0).ToString();
        }
    }
    catch (Exception ex) { }
    finally { d.con.Close(); }
}
    public void company_bank_load()
    {

        try
        {
            ddl_payment_bank.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE is null", d3.con);
            d3.con.Open();



            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_payment_bank.DataSource = dt_item;
                ddl_payment_bank.DataTextField = dt_item.Columns[0].ToString();
                ddl_payment_bank.DataValueField = dt_item.Columns[0].ToString();
                ddl_payment_bank.DataBind();
                d3.con.Close();
            }

            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_payment_bank.Items.Insert(0, new ListItem("Select"));
            d3.con.Close();
        }

    }
    protected void lnk_account_no_Click(object sender, EventArgs e)
    {
       
        hidtab.Value = "9";
        string new_file_name = "";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        string company_bank_check = d.getsinglestring("select `payment_ag_bank`,`company_ac_no` from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and vendor_id = '" + txtvendorid.Text + "' and payment_ag_bank='" + ddl_payment_bank.SelectedValue + "' and company_ac_no ='" + txt_comp_ac_no.Text + "' ");

        if (company_bank_check!="") 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('This Record Already Added  !!')", true);
            return;
        
        }
        if (upload_sheet.HasFile)
        {
            int res = 0;
            string fileExt = System.IO.Path.GetExtension(upload_sheet.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt == ".PDF" || fileExt == ".pdf" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".RAR" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(upload_sheet.PostedFile.FileName);
                upload_sheet.PostedFile.SaveAs(Server.MapPath("~/vendor_company/") + fileName);
                // string id = d.getsinglestring("select ifnull(max(id),0) from pay_material_details");

                string file_name = "" + ddl_payment_bank.SelectedValue + "_"+ txtvendorid.Text;
                 new_file_name = file_name + fileExt;

                File.Copy(Server.MapPath("~/vendor_company/") + fileName, Server.MapPath("~/vendor_company/") + new_file_name, true);
                File.Delete(Server.MapPath("~/vendor_company/") + fileName);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please First Select The File For Upload ');", true);
                return;
            }
        }
        int result = d.operation("insert into pay_company_bank_details (comp_code,vendor_id,payment_ag_bank,company_ac_no,file_upload)values('" + Session["comp_code"].ToString() + "','" + txtvendorid.Text + "','" + ddl_payment_bank.SelectedValue + "','" + txt_comp_ac_no.Text + "','" + new_file_name + "')");

        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Company Bank Details Successfully Added... !!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company Bank Details Addition Failed... !!!');", true);
        }
        company_bank_details();
    }

    protected void company_bank_details()
    {
        gv_company_bank.DataSource = null;
        gv_company_bank.DataBind();

        MySqlCommand cmd1 = new MySqlCommand("select id,payment_ag_bank,company_ac_no,file_upload,vendor_id from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and vendor_id = '" + txtvendorid.Text + "' ", d4.con);
        d4.con.Open();
        try
        {
             MySqlDataReader dr1 = cmd1.ExecuteReader();
             if (dr1.HasRows)
             {
                 System.Data.DataTable dt = new System.Data.DataTable();
                 dt.Load(dr1);
                 if (dt.Rows.Count > 0)
                 {
                     gv_company_bank.DataSource = dt;
                     gv_company_bank.DataBind();
                     gv_company_bank.Visible = true;
                 }
                 else
                 {
                     
                     gv_company_bank.DataSource = dt;
                     gv_company_bank.DataBind();
                 }
             }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d4.con.Close(); }
    }
    protected void gv_company_bank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }


        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void gv_company_bank_PreRender(object sender, EventArgs e)
    {

        try
        {
            gv_company_bank.UseAccessibleHeader = false;
            gv_company_bank.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void lnk_remove_com_bank_Click(object sender, EventArgs e)
    {
        hidtab.Value = "9";
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_company_bank_details where id = '" + grdrow.Cells[2].Text + "'");

        company_bank_details();
    }
   
    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        
        string unit_name = commandArgs[0];

        if (filename != "")
        {
            downloadfile_admini(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }
    }
    protected void downloadfile_admini(string filename, string unit_name)
    {
        var result = filename.Substring(filename.Length - 4);
        if (result.Contains("jpeg"))
        {
            result = ".jpeg";
        }
        try
        {
            string path2 = Server.MapPath("~\\vendor_company\\" + filename);
            string unitName = unit_name + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\vendor_company\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }
        catch (Exception ex) { }
    }
}