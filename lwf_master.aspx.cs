using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mlwf_master : System.Web.UI.Page
{
    DAL d = new DAL();
    public MySqlDataReader drmax = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

             d.con.Open();
             MySqlCommand cmd = new MySqlCommand("select id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution from pay_master_lwf", d.con);
            DataSet ds=new DataSet();
            MySqlDataAdapter ada=new MySqlDataAdapter(cmd);
            ada.Fill(ds);
            gv_MLWF.DataSource=ds.Tables[0];
            gv_MLWF.DataBind();
            d.con.Close();

            btn_new_Click(null, null);
            btn_update.Visible = false;
            btn_delete.Visible = false;
        
        }

    }

    protected void text_clear()
    {
        txt_city.Text = "";
        txt_comp_contribution.Text = "0";
        txt_econtribution.Text = "0";
        txt_empcategory.Text = "";
        txt_lastday.Text = "";
        txt_lwfact.Text = "";
        //txt_total.Text = "";
        ddl_contractlaobou.SelectedValue = "0";
        ddl_state.SelectedValue = "0";
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string comp_code = Session["COMP_CODE"].ToString();
        int res = 0;
        try
        {
            //res = d.operation("insert into pay_master_lwf(COMP_CODE,id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution)value('" + Session["COMP_CODE"].ToString() + "','" + txt_id.Text + "','" + ddl_state.SelectedValue + "','" + txt_lwfact.Text + "','" + txt_empcategory.Text + "','" + ddl_contractlaobou.SelectedValue + "','" + txt_city.Text + "','" + txt_lastday.Text + "','" + txt_econtribution.Text + "','" + txt_comp_contribution.Text + "','" + txt_total.Text + "')");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added failed !!');", true);

            }

        }
        catch { }
        finally 
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution from pay_master_lwf where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
            DataSet ds = new DataSet();
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(ds);
            gv_MLWF.DataSource = ds.Tables[0];
            gv_MLWF.DataBind();
            d.con.Close();

           
            text_clear();
            btn_new_Click(null, null);
        }

    
    
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        int res = 0;
        try
        {
            //res = d.operation("update pay_master_lwf set state_name='" + ddl_state.SelectedValue + "',app_LWF_act='" + txt_lwfact.Text + "',category_employees='" + txt_empcategory.Text + "',contract_labours='" + ddl_contractlaobou.SelectedValue + "',period='" + txt_city.Text + "',last_day_submission='" + txt_lastday.Text + "',employee_contribution='" + txt_econtribution.Text + "',employer_contribution='" + txt_comp_contribution.Text + "',total_contribution='" + txt_total.Text + "'");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Update successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Update failed !!');", true);

            }
        }
        catch
        {
        }
        finally 
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution from pay_master_lwf where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
            DataSet ds = new DataSet();
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            ada.Fill(ds);
            gv_MLWF.DataSource = ds.Tables[0];
            gv_MLWF.DataBind();
            d.con.Close();

            btn_save.Visible = true;
            text_clear();
            btn_new_Click(null, null);

        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_delete_Click(object sender,EventArgs e)
    {
        System.Web.UI.WebControls.Label lblid = (System.Web.UI.WebControls.Label)gv_MLWF.SelectedRow.FindControl("lbl_srnumber");
        string mlwfid = lblid.Text;

        int res = 0;
        res = d.operation("delete from pay_master_lwf where id='" + mlwfid + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alter", "alter('Record Deleted Successfully !!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alter", "alter('Record Deleted failed !!!');", true);
        }
        btn_save.Visible = true;
        btn_update.Visible = false;
        btn_delete.Visible = false;
        text_clear();
        btn_new_Click(null, null);
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution from pay_master_lwf where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        DataSet ds = new DataSet();
        MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
        ada.Fill(ds);
        gv_MLWF.DataSource = ds.Tables[0];
        gv_MLWF.DataBind();
        d.con.Close();

    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
     
        //-----------------------------------------------
        d.con1.Open();
        MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(id AS UNSIGNED))+1 FROM  pay_master_lwf", d.con1);
        drmax = cmdmax.ExecuteReader();
        if (!drmax.HasRows)
        {
        }
        else if (drmax.Read())
        {
            string str = drmax.GetValue(0).ToString();
            if (str == "")
            {
                txt_id.Text = "M0001";
            }
            else
            {
                int max_srno = int.Parse(drmax.GetValue(0).ToString());
                if (max_srno < 10)
                {
                    txt_id.Text = "M000" + max_srno;
                }
                else if (max_srno > 9 && max_srno < 100)
                {
                    txt_id.Text = "M00" + max_srno;
                }
                else if (max_srno > 99 && max_srno < 1000)
                {
                    txt_id.Text = "M0" + max_srno;
                }

                else
                {
                }
            }
        }
        drmax.Close();
        d.con1.Close();
    }

    protected void gv_MLWF_SelectedIndexChanged(object sender,EventArgs e)
    {
      d.con.Open();
        System.Web.UI.WebControls.Label lblid=(System.Web.UI.WebControls.Label)gv_MLWF.SelectedRow.FindControl("lbl_srnumber");
        string mlwfid=lblid.Text;

        MySqlCommand cmd = new MySqlCommand("select id,state_name,app_LWF_act,category_employees,contract_labours,period,last_day_submission,employee_contribution,employer_contribution,total_contribution from pay_master_lwf where id='" + mlwfid + "' ", d.con);
        MySqlDataReader dr=cmd.ExecuteReader();
        if(dr.Read())
        {
            txt_id.Text=dr.GetValue(0).ToString();
            ddl_state.SelectedValue=dr.GetValue(1).ToString();
            txt_lwfact.Text=dr.GetValue(2).ToString();
            txt_empcategory.Text=dr.GetValue(3).ToString();
            ddl_contractlaobou.SelectedValue=dr.GetValue(4).ToString();
            txt_city.Text=dr.GetValue(5).ToString();
            txt_lastday.Text=dr.GetValue(6).ToString();
            txt_econtribution.Text=dr.GetValue(7).ToString();
            txt_comp_contribution.Text=dr.GetValue(8).ToString();
            //txt_total.Text=dr.GetValue(9).ToString();
         }

        dr.Close();
        d.con.Close();
        btn_save.Visible=false;
        btn_update.Visible=true;
        btn_delete.Visible=true;

    }
    protected void gv_MLWF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_MLWF, "Select$" + e.Row.RowIndex);

        }
    }

    protected void gv_MLWF_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_MLWF.UseAccessibleHeader = false;
            gv_MLWF.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}