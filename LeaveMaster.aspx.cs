using System;
using System.Data;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Web.UI.WebControls;


public partial class LeaveMaster : System.Web.UI.Page
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

            // btn_add.Visible = false;
            //btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "U")
        {

            //  btn_add.Visible = false;
            // btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Leave Apply", Session["COMP_CODE"].ToString()) == "C")
        {

        }

        LeaveTypeGridView_details();

        if (!IsPostBack)
        {
            // loadgridview();
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            //  btn_add.Visible = false;


        }



    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_itemslist.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("holiday");
        dt.Columns.Add("holiday_name");
        dt.Columns.Add("date");
        dt.Columns.Add("HOLIDAY_STATE");

        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                Label lbl_holi_name = (Label)gv_itemslist.Rows[rownum].Cells[1].FindControl("lbl_holi_name");
                dr["holiday"] = lbl_holi_name.Text.ToString();
                Label lbl_item_code = (Label)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_state_holi");
                dr["holiday_name"] = lbl_item_code.Text.ToString();
                Label lbl_particular = (Label)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_state_date");
                dr["date"] = lbl_particular.Text.ToString();
                Label lbl_state = (Label)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_state");
                dr["HOLIDAY_STATE"] = lbl_state.Text.ToString();



                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["holiday"] = ddl_stateholi.SelectedItem.ToString();
        dr["holiday_name"] = txt_stateholi_name.Text;
        dr["date"] = txt_date.Text;
        dr["HOLIDAY_STATE"] = dd_state.SelectedItem.ToString();

        //dr["abb"] = txt_abb.Text;

        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();

        ViewState["CurrentTable"] = dt;
        dd_state.SelectedValue="Select";
        ddl_stateholi.SelectedValue = "0";
        txt_stateholi_name.Text = "";
        txt_date.Text = "";
        // txt_abb.Text = "";
    }

    protected void lnkbtn_removeitem_Click_remove(object sender, EventArgs e)
    {
          try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
      catch { }
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



    }

    protected void lnkbtn_removeitem_Click1(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable1"] = dt;
            LeaveTypeGridView.DataSource = dt;
            LeaveTypeGridView.DataBind();
            //  discount_calculate(dt, 1);
            //gst_calculate(dt);
        }


    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = true;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }

                if (ddl_stateholi.SelectedValue == "2")
                {
                    gv_itemslist.HeaderRow.Cells[4].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
            }
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = true;
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

    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            MySqlCommand menu = new MySqlCommand("select max(ID)+1 from pay_leave_master where comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con1);
            d.con1.Open();
            MySqlDataReader dr = menu.ExecuteReader();
            while (dr.Read())
            {
                //Session["LEAVE_MASTER_ID"] = dr.GetValue(0).ToString();
                id = int.Parse(dr.GetValue(0).ToString());

            }
            menu.Dispose();
            dr.Close();
            dr.Dispose();

            if (id > 0)
            {
                int res = 0;
                // res = d.operation("INSERT INTO pay_leave_master(COMP_CODE,leave_name,abbreviation,gender,max_no_of_leave,carry_forward,start_date,end_date ) VALUES( '" + Session["COMP_CODE"].ToString() + "','" + txt_leave_name.Text + "','" + txt_abbreviation.Text + "','" + ddl_gender.SelectedValue + "','" + txt_max_no_of_leave.Text + "','" + txt_carry_forward.Text + "','" + txt_fromdate.Text + "','" + txt_todate.Text + "')");
                res = d.operation("update pay_leave_master set COMP_CODE='" + Session["COMP_CODE"].ToString() + "',leave_name='" + txt_leave_name.SelectedValue + "',abbreviation='" + txt_abbreviation.Text + "',gender='" + ddl_gender.SelectedValue + "',max_no_of_leave='" + txt_max_no_of_leave.Text + "',carry_forward='" + txt_carry_forward.Text + "',start_date='" + txt_fromdate.Text + "',end_date='" + txt_todate.Text + "' , submit='1',policy_name='" + txt_policy_name.Text + "',Leave_Accural='" + txt_leave_accural.Text + "',Advance_request='" + ddl_adv_req.SelectedItem + "',Backdated_request='" + ddl_bck_date.SelectedItem + "' where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and id='" + lbl_id.Text + "'");
                if (res > 0)
                {
                    int result = 0;
                    if (ddl_gender.SelectedValue.ToString() == "B")
                    {
                        result = d.operation("INSERT INTO pay_leave_emp_balance(COMP_CODE,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) SELECT '" + Session["COMP_CODE"].ToString() + "',pay_leave_emp_balance.unit_code, pay_leave_emp_balance.EMP_CODE , NOW(), '" + Session["LOGIN_ID"].ToString() + "', NOW(), pay_leave_master.leave_name, pay_leave_master.abbreviation, pay_leave_emp_balance.gender, pay_leave_master.max_no_of_leave, pay_leave_master.max_no_of_leave FROM pay_leave_master ,pay_leave_emp_balance, pay_employee_master WHERE pay_leave_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_leave_master.abbreviation ='" + txt_abbreviation.Text + "' AND pay_leave_emp_balance.gender IN('B') group by EMP_CODE ");

                    }
                    else
                    {
                        result = d.operation("INSERT INTO pay_leave_emp_balance(COMP_CODE,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) SELECT '" + Session["COMP_CODE"].ToString() + "',pay_leave_emp_balance.unit_code, pay_leave_emp_balance.EMP_CODE , NOW(), '" + Session["LOGIN_ID"].ToString() + "', NOW(), pay_leave_master.leave_name, pay_leave_master.abbreviation, pay_leave_emp_balance.gender, pay_leave_master.max_no_of_leave, pay_leave_master.max_no_of_leave FROM pay_leave_master ,pay_leave_emp_balance, pay_employee_master WHERE pay_leave_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_leave_master.abbreviation ='" + txt_abbreviation.Text + "' AND pay_leave_emp_balance.gender IN('" + ddl_gender.SelectedValue.ToString() + "') group by EMP_CODE ");

                    }
                    // result = d.operation("INSERT INTO pay_leave_emp_balance(COMP_CODE,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) select '" + Session["COMP_CODE"].ToString() + "', '" + ddl_unitcode.SelectedValue.ToString().Substring(0, 4) + "','" + txt_eecode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "',now(),leave_name,abbreviation,gender,max_no_of_leave,max_no_of_leave from pay_leave_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and gender in ('" + ddl_gender.SelectedValue + "','B') ");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Submited successfully !!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Submited failed!!');", true);
                }
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            text_Clear();
            LeaveTypeGridView_details();

        }
    }

    protected void btn_save_as_draft(object sender, EventArgs e)
    {

        try
        {
            if (txt_leave_accural.Text == "") { txt_leave_accural.Text = "0"; }
            if (txt_carry_forward.Text == "0") { txt_carry_forward.Text = "0"; }
            string id = lbl_id.Text;
            int flag1 = 0;
            int flag2 = 0;



            int res = 0;



            res = d.operation("INSERT INTO pay_policy_master(COMP_CODE,policy_name,start_date,end_date) VALUES( '" + Session["COMP_CODE"].ToString() + "','" + txt_policy_name.Text + "',str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y'),str_to_date('" + txt_todate.Text + "','%d/%m/%Y'))");



            if (GridView1.Rows.Count >= 1 || gv_itemslist.Rows.Count >= 1)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lbl_Leave_Name = (Label)row.FindControl("lbl_Leave_Name");
                    string Leave_Name = (lbl_Leave_Name.Text);

                    Label lbl_abb = (Label)row.FindControl("lbl_Abb");
                    string abb = (lbl_abb.Text);

                    Label lbl_Gender = (Label)row.FindControl("lbl_Gender");
                    string gender = (lbl_Gender.Text);


                    Label lbl_max_no_leave = (Label)row.FindControl("lbl_max_no_leave");
                    string max_no_leave = (lbl_max_no_leave.Text);


                    Label lbl_carry_forward = (Label)row.FindControl("lbl_carry_forward");
                    string carry_forward = (lbl_carry_forward.Text);


                    Label lbl_leave_accural = (Label)row.FindControl("lbl_leave_accural");
                    string leave_accural = (lbl_leave_accural.Text);

                    Label lbl_Advance_request = (Label)row.FindControl("lbl_Advance_request");
                    string Advance_request = (lbl_Advance_request.Text);

                    Label lbl_Backdated_request = (Label)row.FindControl("lbl_Backdated_request");
                    string Backdated_request = (lbl_Backdated_request.Text);




                    res = d.operation("INSERT INTO pay_leave_master(COMP_CODE,leave_name,abbreviation,gender,max_no_of_leave,carry_forward,submit,policy_name,Leave_Accural,Advance_request,Backdated_request) VALUES( '" + Session["COMP_CODE"].ToString() + "','" + Leave_Name + "','" + abb + "','" + gender + "','" + max_no_leave + "','" + carry_forward + "','0','" + txt_policy_name.Text + "','" + leave_accural + "','" + Advance_request + "','" + Backdated_request + "')");

                    if (res > 0)
                        flag1 = 1;
                }



                foreach (GridViewRow row1 in gv_itemslist.Rows)
                {
                    Label lbl_holi_name = (Label)row1.FindControl("lbl_holi_name");
                    string holi_name = (lbl_holi_name.Text);

                    Label lbl_state_holi = (Label)row1.FindControl("lbl_state_holi");
                    string state_holi = (lbl_state_holi.Text);

                    Label lbl_state_date = (Label)row1.FindControl("lbl_state_date");
                    string state_date = (lbl_state_date.Text);

                    Label lbl_state1 = (Label)row1.FindControl("lbl_state");
                    string state = (lbl_state1.Text);



                    res = d.operation("INSERT INTO pay_holiday_master(policy_name,holiday,holiday_name,date,HOLIDAY_STATE) VALUES( '" + txt_policy_name.Text + "','" + holi_name + "','" + state_holi + "','" + state_date + "','" + state + "'  )");
                    if (res > 0)
                        flag2 = 1;
                }
            }
            else
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add aleast one leave or holiday in policy!!');", true); }
            if (flag1 == 1 || flag2 == 1)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added save as draft successfully!!');", true);
                GridView1.Visible = false;
                gv_itemslist.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding save as draft failed!!');", true);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }


        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            text_Clear();
            LeaveTypeGridView_details();

        }


    }

    protected void btnclose_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        text_Clear();
        GridView1.Visible = false;
        gv_itemslist.Visible = false;
        LeaveTypeGridView_details();
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

    }

    public MySqlDataReader drmax { get; set; }

    protected void text_Clear()
    {
        txt_policy_name.Text = "";
        txt_leave_name.SelectedIndex = 0;
        txt_abbreviation.Text = "";
        ddl_gender.SelectedValue = "select";
        txt_max_no_of_leave.Text = "";
        txt_carry_forward.Text = "";
        txt_fromdate.Text = "";
        txt_todate.Text = "";


    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (rowID < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
        }

    }

    public void LeaveTypeGridView_details()
    {

        MySqlCommand cmd11 = new MySqlCommand("SELECT comp_code,policy_name,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date FROM pay_policy_master where comp_code='" + Session["COMP_CODE"].ToString() + "' order by id desc", d.con1);
        DataSet ds11 = new DataSet();
        MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
        adp11.Fill(ds11);
        LeaveTypeGridView.DataSource = ds11.Tables[0];
        LeaveTypeGridView.DataBind();
        cmd11.Dispose();
        ds11.Dispose();
        adp11.Dispose();
    }


    public void leavedetails()
    {

        System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_policy_name");
        MySqlCommand cmd11 = new MySqlCommand("SELECT leave_name,abbreviation,gender,max_no_of_leave,carry_forward,Leave_Accural,Advance_request,Backdated_request FROM pay_leave_master where comp_code='" + Session["COMP_CODE"].ToString() + "' AND POLICY_NAME='" + lbl_leaveid1.Text + "'", d.con1);
        DataSet ds11 = new DataSet();
        MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
        adp11.Fill(ds11);
        GridView1.DataSource = ds11.Tables[0];
        GridView1.DataBind();

        try
        {
            d.con.Open();
            MySqlCommand cmd12 = new MySqlCommand("SELECT leave_name,abbreviation,gender,max_no_of_leave,carry_forward,Leave_Accural,Advance_request,Backdated_request FROM pay_leave_master where comp_code='" + Session["COMP_CODE"].ToString() + "' AND POLICY_NAME='" + lbl_leaveid1.Text + "'", d.con);

            MySqlDataReader dr2 = cmd12.ExecuteReader();
            if (dr2.HasRows)
            {
                DataTable dt1 = new DataTable();
                dt1.Load(dr2);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt1;
                }
                GridView1.DataSource = dt1;
                GridView1.DataBind();

            }
            dr2.Close();

        }
        catch (Exception ex) { throw ex; }

        d.con.Close();
        //d1.con1.close();


        cmd11.Dispose();
        ds11.Dispose();
        adp11.Dispose();
    }

    public void holidaydetails()
    {
        System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_policy_name");
        MySqlCommand cmd11 = new MySqlCommand("SELECT holiday,holiday_name,date_format(date,'%d/%m/%Y') as date,HOLIDAY_STATE FROM pay_holiday_master where POLICY_NAME='" + lbl_leaveid1.Text + "'", d.con1);
        DataSet ds11 = new DataSet();
        MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
        adp11.Fill(ds11);
        gv_itemslist.DataSource = ds11.Tables[0];
        gv_itemslist.DataBind();


        try
        {
            d.con.Open();
            MySqlCommand cmd12 = new MySqlCommand("SELECT holiday,holiday_name,date_format(date,'%d/%m/%Y') as date,HOLIDAY_STATE FROM pay_holiday_master where POLICY_NAME='" + lbl_leaveid1.Text + "'", d.con);

            MySqlDataReader dr2 = cmd12.ExecuteReader();
            if (dr2.HasRows)
            {
                DataTable dt1 = new DataTable();
                dt1.Load(dr2);
                if (dt1.Rows.Count > 0)
                {
                    ViewState["CurrentTable1"] = dt1;
                }
                gv_itemslist.DataSource = dt1;
                gv_itemslist.DataBind();

            }
            dr2.Close();

        }
        catch (Exception ex) { throw ex; }

        d.con.Close();


        cmd11.Dispose();
        ds11.Dispose();
        adp11.Dispose();
    }
    protected void LeaveTypeGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        d1.con1.Open();

        txt_policy_name.ReadOnly = true;


        System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_policy_name");
        MySqlCommand cmd_hol = new MySqlCommand("SELECT policy_name,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date FROM pay_policy_master where  comp_code='" + Session["COMP_CODE"].ToString() + "' and policy_name='" + lbl_leaveid1.Text + "'", d1.con1);
        MySqlDataReader dr_hol = cmd_hol.ExecuteReader();
        while (dr_hol.Read())
        {
            txt_policy_name.Text = dr_hol.GetValue(0).ToString();
            txt_fromdate.Text = dr_hol.GetValue(1).ToString();
            txt_todate.Text = dr_hol.GetValue(2).ToString();

        }

        leavedetails();
        holidaydetails();

        dr_hol.Dispose();
        cmd_hol.Dispose();
        d1.con1.Close();
        btn_save_draft.Visible = false;
        //btn_add.Visible = false;
        btn_edit.Visible = true;
        btn_delete.Visible = true;

        GridView1.Visible = true;
        gv_itemslist.Visible = true;
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        int res = 0;
        int res2 = 0;
        int flag = 0;
        int flag2 = 0;
        txt_leave_accural.Text = "0";
        txt_carry_forward.Text = "0";
        try
        {

            txt_policy_name.ReadOnly = true;

            d.operation("DELETE FROM pay_leave_master WHERE policy_name='" + txt_policy_name.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");

            d.operation("DELETE FROM pay_holiday_master WHERE policy_name='" + txt_policy_name.Text + "' ");

            if (GridView1.Rows.Count >= 1 || gv_itemslist.Rows.Count >= 1)
            {


                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lbl_leave_name = (Label)row.FindControl("lbl_Leave_Name");
                    string leave_name = (lbl_leave_name.Text);

                    Label lbl_abbreviation = (Label)row.FindControl("lbl_Abb");
                    string abbreviation = (lbl_abbreviation.Text);

                    Label lbl_gender = (Label)row.FindControl("lbl_Gender");
                    string gender = (lbl_gender.Text);

                    Label lbl_max_no_of_leave = (Label)row.FindControl("lbl_max_no_leave");
                    string max_no_leave = (lbl_max_no_of_leave.Text);

                    Label lbl_carry_forward = (Label)row.FindControl("lbl_carry_forward");
                    string carry_forward = (lbl_carry_forward.Text);

                    Label lbl_Leave_Accural = (Label)row.FindControl("lbl_leave_accural");
                    string Leave_Accural = (lbl_Leave_Accural.Text);

                    Label lbl_Advance_request = (Label)row.FindControl("lbl_Advance_request");
                    string Advance_request = (lbl_Advance_request.Text);

                    Label lbl_Backdated_request = (Label)row.FindControl("lbl_Backdated_request");
                    string Backdated_request = (lbl_Backdated_request.Text);

                    res = d.operation("INSERT INTO pay_leave_master(COMP_CODE,leave_name,abbreviation,gender,max_no_of_leave,carry_forward,submit,policy_name,Leave_Accural,Advance_request,Backdated_request) VALUES( '" + Session["COMP_CODE"].ToString() + "','" + leave_name + "','" + abbreviation + "','" + gender + "','" + max_no_leave + "','" + carry_forward + "','0','" + txt_policy_name.Text + "','" + Leave_Accural + "','" + Advance_request + "','" + Backdated_request + "')");
                    if (res > 0)
                        flag = 1;

                }

                foreach (GridViewRow row in gv_itemslist.Rows)
                {
                    Label lbl_holi_name = (Label)row.FindControl("lbl_holi_name");
                    string holi_name = (lbl_holi_name.Text);

                    Label lbl_state_holi = (Label)row.FindControl("lbl_state_holi");
                    string state_holi = (lbl_state_holi.Text);

                    Label lbl_state_date = (Label)row.FindControl("lbl_state_date");
                    string state_date = (lbl_state_date.Text);

                    Label lbl_state = (Label)row.FindControl("lbl_state");
                    string state = (lbl_state.Text);


                    res2 = d.operation("INSERT INTO pay_holiday_master(policy_name,holiday,holiday_name,date,HOLIDAY_STATE) VALUES( '" + txt_policy_name.Text + "','" + holi_name + "','" + state_holi + "','" + state_date + "' ,'" + state + "' )");

                    if (res2 > 0)
                        flag2 = 1;

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Add Atleast one leave or holiday...')", true);
            }


            if (flag == 1 || flag2 == 1)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);
                GridView1.Visible = false;
                gv_itemslist.Visible = false;
                txt_policy_name.ReadOnly = false;
                text_Clear();
                btn_edit.Visible = true;


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);
                GridView1.Visible = false;
                gv_itemslist.Visible = false;
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record Updated Successfully... !!!')", true);
            LeaveTypeGridView_details();


        }

    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_policy_name");
            //  pay_holiday_master 
            int res = d.operation("DELETE FROM pay_POLICY_master WHERE `policy_name`='" + txt_policy_name.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
            d.operation("DELETE FROM pay_leave_master WHERE `policy_name`='" + txt_policy_name.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
            d.operation("DELETE FROM pay_holiday_master WHERE `policy_name`='" + txt_policy_name.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy deleted successfully !!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy deletion failed !!');", true);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            text_Clear();
            LeaveTypeGridView_details();
            // btn_add.Visible = true;
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            GridView1.Visible = false;
            Panel4.Visible = false;

        }
    }


    protected void txt_fromdate_TextChanged(object sender, EventArgs e)
    {
        if (txt_fromdate.Text != "" && txt_todate.Text != "")
        {
            DateTime FromYear = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToYear = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //Storing input Dates  
            //DateTime FromYear = Convert.ToDateTime(txt_fromdate.Text.Substring(3, 2) + "/" + txt_fromdate.Text.Substring(0, 2) + "/" + txt_fromdate.Text.Substring(6, 4));
            //DateTime ToYear = Convert.ToDateTime(txt_todate.Text.Substring(3, 2) + "/" + txt_todate.Text.Substring(0, 2) + "/" + txt_todate.Text.Substring(6, 4));

            //Creating object of TimeSpan Class  
            TimeSpan objTimeSpan = ToYear - FromYear;

            //TotalDays  
            double Days = Convert.ToDouble(objTimeSpan.TotalDays);
            Days = Days + 1;
            while (FromYear < ToYear)
            {
                //assuming saturday and sunday are weekends

                if ((FromYear.DayOfWeek == DayOfWeek.Saturday) || (FromYear.DayOfWeek == DayOfWeek.Sunday))
                {
                    Days = Days - 1;
                }
                FromYear = FromYear.AddDays(1);

            }

            // Store final value in textbox

        }
    }
    protected void btn_leave_cancel_Click(object sender, EventArgs e)
    {
        int res = 0;
        try
        {

            res = d.operation("update pay_leave_transaction set Leave_Status='Cancelled', status_comment = 'Employee Cancelled'  where comp_code='" + Session["COMP_CODE"].ToString() + "' and LEAVE_ID = " + ViewState["leaveid"].ToString());
            int result = 0;
            if (result > 0)
            {
                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);
                }
                res = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0','" + Session["USERNAME"].ToString() + " - Cancelled Leave','Leave_form_management.aspx')");
            }
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txt_leave_accural.Text == "")
        {
            txt_leave_accural.Text = "0";
        
        }
        ddl_adv_req.SelectedValue = "select";
        ddl_bck_date.SelectedValue = "select";
        GridView1.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("leave_name");
        dt.Columns.Add("abbreviation");
        dt.Columns.Add("gender");
        dt.Columns.Add("max_no_of_leave");
        dt.Columns.Add("carry_forward");
        dt.Columns.Add("Leave_Accural");
        dt.Columns.Add("Advance_request");
        dt.Columns.Add("Backdated_request");


        int rownum = 0;
        for (rownum = 0; rownum < GridView1.Rows.Count; rownum++)
        {
            if (GridView1.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                Label lbl_Leave_Name = (Label)GridView1.Rows[rownum].Cells[1].FindControl("lbl_Leave_Name");
                dr["leave_name"] = lbl_Leave_Name.Text.ToString();
                Label lbl_Abb = (Label)GridView1.Rows[rownum].Cells[2].FindControl("lbl_Abb");
                dr["abbreviation"] = lbl_Abb.Text.ToString();
                Label lbl_Gender = (Label)GridView1.Rows[rownum].Cells[3].FindControl("lbl_Gender");
                dr["gender"] = lbl_Gender.Text.ToString();

                Label lbl_max_no_leave = (Label)GridView1.Rows[rownum].Cells[4].FindControl("lbl_max_no_leave");
                dr["max_no_of_leave"] = lbl_max_no_leave.Text.ToString();


                Label lbl_carry_forward = (Label)GridView1.Rows[rownum].Cells[5].FindControl("lbl_carry_forward");
                dr["carry_forward"] = lbl_carry_forward.Text.ToString();

                Label lbl_leave_accural = (Label)GridView1.Rows[rownum].Cells[6].FindControl("lbl_leave_accural");
                dr["Leave_Accural"] = lbl_leave_accural.Text.ToString();

                Label lbl_Advance_request = (Label)GridView1.Rows[rownum].Cells[6].FindControl("lbl_Advance_request");
                dr["Advance_request"] = lbl_Advance_request.Text.ToString();

                Label lbl_Backdated_request = (Label)GridView1.Rows[rownum].Cells[6].FindControl("lbl_Backdated_request");
                dr["Backdated_request"] = lbl_Backdated_request.Text.ToString();




                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["leave_name"] = txt_leave_name.SelectedValue;
        dr["abbreviation"] = txt_abbreviation.Text;
        dr["gender"] = ddl_gender.SelectedItem;
        dr["max_no_of_leave"] = txt_max_no_of_leave.Text;
        dr["carry_forward"] = txt_carry_forward.Text;
        dr["Leave_Accural"] = txt_leave_accural.Text;
        dr["Advance_request"] = ddl_adv_req.SelectedItem;
        dr["Backdated_request"] = ddl_bck_date.SelectedItem;


        //dr["abb"] = txt_abb.Text;GridView1

        dt.Rows.Add(dr);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        ViewState["CurrentTable"] = dt;

        txt_leave_name.SelectedIndex = 0;
        txt_abbreviation.Text = "";
        ddl_gender.SelectedValue = "Select";
        txt_max_no_of_leave.Text = "";
        txt_carry_forward.Text = "";
        txt_leave_accural.Text = "";



    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        d1.con1.Open();

        System.Web.UI.WebControls.Label lbl_leaveid1 = (System.Web.UI.WebControls.Label)LeaveTypeGridView.SelectedRow.FindControl("lbl_Leave_Name");
        MySqlCommand cmd_hol = new MySqlCommand("SELECT leave_name,abbreviation,gender,max_no_of_leave,carry_forward,Leave_Accural FROM pay_policy_master", d1.con1);
        MySqlDataReader dr_hol = cmd_hol.ExecuteReader();
        while (dr_hol.Read())
        {
            txt_leave_name.SelectedValue = dr_hol.GetValue(0).ToString();
            txt_abbreviation.Text = dr_hol.GetValue(1).ToString();
            ddl_gender.SelectedValue = dr_hol.GetValue(2).ToString();
            txt_max_no_of_leave.Text = dr_hol.GetValue(3).ToString();
            txt_carry_forward.Text = dr_hol.GetValue(4).ToString();
            txt_leave_accural.Text = dr_hol.GetValue(5).ToString();
        }


        dr_hol.Dispose();
        cmd_hol.Dispose();
        d1.con1.Close();
        //btn_add.Visible = false;
        btn_edit.Visible = true;
        btn_delete.Visible = false;
    }

    protected void ddl_state_click(object sender, EventArgs e)
    {

        try {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", " state_holiday(()", true);
        
        }
        catch { }


        if (ddl_stateholi.SelectedValue == "1")
        {
            dd_state.Visible = true;
          abc.Style.Add("display", "show");
            dd_state.Items.Clear();

            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {

                    dd_state.Items.Add(dr_item1[0].ToString());

                }
                dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }
            dd_state.Items.Insert(0, new ListItem("Select"));
        }
        else
        {
            dd_state.Items.Clear();
            dd_state.Items.Insert(0, new ListItem("Select"));
            //dd_state.Visible = false;
            abc.Style.Add("display", "none");
        }


    }



    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        try
        {
            GridView1.UseAccessibleHeader = false;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_itemslist_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemslist.UseAccessibleHeader = false;
            gv_itemslist.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}