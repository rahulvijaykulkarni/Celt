using MySql.Data.MySqlClient;
using System;
using System.Web.UI;

public partial class employee_details : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }
        if (!IsPostBack)
        {
            client_name();
        }
        

    }
    protected void client_name()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "'  ORDER BY client_code", d.con);//AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
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
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        //state
        ddl_state.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  order by 1", d.con);//and state_Name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "')
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state.DataSource = dt_item;
                ddl_state.DataTextField = dt_item.Columns[0].ToString();
                ddl_state.DataValueField = dt_item.Columns[0].ToString();
                ddl_state.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state.Items.Insert(0, "Select");


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and  pay_unit_master.UNIT_CODE  in ( select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in(" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND pay_unit_master.branch_status = 0   ORDER BY pay_unit_master.state_name", d.con);
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
                
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }

    }
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        
    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d2.con.Open();
            //MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,fine,cca_desc,advance_desc ,fine_desc,conveyance_rate,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d.con);
            MySqlCommand cmd = new MySqlCommand("select PAN_NUMBER ,EMP_NEW_PAN_NO ,ESIC_NUMBER ,ESIC_DEDUCTION_FLAG ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,Group_insurance,esic_policy_id,esicpolicy_start_date,esicpolicy_end_date,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE,salary_status,salary_desc, EMP_ADVANCE_PAYMENT,cca_desc,advance_desc,BANK_BRANCH,PF_BANK_NAME from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "' ", d2.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_bankaccountno.Text = dr.GetValue(13).ToString();
                txt_holdaer_name.Text = dr.GetValue(14).ToString();
                txt_ifsccode.Text = dr.GetValue(15).ToString();
                txt_pfbankname.Text = dr.GetValue(9).ToString();
               
                // txt_fine.Text = dr.GetValue(19).ToString();
                ddl_bankcode.Text = dr.GetValue(21).ToString();
                txt_pfbankname.Text = dr.GetValue(22).ToString();

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
    protected void ddl_employee_type_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        
        cmd_item = new MySqlDataAdapter("Select (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',EMP_CODE from pay_employee_master " + where, d.con);
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
            ddl_employee.Items.Insert(0, "Select");
            ddl_employee.SelectedIndex = 0;



            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    //protected void btn_add_Click(object sender, EventArgs e)
    //{
    //    try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
    //    catch { }
    //    int res = 0;
    //    //check account no

    //        string emp_acc_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_bankaccountno.Text + "' = '',original_bank_account_no ='######',original_bank_account_no ='" + txt_bankaccountno.Text + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "'");
    //        if (!emp_acc_name.Equals(""))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Account  Number Already Present for this employee : " + emp_acc_name + " !!!')", true);
    //            txt_bankaccountno.Focus();
    //            return;
    //        }

    protected void btn_edit_Click(object sender, EventArgs e)
    {

        int res = 0;
        if (!ddl_client.SelectedValue.Equals("ALL"))
        {
            res = d.operation("update pay_employee_master set original_bank_account_no='" + txt_bankaccountno.Text + "',BANK_HOLDER_NAME='" + txt_holdaer_name.Text + "',PF_IFSC_CODE='" + txt_ifsccode.Text + "',PF_BANK_NAME  = '" + txt_pfbankname.Text + "',BANK_BRANCH = '" + ddl_bankcode.Text + "'  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "'  AND CLIENT_CODE='" + ddl_client.SelectedValue + "' ");
            d.operation("update pay_employee_master set original_bank_account_no='" + txt_bankaccountno.Text + "',BANK_HOLDER_NAME='" + txt_holdaer_name.Text + "',PF_IFSC_CODE='" + txt_ifsccode.Text + "',PF_BANK_NAME  = '" + txt_pfbankname.Text + "',BANK_BRANCH = '" + ddl_bankcode.Text + "'  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "'  AND CLIENT_CODE='" + ddl_client.SelectedValue + "' ");
        }
        string emp_acc_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and  if('" + txt_bankaccountno.Text + "' = '',original_bank_account_no ='######',original_bank_account_no ='" + txt_bankaccountno.Text + "') and  if(left_date is null , left_date is null,left_date ='') and employee_type='Permanent' AND EMP_CODE !='" + ddl_employee.SelectedValue + "'");
        if (!emp_acc_name.Equals(""))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Account  Number Already Present for this employee : " + emp_acc_name + " !!!')", true);
            txt_bankaccountno.Focus();
            return;
        }
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Update successfully!!');", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        ddl_client.SelectedValue = "Select";
        ddl_state.SelectedValue = "Select";
        ddl_unitcode.SelectedValue="Select";
        ddl_employee_type.SelectedValue="Select";
        ddl_employee.SelectedValue = "Select";
        
        txt_bankaccountno.Text = "";
        txt_holdaer_name.Text = "";
        txt_ifsccode.Text = "";
        ddl_bankcode.Text = "";
        txt_pfbankname.Text = "";

    }
    //protected void btn_delete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    //    catch { }
    //}
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

   
}

