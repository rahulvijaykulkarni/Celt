using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class Emp_doc_details : System.Web.UI.Page
{
    DAL d = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            client_list();
        }
    }

    public void client_list()
    {
        d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (pay_client_state_role_grade.`client_code`),client_name FROM  `pay_client_state_role_grade`inner join  `pay_employee_master` on pay_employee_master.comp_code = pay_client_state_role_grade.comp_code    and pay_employee_master.emp_code = pay_client_state_role_grade.emp_code  INNER JOIN   pay_client_master ON  pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_state_role_grade.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "'  and ( REPORTING_TO in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code ='" + Session["LOGIN_ID"].ToString() + "')", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unit_client.DataSource = dt_item;
                ddl_unit_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_unit_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_unit_client.DataBind();


               
            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_unit_client.Items.Insert(0, "Select");
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }

    protected void get_city_list1(object sender, EventArgs e)
    {

        if (ddl_unit_client.SelectedValue != "Select")
        {
            //State
            ddl_clientwisestate.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" +ddl_unit_client.SelectedValue+ "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);

            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_clientwisestate.DataSource = dt_item;
                    ddl_clientwisestate.DataTextField = dt_item.Columns[0].ToString();
                    ddl_clientwisestate.DataValueField = dt_item.Columns[0].ToString();
                    ddl_clientwisestate.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_clientwisestate.Items.Insert(0, "Select");
            }
        }
        else
        {
            ddl_unitcode.Items.Clear();
        }
       
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_clientwisestate.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_unit_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" +ddl_unit_client.SelectedValue + "')  ORDER BY STATE_NAME", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unitcode.DataSource = dt_item;
                ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                ddl_unitcode.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
    }

    protected void btn_close_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_rem_documents_Click(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            string where = "";
            if (ddl_unit_client.SelectedValue != "Select")
            {
                where = " and pay_document_details.client_code = '" + ddl_unit_client.SelectedValue + "'";
                // where = where + " and pay_document_details.state_name = '" + ddl_clientwisestate.SelectedValue + "'";
                    if (ddl_unitcode.SelectedValue != "Select")
                    {
                        where = where + " and pay_document_details.unit_code = '" + ddl_unitcode.SelectedValue + "'";
                    }
                //}
            }


          //  string sql = "SELECT pay_client_master.client_name, pay_unit_master.state_name, pay_unit_master.unit_name, pay_employee_master.EMP_NAME, CASE WHEN EMP_ADHAR_PAN IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_ADHAR_PAN) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_ADHAR_PAN', CASE WHEN EMP_BIODATA IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_BIODATA) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_BIODATA', CASE WHEN EMP_PASSPORT IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_PASSPORT) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_PASSPORT', CASE WHEN EMP_DRIVING_LISCENCE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DRIVING_LISCENCE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DRIVING_LISCENCE', CASE WHEN EMP_10TH_MARKSHEET IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_10TH_MARKSHEET) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_10TH_MARKSHEET', CASE WHEN EMP_12TH_MARKSHEET IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_12TH_MARKSHEET) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_12TH_MARKSHEET', CASE WHEN EMP_DIPLOMA_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DIPLOMA_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DIPLOMA_CERTIFICATE', CASE WHEN EMP_DEGREE_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_DEGREE_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_DEGREE_CERTIFICATE', CASE WHEN EMP_POST_GRADUATION_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_POST_GRADUATION_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_POST_GRADUATION_CERTIFICATE', CASE WHEN EMP_EDUCATION_CERTIFICATE IS NULL THEN 'No' ELSE CASE WHEN TRIM(EMP_EDUCATION_CERTIFICATE) = '' THEN 'No' ELSE 'Yes' END END AS 'EMP_EDUCATION_CERTIFICATE', CASE WHEN FORMNO_2 IS NULL THEN 'No' ELSE CASE WHEN TRIM(FORMNO_2) = '' THEN 'No' ELSE 'Yes' END END AS 'FORMNO_2', CASE WHEN FORMNO_11 IS NULL THEN 'No' ELSE CASE WHEN TRIM(FORMNO_11) = '' THEN 'No' ELSE 'Yes' END END AS 'FORMNO_11', CASE WHEN noc_form IS NULL THEN 'No' ELSE CASE WHEN TRIM(noc_form) = '' THEN 'No' ELSE 'Yes' END END AS 'noc_form', CASE WHEN medical_form IS NULL THEN 'No' ELSE CASE WHEN TRIM(medical_form) = '' THEN 'No' ELSE 'Yes' END END AS 'medical_form', CASE WHEN emp_signature IS NULL THEN 'No' ELSE CASE WHEN TRIM(emp_signature) = '' THEN 'No' ELSE 'Yes' END END AS 'emp_signature', CASE WHEN original_photo IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_photo) = '' THEN 'No' ELSE 'Yes' END END AS 'original_photo', CASE WHEN original_adhar_card IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_adhar_card) = '' THEN 'No' ELSE 'Yes' END END AS 'original_adhar_card', CASE WHEN original_policy_document IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_policy_document) = '' THEN 'No' ELSE 'Yes' END END AS 'original_policy_document', CASE WHEN original_address_proof IS NULL THEN 'No' ELSE CASE WHEN TRIM(original_address_proof) = '' THEN 'No' ELSE 'Yes' END END AS 'original_address_proof', CASE WHEN bank_passbook IS NULL THEN 'No' ELSE CASE WHEN TRIM(bank_passbook) = '' THEN 'No' ELSE 'Yes' END END AS 'bank_passbook' FROM pay_images_master inner join pay_employee_master on pay_employee_master.emp_code = pay_images_master.emp_code and pay_employee_master.comp_code = pay_images_master.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and pay_employee_master.comp_code = pay_client_master.comp_code where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'" + where;
           //string sql = "SELECT CLIENT_NAME AS 'client_code'  ,CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master  WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS unit_code,  (select EMP_NAME from pay_employee_master where emp_code=`pay_document_details`.`emp_code` )AS EMP_CODE , (select `STATE_NAME` from pay_unit_master where unit_code=pay_document_details.unit_code and comp_code=pay_document_details.comp_code)as `STATE_NAME`  FROM `pay_document_details`  	INNER JOIN `pay_client_master` ON `pay_document_details`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_document_details`.`client_code` = `pay_client_master`.`client_code`  	INNER JOIN `pay_unit_master` ON `pay_document_details`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_document_details`.`unit_code` = `pay_unit_master`.`unit_code` where document_type='" + ddl_type.SelectedValue + "' and pay_document_details.comp_code = '" + Session["COMP_CODE"].ToString() + "'" + where;

            string sql = "	SELECT client_name AS `client_code`,STATE_NAME, CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS unit_code, `pay_employee_master`.`EMP_NAME` as 'emp_code'  FROM    `pay_employee_master`      left outer JOIN `pay_document_details`  ON   `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code`   AND `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code`    AND `pay_employee_master`.`unit_code` = `pay_document_details`.`unit_code`  AND `document_type` = '"+ddl_type.SelectedValue+"'  inner join pay_client_master on pay_employee_master.client_code=pay_client_master.client_code and pay_employee_master.comp_code=pay_client_master.comp_code  inner join pay_unit_master on pay_employee_master.unit_code=pay_unit_master.unit_code and pay_employee_master.comp_code=pay_unit_master.comp_code  WHERE    `pay_employee_master`.`left_date` IS NULL AND `pay_employee_master`.`comp_code` = '"+Session["comp_code"].ToString()+"' AND `pay_employee_master`.`unit_code` = '"+ddl_unitcode.SelectedValue+"'  	and pay_document_details.emp_code is null";

            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Remaining_Documents.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(stringWrite.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate(ListItemType type, DataSet ds)
        {
            this.type = type;
            this.ds = ds;
            ctr = 0;
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:
                    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th colspan=3 font-size=8>CLIENT NAME</th><th colspan=3 font-size=8>BRANCH STATE</th><th colspan=3 font-size=8>BRANCH</th><th colspan=3 font-size=8>EMPLOYEE NAME</th></tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td colspan=3 font-size=8>" + ds.Tables[0].Rows[ctr]["client_code"].ToString().ToUpper() + "</td><td colspan=3 font-size=8>" + ds.Tables[0].Rows[ctr]["STATE_NAME"].ToString().ToUpper() + "</td><td colspan=3 font-size=8>" + ds.Tables[0].Rows[ctr]["unit_code"].ToString().ToUpper() + "</td><td colspan=3 font-size=8>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + "</td></tr>");
                    ctr++;
                    break;
                case ListItemType.Footer:
                    lc = new LiteralControl("</table>");
                    ctr = 0;
                    break;
            }
            container.Controls.Add(lc);
        }
    }
}