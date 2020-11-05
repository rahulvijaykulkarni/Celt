using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;



public partial class Offerletter : System.Web.UI.Page
{
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    DAL d = new DAL();
    DAL d1 = new DAL();
    ReportDocument crystalReport = new ReportDocument();
    public MySqlDataReader drmax = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Offer Letter", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Offer Letter", Session["COMP_CODE"].ToString()) == "R")
        {

            btn_add.Visible = false;

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Offer Letter", Session["COMP_CODE"].ToString()) == "U")
        {

            btn_add.Visible = false;


        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Offer Letter", Session["COMP_CODE"].ToString()) == "C")
        {

        }

        if (!IsPostBack)
        {
            btn_print.Visible = false;
            //joining_letter.Visible = false;
            btn_delete.Visible = false;
           // Panel1.Visible = false;
            updateincrement.Visible = false;
            panelnew.Visible = false;
            appoimentpanel.Visible = false;
            AppointmentSearch.Visible = false;
            IncrementAmountpanel.Visible = false;
            Panel2.Visible = false;
            IncrementAmountpanel.Visible = false;
            incrementgvpanel.Visible = false;
            panelupdaeincrementamt.Visible = false;
            panelapposearch.Visible = false;

            Panel_Realiving.Visible = false;
            panel_Experiance_Letter.Visible = false;
            panel_relivingSearch.Visible = false;
            panem_exp_letter.Visible = false;
            txtdesination.Items.Clear();
            citydetails();
            //  get_city_list();
            Panel_terminate.Visible = false;
            Panel_terminate_gv.Visible = false;
            Panel_Uniform.Visible = false;
            try
            {
                MySqlCommand cmd_itemcode = new MySqlCommand("Select (GRADE_CODE) from pay_grade_master where comp_code='" + Session["comp_code"] + "'  ", d.con);
                d.conopen();
                MySqlDataReader dr_itemcode = cmd_itemcode.ExecuteReader();
                while (dr_itemcode.Read())
                {
                    txtdesination.Items.Add(dr_itemcode.GetValue(0).ToString());
                }
                dr_itemcode.Close();
                txtdesination.Items.Insert(0, "Select Item");
            }
            catch (Exception ex) { }
            finally { d.conclose(); }


            //  fill_warring_gv();
            panel_warring.Visible = false;
            panel_gv_earring.Visible = false;


            DataSet ds = new DataSet();
            ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH, STATE, CITY  FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            Auto_Increment();
            client_list();
        }
        // btn_new_terminal_Click(null, null);
        btn_new_warring_Click(null, null);
        btn_new_terminal_Click(null, null);
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_branch_warn.Items.Clear();

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_warn.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d2.con);
        d2.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_warn.DataSource = dt_item;
                ddl_branch_warn.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_warn.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_warn.DataBind();

            }
            dt_item.Dispose();
            d2.con.Close();

            ddl_branch_warn.Items.Insert(0, "ALL");


            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d2.con.Close();
        }


    }
    protected void ddl_client1_SelectedIndexChanged(object sender, EventArgs e)
    {


        //   ddl_unit_term.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_term.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_unit_term.DataSource = dt_item;
                ddl_unit_term.DataTextField = dt_item.Columns[0].ToString();
                ddl_unit_term.DataValueField = dt_item.Columns[1].ToString();
                ddl_unit_term.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();

            ddl_unit_term.Items.Insert(0, "ALL");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }
    protected void ddl_termination_emp(object sender, EventArgs e)
    {
        ddl_emp_warn.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select  EMP_NAME ,EMP_CODE from pay_employee_master where comp_code='" + Session["comp_code"] + "' ", d3.con);
        d3.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_emp_term.DataSource = dt_item;
                ddl_emp_term.DataTextField = dt_item.Columns[0].ToString();
                ddl_emp_term.DataValueField = dt_item.Columns[1].ToString();

                ddl_emp_term.DataBind();
            }
            dt_item.Dispose();
            d3.con.Close();
            ddl_emp_warn.Items.Insert(0, "Select");
            //  ddl_emp_warn.Items.Insert(1, "ALL");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
        }

      
    }
    protected void ddl_employee_termination(object sender, EventArgs e)
    {
        ddl_emp_warn.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select  EMP_NAME ,EMP_CODE from pay_employee_master where comp_code='" + Session["comp_code"] + "' ", d3.con);
        d3.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {

                ddl_emp_term.DataSource = dt_item;
                ddl_emp_term.DataTextField = dt_item.Columns[0].ToString();
                ddl_emp_term.DataValueField = dt_item.Columns[1].ToString();

                ddl_emp_term.DataBind();
            }
            dt_item.Dispose();
            d3.con.Close();
            ddl_emp_warn.Items.Insert(0, "Select");
            //  ddl_emp_warn.Items.Insert(1, "ALL");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d3.con.Close();
        }

       
    }
    protected void ddl_unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_emp_term.SelectedValue != "Select")
        {
            ddl_emp_warn.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select EMP_NAME, EMP_CODE   from pay_employee_master where comp_code='" + Session["comp_code"] + "' and  unit_code='" + ddl_branch_warn.SelectedValue + "' and LEFT_DATE IS NULL ", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_emp_warn.DataSource = dt_item;

                    ddl_emp_warn.DataTextField = dt_item.Columns[0].ToString();
                    ddl_emp_warn.DataValueField = dt_item.Columns[1].ToString();
                    ddl_emp_warn.DataBind();


                }
                dt_item.Dispose();
                d.con.Close();
                ddl_emp_warn.Items.Insert(0, "Select");
                //  ddl_emp_warn.Items.Insert(1, "ALL");
                //show_controls();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_branch_warn.Items.Clear();
            //  hide_controls();
        }
    }
    protected void ddl_unit1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_unit_term.SelectedValue != "Select")
        {
            ddl_employee_termination(null, null);
        }
        else
        {
            ddl_unit_term.Items.Clear();
            //  hide_controls();
        }
    }
    protected void btn_close_warn_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_new_warring_Click(object sender, EventArgs e)
    {


        //-----------------------------------------------
        try
        {
            d.con1.Open();
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(Id, 2, 4) AS UNSIGNED))+1 FROM  pay_warring_letter WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {
            }
            else if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_emp_id_warn.Text = "W0001";
                }
                else
                {
                    int max_srno = int.Parse(drmax.GetValue(0).ToString());
                    if (max_srno < 10)
                    {
                        txt_emp_id_warn.Text = "W000" + max_srno;
                    }
                    else if (max_srno > 9 && max_srno < 100)
                    {
                        txt_emp_id_warn.Text = "W00" + max_srno;
                    }
                    else if (max_srno > 99 && max_srno < 1000)
                    {
                        txt_emp_id_warn.Text = "W0" + max_srno;
                    }

                    else
                    {
                    }
                }
            }
            drmax.Close();
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }

    }
    protected void select_count(object sender, EventArgs e)
    {
        MySqlCommand cmd = new MySqlCommand("select count(EMP_CODE) from pay_warring_letter where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + ddl_emp_warn.SelectedValue + "' ", d.con);
        d.con.Open();
        MySqlDataReader drmax = cmd.ExecuteReader();

        if (drmax.Read())
        {
            txt_count.Text = drmax.GetValue(0).ToString();
            txt_count.ReadOnly = true;
            if (txt_count.Text == "3")
            {
             //   btn_move_to_terminate.Visible = true;
              //  btn_save_warn.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('All Ready Send Warnning Letter Three Time!!');", true);
               
            }
            else { txt_count.Text = "0"; }
           
        }
        drmax.Close();
        d.con.Close();
    }
    protected void gv_warring_selectedIndexChange(object sender, EventArgs e)
    {
   //   string id=  gv_warring.SelectedRow.Cells[0].Text;
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_warring.SelectedRow.FindControl("lbl_wsrnumber");
        string tds_Id = lblno.Text;


        MySqlCommand cmd = new MySqlCommand("select warring_date,count,Reason,client_code,unit_code,EMP_CODE from pay_warring_letter where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND ID='" + tds_Id + "' ", d.con);
        d.con.Open();
        MySqlDataReader drmax = cmd.ExecuteReader();
      
        if (drmax.Read())
        {
           
            txt_warn_date.Text = drmax.GetValue(0).ToString();
            txt_count.Text = drmax.GetValue(1).ToString();
            txt_resion_warn.Text = drmax.GetValue(2).ToString();
            client_warnning_letter(null, null);
            ddl_client_warn.SelectedValue = drmax.GetValue(3).ToString();
            ddl_client_SelectedIndexChanged(null, null);
            ddl_branch_warn.SelectedValue = drmax.GetValue(4).ToString();
            ddl_employee_termination(null, null);
            ddl_emp_warn.SelectedItem.Text = drmax.GetValue(5).ToString();
        }
        drmax.Close();
        d.con.Close();
    }
    protected void btn_save_warn_Click(object sender, EventArgs e)
    {

            d.con.Open();
            try
            {
                int res = 0;
                res = d.operation("insert into pay_warring_letter (Id,EMP_CODE,warring_date,count,Reason,comp_code,client_code,unit_code ) values('" + txt_emp_id_warn.Text + "','" + ddl_emp_warn.SelectedValue + "' ,str_to_date('" + txt_warn_date.Text + "','%d/%m/%Y'),'" + txt_count.Text + "','" + txt_resion_warn.Text + "','" + Session["comp_code"].ToString() + "','" + ddl_client_warn.SelectedValue + "','" + ddl_branch_warn .SelectedValue+ "') ");
                //  res = d.operation ("insert into pay_warring_letter ID = '" + txt_emp_id_warn.Text+ "', Emp_Name='" + ddl_emp_warn.SelectedValue + "',warring_date='" + txt_warn_date.Text + "',count='" + txt_count.Text + "',Reason='" + txt_resion_warn.Text + "''");
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('successfully Save!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save failed!!');", true);
                }

            }
            catch { }
            finally
            {
                string dowmloadname = "Warning Letter";
                string query = null;
                string UnitList = "";
                crystalReport.Load(Server.MapPath("~/warning_letter.rpt"));

                query = "SELECT Emp_Name, EMP_CURRENT_ADDRESS, EMP_CURRENT_CITY, EMP_CURRENT_STATE,DATE_FORMAT(warring_date,'%d/%m/%Y') as warring_date, Reason, count FROM pay_employee_master INNER JOIN pay_warring_letter ON pay_employee_master.EMP_CODE = pay_warring_letter.EMP_CODE WHERE pay_warring_letter.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.EMP_CODE = '" + ddl_emp_warn.SelectedValue + "' Order BY pay_warring_letter.id  desc limit 1 ";
                //Session["ReportMonthNo"] = "01";
                ReportLoad(query, dowmloadname);

                d.con.Close();

                fill_warring_gv();


                btn_new_warring_Click(null, null);
                txt_warring_clear();

            }

        }
   // }

    protected void btn_move_to_terminate_Click(object sender, EventArgs e)
    {

        int res = 0;
        res = d.operation("insert into pay_terminate_letter (Id,EMP_CODE,terminate_date,Reason,comp_code,client_code,unit_code ) values('" + txt_id_term.Text + "','" + ddl_emp_warn.SelectedValue + "' ,'" + txt_warn_date.Text + "','" + txt_resion_warn.Text + "','" + Session["comp_code"].ToString() + "','" + ddl_client_warn.SelectedValue + "','" + ddl_branch_warn .SelectedValue+ "') ");

        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Move To Terminate successfully!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Termination failed!!');", true);
        }
    }
    protected void txt_warring_clear()
    {
        ddl_emp_warn.SelectedValue = "Select";
        txt_warn_date.Text = "";
        txt_count.Text = "0";
        txt_resion_warn.Text = "";

    }

    protected void fill_warring_gv()
    {
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select ID ,(SELECT EMP_NAME FROM pay_employee_master WHERE pay_employee_master.EMP_CODE = pay_warring_letter.EMP_CODE) as 'EMP_CODE',warring_date,Reason,count from pay_warring_letter where comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            DataSet ds = new DataSet();
            MySqlDataAdapter adr = new MySqlDataAdapter(cmd);
            adr.Fill(ds);
            gv_warring.DataSource = ds.Tables[0];
            gv_warring.DataBind();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

    }

    protected void fill_termination_gv()
    {
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select ID ,(SELECT EMP_NAME FROM pay_employee_master WHERE pay_employee_master.EMP_CODE = pay_terminate_letter.EMP_CODE) as 'EMP_NAME',terminate_date,Reason from pay_terminate_letter where comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
            DataSet ds = new DataSet();
            MySqlDataAdapter adr = new MySqlDataAdapter(cmd);
            adr.Fill(ds);
            gv_terminal.DataSource = ds.Tables[0];
            gv_terminal.DataBind();
        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

    }

    protected void gv_terminal_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_terminal.SelectedRow.FindControl("lbl_wsrnumber1");
        string tds_Id = lblno.Text;

        MySqlCommand cmd = new MySqlCommand("select Id,client_code,unit_code, Emp_Code,terminate_date,Reason  from pay_terminate_letter where comp_code='" + Session["comp_code"].ToString() + "' and Id='" + tds_Id + "'  ", d.con1);

        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            termination_ddlclient(null, null);
            ddl_client_term.SelectedValue = dr.GetValue(1).ToString();
            ddl_client1_SelectedIndexChanged(null, null);
            ddl_unit_term.SelectedValue = dr.GetValue(2).ToString();
            ddl_employee_termination(null, null);
            ddl_emp_term.SelectedValue = dr.GetValue(3).ToString();
            txt_date_term.Text = dr.GetValue(4).ToString();
            txt_resion_term.Text = dr.GetValue(5).ToString();
            //   txt_resion_term.Text = dr.GetValue(4).ToString();

        }
        dr.Close();
        d.con1.Close();

    }
    protected void gv_warring_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_warring.SelectedRow.FindControl("lbl_wsrnumber");
        string tds_Id = lblno.Text;

        MySqlCommand cmd = new MySqlCommand("select Id, Emp_Code,warring_date,Reason,count from pay_warring_letter where comp_code='" + Session["comp_code"].ToString() + "' and Id='" + tds_Id + "'  ", d.con1);

        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            ddl_emp_warn.SelectedValue = dr.GetValue(1).ToString();
            txt_warn_date.Text = dr.GetValue(2).ToString();
            txt_resion_warn.Text = dr.GetValue(3).ToString();
            txt_count.Text = dr.GetValue(4).ToString();
        }
        dr.Close();
        d.con1.Close();

    }
    protected void client_warnning_letter(object sender, EventArgs e)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d1.con);
        d1.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_warn.DataSource = dt_item;
                ddl_client_warn.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_warn.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_warn.DataBind();
            }
            dt_item.Dispose();
            //  hide_controls();
            d1.con.Close();
            ddl_client_warn.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
    }

    protected void btn_warning_Letter_Click(object sender, EventArgs e)
    {
       

        panel_warring.Visible = true;
        panel_gv_earring.Visible = true;
        fill_warring_gv();
        gv_warring.Visible = true;
        IncrementAmountpanel.Visible = false;
        panelnew.Visible = false;
        appoimentpanel.Visible = false;
        Panel2.Visible = false;
        incrementsearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        panelupdaeincrementamt.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        Panel_Uniform.Visible = false;
        Panel_terminate.Visible = false;
        Panel_terminate_gv.Visible = false;
        txt_resion_warn.Text="";
            txt_count.Text="";
            txt_warn_date.Text = "";
                ddl_emp_warn.Items.Clear();
                ddl_branch_warn.Items.Clear();
                client_warnning_letter(null, null);

    }
    protected void gv_warring_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_warring, "Select$" + e.Row.RowIndex);

        }
    }
    protected void gv_terminal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_terminal, "Select$" + e.Row.RowIndex);

        }
    }

    protected void termination_ddlclient(object sender, EventArgs e)
    {
     ddl_client_term.Items.Clear();
        System.Data.DataTable dt_item1 = new System.Data.DataTable();
        MySqlDataAdapter cmd_item1 = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item1.Fill(dt_item1);
            if (dt_item1.Rows.Count > 0)
            {
                ddl_client_term.DataSource = dt_item1;
                ddl_client_term.DataTextField = dt_item1.Columns[0].ToString();
                ddl_client_term.DataValueField = dt_item1.Columns[1].ToString();
                ddl_client_term.DataBind();
            }
            dt_item1.Dispose();
            //  hide_controls();
            d.con.Close();

            ddl_client_term.Items.Insert(0, "Select");
             
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    
    }
    protected void btn_terminate_click(object sender, EventArgs e)
    {
        termination_ddlclient(null, null);
        Panel_terminate.Visible = true;
        Panel_terminate_gv.Visible = true;
        fill_termination_gv();
        panel_warring.Visible = false;
        panel_gv_earring.Visible = false;
        IncrementAmountpanel.Visible = false;
        panelnew.Visible = false;
        appoimentpanel.Visible = false;
        Panel2.Visible = false;
        incrementsearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        panelupdaeincrementamt.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        Panel_Uniform.Visible = false;
        ddl_unit_term.Items.Clear();
        ddl_emp_term.Items.Clear();
            txt_id_term.Text="";
            txt_date_term.Text = "";
            txt_resion_term.Text = "";
    }
    protected void btn_new_terminal_Click(object sender, EventArgs e)
    {


        //-----------------------------------------------
        try
        {
            d.con1.Open();
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(Id, 2, 4) AS UNSIGNED))+1 FROM  pay_terminate_letter WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {
            }
            else if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_id_term.Text = "T0001";
                }
                else
                {
                    int max_srno = int.Parse(drmax.GetValue(0).ToString());
                    if (max_srno < 10)
                    {
                        txt_id_term.Text = "T000" + max_srno;
                    }
                    else if (max_srno > 9 && max_srno < 100)
                    {
                        txt_id_term.Text = "T00" + max_srno;
                    }
                    else if (max_srno > 99 && max_srno < 1000)
                    {
                        txt_id_term.Text = "T0" + max_srno;
                    }

                    else
                    {
                    }
                }
            }
            drmax.Close();
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }
    }
    
    protected void btn_save_term_Click(object sender, EventArgs e)
    {
        MySqlCommand cmd = new MySqlCommand("select EMP_CODE from pay_terminate_letter where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and emp_code='"+ddl_emp_term.Text+"' ", d.con);
        d.con.Open();
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            string emp_code = dr.GetValue(0).ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Employee Is AlReady Terminated.');", true);
            text_clear();
        }
        else
        {
            d1.con.Open();
            try
            {
                int res = 0;
                //   string date= str_to_date('" + txt_date_term.Text + "','%d/%m/%Y') ;
                res = d.operation("insert into pay_terminate_letter (Id,EMP_CODE,terminate_date,Reason,comp_code,client_code,unit_code ) values('" + txt_id_term.Text + "','" + ddl_emp_term.SelectedValue + "' ,'" + txt_date_term.Text + "','" + txt_resion_term.Text + "','" + Session["comp_code"].ToString() + "','" + ddl_client_term.SelectedValue + "','" + ddl_unit_term .SelectedValue+ "') ");
                //  res = d.operation ("insert into pay_warring_letter ID = '" + txt_emp_id_warn.Text+ "', Emp_Name='" + ddl_emp_warn.SelectedValue + "',warring_date='" + txt_warn_date.Text + "',count='" + txt_count.Text + "',Reason='" + txt_resion_warn.Text + "''");
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('successfully Save!!');", true);
                    text_clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save failed!!');", true);
                }
            }
            catch { }
            finally
            {
                string dowmloadname = "Termination Letter";
                string query = null;
                string UnitList = "";
                crystalReport.Load(Server.MapPath("~/termination_letter.rpt"));
                query = "SELECT  Emp_Name,  EMP_CURRENT_ADDRESS,  EMP_CURRENT_CITY,  EMP_CURRENT_STATE,  DATE_FORMAT(terminate_date, '%d/%m/%Y') as warring_date,  Reason, (Select GRADE_DESC from pay_grade_master WHERE pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE and `pay_employee_master`.`COMP_CODE` = `pay_grade_master`.`COMP_CODE` ) as 'GRADE_CODE' FROM  pay_employee_master  INNER JOIN pay_terminate_letter ON pay_employee_master.EMP_CODE = pay_terminate_letter.EMP_CODE WHERE  pay_terminate_letter.comp_code = 'C01' AND pay_employee_master.EMP_CODE = '" + ddl_emp_term.SelectedValue + "' ORDER BY pay_terminate_letter.id ";
                //Session["ReportMonthNo"] = "01";
                ReportLoad(query, dowmloadname);
                d1.con.Close();
                btn_new_terminal_Click(null, null);
                fill_termination_gv();
             //   ddl_client_term.Items.Insert('0', new ListItem("Select"));
                ddl_emp_term.Items.Clear();
                txt_id_term.Text = "";
                ddl_unit_term.Items.Clear();
                txt_date_term.Text = "";
                txt_resion_term.Text = "";
                btn_new_terminal_Click(null, null);
                fill_termination_gv();
            }
        }
        d.con.Close();
    }
    public void text_clear()
    {
        ddl_emp_term.Items.Clear();
        txt_date_term.Text = "";
        txt_resion_term.Text = "";
        ddl_client_term.Items.Clear();
    }
    protected void citydetails()
    {
        ddlstate.Items.Clear();

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                //ddl_permstate.Items.Add(dr_item1[0].ToString());
                ddlstate.Items.Add(dr_item1[0].ToString());
                // ddl_location.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        ddlstate.Items.Insert(0, new ListItem("Select"));
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
        string tds_Id = lblno.Text;
        string com_code = Session["comp_code"].ToString();
        d.con1.Open();
        try
        {
            int res = 0;
            res = d.operation("DELETE FROM offer_letter WHERE comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + tds_Id + "' ");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Delete !!');", true);
                int res1 = 0;
                res1 = d.operation("DELETE FROM appointment_letter WHERE comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + tds_Id + "' ");

                if (res1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('successfully Delete !!');", true);
                }
               
                Auto_Increment();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Deletion failed !!');", true);

            }
        }
        catch (Exception ee)
        {

        }
        finally
        {
            DataSet ds = new DataSet();
            ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH, STATE, CITY  FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            d.con1.Close();
            btn_print.Visible = false;
            btn_delete.Visible = false;
            btn_move_to_app.Visible = false;
            btn_add.Visible = true;
            Auto_Increment();
            text_Clear();
        }

        d.con1.Close();
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {


        string com_code = Session["comp_code"].ToString();
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
        string tds_Id = lblno.Text;

        d.con1.Open();
        try
        {

            {


                string downloadname = "offer_letter";
                string query = null;


                crystalReport.Load(Server.MapPath("~/Offerletter.rpt"));

                query = " SELECT offer_letter.comp_code, EMP_NAME, DATE_FORMAT(offer_letter.JOINING_DATE, '%d/%m/%Y') as 'JOINING_DATE', ADDRESS, GRADE_CODE, offer_letter. SALARY_PER_MONTH , pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE, offer_letter.CITY , offer_letter.STATE , pay_company_master.COMPANY_CONTACT_NO , date_format(offer_letter.System_Date,'%d/%m/%Y') as 'System_Date'  FROM offer_letter INNER JOIN pay_company_master ON offer_letter.comp_code = pay_company_master.comp_code WHERE offer_letter.EMP_CODE='" + tds_Id + "' AND offer_letter.comp_code='" + Session["comp_code"].ToString() + "'";

                ///crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
                ReportLoad(query, downloadname);

                Response.End();


                //   Response.End();
                text_Clear();
                Auto_Increment();
            }


        }
        catch (Exception ee)
        {

        }
        finally
        {

            //   pcount = acount;

            DataSet ds = new DataSet();
            ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH, STATE, CITY  FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();

            d.con1.Close();
        }
    }
    protected void btn_move_to_appointment(object sender, EventArgs e)
    {

        string com_code = Session["comp_code"].ToString();
        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
        string tds_Id = lblno.Text;
        d.con1.Close();
        d.con1.Open();

        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select flag from offer_letter  where EMP_CODE ='" + tds_Id + "' and comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int res = Convert.ToInt32(dr.GetValue(0));
                if (res == 0)
                {
                    int result = 0, result1 = 0;

                    result = d.operation("Insert into appointment_letter (EMP_CODE,comp_code,EMP_NAME,JOINING_DATE,ADDRESS,GRADE_CODE,SALARY_PER_MONTH,STATE,CITY,System_Date) values ('" + tds_Id + "','" + Session["comp_code"] + "','" + txt_Emp_Name.Text + "',STR_TO_DATE('" + txt_joining_Date.Text + "','%d/%m/%Y'),'" + txt_address.Text + "','" + txt_grade_code.Text + "','" + txt_per_month_salary.Text + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "',now())");
                    result1 = d.operation("update offer_letter set flag='" + 1 + "' where EMP_CODE ='" + tds_Id + "' and comp_code='" + Session["comp_code"].ToString() + "'");

                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Move to appointment letter successfully!!');", true);
                        //();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record move to appointment letter failed...');", true);
                        //text_Clear();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record All Ready Move to appointment letter...');", true);
                }
                dr.Close();
                d.con.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record move to appointment letter Failed...')", true);
        }
        finally
        {
            DataSet ds = new DataSet();
            ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH , STATE, CITY FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();

            d.con1.Close();
            text_Clear();
            Auto_Increment();


        }

    }
    protected void SearchGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_emp_code.ReadOnly = true;
        // txt_emp_code.Enabled = true;
        txt_Emp_Name.ReadOnly = true;
        txt_joining_Date.ReadOnly = true;
        txt_address.ReadOnly = true;
        txt_grade_code.ReadOnly = true;
        txt_per_month_salary.ReadOnly = true;
        d.con1.Open();



        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
        string tds_Id = lblno.Text;



        //  MySqlCommand cmd2 = new MySqlCommand("SELECT Id,NAME,PAN_NO,US,RATE_OF_TDS,str_to_date(PAYMENT_DATE,'%d/%m/%Y'),str_to_date(DEDUCTION_DATE,'%d/%m/%Y'),AMOUNT,TAX_DEDUCTIBLE,TDS_PAID,str_to_date(TDS_PAYMENT_DATE,'%d/%m/%Y'),CHALLEN_NO,UNPAID_TDS,INTEREST,PAYABLE FROM pay_tds_calculation WHERE comp_code='" + Session["comp_code"].ToString() + "' and Id='" + tds_Id + "'  ", d.con);

        MySqlCommand cmd2 = new MySqlCommand("SELECT  EMP_CODE, EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS,GRADE_CODE,SALARY_PER_MONTH, STATE, CITY FROM offer_letter WHERE comp_code='" + Session["comp_code"].ToString() + "' and EMP_CODE='" + tds_Id + "'  ", d.con);
        d.conopen();
        MySqlDataReader dr = cmd2.ExecuteReader();
        if (dr.Read())
        {


            txt_emp_code.Text = dr.GetValue(0).ToString();
            txt_Emp_Name.Text = dr.GetValue(1).ToString();
            txt_joining_Date.Text = dr.GetValue(2).ToString();
            txt_address.Text = dr.GetValue(3).ToString();
            txt_grade_code.Text = dr.GetValue(4).ToString();
            txt_per_month_salary.Text = dr.GetValue(5).ToString();
            ddlstate.SelectedValue = dr.GetValue(6).ToString();
            get_city(dr.GetValue(6).ToString());
            ddlcity.SelectedValue = dr.GetValue(7).ToString();
        }
        btn_print.Visible = true;
        btn_delete.Visible = true;
        btn_add.Visible = false;
        btn_move_to_app.Visible = true;
        dr.Close();
        d.conclose();


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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.SearchGridView, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;
    }

    public void updateAmount()
    {

        double incremenyAmount = double.Parse(txtincrementamount.Text);
        double calAmount = (incremenyAmount - 1600);
        double monthly_basic = (calAmount * 0.3);
        double annuly_basic = monthly_basic * 12;
        double monthly_hra = monthly_basic * 0.5;
        double annuly_hra = monthly_hra * 12;
        double monthly_convence = 1600;
        double annual_convence = monthly_convence * 12;
        double monthly_pf = monthly_basic * 0.12;
        double annualy_pf = monthly_pf * 12;
        double monthly_gross = monthly_basic + monthly_convence + monthly_hra;
        double annualy_gross = monthly_gross * 12;
        double monthly_esic;
        if (monthly_gross <= 21000)
        {
            monthly_esic = monthly_gross * 0.0175;
        }
        else
        {
            monthly_esic = 0;
        }
        double annualy_esic = monthly_esic * 12;
        double monthly_Pt = 200;
        double annualy_Pt = monthly_Pt * 12;
        double monthly_net = monthly_gross - (monthly_esic + monthly_pf + monthly_Pt);
        double annualy_net = monthly_net * 12;

        //double head1 = double.Parse(txtlhead1.Text);
        // double head2 = double.Parse(txtlhead2.Text);
        double head3 = double.Parse(txtlhead3.Text);
        //  double head4 = double.Parse(txtlhead4.Text);
        double head5 = double.Parse(txtlhead5.Text);
        double head6 = double.Parse(txtlhead6.Text);
        double head7 = double.Parse(txtlhead7.Text);
        double head8 = double.Parse(txtlhead8.Text);
        double head9 = double.Parse(txtlhead9.Text);
        double head10 = double.Parse(txtlhead10.Text);
        double head11 = double.Parse(txtlhead11.Text);
        double head12 = double.Parse(txtlhead12.Text);
        double head13 = double.Parse(txtlhead13.Text);
        double head14 = double.Parse(txtlhead14.Text);
        double head15 = double.Parse(txtlhead15.Text);
        //   double otrate = double.Parse(txtotrate.Text);
        double incrementAmount = double.Parse(txtincrementamount.Text);


        d.con.Close();
        d1.con.Close();

        d.con.Open();
        string com_code = Session["comp_code"].ToString();

        string empcode = IEmp_code.Text;

        //   d.operation("UPDATE pay_employee_master SET E_HEAD01 ='" + monthly_basic + "',E_HEAD02='" + monthly_hra + "',E_HEAD03='" + head3 + "',E_HEAD04='" + monthly_convence + "',E_HEAD05='" + head5 + "',E_HEAD06='" + head6 + "',E_HEAD07='" + head7 + "',E_HEAD08='" + head8 + "',E_HEAD09='" + head9 + "',E_HEAD10='" + head10 + "',E_HEAD11='" + head11 + "',E_HEAD12='" + head12 + "',E_HEAD13='" + head13 + "',E_HEAD14='" + head14 + "',E_HEAD15='" + head15 + "',BASIC_PAY='" + otrate + "' WHERE comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + empcode + "' ");

        d.operation("UPDATE pay_employee_master SET GRADE_CODE='" + txtdesination.SelectedItem.Text + "',E_HEAD01 ='" + monthly_basic + "',E_HEAD02='" + monthly_hra + "',E_HEAD03='" + head3 + "',E_HEAD04='" + monthly_convence + "',E_HEAD05='" + head5 + "',E_HEAD06='" + head6 + "',E_HEAD07='" + head7 + "',E_HEAD08='" + head8 + "',E_HEAD09='" + head9 + "',E_HEAD10='" + head10 + "',E_HEAD11='" + head11 + "',E_HEAD12='" + head12 + "',E_HEAD13='" + head13 + "',E_HEAD14='" + head14 + "',E_HEAD15='" + head15 + "',EARN_TOTAL='" + incrementAmount + "' WHERE comp_code='" + Session["comp_code"].ToString() + "' AND EMP_CODE='" + empcode + "' ");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Updated...')", true);

    }


    private void ReportLoad(string query, string downloadfilename)
    {

        try
        {
            //btnsendemail.Visible = true;
            text_clear();
            //string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query,d.con);
            d.con.Open();
            MySqlDataReader sda = cmd.ExecuteReader();
            dt.Constraints.Clear();
            dt.Load(sda);
            d.con.Close();

            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();

            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadfilename);

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            //var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            //var script = string.Format("alert({0});", message);
            //ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "", script, true);
        }
        finally
        {
            d.con.Close();
            d1.con.Close();
            d.con1.Close();
        }
    }
    protected void incrementclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        string com_code = Session["comp_code"].ToString();
        d.con1.Close();
        d.con1.Open();

        try
        {
            int result = 0;

            result = d.operation("Insert into offer_letter (EMP_CODE,comp_code,EMP_NAME,JOINING_DATE,ADDRESS,GRADE_CODE,SALARY_PER_MONTH,STATE,CITY,System_Date,flag) values ('" + txt_emp_code.Text + "','" + Session["comp_code"] + "','" + txt_Emp_Name.Text + "',STR_TO_DATE('" + txt_joining_Date.Text + "','%d/%m/%Y'),'" + txt_address.Text + "','" + txt_grade_code.Text + "','" + txt_per_month_salary.Text + "','" + ddlstate.SelectedValue + "','" + ddlcity.SelectedValue + "',now(),'0')");


            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Offer Letter Save Successfully!!');", true);
                offer_text_clear();
                DataSet ds = new DataSet();
                ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH, STATE, CITY  FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
                // SearchGridView.DataSource = null;
                // SearchGridView.DataBind();
                SearchGridView.DataSource = ds.Tables[0];
                SearchGridView.DataBind();
                text_Clear();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Offer Letter Save failed...');", true);
                //text_Clear();
            }


            string downloadname = "offer_letter";
            string query = null;



            crystalReport.Load(Server.MapPath("~/Offerletter.rpt"));

            //query = " SELECT comp_code,EMP_NAME,JOINING_DATE,ADDRESS,GRADE_CODE,SALARY_PER_MONTH FROM offer_letter WHERE EMP_CODE='" + txt_emp_code.Text + "' AND comp_code='" + Session["comp_code"].ToString() + "'";
            query = "SELECT offer_letter.comp_code, EMP_NAME, DATE_FORMAT(offer_letter.JOINING_DATE, '%d/%m/%Y') as 'JOINING_DATE', ADDRESS, GRADE_CODE, offer_letter. SALARY_PER_MONTH , pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE, offer_letter.CITY , offer_letter.STATE , pay_company_master.COMPANY_CONTACT_NO , date_format(offer_letter.System_Date,'%d/%m/%Y') as 'System_Date'  FROM offer_letter INNER JOIN pay_company_master ON offer_letter.comp_code = pay_company_master.comp_code	 WHERE EMP_CODE='" + txt_emp_code.Text + "' AND offer_letter.comp_code='" + Session["comp_code"].ToString() + "'";
            ///crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
            ReportLoad(query, downloadname);
            // Auto_Increment();
            Response.End();
            text_Clear();



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Save Failed...')", true);
        }
        finally
        {
            text_Clear();
            DataSet ds = new DataSet();
            ds = d.getData("SELECT EMP_CODE,EMP_NAME ,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE ,ADDRESS ,GRADE_CODE ,SALARY_PER_MONTH, STATE, CITY  FROM offer_letter where comp_code='" + Session["comp_code"].ToString() + "'");
            // SearchGridView.DataSource = null;
            // SearchGridView.DataBind();
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            Auto_Increment();
            offer_text_clear();
            Auto_Increment();

            SearchGridView.Visible = true;
            Panel2.Visible = true;
        }

    }
    public void offer_text_clear()
    {
        // IEmp_code.Text = "";
        //IEmp_Name.Text = "";
        txt_Emp_Name.Text = "";
        txt_joining_Date.Text = "";
        txt_address.Text = "";
        txt_grade_code.Text = "";
        txt_per_month_salary.Text = "";
        ddlstate.SelectedIndex = 0;
    }
    public void text_Clear()
    {
        ddlcity.Items.Clear();
        txtlhead1.Text = "";
        txtlhead2.Text = "";
        txtlhead3.Text = "";
        txtlhead4.Text = "";
        txtlhead5.Text = "";
        txtlhead6.Text = "";
        txtlhead7.Text = "";
        txtlhead8.Text = "";
        txtlhead9.Text = "";
        txtlhead10.Text = "";
        txtlhead11.Text = "";
        txtlhead12.Text = "";
        txtlhead13.Text = "";
        txtlhead14.Text = "";
        txtlhead15.Text = "";
        ddlstate.SelectedValue = "Select";
        txt_per_month_salary.Text = "0";
        txt_grade_code.Text = "";
        txt_address.Text = "";
        txt_joining_Date.Text = "";
        txt_Emp_Name.Text = "";



    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        d.reset(this);
        text_Clear();
    }
    protected void btnclose1_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");
    }


    public void btn_incrementLetter_Click(object sender, EventArgs e)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_client_increment.DataSource = dt_item;
                ddl_client_increment.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_increment.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_increment.DataBind();
            }
            dt_item.Dispose();
            //  hide_controls();
            d.con.Close();
            ddl_client_increment.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        incrementgvpanel.Visible = true;


        panelapposearch.Visible = true;
        incrementsearch.Visible = true;

        appoimentpanel.Visible = false;
        panelnew.Visible = false;
        panelapposearch.Visible = false;
        appoimentpanel.Visible = false;
        panelnew.Visible = false;
        Panel2.Visible = true;
        panelapposearch.Visible = false;

        // incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        panelupdaeincrementamt.Visible = false;

        IncrementAmountpanel.Visible = true;
    }



    public void btn_OfferLetter_Click(object sender, EventArgs e)
    {
        offer_text_clear();
        IncrementAmountpanel.Visible = false;

        appoimentpanel.Visible = false;
        panelnew.Visible = true;
        Panel2.Visible = true;
        panelapposearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        panelupdaeincrementamt.Visible = false;

        Panel_terminate.Visible = false;
        Panel_terminate_gv.Visible = false;
        panel_warring.Visible = false;
        Panel_Uniform.Visible = false;

        citydetails();
        txt_emp_code.ReadOnly = true;
        // txt_emp_code.Enabled = true;
        txt_Emp_Name.ReadOnly = false;
        txt_joining_Date.ReadOnly = false;
        txt_address.ReadOnly = false;
        txt_grade_code.ReadOnly = false;
        txt_per_month_salary.ReadOnly = false;
        ddlcity.Items.Clear();
        btn_print.Visible = false;
            btn_delete.Visible = false;
            btn_move_to_app.Visible = false;
            btn_add.Visible = true;
    }

    public void btn_AppoinymentLetter_Click(object sender, EventArgs e)
    {
        panelapposearch.Visible = true;

        try
        {
            d.con1.Open();

            // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE' ,GRADE_CODE,state as 'WORKING_STATE',city as 'WORKING_CITY',SALARY_PER_MONTH as 'SALARY_PER_MONTH' from appointment_letter where ((appointment_letter.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (appointment_letter.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%')) AND appointment_letter.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this company!!');", true);
            }
            else
            {
                panelapposearch.Visible = true;
                AppointmentSearch.Visible = true;

            }
            panelapposearch.Visible = true;

            AppointmentSearch.DataSource = ds.Tables[0];
            AppointmentSearch.DataBind();
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }

        IncrementAmountpanel.Visible = false;
        panelnew.Visible = false;
        appoimentpanel.Visible = true;
        Panel2.Visible = false;
        incrementsearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        panelupdaeincrementamt.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        panel_warring.Visible = false;
        Panel_terminate.Visible = false;
        Panel_terminate_gv.Visible = false;
        Panel_Uniform.Visible = false;
    }

    public void btn_Experiance_Letter_Click(object sender, EventArgs e)
    {

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_client_experiance.DataSource = dt_item;
                ddl_client_experiance.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_experiance.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_experiance.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_client_experiance.Items.Insert(0, "Select");
           // ddl_branch_experiance.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        panel_Experiance_Letter.Visible = true;


        panem_exp_letter.Visible = true;
        IncrementAmountpanel.Visible = false;
        panelnew.Visible = false;
        appoimentpanel.Visible = false;
        Panel2.Visible = false;
        incrementsearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        panelupdaeincrementamt.Visible = false;
        Panel_Realiving.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = true;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = true;

        panel_warring.Visible = false;
        Panel_terminate.Visible = false;
        Panel_terminate_gv.Visible = false;
        Panel_Uniform.Visible = false;
        ddl_branch_experiance.Items.Clear();
    }
    public void btn_Reliving_Letter_Click(object sender, EventArgs e)
    {


        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_client_reliev.DataSource = dt_item;
                ddl_client_reliev.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_reliev.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_reliev.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_client_reliev.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        Panel_Realiving.Visible = true;


        panelapposearch.Visible = true;
        IncrementAmountpanel.Visible = false;
        panelnew.Visible = false;
        appoimentpanel.Visible = false;
        Panel2.Visible = false;
        incrementsearch.Visible = false;
        IncrementAmountpanel.Visible = false;
        incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        panelupdaeincrementamt.Visible = false;
        Panel_Realiving.Visible = true;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = true;
        panem_exp_letter.Visible = true;
        Panel_Realiving.Visible = true;
        panel_warring.Visible = false;
        Panel_terminate.Visible = false;
        Panel_terminate_gv.Visible = false;
        Panel_Uniform.Visible = false;
        ddl_branch_reliev.Items.Clear();

    }
    protected void btn_Increment_Click(object sender, EventArgs e)
    {




        incrementgvpanel.Visible = true;

        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT EMP_CODE,EMP_NAME ,GRADE_CODE,EARN_TOTAL as 'INCREMETN_AMOUNT'  FROM pay_employee_master  WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtincrementempcode.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtincrementempcode.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this Client!!');", true);
        }
        else
        {
            panelapposearch.Visible = true;
            incrementsearch.Visible = true;
        }
        panelapposearch.Visible = true;

        incrementsearch.DataSource = ds.Tables[0];
        incrementsearch.DataBind();
        d.con1.Close();

    }
    protected void btn_Experiancesearch_Letter_Click(object sender, EventArgs e)
    {
        panel_Experiance_Letter.Visible = true;

        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE' ,GRADE_CODE,LOCATION,date_format(BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE',EARN_TOTAL  FROM pay_employee_master  WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
        // MySqlDataAdapter adp = new MySqlDataAdapter("select increment_latter.comp_code,increment_latter.EMP_CODE,increment_latter.EMP_NAME,GRADE_CODE,increment_latter.YEAR,INCREMETN_AMOUNT , pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_PAN_NO, pay_company_master.COMPANY_TAN_NO, pay_company_master.COMPANY_WEBSITE,pay_company_master.COMPANY_CIN_NO  FROM increment_latter INNER JOIN pay_company_master  ON increment_latter.comp_code = pay_company_master.comp_code  WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);

        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this Client!!');", true);
        }
        else
        {
            // panelapposearch.Visible = true;
            //AppointmentSearch.Visible = true;
            panel_Experiance_Letter.Visible = true;
            panel_Experiance_Letter.Visible = true;


        }
        panem_exp_letter.Visible = true;

        gv_experianceletter.DataSource = ds.Tables[0];
        gv_experianceletter.DataBind();
        d.con1.Close();
    }
    protected void btn_realivingSearch_Click(object sender, EventArgs e)
    {
        Panel_Realiving.Visible = true;

        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',GRADE_CODE,LOCATION,date_format(BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE',EARN_TOTAL  FROM pay_employee_master  WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this Client!!');", true);
        }
        else
        {
            // panelapposearch.Visible = true;
            //AppointmentSearch.Visible = true;
            panel_relivingSearch.Visible = true;
            Panel_Realiving.Visible = true;

        }
        panelapposearch.Visible = true;

        gv_realiving.DataSource = ds.Tables[0];
        gv_realiving.DataBind();
        d.con1.Close();
    }
    protected void btn_APPOINMENTSEARCH_Click(object sender, EventArgs e)
    {
        panelapposearch.Visible = true;

        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("select comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE' ,GRADE_CODE,state as 'WORKING_STATE',city as 'WORKING_CITY',SALARY_PER_MONTH as 'SALARY_PER_MONTH'  from appointment_letter where 	((appointment_letter.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (appointment_letter.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%')) AND appointment_letter.comp_code='" + Session["comp_code"].ToString() + " ' ", d.con1);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this Client!!');", true);
        }
        else
        {
            panelapposearch.Visible = true;
            AppointmentSearch.Visible = true;

        }
        panelapposearch.Visible = true;

        AppointmentSearch.DataSource = ds.Tables[0];
        AppointmentSearch.DataBind();
        d.con1.Close();
    }
    private void ReportLoad2(string query2, string downloadfilename2)
    {

        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename2;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query2);
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            sda = cmd.ExecuteReader();
            dt.Load(sda);
            d.con.Close();

            //MySqlCommand cmd_item1 = new MySqlCommand("SELECT COMP_LOGO from pay_company_master where comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
            //d.con.Open();
            //MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            //if (dr_item1.Read())
            //{
            //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\" + dr_item1.GetValue(0).ToString());
            //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
            //    PictureObject TAddress1 = (PictureObject)crystalReport.ReportDefinition.Sections[0].ReportObjects["Picture1"];
            //    TAddress1.Width = 450;
            //    TAddress1.Height = 180;
            //}
            //else
            //{
            //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\logo.png");
            //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
            //}
            //dr_item1.Close();
            //d.con.Close();

            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            //  crystalReport.SetDatabaseLogon(@"Tanvi-Tej\Tanvi","");
            //Response.End();
            updateAmount();
            text_Clear();
            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadname);
            // updateAmount();

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            //var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            //var script = string.Format("alert({0});", message);
            //ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "", script, true);
        }
        finally
        {
            d.con.Close();
            d1.con.Close();
        }
    }

    protected void brnclear2_Click(object sender, EventArgs e)
    {
        text_Clear();
    }
    protected void btnincrementsave_Click(object sender, EventArgs e)
    {
        string com_code = Session["comp_code"].ToString();
        d.con1.Open();
        try
        {
            int res = 0;
            res = d.operation("INSERT INTO increment_latter(comp_code,EMP_CODE,EMP_NAME,GRADE_CODE,YEAR,INCREMETN_AMOUNT ) VALUES( '" + Session["comp_code"].ToString() + "','" + IEmp_code.Text + "','" + IEmp_Name.Text + "','" + txtdesination.Text + "','" + txtyear.Text + "','" + txtincrementamount.Text + "')");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('successfully Save!!');", true);



            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save failed!!');", true);

            }

            string downloadname2 = "Increment_Letter";
            string query2 = null;



            crystalReport.Load(Server.MapPath("~/IncrementLetterRpt.rpt"));
            query2 = "	select increment_latter.comp_code,increment_latter.EMP_CODE,increment_latter.EMP_NAME,increment_latter.GRADE_CODE,increment_latter.YEAR,INCREMETN_AMOUNT , pay_company_master.COMPANY_NAME as COMPANY_NAME, pay_company_master.ADDRESS1 as ADDRESS1, pay_company_master.ADDRESS2 as ADDRESS2, pay_company_master.CITY as CITY, pay_company_master.STATE as STATE, pay_company_master.COMPANY_PAN_NO as COMPANY_PAN_NO, pay_company_master.COMPANY_TAN_NO as COMPANY_TAN_NO, pay_company_master.COMPANY_WEBSITE as COMPANY_WEBSITE,pay_company_master.COMPANY_CIN_NO as COMPANY_CIN_NO  FROM increment_latter INNER JOIN pay_company_master  ON increment_latter.comp_code = pay_company_master.comp_code WHERE increment_latter.EMP_CODE='" + IEmp_code.Text + "' AND increment_latter.comp_code='" + Session["comp_code"].ToString() + "' ORDER BY increment_latter.id desc LIMIT 1";
            ///crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
            ReportLoad2(query2, downloadname2);
            Response.End();
            text_Clear();

        }
        catch (Exception ee)
        {

        }
        finally
        {



            MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_CODE,EMP_NAME,GRADE_CODE,YEAR,INCREMETN_AMOUNT FROM increment_latter WHERE comp_code='" + Session["comp_code"].ToString() + "'  ", d.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);

            updateincrementgv.DataSource = ds1.Tables[0];
            updateincrementgv.DataBind();
            text_Clear();
            panelupdaeincrementamt.Visible = true;
            d.con1.Close();
        }
    }



    protected void incrementsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        updateincrement.Visible = true;
        System.Web.UI.WebControls.Label lbl_Increment_CODE = (System.Web.UI.WebControls.Label)incrementsearch.SelectedRow.FindControl("lbl_increment_id");
        string l_Increment_CODE = lbl_Increment_CODE.Text;

        try
        {
            d.con1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT EMP_CODE,EMP_NAME ,GRADE_CODE,EARN_TOTAL,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15  FROM pay_employee_master WHERE  EMP_CODE='" + l_Increment_CODE + "' and comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                IEmp_code.Text = dr.GetValue(0).ToString();
                IEmp_Name.Text = dr.GetValue(1).ToString();
                txtdesination.Text = dr.GetValue(2).ToString();
                txtincrementamount.Text = dr.GetValue(3).ToString();

                txtlhead1.Text = dr.GetValue(4).ToString();
                txtlhead2.Text = dr.GetValue(5).ToString();
                txtlhead3.Text = dr.GetValue(6).ToString();
                txtlhead4.Text = dr.GetValue(7).ToString();
                txtlhead5.Text = dr.GetValue(8).ToString();
                txtlhead6.Text = dr.GetValue(8).ToString();
                txtlhead7.Text = dr.GetValue(9).ToString();
                txtlhead8.Text = dr.GetValue(10).ToString();
                txtlhead9.Text = dr.GetValue(11).ToString();
                txtlhead10.Text = dr.GetValue(12).ToString();
                txtlhead11.Text = dr.GetValue(13).ToString();
                txtlhead12.Text = dr.GetValue(14).ToString();
                txtlhead13.Text = dr.GetValue(15).ToString();
                txtlhead14.Text = dr.GetValue(16).ToString();
                txtlhead15.Text = dr.GetValue(17).ToString();
                // txtotratetxtotrate.Text = dr.GetValue(18).ToString();


            }
            dr.Close();
            cmd.Dispose();

        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }
    }

    public void calBasic()
    {
        double head1 = double.Parse(txtlhead1.Text);
        double head2 = double.Parse(txtlhead2.Text);
        double head3 = double.Parse(txtlhead3.Text);
        double head4 = double.Parse(txtlhead4.Text);
        double head5 = double.Parse(txtlhead5.Text);
        double head6 = double.Parse(txtlhead6.Text);
        double head7 = double.Parse(txtlhead7.Text);
        double head8 = double.Parse(txtlhead8.Text);
        double head9 = double.Parse(txtlhead9.Text);
        double head10 = double.Parse(txtlhead10.Text);
        double head11 = double.Parse(txtlhead11.Text);
        double head12 = double.Parse(txtlhead12.Text);
        double head13 = double.Parse(txtlhead13.Text);
        double head14 = double.Parse(txtlhead14.Text);
        double head15 = double.Parse(txtlhead15.Text);
        //  double otrate = double.Parse(txtotrate.Text);

        double IncrementAmount = double.Parse(txtincrementamount.Text);

        double Total = (head1 + head2 + head3 + head4 + head5 + head6 + head7 + head8 + head9 + head10 + head11 + head12 + head13 + head14 + head15);

        if (Total > IncrementAmount)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Amount Less Than Increment Amount!!');", true);

        }
        else { }

    }


    protected void txtincrementamount_TextChanged(object sender, EventArgs e)
    {
        double incremenyAmount = double.Parse(txtincrementamount.Text);
        double calAmount = (incremenyAmount - 1600);
        double monthly_basic = (calAmount * 0.3);
        double annuly_basic = monthly_basic * 12;
        double monthly_hra = monthly_basic * 0.5;
        double annuly_hra = monthly_hra * 12;
        double monthly_convence = 1600;
        double annual_convence = monthly_convence * 12;
        double monthly_pf = monthly_basic * 0.12;
        double annualy_pf = monthly_pf * 12;
        double monthly_gross = monthly_basic + monthly_convence + monthly_hra;
        double annualy_gross = monthly_gross * 12;
        double monthly_esic;
        if (monthly_gross <= 21000)
        {
            monthly_esic = monthly_gross * 0.0175;
        }
        else
        {
            monthly_esic = 0;
        }
        double annualy_esic = monthly_esic * 12;
        double monthly_Pt = 200;
        double annualy_Pt = monthly_Pt * 12;
        double monthly_net = monthly_gross - (monthly_esic + monthly_pf + monthly_Pt);
        double annualy_net = monthly_net * 12;

        txtlhead1.Text = monthly_basic.ToString();
        txtlhead2.Text = monthly_hra.ToString();
        txtlhead4.Text = monthly_convence.ToString();



    }
    protected void gv_Experiance_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)gv_experianceletter.SelectedRow.FindControl("lbl_realiving_id");
        //    string l_EMP_code1 = lbl_EMP_code.Text;
        //    string downloadname = "Experience_Letter_" + l_EMP_code1;
        //    string query = null;

        //    crystalReport.Load(Server.MapPath("~/experience_letter.rpt"));
        //    string temp = d1.getsinglestring("SELECT date_format(LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE' from pay_employee_master WHERE EMP_CODE='" + l_EMP_code1 + "'AND comp_code='" + Session["comp_code"].ToString() + "'");
        //    if (!temp.Equals(""))
        //    {
        //        query = "SELECT pay_employee_master.comp_code as comp_code,pay_employee_master.EMP_NAME as EMP_NAME,date_format(pay_employee_master.JOINING_DATE,'%d/%m/%Y')as JOINING_DATE,pay_employee_master.EMP_CODE as EMP_CODE,pay_employee_master.DESIGNATION_CODE as DESIGNATION_CODE,pay_grade_master.GRADE_DESC AS GRADE_CODE,date_format(LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE', pay_employee_master.REPORTING_TO as REPORTING_TO, pay_company_master.COMPANY_NAME as'COMPANY_NAME', pay_company_master.ADDRESS1 as ADDRESS1, pay_company_master.ADDRESS2 as ADDRESS2, pay_company_master.CITY as CITY, pay_company_master.STATE as STATE, pay_company_master.COMPANY_PAN_NO as COMPANY_PAN_NO, pay_company_master.COMPANY_TAN_NO as COMPANY_TAN_NO, pay_company_master.COMPANY_WEBSITE as 'COMPANY_WEBSITE',pay_company_master.COMPANY_CIN_NO as COMPANY_CIN_NO,pay_company_master.COMPANY_CONTACT_NO as COMPANY_CONTACT_NO FROM pay_employee_master INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON   pay_employee_master.GRADE_CODE =pay_grade_master.GRADE_CODE WHERE EMP_CODE='" + l_EMP_code1 + "'AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'";
        //        ReportLoad1(query, downloadname);
        //        Response.End();
        //    }



        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This employee is still working as per system.');", true);
        //    }
        //}
        //catch (Exception ex) { }
        //finally { d.con1.Close(); }

        try
        {
            System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)gv_experianceletter.SelectedRow.FindControl("lbl_realiving_id");
            string l_EMP_code1 = lbl_EMP_code.Text;
            string downloadname = "Experience_Letter_" + l_EMP_code1;
            string query = null;

            crystalReport.Load(Server.MapPath("~/experience_letter.rpt"));
            string temp = d1.getsinglestring("SELECT date_format(LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE' from pay_employee_master WHERE EMP_CODE='" + l_EMP_code1 + "'AND comp_code='" + Session["comp_code"].ToString() + "'");
            if (!temp.Equals(""))
            {
                query = "SELECT a.comp_code,a.EMP_NAME,date_format(a.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',a.EMP_CODE,pay_grade_master.GRADE_DESC AS GRADE_CODE, date_format(a.LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE',(select emp_name from pay_employee_master b where b.emp_code = a.REPORTING_TO) as REPORTING_TO, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2,pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE,pay_company_master.COMPANY_CONTACT_NO as COMPANY_CONTACT_NO FROM pay_employee_master a INNER JOIN pay_company_master ON a.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON a.GRADE_CODE =pay_grade_master.GRADE_CODE WHERE a.EMP_CODE='" + l_EMP_code1 + "'AND a.comp_code='" + Session["comp_code"].ToString() + "'";
                ReportLoad1(query, downloadname);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This employee is still working as per system.');", true);
            }
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }
    }

    protected void gv_realiving_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)gv_realiving.SelectedRow.FindControl("lbl_realiving_id");
            string l_EMP_code1 = lbl_EMP_code.Text;
            string downloadname = "Relieving_Letter_" + l_EMP_code1;
            string query = null;

            crystalReport.Load(Server.MapPath("~/relive_letter.rpt"));
            string temp = d1.getsinglestring("SELECT date_format(LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE' from pay_employee_master WHERE EMP_CODE='" + l_EMP_code1 + "'AND comp_code='" + Session["comp_code"].ToString() + "'");
            if (!temp.Equals(""))
            {
                query = "SELECT a.comp_code,a.EMP_NAME,date_format(a.JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',a.EMP_CODE,pay_grade_master.GRADE_DESC AS GRADE_CODE, date_format(a.LEFT_DATE,'%d/%m/%Y') as 'LEFT_DATE',(select emp_name from pay_employee_master b where b.emp_code = a.REPORTING_TO) as REPORTING_TO, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2,pay_company_master.CITY, pay_company_master.STATE, pay_company_master.COMPANY_WEBSITE,pay_company_master.COMPANY_CONTACT_NO as COMPANY_CONTACT_NO FROM pay_employee_master a INNER JOIN pay_company_master ON a.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON a.GRADE_CODE =pay_grade_master.GRADE_CODE WHERE a.EMP_CODE='" + l_EMP_code1 + "'AND a.comp_code='" + Session["comp_code"].ToString() + "'";
                ReportLoad1(query, downloadname);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This employee is still working as per system.');", true);
            }
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }
    }
    protected void AppointmentSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con1.Open();
            System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)AppointmentSearch.SelectedRow.FindControl("lbl_appointment_id");


            string l_EMP_code1 = lbl_EMP_code.Text;
            d.con1.Close();
            string downloadname = "appointment_letter";
            string query = null;



            crystalReport.Load(Server.MapPath("~/AppoimentLetter.rpt"));

            // query = " SELECT pay_employee_master.comp_code,pay_employee_master.  EMP_NAME,  pay_employee_master.JOINING_DATE,pay_employee_master.EMP_CODE,pay_employee_master. DESIGNATION_CODE, pay_employee_master. GRADE_CODE,pay_employee_master. LEFT_DATE,pay_employee_master. REPORTING_TO,pay_company_master.COMPANY_NAME ,pay_company_master.ADDRESS1 ,pay_company_master.ADDRESS2 ,pay_company_master.CITY ,pay_company_master.STATE , pay_company_master.COMPANY_PAN_NO ,pay_company_master.COMPANY_TAN_NO ,pay_company_master.COMPANY_WEBSITE ,pay_company_master.COMPANY_CIN_NO FROMpay_employee_master,pay_company_master WHERE pay_employee_master.comp_code = pay_company_master.comp_code and EMP_CODE='" + l_EMP_code1 + "'";
            query = "SELECT  appointment_letter.EMP_CODE,  appointment_letter.EMP_NAME,  DATE_FORMAT(appointment_letter.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',  appointment_letter.GRADE_CODE,  appointment_letter.state AS 'STATE',  appointment_letter.city AS 'CITY',  appointment_letter.salary_per_month,  pay_company_master.COMPANY_NAME,    pay_company_master.ADDRESS2,  pay_company_master.CITY,  pay_company_master.STATE,  pay_company_master.COMPANY_CONTACT_NO,  pay_company_master.COMPANY_WEBSITE,	appointment_letter.ADDRESS as ADDRESS1	FROM  appointment_letter    INNER JOIN pay_company_master ON appointment_letter.comp_code = pay_company_master.comp_code  where appointment_letter.comp_code='" + Session["comp_code"].ToString() + "' and appointment_letter.emp_code='" + l_EMP_code1 + "'";
            //crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
            ReportLoad1(query, downloadname);
            Response.End();

            ReportView1();
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }
    }

    private void ReportLoad1(string query, string downloadfilename)
    {

        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            sda = cmd.ExecuteReader();
            dt.Load(sda);
            d.con.Close();


            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            //  crystalReport.SetDatabaseLogon(@"Tanvi-Tej\Tanvi","");
            //Response.End();
            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadname);

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            //var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            //var script = string.Format("alert({0});", message);
            //ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "", script, true);
        }
        finally
        {
            d.con.Close();
            d1.con.Close();
        }
    }


    public void ReportView1()
    {

        d.con1.Open();
        System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)AppointmentSearch.SelectedRow.FindControl("lbl_appointment_id");
        d.con1.Close();

    }



    //protected void btn_edit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {


    //        System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
    //        string TaskName = lbl_id.Text;

    //        d.operation("update pay_to_do_list set  Task_Name='" + txt_Task_Name.Text + "',Task_Description= '" + txt_Task_Description.Text + "',Start_Date=STR_TO_DATE('" + txt_Start_Date.Text + "','%d/%m/%Y'),Remind_Till=STR_TO_DATE('" + txt_Remind_Till.Text + "','%d/%m/%Y'),Reminder='" + ddl_Reminder.SelectedValue + "' where ID= " + TaskName);
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);

    //    }
    //    catch (Exception ee)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);

    //    }
    //    finally
    //    {
    //        DataSet ds = new DataSet();
    //        ds = d.getData("SELECT ID as 'ID',Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list WHERE comp_code='" + Session["comp_code"].ToString() + "'");
    //        SearchGridView.DataSource = null;
    //        SearchGridView.DataBind();
    //        SearchGridView.DataSource = ds.Tables[0];
    //        SearchGridView.DataBind();
    //        text_Clear();
    //       // d.reset(this);
    //        btn_delete.Visible = false;
    //        btn_edit.Visible = false;
    //        btn_add.Visible = true;
    //    }


    //}

    //protected void btn_delete_Click(object sender, EventArgs e)
    //{
    //    System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
    //    string TaskName = lbl_id.Text;

    //    MySqlCommand cmd_1 = new MySqlCommand("Select Task_Name from pay_to_do_list  where  ID= " + TaskName, d.con1);
    //    d.con1.Close();
    //    d.con1.Open();
    //    int result = 0;
    //    try
    //    {
    //        result = d.operation("DELETE FROM pay_to_do_list  WHERE ID=" + TaskName);//delete command
    //        d.reset(this);

    //        if (result > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);

    //        }

    //    }
    //    catch (Exception ee)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(ee);", true);
    //    }
    //    finally
    //    {
    //        DataSet ds = new DataSet();
    //        ds = d.getData("SELECT Membership_No,Husband_Name,Wife_Name,Cell_Number as 'Mobile_Number',City FROM enquiry_form ");
    //        SearchGridView.DataSource = ds.Tables[0];
    //        SearchGridView.DataBind();

    //        text_Clear();
    //        //d.reset(this);
    //        btn_delete.Visible = false;
    //        btn_edit.Visible = false;
    //        btn_add.Visible = true;
    //    }



    //}

    //protected void btn_search_Click1(object sender, EventArgs e)
    //{
    //    Panel2.Visible = true;
    //    d.con1.Open();
    //    MySqlCommand cmd1 = new MySqlCommand("SELECT ID as 'ID',Task_Name as 'Task_Name',date_format(Start_Date,'%d/%m/%Y') as 'Start_Date' ,Task_Description as 'Task_Description' ,Reminder as 'Reminder' , Id As 'TASK_ID' FROM pay_to_do_list WHERE ((pay_to_do_list.Task_Name LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Start_Date LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Task_Description LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Reminder LIKE '%" + txt_ref_no.Text + "%')) AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
    //    DataSet ds1 = new DataSet();
    //    MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
    //    adp1.Fill(ds1);
    //    SearchGridView.DataSource = null;
    //    SearchGridView.DataBind();
    //    SearchGridView.DataSource = ds1.Tables[0];
    //    SearchGridView.DataBind();
    //    d.con1.Close();
    //    cmd1.Dispose();
    //    ds1.Dispose();
    //    adp1.Dispose();
    //}




    //protected void SearchGridView_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
    //        string Id = lbl_id.Text;


    //        d.con1.Open();
    //        MySqlCommand cmd2 = new MySqlCommand("SELECT Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list WHERE Id=" + Id + "", d.con);
    //        d.conopen();
    //        MySqlDataReader dr = cmd2.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            txt_Task_Name.Text = dr.GetValue(0).ToString();
    //            txt_Task_Description.Text = dr.GetValue(1).ToString();

    //            string date = dr.GetValue(2).ToString();
    //            if (date == "")
    //            {
    //                txt_Start_Date.Text = dr.GetValue(2).ToString();
    //            }
    //            else
    //            {

    //                txt_Start_Date.Text = date.ToString();
    //            }

    //            string remind = dr.GetValue(3).ToString();
    //            if (remind == "")
    //            {
    //                txt_Remind_Till.Text = dr.GetValue(3).ToString();
    //            }
    //            else
    //            {

    //                txt_Remind_Till.Text = remind.ToString();
    //            }


    //            ddl_Reminder.Text = dr.GetValue(4).ToString();

    //        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Successfully...!')", true);
    //        }
    //        dr.Close();
    //        d.conclose();
    //        btn_delete.Visible = true;
    //        btn_edit.Visible = true;
    //        btn_add.Visible = false;
    //    }
    //    catch (Exception ee)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Failed...!')", true);
    //    }
    //    finally
    //    {


    //    }


    //    //------------------------------------------------------------------------------------------------------------------------------

    //}

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }


    }

    protected void panelupdaeincrementamt_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.updateincrementgv, "Select$" + e.Row.RowIndex);

        }

    }
    protected void expering_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_experianceletter, "Select$" + e.Row.RowIndex);


        }
    }
    protected void realiving_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_realiving, "Select$" + e.Row.RowIndex);


        }

    }
    protected void Appoint_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.AppointmentSearch, "Select$" + e.Row.RowIndex);


        }
    }
    protected void Increment_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.incrementsearch, "Select$" + e.Row.RowIndex);


        }
    }


    protected void Auto_Increment()
    {
        d.con1.Close();

        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  offer_letter WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            d.con1.Open();
            drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {

            }
            else if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_emp_code.Text = "E01";
                }
                else
                {
                    int max_emp_code = int.Parse(drmax.GetValue(0).ToString());
                    if (max_emp_code < 10)
                    {
                        txt_emp_code.Text = "E0" + max_emp_code;
                    }
                    else if (max_emp_code > 9 && max_emp_code < 100)
                    {
                        txt_emp_code.Text = "E" + max_emp_code;
                    }
                    else
                    {
                    }
                }


            }
            drmax.Close();
        }
        catch (Exception ex) { }
        finally { d.con1.Close(); }


        //text_Clear();


    }



    protected void btn_new_Click(object sender, EventArgs e)
    {
        //        d.con1.Open();
        //        MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  offer_letter WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        //        drmax = cmdmax.ExecuteReader();
        //        if (!drmax.HasRows)
        //        {

        //        }
        //        else if (drmax.Read())
        //        {
        //            string str = drmax.GetValue(0).ToString();
        //            if (str == "")
        //            {
        //                txt_emp_code.Text = "E01";
        //            }
        //            else
        //            {
        //                int max_emp_code = int.Parse(drmax.GetValue(0).ToString());
        //                if (max_emp_code < 10)
        //                {
        //                    txt_emp_code.Text = "E0" + max_emp_code;
        //                }
        //                else if (max_emp_code > 9 && max_emp_code < 100)
        //                {
        //                    txt_emp_code.Text = "E" + max_emp_code;
        //                }
        //                else
        //                {
        //                }  
        //           }


        //        }
        //        drmax.Close();
        //        d.con1.Close();

        //        text_Clear();

    }


    protected void SearchGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            SearchGridView.UseAccessibleHeader = false;
            SearchGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod dont apply catch
    }
    protected void incrementsearch_PreRender(object sender, EventArgs e)
    {
        try
        {
            incrementsearch.UseAccessibleHeader = false;
            incrementsearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }
    protected void updateincrementgv_PreRender(object sender, EventArgs e)
    {
        try
        {
            updateincrementgv.UseAccessibleHeader = false;
            updateincrementgv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }

    protected void updatterminate_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_terminal.UseAccessibleHeader = false;
            gv_terminal.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }
    protected void updatewarring_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_warring.UseAccessibleHeader = false;
            gv_warring.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }
    protected void AppointmentSearch_PreRender(object sender, EventArgs e)
    {
        try
        {
            AppointmentSearch.UseAccessibleHeader = false;
            AppointmentSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }
    protected void gv_realiving_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_realiving.UseAccessibleHeader = false;
            gv_realiving.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }


    protected void gv_experianceletter_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_experianceletter.UseAccessibleHeader = false;
            gv_experianceletter.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {

        } //vinod
    }
    protected void get_city_list(object sender, EventArgs e)
    {
        if (ddlstate.SelectedItem.ToString() != "Select")
        {
            get_city(ddlstate.SelectedItem.ToString());
        }
    }

    public void get_city(string state)
    {
        //string name=  ddl_state.SelectedItem.ToString();
        ddlcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + state + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddlcity.Items.Add(dr_item1[0].ToString());
                ddlcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            // txt_particular.Items.Insert(0, "Select");
            ddlcity.Items.Insert(0, new ListItem("Select City", ""));
            ddlcity.Items.Insert(0, new ListItem("Select City", ""));
        }

    }

    protected void unoform_click(object sender, EventArgs e)
    {
        txt_uniform_rcv_date.Text = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
        txt_icard_rcv_date.Text = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {


                ddl_client_uniform.DataSource = dt_item;
                ddl_client_uniform.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_uniform.DataValueField = dt_item.Columns[1].ToString();
                ddl_client_uniform.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_client_uniform.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        incrementgvpanel.Visible = false;
        Panel_Uniform.Visible = true;

        panelapposearch.Visible = false;
        incrementsearch.Visible = false;

        appoimentpanel.Visible = false;
        panelnew.Visible = false;
        panelapposearch.Visible = false;
        appoimentpanel.Visible = false;
        panelnew.Visible = false;
        Panel2.Visible = false;
        panelapposearch.Visible = false;

        // incrementgvpanel.Visible = false;
        updateincrement.Visible = false;
        Panel_Realiving.Visible = false;
        panel_Experiance_Letter.Visible = false;
        panel_relivingSearch.Visible = false;
        panem_exp_letter.Visible = false;
        panelupdaeincrementamt.Visible = false;

        IncrementAmountpanel.Visible = false;
        Panel_terminate.Visible = false;





    }
    protected void ddl_client_uniform_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branch_uniform.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_uniform.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_uniform.DataSource = dt_item;
                ddl_branch_uniform.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_uniform.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_uniform.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();

            ddl_branch_uniform.Items.Insert(0, "ALL");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_branch_uniform_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_emp_uniform.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select EMP_NAME, EMP_CODE from pay_employee_master where comp_code='" + Session["comp_code"] + "' and  unit_code='" + ddl_branch_uniform.SelectedValue + "' and LEFT_DATE IS NULL ", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_emp_uniform.DataSource = dt_item;

                ddl_emp_uniform.DataTextField = dt_item.Columns[0].ToString();
                ddl_emp_uniform.DataValueField = dt_item.Columns[1].ToString();
                ddl_emp_uniform.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            ddl_emp_uniform.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_uniform_submit_Click(object sender, EventArgs e)
    {
        
        string downloadname2 = "Uniform_Letter";
        string query2 = null;

        try
        {
            crystalReport.Load(Server.MapPath("~/uniform.rpt"));
            crystalReport.DataDefinition.FormulaFields["uniform_status"].Text = "'" + (rb_uni_yes.Checked == true ? "Yes" : "No") + "'";
            crystalReport.DataDefinition.FormulaFields["icard_status"].Text = "'" + (rb_id_yes.Checked == true ? "Yes" : "No") + "'";

            query2 = "SELECT EMP_NAME,(SELECT GRADE_DESC FROM pay_grade_master WHERE pay_grade_master.GRADE_CODE = pay_employee_master .GRADE_CODE and COMP_CODE='"+Session["COMP_CODE"].ToString()+"') as GRADE_CODE,'" + txt_uniform_rcv_date.Text + "' as UNIFORM_ISSU_DATE,'" + txt_icard_rcv_date.Text + "' as ICARD_ISSU_DATE,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE pay_state_master.STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_CODE,  EMP_CURRENT_STATE FROM pay_employee_master inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and pay_employee_master.comp_code = pay_unit_master.comp_code where pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master.EMP_CODE = '" + ddl_emp_uniform.SelectedValue + "' ";
            ReportLoad1(query2, downloadname2);
            Response.End();
            text_Clear();

        }
        catch (Exception ee)
        {

        }
        finally
        {
        }
    }
    protected void ddl_client_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branch_increment.Items.Clear();

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_increment.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_increment.DataSource = dt_item;
                ddl_branch_increment.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_increment.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_increment.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            //ddl_branch_increment.Items.Insert(0, "Select");
            ddl_branch_increment.Items.Insert(0, "ALL");



        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }
    }
    protected void ddl_branch_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT EMP_CODE,EMP_NAME ,GRADE_CODE,EARN_TOTAL as 'INCREMETN_AMOUNT'  FROM pay_employee_master  WHERE pay_employee_master.UNIT_CODE = '" + ddl_branch_increment.SelectedValue + "'   AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this Client!!');", true);
        }
        else
        {
            panelapposearch.Visible = true;
            incrementsearch.Visible = true;
        }
        incrementsearch.DataSource = ds.Tables[0];
        incrementsearch.DataBind();
        d.con1.Close();
    }
    protected void ddl_client_reliev_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branch_reliev.Items.Clear();

        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_reliev.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_reliev.DataSource = dt_item;
                ddl_branch_reliev.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_reliev.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_reliev.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();

            ddl_branch_reliev.Items.Insert(0, "ALL");



        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }
    }
    protected void ddl_branch_reliev_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con1.Open();

            // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE',GRADE_CODE,LOCATION,date_format(BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE'  FROM pay_employee_master  WHERE  pay_employee_master.UNIT_CODE = '" + ddl_branch_reliev.SelectedValue + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and LEFT_DATE!='' and LEFT_DATE!='0000-00-00' ", d.con1);
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this client!!');", true);
            }
            else
            {
                panel_relivingSearch.Visible = true;
                Panel_Realiving.Visible = true;
            }
            gv_realiving.DataSource = ds.Tables[0];
            gv_realiving.DataBind();
        }
        catch (Exception ex) { }
        finally
        {
            d.con1.Close();
        }
    }
    protected void ddl_client_experiance_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branch_experiance.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_experiance.SelectedValue + "'  AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch_experiance.DataSource = dt_item;
                ddl_branch_experiance.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch_experiance.DataValueField = dt_item.Columns[1].ToString();
                ddl_branch_experiance.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();

            ddl_branch_experiance.Items.Insert(0, "ALL");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_branch_experiance_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();

        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as 'JOINING_DATE' ,GRADE_CODE,LOCATION,date_format(BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE'  FROM pay_employee_master  WHERE   pay_employee_master.UNIT_CODE = '" + ddl_branch_experiance.SelectedValue + "' and `LEFT_DATE`!=''  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and LEFT_DATE!='' and LEFT_DATE!='0000-00-00' ", d.con1);

        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this client!!');", true);
        }
        else
        {
            panel_Experiance_Letter.Visible = true;
            panel_Experiance_Letter.Visible = true;
        }
        gv_experianceletter.DataSource = ds.Tables[0];
        gv_experianceletter.DataBind();
        d.con1.Close();
    }

    protected void btn_joiningletter_Click(object  sender, EventArgs e) {
        //joining_letter.Visible = true;
        //panelnew.Visible = false;
        Panel1.Visible = true;
        


    }

    // rahul 29-04-2019 start
    protected void btn_joiningletter_print_click(object sender ,EventArgs e) { 
        string downloadname = "joining_letter";
        string query = null;
        string headerpath=null;
        string leftfooterpath = null;
        string rightfooterpath = null;
        string stamppath = null;

        try {

           
            //crystalReport.Load(Server.MapPath("~/joining_letter.rpt"));
            crystalReport.Load(Server.MapPath("~/joining_letter1.rpt"));
            if(Session["COMP_CODE"].ToString() =="C02" ){
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_header_image.png");
                leftfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_footer1.png");
               // rightfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\footer3.png");
                stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
            }else{
                headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_header_image.png");
                leftfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_footer1.png");
               // rightfooterpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\footer3.png");
                stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
            }
           // headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
            headerpath = headerpath.Replace("\\", "\\\\");
            leftfooterpath = leftfooterpath.Replace("\\", "\\\\");
            rightfooterpath = "";
            stamppath = stamppath.Replace("\\", "\\\\");
           // query = "select * from pay_employee_master";
            //old query
            //query = "select pay_employee_master.emp_name,pay_grade_master.grade_desc,pay_client_master.client_name, pay_unit_master.unit_name,pay_unit_master.unit_add2,pay_employee_master.id_as_per_dob as ihmscode,date_format(pay_employee_master.joining_date,'%d-%m-%Y') as joining_date, pay_company_master.company_name,pay_company_master.address1,pay_company_master.city,pay_company_master.state  ,pay_company_master.company_contact_no,'"+headerpath+"' as headerpath,'"+leftfooterpath+"' as 'leftfooterpath' ,'"+stamppath+"' as 'stappath' from pay_employee_master  inner join pay_unit_master on  pay_employee_master.comp_code=pay_unit_master.comp_code and pay_employee_master.unit_code=pay_unit_master.unit_code inner join pay_client_master on  pay_employee_master.comp_code=pay_client_master.comp_code and pay_employee_master.client_code=pay_client_master.client_code  inner join pay_company_master on pay_employee_master.comp_code=pay_company_master.comp_code inner join pay_grade_master on pay_employee_master.comp_code=pay_grade_master.comp_code and pay_employee_master.grade_code=pay_grade_master.grade_code where pay_employee_master.emp_code='"+ddl_joining_employee.SelectedValue+"' and pay_employee_master.comp_code='"+Session["COMP_CODE"].ToString()+"' ";

            query = "  SELECT concat('THIS IS TO BE INFORM YOU THAT WE ARE ',pay_company_master.company_name,', @@@@@@@@EMPLOYEE NAME - ',pay_employee_master.emp_name,'  @@@@@@@@EMPLOYEE ID - ',pay_employee_master.id_as_per_dob,' @@@@@@@@DESIGNATION - ',pay_grade_master.grade_desc,' @@@@@@@@DEPLOYED CLIENT & ADDRESS - ',pay_client_master.client_name,' ',pay_unit_master.unit_add2,'. @@@@@@@@JOINING DATE -  ',upper(DATE_FORMAT(pay_employee_master.joining_date, '%D %b %Y'))) as 'rightfooterpath', pay_employee_master.emp_name, pay_grade_master.grade_desc, pay_client_master.client_name, pay_unit_master.unit_name, pay_unit_master.unit_add2, pay_employee_master.id_as_per_dob AS 'ihmscode', DATE_FORMAT(pay_employee_master.joining_date, '%d-%m-%Y') AS 'joining_date', upper(pay_company_master.company_name) as company_name, upper(pay_company_master.address1) as address1, pay_company_master.city, pay_company_master.state, pay_company_master.company_contact_no,'" + headerpath + "' as headerpath,'" + leftfooterpath + "' as 'leftfooterpath' ,'" + stamppath + "' as 'stappath' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_company_master ON pay_employee_master.comp_code = pay_company_master.comp_code INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code where  pay_employee_master.emp_code='" + ddl_joining_employee.SelectedValue + "' and pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' ";
            ///crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
            ReportLoad(query, downloadname);
            // Auto_Increment();
            Response.Flush();
            //response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            ddl_joining_unit.SelectedValue="";
            ddl_joining_state.SelectedValue="";
           	ddl_joining_employee.SelectedValue="";

        }
        catch(Exception eq){
            throw eq;
        }
    
    }

    protected void btn_close_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btn_joining_client_list(object  sender , EventArgs e) {

        ddl_joining_employee.Items.Clear();
        if (ddl_joining_client.SelectedValue != "Select")
        {

            //State
            ddl_joining_state.Items.Clear();
            ddl_joining_unit.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_joining_client.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);

            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_joining_state.DataSource = dt_item;
                    ddl_joining_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_joining_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_joining_state.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_joining_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_joining_unit.Items.Clear();
               
            }
        }

    }


    protected void btn_joining_state_list(object  sender, EventArgs e) {
        ddl_joining_employee.Items.Clear();
        ddl_joining_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_joining_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_joining_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_joining_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_joining_unit.DataSource = dt_item;
                ddl_joining_unit.DataTextField = dt_item.Columns[0].ToString();
                ddl_joining_unit.DataValueField = dt_item.Columns[1].ToString();
                ddl_joining_unit.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            ddl_joining_unit.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
           

        }
    }

    protected void btn_joining_unit_list(object sender , EventArgs e) {
        ddl_joining_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct emp_code,emp_name from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code='" + ddl_joining_unit.SelectedValue + "' and employee_type='Permanent' and left_date is null and legal_flag !=  0", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_joining_employee.DataSource = dt_item;
                ddl_joining_employee.DataTextField = dt_item.Columns[1].ToString();
                ddl_joining_employee.DataValueField = dt_item.Columns[0].ToString();
                ddl_joining_employee.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            ddl_joining_employee.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();

           
        }
    }

    public void client_list()
    {
        d.con1.Close();
        ddl_joining_employee.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ") and client_active_close='0'", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_joining_client.DataSource = dt_item;
                ddl_joining_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_joining_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_joining_client.DataBind();


            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_joining_client.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
           

        }

    }

    // rahul code end
}