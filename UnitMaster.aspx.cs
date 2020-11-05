using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;


public partial class UnitDetails : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d_cg = new DAL();
    public string start_date = "", end_date = "";
    public string free_count = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = true;
            // btnexporttoexcelunit.Visible = false;
            //  btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = true;
            //  btn_new.Visible = false;
            //btnexporttoexcelunit.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            //btnexporttoexcelunit.Visible = false;
        }
        txt_unitcode.ReadOnly = true;

        //MySqlCommand cmd = new MySqlCommand("select DATE_FORMAT(start_date,'%d/%m/%Y') ,DATE_FORMAT(end_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and STATE='" + ddl_state.SelectedValue + "' and unit_code is null limit 1 ", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{
        //  //  this.Hidden1.Value = "swara";
        //    this.Hidden1.Value = dr.GetValue(0).ToString();
        //    this.Hidden2.Value = dr.GetValue(1).ToString();
        //}
        //dr.Close();
        //d.con.Close();



        if (!IsPostBack)
        {
            UnitGridView.Visible = true;
            d.con1.Open();
            try
            {
                // MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT client_code, client_NAME FROM pay_client_master where comp_code = '" + Session["comp_code"].ToString() + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND EMP_CODE ='"+Session["LOGIN_ID"].ToString()+"')", d.con1);
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT client_code, CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name' FROM pay_client_master WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_active_close='0' ", d.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);
                ddlunitclient.DataSource = DS;
                ddlunitclient.DataBind();
                ddlunitclient1.DataSource = DS;
                ddlunitclient1.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con1.Close();
                ddlunitclient.Items.Insert(0, new ListItem("Select", "Select"));

            }


            try
            {
                d.con1.Open();
                // MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT client_code, client_NAME FROM pay_client_master where comp_code = '" + Session["comp_code"].ToString() + "' AND  client_code in(select client_code from pay_client_state_role_grade where COMP_CODE='"+Session["COMP_CODE"].ToString()+"' AND EMP_CODE ='"+Session["LOGIN_ID"].ToString()+"')", d.con1);
                MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_active_close ='0' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ")", d.con1);
                DataSet DS = new DataSet();
                MySqlDataAdapter.Fill(DS);

                ddlunitclient1.DataSource = DS;
                ddlunitclient1.DataBind();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con1.Close();

                ddlunitclient1.Items.Insert(0, new ListItem("Select", "Select"));
            }
            loadclientgrid();
            txt_reason_updation.Text = string.Empty;
            if (txt_count.Text == "")
            {
                txt_count.Text = "0";
            }
            gv_start_end_date();
        }
    }

    protected void ddl_desid(object sender, EventArgs e)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand("select DATE_FORMAT(start_date,'%d/%m/%Y') ,DATE_FORMAT(end_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and STATE='" + ddl_state.SelectedValue + "' and `DESIGNATION`='" + ddl_designation.SelectedValue + "' and unit_code is null limit 1 ", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //  this.Hidden1.Value = "swara";
                this.Hidden1.Value = dr.GetValue(0).ToString();
                this.Hidden2.Value = dr.GetValue(1).ToString();
            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }

        try
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("select category from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and STATE='" + ddl_state.SelectedValue + "' and `DESIGNATION`='" + ddl_designation.SelectedValue + "' and unit_code is null limit 1 ", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txt_category.Text = (dr[0].ToString());
            }
            dr.Close();
            des_date();
            txt_end_date.ReadOnly = true;
            //d.con.Close();
            //MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            //while (dr_item1.Read())
            //{
            //    txtunitcity.Items.Add(dr_item1[0].ToString());
            //}
            //dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Close();
        }
    }
    protected void gv_branch_wise(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT client_code, client_NAME FROM pay_client_master where comp_code = '" + Session["comp_code"].ToString() + "' and UNIT_CODE='" + ddlunitclient1.SelectedValue + "'", d.con1);
            DataSet DS = new DataSet();
            MySqlDataAdapter.Fill(DS);
            //ddlunitclient.DataSource = DS;
            // ddlunitclient.DataBind();
            ddlunitclient1.DataSource = DS;
            ddlunitclient1.DataBind();
            d.con1.Open();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            ddlunitclient1.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }
    private void loadclientgrid()
    {
        UnitBAL ubl1 = new UnitBAL();
        DataSet ds = new DataSet();
        string employee_code = Session["REPORTING_EMP_SERIES"].ToString();
        ds = ubl1.UnitSelect(Session["comp_code"].ToString(), ddlunitclient1.SelectedValue, employee_code);
        UnitGridView.DataSource = ds.Tables[0];
        UnitGridView.DataBind();
        btn_add.Visible = true;
        btn_delete.Visible = false;
        btn_edit.Visible = false;
        btn_approval.Visible = false;
        //btnexporttoexcelunit.Visible = false;
        reason_panel.Visible = false;
        load_reporting_grdv();
        text_clear();
    }
    protected void lnkbtn_addmoreitem_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }


        if (d.getsinglestring("select client_code from pay_designation_count where (str_to_date('" + txt_start_date.Text + "','%d/%m/%Y') between (start_date) and (end_date)) and (str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') between (start_date) and (end_date))  and client_code = '" + ddlunitclient.SelectedValue + "' order by client_code limit 1").Equals(""))
        {

            string date1 = d.getsinglestring("select date_format(start_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddlunitclient.SelectedValue + "' and state='" + ddl_state.SelectedValue + "' and designation='" + ddl_designation.SelectedValue + "' limit 1");
            string date2 = txt_end_date.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start " + date1 + " and End " + date2 + " Date should be betweeen Client Contract dates.');", true);
            txt_start_date.Focus();
            return;
        }
        if (txt_count.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation Count Cannot be Zero (0).');", true);
            return;
        }
        string res = "", res1 = "", count = "";
        res = d.getsinglestring("select sum(count) from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddlunitclient.SelectedValue + "' and state='" + ddl_state.SelectedValue + "' and designation='" + ddl_designation.SelectedValue + "'  and UNIT_CODE is null group by designation");

        res1 = d.getsinglestring("select sum(count) from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code= '" + ddlunitclient.SelectedValue + "' and state='" + ddl_state.SelectedValue + "' and designation='" + ddl_designation.SelectedValue + "' And branch_status = 0 and UNIT_CODE is not null group by designation");
        if (res == "") { res = "0"; } if (res1 == "") { res1 = "0"; } int i = int.Parse(res); int j = int.Parse(res1);
        if (i <= j)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('For this designation,count limit already full fill.');", true);
            return;
        }
        count = txt_count.Text;
        j = j + int.Parse(count);
        //for (int rownum1 = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
        //{
        //    if (gv_itemslist.Rows[rownum1].RowType == DataControlRowType.DataRow)
        //    {
        //        if ((ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text))
        //        {
        //            string gv_count = "";
        //            gv_count = gv_itemslist.Rows[rownum1].Cells[3].Text; if (gv_count == "") { gv_count = "0"; }
        //            int gv_i = int.Parse(gv_count);
        //            j = j + gv_i;
        //            gv_i = 0;
        //        }
        //    }
        //}
        if (i < j)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('For this designation,count limit already full fill.');", true);
            return;
        }
        for (int rownum1 = 0; rownum1 < gv_itemslist.Rows.Count; rownum1++)
        {
            if (gv_itemslist.Rows[rownum1].RowType == DataControlRowType.DataRow)
            {

                if ((txt_start_date.Text == gv_itemslist.Rows[rownum1].Cells[5].Text))
                {

                    if (ddl_designation.SelectedItem.Text == gv_itemslist.Rows[rownum1].Cells[2].Text)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('same starting date does not added');", true);
                        return;

                    }
                }
            }
        }
        if (DateTime.ParseExact(txt_start_date.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txt_end_date.Text, "dd/MM/yyyy", null))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start Date Cannot be Grater than  end date')", true);
            txt_start_date.Focus();
            return;
        }
        if (chkcount() == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Designation Count ,Does Not Match Client Contract');", true);
            return;
        }
        else
        {
            string id = d.getsinglestring("select max(id) from pay_designation_count");
            gv_itemslist.Visible = true;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("DESIGNATION");
            dt.Columns.Add("COUNT");
            dt.Columns.Add("HOURS");
            dt.Columns.Add("unit_start_date");
            dt.Columns.Add("unit_end_date");
            dt.Columns.Add("ID");
            dt.Columns.Add("category");
            int rownum = 0;
            for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
            {
                if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
                {
                    dr = dt.NewRow();
                    dr["DESIGNATION"] = gv_itemslist.Rows[rownum].Cells[2].Text;
                    dr["COUNT"] = gv_itemslist.Rows[rownum].Cells[3].Text;
                    dr["HOURS"] = gv_itemslist.Rows[rownum].Cells[4].Text;
                    dr["unit_start_date"] = gv_itemslist.Rows[rownum].Cells[5].Text;
                    dr["unit_end_date"] = gv_itemslist.Rows[rownum].Cells[6].Text;
                    dr["ID"] = gv_itemslist.Rows[rownum].Cells[7].Text;
                    dr["category"] = gv_itemslist.Rows[rownum].Cells[8].Text;
                    dt.Rows.Add(dr);
                }
            }
            dr = dt.NewRow();
            dr["DESIGNATION"] = ddl_designation.SelectedItem.Text;
            dr["COUNT"] = txt_count.Text;
            dr["HOURS"] = txt_working_hrs.Text;
            dr["unit_start_date"] = txt_start_date.Text;
            dr["unit_end_date"] = txt_end_date.Text;
            dr["ID"] = txt_end_date.Text;
            dr["category"] = txt_category.Text;
            dt.Rows.Add(dr);
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
            ViewState["CurrentTable"] = dt;
            txt_count.Text = "0";
            ddl_designation.Text = "";
            txt_working_hrs.Text = "0";
            txt_start_date.Text = "";
            txt_end_date.Text = "";
            txt_category.Text = "";
        }
    }

    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
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
        e.Row.Cells[7].Visible = false;
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

                    d1.operation("DELETE FROM pay_designation_count WHERE comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + txt_unitcode.Text + "'  and `id` = '" + dt.Rows[rowID][0].ToString() + "'");//delete command
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
        }

    }
    protected void designstion_details1(object sender, EventArgs e)
    {
        d1.con1.Open();
        try
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
            //  MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddlunitclient.SelectedValue + "' and state in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddlunitclient.SelectedValue + "') and unit_code is null ORDER BY STATE", d1.con1);

            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddlunitclient.SelectedValue + "'  and unit_code is null union  SELECT distinct state FROM pay_designation_count where CLIENT_CODE = '" + ddlunitclient.SelectedValue + "' and unit_code is null and comp_code = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE not in (select distinct client_code from pay_unit_master where comp_code = '" + Session["COMP_CODE"].ToString() + "') ", d1.con1);
            DataSet DS = new DataSet();
            MySqlDataAdapter.Fill(DS);
            ddl_state.DataSource = DS;
            ddl_state.DataBind();
            d1.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_state.Items.Insert(0, new ListItem("Select", ""));
            d1.con1.Close();
            d.con.Close();


            MySqlDataAdapter dr = new MySqlDataAdapter("select comp_name,Companyname_gst_no,gst_address,percent from  pay_company_group where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and (unit_code is NULL or unit_code = '')", d.con);
            d.con.Open();
            DataTable dt = new DataTable();
            dr.Fill(dt);
            gv_compgroup_type.DataSource = dt;
            gv_compgroup_type.DataBind();
            d.con.Close();
        }
        txt_zone_region();
        rm_and_ae_check();
    }
    protected void get_empcount_details(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT distinct GRADE_CODE,GRADE_DESC FROM pay_grade_master where comp_code ='" + Session["comp_code"].ToString() + "' ", d.con);
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string ehead1 = dr.GetValue(0).ToString();
            }
            int n = 10;
            TextBox[] textBoxes = new TextBox[n];
            Label[] labels = new Label[n];

            for (int i = 0; i < n; i++)
            {
                textBoxes[i] = new TextBox();
                // Here you can modify the value of the textbox which is at textBoxes[i]

                labels[i] = new Label();
                // Here you can modify the value of the label which is at labels[i]
            }

            // This adds the controls to the form (you will need to specify thier co-ordinates etc. first)
            for (int i = 0; i < n; i++)
            {
                this.Controls.Add(textBoxes[i]);
                this.Controls.Add(labels[i]);
            }

        }
        catch { }
        finally { }

    }
    protected void checklistcox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int n = 1;
        TextBox[] textBoxes = new TextBox[n];
        // Label[] labels = new Label[n];

        for (int i = 0; i < n; i++)
        {
            textBoxes[i] = new TextBox();
        }

        for (int i = 0; i < n; i++)
        {
            this.Controls.Add(textBoxes[i]);
            //  this.Controls.Add(labels[i]);
        }

    }
    protected void gesignation_fill()
    {
        d.con1.Open();
        try
        {
            ddl_designation.Items.Clear();
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("SELECT DISTINCT(DESIGNATION) from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and state='" + ddl_state.SelectedValue + "' and unit_code is null ORDER BY DESIGNATION ", d.con1);

            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_designation.DataSource = dt_item;
                ddl_designation.DataTextField = dt_item.Columns[0].ToString();
                ddl_designation.DataValueField = dt_item.Columns[0].ToString();
                ddl_designation.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            ddl_designation.Items.Insert(0, new ListItem("Select", ""));
        }
    }

    protected void count_Info(object sender, EventArgs e)
    {
        if (chkcount() == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Value Does Not Match With Client Contract');", true);
        }
    }

    private Boolean chkcount()
    {
        string client_emp_count;
        string unit_emp_count;
        if (ddl_branchStatus.SelectedValue == "0")
        {
            //  if (gv_itemslist.Rows.Count > 0)
            //{

            if (ddl_designation.SelectedItem.ToString() == "Select")
            {
                client_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "'   and UNIT_CODE is null  and `STATE`='" + ddl_state.SelectedValue + "'  and branch_status='0'");
            }
            else
            {
                client_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' and `HOURS`='" + txt_working_hrs.Text + "' and `DESIGNATION`='" + ddl_designation.SelectedValue + "'   and UNIT_CODE is null  and `STATE`='" + ddl_state.SelectedValue + "'  and branch_status='0'");
            }
            if (client_emp_count == "" || int.Parse(client_emp_count) < int.Parse(txt_count.Text))
            {
                txt_count.Text = "0";
                txt_working_hrs.Text = "0";
                return false;
            }
            if (ddl_designation.SelectedItem.ToString() == "Select")
            {

                unit_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "'  and `STATE`='" + ddl_state.SelectedValue + "' and branch_status='0'  AND `UNIT_CODE`='" + txt_unitcode.Text + "'  AND `UNIT_CODE` IS NOT  NULL");
            }
            else
            {
                unit_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "' and `HOURS`='" + txt_working_hrs.Text + "' and `DESIGNATION`='" + ddl_designation.SelectedValue + "'  and UNIT_CODE in (select unit_code from pay_unit_master where client_code = '" + ddlunitclient.SelectedValue + "' )  and `STATE`='" + ddl_state.SelectedValue + "' and branch_status='0'");
            }
            try
            {
                if (client_emp_count != "" && unit_emp_count != "")
                {
                    if (ddl_designation.SelectedItem.ToString() == "Select")
                    {
                        if (btn_edit.Visible == true)
                        {
                            if ((int.Parse(unit_emp_count) > int.Parse(client_emp_count)))
                            {
                                txt_count.Text = "0";
                                txt_working_hrs.Text = "0";
                                return false;
                            }
                        }
                        else
                        {
                            if ((int.Parse(unit_emp_count) >= int.Parse(client_emp_count)))
                            {
                                txt_count.Text = "0";
                                txt_working_hrs.Text = "0";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if ((int.Parse(unit_emp_count) + int.Parse(txt_count.Text)) > int.Parse(client_emp_count))
                        {
                            txt_count.Text = "0";
                            txt_working_hrs.Text = "0";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        //  }
        return true;
    }
    protected void txt_zone1_region(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
            txt_zone.Items.Clear();
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("SELECT DISTINCT(REGION) from pay_zone_master WHERE comp_code ='" + Session["comp_code"].ToString() + "'  and ZONE='" + txt_zone1.SelectedValue + "' and CLIENT_CODE='" + ddlunitclient.SelectedValue + "' and Type='REGION' and region is not null", d.con1);

            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txt_zone.DataSource = dt_item;
                txt_zone.DataTextField = dt_item.Columns[0].ToString();
                txt_zone.DataValueField = dt_item.Columns[0].ToString();
                txt_zone.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            txt_zone.Items.Insert(0, new ListItem("Select", ""));
        }
    }

    protected void txt_zone_region()
    {
        d.con1.Open();
        try
        {
            txt_zone1.Items.Clear();
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("select distinct(zone) from pay_zone_master where client_code='" + ddlunitclient.SelectedValue + "' and Type='REGION' and zone is not null and comp_code ='" + Session["comp_code"].ToString() + "' ", d.con1);

            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                txt_zone1.DataSource = dt_item;
                txt_zone1.DataTextField = dt_item.Columns[0].ToString();
                txt_zone1.DataValueField = dt_item.Columns[0].ToString();
                txt_zone1.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
            txt_zone1.Items.Insert(0, new ListItem("Select", ""));
        }
    }


    protected void fill_gv()
    {
        d.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT comp_code,STATE_CODE,Concat(select STATE_CODE. from pay_state_master where STATE_NAME='" + ddl_state.SelectedValue + "') ,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME , UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME, OT_RATE, OT_FORMULA, ESIC_FORMULA, PF_FORMULA, ESIC_ON_OT_FORMULA,CALENDER_START_DATE,TO_DATE, LEAVE_WAGE_PERCENTAGE, LEAVE_FORMULA, HEAD_CODE, MONTHLY_CALCULATION,  ACTIVE_FLAG, CUSTOMER_ID, PTAX_ON_OT, PTAX_ON_BONUS, BONUS_AMT_MONTHLY, UNIT_ESIC_DEDUCTION,REPORT_CATEGORY,OT_RATE_AMOUNT,TOTAL_DAY,HOLIDAY,WEEKLY_OFF,WORKING_DAYS,CAL_MONTH,CAL_YEAR, EXCEL_REPORT_FORMAT,PTAX_ON_EXTRA_OT,OT_MONTHLY_CAL_DAYS ,PF_SLAB ,PF_SLAB_PENSION,UNIT_EMAIL_ID,ESIC_UNIT_PERCENTAGE,unit_gst_no,service_tax_formula,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,SAC_CODE,file_no,Emp_count FROM pay_unit_master WHERE comp_code ='" + Session["comp_code"].ToString() + "' ORDER BY comp_code, UNIT_CODE", d.con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            UnitGridView.DataSource = ds.Tables[0];
            UnitGridView.DataBind();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }


    public static bool IsValidEmailId(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
            return true;
        else
            return false;
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (gv_itemslist.Rows.Count >= 1)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT(select COUNT(pay_unit_master.client_code) from pay_unit_master where client_code='" + ddlunitclient.SelectedValue.ToString() + "' and branch_status='0') as a  ,	(select pay_client_master.NO_OF_BRANCH from pay_client_master where client_code='" + ddlunitclient.SelectedValue.ToString() + "') as b", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item = cmd.ExecuteReader();

                if (dr_item.Read())
                {
                    UnitBAL ubl2 = new UnitBAL();
                    string cmpcode = Session["comp_code"].ToString();
                    string emp_name = Session["USERNAME"].ToString();
                    string newdate = Convert.ToString(System.DateTime.Now);
                    int result = 0;
                    try
                    {
                        unit_code();
                        //  btnnew_Click();
                        txt_unit_password.Text = cmpcode + "gJiYRq5463";
                        // string name = txt_gst_no.Text.ToString();
                        string branchcode = ddl_state.SelectedValue + "-" + txtunitaddress1.Text + "-" + txt_unitname.Text;
                        //if (gv_itemslist.Rows.Count < 1)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add atleast one Designation.');", true);
                        //    return;
                        //}
                        if (txt_material_area.Text == "")
                        {
                            txt_material_area.Text = "0";
                        }
                        if (txtbranch_cost_centre_code.Text == "")
                        {
                            txtbranch_cost_centre_code.Text = "0";
                        }
                        int resins1 = 0;
                        string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
                        result = ubl2.UnitInsert(cmpcode, txt_unitcode.Text, txt_unitname.Text, txtunitaddress1.Text.ToString(), txtunitaddress2.Text, txtunitcity.Text, ddl_state.SelectedItem.Text, txt_lattitude.Text.ToString(), txt_longitude.Text.ToString(), txt_area.Text.ToString(), txt_zone.SelectedValue, ddlunitclient.SelectedValue, file_txt.Text.ToString(), txtemailid.Text, txt_operationname.Text, txt_omobileno.Text, txt_oemailid.Text, txt_financename.Text, txt_fmobileno.Text, txt_femailid.Text, txt_locationname.Text, txt_lmobileno.Text, txt_lemailid.Text, txt_othername.Text, txt_othermobno.Text, txt_otheremailid.Text, ddl_designation.SelectedValue, txt_zone1.SelectedValue, reporting, txt_pincode.Text, txt_adminname.Text, txt_adminmobileno.Text, txt_adminemailid.Text, ddl_location_heaad_title.SelectedValue, ddl_operation_heaad_title.SelectedValue, ddl_finance_heaad_title.SelectedValue, ddl_admin_heaad_title.SelectedValue, ddl_other_heaad_title.SelectedValue, txt_clientbranchcode.Text, txt_opus_code.Text, txt_p_mob1.Text, txt_p_mob2.Text, txt_p_mob3.Text, txt_p_mob4.Text, txt_p_mob5.Text, txt_p_emailid1.Text, txt_p_emailid2.Text, txt_p_emailid3.Text, txt_p_emailid4.Text, txt_p_emailid5.Text, txt_p_bdate1.Text, txt_p_bdate2.Text, txt_p_bdate3.Text, txt_p_bdate4.Text, txt_p_bdate5.Text, txt_p_adate1.Text, txt_p_adate2.Text, txt_p_adate3.Text, txt_p_adate4.Text, txt_p_adate5.Text, "0", txtbranch_cost_centre_code.Text, ddl_location_type.SelectedValue.ToString(), txt_pccode.Text, txt_disticitivecode.Text, ddl_branch_type.SelectedValue, txt_material_area.Text, ddl_branchStatus.SelectedValue, ddl_labour_office.SelectedValue, txt_budget_material.Text, ddl_android_attendances_flag.SelectedValue.ToString(), ddl_service.SelectedValue, ddl_admin_expence.SelectedValue);


                        // fire extinguisher changes komal 24-07-2020
                        //fire_extinguisher_insert();
                        fire_extinguisher_function();







                        foreach (GridViewRow row in gv_itemslist.Rows)
                        {
                            string lbl_srno1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber")).Text;
                            string designation = row.Cells[2].Text;
                            int Count = int.Parse(row.Cells[3].Text);
                            int hours = int.Parse(row.Cells[4].Text);
                            string startdate = (row.Cells[5].Text);
                            string enddate = (row.Cells[6].Text);
                            string category = (row.Cells[8].Text);
                            resins1 = d.operation("INSERT INTO pay_designation_count(comp_code,CLIENT_CODE,STATE,DESIGNATION,UNIT_CODE,COUNT,CREATED_BY,CREATED_DATE,SR_NO, Hours,unit_start_date,unit_end_date,category) VALUES('" + Session["comp_code"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + ddl_state.SelectedItem.Text + "','" + designation + "','" + txt_unitcode.Text + "','" + Count + "','" + Session["LOGIN_ID"].ToString() + "',now(),'" + lbl_srno1 + "'," + hours + ",str_to_date('" + startdate + "','%d/%m/%Y') ,str_to_date('" + enddate + "','%d/%m/%Y'),'" + category + "')");

                            d.con.Open();
                            try
                            {
                                MySqlCommand cmd1 = new MySqlCommand("select sum(COUNT) from pay_designation_count where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
                                MySqlDataReader dr = cmd1.ExecuteReader();
                                if (dr.Read())
                                {
                                    string count = dr.GetValue(0).ToString();
                                    txt_empcount.Text = count;
                                    string count1 = txt_empcount.Text;
                                    d.operation("update pay_unit_master set Emp_count='" + count1 + "' where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    dr.Close();

                                }
                                d.con.Close();
                            }
                            catch (Exception ex) { throw ex; }
                            finally { d.con.Close(); }
                        }
                        gv_itemslist.DataSource = null;
                        gv_itemslist.DataBind();

                        foreach (GridViewRow row in gv_emailsend.Rows)
                        {
                            d.operation("INSERT INTO pay_branch_mail_details (COMP_CODE,CLIENT_CODE,unit_code,head_type,head_name,head_email_id,state)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + txt_unitcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + ddl_state.SelectedValue + "')");
                        }

                        if (ddlunitclient.SelectedValue == "RCPL")
                        {
                            if (check_comp_percent())
                            {


                                foreach (GridViewRow row in gv_compgroup_type.Rows)
                                {
                                    Label lbl_compname = (Label)row.FindControl("lbl_compname");
                                    string compname = (lbl_compname.Text);
                                    Label lbl_cmp_gst = (Label)row.FindControl("lbl_cmp_gst");
                                    string comp_gstno = (lbl_cmp_gst.Text);
                                    Label lbl_comp_address = (Label)row.FindControl("lbl_comp_address");
                                    string comp_address = (lbl_comp_address.Text);
                                    TextBox txt_gvper = (TextBox)row.FindControl("txt_gvper");
                                    string gst_per = txt_gvper.Text.ToString();

                                    d.operation("INSERT INTO pay_company_group (comp_code,client_code,comp_name,Companyname_gst_no,gst_address,unit_code,`percent`)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + compname + "','" + comp_gstno + "','" + comp_address + "','" + txt_unitcode.Text + "','" + gst_per + "')");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Company Percent Should be Equals To 100 !!')", true);
                                return;
                            }
                        }

                        if (result > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
                            string statecode = ddl_state.SelectedValue;
                            if (txt_unit_password.Text != "")
                            {
                                d.operation("delete from pay_user_master where unit_flag='1' and UNIT_CODE='" + txt_unitcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                                d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,FLAG,create_date,first_login,comp_code,UNIT_FLAG,UNIT_CODE,password_changed_date,client_code,client_zone,zone_region) Values('" + txt_unitcode.Text + "','" + txt_unitname.Text + "','" + GetSha256FromString(txt_unit_password.Text) + "','A',now(),'0','" + Session["comp_code"].ToString() + "','1','" + txt_unitcode.Text + "',now(),'" + ddlunitclient.SelectedValue + "','" + txt_zone1.SelectedValue + "','" + txt_zone.SelectedValue + "')");
                                // d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,ROLE,flag,create_user, create_date, password_changed_date,first_login,comp_code) VALUES('" + txt_eecode.Text + "','" + txt_eename.Text + "','" + GetSha256FromString(txt_birthdate.Text) + "','" + DropDownList1.SelectedItem.Text + "','A','" + Session["USERID"].ToString() + "',now(),now(),'0','" + Session["comp_code"].ToString() + "')");
                                send_email(Server.MapPath("~/User_Details.htm"), txtemailid.Text);
                            }
                            gv_itemslist.DataSource = null;
                            gv_itemslist.DataBind();

                            text_clear();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
                            gv_itemslist.DataSource = null;
                            gv_itemslist.DataBind();
                            text_clear();
                        }
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
                    }
                    catch (Exception ee)
                    {
                        throw ee;
                    }
                    finally
                    {
                        dr_item.Close();
                        d.con.Close();

                        loadclientgrid();
                        gv_compgroup_type.DataSource = null;
                        gv_compgroup_type.DataBind();
                    }


                }
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Click on Add Button to add Designation')", true);
            return;
        }
    }
    protected void UnitGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_itemslist.Visible = false;
        //gv_itemslist.DataSource = null;
        //System.Web.UI.WebControls.Label lbl_EMP_CODE = (System.Web.UI.WebControls.Label)UnitGridView.SelectedRow.FindControl("UNIT_CODE");

        //string l_unit_CODE = lbl_EMP_CODE.Text;
        string l_unit_CODE = UnitGridView.SelectedRow.Cells[1].Text;
        // getEmployee(l_EMP_CODE);
        load_fields(l_unit_CODE, 1);
        Session["UNIT_NO"] = l_unit_CODE.ToString();
    }
    private void load_fields(string unitcode, int counter)
    {
        MySqlCommand cmd1 = new MySqlCommand("SELECT Id,SR_NO, DESIGNATION,UNIT_CODE,COUNT,Hours,date_format(unit_start_date,'%d/%m/%Y')AS 'unit_start_date',date_format(unit_end_date,'%d/%m/%Y')AS 'unit_end_date',category from pay_designation_count WHERE UNIT_CODE ='" + unitcode + "' AND comp_code='" + Session["comp_code"].ToString() + "' ORDER BY SR_NO ", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt;
                    gv_itemslist.DataSource = dt;
                    gv_itemslist.DataBind();
                    Panel3.Visible = true;
                    Panel4.Visible = true;
                    gv_itemslist.Visible = true;
                }

            }
            dr1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        if (counter == 1)
        {
            reason_panel.Visible = true;
            btn_add.Visible = false;
            btn_delete.Visible = true;
            btn_edit.Visible = true;
        }

        MySqlCommand cmd2;
        if (counter == 1)
        {
            //  cmd2 = new MySqlCommand("SELECT comp_code, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_EmailId,FinanceHead_Mobileno,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments,PINCODE FROM pay_unit_master WHERE unit_CODE = '" + unitcode + "' AND comp_code='" + Session["comp_code"].ToString() + "' and (approval='' || approval is null)", d.con);
            cmd2 = new MySqlCommand("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,'',unit_Lattitude,unit_Longtitude,unit_distance,ZONE,pay_unit_master.client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_Mobileno,FinanceHead_EmailId,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments,PINCODE,LOGIN_ID,adminhead_name ,adminhead_mobile,adminhead_email,Client_branch_code,OPus_NO,p_mobileno1,p_mobileno2,p_mobileno3,p_mobileno4,p_mobileno5,p_emailid1,p_emailid2,p_emailid3,p_emailid4,p_emailid5,p_birthdate1,p_birthdate2,p_birthdate3,p_birthdate4,p_birthdate5,p_anniversaryate1,p_anniversaryate2,p_anniversaryate3,p_anniversaryate4,p_anniversaryate5,location_head_title,operation_head_title,finance_head_title,admin_head_title,other_head_title,branch_cost_centre_code,location_type,pc_code,distictive_code,IFNULL(branch_type,0) as 'branch_type',material_area,branch_status,labour_office,Budget_Materials,android_att_flag,date_format(branch_close_date,'%d/%m/%Y'),r_m_applicable,administrative_applicable  FROM pay_unit_master left outer JOIN pay_user_master ON pay_unit_master.UNIT_CODE = pay_user_master.UNIT_CODE WHERE pay_unit_master.unit_CODE = '" + unitcode + "' AND pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "'", d_cg.con1);
        }
        else
        {
            // cmd2 = new MySqlCommand("SELECT comp_code, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_EmailId,FinanceHead_Mobileno,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments,PINCODE FROM pay_unit_master WHERE unit_CODE = '" + unitcode + "' AND comp_code='" + Session["comp_code"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "'", d.con);
            cmd2 = new MySqlCommand("SELECT comp_code, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,'',unit_Lattitude,unit_Longtitude,unit_distance,ZONE,pay_unit_master.client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_Mobileno,FinanceHead_EmailId,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments,PINCODE,LOGIN_ID,adminhead_name ,adminhead_mobile,adminhead_email,Client_branch_code,OPus_NO,p_mobileno1,p_mobileno2,p_mobileno3,p_mobileno4,p_mobileno5,p_emailid1,p_emailid2,p_emailid3,p_emailid4,p_emailid5,p_birthdate1,p_birthdate2,p_birthdate3,p_birthdate4,p_birthdate5,p_anniversaryate1,p_anniversaryate2,p_anniversaryate3,p_anniversaryate4,p_anniversaryate5,location_head_title,operation_head_title,finance_head_title,admin_head_title,other_head_title,,branch_cost_centre_code,location_type,pc_code,distictive_code,IFNULL(branch_type,0) as 'branch_type',material_area,branch_status,labour_office,Budget_Materials,android_att_flag,date_format(branch_close_date,'%d/%m/%Y'),r_m_applicable,administrative_applicable FROM pay_unit_master left outer JOIN pay_user_master ON pay_unit_master.UNIT_CODE = pay_user_master.UNIT_CODE WHERE pay_unit_master.unit_CODE = '" + unitcode + "' AND pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "'", d_cg.con1);
        }
        d_cg.con1.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                int split = dr2.GetValue(2).ToString().LastIndexOf("-");
                ddlunitclient.SelectedValue = dr2.GetValue(12).ToString();

                txt_unitcode.Text = dr2.GetValue(1).ToString();
                txt_unitname.Text = dr2.GetValue(2).ToString().Substring(split + 1);
                txtunitaddress1.Text = dr2.GetValue(3).ToString();
                txtunitaddress2.Text = dr2.GetValue(4).ToString();
                designstion_details1(null, null);
                try
                {
                    ddl_state.SelectedValue = dr2.GetValue(6).ToString();
                    get_city_list(null, null);
                }
                catch { }
                txtunitcity.Text = dr2.GetValue(5).ToString();
                // txt_gst_no.Text = dr2.GetValue(7).ToString();

                txt_lattitude.Text = dr2.GetValue(8).ToString();
                txt_longitude.Text = dr2.GetValue(9).ToString();
                txt_area.Text = dr2.GetValue(10).ToString();
                file_txt.Text = dr2.GetValue(13).ToString();

                if (dr2.GetValue(14).ToString() != "")
                {
                    txtemailid.Text = dr2.GetValue(14).ToString();
                }
                else { txtemailid.Text = ""; }

                txt_operationname.Text = dr2.GetValue(15).ToString();
                txt_omobileno.Text = dr2.GetValue(16).ToString();
                txt_oemailid.Text = dr2.GetValue(17).ToString();

                txt_financename.Text = dr2.GetValue(18).ToString();
                txt_fmobileno.Text = dr2.GetValue(19).ToString();
                txt_femailid.Text = dr2.GetValue(20).ToString();

                txt_locationname.Text = dr2.GetValue(21).ToString();
                txt_lmobileno.Text = dr2.GetValue(22).ToString();
                txt_lemailid.Text = dr2.GetValue(23).ToString();

                txt_othername.Text = dr2.GetValue(24).ToString();
                txt_othermobno.Text = dr2.GetValue(25).ToString();
                txt_otheremailid.Text = dr2.GetValue(26).ToString();
                try
                {
                    gesignation_fill();
                    if (!dr2.GetValue(27).ToString().Equals(""))
                    {
                        ddl_designation.Text = dr2.GetValue(27).ToString();
                    }

                    txt_zone_region();
                    if (!dr2.GetValue(28).ToString().Equals(""))
                    {
                        txt_zone1.SelectedValue = dr2.GetValue(28).ToString();
                        txt_zone1_region(null, null);
                    }
                    txt_zone.SelectedValue = dr2.GetValue(11).ToString();
                    string tet = reason_updt(dr2.GetValue(29).ToString(), 1);
                }
                catch { }
                txt_pincode.Text = dr2.GetValue(30).ToString();
                txt_unit_login_id.Text = dr2.GetValue(31).ToString();

                txt_adminname.Text = dr2.GetValue(32).ToString();
                txt_adminmobileno.Text = dr2.GetValue(33).ToString();
                txt_adminemailid.Text = dr2.GetValue(34).ToString();
                txt_clientbranchcode.Text = dr2.GetValue(35).ToString();


                txt_opus_code.Text = dr2.GetValue(36).ToString();
                txt_p_mob1.Text = dr2.GetValue(37).ToString();
                txt_p_mob2.Text = dr2.GetValue(38).ToString();
                txt_p_mob3.Text = dr2.GetValue(39).ToString();
                txt_p_mob4.Text = dr2.GetValue(40).ToString();
                txt_p_mob5.Text = dr2.GetValue(41).ToString();
                txt_p_emailid1.Text = dr2.GetValue(42).ToString();
                txt_p_emailid2.Text = dr2.GetValue(43).ToString();
                txt_p_emailid3.Text = dr2.GetValue(44).ToString();
                txt_p_emailid4.Text = dr2.GetValue(45).ToString();
                txt_p_emailid5.Text = dr2.GetValue(46).ToString();
                txt_p_bdate1.Text = dr2.GetValue(47).ToString();
                txt_p_bdate2.Text = dr2.GetValue(48).ToString();
                txt_p_bdate3.Text = dr2.GetValue(49).ToString();
                txt_p_bdate4.Text = dr2.GetValue(50).ToString();
                txt_p_bdate5.Text = dr2.GetValue(51).ToString();
                txt_p_adate1.Text = dr2.GetValue(52).ToString();
                txt_p_adate2.Text = dr2.GetValue(53).ToString();
                txt_p_adate3.Text = dr2.GetValue(54).ToString();
                txt_p_adate4.Text = dr2.GetValue(55).ToString();
                txt_p_adate5.Text = dr2.GetValue(56).ToString();

                ddl_location_heaad_title.SelectedValue = dr2.GetValue(57).ToString();
                ddl_operation_heaad_title.SelectedValue = dr2.GetValue(58).ToString();
                ddl_finance_heaad_title.SelectedValue = dr2.GetValue(59).ToString();
                ddl_admin_heaad_title.SelectedValue = dr2.GetValue(60).ToString();
                ddl_other_heaad_title.SelectedValue = dr2.GetValue(61).ToString();
                txtbranch_cost_centre_code.Text = dr2.GetValue(62).ToString();
                ddl_location_type.SelectedValue = dr2.GetValue(63).ToString();
                txt_pccode.Text = dr2.GetValue(64).ToString();
                txt_disticitivecode.Text = dr2.GetValue(65).ToString();
                ddl_branch_type.SelectedValue = dr2.GetValue(66).ToString();
                txt_material_area.Text = dr2.GetValue(67).ToString();
                ddl_branchStatus.SelectedValue = dr2.GetValue(68).ToString();
                labour_office();
                if (dr2.GetValue(69).ToString() == "")
                {
                    ddl_labour_office.SelectedValue = "Select";
                }
                else
                {
                    ddl_labour_office.SelectedValue = dr2.GetValue(69).ToString();
                }
                txt_budget_material.Text = dr2.GetValue(70).ToString();
                ddl_android_attendances_flag.SelectedValue = dr2.GetValue(71).ToString();
                txt_br_cose_date.Text = dr2.GetValue(72).ToString();
                ddl_service.SelectedValue = dr2.GetValue(73).ToString();
                ddl_admin_expence.SelectedValue = dr2.GetValue(74).ToString();
                btn_add.Visible = false;
                txt_unitcode.ReadOnly = true;
            }
            upload_approval_documents();
            dr2.Dispose();
            d_cg.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_cg.con1.Close();
        }
        MySqlCommand cmd_hd = new MySqlCommand("SELECT head_type,head_name,head_email_id,mobileno FROM pay_branch_mail_details inner join pay_unit_master on pay_branch_mail_details.unit_code=pay_unit_master.unit_code and  pay_branch_mail_details.comp_code=pay_unit_master.comp_code WHERE pay_unit_master.client_code ='" + ddlunitclient.SelectedValue + "' AND pay_branch_mail_details.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and pay_branch_mail_details.unit_code='" + txt_unitcode.Text + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
            if (dr_hd.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_hd);
                if (dt.Rows.Count > 0)
                {
                    ViewState["emailtable"] = dt;
                }
                gv_emailsend.DataSource = dt;
                gv_emailsend.DataBind();
            }
            d.con.Close();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }



        ///// 


        gridview_fire_extinguisher.DataSource = null;
        gridview_fire_extinguisher.DataBind();


        d.con.Open();
        try
        {

            MySqlDataAdapter adp_govt = new MySqlDataAdapter("select id,fire_ex_type,DATE_FORMAT(renewal_date,'%d/%m/%Y') as 'renewal_date',DATE_FORMAT(expiry_date,'%d/%m/%Y') as 'expiry_date',weight_in_kg,vender_name,vender_no,fire_upload from pay_fire_extinguisher where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient.SelectedValue + "' and unit_code = '" + txt_unitcode.Text + "' and state_name = '" + ddl_state.SelectedValue + "'", d.con);

            DataTable dt = new DataTable();
            adp_govt.Fill(dt);
            if ((dt.Rows.Count > 0) && (dt.Rows[0][0] != DBNull.Value))
            {
                ViewState["fire_extinguisher"] = dt;

                gridview_fire_extinguisher.DataSource = dt;
                gridview_fire_extinguisher.DataBind();
            }

            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
        //
        //Company Group
        //MySqlCommand cmd_cg = new MySqlCommand("SELECT comp_name,Companyname_gst_no, gst_address,percent FROM  pay_company_group WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Unit_code = '" + unitcode + "'  or unit_code", d.con);
        //MySqlCommand cmd_cg = new MySqlCommand("SELECT comp_name, Companyname_gst_no, gst_address, percent FROM pay_company_group WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND (Unit_code = '" + unitcode + "' Or `unit_code` IS NULL)   AND comp_name IN (SELECT comp_name FROM pay_company_group WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND `unit_code` IS NULL And Client_Code = '" + ddlunitclient.SelectedValue + "') ", d.con);
        MySqlCommand cmd_cg = new MySqlCommand("SELECT comp_name, Companyname_gst_no, gst_address, percent FROM pay_company_group WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND Unit_code = '" + unitcode + "' AND percent > 0 AND Client_Code = '" + ddlunitclient.SelectedValue + "' UNION SELECT comp_name, Companyname_gst_no, gst_address, percent FROM pay_company_group WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND (Unit_code = '" + unitcode + "' OR unit_code IS NULL) AND comp_name NOT IN (SELECT comp_name FROM pay_company_group WHERE COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND Unit_code = '" + unitcode + "' AND percent > 0) ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_cg = cmd_cg.ExecuteReader();
            if (dr_cg.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr_cg);
                if (dt.Rows.Count > 0)
                {
                    ViewState["Comp_Group"] = dt;
                }
                gv_compgroup_type.DataSource = dt;
                gv_compgroup_type.DataBind();
            }
            d.con.Close();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

        string login_user = d.getsinglestring("select Login_id from pay_user_master where UNIT_FLAG = '1' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + unitcode + "'");
        if (login_user != "")
        {
            txt_unit_login_id.Text = login_user;
        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {

        if (ddl_branchStatus.SelectedValue == "1")
        {
            string branch = d.getsinglestring(" SELECT count(emp_code) FROM  pay_employee_master  WHERE  pay_employee_master.client_code = '" + ddlunitclient.SelectedValue + "'  AND pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND unit_code = '" + txt_unitcode.Text + "' AND (left_date IS NULL || left_date = '')");

            if (branch != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Left All Employee First')", true);
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('You Can't close your branch!!!')", true);

                return;
            }
        }
        if (gv_itemslist.Rows.Count >= 1)
        {
            if (chkcount() == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Designation Count ,Does Not Match Client Contract');", true);
                return;
            }
            else
            {
                UnitBAL ubl3 = new UnitBAL();
                string cmpcode = Session["comp_code"].ToString();
                string emp_name = Session["USERNAME"].ToString();
                string newdate = Convert.ToString(System.DateTime.Now);
                int result = 0;
                try
                {
                    d.operation("Delete From pay_branch_mail_details where client_code = '" + ddlunitclient.SelectedValue + "' and unit_code='" + txt_unitcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                    if (file_txt.Text == "")
                    {
                        file_txt.Text = "0";
                    }
                    int resins1 = 0;
                    string reporting = d.update_reporting_emp(Session["LOGIN_ID"].ToString());
                    //if (reporting == "")
                    //{
                    if (txt_material_area.Text == "")
                    {
                        txt_material_area.Text = "0";
                    }
                    if (txtbranch_cost_centre_code.Text == "")
                    {
                        txtbranch_cost_centre_code.Text = "0";
                    }
                    if (ddl_branchStatus.SelectedValue != "1")
                    {
                        txt_br_cose_date.Text = null;
                    }
                    result = ubl3.UnitUpdate(cmpcode, txt_unitcode.Text, txt_unitname.Text.ToString(), txtunitaddress1.Text.ToString(), txtunitaddress2.Text, txtunitcity.Text, ddl_state.SelectedItem.Text, txt_lattitude.Text.ToString(), txt_longitude.Text.ToString(), txt_area.Text.ToString(), txt_zone.SelectedValue, ddlunitclient.SelectedValue, file_txt.Text.ToString(), txtemailid.Text, txt_operationname.Text, txt_omobileno.Text, txt_oemailid.Text, txt_financename.Text, txt_fmobileno.Text, txt_femailid.Text, txt_locationname.Text, txt_lmobileno.Text, txt_lemailid.Text, txt_othername.Text, txt_othermobno.Text, txt_otheremailid.Text, ddl_designation.SelectedValue, txt_zone1.SelectedValue, txt_reason_updation.Text + " BY-" + Session["USERNAME"].ToString(), reporting, txt_pincode.Text, txt_adminname.Text, txt_adminmobileno.Text, txt_adminemailid.Text, ddl_location_heaad_title.SelectedValue.ToString(), ddl_operation_heaad_title.SelectedValue.ToString(), ddl_finance_heaad_title.SelectedValue.ToString(), ddl_admin_heaad_title.SelectedValue.ToString(), ddl_other_heaad_title.SelectedValue.ToString(), txt_clientbranchcode.Text, txt_opus_code.Text, txt_p_mob1.Text, txt_p_mob2.Text, txt_p_mob3.Text, txt_p_mob4.Text, txt_p_mob5.Text, txt_p_emailid1.Text, txt_p_emailid2.Text, txt_p_emailid3.Text, txt_p_emailid4.Text, txt_p_emailid5.Text, txt_p_bdate1.Text, txt_p_bdate2.Text, txt_p_bdate3.Text, txt_p_bdate4.Text, txt_p_bdate5.Text, txt_p_adate1.Text, txt_p_adate2.Text, txt_p_adate3.Text, txt_p_adate4.Text, txt_p_adate5.Text, txtbranch_cost_centre_code.Text, ddl_location_type.SelectedValue.ToString(), txt_pccode.Text, txt_disticitivecode.Text, ddl_branch_type.SelectedValue, txt_material_area.Text, ddl_branchStatus.SelectedValue, ddl_labour_office.SelectedValue, txt_budget_material.Text, txt_br_cose_date.Text, ddl_android_attendances_flag.SelectedValue.ToString(), ddl_service.SelectedValue, ddl_admin_expence.SelectedValue);

                    // fire extinguisher komal 21-07-2020 komal
                    //     fire_extinguisher_insert();
                    fire_extinguisher_function();

                    // fire extinguisher komal 21-07-2020 komal

                    //}
                    //else
                    //{
                    //    result = ubl3.UnitInsert(cmpcode, txt_unitcode.Text, txt_unitname.Text, txtunitaddress1.Text.ToString(), txtunitaddress2.Text, txtunitcity.Text, ddl_state.SelectedItem.Text, txt_lattitude.Text.ToString(), txt_longitude.Text.ToString(), txt_area.Text.ToString(), txt_zone.SelectedValue, ddlunitclient.SelectedValue, file_txt.Text.ToString(), txtemailid.Text, txt_operationname.Text, txt_omobileno.Text, txt_oemailid.Text, txt_financename.Text, txt_fmobileno.Text, txt_femailid.Text, txt_locationname.Text, txt_lmobileno.Text, txt_lemailid.Text, txt_othername.Text, txt_othermobno.Text, txt_otheremailid.Text, ddl_designation.SelectedValue, txt_zone1.SelectedValue, reporting, txt_pincode.Text, txt_adminname.Text, txt_adminmobileno.Text, txt_adminemailid.Text, ddl_location_heaad_title.SelectedValue.ToString(), ddl_operation_heaad_title.SelectedValue.ToString(), ddl_finance_heaad_title.SelectedValue.ToString(), ddl_admin_heaad_title.SelectedValue.ToString(), ddl_other_heaad_title.SelectedValue.ToString(), txt_clientbranchcode.Text, txt_opus_code.Text, txt_p_mob1.Text, txt_p_mob2.Text, txt_p_mob3.Text, txt_p_mob4.Text, txt_p_mob5.Text, txt_p_emailid1.Text, txt_p_emailid2.Text, txt_p_emailid3.Text, txt_p_emailid4.Text, txt_p_emailid5.Text, txt_p_bdate1.Text, txt_p_bdate2.Text, txt_p_bdate3.Text, txt_p_bdate4.Text, txt_p_bdate5.Text, txt_p_adate1.Text, txt_p_adate2.Text, txt_p_adate3.Text, txt_p_adate4.Text, txt_p_adate5.Text,txt_count.Text );
                    //}
                    string doc_no = UnitGridView.SelectedRow.Cells[1].Text;
                    int result_del = d1.operation("DELETE FROM pay_designation_count WHERE comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + doc_no + "' ");//delete command
                    foreach (GridViewRow row in gv_itemslist.Rows)
                    {
                        string lbl_srno1 = ((System.Web.UI.WebControls.Label)row.FindControl("lbl_srnumber")).Text;
                        string designation = row.Cells[2].Text;
                        int Count = int.Parse(row.Cells[3].Text);
                        int hours = int.Parse(row.Cells[4].Text);

                        string startdate = (row.Cells[5].Text);
                        string enddate = (row.Cells[6].Text);
                        string category = (row.Cells[8].Text);
                        resins1 = d.operation("INSERT INTO pay_designation_count(comp_code,CLIENT_CODE,STATE,DESIGNATION,UNIT_CODE,COUNT,CREATED_BY,CREATED_DATE,SR_NO, Hours,unit_start_date,unit_end_date,branch_status,category) VALUES('" + Session["comp_code"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + ddl_state.SelectedItem.Text + "','" + designation + "','" + txt_unitcode.Text + "','" + Count + "','" + Session["LOGIN_ID"].ToString() + "',now(),'" + lbl_srno1 + "'," + hours + ",str_to_date('" + startdate + "','%d/%m/%Y') ,str_to_date('" + enddate + "','%d/%m/%Y'),'" + ddl_branchStatus.SelectedValue + "','" + category + "')");
                    }
                    //  }
                    //else
                    //{
                    //    string doc_no = UnitGridView.SelectedRow.Cells[1].Text;
                    //    int result_del = d1.operation("DELETE FROM pay_designation_count WHERE comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + doc_no + "' ");//delete command
                    //}

                    d.con.Open();
                    try
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select sum(COUNT) from pay_designation_count where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
                        MySqlDataReader dr = cmd1.ExecuteReader();
                        if (dr.Read())
                        {
                            string count = dr.GetValue(0).ToString();
                            if (count == "") { count = "0"; }
                            txt_empcount.Text = count;
                            string count1 = txt_empcount.Text;
                            d.operation("update pay_unit_master set Emp_count='" + count1 + "' where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                            dr.Close();
                            d.con.Close();
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally { d.con.Close(); }
                    //      }
                    gv_itemslist.DataSource = null;
                    gv_itemslist.DataBind();

                    foreach (GridViewRow row in gv_emailsend.Rows)
                    {
                        d.operation("INSERT INTO pay_branch_mail_details (COMP_CODE,CLIENT_CODE,unit_code,head_type,head_name,head_email_id,state,mobileno)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + txt_unitcode.Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + ddl_state.SelectedValue + "','" + row.Cells[5].Text + "')");
                    }

                    if (ddlunitclient.SelectedValue == "RCPL")
                    {
                        if (check_comp_percent())
                        {
                            d1.operation("DELETE FROM pay_company_group WHERE comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + doc_no + "' ");//delete command
                            foreach (GridViewRow row in gv_compgroup_type.Rows)
                            {
                                Label lbl_compname = (Label)row.FindControl("lbl_compname");
                                string compname = (lbl_compname.Text);
                                Label lbl_cmp_gst = (Label)row.FindControl("lbl_cmp_gst");
                                string comp_gstno = (lbl_cmp_gst.Text);
                                Label lbl_comp_address = (Label)row.FindControl("lbl_comp_address");
                                string comp_address = (lbl_comp_address.Text);
                                TextBox txt_gvper = (TextBox)row.FindControl("txt_gvper");
                                string gst_per = txt_gvper.Text.ToString();

                                d.operation("INSERT INTO pay_company_group (comp_code,client_code,comp_name,Companyname_gst_no,gst_address,unit_code,`percent`)VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + compname + "','" + comp_gstno + "','" + comp_address + "','" + txt_unitcode.Text + "','" + gst_per + "')");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Company Percent Should be Equals To 100 !!')", true);
                            return;
                        }
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record updated successfully!!')", true);


                        txt_unit_password.Text = cmpcode + "gJiYRq5463";

                        if (txt_unit_password.Text != "")
                        {
                            //d.operation("delete from pay_user_master where unit_flag='1' and UNIT_CODE='" + txt_unitcode.Text + "'");
                            //   d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,FLAG,create_date,first_login,comp_code,UNIT_FLAG,UNIT_CODE) Values('" + txt_unit_login_id.Text + "','" + txt_unitname.Text + "','" + GetSha256FromString(txt_unit_password.Text) + "','A',now(),'0','" + Session["comp_code"].ToString() + "','1','" + txt_unitcode.Text + "')");

                            d.operation("update pay_user_master set LOGIN_ID='" + txt_unitcode.Text + "',USER_NAME='" + txt_unitname.Text + "',USER_PASSWORD='" + GetSha256FromString(txt_unit_password.Text) + "',FLAG='A',create_date=now(),first_login='0',comp_code='" + Session["comp_code"].ToString() + "',UNIT_FLAG='1',UNIT_CODE='" + txt_unitcode.Text + "', password_changed_date=now(),client_code='" + ddlunitclient.SelectedValue + "',client_zone='" + txt_zone1.SelectedValue + "',zone_region='" + txt_zone.SelectedValue + "' where login_id = '" + txt_unitcode.Text + "' and comp_code='" + cmpcode + "'");
                            send_email(Server.MapPath("~/User_Details.htm"), txtemailid.Text);
                        }
                        gv_itemslist.DataSource = null;
                        gv_itemslist.DataBind();
                        text_clear();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record updation failed!!')", true);
                        gv_itemslist.DataSource = null;
                        gv_itemslist.DataBind();
                        text_clear();
                    }

                }
                catch (Exception ee)
                {
                    throw ee;
                }
                finally
                {
                    loadclientgrid();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Click on Add Button to add Designation')", true);
            return;
        }

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        UnitBAL ubl4 = new UnitBAL();
        string cmpcode = Session["comp_code"].ToString();
        int result = 0;
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("Select EMP_CODE from pay_employee_master where comp_code='" + cmpcode + "' AND UNIT_CODE='" + txt_unitcode.Text + "'", d.con1);
            if (d.con1.State == ConnectionState.Open)
            {
                d.con1.Close();
                d.con1.Dispose();
                d.con1.ClearPoolAsync(d.con1);
            }
            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee against Unit Code transaction exist can not delete Unit');", true);
                text_clear();
            }
            else
            {
                result = ubl4.UnitDelete(cmpcode, txt_unitcode.Text);
                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deleted successfully!!')", true);
                    text_clear();
                    d.reset(this);
                    d.con1.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deletion failed !!')", true);
                    text_clear();
                }
            }
            d.con1.Close();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();
            loadclientgrid();
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        loadclientgrid();
        text_clear();
    }

    public void unit_code()
    {
        d.con1.Open();
        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(UNIT_CODE, 2, 5) AS UNSIGNED))+1 FROM  pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_unitcode.Text = "U001";
                }
                else
                {
                    int max_unitcode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_unitcode < 10)
                    {
                        txt_unitcode.Text = "U00" + max_unitcode;
                    }
                    else if (max_unitcode > 9 && max_unitcode < 100)
                    {
                        txt_unitcode.Text = "U0" + max_unitcode;
                    }
                    else if (max_unitcode > 99)
                    {
                        txt_unitcode.Text = "U" + max_unitcode;
                    }
                }
            }
            drmax.Close();
            d.con1.Close();
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    //protected void btnexporttoexcelunit_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=UnitMaster.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        //To Export all pages
    //        UnitGridView.AllowPaging = false;
    //        UnitBAL ubl1 = new UnitBAL();

    //        foreach (TableCell cell in UnitGridView.HeaderRow.Cells)
    //        {
    //            cell.BackColor = UnitGridView.HeaderStyle.BackColor;
    //        }
    //        foreach (GridViewRow row in UnitGridView.Rows)
    //        {



    //            foreach (TableCell cell in row.Cells)
    //            {
    //                if (row.RowIndex % 2 == 0)
    //                {
    //                    cell.BackColor = UnitGridView.AlternatingRowStyle.BackColor;
    //                }
    //                else
    //                {
    //                    cell.BackColor = UnitGridView.RowStyle.BackColor;
    //                }
    //                cell.CssClass = "textmode";
    //            }
    //        }

    //        UnitGridView.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .textmode { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    //}
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void UnitGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // e.Row.Cells[3].Text = "Landmark";
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(UnitGridView, "Select$" + e.Row.RowIndex);
        }
    }

    public void text_clear()
    {
        txt_unitname.Text = "";
        txt_material_area.Text = "0";
        ddl_branchStatus.SelectedValue = "0";
        txtunitaddress1.Text = "";
        txtunitaddress2.Text = "";
        txtunitcity.Text = "";
        txtemailid.Text = "";
        txt_pincode.Text = "";
        file_txt.Text = "";
        txt_operationname.Text = "";
        txt_othermobno.Text = "";
        txt_otheremailid.Text = "";
        txt_financename.Text = "";
        txt_fmobileno.Text = "";
        txt_femailid.Text = "";
        txt_locationname.Text = "";
        txt_lmobileno.Text = "";
        txt_lemailid.Text = "";
        txt_othername.Text = "";
        txt_othermobno.Text = "";
        txt_otheremailid.Text = "";
        if (txt_count.Text == "")
        {
            txt_count.Text = "0";
        }
        txt_reason_updation.Text = "";
        gv_itemslist.Visible = false;
        ddlunitclient.SelectedIndex = 0;
        txt_unitcode.Text = "";
        //ddl_state.SelectedIndex = 0;
        txt_lattitude.Text = "";
        txt_longitude.Text = "";
        txt_area.Text = "";
        //txt_gst_no.Text = "";
        txt_operationname.Text = "";
        txt_operationname.Text = "";
        txt_financename.Text = "";
        txt_locationname.Text = "";
        txt_othername.Text = "";
        txt_omobileno.Text = "";
        txt_fmobileno.Text = "";
        txt_lmobileno.Text = "";
        txt_othermobno.Text = "";
        txt_oemailid.Text = "";
        txt_femailid.Text = "";
        txt_lemailid.Text = "";
        txt_otheremailid.Text = "";
        txt_unit_login_id.Text = "";
        txtbranch_cost_centre_code.Text = "";
        ddl_state.SelectedIndex = 0;
        ddl_branch_type.SelectedIndex = 0;
        txt_opus_code.Text = "";
        //ddl_branch_type.SelectedValue = "Select";
        // ddl_designation.SelectedValue = "Select";
        txt_p_bdate1.Text = "";
        txt_p_bdate2.Text = "";
        txt_p_bdate3.Text = "";
        txt_p_bdate4.Text = "";
        txt_p_bdate5.Text = "";
        txt_p_adate1.Text = "";
        txt_p_adate2.Text = "";
        txt_p_adate3.Text = "";

        txt_p_adate4.Text = "";
        txt_p_adate5.Text = "";
        txt_adminname.Text = "";
        txt_adminemailid.Text = "";
        txt_adminmobileno.Text = "";
        txt_clientbranchcode.Text = "";
        txt_zone.DataSource = null;
        txt_zone.DataBind();
        txt_zone1.DataSource = null;
        txt_zone1.DataBind();
        txt_zone.SelectedValue = null;
        txt_zone1.SelectedValue = null;
        ddl_other_heaad_title.SelectedValue = "Select";
        ddl_admin_heaad_title.SelectedValue = "Select";
        ddl_finance_heaad_title.SelectedValue = "Select";
        ddl_location_heaad_title.SelectedValue = "Select";
        ddl_operation_heaad_title.SelectedValue = "Select";
        txt_hadename.Text = "";
        txt_head_emailid.Text = "";
        gv_emailsend.DataSource = null;
        gv_emailsend.DataBind();

        gridview_fire_extinguisher.DataSource = null;
        gridview_fire_extinguisher.DataBind();

        txt_disticitivecode.Text = "";
        txt_pccode.Text = "";
        ddl_labour_office.SelectedValue = "Select";
        txt_budget_material.Text = "0";
        ddl_android_attendances_flag.SelectedValue = "select";
        grd_approval_documents.DataSource = null;
        grd_approval_documents.DataBind();

    }

    protected void get_city_list(object sender, EventArgs e)
    {

        txtunitcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txtunitcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            gesignation_fill();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            labour_office();
            txtunitcity.Items.Insert(0, new ListItem("Select", ""));
        }
    }



    protected void Button5_Click(object sender, EventArgs e)
    {
        txt_lattitude.Text = Session["MAP_LATTITUDE"].ToString();
        txt_longitude.Text = Session["MAP_LONGITUDE"].ToString();
        txtunitaddress2.Text = Session["MAP_ADDRESS"].ToString();
        txt_area.Text = Session["MAP_AREA"].ToString();
    }

    protected void UnitGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            // UnitGridView.UseAccessibleHeader = false;
            UnitGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    protected void btn_approval_Click(object sender, EventArgs e)
    {
        d.operation("update pay_unit_master set approval=REPLACE(approval, '" + Session["LOGIN_ID"].ToString() + "', '') where comp_code = '" + Session["comp_code"].ToString() + "' and unit_code = '" + txt_unitcode.Text + "' and approval like '%" + Session["LOGIN_ID"].ToString() + "%'");

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT id from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and unit_code = '" + txt_unitcode.Text + "' and (approval = '' OR approval is null) order by id", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr_item1);
            int numberOfResults = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (numberOfResults != 1)
                {
                    d.operation("delete from pay_unit_master where id = " + dr["id"].ToString());
                    numberOfResults = numberOfResults - 1;
                }
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        loadclientgrid();
    }
    private void load_reporting_grdv()
    {
        try
        {
            d.con1.Open();
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT comp_code, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,id FROM pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "' ORDER BY unit_CODE", d.con1);
            DataSet DS1 = new DataSet();
            MySqlDataAdapter1.Fill(DS1);
            GridView1.DataSource = DS1;
            GridView1.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item = (int)GridView1.DataKeys[e.RowIndex].Value;
        d.operation("delete from pay_unit_master WHERE id=" + item);
        loadclientgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        load_fields(GridView1.Rows[e.NewEditIndex].Cells[2].Text, 2);
        btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true;
        load_reporting_grdv();
    }
    public string reason_updt(string reason, int type)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (type == 1)
        {
            int counter = 0;
            lbx_reason_updation.Items.Clear();
            while (reason.Contains("@#$%"))
            {
                if (!reason.Equals("@#$%"))
                {
                    string listitem = reason.Substring(0, reason.IndexOf("@#$%"));
                    lbx_reason_updation.Items.Add(new ListItem(++counter + ".  " + listitem));
                    reason = reason.Substring(reason.IndexOf("@#$%") + 4, reason.Length - (reason.IndexOf("@#$%") + 4));
                }
                else
                {
                    reason = "";
                }
            }
        }
        else if (type == 2)
        {
            for (int i = 0; i < lbx_reason_updation.Items.Count; i++)
            {
                if (i <= 9)
                {
                    sb.Append(lbx_reason_updation.Items[i].Text.Substring(3, lbx_reason_updation.Items[i].Text.Length - 3) + "@#$%");
                }
                else
                {
                    sb.Append(lbx_reason_updation.Items[i].Text.Substring(4, lbx_reason_updation.Items[i].Text.Length - 4) + "@#$%");
                }
            }
            sb.Append(txt_reason_updation.Text + "@#$%");
        }
        return sb.ToString();
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    private void send_email(string emailhtmlfile, string toaddress)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

            mail.From = new MailAddress("info@celtsoft.com");
            mail.To.Add(toaddress);
            // mail.CC.Add(tocc);
            mail.Subject = "User Details";
            d.con1.Open();
            try
            {
                MySqlCommand cmdmax1 = new MySqlCommand("SELECT USER_NAME,LOGIN_ID,USER_PASSWORD FROM pay_user_master WHERE UNIT_CODE = '" + txt_unitcode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                MySqlDataReader drmax = cmdmax1.ExecuteReader();
                while (drmax.Read())
                {
                    body = body.Replace("{Name}", drmax.GetValue(0).ToString());
                    body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
                    body = body.Replace("{Password}", "Admin123");
                    //  body = "Admin123");
                    //mail.Body = tosubject;
                }
                drmax.Close();
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            mail.Body = body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }

        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false); }


    }
    protected void ddlunitclient1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UnitBAL ubl1 = new UnitBAL();
        //DataSet ds = new DataSet();
        //ds = ubl1.UnitSelect(Session["comp_code"].ToString(), ddlunitclient1.SelectedValue);
        //UnitGridView.DataSource = ds.Tables[0];
        //UnitGridView.DataBind();

        lnk_freecount();
        UnitGridView.Visible = true;
        btn_add.Visible = true;

        try
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
            DataSet ds = new DataSet();
            //MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE,pay_unit_master.client_code,STATE_NAME,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, unit_gst_no  FROM pay_unit_master  where comp_code = '" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient1.SelectedValue.ToString() + "' and (approval='' || approval is null) ", d.con1);
            // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE,(pay_client_master.CLIENT_NAME) as client_code,STATE_NAME,Concat(STATE_NAME,'-',UNIT_NAME,'-',UNIT_ADD1) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, unit_gst_no,Emp_count  FROM pay_unit_master  INNER JOIN pay_client_master ON pay_unit_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code where pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "' and (pay_unit_master.approval='' || pay_unit_master.approval is null) ", d.con1);
            // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,UNIT_CODE,client_code,STATE_NAME,UNIT_NAME,UNIT_ADD1,UNIT_ADD2,UNIT_CITY, IFNULL(Emp_count,0) as 'Emp_count', IFNULL((Emp_count- employee_assign),0) as 'File_No' FROM (SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE,(pay_client_master.CLIENT_NAME) AS 'client_code',STATE_NAME, CONCAT(STATE_NAME, '-', UNIT_NAME, '-', UNIT_ADD1) AS 'UNIT_NAME',UNIT_ADD1,UNIT_ADD2,UNIT_CITY,Emp_count,total_employee,(total_employee - SUM(emp_count)) AS 'diff',(SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE unit_code = pay_unit_master.unit_code AND comp_code = '" + Session["comp_code"].ToString() + "'  AND LEFT_DATE is NUll and Employee_type = 'Permanent') AS 'employee_assign'  FROM pay_unit_master INNER JOIN pay_client_master ON pay_unit_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' and pay_unit_master.client_code='" + ddlunitclient1.SelectedValue + "' GROUP BY pay_unit_master.unit_code) AS branch_grid", d.con1);
            //  MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code, UNIT_CODE, client_code, STATE_NAME, UNIT_NAME, UNIT_ADD1, UNIT_ADD2, UNIT_CITY, IFNULL(Emp_count, 0) AS 'Emp_count', IFNULL((Emp_count - employee_assign), 0) AS 'File_No' FROM (SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE, (pay_client_master.CLIENT_NAME) AS 'client_code', pay_unit_master.STATE_NAME AS 'STATE_NAME', CONCAT(pay_unit_master.STATE_NAME, '-', pay_unit_master.UNIT_NAME, '-', pay_unit_master.UNIT_ADD1) AS 'UNIT_NAME', UNIT_ADD1, UNIT_ADD2, UNIT_CITY, Emp_count, total_employee, (total_employee - SUM(emp_count)) AS 'diff', (SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE unit_code = pay_unit_master.unit_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Employee_type != 'Reliever' AND LEFT_DATE IS NULL) AS 'employee_assign' FROM pay_unit_master INNER JOIN pay_client_master ON pay_unit_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code INNER JOIN pay_client_state_role_grade ON pay_unit_master.CLIENT_CODE = pay_client_state_role_grade.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.UNIT_CODE = pay_client_state_role_grade.UNIT_CODE  WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code='" + ddlunitclient1.SelectedValue + "'  AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") GROUP BY pay_unit_master.unit_code) AS branch_grid", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code, UNIT_CODE, client_code, STATE_NAME, UNIT_NAME, UNIT_ADD1, UNIT_ADD2, UNIT_CITY, IFNULL(Emp_count, 0) AS 'Emp_count', IFNULL((Emp_count - employee_assign), 0) AS 'File_No',branch_status FROM (SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE, (pay_client_master.CLIENT_NAME) AS 'client_code', pay_unit_master.STATE_NAME AS 'STATE_NAME', CONCAT(pay_unit_master.STATE_NAME, '-', pay_unit_master.UNIT_NAME, '-', pay_unit_master.UNIT_ADD1) AS 'UNIT_NAME', UNIT_ADD1, UNIT_ADD2, UNIT_CITY, Emp_count, total_employee, (total_employee - SUM(emp_count)) AS 'diff',   CASE WHEN `branch_status` = 0 THEN 'Active' ELSE 'Close' END AS 'branch_status',(SELECT COUNT(EMP_CODE) FROM pay_employee_master WHERE  `pay_unit_master`.`branch_status` = '0' and unit_code = pay_unit_master.unit_code AND comp_code = '" + Session["COMP_CODE"].ToString() + "' AND Employee_type != 'Reliever' AND LEFT_DATE IS NULL) AS 'employee_assign' FROM pay_unit_master INNER JOIN pay_client_master ON pay_unit_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code INNER JOIN pay_client_state_role_grade ON pay_unit_master.CLIENT_CODE = pay_client_state_role_grade.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code AND pay_unit_master.UNIT_CODE = pay_client_state_role_grade.UNIT_CODE  WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code='" + ddlunitclient1.SelectedValue + "'  AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") GROUP BY pay_unit_master.unit_code) AS branch_grid", d.con1);
            d.con1.Open();
            adp.Fill(ds);
            Panel5.Visible = true;
            UnitGridView.DataSource = ds.Tables[0];
            UnitGridView.DataBind();
            // text_Clear();
            // Panel5.Visible = true;
            UnitGridView.Visible = true;
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void ddl_head_emailid(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        txt_hadename.Text = "";
        txt_head_emailid.Text = "";
        if (ddl_sendemail_type.SelectedValue == "Location_Head")
        {

            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select LocationHead_Name, LocationHead_Emailid ,LocationHead_mobileno from pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and unit_code='" + txt_unitcode.Text + "' and client_code='" + ddlunitclient.SelectedValue + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_hadename.Text = dr[0].ToString();
                    txt_head_emailid.Text = dr[1].ToString();
                    txt_mobileno.Text = dr[2].ToString();

                }
                dr.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
        if (ddl_sendemail_type.SelectedValue == "Operation_Head")
        {

            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select OperationHead_Name, OperationHead_EmailId,OperationHead_Mobileno from pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and unit_code='" + txt_unitcode.Text + "' and client_code='" + ddlunitclient.SelectedValue + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_hadename.Text = dr[0].ToString();
                    txt_head_emailid.Text = dr[1].ToString();
                    txt_mobileno.Text = dr[2].ToString();

                }
                dr.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        if (ddl_sendemail_type.SelectedValue == "Finance_Head")
        {

            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select FinanceHead_Name, FinanceHead_EmailId,FinanceHead_Mobileno from pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and unit_code='" + txt_unitcode.Text + "' and client_code='" + ddlunitclient.SelectedValue + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_hadename.Text = dr[0].ToString();
                    txt_head_emailid.Text = dr[1].ToString();
                    txt_mobileno.Text = dr[2].ToString();

                }
                dr.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
        if (ddl_sendemail_type.SelectedValue == "Admin_Head")
        {

            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select adminhead_name, adminhead_email,adminhead_mobile from pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and unit_code='" + txt_unitcode.Text + "' and client_code='" + ddlunitclient.SelectedValue + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_hadename.Text = dr[0].ToString();
                    txt_head_emailid.Text = dr[1].ToString();
                    txt_mobileno.Text = dr[2].ToString();

                }
                dr.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        if (ddl_sendemail_type.SelectedValue == "Other_Head")
        {

            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select OtherHead_Name, OtherHead_Emailid,OtherHead_Monileno from pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and unit_code='" + txt_unitcode.Text + "' and client_code='" + ddlunitclient.SelectedValue + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_hadename.Text = dr[0].ToString();
                    txt_head_emailid.Text = dr[1].ToString();
                    txt_mobileno.Text = dr[2].ToString();

                }
                dr.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void lnk_add_emailid_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        gv_emailsend.Visible = true;
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("head_type");
        dt.Columns.Add("head_name");
        dt.Columns.Add("head_email_id");
        dt.Columns.Add("mobileno");


        int rownum = 0;
        for (rownum = 0; rownum < gv_emailsend.Rows.Count; rownum++)
        {
            if (gv_emailsend.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["head_type"] = gv_emailsend.Rows[rownum].Cells[2].Text;
                dr["head_name"] = gv_emailsend.Rows[rownum].Cells[3].Text;
                dr["head_email_id"] = gv_emailsend.Rows[rownum].Cells[4].Text;
                dr["mobileno"] = gv_emailsend.Rows[rownum].Cells[5].Text;

                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();


        dr["head_type"] = ddl_sendemail_type.SelectedValue;

        dr["head_name"] = txt_hadename.Text;
        dr["head_email_id"] = txt_head_emailid.Text;
        dr["mobileno"] = txt_mobileno.Text;
        dt.Rows.Add(dr);
        gv_emailsend.DataSource = dt;
        gv_emailsend.DataBind();

        ViewState["emailtable"] = dt;

        ddl_sendemail_type.SelectedValue = "Select";
        txt_hadename.Text = "";
        txt_head_emailid.Text = "";
        txt_mobileno.Text = "";
    }

    protected void gv_statewise_gst_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnk_remove_mail_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["emailtable"] != null)
        {
            DataTable dt = (DataTable)ViewState["emailtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["emailtable"] = dt;
            gv_emailsend.DataSource = dt;
            gv_emailsend.DataBind();
        }
    }

    protected void gv_start_end_date()
    {
        try
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(" SELECT DISTINCT(unit_start_date) AS 'start_date', (unit_end_date) AS 'end_date', (`client_name`) AS 'Client_name', CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master 	 WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME)  AS 'Unit_Name' FROM   `pay_designation_count`     INNER JOIN `pay_unit_master` ON `pay_designation_count`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_designation_count`.`comp_code` = `pay_unit_master`.`comp_code`     INNER JOIN `pay_client_master` ON `pay_designation_count`.`client_code` = `pay_client_master`.`client_code` AND `pay_designation_count`.`comp_code` = `pay_client_master`.`comp_code` WHERE   `pay_designation_count`.`comp_code` = '" + Session["comp_code"].ToString() + "'     AND (CURDATE() > unit_end_date - 7)     AND `pay_designation_count`.`unit_end_date` != 'NULL'", d.con);
            //  MySqlDataAdapter adp = new MySqlDataAdapter(" select distinct(start_date) as 'start_date',(end_date) as 'end_date', (client_name) as 'Client_name',unit_name as 'Unit_Name' from pay_client_master inner join pay_designation_count on pay_client_master.client_code=pay_designation_count.client_code and  pay_client_master.comp_code=pay_designation_count.comp_code inner join pay_unit_master on pay_unit_master.unit_code=pay_designation_count.unit_code and  pay_unit_master.comp_code=pay_designation_count.comp_code    where pay_unit_master.comp_code='" + Session["comp_code"] + "'  and (CURDATE()> end_date-7) and pay_designation_count.start_date!='' and pay_unit_master.unit_code is not null", d.con);
            d.con.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            gv_start_end_date_details.DataSource = dt;
            gv_start_end_date_details.DataBind();
            d.con.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
        }
    }



    protected void gv_start_end_date_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_start_end_date_details.UseAccessibleHeader = false;
            gv_start_end_date_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    public void client_list()
    {
        //d.con1.Close();
        //System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ")", d.con1);
        //d.con1.Open();
        //try
        //{
        //    cmd_item.Fill(dt_item);
        //    if (dt_item.Rows.Count > 0)
        //    {
        //        ddl_unit_client.DataSource = dt_item;
        //        ddl_unit_client.DataTextField = dt_item.Columns[1].ToString();
        //        ddl_unit_client.DataValueField = dt_item.Columns[0].ToString();
        //        ddl_unit_client.DataBind();


        //    }
        //    dt_item.Dispose();
        //    d.con1.Close();
        //    ddl_unit_client.Items.Insert(0, "Select");

        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con1.Close();

        //}

    }

    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gv_head_type_files_PreRender(object sender, EventArgs e)
    {

    }

    protected void gv_head_type_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gv_emailsend_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_emailsend.UseAccessibleHeader = false;
            gv_emailsend.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void labour_office()
    {
        ddl_labour_office.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_labour_office where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            //  ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "CallMyFunction", "unblock()", true);
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_labour_office.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
            // gesignation_fill();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            ddl_labour_office.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }

    //suraj start
    protected void lnk_freecount()
    {
        try
        {
            free_count = "0";
            ViewState["free_count"] = "";
            gv_free_count.DataSource = null;
            gv_free_count.DataBind();

            MySqlDataAdapter cmd1 = new MySqlDataAdapter("SELECT client_name,pay_unit_master.state_name,pay_unit_master.unit_name,pay_designation_count.designation,pay_designation_count.COUNT FROM pay_designation_count INNER JOIN pay_unit_master ON pay_designation_count.comp_code = pay_unit_master.comp_code AND pay_designation_count.unit_code = pay_unit_master.unit_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_unit_master.comp_code  AND  pay_client_master.client_code  =  pay_unit_master.client_code WHERE pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_master.client_code  = '" + ddlunitclient1.SelectedValue + "'  AND pay_unit_master.branch_status != 0 and pay_designation_count.unit_code is not null group by pay_designation_count.unit_code,pay_designation_count. designation", d.con);
            d.con.Open();
            // MySqlDataReader dr1 = cmd1.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
            cmd1.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ViewState["free_count"] = dt.Rows.Count.ToString();
                free_count = ViewState["free_count"].ToString();

                gv_free_count.DataSource = dt;
                gv_free_count.DataBind();

            }
            dt.Dispose();
            d.con.Close();

            //newpanel.Visible = true;
            // panel4.Visible = true;
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
    // suraj close

    protected bool check_comp_percent()
    {
        int per_count = 0;


        foreach (GridViewRow row in gv_compgroup_type.Rows)
        {
            TextBox txt_gvper = (TextBox)row.FindControl("txt_gvper");
            string gst_per = txt_gvper.Text.ToString();
            per_count = per_count + int.Parse(gst_per);
        }
        if (per_count == 100)
        {
            return true;
        }
        return false;
    }
    protected void des_date()
    {

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd_item = new MySqlCommand("select date_format(start_date,'%d/%m/%Y'),date_format(end_date,'%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and STATE='" + ddl_state.SelectedValue + "' and DESIGNATION='" + ddl_designation.SelectedValue + "' limit 1 ", d.con);
            MySqlDataReader dr = cmd_item.ExecuteReader();

            if (dr.Read())
            {
                txt_start_date.Text = dr.GetValue(0).ToString();
                txt_end_date.Text = dr.GetValue(1).ToString();
            }
            txt_end_date.ReadOnly = true;

        }
        catch (Exception ex) { }
        finally
        {

            d.con.Close();
        }

    }
    protected void btn_upload_approval_Click(object sender, EventArgs e)
    {
        hidtab.Value = "3";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        upload_documents_approval(document_file_approval, txt_document_approval.Text, "_doc");

    }

    private void upload_documents_approval(FileUpload document_file, string file_name, string file1)
    {
        if (document_file.HasFile)
        {

            string fileExt = System.IO.Path.GetExtension(document_file.FileName);
            if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".pdf")
            {
                string fileName = Path.GetFileName(document_file.PostedFile.FileName);
                document_file.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + Session["COMP_CODE"].ToString() + "_" + ddlunitclient.SelectedValue + "_" + txt_document_approval.Text.Replace(" ", "_") + fileExt, true);
                File.Delete(Server.MapPath("~/Images/") + fileName);

                d.operation("insert into pay_images (comp_code,client_code,`approval_unit_code`,description,file_name,start_date,end_date,created_by,created_date) values ('" + Session["COMP_CODE"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + txt_unitcode.Text + "','" + txt_document_approval.Text + "','" + Session["COMP_CODE"].ToString() + "_" + ddlunitclient.SelectedValue + "_" + txt_document_approval.Text.Replace(" ", "_") + fileExt + "',str_to_date('" + txt_from_date_approval.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date_approval.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }


        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Document !!')", true);
        }
        upload_approval_documents();

    }

    protected void upload_approval_documents()
    {

        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,client_code, description,concat('~/Images/',file_name) as Value,date_format(start_date,'%d/%m/%Y') as start_date,date_format(end_date,'%d/%m/%Y') as end_date,file_name FROM pay_images where comp_Code = '" + Session["COMP_CODE"].ToString() + "' and client_code= '" + ddlunitclient.SelectedValue + "' and `approval_unit_code` is not null ", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        grd_approval_documents.DataSource = DS1;
        grd_approval_documents.DataBind();
        txt_document_approval.Text = "";
        txt_from_date_approval.Text = "";
        txt_to_date_approval.Text = "";


    }

    protected void lnkDownload_approval_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }


    protected void grd_approval_documents_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_approval_documents.UseAccessibleHeader = false;
            grd_approval_documents.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    //protected void lnk_remove_bank_Click(object sender, EventArgs e)
    //{

    //    hidtab.Value = "3";
    //    try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
    //    catch { }

    //    GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
    //    d.operation("delete from pay_images where id = '" + grdrow.Cells[2].Text + "'");
    //    upload_approval_documents();


    //}
    protected void grd_company_files_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hidtab.Value = "3";
        int item = (int)grd_approval_documents.DataKeys[e.RowIndex].Value;
        string temp = d.getsinglestring("SELECT file_name FROM pay_images WHERE id=" + item);
        if (temp != "")
        {
            File.Delete(Server.MapPath("~/Images/") + temp);
        }
        d.operation("delete from pay_images WHERE id=" + item);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
        upload_approval_documents();


        if (reason_panel.Visible == true)
        {
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            btn_add.Visible = false;

        }
        else
        {

            btn_edit.Visible = false;
            btn_delete.Visible = false;
            btn_add.Visible = true;

        }
    }
    protected void grd_company_files_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        hidtab.Value = "0";
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
    }



    // fire extinguisher code komal 30-07-2020

    protected void fire_extinguisher_select()
    {
        gridview_fire_extinguisher.DataSource = null;
        gridview_fire_extinguisher.DataBind();

        System.Data.DataTable dt_id_gv = new System.Data.DataTable();
        MySqlDataAdapter cmd_id_gv = new MySqlDataAdapter("select id ,fire_ex_type  ,date_format(renewal_date,'%d/%m/%Y') as 'renewal_date', date_format(expiry_date,'%d/%m/%Y') as 'expiry_date',`weight_in_kg`,vender_name,vender_no,`fire_upload` from pay_fire_extinguisher where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient.SelectedValue + "' and unit_code = '" + txt_unitcode.Text + "'  ", d.con);


        cmd_id_gv.Fill(dt_id_gv);
        //appro_emp_legal = "0";
        if (dt_id_gv.Rows.Count > 0)
        {
            //ViewState["appro_emp_legal"] = dt_id_gv.Rows.Count.ToString();
            //appro_emp_legal = ViewState["appro_emp_legal"].ToString();

            gridview_fire_extinguisher.DataSource = dt_id_gv;
            gridview_fire_extinguisher.DataBind();


        }
        dt_id_gv.Dispose();


    }

    protected void lnk_fire_extinguisher_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";

        fire_extinguisher_insert();

        System.Data.DataTable dt_id_gv = new System.Data.DataTable();
        MySqlDataAdapter cmd_id_gv = new MySqlDataAdapter("select id ,fire_ex_type , date_format(expiry_date,'%d/%m/%Y') as 'expiry_date',`weight_in_kg`,vender_name,vender_no,`fire_upload` from pay_fire_extinguisher where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient.SelectedValue + "' and unit_code = '" + txt_unitcode.Text + "'  ", d.con);


        cmd_id_gv.Fill(dt_id_gv);
        //appro_emp_legal = "0";
        if (dt_id_gv.Rows.Count > 0)
        {
            //ViewState["appro_emp_legal"] = dt_id_gv.Rows.Count.ToString();
            //appro_emp_legal = ViewState["appro_emp_legal"].ToString();

            gridview_fire_extinguisher.DataSource = dt_id_gv;
            gridview_fire_extinguisher.DataBind();


        }
        dt_id_gv.Dispose();

        // txt_start_date_fr.Text = "";
        txt_end_date_fr.Text = "";
        txt_vender_no.Text = "";
        txt_vender_name.Text = "";
        ddl_weight_kg.SelectedValue = "Select";
        ddl_type_extinguisher.SelectedValue = "Select";

    }



    protected void fire_extinguisher_insert()
    {

        try
        {

            //int result_del = d1.operation("DELETE FROM pay_fire_extinguisher WHERE comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient.SelectedValue + "' AND UNIT_CODE ='" + txt_unitcode.Text + "' and state_name = '" + ddl_state.SelectedValue + "' ");//delete command

            string fname = null;

            string fileExt = System.IO.Path.GetExtension(txt_fire_report.FileName);
            if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".JPEG")
            {

                string id_fire = d.getsinglestring("select max(id)+1 from pay_fire_extinguisher");

                string fileName = Path.GetFileName(txt_fire_report.PostedFile.FileName);
                txt_fire_report.PostedFile.SaveAs(Server.MapPath("~/fire_extinguisher/") + fileName);
                fname = ddlunitclient.SelectedValue + "_" + txt_unitcode.Text + "_" + ddl_state.SelectedValue + "_" + ddl_type_extinguisher.SelectedValue + "_" + txt_vender_name.Text + "_" + id_fire + "_" + fileExt;
                File.Copy(Server.MapPath("~/fire_extinguisher/") + fileName, Server.MapPath("~/fire_extinguisher/") + fname, true);
                ResizeImage(Server.MapPath("~/fire_extinguisher/") + fname, 60, 60);
                File.Delete(Server.MapPath("~/fire_extinguisher/") + fileName);

                d.operation(" insert into pay_fire_extinguisher (comp_code,client_code,unit_code,state_name,fire_ex_type,expiry_date ,weight_in_kg,unit_Add,vender_name,vender_no,fire_upload) values('" + Session["comp_code"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + txt_unitcode.Text + "','" + ddl_state.SelectedItem.Text + "','" + ddl_type_extinguisher.SelectedValue + "',str_to_date('" + txt_end_date_fr.Text + "','%d/%m/%Y') ,'" + ddl_weight_kg.SelectedValue + "','" + txtunitaddress2.Text + "','" + txt_vender_name.Text + "','" + txt_vender_no.Text + "','" + ddlunitclient.SelectedValue + "_" + txt_unitcode.Text + "_" + ddl_state.SelectedValue + "_" + ddl_type_extinguisher.SelectedValue + "_" + txt_vender_name.Text + "_" + id_fire + "_" + fileExt + "') ");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and JPEG Files !!!')", true);
                return;

            }

        }
        catch (Exception ex) { throw ex; }
        finally { }


    }


    protected void fire_extinguisher_function()
    {

        try
        {


            foreach (GridViewRow row in gridview_fire_extinguisher.Rows)
            {
                int sr_number = int.Parse(((Label)row.FindControl("lbl_srnumber")).Text);
                // string fire_appli = row.Cells[3].Text;
                string ex_type = row.Cells[3].Text;
                //   string renewal_date = row.Cells[4].Text;
                string expiry_date = row.Cells[4].Text;
                string weight_in_kg = row.Cells[5].Text;
                string file_upload = row.Cells[6].Text;

                d.operation("insert into pay_fire_equipment_history (comp_code,client_code,unit_code,state_name,type_of_extinguisher,txt_end_date_fire,weight_in_kg,unit_add,vender_name,vender_no,fire_report)values('" + Session["comp_code"].ToString() + "','" + ddlunitclient.SelectedValue + "','" + txt_unitcode.Text + "','" + ddl_state.SelectedValue + "','" + ex_type + "', str_to_date('" + expiry_date + "','%d/%m/%Y') ,'" + weight_in_kg + "','" + txtunitaddress2.Text + "','" + txt_vender_name.Text + "','" + txt_vender_no.Text + "','" + file_upload + "') ");

                //  d.operation("Insert Into pay_designation_count (COMP_CODE,SR_NO,CLIENT_CODE,STATE,DESIGNATION,COUNT,HOURS,CREATED_BY,CREATED_DATE,start_date,end_date,location,category) VALUES ('" + Session["COMP_CODE"].ToString() + "'," + Convert.ToInt32(sr_number) + ",'" + txt_clientcode.Text + "','" + lbl_dsgstate.ToString() + "','" + designation.ToString() + "'," + Convert.ToInt32(em_count) + "," + Convert.ToInt32(wrk_hrs) + ",'" + Session["LOGIN_ID"].ToString() + "',now(),str_to_date('" + startdate + "','%d/%m/%Y') ,str_to_date('" + endsate + "','%d/%m/%Y'),'" + location + "','" + category + "')");
            }

            //ddl_fire_applicable.SelectedValue = "Not Applicable";
            //  txt_start_date_fr.Text = "";
            txt_end_date_fr.Text = "";
            txt_vender_no.Text = "";
            txt_vender_name.Text = "";
            ddl_weight_kg.SelectedValue = "Select";
            ddl_type_extinguisher.SelectedValue = "Select";
        }
        catch (Exception ex) { throw ex; }
        finally { }

    }


    protected void lnk_remove_fire_Click(object sender, EventArgs e)
    {
        hidtab.Value = "4";

        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

        int result = 0;
        result = d.operation("delete from pay_fire_extinguisher where id = '" + grdrow.Cells[2].Text + "'");

        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);

        }


        fire_extinguisher_select();


    }
    protected void gridview_fire_extinguisher_RowDataBound(object sender, GridViewRowEventArgs e)
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

        e.Row.Cells[2].Visible = false;


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl = "";
            if (dr["fire_upload"].ToString() != "")
            {

                imageUrl = "~/fire_extinguisher/" + dr["fire_upload"];
                (e.Row.FindControl("fire_upload") as System.Web.UI.WebControls.Image).ImageUrl = imageUrl;

            }
        }

    }



    protected void gridview_fire_extinguisher_PreRender(object sender, EventArgs e)
    {
        try
        {
            // UnitGridView.UseAccessibleHeader = false;
            gridview_fire_extinguisher.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }




    protected void btn_excel_fire_equipment_Click(object sender, EventArgs e)
    {
        string sql = null;
        string status = " ";

        sql = "select client_name ,unit_name ,pay_fire_extinguisher.state_name,Client_branch_code ,UNIT_ADD2 ,fire_ex_type ,weight_in_kg , expiry_date,pay_unit_master.zone,irdai_code,co_name from pay_fire_extinguisher  INNER JOIN `pay_client_master` ON `pay_fire_extinguisher`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_fire_extinguisher`.`client_code` = `pay_client_master`.`client_code` INNER JOIN `pay_unit_master` ON `pay_fire_extinguisher`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_fire_extinguisher`.`unit_code` = `pay_unit_master`.`unit_code` where pay_fire_extinguisher.comp_code = '" + Session["comp_code"].ToString() + "' and pay_fire_extinguisher.client_code = '" + ddlunitclient1.SelectedValue + "'";

        MySqlDataAdapter adp1 = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        adp1.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=CLIENTWISE_EXCEL_" + ddlunitclient1.SelectedItem.Text.Replace(" ", "_") + ".xls");



            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds, status);
            Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds, status);
            Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null, status);
            Repeater1.DataBind();

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Repeater1.RenderControl(htmlWrite);

            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(stringWrite.ToString());
            Response.Flush();
            Response.End();
            d.con1.Close();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
        }


    }


    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string status;

        int i = 0;
        //    double txt_amt = 0, cgst = 0, sgst = 0, igst = 0, manual_grand_total = 0, Total = 0;
        static int ctr;
        public MyTemplate1(ListItemType type, DataSet ds, string status)
        {
            this.type = type;
            this.ds = ds;
            this.status = status;

            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:


                    lc = new LiteralControl("<table border=1><table border=1><th bgcolor=yellow colspan=12 align=center> Fire Equipment Report </th><tr><th>SR. NO.</th><th>PC CODE</th><th>CLIENT NAME</th><th>IRDI CODE</th><th> ZONE</th><th> STATE NAME</th><th>BRANCH NAME</th><th> BRANCH ADDRESS</th><th>ASSET DISCRIPTION</th><th>WEIGHT IN KG </th><th>EXPIRY DATE </th><th>CO. NAME</th></tr>");

                    break;
                case ListItemType.Item:


                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["Client_branch_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["irdai_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["zone"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["UNIT_ADD2"] + "</td><td>" + ds.Tables[0].Rows[ctr]["fire_ex_type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["weight_in_kg"] + "</td><td>" + ds.Tables[0].Rows[ctr]["expiry_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["co_name"] + "</td></tr>");


                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }

    public void ResizeImage(string fileName, int width, int height)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(fileName))
        {
            using (System.Drawing.Image newImage = new Bitmap(image, width, height))
            {
                image.Dispose();
                newImage.Save(fileName);
            }
        }
    }


    protected void btn_upload_Click(object sender, EventArgs e)
    {
        upload_unit();

    }
    private void upload_unit()
    {

        try
        {
            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);

            string fname = null;

            string csvPath = "";
            if (FileUpload1.HasFile)
            {
                try
                {
                    if (fileExt.ToUpper() == ".CSV")
                    {
                        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        if (Extension == ".csv")
                        {
                            csvPath = "~/unit_upload/";
                            csvPath = Server.MapPath(csvPath + FileName);
                            FileUpload1.SaveAs(csvPath);
                            btn_Import_Click(csvPath, Extension, "Yes", FileName, fname);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully...');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload a valid csv file.');", true);
                        }
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('System Error - Please Try again....');", true);
                }
                finally
                {
                    File.Delete(csvPath);
                }
            }


        }
        catch (Exception ex) { throw ex; }
        finally { }

    }
    public void btn_Import_Click(string csvPath, string Extension, string IsHDR, string filename, string fname)
    {

        FileUpload1.SaveAs(csvPath);

        //Create a DataTable.  
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[15] 
            { 
        new DataColumn("Client Name", typeof(string)),  
        new DataColumn("State", typeof(string)),  
        new DataColumn("branch City", typeof(string)),  
        new DataColumn("branch name",typeof(string)),
        new DataColumn("Location",typeof(string)),
        new DataColumn("Address",typeof(string)),
        new DataColumn("Pincode",typeof(string)),
        new DataColumn("Latitude",typeof(string)),
        new DataColumn("Longitude",typeof(string)),
        new DataColumn("Area",typeof(string)),
        new DataColumn("File No",typeof(string)),
        new DataColumn("Designation",typeof(string)),
         new DataColumn("Designation Count",typeof(string)),
          new DataColumn("Designation End Date",typeof(string)),
           new DataColumn("Client Branch Code",typeof(string))
        });

        //Read the contents of CSV file.  
        string csvData = File.ReadAllText(csvPath);

        string csvdata1 = csvData;
        //Execute a loop over the rows.  
        foreach (string row in csvData.Split('\n'))
        {
            //csvdata1 = csvdata1.Replace(row + "\n", "");
            //csvdata1 = csvdata1.Replace(row + "\r", "");
            if (!string.IsNullOrEmpty(row))
            {
                dt.Rows.Add();
                int i = 0;
                string row1 = row.Replace("\r", "");
                //Execute a loop over the columns.  
                foreach (string cell in row1.Split(','))
                {

                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }
        }


        DataTable table2 = new DataTable("unit");

        table2.Columns.Add("Client_Name");
        table2.Columns.Add("State");
        table2.Columns.Add("Branch_Name");
        table2.Columns.Add("Comments");
        try
        {

            foreach (DataRow r in dt.Rows)
            {
                try
                {

                    unit_code();

                    int res = 0;

                    if (r[0].ToString().Trim() != "" && r[1].ToString().Trim() != "" && r[2].ToString().Trim() != "" && r[3].ToString().Trim() != "" && r[4].ToString().Trim() != "" && r[5].ToString().Trim() != "" && r[6].ToString().Trim() != "" && r[7].ToString().Trim() != "" && r[8].ToString().Trim() != "" && r[9].ToString().Trim() != "" && r[10].ToString().Trim() != "" && r[11].ToString().Trim() != "" && r[12].ToString().Trim() != "" && r[13].ToString().Trim() != "" && r[14].ToString().Trim() != "")
                    {
                        if (r[6].ToString().Trim().Length == 6 && r[6].ToString().Trim().All(char.IsDigit))
                        {
                            string days = "", month = "", year = "";
                            //string e_date = r[13].ToString();
                            //if (e_date.Length > 10) { e_date = e_date.Substring(0, 10); }
                            try
                            {
                                string j_date = r[13].ToString();
                                if (j_date.Length >= 10) { j_date = j_date.Substring(0, 10); }
                                if (j_date != "")
                                {
                                    days = j_date.Substring(0, 2);
                                    month = j_date.Substring(3, 2);
                                    year = j_date.Substring(6, 4);
                                }

                                string client_code = "", designation = "", branch_code = "", designation1 = "", client_count = "", unit_count = "", upload_count = "", state_name = "", category = "", hours = "", ucity = "", emp_count = "";
                                int c_count = 0, u_count = 0, up_count = 0, j = 0;
                                client_code = d.getsinglestring("SELECT DISTINCT (client_code) FROM pay_client_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_name='" + r[0].ToString() + "'");
                                if (client_code != "")
                                {
                                    state_name = d.getsinglestring("SELECT DISTINCT (state) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString().Trim() + "'");
                                    if (state_name != "")
                                    {
                                        ucity = d.getsinglestring("select city from pay_state_master where state_name = '" + r[1].ToString().Trim() + "' and city = '" + r[2].ToString().Trim() + "'");
                                    if (ucity != "")
                                    {

                                        int counter = 1;
                                        if (r[11].ToString().Trim().Contains("&&"))
                                        {
                                            string desig = r[11].ToString().Trim().Replace("&&", ",");
                                            List<string> names = new List<string>(desig.Split(','));
                                            foreach (string str in names)
                                            {
                                                designation = d.getsinglestring("SELECT DISTINCT (DESIGNATION) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and '" + str + "'  = DESIGNATION");
                                                counter = 1;
                                                if (designation == "")
                                                {
                                                    counter = 0;
                                                    break;
                                                }
                                            }
                                        }

                                        if (counter == 1)
                                        {
                                            if (r[12].ToString().Trim().All(char.IsDigit))
                                            {
                                                designation = d.getsinglestring("SELECT DISTINCT (DESIGNATION) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and '" + r[11].ToString() + "' like concat('%',DESIGNATION,'%')");
                                                if (designation != "")
                                                {
                                                    client_count = d.getsinglestring("SELECT sum(count) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and '" + r[11].ToString() + "' like concat('%',DESIGNATION,'%') and unit_code is  null group by designation");
                                                    unit_count = d.getsinglestring("SELECT sum(count) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and '" + r[11].ToString() + "' like concat('%',DESIGNATION,'%') and unit_code  is not null group by designation");
                                                    upload_count = r[12].ToString();
                                                    c_count = int.Parse(client_count); u_count = int.Parse(unit_count); up_count = int.Parse(upload_count);

                                                    j = u_count + up_count;
                                                    if (c_count >= j)
                                                    {
                                                        j = 0;
                                                        branch_code = d.getsinglestring("SELECT DISTINCT (unit_code) FROM pay_unit_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state_name= '" + r[1].ToString() + "'  and UNIT_NAME='" + r[3].ToString() + "'");
                                                        if (branch_code == "")
                                                        {
                                                            res = d.operation("insert into pay_unit_master (comp_code,client_code,state_name,UNIT_CITY,UNIT_NAME,UNIT_CODE,UNIT_ADD1,UNIT_ADD2,PINCODE,unit_Lattitude,unit_Longtitude,unit_distance,File_No,Client_branch_code)values('" + Session["comp_code"].ToString() + "','" + client_code + "','" + PropCase(r[1].ToString().Trim()) + "','" + PropCase(r[2].ToString().Trim()) + "','" + PropCase(r[3].ToString().Trim()) + "','" + txt_unitcode.Text.Trim() + "','" + PropCase(r[4].ToString().Trim()) + "' ,'" + PropCase(r[5].ToString().Trim()) + "','" + PropCase(r[6].ToString().Trim()) + "','" + PropCase(r[7].ToString().Trim()) + "','" + PropCase(r[8].ToString().Trim()) + "','" + PropCase(r[9].ToString().Trim()) + "','" + PropCase(r[10].ToString().Trim()) + "','" + PropCase(r[14].ToString().Trim()) + "')");

                                                            if (res > 0)
                                                            {
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Branches Added Successfully... !!!');", true);
                                                            }
                                                        }
                                                        if (r[11].ToString().Trim().Contains("&&"))
                                                        {
                                                            string desig = r[11].ToString().Trim().Replace("&&", ",");
                                                            List<string> names = new List<string>(desig.Split(','));
                                                            foreach (string str in names)
                                                            {
                                                                //"select category from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddlunitclient.SelectedValue + "' and STATE='" + ddl_state.SelectedValue + "' and `DESIGNATION`='" + ddl_designation.SelectedValue + "' and unit_code is null limit 1 "
                                                                hours = d.getsinglestring("SELECT hours FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + str + "'and unit_code is  null limit 1");
                                                                category = d.getsinglestring("SELECT category FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + str + "'and unit_code is  null limit 1");
                                                                designation1 = d.getsinglestring("SELECT DISTINCT (DESIGNATION) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + str + "'and unit_code is not null and unit_code='" + branch_code + "'");
                                                                if (designation1 == "")
                                                                {
                                                                    res = d.operation("INSERT INTO pay_designation_count(comp_code,CLIENT_CODE,STATE,DESIGNATION,UNIT_CODE,CREATED_BY,CREATED_DATE,count,category,hours,unit_start_date,unit_end_date) VALUES('" + Session["comp_code"].ToString() + "','" + client_code + "','" + PropCase(r[1].ToString().Trim()) + "','" + str + "','" + txt_unitcode.Text + "','" + Session["LOGIN_ID"].ToString() + "',now(),'" + up_count + "','" + category + "','" + hours + "',now(),str_to_date('" + year + "-" + month + "-" + days + "','%Y-%m-%d'))");
                                                                    j = 0; c_count = 0; up_count = 0;
                                                                    emp_count = d.getsinglestring("select sum(COUNT) from pay_designation_count where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                                                    d.operation("update pay_unit_master set Emp_count='" + emp_count + "' where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            hours = d.getsinglestring("SELECT hours FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + r[11].ToString() + "' and unit_code is  null limit 1");
                                                            category = d.getsinglestring("SELECT category FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + r[11].ToString() + "' and unit_code is  null limit 1");
                                                            designation1 = d.getsinglestring("SELECT DISTINCT (DESIGNATION) FROM pay_designation_count where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_code='" + client_code + "' and state= '" + r[1].ToString() + "'  and DESIGNATION = '" + r[11].ToString() + "'and unit_code is not null and unit_code='" + branch_code + "'");
                                                            if (designation1 == "")
                                                            {
                                                                res = d.operation("INSERT INTO pay_designation_count(comp_code,CLIENT_CODE,STATE,DESIGNATION,UNIT_CODE,CREATED_BY,CREATED_DATE,count,category,hours,start_date,end_date) VALUES('" + Session["comp_code"].ToString() + "','" + client_code + "','" + PropCase(r[1].ToString().Trim()) + "','" + designation + "','" + txt_unitcode.Text + "','" + Session["LOGIN_ID"].ToString() + "',now(),'" + up_count + "','" + category + "','" + hours + "',now(),str_to_date('" + year + "-" + month + "-" + days + "','%Y-%m-%d'))");
                                                                j = 0; c_count = 0; up_count = 0;
                                                                emp_count = d.getsinglestring("select sum(COUNT) from pay_designation_count where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                                                d.operation("update pay_unit_master set Emp_count='" + emp_count + "' where UNIT_CODE='" + txt_unitcode.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                                            }
                                                            else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Designation Already Added"); }
                                                        }

                                                    }
                                                    else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Designation Count Not Match"); }

                                                }
                                                else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Designation Not Added Client Master"); }

                                            }
                                            else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Designation Count Should be Numeric"); }
                                        }
                                        else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Designation not Present"); }
                                    }
                                    else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "City Name not Present."); }
                                    }
                                    else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "State Not Created"); }

                                }
                                else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Client Name Not Correct"); }
                            }
                            catch
                            {
                                table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), "Date format not proper for End date.");
                            }
                        }
                        else { table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), "Pin Number Should be Numeric and 6 Digits"); }
                    }
                    else
                    {
                        table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[3].ToString(), "Fill All Branch Details");

                    }
                    if (res > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Branch Added Successfully... !!!');", true);
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con.Close();
                }


            }

        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }

        if (table2.Rows.Count > 0)
        {
            DataSet ds = new DataSet("unit");
            ds.Tables.Add(table2);
            send_file(ds);
        }


    }



    private void send_file(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Branch_upload_issues.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds);
            Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds);
            Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null);
            Repeater1.DataBind();

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Repeater1.RenderControl(htmlWrite);

            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(stringWrite.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
        }
    }
    public class MyTemplate : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;

            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {

            switch (type)
            {
                case ListItemType.Header:

                    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th> CLIENT NAME </th><th> STATE NAME</th><th>Branch NAME</th><th>COMMENT</th>");

                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_NAME"] + " </td><td>" + ds.Tables[0].Rows[ctr]["State"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Branch_Name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Comments"] + " </td>");

                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
    public static string PropCase(string strText)
    {
        return new CultureInfo("en").TextInfo.ToTitleCase(strText.ToLower());
    }

    protected void rm_and_ae_check()
    {
        string rm_applicable = d.getsinglestring("Select R_and_M_service from pay_client_master where client_code='" + ddlunitclient.SelectedValue + "'");
        string ae_applicable = d.getsinglestring("Select administrative_expense from pay_client_master where client_code='" + ddlunitclient.SelectedValue + "'");
        if (rm_applicable == "1") { rm.Visible = true; } else { rm.Visible = false; }
        if (ae_applicable == "1") { ae.Visible = true; } else { ae.Visible = false; }
    }
}