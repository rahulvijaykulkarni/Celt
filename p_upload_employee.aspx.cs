using System;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Web;

public partial class Default2 : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
        if (!IsPostBack)
        {
            load_ddl();
        }

    }

    private void load_ddl()
    {
        //load client ddl
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ") and client_active_close='0'", d.con1);
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
            ddlunitclient1.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void ddlunitclient1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_gv_statewise.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct LOCATION FROM pay_employee_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "'  AND LOCATION in(SELECT DISTINCT  (pay_client_state_role_grade.state_name)  FROM  pay_client_state_role_grade  INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code AND pay_employee_master.emp_code = pay_client_state_role_grade.emp_code  WHERE  pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND  pay_client_state_role_grade.client_code='" + ddlunitclient1.SelectedValue + "' AND (REPORTING_TO IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) ORDER BY EMP_CURRENT_STATE", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_gv_statewise.Items.Add(dr_item1[0].ToString());

            }
            dr_item1.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        ddl_gv_statewise.Items.Insert(0, new ListItem("Select"));
    }

    protected void ddl_gv_statewise_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_gv_branchwise.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddlunitclient1.SelectedValue + "' and state_name = '" + ddl_gv_statewise.SelectedValue + "' AND  UNIT_CODE in(SELECT DISTINCT (pay_client_state_role_grade.unit_code) FROM pay_client_state_role_grade INNER JOIN pay_employee_master ON pay_employee_master.comp_code = pay_client_state_role_grade.comp_code WHERE pay_client_state_role_grade.COMP_CODE = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.client_code = '" + ddlunitclient1.SelectedValue + "' AND pay_client_state_role_grade.state_name = '" + ddl_gv_statewise.SelectedValue + "' AND (pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR pay_client_state_role_grade.emp_code = '" + Session["LOGIN_ID"].ToString() + "')) and branch_status = 0  ORDER BY UNIT_CODE", d.con);
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
            ddl_gv_branchwise.Items.Insert(0, new ListItem("Select"));
            dt_item.Dispose();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        upload_emp();
       
    }

    private void upload_emp() {

        try
        {
            
            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);


            string fname = null;
            
            string FilePath = "";
            if (FileUpload1.HasFile)
            {
                try
                {
                    if (fileExt.ToUpper() == ".XLS" || fileExt.ToUpper() == ".XLSX")
                    {
                        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/employee_upload/") + FileName);
                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        if (Extension == ".xls" || Extension == ".xlsx")
                        {

                            fname = ddlunitclient1.SelectedValue + "_" + ddl_gv_statewise.SelectedValue + "_" + ddl_gv_branchwise.SelectedValue + "_" + fileExt;
                            File.Copy(Server.MapPath("~/employee_upload/") + FileName, Server.MapPath("~/employee_upload/") + fname, true);

                            string FolderPath = "~/employee_upload/";
                            FilePath = Server.MapPath(FolderPath + FileName);
                            FileUpload1.SaveAs(FilePath);
                            btn_Import_Click(FilePath, Extension, "Yes", FileName, fname);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Ledger File Uploaded Successfully...');", true);
                           // File.Delete(FilePath);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please upload a valid excel file.');", true);
                        }
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
                }
            }


        }
        catch (Exception ex) { throw ex; }
        finally { }

    
    
    
    }


    public void btn_Import_Click(string FilePath, string Extension, string IsHDR, string filename, string fname)
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

        try {

            // foreach for to check dublicate bank account number
            foreach (DataRow r in dt.Rows)
            {

                if (r[0].ToString().Trim() != "" && r[1].ToString().Trim() != "" && r[2].ToString().Trim() != "" && r[3].ToString().Trim() != "" && r[4].ToString().Trim() != "" && r[5].ToString().Trim() != "")
                {

                    string account_no = d.getsinglestring("SELECT GROUP_CONCAT(`BANK_EMP_AC_CODE`) FROM `pay_employee_master` WHERE `comp_code` = '"+Session["comp_code"].ToString()+"' AND `BANK_EMP_AC_CODE` != ''");

                    string[] account_no1 = account_no.Split(',');

                    foreach (object obj in account_no1)
                    {
                        string aaa = "" + obj + "";

                        if (aaa == r[4].ToString().Trim()) 
                        {
                            string emp_no = d.getsinglestring("select emp_code from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and BANK_EMP_AC_CODE = '" + aaa + "' ");
                            string emp_name = d.getsinglestring("select EMP_NAME from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and BANK_EMP_AC_CODE = '"+aaa+"' ");

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('bank account number already exist for this employee " + emp_no + "-" + emp_name + " in table , Please Change Bank Account No for this employee " + r[0].ToString().Trim() + "  in excel  try another account number .');", true);
                            return;

                        
                        }
                    
                    }
                
                
                }
         
            }

            // foreach for new employee code
         
            foreach (DataRow r in dt.Rows)
            {
                string initcode = "";
                //if (dt.Rows.Count>0)
                //{
                // for new employee code genarated
                d2.con.Open();
                string new_employee_code = "";
                int max_empcode = 0;
                MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d2.con);
                MySqlDataReader drmax = cmdmax.ExecuteReader();
                if (!drmax.HasRows)
                {

                }
                else if (drmax.Read())
                {
                    d1.con.Open();
                    try
                    {
                        MySqlCommand cmdinit = new MySqlCommand("SELECT EMP_SERIES_INIT FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
                        MySqlDataReader drinit = cmdinit.ExecuteReader();
                        if (!drinit.HasRows)
                        {
                        }
                        else if (drinit.Read())
                        {
                            string stinit = drinit.GetValue(0).ToString();
                           
                            if (stinit != "")
                            {
                                initcode = stinit.ToString();
                                string str = drmax.GetValue(0).ToString();
                                if (str == "")
                                {
                                    //txt_eecode.Text = initcode.ToString() + "00001";
                                }
                                else
                                {
                                    max_empcode = int.Parse(drmax.GetValue(0).ToString());



                                    if (max_empcode < 10)
                                    {
                                        new_employee_code = initcode.ToString() + "0000" + max_empcode;
                                    }
                                    else if (max_empcode >= 10 && max_empcode < 100)
                                    {
                                        new_employee_code = initcode.ToString() + "000" + max_empcode;
                                    }
                                    else if (max_empcode >= 100 && max_empcode < 1000)
                                    {
                                        new_employee_code = initcode.ToString() + "00" + max_empcode;
                                    }
                                    else if (max_empcode >= 1000 && max_empcode < 10000)
                                    {
                                        new_employee_code = initcode.ToString() + "0" + max_empcode;
                                    }
                                    else if (max_empcode == 10000)
                                    {
                                        new_employee_code = initcode.ToString() + "" + max_empcode;
                                    }

                                }
                            }
                        }
                        drinit.Close();
                        //  d1.con1.Close();
                        d1.con.Close();

                        string payment_date = r[3].ToString();
                        if (payment_date.Length > 10) { payment_date = payment_date.Substring(0, 9); }


                        int res = 0;

                        if (r[0].ToString().Trim() != "" && r[1].ToString().Trim() != "" && r[2].ToString().Trim() != "" && r[3].ToString().Trim() != "" && r[4].ToString().Trim() != "" && r[5].ToString().Trim() != "")
                        {


                            res = d.operation("insert into pay_employee_master (comp_code,client_code,`client_wise_state`,unit_code,emp_code,EMP_NAME,GRADE_CODE,Employee_type,JOINING_DATE,BANK_EMP_AC_CODE,PF_IFSC_CODE,employee_upload)values('" + Session["comp_code"].ToString() + "','" + ddlunitclient1.SelectedValue + "','" + ddl_gv_statewise.SelectedValue + "','" + ddl_gv_branchwise.SelectedValue + "', '" + new_employee_code + "','" + r[0].ToString() + " ','" + r[1].ToString() + " ','" + r[2].ToString() + " ',str_to_date('" + payment_date + "','%d/%m/%Y'),'" + r[4].ToString() + "' ,'" + r[5].ToString() + "','" + fname + "')");
                            if (res > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Employee Added Successfully... !!!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Employee Added Failed... !!!');", true);
                            }

                        }
                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Employee Added1111 Successfully... !!!');", true); }


                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        d2.con.Close();
                        // d1.con1.Close();
                    }


                }

           // }
            
            }
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    

    }

    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

        string ee_code = d.getsinglestring("SELECT MAX(emp_code) FROM  `pay_employee_master`WHERE comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient1.SelectedValue + "' and EMP_CURRENT_STATE = '" + ddl_gv_statewise.SelectedValue + "' and unit_code = '" + ddl_gv_branchwise.SelectedValue + "'");

        string data = d.getsinglestring(" select employee_upload from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddlunitclient1.SelectedValue + "' and EMP_CURRENT_STATE = '" + ddl_gv_statewise.SelectedValue + "' and unit_code = '" + ddl_gv_branchwise.SelectedValue + "' and emp_code = '" + ee_code + "' and employee_upload is not null  ");
        string filename = data;
      
        if (filename != "")
        {
           downloadfile(filename);
           
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
        }


    }


    protected void downloadfile(string filename)
    {

        //I03526_25.jpg
        //I03118_25
        try
        {
            var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }

            string path2 = Server.MapPath("~\\employee_upload\\" + filename);
            //  string unitName = stamp_copy + "-Attendance" + result;
            Response.Clear();
            Response.ContentType = "Application/xls/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile("~\\employee_upload\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();


        }
        catch (Exception ex) { }
    }

    
}