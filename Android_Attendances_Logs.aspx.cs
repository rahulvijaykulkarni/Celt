using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class Android_Attendances_Logs : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();

    GradeBAL gbl3 = new GradeBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "R")
        {
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "C")
        {
        }

        if (!IsPostBack)
        {
            client_code();
            client_fire_code();
           //fire_extinguisher_photo();
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        int result = 0;

        System.Web.UI.WebControls.Label lbl_GRADE_CODE = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_GRADE_CODE");
        string l_GRADE_CODE = lbl_GRADE_CODE.Text;
        d.con1.Open();
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("Select GRADE_CODE from pay_employee_master where GRADE_CODE='" + l_GRADE_CODE + "' and comp_code='" + Session["comp_code"] + "' ", d.con1);
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Grade against employee exist can not delete this Grade');", true);
            }
            else
            {
                result = d.operation("DELETE FROM pay_grade_master WHERE comp_code='" + Session["comp_code"].ToString() + "' AND GRADE_CODE='" + l_GRADE_CODE + "'");
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed!!');", true);
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
        }
    }
    protected void btnexporttoexcelgrade_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 30;


        Range rng = ws.get_Range("E1:E1");
        rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange2 = ws.get_Range("E1:E1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange2.Font.Size = 20;

        Range rng1 = ws.get_Range("E2:E2");
        rng1.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange3 = ws.get_Range("E2:E2");
        formateRange3.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange3.Font.Size = 20;

        Range rng3 = ws.get_Range("A5:C5");
        rng3.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange4 = ws.get_Range("A5:C5");
        formateRange4.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange4.Font.Size = 15;


        ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        ws.Cells[2, 5] = "GRADE LIST";
        ws.Cells[5, 1] = "GRADE CODE";
        ws.Cells[5, 2] = "GRADE NAME";
        ws.Cells[5, 3] = "REPORTING TO";
        try
        {
            d.con1.Open();
            MySqlCommand cmd2 = new MySqlCommand("SELECT GRADE_CODE,GRADE_DESC, REPORTING_TO FROM pay_grade_master WHERE (comp_code = '" + Session["comp_code"].ToString() + "') ORDER BY GRADE_CODE", d.con1);
            DataSet ds2 = new DataSet();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT GRADE_CODE,GRADE_DESC, REPORTING_TO FROM pay_grade_master WHERE (comp_code = '" + Session["comp_code"].ToString() + "') ORDER BY GRADE_CODE", d.con1);
            //adp2.Fill(ds2);
            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 6;
            //int i=0;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                //string mystr = row["EMP_CODE"].ToString();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[j, i + 1] = row[i].ToString();
                }
                j++;
            }
            xla.Visible = true;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
        finally
        {
            d.con1.Close();

        }

    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (dr["Attendances_intime_images"].ToString() != "")
            {
                imageUrl = "~/attendance_images/" + dr["Attendances_intime_images"];
                (e.Row.FindControl("Camera_Image1") as Image).ImageUrl = imageUrl;

            }
            if (dr["Attendances_outtime_images"].ToString() != "")
            {

                imageUrl2 = "~/attendance_images/" + dr["Attendances_outtime_images"];
                (e.Row.FindControl("Camera_Image2") as Image).ImageUrl = imageUrl2;

            }
            if (dr["Camera_intime_images"].ToString() != "")
            {
                imageUrl = "~/attendance_images/" + dr["Camera_intime_images"];
                (e.Row.FindControl("Camera_Image1") as Image).ImageUrl = imageUrl;
            }
            if (dr["Camera_outtime_images"].ToString() != "")
            {
                imageUrl2 = "~/attendance_images/" + dr["Camera_outtime_images"];
                (e.Row.FindControl("Camera_Image2") as Image).ImageUrl = imageUrl2;
            }

            //string imageUrl = "~/attendance_images/" + dr["Camera_intime_images"];
            //(e.Row.FindControl("Camera_Image1") as Image).ImageUrl = imageUrl;

            //string imageUrl2 = "~/attendance_images/" + dr["Camera_outtime_images"];
            //(e.Row.FindControl("Camera_Image2") as Image).ImageUrl = imageUrl2;

            //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "Select$" + e.Row.RowIndex);
        }
    }

    protected void GradeGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            GradeGridView.UseAccessibleHeader = false;
            GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void client_code()
    {
        GradeGridView.DataSource = null;
        GradeGridView.DataBind();
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ")) and client_active_close='0' ORDER BY client_code", d.con);
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
            // hide_controls();
            d.con.Close();
            ddl_client.Items.Insert(0, "ALL");
            ddlunitselect.Items.Insert(0, "Select");
            ddlunitselect.Items.Insert(1, "ALL");
            grd_work_image.Visible = false;
            GradeGridView.Visible = false;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
       
        if (ddl_client.SelectedValue == "ALL")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item1 = new System.Data.DataTable();
            MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item1.Fill(dt_item1);
                if (dt_item1.Rows.Count > 0)
                {
                    ddl_state.DataSource = dt_item1;
                    ddl_state.DataTextField = dt_item1.Columns[0].ToString();
                    ddl_state.DataValueField = dt_item1.Columns[0].ToString();
                    ddl_state.DataBind();
                }
                dt_item1.Dispose();
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

    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
  {
        GradeGridView.DataSource = null;
        GradeGridView.DataBind();

        if (ddl_state.SelectedValue == "ALL")
        {
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME' , `unit_code` from pay_unit_master where comp_code='" + Session["comp_code"] + "'  and branch_status = 0 ORDER BY UNIT_NAME", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddlunitselect.DataSource = dt_item;
                    ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                    ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                    ddlunitselect.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                //ddl_branch_increment.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(1, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }


        }
        else
        {
            ddlunitselect.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ) AND state_name='" + ddl_state.SelectedValue + "' and  branch_status = 0 ORDER BY UNIT_NAME", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddlunitselect.DataSource = dt_item;
                    ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                    ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                    ddlunitselect.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                //ddl_branch_increment.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(1, "ALL");
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
        //companyGridView1.DataSource = null;
        //companyGridView1.DataBind();
        //companyGridView.DataSource = null;
        //companyGridView.DataBind();
        //if (ddl_client.SelectedValue != "Select")
        //{
            //State
        GradeGridView.DataSource = null;
        GradeGridView.DataBind();
        ddl_state.Items.Clear();
        ddlunitselect.Items.Clear();
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
                ddl_state.Items.Insert(0, "ALL");
                
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string unit_code = "";
        string state_name = "";
        if (ddl_state.SelectedValue == "ALL")
        {
             state_name = "SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1";
        }
        else
        {
            state_name = "'" + ddl_state.SelectedValue + "'";
        
        }
        if (ddl_client.SelectedValue == "ALL")
        {
             unit_code = ("select unit_code FROM pay_unit_master WHERE  comp_code = '" + Session["COMP_CODE"].ToString() + "' AND  state_name in ("+state_name+") and  branch_status = 0 ORDER BY UNIT_NAME");
        }
        else
        {
            unit_code = ("select unit_code FROM pay_unit_master WHERE  comp_code = '" + Session["COMP_CODE"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' and state_name in (" + state_name + ")  AND branch_status = 0 ORDER BY UNIT_NAME");
        }

        if (ddl_att_work.SelectedValue == "Attendance")
        {
            d.con.Open();
            try
            {
                MySqlDataAdapter dscmd;

                if (ddl_client.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter(" SELECT `pay_client_master`.`client_name` , `pay_unit_master`.`unit_name` , `pay_android_attendance_logs`.`EMP_CODE`, `UNIT_LATITUDE`, `UNIT_LONGTUTDE`, `EMP_LATITUDE`, `EMP_LONGTUTDE`, `DISTANCES`, `ADDRESS`, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime, `Camera_intime_images`, `Camera_outtime_images`, `Attendances_intime_images`, `Attendances_outtime_images` FROM `pay_android_attendance_logs`  inner join `pay_unit_master` on `pay_android_attendance_logs`.`comp_code`=`pay_unit_master`.`comp_code`  and `pay_android_attendance_logs`.`unit_code`=`pay_unit_master`.`unit_code` inner join `pay_client_master` on `pay_unit_master`.`comp_code`=`pay_client_master`.`comp_code` and `pay_unit_master`.`client_code`=`pay_client_master`.`client_code` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`EMP_CODE` = `pay_android_attendance_logs`.`emp_code` WHERE pay_android_attendance_logs.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_android_attendance_logs.unit_code in (" + unit_code + ")  and pay_android_attendance_logs.`date_time` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')  order by date_time desc limit 200", d.con);
                }
                else if (ddl_client.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter("  SELECT `pay_client_master`.`client_name` , `pay_unit_master`.`unit_name` , `pay_android_attendance_logs`.`EMP_CODE`, `UNIT_LATITUDE`, `UNIT_LONGTUTDE`, `EMP_LATITUDE`, `EMP_LONGTUTDE`, `DISTANCES`, `ADDRESS`, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime, `Camera_intime_images`, `Camera_outtime_images`, `Attendances_intime_images`, `Attendances_outtime_images` FROM `pay_android_attendance_logs`  inner join `pay_unit_master` on `pay_android_attendance_logs`.`comp_code`=`pay_unit_master`.`comp_code`  and `pay_android_attendance_logs`.`unit_code`=`pay_unit_master`.`unit_code` inner join `pay_client_master` on `pay_unit_master`.`comp_code`=`pay_client_master`.`comp_code` and `pay_unit_master`.`client_code`=`pay_client_master`.`client_code` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`EMP_CODE` = `pay_android_attendance_logs`.`emp_code` WHERE pay_android_attendance_logs.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code='" + ddl_client.SelectedValue + "' and pay_android_attendance_logs.unit_code in (" + unit_code + ") and pay_android_attendance_logs.`date_time` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')  order by date_time desc limit 200", d.con);
                }
                else
                {
                    dscmd = new MySqlDataAdapter("Select (select client_name from pay_client_master where client_code='" + ddl_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') as client_name ,(SELECT DISTINCT(`pay_unit_master`.`unit_name`) FROM `pay_unit_master` WHERE `unit_code` = '" + ddlunitselect.SelectedValue + "' AND comp_code='" + Session["COMP_CODE"].ToString() + "')as unit_name,pay_android_attendance_logs.EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,(SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END) AS 'EMP_NAME',date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime,Camera_intime_images,Camera_outtime_images,Attendances_intime_images,Attendances_outtime_images from pay_android_attendance_logs inner join pay_employee_master on pay_employee_master.`EMP_CODE`=pay_android_attendance_logs.emp_code where pay_android_attendance_logs.UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND pay_android_attendance_logs.comp_code = '" + Session["comp_code"].ToString() + "' and date_time between str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + " 23:59:59','%d/%m/%Y %H:%i:%s') order by date_time desc limit 200", d.con);
                }
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GradeGridView.DataSource = ds;
                    GradeGridView.DataBind();
                    GradeGridView.Visible = true;
                    grd_work_image.Visible = false;
                    grd_current_location.Visible = false;
                    gv_attendances_excel.Visible = false;
                }
                else
                {
                    GradeGridView.DataSource = null;
                    GradeGridView.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records');", true);
                }
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else if (ddl_att_work.SelectedValue == "Attendance Excel")
            {
                d.con.Open();
                try
                {
                    MySqlDataAdapter dscmd;

                    if (ddl_client.SelectedValue == "ALL")
                    {
                        dscmd = new MySqlDataAdapter(" SELECT `pay_client_master`.`client_name` , `pay_unit_master`.`unit_name` , `pay_android_attendance_logs`.`EMP_CODE`, `UNIT_LATITUDE`, `UNIT_LONGTUTDE`, `EMP_LATITUDE`, `EMP_LONGTUTDE`, `DISTANCES`, `ADDRESS`, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime FROM `pay_android_attendance_logs`  inner join `pay_unit_master` on `pay_android_attendance_logs`.`comp_code`=`pay_unit_master`.`comp_code`  and `pay_android_attendance_logs`.`unit_code`=`pay_unit_master`.`unit_code` inner join `pay_client_master` on `pay_unit_master`.`comp_code`=`pay_client_master`.`comp_code` and `pay_unit_master`.`client_code`=`pay_client_master`.`client_code` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`EMP_CODE` = `pay_android_attendance_logs`.`emp_code` WHERE pay_android_attendance_logs.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_android_attendance_logs.unit_code in (" + unit_code + ")  and pay_android_attendance_logs.`date_time` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')  order by pay_android_attendance_logs.id desc", d.con);
                    }
                    else if (ddl_client.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL")
                    {
                        dscmd = new MySqlDataAdapter("  SELECT `pay_client_master`.`client_name` , `pay_unit_master`.`unit_name` , `pay_android_attendance_logs`.`EMP_CODE`, `UNIT_LATITUDE`, `UNIT_LONGTUTDE`, `EMP_LATITUDE`, `EMP_LONGTUTDE`, `DISTANCES`, `ADDRESS`, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime FROM `pay_android_attendance_logs`  inner join `pay_unit_master` on `pay_android_attendance_logs`.`comp_code`=`pay_unit_master`.`comp_code`  and `pay_android_attendance_logs`.`unit_code`=`pay_unit_master`.`unit_code` inner join `pay_client_master` on `pay_unit_master`.`comp_code`=`pay_client_master`.`comp_code` and `pay_unit_master`.`client_code`=`pay_client_master`.`client_code` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`EMP_CODE` = `pay_android_attendance_logs`.`emp_code` WHERE pay_android_attendance_logs.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code='" + ddl_client.SelectedValue + "' and pay_android_attendance_logs.unit_code in (" + unit_code + ") and pay_android_attendance_logs.`date_time` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')  order by pay_android_attendance_logs.id desc", d.con);
                    }
                    else
                    {
                        dscmd = new MySqlDataAdapter("Select (select client_name from pay_client_master where client_code='" + ddl_client.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "') as client_name ,(SELECT DISTINCT(`pay_unit_master`.`unit_name`) FROM `pay_unit_master` WHERE `unit_code` = '" + ddlunitselect.SelectedValue + "' AND comp_code='" + Session["COMP_CODE"].ToString() + "')as unit_name,pay_android_attendance_logs.EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,(SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END) AS 'EMP_NAME',date_format(`Date_Time`,'%d/%m/%Y %h:%i:%s %p') as Date_Time,date_format(`attendances_intime`,'%d/%m/%Y %h:%i:%s %p') as attendances_intime, date_format(`attendances_outtime`,'%d/%m/%Y %h:%i:%s %p') as attendances_outtime ,date_format(`camera_intime`,'%d/%m/%Y %h:%i:%s %p') as camera_intime,date_format(`camera_outtime`,'%d/%m/%Y %h:%i:%s %p') as camera_outtime from pay_android_attendance_logs inner join pay_employee_master on pay_employee_master.`EMP_CODE`=pay_android_attendance_logs.emp_code where pay_android_attendance_logs.UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND pay_android_attendance_logs.comp_code = '" + Session["comp_code"].ToString() + "' and date_time between str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + " 23:59:59','%d/%m/%Y %H:%i:%s') order by pay_android_attendance_logs.id desc", d.con);
                    }
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_attendances_excel.DataSource = ds;
                        gv_attendances_excel.DataBind();
                        gv_attendances_excel.Visible = true;
                        GradeGridView.Visible = false;
                        grd_work_image.Visible = false;
                        grd_current_location.Visible = false;
                    }
                    else
                    {
                        gv_attendances_excel.DataSource = null;
                        gv_attendances_excel.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records');", true);
                    }
                    d.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con.Close();
                }
            }
        else if (ddl_att_work.SelectedValue == "Work")
        {  
        
            d.con.Open();
            try
            {
                MySqlDataAdapter dscmd;

                if (ddl_client.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter(" SELECT pay_android_working_image.emp_code, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', pay_unit_master.`unit_name`, pay_android_working_image.`datecurrent`, pay_android_working_image.`image_name` FROM `pay_android_working_image`  inner join `pay_unit_master` on `pay_android_working_image`.`comp_code`=`pay_unit_master`.`comp_code` and  `pay_android_working_image`.`unit_code`=`pay_unit_master`.`unit_code` INNER JOIN `pay_employee_master` ON `pay_android_working_image`.`EMP_CODE` = `pay_employee_master`.`emp_code` WHERE `pay_android_working_image`.`comp_code` = '" + Session["comp_code"].ToString() + "'  AND `datecurrent` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s') ORDER BY pay_android_working_image.`datecurrent`,`pay_android_working_image`.`unit_code`  DESC LIMIT 200", d.con);
                
                }
                else if (ddl_client.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter("SELECT (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',  pay_unit_master.`unit_name`, pay_android_working_image.`datecurrent`, pay_android_working_image.`image_name` FROM `pay_android_working_image` inner join `pay_unit_master` on `pay_android_working_image`.`comp_code`=`pay_unit_master`.`comp_code`  and `pay_android_working_image`.`unit_code`=`pay_unit_master`.`unit_code` inner join `pay_client_master` on `pay_unit_master`.`comp_code`=`pay_client_master`.`comp_code` and `pay_unit_master`.`client_code`=`pay_client_master`.`client_code` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`EMP_CODE` = `pay_android_working_image`.`emp_code` WHERE `pay_android_working_image`.`comp_code` = '" + Session["comp_code"].ToString() + "' and  `pay_unit_master`.`client_code`='" + ddl_client.SelectedValue + "' and pay_android_working_image.unit_code in (" + unit_code + ") AND `datecurrent` BETWEEN STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') AND STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s') ORDER BY pay_android_working_image.`datecurrent` DESC LIMIT 200", d.con);
                
                }
                else
                {
                    dscmd = new MySqlDataAdapter("select (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME', unit_name, datecurrent, image_name from pay_android_working_image inner join pay_employee_master on pay_android_working_image.emp_code = pay_employee_master.emp_code inner join pay_unit_master on pay_android_working_image.unit_code = pay_unit_master.unit_code and pay_android_working_image.comp_code = pay_unit_master.comp_code where pay_android_working_image.UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND pay_android_working_image.comp_code = '" + Session["comp_code"].ToString() + "' and datecurrent between str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + " 23:59:59','%d/%m/%Y %H:%i:%s') order by datecurrent desc limit 200", d.con);
                }
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_work_image.DataSource = ds;
                    grd_work_image.DataBind();
                    grd_work_image.Visible = true;
                    GradeGridView.Visible = false;
                    grd_current_location.Visible = false;
                    gv_attendances_excel.Visible = false;
                }
                else
                {
                    grd_work_image.DataSource = null;
                    grd_work_image.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records');", true);
                }
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        
        else if (ddl_att_work.SelectedValue == "Employee Current Location")
          
            {
            d.con.Open();
            try
            {
                MySqlDataAdapter dscmd;

                if (ddl_client.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter("SELECT `pay_geolocation_address`.`id`, pay_geolocation_address.unit_code,pay_geolocation_address.client_code,( SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END ) AS 'emp_code', `cur_address`, `cur_latitude`, `cur_longtitude`, `cur_date` FROM `pay_geolocation_address` INNER JOIN `pay_employee_master` ON `pay_geolocation_address`.`EMP_CODE` = `pay_employee_master`.`emp_code`  WHERE `pay_geolocation_address`.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_geolocation_address.`cur_date` BETWEEN   STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') and  STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')    ORDER BY pay_geolocation_address.`cur_date` DESC LIMIT 200", d.con);

                }
                else if (ddl_client.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL")
                {
                    dscmd = new MySqlDataAdapter("SELECT `pay_geolocation_address`.`id`, pay_geolocation_address.unit_code,pay_geolocation_address.client_code,( SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END ) AS 'emp_code', `cur_address`, `cur_latitude`, `cur_longtitude`, `cur_date` FROM `pay_geolocation_address` INNER JOIN `pay_employee_master` ON `pay_geolocation_address`.`EMP_CODE` = `pay_employee_master`.`emp_code`  WHERE `pay_geolocation_address`.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_geolocation_address.client_code='" + ddl_client.SelectedValue + "' and  pay_geolocation_address.unit_code in (" + unit_code + ") pay_geolocation_address.`cur_date` BETWEEN   STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') and  STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')    ORDER BY pay_geolocation_address.`cur_date` DESC LIMIT 200", d.con);

                }
                else {
                    dscmd = new MySqlDataAdapter("SELECT `pay_geolocation_address`.`id`,pay_geolocation_address.unit_code,pay_geolocation_address.client_code,( SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END ) AS 'emp_code', `cur_address`, `cur_latitude`, `cur_longtitude`, `cur_date` FROM `pay_geolocation_address` INNER JOIN `pay_employee_master` ON `pay_geolocation_address`.`EMP_CODE` = `pay_employee_master`.`emp_code`  WHERE `pay_geolocation_address`.`comp_code` = '" + Session["comp_code"].ToString() + "' and pay_geolocation_address.client_code='" + ddl_client.SelectedValue + "' and pay_geolocation_address.unit_code='" + ddlunitselect.SelectedValue + "' and pay_geolocation_address.`cur_date` BETWEEN   STR_TO_DATE('" + txt_satrtdate.Text + "', '%d/%m/%Y') and  STR_TO_DATE('" + txt_enddate.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')    ORDER BY pay_geolocation_address.`cur_date` DESC LIMIT 200", d.con);
                
                }
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    grd_current_location.DataSource = ds;
                    grd_current_location.DataBind();
                    grd_current_location.Visible = true;
                    GradeGridView.Visible = false;
                    grd_work_image.Visible = false;
                    gv_attendances_excel.Visible = false;
                }
                else
                {
                    grd_current_location.DataSource = null;
                    grd_current_location.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records');", true);
                }
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            }
        else if (ddl_att_work.SelectedValue == "Attendance Excel Download") {
        
        
        }

        

    }
    protected void grd_work_image_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_work_image.UseAccessibleHeader = false;
            grd_work_image.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void grd_work_image_RowDataBound(object sender, GridViewRowEventArgs e)
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
            string imageUrl = "";
            //imageUrl = "~/attendance_images/" + dr["image_name"];
            imageUrl =  dr["image_name"].ToString();
            (e.Row.FindControl("Camera_Image3") as Image).ImageUrl = imageUrl;
        }
    }

    protected void grd_location_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_current_location.UseAccessibleHeader = false;
            grd_current_location.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void GradeGridView_RowDataBound_location(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grd_current_location, "Select$" + e.Row.RowIndex);
            
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView dr = (DataRowView)e.Row.DataItem;

        //    string latitude = dr["cur_latitude"].ToString();
        //    string longtitude = dr["cur_longtitude"].ToString();
        //    string address = dr["cur_address"].ToString();
        //    //(e.Row.FindControl("Camera_Image3") as Image).ImageUrl = imageUrl;
        //    Session["MAP_UNIT_CODE"] = "U003";
        //    Response.Redirect("location_map.aspx");

        //}
    }

    protected void Location_SelectedIndexChanged(object sender, EventArgs e)
    {
       // System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)grd_current_location.SelectedRow.FindControl("id");
        string id_no = grd_current_location.SelectedRow.Cells[1].Text;
       
        Session["UNIT_NO"] = ddlunitselect.SelectedValue.ToString();
         Session["MAP_ID"]=id_no;
         Session["MAP_ADDRESS"] = grd_current_location.SelectedRow.Cells[6].Text;
         Session["MAP_LONGITUDE"] = grd_current_location.SelectedRow.Cells[4].Text;
         Session["MAP_LATTITUDE"] = grd_current_location.SelectedRow.Cells[3].Text;
         Session["MAP_AREA"] = "100";


            Response.Redirect("location_map.aspx");
          
    }
    protected void gv_fire_photo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[8].Visible = false;
      //  e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl = "";
            if (dr["image_path"].ToString() != "")
            {

                imageUrl = "~/fire_extinguisher_image/" + dr["image_path"];
                (e.Row.FindControl("fire_upload_image") as Image).ImageUrl = imageUrl;

            }
        }


    }
    protected void gv_fire_photo_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_fire_photo.UseAccessibleHeader = false;
            gv_fire_photo.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void fire_extinguisher_photo()
    {

        MySqlDataAdapter cmd_id_gv = new MySqlDataAdapter(" select pay_fire_extinguisher_photo.id,pay_fire_extinguisher_photo.client_code,client_name,unit_code,unit_name,emp_code,emp_name,state_name,curr_date,image_path,CASE WHEN `approve_fire` = '0' THEN 'Pending' WHEN `approve_fire` = '1' THEN 'Rejected' WHEN `approve_fire` = '2' THEN 'Approve' when approve_fire = '3' then 'Move This Record' END AS 'approve_fire',reject_reason,type_name from pay_fire_extinguisher_photo  INNER JOIN `pay_client_master` ON `pay_fire_extinguisher_photo`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_fire_extinguisher_photo`.`client_code` = `pay_client_master`.`client_code` where pay_fire_extinguisher_photo.comp_code = '" + Session["comp_code"].ToString() + "' ", d.con);
        d.con.Open();
        System.Data.DataTable dt_id_gv = new System.Data.DataTable();

        cmd_id_gv.Fill(dt_id_gv);

        gv_fire_photo.DataSource = dt_id_gv;
        gv_fire_photo.DataBind();


        dt_id_gv.Dispose();


    }
    protected void lnk_remove_fire_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        int result = 0;
        result = d.operation("delete from pay_fire_extinguisher_photo where id = '" + grdrow.Cells[3].Text + "'");

        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);

        }


    }
    protected void btn_approve_fire_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try 
        {
            string inlist = null;
            foreach (GridViewRow gvrow in gv_fire_photo.Rows)
            {
                string id = gv_fire_photo.Rows[gvrow.RowIndex].Cells[3].Text;
                string client_code = gv_fire_photo.Rows[gvrow.RowIndex].Cells[15].Text;
                string unit_code = gv_fire_photo.Rows[gvrow.RowIndex].Cells[5].Text;
                string state_name = gv_fire_photo.Rows[gvrow.RowIndex].Cells[6].Text;
                string type_fire = gv_fire_photo.Rows[gvrow.RowIndex].Cells[14].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    
                    inlist = "" + id + "";

                int result = 0;

                    if (inlist != "")
                    {
                        string check_entry = d.getsinglestring("select client_code, unit_code,`state_name`,`fire_ex_type` from pay_fire_extinguisher where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + client_code + "' and unit_code = '" + unit_code + "' and state_name = '" + state_name + "' and `fire_ex_type` = '" + type_fire + "'  ");

                        if (check_entry == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('This Record Not Added In Branch Master !!!')", true);
                            return;
                        }


                        result = d.operation("UPDATE pay_fire_extinguisher_photo SET approve_fire = '2',`reject_reason` ='' WHERE comp_code = '" + Session["comp_code"].ToString() + "'  and id = '" + inlist + "' ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Record Approve  Successfully !!!')", true);
                    }
                }

            
            }
            fire_extinguisher_photo();
        }
        catch (Exception ex) { throw ex; }
        finally{}
    }
    protected void btn_reject_fire_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try
        {
         string inlist = null; string reason_fire = null;
            foreach (GridViewRow gvrow in gv_fire_photo.Rows)
            {
                string id = gv_fire_photo.Rows[gvrow.RowIndex].Cells[3].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {
                    
                    inlist = "" + id + "";

                     System.Web.UI.WebControls.TextBox txt_fire_amt = (System.Web.UI.WebControls.TextBox)gvrow.FindControl("txt_fire_amt");
                     reason_fire = (txt_fire_amt.Text);


                       if (reason_fire=="") 
                       {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reject Reason')", true);
                           return;
                       
                       }

                }

                int result = 0;

                if (reason_fire != "")
                {
                    result = d.operation("UPDATE pay_fire_extinguisher_photo SET approve_fire = '1' ,`reject_reason` = '" + reason_fire + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "'  and id = '" + inlist + "' ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Record Rejected  Successfully !!!')", true);
                }

                    

            }

            fire_extinguisher_photo();
        }
    
        catch (Exception ex) { throw ex; }
        finally{}

    }
    protected void btn_move_fire_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        try {
           // d1.con.Open();
            string newpath = null;
            string inlist = null;

            foreach (GridViewRow gvrow in gv_fire_photo.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string id = gv_fire_photo.Rows[gvrow.RowIndex].Cells[3].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    inlist =  "" + id + "";


                    if (inlist != "")
                    {

                        d2.con.Open();
                        MySqlCommand cmd_re = new MySqlCommand("select client_code,unit_code,`type_name`,image_path,approve_fire,state_name from pay_fire_extinguisher_photo where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + inlist + "'", d2.con);



                        MySqlDataReader dr = cmd_re.ExecuteReader();
                        if (dr.Read())
                        {
                            string client_code = dr.GetValue(0).ToString();
                            string unit_code = dr.GetValue(1).ToString();
                            string type_name = dr.GetValue(2).ToString();
                            string path = dr.GetValue(3).ToString();
                            string approve = dr.GetValue(4).ToString();
                            string state_name = dr.GetValue(5).ToString();
                            //  int approve_fire = Int32.Parse(dr.GetValue(4).ToString());

                            if (approve == "2")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_fire_extinguisher_photo where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + client_code + "' and unit_code = '" + unit_code + "' and type_name ='" + type_name + "' and id = '" + inlist + "' ");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";

                                int res = d.operation("update pay_fire_extinguisher set fire_upload='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and fire_ex_type ='" + type_name + "' and client_code = '" + client_code + "' and unit_code = '" + unit_code + "' and state_name = '" + state_name + "'");
                                int result = d.operation("UPDATE pay_fire_extinguisher_photo SET approve_fire = '3' WHERE comp_code = '" + Session["comp_code"].ToString() + "'  and id = '" + inlist + "' ");

                                if (result > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Move Images Successfully !!!')", true);
                                    // int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + path + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    //System.IO.File.Delete(Server.MapPath("~/fire_extinguisher_image/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/fire_extinguisher_image/") + path, Server.MapPath("~/fire_extinguisher/") + path);
                                    // System.IO.File.Copy(Server.MapPath("~/fire_extinguisher_image/") + path, Server.MapPath("~/fire_extinguisher/") + newpath);
                                    // System.IO.File.Delete(Server.MapPath("~/fire_extinguisher/") + path);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('First Approve This Record !!!')", true);
                                return;

                            }
                        }  // for dr

                        dr.Dispose();

                    } // for inlist
                }

            
           }  // for foreach

            fire_extinguisher_photo();
        }
        catch (Exception ex) { throw ex; }
        finally { d2.con.Close(); }
    }

    // fire extinguisher fire filter 19-08-2020

    protected void client_fire_code() 
    {
        ddl_client_fire.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_fire.DataSource = dt_item;
                ddl_client_fire.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_fire.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_fire.DataBind();
            }
            dt_item.Dispose();
           
            d.con.Close();
            ddl_client_fire.Items.Insert(0, "Select");
            ddl_client_fire.Items.Insert(1, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    

    }

    protected void ddl_client_fire_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string where = "";
        if (!ddl_client_fire.SelectedValue.Equals("ALL"))
        {
            where = "and client_code = '" + ddl_client_fire.SelectedValue + "'";
        }


        //State

        System.Data.DataTable dt_item = new System.Data.DataTable();
        ddl_state_fire.Items.Clear();
        dt_item = new System.Data.DataTable();

        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + where + " ORDER BY state_name", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state_fire.DataSource = dt_item;
                ddl_state_fire.DataTextField = dt_item.Columns[0].ToString();
                ddl_state_fire.DataValueField = dt_item.Columns[0].ToString();
                ddl_state_fire.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state_fire.Items.Insert(0, "ALL");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            //ddl_state_SelectedIndexChanged(null, null);
        }


    }
    protected void ddl_state_fire_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_show_fire_Click(object sender, EventArgs e)
    {
        string where = "";
        if (!ddl_state_fire.SelectedValue.Equals("ALL"))
        {
            where = "and state_name = '" + ddl_state_fire.SelectedValue + "'";
        }

        MySqlDataAdapter cmd_id_gv = new MySqlDataAdapter(" select pay_fire_extinguisher_photo.id,pay_fire_extinguisher_photo.client_code,client_name,unit_code,unit_name,emp_code,emp_name,state_name,curr_date,image_path,CASE WHEN `approve_fire` = '0' THEN 'Pending' WHEN `approve_fire` = '1' THEN 'Rejected' WHEN `approve_fire` = '2' THEN 'Approve' when approve_fire = '3' then 'Move This Record' END AS 'approve_fire',reject_reason,type_name from pay_fire_extinguisher_photo  INNER JOIN `pay_client_master` ON `pay_fire_extinguisher_photo`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_fire_extinguisher_photo`.`client_code` = `pay_client_master`.`client_code` where pay_fire_extinguisher_photo.comp_code = '" + Session["comp_code"].ToString() + "' and pay_fire_extinguisher_photo.client_code = '" + ddl_client_fire.SelectedValue + "' " + where + " ", d.con);
        d.con.Open();
        System.Data.DataTable dt_id_gv = new System.Data.DataTable();

        cmd_id_gv.Fill(dt_id_gv);

        gv_fire_photo.DataSource = dt_id_gv;
        gv_fire_photo.DataBind();


        dt_id_gv.Dispose();




    }

    protected void gv_attendances_excel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_attendances_excel, "Select$" + e.Row.RowIndex);

        }
    }

    protected void attendances_excel_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_attendances_excel.UseAccessibleHeader = false;
            gv_attendances_excel.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}