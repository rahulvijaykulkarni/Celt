
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReports : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_gstr1b2bcsvfile(object sender, EventArgs e) {

        generate_slab_report(1, "", "");
    }

    protected void btn_gstr1hsncodecsvfile(object sender, EventArgs e)
    {

        generate_slab_report(2, "", "");
    }

    protected void btn_gstb2bcsvfile(object sender ,EventArgs  e) {


        // 27AAECI3733R1Z1
        //excel genter

       // MySqlDataAdapter dr = new MySqlDataAdapter("select  unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number',date_format(`billing_date`,'%d-%b-%Y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value',concat(SUBSTRING(unit_gst_no,1,2),'-',`state_name`) as 'Place Of Supply','N' as 'Reverse Charge','Regular' as 'Invoice Type','' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  where  comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and unit_gst_no != '' and auto_invoice_no !='' group by auto_invoice_no order by billing_date,auto_invoice_no", d.con);

        // start
       // MySqlDataAdapter dr = new MySqlDataAdapter("select  pay_billing_unit_rate_history.unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number', date_format(`billing_date`,'%d-%b-%y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value', concat(SUBSTRING(pay_billing_unit_rate_history.unit_gst_no,1,2),'-',pay_billing_unit_rate_history.`state_name`) as 'Place Of Supply', 'N' as 'Reverse Charge',case GST_to_be when 'R' then 'Regular' when 'SEWP' then 'SEZ supplies with payment' when 'SEWOP' then 'SEZ supplies without payment' when 'DE' then 'Deemed Exp' when 'SCU' then 'Supplies covered under section 7 of IGST Act' else '' end as 'Invoice Type', '' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  inner join pay_unit_master on pay_billing_unit_rate_history.comp_code=pay_unit_master.comp_code and pay_billing_unit_rate_history.client_code=pay_unit_master.client_code and pay_billing_unit_rate_history.unit_code=pay_unit_master.unit_code  where  pay_billing_unit_rate_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and pay_billing_unit_rate_history.unit_gst_no != ''  and auto_invoice_no !='' and GST_to_be in ('R','SEWOP')  group by auto_invoice_no order by billing_date,auto_invoice_no  ", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter("SELECT pay_report_gst.gst_no AS 'GSTIN/UIN of Recipient', client_name AS 'Receiver Name', invoice_no AS 'Invoice Number', DATE_FORMAT(invoice_date, '%d-%b-%y') AS 'Invoice date', (ROUND((amount + CGST + SGST + IGST), 2)) AS 'Invoice Value', CONCAT(SUBSTRING(pay_report_gst.gst_no,1, 2), '-', pay_report_gst.state_name) AS 'Place Of Supply', 'N' AS 'Reverse Charge', CASE pay_report_gst.GST_to_be WHEN 'R' THEN 'Regular' WHEN 'SEWP' THEN 'SEZ supplies with payment' WHEN 'SEWOP' THEN 'SEZ supplies without payment' WHEN 'DE' THEN 'Deemed Exp' WHEN 'SCU' THEN 'Supplies covered under section 7 of IGST Act' ELSE '' END AS 'Invoice Type', '' AS 'E-Commerce GSTIN', '18' AS 'Rate', '' AS 'Applicable % of Tax Rate', (ROUND(amount, 2)) AS 'Taxable Value', '' AS 'Cess Amount' FROM pay_report_gst INNER JOIN pay_unit_master ON pay_report_gst.comp_code = pay_unit_master.comp_code AND pay_report_gst.client_code = pay_unit_master.client_code WHERE pay_report_gst.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND invoice_date BETWEEN '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' AND pay_report_gst.gst_no != '' AND invoice_no != '' AND pay_report_gst.GST_to_be IN ('R', 'SEWOP') and (amount is not null ||amount != 0) GROUP BY invoice_no ORDER BY invoice_date, invoice_no ", d.con);
        d.con.Open();
        System.Data.DataTable dt = new System.Data.DataTable();
        DataSet DS1 = new DataSet();
        dr.SelectCommand.CommandTimeout = 200;
        dr.Fill(dt);

        gv_gstr1_b2b_csv.DataSource = dt;
        gv_gstr1_b2b_csv.DataBind();
        d.con.Close();
        panel_hsncode.Visible = false;
        // end


        // statr excel data table

        //MySqlCommand cmd = new MySqlCommand("select  pay_billing_unit_rate_history.unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number', date_format(`billing_date`,'%d-%b-%y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value', concat(SUBSTRING(pay_billing_unit_rate_history.unit_gst_no,1,2),'-',pay_billing_unit_rate_history.`state_name`) as 'Place Of Supply', 'N' as 'Reverse Charge',case GST_to_be when 'R' then 'Regular' when 'SEWP' then 'SEZ supplies with payment' when 'SEWOP' then 'SEZ supplies without payment' when 'DE' then 'Deemed Exp' when 'SCU' then 'Supplies covered under section 7 of IGST Act' else '' end as 'Invoice Type', '' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  inner join pay_unit_master on pay_billing_unit_rate_history.comp_code=pay_unit_master.comp_code and pay_billing_unit_rate_history.client_code=pay_unit_master.client_code and pay_billing_unit_rate_history.unit_code=pay_unit_master.unit_code  where  pay_billing_unit_rate_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and pay_billing_unit_rate_history.unit_gst_no != ''  and auto_invoice_no !=''  group by auto_invoice_no order by billing_date,auto_invoice_no  ", d.con);
       
        //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //d.con.Open();
        //DataTable dt = new DataTable();
        //da.SelectCommand.CommandTimeout = 200;
        //da.Fill(dt);


        ////Populating a DataTable from database.
        //// DataTable dt = this.GetData();

        ////Building an HTML string.
        //StringBuilder html = new StringBuilder();

        ////Table start.
        //// html.Append("<table style=\"width: 100%;\" aria-describedby=\"example_info\" role=\"grid\" id=\"example\" class=\"table table-striped table-bordered dataTable no-footer\" width=\"100%\" cellspacing=\"0\">");

        //html.Append("<thead>");
        ////Building the Header row.
        //html.Append("<tr>");
        //foreach (DataColumn column in dt.Columns)
        //{
        //    html.Append("<th>");
        //    html.Append(column.ColumnName);
        //    html.Append("</th>");
        //}
        //html.Append("</tr>");
        //html.Append("</thead>");
        //html.Append("<tbody>");
        ////Building the Data rows.
        //foreach (DataRow row in dt.Rows)
        //{
        //    html.Append("<tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        html.Append("<td>");
        //        html.Append(row[column.ColumnName]);
        //        html.Append("</td>");
        //    }
        //    html.Append("</tr>");
        //}
        //html.Append("</tbody>");

        //BodyContent1.Controls.Add(new Literal { Text = html.ToString() });
        //d.con.Close();
        // end excel data table


        //d.con1.Open();
        //String UnitList = "";

        ////MySqlCommand cmd = new MySqlCommand();
        //System.Data.DataTable dt = new System.Data.DataTable();
        //MySqlDataAdapter adp;
        //string query = "";
        ////if (ddl_uan_state_name.SelectedValue.ToString() == "ALL" && ddl_uan_unit_name.SelectedValue.ToString() == "ALL")
        ////{
        ////    adp = new MySqlDataAdapter("SELECT concat('#~#',   '#~#',  '' ,'#~#',  ifnull(emp_name,''),'#~#', date_format(birth_date,'%d/%m/%Y') ,'#~#',  date_format(joining_date,'%d/%m/%Y'),'#~#',  ifnull(Gender,''),'#~#',  ifnull(emp_father_name,'') ,'#~#',  (CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END) ,'#~#',  ifnull(emp_mobile_no,'') ,'#~#',  ifnull(emp_email_id,'') ,'#~#',  ifnull(emp_nationality,'') ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(emp_marrital_status,'U') ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#','' ,'#~#',  '' ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(original_bank_account_no,'') ,'#~#',  ifnull(pf_ifsc_code,'') ,'#~#',  ifnull(bank_holder_name,'') ,'#~#',  '#~#',  '' ,'#~#',  ifnull(p_tax_number,'') ,'#~#',  ifnull(emp_name,'')) as 'rr'FROM  pay_employee_master WHERE  comp_code = '" + Session["COMP_CODE"].ToString() + "' and employee_type='Permanent' and (pan_number ='' or pan_number is null) and original_bank_account_no != ''  and (pay_employee_master.LEFT_REASON is null or pay_employee_master.LEFT_REASON = '') and client_code='" + ddl_uan_client_list.SelectedValue + "' ORDER BY  emp_code", d.con1);
        ////    // query = "select  isnull (PAY_EMPLOYEE_MASTER.PAN_NUMBER,'') + '#~#' + '#~#'+isnull(EMP_NAME,'')+'#~#' + isnull(DATE_FORMAT(BIRTH_DATE,'%d/%m/%Y'),'')+ '#~#' + isnull(DATE_FORMAT(JOINING_DATE,'%d/%m/%Y'),'')+ '#~#' + isnull(PAY_EMPLOYEE_MASTER.GENDER,'')+ '#~#' +  isnull(EMP_FATHER_NAME,'')+ '#~#' + CASE WHEN FATHER_RELATION='Father' then 'F' else 'H' END + '#~#' + isnull(EMP_MOBILE_NO,'')+ '#~#' +  ''+ '#~#' + 'INDIAN'+ '#~#' +  ''+ '#~#' +  ''+'#~#'+Case When EMP_MARRITAL_STATUS ='Married' then 'M' When EMP_MARRITAL_STATUS = 'Single' then 'U' When EMP_MARRITAL_STATUS='Divorced' then 'D' When EMP_MARRITAL_STATUS='Widowed' then 'W' Else '' END+'#~#'+'N'+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+'N'+'#~#'+''+'#~#'+''+'#~#'+''+'#~#'+isnull(BANK_EMP_AC_CODE,'')+'#~#'+isnull(PF_IFSC_CODE,'')+'#~#'+ Case When BANK_EMP_AC_CODE != NULL then EMP_NAME When BANK_EMP_AC_CODE != '' then EMP_NAME else '' END+'#~#'+isnull(EMP_NEW_PAN_NO,'')+'#~#'+CASE When EMP_NEW_PAN_NO != NULL then EMP_NAME When EMP_NEW_PAN_NO != '' then EMP_NAME ELSE '' END+'#~#'+isnull(ADHARNO,'')+'#~#'+Case When ADHARNO != NULL then EMP_NAME When ADHARNO != '' then EMP_NAME Else '' END FROM PAY_EMPLOYEE_MASTER WHERE (PAY_EMPLOYEE_MASTER.LEFT_REASON is null or PAY_EMPLOYEE_MASTER.LEFT_REASON = '') and PAY_EMPLOYEE_MASTER.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' order by PAY_EMPLOYEE_MASTER.PAN_NUMBER";
        ////}
        ////else
        ////{
            

        ////    adp = new MySqlDataAdapter("SELECT concat('#~#',   '#~#',  '' ,'#~#',  ifnull(emp_name,''),'#~#', date_format(birth_date,'%d/%m/%Y') ,'#~#',  date_format(joining_date,'%d/%m/%Y'),'#~#',  ifnull(Gender,''),'#~#',  ifnull(emp_father_name,'') ,'#~#',  (CASE father_relation WHEN 'Father' THEN 'F' WHEN 'Husband' THEN 'H' ELSE '' END) ,'#~#',  ifnull(emp_mobile_no,'') ,'#~#',  ifnull(emp_email_id,'') ,'#~#',  ifnull(emp_nationality,'') ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(emp_marrital_status,'U') ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#','' ,'#~#',  '' ,'#~#',  'N' ,'#~#',  '' ,'#~#',  '' ,'#~#',  '' ,'#~#',  ifnull(original_bank_account_no,'') ,'#~#',  ifnull(pf_ifsc_code,'') ,'#~#',  ifnull(bank_holder_name,'') ,'#~#',  '#~#',  '' ,'#~#',  ifnull(p_tax_number,'') ,'#~#',  ifnull(emp_name,'')) as 'rr'FROM  pay_employee_master WHERE  comp_code = '" + Session["COMP_CODE"].ToString() + "' and employee_type='Permanent' and (pan_number ='' or pan_number is null) and original_bank_account_no != ''  and (pay_employee_master.LEFT_REASON is null or pay_employee_master.LEFT_REASON = '') and client_code='" + ddl_uan_client_list.SelectedValue + "' and unit_code='" + ddl_uan_unit_name.SelectedValue + "' ORDER BY  emp_code", d.con1);
        ////}

        //adp = new MySqlDataAdapter("select  unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number',`billing_date` as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value',concat(SUBSTRING(unit_gst_no,1,2),'-',`state_name`) as 'Place Of Supply','N' as 'Reverse Charge','Regular' as 'Invoice Type','' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  where  comp_code='"+Session["COMP_CODE"].ToString()+"' and billing_date BETWEEN  '2020-01-01'  AND '2020-01-31' and unit_gst_no != ''  group by auto_invoice_no order by client ", d.con1);

        //adp.Fill(dt);


        //string csv = string.Empty;

        //dt.DefaultView.Sort = "Receiver Name";
        //dt = dt.DefaultView.ToTable();
        //// dt.DefaultView.Sort= "PAY_EMPLOYEE_MASTER.PF_NUMBER";
        //int a = 0;
        //foreach (DataRow row in dt.Rows)
        //{
        //    a = 0;
        //    //foreach (DataColumn column in dt.Columns)
        //    foreach (DataRow column in dt.Rows)
        //    {
        //        //Add the Data rows.
        //        if (a == 0)
        //        {
        //           // csv += row[column.ColumnName].ToString();
        //            csv += column.ToString();
                   
        //            a = 1;
        //        }
        //    }

        //    //Add new line.
        //    csv += "\r\n";
        //}



        //String Company_name = Session["COMP_NAME"].ToString();
        //Company_name = Company_name.Replace(' ', '_');
        ////Download the CSV file.
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "attachment;filename=GSTR1B2B " + Company_name + ".csv");
        //Response.Charset = "";
        //Response.ContentType = "application/text";
        //Response.Output.Write(csv);
        //Response.Flush();
        //Response.End();

    }

    // hsn code csv file generated

    protected void btn_hsncodecsvclick(object sender ,EventArgs e) {
        
        // start
      //  MySqlDataAdapter dr = new MySqlDataAdapter(" select `housekeeiing_sac_code` as 'HSN', '' as 'Description', 'NOS-NUMBERS' as 'UQC',count(emp_code) as 'Total Quantity',sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Total Value',sum(round(amount+Service_charge,2)) as 'Taxable Value',sum(round(IGST18,2)) as 'Integrated Tax Amount', sum(round(CGST9,2)) as 'Central Tax Amount' ,sum(round(SGST9,2)) as 'State/UT Tax Amount','' as 'Cess Amount' from pay_billing_unit_rate_history  inner join pay_unit_master on pay_billing_unit_rate_history.comp_code=pay_unit_master.comp_code and pay_billing_unit_rate_history.client_code=pay_unit_master.client_code and pay_billing_unit_rate_history.unit_code=pay_unit_master.unit_code  where  pay_billing_unit_rate_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and pay_billing_unit_rate_history.unit_gst_no != ''  and auto_invoice_no !='' and GST_to_be in ('R','SEWOP')  group by housekeeiing_sac_code ", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter("SELECT sac_code AS 'HSN', '' AS 'Description', 'NOS-NUMBERS' AS 'UQC', (pay_report_gst.emp_count) AS 'Total Quantity', (ROUND((amount + CGST + SGST + IGST), 2)) AS 'Total Value', (ROUND(amount, 2)) AS 'Taxable Value', (ROUND(IGST, 2)) AS 'Integrated Tax Amount', (ROUND(CGST, 2)) AS 'Central Tax Amount', (ROUND(SGST, 2)) AS 'State/UT Tax Amount', '' AS 'Cess Amount' FROM pay_report_gst INNER JOIN pay_unit_master ON pay_report_gst.comp_code = pay_unit_master.comp_code AND pay_report_gst.client_code = pay_unit_master.client_code WHERE pay_report_gst.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND invoice_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' AND pay_report_gst.gst_no != '' AND invoice_no != '' AND pay_report_gst.GST_to_be IN ('R', 'SEWOP') and (amount is not null ||amount != 0) GROUP BY sac_code", d.con);
        d.con.Open();
        System.Data.DataTable dt = new System.Data.DataTable();
        DataSet DS1 = new DataSet();
        dr.SelectCommand.CommandTimeout = 200;
        dr.Fill(dt);

        gv_hsncodecsv.DataSource = dt;
        gv_hsncodecsv.DataBind();
        d.con.Close();
        panel_gstr1_b2b_csv.Visible = false;

        // end

    
    }


    private void generate_slab_report(int i, string where, string month_year_name)
    {

        try
        {
            string sql = null;

           if (i == 1)
            {
                //sql = "select  unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number',date_format(`billing_date`,'%d-%b-%Y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value',concat(SUBSTRING(unit_gst_no,1,2),'-',`state_name`) as 'Place Of Supply','N' as 'Reverse Charge','Regular' as 'Invoice Type','' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  where  comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '2020-01-01'  AND '2020-01-31' and unit_gst_no != '' and auto_invoice_no !='' group by auto_invoice_no order by billing_date,auto_invoice_no";
               // old excel 
              // sql = "select  unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number',date_format(`billing_date`,'%d-%b-%Y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value',concat(SUBSTRING(unit_gst_no,1,2),'-',`state_name`) as 'Place Of Supply','N' as 'Reverse Charge','Regular' as 'Invoice Type','' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  where  comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and unit_gst_no != '' and auto_invoice_no !='' group by auto_invoice_no order by billing_date,auto_invoice_no";
               //new 
               // sql = "select  pay_billing_unit_rate_history.unit_gst_no as 'GSTIN/UIN of Recipient',client as 'Receiver Name',`auto_invoice_no` as 'Invoice Number', date_format(`billing_date`,'%d-%b-%y') as 'Invoice date', sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Invoice Value', concat(SUBSTRING(pay_billing_unit_rate_history.unit_gst_no,1,2),'-',pay_billing_unit_rate_history.`state_name`) as 'Place Of Supply', 'N' as 'Reverse Charge',case GST_to_be when 'R' then 'Regular' when 'SEWP' then 'SEZ supplies with payment' when 'SEWOP' then 'SEZ supplies without payment' when 'DE' then 'Deemed Exp' when 'SCU' then 'Supplies covered under section 7 of IGST Act' else '' end as 'Invoice Type', '' as 'E-Commerce GSTIN', '18' as 'Rate','' as 'Applicable % of Tax Rate',sum(round(amount+Service_charge,2)) as 'Taxable Value','' as 'Cess Amount' from pay_billing_unit_rate_history  inner join pay_unit_master on pay_billing_unit_rate_history.comp_code=pay_unit_master.comp_code and pay_billing_unit_rate_history.client_code=pay_unit_master.client_code and pay_billing_unit_rate_history.unit_code=pay_unit_master.unit_code  where  pay_billing_unit_rate_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and pay_billing_unit_rate_history.unit_gst_no != ''  and auto_invoice_no !='' and GST_to_be in ('R','SEWOP')  group by auto_invoice_no order by billing_date,auto_invoice_no ";
                sql = "SELECT pay_report_gst.gst_no AS 'GSTIN/UIN of Recipient', client_name AS 'Receiver Name', invoice_no AS 'Invoice Number', DATE_FORMAT(invoice_date, '%d-%b-%y') AS 'Invoice date', ROUND(ROUND((amount + CGST + SGST + IGST), 2)) AS 'Invoice Value', CONCAT(SUBSTRING(pay_report_gst.gst_no,1, 2), '-', pay_report_gst.state_name) AS 'Place Of Supply', 'N' AS 'Reverse Charge', CASE pay_report_gst.GST_to_be WHEN 'R' THEN 'Regular' WHEN 'SEWP' THEN 'SEZ supplies with payment' WHEN 'SEWOP' THEN 'SEZ supplies without payment' WHEN 'DE' THEN 'Deemed Exp' WHEN 'SCU' THEN 'Supplies covered under section 7 of IGST Act' ELSE '' END AS 'Invoice Type', '' AS 'E-Commerce GSTIN', '18' AS 'Rate', '' AS 'Applicable % of Tax Rate', (ROUND(amount, 2)) AS 'Taxable Value', '' AS 'Cess Amount' FROM pay_report_gst INNER JOIN pay_unit_master ON pay_report_gst.comp_code = pay_unit_master.comp_code AND pay_report_gst.client_code = pay_unit_master.client_code WHERE pay_report_gst.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND invoice_date BETWEEN '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' AND pay_report_gst.gst_no != '' AND invoice_no != '' AND pay_report_gst.GST_to_be IN ('R', 'SEWOP') and (amount is not null ||amount != 0) GROUP BY invoice_no ORDER BY invoice_date, invoice_no ";
          
           }
           else  if(i == 2){

               //sql = "select `housekeeiing_sac_code` as 'HSN', '' as 'Description', 'NOS-NUMBERS' as 'UQC',count(emp_code) as 'Total Quantity',sum(round((amount+CGST9+SGST9+IGST18+`Service_charge`),2)) as 'Total Value',sum(round(amount+Service_charge,2)) as 'Taxable Value',sum(round(IGST18,2)) as 'Integrated Tax Amount', sum(round(CGST9,2)) as 'Central Tax Amount' ,sum(round(SGST9,2)) as 'State/UT Tax Amount','' as 'Cess Amount' from pay_billing_unit_rate_history  inner join pay_unit_master on pay_billing_unit_rate_history.comp_code=pay_unit_master.comp_code and pay_billing_unit_rate_history.client_code=pay_unit_master.client_code and pay_billing_unit_rate_history.unit_code=pay_unit_master.unit_code  where  pay_billing_unit_rate_history.comp_code='" + Session["COMP_CODE"].ToString() + "' and billing_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' and pay_billing_unit_rate_history.unit_gst_no != ''  and auto_invoice_no !='' and GST_to_be in ('R','SEWOP')  group by housekeeiing_sac_code ";
               sql = "SELECT  sac_code AS 'HSN', '' AS 'Description', 'NOS-NUMBERS' AS 'UQC', (pay_report_gst.emp_count) AS 'Total Quantity', (ROUND((amount + CGST + SGST + IGST), 2)) AS 'Total Value', (ROUND(amount, 2)) AS 'Taxable Value', (ROUND(IGST, 2)) AS 'Integrated Tax Amount', (ROUND(CGST, 2)) AS 'Central Tax Amount', (ROUND(SGST, 2)) AS 'State/UT Tax Amount', '' AS 'Cess Amount' FROM pay_report_gst INNER JOIN pay_unit_master ON pay_report_gst.comp_code = pay_unit_master.comp_code AND pay_report_gst.client_code = pay_unit_master.client_code WHERE pay_report_gst.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND invoice_date BETWEEN  '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-01'  AND '" + txt_salarymonth.Text.Substring(3, 4).ToString() + "-" + txt_salarymonth.Text.Substring(0, 2).ToString() + "-31' AND pay_report_gst.gst_no != '' AND invoice_no != '' AND pay_report_gst.GST_to_be IN ('R', 'SEWOP') and (amount is not null ||amount != 0) GROUP BY sac_code";
           
           }
            d.con.Open();
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            DataSet ds = new DataSet();
            dscmd.SelectCommand.CommandTimeout = 200;
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
               
                    if (i == 1)
                    {
                        Response.AddHeader("content-disposition", "attachment;filename=b2b" + ".xls");
                    }else if(i==2){
                        Response.AddHeader("content-disposition", "attachment;filename=hsn" + ".xls");
                    
                    }

                Response.Charset = "";
               // Response.ContentType = "application/vnd.ms-excel";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, i, month_year_name);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, i, month_year_name);
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, null, i, month_year_name);
                Repeater1.DataBind();

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Repeater1.RenderControl(htmlWrite);

             //   string style = @"<style> .textmode { } </style>";
              //  Response.Write(style);
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
        finally { d.con.Close(); }
    }

    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        int i;
        string invoice = "";
        string bill_date = "";

        string month_year = "";
        string header = "";
        string bodystr = "";
        int total_emp_count;



        public MyTemplate(ListItemType type, DataSet ds, int i, string month_year)
        {
            this.type = type;
            this.ds = ds;
            this.i = i;
            this.month_year = month_year;
            ctr = 0;
            //paid_days = 0;
            //rate = 0;
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:
                    if (i == 1)
                    {
                        // lc1 = new LiteralControl("<table border=1 ><tr><td>Client Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["client"] + "</td></tr><tr><td>Employee PF Statement</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["month"] + " / " + ds.Tables[0].Rows[ctr]["year"] + "</td></tr><tr><td>Site Name</td><td align=center colspan = 3>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td align=center colspan = 8></td><td>A/C-01</td><td>A/C-01</td><td>A/C-10</td><td>A/C-02</td><td>A/C-21</td><td>A/C-22</td></tr><tr><th>SR. NO.</th><th>EMP CODE</th><th >EMPLOYEE NAME</th><th>EMP.P.F. NO.</th><th>EMPLOYEE DESIGNATION</th><th >EMP. D.O.J.</th><th >PAID DAYS</th><th >BASIC</th><th>BASIC ARR.</th><th>VARIABLE DA</th><th>VARIABLE DA ARR.</th><th>GROSS For PF (BA+DA)</th><th>EE (12%)</th><th>PF ER (3.67%)</th><th>APS RE (8.33%)</th><th>Admin Charges (0.65%)</th><th>EDLI (0.5%)</th><th>EDLI (0.00%)</th></tr>");

                        lc = new LiteralControl("<table border=1 ><tr><th align=left>GSTIN/UIN of Recipient</th><th align=left>Receiver Name</th><th align=left>Invoice Number</th><th align=left>Invoice date</th><th align=left>Invoice Value</th><th align=left>Place Of Supply</th><th align=left>Reverse Charge</th><th align=left>Invoice Type</th><th align=left>E-Commerce GSTIN</th><th align=left>Rate</th><th align=left>Applicable % of Tax Rate</th><th align=left>Taxable Value</th><th align=left>Cess Amount</th></tr>");

                    }
                    else if (i == 2){
                        lc = new LiteralControl("<table border=1 ><tr><th align=left>HSN</th><th align=left>Description</th><th align=left>UQC</th><th align=left>Total Quantity</th><th align=left>Total Value</th><th align=left>Taxable Value</th><th align=left>Integrated Tax Amount</th><th align=left>Central Tax Amount</th><th align=left>State/UT Tax Amount</th><th align=left>Cess Amount</th></tr>");
                    }
                    break;
                case ListItemType.Item:
                   
                     if (i == 1)
                    {
                       // ctr++;
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["GSTIN/UIN of Recipient"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Receiver Name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Invoice Number"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Invoice date"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["Invoice Value"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Place Of Supply"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Reverse Charge"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Invoice Type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["E-Commerce GSTIN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Rate"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["Applicable % of Tax Rate"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Taxable Value"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["Cess Amount"] + "</td></tr>");


                        //pf_paid_days = pf_paid_days + double.Parse(ds.Tables[0].Rows[ctr]["Total_Days_Present"].ToString());
                        //total_basic1 = total_basic1 + double.Parse(ds.Tables[0].Rows[ctr]["basic"].ToString());
                        //total_basic_arr = total_basic_arr + double.Parse(ds.Tables[0].Rows[ctr]["basic_arr"].ToString());
                        //total_var_da = total_var_da + double.Parse(ds.Tables[0].Rows[ctr]["variable_da"].ToString());
                        //total_var_da_arr = total_var_da_arr + double.Parse(ds.Tables[0].Rows[ctr]["var_da_arr"].ToString());
                        //total_gross = total_gross + double.Parse(ds.Tables[0].Rows[ctr]["gross"].ToString());
                        //total_ee = total_ee + double.Parse(ds.Tables[0].Rows[ctr]["ee"].ToString());
                        //total_pf_er = total_pf_er + double.Parse(ds.Tables[0].Rows[ctr]["pf_er"].ToString());
                        //total_eps_er = total_eps_er + double.Parse(ds.Tables[0].Rows[ctr]["eps_er"].ToString());
                        //total_admin_charges = total_admin_charges + double.Parse(ds.Tables[0].Rows[ctr]["admin_charges"].ToString());
                        //total_edli_1 = total_edli_1 + double.Parse(ds.Tables[0].Rows[ctr]["edli_1"].ToString());
                        //total_edli_2 = total_edli_2 + double.Parse(ds.Tables[0].Rows[ctr]["edli_2"].ToString());

                        //basic_vda = basic_vda + double.Parse(ds.Tables[0].Rows[ctr]["actual_basic_vda"].ToString());
                        //pf_working_days = pf_working_days + double.Parse(ds.Tables[0].Rows[ctr]["month_days"].ToString());
                        //gross_wages = gross_wages + double.Parse(ds.Tables[0].Rows[ctr]["emp_basic_vda"].ToString());
                        //eps_wages = eps_wages + double.Parse(ds.Tables[0].Rows[ctr]["EPS_WAGES"].ToString());
                        //epf_cr = epf_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString());
                        //eps_cr = eps_cr + double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString());
                        //diff_cr = diff_cr + (double.Parse(ds.Tables[0].Rows[ctr]["EPF_CR"].ToString()) - double.Parse(ds.Tables[0].Rows[ctr]["EPS_CR"].ToString()));
                        //ncp_days = ncp_days + double.Parse(ds.Tables[0].Rows[ctr]["ncp_days"].ToString());
                        //ac2 = (gross_wages * 0.5) / 100;


                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                           // if (ac2 < 500) { ac2 = 500; }
                           // lc.Text = lc.Text + "<tr><b><td align=center colspan = 6>Total</td><td>" + pf_paid_days + "</td><td>" + total_basic1 + "</td><td>" + total_basic_arr + "</td><td>" + total_var_da + "</td><td>" + total_var_da_arr + "</td><td>" + total_gross + "</td><td>" + total_ee + "</td><td>" + total_pf_er + "</td><td>" + total_eps_er + "</td><td>" + total_admin_charges + "</td><td>" + total_edli_1 + "</td><td>" + total_edli_2 + "</td></tr><tr></tr><tr><tr><td></td><td>No. Of Employees</td><td>" + (ctr + 1) + "</td></tr><tr><td></td><td>Gross for PF</td><td>" + total_basic1 + "</td></tr><td>A/C 01</td><td>EE (12%)</td><td>" + total_ee + "</td></tr><tr><td>A/C 01</td><td>PF ER (3.67%)</td><td>" + total_pf_er + "</td></tr><tr><td>A/C 10</td><td>EPS ER (8.33%)</td><td>" + total_eps_er + "</td></tr><tr><td>A/C 02</td><td>Admin Charges (0.65%)</td><td>" + total_admin_charges + "</td></tr><tr><td>A/C 21</td><td>EDIL (0.5%)</td><td>" + total_edli_1 + "</td></tr><tr><td>A/C 22</td><td>EDIL (0.00%)</td><td>" + total_edli_2 + "</td></tr><tr><td>Ac 1+1+2+10+21+22 </td><td>Total</td><td>" + (total_ee + total_eps_er + total_pf_er + total_admin_charges + total_edli_1 + total_edli_2) + "</td></tr>";
                        }
                    }
                     else if (i == 2){
                         lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["HSN"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Description"] + "</td><td>" + ds.Tables[0].Rows[ctr]["UQC"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total Quantity"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["Total Value"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Taxable Value"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Integrated Tax Amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Central Tax Amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["State/UT Tax Amount"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Cess Amount"].ToString() + "</td></tr>");
                         if (ds.Tables[0].Rows.Count == ctr + 1)
                         {
                             // if (ac2 < 500) { ac2 = 500; }
                             // lc.Text = lc.Text + "<tr><b><td align=center colspan = 6>Total</td><td>" + pf_paid_days + "</td><td>" + total_basic1 + "</td><td>" + total_basic_arr + "</td><td>" + total_var_da + "</td><td>" + total_var_da_arr + "</td><td>" + total_gross + "</td><td>" + total_ee + "</td><td>" + total_pf_er + "</td><td>" + total_eps_er + "</td><td>" + total_admin_charges + "</td><td>" + total_edli_1 + "</td><td>" + total_edli_2 + "</td></tr><tr></tr><tr><tr><td></td><td>No. Of Employees</td><td>" + (ctr + 1) + "</td></tr><tr><td></td><td>Gross for PF</td><td>" + total_basic1 + "</td></tr><td>A/C 01</td><td>EE (12%)</td><td>" + total_ee + "</td></tr><tr><td>A/C 01</td><td>PF ER (3.67%)</td><td>" + total_pf_er + "</td></tr><tr><td>A/C 10</td><td>EPS ER (8.33%)</td><td>" + total_eps_er + "</td></tr><tr><td>A/C 02</td><td>Admin Charges (0.65%)</td><td>" + total_admin_charges + "</td></tr><tr><td>A/C 21</td><td>EDIL (0.5%)</td><td>" + total_edli_1 + "</td></tr><tr><td>A/C 22</td><td>EDIL (0.00%)</td><td>" + total_edli_2 + "</td></tr><tr><td>Ac 1+1+2+10+21+22 </td><td>Total</td><td>" + (total_ee + total_eps_er + total_pf_er + total_admin_charges + total_edli_1 + total_edli_2) + "</td></tr>";
                         }
                     }
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


    protected void gv_salary_deuction_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_gstr1_b2b_csv.UseAccessibleHeader = false;
            gv_gstr1_b2b_csv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }


    protected void gv_hsncodecsv_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_hsncodecsv.UseAccessibleHeader = false;
            gv_hsncodecsv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }


    protected void btn_gstr3bexcel(object sender,EventArgs e) {
        //MySqlCommand menu1;
        try {

            string month = txt_salarymonth.Text.Substring(0, 2).ToString();
            string year = txt_salarymonth.Text.Substring(3, 4).ToString();

            string check = d.getsinglestring("select  invoice_no from pay_report_gst WHERE `pay_report_gst`.`comp_code` = '"+Session["COMP_CODE"].ToString()+"'AND `invoice_date` BETWEEN STR_TO_DATE('01/" + txt_salarymonth.Text.ToString() + "', '%d/%m/%Y') AND STR_TO_DATE('31/" + txt_salarymonth.Text.ToString() + " 23:59:59', '%d/%m/%Y %H:%i:%s')");

            if (check != null && check !="")
            {

            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            xla.Visible = false;
            xla.DisplayAlerts = false;

            String path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "gst_excel\\GSTR3B");
            //String path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\cs_rolling_shutter");
            Microsoft.Office.Interop.Excel.Workbook wb1 = xla.Workbooks.Open(path, 0, true);
            // Worksheet ws1 = (Worksheet)wb1.Sheets["celtsoft"];
            Microsoft.Office.Interop.Excel.Worksheet ws1 = (Microsoft.Office.Interop.Excel.Worksheet)wb1.Sheets["GSTR-3B"];
            //MySqlCommand menu1 = new MySqlCommand("select  sum(ROUND(`amount`,2)) AS 'Taxable Value', sum(ROUND(IGST,2)) as 'IGST', sum(ROUND(CGST,2)) as 'CGST', sum(ROUND(SGST,2)) as 'SGST', company_name,`SERVICE_TAX_REG_NO`,MONTHNAME(STR_TO_DATE(substring(invoice_date,6,2), '%m')) as 'month',concat(substring(invoice_date,1,4),'-',substring(substring(invoice_date,1,4),2,3)+1) as 'year' FROM `pay_report_gst` INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` WHERE `pay_report_gst`.`comp_code` = 'C01' AND `invoice_date` BETWEEN '2020-01-01' AND '2020-01-31' AND `pay_report_gst`.`gst_no` != '' AND `invoice_no` != ''", d.con1);
            
            //MySqlCommand menu1 = new MySqlCommand("SELECT CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`amount`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`amount`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`amount`, 2)) WHEN 'DE' THEN SUM(ROUND(`amount`, 2)) WHEN ' SCU' THEN SUM(ROUND(`amount`, 2)) ELSE 0 END AS 'Taxable Value', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`IGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`IGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`IGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`IGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`IGST`, 2)) ELSE 0 END AS 'IGST', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`CGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`CGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`CGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`CGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`CGST`, 2)) ELSE 0 END AS 'CGST', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`SGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`SGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`SGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`SGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`SGST`, 2)) ELSE 0 END AS 'SGST',  company_name,`SERVICE_TAX_REG_NO`,MONTHNAME(STR_TO_DATE(substring(invoice_date,6,2), '%m')) as 'month', concat(substring(invoice_date,1,4),'-',substring(substring(invoice_date,1,4),2,3)+1) as 'year', `GST_to_be` FROM `pay_report_gst` INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code` inner JOIN `pay_unit_master` ON `pay_report_gst`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_unit_master`.`client_code`  WHERE `pay_report_gst`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `invoice_date` BETWEEN STR_TO_DATE('01/" + txt_salarymonth.Text.ToString() + "', '%d/%m/%Y') AND STR_TO_DATE('31/" + txt_salarymonth.Text.ToString() + " 23:59:59', '%d/%m/%Y %H:%i:%s') AND `pay_report_gst`.`gst_no` != '' AND `invoice_no` != '' GROUP BY `GST_to_be`", d.con1);

            MySqlCommand menu1 = new MySqlCommand("SELECT CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`amount`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`amount`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`amount`, 2)) WHEN 'DE' THEN SUM(ROUND(`amount`, 2)) WHEN ' SCU' THEN SUM(ROUND(`amount`, 2)) ELSE 0 END AS 'Taxable Value', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`IGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`IGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`IGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`IGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`IGST`, 2)) ELSE 0 END AS 'IGST', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`CGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`CGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`CGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`CGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`CGST`, 2)) ELSE 0 END AS 'CGST', CASE `GST_to_be` WHEN 'SEL' THEN 0 WHEN 'R' THEN SUM(ROUND(`SGST`, 2)) WHEN 'SWEP' THEN SUM(ROUND(`SGST`, 2)) WHEN 'SEWOP' THEN SUM(ROUND(`SGST`, 2)) WHEN 'DE' THEN SUM(ROUND(`SGST`, 2)) WHEN ' SCU' THEN SUM(ROUND(`SGST`, 2)) ELSE 0 END AS 'SGST',  company_name,`SERVICE_TAX_REG_NO`,MONTHNAME(STR_TO_DATE(substring(invoice_date,6,2), '%m')) as 'month', concat(substring(invoice_date,1,4),'-',substring(substring(invoice_date,1,4),2,3)+1) as 'year', `GST_to_be` FROM `pay_report_gst` INNER JOIN `pay_company_master` ON `pay_report_gst`.`comp_code` = `pay_company_master`.`comp_code`  WHERE `pay_report_gst`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `invoice_date` BETWEEN STR_TO_DATE('01/" + txt_salarymonth.Text.ToString() + "', '%d/%m/%Y') AND STR_TO_DATE('31/" + txt_salarymonth.Text.ToString() + " 23:59:59', '%d/%m/%Y %H:%i:%s') AND `pay_report_gst`.`gst_no` != '' AND `invoice_no` != '' and (amount is not null || amount != 0) GROUP BY `GST_to_be`  ", d.con1);

            d.con1.Open();

            MySqlDataReader dr = menu1.ExecuteReader();

             while(dr.Read()){

                try {

                    if (dr.GetValue(8).ToString() == "R") {
                        ws1.Cells[11, 3] = dr.GetValue(0).ToString();
                        ws1.Cells[11, 4] = dr.GetValue(1).ToString();
                        ws1.Cells[11, 5] = dr.GetValue(2).ToString();
                    }
                    else if (dr.GetValue(8).ToString() == "SEWOP")
                    {
                        ws1.Cells[13, 3] = dr.GetValue(0).ToString();
                        //ws1.Cells[11, 4] = dr.GetValue(1).ToString();
                        //ws1.Cells[11, 5] = dr.GetValue(2).ToString();
                    }
                
               // ws1.Cells[11, 6] = dr.GetValue(3).ToString();
                ws1.Cells[6, 3] = dr.GetValue(4).ToString();
                ws1.Cells[5, 3] = dr.GetValue(5).ToString();
                ws1.Cells[5, 7] = dr.GetValue(7).ToString();
                ws1.Cells[6, 7] = dr.GetValue(6).ToString();

               
                }
                catch (Exception ee) { throw ee; }
                finally
                {
                   
                   // wb1.Close();
                  //  xla.Quit();
                   // System.Runtime.InteropServices.Marshal.ReleaseComObject(wb1);
                   // System.Runtime.InteropServices.Marshal.ReleaseComObject(xla);
                }
                
            }
            
            //xla.Visible = true;
            
         // wb1.SaveAs("j:\\GSTR3B.xlsm");
          wb1.SaveAs(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "gst_excel\\GSTR3B_new.xlsm"));
            
          wb1.Close(true);
            xla.Quit();

           // System.Runtime.InteropServices.Marshal.ReleaseComObject(wb1);
          // System.Runtime.InteropServices.Marshal.ReleaseComObject(xla);
         // System.Runtime.InteropServices.Marshal.ReleaseComObject(ws1);

         // download excel code start
          var fileName = "GSTR3B_"+month+"_"+year+".xlsm";
          FileInfo file = new FileInfo(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "gst_excel\\GSTR3B_new.xlsm"));
          if (file.Exists)
          {
              Response.Clear();
              Response.ClearHeaders();
              Response.ClearContent();
              Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
              Response.AddHeader("Content-Type", "application/Excel");
              Response.ContentType = "application/vnd.xls";
              Response.AddHeader("Content-Length", file.Length.ToString());
              Response.WriteFile(file.FullName);
              Response.Flush();
              Response.End();

          }
          else
          {
              Response.Write("This file does not exist.");
          }
           //end

          d.con1.Close();
            menu1.Dispose();
            dr.Close();
            dr.Dispose();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);

            } 
        

        }
        catch(Exception ee){
            throw ee;
        }finally{
          File.Delete(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "gst_excel\\GSTR3B_new.xlsm"));
            
        }
    
    }

}