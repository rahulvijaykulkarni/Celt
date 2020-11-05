using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Threading;
using System.Net.Mail;
using System.Collections.Generic;

public partial class p_add_new_travel_plan2 : System.Web.UI.Page
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
            ViewState["save"] = "0";
            ////// chaitali 3/04/2020
            //MySqlCommand cmd_unitcode = new MySqlCommand("Select currency_code from currency_codes", d.con);
            //d.con.Open();
            //try
            //{
            //    MySqlDataReader dr_unitcode = cmd_unitcode.ExecuteReader();
            //    while (dr_unitcode.Read())
            //    {
            //        ddl_currency.Items.Add(dr_unitcode.GetValue(0).ToString());
            //    }
            //    dr_unitcode.Close();
            //}
            //catch (Exception ex) { throw ex; }
            //finally
            //{
            //    d.con.Close();
            //}
            //ddl_currency.Items.Insert(0, "Select Currency");
            //load_sub_grid(Session["EXPENSE_ID"].ToString());
            btn_submit.Visible = false;
            load_city();
            load_mode(0);
            if (!Session["EXPENSE_ID"].ToString().Equals(""))
            {
                load_sub_grid(Session["EXPENSE_ID"].ToString());
            }
            else
            {
                gv_paymentdetails.DataSource = null;
                gv_paymentdetails.DataBind();
            }
            rate.Visible = false;
            type_ddl.Visible = false;
        }
    }
    //protected void currencye()
    //{
    //    d.con.Open();
    //    try
    //    {
    //        MySqlCommand cmd = new MySqlCommand("SELECT currency_desp,currency_countries from currency_codes WHERE currency_code = '" + ddl_currency.SelectedItem.ToString() + "'  ", d.con1);

    //        DataSet ds = new DataSet();
    //        DataSet ds1 = new DataSet();
    //        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd);
    //        adp1.Fill(ds1);
    //        gv_currency.DataSource = ds1.Tables[0];
    //        gv_currency.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        d.con.Close();
    //    }

    //}
    //protected void ddl_currency_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    d.con.Open();
    //    try
    //    {
    //        MySqlCommand cmd = new MySqlCommand("SELECT currency_desp,currency_countries from currency_codes WHERE currency_code = '" + ddl_currency.SelectedItem.ToString() + "'  ", d.con1);

    //        DataSet ds = new DataSet();
    //        DataSet ds1 = new DataSet();
    //        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd);
    //        adp1.Fill(ds1);
    //        gv_currency.DataSource = ds1.Tables[0];
    //        gv_currency.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //}

    protected void chk_exception_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_exception.Checked)
        {
            load_mode(1);
        }
        else
        {
            load_mode(0);
        }
    }

    protected void load_mode(int exception)
    {
        d.con.Open();
        emp_category.Items.Clear();
       // emp_category.Items.Insert(0, new ListItem("Select"));
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("select chkair,chkbus,chktraint1,chktraint2,chktraint3,chkcabtaxi,chkauto,chkownedvehicle from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con);
            MySqlDataReader cad1 = cmd_1.ExecuteReader();
            if (!cad1.HasRows)
            {
                btn_submit.Visible = false;
                btn_save.Visible = false;
            }
            while (cad1.Read())
            {
                int i = 0;
                if (exception == 0)
                {
                    if (cad1.GetValue(0).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Air", "Air"));
                    }
                    if (cad1.GetValue(1).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Bus", "Bus"));
                    }
                    if (cad1.GetValue(2).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier1", "Train AC Tier1"));
                    }
                    if (cad1.GetValue(3).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier2", "Train AC Tier2"));
                    }
                    if (cad1.GetValue(4).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier3", "Train AC Tier3"));
                    }
                    if (cad1.GetValue(5).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Cab/Taxi", "Cab/Taxi"));
                    }
                    if (cad1.GetValue(6).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Auto", "Auto"));
                    }
                    if (cad1.GetValue(7).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i, new ListItem("Owned Vehicle", "Owned Vehicle"));
                    }
                   

                }
                if (exception == 1)
                {
                    if (!cad1.GetValue(0).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Air", "Air"));
                    }
                    if (!cad1.GetValue(1).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Bus", "Bus"));
                    }
                    if (!cad1.GetValue(2).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier1", "Train AC Tier1"));
                    }
                    if (!cad1.GetValue(3).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier2", "Train AC Tier2"));
                    }
                    if (!cad1.GetValue(4).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Train AC Tier3", "Train AC Tier3"));
                    }
                    if (!cad1.GetValue(5).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Cab/Taxi", "Cab/Taxi"));
                    }
                    if (!cad1.GetValue(6).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i++, new ListItem("Auto", "Auto"));
                    }
                    if (!cad1.GetValue(7).ToString().Equals("1"))
                    {
                        emp_category.Items.Insert(i, new ListItem("Owned Vehicle", "Owned Vehicle"));
                    }
                   
                }

            }
            emp_category.DataBind();
           
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            emp_category.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    }
    protected void gv_paymentdetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con1.Open();
            if (ddl_type_bus.SelectedValue == "Select")
            { type_ddl.Visible = false; }
            else
            { type_ddl.Visible = true; }
            string expenses_Id_edit = gv_paymentdetails.SelectedRow.Cells[3].Text;

            MySqlCommand cmd = new MySqlCommand("SELECT city_type,travel_mode,type,exception_case,from_designation,to_designation,date_format(from_date,'%d/%m/%Y') as from_date,date_format(to_date,'%d/%m/%Y') as to_date,curreny_id,adv_amount,Add_Description,id, expenses_id, expense_status FROM apply_travel_plan WHERE  id=" + expenses_Id_edit, d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.GetValue(1).ToString() == "Yes")
                {
                    chk_exception.Checked = true;
                    chk_exception_CheckedChanged(null, null);
                }
                else
                {
                    chk_exception.Checked = false;
                }
                ddl_city.SelectedValue = dr.GetValue(0).ToString();
                emp_category.SelectedValue = dr.GetValue(1).ToString();

                ddl_type_bus.SelectedValue = dr.GetValue(2).ToString();

                txt_dest_from.Text = dr.GetValue(4).ToString();

                txttodeg.Text = dr.GetValue(5).ToString();

                txt_from_date.Text = dr.GetValue(6).ToString();

                txt_to_date.Text = dr.GetValue(7).ToString();

                ddl_currency.SelectedValue = dr.GetValue(8).ToString();
                txt_amout.Text = dr.GetValue(9).ToString();
                txt_add.Text = dr.GetValue(10).ToString();
                ViewState["ID"] = dr.GetValue(11).ToString();
                ViewState["expense_id"] = dr.GetValue(12).ToString();
               
                ViewState["save"] = "1";
              
            }
            dr.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con1.Close();
        }

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
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_paymentdetails, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {

        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        d.operation("delete from apply_travel_plan where id = " + int.Parse(grdrow.Cells[3].Text));
        load_sub_grid(Session["EXPENSE_ID"].ToString());

    }

    private void getexpenseid(string expense_id)
    {
        if (expense_id.Equals(""))
        {
            try
            {
                d.con1.Open();
                MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTR(expenses_id, 3, 5) AS unsigned))+1 FROM  apply_travel_plan", d.con1);
                MySqlDataReader drmax = cmdmax.ExecuteReader();
                if (!drmax.HasRows)
                {
                }
                else if (drmax.Read())
                {
                    string str = drmax.GetValue(0).ToString();
                    if (str == "")
                    {
                        Session["EXPENSE_ID"] = "EP01";
                    }
                    else
                    {
                        int max_compcode = int.Parse(drmax.GetValue(0).ToString());
                        if (max_compcode < 10)
                        {
                            Session["EXPENSE_ID"] = "EP0" + max_compcode;

                        }
                        else if (max_compcode > 9 && max_compcode < 100)
                        {
                            Session["EXPENSE_ID"] = "EP" + max_compcode;

                        }
                        else
                        {
                        }
                    }
                    drmax.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }
        }


    }

    private void update_notification()
    {
        try
        {
            if (ViewState["save"].ToString().Equals("1"))
            {
                d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan Updated By Employee " + Session["USERNAME"].ToString() + "','0',(select reporting_to from pay_employee_master b where b.emp_code = a.EMP_CODE),'app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1");
            }
            else
            { d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Plan Added By Employee " + Session["USERNAME"].ToString() + "','0',(select reporting_to from pay_employee_master b where b.emp_code = a.EMP_CODE),'app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1"); }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string approval_level = "";
            string query = "Select txt_approval_level, date_format(txt_start_date,'%d/%m/%Y') as 'from_date',case when txt_end_date = null then '' else date_format(txt_end_date,'%d/%m/%Y') end as 'to_date' from pay_travel_policy_master Where ID in (Select policy_id from pay_travel_emp_policy Where emp_code = '" + Session["LOGIN_ID"].ToString() + "')";
            MySqlCommand cmd_level = new MySqlCommand(query, d.con1);
            d.con1.Open();
            try
            {
                MySqlDataReader dr_level = cmd_level.ExecuteReader();
                if (dr_level.Read())
                {
                    DateTime from_db = DateTime.Now.Date;
                    int day = Convert.ToInt32(txt_from_date.Text.Substring(0, 2));
                    int month = Convert.ToInt32(txt_from_date.Text.Substring(3, 2));
                    int year = Convert.ToInt32(txt_from_date.Text.Substring(6, 4));
                    from_db = new DateTime(year, month, day);

                    DateTime txt_from = DateTime.Now.Date;
                    int day1 = Convert.ToInt32(txt_from_date.Text.Substring(0, 2));
                    int month1 = Convert.ToInt32(txt_from_date.Text.Substring(3, 2));
                    int year1 = Convert.ToInt32(txt_from_date.Text.Substring(6, 4));
                    txt_from = new DateTime(year1, month1, day1);

                    DateTime txt_to = DateTime.Now.Date;
                    int day2 = Convert.ToInt32(txt_from_date.Text.Substring(0, 2));
                    int month2 = Convert.ToInt32(txt_from_date.Text.Substring(3, 2));
                    int year2 = Convert.ToInt32(txt_from_date.Text.Substring(6, 4));
                    txt_to = new DateTime(year2, month2, day2);
                   
                    
                    
                    //DateTime from_db = DateTime.ParseExact(dr_level[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime txt_from = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime txt_to = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (dr_level[2].ToString().Equals(""))
                    {
                        if (txt_from < from_db || txt_to < from_db)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The assigned Travel Policy is between " + dr_level[1].ToString() + " to " + dr_level[2].ToString() + ", Select another date.');", true);
                            return;
                        }
                    }
                    else
                    {
                        DateTime to_db = DateTime.ParseExact(dr_level[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (txt_from < from_db || txt_to < from_db || txt_from > to_db || txt_to > to_db)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The assigned Travel Policy is between " + dr_level[1].ToString() + " to " + dr_level[2].ToString() + ", Select another date.');", true);
                            return;
                        }
                    }
                    approval_level = dr_level[0].ToString();
                    if (approval_level.Trim() == "" || approval_level == null || approval_level == "0")
                    {
                        approval_level = "1";
                    }
                    //if (ddl_type_bus.SelectedValue == "Owned Vehicle")
                    //{
                    string text_box = "";
                    if(ddl_type_bus.SelectedValue!="Select")
                    {
                        if (emp_category.SelectedValue != "Owned Vehicle")
                        {

                            text_box = "" + ddl_type_bus.SelectedValue + "";
                            string db_amount = d.getsinglestring("select " + text_box + " from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");

                            double val1 = Convert.ToDouble("" + txt_amout.Text + "");
                            double val2 = Convert.ToDouble("" + db_amount + "");

                            if (val1 > val2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + val2 + "')", true);
                                return;
                            }
                        }
                    }
                        
                    //}
                    string db_limit = "";
                    if (ddl_city.SelectedValue == "1")
                    {
                         db_limit = d.getsinglestring("select txt_per_day_limit from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");
                    }
                    else
                    {
                         db_limit = d.getsinglestring("select Textbox_outside from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");
                    }
                   
                   

                   string date=""+txt_from_date.Text+"";
                   string date1 = "" + txt_to_date.Text + "";
                      DateTime dateTime1, dateTime2;

                      DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime1);
                      DateTime.TryParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime1);

                   DateTime fDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                   DateTime tDate = DateTime.ParseExact(date1, "dd/MM/yyyy", null);
                    
                    String diff2 = (tDate - fDate).TotalDays.ToString();

                    int db_limit1 = Int32.Parse(db_limit);
                    int no_of_days_count = Int32.Parse(diff2);

                   int total = (( db_limit1 ) * (no_of_days_count ));

                   //int txt_amount = Int32.Parse(""+txt_amout.Text+"");
                    double txt_amount = Convert.ToDouble("" + txt_amout.Text + "");
                    if (total != 0)
                    {
                        if (total < txt_amount)
                        {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + db_limit + "')", true);
                           return;
                        }
                    }
                    double val = Convert.ToDouble("" + db_limit + "");
                   
                    string city_type = d.getsinglestring("select SUM(adv_amount) as 'Amount' FROM apply_travel_plan WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "' and from_date=str_to_date('" + txt_from_date.Text + "','%d/%m/%Y') and to_date=str_to_date('" + txt_to_date.Text + "','%d/%m/%Y')");
                    if (city_type != "")
                    {
                       
                      
                        double val3 = Convert.ToDouble("" + city_type + "");
                        double tol_amt = val3 + txt_amount;
                        if (val < val3)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + db_limit + "')", true);
                            return;
                        }
                        else if (tol_amt > val)
                        {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + db_limit + "')", true);
                            return;
                        }

                    }
                   
                    if (txt_amount > val)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + db_limit + "')", true);
                        return;
                    }
                    
                }
                dr_level.Close();
                dr_level.Dispose();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            if (ViewState["save"].ToString().Equals("1"))
            { btn_update_Click(null, null); }
            else
            {
                if (txt_from_date.Text != "")
                {
                    getexpenseid(Session["EXPENSE_ID"].ToString());
                    d.con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT date_format(from_date,'%d/%m/%Y') as 'from_date',date_format(to_date,'%d/%m/%Y') as 'to_date' FROM apply_travel_plan  WHERE emp_code= '" + Session["LOGIN_ID"].ToString() + "' and expenses_id != '" + Session["EXPENSE_ID"].ToString() + "' and ((from_date between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') and STR_TO_DATE ('" + txt_to_date.Text + "','%d/%m/%Y')) || (to_date between STR_TO_DATE('" + txt_from_date.Text + "','%d/%m/%Y') and STR_TO_DATE ('" + txt_to_date.Text + "','%d/%m/%Y')))", d.con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The Travel Plan already exist from " + dr.GetValue(0).ToString() + " to " + dr.GetValue(1).ToString() + ", Select another date.');", true);
                    }
                    else
                    {
                        getexpenseid(Session["EXPENSE_ID"].ToString());
                        int res = d.operation("INSERT INTO apply_travel_plan(comp_code,unit_code,emp_code,travel_mode,exception_case,from_designation,to_designation,from_date,to_date,adv_amount,Add_Description,expense_status,modified_by,curreny_id,expenses_id,Approval_level,Approved_level,type,city_type) VALUES('" + Session["COMP_CODE"].ToString() + "','" + Session["UNIT_CODE"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','" + emp_category.SelectedValue + "','" + (chk_exception.Checked ? "Yes" : "No") + "','" + txt_dest_from.Text + "','" + txttodeg.Text + "',str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y')," + txt_amout.Text + ",'" + txt_add.Text + "','Draft',(select emp_name from pay_employee_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "'), '" + ddl_currency.SelectedValue + "','" + Session["EXPENSE_ID"].ToString() + "','" + approval_level + "','0','" + ddl_type_bus.SelectedValue + "','"+ddl_city.SelectedValue+"')");
                        string id = d.getsinglestring("select id from apply_travel_plan where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + Session["LOGIN_ID"].ToString() + "' and expenses_id='" + Session["EXPENSE_ID"].ToString() + "'");
                        ViewState["ID"] = id.ToString();
                        if (chk_roundtrip.Checked)
                        {
                            res = d.operation("INSERT INTO apply_travel_plan(comp_code,unit_code,emp_code,travel_mode,exception_case,from_designation,to_designation,from_date,to_date,adv_amount,Add_Description,expense_status,modified_by,curreny_id,expenses_id,Approval_level,Approved_level,type,city_type) VALUES('" + Session["COMP_CODE"].ToString() + "','" + Session["UNIT_CODE"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','" + emp_category.SelectedValue + "','" + (chk_exception.Checked ? "Yes" : "No") + "','" + txttodeg.Text + "','" + txt_dest_from.Text + "',str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + txt_amout.Text + "','" + txt_add.Text + "','Draft',(select emp_name from pay_employee_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "'), '" + ddl_currency.SelectedValue + "','" + Session["EXPENSE_ID"].ToString() + "','" + approval_level + "','0','" + ddl_type_bus.SelectedValue + "','"+ddl_city.SelectedValue+"')");
                        }
                        update_reporting_emp(approval_level, Session["EXPENSE_ID"].ToString(), Session["LOGIN_ID"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Plan Saved Successfully');", true);
                        load_sub_grid(Session["EXPENSE_ID"].ToString());
                        text_clear();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Required Fields First.');", true);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            //load_sub_grid(Session["EXPENSE_ID"].ToString());
            //ddl_type_bus.Items.Insert(0, new ListItem("Select"));
           // ddl_type_bus.Items.Clear();
            btn_submit.Visible = true;
        }
    }

    private void update_reporting_emp(string app_level, string expense_id, string emp_code)
    {
        if (app_level.Equals(""))
        {
            string query = "Select txt_approval_level, date_format(txt_start_date,'%d/%m/%Y') as 'from_date',case when txt_end_date = null then '' else date_format(txt_end_date,'%d/%m/%Y') end as 'to_date' from pay_travel_policy_master Where ID in (Select policy_id from pay_travel_emp_policy Where emp_code = '" + Session["LOGIN_ID"].ToString() + "')";
            MySqlCommand cmd_level = new MySqlCommand(query, d.con1);
            d.con1.Open(); try
            {
                MySqlDataReader dr_level = cmd_level.ExecuteReader();
                if (dr_level.Read())
                {
                    app_level = dr_level[0].ToString();
                }
                if (app_level.Trim() == "" || String.IsNullOrEmpty(app_level) || app_level == "0")
                {
                    app_level = "1";
                }
                dr_level.Close(); dr_level.Dispose();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }
        }
        string r_emp_code = "";
        int i_app_level = int.Parse(app_level);
        while (i_app_level > 0)
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select reporting_to from pay_employee_master where emp_code = '" + emp_code + "'", d.con);
            try
            {
                string r_temp_emp = (string)cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(r_temp_emp))
                {
                    r_emp_code = r_emp_code + (string)cmd.ExecuteScalar();
                    emp_code = (string)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                cmd.Dispose();
            }
            i_app_level = --i_app_level;
        }
        d.operation("update apply_travel_plan set approved_emp=null, approval_emp = '" + r_emp_code + "' where expenses_id = '" + expense_id + "'");
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        int result = 0;

        try
        {
            result = d.operation("UPDATE apply_travel_plan SET  travel_mode='" + emp_category.SelectedValue + "',exception_case='" + (chk_exception.Checked ? "Yes" : "No") + "',from_designation='" + txt_dest_from.Text + "',to_designation ='" + txttodeg.Text + "',from_date =str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),to_date =str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'), curreny_id='" + ddl_currency.SelectedValue.ToString() + "', adv_amount ='" + txt_amout.Text + "', Add_Description='" + txt_add.Text + "' ,expense_status ='Draft',type='" + ddl_type_bus.SelectedValue + "',city_type='" + ddl_city.SelectedValue + "' WHERE id = " + int.Parse(ViewState["ID"].ToString()));
            if (chk_roundtrip.Checked)
            {

                result = d.operation("INSERT INTO apply_travel_plan(comp_code,unit_code,emp_code,travel_mode,exception_case,from_designation,to_designation,from_date,to_date,adv_amount,Add_Description,expense_status,modified_by,curreny_id,expenses_id,type,city_type) VALUES('" + Session["COMP_CODE"].ToString() + "','" + Session["UNIT_CODE"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','" + emp_category.SelectedValue + "','" + (chk_exception.Checked ? "Yes" : "No") + "','" + txttodeg.Text + "','" + txt_dest_from.Text + "',str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + txt_amout.Text + "','" + txt_add.Text + "','Draft',(select emp_name from pay_employee_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "'), '" + ddl_currency.SelectedValue + "','" + Session["EXPENSE_ID"].ToString() + "','" + ddl_type_bus.SelectedValue + "','"+ddl_city.SelectedValue+"')");
            }
            update_reporting_emp("", Session["EXPENSE_ID"].ToString(), Session["LOGIN_ID"].ToString());
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updated successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updation failed!!');", true);
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            load_sub_grid(Session["EXPENSE_ID"].ToString());
        }
        text_clear();
    }

    private void load_sub_grid(string expenses_Id_edit)
    {
        double a = 0, c = 0;
        try
        {
            if (ddl_type_bus.SelectedValue == "Select")
            { type_ddl.Visible = false; }
            else
            { type_ddl.Visible = true; }
            MySqlCommand cmd1 = null;
            //if (expenses_Id_edit != "")
            //{
            //    //string expenses_Id = gv_paymentdetails.SelectedRow.Cells[3].Text;
            //    cmd1 = new MySqlCommand("select id, expenses_id, comp_code,unit_code,emp_code, case when city_type=1 then 'Inside City' when city_type=0 then 'Outside City' end as 'city_type',travel_mode,type,exception_case,from_designation,to_designation,date_format(from_date,'%d/%m/%Y') as 'from_date',date_format(to_date,'%d/%m/%Y') as 'to_date',adv_amount,Add_Description,expense_status,modified_by,curreny_id,expenses_id from apply_travel_plan where Expenses_id='" + expenses_Id_edit + "' and id='" + int.Parse(ViewState["ID"].ToString()) + "'", d.con);
            //}
            //else
            //{
            cmd1 = new MySqlCommand("select id, expenses_id, comp_code,unit_code,emp_code, case when city_type=1 then 'Inside City' when city_type=2 then 'Outside City' end as 'city_type',travel_mode,type,exception_case,from_designation,to_designation,date_format(from_date,'%d/%m/%Y') as 'from_date',date_format(to_date,'%d/%m/%Y') as 'to_date',adv_amount,Add_Description,expense_status,modified_by,curreny_id,expenses_id from apply_travel_plan where Expenses_id='" + expenses_Id_edit + "'", d.con);
            //}
            d.con.Open();
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                gv_paymentdetails.DataSource = dt;
                gv_paymentdetails.DataBind();
                gv_paymentdetails.Visible = true;

                foreach (DataRow row in dt.Rows)
                {
                    a = double.Parse(row["adv_amount"].ToString());
                    c = c + a;
                    if (!row["expense_status"].ToString().Equals("Draft"))
                    {
                        btn_save.Visible = false;
                        btn_submit.Visible = false;
                    }
                    else
                    {
                        btn_save.Visible = true;
                        btn_submit.Visible = true;
                    }
                }
                gv_paymentdetails.FooterRow.Cells[12].Text = "Total:";
                gv_paymentdetails.FooterRow.Cells[13].Text = c.ToString();
                gv_paymentdetails.FooterRow.Cells[13].Visible = true;
                gv_paymentdetails.Visible = true;
                newpanel.Visible = true;

            }

            dr1.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();
            btn_submit.Visible = true;
        }
    }
    public void text_clear()
    {
        txt_add.Text = "";
        txt_dest_from.Text = "";
        txttodeg.Text = "";
        txt_from_date.Text = "";
        txt_to_date.Text = "";
        txt_amout.Text = "";
        txt_rate.Text = "";
        ddl_city.SelectedValue = "Select";
        ddl_type_bus.SelectedValue = "Select";
        ddl_currency.SelectedIndex = 0;
        emp_category.SelectedIndex = 0;
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        d.operation("UPDATE apply_travel_plan set expense_status = 'Submitted' where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
        try
        {
            update_notification();//for mail
            d.SendHtmltravelEmail(Server.MapPath("~/travel_plan.htm"), Session["EXPENSE_ID"].ToString(), 0, "", Session["USERNAME"].ToString(),"");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send successfully');", true);
        }
        catch
        { }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travelling Plan Submitted Successfully');", true);
        btn_save.Visible = false;
        btn_submit.Visible = false;
    }
    //protected void chk_roundtrip_CheckedChanged1(object sender, EventArgs e)
    //{
    //    if (chk_roundtrip.Checked)
    //    {
    //        string name = "";
    //        name = txt_dest_from.Text.ToString();
    //        txt_dest_from.Text = txttodeg.Text.ToString();
    //        txttodeg.Text = name;
    //    }
    //    else
    //    { 


    //    }

    //}
    //chaitali start 27-03-2020
    protected void emp_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        // grd = new MySqlDataAdapter("select CASE WHEN CheckBox_ac = 1 THEN 'AC' WHEN CheckBox_ac = 0 THEN '0' END AS 'CheckBox_ac',CASE WHEN CheckBox_nonac = 1 THEN 'NON-AC'  WHEN CheckBox_nonac = 0 THEN '0'  END AS 'CheckBox_nonac',CASE WHEN CheckBox_citybus = 1 THEN 'CITY_BUS'  WHEN CheckBox_citybus = 0 THEN '0'  END AS 'CheckBox_citybus' FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') and chkbus=1", d.con1);
        //grd = new MySqlDataAdapter("SELECT GROUP_CONCAT(CASE WHEN `CheckBox_ac` = 1 THEN 'AC' WHEN `CheckBox_ac` = 0 THEN '0' END, CASE WHEN `CheckBox_nonac` = 1 THEN 'NON-AC' WHEN `CheckBox_nonac` = 0 THEN '0' END, CASE WHEN `CheckBox_citybus` = 1 THEN 'CITY_BUS' WHEN `CheckBox_citybus` = 0 THEN '0' END) AS LIST FROM `pay_travel_policy_master` WHERE `id` = (SELECT `policy_id` FROM `pay_travel_emp_policy` WHERE `emp_code` = '" + Session["LOGIN_ID"].ToString() + "') AND `chkbus` = 1", d.con1);
           
        d.con1.Open();
        try
        {
            rate.Visible = false;
            type_ddl.Visible = false;
            txt_amout.Text = "";
            txt_amout.ReadOnly = false;
                ddl_type_bus.Items.Clear();
                d.con.Open();
                if (emp_category.SelectedValue == "Bus")
                {
                    type_ddl.Visible = true;
                    MySqlCommand cmd_1 = new MySqlCommand("SELECT CheckBox_ac,CheckBox_nonac,CheckBox_citybus FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkbus='1'", d.con);
                    MySqlDataReader cad1 = cmd_1.ExecuteReader();
                    while (cad1.Read())
                    {
                        int i = 0;
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type_bus.Items.Insert(i++, new ListItem("AC", "AC"));
                        }
                        if (cad1.GetValue(1).ToString().Equals("1"))
                        {
                            ddl_type_bus.Items.Insert(i++, new ListItem("NonAc", "NonAc"));
                        }
                        if (cad1.GetValue(2).ToString().Equals("1"))
                        {
                            ddl_type_bus.Items.Insert(i++, new ListItem("CityBus", "CityBus"));
                        }
                    }
                }
                else if (emp_category.SelectedValue == "Owned Vehicle")
                {
                    type_ddl.Visible = true;
                    rate.Visible = true;
                    MySqlCommand cmd_1 = new MySqlCommand("SELECT  CheckBox_car,CheckBox_bike FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkownedvehicle='1'", d.con);
                    MySqlDataReader cad1 = cmd_1.ExecuteReader();
                    while (cad1.Read())
                    {
                        int i = 0;
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type_bus.Items.Insert(i++, new ListItem("Car", "Car"));
                            rate.Visible = true;
                        }
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type_bus.Items.Insert(i++, new ListItem("Bike", "Bike"));
                            rate.Visible = true;
                        }

                    }
                }
                //else if (emp_category.SelectedValue == "Expence")
                //{
                //    MySqlCommand cmd_1 = new MySqlCommand("SELECT CheckBox_inside,CheckBox_outside FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chk_expenses_allowed='1'", d.con);
                //    MySqlDataReader cad1 = cmd_1.ExecuteReader();
                //    while (cad1.Read())
                //    {
                //        int i = 0;
                //        if (cad1.GetValue(0).ToString().Equals("1"))
                //        {
                //            ddl_type_bus.Items.Insert(i++, new ListItem("inside", "txt_per_day_limit"));
                //        }
                //        if (cad1.GetValue(1).ToString().Equals("1"))
                //        {
                //            ddl_type_bus.Items.Insert(i++, new ListItem("outside", "Textbox_outside"));
                //        }
                //    }
                //}
                else if (emp_category.SelectedValue!= "Bus")
                {
                    //ddl_type_bus.SelectedValue = "";
                    txt_amout.Text = "";
                    ddl_type_bus.Items.Insert(0, "Select");
                    
                }
                ddl_type_bus.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_type_bus.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
        }
      
    }
    protected void ddl_type_bus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con1.Open();
            MySqlCommand cmd = null;
            if (ddl_type_bus.SelectedValue == "AC")
            {
                cmd = new MySqlCommand("SELECT AC FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con1);
            }
            else if (ddl_type_bus.SelectedValue == "NonAc")
            {
                cmd = new MySqlCommand("SELECT NonAc FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con1);
            }
            else if (ddl_type_bus.SelectedValue == "CityBus")
            {
                cmd = new MySqlCommand("SELECT CityBus FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con1);
            }
            else if (ddl_type_bus.SelectedValue == "inside")
            {
                cmd = new MySqlCommand("SELECT txt_per_day_limit FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chk_expenses_allowed= 1", d.con1);
            }
            else if (ddl_type_bus.SelectedValue == "outside")
            {
                cmd = new MySqlCommand("SELECT Textbox_outside FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chk_expenses_allowed= 1", d.con1);
            }
            //MySqlCommand cmd = new MySqlCommand("SELECT travel_mode,exception_case,from_designation,to_designation,date_format(from_date,'%d/%m/%Y') as from_date,date_format(to_date,'%d/%m/%Y') as to_date,curreny_id,adv_amount,Add_Description,id, expenses_id, expense_status FROM apply_travel_plan WHERE  id=" + expenses_Id_edit, d.con1);
            if (cmd != null)
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_amout.Text = dr.GetValue(0).ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            else
            {
                txt_amout.Text = "";
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
           
            d.con1.Close();
            //txt_amout.ReadOnly = true;
        }
    }
   
    protected void txt_rate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            d.con1.Open();
            MySqlCommand cmd = null;
            
            string rate = "" + txt_rate.Text + "";
            if (ddl_type_bus.SelectedItem.Text == "Car")
            {

                cmd = new MySqlCommand("SELECT ((Car)*(" + rate + ")) FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con1);
                txt_amout.ReadOnly = true;
            }
            else if (ddl_type_bus.SelectedItem.Text == "Bike")
            {
                cmd = new MySqlCommand("SELECT ((Bike)*(" + rate + ")) FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con1);
                txt_amout.ReadOnly = true;
            }
            if (cmd != null)
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_amout.Text = dr.GetValue(0).ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            else
            {
                txt_amout.Text = "";
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            //txt_amount.ReadOnly = true;
            d.con1.Close();
        }
    }
    protected void load_city()
    { 
        d.con.Open();
        ddl_city.Items.Clear();
       // emp_category.Items.Insert(0, new ListItem("Select"));
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("select  CheckBox_inside,CheckBox_outside from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con);
            MySqlDataReader cad1 = cmd_1.ExecuteReader();
            while (cad1.Read())
            {
                int i = 0;
                if (cad1.GetValue(0).ToString().Equals("1"))
                    {
                        ddl_city.Items.Insert(i++, new ListItem("Inside City", "1"));
                    }
                    if (cad1.GetValue(1).ToString().Equals("1"))
                    {
                        ddl_city.Items.Insert(i++, new ListItem("Outside City", "2"));
                    }
            }
            ddl_city.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            ddl_city.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    }
    //chaitali end 27-03-2020
}