
using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class AddNewEmployee_query : System.Web.UI.Page
{
    DAL d = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        b1_Click();
    }
    protected void b1_Click()
    {
        int total = 0;
        d.con.Open();
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select pay_unit_master.state_name as STATE, pay_designation_count.designation as DESIGNATION, pay_designation_count.hours as 'WORKING HOURS', count(pay_designation_count.designation) as COUNT from pay_designation_count inner join pay_unit_master on pay_designation_count.unit_code = pay_unit_master.unit_code where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.client_code = '" + Session["CLIENT_CODE"].ToString() + "' group by pay_unit_master.state_name,pay_designation_count.hours,pay_designation_count.designation", d.con1);
        try
        {
            DataSet DS1 = new DataSet();
            MySqlDataAdapter1.Fill(DS1);
            SearchGridView.DataSource = DS1;
            SearchGridView.DataBind();
           // decimal total = DS1.AsEnumerable().Sum(row => row.Field<decimal>("COUNT"));
           //// int totalq = DS1.AsEnumerable().Sum(row => int.Parse(row.Field("COUNT").ToString()));
           // SearchGridView.FooterRow.Cells[2].Text = "Total";
           // //SearchGridView.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
           // SearchGridView.FooterRow.Cells[3].Text = total.ToString("N2");
            SearchGridView.Visible = true;
            Panel5.Visible = true;
            d.con.Close();
            foreach (DataTable table in DS1.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    total = total + int.Parse(dr["Count"].ToString());
                }
            }

            header_1.Text = "Total Employee Count is " + total;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
}
