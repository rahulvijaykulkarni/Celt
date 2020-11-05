using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

public partial class Datatable : System.Web.UI.Page
{
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d1.getaccess(Session["ROLE"].ToString(), "Search", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        if (!IsPostBack) { fill_ddl(); }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = get_select();
            if (!ddl_tables.SelectedValue.Equals("All"))
            {
                query = query + " and B.UNIT_CODE = '" + ddl_tables.SelectedValue + "'";
            }
            if (!ddl_department.SelectedValue.Equals("All"))
            {
                query = query + " and c.dept_CODE = '" + ddl_department.SelectedValue + "'";
            }
           
            d1.con1.Open();
            MySqlCommand cmd = new MySqlCommand(query, d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            StringBuilder html = new StringBuilder();
            html.Append("<thead>");
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");
            BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con1.Close(); }
    }

    private string get_select()
    {
        string select = "Select emp_name AS 'Name',", mnth = "", yr = "";
        System.DateTime dt1 = System.DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", null);
        System.DateTime dt2 = System.DateTime.ParseExact(txt_to_date.Text, "dd/MM/yyyy", null);
        while (dt1 <= dt2)
        {
            int month = dt1.Month;
            int year = dt1.Year;
            string day = dt1.Day.ToString();
            if (day.Length.Equals(1))
            {
                day = "0" + day;
            }
            if (!ddl_shift.SelectedValue.Equals("All"))
            {
                select = select + "(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = " + ddl_shift.SelectedValue + " and A.id = B.day" + day + ") as " + getmonth(month.ToString()) + day + ",";
            }
            else {
                select = select + "(SELECT shift_name FROM pay_shift_master AS A WHERE A.id = B.day" + day + ") as " + getmonth(month.ToString()) + day + ",";
            }

            if (!mnth.Contains(month.ToString())) { mnth = "'" + month.ToString() + "',"; }
            if (!yr.Contains(year.ToString())) { yr = "'" + year.ToString() + "',"; }

            dt1 = dt1.AddDays(1);
        }
        mnth = mnth.Substring(0, mnth.Length - 1);
        yr = yr.Substring(0, yr.Length - 1);
        return select.Substring(0, select.Length - 1) + " from shift_calendar B inner join pay_employee_master c on B.emp_code = c.emp_code where month in (" + mnth + ") and year in (" + yr + ")";
    
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    private void fill_ddl()
    {
        d1.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT unit_name, unit_code FROM pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ddl_tables.DataSource = ds.Tables[0];
            ddl_tables.DataTextField = "unit_name";
            ddl_tables.DataValueField = "unit_code";
            ddl_tables.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }

        d1.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT dept_name, dept_code FROM pay_department_master where comp_code = '" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ddl_department.DataSource = ds.Tables[0];
            ddl_department.DataTextField = "dept_name";
            ddl_department.DataValueField = "dept_code";
            ddl_department.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }

        d1.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT shift_name, id FROM pay_shift_master where comp_code = '" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ddl_shift.DataSource = ds.Tables[0];
            ddl_shift.DataTextField = "shift_name";
            ddl_shift.DataValueField = "id";
            ddl_shift.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally { d1.con.Close(); }
    
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
}