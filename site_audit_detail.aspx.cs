using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;


public partial class site_audit_detail : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        reject1.Visible = false;
        if (d.getaccess(Session["ROLE"].ToString(), "Site Audit", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Site Audit", Session["COMP_CODE"].ToString()) == "R")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Site Audit", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Site Audit", Session["COMP_CODE"].ToString()) == "C")
        {

        }
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(pay_client_state_role_grade.`client_code`),`client_name` FROM `pay_client_master` INNER JOIN  pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_state_role_grade.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") and client_active_close='0' order by `client_code`", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client.DataSource = dt_item;
                    ddl_client.DataTextField = dt_item.Columns[1].ToString();
                    ddl_client.DataValueField = dt_item.Columns[0].ToString();
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
        ///


        //try
        //{

        //    companyGridView.Visible = true;
        //    d.con1.Open();

        //  // MySqlDataAdapter adp_grid = new MySqlDataAdapter("select id, (select emp_name from pay_employee_master where emp_code = pay_site_audit.emp_code) as emp_code,grade_name,concat('Guards present On Right Place ? :- ',que_1_ans) as que_1_ans,que_1_path,concat( 'Is Ac Working ? :- ',que_2_ans) as que_2_ans,que_2_path,concat('Is Guards strength correct ? :- ',que_3_ans) as que_3_ans,que_3_path,concat('Is CCTV Working Properly ? :- ',que_4_ans) as que_4_ans,que_4_path,concat('Is Traning Needed On Site ? :- ',que_5_ans) as que_5_ans,que_5_path,location,comment,remark from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' ", d.con);
        //    MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT  `id`,(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code',`grade_name`,(case when grade_name='HOUSEKEEPING'	then  CONCAT('Meet to concern person ? :- ', `que_1_ans`) when grade_name='SECURITY GUARD'	then 	 CONCAT('Meet to concern person ? :- ', `que_1_ans`) when grade_name='R&M'	then 	CONCAT('Meet to concern person ? :- ', `que_1_ans`) else CONCAT('Meet to concern person ? :- ', `que_1_ans`) end)AS 'que_1_ans',`que_1_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) when grade_name='SECURITY GUARD'	then  CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) when grade_name='R&M'	then 	CONCAT('Check wiring  ? :- ', `que_2_ans`) else CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`)	end )AS 'que_2_ans',`que_2_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check dressup & grooming? :- ', `que_3_ans`)  when grade_name='SECURITY GUARD' then  CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) when grade_name='R&M'	then 	CONCAT('Check electrical / electronics items? :- ', `que_3_ans`)else CONCAT('Check dressup & grooming? :- ', `que_3_ans`) 	end )AS 'que_3_ans',`que_3_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) when grade_name='SECURITY GUARD' then  CONCAT('Check I-card & shooes ? :- ', `que_4_ans`)  when grade_name='R&M'	then 	CONCAT('Check furniture ? :- ', `que_4_ans`)else CONCAT('Check I-card & shooes ? :- ', `que_4_ans`)	end )AS 'que_4_ans',`que_4_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check office cleaning  ? :- ', `que_5_ans`) when grade_name='SECURITY GUARD' then  CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`)  when grade_name='R&M'	then 	CONCAT('Check plumbing work? :- ', `que_5_ans`) else CONCAT('Check office cleaning  ? :- ', `que_5_ans`)	end )AS 'que_5_ans',`que_5_path`, (case when grade_name='HOUSEKEEPING'	then CONCAT('Check washroom cleaning ? :- ', `que_6_ans`)  when grade_name='SECURITY GUARD' then  CONCAT('Guard is present on place  ? :- ', `que_6_ans`)  when grade_name='R&M'	then 	CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) else CONCAT('Check washroom cleaning ? :- ', `que_6_ans`)	end )AS 'que_6_ans',`que_6_path`, location,comment,remark,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' when 3 then 'Completed' else '' end ) as 'Status' FROM `pay_site_audit`  where comp_code='" + Session["COMP_CODE"].ToString() + "' order by id DESC ", d.con);
        //    DataSet ds = new DataSet();
        //    adp_grid.Fill(ds);
        //    companyGridView.DataSource = ds;
        //    companyGridView.DataBind();
        //    d.con1.Close();



        //    // }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }


    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        //companyGridView1.DataSource = null;
        //companyGridView1.DataBind();
        //companyGridView.DataSource = null;
        //companyGridView.DataBind();
        if (ddl_client.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `client_code` = '" + ddl_client.SelectedValue + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
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
                client_search();
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
    protected void client_search()
    {
        try
        {

            companyGridView.Visible = true;
            d.con1.Open();
            MySqlDataAdapter adp_grid = null;
            //vikas 08-01-19
            if (ddl_feedback_type.SelectedValue=="1")
            {
                //rahul 09-10-2020
                adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code  where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_site_audit.`client_code` = '" + ddl_client.SelectedValue + "' order by pay_site_audit.id DESC ", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView.DataSource = ds;
                companyGridView.DataBind();
            }
            else
            {
                adp_grid = new MySqlDataAdapter("select `ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "' ORDER BY `id` DESC", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView1.DataSource = ds;
                companyGridView1.DataBind();
            }
           
            d.con1.Close();

            // }
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

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ")  AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
             state_search();
        }
    }
    protected void state_search()
    {
        try
        {

            companyGridView.Visible = true;
            d.con1.Open();
            //vikas 08-01-19
            MySqlDataAdapter adp_grid = null;
            if (ddl_feedback_type.SelectedValue == "1")
            {
                //rahul 09-10-2020
                adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code   where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_site_audit.`client_code` = '" + ddl_client.SelectedValue + "' and pay_unit_master.`STATE_NAME`='" + ddl_state.SelectedValue + "'   order by pay_site_audit.id DESC ", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView.DataSource = ds;
                companyGridView.DataBind();
            }
            else
            {
                adp_grid = new MySqlDataAdapter("select pay_service_rating.`ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE pay_service_rating.`flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code INNER JOIN `pay_unit_master` ON `pay_service_rating`.`client_code` = `pay_unit_master`.`client_code` AND `pay_service_rating`.`UNIT_CODE` = `pay_unit_master`.`UNIT_CODE`  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "' and pay_unit_master.`STATE_NAME`='" + ddl_state.SelectedValue + "'  ORDER BY `id` DESC", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView1.DataSource = ds;
                companyGridView1.DataBind();
            }
            d.con1.Close();

            // }
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

    //protected void btn_close_click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Home.aspx");
    //}

    protected void companyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if ((e.Row.Cells[18].Text == "Reject") || (e.Row.Cells[18].Text == "Approved"))
            {
                e.Row.Cells[19].Text = "";

            }

            if ((e.Row.Cells[18].Text == "Reject") || (e.Row.Cells[18].Text == "Pending") || (e.Row.Cells[18].Text == "Completed") || (e.Row.Cells[18].Text == "Mailsend"))
            {
                e.Row.Cells[20].Text = "";

            }

            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl, imageUrl2, imageUrl3, imageUrl4, imageUrl5, imageUrl6 = "";
            if (dr["que_1_path"].ToString() != "")
            {

                imageUrl = "~/site_audit/" + dr["que_1_path"];
                (e.Row.FindControl("que_1_path") as Image).ImageUrl = imageUrl;

            }

            if (dr["que_2_path"].ToString() != "")
            {
                imageUrl2 = "~/site_audit/" + dr["que_2_path"];
                (e.Row.FindControl("que_2_path") as Image).ImageUrl = imageUrl2;

            }
            if (dr["que_3_path"].ToString() != "")
            {
                imageUrl3 = "~/site_audit/" + dr["que_3_path"];
                (e.Row.FindControl("que_3_path") as Image).ImageUrl = imageUrl3;

            }
            if (dr["que_4_path"].ToString() != "")
            {
                imageUrl4 = "~/site_audit/" + dr["que_4_path"];
                (e.Row.FindControl("que_4_path") as Image).ImageUrl = imageUrl4;

            }
            if (dr["que_5_path"].ToString() != "")
            {
                imageUrl5 = "~/site_audit/" + dr["que_5_path"];
                (e.Row.FindControl("que_5_path") as Image).ImageUrl = imageUrl5;

            }
            if (dr["que_6_path"].ToString() != "")
            {
                imageUrl6 = "~/site_audit/" + dr["que_6_path"];
                (e.Row.FindControl("que_6_path") as Image).ImageUrl = imageUrl6;

            }

        }

        e.Row.Cells[19].Visible = false;
    }
    //protected void btn_show_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //        if (ddl_feedback_type.SelectedValue == "1")
    //        {
    //            companyGridView1.DataSource = null;
    //            companyGridView1.DataBind();
    //            System.Data.DataTable dt_item = new System.Data.DataTable();
    //            // vikas 08-01-19
    //            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  `id`,(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code',`grade_name`,(case when grade_name='HOUSEKEEPING'	then  CONCAT('Meet to concern person ? :- ', `que_1_ans`) when grade_name='SECURITY GUARD'	then 	 CONCAT('Meet to concern person ? :- ', `que_1_ans`) when grade_name='R&M'	then 	CONCAT('Meet to concern person ? :- ', `que_1_ans`) else CONCAT('Meet to concern person ? :- ', `que_1_ans`) end)AS 'que_1_ans',`que_1_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) when grade_name='SECURITY GUARD'	then  CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) when grade_name='R&M'	then 	CONCAT('Check wiring  ? :- ', `que_2_ans`) else CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`)	end )AS 'que_2_ans',`que_2_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check dressup & grooming? :- ', `que_3_ans`)  when grade_name='SECURITY GUARD' then  CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) when grade_name='R&M'	then 	CONCAT('Check electrical / electronics items? :- ', `que_3_ans`)else CONCAT('Check dressup & grooming? :- ', `que_3_ans`) 	end )AS 'que_3_ans',`que_3_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) when grade_name='SECURITY GUARD' then  CONCAT('Check I-card & shooes ? :- ', `que_4_ans`)  when grade_name='R&M'	then 	CONCAT('Check furniture ? :- ', `que_4_ans`)else CONCAT('Check I-card & shooes ? :- ', `que_4_ans`)	end )AS 'que_4_ans',`que_4_path`,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check office cleaning  ? :- ', `que_5_ans`) when grade_name='SECURITY GUARD' then  CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`)  when grade_name='R&M'	then 	CONCAT('Check plumbing work? :- ', `que_5_ans`) else CONCAT('Check office cleaning  ? :- ', `que_5_ans`)	end )AS 'que_5_ans',`que_5_path`, (case when grade_name='HOUSEKEEPING'	then CONCAT('Check washroom cleaning ? :- ', `que_6_ans`)  when grade_name='SECURITY GUARD' then  CONCAT('Guard is present on place  ? :- ', `que_6_ans`)  when grade_name='R&M'	then 	CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) else CONCAT('Check washroom cleaning ? :- ', `que_6_ans`)	end )AS 'que_6_ans',`que_6_path`, location,comment,remark,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' else '' end ) as 'Status' FROM pay_site_audit where  comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code = '" + dd1_super.SelectedValue + "' AND client_code = '" + ddl_client.SelectedValue + "' AND unit_code = '" + ddl_unit.SelectedValue + "'  order by id desc", d.con);

    //            cmd_item.Fill(dt_item);

    //            if (dt_item.Rows.Count > 0)
    //            {

    //                companyGridView.DataSource = dt_item;
    //                companyGridView.DataBind();
    //            }
    //            else
    //            {
    //                companyGridView.DataSource = null;
    //                companyGridView.DataBind();
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records Found');", true);
    //            }
    //            dt_item.Dispose();
    //            d.con.Close();
    //        }
    //        //ddl_client.Items.Insert(0, "Select");
    //        else
    //        {
    //            load_service_gv();
    //        }

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {

    //        d.con.Close();
    //    }
    //}

    protected void dd1_super_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            companyGridView.Visible = true;
            d.con1.Open();
            MySqlDataAdapter adp_grid = null;
            if (ddl_feedback_type.SelectedValue == "1")
            {
                //rahul - 09-10-2020
                adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code  where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_site_audit.`client_code` = '" + ddl_client.SelectedValue + "'  AND pay_site_audit.`unit_code` = '" + ddl_unit.SelectedValue + "' and pay_site_audit.`emp_code`='" + dd1_super.SelectedValue + "'  order by pay_site_audit.id DESC ", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView.DataSource = ds;
                companyGridView.DataBind();
            }
            else
            {
                adp_grid = new MySqlDataAdapter("select pay_service_rating.`ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE pay_service_rating.`flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code INNER JOIN `pay_unit_master` ON `pay_service_rating`.`client_code` = `pay_unit_master`.`client_code` AND `pay_service_rating`.`UNIT_CODE` = `pay_unit_master`.`UNIT_CODE`  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "'  AND pay_service_rating.unit_code = '" + ddl_unit.SelectedValue + "' and pay_service_rating.`emp_code`='" + dd1_super.SelectedValue + "'   ORDER BY `id` DESC", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView1.DataSource = ds;
                companyGridView1.DataBind();
            }
            d.con1.Close();
            // }
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
    protected void ddl_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd1_super.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        //vikas 08-01-2019
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_op_management.EMP_CODE FROM pay_op_management INNER JOIN pay_employee_master  ON pay_op_management.COMP_CODE=pay_employee_master.COMP_CODE  AND pay_op_management.EMP_CODE = pay_employee_master.EMP_CODE WHERE pay_op_management.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_op_management.unit_code = '" + ddl_unit.SelectedValue + "' order by id", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                dd1_super.DataSource = dt_item;
                dd1_super.DataTextField = dt_item.Columns[0].ToString();
                dd1_super.DataValueField = dt_item.Columns[1].ToString();

                dd1_super.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            dd1_super.Items.Insert(0, "Select");
            branch_search();
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void branch_search()
    {
        try
        {
            companyGridView.Visible = true;
            d.con1.Open();

            MySqlDataAdapter adp_grid = null;
            if (ddl_feedback_type.SelectedValue == "1")
            {
                // rahul - 09-10-2020
                adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code  where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_site_audit.`client_code` = '" + ddl_client.SelectedValue + "' and pay_site_audit.`unit_code`='" + ddl_unit.SelectedValue + "'  order by pay_site_audit.id DESC ", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView.DataSource = ds;
                companyGridView.DataBind();
            }
            else
            {
                adp_grid = new MySqlDataAdapter("select `ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "' and pay_service_rating.`unit_code`='" + ddl_unit.SelectedValue + "'  ORDER BY `id` DESC", d.con);
                DataSet ds = new DataSet();
                adp_grid.Fill(ds);
                companyGridView1.DataSource = ds;
                companyGridView1.DataBind();
            }

            //vikas 08-01-19
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

    protected void gv_emp_d_varification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int index = companyGridView.Rows[e.NewEditIndex].RowIndex;
            //int request_id = int.Parse(companyGridView.DataKeys[index].Values[1].ToString());
            //int request_id = int.Parse(companyGridView.DataKeys[e.RowIndex].Values[0].ToString());
            int request_id = int.Parse(companyGridView.DataKeys[index].Values[0].ToString());

            int res = d.operation("update pay_site_audit set comment='Approved Document' where  Id='" + request_id + "'");

        }
        catch (Exception ex) { throw ex; }
        finally
        {


        }

    }


    protected void gv_emp_varification_reject(object sender, GridViewDeleteEventArgs e)
    {
        //  int index = companyGridView.Rows[e.NewEditIndex].RowIndex;
        int request_id = int.Parse(companyGridView.DataKeys[e.RowIndex].Values[0].ToString());

        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Id, comment from pay_site_audit  where comp_code='" + Session["comp_code"].ToString() + "' and  Id='" + request_id + "'   ", d.con);
            d.con.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                text_id.Text = dr.GetValue(0).ToString();
                text_comment.Text = dr.GetValue(1).ToString();
                Session["text_id"] = request_id;
            }

        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
            reject1.Visible = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string emp_code = "", client_code = "", unit_code = "", que_1_newpath = "", que_2_newpath = "", que_3_newpath = "", que_4_newpath = "", que_5_newpath = "", que_6_newpath = "";
        int reject_id = 0;
        string que_1_finalpath1 = "", que_2_finalpath = "", que_3_finalpath = "", que_4_finalpath = "", que_5_finalpath = "", que_6_finalpath = "";
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("select client_code,unit_code,emp_code,que_1_path,que_2_path,que_3_path,que_4_path,que_5_path,reject,que_6_path from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + text_id.Text + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code = dr.GetValue(0).ToString();
                unit_code = dr.GetValue(1).ToString();
                emp_code = dr.GetValue(2).ToString();
                que_1_newpath = dr.GetValue(3).ToString();
                que_2_newpath = dr.GetValue(4).ToString();
                que_3_newpath = dr.GetValue(5).ToString();
                que_4_newpath = dr.GetValue(6).ToString();
                que_5_newpath = dr.GetValue(7).ToString();
                reject_id = Int32.Parse(dr.GetValue(8).ToString());
                que_6_newpath = dr.GetValue(9).ToString();

                if (reject_id == 0)
                {
                    string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and unit_code='" + unit_code + "'");
                    String newpath1233 = que_1_newpath.Replace(".png", "");
                    // String newpath = path.Remove(path.Length - 3);
                    que_1_finalpath1 = que_1_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_2_finalpath = que_2_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_3_finalpath = que_3_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_4_finalpath = que_4_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_5_finalpath = que_5_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_6_finalpath = que_6_newpath.Replace(".png", "") + "_" + temp1 + ".png";



                    // System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                    // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_1_newpath, Server.MapPath("~/site_audit/") + que_1_finalpath1);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_1_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_2_newpath, Server.MapPath("~/site_audit/") + que_2_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_2_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_3_newpath, Server.MapPath("~/site_audit/") + que_3_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_3_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_4_newpath, Server.MapPath("~/site_audit/") + que_4_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_4_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_5_newpath, Server.MapPath("~/site_audit/") + que_5_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_5_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_6_newpath, Server.MapPath("~/site_audit/") + que_6_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_6_newpath);

                    int res = d.operation("update pay_site_audit set comment='" + text_comment.Text + "', que_1_path='" + que_1_finalpath1 + "',que_2_path='" + que_2_finalpath + "',que_3_path='" + que_3_finalpath + "', que_4_path='" + que_4_finalpath + "',que_5_path='" + que_5_finalpath + "',que_6_path='" + que_6_finalpath + "', reject='1',android_flag='1' where  Id='" + text_id.Text + "'");
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Reject site audit successfully !!!')", true);
                    Response.Write("<script type='text/javascript'>");
                    Response.Write("alert('Record Reject Successfully');");
                    Response.Write("document.location.href='site_audit_detail.aspx';");
                    Response.Write("</script>");
                    // int res = d.operation("update pay_site_audit set comment='Approved Document' where  Id='" + item + "'");
                }
                else if (reject_id == 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Site audit already Approve  !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Site audit already rejected  !!!')", true);
                }
            }
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            // rahul 09-10-2020
            d.con1.Open();
            //MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT Id,client_name,unit_name,new_employee_name,grade FROM pay_new_employee_requirement ORDER BY ID", d1.con1);
            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "'  order by pay_site_audit.id DESC", d.con);

            DataSet ds_grid = new DataSet();
            adp_grid.Fill(ds_grid);
            companyGridView.DataSource = ds_grid;
            companyGridView.DataBind();
            d.con1.Close();
            reject1.Visible = false;
        }
        //     int res = d.operation("update pay_site_audit set comment='" + text_comment.Text + "' where  id='" + text_id.Text + "' ");
        //if (res > 0)
        //{
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Successfully.');", true);
        //    Response.Write("<script type='text/javascript'>");
        //    Response.Write("alert('Record Reject Successfully');");
        //    Response.Write("document.location.href='site_audit_detail.aspx';");
        //    Response.Write("</script>");
        //    //Response.Write("<script language='javascript'>alert('OK');</script>");
        //    //Response.Redirect("site_audit_detail.aspx");
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Faill!!.');", true);
        //}

        //text_comment.Text = "";


    }
    protected void companyGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string myString = companyGridView.SelectedRow.Cells[0].Text.ToString();


        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT comment,Id from pay_site_audit  where  Id='" + text_id.Text + "'   ", d.con);


            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                text_comment.Text = dr.GetValue(0).ToString();
                text_id.Text = dr.GetValue(1).ToString();


            }

        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();

            //    btn_Delet.Visible = true;
            //    btn_Update.Visible = true;

        }



    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        int item = (int)companyGridView.DataKeys[gvrow.RowIndex].Value;

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("SELECT Id, comment from pay_site_audit  where comp_code='" + Session["comp_code"].ToString() + "' and  Id='" + item + "'  ", d.con);
            d.con.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                text_id.Text = dr.GetValue(0).ToString();
                text_comment.Text = dr.GetValue(1).ToString();
            }

        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
            reject1.Visible = true;
        }

    }


    protected void lnkbtn_edititem_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        int item = (int)companyGridView.DataKeys[gvrow.RowIndex].Value;
        string emp_code = "", client_code = "", unit_code = "", que_1_newpath = "", que_2_newpath = "", que_3_newpath = "", que_4_newpath = "", que_5_newpath = "", que_6_newpath = "";
        int reject_id = 0;
        string que_1_finalpath1 = "", que_2_finalpath = "", que_3_finalpath = "", que_4_finalpath = "", que_5_finalpath = "", que_6_finalpath = "";
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("select client_code,unit_code,emp_code,que_1_path,que_2_path,que_3_path,que_4_path,que_5_path,reject,que_6_path from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + item + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client_code = dr.GetValue(0).ToString();
                unit_code = dr.GetValue(1).ToString();
                emp_code = dr.GetValue(2).ToString();
                que_1_newpath = dr.GetValue(3).ToString();
                que_2_newpath = dr.GetValue(4).ToString();
                que_3_newpath = dr.GetValue(5).ToString();
                que_4_newpath = dr.GetValue(6).ToString();
                que_5_newpath = dr.GetValue(7).ToString();
                reject_id = Int32.Parse(dr.GetValue(8).ToString());
                que_6_newpath = dr.GetValue(9).ToString();

                if (reject_id == 0)
                {
                    string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and unit_code='" + unit_code + "'");
                    String newpath1233 = que_1_newpath.Replace(".png", "");
                    // String newpath = path.Remove(path.Length - 3);
                    que_1_finalpath1 = que_1_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_2_finalpath = que_2_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_3_finalpath = que_3_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_4_finalpath = que_4_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_5_finalpath = que_5_newpath.Replace(".png", "") + "_" + temp1 + ".png";
                    que_6_finalpath = que_6_newpath.Replace(".png", "") + "_" + temp1 + ".png";



                    // System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                    // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_1_newpath, Server.MapPath("~/site_audit/") + que_1_finalpath1);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_1_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_2_newpath, Server.MapPath("~/site_audit/") + que_2_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_2_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_3_newpath, Server.MapPath("~/site_audit/") + que_3_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_3_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_4_newpath, Server.MapPath("~/site_audit/") + que_4_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_4_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_5_newpath, Server.MapPath("~/site_audit/") + que_5_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_5_newpath);
                    System.IO.File.Copy(Server.MapPath("~/site_audit/") + que_6_newpath, Server.MapPath("~/site_audit/") + que_6_finalpath);
                    System.IO.File.Delete(Server.MapPath("~/site_audit/") + que_6_newpath);

                    int res = d.operation("update pay_site_audit set comment='Approved Document', que_1_path='" + que_1_finalpath1 + "',que_2_path='" + que_2_finalpath + "',que_3_path='" + que_3_finalpath + "', que_4_path='" + que_4_finalpath + "',que_5_path='" + que_5_finalpath + "',que_6_path='" + que_6_finalpath + "', reject='2',android_flag='1' where  Id='" + item + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve site audit successfully !!!')", true);
                    // int res = d.operation("update pay_site_audit set comment='Approved Document' where  Id='" + item + "'");
                }
                else if (reject_id == 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Site audit already Approve  !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Site audit already rejected  !!!')", true);
                }
            }
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            // rahul -09-10-2020
            d.con1.Open();
            //MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT Id,client_name,unit_name,new_employee_name,grade FROM pay_new_employee_requirement ORDER BY ID", d1.con1);
            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code  where pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "'  order by pay_site_audit.id DESC", d.con);
            DataSet ds = new DataSet();
            adp_grid.Fill(ds);
            companyGridView.DataSource = ds;
            companyGridView.DataBind();
            d.con1.Close();

        }

    }

    protected void companyGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            companyGridView.UseAccessibleHeader = false;
            companyGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void lnkbtn_complete_Click(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            int item = (int)companyGridView.DataKeys[gvrow.RowIndex].Value;

            int res = d.operation("update pay_site_audit set  reject='3',comment='Completed Document',android_flag='3' where  Id='" + item + "'");
         
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Completed successfully !!!')", true);

            site_audit_gv();
         
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void  site_audit_gv()
    { 
            
                companyGridView1.DataSource = null;
                companyGridView1.DataBind();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            // rahul 09-10-2020
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT pay_site_audit.`id`, pay_unit_master.STATE_NAME as 'state_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  as 'unit_name',date_format(cur_date,'%d/%m/%Y %H:%i') as 'visit_date', (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END AS 'EMP_NAME' FROM `pay_employee_master` WHERE `emp_code` = `pay_site_audit`.`emp_code`) AS 'emp_code', `grade_name`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Meet to concern person ? :- ', `que_1_ans`) ELSE CONCAT('Meet to concern person ? :- ', `que_1_ans`) END) AS 'que_1_ans', `que_1_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Meet to security guard employee ? :- ', `que_2_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check wiring  ? :- ', `que_2_ans`) ELSE CONCAT('Meet to housekeeping employee ? :- ', `que_2_ans`) END) AS 'que_2_ans', `que_2_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check dressup & grooming? :- ', `que_3_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check dressup & grooming ? :- ', `que_3_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check electrical / electronics items? :- ', `que_3_ans`) ELSE CONCAT('Check dressup & grooming? :- ', `que_3_ans`) END) AS 'que_3_ans', `que_3_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check furniture ? :- ', `que_4_ans`) ELSE CONCAT('Check I-card & shooes ? :- ', `que_4_ans`) END) AS 'que_4_ans', `que_4_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check office cleaning  ? :- ', `que_5_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard maintain register proparly ? :- ', `que_5_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Check plumbing work? :- ', `que_5_ans`) ELSE CONCAT('Check office cleaning  ? :- ', `que_5_ans`) END) AS 'que_5_ans', `que_5_path`, (CASE WHEN `grade_name` = 'HOUSEKEEPING' THEN CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) WHEN `grade_name` = 'SECURITY GUARD' THEN CONCAT('Guard is present on place  ? :- ', `que_6_ans`) WHEN `grade_name` = 'R&M' THEN CONCAT('Is there any query or concerns ? :- ', `que_6_ans`) ELSE CONCAT('Check washroom cleaning ? :- ', `que_6_ans`) END) AS 'que_6_ans', `que_6_path`, `location`, `comment`, `remark`, (CASE `android_flag` WHEN 0 THEN 'Pending' WHEN 2 THEN 'Reject' WHEN 1 THEN 'Approved' WHEN 4 THEN 'Mailsend' WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' FROM `pay_site_audit` inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code  where  pay_site_audit.comp_code='" + Session["COMP_CODE"].ToString() + "'  order by pay_site_audit.id desc", d.con);

            cmd_item.Fill(dt_item);

            if (dt_item.Rows.Count > 0)
            {

                companyGridView.DataSource = dt_item;
                companyGridView.DataBind();
            }
            else
            {
                companyGridView.DataSource = null;
                companyGridView.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records Found');", true);
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_client.SelectedValue = "Select";
            ddl_state.SelectedValue = "Select";
            ddl_unit.SelectedValue = "Select";
            dd1_super.SelectedValue = "Select";
    
    }
    protected void service_rating_gv()
    {
        d.con.Open();
        companyGridView.DataSource = null;
        companyGridView.DataBind();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        try
        {
            DataSet ds_status = new DataSet();
            MySqlDataAdapter dt_status = new MySqlDataAdapter("select `ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' WHEN 4 THEN 'Mail Send' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "' ", d.con);
           // MySqlDataAdapter dt_status = new MySqlDataAdapter("select `ID`,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning og office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating.`emp_code` = '" + dd1_super.SelectedValue + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "' AND `pay_service_rating`.`unit_code` = '" + ddl_unit.SelectedValue + "'  ORDER BY `id` DESC", d.con);
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                companyGridView1.DataSource = ds_status;
                companyGridView1.DataBind();
            }
            ddl_client.SelectedValue = "Select";
            ddl_state.SelectedValue = "Select";
            ddl_unit.SelectedValue = "Select";
            dd1_super.SelectedValue = "Select";

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void load_service_gv()
    { 
        d.con.Open();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        try
        {
            DataSet ds_status = new DataSet();
            //MySqlDataAdapter dt_status = new MySqlDataAdapter("select `ID`,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning of office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating. comp_code='" + Session["COMP_CODE"].ToString() + "'  ORDER BY `id` DESC", d.con);
            MySqlDataAdapter dt_status = new MySqlDataAdapter("select `ID`,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_service_rating.state_name,pay_service_rating.unit_name,pay_service_rating.cur_date,pay_employee_master.emp_name, CONCAT('Washroom & Pantry Cleaning Condition ? :- ', `answer1`) AS 'que_1_ans',CONCAT('Training & Grooming including uniform ? :-', `answer2`) AS 'que_2_ans',CONCAT('Status of /cleaning/dusting of service & store room ? :-', `answer3`) AS 'que_3_ans', CONCAT('Maintain HK Staff job card and Supervisor visit ? :-', `answer4`) AS 'que_4_ans',CONCAT('Check for 5S Store Setup ? :-', `answer5`) AS 'que_5_ans', CONCAT('Deep Cleaning og office on every Saturday ? :-', `answer6`) AS 'que_6_ans',CONCAT('Reporting of office hygiene & pest Control ? :-', `answer7`) AS 'que_7_ans',CONCAT('Meeting with Client ? :-', `answer8`) AS 'que_8_ans',CONCAT('Compliance Management ? :-', `answer9`) AS 'que_9_ans',CONCAT('Dusting cleaning of workstation,windows,doors etc./Dust Bins Condition/Cleaning Material supply ? :-', `answer10`) AS 'que_10_ans',`pay_service_rating`.`remark`,(CASE `flag`   WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Reject'  WHEN 3 THEN 'Completed' ELSE '' END) AS 'Status' from pay_service_rating  inner join pay_employee_master on pay_service_rating.comp_code=pay_employee_master.comp_code and pay_service_rating.emp_code=pay_employee_master.emp_code  WHERE pay_service_rating.`emp_code` = '" + dd1_super.SelectedValue + "' AND `pay_service_rating`.`client_code` = '" + ddl_client.SelectedValue + "' AND `pay_service_rating`.`unit_code` = '" + ddl_unit.SelectedValue + "'  ORDER BY `id` DESC", d.con);
            dt_status.Fill(ds_status);
            if (ds_status.Tables[0].Rows.Count > 0)
            {
                companyGridView1.DataSource = ds_status;
                companyGridView1.DataBind();
            }
            else
            {
                companyGridView1.DataSource = null;
                companyGridView1.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records Found');", true);
            }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    }
    protected void companyGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if ((e.Row.Cells[16].Text == "Reject") || (e.Row.Cells[16].Text == "Approved"))
            {
                e.Row.Cells[17].Text = "";

            }

            if ((e.Row.Cells[16].Text == "Reject") || (e.Row.Cells[16].Text == "Pending") || (e.Row.Cells[16].Text == "Completed") || (e.Row.Cells[16].Text == "Mail Send"))
            {
                e.Row.Cells[18].Text = "";

            }
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[17].Visible = false;
    }
    protected void companyGridView1_PreRender(object sender, EventArgs e)
    {
        try
        {
            companyGridView1.UseAccessibleHeader = false;
            companyGridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void lnkbtn_complete1_Click(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            int item = (int)companyGridView1.DataKeys[gvrow.RowIndex].Value;

            int res = d.operation("update pay_service_rating set comment='Completed Document',flag='3' where  Id='" + item + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Completed successfully !!!')", true);
            service_rating_gv();
           
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void ddl_feedback_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (ddl_feedback_type.SelectedValue == "1")
            {
                site_audit_gv();
            }
            else
            {
                service_rating_gv();
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }
    }
}

