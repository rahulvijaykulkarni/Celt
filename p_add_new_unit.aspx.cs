using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class p_add_new_unit : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
   // DepartmentBAL dptbl3 = new DepartmentBAL();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_delete.Visible = false;
        txt_update.Visible = false;
        
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        } 
        if(!IsPostBack)
        {
            master_table();
        }
    }

    public MySqlDataReader drdept = null;
    MySqlDataReader drmax = null;
    protected void btncancel_Click(object sender, EventArgs e)
    {
        txt_item_name.Text = "";
        txt_piece_per.Text = "";
        btn_newn.Visible = true;
    }

    protected void DesignationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.DepartmentGridView, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void DepartmentGridView_SelectedIndexChanged(Object Sender, EventArgs e)
    {
        Label lbl_doccode = (Label)DepartmentGridView.SelectedRow.FindControl("lbl_item_name");
        string item_name = lbl_doccode.Text;
        MySqlCommand cmd2 = new MySqlCommand("SELECT item_unit_name, item_pieces FROM item_unit_master  WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and item_unit_name='" + item_name + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                txt_item_name.Text = dr2[0].ToString();
                txt_piece_per.Text = dr2[1].ToString();
            }
            dr2.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        txt_update.Visible = true;
        txt_delete.Visible = true;
        btn_newn.Visible = false;
    }
    protected void btnclose_Click(object sender, EventArgs e) 
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnnew_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result = 0;
            result = d.operation("INSERT INTO item_unit_master(item_unit_name,item_pieces,comp_code) VALUES('" + txt_item_name.Text + "','" + txt_piece_per.Text + "','" + Session["COMP_CODE"].ToString() + "')");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);
                text_clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);
                text_clear();
                master_table();
            }
        }
        catch (Exception ee)
        {
            lblerrmsg.ForeColor = System.Drawing.Color.Red;
            lblerrmsg.Text = ee.Message;
        }
        finally
        {
            btn_newn.Visible = true;

            master_table();
        }
    }

    protected void btn_update_click(object sender, EventArgs e)
    {
        string cmpcode = Session["COMP_CODE"].ToString();
        int result = 0;
        System.Web.UI.WebControls.Label lbl_doccode = (System.Web.UI.WebControls.Label)DepartmentGridView.SelectedRow.FindControl("lbl_item_name");
        string txt_item_namenew = lbl_doccode.Text;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            result = d.operation("Update item_unit_master set item_unit_name='" + txt_item_name.Text + "', item_pieces='" + txt_piece_per.Text + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and item_unit_name='" + txt_item_namenew + "'");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updated successfully!!');", true);
                text_clear();
                d.reset(this);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record updation failed...');", true);
                text_clear();
                master_table();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            master_table();
            btn_newn.Visible = true;
        }
    }

    protected void btn_delete_click(object sender, EventArgs e) {

        string cmpcode = Session["COMP_CODE"].ToString();
        int result = 0;
        try
        {
            result = d.operation("delete from item_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and item_unit_name='" + txt_item_name.Text + "'");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Delete successfully!!');", true);
                text_clear();
                d.reset(this);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Delete failed...');", true);
                text_clear();
                master_table();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
           // master_table();
            btn_newn.Visible = true;
            master_table();
        }
    
    }
   public void text_clear()
    {
        txt_item_name.Text = "";
        txt_piece_per.Text = "";
    }

    public void master_table()
    {
        DataSet ds_vend_gv = new DataSet();
        MySqlCommand cmd_vendor_gv = new MySqlCommand("SELECT item_unit_name, item_pieces FROM item_unit_master  WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY id", d1.con1);
        d1.con1.Open();
        MySqlDataAdapter adp_vend_gv = new MySqlDataAdapter(cmd_vendor_gv);
        adp_vend_gv.Fill(ds_vend_gv);
        DepartmentGridView.DataSource = ds_vend_gv;
        DepartmentGridView.DataBind();
        d1.con1.Close();
    }
    protected void DepartmentGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            DepartmentGridView.UseAccessibleHeader = false;
            DepartmentGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}