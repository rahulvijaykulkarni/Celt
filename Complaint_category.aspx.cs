using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
public partial class Complaint_category : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            load_sub_grid();
        }
    }
 
    private void load_sub_grid()
    {
        try
        {
            d1.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("select id,category from pay_complaint_category", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataTable DS1 = new DataTable();
            cad1.Fill(DS1);
            SearchGridView.DataSource = DS1;
            ViewState["Travelling_grid"] = DS1;
            SearchGridView.DataBind();

        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();

        }
    }


    protected void GradeGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            SearchGridView.UseAccessibleHeader = false;
            SearchGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    //protected void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    for (int i = 0; i < e.Row.Cells.Count; i++)
    //    {
    //        if (e.Row.Cells[i].Text == "&nbsp;")
    //        {
    //            e.Row.Cells[i].Text = "";
    //        }
    //    }
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
    //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.SearchGridView, "Select$" + e.Row.RowIndex);
    //    }
    //}
    protected void btn_add_Click(object sender, EventArgs e)
    {
        int res;

        res = d.operation("insert into pay_complaint_category(category)Values('" + txt_category.Text + "')");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Added Successfully... !!');", true);
            load_sub_grid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Added Faill.... !!');", true);
        }
        txt_category.Text = "";
    }
    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["Travelling_grid"] != null)
        {
            DataTable dt = (DataTable)ViewState["Travelling_grid"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    d.operation("Delete  from pay_complaint_category where  Id = '" + dt.Rows[rowID][0] + "'");
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["Travelling_grid"] = dt;
            SearchGridView.DataSource = dt;
            SearchGridView.DataBind();

            SearchGridView.DataSource = dt;
            //Grid_gst_rate.DataTextField = dt.Columns[0].ToString();
            //Grid_gst_rate.DataValueField = dt.Columns[0].ToString();
            SearchGridView.DataBind();


        }
    }
}
