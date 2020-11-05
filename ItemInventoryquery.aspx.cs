using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class ItemInventoryquery : System.Web.UI.Page
{
    DAL d = new DAL();
    double a = 0, b = 0, c = 0;
    static int serialno;
    public string reject_emp_legal = "0";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
        }
    }
    
    
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void rdb_emp_adv_salary_CheckedChanged(object sender, EventArgs e)
    {
        //string query = " select PAY_ITEM_TRANSACTION.UNIT_CODE ,PAY_UNIT_MASTER.UNIT_NAME,SUM(CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE IN ('STP','STT') THEN QUANTITY ELSE 0 END ) AS RECEIPT, SUM(CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE NOT IN ('STP','STT') THEN QUANTITY ELSE 0 END ) AS ISSUE, SUM(CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE IN ('STP','STT') THEN QUANTITY ELSE QUANTITY*-1 END ) AS STOCK,SUM(CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE IN ('STP','STT') THEN QUANTITY*PAY_ITEM_MASTER.SALES_RATE ELSE QUANTITY*-1*PAY_ITEM_MASTER.SALES_RATE END ) AS STOCK_VAL from PAY_ITEM_TRANSACTION,PAY_ITEM_TRANSACTION_DETAIL ,PAY_ITEM_MASTER ,PAY_UNIT_MASTER WHERE PAY_ITEM_TRANSACTION.COMP_CODE=PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE AND PAY_ITEM_TRANSACTION.TASK_CODE=PAY_ITEM_TRANSACTION_DETAIL.TASK_CODE AND PAY_ITEM_TRANSACTION.DOC_NO=PAY_ITEM_TRANSACTION_DETAIL.DOC_NO AND  PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE=PAY_ITEM_MASTER.COMP_CODE AND PAY_ITEM_TRANSACTION_DETAIL.ITEM_CODE=PAY_ITEM_MASTER.ITEM_CODE AND PAY_ITEM_TRANSACTION.COMP_CODE=PAY_UNIT_MASTER.COMP_CODE AND PAY_ITEM_TRANSACTION.UNIT_CODE=PAY_UNIT_MASTER.UNIT_CODE AND  PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE='C02' AND PAY_ITEM_TRANSACTION_DETAIL.ITEM_CODE='I0001' GROUP BY PAY_ITEM_TRANSACTION.UNIT_CODE,PAY_UNIT_MASTER.UNIT_NAME ORDER BY PAY_ITEM_TRANSACTION.UNIT_CODE ";

        //DataSet ds = new DataSet();
        //MySqlDataAdapter adp = new MySqlDataAdapter(query, d.con);
        //d.con.Open();
        //adp.Fill(ds);
        //gv_itemquery.DataSource = ds.Tables[0];
        //gv_itemquery.DataBind();
    }
  
    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            gv_itemunitquery.DataSource = dt;
            gv_itemunitquery.DataBind();
        }
    }
    
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        //Panel9.Visible = false;
        chk_item_code.Visible = false;
        //Panel3.Visible = false;
        //Panel5.Visible = false;
        gv_itemquery.DataSource = null;
        gv_itemquery.DataBind();
        gv_itemunitquery.DataSource = null;
        gv_itemunitquery.DataBind();
        gv_itemdetails.DataSource = null;
        gv_itemdetails.DataBind();
       // btn_excel.Visible = false;
        //rdb_emp_adv_salary.Checked = false;
    }
    protected void btn_Process_Click(object sender, EventArgs e)
    {

        //string query = "SELECT `PAY_ITEM_MASTER`.`ITEM_CODE`,`ITEM_NAME`,  pay_transaction_po.CUST_NAME,`unit`,`inward` AS 'INWARD',`outward` AS 'OUTWARD',`Stock` AS 'STOCK',(`Stock` * `PURCHASE_RATE`) AS 'STOCK_VAL' FROM `pay_item_master` INNER JOIN `pay_transaction_po` ON `pay_item_master`.`comp_code` = `pay_transaction_po`.`comp_code` and  `pay_item_master`.`product_service` = `pay_transaction_po`.`vendor_categorie` WHERE `Stock` != '0' AND PAY_ITEM_MASTER.COMP_CODE ='" + Session["COMP_CODE"] + "'  group by ITEM_CODE";
        string query = "SELECT `PAY_ITEM_MASTER`.`ITEM_CODE`,`ITEM_NAME`,  pay_transaction_po.CUST_NAME,`unit`, sum(`Stock`) AS 'STOCK',(`Stock` * `PURCHASE_RATE`) AS 'STOCK_VAL' FROM `pay_item_master` INNER JOIN `pay_transaction_po` ON `pay_item_master`.`comp_code` = `pay_transaction_po`.`comp_code` and  `pay_item_master`.`product_service` = `pay_transaction_po`.`vendor_categorie` WHERE `Stock` != '0' AND PAY_ITEM_MASTER.COMP_CODE ='" + Session["COMP_CODE"] + "'  group by ITEM_NAME";
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); 
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(query, d.con);
            

            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_itemquery.Visible = true;
                gv_itemquery.DataSource = ds.Tables[0];
                gv_itemquery.DataBind();
                d.con.Close();
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Stock Is Empty');", true);
            }
            // btn_excel.Visible = true;
        }
        catch (Exception ee)
        {
        }
        finally
        {
            d.con.Close();
        }

        
    }
    protected void gv_itemunitquery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       // e.Row.Cells[0].Style["display"] = "none";// = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_itemquery, "Select$" + e.Row.RowIndex);
        }
       
    }
   
    protected void gv_itemquery_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        System.Web.UI.WebControls.Label chk_item_code = (System.Web.UI.WebControls.Label)gv_itemquery.SelectedRow.FindControl("chk_item_code");
        string unitcode = chk_item_code.Text;

        System.Web.UI.WebControls.Label lbl_custname = (System.Web.UI.WebControls.Label)gv_itemquery.SelectedRow.FindControl("lbl_custname");
        string vendor = lbl_custname.Text;

        System.Web.UI.WebControls.Label lbl_itemname = (System.Web.UI.WebControls.Label)gv_itemquery.SelectedRow.FindControl("lbl_itemname");
        string itemname = lbl_itemname.Text;

    
         //   string query = "SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE', CAST(group_concat(distinct(`PAY_TRANSACTION`.`DOC_NO`)) as CHAR) as DOC_NO, DATE_FORMAT(`PAY_TRANSACTION`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', (SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` = 'INV' THEN (SELECT `pay_customer_master`.`cust_name` FROM `pay_customer_master` WHERE `pay_transaction`.`cust_code` = `pay_customer_master`.`cust_id`) ELSE (SELECT `pay_vendor_master`.`vend_name` FROM `pay_vendor_master` WHERE `pay_transaction`.`cust_code` = `pay_vendor_master`.`vend_id`) END) AS 'PARTY NAME', CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` IN ('STP', 'STT') THEN `QUANTITY` ELSE 0 END AS 'RECEIPT', CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum( `QUANTITY`) ELSE 0 END AS 'ISSUE', `PAY_TRANSACTION`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `PAY_TRANSACTION` INNER JOIN `PAY_TRANSACTION_DETAILS` ON `PAY_TRANSACTION`.`COMP_CODE` = `PAY_TRANSACTION_DETAILS`.`COMP_CODE` AND `PAY_TRANSACTION`.`TASK_CODE` = `PAY_TRANSACTION_DETAILS`.`TASK_CODE` AND `PAY_TRANSACTION`.`DOC_NO` = `PAY_TRANSACTION_DETAILS`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = '" + unitcode.ToString() + "' group by `PAY_TRANSACTION`.`DOC_NO` UNION SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE',CAST(GROUP_CONCAT(DISTINCT (`pay_transactionp`.`DOC_NO`)) AS char) AS 'DOC_NO', DATE_FORMAT(`pay_transactionp`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', (SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` = 'INV' THEN (SELECT `pay_customer_master`.`cust_name` FROM `pay_customer_master` WHERE `pay_transactionp`.`cust_code` = `pay_customer_master`.`cust_id`) ELSE (SELECT `pay_vendor_master`.`vend_name` FROM `pay_vendor_master` WHERE `pay_transactionp`.`cust_code` = `pay_vendor_master`.`vend_id`) END) AS 'PARTY NAME', CASE WHEN `pay_transactionp_details`.`TASK_CODE` IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'RECEIPT', CASE WHEN `pay_transactionp_details`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'ISSUE', `pay_transactionp`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `pay_transactionp` INNER JOIN `pay_transactionp_details` ON `pay_transactionp`.`COMP_CODE` = `pay_transactionp_details`.`COMP_CODE` AND `pay_transactionp`.`TASK_CODE` = `pay_transactionp_details`.`TASK_CODE` AND `pay_transactionp`.`DOC_NO` = `pay_transactionp_details`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `pay_transactionp_details`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `pay_transactionp_details`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `pay_transactionp_details`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_transactionp_details`.`ITEM_CODE` = '" + unitcode.ToString() + "'  group by `pay_transactionp`.`DOC_NO` ";
        //string query = "SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE', CAST(group_concat(distinct(`PAY_TRANSACTION`.`DOC_NO`)) as CHAR) as DOC_NO, DATE_FORMAT(`PAY_TRANSACTION`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', ''AS 'VENDOR_NAME',(SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` = 'INV' THEN PAY_TRANSACTION.`CUST_NAME` ELSE (SELECT `pay_vendor_master`.`vend_name` FROM `pay_vendor_master` WHERE `pay_transaction`.`cust_code` = `pay_vendor_master`.`vend_id`) END) AS 'CLIENT NAME',(SELECT `UNIT_NAME` FROM `pay_unit_master` WHERE `unit_code` = `pay_transaction`.`branch_name` AND `comp_code` = `pay_transaction`.`COMP_CODE`) AS 'LOCATION', ''  AS 'uniform_size', CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` IN ('STP', 'STT') THEN `QUANTITY` ELSE 0 END AS 'RECEIPT',CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum( `QUANTITY`) ELSE 0 END AS 'ISSUE', `PAY_TRANSACTION`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `PAY_TRANSACTION` INNER JOIN `PAY_TRANSACTION_DETAILS` ON `PAY_TRANSACTION`.`COMP_CODE` = `PAY_TRANSACTION_DETAILS`.`COMP_CODE` AND `PAY_TRANSACTION`.`TASK_CODE` = `PAY_TRANSACTION_DETAILS`.`TASK_CODE` AND `PAY_TRANSACTION`.`DOC_NO` = `PAY_TRANSACTION_DETAILS`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = '" + unitcode.ToString() + "' group by `PAY_TRANSACTION`.`DOC_NO` UNION SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE',CAST(GROUP_CONCAT(DISTINCT (`pay_transactionp`.`DOC_NO`)) AS char) AS 'DOC_NO', DATE_FORMAT(`pay_transactionp`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', (SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` = 'INV' THEN (SELECT `pay_customer_master`.`cust_name` FROM `pay_customer_master` WHERE `pay_transactionp`.`cust_code` = `pay_customer_master`.`cust_id`) ELSE (SELECT `pay_transactionp`.`CUST_NAME` FROM `pay_transactionp` WHERE `pay_transactionp_details`.`DOC_NO` = `pay_transactionp`.`DOC_NO`) END) AS 'VENDOR_NAME',''AS 'CLIENT NAME',''AS 'LOCATION', pay_transactionp_details.`size_uniform`, CASE WHEN `pay_transactionp_details`.`TASK_CODE` IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'RECEIPT', CASE WHEN `pay_transactionp_details`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'ISSUE', `pay_transactionp`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `pay_transactionp` INNER JOIN `pay_transactionp_details` ON `pay_transactionp`.`COMP_CODE` = `pay_transactionp_details`.`COMP_CODE` AND `pay_transactionp`.`TASK_CODE` = `pay_transactionp_details`.`TASK_CODE` AND `pay_transactionp`.`DOC_NO` = `pay_transactionp_details`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `pay_transactionp_details`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `pay_transactionp_details`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `pay_transactionp_details`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_transactionp_details`.`ITEM_CODE` = '" + unitcode.ToString() + "'  group by `pay_transactionp`.`DOC_NO` ";

        gv_itemdetails.DataSource = null;
       // string query = "SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE', CAST(group_concat(distinct(`PAY_TRANSACTION`.`DOC_NO`)) as CHAR) as DOC_NO, DATE_FORMAT(`PAY_TRANSACTION`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', ''AS 'VENDOR_NAME',(SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` = 'INV' THEN PAY_TRANSACTION.`CUST_NAME` ELSE (SELECT `pay_vendor_master`.`vend_name` FROM `pay_vendor_master` WHERE `pay_transaction`.`cust_code` = `pay_vendor_master`.`vend_id`) END) AS 'CLIENT NAME',(SELECT `UNIT_NAME` FROM `pay_unit_master` WHERE `unit_code` = `pay_transaction`.`branch_name` AND `comp_code` = `pay_transaction`.`COMP_CODE`) AS 'LOCATION', ''  AS 'SIZE', CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` IN ('STP', 'STT') THEN `QUANTITY` ELSE 0 END AS 'RECEIPT',CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum( `QUANTITY`) ELSE 0 END AS 'ISSUE', `PAY_TRANSACTION`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `PAY_TRANSACTION` INNER JOIN `PAY_TRANSACTION_DETAILS` ON `PAY_TRANSACTION`.`COMP_CODE` = `PAY_TRANSACTION_DETAILS`.`COMP_CODE` AND `PAY_TRANSACTION`.`TASK_CODE` = `PAY_TRANSACTION_DETAILS`.`TASK_CODE` AND `PAY_TRANSACTION`.`DOC_NO` = `PAY_TRANSACTION_DETAILS`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = '" + unitcode.ToString() + "' group by `PAY_TRANSACTION`.`DOC_NO` UNION SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE',CAST(GROUP_CONCAT(DISTINCT (`pay_transactionp`.`DOC_NO`)) AS char) AS 'DOC_NO', DATE_FORMAT(`pay_transactionp`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', (SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` = 'INV' THEN (SELECT `pay_customer_master`.`cust_name` FROM `pay_customer_master` WHERE `pay_transactionp`.`cust_code` = `pay_customer_master`.`cust_id`) ELSE (SELECT `pay_transactionp`.`CUST_NAME` FROM `pay_transactionp` WHERE `pay_transactionp_details`.`DOC_NO` = `pay_transactionp`.`DOC_NO`) END) AS 'VENDOR_NAME',''AS 'CLIENT NAME',''AS 'LOCATION', pay_transactionp_details.`size_uniform`, CASE WHEN `pay_transactionp_details`.`TASK_CODE` IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'RECEIPT', CASE WHEN `pay_transactionp_details`.`TASK_CODE` NOT IN ('STP', 'STT') THEN sum(`QUANTITY`) ELSE 0 END AS 'ISSUE', `pay_transactionp`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `pay_transactionp` INNER JOIN `pay_transactionp_details` ON `pay_transactionp`.`COMP_CODE` = `pay_transactionp_details`.`COMP_CODE` AND `pay_transactionp`.`TASK_CODE` = `pay_transactionp_details`.`TASK_CODE` AND `pay_transactionp`.`DOC_NO` = `pay_transactionp_details`.`DOC_NO` INNER JOIN `PAY_ITEM_MASTER` ON `pay_transactionp_details`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `pay_transactionp_details`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `pay_transactionp_details`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "'  and pay_transactionp.`CUST_NAME` = '" + vendor.ToString() + "'  group by `pay_transactionp`.`DOC_NO` ";
        string query = "SELECT CASE WHEN `PAY_TRANSACTION`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE',CAST(GROUP_CONCAT(DISTINCT (`PAY_TRANSACTION`.`DOC_NO`)) AS char) AS 'DOC_NO',   DATE_FORMAT(`PAY_TRANSACTION`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', '' AS 'VENDOR_NAME', '' AS 'SIZE', CASE WHEN `PAY_TRANSACTION_DETAILS`.`TASK_CODE` IN ('STP', 'STT') THEN `QUANTITY` ELSE 0 END AS 'RECEIPT', `PAY_TRANSACTION`.`FINAL_PRICE` AS 'BILL TOTAL' FROM  `PAY_TRANSACTION` INNER JOIN `PAY_TRANSACTION_DETAILS` ON `PAY_TRANSACTION`.`COMP_CODE` = `PAY_TRANSACTION_DETAILS`.`COMP_CODE` AND `PAY_TRANSACTION`.`TASK_CODE` = `PAY_TRANSACTION_DETAILS`.`TASK_CODE` AND `PAY_TRANSACTION`.`DOC_NO` = `PAY_TRANSACTION_DETAILS`.`DOC_NO`  INNER JOIN `PAY_ITEM_MASTER` ON `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE `PAY_TRANSACTION_DETAILS`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `PAY_TRANSACTION_DETAILS`.`ITEM_CODE` = '" + unitcode.ToString() + "' GROUP BY `PAY_TRANSACTION`.`DOC_NO` UNION SELECT   CASE WHEN `pay_transactionp`.`TASK_CODE` IN ('STP') THEN 'INWARD' ELSE 'OUTWARD' END AS 'TASK_CODE', CAST(GROUP_CONCAT(DISTINCT (`pay_transactionp`.`DOC_NO`)) AS char) AS 'DOC_NO',DATE_FORMAT(`pay_transactionp`.`DOC_DATE`, '%d/%m/%Y') AS 'DOC_DATE', (SELECT CASE WHEN `pay_transactionp`.`TASK_CODE` = 'INV' THEN (SELECT `pay_customer_master`.`cust_name` FROM `pay_customer_master` WHERE `pay_transactionp`.`cust_code` = `pay_customer_master`.`cust_id`) ELSE (SELECT `pay_transactionp`.`CUST_NAME` FROM `pay_transactionp` WHERE `pay_transactionp_details`.`DOC_NO` = `pay_transactionp`.`DOC_NO`) END) AS 'VENDOR_NAME', CASE WHEN `pay_transactionp_details`.`item_type` = 'Uniform' THEN `size_uniform` WHEN `pay_transactionp_details`.`item_type` = 'Shoes' THEN `size_shoes` END AS 'size', CASE WHEN `pay_transactionp_details`.`TASK_CODE` IN ('STP', 'STT') THEN SUM(`QUANTITY`) ELSE 0 END AS 'RECEIPT',`pay_transactionp`.`FINAL_PRICE` AS 'BILL TOTAL' FROM `pay_transactionp` INNER JOIN `pay_transactionp_details` ON `pay_transactionp`.`COMP_CODE` = `pay_transactionp_details`.`COMP_CODE` AND `pay_transactionp`.`TASK_CODE` = `pay_transactionp_details`.`TASK_CODE` AND `pay_transactionp`.`DOC_NO` = `pay_transactionp_details`.`DOC_NO`  INNER JOIN `PAY_ITEM_MASTER` ON `pay_transactionp_details`.`COMP_CODE` = `PAY_ITEM_MASTER`.`COMP_CODE` AND `pay_transactionp_details`.`ITEM_CODE` = `PAY_ITEM_MASTER`.`ITEM_CODE` WHERE   `pay_transactionp_details`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "'  AND `pay_transactionp`.`CUST_NAME` = '" + vendor.ToString() + "'  GROUP BY   `pay_transactionp_details`.`size_uniform`  ";
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter(query, d.con);
        d.con.Open();
        adp.Fill(ds);
        gv_itemdetails.DataSource = ds.Tables[0];
        gv_itemdetails.DataBind();
        Panel9.Visible = true;
        d.con.Close();
    }
    protected void chk_unit_CheckedChanged(object sender, EventArgs e)
    {
        string unitcode = null;
        
        foreach (GridViewRow row in gv_itemquery.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("chk_item_code");
            if (check.Checked)
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffff"); 
                unitcode = check.Text.ToString();
                gv_itemdetails.DataSource = null;

                string query = " select PAY_ITEM_TRANSACTION.TASK_CODE,PAY_ITEM_TRANSACTION.DOC_NO,DATE_FORMAT(PAY_ITEM_TRANSACTION.DOC_DATE,'%d/%m/%Y') AS DOC_DATE ,(select case When PAY_ITEM_TRANSACTION.TASK_CODE = 'ISS' then (select pay_customer_master.cust_name from pay_customer_master Where pay_item_transaction.party_code = pay_customer_master.cust_id) else (select pay_vendor_master.vend_name from pay_vendor_master where pay_item_transaction.party_code = pay_vendor_master.vend_id) END) as 'PARTY NAME', CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE IN ('STP','STT') THEN QUANTITY ELSE 0 END  AS RECEIPT, CASE WHEN PAY_ITEM_TRANSACTION.TASK_CODE NOT IN ('STP','STT') THEN QUANTITY ELSE 0 END AS ISSUE , PAY_ITEM_TRANSACTION.NET_TOTAL As 'BILL TOTAL'  from PAY_ITEM_TRANSACTION,PAY_ITEM_TRANSACTION_DETAIL ,PAY_ITEM_MASTER ,PAY_UNIT_MASTER WHERE PAY_ITEM_TRANSACTION.COMP_CODE=PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE AND PAY_ITEM_TRANSACTION.TASK_CODE=PAY_ITEM_TRANSACTION_DETAIL.TASK_CODE AND PAY_ITEM_TRANSACTION.DOC_NO=PAY_ITEM_TRANSACTION_DETAIL.DOC_NO AND  PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE=PAY_ITEM_MASTER.COMP_CODE AND PAY_ITEM_TRANSACTION_DETAIL.ITEM_CODE=PAY_ITEM_MASTER.ITEM_CODE AND PAY_ITEM_TRANSACTION.COMP_CODE=PAY_UNIT_MASTER.COMP_CODE AND PAY_ITEM_TRANSACTION.UNIT_CODE=PAY_UNIT_MASTER.UNIT_CODE AND  PAY_ITEM_TRANSACTION_DETAIL.COMP_CODE='" + Session["COMP_CODE"] + "' AND PAY_ITEM_TRANSACTION_DETAIL.ITEM_CODE='" + unitcode.ToString() + "' ORDER BY PAY_ITEM_TRANSACTION.DOC_DATE ";
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter(query, d.con);
                d.con.Open();
                adp.Fill(ds);
                gv_itemdetails.DataSource = ds.Tables[0];
                gv_itemdetails.DataBind();
                Panel9.Visible = true;
                d.con.Close();
            }
            else
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF"); 
            }
        }
      
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
    //protected void btn_excel_Click(object sender, EventArgs e)
    //{
    //    if (gv_itemquery.Rows.Count > 0)
    //    {
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", "attachment;filename=Inventory_excel_data.xls");
    //        Response.Charset = "";
    //        Response.ContentType = "application/vnd.ms-excel";
    //        using (StringWriter sw = new StringWriter())
    //        {
    //            HtmlTextWriter hw = new HtmlTextWriter(sw);

    //            //To Export all pages
    //            gv_itemquery.AllowPaging = false;
    //            UnitBAL ubl1 = new UnitBAL();
    //            //DataSet ds = new DataSet();
    //            //ds = ubl1.UnitSelect();
    //            //UnitGridView.DataSource = ds.Tables["PAY_UNIT_MASTER"];


    //            foreach (TableCell cell in gv_itemquery.HeaderRow.Cells)
    //            {
    //                cell.BackColor = gv_itemquery.HeaderStyle.BackColor;
    //            }
    //            foreach (GridViewRow row in gv_itemquery.Rows)
    //            {

    //                foreach (TableCell cell in row.Cells)
    //                {
    //                    if (row.RowIndex % 2 == 0)
    //                    {
    //                        cell.BackColor = gv_itemquery.AlternatingRowStyle.BackColor;
    //                    }
    //                    else
    //                    {
    //                        cell.BackColor = gv_itemquery.RowStyle.BackColor;
    //                    }
    //                    cell.CssClass = "textmode";
    //                }
    //            }

    //            gv_itemquery.RenderControl(hw);

    //            //style to format numbers to string
    //            string style = @"<style> .textmode { } </style>";
    //            Response.Write(style);
    //            Response.Output.Write(sw.ToString());
    //            Response.Flush();
    //            Response.End();
    //        }
    //    }
    //}

    //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        int columns = gv_itemquery.Columns.Count;

    //        //GridView HeaderGrid = (GridView)sender;
    //        //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        //TableCell HeaderCell = new TableCell();
    //        //HeaderCell.Text = Session["COMP_NAME"].ToString();
    //        //HeaderCell.ColumnSpan = 5;
    //        //HeaderGridRow.Cells.Add(HeaderCell);
    //        //gv_itemquery.Controls[0].Controls.AddAt(0, HeaderGridRow);

            
    //        //GridViewRow HeaderGridRow2 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
    //        //TableCell HeaderCell2 = new TableCell();
    //        //HeaderCell2.Text = "Stock Status";
    //        //HeaderCell2.ColumnSpan = 5;
    //        //HeaderGridRow2.Cells.Add(HeaderCell2);
    //        //gv_itemquery.Controls[0].Controls.AddAt(1, HeaderGridRow2);

    //        //GridViewRow HeaderGridRow3 = new GridViewRow(2, 2, DataControlRowType.Header, DataControlRowState.Insert);
    //        //TableCell HeaderCell3 = new TableCell();
    //        //HeaderCell3.Text = System.DateTime.Now.Date.ToShortDateString();

    //        //HeaderCell3.ColumnSpan = 5;
    //        //HeaderGridRow3.Cells.Add(HeaderCell3);
    //        //gv_itemquery.Controls[0].Controls.AddAt(2, HeaderGridRow3);

    //        //HeaderGridRow.BackColor = ColorTranslator.FromHtml("#fff");
    //        //HeaderGridRow.ForeColor = ColorTranslator.FromHtml("#000");

    //        //HeaderGridRow2.BackColor = ColorTranslator.FromHtml("#fff");
    //        //HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("#000");

    //        //HeaderGridRow3.BackColor = ColorTranslator.FromHtml("#fff");
    //        //HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("#000");
    //    }

    //}

    
    protected void gv_itemquery_PreRender(object sender, EventArgs e)
    {
        try
        {
            //gv_itemquery.UseAccessibleHeader = false;
            gv_itemquery.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
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
    protected void gv_itemdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string username = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Product_Name"));
            LinkButton link_b_Click = (LinkButton)e.Row.FindControl("link_b_Click");
            
        }
    }

    protected void link_b_Click(object sender, EventArgs e)
    {
        GridViewRow gv_itemdetails = (GridViewRow)((LinkButton)sender).NamingContainer;


        Session["EXPENSE_ID"] = gv_itemdetails.Cells[5].Text;
        //string size = Session["EXPENSE_ID"].ToString();

        mp.Show();
    }
}