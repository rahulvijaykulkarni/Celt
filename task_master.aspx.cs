using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
//using System.IO;
using System.Drawing;

public partial class task_master : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected int result = 0;
    public string Message = "0";
    public int count = 0;
    protected double billing_amount = 0, recived_amt = 0;
    private string selectedvalue;

    protected void Page_Load(object sender, EventArgs e)
    {
        get_roll();
        if (!IsPostBack)
        {

         
            client_code();//vikas
            client_location();// location tab client 
            //tem_coment client_page();//client gridwiev in page load
           // location_gridwive();
            client_list();//employee
            client_code_billing();//billing
            ddl_client_list_sallary();//sallary
            client_attendance();//attendance_remaining
            Employee_Compliances();//employee compliances
        }
     }
    protected void get_roll()
    {


        //client
        if (d.getaccess(Session["ROLE"].ToString(), "Client Master", Session["COMP_CODE"].ToString()) == "D")
        {
            panel_client.Visible = true;
        }
        else
        {
            panel_client.Visible = false;
        }
        //branch location
        if (d.getaccess(Session["ROLE"].ToString(), "Branch Master", Session["COMP_CODE"].ToString()) == "D")
        {
            panel_banch_location.Visible = true;
        }
        else
        {
            panel_banch_location.Visible = false;
        }
        //employee
        //if (d.getaccess(Session["ROLE"].ToString(), "Employee Master") == "D")
        //{
        //    panel_employee.Visible = true;
        //}
        //else
        //{
        //    panel_employee.Visible = false;
      //  }
        //billing
        if (d.getaccess(Session["ROLE"].ToString(), "Attendance Sheet",Session["COMP_CODE"].ToString()) == "D" || d.getaccess(Session["ROLE"].ToString(), "Policy Master",Session["COMP_CODE"].ToString()) == "D")
        {
            panel_billing.Visible = true;
        }
        else
        {
            panel_billing.Visible = false;
        }
       
        //salary
        if (d.getaccess(Session["ROLE"].ToString(), "Payment History", Session["COMP_CODE"].ToString()) == "D")
        {
            panel_salary.Visible = true;
        }
        else
        {
            panel_salary.Visible = false;
        }
        //employee compiances
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Compliance", Session["COMP_CODE"].ToString()) == "D")
        {
            panel_Employee_Compliances.Visible = true;
        }
        else
        {
            panel_Employee_Compliances.Visible = true;
        }

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }
    }
    //client
    protected void client_code()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
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
      try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand("Select ADDRESS1,total_employee from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY id", d.con);
            d.con.Open();
             MySqlDataReader dr = cmd.ExecuteReader();
             if (dr.Read())
             {
             
                 txt_client_address.Text = dr.GetValue(0).ToString();
                  txt_emp_count.Text = dr.GetValue(1).ToString();
                 remaning(); 
                 designation();
                 
             }
             d.con.Dispose();
           d.con.Close();
        }
        catch (Exception ex)
        { 
            throw ex; }
        finally
            {
            d.con.Close();
            }

        // client_pending grd
      try
      {

        //  MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'HEAD' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is null and Region is null and `pay_zone_master`.`Type` = 'HEAD' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as HEAD_Info,ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'ZONE' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is null and `pay_zone_master`.`Type` = 'ZONE' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'ZONE_Info',ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'REGION' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is not null and `pay_zone_master`.`Type` = 'REGION' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'REGION_Info',ifnull((select 'Done' from pay_images where pay_images.client_code = pay_client_master.client_code group by client_code),'Pending') as 'Document' FROM pay_client_master  where comp_code='" + Session["comp_code"] + "'and client_code = '" + ddl_client.SelectedValue + "'", d.con);
          MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'HEAD' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is null and Region is null and `pay_zone_master`.`Type` = 'HEAD' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as HEAD_Info,ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'ZONE' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is null and `pay_zone_master`.`Type` = 'ZONE' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'ZONE_Info',ifnull((select CASE WHEN `pay_zone_master`.`Type` = 'REGION' THEN 'Done' ELSE 'Pending' END from pay_zone_master where zone is not null and Region is not null and `pay_zone_master`.`Type` = 'REGION' and pay_zone_master.client_code = pay_client_master.client_code group by client_code),'Pending') as 'REGION_Info',IFNULL((SELECT CASE WHEN `pay_zone_master`.`Type` = 'GST' THEN 'Done' ELSE 'Pending' END FROM `pay_zone_master` WHERE `zone` IS NULL AND `Region` IS NOT NULL AND `pay_zone_master`.`Type` = 'GST' AND `pay_zone_master`.`client_code` = `pay_client_master`.`client_code` GROUP BY `client_code`), 'Pending') AS 'GST_Info' ,ifnull((select 'Done' from pay_images where pay_images.client_code = pay_client_master.client_code group by client_code),'Pending') as 'Document' FROM pay_client_master  where comp_code='" + Session["comp_code"] + "'and client_code = '" + ddl_client.SelectedValue + "'", d.con);
              d.con.Open();
              DataSet ds = new DataSet();
              adp_grid2.Fill(ds);
             // grd_client_pending.DataSource = ds;
             // grd_client_pending.DataBind();
              
          if (ds.Tables[0].Rows.Count > 0)
              {
                  grd_client_pending.DataSource = ds;
                  grd_client_pending.DataBind();
              }

          for (int i = 0; i < grd_client_pending.Rows.Count; i++)
              {
                  if (grd_client_pending.Rows[i].Cells[0].Text == "Done")
                      grd_client_pending.Rows[i].Cells[0].ControlStyle.BackColor = Color.GreenYellow;

                  else
                      grd_client_pending.Rows[i].Cells[0].ControlStyle.BackColor = Color.Red;
                  
              if (grd_client_pending.Rows[i].Cells[1].Text == "Done")
                      grd_client_pending.Rows[i].Cells[1].ControlStyle.BackColor = Color.GreenYellow;

                  else
                      grd_client_pending.Rows[i].Cells[1].ControlStyle.BackColor = Color.Red;
                 
              if (grd_client_pending.Rows[i].Cells[2].Text == "Done")
                      grd_client_pending.Rows[i].Cells[2].ControlStyle.BackColor = Color.GreenYellow;

                  else
                      grd_client_pending.Rows[i].Cells[2].ControlStyle.BackColor = Color.Red;

              if (grd_client_pending.Rows[i].Cells[3].Text == "Done")
                  grd_client_pending.Rows[i].Cells[3].ControlStyle.BackColor = Color.GreenYellow;

              else
                  grd_client_pending.Rows[i].Cells[3].ControlStyle.BackColor = Color.Red;
              
              if (grd_client_pending.Rows[i].Cells[4].Text == "Done")
                  grd_client_pending.Rows[i].Cells[4].ControlStyle.BackColor = Color.GreenYellow;

              else
                  grd_client_pending.Rows[i].Cells[4].ControlStyle.BackColor = Color.Red;

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
    protected void grd_client_pending_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void remaning()
    {
        try
        {
            System.Data.DataTable dt_item = new System.Data.DataTable();
           // MySqlCommand cmd = new MySqlCommand("SELECT sum(COUNT) FROM pay_designation_count WHERE comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'and UNIT_CODE is null", d1.con);
            MySqlCommand cmd = new MySqlCommand("SELECT sum(COUNT),(total_employee-sum(COUNT)) as 'differance' FROM `pay_client_master` inner join pay_designation_count on pay_client_master.comp_code=pay_designation_count.comp_code  and pay_client_master.client_code=pay_designation_count.client_code WHERE pay_designation_count.comp_code='" + Session["comp_code"] + "' and pay_designation_count.client_code = '" + ddl_client.SelectedValue + "'and UNIT_CODE is null", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_diploy_emp.Text = dr.GetValue(0).ToString();
               txt_differance_count.Text = dr.GetValue(1).ToString();

            }
            d1.con.Dispose();
            d1.con.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d1.con.Close();
        }
    }

    protected void designation()
    {
        try
        {
          //MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT Id,client_name,unit_name,new_employee_name,grade FROM pay_new_employee_requirement ORDER BY ID", d1.con1);
            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT Distinct(STATE),DESIGNATION, COUNT, HOURS FROM pay_designation_count WHERE comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'and UNIT_CODE is null ", d1.con1);
            d1.con1.Open();
            DataSet ds = new DataSet();
            adp_grid.Fill(ds);
            grd_client.DataSource = ds;
            grd_client.DataBind();
            d.con1.Close();
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

    protected void grd_client_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void grd_client_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_client.UseAccessibleHeader = false;
            grd_client.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    
    //Branch/location tab


    protected void client_location()
    {
        ddl_client_b.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_b.DataSource = dt_item;
                ddl_client_b.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_b.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_b.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client_b.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }
    protected void ddl_client_b_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_state_b.Items.Clear();
        ddl_branch_b.Items.Clear();
        txt_admin.Text = "";
        txt_operation.Text = "";
        txt_fainance.Text = "";
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddl_client_b.SelectedValue + "' and state in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client_b.SelectedValue + "')  and unit_code is null ORDER BY STATE", d1.con1);
        //d1.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_b.DataSource = dt_item;
                ddl_state_b.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_b.DataBind();
               // location_gridwive();
            }
            dt_item.Dispose();
            ddl_state_b.Items.Insert(0, new ListItem("Select", "0"));
            d1.con1.Close();
           
        }
            
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }
       

    }

    protected void location_gridwive()
    {
        
        try
        {

            
            //MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT pay_unit_master.UNIT_CODE,pay_unit_master.File_No,pay_unit_master.UNIT_CITY,pay_designation_count.DESIGNATION, pay_designation_count.COUNT,pay_designation_count.HOURS,pay_unit_master.Emp_count,pay_unit_master.OperationHead_Name,pay_unit_master.OperationHead_Mobileno, pay_unit_master.OperationHead_EmailId,pay_unit_master.FinanceHead_Name,pay_unit_master.FinanceHead_Mobileno,pay_unit_master.FinanceHead_EmailId,pay_unit_master.LocationHead_Name,pay_unit_master.LocationHead_mobileno,pay_unit_master.LocationHead_Emailid,pay_unit_master.OtherHead_Name,pay_unit_master.OtherHead_Monileno,pay_unit_master.OtherHead_Emailid,pay_unit_master.adminhead_name,pay_unit_master.adminhead_mobile,pay_unit_master.adminhead_email FROM `pay_unit_master` INNER JOIN `pay_designation_count` ON `pay_unit_master`.`COMP_CODE` = `pay_designation_count`.`COMP_CODE` AND `pay_unit_master`.`UNIT_CODE` = `pay_designation_count`.`UNIT_CODE` WHERE pay_unit_master.CLIENT_CODE = '" + ddl_client_b.SelectedValue + "' and pay_unit_master.STATE_NAME='" + ddl_state_b.SelectedValue + "'   ", d.con);
           // MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT pay_unit_master.UNIT_NAME,File_No,pay_unit_master.UNIT_CITY,pay_designation_count.DESIGNATION, pay_designation_count.COUNT,pay_designation_count.HOURS,pay_unit_master.Emp_count,pay_unit_master.OperationHead_Name,pay_unit_master.OperationHead_Mobileno, pay_unit_master.OperationHead_EmailId,pay_unit_master.FinanceHead_Name,pay_unit_master.FinanceHead_Mobileno,pay_unit_master.FinanceHead_EmailId,pay_unit_master.LocationHead_Name,pay_unit_master.LocationHead_mobileno,pay_unit_master.LocationHead_Emailid,pay_unit_master.OtherHead_Name,pay_unit_master.OtherHead_Monileno,pay_unit_master.OtherHead_Emailid,pay_unit_master.adminhead_name,pay_unit_master.adminhead_mobile,pay_unit_master.adminhead_email FROM `pay_unit_master` INNER JOIN `pay_designation_count` ON `pay_unit_master`.`COMP_CODE` = `pay_designation_count`.`COMP_CODE` AND `pay_unit_master`.`UNIT_CODE` = `pay_designation_count`.`UNIT_CODE` WHERE pay_unit_master.comp_code='" + Session["comp_code"] + "' and pay_unit_master.CLIENT_CODE = '" + ddl_client_b.SelectedValue + "' and pay_unit_master.STATE_NAME='" + ddl_state_b.SelectedValue + "'   ", d.con);
            MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT  `pay_unit_master`.`UNIT_NAME`, IFNULL((`Emp_count` - (SELECT COUNT(`EMP_CODE`) FROM `pay_employee_master` WHERE `unit_code` = `pay_unit_master`.`unit_code` AND `comp_code`='" + Session["comp_code"].ToString() + "'  AND `Employee_type` != 'Reliever' AND `LEFT_DATE` IS NULL)), 0) AS 'File_No', `pay_unit_master`.`UNIT_CITY`,`pay_designation_count`.`DESIGNATION`,`pay_designation_count`.`COUNT`,`pay_designation_count`.`HOURS`,`pay_unit_master`.`Emp_count`,`pay_unit_master`.`OperationHead_Name`,`pay_unit_master`.`OperationHead_Mobileno`,`pay_unit_master`.`OperationHead_EmailId`,`pay_unit_master`.`FinanceHead_Name`,`pay_unit_master`.`FinanceHead_Mobileno`,`pay_unit_master`.`FinanceHead_EmailId`,`pay_unit_master`.`LocationHead_Name`,`pay_unit_master`.`LocationHead_mobileno`, `pay_unit_master`.`LocationHead_Emailid`,`pay_unit_master`.`OtherHead_Name`,`pay_unit_master`.`OtherHead_Monileno`,`pay_unit_master`.`OtherHead_Emailid`,`pay_unit_master`.`adminhead_name`,`pay_unit_master`.`adminhead_mobile`,`pay_unit_master`.`adminhead_email`,(SELECT COUNT(`EMP_CODE`) FROM `pay_employee_master` WHERE`unit_code` = `pay_unit_master`.`unit_code` AND `comp_code` = 'C01' AND `Employee_type` != 'Reliever' AND `LEFT_DATE` IS NULL) AS 'employee_assign' FROM `pay_unit_master`INNER JOIN `pay_designation_count` ON `pay_unit_master`.`COMP_CODE` = `pay_designation_count`.`COMP_CODE` AND `pay_unit_master`.`UNIT_CODE` = `pay_designation_count`.`UNIT_CODE`   WHERE pay_unit_master.comp_code='" + Session["comp_code"] + "' and pay_unit_master.CLIENT_CODE = '" + ddl_client_b.SelectedValue + "' and pay_unit_master.STATE_NAME='" + ddl_state_b.SelectedValue + "'  and pay_unit_master.branch_status=0  ", d.con);
            
            d.con.Open();
            DataSet ds = new DataSet();
            adp_grid1.Fill(ds);
            location_branch.DataSource = ds;
            location_branch.DataBind();
            d.con.Close();
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
    protected void location_branch_DataBound(object sender, GridViewRowEventArgs e)
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
   




    protected void location_branch_PreRender(object sender, EventArgs e)
    {
        try
        {
            location_branch.UseAccessibleHeader = false;
            location_branch.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void branch_pending()
    {
        try
        {

            // MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT pay_unit_master.UNIT_CODE,pay_unit_master.UNIT_CITY,pay_designation_count.DESIGNATION, pay_designation_count.COUNT,pay_designation_count.HOURS,pay_unit_master.Emp_count FROM  pay_designation_count  INNER JOIN  pay_unit_master On pay_unit_master.COMP_CODE= pay_designation_count.COMP_CODE and pay_unit_master.CLIENT_CODE  = pay_designation_count.CLIENT_CODE and pay_unit_master.UNIT_CODE  = pay_designation_count.UNIT_CODE WHERE pay_unit_master.client_code = '" + Session["comp_code"] + "'", d.con);
            MySqlDataAdapter adp_grid1 = new MySqlDataAdapter("SELECT pay_unit_master.UNIT_NAME,pay_unit_master.UNIT_CITY,pay_unit_master.Client_branch_code,pay_unit_master.OPus_NO,pay_unit_master.txt_zone, pay_unit_master.ZONE,pay_unit_master.UNIT_EMAIL_ID FROM  pay_unit_master WHERE comp_code='" + Session["comp_code"] + "' and pay_unit_master.CLIENT_CODE = '" + ddl_client_b.SelectedValue + "' and pay_unit_master.STATE_NAME='" + ddl_state_b.SelectedValue + "' and branch_status=0 ", d.con);
            d.con.Open();
            DataSet ds = new DataSet();
            adp_grid1.Fill(ds);
            location_branch_pending.DataSource = ds;
            location_branch_pending.DataBind();
            d.con.Close();
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
    protected void location_branch_pending_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if(i !=0 && i != 1)
                {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "Pending";
                    e.Row.Cells[i].BackColor = Color.Red;
                }
                else
                {
                    
                    e.Row.Cells[i].Text = "done";
                    e.Row.Cells[i].BackColor = Color.GreenYellow;
                }
                }
               
            }
        }

    }
    protected void ddl_state_b_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            d.con.Open();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            ddl_branch_b.Items.Clear();
            txt_admin.Text = "";
            txt_operation.Text = "";
            txt_fainance.Text = "";
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME', `unit_code` FROM `pay_unit_master`  WHERE  comp_code='" + Session["comp_code"] + "' AND `client_code` = '" + ddl_client_b.SelectedValue + "' and `STATE_NAME`='" + ddl_state_b.SelectedValue + "' AND `UNIT_CODE` IN (SELECT `UNIT_CODE` FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `EMP_CODE` = '" + Session["LOGIN_ID"] + "' AND `client_code` = '" + ddl_client_b.SelectedValue + "' AND `state_name` = '" + ddl_state_b.SelectedValue + "') and branch_status=0 ORDER BY `UNIT_CODE`", d.con);
            
            DataSet ds1 = new DataSet();
            cmd_item.Fill(ds1);
            ddl_branch_b.DataSource = ds1.Tables[0];
            ddl_branch_b.DataValueField = "unit_code";
            ddl_branch_b.DataTextField = "UNIT_NAME";
            ddl_branch_b.DataBind();
            ddl_branch_b.Items.Insert(0,"Select");
            dt.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
            finally{
                
                d.con.Close();
        }
        location_gridwive();
        branch_pending();
    }

    protected void ddl_branch_b_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            txt_admin.Text = "";
            txt_operation.Text = "";
            txt_fainance.Text = "";
            d.con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd_item =new  MySqlDataAdapter("SELECT `pay_employee_master`.`EMP_NAME`, pay_client_state_role_grade.`department_type` FROM `pay_client_state_role_grade` Left JOIN `pay_employee_master` ON `pay_employee_master`.`emp_code` = `pay_client_state_role_grade`.`emp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` ='" + Session["comp_code"] + "' and `pay_client_state_role_grade`.`UNIT_CODE` = '"+ddl_branch_b.SelectedValue+"' ", d.con);
           
            
            cmd_item.Fill(dt);
            int j = 6;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (row[1].ToString() == "Admin")
                    { 
                    txt_admin.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Operation")
                    { 
                    txt_operation.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Finance")
                    {
                        txt_fainance.Text = row[0].ToString();
                    }
                }
               
            }
            d.con.Close();
            cmd_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }
      
    }
    
    //Employee

    protected void ddl_branch_emp_OnSelectedIndexChanged()
    {

        try
        {
            txt_emp_admin.Text = "";
            txt_op_name.Text = "";
            txt_emp_finance.Text = "";
            d.con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT `pay_employee_master`.`EMP_NAME`, pay_client_state_role_grade.`department_type` FROM `pay_client_state_role_grade` Left JOIN `pay_employee_master` ON `pay_employee_master`.`emp_code` = `pay_client_state_role_grade`.`emp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` ='" + Session["comp_code"] + "' and `pay_client_state_role_grade`.`UNIT_CODE` = '" + DropDownList3.SelectedValue + "' ", d.con);


            cmd_item.Fill(dt);
            int j = 6;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (row[1].ToString() == "Admin")
                    {
                        txt_emp_admin.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Operation")
                    {
                        txt_op_name.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Finance")
                    {
                        txt_emp_finance.Text = row[0].ToString();
                    }
                }

            }
            d.con.Close();
            cmd_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }

    }
    
    
   //Employee Compliances


    protected void ddl_branch_Compliances_OnSelectedIndexChanged()
    {

        try
        {
            txt_emp_co.Text = "";
            txt_emp_co_name.Text = "";
            txt_emp_co_finance.Text = "";
            d.con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT `pay_employee_master`.`EMP_NAME`, pay_client_state_role_grade.`department_type` FROM `pay_client_state_role_grade` Left JOIN `pay_employee_master` ON `pay_employee_master`.`emp_code` = `pay_client_state_role_grade`.`emp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` ='" + Session["comp_code"] + "' and `pay_client_state_role_grade`.`UNIT_CODE` = '" + ddl_brachCompliances.SelectedValue + "' ", d.con);


            cmd_item.Fill(dt);
            int j = 6;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (row[1].ToString() == "Admin")
                    {
                        txt_emp_co.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Operation")
                    {
                        txt_emp_co_name.Text = row[0].ToString();
                    }
                    else if (row[1].ToString() == "Finance")
                    {
                        txt_emp_co_finance.Text = row[0].ToString();
                    }
                }

            }
            d.con.Close();
            cmd_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }

    }
   
    
    
    //employee tab

    public void client_list()
    {
       
       // d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con1);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                DropDownList1.DataSource = dt_item;
                DropDownList1.DataTextField = dt_item.Columns[0].ToString();
                DropDownList1.DataValueField = dt_item.Columns[1].ToString();
                DropDownList1.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
            DropDownList1.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2.Items.Clear();
        DropDownList3.Items.Clear();
        txt_emp_admin.Text = "";
        txt_op_name.Text = "";
        txt_emp_finance.Text = "";

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + DropDownList1.SelectedValue + "'  AND `LOCATION` in(select STATE_NAME from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + DropDownList1.SelectedValue + "') ORDER BY EMP_CURRENT_STATE", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                DropDownList2.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        DropDownList2.Items.Insert(0, new ListItem("Select"));
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList3.Items.Clear();
        txt_emp_admin.Text = "";
        txt_op_name.Text = "";
        txt_emp_finance.Text = "";
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + DropDownList1.SelectedValue + "' and state_name = '" + DropDownList2.SelectedValue + "'  AND `UNIT_CODE` IN (SELECT `UNIT_CODE` FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `EMP_CODE` = '" + Session["LOGIN_ID"] + "' AND `client_code` = '" + DropDownList1.SelectedValue + "' AND `state_name` = '" + DropDownList2.SelectedValue + "') and branch_status=0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                DropDownList3.DataSource = dt_item;
                DropDownList3.DataTextField = dt_item.Columns[0].ToString();
                DropDownList3.DataValueField = dt_item.Columns[1].ToString();
                DropDownList3.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            DropDownList3.Items.Insert(0, new ListItem("Select"));
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            txt_emp_admin.Text = "";
            txt_op_name.Text = "";
            txt_emp_finance.Text = "";
            ddl_branch_emp_OnSelectedIndexChanged();
            string emp_namee = d.getsinglestring("SELECT `pay_employee_master`.`emp_name` from pay_employee_master where comp_code='" + Session["comp_code"] + "'and `pay_employee_master`.`UNIT_CODE` = '" + DropDownList3.SelectedValue + "'");
              if (emp_namee == "")
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found !!!');", true);
              }
              else
              {

                  //   MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode, pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE'  FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  pay_employee_master.UNIT_CODE='" + DropDownList3.SelectedValue + "' AND pay_employee_master.client_code='" + DropDownList1.SelectedValue + "'  and LOCATION='" + DropDownList2.SelectedValue + "' AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) ", d.con1);


                  // MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT pay_employee_master.emp_name,pay_employee_master.EMP_MOBILE_NO,pay_employee_master.original_bank_account_no,pay_employee_master.PF_NOMINEE_RELATION,pay_employee_master.PF_NOMINEE_NAME,pay_images_master.original_photo,pay_images_master.original_adhar_card,pay_images_master.original_policy_document,pay_images_master.original_address_proof,pay_images_master.bank_passbook,pay_images_master.emp_signature, pay_images_master.noc_form, cast(group_concat(pay_document_details.document_type) as char) FROM pay_employee_master INNER JOIN pay_images_master ON pay_employee_master.COMP_CODE = pay_images_master.COMP_CODE AND pay_employee_master.EMP_CODE = pay_images_master.EMP_CODE inner join pay_document_details on pay_employee_master.COMP_CODE= pay_document_details.COMP_CODE and pay_employee_master.EMP_CODE=pay_document_details.EMP_CODE WHERE pay_employee_master.UNIT_CODE='" + DropDownList3.SelectedValue + "' AND pay_employee_master.client_code='" + DropDownList1.SelectedValue + "'  and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) group by pay_employee_master.EMP_CODE", d.con);
                //  MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT`pay_employee_master`.`emp_name`,`pay_employee_master`.`EMP_MOBILE_NO`,`pay_employee_master`.`original_bank_account_no`, `pay_employee_master`.`PF_NOMINEE_RELATION`,`pay_employee_master`.`PF_NOMINEE_NAME`,`pay_images_master`.`original_photo`,`pay_images_master`.`original_adhar_card`, `pay_images_master`.`original_policy_document`,`pay_images_master`.`original_address_proof`,`pay_images_master`.`bank_passbook`,`pay_images_master`.`emp_signature`, `pay_images_master`.`noc_form`, CAST(GROUP_CONCAT(`pay_document_details`.`document_type`) AS char) AS document_type  FROM `pay_employee_master` left outer JOIN `pay_images_master` ON `pay_employee_master`.`COMP_CODE` = `pay_images_master`.`COMP_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_images_master`.`EMP_CODE`left outer join `pay_document_details` ON `pay_employee_master`.`COMP_CODE` = `pay_document_details`.`COMP_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_document_details`.`EMP_CODE` WHERE `pay_employee_master`.`UNIT_CODE` = '" + DropDownList3.SelectedValue + "' AND `pay_employee_master`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND (`pay_employee_master`.`LEFT_REASON` = '' || `pay_employee_master`.`LEFT_REASON` IS NULL) GROUP BY `pay_employee_master`.`EMP_CODE`", d.con);
                 
                  //vikas 08-01-19
                  
                  MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME',`pay_employee_master`.`EMP_MOBILE_NO`,`pay_employee_master`.`original_bank_account_no`,`pay_employee_master`.`PF_NOMINEE_RELATION`,`pay_employee_master`.`PF_NOMINEE_NAME`,`pay_images_master`.`original_photo`,`pay_images_master`.`original_adhar_card`,`pay_images_master`.`original_policy_document`, `pay_images_master`.`original_address_proof`,`pay_images_master`.`bank_passbook`,`pay_images_master`.`emp_signature`,`pay_images_master`.`noc_form`,(Select if(document_type is not null,'Done','Pending') from pay_document_details where pay_employee_master.emp_code = pay_document_details.emp_code and document_type = 'ID_Card') as 'ID CARD',(Select if(document_type is not null,'Done','Pending') from pay_document_details where pay_employee_master.emp_code = pay_document_details.emp_code and document_type = 'Uniform') as 'UNIFORM',(Select if(document_type is not null,'Done','Pending') from pay_document_details where pay_employee_master.emp_code = pay_document_details.emp_code and document_type = 'Sweater') as 'SWEATER' FROM `pay_employee_master` LEFT OUTER JOIN `pay_images_master` ON `pay_employee_master`.`COMP_CODE` = `pay_images_master`.`COMP_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_images_master`.`EMP_CODE`  LEFT OUTER JOIN `pay_document_details` ON `pay_employee_master`.`COMP_CODE` = `pay_document_details`.`COMP_CODE` AND `pay_employee_master`.`EMP_CODE` = `pay_document_details`.`EMP_CODE` WHERE `pay_employee_master`.`UNIT_CODE` = '" + DropDownList3.SelectedValue + "' AND `pay_employee_master`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND (`pay_employee_master`.`LEFT_REASON` = '' || `pay_employee_master`.`LEFT_REASON` IS NULL) GROUP BY `pay_employee_master`.`EMP_CODE`", d.con);
                  d.con.Open();
                  DataSet ds = new DataSet();
                  adp_grid2.Fill(ds);
                  employee_grd.DataSource = ds;
                  employee_grd.DataBind();
                  d.con.Close();
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
    protected void employee_grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0 && i != 1)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "Pending";
                        e.Row.Cells[i].BackColor = Color.Red;


                    }
                    else
                    {
                        e.Row.Cells[i].Text = "Done";
                        e.Row.Cells[i].BackColor = Color.GreenYellow;
                    }
                }

            }
        }

    }
   
    
    //billing tab
    protected void client_code_billing()
    {
        client_Billing.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                client_Billing.DataSource = dt_item;
                client_Billing.DataTextField = dt_item.Columns[0].ToString();
                client_Billing.DataValueField = dt_item.Columns[1].ToString();
                client_Billing.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            client_Billing.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }
    protected void client_Billing_SelectedIndexChanged(object sender, EventArgs e)
    {
        state_Billing.Items.Clear();
       


      //  ddl_clientwisestate.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + client_Billing.SelectedValue + "' and unit_code is null ORDER BY STATE", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                state_Billing.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            state_Billing.Items.Insert(0, new ListItem("Select"));

            ddl_unitcode.Items.Clear();
          //  ddl_designation.Items.Clear();
            //load_grdview_bill();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
    }
    
    private void load_grdview_bill()
    {
        d.con1.Open();
        try
        {
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select pay_billing_master.id, policy_name1 as 'Policy',client_name as 'Client',unit_name as 'Branch',billing_state as 'State', (Select grade_desc from pay_grade_master where grade_code= pay_billing_master.Designation and comp_code='" + Session["comp_code"].ToString() + "') as Designation, cast(concat(hours,' Hrs') as char) as 'Working Hours', date_format(start_date,'%d/%m/%Y') as 'Policy Start Date', date_format(end_date,'%d/%m/%Y') as 'Policy End Date' from pay_billing_master inner join pay_client_master on pay_client_master.client_code = pay_billing_master.billing_client_code and pay_client_master.comp_code = pay_billing_master.comp_code inner join pay_unit_master on pay_unit_master.unit_code = pay_billing_master.billing_unit_code and pay_unit_master.comp_code = pay_billing_master.comp_code where billing_client_code = '" + client_Billing.SelectedValue + "' and pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con1);
            System.Data.DataTable DS1 = new System.Data.DataTable();
            MySqlDataAdapter1.Fill(DS1);
            //grd_policy.DataSource = DS1;
            //grd_policy.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }   
    protected void state_Billing_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            ddl_unitcode.Items.Clear();
            MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE  state_name= '" + state_Billing.SelectedValue + "' and CLIENT_CODE = '" + client_Billing.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + client_Billing.SelectedValue + "') and branch_status=0 ", d.con);
            MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            ddl_unitcode.DataSource = ds1.Tables[0];
            ddl_unitcode.DataValueField = "UNIT_CODE";
            ddl_unitcode.DataTextField = "CUNIT";
            ddl_unitcode.DataBind();

            // ddl_Existing_policy_name.Items.Clear();
            //ddl_designation.Items.Clear();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        d.con.Open();
        try
        {
            ddl_unitcode_without.Items.Clear();
            MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE state_name = '" + state_Billing.SelectedValue + "' and CLIENT_CODE = '" + client_Billing.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + client_Billing.SelectedValue + "')", d.con);
            MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            ddl_unitcode_without.DataSource = ds1.Tables[0];
            ddl_unitcode_without.DataValueField = "UNIT_CODE";
            ddl_unitcode_without.DataTextField = "CUNIT";
            ddl_unitcode_without.DataBind();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }

    //sallary Tab

    protected void ddl_client_list_sallary()
    {
        ddl_client_sallary.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CLIENT_NAME, CLIENT_CODE from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' ORDER BY client_code", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "')  and client_active_close='0'  ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_client_sallary.DataSource = dt_item;
                ddl_client_sallary.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_sallary.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_sallary.DataBind();
                }
            dt_item.Dispose();

            d.con.Close();

            ddl_client_sallary.Items.Insert(0, "Select");
            }
        catch (Exception ex) { throw ex; }
        finally
        {
           d.con.Close();
        }
    }
    protected void ddl_client_sallary_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client_sallary.SelectedValue != "Select")
        {
            //State
            ddl_state_sallary.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_sallary.SelectedValue + "' order by 1", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_state_sallary.DataSource = dt_item;
                    ddl_state_sallary.DataTextField = dt_item.Columns[0].ToString();
                    ddl_state_sallary.DataValueField = dt_item.Columns[0].ToString();
                    ddl_state_sallary.DataBind();

                }
                dt_item.Dispose();
                d.con.Close();
                ddl_state_sallary.Items.Insert(0, "Select");
                ddl_state_sallary.Items.Insert(1, "All");
                Bill_not_process.Items.Clear();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }

        }
        else
        { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }

    }
    
    //protected void ddl_state_sallary_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        Bill_not_process.Items.Clear();
    //       // if()
    //        string billing_amount = d.getsinglestring("SELECT billing_amt from payment_history where comp_code='" + Session["comp_code"] + "'and CLIENT_CODE = '" + ddl_client_sallary.SelectedValue + "' and state= '" + ddl_state_sallary.SelectedValue + "'");
    //        //if (ddl_state_sallary.SelectedValue == "ALL")
    //        //{
    //            if (billing_amount == "" || billing_amount == "0" || billing_amount == "0.0")
    //            {


    //                try
    //                {

    //                    // MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_unit_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM payment_history  WHERE state_name = '" + ddl_state_sallary.SelectedValue + "' and CLIENT_CODE = '" + ddl_client_sallary.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select state from payment_history where comp_code = '" + Session["COMP_CODE"].ToString() + "') ", d.con);
    //                    MySqlCommand cmd2 = new MySqlCommand("SELECT `pay_unit_master`.`unit_name`FROM  `pay_unit_master` where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and `pay_unit_master`.CLIENT_CODE= '" + ddl_client_sallary.SelectedValue + "' and `pay_unit_master`.state_name='" + ddl_state_sallary.SelectedValue + "'  and unit_code not in (select state from payment_history where comp_code = '" + Session["COMP_CODE"].ToString() + "') ", d.con);
    //                    d.con.Open();
    //                    MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
    //                    DataSet ds1 = new DataSet();
    //                    sda1.Fill(ds1);
    //                    Bill_not_process.DataSource = ds1.Tables[0];
    //                    Bill_not_process.DataValueField = "unit_name";
    //                    Bill_not_process.DataBind();
    //                    d.con.Close();
    //                }
    //                catch (Exception ex) { throw ex; }
    //                finally { d.con.Close(); }
    //            }
    //        //}

    //        else
    //        {
    //            MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("SELECT client_name,state,billing_amt,month_year,recived_amt,balance_amt from payment_history where comp_code='" + Session["comp_code"] + "'and CLIENT_CODE = '" + ddl_client_sallary.SelectedValue + "' and `state`='" + ddl_state_sallary.SelectedValue + "' ", d.con);
    //            d.con.Open();
    //            DataSet ds = new DataSet();

    //            adp_grid2.Fill(ds);
    //            grd_sallary.DataSource = ds;
    //            grd_sallary.DataBind();


    //            d.con.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        //d.con.Dispose();
    //        d.con.Close();
    //    }

    //}
    protected void grd_sallary_RowDataBound(object sender, GridViewRowEventArgs e)
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
   
    //attendace_remainig

    protected void client_attendance()
    {

        ddl_client_attendance.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_attendance.DataSource = dt_item;
                ddl_client_attendance.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_attendance.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_attendance.DataBind();
            }
            dt_item.Dispose();
            // hide_controls();
            d.con.Close();
            ddl_client_attendance.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void ddl_attendance_details1(Object sender, EventArgs e)
    {

        System.Data.DataTable dt_item = new System.Data.DataTable();
        //  pnl_branch.Visible = false;
        gridService.DataSource = null;
        gridService.DataBind();
        dt_item = d.chk_attendance1(Session["COMP_CODE"].ToString(), ddl_client_attendance.SelectedValue, ddl_unitcode1.SelectedValue, txt_date1.Text,0);
        if (dt_item.Rows.Count > 0)
        {
            Message = dt_item.Rows.Count.ToString();
            gridService.DataSource = dt_item;
            gridService.DataBind();
            //  pnl_branch.Visible = true;
        }
        else //new for comment
        {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found !!!');", true);
        }
        dt_item.Dispose();
         //System.Data.DataTable dt_item = new System.Data.DataTable();
         //       pnl_branch.Visible = false;
         //       gridService.DataSource = null;
         //       gridService.DataBind();
         //       dt_item = d.chk_attendance(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, txttodate.Text, 0);
         //       if (dt_item.Rows.Count > 0)
         //       {
         //           Message = dt_item.Rows.Count.ToString();
         //           gridService.DataSource = dt_item;
         //           gridService.DataBind();
         //           pnl_branch.Visible = true;
         //       }
         //       dt_item.Dispose();
         //   }
         //   catch (Exception ex) { throw ex; }
         //   finally
         //   {
         //       d.con1.Close();
         //   }
    }

    protected void ddl_client_attendance_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_unitcode1.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_attendance.SelectedValue + "' order by 1", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unitcode1.DataSource = dt_item;
                ddl_unitcode1.DataTextField = dt_item.Columns[0].ToString();
                ddl_unitcode1.DataValueField = dt_item.Columns[0].ToString();
                ddl_unitcode1.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            ddl_unitcode1.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        //ddl_attendance_details1(null, null);
       
    }
    protected void btn_attendance_search_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        ddl_attendance_details1(null, null);
    }

    protected void gridService_RowDataBound(object sender, GridViewRowEventArgs e)
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
  
    
// Employee Copliacence

    public void Employee_Compliances()
    {
        ddl_sate_empCompliances.Items.Clear();
        ddl_brachCompliances.Items.Clear();
        txt_emp_co.Text = "";
        txt_emp_co_name.Text = "";
        txt_emp_co_finance.Text = "";


        System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con1);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE ='" + Session["LOGIN_ID"].ToString() + "') and client_active_close='0' ORDER BY client_code", d.con);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_empCompliances.DataSource = dt_item;
                ddl_client_empCompliances.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_empCompliances.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_empCompliances.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_client_empCompliances.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }

    protected void ddl_client_empCompliances_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_sate_empCompliances.Items.Clear();
        ddl_brachCompliances.Items.Clear();
        txt_emp_co.Text = "";
        txt_emp_co_name.Text = "";
        txt_emp_co_finance.Text = "";

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_client_empCompliances.SelectedValue + "' and `LOCATION` in (select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client_empCompliances.SelectedValue + "')  ORDER BY LOCATION", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_sate_empCompliances.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        ddl_sate_empCompliances.Items.Insert(0, new ListItem("Select"));


    }
    protected void ddl_sate_empCompliances_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_brachCompliances.Items.Clear();
        txt_emp_co.Text = "";
        txt_emp_co_name.Text = "";
        txt_emp_co_finance.Text = "";

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_empCompliances.SelectedValue + "' and state_name = '" + ddl_sate_empCompliances.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_client_empCompliances.SelectedValue + "' AND state_name='" + ddl_sate_empCompliances.SelectedValue + "') and branch_status=0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_brachCompliances.DataSource = dt_item;
                ddl_brachCompliances.DataTextField = dt_item.Columns[0].ToString();
                ddl_brachCompliances.DataValueField = dt_item.Columns[1].ToString();
                ddl_brachCompliances.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_brachCompliances.Items.Insert(0, new ListItem("Select"));
        }

    }
    protected void ddl_brachCompliances_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        //   MySqlCommand cmd_item1 = new MySqlCommand("select  emp_name,PAN_NUMBER,EMP_NEW_PAN_NO ,ESIC_NUMBER ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE='" + ddl_brachCompliances.SelectedValue + "' AND client_code='" + ddl_client_empCompliances.SelectedValue + "'  and LOCATION='" + ddl_sate_empCompliances.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)", d1.con);
        //d1.con.Open();
        //try
        //{
        //    MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
        //    while (dr_item1=='')
        //    {
        //        ddl_sate_empCompliances.Items.Add(dr_item1[0].ToString());

        //    }
        //    dr_item1.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d1.con.Close();
        //}

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            string emp_namee = d.getsinglestring("SELECT `pay_employee_master`.`emp_name` from pay_employee_master where comp_code='" + Session["comp_code"] + "'and `pay_employee_master`.`UNIT_CODE` = '" + ddl_brachCompliances.SelectedValue + "'");
            if (emp_namee == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Record Found !!!');", true);
            }
            else
            {


                // System.Data.DataTable dt_item = new System.Data.DataTable();
                MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("select  (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'emp_name',PAN_NUMBER,EMP_NEW_PAN_NO ,ESIC_NUMBER ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND UNIT_CODE='" + ddl_brachCompliances.SelectedValue + "' AND client_code='" + ddl_client_empCompliances.SelectedValue + "'  and LOCATION='" + ddl_sate_empCompliances.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)", d.con);
                //MySqlDataAdapter adp_grid2 = new MySqlDataAdapter("select  emp_name,PAN_NUMBER,EMP_NEW_PAN_NO ,ESIC_NUMBER ,PF_NUMBER ,PF_DEDUCTION_FLAG ,cca,gratuity,special_allow,original_bank_account_no,BANK_HOLDER_NAME,PF_IFSC_CODE from pay_employee_master  where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)", d.con);
                d.con.Open();
                DataSet ds = new DataSet();
                adp_grid2.Fill(ds);
                Grid_compliances.DataSource = ds;
                Grid_compliances.DataBind();
            }
            d.con.Close();
           
            ddl_branch_Compliances_OnSelectedIndexChanged();
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

    protected void Grid_compliances_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0 && i!=7 && i!=8 && i!=9)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {

                        e.Row.Cells[i].Text = "Pending";
                        count++;
                        e.Row.Cells[i].BackColor = Color.Red;


                    }

                    else
                    {
                        e.Row.Cells[i].Text = "Done";

                        e.Row.Cells[i].BackColor = Color.GreenYellow;
                    }

                }
            }
            txt_count.Text = count.ToString();
        }

    }


//salary tab
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        final_billing();
        if (billing_amount == 0.0)
        {
            //txt_date.Text = "";
           // ddl_client_sallary.SelectedValue = "Select";
            //ddl_state_sallary.Items.Clear();
              

                                // MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_unit_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM payment_history  WHERE state_name = '" + ddl_state_sallary.SelectedValue + "' and CLIENT_CODE = '" + ddl_client_sallary.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select state from payment_history where comp_code = '" + Session["COMP_CODE"].ToString() + "') ", d.con);
            MySqlCommand cmd2 = new MySqlCommand("SELECT `pay_unit_master`.`unit_name`FROM  `pay_unit_master` where pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and `pay_unit_master`.CLIENT_CODE= '" + ddl_client_sallary.SelectedValue + "' and `pay_unit_master`.state_name='" + ddl_state_sallary.SelectedValue + "'  and unit_code not in (select state from payment_history where comp_code = '" + Session["COMP_CODE"].ToString() + "') and branch_status=0 ", d.con);
                                d.con.Open();
                                MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
                                DataSet ds1 = new DataSet();
                                sda1.Fill(ds1);
                                Bill_not_process.DataSource = ds1.Tables[0];
                                Bill_not_process.DataValueField = "unit_name";
                                Bill_not_process.DataBind();
                                d.con.Close();
                          
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('First Complete Billing Process !!!');", true);

                         

        }
        else
        {


            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                double recived_amt = 0;
                double balance_amt = 0;

                result = d.operation("INSERT INTO payment_history(CLIENT_CODE,client_name,state,billing_amt,month_year,recived_amt,balance_amt,comp_code) VALUES('" + ddl_client_sallary.SelectedItem.Value + "','" + ddl_client_sallary.SelectedItem.Text + "','" + ddl_state_sallary.SelectedItem.Value + "','" + billing_amount + "','" + txt_date.Text + "','" + recived_amt + "','" + billing_amount + "','" + Session["comp_code"].ToString() + "')");

                if (result > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Save Succsefully !!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment NOT save !!!');", true);
                }

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                gv_payment_history();
                d.con.Close();
            }
        }
    }


    protected void final_billing()
    {

        if (ddl_state_sallary.SelectedValue.ToString() == "ALL")
        {

            d.con.Open();

            String sql = "SELECT sum(Total + pf + esic) AS 'Amount', sum(((Total + pf + esic + (ot_rate * ot_hours)) * bill_service_charge) / 100) AS 'Service_charge', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform + (ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'CGST9', sum(CASE WHEN company_state != state_name THEN ROUND(((((Total + pf + esic+ operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 18) / 100), 2) ELSE 0 END) AS 'IGST18', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'SGST9',sum(uniform),sum(operational_cost), bill_service_charge, NH, hours,case when emp_cca = 0 then (sub_total_c -ot_rate) else (bill_gross + ((bill_gross * esic_percent) / 100) + bill_pf + bill_uniform ) end AS 'sub_total_c',ot_rate,ot_hours,(ot_rate * ot_hours) as 'ot_amount' FROM (SELECT client, company_state,unit_name,state_name, unit_city, client_branch_code, emp_name, grade_desc, emp_basic_vda, hra, bonus_gross, leave_gross, gratuity_gross, washing, travelling, education, allowances, cca_billing, other_allow, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) AS 'gross', bonus_after_gross, leave_after_gross, gratuity_after_gross, (((emp_basic_vda) / 100) * pf_percent) AS 'pf', (((emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) / 100) * esic_percent) AS 'esic', hrs_12_ot, (((hrs_12_ot) * esic_percent) / 100) AS 'esic_ot', lwf, CASE WHEN bill_ser_uniform = 1 THEN 0 ELSE uniform END AS 'uniform',relieving_charg, CASE WHEN bill_ser_operations = 1 THEN 0 ELSE operational_cost END AS 'operational_cost', tot_days_present, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross + bonus_after_gross + leave_after_gross + gratuity_after_gross + lwf + CASE WHEN bill_ser_uniform = 0 THEN 0 ELSE uniform END + relieving_charg + CASE WHEN bill_ser_operations = 0 THEN 0 ELSE operational_cost END + NH+ hrs_12_ot) AS 'Total', bill_service_charge, NH,hours,(bill_gross + emp_cca ) as 'bill_gross',sub_total_c,bill_ser_uniform,bill_ser_operations,(ot_rate+esi_on_ot_amount) as 'ot_rate',ot_hours,esic_amount,esi_on_ot_amount,emp_cca,bill_pf,bill_uniform,esic_percent FROM (SELECT (SELECT client_name FROM pay_client_master WHERE client_code = pay_unit_master.client_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'client', pay_company_master.state as 'company_state',pay_unit_master.unit_name, pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.client_branch_code, pay_employee_master.emp_name, pay_grade_master.grade_desc, (((pay_billing_master_history.basic + pay_billing_master_history.vda) / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_billing_unit_rate.hra / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra', CASE WHEN bonus_taxable = '1' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_gross', CASE WHEN leave_taxable = '0' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_after_gross', CASE WHEN gratuity_taxable = '1' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_gross', CASE WHEN gratuity_taxable = '0' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_after_gross', ((pay_billing_unit_rate.washing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing', ((pay_billing_unit_rate.traveling / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling', ((pay_billing_unit_rate.education / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education', ((pay_billing_unit_rate.national_holiday_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'NH', ((pay_billing_unit_rate.allowances / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_billing_unit_rate.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE ((pay_employee_master.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'cca_billing', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_billing_master_history.ot_policy_billing ='1' THEN ((pay_billing_master_history.ot_amount_billing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'hrs_12_ot', pay_billing_master_history.bill_esic_percent AS 'esic_percent', pay_billing_master_history.bill_pf_percent AS 'pf_percent', ((pay_billing_unit_rate.lwf / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'lwf', ((pay_billing_unit_rate.uniform / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'uniform', ((pay_billing_unit_rate.relieving_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'relieving_charg', ((pay_billing_unit_rate.operational_cost / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'operational_cost', pay_attendance_muster.tot_days_present, ROUND(((pay_billing_unit_rate.sub_total_c / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present), 2) AS 'baseamount', bill_service_charge,	pay_billing_master_history.hours,pay_billing_unit_rate.sub_total_c,	pay_billing_master_history.bill_ser_operations,	pay_billing_master_history.bill_ser_uniform,pay_billing_unit_rate.ot_1_hr_amount as 'ot_rate' ,pay_attendance_muster.ot_hours, pay_billing_unit_rate.esic_amount,pay_billing_unit_rate.esi_on_ot_amount,pay_employee_master.cca as 'emp_cca',      pay_billing_unit_rate.gross as 'bill_gross',pay_billing_unit_rate.pf_amount as 'bill_pf',pay_billing_unit_rate.uniform as 'bill_uniform'  FROM pay_employee_master INNER JOIN  pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND  pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE WHERE pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client_sallary.SelectedValue + "' and pay_attendance_muster.month = '" + txt_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0) AS billing_table) as Final_billing";

            MySqlCommand cmd_item = new MySqlCommand(sql, d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();


            if (dr.Read())
            {
                string amount = (dr.GetValue(0).ToString());
                string Service_charge = ((dr.GetValue(1).ToString()));
                string CGST9 = ((dr.GetValue(2).ToString()));
                string IGST18 = ((dr.GetValue(3).ToString()));
                string SGST8 = ((dr.GetValue(4).ToString()));
                string baseamount = ((dr.GetValue(5).ToString()));
                string emp_cca = ((dr.GetValue(6).ToString()));

                if (amount == "") { amount = "0"; }
                if (Service_charge == "") { Service_charge = "0"; }
                if (CGST9 == "") { CGST9 = "0"; }
                if (IGST18 == "") { IGST18 = "0"; }
                if (SGST8 == "") { SGST8 = "0"; }
                if (baseamount == "") { baseamount = "0"; }
                if (emp_cca == "") { emp_cca = "0"; }

                billing_amount = (double.Parse(amount) + double.Parse(Service_charge) + double.Parse(CGST9) + double.Parse(IGST18) + double.Parse(SGST8) + double.Parse(baseamount) + double.Parse(emp_cca));

            }
            dr.Close();
            d.con.Close();
        }
        else
        {
            d.con.Open();

            String sql = "SELECT sum(Total + pf + esic) AS 'Amount', sum(((Total + pf + esic + (ot_rate * ot_hours)) * bill_service_charge) / 100) AS 'Service_charge', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform + (ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'CGST9', sum(CASE WHEN company_state != state_name THEN ROUND(((((Total + pf + esic+ operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 18) / 100), 2) ELSE 0 END) AS 'IGST18', sum(CASE WHEN company_state = state_name THEN ROUND(((((Total + pf + esic + operational_cost + uniform+(ot_rate * ot_hours)) + (((Total + pf + esic+(ot_rate * ot_hours)) * bill_service_charge) / 100)) * 9) / 100), 2) ELSE 0 END) AS 'SGST9',sum(uniform),sum(operational_cost), bill_service_charge, NH, hours,case when emp_cca = 0 then (sub_total_c -ot_rate) else (bill_gross + ((bill_gross * esic_percent) / 100) + bill_pf + bill_uniform ) end AS 'sub_total_c',ot_rate,ot_hours,(ot_rate * ot_hours) as 'ot_amount' FROM (SELECT client, company_state,unit_name,state_name, unit_city, client_branch_code, emp_name, grade_desc, emp_basic_vda, hra, bonus_gross, leave_gross, gratuity_gross, washing, travelling, education, allowances, cca_billing, other_allow, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) AS 'gross', bonus_after_gross, leave_after_gross, gratuity_after_gross, (((emp_basic_vda) / 100) * pf_percent) AS 'pf', (((emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross+ hrs_12_ot) / 100) * esic_percent) AS 'esic', hrs_12_ot, (((hrs_12_ot) * esic_percent) / 100) AS 'esic_ot', lwf, CASE WHEN bill_ser_uniform = 1 THEN 0 ELSE uniform END AS 'uniform',relieving_charg, CASE WHEN bill_ser_operations = 1 THEN 0 ELSE operational_cost END AS 'operational_cost', tot_days_present, (emp_basic_vda + hra + bonus_gross + leave_gross + washing + travelling + education + allowances + cca_billing + other_allow + gratuity_gross + bonus_after_gross + leave_after_gross + gratuity_after_gross + lwf + CASE WHEN bill_ser_uniform = 0 THEN 0 ELSE uniform END + relieving_charg + CASE WHEN bill_ser_operations = 0 THEN 0 ELSE operational_cost END + NH+ hrs_12_ot) AS 'Total', bill_service_charge, NH,hours,(bill_gross + emp_cca ) as 'bill_gross',sub_total_c,bill_ser_uniform,bill_ser_operations,(ot_rate+esi_on_ot_amount) as 'ot_rate',ot_hours,esic_amount,esi_on_ot_amount,emp_cca,bill_pf,bill_uniform,esic_percent FROM (SELECT (SELECT client_name FROM pay_client_master WHERE client_code = pay_unit_master.client_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "') AS 'client', pay_company_master.state as 'company_state',pay_unit_master.unit_name, pay_unit_master.state_name, pay_unit_master.unit_city, pay_unit_master.client_branch_code, pay_employee_master.emp_name, pay_grade_master.grade_desc, (((pay_billing_master_history.basic + pay_billing_master_history.vda) / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_billing_unit_rate.hra / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra', CASE WHEN bonus_taxable = '1' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_gross', CASE WHEN bonus_taxable = '0' THEN ((pay_billing_unit_rate.bonus_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'bonus_after_gross', CASE WHEN leave_taxable = '1' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_gross', CASE WHEN leave_taxable = '0' THEN ((pay_billing_unit_rate.leave_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_after_gross', CASE WHEN gratuity_taxable = '1' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_gross', CASE WHEN gratuity_taxable = '0' THEN ((pay_billing_unit_rate.grauity_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'gratuity_after_gross', ((pay_billing_unit_rate.washing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing', ((pay_billing_unit_rate.traveling / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling', ((pay_billing_unit_rate.education / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education', ((pay_billing_unit_rate.national_holiday_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'NH', ((pay_billing_unit_rate.allowances / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_billing_unit_rate.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE ((pay_employee_master.cca / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'cca_billing', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_billing_master_history.other_allow / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_billing_master_history.ot_policy_billing ='1' THEN ((pay_billing_master_history.ot_amount_billing / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'hrs_12_ot', pay_billing_master_history.bill_esic_percent AS 'esic_percent', pay_billing_master_history.bill_pf_percent AS 'pf_percent', ((pay_billing_unit_rate.lwf / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'lwf', ((pay_billing_unit_rate.uniform / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'uniform', ((pay_billing_unit_rate.relieving_amount / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'relieving_charg', ((pay_billing_unit_rate.operational_cost / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'operational_cost', pay_attendance_muster.tot_days_present, ROUND(((pay_billing_unit_rate.sub_total_c / pay_billing_unit_rate.month_days) * pay_attendance_muster.tot_days_present), 2) AS 'baseamount', bill_service_charge,	pay_billing_master_history.hours,pay_billing_unit_rate.sub_total_c,	pay_billing_master_history.bill_ser_operations,	pay_billing_master_history.bill_ser_uniform,pay_billing_unit_rate.ot_1_hr_amount as 'ot_rate' ,pay_attendance_muster.ot_hours, pay_billing_unit_rate.esic_amount,pay_billing_unit_rate.esi_on_ot_amount,pay_employee_master.cca as 'emp_cca',      pay_billing_unit_rate.gross as 'bill_gross',pay_billing_unit_rate.pf_amount as 'bill_pf',pay_billing_unit_rate.uniform as 'bill_uniform'  FROM pay_employee_master INNER JOIN  pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code AND pay_attendance_muster.comp_code = pay_unit_master.comp_code INNER JOIN pay_billing_unit_rate ON pay_attendance_muster.unit_code = pay_billing_unit_rate.unit_code AND pay_attendance_muster.month = pay_billing_unit_rate.month AND pay_attendance_muster.year = pay_billing_unit_rate.year INNER JOIN pay_billing_master_history ON pay_billing_master_history.comp_code = pay_billing_unit_rate.comp_code AND  pay_billing_master_history.comp_code = pay_employee_master.comp_code AND pay_billing_master_history.billing_client_code = pay_billing_unit_rate.client_code AND pay_billing_master_history.billing_unit_code = pay_billing_unit_rate.unit_code AND pay_billing_master_history.month = pay_billing_unit_rate.month AND pay_billing_master_history.year = pay_billing_unit_rate.year AND pay_employee_master.grade_code = pay_billing_master_history.designation AND pay_billing_master_history.designation = pay_billing_unit_rate.designation AND pay_billing_master_history.hours = pay_billing_unit_rate.hours INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_billing_master_history.comp_code = pay_grade_master.comp_code AND pay_billing_master_history.designation = pay_grade_master.GRADE_CODE WHERE pay_company_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client_sallary.SelectedValue + "' and pay_unit_master.state_name = '" + ddl_state_sallary.SelectedValue + "' and pay_attendance_muster.month = '" + txt_date.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_date.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0) AS billing_table) as Final_billing";

            MySqlCommand cmd_item = new MySqlCommand(sql, d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();


            if (dr.Read())
            {

                string amount = (dr.GetValue(0).ToString());
                string Service_charge = ((dr.GetValue(1).ToString()));
                string CGST9 = ((dr.GetValue(2).ToString()));
                string IGST18 = ((dr.GetValue(3).ToString()));
                string SGST8 = ((dr.GetValue(4).ToString()));
                string baseamount = ((dr.GetValue(5).ToString()));
                string emp_cca = ((dr.GetValue(6).ToString()));

                if (amount == "") { amount = "0"; }
                if (Service_charge == "") { Service_charge = "0"; }
                if (CGST9 == "") { CGST9 = "0"; }
                if (IGST18 == "") { IGST18 = "0"; }
                if (SGST8 == "") { SGST8 = "0"; }
                if (baseamount == "") { baseamount = "0"; }
                if (emp_cca == "") { emp_cca = "0"; }

                billing_amount = (double.Parse(amount) + double.Parse(Service_charge) + double.Parse(CGST9) + double.Parse(IGST18) + double.Parse(SGST8) + double.Parse(baseamount) + double.Parse(emp_cca));
                // billing_amount = Math.Round(((double.Parse(dr.GetValue(0).ToString())) + (double.Parse(dr.GetValue(1).ToString())) + (double.Parse(dr.GetValue(2).ToString())) + (double.Parse(dr.GetValue(3).ToString())) + (double.Parse(dr.GetValue(4).ToString())) + (double.Parse(dr.GetValue(5).ToString())) + (double.Parse(dr.GetValue(6).ToString()))), 2);


            }
            dr.Close();
            d.con.Close();
        }

    }
    protected void gv_payment_history()
    {

        DataTable dt_item = new DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  sr_no,client_name,state,billing_amt,month_year,recived_amt,balance_amt   from payment_history  ", d.con);
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                grd_sallary.DataSource = dt_item;
                grd_sallary.DataBind();
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

    protected void grd_sallary_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_sallary.UseAccessibleHeader = false;
            grd_sallary.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}
