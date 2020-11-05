using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class p_add_gst_rate : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected int result = 0;//vikas
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            display_table();
           
        }

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            result = d.operation("INSERT INTO pay_gst_rate(gst_rate) VALUES('" + txt_gst_rate.Text + "');");

            if (result > 0)
            {
                 Response.Write("<script type='text/javascript'>");
                 Response.Write("alert('add record succsefully');");
                 Response.Write("document.location.href='p_add_gst_rate.aspx';");
                         Response.Write("</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('add record succsefully  !!!');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' record  NOT succsefully !!!');", true);
            }

            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
           
            d.con.Close();
        }

    }

    public void display_table()
    {
        DataSet ds_vend_gv = new DataSet();
        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT `Id`, gst_rate FROM pay_gst_rate  ORDER BY id", d1.con1);
        d1.con1.Open();
        MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
        adp_vend_gv.Fill(ds_vend_gv);
        Grid_gst_rate.DataSource = ds_vend_gv;
        Grid_gst_rate.DataBind();
        d1.con1.Close();
    }
    protected void Grid_gst_rate_PreRender(object sender, EventArgs e)
    {
        try
        {
            Grid_gst_rate.UseAccessibleHeader = false;
            Grid_gst_rate.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}