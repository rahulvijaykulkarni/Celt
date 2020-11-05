using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Unit_reject_reason : System.Web.UI.Page
{
    DAL d = new DAL();
    double a = 0, c = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        if (!IsPostBack)
        {
            if (!Session["Ticket_ID"].ToString().Equals(""))
            {
                load_sub_grid(Session["Ticket_ID"].ToString());
            }
            else
            {
                SearchGridView.DataSource = null;
                SearchGridView.DataBind();
            }
        }
    }
    //private void update_notification(string status, string to_email)
    //{
    //    try
    //    {
    //        d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + Session["USERNAME"].ToString() + "','0',EMP_CODE,'apply_expencess.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1");

    //        if (!to_email.Equals(""))
    //        {
    //            d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan status " + status + "  By " + Session["USERNAME"].ToString() + "','0','" + to_email + "','app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1");
    //            d.SendHtmltravelEmail(Server.MapPath("~/travel_plan.htm"), Session["EXPENSE_ID"].ToString(), 3, "", Session["USERNAME"].ToString(), to_email);
    //        }
    //        d.SendHtmltravelEmail(Server.MapPath("~/travel_plan_confirm.htm"), Session["EXPENSE_ID"].ToString(), 2, status, "", "");

    //    }
    //    //catch (Exception ex)
    //    //{
    //    //    throw ex;
    //    //}
    //    finally
    //    {
    //    }
    //}
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        try
        {
            d.approve(Session["EXPENSE_ID"].ToString(), Session["LOGIN_ID"].ToString(), Session["USERNAME"].ToString(), Server.MapPath("~/travel_plan.htm"), Server.MapPath("~/travel_plan_confirm.htm"));
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
          //  update_grid();
            btn_reject.Visible = false;
            
        }
    }

    //public void approve()
    //{
    //    DAL d_level = new DAL();
    //    int appcount = 0, count = 0;
    //    try
    //    {
    //        string querynew = "Select txt_approval_level from pay_travel_policy_master Where ID in (Select policy_id from pay_travel_emp_policy Where emp_code in (select emp_code from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"].ToString() + "')) ";
    //        MySqlCommand cmd_level = new MySqlCommand(querynew, d_level.con1);
    //        d_level.con1.Open();
    //        MySqlDataReader dr_level = cmd_level.ExecuteReader();
    //        if (dr_level.Read())
    //        {
    //            string approval_level1 = dr_level[0].ToString();
    //            if (approval_level1.Equals(null) || approval_level1.Trim().Equals("")) { approval_level1 = "1"; }
    //            appcount = Int32.Parse(approval_level1);
    //        }
    //        d_level.con1.Close();
    //        string app_level = "select Approved_level from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"].ToString() + "'";
    //        d.con1.Open();
    //        MySqlCommand app_cmd = new MySqlCommand(app_level, d.con1);

    //        MySqlDataReader app_dr = app_cmd.ExecuteReader();
    //        if (app_dr.Read())
    //        {
    //            string app_level1 = app_dr[0].ToString();
    //            if (app_level1.Equals(null) || app_level1.Trim().Equals("")) { app_level1 = "1"; }
    //            count = Int32.Parse(app_level1);
    //        }
    //        d.con1.Close();
    //        string query = "";
    //        if (Session["LOGIN_ID"] == null || Session["LOGIN_ID"].ToString() == "")
    //        {
    //            query = "select REPORTING_TO from pay_employee_master where EMP_code in (select substring(approval_emp,1,6) from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"].ToString() + "' limit 1)";
    //        }
    //        else
    //        {
    //            query = "select REPORTING_TO from pay_employee_master where EMP_code='" + Session["LOGIN_ID"].ToString() + "'";
    //        }
    //        string first_reporting_to = "";
    //        d.con1.Open();
    //        MySqlCommand cmd = new MySqlCommand(query, d.con1);
    //        MySqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            first_reporting_to = dr[0].ToString();
    //        }
    //        d.con1.Close();

    //        count++;

    //        if (count == appcount)
    //        {
    //            d.operation("Update apply_travel_plan SET expense_status = 'Approved'  Where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
    //            update_notification("Approved", first_reporting_to);
    //        }
    //        else
    //        {
    //            d.operation("Update apply_travel_plan SET expense_status = 'Pending', Approved_level='" + count + "', approval_emp = replace(approval_emp,'" + Session["LOGIN_ID"].ToString() + "',''), approved_emp=concat(ifnull(approved_emp,''),'" + Session["LOGIN_ID"].ToString() + "'),modified_by=(select emp_name from pay_employee_master where emp_code ='" + Session["LOGIN_ID"].ToString() + "') Where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
    //            int approve_result1 = d.operation("Update apply_travel_plan SET expense_status = 'Approved'  Where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' and approval_emp = ''");
    //            update_notification("Approved", first_reporting_to);
    //        }

    //    }
    //    catch (Exception ee)
    //    {
            
    //    }
    //    finally
    //    {
    //        d_level.con1.Close();
    //        d.con1.Close();
    //    }
    //}

    public void update_grid()
    {

        //System.Data.DataTable dt_gv = new System.Data.DataTable();
        //d.con.Open();
        ////MySqlCommand cmd_gv = new MySqlCommand("SELECT EP_NO,expense_title,case When expense_status = 1 then 'Pending' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = submitter_code AND COMP_CODE ='"+Session["COMP_CODE"].ToString()+"') AS submitter,DATE_FORMAT(submitted_on,'%d/%m/%Y') As submitted_on,expense_amount FROM expense_approval WHERE (COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND approver = '"+Session["LOGIN_ID"].ToString()+"') ORDER BY EP_NO desc", d.con);
        ////MySqlCommand cmd_gv = new MySqlCommand(" Select Id, expenses_id ,exception_case,travel_mode ,from_designation,to_designation,DATE_FORMAT(from_date,'%d/%m/%Y') As from_date,DATE_FORMAT(to_date,'%d/%m/%Y') As to_date ,modified_by,Comments,adv_amount,curreny_id, case expense_status When 0 then 'In Progress' When 1 then 'Submitted' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = apply_travel_plan.Emp_code AND COMP_CODE ='C01' ) AS submitter  from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"] + "' group BY expenses_id Order by expenses_id desc", d.con);
        //MySqlCommand cmd_gv = new MySqlCommand("Select Id, expenses_id ,exception_case,travel_mode ,from_designation,to_designation,DATE_FORMAT(from_date,'%d/%m/%Y') As from_date,DATE_FORMAT(to_date,'%d/%m/%Y') As to_date ,modified_by,Comments,adv_amount,curreny_id, expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = apply_travel_plan.Emp_code AND COMP_CODE ='C01' ) AS submitter  from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"] + "' ", d.con);
        //MySqlDataAdapter dr_gv = new MySqlDataAdapter(cmd_gv);
        //dr_gv.Fill(dt_gv);

        //gv_paymentdetails.DataSource = dt_gv;
        //gv_paymentdetails.DataBind();
        //d.con.Close();
    }



    private void load_sub_grid(string request_Id_edit)
    {
        try
        {
            d.con.Open();
            DataSet ds = new DataSet();
            // MySqlDataAdapter dr = new MySqlDataAdapter("select Id,services,pay_service_master.location,priority,pay_service_master.status,additional_comment,EMP_NAME as forword_to,documents from  pay_service_master inner join pay_employee_master on pay_service_master.comp_code=pay_employee_master.comp_code and  pay_service_master.forword_to=pay_employee_master.emp_code   where pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' order by Id  ", d.con);
            MySqlDataAdapter dr = new MySqlDataAdapter("SELECT pay_service_master.`Id`, `services`, `priority`, `pay_service_master`.`status`, `additional_comment`, `date`, concat('~/Images/',file_name) as Value FROM `pay_service_master` INNER JOIN `pay_service_documents` ON `pay_service_master`.`comp_code` = `pay_service_documents`.`comp_code` AND `pay_service_master`.`id` = `pay_service_documents`.`id` WHERE pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' and pay_service_master.id = '" + request_Id_edit + "' order by pay_service_master.Id desc", d.con);
            dr.Fill(ds);
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            d.con.Close();
            SearchGridView.Visible = true;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();

        }
    }


    protected void btn_reject_Click(object sender, EventArgs e)
    {
        //int total_approve = 0;
        //int total_checked = 0;
        string status = "";
        commenrid.Visible = true;
        txt_comments.Visible = true;

        try
        {
            d.con.Open();
            MySqlCommand cmd_status = new MySqlCommand("Select status From pay_service_master where id = '" + Session["Ticket_ID"].ToString() + "'", d.con);
            MySqlDataReader dr_status = cmd_status.ExecuteReader();
            if (dr_status.Read())
            {
                if (dr_status.GetValue(0).ToString().Contains( "Quotation Generate"))
                {
                    status = "Quotation Rejected";
                }
                else if (dr_status.GetValue(0).ToString().Contains("Bill Generated"))
                {
                    status = "Quotation Rejected";
                }
                else if (dr_status.GetValue(0).ToString().Contains("Quotation Approved"))
                {
                    status = "Quotation Rejected";
                }
                else { status = dr_status.GetValue(0).ToString(); }
            }
            dr_status.Dispose();
            d.con.Close();
            d.operation("Update pay_service_master set status = '" + status + " - " + txt_comments.Text + "' where id = '" + Session["Ticket_ID"].ToString() + "'");
            d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Your Quotation Is Rejected By - " + Session["unit_code"].ToString() + "','0',`unit_code`,'IHMS_services.aspx' from pay_service_master a where id = '" + Session["Ticket_ID"].ToString() + "' limit 1");
       
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            update_grid();
            btn_reject.Visible = false;
            txt_comments.Text = "";
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

        //e.Row.Cells[1].Visible = false;
        //e.Row.Cells[2].Visible = false;
    }
}