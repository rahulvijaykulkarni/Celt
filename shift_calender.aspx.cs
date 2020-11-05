using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Default2 : System.Web.UI.Page
{
    DAL d = new DAL();
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
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and shift=1 ORDER BY 1", d.con);
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
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            main_div.Visible = false;
        }
    }
   
    protected void lnkaddtravelplan_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    private void gridcalender_update(int mnth, int year)
    {
        try
        {
            string where_unit = "";
            if (ddl_unitcode.SelectedValue == "ALL") { where_unit = " and unit_code in (select unit_code from pay_unit_master where client_code = '"+ddl_client.SelectedValue+"')";}
            else { where_unit = " and unit_code = '" + ddl_unitcode.SelectedValue + "')"; }

            string where = "";
            if (mnth == 0 && year == 0)
            {
                where = " and MONTH = MONTH(now()) and YEAR = YEAR(now())";
                d.operation("INSERT INTO shift_calendar(comp_code,UNIT_CODE,EMP_CODE,MONTH,YEAR) (SELECT pay_employee_master.comp_code,pay_employee_master.UNIT_CODE,pay_employee_master.EMP_CODE,MONTH(now()),YEAR(now()) FROM pay_employee_master WHERE pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM shift_calendar WHERE comp_code='" + Session["comp_code"].ToString() + "' and month=MONTH(now()) and year=YEAR(now()) "+where_unit+") "+ where_unit +")");
            }
            else
            {
                d.operation("INSERT INTO shift_calendar(comp_code,UNIT_CODE,EMP_CODE,MONTH,YEAR) (SELECT pay_employee_master.comp_code,pay_employee_master.UNIT_CODE,pay_employee_master.EMP_CODE,'" + mnth + "','" + year + "' FROM pay_employee_master WHERE pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM shift_calendar WHERE comp_code='" + Session["comp_code"].ToString() + "' and month='" + mnth + "' and year='" + year + "' " + where_unit + ") "+where_unit+")");
                where = " and MONTH = '" + mnth + "' and YEAR = '" + year + "'";
            }
            update_label(mnth, year);
            DataSet ds = new DataSet();
            d.con1.Open();
            ds = new DataSet();
            //MySqlDataAdapter adp = new MySqlDataAdapter("select shift_calendar.EMP_CODE,emp_name,DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31 from shift_calendar inner join pay_employee_master on shift_calendar.emp_code = pay_employee_master.emp_code where shift_calendar.comp_code = '" + Session["comp_code"].ToString() + "' and shift_calendar.unit_code = '" + Session["UNIT_CODE"].ToString() + "'" + where + " and shift_calendar.emp_code in (select emp_code from pay_employee_master where reporting_to = '" + Session["LOGIN_ID"].ToString() + "')", d.con1);
            where_unit = where_unit.Replace("and unit_code", "and shift_calendar.unit_code");
            MySqlDataAdapter adp = new MySqlDataAdapter("select shift_calendar.EMP_CODE,emp_name,DAY01,DAY02,DAY03,DAY04,DAY05,DAY06,DAY07,DAY08,DAY09,DAY10,DAY11,DAY12,DAY13,DAY14,DAY15,DAY16,DAY17,DAY18,DAY19,DAY20,DAY21,DAY22,DAY23,DAY24,DAY25,DAY26,DAY27,DAY28,DAY29,DAY30,DAY31 from shift_calendar inner join pay_employee_master on shift_calendar.emp_code = pay_employee_master.emp_code where shift_calendar.comp_code = '" + Session["comp_code"].ToString() + "' " + where_unit + " " + where + " order by 2", d.con1);
            adp.Fill(ds);
            shiftcalendar.DataSource = ds.Tables[0];
            shiftcalendar.DataBind();
            if(ds.Tables[0].Rows.Count > 0)
            {
                main_div.Visible = true;
                loadheaders(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value));
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void shiftcalendar_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((Control)sender).NamingContainer as GridViewRow;
        if (row != null)
        {
            DropDownList ddl1 = (DropDownList)row.FindControl("DropDownList1");
            DropDownList ddl2 = (DropDownList)row.FindControl("DropDownList2");
            DropDownList ddl3 = (DropDownList)row.FindControl("DropDownList3");
            DropDownList ddl4 = (DropDownList)row.FindControl("DropDownList4");
            DropDownList ddl5 = (DropDownList)row.FindControl("DropDownList5");
            DropDownList ddl6 = (DropDownList)row.FindControl("DropDownList6");
            DropDownList ddl7 = (DropDownList)row.FindControl("DropDownList7");
            DropDownList ddl8 = (DropDownList)row.FindControl("DropDownList8");
            DropDownList ddl9 = (DropDownList)row.FindControl("DropDownList9");
            DropDownList ddl10 = (DropDownList)row.FindControl("DropDownList10");
            DropDownList ddl11 = (DropDownList)row.FindControl("DropDownList11");
            DropDownList ddl12 = (DropDownList)row.FindControl("DropDownList12");
            DropDownList ddl13 = (DropDownList)row.FindControl("DropDownList13");
            DropDownList ddl14 = (DropDownList)row.FindControl("DropDownList14");
            DropDownList ddl15 = (DropDownList)row.FindControl("DropDownList15");
            DropDownList ddl16 = (DropDownList)row.FindControl("DropDownList16");
            DropDownList ddl17 = (DropDownList)row.FindControl("DropDownList17");
            DropDownList ddl18 = (DropDownList)row.FindControl("DropDownList18");
            DropDownList ddl19 = (DropDownList)row.FindControl("DropDownList19");
            DropDownList ddl20 = (DropDownList)row.FindControl("DropDownList20");
            DropDownList ddl21 = (DropDownList)row.FindControl("DropDownList21");
            DropDownList ddl22 = (DropDownList)row.FindControl("DropDownList22");
            DropDownList ddl23 = (DropDownList)row.FindControl("DropDownList23");
            DropDownList ddl24 = (DropDownList)row.FindControl("DropDownList24");
            DropDownList ddl25 = (DropDownList)row.FindControl("DropDownList25");
            DropDownList ddl26 = (DropDownList)row.FindControl("DropDownList26");
            DropDownList ddl27 = (DropDownList)row.FindControl("DropDownList27");
            DropDownList ddl28 = (DropDownList)row.FindControl("DropDownList28");
            DropDownList ddl29 = (DropDownList)row.FindControl("DropDownList29");
            DropDownList ddl30 = (DropDownList)row.FindControl("DropDownList30");
            DropDownList ddl31 = (DropDownList)row.FindControl("DropDownList31");

            d.operation("Update shift_calendar set DAY01='" + ddl1.SelectedValue + "', DAY02='" + ddl2.SelectedValue + "', DAY03='" + ddl3.SelectedValue + "', DAY04='" + ddl4.SelectedValue + "', DAY05='" + ddl5.SelectedValue + "', DAY06='" + ddl6.SelectedValue + "', DAY07='" + ddl7.SelectedValue + "', DAY08='" + ddl8.SelectedValue + "', DAY09='" + ddl9.SelectedValue + "', DAY10='" + ddl10.SelectedValue + "', DAY11='" + ddl11.SelectedValue + "', DAY12='" + ddl12.SelectedValue + "', DAY13='" + ddl13.SelectedValue + "', DAY14='" + ddl14.SelectedValue + "', DAY15='" + ddl15.SelectedValue + "', DAY16='" + ddl16.SelectedValue + "', DAY17='" + ddl17.SelectedValue + "', DAY18='" + ddl18.SelectedValue + "', DAY19='" + ddl19.SelectedValue + "', DAY20='" + ddl20.SelectedValue + "', DAY21='" + ddl21.SelectedValue + "', DAY22='" + ddl22.SelectedValue + "', DAY23='" + ddl23.SelectedValue + "', DAY24='" + ddl24.SelectedValue + "', DAY25='" + ddl25.SelectedValue + "', DAY26='" + ddl26.SelectedValue + "', DAY27='" + ddl27.SelectedValue + "', DAY28='" + ddl28.SelectedValue + "', DAY29='" + ddl29.SelectedValue + "', DAY30='" + ddl30.SelectedValue + "', DAY31='" + ddl31.SelectedValue + "' where comp_code = '" + Session["comp_code"].ToString() + "' and month = '" + hidden_month.Value + "' and year = '" + hidden_year.Value + "' and emp_code = '" + Convert.ToString(shiftcalendar.DataKeys[row.RowIndex].Value) + "'");
            Sendemail(Convert.ToString(shiftcalendar.DataKeys[row.RowIndex].Value), int.Parse(hidden_month.Value), int.Parse(hidden_year.Value));
        }
    }


    protected void shiftcalendar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            DropDownList ddlCategories1 = e.Row.FindControl("DropDownList1") as DropDownList;
            DropDownList ddlCategories2 = e.Row.FindControl("DropDownList2") as DropDownList;
            DropDownList ddlCategories3 = e.Row.FindControl("DropDownList3") as DropDownList;
            DropDownList ddlCategories4 = e.Row.FindControl("DropDownList4") as DropDownList;
            DropDownList ddlCategories5 = e.Row.FindControl("DropDownList5") as DropDownList;
            DropDownList ddlCategories6 = e.Row.FindControl("DropDownList6") as DropDownList;
            DropDownList ddlCategories7 = e.Row.FindControl("DropDownList7") as DropDownList;
            DropDownList ddlCategories8 = e.Row.FindControl("DropDownList8") as DropDownList;
            DropDownList ddlCategories9 = e.Row.FindControl("DropDownList9") as DropDownList;
            DropDownList ddlCategories10 = e.Row.FindControl("DropDownList10") as DropDownList;
            DropDownList ddlCategories11 = e.Row.FindControl("DropDownList11") as DropDownList;
            DropDownList ddlCategories12 = e.Row.FindControl("DropDownList12") as DropDownList;
            DropDownList ddlCategories13 = e.Row.FindControl("DropDownList13") as DropDownList;
            DropDownList ddlCategories14 = e.Row.FindControl("DropDownList14") as DropDownList;
            DropDownList ddlCategories15 = e.Row.FindControl("DropDownList15") as DropDownList;
            DropDownList ddlCategories16 = e.Row.FindControl("DropDownList16") as DropDownList;
            DropDownList ddlCategories17 = e.Row.FindControl("DropDownList17") as DropDownList;
            DropDownList ddlCategories18 = e.Row.FindControl("DropDownList18") as DropDownList;
            DropDownList ddlCategories19 = e.Row.FindControl("DropDownList19") as DropDownList;
            DropDownList ddlCategories20 = e.Row.FindControl("DropDownList20") as DropDownList;
            DropDownList ddlCategories21 = e.Row.FindControl("DropDownList21") as DropDownList;
            DropDownList ddlCategories22 = e.Row.FindControl("DropDownList22") as DropDownList;
            DropDownList ddlCategories23 = e.Row.FindControl("DropDownList23") as DropDownList;
            DropDownList ddlCategories24 = e.Row.FindControl("DropDownList24") as DropDownList;
            DropDownList ddlCategories25 = e.Row.FindControl("DropDownList25") as DropDownList;
            DropDownList ddlCategories26 = e.Row.FindControl("DropDownList26") as DropDownList;
            DropDownList ddlCategories27 = e.Row.FindControl("DropDownList27") as DropDownList;
            DropDownList ddlCategories28 = e.Row.FindControl("DropDownList28") as DropDownList;
            DropDownList ddlCategories29 = e.Row.FindControl("DropDownList29") as DropDownList;
            DropDownList ddlCategories30 = e.Row.FindControl("DropDownList30") as DropDownList;
            DropDownList ddlCategories31 = e.Row.FindControl("DropDownList31") as DropDownList;

            //ddlCategories1.Items.Insert(0, new ListItem("Select Shift", "0"));
            if (ddlCategories1 != null)
            {
                ddlCategories1.SelectedValue = drv["DAY01"].ToString();
            }
            else { ddlCategories1.SelectedValue = "0"; }
            if (ddlCategories2 != null)
            {
                ddlCategories2.SelectedValue = drv["DAY02"].ToString();
            }
            else { ddlCategories2.SelectedValue = "0"; }
            if (ddlCategories3 != null)
            {
                ddlCategories3.SelectedValue = drv["DAY03"].ToString();
            }
            else { ddlCategories3.SelectedValue = "0"; }
            if (ddlCategories4 != null)
            {
                ddlCategories4.SelectedValue = drv["DAY04"].ToString();
            }
            else { ddlCategories4.SelectedValue = "0"; }
            if (ddlCategories5 != null)
            {
                ddlCategories5.SelectedValue = drv["DAY05"].ToString();
            }
            else { ddlCategories5.SelectedValue = "0"; }
            if (ddlCategories6 != null)
            {
                ddlCategories6.SelectedValue = drv["DAY06"].ToString();
            }
            else { ddlCategories6.SelectedValue = "0"; }
            if (ddlCategories7 != null)
            {
                ddlCategories7.SelectedValue = drv["DAY07"].ToString();
            }
            else { ddlCategories7.SelectedValue = "0"; }
            if (ddlCategories8 != null)
            {
                ddlCategories8.SelectedValue = drv["DAY08"].ToString();
            }
            else { ddlCategories8.SelectedValue = "0"; }
            if (ddlCategories9 != null)
            {
                ddlCategories9.SelectedValue = drv["DAY09"].ToString();
            }
            else { ddlCategories9.SelectedValue = "0"; }
            if (ddlCategories10 != null)
            {
                ddlCategories10.SelectedValue = drv["DAY10"].ToString();
            }
            else { ddlCategories10.SelectedValue = "0"; }
            if (ddlCategories11 != null)
            {
                ddlCategories11.SelectedValue = drv["DAY11"].ToString();
            }
            else { ddlCategories11.SelectedValue = "0"; }
            if (ddlCategories2 != null)
            {
                ddlCategories12.SelectedValue = drv["DAY12"].ToString();
            }
            else { ddlCategories12.SelectedValue = "0"; }
            if (ddlCategories13 != null)
            {
                ddlCategories13.SelectedValue = drv["DAY13"].ToString();
            }
            else { ddlCategories13.SelectedValue = "0"; }
            if (ddlCategories14 != null)
            {
                ddlCategories14.SelectedValue = drv["DAY14"].ToString();
            }
            else { ddlCategories14.SelectedValue = "0"; }
            if (ddlCategories15 != null)
            {
                ddlCategories15.SelectedValue = drv["DAY15"].ToString();
            }
            else { ddlCategories15.SelectedValue = "0"; }
            if (ddlCategories16 != null)
            {
                ddlCategories16.SelectedValue = drv["DAY16"].ToString();
            }
            else { ddlCategories16.SelectedValue = "0"; }
            if (ddlCategories17 != null)
            {
                ddlCategories17.SelectedValue = drv["DAY17"].ToString();
            }
            else { ddlCategories17.SelectedValue = "0"; }
            if (ddlCategories18 != null)
            {
                ddlCategories18.SelectedValue = drv["DAY18"].ToString();
            }
            else { ddlCategories18.SelectedValue = "0"; }
            if (ddlCategories19 != null)
            {
                ddlCategories19.SelectedValue = drv["DAY19"].ToString();
            }
            else { ddlCategories19.SelectedValue = "0"; }
            if (ddlCategories20 != null)
            {
                ddlCategories20.SelectedValue = drv["DAY20"].ToString();
            }
            else { ddlCategories20.SelectedValue = "0"; }
            if (ddlCategories21 != null)
            {
                ddlCategories21.SelectedValue = drv["DAY21"].ToString();
            }
            else { ddlCategories21.SelectedValue = "0"; }
            if (ddlCategories22 != null)
            {
                ddlCategories22.SelectedValue = drv["DAY22"].ToString();
            }
            else { ddlCategories22.SelectedValue = "0"; }
            if (ddlCategories23 != null)
            {
                ddlCategories23.SelectedValue = drv["DAY23"].ToString();
            }
            else { ddlCategories23.SelectedValue = "0"; }
            if (ddlCategories24 != null)
            {
                ddlCategories24.SelectedValue = drv["DAY24"].ToString();
            }
            else { ddlCategories24.SelectedValue = "0"; }
            if (ddlCategories25 != null)
            {
                ddlCategories25.SelectedValue = drv["DAY25"].ToString();
            }
            else { ddlCategories25.SelectedValue = "0"; }
            if (ddlCategories26 != null)
            {
                ddlCategories26.SelectedValue = drv["DAY26"].ToString();
            }
            else { ddlCategories26.SelectedValue = "0"; }
            if (ddlCategories27 != null)
            {
                ddlCategories27.SelectedValue = drv["DAY27"].ToString();
            }
            else { ddlCategories27.SelectedValue = "0"; }
            if (ddlCategories28 != null)
            {
                ddlCategories28.SelectedValue = drv["DAY28"].ToString();
            }
            else { ddlCategories28.SelectedValue = "0"; }
            if (ddlCategories29 != null)
            {
                ddlCategories29.SelectedValue = drv["DAY29"].ToString();
            }
            else { ddlCategories29.SelectedValue = "0"; }
            if (ddlCategories30 != null)
            {
                ddlCategories30.SelectedValue = drv["DAY30"].ToString();
            }
            else { ddlCategories30.SelectedValue = "0"; }
            if (ddlCategories31 != null)
            {
                ddlCategories31.SelectedValue = drv["DAY31"].ToString();
            }
            else { ddlCategories31.SelectedValue = "0"; }
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[10].Visible = false;

        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;
        e.Row.Cells[16].Visible = false;
        e.Row.Cells[17].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;

        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
        e.Row.Cells[24].Visible = false;
        e.Row.Cells[25].Visible = false;
        e.Row.Cells[26].Visible = false;
        e.Row.Cells[27].Visible = false;
        e.Row.Cells[28].Visible = false;
        e.Row.Cells[29].Visible = false;
        e.Row.Cells[30].Visible = false;
        e.Row.Cells[31].Visible = false;
        e.Row.Cells[63].Visible = false;

        int days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value));

        if (days == 30) {
            e.Row.Cells[62].Visible = false;
        }
        else if (days == 29)
        {
            e.Row.Cells[61].Visible = false;
            e.Row.Cells[62].Visible = false;
        }
        else if (days == 28)
        {
            e.Row.Cells[60].Visible = false;
            e.Row.Cells[61].Visible = false;
            e.Row.Cells[62].Visible = false;
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (hidden_month.Value.Equals("1")) { gridcalender_update(12, (int.Parse(hidden_year.Value) - 1)); }
        else { gridcalender_update((int.Parse(hidden_month.Value) - 1), int.Parse(hidden_year.Value)); }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (hidden_month.Value.Equals("12")) { gridcalender_update(1, (int.Parse(hidden_year.Value) + 1)); }
        else { gridcalender_update((int.Parse(hidden_month.Value) + 1), int.Parse(hidden_year.Value)); }
    }
    private void update_label(int month, int year)
    {
        if (month.Equals(0) && year.Equals(0))
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT MONTH(now()), YEAR(now())", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lbl_month_year.Text = getmonth(dr.GetValue(0).ToString()) + " " + dr.GetValue(1).ToString();
                hidden_month.Value = dr.GetValue(0).ToString();
                hidden_year.Value = dr.GetValue(1).ToString();
            }
            dr.Close();
            cmd.Dispose();
        }
        else
        {
            lbl_month_year.Text = getmonth(month.ToString()) + " " + year;
            hidden_month.Value = month.ToString();
            hidden_year.Value = year.ToString();
            
        }
    }
    private void loadheaders(int month, int year)
    {
        DateTime dt = new DateTime(year, month, 1);
        shiftcalendar.HeaderRow.Cells[32].Text = "1 " + dt.AddDays(0).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[33].Text = "2 " + dt.AddDays(1).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[34].Text = "3 " + dt.AddDays(2).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[35].Text = "4 " + dt.AddDays(3).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[36].Text = "5 " + dt.AddDays(4).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[37].Text = "6 " + dt.AddDays(5).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[38].Text = "7 " + dt.AddDays(6).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[39].Text = "8 " + dt.AddDays(7).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[40].Text = "9 " + dt.AddDays(8).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[41].Text = "10 " + dt.AddDays(9).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[42].Text = "11 " + dt.AddDays(10).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[43].Text = "12 " + dt.AddDays(11).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[44].Text = "13 " + dt.AddDays(12).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[45].Text = "14 " + dt.AddDays(13).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[46].Text = "15 " + dt.AddDays(14).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[47].Text = "16 " + dt.AddDays(15).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[48].Text = "17 " + dt.AddDays(16).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[49].Text = "18 " + dt.AddDays(17).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[50].Text = "19 " + dt.AddDays(18).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[51].Text = "20 " + dt.AddDays(19).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[52].Text = "21 " + dt.AddDays(20).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[53].Text = "22 " + dt.AddDays(21).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[54].Text = "23 " + dt.AddDays(22).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[55].Text = "24 " + dt.AddDays(23).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[56].Text = "25 " + dt.AddDays(24).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[57].Text = "26 " + dt.AddDays(25).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[58].Text = "27 " + dt.AddDays(26).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[59].Text = "28 " + dt.AddDays(27).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[60].Text = "29 " + dt.AddDays(28).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[61].Text = "30 " + dt.AddDays(29).DayOfWeek.ToString().Substring(0, 3).ToUpper();
        shiftcalendar.HeaderRow.Cells[62].Text = "31 " + dt.AddDays(30).DayOfWeek.ToString().Substring(0, 3).ToUpper();


    }
    private string getmonth(string month)
    {
        if (month == "1")
        {
            return "JAN";
        }
        else if (month == "2")
        { return "FEB"; }
        else if (month == "3")
        { return "MAR"; }
        else if (month == "4")
        { return "APR"; }
        else if (month == "5")
        { return "MAY"; }
        else if (month == "6")
        { return "JUN"; }
        else if (month == "7")
        { return "JUL"; }
        else if (month == "8")
        { return "AUG"; }
        else if (month == "9")
        { return "SEP"; }
        else if (month == "10")
        { return "OCT"; }
        else if (month == "11")
        { return "NOV"; }
        else if (month == "12")
        { return "DEC"; }
        return "";

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        gridcalender_update(int.Parse(hidden_month.Value), int.Parse(hidden_year.Value));
    }




    private void count_Shift_Employee()
    {


        //d.con1.Open();
        //String currMonth = txt_fromdate.Text.Substring(0, 2);

        //String UnitList = "";
        //string strQuery = "";

        //if (ddl_reporting_to.SelectedValue.ToString() == "ALL")
        //{
        //    strQuery = "SELECT (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day" + currMonth + ") as 'Shift_Name', count(*) as Number_Of_Employee from  shift_calendar B  group by Shift_Name";
        //}
        //else
        //{
        //    foreach (ListItem listItem in ddl_reporting_to.Items)
        //    {
        //        if (listItem.Selected == true)
        //        {
        //            UnitList += "'" + listItem.Text + "',";
        //        }
        //    }
        //    UnitList = UnitList.Substring(0, UnitList.Length - 1);

        //    strQuery = "SELECT (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day20) as 'Shift_Name', count(*) as Number_Of_Employee from  shift_calendar B inner join pay_employee_master as c on b.emp_code = c.emp_code where c.reporting_to in (select pay_employee_master.Emp_Code from pay_employee_master where pay_employee_master.emp_name in (" + UnitList + ")) group by Shift_Name";
        //}

        //MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, d.con1);
        //DataSet ds = new DataSet();
        //adapter.Fill(ds);


        //UnitGrid_PF.DataSource = ds;

        //UnitGrid_PF.DataBind();

        //UnitGrid_PF.Visible = true;

        //d.con1.Close();
        //ModalPopupExtender2.Show();

        System.Data.DataTable dt6 = new System.Data.DataTable();
        dt6.Columns.Add("Shift", typeof(string));
        dt6.Columns.Add("1", typeof(string));
        dt6.Columns.Add("2", typeof(string));
        dt6.Columns.Add("3", typeof(string));
        dt6.Columns.Add("4", typeof(string));
        dt6.Columns.Add("5", typeof(string));
        dt6.Columns.Add("6", typeof(string));
        dt6.Columns.Add("7", typeof(string));
        dt6.Columns.Add("8", typeof(string));
        dt6.Columns.Add("9", typeof(string));
        dt6.Columns.Add("10", typeof(string));
        dt6.Columns.Add("11", typeof(string));
        dt6.Columns.Add("12", typeof(string));
        dt6.Columns.Add("13", typeof(string));
        dt6.Columns.Add("14", typeof(string));
        dt6.Columns.Add("15", typeof(string));
        dt6.Columns.Add("16", typeof(string));
        dt6.Columns.Add("17", typeof(string));
        dt6.Columns.Add("18", typeof(string));
        dt6.Columns.Add("19", typeof(string));
        dt6.Columns.Add("20", typeof(string));
        dt6.Columns.Add("21", typeof(string));
        dt6.Columns.Add("22", typeof(string));
        dt6.Columns.Add("23", typeof(string));
        dt6.Columns.Add("24", typeof(string));
        dt6.Columns.Add("25", typeof(string));
        dt6.Columns.Add("26", typeof(string));
        dt6.Columns.Add("27", typeof(string));
        dt6.Columns.Add("28", typeof(string));
        int days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value));

        if (days == 29)
        { dt6.Columns.Add("29", typeof(string)); }
        else if (days == 30)
        {
            dt6.Columns.Add("29", typeof(string));
            dt6.Columns.Add("30", typeof(string));
        }
        else if (days == 31)
        {
            dt6.Columns.Add("29", typeof(string));
            dt6.Columns.Add("30", typeof(string));
            dt6.Columns.Add("31", typeof(string));
        }
        

        lbl_header.Text = "Employee Count for " + getmonth(hidden_month.Value) + " - " + hidden_year.Value;

        d.con.Open();

        try
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT shift_name from pay_shift_master where comp_code = '" + Session["comp_code"].ToString() + "' and UNIT_CODE = '" + Session["UNIT_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                DataRow dr_pf = dt6.NewRow();
                dr_pf["Shift"] = dr1.GetValue(0).ToString();
                              
                d.con1.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT shift_name, count(*) as count, '1' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY01 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '2' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY02 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '3' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY03 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '4' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY04 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '5' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY05 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '6' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY06 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '7' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY07 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '8' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY08 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '9' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY09 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '10' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY10 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '11' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY11 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '12' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY12 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '13' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY13 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '14' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY14 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '15' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY15 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '16' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY16 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '17' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY17 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '18' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY18 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '19' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY19 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '20' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY20 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '21' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY21 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '22' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY22 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '23' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY23 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '24' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY24 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '25' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY25 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '26' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY26 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '27' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY27 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '28' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY28 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '29' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY29 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '30' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY30 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID union SELECT shift_name, count(*) as count, '31' as day FROM shift_calendar hc LEFT JOIN pay_shift_master hv	 on hc.DAY31 = hv.ID   where month='" + hidden_month.Value + "' and year='" + hidden_year.Value + "' and shift_name is not null group by hv.ID", d.con1);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("1"))
                        {
                        dr_pf["1"] = dr.GetValue(1).ToString();}
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("2"))
                        {
                            dr_pf["2"] = dr.GetValue(1).ToString();
                        }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("3"))
                        {
                            dr_pf["3"] = dr.GetValue(1).ToString();
                        }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("4"))
                        {
                            dr_pf["4"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("5"))
                        {
                        dr_pf["5"] = dr.GetValue(1).ToString();
                             }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("6"))
                        {
                        dr_pf["6"] = dr.GetValue(1).ToString(); }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("7"))
                        {
                        dr_pf["7"] = dr.GetValue(1).ToString(); }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("8"))
                        {
                        dr_pf["8"] = dr.GetValue(1).ToString(); }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("9"))
                        {
                        dr_pf["9"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("10"))
                        {dr_pf["10"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("11"))
                        {dr_pf["11"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("12"))
                        {dr_pf["12"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("13"))
                        {dr_pf["13"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("14"))
                        {dr_pf["14"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("15"))
                        {dr_pf["15"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("16"))
                        {dr_pf["16"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("17"))
                        {dr_pf["17"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("18"))
                        {dr_pf["18"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("19"))
                        {dr_pf["19"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("20"))
                        {dr_pf["20"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("21"))
                        {dr_pf["21"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("22"))
                        {dr_pf["22"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("23"))
                        {dr_pf["23"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("24"))
                        {dr_pf["24"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("25"))
                        {dr_pf["25"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("26"))
                        {dr_pf["26"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("27"))
                        {dr_pf["27"] = dr.GetValue(1).ToString();
                         }
                        if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("28"))
                        {dr_pf["28"] = dr.GetValue(1).ToString();
                         }

                        if (days == 29)
                        {
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("29"))
                            {
                                dr_pf["29"] = dr.GetValue(1).ToString();
                            }
                        }
                        else if (days == 30)
                        {
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("29"))
                            {
                                dr_pf["29"] = dr.GetValue(1).ToString();
                            }
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("30"))
                            {
                                dr_pf["30"] = dr.GetValue(1).ToString();
                            }
                        }
                        else if (days == 31)
                        {
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("29"))
                            {
                                dr_pf["29"] = dr.GetValue(1).ToString();
                            }
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("30"))
                            {
                                dr_pf["30"] = dr.GetValue(1).ToString();
                            }
                            if (dr1.GetValue(0).ToString().Equals(dr.GetValue(0).ToString()) && dr.GetValue(2).ToString().Equals("31"))
                            { dr_pf["31"] = dr.GetValue(1).ToString(); }
                        }

                    }
                    dr.Close();
                    cmd.Dispose();

                }
                catch (Exception ex) { throw ex; }
                finally { d.con1.Close(); }
                dt6.Rows.Add(dr_pf);
            }
            dr1.Close();
            cmd1.Dispose();

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        UnitGrid_PF.DataSource = dt6;

        UnitGrid_PF.DataBind();

        UnitGrid_PF.Visible = true;
        ModalPopupExtender2.Show();

    }

    //private void reporting_to_Employee()
    //{


    //    d.con.Close();

    //    d.con.Open();
    //    try
    //    {
    //        DataTable dt_gv = new DataTable();
    //        MySqlCommand cmd_gv = new MySqlCommand("select DISTINCT(Reporting_to) as emp_code,emp_name from pay_employee_master where comp_code='" + Session["comp_code"].ToString() + "' and UNIT_CODE='" + Session["UNIT_CODE"].ToString() + "' group by emp_code ", d.con);
    //        MySqlDataAdapter dr_gv = new MySqlDataAdapter(cmd_gv);
    //        dr_gv.Fill(dt_gv);
    //        ddl_reporting_to.DataSource = dt_gv;
    //        ddl_reporting_to.DataBind();
    //        ddl_reporting_to.Items.Insert(0, "ALL");
    //        ddl_reporting_to.SelectedIndex = 0;
    //        dr_gv.Dispose();
    //        cmd_gv.Dispose();
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

    protected void all_employee_excel(object sender, EventArgs e)
    {

        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
       // xla.Columns.ColumnWidth = 20;

        //Microsoft.Office.Interop.Excel.Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[500, 300]);
        //rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


        Microsoft.Office.Interop.Excel.Range rng = ws.get_Range("E1:G1");
        rng.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkGreen;

        Microsoft.Office.Interop.Excel.Range formateRange2 = ws.get_Range("E1:G1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //ws.Range["E1"].Text = "Large size";

       // formateRange2.Font.Size = 20;
        //formateRange2.Font.IsBold = true;


        Microsoft.Office.Interop.Excel.Range rng1 = ws.get_Range("E2:I2");
        rng1.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;

        Microsoft.Office.Interop.Excel.Range formateRange1 = ws.get_Range("E2:I2");
        formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
       // formateRange1.Font.Size = 20;


        // Microsoft.Office.Interop.Excel.Range rng4 = ws.get_Range("C3:L3");
        //rng4.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
        //rng4.Font.Size = 15;

        // Microsoft.Office.Interop.Excel.Range formateRange4 = ws.get_Range("C3:L3");
        // formateRange4.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        // formateRange4.Font.Size = 15;

        Microsoft.Office.Interop.Excel.Range rng2 = ws.get_Range("A4:AK4");
        rng2.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlue;
       // rng2.Font.Size = 15;

        Microsoft.Office.Interop.Excel.Range formateRange = ws.get_Range("A4:AK4");
        formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
       // formateRange.Font.Size = 15;


        ws.Cells[1, 5] = "Company : " + Session["COMP_NAME"].ToString();
        ws.Cells[2, 5] = "Employee Shift For Month " + getmonth(hidden_month.Value) + " Year " + hidden_year.Value;

        ws.Cells[4, 1] = "EMPLOYEE NAME";
        ws.Cells[4, 2] = "DAY01";
        ws.Cells[4, 3] = "DAY02";
        ws.Cells[4, 4] = "DAY03";
        ws.Cells[4, 5] = "DAY04";
        ws.Cells[4, 6] = "DAY05";
        ws.Cells[4, 7] = "DAY06";
        ws.Cells[4, 8] = "DAY07";
        ws.Cells[4, 9] = "DAY08";
        ws.Cells[4, 10] = "DAY09";
        ws.Cells[4, 11] = "DAY10";
        ws.Cells[4, 12] = "DAY11";
        ws.Cells[4, 13] = "DAY12";
        ws.Cells[4, 14] = "DAY13";
        ws.Cells[4, 15] = "DAY14";
        ws.Cells[4, 16] = "DAY15";
        ws.Cells[4, 17] = "DAY16";
        ws.Cells[4, 18] = "DAY17";
        ws.Cells[4, 19] = "DAY18";
        ws.Cells[4, 20] = "DAY19";
        ws.Cells[4, 21] = "DAY20";
        ws.Cells[4, 22] = "DAY21";
        ws.Cells[4, 23] = "DAY22";
        ws.Cells[4, 24] = "DAY23";
        ws.Cells[4, 25] = "DAY24";
        ws.Cells[4, 26] = "DAY25";
        ws.Cells[4, 27] = "DAY26";
        ws.Cells[4, 28] = "DAY27";
        ws.Cells[4, 29] = "DAY28";
        int days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value));

        if (days == 29)
        { ws.Cells[4, 30] = "DAY29"; }
        else if (days == 30)
        { ws.Cells[4, 30] = "DAY29";
        ws.Cells[4, 31] = "DAY30";
        }
        else if (days == 31)
        {
            ws.Cells[4, 30] = "DAY29";
            ws.Cells[4, 31] = "DAY30";
            ws.Cells[4, 32] = "DAY31";
        }

        try
        {
            d.con1.Open();
            DataSet ds2 = new DataSet();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT (SELECT emp_name FROM pay_employee_master AS C WHERE C.emp_code = B.emp_code) AS 'Emp_name', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day01) AS 'day01', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day02) AS 'day02', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day03) AS 'day03', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day04) AS 'day04', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day05) AS 'day05', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day06) AS 'day06', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day07) AS 'day07', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day08) AS 'day08', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day09) AS 'day09', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day10) AS 'day10', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day11) AS 'day11', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day12) AS 'day12', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day13) AS 'day13', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day14) AS 'day14', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day15) AS 'day15', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day16) AS 'day16', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day17) AS 'day17', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day18) AS 'day18', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day19) AS 'day19', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day20) AS 'day20', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day21) AS 'day21', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day22) AS 'day22', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day23) AS 'day23', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day24) AS 'day24', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day25) AS 'day25', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day26) AS 'day26', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day27) AS 'day27', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day28) AS 'day28', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day29) AS 'day29', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day30) AS 'day30', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day31) AS 'day31' FROM shift_calendar AS B  where B.month = '" + hidden_month.Value + "' and B.year = '" + hidden_year.Value + "' and comp_code='"+Session["comp_code"].ToString()+"'", d.con1);
            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 5;

            foreach (System.Data.DataRow row in dt.Rows)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    if (row[i].ToString() == "")
                    { ws.Cells[j, i + 1] = " "; }
                    else
                    { ws.Cells[j, i + 1] = row[i].ToString(); }
                    //ws.Cells[j, i + 1] = row[i].ToString();

                }
                j++;

            }
            //ws.Range["A6"].Locked =false;
            //ws.get_Range("A1", "Q12").Style.Locked = true;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
        finally
        {
            xla.Visible = true;
            d.con1.Close();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void LinkButtonshift_Click(object sender, EventArgs e)
    {
        count_Shift_Employee();
        ModalPopupExtender2.Show();

       // reporting_to_Employee();
    }

    protected void reporting_excel(object sender, EventArgs e)
    {


        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
        // xla.Columns.ColumnWidth = 20;

        //Microsoft.Office.Interop.Excel.Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[500, 300]);
        //rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


        Microsoft.Office.Interop.Excel.Range rng = ws.get_Range("B1:F1");
        rng.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbDarkGreen;

        Microsoft.Office.Interop.Excel.Range formateRange2 = ws.get_Range("B1:F1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //ws.Range["E1"].Text = "Large size";

        // formateRange2.Font.Size = 20;
        //formateRange2.Font.IsBold = true;


        Microsoft.Office.Interop.Excel.Range rng1 = ws.get_Range("B2:G2");
        rng1.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;

        Microsoft.Office.Interop.Excel.Range formateRange1 = ws.get_Range("B2:G2");
        formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        //formateRange1.Font.Size = 20;


        // Microsoft.Office.Interop.Excel.Range rng4 = ws.get_Range("C3:L3");
        //rng4.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
        //rng4.Font.Size = 15;

        // Microsoft.Office.Interop.Excel.Range formateRange4 = ws.get_Range("C3:L3");
        // formateRange4.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        // formateRange4.Font.Size = 15;

        Microsoft.Office.Interop.Excel.Range rng2 = ws.get_Range("A4:AF4");
        rng2.Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlue;
        //rng2.Font.Size = 15;

        Microsoft.Office.Interop.Excel.Range formateRange = ws.get_Range("A4:AF4");
        formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        // formateRange.Font.Size = 15;


        ws.Cells[1, 2] = "Company : " + Session["COMP_NAME"].ToString();
        ws.Cells[2, 2] = "Employee Shift For Month " + getmonth(hidden_month.Value) + " Year " + hidden_year.Value;

        ws.Cells[4, 1] = "EMPLOYEE NAME";
        ws.Cells[4, 2] = "DAY01";
        ws.Cells[4, 3] = "DAY02";
        ws.Cells[4, 4] = "DAY03";
        ws.Cells[4, 5] = "DAY04";
        ws.Cells[4, 6] = "DAY05";
        ws.Cells[4, 7] = "DAY06";
        ws.Cells[4, 8] = "DAY07";
        ws.Cells[4, 9] = "DAY08";
        ws.Cells[4, 10] = "DAY09";
        ws.Cells[4, 11] = "DAY10";
        ws.Cells[4, 12] = "DAY11";
        ws.Cells[4, 13] = "DAY12";
        ws.Cells[4, 14] = "DAY13";
        ws.Cells[4, 15] = "DAY14";
        ws.Cells[4, 16] = "DAY15";
        ws.Cells[4, 17] = "DAY16";
        ws.Cells[4, 18] = "DAY17";
        ws.Cells[4, 19] = "DAY18";
        ws.Cells[4, 20] = "DAY19";
        ws.Cells[4, 21] = "DAY20";
        ws.Cells[4, 22] = "DAY21";
        ws.Cells[4, 23] = "DAY22";
        ws.Cells[4, 24] = "DAY23";
        ws.Cells[4, 25] = "DAY24";
        ws.Cells[4, 26] = "DAY25";
        ws.Cells[4, 27] = "DAY26";
        ws.Cells[4, 28] = "DAY27";
        ws.Cells[4, 29] = "DAY28";
        int days = DateTime.DaysInMonth(int.Parse(hidden_year.Value), int.Parse(hidden_month.Value));

        if (days == 29)
        { ws.Cells[4, 30] = "DAY29"; }
        else if (days == 30)
        {
            ws.Cells[4, 30] = "DAY29";
            ws.Cells[4, 31] = "DAY30";
        }
        else if (days == 31)
        {
            ws.Cells[4, 30] = "DAY29";
            ws.Cells[4, 31] = "DAY30";
            ws.Cells[4, 32] = "DAY31";
        }




        try
        {
            d.con1.Open();
            DataSet ds2 = new DataSet();
            // MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT pay_attendance_muster.comp_code, pay_attendance_muster.UNIT_CODE, pay_attendance_muster.EMP_CODE, pay_employee_master .EMP_NAME, pay_attendance_muster.DAY01, pay_attendance_muster.DAY02, pay_attendance_muster.DAY03, pay_attendance_muster.DAY04, pay_attendance_muster.DAY05, pay_attendance_muster.DAY06, pay_attendance_muster.DAY07, pay_attendance_muster.DAY08, pay_attendance_muster.DAY09, pay_attendance_muster.DAY10, pay_attendance_muster.DAY11, pay_attendance_muster.DAY12, pay_attendance_muster.DAY13, pay_attendance_muster.DAY14, pay_attendance_muster.DAY15, pay_attendance_muster.DAY16, pay_attendance_muster.DAY17, pay_attendance_muster.DAY18, pay_attendance_muster.DAY19, pay_attendance_muster.DAY20, pay_attendance_muster.DAY21, pay_attendance_muster.DAY22, pay_attendance_muster.DAY23, pay_attendance_muster.DAY24, pay_attendance_muster.DAY25, pay_attendance_muster.DAY26, pay_attendance_muster.DAY27, pay_attendance_muster.DAY28, pay_attendance_muster.DAY29, pay_attendance_muster.DAY30, pay_attendance_muster.DAY31, pay_attendance_muster.TOT_DAYS_PRESENT,pay_attendance_muster.TOT_DAYS_ABSENT, pay_attendance_muster.TOT_CL,pay_attendance_muster.TOT_PL,pay_attendance_muster.TOT_MATERNITY,pay_attendance_muster.TOT_PATERNITY,pay_attendance_muster.TOT_LEAVES, pay_attendance_muster.WEEKLY_OFF, pay_attendance_muster.HOLIDAYS, pay_attendance_muster.TOT_CO,pay_attendance_muster.TOT_WORKING_DAYS, concat('''',pay_attendance_muster.MONTH) as Month, pay_attendance_muster.YEAR,(select emp_name from pay_employee_master a where a.emp_code = pay_employee_master.REPORTING_TO) As 'Reporting To' FROM pay_attendance_muster,pay_employee_master  WHERE pay_attendance_muster .EMP_CODE =pay_employee_master .EMP_CODE  AND pay_attendance_muster .comp_code ='" + Session["comp_code"].ToString() + "'  AND pay_attendance_muster .UNIT_CODE ='" + ddl_unitcode.SelectedValue.ToString().Substring(0, 4) + "' and month='" + month + "' and year='" + year + "' ORDER BY  pay_employee_master .EMP_CODE", d.con1);
            // MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT B.comp_code, B.unit_code,(select emp_name from pay_employee_master as A where A.emp_code=B.emp_code) as Emp_name, B.month, B.year,(select emp_name from pay_employee_master a where a.emp_code = pay_employee_master.REPORTING_TO) As 'Reporting To',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day01) AS 'day01',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day02) AS 'day02',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day03) AS 'day03',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day04) AS 'day04',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day05) AS 'day05',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day06) AS 'day06',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day07) AS 'day07',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day08) AS 'day08',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day09) AS 'day09',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day10) AS 'day10',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day11) AS 'day11',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day12) AS 'day12',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day13) AS 'day13',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day14) AS 'day14',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day15) AS 'day15',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day16) AS 'day16',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day17) AS 'day17',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day18) AS 'day18',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day19) AS 'day19',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day20) AS 'day20',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day21) AS 'day21',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day22) AS 'day22',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day23) AS 'day23',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day24) AS 'day24',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day25) AS 'day25',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day26) AS 'day26',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day27) AS 'day27',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day28) AS 'day28',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day29) AS 'day29',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day30) AS 'day30',(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day31) AS 'day31' FROM shift_calendar AS B, pay_employee_master   where B.month = '" + hidden_month.Value + "' and B.year = '" + hidden_year.Value + "'", d.con1);
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT C.Emp_name, (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day01) AS 'day01', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day02) AS 'day02', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day03) AS 'day03', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day04) AS 'day04', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day05) AS 'day05', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day06) AS 'day06', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day07) AS 'day07', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day08) AS 'day08', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day09) AS 'day09', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day10) AS 'day10', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day11) AS 'day11', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day12) AS 'day12', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day13) AS 'day13', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day14) AS 'day14', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day15) AS 'day15', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day16) AS 'day16', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day17) AS 'day17', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day18) AS 'day18', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day19) AS 'day19', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day20) AS 'day20', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day21) AS 'day21', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day22) AS 'day22', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day23) AS 'day23', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day24) AS 'day24', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day25) AS 'day25', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day26) AS 'day26', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day27) AS 'day27', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day28) AS 'day28', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day29) AS 'day29', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day30) AS 'day30', (SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day31) AS 'day31' FROM shift_calendar AS B inner join pay_employee_master as c on b.emp_code = c.emp_code where B.month = '" + hidden_month.Value + "' and B.year = '" + hidden_year.Value + "' and c.reporting_to = '" + Session["LOGIN_ID"].ToString() + "'", d.con1);
            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 5;

            foreach (System.Data.DataRow row in dt.Rows)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    if (row[i].ToString() == "")
                    { ws.Cells[j, i + 1] = " "; }
                    else
                    { ws.Cells[j, i + 1] = row[i].ToString(); }
                    //ws.Cells[j, i + 1] = row[i].ToString();

                }
                j++;

            }
            //ws.Range["A6"].Locked =false;
            //ws.get_Range("A1", "Q12").Style.Locked = true;
        }
        catch (Exception ee)
        {
            Response.Write(ee.Message);
        }
        finally
        {
            xla.Visible = true;
            d.con1.Close();
        }
    }
    private void Sendemail(string emp_code, int month, int year)
    {
        bool sendemail = false;
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select send_email from pay_shift_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"].ToString() + "'", d.con);
        try
        {
            if (((string)cmd.ExecuteScalar()).Equals("1"))
            {
                sendemail = true;
            }

        }
        catch
        {
            throw;
        }
        finally
        {
            d.con.Close();
            cmd.Dispose();
        }
        if (sendemail)
        {
            emp_code = emp_code.Replace("'", "");
            List<string> names = new List<string>(emp_code.Split(','));
            foreach (string item in names)
            {
                try
                {
                    d.SendshiftEmail(Server.MapPath("~/shift_mgmt.htm"), item, month, year);
                }
                catch (Exception ex) { } // dont throw vinod Pol
            }
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void shiftcalendar_PreRender(object sender, EventArgs e)
    {
        
             try
        {
            shiftcalendar.UseAccessibleHeader = false;
            shiftcalendar.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unitcode.DataSource = dt_item;
                    ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unitcode.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unitcode.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_unitcode.Items.Clear();
        }
    }
    protected void btn_process_Click(object sender, EventArgs e)
    {
        if (txt_month_year.Text == "")
        {
            hidden_month.Value = "0";
            hidden_year.Value = "0";
        }
        else
        {
            hidden_month.Value = txt_month_year.Text.Substring(0, 2);
            hidden_year.Value = txt_month_year.Text.Substring(3);
        }
        gridcalender_update(0, 0);
    }
    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}

