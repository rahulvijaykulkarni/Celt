using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Collections;
using System.Web;
using System.Threading;
using System.Configuration;

public partial class Operation_management : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    int res = 0;
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            employee_list();
            
            //get_client_list();
            ddl_field_officer1();
          //  get_state_list();

            gv_operation_gridview();
            Travelling_Gridview1();
            btn_send_mail.Visible = false;
            //btndelete.Visible = false;
           
           // btn_update.Visible = false;
            ViewState["visible"] = 0;
        }
    }

    protected void get_state_list() {
        ddl_state_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(state_name) from pay_state_master", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_state_name.DataSource = dt_item;
                ddl_state_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_name.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_name.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_unitcode_without1.Items.Clear();
            ddl_unitcode1.Items.Clear();
        }

    
    }
    protected void get_client_list()
    {

        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
               

                ddl_client_name.DataSource = dt_item;
                ddl_client_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_name.DataValueField = dt_item.Columns[1].ToString();
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
            ddl_unitcode_without1.Items.Clear();
            ddl_unitcode1.Items.Clear();
        }

    }
    public void get_city(string state_name) 
    {
       

    }
    public void employee_list() 
    {
        try
        {
            d1.con.Open();
         //08-01-19
            MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct(EMP_CODE),(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME' FROM pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and GRADE_CODE ='AssMangOps' order by EMP_NAME", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddl_employee.DataSource = ds1.Tables[0];
                ddl_employee.DataValueField = "EMP_CODE";
                ddl_employee.DataTextField = "EMP_NAME";
                ddl_employee.DataBind();

                //ddl_field_officer.DataSource = ds1.Tables[0];
                //ddl_field_officer.DataValueField = "EMP_CODE";
                //ddl_field_officer.DataTextField = "EMP_NAME";
                //ddl_field_officer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ddl_employee.Items.Insert(0, new ListItem("Select", "Select"));
           // ddl_field_officer.Items.Insert(0, new ListItem("Select", "Select"));
            d1.con.Close();
        }  

    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
      
	  try
        {
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d1.con1.Open();
            Field_ofc_history_gv.DataSource = null;
            Field_ofc_history_gv.DataBind();
           // MySqlCommand cmd_1 = new MySqlCommand("SELECT  COUNT(`UNIT_CODE`)  from pay_op_management_details WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE = '" + ddl_field_officer.SelectedValue + "' AND `STATE` = '" + ddl_state.SelectedValue + "' AND `UNIT_CODE` = '" + ddl_branch.SelectedValue + "'", d1.con1);
           // MySqlDataReader dr1 = cmd_1.ExecuteReader();
            // DataTable dt1 = new DataTable();
            //dr1.Fill(dt1);

            string region_id = d.getsinglestring("SELECT  COUNT(`UNIT_CODE`)  from pay_op_management_details WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE = '" + ddl_field_officer.SelectedValue + "' AND `STATE` = '" + ddl_state.SelectedValue + "' AND `UNIT_CODE` = '" + ddl_branch.SelectedValue + "' and OPERATION_DATE='"+txt_schedule_date.Text+"' ");

            string check_date = d.getsinglestring("select unit_code from pay_op_management_details where CLIENT_CODE='" + ddl_client.SelectedValue + "' and EMP_CODE='" + ddl_field_officer.SelectedValue + "'  and STATE='" + ddl_state.SelectedValue + "' and OPERATION_DATE=str_to_date('" + txt_schedule_date.Text + "','%d-%m-%Y')");

            string current_date = ddl_branch.Text.ToString();

            if (ddl_schedule_type.SelectedValue=="0")
            {

            if (check_date == current_date)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This branch Already Added')", true);
                return;
            }

          }

            if (ddl_schedule_type.SelectedValue == "1") {

                string current_location = "" + ddl_branch.Text + "";
                if (check_date == current_location)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This branch Already Added')", true);
                    return;
                }

            
            }



            int rownum1 = 0, Num = 0;
            for (rownum1 = 0, Num = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
                {
                    if (gv_itemslist.Rows[rownum1].Cells[7].Text == ddl_state.SelectedValue && gv_itemslist.Rows[rownum1].Cells[4].Text == ddl_branch.SelectedValue)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Can Not shedule Same branch ...');", true);
                        Num = 1;
                        return;
                    }
                    //if (gv_itemslist.Rows[rownum1].Cells[9].Text == txt_schedule_date.Text && gv_itemslist.Rows[rownum1].Cells[3].Text == ddl_field_officer.SelectedValue)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Schedule Alredy Added For This Date ...');", true);
                    //    return;
                    //}
               
                }
         
                string visible = ViewState["visible"].ToString();
                if (Num == 0 && region_id=="0")
                {
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add("CLIENT_CODE");
                    dt.Columns.Add("EMP_CODE");
                    dt.Columns.Add("STATE");
                    dt.Columns.Add("UNIT_CODE");
                    dt.Columns.Add("EMP_NAME");
                    dt.Columns.Add("CLIENT_NAME");
                    dt.Columns.Add("UNIT_NAME");
                    dt.Columns.Add("OPERATION_DATE");
                    dt.Columns.Add("START_TIME");
                    dt.Columns.Add("END_TIME");
                    int rownum = 0;
                    for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
                    {
                        if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
                        {
                            dr = dt.NewRow();

                            dr["CLIENT_CODE"] = gv_itemslist.Rows[rownum].Cells[2].Text;
                            dr["EMP_CODE"] = gv_itemslist.Rows[rownum].Cells[3].Text;
                            dr["STATE"] = gv_itemslist.Rows[rownum].Cells[7].Text;
                            dr["UNIT_CODE"] = gv_itemslist.Rows[rownum].Cells[4].Text;
                            dr["EMP_NAME"] = gv_itemslist.Rows[rownum].Cells[5].Text;
                            dr["CLIENT_NAME"] = gv_itemslist.Rows[rownum].Cells[6].Text;
                            dr["UNIT_NAME"] = gv_itemslist.Rows[rownum].Cells[8].Text;

                            dr["OPERATION_DATE"] = gv_itemslist.Rows[rownum].Cells[9].Text;
                            dr["START_TIME"] = gv_itemslist.Rows[rownum].Cells[10].Text;
                            dr["END_TIME"] = gv_itemslist.Rows[rownum].Cells[11].Text;
                            dt.Rows.Add(dr);

                        }
                    }
                    dr = dt.NewRow();
                    dr["CLIENT_CODE"] = ddl_client.SelectedValue;
                    dr["EMP_CODE"] = ddl_field_officer.SelectedValue;
                    dr["STATE"] = ddl_state.SelectedValue;
                    dr["UNIT_CODE"] = ddl_branch.SelectedValue;
                    dr["EMP_NAME"] = ddl_field_officer.SelectedItem.Text;
                    dr["CLIENT_NAME"] = ddl_client.SelectedItem.Text;
                    dr["UNIT_NAME"] = ddl_branch.SelectedItem.Text;
                    dr["OPERATION_DATE"] = txt_schedule_date.Text;
                    dr["START_TIME"] = ddl_start_time.SelectedValue;
                    dr["END_TIME"] = ddl_end_time.SelectedValue;
                    dt.Rows.Add(dr);
                    gv_itemslist.DataSource = dt;
                    gv_itemslist.DataBind();

                    ViewState["visible"] = dt;

                    ddl_field_officer.SelectedIndex = 0;
                    ddl_client.Items.Clear();
                    ddl_state.Items.Clear();
                    ddl_branch.Items.Clear();
                    txt_schedule_date.Text = "";
                    ddl_start_time.SelectedValue = "Flexible";
                    ddl_end_time.SelectedValue = "Flexible";
                    ViewState["visible"] = 0;
                }

                else if (int.Parse(visible) == 0)
                {

                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add("CLIENT_CODE");
                    dt.Columns.Add("EMP_CODE");
                    dt.Columns.Add("STATE");
                    dt.Columns.Add("UNIT_CODE");
                    dt.Columns.Add("EMP_NAME");
                    dt.Columns.Add("CLIENT_NAME");
                    dt.Columns.Add("UNIT_NAME");
                    dt.Columns.Add("OPERATION_DATE");
                    dt.Columns.Add("START_TIME");
                    dt.Columns.Add("END_TIME");
                    int rownum = 0;
                    for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
                    {
                        if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
                        {
                            dr = dt.NewRow();

                            dr["CLIENT_CODE"] = gv_itemslist.Rows[rownum].Cells[2].Text;
                            dr["EMP_CODE"] = gv_itemslist.Rows[rownum].Cells[3].Text;
                            dr["STATE"] = gv_itemslist.Rows[rownum].Cells[7].Text;
                            dr["UNIT_CODE"] = gv_itemslist.Rows[rownum].Cells[4].Text;
                            dr["EMP_NAME"] = gv_itemslist.Rows[rownum].Cells[5].Text;
                            dr["CLIENT_NAME"] = gv_itemslist.Rows[rownum].Cells[6].Text;
                            dr["UNIT_NAME"] = gv_itemslist.Rows[rownum].Cells[8].Text;

                            dr["OPERATION_DATE"] = gv_itemslist.Rows[rownum].Cells[9].Text;
                            dr["START_TIME"] = gv_itemslist.Rows[rownum].Cells[10].Text;
                            dr["END_TIME"] = gv_itemslist.Rows[rownum].Cells[11].Text;
                            dt.Rows.Add(dr);

                        }
                    }
                    dr = dt.NewRow();
                    dr["CLIENT_CODE"] = ddl_client.SelectedValue;
                    dr["EMP_CODE"] = ddl_field_officer.SelectedValue;
                    dr["STATE"] = ddl_state.SelectedValue;
                    dr["UNIT_CODE"] = ddl_branch.SelectedValue;
                    dr["EMP_NAME"] = ddl_field_officer.SelectedItem.Text;
                    dr["CLIENT_NAME"] = ddl_client.SelectedItem.Text;
                    dr["UNIT_NAME"] = ddl_branch.SelectedItem.Text;
                    dr["OPERATION_DATE"] = txt_schedule_date.Text;
                    dr["START_TIME"] = ddl_start_time.SelectedValue;
                    dr["END_TIME"] = ddl_end_time.SelectedValue;
                    dt.Rows.Add(dr);
                    gv_itemslist.DataSource = dt;
                    gv_itemslist.DataBind();

                    ViewState["CurrentTable"] = dt;

                    ddl_field_officer.SelectedIndex = 0;
                    ddl_client.Items.Clear();
                    ddl_state.Items.Clear();
                    ddl_branch.Items.Clear();
                    txt_schedule_date.Text = "";
                    ddl_start_time.SelectedValue = "Flexible";
                    ddl_end_time.SelectedValue = "Flexible";
                    ViewState["visible"] = 0;
                }
               
                //else
                //{
                //    if (Num == 1) { }
                //    else
                //    {
                //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Branch  Already Travelling Shedule First Remove This')", true);
                //    }
                //}
               
            }
        
        catch (Exception ex) { throw ex; }
        finally{

            d1.con1.Close();
        }
	  
	      }
    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
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
        }
    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_itemslist, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;

    }
   
    protected void btnadd_Click(object sender, EventArgs e)
    {

       // int li_select = 0;


        //if (ddl_unitcode_without1.SelectedIndex > 0 || ddl_unitcode_without1.SelectedIndex == 0)
        //{
        //    for (int i = 0; i < ddl_unitcode_without1.Items.Count; i++)
        //    {
        //        if (ddl_unitcode_without1.Items[i].Selected)
        //        {
        //            if (!arraylist2.Contains(ddl_unitcode_without1.Items[i]))
        //            {
        //                arraylist2.Add(ddl_unitcode_without1.Items[i]);
        //            }
        //        }
        //    }
        //    for (int i = 0; i < arraylist2.Count; i++)
        //    {
        //        if (!ddl_unitcode1.Items.Contains(((ListItem)arraylist2[i])))
        //        {
        //            ddl_unitcode1.Items.Add(((ListItem)arraylist2[i]));
        //        }
        //        ddl_unitcode_without1.Items.Remove(((ListItem)arraylist2[i]));
        //    }
        //    ddl_unitcode1.SelectedIndex = -1;
        //}
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            IEnumerable<string> selectedValues = from item in ddl_unitcode_without1.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_unitcode = string.Join(",", selectedValues);

            //if (listvalues_ddl_unitcode == "") { listvalues_ddl_unitcode = string.Join(",", selectedValues); }
            //else
            //{
            //    listvalues_ddl_unitcode = listvalues_ddl_unitcode + "," + string.Join(",", selectedValues);
            //}



            //string temp = d1.getsinglestring("select distinct(POLICY_NAME1) FROM pay_billing_master where POLICY_NAME1 = '" + txt_policy_name1.Text + "'");
            //if (temp == txt_policy_name1.Text)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Name Already Exist !!');", true);
            //    return;
            //}

            //d.operation("delete from pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and POLICY_NAME1 = '" + txt_policy_name1.Text + "'");


            var elements = listvalues_ddl_unitcode.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

           
            if (elements.Length != 0)
            {
                foreach (string unit_code in elements)
                {

                
                res = d.operation("INSERT INTO pay_op_management (COMP_CODE,EMP_CODE,MOBILE_NO,CLIENT_CODE,STATE,CREATED_BY,field_officer_name,flag,unit_code)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_employee.SelectedValue + "','" + txt_mobile_no.Text + "','" + ddl_client_name.SelectedValue + "','" + ddl_state_name.SelectedValue + "','" + Session["LOGIN_ID"].ToString() + "','" + ddl_employee.SelectedItem.Text + "','1','" + unit_code.ToString() + "')");

                // res = d.operation("update pay_op_management  where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
            }
            
            if (res > 0)
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Field Officer Asign  successfully!!!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Field Officer Asign failed to Insert...')", true);
            }

            }
            else {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Branch First...!!')", true);
                ddl_unitcode_without1.Focus();
            }
            //Scheduled
            //try
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            //    if (gv_itemslist.Rows.Count > 0)
            //    {
            //        d.operation("Delete from pay_op_management where emp_code= '" + ddl_employee.SelectedValue + "'");
            //        foreach (GridViewRow row in gv_itemslist.Rows)
            //        {
            //            res = d.operation("INSERT INTO pay_op_management (COMP_CODE,EMP_CODE,MOBILE_NO,CLIENT_CODE,STATE,UNIT_CODE,OPERATION_DATE,START_TIME,END_TIME,CREATED_BY)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_employee.SelectedValue + "','" + txt_mobile_no.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[5].Text + "','%d/%m/%Y'),'" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + Session["LOGIN_ID"].ToString() + "')");
            //            if (res > 0)
            //            {
            //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Schedule Inserted successfully!!')", true);

            //            }
            //            else
            //            {
            //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Schedule failed to Insert...')", true);
            //            }
            //        }
            //    }
            //    else {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Schedule Operations !!')", true);
            //    }

            //}
        }
            catch (Exception ex) { }
            finally 
                 {
                     if (res > 0)
                     {
                         ddl_employee.SelectedValue = "Select";
                        // ddl_state_name.SelectedValue = "Select";
                        // ddl_state_SelectedIndexChanged1(null, null);
                         ddl_state_name.Items.Clear();
                         ddl_client_name.Items.Clear();
                         ddl_unitcode_without1.Items.Clear();
                         ddl_unitcode1.Items.Clear();
                         txt_mobile_no.Text = "";
                         gv_itemslist.DataSource = null;
                         gv_itemslist.DataBind();
                         ddl_field_officer1();
                         gv_operation_gridview();
                     }
               }
        }
    protected void btnClear_Click(object sender, EventArgs e) {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        ddl_employee.SelectedValue = "Select";
        ddl_state_name.Items.Clear(); ;
       // ddl_state_SelectedIndexChanged1(null, null);
        ddl_client_name.Items.Clear();
       ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        txt_mobile_no.Text = "";
        //btn_delete.Visible = false;
        //btn_save_op.Visible = true;
      
    
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

    }
    //protected void btndelete_Click(object sender, EventArgs e)
    //{
    //    if (gv_itemslist.Rows.Count > 0)
    //    {
    //        d.operation("Delete from pay_op_management where emp_code= '" + ddl_employee.SelectedValue + "'");
    //    }
    //    else {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Schedule Operations !!')", true);
    //    }
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void ddl_state_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddl_state_name.SelectedValue != "Select")
        {
            //State
          //  ddl_state.Items.Clear();
            ddl_client_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(client_code),client_name from pay_client_master where comp_code='"+Session["COMP_CODE"].ToString()+"' and `STATE`=(select distinct(`STATE_CODE`) from pay_state_master where state_name='"+ddl_state_name.SelectedValue+"') order by 1", d1.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (pay_client_master.client_code), pay_client_master.client_name FROM pay_client_master INNER JOIN pay_unit_master ON pay_client_master.COMP_CODE = pay_unit_master.COMP_CODE AND pay_client_master.CLIENT_CODE = pay_unit_master.client_code INNER JOIN pay_state_master ON pay_unit_master.STATE_NAME = pay_state_master.STATE_NAME WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.state_name = '" + ddl_state_name.SelectedValue + "' and client_active_close='0' ORDER BY 1", d1.con);
            
            
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {


                    ddl_client_name.DataSource = dt_item;
                    ddl_client_name.DataValueField = dt_item.Columns[0].ToString();
                    ddl_client_name.DataTextField = dt_item.Columns[1].ToString();
                   
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
                ddl_unitcode_without1.Items.Clear();
                ddl_unitcode1.Items.Clear();

            }

        }
        else
        {
            ddl_branch.Items.Clear();

        }
    }
  //NOT USE
    //protected void ddl_details_client(object sender, EventArgs e)
    //{
    //    if (ddl_client.SelectedValue != "Select")
    //    {
    //        //State
    //        //  ddl_state.Items.Clear();
    //        ddl_state_name.Items.Clear();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT client_name,pay_op_management.STATE,(Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)) as UNIT_NAME FROM pay_op_management INNER JOIN pay_client_master ON pay_op_management.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_op_management.COMP_CODE = pay_client_master.COMP_CODE inner join pay_unit_master on pay_op_management.unit_code=pay_unit_master.unit_code and pay_op_management.comp_code=pay_unit_master.comp_code WHERE pay_op_management.comp_code = '"+Session["COMP_CODE"].ToString()+"' AND field_officer_name = '" + ddl_field_officer .SelectedValue+ "' ", d.con);
    //        d.con.Open();
    //        try
    //        {
    //            cmd_item.Fill(dt_item);
    //            if (dt_item.Rows.Count > 0)
    //            {


    //                ddl_state_name.DataSource = dt_item;
    //                ddl_state_name.DataTextField = dt_item.Columns[0].ToString();
    //                ddl_state_name.DataValueField = dt_item.Columns[0].ToString();
    //                ddl_state_name.DataBind();
    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //            ddl_state_name.Items.Insert(0, "Select");
    //        }
    //        catch (Exception ex) { throw ex; }
    //        finally
    //        {
    //            d.con.Close();
    //        }

    //    }
    
    //}


    //gridview field officer

    protected void ddl_field_officer1() 
    {

        try
        {

            d1.con.Open();
			//vikas 08-01-19
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT  (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_op_management.EMP_CODE FROM pay_op_management INNER JOIN pay_employee_master ON pay_op_management.EMP_CODE =pay_employee_master.EMP_CODE AND pay_op_management.COMP_CODE =pay_employee_master.COMP_CODE WHERE pay_op_management.comp_code = '" + Session["COMP_CODE"].ToString() + "' ", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                //ddl_employee.DataSource = ds1.Tables[0];
                //ddl_employee.DataValueField = "EMP_CODE";
                //ddl_employee.DataTextField = "EMP_NAME";
                //ddl_employee.DataBind();

                ddl_field_officer.DataSource = ds1.Tables[0];
                ddl_field_officer.DataValueField = "EMP_CODE";
                ddl_field_officer.DataTextField = "EMP_NAME";
                ddl_field_officer.DataBind();
                cad1.Dispose();
                d1.con.Close();
            }
            ddl_field_officer.Items.Insert(0,"Select");
        }
        catch (Exception e) { throw e; }
        finally {
            
            d1.con.Close();
            
        }
    }
    protected void ddl_field_officer_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_field_officer.SelectedValue != "Select")
        {
            try
            {

                d1.con.Open();
                MySqlCommand cmd_1 = new MySqlCommand("SELECT  DISTINCT pay_client_master.client_name AS client_name, pay_client_master.client_code AS client_code FROM  pay_client_master INNER JOIN pay_op_management ON pay_client_master.CLIENT_CODE = pay_op_management.CLIENT_CODE WHERE pay_op_management.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND   pay_op_management.EMP_CODE = '" + ddl_field_officer.SelectedValue + "' and client_active_close='0' ORDER BY client_code ", d1.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                DataSet ds1 = new DataSet();
                cad1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //ddl_employee.DataSource = ds1.Tables[0];
                    //ddl_employee.DataValueField = "EMP_CODE";
                    //ddl_employee.DataTextField = "EMP_NAME";
                    //ddl_employee.DataBind();

                    ddl_client.DataSource = ds1.Tables[0];
                    ddl_client.DataValueField = "client_code";
                    ddl_client.DataTextField = "client_name";
                    ddl_client.DataBind();

                }
                cad1.Dispose();
                d1.con.Close();
                ddl_client.Items.Insert(0, "Select");
                Travelling_Gridview1();
            }
            catch (Exception ex) { throw ex; }
            finally
            {

                d1.con.Close();
            }

            try
            {
                Field_ofc_history_gv.DataSource = null;
                Field_ofc_history_gv.DataBind();

                d1.con.Open();
                string where = "FROM pay_op_management_details INNER JOIN pay_client_master ON pay_op_management_details.comp_code = pay_client_master.comp_code AND pay_op_management_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_op_management_details.comp_code = pay_unit_master.comp_code AND pay_op_management_details.unit_code = pay_unit_master.unit_code LEFT OUTER JOIN pay_site_audit ON pay_op_management_details.EMP_CODE = pay_site_audit.EMP_CODE WHERE  pay_op_management_details.EMP_CODE = '" + ddl_field_officer.SelectedValue + "' ";
                string where1 = "FROM pay_site_audit INNER JOIN `pay_client_master` ON `pay_site_audit`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_site_audit`.`client_code` = `pay_client_master`.`client_code` INNER JOIN `pay_unit_master` ON `pay_site_audit`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_site_audit`.`unit_code` = `pay_unit_master`.`unit_code` LEFT OUTER JOIN `pay_op_management_details` ON `pay_site_audit`.`EMP_CODE` = `pay_op_management_details`.`EMP_CODE` WHERE  pay_site_audit.EMP_CODE = '" + ddl_field_officer.SelectedValue + "'";


                MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT field_officer_name, pay_client_master.client_name, pay_op_management_details.STATE, pay_unit_master.UNIT_NAME, DATE_FORMAT(OPERATION_DATE, '%d/%m/%y') AS 'Operation_date', pay_op_management_details.START_TIME, pay_op_management_details.END_TIME, pay_op_management_details.flag as 'reject'" + where + " UNION SELECT DISTINCT field_officer_name , pay_client_master . client_name ,  '' AS 'STATE',pay_unit_master . UNIT_NAME , DATE_FORMAT( cur_date , '%d/%m/%y') AS 'Operation_date', pay_op_management_details . START_TIME , pay_op_management_details . END_TIME , pay_site_audit . reject  " + where1 + " AND reject = 3 ", d1.con);
                //MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct field_officer_name,pay_client_master.client_name,pay_op_management_details.STATE,pay_unit_master.UNIT_NAME, DATE_FORMAT(`OPERATION_DATE`, '%d/%m/%y') as Operation_date,pay_op_management_details.START_TIME,pay_op_management_details.END_TIME,pay_site_audit.reject from  pay_op_management_details  INNER JOIN pay_client_master ON pay_op_management_details.comp_code = pay_client_master.comp_code AND pay_op_management_details.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_op_management_details.comp_code = pay_unit_master.comp_code AND pay_op_management_details.unit_code = pay_unit_master.unit_code   LEFT OUTER JOIN  pay_site_audit on `pay_op_management_details`.`EMP_CODE` = `pay_site_audit`.`EMP_CODE` where pay_op_management_details.EMP_CODE='" + ddl_field_officer.SelectedValue + "' ", d1.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                DataSet ds1 = new DataSet();
                cad1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Field_ofc_history_gv.DataSource = ds1.Tables[0];
                    Field_ofc_history_gv.DataBind();

                }
                cad1.Dispose();
                d1.con.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {

                d1.con.Close();
            }
        }
        else {
            ddl_client.Items.Insert(0, "Select");
        
        }

        
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
       // if (ddl_client.SelectedValue != "Select")
      //  {
            //State
            ddl_state.Items.Clear();
           
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (STATE_NAME) FROM pay_unit_master INNER JOIN pay_op_management on pay_unit_master.STATE_NAME = pay_op_management.STATE WHERE pay_op_management.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND  pay_op_management.EMP_CODE='" + ddl_field_officer.SelectedValue + "' AND pay_op_management.client_code = '" + ddl_client.SelectedValue + "'  ORDER BY 1", d.con);
           
            d.con.Open();
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
                d.con.Close();
                ddl_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

      //  }
        //else
        //{
        //    ddl_branch.Items.Clear();
          
        //}
    }
    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddl_client_name.SelectedValue != "Select")
        {
            ddl_branch.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            System.Data.DataTable dt_item1 = new System.Data.DataTable();
            d.con.Open();
            try
            {

                MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "'and unit_code not in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "') AND branch_status = 0  ORDER BY UNIT_NAME", d.con);
          
                   
                    DataSet ds1 = new DataSet();
                    cmd_item.Fill(ds1);
                    ddl_unitcode_without1.DataSource = ds1.Tables[0];
                    ddl_unitcode_without1.DataValueField = "unit_code";
                    ddl_unitcode_without1.DataTextField = "UNIT_NAME";
                    ddl_unitcode_without1.DataBind();
                
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
               
               // MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master where emp_code in (select emp_code from pay_travel_emp_policy where policy_id = " + ddlpolicies.SelectedValue + ") and comp_code ='" + Session["comp_code"] + "'  order by EMP_NAME", d.con);
                MySqlCommand cmd_1 = new MySqlCommand("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, pay_op_management.unit_code from pay_op_management inner join pay_unit_master on pay_op_management.unit_code=pay_unit_master.unit_code and pay_op_management.comp_code=pay_unit_master.comp_code  where pay_op_management.comp_code='" + Session["comp_code"].ToString() + "' and pay_op_management.client_code = '" + ddl_client_name.SelectedValue + "' and pay_unit_master.state_name='" + ddl_state_name.SelectedValue + "' and pay_unit_master.unit_code in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_client_name.SelectedValue + "' and EMP_CODE='" + ddl_employee.SelectedValue + "') ORDER BY pay_op_management.UNIT_CODE", d.con);
                 MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
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
        
        else
        {
            ddl_branch.Items.Clear();
        }
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddl_client.SelectedValue != "Select")
        //{
           
            try
            {

                // MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master where emp_code in (select emp_code from pay_travel_emp_policy where policy_id = " + ddlpolicies.SelectedValue + ") and comp_code ='" + Session["comp_code"] + "'  order by EMP_NAME", d.con);
                MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'UNIT_NAME', pay_op_management.unit_code FROM pay_op_management INNER JOIN pay_unit_master ON pay_op_management.unit_code = pay_unit_master.unit_code AND pay_unit_master.COMP_CODE =  pay_op_management.COMP_CODE  INNER JOIN pay_client_master ON pay_client_master.CLIENT_CODE = pay_op_management.CLIENT_CODE AND  pay_client_master.client_code = pay_unit_master.client_code   WHERE pay_op_management.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_op_management.CLIENT_CODE='" + ddl_client.SelectedValue + "' AND pay_op_management.STATE='" + ddl_state.SelectedValue + "' AND pay_op_management.EMP_CODE ='" + ddl_field_officer.SelectedValue + "' AND branch_status = 0 ", d.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                DataSet ds1 = new DataSet();

                cad1.Fill(ds1);
                ddl_branch.DataSource = ds1.Tables[0];
                ddl_branch.DataValueField = "unit_code";
                ddl_branch.DataTextField = "UNIT_NAME";
                ddl_branch.DataBind();

                cad1.Dispose();
                d.con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                
                ddl_branch.Items.Insert(0, "Select");
                d.con.Close();
            }
        

       // else
        //{
          //  ddl_branch.Items.Clear();

    }
    protected void gv_itemslist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_MOBILE_NO from  pay_employee_master  WHERE comp_code='" + Session["comp_code"].ToString() + "' and emp_code = '"+ ddl_employee.SelectedValue +"'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item = cmd1.ExecuteReader();
           if(dr_item.Read())
            {
                txt_mobile_no.Text = dr_item.GetValue(0).ToString();
            }
            dr_item.Close();
        }catch (Exception ex) { throw ex; }
        finally{d.con.Close();

        //ddl_state_name.SelectedValue = "Select";
       // ddl_state_SelectedIndexChanged1(null, null);
        ddl_client_name.Items.Clear();
        ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        //txt_mobile_no.Text = "";
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        ddl_field_officer1();
        gv_operation_gridview();
        get_state_list();
        
        }
        
        // Gridview
        //d1.con.Open();
        //MySqlDataAdapter adp = new MySqlDataAdapter("Select client_code,state,unit_code,date_format(OPERATION_DATE,'%d/%m/%Y') as OPERATION_DATE,start_time,end_time From pay_op_management where emp_code = '" + ddl_employee.SelectedValue + "' and comp_code = '"+ Session["COMP_CODE"].ToString() +"'", d1.con);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    gv_itemslist.DataSource = ds.Tables[0];
        //    gv_itemslist.DataBind();
        //    ViewState["CurrentTable"] = ds.Tables[0];
        //    btn_send_mail.Visible = true;
        //    btndelete.Visible = true;
        //}
        //else {
        //    gv_itemslist.DataSource = null;
        //    gv_itemslist.DataBind();
        //    btn_send_mail.Visible = false;
        //    btndelete.Visible = false;
        //}
        //d1.con.Close();
    }
    protected void btn_send_mail_Click(object sender, EventArgs e)
    {
        if (gv_itemslist.Rows.Count > 0)
        {
            string email = "";
            MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_EMAIL_ID from  pay_employee_master  WHERE comp_code='" + Session["comp_code"].ToString() + "' and emp_code = '" + ddl_employee.SelectedValue + "'", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item = cmd1.ExecuteReader();
                if (dr_item.Read())
                {
                    email = dr_item.GetValue(0).ToString();
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }

            int rowcount = 0;
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    rowcount++;
                }
            }
            string strHTML = "<html><STYLE='font-size: 12px;'>" +
                "<body>" +
                "<table border='1'>" +
                "<thead>" + "<tr>" + "Your Operation Has Been Schedule as per Below:" + "</tr>" +
                   "<tr>" + "<th>CLIENT</th>" + "<th>STATE</th>" + "<th>BRANCH</th>" + "<th>DATE</th>" + "<th>START TIME</th>" +
                     "<th>END TIME</th>" +
                  "</tr>" +
                "</thead>";
            for (int a = 0; a < rowcount; a = a + 1)
            {
                strHTML = strHTML + "<tr>";
                for (int b = 2; b < 8; b = b + 1)
                {
                    strHTML = strHTML + "<td>" + gv_itemslist.Rows[a].Cells[b].Text + "</td>";
                }
            }
            strHTML = strHTML + "</tr></table>";

            send_email(strHTML, email);
        }
        else {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Schedule Operations !!')", true);
        }
    }
    private void send_email(string emailhtmlfile,string tomail)
    {
        string body = string.Empty;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

            mail.From = new MailAddress("info@celtsoft.com");
            mail.To.Add(tomail);
            mail.Subject = "Operation Schedule";
            mail.Body = emailhtmlfile;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
        }
        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Internet... Email not sent !!!')", false); }

    }
    
    //Gridview submit MD

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (gv_itemslist.Rows.Count > 0)
        {
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                //res = d.operation("INSERT INTO pay_op_management (COMP_CODE,CLIENT_CODE,STATE,EMP_CODE,unit_code,OPERATION_DATE,START_TIME,END_TIME,)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_employee.SelectedValue + "','" + txt_mobile_no.Text + "','" + ddl_client_name.SelectedValue + "','" + ddl_state_name.SelectedValue + "','" + Session["LOGIN_ID"].ToString() + "','" + ddl_employee.SelectedValue + "','1','" ++ "')");

                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    res = d.operation("DELETE FROM pay_op_management_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + row.Cells[3].Text.ToString() + "' and state='" + row.Cells[7].Text.ToString() + "' and unit_code='" + row.Cells[4].Text.ToString() + "' and `flag`='2' ");
                    res = d.operation("INSERT INTO pay_op_management_details (COMP_CODE,EMP_CODE,CLIENT_CODE,STATE,unit_code,OPERATION_DATE,START_TIME,END_TIME,field_officer_name,flag)VALUES('" + Session["COMP_CODE"].ToString() + "','" + row.Cells[3].Text + "','" + row.Cells[2].Text + "','" + row.Cells[7].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[9].Text + "','%d-%m-%Y'),'" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[5].Text + "','5')");
                }
               
                foreach (GridViewRow Row in gv_itemslist.Rows)
                {
                    travel_mail_send(Row.Cells[2].Text.ToString(), Row.Cells[3].Text.ToString(), Row.Cells[7].Text.ToString(), Row.Cells[4].Text.ToString(), Row.Cells[9].Text.ToString(), Row.Cells[10].Text.ToString(), Row.Cells[11].Text.ToString());
                }
               
                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Travelling Shedule  Created successfully!!')", true);
                    gv_itemslist.DataSource = null;
                    gv_itemslist.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failed to Insert...')", true);
                }
                

            }
            catch (Exception ex) { }
            finally
            {
                d.con.Close();
                Travelling_Gridview1();
            }
        }
        else {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Save Travelling Shedule!!!!')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
   
        
        }
    }

    protected void gv_operation_gridview() {

        try
        {
            d1.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(pay_op_management.id) AS 'Id',pay_op_management.UNIT_CODE AS 'UNIT_CODE',pay_op_management.COMP_CODE AS 'COMP_CODE',pay_op_management.field_officer_name AS 'field_officer_name',pay_op_management.MOBILE_NO AS 'MOBILE_NO',pay_client_master.CLIENT_NAME AS 'CLIENT_NAME',pay_op_management.STATE AS 'STATE', CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_op_management.STATE),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS 'UNIT_NAME' FROM pay_op_management INNER JOIN pay_unit_master ON pay_op_management.UNIT_CODE = pay_unit_master.UNIT_CODE  and pay_op_management.COMP_CODE = pay_unit_master.COMP_CODE INNER JOIN pay_client_master  ON pay_client_master.CLIENT_CODE = pay_op_management.CLIENT_CODE AND  pay_client_master.client_code = pay_unit_master.client_code WHERE pay_op_management.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and client_active_close='0' order by pay_op_management.Id desc", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet DS1 = new DataSet();
            cad1.Fill(DS1);
            gv_operation.DataSource = DS1;
            gv_operation.DataBind();
            cad1.Dispose();
        }
        catch (Exception ex)
        {

        } 
        finally {
            d1.con.Close();
            
        
        }
    }
    protected void gv_operation_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int i = 0; i < e.Row.Cells.Count; i++)
        //{
        //    if (e.Row.Cells[i].Text == "&nbsp;")
        //    {
        //        e.Row.Cells[i].Text = "";
        //    }
        //}
       // e.Row.Cells[0].Visible = false;
        //e.Row.Cells[1].Visible = false;
       // e.Row.Cells[2].Visible = false;
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_operation, "Select$" + e.Row.RowIndex);

        //}
     
    
    }

    protected void gv_operation_OnSelectedIndexChanged(object sender, EventArgs e) {

        //System.Web.UI.WebControls.Label lbl_Id_CODE1 = (System.Web.UI.WebControls.Label)gv_operation.SelectedRow.FindControl("lbl_Id_CODE");
        //string lbl_grade_code = lbl_Id_CODE1.Text;

        //try
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //    d2.con.Open();
        //    MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(pay_op_management.id) AS 'Id',pay_op_management.EMP_CODE,pay_op_management.MOBILE_NO,pay_client_master.CLIENT_CODE,pay_op_management.STATE, CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_op_management.STATE),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS 'UNIT_NAME' FROM pay_op_management INNER JOIN pay_unit_master ON pay_op_management.UNIT_CODE = pay_unit_master.UNIT_CODE  and pay_op_management.COMP_CODE = pay_unit_master.COMP_CODE INNER JOIN pay_client_master  ON pay_client_master.CLIENT_CODE = pay_op_management.CLIENT_CODE AND  pay_client_master.client_code = pay_unit_master.client_code WHERE pay_op_management.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_op_management.Id='" + lbl_grade_code + "'  order by pay_op_management.Id desc", d2.con);

        //    MySqlDataReader dr = cmd_1.ExecuteReader();
        //    while (dr.Read())
        //    {

        //        ddl_employee.SelectedValue = dr.GetValue(1).ToString();
       
        //        txt_mobile_no.Text = dr.GetValue(2).ToString();
        //        get_state_list();
        //        ddl_state_name.SelectedValue = dr.GetValue(4).ToString();
        //        ddl_state_SelectedIndexChanged1(null, null);
        //        ddl_client_name.SelectedValue = dr.GetValue(3).ToString();
               
        //        ddl_unitcode1_SelectedIndexChanged1();
        //        ddl_unitcode1.SelectedValue = dr.GetValue(5).ToString();
        //        dr.Dispose();
        //        d2.con.Close();

        //    }

        //}
        //catch (Exception ex)
        //{

        //}
        //finally {

        //    btn_save_op.Visible = false;
        //    btn_delete.Visible = true;
        //    d2.con.Close();
        //}

    }

    protected void ddl_unitcode1_SelectedIndexChanged1() {
        System.Web.UI.WebControls.Label lbl_UNIT_CODE1 = (System.Web.UI.WebControls.Label)gv_operation.SelectedRow.FindControl("lbl_UNIT_CODE");
        string lbl_unit_code = lbl_UNIT_CODE1.Text;

        
        try
        {
            d.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("Select DISTINCT CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state_name.SelectedValue + "'and unit_code  in (select UNIT_CODE from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' and UNIT_CODE = '" + lbl_unit_code+ "') ORDER BY UNIT_CODE ", d.con);
            MySqlDataAdapter dt = new MySqlDataAdapter(cmd_1);
             DataSet ds1 = new DataSet();
              
                    dt.Fill(ds1);
                    ddl_unitcode1.DataSource = ds1.Tables[0];
                    ddl_unitcode1.DataValueField = "unit_code";
                    ddl_unitcode1.DataTextField = "UNIT_NAME";
                    ddl_unitcode1.DataBind();
                
                  dt.Dispose();
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
    protected void btndelete_Click(object sender, EventArgs e)
    {
     d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            IEnumerable<string> selectedValues = from item in ddl_unitcode1.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_unitcode = string.Join(",", selectedValues);

           
            var elements = listvalues_ddl_unitcode.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

           
            if (elements.Length != 0)
            {
                foreach (string unit_code in elements)
                {


                    res = d.operation("Delete  from pay_op_management where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE = '" + unit_code + "'");

                    // res = d.operation("update pay_op_management  where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
                }

                if (res > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Field Officer Remove  successfully!!!!');", true);
                    item_clear();
                    //btn_delete.Visible = false;
                    //btn_save_op.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please select branch first');", true);
                }
            }
            else {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Branch First....');", true);
                ddl_unitcode1.Focus();
            }

          
        }
            catch (Exception ex) { }
            finally 
                 {
                     
            }


    }


    //Travelling_Gridview 
    protected void Travelling_Gridview1()
    {

        try
        {
            d1.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(pay_op_management_details.id) AS 'Id',pay_op_management_details.UNIT_CODE AS 'UNIT_CODE',pay_op_management_details.COMP_CODE,pay_op_management_details.field_officer_name AS 'field_officer_name',pay_client_master.CLIENT_NAME AS 'CLIENT_NAME',pay_op_management_details.STATE AS 'STATE',CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_op_management_details.STATE), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'UNIT_NAME',DATE_FORMAT(pay_op_management_details.OPERATION_DATE,'%d-%m-%Y') AS 'DATE',pay_op_management_details.START_TIME AS 'START_TIME',pay_op_management_details.END_TIME AS 'END_TIME',pay_op_management_details.status,pay_op_management_details.comment FROM pay_op_management_details INNER JOIN pay_unit_master ON pay_op_management_details.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_op_management_details.COMP_CODE = pay_unit_master.COMP_CODE INNER JOIN pay_client_master ON pay_client_master.CLIENT_CODE = pay_op_management_details.CLIENT_CODE AND pay_client_master.client_code = pay_unit_master.client_code WHERE pay_op_management_details.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' ORDER BY pay_op_management_details.Id DESC", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataTable DS1 = new DataTable();
            cad1.Fill(DS1);
            Travelling_Gridview.DataSource = DS1;
            ViewState["Travelling_grid"] = DS1;
            Travelling_Gridview.DataBind();

            cad1.Dispose();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            d1.con.Close();


        }
    }

    protected void Travelling_Gridview_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[11].Text == "Not Available")
            {
                e.Row.Cells[13].Text = "Reschedule";
            }
            if (e.Row.Cells[11].Text == "Available")
            {
                e.Row.Cells[13].Text = "Available";
            }
            if (e.Row.Cells[11].Text == "")
            {
                e.Row.Cells[13].Text = "new";
            }
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            e.Row.Cells[11].Visible = false;
        }
        //e.Row.Cells[0].Visible = false;
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.Travelling_Gridview, "Select$" + e.Row.RowIndex);
        }
        try
        {
            GridViewRow Travelling_Gridview = e.Row;
            if (Travelling_Gridview.Cells[13].Text.Equals("Available"))
            {
                e.Row.BackColor = System.Drawing.Color.Green;
            }
            if (Travelling_Gridview.Cells[13].Text.Equals("Reschedule"))
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void Travelling_Gridview_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        System.Web.UI.WebControls.Label lbl_Id_CODE1 = (System.Web.UI.WebControls.Label)Travelling_Gridview.SelectedRow.FindControl("lbl_Id_CODE");
        string lbl_grade_code = lbl_Id_CODE1.Text;

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d2.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(pay_op_management_details.id) AS 'Id',pay_op_management_details.EMP_CODE,pay_client_master.client_code,pay_op_management_details.STATE AS 'STATE',CONCAT((SELECT DISTINCT (STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_op_management_details.STATE), '_', UNIT_CITY, '_', UNIT_ADD1, '_', UNIT_NAME) AS 'UNIT_NAME',pay_op_management_details.UNIT_CODE,DATE_FORMAT(pay_op_management_details.OPERATION_DATE,'%d-%m-%Y') AS 'DATE',pay_op_management_details.START_TIME AS 'START_TIME',pay_op_management_details.END_TIME AS 'END_TIME' FROM pay_op_management_details INNER JOIN pay_unit_master ON pay_op_management_details.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_op_management_details.COMP_CODE = pay_unit_master.COMP_CODE INNER JOIN pay_client_master ON pay_client_master.CLIENT_CODE = pay_op_management_details.CLIENT_CODE AND pay_client_master.client_code = pay_unit_master.client_code WHERE pay_op_management_details.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_op_management_details.Id = '" + lbl_grade_code + "' ORDER BY pay_op_management_details.Id DESC", d2.con);

            MySqlDataReader dr = cmd_1.ExecuteReader();
            while (dr.Read())
            {

                ddl_field_officer.SelectedValue = dr.GetValue(1).ToString();
                ddl_field_officer_SelectedIndexChanged(null,null);
                ddl_client.SelectedValue = dr.GetValue(2).ToString();
                ddl_client_SelectedIndexChanged(null,null);
                ddl_state.SelectedValue = dr.GetValue(3).ToString();
                ddl_state_SelectedIndexChanged(null, null);
                ddl_branch.SelectedValue = dr.GetValue(5).ToString();
                txt_schedule_date.Text = dr.GetValue(6).ToString();
                ddl_start_time.SelectedValue = dr.GetValue(7).ToString();
                ddl_end_time.SelectedValue = dr.GetValue(8).ToString();
               
                
                dr.Dispose();
                d2.con.Close();
                Field_ofc_history_gv.DataSource = null;
                Field_ofc_history_gv.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
        finally
        {


            d2.con.Close();
            ViewState["visible"] = 1;
            //btn_update.Visible = true;
           // btndelete.Visible = true;
            gv_itemslist.DataSource = null;
            gv_itemslist.DataBind();
            
        }

    }

    protected void item_clear()
    {


        ddl_employee.SelectedValue = "Select";
        ddl_state_name.Items.Clear();
       // ddl_state_SelectedIndexChanged1(null, null);
        ddl_client_name.Items.Clear();
        ddl_unitcode_without1.Items.Clear();
        ddl_unitcode1.Items.Clear();
        txt_mobile_no.Text = "";
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        ddl_field_officer1();
        gv_operation_gridview();
        //btn_delete.Visible = false;
        //btn_save_op.Visible = true;
       
    }

    //protected void btn_update_Click(object sender, EventArgs e) {
    //    System.Web.UI.WebControls.Label lbl_Id_CODE1 = (System.Web.UI.WebControls.Label)Travelling_Gridview.SelectedRow.FindControl("lbl_Id_CODE");
    //    string lbl_grade_code = lbl_Id_CODE1.Text;

    //    if (gv_itemslist.Rows.Count >0)
    //    {
    //        d.con.Open();
    //        try
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //            //res = d.operation("UPDATE pay_op_management_details  SET EMP_CODE = '" + ddl_field_officer.SelectedValue + "',CLIENT_CODE = '" + ddl_client.SelectedValue + "', STATE = '" + ddl_state.SelectedValue + "',UNIT_CODE='" + ddl_branch.SelectedValue + "',OPERATION_DATE = str_to_date('" + txt_schedule_date.Text + "','%d-%m-%Y'), START_TIME = '" + ddl_start_time.SelectedValue + "',END_TIME = '" + ddl_end_time.SelectedValue + "',field_officer_name ='" + ddl_field_officer.SelectedItem.Text + "' WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Id='" +lbl_grade_code+ "' ");
    //            res = d.operation("DELETE FROM pay_op_management_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Id='" + lbl_grade_code + "'");
    //            foreach (GridViewRow row in gv_itemslist.Rows)
    //            {
    //                res = d.operation("INSERT INTO pay_op_management_details (COMP_CODE,EMP_CODE,CLIENT_CODE,STATE,unit_code,OPERATION_DATE,START_TIME,END_TIME,field_officer_name)VALUES('" + Session["COMP_CODE"].ToString() + "','" + row.Cells[3].Text + "','" + row.Cells[2].Text + "','" + row.Cells[7].Text + "','" + row.Cells[4].Text + "',str_to_date('" + row.Cells[9].Text + "','%d-%m-%Y'),'" + row.Cells[10].Text + "','" + row.Cells[11].Text + "','" + row.Cells[5].Text + "')");

    //            }
    //            if (res > 0)
    //            {

    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Shedule  Updated  successfully!!!!');", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Shedule  Updation  Faild!!!!')", true);
    //            }


    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {

    //            d.con.Close();
    //            ddl_field_officer.SelectedIndex = 0;
    //            ddl_client.Items.Clear();
    //            ddl_state.Items.Clear();
    //            ddl_branch.Items.Clear();
    //            txt_schedule_date.Text = "";
    //            ddl_start_time.SelectedValue = "Flexible";
    //            ddl_end_time.SelectedValue = "Flexible";
    //            Travelling_Gridview1();
    //            //  lnkbtn_addmoreitem_Click(null,null);
    //            gv_itemslist.DataSource = null;
    //            btn_update.Visible = false;
    //            btn_submit.Visible = true;
    //            ViewState["visible"] = 0;
    //            gv_itemslist.DataBind();

    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Click on Link button!!!!')", true);
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    
    
    //}

    protected void lnkbtn_removetravelling_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

        if (ViewState["Travelling_grid"] != null)
        {
            System.Data.DataTable dt = (DataTable)ViewState["Travelling_grid"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    d.operation("Delete  from pay_op_management_details where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + dt.Rows[rowID][0] +"'");
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["Travelling_grid"] = dt;
            Travelling_Gridview.DataSource = dt;
            Travelling_Gridview.DataBind();
        }
        
        //  System.Web.UI.WebControls.Label lbl_Id_CODE1 = (System.Web.UI.WebControls.Label)Travelling_Gridview.SelectedRow.FindControl("lbl_Id_CODE");
        //string lbl_grade_code = lbl_Id_CODE1.Text;

        //d.con.Open();
        //try
        //{

        //    res = d.operation("Delete  from pay_op_management_details where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND Id = '" + lbl_grade_code + "'");

        //        // res = d.operation("update pay_op_management  where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
        

        //    if (res > 0)
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Shedule  deleted  successfully!!!!');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Shedule  failed to Remove...')", true);
        //    }



        //}
        //catch (Exception ex) { }
        //finally
        //{
        //    d.con.Close();
        //    ddl_field_officer.SelectedIndex = 0;
        //    ddl_client.Items.Clear();
        //    ddl_state.Items.Clear();
        //    ddl_branch.Items.Clear();
        //    txt_schedule_date.Text = "";
        //    ddl_start_time.SelectedValue = "Flexible";
        //    ddl_end_time.SelectedValue = "Flexible";
        //    Travelling_Gridview1();
        //    gv_itemslist.DataSource = null;
        //    gv_itemslist.DataBind();
        //}


    }


    protected void Travelling_item_clear(object sender, EventArgs e)
    {

        ddl_field_officer.SelectedIndex = 0;
        ddl_client.Items.Clear();
        ddl_state.Items.Clear();
        ddl_branch.Items.Clear();
        txt_schedule_date.Text = "";
        ddl_start_time.SelectedValue = "Flexible";
        ddl_end_time.SelectedValue = "Flexible";
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        btn_submit.Visible = true;
        //btn_update.Visible = false;
        
    }

    protected void gv_operation_PreRender1(object sender, EventArgs e)
    {
 
        try
        {
            gv_operation.UseAccessibleHeader = false;
            gv_operation.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    
    }
    protected void Travelling_Gridview_PreRender(object sender, EventArgs e)
    {
        try
        {
            Travelling_Gridview.UseAccessibleHeader = false;
            Travelling_Gridview.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void travel_mail_send(string client_code, string EMP_CODE, string STATE, string UNIT_CODE, string OPERATION_DATE, string START_TIME, string END_TIME)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
          
            d3.con.Open();
            MySqlCommand cmd = new MySqlCommand("select comp_code,unit_code FROM pay_unit_master where client_code='" + client_code + "' and unit_code='" + UNIT_CODE + "' ", d3.con);
            MySqlDataReader dr = cmd.ExecuteReader();
          
            while (dr.Read())
            {
                string query = "";
                d.con.Open();
                query = "select pay_unit_master.unit_name,pay_employee_master.EMP_MOBILE_NO,field_officer_name,date_format(OPERATION_DATE,'%d-%m-%Y')as OPERATION_DATE ,START_TIME,END_TIME,pay_op_management_details.id from pay_op_management_details INNER JOIN pay_unit_master ON pay_op_management_details.comp_code = pay_unit_master.comp_code AND pay_op_management_details.unit_code =pay_unit_master.unit_code INNER JOIN pay_employee_master ON pay_op_management_details.comp_code = pay_employee_master.comp_code AND pay_op_management_details.emp_code = pay_employee_master.emp_code where pay_op_management_details.client_code='" + client_code + "' and pay_op_management_details.unit_code='" + UNIT_CODE + "' and pay_op_management_details.OPERATION_DATE=str_to_date('" + OPERATION_DATE + "','%d-%m-%Y') ";
                System.Data.DataTable dt = new System.Data.DataTable();
                MySqlCommand cmd1 = new MySqlCommand(query);
                MySqlDataReader sda = null;
                cmd1.Connection = d.con;
               // d.con.Open();
                sda = cmd1.ExecuteReader();
                dt.Load(sda);
               // d.con.Close();
                string body = "";
                string body2 = "";
                if (dt.Rows.Count > 0)
                {
                    string ids = "",fofficer="";
                    int i = 0;
                    body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir/Madam, <p>We at IH&MS would like to update you that our field officer will be shortlty visiting your place as per Provided below with him During his visit & in case not resolved please feel free to revert on the following mail id. </FONT></FONT></FONT></B><p><Table border =1><tr><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>SR. No.</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>FIELD OFFICER NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>MOBILE NUMBER</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>LOCATION NAME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>VISIT ON</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>START TIME</FONT></FONT></FONT></th><th><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>END TIME</FONT></FONT></FONT></th></tr>";
                    foreach (DataRow row in dt.Rows)
                    {
                        body = body + "<tr><td><FONT ',COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + ++i + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["field_officer_name"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["EMP_MOBILE_NO"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["UNIT_NAME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["OPERATION_DATE"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["START_TIME"].ToString() + "</FONT></FONT></FONT></td><td><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>" + row["END_TIME"].ToString() + "</FONT></FONT></FONT></td><td><tr><td></td><td colspan=3 align=center><button class=accept ><a href={accept_url}><span>Available</span></a></button></td><td colspan=3 align=center><button class=reject ><a href={reject_url}><span>Not Available</span></a></button></tr> ";
                        ids = ids + row[6].ToString(); //+ ","
                        fofficer = row[2].ToString();

                    }
                    
                    body = body.Replace("{accept_url}" , d.check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=acceptfo&B=" + ids + "&C=" + fofficer + "&D=acceptfo");
                    body = body.Replace("{reject_url}", d.check_url(ConfigurationManager.AppSettings["URL"]) + "receiver.aspx?A=rejectfo&B=" + ids + "&C=" + fofficer + "&D=rejectfo");

                    body = body + "</td></tr></Table> <p>";
                    body2 = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2> <p> Also will Request you to confirm you are available or not available by clicking on button, In case no response found from your end within next 24hrs then we will confirm that you are available on the Schedule date. </FONT></FONT></FONT></B></th></tr><br>";

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    
                    try
                    {
                        d1.con.Open();
                        MySqlCommand cmdnew = new MySqlCommand("SET SESSION group_concat_max_len = 100000;SELECT cast(group_concat(distinct head_email_id) as char), head_name, pay_branch_mail_details.client_code, pay_branch_mail_details.comp_code, pay_branch_mail_details.state,pay_unit_master.zone,pay_zone_master.field5 FROM pay_branch_mail_details inner join pay_unit_master on pay_unit_master.comp_code = pay_branch_mail_details.comp_code and pay_unit_master.unit_code = pay_branch_mail_details.unit_code left outer join pay_zone_master on pay_zone_master.comp_code = pay_unit_master.comp_code and pay_zone_master.client_code = pay_unit_master.client_code and pay_zone_master.Region = pay_unit_master.zone and type = 'REGION' where pay_branch_mail_details.comp_code = '" + dr.GetValue(0).ToString() + "' and pay_branch_mail_details.unit_code = '" + dr.GetValue(1).ToString() + "'", d1.con);
                        MySqlDataReader drnew = cmdnew.ExecuteReader();
                        System.Data.DataTable DataTable1 = new System.Data.DataTable();
                        DataTable1.Load(drnew);
                         d1.con.Close();
                       
                            foreach (DataRow row in DataTable1.Rows)
                            {
                                mail_send(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), 3, "Schedule Visit For Field Officers", ddl_state.SelectedValue, dr.GetValue(1).ToString(), body2, row[6].ToString());
                            }
                        
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con.Close(); }
                }
            }
            d3.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d3.con.Close();
            d3.con.Close();
           // File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf");
        }
    
    }
    protected void mail_send(string head_email_id, string head_email_name, string client_name, string comp_code, int counter, string subject, string state_name, string unit_code, string body1, string h_email_id)
    {
            List<string> list1 = new List<string>();
            string from_emailid = "", password = "";
            try
            {

               // d.con.Open();
                MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where client_code = '" + client_name + "' ", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    from_emailid = dr.GetValue(0).ToString();
                    password = dr.GetValue(1).ToString();

                }
                from_emailid = "manager@ihmsindia.com";
                password = "manager@123";
                dr.Close();
                d.con.Close();
                if (!(from_emailid == "") || !(password == ""))
                {
                    string body = body1;
                    string name = d.getsinglestring("select group_concat( Field4 ,'<br />', Field5 ,'<br />Mobile - ', Field6 , '<br />Immediate Manager - Chaitali Nilawar(manager@ihmsindia.com)</FONT></FONT></FONT></B>') as 'ss' from pay_zone_master where type='client_Email' and  Field1 = 'Admin' and client_code='" + client_name + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                    body = body + "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2><br />Thanks & Regards,<br />" + name + "";

                    using (MailMessage mailMessage = new MailMessage())
                    {
                       SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mailMessage.From = new MailAddress(from_emailid);

                        if (head_email_id != "")
                        {

                           mailMessage.To.Add(head_email_id);

                           if (!h_email_id.Equals(""))
                            {
                                mailMessage.CC.Add(h_email_id);
                           }
                            //mailMessage.CC.Add("chaitalipatilckp1996@gmail.com");
                            mailMessage.Subject = subject;
                            mailMessage.Body = body;
                           //if (counter1 == 1)
                           //{
                            //    mailMessage.Attachments.Add(new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\") + "Joining_letter.pdf"));
                            //}

                            mailMessage.IsBodyHtml = true;
                            SmtpServer.UseDefaultCredentials = true;
                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                            SmtpServer.EnableSsl = true;

                            try
                            {
                                SmtpServer.Send(mailMessage);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Sent successfully!!');", true);
                            }
                            catch
                            {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error in Sending Email !!');", true);

                            }
                            Thread.Sleep(500);
                       }
                   }
                }
            }
           catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d.con.Close();
            }

        }
   
    protected void Field_ofc_history_gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[7].Text == "3")
            {
                e.Row.Cells[8].Text = "Complete";
            }
            if (e.Row.Cells[7].Text == "5")
            {
                e.Row.Cells[8].Text = "new";
            }
            //if (e.Row.Cells[7].Text == "0")
            //{
            //    e.Row.Cells[8].Text = "Pending";
            //}
            //if (e.Row.Cells[7].Text == "1")
            //{
            //    e.Row.Cells[8].Text = "Pending";
            //}
            //if (e.Row.Cells[7].Text == "2")
            //{
            //    e.Row.Cells[8].Text = "Pending";
            //}
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text.Equals("0"))
                e.Row.Visible = false;
            if (e.Row.Cells[7].Text.Equals("1"))
                e.Row.Visible = false;
            if (e.Row.Cells[7].Text.Equals("2"))
                e.Row.Visible = false;
        }
    }
    protected void Field_ofc_history_gv_PreRender(object sender, EventArgs e)
    {

        try
        {
            Field_ofc_history_gv.UseAccessibleHeader = false;
            Field_ofc_history_gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}