using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class size_details : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
        if (!IsPostBack)
        {
           
        }
        load_ddl();
    }

    protected void load_ddl() 
    {
        Session["EXPENSE_ID"].ToString();
        string query1 = "SELECT pay_document_details.emp_code, pay_document_details.No_of_set,pay_document_details.size,pay_dispatch_billing.dispatch_date FROM pay_document_details INNER JOIN pay_dispatch_billing ON pay_document_details.emp_code = pay_dispatch_billing.emp_code WHERE  pay_dispatch_billing.dispatch_date IS NOT NULL and size=  '"+Session["EXPENSE_ID"].ToString()+"' ";
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter(query1, d.con);
        d.con.Open();
        adp.Fill(ds);
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        //Panel9.Visible = true;
        d.con.Close();
    
    }
}