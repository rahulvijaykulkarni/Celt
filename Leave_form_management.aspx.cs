using System.Web.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;


public partial class Leave_form_management : System.Web.UI.Page
{

    DAL d = new DAL();
    DAL d1 = new DAL();
    Leave_Apply lbl3 = new Leave_Apply();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Leave Request", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Request", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_edit.Visible = false;

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Request", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Request", Session["COMP_CODE"].ToString()) == "C")
        {
        }






        //d.con1.Open();
        //MySqlCommand cmd11 = new MySqlCommand("SELECT EMP_CODE,EMP_NAME,date_format(Leave_Apply_Date,'%d/%m/%Y') as Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,date_format(FROM_DATE,'%d/%m/%Y') as FROM_DATE,date_format(TO_DATE,'%d/%m/%Y') as TO_DATE,NO_OF_DAYS,LEAVE_STATUS,STATUS_COMMENT,Leave_Approved_Date FROM pay_leave_transaction where MANAGER='" + Session["LOGIN_ID"] + "' ORDER BY  LEAVE_ID Desc", d.con1);
        //DataSet ds11 = new DataSet();
        //MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
        //adp11.Fill(ds11);
        //LeaveTypeGridView.DataSource = ds11.Tables[0];
        //LeaveTypeGridView.DataBind();
        //d.con1.Close();
        //cmd11.Dispose();
        //ds11.Dispose();
        //adp11.Dispose();

        loadgrid();

        txt_eecode.ReadOnly = true;
        txt_employee_name.ReadOnly = true;
        txt_leave_apply_date.ReadOnly = true;
        ddl_leave_type.ReadOnly = true;
        txt_reason.ReadOnly = true;
        //  txt_balance_leave.ReadOnly = true;    
        txt_fromdate.ReadOnly = true;
        txt_todate.ReadOnly = true;
        txt_no_of_days.ReadOnly = true;
        btn_edit.Visible = false;
    }


    protected void btnclose_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");

    }

    protected void LeaveTypeGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        d1.con1.Open();
        System.Web.UI.WebControls.Label lbl_LEAVE_TYPE1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_Leave_ID");
        string leaveid1 = lbl_LEAVE_TYPE1.Text;

        MySqlCommand cmd_hol = new MySqlCommand("SELECT month_end FROM pay_leave_transaction where LEAVE_ID = " + lbl_LEAVE_TYPE1.Text, d1.con1);
        MySqlDataReader dr_hol = cmd_hol.ExecuteReader();
        while (dr_hol.Read())
        {
            if (dr_hol.GetValue(0).ToString() == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Month End Process completed, You cannot make changes !!')", true);
                d1.con1.Close();
                return;
            }

        }
        dr_hol.Dispose();
        cmd_hol.Dispose();
        d1.con1.Close();


        try
        {

            //System.Web.UI.WebControls.Label lbl_EMP_CODE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_CODE");
            //string empcode = lbl_EMP_CODE.Text;

            //System.Web.UI.WebControls.Label lbl_EMP_NAME = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_NAME");
            //string empname = lbl_EMP_NAME.Text;

            System.Web.UI.WebControls.Label lbl_LEAVE_TYPE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_Leave_ID");
            string leaveid = lbl_LEAVE_TYPE.Text;


            MySqlCommand cmd2 = new MySqlCommand("SELECT EMP_CODE,EMP_NAME,date_format(Leave_Apply_Date,'%d/%m/%Y') as Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,date_format(FROM_DATE,'%d/%m/%Y') as FROM_DATE,date_format(TO_DATE,'%d/%m/%Y') as TO_DATE,NO_OF_DAYS,LEAVE_STATUS,STATUS_COMMENT,Leave_id FROM   pay_leave_transaction WHERE comp_code='" + Session["comp_code"].ToString() + "' and LEAVE_ID = " + int.Parse(leaveid), d.con);
            d.conopen();
            MySqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                txt_eecode.Text = dr.GetValue(0).ToString();
                txt_employee_name.Text = dr.GetValue(1).ToString();

                string leave_apply_date = dr.GetValue(2).ToString();
                if (leave_apply_date == "")
                {
                    txt_leave_apply_date.Text = dr.GetValue(2).ToString();
                }
                else
                {
                    txt_leave_apply_date.Text = leave_apply_date.ToString();
                }

                ddl_leave_type.Text = dr.GetValue(3).ToString();
                
                txt_reason.Text = dr.GetValue(4).ToString();
                //txt_balance_leave.Text = dr.GetValue(5).ToString();

                string date = dr.GetValue(5).ToString();
                if (date == "")
                {
                    txt_fromdate.Text = dr.GetValue(5).ToString();
                }
                else
                {
                    txt_fromdate.Text = date.ToString();
                }


                string date1 = dr.GetValue(6).ToString();
                if (date1 == "")
                {
                    txt_todate.Text = dr.GetValue(6).ToString();
                }
                else
                {
                    txt_todate.Text = date1.ToString();
                }
                txt_no_of_days.Text = dr.GetValue(7).ToString();
                txt_Status.SelectedValue = dr.GetValue(8).ToString();
                ViewState["leavestatus"] = dr.GetValue(8).ToString();
                txt_status_comment.Text = dr.GetValue(9).ToString();
                ViewState["balanceleave"] = dr.GetValue(10).ToString();
                btn_edit.Visible = true;
            }
            dr.Close();
            d.conclose();
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Failed...!')", true);
        }
        finally
        {


        }

    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        int res = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            System.Web.UI.WebControls.Label lbl_EMP_CODE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_CODE");
            string empcode = lbl_EMP_CODE.Text;

            System.Web.UI.WebControls.Label lbl_EMP_NAME = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_NAME");
            string empname = lbl_EMP_NAME.Text;

            System.Web.UI.WebControls.Label lbl_LEAVE_TYPE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_TYPE");
            string leavetype = lbl_LEAVE_TYPE.Text;
            
            int index = leavetype.IndexOf("-");
            string leave_abbr = leavetype.Substring(index + 1);

            res = d.operation("update pay_leave_transaction set LEAVE_STATUS ='" + txt_Status.SelectedValue + "',STATUS_COMMENT='" + txt_status_comment.Text + "',Leave_Approved_Date=STR_TO_DATE('" + Session["system_curr_date"] + "','%d/%m/%Y') where LEAVE_ID= " + int.Parse(ViewState["balanceleave"].ToString()));

            if (ddl_leave_type.Text != "")
            {
                if (ViewState["leavestatus"].ToString().Equals("Rejected"))
                {
                    if (txt_Status.SelectedValue.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set balance_leave = balance_leave - " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' and  abbreviation ='" + leave_abbr + "' AND comp_code='" + Session["comp_code"].ToString() + "' ");
                    }
                
                }
                else if (txt_Status.SelectedValue.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set balance_leave = balance_leave + " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' and  abbreviation ='" + leave_abbr + "' AND comp_code='" + Session["comp_code"].ToString() + "'");
                }
            }
            else if (ddl_leave_type.Text == "PL")
            {
                if (ViewState["leavestatus"].ToString().Equals("Rejected"))
                {
                    if (txt_Status.SelectedValue.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set PL = PL - " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' AND comp_code='" + Session["comp_code"].ToString() + "'");
                    }

                }
                else if (txt_Status.SelectedValue.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' AND comp_code='" + Session["comp_code"].ToString() + "'");
                }
            }
            else if (ddl_leave_type.Text == "HD")
            {
                if (ViewState["leavestatus"].ToString().Equals("Rejected"))
                {
                    if (txt_Status.SelectedValue.Equals("Approved"))
                    {
                        d.operation("update pay_leave_emp_balance set HD = HD - " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' AND comp_code='" + Session["comp_code"].ToString() + "'");
                    }

                }
                else if (txt_Status.SelectedValue.Equals("Rejected"))
                {
                    d.operation("update pay_leave_emp_balance set HD = HD + " + int.Parse(txt_no_of_days.Text) + " where emp_code = '" + txt_eecode.Text + "' AND comp_code='" + Session["comp_code"].ToString() + "'");
                }
            }

            if (res > 0)
            {
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);
            }
            res = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + txt_eecode.Text + "','Message from " + Session["USERNAME"].ToString() + " -Leave- " + txt_Status.SelectedValue + "','Leave_form.aspx')");

            loadgrid();
        }
        catch (Exception ee)
        {

        }
        finally
        {
            d.reset(this);
        }



    }

    //protected void btn_delete_Click(object sender, EventArgs e)
    //{
    //    System.Web.UI.WebControls.Label lbl_EMP_CODE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_CODE");
    //    string empcode = lbl_EMP_CODE.Text;

    //    System.Web.UI.WebControls.Label lbl_EMP_NAME = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_EMP_NAME");
    //    string empname = lbl_EMP_NAME.Text;

    //    System.Web.UI.WebControls.Label lbl_LEAVE_TYPE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_TYPE");
    //    string leavetype = lbl_LEAVE_TYPE.Text;

    //    int result = 0;
    //    MySqlCommand cmd_1 = new MySqlCommand("Select LEAVE_ID from pay_leave_transaction  WHERE  comp_code='" + Session["comp_code"] + "' AND EMP_CODE='" + empcode + "' AND  EMP_NAME='" + empname + "' AND LEAVE_TYPE='" + leavetype + "' ", d.con1);
    //    d.con1.Close();
    //    d.con1.Open();

    //    try
    //    {
    //        result = d.operation("DELETE FROM pay_leave_transaction   WHERE  comp_code='" + Session["comp_code"] + "' AND EMP_CODE='" + empcode + "' AND  EMP_NAME='" + empname + "' AND LEAVE_TYPE='" + leavetype + "' ");//delete command

    //        if (result > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!!');", true);
    //            text_Clear();
    //            d.reset(this);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed!!!');", true);
    //            text_Clear();
    //        }

    //    }

    //    catch (Exception ee)
    //    {

    //    }
    //    finally
    //    {

    //    }
    //}

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        text_Clear();
        d.reset(this);

    }

    public MySqlDataReader drmax { get; set; }

    protected void text_Clear()
    {
        txt_employee_name.Text = "";
        //txt_balance_leave.Text = "";
        txt_fromdate.Text = "";
        txt_todate.Text = "";
        txt_no_of_days.Text = "";
        txt_reason.Text = "";

        txt_Status.SelectedValue = "In Progress";
        txt_status_comment.Text = "";

    }

    protected void LeaveTypeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LeaveTypeGridView, "Select$" + e.Row.RowIndex);
        }
    }
    private void loadgrid()
    {
        LeaveTypeGridView.DataSource = null;
        LeaveTypeGridView.DataBind();
        Panel2.Visible = true;
        DataSet ds = new DataSet();
        ds = d.getData("SELECT EMP_CODE,EMP_NAME,date_format(Leave_Apply_Date,'%d/%m/%Y') as Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,date_format(FROM_DATE,'%d/%m/%Y') as FROM_DATE,date_format(TO_DATE,'%d/%m/%Y') as TO_DATE,NO_OF_DAYS,LEAVE_STATUS,STATUS_COMMENT,date_format(Leave_Approved_Date,'%d/%m/%Y') as Leave_Approved_Date,leave_id FROM pay_leave_transaction where `reporing_to` = '" + Session["LOGIN_ID"].ToString() + "' and  comp_code='" + Session["comp_code"].ToString() + "' order by leave_id desc");
        LeaveTypeGridView.DataSource = ds.Tables[0];
        LeaveTypeGridView.DataBind();
    }


    protected void LeaveTypeGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            LeaveTypeGridView.UseAccessibleHeader = false;
            LeaveTypeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}