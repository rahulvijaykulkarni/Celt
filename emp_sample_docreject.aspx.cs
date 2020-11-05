using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emp_sample_docreject : System.Web.UI.Page
{
    DAL d = new DAL();
    double a = 0, c = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        if (!IsPostBack)
        {
            if (!Session["Ticket_ID"].ToString().Equals(""))
            {
                load_sub_grid(Session["Ticket_ID"].ToString());
            }
            else
            {
                SearchGridView.DataSource = null;
                SearchGridView.DataBind();
            }
        }
    }
 
    private void load_sub_grid(string request_Id_edit)
    {
        try
        {
            d.con.Open();
            DataSet ds = new DataSet();
            // MySqlDataAdapter dr = new MySqlDataAdapter("select Id,services,pay_service_master.location,priority,pay_service_master.status,additional_comment,EMP_NAME as forword_to,documents from  pay_service_master inner join pay_employee_master on pay_service_master.comp_code=pay_employee_master.comp_code and  pay_service_master.forword_to=pay_employee_master.emp_code   where pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' order by Id  ", d.con);
            MySqlDataAdapter dr = new MySqlDataAdapter("SELECT id,comp_code,client_code,unit_code,month,year,(SELECT emp_name FROM pay_employee_master WHERE uploaded_by = pay_employee_master.emp_code) AS uploaded_by, uploaded_date,description,status FROM pay_files_timesheet where id= '" + Session["Ticket_ID"].ToString() + "'", d.con);
            dr.Fill(ds);
            SearchGridView.DataSource = ds.Tables[0];
            SearchGridView.DataBind();
            d.con.Close();
            SearchGridView.Visible = true;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();

        }
    }


    protected void btn_reject_Click(object sender, EventArgs e)
    {
        commenrid.Visible = true;
        txt_comments.Visible = true;

        try
        {
            d.operation("Update pay_files_timesheet set status = 'Reject - " + txt_comments.Text + "' where id = '" + Session["Ticket_ID"].ToString() + "'");
       
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            load_sub_grid(Session["Ticket_ID"].ToString());
            btn_reject.Visible = false;
            txt_comments.Text = "";
        }

    }
  
    protected void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        //e.Row.Cells[1].Visible = false;
        //e.Row.Cells[2].Visible = false;
    }
}