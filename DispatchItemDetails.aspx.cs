using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class DispatchItemDetails : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    double a = 0, b = 0, c = 0;
    static int serialno;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            //fill_gridview();
            client_list();
        }

    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void fill_gridview()
    {
        d.con.Open();
        MySqlDataAdapter acmd = new MySqlDataAdapter("SELECT  pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.EMP_CODE,EMP_NAME AS 'EMP_NAME', document_type AS 'DOCUMENT_TYPE',  No_of_set AS 'NO_OF_SET', size AS 'SIZE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code  INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE  AND pay_employee_master.comp_code = pay_client_master.comp_code 	INNER JOIN pay_document_details ON pay_employee_master.emp_code = pay_document_details.emp_code  AND pay_employee_master.comp_code = pay_document_details.comp_code where pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and dispatch_flag='0'", d.con);
        DataSet ds = new DataSet();
        acmd.Fill(ds);
        gv_itemdetails.DataSource = ds.Tables[0];
        gv_itemdetails.DataBind();
        d.con.Close();
    }
  
   
    protected void chk_bal_redeposite_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    
    protected void gv_itemdetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemdetails.UseAccessibleHeader = false;
            gv_itemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void ddlunitclient1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_itemdetails.Visible = true;


        try
        {
            DataSet ds = new DataSet();
            //vikas 08-01-19
           // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode,(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO ,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE', DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and `pay_employee_master`.`unit_code` IN (SELECT DISTINCT (`pay_client_state_role_grade`.`unit_code`) FROM `pay_client_state_role_grade` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`comp_code` = `pay_client_state_role_grade`.`comp_code` AND `pay_employee_master`.`emp_code` = `pay_client_state_role_grade`.`emp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`client_code`='" + ddlunitclient1.SelectedValue + "' AND (`REPORTING_TO` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR `pay_client_state_role_grade`.`emp_code` = '" + Session["LOGIN_ID"].ToString() + "')) and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_unit_master.branch_status = 0 ", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT  pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.EMP_CODE,EMP_NAME AS 'EMP_NAME', document_type AS 'DOCUMENT_TYPE',  No_of_set AS 'NO_OF_SET', size AS 'SIZE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code  INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE  AND pay_employee_master.comp_code = pay_client_master.comp_code 	INNER JOIN pay_document_details ON pay_employee_master.emp_code = pay_document_details.emp_code  AND pay_employee_master.comp_code = pay_document_details.comp_code where pay_document_details.`client_code`='" + ddlunitclient1.SelectedValue + "' and pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and dispatch_flag='0'", d.con);
            adp.Fill(ds);

            gv_itemdetails.DataSource = ds.Tables[0];
            gv_itemdetails.DataBind();

            gv_itemdetails.Visible = true;
            Panel9.Visible = true;
            // text_Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_statewise.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "'  AND LOCATION in(SELECT DISTINCT  (pay_client_state_role_grade.state_name)  FROM  pay_client_state_role_grade  INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code  WHERE  pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code='" + ddlunitclient1.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) ORDER BY EMP_CURRENT_STATE", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_gv_statewise.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        ddl_gv_statewise.Items.Insert(0, new ListItem("Select"));

        gv_itemdetails.Visible = true;

    }

    public void client_list()
    {
        d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ")", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {



                ddlunitclient1.DataSource = dt_item;
                ddlunitclient1.DataTextField = dt_item.Columns[1].ToString();
                ddlunitclient1.DataValueField = dt_item.Columns[0].ToString();
                ddlunitclient1.DataBind();
            }
            dt_item.Dispose();
            d.con1.Close();

            ddlunitclient1.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }

    protected void ddlsatewises_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_itemdetails.Visible = true;
      

        try
        {
            DataSet ds = new DataSet();
           
           // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode,(SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE', DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code 		INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_employee_master.LOCATION='" + ddl_gv_statewise.SelectedValue + "' AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_employee_master.unit_code in (SELECT DISTINCT (`pay_client_state_role_grade`.`unit_code`) FROM `pay_client_state_role_grade` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`comp_code` = `pay_client_state_role_grade`.`comp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`client_code` = '" + ddlunitclient1.SelectedValue + "' AND `pay_client_state_role_grade`.`state_name`='" + ddl_gv_statewise.SelectedValue + "' AND (pay_client_state_role_grade.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR `pay_client_state_role_grade`.`emp_code` = '" + Session["LOGIN_ID"].ToString() + "')) and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_unit_master.branch_status = 0 ", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT  pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.EMP_CODE,EMP_NAME AS 'EMP_NAME', document_type AS 'DOCUMENT_TYPE',  No_of_set AS 'NO_OF_SET', size AS 'SIZE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code  INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE  AND pay_employee_master.comp_code = pay_client_master.comp_code 	INNER JOIN pay_document_details ON pay_employee_master.emp_code = pay_document_details.emp_code  AND pay_employee_master.comp_code = pay_document_details.comp_code where pay_document_details.`client_code`='" + ddlunitclient1.SelectedValue + "' AND pay_employee_master.LOCATION='" + ddl_gv_statewise.SelectedValue + "'and  pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and dispatch_flag='0'", d.con);
            adp.Fill(ds);
          
            gv_itemdetails.DataSource = ds.Tables[0];
            gv_itemdetails.DataBind();
            // text_Clear();
          
            gv_itemdetails.Visible = true;
            Panel9.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_branchwise.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddlunitclient1.SelectedValue + "' and state_name = '" + ddl_gv_statewise.SelectedValue + "' AND  UNIT_CODE in(SELECT DISTINCT (`pay_client_state_role_grade`.`unit_code`) FROM `pay_client_state_role_grade` INNER JOIN `pay_employee_master` ON `pay_employee_master`.`comp_code` = `pay_client_state_role_grade`.`comp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`client_code` = '" + ddlunitclient1.SelectedValue + "' AND `pay_client_state_role_grade`.`state_name` = '" + ddl_gv_statewise.SelectedValue + "' AND (`pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR `pay_client_state_role_grade`.`emp_code` = '" + Session["LOGIN_ID"].ToString() + "')) and branch_status = 0  ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_gv_branchwise.DataSource = dt_item;
                ddl_gv_branchwise.DataTextField = dt_item.Columns[0].ToString();
                ddl_gv_branchwise.DataValueField = dt_item.Columns[1].ToString();
                ddl_gv_branchwise.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_gv_branchwise.Items.Insert(0, new ListItem("Select"));
        }

        
    }

    protected void ddlbeabchwise1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_itemdetails.Visible = true;
      

        try
        {
            DataSet ds = new DataSet();
           
           // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode, (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE' , DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_employee_master.UNIT_CODE='" + ddl_gv_branchwise.SelectedValue + "' AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  and LOCATION='" + ddl_gv_statewise.SelectedValue + "' AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'  and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) ", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT  pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.EMP_CODE,EMP_NAME AS 'EMP_NAME', document_type AS 'DOCUMENT_TYPE',  No_of_set AS 'NO_OF_SET', size AS 'SIZE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code  INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE  AND pay_employee_master.comp_code = pay_client_master.comp_code 	INNER JOIN pay_document_details ON pay_employee_master.emp_code = pay_document_details.emp_code  AND pay_employee_master.comp_code = pay_document_details.comp_code where pay_document_details.`client_code`='" + ddlunitclient1.SelectedValue + "' and pay_document_details.unit_code='" + ddl_gv_branchwise.SelectedValue + "' AND pay_employee_master.LOCATION='" + ddl_gv_statewise.SelectedValue + "'and  pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and dispatch_flag='0'", d.con);
            adp.Fill(ds);
           
            gv_itemdetails.DataSource = ds.Tables[0];
            gv_itemdetails.DataBind();
           
           
            gv_itemdetails.Visible = true;
            Panel9.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void ddl_product_type_SelectedIndexedChanged(object sender, EventArgs e)
    {
        gv_itemdetails.Visible = true;


        try
        {
            DataSet ds = new DataSet();

            // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_employee_master.EMP_CODE,pay_employee_master.ihmscode, (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) AS 'EMP_NAME',pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.employee_type,pay_employee_master.EMP_MOBILE_NO,date_format(pay_employee_master.BIRTH_DATE,'%d/%m/%Y') as 'BIRTH_DATE' , DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_employee_master.comp_code = pay_client_master.comp_code WHERE  ((pay_employee_master.EMP_CODE LIKE '%" + txtsearchempid.Text + "%') OR (pay_employee_master.EMP_NAME LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.PAN_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.P_TAX_NUMBER LIKE '%" + txtsearchempid.Text + "%')OR (pay_employee_master.EMP_MOBILE_NO LIKE '%" + txtsearchempid.Text + "%')) AND pay_employee_master.UNIT_CODE='" + ddl_gv_branchwise.SelectedValue + "' AND pay_employee_master.client_code='" + ddlunitclient1.SelectedValue.ToString() + "'  and LOCATION='" + ddl_gv_statewise.SelectedValue + "' AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'  and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) ", d.con1);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT  pay_employee_master.EMP_NAME,pay_client_master.CLIENT_NAME, pay_unit_master.UNIT_NAME,pay_employee_master.EMP_CODE,EMP_NAME AS 'EMP_NAME', document_type AS 'DOCUMENT_TYPE',  No_of_set AS 'NO_OF_SET', size AS 'SIZE' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_employee_master.comp_code = pay_unit_master.comp_code  INNER JOIN pay_client_master ON pay_employee_master.CLIENT_CODE = pay_client_master.CLIENT_CODE  AND pay_employee_master.comp_code = pay_client_master.comp_code 	INNER JOIN pay_document_details ON pay_employee_master.emp_code = pay_document_details.emp_code  AND pay_employee_master.comp_code = pay_document_details.comp_code where pay_document_details.`client_code`='" + ddlunitclient1.SelectedValue + "' and `document_type`='" + ddl_product_type.SelectedValue + "' and pay_document_details.unit_code='" + ddl_gv_branchwise.SelectedValue + "' AND pay_employee_master.LOCATION='" + ddl_gv_statewise.SelectedValue + "'and  pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' and dispatch_flag='0'", d.con);
            adp.Fill(ds);

            gv_itemdetails.DataSource = ds.Tables[0];
            gv_itemdetails.DataBind();


            gv_itemdetails.Visible = true;
            Panel9.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    
    }
}