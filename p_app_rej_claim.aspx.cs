using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
        if (!IsPostBack)
        {
           
            if (!Session["EXPENSE_ID"].ToString().Equals(""))
            {
                load_gridview(Session["EXPENSE_ID"].ToString());
                //btn_add.Visible = true;
                //btnupdate.Visible = false;
            }
            else
            {
                gvexpenessadd.DataSource = null;
                gvexpenessadd.DataBind();
            }
            comments_box.Visible = false;
            comments_box1.Visible = false;
            btn_Seeking_Clarification.Visible = false;
        }
    }
    
    private void load_gridview(string expenses_Id_edit)
    {
        double a = 0, c = 0;
        try
        {
            string paths = Server.MapPath("~").ToString().Replace("\\", "\\\\");

            MySqlCommand cmd1 = new MySqlCommand("select id,Expenses_id,COMP_CODE,Emp_code,DATE_FORMAT(Date,'%d/%m/%Y') as Date,CASE WHEN `city_type` = 1 THEN 'Inside City' WHEN `city_type` = 2 THEN 'Outside City' END AS 'city_type',travel_type,type,Merchant,Amount,description,status,comments, app_amount , case When IsNull(Expness_Image) then Concat('~//Images//placeholder.png') else Concat('~//EMP_Images//',Expness_Image) END as 'Path' from pay_add_expenses where Expenses_id='" + expenses_Id_edit + "'", d.con);
            d.con.Open();
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                foreach (DataRow row in dt.Rows)
                {
                    a = a + double.Parse(row["Amount"].ToString());
                    c = c + double.Parse(row["app_amount"].ToString());
                }
                gvexpenessadd.DataSource = dt;
                gvexpenessadd.DataBind();
                gvexpenessadd.Visible = true;
                gvexpenessadd.FooterRow.Cells[7].Text = "Total:";
                gvexpenessadd.FooterRow.Cells[8].Text = a.ToString();
                gvexpenessadd.FooterRow.Cells[9].Text = c.ToString();
                gvexpenessadd.FooterRow.Cells[8].Visible = true;
                gvexpenessadd.FooterRow.Cells[7].Visible = true;
            }

            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        gvexpenessadd.Visible = true;

    }

   
    protected void gv_paymentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (dr["Path"].ToString() == "")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;
                e.Row.Cells[14].Text = "";

            }
        }
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            ImageButton im = default(ImageButton);
            im = (ImageButton)e.Row.FindControl("Image1");
            im.Attributes.Add("onclick", "javascript:window.open('../CeltPayroll/show_image.aspx?ID=" + DataBinder.Eval(e.Row.DataItem, "ID") + "','','height=400,   width=400,   top=120,   left=600,   toolbar=no,   menubar=yes,   scrollbars=yes,resizable=yes,location=no')");
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
  
    private void update_notification()
    {
        d.con1.Open();

        try
        {
            if (Application["policy_id"] == null || Application["policy_id"].ToString() == "")
            {
                Application["policy_id"] = Session["system_curr_date"].ToString();
                // MySqlCommand cmd1 = new MySqlCommand("SELECT id,txt_policy_name FROM pay_travel_policy_master  WHERE COMP_CODE=@COMP_CODE and UNIT_CODE=UNIT_CODE", d1.con1);
                MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_CODE,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["policy_id"].ToString() + "','%d/%m'),'%d %b'))as noti from PAY_EMPLOYEE_MASTER WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["policy_id"].ToString() + "',1,5)", d.con1);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select  'Claim for " + Session["USERNAME"].ToString() + " -status- ','0',EMP_CODE,'app_rej_travelplan.aspx' from pay_add_expenses");
                }

            }
            else
            {
                //DateTime policy_id = DateTime.ParseExact(Application["policy_id"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime systemdate = DateTime.ParseExact(Session["system_curr_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //if (policy_id <= systemdate)
                //{
                //    for (int i = 1; policy_id != systemdate; i++)
                //    {
                MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_NAME,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["policy_id"].ToString() + "','%d/%m'),'%d %b'))as noti from PAY_EMPLOYEE_MASTER WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["policy_id"].ToString() + "',1,5)", d.con1);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select  'Claim for " + Session["USERNAME"].ToString() + " -status-','0',EMP_CODE,'app_rej_travelplan.aspx' from pay_add_expenses");
                }
                dr1.Close();
                //policy_id = policy_id.AddDays(1);
                //Application["policy_id"] = policy_id.ToString("dd/MM/yyyy");

                //    }
                //}

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
        }
        //insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select (select concat('Its ',EMP_NAME,' birthday today - ', DATE_FORMAT(NOW(),'%d %b %y')) from PAY_EMPLOYEE_MASTER WHERE str_to_date(BIRTH_DATE,'%d/%m/%Y') = date(now())),'0',EMP_CODE,'birthday.aspx' from PAY_EMPLOYEE_MASTER

    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        comments_box.Visible = false;
        comments_box1.Visible = false;
        int total_approve = 0;
        try
        {
            for (int k = 0; k < gvexpenessadd.Rows.Count; k++)
            {
                System.Web.UI.WebControls.CheckBox CheckBoxchckrw1 = (System.Web.UI.WebControls.CheckBox)gvexpenessadd.Rows[k].FindControl("chkbox_expense_gv");
                TextBox txt_comment = (TextBox)gvexpenessadd.Rows[k].Cells[5].FindControl("txt_comment"); ;
                int txt_comment1 = Convert.ToInt32(txt_comment.Text);
                //int j = 0;
                //string txt_comment1 = gvexpenessadd.Rows[k].Cells[5].Text;

                if (txt_comment1.ToString() == "0" )
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Approve Amount!!');", true);

                }

                else
                {
                 
                    for (int i = 0; i < gvexpenessadd.Rows.Count; i++)
                    {
                        System.Web.UI.WebControls.CheckBox CheckBoxchckrw = (System.Web.UI.WebControls.CheckBox)gvexpenessadd.Rows[i].FindControl("chkbox_expense_gv");

                        string expenses_Id_edit = gvexpenessadd.Rows[i].Cells[1].Text;

                        int approve_result = d.operation("Update pay_add_expenses SET status = 'Approved' Where Expenses_id = '" + expenses_Id_edit + "'");
                        if (approve_result > 0)
                        {
                            total_approve = total_approve + 1;


                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Add Claim Approved!!');", true);
                    }
                    // }
                    //if (total_checked == total_approve)
                    //{

                    //}
                    //else
                    //{

                    //}

                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            update_grid();
        }
        update_notification();
    }
    public void update_grid()
    {
        System.Data.DataTable dt_gv = new System.Data.DataTable();
        d.con.Open();
        //MySqlCommand cmd_gv = new MySqlCommand("SELECT EP_NO,expense_title,case When expense_status = 1 then 'Pending' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = submitter_code AND COMP_CODE ='"+Session["COMP_CODE"].ToString()+"') AS submitter,DATE_FORMAT(submitted_on,'%d/%m/%Y') As submitted_on,expense_amount FROM expense_approval WHERE (COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND approver = '"+Session["LOGIN_ID"].ToString()+"') ORDER BY EP_NO desc", d.con);

        MySqlCommand cmd_gv = new MySqlCommand("Select Expenses_id,id,DATE_FORMAT(Date,'%d/%m/%Y') as Date,CASE WHEN `city_type` = 1 THEN 'Inside City' WHEN `city_type` = 2 THEN 'Outside City' END AS 'City_type',travel_type,type,Merchant ,Amount,app_amount,description,comments, status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = pay_add_expenses.Emp_code AND COMP_CODE ='" + Session["COMP_CODE"].ToString() + "') AS submitter,Amount As expense_amount,case When IsNull(Expness_Image) then Concat('~//Images//placeholder.png') else Concat('~//EMP_Images//',Expness_Image) END as 'Path' from pay_add_expenses where Expenses_id='" + Session["EXPENSE_ID"] + "'", d.con);

       // MySqlCommand cmd_gv = new MySqlCommand("Select Expenses_id,id,Date,Merchant ,Category,Amount,app_amount,description,comments,case status When 0 then 'In Progress' When 1 then 'Submitted' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = pay_add_expenses.Emp_code AND COMP_CODE ='" + Session["COMP_CODE"].ToString() + "') AS submitter,sum(Amount) As expense_amount from pay_add_expenses where Expenses_id='" + Session["EXPENSE_ID"] + "'", d.con);
        MySqlDataAdapter dr_gv = new MySqlDataAdapter(cmd_gv);
        dr_gv.Fill(dt_gv);
        gvexpenessadd.DataSource = dt_gv;
        gvexpenessadd.DataBind();
        d.con.Close();
    }

    protected void btn_reject_Click(object sender, EventArgs e)
    {
        int total_approve = 0;
        int total_checked = 0;
        comments_box.Visible = true;
        comments_box1.Visible = true;
        update_notification();
        if (txt_comments.Text != "")
        {
            try
            {
                for (int i = 0; i < gvexpenessadd.Rows.Count; i++)
                {
                    System.Web.UI.WebControls.CheckBox CheckBoxchckrw = (System.Web.UI.WebControls.CheckBox)gvexpenessadd.Rows[i].FindControl("chkbox_expense_gv");

                    //string Expenses_id=VendorEventGridView.SelectedRow.Cells[1].Text;
                    string expenses_Id_edit = gvexpenessadd.Rows[i].Cells[1].Text;
                    // string ep_id = VendorEventGridView.Rows[i].Cells[2].Text; 
                    //  if (CheckBoxchckrw.Checked == true)
                    // {
                    //total_checked++;
                    int approve_result = d.operation("Update pay_add_expenses SET status = 'Rejected' , comments='" + txt_comments.Text + "' Where Expenses_id = '" + expenses_Id_edit + "'");
                    if (approve_result > 0)
                    {// total_approve = total_approve + 1; 
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Rejected!!');", true);
                        comments_box.Visible = false;
                        comments_box1.Visible = false;
                    }
                    // }
                }
                if (total_checked == total_approve)
                {

                }
                else
                {

                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                update_grid();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Comments for Rejection!!');", true);
        }
       
    }
    protected void btn_Seeking_Clarification_Click(object sender, EventArgs e)
    {
        int total_approve = 0;
        int total_checked = 0;


        try
        {
            for (int i = 0; i < gvexpenessadd.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox CheckBoxchckrw = (System.Web.UI.WebControls.CheckBox)gvexpenessadd.Rows[i].FindControl("chkbox_expense_gv");

                //string Expenses_id=VendorEventGridView.SelectedRow.Cells[1].Text;
                string expenses_Id_edit = gvexpenessadd.Rows[i].Cells[1].Text;
                // string ep_id = VendorEventGridView.Rows[i].Cells[2].Text; 
                //  if (CheckBoxchckrw.Checked == true)
                // {
                //total_checked++;
                int approve_result = d.operation("Update pay_add_expenses SET status = 'Seeking Clarification' , comments='" + txt_comments.Text + "' Where Expenses_id = '" + expenses_Id_edit + "'");
                if (approve_result > 0)
                {// total_approve = total_approve + 1; 
                }
                // }
            }
            if (total_checked == total_approve)
            {

            }
            else
            {

            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            update_grid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Will Not Approve!!');", true);
        }
        update_notification();
    }
    protected void txt_comment_TextChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow row in gvexpenessadd.Rows)
        {
        Label lbl_amount = (Label)row.FindControl("lbl_amount");
        int lbl_amount1 = Convert.ToInt32(lbl_amount.Text);

        TextBox txt_comment = (TextBox)row.FindControl("txt_comment");
        if (string.IsNullOrEmpty(txt_comment.Text))
        {
            txt_comment.Text = "0";
        }

        int txt_comment1 = Convert.ToInt32(txt_comment.Text);

        if (txt_comment1 > lbl_amount1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Approved Amount Less Then Or Equal To Claimed Amount " + lbl_amount1 + "')", true);
                txt_comment.Text = "0";
            }
            else
            {
                txt_comment.Text.ToString().Trim();
                int index = row.RowIndex;
                d.operation("update pay_add_expenses set app_amount = '" + txt_comment.Text + "' where ID = " + int.Parse(gvexpenessadd.DataKeys[index].Values[0].ToString()));
                load_gridview(Session["EXPENSE_ID"].ToString());

            }





        //int totalrows = gvexpenessadd.Rows.Count;
        //if (totalrows > 0)
        //{

        //    int gridsaves = 0;
        //    for (int i = 0; i < totalrows; i++)
        //    {
        //        string expenses_Id_edit = gvexpenessadd.SelectedRow.Cells[4].Text;

        //        Label lbl_claimamt = (Label)gvexpenessadd.Rows[i].Cells[4].FindControl("Amount");
        //        int claim_amt = Convert.ToInt32(lbl_claimamt.Text);

        //        TextBox lbl_appamt = (TextBox)gvexpenessadd.Rows[i].Cells[5].FindControl("app_amount");
        //        int app_amt = Convert.ToInt32(lbl_appamt.Text);

        //        if (app_amt > claim_amt)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Correct Amount');", true);
        //        }
        //        else
        //        {

                   
        //        }
        //    }
        }
    }

    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {

        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


    }
}