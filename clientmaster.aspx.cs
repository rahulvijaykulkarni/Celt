using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public partial class clientmaster : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    string txt_region_code = "";
    public string zone_code = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = false;
            //btnexporttoexcelstate.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            // btnexporttoexcelstate.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            // btnexporttoexcelstate.Visible = false;
        }
        if (!IsPostBack)
        {

            email_grid();
            // Designation
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            d.con1.Open();
            try
            {
                DataTable dt_item = new DataTable();
                MySqlDataAdapter grd = new MySqlDataAdapter("SELECT distinct GRADE_CODE,GRADE_DESC FROM pay_grade_master where COMP_CODE ='" + Session["COMP_CODE"].ToString() + "' ", d.con1);
                grd.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_designation.DataSource = dt_item;
                    ddl_designation.DataTextField = dt_item.Columns[1].ToString();
                    ddl_designation.DataValueField = dt_item.Columns[0].ToString();
                    ddl_designation.DataBind();
                }
                dt_item.Dispose();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ddl_designation.Items.Insert(0, new ListItem("Select"));
                d.con1.Close();
            }
            //komal 23-04-2020 company bank details
            company_bank_load();
            //end

            state();
            loadclientgrid();
            Session["CLIENT_CODE"] = "";
            txt_reason_updation.Text = string.Empty;
            btn_approval.Visible = false;
            l1.Visible = false;
            bill.Visible = false;
            ddl_item_list();
            txt_enddate.ReadOnly = true;

            //MySqlCommand cmd = new MySqlCommand("select `start_date`,`end_date` from pay_designation_count where comp_code='" + Session["comp_code"] + "' and client_code='" + txt_clientcode.Text + "' ", d.con);
            //MySqlDataReader dr = cmd.ExecuteReader();
            //d.con.Open();
            //if (dr.Read())
            //{
            //    string start_date = dr.GetValue(0).ToString();
            //    string end_date = dr.GetValue(1).ToString();
            //    DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
            //    DateTime start_date1 = DateTime.ParseExact(start_date, "dd/MM/yyyy", null);
            //    DateTime end_date1 = DateTime.ParseExact(end_date, "dd/MM/yyyy", null);
            //    if (newdate1 < end_date1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('End Date Exipre For (" + txt_clientname.Text + ") Please Update Date!!');", true);
            //    }
            //}
            //dr.Close();
            //d.con.Close();
            gv_start_end_date();

            //24-05-19 komal
            d.con1.Open();

            try
            {

                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddl_esic_state.DataSource = DS;
                ddl_esic_state.DataBind();
                d.con1.Close();


            }
            catch (Exception ex) { throw ex; }

            finally
            {
                ddl_esic_state.Items.Insert(0, new ListItem("Select", ""));
                d.con1.Close();
            }

            // Bank
            ddl_bank.Items.Clear();
            System.Data.DataTable dt_bank = new System.Data.DataTable();
            MySqlDataAdapter cmd_bank = new MySqlDataAdapter("select distinct(Field1) from pay_zone_master where comp_code='" + Session["comp_code"] + "' and Type='bank_details' order by Field1", d.con);
            d.con.Open();
            try
            {
                cmd_bank.Fill(dt_bank);
                if (dt_bank.Rows.Count > 0)
                {
                    ddl_bank.DataSource = dt_bank;
                    ddl_bank.DataTextField = dt_bank.Columns[0].ToString();
                    ddl_bank.DataValueField = dt_bank.Columns[0].ToString();
                    ddl_bank.DataBind();
                }
                dt_bank.Dispose();
                d.con.Close();
                //ddl_bank.Items.Insert(0, "Select");

            }
            catch (Exception ex) { }
            finally { ddl_bank.Items.Insert(0, new ListItem("Select", "")); }
        }
    }
 //vikas 
    protected void email_grid()
    {
        string dep="";       
        DataTable dt_item = new DataTable();
        for (int i = 1; i <= 6; i++)
        {
            if (i == 1)
            {
                dep = "Admin";
            }
            else if (i == 2)
            {
                dep = "Account";
            }
            else if (i == 3)
            {
                dep = "Manager";
            }
            else if (i == 4)
            {
                dep = "Legal";
            }
            else if (i == 5)
            {
                dep = "Invoice/Finance";
            }
            else if (i == 6)
            {
                dep = "Operation";
            }
            MySqlDataAdapter grd = new MySqlDataAdapter(" select '" + dep + "' as 'field1','' as 'field2','' as 'field3','' as 'field4','' as 'field5','' as 'Field6','' as 'Field7','' as 'Field8','' as 'field9' FROM pay_zone_master limit 1 ", d.con1);
            grd.Fill(dt_item);      
            
        }

        email_grid1.DataSource = dt_item;
        email_grid1.DataBind();    
    }
    protected void get_city_list(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        //string name=  ddl_state.SelectedItem.ToString();
        ddl_location.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_dsg_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            ddl_location.Items.Insert(0, new ListItem("Select", ""));


        }

    }


    public void state()
    {
        //Client State

        DataTable dt_state = new DataTable();
        MySqlDataAdapter adp_state = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            adp_state.Fill(dt_state);
            if (dt_state.Rows.Count > 0)
            {
                ddl_client_state.DataSource = dt_state;
                ddl_client_state.DataTextField = dt_state.Columns[0].ToString();
                ddl_client_state.DataValueField = dt_state.Columns[1].ToString();
                ddl_client_state.DataBind();
            }
            dt_state.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_client_state.Items.Insert(0, new ListItem("Select"));
            d1.con.Close();
        }


        //Designation State

        DataTable dt_dsgstate = new DataTable();
        MySqlDataAdapter adp_dsgstate = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            adp_dsgstate.Fill(dt_dsgstate);
            if (dt_dsgstate.Rows.Count > 0)
            {
                ddl_dsg_state.DataSource = dt_dsgstate;
                ddl_dsg_state.DataTextField = dt_dsgstate.Columns[0].ToString();
                ddl_dsg_state.DataValueField = dt_dsgstate.Columns[1].ToString();
                ddl_dsg_state.DataBind();
            }
            dt_dsgstate.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_dsg_state.Items.Insert(0, new ListItem("Select"));
            d1.con.Close();
        }

        //GST State

        //DataTable dt_gststate = new DataTable();
        //MySqlDataAdapter adp_gststate = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d1.con);
        //d1.con.Open();
        //try
        //{
        //    adp_gststate.Fill(dt_gststate);
        //    if (dt_gststate.Rows.Count > 0)
        //    {
        //        ddl_gst_state.DataSource = dt_gststate;
        //        ddl_gst_state.DataTextField = dt_gststate.Columns[0].ToString();
        //        ddl_gst_state.DataValueField = dt_gststate.Columns[1].ToString();
        //        ddl_gst_state.DataBind();
        //    }
        //    dt_gststate.Dispose();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    ddl_gst_state.Items.Insert(0, new ListItem("Select"));
        //    d1.con.Close();
        // }

    }
    protected void lnkaddtravelplan_Click(object sender, EventArgs e)
    {

        mp1.Show();


    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //company bank details 23-04-2020 komal
        gv_company_bank.DataSource = null;
        gv_company_bank.DataBind();

        //

        Panel20.Visible = false;
        string s = "", pay_zone_master = "pay_zone_master";
        d.con.Open();

        MySqlCommand cmd = new MySqlCommand("select client_code from pay_client_master where client_code ='" + txt_clientcode.Text + "'", d.con);

        MySqlDataReader dr2 = cmd.ExecuteReader();
        if (dr2.Read())
        {
            s = dr2.GetValue(0).ToString();

        }

        if (s.Equals(txt_clientcode.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Client Code already Exist....Please Enter other Client Code !!!')", true);
            txt_clientcode.Text = "";
            return;
        }

        d.con.Close();

        int result = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            //if (gv_itemslist.Rows.Count > 0)
            //{
            //    if (gv_head_type.Rows.Count > 0)
            //    {
            //        if (gv_statewise_gst.Rows.Count > 0)
            //    {

            //if (gv_zone_add.Rows.Count > 0)
            //{
            //    if (gv_regional_zone.Rows.Count > 0)
            //    {

            string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
            if (reporting == "")
            {
                //komal 20-06-19
                result = d.operation("INSERT INTO pay_client_master (COMP_CODE,CLIENT_NAME,CLIENT_CODE,AG_START_DATE,AG_END_DATE,ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,DESIGNATION,total_employee,email_id,password,approval,ot_applicable,iot_applicable,client_phonno,penalty,bill_amount,gst_applicable,material_calc,material_days,Budget_Materials,start_date_billing,end_date_billing,material_rental_policy,comp_bank_name,comp_acc_no,tds_applicable,tds_percentage,tds_on,txt_tds_on,client_active_close,android_att_flag,bank,billing_ot,R_and_M_service,administrative_expense) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientname.Text + "','" + txt_clientcode.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_client_address1.Text + "','" + txt_client_address2.Text + "','" + ddl_client_state.SelectedValue + "','" + ddl_client_city.SelectedValue + "','" + txt_file_no.Text + "','" + txt_branch_count.Text + "','" + txt_reg_no.Text + "','" + txt_gst_no.Text + "','" + txt_pan_no.Text + "','" + txt_license.Text + "','" + txt_bank_detail.Text + "','" + txt_website.Text + "','" + ddl_designation.SelectedValue + "','" + txt_employee_total.Text + "','" + txt_clientemailid.Text + "','" + txt_password.Text + "','','" + ddl_ot_applicable.SelectedValue + "','" + ddl_iot_applicable.SelectedValue + "','" + txt_phoneno.Text + "','" + txt_penalty.Text + "','" + txt_bill_amount.Text + "','" + ddl_gst_applicable.SelectedValue + "','" + ddl_material_calc.SelectedValue + "','" + txt_material_days.Text + "','" + txt_budget_material.Text + "','" + txt_start_date_client.SelectedValue + "','" + txt_end_date_client.Text + "','" + ddl_machine_rent_p.SelectedValue + "','" + ddl_company_bank.SelectedValue + "','" + txt_comp_ac_no.Text + "','" + ddl_tds_applicable.SelectedValue + "', '" + ddl_tds_persent.SelectedValue + "', '" + ddl_tds_on.SelectedValue + "','" + ddl_tds_on.SelectedItem.Text + "','" + ddl_client_ac.SelectedValue + "','" + ddl_android_attendances_flag.SelectedValue + "','" + ddl_bank.SelectedValue + "','" + ddl_ot_billing.SelectedValue + "','" + ddl_service.SelectedValue + "','" + ddl_admin_expence.SelectedValue + "')");
            }
            else
            {
                result = d.operation("INSERT INTO pay_client_master (COMP_CODE,CLIENT_NAME,CLIENT_CODE,AG_START_DATE,AG_END_DATE,ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,DESIGNATION,total_employee,email_id,password,approval,ot_applicable,iot_applicable,client_phonno,penalty,bill_amount,gst_applicable,material_calc,material_days,Budget_Materials,start_date_billing,end_date_billing,material_rental_policy,comp_bank_name,comp_acc_no,tds_applicable,tds_percentage,tds_on,txt_tds_on,client_active_close,android_att_flag,bank,billing_ot,R_and_M_service,administrative_expense) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientname.Text + "','" + txt_clientcode.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_client_address1.Text + "','" + txt_client_address2.Text + "','" + ddl_client_state.SelectedValue + "','" + ddl_client_city.SelectedValue + "','" + txt_file_no.Text + "','" + txt_branch_count.Text + "','" + txt_reg_no.Text + "','" + txt_gst_no.Text + "','" + txt_pan_no.Text + "','" + txt_license.Text + "','" + txt_bank_detail.Text + "','" + txt_website.Text + "','" + ddl_designation.SelectedValue + "','" + txt_employee_total.Text + "','" + txt_clientemailid.Text + "','" + txt_password.Text + "','','" + ddl_ot_applicable.SelectedValue + "','" + ddl_iot_applicable.SelectedValue + "','" + txt_phoneno.Text + "','" + txt_penalty.Text + "','" + txt_bill_amount.Text + "','" + ddl_gst_applicable.SelectedValue + "','" + ddl_material_calc.SelectedValue + "','" + txt_material_days.Text + "','" + txt_budget_material.Text + "','" + txt_start_date_client.SelectedValue + "','" + txt_end_date_client.Text + "','" + ddl_machine_rent_p.SelectedValue + "','" + ddl_company_bank.SelectedValue + "','" + txt_comp_ac_no.Text + "','" + ddl_tds_applicable.SelectedValue + "', '" + ddl_tds_persent.SelectedValue + "', '" + ddl_tds_on.SelectedValue + "','" + ddl_tds_on.SelectedItem.Text + "','" + ddl_client_ac.SelectedValue + "','" + ddl_android_attendances_flag.SelectedValue + "','" + ddl_bank.SelectedValue + "','" + ddl_ot_billing.SelectedValue + "','" + ddl_service.SelectedValue + "','" + ddl_admin_expence.SelectedValue + "')");
            }

            update_policy();
            fire_ext_function();
            // 

            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                int sr_number = int.Parse(((Label)row.FindControl("lbl_srnumber")).Text);
                string lbl_dsgstate = row.Cells[2].Text;
                string designation = row.Cells[3].Text;
                string em_count = row.Cells[4].Text;
                string wrk_hrs = row.Cells[5].Text;
                string startdate = row.Cells[6].Text;
                string endsate = row.Cells[7].Text;
                string location = row.Cells[8].Text;
                string category = row.Cells[9].Text;

                d.operation("Insert Into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location,category) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + txt_clientcode.Text + "','" + lbl_dsgstate.ToString() + "','" + designation.ToString() + "'," + Convert.ToInt32(em_count) + "," + Convert.ToInt32(wrk_hrs) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),str_to_date('" + startdate + "','%d/%m/%Y') ,str_to_date('" + endsate + "','%d/%m/%Y'),'" + location + "','"+ category +"')");
            }
            //check list
            foreach (GridViewRow row in gv_billing_type.Rows)
            {

                string chk_list = row.Cells[2].Text;

                string chk_number = row.Cells[3].Text;

                string checklist_billing = row.Cells[4].Text;
                string checklist_billingid = row.Cells[5].Text;
                string ddl_state = row.Cells[6].Text;

                d.operation("Insert Into pay_client_billing_details (comp_code,client_code,billing_name,billing_id,billing_wise,billingwise_id,state) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + chk_list + "','" + chk_number + "','" + checklist_billing + "','" + checklist_billingid + "','" + ddl_state + "')");
            }
            //vikas add 19-07-2019
            foreach (GridViewRow row in grv_dwduction.Rows)
            {
                string item_name = row.Cells[2].Text;
                string amount = row.Cells[3].Text;
                d.operation("Insert Into pay_deduction (comp_code,client_code,deduction_item,deduction_amount) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + item_name + "','" + amount + "')");
            }

            //bank details MD CHANGE

            //bank_details
            d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and type='bank_details' and CLIENT_CODE='" + txt_clientcode.Text + "'");
            foreach (GridViewRow row in grd_bank_details.Rows)
            {
                d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,CLIENT_CODE,type,Field1,Field2,Field3) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','bank_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
            }
            ViewState["grd_bankdetails"] = "";
            grd_bank_details.DataSource = null;
            grd_bank_details.DataBind();
            // Head Info
            foreach (GridViewRow row in gv_head_type.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field10,Field2,Field1,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field11,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','HEAD','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "')");
                head_login(row.Cells[2].Text, row.Cells[4].Text);
            }

            //Zone Head Info

            foreach (GridViewRow row in gv_zone_add.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,ZONE,Field1,Field11,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','ZONE','" + row.Cells[3].Text + "','" + row.Cells[2].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "','" + row.Cells[15].Text + "')");
                zone_login(row.Cells[3].Text, row.Cells[5].Text);
            }

            // Region Head Info
            foreach (GridViewRow row in gv_regional_zone.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','REGION','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "','" + row.Cells[15].Text + "','" + row.Cells[16].Text + "')");

                region_login(row.Cells[3].Text, row.Cells[4].Text, row.Cells[6].Text);
            }


            //24-05-19 komal ESIC details

            foreach (GridViewRow gr in grid_esic.Rows)
            {
                string cell_1_Value = grid_esic.Rows[gr.RowIndex].Cells[2].Text;
                string cell_2_Value = grid_esic.Rows[gr.RowIndex].Cells[3].Text;
                string cell_3_Value = grid_esic.Rows[gr.RowIndex].Cells[4].Text;
                d.operation("Insert Into pay_esic_table (comp_code,client_code,`ESIC_STATE`,`ESIC_ADDRESS`,`ESIC_CODE`,type) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + cell_1_Value + "','" + cell_2_Value + "','" + cell_3_Value + "','ESIC')");

            }




            // GST Info
            foreach (GridViewRow row in gv_statewise_gst.Rows)
            {

                Label lbl_gststate = (Label)row.FindControl("lbl_gststate");
                string gststate = (lbl_gststate.Text);
                Label lbl_gst_addr = (Label)row.FindControl("lbl_gst_addr");
                string gst_addr = (lbl_gst_addr.Text);
                Label lbl_gstin = (Label)row.FindControl("lbl_gstin");
                string gstin = lbl_gstin.Text.ToString();

                d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,REGION,Field1,Field2) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','GST','" + gststate.ToString() + "','" + gst_addr.ToString() + "','" + gstin.ToString() + "')");
            }

            //foreach (GridViewRow row in gv_services.Rows)
            //{

            //    Label lbl_servicetype = (Label)row.FindControl("lbl_servicestype");
            //    string lbl_servicetype1 = (lbl_servicetype.Text);
            //    Label lbl_startdate = (Label)row.FindControl("lbl_lnkstartdate");
            //    string lbl_startdate1 = (lbl_startdate.Text);
            //    Label lbl_enddate = (Label)row.FindControl("lbl_enddate");
            //    string lbl_enddate1 = lbl_enddate.Text.ToString();

            //    d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,Field2,Field3) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','SERVICES','" + lbl_servicetype1.ToString() + "','" + lbl_startdate1.ToString() + "','" + lbl_enddate1.ToString() + "')");
            //}

            foreach (GridViewRow row in gv_emailsend.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,Field2,Field3,Field4)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','EMAIL','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");

            }

            foreach (GridViewRow row in gv_clientItems.Rows)
            {
                d.operation("INSERT INTO pay_client_item_list (COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");

            }
            foreach (GridViewRow row in gv_comp_group.Rows)
            {
                d.operation("INSERT INTO pay_company_group (comp_code,client_code,comp_name,Companyname_gst_no,gst_address)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
            }


            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Zone And Enter Its Region !!')", true);
            //    }

            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Zone And Related Contact Person !!')", true);
            //}
            //    }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please add At Least One GST Details !!')", true);
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Head !!')", true);
            //        ddl_head_type.Focus();

            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Designation !!')", true);
            //    ddl_dsg_state.Focus();
            //}
		 //vikas add for email
            foreach (GridViewRow row in email_grid1.Rows)
            {
                Label Field = (Label)row.FindControl("lbl_dep");
                string Field1 = (Field.Text);
                TextBox txt_email1 = (TextBox)row.FindControl("txt_email");
                string txt_email = (txt_email1.Text);
                TextBox txt_password11 = (TextBox)row.FindControl("txt_password");
                string txt_password1 = (txt_password11.Text);
                TextBox txt_name1 = (TextBox)row.FindControl("txt_name");
                string txt_name = (txt_name1.Text);
                TextBox txt_mobile1 = (TextBox)row.FindControl("txt_mobile");
                string txt_mobile = (txt_mobile1.Text);
                TextBox txt_deg1 = (TextBox)row.FindControl("txt_deg");
                string txt_deg = (txt_deg1.Text);

                TextBox txt_cc1 = (TextBox)row.FindControl("txt_cc");
                string txt_cc = (txt_cc1.Text);
                TextBox txt_bcc1 = (TextBox)row.FindControl("txt_bcc");
                string txt_bcc = (txt_bcc1.Text);
                TextBox txt_to1 = (TextBox)row.FindControl("txt_to");
                string txt_to = (txt_to1.Text);

                d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,field1,field2,field3,field4,field5,field6,field7,field8,field9) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','client_Email','" + Field1 + "','" + txt_email + "','" + txt_password1 + "','" + txt_name + "','" + txt_deg + "','" + txt_mobile + "','" + txt_cc + "','" + txt_bcc + "','" + txt_to + "')");
            }
            if (result > 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Client Inserted successfully!!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client Inserted successfully!!');", true);
                text_clear();
                loadclientgrid();
            }
            else
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Client failed to Insert...')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client failed to Insert...');", true);
            }
        }
        catch (Exception ee)
        {
            //d.operation("Delete From pay_client_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and approval!=''");
            //d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            //d.operation("Delete From pay_designation_count where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            //d.operation("Delete From pay_images where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            throw ee;
        }
        finally
        {

        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        //company bank details 23-04-2020 komal
        gv_company_bank.DataSource = null;
        gv_company_bank.DataBind();

        //

        Panel20.Visible = false;
        int result = 0;
        string pay_zone_master = "pay_zone_master";
        //if (ddl_client_ac.SelectedValue == "1")
        //{
        //    string count = d.getsinglestring("SELECT COUNT(`unit_code`) FROM `pay_unit_master` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `client_code` = '" + txt_clientcode.Text + "' AND `branch_status` = '0'");
        //    if (count != "0")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please close all active branch first !!!')", true);

        //        return;
        //    }
        //    }
        try
        {
            //if (gv_itemslist.Rows.Count > 0)
            //{
            //    if (gv_head_type.Rows.Count > 0)
            //    {
            //if (gv_zone_add.Rows.Count > 0)
            //{
            //    if (gv_regional_zone.Rows.Count > 0)
            //    {
            d.operation("Delete From pay_company_group where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code is NULL");
            d.operation("Delete From pay_designation_count where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code is null");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'HEAD'");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'ZONE'");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'REGION'");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'GST'");
            //d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'SERVICES'");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'EMAIL'");
            d.operation("Delete From pay_client_item_list where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("delete from pay_zone_master where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and type='bank_details' and CLIENT_CODE='" + txt_clientcode.Text + "'");
            d.operation("Delete From pay_client_billing_details where client_code  = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete From pay_esic_table where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'ESIC'");
            //vikas add 06-07-2019
            d.operation("Delete From pay_deduction where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'client_Email'");
            d.operation("Delete From pay_advance_deduction where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
            d.operation("delete from pay_document_details_history where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ");
            string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
            //if (reporting == "")
            //{
            string active_close = d.getsinglestring("select client_active_close from pay_client_master where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
            result = d.operation("UPDATE pay_client_master set client_active_close = '" + ddl_client_ac.SelectedValue + "', CLIENT_NAME='" + txt_clientname.Text + "',AG_START_DATE = str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),AG_END_DATE = str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),ADDRESS1 = '" + txt_client_address1.Text + "',ADDRESS2 = '" + txt_client_address2.Text + "',STATE = '" + ddl_client_state.SelectedValue + "',CITY = '" + ddl_client_city.SelectedValue + "',FILE_NO = '" + txt_file_no.Text + "',NO_OF_BRANCH = '" + txt_branch_count.Text + "',REG_NO = '" + txt_reg_no.Text + "',GST_NO = '" + txt_gst_no.Text + "',PAN_NO = '" + txt_pan_no.Text + "',LICENSE_NO = '" + txt_license.Text + "',BANK_DETAILS = '" + txt_bank_detail.Text + "',WEBSITE = '" + txt_website.Text + "',DESIGNATION = '" + ddl_designation.SelectedValue + "',comments= concat(IFNULL(comments,''),'" + txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString() + " ON-',now(),'@#$%'),total_employee='" + txt_employee_total.Text + "', approval='" + reporting + "',email_id= '" + txt_clientemailid.Text + "',password='" + txt_password.Text + "',ot_applicable = '" + ddl_ot_applicable.SelectedValue + "',iot_applicable='" + ddl_iot_applicable.SelectedValue + "' ,client_phonno='" + txt_phoneno.Text + "',penalty= '" + txt_penalty.Text + "',bill_amount = '" + txt_bill_amount.Text + "',gst_applicable='" + ddl_gst_applicable.SelectedValue + "',material_calc='" + ddl_material_calc.SelectedValue + "',material_days='" + txt_material_days.Text + "',Budget_Materials='" + txt_budget_material.Text + "' , start_date_billing='" + txt_start_date_client.SelectedValue + "', end_date_billing='" + txt_end_date_client.Text + "',material_rental_policy='" + ddl_machine_rent_p.SelectedValue + "',comp_bank_name = '" + ddl_company_bank.SelectedValue + "',comp_acc_no ='" + txt_comp_ac_no.Text + "' , tds_applicable =  '" + ddl_tds_applicable.SelectedValue + "',tds_percentage = '" + ddl_tds_persent.SelectedValue + "', tds_on = '" + ddl_tds_on.SelectedValue + "', txt_tds_on = '" + ddl_tds_on.SelectedItem.Text + "',android_att_flag='" + ddl_android_attendances_flag.SelectedValue + "',bank ='" + ddl_bank.SelectedValue + "',billing_ot ='" + ddl_ot_billing.SelectedValue + "',R_and_M_service='" + ddl_service.SelectedValue + "',administrative_expense='" + ddl_admin_expence .SelectedValue+ "' WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

            if (!ddl_client_ac.SelectedValue.Equals(active_close))
            {
                if (ddl_client_ac.SelectedValue.Equals("0"))
                {
                    d.operation("UPDATE pay_unit_master set branch_status = '" + ddl_client_ac.SelectedValue + "' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
                    d.operation("UPDATE pay_employee_master set left_date = null, left_reason=null where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
                }
                else if (ddl_client_ac.SelectedValue.Equals("1"))
                {
                    d.operation("UPDATE pay_unit_master set branch_status = '" + ddl_client_ac.SelectedValue + "' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
                    d.operation("UPDATE pay_employee_master set left_date = now(), left_reason='LEFT' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
                }
            
            }

            update_policy();
            fire_ext_function();
            //}
            //else
            //{
            //    result = d.operation("INSERT INTO pay_client_master (COMP_CODE,CLIENT_NAME,CLIENT_CODE,AG_START_DATE,AG_END_DATE,ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,DESIGNATION,total_employee, approval,email_id,password) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientname.Text + "','" + txt_clientcode.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_client_address1.Text + "','" + txt_client_address2.Text + "','" + ddl_client_state.SelectedValue + "','" + ddl_client_city.SelectedValue + "','" + txt_file_no.Text + "','" + txt_branch_count.Text + "','" + txt_reg_no.Text + "','" + txt_gst_no.Text + "','" + txt_pan_no.Text + "','" + txt_license.Text + "','" + txt_bank_detail.Text + "','" + txt_website.Text + "','" + ddl_designation.SelectedValue + "','" + txt_employee_total.Text + "','" + reporting + "','" + txt_clientemailid.Text + "','" + txt_password.Text + "')");
            //}
            // d.operation("Update pay_zone_master set Field1 = '" + txt_op_name.Text + "',Field2 = '" + txt_op_mobile.Text + "',Field3 = '" + txt_op_email.Text + "',Field4 = '" + txt_fn_name.Text + "',Field5 = '" + txt_fn_mobile.Text + "',Field6 = '" + txt_fn_email.Text + "',Field7 = '" + txt_lc_name.Text + "',Field8 = '" + txt_lc_mobile.Text + "',Field9 = '" + txt_lc_email.Text + "',Field10 = '" + txt_oth_name.Text + "',Field11 = '" + txt_oth_mobile.Text + "',Field12 = '" + txt_oth_email.Text + "' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Type = 'HEAD' ");
            // d.operation("Update pay_zone_master set Field1 = '" + txt_rgop_name.Text + "',Field2 = '" + txt_rgop_mobile.Text + "',Field3 = '" + txt_rgop_email.Text + "',Field4 = '" + txt_rgfn_name.Text + "',Field5 = '" + txt_rgfn_mobile.Text + "',Field6 = '" + txt_rgfn_email.Text + "',Field7 = '" + txt_rglc_name.Text + "',Field8 = '" + txt_rglc_mobile.Text + "',Field9 = '" + txt_rglc_email.Text + "',Field10 = '" + txt_rgoth_name.Text + "',Field11 = '" + txt_rgoth_mobile.Text + "',Field12 = '" + txt_rgoth_email.Text + "' where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Type = 'REGION' ");


            //check list
            foreach (GridViewRow row in gv_billing_type.Rows)
            {

                string chk_list = row.Cells[2].Text;
                string chk_number = row.Cells[3].Text;
                string checklist_billing = row.Cells[4].Text;
                string checklist_billingid = row.Cells[5].Text;
                string ddl_state = row.Cells[6].Text;
                string inv_shipping_add = row.Cells[7].Text;

                d.operation("Insert Into pay_client_billing_details (comp_code,client_code,billing_name,billing_id,billing_wise,billingwise_id,state,invoice_shipping_address) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + chk_list + "','" + chk_number + "','" + checklist_billing + "','" + checklist_billingid + "','" + ddl_state + "','" + inv_shipping_add + "')");
            }
            //}
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                int sr_number = int.Parse(((Label)row.FindControl("lbl_srnumber")).Text);
                string lbl_dsgstate = row.Cells[2].Text;
                string designation = row.Cells[3].Text;
                string em_count = row.Cells[4].Text;
                string wrk_hrs = row.Cells[5].Text;
                string startdate = row.Cells[6].Text;
                string endsate = row.Cells[7].Text;
                string location = row.Cells[8].Text;
                string category = row.Cells[9].Text;

                d.operation("Insert Into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location,category) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + txt_clientcode.Text + "','" + lbl_dsgstate.ToString() + "','" + designation.ToString() + "'," + Convert.ToInt32(em_count) + "," + Convert.ToInt32(wrk_hrs) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),str_to_date('" + startdate + "','%d/%m/%Y'),str_to_date('" + endsate + "','%d/%m/%Y'),'" + location + "','" + category + "')");


                //  d.operation("Insert Into pay_designation_count (COMP_CODE,SR_NO,STATE,CLIENT_CODE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + lbl_dsgstate.ToString() + "','" + txt_clientcode.Text + "','" + designation.ToString() + "'," + Convert.ToInt32(em_count) + "," + Convert.ToInt32(wrk_hrs) + ",'" + Session["LOGIN_ID"].ToString() + "',now())");
            }
//vikas add 6-07-2019
            //check list
            foreach (GridViewRow row in grv_dwduction.Rows)
            {

                string chk_list = row.Cells[2].Text;
                string chk_number = row.Cells[3].Text;
               d.operation("Insert Into pay_deduction (comp_code,client_code,deduction_item,deduction_amount) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + chk_list + "','" + chk_number + "')");
            }



            // advance deduction 

            foreach (GridViewRow row in gridview_advance_deduction.Rows)
            {

                string chk_deduction = row.Cells[2].Text;
                string chk_deduction_no = row.Cells[3].Text;
                string chk_uniform = row.Cells[4].Text;
                string chk_shoes = row.Cells[5].Text;
                string chk_id = row.Cells[6].Text;
                string chk_month = row.Cells[7].Text;
                string chk_year = row.Cells[7].Text;

                d.operation("Insert Into pay_advance_deduction (comp_code,client_code,deduction,deduction_no,no_of_uniform,no_of_shoes,no_of_id,month,year,ad_state) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + chk_deduction + "','" + chk_deduction_no + "','" + chk_uniform + "','" + chk_shoes + "','" + chk_id + "','" + chk_month.Substring(0, 2) + "','" + chk_year.Substring(3) + "','" + ddl_client_state.SelectedValue + "')");
            
            }

            //update for pay_document_details_history
            foreach (GridViewRow row in gridview_advance_deduction.Rows)
            {

                string chk_deduction = row.Cells[2].Text;
                string chk_deduction_no = row.Cells[3].Text;
                string chk_uniform = row.Cells[4].Text;
                string chk_shoes = row.Cells[5].Text;
                string chk_id = row.Cells[6].Text;
                string chk_month = row.Cells[7].Text;
                string chk_year = row.Cells[7].Text;


                string sql = null; string month = null;

             

                if (chk_uniform == "1" || chk_uniform == "2")
                {

                    month = d.getsinglestring("SELECT `no_of_set` FROM `pay_document_details_history` WHERE  comp_code = '" + Session["comp_code"].ToString() + "' and`client_code` = '" + txt_clientcode.Text + "' AND `month` IS NOT NULL and month = '" + chk_year + "' and document_type='Uniform'");
                    if (month == "")
                    {

                        sql = "select pay_document_details.comp_code,pay_document_details.client_code, pay_document_details.unit_code,pay_document_details.emp_code,document_type,No_of_set,size," + chk_uniform + ",'" + chk_month + "' from pay_document_details  INNER JOIN `pay_employee_master` ON `pay_document_details`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_document_details`.`client_code` = `pay_employee_master`.`client_code` AND `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + txt_clientcode.Text + "' and no_of_set = '1'  and document_type = 'Uniform'and left_date is null";
                        d.operation("insert into pay_document_details_history (comp_code,client_code,unit_code,emp_code,document_type,No_of_set,size,no_of_deduction,month)" + sql);

                    }


                    sql = "select pay_document_details.comp_code,pay_document_details.client_code, pay_document_details.unit_code,pay_document_details.emp_code,document_type,No_of_set,size," + chk_uniform + ",'" + chk_month + "' from pay_document_details  INNER JOIN `pay_employee_master` ON `pay_document_details`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_document_details`.`client_code` = `pay_employee_master`.`client_code` AND `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + txt_clientcode.Text + "' and no_of_set = '2'  and document_type = 'Uniform'and left_date is null";
                        d.operation("insert into pay_document_details_history (comp_code,client_code,unit_code,emp_code,document_type,No_of_set,size,no_of_deduction,month)" + sql);

                    
                }

                if (chk_shoes == "1" || chk_shoes == "0")
                {
                    sql = "select pay_document_details.comp_code,pay_document_details.client_code, pay_document_details.unit_code,pay_document_details.emp_code,document_type,No_of_set,size," + chk_shoes + ",'" + chk_month + "' from pay_document_details  where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + txt_clientcode.Text + "' and document_type = 'Shoes'  ";
                    d.operation("insert into pay_document_details_history (comp_code,client_code,unit_code,emp_code,document_type,No_of_set,size,no_of_deduction,month)" + sql);
                }
                 
                if (chk_id == "1" || chk_id == "0")
                {
                    sql = "select pay_document_details.comp_code,pay_document_details.client_code,pay_document_details. unit_code,pay_document_details.emp_code,document_type,No_of_set,size," + chk_id + ",'" + chk_month + "' from pay_document_details where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + txt_clientcode.Text + "' and document_type = 'Id_Card'  ";
                    d.operation("insert into pay_document_details_history (comp_code,client_code,unit_code,emp_code,document_type,No_of_set,size,no_of_deduction,month)" + sql);
                }
                
            }



            //bank_details MD CHANGE

            foreach (GridViewRow row in grd_bank_details.Rows)
            {
                d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,CLIENT_CODE,type,Field1,Field2,Field3) VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','bank_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
            }
            grd_bank_details.DataSource = null;
            grd_bank_details.DataBind();
            ViewState["grd_bankdetails"] = "";

            // Head Info
            foreach (GridViewRow row in gv_head_type.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field10,Field2,Field1,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field11,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','HEAD','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "')");
                string temp1 = d.getsinglestring("SELECT LOGIN_ID from pay_user_master where unit_flag='4' and client_code='" + txt_clientcode.Text.ToString() + "' AND client_zone='" + row.Cells[2].Text + "'");
                if (temp1 == "")
                {

                    head_login(row.Cells[2].Text, row.Cells[4].Text);
                }
            }

            //Zone Head Info

            foreach (GridViewRow row in gv_zone_add.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,ZONE,Field1,Field11,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','ZONE','" + row.Cells[3].Text + "','" + row.Cells[2].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "','" + row.Cells[15].Text + "')");

                string temp1 = d.getsinglestring("SELECT LOGIN_ID from pay_user_master where unit_flag='2' and client_code='" + txt_clientcode.Text.ToString() + "' AND client_zone='" + row.Cells[3].Text + "'");
                if (temp1 == "")
                {

                    zone_login(row.Cells[3].Text, row.Cells[5].Text);
                }
            }

            // Region Head Info
            foreach (GridViewRow row in gv_regional_zone.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','REGION','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text + "','" + row.Cells[9].Text + "','" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[12].Text + "','" + row.Cells[13].Text + "','" + row.Cells[14].Text + "','" + row.Cells[15].Text + "','" + row.Cells[16].Text + "')");
                //Can not merge
                string temp = d.getsinglestring("SELECT LOGIN_ID from pay_user_master where unit_flag='3' and client_code='" + txt_clientcode.Text.ToString() + "' AND client_zone='" + row.Cells[3].Text + "' and zone_region='" + row.Cells[4].Text + "'");
                if (temp == "")
                {
                    region_login(row.Cells[3].Text, row.Cells[4].Text, row.Cells[6].Text);
                }

            }




            //24-05-19 komal ESIC details

            foreach (GridViewRow gr in grid_esic.Rows)
            {
                string cell_1_Value = grid_esic.Rows[gr.RowIndex].Cells[2].Text;
                string cell_2_Value = grid_esic.Rows[gr.RowIndex].Cells[3].Text;
                string cell_3_Value = grid_esic.Rows[gr.RowIndex].Cells[4].Text;
                d.operation("Insert Into pay_esic_table (comp_code,client_code,`ESIC_STATE`,`ESIC_ADDRESS`,`ESIC_CODE`,type) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + cell_1_Value + "','" + cell_2_Value + "','" + cell_3_Value + "','ESIC')");

            }

            foreach (GridViewRow row in gv_statewise_gst.Rows)
            {
                //Label lbl_srnumber = (Label)row.FindControl("lbl_srnumber");
                //int sr_number = Convert.ToInt32(lbl_srnumber.Text);
                Label lbl_gststate = (Label)row.FindControl("lbl_gststate");
                string gststate = (lbl_gststate.Text);
                Label lbl_gst_addr = (Label)row.FindControl("lbl_gst_addr");
                string gst_addr = (lbl_gst_addr.Text);
                Label lbl_gstin = (Label)row.FindControl("lbl_gstin");
                string gstin = lbl_gstin.Text.ToString();

                d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,REGION,Field1,Field2) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','GST','" + gststate.ToString() + "','" + gst_addr.ToString() + "','" + gstin.ToString() + "')");
            }

            //chaitali 18-12-2019
            //foreach (GridViewRow row in gv_services.Rows)
            //{

            //    Label lbl_servicetype = (Label)row.FindControl("lbl_servicestype");
            //    string lbl_servicetype1 = (lbl_servicetype.Text);
            //    Label lbl_startdate = (Label)row.FindControl("lbl_lnkstartdate");
            //    string lbl_startdate1 = (lbl_startdate.Text);
            //    Label lbl_enddate = (Label)row.FindControl("lbl_enddate");
            //    string lbl_enddate1 = lbl_enddate.Text.ToString();

            //    d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,Field2,Field3) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','SERVICES','" + lbl_servicetype1.ToString() + "','" + lbl_startdate1.ToString() + "','" + lbl_enddate1.ToString() + "')");
            //}

            foreach (GridViewRow row in gv_emailsend.Rows)
            {
                d.operation("INSERT INTO pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,Field2,Field3,Field4)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','EMAIL','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");

            }

            foreach (GridViewRow row in gv_clientItems.Rows)
            {
                d.operation("INSERT INTO pay_client_item_list (COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");

            }
            string comp_str = "";
            //List<string> comp_list = new List<string>();
            foreach (GridViewRow row in gv_comp_group.Rows)
            {
                d.operation("INSERT INTO pay_company_group (comp_code,client_code,comp_name,Companyname_gst_no,gst_address)VALUES('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
                //comp_list.Add(row.Cells[2].Text);
                comp_str = comp_str + "'" + row.Cells[2].Text + "',";

            }
 //vikas add for email
            foreach (GridViewRow row in email_grid1.Rows)
            {
                Label Field = (Label)row.FindControl("lbl_dep");
                string Field1 = (Field.Text);
                TextBox txt_email1 = (TextBox)row.FindControl("txt_email");
                string txt_email = (txt_email1.Text);
                TextBox txt_password11 = (TextBox)row.FindControl("txt_password");
                string txt_password1 = (txt_password11.Text);
                TextBox txt_name1 = (TextBox)row.FindControl("txt_name");
                string txt_name = (txt_name1.Text);
                TextBox txt_mobile1 = (TextBox)row.FindControl("txt_mobile");
                string txt_mobile = (txt_mobile1.Text);
                TextBox txt_deg1 = (TextBox)row.FindControl("txt_deg");
                string txt_deg = (txt_deg1.Text);

                TextBox txt_cc1 = (TextBox)row.FindControl("txt_cc");
                string txt_cc = (txt_cc1.Text);
                TextBox txt_bcc1 = (TextBox)row.FindControl("txt_bcc");
                string txt_bcc = (txt_bcc1.Text);
                TextBox txt_to1 = (TextBox)row.FindControl("txt_to");
                string txt_to = (txt_to1.Text);
                d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,field1,field2,field3,field4,field5,field6,field7,field8,field9) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','client_Email','" + Field1 + "','" + txt_email + "','" + txt_password1 + "','" + txt_name + "','" + txt_deg + "','" + txt_mobile + "','" + txt_cc + "','" + txt_bcc + "','" + txt_to + "')");
            }
            
            //if (cg_flag == 1)
            //{
            if (!comp_str.Equals(""))
            {
                comp_str = comp_str.Substring(0, comp_str.Length - 1);
                d.operation("Delete From pay_company_group where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and comp_name not in (" + comp_str + ") and unit_code is not null");
            }
           // }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Zone And Enter Its Region !!')", true);
            //    }

            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Zone And Related Contact Person !!')", true);
            //}
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Head !!')", true);
            //        ddl_head_type.Focus();

            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Designation !!')", true);
            //    ddl_dsg_state.Focus();
            //}
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result1 = 0;
            if (ddl_gst_payed.SelectedValue != "SEL")
            {
                result1 = d.operation("update pay_unit_master set GST_to_be='" + ddl_gst_payed.SelectedValue.ToString() + "' where client_code ='" + txt_clientcode.Text + "' and comp_code='"+Session["COMP_CODE"].ToString()+"'");
            }
           
            if (result > 0)
            {
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client updated successfully!!');", true);
                email_grid();
                text_clear();
                btn_add.Visible = true;
                btn_edit.Visible = false;
                btn_delete.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client Updating failed...');", true);
                
            }

            gridview_advance_deduction.DataSource = null;
            gridview_advance_deduction.DataBind();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            loadclientgrid();
            txt_clientcode.ReadOnly = false;
        }
        // }
    }



    protected void btn_delete_Click(object sender, EventArgs e)
    {
        //StateBAL stbl4 = new StateBAL();
        int result = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd_1 = new MySqlCommand("SELECT EMP_CODE FROM pay_employee_master WHERE unit_code in (select unit_code from pay_unit_master where client_code = '" + txt_clientcode.Text + "') limit 1", d.con1);
            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee exist can not delete this Client');", true);
            }
            else
            {
                result = d.operation("Delete From pay_client_master where client_code ='" + txt_clientcode.Text + "' ");
                d.operation("Delete From pay_zone_master where client_code ='" + txt_clientcode.Text + "' ");
                d.operation("Delete From pay_images where client_code ='" + txt_clientcode.Text + "' ");
                d.operation("Delete From pay_company_group where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                d.operation("Delete From pay_esic_table where client_code = '" + txt_clientcode.Text + "' ");
                //vikas 16-07-2019
                d.operation("Delete From pay_deduction where client_code = '" + txt_clientcode.Text + "' ");
                if (result > 0)
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Client deleted successfully!!')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client deleted successfully!!');", true);
                    text_clear();
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Client deletion failed...')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Client deletion failed...');", true);
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();
            loadclientgrid();
            Session["CLIENT_CODE"] = "";
            txt_reason_updation.Text = string.Empty;
            btn_approval.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Client deleted successfully!!')", true);
    }
    public void loadclientgrid()
    {
        try
        {
            //client grid
            //MySqlCommand cmd_1 = new MySqlCommand("SELECT CLIENT_CODE FROM PAY_ASSIGN_CLIENT WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and EMP_CODE = '" + Session["LOGIN_ID"].ToString() + "'", d.con1);
            //d.con1.Open();
            //MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            //if (dr_1.HasRows)
            //{
            //    while (dr_1.Read())
            //    {
            //        ClientGridView.Visible = true;
            //        d1.con1.Open();
            //        MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT CLIENT_NAME AS 'CLIENT NAME',CLIENT_CODE,(SELECT DISTINCT(STATE_NAME) FROM pay_state_master WHERE STATE_CODE = PAY_CLIENT_MASTER.STATE) AS STATE,CITY,AG_START_DATE AS 'START DATE',AG_END_DATE AS 'END DATE' FROM PAY_CLIENT_MASTER WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE in ('" + dr_1.GetValue(0).ToString() + "') and (approval='' || approval is null) ORDER BY ID", d1.con1);
            //        DataSet ds = new DataSet();
            //        adp_grid.Fill(ds);
            //        ClientGridView.DataSource = ds;
            //        ClientGridView.DataBind();
            //        d1.con1.Close();
            //    }
            //}
            //else
            //{
            ClientGridView.Visible = true;
            d1.con1.Open();
            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT CLIENT_NAME AS 'CLIENT NAME',CLIENT_CODE,(SELECT DISTINCT(STATE_NAME) FROM pay_state_master WHERE STATE_CODE = pay_client_master.STATE) AS STATE,CITY,total_employee as 'TOTAL EMPLOYEE' FROM pay_client_master WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' ORDER BY ID", d1.con1);
            DataSet ds = new DataSet();
            adp_grid.Fill(ds);
            ClientGridView.DataSource = ds;
            ClientGridView.DataBind();
            d1.con1.Close();


            // }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            files_upload.Visible = false;
            load_reporting_grdv();
            reason_panel.Visible = false;
            text_clear();
        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
    }

    protected void btnreportstate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }


    protected void lnk_new_designation_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        Response.Redirect("GradeMaster.aspx");
    }

    protected void ddl_client_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_client_city.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_code='" + ddl_client_state.SelectedValue + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_client_city.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            ddl_client_city.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }
    protected void ClientGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel20.Visible = true;
        gv_statewise_gst.DataSource = null;
        //  grid_esic.DataSource = null;
        gv_regional_zone.DataSource = null;
        gv_regional_zone.DataBind();
        gv_zone_add.DataSource = null;
        gv_zone_add.DataBind();
        gv_head_type.DataSource = null;
        gv_head_type.DataBind();
        gv_emailsend.DataSource = null;
        gv_billing_type.DataSource = null;
        grid_esic.DataSource = null;
        grid_esic.DataBind();
        //vikas 16-07-2019
        grv_dwduction.DataSource = null;
        grv_dwduction.DataBind();

        gridview_advance_deduction.DataSource = null;
        gridview_advance_deduction.DataBind();

        // 21-07-2020 fire ext komal 

        gv_fire_ext.DataSource = null;
        gv_fire_ext.DataBind();

        // 21-07-2020 fire ext komal end

        // gv_statewise_gst.Visible = false;
        //gv_regional_zone.Visible = false;
        //gv_zone_add.Visible = false;
        Session["CLIENT_CODE"] = ClientGridView.SelectedRow.Cells[1].Text;
        load_fields(ClientGridView.SelectedRow.Cells[1].Text, 1);
        l1.Visible = true;
        //MySqlCommand cmd = new MySqlCommand("select distinct(end_date),`start_date` from pay_designation_count where comp_code='" + Session["comp_code"] + "' and client_code='" + txt_clientcode.Text + "' limit 1 ", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();

        //if (dr.Read())
        //{
        //    string start_date = dr.GetValue(1).ToString();
        //    string end_date = dr.GetValue(0).ToString();
        //    if (start_date != "" || end_date != "")
        //    {
        //        DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
        //        DateTime start_date1 = DateTime.ParseExact(start_date, "dd/MM/yyyy", null);
        //        DateTime end_date1 = DateTime.ParseExact(end_date, "dd/MM/yyyy", null);
        //        if (newdate1 > end_date1)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('End Date Exipre For (" + txt_clientname.Text + ") Please Update Date!!');", true);
        //        }
        //    }
        //}
        //dr.Close();
        //d.con.Close();

        company_bank_details();
    }

    protected void gv_start_end_date()
    {
        try
        {
            //  MySqlDataAdapter adp = new MySqlDataAdapter("select distinct(start_date) as 'start_date',`end_date` as 'end_date',client_name as 'Client_name' from pay_designation_count inner join pay_client_master on pay_designation_count.client_code=pay_client_master.client_code and  pay_designation_count.comp_code=pay_client_master.comp_code  where   (DATE_FORMAT(CURDATE(), '%d/%m/%Y')> end_date) and pay_designation_count.comp_code='" + Session["comp_code"] + "' and start_date!=''  ", d.con);
            MySqlDataAdapter adp = new MySqlDataAdapter(" select distinct(start_date) as 'start_date',(end_date) as 'end_date', (client_name) as 'Client_name' from pay_client_master inner join pay_designation_count on pay_client_master.client_code=pay_designation_count.client_code and  pay_client_master.comp_code=pay_designation_count.comp_code   where pay_client_master.comp_code='" + Session["comp_code"] + "'  and (CURDATE()> end_date) and pay_designation_count.start_date!=''", d.con);
            d.con.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            gv_start_end_date_details.DataSource = dt;
            gv_start_end_date_details.DataBind();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {

        }



        d.con.Close();
    }

    private void load_fields(string clientcode, int counter)
    {

        //if (counter == 1)
        //{
        MySqlCommand cmd2 = new MySqlCommand("select CLIENT_NAME,CLIENT_CODE,DATE_FORMAT(AG_START_DATE,'%d/%m/%Y'),DATE_FORMAT(AG_END_DATE,'%d/%m/%Y'),ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,total_employee,comments,email_id,password,ot_applicable,iot_applicable,client_phonno,penalty,bill_amount,gst_applicable ,material_calc,material_days,Budget_Materials,start_date_billing,end_date_billing,material_rental_policy,comp_bank_name,comp_acc_no,tds_applicable,tds_percentage,tds_on,client_active_close,android_att_flag,bank,billing_ot,R_and_M_service,administrative_expense FROM pay_client_master WHERE CLIENT_CODE = '" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
        //}
        //else
        //{
        //    cmd2 = new MySqlCommand("select CLIENT_NAME,CLIENT_CODE,DATE_FORMAT(AG_START_DATE,'%d/%m/%Y'),DATE_FORMAT(AG_END_DATE,'%d/%m/%Y'),ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,total_employee,comments,email_id,password   FROM pay_client_master WHERE CLIENT_CODE = '" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "'", d.con);
        //}
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_clientname.Text = dr2.GetValue(0).ToString();
                txt_clientcode.Text = dr2.GetValue(1).ToString();
                txt_start_date.Text = dr2.GetValue(2).ToString();
                txt_end_date.Text = dr2.GetValue(3).ToString();
                txt_client_address1.Text = dr2.GetValue(4).ToString();
                txt_client_address2.Text = dr2.GetValue(5).ToString();
                ddl_client_state.SelectedValue = dr2.GetValue(6).ToString();
                ddl_client_state_SelectedIndexChanged(null, null);
                ddl_client_city.SelectedValue = dr2.GetValue(7).ToString();
                txt_file_no.Text = dr2.GetValue(8).ToString();
                txt_branch_count.Text = dr2.GetValue(9).ToString();
                txt_reg_no.Text = dr2.GetValue(10).ToString();
                txt_gst_no.Text = dr2.GetValue(11).ToString();
                txt_pan_no.Text = dr2.GetValue(12).ToString();
                txt_license.Text = dr2.GetValue(13).ToString();
                txt_bank_detail.Text = dr2.GetValue(14).ToString();
                txt_website.Text = dr2.GetValue(15).ToString();
                txt_employee_total.Text = dr2.GetValue(16).ToString();
                string tet = reason_updt(dr2.GetValue(17).ToString(), 1);

                txt_clientemailid.Text = dr2.GetValue(18).ToString();
                txt_password.Text = dr2.GetValue(19).ToString();
                ddl_ot_applicable.SelectedValue = dr2.GetValue(20).ToString();
                ddl_iot_applicable.SelectedValue = dr2.GetValue(21).ToString();
                txt_phoneno.Text = dr2.GetValue(22).ToString();
                txt_penalty.Text = dr2.GetValue(23).ToString();
                txt_bill_amount.Text = dr2.GetValue(24).ToString();
                ddl_gst_applicable.SelectedValue = dr2.GetValue(25).ToString();

                ddl_material_calc.SelectedValue = dr2.GetValue(26).ToString();
                txt_material_days.Text = dr2.GetValue(27).ToString();
                txt_budget_material.Text = dr2.GetValue(28).ToString();
                //komal 20-06-19
                txt_start_date_client.SelectedValue = dr2.GetValue(29).ToString();
                txt_end_date_client.Text = dr2.GetValue(30).ToString();
                ddl_machine_rent_p.SelectedValue = dr2.GetValue(31).ToString();
                company_bank_load();
                ddl_company_bank.SelectedValue = dr2.GetValue(32).ToString();
                txt_comp_ac_no.Text = dr2.GetValue(33).ToString();
                ddl_tds_applicable.SelectedValue = dr2.GetValue(34).ToString();
                ddl_tds_persent.SelectedValue = dr2.GetValue(35).ToString();
                ddl_tds_on.SelectedValue = dr2.GetValue(36).ToString();
                ddl_client_ac.SelectedValue = dr2.GetValue(37).ToString();
                ddl_android_attendances_flag.SelectedValue = dr2.GetValue(38).ToString();
                ddl_bank.SelectedValue = dr2.GetValue(39).ToString();
                ddl_ot_billing.SelectedValue = dr2.GetValue(40).ToString();
				ddl_service.SelectedValue = dr2.GetValue(41).ToString();
                ddl_admin_expence.SelectedValue = dr2.GetValue(42).ToString();
            
            }
            dr2.Dispose();
        }
        catch (Exception ex) { }
        finally
        {
            if (txt_clientcode.Text == "Credence")
            {
                bill.Visible = true;
            }
            d.con.Close();
            files_upload.Visible = true;
            load_grdview();
            txt_clientcode.ReadOnly = true;
            ddl_reginheadzone();
        }

        MySqlCommand cmd1 = new MySqlCommand("SELECT SR_NO,STATE,DESIGNATION,COUNT,HOURS,DATE_FORMAT(start_date,'%d/%m/%Y')as 'start_date',DATE_FORMAT(end_date,'%d/%m/%Y') as 'end_date',location,category FROM pay_designation_count WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and UNIT_CODE is null ORDER BY 2 ", d.con);
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

                if (dt.Rows.Count > 0)
                {
                    ddl_gst_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[1].ToString());
                    ddl_gst_state.DataTextField = dt.Columns[1].ToString();
                    ddl_gst_state.DataValueField = dt.Columns[1].ToString();
                    ddl_gst_state.DataBind();

                    ddl_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[1].ToString());
                    ddl_state.DataTextField = dt.Columns[1].ToString();
                    ddl_state.DataValueField = dt.Columns[1].ToString();
                    ddl_state.DataBind();
                    ddl_state.Items.Insert(0, new ListItem("ALL"));
                    ddl_gst_state.Items.Insert(0, new ListItem("Select"));
                    ddl_gst_state.Items.Insert(1, new ListItem("All"));
                }
            }
            dr1.Close();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        // Client Head

        MySqlCommand cmd_hd = new MySqlCommand("SELECT Field10,Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9 ,Field11,Field12,Field13 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'HEAD' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
            if (dr_hd.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_hd);
                if (dt.Rows.Count > 0)
                {
                    ViewState["headtable"] = dt;
                }
                gv_head_type.DataSource = dt;
                gv_head_type.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        // deduction  Head

        MySqlCommand cmd_deduction = new MySqlCommand("SELECT deduction_item,deduction_amount FROM pay_deduction WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_hd = cmd_deduction.ExecuteReader();
            if (dr_hd.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_hd);
                if (dt.Rows.Count > 0)
                {
                    ViewState["deduc_group"] = dt;
                }
                grv_dwduction.DataSource = dt;
                grv_dwduction.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        // advance deduction

        MySqlCommand cmd_deduction_advance = new MySqlCommand("SELECT deduction,deduction_no,no_of_uniform,GROUP_CONCAT(`month`, '/', `year`) AS 'month',no_of_shoes,no_of_id FROM pay_advance_deduction WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' group by month ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_hd = cmd_deduction_advance.ExecuteReader();
            if (dr_hd.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_hd);
                if (dt.Rows.Count > 0)
                {
                    ViewState["deduc_group"] = dt;
                }
                gridview_advance_deduction.DataSource = dt;
                gridview_advance_deduction.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        // for fire extinguisher komal 21-07-2020

        gv_fire_ext.DataSource = null;
        gv_fire_ext.DataBind();
      //  MySqlCommand cmd_fire = new MySqlCommand("SELECT fire_ext_applicable,no_of_days,txt_interval FROM pay_client_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
       MySqlDataAdapter cmd_fire = new MySqlDataAdapter("SELECT fire_ext_applicable,no_of_days,txt_interval FROM pay_client_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  ", d.con);
        d.con.Open();
        try
        {

            //DataSet DS1 = new DataSet();
            //cmd_fire.Fill(DS1);

            //ViewState["fire_ext_grp"] = DS1;

            ////DataTable dt = new DataTable();
            ////ViewState["deduc_group"] = dt;

            //gv_fire_ext.DataSource = DS1;
            //gv_fire_ext.DataBind();

            DataTable dt = new DataTable();
            cmd_fire.Fill(dt);
            if ((dt.Rows.Count > 0) && (dt.Rows[0][0] != DBNull.Value))
            {
                ViewState["fire_ext_grp"] = dt;

                gv_fire_ext.DataSource = dt;
                gv_fire_ext.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
   
        // for fire extinguisher komal 21-07-2020 end 



        //string type = "";
        //MySqlCommand cmd_zon = new MySqlCommand("SELECT Type, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12,ZONE FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'HEAD' ", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataReader dr_zn = cmd_zon.ExecuteReader();
        //    while (dr_zn.Read())
        //    {
        //        type = dr_zn.GetValue(0).ToString();


        //            txt_op_name.Text = dr_zn.GetValue(1).ToString();
        //            txt_op_mobile.Text = dr_zn.GetValue(2).ToString();
        //            txt_op_email.Text = dr_zn.GetValue(3).ToString();
        //            txt_fn_name.Text = dr_zn.GetValue(4).ToString();
        //            txt_fn_mobile.Text = dr_zn.GetValue(5).ToString();
        //            txt_fn_email.Text = dr_zn.GetValue(6).ToString();
        //            txt_lc_name.Text = dr_zn.GetValue(7).ToString();
        //            txt_lc_mobile.Text = dr_zn.GetValue(8).ToString();
        //            txt_lc_email.Text = dr_zn.GetValue(9).ToString();
        //            txt_oth_name.Text = dr_zn.GetValue(10).ToString();
        //            txt_oth_mobile.Text = dr_zn.GetValue(11).ToString();
        //            txt_oth_email.Text = dr_zn.GetValue(12).ToString();

        //        //if (type == "REGION")
        //        //{
        //        //    txt_rgop_name.Text = dr_zn.GetValue(1).ToString();
        //        //    txt_rgop_mobile.Text = dr_zn.GetValue(2).ToString();
        //        //    txt_rgop_email.Text = dr_zn.GetValue(3).ToString();
        //        //    txt_rgfn_name.Text = dr_zn.GetValue(4).ToString();
        //        //    txt_rgfn_mobile.Text = dr_zn.GetValue(5).ToString();
        //        //    txt_rgfn_email.Text = dr_zn.GetValue(6).ToString();
        //        //    txt_rglc_name.Text = dr_zn.GetValue(7).ToString();
        //        //    txt_rglc_mobile.Text = dr_zn.GetValue(8).ToString();
        //        //    txt_rglc_email.Text = dr_zn.GetValue(9).ToString();
        //        //    txt_rgoth_name.Text = dr_zn.GetValue(10).ToString();
        //        //    txt_rgoth_mobile.Text = dr_zn.GetValue(11).ToString();
        //        //    txt_rgoth_email.Text = dr_zn.GetValue(12).ToString();
        //        //}
        //    }
        //    dr_zn.Close();
        //}
        //catch (Exception ex) { }
        //finally { d.con.Close(); }

        // Zone Head
        MySqlCommand cmd_zn = new MySqlCommand("SELECT ZONE,Field1,Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12,Field13 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'ZONE' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_zn1 = cmd_zn.ExecuteReader();
            if (dr_zn1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_zn1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["zonetable"] = dt;
                }
                gv_zone_add.DataSource = dt;
                gv_zone_add.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }


        // Region Head
        MySqlCommand cmd_rg = new MySqlCommand("SELECT ZONE,REGION,Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12,Field13 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'REGION' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_rg = cmd_rg.ExecuteReader();
            if (dr_rg.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_rg);
                if (dt.Rows.Count > 0)
                {
                    ViewState["regiontable"] = dt;
                }
                gv_regional_zone.DataSource = dt;
                gv_regional_zone.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }



        //25-05-19 komal ESIC_info

        MySqlCommand cmd_esic = new MySqlCommand("select `ESIC_STATE`,`ESIC_ADDRESS`,`ESIC_CODE` from pay_esic_table where client_code ='" + clientcode + "'and comp_code = '" + Session["comp_code"].ToString() + "' and type = 'ESIC'", d.con);
        d.con.Open();
        try
        {

            MySqlDataReader dr_esic = cmd_esic.ExecuteReader();

            if (dr_esic.HasRows)
            {
                DataTable dt1 = new DataTable();
                dt1.Load(dr_esic);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt1;
                }
                grid_esic.DataSource = dt1;
                grid_esic.DataBind();

            }


        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        // GST Info
        MySqlCommand cmd_gst = new MySqlCommand("SELECT REGION,Field1, Field2 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'GST'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_gst = cmd_gst.ExecuteReader();
            if (dr_gst.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_gst);
                if (dt.Rows.Count > 0)
                {
                    ViewState["gsttable"] = dt;
                }
                gv_statewise_gst.DataSource = dt;
                gv_statewise_gst.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }


        MySqlCommand cmd_gst1 = new MySqlCommand("SELECT ID,Field1, Field2,Field3 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'SERVICES'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_gst1 = cmd_gst1.ExecuteReader();
            if (dr_gst1.HasRows)
            {
                DataTable dt1 = new DataTable();
                dt1.Load(dr_gst1);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["servicestable"] = dt1;
                }
                gv_services.DataSource = dt1;
                gv_services.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        MySqlCommand cmd_email = new MySqlCommand("SELECT Field1,Field2, Field3, Field4 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'EMAIL' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_zn1 = cmd_email.ExecuteReader();
            if (dr_zn1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_zn1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["emailtable"] = dt;
                }
                gv_emailsend.DataSource = dt;
                gv_emailsend.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        MySqlCommand cmd_email1 = new MySqlCommand("SELECT item_name,quantiry, validity, expiry_date FROM  pay_client_item_list WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_zn1 = cmd_email1.ExecuteReader();
            if (dr_zn1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_zn1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["clientitemtable"] = dt;
                }
                gv_clientItems.DataSource = dt;
                gv_clientItems.DataBind();
            }
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        //Company Group
        MySqlCommand cmd_cg = new MySqlCommand("SELECT comp_name,Companyname_gst_no, gst_address FROM  pay_company_group WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and (Unit_code is null Or Unit_code = '') ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_cg = cmd_cg.ExecuteReader();
            if (dr_cg.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_cg);
                if (dt.Rows.Count > 0)
                {
                    ViewState["Comp_Group"] = dt;
                }
                gv_comp_group.DataSource = dt;
                gv_comp_group.DataBind();
            }
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }


        //Billing Type
        gv_billing_type.DataSource = null;
        gv_billing_type.DataBind();

        MySqlCommand cmd1_cg = new MySqlCommand("SELECT `billing_id` as 'checklist_number', `billingwise_id` as 'checklist_billingNo' , billing_name as 'checklist_name',billing_wise  as 'checklist_billing',state as 'State',invoice_shipping_address as 'invoice_shipping_address' FROM  pay_client_billing_details WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_cg = cmd1_cg.ExecuteReader();
            if (dr_cg.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_cg);
                if (dt.Rows.Count > 0)
                {
                    ViewState["Billing_Type"] = dt;
                }
                gv_billing_type.DataSource = dt;
                gv_billing_type.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
        //email vikas
       

        MySqlCommand cmd_e = new MySqlCommand("SELECT Field1, Field2, Field3, Field4, Field5,Field6,Field7,Field8,field9 FROM pay_zone_master WHERE client_code ='" + clientcode + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'client_Email' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader cmd_4 = cmd_e.ExecuteReader();
            if (cmd_4.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(cmd_4);
                if (dt.Rows.Count > 0)
                {
                    ViewState["email_type"] = dt;
                }
                email_grid1.DataSource = dt;
                email_grid1.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        try
        {
            load_bank_gridview(Session["COMP_CODE"].ToString(), txt_clientcode.Text);
        }
        catch (Exception ex) { throw ex; }
        finally { };

        if (counter == 1)
        {
            reason_panel.Visible = true;
            btn_add.Visible = false;
            btn_delete.Visible = true;
            btn_edit.Visible = true;
        }
        else
        {
            files_upload.Visible = true;
        }
    }

    protected void ClientGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.ClientGridView, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[1].Visible = false;
    }


    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        string chk_category = d.getsinglestring(" select category from pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and  client_code='" + txt_clientcode.Text + "' and `DESIGNATION`='" + ddl_designation.SelectedItem + "'  and state='" + ddl_dsg_state.SelectedItem + "' and UNIT_CODE is NULL  ");
        string des = ""+ ddl_categories.SelectedItem +"";
        if (chk_category != "")
        {
            if (chk_category != des)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Designation Already Added For " + chk_category + " . !!')", true);
                return;

            }
        }
        if (!count_emp_count())
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Increase The Value Of Total Number Of Employee!!')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Increase The Value Of Total Number Of Employee!!')", true);
            return;
        }

        if (txt_emp_count.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation Count Cannot be Zero (0).');", true);
            return;
        }

        int count = 0;
        //vikas comment for second add designation 08/06/2019 
        for (int rownum1 = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
        {
            if (gv_itemslist.Rows[rownum1].RowType == DataControlRowType.DataRow)
            {
                //        if ((ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[3].Text) && (ddl_dsg_state.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text) && (txt_working_hrs.Text == gv_itemslist.Rows[rownum1].Cells[5].Text))
                //        {
                //            count = 1;
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation already added with same State and Working Hours.');", true);
                //            return;
                //        }
                //komal 21-06-19

                if ((txt_satrtdate.Text == gv_itemslist.Rows[rownum1].Cells[6].Text))
                {

                    if (ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[3].Text)
                    {
                        if (ddl_dsg_state.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text)
                        {
                            count = 1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('same starting date does not added');", true);
                            return;
                        }
                    }
                }
            }
        }
        if (gv_itemslist.Rows.Count > 0)
        {

            for (int rownum1 = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
            {
                if (gv_itemslist.Rows[rownum1].RowType == DataControlRowType.DataRow)
                {
                    if ((ddl_dsg_state.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text && ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[3].Text && txt_satrtdate.Text == gv_itemslist.Rows[rownum1].Cells[6].Text))
                    {
                        count = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same record already added');", true);
                        return;
                    }
                }
            }
        }
      
        if (DateTime.ParseExact(txt_satrtdate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txt_enddate.Text, "dd/MM/yyyy", null))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start Date Cannot be Grater than  end date')", true);
            txt_satrtdate.Focus();
            return;
        }
        if (gv_itemslist.Rows.Count > 0)
        {
            for (int rownum1 = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
            {
                if (gv_itemslist.Rows[rownum1].RowType == DataControlRowType.DataRow)
                {
                    if (ddl_dsg_state.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text)
                    {
                    DateTime date = DateTime.ParseExact(txt_satrtdate.Text, "dd/MM/yyyy", null);

                    DateTime date1 = DateTime.ParseExact(gv_itemslist.Rows[rownum1].Cells[6].Text, "dd/MM/yyyy", null);
                   // DateTime date2 = DateTime.ParseExact(date1, "dd/MM/yyyy", null);

                    if ((ddl_dsg_state.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text && ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[3].Text && date < date1))
                    {
                        count = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Start Date greater than " + date1.ToString("dd/MM/yyyy",null) + " ');", true);
                        return;
                    }
                    }
                    
                }
            }
        }

        if (count == 0)
        {
            btn_add.Visible = true;
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            gv_itemslist.Visible = true;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("STATE");
            dt.Columns.Add("DESIGNATION");
            dt.Columns.Add("COUNT");
            dt.Columns.Add("HOURS");
            dt.Columns.Add("start_date");
            dt.Columns.Add("end_date");
            dt.Columns.Add("location");
            dt.Columns.Add("category");

            int rownum = 0;
            for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
            {
                if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();

                    dr["STATE"] = gv_itemslist.Rows[rownum].Cells[2].Text;
                    dr["DESIGNATION"] = gv_itemslist.Rows[rownum].Cells[3].Text;
                    dr["COUNT"] = gv_itemslist.Rows[rownum].Cells[4].Text;
                    dr["HOURS"] = gv_itemslist.Rows[rownum].Cells[5].Text;
                    dr["start_date"] = gv_itemslist.Rows[rownum].Cells[6].Text;
                    dr["end_date"] = gv_itemslist.Rows[rownum].Cells[7].Text;
                    dr["location"] = gv_itemslist.Rows[rownum].Cells[8].Text;
                    dr["category"] = gv_itemslist.Rows[rownum].Cells[9].Text;
                    dt.Rows.Add(dr);

                }
            }
            dr = dt.NewRow();
            dr["STATE"] = ddl_dsg_state.SelectedItem.Text;
            dr["DESIGNATION"] = ddl_designation.SelectedItem.Text;
            dr["COUNT"] = txt_emp_count.Text;
            dr["HOURS"] = txt_working_hrs.Text;
            dr["start_date"] = txt_satrtdate.Text;
            dr["end_date"] = txt_enddate.Text;
            dr["location"] = ddl_location.SelectedItem.Text;
            dr["category"] = ddl_categories.SelectedItem.Text;
            dt.Rows.Add(dr);
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();

            ViewState["CurrentTable"] = dt;

            if (dt.Rows.Count > 0)
            {
                ddl_gst_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[0].ToString());
                ddl_gst_state.DataTextField = dt.Columns[0].ToString();
                ddl_gst_state.DataValueField = dt.Columns[0].ToString();
                ddl_gst_state.DataBind();

                ddl_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[0].ToString());
                ddl_state.DataTextField = dt.Columns[0].ToString();
                ddl_state.DataValueField = dt.Columns[0].ToString();
                ddl_state.DataBind();

                ddl_gst_state.Items.Insert(0, new ListItem("Select"));
                ddl_state.Items.Insert(0, new ListItem("ALL"));
            }

            ddl_dsg_state.SelectedIndex = 0;
            ddl_designation.SelectedIndex = 0;
            txt_emp_count.Text = "0";
            txt_working_hrs.Text = "0";
            txt_satrtdate.Text = "";
            txt_enddate.Text = "";
            ddl_location.SelectedIndex = 0;
            ddl_categories.SelectedIndex = 0;
           // btn_add.Visible = false;
        }
    }

    protected void lnk_add_Comp_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //vikas 08/05/2019
        string gst = d.getsinglestring("Select `Companyname_gst_no` from pay_company_group where Companyname_gst_no like '%" + txt_comp_gst_no.Text + "%'");
        if (gst == "")
        {
            btn_add.Visible = true;
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            gv_comp_group.Visible = true;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("comp_name");
            dt.Columns.Add("Companyname_gst_no");
            dt.Columns.Add("gst_address");

            foreach (GridViewRow row in gv_comp_group.Rows)
            {
                string gststate = row.Cells[3].Text;

                if (txt_comp_gst_no.Text.Equals(gststate))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert to same GST Number!!')", true);
                    return;
                }
            }

            int rownum = 0;
            for (rownum = 0; rownum < gv_comp_group.Rows.Count; rownum++)
            {
                if (gv_comp_group.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();

                    dr["comp_name"] = gv_comp_group.Rows[rownum].Cells[2].Text;
                    dr["Companyname_gst_no"] = gv_comp_group.Rows[rownum].Cells[3].Text;
                    dr["gst_address"] = gv_comp_group.Rows[rownum].Cells[4].Text;

                    dt.Rows.Add(dr);

                }
            }
            dr = dt.NewRow();
            dr["comp_name"] = txt_company_name.Text;
            dr["Companyname_gst_no"] = txt_comp_gst_no.Text;
            dr["gst_address"] = txt_gst_address.Text;

            dt.Rows.Add(dr);
            gv_comp_group.DataSource = dt;
            gv_comp_group.DataBind();

            ViewState["Comp_Group"] = dt;
            txt_company_name.Text = "";
            txt_comp_gst_no.Text = "";
            txt_gst_address.Text = "";
            //if (dt.Rows.Count > 0)
            //{
            //    ddl_gst_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[0].ToString());
            //    ddl_gst_state.DataTextField = dt.Columns[0].ToString();
            //    ddl_gst_state.DataValueField = dt.Columns[0].ToString();
            //    ddl_gst_state.DataBind();
            //    ddl_gst_state.Items.Insert(0, new ListItem("Select"));
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('GST No Already Exists !!!')", true);
        }

    }

    protected void lnk_add_billing_Click(object sender, EventArgs e)
    {
        hidtab.Value = "11";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (gv_itemslist.Rows.Count > 0)
        {
            int count = 0;
            for (int rownum1 = 0; rownum1 < gv_billing_type.Rows.Count; rownum1++)
            {
                if (gv_billing_type.Rows[rownum1].RowType == DataControlRowType.DataRow)
                {
                    if ((ddl_checklist_name.SelectedItem.Text == gv_billing_type.Rows[rownum1].Cells[2].Text && ddl_checklist_billing.SelectedItem.Text == gv_billing_type.Rows[rownum1].Cells[4].Text && ddl_state.SelectedItem.Text == gv_billing_type.Rows[rownum1].Cells[6].Text))
                    {
                        count = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Billing already added');", true);
                        return;
                    }
                }
            }

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("checklist_name");
            dt.Columns.Add("checklist_number");
            dt.Columns.Add("checklist_billing");
            dt.Columns.Add("checklist_billingNo");
            dt.Columns.Add("state");
            dt.Columns.Add("invoice_shipping_address");

            int rownum = 0;
            for (rownum = 0; rownum < gv_billing_type.Rows.Count; rownum++)
            {
                if (gv_billing_type.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();

                    dr["checklist_name"] = gv_billing_type.Rows[rownum].Cells[2].Text;
                    dr["checklist_number"] = gv_billing_type.Rows[rownum].Cells[3].Text;
                    dr["checklist_billing"] = gv_billing_type.Rows[rownum].Cells[4].Text;
                    dr["checklist_billingNo"] = gv_billing_type.Rows[rownum].Cells[5].Text;
                    dr["state"] = gv_billing_type.Rows[rownum].Cells[6].Text;
                    dr["invoice_shipping_address"] = gv_billing_type.Rows[rownum].Cells[7].Text;
                    dt.Rows.Add(dr);

                }
            }

            if (ddl_state.SelectedValue.Equals("ALL"))
            {
                foreach (ListItem li in ddl_state.Items)
                {
                    if (li.Text != "ALL")
                    {
                        dr = dt.NewRow();
                        dr["checklist_name"] = ddl_checklist_name.SelectedItem;
                        dr["checklist_number"] = ddl_checklist_name.SelectedValue;
                        dr["checklist_billing"] = ddl_checklist_billing.SelectedItem;
                        dr["checklist_billingNo"] = ddl_checklist_billing.SelectedValue;
                        dr["invoice_shipping_address"] = inv_shipping_add.Text;
                        dr["state"] = li;

                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                dr = dt.NewRow();
                dr["checklist_name"] = ddl_checklist_name.SelectedItem;
                dr["checklist_number"] = ddl_checklist_name.SelectedValue;
                dr["checklist_billing"] = ddl_checklist_billing.SelectedItem;
                dr["checklist_billingNo"] = ddl_checklist_billing.SelectedValue;
                dr["state"] = ddl_state.SelectedItem;
                dr["invoice_shipping_address"] = inv_shipping_add.Text;

                dt.Rows.Add(dr);
            }
            gv_billing_type.DataSource = dt;
            gv_billing_type.DataBind();

            ViewState["Billing_Type"] = dt;
            ddl_checklist_name.SelectedIndex = 0;
            ddl_checklist_billing.SelectedIndex = 0;
            ddl_state.SelectedIndex = 0;
            inv_shipping_add.Text = "";

            //if (dt.Rows.Count > 0)
            //{
            //    ddl_gst_state.DataSource = dt.DefaultView.ToTable(true, dt.Columns[0].ToString());
            //    ddl_gst_state.DataTextField = dt.Columns[0].ToString();
            //    ddl_gst_state.DataValueField = dt.Columns[0].ToString();
            //    ddl_gst_state.DataBind();
            //    ddl_gst_state.Items.Insert(0, new ListItem("Select"));
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please insert at list one record in designation');", true);
        }

    }

    protected void gv_comp_group_RowDataBound(object sender, GridViewRowEventArgs e)
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
        e.Row.Cells[3].Visible = false;
        // e.Row.Cells[5].Visible = false;
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
    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
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

            ddl_gst_state.DataSource = dt;
            ddl_gst_state.DataTextField = dt.Columns[0].ToString();
            ddl_gst_state.DataValueField = dt.Columns[0].ToString();
            ddl_gst_state.DataBind();
            ddl_gst_state.Items.Insert(0, new ListItem("Select"));

            ddl_state.DataSource = dt;
            ddl_state.DataTextField = dt.Columns[0].ToString();
            ddl_state.DataValueField = dt.Columns[0].ToString();
            ddl_state.DataBind();
            ddl_state.Items.Insert(0, new ListItem("ALL"));

        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_delete.Visible = true;
        btn_edit.Visible = true;
        btn_add.Visible = false;
        upload_documents(document1_file, txt_document1.Text, "_doc");
    }
    private void upload_documents(FileUpload document_file, string file_name, string file1)
    {
        if (document_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(document_file.FileName);
            if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".pdf")
            {
                string fileName = Path.GetFileName(document_file.PostedFile.FileName);
                document_file.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + Session["COMP_CODE"].ToString() + "_" + txt_clientcode.Text + "_" + txt_document1.Text.Replace(" ", "_") + fileExt, true);
                File.Delete(Server.MapPath("~/Images/") + fileName);

                d.operation("insert into pay_images (comp_code,client_code,description,file_name,start_date,end_date,created_by,created_date) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','" + txt_document1.Text + "','" + Session["COMP_CODE"].ToString() + "_" + txt_clientcode.Text + "_" + txt_document1.Text.Replace(" ", "_") + fileExt + "',str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Document !!')", true);
        }
        load_grdview();
    }
    private void load_grdview()
    {
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,client_code, description,concat('~/Images/',file_name) as Value,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date FROM pay_images where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and client_code= '" + Session["CLIENT_CODE"].ToString() + "'", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        grd_company_files.DataSource = DS1;
        grd_company_files.DataBind();
        txt_document1.Text = "";
        txt_from_date.Text = "";
        txt_to_date.Text = "";

    }
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
        hidtab.Value = "0";
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
    }
    protected void grd_company_files_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item = (int)grd_company_files.DataKeys[e.RowIndex].Value;
        string temp = d.getsinglestring("SELECT file_name FROM pay_images WHERE id=" + item);
        if (temp != "")
        {
            File.Delete(Server.MapPath("~/Images/") + temp);
        }
        d.operation("delete from pay_images WHERE id=" + item);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
        load_grdview();

        if (reason_panel.Visible == true)
        {
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            btnclose.Visible = true;
            btn_add.Visible = false;

        }
        else
        {

            btn_edit.Visible = false;
            btn_delete.Visible = false;
            btnclose.Visible = true;
            btn_add.Visible = true;

        }
    }

    public void text_clear()
    {
        txt_clientname.Text = "";
        txt_clientcode.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
        txt_client_address1.Text = "";
        txt_client_address2.Text = "";
        ddl_client_state.SelectedValue = "Select";
        ddl_client_city.SelectedValue = "Select";
        ddl_checklist_name.SelectedValue = "Select";
        ddl_checklist_billing.SelectedValue = "Select";
        txt_file_no.Text = "";
        txt_budget_material.Text = "0";
        txt_branch_count.Text = "0";
        txt_reg_no.Text = "";
        txt_gst_no.Text = "";
        txt_pan_no.Text = "";
        txt_license.Text = "";
        txt_bank_detail.Text = "";
        txt_website.Text = "";
        ddl_designation.SelectedValue = "Select";
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        ddl_state.Items.Clear();
        grd_company_files.DataSource = null;
        grd_company_files.DataBind();
        txt_emp_count.Text = "0";
        txt_working_hrs.Text = "0";
        ddl_iot_applicable.SelectedValue = "0";
        ddl_ot_applicable.SelectedValue = "0";
        txt_phoneno.Text = "";
        txt_clientemailid.Text = "";
        txt_password.Text = "";
        gv_head_type.DataSource = null;
        gv_head_type.DataBind();
        gv_emailsend.DataSource = null;
        gv_emailsend.DataBind();
        txt_employee_total.Text = "";
        gv_clientItems.DataSource = null;
        gv_clientItems.DataBind();
        gv_billing_type.DataSource = null;
        gv_billing_type.DataBind();
        gv_comp_group.DataSource = null;
        gv_comp_group.DataBind();

        gv_fire_ext.DataSource = null;
        gv_fire_ext.DataBind();


        if (txt_clientcode.Text == "Credence")
        {
            bill.Visible = true;
        }
        else
        {
            bill.Visible = false;
        }
        txt_bill_amount.Text = "0";
        ddl_gst_applicable.SelectedValue = "0";

        txt_material_days.Text = "0";
        ddl_material_calc.SelectedValue = "0";
        txt_comp_ac_no.Text = "";
        ddl_android_attendances_flag.SelectedValue = "select";
      //  ddl_bank.SelectedValue = "Select";

        ddl_company_bank.SelectedIndex = 0;
        //vikas add 16-07-2019
        grv_dwduction.DataSource = null;
        grv_dwduction.DataBind();
        //txt_op_name.Text = "";
        //txt_op_mobile.Text = "";
        //txt_op_email.Text = "";
        //txt_fn_name.Text = "";
        //txt_fn_mobile.Text = "";
        //txt_fn_email.Text = "";
        //txt_lc_name.Text = "";
        //txt_lc_mobile.Text = "";
        //txt_lc_email.Text = "";
        //txt_oth_name.Text = "";
        //txt_oth_mobile.Text = "";
        //txt_oth_email.Text = "";

        //txt_znop_name.Text = "";
        //txt_znop_mobile.Text = "";
        //txt_znop_email.Text = "";
        //txt_znfn_name.Text = "";
        //txt_znfn_mobile.Text = "";
        //txt_znfn_email.Text = "";
        //txt_znlc_name.Text = "";
        //txt_znlc_mobile.Text = "";
        //txt_znlc_email.Text = "";
        //txt_znoth_name.Text = "";
        //txt_znoth_mobile.Text = "";
        //txt_znoth_email.Text = "";

        //txt_rgop_name.Text = "";
        //txt_rgop_mobile.Text = "";
        //txt_rgop_email.Text = "";
        //txt_rgfn_name.Text = "";
        //txt_rgfn_mobile.Text = "";
        //txt_rgfn_email.Text = "";
        //txt_rglc_name.Text = "";
        //txt_rglc_mobile.Text = "";
        //txt_rglc_email.Text = "";
        //txt_rgoth_name.Text = "";
        //txt_rgoth_mobile.Text = "";
        //txt_rgoth_email.Text = "";
        txt_reason_updation.Text = "";
        // ddl_ot_applicable_YN.SelectedValue = "NO";
        // ddl_ot_daily.SelectedValue = "0";
        ddl_gst_payed.SelectedValue = "select";
        gv_regional_zone.DataSource = null;
        gv_regional_zone.DataBind();

        gv_zone_add.DataSource = null;
        gv_zone_add.DataBind();

        gv_statewise_gst.DataSource = null;
        gv_statewise_gst.DataBind();
        gv_clientItems.DataSource = null;
        gv_clientItems.DataBind();
        gv_emailsend.DataSource = null;
        gv_emailsend.DataBind();
        gv_services.DataSource = null;
        gv_services.DataBind();
        l1.Visible = false;
        ddl_bank.SelectedIndex = 0;
        ddl_ot_billing.SelectedIndex = 0;
        ddl_tds_persent.SelectedIndex = 0;
        ddl_tds_on.SelectedIndex = 0;
        ddl_tds_applicable.SelectedIndex = 0;
        ddl_machine_rent_p.SelectedIndex = 0;
        ddl_gst_payed.SelectedIndex = 0;
        txt_penalty.Text = "0";
        ddl_material_calc.SelectedIndex = 0;
        ddl_service.SelectedIndex = 0;
        ddl_admin_expence.SelectedIndex = 0;
    }
    protected void ClientGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            // ClientGridView.UseAccessibleHeader = false;
            ClientGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }

    protected void lnk_zone_add_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //MD change
        string region_id = d.getsinglestring("select login_id from   pay_user_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND client_code='" + Session["client_code"].ToString() + "' AND  client_zone='" + ddl_zone.SelectedItem.Text + "' AND zone_region is null ");

        int rownum1 = 0, Num = 0;
        if (region_id == "")
        {
            for (rownum1 = 0, Num = 0; rownum1 < gv_zone_add.Rows.Count; rownum1++)
            {
                if (gv_zone_add.Rows[rownum1].Cells[3].Text == ddl_zone.SelectedValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Can Not Create Same Zone in ...');", true);
                    ddl_zone.Focus();
                    Num = 1;
                    break;
                }
            }
        }
        if (region_id == "" && Num == 0)
        {
            btn_add.Visible = true;
            btn_edit.Visible = true;
            btn_delete.Visible = true;

            gv_zone_add.Visible = true;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Field1");
            dt.Columns.Add("ZONE");
            dt.Columns.Add("Field11");
            dt.Columns.Add("Field2");
            dt.Columns.Add("Field3");
            dt.Columns.Add("Field4");
            dt.Columns.Add("Field5");
            dt.Columns.Add("Field6");
            dt.Columns.Add("Field7");
            dt.Columns.Add("Field8");
            dt.Columns.Add("Field9");
            dt.Columns.Add("Field10");
            dt.Columns.Add("Field12");
            dt.Columns.Add("Field13");


            int rownum = 0;
            for (rownum = 0; rownum < gv_zone_add.Rows.Count; rownum++)
            {
                if (gv_zone_add.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();

                    dr["Field1"] = gv_zone_add.Rows[rownum].Cells[2].Text;
                    dr["ZONE"] = gv_zone_add.Rows[rownum].Cells[3].Text;

                    dr["Field11"] = gv_zone_add.Rows[rownum].Cells[4].Text;
                    dr["Field2"] = gv_zone_add.Rows[rownum].Cells[5].Text;
                    dr["Field3"] = gv_zone_add.Rows[rownum].Cells[6].Text;
                    dr["Field4"] = gv_zone_add.Rows[rownum].Cells[7].Text;
                    dr["Field5"] = gv_zone_add.Rows[rownum].Cells[8].Text;
                    dr["Field6"] = gv_zone_add.Rows[rownum].Cells[9].Text;
                    dr["Field7"] = gv_zone_add.Rows[rownum].Cells[10].Text;
                    dr["Field8"] = gv_zone_add.Rows[rownum].Cells[11].Text;
                    dr["Field9"] = gv_zone_add.Rows[rownum].Cells[12].Text;
                    dr["Field10"] = gv_zone_add.Rows[rownum].Cells[13].Text;
                    dr["Field12"] = gv_zone_add.Rows[rownum].Cells[14].Text;
                    dr["Field13"] = gv_zone_add.Rows[rownum].Cells[15].Text;

                    dt.Rows.Add(dr);
                }
            }
            dr = dt.NewRow();
            dr["Field1"] = ddl_zone_head.SelectedValue;
            dr["ZONE"] = ddl_zone.SelectedValue;

            dr["Field11"] = ddl_zn_title.SelectedValue;
            dr["Field2"] = txt_zn_head_name.Text;
            dr["Field3"] = txt_zn_head_mobile.Text;
            dr["Field4"] = txt_zn_head_email.Text;
            dr["Field5"] = txt_zn_head_birthdate.Text;
            dr["Field6"] = txt_zn_anniversary.Text;
            dr["Field7"] = txt_zn_child1.Text;
            dr["Field8"] = txt_zn_ch1bday.Text;
            dr["Field9"] = txt_zn_child2.Text;
            dr["Field10"] = txt_zn_ch2bday.Text;
            dr["Field12"] = personal_mobile_no_zonehead.Text;
            dr["Field13"] = personal_mail_id_zonehead.Text;

            dt.Rows.Add(dr);
            gv_zone_add.DataSource = dt;
            gv_zone_add.DataBind();
            ViewState["zonetable"] = dt;

            ddl_zone.SelectedValue = "0";
            ddl_zone_head.SelectedValue = "Select";
            ddl_zn_title.SelectedValue = "0";
            txt_zn_head_name.Text = "";
            txt_zn_head_mobile.Text = "";
            txt_zn_head_email.Text = "";
            txt_zn_head_birthdate.Text = "";
            txt_zn_anniversary.Text = "";
            txt_zn_child1.Text = "";
            txt_zn_ch1bday.Text = "";
            txt_zn_child2.Text = "";
            txt_zn_ch2bday.Text = "";
            personal_mobile_no_zonehead.Text = "";
            personal_mail_id_zonehead.Text = "";
            //txt_znlc_email.Text = "";
            //txt_znoth_name.Text = "";
            //txt_znoth_mobile.Text = "";
            //txt_znoth_email.Text = "";
        }
        else
        {
            if (Num == 1) { }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same  Zone!!')", true);
            }
        }
    }
    protected void lnk_remove_zone_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["zonetable"] != null)
        {
            DataTable dt = (DataTable)ViewState["zonetable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["zonetable"] = dt;
            gv_zone_add.DataSource = dt;
            gv_zone_add.DataBind();
        }
    }
    protected void gv_zone_add_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_regional_zone_add_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //MD change
        string region_id = d.getsinglestring("select login_id from   pay_user_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND client_code='" + Session["client_code"].ToString() + "' AND  client_zone='" + ddl_rgn_zone.SelectedItem.Text + "' AND zone_region='" + txt_region.Text + "' ");

        int rownum1 = 0, Num = 0;
        if (region_id == "")
        {

            for (rownum1 = 0, Num = 0; rownum1 < gv_regional_zone.Rows.Count; rownum1++)
            {
                if (gv_regional_zone.Rows[rownum1].Cells[3].Text == ddl_rgn_zone.SelectedValue && gv_regional_zone.Rows[rownum1].Cells[4].Text == txt_region.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Can Not Create Same Region in Zone...');", true);
                    txt_region.Focus();
                    Num = 1;
                    break;
                }
            }
        }
        if (region_id == "" && Num == 0)
        {
            //btn_add.Visible = true;
            //btn_edit.Visible = true;
            //btn_delete.Visible = true;
            gv_regional_zone.Visible = true;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("ZONE");
            dt.Columns.Add("REGION");
            dt.Columns.Add("Field1");
            dt.Columns.Add("Field2");
            dt.Columns.Add("Field3");
            dt.Columns.Add("Field4");
            dt.Columns.Add("Field5");
            dt.Columns.Add("Field6");
            dt.Columns.Add("Field7");
            dt.Columns.Add("Field8");
            dt.Columns.Add("Field9");
            dt.Columns.Add("Field10");
            dt.Columns.Add("Field11");
            dt.Columns.Add("Field12");
            dt.Columns.Add("Field13");


            int rownum = 0;
            for (rownum = 0; rownum < gv_regional_zone.Rows.Count; rownum++)
            {
                if (gv_regional_zone.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();
                    dr["Field1"] = gv_regional_zone.Rows[rownum].Cells[2].Text;
                    dr["ZONE"] = gv_regional_zone.Rows[rownum].Cells[3].Text;

                    dr["REGION"] = gv_regional_zone.Rows[rownum].Cells[4].Text;
                    dr["Field2"] = gv_regional_zone.Rows[rownum].Cells[5].Text;
                    dr["Field3"] = gv_regional_zone.Rows[rownum].Cells[6].Text;
                    dr["Field4"] = gv_regional_zone.Rows[rownum].Cells[7].Text;
                    dr["Field5"] = gv_regional_zone.Rows[rownum].Cells[8].Text;
                    dr["Field6"] = gv_regional_zone.Rows[rownum].Cells[9].Text;
                    dr["Field7"] = gv_regional_zone.Rows[rownum].Cells[10].Text;
                    dr["Field8"] = gv_regional_zone.Rows[rownum].Cells[11].Text;
                    dr["Field9"] = gv_regional_zone.Rows[rownum].Cells[12].Text;
                    dr["Field10"] = gv_regional_zone.Rows[rownum].Cells[13].Text;
                    dr["Field11"] = gv_regional_zone.Rows[rownum].Cells[14].Text;
                    dr["Field12"] = gv_regional_zone.Rows[rownum].Cells[15].Text;
                    dr["Field13"] = gv_regional_zone.Rows[rownum].Cells[16].Text;


                    dt.Rows.Add(dr);
                }
            }
            dr = dt.NewRow();
            dr["Field1"] = ddl_region_head.SelectedValue;
            dr["ZONE"] = ddl_rgn_zone.SelectedValue;
            dr["REGION"] = txt_region.Text;

            dr["Field2"] = ddl_rgn_title.SelectedValue;
            dr["Field3"] = txt_rgn_head_name.Text;
            dr["Field4"] = txt_rgn_head_mobile.Text;
            dr["Field5"] = txt_rgn_head_email.Text;
            dr["Field6"] = txt_rgn_head_birthdate.Text;
            dr["Field7"] = txt_rgn_anniversary.Text;
            dr["Field8"] = txt_rgn_child1.Text;
            dr["Field9"] = txt_rgn_ch1bday.Text;
            dr["Field10"] = txt_rgn_child2.Text;
            dr["Field11"] = txt_rgn_ch2bday.Text;
            dr["Field12"] = personal_mobile_no_regionhead.Text;
            dr["Field13"] = personal_mail_id_regionhead.Text;


            dt.Rows.Add(dr);
            gv_regional_zone.DataSource = dt;
            gv_regional_zone.DataBind();

            ViewState["regiontable"] = dt;

            ddl_rgn_zone.SelectedValue = "0";
            txt_region.Text = "";
            ddl_region_head.SelectedValue = "Select";
            txt_rgn_head_name.Text = "";
            txt_rgn_head_mobile.Text = "";
            txt_rgn_head_email.Text = "";
            txt_rgn_head_birthdate.Text = "";
            txt_rgn_anniversary.Text = "";
            txt_rgn_child1.Text = "";
            txt_rgn_ch1bday.Text = "";
            txt_rgn_child2.Text = "";
            txt_rgn_ch2bday.Text = "";
            ddl_rgn_title.SelectedValue = "0";
            personal_mail_id_regionhead.Text = "";
            personal_mobile_no_regionhead.Text = "";
        }
        else
        {
            if (Num == 1) { }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same Region Name   for one Zone!!')", true);
            }
        }
    }
    protected void gv_regional_zone_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_remove_rgzone_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["regiontable"] != null)
        {
            DataTable dt = (DataTable)ViewState["regiontable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["regiontable"] = dt;
            gv_regional_zone.DataSource = dt;
            gv_regional_zone.DataBind();
        }
    }
    protected void btn_approval_Click(object sender, EventArgs e)
    {
        d.operation("update pay_client_master set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%'");

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT id from pay_client_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and (approval = '' OR approval is null) order by id", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr_item1);
            int numberOfResults = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (numberOfResults != 1)
                {
                    d.operation("delete from pay_client_master where id = " + dr["id"].ToString());
                    numberOfResults = numberOfResults - 1;
                }
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        loadclientgrid();
    }
    private void load_reporting_grdv()
    {
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT CLIENT_NAME AS 'CLIENT NAME',CLIENT_CODE,(SELECT DISTINCT(STATE_NAME) FROM pay_state_master WHERE STATE_CODE = pay_client_master.STATE) AS STATE,CITY,AG_START_DATE AS 'START DATE',AG_END_DATE AS 'END DATE',id as ID FROM pay_client_master where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "' ORDER BY client_CODE", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        GridView1.DataSource = DS1;
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item = (int)GridView1.DataKeys[e.RowIndex].Value;
        d.operation("delete from pay_client_master WHERE id=" + item);
        loadclientgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["CLIENT_CODE"] = GridView1.Rows[e.NewEditIndex].Cells[3].Text;
        load_fields(GridView1.Rows[e.NewEditIndex].Cells[3].Text, 2);
        btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true;
        load_reporting_grdv();
    }
    public string reason_updt(string reason, int type)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (type == 1)
        {
            int counter = 0;
            lbx_reason_updation.Items.Clear();
            while (reason.Contains("@#$%"))
            {
                if (!reason.Equals("@#$%"))
                {
                    string listitem = reason.Substring(0, reason.IndexOf("@#$%"));
                    lbx_reason_updation.Items.Add(new ListItem(++counter + ".  " + listitem));
                    reason = reason.Substring(reason.IndexOf("@#$%") + 4, reason.Length - (reason.IndexOf("@#$%") + 4));
                }
                else
                {
                    reason = "";
                }
            }
        }
        else if (type == 2)
        {
            for (int i = 0; i < lbx_reason_updation.Items.Count; i++)
            {
                if (i <= 9)
                {
                    sb.Append(lbx_reason_updation.Items[i].Text.Substring(3, lbx_reason_updation.Items[i].Text.Length - 3) + "@#$%");
                }
                else
                {
                    sb.Append(lbx_reason_updation.Items[i].Text.Substring(4, lbx_reason_updation.Items[i].Text.Length - 4) + "@#$%");
                }
            }
            sb.Append(txt_reason_updation.Text + "@#$%");
        }
        return sb.ToString();
    }

    protected void lnk_add_gst_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string gst = d.getsinglestring("Select `Field2` from pay_zone_master where  type='GST' and Field2 like '%" + txt_gst_no.Text + "%'");
        if (gst == "")
        {

            DataTable dt_gst = new DataTable();
            DataRow dr_gst;
            dt_gst.Columns.Add("REGION");
            dt_gst.Columns.Add("Field1");
            dt_gst.Columns.Add("Field2");


            foreach (GridViewRow row in gv_statewise_gst.Rows)
            {
                Label lbl_gststate = (Label)row.FindControl("lbl_gststate");
                string gststate = (lbl_gststate.Text);

                if (ddl_gst_state.SelectedItem.Text.Equals(gststate))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same GST State for one Company!!')", true);
                    return;
                }
            }

            foreach (GridViewRow row in gv_statewise_gst.Rows)
            {
                Label lbl_gstin = (Label)row.FindControl("lbl_gstin");
                string gstin = lbl_gstin.Text.ToString();
                if (txt_gst_no.Text.Equals(gstin))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same GST Number for one Company!!')", true);
                    return;
                }
            }




            int rownum = 0;
            for (rownum = 0; rownum < gv_statewise_gst.Rows.Count; rownum++)
            {
                if (gv_statewise_gst.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr_gst = dt_gst.NewRow();

                    Label lbl_gststate = (Label)gv_statewise_gst.Rows[rownum].Cells[2].FindControl("lbl_gststate");
                    dr_gst["REGION"] = lbl_gststate.Text.ToString();
                    Label gst_adddr = (Label)gv_statewise_gst.Rows[rownum].Cells[3].FindControl("lbl_gst_addr");
                    dr_gst["Field1"] = gst_adddr.Text.ToString();
                    Label gstin = (Label)gv_statewise_gst.Rows[rownum].Cells[4].FindControl("lbl_gstin");
                    dr_gst["Field2"] = gstin.Text.ToString();

                    dt_gst.Rows.Add(dr_gst);
                }
            }

            dr_gst = dt_gst.NewRow();

            dr_gst["REGION"] = ddl_gst_state.SelectedItem.Text;
            dr_gst["Field1"] = txt_gst_addr.Text;
            dr_gst["Field2"] = txt_gst_no.Text;

            dt_gst.Rows.Add(dr_gst);
            gv_statewise_gst.DataSource = dt_gst;
            gv_statewise_gst.DataBind();

            ViewState["gsttable"] = dt_gst;

            ddl_gst_state.SelectedIndex = 0;
            txt_gst_addr.Text = "";
            txt_gst_no.Text = "";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('GST No Already Exists !!!')", true);
        }

    }

    protected void gv_services_RowDataBound(object sender, GridViewRowEventArgs e)
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
        e.Row.Cells[5].Visible=false;
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
    protected void lnkbtn_gst_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["gsttable"] != null)
        {
            DataTable dt = (DataTable)ViewState["gsttable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["gsttable"] = dt;
            gv_statewise_gst.DataSource = dt;
            gv_statewise_gst.DataBind();
        }
    }
    protected void lnkbtn_services_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        GridViewRow row1 = gv_services.Rows[row.RowIndex];
        Label MyLabel = (Label)row1.FindControl("ID");
        string abc = MyLabel.Text;

        d.operation("delete from pay_zone_master where id='" + abc + "'");

        category_gv();

        //if (ViewState["servicestable"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["servicestable"];
        //    if (dt.Rows.Count >= 1)
        //    {
        //        if (row.RowIndex < dt.Rows.Count)
        //        {
        //            dt.Rows.Remove(dt.Rows[rowID]);
        //        }
        //    }
        //    ViewState["servicestable"] = dt;
        //    gv_services.DataSource = dt;
        //    gv_services.DataBind();
        //}
    }
    protected void lnk_service_category_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        string category = d.getsinglestring(" SELECT `Field1` FROM pay_zone_master WHERE client_code ='" + txt_clientcode.Text + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Field1='" + dddl_services.SelectedItem + "'  and Type = 'SERVICES'");

        if (category.ToString() == dddl_services.SelectedItem.Text)
          {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Same Service Category not Added !!!')", true);
                      return;
           }
      
       // d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and Type = 'SERVICES'");


        d.operation("Insert Into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,Field2,Field3) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_clientcode.Text + "','SERVICES','" + dddl_services.SelectedItem + "','" + txt_staredate.Text + "','" + txt_enddate1.Text+ "')");
        category_gv();


        //DataTable dt_gst = new DataTable();
        //DataRow dr_gst;
        //dt_gst.Columns.Add("Field1");
        //dt_gst.Columns.Add("Field2");
        //dt_gst.Columns.Add("Field3");

        //int rownum = 0;
        //for (rownum = 0; rownum < gv_services.Rows.Count; rownum++)
        //{
        //    if (gv_services.Rows[rownum].RowType == DataControlRowType.DataRow)
        //    {
        //        dr_gst = dt_gst.NewRow();

        //        Label lbl_gststate = (Label)gv_services.Rows[rownum].Cells[2].FindControl("lbl_servicestype");
        //        dr_gst["Field1"] = lbl_gststate.Text.ToString();
        //        Label gst_adddr = (Label)gv_services.Rows[rownum].Cells[3].FindControl("lbl_lnkstartdate");
        //        dr_gst["Field2"] = gst_adddr.Text.ToString();
        //        Label gstin = (Label)gv_services.Rows[rownum].Cells[4].FindControl("lbl_enddate");
        //        dr_gst["Field3"] = gstin.Text.ToString();
        //        if (lbl_gststate.Text.ToString() == dddl_services.SelectedItem.Text)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Same Service Category not Added !!!')", true);

        //            return;
        //        }
        //        dt_gst.Rows.Add(dr_gst);
        //    }
        //}

        //dr_gst = dt_gst.NewRow();

        //dr_gst["Field1"] = dddl_services.SelectedItem.Text;
       // dr_gst["Field2"] = txt_staredate.Text;
        //dr_gst["Field3"] = txt_enddate1.Text;

       // dt_gst.Rows.Add(dr_gst);
        //gv_services.DataSource = dt_gst;
        //gv_services.DataBind();

        //ViewState["servicestable"] = dt_gst;

        dddl_services.SelectedIndex = 0;
        txt_staredate.Text = "";
        txt_enddate1.Text = "";
    }

    protected void lnk_add_head_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_head_type.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Field1");
        dt.Columns.Add("Field2");
        dt.Columns.Add("Field3");
        dt.Columns.Add("Field4");
        dt.Columns.Add("Field5");
        dt.Columns.Add("Field6");
        dt.Columns.Add("Field7");
        dt.Columns.Add("Field8");
        dt.Columns.Add("Field9");
        dt.Columns.Add("Field10");
        dt.Columns.Add("Field11");
        dt.Columns.Add("Field12");
        dt.Columns.Add("Field13");
        int rownum = 0;
        for (rownum = 0; rownum < gv_head_type.Rows.Count; rownum++)
        {
            if (gv_head_type.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["Field10"] = gv_head_type.Rows[rownum].Cells[2].Text;
                dr["Field2"] = gv_head_type.Rows[rownum].Cells[3].Text;
                dr["Field1"] = gv_head_type.Rows[rownum].Cells[4].Text;


                dr["Field3"] = gv_head_type.Rows[rownum].Cells[5].Text;
                dr["Field4"] = gv_head_type.Rows[rownum].Cells[6].Text;
                dr["Field5"] = gv_head_type.Rows[rownum].Cells[7].Text;
                dr["Field6"] = gv_head_type.Rows[rownum].Cells[8].Text;
                dr["Field7"] = gv_head_type.Rows[rownum].Cells[9].Text;
                dr["Field8"] = gv_head_type.Rows[rownum].Cells[10].Text;
                dr["Field9"] = gv_head_type.Rows[rownum].Cells[11].Text;
                dr["Field11"] = gv_head_type.Rows[rownum].Cells[12].Text;
                dr["Field12"] = gv_head_type.Rows[rownum].Cells[13].Text;
                dr["Field13"] = gv_head_type.Rows[rownum].Cells[14].Text;





                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["Field10"] = ddl_head_type.SelectedValue;
        dr["Field2"] = ddl_head_title.Text;
        dr["Field1"] = txt_head_name.Text;

        dr["Field3"] = txt_head_mobile.Text;
        dr["Field4"] = txt_head_email.Text;
        dr["Field5"] = txt_head_birthdate.Text;
        dr["Field6"] = txt_anniversary.Text;
        dr["Field7"] = txt_child1.Text;
        dr["Field8"] = txt_ch1bday.Text;
        dr["Field9"] = txt_child2.Text;
        dr["Field11"] = txt_ch2bday.Text;
        dr["Field12"] = personal_mobile_no_head.Text;
        dr["Field13"] = personal_mail_id_head.Text;
        dt.Rows.Add(dr);
        gv_head_type.DataSource = dt;
        gv_head_type.DataBind();

        ViewState["headtable"] = dt;

        ddl_head_type.SelectedValue = "Select";
        txt_head_name.Text = "";
        txt_head_mobile.Text = "";
        txt_head_email.Text = "";
        txt_head_birthdate.Text = "";
        txt_anniversary.Text = "";
        txt_child1.Text = "";
        txt_ch1bday.Text = "";
        txt_child2.Text = "";
        txt_ch2bday.Text = "";
        ddl_head_title.SelectedValue = "0";
        personal_mobile_no_head.Text = "";
        personal_mail_id_head.Text = "";
    }
    protected void gv_head_type_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_remove_head_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            DataTable dt = (DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_head_type.DataSource = dt;
            gv_head_type.DataBind();
        }
    }
    protected void lnk_remove_mail_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["emailtable"] != null)
        {
            DataTable dt = (DataTable)ViewState["emailtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["emailtable"] = dt;
            gv_emailsend.DataSource = dt;
            gv_emailsend.DataBind();
        }
    }

    private bool count_emp_count()
    {
        try
        {
            if (int.Parse(txt_emp_count.Text) > int.Parse(txt_employee_total.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Increase The Value Of Total Number Of Employee!!')", true);
                txt_emp_count.Text = "0";
                txt_emp_count.Focus();
                return false;
            }
            int sum = 0;
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                sum = sum + int.Parse(row.Cells[4].Text);
                int sumnew = sum + int.Parse(txt_emp_count.Text);
                int a = int.Parse(txt_employee_total.Text);
                if (sumnew > a)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Increase The Value Of Total Number Of Employee!!')", true);
                    txt_emp_count.Text = "0";
                    txt_emp_count.Focus();
                    return false;
                }
            }
            return true;
        }
        catch { }
        return true;
    }


    protected void GridView1_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            GridView1.UseAccessibleHeader = false;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_services_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_services.UseAccessibleHeader = false;
            gv_services.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_statewise_gst_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_statewise_gst.UseAccessibleHeader = false;
            gv_statewise_gst.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_regional_zone_gst_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_regional_zone.UseAccessibleHeader = false;
            gv_regional_zone.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_zone_add_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_zone_add.UseAccessibleHeader = false;
            gv_zone_add.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_head_type_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_head_type.UseAccessibleHeader = false;
            gv_head_type.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
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
    protected void gv_itemslist_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemslist.UseAccessibleHeader = false;
            gv_itemslist.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void ddl_head_names_email(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (ddl_sendemail_type.SelectedValue == "Head_Info")
        {
            ddl_mail_headname.Items.Clear();
            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field1 from pay_zone_master where Type='HEAD' and Field10='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and client_code='" + txt_clientcode.Text + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl_mail_headname.Items.Add(dr[0].ToString());
            }
            dr.Close();
            d.con.Close();
            ddl_mail_headname.Items.Insert(0, new ListItem("Select"));

        }
        if (ddl_sendemail_type.SelectedValue == "Zone_Head_Info")
        {
            ddl_mail_headname.Items.Clear();
            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field2 from pay_zone_master where Type='ZONE' and Field1='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and client_code='" + txt_clientcode.Text + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl_mail_headname.Items.Add(dr[0].ToString());
            }
            dr.Close();
            d.con.Close();
            ddl_mail_headname.Items.Insert(0, new ListItem("Select"));
        }
        if (ddl_sendemail_type.SelectedValue == "Region_Head_Info")
        {
            ddl_mail_headname.Items.Clear();
            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field3 from pay_zone_master where Type='REGION' and Field1='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and client_code='" + txt_clientcode.Text + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl_mail_headname.Items.Add(dr[0].ToString());
            }
            dr.Close();
            d.con.Close();
            ddl_mail_headname.Items.Insert(0, new ListItem("Select"));
        }
    }
    protected void ddl_head_emailid(object sender, EventArgs e)
    {

        if (ddl_sendemail_type.SelectedValue == "Head_Info")
        {

            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field4 from pay_zone_master where Type='HEAD' and Field10='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Field1='" + ddl_mail_headname.SelectedValue + "' and client_code='" + txt_clientcode.Text + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_head_emailid.Text = dr[0].ToString();

            }
            dr.Close();
            d.con.Close();


        }
        if (ddl_sendemail_type.SelectedValue == "Zone_Head_Info")
        {

            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field4 from pay_zone_master where Type='ZONE' and Field1='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Field2='" + ddl_mail_headname.SelectedValue + "' and client_code='" + txt_clientcode.Text + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_head_emailid.Text = dr[0].ToString();
            }
            dr.Close();
            d.con.Close();

        }
        if (ddl_sendemail_type.SelectedValue == "Region_Head_Info")
        {

            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select Field5 from pay_zone_master where Type='REGION' and Field1='" + ddl_head_type_email.SelectedValue + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'and Field3='" + ddl_mail_headname.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_head_emailid.Text = dr[0].ToString();
            }
            dr.Close();
            d.con.Close();

        }
    }

    protected void lnk_add_emailid_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_emailsend.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Field1");
        dt.Columns.Add("Field2");
        dt.Columns.Add("Field3");
        dt.Columns.Add("Field4");

        int rownum = 0;
        for (rownum = 0; rownum < gv_emailsend.Rows.Count; rownum++)
        {
            if (gv_emailsend.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["Field1"] = gv_emailsend.Rows[rownum].Cells[2].Text;
                dr["Field2"] = gv_emailsend.Rows[rownum].Cells[3].Text;
                dr["Field3"] = gv_emailsend.Rows[rownum].Cells[4].Text;
                dr["Field4"] = gv_emailsend.Rows[rownum].Cells[5].Text;

                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();


        dr["Field1"] = ddl_sendemail_type.SelectedValue;
        dr["Field2"] = ddl_head_type_email.SelectedValue;
        dr["Field3"] = ddl_mail_headname.SelectedValue;
        dr["Field4"] = txt_head_emailid.Text;

        dt.Rows.Add(dr);
        gv_emailsend.DataSource = dt;
        gv_emailsend.DataBind();

        ViewState["emailtable"] = dt;

        ddl_sendemail_type.SelectedValue = "Select";
        ddl_head_type_email.SelectedValue = "Select";
        txt_head_emailid.Text = "";
    }

    protected void gv_clientItems_RowDataBound(object sender, GridViewRowEventArgs e)
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

    //Zone login MD CHANGE
    public void zone_login(string zone_name, string user_name)
    {
        zone_login_code();
        string clientCode = "";
        string type_name = "";
        string zone = "";
        string zone_head_type = "";
        string zone_head_name = "";
        string zone_head_no = "";
        string zone_head_email = "";
        string pwd_zone = "gJiYRq5463";
        string login_id = zone_code;
        //d.operation("delete from pay_user_master where unit_flag='2' and client_code='" + txt_clientcode.Text.ToString() + "' and Login_id='" + login_id + "'");
        if (d.getsinglestring("select login_id from pay_user_master where LOGIN_ID = '"+zone_code+"'").Equals(""))
        {
            d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,FLAG,create_date,first_login,comp_code,UNIT_FLAG,client_code,password_changed_date,create_user,client_zone) Values('" + login_id + "','" + user_name + "','" + GetSha256FromString(pwd_zone) + "','A',now(),'0','" + Session["comp_code"].ToString() + "','2','" + txt_clientcode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "','" + zone_name + "')");
        }

        MySqlCommand cmd_item = new MySqlCommand("select client_code,type,zone,field1,field2,field3,field4 from pay_zone_master where pay_zone_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_zone_master.client_code='" + txt_clientcode.Text + "' and pay_zone_master.type='ZONE'  AND pay_zone_master.zone='" + zone_name + "'", d1.con1);
        d1.con1.Open();
        try
        {
            MySqlDataReader dr2 = cmd_item.ExecuteReader();
            while (dr2.Read())
            {
                clientCode = dr2.GetValue(0).ToString();
                type_name = dr2.GetValue(1).ToString();
                zone = dr2.GetValue(2).ToString();
                zone_head_type = dr2.GetValue(3).ToString();
                zone_head_name = dr2.GetValue(4).ToString();
                zone_head_no = dr2.GetValue(5).ToString();
                zone_head_email = dr2.GetValue(6).ToString();


            }
            dr2.Close();
            cmd_item.Dispose();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }


        //commnet code from here...as per vinod
        //string body = string.Empty;
        //string body1 = string.Empty;
        //using (StreamReader reader = new StreamReader(Server.MapPath("~/User_Details.htm")))
        //{
        //    body = reader.ReadToEnd();
        //}
        //try
        //{
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

        //    mail.From = new MailAddress("info@celtsoft.com");
        //    mail.To.Add(zone_head_email);
        //    mail.Subject = "User Details";

        //    d.con1.Open();
        //    MySqlCommand cmdmax1 = new MySqlCommand("SELECT USER_NAME,LOGIN_ID,USER_PASSWORD FROM pay_user_master WHERE client_code = '" + clientCode + "' AND  comp_code='" + Session["comp_code"].ToString() + "'  and  client_zone='" + zone_name + "' and unit_flag='2'", d.con1);
        //    MySqlDataReader drmax = cmdmax1.ExecuteReader();
        //    while (drmax.Read())
        //    {
        //        body = body.Replace("{Name}", drmax.GetValue(0).ToString());
        //        body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
        //        body = body.Replace("{Password}", pwd_zone);
        //    }
        //    drmax.Close();
        //    d.con1.Close();

        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SmtpServer.Port = 587;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
        //    SmtpServer.EnableSsl = false;
        //    SmtpServer.Send(mail);
        //}

        //catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email not sent. Please Check Internet Connection !!!')", false); }
        //finally { d.con1.Close(); }
    }

    public void head_login(string zone_name, string user_name)
    {
        head_login_code();
        string clientCode = "";
        string type_name = "";
        string head = "";
        string head_type = "";
        string head_name = "";
        string head_no = "";
        string head_email = "";
        string pwd_head = "gJiYRq5463";
        string login_id = zone_code;
        //d.operation("delete from pay_user_master where unit_flag='4' and client_code='" + txt_clientcode.Text.ToString() + "' and Login_id='" + login_id + "'");
        if (d.getsinglestring("Select login_id from pay_user_master where login_id = '" + login_id + "'").Equals(""))
        {
            d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,FLAG,create_date,first_login,comp_code,UNIT_FLAG,client_code,password_changed_date,create_user,client_zone) Values('" + login_id + "','" + user_name + "','" + GetSha256FromString(pwd_head) + "','A',now(),'0','" + Session["comp_code"].ToString() + "','4','" + txt_clientcode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "','" + zone_name + "')");
        }

        MySqlCommand cmd_item = new MySqlCommand("select client_code,type,zone,field1,field2,field3,field4 from pay_zone_master where pay_zone_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_zone_master.client_code='" + txt_clientcode.Text + "' and pay_zone_master.type='ZONE'  AND pay_zone_master.zone='" + head_name + "'", d1.con1);
        d1.con1.Open();
        try
        {
            MySqlDataReader dr2 = cmd_item.ExecuteReader();
            while (dr2.Read())
            {
                clientCode = dr2.GetValue(0).ToString();
                type_name = dr2.GetValue(1).ToString();
                head = dr2.GetValue(2).ToString();
                head_type = dr2.GetValue(3).ToString();
                head_name = dr2.GetValue(4).ToString();
                head_no = dr2.GetValue(5).ToString();
                head_email = dr2.GetValue(6).ToString();


            }
            dr2.Close();
            cmd_item.Dispose();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }
}

    protected void ddl_item_list()
    {
        ddl_itemname.Items.Clear();
        MySqlCommand cmd_item = new MySqlCommand("SELECT item_name , item_code  from pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY ITEM_NAME", d.con);
        d.con.Open();
        try
        {
            int i = 0;
            MySqlDataReader dr_item = cmd_item.ExecuteReader();
            while (dr_item.Read())
            {
                ddl_itemname.Items.Insert(i++, new ListItem(dr_item[0].ToString(), dr_item[1].ToString()));
            }
            dr_item.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_itemname.Items.Insert(0, new ListItem("Select", "0"));

        }

    }
    protected void lnk_add_ClientItems_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_emailsend.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("item_name");
        dt.Columns.Add("quantiry");
        dt.Columns.Add("validity");
        dt.Columns.Add("expiry_date");

        int rownum = 0;
        for (rownum = 0; rownum < gv_clientItems.Rows.Count; rownum++)
        {
            if (gv_clientItems.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["item_name"] = gv_clientItems.Rows[rownum].Cells[2].Text;
                dr["quantiry"] = gv_clientItems.Rows[rownum].Cells[3].Text;
                dr["validity"] = gv_clientItems.Rows[rownum].Cells[4].Text;
                dr["expiry_date"] = gv_clientItems.Rows[rownum].Cells[5].Text;

                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["item_name"] = ddl_itemname.SelectedValue;
        dr["quantiry"] = txt_quantity.Text;
        dr["validity"] = txt_validity.Text;
        dr["expiry_date"] = txt_expirydate.Text;

        dt.Rows.Add(dr);
        gv_clientItems.DataSource = dt;
        gv_clientItems.DataBind();

        ViewState["clientitemtable"] = dt;

        // ddl_itemname.SelectedValue = "Select";
        txt_quantity.Text = "";
        txt_expirydate.Text = "";
        txt_validity.Text = "";
    }

    protected void lnk_remove_item_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["clientitemtable"] != null)
        {
            DataTable dt = (DataTable)ViewState["clientitemtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["clientitemtable"] = dt;
            gv_clientItems.DataSource = dt;
            gv_clientItems.DataBind();
        }
    }
    protected void ddl_reginheadzone2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_reginheadzone();
    }
    protected void ddl_reginheadzone()
    {

        ddl_rgn_zone.Items.Clear();
        MySqlCommand cmd_item = new MySqlCommand("select ZONE from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + txt_clientcode.Text + "' and `Type`='ZONE'", d.con);
        d.con.Open();
        try
        {
            int i = 0;
            MySqlDataReader dr_item = cmd_item.ExecuteReader();
            while (dr_item.Read())
            {
                ddl_rgn_zone.Items.Insert(i++, new ListItem(dr_item[0].ToString()));
            }
            dr_item.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_rgn_zone.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    // approval code
    //private void load_reporting_grdv()
    //{
    //    MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT ID,CLIENT_NAME AS 'CLIENT NAME',CLIENT_CODE,(SELECT DISTINCT(STATE_NAME) FROM pay_state_master WHERE STATE_CODE = pay_client_master_approval.STATE) AS STATE,CITY FROM pay_client_master_approval where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "' ORDER BY client_CODE", d.con1);
    //    DataSet DS1 = new DataSet();
    //    MySqlDataAdapter1.Fill(DS1);
    //    GridView1.DataSource = DS1;
    //    GridView1.DataBind();
    //}
    //protected void btn_approval_Click(object sender, EventArgs e)
    //{
    //    //string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString(),1);
    //    string approval_reporting = d.getsinglestring("Select substring(approval,7,6) from pay_client_master_approval where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Id = '" + Session["Approval_Id"].ToString() + "'");

    //    if (approval_reporting == "")
    //    {
    //        if (check_client_code()) 
    //        {
    //           d.operation("UPDATE pay_client_master set CLIENT_NAME='" + txt_clientname.Text + "',AG_START_DATE = str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),AG_END_DATE = str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),ADDRESS1 = '" + txt_client_address1.Text + "',ADDRESS2 = '" + txt_client_address2.Text + "',STATE = '" + ddl_client_state.SelectedValue + "',CITY = '" + ddl_client_city.SelectedValue + "',FILE_NO = '" + txt_file_no.Text + "',NO_OF_BRANCH = '" + txt_branch_count.Text + "',REG_NO = '" + txt_reg_no.Text + "',GST_NO = '" + txt_gst_no.Text + "',PAN_NO = '" + txt_pan_no.Text + "',LICENSE_NO = '" + txt_license.Text + "',BANK_DETAILS = '" + txt_bank_detail.Text + "',WEBSITE = '" + txt_website.Text + "',DESIGNATION = '" + ddl_designation.SelectedValue + "',comments= concat(IFNULL(comments,''),'" + txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString() + " ON-',now(),'@#$%'),total_employee='" + txt_employee_total.Text + "', approval=''  WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //           d.operation("Delete from pay_client_master_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Id = '" + Session["Approval_Id"].ToString() + "'");
    //           d.operation("Delete From pay_designation_count where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code is null");
    //           d.operation("Delete From pay_zone_master where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
    //           d.operation("Delete From pay_client_item_list where client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' ");

    //           d.operation("Insert into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location) Select COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location From pay_designation_count_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and unit_code is null");
    //           d.operation("Delete from pay_designation_count_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and unit_code is null");

    //           d.operation("Insert into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13) Select COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13 From pay_zone_master_approval Where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ");
    //           d.operation("Delete from pay_zone_master_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //           d.operation("Insert into pay_images (comp_code,client_code,description,file_name,start_date,end_date,created_by,created_date) Select comp_code,client_code,description,file_name,start_date,end_date,created_by,created_date From pay_images_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ");
    //           d.operation("Delete from pay_images_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //           d.operation("Insert into pay_client_item_list (COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date) Select COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date From pay_client_item_list_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
    //           d.operation("Delete from pay_client_item_list_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //        }
    //        else
    //        {
    //            d.operation("Insert into pay_client_master(COMP_CODE,CLIENT_NAME,CLIENT_CODE,AG_START_DATE ,AG_END_DATE,ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,DESIGNATION,total_employee) Select COMP_CODE,CLIENT_NAME,CLIENT_CODE,AG_START_DATE,AG_END_DATE,ADDRESS1,ADDRESS2,STATE,CITY,FILE_NO,NO_OF_BRANCH,REG_NO,GST_NO,PAN_NO,LICENSE_NO,BANK_DETAILS,WEBSITE,DESIGNATION,total_employee From pay_client_master_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Id = '" + Session["Approval_Id"].ToString() + "'");
    //            d.operation("Delete from pay_client_master_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and Id = '" + Session["Approval_Id"].ToString() + "'");

    //            d.operation("Insert into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location) Select COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location From pay_designation_count_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and unit_code is null");
    //            d.operation("Delete from pay_designation_count_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and unit_code is null");

    //            d.operation("Insert into pay_zone_master (COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13) Select COMP_CODE,CLIENT_CODE,Type,Field1,ZONE,REGION,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10,Field11,Field12,Field13 From pay_zone_master_approval Where comp_code ='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ");
    //            d.operation("Delete from pay_zone_master_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //            d.operation("Insert into pay_images (comp_code,client_code,description,file_name,start_date,end_date,created_by,created_date) Select comp_code,client_code,description,file_name,start_date,end_date,created_by,created_date From pay_images_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ");
    //            d.operation("Delete from pay_images_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //            d.operation("Insert into pay_client_item_list (COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date) Select COMP_CODE,CLIENT_CODE,item_name,quantiry,validity,expiry_date From pay_client_item_list_approval Where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
    //            d.operation("Delete from pay_client_item_list_approval where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");

    //            d.operation("update pay_client_master set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%'");
    //        }
    //        d.operation("Update approval_status set status = 'Approved',approval_date = now(),approved_by = '" + Session["LOGIN_ID"].ToString() + "' where ref_no ='" + Session["Approval_id"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and (comment = '' OR comment is null)");
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved successfully!!');", true);    
    //    }
    //    else
    //    {
    //        d.operation("update pay_client_master_approval set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%' and Id = '" + Session["Approval_Id"].ToString() + "'");
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Send to the next level Approval !!');", true);
    //    }

    //    MySqlCommand cmd_item1 = new MySqlCommand("SELECT id from pay_client_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' order by id", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
    //        DataTable dt = new DataTable();
    //        dt.Load(dr_item1);
    //        int numberOfResults = dt.Rows.Count;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            if (numberOfResults != 1)
    //            {
    //                d.operation("delete from pay_client_master where id = " + dr["id"].ToString());
    //                numberOfResults = numberOfResults - 1;
    //            }
    //        }
    //        dr_item1.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //    loadclientgrid();
    //    btn_approval.Visible = false;
    //    btn_reject.Visible = false;
    //    btn_add.Visible = true;
    //    btnclose.Visible = true;

    //}
    //protected void GridView1_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
    //    catch { }

    //    Session["Approval_Id"] = GridView1.SelectedRow.Cells[0].Text;
    //    Session["Approval_Code"] = GridView1.SelectedRow.Cells[2].Text;
    //    load_fields(GridView1.SelectedRow.Cells[2].Text, 2);
    //    btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true; btn_reject.Visible = true;

    //    load_reporting_grdv();
    //}
    //private void approval_status()
    //{
    //    MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id as 'ID', COMP_CODE as 'COMP CODE',client_code as 'CLIENT CODE',status as 'STATUS',comment as 'COMMENT',created_by as 'REQUESTED BY',approved_by as 'APPROVED/REJECT BY',approval_date as 'APPROVAL DATE' FROM approval_status where client_code is not null", d.con1);
    //    DataTable Dt = new DataTable();
    //    MySqlDataAdapter1.Fill(Dt);
    //    GridView2.DataSource = Dt;
    //    GridView2.DataBind();
    //    ViewState["approval_grid"] = Dt;
    //    d.con1.Close();
    //}
    //protected void btn_reject_Click(object sender, EventArgs e)
    //{
    //    //Session["Approval_Id"] = txt_clientcode.Text;
    //    ModalPopupExtender1.Show();
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

    //    }
    //    catch
    //    {
    //    }

    //}
    //protected void lnkbtn_remove_aprvl_status_Click(object sender, EventArgs e)
    //{
    //    int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

    //    if (ViewState["approval_grid"] != null)
    //    {
    //        System.Data.DataTable dt = (DataTable)ViewState["approval_grid"];
    //        if (dt.Rows.Count >= 1)
    //        {
    //            if (rowID < dt.Rows.Count)
    //            {
    //                d.operation("Delete from approval_status where id= " + dt.Rows[rowID][0]);
    //                dt.Rows.Remove(dt.Rows[rowID]);
    //            }
    //        }
    //        ViewState["approval_grid"] = dt;
    //        GridView2.DataSource = dt;
    //        GridView2.DataBind();
    //    }
    //}
    //private bool check_client_code()
    //{
    //    foreach (GridViewRow row in ClientGridView.Rows)
    //    {
    //        if (row.Cells[1].Text.Equals(txt_clientcode.Text))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    //protected void Button8_Click(object sender, EventArgs e)
    //{
    //    load_reporting_grdv();
    //    text_clear();
    //    approval_status();
    //    btn_reject.Visible = false;
    //    btn_add.Visible = true;
    //    btn_approval.Visible = false;
    //}

    //MD code 
    public void region_code()
    {

        d.con1.Open();
        try
        {
            string code_series = d.getsinglestring("select `EMP_SERIES_INIT` from pay_company_master where comp_code='" + Session["COMP_CODE"].ToString() + "'");
           // MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(LOGIN_ID,5) AS UNSIGNED))+1 FROM  pay_user_master WHERE comp_code='" + Session["comp_code"].ToString() + "' AND (zone_region != '' || zone_region is not null) ", d.con1);
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(LOGIN_ID,4,3) AS UNSIGNED))+1 FROM  `pay_user_master` WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND (`zone_region` != '' || `zone_region` IS NOT NULL)  AND `UNIT_FLAG` = 3", d.con1);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_region_code = code_series + "CR001";
                }
                else
                {
                    int max_unitcode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_unitcode < 10)
                    {
                        txt_region_code = code_series + "CR00" + max_unitcode;
                    }
                    else if (max_unitcode > 9 && max_unitcode < 100)
                    {
                        txt_region_code = code_series + "CR0" + max_unitcode;
                    }
                    else if (max_unitcode > 99 && max_unitcode < 1000)
                    {
                        txt_region_code = code_series + "CR" + max_unitcode;
                    }
                }
            }
            drmax.Close();
            d.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    //Region login MD CHANGE
    public void region_login(string zone_code, string region_ccode, string head_name)
    {

        string clientCode = "";
        string type_name = "";
        string region = "";
        string zone = "";
        string region_head_type = "";
        string region_head_name = "";
        string region_head_no = "";
        string region_head_email = "";
        string pwd_region = "gJiYRq5463";
        region_code();
        string login_id = txt_region_code;
        string zone_code1 = zone_code;
        string region_ccode1 = region_ccode;
        // d.operation("delete from pay_user_master where unit_flag='3' and client_code='" + txt_clientcode.Text.ToString() + "' AND client_zone='" + zone_code1 + "' and zone_region='" + region_ccode1 + "'");
        if (d.getsinglestring("Select login_id from pay_user_master where login_id = '" + login_id + "'").Equals(""))
        {
            d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,FLAG,create_date,first_login,comp_code,UNIT_FLAG,client_code,password_changed_date,create_user,zone_region,client_zone) Values('" + login_id + "','" + head_name + "','" + GetSha256FromString(pwd_region) + "','A',now(),'0','" + Session["comp_code"].ToString() + "','3','" + txt_clientcode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "','" + region_ccode1 + "','" + zone_code1 + "')");
        }



        //MySqlCommand cmd_item = new MySqlCommand("select client_code,type,zone,field1,field2,field3,field5 from pay_zone_master where pay_zone_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_zone_master.client_code='" + txt_clientcode.Text + "' and pay_zone_master.type='REGION'", d1.con1);
        //d1.con1.Open();
        //MySqlDataReader dr2 = cmd_item.ExecuteReader();
        //while (dr2.Read())
        //{
        //    clientCode = dr2.GetValue(0).ToString();
        //    type_name = dr2.GetValue(1).ToString();
        //    region = dr2.GetValue(2).ToString();
        //    region_head_type = dr2.GetValue(3).ToString();
        //    region_head_name = dr2.GetValue(4).ToString();
        //    region_head_no = dr2.GetValue(5).ToString();
        //    region_head_email = dr2.GetValue(6).ToString();

        //    // send_email(Server.MapPath("~/User_Details.htm"), zone_head_email, zone);
        //    string body = string.Empty;
        //    string body1 = string.Empty;
        //    using (StreamReader reader = new StreamReader(Server.MapPath("~/User_Details.htm")))
        //    {
        //        body = reader.ReadToEnd();
        //    }
        //    try
        //    {
        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

        //        mail.From = new MailAddress("info@celtsoft.com");
        //        mail.To.Add(region_head_email);
        //       // mail.CC.Add(tocc);
        //        mail.Subject = "User Details";

        //        d.con1.Open();
        //        MySqlCommand cmdmax1 = new MySqlCommand("SELECT USER_NAME,LOGIN_ID,USER_PASSWORD FROM pay_user_master WHERE client_code = '" + clientCode + "' AND  comp_code='" + Session["comp_code"].ToString() + "' and unit_flag='3'", d.con1);
        //        MySqlDataReader drmax = cmdmax1.ExecuteReader();
        //        while (drmax.Read())
        //        {
        //            body = body.Replace("{Name}", drmax.GetValue(0).ToString());
        //            body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
        //            body = body.Replace("{Password}", pwd_region);
        //            //  body = "Admin123");
        //            //mail.Body = tosubject;
        //        }
        //        drmax.Close();
        //        d.con1.Close();

        //        mail.Body = body;
        //        mail.IsBodyHtml = true;
        //        SmtpServer.Port = 587;
        //        SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
        //        SmtpServer.EnableSsl = false;

        //        SmtpServer.Send(mail);
        //    }

        //    catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false); }

        //}
        //dr2.Close();
        //cmd_item.Dispose();
        //d1.con1.Close();



        //MD Change mail


        MySqlCommand cmd_item = new MySqlCommand("select client_code,type,zone,REGION,field1,field2,field3,field5 from pay_zone_master where pay_zone_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_zone_master.client_code='" + txt_clientcode.Text + "' and pay_zone_master.type='REGION' AND pay_zone_master.zone='" + zone_code + "' AND pay_zone_master.`REGION` = '" + region_ccode + "'", d1.con1);
        d1.con1.Open();
        try
        {
            MySqlDataReader dr2 = cmd_item.ExecuteReader();
            while (dr2.Read())
            {
                clientCode = dr2.GetValue(0).ToString();
                type_name = dr2.GetValue(1).ToString();
                clientCode = dr2.GetValue(0).ToString();
                zone = dr2.GetValue(2).ToString();
                region = dr2.GetValue(3).ToString();
                region_head_type = dr2.GetValue(4).ToString();
                region_head_name = dr2.GetValue(6).ToString();
                //region_head_no = dr2.GetValue(6).ToString();
                region_head_email = dr2.GetValue(7).ToString();


            }
            dr2.Close();
            cmd_item.Dispose();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }


        //commnet code from here...as per vinod
        //string body = string.Empty;
        //string body1 = string.Empty;
        //using (StreamReader reader = new StreamReader(Server.MapPath("~/User_Details.htm")))
        //{
        //    body = reader.ReadToEnd();
        //}
        //try
        //{
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

        //    mail.From = new MailAddress("info@celtsoft.com");
        //    mail.To.Add(region_head_email);
        //    mail.Subject = "User Details";

        //    d.con1.Open();
        //    MySqlCommand cmdmax1 = new MySqlCommand("SELECT USER_NAME,LOGIN_ID,USER_PASSWORD FROM pay_user_master WHERE client_code = '" + clientCode + "' AND  comp_code='" + Session["comp_code"].ToString() + "' and unit_flag='3' and client_zone='"+zone_code+"' and zone_region='"+region_ccode+"' ", d.con1);
        //    MySqlDataReader drmax = cmdmax1.ExecuteReader();
        //    while (drmax.Read())
        //    {
        //        body = body.Replace("{Name}", drmax.GetValue(0).ToString());
        //        body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
        //        body = body.Replace("{Password}", pwd_region);
        //    }
        //    drmax.Close();
        //    d.con1.Close();

        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SmtpServer.Port = 587;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
        //    SmtpServer.EnableSsl = false;
        //    SmtpServer.Send(mail);
        //}

        //catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email not sent. Please Check Internet Connection !!!')", false); }
        //finally { d.con1.Close(); }
    }

    public void zone_login_code()
    {

        d.con1.Open();
        try
        {
            string code_series = d.getsinglestring("select `EMP_SERIES_INIT` from pay_company_master where comp_code='" + Session["COMP_CODE"].ToString() + "'");
            //MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(LOGIN_ID,5) AS UNSIGNED))+1 FROM  pay_user_master WHERE comp_code='" + Session["COMP_CODE"].ToString() + "' AND  (client_zone is not null AND zone_region is null)", d.con1);
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(`LOGIN_ID`,4, 3) AS unsigned)) + 1 FROM  pay_user_master WHERE comp_code='" + Session["COMP_CODE"].ToString() + "' AND  (client_zone is not null AND zone_region is null) and `UNIT_FLAG` = 2", d.con1);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    zone_code = code_series + "CZ001";
                }
                else
                {
                    int max_zonecode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_zonecode < 10)
                    {
                        zone_code = code_series + "CZ00" + max_zonecode;
                    }
                    else if (max_zonecode > 9 && max_zonecode < 100)
                    {
                        zone_code = code_series + "CZ0" + max_zonecode;
                    }
                    else if (max_zonecode > 99 && max_zonecode < 1000)
                    {
                        zone_code = code_series + "CZ" + max_zonecode;
                    }
                }
                //return zone_code;
            }
            drmax.Close();
            d.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }


    public void head_login_code()
    {

        d.con1.Open();
        try
        {
            string code_series = d.getsinglestring("select `EMP_SERIES_INIT` from pay_company_master where comp_code='" + Session["COMP_CODE"].ToString() + "'");
            //MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(LOGIN_ID,5) AS UNSIGNED))+1 FROM  pay_user_master WHERE comp_code='" + Session["COMP_CODE"].ToString() + "' AND  (client_zone is not null AND zone_region is null)", d.con1);
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(`LOGIN_ID`,4, 3) AS unsigned)) + 1 FROM `pay_user_master` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND (`client_zone` IS NOT NULL AND `zone_region` IS NULL) AND `UNIT_FLAG` = 4", d.con1);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    zone_code = code_series + "CH001";
                }
                else
                {
                    int max_zonecode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_zonecode < 10)
                    {
                        zone_code = code_series + "CH00" + max_zonecode;
                    }
                    else if (max_zonecode > 9 && max_zonecode < 100)
                    {
                        zone_code = code_series + "CH0" + max_zonecode;
                    }
                    else if (max_zonecode > 99 && max_zonecode < 1000)
                    {
                        zone_code = code_series + "CH" + max_zonecode;
                    }
                }
                //return zone_code;
            }
            drmax.Close();
            d.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    //MD change for bank details

    protected void lnk_bankdetails_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        grd_bank_details.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Field1");
        dt.Columns.Add("Field2");
        dt.Columns.Add("Field3");
        int rownum = 0;
        for (rownum = 0; rownum < grd_bank_details.Rows.Count; rownum++)
        {
            if (grd_bank_details.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["Field1"] = grd_bank_details.Rows[rownum].Cells[2].Text;
                dr["Field2"] = grd_bank_details.Rows[rownum].Cells[3].Text;
                dr["Field3"] = grd_bank_details.Rows[rownum].Cells[4].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["Field1"] = txt_bank_name.Text;
        dr["Field2"] = txt_account_no.Text;
        dr["Field3"] = txt_ifsc_code.Text;
        dt.Rows.Add(dr);
        grd_bank_details.DataSource = dt;
        grd_bank_details.DataBind();
        grd_bank_details.Visible = true;
        ViewState["grd_bankdetails"] = dt;
        txt_bank_name.Text = "";
        txt_account_no.Text = "";
        txt_ifsc_code.Text = "";

    }
    protected void grd_bank_details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
                if (e.Row.Cells[i].Text == "&amp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }
    protected void lnk_remove_bank_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        if (ViewState["grd_bankdetails"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["grd_bankdetails"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["grd_bankdetails"] = dt;
            grd_bank_details.DataSource = dt;
            grd_bank_details.DataBind();
        }

    }
    protected void gv_start_end_date_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_start_end_date_details.UseAccessibleHeader = false;
            gv_start_end_date_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
    protected void grd_bank_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_bank_details.UseAccessibleHeader = false;
            grd_bank_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    private void load_bank_gridview(string compcode, string client_code)
    {

        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3 from pay_zone_master where comp_Code = '" + compcode + "' and client_code ='" + client_code + "' and type = 'bank_details'", d.con1);
        DataTable DS1 = new DataTable();
        MySqlDataAdapter1.Fill(DS1);
        grd_bank_details.DataSource = DS1;
        grd_bank_details.DataBind();
        ViewState["grd_bankdetails"] = DS1;
        txt_bank_name.Text = "";
        txt_account_no.Text = "";
        txt_ifsc_code.Text = "";
        grd_bank_details.Visible = true;
    }

    protected void lnk_remove_comp_group_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["Comp_Group"] != null)
        {
            DataTable dt = (DataTable)ViewState["Comp_Group"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["Comp_Group"] = dt;
            gv_comp_group.DataSource = dt;
            gv_comp_group.DataBind();
        }
    }

    protected void lnk_remove_Billing_Type_Click(object sender, EventArgs e)
    {
        hidtab.Value = "11";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["Billing_Type"] != null)
        {
            DataTable dt = (DataTable)ViewState["Billing_Type"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["Billing_Type"] = dt;
            gv_billing_type.DataSource = dt;
            gv_billing_type.DataBind();
        }
    }



    //komal 24-05-19

    protected void linkbtn_esic_Click(object sender, EventArgs e)
    {
        hidtab.Value = "13";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //komal 21-06-19

        foreach (GridViewRow gr in grid_esic.Rows)
        {
            //Label lbl_gststate = (Label)row.FindControl("lbl_gststate");
            //string gststate = (lbl_gststate.Text);

            string cell_1_Value = grid_esic.Rows[gr.RowIndex].Cells[2].Text;
            if (ddl_esic_state.SelectedItem.Text.Equals(cell_1_Value))
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same GST State for one Company!!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Can Not Insert 2 same ESIC STATE for one Company!!!')", true);

                return;
            }
        }

        //komal 21-06-19

        foreach (GridViewRow gr in grid_esic.Rows)
        {
            //Label lbl_gststate = (Label)row.FindControl("lbl_gststate");
            //string gststate = (lbl_gststate.Text);

            string cell_1_Value = grid_esic.Rows[gr.RowIndex].Cells[4].Text;
            if (ddl_esic_state.SelectedItem.Text.Equals(cell_1_Value))
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same GST State for one Company!!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Can Not Insert 2 same ESIC CODE for one Company!!!')", true);

                return;
            }
        }


        grid_esic.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ESIC_STATE");
        dt.Columns.Add("ESIC_ADDRESS");
        dt.Columns.Add("ESIC_CODE");
        int rownum = 0;
        for (rownum = 0; rownum < grid_esic.Rows.Count; rownum++)
        {
            if (grid_esic.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["ESIC_STATE"] = grid_esic.Rows[rownum].Cells[2].Text;
                dr["ESIC_ADDRESS"] = grid_esic.Rows[rownum].Cells[3].Text;
                dr["ESIC_CODE"] = grid_esic.Rows[rownum].Cells[4].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["ESIC_STATE"] = ddl_esic_state.SelectedValue;
        dr["ESIC_ADDRESS"] = txt_esic_address.Text;
        dr["ESIC_CODE"] = txt_esicregistrationcode.Text;
        dt.Rows.Add(dr);
        grid_esic.DataSource = dt;
        grid_esic.DataBind();
        ViewState["CurrentTable"] = dt;
        ddl_esic_state.SelectedIndex = 0;
        txt_esic_address.Text = "";
        txt_esicregistrationcode.Text = "";
    }
    public void company_bank_load()
    {

        try
        {
            ddl_company_bank.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select Field1 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and CLIENT_CODE is null", d3.con);
            d3.con.Open();



            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_company_bank.DataSource = dt_item;
                ddl_company_bank.DataTextField = dt_item.Columns[0].ToString();
                ddl_company_bank.DataValueField = dt_item.Columns[0].ToString();
                ddl_company_bank.DataBind();
                d3.con.Close();
            }

            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_company_bank.Items.Insert(0, new ListItem("Select"));
            d3.con.Close();
        }

    }

    protected void grid_esic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            if (e.Row.Cells[i].Text == "&amp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grid_esic, "Select$" + e.Row.RowIndex);

        //}
    }

    protected void linkbtn_removeitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
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
            grid_esic.DataSource = dt;
            grid_esic.DataBind();
        }

    }
    protected void grid_esic_PreRender(object sender, EventArgs e)
    {
        try
        {
            // grid_esic.UseAccessibleHeader = false;
            grid_esic.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_clientItems_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_clientItems.UseAccessibleHeader = false;
            gv_clientItems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_comp_group_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_comp_group.UseAccessibleHeader = false;
            gv_comp_group.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_billing_type_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_billing_type.UseAccessibleHeader = false;
            gv_billing_type.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void ddl_company_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "17";
        try
        {
            d.con.Open();
            txt_comp_ac_no.Text = "";
            MySqlCommand cmd_item = new MySqlCommand("Select Field2 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'bank_details' and Field1 = '" + ddl_company_bank.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();
            if (dr.Read())
            {
                txt_comp_ac_no.Text = dr.GetValue(0).ToString();
            }
            if (reason_panel.Visible == true)
            {
                btn_edit.Visible = true;
                btn_delete.Visible = true;
                btnclose.Visible = true;
                btn_add.Visible = false;

            }
            else
            {

                btn_edit.Visible = false;
                btn_delete.Visible = false;
                btnclose.Visible = true;
                btn_add.Visible = true;

            }
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    }
    private void update_policy()
    {
        d.operation("update pay_billing_master set start_date_common="+txt_start_date_client.SelectedValue+", end_date_common="+txt_end_date_client.Text+" where billing_client_code='"+txt_clientcode.Text+"'");
    
    }
    protected void lnk_deduct_Click(object sender, EventArgs e)
    {
        hidtab.Value = "14";
        DataTable dt = new DataTable();
        DataRow dr;//client_code,
        dt.Columns.Add("deduction_item");
        dt.Columns.Add("deduction_amount");


        foreach (GridViewRow row in grv_dwduction.Rows)
        {
            string item = row.Cells[2].Text;

            if (ddl_dedu_iteam.SelectedValue.Equals(item))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Iteam Name Already Added!!')", true);
                return;
            }
        }

        int rownum = 0;
        for (rownum = 0; rownum < grv_dwduction.Rows.Count; rownum++)
        {
            if (grv_dwduction.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                dr["deduction_item"] = grv_dwduction.Rows[rownum].Cells[2].Text;
                dr["deduction_amount"] = grv_dwduction.Rows[rownum].Cells[3].Text;
              

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["deduction_item"] = ddl_dedu_iteam.SelectedValue;
        dr["deduction_amount"] = txt_deduc_amount.Text;
       

        dt.Rows.Add(dr);
        grv_dwduction.DataSource = dt;
        grv_dwduction.DataBind();

        ViewState["deduc_group"] = dt;
        ddl_dedu_iteam.SelectedValue = "Select";
        txt_deduc_amount.Text = "0";

    }
    protected void lnk_remove_deduct_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["deduc_group"] != null)
        {
            DataTable dt = (DataTable)ViewState["deduc_group"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["deduc_group"] = dt;
            grv_dwduction.DataSource = dt;
            grv_dwduction.DataBind();
        }
    }
    protected void email_grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_designation_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            txt_satrtdate.Text = "";
            txt_enddate.Text = "";
            string where = "";


            if (ddl_designation.SelectedValue == "SG")
            {
                where = " and client_code ='" + txt_clientcode.Text + "' and `field1` = 'SECURITY GUARD'";
            }
            else
            {
                where = " and client_code ='" + txt_clientcode.Text + "' and `field1` = 'HOUSEKEEPING'";
            }

            MySqlCommand cmd_item = new MySqlCommand("Select Field2,Field3 from pay_zone_master where comp_code='" + Session["comp_code"].ToString() + "' and Type = 'services' " + where + " ", d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();
            if (dr.Read())
            {
                //this.Hidden1.Value = "swara";
                this.Hidden1.Value = dr.GetValue(0).ToString();
                this.Hidden2.Value = dr.GetValue(1).ToString();
                txt_satrtdate.Text = dr.GetValue(0).ToString();
                txt_enddate.Text = dr.GetValue(1).ToString();
            }
           
        }
        catch (Exception ex) { }
        finally
        {

            d.con.Close();
        }
    }
    protected void category_gv() 
    {
        gv_services.DataSource = null;
        gv_services.DataBind();
        MySqlCommand cmd_gst1 = new MySqlCommand("SELECT ID,Field1, Field2,Field3 FROM pay_zone_master WHERE client_code ='" + txt_clientcode.Text + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'SERVICES'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_gst1 = cmd_gst1.ExecuteReader();
            if (dr_gst1.HasRows)
            {
                DataTable dt1 = new DataTable();
                dt1.Load(dr_gst1);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["servicestable"] = dt1;
                }
                gv_services.DataSource = dt1;
                gv_services.DataBind();
            }

        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }
    }

	 protected void ddl_adv_deduction_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            d.con.Open();
          
           
                  }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void link_adv_deduction_Click(object sender, EventArgs e)
    {
        hidtab.Value = "16";

        if (ddl_adv_deduction.SelectedValue == "0")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Deduction Yes  !!');", true);
            return;

        }

      
        
        string adv_deduction = "";

        if (ddl_adv_deduction.SelectedValue == "1")
        {
            //Panel24.Visible = true;
            ddl_adv_no.Visible = true;
            adv_deduction = d.getsinglestring("select `deduction_item`, `deduction_amount` from pay_deduction where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
            if (adv_deduction == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Add Deduction Amount in Deduction tab  !!');", true);
                return;

            }

            //ddl_adv_deduction.Items.Insert(0, "No");
        }


        


        foreach (GridViewRow row in gridview_advance_deduction.Rows)
        {


            string uniform = row.Cells[4].Text;
            string Shoes = row.Cells[5].Text;
            string Id_card = row.Cells[6].Text;

            // for uniform
            if (uniform == "2")
            {
                if (ddl_adv_no.SelectedValue == uniform || ddl_adv_no.SelectedValue =="1")
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this client uniform no of set completed ... !!!');", true);
                    return;
                }

            }else if (uniform == "1"){

                if (ddl_adv_no.SelectedValue != uniform)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this client uniform 1 set insert select only 1 set... !!!');", true);
                    return;
                }
            
            
            }
            // for shoes 
            if (Shoes == "1")
            {
                if (ddl_adv_shoes.SelectedValue == Shoes)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' NO Of Shoes And No Of ID Card Already Exist ... !!!');", true);
                    return;
                }
            }
            else if (Shoes == "0")
            {

                if (ddl_adv_shoes.SelectedValue != Shoes)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this client Shoes 1 set completed... !!!');", true);
                    return;
                }


            }

            // for id card

            if (Id_card == "1")
            {
                if (ddl_adv_id.SelectedValue == Id_card)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this client Id Card no of set completed ... !!!');", true);
                    return;
                }
            }
            else if (Id_card == "0")
            {

                if (ddl_adv_id.SelectedValue != Id_card)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this client Id Card 1 set completed... !!!');", true);
                    return;
                }


            }
        }

        if (txttodate.Text == "")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Month  !!');", true);
            return;

        }




        //string date = d.getsinglestring("select month from pay_advance_deduction where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "'");
        foreach (GridViewRow row in gridview_advance_deduction.Rows)
        {


            string date = row.Cells[7].Text;
            if (txttodate.Text == date)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' This Month Uniform Set All Ready Decided Please Select Another month ... !!!');", true);
                return;
            }
        }
        
        DataTable dt = new DataTable();
        DataRow dr;//client_code,
        
        dt.Columns.Add("deduction");
        dt.Columns.Add("deduction_no");
        dt.Columns.Add("no_of_uniform");
        dt.Columns.Add("no_of_shoes");
        dt.Columns.Add("no_of_id");
        dt.Columns.Add("month");
       


        

        int rownum = 0;
        for (rownum = 0; rownum < gridview_advance_deduction.Rows.Count; rownum++)
        {
            if (gridview_advance_deduction.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                
                dr["deduction"] = gridview_advance_deduction.Rows[rownum].Cells[2].Text;
                dr["deduction_no"] = gridview_advance_deduction.Rows[rownum].Cells[3].Text;
                dr["no_of_uniform"] = gridview_advance_deduction.Rows[rownum].Cells[4].Text;
                dr["no_of_shoes"] = gridview_advance_deduction.Rows[rownum].Cells[5].Text;
                dr["no_of_id"] = gridview_advance_deduction.Rows[rownum].Cells[6].Text;
                dr["month"] = gridview_advance_deduction.Rows[rownum].Cells[7].Text;


                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["deduction"] = ddl_adv_deduction.SelectedValue;
        dr["deduction_no"] = ddl_adv_deduction.SelectedItem;
        dr["no_of_uniform"] = ddl_adv_no.SelectedValue;
        dr["no_of_shoes"] = ddl_adv_shoes.SelectedValue;
        dr["no_of_id"] = ddl_adv_id.SelectedValue;
        dr["month"] = txttodate.Text;


        dt.Rows.Add(dr);
        gridview_advance_deduction.DataSource = dt;
        gridview_advance_deduction.DataBind();

        ViewState["deduc_group"] = dt;



        txttodate.Text = "";
        ddl_adv_deduction.SelectedValue = "0";
        ddl_adv_no.SelectedValue = "1";
        ddl_adv_shoes.SelectedValue = "0";
        ddl_adv_id.SelectedValue = "0";
        

    }
    protected void lnk_remove_adv_deduct_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["deduc_group"] != null)
        {
            DataTable dt = (DataTable)ViewState["deduc_group"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["deduc_group"] = dt;
            gridview_advance_deduction.DataSource = dt;
            gridview_advance_deduction.DataBind();
        }
    }
    protected void gridview_advance_deduction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
                if (e.Row.Cells[i].Text == "&amp;")
                {
                    e.Row.Cells[i].Text = "";
                }

            }
        }
        e.Row.Cells[2].Visible = false;
    }

    // company bannk details komal 22-04-2020
    protected void lnk_company_bank_Click(object sender, EventArgs e)
    {
        hidtab.Value = "17";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        string company_bank_check = d.getsinglestring("select `payment_ag_bank`,`company_ac_no` from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and payment_ag_bank='" + ddl_company_bank.SelectedValue + "' and company_ac_no ='" + txt_comp_ac_no.Text + "' ");

        if (company_bank_check!="") 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('This Record All Ready Added  !!')", true);
            return;
        
        }

        int result = d.operation("insert into pay_company_bank_details (comp_code,client_code,payment_ag_bank,company_ac_no)values('" + Session["comp_code"].ToString() + "','" + txt_clientcode.Text + "','" + ddl_company_bank.SelectedValue + "','" + txt_comp_ac_no.Text + "')");

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

    // for gridview

    protected void company_bank_details() 
    {
        MySqlCommand cmd1 = new MySqlCommand("select id,`payment_ag_bank`,`company_ac_no` from pay_company_bank_details where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' ", d4.con);
        d4.con.Open();
        try {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {

                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_company_bank.DataSource = dt;
                gv_company_bank.DataBind();
                gv_company_bank.Visible = true;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_company_bank.DataSource = dt;
                gv_company_bank.DataBind();
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d4.con.Close(); }
    

    
    }

    
    protected void lnk_remove_com_bank_Click(object sender, EventArgs e)
    {
        hidtab.Value = "17";
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_company_bank_details where id = '" + grdrow.Cells[2].Text + "'");

        company_bank_details();
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
    
	 protected void grv_dwduction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

       
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
	
	 protected void grv_dwduction_PreRender(object sender, EventArgs e)
    {
        try
        {
            grv_dwduction.UseAccessibleHeader = false;
            grv_dwduction.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
	
    protected void gridview_advance_deduction_PreRender(object sender, EventArgs e)
    {
        try
        {
            gridview_advance_deduction.UseAccessibleHeader = false;
            gridview_advance_deduction.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    // fire extinguisher komal 20-07-2020
    protected void ddl_fire_ext_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "18";
        try 
        {

            if (ddl_fire_ext.SelectedValue=="Applicable") 
            {
            //    Panel_no_days.Visible = true;
            }
        
        
        }
        catch (Exception ex) { throw ex; }
        finally{}
    }
    protected void lnk_fire_ext_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;//client_code,
        dt.Columns.Add("fire_ext_applicable");
        dt.Columns.Add("no_of_days");
        dt.Columns.Add("txt_interval");
    


        //foreach (GridViewRow row in grv_dwduction.Rows)
        //{
        //    string item = row.Cells[2].Text;

        //    if (ddl_dedu_iteam.SelectedValue.Equals(item))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Iteam Name Already Added!!')", true);
        //        return;
        //    }
        //}

        int rownum = 0;
        for (rownum = 0; rownum < gv_fire_ext.Rows.Count; rownum++)
        {
            if (gv_fire_ext.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                dr["fire_ext_applicable"] = gv_fire_ext.Rows[rownum].Cells[2].Text;
                dr["no_of_days"] = gv_fire_ext.Rows[rownum].Cells[3].Text;
                dr["txt_interval"] = gv_fire_ext.Rows[rownum].Cells[4].Text;
               

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["fire_ext_applicable"] = ddl_fire_ext.SelectedItem.Text;
        dr["no_of_days"] = txt_no_of_day.Text;
        dr["txt_interval"] = txt_interval_time.Text;
        


        dt.Rows.Add(dr);
        gv_fire_ext.DataSource = dt;
        gv_fire_ext.DataBind();

        ViewState["fire_ext_grp"] = dt;
        //ddl_fire_ext.SelectedValue = "";
        //txt_no_of_day.Text = "0";


    }
    protected void lnk_remove_fire_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //btn_edit.Visible = true;
        //btn_delete.Visible = true;
        //btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;

        if (ViewState["fire_ext_grp"] != null)
        {
            DataTable dt = new DataTable();

            ViewState["fire_ext_grp"] = dt;
           // DataSet DS = (DataSet)ViewState["fire_ext_grp"];

           DataTable dt1 = (DataTable)dt;
            if (dt1.Rows.Count >= 1)
            {
                if (row.RowIndex < dt1.Rows.Count)
                {
                    dt1.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["fire_ext_grp"] = dt1;

            gv_fire_ext.DataSource = dt1;
            gv_fire_ext.DataBind();


        }
    }

    protected void fire_ext_function() 
    {

        try {
            foreach (GridViewRow row in gv_fire_ext.Rows)
            {
                int sr_number = int.Parse(((Label)row.FindControl("lbl_srnumber")).Text);
                string fire_appli = row.Cells[2].Text;
                string no_of_days = row.Cells[3].Text;
                string interval_time = row.Cells[4].Text;


                d.operation("update pay_client_master set fire_ext_applicable = '" + fire_appli + "', no_of_days = '" + no_of_days + "',txt_interval= '" + interval_time + "' where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + txt_clientcode.Text + "' and client_name = '" + txt_clientname.Text + "' and `ADDRESS1` = '" + txt_client_address1.Text + "' and `STATE` = '" + ddl_client_state.SelectedValue + "' and `CITY` = '" + ddl_client_city.SelectedValue + "' and `FILE_NO` = '" + txt_file_no.Text + "' and `total_employee` = '" + txt_employee_total.Text + "'  ");

              //  d.operation("Insert Into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location,category) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + txt_clientcode.Text + "','" + lbl_dsgstate.ToString() + "','" + designation.ToString() + "'," + Convert.ToInt32(em_count) + "," + Convert.ToInt32(wrk_hrs) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),str_to_date('" + startdate + "','%d/%m/%Y') ,str_to_date('" + endsate + "','%d/%m/%Y'),'" + location + "','" + category + "')");
            }

            ddl_fire_ext.SelectedValue = "Not Applicable";
            txt_no_of_day.Text = "0";
            txt_interval_time.Text = "0";

        }
        catch (Exception ex) { throw ex; }
        finally{}
    
    }



    protected void gv_fire_ext_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gv_fire_ext_PreRender(object sender, EventArgs e)
    {
        try
        {
            // UnitGridView.UseAccessibleHeader = false;
            gv_fire_ext.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void deployment_report_Click(object sender, EventArgs e)
    {
        export_xl(1);
    }
    protected void export_xl(int i)
    {

        try
        {
            string sql = "", From_month = "", To_month = "", final_sql="";

            sql = "SELECT client_name, pay_designation_count_history.state, unit_name, pay_designation_count_history.DESIGNATION, pay_designation_count.count AS 'as_per_contract', (pay_designation_count_history.permanent_emp) AS 'actual_deployment', pay_designation_count_history.unit_start_date, pay_designation_count_history.unit_end_date, pay_designation_count_history.month, pay_designation_count_history.year FROM pay_designation_count_history INNER JOIN pay_designation_count ON pay_designation_count.comp_code = pay_designation_count_history.comp_code AND pay_designation_count.client_code = pay_designation_count_history.client_code AND pay_designation_count.unit_code = pay_designation_count_history.unit_code AND pay_designation_count.DESIGNATION = pay_designation_count_history.DESIGNATION INNER JOIN pay_unit_master ON pay_unit_master.comp_code = pay_designation_count_history.comp_code AND pay_unit_master.client_code = pay_designation_count_history.client_code AND pay_unit_master.unit_code = pay_designation_count_history.unit_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_designation_count_history.comp_code AND pay_client_master.client_code = pay_designation_count_history.client_code WHERE pay_designation_count_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_designation_count_history. client_code = '" + txt_clientcode.Text + "' ";
           
            if (txt_from_date.Text.Substring(6) != txt_to_date.Text.Substring(6))
            {
                int month = int.Parse(txt_from_date.Text.Substring(3, 2));
                int month1 = int.Parse(txt_to_date.Text.Substring(3, 2));
                for (int j = month; j <= 12; j++)
                {
                    From_month = From_month + j + ",";

                }
                From_month = From_month.Substring(0, From_month.Length - 1);
                for (int j = 1; j <= month1; j++)
                {
                    To_month = To_month + j + ",";

                }
                To_month = To_month.Substring(0, To_month.Length - 1);
                final_sql = " union " + sql + " and pay_designation_count_history.month IN (" + To_month + ") and pay_designation_count_history.year='" + txt_to_date.Text.Substring(6) + "' AND pay_designation_count.unit_code IS NOT NULL  GROUP BY pay_designation_count_history.month,pay_designation_count_history.unit_code ,pay_designation_count_history. DESIGNATION";
            }
            else
            {
                int month = int.Parse(txt_from_date.Text.Substring(3, 2));
                int month1 = int.Parse(txt_to_date.Text.Substring(3, 2));
                for (int j = month; j <= month1; j++)
                {
                    From_month = From_month + j + ",";

                }
                From_month = From_month.Substring(0, From_month.Length - 1);
            }
            sql = " " + sql + " and pay_designation_count_history.month IN (" + From_month + ") and pay_designation_count_history.year='" + txt_from_date.Text.Substring(6) + "' AND pay_designation_count.unit_code IS NOT NULL   GROUP BY pay_designation_count_history.month,pay_designation_count_history.unit_code , pay_designation_count_history.DESIGNATION " + final_sql;
            MySqlCommand cmd_cg = new MySqlCommand(sql, d.con);
            cmd_cg.CommandTimeout = 300;
            MySqlDataAdapter dscmd = new MySqlDataAdapter(cmd_cg);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;

                if (i == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=DEPLOYMENT_REPORT.xls");
                }


                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds, i);
                Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds, i);
                Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, ds, i);
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
    public class MyTemplate2 : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;



        public MyTemplate2(ListItemType type, DataSet ds, int i)
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
                        lc = new LiteralControl("<table border=1><tr ><th  bgcolor=yellow colspan=11>Deployment Report</th></tr><tr><th>SR NO.</th><th>Client Name</th><th>State Name</th><th>Branch Name</th><th>Designation</th><th>As Per Contract Deployment</th><th>As Per Actual Deployment</th><th>Start Date</th><th>End Date</th><th>Month</th><th>Year</th></tr> ");

                    }

                    break;
                case ListItemType.Item:
                    if (i == 1)
                    {

                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["state"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["as_per_contract"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["actual_deployment"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_start_date"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_end_date"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["month"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["year"].ToString().ToUpper() + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan = 5>Total</td><td>=ROUND(SUM(F3:F" + (ctr + 3) + "),2)</td><td>=ROUND(SUM(G3:G" + (ctr + 3) + "),2)</td></b></tr>";
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
}
