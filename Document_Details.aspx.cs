using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Document_Details : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    public MySqlDataReader drmax = null;

    
    protected void Page_Load(object sender, EventArgs e)
    {
		
		if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }



        if (d1.getaccess(Session["ROLE"].ToString(), "TDS Calculation", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "TDS Calculation", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_Delete.Visible = false;
            btn_Update.Visible = false;

            btn_Save.Visible = false;
           // btnhelp.Visible = false;
        
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "TDS Calculation", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_Delete.Visible = false;
          
        
            
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "TDS Calculation", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_Delete.Visible = false;
        
        }
        



        if (!IsPostBack)
        {

            string com_code = Session["COMP_CODE"].ToString();
            d.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT Id,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  ", d.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            gv_Tds.DataSource = ds1.Tables[0];
            gv_Tds.DataBind();
            d.con1.Close();
            btn_Save.Visible = false;
            btn_Update.Visible = false;
            btn_Delete.Visible = false;
            gv_Tds.Visible = true;
            btn_new_Click();
            employee_ddl();
        }
    }

    protected void btn_new_Click()
    {
        btn_Save.Visible = true;
        btn_Update.Visible = false;
        btn_Delete.Visible = false;

       
       
      
       
        //-----------------------------------------------
        d.con1.Open();
        MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(Id, 2, 4) AS UNSIGNED))+1 FROM  pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con1);
        drmax = cmdmax.ExecuteReader();
        if (!drmax.HasRows)
        {
        }
        else if (drmax.Read())
        {
            string str = drmax.GetValue(0).ToString();
            if (str == "")
            {
                txt_Sr_No.Text = "I0001";
            }
            else
            {
                int max_srno = int.Parse(drmax.GetValue(0).ToString());
                if (max_srno < 10)
                {
                    txt_Sr_No.Text = "I000" + max_srno;
                }
                else if (max_srno > 9 && max_srno < 100)
                {
                    txt_Sr_No.Text = "I00" + max_srno;
                }
                else if (max_srno > 99 && max_srno < 1000)
                {
                    txt_Sr_No.Text = "I0" + max_srno;
                }

                else
                {
                }
            }
        }
        drmax.Close();
        d.con1.Close();
    }


    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");

    }
 

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string com_code = Session["COMP_CODE"].ToString();
        d.con1.Open();
        try
        {
            int res = 0;
            res = d.operation("INSERT INTO pay_document_details(COMP_CODE,Id,emp_code,reporting_to,document_type,start_date,end_date ) VALUES( '" + Session["COMP_CODE"].ToString() + "','" + txt_Sr_No.Text + "','" + ddl_employeename.SelectedItem.Text + "','" + txt_reporting_to.Text + "','" + ddl_document_list.SelectedItem.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'))");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);

                Clear_Fill();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed!!');", true);

            }
        }
        catch (Exception ee)
        {

        }
        finally
        {



            MySqlCommand cmd1 = new MySqlCommand("SELECT Id,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'   ", d.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            gv_Tds.DataSource = ds1.Tables[0];
            gv_Tds.DataBind();
             d.con1.Close();
             btn_new_Click();
             employee_ddl();
        }
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {

        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_Tds.SelectedRow.FindControl("lbl_srnumber");

        string tds_Id = lblno.Text;
       
        string com_code = Session["COMP_CODE"].ToString();
        d.con1.Open();

        try
        {


            d.operation("UPDATE pay_tds_calculation SET emp_code='"+ddl_employeename.SelectedValue+"',reporting_to='"+txt_reporting_to.Text+"',document_type='"+ddl_document_list.SelectedValue+"',start_date='"+txt_start_date.Text+"',end_date='"+txt_end_date.Text+"' WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Id='" + tds_Id + "' ");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);
            Clear_Fill();
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);
        }
        finally
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT Id,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  ", d.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            gv_Tds.DataSource = ds1.Tables[0];
            gv_Tds.DataBind();

           
            d.reset(this);
            btn_new_Click();
            employee_ddl();
        }
      
        d.con1.Close();

    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {

        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_Tds.SelectedRow.FindControl("lbl_srnumber");

        string tds_Id = lblno.Text;

        string com_code = Session["COMP_CODE"].ToString();
        d.con1.Open();

      
        try
        {
            int res = 0;
            res = d.operation("DELETE FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND Id='" + tds_Id + "' ");
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully !!');", true);
                Clear_Fill();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed !!');", true);

            }
        }
        catch (Exception ee)
        {

        }
        finally
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT Id,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            gv_Tds.DataSource = ds1.Tables[0];
            gv_Tds.DataBind();
            d.reset(this);
            btn_new_Click();
            employee_ddl();
        }

        d.con1.Close();
    }

 
    
    


    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Clear_Fill();
        d.reset(this);
        btn_new_Click();
        employee_ddl();
    }
  

    protected void gv_Tds_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        d.con1.Open();



        System.Web.UI.WebControls.Label lblno = (System.Web.UI.WebControls.Label)gv_Tds.SelectedRow.FindControl("lbl_srnumber");

        string tds_Id = lblno.Text;
                                        
        MySqlCommand cmd2 = new MySqlCommand("SELECT Id,emp_code,reporting_to,document_type,start_date,end_date FROM pay_document_details WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Id='" + tds_Id + "'  ", d.con);
        d.conopen();
        MySqlDataReader dr = cmd2.ExecuteReader();
        if (dr.Read())
        {
          
            txt_Sr_No.Text = dr.GetValue(0).ToString();
           ddl_employeename.SelectedValue=dr.GetValue(1).ToString();
           txt_reporting_to.Text = dr.GetValue(2).ToString();
           ddl_document_list.SelectedValue = dr.GetValue(3).ToString();
           txt_start_date.Text = dr.GetValue(4).ToString();
           txt_end_date.Text = dr.GetValue(5).ToString();
        }
        dr.Close();
        d.conclose();
        btn_Save.Visible = false;
        btn_Update.Visible = true;

        btn_Delete.Visible = true;


    }

    protected void gv_Tds_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_Tds, "Select$" + e.Row.RowIndex);

        }
    }

    protected void Clear_Fill()
    {
        
        ddl_employeename.SelectedValue = "0";
        txt_reporting_to.Text = "";
        ddl_document_list.SelectedValue = "0";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
    }

    protected void employee_ddl()
    {
        d.con.Open();
        MySqlDataAdapter adp = new MySqlDataAdapter("select emp_name,emp_code from pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "'  ", d.con);

        System.Data.DataTable dt = new System.Data.DataTable();
        adp.Fill(dt);

        if (dt.Columns.Count > 0)
        {
            ddl_employeename.DataSource = dt;
            ddl_employeename.DataTextField = dt.Columns[0].ToString();
            ddl_employeename.DataValueField = dt.Columns[1].ToString();
            ddl_employeename.DataBind();
            dt.Dispose();
            d.con.Close();
         
        }
     }

    protected void reporting_to_drtails(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select REPORTING_TO from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and EMP_CODE='"+ddl_employeename.SelectedValue+"'",d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txt_reporting_to.Text = dr[0].ToString();
        
        }

        dr.Close();
        d.con.Close();
    
    }
     

    
}