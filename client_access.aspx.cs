using System.Web.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;


public partial class client_access : System.Web.UI.Page
{
    DAL d = new DAL();
    int res = 0,flag=0;
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

           
            gv_employee_list();
            //client_list1();
          //  employee_list();
            gv_operation_gridview();
        }
       
        //btn_save.Visible = false;
        //btn_delete.Visible = false;
    }


   //protected void employee_list()
   // {

   //     try
   //     {
   //         d.con.Open();

   //         MySqlCommand cmd = new MySqlCommand("SELECT distinct pay_client_state_role_grade.`EMP_CODE`, `EMP_NAME`, `Employee_type` FROM `pay_employee_master` inner join pay_client_state_role_grade on pay_employee_master.`COMP_CODE`=  pay_client_state_role_grade.COMP_CODE and  pay_employee_master.EMP_CODE  = pay_client_state_role_grade.EMP_CODE  AND `pay_client_state_role_grade`.`department_type` = `pay_employee_master`.`department_type`", d.con);
   //         MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
   //         DataSet ds = new DataSet();
   //         sda.Fill(ds);
           

   //         ddl_employee.DataSource = ds.Tables[0];
   //         ddl_employee.DataTextField = "EMP_NAME";
   //         ddl_employee.DataValueField = "EMP_CODE";
   //         ddl_employee.DataBind();
        
   //         ddl_employee.Items.Insert(0, new ListItem("Select Staff", ""));
   //         sda.Dispose();
   //         d.con.Close();
   //     }
   //     catch (Exception ex) { throw ex; }
   //     finally
   //     {
   //         d.con.Close();
   //     }

   // }

    protected void ddl_department_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            text_clear1();
            MySqlCommand cmd = new MySqlCommand("Select EMP_CODE,EMP_NAME,Employee_type from pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Employee_type='Staff' and left_date is null AND department_type='"+ddl_department.SelectedValue+"'", d.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ddl_employee_type.DataSource = ds.Tables[0];
            ddl_employee_type.DataTextField = "EMP_NAME";
            ddl_employee_type.DataValueField = "EMP_CODE";
            ddl_employee_type.DataBind();

          
            ddl_employee_type.Items.Insert(0, new ListItem("Select Staff", ""));

        
            sda.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally {
            d.con.Close();
        }
    
    }
    protected void gv_employee_list()
    {

        try
        {
            d.con.Open();

            MySqlCommand cmd = new MySqlCommand("Select EMP_CODE,EMP_NAME,Employee_type from pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Employee_type='Staff' AND EMP_CODE in(select EMP_CODE from pay_client_state_role_grade where COMP_CODE='"+Session["COMP_CODE"].ToString()+"')", d.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
           
            ddl_employee.DataSource = ds.Tables[0];
            ddl_employee.DataTextField = "EMP_NAME";
            ddl_employee.DataValueField = "EMP_CODE";
            ddl_employee.DataBind();
         
            ddl_employee.Items.Insert(0, new ListItem("Select Staff", ""));
            sda.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }

    //protected void selected_client_list1()
    //{

    //    try
    //    {
    //        d.con.Open();
    //        client_list.Items.Clear();
    //        MySqlDataAdapter cmd = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code  in (select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + ddl_employee_type.SelectedValue + "')   ORDER BY client_code", d.con);
    //        DataTable dt = new DataTable();
    //        cmd.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            client_list.DataSource = dt;
    //            client_list.DataTextField = dt.Columns[0].ToString();
    //            client_list.DataValueField = dt.Columns[1].ToString();
    //            client_list.DataBind();
    //        }

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }



    //}


    protected void non_client_list1() {

        try
        {
            d.con.Open();
            ddl_client_name.Items.Clear();
            ddl_client.Items.Clear();
            MySqlDataAdapter cmd = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_client_name.DataSource = dt;
                ddl_client_name.DataTextField = dt.Columns[0].ToString();
                ddl_client_name.DataValueField = dt.Columns[1].ToString();
                ddl_client_name.DataBind();

               
                dt.Dispose();
                d.con.Close();
            }
            dt.Dispose();
            ddl_client_name.Items.Insert(0,"Select");
            
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    
    
    }

    //protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    IEnumerable<string> selectedValues = from item in client_list.Items.Cast<ListItem>()
    //                                         where item.Selected
    //                                         select item.Value;
    //    string listvalues_ddl_unitclient = string.Join("','", selectedValues);

    //    state_list.Items.Clear();
    //    MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE in ('" + listvalues_ddl_unitclient + "') and unit_code is null order by 1", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
    //        while (dr_item1.Read())
    //        {
    //            state_list.Items.Add(dr_item1[0].ToString());

    //        }
    //        dr_item1.Close();
    //        // ddl_clientwisestate.Items.Insert(0, new ListItem("All"));
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }

    //}

    protected void btn_submit_click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
       
        }
        catch { }
        IEnumerable<string> selectedValues = from item in ddl_unitcode_without1.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Value;
        string listvalues_ddl_unitclient = string.Join(",", selectedValues);

        //IEnumerable<string> state = from item in state_list.Items.Cast<ListItem>()
        //                            where item.Selected
        //                            select item.Value;
        //string listvalues_ddl_state = string.Join(",", state);

        var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        //var elements1 = listvalues_ddl_state.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);


       // d.operation("delete from pay_client_state_role_grade where  comp_code = '" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='"+ddl_employee_type.SelectedValue+"'");
        if (ddl_state_name.SelectedValue == "ALL") {
            flag = 1;
        }

        if (elements.Length != 0)
        {

            foreach (string branch in elements)
            {
                string state_name = d.getsinglestring("select state_name from pay_unit_master where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND client_code='"+ddl_client_name.SelectedValue+"' AND UNIT_CODE= '" + branch + "'");
                //foreach (string state1 in elements1)
                //{
                // string temp1 = d.getsinglestring("SELECT state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + client + "' and state = '" + state1 + "' unit_code is null");
                //if (temp1 == state1)
                //{
                res = d.operation("insert into pay_client_state_role_grade (comp_code,client_code,created_by,created_date,EMP_CODE,`state_name`,`UNIT_CODE`,flag,department_type) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client_name.SelectedValue + "','" + Session["LOGIN_ID"].ToString() + "',now(),'" + ddl_employee_type.SelectedValue + "','" + state_name + "','" + branch + "','" + flag + "','"+ddl_department.SelectedValue+"')");
                //  } 


                //}
            }
            if (res > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Staff Asign Succsesfully !!!')", true);
                text_clear();
                //employee_list();
                gv_employee_list();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Staff Asign Faild')", true);

            }
           // selected_client_list1();
           // non_client_list1();
            gv_operation_gridview();
        }
        else {

            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Select Branch...!!!')", true);
            ddl_unitcode_without1.Focus();
        }
    }

    protected void emp_onSelectedValue(object sendser, EventArgs e) {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

           //d.con.Open();
           //try
           //{
           //    MySqlCommand cmd = new MySqlCommand("Select cast(GROUP_CONCAT(distinct(`client_code`)) as char) as client_code,CAST(GROUP_CONCAT(DISTINCT (`state_name`)) AS char) AS 'state_name'  from pay_client_state_role_grade where comp_code='" + Session["comp_code"] + "' AND EMP_CODE='" + ddl_employee_type.SelectedValue + "' ORDER BY client_code", d.con);
           //    MySqlDataReader dr = cmd.ExecuteReader();
           //    if (dr.Read())
           //    {
           //        string controller_type = dr.GetValue(0).ToString();
           //        update_listbox(client_list, controller_type);

           //       // state_list.Items.Add(dr[1].ToString());
           //    }
           //}
           //catch (Exception ex) { }
           //finally { d.con.Close(); }
        txt_employee_code.Text = ddl_employee_type.SelectedValue;
        ddl_client_name.Items.Clear();
        ddl_state_name.Items.Clear();
        ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        non_client_list1();
        //selected_client_list1();

          
    }
    //protected void update_listbox(ListBox client_list, string controller_type)
    //{
       

    //    controller_type = controller_type.Replace(",", ""); 
        
    //    client_list.ClearSelection();

    //    for (int i = 0; i <= controller_type.Length - 1; i++)
    //    {
    //        if (controller_type.Length == 1)
    //        {
    //            client_list.Items[int.Parse(controller_type)].Selected = true;
    //        }
    //        else
    //        {
    //            client_list.Items.[int.Parse(controller_type.Substring(i,1))].Selected = true;
    //        }
    //    }

    //}

    protected void btn_delete_click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
       
        IEnumerable<string> selectedValues = from item in ddl_unitcode1.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Value;
        string listvalues_ddl_unitclient = string.Join(",", selectedValues);
        var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (elements.Length != 0)
        {
            string reporting_emp_series = d.reporting_emp_series(Session["COMP_CODE"].ToString(),ddl_employee_type.SelectedValue);
            foreach (string branch in elements)
            {
                 string state_name = d.getsinglestring("select state_name from pay_unit_master where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND client_code='"+ddl_client_name.SelectedValue+"' AND UNIT_CODE= '" + branch + "'");
                 res = d.operation("Delete from pay_client_state_role_grade Where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'AND EMP_CODE in (" + reporting_emp_series + ") AND client_code='" + ddl_client_name.SelectedValue + "' AND state_name='" + state_name + "' AND UNIT_CODE='" + branch + "' and department_type='" + ddl_department.SelectedValue + "'");
            }

            if (res > 0)
            {
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Branch  Remove Succsesfully')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Branch  Remove Succsesfully !!!')", true);
                text_clear();
                //employee_list();
                gv_employee_list();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Branch Remove Faild')", true);
            }
            //selected_client_list1();
           // non_client_list1();
            gv_operation_gridview();
        }
        else {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Branch ')", true);
               ddl_unitcode1.Focus();
        }
      
    }

     protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddl_client_name.SelectedValue != "Select" )
        {
            //State
          //  ddl_state.Items.Clear();
            ddl_state_name.Items.Clear();
            ddl_unitcode_without1.Items.Clear();
            ddl_unitcode1.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' order by 1", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                   

                    ddl_state_name.DataSource = dt_item;
                    ddl_state_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_state_name.DataValueField = dt_item.Columns[0].ToString();
                    ddl_state_name.DataBind();
                    d.con.Close();
                  
                }
                dt_item.Dispose();
                d.con.Close();
               
                ddl_state_name.Items.Insert(0, "Select");
                ddl_state_name.Items.Insert(1, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
        else
        {
            
        }
    }
     protected void ddl_state_SelectedIndexChanged1(object sender, EventArgs e)
     {

         string reporting_emp_series = d.reporting_emp_series(Session["COMP_CODE"].ToString(), ddl_employee_type.SelectedValue);
             System.Data.DataTable dt_item = new System.Data.DataTable();
             System.Data.DataTable dt_item1 = new System.Data.DataTable();
             d.con.Open();
             try
             {
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                 MySqlDataAdapter cmd_item;
                  if (ddl_state_name.SelectedValue == "ALL")
                 {
                 //   cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' unit_code not in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
                    //(MD change) cmd_item = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE  comp_code='" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' AND  `unit_code` NOT IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "' and department_type='"+ddl_department.SelectedValue+"' ) ORDER BY `UNIT_CODE`", d.con);
                     cmd_item = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE  comp_code='" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' AND  `unit_code` NOT IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "'  AND EMP_CODE in (" + reporting_emp_series + ") ) AND branch_status = 0 ORDER BY `UNIT_CODE`", d.con);
                     flag = 1;
                  }
                  else
                  {
                   // cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "'and unit_code not in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
                    // (MD change) cmd_item = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE `comp_code` = '" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' AND  `unit_code` NOT IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' and department_type='"+ddl_department.SelectedValue+"') ORDER BY `UNIT_CODE`", d.con);
                      cmd_item = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE `comp_code` = '" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' AND  `unit_code` NOT IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' AND EMP_CODE in (" + reporting_emp_series + ")) AND branch_status = 0 ORDER BY `UNIT_CODE`", d.con);
                      flag = 0;
                  }

                 DataSet ds1 = new DataSet();
                 cmd_item.Fill(ds1);
                 ddl_unitcode_without1.DataSource = ds1.Tables[0];
                 ddl_unitcode_without1.DataValueField = "unit_code";
                 ddl_unitcode_without1.DataTextField = "UNIT_NAME";
                 ddl_unitcode_without1.DataBind();
                 cmd_item.Dispose();
                 dt_item.Dispose();
                 d.con.Close();
                 //ddl_branch.Items.Insert(0, "ALL");
             }
             catch (Exception ex) { throw ex; }
             finally
             {
                 d.con.Close();
             }
             d.con.Open();
             try
             {
                 MySqlDataAdapter cad1;
                 if (ddl_state_name.SelectedValue == "ALL")
                 {
                     //   cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' unit_code not in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
                     cad1 = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE  comp_code='" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' AND  `unit_code`  IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "' AND EMP_CODE in("+reporting_emp_series+")) ORDER BY `UNIT_CODE`", d.con);
                     flag = 1;
                 }
                 else
                 {
                     // cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "'and unit_code not in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') ORDER BY UNIT_CODE", d.con);
                     cad1 = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE `comp_code` = '" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' AND  `unit_code`  IN (SELECT IFNULL(`UNIT_CODE`,0) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `CLIENT_CODE` = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "' AND EMP_CODE in ("+reporting_emp_series+")) ORDER BY `UNIT_CODE`", d.con);
                     flag = 0;
                 }

                 //// MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master where emp_code in (select emp_code from pay_travel_emp_policy where policy_id = " + ddlpolicies.SelectedValue + ") and comp_code ='" + Session["comp_code"] + "'  order by EMP_NAME", d.con);
                 //MySqlCommand cmd_1 = new MySqlCommand("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, pay_op_management.unit_code from pay_op_management inner join pay_unit_master on pay_op_management.unit_code=pay_unit_master.unit_code and pay_op_management.comp_code=pay_unit_master.comp_code  where pay_op_management.comp_code='" + Session["comp_code"].ToString() + "' and pay_op_management.client_code = '" + ddl_client_name.SelectedValue + "' and pay_unit_master.state_name='" + ddl_state_name.SelectedValue + "' and pay_unit_master.unit_code in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') ORDER BY pay_op_management.UNIT_CODE", d.con);
                 
                 DataSet ds1 = new DataSet();

                 cad1.Fill(ds1);
                 ddl_unitcode1.DataSource = ds1.Tables[0];
                 ddl_unitcode1.DataValueField = "unit_code";
                 ddl_unitcode1.DataTextField = "UNIT_NAME";
                 ddl_unitcode1.DataBind();
               
                 dt_item.Dispose();
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


     protected void gv_operation_gridview()
     {

         try
         {
             d.con.Open();
             MySqlCommand cmd_1 = new MySqlCommand("SELECT pay_client_state_role_grade.department_type as 'Department',pay_employee_master.EMP_NAME AS  'Staff Assign',client_name as 'CLIENT NAME', pay_client_state_role_grade.state_name as 'STATE NAME',  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_client_state_role_grade.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'BRANCH NAME'  FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_client_state_role_grade.EMP_CODE=pay_employee_master.EMP_CODE AND  pay_client_state_role_grade.comp_code=pay_employee_master.comp_code  and pay_client_state_role_grade.department_type = pay_employee_master.department_type INNER JOIN pay_client_master ON pay_client_state_role_grade.CLIENT_CODE=pay_client_master.CLIENT_CODE AND  pay_client_state_role_grade.comp_code=pay_client_master.comp_code INNER JOIN pay_unit_master ON pay_client_state_role_grade.UNIT_CODE=pay_unit_master.UNIT_CODE AND  pay_client_state_role_grade.comp_code=pay_unit_master.comp_code WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and client_active_close='0' order by pay_client_state_role_grade.id  desc", d.con);
             MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
             DataSet DS1 = new DataSet();
             cad1.Fill(DS1);
             gv_client_access.DataSource = DS1;
             gv_client_access.DataBind();
             cad1.Dispose();
             d.con.Close();
         }
         catch (Exception ex)
         {

         }
         finally
         {
             d.con.Close();


         }
     }

     protected void emp_gv_onSelectedValue(object sendser, EventArgs e)
     {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
         d.con.Open();
         //try
         //{
         //    MySqlCommand cmd = new MySqlCommand("Select cast(GROUP_CONCAT(distinct(`client_code`)) as char) as client_code,CAST(GROUP_CONCAT(DISTINCT (`state_name`)) AS char) AS 'state_name'  from pay_client_state_role_grade where comp_code='" + Session["comp_code"] + "' AND EMP_CODE='" + ddl_employee_type.SelectedValue + "' ORDER BY client_code", d.con);
         //    MySqlDataReader dr = cmd.ExecuteReader();
         //    if (dr.Read())
         //    {
         //        string controller_type = dr.GetValue(0).ToString();
         //        update_listbox(client_list, controller_type);

         //       // state_list.Items.Add(dr[1].ToString());
         //    }
         //}
         //catch (Exception ex) { }
         //finally { d.con.Close(); }
         gv_client_access.DataSource = null;
         gv_client_access.DataBind();
         MySqlCommand cmd_1 = new MySqlCommand("SELECT pay_client_state_role_grade.department_type as 'Department',pay_employee_master.`EMP_NAME` AS  'Staff Assign', client_name as 'CLIENT NAME', pay_client_state_role_grade.state_name as 'STATE NAME',  CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_client_state_role_grade`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'BRANCH NAME'  FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_client_state_role_grade.EMP_CODE=pay_employee_master.EMP_CODE AND  pay_client_state_role_grade.comp_code=pay_employee_master.comp_code and pay_client_state_role_grade.department_type = pay_employee_master.department_type INNER JOIN pay_client_master ON pay_client_state_role_grade.CLIENT_CODE=pay_client_master.CLIENT_CODE AND  pay_client_state_role_grade.comp_code=pay_client_master.comp_code INNER JOIN pay_unit_master ON pay_client_state_role_grade.UNIT_CODE=pay_unit_master.UNIT_CODE AND  pay_client_state_role_grade.comp_code=pay_unit_master.comp_code WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.EMP_CODE='" + ddl_employee.SelectedValue + "' order by pay_client_state_role_grade.id  desc", d.con);
         MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
         DataSet DS1 = new DataSet();
         cad1.Fill(DS1);
         gv_client_access.DataSource = DS1;
         gv_client_access.DataBind();
         cad1.Dispose();
         d.con.Close();
         


         string employee_code = ddl_employee.SelectedValue;
         client_list1();

         //selected_client_list1();


     }

     protected void client_list1()
     {

         try
         {
             d.con.Open();
           
             ddl_client.Items.Clear();
             MySqlDataAdapter cmd = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code in(select client_code from  pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_employee.SelectedValue + "') and client_active_close='0' ORDER BY client_code", d.con);
             DataTable dt = new DataTable();
             cmd.Fill(dt);
             if (dt.Rows.Count > 0)
             {
                

                 ddl_client.DataSource = dt;
                 ddl_client.DataTextField = dt.Columns[0].ToString();
                 ddl_client.DataValueField = dt.Columns[1].ToString();
                 ddl_client.DataBind();
                 dt.Dispose();
                 d.con.Close();
             }
             dt.Dispose();
             
             ddl_client.Items.Insert(0, "Select");
         }
         catch (Exception ex) { throw ex; }
         finally
         {
             d.con.Close();
         }



     }

     protected void ddl_client_gv_SelectedIndexChanged1(object sender, EventArgs e)
     {
         if (ddl_client.SelectedValue != "Select")
         {
             //State
               ddl_state.Items.Clear();
            // ddl_state_name.Items.Clear();
             System.Data.DataTable dt_item = new System.Data.DataTable();
             MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and  state_name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND client_code='" + ddl_client.SelectedValue+ "' and  EMP_CODE='" + ddl_employee.SelectedValue+ "') order by 1", d.con);
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
                     d.con.Close();
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
         else
         {

         }
         try
         {
             d.con.Open();
             gv_client_access.DataSource = null;
             gv_client_access.DataBind();
             MySqlCommand cmd_1 = new MySqlCommand("SELECT pay_client_state_role_grade.department_type as 'Department', pay_employee_master.`EMP_NAME` AS  'Staff Assign', client_name as 'CLIENT NAME', pay_client_state_role_grade.state_name as 'STATE NAME',  CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_client_state_role_grade`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'BRANCH NAME'  FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_client_state_role_grade.EMP_CODE=pay_employee_master.EMP_CODE AND  pay_client_state_role_grade.comp_code=pay_employee_master.comp_code and pay_client_state_role_grade.department_type = pay_employee_master.department_type INNER JOIN pay_client_master ON pay_client_state_role_grade.CLIENT_CODE=pay_client_master.CLIENT_CODE AND  pay_client_state_role_grade.comp_code=pay_client_master.comp_code INNER JOIN pay_unit_master ON pay_client_state_role_grade.UNIT_CODE=pay_unit_master.UNIT_CODE AND  pay_client_state_role_grade.comp_code=pay_unit_master.comp_code WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.EMP_CODE='" + ddl_employee.SelectedValue + "' AND pay_client_state_role_grade.client_code='" + ddl_client.SelectedValue + "' order by pay_client_state_role_grade.id  desc", d.con);
             MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
             DataSet DS1 = new DataSet();
             cad1.Fill(DS1);
             gv_client_access.DataSource = DS1;
             gv_client_access.DataBind();
             cad1.Dispose();
             d.con.Close();
         }
         catch (Exception ex) { throw ex; }
         finally {
             d.con.Close();
         }
     }

     protected void ddl_state_gv_SelectedIndexChanged1(object sender, EventArgs e)
     {
         
         try
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
             d.con.Open();
             gv_client_access.DataSource = null;
             gv_client_access.DataBind();
             MySqlCommand cmd_1 = new MySqlCommand("SELECT pay_client_state_role_grade.department_type as 'Department', pay_employee_master.EMP_NAME AS  'Staff Assign', client_name as 'CLIENT NAME', pay_client_state_role_grade.state_name as 'STATE NAME',  CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_client_state_role_grade.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'BRANCH NAME'  FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_client_state_role_grade.EMP_CODE=pay_employee_master.EMP_CODE AND  pay_client_state_role_grade.comp_code=pay_employee_master.comp_code and pay_client_state_role_grade.department_type = pay_employee_master.department_type INNER JOIN pay_client_master ON pay_client_state_role_grade.CLIENT_CODE=pay_client_master.CLIENT_CODE AND  pay_client_state_role_grade.comp_code=pay_client_master.comp_code INNER JOIN pay_unit_master ON pay_client_state_role_grade.UNIT_CODE=pay_unit_master.UNIT_CODE AND  pay_client_state_role_grade.comp_code=pay_unit_master.comp_code WHERE pay_client_state_role_grade.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.EMP_CODE='" + ddl_employee.SelectedValue + "' AND pay_client_state_role_grade.client_code='" + ddl_client.SelectedValue + "' and pay_client_state_role_grade.state_name='" + ddl_state.SelectedValue + "' and client_active_close='0' order by pay_client_state_role_grade.id  desc", d.con);
             MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
             DataSet DS1 = new DataSet();
             cad1.Fill(DS1);
             gv_client_access.DataSource = DS1;
             gv_client_access.DataBind();
             cad1.Dispose();
             d.con.Close();
         }
         catch (Exception ex) { throw ex; }
         finally
         {
             d.con.Close();
         }
     }

     protected void btn_clear_click(object sender, EventArgs e) {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
         text_clear();
     }
    protected void text_clear(){
        ddl_department.SelectedValue = "Select";
    ddl_employee_type.Items.Clear();
    txt_employee_code.Text = "";
       // ddl_client_name.SelectedIndex=-1;

        ddl_state_name.Items.Clear();
        ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        ddl_client_name.Items.Clear();

    
    }
    protected void text_clear1()
    {
       // ddl_department.SelectedValue = "Select";
        ddl_employee_type.Items.Clear();
        txt_employee_code.Text = "";
        // ddl_client_name.SelectedIndex=-1;

        ddl_state_name.Items.Clear();
        ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        ddl_client_name.Items.Clear();


    }
    protected void btn_close_click(object sender, EventArgs e) {

        Response.Redirect("Home.aspx");
    }
    protected void gv_client_access_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_client_access.UseAccessibleHeader = false;
            gv_client_access.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}