using MySql.Data.MySqlClient;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class hsn_master : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            gst_rate();
            fill_gridview();
            btn_update.Visible = false;
            btn_delete.Visible = false;
        }

    }
    protected void gst_rate()
    {
         DataTable dt_item = new DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select gst_rate from pay_gst_rate ORDER BY id", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_gst_rate.DataSource = dt_item;
                ddl_gst_rate.DataTextField = dt_item.Columns[0].ToString();

                ddl_gst_rate.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_gst_rate.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        //MySqlCommand cmd = new MySqlCommand("select hsn_code from pay_hsn_master where hsn_code='"+txt_hsn_code.Text+"' ", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This HSn Code All Ready Present... !!!');", true);
        //}
        //else
        //{
            int result = 0;
            result = d.operation("insert into pay_hsn_master(hsn_code,`gst_rate`,`hsn_category`)values('" + txt_hsn_code.Text + "','" + ddl_gst_rate.SelectedValue + "','" + txt_hsn_category.Text + "')");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Add succsefully... !!!');", true);
                fill_gridview();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Add faill.... !!!');", true);
            }
            fill_gridview();
            txt_clear();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //}
        //dr.Close();
        //d.con.Close();
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
    }

    protected void fill_gridview()
    {
        d.con.Open();
        MySqlDataAdapter adp = new MySqlDataAdapter("select Id,hsn_code,gst_rate,hsn_category from pay_hsn_master ", d.con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        Grid_hsn_rate.DataSource = dt;
        Grid_hsn_rate.DataBind();
        adp.Dispose();
        d.con.Close();

    }

    protected void btn_close_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void fill_gridview_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string id = Grid_hsn_rate.SelectedRow.Cells[0].Text;
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select hsn_code,gst_rate,hsn_category,Id from pay_hsn_master where Id='" + id + "'",d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_hsn_code.Text = dr.GetValue(0).ToString();
            ddl_gst_rate.SelectedValue = dr.GetValue(1).ToString();
            txt_hsn_category.Text = dr.GetValue(2).ToString();
            txt_id.Text = dr.GetValue(3).ToString();
        }
        dr.Close();
        d.con.Close();
        btn_delete.Visible = true;
        btn_update.Visible = true;
        btn_save.Visible = false;
       // txt_clear();
    }
   
    protected void btn_update_click(object sender, EventArgs e)
    {
        // MySqlCommand cmd = new MySqlCommand("select hsn_code from pay_hsn_master where hsn_code='"+txt_hsn_code.Text+"' ", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This HSn Code All Ready Present... !!!');", true);

        //}
        //else
        //{
            int result = 0;
            result = d.operation("update pay_hsn_master set hsn_code='" + txt_hsn_code.Text + "',gst_rate='" + ddl_gst_rate.SelectedValue + "', hsn_category='" + txt_hsn_category.Text + "' where Id='" + txt_id.Text + "' ");

            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Update succsefully... !!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Update faill.... !!!');", true);

           // }
        }
       // dr.Read();
        d.con.Close();

        fill_gridview();
        btn_delete.Visible = false;
        btn_update.Visible = false;
        btn_save.Visible = true;
        txt_clear();
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }

    protected void Grid_hsn_rate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // e.Row.Cells[3].Text = "Landmark";
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
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(Grid_hsn_rate, "Select$" + e.Row.RowIndex);
        }
    }

    protected void btn_delete_click(object sender, EventArgs e)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT hsn_number FROM pay_item_master WHERE hsn_number='" + txt_hsn_code .Text+ "'", d.con);
            d.con.Open( );
            MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            dr.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' first delete hsn code from item master...');", true);
            }
            else
            {
        int result = 0;
      result=  d.operation("delete from pay_hsn_master where Id='" + txt_id.Text + "' ");
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted succsefully... !!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted faill.... !!!');", true);
        }
        fill_gridview();
            }
           

        }
        catch (Exception ex)
        {
        }
        finally
        {
            d.con.Close();
        }

        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }
    protected void txt_clear()
    {
        txt_hsn_code.Text = "";
        txt_hsn_category.Text = "";
        ddl_gst_rate.SelectedValue = "Select";
    
    }

    protected void Grid_hsn_rate_PreRender(object sender, EventArgs e)
    {
        try
        {
            Grid_hsn_rate.UseAccessibleHeader = false;
            Grid_hsn_rate.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
   
}