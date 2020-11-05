using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Data.OleDb;

public partial class balance_sheet_accounting : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d2 = new DAL();
    DAL d3 = new DAL();
    DAL d4 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            group_name();

            btn_reject_sr.Visible = false;
        }
        ledger_gv();
        upload_gv();

        // receipt details approve reject komal 20-06-2020
        panel_received_type.Visible = false;
        panel_receipt_details.Visible = false;


    }

    //protected void btn_save_Click(object sender, EventArgs e)
    //{
    //    //minibank details
    //    foreach (GridViewRow row in gv_data.Rows)
    //    {

    //       // d.operation("INSERT INTO " + pay_zone_master + "(COMP_CODE,Field1,Type) VALUES('" + txt_companycode.Text + "','" + row.Cells[2].Text + "','minibank')");
    //    }
    //}
    protected void btn_group_Click(object sender, EventArgs e)
    {
        try
        {
            int res = 0;
            res = d.operation("INSERT INTO  pay_group_balancesheet SET group_name = '" + txt_group.Text + "' ,expese = '" + ddl_expanse.Text + "'");
            if (res != 0)
            {
                group_name();
                ledger_gv();
                txt_group.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Group Create successfully!!');", true);

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void group_name()
    {
        ddl_group_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT group_name from pay_group_balancesheet", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_group_name.DataSource = dt_item;
                ddl_group_name.DataTextField = dt_item.Columns[0].ToString();
                ddl_group_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_group_name.DataBind();

                ddl_group.DataSource = dt_item;
                ddl_group.DataTextField = dt_item.Columns[0].ToString();
                ddl_group.DataValueField = dt_item.Columns[0].ToString();
                ddl_group.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_group_name.Items.Insert(0, "Select");

            ddl_group.Items.Insert(0, "Select");


        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }

    protected void btn_subgp_Click(object sender, EventArgs e)
    {
        try
        {
            string sub_group = "";
            int res = 0;
            sub_group = d.getsinglestring("select subgroup_name from pay_group_balancesheet where subgroup_name = '" + txt_sub_group.Text + "' and group_name = '" + ddl_group_name.SelectedValue + "'");
            if (sub_group == "")
            {
                sub_group = d.getsinglestring("select subgroup_name from pay_group_balancesheet where group_name = '" + ddl_group_name.SelectedValue + "'");
                if (sub_group == "")
                {
                    res = d.operation("update pay_group_balancesheet SET subgroup_name = '" + txt_sub_group.Text + "' ,expese = '" + txt_expese.Text + "' where group_name = '" + ddl_group_name.SelectedValue + "'");

                }
                else
                {
                    res = d.operation("INSERT INTO pay_group_balancesheet (group_name,expese,subgroup_name) VALUES ('" + ddl_group_name.SelectedValue + "','" + txt_expese.Text + "','" + txt_sub_group.Text + "')");
                }
                if (res != 0)
                {
                    group_name();
                    ledger_gv();
                    txt_sub_group.Text = "";
                    txt_expese.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sub Group Create successfully!!');", true);
                }
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' This Sub Group Already Created!!');", true); }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_group_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ddl_group_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT subgroup_name from pay_group_balancesheet where group_name = '" + ddl_group.SelectedValue + "' ", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_sub_group.DataSource = dt_item;
                ddl_sub_group.DataTextField = dt_item.Columns[0].ToString();
                ddl_sub_group.DataValueField = dt_item.Columns[0].ToString();
                ddl_sub_group.DataBind();

            }
            dt_item.Dispose();
            d.con.Close();
            ddl_sub_group.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

        System.Data.DataTable dt_item1 = new System.Data.DataTable();
        MySqlCommand cmd = new MySqlCommand("Select DISTINCT if(expese = 0,'Direct','Indirect') from pay_group_balancesheet where group_name = '" + ddl_group.SelectedValue + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_expese1.Text = dr.GetValue(0).ToString();
                dr.Close();
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_ledger_Click(object sender, EventArgs e)
    {
        try
        {
            string sub_group = "";
            int res = 0;
            sub_group = d.getsinglestring("select ledger_name from pay_group_balancesheet where group_name = '" + ddl_group.SelectedValue + "' and subgroup_name = '" + ddl_sub_group.Text + "' and ledger_name = '" + txt_ledger.Text + "' ");
            if (sub_group == "")
            {
                sub_group = d.getsinglestring("select ledger_name from pay_group_balancesheet where group_name = '" + ddl_group.SelectedValue + "' and  subgroup_name = '" + ddl_sub_group.SelectedValue + "'");
                if (sub_group == "")
                {
                    res = d.operation("update pay_group_balancesheet SET ledger_name = '" + txt_ledger.Text + "',expese = '" + txt_expese1.Text + "' where group_name = '" + ddl_group.SelectedValue + "' and subgroup_name = '" + ddl_sub_group.SelectedValue + "'");
                }
                else
                {
                    res = d.operation("INSERT INTO pay_group_balancesheet (group_name,expese,subgroup_name,ledger_name) VALUES ('" + ddl_group.SelectedValue + "','" + txt_expese1.Text + "','" + ddl_sub_group.Text + "','" + txt_ledger.Text + "')");
                }
                if (res != 0)
                {
                    group_name();
                    ledger_gv();
                    txt_ledger.Text = "";
                    txt_expese1.Text = "";
                    ddl_sub_group.Items.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Ledger Create successfully!!');", true);

                }
            }
            else
            {
                group_name();
                ledger_gv();
                txt_ledger.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' This Ledger Already Created!!');", true);
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ledger_gv()
    {
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select group_name,subgroup_name,ledger_name,expese from pay_group_balancesheet", d.con);
            cmd_item.Fill(ds);
            gv_ledger.DataSource = ds.Tables[0];
            gv_ledger.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void gv_ledger_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_ledger.UseAccessibleHeader = false;
            gv_ledger.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_ledger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }
    protected void ddl_group_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlCommand cmd = new MySqlCommand("Select DISTINCT if(expese = 0,'Direct','Indirect') from pay_group_balancesheet where group_name = '" + ddl_group_name.SelectedValue + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_expese.Text = dr.GetValue(0).ToString();
                dr.Close();
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string FilePath = "";
        if (file_upload.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(file_upload.PostedFile.FileName);
                string Extension = Path.GetExtension(file_upload.PostedFile.FileName);
                string FolderPath = "~/Temp_images/";
                FilePath = Server.MapPath(FolderPath + FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    // string FolderPath = "~/Temp_images/";
                    //FilePath = Server.MapPath(FolderPath + FileName);
                    file_upload.SaveAs(FilePath);
                    btn_Import_Click(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Ledger File Uploaded Successfully...');", true);
                    File.Delete(FilePath);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload a valid excel file.');", true);
                }
            }
            catch (Exception ee)
            {
                throw ee;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('System Error - Please Try again....');", true);
            }
            finally
            {
                File.Delete(FilePath);
                upload_gv();

                ddl_bank.SelectedIndex = 0;
                txt_date.Text = "";
            }
        }
    }
    public void btn_Import_Click(string FilePath, string Extension, string IsHDR, string filename)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, IsHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        //   OleDbCommand cmdExcel1 = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        // OleDbDataAdapter oda1 = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        //System.Data.DataTable dt1 = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;
        //cmdExcel1.Connection = connExcel;

        // Get The Name of First Sheet
        connExcel.Open();
        System.Data.DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet

        connExcel.Open();
        cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);

        connExcel.Close();

        //check file format
        int res = 0;
        //Push Datatable to database
        DataTable table2 = new DataTable("Accounting");

        table2.Columns.Add("Date");
        table2.Columns.Add("type");
        table2.Columns.Add("description");
        // table2.Columns.Add("debit");
        table2.Columns.Add("credit");
        // table2.Columns.Add("balance");
        table2.Columns.Add("client_name");
        table2.Columns.Add("Comments");
        // table2.Columns.Add("client_code");
        try
        {
            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    string client_name = "", temp = "";
                    if (ddl_bank.SelectedValue.Equals("INDUSIND BANK"))
                    {
                       
                        string payment_date = r[1].ToString().Trim();
                        if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }

                        if (d.getsinglestring("select client_name from pay_bank_statements where transaction_date = '" + payment_date + "' and balance = '" + r[6].ToString() + "'and description ='" + r[3].ToString() + "'  and credit ='" + r[5].ToString() + "' and type= '" + r[2].ToString() + "' and client_name ='" + r[7].ToString() + "' and bank ='" + ddl_bank.SelectedValue + "'").Equals(""))
                        {
                            if (r[7].ToString() == "" && payment_date != "") { client_name = "Suspence"; } else { client_name = r[7].ToString().ToUpper(); }
                            temp = d.getsinglestring("SELECT DISTINCT (CLIENT_NAME) FROM pay_client_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_name='" + client_name + "'");
                            if ( r[4].ToString().Trim() != "" || r[5].ToString().Trim() != "" || r[6].ToString().Trim() != "")
                            {
                                if (temp != "" || client_name == "Suspence")
                                {

                                    res = d.operation("insert into pay_bank_statements (transaction_date,type, description, debit, credit, balance, client_name,  uploded_date,bank) values ('" + payment_date + "','" + r[2].ToString() + "','" + r[3].ToString() + "','" + r[4].ToString() + "','" + r[5].ToString() + "','" + r[6].ToString() + "','" + client_name + "','" + txt_date.Text + "','" + ddl_bank.SelectedValue + "')");

                                }
                                else
                                {
                                    table2.Rows.Add(r[1].ToString(), r[2].ToString(), r[3].ToString(), r[5].ToString(), r[7].ToString(), "Client name not matching in system");
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Already Uploaded !!!');", true);
                            return;
                        }

                    }
                    else if (ddl_bank.SelectedValue.Equals("SBI BANK"))
                    {
                        
                        string payment_date = r[0].ToString().Trim();
                        if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                        if (d.getsinglestring("select client_name from pay_bank_statements where transaction_date = '" + payment_date + "' and balance ='" + r[7].ToString() + "' and description ='" + r[2].ToString() + "'  and credit ='" + r[6].ToString() + "' and type= '" + r[3].ToString() + "' and balance ='" + r[7].ToString() + "'and client_name ='" + r[8].ToString() + "' and bank ='" + ddl_bank.SelectedValue + "'").Equals(""))
                        {
                            if (r[8].ToString() == "" && payment_date != "") { client_name = "Suspence"; } else { client_name = r[8].ToString().ToUpper(); }
                            temp = d.getsinglestring("SELECT DISTINCT (CLIENT_NAME) FROM pay_client_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_name='" + client_name + "'");
                            if (r[5].ToString().Trim() != "" || r[6].ToString().Trim() != "" || r[7].ToString().Trim() != "")
                            {
                                if (temp != "" || client_name == "Suspence")
                                {

                                    res = d.operation("insert into pay_bank_statements (transaction_date,type, description, debit, credit, balance, client_name,  uploded_date,bank) values ('" + payment_date + "','" + r[3].ToString() + "','" + r[2].ToString() + "','" + r[5].ToString() + "','" + r[6].ToString() + "','" + r[7].ToString() + "','" + client_name + "','" + txt_date.Text + "','" + ddl_bank.SelectedValue + "')");
                                }
                                else
                                {
                                    table2.Rows.Add(r[0].ToString(), r[3].ToString(), r[2].ToString(), r[6].ToString(), r[8].ToString(), "Client name not matching in system");
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Already Uploaded !!!');", true);
                            return;
                        }

                    }
                    else if (ddl_bank.SelectedValue.Equals("Axis BANK"))
                    {

                        string payment_date = r[1].ToString().Trim();
                        if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 10); }
                        if (d.getsinglestring("select client_name from pay_bank_statements where transaction_date = '" + payment_date + "'  and description ='" + r[2].ToString() + "'  and credit ='" + r[5].ToString() + "' and type= '" + r[4].ToString() + "' and balance ='" + r[6].ToString() + "'and client_name ='" + r[8].ToString() + "' and bank ='" + ddl_bank.SelectedValue + "'").Equals(""))
                        {
                            if (r[8].ToString() == "" && payment_date != "") { client_name = "Suspence"; } else { client_name = r[8].ToString().ToUpper(); }
                            temp = d.getsinglestring("SELECT DISTINCT (CLIENT_NAME) FROM pay_client_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and client_name='" + client_name + "'");
                            if (r[5].ToString().Trim() != "" || r[6].ToString().Trim() != "")
                            {
                                if (temp != "" || client_name == "Suspence")
                                {

                                    res = d.operation("insert into pay_bank_statements (transaction_date,type, description, credit, balance, client_name,  uploded_date,bank) values ('" + payment_date + "','" + r[4].ToString() + "','" + r[2].ToString() + "','" + r[5].ToString() + "','" + r[6].ToString() + "','" + client_name + "','" + txt_date.Text + "','" + ddl_bank.SelectedValue + "')");
                                }
                                else
                                {
                                    table2.Rows.Add(r[1].ToString(), r[4].ToString(), r[2].ToString(), r[5].ToString(), r[8].ToString(), "Client name not matching in system");
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Already Uploaded !!!');", true);
                            return;
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (table2.Rows.Count > 0)
            {
                DataSet ds = new DataSet("Accounting");
                ds.Tables.Add(table2);
                send_file(ds);
            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully !!!');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            connExcel.Close();
        }
    }
    private void send_file(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Bank_statement_issues.xls");
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

                    lc = new LiteralControl("<table border=1><tr><th>SR. NO.</th><th> TRANSACTION DATE </th><th> TYPE</th><th>DESCRIPTION</th><th>CREDIT</th><th>CLIENT NAME</th><th>COMMENT</th>");

                    break;
                case ListItemType.Item:

                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["Date"] + " </td><td>" + ds.Tables[0].Rows[ctr]["type"] + " </td><td>" + ds.Tables[0].Rows[ctr]["description"] + " </td><td>" + ds.Tables[0].Rows[ctr]["credit"] + "<td>" + ds.Tables[0].Rows[ctr]["client_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["Comments"] + " </td>");

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

    protected void upload_gv()
    {
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select transaction_date,Type,Credit,Debit,Balance,Client_Name from pay_bank_statements", d.con);
            cmd_item.Fill(ds);
            gv_upload.DataSource = ds.Tables[0];
            gv_upload.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void gv_upload_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_upload.UseAccessibleHeader = false;
            gv_upload.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_upload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }

    // receipt/receipt details approve reject komal 20-06-2020
    protected void ddl_receipt_details_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_minibank_receipt.DataSource = null;
        gv_minibank_receipt.DataBind();

        gv_receipt_details.DataSource = null;
        gv_receipt_details.DataBind();

        //ddl_received_type.SelectedValue = "Select";
        //ddl_bill_client_receipt.SelectedValue = "Select";
        

        hidtab.Value = "2";
        if (ddl_receipt_details.SelectedValue=="1")
        {
        panel_received_type.Visible = true;
     
        }

            // for receipt details client
        else
            if (ddl_receipt_details.SelectedValue == "2")
        {
             d.con.Open();

             panel_receipt_details.Visible = true;

            ddl_bill_client_receipt.Items.Clear();
            MySqlDataAdapter cmd_item = null;
            System.Data.DataTable dt_item = new System.Data.DataTable();


            cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.`client_name`, pay_report_gst.`client_code` FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE pay_report_gst.`comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_de_approve` != '0'  ORDER BY `client_name`", d.con);
           
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_bill_client_receipt.DataSource = dt_item;
                ddl_bill_client_receipt.DataTextField = dt_item.Columns[0].ToString();
                ddl_bill_client_receipt.DataValueField = dt_item.Columns[1].ToString();
                ddl_bill_client_receipt.DataBind();

            }
            ddl_bill_client_receipt.Items.Insert(0, "Select");
            //ddl_bill_client.SelectedValue = "elect";

            dt_item.Dispose();
            d.con.Close();
            
        }
    }

    protected void ddl_received_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try 
        {
            d.con.Open();
            ddl_bill_client_receipt.Items.Clear();
            MySqlDataAdapter cmd_item = null;
            System.Data.DataTable dt_item = new System.Data.DataTable();

            if (ddl_received_type.SelectedValue=="0")
            {
                panel_received_type.Visible = true;
                cmd_item = new MySqlDataAdapter("SELECT DISTINCT `client_name`, `client_code` FROM `pay_minibank_master` WHERE `comp_code` = '"+Session["comp_code"].ToString()+"' AND `receipt_approve` != '0' AND `client_code` != '' ORDER BY `client_name`", d.con);
           
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_bill_client_receipt.DataSource = dt_item;
                ddl_bill_client_receipt.DataTextField = dt_item.Columns[0].ToString();
                ddl_bill_client_receipt.DataValueField = dt_item.Columns[1].ToString();
                ddl_bill_client_receipt.DataBind();

            }
            ddl_bill_client_receipt.Items.Insert(0, "Select");
            //ddl_bill_client.SelectedValue = "elect";

            dt_item.Dispose();
            d.con.Close();

            }
            else
                if (ddl_received_type.SelectedValue == "1")
                {
                    panel_received_type.Visible = true;
                    cmd_item = new MySqlDataAdapter("SELECT DISTINCT `client_name` FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_approve` != '0' AND `client_code` is null ORDER BY `client_name`", d.con);

                    cmd_item.Fill(dt_item);
                    if (dt_item.Rows.Count > 0)
                    {
                        ddl_bill_client_receipt.DataSource = dt_item;
                        ddl_bill_client_receipt.DataTextField = dt_item.Columns[0].ToString();
                        ddl_bill_client_receipt.DataValueField = dt_item.Columns[0].ToString();
                        ddl_bill_client_receipt.DataBind();

                    }
                    ddl_bill_client_receipt.Items.Insert(0, "Select");
                    //ddl_bill_client.SelectedValue = "Select";

                    dt_item.Dispose();
                    d.con.Close();

                }

        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }

    protected void receipt_gv()
    {
        try
        {
           // panel_receipt_details.Visible = true;
            d2.con.Open();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            gv_minibank_receipt.DataSource = null;
            gv_minibank_receipt.DataBind();

            gv_receipt_details.DataSource = null;
            gv_receipt_details.DataBind();

            DataSet ds1 = new DataSet();

            MySqlDataAdapter adp1 = null;

            // for receipt gridview 
            if (ddl_receipt_details.SelectedValue == "1")
            {
                panel_received_type.Visible = true;

                // for client gv
                if (ddl_received_type.SelectedValue == "0")
                {
                    adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_code` = '" + ddl_bill_client_receipt.SelectedValue + "' AND  (`receipt_approve` != '0' && `receipt_approve` != '3') ", d2.con);


                }
                else

                    // for other gv
                    if (ddl_received_type.SelectedValue == "1")
                    {
                        adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_name` = '" + ddl_bill_client_receipt.SelectedItem + "' AND  (`receipt_approve` != '0' && `receipt_approve` != '3') ", d.con1);


                    }

                adp1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gv_minibank_receipt.DataSource = ds1.Tables[0];
                    gv_minibank_receipt.DataBind();
                    d2.con.Close();
                }
            }

            // for receipt details gridview
            //else if (ddl_receipt_details.SelectedValue == "2")
            //{


            //    adp1 = new MySqlDataAdapter("SELECT DISTINCT pay_report_gst.id,receipt_de_approve, case when receipt_de_approve = '0' then 'Pending' when receipt_de_approve ='1' then 'Approve By Jr Acc' when receipt_de_approve ='2' then 'Approve By Sr Acc' end as 'Status',receipt_de_reasons as 'receipt_reasons',pay_client_master.`client_name`,invoice_no,date_format(received_date,'%d-%m-%Y') as 'Receive Date',`received_amt` as 'Received Amount',billing_amt as 'Billing Amount'  FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_report_gst`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_de_approve` != '0' and pay_report_gst.client_code = '" + ddl_bill_client_receipt.SelectedValue + "' ORDER BY `client_name` ", d.con1);


            //    adp1.Fill(ds1);
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        gv_receipt_details.DataSource = ds1.Tables[0];
            //        gv_receipt_details.DataBind();
            //        d.con1.Close();
            //    }

            //    receving_date_check();
            //}
        }
        catch (Exception ex) { throw ex; }
        finally { d2.con.Close(); }


    }

    protected void ddl_bill_client_receipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {
          
            d.con1.Open();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            gv_minibank_receipt.DataSource = null;
            gv_minibank_receipt.DataBind();

            gv_receipt_details.DataSource = null;
            gv_receipt_details.DataBind();

            DataSet ds1 = new DataSet();

            MySqlDataAdapter adp1 = null;

            // for receipt gridview 
            if (ddl_receipt_details.SelectedValue == "1")
            {
                panel_received_type.Visible = true;

                // for client gv
                if (ddl_received_type.SelectedValue == "0")
                {
                    adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_code` = '" + ddl_bill_client_receipt.SelectedValue + "' AND ( `receipt_approve` != '0' && `receipt_approve` != '3' ) ", d.con1);


                }else

                    // for other gv
                if (ddl_received_type.SelectedValue == "1")
                {
                    adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_name` = '" + ddl_bill_client_receipt.SelectedItem + "' AND ( `receipt_approve` != '0' && `receipt_approve` != '3' )", d.con1);


                }

                adp1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gv_minibank_receipt.DataSource = ds1.Tables[0];
                    gv_minibank_receipt.DataBind();
                    d.con1.Close();
                }
            }
            else if (ddl_receipt_details.SelectedValue == "2")
            {

                panel_receipt_details.Visible = true;
            
            }

                // for receipt details gridview
            //else if (ddl_receipt_details.SelectedValue == "2")
            //{


            //    adp1 = new MySqlDataAdapter("SELECT DISTINCT pay_report_gst.id,receipt_de_approve, case when receipt_de_approve = '0' then 'Pending' when receipt_de_approve ='1' then 'Approve By Jr Acc' when receipt_de_approve ='2' then 'Approve By Sr Acc' end as 'Status',receipt_de_reasons as 'receipt_reasons',pay_client_master.`client_name`,invoice_no,date_format(received_date,'%d-%m-%Y') as 'Receive Date',`received_amt` as 'Received Amount',billing_amt as 'Billing Amount'  FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_report_gst`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_de_approve` != '0' and pay_report_gst.client_code = '" + ddl_bill_client_receipt.SelectedValue + "' ORDER BY `client_name` ", d.con1);


            //    adp1.Fill(ds1);
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        gv_receipt_details.DataSource = ds1.Tables[0];
            //        gv_receipt_details.DataBind();
            //        d.con1.Close();
            //    }

            //    receving_date_check();
            //}

            if (ddl_receipt_details.SelectedValue=="2") 
            {
                receving_date_check();
            
            }

        }
        catch (Exception ex) { throw ex; }
        finally {// receving_date_check(); 
        }
    }

    protected void btn_edit_receipt_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        try
        {
            string inlist = "";
            string id1 = "";
            string re_date = "";
            string amount = "";
            string reason_receipt = "";

            if (ddl_receipt_details.SelectedValue == "1")
            {
                panel_received_type.Visible = true;

            }

            else

            if (ddl_receipt_details.SelectedValue == "2")
            {

                panel_receipt_details.Visible = true;

            }



            if (ddl_receipt_details.SelectedValue == "1")
            {

                foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    //string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[2].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        //inlist = "" + id + "";
                        id1 = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;
                        re_date = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[8].Text;
                        amount = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[10].Text;


                    }

                   // string check_receipt_details = d.getsinglestring("select `received_date`,`received_amt`,client_code from payment_history_details where comp_code = '"+Session["comp_code"].ToString()+"' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and received_date = str_to_date('" + re_date + "','%d-%m-%Y') and received_amt = '" + amount + "'");

                    string check_receipt_details = d.getsinglestring("select `received_date`,`received_amt`,client_code from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `receipt_de_approve` !='0' and received_date = str_to_date('" + re_date + "','%d-%m-%Y') ");
                    if (check_receipt_details!="")
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Reject Receipt Details Record... !!!');", true);
                   return;
               
               }

                
                }


                foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    //string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[2].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        //inlist = "" + id + "";
                       id1 = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;

                       TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_recive_amt");
                       reason_receipt = (txt_returnqty.Text);


                       if (reason_receipt=="") 
                       {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reject Reason')", true);
                        
                           return;
                       
                       }

                          }

                    int result = 0;

                    if (reason_receipt != "")
                    {

                        if (ddl_received_type.SelectedValue == "0")
                        {

                            result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '3',receipt_reasons = '" + reason_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + id1 + "' ");
                        }

                        else
                            if (ddl_receipt_details.SelectedValue == "1")
                            {

                                result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '3',receipt_reasons = '" + reason_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_name = '" + ddl_bill_client_receipt.SelectedItem + "' and id = '" + id1 + "' ");
                            }




                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Reject By Sr Acc Successfully !!!')", true);
                    }
                }
            }
            else

            // for receipt details approve
            if (ddl_receipt_details.SelectedValue == "2")
            {
                string inv_no = null; string reason_de_receipt = "";
                foreach (GridViewRow gvrow in gv_receipt_details.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    string id = gv_receipt_details.Rows[gvrow.RowIndex].Cells[3].Text;
                    string invoice_no = gv_receipt_details.Rows[gvrow.RowIndex].Cells[8].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        inlist = "" + id + "";
                        inv_no = "" + invoice_no + "";


                        TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_receive_de_reason");
                        reason_de_receipt = (txt_returnqty.Text);

                        if (reason_de_receipt == "") {

                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reject Reason')", true);
            
             
                            return;
                        
                        }
                    }

                    int result = 0;

                    if (reason_de_receipt != "")
                    {

                        result = d.operation("UPDATE pay_report_gst SET receipt_de_approve = '3',receipt_de_reasons='" + reason_de_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_de_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + inlist + "' and invoice_no = '" + inv_no + "' ");

                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Reject By Sr Acc Successfully !!!')", true);

                }

            }

            edit_approve_filter();
        }
        catch (Exception ex) { throw ex; }
        finally {  }

    }
    protected void btn_approve_receipt_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";

        try
        {
           
            string inlist = "";

            // for receipt approve
            if (ddl_receipt_details.SelectedValue == "1")
            {

            foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    inlist =  "" + id + "";
                }

                int result = 0;

                
                    if (ddl_received_type.SelectedValue == "0")
                    {

                        result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '2' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + inlist + "' ");
                    }

                    else
                        if (ddl_receipt_details.SelectedValue == "1")
                        {

                            result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '2' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_name = '" + ddl_bill_client_receipt.SelectedItem + "' and id = '" + inlist + "' ");
                        }


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Record Approve Successfully !!!')", true);
               
            }
        
         }

                else
                    // for receipt details approve
                    if (ddl_receipt_details.SelectedValue == "2")
                    {

                        string inv_no=null;
                        foreach (GridViewRow gvrow in gv_receipt_details.Rows)
                        {

                            // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                            string id = gv_receipt_details.Rows[gvrow.RowIndex].Cells[3].Text;
                            string invoice_no = gv_receipt_details.Rows[gvrow.RowIndex].Cells[8].Text;

                            var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                            if (checkbox.Checked == true)
                            {

                                inlist = "" + id + "";
                                inv_no = "" + invoice_no + "";
                            }

                            int result = 0;
                            if (inv_no!="")
                            {

                            result = d.operation("UPDATE pay_report_gst SET receipt_de_approve = '2' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_de_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + inlist + "' and invoice_no = '"+ inv_no +"' ");

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Record Approve  Successfully !!!')", true);
                            }

                        }

                       
                    
                    }

            edit_approve_filter(); 
        }
        catch (Exception ex) { throw ex; }
        finally { }
    }
    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {
        hidtab.Value = "2";

        string filename = e.CommandArgument.ToString();


        if (filename != "")
        {
            string path2 = Server.MapPath("~\\Account_images\\" + filename);

            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip/xls/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile("~\\Account_images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }
    }

    protected void receving_date_check() 
    {
        try { 
        
            
          DataTable dt_item = new DataTable();
               
        //  MySqlDataAdapter cmd_item = new MySqlDataAdapter("select DATE_FORMAT(`receive_date`, '%d-%m-%Y') from(SELECT pay_minibank_master.ID,receive_date, ROUND(pay_minibank_master.Amount - (IFNULL(SUM(pay_report_gst.received_amt), 0)), 2) AS 'REMANING_AMOUNT' FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_report_gst.payment_id = pay_minibank_master.id WHERE pay_minibank_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_minibank_master.client_code='" + ddl_bill_client_receipt.SelectedValue + "' AND (`receipt_approve` != '0' && `receipt_approve` != '3')  GROUP BY pay_minibank_master.receive_date) as t1 where REMANING_AMOUNT >0.99 ", d3.con);

          MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct date_format(received_date,'%d-%m-%Y') as received_date  from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and receipt_de_approve = '1' ", d3.con);
            d3.con.Open();

                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_receving_date.DataSource = dt_item;

                    ddl_receving_date.DataValueField = dt_item.Columns[0].ToString();

                    ddl_receving_date.DataBind();
                }
                ddl_receving_date.Items.Insert(0, "Select");
                dt_item.Dispose();
                d3.con.Close();
        
        
        
        
        }
        catch (Exception ex) { throw ex; }
        finally { d3.con.Close(); }
    
    }


    protected void gv_minibank_receipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        Color color = System.Drawing.ColorTranslator.FromHtml("#90EE90");
        Color color1 = System.Drawing.ColorTranslator.FromHtml("#FF4C4C");
        Color color2 = System.Drawing.ColorTranslator.FromHtml("#FACC2E");



        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            if (e.Row.Cells[5].Text.ToUpper().Equals("2"))
            {
                e.Row.BackColor = color;
            }
            //else if (e.Row.Cells[5].Text.ToUpper().Equals("REJECT BY FINANCE"))
            //{
            //    e.Row.BackColor = color1;
            //}
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[3].Text) == "")
            {
                TextBox txtName = (e.Row.FindControl("txt_recive_amt") as TextBox);
                txtName.Focus();

            }
        }




        e.Row.Cells[2].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[12].Visible = false;
    }
    protected void gv_receipt_details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }

        Color color = System.Drawing.ColorTranslator.FromHtml("#90EE90");
        Color color1 = System.Drawing.ColorTranslator.FromHtml("#FF4C4C");
        Color color2 = System.Drawing.ColorTranslator.FromHtml("#FACC2E");


        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
            if (e.Row.Cells[4].Text.ToUpper().Equals("2"))
            {
                e.Row.BackColor = color;
            }
            //else if (e.Row.Cells[5].Text.ToUpper().Equals("REJECT BY FINANCE"))
            //{
            //    e.Row.BackColor = color1;
            //}
        }

        if (d.getsinglestring("select `receipt_de_approve` from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `received_date` =  str_to_date('" + ddl_receving_date.SelectedValue + "','%d/%m/%Y')").Equals("2"))
        {
            e.Row.BackColor = color2;
        }




        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[2].Text) == "")
            {
                TextBox txtName = (e.Row.FindControl("txt_receive_de_reason") as TextBox);
                txtName.Focus();

            }
        }
        
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
      

    }

    // here update reason to click reject 02-07-2020 komal
    protected void reason_update() 
    {

        try {
        
            // for receipt
        if(ddl_receipt_details.SelectedValue=="1")
        {
            string id1 = "";
            string reason_receipt = "";
            foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                //string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[2].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    //inlist = "" + id + "";
                    id1 = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;

                      TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_recive_amt");
                reason_receipt = (txt_returnqty.Text);


                }

                int result = 0;


                if (ddl_received_type.SelectedValue == "0")
                {

                    result = d.operation("UPDATE pay_minibank_master SET receipt_reasons = '" + reason_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + id1 + "' ");
                }

                else
                    if (ddl_receipt_details.SelectedValue == "1")
                    {

                        result = d.operation("UPDATE pay_minibank_master SET receipt_reasons = '"+ reason_receipt+"' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_name = '" + ddl_bill_client_receipt.SelectedItem + "' and id = '" + id1 + "' ");
                    }
            }

        }
        else
            // for receipt details
            if (ddl_receipt_details.SelectedValue=="2") 
            {

                string inv_no = null; string inlist = ""; string reason_de_receipt = "";
                foreach (GridViewRow gvrow in gv_receipt_details.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    string id = gv_receipt_details.Rows[gvrow.RowIndex].Cells[3].Text;
                    string invoice_no = gv_receipt_details.Rows[gvrow.RowIndex].Cells[8].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        inlist = "" + id + "";
                        inv_no = "" + invoice_no + "";

                        TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_receive_de_reason");
                        reason_de_receipt = (txt_returnqty.Text);

                    }

                    int result = 0;

                    result = d.operation("UPDATE pay_report_gst SET receipt_de_approve = '0' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_de_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + inlist + "' and invoice_no = '" + inv_no + "' ");

                }
            
            
            
            }
        
        
        }
        catch (Exception ex) { throw ex; }
        finally{}
    
    }


    protected void ddl_receving_date_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_client_resive_amt.Items.Clear();
        try
        {
            panel_receipt_details.Visible = true;

            DataTable dt_item = new DataTable();
            ddl_client_resive_amt.Items.Clear();
            //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select Id,amount from ( SELECT pay_minibank_master.id AS 'Id',pay_minibank_master.amount   FROM pay_minibank_master LEFT JOIN pay_report_gst ON pay_minibank_master.id = pay_report_gst.payment_id AND pay_minibank_master.CLIENT_CODE = pay_report_gst.CLIENT_CODE WHERE pay_minibank_master.receive_date = str_to_date('" + ddl_receving_date.Text + "', '%d-%m-%Y') AND pay_minibank_master.client_code = '" + ddl_bill_client_receipt.SelectedValue + "' AND `receipt_approve` != '0' GROUP BY pay_minibank_master.id, pay_report_gst.payment_id)  as t1 where  amount > 0 ORDER BY amount  ", d.con);

            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select  distinct `received_original_amount` from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `received_date` = STR_TO_DATE('"+ddl_receving_date.Text+"', '%d-%m-%Y') ", d.con);
            d.con.Open();

            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client_resive_amt.DataSource = dt_item;

                ddl_client_resive_amt.DataValueField = dt_item.Columns[0].ToString();
                ddl_client_resive_amt.DataTextField = dt_item.Columns[0].ToString();
                ddl_client_resive_amt.DataBind();

            }
            ddl_client_resive_amt.Items.Insert(0, "Select");
            //ddl_client_resive_amt.Items.Insert(0, "Select");
            dt_item.Dispose();
            d.con.Close();

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
        }
    }

    protected void receipt_details_gv() 
    {
    
     if (ddl_receipt_details.SelectedValue == "2")
            {
                panel_receipt_details.Visible = true;
                d4.con.Open();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

                gv_minibank_receipt.DataSource = null;
                gv_minibank_receipt.DataBind();

                gv_receipt_details.DataSource = null;
                gv_receipt_details.DataBind();

                DataSet ds1 = new DataSet();

                MySqlDataAdapter adp1 = null;


                adp1 = new MySqlDataAdapter("SELECT DISTINCT pay_report_gst.id,receipt_de_approve, case when receipt_de_approve = '0' then 'Pending' when receipt_de_approve ='1' then 'Approve By Jr Acc' when receipt_de_approve ='2' then 'Approve By Sr Acc' end as 'Status',receipt_de_reasons as 'receipt_reasons',pay_client_master.`client_name`,invoice_no,date_format(received_date,'%d-%m-%Y') as 'Receive Date',`received_amt` as 'Received Amount',billing_amt as 'Billing Amount'  FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_report_gst`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND (`receipt_de_approve` != '0' && `receipt_de_approve` != '3') and pay_report_gst.client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `received_date`= str_to_date('" + ddl_receving_date.SelectedItem + "','%d-%m-%Y') and received_original_amount='" + ddl_client_resive_amt.SelectedValue + "'  ORDER BY `client_name` ", d4.con);


                adp1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gv_receipt_details.DataSource = ds1.Tables[0];
                    gv_receipt_details.DataBind();
                    d4.con.Close();
                }

               // receving_date_check();
            }
    
    }



    protected void edit_approve_filter() 
    {
        if (ddl_receipt_details.SelectedValue=="1")
    {

        receipt_gv();

    }
    else if (ddl_receipt_details.SelectedValue=="2")
    {


        receipt_details_gv();
    
    }
    
    
    }

    protected void ddl_client_resive_amt_SelectedIndexChanged(object sender, EventArgs e)
    {
        receipt_details_gv();
    }
    protected void btn_sr_approved_Click(object sender, EventArgs e)
    {
        
            hidtab.Value = "2";
            try
            {
                btn_edit_receipt.Visible = false;
                btn_reject_sr.Visible = true;

                d.con1.Open();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

                gv_minibank_receipt.DataSource = null;
                gv_minibank_receipt.DataBind();

                gv_receipt_details.DataSource = null;
                gv_receipt_details.DataBind();

                DataSet ds1 = new DataSet();

                MySqlDataAdapter adp1 = null;

                // for receipt gridview 
                if (ddl_receipt_details.SelectedValue == "1")
                {
                    panel_received_type.Visible = true;

                    // for client gv
                    if (ddl_received_type.SelectedValue == "0")
                    {
                        adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_code` = '" + ddl_bill_client_receipt.SelectedValue + "' AND  `receipt_approve` = '2'  ", d.con1);


                    }
                    else

                        // for other gv
                        if (ddl_received_type.SelectedValue == "1")
                        {
                            adp1 = new MySqlDataAdapter("SELECT id as 'id',receipt_approve, case when receipt_approve = '0' then 'Pending' when receipt_approve ='1' then 'Approve By Jr Acc' when receipt_approve ='2' then 'Approve By Sr Acc' end as 'Status', client_name as 'Client Name', date_format(receive_date,'%d-%m-%Y') as 'Receive Date',`description` as 'Payment Description',`Amount` as 'Credit Amount',Upload_file as 'Upload_file',receipt_reasons as 'receipt_reasons' FROM `pay_minibank_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' AND `client_name` = '" + ddl_bill_client_receipt.SelectedItem + "' AND  `receipt_approve` = '2' ", d.con1);


                        }

                    adp1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gv_minibank_receipt.DataSource = ds1.Tables[0];
                        gv_minibank_receipt.DataBind();
                        d.con1.Close();
                    }
                }
                else if (ddl_receipt_details.SelectedValue == "2")
                {
                    panel_receipt_details.Visible = true;


                    gv_minibank_receipt.DataSource = null;
                    gv_minibank_receipt.DataBind();

                    gv_receipt_details.DataSource = null;
                    gv_receipt_details.DataBind();

                    //adp1 = new MySqlDataAdapter("SELECT DISTINCT pay_report_gst.id,receipt_de_approve, case when receipt_de_approve = '0' then 'Pending' when receipt_de_approve ='1' then 'Approve By Jr Acc' when receipt_de_approve ='2' then 'Approve By Sr Acc' end as 'Status',receipt_de_reasons as 'receipt_reasons',pay_client_master.`client_name`,invoice_no,date_format(received_date,'%d-%m-%Y') as 'Receive Date',`received_amt` as 'Received Amount',billing_amt as 'Billing Amount'  FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_report_gst`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_de_approve` = '2' and pay_report_gst.client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `received_date`= str_to_date('" + ddl_receving_date.SelectedItem + "','%d-%m-%Y') and received_original_amount='" + ddl_client_resive_amt.SelectedValue + "'  ORDER BY `client_name` ", d4.con);

                    adp1 = new MySqlDataAdapter("SELECT DISTINCT pay_report_gst.id,receipt_de_approve, case when receipt_de_approve = '0' then 'Pending' when receipt_de_approve ='1' then 'Approve By Jr Acc' when receipt_de_approve ='2' then 'Approve By Sr Acc' end as 'Status',receipt_de_reasons as 'receipt_reasons',pay_client_master.`client_name`,invoice_no,date_format(received_date,'%d-%m-%Y') as 'Receive Date',`received_amt` as 'Received Amount',billing_amt as 'Billing Amount'  FROM `pay_report_gst` INNER JOIN `pay_client_master` ON `pay_report_gst`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_report_gst`.`client_code` = `pay_client_master`.`client_code` WHERE `pay_report_gst`.`comp_code` = '" + Session["comp_code"].ToString() + "' AND `receipt_de_approve` = '2' and pay_report_gst.client_code = '" + ddl_bill_client_receipt.SelectedValue + "'  ORDER BY `client_name` ", d4.con);
                   
                    adp1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gv_receipt_details.DataSource = ds1.Tables[0];
                        gv_receipt_details.DataBind();
                        d.con1.Close();
                    }
                }

            }
            catch (Exception ex) { throw ex; }
            finally { }
        }

    protected void gv_receipt_details_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_receipt_details.UseAccessibleHeader = false;
            gv_receipt_details.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }

    }
	
	protected void btn_reject_sr_Click(object sender, EventArgs e)
    {


        hidtab.Value = "2";
        try
        {
            string inlist = "";
            string id1 = "";
            string re_date = "";
            string amount = "";
            string reason_receipt = "";

            btn_reject_sr.Visible = false;

            if (ddl_receipt_details.SelectedValue == "1")
            {
                panel_received_type.Visible = true;

            }

            else

                if (ddl_receipt_details.SelectedValue == "2")
                {

                    panel_receipt_details.Visible = true;

                }



            if (ddl_receipt_details.SelectedValue == "1")
            {

                foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    //string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[2].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        //inlist = "" + id + "";
                        id1 = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;
                        re_date = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[8].Text;
                        amount = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[10].Text;


                    }

                    // string check_receipt_details = d.getsinglestring("select `received_date`,`received_amt`,client_code from payment_history_details where comp_code = '"+Session["comp_code"].ToString()+"' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and received_date = str_to_date('" + re_date + "','%d-%m-%Y') and received_amt = '" + amount + "'");

                    string check_receipt_details = d.getsinglestring("select `received_date`,`received_amt`,client_code from pay_report_gst where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and `receipt_de_approve` !='0' and received_date = str_to_date('" + re_date + "','%d-%m-%Y') ");
                    if (check_receipt_details != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please First Reject Receipt Details Record... !!!');", true);
                        return;

                    }


                }


                foreach (GridViewRow gvrow in gv_minibank_receipt.Rows)
                {

                    // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                    //string id = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[2].Text;

                    var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked == true)
                    {

                        //inlist = "" + id + "";
                        id1 = gv_minibank_receipt.Rows[gvrow.RowIndex].Cells[4].Text;

                        TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_recive_amt");
                        reason_receipt = (txt_returnqty.Text);


                        if (reason_receipt == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reject Reason')", true);

                            return;

                        }

                    }

                    int result = 0;

                    if (reason_receipt != "")
                    {

                        if (ddl_received_type.SelectedValue == "0")
                        {

                            result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '3',receipt_reasons = '" + reason_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + id1 + "' ");
                        }

                        else
                            if (ddl_receipt_details.SelectedValue == "1")
                            {

                                result = d.operation("UPDATE pay_minibank_master SET receipt_approve = '3',receipt_reasons = '" + reason_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_approve != '0'  AND client_name = '" + ddl_bill_client_receipt.SelectedItem + "' and id = '" + id1 + "' ");
                            }




                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Reject By Sr Acc Successfully !!!')", true);
                    }
                }
            }
            else

                // for receipt details approve
                if (ddl_receipt_details.SelectedValue == "2")
                {
                    string inv_no = null; string reason_de_receipt = "";
                    foreach (GridViewRow gvrow in gv_receipt_details.Rows)
                    {

                        // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                        string id = gv_receipt_details.Rows[gvrow.RowIndex].Cells[3].Text;
                        string invoice_no = gv_receipt_details.Rows[gvrow.RowIndex].Cells[8].Text;

                        var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                        if (checkbox.Checked == true)
                        {

                            inlist = "" + id + "";
                            inv_no = "" + invoice_no + "";


                            TextBox txt_returnqty = (TextBox)gvrow.FindControl("txt_receive_de_reason");
                            reason_de_receipt = (txt_returnqty.Text);

                            if (reason_de_receipt == "")
                            {


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Reject Reason')", true);
                                btn_reject_sr.Visible = true;

                                return;

                            }
                        }

                        int result = 0;

                        if (reason_de_receipt != "")
                        {

                            result = d.operation("UPDATE pay_report_gst SET receipt_de_approve = '3',receipt_de_reasons='" + reason_de_receipt + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' and receipt_de_approve != '0'  AND client_code = '" + ddl_bill_client_receipt.SelectedValue + "' and id = '" + inlist + "' and invoice_no = '" + inv_no + "' ");

                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert(' Reject By Sr Acc Successfully !!!')", true);

                    }

                }

            edit_approve_filter();
        }
        catch (Exception ex) { throw ex; }
        finally { }



    }
}
