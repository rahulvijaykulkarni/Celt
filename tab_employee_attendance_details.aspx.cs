using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class tab_employee_attendance_details : System.Web.UI.Page
{
    DAL d = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            client_name();
        }

    }

    protected void client_name() {
        d.con.Open();
        try
        {
            MySqlDataAdapter grd_client = new MySqlDataAdapter("select CLIENT_NAME,`CLIENT_CODE` from pay_client_master where COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' ", d.con);
            DataTable dt_client = new DataTable();
            grd_client.Fill(dt_client);
            if (dt_client.Rows.Count>0) {

                ddl_client.DataSource = dt_client;
                ddl_client.DataTextField = dt_client.Columns[0].ToString();
                ddl_client.DataValueField = dt_client.Columns[1].ToString();
                ddl_client.DataBind();
              
            }
           

        }
        catch (Exception ex) { throw ex; }
        finally {
            ddl_client.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

      
        if (ddl_client.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') order by 1", d.con);
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
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "'  and UNIT_CODE  in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
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
            ddl_unit.Items.Insert(0, "Select");
         
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT unit_name as unit_code,emp_name,grade_desc,shifttime,CASE punctuality WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'punctuality',CASE `uniforms` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'uniforms',  CASE `cap` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'cap',  CASE `shoes` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shoes',  CASE `belt` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'belt',  CASE `id_card` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'id_card',  CASE `shaving` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shaving',  CASE `hairs` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'hairs',  CASE `nails` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'nails', CASE `briefing` WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'briefing',  `intime_imgpath`,  `outtime_imgpath`,remarks,location_add,date_format(`currdate`,'%d/%m/%Y %H:%i:%s') as 'currdate' FROM  `pay_tab_employee_attendances_details` INNER JOIN `pay_unit_master` ON `pay_tab_employee_attendances_details`.`client_code` = `pay_unit_master`.`client_code` AND `pay_tab_employee_attendances_details`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_tab_employee_attendances_details.comp_code='" + Session["comp_code"] + "' and  pay_tab_employee_attendances_details.`client_code` = '" + ddl_client.SelectedValue + "' AND pay_tab_employee_attendances_details.`unit_code` = '" + ddl_unit.SelectedValue + "' and date(currdate)=date(str_to_date('" + txt_date.Text.ToString() + "','%d/%m/%Y'))", d.con);
            DataTable dt_client = new DataTable();
            grd_client.Fill(dt_client);
            if (dt_client.Rows.Count > 0)
            {

                gv_emp_attendance.DataSource = dt_client;
                gv_emp_attendance.DataBind();

            }


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            
            d.con.Close();
        }
    }
    protected void gv_emp_attendance_RowDataBound(object sender, GridViewRowEventArgs e)
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
             DataRowView dr = (DataRowView)e.Row.DataItem;
             string imageUrl, imageUrl2 = "";
             if (dr["intime_imgpath"].ToString() != "")
             {

                 imageUrl = "~/tab_attendance_images/" + dr["intime_imgpath"];
                 (e.Row.FindControl("intime_imgpath") as Image).ImageUrl = imageUrl;

             }

             if (dr["outtime_imgpath"].ToString() != "")
             {
                 imageUrl2 = "~/tab_attendance_images/" + dr["outtime_imgpath"];
                 (e.Row.FindControl("outtime_imgpath") as Image).ImageUrl = imageUrl2;

             }
         }
    }
    protected void gv_emp_attendance_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_emp_attendance.UseAccessibleHeader = false;
            gv_emp_attendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}