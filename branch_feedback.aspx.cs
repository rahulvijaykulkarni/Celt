using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class branch_feedback : System.Web.UI.Page
{
    DAL d = new DAL();
    public string comp_name, branch,month_year;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Action = Request.QueryString["A"];
            ViewState["Cmp_code"] = Action;
            string uni_code = Request.QueryString["B"];
            ViewState["uni_code"] = uni_code;
            string mnth = Request.QueryString["C"];
            ViewState["month"] = mnth;
            string year = Request.QueryString["D"];
            ViewState["year"] = year;
            if (!d.getsinglestring("select feedback1 from client_feedback where  comp_code='" + ViewState["Cmp_code"].ToString() + "' and unit_code = '" + ViewState["uni_code"].ToString() + "' and month=" + ViewState["month"].ToString() + " and year = " + ViewState["year"].ToString()).Equals(""))
            {
                month_year = "IH&MS - Feedback already received for the month of " + getMonth(ViewState["month"].ToString()) + " " + ViewState["year"].ToString();
                hide_panel.Visible = false;
            }
            else
            {
                MySqlCommand cmd2 = new MySqlCommand("select client_name, unit_add2 from pay_unit_master inner join pay_client_master on pay_unit_master.client_code = pay_client_master.client_code and pay_unit_master.comp_code = pay_client_master.comp_code where pay_unit_master.comp_code='" + ViewState["Cmp_code"].ToString() + "' and pay_unit_master.unit_Code = '" + ViewState["uni_code"].ToString() + "'", d.con);
                d.con.Open();
                MySqlDataReader dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    comp_name = dr.GetValue(0).ToString();
                    branch = dr.GetValue(1).ToString();
                }
                month_year = "IH&MS - Feedback for the month of " + getMonth(ViewState["month"].ToString()) + " " + ViewState["year"].ToString();
                hide_panel.Visible = true;
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        d.operation("update client_feedback set feedback1=" + ddl_employee_groom.SelectedValue + ",feedback2=" + ddl_employee_hygiene.SelectedValue + ",feedback3=" + ddl_employee_duty.SelectedValue + ",feedback4=" + ddl_employee_behaviour.SelectedValue + ",feedback5=" + ddl_employee_support.SelectedValue + " where comp_code='" + ViewState["Cmp_code"].ToString() + "' and unit_code = '" + ViewState["uni_code"].ToString() + "' and month=" + ViewState["month"].ToString() + " and year = " + ViewState["year"].ToString());
        hide_panel.Visible = false;
        month_year = "IH&MS - Thank you for your valuable time.";
    }
    private string getMonth(string mnth)
    {
        try
        {
            int month = int.Parse(mnth);
            if (month == 1) { return "January"; }
            else if (month == 2) { return "February"; }
            else if (month == 3) { return "March"; }
            else if (month == 4) { return "April"; }
            else if (month == 5) { return "May"; }
            else if (month == 6) { return "June"; }
            else if (month == 7) { return "July"; }
            else if (month == 8) { return "August"; }
            else if (month == 9) { return "September"; }
            else if (month == 10) { return "October"; }
            else if (month == 11) { return "November"; }
            else if (month == 12) { return "December"; }
            return month.ToString();
        }
        catch { }
        return "";
    
    }
}