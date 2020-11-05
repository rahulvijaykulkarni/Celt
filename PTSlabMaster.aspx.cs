using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;

public partial class PTSlabMaster : System.Web.UI.Page
{
    DAL d = new DAL();
    public MySqlDataReader drmax = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d.getaccess(Session["ROLE"].ToString(), "PT- Slab Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "PT- Slab Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = false;
            btnexporttoexcelptslab.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "PT- Slab Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            btnexporttoexcelptslab.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "PT- Slab Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            btnexporttoexcelptslab.Visible = false;
        }

        if (!IsPostBack)
        {
            btn_clear_data(null, null);
            ddl_state.Items.Insert(0, new ListItem("Select State"));
            state();
            btn_esic_sheet.Visible = false;
            btn_pf_sheet.Visible = false;
            btn_update.Visible = false;
            gv_labourOffice();//vikas add 09/05/2019
            //btn_LWFNew_Click();
            btn_pfNew_Click();
            fill_pf_gv();
            fill_lwf_gv();
            ViewState["lwf_id"] = "";
            pnl_sys_upload.Visible = false;
            pnl_esic.Visible = false;
            // Client List
            ddl_esic_client_SelectedIndexChanged();
            //ddl_esic_client.Items.Clear();
            ddl_pf_client.Items.Clear();
            ddl_pt_client.Items.Clear();
            ddl_bclient.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  and client_active_close= '0' ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    //ddl_esic_client.DataSource = dt_item;
                    //ddl_esic_client.DataTextField = dt_item.Columns[0].ToString();
                    //ddl_esic_client.DataValueField = dt_item.Columns[1].ToString();
                    //ddl_esic_client.DataBind();

                    ddl_pf_client.DataSource = dt_item;
                    ddl_pf_client.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pf_client.DataValueField = dt_item.Columns[1].ToString();
                    ddl_pf_client.DataBind();

                    ddl_pt_client.DataSource = dt_item;
                    ddl_pt_client.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pt_client.DataValueField = dt_item.Columns[1].ToString();
                    ddl_pt_client.DataBind();

                    ddl_uan_client_list.DataSource = dt_item;
                    ddl_uan_client_list.DataTextField = dt_item.Columns[0].ToString();
                    ddl_uan_client_list.DataValueField = dt_item.Columns[1].ToString();
                    ddl_uan_client_list.DataBind();

                    // for pf_esic client

                    ddl_clientname_pf_esic.DataSource = dt_item;
                    ddl_clientname_pf_esic.DataTextField = dt_item.Columns[0].ToString();
                    ddl_clientname_pf_esic.DataValueField = dt_item.Columns[1].ToString();
                    ddl_clientname_pf_esic.DataBind();
                    
					//Bonous
					ddl_bclient.DataSource = dt_item;
                    ddl_bclient.DataTextField = dt_item.Columns[0].ToString();
                    ddl_bclient.DataValueField = dt_item.Columns[1].ToString();
                    ddl_bclient.DataBind();

                }
                dt_item.Dispose();
                d.con.Close();
                //  ddl_esic_client.Items.Insert(0, "ALL");
               // ddl_pf_client.Items.Insert(0, "ALL");
                ddl_pt_client.Items.Insert(0, "ALL");
                ddl_clientname_pf_esic.Items.Insert(0, "ALL");
				ddl_bclient.Items.Insert(0, "Select");
                ddl_pf_client.Items.Insert(0, new ListItem("Select"));
                ddl_uan_client_list.Items.Insert(0, new ListItem("ALL"));
                //ddl_unitcode.Items.Insert(0, "ALL");
                // ddl_esic_client_SelectedIndexChanged(null, null);
                //ddl_pf_client_SelectedIndexChanged(null, null);
                ddl_pt_client_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            state_laber_office();//vikas add 08/05/2019
        }

        // ddl_state.Items.Insert(0, new ListItem("Select State", ""));
    }

    public void state()
    {
        //Client State

        System.Data.DataTable dt_state = new System.Data.DataTable();
        MySqlDataAdapter adp_state = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d.con1);
        d.con1.Open();
        try
        {
            adp_state.Fill(dt_state);
            if (dt_state.Rows.Count > 0)
            {
                Select_lwf_state.DataSource = dt_state;
                Select_lwf_state.DataTextField = dt_state.Columns[0].ToString();
                Select_lwf_state.DataValueField = dt_state.Columns[1].ToString();
                Select_lwf_state.DataBind();
                ddl_lwf_state.DataSource = dt_state;
                ddl_lwf_state.DataTextField = dt_state.Columns[0].ToString();
                ddl_lwf_state.DataValueField = dt_state.Columns[0].ToString();
                ddl_lwf_state.DataBind();
            }
            dt_state.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            Select_lwf_state.Items.Insert(0, new ListItem("Select"));
            ddl_lwf_state.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
        }


        System.Data.DataTable dt_state1 = new System.Data.DataTable();
        MySqlDataAdapter adp_state1 = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d.con1);
        d.con1.Open();
        try
        {
            adp_state.Fill(dt_state1);
            if (dt_state1.Rows.Count > 0)
            {
                ddl_state.DataSource = dt_state;
                ddl_state.DataTextField = dt_state1.Columns[0].ToString();
                ddl_state.DataValueField = dt_state1.Columns[0].ToString();
                ddl_state.DataBind();
            }
            dt_state.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_state.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
        }
    }

    protected void btn_pfNew_Click()
    {

        //-----------------------------------------------
        d.con1.Open();
        MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(SR_NO, 2, 4) AS UNSIGNED))+1 FROM  pay_company_pf_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con1);
        drmax = cmdmax.ExecuteReader();
        if (!drmax.HasRows)
        {
        }
        else if (drmax.Read())
        {
            string str = drmax.GetValue(0).ToString();
            if (str == "")
            {
                txt_srno_pf.Text = "P0001";
            }
            else
            {
                int max_srno = int.Parse(drmax.GetValue(0).ToString());
                if (max_srno < 10)
                {
                    txt_srno_pf.Text = "P000" + max_srno;
                }
                else if (max_srno > 9 && max_srno < 100)
                {
                    txt_srno_pf.Text = "P00" + max_srno;
                }
                else if (max_srno > 99 && max_srno < 1000)
                {
                    txt_srno_pf.Text = "P0" + max_srno;
                }

                else
                {
                }
            }
        }
        drmax.Close();
        d.con1.Close();
    }
    //protected void btn_LWFNew_Click()
    //{

    //    //-----------------------------------------------
    //    d.con1.Open();
    //    MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(SR_NO, 2, 4) AS UNSIGNED))+1 FROM  pay_master_lwf ", d.con1);
    //    drmax = cmdmax.ExecuteReader();
    //    if (!drmax.HasRows)
    //    {
    //    }
    //    else if (drmax.Read())
    //    {
    //        string str = drmax.GetValue(0).ToString();
    //        if (str == "")
    //        {
    //            txt_lwfsr_no.Text = "L0001";
    //        }
    //        else
    //        {
    //            int max_srno = int.Parse(drmax.GetValue(0).ToString());
    //            if (max_srno < 10)
    //            {
    //                txt_lwfsr_no.Text = "L000" + max_srno;
    //            }
    //            else if (max_srno > 9 && max_srno < 100)
    //            {
    //                txt_lwfsr_no.Text = "L00" + max_srno;
    //            }
    //            else if (max_srno > 99 && max_srno < 1000)
    //            {
    //                txt_lwfsr_no.Text = "L0" + max_srno;
    //            }

    //            else
    //            {
    //            }
    //        }
    //    }
    //    drmax.Close();
    //    d.con1.Close();
    //}
    protected void btn_pf_save_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
        string count_aa = "0";
         d.con.Open();
        
         MySqlCommand cmd = new MySqlCommand("select type ,count(type) from pay_company_pf_details where type='" + Select_employee_type.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        
                   if (dr.GetValue(0).ToString() == "Employer")
                     { 
                
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete All Related Exit Record !!');", true);
                   
                      }
                     else if (dr.GetValue(0).ToString() == "Employee")
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete All Related Exit Record !!');", true);

                     }
                     else
                     {
                         res = d.operation("Insert Into pay_company_pf_details (COMP_CODE,SR_NO,type,pf_account,pension_account,admin_charge,edil_chares,Total) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_srno_pf.Text + "','" + Select_employee_type.SelectedValue + "','" + txt_pf_account.Text + "','" + txt_pension_account.Text + "','" + txt_admin_charge.Text + "','" + txt_edli.Text + "','" + txt_total.Text + "')");
                         if (res > 0)
                         {
                             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
                            
                         }
                         else
                         {
                             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record adding failed...')", true);
                             

                    }
                }
                dr.Close();
                d.con.Close();
        
        fill_pf_gv();
        text_pf_clear();
    }
    protected void btn_pf_update_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        int res = d.operation("update  pay_company_pf_details set type='" + Select_employee_type.SelectedValue + "',pf_account='" + txt_pf_account.Text + "',pension_account='" + txt_pension_account.Text + "',admin_charge='" + txt_admin_charge.Text + "',edil_chares='" + txt_edli.Text + "',Total='" + txt_total.Text + "' where type='" + Select_employee_type.SelectedValue + "'");
        if (res > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update successfully!!')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update failed...')", true);
        }
        fill_pf_gv();
        pf_save.Visible = true;
        pf_update.Visible = false;
        pf_delete.Visible = false;
        text_pf_clear();


    }

    protected void btn_lwfsave_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";

        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
        string abv = hf_lwf.Value;
        res = d.operation("Insert into  pay_master_lwf (state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,per_month,catagory) VALUES ('" + Select_lwf_state.SelectedItem.Text + "','" + txt_lwf_act.Text + "','" + txt_emp_category.Text + "','" + ddl_contract_laobour.SelectedItem.Text + "','" + txt_period.Text + "','" + txt_last_day.Text + "','" + txt_e_contribution.Text + "','" + txt_c_contribution.Text + "','" + txt_monthly_charges.Text + "','" + abv + "')");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record adding failed...')", true);
        }
        fill_lwf_gv();
        txt_lwfclear();
    }
    protected void btn_lwfedit_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string abv = hf_lwf.Value;
        int res = d.operation("update pay_master_lwf set state_name='" + Select_lwf_state.SelectedItem.Text + "',app_LWF_act='" + txt_lwf_act.Text + "',category_employees='" + txt_emp_category.Text + "',contract_labours='" + ddl_contract_laobour.SelectedItem.Text + "',period='" + txt_period.Text + "',last_day_submission='" + txt_last_day.Text + "',employee_contribution='" + txt_e_contribution.Text + "',employer_contribution='" + txt_c_contribution.Text + "',per_month='" + txt_monthly_charges.Text + "',catagory='" + abv + "' where id =" + ViewState["lwf_id"].ToString());
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Update successfully!!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Update failed...')", true);
        }
        fill_lwf_gv();
        lwf_update.Visible = false;
        lwf_delete.Visible = false;
        lwf_save.Visible = true;
        txt_lwfclear();

    }
    protected void text_pf_clear()
    {
        Select_employee_type.SelectedValue = "Select";
        txt_srno_pf.Text =
            // Select_employee_type.se;
        txt_pf_account.Text = "0";
        txt_pension_account.Text = "0";
        txt_admin_charge.Text = "0";
        txt_edli.Text = "0";
        txt_total.Text = "0";
    }
    protected void btn_deletelwf_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = d.operation("Delete From pay_master_lwf where id =" + ViewState["lwf_id"].ToString());
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Delete successfully!!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Delete failed...')", true);
        }
        fill_lwf_gv();
        lwf_update.Visible = false;
        lwf_delete.Visible = false;
        lwf_save.Visible = true;
        txt_lwfclear();
    }
    protected void btn_deletepf_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
       
        res = d.operation("Delete From pay_company_pf_details where type='" + Select_employee_type.SelectedValue + "'");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Delete successfully!!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Record Delete failed...')", true);
        }
        fill_pf_gv();
        pf_save.Visible = true;
        pf_update.Visible = false;
        pf_delete.Visible = false;
        text_pf_clear();
    }

    protected void fill_pf_gv()
    {

        d.con.Open();
        MySqlCommand cmd = new MySqlCommand(" select SR_NO,id,type,pf_account,pension_account,admin_charge,edil_chares,Total  from pay_company_pf_details ", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        dr.Fill(ds);
        gv_pfdetails.DataSource = ds.Tables[0];
        gv_pfdetails.DataBind();
        d.con.Close();
    }

    protected void fill_lwf_gv()
    {

        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select id, state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,catagory  from pay_master_lwf ", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        dr.Fill(ds);
        gv_lwfdetails.DataSource = ds.Tables[0];
        gv_lwfdetails.DataBind();
        d.con.Close();
    }

    protected void gv_pfdetails_selected_Index(object sender, EventArgs e)
    {
        int res = 0;
        //System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_pfdetails.SelectedRow.FindControl("id");

        //string id = lblno.Text;
        string id1=gv_pfdetails.SelectedRow.Cells[1].Text;
        MySqlCommand cmd1 = new MySqlCommand("SELECT SR_NO,type,pf_account,pension_account,admin_charge,edil_chares,Total,id FROM pay_company_pf_details WHERE   id='" + id1 + "'", d.con);
        d.con.Open();
        try{
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); 
            MySqlDataReader dr_hd = cmd1.ExecuteReader();
            if (dr_hd.Read())
            {
                txt_srno_pf.Text = dr_hd.GetValue(0).ToString();
                Select_employee_type.SelectedValue = dr_hd.GetValue(1).ToString();
                txt_pf_account.Text = dr_hd.GetValue(2).ToString();
                txt_pension_account.Text = dr_hd.GetValue(3).ToString();
                txt_admin_charge.Text = dr_hd.GetValue(4).ToString();
                txt_edli.Text = dr_hd.GetValue(5).ToString();
                txt_total.Text = dr_hd.GetValue(6).ToString();
            }
        }
        catch (Exception ex) { }
        finally
        {
            d.con.Close();
            //    fill_pf_gv();
            pf_save.Visible = false;
            pf_update.Visible = true;
            pf_delete.Visible = true;

        }

    }
    protected void gv_lwfdetails_selected_Index_Change(object sender, EventArgs e)
    {
        // int res = 0;
        //System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_lwfdetails.SelectedRow.FindControl("lbl_srnumber");
        hidtab.Value = "3";
        MySqlCommand cmd1 = new MySqlCommand("SELECT id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution, per_month,catagory FROM pay_master_lwf WHERE id =" + gv_lwfdetails.SelectedRow.Cells[0].Text, d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_hd = cmd1.ExecuteReader();
            if (dr_hd.Read())
            {
                ViewState["lwf_id"] = dr_hd.GetValue(0).ToString();
                Select_lwf_state.SelectedItem.Text = dr_hd.GetValue(1).ToString();
                txt_lwf_act.Text = dr_hd.GetValue(2).ToString();
                txt_emp_category.Text = dr_hd.GetValue(3).ToString();
                ddl_contract_laobour.SelectedValue = dr_hd.GetValue(4).ToString();
                txt_period.Text = dr_hd.GetValue(5).ToString();
                txt_last_day.Text = dr_hd.GetValue(6).ToString();
                txt_e_contribution.Text = dr_hd.GetValue(7).ToString();
                txt_c_contribution.Text = dr_hd.GetValue(8).ToString();
                txt_monthly_charges.Text = dr_hd.GetValue(9).ToString();
               // lbx_month.SelectedValue = dr_hd.GetValue(10).ToString();
                string utility_cost = dr_hd.GetValue(10).ToString();
                update_listbox(lbx_month, utility_cost);
            }
        }
        catch (Exception ex) { }
        finally
        {
            d.con.Close();
            //  fill_lwf_gv();
            lwf_update.Visible = true;
            lwf_delete.Visible = true;
            lwf_save.Visible = false;
        }
    }
    protected void update_listbox(System.Web.UI.WebControls.ListBox lbx_month, string listarray1)
    {
        
       //listarray1 = listarray1.Replace(",", "");
        //string count = listarray1;
        lbx_month.ClearSelection();
        int num = 0;
        string month_name = "";
        string[] ary = listarray1.Split(',');
        foreach (object a in ary)
        {
            //num = num + 1;
             month_name = a.ToString();
             lbx_month.SelectedValue = month_name.ToString();
        }
       // lbx_month.ClearSelection();

        //for (int i = 0; i <= num; i++)
        //{
        //    lbx_month.SelectedValue = month_name.ToString();
        //    //lbx_month.Items[listarray1.Substring(i, 3)].Selected = true;
           
        //}
    }
    protected void txt_lwfclear()
    {
       // txt_lwfsr_no.Text = "";
        Select_employee_type.SelectedValue = "Select";
        Select_lwf_state.SelectedItem.Text = "";
        txt_lwf_act.Text = "";
        txt_emp_category.Text = "";
        // ddl_contract_laobour.SelectedValue = "";
        txt_period.Text = "";
        txt_last_day.Text = "";
        txt_e_contribution.Text = "0";
        txt_c_contribution.Text = "0";
        txt_monthly_charges.Text = "0";

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        int result = 0;
       
            try
            {
                result = d.operation("INSERT INTO pay_pt_slab_master(STATE_NAME, FROM_AMOUNT, TO_AMOUNT, SLAB_AMOUNT,comp_code, FROM_month, TO_month) VALUES('" + ddl_state.SelectedItem.Text + "'," + double.Parse(txt_fromamount.Text) + "," + double.Parse(txt_toamount.Text) + "," + double.Parse(txt_slabamount.Text) + ",'" + Session["comp_code"].ToString() + "'," + ddl_from_month.SelectedValue + "," + ddl_to_month.SelectedValue + ")");//insert command


                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record adding failed...')", true);
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                ddl_from_month.SelectedValue = "0";
                ddl_to_month.SelectedValue = "0";
                ddl_state.SelectedValue = "Select";
                btn_clear_data(null, null);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
           
        
    }
    protected void PTSlabGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        MySqlCommand cmd2 = new MySqlCommand("SELECT id,STATE_NAME,FROM_AMOUNT,TO_AMOUNT,SLAB_AMOUNT,FROM_month,TO_month from pay_pt_slab_master WHERE  ID=" + PTSlabGridView.SelectedRow.Cells[0].Text, d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                ViewState["ID"] = dr2[0].ToString();
                state();
                ddl_state.SelectedValue = dr2[1].ToString();
                txt_fromamount.Text = dr2[2].ToString();
                txt_toamount.Text = dr2[3].ToString();
                txt_slabamount.Text = dr2[4].ToString();
                ddl_from_month.SelectedValue = dr2[5].ToString();
                ddl_to_month.SelectedValue = dr2[6].ToString();
            }
            dr2.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            btn_add.Visible = false;
        }

        //MySqlCommand cmd1 = new MySqlCommand("SELECT SR_NO,type,pf_account,pension_account,admin_charge,edil_chares,Total FROM pay_company_pf_details WHERE  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataReader dr_hd = cmd1.ExecuteReader();
        //    if (dr_hd.HasRows)
        //    {
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        dt.Load(dr_hd);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ViewState["headtable"] = dt;
        //        }
        //        gv_pfdetails.DataSource = dt;
        //        gv_pfdetails.DataBind();
        //    }
        //}
        //catch (Exception ex) { }
        //finally { d.con.Close(); }


        //MySqlCommand cmd = new MySqlCommand("SELECT SR_NO,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution FROM pay_master_lwf WHERE  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataReader dr_hd = cmd.ExecuteReader();
        //    if (dr_hd.HasRows)
        //    {
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        dt.Load(dr_hd);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ViewState["CurrentTable"] = dt;
        //        }
        //        gv_lwfdetails.DataSource = dt;
        //        gv_lwfdetails.DataBind();
        //    }
        //}
        //catch (Exception ex) { }
        //finally { d.con.Close(); }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {

        try
        {
            int res = d.operation("update pay_pt_slab_master set STATE_NAME='" + ddl_state.SelectedValue + "', FROM_AMOUNT='" + txt_fromamount.Text + "', TO_AMOUNT='" + txt_toamount.Text + "', SLAB_AMOUNT='" + txt_slabamount.Text + "', FROM_month=" + ddl_from_month.SelectedValue + ", TO_month=" + ddl_to_month.SelectedValue + " where id=" + ViewState["ID"].ToString());
            //foreach (GridViewRow row in gv_pfdetails.Rows)
            //{
            //    int sr_number = int.Parse(((System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber")).Text);
            //    string lbl_type = row.Cells[2].Text;
            //    string pf_account = row.Cells[3].Text;
            //    string pension_account = row.Cells[4].Text;
            //    string admin_charges = row.Cells[5].Text;
            //    string edil_charges = row.Cells[6].Text;
            //    string total = row.Cells[7].Text;


            //    d.operation("Insert Into pay_company_pf_details (COMP_CODE,SR_NO,type,pf_account,pension_account,admin_charge,edil_chares,Total) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + lbl_type + "','" + pf_account + "','" + pension_account + "'," + admin_charges + "," + edil_charges + ",'" + total + "')");
            //}

            //foreach (GridViewRow row in gv_lwfdetails.Rows)
            //{
            //    int sr_number = int.Parse(((System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber")).Text);
            //    string state = row.Cells[2].Text;
            //    string lwf_act = row.Cells[3].Text;
            //    string category = row.Cells[4].Text;
            //    string contract_laboru = row.Cells[5].Text;
            //    // string contract_laboru = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber")).Text;
            //    string period = row.Cells[6].Text;
            //    string last_day = row.Cells[7].Text;
            //    string employee_cont = row.Cells[8].Text;
            //    string employer_cont = row.Cells[9].Text;

            //    d.operation("Insert Into pay_master_lwf (COMP_CODE,SR_NO,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + state + "','" + lwf_act + "','" + category + "','" + contract_laboru + "'," + period + ",'" + last_day + "','" + employee_cont + "','" + employer_cont + "')");
            //}




            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('PT Slab Updated Successfully');", true);
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ddl_from_month.SelectedValue = "0";
            ddl_to_month.SelectedValue = "0";
            ddl_state.SelectedValue = "Select";
            btn_clear_data(null, null);
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            int result = d.operation("DELETE FROM pay_pt_slab_master WHERE id=" + ViewState["ID"].ToString());
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deleted successfully!!')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deletion failed...')", true);

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ddl_from_month.SelectedValue = "0";
            ddl_to_month.SelectedValue = "0";
            ddl_state.SelectedValue = "Select";
            btn_clear_data(null, null);
        }
    }
    protected void btnexporttoexcelptslab_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        ws.Cells[2, 5] = "PT SLAB LIST";
        ws.Cells[5, 1] = "STATE_NAME";
        ws.Cells[5, 2] = "FROM AMOUNT";
        ws.Cells[5, 3] = "TO AMOUNT";
        ws.Cells[5, 4] = "SLAB AMOUNT";

        try
        {
            d.con1.Open();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT STATE_NAME,FROM_AMOUNT,TO_AMOUNT,SLAB_AMOUNT  FROM pay_pt_slab_master ORDER BY STATE_NAME,SR_NO", d.con1);

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
            d.con1.Close();
        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void PTSlabGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.PTSlabGridView, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
    }

    protected void gv_pfdetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_pfdetails, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
    }
    protected void gv_lwfdetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_lwfdetails, "Select$" + e.Row.RowIndex);

        }
       // e.Row.Cells[0].Visible = false;
    }
    protected void btn_clear_data(object sendar, EventArgs e)
    {
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd1 = new MySqlCommand("SELECT Id,comp_code,STATE_NAME,FROM_AMOUNT,TO_AMOUNT,SLAB_AMOUNT, case when from_month = 1 then 'Januray' when from_month = 2 then 'February' when from_month = 3 then 'March' when from_month = 4 then 'April' when from_month = 5 then 'May' when from_month = 6 then 'June' when from_month = 7 then 'July' when from_month = 8 then 'August' when from_month = 9 then 'September' when from_month = 10 then 'October' when from_month = 11 then 'November' when from_month = 12 then 'December' else '' END as from_month, case when to_month = 1 then 'Januray' when to_month = 2 then 'February' when to_month = 3 then 'March' when to_month = 4 then 'April' when to_month = 5 then 'May' when to_month = 6 then 'June' when to_month = 7 then 'July' when to_month = 8 then 'August' when to_month = 9 then 'September' when to_month = 10 then 'October' when to_month = 11 then 'November' when to_month = 12 then 'December' else '' END as to_month FROM pay_pt_slab_master ", d.con1);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            PTSlabGridView.DataSource = ds1.Tables[0];
            PTSlabGridView.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            txt_fromamount.Text = "";
            txt_toamount.Text = "";
            txt_slabamount.Text = "";
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            btnexporttoexcelptslab.Visible = false;
            btn_add.Visible = true;
            ViewState["ID"] = "";
            pf_update.Visible = false;
            pf_delete.Visible = false;
            lwf_update.Visible = false;
            lwf_delete.Visible = false;
        }
    }
    protected void PTSlabGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            PTSlabGridView.UseAccessibleHeader = false;
            PTSlabGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_pfdetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_pfdetails.UseAccessibleHeader = false;
            gv_pfdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_lwfdetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_lwfdetails.UseAccessibleHeader = false;
            gv_lwfdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    private string get_month(int month)
    {
        if (month == 1) { return "January"; }
        if (month == 2) { return "February"; }
        if (month == 3) { return "March"; }
        if (month == 4) { return "April"; }
        if (month == 5) { return "May"; }
        if (month == 6) { return "June"; }
        if (month == 7) { return "July"; }
        if (month == 8) { return "August"; }
        if (month == 9){ return "September"; }
        if (month == 10) { return "October"; }
        if (month == 11) { return "November"; }
        if (month == 12) { return "December"; }
        return "";
    }

    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        gv_pfdetails.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("type");
        dt.Columns.Add("pf_account");
        dt.Columns.Add("pension_account");
        dt.Columns.Add("admin_charge");
        dt.Columns.Add("edil_chares");
        dt.Columns.Add("Total");
        int rownum = 0;
        for (rownum = 0; rownum < gv_pfdetails.Rows.Count; rownum++)
        {
            if (gv_pfdetails.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["type"] = gv_pfdetails.Rows[rownum].Cells[2].Text;
                dr["pf_account"] = gv_pfdetails.Rows[rownum].Cells[3].Text;
                dr["pension_account"] = gv_pfdetails.Rows[rownum].Cells[4].Text;
                dr["admin_charge"] = gv_pfdetails.Rows[rownum].Cells[5].Text;
                dr["edil_chares"] = gv_pfdetails.Rows[rownum].Cells[6].Text;
                dr["Total"] = gv_pfdetails.Rows[rownum].Cells[7].Text;
                dt.Rows.Add(dr);
            }
        }
        dr = dt.NewRow();
        dr["type"] = Select_employee_type.SelectedValue;
        dr["pf_account"] = txt_pf_account.Text;
        dr["pension_account"] = txt_pension_account.Text;
        dr["admin_charge"] = txt_admin_charge.Text;
        dr["edil_chares"] = txt_edli.Text;
        dr["Total"] = txt_total.Text;
        dt.Rows.Add(dr);
        gv_pfdetails.DataSource = dt;
        gv_pfdetails.DataBind();
        ViewState["headtable"] = dt;
        txt_pf_account.Text = "0";
        txt_pf_account.Text = "0";
        txt_admin_charge.Text = "0";
        txt_edli.Text = "0";
        txt_total.Text = "0";
    }

    protected void lnkbtn_add_lwfdetails_Click(object sender, EventArgs e)
    {
        gv_lwfdetails.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("state_name");
        dt.Columns.Add("app_LWF_act");
        dt.Columns.Add("category_employees");
        dt.Columns.Add("contract_labours");
        dt.Columns.Add("period");
        dt.Columns.Add("last_day_submission");
        dt.Columns.Add("employee_contribution");
        dt.Columns.Add("employer_contribution");
        dt.Columns.Add("monthly_amount");
        int rownum = 0;
        for (rownum = 0; rownum < gv_lwfdetails.Rows.Count; rownum++)
        {
            if (gv_pfdetails.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["state_name"] = gv_lwfdetails.Rows[rownum].Cells[2].Text;
                dr["app_LWF_act"] = gv_lwfdetails.Rows[rownum].Cells[3].Text;
                dr["category_employees"] = gv_lwfdetails.Rows[rownum].Cells[4].Text;
                dr["contract_labours"] = gv_lwfdetails.Rows[rownum].Cells[5].Text;
                dr["period"] = gv_lwfdetails.Rows[rownum].Cells[6].Text;
                dr["last_day_submission"] = gv_lwfdetails.Rows[rownum].Cells[7].Text;
                dr["employee_contribution"] = gv_lwfdetails.Rows[rownum].Cells[8].Text;
                dr["employer_contribution"] = gv_lwfdetails.Rows[rownum].Cells[9].Text;
                dr["monthly_amount"] = gv_lwfdetails.Rows[rownum].Cells[10].Text;
                dt.Rows.Add(dr);
            }
        }
        dr = dt.NewRow();
        dr["state_name"] = Select_lwf_state.SelectedItem.Text;
        dr["app_LWF_act"] = txt_lwf_act.Text;
        dr["category_employees"] = txt_emp_category.Text;
        dr["contract_labours"] = ddl_contract_laobour.SelectedItem.Text;
        dr["period"] = txt_period.Text;
        dr["last_day_submission"] = txt_last_day.Text;
        dr["employee_contribution"] = txt_e_contribution.Text;
        dr["employer_contribution"] = txt_c_contribution.Text;
        dr["monthly_amount"] = txt_monthly_charges.Text;
        dt.Rows.Add(dr);
        gv_lwfdetails.DataSource = dt;
        gv_lwfdetails.DataBind();
        ViewState["CurrentTable"] = dt;
        txt_emp_category.Text = "";
        txt_period.Text = "";
        txt_last_day.Text = "";
        txt_e_contribution.Text = "0";
        txt_c_contribution.Text = "0";
        txt_monthly_charges.Text = "0";
    }

    protected void lnkbtn_removeitemlwf_Click(object sender, EventArgs e)
    {
        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_lwfdetails.DataSource = dt;
            gv_lwfdetails.DataBind();
        }
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_pfdetails.DataSource = dt;
            gv_pfdetails.DataBind();
        }
    }
    protected void ddl_esic_client_SelectedIndexChanged()
    {
        //State
        ddl_esic_state.Items.Clear();
        System.Data.DataTable dt_item_state = new System.Data.DataTable();
        string where = "comp_code='" + Session["comp_code"] + "'";
        //if (ddl_esic_client.SelectedValue == "ALL")
        //{
        //    where = "comp_code='" + Session["comp_code"] + "'";
        //}
        MySqlDataAdapter cmd_item_state = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where " + where + " order by 1", d.con);
        d.con.Open();
        try
        {
            cmd_item_state.Fill(dt_item_state);
            if (dt_item_state.Rows.Count > 0)
            {
                ddl_esic_state.DataSource = dt_item_state;
                ddl_esic_state.DataTextField = dt_item_state.Columns[0].ToString();
                ddl_esic_state.DataValueField = dt_item_state.Columns[0].ToString();
                ddl_esic_state.DataBind();
            }
            dt_item_state.Dispose();
            d.con.Close();
           // ddl_esic_state.Items.Insert(0, "ALL");
           //ddl_esic_state.Items.Insert(0, new ListItem("Select"));
            ddl_esic_state.Items.Insert(0, new ListItem("ALL"));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //ddl_state_SelectedIndexChanged(null, null);
    }
    //protected void ddl_pf_client_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_pf_client.SelectedValue != "ALL")
    //    {
    //        //branch

    //        ddl_pf_unit.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pf_client.SelectedValue + "' AND branch_status = 0  ORDER BY UNIT_CODE", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_pf_unit.DataSource = dt_item;
    //                ddl_pf_unit.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_pf_unit.DataValueField = dt_item.Columns[1].ToString();
    //                ddl_pf_unit.DataBind();
    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //            ddl_pf_unit.Items.Insert(0, "ALL");

    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }
    //    }
    //    else
    //    {
    //        // ddl_unitcode.Items.Clear();
    //        ddl_pf_unit.Items.Insert(0, "ALL");
    //    }
    //    //State
    //    ddl_pf_state.Items.Clear();
    //    System.Data.DataTable dt_item_state = new System.Data.DataTable();
    //    string where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pf_client.SelectedValue + "'";
    //    if (ddl_pf_client.SelectedValue == "ALL")
    //    {
    //        where = "comp_code='" + Session["comp_code"] + "'";
    //    }
    //    MySqlDataAdapter cmd_item_state = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where " + where + " order by 1", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        cmd_item_state.Fill(dt_item_state);
    //        if (dt_item_state.Rows.Count > 0)
    //        {
    //            ddl_pf_state.DataSource = dt_item_state;
    //            ddl_pf_state.DataTextField = dt_item_state.Columns[0].ToString();
    //            ddl_pf_state.DataValueField = dt_item_state.Columns[0].ToString();
    //            ddl_pf_state.DataBind();
    //        }
    //        dt_item_state.Dispose();
    //        d.con.Close();
    //        ddl_pf_state.Items.Insert(0, "ALL");
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //    //ddl_state_SelectedIndexChanged(null, null);
    //}
    protected void ddl_pt_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pt_client.SelectedValue != "ALL")
        {
            //branch

            ddl_pt_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
       //comment 30/09     MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pt_client.SelectedValue + "' AND branch_status = 0  ORDER BY UNIT_CODE", d.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pt_client.SelectedValue + "' AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txt_pt_date.Text + "', '%d/%m/%Y'))  ORDER BY UNIT_CODE", d.con);
			d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_pt_unit.DataSource = dt_item;
                    ddl_pt_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pt_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_pt_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_pt_unit.Items.Insert(0, "ALL");

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            // ddl_pt_unit.Items.Clear();
            ddl_pt_unit.Items.Insert(0, "ALL");
        }
        //State
        ddl_pt_state.Items.Clear();
        System.Data.DataTable dt_item_state = new System.Data.DataTable();
        string where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pt_client.SelectedValue + "'";
        if (ddl_pt_client.SelectedValue == "ALL")
        {
            where = "comp_code='" + Session["comp_code"] + "'";
        }
      //comment 30/09  MySqlDataAdapter cmd_item_state = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where " + where + " order by 1", d.con);
       MySqlDataAdapter cmd_item_state = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where " + where + " AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txt_pt_date.Text + "', '%d/%m/%Y')) order by 1", d.con);
	    d.con.Open();
        try
        {
            cmd_item_state.Fill(dt_item_state);
            if (dt_item_state.Rows.Count > 0)
            {
                ddl_pt_state.DataSource = dt_item_state;
                ddl_pt_state.DataTextField = dt_item_state.Columns[0].ToString();
                ddl_pt_state.DataValueField = dt_item_state.Columns[0].ToString();
                ddl_pt_state.DataBind();
            }
            dt_item_state.Dispose();
            d.con.Close();
            ddl_pt_state.Items.Insert(0, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //ddl_state_SelectedIndexChanged(null, null);
    }

    //protected void ddl_esic_state_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_esic_state.SelectedValue != "ALL")
    //    {
    //        ddl_esic_unit.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        string where = "comp_code='" + Session["comp_code"] + "' and state_name='" + ddl_esic_state.SelectedValue + "'";
    //        if (ddl_esic_state.SelectedValue == "ALL")
    //        {
    //            where = "comp_code='" + Session["comp_code"] + "'";
    //        }
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where " + where + "  AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_esic_unit.DataSource = dt_item;
    //                ddl_esic_unit.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_esic_unit.DataValueField = dt_item.Columns[1].ToString();
    //                ddl_esic_unit.DataBind();
    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //            ddl_esic_unit.Items.Insert(0, "ALL");

    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }
    //    }
    //    //else
    //    //{
    //    //    ddl_esic_client_SelectedIndexChanged(null, null);
    //    //}
    //}
    //protected void ddl_pf_state_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_pf_state.SelectedValue != "ALL")
    //    {
    //        ddl_pf_unit.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        string where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and state_name='" + ddl_pf_state.SelectedValue + "'";
    //        if (ddl_pf_state.SelectedValue == "ALL")
    //        {
    //            where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pf_client.SelectedValue + "'";
    //        }
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where " + where + "  AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_pf_unit.DataSource = dt_item;
    //                ddl_pf_unit.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_pf_unit.DataValueField = dt_item.Columns[1].ToString();
    //                ddl_pf_unit.DataBind();
    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //            ddl_pf_unit.Items.Insert(0, "ALL");

    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }
    //    }
    //    else
    //    {
    //        ddl_pf_client_SelectedIndexChanged(null, null);
    //    }
    //}
    protected void ddl_pt_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pt_state.SelectedValue != "ALL")
        {
            ddl_pt_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            string where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pt_client.SelectedValue + "' and state_name='" + ddl_pt_state.SelectedValue + "'";
            if (ddl_pt_state.SelectedValue == "ALL")
            {
                where = "comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_pt_client.SelectedValue + "'";
            }
      //comment 30/09      MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where " + where + "  AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
           MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where " + where + "  AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txt_pt_date.Text + "', '%d/%m/%Y')) ORDER BY UNIT_CODE", d.con);
		    d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_pt_unit.DataSource = dt_item;
                    ddl_pt_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_pt_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_pt_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_pt_unit.Items.Insert(0, "ALL");

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_pt_client_SelectedIndexChanged(null, null);
        }
    }
    private void generate_slab_report(int i, string where, string month_year_name)
    {

        try
        {
            string sql = null;


            //ESIC statement
            if (i == 1)
            {
                // sql = "SELECT client, state_name, unit_name, grade, ESIC_NO, emp_name, total_gross, Total_Days_Present,month_days, gross,sal_esic, (gross * bill_esic_percent)/100 as 'bill_esic',(emp_basic_vda * bill_esic_percent) / 100 AS 'bill_esic_basic',	(emp_basic_vda * sal_esic_percent) / 100 AS 'sal_esic_basic' FROM (SELECT state_name, unit_city, emp_name, grade, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_esic_percent) AS 'sal_esic', lwf_salary, sal_uniform_rate, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 END ELSE PT END AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ESIC_NO, unit_name,month_days,total_gross,bill_esic_percent,sal_esic_percent FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_name, unit_city, pay_employee_master.emp_name, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.Bank_holder_name, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, pay_employee_master.ESIC_NUMBER AS 'ESIC_NO', (SELECT Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', CONCAT((SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code),'-',pay_salary_unit_rate.designation) AS 'client', (((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable_salary = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable_salary = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education_salary', ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END AS 'cca_salary', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_billing_master_history.sal_esic AS 'sal_esic_percent', pay_billing_master_history.sal_pf AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'sal_ot', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/06/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/06/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/06/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT,pay_salary_unit_rate.month_days,pay_salary_unit_rate.gross as 'total_gross',pay_billing_master_history.bill_esic_percent AS 'bill_esic_percent' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours  AND pay_billing_master_history.type = 'salary' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code WHERE (pay_employee_master.ESIC_NUMBER !='' And pay_employee_master.ESIC_NUMBER is not null) and " + where + ") as table_salary) AS Final_salary";

                //sql = "SELECT CONCAT(pay_pro_master.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, grade, ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, ROUND(total_gross, 2) AS 'total_gross', ROUND(Total_Days_Present, 2) AS 'Total_Days_Present', pay_pro_master.month_days, ROUND((pay_pro_master.gross), 2) AS 'gross', ROUND(((pay_pro_master.gross * 1.75)/100), 2) AS 'sal_esic', ROUND(((pay_pro_master.gross * 4.75)/100), 2) AS 'bill_esic', ROUND(((pay_pro_master.emp_basic_vda * bill_esic_percent) / 100), 2) AS 'bill_esic_basic', ROUND(((pay_pro_master.emp_basic_vda * sal_esic_percent) / 100), 2) AS 'sal_esic_basic' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND invoice_no IS NOT NULL WHERE  " + where;
               //rahul
                if (check1(i) != "")
                {
                    sql = "SELECT DISTINCT CONCAT(pay_billing_unit_rate_history.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, case grade when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else grade end as grade, ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, ROUND(total_gross, 2) AS 'total_gross', ROUND(Total_Days_Present, 2) AS 'Total_Days_Present', pay_pro_master.month_days, ROUND((pay_pro_master.gross), 2) AS 'gross', if(`pay_pro_master`.`gross`>=21000,0,ROUND(((pay_pro_master.gross * sal_esic_percent) / 100), 2)) AS 'sal_esic', if(`pay_pro_master`.`gross`>=21000,0,ROUND(((pay_pro_master.gross * bill_esic_percent) / 100), 2)) AS 'bill_esic', ROUND(((pay_pro_master.emp_basic_vda * bill_esic_percent) / 100), 2) AS 'bill_esic_basic', ROUND(((pay_pro_master.emp_basic_vda * sal_esic_percent) / 100), 2) AS 'sal_esic_basic' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL)  inner join pay_employee_master on  pay_pro_master.emp_code = pay_employee_master.emp_code  WHERE     `pay_pro_master`.`esic_status` IS NULL and " + where;
                }
                else
                {
                    sql = "SELECT DISTINCT CONCAT(pay_billing_unit_rate_history.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, case grade when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else grade end as grade, ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, ROUND(total_gross, 2) AS 'total_gross', ROUND(Total_Days_Present, 2) AS 'Total_Days_Present', pay_pro_master.month_days, ROUND((pay_pro_master.gross), 2) AS 'gross', if(`pay_pro_master`.`gross`>=21000,0,ROUND(((pay_pro_master.gross * sal_esic_percent) / 100), 2)) AS 'sal_esic', if(`pay_pro_master`.`gross`>=21000,0,ROUND(((pay_pro_master.gross * bill_esic_percent) / 100), 2)) AS 'bill_esic', ROUND(((pay_pro_master.emp_basic_vda * bill_esic_percent) / 100), 2) AS 'bill_esic_basic', ROUND(((pay_pro_master.emp_basic_vda * sal_esic_percent) / 100), 2) AS 'sal_esic_basic' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL)  inner join pay_employee_master on  pay_pro_master.emp_code = pay_employee_master.emp_code WHERE  " + where;
                }
            }
            //ESIC UPLOAD
            if (i == 5)
            {
                // sql = "SELECT client, state_name, unit_name, grade, ESIC_NO, emp_name, total_gross, Total_Days_Present,month_days, gross,sal_esic, (gross * bill_esic_percent)/100 as 'bill_esic',(emp_basic_vda * bill_esic_percent) / 100 AS 'bill_esic_basic',	(emp_basic_vda * sal_esic_percent) / 100 AS 'sal_esic_basic' FROM (SELECT state_name, unit_city, emp_name, grade, emp_basic_vda, hra_amount_salary, sal_bonus_gross, leave_sal_gross, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow, gratuity_gross, (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) AS 'gross', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'sal_pf', (((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) / 100) * sal_esic_percent) AS 'sal_esic', lwf_salary, sal_uniform_rate, CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary + allowances_salary + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 END ELSE PT END AS 'PT_AMOUNT', sal_ot, sal_bonus_after_gross, leave_sal_after_gross, gratuity_after_gross, fine, EMP_ADVANCE_PAYMENT, Total_Days_Present, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, Account_no, STATUS, NI, date, client, ESIC_NO, unit_name,month_days,total_gross,bill_esic_percent,sal_esic_percent FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_name, unit_city, pay_employee_master.emp_name, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.Bank_holder_name, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, pay_employee_master.ESIC_NUMBER AS 'ESIC_NO', (SELECT Field2 FROM pay_zone_master WHERE Field1 = 'AXIS BANK' AND type = 'bank_details' AND comp_code = pay_employee_master.comp_code) AS 'Account_no', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', CASE WHEN INSTR(pay_employee_master.PF_IFSC_CODE, 'UTI') > 0 THEN 'I' ELSE 'N' END AS 'NI', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', CONCAT((SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code),'-',pay_salary_unit_rate.designation) AS 'client', (((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra_amount_salary', CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable_salary = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable_salary = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education_salary', ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END AS 'cca_salary', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_billing_master_history.sal_esic AS 'sal_esic_percent', pay_billing_master_history.sal_pf AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'sal_ot', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/06/2018', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/06/2018'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/06/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT,pay_salary_unit_rate.month_days,pay_salary_unit_rate.gross as 'total_gross',pay_billing_master_history.bill_esic_percent AS 'bill_esic_percent' FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code AND pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours  AND pay_billing_master_history.type = 'salary' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code WHERE (pay_employee_master.ESIC_NUMBER !='' And pay_employee_master.ESIC_NUMBER is not null) and " + where + ") as table_salary) AS Final_salary";
                if (check1(i) != "")
                {
                    sql = "SELECT distinct pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.emp_name , ROUND(pay_pro_master.Total_Days_Present,2) AS 'Total_Days_Present', ROUND((pay_pro_master.gross), 2) AS 'esic_salary', CASE WHEN pay_pro_master.STATUS = 'LEFT' THEN 2 WHEN pay_pro_master.STATUS = 'YES' THEN 0 END AS 'reason_code'    FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) WHERE  `pay_pro_master`.`esic_status` IS NULL and `pay_pro_master`.`gross` <= 21000 and " + where;
                }
                else
                {
                    sql = "SELECT distinct pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.emp_name , ROUND(pay_pro_master.Total_Days_Present,2) AS 'Total_Days_Present', ROUND((pay_pro_master.gross), 2) AS 'esic_salary', CASE WHEN pay_pro_master.STATUS = 'LEFT' THEN 2 WHEN pay_pro_master.STATUS = 'YES' THEN 0 END AS 'reason_code'    FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) WHERE `pay_pro_master`.`gross` <= 21000 and " + where;
                }
            }
            //PF STATEMENT
            else if (i == 2)
            {
               // sql = "SELECT client, state_name, unit_name, PF_NO, ESIC_NO, UAN, emp_name, grade, actual_basic_vda, STATUS, month_days, Total_Days_Present, emp_basic_vda, EPS_WAGES, EPF_CR, ((EPS_WAGES * EPS_PER) / 100) AS 'EPS_CR', (month_days - Total_Days_Present) AS 'ncp_days' FROM (SELECT state_name, unit_city, emp_name, grade, Total_Days_Present, emp_basic_vda, date, client, ESIC_NO, unit_name, month_days, UAN, PF_NO, actual_basic_vda, STATUS, IF(emp_basic_vda > 15000, 15000, emp_basic_vda) AS 'EPS_WAGES', ((emp_basic_vda * pf_percent) / 100) AS 'EPF_CR', EPS_PER FROM (SELECT pay_unit_master.state_name, pay_unit_master.unit_name, unit_city, pay_employee_master.emp_name, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_employee_master.Bank_holder_name, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, pay_employee_master.PF_NUMBER AS 'PF_NO', pay_employee_master.PAN_NUMBER AS 'UAN', pay_employee_master.ESIC_NUMBER AS 'ESIC_NO', CASE WHEN LENGTH(left_date) > 0 THEN 'LEFT' ELSE 'YES' END AS 'STATUS', DATE_FORMAT(NOW(), '%d-%m-%Y') AS 'date', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = pay_unit_master.client_code) AS 'client', (((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', pay_salary_unit_rate.month_days, (pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) AS 'actual_basic_vda', pay_billing_master_history.sal_pf AS 'pf_percent', IFNULL((SELECT pension_account FROM pay_company_pf_details WHERE type = 'Employer'), 0) AS 'EPS_PER'  FROM pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_salary_unit_rate ON pay_unit_master.comp_code = pay_salary_unit_rate.comp_code and pay_attendance_muster.unit_code = pay_salary_unit_rate.unit_code AND pay_attendance_muster.month = pay_salary_unit_rate.month AND pay_attendance_muster.year = pay_salary_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_master_history.billing_client_code = pay_salary_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_salary_unit_rate.unit_code AND pay_billing_master_history.month = pay_salary_unit_rate.month AND pay_billing_master_history.year = pay_salary_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_salary_unit_rate.designation AND pay_billing_master_history.hours = pay_salary_unit_rate.hours AND pay_billing_master_history.type = 'salary' INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code WHERE (pay_employee_master.PF_NUMBER !='' AND pay_employee_master.PF_NUMBER is not null) and  " + where + ") as table_salary) AS Final_salary";
                //sql = "SELECT CONCAT(pay_pro_master.client, '-',  IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.PF_NO, pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.PAN_No AS 'UAN', pay_pro_master.emp_name, pay_pro_master.grade, round(pay_pro_master.actual_basic_vda) as 'actual_basic_vda', pay_pro_master.STATUS, pay_pro_master.month_days, pay_pro_master.Total_Days_Present, round(pay_pro_master.emp_basic_vda) as 'emp_basic_vda',  IF(pay_pro_master.emp_basic_vda > 15000, 15000, round(pay_pro_master.emp_basic_vda)) AS 'EPS_WAGES', IF(pay_pro_master.emp_basic_vda > 15000, ROUND((15000 * 8.33) / 100), ROUND((pay_pro_master.emp_basic_vda * 8.33) / 100)) AS 'EPS_CR', ROUND((pay_pro_master.emp_basic_vda * 12) / 100) AS 'EPF_CR', (pay_pro_master.month_days - pay_pro_master.Total_Days_Present) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND invoice_no IS NOT NULL WHERE  " + where;
               //rahul
                //sql = "SELECT CONCAT(pay_billing_unit_rate_history.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.PF_NO, pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.PAN_No AS 'UAN', pay_pro_master.emp_name, CASE grade WHEN 'OFFICE BOY' THEN CASE pay_employee_master.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade', ROUND(CASE WHEN pay_pro_master.client_code = 'HDFC' THEN pay_pro_master.actual_basic_vda + ((pay_pro_master.sal_bonus_gross) / pay_pro_master.Total_Days_Present * pay_pro_master.month_days) ELSE pay_pro_master.actual_basic_vda + ((pay_pro_master.cca_salary) / pay_pro_master.Total_Days_Present * pay_pro_master.month_days) + ((pay_pro_master.other_allow) / pay_pro_master.Total_Days_Present * pay_pro_master.month_days) END) AS 'actual_basic_vda', pay_pro_master.STATUS, pay_pro_master.month_days, pay_pro_master.Total_Days_Present, ROUND(CASE WHEN pay_pro_master.client_code = 'HDFC' THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross ELSE pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END) AS 'emp_basic_vda', IF((`pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow`) > 15000, 15000, ROUND(CASE WHEN pay_pro_master.client_code = 'HDFC' THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross ELSE pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)) AS 'EPS_WAGES', IF((`pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow`) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN pay_pro_master.client_code = 'HDFC' THEN ((pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross)*8.33)/100 ELSE (pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow) * 8.33 / 100 end)))) AS 'EPS_CR', ROUND(pay_pro_master.sal_pf) AS 'EPF_CR', (pay_pro_master.month_days - pay_pro_master.Total_Days_Present) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND if(pay_billing_unit_rate_history.client_code = 'RCPL',invoice_no IS NULL,invoice_no IS NOT NULL) inner join pay_employee_master on  pay_pro_master.emp_code = pay_employee_master.emp_code WHERE  " + where;
                //changes by vinod for bagictm and RLIC HK
             //commwnt for pf corection 19/11   sql = "SELECT CONCAT(pay_billing_unit_rate_history.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.PF_NO, pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.PAN_No AS 'UAN', pay_pro_master.emp_name, CASE grade WHEN 'OFFICE BOY' THEN CASE pay_employee_master.gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade',ROUND((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)/pay_pro_master.Total_Days_Present * pay_pro_master.month_days) AS 'actual_basic_vda', pay_pro_master.STATUS, pay_pro_master.month_days, pay_pro_master.Total_Days_Present, round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END) AS 'emp_basic_vda', IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END) > 15000, 15000, round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)) AS 'EPS_WAGES', IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END * 8.33 / 100)))) AS 'EPS_CR', ROUND(pay_pro_master.sal_pf) AS 'EPF_CR', (pay_pro_master.month_days - pay_pro_master.Total_Days_Present) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  " + where;
                if (check1(i) != "")
                {
                   // sql = "SELECT CONCAT( pay_billing_unit_rate_history . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client', pay_pro_master . state_name , pay_pro_master . unit_name , pay_pro_master . PF_NO , pay_pro_master . ESI_No AS 'ESIC_NO', pay_pro_master . PAN_No AS 'UAN', pay_pro_master . emp_name , CASE grade WHEN 'OFFICE BOY' THEN CASE pay_employee_master . gender WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade', ROUND((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) / pay_pro_master . Total_Days_Present * pay_pro_master . month_days ) AS 'actual_basic_vda', pay_pro_master . STATUS , pay_pro_master . month_days , pay_pro_master . Total_Days_Present , ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) AS 'emp_basic_vda', IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, 15000, ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END)) AS 'EPS_WAGES',IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END * 8.33 / 100)))) AS 'EPS_CR',ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',( pay_pro_master . month_days - pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  " + where;
                    sql = "SELECT DISTINCT CONCAT( pay_billing_unit_rate_history . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client', pay_pro_master . state_name , pay_pro_master . unit_name , pay_pro_master . PF_NO , pay_pro_master . ESI_No AS 'ESIC_NO', pay_pro_master . PAN_No AS 'UAN', pay_pro_master . emp_name , CASE grade WHEN 'OFFICE BOY' THEN CASE pay_billing_unit_rate_history.grade_code WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade', ROUND((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) / pay_pro_master . Total_Days_Present * pay_pro_master . month_days ) AS 'actual_basic_vda', pay_pro_master . STATUS , pay_pro_master . month_days , pay_pro_master . Total_Days_Present , ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) AS 'emp_basic_vda', IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, 15000, ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END)) AS 'EPS_WAGES',IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END * 8.33 / 100)))) AS 'EPS_CR',ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',( pay_pro_master . month_days - pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL)  INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation  WHERE    `pay_pro_master`.`pf_status` IS NULL  and " + where;
                }
                else
                {
                    sql = "SELECT DISTINCT CONCAT( pay_billing_unit_rate_history . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client', pay_pro_master . state_name , pay_pro_master . unit_name , pay_pro_master . PF_NO , pay_pro_master . ESI_No AS 'ESIC_NO', pay_pro_master . PAN_No AS 'UAN', pay_pro_master . emp_name , CASE grade WHEN 'OFFICE BOY' THEN CASE pay_billing_unit_rate_history.grade_code WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE grade END AS 'grade', ROUND((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) / pay_pro_master . Total_Days_Present * pay_pro_master . month_days ) AS 'actual_basic_vda', pay_pro_master . STATUS , pay_pro_master . month_days , pay_pro_master . Total_Days_Present , ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) AS 'emp_basic_vda', IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, 15000, ROUND(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END)) AS 'EPS_WAGES',IF((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN pf_cmn_on = 0 THEN pay_pro_master . emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master . emp_basic_vda + pay_pro_master . hra_amount_salary + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master . emp_basic_vda + pay_pro_master . sal_bonus_gross + pay_pro_master . leave_sal_gross + pay_pro_master . washing_salary + pay_pro_master . travelling_salary + pay_pro_master . education_salary + pay_pro_master . other_allow + pay_pro_master . cca_salary + pay_pro_master . gratuity_gross + pay_pro_master . sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master . emp_basic_vda + pay_pro_master . cca_salary + pay_pro_master . other_allow END * 8.33 / 100)))) AS 'EPS_CR',ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',( pay_pro_master . month_days - pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL)  INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  " + where;
                }
            
            }
                // PF UPLOAD
            else if (i == 3)
            {
                // commwnt for pf corection 19/11 sql = "SELECT CONCAT(pay_pro_master.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client', pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.PF_NO, pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.PAN_No AS 'UAN', pay_pro_master.emp_name, pay_pro_master.grade, ROUND(pay_pro_master.actual_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow) AS 'actual_basic_vda', pay_pro_master.STATUS, pay_pro_master.month_days, pay_pro_master.Total_Days_Present, round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END) AS 'emp_basic_vda', IF((round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)) > 15000, 15000, round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)) AS 'EPS_WAGES', IF((round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((round(CASE WHEN pf_cmn_on = 0 THEN pay_pro_master.emp_basic_vda WHEN pf_cmn_on = 1 THEN pay_pro_master.emp_basic_vda + pay_pro_master.hra_amount_salary + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 2 THEN pay_pro_master.emp_basic_vda + pay_pro_master.sal_bonus_gross + pay_pro_master.leave_sal_gross + pay_pro_master.washing_salary + pay_pro_master.travelling_salary + pay_pro_master.education_salary + pay_pro_master.other_allow + pay_pro_master.cca_salary + pay_pro_master.other_allow + pay_pro_master.gratuity_gross + pay_pro_master.sal_ot WHEN pf_cmn_on = 3 THEN pay_pro_master.emp_basic_vda + pay_pro_master.cca_salary + pay_pro_master.other_allow END)*8.33)/100))) AS 'EPS_CR', ROUND(pay_pro_master.sal_pf) AS 'EPF_CR', (pay_pro_master.month_days - pay_pro_master.Total_Days_Present) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  " + where;
                if (check1(i) != "")
                {
                    sql = "SELECT DISTINCT CONCAT( pay_pro_master . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client',  pay_pro_master . state_name ,  pay_pro_master . unit_name ,  pay_pro_master . PF_NO ,  pay_pro_master . ESI_No  AS 'ESIC_NO',  pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name ,  pay_pro_master . grade , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda',  pay_pro_master . STATUS ,  pay_pro_master . month_days ,  pay_pro_master . Total_Days_Present , ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR', ( pay_pro_master . month_days  -  pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation  WHERE    `pay_pro_master`.`pf_status` IS NULL  and  " + where;
                }
                else
                {
                    sql = "SELECT DISTINCT CONCAT( pay_pro_master . client , '-', IFNULL( pay_pro_master . designation , '')) AS 'client',  pay_pro_master . state_name ,  pay_pro_master . unit_name ,  pay_pro_master . PF_NO ,  pay_pro_master . ESI_No  AS 'ESIC_NO',  pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name ,  pay_pro_master . grade , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda',  pay_pro_master . STATUS ,  pay_pro_master . month_days ,  pay_pro_master . Total_Days_Present , ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR', ( pay_pro_master . month_days  -  pay_pro_master . Total_Days_Present ) AS 'ncp_days' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation WHERE  " + where;
                }
            }
                //PT
            else if (i == 4)
            {
                sql = "SELECT DISTINCT (pay_pro_master.state_name) AS 'state', COUNT(pay_pro_master.emp_name) AS 'emp_count', IFNULL(ROUND(SUM(pay_pro_master.PT_AMOUNT)), 0) AS 'pt', pay_pro_master.client FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND invoice_no IS NOT NULL WHERE " + where;
            }
                    // esic excel sheet
            else if (i == 6)
            {
                sql = "SELECT CONCAT(pay_billing_unit_rate_history.client, '-', IFNULL(pay_pro_master.designation, '')) AS 'client',pay_pro_master.EMP_CODE, pay_pro_master.state_name, pay_pro_master.unit_name, case grade when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else grade end as grade, ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, ROUND(total_gross, 2) AS 'total_gross', ROUND(Total_Days_Present, 2) AS 'Total_Days_Present', pay_pro_master.month_days, ROUND((pay_pro_master.gross), 2) AS 'gross', ROUND(((pay_pro_master.gross * sal_esic_percent) / 100), 2) AS 'sal_esic', ROUND(((pay_pro_master.gross * bill_esic_percent) / 100), 2) AS 'bill_esic', ROUND(((pay_pro_master.emp_basic_vda * bill_esic_percent) / 100), 2) AS 'bill_esic_basic', ROUND(((pay_pro_master.emp_basic_vda * sal_esic_percent) / 100), 2) AS 'sal_esic_basic',`pay_pro_master`.`month`,`pay_pro_master`.`year` FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL)  inner join pay_employee_master on  pay_pro_master.emp_code = pay_employee_master.emp_code WHERE  " + where;
            }
                // PF Excel Sheet
            else if(i == 7 )
            {
                sql = "SELECT CONCAT(`pay_billing_unit_rate_history`.`client`, '-', IFNULL(`pay_pro_master`.`designation`, '')) AS 'client', `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_code`, `pay_pro_master`.`emp_name`, `pay_pro_master`.`PF_NO`, CASE `grade` WHEN 'OFFICE BOY' THEN CASE `pay_employee_master`.`gender` WHEN 'M' THEN 'OFFICE BOY' WHEN 'F' THEN 'OFFICE LADY' ELSE 'OFFICE BOY' END ELSE `grade` END AS 'grade', `pay_employee_master`.`joining_date`, `pay_pro_master`.`Total_Days_Present`, ROUND((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END)) AS 'basic', '0' AS 'basic_arr', '0' AS 'variable_da', '0' AS 'var_da_arr', ROUND((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END)) AS 'gross', IF((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END) > 15000, ROUND((15000 * 12) / 100), ROUND(((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END * 12 / 100)))) AS 'ee', IF((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END) > 15000, ROUND((15000 * 3.67) / 100), ROUND(((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END * 3.67 / 100)))) AS 'pf_er', IF((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END * 8.33 / 100)))) AS 'eps_er', IF((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END) > 15000, ROUND((15000 * 0.65) / 100), ROUND(((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END * 0.65 / 100)))) AS 'admin_charges', IF((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END) > 15000, ROUND((15000 * 0.5) / 100), ROUND(((CASE WHEN `pf_cmn_on` = 0 THEN `pay_pro_master`.`emp_basic_vda` WHEN `pf_cmn_on` = 1 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`hra_amount_salary` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 2 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`sal_bonus_gross` + `pay_pro_master`.`leave_sal_gross` + `pay_pro_master`.`washing_salary` + `pay_pro_master`.`travelling_salary` + `pay_pro_master`.`education_salary` + `pay_pro_master`.`other_allow` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`gratuity_gross` + `pay_pro_master`.`sal_ot` WHEN `pf_cmn_on` = 3 THEN `pay_pro_master`.`emp_basic_vda` + `pay_pro_master`.`cca_salary` + `pay_pro_master`.`other_allow` END * 0.5 / 100)))) AS 'edli_1', '0' AS 'edli_2',pay_pro_master.month,pay_pro_master.year FROM `pay_pro_master` INNER JOIN `pay_billing_unit_rate_history` ON `pay_pro_master`.`month` = `pay_billing_unit_rate_history`.`month` AND `pay_pro_master`.`year` = `pay_billing_unit_rate_history`.`year` AND `pay_pro_master`.`comp_code` = `pay_billing_unit_rate_history`.`comp_code` AND `pay_pro_master`.`state_name` = `pay_billing_unit_rate_history`.`state_name` AND `pay_pro_master`.`emp_code` = `pay_billing_unit_rate_history`.`emp_code` AND IF(`pay_billing_unit_rate_history`.`client_code` = 'RCPL', `invoice_no` IS NULL, `invoice_no` IS NOT NULL) INNER JOIN `pay_employee_master` ON `pay_pro_master`.`emp_code` = `pay_employee_master`.`emp_code` INNER JOIN `pay_salary_unit_rate` ON `pay_pro_master`.`comp_code` = `pay_salary_unit_rate`.`comp_code` AND `pay_billing_unit_rate_history`.`unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_billing_unit_rate_history`.`month` = `pay_salary_unit_rate`.`month` AND `pay_billing_unit_rate_history`.`year` = `pay_salary_unit_rate`.`year` AND `pay_billing_unit_rate_history`.`grade_code` = `pay_salary_unit_rate`.`designation` WHERE  " + where;
            }
            d.con.Open();
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                if (i == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ESIC_STATEMENT_" + "ALL" + ".xls");
                }
                else if (i == 5)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ESIC_UPLOAD_" + "ALL" + ".xls");
                }
                else if (i == 2)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=PF_STATEMENT_" + ddl_pf_client.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }
                else if (i == 3)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=PF_UPLOAD_" + ddl_pf_client.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }
                else if (i == 4)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=PT_UPLOAD_" + ddl_pt_unit.SelectedItem.Text.Replace(" ", "_") + ".xls");
                }else
                    if (i == 6)
                {
                        Response.AddHeader("content-disposition", "attachment;filename=ESIC_STATEMENT_EXCEL" + "ALL" + ".xls");
                }
                else  if(i == 7)
                    {
                        Response.AddHeader("content-disposition", "attachment;filename=PF_STATEMENT_EXCEL" + ddl_pf_client.SelectedItem.Text.Replace(" ", "_") + ".xls");
                    }

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, i, month_year_name);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, i, month_year_name);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, i, month_year_name);
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
        finally { d.con.Close(); }
    }




    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;
        string invoice = "";
        string bill_date = "";
        double rate = 0, paid_days = 0, working_days = 0, salary_esic = 0, emp_esic = 0, empr_esic = 0, total = 0, emp_esic_basic = 0, empr_esic_basic = 0, total_basic = 0, emp_count = 0, pt_total = 0,ac2 = 0;

        double basic_vda = 0, pf_paid_days = 0, pf_working_days = 0, gross_wages = 0, eps_wages = 0, epf_cr = 0, eps_cr = 0, diff_cr = 0, ncp_days = 0, refund = 0;
        string month_year = "";
        string header = "";
        string bodystr = "";
        int total_emp_count;

        double total_basic1;
        double total_basic_arr;
        double total_var_da;
        double total_var_da_arr;
        double total_gross;
        double total_ee;
        double total_pf_er;
        double total_eps_er;
        double total_admin_charges;
        double total_edli_1;
        double total_edli_2;
        private ListItemType listItemType;       


        public MyTemplate(ListItemType type, DataSet ds, int i,string month_year)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.month_year = month_year;
            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }

        public MyTemplate(ListItemType listItemType, DataSet ds, int i)
        {
            // TODO: Complete member initialization
            this.listItemType = listItemType;
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
                        lc = new LiteralControl("<table border=1 ><tr><th>SR. NO.</th><th>CLIENT NAME</th><th >STATE</th><th >LOCATION</th><th >ESIC NO.</th><th >EMPLOYEE NAME</th><th >DEG.</th><th>ESIC BASIC</th><th>WORKING DAYS ADJUSTMENT</th><th>WORKING DAYS CALCULATION DAYS</th><th>ESIC SALARY</th><th>EMPLOYEE CONTRIBUTION ON GROSS</th><th >EMPLOYER CONTRIBUTION ON GROSS</th><th>TOTAL</th><th>EMPLOYEE CONTRIBUTION ON BASIC</th><th >EMPLOYER CONTRIBUTION ON BASIC</th><th>TOTAL OF BASIC CONTRIBUTION</th></tr>");
                    }
                    else if (i == 5)
                    {
                        lc = new LiteralControl("<table border=1 ><tr><th>SR. NO.</th><th>IP NUMBER</th><th >IP NAME</th><th >NO OF DAYS FOR WHICH WAGES PAID/PAYABLE DURING THE MONTH</th><th >Total Monthly Wages</th><th >REASON CODE</th></tr>");
                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<table border=1 ><tr><th bgcolor=yellow colspan=26 align=center>" + ds.Tables[0].Rows[ctr]["client"] + " PF-ESIC STATEMENT " + month_year + "</th></tr><tr><th>SR. NO.</th><th >CLIENT NAME</th><th >STATE</th><th >LOCATION</th><th>EPF NO.</th><th >ESIC NO.</th><th >UAN</th><th >EMPLOYEE NAME</th><th>DESIGNATION</th><th>RATE(BASIC+VDA)</th><th>NEW/OLD</th><th>STATUS</th><th>BASIC RATE</th><th>WORKING M/O</th><th>WORKING DAYS(ACTUAL)</th><th>WORKING DAYS CALCULATION</th><th>RATE</th><th>GROSS WAGES</th><th>EPF WAGES</th><th>EPS WAGES</th><th>EDLI WAGES</th><th>EPF CONTRI REMITTED</th><th>EPS CONTRI REMITTED</th><th>EPF EPS DIFF REMITTED</th><th>NCP DAYS</th><th>REFUND OF ADVANCES</th></tr>");
                    }
                    else if (i == 3)
                    {
                        lc = new LiteralControl("<table  border=1 ><tr><th >UAN</th><th >EMPLOYEE NAME</th><th>GROSS</th><th>EPFO</th><th>EDLI</th><th>EPS</th><th>EPF CONTRI REMITTED</th><th>EPS CONTRI REMITTED</th><th>EPF EPS DIFF REMITTED</th><th>NCP DAYS</th><th>REFUND OF ADVANCES</th></tr>");
                    }
                    else if (i == 4)
                    {
                        lc = new LiteralControl("<table  border=1 ><tr><th>SR. NO.</th><th>CLIENT NAME</th><th >STATE</th><th>EMPLOYEE COUNT</th><th>TOTAL PT AMOUNT</th></tr>");
                    }
                    else if(i == 6)
                    {
                      //  lc = new LiteralControl("<table border=1 ><tr><td>Client Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["client"] + "</td></tr><tr><td>Employee PF Statement</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["month"] + " / " + ds.Tables[0].Rows[ctr]["year"] + "</td></tr><tr><td>Site Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td></tr><tr><th>SR. NO.</th><th>CLIENT NAME</th><th >STATE</th><th >LOCATION</th><th >ESIC NO.</th><th >EMPLOYEE NAME</th><th >DEG.</th><th>ESIC BASIC</th><th>WORKING DAYS ADJUSTMENT</th><th>WORKING DAYS CALCULATION DAYS</th><th>ESIC SALARY</th><th>EMPLOYEE CONTRIBUTION ON GROSS</th><th >EMPLOYER CONTRIBUTION ON GROSS</th><th>TOTAL</th><th>EMPLOYEE CONTRIBUTION ON BASIC</th><th >EMPLOYER CONTRIBUTION ON BASIC</th><th>TOTAL OF BASIC CONTRIBUTION</th></tr>");
                        //10-02-2020
                        //lc = new LiteralControl("<table border=1 ><tr><th>SR. NO.</th><th>CLIENT NAME</th><th >STATE</th><th >LOCATION</th><th >ESIC NO.</th><th >EMPLOYEE NAME</th><th >DEG.</th><th>ESIC BASIC</th><th>WORKING DAYS ADJUSTMENT</th><th>WORKING DAYS CALCULATION DAYS</th><th>ESIC SALARY</th><th>EMPLOYEE CONTRIBUTION ON GROSS</th><th >EMPLOYER CONTRIBUTION ON GROSS</th><th>TOTAL</th><th>EMPLOYEE CONTRIBUTION ON BASIC</th><th >EMPLOYER CONTRIBUTION ON BASIC</th><th>TOTAL OF BASIC CONTRIBUTION</th></tr>");
                        lc = new LiteralControl("<table border=1 ><tr><td>Client Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["client"] + "</td></tr><tr><td>Employee PF Statement</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["month"] + " / " + ds.Tables[0].Rows[ctr]["year"] + "</td></tr><tr><td>Site Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><tr><th>SR. NO.</th><th >ESIC NO.</th><th >Emp Code</th><th >EMPLOYEE NAME</th><th >EMPLOYEE DEG</th><th>ESIC BASIC</th><th>WORKING DAYS ADJUSTMENT</th><th>PAID DAYS</th><th>GROSS SALARY</th><th>EMPLOYEE ESIC DEDN(0.75)</th><th >EMPLOYER ESIC DEDN(3.25)</th><th>TOTAL</th></tr>");
                    }
                     else if (i == 7)
                    {
                        lc = new LiteralControl("<table border=1 ><tr><td>Client Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["client"] + "</td></tr><tr><td>Employee PF Statement</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["month"] + " / " + ds.Tables[0].Rows[ctr]["year"] + "</td></tr><tr><td>Site Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td align=center colspan = 8></td><td>A/C-01</td><td>A/C-01</td><td>A/C-10</td><td>A/C-02</td><td>A/C-21</td><td>A/C-22</td></tr><tr><th>SR. NO.</th><th>EMP CODE</th><th >EMPLOYEE NAME</th><th>EMP.P.F. NO.</th><th>EMPLOYEE DESIGNATION</th><th >EMP. D.O.J.</th><th >PAID DAYS</th><th >BASIC</th><th>BASIC ARR.</th><th>VARIABLE DA</th><th>VARIABLE DA ARR.</th><th>GROSS For PF (BA+DA)</th><th>EE (12%)</th><th>PF ER (3.67%)</th><th>APS RE (8.33%)</th><th>Admin Charges (0.65%)</th><th>EDLI (0.5%)</th><th>EDLI (0.00%)</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                     if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["total_gross"].ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["month_days"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString())), 2) + "</td></tr>");

                        rate = rate + double.Parse(ds.Tables[0].Rows[ctr]["total_gross"].ToString());
                        working_days = working_days + double.Parse(ds.Tables[0].Rows[ctr]["month_days"].ToString());
                        paid_days = paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                        salary_esic = salary_esic + double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString());
                        emp_esic = emp_esic + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2);
                        empr_esic = empr_esic + (double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));
                        total = total + (double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));

                        emp_esic_basic = emp_esic_basic + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString())), 2);
                        empr_esic_basic = empr_esic_basic + (double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString()));
                        total_basic = total_basic + (double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString()));
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 7>Total</td><td>" + Math.Round(rate,2) + "</td><td>" + working_days + "</td><td>" + Math.Round(paid_days) + "</td><td>" + Math.Round((salary_esic)) + "</td><td>" +Math.Round(emp_esic) + "</td><td>" + Math.Round((empr_esic)) + "</td><td>" + Math.Round((total)) + "</td><td>" + Math.Round(emp_esic_basic) + "</td><td>" + Math.Round((empr_esic_basic)) + "</td><td>" + Math.Round((total_basic)) + "</td></b></tr>";
                        }

                    }
                    if (i == 5)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + ds.Tables[0].Rows[ctr]["esic_salary"] + "</td><td>" + ds.Tables[0].Rows[ctr]["reason_code"] + "</td></tr>");

                        
                        paid_days = paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                        salary_esic = salary_esic + double.Parse(ds.Tables[0].Rows[ctr]["esic_salary"].ToString());
                       
                         if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 3>Total</td><td>" + Math.Round(paid_days) + "</td><td>" + Math.Round(salary_esic) + "</td></tr>";
                        }

                    }
                    else if (i == 2)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["PF_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["UAN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString()) + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["STATUS"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["month_days"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString()) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()))) + "</td><td>" + ds.Tables[0].Rows[ctr]["ncp_days"] + "</td><td>0</td></tr>");

                         basic_vda = basic_vda + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString());
                         pf_working_days = pf_working_days + double.Parse(ds.Tables[0].Rows[ctr]["month_days"].ToString());
                         pf_paid_days = pf_paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                         gross_wages = gross_wages + double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString());
                         eps_wages = eps_wages + double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString());
                         epf_cr = epf_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString());
                         eps_cr = eps_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString());
                         diff_cr = diff_cr + (double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()));
                         ncp_days = ncp_days + double.Parse(ds.Tables[0].Rows[ctr]["ncp_days"].ToString());
                         ac2 = (gross_wages * 0.5) / 100;
                         

                         if (ds.Tables[0].Rows.Count == ctr + 1)
                         {
                             if (ac2 < 500) { ac2 = 500; }
                             lc.Text = lc.Text + "<tr><b><td align=center colspan = 9>Total</td><td>" + basic_vda + "</td><td></td><td></td><td>" + basic_vda + "</td><td>" + pf_working_days + "</td><td>" + pf_paid_days + "</td><td>" + pf_paid_days + "</td><td>" + basic_vda + "</td><td>" + Math.Round((gross_wages), 2) + "</td><td>" + Math.Round((gross_wages), 2) + "</td><td>" + eps_wages + "</td><td>" + eps_wages + "</td><td>" + Math.Round((epf_cr)) + "</td><td>" + Math.Round((eps_cr)) + "</td><td>" + Math.Round((diff_cr)) + "</td><td>" + ncp_days + "</td><td>0</td></b></tr><tr></tr><tr><td colspan = 5></td><td>Employee & Employer</td><td>A/C 1</td><td>" + (epf_cr + diff_cr) + "</td><td colspan = 18></tr><tr><td colspan = 5></td><td>Administrative charge</td><td>A/C 2</td><td>" + Math.Ceiling(ac2) + "</td><td colspan = 18></tr><tr><td colspan = 5></td><td>Pension Fund</td><td>A/C 10</td><td>" + eps_cr + "</td><td colspan = 18></tr><tr><td colspan = 5></td><td>Employer Contribution</td><td>A/C 21</td><td>" + Math.Ceiling((gross_wages * 0.5) / 100) + "</td><td colspan = 18></tr><tr><td colspan = 5></td><td>Administrative charge</td><td>A/C 22</td><td>0</td><td colspan = 18></tr><tr><td colspan = 5></td><td></td><td>Total</td><td>" + Math.Round(((epf_cr + diff_cr) + (ac2) + ((gross_wages * 0.5) / 100) + eps_cr)) + "</td><td colspan = 18></tr>";
                         }
                     }
                     else if (i == 3)
                     {
                         lc = new LiteralControl("<tr><td>'" + ds.Tables[0].Rows[ctr]["UAN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString())), 2) + "</td><td>" + ds.Tables[0].Rows[ctr]["ncp_days"] + "</td><td>0</td></tr>");

                         gross_wages = gross_wages + double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString());
                         eps_wages = eps_wages + double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString());
                         epf_cr = epf_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString());
                         eps_cr = eps_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString());
                         diff_cr = diff_cr + (double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()));
                         ncp_days = ncp_days + double.Parse(ds.Tables[0].Rows[ctr]["ncp_days"].ToString());
                         ac2 = Math.Ceiling(((gross_wages * 0.5) / 100));
                         

                         if (ds.Tables[0].Rows.Count == ctr + 1)
                         {
                             if (ac2 < 500) { ac2 = 500; }
                             lc.Text = lc.Text + "<tr><b><td align=center colspan = 2>Total</td><td>" + Math.Round((gross_wages), 2) + "</td><td>" + Math.Round((gross_wages), 2) + "</td><td>" + eps_wages + "</td><td>" + eps_wages + "</td><td>" + Math.Round((epf_cr), 2) + "</td><td>" + Math.Round((eps_cr), 2) + "</td><td>" + Math.Round((diff_cr), 2) + "</td><td>" + ncp_days + "</td><td>0</td></b></tr><tr></tr><tr><td>A/C 1</td><td>" + Math.Round((epf_cr + diff_cr)) + "</td></tr><tr><td>A/C 2</td><td>" + Math.Ceiling(ac2) + "</td></tr><tr><td>A/C 10</td><td>" + eps_cr + "</td></tr><tr><td>A/C 21</td><td>" + Math.Ceiling((gross_wages * 0.5) / 100) + "</td></tr><tr><td>A/C 22</td><td>0</td></tr><tr><td>TOTAL</td><td>" + Math.Round(((epf_cr + diff_cr) + (ac2) + ((gross_wages * 0.5) / 100) + eps_cr), 2) + "</td></tr><tr><td>TOTAL EMP</td><td>" + ds.Tables[0].Rows.Count + "</td></tr>";
                         }
                         
                     }
                     else if (i == 4)
                     {
                         lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_count"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["pt"].ToString()) + "</td></tr>");

                         emp_count = emp_count + double.Parse(ds.Tables[0].Rows[ctr]["emp_count"].ToString());
                         pt_total = pt_total + double.Parse(ds.Tables[0].Rows[ctr]["pt"].ToString());

                         if (ds.Tables[0].Rows.Count == ctr + 1)
                         {
                             lc.Text = lc.Text + "<tr><b><td align=center colspan = 3>Total</td><td>" + emp_count + "</td><td>" + Math.Round((pt_total), 2) + "</td></tr>";
                         }
                     }
                    else if (i == 6)
                    {
                        //10-02-2020
                        //lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["total_gross"].ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["month_days"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString())), 2) + "</td></tr>");
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["total_gross"].ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["month_days"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString())), 2) + "</td></tr>");

                        rate = rate + double.Parse(ds.Tables[0].Rows[ctr]["total_gross"].ToString());
                        working_days = working_days + double.Parse(ds.Tables[0].Rows[ctr]["month_days"].ToString());
                        paid_days = paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                        salary_esic = salary_esic + double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString());
                        emp_esic = emp_esic + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString())), 2);
                        empr_esic = empr_esic + (double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));
                        total = total + (double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic"].ToString()));

                       // emp_esic_basic = emp_esic_basic + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString())), 2);
                        //empr_esic_basic = empr_esic_basic + (double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString()));
                        //total_basic = total_basic + (double.Parse(ds.Tables[0].Rows[ctr]["sal_esic_basic"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["bill_esic_basic"].ToString()));
                        total_emp_count = total_emp_count + ctr;
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>" + Math.Round(rate, 2) + "</td><td>" + working_days + "</td><td>" + Math.Round(paid_days) + "</td><td>" + Math.Round((salary_esic)) + "</td><td>" + Math.Round(emp_esic) + "</td><td>" + Math.Round((empr_esic)) + "</td><td>" + Math.Round((total)) + "</td></b></tr><tr></tr><tr><td>Client Name </td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["client"] + "</td></tr><tr><td>ESIC Summery</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["month"] + " / " + ds.Tables[0].Rows[ctr]["year"] + "</td></tr><tr><td>Site Name </td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td></tr><tr><td>ESIC Subcode </td><td align=center colspan = 3>'33310485630021001</td></tr><tr><td align=center colspan = 3>No of Employee</td><td>" + (ctr + 1) + "</td></tr><tr><td align=center colspan = 3> Gross for Esic</td><td> " + Math.Round(rate, 2) + "</td></tr><tr><td align=center colspan = 3>ESIC EE 0.75%</td><td>" + Math.Round(emp_esic) + "</td></tr><tr><td align=center colspan = 3>ESIC ER 3.25% </td><td>" + Math.Round((empr_esic)) + "</td></tr><tr><td align=center colspan = 3>Total </td><td>" + Math.Round((total)) + "</td></tr>";
                        }

                    }
                    else if(i==7){
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_NO"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + ds.Tables[0].Rows[ctr]["basic"] + "</td><td>" + ds.Tables[0].Rows[ctr]["basic_arr"] + "</td><td>" + ds.Tables[0].Rows[ctr]["variable_da"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["var_da_arr"] + "</td><td>" + double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString()) + "</td><td>" + ds.Tables[0].Rows[ctr]["ee"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pf_er"] + "</td><td>" + ds.Tables[0].Rows[ctr]["eps_er"] + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["admin_charges"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["edli_1"].ToString()))) + "</td><td>" + Math.Round((double.Parse(ds.Tables[0].Rows[ctr]["edli_2"].ToString()))) + "</td></tr>");


                        pf_paid_days = pf_paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                         total_basic1 = total_basic1 + double.Parse(ds.Tables[0].Rows[ctr]["basic"].ToString());
                         total_basic_arr = total_basic_arr + double.Parse(ds.Tables[0].Rows[ctr]["basic_arr"].ToString());
                         total_var_da = total_var_da + double.Parse(ds.Tables[0].Rows[ctr]["variable_da"].ToString());
                         total_var_da_arr = total_var_da_arr + double.Parse(ds.Tables[0].Rows[ctr]["var_da_arr"].ToString());
                         total_gross = total_gross + double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString());
                         total_ee = total_ee + double.Parse(ds.Tables[0].Rows[ctr]["ee"].ToString());
                         total_pf_er = total_pf_er + double.Parse(ds.Tables[0].Rows[ctr]["pf_er"].ToString());
                         total_eps_er = total_eps_er + double.Parse(ds.Tables[0].Rows[ctr]["eps_er"].ToString());
                         total_admin_charges = total_admin_charges + double.Parse(ds.Tables[0].Rows[ctr]["admin_charges"].ToString());
                         total_edli_1 = total_edli_1 + double.Parse(ds.Tables[0].Rows[ctr]["edli_1"].ToString());
                         total_edli_2 = total_edli_2 + double.Parse(ds.Tables[0].Rows[ctr]["edli_2"].ToString());

                        //basic_vda = basic_vda + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString());
                        //pf_working_days = pf_working_days + double.Parse(ds.Tables[0].Rows[ctr]["month_days"].ToString());
                        //gross_wages = gross_wages + double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString());
                        //eps_wages = eps_wages + double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString());
                        //epf_cr = epf_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString());
                        //eps_cr = eps_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString());
                        //diff_cr = diff_cr + (double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()));
                        //ncp_days = ncp_days + double.Parse(ds.Tables[0].Rows[ctr]["ncp_days"].ToString());
                        //ac2 = (gross_wages * 0.5) / 100;
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            if (ac2 < 500) { ac2 = 500; }
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 6>Total</td><td>" + pf_paid_days + "</td><td>" + total_basic1 + "</td><td>" + total_basic_arr + "</td><td>" + total_var_da + "</td><td>" + total_var_da_arr + "</td><td>" + total_gross + "</td><td>" + total_ee + "</td><td>" + total_pf_er + "</td><td>" + total_eps_er + "</td><td>" + total_admin_charges + "</td><td>" + total_edli_1 + "</td><td>" + total_edli_2 + "</td></tr><tr></tr><tr><tr><td></td><td>No. Of Employees</td><td>" + (ctr + 1) + "</td></tr><tr><td></td><td>Gross for PF</td><td>" + total_basic1 + "</td></tr><td>A/C 01</td><td>EE (12%)</td><td>" + total_ee + "</td></tr><tr><td>A/C 01</td><td>PF ER (3.67%)</td><td>" + total_pf_er + "</td></tr><tr><td>A/C 10</td><td>EPS ER (8.33%)</td><td>" + total_eps_er + "</td></tr><tr><td>A/C 02</td><td>Admin Charges (0.65%)</td><td>" + total_admin_charges + "</td></tr><tr><td>A/C 21</td><td>EDIL (0.5%)</td><td>" + total_edli_1 + "</td></tr><tr><td>A/C 22</td><td>EDIL (0.00%)</td><td>" + total_edli_2 + "</td></tr><tr><td>Ac 1+1+2+10+21+22 </td><td>Total</td><td>" + (total_ee + total_eps_er + total_pf_er + total_admin_charges + total_edli_1 + total_edli_2) + "</td></tr>";
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
    protected void btn_esic_statement_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
         string where = where_clause("ALL", ddl_esic_state.SelectedValue, "ALL", txt_esic_date.Text,1);
        string monthname = month_date(txt_esic_date.Text);
        //remove (group by pay_employee_master.emp_code) for timeout 
        //generate_slab_report(1, where.Replace("order by", "  group by pay_employee_master.emp_code order by"), monthname);
        generate_slab_report(1, where.Replace("order by", "   order by"), monthname);
    }
    protected void btn_esic_upload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        string where = where_clause("ALL", ddl_esic_state.SelectedValue, "ALL", txt_esic_date.Text,1);
        string monthname = month_date(txt_esic_date.Text);
        generate_slab_report(5, where, monthname);
    }

    protected void btn_esic_excel_Click(object sender,EventArgs e) {

        hidtab.Value = "4";
        string where = where_clause("ALL", ddl_esic_state.SelectedValue, "ALL", txt_esic_date.Text, 1);
        string monthname = month_date(txt_esic_date.Text);
        generate_slab_report(6, where.Replace("order by", "  group by pay_employee_master.emp_code order by"), monthname);

    }

    protected void btn_pf_upload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        string where = where_clause(ddl_pf_client.SelectedValue, "ALL", "ALL", txt_pf_date.Text,0);
        string monthname = month_date(txt_pf_date.Text);
        //generate_slab_report(2, where.Replace("order by", "  group by pay_pro_master.emp_code order by"), monthname);
        generate_slab_report(2, where.Replace("order by", "   order by"), monthname);
    }
    protected void btn_pf_upload_count_Click(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        string where = where_clause(ddl_pf_client.SelectedValue, "ALL", "ALL", txt_pf_date.Text,0);
        string monthname = month_date(txt_pf_date.Text);
        //generate_slab_report(3, where.Replace("order by", "  group by pay_pro_master.emp_code order by"), monthname);
        generate_slab_report(3, where.Replace("order by", "  order by"), monthname);
    }
    protected void btn_pt_Click(object sender, EventArgs e)
    {
        hidtab.Value = "6";
        string where = where_clause(ddl_pt_client.SelectedValue, ddl_pt_state.SelectedValue, ddl_pt_unit.SelectedValue, txt_pt_date.Text,0);
        string monthname = month_date(txt_pt_date.Text);
        generate_slab_report(4, where, monthname);
    }


    protected void btn_pf_excel_sheet(object  sender , EventArgs e) {
        hidtab.Value = "5";
        string where = where_clause(ddl_pf_client.SelectedValue, "ALL", "ALL", txt_pf_date.Text, 0);
        string monthname = month_date(txt_pf_date.Text);
        generate_slab_report(7, where.Replace("order by", "  group by pay_employee_master.emp_code order by"), monthname);
    }

    //PT slab master Esic,pf,pt
    public string where_clause(string client_code, string state_name, string unit_code, string txt_month_year ,int counter)
    {

        string sql = null;
        string where = "", emp_type = "";
        try
        {

            if (client_code == "ALL" && counter == 1) { emp_type = " AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent')"; }
            else if (client_code == "BAGICTM" || client_code == "BAGIC TM") { emp_type = " AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent')"; }
            else { emp_type = " and pay_pro_master.Employee_type = 'Permanent'"; }
            string hdfc_type = "";
            if (ddl_pf_client.SelectedValue == "HDFC") { hdfc_type = " and pay_pro_master.hdfc_type='manpower_bill' "; }
                
            if (client_code == "ALL")
            {
                if (state_name == "ALL")
                {

                    //AND IF(pay_unit_master.client_code = 'BAGIC TM', pay_employee_master.Employee_type = 'Temporary' OR pay_employee_master.Employee_type = 'Permanent', pay_employee_master.Employee_type = 'Permanent')
                    where = "pay_pro_master.month ='" + txt_month_year.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Substring(3) + "' AND pay_pro_master.comp_code='" + Session["comp_code"] + "'   " + emp_type + "  and (PAN_No is not null and PAN_No!='') AND (ESI_No is not null AND ESI_No!='')" + hdfc_type + " order by pay_pro_master.client,pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name ";
                }
                else
                {
                    where = "pay_pro_master.month ='" + txt_month_year.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Substring(3) + "' AND pay_pro_master.comp_code='" + Session["comp_code"] + "' and pay_pro_master.state_name='" + state_name + "' " + emp_type + " and (PAN_No is not null and PAN_No!='') AND (ESI_No is not null AND ESI_No!='') "+hdfc_type+" order by pay_pro_master.client,pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name ";
                }
            }
            else
            {
                if (state_name == "ALL")
                {
                    where = "pay_pro_master.month ='" + txt_month_year.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Substring(3) + "' AND pay_pro_master.comp_code='" + Session["comp_code"] + "' and pay_pro_master.client_code = '" + client_code + "' " + emp_type + " and (PAN_No is not null and PAN_No!='') AND (ESI_No is not null AND ESI_No!='') " + hdfc_type + " order by pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name ";
                }
                else
                {
                    if (unit_code == "ALL")
                    {
                        where = "pay_pro_master.month ='" + txt_month_year.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Substring(3) + "' AND pay_pro_master.comp_code='" + Session["comp_code"] + "' and pay_pro_master.client_code = '" + client_code + "' and pay_pro_master.state_name='" + state_name + "'  " + emp_type + "  and (PAN_No is not null and PAN_No!='') AND (ESI_No is not null AND ESI_No!='') " + hdfc_type + " order by pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name";
                    }
                    else
                    {
                        where = "pay_pro_master.month ='" + txt_month_year.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_month_year.Substring(3) + "' AND pay_pro_master.comp_code='" + Session["comp_code"] + "' and pay_pro_master.client_code = '" + client_code + "' and pay_pro_master.state_name='" + state_name + "' and pay_pro_master.unit_code ='" + unit_code + "' " + emp_type + "  and (PAN_No is not null and PAN_No!='') AND (ESI_No is not null AND ESI_No!='') " + hdfc_type + " order by pay_pro_master.state_name,pay_pro_master.unit_name,pay_pro_master.emp_name ";
                    }

                }
               
            }
            return where;
        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
       
    }

    public string month_date(string txt_month_year)
    {

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        return (mfi.GetMonthName(int.Parse(txt_month_year.Substring(0, 2))).ToString().ToUpper()) + "_" + txt_month_year.Substring(3);

    }

    protected void btn_upload_pf_Click(object sender, EventArgs e)
    {
        hidtab.Value = "5";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (d.getsinglestring("select comp_code from pay_compliance_files where  comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year = '" + txt_pf_date.Text.Substring(3) + "' and type='PF'").Equals(""))
        {
            d.operation("insert into pay_compliance_files (comp_code,Client_name,client_code,month,year,last_updated,last_update_time,type) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_pf_client.SelectedItem.ToString() + "','" + ddl_pf_client.SelectedValue + "','" + txt_pf_date.Text.Substring(0, 2) + "','" + txt_pf_date.Text.Substring(3) + "','" + Session["LOGIN_ID"].ToString() + "',now(),'PF')");
        }
        if (upd_ack.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_ack.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_ack.PostedFile.FileName);
                upd_ack.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_ack" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_pf_amount.Text + ", file_ack='" + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_ack" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year = '" + txt_pf_date.Text.Substring(3) + "' and type='PF'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_trrn.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_trrn.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_trrn.PostedFile.FileName);
                upd_trrn.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_trrn" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_pf_amount.Text + ", file_trrn='" + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_trrn" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year = '" + txt_pf_date.Text.Substring(3) + "' and type='PF'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_challan.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_challan.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_challan.PostedFile.FileName);
                upd_challan.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_challan" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_pf_amount.Text + ", file_challan='" + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_challan" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year = '" + txt_pf_date.Text.Substring(3) + "' and type='PF'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_ecr.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_ecr.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_ecr.PostedFile.FileName);
                upd_ecr.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_ecr" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_pf_amount.Text + ", file_ecr='" + Session["COMP_CODE"].ToString() + "_" + ddl_pf_client.SelectedValue + txt_pf_date.Text.Replace("/", "_") + "_ecr" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year = '" + txt_pf_date.Text.Substring(3) + "' and type='PF'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Files Uploaded Successfully !!!')", true);
        load_grdview_pf();
        txt_pf_amount.Text = "";
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
        e.Row.Cells[7].Text = set_month(e.Row.Cells[7].Text);
        e.Row.Cells[1].Visible = false;
    }
    protected void grd_company_files_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string[] stringArray = { "file_ack", "file_trrn", "file_challan", "file_ecr" };
        int item = (int)grd_company_files.DataKeys[e.RowIndex].Value;
        for (int i = 0; i < stringArray.Length; i++)
        {
            string temp = d.getsinglestring("SELECT " + stringArray[i].ToString() + " FROM pay_compliance_files WHERE id=" + item);
            if (temp != "")
            {
                File.Delete(Server.MapPath("~/compliance/") + temp);
            }
        }
        d.operation("delete from pay_compliance_files WHERE id=" + item);
        load_grdview_pf();
    }
    private void load_grdview_pf()
    {
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,client_code,client_name, concat('~/compliance/',file_ack) as Value1,concat('~/compliance/',file_trrn) as Value2,concat('~/compliance/',file_challan) as Value3,concat('~/compliance/',file_ecr) as Value4,month,year FROM pay_compliance_files where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and client_code= '" + ddl_pf_client.SelectedValue + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' and year='" + txt_pf_date.Text.Substring(3) + "' and type = 'PF'", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        grd_company_files.DataSource = DS1;
        grd_company_files.DataBind();
    }
    private void load_grdview_esic()
    {
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,state_name, concat('~/compliance/',file_ack) as Value1,concat('~/compliance/',file_trrn) as Value2,concat('~/compliance/',file_challan) as Value3,concat('~/compliance/',file_ecr) as Value4,month,year FROM pay_compliance_files where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and state_name= '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year='" + txt_esic_date.Text.Substring(3) + "' and type = 'ESIC'", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        grd_esic_upload.DataSource = DS1;
        grd_esic_upload.DataBind();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void grd_company_files_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_company_files.UseAccessibleHeader = false;
            grd_company_files.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_sys_upload_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        hidtab.Value = "5";
        pnl_sys_upload.Visible = true;
        load_grdview_pf();
    }
    private string set_month(string month)
    {
        if (month == "1") { return "January"; }
        else if (month == "2") { return "February"; }
        else if (month == "3") { return "March"; }
        else if (month == "4") { return "April"; }
        else if (month == "5") { return "May"; }
        else if (month == "6") { return "June"; }
        else if (month == "7") { return "July"; }
        else if (month == "8") { return "August"; }
        else if (month == "9") { return "September"; }
        else if (month == "10") { return "October"; }
        else if (month == "11") { return "November"; }
        else if (month == "12") { return "December"; }
        return month;
    }
    protected void btn_esic_uload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";
        pnl_esic.Visible = true;
        load_grdview_esic();
    }
    protected void btn_esic_sys_upload_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (d.getsinglestring("select comp_code from pay_compliance_files where comp_code ='" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year = '" + txt_esic_date.Text.Substring(3) + "' and type='ESIC'").Equals(""))
        {
            d.operation("insert into pay_compliance_files (comp_code,state_name,month,year,last_updated,last_update_time,type) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_esic_state.SelectedValue + "','" + txt_esic_date.Text.Substring(0, 2) + "','" + txt_esic_date.Text.Substring(3) + "','" + Session["LOGIN_ID"].ToString() + "',now(),'ESIC')");
        }
        if (upd_esic_ack.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_esic_ack.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_esic_ack.PostedFile.FileName);
                upd_esic_ack.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_ack" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_esic_amount.Text + ", file_ack='" + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_ack" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year = '" + txt_esic_date.Text.Substring(3) + "' and type='ESIC'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_esic_trrn.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_esic_trrn.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_esic_trrn.PostedFile.FileName);
                upd_esic_trrn.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_trrn" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_esic_amount.Text + ", file_trrn='" + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_trrn" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year = '" + txt_esic_date.Text.Substring(3) + "' and type='ESIC'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_esic_challan.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_esic_challan.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_esic_challan.PostedFile.FileName);
                upd_esic_challan.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_challan" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_esic_amount.Text + ", file_challan='" + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_challan" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year = '" + txt_esic_date.Text.Substring(3) + "' and type='ESIC'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        if (upd_esic_ecr.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(upd_esic_ecr.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG")
            {
                string fileName = Path.GetFileName(upd_esic_ecr.PostedFile.FileName);
                upd_esic_ecr.PostedFile.SaveAs(Server.MapPath("~/compliance/") + fileName);

                File.Copy(Server.MapPath("~/compliance/") + fileName, Server.MapPath("~/compliance/") + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_ecr" + fileExt, true);
                File.Delete(Server.MapPath("~/compliance/") + fileName);

                d.operation("update pay_compliance_files set amount=" + txt_esic_amount.Text + ", file_ecr='" + Session["COMP_CODE"].ToString() + "_" + ddl_esic_state.SelectedValue + txt_esic_date.Text.Replace("/", "_") + "_ecr" + fileExt + "' where comp_code ='" + Session["COMP_CODE"].ToString() + "' and state_name = '" + ddl_esic_state.SelectedValue + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' and year = '" + txt_esic_date.Text.Substring(3) + "' and type='ESIC'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Files Uploaded Successfully !!!')", true);
        load_grdview_esic();
        txt_esic_amount.Text = "";
    }
    protected void grd_esic_upload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[7].Text = set_month(e.Row.Cells[7].Text);
        e.Row.Cells[1].Visible = false;
    }
    protected void grd_esic_upload_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string[] stringArray = { "file_ack", "file_trrn", "file_challan", "file_ecr" };
        int item = (int)grd_esic_upload.DataKeys[e.RowIndex].Value;
        for (int i = 0; i < stringArray.Length; i++)
        {
            string temp = d.getsinglestring("SELECT " + stringArray[i].ToString() + " FROM pay_compliance_files WHERE id=" + item);
            if (temp != "")
            {
                File.Delete(Server.MapPath("~/compliance/") + temp);
            }
        }
        d.operation("delete from pay_compliance_files WHERE id=" + item);
        load_grdview_esic();
    }
    protected void grd_esic_upload_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_esic_upload.UseAccessibleHeader = false;
            grd_esic_upload.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
    //vikas 09/05/2019
    protected void state_laber_office()
    {
        ddl_labour_state.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct STATE_NAME,STATE_CODE from pay_state_master order by state_name", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_labour_state.DataSource = dt_item;
                ddl_labour_state.DataTextField = dt_item.Columns[0].ToString();
                ddl_labour_state.DataBind();
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
    protected void btn_labour_save_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res=0;
        res = d.operation("insert into pay_labour_office(state_name,city,address)values('" + ddl_labour_state.SelectedValue + "','" + txt_labour_location.Text + "','" + txt_labour_address.Text + "')");
     if (res > 0)
     {
         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Insert Sucessfuly !!');", true);
         gv_labourOffice();
         clear();
     }
    }
    protected void gv_labourOffice()
{
    System.Data.DataTable dt_item = new System.Data.DataTable();
    MySqlDataAdapter cmd_item = new MySqlDataAdapter("select id, STATE_NAME,city,address from pay_labour_office order by state_name", d.con);
    d.con.Open();
    try
    {
        cmd_item.Fill(dt_item);
        if (dt_item.Rows.Count > 0)
        {
            gv_labour_office.DataSource = dt_item;
            gv_labour_office.DataBind();
        }
        else
        {
            gv_labour_office.DataSource = null;
            gv_labour_office.DataBind();
        }
        dt_item.Dispose();
        d.con.Close();

    }
    catch (Exception ex) { throw ex; }
    finally
    {
        txt_labour_location.Text = "";
        txt_labour_address.Text = "";
        d.con.Close();
    }
}
    protected void btn_labour_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void gv_labour_office_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_labour_office.UseAccessibleHeader = false;
            gv_labour_office.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_labour_office_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string id = gv_labour_office.SelectedRow.Cells[0].Text;
            MySqlCommand cmd = new MySqlCommand("select id,state_name,city,address from pay_labour_office where id='" + id + "' ", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ddl_labour_state.SelectedValue = dr.GetValue(1).ToString();
                txt_labour_location.Text = dr.GetValue(2).ToString();
                txt_labour_address.Text = dr.GetValue(3).ToString();

            }
            d.con.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            d.con.Close();
            btn_labour_save.Visible = false;
            btn_update.Visible = true;
        }
    }
    protected void gv_labour_office_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_labour_office, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string id = gv_labour_office.SelectedRow.Cells[0].Text;
        int resolt = 0;
        resolt = d.operation("Update pay_labour_office Set state_name='" + ddl_labour_state.SelectedValue + "',city='" + txt_labour_location.Text + "',address='" + txt_labour_address.Text + "' where id='" + id + "'");
        if (resolt > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Succsefully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Faild!');", true);
        }
        btn_labour_save.Visible = true;
        btn_update.Visible = false;
        gv_labourOffice();
        clear();
    }
    protected void lnkbtn_removeitem_Click1(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string id = gv_labour_office.SelectedRow.Cells[0].Text;
        int resolt = 0;
        resolt = d.operation("DELETE FROM pay_labour_office WHERE  id='" + id + "' ");
        if (resolt > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Succsefully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Faild!');", true);
        }
        gv_labourOffice();
        btn_update.Visible = false;
        btn_labour_save.Visible = true;
        clear();
    }
    protected void clear()
    {
        ddl_labour_state.SelectedValue = "Andaman and Nicobar Islands";
        txt_labour_address.Text = "";
        txt_labour_location.Text = "";
    }

    protected void ddl_uan_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "8";
                    //state name
            string query = "";
            if (ddl_uan_client_list.SelectedValue == "ALL")
            {
                query = "select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["COMP_CODE"] + "' order by 1";
            }
            else
            {
                query = "select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["COMP_CODE"] + "' and client_code = '" + ddl_uan_client_list.SelectedValue+ "'  order by 1";
            }

            ddl_uan_state_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter(query, d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_uan_state_name.DataSource = dt_item;
                    ddl_uan_state_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_uan_state_name.DataValueField = dt_item.Columns[0].ToString();
                    ddl_uan_state_name.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_uan_state_name.Items.Insert(0, "ALL");

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        
    }
    protected void ddl_uan_state_SelectedIndexChanged(object sender ,EventArgs e) {
        // unit name
        hidtab.Value = "8";
        string query = "";
        if (ddl_uan_state_name.SelectedValue == "ALL")
        {
            query = "SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'UNIT_NAME', unit_code FROM pay_unit_master WHERE comp_code = '"+Session["COMP_CODE"].ToString()+"' AND client_code = '"+ddl_uan_client_list.SelectedValue+"'  AND branch_status = 0 ORDER BY UNIT_CODE";
        }
        else
        {
            query = "SELECT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'UNIT_NAME', unit_code FROM pay_unit_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_uan_client_list.SelectedValue + "' and STATE_NAME='" + ddl_uan_state_name.SelectedItem + "' AND branch_status = 0 ORDER BY UNIT_CODE";
        }

        ddl_uan_unit_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter(query, d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_uan_unit_name.DataSource = dt_item;
                ddl_uan_unit_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_uan_unit_name.DataValueField = dt_item.Columns[1].ToString();
                ddl_uan_unit_name.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_uan_unit_name.Items.Insert(0, "Select Unit");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_uan_exceldownload_click(object  sender , EventArgs e) 
    {
        hidtab.Value = "8";
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        

        ws.Cells[1, 1] = "UAN";
        ws.Cells[1, 2] = "PrvMemberId";
        ws.Cells[1, 3] = "MemberName";
        ws.Cells[1, 4] = "DOB";
        ws.Cells[1, 5] = "DOJ ";
        ws.Cells[1, 6] = "Gender";
        ws.Cells[1, 7] = "FatherHusbandName";
        ws.Cells[1, 8] = "Relation";
        ws.Cells[1, 9] = "MobileNo";
        ws.Cells[1, 10] = "EmailId";
        ws.Cells[1, 11] = "Nationality";
        ws.Cells[1, 12] = "Wages";
        ws.Cells[1, 13] = "Qualification";
        ws.Cells[1, 14] = "MaritalStatus";
        ws.Cells[1, 15] = "IsInternationalWorker";
        ws.Cells[1, 16] = "CountryOfOrigin";
        ws.Cells[1, 17] = "PassportNumber";
        ws.Cells[1, 18] = "PassportValidFrom";
        ws.Cells[1, 19] = "PassportValidTill";
        ws.Cells[1, 20] = "IsPhisicalHandicap";
        ws.Cells[1, 21] = "Locomotive";
        ws.Cells[1, 22] = "Hearing";
        ws.Cells[1, 23] = "Visual";
        ws.Cells[1, 24] = "BankAccNo";
        ws.Cells[1, 25] = "Ifsc";
        ws.Cells[1, 26] = "NameAsPerBank";
        ws.Cells[1, 27] = "PAN";
        ws.Cells[1, 28] = "NameAsPerPan";
        ws.Cells[1, 29] = "AdhaarNo";
        ws.Cells[1, 30] = "NameAsPerAdhaar";
      

        try
        {
            string query="";
            if(ddl_uan_state_name.SelectedValue=="ALL" && ddl_uan_unit_name.SelectedValue=="ALL"){
                query = "select  concat(',',pan_number) as  'UAN' , '' AS 'PrvMemberId',emp_name AS 'MemberName',CONCAT(' ', DATE_FORMAT(birth_date, '%d/%m/%Y')) AS 'DOB',CONCAT(' ', DATE_FORMAT(joining_date, '%d/%m/%Y')) AS 'DOJ',Gender,emp_father_name AS 'FatherHusbandName',CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END AS 'Relation',CONCAT_WS(' ', emp_mobile_no) AS 'MobileNo',emp_email_id AS 'EmailId',emp_nationality AS 'Nationality','' AS 'Wages',emp_qualification AS 'Qualification',emp_marrital_status AS 'MaritalStatus','N' AS 'IsInternationalWorker','' AS 'CountryOfOrigin','' AS 'PassportNumber','' AS 'PassportValidFrom','' AS 'PassportValidTill','N' AS 'IsPhisicalHandicap','' AS 'Locomotive','' AS 'Hearing','' AS 'Visual',CONCAT(',', original_bank_account_no) AS 'BankAccNo',pf_ifsc_code AS 'Ifsc',bank_holder_name AS 'NameAsPerBank',EMP_NEW_PAN_NO AS 'PAN','' AS 'NameAsPerPan',CONCAT(',', p_tax_number) AS 'AdhaarNo',emp_name AS 'NameAsPerAdhaar' from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_uan_client_list.SelectedValue + "' and  employee_type in ('Permanent','Temporary') AND (pay_employee_master.LEFT_REASON IS NULL OR pay_employee_master.LEFT_REASON = '') order by  pan_number='' , emp_code ";
            }else {
                query = "select  concat(',',pan_number) as  'UAN' , '' AS 'PrvMemberId',emp_name AS 'MemberName',CONCAT(' ', DATE_FORMAT(birth_date, '%d/%m/%Y')) AS 'DOB',CONCAT(' ', DATE_FORMAT(joining_date, '%d/%m/%Y')) AS 'DOJ',Gender,emp_father_name AS 'FatherHusbandName',CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END AS 'Relation',CONCAT_WS(' ', emp_mobile_no) AS 'MobileNo',emp_email_id AS 'EmailId',emp_nationality AS 'Nationality','' AS 'Wages',emp_qualification AS 'Qualification',emp_marrital_status AS 'MaritalStatus','N' AS 'IsInternationalWorker','' AS 'CountryOfOrigin','' AS 'PassportNumber','' AS 'PassportValidFrom','' AS 'PassportValidTill','N' AS 'IsPhisicalHandicap','' AS 'Locomotive','' AS 'Hearing','' AS 'Visual',CONCAT(',', original_bank_account_no) AS 'BankAccNo',pf_ifsc_code AS 'Ifsc',bank_holder_name AS 'NameAsPerBank',EMP_NEW_PAN_NO AS 'PAN','' AS 'NameAsPerPan',CONCAT(',', p_tax_number) AS 'AdhaarNo',emp_name AS 'NameAsPerAdhaar' from pay_employee_master   where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_uan_client_list.SelectedValue + "' and unit_code='" + ddl_uan_unit_name.SelectedValue + "' and  employee_type in ('Permanent','Temporary') AND (pay_employee_master.LEFT_REASON IS NULL OR pay_employee_master.LEFT_REASON = '') order by  pan_number='' , emp_code ";
            }
            
            d.con1.Open();
            MySqlDataAdapter adp2 = new MySqlDataAdapter(query, d.con1);

            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 2;

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
            d.con1.Close();
        }
    }
    protected void btn_uan_cvcdownload_click(object sender, EventArgs e)
    {
        hidtab.Value = "8";

        d.con1.Open();
        String UnitList = "";

        //MySqlCommand cmd = new MySqlCommand();
        System.Data.DataTable dt = new System.Data.DataTable();
        MySqlDataAdapter adp;
        string query = "";
        if (ddl_uan_state_name.SelectedValue.ToString() == "ALL" && ddl_uan_unit_name.SelectedValue.ToString() == "ALL")
        {
            adp = new MySqlDataAdapter("SELECT concat('#~#',   '#~#',  '' ,'#~#',  ifnull(emp_name,''),'#~#', date_format(birth_date,'%d/%m/%Y') ,'#~#',  date_format(joining_date,'%d/%m/%Y'),'#~#',  ifnull(Gender,''),'#~#',  ifnull(emp_father_name,'') ,'#~#',  (CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END) ,'#~#',  ifnull(emp_mobile_no,'') ,'#~#',  ifnull(emp_email_id,'') ,'#~#',  ifnull(emp_nationality,'') ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(emp_marrital_status,'U') ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#','' ,'#~#',  '' ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(original_bank_account_no,'') ,'#~#',  ifnull(pf_ifsc_code,'') ,'#~#',  ifnull(bank_holder_name,'') ,'#~#',  '#~#',  '' ,'#~#',  ifnull(p_tax_number,'') ,'#~#',  ifnull(emp_name,'')) as 'rr'FROM  pay_employee_master WHERE  comp_code = '"+Session["COMP_CODE"].ToString()+"' and employee_type='Permanent' and (pan_number ='' or pan_number is null) and original_bank_account_no != ''  and (pay_employee_master.LEFT_REASON is null or pay_employee_master.LEFT_REASON = '') and client_code='" + ddl_uan_client_list.SelectedValue + "' ORDER BY  emp_code", d.con1);
           // query = "select  isnull (PAY_EMPLOYEE_MASTER.PAN_NUMBER,'') + '#~#' + '#~#'+isnull(EMP_NAME,'')+'#~#' + isnull(DATE_FORMAT(BIRTH_DATE,'%d/%m/%Y'),'')+ '#~#' + isnull(DATE_FORMAT(JOINING_DATE,'%d/%m/%Y'),'')+ '#~#' + isnull(PAY_EMPLOYEE_MASTER.GENDER,'')+ '#~#' +  isnull(EMP_FATHER_NAME,'')+ '#~#' + CASE WHEN FATHER_RELATION='Father' then 'F' else 'H' END + '#~#' + isnull(EMP_MOBILE_NO,'')+ '#~#' +  ''+ '#~#' + 'INDIAN'+ '#~#' +  ''+ '#~#' +  ''+'#~#'+Case When EMP_MARRITAL_STATUS ='Married' then 'M' When EMP_MARRITAL_STATUS = 'Single' then 'U' When EMP_MARRITAL_STATUS='Divorced' then 'D' When EMP_MARRITAL_STATUS='Widowed' then 'W' Else '' END+'#~#'+'N'+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+'N'+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+isnull(BANK_EMP_AC_CODE,'')+'#~#'+isnull(PF_IFSC_CODE,'')+'#~#'+ Case When BANK_EMP_AC_CODE != NULL then EMP_NAME When BANK_EMP_AC_CODE != '' then EMP_NAME else '' END+'#~#'+isnull(EMP_NEW_PAN_NO,'')+'#~#'+CASE When EMP_NEW_PAN_NO != NULL then EMP_NAME When EMP_NEW_PAN_NO != '' then EMP_NAME ELSE '' END+'#~#'+isnull(ADHARNO,'')+'#~#'+Case When ADHARNO != NULL then EMP_NAME When ADHARNO != '' then EMP_NAME Else '' END FROM PAY_EMPLOYEE_MASTER WHERE (PAY_EMPLOYEE_MASTER.LEFT_REASON is null or PAY_EMPLOYEE_MASTER.LEFT_REASON = '') and PAY_EMPLOYEE_MASTER.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' order by PAY_EMPLOYEE_MASTER.PAN_NUMBER";
        }
        else
        {
            //foreach (ListItem listItem in ddlunitselect.Items)
            //{
            //    if (listItem.Selected == true)
            //    {
            //        UnitList += "'" + listItem.Text.Substring(0, 4) + "',";
            //    }
            //}
            //UnitList = UnitList.Substring(0, UnitList.Length - 1);

            adp = new MySqlDataAdapter("SELECT concat('#~#',   '#~#',  '' ,'#~#',  ifnull(emp_name,''),'#~#', date_format(birth_date,'%d/%m/%Y') ,'#~#',  date_format(joining_date,'%d/%m/%Y'),'#~#',  ifnull(Gender,''),'#~#',  ifnull(emp_father_name,'') ,'#~#',  (CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END) ,'#~#',  ifnull(emp_mobile_no,'') ,'#~#',  ifnull(emp_email_id,'') ,'#~#',  ifnull(emp_nationality,'') ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(emp_marrital_status,'U') ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#','' ,'#~#',  '' ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(original_bank_account_no,'') ,'#~#',  ifnull(pf_ifsc_code,'') ,'#~#',  ifnull(bank_holder_name,'') ,'#~#',  '#~#',  '' ,'#~#',  ifnull(p_tax_number,'') ,'#~#',  ifnull(emp_name,'')) as 'rr'FROM  pay_employee_master WHERE  comp_code = '"+Session["COMP_CODE"].ToString()+"' and employee_type='Permanent' and (pan_number ='' or pan_number is null) and original_bank_account_no != ''  and (pay_employee_master.LEFT_REASON is null or pay_employee_master.LEFT_REASON = '') and client_code='" + ddl_uan_client_list.SelectedValue + "' and unit_code='" + ddl_uan_unit_name.SelectedValue + "' ORDER BY  emp_code", d.con1);
        }
        adp.Fill(dt);


        string csv = string.Empty;

        dt.DefaultView.Sort = "rr";
        dt = dt.DefaultView.ToTable();
        // dt.DefaultView.Sort= "PAY_EMPLOYEE_MASTER.PF_NUMBER";
        int a = 0;
        foreach (DataRow row in dt.Rows)
        {
            a = 0;
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                if (a == 0)
                {
                    csv += row[column.ColumnName].ToString();
                    a = 1;
                }
            }

            //Add new line.
            csv += "\r\n";
        }



        String Company_name = Session["COMP_NAME"].ToString();
        Company_name = Company_name.Replace(' ', '_');
        //Download the CSV file.
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=UAN " + Company_name + ".csv");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(csv);
        Response.Flush();
        Response.End();

    }
	 protected void btn_report_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        try{
             string chake = d.getsinglestring("Select COUNT(emp_code) AS 'emp_name' FROM pay_pro_master WHERE state_name = '" + ddl_lwf_state.SelectedValue + "' AND month = '" + txt_lwf_month.Text.Substring(0, 2) + "' AND year = '" + txt_lwf_month.Text.Substring(3) + "' AND employee_type = 'Permanent' AND pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "'");
             if (chake != "0")
             {
                 string mont = txt_lwf_month.Text.Substring(0, 2);
                 string day = "";
                 if (mont == "01") { day = "JAN"; }
                 else if (mont == "02") { day = "FEB"; }
                 else if (mont == "03") { day = "MAR"; }
                 else if (mont == "04") { day = "APR"; }
                 else if (mont == "05") { day = "MAY"; }
                 else if (mont == "06") { day = "JUN"; }
                 else if (mont == "07") { day = "JULY"; }
                 else if (mont == "08") { day = "AUG"; }
                 else if (mont == "09") { day = "SEP"; }
                 else if (mont == "10") { day = "OCT"; }
                 else if (mont == "11") { day = "NOV"; }
                 else if (mont == "12") { day = "DEC"; }

                 string month = d.getsinglestring("select catagory from pay_master_lwf where state_name='" + ddl_lwf_state.SelectedValue + "' ");
                 string[] abc = month.Split(',');

                 string stamppath = "";
                 ReportDocument crystalReport = new ReportDocument();
                 System.Data.DataTable dt = new System.Data.DataTable();
                 //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                 string query1 = "";
                 foreach (object obj in abc)
                 {
                     if (day.Equals(obj))
                     {
                         if (Session["COMP_CODE"].ToString() == "C02")
                         {

                             stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
                         }
                         else
                         {

                             stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
                         }

                         stamppath = stamppath.Replace("\\", "\\\\");

                         query1 = "SELECT field4 as comp_name,COUNT(emp_code) as 'emp_name',COMPANY_NAME as 'comp_code',Field2 AS 'sho_act_details', Field3 as 'reg_date',date_format(now(),'%d/%m/%Y') as 'reg_details',concat(month,'/',year) as 'month_year','" + stamppath + "' as 'stappath' FROM pay_pro_master LEFT JOIN pay_zone_master ON pay_pro_master.state_name = pay_zone_master.Field1 AND pay_pro_master.comp_code = pay_zone_master.comp_code and type='lwf' WHERE state_name = '" + ddl_lwf_state.SelectedValue + "' AND month = '" + txt_lwf_month.Text.Substring(0, 2) + "' AND year = '" + txt_lwf_month.Text.Substring(3) + "' AND employee_type = 'Permanent' AND pay_pro_master.comp_code = '" + Session["comp_code"].ToString() + "'";
                     }
                 }
                 if (query1 != "")
                 {
                     MySqlCommand cmd = new MySqlCommand(query1, d.con);
                     MySqlDataReader sda = null;
                     d.con.Open();
                     try
                     {
                         sda = cmd.ExecuteReader();
                         dt.Load(sda);


                     }
                     catch (Exception ex) { throw ex; }
                     crystalReport.Load(Server.MapPath("~/lwf_letter.rpt"));
                     crystalReport.SetDataSource(dt);
                     crystalReport.Refresh();
                     crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, "TaxInvoice");
                 }
                 else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select " + month + " Thise Month !!');", true); }
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found');", true); 
             }
    }

    catch (Exception ee)
    {
        throw ee;
    }
    finally
    {
        d.con.Close();
    }
    }

     protected void btn_lwf_excel_Click(object sender, EventArgs e)
     {
         try
         {
             hidtab.Value = "3";
             string sql = "";
             int i = 0;
             int count = 0;
             string mont = txt_lwf_month.Text.Substring(0, 2);
             string day = "";
             if (mont == "01") { day = "JAN"; }
             else if (mont == "02") { day = "FEB"; }
             else if (mont == "03") { day = "MAR"; }
             else if (mont == "04") { day = "APR"; }
             else if (mont == "05") { day = "MAY"; }
             else if (mont == "06") { day = "JUN"; }
             else if (mont == "07") { day = "JULY"; }
             else if (mont == "08") { day = "AUG"; }
             else if (mont == "09") { day = "SEP"; }
             else if (mont == "10") { day = "OCT"; }
             else if (mont == "11") { day = "NOV"; }
             else if (mont == "12") { day = "DEC"; }

             string month = d.getsinglestring("select catagory from pay_master_lwf where state_name='" + ddl_lwf_state.SelectedValue + "' ");
             string[] abc = month.Split(',');

            // string stamppath = "";
             ReportDocument crystalReport = new ReportDocument();
             System.Data.DataTable dt = new System.Data.DataTable();
             foreach (object obj in abc)
             {
                 count++;
             }
            // string query1 = "";
             foreach (object obj in abc)
             {
                 if (day.Equals(obj))
                 {
                     
                     int month_count = 12 / count;
                     sql = "SELECT  pay_pro_master . state_name ,COUNT( pay_pro_master . emp_code ) AS 'emp_name', per_month  AS 'employer', employee_contribution  AS 'employee',(( per_month  * " + month_count + ") * COUNT( pay_pro_master . emp_code )) AS 'employer1',(( employee_contribution  * " + month_count + ") * COUNT( pay_pro_master . emp_code )) AS 'employee1',(( per_month  * " + month_count + ") * COUNT( pay_pro_master . emp_code )) + (( employee_contribution  * " + month_count + ") * COUNT( pay_pro_master . emp_code )) AS 'total' FROM   pay_pro_master    INNER JOIN  pay_master_lwf  ON  pay_pro_master . state_name  =  pay_master_lwf . state_name  WHERE  pay_pro_master . state_name  = '" + ddl_lwf_state.SelectedValue + "' AND  pay_pro_master . month  = '" + txt_lwf_month.Text.Substring(0, 2) + "' AND  pay_pro_master . year  = '" + txt_lwf_month.Text.Substring(3) + "' AND  employee_type  = 'Permanent' AND  pay_pro_master . comp_code  = '" + Session["comp_code"].ToString() + "'";
                 }
             }


              if(sql!="")
              {
             
             MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
             DataSet ds = new DataSet();
             dscmd.Fill(ds);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 Response.Clear();
                 Response.Buffer = true;
                 Response.AddHeader("content-disposition", "attachment;filename=LWF" + ddl_lwf_state.SelectedItem.Text.Replace(" ", "_") + ".xls");

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
             }
             else
             {
                
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select " + month + " Thise Month !!');", true);
             }
         }
         catch (Exception ex) { throw ex; }
         finally
         {
             d.con.Close();
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
             this.i = i;
             ctr = 0;
             //paid_days = 0;
             //rate = 0;
         }

         public void InstantiateIn(Control container)
         {


             switch (type)
             {
                 case ListItemType.Header:

                     lc = new LiteralControl("<table border=1><tr ><th colspan=8>LWF Reports</th></tr><tr><th>SR NO.</th><th>STATE NAME</th><th>EMPLOYER Rate</th> <th>EMPLOYEE Rate</th><th>EMPLOYEE Count</th><th>EMPLOYER</th><th>EMPLOYEE</th><th>Total</th></tr> ");

                     
                     break;
                 case ListItemType.Item:

                     lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["employer"] + "</td><td>" + ds.Tables[0].Rows[ctr]["employee"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["employer1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["employee1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["total"] + "</td></tr>");

                     
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

     protected void btn_pf_sheet_Click(object sender, EventArgs e)
     {
         hidtab.Value = "9";
         string where = where_clause(ddl_clientname_pf_esic.SelectedValue, "ALL", "ALL", txt_month_pf_esic.Text, 0);
         string monthname = month_date(txt_month_pf_esic.Text);
         generate_slab_report(7, where.Replace("order by", "  group by pay_employee_master.emp_code order by"), monthname);

     }
     protected void btn_esic_sheet_Click(object sender, EventArgs e)
     {
         hidtab.Value = "9";
         string where = where_clause(ddl_clientname_pf_esic.SelectedValue, "ALL", "ALL", txt_month_pf_esic.Text, 1);
         string monthname = month_date(txt_month_pf_esic.Text);
         generate_slab_report(6, where.Replace("order by", "  group by pay_employee_master.emp_code order by"), monthname);
     }
     protected void ddl_type_pf_esic_SelectedIndexChanged(object sender, EventArgs e)
     {

         if (ddl_type_pf_esic.SelectedValue=="PF") {
             hidtab.Value = "9";
             btn_pf_sheet.Visible = true;
             btn_esic_sheet.Visible = false;
         }
         else if (ddl_type_pf_esic.SelectedValue == "ESIC") {
             btn_esic_sheet.Visible = true;
             btn_pf_sheet.Visible = false;
         }

     }
protected void btn_bonus_report_Click(object sender, EventArgs e)
    {
        hidtab.Value = "9";
        export_xl(1);
    }
    private void export_xl(int i)
    {
        try
        {
            string From_month = "";
            string To_month = "";
            string query = "";
            if (ddl_type.SelectedValue.Equals("Bonus"))
            {
                query = "SELECT pay_billing_unit_rate_history.emp_code, pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.tot_days_Present, ROUND(sum(pay_billing_unit_rate_history.emp_basic_vda), 2) AS 'emp_basic_vda', ROUND(IF(IF(sum(bonus_gross) > 0, IF(sum(sal_bonus_gross) > 0, 0, bonus_gross), 0) > 0, IF(sum(bonus_gross) > 0, IF(sum(sal_bonus_gross) > 0, 0, sum(bonus_gross)), 0), IF(bonus_after_gross > 0, IF(sal_bonus_after_gross > 0, 0, bonus_after_gross), 0)), 0) AS 'BONUS', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, pay_billing_unit_rate_history.month_days, ROUND((sum(bonus_gross) + sum(bonus_after_gross)), 2) AS 'bns', IF(pay_employee_master.left_date IS NULL, pay_billing_unit_rate_history.emp_type, 'LEFT') AS 'status' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year INNER JOIN pay_employee_master ON pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code WHERE pay_billing_unit_rate_history.client_code = '" + ddl_bclient.SelectedValue + "' and pay_billing_unit_rate_history.invoice_flag in (1,2) ";
            }
            else if (ddl_type.SelectedValue.Equals("Leave"))
            {
                query = "SELECT pay_billing_unit_rate_history.emp_code, pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.tot_days_Present, ROUND(sum(pay_billing_unit_rate_history.emp_basic_vda), 2) AS 'emp_basic_vda', ROUND(IF(IF(sum(leave_gross) > 0, IF(sum(leave_sal_gross) > 0, 0, sum(leave_gross)), 0) > 0, IF(sum(leave_gross) > 0, IF(sum(leave_sal_gross) > 0, 0, sum(leave_sal_gross)), 0), IF(sum(leave_after_gross) > 0, IF(sum(leave_sal_after_gross) > 0, 0, sum(leave_after_gross)), 0)), 2) AS 'BONUS', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, pay_billing_unit_rate_history.month_days, ROUND((sum(leave_gross) + sum(leave_after_gross)), 2) AS 'bns', IF(pay_employee_master.left_date IS NULL, pay_billing_unit_rate_history.emp_type, 'LEFT') AS 'status' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year inner join pay_employee_master on pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code WHERE pay_billing_unit_rate_history.client_code = '" + ddl_bclient.SelectedValue + "' and pay_billing_unit_rate_history.invoice_flag in (1,2) ";
            }
            else if (ddl_type.SelectedValue.Equals("Gratuity"))
            {
                query = "SELECT pay_billing_unit_rate_history.emp_code, pay_billing_unit_rate_history.client, pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_city, pay_billing_unit_rate_history.emp_name, pay_billing_unit_rate_history.grade_desc, pay_billing_unit_rate_history.tot_days_Present, ROUND(sum(pay_billing_unit_rate_history.emp_basic_vda), 2) AS 'emp_basic_vda', ROUND(IF(IF(sum(pay_billing_unit_rate_history.gratuity_gross) > 0, IF(sum(pay_pro_master.gratuity_gross) > 0, 0, sum(pay_billing_unit_rate_history.gratuity_gross)), 0) > 0, IF (sum(pay_billing_unit_rate_history.gratuity_gross) > 0, IF (sum(pay_pro_master.gratuity_gross) > 0, 0, sum(pay_pro_master.gratuity_gross)), 0), IF(sum(pay_billing_unit_rate_history.gratuity_after_gross) > 0, IF(sum(pay_pro_master.gratuity_after_gross) > 0, 0, sum(pay_billing_unit_rate_history.gratuity_after_gross)), 0)), 2) AS 'BONUS', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, pay_billing_unit_rate_history.month_days, ROUND(sum(pay_billing_unit_rate_history.gratuity_gross) + sum(pay_billing_unit_rate_history.gratuity_after_gross), 2) AS 'bns', IF(pay_employee_master.left_date IS NULL, pay_billing_unit_rate_history.emp_type, 'LEFT') AS 'status' FROM pay_billing_unit_rate_history INNER JOIN pay_pro_master ON pay_billing_unit_rate_history.emp_code = pay_pro_master.emp_code AND pay_billing_unit_rate_history.month = pay_pro_master.month AND pay_billing_unit_rate_history.year = pay_pro_master.year inner join pay_employee_master on pay_billing_unit_rate_history.emp_code = pay_employee_master.emp_code WHERE pay_billing_unit_rate_history.client_code = '" + ddl_bclient.SelectedValue + "'  and pay_billing_unit_rate_history.invoice_flag in (1,2) ";
            }
            string final_query = "", html2 = "", html_top1 = "<th colspan=9>W.E.F " + get_month(int.Parse(txt_work_img_from.Text.Substring(0, 2))).ToUpper() + "-" + txt_work_img_from.Text.Substring(3) + " TO " + get_month(int.Parse(txt_work_img_to.Text.Substring(0, 2))).ToUpper() + "-" + txt_work_img_to.Text.Substring(3) + "</th>";
            string temp_html = "<th>Worked Days in a month</th><th>Earned Basic+DA</th><th>Basic + DA for 26 days</th><th>Monthly "+ddl_type.SelectedValue+" according to New amendment : (Basic+DA)/26</th>";

            if (txt_work_img_from.Text.Substring(3) != txt_work_img_to.Text.Substring(3))
            {
                int month = int.Parse(txt_work_img_from.Text.Substring(0, 2));
                int month1 = int.Parse(txt_work_img_to.Text.Substring(0, 2));
                for (int j = month; j <= 12; j++)
                {
                    From_month = From_month + j + ",";
                    html_top1 = html_top1 + "<th colspan=4>" + get_month(j) + " - " + txt_work_img_from.Text.Substring(3);
                    html2 = html2 + temp_html;
                }
                From_month = From_month.Substring(0, From_month.Length - 1);
                for (int j = 1; j <= month1; j++)
                {
                    To_month = To_month + j + ",";
                    html_top1 = html_top1 + "<th colspan=4>" + get_month(j) + " - " + txt_work_img_to.Text.Substring(3);
                    html2 = html2 + temp_html;
                }
                To_month = To_month.Substring(0, To_month.Length - 1);
                final_query = " union " + query + " and pay_billing_unit_rate_history.month IN (" + To_month + ") and pay_billing_unit_rate_history.year='" + txt_work_img_to.Text.Substring(3) + "' group by pay_billing_unit_rate_history.emp_code, pay_billing_unit_rate_history.month,pay_billing_unit_rate_history.year";
            }
            else
            {
                int month = int.Parse(txt_work_img_from.Text.Substring(0, 2));
                int month1 = int.Parse(txt_work_img_to.Text.Substring(0, 2));
                for (int j = month; j <= month1; j++)
                {
                    From_month = From_month + j + ",";
                    html_top1 = html_top1 + "<th colspan=4>" + get_month(j) + " - " + txt_work_img_from.Text.Substring(3);
                    html2 = html2 + temp_html;
                }
                From_month = From_month.Substring(0, From_month.Length - 1);
            }

            d.con.Open();
            if (i == 1)
            {
                query = "select * from (" + query + " AND pay_billing_unit_rate_history.month IN (" + From_month + ") and pay_billing_unit_rate_history.year='" + txt_work_img_from.Text.Substring(3) + "' group by pay_billing_unit_rate_history.emp_code, pay_billing_unit_rate_history.month,pay_billing_unit_rate_history.year " + final_query + ") as a order by emp_code, year, month";
            }

            string html_header = "<table border=1><tr>" + html_top1 + "<th colspan=5>Summary of " + ddl_type.SelectedValue + " Return Details</th></tr><tr><th>Sr No</th><th>State</th><th>Location</th><th>Name of Employee</th><th>Names and address of Contractor</th><th>Contract Period From</th><th>Contract Period To</th><th>Nature of Work</th></th><th>Employee Type</th>" + html2 + "<th>TOTAL NO. OF DAYS IN A YEAR</th><th>Total Yearly Earned Basic + DA</th><th>Total Annual " + ddl_type.SelectedValue + "</th><th>Actual " + ddl_type.SelectedValue + " to be paid to employees</th><th>Current Status</th></tr>";

            MySqlCommand cmd = new MySqlCommand(query, d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
                d.con.Close();
                return;
            }
            int ctr = 0, month2 = 0, year1 = 0;
            string data = "", Comment1 = "" + ddl_type.SelectedValue + " distribution Monthly";
            double total_days = 0, earn_basic_da = 0, total_basic_da = 0, actual_bonus = 0;
            string left = "";
            string emp_code = "";
            while (dr.Read())
            {
                //check for new employee
                if (!emp_code.Equals(dr.GetValue(0).ToString()))
                {
                    if (!month2.ToString().Equals("0"))
                    {
                        data = data + funcname((month2.ToString().Length == 1 ? "0" + month2.ToString() : month2.ToString()) + "/" + year1, (txt_work_img_to.Text.Substring(0, 2).Length == 1 ? "0" + txt_work_img_to.Text.Substring(0, 2) : txt_work_img_to.Text.Substring(0, 2)) + "/" + txt_work_img_to.Text.Substring(3));
                    }
                    total_basic_da = actual_bonus;
                    if (ddl_type.SelectedValue.Equals("Bonus"))
                    {
                        if (total_days < 30)
                        { actual_bonus = 0; }   
                    }
                    else if (ddl_type.SelectedValue.Equals("Leave"))
                    {
                        if (total_days < 240)
                        { actual_bonus = 0; }
                    }
                    left = "<td>" + dr.GetValue(14).ToString() + "</td>";
                    data = data + (emp_code.Equals("") ? "" : "<td>" + total_days + "</td><td>" + Math.Round(earn_basic_da,2) + "</td><td>" + Math.Round(total_basic_da,2) + "</td><td>" + Math.Round(actual_bonus,2) + "</td><td>" + Comment1 + "</td></tr>");
                    data = data + "<tr><td>" + ++ctr + "</td><td>" + dr.GetValue(3).ToString() + "</td><td>" + dr.GetValue(4).ToString() + "</td><td>" + dr.GetValue(5).ToString() + "</td><td>IH&MS</td><td>" + get_month(int.Parse(txt_work_img_from.Text.Substring(0, 2))) + "-" + txt_work_img_from.Text.Substring(3) + "</td><td>" + get_month(int.Parse(txt_work_img_to.Text.Substring(0, 2))) + "-" + txt_work_img_to.Text.Substring(3) + "</td><td>" + dr.GetValue(6).ToString() + "</td>" + left;
                    total_days = 0;
                    total_basic_da = 0;
                    earn_basic_da = 0;
                    actual_bonus = 0;
                    data = data + funcname((txt_work_img_from.Text.Substring(0, 2).Length == 1 ? "0" + txt_work_img_from.Text.Substring(0, 2) : txt_work_img_from.Text.Substring(0, 2)) + "/" + txt_work_img_from.Text.Substring(3), (dr.GetValue(10).ToString().Length == 1 ? "0" + dr.GetValue(10).ToString() : dr.GetValue(10).ToString()) + "/" + dr.GetValue(11).ToString());
                }
                else
                {
                    if(!left.Equals(dr.GetValue(14).ToString()))
                    {
                        data.Replace(left, "<td>" + dr.GetValue(14).ToString() + "</td>");
                    }
                    month2 = month2 + 1;
                    if (month2 == 13) { month2 = 1; year1 = year1 + 1; }
                        data = data + funcname((month2.ToString().Length == 1 ? "0" + month2.ToString() : month2.ToString()) + "/" + year1, (dr.GetValue(10).ToString().Length == 1 ? "0" + dr.GetValue(10).ToString() : dr.GetValue(10).ToString()) + "/" + dr.GetValue(11).ToString());
                }
               
                emp_code = dr.GetValue(0).ToString();
                month2 = int.Parse(dr.GetValue(10).ToString());
                year1 = int.Parse(dr.GetValue(11).ToString());
                //check if monthly or yearly
                if (double.Parse(dr.GetValue(9).ToString()) > 0) { Comment1 = "" + ddl_type.SelectedValue + " distribution Yearly"; }


                //Put data as per Bonus, leave, Gratuity format
                if (txt_work_img_from.Text.Substring(3) != txt_work_img_to.Text.Substring(3))
                {
                    int month = int.Parse(txt_work_img_from.Text.Substring(0, 2));
                    int month1 = int.Parse(txt_work_img_to.Text.Substring(0, 2));
                    for (int j = month; j <= 12; j++)
                    {
                        if (j == int.Parse(dr.GetValue(10).ToString()) && txt_work_img_from.Text.Substring(3) == dr.GetValue(11).ToString())
                        {
                            data = data + "<td>" + dr.GetValue(7).ToString() + "</td><td>" + dr.GetValue(8).ToString() + "</td><td>" + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString())) + "</td><td>" + dr.GetValue(13).ToString() + "</td>";
                            total_days = total_days + double.Parse(dr.GetValue(7).ToString());
                            //total_basic_da = total_basic_da + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString()));
                            earn_basic_da = earn_basic_da + double.Parse(dr.GetValue(8).ToString());
                            actual_bonus = actual_bonus + double.Parse(dr.GetValue(13).ToString());
                        }
                        
                    }
                    for (int j = month1; j > 0; j--)
                    {
                        if (j == int.Parse(dr.GetValue(10).ToString()) && txt_work_img_to.Text.Substring(3) == dr.GetValue(11).ToString())
                        {
                            data = data + "<td>" + dr.GetValue(7).ToString() + "</td><td>" + dr.GetValue(8).ToString() + "</td><td>" + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString())) + "</td><td>" + dr.GetValue(13).ToString() + "</td>";
                            total_days = total_days + double.Parse(dr.GetValue(7).ToString());
                           // total_basic_da = total_basic_da + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString()));
                            earn_basic_da = earn_basic_da + double.Parse(dr.GetValue(8).ToString());
                            actual_bonus = actual_bonus + double.Parse(dr.GetValue(13).ToString());
                        }
                    }
                }
                else
                {
                    int month = int.Parse(txt_work_img_from.Text.Substring(0, 2));
                    int month1 = int.Parse(txt_work_img_to.Text.Substring(0, 2));
                    for (int j = month; j <= month1; j++)
                    {
                        if (j == int.Parse(dr.GetValue(10).ToString()) && txt_work_img_from.Text.Substring(3) == dr.GetValue(11).ToString())
                        {
                            data = data + "<td>" + dr.GetValue(7).ToString() + "</td><td>" + dr.GetValue(8).ToString() + "</td><td>" + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString())) + "</td><td>" + dr.GetValue(13).ToString() + "</td>";
                            total_days = total_days + double.Parse(dr.GetValue(7).ToString());
                            //total_basic_da = total_basic_da + ((double.Parse(dr.GetValue(8).ToString()) * double.Parse(dr.GetValue(12).ToString())) / double.Parse(dr.GetValue(7).ToString()));
                            earn_basic_da = earn_basic_da + double.Parse(dr.GetValue(8).ToString());
                            actual_bonus = actual_bonus + double.Parse(dr.GetValue(13).ToString());
                        }
                    }
                }
            }
            data = data + funcname((month2.ToString().Length == 1 ? "0" + month2.ToString() : month2.ToString()) + "/" + year1, (txt_work_img_to.Text.Substring(0, 2).Length == 1 ? "0" + txt_work_img_to.Text.Substring(0, 2) : txt_work_img_to.Text.Substring(0, 2)) + "/" + txt_work_img_to.Text.Substring(3));
            total_basic_da = actual_bonus;
            if (ddl_type.SelectedValue.Equals("Bonus"))
            {
                if (total_days < 30)
                { actual_bonus = 0; }
            }
            else if (ddl_type.SelectedValue.Equals("Leave"))
            {
                if (total_days < 240)
                { actual_bonus = 0; }
            }
            data = data + "<td>" + total_days + "</td><td>" + Math.Round(earn_basic_da, 2) + "</td><td>" + Math.Round(total_basic_da, 2) + "</td><td>" + Math.Round(actual_bonus, 2) + "</td><td>" + Comment1 + "</td></tr></table>";

            html_header = html_header + data;
            Response.Clear();
            Response.Buffer = true;
            if (i == 1)
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + ddl_type.SelectedValue + "_REPORT_"+ddl_bclient.SelectedItem.Text.Replace(" ","_")+".xls");
            }
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            //Repeater Repeater1 = new Repeater();
            //Repeater1.DataSource = dt;
            ////Repeater1.HeaderTemplate = new MyTemplate12(ListItemType.Header, ds, i);
            ////Repeater1.ItemTemplate = new MyTemplate12(ListItemType.Item, ds, i);
            ////Repeater1.FooterTemplate = new MyTemplate12(ListItemType.Footer, ds, i);
            //Repeater1.DataBind();

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //Repeater1.RenderControl(htmlWrite);

            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            //Response.Output.Write(stringWrite.ToString());
            Response.Output.Write(html_header.ToString());
            Response.Flush();
            Response.End();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    public class MyTemplate12 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        System.Data.DataSet dt;
        static int ctr;
        static int ctr1;
        int i;
        string emp_type;
        int i3 = 1;
        private ListItemType listItemType;

        public MyTemplate12(ListItemType type, System.Data.DataSet dt, int i)
        {
            // TODO: Complete member initialization
            this.type = type;
            this.dt = dt;
            this.i = i;

        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:

                    if (i == 1)
                    {
                        lc = new LiteralControl("<table border=1><tr><th bgcolor=yellow colspan=22 align=center> BONUS REPORT </th></tr><table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>CLIENT CODE</th><th>STATE NAME</th><th>UNIT CITY</th><th>EMP NAME</th><th>grade desc</th><th>tot days Present</th><th>EMP BASIC VDA</th><th>BONUS</th><th>MONTH</th><th>YEAR</th></tr>");
                    }

                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + dt.Tables[0].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[1].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[2].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[3].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[4].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[5].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[6].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[7].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[8].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[9].Rows[ctr].ToString().ToUpper() + "</td><td>" + dt.Tables[10].Rows[ctr].ToString().ToUpper() + "</td></tr>");
                       
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
    private string getmonthname(int month)
    {
        if (month == 1)
        { return "January"; }
        else if (month == 2)
        { return "February"; }
        else if (month == 3)
        { return "March"; }
        else if (month == 4)
        { return "April"; }
        else if (month == 5)
        { return "May"; }
        else if (month == 6)
        { return "June"; }
        else if (month == 7)
        { return "July"; }
        else if (month == 8)
        { return "August"; }
        else if (month == 9)
        { return "September"; }
        else if (month == 10)
        { return "October"; }
        else if (month == 11)
        { return "November"; }
        else if (month == 12)
        { return "December"; }

        return "";

    }
    private string funcname(string from, string to)
    {
        string data = "";
        int i = int.Parse(from.Substring(0, 2));
        int j = int.Parse(to.Substring(0, 2));
        if (!from.Substring(3).Equals(to.Substring(3)))
        {
            j = 12 - i + j;
        }
        else
        {
            j = j - i;
        }
        int d = 0;
        for (int k = 1; k <= j; k++)
        {
            if (i < 13)
            {
                data = data + "<td>0</td><td>0</td><td>0</td><td>0</td>";
            }
            else
            {
                ++d;
                data = data + "<td>0</td><td>0</td><td>0</td><td>0</td>";
            }
            ++i;
        }
        return data ;
    }

    protected void Report_Click(object sender, EventArgs e)
    {
        try
        {
            string sql="";
            d.con1.Open();
            MySqlDataAdapter adp;
            string status = "" + ddl_report.SelectedValue + "";
            string PF_NO = "";
            PF_NO = d.getsinglestring("SELECT group_concat(UAN) FROM pay_emp_compliance WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = '" + txt_pf_date.Text.Substring(0, 2) + "' AND year = '" + txt_pf_date.Text.Substring(3) + "' AND client_code = '" + ddl_pf_client.SelectedValue + "'");
            PF_NO = "'" + PF_NO + "'";
            PF_NO = PF_NO.Replace(",", "','");

            string txt_date = "" + txt_pf_date .Text+ "";
            string db_date = d.getsinglestring("select concat(month,'/',year) from pay_emp_compliance where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + ddl_pf_client.SelectedValue + "'");

            if (ddl_report.SelectedValue == "1")
            {
                sql = "SELECT comp_code, client_code as 'client_name',emp_name,UAN,EPF_CR,EPS_CR,ER,CASE WHEN flag_PF = 1 THEN 'Paid' when flag_PF=0 then 'Unpaid' END AS 'Status' FROM pay_emp_compliance where client_code='" + ddl_pf_client.SelectedValue + "' and month='" + txt_pf_date.Text.Substring(0, 2) + "' and year='" + txt_pf_date.Text.Substring(3) + "' AND flag_pf = '1'";
            }
            else if (ddl_report.SelectedValue == "0")
            {
                sql = "SELECT DISTINCT client_name,UAN,emp_name,EPF_CR,EPS_CR,(EPF_CR - EPS_CR) AS 'ER',Status FROM (SELECT ( pay_pro_master . client ) AS 'client_name',  pay_pro_master . state_name , pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda', ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',   IF(`pf_status` = '1', 'Paid', 'Unpaid') AS 'Status' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation LEFT JOIN `pay_emp_compliance` ON `pay_emp_compliance`.`comp_code` = `pay_pro_master`.`comp_code` AND `pay_emp_compliance`.`UAN` = `pay_pro_master`.`PAN_No` WHERE   `pay_pro_master`.`month` = '" + txt_pf_date.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_pf_date.Text.Substring(3) + "' AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`client_code` = '" + ddl_pf_client.SelectedValue + "'  AND `pay_pro_master`.`pf_status` IS  NULL  AND (`PAN_No` IS NOT NULL AND `PAN_No` != '')  ORDER BY `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_name`) AS a";
            }
            else if (ddl_report.SelectedValue == "ALL")
            {
                if (txt_date == db_date)
                {
                    sql = "SELECT DISTINCT client_name,UAN,emp_name,EPF_CR,EPS_CR,(EPF_CR - EPS_CR) AS 'ER',Status FROM (SELECT ( pay_pro_master . client ) AS 'client_name',  pay_pro_master . state_name , pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda', ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',   IF(`pf_status` = '1', 'Paid', 'Unpaid') AS 'Status' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation LEFT JOIN `pay_emp_compliance` ON `pay_emp_compliance`.`comp_code` = `pay_pro_master`.`comp_code` AND `pay_emp_compliance`.`UAN` = `pay_pro_master`.`PAN_No` WHERE  `pay_pro_master`.`month` = '" + txt_pf_date.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_pf_date.Text.Substring(3) + "' AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`client_code` = '" + ddl_pf_client.SelectedValue + "'  AND (`PAN_No` IS NOT NULL AND `PAN_No` != '')  ORDER BY `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_name`) AS a";
                }
                else
                {
                    sql = "SELECT DISTINCT client_name,UAN,emp_name,EPF_CR,EPS_CR,(EPF_CR - EPS_CR) AS 'ER',Status FROM (SELECT ( pay_pro_master . client ) AS 'client_name',  pay_pro_master . state_name , pay_pro_master . PAN_No  AS 'UAN',  pay_pro_master . emp_name , ROUND( pay_pro_master . actual_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow ) AS 'actual_basic_vda', ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) AS 'emp_basic_vda', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, 15000, ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) AS 'EPS_WAGES', IF((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary   +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END)) > 15000, ROUND((15000 * 8.33) / 100), ROUND(((ROUND(CASE WHEN  pf_cmn_on  = 0 THEN  pay_pro_master . emp_basic_vda  WHEN  pf_cmn_on  = 1 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . hra_amount_salary  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 2 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . sal_bonus_gross  +  pay_pro_master . leave_sal_gross  +  pay_pro_master . washing_salary  +  pay_pro_master . travelling_salary  +  pay_pro_master . education_salary  +  pay_pro_master . other_allow  +  pay_pro_master . cca_salary  +  pay_pro_master . gratuity_gross  +  pay_pro_master . sal_ot  WHEN  pf_cmn_on  = 3 THEN  pay_pro_master . emp_basic_vda  +  pay_pro_master . cca_salary  +  pay_pro_master . other_allow  END) * 8.33) / 100))) AS 'EPS_CR', ROUND( pay_pro_master . sal_pf ) AS 'EPF_CR',   IF(`pf_status` = '', 'Paid', 'Unpaid') AS 'Status' FROM pay_pro_master INNER JOIN pay_billing_unit_rate_history ON pay_pro_master.month = pay_billing_unit_rate_history.month AND pay_pro_master.year = pay_billing_unit_rate_history.year AND pay_pro_master.comp_code = pay_billing_unit_rate_history.comp_code AND pay_pro_master.state_name = pay_billing_unit_rate_history.state_name AND pay_pro_master.emp_code = pay_billing_unit_rate_history.emp_code AND IF(pay_billing_unit_rate_history.client_code = 'RCPL', invoice_no IS NULL, invoice_no IS NOT NULL) INNER JOIN pay_salary_unit_rate ON pay_pro_master.comp_code = pay_salary_unit_rate.comp_code AND pay_billing_unit_rate_history.unit_code = pay_salary_unit_rate.unit_code AND pay_billing_unit_rate_history.month = pay_salary_unit_rate.month AND pay_billing_unit_rate_history.year = pay_salary_unit_rate.year AND pay_billing_unit_rate_history.grade_code = pay_salary_unit_rate.designation LEFT JOIN `pay_emp_compliance` ON `pay_emp_compliance`.`comp_code` = `pay_pro_master`.`comp_code` AND `pay_emp_compliance`.`UAN` = `pay_pro_master`.`PAN_No` WHERE  `pay_pro_master`.`month` = '" + txt_pf_date.Text.Substring(0, 2) + "' AND `pay_pro_master`.`year` = '" + txt_pf_date.Text.Substring(3) + "' AND `pay_pro_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_pro_master`.`client_code` = '" + ddl_pf_client.SelectedValue + "'  AND (`PAN_No` IS NOT NULL AND `PAN_No` != '')  ORDER BY `pay_pro_master`.`state_name`, `pay_pro_master`.`unit_name`, `pay_pro_master`.`emp_name`) AS a";
                }
            }
            MySqlDataAdapter adp1 = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            adp1.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                
                Response.AddHeader("content-disposition", "attachment;filename=PF_REPORT" + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds, status);
                Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds, status);
                Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, null, status);
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
    public class MyTemplate2 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string status;
        int i = 0;
        double epf_cr = 0, eps_cr = 0, diff_cr = 0;
        static int ctr;
        public MyTemplate2(ListItemType type, DataSet ds, string status)
        {
            this.type = type;
            this.ds = ds;
            this.status = status;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    if (status != "ALL")
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=8 align=center> PF PAID/UNPAID PAYMENT REPORT </th><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>EMP NAME</th><th>UAN</th><th>EPF</th><th>EPS</th><th>ER</th><th>STATUS</th></tr>");
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=8 align=center> PF PAYMENT REPORT </th><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>EMP NAME</th><th>UAN</th><th>EPF</th><th>EPS</th><th>ER</th><th>STATUS</th></tr>");

                    }
                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["UAN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EPF_CR"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EPS_CR"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Status"] + "</td></tr>");
                        epf_cr = epf_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString());
                        eps_cr = eps_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString());
                        diff_cr = diff_cr + double.Parse(ds.Tables[0].Rows[ctr]["ER"].ToString());
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td></b><td>" + Math.Round((epf_cr), 2) + "</td><td>" + Math.Round((eps_cr), 2) + "</td><td>" + diff_cr + "</td></tr>";
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

    protected void btn_report_esic_Click(object sender, EventArgs e)
    {
       

        try
        {
            d.con1.Open();
           // MySqlDataAdapter adp5=null;
            string status = "" + ddl_report_esic.SelectedValue + "";
            string ESIC_NO = "";
            string sql = "";
            ESIC_NO = d.getsinglestring("SELECT group_concat(ESIC_NO) FROM pay_emp_compliance WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and month = '" + txt_esic_date.Text.Substring(0, 2) + "' AND year = '" + txt_esic_date.Text.Substring(3) + "' AND state_name = '" + ddl_esic_state.SelectedValue + "'");
            ESIC_NO = "'" + ESIC_NO + "'";
            ESIC_NO = ESIC_NO.Replace(",", "','");

            string txt_date = "" + txt_esic_date.Text + "";
            string db_date = d.getsinglestring("select concat(month,'/',year) from pay_emp_compliance where comp_code='C01' and state_name='Goa'");

            //For paid ESIC
            if (ddl_report_esic.SelectedValue == "1")
            {
                sql = "SELECT DISTINCT comp_code,state_name,emp_name,ESIC_NO,esic_salary,sal_esic, CASE WHEN `Flag_ESIC` = 1 THEN 'Paid' when `Flag_ESIC`=0 then 'Unpaid' END AS 'Status' FROM pay_emp_compliance where state_name='" + ddl_esic_state.SelectedValue + "' and month='" + txt_esic_date.Text.Substring(0, 2) + "' and year='" + txt_esic_date.Text.Substring(3) + "' AND Flag_ESIC = '1'";
            }
            else if (ddl_report_esic.SelectedValue == "0")
            {
                sql = "SELECT DISTINCT pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, pay_pro_master.state_name, ROUND((`pay_pro_master`.`gross`), 2) AS 'esic_salary', CAST(CEILING(((`pay_pro_master`.`gross` * `sal_esic_percent`) / 100)) AS decimal(16,2)) AS 'sal_esic', IF(`esic_status` = '', 'Paid', 'Unpaid') AS 'status' FROM pay_pro_master LEFT JOIN pay_emp_compliance ON pay_emp_compliance.comp_code = pay_pro_master.comp_code AND pay_emp_compliance.ESIC_NO = pay_pro_master.ESI_NO WHERE  pay_pro_master.state_name = '" + ddl_esic_state.SelectedValue + "' AND pay_pro_master.month = '" + txt_esic_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_esic_date.Text.Substring(3) + "' AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent') AND (ESI_No IS NOT NULL AND ESI_No != '') AND pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'   AND `pay_pro_master`.`esic_status` IS NULL ORDER BY  pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.emp_name ";
            }
            // for ALL
            else if (ddl_report_esic.SelectedValue == "ALL")
            {
                if (txt_date == db_date)
                {
                    sql = "SELECT DISTINCT pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, pay_pro_master.state_name, ROUND((`pay_pro_master`.`gross`), 2) AS 'esic_salary', CAST(CEILING(((`pay_pro_master`.`gross` * `sal_esic_percent`) / 100)) AS decimal(16,2)) AS 'sal_esic', IF(`esic_status` = '1', 'Paid', 'Unpaid') AS 'status' FROM pay_pro_master LEFT JOIN pay_emp_compliance ON pay_emp_compliance.comp_code = pay_pro_master.comp_code AND pay_emp_compliance.ESIC_NO = pay_pro_master.ESI_NO WHERE  pay_pro_master.state_name = '" + ddl_esic_state.SelectedValue + "' AND pay_pro_master.month = '" + txt_esic_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_esic_date.Text.Substring(3) + "' AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent') AND (ESI_No IS NOT NULL AND ESI_No != '') AND pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.emp_name ";

                }
                else
                {
                    sql = "SELECT DISTINCT pay_pro_master.ESI_No AS 'ESIC_NO', pay_pro_master.emp_name, pay_pro_master.state_name, ROUND((`pay_pro_master`.`gross`), 2) AS 'esic_salary', CAST(CEILING(((`pay_pro_master`.`gross` * `sal_esic_percent`) / 100)) AS decimal(16,2)) AS 'sal_esic', IF(`esic_status` = '', 'Paid', 'Unpaid') AS 'status' FROM pay_pro_master LEFT JOIN pay_emp_compliance ON pay_emp_compliance.comp_code = pay_pro_master.comp_code AND pay_emp_compliance.ESIC_NO = pay_pro_master.ESI_NO WHERE  pay_pro_master.state_name = '" + ddl_esic_state.SelectedValue + "' AND pay_pro_master.month = '" + txt_esic_date.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txt_esic_date.Text.Substring(3) + "' AND (pay_pro_master.Employee_type = 'Temporary' OR pay_pro_master.Employee_type = 'Permanent') AND (ESI_No IS NOT NULL AND ESI_No != '') AND pay_pro_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' ORDER BY  pay_pro_master.state_name, pay_pro_master.unit_name, pay_pro_master.emp_name ";
                }
            }

            MySqlDataAdapter adp5 = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            adp5.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=ESIC_REPORT" + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate3(ListItemType.Header, ds, status);
                Repeater1.ItemTemplate = new MyTemplate3(ListItemType.Item, ds, status);
                Repeater1.FooterTemplate = new MyTemplate3(ListItemType.Footer, null, status);
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
    public class MyTemplate3 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string status;
        int i = 0;
        double esic_salary = 0, sal_esic = 0;
        static int ctr;
        public MyTemplate3(ListItemType type, DataSet ds, string status)
        {
            this.type = type;
            this.ds = ds;
            this.status = status;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    if (status != "ALL")
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=7 align=center> ESIC PAID/UNPAID PAYMENT REPORT </th><tr><th>SR. NO.</th><th>STATE NAME</th><th>EMP NAME</th><th>ESIC_NO</th><th>ESIC SALARY</th><th>SAL ESIC</th><th>STATUS</th></tr>");
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><th bgcolor=yellow colspan=7 align=center> ESIC PAYMENT REPORT </th><tr><th>SR. NO.</th><th>STATE NAME</th><th>EMP NAME</th><th>ESIC_NO</th><th>ESIC SALARY</th><th>SAL ESIC</th><th>STATUS</th></tr>");

                    }
                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["esic_salary"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sal_esic"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Status"] + "</td></tr>");
                    esic_salary = esic_salary + double.Parse(ds.Tables[0].Rows[ctr]["esic_salary"].ToString());
                    sal_esic = sal_esic + double.Parse(ds.Tables[0].Rows[ctr]["sal_esic"].ToString());
                  
                    if (ds.Tables[0].Rows.Count == ctr + 1)
                    {
                        lc.Text = lc.Text + "<tr><b><td align=center colspan = 4>Total</td></b><td>" + Math.Round((esic_salary), 2) + "</td><td>" + Math.Round((sal_esic), 2) + "</td></tr>";
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
    //protected string check_status()
    //{
    //     MySqlDataAdapter data=null;

    //     try
    //     {
    //         d.con1.Open();
    //         data = new MySqlDataAdapter("SELECT ESIC_NO FROM pay_emp_compliance WHERE `month` = '06' AND `year` = '2019' AND `state_name` = 'goa'  AND `Flag_ESIC` = '0'",d.con1);
    //         DataSet ds = new DataSet();
    //         data.Fill(ds);
             

    //     }
    //     catch { }
    
    //}

    protected string check1(int i)
    {
        string count = "";
        string where = "";
        string field_name = "";
       
        //ESIC Statement
         if(i==1)
        {
            where = "month='" + txt_esic_date.Text.Substring(0, 2) + "' and year='" + txt_esic_date.Text.Substring(3) + "' and state_name='" + ddl_esic_state.SelectedValue + "'  ";
            field_name = "ESIC_NO";
         }
        //ESIC upload
        else if(i==5)
        {
            where = "month = '" + txt_esic_date.Text.Substring(0, 2) + "' AND year = '" + txt_esic_date.Text.Substring(3) + "' AND state_name = '" + ddl_esic_state.SelectedValue + "' ";
            field_name = "ESIC_NO";
        }
        //pf upload
        else if (i == 3)
        {
            where = "month = '" + txt_pf_date.Text.Substring(0, 2) + "' AND year = '" + txt_pf_date.Text.Substring(3) + "' AND client_code = '" + ddl_pf_client.SelectedValue + "'";
            field_name = "UAN";
        }
        //pf Statement
         else if (i == 2)
         {
             where = "month = '" + txt_pf_date.Text.Substring(0, 2) + "' AND year = '" + txt_pf_date.Text.Substring(3) + "' AND client_code = '" + ddl_pf_client.SelectedValue + "' ";
             field_name = "UAN";
         }
         count = d.getsinglestring("SELECT group_concat(" + field_name + ") FROM pay_emp_compliance WHERE " + where + "");
         //count = "'" + count + "'";
         count = count.Replace(",", "','");
        return count;
    }
}
