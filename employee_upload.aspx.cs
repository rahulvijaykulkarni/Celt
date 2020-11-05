using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.OleDb;

public partial class GradeDetails : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "R")
        {
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "U")
        {
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Grade Master", Session["COMP_CODE"].ToString()) == "C")
        {
        }

        if (!IsPostBack)
        {
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string FilePath = "";
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    string FolderPath = "~/Temp_images/";
                    FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    btn_Import_Click(FilePath, Extension, "Yes", FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee File Uploaded Successfully...');", true);
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
                FileUpload1.PostedFile.InputStream.Dispose();
                FileUpload1.Attributes.Clear();
            }
        }
    }
    protected void btn_download_template_Click(object sender, EventArgs e)
    {
        FileInfo fileInfo = new FileInfo(Server.MapPath("~\\excelsheets\\EMPLOYEE_UPLOAD.xlsx"));
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(fileInfo.FullName);
        Response.End();

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
        OleDbDataAdapter oda = new OleDbDataAdapter();
        System.Data.DataTable dt = new System.Data.DataTable();
        cmdExcel.Connection = connExcel;

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

        //Push Datatable to database
        System.Data.DataTable table2 = new System.Data.DataTable("emp");
        table2.Columns.Add("Client Name");
        table2.Columns.Add("Branch City");
        table2.Columns.Add("Employee Name");
        table2.Columns.Add("Father Name");
        table2.Columns.Add("Employee Type");
        table2.Columns.Add("Designation");
        table2.Columns.Add("Date of Birth");
        table2.Columns.Add("Joining Date");
        table2.Columns.Add("Mobile Number");
        table2.Columns.Add("Address");
        table2.Columns.Add("City");
        table2.Columns.Add("State");
        table2.Columns.Add("Aadhar Card / Enrollment No");
        table2.Columns.Add("Bank A/C Number");
        table2.Columns.Add("Bank Account Holder Name");
        table2.Columns.Add("Bank Name");
        table2.Columns.Add("Branch Location Name");
        table2.Columns.Add("IFSC Code");
        table2.Columns.Add("Comments");

        try
        {
            foreach (DataRow r in dt.Rows)
            {
                string comments = "", unit_code = "", client_code = "", emp_name = "";
                int i = 0;
                try
                {
                    if (r[0].ToString().Trim() != "" && !r[0].ToString().ToUpper().Contains("CLIENT"))
                    {
                        client_code = d.getsinglestring("select client_code from pay_client_master where client_name = '" + r[0].ToString() + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                        if (client_code == "")
                        {
                            comments = "Client Not Present.";
                            i = 1;
                        }
                        else
                        {
                            unit_code = d.getsinglestring("select unit_code from pay_unit_master where client_code = '" + client_code + "' and unit_city = '" + r[1].ToString() + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                            if (unit_code == "")
                            {
                                comments = "Branch City Not Present.";
                                i = 1;
                            }
                            else
                            {
                                emp_name = d.getsinglestring("select emp_name from pay_employee_master where unit_code = '" + unit_code + "' and emp_name = '" + r[2].ToString() + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");
                                if (emp_name != "")
                                {
                                    comments = "Employee already Present for this Branch.";
                                    i = 1;
                                }
                                else if (d.getsinglestring("select grade_code from pay_grade_master where grade_desc = '" + r[5].ToString() + "'") == "")
                                {
                                    comments = "Please check the Designation.";
                                    i = 1;
                                }
                            }
                        }

                        if (i == 1)
                        {
                            table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), comments);
                        }
                        else
                        {
                            string emp_code = btnnew_Click();
                            try
                            {
                                d.operation("insert into pay_employee_master (comp_code, client_code, unit_code, emp_name, EMP_FATHER_NAME, Employee_type, grade_code,BIRTH_DATE,JOINING_DATE,EMP_MOBILE_NO,EMP_CURRENT_ADDRESS,LOCATION_CITY,EMP_CURRENT_CITY,LOCATION,EMP_CURRENT_STATE,client_wise_state,P_TAX_NUMBER,original_bank_account_no,BANK_HOLDER_NAME,PF_BANK_NAME,BANK_BRANCH,PF_IFSC_CODE,NFD_CODE,emp_nationality,emp_code,Gender) values ('" + Session["COMP_CODE"].ToString() + "','" + client_code + "','" + unit_code + "','" + r[2].ToString() + "','" + r[3].ToString() + "','" + r[4].ToString() + "',(select grade_code from pay_grade_master where grade_desc = '" + r[5].ToString() + "' limit 1),str_to_date('" + r[6].ToString().Replace(" 12:00:00 AM", "") + "','%d-%m-%Y'),str_to_date('" + r[7].ToString().Replace(" 12:00:00 AM", "") + "','%d-%m-%Y'),'" + r[8].ToString() + "','" + r[9].ToString() + "','" + r[10].ToString() + "','" + r[10].ToString() + "','" + r[11].ToString() + "','" + r[11].ToString() + "','" + r[11].ToString() + "','" + r[12].ToString().Replace("'", "") + "','" + r[13].ToString().Replace("'", "") + "','" + r[14].ToString() + "','" + r[15].ToString() + "','" + r[16].ToString() + "','" + r[17].ToString() + "','N','Indian','" + emp_code + "','M')");
                            }
                            catch (Exception ex)
                            {
                                table2.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), ex.Message.ToString());
                            }
                            finally { }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // comments = ex.Message;
                    throw ex;
                }
            }
            if (table2.Rows.Count > 0)
            {
                DataSet ds = new DataSet("employee");
                ds.Tables.Add(table2);
                send_file(ds);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Uploaded Successfully !!!');", true);

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
            Response.AddHeader("content-disposition", "attachment;filename=Issues_Employee_add.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate2(ListItemType.Header, ds);
            Repeater1.ItemTemplate = new MyTemplate2(ListItemType.Item, ds);
            Repeater1.FooterTemplate = new MyTemplate2(ListItemType.Footer, null);
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
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Matching Records Found.');", true);
        //}
    }
    public class MyTemplate2 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;

        public MyTemplate2(ListItemType type, DataSet ds)
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
                    lc = new LiteralControl("<table border=1><tr><th>Client Name</th><th>Branch City</th><th>Employee Name</th><th>Father Name</th><th>Employee Type</th><th>Designation</th><th>Date of Birth (dd-mm-yyyy)</th><th>Joining Date (dd-mm-yyyy)</th><th>Mobile Number</th><th>Address</th><th>City</th><th>State</th><th>Aadhar Card / Enrollment No</th><th>Bank A/C Number</th><th>Bank Account Holder Name</th><th>Bank Name</th><th>Branch Location Name</th><th>IFSC Code</th><th>Comments</th></tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["Client Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Branch City"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Employee Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Father Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Employee Type"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Designation"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Date of Birth"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Joining Date"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Mobile Number"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Address"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["City"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["State"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Aadhar Card / Enrollment No"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Bank A/C Number"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Bank Account Holder Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Bank Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Branch Location Name"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["IFSC Code"].ToString().ToUpper() + "</td><td>" + ds.Tables[0].Rows[ctr]["Comments"].ToString().ToUpper() + "</td></tr>");
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
    protected string btnnew_Click()
    {
        d1.con.Open();
        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {

            }
            else if (drmax.Read())
            {
                d1.con1.Open();
                try
                {
                    MySqlCommand cmdinit = new MySqlCommand("SELECT EMP_SERIES_INIT FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
                    MySqlDataReader drinit = cmdinit.ExecuteReader();
                    if (!drinit.HasRows)
                    {
                    }
                    else if (drinit.Read())
                    {
                        string stinit = drinit.GetValue(0).ToString();
                        string initcode = "";
                        if (stinit != "")
                        {
                            initcode = stinit.ToString();
                            string str = drmax.GetValue(0).ToString();
                            if (str == "")
                            {
                                return initcode.ToString() + "00001";
                            }
                            else
                            {
                                int max_empcode = int.Parse(drmax.GetValue(0).ToString());
                                if (max_empcode < 10)
                                {
                                    return initcode.ToString() + "0000" + max_empcode;
                                }
                                else if (max_empcode >= 10 && max_empcode < 100)
                                {
                                    return initcode.ToString() + "000" + max_empcode;
                                }
                                else if (max_empcode >= 100 && max_empcode < 1000)
                                {
                                    return initcode.ToString() + "00" + max_empcode;
                                }
                                else if (max_empcode >= 1000 && max_empcode < 10000)
                                {
                                    return initcode.ToString() + "0" + max_empcode;
                                }
                                else if (max_empcode == 10000)
                                {
                                    return initcode.ToString() + "" + max_empcode;
                                }
                            }
                        }
                    }
                    drinit.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con1.Close();
                    drmax.Close();
                }
            }
            return "";
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

    }
}
