using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GradeDetails : System.Web.UI.Page
{
    DAL d = new DAL();
    GradeBAL gbl3 = new GradeBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_delete.Visible = false;
        btn_edit.Visible = false;

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
            btn_add.Visible = false;
            //btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            // btnexporttoexcelgrade.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            // btnexporttoexcelgrade.Visible = false;
        }

        if (!IsPostBack)
        {
            btn_add.Visible = false;
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            main_body.Visible = false;
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        GradeBAL gbl2 = new GradeBAL();
        int result = 0;
        string newdate = Convert.ToString(System.DateTime.Now);
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            result = gbl2.GradeInsert(Session["comp_code"].ToString(), txt_grade.Text, txtgradedesc.Text, ddl_reportingto.SelectedValue, Session["USERNAME"].ToString(), newdate, ddl_hours.SelectedValue, ddl_employee_type.SelectedValue);
            //result1 = gbl2.GradeInsertlog(cmpcode, txt_grade.Text, txtgradedesc.Text, ddl_reportingto.SelectedValue, emp_name, newdate);
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation added successfully!!');", true);
                clear_text();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation adding failed...');", true);
                clear_text();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ddl_hours.SelectedValue = "0";
            clear_text();
            ddl_employee_type.SelectedValue = "Select";
            //MySqlCommand cmd_item1 = new MySqlCommand("SELECT Count(GRADE_CODE) FROM pay_grade_master ", d.con);
            //d.con.Open();

            //MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            //while (dr_item1.Read())
            //{
            //    int a = Convert.ToInt32(dr_item1.GetValue(0));
            //    if (a == 1)
            //    {
            //        Response.Redirect("Manage_Role.aspx");
            //    }

            //}
            //dr_item1.Close();
            //d.con.Close();
        }
    }
    protected void GradeGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Label lbl_GRADE_CODE = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_GRADE_CODE");
        string l_GRADE_CODE = lbl_GRADE_CODE.Text;

        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd = new MySqlCommand("SELECT GRADE_CODE,GRADE_DESC,Reporting_to,Working_Hours FROM pay_grade_master WHERE GRADE_CODE='" + l_GRADE_CODE + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_grade.Text = dr.GetValue(0).ToString();
                txtgradedesc.Text = dr.GetValue(1).ToString();
                ddl_reportingto.SelectedValue = dr.GetValue(2).ToString();
                ddl_hours.SelectedValue = dr.GetValue(3).ToString();
            }
            dr.Close();
            cmd.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

        btn_edit.Visible = true;
        btn_delete.Visible = true;
        txt_grade.ReadOnly = false;
        txtgradedesc.ReadOnly = false;
        btn_add.Visible = false;
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        int result = 0;
        string emp_name = Session["USERNAME"].ToString();
        string newdate = Convert.ToString(System.DateTime.Now);
        string l_GRADE_CODE = ((System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_GRADE_CODE")).Text;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            result = d.operation("UPDATE pay_grade_master SET comp_code='" + Session["comp_code"] + "',  GRADE_CODE='" + txt_grade.Text + "', GRADE_DESC='" + txtgradedesc.Text + "', REPORTING_TO='" + ddl_reportingto.SelectedValue.ToString() + "' ,Login_Person='" + emp_name + "', LastModifyDate = '" + newdate + "',Working_Hours='" + ddl_hours.SelectedValue + "',employee_type='" + ddl_employee_type.SelectedValue + "' WHERE GRADE_CODE='" + l_GRADE_CODE + "' and  comp_code='" + Session["comp_code"].ToString() + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'");
            // result1 = d.operation("UPDATE pay_grade_master_log SET Login_Person='" + emp_name + "',LastModifyDate='" + newdate + "'  WHERE GRADE_CODE='" + l_GRADE_CODE + "' ");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation updated successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation updation failed!!');", true);
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ddl_hours.SelectedValue = "0";
            clear_text();
        }
    }
    private void clear_text()
    {
        btn_add.Visible = true;
        btn_edit.Visible = false;
        btn_delete.Visible = false;

        txt_grade.ReadOnly = false;
        txtgradedesc.ReadOnly = false;

        txt_grade.Text = "";
        txtgradedesc.Text = "";
        DataSet ds = new DataSet();
        ds = d.getData("SELECT GRADE_CODE,GRADE_DESC,Reporting_to,Working_Hours FROM pay_grade_master where comp_code = '" + Session["comp_code"].ToString() + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'");
        GradeGridView.DataSource = ds.Tables[0];
        GradeGridView.DataBind();
        populate_ddl_reportingto();
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        clear_text();
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        int result = 0;

        System.Web.UI.WebControls.Label lbl_GRADE_CODE = (System.Web.UI.WebControls.Label)GradeGridView.SelectedRow.FindControl("lbl_GRADE_CODE");
        string l_GRADE_CODE = lbl_GRADE_CODE.Text;
        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            MySqlCommand cmd_1 = new MySqlCommand("Select GRADE_CODE from pay_employee_master where GRADE_CODE='" + l_GRADE_CODE + "' and comp_code='" + Session["comp_code"] + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'", d.con1);
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation against employee exist can not delete this Designation');", true);
            }
            else
            {
                result = d.operation("DELETE FROM pay_grade_master WHERE comp_code='" + Session["comp_code"].ToString() + "' AND GRADE_CODE='" + l_GRADE_CODE + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'");
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation deleted successfully!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Designation deletion failed!!');", true);
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            ddl_hours.SelectedValue = "0";
            
            d.con1.Close();
            clear_text();
        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
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

    protected void txt_grade_TextChanged(object sender, EventArgs e)
    {
        MySqlCommand cmd_1 = new MySqlCommand("Select GRADE_CODE from pay_grade_master where GRADE_CODE='" + txt_grade.Text + "' and comp_code='" + Session["comp_code"].ToString() + "' and employee_type = '" + ddl_employee_type.SelectedValue + "'", d.con1);
        d.con1.Open();
        try
        {
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This grade code already exist, try another.');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    private void populate_ddl_reportingto()
    {
        d.con1.Open();
        try
        {
            MySqlCommand com = new MySqlCommand("Select GRADE_CODE from pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' and employee_type = '" + ddl_employee_type.SelectedValue + "' order by Grade_code", d.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddl_reportingto.DataTextField = ds.Tables[0].Columns["GRADE_CODE"].ToString();
            ddl_reportingto.DataValueField = ds.Tables[0].Columns["GRADE_CODE"].ToString();
            ddl_reportingto.DataSource = null;
            ddl_reportingto.DataBind();
            ddl_reportingto.DataSource = ds.Tables[0];
            ddl_reportingto.DataBind();
            ddl_reportingto.Items.Insert(0, new ListItem("Select Reporting", ""));
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void GradeGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            //GradeGridView.UseAccessibleHeader = false;
            GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void ddl_employee_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        if (ddl_employee_type.SelectedValue != "")
        {
            main_body.Visible = true;
            clear_text();
        }
        else
        {
            main_body.Visible = false;
        }
    }
}
