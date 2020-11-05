using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;



public partial class AddNewEmployee : System.Web.UI.Page
{
    ReportDocument crystalReport = new ReportDocument();
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    DAL d_shift = new DAL();
    EmployeeBAL ebl2 = new EmployeeBAL();
    string birthdate;
    string joining_date;

    string check_left = "";
    string header_date = "";

    public string gv_app_query = "SELECT emp_code,client_name, joining_date, Grade_code, client_wise_state AS 'state_name', unit_name AS 'branch_name', emp_name,ap_status AS 'status' , reject_reason as 'Reason' FROM pay_client_master INNER JOIN pay_employee_master ON pay_client_master.comp_code = pay_employee_master.comp_code AND pay_client_master.client_code = pay_employee_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code where pay_client_master.client_active_close = '0' ", gv_app_where = "";
    public string unifom_hold_count = "0";
    public string left_emp_count = "0";
    public string emp_listcount = "0";

    public string rem_emp_count = "0", appro_emp_count = "0", appro_emp_legal = "0", reject_emp_legal = "0", branch_list = "0", employee_list = "0" ,appro_emp_bank = "0", rejected_bank_emp = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_fine.ReadOnly = true;
        panel_visibility();
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["comp_code"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["comp_code"].ToString()) == "R")
        {
            btndelete.Visible = false;
            btnupdate.Visible = false;
            btn_add.Visible = false;
            //btnexcelexport.Visible = false;
            btnhelp.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["comp_code"].ToString()) == "U")
        {
            btndelete.Visible = false;
            btn_add.Visible = false;
            // btnexcelexport.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["comp_code"].ToString()) == "C")
        {
            btndelete.Visible = false;
            //btnexcelexport.Visible = false;
            btnhelp.Visible = false;
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["comp_code"].ToString()) != "D")
        {
        }


        if (!IsPostBack)
        {
            d.con1.Open();
            ViewState["id"] = "0";
            ViewState["permanent"] = "0";
            ViewState["rejoin_update"] = "0";
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT role_name FROM pay_role_master where comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataTextField = "role_name";
                DropDownList1.DataBind();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }
            DropDownList1.Items.Insert(0, new ListItem("--Select Role--", ""));
            btn_Leftemp_Export.Visible = false;
            ddl_permstate.Items.Clear();
            ddl_state.Items.Clear();
            //  ddl_location.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    ddl_permstate.Items.Add(dr_item1[0].ToString());
                    ddl_state.Items.Add(dr_item1[0].ToString());
                    //  ddl_location.Text=(dr_item1[0].ToString());
                }
                dr_item1.Close();
                d1.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }
            ddl_permstate.Items.Insert(0, new ListItem("Select"));
            ddl_state.Items.Insert(0, new ListItem("Select"));
            txt_presentcity.Items.Insert(0, new ListItem("Select"));
            //  ddl_location.Items.Insert(0, new ListItem("Select"));
            //ddl_location_city.Items.Insert(0, new ListItem("Select"));
            ddl_unitcode.Items.Insert(0, new ListItem("Select", "Select"));
            txt_permanantcity.Items.Insert(0, new ListItem("Select"));
            Session["EMP_CODE"] = "0";
            if (Session["EMP_CODE"].ToString() != "")
            {
                getEmployee(Session["EMP_CODE"].ToString());
                Session["EMP_CODE"] = "";

            }
            else
            {
                // btnnew_Click();
            }
            // clien_namelist();
            newpanel.Visible = true;
            panel_dispatch.Visible = false;
            client_list();
            txt_reason_updation.Text = string.Empty;
            reason_panel.Visible = false;
            btnhelp_Click(sender, e);//vinod to load employee master faster
            ViewState["left"] = "0";
            Panel_app_gv.Visible = false;
            ViewState["rem_emp_count"] = 0;
            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            ViewState["appro_emp_bank"] = 0;
            ViewState["rejected_bank_emp"] = 0;
            //add 25/04/2019
            ViewState["unifom_hold_count"] = 0;
            ViewState["vakant_branch"] = 0;
            ViewState["left_emp_count"] = 0;
            ViewState["rejoin_empcode"] = 0;
            ViewState["rejoin_date"] = "";
            panel_approval.Visible = false;
            ViewState["rejoin_empcode"] = 0;
        }


        btnupdate.Visible = false;
        btndelete.Visible = false;
        btn_add.Visible = true;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        newpanel.Visible = false;
        reliveemp_notification();
        //  Button1.Visible = false;
        btn_approve.Visible = false;

        rem_emp_count = ViewState["rem_emp_count"].ToString();
        appro_emp_count = ViewState["appro_emp_count"].ToString();
        appro_emp_legal = ViewState["appro_emp_legal"].ToString();
        reject_emp_legal = ViewState["reject_emp_legal"].ToString();
        appro_emp_bank = ViewState["appro_emp_bank"].ToString();
        rejected_bank_emp = ViewState["rejected_bank_emp"].ToString();

        //add 25/04/2019
        unifom_hold_count = ViewState["unifom_hold_count"].ToString();
        left_emp_count = ViewState["left_emp_count"].ToString();
        branch_list = ViewState["vakant_branch"].ToString();
        //vikas add 03/04/2019
        if (reason_panel.Visible == true)
        {
            btn_approve.Visible = true;
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btn_add.Visible = false;
        }
        // dedignationpanel.Visible = false;
        panel_adhar_card.Visible = false;
        btndelete.Visible = false;

    }
    //protected void fill_enable(string flag)
    //{
    //    if (flag == "true")
    //    {
    //        //    ddl_employee_type.Enabled = false;
    //        //    ddl_department.Enabled = false;
    //        //    ddl_unit_client.Enabled = false;
    //        //    ddl_clientwisestate.Enabled = false;
    //        //    ddl_unitcode.Enabled = false;
    //        //    ddl_grade.Enabled = false;
    //        //    txt_eecode.Enabled = false;
    //        //    txt_eename.Enabled = false;
    //        //
    //    }
    //    else
    //    {

    //        //ddl_employee_type.Enabled = true;
    //        //ddl_department.Enabled = true;
    //        //ddl_unit_client.Enabled = true;
    //        //ddl_clientwisestate.Enabled = true;
    //        //ddl_unitcode.Enabled = true;
    //        //ddl_grade.Enabled = true;
    //        //txt_eecode.Enabled = true;
    //        //txt_eename.Enabled = true;
    //    }
    //}
    public void designation_unitwise(object sender, EventArgs e)
    {
        //  ddl_location_city.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT  UNIT_CITY FROM pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  UNIT_CODE = '" + ddl_unitcode.SelectedValue + "' ", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location_city.Text = (dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        ddl_grade.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //    MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT GRADE_CODE, CONCAT(GRADE_DESC,'-',Working_Hours) as GRADE_DESC FROM pay_designation_count  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and UNIT_CODE='" + ddl_unitcode.SelectedValue + "'", d.con);

        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_grade.DataSource = dt_item;
                ddl_grade.DataTextField = dt_item.Columns[1].ToString();
                ddl_grade.DataValueField = dt_item.Columns[0].ToString();
                ddl_grade.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            ddl_grade.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            newpanel.Visible = true;
            if (reason_panel.Visible == true)
            {
                btnupdate.Visible = true;
                btndelete.Visible = true;
                btn_add.Visible = false;
            }
            else
            {
                btnupdate.Visible = false;
                btndelete.Visible = false;
                btn_add.Visible = true;
            }
        }

    }
    public void client_list()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ") and client_active_close='0'", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unit_client.DataSource = dt_item;
                ddl_unit_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_unit_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_unit_client.DataBind();


                ddlunitclient1.DataSource = dt_item;
                ddlunitclient1.DataTextField = dt_item.Columns[1].ToString();
                ddlunitclient1.DataValueField = dt_item.Columns[0].ToString();
                ddlunitclient1.DataBind();

                ddl_app_client.DataSource = dt_item;
                ddl_app_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_app_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_app_client.DataBind();

                //dispatch client

                ddl_client_name_dispatch.DataSource = dt_item;
                ddl_client_name_dispatch.DataTextField = dt_item.Columns[1].ToString();
                ddl_client_name_dispatch.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_name_dispatch.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_unit_client.Items.Insert(0, "Select");
            ddlunitclient1.Items.Insert(0, "Select");
            ddl_client_name_dispatch.Items.Insert(0, "Select");
            ddl_app_client.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }
    protected void get_city_list1(object sender, EventArgs e)
    {
        ddl_location.Text = ddl_clientwisestate.SelectedValue;
        newpanel.Visible = true;


        ddl_unitcode.Items.Clear();

        MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and STATE_NAME='" + ddl_clientwisestate.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE in(SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_unit_client.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_clientwisestate.SelectedValue + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) AND branch_status = 0  ", d.con);
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            ddl_unitcode.DataSource = ds1.Tables[0];
            ddl_unitcode.DataValueField = "UNIT_CODE";
            ddl_unitcode.DataTextField = "CUNIT";
            ddl_unitcode.DataBind();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
        ddl_unitcode.Items.Insert(0, new ListItem("Select"));
        newpanel.Visible = true;

        if (reason_panel.Visible == true)
        {
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btn_add.Visible = false;
        }
        else
        {
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btn_add.Visible = true;
        }

    }
    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_clientwisestate.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state in(SELECT DISTINCT (pay_client_state_role_grade.state_name) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code='" + ddl_unit_client.SelectedValue + "' AND  (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + "))) AND  unit_code is null ORDER BY STATE", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_clientwisestate.Items.Add(dr_item1[0].ToString());
                ddl_location.Text = (dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));

        newpanel.Visible = true;

        if (reason_panel.Visible == true)
        {
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btn_add.Visible = false;
        }
        else
        {
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btn_add.Visible = true;

        }
        string emp_listcount = "1";
        employee_count(emp_listcount);
    }

    protected void get_family_details(object sender, EventArgs e)
    {
        if (ddl_relation.SelectedItem.Text == "Father")
        {
            txt_name1.Text = txt_eefatharname.Text;
            txt_name4.Text = "";
        }
        if (ddl_relation.SelectedItem.Text == "Husband")
        {
            txt_name4.Text = txt_eefatharname.Text;
            txt_name1.Text = "";
        }
        newpanel.Visible = true;
        if (reason_panel.Visible == true)
        {
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btn_add.Visible = false;
        }
        else
        {
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btn_add.Visible = true;
        }
    }
    protected void reliveemp_notification()
    {
        d1.operation("delete from pay_notification_master where EMP_CODE='" + Session["Login_Id"].ToString() + "'");
        MySqlCommand cmd = new MySqlCommand("select Employee_type,emp_code,date_format(JOINING_DATE,'%d/%m/%Y'),REPORTING_TO,emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and Employee_type='Reliever' and REPORTING_TO='" + Session["Login_Id"].ToString() + "'  ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string emptype = dr.GetValue(0).ToString();
                string unitcempcodeode = dr.GetValue(1).ToString();
                string joiningdate = dr.GetValue(2).ToString();
                string REPORTING_TO = dr.GetValue(3).ToString();
                string emp_name = dr.GetValue(4).ToString();
                // DateTime joiningdate1 = Convert.ToDateTime(joiningdate);
                DateTime joiningdate1 = DateTime.ParseExact(joiningdate, "dd/MM/yyyy", null);
                int date1 = Convert.ToInt32(joiningdate.Substring(0, 2));
                DateTime currentdate = DateTime.Now;
                double newdat = (currentdate - joiningdate1).TotalDays;
                if (emptype == "Reliever")
                {
                    if (newdat > 27)
                    {
                        d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + REPORTING_TO + "','Message from  " + Session["USERNAME"].ToString() + " Please Left Employee Where Emp Name Is   " + emp_name + " ','EmployeeMaster.aspx')");
                    }
                }
            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }

    protected void clien_namelist()
    {
        ddl_clientname.Items.Clear();
        MySqlCommand cmd1 = new MySqlCommand("SELECT CLIENT_NAME from  pay_client_master  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
        d.con.Open();
        try
        {
            int i = 0;
            MySqlDataReader dr_item = cmd1.ExecuteReader();
            while (dr_item.Read())
            {
                ddl_clientname.Items.Insert(i++, new ListItem(dr_item[0].ToString()));
            }
            dr_item.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_clientname.Items.Insert(0, new ListItem("Select", "Select"));
        }


    }
    protected void btn_add_employee_Click(object sender, EventArgs e)
    {
        txt_search_adhar.Text = "";
        panel_adhar_card.Visible = true;
        pln_searchemp.Visible = false;
        pnl_buttons.Visible = false;
        btn_adhar_add_emp.Visible = false;
        Panel_app_gv.Visible = false;
        ddl_app_client.Visible = false;
        ddl_app_state.Visible = false;
        panel_approval.Visible = false;
        btn_left.Visible = false;
        ViewState["left"] = 0;
        //newpanel.Visible = true;
        //set_data();
        //btnnew_Click();
        //btnhelp_Click(sender, e);
        //if (ViewState["id"] == "1")
        //{
        //    btn_add.Visible = false;

        //    btnupdate.Visible = true;
        //    btndelete.Visible = true;
        //}
        //else
        //{
        //    btn_add.Visible = true;
        //  //  btn_reject.Visible = false;
        //    btnupdate.Visible = false;
        //    btndelete.Visible = false;

        //}


    }
    protected void add_new_emp(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        panel_adhar_card.Visible = false;
        newpanel.Visible = true;
        set_data();
        bank_detail_visibility(1);
        //btnnew_Click();
        btnhelp_Click(sender, e);
        ViewState["permanent"] = "0";

        txt_ptaxnumber.Text = txt_search_adhar.Text; ;
        if (ViewState["id"] == "1")
        {
            btn_add.Visible = false;
            Panel_app_gv.Visible = false;
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btnUpload.Visible = true;
            btncloselow.Visible = true;

            panel_approval.Visible = false;
        }
        else
        {
            btn_add.Visible = true;
            //  btn_reject.Visible = false;
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btn_releiving_letter.Visible = false;
            Panel5.Visible = true;
            Panel_app_gv.Visible = false;
            btnUpload.Visible = true;
            btncloselow.Visible = true;

            panel_approval.Visible = false;

            text_Clear_new_employee(); // vikas 22/04/2019
        }

        //ddl_employee_type.Enabled = true;
        //ddl_department.Enabled = true;
        //ddl_unit_client.Enabled = true;
        //ddl_clientwisestate.Enabled = true;
        //ddl_unitcode.Enabled = true;
        //ddl_grade.Enabled = true;
        //txt_eecode.Enabled = true;
        //txt_eename.Enabled = true;
    }
    protected void gv_expeness_PreRender(object sender, EventArgs e)
    {
        try
        {
            SearchGridView.UseAccessibleHeader = false;
            SearchGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_product_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_product_details.UseAccessibleHeader = false;
            gv_product_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_app_gridview_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_app_gridview.UseAccessibleHeader = false;
            gv_app_gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        ViewState["left"] = 0;
        if (txt_eecode.Text == "")
        {
            if (ddl_employee_type.SelectedValue == "Reliever")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Upload Bank PassBook Of Employee ')", true);
                newpanel.Visible = true;
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Upload Original photo and Aadhar card of Employee')", true);
                newpanel.Visible = true;
                return;
            }
        }
        string joining_date = d.getsinglestring("select DATE_FORMAT(unit_start_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION=(select GRADE_DESC from pay_grade_master where GRADE_CODE = '" + ddl_grade.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') and unit_code='" + ddl_unitcode.SelectedValue + "' limit 1 ");
        string end_date = d.getsinglestring("select DATE_FORMAT(unit_end_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION=(select GRADE_DESC from pay_grade_master where GRADE_CODE = '" + ddl_grade.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') and unit_code='" + ddl_unitcode.SelectedValue + "' limit 1 ");
        try
        {

            if (joining_date != "" && end_date != "")
            {
                if (joining_date != "00/00/0000" && end_date != "00/00/0000")
                {

                    if (DateTime.ParseExact(txt_joiningdate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(joining_date, "dd/MM/yyyy", null))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Joining Date Cannot be less than " + joining_date + ".')", true);
                        txt_joiningdate.Focus();
                        newpanel.Visible = true;
                        return;
                    }
                    if (DateTime.ParseExact(txt_joiningdate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(end_date, "dd/MM/yyyy", null))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Joining Date Cannot be Grater than " + end_date + ".')", true);
                        txt_joiningdate.Focus();
                        newpanel.Visible = true;
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please check joining date " + joining_date + " and end date  " + end_date + " in unit master .')", true);
                    txt_joiningdate.Focus();
                    newpanel.Visible = true;
                    return;
                }

            }

        }
        catch (Exception ex) { throw ex; }
        finally { }


        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
        if (chkcount())
        {
            if (ddl_employee_type.SelectedValue == "Reliever")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Reliver Employee Count Limit Is FulFill!!.');", true);
                newpanel.Visible = true;
                return;
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Count Full For This Designation.');", true);
                newpanel.Visible = true;
                return;
            }
        }

        int result = 0;
        //int rateresult = 0;
        set_data();
        try
        {
            int age = 0;
            DateTime current_date = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime birth = DateTime.ParseExact(txt_birthdate.Text.ToString(), "dd/MM/yyyy", null);
            age = current_date.Year - birth.Year;
            if (current_date < birth.AddYears(age))
            {
                //joinning date should be grater than birthdate
                age--;

            }
            if (age < 18 || age > 55)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please check Date of Birth !!! Employee should be 18 to 55 Years old');", true);
                txt_birthdate.Text = "";
                txt_birthdate.Focus();
                newpanel.Visible = true;
                return;
            }


            //if (txt_employeeaccountnumber.Text != "")
            //{
            //    d.con.Open();

            //    MySqlCommand cmd = new MySqlCommand("select EMP_BANK_STATEMENT from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            //    MySqlDataReader dr = cmd.ExecuteReader();

            //    if (dr.Read())
            //    {
            //    }
            //    //else
            //    //{
            //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Submit The Aadhar Card of Employee');", true);
            //    //    newpanel.Visible = true;
            //    //    return;
            //    //}
            //    dr.Close();
            //}
            // passpo photo mahendra

            if (ddl_employee_type.SelectedValue == "Permanent")
            {
                string s1 = "", s2 = "";
                d.con1.Open();
                MySqlCommand cmd4 = new MySqlCommand("select original_adhar_card,original_photo from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                try
                {
                    MySqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.Read())
                    {
                        s1 = dr4.GetValue(0).ToString();
                        s2 = dr4.GetValue(1).ToString();
                        if (ddl_clientwisestate.SelectedValue == "Arunachal Pradesh" || ddl_clientwisestate.SelectedValue == "Assam" || ddl_clientwisestate.SelectedValue == "Manipur" || ddl_clientwisestate.SelectedValue == "Meghalaya" || ddl_clientwisestate.SelectedValue == "Mizoram " || ddl_clientwisestate.SelectedValue == "Nagaland" || ddl_clientwisestate.SelectedValue == "Sikkim" || ddl_clientwisestate.SelectedValue == "Tripura")
                        {
                            if (s2 == "")
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo ');", true);
                                newpanel.Visible = true;
                                dr4.Close();
                                d.con1.Close();
                                return;
                            }
                        }
                        else if (s1 == "" || s2 == "")
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo and Adhar Card of Employee');", true);
                            newpanel.Visible = true;
                            dr4.Close();
                            d.con1.Close();
                            return;
                        }

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo and Adhar Card of Employee');", true);
                        newpanel.Visible = true;
                        dr4.Close();
                        d.con1.Close();
                        return;
                    }

                    dr4.Close();
                    d.con1.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con1.Close();
                }
            }
            DateTime joinindgdate = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
            TimeSpan count = newdate1 - joinindgdate;
            int Days = Convert.ToInt32(count.TotalDays);

            int differance = Convert.ToInt32(Days);


            //Adharcard Num
            if (ddl_employee_type.SelectedValue != "Select")
            {
                d.con1.Open();
                try
                {
                    //chaitali 20-06-2019
                    // string abcd=ViewState["rejoin_empcode"].ToString();
                    if (ViewState["rejoin_empcode"].ToString() == "0")
                    {
                        MySqlCommand cmd_1 = new MySqlCommand("SELECT P_TAX_NUMBER,EMP_NAME,EMP_CODE from  pay_employee_master WHERE  Employee_type!='Reliever' and P_TAX_NUMBER='" + txt_ptaxnumber.Text + "' and  P_TAX_NUMBER != ''  AND pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con1);
                        MySqlDataReader dr_1 = cmd_1.ExecuteReader();
                        if (dr_1.Read())
                        {
                            newpanel.Visible = true;
                            string adnar_card_no = dr_1.GetValue(0).ToString();
                            string emp_name = dr_1.GetValue(1).ToString();
                            string emp_id = dr_1.GetValue(2).ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This  Adhar Card number already exist for this employee : " + emp_name + ", try another Adhar number.');", true);
                            dr_1.Close();
                            d.con1.Close();
                            return;
                        }
                        newpanel.Visible = true;
                    }
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con1.Close();
                }
            }

            if (ddl_employee_type.SelectedValue != "Reliever")
            {
                d.con1.Open();
                try
                {

                    if (ViewState["rejoin_empcode"].ToString() == "0")
                    {
                        MySqlCommand cmd_1 = new MySqlCommand("SELECT BANK_EMP_AC_CODE,EMP_NAME,EMP_CODE FROM pay_employee_master WHERE BANK_EMP_AC_CODE!='' and BANK_EMP_AC_CODE='" + (txt_employeeaccountnumber.Text == "" ? txt_originalbankaccountno.Text.Trim() : txt_employeeaccountnumber.Text) + "' and comp_code='" + Session["comp_code"].ToString() + "' and Employee_type!='Reliever'", d.con1);
                        MySqlDataReader dr_1 = cmd_1.ExecuteReader();
                        if (dr_1.Read())
                        {
                            newpanel.Visible = true;
                            string emp_name = dr_1.GetValue(1).ToString();
                            string emp_id = dr_1.GetValue(2).ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Duplicate bank account number already exist for employee " + emp_id + "-" + emp_name + " , try another account number.');", true);
                            return;
                        }
                        dr_1.Close();
                        d.con1.Close();
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con1.Close();
                }
                if (check_document() == false)
                {
                    return;
                }



                if (chk_uniform() == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Uniform / Apron and Shoes Compulsory For Employee!!.');", true);
                    newpanel.Visible = true;
                    return;
                }
                if (chk_IDCard() == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter ID-Card Information Of Employee!!.');", true);
                    newpanel.Visible = true;
                    return;
                }

                newpanel.Visible = true;


                string newdate = Convert.ToString(System.DateTime.Now);
                string enteruserid = Session["USERID"].ToString();
                string entrydatestmp = Session["system_curr_date"].ToString();
                string emp_logname = Session["USERNAME"].ToString();


                if (txt_fine.Text == "")
                {
                    txt_fine.Text = "0";
                }
                //if ((d.getsinglestring("select emp_code from pay_images_master where emp_code = '" + txt_eecode.Text + "'")).Equals(""))
                //{
                //    btnnew_Click();
                //}
                //    result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES, EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child, Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,refaddress1,refaddress2,cca,gratuity,special_allow,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2, product_type ,  uniform_set ,  uniform_size,  uniform_issudate ,  uniform_expdate , shooes_set ,  shooes_size ,  shooes_issudate ,  shooes_expdate , 		swetor_set ,  swetor_size ,  swetor_issudate ,   swetor_expdate ,  idcard_set ,  idcard_size ,  idcard_issudate ,  idcard_expdate ,  raincoat_set ,  raincoat_size ,  raincoat_issudate ,  raincoat_expdate ,  tourch_set ,  tourch_size ,  tourch_issudate ,   tourch_expdate , whistel_set ,  whistel_size ,  whistel_issudate ,  whistel_expdate , baton_set ,  baton_size ,  baton_issudate ,  baton_expdate ,  belt_set ,  belt_size ,  belt_issudate ,  belt_expdate  ) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y')," + (txt_leftdate.Text == "" ? "NULL" : "str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y')") + ", '" + ddl_gender.Text + "', '" + ddl_unitcode.SelectedValue + "', '" + ddl_grade.SelectedValue + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + txt_maritalstaus.Text + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "',  '" + ddl_location.Text + "',  '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "','" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'),  '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.Text + "', '" + ddl_intime.SelectedValue + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "', '" + emp_name + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.Text + "','" + txt_bankholder.Text + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "','" + txt_ihmscode.Text + "','" + ddl_infitcode.SelectedValue + "','" + txt_emailid1.Text + "','" + txt_emailid2.Text + "','" + ddl_unit_client.SelectedValue + "',  '" + txt_address1.Text + "','" + txt_address2.Text + "','" + Txt_cca.Text + "','" + Txt_gra.Text + "','" + Txt_allow.Text + "','" + txt_fine.Text + "','" + ddl_employee_type.SelectedValue + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + txt_ranteagrement_satrtdate.Text + "','" + txt_ranteagrement_enddate.Text + "','" + ddl_clientwisestate.SelectedValue + "','" + pre_mobileno_1.Text + "','" + pre_mobileno_2.Text + "','" + txt_premanent_mob1.Text + "','" + txt_premanent_mob2.Text + "','" + select_designation.SelectedValue + "','" + ddl_uniformset.SelectedValue + "','" + uniform_size.Text + "',str_to_date('" + uniform_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + uniform_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_shooesset.SelectedValue + "','" + shoes_size.Text + "',str_to_date('" + shoes_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + shoes_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_swetorset.SelectedValue + "','" + swetor_size.Text + "',str_to_date('" + swetor_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + swetor_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_id_cardset.SelectedValue + "','" + id_card_size.Text + "',str_to_date('" + id_card_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + id_card_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_raincoatset.SelectedValue + "','" + raincoat_size.Text + "',str_to_date('" + raincoat_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + raincoat_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_tourchset.SelectedValue + "','" + tourch_size.Text + "',str_to_date('" + torch_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + Tourch_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_whistleset.SelectedValue + "','" + whistle_size.Text + "',str_to_date('" + whistle_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + whistle_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_batonset.SelectedValue + "','" + baton_size.Text + "',str_to_date('" + baton_issue_date.Text + "','%d/%m/%Y') ,str_to_date('" + baton_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_beltset.SelectedValue + "','" + belt_size.Text + "',str_to_date('" + belt_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + belt_expiry_date.Text + "','%d/%m/%Y'))");



                //chaitali
                string rejoin_date = ViewState["rejoin_date"].ToString();
                result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES, EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child, Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,refaddress1,refaddress2,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2,original_bank_account_no,department_type,REJOIN_DATE ) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y')," + (txt_leftdate.Text == "" ? "NULL" : "str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y')") + ", '" + ddl_gender.Text + "', '" + ddl_unitcode.SelectedValue + "', '" + ddl_grade.SelectedValue + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + ddl_MaritalStatus.SelectedValue + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "',  '" + ddl_location.Text + "',  '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "','" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text.Trim() + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'),  '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.SelectedValue + "', '" + ddl_intime.SelectedValue + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "', '" + emp_logname + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.Text + "','" + txt_bankholder.Text.Trim() + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "','" + txt_ihmscode.Text + "','" + ddl_infitcode.SelectedValue + "','" + txt_emailid1.Text + "','" + txt_emailid2.Text + "','" + ddl_unit_client.SelectedValue + "',  '" + txt_address1.Text + "','" + txt_address2.Text + "','" + txt_fine.Text + "','" + ddl_employee_type.SelectedValue + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + txt_ranteagrement_satrtdate.Text + "','" + txt_ranteagrement_enddate.Text + "','" + ddl_clientwisestate.SelectedValue + "','" + pre_mobileno_1.Text + "','" + pre_mobileno_2.Text + "','" + txt_premanent_mob1.Text + "','" + txt_premanent_mob2.Text + "','" + txt_originalbankaccountno.Text.Trim() + "','" + ddl_department.SelectedValue + "','" + rejoin_date + "')");
                if (ViewState["rejoin_update"].ToString() == "1")
                {   
                    rejoin_update();
                }
                foreach (GridViewRow row in gv_product_details.Rows)
                {
                    d.operation("INSERT INTO pay_document_details (comp_code,client_code,unit_code,emp_code,reporting_to,document_type,No_of_set,size,start_date,end_date,dispatch_flag,requested_by,requested_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "','" + ddl_reporting_to.SelectedValue + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[5].Text + "','%d/%m/%Y'),str_to_date('" + row.Cells[6].Text + "','%d/%m/%Y'),'0','" + Session["LOGIN_ID"].ToString() + "',now())");
                }

                MySqlCommand cmdmax = new MySqlCommand();
                d1.con.Open();
                if (ddl_employee_type.SelectedValue == "Permanent")
                {
                    try
                    {
                        if (Session["comp_code"].ToString() == "C01")
                        {
                            cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 8)+1) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                        }

                        else if (Session["comp_code"].ToString() == "C02")
                        {
                            cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 7)+1) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                        }

                        MySqlDataReader drmax = cmdmax.ExecuteReader();
                        if (drmax.Read())
                        {
                            int rownum = int.Parse(drmax.GetValue(0).ToString());
                            int rownum1 = int.Parse(drmax.GetValue(0).ToString());
                            if (Session["comp_code"].ToString() == "C02")
                            {
                                d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IHMS-','M0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IHMS-','M000','" + rownum + "') when " + rownum1 + "<999 then concat('IHMS-','M00','" + rownum + "') when " + rownum1 + "<9999 then concat('IHMS-','M0','" + rownum + "') end) where comp_code='C02' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                            }
                            else if (Session["comp_code"].ToString() == "C01")
                            {
                                d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IH&MS-','I0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IH&MS-','I000','" + rownum + "') when " + rownum1 + "<999 then concat('IH&MS-','I00','" + rownum + "') when " + rownum1 + "<9999 then concat('IH&MS-','I0','" + rownum + "') end) where comp_code='C01' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                            }
                        }

                        d1.con.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d1.con.Close(); }
                }
                if (result > 0)
                {
                    //add_values();
                    //send_email(Server.MapPath("~/User_Details.htm"), txt_email.Text);
                    employee_leave();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Added Successfully !!');", true);
                    text_Clear();
                    newpanel.Visible = false;
                    pln_searchemp.Visible = true;
                    pnl_buttons.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);

                }
            }
            else
            {
                string newdate = Convert.ToString(System.DateTime.Now);
                string enteruserid = Session["USERID"].ToString();
                string entrydatestmp = Session["system_curr_date"].ToString();
                string emp_name = Session["USERNAME"].ToString();


                //if (Txt_cca.Text == "")
                //{
                //    Txt_cca.Text = "0";
                //}
                //if (Txt_gra.Text == "")
                //{
                //    Txt_gra.Text = "0";
                //}

                //if (Txt_allow.Text == "")
                //{
                //    Txt_allow.Text = "0";
                //}

                if (txt_fine.Text == "")
                {
                    txt_fine.Text = "0";
                }
                //  btnnew_Click();

                //    result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES, EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child, Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,refaddress1,refaddress2,cca,gratuity,special_allow,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2, product_type ,  uniform_set ,  uniform_size,  uniform_issudate ,  uniform_expdate , shooes_set ,  shooes_size ,  shooes_issudate ,  shooes_expdate , 		swetor_set ,  swetor_size ,  swetor_issudate ,   swetor_expdate ,  idcard_set ,  idcard_size ,  idcard_issudate ,  idcard_expdate ,  raincoat_set ,  raincoat_size ,  raincoat_issudate ,  raincoat_expdate ,  tourch_set ,  tourch_size ,  tourch_issudate ,   tourch_expdate , whistel_set ,  whistel_size ,  whistel_issudate ,  whistel_expdate , baton_set ,  baton_size ,  baton_issudate ,  baton_expdate ,  belt_set ,  belt_size ,  belt_issudate ,  belt_expdate  ) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y')," + (txt_leftdate.Text == "" ? "NULL" : "str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y')") + ", '" + ddl_gender.Text + "', '" + ddl_unitcode.SelectedValue + "', '" + ddl_grade.SelectedValue + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + txt_maritalstaus.Text + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "',  '" + ddl_location.Text + "',  '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "','" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'),  '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.Text + "', '" + ddl_intime.SelectedValue + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "', '" + emp_name + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.Text + "','" + txt_bankholder.Text + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "','" + txt_ihmscode.Text + "','" + ddl_infitcode.SelectedValue + "','" + txt_emailid1.Text + "','" + txt_emailid2.Text + "','" + ddl_unit_client.SelectedValue + "',  '" + txt_address1.Text + "','" + txt_address2.Text + "','" + Txt_cca.Text + "','" + Txt_gra.Text + "','" + Txt_allow.Text + "','" + txt_fine.Text + "','" + ddl_employee_type.SelectedValue + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + txt_ranteagrement_satrtdate.Text + "','" + txt_ranteagrement_enddate.Text + "','" + ddl_clientwisestate.SelectedValue + "','" + pre_mobileno_1.Text + "','" + pre_mobileno_2.Text + "','" + txt_premanent_mob1.Text + "','" + txt_premanent_mob2.Text + "','" + select_designation.SelectedValue + "','" + ddl_uniformset.SelectedValue + "','" + uniform_size.Text + "',str_to_date('" + uniform_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + uniform_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_shooesset.SelectedValue + "','" + shoes_size.Text + "',str_to_date('" + shoes_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + shoes_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_swetorset.SelectedValue + "','" + swetor_size.Text + "',str_to_date('" + swetor_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + swetor_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_id_cardset.SelectedValue + "','" + id_card_size.Text + "',str_to_date('" + id_card_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + id_card_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_raincoatset.SelectedValue + "','" + raincoat_size.Text + "',str_to_date('" + raincoat_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + raincoat_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_tourchset.SelectedValue + "','" + tourch_size.Text + "',str_to_date('" + torch_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + Tourch_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_whistleset.SelectedValue + "','" + whistle_size.Text + "',str_to_date('" + whistle_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + whistle_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_batonset.SelectedValue + "','" + baton_size.Text + "',str_to_date('" + baton_issue_date.Text + "','%d/%m/%Y') ,str_to_date('" + baton_expiry_date.Text + "','%d/%m/%Y'),'" + ddl_beltset.SelectedValue + "','" + belt_size.Text + "',str_to_date('" + belt_issue_date.Text + "','%d/%m/%Y'),str_to_date('" + belt_expiry_date.Text + "','%d/%m/%Y'))");

                result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES, EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child, Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,refaddress1,refaddress2,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2,original_bank_account_no,department_type ) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y')," + (txt_leftdate.Text == "" ? "NULL" : "str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y')") + ", '" + ddl_gender.Text + "', '" + ddl_unitcode.SelectedValue + "', '" + ddl_grade.SelectedValue + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + ddl_MaritalStatus.SelectedValue + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "',  '" + ddl_location.Text + "',  '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "','" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text.Trim() + "', '" + txt_pfifsccode.Text.Trim() + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'),  '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.SelectedValue + "', '" + ddl_intime.SelectedValue + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "', '" + emp_name + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.Text + "','" + txt_bankholder.Text.Trim() + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "','" + txt_ihmscode.Text + "','" + ddl_infitcode.SelectedValue + "','" + txt_emailid1.Text + "','" + txt_emailid2.Text + "','" + ddl_unit_client.SelectedValue + "',  '" + txt_address1.Text + "','" + txt_address2.Text + "','" + txt_fine.Text + "','" + ddl_employee_type.SelectedValue + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + txt_ranteagrement_satrtdate.Text + "','" + txt_ranteagrement_enddate.Text + "','" + ddl_clientwisestate.SelectedValue + "','" + pre_mobileno_1.Text + "','" + pre_mobileno_2.Text + "','" + txt_premanent_mob1.Text + "','" + txt_premanent_mob2.Text + "','" + txt_originalbankaccountno.Text.Trim() + "','" + ddl_department.SelectedValue + "')");


                foreach (GridViewRow row in gv_product_details.Rows)
                {
                    d.operation("INSERT INTO pay_document_details (comp_code,client_code,unit_code,emp_code,reporting_to,document_type,admin_no_of_set,remaining_no_set,size,start_date,end_date,dispatch_flag,requested_by,requested_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "','" + ddl_reporting_to.SelectedValue + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[5].Text + "','%d/%m/%Y'),str_to_date('" + row.Cells[6].Text + "','%d/%m/%Y'),'0','" + Session["LOGIN_ID"].ToString() + "',now())");
                }

                if (ddl_employee_type.SelectedValue == "Permanent")
                {

                    MySqlCommand cmdmax = new MySqlCommand();
                    d1.con.Open();

                    try
                    {
                        string comp_code_dob = Session["comp_code"].ToString();

                        if (comp_code_dob == "C01")
                        {
                            cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 8, 6)) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                        }

                        else if (comp_code_dob == "C02")
                        {
                            cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 8, 6)) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                        }

                        MySqlDataReader drmax = cmdmax.ExecuteReader();
                        if (drmax.Read())
                        {
                            int rownum = int.Parse(drmax.GetValue(0).ToString());
                            int rownum1 = int.Parse(drmax.GetValue(0).ToString());
                            rownum = rownum + 1;
                            if (comp_code_dob == "C02")
                            {
                                d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IHMS-','M0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IHMS-','M000','" + rownum + "') when " + rownum1 + "<999 then concat('IHMS-','M00','" + rownum + "') when " + rownum1 + "<9999 then concat('IHMS-','M0','" + rownum + "') end) where comp_code='C02' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                            }
                            else if (comp_code_dob == "C01")
                            {
                                d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IH&MS-','I0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IH&MS-','I000','" + rownum + "') when " + rownum1 + "<999 then concat('IH&MS-','I00','" + rownum + "') when " + rownum1 + "<9999 then concat('IH&MS-','I0','" + rownum + "') end) where comp_code='C01' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                            }
                        }
                        drmax.Close();
                        d1.con.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d1.con.Close(); }
                }
                if (result > 0)
                {
                    //add_values();
                    //send_email(Server.MapPath("~/User_Details.htm"), txt_email.Text);
                    employee_leave();
                    if (ViewState["rejoin_empcode"] != "0")
                    {

                        d.operation("update pay_employee_master set rejoin_flag=1 where emp_code='" + ViewState["rejoin_empcode"].ToString() + "' ");
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Added Successfully !!');", true);
                    text_Clear();
                    newpanel.Visible = false;
                    pln_searchemp.Visible = true;//add vikas 22/04/2019
                    pnl_buttons.Visible = true;// add vikas 22/04/2019
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);

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
            btn_add.Visible = false;
            // btnnew_Click();
            btnhelp_Click(sender, e);
        }
    }


    public void employee_leave()
    {
        //int result = 0;
        //result = d.operation("INSERT INTO pay_leave_emp_balance(comp_code,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) select '" + Session["comp_code"].ToString() + "', '" + ddl_unitcode.SelectedValue.ToString().Substring(0, 4) + "','" + txt_eecode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "',now(),leave_name,abbreviation,gender,max_no_of_leave,max_no_of_leave from pay_leave_master where comp_code='" + Session["comp_code"].ToString() + "' and gender in ('" + ddl_gender.SelectedValue + "','B') ");
        d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,ROLE,flag,create_user, create_date, password_changed_date,first_login,comp_code) VALUES('" + txt_eecode.Text + "','" + txt_eename.Text + "','" + GetSha256FromString(txt_birthdate.Text) + "','" + DropDownList1.SelectedItem.Text + "','A','" + Session["USERID"].ToString() + "',now(),now(),'0','" + Session["comp_code"].ToString() + "')");
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnhelp_Click(object sender, EventArgs e)
    {
        ViewState["left"] = 0;
        SearchGridView.Visible = true;
        btn_add.Visible = true;

        try
        {
            DataSet ds = new DataSet();
            //MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode,pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME,pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code = '"+Session["COMP_CODE"].ToString()+"' AND pay_client_master.client_code IN (SELECT DISTINCT(pay_client_state_role_grade.client_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '"+Session["COMP_CODE"].ToString()+"' AND (REPORTING_TO IN ("+Session["REPORTING_EMP_SERIES"].ToString()+") OR pay_client_state_role_grade.emp_code ='"+Session["LOGIN_ID"].ToString()+"')) AND pay_unit_master.unit_code IN (SELECT DISTINCT(pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '"+Session["COMP_CODE"].ToString()+"' AND (REPORTING_TO IN ("+Session["REPORTING_EMP_SERIES"].ToString()+") OR pay_client_state_role_grade.emp_code = '"+Session["LOGIN_ID"].ToString()+"')) AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL)", d.con1);
            //vikas 08-01-19
            d.con1.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT distinct pay_employee_master.EMP_CODE, pay_employee_master.ihmscode,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',(select GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code AND pay_grade_master.GRADE_CODE = pay_employee_master.GRADE_CODE limit 1) as 'GRADE_DESC', pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME, pay_employee_master.employee_type, pay_employee_master.EMP_MOBILE_NO, DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',client_wise_state FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code INNER JOIN pay_client_state_role_grade ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.unit_code = pay_client_state_role_grade.unit_code where ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL) and pay_client_master.client_active_close = '0'", d.con1);
            adp.Fill(ds);

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //   // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this company!!');", true);
            //    Panel5.Visible = false;
            //    btndelete.Visible = false;
            //    btnupdate.Visible = false;
            //}
            //else
            //{
            //    Panel5.Visible = true;
            //    btndelete.Visible = true;
            //btnupdate.Visible = true;
            //}

            Panel5.Visible = true;
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            d.con1.Close();
            // text_Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void btn_history_click(object sender, EventArgs e)
    {
        btn_Leftemp_Export.Visible = true;
        SearchGridView.Visible = true;
        btn_add.Visible = false;
        d.con1.Open();
        try
        {
            DataSet ds = new DataSet();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataAdapter adp = new MySqlDataAdapter("select `pay_employee_master`.`EMP_CODE`, `pay_employee_master`.`ihmscode`, CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END AS 'EMP_NAME', pay_grade_master.GRADE_DESC AS 'GRADE_DESC' ,pay_client_master.client_name AS 'client_name' ,pay_unit_master.unit_name AS 'unit_name', `pay_employee_master`.`Employee_type`, `pay_employee_master`.`EMP_MOBILE_NO`, DATE_FORMAT(`pay_employee_master`.`LEFT_DATE`, '%d/%m/%Y') AS 'BIRTH_DATE', DATE_FORMAT(`pay_employee_master`.`JOINING_DATE`, '%d/%m/%Y') AS 'JOINING_DATE', pay_employee_master.`client_wise_state` FROM `pay_employee_master` INNER JOIN `pay_client_master` ON `pay_employee_master`.`client_code` = `pay_client_master`.`client_code` and `pay_employee_master`.`comp_code` = `pay_client_master`.`comp_code` inner join pay_unit_master on  `pay_employee_master`.`client_code` = `pay_unit_master`.`client_code` and `pay_employee_master`.`comp_code` = `pay_unit_master`.`comp_code` and `pay_employee_master`.`unit_code` = `pay_unit_master`.`unit_code` inner join pay_grade_master on `pay_grade_master`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_grade_master`.`GRADE_CODE` = `pay_employee_master`.`GRADE_CODE` WHERE `pay_employee_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND (`pay_employee_master`.`LEFT_REASON` != '' AND (`pay_employee_master`.`LEFT_REASON` != '' || `pay_employee_master`.`LEFT_REASON` IS NOT NULL))  and pay_client_master.client_active_close='0' ORDER BY `pay_employee_master`.`LEFT_DATE`", d.con1);
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this company!!');", true);
                Panel5.Visible = false;
                btndelete.Visible = false;
                btnupdate.Visible = false;
            }
            else
            {
                Panel5.Visible = true;
                btndelete.Visible = true;
                btnupdate.Visible = true;
            }
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            SearchGridView.Columns[9].HeaderText = "LEFT DATE";

            ViewState["left"] = 1;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

    }


    public int getEmployee(string EmployeeCode)
    {
        gv_product_details.DataSource = null;
        gv_product_details.DataBind();
        int returnEmployee = 0;
        d.con1.Open();
        try
        {
            string l_EMP_CODE = EmployeeCode.ToString();

            // MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),LEFT_DATE,GENDER,UNIT_CODE,DESIGNATION_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,ADHARNO,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,AUTOATTENDANCE_CODE,PF_SHEET,EARN_TOTAL,FATHER_RELATION,DATE_STMP,ENTER_USER_ID,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,KYC_CONFIRM,REPORTING_TO,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,emistartdate,attendance_id,in_time,pfemployeepercentage FROM pay_employee_master WHERE EMP_CODE='" + l_EMP_CODE + "'", d.con1);

            MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),date_format(LEFT_DATE,'%d/%m/%Y'),GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,date_format(PF_NOMINEE_BDATE,'%d/%m/%Y'),EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7, ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,(select client_code from pay_unit_master where unit_code = pay_employee_master.unit_code and comp_code = '" + Session["COMP_CODE"].ToString() + "') as client_code,refaddress1,refaddress2,cca,gratuity,special_allow,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date ,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2,original_bank_account_no,comments,department_type,Id_as_per_dob FROM pay_employee_master WHERE EMP_CODE='" + l_EMP_CODE + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                returnEmployee = 1;
                txt_eecode.Text = dr.GetValue(1).ToString();
                txt_eename.Text = dr.GetValue(2).ToString();
                txt_eefatharname.Text = dr.GetValue(3).ToString();
                string bdate = dr.GetValue(4).ToString();
                if (bdate == "")
                {
                    txt_birthdate.Text = dr.GetValue(4).ToString();
                }
                else
                {
                    txt_birthdate.Text = bdate.ToString();
                }

                string joindate = dr.GetValue(5).ToString();
                if (joindate == "")
                {
                    txt_joiningdate.Text = dr.GetValue(5).ToString();
                }
                else
                {
                    // txt_joiningdate.Text = DateTime.Parse(joindate).ToString("dd/MM/yy");
                    txt_joiningdate.Text = joindate.ToString();
                }

                if (!ViewState["rejoin_date"].ToString().Equals(""))
                {
                    txt_joiningdate.Text = ViewState["rejoin_date"].ToString();
                }

                string confrmdate = dr.GetValue(6).ToString();
                if (confrmdate == "")
                {
                    txt_confirmationdate.Text = dr.GetValue(6).ToString();
                }
                else
                {
                    txt_confirmationdate.Text = confrmdate.ToString();
                }

                string lftdate = dr.GetValue(7).ToString();
                if (lftdate == "")
                {
                    txt_leftdate.Text = dr.GetValue(7).ToString();
                }
                else
                {
                    txt_leftdate.Text = lftdate.ToString();
                }
                if (!txt_leftdate.Text.Equals(""))
                {
                    btn_releiving_letter.Visible = true;
                }
                else { btn_releiving_letter.Visible = false; }
                ddl_gender.SelectedValue = dr.GetValue(8).ToString();
                ddl_bankcode.Text = dr.GetValue(11).ToString();
                txt_employeeaccountnumber.Text = dr.GetValue(12).ToString();
                txt_presentaddress.Text = dr.GetValue(13).ToString();
                ddl_state.SelectedValue = dr.GetValue(15).ToString();
                get_city_list(null, null);
                txt_presentcity.SelectedValue = dr.GetValue(14).ToString();
               
                txt_presentpincode.Text = dr.GetValue(16).ToString();
                txt_permanantaddress.Text = dr.GetValue(17).ToString();

                ddl_permstate.SelectedValue = dr.GetValue(19).ToString();
                get_city_list_shipping(null, null);
                txt_permanantcity.SelectedItem.Text = dr.GetValue(18).ToString();

                txt_permanantpincode.Text = dr.GetValue(20).ToString();
                txt_mobilenumber.Text = dr.GetValue(21).ToString();
                txt_residencecontactnumber.Text = dr.GetValue(22).ToString();
                txt_email.Text = dr.GetValue(23).ToString();
                if (dr.GetValue(24).ToString() != "")
                {
                    ddl_MaritalStatus.SelectedValue = dr.GetValue(24).ToString();
                }
                ddl_bloodgroup.Text = dr.GetValue(25).ToString();
                txt_hobbies.Text = dr.GetValue(26).ToString();

                txt_weight.Text = dr.GetValue(27).ToString();
                if (dr.GetValue(28).ToString() != "")
                {
                    ddl_religion.SelectedValue = dr.GetValue(28).ToString();
                }
                else { ddl_religion.SelectedValue = "Select"; }

                txt_height.Text = dr.GetValue(29).ToString();

                /////-------------------------------------------------------------

                ddl_ptaxdeductionflag.SelectedItem.Text = dr.GetValue(31).ToString();
                txt_ptaxnumber.Text = dr.GetValue(32).ToString();//adhaar no;
                // ddl_dept.SelectedValue= dr.GetValue(41).ToString();

                /////----------------------------------------------------------

                txtreasonforleft.Text = dr.GetValue(33).ToString();
                ddlpfregisteremp.SelectedValue = dr.GetValue(34).ToString();

                if (dr.GetValue(35).ToString() != "")
                {
                    ddl_relation.SelectedValue = dr.GetValue(35).ToString();
                }
                else { ddl_relation.SelectedIndex = 0; }
                // txt_eecode.Text = dr.GetValue(76).ToString();

                txt_pfbankname.Text = dr.GetValue(36).ToString();
                txt_pfifsccode.Text = dr.GetValue(37).ToString();
                txt_pfnomineename.Text = dr.GetValue(38).ToString();
                txt_pfnomineerelation.Text = dr.GetValue(39).ToString();

                string pfbdate = dr.GetValue(40).ToString();
                if (pfbdate == "")
                {
                    txt_pfbdate.Text = dr.GetValue(40).ToString();
                }
                else
                {

                    txt_pfbdate.Text = pfbdate.ToString();
                }

                //txt_pan_new_num.Text = dr.GetValue(50).ToString();
                txt_advance_payment.Text = dr.GetValue(41).ToString();

                txtrefname1.Text = dr.GetValue(42).ToString();
                txtref1mob.Text = dr.GetValue(43).ToString();
                txtrefname2.Text = dr.GetValue(44).ToString();
                txtref2mob.Text = dr.GetValue(45).ToString();

                txt_Nationality.Text = dr.GetValue(46).ToString();
                txt_Identitymark.Text = dr.GetValue(47).ToString();
                ddl_Mother_Tongue.Text = dr.GetValue(48).ToString();
                txt_Passport_No.Text = dr.GetValue(49).ToString();
                ddl_Visa_Country.SelectedItem.Text = dr.GetValue(50).ToString();
                txt_Driving_License_No.Text = dr.GetValue(51).ToString();
                txt_Mise.Text = dr.GetValue(52).ToString();
                txt_Place_Of_Birth.Text = dr.GetValue(53).ToString();
                txt_Language_Known.Text = dr.GetValue(54).ToString();
                txt_Area_Of_Expertise.Text = dr.GetValue(55).ToString();
                txt_Passport_Validity_Date.Text = dr.GetValue(56).ToString();
                txt_Visa_Validity_Date.Text = dr.GetValue(57).ToString();
                txt_Details_Of_Handicap.Text = dr.GetValue(58).ToString();


                txt_qualification_1.Text = dr.GetValue(59).ToString();
                txt_year_of_passing_1.Text = dr.GetValue(60).ToString();
                txt_qualification_2.Text = dr.GetValue(61).ToString();
                txt_year_of_passing_2.Text = dr.GetValue(62).ToString();
                txt_qualification_3.Text = dr.GetValue(63).ToString();
                txt_year_of_passing_3.Text = dr.GetValue(64).ToString();
                txt_qualification_4.Text = dr.GetValue(65).ToString();
                txt_year_of_passing_4.Text = dr.GetValue(66).ToString();
                txt_qualification_5.Text = dr.GetValue(67).ToString();
                txt_year_of_passing_5.Text = dr.GetValue(68).ToString();
                txt_key_skill_1.Text = dr.GetValue(69).ToString();
                txt_experience_in_months_1.Text = dr.GetValue(70).ToString();
                txt_key_skill_2.Text = dr.GetValue(71).ToString();
                txt_experience_in_months_2.Text = dr.GetValue(72).ToString();
                txt_key_skill_3.Text = dr.GetValue(73).ToString();
                txt_experience_in_months_3.Text = dr.GetValue(74).ToString();
                txt_key_skill_4.Text = dr.GetValue(75).ToString();
                txt_experience_in_months_4.Text = dr.GetValue(76).ToString();
                txt_key_skill_5.Text = dr.GetValue(77).ToString();
                txt_experience_in_months_5.Text = dr.GetValue(78).ToString();

                //ddl_grade_SelectedIndexChanged(null, null);

                //  else
                //{
                //    ddl_reporting_to.SelectedValue = dr.GetValue(122).ToString();
                //}
                // txt_loandate.Text = dr.GetValue(80).ToString();

                ddl_intime.SelectedValue = dr.GetValue(82).ToString();

                //---Family details -----
                txt_name1.Text = dr.GetValue(83).ToString();
                txt_relation1.Text = dr.GetValue(84).ToString();
                txt_dob1.Text = dr.GetValue(85).ToString();
                txt_pan1.Text = dr.GetValue(86).ToString();
                txt_adhaar1.Text = dr.GetValue(87).ToString();
                txt_name2.Text = dr.GetValue(88).ToString();
                txt_relation2.Text = dr.GetValue(89).ToString();
                txt_dob2.Text = dr.GetValue(90).ToString();
                txt_pan2.Text = dr.GetValue(91).ToString();
                txt_adhaar2.Text = dr.GetValue(92).ToString();
                txt_name3.Text = dr.GetValue(93).ToString();
                txt_relation3.Text = dr.GetValue(94).ToString();
                txt_dob3.Text = dr.GetValue(95).ToString();
                txt_pan3.Text = dr.GetValue(96).ToString();
                txt_adhaar3.Text = dr.GetValue(97).ToString();
                txt_name4.Text = dr.GetValue(98).ToString();
                txt_relation4.Text = dr.GetValue(99).ToString();
                txt_dob4.Text = dr.GetValue(100).ToString();
                txt_pan4.Text = dr.GetValue(101).ToString();
                txt_adhaar4.Text = dr.GetValue(102).ToString();
                txt_name5.Text = dr.GetValue(103).ToString();
                txt_relation5.Text = dr.GetValue(104).ToString();
                txt_dob5.Text = dr.GetValue(105).ToString();
                txt_pan5.Text = dr.GetValue(106).ToString();
                txt_adhaar5.Text = dr.GetValue(107).ToString();
                txt_name6.Text = dr.GetValue(108).ToString();
                txt_relation6.Text = dr.GetValue(109).ToString();
                txt_dob6.Text = dr.GetValue(110).ToString();
                txt_pan6.Text = dr.GetValue(111).ToString();
                txt_adhaar6.Text = dr.GetValue(112).ToString();
                txt_name7.Text = dr.GetValue(113).ToString();
                txt_relation7.Text = dr.GetValue(114).ToString();
                txt_dob7.Text = dr.GetValue(115).ToString();
                txt_pan7.Text = dr.GetValue(116).ToString();
                txt_adhaar7.Text = dr.GetValue(117).ToString();
                Numberchild.Text = dr.GetValue(119).ToString();



                txt_kra.Text = dr.GetValue(119).ToString();

                //ddl_location.Text = dr.GetValue(30).ToString();
                // ddl_location_SelectedIndexChanged(null, null);

                ddl_location_city.Text = dr.GetValue(120).ToString();


                txt_bankholder.Text = dr.GetValue(121).ToString();
                txt_policestationname.Text = dr.GetValue(122).ToString();
                txt_fmobile1.Text = dr.GetValue(123).ToString();
                txt_fmobile2.Text = dr.GetValue(124).ToString();
                txt_fmobile3.Text = dr.GetValue(125).ToString();
                txt_fmobile4.Text = dr.GetValue(126).ToString();
                txt_fmobile5.Text = dr.GetValue(127).ToString();
                txt_fmobile6.Text = dr.GetValue(128).ToString();
                txt_fmobile7.Text = dr.GetValue(129).ToString();


                txt_ihmscode.Text = dr.GetValue(130).ToString();
                ddl_infitcode.SelectedValue = dr.GetValue(131).ToString();
                txt_emailid1.Text = dr.GetValue(132).ToString();
                txt_emailid2.Text = dr.GetValue(133).ToString();

                // ddl_clientname.SelectedValue = dr.GetValue(183).ToString();
                // ddl_clientname_SelectedIndexChanged(null, null);
                // if (ddl_unitcode.Items.Count > 0)
                // {
                ddl_unit_client.SelectedValue = dr.GetValue(134).ToString();
                ddl_clientname_SelectedIndexChanged(null, null);
                try
                {
                    ddl_clientwisestate.SelectedValue = dr.GetValue(147).ToString();
                    get_city_list1(null, null);
                    ddl_unitcode.SelectedValue = dr.GetValue(9).ToString();
                    designation_unitwise(null, null);
                    ddl_grade.SelectedValue = dr.GetValue(10).ToString();
                    ddl_grade_SelectedIndexChanged(null, null);
                    txt_attendanceid.SelectedValue = dr.GetValue(81).ToString();
                    if (ddl_reporting_to.Items.Count > 0)
                    {
                        ddl_reporting_to.SelectedValue = dr.GetValue(79).ToString();
                    }
                }
                catch { }



                txt_address1.Text = dr.GetValue(136).ToString();
                txt_address2.Text = dr.GetValue(137).ToString();

                //Txt_cca.Text = dr.GetValue(138).ToString();
                //Txt_gra.Text = dr.GetValue(139).ToString();
                //Txt_allow.Text = dr.GetValue(140).ToString();
                txt_fine.Text = dr.GetValue(141).ToString();
                ddl_employee_type.SelectedValue = dr.GetValue(142).ToString();
                txt_start_date.Text = dr.GetValue(143).ToString();
                txt_end_date.Text = dr.GetValue(144).ToString();
                txt_ranteagrement_satrtdate.Text = dr.GetValue(145).ToString();
                txt_ranteagrement_enddate.Text = dr.GetValue(146).ToString();
                try
                {

                    ddl_location.Text = ddl_clientwisestate.SelectedValue;
                }
                catch { }
                pre_mobileno_1.Text = dr.GetValue(148).ToString();
                pre_mobileno_2.Text = dr.GetValue(149).ToString();
                txt_premanent_mob1.Text = dr.GetValue(150).ToString();
                txt_premanent_mob2.Text = dr.GetValue(151).ToString();
                txt_originalbankaccountno.Text = dr.GetValue(152).ToString();
                string tet = reason_updt(dr.GetValue(153).ToString(), 1);

                if (dr.GetValue(154).ToString() == "")
                {
                    ddl_department.SelectedValue = "Select";
                }
                else
                {
                    ddl_department.SelectedValue = dr.GetValue(154).ToString();
                }
                txt_id_as_per_dob.Text = dr.GetValue(155).ToString();
                MySqlCommand cmd_hd = new MySqlCommand("SELECT document_type,admin_no_of_set,size,date_format(start_date,'%d/%m/%Y') as start_date ,date_format(end_date,'%d/%m/%Y') as end_date,remaining_no_set FROM pay_document_details WHERE emp_code ='" + txt_eecode.Text + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
                d.con.Open();
                try
                {
                    MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
                    if (dr_hd.HasRows)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Load(dr_hd);
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["headtable"] = dt;
                        }
                        gv_product_details.DataSource = dt;
                        gv_product_details.DataBind();
                        dr_hd.Close();
                    }
                    d.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }




            }
            dr.Close();
            cmd.Dispose();

            //Role of user
            cmd = new MySqlCommand("select role from pay_user_master where login_id ='" + l_EMP_CODE + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                try
                {
                    DropDownList1.SelectedValue = dr.GetValue(0).ToString();
                }
                catch { }
            }

            dr.Close();
            cmd.Dispose();

            load_images(l_EMP_CODE);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            ddl_grade_SelectedIndexChanged(null, null);
            btn_add.Visible = false;
            btnupdate.Visible = true;
            btndelete.Visible = true;
            reason_panel.Visible = true;
            btncloselow.Visible = true;
        }

        return returnEmployee;
    }
    protected void SearchGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_dublicate_id_card.DataSource = null;
        gv_dublicate_id_card.DataBind();
       
        try
        {
            Panel_dispatch1.Visible = false;
            ddl_emp_type.Visible = false;
            btn_dispatch_details.Visible = false;
            string name = SearchGridView.SelectedRow.Cells[5].Text.ToString();
            if (name == "Permanent")
            {
                btn_approve.Visible = true;
            }
            else
            {
                btn_approve.Visible = false;
            }
            images_visibility();
            getEmployee(SearchGridView.SelectedRow.Cells[0].Text);
        }
        catch (Exception GetEmpError)
        {
            throw GetEmpError;
        }
        finally
        {
            string legal_flag = "";
            if (d.getsinglestring("select employee_type from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + txt_eecode.Text + "'").Contains("Permanent"))
            {
                legal_flag = d.getsinglestring("select legal_flag from pay_employee_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'");
                if (legal_flag.Equals("0") || legal_flag.Equals("3"))
                {
                    visiblity_true();
                    btnupdate.Visible = true;
                    btndelete.Visible = true;
                    btn_approve.Visible = true;
                    btnUpload.Visible = true;
                    ViewState["permanent"] = "1";
                    bank_detail_visibility(1);
                    btncloselow.Visible = true;

                }
                else
                {
                    visiblity();
                    btnupdate.Visible = false;
                    btndelete.Visible = false;
                    btn_approve.Visible = false;
                    btncloselow.Visible = false;
                    btnUpload.Visible = false;
                    bank_detail_visibility(0);
                }
                if (legal_flag.Equals("2"))
                {
                    btn_left.Visible = true;
                }
                else { btn_left.Visible = false; }
                
            }
            else
            {
                visiblity_true();
                btnupdate.Visible = true;
                btndelete.Visible = true;
                Button1.Visible = true;
                //  Button1.Visible = true;
                ViewState["id"] = "1";
                btnUpload.Visible = true;
                ViewState["permanent"] = "0";
            }
            ViewState["id"] = "1";


        }
        newpanel.Visible = true;
        ////ddl_employee_type.Enabled = false;
        //ddl_department.Enabled = false;
        //ddl_unit_client.Enabled = false;
        //ddl_clientwisestate.Enabled = false;
        //ddl_unitcode.Enabled = false;
        //ddl_grade.Enabled = false;
        //txt_eecode.Enabled = false;
        //txt_eename.Enabled = false;

        //vikas add 03/04/2019
        load_dublicate_id_grdview();
        select_designation.SelectedValue = "0";
        ddl_product_type.SelectedValue = "0";
        ddl_uniformset.SelectedValue = "0";
        uniform_size.SelectedValue = "Select";
        // lbl_qty.Text = "";
        txt_quantity1.Visible = false;
        uniform_issue_date.Text = "";
        uniform_expiry_date.Text = "";
    }

    protected void Id_As_Per_DOB()
    {
        MySqlCommand cmdmax = new MySqlCommand();
        string initcode = "";
        if (Session["comp_code"] == "C01")
        {
            cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(Id_as_per_dob, 7, 5) AS UNSIGNED))+1 FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            initcode = "IH&MS-I";
        }

        else if (Session["comp_code"] == "C02")
        {
            cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(Id_as_per_dob, 6, 5) AS UNSIGNED))+1 FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            initcode = "IHMS-M";
        }

        MySqlDataReader drmax = cmdmax.ExecuteReader();
        if (drmax.Read())
        {
            int max_empcode = int.Parse(drmax.GetValue(0).ToString());


            if (max_empcode >= 100 && max_empcode < 1000)
            {
                txt_id_as_per_dob.Text = initcode.ToString() + "00" + max_empcode;
            }
            else if (max_empcode >= 1000 && max_empcode < 10000)
            {
                txt_id_as_per_dob.Text = initcode.ToString() + "0" + max_empcode;
            }
            else if (max_empcode == 10000)
            {
                txt_id_as_per_dob.Text = initcode.ToString() + "" + max_empcode;
            }
        }
    }

    protected void btnnew_Click()
    {
        int max_empcode = 0;
        d1.con.Open();
        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  pay_images_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {

            }
            else if (drmax.Read())
            {
                d1.con1.Open();
                try
                {
                    MySqlCommand cmdinit = new MySqlCommand("SELECT EMP_SERIES_INIT FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
                    MySqlDataReader drinit = cmdinit.ExecuteReader();
                    if (!drinit.HasRows)
                    {
                    }
                    else if (drinit.Read())
                    {
                        string stinit = drinit.GetValue(0).ToString();
                        string initcode = "";
                        if (stinit != "")
                        {
                            initcode = stinit.ToString();
                            string str = drmax.GetValue(0).ToString();
                            if (str == "")
                            {
                                txt_eecode.Text = initcode.ToString() + "00001";
                            }
                            else
                            {
                                max_empcode = int.Parse(drmax.GetValue(0).ToString());

                                //if (Session["comp_code"].ToString().Equals("C01"))
                                //{
                                //    if (!Application["comp1emp_code"].ToString().Equals(""))
                                //    {

                                //        if (int.Parse(Application["comp1emp_code"].ToString().Substring(2)) >= max_empcode)
                                //        {
                                //            max_empcode = int.Parse(Application["comp1emp_code"].ToString().Substring(2)) + 1;

                                //        }

                                //    }
                                //}
                                //if (Session["comp_code"].ToString().Equals("C02"))
                                //{
                                //    if (!Application["comp2emp_code"].ToString().Equals(""))
                                //    {

                                //        if (int.Parse(Application["comp2emp_code"].ToString().Substring(2)) >= max_empcode)
                                //        {
                                //            max_empcode = int.Parse(Application["comp2emp_code"].ToString().Substring(2)) + 1;

                                //        }

                                //    }
                                //}
                                if (max_empcode < 10)
                                {
                                    txt_eecode.Text = initcode.ToString() + "0000" + max_empcode;
                                }
                                else if (max_empcode >= 10 && max_empcode < 100)
                                {
                                    txt_eecode.Text = initcode.ToString() + "000" + max_empcode;
                                }
                                else if (max_empcode >= 100 && max_empcode < 1000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "00" + max_empcode;
                                }
                                else if (max_empcode >= 1000 && max_empcode < 10000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "0" + max_empcode;
                                }
                                else if (max_empcode == 10000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "" + max_empcode;
                                }

                                //if (Session["comp_code"].ToString().Equals("C01"))
                                //{
                                //    Application["comp1emp_code"] = txt_eecode.Text;
                                //}
                                //if (Session["comp_code"].ToString().Equals("C02"))
                                //{
                                //    Application["comp2emp_code"] = txt_eecode.Text;
                                //}

                            }
                        }
                    }
                    drinit.Close();
                    d1.con1.Close();
                    d1.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con.Close();
                    d1.con1.Close();
                }
            }
            drmax.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

        }
        btn_add.Visible = true;
        btn_add.Enabled = true;
        btnupdate.Visible = false;
        btndelete.Visible = false;
        SearchGridView.Visible = false;
    }

    public void text_Clear()
    {
        txt_id_as_per_dob.Text = "";
        txt_reason_updation.Text = "";
        ddl_department.SelectedValue = "Select";
        txt_originalbankaccountno.Text = "";
        ddl_clientwisestate.Items.Clear();
        txt_eename.Text = "";
        txt_eefatharname.Text = "";

        txtreasonforleft.Text = "";
        // txtautoattendancecode.Text = "";

        ddl_relation.SelectedIndex = 0;
        txt_birthdate.Text = "";
        ddl_gender.SelectedIndex = 0;
        txt_ihmscode.Text = "";

        gv_product_details.DataSource = null;
        gv_product_details.DataBind();
        gv_search_adharcardno.DataSource = null;
        gv_search_adharcardno.DataBind();

        //   ddl_unitcode.SelectedIndex = 0;
        //ddl_dept.SelectedIndex = 0;

        ddl_grade_SelectedIndexChanged(null, null);
        ddl_grade.SelectedIndex = 0;
        txt_presentaddress.Text = "";
        txt_presentcity.SelectedIndex = 0;
        ddl_state.SelectedIndex = 0;
        txt_presentpincode.Text = "";
        txtrefname1.Text = "";
        txtrefname2.Text = "";

        txt_permanantaddress.Text = "";
        //    txt_permanantcity.Items.Clear();
        txt_permanantcity.SelectedIndex = 0;
        ddl_permstate.SelectedIndex = 0;
        txt_permanantpincode.Text = "";
        txtref1mob.Text = "";
        txtref2mob.Text = "";
        txt_address2.Text = "";
        txt_address1.Text = "";
        txt_mobilenumber.Text = "";
        txt_residencecontactnumber.Text = "";
        txt_confirmationdate.Text = "";
        txt_joiningdate.Text = "";
        txt_advance_payment.Text = "";

        ddl_location.Text = "";
        ddl_location_city.Text = "";

        ddl_ptaxdeductionflag.SelectedIndex = 1;
        txt_ptaxnumber.Text = "";
        txt_leftdate.Text = "0";
        txtreasonforleft.Text = "";
        ddlpfregisteremp.SelectedIndex = 1;
        txt_pfbankname.Text = "";
        txt_pfnomineename.Text = "";
        txt_pfifsccode.Text = "";
        txt_pfnomineerelation.Text = "";
        txt_employeeaccountnumber.Text = "";
        txt_pfbdate.Text = "";
        //ddl_bankcode.SelectedIndex =;
        ddl_MaritalStatus.SelectedIndex = 0;
        ddl_bloodgroup.SelectedIndex = 0;
        txt_hobbies.Text = "";

        txt_weight.Text = "0";
        ddl_religion.SelectedValue = "Select";

        txt_height.Text = "0";
        if (ddl_reporting_to.SelectedIndex == -1)
        {
            //ddl_reporting_to.SelectedIndex = 0;
        }
        else
        {
            ddl_reporting_to.SelectedIndex = 0;
        }

        txtsearchempid.Text = "";
        txt_email.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
        txt_ranteagrement_satrtdate.Text = "";
        txt_ranteagrement_enddate.Text = "";
        txt_Nationality.Text = "INDIAN";
        txt_Identitymark.Text = "";
        ddl_Mother_Tongue.Text = "";
        txt_Passport_No.Text = "";
        ddl_Visa_Country.SelectedIndex = 0;
        txt_Driving_License_No.Text = "";
        txt_Mise.Text = "";
        txt_Place_Of_Birth.Text = "";
        txt_Language_Known.Text = "";
        txt_Area_Of_Expertise.Text = "";
        txt_Passport_Validity_Date.Text = "";
        txt_Visa_Validity_Date.Text = "";
        txt_Details_Of_Handicap.Text = "";
        txt_qualification_1.Text = "";
        txt_year_of_passing_1.Text = "";
        txt_qualification_2.Text = "";
        txt_year_of_passing_2.Text = "";
        txt_qualification_3.Text = "";
        txt_year_of_passing_3.Text = "";
        txt_qualification_4.Text = "";
        txt_year_of_passing_4.Text = "";
        txt_qualification_5.Text = "";
        txt_year_of_passing_5.Text = "";
        txt_key_skill_1.Text = "";
        txt_experience_in_months_1.Text = "";
        txt_key_skill_2.Text = "";
        txt_experience_in_months_2.Text = "";
        txt_key_skill_3.Text = "";
        txt_experience_in_months_3.Text = "";
        txt_key_skill_4.Text = "";
        txt_experience_in_months_4.Text = "";
        txt_key_skill_5.Text = "";
        txt_experience_in_months_5.Text = "";
        //--family Details ---
        txt_name1.Text = "";
        txt_relation1.Text = "Father";
        txt_dob1.Text = "";
        txt_pan1.Text = "";
        txt_adhaar1.Text = "";
        txt_name2.Text = "";
        txt_relation2.Text = "Mother";
        txt_dob2.Text = "";
        txt_pan2.Text = "";
        txt_adhaar2.Text = "";
        txt_name3.Text = "";
        txt_relation3.Text = "Wife";
        txt_dob3.Text = "";
        txt_pan3.Text = "";
        txt_adhaar3.Text = "";
        txt_name4.Text = "";
        txt_relation4.Text = "Child";
        txt_dob4.Text = "";
        txt_pan4.Text = "";
        txt_adhaar4.Text = "";
        txt_name5.Text = "";
        txt_relation5.Text = "Child";
        txt_dob5.Text = "";
        txt_pan5.Text = "";
        txt_adhaar5.Text = "";
        txt_name6.Text = "";
        txt_relation6.Text = "Child";
        txt_dob6.Text = "";
        txt_pan6.Text = "";
        txt_adhaar6.Text = "";
        txt_name7.Text = "";
        txt_relation7.Text = "Child";
        txt_dob7.Text = "";
        txt_pan7.Text = "";
        txt_adhaar7.Text = "";
        Numberchild.Text = "0";
        ddl_unit_client.SelectedValue = "Select";
        ddl_unitcode.Items.Clear();
        txt_emailid1.Text = "";
        txt_emailid2.Text = "";
        try
        {
            DropDownList1.SelectedIndex = 0;
        }
        catch { }
        txt_bankholder.Text = "";
        ddl_bankcode.Text = "";
        ddl_infitcode.SelectedValue = "Select";
        //Txt_cca.Text = "0";
        //Txt_gra.Text = "0";
        //Txt_allow.Text = "0";
        txt_fine.Text = "0";
        ddl_employee_type.SelectedIndex = 0;
        ddl_location.Text = "";
        Image4.ImageUrl = "~/Images/placeholder.png";
        Image1.ImageUrl = "~/Images/pan.jpg";
        Image10.ImageUrl = "~/Images/certificate.jpg";
        Image11.ImageUrl = "~/Images/certificate.jpg";
        Image12.ImageUrl = "~/Images/certificate.jpg";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image3.ImageUrl = "~/Images/Biodata.png";
        Image5.ImageUrl = "~/Images/Passport.jpg";
        Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        Image7.ImageUrl = "~/Images/marksheet.jpg";
        Image8.ImageUrl = "~/Images/marksheet.jpg";
        Image9.ImageUrl = "~/Images/certificate.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
        Image15.ImageUrl = "~/Images/certificate.jpg";
        Image16.ImageUrl = "~/Images/certificate.jpg";
        Image17.ImageUrl = "~/Images/certificate.jpg";
        Image18.ImageUrl = "~/Images/certificate.jpg";
        Image19.ImageUrl = "~/Images/certificate.jpg";
        Image20.ImageUrl = "~/Images/certificate.jpg";
        Image21.ImageUrl = "~/Images/certificate.jpg";
        Image22.ImageUrl = "~/Images/certificate.jpg";
        Image23.ImageUrl = "~/Images/certificate.jpg";
        Image24.ImageUrl = "~/Images/certificate.jpg";
        Image25.ImageUrl = "~/Images/certificate.jpg";
        //  txt_attendanceid.SelectedValue = "0";
    }

    public void photo_image_clear()
    {

        Image4.ImageUrl = "~/Images/placeholder.png";
        Image1.ImageUrl = "~/Images/pan.jpg";
        Image10.ImageUrl = "~/Images/certificate.jpg";
        Image11.ImageUrl = "~/Images/certificate.jpg";
        Image12.ImageUrl = "~/Images/certificate.jpg";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image3.ImageUrl = "~/Images/Biodata.png";
        Image5.ImageUrl = "~/Images/Passport.jpg";
        Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        Image7.ImageUrl = "~/Images/marksheet.jpg";
        Image8.ImageUrl = "~/Images/marksheet.jpg";
        Image9.ImageUrl = "~/Images/certificate.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
        Image15.ImageUrl = "~/Images/certificate.jpg";
        Image16.ImageUrl = "~/Images/certificate.jpg";
        Image17.ImageUrl = "~/Images/certificate.jpg";
        Image18.ImageUrl = "~/Images/certificate.jpg";
        Image19.ImageUrl = "~/Images/certificate.jpg";
        Image20.ImageUrl = "~/Images/certificate.jpg";
        Image21.ImageUrl = "~/Images/certificate.jpg";
        Image22.ImageUrl = "~/Images/certificate.jpg";
        Image23.ImageUrl = "~/Images/certificate.jpg";
        Image24.ImageUrl = "~/Images/certificate.jpg";
        Image25.ImageUrl = "~/Images/certificate.jpg";
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        EmployeeBAL ebl3 = new EmployeeBAL();
        int result = 0;
        try
        {
            string diff_emp_type = d.getsinglestring("select `Employee_type` from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' AND `pay_employee_master`.`client_code` = '" + ddl_unit_client.SelectedValue + "' AND `pay_employee_master`.`location` = '" + ddl_clientwisestate.SelectedValue + "' AND `pay_employee_master`.`unit_code` = '" + ddl_unitcode.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

            string employee_type11 = ""+ddl_employee_type.SelectedValue+"";
            if (diff_emp_type != employee_type11)
            {

            // count check 23-05-2020 komal 
                string designation_count = d.getsinglestring("SELECT sum(count) FROM `pay_designation_count` WHERE `pay_designation_count`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_designation_count`.`client_code` = '" + ddl_unit_client.SelectedValue + "' AND `pay_designation_count`.`STATE` = '" + ddl_clientwisestate.SelectedValue + "' AND `pay_designation_count`.`unit_code` = '" + ddl_unitcode.SelectedValue + "' and `DESIGNATION` = '" + ddl_grade.SelectedItem + "'");


            string emp_count_check = d.getsinglestring("SELECT count(emp_code) FROM `pay_employee_master` WHERE `pay_employee_master`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' and `LEFT_DATE` is null  AND `pay_employee_master`.`client_code` = '" + ddl_unit_client.SelectedValue + "' AND `pay_employee_master`.`location` = '" + ddl_clientwisestate.SelectedValue + "' AND `pay_employee_master`.`unit_code` = '" + ddl_unitcode.SelectedValue + "' AND `employee_type` = '" + ddl_employee_type.SelectedValue + "'");

            if (designation_count == emp_count_check)
            {
                string employee_type = "" + ddl_employee_type.SelectedValue + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Count Full For Employee Type of " + employee_type + "  !!.');", true);
                return;
            }
            
            }

        string joining_date11 = d.getsinglestring("select DATE_FORMAT(unit_start_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION=(select GRADE_DESC from pay_grade_master where GRADE_CODE = '" + ddl_grade.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') and unit_code='" + ddl_unitcode.SelectedValue + "' limit 1 ");
        string end_date = d.getsinglestring("select DATE_FORMAT(unit_end_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION=(select GRADE_DESC from pay_grade_master where GRADE_CODE = '" + ddl_grade.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') and unit_code='" + ddl_unitcode.SelectedValue + "' limit 1 ");
       

            if (joining_date11 != "" && end_date != "")
            {
                if (joining_date11 != "00/00/0000" && end_date != "00/00/0000")
                {

                    if (DateTime.ParseExact(txt_joiningdate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(joining_date11, "dd/MM/yyyy", null))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Joining Date Cannot be less than " + joining_date11 + ".')", true);
                        txt_joiningdate.Focus();
                        newpanel.Visible = true;
                        return;
                    }
                    if (DateTime.ParseExact(txt_joiningdate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(end_date, "dd/MM/yyyy", null))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Joining Date Cannot be Grater than " + end_date + ".')", true);
                        txt_joiningdate.Focus();
                        newpanel.Visible = true;
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please check joining date " + joining_date11 + " and end date  " + end_date + " in unit master .')", true);
                    txt_joiningdate.Focus();
                    newpanel.Visible = true;
                    return;
                }

            }



            //  25-05-2020 komal end

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string check = d.getsinglestring("SELECT grade_code FROM pay_employee_master WHERE  comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND client_code = '" + ddl_unit_client.SelectedValue + "'  AND UNIT_CODE =  '" + ddl_unitcode.SelectedValue + "'  and emp_code='" + txt_eecode.Text + "' AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL)");
            if (ddl_grade.SelectedValue != check)
            {
                string d_count = d.getsinglestring("SELECT COUNT(employee_type) FROM  pay_employee_master WHERE client_code = '" + ddl_unit_client.SelectedValue + "'  AND unit_code = '" + ddl_unitcode.SelectedValue + "' AND comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND GRADE_CODE = '" + ddl_grade.SelectedValue + "'  AND employee_type = 'Permanent' and left_date is null");
                string d_count_d = d.getsinglestring("SELECT sum(count) FROM pay_designation_count WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND client_code = '" + ddl_unit_client.SelectedValue + "'  AND state = '" + ddl_clientwisestate.SelectedValue + "'  AND UNIT_CODE = '" + ddl_unitcode.SelectedValue + "' AND DESIGNATION = '" + ddl_grade.SelectedItem + "'");
                if (ddl_employee_type.SelectedValue == "Permanent" || ddl_employee_type.SelectedValue == "Staff")
                {
                    if (int.Parse(d_count_d) <= int.Parse(d_count))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Count Already Full Fill!!.');", true);
                        reason_panel.Visible = false;
                        return;

                    }
                }
                else
                {
                    string reliver_emp_count = d.getsinglestring("Select ifnull(sum(1),0) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND pay_employee_master.Employee_type = 'Reliever' and attendance_id = '" + txt_attendanceid.SelectedValue + "'  and GRADE_CODE ='" + ddl_grade.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
                    int count = Convert.ToInt32(d_count) * 3;
                    if (Convert.ToInt32(reliver_emp_count) >= (count))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Count Already Full Fill!!.');", true);
                        reason_panel.Visible = false;
                        return;
                    }
                }

            }
            //if (!(ddl_unit_client.SelectedValue == "BALIC" || ddl_unit_client.SelectedValue == "BAGIC"))
            //{
            //    if (d.getsinglestring("select employee_type from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + txt_eecode.Text + "'").Contains("Permanent"))
            //    {
            //        if (!check_document())
            //        {
            //            return;
            //        }
            //    }
            //}
            int age = 0;

            DateTime current_date = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime birth = DateTime.ParseExact(txt_birthdate.Text.ToString(), "dd/MM/yyyy", null);

            age = current_date.Year - birth.Year;

            if (current_date < birth.AddYears(age))
            {
                age--;

            }
            if (age < 18 || age > 55)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please check Date of Birth !!! Employee should be 18 to 55 Years old');", true);
                txt_birthdate.Text = "";
                txt_birthdate.Focus();
                newpanel.Visible = true;
                return;
            }

            string cmpcode = Session["comp_code"].ToString();
            string enteruserid = Session["USERID"].ToString();
            string entrydatestmp = Session["system_curr_date"].ToString();
            set_data();
            string emp_name = Session["USERNAME"].ToString();
            string newdate = Convert.ToString(System.DateTime.Now);


            d.con1.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select EMP_BANK_STATEMENT from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Submit the ADHAR_CARD of employee');", true);
                    newpanel.Visible = true;
                    return;
                }
                dr.Close();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }


            //passpo photo
            if (ddl_employee_type.SelectedValue == "Permanent")
            {
                string s1 = "", s2 = "";
                d.con1.Open();
                MySqlCommand cmd4 = new MySqlCommand("select original_adhar_card,original_photo from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                try
                {
                    MySqlDataReader dr4 = cmd4.ExecuteReader();
                    if (dr4.Read())
                    {
                        s1 = dr4.GetValue(0).ToString();
                        s2 = dr4.GetValue(1).ToString();
                        if (ddl_clientwisestate.SelectedValue == "Arunachal Pradesh" || ddl_clientwisestate.SelectedValue == "Assam" || ddl_clientwisestate.SelectedValue == "Manipur" || ddl_clientwisestate.SelectedValue == "Meghalaya" || ddl_clientwisestate.SelectedValue == "Mizoram " || ddl_clientwisestate.SelectedValue == "Nagaland" || ddl_clientwisestate.SelectedValue == "Sikkim" || ddl_clientwisestate.SelectedValue == "Tripura")
                        {
                            if (s2 == "")
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo ');", true);
                                newpanel.Visible = true;
                                return;
                            }
                        }
                        else if (s1 == "" || s2 == "")
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo and Adhar Card of Employee');", true);
                            newpanel.Visible = true;
                            return;
                        }

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit The Employee Photo and Adhar Card of Employee');", true);
                        newpanel.Visible = true;
                        return;
                    }

                    dr4.Close();
                    d.con1.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con1.Close();
                }
            }
            //Police ification doc

            //d.con1.Open();
            //try
            //{
            //    MySqlCommand cmd5 = new MySqlCommand("select POLICE_VERIFICATION_DOC,original_policy_document from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            //    MySqlDataReader dr5 = cmd5.ExecuteReader();

            //    if (dr5.Read())
            //    {
            //        DateTime joinindgdate1 = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
            //        DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
            //        TimeSpan count = newdate1 - joinindgdate1;
            //        int Days = Convert.ToInt32(count.TotalDays);

            //        int differance = Convert.ToInt32(Days);

            //        if (differance > 15)
            //        {
            //            string s = dr5.GetValue(0).ToString();
            //            string s1 = dr5.GetValue(1).ToString();

            //            if ((s == "") && (s1 == ""))
            //            {
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Submit At List One Police Verification Document Proof of employee');", true);
            //                newpanel.Visible = true;
            //                return;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex) { throw ex; }
            //finally { d.con1.Close(); }
            ////////addrresss proof
            if (txt_presentaddress.Text != txt_permanantaddress.Text)
            {

                DateTime joinindgdate1 = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
                DateTime newdate2 = Convert.ToDateTime(System.DateTime.Now);
                TimeSpan count1 = newdate2 - joinindgdate1;
                int Days1 = Convert.ToInt32(count1.TotalDays);

                int differance1 = Convert.ToInt32(Days1);

                if (differance1 > 15)
                {
                    d.con1.Open();
                    try
                    {

                        MySqlCommand cmd1 = new MySqlCommand("select present_add_proof,original_address_proof from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();

                        if (dr1.Read())
                        {
                            if (dr1.GetValue(0).ToString() == "" && dr1.GetValue(1).ToString() == "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Submit the Present Address Proof of Employee');", true);
                                newpanel.Visible = true;
                                return;
                            }
                        }
                        dr1.Close();
                        d.con1.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        d.con1.Close();
                    }
                }
            }

            //check  left employee
            if (!txt_leftdate.Text.Equals(""))
            {
                DateTime joining_date = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
                DateTime left_date = DateTime.ParseExact(txt_leftdate.Text.ToString(), "dd/MM/yyyy", null);
                int date = DateTime.Compare(joining_date, left_date);
                if (date > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Left date should be after than joining date ');", true);
                    newpanel.Visible = true;
                    return;
                }
            }
            //Adharcard Num
            if (ddl_employee_type.SelectedValue != "Select" && txt_leftdate.Text.Equals(""))
            {
                string rejoin_date = d.getsinglestring("SELECT rejoin_date FROM pay_employee_master WHERE   emp_code = '" + txt_eecode.Text + "' AND pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'");
                if (rejoin_date == "")
                {

                    d.con1.Open();
                    try
                    {
                        MySqlCommand cmd_1 = new MySqlCommand("SELECT P_TAX_NUMBER,EMP_NAME,EMP_CODE from  pay_employee_master WHERE  Employee_type!='Reliever' and P_TAX_NUMBER='" + txt_ptaxnumber.Text + "'  and emp_code!='" + txt_eecode.Text + "' and P_TAX_NUMBER !='' AND pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con1);
                        //  MySqlCommand cmd_1 = new MySqlCommand("SELECT P_TAX_NUMBER,EMP_NAME,EMP_CODE from  pay_employee_master", d.con1);
                        MySqlDataReader dr_1 = cmd_1.ExecuteReader();
                        if (dr_1.Read())
                        {
                            newpanel.Visible = true;
                            string adnar_card_no = dr_1.GetValue(0).ToString();
                            string emp_name1 = dr_1.GetValue(1).ToString();
                            string emp_id = dr_1.GetValue(2).ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Duplicate Adhar Card number already exist, for this : " + emp_name1 + " employee try another Adhar number.');", true);
                            dr_1.Close();
                            d.con1.Close();
                            return;
                        }
                        newpanel.Visible = true;
                        d.con1.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con1.Close(); }
                }
            }
            if (ddl_employee_type.SelectedValue != "Reliever" && txt_leftdate.Text.Equals(""))
            {
                //d.con1.Open();
                //MySqlCommand cmd_1 = new MySqlCommand("SELECT BANK_EMP_AC_CODE,original_bank_account_no,EMP_NAME,EMP_CODE FROM pay_employee_master WHERE BANK_EMP_AC_CODE='" + (txt_employeeaccountnumber.Text == "" ? txt_originalbankaccountno.Text : txt_employeeaccountnumber.Text) + "' and comp_code='" + Session["comp_code"].ToString() + "' and Employee_type!='Reliever'  and  emp_code!='" + txt_eecode.Text + "'", d.con1);
                //MySqlDataReader dr_1 = cmd_1.ExecuteReader();
                //if (dr_1.Read())
                //{
                //    newpanel.Visible = true;
                //    string emp_name1 = dr_1.GetValue(1).ToString();
                //    string emp_id = dr_1.GetValue(2).ToString();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Duplicate   bank account number already exist for employee '+'" + emp_id + "'+'-'+'" + emp_name1 + "'+' , try another account number.');", true);
                //    dr_1.Close();
                //}
                //dr_1.Close();
                //d2.con.Open();
                try
                {
                    string rejoin_date = d.getsinglestring("SELECT rejoin_date FROM pay_employee_master WHERE   emp_code = '" + txt_eecode.Text + "' AND pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'");
                    if (rejoin_date == "")
                    {
                        d2.con.Open();
                        MySqlCommand cmd_2 = new MySqlCommand("SELECT original_bank_account_no,original_bank_account_no,EMP_NAME,EMP_CODE FROM pay_employee_master WHERE original_bank_account_no='" + (txt_originalbankaccountno.Text.Trim() == "" ? txt_employeeaccountnumber.Text : txt_originalbankaccountno.Text.Trim()) + "' and comp_code='" + Session["comp_code"].ToString() + "' and Employee_type!='Reliever'     and (left_date is null || left_date = '') and  emp_code!='" + txt_eecode.Text + "'", d2.con);
                        MySqlDataReader dr_2 = cmd_2.ExecuteReader();
                        if (dr_2.Read())
                        {
                            if (!dr_2.GetValue(1).ToString().Trim().Equals(""))
                            {
                                newpanel.Visible = true;
                                string emp_name1 = dr_2.GetValue(1).ToString();
                                string emp_id = dr_2.GetValue(2).ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Original bank account number already exist for employee '+'" + emp_id + "'+'-'+'" + emp_name1 + "'+' , try another account number.');", true);
                                dr_2.Close();
                                return;
                            }
                        }
                        dr_2.Close();
                    }
                    {
                        txt_fine.Text = "0";
                    }
                    ////vikas add for 02/07/2019
                    if (check_document1() == false)
                    {
                        return;
                    }
                    //else
                    //{ return; }   


                    if (chk_uniform() == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Uniform Or Apron and  Shose compulsory Of Employee!!.');", true);
                        newpanel.Visible = true;
                        return;
                    }
                    if (chk_IDCard() == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter ID-Card Information Of Employee!!.');", true);
                        newpanel.Visible = true;
                        return;
                    }
                    //string no_of_set0 = "";
                    //no_of_set0 = d.getsinglestring("select No_of_set from pay_document_details where comp_code = '"+Session[""].ToString()+"'");

                    d.operation("Delete From pay_document_details where emp_code = '" + txt_eecode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' ");

                    result = d.operation("UPDATE pay_employee_master SET comp_code='" + Session["comp_code"] + "',EMP_CODE='" + txt_eecode.Text + "',EMP_NAME = '" + txt_eename.Text + "',EMP_FATHER_NAME = '" + txt_eefatharname.Text + "',BIRTH_DATE =  str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y'),JOINING_DATE = str_to_date('" + txt_joiningdate.Text + "','%d/%m/%Y'),CONFIRMATION_DATE = str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y'),LEFT_DATE = case when '" + txt_leftdate.Text.Trim() + "'='' then NULL else str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y') END,GENDER = '" + ddl_gender.Text + "',UNIT_CODE = '" + ddl_unitcode.SelectedValue + "',GRADE_CODE = '" + ddl_grade.SelectedValue + "',BANK_BRANCH = '" + ddl_bankcode.Text + "',BANK_EMP_AC_CODE = '" + txt_employeeaccountnumber.Text + "',EMP_CURRENT_ADDRESS = '" + txt_presentaddress.Text + "',EMP_CURRENT_CITY = '" + txt_presentcity.SelectedItem.Text + "',EMP_CURRENT_STATE = '" + ddl_state.SelectedItem.Text + "',EMP_CURRENT_PIN = '" + txt_presentpincode.Text + "',EMP_PERM_ADDRESS = '" + txt_permanantaddress.Text + "',EMP_PERM_CITY = '" + txt_permanantcity.SelectedItem.Text + "',EMP_PERM_STATE = '" + ddl_permstate.SelectedItem.Text + "',EMP_PERM_PIN = '" + txt_permanantpincode.Text + "',EMP_MOBILE_NO = '" + txt_mobilenumber.Text + "',EMP_MOBILE_NO2 = '" + txt_residencecontactnumber.Text + "',EMP_EMAIL_ID = '" + txt_email.Text + "',EMP_MARRITAL_STATUS = '" + ddl_MaritalStatus.SelectedValue + "',EMP_BLOOD_GROUP = '" + ddl_bloodgroup.Text + "',EMP_HOBBIES = '" + txt_hobbies.Text + "',EMP_WEIGHT = '" + float.Parse((txt_weight.Text)) + "',EMP_RELIGION = '" + ddl_religion.SelectedItem.Text + "',EMP_HEIGHT = '" + float.Parse((txt_height.Text)) + "',LOCATION = '" + ddl_location.Text + "' ,P_TAX_DEDUCTION_FLAG = '" + ddl_ptaxdeductionflag.SelectedItem.Text + "',P_TAX_NUMBER = '" + txt_ptaxnumber.Text + "',LEFT_REASON = '" + txtreasonforleft.Text + "',PF_SHEET = '" + ddlpfregisteremp.Text + "' ,FATHER_RELATION = '" + ddl_relation.SelectedItem.Text + "', ENTER_USER_ID='" + enteruserid + "',DATE_STMP = str_to_date('" + entrydatestmp + "','%d/%m/%Y'),PF_BANK_NAME  = '" + txt_pfbankname.Text + "',PF_IFSC_CODE  = '" + txt_pfifsccode.Text.Trim() + "',PF_NOMINEE_NAME  = '" + txt_pfnomineename.Text + "',PF_NOMINEE_RELATION  = '" + txt_pfnomineerelation.Text + "',PF_NOMINEE_BDATE = case when '" + txt_pfbdate.Text.Trim() + "'='' then NULL else str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y') END,EMP_ADVANCE_PAYMENT = '" + txt_advance_payment.Text + "',REFNAME1 = '" + txtrefname1.Text + "',REFMOBILE1 = '" + txtref1mob.Text + "',REFADDRESS1='" + txt_address1.Text + "',REFNAME2 = '" + txtrefname2.Text + "',REFMOBILE2 = '" + txtref2mob.Text + "',REFADDRESS2='" + txt_address2.Text + "',EMP_NATIONALITY = '" + txt_Nationality.Text + "',EMP_IDENTITYMARK = '" + txt_Identitymark.Text + "',EMP_MOTHERTONGUE = '" + ddl_Mother_Tongue.Text + "',EMP_PASSPORTNUMBER = '" + txt_Passport_No.Text + "',EMP_VISA_COUNTRY = '" + ddl_Visa_Country.SelectedItem.Text + "',EMP_DRIVING_LICENSCE = '" + txt_Driving_License_No.Text + "',EMP_MISE = '" + txt_Mise.Text + "',EMP_PLACE_OF_BIRTH = '" + txt_Place_Of_Birth.Text + "',EMP_LANGUAGE_KNOWN = '" + txt_Language_Known.Text + "',EMP_AREA_OF_EXPERTISE = '" + txt_Area_Of_Expertise.Text + "',EMP_PASSPORT_VALIDITY_DATE = '" + txt_Passport_Validity_Date.Text + "',EMP_VISA_VALIDITY_DATE = '" + txt_Visa_Validity_Date.Text + "',EMP_DETAILS_OF_HANDICAP = '" + txt_Details_Of_Handicap.Text + "',QUALIFICATION_1 = '" + txt_qualification_1.Text + "',YEAR_OF_PASSING_1 = '" + txt_year_of_passing_1.Text + "',QUALIFICATION_2 = '" + txt_qualification_2.Text + "',YEAR_OF_PASSING_2 = '" + txt_year_of_passing_2.Text + "',QUALIFICATION_3 = '" + txt_qualification_3.Text + "',YEAR_OF_PASSING_3 = '" + txt_year_of_passing_3.Text + "',QUALIFICATION_4 = '" + txt_qualification_4.Text + "',YEAR_OF_PASSING_4 = '" + txt_year_of_passing_4.Text + "',QUALIFICATION_5 = '" + txt_qualification_5.Text + "',YEAR_OF_PASSING_5 = '" + txt_year_of_passing_5.Text + "',KEY_SKILL_1 = '" + txt_key_skill_1.Text + "',EXPERIENCE_MONTH_1 = '" + txt_experience_in_months_1.Text + "',KEY_SKILL_2 = '" + txt_key_skill_2.Text + "',EXPERIENCE_MONTH_2 = '" + txt_experience_in_months_2.Text + "',KEY_SKILL_3 = '" + txt_key_skill_3.Text + "',EXPERIENCE_MONTH_3 = '" + txt_experience_in_months_3.Text + "',KEY_SKILL_4 = '" + txt_key_skill_4.Text + "',EXPERIENCE_MONTH_4 = '" + txt_experience_in_months_4.Text + "',KEY_SKILL_5 = '" + txt_key_skill_5.Text + "',EXPERIENCE_MONTH_5 = '" + txt_experience_in_months_5.Text + "',reporting_to = '" + ddl_reporting_to.SelectedValue + "',emistartdate = '" + txt_loandate.Text + "',attendance_id = '" + txt_attendanceid.Text + "',in_time = '" + ddl_intime.SelectedValue + "',name1='" + txt_name1.Text + "',relation1='" + txt_relation1.Text + "',dob1='" + txt_dob1.Text + "',pan1='" + txt_pan1.Text + "',adhaar1='" + txt_adhaar1.Text + "',name2='" + txt_name2.Text + "',relation2='" + txt_relation2.Text + "',dob2='" + txt_dob2.Text + "',pan2='" + txt_pan2.Text + "',adhaar2='" + txt_adhaar2.Text + "',name3='" + txt_name3.Text + "',relation3='" + txt_relation3.Text + "',dob3='" + txt_dob3.Text + "',pan3='" + txt_pan3.Text + "',adhaar3='" + txt_adhaar3.Text + "',name4='" + txt_name4.Text + "',relation4='" + txt_relation4.Text + "',dob4='" + txt_dob4.Text + "',pan4='" + txt_pan4.Text + "',adhaar4='" + txt_adhaar4.Text + "',name5='" + txt_name5.Text + "',relation5='" + txt_relation5.Text + "',dob5='" + txt_dob5.Text + "',pan5='" + txt_pan5.Text + "',adhaar5='" + txt_adhaar5.Text + "',name6='" + txt_name6.Text + "',relation6='" + txt_relation6.Text + "',dob6='" + txt_dob6.Text + "',pan6='" + txt_pan6.Text + "',adhaar6='" + txt_adhaar6.Text + "',name7='" + txt_name7.Text + "',relation7='" + txt_relation7.Text + "',dob7='" + txt_dob7.Text + "',pan7='" + txt_pan7.Text + "',adhaar7='" + txt_adhaar7.Text + "',No_of_child='" + Numberchild.Text + "'  ,Login_Person='" + emp_name + "',LastModifyDate= now(),KRA='" + txt_kra.Text + "',LOCATION_CITY='" + ddl_location_city.Text + "' ,BANK_HOLDER_NAME='" + txt_bankholder.Text.Trim() + "' ,POLICE_STATION_NAME='" + txt_policestationname.Text + "' ,F_MOBILE1='" + txt_fmobile1.Text + "' ,F_MOBILE2='" + txt_fmobile2.Text + "',F_MOBILE3='" + txt_fmobile3.Text + "',F_MOBILE4='" + txt_fmobile4.Text + "',F_MOBILE5='" + txt_fmobile5.Text + "' ,F_MOBILE6='" + txt_fmobile6.Text + "' ,F_MOBILE7='" + txt_fmobile7.Text + "' ,ihmscode='" + txt_ihmscode.Text + "',NFD_CODE='" + ddl_infitcode.SelectedValue + "',CONTANCEPERSON1_EMAILID='" + txt_emailid1.Text + "',CONTANCEPERSON2_EMAILID='" + txt_emailid2.Text + "',CLIENT_CODE='" + ddl_unit_client.SelectedValue.ToString() + "' , comments=concat(IFNULL(comments,''),'" + txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString() + " ON-',now(),'@#$%'),fine='" + txt_fine.Text + "',Employee_type='" + ddl_employee_type.SelectedValue + "',police_verification_start_date= case when '" + txt_start_date.Text.Trim() + "'='' then NULL else str_to_date('" + txt_start_date.Text + "','%d/%m/%Y') END,police_verification_End_date= case when '" + txt_end_date.Text.Trim() + "'='' then NULL else str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') END,rent_agreement_start_date='" + txt_ranteagrement_satrtdate.Text + "',rent_agreement_end_date= case when '" + txt_ranteagrement_enddate.Text.Trim() + "'='' then NULL else str_to_date('" + txt_ranteagrement_enddate.Text + "','%d/%m/%Y') END,client_wise_state='" + ddl_clientwisestate.SelectedValue + "',pre_mobileno_1='" + pre_mobileno_1.Text + "',pre_mobileno_2='" + pre_mobileno_2.Text + "',premnent_mobileno_1='" + txt_premanent_mob1.Text + "',premnent_mobileno_2='" + txt_premanent_mob2.Text + "',original_bank_account_no='" + txt_originalbankaccountno.Text.Trim() + "',department_type='" + ddl_department.SelectedValue + "'  WHERE (comp_code = '" + cmpcode + "') AND (EMP_CODE = '" + txt_eecode.Text + "')");


                    foreach (GridViewRow row in gv_product_details.Rows)
                    {
                        d.operation("INSERT INTO pay_document_details (comp_code,client_code,unit_code,emp_code,reporting_to,document_type,admin_no_of_set,remaining_no_set,size,start_date,end_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "','" + ddl_reporting_to.SelectedValue + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[7].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[5].Text + "','%d/%m/%Y'),str_to_date('" + row.Cells[6].Text + "','%d/%m/%Y'))");
                    }


                    d.operation("update pay_user_master set USER_NAME= '" + txt_eename.Text + "',ROLE='" + DropDownList1.SelectedItem.Text + "',modify_date=now(),modify_user='" + Session["USERID"].ToString() + "' where login_id = '" + txt_eecode.Text + "' and comp_code='" + cmpcode + "'");

                    if ((txt_leftdate.Text).Trim() != "")
                    {
                        d.operation("UPDATE pay_user_master SET flag = 'L' WHERE LOGIN_ID='" + txt_eecode.Text + "' and comp_code ='" + cmpcode + "'");
                      //  btn_releiving_letter_Click(null, null);
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updated successfully!!');", true);

                        //  btnnew_Click();

                        reason_panel.Visible = false;
                        pln_searchemp.Visible = true;
                        pnl_buttons.Visible = true;
                        gv_app_gridview.Visible = false;
                        newpanel.Visible = false;
                        text_Clear();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updated Successfully!!.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updation failed...');", true);

                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con1.Close();
                    d2.con.Close();
                }
            }
            else
            {
                //if (Txt_cca.Text == "")
                //{
                //    Txt_cca.Text = "0";
                //}
                //if (Txt_gra.Text == "")
                //{
                //    Txt_gra.Text = "0";
                //}

                //if (Txt_allow.Text == "")
                //{
                //    Txt_allow.Text = "0";
                //}

                if (txt_fine.Text == "")
                {
                    txt_fine.Text = "0";
                }

                d.operation("Delete From pay_document_details where emp_code = '" + txt_eecode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' ");
                result = d.operation("UPDATE pay_employee_master SET comp_code='" + Session["comp_code"] + "',EMP_CODE='" + txt_eecode.Text + "',EMP_NAME = '" + txt_eename.Text + "',EMP_FATHER_NAME = '" + txt_eefatharname.Text + "',BIRTH_DATE =  str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y'),JOINING_DATE = str_to_date('" + txt_joiningdate.Text + "','%d/%m/%Y'),CONFIRMATION_DATE = str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y'),LEFT_DATE = case when '" + txt_leftdate.Text + "'='' then NULL else str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y') END,GENDER = '" + ddl_gender.Text + "',UNIT_CODE = '" + ddl_unitcode.SelectedValue + "',GRADE_CODE = '" + ddl_grade.SelectedValue + "',BANK_BRANCH = '" + ddl_bankcode.Text + "',BANK_EMP_AC_CODE = '" + txt_employeeaccountnumber.Text + "',EMP_CURRENT_ADDRESS = '" + txt_presentaddress.Text + "',EMP_CURRENT_CITY = '" + txt_presentcity.SelectedItem.Text + "',EMP_CURRENT_STATE = '" + ddl_state.SelectedItem.Text + "',EMP_CURRENT_PIN = '" + txt_presentpincode.Text + "',EMP_PERM_ADDRESS = '" + txt_permanantaddress.Text + "',EMP_PERM_CITY = '" + txt_permanantcity.SelectedItem.Text + "',EMP_PERM_STATE = '" + ddl_permstate.SelectedItem.Text + "',EMP_PERM_PIN = '" + txt_permanantpincode.Text + "',EMP_MOBILE_NO = '" + txt_mobilenumber.Text + "',EMP_MOBILE_NO2 = '" + txt_residencecontactnumber.Text + "',EMP_EMAIL_ID = '" + txt_email.Text + "',EMP_MARRITAL_STATUS = '" + ddl_MaritalStatus.SelectedValue + "',EMP_BLOOD_GROUP = '" + ddl_bloodgroup.Text + "',EMP_HOBBIES = '" + txt_hobbies.Text + "',EMP_WEIGHT = '" + float.Parse((txt_weight.Text)) + "',EMP_RELIGION = '" + ddl_religion.SelectedItem.Text + "',EMP_HEIGHT = '" + float.Parse((txt_height.Text)) + "',LOCATION = '" + ddl_location.Text + "' ,P_TAX_DEDUCTION_FLAG = '" + ddl_ptaxdeductionflag.SelectedItem.Text + "',P_TAX_NUMBER = '" + txt_ptaxnumber.Text + "',LEFT_REASON = '" + txtreasonforleft.Text + "',PF_SHEET = '" + ddlpfregisteremp.Text + "' ,FATHER_RELATION = '" + ddl_relation.SelectedItem.Text + "', ENTER_USER_ID='" + enteruserid + "',DATE_STMP = str_to_date('" + entrydatestmp + "','%d/%m/%Y'),PF_BANK_NAME  = '" + txt_pfbankname.Text.Trim() + "',PF_IFSC_CODE  = '" + txt_pfifsccode.Text.Trim() + "',PF_NOMINEE_NAME  = '" + txt_pfnomineename.Text + "',PF_NOMINEE_RELATION  = '" + txt_pfnomineerelation.Text + "',PF_NOMINEE_BDATE = str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'),EMP_ADVANCE_PAYMENT = '" + txt_advance_payment.Text + "',REFNAME1 = '" + txtrefname1.Text + "',REFMOBILE1 = '" + txtref1mob.Text + "',REFADDRESS1='" + txt_address1.Text + "',REFNAME2 = '" + txtrefname2.Text + "',REFMOBILE2 = '" + txtref2mob.Text + "',REFADDRESS2='" + txt_address2.Text + "',EMP_NATIONALITY = '" + txt_Nationality.Text + "',EMP_IDENTITYMARK = '" + txt_Identitymark.Text + "',EMP_MOTHERTONGUE = '" + ddl_Mother_Tongue.Text + "',EMP_PASSPORTNUMBER = '" + txt_Passport_No.Text + "',EMP_VISA_COUNTRY = '" + ddl_Visa_Country.SelectedItem.Text + "',EMP_DRIVING_LICENSCE = '" + txt_Driving_License_No.Text + "',EMP_MISE = '" + txt_Mise.Text + "',EMP_PLACE_OF_BIRTH = '" + txt_Place_Of_Birth.Text + "',EMP_LANGUAGE_KNOWN = '" + txt_Language_Known.Text + "',EMP_AREA_OF_EXPERTISE = '" + txt_Area_Of_Expertise.Text + "',EMP_PASSPORT_VALIDITY_DATE = '" + txt_Passport_Validity_Date.Text + "',EMP_VISA_VALIDITY_DATE = '" + txt_Visa_Validity_Date.Text + "',EMP_DETAILS_OF_HANDICAP = '" + txt_Details_Of_Handicap.Text + "',QUALIFICATION_1 = '" + txt_qualification_1.Text + "',YEAR_OF_PASSING_1 = '" + txt_year_of_passing_1.Text + "',QUALIFICATION_2 = '" + txt_qualification_2.Text + "',YEAR_OF_PASSING_2 = '" + txt_year_of_passing_2.Text + "',QUALIFICATION_3 = '" + txt_qualification_3.Text + "',YEAR_OF_PASSING_3 = '" + txt_year_of_passing_3.Text + "',QUALIFICATION_4 = '" + txt_qualification_4.Text + "',YEAR_OF_PASSING_4 = '" + txt_year_of_passing_4.Text + "',QUALIFICATION_5 = '" + txt_qualification_5.Text + "',YEAR_OF_PASSING_5 = '" + txt_year_of_passing_5.Text + "',KEY_SKILL_1 = '" + txt_key_skill_1.Text + "',EXPERIENCE_MONTH_1 = '" + txt_experience_in_months_1.Text + "',KEY_SKILL_2 = '" + txt_key_skill_2.Text + "',EXPERIENCE_MONTH_2 = '" + txt_experience_in_months_2.Text + "',KEY_SKILL_3 = '" + txt_key_skill_3.Text + "',EXPERIENCE_MONTH_3 = '" + txt_experience_in_months_3.Text + "',KEY_SKILL_4 = '" + txt_key_skill_4.Text + "',EXPERIENCE_MONTH_4 = '" + txt_experience_in_months_4.Text + "',KEY_SKILL_5 = '" + txt_key_skill_5.Text + "',EXPERIENCE_MONTH_5 = '" + txt_experience_in_months_5.Text + "',reporting_to = '" + ddl_reporting_to.SelectedValue + "',emistartdate = '" + txt_loandate.Text + "',attendance_id = '" + txt_attendanceid.Text + "',in_time = '" + ddl_intime.SelectedValue + "',name1='" + txt_name1.Text + "',relation1='" + txt_relation1.Text + "',dob1='" + txt_dob1.Text + "',pan1='" + txt_pan1.Text + "',adhaar1='" + txt_adhaar1.Text + "',name2='" + txt_name2.Text + "',relation2='" + txt_relation2.Text + "',dob2='" + txt_dob2.Text + "',pan2='" + txt_pan2.Text + "',adhaar2='" + txt_adhaar2.Text + "',name3='" + txt_name3.Text + "',relation3='" + txt_relation3.Text + "',dob3='" + txt_dob3.Text + "',pan3='" + txt_pan3.Text + "',adhaar3='" + txt_adhaar3.Text + "',name4='" + txt_name4.Text + "',relation4='" + txt_relation4.Text + "',dob4='" + txt_dob4.Text + "',pan4='" + txt_pan4.Text + "',adhaar4='" + txt_adhaar4.Text + "',name5='" + txt_name5.Text + "',relation5='" + txt_relation5.Text + "',dob5='" + txt_dob5.Text + "',pan5='" + txt_pan5.Text + "',adhaar5='" + txt_adhaar5.Text + "',name6='" + txt_name6.Text + "',relation6='" + txt_relation6.Text + "',dob6='" + txt_dob6.Text + "',pan6='" + txt_pan6.Text + "',adhaar6='" + txt_adhaar6.Text + "',name7='" + txt_name7.Text + "',relation7='" + txt_relation7.Text + "',dob7='" + txt_dob7.Text + "',pan7='" + txt_pan7.Text + "',adhaar7='" + txt_adhaar7.Text + "',No_of_child='" + Numberchild.Text + "'  ,Login_Person='" + emp_name + "',LastModifyDate= now(),KRA='" + txt_kra.Text + "',LOCATION_CITY='" + ddl_location_city.Text + "' ,BANK_HOLDER_NAME='" + txt_bankholder.Text.Trim() + "' ,POLICE_STATION_NAME='" + txt_policestationname.Text + "' ,F_MOBILE1='" + txt_fmobile1.Text + "' ,F_MOBILE2='" + txt_fmobile2.Text + "',F_MOBILE3='" + txt_fmobile3.Text + "',F_MOBILE4='" + txt_fmobile4.Text + "',F_MOBILE5='" + txt_fmobile5.Text + "' ,F_MOBILE6='" + txt_fmobile6.Text + "' ,F_MOBILE7='" + txt_fmobile7.Text + "' ,ihmscode='" + txt_ihmscode.Text + "',NFD_CODE='" + ddl_infitcode.SelectedValue + "',CONTANCEPERSON1_EMAILID='" + txt_emailid1.Text + "',CONTANCEPERSON2_EMAILID='" + txt_emailid2.Text + "',CLIENT_CODE='" + ddl_unit_client.SelectedValue.ToString() + "' , comments=concat(IFNULL(comments,''),'" + txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString() + " ON-',now(),'@#$%'),fine='" + txt_fine.Text + "',Employee_type='" + ddl_employee_type.SelectedValue + "',police_verification_start_date='" + txt_start_date.Text + "',police_verification_End_date='" + txt_end_date.Text + "',rent_agreement_start_date='" + txt_ranteagrement_satrtdate.Text + "',rent_agreement_end_date='" + txt_ranteagrement_enddate.Text + "',client_wise_state='" + ddl_clientwisestate.SelectedValue + "',pre_mobileno_1='" + pre_mobileno_1.Text + "',pre_mobileno_2='" + pre_mobileno_2.Text + "',premnent_mobileno_1='" + txt_premanent_mob1.Text + "',premnent_mobileno_2='" + txt_premanent_mob2.Text + "',original_bank_account_no='" + txt_originalbankaccountno.Text.Trim() + "',department_type='" + ddl_department.SelectedValue + "'  WHERE (comp_code = '" + cmpcode + "') AND (EMP_CODE = '" + txt_eecode.Text + "')");///P_TAX_NUMBER = '" + txt_ptaxnumber.Text + "' is Aadhar No   


                foreach (GridViewRow row in gv_product_details.Rows)
                {
                    d.operation("INSERT INTO pay_document_details (comp_code,client_code,unit_code,emp_code,reporting_to,document_type,admin_no_of_set,remaining_no_set,size,start_date,end_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "','" + ddl_reporting_to.SelectedValue + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[7].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[5].Text + "','%d/%m/%Y'),str_to_date('" + row.Cells[6].Text + "','%d/%m/%Y'))");
                }


                d.operation("update pay_user_master set USER_NAME= '" + txt_eename.Text + "',ROLE='" + DropDownList1.SelectedItem.Text + "',modify_date=now(),modify_user='" + Session["USERID"].ToString() + "' where login_id = '" + txt_eecode.Text + "' and comp_code='" + cmpcode + "'");

                if ((txt_leftdate.Text).Trim() != "")
                {
                    d.operation("UPDATE pay_user_master SET flag = 'L' WHERE LOGIN_ID='" + txt_eecode.Text + "' and comp_code ='" + cmpcode + "'");
                   // btn_releiving_letter_Click(null, null);
                }
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updated Successfully!!');", true);
                    text_Clear();
                    // btnnew_Click();
                    newpanel.Visible = false;
                    btnupdate.Visible = false;
                    btnUpload.Visible = false;
                    reason_panel.Visible = false;
                    pln_searchemp.Visible = true;
                    pnl_buttons.Visible = true;


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updation failed...');", true);

                }
            }

        }
        catch (Exception ee)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ee.Message + "');", true);

        }
        finally
        {
            btnhelp_Click(sender, e);
            newpanel.Visible = false;
            if (reason_panel.Visible == true)
            {
                btn_add.Visible = false;
                btnupdate.Visible = true;
                btndelete.Visible = true;
                newpanel.Visible = true;
            }
            else
            {
                btn_add.Visible = true;
                btnupdate.Visible = false;
                btndelete.Visible = false;
            }

        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        EmployeeBAL ebl4 = new EmployeeBAL();
        int result = 0;
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("Select EMP_CODE from pay_employee_master where  comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + txt_eecode.Text + "' and (EMP_CODE IN (Select EMP_CODE from pay_attendance_history) OR EMP_CODE IN (Select EMP_CODE from pay_attendance))", d.con1);
            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                dr_1.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee in salaries exist can not delete this employee');", true);
            }
            else
            {
                dr_1.Close();
                result = ebl4.EmployeeDelete(Session["comp_code"].ToString(), txt_eecode.Text);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);

                }
            }
            dr_1.Close();
            d.con1.Close();
        }
        catch (Exception ee)
        {
            throw ee;

        }
        finally
        {
            d.con1.Close();
            text_Clear();
            btnhelp_Click(sender, e);
            newpanel.Visible = false;
            reason_panel.Visible = false;
        }

    }

    protected void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.SearchGridView, "Select$" + e.Row.RowIndex);


        }
    }

    //protected void btnexcelexport_Click(object sender, EventArgs e)
    //{
    //    if (ddl_gv_statewise.SelectedValue != "Select" && ddl_gv_branchwise.SelectedValue != "Select" && ddlunitclient1.SelectedValue!="Select")
    //    {
    //        unit_export_to_excel();

    //    }

    //    else if (ddl_gv_statewise.SelectedValue != "Select" && ddlunitclient1.SelectedValue != "Select")
    //    {
    //        Statewise_export_to_excel();

    //    }
    //    else if (ddlunitclient1.SelectedValue != "Select")
    //    {
    //        client_export_to_excel();
    //    }


    //}

    protected void client_export_to_excel()
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 16;

        //Change all cells' alignment to center
        Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[500, 300]);
        rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


        Range rng = ws.get_Range("G1:G1");
        rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange2 = ws.get_Range("G1:G1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange2.Font.Size = 20;

        Range rng1 = ws.get_Range("G3:G3");
        rng1.Interior.Color = XlRgbColor.rgbGreen;

        Range formateRange1 = ws.get_Range("G3:G3");
        formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange1.Font.Size = 20;

        Range rng2 = ws.get_Range("A5:BM5");
        rng2.Interior.Color = XlRgbColor.rgbBlue;

        Range formateRange = ws.get_Range("A5:BM5");
        formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange.Font.Size = 15;


        ws.Cells[1, 7] = Session["COMP_NAME"].ToString();
        ws.Cells[3, 7] = "Employee List";
        ws.Cells[5, 1] = "CLIENT_NAME";
        ws.Cells[5, 2] = "EMP_CODE";
        ws.Cells[5, 3] = "EMP_NAME";
        ws.Cells[5, 4] = "FATHER NAME";
        ws.Cells[5, 5] = "JOINING DT";
        ws.Cells[5, 6] = "BIRTH DT";
        ws.Cells[5, 7] = "GRADE NAME";
        // ws.Cells[5, 7] = "DESIGNATION";

        //**AA**************************
        ws.Cells[5, 8] = "Reporting To";
        ws.Cells[5, 9] = "Present Address";
        ws.Cells[5, 10] = "Permanant Address";
        ws.Cells[5, 11] = "Mobile Number";
        //ws.Cells[5,17]  ="Joining Date"; 
        ws.Cells[5, 12] = "PAN Number";
        ws.Cells[5, 13] = "Adhar Card No.";

        //    //*********************************************************888888


        ws.Cells[5, 14] = "PF_NUMBER";

        ws.Cells[5, 15] = "ESIC_NUMBER";


        //****BB*****************************************************
        ws.Cells[5, 16] = "Bank Name";
        ws.Cells[5, 17] = "IFSC CODE";
        ws.Cells[5, 18] = "Bank Account Number";
        ws.Cells[5, 19] = "Nationality";
        ws.Cells[5, 20] = "Bank Holder Name";
        ws.Cells[5, 21] = "Working State";
        ws.Cells[5, 22] = "Working City";
        MySqlDataAdapter adp2;

        try
        {
            d.con1.Open();
            if (ddlunitclient1.SelectedValue == "Select")
            {
                // MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT pay_employee_master.CLENT_CODE,pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME,date_format(pay_employee_master.JOINING_DATE,'%d/%m/%Y') as JOINING_DATE,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as BIRTH_DATE,pay_grade_master.GRADE_DESC, pay_employee_master.STATUS,pay_department_master.DEPT_NAME,pay_unit_master.UNIT_NAME,(select emp_name from pay_employee_master a where a.emp_code = pay_employee_master.REPORTING_TO) as REPORTING_TO,pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_EMAIL_ID,date_format(pay_employee_master.CONFIRMATION_DATE,'%d/%m/%Y') as CONFIRMATION_DATE,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER,pay_employee_master.PF_DEDUCTION_FLAG,pay_employee_master.PF_NUMBER,pay_employee_master.ESIC_DEDUCTION_FLAG,pay_employee_master.ESIC_NUMBER,pay_employee_master.P_TAX_DEDUCTION_FLAG,pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.EMP_IDENTITYMARK, pay_employee_master.EMP_LANGUAGE_KNOWN,pay_employee_master.EMP_PASSPORTNUMBER,pay_employee_master.EMP_PASSPORT_VALIDITY_DATE,pay_employee_master.EMP_VISA_COUNTRY,pay_employee_master.EMP_VISA_VALIDITY_DATE,pay_employee_master.EMP_MARRITAL_STATUS,pay_employee_master.QUALIFICATION_1,pay_employee_master.YEAR_OF_PASSING_1,pay_employee_master.E_HEAD01,pay_employee_master.E_HEAD02,pay_employee_master.E_HEAD03,pay_employee_master.E_HEAD04,pay_employee_master.E_HEAD05,pay_employee_master.E_HEAD06,pay_employee_master.E_HEAD07,pay_employee_master.E_HEAD08,pay_employee_master.E_HEAD09,pay_employee_master.E_HEAD10,pay_employee_master.E_HEAD11,pay_employee_master.E_HEAD12,pay_employee_master.E_HEAD13,pay_employee_master.E_HEAD14,pay_employee_master.E_HEAD15,pay_employee_master.LS_HEAD01,pay_employee_master.LS_HEAD02,pay_employee_master.LS_HEAD03,pay_employee_master.LS_HEAD04,pay_employee_master.LS_HEAD05,pay_employee_master.D_HEAD01,pay_employee_master.D_HEAD02,pay_employee_master.D_HEAD03,pay_employee_master.D_HEAD04,pay_employee_master.D_HEAD05,pay_employee_master.D_HEAD06,pay_employee_master.D_HEAD07,pay_employee_master.D_HEAD08,pay_employee_master.D_HEAD09,pay_employee_master.D_HEAD010 FROM pay_company_master  INNER JOIN   pay_employee_master ON pay_company_master.comp_code = pay_employee_master.comp_code  INNER JOIN pay_department_master ON   pay_employee_master.comp_code = pay_department_master.comp_code AND pay_employee_master.DEPT_CODE = pay_department_master.DEPT_CODE INNER JOIN  pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN  pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND  pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE WHERE (pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "') ORDER BY pay_employee_master.EMP_CODE", d.con1);
                adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='" + Session["comp_code"].ToString() + "') as CLIENT_NAME, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.PF_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME,pay_employee_master.LOCATION ,pay_employee_master.LOCATION_CITY FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' ORDER BY pay_employee_master.EMP_CODE", d.con1);

            }
            else
            {
                adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='" + Session["comp_code"].ToString() + "') as CLIENT_NAME, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER,pay_employee_master.PF_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME,pay_employee_master.LOCATION ,pay_employee_master.LOCATION_CITY FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE='" + ddlunitclient1.SelectedValue + "'  ORDER BY pay_employee_master.EMP_CODE", d.con1);
            }
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
            d.con1.Close();
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

    //protected void Statewise_export_to_excel()
    //{
    //    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
    //    Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
    //    Worksheet ws = (Worksheet)xla.ActiveSheet;
    //    xla.Columns.ColumnWidth = 16;

    //    //Change all cells' alignment to center
    //    Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[500, 300]);
    //    rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


    //    Range rng = ws.get_Range("G1:G1");
    //    rng.Interior.Color = XlRgbColor.rgbDarkGreen;

    //    Range formateRange2 = ws.get_Range("G1:G1");
    //    formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange2.Font.Size = 20;

    //    Range rng1 = ws.get_Range("G3:G3");
    //    rng1.Interior.Color = XlRgbColor.rgbGreen;

    //    Range formateRange1 = ws.get_Range("G3:G3");
    //    formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange1.Font.Size = 20;

    //    Range rng2 = ws.get_Range("A5:BM5");
    //    rng2.Interior.Color = XlRgbColor.rgbBlue;

    //    Range formateRange = ws.get_Range("A5:BM5");
    //    formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange.Font.Size = 15;


    //    ws.Cells[1, 7] = Session["COMP_NAME"].ToString();
    //    ws.Cells[3, 7] = "Employee List";
    //    ws.Cells[5, 1] = "CLIENT_NAME";
    //    ws.Cells[5, 2] = "LOCATION";
    //    ws.Cells[5, 3] = "EMP_CODE";
    //    ws.Cells[5, 4] = "EMP_NAME";
    //    ws.Cells[5, 5] = "FATHER NAME";
    //    ws.Cells[5, 6] = "JOINING DT";
    //    ws.Cells[5, 7] = "BIRTH DT";
    //    ws.Cells[5, 8] = "GRADE NAME";
    //    // ws.Cells[5, 7] = "DESIGNATION";

    //    //**AA**************************
    //    ws.Cells[5, 9] = "Reporting To";
    //    ws.Cells[5, 10] = "Present Address";
    //    ws.Cells[5, 11] = "Permanant Address";
    //    ws.Cells[5, 12] = "Mobile Number";
    //    //ws.Cells[5,17]  ="Joining Date"; 
    //    ws.Cells[5, 13] = "PAN Number";
    //    ws.Cells[5, 14] = "Adhar Card No.";

    //*********************************************************888888


    //    ws.Cells[5, 15] = "PF_NUMBER";

    //    ws.Cells[5, 16] = "ESIC_NUMBER";


    //    //****BB*****************************************************
    //    ws.Cells[5, 17] = "Bank Name";
    //    ws.Cells[5, 18] = "IFSC CODE";
    //    ws.Cells[5, 19] = "Bank Account Number";
    //    ws.Cells[5, 20] = "Nationality";
    //    ws.Cells[5, 21] = "Bank Holder Name";

    //    MySqlDataAdapter adp2;

    //    try
    //    {
    //        d.con1.Open();

    //        adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='C01') as CLIENT_NAME,pay_employee_master.LOCATION, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER,pay_employee_master.PF_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE='" + ddlunitclient1.SelectedValue + "' and LOCATION='" + ddl_gv_statewise.SelectedValue + "'  ORDER BY pay_employee_master.EMP_CODE", d.con1);

    //        System.Data.DataTable dt = new System.Data.DataTable();
    //        adp2.Fill(dt);
    //        int j = 6;
    //        foreach (System.Data.DataRow row in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                ws.Cells[j, i + 1] = row[i].ToString();
    //            }
    //            j++;
    //        }

    //        xla.Visible = true;
    //    }
    //    catch (Exception ee)
    //    {
    //        Response.Write(ee.Message);
    //    }
    //    finally
    //    {
    //        d.con1.Close();
    //    }

    //}

    //protected void unit_export_to_excel()
    //{
    //    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
    //    Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
    //    Worksheet ws = (Worksheet)xla.ActiveSheet;
    //    xla.Columns.ColumnWidth = 16;

    //    //Change all cells' alignment to center
    //    Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[500, 300]);
    //    rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


    //    Range rng = ws.get_Range("G1:G1");
    //    rng.Interior.Color = XlRgbColor.rgbDarkGreen;

    //    Range formateRange2 = ws.get_Range("G1:G1");
    //    formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange2.Font.Size = 20;

    //    Range rng1 = ws.get_Range("G3:G3");
    //    rng1.Interior.Color = XlRgbColor.rgbGreen;

    //    Range formateRange1 = ws.get_Range("G3:G3");
    //    formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange1.Font.Size = 20;

    //    Range rng2 = ws.get_Range("A5:BM5");
    //    rng2.Interior.Color = XlRgbColor.rgbBlue;

    //    Range formateRange = ws.get_Range("A5:BM5");
    //    formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //    formateRange.Font.Size = 15;


    //    ws.Cells[1, 7] = Session["COMP_NAME"].ToString();
    //    ws.Cells[3, 7] = "Employee List";
    //    ws.Cells[5, 1] = "CLIENT_NAME";
    //    ws.Cells[5, 2] = "LOCATION";
    //    ws.Cells[5, 3] = "UNIT_NAME";
    //    ws.Cells[5, 4] = "EMP_CODE";
    //    ws.Cells[5, 5] = "EMP_NAME";
    //    ws.Cells[5, 6] = "FATHER NAME";
    //    ws.Cells[5, 7] = "JOINING DT";
    //    ws.Cells[5, 8] = "BIRTH DT";
    //    ws.Cells[5, 9] = "GRADE NAME";
    //    // ws.Cells[5, 7] = "DESIGNATION";

    //    //**AA**************************
    //    ws.Cells[5, 10] = "Reporting To";
    //    ws.Cells[5, 11] = "Present Address";
    //    ws.Cells[5, 12] = "Permanant Address";
    //    ws.Cells[5, 13] = "Mobile Number";
    //    //ws.Cells[5,17]  ="Joining Date"; 
    //    ws.Cells[5, 14] = "PAN Number";
    //    ws.Cells[5, 15] = "Adhar Card No.";

    //*********************************************************888888


    //    ws.Cells[5, 16] = "PF_NUMBER";

    //    ws.Cells[5, 17] = "ESIC_NUMBER";


    //    //****BB*****************************************************
    //    ws.Cells[5, 18] = "Bank Name";
    //    ws.Cells[5, 19] = "IFSC CODE";
    //    ws.Cells[5, 20] = "Bank Account Number";
    //    ws.Cells[5, 21] = "Nationality";
    //    ws.Cells[5, 22] = "Bank Holder Name";

    //    MySqlDataAdapter adp2;

    //    try
    //    {
    //        d.con1.Open();

    //      //  adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='C01') as CLIENT_NAME, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO, pay_employee_master.EMP_EMAIL_ID,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.PF_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE='" + ddlunitclient1.SelectedValue + "'  ORDER BY pay_employee_master.EMP_CODE", d.con1);
    //        adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='C01') as CLIENT_NAME,pay_employee_master.LOCATION,(Select UNIT_NAME from pay_unit_master where UNIT_CODE= pay_employee_master.UNIT_CODE and comp_code='C01') as UNIT_NAME ,pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER,pay_employee_master.PF_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE='" + ddlunitclient1.SelectedValue + "' and LOCATION='" + ddl_gv_statewise.SelectedValue + "' and unit_code='" + ddl_gv_branchwise.SelectedValue + "'  ORDER BY pay_employee_master.EMP_CODE", d.con1);
    //        System.Data.DataTable dt = new System.Data.DataTable();
    //        adp2.Fill(dt);
    //        int j = 6;
    //        foreach (System.Data.DataRow row in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                ws.Cells[j, i + 1] = row[i].ToString();
    //            }
    //            j++;
    //        }

    //        xla.Visible = true;
    //    }
    //    catch (Exception ee)
    //    {
    //        Response.Write(ee.Message);
    //    }
    //    finally
    //    {
    //        d.con1.Close();
    //    }

    //}

    //protected void txt_pfnumber_TextChanged(object sender, EventArgs e)
    //{   d.con.Open();
    //        try
    //        {
    //            MySqlCommand cmd = new MySqlCommand("SELECT EMP_CODE+'-'+EMP_NAME+'-'+PF_NUMBER FROM pay_employee_master WHERE PF_NUMBER='" + txt_pfnumber.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con);
    //            MySqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string oldpf = dr.GetValue(0).ToString();
    //               // txt_pfnumber.Text = "A";
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This PF Number already exist to Employee '+'" + oldpf + "'+' , try another number.');", true);

    //            }

    //            btnupdate.Visible = false;
    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally { d.con.Close(); }

    //    newpanel.Visible = true;
    //}

    protected void Upload_File(object sender, EventArgs e)
    {
        hidtab.Value = "7";
        if (txt_eecode.Text == "")
        {
            btnnew_Click();
        }
        if (txt_eecode.Text != "")
        {
            //if (btnupdate.Visible == false)
            //{
            //    btnnew_Click();
            //} 

            string cnt = "0";
            d.con.Open();
            try
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) FROM pay_images_master WHERE emp_code = '" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnt = dr.GetValue(0).ToString();
                }
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
            if (cnt == "0")
            {
                d.operation("INSERT INTO pay_images_master (EMP_CODE, emp_name, comp_code,MODIF_DATE) values ('" + txt_eecode.Text + "','" + txt_eename.Text + "', '" + Session["comp_code"].ToString() + "',curdate())");
            }
        }
        /////////---------------------------------------EMP_PHOTO-------------------------------------------------------------

        if (photo_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(photo_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(photo_upload.PostedFile.FileName);
                    photo_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_1" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_PHOTO = '" + txt_eecode.Text + "_1" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////////---------------------------------------PAN-------------------------------------------------------------

        if (adhar_pan_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(adhar_pan_upload.FileName);

            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(adhar_pan_upload.PostedFile.FileName);
                    adhar_pan_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_2" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_ADHAR_PAN = '" + txt_eecode.Text + "_2" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////////---------------------------------------EMP_BANK_STATEMENT ~ adhar card of empolyee-------------------------------------------------------------

        if (bank_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(bank_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(bank_upload.PostedFile.FileName);
                    bank_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_3" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_BANK_STATEMENT = '" + txt_eecode.Text + "_3" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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

        /////////---------------------------------------EMP_BIODATA-------------------------------------------------------------
        if (biodata_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(biodata_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(biodata_upload.PostedFile.FileName);
                    biodata_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_4" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_BIODATA = '" + txt_eecode.Text + "_4" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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


        /////////---------------------------------------EMP_PASSPORT-------------------------------------------------------------


        if (Passport_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Passport_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Passport_upload.PostedFile.FileName);
                    Passport_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_5" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_PASSPORT = '" + txt_eecode.Text + "_5" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('uploadPlease select only JPG and PNG Images !!!')", true);
            }

        }

        ///////-------------------------------------------EMP_DRIVING_LISCENCE----------------------------------------------------------------
        if (Driving_Liscence_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Driving_Liscence_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Driving_Liscence_upload.PostedFile.FileName);
                    Driving_Liscence_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_6" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_DRIVING_LISCENCE = '" + txt_eecode.Text + "_6" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////-----------------------------------------EMP_10TH_MARKSHEET--------------------------------------------------------
        if (Tenth_Marksheet_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Tenth_Marksheet_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Tenth_Marksheet_upload.PostedFile.FileName);
                    Tenth_Marksheet_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_7" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_10TH_MARKSHEET = '" + txt_eecode.Text + "_7" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////-------------------------------------------------EMP_12TH_MARKSHEET--------------------------------------------------------------------------
        if (Twelve_Marksheet_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Twelve_Marksheet_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Twelve_Marksheet_upload.PostedFile.FileName);
                    Twelve_Marksheet_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_8" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_12TH_MARKSHEET = '" + txt_eecode.Text + "_8" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
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
        /////---------------------------------------------EMP_DIPLOMA_CERTIFICATE-----------------------------------------------------
        if (Diploma_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Diploma_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Diploma_upload.PostedFile.FileName);
                    Diploma_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_9" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_DIPLOMA_CERTIFICATE = '" + txt_eecode.Text + "_9" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////-------------------------------------EMP_DEGREE_CERTIFICATE-----------------------------------------------------
        if (Degree_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Degree_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Degree_upload.PostedFile.FileName);
                    Degree_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_10" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_DEGREE_CERTIFICATE = '" + txt_eecode.Text + "_10" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////-------------------------------------EMP_POST_GRADUATION_CERTIFICATE-----------------------------------------------------

        if (Post_Graduation_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Post_Graduation_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Post_Graduation_upload.PostedFile.FileName);
                    Post_Graduation_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_11" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_POST_GRADUATION_CERTIFICATE = '" + txt_eecode.Text + "_11" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////------------------------------------------EMP_EDUCATION_CERTIFICATE------------------------------------------------
        if (Education_4_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Education_4_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Education_4_upload.PostedFile.FileName);
                    Education_4_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_12" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_EDUCATION_CERTIFICATE = '" + txt_eecode.Text + "_12" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////------------------------------------------EMP_Polic station varification doc--------------------------------
        if (Police_document.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Police_document.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Police_document.PostedFile.FileName);
                    Police_document.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_13" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), POLICE_VERIFICATION_DOC = '" + txt_eecode.Text + "_13" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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
        /////------------------------------------------EMP_Form No 2--------------------------------
        if (Formno_2.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Formno_2.FileName);
            // if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            //{
            if (txt_eecode.Text != "")
            {
                string fileName = Path.GetFileName(Formno_2.PostedFile.FileName);
                Formno_2.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_14" + fileExt, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), FormNo_2 = '" + txt_eecode.Text + "_14" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
            }
        }
        // else
        // {
        //   ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
        //}

        //////-----------------------------------------------------------Present_add_proof------------------------------

        if (Address_Proof.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Address_Proof.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(Address_Proof.PostedFile.FileName);
                    Address_Proof.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_15" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), present_add_proof = '" + txt_eecode.Text + "_15" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

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


        /////------------------------------------------EMP_Polic station varification doc--------------------------------
        if (Formno_11.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(Formno_11.FileName);
            // if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            // {
            if (txt_eecode.Text != "")
            {
                string fileName = Path.GetFileName(Formno_11.PostedFile.FileName);
                Formno_11.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_16" + fileExt, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), FormNo_11 = '" + txt_eecode.Text + "_16" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
            }
        }
        /////------------------------------------------EMP_noc_form--------------------------------
        if (noc_form.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(noc_form.FileName);
            // if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            // {
            if (txt_eecode.Text != "")
            {
                string fileName = Path.GetFileName(noc_form.PostedFile.FileName);
                noc_form.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_17" + fileExt, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), noc_form = '" + txt_eecode.Text + "_17" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
            }
        }
        /////------------------------------------------EMP_medical--------------------------------
        if (medical_form.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(medical_form.FileName);
            // if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            // {
            if (txt_eecode.Text != "")
            {
                string fileName = Path.GetFileName(medical_form.PostedFile.FileName);
                medical_form.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_18" + fileExt, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), medical_form = '" + txt_eecode.Text + "_18" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
            }
        }


        /////------------------------------------------EMP_signature--------------------------------
        if (emp_signature.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(emp_signature.FileName);
            // if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
            // {
            if (txt_eecode.Text != "")
            {
                string fileName = Path.GetFileName(emp_signature.PostedFile.FileName);
                emp_signature.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_19" + fileExt, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), emp_signature = '" + txt_eecode.Text + "_19" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }

        /////////---------------------------------------Original EMP_PHOTO-------------------------------------------------------------

        if (originalphoto.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(originalphoto.FileName);
            if (fileExt.ToLower() == ".jpg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(originalphoto.PostedFile.FileName);
                    originalphoto.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_20" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), original_photo = '" + txt_eecode.Text + "_20" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG Photo !!!')", true);
                newpanel.Visible = true;
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }

        /////////---------------------------------------Original Adharcard-------------------------------------------------------------

        if (original_adharcard_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(original_adharcard_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(original_adharcard_upload.PostedFile.FileName);
                    original_adharcard_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_21" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), original_adhar_card = '" + txt_eecode.Text + "_21" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }
        /////////---------------------------------------Original Ploicy_verification Document-------------------------------------------------------------

        if (original_police_document.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(original_police_document.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(original_police_document.PostedFile.FileName);
                    original_police_document.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_22" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), original_policy_document = '" + txt_eecode.Text + "_22" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }

        /////////---------------------------------------Original Address Proof-------------------------------------------------------------

        if (original_Address_Proof.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(original_Address_Proof.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(original_Address_Proof.PostedFile.FileName);
                    original_Address_Proof.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_23" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), original_address_proof = '" + txt_eecode.Text + "_23" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }
        /////////---------------------------------------Bank Passbook-------------------------------------------------------------

        if (bank_passbook.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(bank_passbook.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(bank_passbook.PostedFile.FileName);
                    bank_passbook.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_24" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), bank_passbook = '" + txt_eecode.Text + "_24" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }

        //////--------------komal--------------------



        /////------------------------------------------ITC document--------------------------------
        if (itc_upload_form.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(itc_upload_form.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(itc_upload_form.PostedFile.FileName);
                    itc_upload_form.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_25" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), itc_upload_form = '" + txt_eecode.Text + "_25" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);
        }

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images Uploaded Successfully!!!')", true);

        newpanel.Visible = true;
        if (reason_panel.Visible == true)
        {
            btn_add.Visible = false;
            btnupdate.Visible = true;
            btndelete.Visible = true;
            if (d.getsinglestring("select employee_type from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + txt_eecode.Text + "'").Contains("Permanent"))
            {
                btn_approve.Visible = true;
            }
        }
        else
        {
            btn_add.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btn_approve.Visible = false;
        }
        if (!txt_eecode.Text.Equals(""))
        {
            load_images(txt_eecode.Text);
        }
    }

    protected void image_click(object sender, ImageClickEventArgs e)
    {
        System.Web.UI.WebControls.Image button = sender as System.Web.UI.WebControls.Image;
        Response.Redirect(button.ImageUrl);

        //if (txt_eecode.Text != "")
        //{
        //    Session["FROM_INVOICE"] = txt_eecode.Text;
        //    Response.Redirect("Employee_Images.aspx");
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select atleast one Employee !!!')", true);
        //}

    }

    private void set_data()
    {
        if (txt_weight.Text == "") { txt_weight.Text = "0"; }
        if (txt_height.Text == "") { txt_height.Text = "0"; }

        if (txt_advance_payment.Text == "") { txt_advance_payment.Text = "0"; }


    }

    protected void ddl_grade_SelectedIndexChanged(object sender, EventArgs e)
    {

        // int result23 = 0;
        //Rahul start
        if (sender != null)
        {
            string grade = "";
            DataSet ds2 = new DataSet();
            //  if ((ddl_unit_client.SelectedValue == "Staff" && Session["COMP_CODE"].ToString().Equals("C01")) && (ddl_unit_client.SelectedValue != "IHMS" && Session["COMP_CODE"].ToString().Equals("C02")))

            if (ddl_unit_client.SelectedValue == "Staff" || ddl_unit_client.SelectedValue == "IHMS") { }

            else
            {
                grade = d.getsinglestring("select designation from pay_billing_master where billing_unit_code= '" + ddl_unitcode.SelectedValue + "' and designation='" + ddl_grade.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                if (grade.Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please create branch Policy......');", true);
                    newpanel.Visible = true;
                    ddl_grade.SelectedIndex = 0;
                    return;
                }
            }
        }
        // rahul end
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item2 = new MySqlCommand("SELECT UNIT_CITY FROM pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  UNIT_CODE = '" + ddl_unitcode.SelectedValue + "' ", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item2 = cmd_item2.ExecuteReader();
            while (dr_item2.Read())
            {
                ddl_location_city.Text = (dr_item2[0].ToString());
            }
            dr_item2.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        //  ddl_location_city.Items.Insert(0, new ListItem("Select"));




        d1.con1.Open();
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select emp_code, emp_name, grade_code from pay_employee_master where grade_code in (select reporting_to from pay_grade_master where grade_code = '" + ddl_grade.SelectedValue.ToString() + "')  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
            adp.Fill(ds);
            ddl_reporting_to.DataSource = ds.Tables[0];
            ddl_reporting_to.DataBind();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
            newpanel.Visible = true;

            MySqlCommand cmd = new MySqlCommand("Select EMP_CODE from pay_employee_master where comp_code='" + Session["comp_code"] + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string abc = dr[0].ToString();
                string xyz = Convert.ToString(txt_eecode.Text);
                if (abc == xyz)
                {
                    btndelete.Visible = true;
                    btnupdate.Visible = true;
                    btn_add.Visible = false;


                    break;

                }
                else
                {

                    btndelete.Visible = false;
                    btnupdate.Visible = false;
                    btn_add.Visible = true;
                }
            }

            dr.Close();
            d.con.Close();

        }

        d.con.Open();
        txt_attendanceid.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(hours) from pay_designation_count where unit_code ='" + ddl_unitcode.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and designation = (Select grade_desc from pay_grade_master where grade_code = '" + ddl_grade.SelectedValue + "' and COMP_CODE = '" + Session["COMP_CODE"].ToString() + "')", d.con);
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txt_attendanceid.DataSource = dt_item;
                txt_attendanceid.DataValueField = dt_item.Columns[0].ToString();
                txt_attendanceid.DataTextField = dt_item.Columns[0].ToString();
                txt_attendanceid.DataBind();

            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //txt_attendanceid.Items.Insert(0, new ListItem("Select","0"));

        select_designation.SelectedItem.Text = ddl_grade.SelectedItem.Text;
    }



    private void add_values()
    {
        string entrydatestmp = Session["system_curr_date"].ToString();
        //add to user login
        d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,ROLE,flag,create_user, create_date, password_changed_date,first_login,comp_code) VALUES('" + txt_eecode.Text + "','" + txt_eename.Text + "','" + GetSha256FromString(txt_birthdate.Text) + "','" + DropDownList1.SelectedItem.Text + "','A','" + Session["USERID"].ToString() + "',now(),now(),'0','" + Session["comp_code"].ToString() + "')");
        //Insert Leave Balance

        if (entrydatestmp.Substring(3, 2) == "01")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',6,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "02")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',5,1,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "03")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',4,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "04")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',3,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "05")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',2,1,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "06")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',1,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "07")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',6,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "08")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',5,1,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "09")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',4,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "10")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',3,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "11")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',2,2,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
        else if (entrydatestmp.Substring(3, 2) == "12")
        {
            d.operation("insert into pay_leave_emp_balance (comp_code,unit_code,emp_code,CL,PL,HD,maternity,paternity,last_update_date,create_user,create_date) values ('" + Session["comp_code"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + txt_eecode.Text + "',1,1,0,0,0,now(),'" + Session["USERID"].ToString() + "',now())");
        }
    }
    protected void ddl_reporting_to_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    }
    protected void get_city_list_shipping(object sender, EventArgs e)
    {

        txt_permanantcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_permstate.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            txt_permanantcity.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }

        //  ddl_clientwisestate.SelectedValue = ddl_location.Text;
    }
    protected void get_city_list(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
        txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_presentcity.Items.Add(dr_item1[0].ToString());
                txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            txt_presentcity.Items.Insert(0, new ListItem("Select", "Select"));
            txt_permanantcity.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }

    }
    protected void get_city_list_per(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
        // txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                //   txt_presentcity.Items.Add(dr_item1[0].ToString());
                txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            //   txt_presentcity.Items.Insert(0, new ListItem("Select", "Select"));
            txt_permanantcity.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }

    }

    private void send_email(string emailhtmlfile, string toaddress)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

            mail.From = new MailAddress("info@celtsoft.com");
            mail.To.Add(toaddress);
            // mail.CC.Add(tocc);
            mail.Subject = "User Details";
            d.con1.Open();
            try
            {
                MySqlCommand cmdmax1 = new MySqlCommand("SELECT EMP_NAME,EMP_CODE as 'Login ID' ,Date_Format( BIRTH_DATE,'%d/%m/%Y') as 'Password'  FROM pay_employee_master WHERE EMP_CODE = '" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                MySqlDataReader drmax = cmdmax1.ExecuteReader();
                while (drmax.Read())
                {
                    body = body.Replace("{Name}", drmax.GetValue(0).ToString());
                    body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
                    body = body.Replace("{Password}", drmax.GetValue(2).ToString());
                    //mail.Body = tosubject;
                }
                drmax.Close();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            mail.Body = body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }

        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false); }

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

    protected void ddl_location_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ddl_location_city.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_location.Text + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location_city.Text = (dr_item1[0].ToString());
            }

            btnupdate.Visible = false;
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            // ddl_location_city.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }
    }

    protected void get_client_details(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
        txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_presentcity.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            newpanel.Visible = true;
        }

    }


    private string generateihmscode()
    {
        //IHMS/CLient Name/Gradecode/statecodeautoincrementnumber
        //examle IHMS/KM/HK/MH18

        string ihmscode = "IH&MS";
        //Client code
        ihmscode = ihmscode + "/" + getsingledata("select client_code from pay_unit_master where unit_code = '" + ddl_unitcode.SelectedValue + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
        //grade code
        ihmscode = ihmscode + "/" + ddl_grade.SelectedValue;
        //state code
        ihmscode = ihmscode + "/" + getsingledata("select state_code from pay_state_master where state_name = '" + ddl_location.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");

        return ihmscode + int.Parse(txt_eecode.Text.Substring(1, txt_eecode.Text.Length - 1)); ;

    }

    private string getsingledata(string sql)
    {
        MySqlCommand cmd = new MySqlCommand(sql, d.con);
        try
        {
            d.con.Open();
            return (string)cmd.ExecuteScalar();
            d.con.Close();
        }
        catch
        { }
        finally
        {
            d.con.Close();
            d.con.Dispose();
            cmd.Dispose();
        }
        return "";
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

    private Boolean chkcount()
    {
        if (ddl_employee_type.SelectedValue == "Reliever")
        {
            string Perment_emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND pay_employee_master.Employee_type = 'Permanent' and attendance_id = '" + txt_attendanceid.SelectedValue + "'  and GRADE_CODE ='" + ddl_grade.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
            string reliver_emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND pay_employee_master.Employee_type = 'Reliever' and attendance_id = '" + txt_attendanceid.SelectedValue + "'  and GRADE_CODE ='" + ddl_grade.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
            if (Perment_emp_count == "") { Perment_emp_count = "0"; }
            int count = Convert.ToInt32(Perment_emp_count) * 3;

            if (reliver_emp_count == "") { reliver_emp_count = "0"; }
            if (Convert.ToInt32(reliver_emp_count) >= (count))
            {
                return true;
            }

        }
        else
        {
            if (ddl_grade.SelectedItem.ToString() != "Select")
            {
                string unit_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' and HOURS = '" + txt_attendanceid.SelectedValue + "'  AND DESIGNATION = '" + ddl_grade.SelectedItem.ToString() + "' and branch_status='0'");
                if (unit_emp_count == "")
                {
                    return false;
                }
                
                // this query comment because of count issue 23-05-2020 komal 
 
                //string emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND pay_employee_master.Employee_type != 'Reliever'    and GRADE_CODE ='" + ddl_grade.SelectedValue + "' and left_date >= str_to_date('"+txt_joiningdate.Text+"','%d/%m/%Y')");

                string emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND pay_employee_master.Employee_type != 'Reliever'    and GRADE_CODE ='" + ddl_grade.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
                
                try
                {
                    // if (unit_emp_count != "" && emp_count != "")
                    // {
                    MySqlCommand cmd = new MySqlCommand("select pay_employee_master.Employee_type from pay_employee_master  where CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "' and emp_code='" + txt_eecode.Text + "'  ", d.con);
                    d.con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        emp_count = emp_count == "" ? "0" : emp_count;
                        string emp_type = dr.GetValue(0).ToString();
                        if (emp_type != ddl_employee_type.SelectedValue)
                        {

                            if (int.Parse(unit_emp_count) < int.Parse(emp_count) + 1)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {

                        emp_count = emp_count == "" ? "0" : emp_count;

                        if (int.Parse(unit_emp_count) < int.Parse(emp_count) + 1)
                        {
                            return true;
                        }

                    }
                    dr.Close();
                    d.con.Close();
                    //  }
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }
            }
        }
        return false;
        //  }
    }
    protected void ddlsatewises_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchGridView.Visible = true;
        btn_add.Visible = true;
        //vikas 08/05/2019
        // button_false();
        try
        {
            left_header();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            //vikas 08-01-19
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',(select GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code AND pay_grade_master.GRADE_CODE = pay_employee_master.GRADE_CODE) as 'GRADE_DESC',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO," + header_date + ", date_format(pay_employee_master.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',client_wise_state FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code 		INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_unit_master.state_name='" + ddl_gv_statewise.SelectedValue + "' AND pay_client_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  AND pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master.unit_code in (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddlunitclient1.SelectedValue + "' AND pay_client_state_role_grade.state_name='" + ddl_gv_statewise.SelectedValue + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "'))  " + check_left + " AND pay_unit_master.branch_status = 0 ", d.con1);
            d.con1.Open();
            try
            {
                adp.Fill(ds);
                Panel5.Visible = true;
                SearchGridView.DataSource = ds.Tables[0];
                SearchGridView.DataBind();
                // text_Clear();
                Panel5.Visible = true;
                SearchGridView.Visible = true;
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_branchwise.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddlunitclient1.SelectedValue + "' and state_name = '" + ddl_gv_statewise.SelectedValue + "' AND  UNIT_CODE in(SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddlunitclient1.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_gv_statewise.SelectedValue + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) and branch_status = 0  ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_gv_branchwise.DataSource = dt_item;
                ddl_gv_branchwise.DataTextField = dt_item.Columns[0].ToString();
                ddl_gv_branchwise.DataValueField = dt_item.Columns[1].ToString();
                ddl_gv_branchwise.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_gv_branchwise.Items.Insert(0, new ListItem("Select"));
        }

        String emp_listcount = "2";
        employee_count(emp_listcount);
        btn_add.Visible = true;
        if (reason_panel.Visible == true)
        {
            newpanel.Visible = true;
            btnupdate.Visible = true;
        }
        else
        {

            newpanel.Visible = false;
        }
        //MySqlCommand cmd2 = new MySqlCommand("SELECT pay_employee_master.UNIT_CODE,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_employee_master inner join pay_unit_master on pay_employee_master.UNIT_CODE=pay_unit_master.UNIT_CODE and pay_employee_master.COMP_CODE=pay_unit_master.COMP_CODE WHERE pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  and pay_unit_master.state_name='" + ddl_gv_statewise.SelectedValue + "'", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
        //    DataSet ds1 = new DataSet();
        //    sda1.Fill(ds1);
        //    ddl_gv_branchwise.DataSource = ds1.Tables[0];
        //    ddl_gv_branchwise.DataValueField = "UNIT_CODE";
        //    ddl_gv_branchwise.DataTextField = "CUNIT";
        //    ddl_gv_branchwise.DataBind();
        //    ddl_gv_branchwise.Items.Insert(0, new ListItem("Select"));
        //}
        //catch (Exception ex) { throw ex; }
        //finally { d.con.Close(); }

    }
    protected void ddlunitclient1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String emp_listcount = "1";
        employee_count(emp_listcount);
        left_employee_count();
        uniformhold_employee_count();
        vacant();
        SearchGridView.Visible = true;
        //vikas add 08/05/2019
        //button_false();
        //   btn_add.Visible = true;

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            //vikas 08-01-19
            left_header();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',(select GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code AND pay_grade_master.GRADE_CODE = pay_employee_master.GRADE_CODE) as 'GRADE_DESC',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO ," + header_date + ", DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',client_wise_state FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master.unit_code IN (SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code='" + ddlunitclient1.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) " + check_left + " AND pay_unit_master.branch_status = 0 and client_active_close='0'", d.con1);
            d.con1.Open();
            adp.Fill(ds);
            Panel5.Visible = true;
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            Panel5.Visible = true;
            SearchGridView.Visible = true;
            d.con1.Close();
            // text_Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_statewise.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "'  AND LOCATION in(SELECT DISTINCT  (pay_client_state_role_grade.state_name)  FROM  pay_client_state_role_grade  INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code  WHERE  pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code='" + ddlunitclient1.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) ORDER BY EMP_CURRENT_STATE", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_gv_statewise.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        ddl_gv_statewise.Items.Insert(0, new ListItem("Select"));

        SearchGridView.Visible = true;


        if (reason_panel.Visible == true)
        {
            newpanel.Visible = true;
            btnupdate.Visible = true;
        }
        else
        {
            newpanel.Visible = false;
            btn_add.Visible = true;
        }

    }

    protected void left_header()
    {
        if (ViewState["left"].ToString() == "1")
        {
            check_left = "and (pay_employee_master.LEFT_REASON !='' and (pay_employee_master.LEFT_REASON !='' || pay_employee_master.LEFT_REASON is not null))";
            header_date = "DATE_FORMAT(pay_employee_master.LEFT_DATE, '%d/%m/%Y') AS 'BIRTH_DATE'";
            SearchGridView.Columns[9].HeaderText = "LEFT DATE";
            SearchGridView.Columns[8].HeaderText = "MOBILE NO.";
        }
        else
        {
            check_left = "and (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON is null)";
            header_date = "date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE'";
            SearchGridView.Columns[9].HeaderText = "BIRTH DATE.";
        }
    }
    protected void ddlbeabchwise1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchGridView.Visible = true;
        btn_add.Visible = true;
        // button_false();//vikas 08/05/2019
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            left_header();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.client_wise_state,pay_employee_master.ihmscode,(SELECT CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END) AS 'emp_name',(select GRADE_DESC from pay_grade_master where pay_grade_master.comp_code = pay_employee_master.comp_code AND pay_grade_master.GRADE_CODE = pay_employee_master.GRADE_CODE) as 'GRADE_DESC',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO, " + header_date + " ,date_format(pay_employee_master.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE'  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_unit_master.UNIT_CODE='" + ddl_gv_branchwise.SelectedValue + "' AND pay_unit_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  and pay_unit_master.state_name='" + ddl_gv_statewise.SelectedValue + "' AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'  " + check_left + " ", d.con1);
            d.con1.Open();
            adp.Fill(ds);
            Panel5.Visible = true;
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            // text_Clear();
            Panel5.Visible = true;
            SearchGridView.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
        String emp_listcount = "3";
        employee_count(emp_listcount);
        if (reason_panel.Visible == true)
        {
            newpanel.Visible = true;
            btnupdate.Visible = true;
        }
        else
        {
            newpanel.Visible = false;
        }
        if (ddl_gv_branchwise.SelectedValue.Equals("Select"))
        {
            ddlsatewises_SelectedIndexChanged(null, null);
        }
    }

    protected void btn_copyadd_Click(object sender, EventArgs e)
    {
        //ddl_permstate.Items.Clear();
        //txt_permanantcity.Items.Clear();
        txt_permanantaddress.Text = txt_presentaddress.Text;
        ddl_permstate.SelectedValue = ddl_state.SelectedValue;
        get_city_list_per(null, null);
        string s = txt_presentcity.SelectedValue;

        txt_permanantcity.SelectedValue = s;
        txt_permanantpincode.Text = txt_presentpincode.Text;

        txtref2mob.Text = txt_mobilenumber.Text;
        txt_premanent_mob1.Text = pre_mobileno_1.Text;
        txt_premanent_mob2.Text = pre_mobileno_2.Text;
        newpanel.Visible = true;
        if (ViewState["id"].ToString() == "1")
        {
            if (ViewState["permanent"].ToString() == "1") { btn_approve.Visible = true; }
            btn_add.Visible = false;
            //btn_reject.Visible = false;
            btnupdate.Visible = true;
            btndelete.Visible = true;
        }
        else
        {
            btn_add.Visible = true;
            //  btn_reject.Visible = false;
            btnupdate.Visible = false;
            btndelete.Visible = false;

        }

    }

    protected void lnk_zone_add_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        foreach (GridViewRow row in gv_product_details.Rows)
        {
            string uf = row.Cells[2].Text;

            if (ddl_product_type.SelectedItem.Text.Equals(uf))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Same Type Already Inserted!!')", true);
                return;
            }
        }
        btnupdate.Visible = true;//new add 
        gv_product_details.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("document_type");
        dt.Columns.Add("admin_no_of_set");
        dt.Columns.Add("size");
        dt.Columns.Add("start_date");
        dt.Columns.Add("end_date");
        dt.Columns.Add("remaining_no_set");


        int rownum = 0;
        for (rownum = 0; rownum < gv_product_details.Rows.Count; rownum++)
        {
            if (gv_product_details.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["document_type"] = gv_product_details.Rows[rownum].Cells[2].Text;
                dr["admin_no_of_set"] = gv_product_details.Rows[rownum].Cells[3].Text;
                dr["size"] = gv_product_details.Rows[rownum].Cells[4].Text;
                dr["start_date"] = gv_product_details.Rows[rownum].Cells[5].Text;
                dr["end_date"] = gv_product_details.Rows[rownum].Cells[6].Text;
                dr["remaining_no_set"] = gv_product_details.Rows[rownum].Cells[7].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();

        dr["document_type"] = ddl_product_type.SelectedItem.Text;
        dr["admin_no_of_set"] = ddl_uniformset.SelectedItem.Text;
        dr["remaining_no_set"] = ddl_uniformset.SelectedValue;
        //if (ddl_product_type.SelectedValue == "1")
        //{
        //    dr["remaining_no_set"] = ddl_uniformset.SelectedValue;
        //}
        //else { dr["remaining_no_set"] = "0"; }

        if (ddl_product_type.SelectedItem.Text == "ID_Card" || ddl_product_type.SelectedItem.Text == "Torch" || ddl_product_type.SelectedItem.Text == "Baton" || ddl_product_type.SelectedItem.Text == "Belt")
        {
            dr["size"] = "";
        }
        else
        {
            dr["size"] = uniform_size.Text;
        }
        dr["start_date"] = uniform_issue_date.Text;
        dr["end_date"] = uniform_expiry_date.Text;

        dt.Rows.Add(dr);
        gv_product_details.DataSource = dt;
        gv_product_details.DataBind();

        ViewState["headtable"] = dt;

        newpanel.Visible = true;

        ddl_product_type.SelectedValue = "0";
        ddl_uniformset.SelectedValue = "0";
        uniform_size.SelectedValue = "Select";
        uniform_expiry_date.Text = "";
        uniform_issue_date.Text = "";
        lbl_qty.Visible = false;
        txt_quantity1.Visible = false;

    }

    protected void gv_product_details_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void lnk_remove_product_Click(object sender, EventArgs e)
    {
        btnupdate.Visible = true;

        btndelete.Visible = true;
        btn_add.Visible = true;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 0)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_product_details.DataSource = dt;
            gv_product_details.DataBind();
        }

        newpanel.Visible = true;
    }

    protected void select_designation_SelectedIndexedChanged(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (select_designation.SelectedValue == "1")
        {
            ddl_product_type.Items.Clear();
            ddl_product_type.Items.Insert(0, new ListItem("Select", "0"));
            ddl_product_type.Items.Insert(1, new ListItem("Uniform", "1"));
            ddl_product_type.Items.Insert(2, new ListItem("Shoes", "2"));
            ddl_product_type.Items.Insert(3, new ListItem("Sweater", "3"));
            ddl_product_type.Items.Insert(4, new ListItem("ID_Card", "4"));
            ddl_product_type.Items.Insert(5, new ListItem("Raincoat", "5"));
            ddl_product_type.Items.Insert(6, new ListItem("Pantry_Jacket", "10"));
            ddl_product_type.Items.Insert(7, new ListItem("Apron", "11"));
        }
        if (select_designation.SelectedValue == "2")
        {
            ddl_product_type.Items.Clear();
            ddl_product_type.Items.Insert(0, new ListItem("Select", "0"));
            ddl_product_type.Items.Insert(1, new ListItem("Uniform", "1"));
            ddl_product_type.Items.Insert(2, new ListItem("Shoes", "2"));
            ddl_product_type.Items.Insert(3, new ListItem("Sweater", "3"));
            ddl_product_type.Items.Insert(4, new ListItem("ID_Card", "4"));
            ddl_product_type.Items.Insert(5, new ListItem("Raincoat", "5"));
            ddl_product_type.Items.Insert(6, new ListItem("Torch", "6"));
            ddl_product_type.Items.Insert(7, new ListItem("Baton", "7"));
            ddl_product_type.Items.Insert(8, new ListItem("Belt", "8"));
            ddl_product_type.Items.Insert(9, new ListItem("Pantry_Jacket", "10"));
            ddl_product_type.Items.Insert(10, new ListItem("Apron", "11"));
        }
        if (select_designation.SelectedValue == "3")
        {
            ddl_product_type.Items.Clear();
            ddl_product_type.Items.Insert(0, new ListItem("Select", "0"));
            ddl_product_type.Items.Insert(1, new ListItem("Uniform", "1"));
            ddl_product_type.Items.Insert(2, new ListItem("Shoes", "2"));
            ddl_product_type.Items.Insert(3, new ListItem("Sweater", "3"));
            ddl_product_type.Items.Insert(4, new ListItem("ID_Card", "4"));
            ddl_product_type.Items.Insert(5, new ListItem("Raincoat", "5"));
            ddl_product_type.Items.Insert(6, new ListItem("Apron", "11"));
        }
        newpanel.Visible = true;

    }
    protected void ddl_product_type_SelectedIndexedChanged(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (ddl_product_type.SelectedValue == "1")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
            ddl_uniformset.Items.Insert(2, new ListItem("2", "2"));
        }
        if (ddl_product_type.SelectedValue == "2")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
        }
        if (ddl_product_type.SelectedValue == "3")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
        }
        if (ddl_product_type.SelectedValue == "4")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
        }
        if (ddl_product_type.SelectedValue == "5")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
        }
        if (ddl_product_type.SelectedValue == "6")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
            ddl_uniformset.Items.Insert(2, new ListItem("2", "2"));
            ddl_uniformset.Items.Insert(3, new ListItem("3", "3"));
            ddl_uniformset.Items.Insert(4, new ListItem("4", "4"));
            ddl_uniformset.Items.Insert(5, new ListItem("5", "5"));
        }
        if (ddl_product_type.SelectedValue == "7")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
            ddl_uniformset.Items.Insert(2, new ListItem("2", "2"));
            ddl_uniformset.Items.Insert(3, new ListItem("3", "3"));
            ddl_uniformset.Items.Insert(4, new ListItem("4", "4"));
            ddl_uniformset.Items.Insert(5, new ListItem("5", "5"));
        }
        if (ddl_product_type.SelectedValue == "8")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
            ddl_uniformset.Items.Insert(2, new ListItem("2", "2"));
            ddl_uniformset.Items.Insert(3, new ListItem("3", "3"));
            ddl_uniformset.Items.Insert(4, new ListItem("4", "4"));
            ddl_uniformset.Items.Insert(5, new ListItem("5", "5"));
        }
        if (ddl_product_type.SelectedValue == "9")
        {
            ddl_uniformset.Items.Clear();
            ddl_uniformset.Items.Insert(0, new ListItem("Select", "0"));
            ddl_uniformset.Items.Insert(1, new ListItem("1", "1"));
            ddl_uniformset.Items.Insert(2, new ListItem("2", "2"));
            ddl_uniformset.Items.Insert(3, new ListItem("3", "3"));
            ddl_uniformset.Items.Insert(4, new ListItem("4", "4"));
            ddl_uniformset.Items.Insert(5, new ListItem("5", "5"));
        }
        //vikas add 03/04/2019
        // lbl_qty.Text = "";
        txt_quantity1.Visible = false;
        uniform_size.SelectedValue = "Select";
        size();
        newpanel.Visible = true;
    }

    protected void txt_workinghours_count(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("select HOURS from pay_designation_count where unit_code='" + ddl_unitcode.SelectedValue + "' and client_code='" + ddl_unit_client.SelectedValue + "' and UNIT_CODE='" + ddl_unitcode.SelectedValue + "' and DESIGNATION='" + ddl_grade.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string a = dr.GetValue(0).ToString();

                if (txt_attendanceid.SelectedValue != a)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Hours Not Match With Branch Hours');", true);
                    return;
                }

                newpanel.Visible = true;
            }
            newpanel.Visible = true;
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }


    }

    //protected void ddl_employee_type_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_employee_type.SelectedValue == "Staff")
    //    {
    //        ddl_grade.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select grade_code, grade_desc from pay_grade_master where comp_code ='" + Session["comp_code"].ToString() + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {
    //                ddl_grade.DataSource = dt_item;
    //                ddl_grade.DataTextField = dt_item.Columns[1].ToString();
    //                ddl_grade.DataValueField = dt_item.Columns[0].ToString();
    //                ddl_grade.DataBind();

    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //            ddl_grade.Items.Insert(0, "Select");
    //            ddl_unit_client.SelectedIndex = 0;
    //            ddl_clientwisestate.SelectedIndex = 0;
    //            ddl_unitcode.SelectedIndex = 0;
    //            ddl_unit_client.Enabled = false;
    //            ddl_clientwisestate.Enabled = false;
    //            ddl_unitcode.Enabled = false;
    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }
    //    }
    //    else
    //    {
    //        ddl_unit_client.Enabled = true;
    //        ddl_clientwisestate.Enabled = true;
    //        ddl_unitcode.Enabled = true;
    //    }
    //}

    private void downloadFile(string filepath)
    {
        Response.Redirect(Server.MapPath(filepath));
        Response.End();

    }

    protected void btn_add_employeeIcard_Click(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            if (d.getsinglestring("select emp_code from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and EMP_CODE ='" + txt_eecode.Text + "' AND legal_flag = 2").Equals("")) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Can not Approve by Legal You can not generate ICARD');", true); return; };
            MySqlCommand cmd4 = new MySqlCommand("select original_photo from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader dr4 = cmd4.ExecuteReader();
            if (dr4.Read())
            {
                string s = dr4.GetValue(0).ToString();
                //    string s1 = dr4.GetValue(1).ToString();
                if ((s == ""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Submit The Original Photo of Employee');", true);
                    newpanel.Visible = true;
                    dr4.Close();
                    d.con1.Close();
                    return;
                }
                else
                {
                    dr4.Close();
                    d.con1.Close();
                }

            }
            dr4.Close();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

        d.con1.Open();
        try
        {

            string downloadname = "I_Card";
            string query = null;
            crystalReport.Load(Server.MapPath("~/I_Card_new.rpt"));
            // query = " SELECT pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE, GRADE_DESC,EMP_MOBILE_NO,LOCATION AS ADDRESS2,EMP_CURRENT_ADDRESS AS ADDRESS1,EMP_CURRENT_CITY AS CITY,EMP_CURRENT_STATE AS STATE,pay_images_master.original_photo as 'EMP_PHOTO',pay_images_master.emp_signature,department_type as 'DEPT_NAME',EMP_BLOOD_GROUP AS 'GENDER' from  pay_employee_master inner join pay_grade_master on  pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code=pay_grade_master.comp_code 	inner join pay_images_master on  pay_employee_master.emp_code=pay_images_master.emp_code and pay_employee_master.comp_code=pay_images_master.comp_code where pay_employee_master.emp_code='" + txt_eecode.Text + "' and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'";
            //rahul
            query = " SELECT pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,    case GRADE_DESC when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else GRADE_DESC end as GRADE_DESC,EMP_MOBILE_NO,LOCATION AS ADDRESS2,EMP_CURRENT_ADDRESS AS ADDRESS1,EMP_CURRENT_CITY AS CITY,EMP_CURRENT_STATE AS STATE,pay_images_master.original_photo as 'EMP_PHOTO',pay_images_master.emp_signature,department_type as 'DEPT_NAME',(select case when EMP_BLOOD_GROUP is not null then EMP_BLOOD_GROUP else '' end ) AS 'GENDER', ( select client_name from pay_client_master where client_code=pay_employee_master.client_code)as 'BANK_CODE' from  pay_employee_master inner join pay_grade_master on  pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code=pay_grade_master.comp_code 	inner join pay_images_master on  pay_employee_master.emp_code=pay_images_master.emp_code and pay_employee_master.comp_code=pay_images_master.comp_code where pay_employee_master.emp_code='" + txt_eecode.Text + "' and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'";
            ReportLoad(query, downloadname);
            Response.End();
            text_Clear();
            d.con1.Close();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();
            newpanel.Visible = true;
            if (reason_panel.Visible == true)
            {
                btnupdate.Visible = true;
                btndelete.Visible = true;
                btn_add.Visible = false;
            }
            else
            {
                btnupdate.Visible = false;
                btndelete.Visible = false;
                btn_add.Visible = true;
            }
        }
    }

    private void ReportLoad(string query, string downloadfilename)
    {

        try
        {

            string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            try
            {
                sda = cmd.ExecuteReader();
                dt.Load(sda);
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }

            MySqlCommand cmd_item1 = new MySqlCommand("SELECT original_photo from pay_images_master where comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + txt_eecode.Text + "' ", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                if (dr_item1.Read())
                {
                    string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/."), "EMP_Images\\" + dr_item1.GetValue(0).ToString() + "");
                    crystalReport.DataDefinition.FormulaFields["emp_photo"].Text = "'" + path + "'";
                    //PictureObject TAddress1 = (PictureObject)crystalReport.ReportDefinition.Sections[0].ReportObjects["Picture1"];
                    //TAddress1.Width = 850;
                    //TAddress1.Height = 400;
                    //TAddress1.Left = 4000;
                }
                else
                {
                    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\placeholder.png");
                    crystalReport.DataDefinition.FormulaFields["emp_photo"].Text = "'" + path + "'";
                }
                dr_item1.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }

            crystalReport.SetDataSource(dt);

            crystalReport.Refresh();

            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadname);

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
        finally
        {
            d.con.Close();
            d1.con.Close();
            d.con1.Close();
        }
    }
    protected void btn_rem_documents_Click(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            string where = "";
            if (ddlunitclient1.SelectedValue != "Select")
            {
                where = " and pay_client_master.client_code = '" + ddlunitclient1.SelectedValue + "'";
                if (ddl_gv_statewise.SelectedValue != "Select")
                {
                    where = where + " and pay_unit_master.state_name = '" + ddl_gv_statewise.SelectedValue + "'";
                    if (ddl_gv_branchwise.SelectedValue != "Select")
                    {
                        where = where + " and pay_unit_master.unit_code = '" + ddl_gv_branchwise.SelectedValue + "'";
                    }
                }
            }


            string sql = "SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name, pay_employee_master.EMP_NAME, CASE WHEN EMP_ADHAR_PAN IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_ADHAR_PAN) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_ADHAR_PAN', CASE WHEN EMP_BIODATA IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_BIODATA) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_BIODATA', CASE WHEN EMP_PASSPORT IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_PASSPORT) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_PASSPORT', CASE WHEN EMP_DRIVING_LISCENCE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DRIVING_LISCENCE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DRIVING_LISCENCE', CASE WHEN EMP_10TH_MARKSHEET IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_10TH_MARKSHEET) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_10TH_MARKSHEET', CASE WHEN EMP_12TH_MARKSHEET IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_12TH_MARKSHEET) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_12TH_MARKSHEET', CASE WHEN EMP_DIPLOMA_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DIPLOMA_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DIPLOMA_CERTIFICATE', CASE WHEN EMP_DEGREE_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DEGREE_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DEGREE_CERTIFICATE', CASE WHEN EMP_POST_GRADUATION_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_POST_GRADUATION_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_POST_GRADUATION_CERTIFICATE', CASE WHEN EMP_EDUCATION_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_EDUCATION_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_EDUCATION_CERTIFICATE', CASE WHEN FORMNO_2 IS NULL THEN 'No' ELSE CASE WHEN TRIM(FORMNO_2) = '' THEN 'No' ELSE 'Yes' END END AS 'FORMNO_2', CASE WHEN FORMNO_11 IS NULL THEN 'No' ELSE CASE WHEN TRIM(FORMNO_11) = '' THEN 'No' ELSE 'Yes' END END AS 'FORMNO_11', CASE WHEN noc_form IS NULL THEN 'No' ELSE CASE WHEN TRIM(noc_form) = '' THEN 'No' ELSE 'Yes' END END AS 'noc_form', CASE WHEN medical_form IS NULL THEN 'No' ELSE CASE WHEN TRIM(medical_form) = '' THEN 'No' ELSE 'Yes' END END AS 'medical_form', CASE WHEN emp_signature IS NULL THEN 'No' ELSE CASE WHEN TRIM(emp_signature) = '' THEN 'No' ELSE 'Yes' END END AS 'emp_signature', CASE WHEN original_photo IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_photo) = '' THEN 'No' ELSE 'Yes' END END AS 'original_photo', CASE WHEN original_adhar_card IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_adhar_card) = '' THEN 'No' ELSE 'Yes' END END AS 'original_adhar_card', CASE WHEN original_policy_document IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_policy_document) = '' THEN 'No' ELSE 'Yes' END END AS 'original_policy_document', CASE WHEN original_address_proof IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_address_proof) = '' THEN 'No' ELSE 'Yes' END END AS 'original_address_proof', CASE WHEN bank_passbook IS NULL THEN 'No' ELSE CASE WHEN TRIM(bank_passbook) = '' THEN 'No' ELSE 'Yes' END END AS 'bank_passbook' FROM pay_images_master inner join pay_employee_master on pay_employee_master.emp_code = pay_images_master.emp_code and pay_employee_master.comp_code = pay_images_master.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and pay_employee_master.comp_code = pay_client_master.comp_code where  client_active_close=0  and pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'" + where;

            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=Remaining_Documents.xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null);
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
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:
                    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th>CLIENT NAME</th><th>BRANCH STATE</th><th>BRANCH</th><th>EMPLOYEE NAME</th><th>ADHAR</th><th>RESUME</th><th>PASSPORT</th><th>DRIVING LICENSE</th><th>10th MARKSHEET</th><th>12th MARKSHEET</th><th>DIPLOMA</th><th>DEGREE</th><th>POST GRADUATION</th><th>EDUCATION</th><th>FORM 2</th><th>FORM 11</th><th>NOC FORM</th><th>MEDICAL REPORT</th><th>EMP SIGNATURE</th><th>PHOTO</th><th>ADHAR</th><th>POLICY</th><th>ADDRESS PROOF</th><th>BANK PASSBOOK</th></tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["STATE_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["UNIT_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_ADHAR_PAN"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_BIODATA"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_PASSPORT"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_DRIVING_LISCENCE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_10TH_MARKSHEET"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_12TH_MARKSHEET"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_DIPLOMA_CERTIFICATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_DEGREE_CERTIFICATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_POST_GRADUATION_CERTIFICATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_EDUCATION_CERTIFICATE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FORMNO_2"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["FORMNO_11"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["noc_form"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["medical_form"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_signature"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["original_photo"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["original_adhar_card"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["original_policy_document"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["original_address_proof"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["bank_passbook"].ToString().ToUpper() + "</td></tr>");
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
    //MD change
    protected void btn_emp_Export_Click(object sender, EventArgs e)
    {
        try
        {

            d.con.Open();
           // MySqlDataAdapter adp2;

            MySqlDataAdapter adp2 = null;
          //  MySqlDataAdapter adp2;
            if (ddlunitclient1.SelectedValue == "Select")
            {
                //rahul 24-04-2019
                // adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='" + Session["comp_code"].ToString() + "') as CLIENT_NAME, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.PF_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME,pay_employee_master.LOCATION,pay_employee_master.LOCATION_CITY FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' ORDER BY pay_employee_master.EMP_CODE", d.con1);
                if (ddl_emp_type.SelectedValue == "1") {

                    adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',  case pay_grade_master.grade_desc when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else pay_grade_master.grade_desc end as 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.original_bank_account_no, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code inner join pay_grade_master on pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code = pay_grade_master.comp_code  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and LEFT_DATE IS NULL  ORDER BY pay_employee_master.EMP_CODE", d.con1);

                }
                else
                    if (ddl_emp_type.SelectedValue == "2")
                    {

                        adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',  case pay_grade_master.grade_desc when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else pay_grade_master.grade_desc end as 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.original_bank_account_no, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code inner join pay_grade_master on pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code = pay_grade_master.comp_code  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and LEFT_DATE IS NOT NULL  ORDER BY pay_employee_master.EMP_CODE", d.con1);

                    }
                
                }
            else
            {
                if (ddl_emp_type.SelectedValue == "1")
                {
                    adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',  case pay_grade_master.grade_desc when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else pay_grade_master.grade_desc end as 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.original_bank_account_no, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code inner join pay_grade_master on pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code = pay_grade_master.comp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "' and LEFT_DATE IS NULL ORDER BY pay_employee_master.EMP_CODE", d.con1);
                }
                else if (ddl_emp_type.SelectedValue == "1")
                {
                    adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',  case pay_grade_master.grade_desc when 'OFFICE BOY' then case pay_employee_master.gender when 'M' then 'OFFICE BOY' when 'F' then 'OFFICE LADY' else 'OFFICE BOY' end else pay_grade_master.grade_desc end as 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.original_bank_account_no, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code inner join pay_grade_master on pay_employee_master.grade_code=pay_grade_master.grade_code and pay_employee_master.comp_code = pay_grade_master.comp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "' and LEFT_DATE IS NOT NULL ORDER BY pay_employee_master.EMP_CODE", d.con1);
                
                }

                 }

            DataSet ds = new DataSet();
            adp2.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Employee_Details.xls");
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
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_Leftemp_Export_Click(object sender, EventArgs e)
    {
        try
        {

            d.con.Open();
            MySqlDataAdapter adp2;
            if (ddlunitclient1.SelectedValue == "Select")
            {

                // adp2 = new MySqlDataAdapter("SELECT	(Select CLIENT_NAME from pay_client_master where CLIENT_CODE= pay_employee_master.CLIENT_CODE and comp_code='" + Session["comp_code"].ToString() + "') as CLIENT_NAME, pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',(Select grade_desc from pay_grade_master where grade_code= pay_employee_master.GRADE_CODE and comp_code='C01') as GRADE_DESC,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS, pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.PF_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.ESIC_NUMBER,pay_employee_master.PF_BANK_NAME,pay_employee_master.PF_IFSC_CODE,pay_employee_master.BANK_EMP_AC_CODE,pay_employee_master.EMP_NATIONALITY,pay_employee_master.BANK_HOLDER_NAME,pay_employee_master.LOCATION,pay_employee_master.LOCATION_CITY FROM pay_employee_master  WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' ORDER BY pay_employee_master.EMP_CODE", d.con1);
                adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(LEFT_DATE,'%d/%m/%Y')as 'LEFT_DATE' ,DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', (SELECT grade_desc FROM pay_grade_master WHERE grade_code = pay_employee_master.GRADE_CODE AND comp_code = 'C01') AS 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and LEFT_DATE IS NOT NULL  ORDER BY pay_employee_master.EMP_CODE", d.con1);

            }
            else
            {
                adp2 = new MySqlDataAdapter("SELECT distinct(STATE_NAME) as 'STATE_NAME' ,(SELECT CLIENT_NAME FROM pay_client_master WHERE CLIENT_CODE = pay_employee_master.CLIENT_CODE AND comp_code = '" + Session["comp_code"].ToString() + "') AS 'CLIENT_NAME', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_CODE, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', DATE_FORMAT(LEFT_DATE,'%d/%m/%Y')as 'LEFT_DATE' , DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', (SELECT grade_desc FROM pay_grade_master WHERE grade_code = pay_employee_master.GRADE_CODE AND comp_code = 'C01') AS 'GRADE_DESC', (SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO', pay_employee_master.EMP_CURRENT_ADDRESS,pay_employee_master.EMP_PERM_ADDRESS,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.EMP_NEW_PAN_NO,pay_employee_master.P_TAX_NUMBER, pay_employee_master.PF_NUMBER, pay_employee_master.ESIC_NUMBER, pay_employee_master.PF_BANK_NAME, pay_employee_master.PF_IFSC_CODE, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.EMP_NATIONALITY, pay_employee_master.BANK_HOLDER_NAME, pay_employee_master.LOCATION, pay_employee_master.LOCATION_CITY,pay_employee_master.PAN_NUMBER FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "' and LEFT_DATE  IS NOT NULL ORDER BY pay_employee_master.EMP_CODE", d.con1);
            }

            DataSet ds = new DataSet();
            adp2.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Employee_Details.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds);
                Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, null);
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
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        btn_Leftemp_Export.Visible = false;
    }

    public class MyTemplate2 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate2(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<TABLE BORDER=1><TR><TH>CLIENT NAME</TH><TH>STATE NAME</TH><TH>BRANCH NAME</TH><TH>EMP CODE</TH><TH>EMP NAME</TH><TH>FATHER NAME</TH><TH>JOINING DATE</TH><TH>LTFT DATE</TH><TH>BIRTH DATE</TH><TH>GRADE NAME</TH><TH>REPORTING TO</TH><TH>PRESENT ADDRESS</TH><TH>PERMANANT ADDRESS</TH><TH>MOBILE NUMBER</TH><TH>PAN NUMBER</TH><TH>ADHAR CARD NO</TH><TH>PF NUMBER</TH><TH>ESIC NUMBER</TH><TH>BANK NAME</TH><TH>IFSC CODE</TH><TH>BANK ACCOUNT NUMBER</TH><TH>NATIONALITY</TH><TH>BANK HOLDER NAME</TH><TH>WORK STATE</TH><TH>WORK CITY</TH><TH>UAN NUMBER</TH></TR>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"] + " </td><td>" + ds.Tables[0].Rows[ctr]["STATE_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["UNIT_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_FATHER_NAME"] + " </td><td>" + ds.Tables[0].Rows[ctr]["JOINING_DATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["LEFT_DATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["BIRTH_DATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["GRADE_DESC"] + "</td><td>" + ds.Tables[0].Rows[ctr]["REPORTING_TO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_CURRENT_ADDRESS"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_PERM_ADDRESS"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NEW_PAN_NO"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["P_TAX_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ESIC_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_BANK_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["BANK_EMP_AC_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NATIONALITY"] + "</td><td>" + ds.Tables[0].Rows[ctr]["BANK_HOLDER_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["LOCATION"] + "</td><td>" + ds.Tables[0].Rows[ctr]["LOCATION_CITY"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["PAN_NUMBER"] + "</td></tr>");
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
        public MyTemplate1(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:
                    lc = new LiteralControl("<TABLE BORDER=1><TR><TH>CLIENT NAME</TH><TH>STATE NAME</TH><TH>BRANCH NAME</TH><TH>EMP CODE</TH><TH>EMP NAME</TH><TH>FATHER NAME</TH><TH>JOINING DATE</TH><TH>BIRTH DATE</TH><TH>GRADE NAME</TH><TH>REPORTING TO</TH><TH>PRESENT ADDRESS</TH><TH>PERMANANT ADDRESS</TH><TH>MOBILE NUMBER</TH><TH>PAN NUMBER</TH><TH>ADHAR CARD NO</TH><TH>PF NUMBER</TH><TH>ESIC NUMBER</TH><TH>BANK NAME</TH><TH>IFSC CODE</TH><TH>BANK ACCOUNT NUMBER</TH><TH>NATIONALITY</TH><TH>BANK HOLDER NAME</TH><TH>WORK STATE</TH><TH>WORK CITY</TH><TH>UAN NUMBER</TH></TR>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["CLIENT_NAME"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["STATE_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["UNIT_CODE"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_CODE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_FATHER_NAME"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["JOINING_DATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["BIRTH_DATE"] + "</td><td>" + ds.Tables[0].Rows[ctr]["GRADE_DESC"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["REPORTING_TO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_CURRENT_ADDRESS"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_PERM_ADDRESS"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_MOBILE_NO"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NEW_PAN_NO"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["P_TAX_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ESIC_NUMBER"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_BANK_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["original_bank_account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_NATIONALITY"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["BANK_HOLDER_NAME"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LOCATION"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["LOCATION_CITY"].ToString().ToUpper() + "</td><td>'" + ds.Tables[0].Rows[ctr]["PAN_NUMBER"] + "</td></tr>");
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

    //MD change start
    protected void download_document1(object sender, CommandEventArgs e)
    {
        string emp_id = SearchGridView.SelectedRow.Cells[0].Text;
        string image_id = emp_id + e.CommandArgument + ".pdf";

        downloadfile(image_id);
    }

    protected void downloadfile(string filename)
    {
        try
        {
            string path2 = Server.MapPath("~\\EMP_Images\\" + filename);

            bool code = File.Exists(path2);
            if (code == true)
            {
                Response.Clear();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path2));

                Response.TransmitFile("~\\EMP_Images\\" + filename);
                Response.WriteFile(path2);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                Response.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Can not Found!!.');", true);
            }

        }
        catch (Exception ex) { throw ex; }


    }

    protected void images_visibility()
    {

        link_photo.Visible = false;
        link_originalphoto.Visible = false;
        link_bank_upload.Visible = false;
        link_original_adharcard_upload.Visible = false;
        link_Police_document.Visible = false;
        link_original_police_document.Visible = false;
        link_Address_Proof.Visible = false;
        link_original_Address_Proof.Visible = false;
        link_Passport_upload.Visible = false;
        link_Driving_Liscence_upload.Visible = false;
        link_Tenth_Marksheet_upload.Visible = false;
        link_Twelve_Marksheet_upload.Visible = false;
        link_Diploma_upload.Visible = false;
        link_Degree_upload.Visible = false;
        link_Post_Graduation_upload.Visible = false;
        link_Education_4_upload.Visible = false;
        link_Formno_2.Visible = false;
        link_Formno_11.Visible = false;
        link_noc_form.Visible = false;
        link_bank_passbook.Visible = false;
        link_adhar_pan_upload.Visible = false;
        link_biodata_upload.Visible = false;
        link_medical_form.Visible = false;
        link_emp_signature.Visible = false;
        itc_link_btn.Visible = false;


    }
    //end
    //protected void ddl_employee_type_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl_employee_type.SelectedValue == "Staff")
    //    {

    //        ddl_department.SelectedValue = "Select";
    //        ddl_department.Enabled = true;
    //    }
    //    else
    //    {

    //        ddl_department.SelectedValue = "HR Department";
    //        ddl_department.Enabled = false;
    //    }
    //}

    protected void txt_joiningdate_Click(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            //  MySqlCommand cmd1 = new MySqlCommand("SELECT emp_code FROM  pay_employee_master WHERE comp_code = '" + Session["comp_code"].ToString() + "'  AND client_code='" + ddl_unit_client.SelectedValue + "'   AND UNIT_CODE ='" + ddl_unitcode.SelectedValue + "'   AND EMP_CURRENT_STATE = '" + ddl_clientwisestate.SelectedValue + "'     AND department_type = '" + ddl_department.SelectedValue + "'  AND EMP_NAME = '" + txt_eename.Text + "'   AND BIRTH_DATE ='" + birthdate + "' ,   ", d.con);
            MySqlCommand cmd1 = new MySqlCommand("SELECT emp_code FROM  pay_employee_master WHERE comp_code = '" + Session["comp_code"].ToString() + "'  AND client_code='" + ddl_unit_client.SelectedValue + "'   AND UNIT_CODE ='" + ddl_unitcode.SelectedValue + "'   AND EMP_CURRENT_STATE = '" + ddl_clientwisestate.SelectedValue + "'     AND department_type = '" + ddl_department.SelectedValue + "'  AND EMP_NAME = '" + txt_eename.Text + "'     ", d.con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Name Employee All Ready  Present In System You Add Same Again Or Not...');", true);
            }
            dr1.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (check_document1())
        {
            if (chk_uniform() == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Uniform Or Apron and  Shose compulsory Of Employee!!.');", true);
                newpanel.Visible = true;
                return;
            }
            if (chk_IDCard() == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter ID-Card Information Of Employee!!.');", true);
                newpanel.Visible = true;
                return;
            }
            int result = 0;

            result = d.operation("update pay_employee_master set legal_flag = 1 ,ap_status='Approve By Admin' ,reject_reason ='' where comp_code='" + Session["Comp_code"].ToString() + "' AND emp_code='" + txt_eecode.Text + "'");


            if (result == 1)
            {
                btn_add.Visible = false;
                btnupdate.Visible = false;
                btnUpload.Visible = false;
                btn_approve.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Details Approve Succsefully ..!!');", true);
                reason_panel.Visible = false;//vikas add from visible fase after approve 22/04/2019
            }
        }


    }

    private void btn_visibility()
    {

        newpanel.Visible = true;
        btn_add.Visible = false;
        btndelete.Visible = true;
        btnupdate.Visible = true;
        btnUpload.Visible = true;
        btn_approve.Visible = true;

    }
    protected bool check_document()
    {


        //passpo photo
        if (ddl_employee_type.SelectedValue != "Reliever")
        {

            if (d.getsinglestring("select original_photo from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload Orignal  Photo of  Employee');", true);
                btn_visibility();
                return false;
            }

            if (!(ddl_clientwisestate.SelectedValue == "Arunachal Pradesh" || ddl_clientwisestate.SelectedValue == "Assam" || ddl_clientwisestate.SelectedValue == "Manipur" || ddl_clientwisestate.SelectedValue == "Meghalaya" || ddl_clientwisestate.SelectedValue == "Mizoram " || ddl_clientwisestate.SelectedValue == "Nagaland" || ddl_clientwisestate.SelectedValue == "Sikkim" || ddl_clientwisestate.SelectedValue == "Tripura"))
            {

                if (d.getsinglestring("select original_adhar_card from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload Orignal Adhar Card of  Employee');", true);
                    btn_visibility();
                    return false;
                }
            }
        }


        return true;
    }
    //vikas add for chacke document upload02-07-2019
    protected bool check_document1()
    {

        if (d.getsinglestring("select original_address_proof from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload Orignal Address Proof of  Employee');", true);
            btn_visibility();
            return false;
        }



        if (d.getsinglestring("select bank_passbook from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload Bank Passbook of  Employee');", true);
            btn_visibility();
            return false;
        }

        if (d.getsinglestring("select emp_signature from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload Employee Joinig  kit');", true);
            btn_visibility();
            return false;
        }
        ////add TIC
        //if (d.getsinglestring("select itc_upload_form from pay_images_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'").Equals(""))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Upload TIC Document ');", true);
        //    btn_visibility();
        //    return false;
        //}

        return true;
    }
    private void visiblity()
    {

        Numberchild.ReadOnly = true;

        ddl_Mother_Tongue.ReadOnly = true;

        ddl_bankcode.ReadOnly = true;

        ddl_location.ReadOnly = true;
        ddl_location_city.ReadOnly = true;

        pre_mobileno_1.ReadOnly = true;
        pre_mobileno_2.ReadOnly = true;
        txt_Area_Of_Expertise.ReadOnly = true;
        txt_Details_Of_Handicap.ReadOnly = true;
        txt_Driving_License_No.ReadOnly = true;
        txt_Identitymark.ReadOnly = true;
        txt_Language_Known.ReadOnly = true;
        txt_Mise.ReadOnly = true;
        // txt_Nationality.ReadOnly = true;
        txt_Passport_No.ReadOnly = true;
        txt_Passport_Validity_Date.ReadOnly = true;
        txt_Place_Of_Birth.ReadOnly = true;
        txt_Visa_Validity_Date.ReadOnly = true;
        txt_address1.ReadOnly = true;
        txt_address2.ReadOnly = true;
        txt_adhaar1.ReadOnly = true;
        txt_adhaar2.ReadOnly = true;
        txt_adhaar3.ReadOnly = true;
        txt_adhaar4.ReadOnly = true;
        txt_adhaar5.ReadOnly = true;
        txt_adhaar6.ReadOnly = true;
        txt_adhaar7.ReadOnly = true;
        txt_advance_payment.ReadOnly = true;
        txt_bankholder.ReadOnly = true;
        txt_birthdate.ReadOnly = true;
        txt_confirmationdate.ReadOnly = true;
        txt_dob1.ReadOnly = true;
        txt_dob2.ReadOnly = true;
        txt_dob3.ReadOnly = true;
        txt_dob4.ReadOnly = true;
        txt_dob5.ReadOnly = true;
        txt_dob6.ReadOnly = true;
        txt_dob7.ReadOnly = true;
        // txt_eecode.ReadOnly = true;
        txt_eefatharname.ReadOnly = true;
        txt_eename.ReadOnly = true;
        txt_email.ReadOnly = true;
        txt_emailid1.ReadOnly = true;
        txt_emailid2.ReadOnly = true;
        txt_employeeaccountnumber.ReadOnly = true;
        txt_end_date.ReadOnly = true;
        txt_experience_in_months_1.ReadOnly = true;
        txt_experience_in_months_2.ReadOnly = true;
        txt_experience_in_months_3.ReadOnly = true;
        txt_experience_in_months_4.ReadOnly = true;
        txt_experience_in_months_5.ReadOnly = true;
        //txt_fine.ReadOnly = true;
        txt_fmobile1.ReadOnly = true;
        txt_fmobile2.ReadOnly = true;
        txt_fmobile3.ReadOnly = true;
        txt_fmobile4.ReadOnly = true;
        txt_fmobile5.ReadOnly = true;
        txt_fmobile6.ReadOnly = true;
        txt_fmobile7.ReadOnly = true;
        txt_height.ReadOnly = true;
        txt_hobbies.ReadOnly = true;
        txt_ihmscode.ReadOnly = true;
        txt_joiningdate.ReadOnly = true;
        txt_key_skill_1.ReadOnly = true;
        txt_key_skill_2.ReadOnly = true;
        txt_key_skill_3.ReadOnly = true;
        txt_key_skill_4.ReadOnly = true;
        txt_key_skill_5.ReadOnly = true;
        txt_kra.ReadOnly = true;
        //txt_leftdate.ReadOnly = true;
        // txt_maritalstaus.ReadOnly = true;
        txt_mobilenumber.ReadOnly = true;
        txt_name1.ReadOnly = true;
        txt_name2.ReadOnly = true;
        txt_name3.ReadOnly = true;
        txt_name4.ReadOnly = true;
        txt_name5.ReadOnly = true;
        txt_name6.ReadOnly = true;
        txt_name7.ReadOnly = true;
        txt_originalbankaccountno.ReadOnly = true;
        txt_pan1.ReadOnly = true;
        txt_pan2.ReadOnly = true;
        txt_pan3.ReadOnly = true;
        txt_pan4.ReadOnly = true;
        txt_pan5.ReadOnly = true;
        txt_pan6.ReadOnly = true;
        txt_pan7.ReadOnly = true;
        txt_permanantaddress.ReadOnly = true;
        txt_permanantpincode.ReadOnly = true;
        txt_pfbankname.ReadOnly = true;
        txt_pfbdate.ReadOnly = true;
        txt_pfifsccode.ReadOnly = true;
        txt_pfnomineename.ReadOnly = true;
        txt_pfnomineerelation.ReadOnly = true;
        txt_policestationname.ReadOnly = true;
        txt_premanent_mob1.ReadOnly = true;
        txt_premanent_mob2.ReadOnly = true;
        txt_presentaddress.ReadOnly = true;
        txt_presentpincode.ReadOnly = true;
        txt_ptaxnumber.ReadOnly = true;
        txt_qualification_1.ReadOnly = true;
        txt_qualification_2.ReadOnly = true;
        txt_qualification_3.ReadOnly = true;
        txt_qualification_4.ReadOnly = true;
        txt_qualification_5.ReadOnly = true;
        txt_ranteagrement_enddate.ReadOnly = true;
        txt_ranteagrement_satrtdate.ReadOnly = true;
        txt_relation1.ReadOnly = true;
        txt_relation2.ReadOnly = true;
        txt_relation3.ReadOnly = true;
        txt_relation4.ReadOnly = true;
        txt_relation5.ReadOnly = true;
        txt_relation6.ReadOnly = true;
        txt_relation7.ReadOnly = true;
        txt_residencecontactnumber.ReadOnly = true;
        txt_start_date.ReadOnly = true;
        txt_weight.ReadOnly = true;
        txt_year_of_passing_1.ReadOnly = true;
        txt_year_of_passing_2.ReadOnly = true;
        txt_year_of_passing_3.ReadOnly = true;
        txt_year_of_passing_4.ReadOnly = true;
        txt_year_of_passing_5.ReadOnly = true;
       // txtreasonforleft.ReadOnly = true;
        txtref1mob.ReadOnly = true;
        txtref2mob.ReadOnly = true;
        txtrefname1.ReadOnly = true;
        txtrefname2.ReadOnly = true;
        txt_pan_new_num.ReadOnly = true;
        nominee_address.ReadOnly = true;
        //uniform_size.ReadOnly = true;
    }

    private void visiblity_true()
    {


        Numberchild.ReadOnly = false;

        ddl_Mother_Tongue.ReadOnly = false;

        ddl_bankcode.ReadOnly = false;

        ddl_location.ReadOnly = false;
        ddl_location_city.ReadOnly = false;

        pre_mobileno_1.ReadOnly = false;
        pre_mobileno_2.ReadOnly = false;
        txt_Area_Of_Expertise.ReadOnly = false;
        txt_Details_Of_Handicap.ReadOnly = false;
        txt_Driving_License_No.ReadOnly = false;
        txt_Identitymark.ReadOnly = false;
        txt_Language_Known.ReadOnly = false;
        txt_Mise.ReadOnly = false;
        //txt_Nationality.ReadOnly = false;
        txt_Passport_No.ReadOnly = false;
        txt_Passport_Validity_Date.ReadOnly = false;
        txt_Place_Of_Birth.ReadOnly = false;
        txt_Visa_Validity_Date.ReadOnly = false;
        txt_address1.ReadOnly = false;
        txt_address2.ReadOnly = false;
        txt_adhaar1.ReadOnly = false;
        txt_adhaar2.ReadOnly = false;
        txt_adhaar3.ReadOnly = false;
        txt_adhaar4.ReadOnly = false;
        txt_adhaar5.ReadOnly = false;
        txt_adhaar6.ReadOnly = false;
        txt_adhaar7.ReadOnly = false;
        txt_advance_payment.ReadOnly = false;
        txt_bankholder.ReadOnly = false;
        txt_birthdate.ReadOnly = false;
        txt_confirmationdate.ReadOnly = false;
        txt_dob1.ReadOnly = false;
        txt_dob2.ReadOnly = false;
        txt_dob3.ReadOnly = false;
        txt_dob4.ReadOnly = false;
        txt_dob5.ReadOnly = false;
        txt_dob6.ReadOnly = false;
        txt_dob7.ReadOnly = false;
        //txt_eecode.ReadOnly = false;
        txt_eefatharname.ReadOnly = false;
        txt_eename.ReadOnly = false;
        txt_email.ReadOnly = false;
        txt_emailid1.ReadOnly = false;
        txt_emailid2.ReadOnly = false;
        txt_employeeaccountnumber.ReadOnly = false;
        txt_end_date.ReadOnly = false;
        txt_experience_in_months_1.ReadOnly = false;
        txt_experience_in_months_2.ReadOnly = false;
        txt_experience_in_months_3.ReadOnly = false;
        txt_experience_in_months_4.ReadOnly = false;
        txt_experience_in_months_5.ReadOnly = false;
        //txt_fine.ReadOnly = false;
        txt_fmobile1.ReadOnly = false;
        txt_fmobile2.ReadOnly = false;
        txt_fmobile3.ReadOnly = false;
        txt_fmobile4.ReadOnly = false;
        txt_fmobile5.ReadOnly = false;
        txt_fmobile6.ReadOnly = false;
        txt_fmobile7.ReadOnly = false;
        txt_height.ReadOnly = false;
        txt_hobbies.ReadOnly = false;
        txt_ihmscode.ReadOnly = false;
        txt_joiningdate.ReadOnly = false;
        txt_key_skill_1.ReadOnly = false;
        txt_key_skill_2.ReadOnly = false;
        txt_key_skill_3.ReadOnly = false;
        txt_key_skill_4.ReadOnly = false;
        txt_key_skill_5.ReadOnly = false;
        txt_kra.ReadOnly = false;
        //txt_leftdate.ReadOnly = false;
      //  txt_maritalstaus.ReadOnly = false;
        txt_mobilenumber.ReadOnly = false;
        txt_name1.ReadOnly = false;
        txt_name2.ReadOnly = false;
        txt_name3.ReadOnly = false;
        txt_name4.ReadOnly = false;
        txt_name5.ReadOnly = false;
        txt_name6.ReadOnly = false;
        txt_name7.ReadOnly = false;
        txt_originalbankaccountno.ReadOnly = false;
        txt_pan1.ReadOnly = false;
        txt_pan2.ReadOnly = false;
        txt_pan3.ReadOnly = false;
        txt_pan4.ReadOnly = false;
        txt_pan5.ReadOnly = false;
        txt_pan6.ReadOnly = false;
        txt_pan7.ReadOnly = false;
        txt_permanantaddress.ReadOnly = false;
        txt_permanantpincode.ReadOnly = false;
        txt_pfbankname.ReadOnly = false;
        txt_pfbdate.ReadOnly = false;
        txt_pfifsccode.ReadOnly = false;
        txt_pfnomineename.ReadOnly = false;
        txt_pfnomineerelation.ReadOnly = false;
        txt_policestationname.ReadOnly = false;
        txt_premanent_mob1.ReadOnly = false;
        txt_premanent_mob2.ReadOnly = false;
        txt_presentaddress.ReadOnly = false;
        txt_presentpincode.ReadOnly = false;
        txt_ptaxnumber.ReadOnly = false;
        txt_qualification_1.ReadOnly = false;
        txt_qualification_2.ReadOnly = false;
        txt_qualification_3.ReadOnly = false;
        txt_qualification_4.ReadOnly = false;
        txt_qualification_5.ReadOnly = false;
        txt_ranteagrement_enddate.ReadOnly = false;
        txt_ranteagrement_satrtdate.ReadOnly = false;
        txt_relation1.ReadOnly = false;
        txt_relation2.ReadOnly = false;
        txt_relation3.ReadOnly = false;
        txt_relation4.ReadOnly = false;
        txt_relation5.ReadOnly = false;
        txt_relation6.ReadOnly = false;
        txt_relation7.ReadOnly = false;
        txt_residencecontactnumber.ReadOnly = false;
        txt_start_date.ReadOnly = false;
        txt_weight.ReadOnly = false;
        txt_year_of_passing_1.ReadOnly = false;
        txt_year_of_passing_2.ReadOnly = false;
        txt_year_of_passing_3.ReadOnly = false;
        txt_year_of_passing_4.ReadOnly = false;
        txt_year_of_passing_5.ReadOnly = false;
        txtreasonforleft.ReadOnly = false;
        txtref1mob.ReadOnly = false;
        txtref2mob.ReadOnly = false;
        txtrefname1.ReadOnly = false;
        txtrefname2.ReadOnly = false;
        txt_pan_new_num.ReadOnly = false;
        nominee_address.ReadOnly = false;
        //uniform_size.ReadOnly = false;

    }

    //Approval / Rejection
    DataSet ds = new DataSet();

    protected void btn_emp_approve_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_app_where = " and pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND left_date IS NULL AND legal_flag != 0 ORDER BY client_wise_state, unit_name, emp_name";

        ds = d.select_data(gv_app_query + gv_app_where);
        gv_app_gridview.DataSource = ds;
        gv_app_gridview.DataBind();

        reason_panel.Visible = false;
        newpanel.Visible = false;
        pln_searchemp.Visible = false;
        panel_approval.Visible = true;
        Panel_app_gv.Visible = true;
        panel_link.Visible = false;
        gv_app_gridview.Visible = true;
    }


    protected void ddl_app_client_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddl_app_client.SelectedValue != "Select")
        {

            //State
            ddl_app_state.Items.Clear();
            ddl_app_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_app_client.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);

            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_app_state.DataSource = dt_item;
                    ddl_app_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_app_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_app_state.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_app_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_unitcode.Items.Clear();


                string gv_app_where = " and  pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code='" + ddl_app_client.SelectedValue + "' AND  left_date IS NULL AND legal_flag != 0 ORDER BY client_wise_state, unit_name, emp_name";

                ds = d.select_data(gv_app_query + gv_app_where);
                gv_app_gridview.DataSource = ds;
                gv_app_gridview.DataBind();
                employee_status();
                panel_link.Visible = true;
                panel_all.Visible = true;
                //panel_data.Visible = false;
            }
        }


    }

    protected void ddl_app_state_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_app_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_app_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_app_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_app_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_app_unit.DataSource = dt_item;
                ddl_app_unit.DataTextField = dt_item.Columns[0].ToString();
                ddl_app_unit.DataValueField = dt_item.Columns[1].ToString();
                ddl_app_unit.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            ddl_app_unit.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();

            string gv_app_where = " and pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code='" + ddl_app_client.SelectedValue + "' AND pay_employee_master.client_wise_state='" + ddl_app_state.SelectedValue + "'  AND left_date IS NULL AND legal_flag != 0 ORDER BY client_wise_state, unit_name, emp_name";

            ds = d.select_data(gv_app_query + gv_app_where);
            gv_app_gridview.DataSource = ds;
            gv_app_gridview.DataBind();
            panel_all.Visible = true;
            panel_link.Visible = true;

        }
    }
    protected void ddl_app_unit_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_app_emp.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct emp_code,emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_app_unit.SelectedValue + "' and employee_type='Permanent' and left_date is null and legal_flag !=  0", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_app_emp.DataSource = dt_item;
                ddl_app_emp.DataTextField = dt_item.Columns[1].ToString();
                ddl_app_emp.DataValueField = dt_item.Columns[0].ToString();
                ddl_app_emp.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            ddl_app_emp.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();

            string gv_app_where = " and pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code='" + ddl_app_client.SelectedValue + "' AND pay_employee_master.client_wise_state='" + ddl_app_state.SelectedValue + "'  AND pay_employee_master.unit_code='" + ddl_app_unit.SelectedValue + "' AND  left_date IS NULL AND legal_flag != 0 ORDER BY client_wise_state, unit_name, emp_name";

            ds = d.select_data(gv_app_query + gv_app_where);
            gv_app_gridview.DataSource = ds;
            gv_app_gridview.DataBind();

            panel_all.Visible = true;
            panel_link.Visible = true;
        }
    }


    protected void ddl_app_emp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string gv_app_where = " and pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code='" + ddl_app_client.SelectedValue + "' AND pay_employee_master.client_wise_state='" + ddl_app_state.SelectedValue + "'  AND pay_employee_master.unit_code='" + ddl_app_unit.SelectedValue + "' AND pay_employee_master.emp_code = '" + ddl_app_emp.SelectedValue + "' AND left_date IS NULL AND legal_flag != 0 ORDER BY client_wise_state, unit_name, emp_name";
            ds = d.select_data(gv_app_query + gv_app_where);
            gv_app_gridview.DataSource = ds;
            gv_app_gridview.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            panel_all.Visible = true;
        }
    }
    public void employee_count(string emp_listcount)
    {
        employee_list = "0";
        newpanel.Visible = true;
        panel4.Visible = true;
        MySqlCommand menu = null;
        if (emp_listcount == "1")
        {
            ViewState["employee_count"] = d.getsinglestring("select count(1) from pay_employee_master where employee_type = 'Permanent' and client_code = '" + ddlunitclient1.SelectedValue + "'AND left_date IS NULL");

            // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select  pay_unit_master.state_name,pay_unit_master.unit_name,count(employee_type) as 'permanent' from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code where pay_employee_master.client_code='" + ddlunitclient1.SelectedValue + "' and employee_type='Permanent' and pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.left_date is null group by pay_employee_master.unit_code", d.con);
            menu = new MySqlCommand("select  pay_unit_master.state_name,pay_unit_master.unit_name,count(employee_type) as 'permanent' from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code where pay_employee_master.client_code='" + ddlunitclient1.SelectedValue + "' and employee_type='Permanent' and pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.left_date is null group by pay_employee_master.unit_code", d.con);
        }
        else if (emp_listcount == "2")
        {
            ViewState["employee_count"] = d.getsinglestring("select count(1) from pay_employee_master where employee_type = 'Permanent' and client_code = '" + ddlunitclient1.SelectedValue + "'AND left_date IS NULL  AND location = '" + ddl_gv_statewise.SelectedValue + "'");
            menu = new MySqlCommand("select  pay_unit_master.state_name,pay_unit_master.unit_name,count(employee_type) as 'permanent' from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code where pay_employee_master.client_code='" + ddlunitclient1.SelectedValue + "' and employee_type='Permanent' and pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.left_date is null AND location = '" + ddl_gv_statewise.SelectedValue + "' group by pay_employee_master.unit_code", d.con);
        }
        else if (emp_listcount == "3")
        {
            ViewState["employee_count"] = d.getsinglestring("select count(1) from pay_employee_master where employee_type = 'Permanent' and client_code = '" + ddlunitclient1.SelectedValue + "'AND left_date IS NULL  AND location = '" + ddl_gv_statewise.SelectedValue + "' AND unit_code = '" + ddl_gv_branchwise.SelectedValue + "'");
            menu = new MySqlCommand("select  pay_unit_master.state_name,pay_unit_master.unit_name,count(employee_type) as 'permanent' from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code where pay_employee_master.client_code='" + ddlunitclient1.SelectedValue + "' and employee_type='Permanent' and pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.left_date is null AND location = '" + ddl_gv_statewise.SelectedValue + "' AND pay_employee_master.unit_code = '" + ddl_gv_branchwise.SelectedValue + "' group by pay_employee_master.unit_code", d.con);
        }

        employee_list = ViewState["employee_count"].ToString();

        d.con.Open();
        MySqlDataAdapter cmd1 = new MySqlDataAdapter(menu);
        System.Data.DataTable dt = new System.Data.DataTable();
        cmd1.Fill(dt);

        if (dt.Rows.Count > 0)
        {

            gv_employee_list.DataSource = dt;
            gv_employee_list.DataBind();

        }
        dt.Dispose();
        d.con.Close();
        //employee_list = ViewState["employee_count"].ToString();
        newpanel.Visible = true;
        panel4.Visible = true;

    }
    public void left_employee_count()
    {
        left_emp_count = "0";
        newpanel.Visible = true;
        panel4.Visible = true;
        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select emp_name,client_name, unit_name,location,left_date from pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code inner join pay_client_master on pay_employee_master.comp_code=pay_client_master.comp_code and pay_employee_master.client_code=pay_client_master.client_code where pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and left_date is not null", d.con);

        var today = DateTime.Now;
        var tommaro = today.AddDays(-8).ToString("dd/MM/yyyy");
        var tommaro1 = today.AddDays(1).ToString("dd/MM/yyyy");


        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct emp_code ,client_code,end_date from pay_document_details where end_date like '%" + tommaro + "%' goup by emp_code", d.con);
        MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct pay_document_details.emp_code, emp_name,client_name,unit_name,(SELECT date_format(end_date,'%d/%m/%Y') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'ID_Card' and end_date BETWEEN  STR_TO_DATE ('" + tommaro + "','%d/%m/%Y')  and STR_TO_DATE ('" + tommaro1 + "','%d/%m/%Y')GROUP BY emp_code) as 'ID_card',(SELECT date_format(end_date,'%d/%m/%Y') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Shoes' and end_date BETWEEN  STR_TO_DATE ('" + tommaro + "','%d/%m/%Y')  and STR_TO_DATE ('" + tommaro1 + "','%d/%m/%Y')GROUP BY emp_code) as 'Shoes',(SELECT date_format(end_date,'%d/%m/%Y') FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Uniform' and end_date BETWEEN  STR_TO_DATE ('" + tommaro + "','%d/%m/%Y')  and STR_TO_DATE ('" + tommaro1 + "','%d/%m/%Y')GROUP BY emp_code) as 'uniform' from pay_document_details inner join pay_employee_master on pay_document_details.comp_code =pay_employee_master.comp_code and  pay_document_details.emp_code =pay_employee_master.emp_code inner join pay_client_master on pay_document_details.comp_code=pay_client_master.comp_code and pay_document_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_document_details.unit_code=pay_unit_master.unit_code and pay_document_details.comp_code=pay_unit_master.comp_code where pay_document_details.client_code='" + ddlunitclient1.SelectedValue + "'and end_date BETWEEN  STR_TO_DATE ('" + tommaro + "','%d/%m/%Y')  and STR_TO_DATE ('" + tommaro1 + "','%d/%m/%Y')  and employee_type='Permanent' and left_date is null  group by emp_code", d.con);


        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct pay_document_details.emp_code, emp_name,client_name,unit_name,end_date from pay_document_details inner join pay_employee_master on pay_document_details.comp_code =pay_employee_master.comp_code and  pay_document_details.emp_code =pay_employee_master.emp_code inner join pay_client_master on pay_document_details.comp_code=pay_client_master.comp_code and pay_document_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_document_details.unit_code=pay_unit_master.unit_code and pay_document_details.comp_code=pay_unit_master.comp_code where end_date like '%" + tommaro + "%' and employee_type='Permanent' and left_date is null  group by emp_code", d.con);




        d.con.Open();
        try
        {
            // MySqlDataReader dr1 = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            cmd1.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ViewState["left_emp_count"] = dt.Rows.Count.ToString();
                left_emp_count = ViewState["left_emp_count"].ToString();

                gv_left_emp_count.DataSource = dt;
                gv_left_emp_count.DataBind();

            }
            dt.Dispose();
            d.con.Close();
            newpanel.Visible = true;
            panel4.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    public void uniformhold_employee_count()
    {
        unifom_hold_count = "0";
        newpanel.Visible = true;
        panel4.Visible = true;
        //var today = DateTime.Now;
        //var tommaro = today.AddDays(1).ToShortDateString();


        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct emp_code client_code,end_date from pay_document_details where end_date like '%" + tommaro + "%' goup by emp_code", d.con);
        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select emp_name,client_name, unit_name from pay_document_details inner join pay_unit_master on pay_document_details.unit_code=pay_unit_master.unit_code and pay_document_details.comp_code=pay_unit_master.comp_code inner join pay_client_master on pay_document_details.comp_code=pay_client_master.comp_code and pay_document_details.client_code=pay_client_master.client_code inner join pay_employee_master on pay_document_details.comp_code=pay_employee_master.comp_code and pay_document_details.client_code=pay_employee_master.client_code where pay_document_details.comp_code='" + Session["COMP_CODE"].ToString() + "' and dispatch_flag='2'", d.con);
        // MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct pay_document_details.emp_code, emp_name,client_name,pay_document_details. unit_code, unit_name from pay_document_details inner join pay_unit_master on pay_document_details.unit_code=pay_unit_master.unit_code and pay_document_details.comp_code=pay_unit_master.comp_code inner join pay_client_master on pay_document_details.comp_code=pay_client_master.comp_code and pay_document_details.client_code=pay_client_master.client_code inner join pay_employee_master on pay_document_details.comp_code=pay_employee_master.comp_code and pay_document_details.client_code=pay_employee_master.client_code where  pay_document_details.client_code='" + ddlunitclient1.SelectedValue + "' and pay_document_details.comp_code='" + Session["COMP_CODE"].ToString() + "' and dispatch_flag='2' and left_date is null and employee_type='Permanent' group by emp_code,unit_code", d.con);
        MySqlDataAdapter cmd1 = new MySqlDataAdapter("select distinct pay_document_details.emp_code, emp_name,client_name,pay_document_details. unit_code, unit_name,(SELECT case when dispatch_flag = '2' then 'Hold' else '' end as 'id_card' FROM pay_document_details WHERE pay_document_details.emp_code = pay_employee_master.emp_code AND document_type = 'ID_Card'  ) as 'ID_card',(SELECT case when dispatch_flag = '2' then 'Hold' else '' end  as 'shoes' FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Shoes'  ) as 'Shoes',(SELECT case when dispatch_flag = '2' then 'Hold' else '' end as 'uniform' FROM pay_document_details WHERE pay_employee_master.emp_code = pay_document_details.emp_code AND document_type = 'Uniform' ) as 'uniform' from pay_document_details inner join pay_unit_master on pay_document_details.unit_code=pay_unit_master.unit_code and pay_document_details.comp_code=pay_unit_master.comp_code inner join pay_client_master on pay_document_details.comp_code=pay_client_master.comp_code and pay_document_details.client_code=pay_client_master.client_code inner join pay_employee_master on pay_document_details.comp_code=pay_employee_master.comp_code and pay_document_details.emp_code=pay_employee_master.emp_code where  pay_document_details.client_code='" + ddlunitclient1.SelectedValue + "' and pay_document_details.comp_code='" + Session["COMP_CODE"].ToString() + "' and dispatch_flag='2' and left_date is null and employee_type='Permanent' group by emp_code,unit_code", d.con);
        d.con.Open();
        try
        {
            // MySqlDataReader dr1 = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            cmd1.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ViewState["unifom_hold_count"] = dt.Rows.Count.ToString();
                unifom_hold_count = ViewState["unifom_hold_count"].ToString();

                gv_unifom_hold_count.DataSource = dt;
                gv_unifom_hold_count.DataBind();

            }
            dt.Dispose();
            d.con.Close();
            newpanel.Visible = true;
            panel4.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void vacant()
    {
        try
        {
            //vakant branch
            ViewState["vakant_branch"] = 0;

            gv_branch_list.DataSource = null;
            gv_branch_list.DataBind();
            d.con.Open();
            // dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 4);
            // cmd_item = new MySqlDataAdapter("select (select client_name from pay_client_master where pay_client_master.comp_code = '" + compcode + "'  and pay_client_master.client_code = pay_unit_master.client_code) as 'Client_Name',state_name, unit_name as 'branch_name' from pay_unit_master WHERE pay_unit_master.comp_code = '" + compcode + "' " + where + "  and  (IFNULL((Emp_count -  (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE pay_unit_master.branch_status = '0' AND unit_code = pay_unit_master.unit_code AND comp_code = '" + compcode + "'  AND Employee_type != 'Reliever' " + where + " AND pay_employee_master.LEFT_DATE IS NULL)), 0)) !='0'", con);
            MySqlDataAdapter cmd1 = new MySqlDataAdapter("select (select client_name from pay_client_master where pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'  and pay_client_master.client_code = pay_unit_master.client_code) as 'Client_Name',state_name, unit_name as 'branch_name' from pay_unit_master WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.client_code = '" + ddlunitclient1.SelectedValue + "' and branch_status='0' and  (IFNULL((Emp_count -  (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE pay_unit_master.branch_status = '0' AND unit_code = pay_unit_master.unit_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND pay_unit_master.client_code = '" + ddlunitclient1.SelectedValue + "' and Employee_type != 'Reliever'  AND pay_employee_master.LEFT_DATE IS NULL)), 0)) !='0'", d.con);
            System.Data.DataTable dt_item = new System.Data.DataTable();
            cmd1.Fill(dt_item);
            branch_list = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["vakant_branch"] = dt_item.Rows.Count.ToString();
                branch_list = ViewState["vakant_branch"].ToString();

                gv_branch_list.DataSource = dt_item;
                gv_branch_list.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();



            branch_list = ViewState["vakant_branch"].ToString();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void employee_status()
    {

        try
        {
            ViewState["rem_emp_count"] = 0;
            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            ViewState["appro_emp_bank"] = 0;
            ViewState["rejected_bank_emp"] = 0;
            // ViewState["vakant_branch"] = 0;
            panel_link.Visible = true;

            gv_rem_emp_count.DataSource = null;
            gv_rem_emp_count.DataBind();

            System.Data.DataTable dt_item = new System.Data.DataTable();
            //policy
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 0);
            rem_emp_count = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["rem_emp_count"] = dt_item.Rows.Count.ToString();
                rem_emp_count = ViewState["rem_emp_count"].ToString();
                gv_rem_emp_count.DataSource = dt_item;
                gv_rem_emp_count.DataBind();

            }
            //approve by admin
            gv_appro_emp_count.DataSource = null;
            gv_appro_emp_count.DataBind();
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 1);
            appro_emp_count = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_count"] = dt_item.Rows.Count.ToString();
                appro_emp_count = ViewState["appro_emp_count"].ToString();

                gv_appro_emp_count.DataSource = dt_item;
                gv_appro_emp_count.DataBind();

            }
            dt_item.Dispose();

            //approve by legal

            gv_appro_emp_legal.DataSource = null;
            gv_appro_emp_legal.DataBind();
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 2);

            appro_emp_legal = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_legal"] = dt_item.Rows.Count.ToString();
                appro_emp_legal = ViewState["appro_emp_legal"].ToString();

                gv_appro_emp_legal.DataSource = dt_item;
                gv_appro_emp_legal.DataBind();

            }
            dt_item.Dispose();

            gv_reject_emp_legal.DataSource = null;
            gv_reject_emp_legal.DataBind();

            //reject by legal
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 3);
            reject_emp_legal = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["reject_emp_legal"] = dt_item.Rows.Count.ToString();
                reject_emp_legal = ViewState["reject_emp_legal"].ToString();

                gv_reject_emp_legal.DataSource = dt_item;
                gv_reject_emp_legal.DataBind();

            }
            dt_item.Dispose();

              // employee bank approve

            gv_appro_emp_bank.DataSource = null;
            gv_appro_emp_bank.DataBind();
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_state.SelectedValue, 4);

            appro_emp_bank = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_bank"] = dt_item.Rows.Count.ToString();
                appro_emp_bank = ViewState["appro_emp_bank"].ToString();

                gv_appro_emp_bank.DataSource = dt_item;
                gv_appro_emp_bank.DataBind();

            }
            dt_item.Dispose();

            gv_rejected_bank_emp.DataSource = null;
            gv_rejected_bank_emp.DataBind();

          // remaining employee bank approve
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_state.SelectedValue, 5);
            rejected_bank_emp = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["rejected_bank_emp"] = dt_item.Rows.Count.ToString();
                rejected_bank_emp = ViewState["rejected_bank_emp"].ToString();

                gv_rejected_bank_emp.DataSource = dt_item;
                gv_rejected_bank_emp.DataBind();

            }
          

            ////vakant branch

            //gv_branch_list.DataSource = null;
            //gv_branch_list.DataBind();
            //dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_app_client.SelectedValue, ddl_app_state.SelectedValue, 4);
            //branch_list = "0";
            //if (dt_item.Rows.Count > 0)
            //{
            //    ViewState["vakant_branch"] = dt_item.Rows.Count.ToString();
            //    branch_list = ViewState["vakant_branch"].ToString();

            //    gv_branch_list.DataSource = dt_item;
            //    gv_branch_list.DataBind();

            //}
            dt_item.Dispose();


            rem_emp_count = ViewState["rem_emp_count"].ToString();
            appro_emp_count = ViewState["appro_emp_count"].ToString();
            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
            reject_emp_legal = ViewState["reject_emp_legal"].ToString();
             appro_emp_bank = ViewState["appro_emp_bank"].ToString();
            rejected_bank_emp = ViewState["rejected_bank_emp"].ToString();
            //   branch_list = ViewState["vakant_branch"].ToString();
        }
        catch (Exception ex) { throw ex; }
        finally { }

    }
    protected void gridService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
        //    //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        //}
    }
    protected void gv_app_gridview_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            images_visibility();
            getEmployee(gv_app_gridview.SelectedRow.Cells[0].Text);
        }
        catch (Exception GetEmpError)
        {
            throw GetEmpError;
        }
        finally
        {
            if (d.getsinglestring("select employee_type from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + txt_eecode.Text + "'").Contains("Permanent"))
            {

                string legal_flag = d.getsinglestring("select legal_flag from pay_employee_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'");
                if (legal_flag.Equals("0") || legal_flag.Equals("3"))
                {

                    visiblity_true();
                    btnupdate.Visible = true;
                    btndelete.Visible = true;
                    btn_approve.Visible = true;

                }
                else
                {
                    visiblity();
                    btnupdate.Visible = false;
                    btndelete.Visible = false;
                    btn_approve.Visible = false;
                    btncloselow.Visible = false;
                    btnUpload.Visible = false;
                }
            }
            else
            {
                visiblity_true();
                btnupdate.Visible = true;
                btndelete.Visible = true;
                Button1.Visible = true;
                //  Button1.Visible = true;
                ViewState["id"] = "1";
            }
        }
        newpanel.Visible = true;
        panel_approval.Visible = false;
        ////ddl_employee_type.Enabled = false;
        //ddl_department.Enabled = false;
        //ddl_unit_client.Enabled = false;
        //ddl_clientwisestate.Enabled = false;
        //ddl_unitcode.Enabled = false;
        //ddl_grade.Enabled = false;
        //txt_eecode.Enabled = false;
        //txt_eename.Enabled = false;

    }
    protected void gv_app_gridview_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_app_gridview, "Select$" + e.Row.RowIndex);
        }
    }

    public void bank_detail_visibility(int i)
    {
        if (i == 1)
        {
            txt_bankholder.ReadOnly = false;
            txt_pfbankname.ReadOnly = false;
            ddl_bankcode.ReadOnly = false;
            txt_employeeaccountnumber.ReadOnly = false;
            txt_originalbankaccountno.ReadOnly = false;
            txt_pfifsccode.ReadOnly = false;
        }
        else
        {
            txt_bankholder.ReadOnly = true;
            txt_pfbankname.ReadOnly = true;
            ddl_bankcode.ReadOnly = true;
            txt_employeeaccountnumber.ReadOnly = true;
            txt_originalbankaccountno.ReadOnly = true;
            txt_pfifsccode.ReadOnly = true;


        }
    }

    //vikas for uniform size ,pantry size,shoes size

    public void panel_visibility()
    {

        Panel111.Visible = false;
        Panel112.Visible = false;
        Panel113.Visible = false;
        Panel114.Visible = false;
    }

    protected void searchadgarcard_Click(object sender, EventArgs e)
    {
        SearchGridView.Visible = true;
        panel_adhar_card.Visible = true;

        try
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            //string same_adhar=d.getsinglestring()
            //DataSet ds = new DataSet();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            //  MySqlDataAdapter adp = new MySqlDataAdapter("SELECT distinct pay_employee_master.EMP_CODE, pay_employee_master.ihmscode,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME', pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME, pay_employee_master.employee_type, pay_employee_master.EMP_MOBILE_NO, DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code INNER JOIN pay_client_state_role_grade ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.unit_code = pay_client_state_role_grade.unit_code where ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) AND (pay_employee_master.LEFT_REASON = '' || pay_employee_master.LEFT_REASON IS NULL)", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT distinct pay_employee_master.EMP_CODE, pay_client_master.CLIENT_NAME,pay_unit_master.state_name as 'location', pay_unit_master.UNIT_NAME,(SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME', pay_employee_master.employee_type,DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',original_bank_account_no,PF_BANK_NAME,REJOIN_DATE, date_format(left_date,'%d/%m/%Y') as left_date  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code where P_TAX_NUMBER='" + txt_search_adhar.Text + "' AND pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' order by left_date", d.con1);
            //
            d.con1.Open();
            adp.Fill(dt_item);

            if (dt_item.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Adharcard Number Employee Not Found');", true);
                panel_adhar_card.Visible = true;
                btn_adhar_add_emp.Visible = true;
                gv_search_adharcardno.DataSource = null;
                gv_search_adharcardno.DataBind();
                Panel3.Visible = false;

                return;
            }
            else
            {
                Panel3.Visible = true;
                reason_panel.Visible = false;
                gv_search_adharcardno.DataSource = dt_item;
                gv_search_adharcardno.DataBind();
            }
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con1.Close();

        }
    }

    protected void gv_search_adharcardno_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_search_adharcardno.UseAccessibleHeader = false;
            gv_search_adharcardno.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void gv_search_adharcardno_SelectedIndexChanged(object sender, EventArgs e)
    {
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_permstate.Items.Add(dr_item1[0].ToString());
                ddl_state.Items.Add(dr_item1[0].ToString());
                //  ddl_location.Text=(dr_item1[0].ToString());
            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }



        string unit_code1 = "";
        string emp_code = gv_search_adharcardno.SelectedRow.Cells[0].Text;
        string unit_code = gv_search_adharcardno.SelectedRow.Cells[2].Text;
        d.con.Open();
        try
        {
            MySqlCommand cmd1 = new MySqlCommand("select unit_code from pay_unit_master where unit_name='" + unit_code + "' ", d.con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                unit_code1 = dr1.GetValue(0).ToString();
            }
            dr1.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


        d_shift.con.Open();
        try
        {
            MySqlCommand cmd_adhar = new MySqlCommand("select LEFT_DATE,LEFT_REASON from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and P_TAX_NUMBER='" + txt_search_adhar.Text + "' and unit_code='" + unit_code1 + "' and emp_code='" + emp_code + "' AND (left_date IS NOT NULL OR left_date != '') ", d_shift.con);
            MySqlDataReader dr_adhar = cmd_adhar.ExecuteReader();
            if (dr_adhar.Read())
            {

                //Session["get_employee_details"] =(gv_search_adharcardno.SelectedRow.Cells[0].Text);
                getEmployee(gv_search_adharcardno.SelectedRow.Cells[0].Text);
                panel_adhar_card.Visible = false;
                newpanel.Visible = true;
                panel_adhar_card.Visible = false;
            }
            else
            {
                //panel_adhar_card.Visible = true;
                //btn_adhar_add_emp.Visible = tru
                pln_searchemp.Visible = true;
                panel_adhar_card.Visible = false;
                pnl_buttons.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Adharcard Number Emplyee All Ready Found');", true);
                return;


            }
            d_shift.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d_shift.con.Close(); }


    }
    public int left_date = 0;
    protected void gv_search_adharcardno_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.Cells[0].Text != "")
            {
                //if (left_date == 1)
                //{
                //    try
                //    {
                LinkButton lb4 = e.Row.FindControl("link") as LinkButton;
                lb4.Visible = true;
                //    }
                //    catch { }
                //}
                //else if (e.Row.Cells[0].Text != "LEFT DATE")
                //{
                //    left_date = 1;
                //}

            }
            else
            {
                ////left_date = 1;
                ////try
                ////{
                LinkButton lb4 = e.Row.FindControl("link") as LinkButton;
                lb4.Visible = false;
                ////}
                ////catch { }
            }
        }
        e.Row.Cells[1].Visible = false;
    }

    //protected void btn_Continue_Click(object sender, EventArgs e)
    //{

    //    string emp_code = gv_search_adharcardno.SelectedRow.Cells[1].Text;
    //    string unit_code = gv_search_adharcardno.SelectedRow.Cells[2].Text;
    //    d.con.Open();
    //    MySqlCommand cmd = new MySqlCommand("select LEFT_DATE,LEFT_REASON from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and P_TAX_NUMBER='" + txt_search_adhar.Text + "' and unit_code='" + unit_code + "' and emp_code='" + emp_code + "' ", d.con);
    //    MySqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.Read())
    //    {
    //        getEmployee(gv_search_adharcardno.SelectedRow.Cells[0].Text);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Adharcard Number Emplyee All Ready Found');", true);

    //        return;
    //    }

    //}

    protected void btn_btn_rejoin(object sender, EventArgs e)
    {
        GridViewRow row = gv_search_adharcardno.SelectedRow;
        string emp_code = row.Cells[1].Text;
        if (emp_code != "")
        {
            getEmployee(emp_code);
        }
    }

    protected void btn_add_employee_click(object sender, EventArgs e)
    {
        panel_adhar_card.Visible = true;
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        //  approval_grid();
        text_Clear();
        //load_reporting_grdv();
        btn_add.Visible = true;
        //btn_approval.Visible = false;
        //btn_reject.Visible = false;
        btnupdate.Visible = false;
        btndelete.Visible = false;
    }
    protected void size()
    {

        if (ddl_product_type.SelectedValue == "1")
        {

            try
            {
                uniform_size.Items.Clear();
                MySqlCommand cmd_cust = new MySqlCommand("SELECT distinct ( size) FROM pay_item_master where product_service='Uniform' and size is not null", d1.con);

                d1.con.Open();

                MySqlDataReader dr_item1 = cmd_cust.ExecuteReader();
                while (dr_item1.Read())
                {
                    uniform_size.Items.Add(dr_item1[0].ToString());
                }
                uniform_size.Items.Insert(0, "Select");
                dr_item1.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }


        }
        else if (ddl_product_type.SelectedValue == "10")
        {
            try
            {
                uniform_size.Items.Clear();
                MySqlCommand cmd_cust2 = new MySqlCommand("SELECT distinct ( size) FROM pay_item_master where product_service='pantry_jacket' and  size is not null", d1.con);

                d1.con.Open();

                MySqlDataReader dr_item12 = cmd_cust2.ExecuteReader();
                while (dr_item12.Read())
                {
                    uniform_size.Items.Add(dr_item12[0].ToString());
                }
                uniform_size.Items.Insert(0, "Select");
                dr_item12.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }

        }
        else if (ddl_product_type.SelectedValue == "2")
        {
            try
            {
                uniform_size.Items.Clear();
                MySqlCommand cmd_cust1 = new MySqlCommand("SELECT distinct ( size) FROM pay_item_master where product_service='Shoes' and size is not null", d1.con);

                d1.con.Open();

                MySqlDataReader dr_item11 = cmd_cust1.ExecuteReader();
                while (dr_item11.Read())
                {
                    uniform_size.Items.Add(dr_item11[0].ToString());
                }
                uniform_size.Items.Insert(0, "Select");
                dr_item11.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }

        else if (ddl_product_type.SelectedValue == "11")
        {
            try
            {
                uniform_size.Items.Clear();
                MySqlCommand cmd_cust1 = new MySqlCommand("SELECT distinct (size) FROM pay_item_master where product_service='Apron' and size is not null", d1.con);

                d1.con.Open();

                MySqlDataReader dr_item11 = cmd_cust1.ExecuteReader();
                while (dr_item11.Read())
                {
                    uniform_size.Items.Add(dr_item11[0].ToString());
                }
                uniform_size.Items.Insert(0, "Select");
                dr_item11.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }

        else
        {
            uniform_size.Items.Clear();
            uniform_size.Items.Insert(0, "Select");
        }

    }

    protected void qty_sum()
    {
        if (ddl_product_type.SelectedValue == "1")
        {
            try
            {
                MySqlCommand cmd_cust = new MySqlCommand("SELECT distinct  (SELECT SUM(QUANTITY) FROM pay_transactionp_details WHERE item_type = 'Uniform' and size_uniform='" + uniform_size.SelectedValue + "') - (SELECT ifnull(SUM(quantity),0) FROM pay_transaction_details WHERE item_type = 'Uniform' and size_uniform='" + uniform_size.SelectedValue + "') AS 'abc' FROM pay_item_master", d1.con);

                d1.con.Open();
                MySqlDataReader dr_cust = cmd_cust.ExecuteReader();
                if (dr_cust.Read())
                {
                    lbl_qty.Text = dr_cust.GetValue(0).ToString();
                    if (lbl_qty.Text == "")
                    {
                        lbl_qty.Text = "0";
                    }
                    txt_quantity1.Visible = true;
                    lbl_qty.Visible = true;
                }
                dr_cust.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }
        if (ddl_product_type.SelectedValue == "2")
        {
            try
            {
                MySqlCommand cmd_cust = new MySqlCommand("SELECT distinct  (SELECT SUM(QUANTITY) FROM pay_transactionp_details WHERE item_type = 'Shoes' and size_shoes='" + uniform_size.SelectedValue + "') - (SELECT ifnull(SUM(quantity),0) FROM pay_transaction_details WHERE item_type = 'Shoes' and size_shoes='" + uniform_size.SelectedValue + "') AS 'abc' FROM pay_item_master", d1.con);

                d1.con.Open();
                MySqlDataReader dr_cust = cmd_cust.ExecuteReader();
                if (dr_cust.Read())
                {
                    lbl_qty.Text = dr_cust.GetValue(0).ToString();
                    if (lbl_qty.Text == "")
                    {
                        lbl_qty.Text = "0";
                    }
                    txt_quantity1.Visible = true;
                    lbl_qty.Visible = true;
                }
                dr_cust.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }
        if (ddl_product_type.SelectedValue == "10")
        {
            try
            {
                MySqlCommand cmd_cust = new MySqlCommand("SELECT distinct  (SELECT SUM(QUANTITY) FROM pay_transactionp_details WHERE item_type = 'pantry_jacket' and size_pantry='" + uniform_size.SelectedValue + "') - (SELECT ifnull(SUM(quantity),0) FROM pay_transaction_details WHERE item_type = 'pantry_jacket' and size_pantry='" + uniform_size.SelectedValue + "') AS 'abc' FROM pay_item_master", d1.con);

                d1.con.Open();
                MySqlDataReader dr_cust = cmd_cust.ExecuteReader();
                if (dr_cust.Read())
                {
                    lbl_qty.Text = dr_cust.GetValue(0).ToString();
                    if (lbl_qty.Text == "")
                    {
                        lbl_qty.Text = "0";
                    }
                    txt_quantity1.Visible = true;
                    lbl_qty.Visible = true;
                }
                dr_cust.Close();
                d1.con.Close();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }
    }
    protected void uniform_size_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        newpanel.Visible = true;
        qty_sum();
    }
    protected void ddl_adhar_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_adhar_search.SelectedValue == "northeast")
        {
            lable_search.Visible = false;
            txt_search_adhar.Visible = false;
            btn_search.Visible = false;
            btn_adhar_add_emp.Visible = true;
            Panel3.Visible = false;
        }
        else
        {
            lable_search.Visible = true;
            txt_search_adhar.Visible = true;
            btn_search.Visible = true;
            btn_adhar_add_emp.Visible = false;
            Panel3.Visible = true;
        }
        panel_adhar_card.Visible = true;
    }

    protected bool chk_uniform()
    {
        // if (ddl_grade.SelectedValue.Equals("RUN")) { return false; }

        if (ddl_unit_client.SelectedValue == "Staff" || ddl_unit_client.SelectedValue == "IHMS" || ddl_unit_client.SelectedValue == "BAGICTM" || ddl_employee_type.SelectedValue != "Permanent" || ddl_employee_type.SelectedValue == "Reliever") { return false; }
        string[] document = new string[4];
        int i = 0;
        foreach (GridViewRow row in gv_product_details.Rows)
        {
            if (row.Cells[2].Text == "Uniform" || row.Cells[2].Text == "Shoes" || row.Cells[2].Text == "Apron")
            {
                document[i] = row.Cells[2].Text;
                i++;
            }

        }
        if (document[0] == null || document[1] == null)
        {
            return true;
        }
        return false;
    }

    protected bool chk_IDCard()
    {
        // if (ddl_grade.SelectedValue.Equals("RUN")) { return false; }

        if (ddl_unit_client.SelectedValue == "Staff" || ddl_unit_client.SelectedValue == "IHMS" || ddl_unit_client.SelectedValue == "BAGICTM" || ddl_employee_type.SelectedValue != "Permanent" || ddl_employee_type.SelectedValue == "Reliever") { return false; }
        string[] document = new string[4];
        int i = 0;
        foreach (GridViewRow row in gv_product_details.Rows)
        {
            if (row.Cells[2].Text == "ID_Card" )
            {
                document[i] = row.Cells[2].Text;
                i++;
            }

        }
        if (document[0] == null)
        {
            return true;
        }
        return false;
    }




    //public string branch_list { get; set; }

    public void text_Clear_new_employee()
    {
        txt_id_as_per_dob.Text = "";
        txt_eecode.Text = "";
        txt_reason_updation.Text = "";
        ddl_department.SelectedValue = "Select";
        txt_originalbankaccountno.Text = "";
        ddl_clientwisestate.Items.Clear();
        txt_eename.Text = "";
        txt_eefatharname.Text = "";

        txtreasonforleft.Text = "";
        // txtautoattendancecode.Text = "";

        ddl_relation.SelectedIndex = 0;
        txt_birthdate.Text = "";
        ddl_gender.SelectedIndex = 0;
        txt_ihmscode.Text = "";

        gv_product_details.DataSource = null;
        gv_product_details.DataBind();

        //   ddl_unitcode.SelectedIndex = 0;
        //ddl_dept.SelectedIndex = 0;

        ddl_grade_SelectedIndexChanged(null, null);
        ddl_grade.SelectedIndex = 0;
        txt_presentaddress.Text = "";
        txt_presentcity.SelectedIndex = 0;
        ddl_state.SelectedIndex = 0;
        txt_presentpincode.Text = "";
        txtrefname1.Text = "";
        txtrefname2.Text = "";

        txt_permanantaddress.Text = "";
        //  txt_permanantcity.Items.Clear();
        txt_permanantcity.SelectedIndex = 0;
        ddl_permstate.SelectedIndex = 0;
        txt_permanantpincode.Text = "";
        txtref1mob.Text = "";
        txtref2mob.Text = "";
        txt_address2.Text = "";
        txt_address1.Text = "";
        txt_mobilenumber.Text = "";
        txt_residencecontactnumber.Text = "";
        txt_confirmationdate.Text = "";
        txt_joiningdate.Text = "";
        txt_advance_payment.Text = "";

        ddl_location.Text = "";
        ddl_location_city.Text = "";

        ddl_ptaxdeductionflag.SelectedIndex = 1;
        txt_leftdate.Text = "";
        txtreasonforleft.Text = "";
        ddlpfregisteremp.SelectedIndex = 1;
        txt_pfbankname.Text = "";
        txt_pfnomineename.Text = "";
        txt_pfifsccode.Text = "";
        txt_pfnomineerelation.Text = "";
        txt_employeeaccountnumber.Text = "";
        txt_pfbdate.Text = "";
        //ddl_bankcode.SelectedIndex =;
        // ddl_MaritalStatus.SelectedValue = "Select";
        ddl_bloodgroup.SelectedIndex = 0;
        txt_hobbies.Text = "";

        txt_weight.Text = "0";
        ddl_religion.SelectedValue = "Select";

        txt_height.Text = "0";
        if (ddl_reporting_to.SelectedIndex == -1)
        {
            //ddl_reporting_to.SelectedIndex = 0;
        }
        else
        {
            ddl_reporting_to.SelectedIndex = 0;
        }

        txtsearchempid.Text = "";
        txt_email.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
        txt_ranteagrement_satrtdate.Text = "";
        txt_ranteagrement_enddate.Text = "";
        // txt_Nationality.Text = "";
        txt_Identitymark.Text = "";
        ddl_Mother_Tongue.Text = "";
        txt_Passport_No.Text = "";
        ddl_Visa_Country.SelectedIndex = 0;
        txt_Driving_License_No.Text = "";
        txt_Mise.Text = "";
        txt_Place_Of_Birth.Text = "";
        txt_Language_Known.Text = "";
        txt_Area_Of_Expertise.Text = "";
        txt_Passport_Validity_Date.Text = "";
        txt_Visa_Validity_Date.Text = "";
        txt_Details_Of_Handicap.Text = "";
        txt_qualification_1.Text = "";
        txt_year_of_passing_1.Text = "";
        txt_qualification_2.Text = "";
        txt_year_of_passing_2.Text = "";
        txt_qualification_3.Text = "";
        txt_year_of_passing_3.Text = "";
        txt_qualification_4.Text = "";
        txt_year_of_passing_4.Text = "";
        txt_qualification_5.Text = "";
        txt_year_of_passing_5.Text = "";
        txt_key_skill_1.Text = "";
        txt_experience_in_months_1.Text = "";
        txt_key_skill_2.Text = "";
        txt_experience_in_months_2.Text = "";
        txt_key_skill_3.Text = "";
        txt_experience_in_months_3.Text = "";
        txt_key_skill_4.Text = "";
        txt_experience_in_months_4.Text = "";
        txt_key_skill_5.Text = "";
        txt_experience_in_months_5.Text = "";
        //--family Details ---
        txt_name1.Text = "";
        txt_relation1.Text = "Father";
        txt_dob1.Text = "";
        txt_pan1.Text = "";
        txt_adhaar1.Text = "";
        txt_name2.Text = "";
        txt_relation2.Text = "Mother";
        txt_dob2.Text = "";
        txt_pan2.Text = "";
        txt_adhaar2.Text = "";
        txt_name3.Text = "";
        txt_relation3.Text = "Wife";
        txt_dob3.Text = "";
        txt_pan3.Text = "";
        txt_adhaar3.Text = "";
        txt_name4.Text = "";
        txt_relation4.Text = "Child";
        txt_dob4.Text = "";
        txt_pan4.Text = "";
        txt_adhaar4.Text = "";
        txt_name5.Text = "";
        txt_relation5.Text = "Child";
        txt_dob5.Text = "";
        txt_pan5.Text = "";
        txt_adhaar5.Text = "";
        txt_name6.Text = "";
        txt_relation6.Text = "Child";
        txt_dob6.Text = "";
        txt_pan6.Text = "";
        txt_adhaar6.Text = "";
        txt_name7.Text = "";
        txt_relation7.Text = "Child";
        txt_dob7.Text = "";
        txt_pan7.Text = "";
        txt_adhaar7.Text = "";
        Numberchild.Text = "0";
        ddl_unit_client.SelectedValue = "Select";
        ddl_unitcode.Items.Clear();
        txt_emailid1.Text = "";
        txt_emailid2.Text = "";
        try
        {
            DropDownList1.SelectedIndex = 0;
        }
        catch { }
        txt_bankholder.Text = "";
        ddl_bankcode.Text = "";
        ddl_infitcode.SelectedValue = "Select";
        //Txt_cca.Text = "0";
        //Txt_gra.Text = "0";
        //Txt_allow.Text = "0";
        txt_fine.Text = "0";
        ddl_employee_type.SelectedIndex = 0;
        ddl_location.Text = "";
        Image4.ImageUrl = "~/Images/placeholder.png";
        Image1.ImageUrl = "~/Images/pan.jpg";
        Image10.ImageUrl = "~/Images/certificate.jpg";
        Image11.ImageUrl = "~/Images/certificate.jpg";
        Image12.ImageUrl = "~/Images/certificate.jpg";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image3.ImageUrl = "~/Images/Biodata.png";
        Image5.ImageUrl = "~/Images/Passport.jpg";
        Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        Image7.ImageUrl = "~/Images/marksheet.jpg";
        Image8.ImageUrl = "~/Images/marksheet.jpg";
        Image9.ImageUrl = "~/Images/certificate.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
        Image15.ImageUrl = "~/Images/certificate.jpg";
        Image16.ImageUrl = "~/Images/certificate.jpg";
        Image17.ImageUrl = "~/Images/certificate.jpg";
        Image18.ImageUrl = "~/Images/certificate.jpg";
        Image19.ImageUrl = "~/Images/certificate.jpg";
        Image20.ImageUrl = "~/Images/certificate.jpg";
        Image21.ImageUrl = "~/Images/certificate.jpg";
        Image22.ImageUrl = "~/Images/certificate.jpg";
        Image23.ImageUrl = "~/Images/certificate.jpg";
        Image24.ImageUrl = "~/Images/certificate.jpg";
        Image25.ImageUrl = "~/Images/certificate.jpg";
        //txt_attendanceid.SelectedValue = "0";

    }
    protected void gv_employee_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

    }
    //vikas add 08/05/2019
    //protected void button_false()
    //{
    //}

    //chaitali(rejoin) 18-06-19
    protected void Link_Click(object sender, EventArgs e)
    {
        ViewState["rejoin_update"] = "1";
        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string name = row.Cells[6].Text;
        string emp_id = row.Cells[1].Text;
        System.Web.UI.WebControls.TextBox rejoin_date1 = row.FindControl("txt_rejoin_date") as System.Web.UI.WebControls.TextBox;
        string rejoin_date = rejoin_date1.Text;
        if (rejoin_date.Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter Rejoin Date.')", true);
            panel_adhar_card.Visible = true;
            return;
        }

        //string left_date = d.getsinglestring("select DATE_FORMAT(left_date, '%d/%m/%Y') from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_id + "'");
        string left_date = row.Cells[0].Text;
        if (!left_date.Equals(""))
        {
            DateTime current_date = DateTime.ParseExact(left_date, "dd/MM/yyyy", null);
            DateTime rejoin = DateTime.ParseExact(rejoin_date, "dd/MM/yyyy", null);
            if (current_date >= rejoin)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please Enter rejoining date greater than Left Date " + left_date + "')", true);
                panel_adhar_card.Visible = true;
                return;
            }
        }

        try
        {


            if (name == "Permanent")
            {
                btn_approve.Visible = true;
            }
            else
            {
                btn_approve.Visible = false;
            }
            images_visibility();
            ViewState["rejoin_date"] = rejoin_date;
            getEmployee(emp_id);
            btnnew_Click();
            txt_leftdate.Text = "";
            txtreasonforleft.Text = "";
            //d.operation("update pay_employee_master set rejoin_flag=1 where emp_code='" + emp_id + "' ");
            ViewState["rejoin_empcode"] = emp_id;
            string abc = ViewState["rejoin_empcode"].ToString();

            photo_image_clear();
            d.operation("update pay_employee_master set rejoin_flag=1 where emp_code='" + emp_id + "' ");
            // string cmd_rejoin = ViewState["rejoin_date"].ToString();
        }
        catch
        {

        }
        finally
        {
            string legal_flag = "";
            if (d.getsinglestring("select employee_type from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + txt_eecode.Text + "'").Contains("Permanent"))
            {
                legal_flag = d.getsinglestring("select legal_flag from pay_employee_master where EMP_CODE ='" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'");
                if (legal_flag.Equals("0") || legal_flag.Equals("3"))
                {
                    visiblity_true();
                    btnupdate.Visible = true;
                    btndelete.Visible = true;
                    btn_approve.Visible = true;
                    btnUpload.Visible = true;
                    ViewState["permanent"] = "1";
                    bank_detail_visibility(1);

                }
                else
                {
                    visiblity();
                    btnupdate.Visible = false;
                    btndelete.Visible = false;
                    btn_approve.Visible = false;
                    btncloselow.Visible = false;
                    btnUpload.Visible = false;
                    bank_detail_visibility(0);
                }
            }
            else
            {
                visiblity_true();
                btnupdate.Visible = false;
                //btnupdate.Visible = true;
                btndelete.Visible = true;
                Button1.Visible = true;
                //  Button1.Visible = true;
                ViewState["id"] = "1";
                btnUpload.Visible = true;
                btn_releiving_letter.Visible = false;
                
                ViewState["permanent"] = "0";
            }
            ViewState["id"] = "1";
        }
        newpanel.Visible = true;
        reason_panel.Visible = false;
        ////ddl_employee_type.Enabled = false;
        //ddl_department.Enabled = false;
        //ddl_unit_client.Enabled = false;
        //ddl_clientwisestate.Enabled = false;
        //ddl_unitcode.Enabled = false;
        //ddl_grade.Enabled = false;
        //txt_eecode.Enabled = false;
        //txt_eename.Enabled = false;

        //vikas add 03/04/2019
        select_designation.SelectedValue = "0";
        ddl_product_type.SelectedValue = "0";
        ddl_uniformset.SelectedValue = "0";
        uniform_size.SelectedValue = "Select";
        // lbl_qty.Text = "";
        txt_quantity1.Visible = false;
        uniform_issue_date.Text = "";
        uniform_expiry_date.Text = "";

    }

    private void load_images(string l_EMP_CODE)
    {
        if (d.con1.State == ConnectionState.Open)
        {
            d.con1.Close();
            d.con1.Dispose();
            d.con1.ClearPoolAsync(d.con1);
        }
        d.con1.Open();
        try
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_PHOTO,EMP_ADHAR_PAN,EMP_BANK_STATEMENT,EMP_BIODATA,EMP_PASSPORT,EMP_DRIVING_LISCENCE,EMP_10TH_MARKSHEET,EMP_12TH_MARKSHEET,EMP_DIPLOMA_CERTIFICATE,EMP_DEGREE_CERTIFICATE,EMP_POST_GRADUATION_CERTIFICATE,EMP_EDUCATION_CERTIFICATE,POLICE_VERIFICATION_DOC,FormNo_2,FormNo_11,present_add_proof,noc_form,medical_form ,emp_signature,original_photo,original_adhar_card,original_policy_document,original_address_proof,original_address_proof,bank_passbook,itc_upload_form FROM pay_images_master WHERE EMP_CODE='" + l_EMP_CODE + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                //////--------------------------------------EMP_PHOTO-----------------------------------------------------

                if (dr.GetValue(0).ToString() != "")
                {

                    Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();

                    var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_photo.Visible = true;
                        Image4.ImageUrl = "~/Images/pdf_format.jpg";

                    }
                }
                else
                {
                    Image4.ImageUrl = "~/Images/placeholder.png";
                }

                //////--------------------------------EMP_ADHAR_PAN----------------------------------------------------------

                if (dr.GetValue(1).ToString() != "")
                {

                    Image1.ImageUrl = "~/EMP_Images/" + dr.GetValue(1).ToString();

                    var result = dr.GetValue(1).ToString().Substring(dr.GetValue(1).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_adhar_pan_upload.Visible = true;
                        Image1.ImageUrl = "~/Images/pdf_format.jpg";

                    }
                }
                else
                {
                    Image1.ImageUrl = "~/Images/pan.jpg";
                }
                //////----------------------------------EMP_BANK_STATEMENT---------------------------------------------------

                if (dr.GetValue(2).ToString() != "")
                {

                    Image2.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
                    var result = dr.GetValue(2).ToString().Substring(dr.GetValue(2).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_bank_upload.Visible = true;
                        Image2.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image2.ImageUrl = "~/Images/passbook.jpg";
                }

                //////-----------------------------EMP_BIODATA--------------------------------------------------------

                if (dr.GetValue(3).ToString() != "")
                {
                    Image3.ImageUrl = "~/EMP_Images/" + dr.GetValue(3).ToString();

                    var result = dr.GetValue(3).ToString().Substring(dr.GetValue(3).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_biodata_upload.Visible = true;
                        Image3.ImageUrl = "~/Images/pdf_format.jpg";

                    }
                }
                else
                {
                    Image3.ImageUrl = "~/Images/Biodata.png";
                }

                //////------------------------------EMP_PASSPORT-------------------------------------------------------

                if (dr.GetValue(4).ToString() != "")
                {
                    Image5.ImageUrl = "~/EMP_Images/" + dr.GetValue(4).ToString();

                    var result = dr.GetValue(4).ToString().Substring(dr.GetValue(4).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Passport_upload.Visible = true;
                        Image5.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image5.ImageUrl = "~/Images/Passport.jpg";
                }
                //////------------------------------------EMP_DRIVING_LISCENCE-------------------------------------------------
                if (dr.GetValue(5).ToString() != "")
                {
                    Image6.ImageUrl = "~/EMP_Images/" + dr.GetValue(5).ToString();
                    var result = dr.GetValue(5).ToString().Substring(dr.GetValue(5).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Driving_Liscence_upload.Visible = true;
                        Image6.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
                }
                //////------------------------------------EMP_10TH_MARKSHEET-------------------------------------------------
                if (dr.GetValue(6).ToString() != "")
                {
                    Image7.ImageUrl = "~/EMP_Images/" + dr.GetValue(6).ToString();
                    var result = dr.GetValue(6).ToString().Substring(dr.GetValue(6).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Tenth_Marksheet_upload.Visible = true;
                        Image7.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image7.ImageUrl = "~/Images/marksheet.jpg";
                }
                //////------------------------------------EMP_12TH_MARKSHEET-------------------------------------------------
                if (dr.GetValue(7).ToString() != "")
                {
                    Image8.ImageUrl = "~/EMP_Images/" + dr.GetValue(7).ToString();
                    var result = dr.GetValue(7).ToString().Substring(dr.GetValue(7).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Twelve_Marksheet_upload.Visible = true;
                        Image8.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image8.ImageUrl = "~/Images/marksheet.jpg";
                }
                //////-------------------------------------EMP_DIPLOMA_CERTIFICATE------------------------------------------------
                if (dr.GetValue(8).ToString() != "")
                {
                    Image9.ImageUrl = "~/EMP_Images/" + dr.GetValue(8).ToString();
                    var result = dr.GetValue(8).ToString().Substring(dr.GetValue(8).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Diploma_upload.Visible = true;
                        Image9.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image9.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_DEGREE_CERTIFICATE------------------------------------------------
                if (dr.GetValue(9).ToString() != "")
                {
                    Image10.ImageUrl = "~/EMP_Images/" + dr.GetValue(9).ToString();
                    var result = dr.GetValue(9).ToString().Substring(dr.GetValue(9).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Degree_upload.Visible = true;
                        Image10.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image10.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_POST_GRADUATION_CERTIFICATE------------------------------------------------
                if (dr.GetValue(10).ToString() != "")
                {
                    Image11.ImageUrl = "~/EMP_Images/" + dr.GetValue(10).ToString();
                    var result = dr.GetValue(10).ToString().Substring(dr.GetValue(10).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Post_Graduation_upload.Visible = true;
                        Image11.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image11.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_EDUCATION_CERTIFICATE------------------------------------------------
                if (dr.GetValue(11).ToString() != "")
                {
                    Image12.ImageUrl = "~/EMP_Images/" + dr.GetValue(11).ToString();
                    var result = dr.GetValue(11).ToString().Substring(dr.GetValue(11).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Education_4_upload.Visible = true;
                        Image12.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image12.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(12).ToString() != "")
                {
                    Image14.ImageUrl = "~/EMP_Images/" + dr.GetValue(12).ToString();
                    var result = dr.GetValue(12).ToString().Substring(dr.GetValue(12).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Police_document.Visible = true;
                        Image14.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image14.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(13).ToString() != "")
                {
                    Image15.ImageUrl = "~/EMP_Images/" + dr.GetValue(13).ToString();
                    var result = dr.GetValue(13).ToString().Substring(dr.GetValue(13).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Formno_2.Visible = true;
                        Image15.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image15.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(14).ToString() != "")
                {
                    Image16.ImageUrl = "~/EMP_Images/" + dr.GetValue(14).ToString();
                    var result = dr.GetValue(14).ToString().Substring(dr.GetValue(14).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Formno_11.Visible = true;
                        Image16.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image16.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(15).ToString() != "")
                {
                    Image17.ImageUrl = "~/EMP_Images/" + dr.GetValue(15).ToString();
                    var result = dr.GetValue(15).ToString().Substring(dr.GetValue(15).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_Address_Proof.Visible = true;
                        Image17.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image17.ImageUrl = "~/Images/Biodata.png";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(16).ToString() != "")
                {
                    Image18.ImageUrl = "~/EMP_Images/" + dr.GetValue(16).ToString();
                    var result = dr.GetValue(16).ToString().Substring(dr.GetValue(16).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_noc_form.Visible = true;
                        Image18.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image18.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(17).ToString() != "")
                {
                    Image19.ImageUrl = "~/EMP_Images/" + dr.GetValue(17).ToString();
                    var result = dr.GetValue(17).ToString().Substring(dr.GetValue(17).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_medical_form.Visible = true;
                        Image19.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image19.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(18).ToString() != "")
                {
                    Image20.ImageUrl = "~/EMP_Images/" + dr.GetValue(18).ToString();
                    var result = dr.GetValue(18).ToString().Substring(dr.GetValue(18).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_emp_signature.Visible = true;
                        Image20.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image20.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(19).ToString() != "")
                {
                    Image21.ImageUrl = "~/EMP_Images/" + dr.GetValue(19).ToString();
                    var result = dr.GetValue(19).ToString().Substring(dr.GetValue(19).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_originalphoto.Visible = true;
                        Image21.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image21.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(20).ToString() != "")
                {
                    Image22.ImageUrl = "~/EMP_Images/" + dr.GetValue(20).ToString();

                    var result = dr.GetValue(20).ToString().Substring(dr.GetValue(20).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_original_adharcard_upload.Visible = true;
                        Image22.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image22.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(21).ToString() != "")
                {
                    Image23.ImageUrl = "~/EMP_Images/" + dr.GetValue(21).ToString();

                    var result = dr.GetValue(21).ToString().Substring(dr.GetValue(21).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_original_police_document.Visible = true;
                        Image23.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image23.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(22).ToString() != "")
                {
                    Image24.ImageUrl = "~/EMP_Images/" + dr.GetValue(22).ToString();
                    var result = dr.GetValue(22).ToString().Substring(dr.GetValue(22).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_original_Address_Proof.Visible = true;
                        Image24.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image24.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(24).ToString() != "")
                {
                    Image25.ImageUrl = "~/EMP_Images/" + dr.GetValue(24).ToString();
                    var result = dr.GetValue(24).ToString().Substring(dr.GetValue(24).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        link_bank_passbook.Visible = true;
                        Image25.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    Image25.ImageUrl = "~/Images/certificate.jpg";
                }
                //////////////////////-------------------------

                if (dr.GetValue(25).ToString() != "")
                {
                    itc_img_btn.ImageUrl = "~/EMP_Images/" + dr.GetValue(25).ToString();
                    var result = dr.GetValue(25).ToString().Substring(dr.GetValue(25).ToString().Length - 4);
                    if (result == ".pdf")
                    {
                        itc_link_btn.Visible = true;
                        itc_img_btn.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    itc_img_btn.ImageUrl = "~/Images/certificate.jpg";
                }

            }
            dr.Close();
            cmd1.Dispose();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    protected void btn_releiving_letter_Click(object sender, EventArgs e)
    {
        try
        {
            //Creating PDF
            string downloadname = "Relieving_Letter_" + txt_eecode.Text;
            crystalReport.Load(Server.MapPath("~/relive_letter.rpt"));
            string headerimagepath1 = null;
            string footerimagepath1 = null;
            string footerimagepath2 = null;
            // string stamppath = null;
            if (Session["COMP_CODE"].ToString() == "C02")
            {
                // crystalReport.Load(Server.MapPath("~/relive_letter.rpt"));  C02_stamp.png
                headerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header.png");
                footerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
                footerimagepath2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath1"].Text = @"'" + footerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath2"].Text = @"'" + footerimagepath2 + "'";
            }
            else
            {
                // crystalReport.Load(Server.MapPath("~/relive_letter.rpt")); C01_stamp.JPG 
                headerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header.png");
                footerimagepath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.JPG");
                footerimagepath2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer.png");
                crystalReport.DataDefinition.FormulaFields["headerimagepath1"].Text = @"'" + headerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath1"].Text = @"'" + footerimagepath1 + "'";
                crystalReport.DataDefinition.FormulaFields["footerimagepath2"].Text = @"'" + footerimagepath2 + "'";
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT a.comp_code,a.EMP_NAME,date_format(a.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',a.EMP_CODE,pay_grade_master.GRADE_DESC AS GRADE_CODE, date_format(a.LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE',(select emp_name from pay_employee_master b where b.emp_code = a.REPORTING_TO) as REPORTING_TO, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2,pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE,pay_company_master.COMPANY_CONTACT_NO as COMPANY_CONTACT_NO FROM pay_employee_master a INNER JOIN pay_company_master ON a.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON a.GRADE_CODE =pay_grade_master.GRADE_CODE WHERE a.EMP_CODE='" + txt_eecode.Text + "'AND a.comp_code='" + Session["comp_code"].ToString() + "' and a.employee_type = 'Permanent'");
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            try
            {
                sda = cmd.ExecuteReader();
                dt.Load(sda);
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Should be Permanent!!');", true);
                return;
            }
            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
            crystalReport.Close();
            crystalReport.Clone();
            crystalReport.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Sending Email
            string body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir/Madam, <p>Please find the attached Releiving letter of " + txt_eename.Text + ". </FONT></FONT></FONT></B><p>";
            d.con.Open();
            System.Data.DataTable DataTable1 = new System.Data.DataTable();
            try
            {
                MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;select cast(group_concat(distinct head_email_id) as char), head_name, client_code,comp_code,state from pay_branch_mail_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "'", d.con);
                MySqlDataReader drnew = cmdnew.ExecuteReader();
                DataTable1.Load(drnew);
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            foreach (DataRow row in DataTable1.Rows)
            {
                if (!row[0].ToString().Equals(""))
                {
                    //  mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "IH&MS - Salary Slip of " + dr.GetValue(4).ToString() + " for the month of " + month_year, dr.GetValue(5).ToString(), dr.GetValue(1).ToString(), month, year, counter1, body);
                    string from_emailid = "";
                    string password = "";
                    d.con.Open();
                    try
                    {
                        cmd = new MySqlCommand("select email_id,password from pay_client_master where client_code = '" + row[2].ToString() + "' ", d.con);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            from_emailid = dr.GetValue(0).ToString();
                            password = dr.GetValue(1).ToString();
                        }
                        dr.Close();
                        d.con.Close();
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con.Close(); }
                    if (!(from_emailid == "") || !(password == ""))
                    {
                        string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Admin' and client_code='" + row[2].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                        body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />" + name + "";


                        using (MailMessage mailMessage = new MailMessage())
                        {
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            mailMessage.From = new MailAddress(from_emailid);

                            if (row[0].ToString() != "")
                            {
                                mailMessage.To.Add(row[0].ToString());
                                mailMessage.Subject = "IH&MS - Relieving Letter of " + txt_eename.Text;
                                mailMessage.Body = body;
                                mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf"));
                                mailMessage.IsBodyHtml = true;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                                SmtpServer.EnableSsl = true;
                                try
                                {
                                    SmtpServer.Send(mailMessage);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Relieving Email Sent successfully!!');", true);
                                    reason_panel.Visible = false;
                                }
                                catch
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Relieving Email Not Sent!!');", true);
                                }
                                // File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + downloadname + ".pdf");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); 
        }
    }

    // komal 26-05-2020 
    protected void btn_dispatch_details_Click(object sender, EventArgs e)
    {

        gv_dispatch_details.DataSource = null;
        gv_dispatch_details.DataBind();

        try {
            d.con1.Open();

            string where = "";

            if (ddl_emp_type.SelectedValue=="1")
            {
                where = "pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "'  AND `receiver_type` = '1' and pay_employee_master.left_date IS Null   group by pay_dispatch_billing.emp_code";
            }
            else if (ddl_emp_type.SelectedValue == "2")
            {
                where = " pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' AND `receiver_type` = '1' and pay_employee_master.left_date IS NOT Null   group by pay_dispatch_billing.emp_code";
            
            }

            DataSet ds = new DataSet();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT distinct`pay_dispatch_billing`.`id`,  `client_name` , `state_dispatch`, IFNULL(`pay_unit_master`.`unit_name`, `pay_dispatch_billing`.`unit_code`) AS 'unit_name',  IFNULL(`pay_employee_master`.`emp_name`, `pay_dispatch_billing`.`emp_code`) AS 'emp_name', `receiver_name_invoice`, `pay_dispatch_billing`.`designation`, `pay_dispatch_billing`.`dispatch_date`, `pay_dispatch_billing`.`receiving_date`,`pay_dispatch_billing`.`shipping_address`, (SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code`and `comp_code` = pay_client_master.comp_code AND `document_type` = 'Uniform' limit 1) AS 'Uniform',(SELECT No_of_set FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` and `comp_code` = pay_client_master.comp_code AND `document_type` = 'Uniform' limit 1) AS 'Set_Uniform',(SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = `pay_unit_master`.`comp_code` AND `document_type` = 'Shoes') AS 'Shoes', IF(((SELECT `ID` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = '" + Session["comp_code"].ToString() + "' AND `document_type` = 'ID_Card') != ''), 'YES', 'NO') AS 'ID_Card',pay_dispatch_billing.`pod_no` FROM pay_dispatch_billing INNER JOIN pay_client_master ON pay_dispatch_billing.comp_code = pay_client_master.comp_code AND pay_dispatch_billing.client_code = pay_client_master.client_code LEFT OUTER JOIN pay_unit_master ON pay_dispatch_billing.unit_code = pay_unit_master.unit_code AND pay_dispatch_billing.comp_code = pay_unit_master.comp_code LEFT OUTER JOIN pay_employee_master ON pay_dispatch_billing.emp_code = pay_employee_master.emp_code AND pay_dispatch_billing.comp_code = pay_employee_master.comp_code where  " + where + "", d.con1);

            adp.Fill(ds);
            gv_dispatch_details.DataSource = ds.Tables[0];
            gv_dispatch_details.DataBind();
            Panel8.Visible = true;
            Panel5.Visible = false;
            SearchGridView.Visible = false;
            panel9.Visible = false;
            panel_dispatch.Visible = true;


        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }
    protected void gv_dispatch_details_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void ddl_client_name_dispatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string where = "";

            if (ddl_emp_type.SelectedValue == "1")
            {
                where = "pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and `pay_employee_master`.`left_date` IS NULL ";
            }
            else if (ddl_emp_type.SelectedValue == "2")
            {
                where = " pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master. left_date IS NOT NULL ";

            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            //vikas 08-01-19
          //  left_header();
            MySqlDataAdapter adp = new MySqlDataAdapter(" SELECT Distinct  `pay_dispatch_billing`.`id`,  `client_name` , `pay_dispatch_billing`.`pod_no`, `state_dispatch`, IFNULL(`pay_unit_master`.`unit_name`, `pay_dispatch_billing`.`unit_code`) AS 'unit_name',  IFNULL(`pay_employee_master`.`emp_name`, `pay_dispatch_billing`.`emp_code`) AS 'emp_name', `receiver_name_invoice`, `pay_dispatch_billing`.`designation`, `pay_dispatch_billing`.`dispatch_date`, `pay_dispatch_billing`.`receiving_date`,`pay_dispatch_billing`.`shipping_address`, (SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Uniform',(SELECT No_of_set FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Set_Uniform',(SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = `pay_unit_master`.`comp_code` AND `document_type` = 'Shoes') AS 'Shoes', IF(((SELECT `ID` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'ID_Card') != ''), 'YES', 'NO') AS 'ID_Card' FROM `pay_dispatch_billing` INNER JOIN `pay_document_details` ON `pay_dispatch_billing`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_document_details`.`client_code` AND `pay_dispatch_billing`.`emp_code` = `pay_document_details`.`emp_code` AND `pay_dispatch_billing`.`unit_code` = `pay_document_details`.`unit_code` INNER JOIN `pay_client_master` ON `pay_dispatch_billing`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_client_master`.`client_code` LEFT OUTER JOIN `pay_unit_master` ON `pay_dispatch_billing`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_dispatch_billing`.`comp_code` = `pay_unit_master`.`comp_code` LEFT OUTER JOIN `pay_employee_master` ON `pay_dispatch_billing`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_dispatch_billing`.`comp_code` = `pay_employee_master`.`comp_code` where " + where + " and pay_client_master.client_code = '" + ddl_client_name_dispatch.SelectedValue + "' ", d.con1);
            d.con1.Open();
            adp.Fill(ds);
           Panel8.Visible = true;
            gv_dispatch_details.DataSource = ds.Tables[0];
            gv_dispatch_details.DataBind();
            Panel8.Visible = true;
            gv_dispatch_details.Visible = true;
            d.con1.Close();
            // text_Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_state_name_dispatch.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_client_name_dispatch.SelectedValue + "'  AND LOCATION in(SELECT DISTINCT  (pay_client_state_role_grade.state_name)  FROM  pay_client_state_role_grade  INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code  WHERE  pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code='" + ddl_client_name_dispatch.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) ORDER BY EMP_CURRENT_STATE", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_state_name_dispatch.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        ddl_state_name_dispatch.Items.Insert(0, new ListItem("Select"));

        gv_dispatch_details.Visible = true;



    }
    protected void ddl_state_name_dispatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string where = "";

        if (ddl_emp_type.SelectedValue == "1")
        {
            where = "pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and `pay_employee_master`.`left_date` IS NULL ";
        }
        else if (ddl_emp_type.SelectedValue == "2")
        {
            where = " pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master. left_date IS NOT NULL ";

        }


        gv_dispatch_details.Visible = true;
        btn_add.Visible = true;
        //vikas 08/05/2019
        // button_false();
        try
        {

           // left_header();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            //vikas 08-01-19
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT Distinct  `pay_dispatch_billing`.`id`, `client_name` ,`pay_dispatch_billing`.`pod_no`, `state_dispatch`, IFNULL(`pay_unit_master`.`unit_name`, `pay_dispatch_billing`.`unit_code`) AS 'unit_name',  IFNULL(`pay_employee_master`.`emp_name`, `pay_dispatch_billing`.`emp_code`) AS 'emp_name', `receiver_name_invoice`, `pay_dispatch_billing`.`designation`, `pay_dispatch_billing`.`dispatch_date`, `pay_dispatch_billing`.`receiving_date`,`pay_dispatch_billing`.`shipping_address`, (SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Uniform',(SELECT No_of_set FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Set_Uniform',(SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = `pay_unit_master`.`comp_code` AND `document_type` = 'Shoes') AS 'Shoes', IF(((SELECT `ID` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'ID_Card') != ''), 'YES', 'NO') AS 'ID_Card' FROM `pay_dispatch_billing` INNER JOIN `pay_document_details` ON `pay_dispatch_billing`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_document_details`.`client_code` AND `pay_dispatch_billing`.`emp_code` = `pay_document_details`.`emp_code` AND `pay_dispatch_billing`.`unit_code` = `pay_document_details`.`unit_code` INNER JOIN `pay_client_master` ON `pay_dispatch_billing`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_client_master`.`client_code` LEFT OUTER JOIN `pay_unit_master` ON `pay_dispatch_billing`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_dispatch_billing`.`comp_code` = `pay_unit_master`.`comp_code` LEFT OUTER JOIN `pay_employee_master` ON `pay_dispatch_billing`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_dispatch_billing`.`comp_code` = `pay_employee_master`.`comp_code` where " + where + " and pay_client_master.client_code = '" + ddl_client_name_dispatch.SelectedValue + "' and pay_dispatch_billing.state_dispatch = '" + ddl_state_name_dispatch.SelectedValue + "' ", d.con1);
            d.con1.Open();
            adp.Fill(ds);
            Panel8.Visible = true;
            gv_dispatch_details.DataSource = ds.Tables[0];
            gv_dispatch_details.DataBind();
            // text_Clear();
            Panel8.Visible = true;
            gv_dispatch_details.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_branch_name_dispatch.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name_dispatch.SelectedValue + "' and state_name = '" + ddl_state_name_dispatch.SelectedValue + "' AND  UNIT_CODE in(SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddl_client_name_dispatch.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_state_name_dispatch.SelectedValue + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) and branch_status = 0  ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_name_dispatch.DataSource = dt_item;
                ddl_branch_name_dispatch.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_name_dispatch.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_name_dispatch.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_branch_name_dispatch.Items.Insert(0, new ListItem("Select"));
        }

    }
    protected void ddl_branch_name_dispatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string where = "";

        if (ddl_emp_type.SelectedValue == "1")
        {
            where = "pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and `pay_employee_master`.`left_date` IS NULL ";
        }
        else if (ddl_emp_type.SelectedValue == "2")
        {
            where = " pay_dispatch_billing.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master. left_date IS NOT NULL ";

        }



        gv_dispatch_details.Visible = true;
       // btn_add.Visible = true;
        // button_false();//vikas 08/05/2019
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
           // left_header();
            MySqlDataAdapter adp = new MySqlDataAdapter(" SELECT Distinct `pay_dispatch_billing`.`id`, `client_name` , `pay_dispatch_billing`.`pod_no`,`state_dispatch`, IFNULL(`pay_unit_master`.`unit_name`, `pay_dispatch_billing`.`unit_code`) AS 'unit_name',  IFNULL(`pay_employee_master`.`emp_name`, `pay_dispatch_billing`.`emp_code`) AS 'emp_name', `receiver_name_invoice`, `pay_dispatch_billing`.`designation`, `pay_dispatch_billing`.`dispatch_date`, `pay_dispatch_billing`.`receiving_date`,`pay_dispatch_billing`.`shipping_address`, (SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Uniform',(SELECT No_of_set FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'Uniform') AS 'Set_Uniform',(SELECT `size` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = `pay_unit_master`.`comp_code` AND `document_type` = 'Shoes') AS 'Shoes', IF(((SELECT `ID` FROM `pay_document_details` WHERE `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` AND `comp_code` = 'C01' AND `document_type` = 'ID_Card') != ''), 'YES', 'NO') AS 'ID_Card' FROM `pay_dispatch_billing` INNER JOIN `pay_document_details` ON `pay_dispatch_billing`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_document_details`.`client_code` AND `pay_dispatch_billing`.`emp_code` = `pay_document_details`.`emp_code` AND `pay_dispatch_billing`.`unit_code` = `pay_document_details`.`unit_code` INNER JOIN `pay_client_master` ON `pay_dispatch_billing`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_dispatch_billing`.`client_code` = `pay_client_master`.`client_code` LEFT OUTER JOIN `pay_unit_master` ON `pay_dispatch_billing`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_dispatch_billing`.`comp_code` = `pay_unit_master`.`comp_code` LEFT OUTER JOIN `pay_employee_master` ON `pay_dispatch_billing`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_dispatch_billing`.`comp_code` = `pay_employee_master`.`comp_code` where " + where + " and pay_client_master.client_code = '" + ddl_client_name_dispatch.SelectedValue + "' and pay_dispatch_billing.state_dispatch = '" + ddl_state_name_dispatch.SelectedValue + "' and pay_unit_master.unit_code = '" + ddl_branch_name_dispatch.SelectedValue + "'", d.con1);
            d.con1.Open();
            adp.Fill(ds);
            Panel8.Visible = true;
            gv_dispatch_details.DataSource = ds.Tables[0];
            gv_dispatch_details.DataBind();
            // text_Clear();
            Panel8.Visible = true;
            gv_dispatch_details.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

    }
    protected void gv_dispatch_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_dispatch_details.UseAccessibleHeader = false;
            gv_dispatch_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }
	 protected void btn_left_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
         txt_joiningdate.ReadOnly = true;
       // DateTime left_date = DateTime.ParseExact(txt_leftdate.Text.ToString(), "dd/MM/yyyy", null);
        int result = 0;
        string date = txt_leftdate.Text;
        if (!txt_leftdate.Text.Equals(""))
        {
            DateTime joining_date = DateTime.ParseExact(txt_joiningdate.Text.ToString(), "dd/MM/yyyy", null);
            DateTime left_date = DateTime.ParseExact(txt_leftdate.Text.ToString(), "dd/MM/yyyy", null);
            int date1 = DateTime.Compare(joining_date, left_date);
            if (date1 > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Left date should be after than joining date ');", true);

                //txt_leftdate.Text = "";
                newpanel.Visible = true;
                btnupdate.Visible = false;
                btnUpload.Visible = false;
                btn_approve.Visible = false;
                return;
            }
        }
        result = d.operation("update pay_employee_master set LEFT_REASON ='" + txtreasonforleft.Text + "',LEFT_DATE = str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y')  where comp_code='" + Session["Comp_code"].ToString() + "'  AND client_code='" + ddl_unit_client.SelectedValue + "' AND emp_code='" + txt_eecode.Text + "' and legal_flag='2'");
        
         if ((txt_leftdate.Text).Trim() != "")
        {
            d.operation("UPDATE pay_user_master SET flag = 'L' WHERE LOGIN_ID='" + txt_eecode.Text + "' and comp_code ='" + Session["Comp_code"].ToString() + "'");
            // btn_releiving_letter_Click(null, null);
        }
        
        if (result == 1)
        {
            btn_add.Visible = false;
            btnupdate.Visible = false;
            btnUpload.Visible = false;
            btn_approve.Visible = false;
            btn_left.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Left Successfully ..!!');", true);
            reason_panel.Visible = false;//vikas add from visible fase after approve 22/04/2019
            btn_dispatch_details.Visible = true;
            
        }
        btnhelp_Click(sender, e);
        
    }
     protected void rejoin_update()
     {
         if (ViewState["rejoin_update"].ToString() == "1")
         {
             d.operation("update pay_employee_master t1 inner join(select PF_NUMBER,ESIC_NUMBER,PAN_NUMBER,PF_DEDUCTION_FLAG,ESIC_DEDUCTION_FLAG,EMP_NEW_PAN_NO from pay_employee_master where emp_code='" + ViewState["rejoin_empcode"].ToString() + "')t2 set t1.PF_NUMBER=t2.PF_NUMBER ,t1.ESIC_NUMBER = t2.ESIC_NUMBER, t1.PAN_NUMBER = t2.PAN_NUMBER, t1.PF_DEDUCTION_FLAG = t2.PF_DEDUCTION_FLAG, t1.ESIC_DEDUCTION_FLAG = t2.ESIC_DEDUCTION_FLAG, t1.EMP_NEW_PAN_NO = t2.EMP_NEW_PAN_NO where t1.comp_code='" + Session["comp_code"].ToString() + "' and t1.emp_code='" + txt_eecode.Text + "'");
             ViewState["rejoin_update"] = "0";
         }
     }


     protected void gv_dublicate_id_card_RowDataBound(object sender, GridViewRowEventArgs e)
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

         e.Row.Cells[1].Visible = false;
         e.Row.Cells[2].Visible = false;
     }
     protected void gv_dublicate_id_card_PreRender(object sender, EventArgs e)
     {
         try
         {
             gv_dublicate_id_card.UseAccessibleHeader = false;
             gv_dublicate_id_card.HeaderRow.TableSection = TableRowSection.TableHeader;
         }
         catch { }//vinod dont apply catch
     }

     private void load_dublicate_id_grdview()
     {

         MySqlCommand cmd1 = new MySqlCommand("SELECT id,admin_id_set,from_date,to_date from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ", d.con);
         d.con.Open();
         try
         {
             MySqlDataReader dr1 = cmd1.ExecuteReader();
             if (dr1.HasRows)
             {
                 System.Data.DataTable dt1 = new System.Data.DataTable();
                 dt1.Load(dr1);
                 gv_dublicate_id_card.DataSource = dt1;
                 gv_dublicate_id_card.DataBind();
                 
             }
             
             dr1.Close();
         }
         catch (Exception ex) { throw ex; }
         finally
         {
             d.con.Close();
         }
     }


     public bool check_date_issueing(string issue_date, string dispatch_date)
     {

         try
         {
             //DateTime dispatch_date = DateTime.Parse(dispatching_date);
             //DateTime resive_date = DateTime.Parse(resiving_date);
             DateTime dispatch_date1 = DateTime.ParseExact(dispatch_date, "dd/MM/yyyy", null);
             DateTime issue_date1 = DateTime.ParseExact(issue_date, "dd/MM/yyyy", null);

             if (dispatch_date1.Date > issue_date1.Date)
             {
                 return true;
             }
             return false;
         }
         catch (Exception ex)
         { throw ex; }

     }



     protected void lnk_dublicate_id_Click(object sender, EventArgs e)
     {

         gv_dublicate_id_card.DataSource = null;
         gv_dublicate_id_card.DataBind();

         reason_panel.Visible = false;

         // 04-05-2020 komal
         string emp_dublicate = d.getsinglestring(" SELECT `pay_employee_master`.`emp_code` FROM `pay_employee_master` WHERE `pay_employee_master`.`comp_code` = '" + Session["comp_code"].ToString() + "'  AND `legal_flag` = 2  AND `ap_status` = 'Approve By Leagal'   AND `reject_reason` = 'Approve By Leagal'  AND `id_card_dispatch_date` IS NOT NULL  AND `pay_employee_master`.`client_code` = '" + ddl_unit_client.SelectedValue + "'  AND `pay_employee_master`.`client_wise_state` = '" + ddl_clientwisestate.SelectedValue + "'  AND `pay_employee_master`.`unit_code` = '" + ddl_unitcode.SelectedValue + "'   AND `pay_employee_master`.`emp_code` = '" + txt_eecode.Text + "'   AND `left_date` IS NULL");

      
         if (emp_dublicate=="")
         {


             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Genrate Original Id Card... !!!');", true);

         }
         // dispatch original id card
         string dispatch_original_id = d.getsinglestring(" SELECT `emp_code` FROM pay_dispatch_billing WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' and `client_code` = '" + ddl_unit_client.SelectedValue + "'  AND state_dispatch = '" + ddl_clientwisestate.SelectedValue + "'  AND `unit_code` = '" + ddl_unitcode.SelectedValue + "'   AND `emp_code` = '" + txt_eecode.Text + "' ");


         if (dispatch_original_id == "")
         {


             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Dispatch Original Id Card... !!!');", true);

         }

         // issueing date


         string dispatch_date = d.getsinglestring("select dispatch_date from pay_dispatch_billing where `comp_code` = '" + Session["comp_code"].ToString() + "' and `client_code` = '" + ddl_unit_client.SelectedValue + "'  AND state_dispatch = '" + ddl_clientwisestate.SelectedValue + "'  AND `unit_code` = '" + ddl_unitcode.SelectedValue + "'   AND `emp_code` = '" + txt_eecode.Text + "' ");

         if (dispatch_date!="")
         {

         if (check_date_issueing(txt_from_date.Text, dispatch_date))
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select Dublicate id issue date greater than original id dispatch date !!!')", true);
             return;
         }

         }
         if (dispatch_date == "")  {

             ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Dispatch original id card !!!')", true);
             return;
         
         }

        
         string dub_emp = d.getsinglestring("select emp_code from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

         if (dub_emp=="")
         {

             int result = d.operation("INSERT INTO pay_id_card_resend (comp_code,client_code,unit_code,state_name,emp_code,id_no_set,admin_id_set,from_date,to_date)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + txt_eecode.Text + "','" + ddl_id_set_dublicate.SelectedValue + "','" + ddl_id_set_dublicate.SelectedValue + "','" + txt_from_date.Text + "','" + txt_to_date.Text + "')");

         if (result > 0)
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Successfully Added... !!!');", true);
             return;
         }
         else
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Addition Failed... !!!');", true);
         }
         }

         else if (dub_emp != "")
         {


             string field3 = d.getsinglestring("select `admin_id_set` from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");
             

             if (field3 == "1")
             {


             // for genrate id 

             string genrate_id1 = d.getsinglestring("select field2 from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");
             // for dispatch id
             string dispatch_id1 = d.getsinglestring("select dispatch_date_du from pay_dispatch_billing where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_dispatch = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

             if (genrate_id1 == "" || dispatch_id1 == "")
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Dispatch First Dublicate Id Card ... !!!');", true);
             }


             else if (genrate_id1 != "" && dispatch_id1 != "" )
             {



                 string id_set = d.getsinglestring("select admin_id_set from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

                 int added_admin_set = Convert.ToInt16(id_set) + 1;

                 
                     int result1 = d.operation("update pay_id_card_resend set id_no_set ='" + ddl_id_set_dublicate.SelectedValue + "',admin_id_set='" + added_admin_set + "'  where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "'");

                     if (result1 > 0)
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Successfully Added... !!!');", true);
                         ddl_id_set_dublicate.SelectedValue = "0";
                         txt_from_date.Text = "";
                         txt_to_date.Text = "";

                         return;

                     }
                     else
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Addition Failed... !!!');", true);
                     }
                 
             }

            }else if (field3 == "2")
               {

                   // for genrate id 

                   string genrate_id2 = d.getsinglestring("select field3 from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");
                   // for dispatch id
                   string dispatch_id2 = d.getsinglestring("select dispatch_date_du2 from pay_dispatch_billing where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_dispatch = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

                   if (genrate_id2 == "" || dispatch_id2 == "")

                   { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Dispatch Second Dublicate Id Card ... !!!');", true); return; }


                   string id_set = d.getsinglestring("select admin_id_set from pay_id_card_resend where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "' ");

                   int added_admin_set = Convert.ToInt16(id_set) + 1;

                   int result1 = d.operation("update pay_id_card_resend set id_no_set ='" + ddl_id_set_dublicate.SelectedValue + "',admin_id_set='" + added_admin_set + "'  where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and emp_code = '" + txt_eecode.Text + "'");

                   if (result1 > 0)
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Successfully Added... !!!');", true);
                       ddl_id_set_dublicate.SelectedValue = "0";
                       txt_from_date.Text = "";
                       txt_to_date.Text = "";
                       return;
                   }
                   else
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Addition Failed... !!!');", true);
                   }




               }else if (field3 == "3")
               
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Duplicate ID card limit fulfill... !!!');", true);

                   ddl_id_set_dublicate.SelectedValue = "0";
                   txt_from_date.Text = "";
                   txt_to_date.Text = "";

                   return;
               }

         }


        
         load_dublicate_id_grdview();


         ddl_id_set_dublicate.SelectedValue = "0";
         txt_from_date.Text = "";
         txt_to_date.Text = "";

     }
     protected void lnk_remove_product_Click1(object sender, EventArgs e)
     {

         hidtab.Value = "3";
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
       
         GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

         d.operation("delete from pay_id_card_resend where id = '" + grdrow.Cells[2].Text + "'");
         load_dublicate_id_grdview();
     }
     protected void emp_upload_Click(object sender, EventArgs e)
     {
         mp1.Show();
     }
     protected void Button4_Click(object sender, EventArgs e)
     {

     }
}

