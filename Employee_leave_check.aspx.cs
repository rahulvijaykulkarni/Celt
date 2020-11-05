using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_leave_check : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d_head = new DAL();
    DAL d_client = new DAL();
    DAL d_unit = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {
            // employee_list();
            client_list();
            getGrideview();

        }
    }

    public void getGrideview()
    {

        MySqlDataAdapter dr = new MySqlDataAdapter("select leave_id ,pay_client_master.client_name as 'Client Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as 'Unit Name',emp_name as 'Employee Name',GRADE_DESC as 'Grade Name',date_format(from_date,'%d/%m/%Y') as 'From Date',date_format(to_date,'%d/%m/%Y') as 'To Date',pay_leave_transaction.no_of_days as 'pay_leave_transaction.No of days Leave',leave_reason as 'Leave Reason',date_format(leave_apply_date,'%d/%m/%Y') as 'Leave apply date',status_comment,leave_status as 'Leave Status' from pay_leave_transaction inner join pay_unit_master on pay_leave_transaction.unit_code=pay_unit_master.unit_code and  pay_leave_transaction.comp_code=pay_unit_master.comp_code inner join pay_client_master  on pay_leave_transaction.client_code=pay_client_master.client_code and pay_leave_transaction.comp_code=pay_client_master.comp_code inner join pay_grade_master on pay_leave_transaction.Grade_code=pay_grade_master.grade_code and pay_leave_transaction.comp_code=pay_grade_master.comp_code where pay_leave_transaction.comp_code='" + Session["COMP_CODE"].ToString() + "' order by leave_id desc", d.con);
       // MySqlDataAdapter dr = new MySqlDataAdapter("select leave_id ,pay_client_master.client_name  as 'Client Name',STATUS_COMMENT from pay_leave_transaction inner join pay_unit_master on pay_leave_transaction.unit_code=pay_unit_master.unit_code and  pay_leave_transaction.comp_code=pay_unit_master.comp_code inner join pay_client_master  on pay_leave_transaction.client_code=pay_client_master.client_code and pay_leave_transaction.comp_code=pay_client_master.comp_code inner join pay_grade_master on pay_leave_transaction.Grade_code=pay_grade_master.grade_code and pay_leave_transaction.comp_code=pay_grade_master.comp_code where pay_leave_transaction.comp_code='" + Session["COMP_CODE"].ToString() + "' order by leave_id desc", d.con);
       
        d.con.Open();
        DataTable dt = new DataTable();
        //DataSet DS1 = new DataSet();
        dr.Fill(dt);

        gv_salary_structure.DataSource = dt;
        gv_salary_structure.DataBind();
        d.con.Close();


    }
    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {

       
        ddl_state.Items.Clear();
        ddl_unit.Items.Clear();
        MySqlDataAdapter cmd_item = null;
        System.Data.DataTable dt_item = new System.Data.DataTable();
        if (ddl_client.SelectedValue != "ALL")
        {
            cmd_item = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `client_code` = '" + ddl_client.SelectedValue + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
        }
        else
        {
            cmd_item = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "'  AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
        }
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
            ddl_state.Items.Insert(1, "ALL");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void GridView1_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_salary_structure.UseAccessibleHeader = false;
            gv_salary_structure.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }

    protected void gv_salary_structure_RowDataBound(object sender, GridViewRowEventArgs e)
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
          //  e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
           // e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
           // e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_salary_structure, "Select$" + e.Row.RowIndex);

        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        // string newdate = txt_dhead10.Text.Replace(@"/", @"");
        Response.Redirect("Home.aspx");
    }

    protected void btn_show_click(object sender,EventArgs e) {
        gv_salary_structure.DataSource = null;
        gv_salary_structure.DataBind();
       // MySqlDataAdapter dr = new MySqlDataAdapter("select leave_id ,pay_client_master.client_name as 'Client Name',pay_unit_master.unit_name as 'Unit Name',emp_name as 'Employee Name',GRADE_DESC as 'Grade Name',date_format(from_date,'%d/%m/%Y') as 'From Date',date_format(to_date,'%d/%m/%Y') as 'To Date',no_of_days as 'No of days Leave',leave_reason as 'Leave Reason',date_format(leave_apply_date,'%d/%m/%Y') as 'Leave apply date' from pay_leave_transaction inner join pay_unit_master on pay_leave_transaction.unit_code=pay_unit_master.unit_code and  pay_leave_transaction.comp_code=pay_unit_master.comp_code inner join pay_client_master  on pay_leave_transaction.client_code=pay_client_master.client_code and pay_leave_transaction.comp_code=pay_client_master.comp_code inner join pay_grade_master on pay_leave_transaction.Grade_code=pay_grade_master.grade_code and pay_leave_transaction.comp_code=pay_grade_master.comp_code where pay_leave_transaction.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_leave_transaction.client_code='" + ddl_client.SelectedValue + "' and pay_leave_transaction.unit_code='" + ddl_unit.SelectedValue + "' and from_date >=  str_to_date('" + txt_satrtdate.Text.ToString() + "','%d/%m/%Y') and to_date <=  str_to_date('" + txt_enddate.Text.ToString() + "','%d/%m/%Y') order by leave_id desc", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter("select leave_id ,pay_client_master.client_name as 'Client Name',CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as 'Unit Name',emp_name as 'Employee Name',grade_desc as 'Grade Name',date_format(from_date,'%d/%m/%Y') as 'From Date',date_format(to_date,'%d/%m/%Y') as 'To Date',no_of_days as 'No of days Leave',leave_reason as 'Leave Reason',date_format(leave_apply_date,'%d/%m/%Y') as 'Leave apply date',status_comment,leave_status as 'Leave Status' from pay_leave_transaction inner join pay_unit_master on pay_leave_transaction.unit_code=pay_unit_master.unit_code and  pay_leave_transaction.comp_code=pay_unit_master.comp_code inner join pay_client_master  on pay_leave_transaction.client_code=pay_client_master.client_code and pay_leave_transaction.comp_code=pay_client_master.comp_code inner join pay_grade_master on pay_leave_transaction.Grade_code=pay_grade_master.grade_code and pay_leave_transaction.comp_code=pay_grade_master.comp_code where pay_leave_transaction.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_leave_transaction.client_code='" + ddl_client.SelectedValue + "' and pay_leave_transaction.unit_code='" + ddl_unit.SelectedValue + "' and from_date >=  str_to_date('" + txt_satrtdate.Text.ToString() + "','%d/%m/%Y') and to_date <=  str_to_date('" + txt_enddate.Text.ToString() + "','%d/%m/%Y') order by leave_id desc", d.con);
       
        d.con.Open();
        DataTable dt = new DataTable();
        //DataSet DS1 = new DataSet();
        dr.Fill(dt);

        gv_salary_structure.DataSource = dt;
        gv_salary_structure.DataBind();
        d.con.Close();
    }

    public void client_list()
    {
        d.con1.Open();
        try
        {
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("select client_code,client_name from pay_client_master where  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "' ", d.con1);
            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client.DataSource = dt_item;
                ddl_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_client.DataBind();
            }
            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_client.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
        }

    }

    protected void lnkbtn_approval_Click(object sender, EventArgs e)
    {
       GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;

      int item = (int)gv_salary_structure.DataKeys[gvrow.RowIndex].Value;
     // string emp_code = gv_salary_structure.Rows[gvrow.RowIndex].FindControl("txt_comment").Text.to;
       //string abc= (string)gv_salary_structure.DataKeys[gvrow.RowIndex].Value;

      string status_commentdetails = (gvrow.FindControl("txt_comment") as System.Web.UI.WebControls.TextBox).Text;

      if (status_commentdetails == null || status_commentdetails == "")
      {
          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter comment !!!')", true);

          return;
       }

      //if (status_commentdetails != null || status_commentdetails != "")
      //{
      //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Are you Conform !!!')", true);

      //    return;
      //}

      int res = d.operation("update pay_leave_transaction set leave_status='Approved', status_comment='" + status_commentdetails + "',android_flag='2' where  leave_id='" + item + "'");

      if (res > 0)
      {
          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Employee Leave successfully !!!')", true);
      }
      else {
          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data not field proper format!!!')", true);
      }
       
    }

    protected void lnkbtn_reject_Click(object  sender , EventArgs e) {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;

        int item = (int)gv_salary_structure.DataKeys[gvrow.RowIndex].Value;
        // string emp_code = gv_salary_structure.Rows[gvrow.RowIndex].FindControl("txt_comment").Text.to;
        //string abc= (string)gv_salary_structure.DataKeys[gvrow.RowIndex].Value;

        string status_commentdetails = (gvrow.FindControl("txt_comment") as System.Web.UI.WebControls.TextBox).Text;
        if (status_commentdetails == null || status_commentdetails == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter comment !!!')", true);

            return;
        }
        int res = d.operation("update pay_leave_transaction set leave_status='Rejected', status_comment='" + status_commentdetails + "',android_flag='3' where  leave_id='" + item + "'");

        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Reject Employee Leave successfully !!!')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data not field proper format!!!')", true);
        }

    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        string client_code = "";

        if (ddl_state.SelectedValue == "ALL")
        {
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "'  and branch_status = 0   ORDER BY UNIT_CODE", d.con);//and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" +ddl_billing_state.SelectedValue + "') 
            //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME' , `unit_code` from pay_unit_master where comp_code='" + Session["comp_code"] + "'  and branch_status = 0 ORDER BY UNIT_NAME", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unit.DataSource = dt_item;
                    ddl_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                //ddl_branch_increment.Items.Insert(0, "Select");
                ddl_unit.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }


        }
        else
        {
            MySqlDataAdapter cmd_item = null;
            ddl_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            if (ddl_client.SelectedValue != "ALL")
            {
                client_code = "" + ddl_client.SelectedValue + "";
                cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ) AND state_name='" + ddl_state.SelectedValue + "' and  branch_status = 0  and client_code='" + client_code + "' ORDER BY UNIT_NAME", d.con);
            }
            else
            {
                cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ) AND state_name='" + ddl_state.SelectedValue + "' and  branch_status = 0   ORDER BY UNIT_NAME", d.con);
            }
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unit.DataSource = dt_item;
                    ddl_unit.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unit.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unit.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                //ddl_branch_increment.Items.Insert(0, "Select");
                ddl_unit.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }

    }
    protected void gv_salary_deuction_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_salary_structure.UseAccessibleHeader = false;
            gv_salary_structure.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }




    
}