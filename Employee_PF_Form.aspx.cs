using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System.Data;

public partial class Employee_PF_Form : System.Web.UI.Page
{
    DAL d = new DAL();
    ReportDocument crystalReport = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            panem_exp_letter.Visible = false;
            panel_Experiance_Letter.Visible = false;
        }

    }

    protected void btn_PRINT_Click(object sender, EventArgs e)
    {
        panel_Experiance_Letter.Visible = true;
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //d.con1.Open();
        //try
        //{
        //    string downloadname = "EMPLOYEE_PF_FORM";
        //        string query = null;
        //    crystalReport.Load(Server.MapPath("~/rpt_emp_fund.rpt"));

        //       query = " SELECT comp_code,EMP_NAME,JOINING_DATE,ADDRESS,GRADE_CODE,SALARY_PER_MONTH FROM offer_letter WHERE EMP_CODE='" + tds_Id + "' AND comp_code='" + Session["comp_code"].ToString() + "'";

        //   //   crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
        //        ReportLoad2(query,downloadname);

        //        //Response.End();


        //        //Response.End();
        //        //text_Clear();
        //        //Auto_Increment();
        // }
        //catch (Exception ee)
        //{

        //}
        //finally
        //{

           
        //}
    
    }
    private void ReportLoad2(string query, string downloadfilename2)
    {
        d.con.Close();
       // d1.con.Close();
        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename2;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            MySqlDataReader sda = null;
            cmd.Connection = d.con;
            d.con.Open();
            sda = cmd.ExecuteReader();
            dt.Load(sda);
            d.con.Close();

            //MySqlCommand cmd_item1 = new MySqlCommand("SELECT COMP_LOGO from pay_company_master where comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
            //d.con.Open();
            //MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            //if (dr_item1.Read())
            //{
            //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\" + dr_item1.GetValue(0).ToString());
            //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
            //    PictureObject TAddress1 = (PictureObject)crystalReport.ReportDefinition.Sections[0].ReportObjects["Picture1"];
            //    TAddress1.Width = 850;
            //    TAddress1.Height = 400;
            //    TAddress1.Left = 4000;
            //}
            //else
            //{
            //    string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\logo.png");
            //    crystalReport.DataDefinition.FormulaFields["image_path"].Text = "'" + path + "'";
            //}
            //dr_item1.Close();
            //d.con.Close();

            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            //  crystalReport.SetDatabaseLogon(@"Tanvi-Tej\Tanvi","");
            //Response.End();
           // updateAmount();
           // text_Clear();
            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadname);
            // updateAmount();

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            //var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
            //var script = string.Format("alert({0});", message);
            //ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "", script, true);
        }
        finally
        {

        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btn_Experiancesearch_Letter_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        panel_Experiance_Letter.Visible = true;

        d.con1.Open();

        // MySqlCommand cmd = new MySqlCommand("SELECT  pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.PAN_NUMBER,pay_employee_master.P_TAX_NUMBER,pay_employee_master.EMP_MOBILE_NO FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT comp_code,EMP_CODE,EMP_NAME,date_format(JOINING_DATE,'%d/%m/%Y') as JOINING_DATE,GRADE_CODE,LOCATION,date_format(BIRTH_DATE,'%d/%m/%Y') as BIRTH_DATE ,EARN_TOTAL  FROM pay_employee_master  WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + appoi_emp_id.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + appoi_emp_id.Text + "%'))  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ", d.con1);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found for this company!!');", true);
        }
        else
        {
            // panelapposearch.Visible = true;
            //AppointmentSearch.Visible = true;
            panel_Experiance_Letter.Visible = true;
            panel_Experiance_Letter.Visible = true;


        }
        panem_exp_letter.Visible = true;

        gv_experianceletter.DataSource = ds.Tables[0];
        gv_experianceletter.DataBind();
        d.con1.Close();
    }
   
    protected void expering_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_experianceletter, "Select$" + e.Row.RowIndex);


        }
    }

    protected void gv_Experiance_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();
        System.Web.UI.WebControls.Label lbl_EMP_code = (System.Web.UI.WebControls.Label)gv_experianceletter.SelectedRow.FindControl("lbl_realiving_id");


        string l_EMP_code1 = lbl_EMP_code.Text;
        d.con1.Close();
        string downloadname = "Experiance_letter";
        string query = null;



        crystalReport.Load(Server.MapPath("~/rpt_emp_fund.rpt"));
        query = "SELECT pay_employee_master.comp_code,pay_employee_master.EMP_NAME as EMP_NAME,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') AS BIRTH_DATE , pay_employee_master.EMP_FATHER_NAME AS EMP_FATHER_NAME, case when pay_employee_master.GENDER = 'm' then 'Male' else 'Female' END as 'GENDER',pay_employee_master. EMP_MOBILE_NO AS EMP_MOBILE_NO, pay_employee_master. EMP_EMAIL_ID as EMP_EMAIL_ID FROM pay_employee_master   WHERE pay_employee_master.EMP_CODE ='" + l_EMP_code1 + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ";
       
        //crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Offerletter");
        ReportLoad2(query, downloadname);
        Response.End();
    }

    protected void btn_form_2_Click(object sender, EventArgs e)
    {
        panel_Experiance_Letter.Visible = false;
        panem_exp_letter.Visible = false;
        string downloadname = "FORM_NO_2";
        string query = null;



        crystalReport.Load(Server.MapPath("~/rpt_nomination_form.rpt"));
       // query = "SELECT pay_employee_master.comp_code,pay_employee_master.EMP_NAME as EMP_NAME,pay_employee_master.BIRTH_DATE AS BIRTH_DATE , pay_employee_master.EMP_FATHER_NAME AS EMP_FATHER_NAME,pay_employee_master. GENDER AS GENDER,pay_employee_master. EMP_MOBILE_NO AS EMP_MOBILE_NO, pay_employee_master. EMP_EMAIL_ID as EMP_EMAIL_ID FROM pay_employee_master   WHERE pay_employee_master.EMP_CODE ='" + l_EMP_code1 + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' ";

        crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "FORM_NO_2");
        ReportLoad1( downloadname);
        Response.End();
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    
    }

    private void ReportLoad1( string downloadfilename2)
    {
        d.con.Close();
        // d1.con.Close();
        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename2;
            System.Data.DataTable dt = new System.Data.DataTable();
           

            crystalReport.SetDataSource(dt);
            crystalReport.Refresh();
            
            crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, downloadname);
            // updateAmount();

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            
        }
        finally
        {

        }
    }
}

