using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class shift : System.Web.UI.Page
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
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = true;
            //btn_new.Visible = false;

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master",Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            //btn_new.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;

        }

        btn_delete.Visible = false;
        btn_edit.Visible = false;
        if (!IsPostBack)
        {

            btn_add.Visible = true;
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            loadshift();
        }
    }

  
    protected void btn_add_Click(object sender, EventArgs e)
    {
        GradeBAL gbl2 = new GradeBAL();
        int result = 0;
        try
        {
            result = gbl2.shiftInsert(Session["comp_code"].ToString(), Session["UNIT_CODE"].ToString(), txt_shift_name.Text, ddl_shift_from.Text, ddl_shift_to.Text, Session["LOGIN_ID"].ToString());
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);
                d.reset(this);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            loadshift();
        }
    }

    protected void GradeGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Label lbl_shift_name1 = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_shift_name");
        string lbl_shift_name11 = lbl_shift_name1.Text;

        d.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT shift_name,DATE_FORMAT(shift_from,'%h:%i %p')As shift_from,DATE_FORMAT(shift_to,'%h:%i %p') As shift_to FROM pay_shift_master WHERE  shift_name='" + lbl_shift_name11 + "' and  comp_code='"+Session["comp_code"].ToString()+"' ", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_shift_name.Text = dr.GetValue(0).ToString();
                ddl_shift_from.Text = dr.GetValue(1).ToString();
                ddl_shift_to.Text = dr.GetValue(2).ToString();
            }
            dr.Close();
            cmd.Dispose();

            btn_add.Visible = false;
            btn_edit.Visible = true;
            btn_delete.Visible = true;
            txt_shift_name.ReadOnly = false;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

    }


    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "Select$" + e.Row.RowIndex);
        }

    }

    private void loadshift()
    {
        DataSet dt_gv = new DataSet();
        d.con.Open();
        try
        {
            MySqlCommand cmd_gv = new MySqlCommand("SELECT shift_name,DATE_FORMAT(shift_from,'%h:%i %p')As shift_from,DATE_FORMAT(shift_to,'%h:%i %p') As shift_to FROM pay_shift_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"].ToString() + "'", d.con);
            MySqlDataAdapter dr_gv = new MySqlDataAdapter(cmd_gv);
            dr_gv.Fill(dt_gv);
            GradeGridView.DataSource = dt_gv;
            GradeGridView.DataBind();
            txt_shift_name.Text = "";
            ddl_shift_from.Text = "";
            ddl_shift_to.Text = "";
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
           
        }
        try
        {

               d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select send_email from pay_shift_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                if (dr1[0].ToString() == "1")
                {
                    chk_send_email.Checked = true;
                }
                else { chk_send_email.Checked = false; }

            }
            else
            {
                chk_send_email.Checked = false;
            }            
            dr1.Close();
            d.con.Close();
            cmd.Dispose();
        }
        catch
        {
            throw;
        }
        finally
        {
            d.con.Close();
            //cmd.Dispose();
        }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        int result = 0;

        System.Web.UI.WebControls.Label lbl_shift_name = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_shift_name");
        string lbl_shift_name1 = lbl_shift_name.Text;
        try
        {
            string query = "UPDATE pay_shift_master SET  shift_name='" + txt_shift_name.Text + "', shift_from =STR_TO_DATE('" + ddl_shift_from.Text.ToString() + "','%h:%i %p'),shift_to = STR_TO_DATE('" + ddl_shift_to.Text.ToString() + "','%h:%i %p') WHERE shift_name='" + lbl_shift_name1 + "' AND comp_code='" + Session["comp_code"].ToString() + "' ";

            result = d.operation("UPDATE pay_shift_master SET  shift_name='" + txt_shift_name.Text + "', shift_from =STR_TO_DATE('" + ddl_shift_from.Text.ToString() + "','%h:%i %p'),shift_to =STR_TO_DATE('" + ddl_shift_to.Text.ToString() + "','%h:%i %p') WHERE shift_name='" + lbl_shift_name1 + "' AND comp_code='" + Session["comp_code"].ToString() + "' ");
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
            loadshift();
        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        int result = 0;

        System.Web.UI.WebControls.Label lbl_shift_name = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_shift_name");
        string lbl_shift_name1 = lbl_shift_name.Text;

        try
        {

            MySqlCommand cmd_1 = new MySqlCommand("Select shift_name from pay_shift_master where shift_name='" + lbl_shift_name1 + "' and comp_code='" + Session["comp_code"] + "' ", d.con1);

            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();

            if (dr_1.Read())
            {
                result = d.operation("DELETE FROM pay_shift_master WHERE comp_code='" + Session["comp_code"].ToString() + "' AND shift_name='" + lbl_shift_name1 + "'");

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed!!');", true);

                }
            }
        }
        catch (Exception ee)
        {
            throw ee;

        }
        finally
        {
            txt_shift_name.Text = "";
            d.con1.Close();
            loadshift();

        }
    }

    protected void btn_new_Click1(object sender, EventArgs e)
    {
        btn_add.Visible = true;
        btn_edit.Visible = false;
        btn_delete.Visible = false;
    }

    protected void btn_close(object sender, EventArgs e) {

        Response.Redirect("Home.aspx");
    
    }


    protected void chk_send_email_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_send_email.Checked)
        {
            d.operation("update pay_shift_master set send_email ='1' where comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"].ToString() + "'");
        }
        else { d.operation("update pay_shift_master set send_email ='0' where comp_code='" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"].ToString() + "'"); }

    }
    protected void GradeGridView_PreRender(object sender, EventArgs e)
    {
             try
        {
            GradeGridView.UseAccessibleHeader = false;
            GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}

