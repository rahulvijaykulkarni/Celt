using System;
using System.Data;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

public partial class Leave_form : System.Web.UI.Page
{

    DAL d = new DAL();
    DAL d1 = new DAL();
    Leave_Apply lbl3 = new Leave_Apply();

    string gender;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "R")
        {

            btn_add.Visible = false;
            //btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "U")
        {

            btn_add.Visible = false;
            // btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "C")
        {

        }


        if (!IsPostBack)
        {

            leaveTypeFill();
            loadgridview();
            btn_edit.Visible = false;
            half.Visible = false;
            ddl_halfday.Visible = false;
            btn_leave_cancel.Visible = false;
            txt_del_date.Visible = false;
            ddl_pl_cl.Visible = false;
            lbl_pl_cl.Visible = false;
           
           // leave_type();
        }

    }



    public void leaveTypeFill()
    
    {
        ddl_leave_type.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(pay_leave_master.leave_name)FROM  pay_leave_master INNER JOIN pay_policy_master ON pay_policy_master.policy_name = pay_leave_master.policy_name WHERE  pay_policy_master.policy_name = (SELECT pay_assign_leave_policy.policy_name FROM pay_assign_leave_policy WHERE pay_assign_leave_policy.emp_code = '"+Session["login_id"].ToString()+"' ) ", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_leave_type.DataSource = dt_item;
                ddl_leave_type.DataValueField = dt_item.Columns[0].ToString();
                ddl_leave_type.DataTextField = dt_item.Columns[0].ToString();
                ddl_leave_type.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.conclose();
        }


        ddl_leave_type.Items.Insert(0, new ListItem("Select", "Select"));



    
    }


    private void loadgridview()
    {
        

        Panel2.Visible = true;
        d.con1.Open();
        try
        {
            MySqlCommand cmd11 = new MySqlCommand("SELECT `LEAVE_ID`,date_format(Leave_Apply_Date,'%d/%m/%Y') as Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,(select emp_name from pay_employee_master b where b.emp_code = a.MANAGER) as MANAGER,date_format(FROM_DATE,'%d/%m/%Y') as FROM_DATE,date_format(TO_DATE,'%d/%m/%Y') as TO_DATE,NO_OF_DAYS,LEAVE_STATUS,STATUS_COMMENT,date_format(Leave_Approved_Date,'%d/%m/%Y') as Leave_Approved_Date,half_day_type, leave_id FROM pay_leave_transaction a where DATE_FORMAT(Leave_Apply_Date, '%Y')= date_format(now(),'%Y') and   EMP_CODE='" + Session["LOGIN_ID"] + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY LEAVE_ID desc", d.con1);
            DataSet ds11 = new DataSet();
            MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
            adp11.Fill(ds11);
            LeaveTypeGridView.DataSource = ds11.Tables[0];
            LeaveTypeGridView.DataBind();
            cmd11.Dispose();
            ds11.Dispose();
            adp11.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        txt_manager.ReadOnly = true;
        txt_balance_leave.ReadOnly = true;

        d.conopen();
        try
        {
            MySqlCommand cmd2 = new MySqlCommand("SELECT  date_format(now(),'%d/%m/%Y') ,reporting_to,GENDER FROM pay_employee_master a WHERE EMP_CODE ='" + Session["LOGIN_ID"] + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                txt_manager.Text = dr.GetValue(1).ToString();
                if (dr.GetValue(2).ToString() == "M")
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }


            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.conclose();
        }

    }




    protected void ddl_leave_type_TextChanged(object sender, EventArgs e)
    {
      
        d.con.Open();
        string s = "";
        MySqlCommand cmd1 = new MySqlCommand("select flag from pay_leave_transaction where leave_type='" + ddl_leave_type.SelectedValue.ToString() + "'", d.con);
        MySqlDataReader dr1 = cmd1.ExecuteReader();
        if (dr1.Read())
        {

            s = dr1.GetValue(0).ToString();
            
        }
        dr1.Close();


        if (s != "")
        {
            MySqlCommand cmd = new MySqlCommand("select max_no_of_leave,carry_forword from pay_leave_transaction where leave_type='" + ddl_leave_type.SelectedValue.ToString() + "' and `policy_name`=(select `policy_name` from pay_assign_leave_policy where emp_code='" + Session["LOGIN_ID"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "')", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_balance_leave.Text = dr.GetValue(0).ToString();
                txt_carry_forword.Text = dr.GetValue(1).ToString();
            }
            dr.Close();
        }
        else
        {
            MySqlCommand cmd = new MySqlCommand("select max_no_of_leave,carry_forward from pay_leave_master where leave_name='" + ddl_leave_type.SelectedValue.ToString() + "' and `policy_name`=(select `policy_name` from pay_assign_leave_policy where emp_code='" + Session["LOGIN_ID"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "')", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_balance_leave.Text = dr.GetValue(0).ToString();
                txt_carry_forword.Text = dr.GetValue(1).ToString();
            }
            dr.Close();
        }

    }


    TimeSpan getDateDifference(DateTime date1, DateTime date2)
    {
        TimeSpan ts = date1 - date2;
        int differenceInDays = ts.Days;
        string differenceAsString = differenceInDays.ToString();
        return ts;

    }


    protected void txt_todate_TextChanged(object sender, EventArgs e)
    {

        //DateTime date1 = DateTime.ParseExact(txt_fromdate1.Text, "d/M/yyyy", null);

        //DateTime date2 = DateTime.ParseExact(txt_todate1.Text, "d/M/yyyy", null);


        //TimeSpan difference = getDateDifference(date1, date2);

    }


    protected void txt_no_of_days_TextChanged(object sender, EventArgs e)
    {

        DateTime date1 = DateTime.ParseExact(txt_fromdate.Text, "d/M/yyyy", null);

        DateTime date2 = DateTime.ParseExact(txt_todate.Text, "d/M/yyyy", null);
        int diff = 1, diff2,diff_month;
        int d1 = date1.Day;
        int d2 = date2.Day;

        int d1_month = date1.Month;
        int d2_month = date2.Month;

        int d1_year = date1.Year;
        int d2_year = date2.Year;
        int count = 0;

        TimeSpan ff = date2 - date1;

        if (d1 > d2 && d1_month > d2_month && d1_year > d2_year)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select start date must be in proper format')", true);
            txt_no_of_days.Text = "";

        }
        else
        {
            TimeSpan days = date2.Subtract(date1);
            
            for (int a = 0; a < days.Days  + 1 && date2 > date1; a++)
            {
                if (date1.DayOfWeek != DayOfWeek.Saturday && date1.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                date1 = date1.AddDays(1.0);
            }

            diff = diff + count;
            txt_no_of_days.Text = diff.ToString();
  
        }

        int i = Convert.ToInt32(txt_balance_leave.Text);

        i = i - diff;
        txt_balance_leave.Text = i.ToString();


        int txt_carry_for = Convert.ToInt32(txt_carry_forword.Text);
        if (i < txt_carry_for)
        {
            txt_carry_forword.Text = "0";
        }

    }

    private object GetAlternatingFridaysStartingFrom(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {

        int result;
        result = d.operation("insert into pay_leave_transaction (COMP_CODE,EMP_CODE,Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,reporing_to,FROM_DATE,TO_DATE,NO_OF_DAYS,max_no_of_leave,carry_forword,EMP_NAME,manager) values ('" + Session["comp_code"].ToString() + "','" + Session["Login_Id"].ToString() + "',sysdate(),'" + ddl_leave_type.SelectedValue + "','" + txt_reason.Text + "','" + txt_manager.Text + "',str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y'),str_to_date('" + txt_todate.Text + "','%d/%m/%Y'),'" + txt_no_of_days.Text + "','" + txt_balance_leave.Text + "','" + txt_carry_forword.Text + "',(select emp_name from  pay_employee_master where emp_code='" + Session["Login_Id"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'),'"+txt_manager.Text+"')");

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Leave Apply successfully!!');", true);
              }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Leave Apply unsuccessfully!!');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            text_Clear();
            loadgridview();



        }
    }









    protected void btnclose_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");

    }


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.LeaveTypeGridView, "Select$" + e.Row.RowIndex);
        }


        e.Row.Cells[0].Visible = false;
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

    }










    



























    protected void LeaveTypeGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        d1.con1.Open();

        System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_leaveid");

        MySqlCommand cmd_hol = new MySqlCommand("SELECT month_end FROM pay_leave_transaction where LEAVE_ID = " + lbl_leaveid1.Text, d1.con1);
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

        System.Web.UI.WebControls.Label lbl_status = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_STATUS");
        if (lbl_status.Text != "Cancelled")
        {


            lbl_Leave_Approved_Date.Visible = true;
            lbl_status.Visible = true;
            lbl_Leave_Status_Comment.Visible = true;
            btn_edit.Visible = true;

            try
            {
                System.Web.UI.WebControls.Label lbl_LEAVE_TYPE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_TYPE");
                string leavetype = lbl_LEAVE_TYPE.Text;

                System.Web.UI.WebControls.Label lbl_LEAVE_REASON = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_REASON");
                string leavereason = lbl_LEAVE_REASON.Text;

                System.Web.UI.WebControls.Label lbl_MANAGER = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_MANAGER");
                string leavemanager = lbl_MANAGER.Text;

                System.Web.UI.WebControls.Label lbl_leaveid = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_leaveid");
                ViewState["leaveid"] = lbl_leaveid.Text;
                string id_leave = lbl_leaveid.Text;
               


                d.con1.Open();
                MySqlCommand cmd2 = new MySqlCommand("SELECT date_format(Leave_Apply_Date,'%d/%m/%Y') as Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,reporing_to,date_format(FROM_DATE,'%d/%m/%Y') as FROM_DATE,date_format(TO_DATE,'%d/%m/%Y') as TO_DATE,NO_OF_DAYS,Leave_Approved_Date,LEAVE_STATUS,STATUS_COMMENT,half_day_type,carry_forword FROM pay_leave_transaction where  LEAVE_ID='" + ViewState["leaveid"].ToString() + "'  ", d.con);  //AND MANAGER='" + leavemanager + "'
                d.conopen();
                MySqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {

                    ddl_leave_type.SelectedValue = dr.GetValue(1).ToString();
                    txt_reason.Text = dr.GetValue(2).ToString();

                    txt_manager.Text = dr.GetValue(3).ToString();
                    //  txt_balance_leave.Text = "0";
                    //ViewState["balanceleave"] = txt_balance_leave.Text;

                    string date = dr.GetValue(4).ToString();
                    if (date == "")
                    {
                        txt_fromdate.Text = dr.GetValue(4).ToString();
                    }
                    else
                    {
                        txt_fromdate.Text = date.ToString();
                    }


                    string date1 = dr.GetValue(5).ToString();
                    if (date1 == "")
                    {
                        txt_todate.Text = dr.GetValue(5).ToString();
                    }
                    else
                    {
                        txt_todate.Text = date1.ToString();
                    }
                    txt_no_of_days.Text = dr.GetValue(6).ToString();
                    ViewState["balanceleave"] = txt_no_of_days.Text;
                    lbl_Leave_Approved_Date.Text = dr.GetValue(7).ToString();
                    lbl_status.Text = dr.GetValue(8).ToString();
                    lbl_Leave_Status_Comment.Text = dr.GetValue(9).ToString();
                    if (ddl_leave_type.SelectedValue == "F")
                    {
                        half.Text = "Half :";
                        half.Visible = true;
                        ddl_halfday.Visible = true;
                        ddl_pl_cl.Visible = true;
                        lbl_pl_cl.Visible = true;
                        txt_del_date.Visible = false;
                        ddl_halfday.SelectedValue = dr.GetValue(10).ToString();
                    }
                    if (ddl_leave_type.SelectedValue == "ML")
                    {
                        half.Text = "Delivery Date :";
                        half.Visible = true;
                        ddl_halfday.Visible = false;
                        txt_del_date.Visible = true;
                        ddl_pl_cl.Visible = false;
                        lbl_pl_cl.Visible = false;
                        ddl_halfday.SelectedValue = dr.GetValue(10).ToString();
                    }
                    txt_carry_forword.Text = dr.GetValue(11).ToString();
                    txt_balance_leave.Text = dr.GetValue(6).ToString();

                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Successfully...!')", true);
                }
                dr.Close();
                d.conclose();
                btn_leave_cancel.Visible = true;
            }
            catch (Exception ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Failed...!')", true);
            }
            finally
            {


            }

            btn_add.Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot make changes !')", true);
        }
    }



    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        try {ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        text_Clear();
        d.reset(this);
    }

    public MySqlDataReader drmax { get; set; }

    protected void text_Clear()
    {

        // txt_manager.Text = "";
        txt_balance_leave.Text = "";
        txt_fromdate.Text = "";
        txt_todate.Text = "";
        txt_no_of_days.Text = "";
        txt_reason.Text = "";
        txt_carry_forword.Text = "";
        txt_manager.Text = "";
        ddl_leave_type.SelectedValue = "Select";

    }

    

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        int res = 0;
        try
        {
            MySqlCommand cmd2 = new MySqlCommand("select count(1) from pay_leave_transaction where (str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y') between FROM_DATE and To_date or str_to_date('" + txt_todate.Text + "','%d/%m/%Y') between FROM_DATE and To_date) and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND emp_code='" + Session["LOGIN_ID"] + "'", d.con);

            d.conopen();

            object CL = cmd2.ExecuteScalar();
            cmd2.Dispose();
            d.conclose();

            if (int.Parse(CL.ToString()) >= 0)
            {
                System.Web.UI.WebControls.Label lbl_LEAVE_TYPE = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_TYPE");
                string leavetype = lbl_LEAVE_TYPE.Text;

                System.Web.UI.WebControls.Label lbl_LEAVE_REASON = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_LEAVE_REASON");
                string leavereason = lbl_LEAVE_REASON.Text;

                System.Web.UI.WebControls.Label lbl_MANAGER = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_MANAGER");
                string leavemanager = lbl_MANAGER.Text;

            // int res1=   d.operation("delete from pay_leave_transaction where  LEAVE_ID = '" + ViewState["leaveid"].ToString()+ "'");
              res = d.operation("update pay_leave_transaction set Leave_Apply_Date= STR_TO_DATE('" + Session["system_curr_date"] + "','%d/%m/%Y'),LEAVE_TYPE= '" + ddl_leave_type.SelectedValue + "',LEAVE_REASON= '" + txt_reason.Text + "',MANAGER= '" + txt_manager.Text + "',FROM_DATE= STR_TO_DATE('" + txt_fromdate.Text + "','%d/%m/%Y'),TO_DATE= STR_TO_DATE('" + txt_todate.Text + "','%d/%m/%Y'),NO_OF_DAYS= '" + txt_no_of_days.Text + "', half_day_type = '" + ddl_halfday.SelectedValue + "'  where LEAVE_ID = " + ViewState["leaveid"].ToString());
               // d.operation("delete from pay_leave_transaction where  LEAVE_ID = '" + ViewState["leaveid"].ToString() + "'");
                //  int res1=  d.operation("insert into pay_leave_transaction (COMP_CODE,EMP_CODE,Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,reporing_to,FROM_DATE,TO_DATE,NO_OF_DAYS,max_no_of_leave,carry_forword,EMP_NAME,manager) values ('" + Session["comp_code"].ToString() + "','" + Session["Login_Id"].ToString() + "',sysdate(),'" + ddl_leave_type.SelectedValue + "','" + txt_reason.Text + "','" + txt_manager.Text + "',str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y'),str_to_date('" + txt_todate.Text + "','%d/%m/%Y'),'" + txt_no_of_days.Text + "','" + txt_balance_leave.Text + "','" + txt_carry_forword.Text + "',(select emp_name from  pay_employee_master where emp_code='" + Session["Login_Id"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'),'" + txt_manager.Text + "')");

                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);
                }
                res = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + txt_manager.Text + "','Message from " + Session["USERNAME"].ToString() + " - Leave Approval','Leave_form_management.aspx')");
           //d.operation("insert into pay_leave_transaction (COMP_CODE,EMP_CODE,Leave_Apply_Date,LEAVE_TYPE,LEAVE_REASON,reporing_to,FROM_DATE,TO_DATE,NO_OF_DAYS,max_no_of_leave,carry_forword,EMP_NAME,manager) values ('" + Session["comp_code"].ToString() + "','" + Session["Login_Id"].ToString() + "',sysdate(),'" + ddl_leave_type.SelectedValue + "','" + txt_reason.Text + "','" + txt_manager.Text + "',str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y'),str_to_date('" + txt_todate.Text + "','%d/%m/%Y'),'" + txt_no_of_days.Text + "','" + txt_balance_leave.Text + "','" + txt_carry_forword.Text + "',(select emp_name from  pay_employee_master where emp_code='" + Session["Login_Id"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "'),'" + txt_manager.Text + "')");

            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.reset(this);
            loadgridview();
        }

    }



   
    protected void btn_leave_cancel_Click(object sender, EventArgs e)
    {
        int res = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            res = d.operation("delete from pay_leave_transaction where LEAVE_ID = " + ViewState["leaveid"].ToString());
            if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deleteed Successfully...')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deleteing Failed...')", true);
                }
                res = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + txt_manager.Text + "','" + Session["USERNAME"].ToString() + " - Cancelled Leave','Leave_form_management.aspx')");
            }
        
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.reset(this);
        }

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