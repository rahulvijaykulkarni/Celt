using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class CompanyMaster1 : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Company Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Company Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Company Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Company Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
        }


        if (!IsPostBack)
        {
            email_grid_view();
            //GST State

            DataTable dt_gststate = new DataTable();
            MySqlDataAdapter adp_gststate = new MySqlDataAdapter("SELECT distinct STATE_NAME,STATE_CODE FROM pay_state_master ORDER BY STATE_NAME", d.con);
            d.con.Open();
            try
            {
                adp_gststate.Fill(dt_gststate);
                if (dt_gststate.Rows.Count > 0)
                {
                    ddl_gst_state.DataSource = dt_gststate;
                    ddl_gst_state.DataTextField = dt_gststate.Columns[0].ToString();
                    ddl_gst_state.DataValueField = dt_gststate.Columns[1].ToString();
                    ddl_gst_state.DataBind();
                }
                dt_gststate.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ddl_gst_state.Items.Insert(0, new ListItem("Select"));
                d.con.Close();
            }

            d.con1.Open();
            try
            {
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddl_state.DataSource = DS;
                ddl_state.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ddl_state.Items.Insert(0, new ListItem("Select", ""));
                d.con1.Close();
            }

            d.con1.Open();
            try
            {
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddl_esic_state.DataSource = DS;
                ddl_esic_state.DataBind();
                ddl_lwf_state.DataSource = DS;
                ddl_lwf_state.DataBind();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                ddl_esic_state.Items.Insert(0, new ListItem("Select", ""));
                ddl_lwf_state.Items.Insert(0, new ListItem("Select", ""));
                d.con1.Close();
            }
            if (!Session["COMP_CODE"].ToString().Equals("0"))
            {
                update_grid();
                //   new_comp_code(null, null);
            }
            else
            {
                btn_delete.Visible = false;
                btn_edit.Visible = false;
                btn_add.Visible = true;
                txtempseriesinit.ReadOnly = false;
                new_comp_code(null, null);
                ViewState["compcode"] = "";
                btn_approval.Visible = false;
            }
            Image4.ImageUrl = null;
            txt_reason_updation.Text = string.Empty;
        }

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (txtempseriesinit.Text == "")
        {

            txtempseriesinit.Focus();
        }
        else
        {
            if (txt_email_id.Text == "" || txt_email_pass.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter User id and Password!!');", true);
                return;
            }
            CompanyBAL cmbl2 = new CompanyBAL();
            int result = 0, result1 = 0;
            string newdate = Convert.ToString(System.DateTime.Now);
            string emp_name = Session["USERNAME"].ToString() + "_" + Session["LOGIN_ID"].ToString();
            string emailpasswd = txt_email_pass.Text;
            try
            {
                new_comp_code(null, null);
                string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
                string pay_company_master = "";
                string pay_zone_master = "";


                if (reporting == "")
                {
                    pay_company_master = "pay_company_master";
                    pay_zone_master = "pay_zone_master";


                    result = cmbl2.CompanyInsert(txt_companycode.Text, txt_companyname.Text, txt_companyaddress1.Text, txt_companyaddress2.Text, txt_companycity.Text, ddl_state.SelectedItem.Text, txt_pin.Text, txt_pfregistrationcode.Text, txt_pfregistrationoffice.Text, txt_esicregistrationcode.Text, txt_companypannumber.Text, txt_companytannumber.Text, txtservicetaxregno.Text, txtempseriesinit.Text, txt_website.Text, txt_contact_no.Text, txt_cin_no.Text, emp_name, newdate, txt_file_no.Text, reporting, txt_sac_housekeeping.Text, txt_sac_security.Text, "1",ddl_paypro_no.SelectedValue);
                }
                else
                {
                    pay_company_master = "pay_company_master_approval";
                    pay_zone_master = "pay_zone_master_approval";

                    result = cmbl2.CompanyInsert(txt_companycode.Text, txt_companyname.Text, txt_companyaddress1.Text, txt_companyaddress2.Text, txt_companycity.Text, ddl_state.SelectedItem.Text, txt_pin.Text, txt_pfregistrationcode.Text, txt_pfregistrationoffice.Text, txt_esicregistrationcode.Text, txt_companypannumber.Text, txt_companytannumber.Text, txtservicetaxregno.Text, txtempseriesinit.Text, txt_website.Text, txt_contact_no.Text, txt_cin_no.Text, emp_name, newdate, txt_file_no.Text, reporting, txt_sac_housekeeping.Text, txt_sac_security.Text, "0","");
                }

                //esic details;
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='ESIC' and CLIENT_CODE is null");
                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3) VALUES('" + txt_companycode.Text + "','ESIC','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
                }
                ViewState["CurrentTable"] = "";
                gv_itemslist.DataSource = null;
                gv_itemslist.DataBind();
                //lwf 
                d.operation("delete from  pay_zone_master  where COMP_CODE = '" + txt_companycode.Text + "' and type='LWF' and CLIENT_CODE is null");
                foreach (GridViewRow row in gv_lwf.Rows)
                {
                    d.operation("INSERT INTO  pay_zone_master (COMP_CODE,type,Field1,Field2,Field3,Field4) VALUES('" + txt_companycode.Text + "','LWF','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");
                }
                ViewState["lwf"] = "";
                gv_lwf.DataSource = null;
                gv_lwf.DataBind();

                //bank_details
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='bank_details' and CLIENT_CODE is null");
                foreach (GridViewRow row in grd_bank_details.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3) VALUES('" + txt_companycode.Text + "','bank_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
                }
                ViewState["grd_bankdetails"] = "";
                grd_bank_details.DataSource = null;
                grd_bank_details.DataBind();
                //branch_details
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='branch_details' and CLIENT_CODE is null");
                foreach (GridViewRow row in grd_branch.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3,field4, Field5) VALUES('" + txt_companycode.Text + "','branch_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "')");
                }
                ViewState["grd_branchdetails"] = "";
                grd_branch.DataSource = null;
                grd_branch.DataBind();
                //insering GSTninfo

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

                    d.operation("Insert Into " + pay_zone_master + " (COMP_CODE,Type,REGION,Field1,Field2) VALUES ('" + txt_companycode.Text + "','GST','" + gststate.ToString() + "','" + gst_addr.ToString() + "','" + gstin.ToString() + "')");
                }
                ViewState["GST_DETAILS"] = "";
                gv_statewise_gst.DataSource = null;
                gv_statewise_gst.DataBind();

                //vikas add for email
                foreach (GridViewRow row in email_reminder_grid.Rows)
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

                    d.operation("Insert Into pay_reminder_email (COMP_CODE,field1,field2,field3,field4,field5,field6,field7,field8,field9) VALUES ('" + txt_companycode.Text + "','" + Field1 + "','" + txt_email + "','" + txt_password1 + "','" + txt_name + "','" + txt_deg + "','" + txt_mobile + "','" + txt_cc + "','" + txt_bcc + "','" + txt_to + "')");
                }
                //minibank details
                foreach (GridViewRow row in gv_data.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,Field1,Type) VALUES('" + txt_companycode.Text + "','" + row.Cells[2].Text + "','minibank')");
                }
                ViewState["minitable"] = "";
                gv_data.DataSource = null;
                gv_data.DataBind();
                if (result > 0)
                {
                    string login_user = d.getsinglestring("select Login_id from pay_user_master where login_id = '" + txt_email_id.Text + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    if (login_user == "" || login_user == null)
                    {
                        if (txt_email_id.Text != "" && txt_email_pass.Text != "")
                        {
                            result1 = d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,ROLE,flag, create_date, password_changed_date,first_login,comp_code,admin_login) VALUES('" + txt_email_id.Text + "','Admin','" + GetSha256FromString(txt_email_pass.Text) + "','admin','A',now(),now(),'0','" + txt_companycode.Text + "','1')");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User already exists, Please select another user!!');", true);
                        cmbl2.CompanyDelete(txt_companycode.Text);
                        return;
                    }
                }
                if (result > 0)
                {
                    if (result1 > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information added successfully!!');", true);
                        txt_email_id.Text = "";
                        txt_email_pass.Text = "";
                        ddl_esic_state.SelectedIndex = 0;
                        txt_esic_address.Text = "";
                        txt_esicregistrationcode.Text = "";
                        txt_bank_name.Text = "";
                        txt_account_no.Text = "";
                        txt_ifsc_code.Text = "";

                        grd_company_files.Visible = false;
                        gv_itemslist.Visible = false;
                        grd_bank_details.Visible = false;
                        grd_branch.Visible = false;
                        gv_statewise_gst.Visible = false;
                        email_reminder_grid.DataSource = null;
                        email_reminder_grid.DataBind();
                        email_grid_view();
                        Upload_File();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User already exists, Please select another user!!');", true);
                        cmbl2.CompanyDelete(txt_companycode.Text);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information adding failed...');", true);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (Session["COMP_CODE"].ToString().Equals("0"))
                {
                    Response.Redirect("Login_Page.aspx");
                }
                update_grid();
            }
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        update_grid();
        try
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
    }
    protected void companyGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["compcode"] = companyGridView.SelectedRow.Cells[0].Text;

        load_fields(companyGridView.SelectedRow.Cells[0].Text, 1);
    }

    private void load_fields(string compcode, int counter)
    {
        d.con1.Open();
        try
        {
            MySqlCommand cmd;
            if (counter == 1)
            {
                cmd = new MySqlCommand("SELECT COMP_CODE,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,PF_REG_NO,PF_REG_OFFICE,COMPANY_PAN_NO,COMPANY_TAN_NO,SERVICE_TAX_REG_NO,EMP_SERIES_INIT,COMP_LOGO,COMPANY_CIN_NO,COMPANY_CONTACT_NO,COMPANY_WEBSITE,pin,file_no,comments,housekeeiing_sac_code,Security_sac_code,paypro_no FROM pay_company_master WHERE COMP_CODE='" + compcode + "' and (approval='' || approval is null)", d.con1);
            }
            else
            {
                cmd = new MySqlCommand("SELECT COMP_CODE,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,PF_REG_NO,PF_REG_OFFICE,COMPANY_PAN_NO,COMPANY_TAN_NO,SERVICE_TAX_REG_NO,EMP_SERIES_INIT,COMP_LOGO,COMPANY_CIN_NO,COMPANY_CONTACT_NO,COMPANY_WEBSITE,pin,file_no,comments,housekeeiing_sac_code,Security_sac_code FROM pay_company_master_approval WHERE COMP_CODE='" + compcode + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "'", d.con1);
            }
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_companycode.Text = dr.GetValue(0).ToString();
                txt_companyname.Text = dr.GetValue(1).ToString();
                txt_companyaddress1.Text = dr.GetValue(2).ToString();
                txt_companyaddress2.Text = dr.GetValue(3).ToString();

                ddl_state.SelectedValue = dr.GetValue(5).ToString();
                get_city_list(null, null);
                txt_companycity.SelectedValue = dr.GetValue(4).ToString();
                txt_pin.Text = dr.GetValue(16).ToString();

                txt_pfregistrationcode.Text = dr.GetValue(6).ToString();
                txt_pfregistrationoffice.Text = dr.GetValue(7).ToString();
                //txt_esicregistrationcode.Text = dr.GetValue(8).ToString();
                txt_companypannumber.Text = dr.GetValue(8).ToString();
                txt_companytannumber.Text = dr.GetValue(9).ToString();
                txtservicetaxregno.Text = dr.GetValue(10).ToString();
                txtempseriesinit.Text = dr.GetValue(11).ToString();
                txtempseriesinit.ReadOnly = true;
                txt_cin_no.Text = dr.GetValue(13).ToString();
                txt_contact_no.Text = dr.GetValue(14).ToString();
                txt_website.Text = dr.GetValue(15).ToString();
                txt_file_no.Text = dr.GetValue(17).ToString();
                string tet = reason_updt(dr.GetValue(18).ToString(), 1);
                if (!dr.GetValue(12).ToString().Equals(""))
                {
                    Image4.ImageUrl = "~/Images/" + dr.GetValue(12).ToString();
                }
                else
                {
                    Image4.ImageUrl = null;
                }
                txt_sac_housekeeping.Text = dr.GetValue(19).ToString();
                txt_sac_security.Text = dr.GetValue(20).ToString();
                ddl_paypro_no.SelectedValue = dr.GetValue(21).ToString();

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        string temp = d.getsinglestring("SELECT Login_id FROM pay_user_master WHERE COMP_CODE='" + compcode + "' and admin_login='1' limit 1");
        if (temp != "")
        {
            txt_email_id.Text = temp;
        }
        else
        { txt_email_id.Text = ""; }

        load_grdview(compcode, counter);
        load_esic_gridview(compcode, counter);
        load_bank_gridview(compcode, counter);
        load_branch_gridview(compcode, counter);
        load_gst_gridview(compcode, counter);
        load_lwf_gridview(compcode, counter);
        load_other_py(compcode,counter);
        gv_statewise_gst.Visible = true;
        files_upload.Visible = true;

        if (counter == 1)
        {
            reason_panel.Visible = true;
            btn_add.Visible = false;
            btn_delete.Visible = true;
            btn_edit.Visible = true;
        }

        //email vikas


        MySqlCommand cmd_e = new MySqlCommand("SELECT Field1, Field2, Field3, Field4, Field5,Field6,Field7,Field8,field9 FROM pay_reminder_email WHERE COMP_CODE='" + compcode + "'  ", d.con);
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
                email_reminder_grid.DataSource = dt;
                email_reminder_grid.DataBind();
            }

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }
    

    }

    private void load_lwf_gridview(string compcode, int counter){
         try
        {

            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3,Field4 from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'LWF'", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3,Field4  from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'LWF'", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            gv_lwf.DataSource = DS1;
            gv_lwf.DataBind();
            gv_lwf.Visible = true;
            ViewState["lwf"] = DS1;
           // ddl_lwf_state.SelectedIndex = ;
            txt_lwf_reg.Text = "";
            txt_lwf_date.Text = "";
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

    }
    private void load_gst_gridview(string compcode, int counter)
    {
        try
        {

            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select REGION,Field1,Field2  from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'GST'", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select REGION,Field1,Field2  from pay_zone_master_approval where comp_Code = '" + compcode + "' and client_code is null and type = 'GST'", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            gv_statewise_gst.DataSource = DS1;
            gv_statewise_gst.DataBind();
            gv_statewise_gst.Visible = true;
            ViewState["GST_DETAILS"] = DS1;
            ddl_gst_state.SelectedIndex = 0;
            txt_gst_addr.Text = "";
            txt_gst_no.Text = "";
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

    }
    private void load_branch_gridview(string compcode, int counter)
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3,Field4,Field5 from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'branch_details'", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3,Field4,Field5 from pay_zone_master_approval where comp_Code = '" + compcode + "' and client_code is null and type = 'branch_details'", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            grd_branch.DataSource = DS1;
            grd_branch.DataBind();
            grd_branch.Visible = true;
            ViewState["grd_branchdetails"] = DS1;
            txt_office_type.Text = "";
            txt_office_address.Text = "";
            txt_office_contact.Text = "";
            txt_start_date.Text = "";
            txt_end_date.Text = "";
            grd_branch.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    private void load_esic_gridview(string compcode, int counter)
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1 as ESIC_STATE,Field2 as ESIC_ADDRESS,Field3 as ESIC_CODE from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'ESIC'", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1 as ESIC_STATE,Field2 as ESIC_ADDRESS,Field3 as ESIC_CODE from pay_zone_master_approval where comp_Code = '" + compcode + "' and client_code is null and type = 'ESIC'", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            gv_itemslist.DataSource = DS1;
            gv_itemslist.DataBind();
            ViewState["CurrentTable"] = DS1;
            ddl_esic_state.SelectedIndex = 0;
            txt_esic_address.Text = "";
            txt_esicregistrationcode.Text = "";
            gv_itemslist.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

    }
    private void load_bank_gridview(string compcode, int counter)
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3 from pay_zone_master where comp_Code = '" + compcode + "' and client_code is null and type = 'bank_details'", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select Field1,Field2,Field3 from pay_zone_master_approval where comp_Code = '" + compcode + "' and client_code is null and type = 'bank_details'", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            grd_bank_details.DataSource = DS1;
            grd_bank_details.DataBind();
            ViewState["grd_bankdetails"] = DS1;
            txt_bank_name.Text = "";
            txt_account_no.Text = "";
            txt_ifsc_code.Text = "";
            grd_bank_details.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    private void load_grdview(string compcode, int counter)
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id, description,concat('~/Images/',file_name) as Value,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date FROM pay_images where comp_Code = '" + compcode + "' and client_code is null", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id, description,concat('~/Images/',file_name) as Value,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date FROM pay_images_approval where comp_Code = '" + compcode + "' and client_code is null", d.con1);
            }
            DataSet DS1 = new DataSet();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            grd_company_files.DataSource = DS1;
            grd_company_files.DataBind();
            txt_document1.Text = "";
            txt_from_date.Text = "";
            txt_to_date.Text = "";
            grd_company_files.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        if (txtempseriesinit.Text == "")
        {
            txtempseriesinit.Focus();
        }
        else
        {
            CompanyBAL cmbl3 = new CompanyBAL();
            int result = 0;
            string newdate = Convert.ToString(System.DateTime.Now);
            string emp_name = Session["USERNAME"].ToString() + "_" + Session["LOGIN_ID"].ToString();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
                string pay_company_master = "";
                string pay_zone_master = "";


                if (reporting == "")
                {
                    pay_company_master = "pay_company_master";
                    pay_zone_master = "pay_zone_master";

                    result = cmbl3.CompanyUpdate(txt_companycode.Text, txt_companyname.Text, txt_companyaddress1.Text, txt_companyaddress2.Text, txt_companycity.Text, ddl_state.SelectedValue, txt_pfregistrationcode.Text, txt_pfregistrationoffice.Text, txt_esicregistrationcode.Text, txt_companypannumber.Text, txt_companytannumber.Text, txtservicetaxregno.Text, txt_website.Text, txt_contact_no.Text, txt_cin_no.Text, txt_pin.Text, emp_name, newdate, txt_file_no.Text, txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString(), reporting, txt_sac_housekeeping.Text, txt_sac_security.Text,ddl_paypro_no.SelectedValue);
                    if (txt_email_pass.Text != "")
                    {
                        d.operation("update pay_user_master set User_password = '" + GetSha256FromString(txt_email_pass.Text) + "', flag ='A', password_changed_date = now(), first_login = '0' where LOGIN_ID = '" + txt_email_id.Text + "' and comp_code = '" + txt_companycode.Text + "' and admin_login='1'");
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company Information updated Successfully!!');", true);
                        txt_email_id.Text = "";
                        txt_email_pass.Text = "";
                        grd_company_files.Visible = false;
                        gv_itemslist.Visible = false;
                        grd_bank_details.Visible = false;
                        grd_branch.Visible = false;
                        gv_statewise_gst.Visible = false;
                        gv_data.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information updation failed...');", true);
                    }
                }
                else
                {
                    new_comp_code(null, null);
                    result = cmbl3.CompanyInsert(txt_companycode.Text, txt_companyname.Text, txt_companyaddress1.Text, txt_companyaddress2.Text, txt_companycity.Text, ddl_state.SelectedItem.Text, txt_pin.Text, txt_pfregistrationcode.Text, txt_pfregistrationoffice.Text, txt_esicregistrationcode.Text, txt_companypannumber.Text, txt_companytannumber.Text, txtservicetaxregno.Text, txtempseriesinit.Text, txt_website.Text, txt_contact_no.Text, txt_cin_no.Text, emp_name, newdate, txt_file_no.Text, reporting, txt_sac_housekeeping.Text, txt_sac_security.Text, "0","");
                    pay_company_master = "pay_company_master_approval";
                    pay_zone_master = "pay_zone_master_approval";
                }
                //esic details;
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='ESIC' and CLIENT_CODE is null");
                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3) VALUES('" + txt_companycode.Text + "','ESIC','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
                }
                ViewState["CurrentTable"] = "";
                gv_itemslist.DataSource = null;
                gv_itemslist.DataBind();
                //bank_details
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='bank_details' and CLIENT_CODE is null");
                foreach (GridViewRow row in grd_bank_details.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3) VALUES('" + txt_companycode.Text + "','bank_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "')");
                }
                grd_bank_details.DataSource = null;
                grd_bank_details.DataBind();
                ViewState["grd_bankdetails"] = "";
                //lwf
                d.operation("delete from  pay_zone_master  where COMP_CODE = '" + txt_companycode.Text + "' and type='LWF' and CLIENT_CODE is null");
                foreach (GridViewRow row in gv_lwf.Rows)
                {
                    d.operation("INSERT INTO  pay_zone_master(COMP_CODE,type,Field1,Field2,Field3,Field4) VALUES('" + txt_companycode.Text + "','LWF','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "')");
                }
                gv_lwf.DataSource = null;
                gv_lwf.DataBind();
                ViewState["lwf"] = "";

                //branch_details
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='branch_details' and CLIENT_CODE is null");
                foreach (GridViewRow row in grd_branch.Rows)
                {
                    d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,type,Field1,Field2,Field3,field4, Field5) VALUES('" + txt_companycode.Text + "','branch_details','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "')");
                }
                ViewState["grd_branchdetails"] = "";
                grd_branch.DataSource = null;
                grd_branch.DataBind();

                ///////04-11-19
                d.operation("delete from pay_reminder_email where comp_code = '" + txt_companycode.Text + "' and CLIENT_CODE is null ");

                foreach (GridViewRow row in email_reminder_grid.Rows)
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

                    d.operation("Insert Into pay_reminder_email (COMP_CODE,field1,field2,field3,field4,field5,field6,field7,field8,field9) VALUES ('" + txt_companycode.Text + "','" + Field1 + "','" + txt_email + "','" + txt_password1 + "','" + txt_name + "','" + txt_deg + "','" + txt_mobile + "','" + txt_cc + "','" + txt_bcc + "','" + txt_to + "')");
                }
                email_grid_view();
                foreach (GridViewRow row in gv_data.Rows)
                {

                    //d.operation(" UPDATE `pay_zone_master` SET Field13 = '" + row.Cells[2].Text + "' WHERE comp_code = '" + txt_companycode.Text + "' and Type='minibank'");
                    d.operation("INSERT INTO  pay_zone_master (COMP_CODE,Field1,Type) VALUES('" + txt_companycode.Text + "','" + row.Cells[3].Text + "','minibank')");
                }

//////////////////////
                //insering GSTninfo
                d.operation("delete from " + pay_zone_master + " where COMP_CODE = '" + txt_companycode.Text + "' and type='GST' and CLIENT_CODE is null");

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

                    d.operation("Insert Into " + pay_zone_master + " (COMP_CODE,Type,REGION,Field1,Field2) VALUES ('" + txt_companycode.Text + "','GST','" + gststate.ToString() + "','" + gst_addr.ToString() + "','" + gstin.ToString() + "')");
                }
                ViewState["GST_DETAILS"] = "";
                gv_statewise_gst.DataSource = null;
                gv_statewise_gst.DataBind();


                Upload_File();
            }
            catch (Exception ee)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ee.Message + "');", true);
            }
            finally
            {
                update_grid();
            }
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {

        //{
        string cmpcode = txt_companycode.Text;
        MySqlCommand cmd_1 = new MySqlCommand("Select CLIENT_CODE from pay_client_master where comp_code='" + cmpcode + "' ", d.con1);
        if (d.con1.State == ConnectionState.Open)
        {
            d.con1.Close();
            d.con1.Dispose();
            d.con1.ClearPoolAsync(d.con1);
        }
        d.con1.Open();
        MySqlDataReader dr_1 = cmd_1.ExecuteReader();
        if (dr_1.Read())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please delete clients of this company first.');", true);

        }
        else
        {

            CompanyBAL cmbl4 = new CompanyBAL();
            int result = 0;
            try
            {
               
                string temp = d.getsinglestring("Select EMP_CODE from pay_employee_master where COMP_CODE='" + txt_companycode.Text + "' limit 1");
                if (temp != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee against Company exist can not delete this Company');", true);
                }
                else
                {
                    result = cmbl4.CompanyDelete(txt_companycode.Text);
                    d.operation("delete from pay_user_master where COMP_CODE='" + txt_companycode.Text + "' and admin_login='1'");
                    d.operation("delete from pay_reminder_email where comp_code = '" + txt_companycode.Text + "'");
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information deleted successfully!!');", true);
                        d.reset(this);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information deletion failed...');", true);
                    }
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
                }

            }

            catch (Exception ex) { throw ex; }
            finally
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company information deleted successfully!!');", true);
                d.con1.Close();
                update_grid();
            }
        }
    }


    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void companyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.companyGridView, "Select$" + e.Row.RowIndex);
        }
    }


    public void new_comp_code(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            txt_companycode.Text = "C01";
            string temp = d.getsinglestring("SELECT MAX(CAST(SUBSTR(COMP_CODE, 2) AS unsigned))+1 FROM  pay_company_master");
            string temp1 = d.getsinglestring("SELECT IFNULL(MAX(CAST(SUBSTR(COMP_CODE, 2) AS unsigned))+1,0) FROM  pay_company_master_approval");
            if (int.Parse(temp1) > int.Parse(temp))
            {
                temp = temp1;
            }
            if (temp != "")
            {
                int max_compcode = int.Parse(temp);
                if (max_compcode < 10)
                {
                    txt_companycode.Text = "C0" + max_compcode;
                }
                else if (max_compcode > 9 && max_compcode < 100)
                {
                    txt_companycode.Text = "C" + max_compcode;
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    private void update_grid()
    {
        d.con1.Open();
        try
        {
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT * FROM pay_company_master where (approval='' || approval is null) ORDER BY COMP_CODE", d.con1);
            DataSet DS1 = new DataSet();
            MySqlDataAdapter1.Fill(DS1);
            companyGridView.DataSource = DS1;
            companyGridView.DataBind();
            d.con1.Close();

            //Reporting
            load_reporting_grdv();
            //approval status
            approval_grid();

            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = true;
            txtempseriesinit.ReadOnly = false;
            files_upload.Visible = false;
            txt_reason_updation.Text = "";
            reason_panel.Visible = false;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            txt_companyname.Text = "";
            txt_companyaddress1.Text = "";
            txt_companyaddress2.Text = "";
            txt_companycity.Text = "";
            ddl_state.SelectedIndex = 0;
            txt_pfregistrationcode.Text = "";
            txt_pfregistrationoffice.Text = "";
            txt_esicregistrationcode.Text = "";
            txt_companypannumber.Text = "";
            txt_companytannumber.Text = "";
            txt_email_id.Text = "";
            txt_email_pass.Text = "";
            txtservicetaxregno.Text = "";
            txtempseriesinit.Text = "";
            txtempseriesinit.ReadOnly = false;
            txt_cin_no.Text = "";
            txt_website.Text = "";
            txt_contact_no.Text = "";
            txt_pin.Text = "";
            Image4.ImageUrl = null;
            txt_file_no.Text = "";
            grd_company_files.DataSource = null;
            grd_company_files.DataBind();
            grd_bank_details.DataSource = null;
            grd_bank_details.DataBind();
            gv_itemslist.DataSource = null;
            gv_itemslist.DataBind();
            gv_data.DataSource = null;
            gv_data.DataBind();
            txt_document1.Text = "";
            txt_from_date.Text = "";
            txt_to_date.Text = "";
            txt_reason_updation.Text = "";
            txt_companycode.Text = "";
            btn_approval.Visible = false;
            gv_statewise_gst.DataSource = null;
            gv_statewise_gst.DataBind();
            txt_sac_housekeeping.Text = "";
            txt_sac_security.Text = "";
        }
    }

    protected void txtempseriesinit_TextChanged(object sender, EventArgs e)
    {
        string user_pass = txt_email_pass.Text;
        try
        {
            string temp = d.getsinglestring("SELECT EMP_SERIES_INIT FROM pay_company_master WHERE EMP_SERIES_INIT='" + txtempseriesinit.Text + "'");
            if (temp != "")
            {
                txtempseriesinit.Text = "";
                txtempseriesinit.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Another Employee Series. It is already used');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            txt_email_pass.Text = user_pass;
        }
    }
    protected void image_click(object sender, ImageClickEventArgs e)
    {
        if (txt_companycode.Text != "")
        {
            Session["FROM_INVOICE"] = txt_companycode.Text;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "javascript:alert('Please select atleast one Company !!!')", true);
        }

    }
    protected void Upload_File()
    {
        if (Header_photo_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Header_photo_upload.FileName);
            if (fileExt == ".jpg" || fileExt == ".png")
            {
                if (txt_companycode.Text != "")
                {
                    string fileName = Path.GetFileName(Header_photo_upload.PostedFile.FileName);
                    Header_photo_upload.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                    File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + txt_companycode.Text + "_1" + fileExt, true);
                    File.Delete(Server.MapPath("~/Images/") + fileName);
                    d.operation("UPDATE pay_company_master SET COMP_LOGO = '" + txt_companycode.Text + "_1" + fileExt + "' where COMP_CODE = '" + txt_companycode.Text + "'");
                   //rahul 
                    //string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
                    //if (reporting == "")
                    //{
                    //    d.operation("UPDATE pay_company_master SET COMP_LOGO = '" + txt_companycode.Text + "_1" + fileExt + "' where COMP_CODE = '" + txt_companycode.Text + "'");
                    //}
                    //else
                    //{
                    //    d.operation("UPDATE pay_company_master_approval SET COMP_LOGO = '" + txt_companycode.Text + "_1" + fileExt + "' where COMP_CODE = '" + txt_companycode.Text + "'");
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }
        }
    }

    protected void get_city_list(object sender, EventArgs e)
    {
        //string name=  ddl_state.SelectedItem.ToString();
        txt_companycity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_companycity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

        // txt_particular.Items.Insert(0, "Select");
        txt_companycity.Items.Insert(0, new ListItem("Select", ""));

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

    protected void companyGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            //companyGridView.UseAccessibleHeader = false;
            companyGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
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
    protected void grd_company_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_company_files.UseAccessibleHeader = false;
            grd_company_files.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    protected void grd_branch_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_branch.UseAccessibleHeader = false;
            grd_branch.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_statewise_gst_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_statewise_gst.UseAccessibleHeader = false;
            gv_statewise_gst.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        upload_documents(document1_file, txt_document1.Text, "_doc");
    }

    private void upload_documents(FileUpload document_file, string file_name, string file1)
    {
        string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
        string temp = d.getsinglestring("SELECT comp_code FROM pay_company_master WHERE comp_code='" + txt_companycode.Text + "'");
        if (temp != "")
        {
            if (document_file.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(document_file.FileName);
                if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".pdf")
                {
                    string fileName = Path.GetFileName(document_file.PostedFile.FileName);
                    document_file.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                    File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + txt_companycode.Text + "_" + txt_document1.Text.Replace(" ", "_") + fileExt, true);
                    File.Delete(Server.MapPath("~/Images/") + fileName);

                    if (reporting == "")
                    {
                        d.operation("insert into pay_images (comp_code,description,file_name,start_date,end_date,created_by,created_date) values ('" + txt_companycode.Text + "','" + txt_document1.Text + "','" + txt_companycode.Text + "_" + txt_document1.Text.Replace(" ", "_") + fileExt + "',str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");
                    }
                    else
                    {
                        d.operation("insert into pay_images_approval (comp_code,description,file_name,start_date,end_date,created_by,created_date) values ('" + txt_companycode.Text + "','" + txt_document1.Text + "','" + txt_companycode.Text + "_" + txt_document1.Text.Replace(" ", "_") + fileExt + "',str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
                }
            }
            if (reporting == "")
            {
                load_grdview(ViewState["compcode"].ToString(), 1);
            }
            else { load_grdview(ViewState["compcode"].ToString(), 2); }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Company first !!!')", true);
        }
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
        string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
        if (reporting == "")
        {
            int item = (int)grd_company_files.DataKeys[e.RowIndex].Value;
            string temp = d.getsinglestring("SELECT file_name FROM pay_images WHERE id=" + item);
            if (temp != "")
            {
                File.Delete(Server.MapPath("~/Images/") + temp);
            }
            d.operation("delete from pay_images WHERE id=" + item);
            load_grdview(ViewState["compcode"].ToString(), 1);

        }
        else
        {
            int item = (int)grd_company_files.DataKeys[e.RowIndex].Value;
            string temp = d.getsinglestring("SELECT file_name FROM pay_images_approval WHERE id=" + item);
            if (temp != "")
            {
                File.Delete(Server.MapPath("~/Images/") + temp);
            }
            d.operation("delete from pay_images_approval WHERE id=" + item);
            load_grdview(ViewState["compcode"].ToString(), 2);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
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
    protected void btn_approval_Click(object sender, EventArgs e)
    {
        string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
        if (reporting == "")
        {
            d.operation("Insert into pay_company_master (comp_code,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,pin,PF_REG_NO,PF_REG_OFFICE,ESIC_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO, SERVICE_TAX_REG_NO, EMP_SERIES_INIT,COMPANY_WEBSITE,COMPANY_CONTACT_NO,COMPANY_CIN_NO,Login_Person,LastModifyDate,file_no,approval,housekeeiing_sac_code,Security_sac_code) Select comp_code,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,pin,PF_REG_NO,PF_REG_OFFICE,ESIC_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO, SERVICE_TAX_REG_NO, EMP_SERIES_INIT,COMPANY_WEBSITE,COMPANY_CONTACT_NO,COMPANY_CIN_NO,Login_Person,LastModifyDate,file_no,approval,housekeeiing_sac_code,Security_sac_code From pay_company_master_approval Where comp_code = '" + txt_companycode.Text + "' and comp_code not in (Select comp_code from pay_company_master)");
            d.operation("Delete from pay_company_master_approval where comp_code = '" + txt_companycode.Text + "'");

            d.operation("Insert into pay_zone_master (COMP_CODE,Type,REGION,Field1,Field2,Field3,Field4,Field5) Select COMP_CODE,Type,REGION,Field1,Field2,Field3,Field4,Field5 From pay_zone_master_approval Where comp_code = '" + txt_companycode.Text + "' ");
            d.operation("Delete from pay_zone_master_approval where comp_code = '" + txt_companycode.Text + "' and client_code is null");

            d.operation("Insert into pay_images (comp_code,description,file_name,start_date,end_date,created_by,created_date) Select comp_code,description,file_name,start_date,end_date,created_by,created_date From pay_images_approval Where comp_code = '" + txt_companycode.Text + "' ");
            d.operation("Delete from pay_images_approval where comp_code = '" + txt_companycode.Text + "' and client_code is null");

            d.operation("update pay_company_master set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + txt_companycode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%'");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approved successfully!!');", true);
            txt_email_id.Text = "";
            txt_email_pass.Text = "";
            ddl_esic_state.SelectedIndex = 0;
            txt_esic_address.Text = "";
            txt_esicregistrationcode.Text = "";
            txt_bank_name.Text = "";
            txt_account_no.Text = "";
            txt_ifsc_code.Text = "";

            gv_itemslist.DataSource = null;
            gv_itemslist.DataBind();
            grd_bank_details.DataSource = null;
            grd_bank_details.DataBind();
            grd_branch.DataSource = null;
            grd_branch.DataBind();
            gv_statewise_gst.DataSource = null;
            gv_statewise_gst.DataBind();

        }
        else
        {
            d.operation("update pay_company_master_approval set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + txt_companycode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%'");
        }
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT id from pay_company_master where comp_code = '" + txt_companycode.Text + "' and (approval = '' OR approval is null) order by id", d.con);
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
                    d.operation("delete from pay_company_master where id = " + dr["id"].ToString());
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
        update_grid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["compcode"] = GridView1.SelectedRow.Cells[1].Text;
        load_fields(GridView1.SelectedRow.Cells[1].Text, 2);
        btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
        e.Row.Cells[2].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item = (int)GridView1.DataKeys[e.RowIndex].Value;
        Session["Approval_Id"] = item;
        ModalPopupExtender2.Show();
        update_grid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["compcode"] = GridView1.Rows[e.NewEditIndex].Cells[3].Text;
        load_fields(GridView1.Rows[e.NewEditIndex].Cells[3].Text, 2);
        btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true;
        load_reporting_grdv();
    }
    private void load_reporting_grdv()
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id, COMP_CODE as 'COMP CODE',COMPANY_NAME AS 'NAME',CITY,STATE,PF_REG_NO AS 'PF REGISTRATION NO',PF_REG_OFFICE AS 'PF REGISTRATION OFFICE',COMPANY_PAN_NO AS 'PAN NO',COMPANY_TAN_NO AS 'TAN NO',SERVICE_TAX_REG_NO AS 'GST REGISTRATION NO',EMP_SERIES_INIT AS 'EMPLOYEE SERIES INIT',COMPANY_CIN_NO AS 'COMPANY CIN NO',COMPANY_CONTACT_NO AS 'COMPANY CONTACT NO',COMPANY_WEBSITE AS 'WEBSITE',Login_Person AS 'REQUESTED BY' FROM pay_company_master_approval where SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "' ORDER BY COMP_CODE", d.con1);
            d.con1.Open();
            DataSet DS1 = new DataSet();
            MySqlDataAdapter1.Fill(DS1);
            GridView1.DataSource = DS1;
            GridView1.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        { d.con1.Close(); }
    }

    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
      catch { }
        gv_itemslist.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("ESIC_STATE");
        dt.Columns.Add("ESIC_ADDRESS");
        dt.Columns.Add("ESIC_CODE");
        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["ESIC_STATE"] = gv_itemslist.Rows[rownum].Cells[2].Text;
                dr["ESIC_ADDRESS"] = gv_itemslist.Rows[rownum].Cells[3].Text;
                dr["ESIC_CODE"] = gv_itemslist.Rows[rownum].Cells[4].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["ESIC_STATE"] = ddl_esic_state.SelectedValue;
        dr["ESIC_ADDRESS"] = txt_esic_address.Text;
        dr["ESIC_CODE"] = txt_esicregistrationcode.Text;
        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        ViewState["CurrentTable"] = dt;
        ddl_esic_state.SelectedIndex = 0;
        txt_esic_address.Text = "";
        txt_esicregistrationcode.Text = "";
    }

    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_itemslist, "Select$" + e.Row.RowIndex);

        }
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
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
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
        }

    }
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
    protected void lnk_branch_details_Click(object sender, EventArgs e)
    {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
        grd_branch.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Field1");
        dt.Columns.Add("Field2");
        dt.Columns.Add("Field3");
        dt.Columns.Add("Field4");
        dt.Columns.Add("Field5");
        int rownum = 0;
        for (rownum = 0; rownum < grd_branch.Rows.Count; rownum++)
        {
            if (grd_branch.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["Field1"] = grd_branch.Rows[rownum].Cells[2].Text;
                dr["Field2"] = grd_branch.Rows[rownum].Cells[3].Text;
                dr["Field3"] = grd_branch.Rows[rownum].Cells[4].Text;
                dr["Field4"] = grd_branch.Rows[rownum].Cells[5].Text;
                dr["Field5"] = grd_branch.Rows[rownum].Cells[6].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["Field1"] = txt_office_type.Text;
        dr["Field2"] = txt_office_address.Text;
        dr["Field3"] = txt_office_contact.Text;
        dr["Field4"] = txt_start_date.Text;
        dr["Field5"] = txt_end_date.Text;
        dt.Rows.Add(dr);
        grd_branch.DataSource = dt;
        grd_branch.DataBind();
        ViewState["grd_branchdetails"] = dt;
        txt_office_type.Text = "";
        txt_office_address.Text = "";
        txt_office_contact.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
    }
    protected void grd_branch_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_remove_branch_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        if (ViewState["grd_branchdetails"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["grd_branchdetails"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["grd_branchdetails"] = dt;
            grd_branch.DataSource = dt;
            grd_branch.DataBind();
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
                if (e.Row.Cells[i].Text == "&amp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }
    protected void lnk_add_gst_Click(object sender, EventArgs e)
    {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
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

        gv_statewise_gst.Visible = true;
        ViewState["gsttable"] = dt_gst;

        ddl_gst_state.SelectedIndex = 0;
        txt_gst_addr.Text = "";
        txt_gst_no.Text = "";
    }
    protected void lnkbtn_gst_removeitem_Click(object sender, EventArgs e)
    {
        try{ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);}
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
    protected void lnkbtn_gst_edititem_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row1 = (GridViewRow)lb.NamingContainer;
        int rowID = row1.RowIndex;

        Label lbl_gststate = (Label)row1.FindControl("lbl_gststate");
        string gststate = (lbl_gststate.Text);

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
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    public void approval_grid()
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id as 'ID', COMP_CODE as 'COMP CODE',status as 'STATUS',comment as 'COMMENT',created_by as 'REJECTED BY' FROM approval_status", d.con1);
            DataSet DS1 = new DataSet();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            GridView2.DataSource = DS1;
            GridView2.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
  protected void lnk_lwf_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        gv_lwf.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("field1");
        dt.Columns.Add("field2");
        dt.Columns.Add("field3");
        dt.Columns.Add("field4");
        foreach (GridViewRow row in gv_lwf.Rows)
        {
            string state = row.Cells[2].Text;
            if (ddl_lwf_state.SelectedValue.Equals(state))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Insert 2 same  State for one Company!!')", true);
                return;
            }
        }
        int rownum = 0;
        for (rownum = 0; rownum < gv_lwf.Rows.Count; rownum++)
        {
            if (gv_lwf.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["field1"] = gv_lwf.Rows[rownum].Cells[2].Text;
                dr["field2"] = gv_lwf.Rows[rownum].Cells[3].Text;
                dr["field3"] = gv_lwf.Rows[rownum].Cells[4].Text;
                dr["field4"] = gv_lwf.Rows[rownum].Cells[5].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["field1"] = ddl_lwf_state.SelectedValue;
        dr["field2"] = txt_lwf_reg.Text;
        dr["field3"] = txt_lwf_date.Text;
        dr["field4"] = txt_office.Text;
        dt.Rows.Add(dr);
        gv_lwf.DataSource = dt;
        gv_lwf.DataBind();
        ViewState["lwf"] = dt;
        ddl_lwf_state.SelectedIndex = 0;
        txt_lwf_reg.Text = "";
        txt_lwf_date.Text = "";
        txt_office.Text = "";
    }
    protected void lnkbtn_removeitem_Click1(object sender, EventArgs e)
    {
        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        if (ViewState["lwf"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["lwf"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
          //  ViewState["CurrentTable"] = dt;
            ViewState["lwf"] = dt;
            gv_lwf.DataSource = dt;
            gv_lwf.DataBind();
        }
    }
    protected void gv_lwf_PreRender(object sender, EventArgs e)
    {
        try
        {
            //companyGridView.UseAccessibleHeader = false;
            gv_lwf.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void email_grid_view() 
    {
        string dep = "";
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
            MySqlDataAdapter grd = new MySqlDataAdapter(" select '" + dep + "' as 'field1','' as 'field2','' as 'field3','' as 'field4','' as 'field5','' as 'Field6','' as 'Field7','' as 'Field8','' as 'field9' FROM pay_reminder_email limit 1 ", d.con1);
            grd.Fill(dt_item);

        }
        email_reminder_grid.DataSource = dt_item;
        email_reminder_grid.DataBind();  
    
    
    }
    protected void lnk_button_Click(object sender, EventArgs e)
    {
        hidtab.Value = "10";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_data.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("COMP_CODE");
        dt.Columns.Add("field1");
       
        int rownum = 0;
        for (rownum = 0; rownum < gv_data.Rows.Count; rownum++)
        {
            if (gv_data.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["COMP_CODE"] = gv_data.Rows[rownum].Cells[2].Text;
                dr["field1"] = gv_data.Rows[rownum].Cells[3].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();
        dr["COMP_CODE"] = txt_companycode.Text;
        dr["field1"] = txt_bb.Text;
     
        dt.Rows.Add(dr);
        gv_data.DataSource = dt;
        gv_data.DataBind();
        ViewState["minitable"] = dt;
        txt_bb.Text = "";

        
    }
    protected void gv_data_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_data.UseAccessibleHeader = false;
            gv_data.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
   
    protected void lnkbtn_removeitem_minibank_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        btn_edit.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["minitable"] != null)
        {
            DataTable dt = (DataTable)ViewState["minitable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["minitable"] = dt;
            gv_data.DataSource = dt;
            gv_data.DataBind();
        }
    }
    protected void gv_data_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }
    protected void load_other_py(string compcode, int counter)
    {
        try
        {
            MySqlDataAdapter MySqlDataAdapter1;
            if (counter == 1)
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select comp_code,field1 ,type from pay_zone_master where comp_code = '" + compcode + "' and type='minibank' ", d.con1);
            }
            else
            {
                MySqlDataAdapter1 = new MySqlDataAdapter("select comp_code,field1 ,type from pay_zone_master where comp_code = '" + compcode + "' and type='minibank' ", d.con1);
            }
            DataTable DS1 = new DataTable();
            d.con1.Open();
            MySqlDataAdapter1.Fill(DS1);
            gv_data.DataSource = DS1;
            gv_data.DataBind();
            ViewState["minitable"] = DS1;
            gv_data.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
}