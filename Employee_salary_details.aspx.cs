using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Web;

using System.Configuration;
using System.Data.OleDb;

public partial class Employee_salary_details : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            txt_date_conveyance.Text = d.getCurrentMonthYear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) and client_active_close='0' ORDER BY client_code", d.con);
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

    }


    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {

            d1.con1.Open();
            ddl_state.Items.Clear();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
           //comment 30/09     MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client.SelectedValue + "' and state in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "')  ORDER BY STATE", d1.con1);
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client.SelectedValue + "' and state in (SELECT pay_client_state_role_grade.state_name FROM pay_client_state_role_grade INNER JOIN   pay_unit_master ON  pay_client_state_role_grade.client_code = pay_unit_master . client_code AND   pay_client_state_role_grade.state_name=pay_unit_master.state_name WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND (  branch_close_date IS NULL || branch_close_date  = '' ||  branch_close_date >= STR_TO_DATE('01/" + txt_date_conveyance.Text+ "', '%d/%m/%Y'))  AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND pay_client_state_role_grade.client_code='" + ddl_client.SelectedValue + "')  ORDER BY STATE", d1.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddl_state.DataSource = DS;
                ddl_state.DataBind();
                ddl_state.Items.Insert(0, "Select");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con1.Close();
            }


            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND state_name ='" + ddl_state.SelectedValue + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unitcode.DataSource = dt_item;
                    ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unitcode.DataBind();
                    ddl_unitcode.Items.Insert(0, "Select");
                }
                dt_item.Dispose();
                d.con.Close();

                //ddl_unitcode.SelectedIndex = 0;
                ddl_state_SelectedIndexChanged(null, null);
                ddlunitselect_SelectedIndexChanged(null, null);
                //ddlemployee_SelectedIndexChanged(null,null);
                ddl_employee_type.SelectedIndex = 0;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
    }
    protected void ddlunitselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        grd_convayance.DataSource = null;
        grd_convayance.DataBind();

        grd_emp_file.DataSource = null;
        grd_emp_file.DataBind();

        gv_payment_hold.DataSource = null;
        gv_payment_hold.DataBind();

        ddl_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item;
        string left = " employee_type = '" + ddl_employee_type.SelectedValue + "' and  (left_date = '' or left_date is null)";
        if (ddl_employee_type.SelectedValue == "Left")
        {
            left = " left_date is not null";
        }
        string where = " where  comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code='" + ddl_unitcode.SelectedValue + "' and " + left + " ORDER BY emp_name";
        if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = " where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' and " + left + " ORDER BY emp_name";
        }
        //vikas 08-01-19
        cmd_item = new MySqlDataAdapter("Select (SELECT CASE Employee_type WHEN 'Reliever' THEN CONCAT(emp_name, '-', 'Reliever') ELSE emp_name END) AS 'EMP_NAME',EMP_CODE from pay_employee_master " + where, d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_employee.DataSource = dt_item;
                ddl_employee.DataTextField = dt_item.Columns[0].ToString();
                ddl_employee.DataValueField = dt_item.Columns[1].ToString();
                ddl_employee.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_employee.Items.Insert(0, "ALL");
            ddl_employee.SelectedIndex = 0;
            ddlemployee_SelectedIndexChanged(null, null);

                        //komal 12-06-19

                convayance_details();
                conveyance_location();
            
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
        //Chk UAN number

        // left permanent employee region by komal 02-09-2020

        string emp_name = null;
        string emp_name1 = null;
        string emp_pf_name = null;
        string emp_acc_name = null;


        if (ddl_employee_type.SelectedValue != "Reliever")
        {
            if (ddl_employee_type.SelectedValue != "Left")
            {
                emp_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_uanno.Text + "' = '',PAN_NUMBER ='######',PAN_NUMBER ='" + txt_uanno.Text + "') AND if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
            }
            else
                if (ddl_employee_type.SelectedValue == "Left")
            {
                emp_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_uanno.Text + "' = '',PAN_NUMBER ='######',PAN_NUMBER ='" + txt_uanno.Text + "') AND if(left_date is not null , left_date is not null,left_date !='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
            }
                if (!emp_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('UAN Number Already Present for this employee : " + emp_name + " !!!')", true);
                txt_uanno.Focus();
                return;
            }
            //Chk ESIC number
          //  string emp_name1 = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_esicnumber.Text + "' = '',ESIC_NUMBER ='######',ESIC_NUMBER ='" + txt_esicnumber.Text + "') AND if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "'");
                if (ddl_employee_type.SelectedValue != "Left")
                {
                    emp_name1 = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_esicnumber.Text + "' = '',ESIC_NUMBER ='######',ESIC_NUMBER ='" + txt_esicnumber.Text + "') AND if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
                }
                else
                    if (ddl_employee_type.SelectedValue == "Left")
                    {
                        emp_name1 = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_esicnumber.Text + "' = '',ESIC_NUMBER ='######',ESIC_NUMBER ='" + txt_esicnumber.Text + "') AND if(left_date is not null , left_date is not null,left_date !='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
                    }
            
            if (!emp_name1.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('ESIC Number Already Present for this employee : " + emp_name1 + " !!!')", true);
                txt_esicnumber.Focus();
                return;
            }
            //check PF  no

           // string emp_pf_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_pfnumber.Text + "' = '',PF_NUMBER ='######',PF_NUMBER ='" + txt_pfnumber.Text + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "'");

            if (ddl_employee_type.SelectedValue != "Left")
            {
                emp_pf_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_pfnumber.Text + "' = '',PF_NUMBER ='######',PF_NUMBER ='" + txt_pfnumber.Text + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
            }
            else
                if (ddl_employee_type.SelectedValue == "Left")
                {
                    emp_pf_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_pfnumber.Text + "' = '',PF_NUMBER ='######',PF_NUMBER ='" + txt_pfnumber.Text + "') and  if(left_date is not null , left_date is not null,left_date !='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
                }
            
            
            if (!emp_pf_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('PF  Number Already Present for this employee : " + emp_pf_name + " !!!')", true);
                txt_pfnumber.Focus();
                return;
            }
            //check account no

           // string emp_acc_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_bankaccountno.Text.Trim() + "' = '',original_bank_account_no ='######',original_bank_account_no ='" + txt_bankaccountno.Text.Trim() + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "'");

            if (ddl_employee_type.SelectedValue != "Left")
            {
                emp_acc_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_bankaccountno.Text.Trim() + "' = '',original_bank_account_no ='######',original_bank_account_no ='" + txt_bankaccountno.Text.Trim() + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
            }
            else
                if (ddl_employee_type.SelectedValue == "Left")
                {
                    emp_acc_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_bankaccountno.Text.Trim() + "' = '',original_bank_account_no ='######',original_bank_account_no ='" + txt_bankaccountno.Text.Trim() + "') and  if(left_date is not null , left_date is not null,left_date !='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "' and rejoin_flag != 1 ");
                }
            
            if (!emp_acc_name.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Account  Number Already Present for this employee : " + emp_acc_name + " !!!')", true);
                txt_bankaccountno.Focus();
                return;
            }
        }
        //txt_bankaccountno
        //res = d.operation("update pay_employee_master set PAN_NUMBER = '" + txt_uanno.Text + "',EMP_NEW_PAN_NO = '" + txt_pan_new_num.Text + "',ESIC_NUMBER = '" + txt_esicnumber.Text + "',ESIC_DEDUCTION_FLAG = '" + ddl_esicdeductionflag.SelectedItem.Text + "',PF_NUMBER = '" + txt_pfnumber.Text + "',PF_DEDUCTION_FLAG = '" + ddl_pfdeductionflag.SelectedItem.Text + "',cca='" + Txt_cca.Text + "',gratuity='" + Txt_gra.Text + "',special_allow='" + Txt_allow.Text + "',Group_insurance='" + txt_greoupinsuranc.Text + "',esic_policy_id='" + txt_policy_id.Text + "',esicpolicy_start_date='" + txt_start_date.Text + "',esicpolicy_end_date='" + txt_end_date.Text + "',original_bank_account_no='" + txt_bankaccountno.Text + "',BANK_HOLDER_NAME='" + txt_holdaer_name.Text + "',PF_IFSC_CODE='" + txt_ifsccode.Text + "',salary_status='" + ddl_payment_status.SelectedValue + "' ,salary_desc='" + txt_desc.Text + "',fine='" + txt_fine.Text + "',conveyance_rate='" + txt_conveyance_amount.Text + "',PF_BANK_NAME  = '" + txt_pfbankname.Text + "',BANK_BRANCH = '" + ddl_bankcode.Text + "'  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ");
        if (!ddl_employee.SelectedValue.Equals("ALL"))
        {
            //vinod
            string acct_no = "";
            if (txt_bankaccountno.Text.Trim() != "")
            {
                acct_no = "original_bank_account_no='" + txt_bankaccountno.Text.Trim() + "',BANK_HOLDER_NAME='" + txt_holdaer_name.Text.Trim() + "',PF_IFSC_CODE='" + txt_ifsccode.Text.Trim() + "',";
            }
            res = d.operation("update pay_employee_master set PAN_NUMBER = '" + txt_uanno.Text + "',EMP_NEW_PAN_NO = '" + txt_pan_new_num.Text + "',ESIC_NUMBER = '" + txt_esicnumber.Text + "',ESIC_DEDUCTION_FLAG = '" + ddl_esicdeductionflag.SelectedItem.Text + "',PF_NUMBER = '" + txt_pfnumber.Text + "',PF_DEDUCTION_FLAG = '" + ddl_pfdeductionflag.SelectedItem.Text + "',cca='" + Txt_cca.Text + "',gratuity='" + Txt_gra.Text + "',special_allow='" + Txt_allow.Text + "',Group_insurance='" + txt_greoupinsuranc.Text + "',esic_policy_id='" + txt_policy_id.Text + "',esicpolicy_start_date='" + txt_start_date.Text + "',esicpolicy_end_date='" + txt_end_date.Text + "',"+acct_no+"PF_BANK_NAME  = '" + txt_pfbankname.Text + "',BANK_BRANCH = '" + ddl_bankcode.Text + "'  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ");
            //res = d.operation("update pay_salary_hold set client_code = '" + ddl_client.SelectedValue + "',unit_code = '" + ddl_unitcode.SelectedValue + "',STATE_NAME = '" + ddl_state.SelectedValue + "',Employee_type = '" + ddl_employee_type.SelectedValue + "',EMP_CODE = '" + ddl_employee.SelectedValue + "',salary_status='" + ddl_payment_status.SelectedValue + "' ,salary_desc='" + txt_desc.Text + "',month='" + txt_payment_date.Text.Substring(0, 2) + "',year='" + txt_payment_date.Text.Substring(3, 4) + "'  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ");
            
            //res = d.operation("insert into pay_salary_hold(comp_code,unit_code,client_code,state_name,employee_type,emp_name,month,year,salary_status)values('" + Session["COMP_CODE"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + ddl_employee.SelectedValue + "','" + txt_payment_date.Text.Substring(0, 2) + "','" + txt_payment_date.Text.Substring(3, 4) + "','" + ddl_payment_status.SelectedValue + "')");
            d.operation("update pay_pro_master set PF_NO = '" + txt_pfnumber.Text + "' ,ESI_No='" + txt_esicnumber.Text + "', PAN_No = '" + txt_uanno.Text + "',updated_by='" + Session["LOGIN_ID"].ToString() + "',updation_date =now()  where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + ddl_employee.SelectedValue + "' and  month >='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year >='" + txt_date_conveyance.Text.Substring(3) + "'");
        }
        foreach (GridViewRow row in gv_payment_hold.Rows)
        {
           
                string salary_status = (row.FindControl("ddl_payment_status") as System.Web.UI.WebControls.DropDownList).Text;
                string salary_desc = (row.FindControl("txt_salary_desc") as System.Web.UI.WebControls.TextBox).Text;
                string cell_2_Value = gv_payment_hold.Rows[row.RowIndex].Cells[3].Text;
                string cell_1_Value = gv_payment_hold.Rows[row.RowIndex].Cells[1].Text;
                d.operation("delete from pay_salary_hold where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and client_code = '" + ddl_client.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and date = '" + txt_date_conveyance.Text + "' and EMP_CODE='" + cell_1_Value + "' and salary_status='1'");
                
            if (salary_status == "1")
                {
                    if (salary_desc == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reason !!');", true);
                        return;
                    }
                    else
                    {
                        d.operation("INSERT INTO pay_salary_hold (comp_code,unit_code,client_code,state_name,employee_type,date,EMP_NAME,salary_status,salary_desc,EMP_CODE,month,year)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + ddl_client.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + ddl_employee_type.SelectedValue + "','" + txt_date_conveyance.Text + "','" + cell_2_Value + "','" + salary_status + "','" + salary_desc + "','" + cell_1_Value + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3) + "')");
                    }
                }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save successfully!!');", true);
        text_clear();


    }

    //komal 12-06-19


    protected bool conveyance_upload()
    {


        if (d.getsinglestring("select conveyance_images from pay_conveyance_upload where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_Code='" + ddl_client.SelectedValue + "' and unit_code= '" + ddl_unitcode.SelectedValue + "' and employee_type in ('Permanent','Temporary') and state = '" + ddl_state.SelectedValue + "'and month ='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3) + "' ") == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Upload Conveyance Sheet  !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }

    //protected void conveyance_update() {

    //    try {
    //        foreach (GridViewRow gr in grd_convayance.Rows)
    //        {

    //            //string cell_1_Value = grd_convayance.Rows[gr.RowIndex].Cells[1].Text;
    //            string cell_1_Value = grd_convayance.Rows[gr.RowIndex].Cells[1].Text;
    //            System.Web.UI.WebControls.TextBox lbl_material_name = (System.Web.UI.WebControls.TextBox)gr.FindControl("txt_conveyance_amount");
    //            string lbl_material_name_1 = (lbl_material_name.Text);

    //            d.operation("update pay_employee_master set conveyance_rate='" + lbl_material_name_1 + "',month_conveyance='" + txt_date_conveyance.Text.Substring(0,2) + "',year_conveyance='" + txt_date_conveyance.Text.Substring(3) + "',month = '" + txt_date_conveyance.Text + "' where  EMP_CODE='" + cell_1_Value + "'");
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save successfully!!');", true);

    //        }

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { }

    //}

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void text_clear()
    {
        //ddl_client.SelectedValue = "Select";
        //ddl_state.SelectedValue =null;
        //ddl_unitcode.SelectedValue = "Select";
        // ddl_employee_type.SelectedValue = "Select";
        //  ddl_employee.SelectedValue = "";
        txt_uanno.Text = "";
        txt_pan_new_num.Text = "";
        txt_esicnumber.Text = "";
        txt_pfnumber.Text = "";
        Txt_cca.Text = "0";
        Txt_allow.Text = "0";
        Txt_gra.Text = "0";
        txt_end_date.Text = "";
        txt_greoupinsuranc.Text = "";
        txt_policy_id.Text = "";
        txt_start_date.Text = "";
        txt_bankaccountno.Text = "";
        txt_ifsccode.Text = "";
        txt_holdaer_name.Text = "";
        //ddl_payment_status.SelectedValue = "0";
        txt_finedesc.Text = "";
        txt_ccadesc.Text = "";
        txt_advancedesc.Text = "";
        txt_advance_payment.Text = "0";
        txt_fine.Text = "0";
        Txt_cca.Text = "0";
        ddl_bankcode.Text = "";
        txt_pfbankname.Text = "";
       // txt_desc.Text = "";
        ddl_employee.SelectedIndex = 0;
        ddlemployee_SelectedIndexChanged(null, null);
        //grd_convayance_location.DataSource = null;
        //grd_convayance_location.DataBind();
        //grd_convayance.DataSource=null;
        //grd_convayance.DataBind();
        // txt_date_conveyance.Text = "";
    }


    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_payment_hold.DataSource = null;
        gv_payment_hold.DataBind();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d2.con.Open();
            //MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,fine,cca_desc,advance_desc ,fine_desc,conveyance_rate,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d.con);
            MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,cca_desc,advance_desc,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d2.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_uanno.Text = dr.GetValue(0).ToString();
                txt_pan_new_num.Text = dr.GetValue(1).ToString();
                txt_esicnumber.Text = dr.GetValue(2).ToString();
                ddl_esicdeductionflag.SelectedValue = dr.GetValue(3).ToString();
                txt_pfnumber.Text = dr.GetValue(4).ToString();
                ddl_pfdeductionflag.SelectedValue = dr.GetValue(5).ToString();
                Txt_cca.Text = dr.GetValue(6).ToString();
                Txt_gra.Text = dr.GetValue(7).ToString();
                Txt_allow.Text = dr.GetValue(8).ToString();
                txt_bankaccountno.Text = dr.GetValue(13).ToString();
                txt_holdaer_name.Text = dr.GetValue(14).ToString();
                txt_ifsccode.Text = dr.GetValue(15).ToString();
                //ddl_payment_status.SelectedValue = dr.GetValue(16).ToString();
                //txt_desc.Text = dr.GetValue(17).ToString();
                txt_advance_payment.Text = dr.GetValue(18).ToString();
                // txt_fine.Text = dr.GetValue(19).ToString();
                txt_ccadesc.Text = dr.GetValue(19).ToString();
                txt_advancedesc.Text = dr.GetValue(20).ToString();
                //txt_finedesc.Text = dr.GetValue(22).ToString();

                // txt_conveyance_amount.Text = dr.GetValue(21).ToString();


                ddl_bankcode.Text = dr.GetValue(21).ToString();
                txt_pfbankname.Text = dr.GetValue(22).ToString();

                //komal 12-06-19
                load_emp_file_grdview();

            }
            dr.Close();
            d2.con.Close();
        }
        catch (Exception ex)
        {
        }
        finally { d2.con.Close(); }
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }

    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
        //comment 30/09    MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND pay_unit_master.branch_status = 0   ORDER BY pay_unit_master.state_name", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + txt_date_conveyance.Text + "', '%d/%m/%Y'))   ORDER BY pay_unit_master.state_name", d.con);
		    d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unitcode.DataSource = dt_item;
                    ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unitcode.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unitcode.Items.Insert(0, "Select");
                //ddl_unitcode.SelectedIndex = 0;
                ddlunitselect_SelectedIndexChanged(null, null);
                ddlemployee_SelectedIndexChanged(null, null);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        d.con.Open();
        hidtab.Value = "0";
        try
        {
            string sql = "SELECT client_name AS client_code,STATE_NAME, CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS unit_code, pay_employee_master.emp_code as 'emp_code' ,pay_employee_master.EMP_NAME as 'EMP_NAME',pay_employee_master.employee_type,original_bank_account_no ,PF_IFSC_CODE,BANK_HOLDER_NAME,'' as comments FROM pay_employee_master left outer JOIN pay_document_details  ON   pay_employee_master.emp_code = pay_document_details.emp_code   AND pay_employee_master.comp_code = pay_document_details.comp_code    AND pay_employee_master.unit_code = pay_document_details.unit_code  inner join pay_client_master on pay_employee_master.client_code=pay_client_master.client_code and pay_employee_master.comp_code=pay_client_master.comp_code  inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code WHERE pay_employee_master.left_date IS NULL AND pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' and EMPloyee_type != 'Staff' order by 1,2,3";

            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);
            send_file(ds);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }
    private void send_file(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Employee_Documents.xls");
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
            { //Original Bank A/C Number ,PF_IFSC_CODE,BANK_HOLDER_NAME
                case ListItemType.Header:
                    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th font-size=8>EMPLOYEE CODE</th><th font-size=8>CLIENT NAME</th><th font-size=8>BRANCH STATE</th><th font-size=8>BRANCH</th><th font-size=8>EMPLOYEE NAME</th><th font-size=8>EMPLOYEE TYPE</th><th font-size=8>BANK ACCOUNT NUMBER</th><th font-size=8>BANK IFSC CODE</th><th font-size=8>ACCOUNT HOLDER NAME</th><th font-size=8>Comments</th></tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["client_code"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["STATE_NAME"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["unit_code"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["employee_type"].ToString().ToUpper() + "</td><td font-size=8>'" + ds.Tables[0].Rows[ctr]["original_bank_account_no"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["PF_IFSC_CODE"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["BANK_HOLDER_NAME"].ToString().ToUpper() + "</td><td font-size=8>" + ds.Tables[0].Rows[ctr]["Comments"].ToString().ToUpper() + "</td></tr>");
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

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr =
                ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema =
      connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        UpdtEmpAttendanceGridView.Caption = Path.GetFileName(FilePath);
        UpdtEmpAttendanceGridView.DataSource = dt;
        UpdtEmpAttendanceGridView.DataBind();
    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        DataTable table2 = new DataTable("emp");
        table2.Columns.Add("client_code");
        table2.Columns.Add("state_name");
        table2.Columns.Add("unit_code");
        table2.Columns.Add("emp_code");
        table2.Columns.Add("emp_name");
        table2.Columns.Add("employee_type");
        table2.Columns.Add("original_bank_account_no");
        table2.Columns.Add("PF_IFSC_CODE");
        table2.Columns.Add("BANK_HOLDER_NAME");
        table2.Columns.Add("Comments");

        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
            string FolderPath = ConfigurationManager.AppSettings["FolderPath1"];
            FilePath = Server.MapPath(FolderPath + "Downloads\\" + FileName);
            FileUpload1.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension, "Yes");

            Panel3.Visible = true;

        }
        if (UpdtEmpAttendanceGridView.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                int result = 0;
                try
                {
                    for (int i = 0; i < UpdtEmpAttendanceGridView.Rows.Count; i++)
                    {
                        string empcode = UpdtEmpAttendanceGridView.Rows[i].Cells[1].Text;
                        string bank_Ac_no = (UpdtEmpAttendanceGridView.Rows[i].Cells[7].Text);
                        bank_Ac_no.Replace("'", "");
                        string ifsc_code = (UpdtEmpAttendanceGridView.Rows[i].Cells[8].Text);
                        string holder_name = (UpdtEmpAttendanceGridView.Rows[i].Cells[9].Text);

                        string emp_name = d.getsinglestring("select emp_name from pay_employee_master where employee_type='Permanent' and emp_code != '" + empcode + "' and original_bank_account_no = '" + bank_Ac_no + "'");
                        if (emp_name != "")
                        {
                            table2.Rows.Add(UpdtEmpAttendanceGridView.Rows[i].Cells[1].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[2].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[3].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[4].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[5].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[6].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[7].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[8].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[9].Text, "Bank Account Number Already for employee : " + emp_name);
                        }
                        else
                        {
                            result = d.operation("update pay_employee_master set original_bank_account_no='" + bank_Ac_no + "', PF_IFSC_CODE='" + ifsc_code + "',BANK_HOLDER_NAME='" + holder_name + "' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and emp_code = '" + empcode + "'");
                            if (result == 0)
                            { table2.Rows.Add(UpdtEmpAttendanceGridView.Rows[i].Cells[1].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[2].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[3].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[4].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[5].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[6].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[7].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[8].Text, UpdtEmpAttendanceGridView.Rows[i].Cells[9].Text, "Record Not Updated. Please check company."); }
                        }
                    }
                }
                catch (Exception es) { throw es; }
                finally
                {
                    File.Delete(FilePath);

                }
            }
            if (table2.Rows.Count > 0)
            {
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file(ds);
            }
            //
        }
    }


    //komal 17-05-19


    protected void add_lnk_file_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }



        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }


        string date = d.getsinglestring(" select date from pay_employee_salary_details where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and client_code = '" + ddl_client.SelectedValue + "' and state = '" + ddl_state.SelectedValue + "' and employee_type= '" + ddl_employee_type.SelectedValue + "' and emp_code='" + ddl_employee.SelectedValue + "' ");

        if (txt_date.Text == date)
        {


            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' this month fine all ready added please delete first ones... !!!');", true);

        }
        else
        {

            int result = d.operation("insert into pay_employee_salary_details(comp_code,unit_code,client_code,state,employee_type,emp_code,fine,fine_description,date,month,year)values('" + Session["COMP_CODE"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + ddl_employee.SelectedValue + "','" + txt_fine.Text + "','" + txt_finedesc.Text + "','" + txt_date.Text + "','" + txt_date.Text.Substring(0, 2) + "','" + txt_date.Text.Substring(3, 4) + "')");

            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Successfully Added... !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Employee Files Addition Failed... !!!');", true);
            }
        }

        txt_fine.Text = "";
        txt_finedesc.Text = "";
        txt_date.Text = "";

        load_emp_file_grdview();

    }

    protected void grd_emp_file_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_remove_bank_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //if (ViewState["grd_emp_file"] != null)
        //{
        //    System.Data.DataTable dt = (System.Data.DataTable)ViewState["grd_emp_file"];
        //    if (dt.Rows.Count >= 1)
        //    {
        //        if (rowID < dt.Rows.Count)
        //        {
        //            dt.Rows.Remove(dt.Rows[rowID]);
        //        }
        //    }
        //    ViewState["grd_emp_file"] = dt;
        //    grd_emp_file.DataSource = dt;
        //    grd_emp_file.DataBind();
        //}


        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        d.operation("delete from pay_employee_salary_details where id = '" + grdrow.Cells[2].Text + "'");
        load_emp_file_grdview();
    }

    private void load_emp_file_grdview()
    {

        MySqlCommand cmd1 = new MySqlCommand("SELECT Id,fine, fine_description, date FROM pay_employee_salary_details WHERE emp_code = '" + ddl_employee.SelectedValue + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                grd_emp_file.DataSource = dt;
                grd_emp_file.DataBind();
                grd_emp_file.Visible = true;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                grd_emp_file.DataSource = dt;
                grd_emp_file.DataBind();
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    //protected void btn_save_file_Click(object sender, EventArgs e)
    //{

    //komal 12-06-19 

    protected void convayance_details()
    {
        string left = " pay_employee_master.employee_type = '" + ddl_employee_type.SelectedValue + "' and  (pay_employee_master.left_date = '' or pay_employee_master.left_date is null)";
        if (ddl_employee_type.SelectedValue == "Left")
        {
            left = " pay_employee_master.left_date is not null";
        }
        string where = " where  pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.unit_code='" + ddl_unitcode.SelectedValue + "' and " + left + " ORDER BY pay_employee_master.emp_name";
        if (ddl_unitcode.SelectedValue == "ALL")
        {
            where = " where pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' and " + left + " ORDER BY pay_employee_master.emp_name";
        }
        MySqlCommand cmd11 = new MySqlCommand("select pay_employee_master.EMP_NAME,pay_employee_master.EMP_CODE,pay_conveyance_amount_history.conveyance_rate,pay_conveyance_amount_history.month, pay_conveyance_amount_history.year, CONCAT(pay_conveyance_amount_history.month, '/', pay_conveyance_amount_history.year) AS 'month' FROM  pay_employee_master LEFT JOIN pay_conveyance_amount_history  ON pay_employee_master.comp_code = pay_conveyance_amount_history.comp_code  AND pay_employee_master.client_code = pay_conveyance_amount_history.client_code  AND pay_employee_master.unit_code = pay_conveyance_amount_history.unit_code  AND pay_employee_master.emp_code = pay_conveyance_amount_history.emp_code AND pay_conveyance_amount_history.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_amount_history.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'" + where, d.con);
        d.con.Open();

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr1 = cmd11.ExecuteReader();
            if (dr1.HasRows)
            {

                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2.Load(dr1);
                if (dt2.Rows.Count > 0)
                {

                    ViewState["grd_convayance"] = dt2;
                }
                grd_convayance.DataSource = dt2;
                grd_convayance.DataBind();

            }
            dr1.Close();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }


    protected void grd_convayance_RowDataBound(object sender, GridViewRowEventArgs e)
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


    //komal 12-06-19
    protected void conveyance_location()
    {
        try
        {
            grd_convayance_location.Visible = true;
            System.Data.DataTable dt = new System.Data.DataTable();

            //apporve_attendace gridview
            grd_convayance_location.DataSource = null;
            grd_convayance_location.DataBind();
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT client_name, pay_unit_master.state_name, unit_name, CAST(CONCAT(pay_conveyance_upload.month, '/', pay_conveyance_upload.year) AS char) AS 'month', pay_conveyance_upload.conveyance_images FROM pay_conveyance_upload INNER JOIN pay_client_master ON pay_conveyance_upload.comp_code = pay_client_master.comp_code AND pay_conveyance_upload.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_conveyance_upload.unit_code = pay_unit_master.unit_code AND pay_conveyance_upload.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'  AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.UNIT_CODE = '" + ddl_unitcode.SelectedValue + "'  AND pay_unit_master.state_name = '" + ddl_state.SelectedValue + "'   AND pay_conveyance_upload.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "'  AND pay_conveyance_upload.year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'", d.con);

            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grd_convayance_location.DataSource = dt;
                grd_convayance_location.DataBind();
                //gv_approve_panel.Visible = true;
            }
            d.con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { d.con.Close(); }


    }

    protected void btn_upload_Click1(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        hidtab.Value = "1";
        if (bill_upload.HasFile)
        {
            conveyance_images();
            conveyance_location();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select File to upload !!')", true);
        }
    }

    protected void conveyance_images()
    {

        try
        {
            d.con.Open();

            string fileExt = System.IO.Path.GetExtension(bill_upload.FileName);
            string fname = null;
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".ZIP")
            {
                string fileName = Path.GetFileName(bill_upload.PostedFile.FileName);
                bill_upload.PostedFile.SaveAs(Server.MapPath("~/approved_attendance_images/") + fileName);
                fname = Session["COMP_CODE"].ToString() + "_" + ddl_client.SelectedValue + "_" + ddl_unitcode.SelectedValue + "_" + txt_date_conveyance.Text.Substring(0, 2) + txt_date_conveyance.Text.Substring(3) + fileExt;
                File.Copy(Server.MapPath("~/approved_attendance_images/") + fileName, Server.MapPath("~/approved_attendance_images/") + fname, true);
                File.Delete(Server.MapPath("~/approved_attendance_images/") + fileName);

                d.operation("delete from pay_conveyance_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and client_code = '" + ddl_client.SelectedValue + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'");
                int result = d.operation("insert into pay_conveyance_upload(comp_code,unit_code,client_code,state,employee_type,month,year,conveyance_images)values('" + Session["COMP_CODE"].ToString() + "','" + ddl_unitcode.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + fname + "')");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploaded Successfully... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Conveyance Files uploading Failed... !!!');", true);
                }
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void downloadfile(string filename, string unit_name)
    {

        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\approved_attendance_images\\" + filename);
            string unitName = unit_name + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + unitName);
            Response.TransmitFile("~\\approved_attendance_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();


        }
        catch (Exception ex) { }

    }


    protected void lnk_conveyance_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string filename = commandArgs[0];
        string unit_name = commandArgs[1];

        //string filename = e.CommandArgument.ToString();
        ////string unit_name = gv_approve_attendace.SelectedRow.Cells[2].ToString();
        if (filename != "")
        {
            downloadfile(filename, unit_name);
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Downloaded !!!')", true);
        }

    }
    protected void grd_convayance_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_convayance.UseAccessibleHeader = false;
            grd_convayance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void grd_convayance_location_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_convayance_location.UseAccessibleHeader = false;
            grd_convayance_location.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_conv_save_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
            try
            {
                foreach (GridViewRow gr in grd_convayance.Rows)
                {
                    string cell_1_Value = grd_convayance.Rows[gr.RowIndex].Cells[1].Text;
                    string cell_2_Value = grd_convayance.Rows[gr.RowIndex].Cells[2].Text;
                    System.Web.UI.WebControls.TextBox lbl_material_name = (System.Web.UI.WebControls.TextBox)gr.FindControl("txt_conveyance_amount");
                    string lbl_material_name_1 = (lbl_material_name.Text);
                    
                    d.operation("delete from pay_conveyance_amount_history where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "' and client_code = '" + ddl_client.SelectedValue + "' and emp_code = '" + cell_1_Value + "' and month  = '" + txt_date_conveyance.Text.Substring(0, 2) + "' and year = '" + txt_date_conveyance.Text.Substring(3, 4) + "'");
                    if (lbl_material_name_1 != "" && lbl_material_name_1 != "0")
                    {
                        d.operation("insert into pay_conveyance_amount_history (comp_code,unit_code,client_code,employee_type,state,month,year,EMP_CODE,emp_name,conveyance_rate)value('" + Session["COMP_CODE"] + "','" + ddl_unitcode.SelectedValue + "','" + ddl_client.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + ddl_state.SelectedValue + "','" + txt_date_conveyance.Text.Substring(0, 2) + "','" + txt_date_conveyance.Text.Substring(3, 4) + "','" + cell_1_Value + "','" + cell_2_Value + "','" + lbl_material_name_1 + "')");
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { }
            if (!conveyance_upload())
            {
                convayance_details();
                return;
            }
            conveyance_location();
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save successfully!!');", true);
        text_clear();

    }
    //chaitali 6-10-2019(payment Hold/Unhold)
    protected void payment_hold() 
    {
        string where = "";
        string where1="";
       
        if (ddl_employee.SelectedValue == "ALL")
        {
            where = " and pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "' ";
        }
        else
        {
            where = " and pay_employee_master.comp_code='" + Session["comp_code"] + "' and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.unit_code = '" + ddl_unitcode.SelectedValue + "' and pay_employee_master.EMP_CODE='" + ddl_employee.SelectedValue + "'";
        }

        if (ddl_employee_type.SelectedValue == "Left")
        {
            where1 =  "pay_employee_master.left_date is not null" ;
        }
        else if (ddl_employee_type.SelectedValue != "Left")
        {
            where1 = "pay_employee_master.employee_type = '" + ddl_employee_type.SelectedValue.ToString() + "' and pay_employee_master.left_date is null ";
        
        }
        try
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            d.con.Open();
            MySqlCommand cmd;
            //cmd=new MySqlCommand("SELECT id,date,pay_salary_hold.salary_desc,emp_name  from pay_salary_hold INNER JOIN `pay_employee_master` ON `pay_salary_hold`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_salary_hold`.`emp_code` = `pay_employee_master`.`emp_code` where  " + where + " ", d.con);

            cmd = new MySqlCommand("SELECT pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_salary_hold.salary_status,pay_salary_hold.salary_desc  from pay_employee_master left outer join pay_salary_hold on pay_employee_master.emp_code = pay_salary_hold.emp_code and pay_salary_hold.date='" + txt_date_conveyance.Text + "' INNER JOIN pay_attendance_muster ON pay_employee_master.emp_code = pay_attendance_muster.emp_code AND pay_attendance_muster.month = '" + txt_date_conveyance.Text.Substring(0, 2) + "' AND pay_attendance_muster.year ='" + txt_date_conveyance.Text.Substring(3) + "' where " + where1 + " " + where + " ", d.con);

           // cmd = new MySqlCommand("SELECT pay_employee_master.EMP_CODE,pay_employee_master.EMP_NAME,pay_salary_hold.salary_status,pay_salary_hold.salary_desc  from pay_employee_master left outer join pay_salary_hold on pay_employee_master.emp_code = pay_salary_hold.emp_code and pay_salary_hold.date='" + txt_date_conveyance.Text + "' where pay_employee_master.employee_type = '" + ddl_employee_type.SelectedValue + "' " + where + "  ", d.con);
            MySqlDataAdapter dt_item = new MySqlDataAdapter(cmd);
            dt_item.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gv_payment_hold.DataSource = dt;
                gv_payment_hold.DataBind();
                //gv_approve_panel.Visible = true;
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }
    protected void gv_payment_hold_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_payment_hold.UseAccessibleHeader = false;
            gv_payment_hold.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catchbtn_show
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        HiddenField1.Value = "2";
        string emp = d.getsinglestring("select count(emp_code) from pay_attendance_muster where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + ddl_unitcode.SelectedValue + "'  and Month='" + txt_date_conveyance.Text.Substring(0, 2) + "' and year='" + txt_date_conveyance.Text.Substring(3) + "' ");
        if (emp == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Not Approve By Admin!!');", true);
            return;
        }
        else
        { 
            payment_hold();
        }
    }
    protected void gv_payment_hold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        try
        {
            DropDownList salary_status = e.Row.FindControl("ddl_payment_status") as System.Web.UI.WebControls.DropDownList;
            salary_status.SelectedValue = e.Row.Cells[2].Text;
        }
        catch { }//vinod no need to apply catch
       e.Row.Cells[1].Visible = false;
       e.Row.Cells[2].Visible = false;
    }
    protected void ddl_payment_status_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
}