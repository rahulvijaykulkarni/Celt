using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class service_category : System.Web.UI.Page
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
            if (!Session["SERVICE"].ToString().Equals(""))
            {
                load_sub_grid();
            }
            else
            {
                SearchGridView.DataSource = null;
                SearchGridView.DataBind();
            }
        }
    }
 
   


    public void update_grid()
    {

        //System.Data.DataTable dt_gv = new System.Data.DataTable();
        //d.con.Open();
        ////MySqlCommand cmd_gv = new MySqlCommand("SELECT EP_NO,expense_title,case When expense_status = 1 then 'Pending' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = submitter_code AND COMP_CODE ='"+Session["COMP_CODE"].ToString()+"') AS submitter,DATE_FORMAT(submitted_on,'%d/%m/%Y') As submitted_on,expense_amount FROM expense_approval WHERE (COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND approver = '"+Session["LOGIN_ID"].ToString()+"') ORDER BY EP_NO desc", d.con);
        ////MySqlCommand cmd_gv = new MySqlCommand(" Select Id, expenses_id ,exception_case,travel_mode ,from_designation,to_designation,DATE_FORMAT(from_date,'%d/%m/%Y') As from_date,DATE_FORMAT(to_date,'%d/%m/%Y') As to_date ,modified_by,Comments,adv_amount,curreny_id, case expense_status When 0 then 'In Progress' When 1 then 'Submitted' When 2 then 'Approved' When 3 then 'Rejected' When 4 then 'Reimbursement' END As expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = apply_travel_plan.Emp_code AND COMP_CODE ='C01' ) AS submitter  from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"] + "' group BY expenses_id Order by expenses_id desc", d.con);
        //MySqlCommand cmd_gv = new MySqlCommand("Select Id, expenses_id ,exception_case,travel_mode ,from_designation,to_designation,DATE_FORMAT(from_date,'%d/%m/%Y') As from_date,DATE_FORMAT(to_date,'%d/%m/%Y') As to_date ,modified_by,Comments,adv_amount,curreny_id, expense_status,(Select EMP_NAME from pay_employee_master Where EMP_CODE = apply_travel_plan.Emp_code AND COMP_CODE ='C01' ) AS submitter  from apply_travel_plan where expenses_id='" + Session["EXPENSE_ID"] + "' ", d.con);
        //MySqlDataAdapter dr_gv = new MySqlDataAdapter(cmd_gv);
        //dr_gv.Fill(dt_gv);

        //gv_paymentdetails.DataSource = dt_gv;
        //gv_paymentdetails.DataBind();
        //d.con.Close();
    }



    private void load_sub_grid()
    {
        try
        {
            d.con.Open();
            DataSet ds = new DataSet();
            // MySqlDataAdapter dr = new MySqlDataAdapter("select Id,services,pay_service_master.location,priority,pay_service_master.status,additional_comment,EMP_NAME as forword_to,documents from  pay_service_master inner join pay_employee_master on pay_service_master.comp_code=pay_employee_master.comp_code and  pay_service_master.forword_to=pay_employee_master.emp_code   where pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' order by Id  ", d.con);
            MySqlDataAdapter dr = new MySqlDataAdapter("SELECT id,service,category from pay_service_category", d.con);
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
    protected void btn_add_Click(object sender, EventArgs e)
    {
        d.operation("Insert Into pay_service_category (service,category) values('"+ Session["SERVICE"].ToString() + "','"+ txt_category.Text +"')");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Category Added Successfully !!!');", true);
        txt_category.Text = "";
    }
}