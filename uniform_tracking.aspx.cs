using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Data;

public partial class uniform_tracking : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    DAL d2 = new DAL();
    DAL d4 = new DAL();
    DAL d5 = new DAL();
    DAL d6 = new DAL();
    public string appro_emp_count = "0", appro_emp_legal = "0", reject_emp_legal = "0";
    //  public string rem_emp_count = "0";//vikas add 
    ReportDocument crystalReport = new ReportDocument();
    string curr_date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_generate_id_card.Visible = false;
            btn_id_resend.Visible = false;
            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            appro_emp_count = ViewState["appro_emp_count"].ToString();
            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
            reject_emp_legal = ViewState["reject_emp_legal"].ToString();



            d1.con1.Open();
            MySqlCommand cmd = new MySqlCommand("select concat(UNIT_CODE,'-',UNIT_NAME) from pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' ORDER BY UNIT_CODE", d1.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlunitselect.Items.Add(dr[0].ToString());//ddl_banklist0.Items.Add(dr_banks[0].ToString());
            }
            ddlunitselect.Items.Insert(0, "ALL");
            ddlunitselect.SelectedIndex = 0;
            d1.con1.Close();
            Session["ReportMonthNo"] = "";

            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client.DataSource = dt_item;
                    ddl_client.DataTextField = dt_item.Columns[0].ToString();
                    ddl_client.DataValueField = dt_item.Columns[1].ToString();
                    ddl_client.DataBind();
                }
                dt_item.Dispose();
                hide_controls();
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
                ddl_client.Items.Insert(1, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

            //Designation

            ddl_designation.Items.Clear();
            System.Data.DataTable dt_item_grade = new System.Data.DataTable();
            MySqlDataAdapter cmd_item_grade = new MySqlDataAdapter("SELECT GRADE_DESC,GRADE_CODE FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            d.con.Open();
            try
            {
                cmd_item_grade.Fill(dt_item_grade);
                if (dt_item_grade.Rows.Count > 0)
                {
                    ddl_designation.DataSource = dt_item_grade;
                    ddl_designation.DataTextField = dt_item_grade.Columns[0].ToString();
                    ddl_designation.DataValueField = dt_item_grade.Columns[1].ToString();
                    ddl_designation.DataBind();
                }
                dt_item_grade.Dispose();
                d.con.Close();
                ddl_designation.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            //ddl_act_SelectedIndexChanged(null, null);


        }
        btn_generate_id_card.Visible = false;
        appro_emp_count = ViewState["appro_emp_count"].ToString();
        appro_emp_legal = ViewState["appro_emp_legal"].ToString();
        reject_emp_legal = ViewState["reject_emp_legal"].ToString();
    }


    private void hide_controls()
    {
        unit_panel.Visible = false;
        // btn_Export.Visible = false;
        //  btn_process.Visible = false;
        //btn_save.Visible = false;
        //FileUpload1.Visible = false;
        //btn_upload.Visible = false;
    }

    private void show_controls()
    {
        unit_panel.Visible = true;
        //btn_Export.Visible = true;
        //  btn_process.Visible = true;
        //btn_save.Visible = true;
        //FileUpload1.Visible = true;
        //btn_upload.Visible = true;
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_checklist_uniform.DataSource = null;
        gv_checklist_uniform.DataBind();



        if (ddl_client.SelectedValue != "Select")
        {
            ddlunitselect.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            string where = "";
            if (!ddl_client.SelectedValue.Equals("ALL"))
            {
                where = "and client_code = '" + ddl_client.SelectedValue + "'";
            }
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' "+where+"  AND pay_unit_master.branch_status = 0  ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddlunitselect.DataSource = dt_item;
                    ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                    ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                    ddlunitselect.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddlunitselect.Items.Insert(0, "ALL");
                ddlunitselect.SelectedIndex = 0;
                show_controls();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                employee_status("1");
                checklist_gridview("1");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }


            //State
            ddl_state.Items.Clear();
            dt_item = new System.Data.DataTable();
            
            cmd_item = new MySqlDataAdapter("Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' "+where+" ORDER BY state_name", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_state.DataSource = dt_item;
                    ddl_state.DataTextField = dt_item.Columns[0].ToString();
                    ddl_state.DataValueField = dt_item.Columns[0].ToString();
                    ddl_state.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_state.Items.Insert(0, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                //ddl_state_SelectedIndexChanged(null, null);
            }
        }
        else
        {
            ddlunitselect.Items.Clear();
            ddl_state.Items.Clear();
            hide_controls();
        }

    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_checklist_uniform.DataSource = null;
        gv_checklist_uniform.DataBind();

        ddlunitselect.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        string where = "";
        if (!ddl_client.SelectedValue.Equals("ALL"))
       {
            where = "and client_code = '" + ddl_client.SelectedValue + "'";
       }
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + where + " and state_name='" + ddl_state.SelectedValue + "' AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddlunitselect.DataSource = dt_item;
                ddlunitselect.DataTextField = dt_item.Columns[0].ToString();
                ddlunitselect.DataValueField = dt_item.Columns[1].ToString();
                ddlunitselect.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddlunitselect.Items.Insert(0, "ALL");
            //show_controls();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (ddl_state.SelectedValue == "ALL")
            {
                employee_status("1");
                checklist_gridview("1");
            }else{
                employee_status("2");
                checklist_gridview("2");
            }
            
            
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }


    }
    protected void ddlunitselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gv_checklist_uniform.DataSource = null;
        //gv_checklist_uniform.DataBind();

        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }

        catch { }


        if (ddlunitselect.SelectedValue == "ALL")
        {
            ddl_designation.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT (designation), grade_code FROM pay_designation_count INNER JOIN pay_grade_master ON pay_designation_count.designation = pay_grade_master.grade_desc AND pay_designation_count.comp_code = pay_grade_master.comp_code WHERE pay_designation_count.comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state = '" + ddl_state.SelectedValue + "' ORDER BY designation", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_designation.DataSource = dt_item;
                    ddl_designation.DataTextField = dt_item.Columns[0].ToString();
                    ddl_designation.DataValueField = dt_item.Columns[1].ToString();
                    ddl_designation.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_designation.Items.Insert(0, "ALL");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                employee_status("2");
                checklist_gridview("3");
                docgv_fill();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_designation.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT (designation), grade_code FROM pay_designation_count INNER JOIN pay_grade_master ON pay_designation_count.designation = pay_grade_master.grade_desc AND pay_designation_count.comp_code = pay_grade_master.comp_code WHERE pay_designation_count.comp_code = '" + Session["comp_code"] + "' and unit_code = '" + ddlunitselect.SelectedValue + "' ORDER BY designation", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_designation.DataSource = dt_item;
                    ddl_designation.DataTextField = dt_item.Columns[0].ToString();
                    ddl_designation.DataValueField = dt_item.Columns[1].ToString();
                    ddl_designation.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_designation.Items.Insert(0, "ALL");
                employee_status("3");
                docgv_fill();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }


    }
    protected void btn_generate_id_card_Click(object sender, EventArgs e)
    {
        if (d.getsinglestring("select id_card_dispatch from pay_employee_master where id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')").Equals(txt_lot.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lot Number already Used !!')", true);
            txt_lot.Focus();
            return;
        }
        string zip_file = "c:/id_card/id_cards.zip";
        string folder_name = "c:/id_card/id_sub_idcard";
        if (File.Exists(zip_file))
        {
            File.Delete(zip_file);
        }
        if (!Directory.Exists("c:/id_card"))
        {
            Directory.CreateDirectory("c:/id_card");
            Directory.CreateDirectory(folder_name);
        }
        else
        {
            Directory.Delete("c:/id_card", recursive: true);
            Directory.CreateDirectory("c:/id_card");
            Directory.CreateDirectory(folder_name);
        }
        generate_excel(folder_name);
        //ZipFile.CreateFromDirectory(folder_name, zip_file);
        //Download_File(zip_file);
        //Directory.Delete("c:/id_card",recursive:true);

    }

    protected void btn_uniform_Click(object sender, EventArgs e)
    {

        // generate_excel_uniform();
        get_excel();

    }

    private void generate_excel(string folder_name)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        try
        {
            //add some text 
            ws.Cells[1, 1] = "EMPLOYEE NAME";
            ws.Cells[1, 2] = "ID";
            ws.Cells[1, 3] = "DEG";
            ws.Cells[1, 4] = "DOJ";
            ws.Cells[1, 5] = "CLIENT NAME";
            ws.Cells[1, 6] = "EMP PHOTO";

            // MySqlCommand cmd = new MySqlCommand("SELECT pay_employee_master.emp_name, id_as_per_dob, pay_grade_master.grade_desc, if(emp_blood_group='Select','',emp_blood_group) as emp_blood_group, DATE_FORMAT(joining_date, '%d/%m/%Y') AS 'DOJ', client_name, emp_photo FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null", d.con1);
            MySqlCommand cmd = new MySqlCommand("SELECT pay_employee_master.emp_name, id_as_per_dob, pay_grade_master.grade_desc, upper(DATE_FORMAT(joining_date, '%d-%b-%Y')) AS 'DOJ', upper(concat(pay_client_master.client_code,'-',pay_unit_master.unit_city)), original_photo FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null and pay_employee_master.legal_flag=2 and pay_employee_master.id_card_dispatch_date is null", d.con1);

            d.con1.Open();
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = 2;
                while (dr.Read())
                {
                    if (!dr.GetValue(5).ToString().Equals(""))
                    {
                        string path = Server.MapPath("~/EMP_Images");
                        try
                        {
                            File.Copy(path + "\\" + dr.GetValue(5).ToString(), folder_name + "/" + (i - 1) + ".jpg");
                            ws.Cells[i, 1] = dr.GetValue(0).ToString();
                            ws.Cells[i, 2] = "ID : " + dr.GetValue(1).ToString();
                            ws.Cells[i, 3] = dr.GetValue(2).ToString();
                            ws.Cells[i, 4] = "DOJ : " + dr.GetValue(3).ToString();
                            ws.Cells[i, 5] = dr.GetValue(4).ToString();
                            ws.Cells[i, 6] = (i - 1);
                            ++i;
                            d.operation("update pay_employee_master set id_card_dispatch_date=now(), id_card_dispatch=" + txt_lot.Text + " WHERE id_as_per_dob = '" + dr.GetValue(1).ToString() + "'");
                        }
                        catch { }
                        //Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 7];
                        //float Left = (float)((double)oRange.Left);
                        //float Top = (float)((double)oRange.Top);
                        //const float ImageSize = 100;
                        //string path = Server.MapPath("~/EMP_Images");
                        //try
                        //{
                        //    ws.Shapes.AddPicture(path + "\\" + dr.GetValue(6).ToString(), Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                        //}
                        //catch { }
                    }
                }
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            //xla.Visible = true;

            wb.SaveAs(folder_name.Replace("/", "\\") + "\\Sheet1", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close();
            xla.Quit();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('COMPLETED, Please check folder " + folder_name + " !!')", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ReleaseComObject(ws);
            ReleaseComObject(wb);
            ReleaseComObject(xla);
        }

    }

    // Excel for uniform/shoes


    private void generate_excel_uniform()
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        try
        {
            //add some text 
            ws.Cells[1, 1] = "EMPLOYEE CODE";
            ws.Cells[1, 2] = "EMPLOYEE NAME";
            ws.Cells[1, 3] = "STATUS";
            ws.Cells[1, 4] = "DOCUMENT TYPE";
            ws.Cells[1, 5] = "SIZE";
            ws.Cells[1, 6] = "NO OF SET";
            ws.Cells[1, 7] = "EMP MOBILE NUMBER";
            ws.Cells[1, 8] = "BRANCH CITY";
            ws.Cells[1, 9] = "STATE NAME";
            ws.Cells[1, 10] = "BRANCH ADDRESS 1";
            ws.Cells[1, 11] = "BRANCH ADDRESS 2";
            ws.Cells[1, 12] = "CLIENT CODE";
            ws.Cells[1, 13] = "GRADE CODE";
            ws.Cells[1, 14] = "GENDER";
            ws.Cells[1, 15] = "PINCODE";
            ws.Cells[1, 16] = "POD";
            ws.Cells[1, 17] = "POD DATE";
            ws.Cells[1, 18] = "COURIER NAME";


            // MySqlCommand cmd = new MySqlCommand("SELECT pay_employee_master.emp_name, id_as_per_dob, pay_grade_master.grade_desc, if(emp_blood_group='Select','',emp_blood_group) as emp_blood_group, DATE_FORMAT(joining_date, '%d/%m/%Y') AS 'DOJ', client_name, emp_photo FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null", d.con1);
            MySqlCommand cmd = new MySqlCommand("select distinct pay_employee_master.emp_code, emp_name, if((left_date != ''),'LEFT','CONTINUE') as Status, document_type, no_of_set, size, emp_mobile_no, unit_city, state_name, unit_add1,unit_add2, pay_employee_master.client_code, pay_employee_master.grade_code, gender,pincode,'POD','POD DATE','COURIER NAME' from  pay_employee_master left join pay_document_details on pay_document_details.emp_code = pay_employee_master.emp_code and document_type != 'ID_Card' inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.COMP_code = pay_employee_master.COMP_code  where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')  AND ID_CARD_DISPATCH = '1'    AND legal_flag = '2'    AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND ap_status = 'Approve By Leagal' group by emp_code", d.con1);

            d.con1.Open();
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                int i = 2;
                while (dr.Read())
                {

                    //string path = Server.MapPath("~/EMP_Images");
                    try
                    {
                        //File.Copy(path + "\\" + dr.GetValue(5).ToString(), folder_name + "/" + (i - 1) + ".jpg");
                        ws.Cells[i, 1] = dr.GetValue(0).ToString();
                        ws.Cells[i, 2] = dr.GetValue(1).ToString();
                        ws.Cells[i, 3] = dr.GetValue(2).ToString();
                        ws.Cells[i, 4] = dr.GetValue(3).ToString();
                        ws.Cells[i, 5] = dr.GetValue(4).ToString();
                        ws.Cells[i, 6] = dr.GetValue(5).ToString();
                        ws.Cells[i, 7] = dr.GetValue(6).ToString();
                        ws.Cells[i, 8] = dr.GetValue(7).ToString();
                        ws.Cells[i, 9] = dr.GetValue(8).ToString();
                        ws.Cells[i, 10] = dr.GetValue(9).ToString();
                        ws.Cells[i, 11] = dr.GetValue(10).ToString();
                        ws.Cells[i, 12] = dr.GetValue(11).ToString();
                        ws.Cells[i, 13] = dr.GetValue(12).ToString();
                        ws.Cells[i, 14] = dr.GetValue(13).ToString();
                        ws.Cells[i, 15] = dr.GetValue(14).ToString();
                        ws.Cells[i, 16] = dr.GetValue(15).ToString();
                        ws.Cells[i, 17] = dr.GetValue(16).ToString();
                        ws.Cells[i, 18] = dr.GetValue(17).ToString();
                        ws.Cells[i, 19] = (i - 1);
                        ++i;

                    }
                    catch { }


                }
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            xla.Visible = true;

            //wb.SaveAs(folder_name.Replace("/", "\\") + "\\Sheet1", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wb.Close();
            //xla.Quit();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('COMPLETED, Please check folder " + folder_name + " !!')", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ReleaseComObject(ws);
            ReleaseComObject(wb);
            ReleaseComObject(xla);
        }

    }
    // uniform excel 20/07/2019
    protected void get_excel()
    {
        string sql = null;
        string where2 = null;
        if (ddl_state.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL")
        {
            where2 = " pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_unit_master.client_code = '" + ddl_client.SelectedValue + "'  and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')  AND pay_unit_master.state_name IN (SELECT DISTINCT (state_name) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY state_name) AND pay_unit_master.unit_code IN (SELECT unit_code FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE)  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "')  AND legal_flag = '2'";

        }
        else
            if (ddl_state.SelectedValue != "ALL" && ddlunitselect.SelectedValue != "ALL" && ddl_designation.SelectedValue != "ALL")
            {
                where2 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and EMP_CURRENT_STATE= '" + ddl_state.SelectedValue + "' and grade_code = '" + ddl_designation.SelectedValue + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y') and legal_flag = '2'";
            }
            else
                if (ddl_state.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL")
                {
                    where2 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_state.SelectedValue + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y') AND pay_unit_master.unit_code IN (SELECT unit_code FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE)  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = '2') ";
                }


        sql = "select distinct pay_employee_master.emp_code, emp_name, if((left_date != ''),'LEFT','CONTINUE') as Status, document_type, no_of_set, size, emp_mobile_no, unit_city, state_name, unit_add1,unit_add2, pay_employee_master.client_code, pay_employee_master.grade_code, gender,pincode,'POD','POD DATE','COURIER NAME' from  pay_employee_master left join pay_document_details on pay_document_details.emp_code = pay_employee_master.emp_code and document_type != 'ID_Card' inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.COMP_code = pay_employee_master.COMP_code  where " + where2 + "";
        MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
        DataSet ds = new DataSet();
        dscmd.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=uniform_Shoes.xls");


            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Repeater Repeater1 = new Repeater();
            Repeater1.DataSource = ds;
            Repeater1.HeaderTemplate = new MyTemplate3(ListItemType.Header, ds);
            Repeater1.ItemTemplate = new MyTemplate3(ListItemType.Item, ds);
            Repeater1.FooterTemplate = new MyTemplate3(ListItemType.Footer, null);
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
    }
    public class MyTemplate3 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;

        static int ctr;
        public MyTemplate3(ListItemType type, DataSet ds)
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
                    lc = new LiteralControl("<table border=1><tr><th>Sr. No.</th><th>EMPLOYEE CODE</th><th>EMPLOYEE NAME</th><th>STATUS</th><th>DOCUMENT</th><th>NO OF SET</th><th>Size</th><th>EMP MOBILE NUMBER</th><th>BRANCH CITY</th><th>STATE NAME</th><th>BRANCH NAME</th><th>BRANCH ADDRESS 2</th><th>CLIENT CODE</th><th>DESIGNATION</th><th>GENDER</th><th>PINCODE</th><th >POD</th><th >POD DATE</th><th>COURIER NAME</th></tr>");
                    break;
                case ListItemType.Item:
                    string color = "";



                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_name"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["Status"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["document_type"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["no_of_set"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["size"].ToString().ToUpper() + " </td><td>" + ds.Tables[0].Rows[ctr]["emp_mobile_no"] + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_city"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["state_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["unit_add1"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["unit_add2"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["client_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gender"] + " </td><td>" + ds.Tables[0].Rows[ctr]["pincode"] + " </td><td>" + ds.Tables[0].Rows[ctr]["POD"] + "</td><td>" + ds.Tables[0].Rows[ctr]["POD DATE"] + " </td><td>" + ds.Tables[0].Rows[ctr]["COURIER NAME"] + " </td></tr>");


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

    private void ReleaseComObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
        }
    }


    protected void multiple_id_genrated() 
    {


        string comp_logo = "";
        string comp_stamppath = "";
        // string rightfooterpath = null;
        // string stamppath = null;
        d.con1.Open();

        try
        {
            string unit = ddl_state.SelectedValue.ToString();
            string brancn = ddlunitselect.SelectedValue.ToString();
            string grae = ddl_designation.SelectedValue.ToString();
            string downloadname = " Dublicate_I_Card";
            string query = "";
            crystalReport.Load(Server.MapPath("~/I_Card_client.rpt"));



            // //for state
            string inlist = "";
            //foreach (GridViewRow gvrow in gv_checklist_uniform.Rows)
            //{
            //    string emp_code = gv_checklist_uniform.Rows[gvrow.RowIndex].Cells[5].Text; 
            //}
            foreach (GridViewRow gvrow in gv_dublicate_id.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string emp_code = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[5].Text;

                var checkbox = gvrow.FindControl("chk_client_dublicate") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    inlist = inlist + "'" + emp_code + "',";
                }

            }

            if (inlist.Length > 0)
            {
                inlist = inlist.Substring(0, inlist.Length - 1);
            }
            else { inlist = "''"; }

            string where = id_uniform_shoes();

            string employee_type = d.getsinglestring("select employee_type from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.emp_code in ( " + inlist + " ) ");

            if (employee_type == "Permanent")
            {
                where = where + " and pay_employee_master.emp_code in ( " + inlist + " ) and pay_employee_master.legal_flag = '2' ";

            }
            else
            {
                where = where + " and pay_employee_master.emp_code in ( " + inlist + " )  ";
            }

            string filepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "EMP_Images\\");
            filepath = filepath.Replace("\\", "\\\\");
            if (Session["COMP_CODE"].ToString() == "C02")
            {
                comp_logo = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_1.png");
                comp_stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
            }
            else
            {
                comp_logo = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_1.png");
                comp_stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
            }
            // headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
            comp_logo = comp_logo.Replace("\\", "\\\\");
            comp_stamppath = comp_stamppath.Replace("\\", "\\\\");

            query = "SELECT pay_employee_master.EMP_CODE as 'celtcode', pay_employee_master.EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, GRADE_DESC, EMP_MOBILE_NO, LOCATION AS 'ADDRESS2', EMP_CURRENT_ADDRESS AS 'ADDRESS1', EMP_CURRENT_CITY AS 'EMP_CITY', concat('DOJ : ',upper(date_format(joining_date,'%d-%M-%Y'))) as 'joining_date',concat('ID : ',id_as_per_dob) as 'EMP_CODE',UPPER(CONCAT(case when pay_client_master.client_code = 'BAGICTM' then 'BAJAJ ALLIANZ GIC LTD.' when `pay_client_master`.client_code = '4' then 'BFL' else pay_client_master.client_name END, ' - ', pay_unit_master.unit_city)) AS 'city', EMP_CURRENT_STATE AS 'STATE', concat('" + filepath + "', pay_images_master.original_photo) AS 'PF_SHEET', pay_images_master.emp_signature, department_type AS 'DEPT_NAME', (SELECT CASE WHEN EMP_BLOOD_GROUP IS NOT NULL THEN EMP_BLOOD_GROUP ELSE '' END) AS 'GENDER', (SELECT client_name FROM pay_client_master WHERE client_code = pay_employee_master.client_code) AS 'BANK_CODE','" + comp_logo + "' as COMPANY_NAME,'" + comp_stamppath + "' as COMPANY_PAN_NO FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE " + where + " and pay_employee_master.id_card_dispatch='1'";
            //string query123 = " SELECT pay_employee_master.EMP_CODE as 'celtcode', pay_employee_master.EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, GRADE_DESC, EMP_MOBILE_NO, LOCATION AS 'ADDRESS2', EMP_CURRENT_ADDRESS AS 'ADDRESS1', EMP_CURRENT_CITY AS 'EMP_CITY', date_format(joining_date,'%d-%M-%Y') as 'joining_date',ihmscode as 'EMP_CODE',concat(pay_client_master.client_code,' - ',pay_unit_master.unit_city) as 'city', EMP_CURRENT_STATE AS 'STATE', concat('" + filepath + "', pay_images_master.original_photo) AS 'PF_SHEET', pay_images_master.emp_signature, department_type AS 'DEPT_NAME', (SELECT CASE WHEN EMP_BLOOD_GROUP IS NOT NULL THEN EMP_BLOOD_GROUP ELSE '' END) AS 'GENDER', (SELECT client_name FROM pay_client_master WHERE client_code = pay_employee_master.client_code) AS 'BANK_CODE' FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null and pay_employee_master.legal_flag=2 and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag=1 ) and pay_unit_master.state_name='" + ddl_state.SelectedValue.ToString() + "' and pay_unit_master.unit_code='" + ddlunitselect.SelectedValue.ToString() + "' and pay_employee_master.grade_code='" + ddl_designation.SelectedValue.ToString() + "'";

            //update flags and dispatch date

            ReportLoad_du(query, downloadname, where);
            // Response.End();
            d.con1.Close();


        }
        catch (Exception ee2)
        {
            throw ee2;
        }
        finally
        {
            id_card_date_update(); 
            d.con.Close();



        }
    
    
    }


    protected void btn_clientwise_all_employee_id_Click(object sender, EventArgs e)
    {
       // HiddenField1.Value = "1";

        string comp_logo = "";
        string comp_stamppath = "";
        // string rightfooterpath = null;
        // string stamppath = null;
        d.con1.Open();
        try
        {
         
            string unit = ddl_state.SelectedValue.ToString();
            string brancn = ddlunitselect.SelectedValue.ToString();
            string grae = ddl_designation.SelectedValue.ToString();
            string downloadname = "I_Card";
            string query = "";
            crystalReport.Load(Server.MapPath("~/I_Card_client.rpt"));



            ////for state start 28-05-2020
            string inlist = "";

            foreach (GridViewRow gvrow in gv_checklist_uniform.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string emp_code = gv_checklist_uniform.Rows[gvrow.RowIndex].Cells[5].Text;

                var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    inlist = inlist + "'" + emp_code + "',";
                }

            }

            if (inlist.Length > 0)
            {
                inlist = inlist.Substring(0, inlist.Length - 1);
            }
            else { inlist = "''"; }


            // end 28-05-2020

           // string id_check = null;
            //string id_name = null;
            //foreach (object obj in inlist)
            //{
            //id_check = d.getsinglestring("select emp_name from pay_document_details  INNER JOIN `pay_employee_master` ON `pay_document_details`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + ddl_client.SelectedValue + "' and document_type = 'ID_Card' and pay_document_details.emp_code in ( " + inlist + " )");

            //if (id_check == "")
            //{
            //    string emp_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code in ( " + inlist + " ) ");

            //    id_name = id_name + "'" + emp_name + "',";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please give id card by Admin for that employee" + id_name + "');", true);
            //    //ddl_state_name.SelectedValue = "Select";
            //    return;


            //    //  }


            //}
           
            string where = id_uniform_shoes();
           // where = where + " and pay_employee_master.emp_code in ( " + inlist + " )";

            string employee_type = d.getsinglestring("select employee_type from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.emp_code in ( " + inlist + " ) ");

            if (employee_type == "Permanent")
            {
                where = where + " and pay_employee_master.emp_code in ( " + inlist + " ) and pay_employee_master.legal_flag = '2' ";
            
            }
            else{
            where = where + " and pay_employee_master.emp_code in ( " + inlist + " )  ";
            }

            string filepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "EMP_Images\\");
            filepath = filepath.Replace("\\", "\\\\");
            if (Session["COMP_CODE"].ToString() == "C02")
            {
                comp_logo = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_1.png");
                comp_stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C02_stamp.png");
            }
            else
            {
                comp_logo = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_1.png");
                comp_stamppath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\C01_stamp.jpg");
            }
            // headerpath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
            comp_logo = comp_logo.Replace("\\", "\\\\");
            comp_stamppath = comp_stamppath.Replace("\\", "\\\\");


            query = "SELECT pay_employee_master.EMP_CODE as 'celtcode', pay_employee_master.EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, GRADE_DESC, EMP_MOBILE_NO, LOCATION AS 'ADDRESS2', EMP_CURRENT_ADDRESS AS 'ADDRESS1', EMP_CURRENT_CITY AS 'EMP_CITY', concat('DOJ : ',upper(date_format(joining_date,'%d-%M-%Y'))) as 'joining_date',concat('ID : ',id_as_per_dob) as 'EMP_CODE',UPPER(CONCAT(case when pay_client_master.client_code = 'BAGICTM' then 'BAJAJ ALLIANZ GIC LTD.' when `pay_client_master`.client_code = '4' then 'BFL' else pay_client_master.client_name END, ' - ', pay_unit_master.unit_city)) AS 'city', EMP_CURRENT_STATE AS 'STATE', concat('" + filepath + "', pay_images_master.original_photo) AS 'PF_SHEET', pay_images_master.emp_signature, department_type AS 'DEPT_NAME', (SELECT CASE WHEN EMP_BLOOD_GROUP IS NOT NULL THEN EMP_BLOOD_GROUP ELSE '' END) AS 'GENDER', (SELECT client_name FROM pay_client_master WHERE client_code = pay_employee_master.client_code) AS 'BANK_CODE','" + comp_logo + "' as COMPANY_NAME,'" + comp_stamppath + "' as COMPANY_PAN_NO FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE  " + where + " and pay_employee_master.id_card_dispatch='0' ";
            
            
            //string query123 = " SELECT pay_employee_master.EMP_CODE as 'celtcode', pay_employee_master.EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, GRADE_DESC, EMP_MOBILE_NO, LOCATION AS 'ADDRESS2', EMP_CURRENT_ADDRESS AS 'ADDRESS1', EMP_CURRENT_CITY AS 'EMP_CITY', date_format(joining_date,'%d-%M-%Y') as 'joining_date',ihmscode as 'EMP_CODE',concat(pay_client_master.client_code,' - ',pay_unit_master.unit_city) as 'city', EMP_CURRENT_STATE AS 'STATE', concat('" + filepath + "', pay_images_master.original_photo) AS 'PF_SHEET', pay_images_master.emp_signature, department_type AS 'DEPT_NAME', (SELECT CASE WHEN EMP_BLOOD_GROUP IS NOT NULL THEN EMP_BLOOD_GROUP ELSE '' END) AS 'GENDER', (SELECT client_name FROM pay_client_master WHERE client_code = pay_employee_master.client_code) AS 'BANK_CODE' FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null and pay_employee_master.legal_flag=2 and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag=1 ) and pay_unit_master.state_name='" + ddl_state.SelectedValue.ToString() + "' and pay_unit_master.unit_code='" + ddlunitselect.SelectedValue.ToString() + "' and pay_employee_master.grade_code='" + ddl_designation.SelectedValue.ToString() + "'";

            //update flags and dispatch date




            ReportLoad(query, downloadname, where);
            // Response.End();
            d.con1.Close();


        }
        catch (Exception ee2)
        {
            throw ee2;
        }
        finally
        {
            
                update_que();

           
            d.con.Close();



        }
        if (ddl_state.SelectedValue != "ALL")
        {
            employee_status("2");
        }
        else { employee_status("1"); }

        // Response.Redirect("uniform_tracking.aspx");
    }
    protected void update_que()
    {
        string client_code = "";
        string client_code2 = "";
        if (ddl_client.SelectedValue !="ALL")
        {
            client_code = " AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' ";
            client_code2 = " and client_code = '" + ddl_client.SelectedValue + "'";
        }
        string update_where = "";
        update_where = " pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' " + client_code + "  AND (`pay_employee_master`.`employee_type` = 'Permanent' || `pay_employee_master`.`employee_type` = 'Staff') and left_date is null  and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag=1)  and pay_employee_master.id_card_dispatch='0' ";
        if (ddl_state.SelectedValue != "ALL")
        {

            update_where = update_where + " and `LOCATION` = '" + ddl_state.SelectedValue + "'";

        }
        else if (ddl_state.SelectedValue == "ALL")
        {

            update_where = update_where + " and `LOCATION` IN (Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY state_name) ";
        }
        if (ddlunitselect.SelectedValue != "ALL")
        {

            update_where = update_where + " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "' ";

        }
        else if (ddlunitselect.SelectedValue == "ALL")
        {
            update_where = update_where + " and pay_unit_master.unit_code in (Select unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + client_code2 + " AND branch_status = 0 ORDER BY UNIT_CODE)";

        }
        if (ddl_designation.SelectedValue != "ALL")
        {
            update_where = update_where + " and pay_employee_master.grade_code = '" + ddl_designation.SelectedValue + "' ";

        }
        else if (ddl_designation.SelectedValue == "ALL")
        {
            update_where = update_where + " and pay_employee_master.grade_code in (SELECT GRADE_CODE FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "') ";
        }
        
        if (ddl_state.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL") 
        {

            update_where = "pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' " + client_code + "  AND (`pay_employee_master`.`employee_type` = 'Permanent' || `pay_employee_master`.`employee_type` = 'Staff')  and left_date is null  and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag=1)  and pay_employee_master.id_card_dispatch='0' and `LOCATION` IN (Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + client_code + " ORDER BY state_name) and pay_unit_master.unit_code in (Select unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' AND branch_status = 0 ORDER BY UNIT_CODE) and pay_employee_master.grade_code in (SELECT GRADE_CODE FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "')   ";
        
        }




        string inlist = "";
        foreach (GridViewRow gvrow in gv_checklist_uniform.Rows)
        {

            // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
            string emp_code = gv_checklist_uniform.Rows[gvrow.RowIndex].Cells[5].Text;

            var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked == true)
            {

                inlist = inlist + "'" + emp_code + "',";
            }

        }

        if (inlist.Length > 0)
        {
            inlist = inlist.Substring(0, inlist.Length - 1);
        }
        else { inlist = "''"; }


     //   string id_check = null;
     //   string id_name = null;
        //foreach (object obj in inlist)
        //{
     //   id_check = d.getsinglestring("select emp_name from pay_document_details  INNER JOIN `pay_employee_master` ON `pay_document_details`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_document_details`.`emp_code` = `pay_employee_master`.`emp_code` where pay_document_details.comp_code = '" + Session["comp_code"].ToString() + "' and pay_document_details.client_code = '" + ddl_client.SelectedValue + "' and document_type = 'ID_Card' and pay_document_details.emp_code in ( " + inlist + " )");

        //if (id_check == "")
        //{
        //    string emp_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code in ( " + inlist + " ) ");

        //    id_name = id_name + "'" + emp_name + "',";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please give id card by Admin for that employee" + id_name + "');",true);
        //    //ddl_state_name.SelectedValue = "Select";
        //    return;


        //    //  }


        //}


        string employee_type = d.getsinglestring("select employee_type from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.emp_code in ( " + inlist + " ) ");

        if (employee_type == "Permanent")
        {
            update_where = update_where + " and pay_employee_master.emp_code in ( " + inlist + " ) and pay_employee_master.legal_flag = '2' ";

        }
        else
        {
            update_where = update_where + " and pay_employee_master.emp_code in ( " + inlist + " )  ";
        }

        d.operation("update pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code = pay_unit_master.comp_code  and pay_employee_master.unit_code = pay_unit_master.unit_code set id_card_dispatch_date = NOW(), id_card_dispatch = '1' where " + update_where + "");

    }

    public string id_uniform_shoes()
    {

        try
        {
            //for state
            d.con.Open();
            string where = null;

            string client_code = "";
            string emp_client_code = "";
            if (!ddl_client.SelectedValue.Equals("ALL"))
            {
                emp_client_code = " AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'";
                client_code = " and client_code = '" + ddl_client.SelectedValue + "'";
            }

            
                 //where = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + emp_client_code + " and pay_employee_master.employee_type='Permanent'  and left_date is null and pay_employee_master.legal_flag = '2' and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag= 1)   ";
            where = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + emp_client_code + "  AND (`pay_employee_master`.`employee_type` = 'Permanent' || `pay_employee_master`.`employee_type` = 'Staff')  and left_date is null  and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag= 1)   ";
                    if (ddl_state.SelectedValue != "ALL")
            {

                where = where + " and `LOCATION` = '" + ddl_state.SelectedValue + "'";

            }
            else if (ddl_state.SelectedValue == "ALL")
            {
                where = where + " and `LOCATION` IN (Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + client_code + " ORDER BY state_name)";

            }
            if (ddlunitselect.SelectedValue != "ALL")
            {

                where = where + " and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' ";

            }
            else if (ddlunitselect.SelectedValue == "ALL")
            {

              
                where = where + " and pay_unit_master.unit_code in (Select unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' "+client_code+" AND branch_status = 0 ORDER BY UNIT_CODE)";


            }
            if (ddl_designation.SelectedValue != "ALL")
            {
                where = where + " and pay_employee_master.grade_code = '" + ddl_designation.SelectedValue + "' ";

            }
            else if (ddl_designation.SelectedValue == "ALL")
            {
                where = where + " and pay_employee_master.grade_code in (SELECT GRADE_CODE FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "') ";
            
            }
            
            if (ddl_state.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL")
            {

                where = "pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + emp_client_code + "  AND (`pay_employee_master`.`employee_type` = 'Permanent' || `pay_employee_master`.`employee_type` = 'Staff')  and left_date is null  and pay_employee_master.emp_code not in (select emp_code from pay_document_details where dispatch_flag= 1)  and `LOCATION` IN (Select distinct(state_name) from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + client_code + " ORDER BY state_name) and pay_unit_master.unit_code in (Select unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' " + client_code + " AND branch_status = 0 ORDER BY UNIT_CODE) and pay_employee_master.grade_code in (SELECT GRADE_CODE FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "')  ";
            }
          


            return where;


        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }



    }

    private void ReportLoad_du(string query, string downloadfilename, string where) 
    {
        d.con.Close();
        d1.con.Close();
        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Connection = d.con;
            d.con.Open();
            // MySqlDataReader sda = cmd.ExecuteReader();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            // dt.Constraints.Clear();
            //dt.Load(sda);
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                d.con.Close();
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                //  crystalReport.SetDatabaseLogon(@"Tanvi-Tej\Tanvi","");
                //Response.End();
                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, downloadname);
                Response.End();
            }
          

        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    
    }



    private void ReportLoad(string query, string downloadfilename, string where)
    {
        d.con.Close();
        d1.con.Close();
        try
        {
            //btnsendemail.Visible = true;
            string downloadname = downloadfilename;
            System.Data.DataTable dt = new System.Data.DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Connection = d.con;
            d.con.Open();
           // MySqlDataReader sda = cmd.ExecuteReader();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
           // dt.Constraints.Clear();
            //dt.Load(sda);
            sda.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {

                d.con.Close();
                crystalReport.SetDataSource(dt);
                crystalReport.Refresh();
                //  crystalReport.SetDatabaseLogon(@"Tanvi-Tej\Tanvi","");
                //Response.End();
                crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, false, downloadname);
                Response.End();
            }
            else
            {


              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('First Approve By Legal');", true);
            }

        }
        catch (Exception ex) { throw ex; }
        finally
        {

        }
    }



    protected void btn_dispatch_print_Click(object sender, EventArgs e)
    {
        generate_letter(15);
    }





    private void generate_letter(int counter)
    {
        string sql = null;

        d2.con.Open();
        try
        {
            string where = "";
            if (counter != 14 && counter != 15 && counter != 16 && counter != 17)
            {
                where = " pay_unit_master.comp_code= '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.joining_date < str_to_date('31/" + txttodate.Text.Substring(0, 2) + "/" + txttodate.Text.Substring(3) + "','%d/%m/%Y')";
                if (!ddl_state.SelectedValue.ToUpper().Equals("ALL"))
                { where = where + " and pay_unit_master.state_name= '" + ddl_state.SelectedValue + "'"; }
                if (!ddlunitselect.SelectedValue.ToUpper().Equals("ALL"))
                {
                    where = where + " and pay_unit_master.unit_code= '" + ddl_state.SelectedValue + "'";
                }
                if (!ddl_designation.SelectedValue.ToUpper().Equals("ALL"))
                { where = where + " and pay_employee_master.grade_code = '" + ddl_designation.SelectedValue + "'"; }

                if (ddl_client.SelectedValue == "BAGIC TM")
                {
                    where = where + " and (pay_employee_master.employee_type='Permanent' OR pay_employee_master.employee_type='Temporary')";
                }
                else
                {
                    where = where + " and pay_employee_master.employee_type='Permanent'";
                }
            }
            if (counter.Equals(1) || counter.Equals(2) || counter.Equals(10))
            {
                if (counter == 1) { where = where + " and 18 > YEAR(DATE_SUB(NOW(), INTERVAL TO_DAYS(birth_date) DAY)) "; }
                else { where = where + " and 18 <= YEAR(DATE_SUB(NOW(), INTERVAL TO_DAYS(birth_date) DAY)) "; }

                sql = "select emp_name,case when gender='M' then 'MALE' else ' FEMALE' END as Gender,emp_father_name,  concat(EMP_PERM_ADDRESS,' ',EMP_PERM_CITY,' ',EMP_PERM_STATE,' ',EMP_PERM_PIN) as a ,concat(EMP_CURRENT_ADDRESS,' ',EMP_CURRENT_CITY,' ',EMP_CURRENT_STATE,' ',EMP_CURRENT_PIN) as b, date_format(joining_date,'%d/%m/%Y') as joining_date, case when Left_date is null then 'CONTINUE' else date_format(left_date,'%d/%m/%Y') END as left_date, case when Left_date is null then 'CONTINUE' else 'LEFT' END as left_reason ,(select concat(company_name,' ',ADDRESS1,' ',CITY,' ',STATE) from pay_company_master where pay_company_master.comp_code = pay_employee_master.comp_code) as company, (select grade_desc from pay_grade_master where pay_grade_master.grade_code = pay_employee_master.grade_code and pay_grade_master.comp_code = pay_employee_master.comp_code) as designation, pay_employee_master.id_as_per_dob FROM pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code = pay_unit_master.comp_code and pay_employee_master.unit_code = pay_unit_master.unit_code WHERE " + where + " and joining_date between str_to_date('1/" + txttodate.Text + "','%d/%m/%Y') and str_to_date('31/" + txttodate.Text + "','%d/%m/%Y') order by pay_employee_master.joining_date";

            }
            else if (counter == 3)
            {
                sql = "select emp_name,case when gender='M' then 'MALE' else ' FEMALE' END as Gender,emp_father_name,  concat(EMP_PERM_ADDRESS,' ',EMP_PERM_CITY,' ',EMP_PERM_STATE,' ',EMP_PERM_PIN) as a ,concat(EMP_CURRENT_ADDRESS,' ',EMP_CURRENT_CITY,' ',EMP_CURRENT_STATE,' ',EMP_CURRENT_PIN) as b, date_format(joining_date,'%d/%m/%Y') as joining_date, case when Left_date is null then 'CONTINUE' else date_format(left_date,'%d/%m/%Y') END as left_date, Left_reason ,(select concat(company_name,' ',ADDRESS1,' ',CITY,' ',STATE) from pay_company_master where pay_company_master.comp_code = pay_employee_master.comp_code) as company, id_as_per_dob from pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code = pay_unit_master.comp_code and pay_employee_master.unit_code = pay_unit_master.unit_code WHERE " + where + " and joining_date between str_to_date('1/" + txttodate.Text + "','%d/%m/%Y') and str_to_date('31/" + txttodate.Text + "','%d/%m/%Y') order by pay_employee_master.joining_date";
            }
            else if (counter == 4)
            {
                sql = "select emp_name,case when gender='M' then 'MALE' else ' FEMALE' END as Gender,emp_father_name,  concat(EMP_PERM_ADDRESS,' ',EMP_PERM_CITY,' ',EMP_PERM_STATE,' ',EMP_PERM_PIN) as a ,concat(EMP_CURRENT_ADDRESS,' ',EMP_CURRENT_CITY,' ',EMP_CURRENT_STATE,' ',EMP_CURRENT_PIN) as b, date_format(joining_date,'%d/%m/%Y') as joining_date, case when Left_date is null then 'CONTINUE' else date_format(left_date,'%d/%m/%Y') END as left_date, Left_reason ,(select concat(company_name,' ',ADDRESS1,' ',CITY,' ',STATE) from pay_company_master where pay_company_master.comp_code = pay_employee_master.comp_code) as company, id_as_per_dob from pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code = pay_unit_master.comp_code and pay_employee_master.unit_code = pay_unit_master.unit_code WHERE " + where + " and joining_date between str_to_date('1/" + txttodate.Text + "','%d/%m/%Y') and str_to_date('31/" + txttodate.Text + "','%d/%m/%Y') order by pay_employee_master.joining_date";
            }
            else if (counter == 5)
            {
                sql = "select emp_name,case when gender='M' then 'MALE' else ' FEMALE' END as Gender,emp_father_name,  concat(EMP_PERM_ADDRESS,' ',EMP_PERM_CITY,' ',EMP_PERM_STATE,' ',EMP_PERM_PIN) as a ,concat(EMP_CURRENT_ADDRESS,' ',EMP_CURRENT_CITY,' ',EMP_CURRENT_STATE,' ',EMP_CURRENT_PIN) as b, date_format(joining_date,'%d/%m/%Y') as joining_date, case when Left_date is null then 'CONTINUE' else date_format(left_date,'%d/%m/%Y') END as left_date, Left_reason ,(select concat(company_name,' ',ADDRESS1,' ',CITY,' ',STATE) from pay_company_master where pay_company_master.comp_code = pay_employee_master.comp_code) as company, id_as_per_dob from pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code = pay_unit_master.comp_code and pay_employee_master.unit_code = pay_unit_master.unit_code WHERE " + where + " and joining_date between str_to_date('1/" + txttodate.Text + "','%d/%m/%Y') and str_to_date('31/" + txttodate.Text + "','%d/%m/%Y')  order by pay_employee_master.joining_date";
            }
            else if (counter == 6)
            {
                sql = "SELECT upper(pay_pro_master.state_name) as state_name, upper(pay_pro_master.unit_city) as unit_city, pay_employee_master.emp_name, CASE WHEN pay_pro_master.gender = 'M' THEN 'MALE' ELSE ' FEMALE' END AS 'Gender', emp_father_name, CONCAT(EMP_PERM_ADDRESS, ' ', EMP_PERM_CITY, ' ', EMP_PERM_STATE, ' ', EMP_PERM_PIN) AS 'a', CONCAT(EMP_CURRENT_ADDRESS, ' ', EMP_CURRENT_CITY, ' ', EMP_CURRENT_STATE, ' ', EMP_CURRENT_PIN) AS 'b', DATE_FORMAT(pay_pro_master.joining_date, '%d/%m/%Y') AS 'joining_date', CASE WHEN Left_date IS NULL THEN 'CONTINUE' ELSE DATE_FORMAT(left_date, '%d/%m/%Y') END AS 'left_date', if(LEFT_REASON !='','LEFT SERVICE',LEFT_REASON) AS 'LEFT_REASON', (SELECT CONCAT(company_name, ' ', ADDRESS1, ' ', CITY, ' ', STATE) FROM pay_company_master WHERE pay_company_master.comp_code = pay_employee_master.comp_code) AS 'company', id_as_per_dob, ROUND((emp_basic_vda / total_days_present), 2) AS 'wage', UPPER(CONCAT(IF(pay_employee_master.grade_code = 'SG', 'SG', 'HK'), ' Services ', pay_pro_master.unit_city)) AS 'grade_code', (SELECT grade_desc FROM pay_grade_master WHERE pay_grade_master.grade_code = pay_employee_master.grade_code LIMIT 1) AS 'designation' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_pro_master ON pay_pro_master.comp_code = pay_unit_master.comp_code AND pay_pro_master.client_code = pay_unit_master.client_code AND pay_pro_master.unit_code = pay_unit_master.unit_code AND pay_pro_master.emp_code = pay_employee_master.emp_code WHERE " + where + " AND pay_employee_master.employee_type = 'Permanent' GROUP BY pay_employee_master.emp_code";
            }
            else if (counter == 7)
            {
                string start_date_common = d.getsinglestring("select start_date_common from pay_billing_master_history where billing_client_code = '" + ddl_client.SelectedValue + "' and month =" + txttodate.Text.Substring(0, 2) + " and year=" + txttodate.Text.Substring(3) + " limit 1");
                if (start_date_common == "1" || start_date_common == "")
                {
                    string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-01','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-01'), '%D %b %Y'))) as fromtodate";
                    sql = "select pay_unit_master.state_name," + daterange + ",day(last_day(str_to_date('01/" + txttodate.Text.Substring(0, 2) + "/" + txttodate.Text.Substring(3) + "','%d/%m/%Y'))) as 'total days',  emp_name, pay_employee_master.id_as_per_dob as 'emp_code',unit_name,case when DAY01 ='P' then 'P' else 'A' END as  DAY01,case when DAY02 ='P' then 'P' else 'A' END as  DAY02,case when DAY03 ='P' then 'P' else 'A' END as  DAY03,case when DAY04 ='P' then 'P' else 'A' END as  DAY04,case when DAY05 ='P' then 'P' else 'A' END as  DAY05,case when DAY06 ='P' then 'P' else 'A' END as  DAY06,case when DAY07 ='P' then 'P' else 'A' END as  DAY07,case when DAY08 ='P' then 'P' else 'A' END as  DAY08,case when DAY09 ='P' then 'P' else 'A' END as  DAY09,case when DAY10 ='P' then 'P' else 'A' END as  DAY10,case when DAY11 ='P' then 'P' else 'A' END as  DAY11,case when DAY12 ='P' then 'P' else 'A' END as  DAY12,case when DAY13 ='P' then 'P' else 'A' END as  DAY13,case when DAY14 ='P' then 'P' else 'A' END as  DAY14,case when DAY15 ='P' then 'P' else 'A' END as  DAY15,case when DAY16 ='P' then 'P' else 'A' END as  DAY16,case when DAY17 ='P' then 'P' else 'A' END as  DAY17,case when DAY18 ='P' then 'P' else 'A' END as  DAY18,case when DAY19 ='P' then 'P' else 'A' END as  DAY19,case when DAY20 ='P' then 'P' else 'A' END as  DAY20,case when DAY21 ='P' then 'P' else 'A' END as  DAY21,case when DAY22 ='P' then 'P' else 'A' END as  DAY22,case when DAY23 ='P' then 'P' else 'A' END as  DAY23,case when DAY24 ='P' then 'P' else 'A' END as  DAY24,case when DAY25 ='P' then 'P' else 'A' END as  DAY25,case when DAY26 ='P' then 'P' else 'A' END as  DAY26,case when DAY27 ='P' then 'P' else 'A' END as  DAY27,case when DAY28 ='P' then 'P' else 'A' END as  DAY28,case when DAY29 ='P' then 'P' else 'A' END as  DAY29,case when DAY30 ='P' then 'P' else 'A' END as  DAY30,case when DAY31 ='P' then 'P' else 'A' END as  DAY31,TOT_DAYS_PRESENT,(select company_name from pay_company_master where pay_company_master.comp_code = pay_unit_master.comp_code) as company,(Select LICENSE_NO From pay_client_master where client_code =pay_unit_master.client_code) as 'LICENSE_NO' from pay_employee_master inner join pay_attendance_muster on pay_attendance_muster.emp_code = pay_employee_master.emp_code inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code where pay_attendance_muster.month = " + txttodate.Text.Substring(0, 2) + " and pay_attendance_muster.year = " + txttodate.Text.Substring(3) + "  and " + where;
                }
                else
                {
                    int month = int.Parse(txttodate.Text.Substring(0, 2)) - 1;
                    int year = int.Parse(txttodate.Text.Substring(3));
                    if (month == 0) { month = 12; year = year - 1; }
                    string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + ((int.Parse(txttodate.Text.Substring(0, 2)) - 1) == 0 ? (int.Parse(txttodate.Text.Substring(3)) - 1).ToString() : txttodate.Text.Substring(3)) + "-" + ((int.Parse(txttodate.Text.Substring(0, 2)) - 1) == 0 ? 12 : (int.Parse(txttodate.Text.Substring(0, 2)) - 1)) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + txttodate.Text.Substring(3) + "-" + txttodate.Text.Substring(0, 2) + "-" + (int.Parse(start_date_common) - 1) + "','%Y-%m-%d'), '%D %b %Y'))) as fromtodate";

                    sql = "select pay_unit_master.state_name, " + daterange + ", day(last_day(str_to_date('01/" + month + "/" + year + "','%d/%m/%Y'))) as 'total days', pay_employee_master.emp_name, pay_employee_master.id_as_per_dob as 'emp_code',unit_name, " + d.get_calendar_days(int.Parse(start_date_common), txttodate.Text.Substring(0, 2) + "/" + txttodate.Text.Substring(3), 1, 1) + "" + d.get_calendar_days(int.Parse(start_date_common), txttodate.Text.Substring(0, 2) + "/" + txttodate.Text.Substring(3), 3, 1) + " pay_attendance_muster.tot_days_present,(select company_name from pay_company_master where pay_company_master.comp_code = pay_unit_master.comp_code) as company,(Select LICENSE_NO From pay_client_master where client_code =pay_unit_master.client_code) as 'LICENSE_NO' from pay_employee_master LEFT JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code AND pay_attendance_muster.comp_code = pay_employee_master.comp_code AND pay_attendance_muster.month =  '" + txttodate.Text.Substring(0, 2) + "'   AND pay_attendance_muster.Year = '" + txttodate.Text.Substring(3) + "'   AND pay_attendance_muster.tot_days_present > 0  INNER JOIN pay_unit_master ON pay_employee_master.unit_code = pay_unit_master.unit_code AND pay_employee_master.comp_code = pay_unit_master.comp_code   left JOIN pay_grade_master ON pay_unit_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE  INNER JOIN pay_company_master ON pay_unit_master.comp_code = pay_company_master.comp_code  LEFT JOIN pay_ot_muster ON  pay_attendance_muster.emp_code = pay_ot_muster.emp_code AND pay_attendance_muster.comp_code = pay_ot_muster.comp_code  AND pay_attendance_muster.UNIT_CODE = pay_ot_muster.UNIT_CODE  AND pay_attendance_muster.MONTH = pay_ot_muster.MONTH  AND pay_attendance_muster.YEAR = pay_ot_muster.YEAR  AND pay_ot_muster.month =  '" + txttodate.Text.Substring(0, 2) + "'  LEFT JOIN pay_attendance_muster t2   ON " + year + "= t2.year AND pay_company_master.COMP_CODE = t2.COMP_CODE  AND pay_unit_master.UNIT_CODE = t2.UNIT_CODE AND pay_employee_master.EMP_CODE = t2.EMP_CODE  AND t2.month ='" + month + "'  AND t2.tot_days_present > 0 LEFT OUTER JOIN pay_ot_muster t3   ON " + year + " = t3.YEAR  AND pay_unit_master.UNIT_CODE = t3.UNIT_CODE AND pay_employee_master.EMP_CODE = t3.EMP_CODE AND pay_company_master.COMP_CODE = t3.COMP_CODE   AND t3.month = '" + month + "' WHERE pay_attendance_muster.month =  '" + txttodate.Text.Substring(0, 2) + "' AND pay_attendance_muster.Year = '" + txttodate.Text.Substring(3) + "'   AND pay_attendance_muster.tot_days_present > 0 and " + where;
                }
            }

            else if (counter == 9)
            {
                sql = "SELECT (SELECT company_name FROM pay_company_master WHERE pay_company_master.comp_code = pay_employee_master.comp_code) AS 'company', ihms AS 'ihms_code', pay_pro_master.emp_name, grade, Basic, ROUND((emp_basic_vda / Total_Days_Present), 2) AS 'Rate_of_Wage', ROUND(emp_basic_vda, 2) AS 'Special_Basic', ROUND(hra_amount_salary, 2) AS 'hra', ROUND(sal_bonus_gross, 2) AS 'Bonus', leave_sal_gross, ROUND(leave_sal_gross, 2) AS 'leave_gross', ROUND((emp_basic_vda + hra_amount_salary + sal_bonus_gross + leave_sal_gross + pay_pro_master.special_allow+ot_amount), 2) AS 'total_earnings', ROUND(PAYMENT, 2) AS 'net_payment', Total_Days_Present, client, pay_pro_master.state_name, COMPANY_NAME, LICENSE_NO, pay_pro_master.EMP_CODE, id_as_per_dob, ROUND(sal_pf, 2) AS 'PF', ROUND(sal_esic, 2) AS 'ESIC', ROUND(lwf_salary, 2) AS 'lwf', ROUND(pt_amount, 2) AS 'pt', ROUND((sal_pf + sal_esic + lwf_salary + pt_amount + pay_pro_master.fine + pay_pro_master.EMP_ADVANCE_PAYMENT), 2) AS 'total_deduction', ROUND(pay_pro_master.special_allow, 2) AS 'allow', ROUND(pay_pro_master.fine, 2) AS 'fine', ROUND(pay_pro_master.EMP_ADVANCE_PAYMENT, 2) AS 'advance', round(ot_amount,2) as ot_amount, ot_hours,sal_bonus_after_gross,leave_sal_after_gross FROM pay_pro_master INNER JOIN pay_employee_master ON pay_pro_master.emp_code = pay_employee_master.emp_code AND pay_pro_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_pro_master.unit_code AND pay_unit_master.comp_code = pay_pro_master.comp_code WHERE pay_pro_master.month = '" + txttodate.Text.Substring(0, 2) + "' AND pay_pro_master.year = '" + txttodate.Text.Substring(3) + "' AND " + where + " order by pay_employee_master.joining_date";
            }
            else if (counter == 11)
            {
                sql = "SELECT pay_employee_master.emp_name, id_as_per_dob, pay_grade_master.grade_desc, if(emp_blood_group='Select','',emp_blood_group) as emp_blood_group, DATE_FORMAT(joining_date, '%d/%m/%Y') AS 'DOJ', client_name, emp_photo FROM pay_employee_master INNER JOIN pay_grade_master ON pay_grade_master.grade_code = pay_employee_master.grade_code AND pay_grade_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.client_code = pay_client_master.client_code AND pay_unit_master.comp_code = pay_employee_master.comp_code INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' and pay_employee_master.employee_type='Permanent' and left_date is null";
            }
            else if (counter == 12)
            {
                sql = "select client, state_name,unit_gst_no, round(sum(CGST9),2) as cgst,round(sum(IGST18),2) as igst,round(sum(SGST9),2) as sgst from pay_billing_unit_rate_history where client_code = '" + ddl_client.SelectedValue + "' and month=" + txttodate.Text.Substring(0, 2) + " and year=" + txttodate.Text.Substring(3) + " and comp_code = '" + Session["COMP_CODE"].ToString() + "' group by state_name order by state_name";
            }
            else if (counter == 13)
            {
                sql = "select client, state_name, unit_city, emp_name, grade, round(gross,2) as gross, pt_amount from pay_pro_master where month=" + txttodate.Text.Substring(0, 2) + " and year=" + txttodate.Text.Substring(3) + " and client_code = '" + ddl_client.SelectedValue + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and pt_amount > 0 order by 1,2,3,4,5";
            }
            else if (counter == 14)
            {
                sql = "SELECT billing_date, invoice_no, client, CONCAT(month, '/', year) AS 'monthyear', state_name, unit_gst_no, CAST(GROUP_CONCAT(DISTINCT grade_desc) AS char) AS 'Service', ROUND((SUM(service_charge) + SUM(amount) + SUM(uniform) + SUM(operational_cost) ), 2) AS 'Basic', ROUND(SUM(CGST9), 2) AS 'cgst', ROUND(SUM(IGST18), 2) as igst, ROUND(SUM(SGST9), 2) as sgst FROM pay_billing_unit_rate_history WHERE comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_date IS NOT NULL AND invoice_no IS NOT NULL GROUP BY invoice_no ORDER BY 3,5";
            }
            else if (counter == 15)
            {
                //string where1 = id_uniform_shoes();

                string where1 = null;

                if (ddl_state.SelectedValue == "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL")
                {
                    where1 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')   AND ID_CARD_DISPATCH = '1' and pay_client_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name IN (SELECT DISTINCT (state_name) FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY state_name) AND pay_unit_master.unit_code IN (SELECT unit_code FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE)  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "')  AND legal_flag = '2' group by emp_name";

                }
                else
                    if (ddl_state.SelectedValue != "ALL" && ddlunitselect.SelectedValue != "ALL" && ddl_designation.SelectedValue != "ALL")
                    {
                        where1 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and EMP_CURRENT_STATE= '" + ddl_state.SelectedValue + "' and grade_code = '" + ddl_designation.SelectedValue + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y') and legal_flag = '2'";

                        // pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.state_name = '" + ddl_state.SelectedValue + "'  AND pay_unit_master.unit_code IN (SELECT unit_code FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE)  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = '2')


                    }


                    else if (ddl_state.SelectedValue != "ALL" && ddlunitselect.SelectedValue == "ALL" && ddl_designation.SelectedValue == "ALL")
                    {
                        //  where1 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "'and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and EMP_CURRENT_STATE= '" + ddl_state.SelectedValue + "' and grade_code = '" + ddl_designation.SelectedValue + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')   AND ID_CARD_DISPATCH = '1'  group by emp_name ";
                        where1 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "'  and EMP_CURRENT_STATE= '" + ddl_state.SelectedValue + "'  and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')   AND ID_CARD_DISPATCH = '1'  AND pay_unit_master.unit_code IN (SELECT unit_code FROM pay_unit_master WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE)  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "') AND legal_flag = '2' group by emp_name ";
                    }
                    else if (ddl_state.SelectedValue != "ALL" && ddlunitselect.SelectedValue != "ALL" && ddl_designation.SelectedValue == "ALL")
                    {
                        where1 = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "'AND pay_client_master.client_code = '" + ddl_client.SelectedValue + "'  and EMP_CURRENT_STATE= '" + ddl_state.SelectedValue + "'  and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y')   AND ID_CARD_DISPATCH = '1'  AND pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'  AND pay_employee_master.grade_code IN (SELECT GRADE_CODE FROM pay_grade_master WHERE comp_code = '" + Session["comp_code"].ToString() + "') AND legal_flag = '2' group by emp_name ";
                    }



                sql = "SELECT REPLACE(client_name, 'NEW', '') AS 'client_name', UPPER(concat(unit_add2,' ',pay_unit_master.state_name,' ',pay_unit_master.pincode)) AS 'unit_add2', UPPER(emp_name) AS 'emp_name', replace(group_concat(UPPER(ifnull(document_type,''))),',','') AS 'document_type', replace(group_concat(ifnull(size,'')),',','/') as size, emp_mobile_no from pay_employee_master left join pay_document_details on pay_document_details.emp_code = pay_employee_master.emp_code and document_type != 'ID_Card' inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code and pay_unit_master.comp_code = pay_employee_master.comp_code inner join pay_client_master on pay_unit_master.client_code = pay_client_master.client_code where " + where1 + " ";
            }
            else if (counter == 16)
            {
                sql = "select distinct pay_employee_master.emp_code, emp_name, if((left_date != ''),'LEFT','CONTINUE') as Status, document_type, no_of_set, size, emp_mobile_no, unit_city, state_name, unit_add1,unit_add2, pay_employee_master.client_code, pay_employee_master.grade_code, gender,pincode,'POD','POD DATE','COURIER NAME' from  pay_employee_master left join pay_document_details on pay_document_details.emp_code = pay_employee_master.emp_code and document_type != 'ID_Card' inner join pay_unit_master on pay_unit_master.unit_code = pay_employee_master.unit_code AND pay_unit_master.COMP_code = pay_employee_master.COMP_code  where pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and id_card_dispatch_date = str_to_date('" + txt_print_list.Text + "','%d/%m/%Y') ";
            }
            else if (counter == 17)
            {
                sql = "select client,upper(state_name) as state_name,upper(unit_city) as unit_city,emp_name,grade_desc,(month_days-tot_days_present) as absent from pay_billing_unit_rate_history WHERE client_code = '" + ddl_client.SelectedValue + "' AND month = " + txttodate.Text.Substring(0, 2) + " AND year = " + txttodate.Text.Substring(3) + " and emp_type = 'Permanent'";
            }
            else if (counter == 18)
            {
                sql = "SELECT pay_employee_master.EMP_NAME, EMP_FATHER_NAME AS 'FatherName', DATE_FORMAT(BIRTH_DATE, '%d/%m/%Y') AS 'Birth_date', DATE_FORMAT(JOINING_DATE, '%d/%m/%Y') AS 'Date_Of_Joining', PAN_NUMBER AS 'UAN_NO', ESIC_NUMBER, DATE_FORMAT(Left_date, '%d/%m/%Y') AS 'left_date', IF(LEFT_REASON != '', 'LEFT SERVICE', LEFT_REASON) AS 'Reason_Of_Exit', pay_unit_master.unit_city FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code where " + where + " and emp_code not in (select emp_code FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code where " + where + " and pay_employee_master.left_date < str_to_date('1/" + txttodate.Text.Substring(0, 2) + "/" + txttodate.Text.Substring(3) + "','%d/%m/%Y') ) order by JOINING_DATE";
            }
            MySqlDataAdapter dscmd = new MySqlDataAdapter(sql, d.con);
            System.Data.DataSet ds = new System.Data.DataSet();
            dscmd.Fill(ds);
            // int days = 0;


            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                if (counter == 1)
                {
                    Response.AddHeader("content-disposition", "attachment;filename=FORM_XIII_REG_WORKMEN_EMPLOYED_BELOW_18.xls");
                }
                else if (counter == 2) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XVI-Muster_Roll.xls"); }
                else if (counter == 3) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XXIII-Register_of_overtime.xls"); }
                else if (counter == 4) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XXII-Register_of_advances.xls"); }
                else if (counter == 5) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XXI-Register_of_fines.xls"); }
                else if (counter == 6) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XX-Reg-deduction.xls"); }
                else if (counter == 7) { Response.AddHeader("content-disposition", "attachment;filename=FORM_D_" + ddl_state.SelectedValue + ".xls"); }
                else if (counter == 8) { Response.AddHeader("content-disposition", "attachment;filename=FORM_A.xls"); }
                else if (counter == 9) { Response.AddHeader("content-disposition", "attachment;filename=FORM_B.xls"); }
                else if (counter == 10) { Response.AddHeader("content-disposition", "attachment;filename=FORM_XIII_REG_WORKMEN_EMPLOYED_ABOVE_18.xls"); }
                else if (counter == 11) { Response.AddHeader("content-disposition", "attachment;filename=ID_CARD_PRINT.xls"); }
                else if (counter == 12) { Response.AddHeader("content-disposition", "attachment;filename=GST.xls"); }
                else if (counter == 13) { Response.AddHeader("content-disposition", "attachment;filename=PROFESSIONAL_TAX.xls"); }
                else if (counter == 14) { Response.AddHeader("content-disposition", "attachment;filename=SALES_REGISTER.xls"); }
                else if (counter == 15) { Response.AddHeader("content-disposition", "attachment;filename=DISPATCH_ADDRESS_LIST.xls"); }
                else if (counter == 16) { Response.AddHeader("content-disposition", "attachment;filename=UNIFORM_SHOES_DETAILS.xls"); }
                else if (counter == 17) { Response.AddHeader("content-disposition", "attachment;filename=LEAVE_REGISTER.xls"); }
                else if (counter == 18) { Response.AddHeader("content-disposition", "attachment;filename=EMPLOYEE_DATA.xls"); }

                string path = Server.MapPath("~/EMP_Images");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                string start_date_common = "1";
                if (counter != 14 && counter != 15 && counter != 16 && counter != 17 && counter != 18)
                { start_date_common = get_start_date(); }
                //if (txttodate.Text != "")
                //{
                //    days = DateTime.DaysInMonth(int.Parse(txttodate.Text.Substring(3)), int.Parse(txttodate.Text.Substring(0, 2)));
                //}
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, ds, txttodate.Text, ddl_designation.SelectedItem.Text, ddl_client.SelectedItem.Text, counter, path, txttodate.Text, d.get_calendar_days(int.Parse(start_date_common), txttodate.Text, 0, 1), start_date_common, ddl_state.SelectedValue);
                Repeater1.ItemTemplate = new MyTemplate(ListItemType.Item, ds, "", ddl_designation.SelectedItem.Text, "", counter, path, txttodate.Text, "", "", "");
                Repeater1.FooterTemplate = new MyTemplate(ListItemType.Footer, ds, "", "", "", 0, "", "", "", "a", "");
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
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d2.con.Close();
        }
    }


    public class MyTemplate : ITemplate
    {
        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        string year;
        string designation;
        string client, state;
        int counter;
        int daysadd = 0, month_days = 0;

        static int ctr;
        string path = "", header1 = "", header = "", start_date_common = "";
        string payment_date;

        public MyTemplate(ListItemType type, DataSet ds, string year, string designation, string client, int counter, string path, string payment_date, string header1, string start_date_common, string state)
        {
            this.counter = counter;
            this.type = type;
            this.ds = ds;
            this.year = year;
            this.designation = designation;
            this.client = client;
            this.path = path;
            this.state = state;
            ctr = 0;
            this.payment_date = payment_date;
            this.header1 = header1;
            this.start_date_common = start_date_common;
            if (counter == 7)
            {
                header = "<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th>";
                month_days = int.Parse(ds.Tables[0].Rows[ctr]["total days"].ToString());
                if (month_days == 29)
                { header = header + "<th>29</th>"; daysadd = 1; }
                else if (month_days == 30)
                {
                    header = header + "<th>29</th><th>30</th>"; daysadd = 2;
                }
                else if (month_days == 31)
                {
                    header = header + "<th>29</th><th>30</th><th>31</th>"; daysadd = 3;
                }
            }
        }
        public void InstantiateIn(Control container)
        {
            switch (type)
            {
                case ListItemType.Header:
                    if (counter == 1 || counter == 10)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=12 align=center><b>FORM XIII</b></td></tr><tr><td colspan=12 align=center>REGISTER OF WORKMEN EMPLOYED BY CONTRACTOR</td></tr><tr><td colspan=12 align=center>[Rule 75]</td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=6 align=left>Name and address of establishment in/under <b>which contract is carried on " + year.Substring(3) + "</b></td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Nature and location of work <b>" + designation + " Services</b></td><td colspan=6 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=12> </td></tr><tr><th>Sl. No.</th><th>Name and surname of workman</th><th>Age and Sex</th><th>Father's/ Husband's name</th><th>Nature of employment/ Designation</th><th>Permanent Home Address of workman (Village and Tahsil/Taluk and District</th><th>Local Address</th><th>Date of commencement of employment</th><th>Signature or thumb impression of workman</th><th>Date of termination of employment</th><th>Reasons for termination</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 2)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=6 align=center><b>FORM XVI</b></td></tr><tr><td colspan=6 align=center><b>MUSTER ROLL</b></td></tr><tr><td colspan=6 align=center>[Rule 78(1)(a)(i)]</td></tr><tr><td colspan=6> </td></tr><tr><td colspan=3 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=3 align=left>Name and address of establishment in/under which contract is carried on " + year.Substring(3) + "</td></tr><tr><td colspan=6> </td></tr><tr><td colspan=3 align=left>Nature and location of work <b>" + designation + " Services</b></td><td colspan=3 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=6> </td></tr><tr><th>Sl. No.</th><th>Name of workman</th><th>Father's/ Husband's name</th><th>Sex</th><th>Dates</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 3)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=12 align=center><b>FORM XXIII</b></td></tr><tr><td colspan=12 align=center>REGISTER OF OVERTIME</td></tr><tr><td colspan=12 align=center>[Rule 78(1)(a)(iii)]</td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=6 align=left>Name and address of establishment in/under <b>which contract is carried on " + year.Substring(3) + "</b></td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Nature and location of work <b>" + designation + " Services</b></td><td colspan=6 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=12> </td></tr><tr><th>Sl. No.</th><th>Name of workman</th><th>Father's/ Husband's name</th><th>Sex</th><th>Nature of employment / Designation</th><th>Date on which overtime worked</th><th>Total overtime worked or production in case of piece rated</th><th>Normal rates of wages</th><th>Overtime rate of wages</th><th>Overtime earnings</th><th>Date on which overtime wages paid</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 4)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=11 align=center><b>FORM XXII</b></td></tr><tr><td colspan=11 align=center>REGISTER OF ADVANCES</td></tr><tr><td colspan=11 align=center>[Rule 78(1)(a)(iii)]</td></tr><tr><td colspan=11> </td></tr><tr><td colspan=6 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=5 align=left>Name and address of establishment in/under <b>which contract is carried on " + year.Substring(3) + "</b></td></tr><tr><td colspan=11> </td></tr><tr><td colspan=6 align=left>Nature and location of work <b>" + designation + " Services</b></td><td colspan=5 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=11> </td></tr><tr><th>Sl. No.</th><th>Name of workman</th><th>Father's/ Husband's name</th><th>Nature of employment / Designation</th><th>Wage period and wages payable</th><th>Date and amount of advance given</th><th>Purpose (s) for which advance made</th><th>No. of installments by which advance to be repaid</th><th>Date and amount of each installment repaid</th><th>Date on which last installment was repaid</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 5)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=12 align=center><b>FORM XXI</b></td></tr><tr><td colspan=12 align=center>REGISTER OF FINES</td></tr><tr><td colspan=12 align=center>[Rule 78(1)(a)(ii)]</td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=6 align=left>Name and address of establishment in/under <b>which contract is carried on " + year.Substring(3) + "</b></td></tr><tr><td colspan=12> </td></tr><tr><td colspan=6 align=left>Nature and location of work <b>" + designation + " Services</b></td><td colspan=6 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=12> </td></tr><tr><th>Sl. No.</th><th>Name of workman</th><th>Father's/ Husband's name</th><th>Nature of employment / Designation</th><th>Act/Omission for which fine imposed</th><th>Date of offence</th><th>Whether workman showned cause against fine</th><th>Name of person in whose presence employee's explaination was heard</th><th>Wage periods and wages payable</th><th>Amount of fine imposed</th><th>Date on which fine realised</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 6)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=15 align=center><b>FORM XX</b></td></tr><tr><td colspan=15 align=center>REGISTER OF DEDUCTIONS FOR DAMAGE OR LOSS</td></tr><tr><td colspan=15 align=center>[Rule 78(1)(a)(ii)]</td></tr><tr><td colspan=15> </td></tr><tr><td colspan=9 align=left>Name and address of contractor <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=6 align=left>Name and address of establishment in/under <b>which contract is carried on " + year.Substring(3) + "</b></td></tr><tr><td colspan=15> </td></tr><tr><td colspan=9 align=left>Nature and location of work <b>" + ds.Tables[0].Rows[ctr]["grade_code"] + " Services</b></td><td colspan=6 align=left>Name and address of Principal Employer <b>" + client + "</b></td></tr><tr><td colspan=15> </td></tr><tr><th>Sl. No.</th><th>Name of workman</th><th>State</th><th>Location</th><th>Father's/ Husband's name</th><th>Nature of employment / Designation</th><th>Particulars of damage or loss</th><th>Date of damage or loss</th><th>Whether workman showned cause against deduction</th><th>Name of person in whose presence employee's explaination was heard</th><th>Amount of deduction imposed</th><th>No. of installments</th><th colspan=2>Date of recovery</th><th>Remarks</th></tr><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th>First Installment</th><th>Last Installment</th><th></th></tr>");
                        break;
                    }
                    else if (counter == 7)
                    {
                        if (start_date_common != "" && start_date_common != "1")
                        {
                            header = header1;
                        }

                        lc = new LiteralControl("<table border=1><tr><td colspan=" + (month_days + 8) + " align=center><b>FORM D</b></td></tr><tr><td colspan=<td colspan=" + (month_days + 8) + " align=center>" + state + "</td></tr><tr><td colspan=" + (month_days + 8) + "></td></tr><tr><td colspan=12>Name of establishment - <b>" + client + "</b></td><td colspan=13>Name of Owner - <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=" + ((month_days + 9) - 26) + ">LIN - " + ds.Tables[0].Rows[ctr]["LICENSE_NO"] + " </td></tr><tr><td colspan=" + (month_days + 8) + "></td></tr><tr><td colspan=" + (month_days + 8) + " align=left>For the Period From <b>" + ds.Tables[0].Rows[ctr]["fromtodate"] + "</b></td></tr><tr><td colspan=" + (month_days + 8) + "></td></tr><tr><th>Sl. No.</th><th>Sl. No. in Emp. Register</th><th>Name</th><th>Relay # or set work</th><th>Place of Work</th><th colspan=" + month_days + ">Dates</th><th>Summary of days</th><th>Remarks No. of hours</th><th>Signature of Register keeper</th></tr><tr><th></th><th></th><th></th><th></th><th></th>" + header + "</th><th></th><th></th></tr>");
                        break;
                    }
                    else if (counter == 8)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=31 align=center><b>SCHEDULE</b></td></tr><tr><td colspan=<td colspan=31 align=center>[ See rule 2(1) ]</td></tr><tr><td colspan=31 align=center>FORM A</td></tr><tr><td colspan=31 align=center>[PART-A]</td></tr><tr><td colspan=15>Name of establishment - <b>" + client + "</b></td><td colspan=16>Name of Owner - <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td></tr><tr><td colspan=31> </td></tr><tr><th>Sr.No.</th><th>Employee Code</th><th>Name and Surname</th><th>Gender</th><th>Father's/Spouse Name</th><th>Date of Birth</th><th>Nationality</th><th>Education Level</th><th>Date of Joining</th><th>Designation</th><th>Category Address *(HS/S/SS/US)</th><th>Type of Employment</th><th>Mobile</th><th>UAN</th><th>PAN</th><th>PF</th><th>ESI IP</th><th>LWF</th><th>AADHAR</th><th>Bank A/c No.</th><th>Bank</th><th>Branch (IFSC)</th><th>Present Address</th><th>Permanent</th><th>Service Book No.</th><th>Date of Exit</th><th>Reason for Exit</th><th>Mark of Identification</th><th>Employee Photo</th><th>Specimen Signature/Thumb Impression</th><th>Remarks</th></tr>");
                        break;
                    }
                    else if (counter == 9)
                    {
                        lc = new LiteralControl("<table border=1><tr><td colspan=37 align=center><b>FORM B</b></td></tr><tr><td colspan=37 align=center><b>" + ds.Tables[0].Rows[ctr]["state_name"] + "</b></td></tr><tr><td colspan=37 align=center><b>Rate of Minimum Wages and since the date</b></td></tr><tr><td colspan=5></td><td colspan=8 align=center><b>Highly Skilled</b></td><td colspan=10 align=center><b>Skilled</b></td><td colspan=8 align=center><b>Semi Skilled</b></td><td colspan=6 align=center><b>Un Skilled</b></td></tr><tr><td colspan=5 aign=left><b>Minimum Basic</b></td><td colspan=8 align=center>N/A</td><td colspan=10 align=center>N/A</td><td colspan=8 align=center>" + ds.Tables[0].Rows[ctr]["Basic"] + "</td><td colspan=6 align=center>N/A</td></tr><tr><td colspan=5 aign=left><b>DA</b></td><td colspan=8></td><td colspan=10</td><td colspan=8></td><td colspan=6></td></tr><tr><td colspan=5 aign=left><b>Overtime</b></td><td colspan=8></td><td colspan=10></td><td colspan=8></td><td colspan=6></td></tr><tr><td colspan=14>Name of establishment - <b>" + client + "</b></td><td colspan=12>Name of Owner - <b>" + ds.Tables[0].Rows[ctr]["company"] + "</b></td><td colspan=11>LIN - <b>" + ds.Tables[0].Rows[ctr]["LICENSE_NO"] + "</td></tr><tr><td colspan=37><b>Wage period From 1/" + year + " To " + month_days + "/" + year + " (Monthly/Fortnightly/Weekly/Daily/Piece Rated)</b></td></tr><tr><td colspan=37></td></tr><tr><td colspan=7></td><td colspan=13 align=center><b>Earned Wages</b></td><td align=center colspan=12><b>Deduction</b></td><td colspan=5></td></tr><tr><th>Employee Code</th><th>Sr.No</th><th>Sl. No. in Employee register</th><th>Name and Surname</th><th>Rate of Wage</th><th>No. of Days worked</th><th>Overtime hours worked</th><th>Basic</th><th>Special Basic</th><th>DA</th><th>SA</th><th>Payments Overtime</th><th>HRA</th><th colspan=6>Others</th><th>Total</th><th>PF</th><th>ESIC</th><th>LWF</th><th>PT</th><th>Society</th><th>Income Tax</th><th>Insurance</th><th colspan=3>Others</th><th>Recoveries</th><th>Total</th><th>Net payment</th><th>Employer Share PF Welfare Fund</th><th>Receipt by Employee/Bank Transaction ID</th><th>Date of Payment</th><th>Remarks</th></tr><tr><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th colspan=6>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th colspan=3>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th><th>29</th><th>30</th></tr><tr><th colspan=13></th><th>Bonus</th><th>Advance Bonus</th><th>Leave</th><th>Advance Leave</th><th>Allowance</th><th>Total Others</th><th colspan=8></th><th>Fine</th><th>Advance</th><th>Total Others</th><th colspan=7></th></tr>");
                        break;
                    }
                    else if (counter == 12)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR NO</th><th>CLIENT NAME</th><th>STATE</th><th>CLIENT GST</th><th>CGST</th><th>IGST</th><th>SGST</th></tr>");
                        break;
                    }
                    else if (counter == 13)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR NO</th><th>CLIENT NAME</th><th>STATE</th><th>BRANCH CITY</th><th>EMPLOYEE NAME</th><th>DESIGNATION</th><th>GROSS</th><th>PT AMOUNT</th></tr>");
                        break;
                    }
                    else if (counter == 14)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR NO</th><th>BILLING DATE</th><th>INVOICE NUMBER</th><th>CLIENT NAME</th><th>MONTH/YEAR</th><th>STATE</th><th>CLIENT GST NO.</th><th>SERVICE</th><th>BASIC</th><th>CGST</th><th>IGST</th><th>SGST</th></tr>");
                        break;
                    }
                    else if (counter == 15)
                    {
                        lc = new LiteralControl("<table border=1>");
                        break;
                    }
                    else if (counter == 16)
                    {
                        //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                        //lc = new LiteralControl("<table border=1><tr><th colspan=9>" + client + " BONUS RETURN - FORM D</th></tr><tr><th colspan=9>MONTH - " + mfi.GetMonthName(int.Parse(year.Substring(0, 2))).ToString() + "-" + year.Substring(3) + "</th></tr><tr><th>SR NO</th><th>STATE</th><th>LOCATON</th><th>NAME</th><th>BASIC</th><th>TOTAL PRESENT DAYS</th><th>NO. OF DAYS WORKED</th><th>SPECIAL BONUS</th><th>BONUS @" + ds.Tables[0].Rows[ctr]["sal_bonus"] + "%</th></tr>");
                        //break;
                        lc = new LiteralControl("<table border=1>");
                        break;
                    }
                    else if (counter == 17)
                    {
                        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                        lc = new LiteralControl("<table border=1><tr><th colspan=7>LEAVE RECORD</th></tr><tr><th colspan=7>MONTH - " + mfi.GetMonthName(int.Parse(year.Substring(0, 2))).ToString() + "-" + year.Substring(3) + "</th></tr><tr><th>SR NO</th><th>CLIENT</th><th>STATE</th><th>LOCATON</th><th>NAME OF THE EMPLOYEE</th><th>DESIGNATION</th><th>ABSENT/LEAVE</th></tr>");
                        break;
                    }
                    else if (counter == 18)
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR. NO</th><th>EMPLOYEE NAME</th><th>FATHER NAME</th><th>DOB</th><th>DOJ</th><th>LOCATION DEPUTED</th><th>UAN NO</th><th>ESIC NO</th><th>DOL</th><th>REMARK</th></tr>");
                        break;
                    }
                    else
                    {
                        lc = new LiteralControl("<table border=1><tr><th>SR NO</th><th>EMPLOYEE NAME</th><th>EMPLOYEE ID</th><th>DEG</th><th>BLOOD GROUP</th><th>DOJ</th><th>CLIENT NAME</th><th>EMPLOYEE PHOTO</th></tr>");
                        break;
                    }
                case ListItemType.Item:
                    if (counter == 1 || counter == 10)
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Gender"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"] + "</td><td>" + ds.Tables[0].Rows[ctr]["a"] + "</td><td>" + ds.Tables[0].Rows[ctr]["b"] + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["left_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["left_reason"] + "</td><td></td></tr>");
                    }
                    else if (counter == 2)
                    {
                        lc = new LiteralControl("<tr align=center><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Gender"] + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["left_reason"] + "</td></tr>");
                    }
                    else if (counter == 3)
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Gender"] + "</td><td>" + designation + "</td><td>NO OVERTIME</td><td>N/A</td><td>N/A</td><td>N/A</td><td>N/A</td><td>N/A</td><td></td></tr>");
                    }
                    else if (counter == 4)
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + designation + "</td><td>N/A</td><td>NOT GIVEN</td><td>N/A</td><td>N/A</td><td>N/A</td><td>N/A</td><td></td></tr>");
                    }
                    else if (counter == 5)
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + designation + "</td><td>NO FINE</td><td>N/A</td><td>NO</td><td>N/A</td><td>N/A</td><td>N/A</td><td>N/A</td><td></td></tr>");
                    }
                    else if (counter == 6)
                    {
                        lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_father_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"] + "</td><td>N/A</td><td>N/A</td><td>NO</td><td>N/A</td><td>NO</td><td>N/A</td><td>N/A</td><td>N/A</td><td></td></tr>");
                    }
                    else if (counter == 7)
                    {
                        string days = ""; if (month_days == 29) { days = "<td " + (ds.Tables[0].Rows[ctr]["DAY29"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td>"; } else if (month_days == 30) { days = "<td " + (ds.Tables[0].Rows[ctr]["DAY29"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY30"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td>"; } else if (month_days == 31) { days = "<td " + (ds.Tables[0].Rows[ctr]["DAY29"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY29"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY30"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY30"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY31"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY31"] + "</td>"; }
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY01"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY01"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY02"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY02"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY03"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY03"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY04"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY04"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY05"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY05"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY06"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY06"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY07"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY07"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY08"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY08"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY09"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY09"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY10"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY10"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY11"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY11"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY12"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY12"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY13"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY13"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY14"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY14"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY15"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY15"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY16"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY16"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY17"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY17"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY18"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY18"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY19"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY19"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY20"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY20"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY21"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY21"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY22"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY22"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY23"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY23"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY24"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY24"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY25"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY25"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY26"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY26"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY27"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY27"] + "</td><td " + (ds.Tables[0].Rows[ctr]["DAY28"].ToString() != "P" ? "bgcolor=red" : "") + ">" + ds.Tables[0].Rows[ctr]["DAY28"] + "</td>" + days + "<td>" + ds.Tables[0].Rows[ctr]["TOT_DAYS_PRESENT"] + "</td><td></td><td></td></tr>");
                    }
                    else if (counter == 8)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Gender"] + "</td><td>" + ds.Tables[0].Rows[ctr]["FatherName"] + "</td><td>" + ds.Tables[0].Rows[ctr]["birth_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["nationality"] + "</td><td>" + ds.Tables[0].Rows[ctr]["QUALIFICATION_1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["date_of_joining"] + "</td><td>" + ds.Tables[0].Rows[ctr]["designation"] + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["employee_type"] + "</td><td>" + ds.Tables[0].Rows[ctr]["employee_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["uan_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["EMP_NEW_PAN_NO"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["pf_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["esic_number"] + "</td><td>" + ds.Tables[0].Rows[ctr]["lwf"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["adhar_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["bank_account_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bankname"] + "</td><td>" + ds.Tables[0].Rows[ctr]["IFSC_code"] + "</td><td>" + ds.Tables[0].Rows[ctr]["present"] + "</td><td>" + (ds.Tables[0].Rows[ctr]["permanent"].ToString() == ds.Tables[0].Rows[ctr]["present"].ToString() ? "DO" : ds.Tables[0].Rows[ctr]["permanent"]) + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["left_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["reason_of_exit"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_identitymark"] + "</td><td></td><td></td><td></td></tr>");
                    }
                    else if (counter == 9)
                    {
                        lc = new LiteralControl("<tr><td align=center>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td align=center>" + (ctr + 1) + "</td><td align=center></td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["rate_of_wage"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Total_Days_Present"] + "</td><td>" + ds.Tables[0].Rows[ctr]["ot_hours"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["Basic"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["Special_Basic"] + "</td><td></td><td></td><td>'" + ds.Tables[0].Rows[ctr]["ot_amount"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["HRA"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["Bonus"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["sal_bonus_after_gross"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["leave_gross"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["leave_sal_after_gross"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["allow"] + "</td><td>'" + (double.Parse(ds.Tables[0].Rows[ctr]["Bonus"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["leave_gross"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["allow"].ToString())) + "</td><td>'" + ds.Tables[0].Rows[ctr]["total_earnings"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["PF"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["ESIC"] + "</td><td>" + ds.Tables[0].Rows[ctr]["LWF"] + "</td><td>" + ds.Tables[0].Rows[ctr]["PT"] + "</td><td></td><td></td><td></td><td>" + ds.Tables[0].Rows[ctr]["fine"] + "</td><td>" + ds.Tables[0].Rows[ctr]["advance"] + "</td><td>" + (double.Parse(ds.Tables[0].Rows[ctr]["fine"].ToString()) + double.Parse(ds.Tables[0].Rows[ctr]["advance"].ToString())) + "</td><td></td><td>" + ds.Tables[0].Rows[ctr]["total_deduction"].ToString() + "</td><td>" + ds.Tables[0].Rows[ctr]["net_payment"] + "</td><td></td><td></td><td>07/" + payment_date + "</td><td></td></tr>");
                    }
                    else if (counter == 12)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["Igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan=4>Total</td><td>=ROUND(SUM(E2:E" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(F2:F" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(G2:G" + (ctr + 2) + "),2)</td></tr>";
                        }
                    }
                    else if (counter == 13)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade"] + "</td><td>" + ds.Tables[0].Rows[ctr]["gross"] + "</td><td>" + ds.Tables[0].Rows[ctr]["pt_amount"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan=7>Total</td><td>=ROUND(SUM(H2:H" + (ctr + 2) + "),2)</td></tr>";
                        }
                    }
                    else if (counter == 14)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["billing_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["invoice_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["monthyear"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_gst_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["service"] + "</td><td>" + ds.Tables[0].Rows[ctr]["basic"] + "</td><td>" + ds.Tables[0].Rows[ctr]["cgst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["igst"] + "</td><td>" + ds.Tables[0].Rows[ctr]["sgst"] + "</td></tr>");
                        if (ds.Tables[0].Rows.Count == ctr + 1)
                        {
                            lc.Text = lc.Text + "<tr><b><td align=center colspan=8>Total</td><td>=ROUND(SUM(I2:I" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(J2:J" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(K2:K" + (ctr + 2) + "),2)</td><td>=ROUND(SUM(L2:L" + (ctr + 2) + "),2)</td></tr>";
                        }
                    }
                    else if (counter == 15)
                    {
                        lc = new LiteralControl("<tr><td style=font-weight:Calibri;font-size:26px;width:700px;><b>M/S. " + ds.Tables[0].Rows[ctr]["client_name"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>ADDRESS : " + ds.Tables[0].Rows[ctr]["unit_add2"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>NAME : " + ds.Tables[0].Rows[ctr]["emp_name"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>" + ds.Tables[0].Rows[ctr]["document_type"] + " - " + ds.Tables[0].Rows[ctr]["size"] + "</b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>CONTACT NO. : " + ds.Tables[0].Rows[ctr]["emp_mobile_no"] + "</b></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr>");
                    }
                    else if (counter == 16)
                    {
                        lc = new LiteralControl("<tr><td style=font-weight:Calibri;font-size:26px;width:700px;><b>EMP_CODE. " + ds.Tables[0].Rows[ctr]["emp_code"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>Employee Name : " + ds.Tables[0].Rows[ctr]["emp_name"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>Status : " + ds.Tables[0].Rows[ctr]["Status"] + "</b></td></tr><tr><td style=font-weight:Calibri;font-size:18px;width:650px;><b>" + ds.Tables[0].Rows[ctr]["document_type"] + " - " + ds.Tables[0].Rows[ctr]["size"] + "</b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>NO Of Set. : " + ds.Tables[0].Rows[ctr]["no_of_set"] + "  </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Employee Mobile No : " + ds.Tables[0].Rows[ctr]["emp_mobile_no"] + "  </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Employee Branch City : " + ds.Tables[0].Rows[ctr]["unit_city"] + "    </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Employee State : " + ds.Tables[0].Rows[ctr]["state_name"] + "    </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Branch Address1 : " + ds.Tables[0].Rows[ctr]["unit_add1"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Branch Address2 : " + ds.Tables[0].Rows[ctr]["unit_add2"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Client Code : " + ds.Tables[0].Rows[ctr]["client_code"] + "    </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Grade Code : " + ds.Tables[0].Rows[ctr]["grade_code"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Gender : " + ds.Tables[0].Rows[ctr]["gender"] + "    </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>Pincode : " + ds.Tables[0].Rows[ctr]["pincode"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>POD : " + ds.Tables[0].Rows[ctr]["POD"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>POD DATE : " + ds.Tables[0].Rows[ctr]["POD DATE"] + "   </b></td></tr><tr><td  style=font-weight:Calibri;font-size:18px;width:650px;><b>COURIER NAME : " + ds.Tables[0].Rows[ctr]["COURIER NAME"] + "   </b></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr>");

                    }
                    else if (counter == 17)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client"] + "</td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["absent"] + "</td></tr>");

                    }
                    else if (counter == 18)
                    {
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["fathername"] + "</td><td>" + ds.Tables[0].Rows[ctr]["birth_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["date_of_joining"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_city"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["uan_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["esic_number"] + "</td><td>" + ds.Tables[0].Rows[ctr]["left_date"] + "</td><td>" + ds.Tables[0].Rows[ctr]["reason_of_exit"] + "</td></tr>");

                    }
                    else
                    {
                        string image_path = "<td align='center' height=130 width=175><img src= '" + path + "\\" + ds.Tables[0].Rows[ctr]["EMP_PHOTO"].ToString() + "' id='myimage' height='125' width='160' align='middle' />";
                        lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["id_as_per_dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["grade_desc"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_blood_group"] + "</td><td>" + ds.Tables[0].Rows[ctr]["DOJ"] + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td>" + (ds.Tables[0].Rows[ctr]["EMP_PHOTO"].ToString() == "" ? "<td>" : image_path) + "</td></tr>");
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




    protected string get_start_date()
    {
        return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND month = '" + txttodate.Text.Substring(0, 2) + "' AND year = '" + txttodate.Text.Substring(3) + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + ddl_client.SelectedValue + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' limit 1))");
    }

    protected void btn_sales_register_Click(object sender, EventArgs e)
    {
        generate_letter(14);
    }
    protected void btn_bank_images_Click(object sender, EventArgs e)
    {
        string folder_name = "c:/bank_passbook";

        if (!Directory.Exists(folder_name))
        {
            Directory.CreateDirectory(folder_name);
        }
        else
        {
            Directory.Delete(folder_name, recursive: true);
            Directory.CreateDirectory(folder_name);
        }
        generate_passbook(folder_name);
    }




    private void generate_passbook(string folder_name)
    {

        try
        {
            string where = " pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'";

            if (ddl_state.SelectedValue != "ALL") { where = where + "AND pay_employee_master.client_wise_state = '" + ddl_state.SelectedValue + "'"; }
            if (ddlunitselect.SelectedValue != "ALL") { where = where + "AND pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "'"; }

            MySqlCommand cmd = new MySqlCommand("SELECT  CONCAT((SELECT DISTINCT state_code FROM pay_state_master WHERE pay_state_master.state_name = pay_unit_master.state_name), '_', unit_name, '_', replace(pay_employee_master.emp_name,' ','_'),SUBSTRING(bank_passbook,LOCATE('.',bank_passbook))) AS 'file_name', bank_passbook FROM pay_employee_master INNER JOIN pay_images_master ON pay_employee_master.emp_code = pay_images_master.emp_code AND pay_employee_master.comp_code = pay_images_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.comp_code = pay_employee_master.comp_code AND pay_unit_master.unit_code = pay_employee_master.unit_code  WHERE " + where + " and left_date is null and bank_passbook is not null  order by file_name", d.con1);

            d.con1.Open();
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.GetValue(1).ToString().Equals(""))
                    {
                        string path = Server.MapPath("~/EMP_Images");
                        try
                        {
                            //string ext = dr.GetValue(1).ToString().Substring(dr.GetValue(1).ToString().IndexOf("."), (dr.GetValue(1).ToString().Length-(dr.GetValue(1).ToString().IndexOf("."))));

                            File.Copy(path + "\\" + dr.GetValue(1).ToString(), folder_name + "/" + dr.GetValue(0).ToString());

                        }
                        catch { }
                    }
                }
                d.con1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('COMPLETED, Please check folder " + folder_name + " !!')", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
        }
    }

    protected void checklist_gridview(string i) 
    {

        try {
            d.con.Open();

            // for id card grid view 
            System.Data.DataTable dt_id_gv = new System.Data.DataTable();
            MySqlDataAdapter cmd_id_gv = null;

            string where = "";
            if (!ddl_client.SelectedValue.Equals("ALL"))
            {
                where = "and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' ";
            }

            string client_name = "" + ddl_client.SelectedValue + "";

            if (client_name == "Staff")
            {

                if (i == "1")
                {
                    cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name,LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'    AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + "   and (employee_type ='Permanent' ||  employee_type ='Staff' ) AND `document_type` = 'Id_Card' and left_date IS NULL order by 1,2,3,4", d.con);
                }
                else if (i == "2")
                {
                    cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'    AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + "  and (employee_type ='Permanent' ||  employee_type ='Staff' ) AND `document_type` = 'Id_Card' and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
                }
                else if (i == "3")
                {
                    cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1')" + where + "  and (employee_type ='Permanent' ||  employee_type ='Staff' ) AND `document_type` = 'Id_Card' and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
                }
                else if (i == "4")
                {
                    cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'   AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + "  and (employee_type ='Permanent' ||  employee_type ='Staff' )  AND `document_type` = 'Id_Card' and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
                }
            
            }
              else
            {

            if (i == "1")
            {
                cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name,LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + " AND `document_type` = 'Id_Card' and left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "2")
            {
                cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + " and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' AND `document_type` = 'Id_Card' AND left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "3")
            {
                cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal' AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1')" + where + " and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "' AND `document_type` = 'Id_Card'  and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "4")
            {
                cmd_id_gv = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name,pay_unit_master.unit_code, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo,id_card_dispatch_date FROM pay_employee_master  INNER JOIN `pay_document_details` ON `pay_employee_master`.`comp_code` = `pay_document_details`.`comp_code` AND `pay_employee_master`.`client_code` = `pay_document_details`.`client_code` AND `pay_employee_master`.`emp_code` = `pay_document_details`.`emp_code` INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND (`ID_CARD_DISPATCH` = '0' || `ID_CARD_DISPATCH` = '1') " + where + " and EMP_CURRENT_STATE='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "' AND `document_type` = 'Id_Card' AND left_date IS NULL order by 1,2,3,4", d.con);
            }


            }

            
            cmd_id_gv.Fill(dt_id_gv);
            //appro_emp_legal = "0";
            if (dt_id_gv.Rows.Count > 0)
            {
                //ViewState["appro_emp_legal"] = dt_id_gv.Rows.Count.ToString();
                //appro_emp_legal = ViewState["appro_emp_legal"].ToString();

                gv_checklist_uniform.DataSource = dt_id_gv;
                gv_checklist_uniform.DataBind();


            }
            dt_id_gv.Dispose();
        
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    
    }


    protected void employee_status(string i)
    {
        d.con.Open();
        try
        {


            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            panel_link.Visible = true;



            System.Data.DataTable dt_item = new System.Data.DataTable();

            //approve by admin
            gv_appro_emp_count.DataSource = null;
            gv_appro_emp_count.DataBind();
            //  dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_state.SelectedValue, 1);
            //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select UNIT_CODE, LOCATION, client_code, comp_code FROM  pay_employee_master WHERE comp_code = '" + Session["comp_code"].ToString + "' AND legal_flag = 2    AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'");
            MySqlDataAdapter cmd_item = null;

            //string client_name = "" + ddl_client.SelectedValue + "";

            //if (client_name == "Staff")
            //{

            //    if (i == "1")
            //    {
            //        cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'", d.con);
            //    }
            //    else if (i == "2")
            //    {
            //        cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "'", d.con);
            //    }
            //    else if (i == "3")
            //    {
            //        cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "'", d.con);
            //    }
            //    else if (i == "4")
            //    {
            //        cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'   AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "'", d.con);
            //    }

            //}
            //else
            //{

            //string client_name = "" + ddl_client.SelectedValue + "";

            //if (client_name != "Staff")
            //{


                if (i == "1")
                {
                    cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 1  AND ap_status = 'Approve By Admin' AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'", d.con);
                }
                else if (i == "2")
                {
                    cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 1  AND ap_status = 'Approve By Admin' AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "'", d.con);
                }
                else if (i == "3")
                {
                    cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 1  AND ap_status = 'Approve By Admin' AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "'", d.con);
                }
                else if (i == "4")
                {
                    cmd_item = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 1  AND ap_status = 'Approve By Admin' AND left_date IS NULL   AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "'", d.con);
                }
      //    }
            appro_emp_count = "0";
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_count"] = dt_item.Rows.Count.ToString();
                appro_emp_count = ViewState["appro_emp_count"].ToString();

                gv_appro_emp_count.DataSource = dt_item;
                gv_appro_emp_count.DataBind();

            }
            dt_item.Dispose();

            //approve by legal
            System.Data.DataTable dt_item3 = new System.Data.DataTable();
            System.Data.DataTable dt_id_gv = new System.Data.DataTable();

            gv_appro_emp_legal.DataSource = null;
            gv_appro_emp_legal.DataBind();
            MySqlDataAdapter cmd_item1 = null;
            
            string where = "";
            if (!ddl_client.SelectedValue.Equals("ALL"))
            {
                where = "and pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' ";
            }

                
            //string client_name1 = "" + ddl_client.SelectedValue + "";

            //if (client_name1 == "Staff")
            //{

            //    if (i == "1")
            //    {
            //        cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name,LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND id_card_dispatch_date IS NULL "+where+" and left_date IS NULL order by 1,2,3,4", d.con);
            //    }
            //    else if (i == "2")
            //    {
            //      cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            //    }
            //    else if (i == "3")
            //    {
            //       cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'    AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            //    }
            //    else if (i == "4")
            //    {
            //           cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'   AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            //    }
            //}

           // else{

            //string client_name1 = "" + ddl_client.SelectedValue + "";

            //if (client_name1 != "Staff")
            //{

            if (i == "1")
            {
                cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name,LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code  INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND id_card_dispatch_date IS NULL "+where+" and left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "2")
            {
                cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "3")
            {
                cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            }
            else if (i == "4")
            {
                cmd_item1 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,pay_employee_master.emp_code, pay_images_master.original_photo FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_images_master ON pay_employee_master.comp_code = pay_images_master.comp_code AND pay_employee_master.emp_code = pay_images_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND legal_flag = 2  AND ap_status = 'Approve By Leagal'  AND reject_reason = 'Approve By Leagal'  AND id_card_dispatch_date IS NULL " + where + " and `LOCATION`='" + ddl_state.SelectedValue + "'and  pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "' AND left_date IS NULL order by 1,2,3,4", d.con);
            }   
            
    //   }
            cmd_item1.Fill(dt_item3);
            appro_emp_legal = "0";
            if (dt_item3.Rows.Count > 0)
            {
                ViewState["appro_emp_legal"] = dt_item3.Rows.Count.ToString();
                appro_emp_legal = ViewState["appro_emp_legal"].ToString();

                gv_appro_emp_legal.DataSource = dt_item3;
                gv_appro_emp_legal.DataBind();

               // gv_checklist_uniform.DataSource = dt_item3;
              // gv_checklist_uniform.DataBind();

            }
            dt_item3.Dispose();

            gv_reject_emp_legal.DataSource = null;
            gv_reject_emp_legal.DataBind();



            // Dispatched dates
            System.Data.DataTable dt_item2 = new System.Data.DataTable();
            gv_reject_emp_legal.DataSource = null;
            gv_reject_emp_legal.DataBind();
            MySqlDataAdapter cmd_item2 = null;

               string client_name2 = "" + ddl_client.SelectedValue + "";

               if (client_name2 == "Staff")
               {

                   if (i == "1")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND id_card_dispatch_date IS NOT NULL and (employee_type = 'Permanent' ||employee_type = 'Staff') AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'", d.con);
                   }
                   else if (i == "2")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and (employee_type = 'Permanent' ||employee_type = 'Staff') and `LOCATION`='" + ddl_state.SelectedValue + "'", d.con);
                   }
                   else if (i == "3")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and (employee_type = 'Permanent' ||employee_type = 'Staff') and `LOCATION`='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "'", d.con);
                   }
                   else if (i == "4")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "'  AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and (employee_type = 'Permanent' ||employee_type = 'Staff') and `LOCATION`='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "'", d.con);
                   }
               
               }
               else
               {

                   if (i == "1")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = 2   AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "'", d.con);
                   }
                   else if (i == "2")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = 2   AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "'", d.con);
                   }
                   else if (i == "3")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = 2   AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "'", d.con);
                   }
                   else if (i == "4")
                   {
                       cmd_item2 = new MySqlDataAdapter("SELECT pay_client_master.client_name, LOCATION, pay_unit_master.unit_name, pay_employee_master.EMP_NAME,id_card_dispatch_date FROM pay_employee_master INNER JOIN pay_client_master ON pay_employee_master.comp_code = pay_client_master.comp_code AND pay_employee_master.client_code = pay_client_master.client_code INNER JOIN pay_unit_master ON pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.unit_code = pay_unit_master.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND legal_flag = 2   AND id_card_dispatch_date IS NOT NULL AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' and `LOCATION`='" + ddl_state.SelectedValue + "' and pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.GRADE_CODE = '" + ddl_designation.SelectedValue + "'", d.con);
                   }

               }
            cmd_item2.Fill(dt_item2);
            reject_emp_legal = "0";
            if (dt_item2.Rows.Count > 0)
            {
                ViewState["reject_emp_legal"] = dt_item2.Rows.Count.ToString();
                reject_emp_legal = ViewState["reject_emp_legal"].ToString();

                gv_reject_emp_legal.DataSource = dt_item2;
                gv_reject_emp_legal.DataBind();

            }
            dt_item2.Dispose();




            appro_emp_count = ViewState["appro_emp_count"].ToString();
            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
            reject_emp_legal = ViewState["reject_emp_legal"].ToString();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }



    protected void gv_appro_emp_count_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
    }
    protected void docgv_fill()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        // MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_document_details.ID,pay_document_details.emp_code,EMP_NAME, GRADE_DESC, EMP_MOBILE_NO, document_type, size from pay_employee_master  INNER JOIN pay_document_details ON pay_employee_master.comp_code = pay_document_details.comp_code AND pay_employee_master.client_code = pay_document_details.client_code AND pay_employee_master.emp_code = pay_document_details.emp_code INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.client_wise_state = '" + ddl_state.SelectedValue + "' AND pay_employee_master.legal_flag = '2' AND (left_date IS NULL OR left_date = '') group by  document_type", d.con);
        MySqlDataAdapter adp = new MySqlDataAdapter("SELECT pay_document_details.ID,pay_document_details.emp_code,EMP_NAME, GRADE_DESC, EMP_MOBILE_NO, CAST(GROUP_CONCAT(CASE WHEN document_type = 'ID_Card' THEN document_type END) AS char) AS 'Id_card',CAST(GROUP_CONCAT(CASE WHEN document_type = 'Shoes' THEN document_type END) AS char) AS 'Shoes',CAST(GROUP_CONCAT(CASE WHEN document_type = 'Uniform' THEN document_type END) AS char) AS 'uniform',CAST(GROUP_CONCAT(CASE WHEN document_type = 'Apron' THEN document_type END) AS char) AS 'Apron',CAST(GROUP_CONCAT(CASE WHEN document_type = 'Shoes' THEN size END) AS char) AS 'shoes_size',CAST(GROUP_CONCAT(CASE WHEN document_type = 'Uniform' THEN size END) AS char) AS 'Uniform_Size',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'Uniform' THEN `no_of_set` END) AS char),0) AS 'unifrom_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN document_type = 'Uniform' THEN admin_no_of_set END) AS char),0)AS 'Uniform_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'Uniform' THEN `remaining_no_set` END) AS char),0 )AS 'remaning_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'Shoes' THEN `no_of_set` END) AS char),0) AS 'shoes_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'Shoes' THEN `admin_no_of_set` END) AS char),0)AS 'Shoes_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'Shoes' THEN `remaining_no_set` END) AS char),0)AS 'shoes_remaning_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'ID_Card' THEN `no_of_set` END) AS char),0) AS 'id_card_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'ID_Card' THEN `admin_no_of_set` END) AS char),0)AS 'id_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN `document_type` = 'ID_Card' THEN `remaining_no_set` END) AS char),0) AS 'id_remaning_No_of_set',COALESCE(CAST(GROUP_CONCAT(CASE WHEN document_type = 'Apron' THEN size END) AS char),0) AS 'Apron_Size',COALESCE(CAST(GROUP_CONCAT(CASE WHEN document_type = 'Apron' THEN No_of_set END) AS char) ,0)AS 'Apron_No_of_set',no_of_set from pay_employee_master  INNER JOIN pay_document_details ON pay_employee_master.comp_code = pay_document_details.comp_code AND pay_employee_master.client_code = pay_document_details.client_code AND pay_employee_master.emp_code = pay_document_details.emp_code INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND pay_employee_master.unit_code = '" + ddlunitselect.SelectedValue + "' and pay_employee_master.client_wise_state = '" + ddl_state.SelectedValue + "' AND pay_employee_master.legal_flag = '2' AND (left_date IS NULL OR left_date = '') group by   pay_document_details.emp_code", d.con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        grd_document.DataSource = ds.Tables[0];
        grd_document.DataBind();
    }

    protected void grd_document_RowDataBound(object sender, GridViewRowEventArgs e)
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
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["Shoes"].ToString() == "")
            {
                System.Web.UI.WebControls.TextBox shoes = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_shoes");
                shoes.ReadOnly = true;
            }       
            if (dr["Uniform"].ToString() == "")
            {
                System.Web.UI.WebControls.TextBox uniform = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_uniform");
                uniform.ReadOnly = true;
            }
            if (dr["Uniform"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox uniform = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_uniform_set");
                uniform.ReadOnly = true;
            }

            if (dr["Shoes"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox shoes = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_shoes_set");
                shoes.ReadOnly = true;
            }

            if (dr["ID_Card"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox id_card = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_id_card_set");
                id_card.ReadOnly = true;
            }

            if (dr["remaning_No_of_set"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox uniform_remaining = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_uniform_set_remainig");
                uniform_remaining.ReadOnly = true;
            }

            if (dr["Shoes_remaning_No_of_set"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox shoes_remaining = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_shoes_set_remainig");
                shoes_remaining.ReadOnly = true;
            }


            if (dr["id_remaning_No_of_set"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox id_remaining = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_id_set_remainig");
                id_remaining.ReadOnly = true;
            }

            if (dr["EMP_MOBILE_NO"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox contact = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_cont_no");
                contact.ReadOnly = true;
            }

            if (dr["shoes_size"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox contact = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_shoes");
                contact.ReadOnly = false;
            }


            if (dr["Uniform_Size"].ToString() != "")
            {
                System.Web.UI.WebControls.TextBox contact = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_uniform");
                contact.ReadOnly = false;
            }



            if (dr["remaning_No_of_set"].ToString() == "0")
            {
                System.Web.UI.WebControls.DropDownList uniform_readonly = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddl_uniform_set");
                uniform_readonly.Enabled = false;
            }

          

            if (dr["Shoes_remaning_No_of_set"].ToString() == "0")
            {
                System.Web.UI.WebControls.DropDownList shoes_readonly = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddl_shoes_set");
                shoes_readonly.Enabled = false;
            }

            if (dr["id_remaning_No_of_set"].ToString() == "0")
            {
                System.Web.UI.WebControls.DropDownList id_readonly = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddl_id_card_set");
                id_readonly.Enabled = false;
            }




            if (dr["Apron"].ToString() != "" || dr["Apron"].ToString() == "")
            {
                if (dr["Apron_Size"].ToString() != "")
                {
                    System.Web.UI.WebControls.DropDownList ddl_apro = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("txt_apron");
                    ddl_apro.SelectedValue = dr["Apron_Size"].ToString();
                }
                else
                {
                    System.Web.UI.WebControls.DropDownList ddl_apro = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("txt_apron");
                    ddl_apro.Items.Clear();
                    ddl_apro.Enabled = false;
                }
            }
            if (dr["Apron"].ToString() == "")
            {
                System.Web.UI.WebControls.TextBox apron = (System.Web.UI.WebControls.TextBox)e.Row.FindControl("txt_apron_set");
                apron.ReadOnly = true;
            }
        }
    }

    protected void grd_document_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_document.UseAccessibleHeader = false;
            grd_document.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void lnkinv_update_Click(object sender, EventArgs e)
    {


        GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        //  string id = row.Cells[1].Text;
        string emp_id = row.Cells[1].Text;
        string shoes = (row.FindControl("txt_shoes") as System.Web.UI.WebControls.TextBox).Text;
        string uniform = (row.FindControl("txt_uniform") as System.Web.UI.WebControls.TextBox).Text;
        string apron = (row.FindControl("txt_apron") as System.Web.UI.WebControls.DropDownList).Text;
        string contact_no = (row.FindControl("txt_cont_no") as System.Web.UI.WebControls.TextBox).Text;
        //for uniform
        string uniform_set = (row.FindControl("ddl_uniform_set") as System.Web.UI.WebControls.DropDownList).Text;
        string apron_set = (row.FindControl("txt_apron_set") as System.Web.UI.WebControls.TextBox).Text;
        string remaining_set = (row.FindControl("txt_uniform_set_remainig") as System.Web.UI.WebControls.TextBox).Text;
        int remaining_uniform = Convert.ToInt16(remaining_set) - Convert.ToInt16(uniform_set);

        // for shoes

        string shoes_set = (row.FindControl("ddl_shoes_set") as System.Web.UI.WebControls.DropDownList).Text;
        string shoes_remaining_set = (row.FindControl("txt_shoes_set_remainig") as System.Web.UI.WebControls.TextBox).Text;

        int remaining_shoes = 0;

        if (shoes_remaining_set != "0")
        {
             remaining_shoes = Convert.ToInt16(shoes_remaining_set) - Convert.ToInt16(shoes_set);
        }
        else if (shoes_remaining_set == "0") {
            remaining_shoes = Convert.ToInt16(shoes_remaining_set);
           
        }
       //  for id_card

        string id_card_set = (row.FindControl("ddl_id_card_set") as System.Web.UI.WebControls.DropDownList).Text;
        string id_remaining_set = (row.FindControl("txt_id_set_remainig") as System.Web.UI.WebControls.TextBox).Text;
        int remaining_id = 0;
        if (id_remaining_set != "0")
        {
             remaining_id = Convert.ToInt16(id_remaining_set) - Convert.ToInt16(id_card_set);
        }
        else if(id_remaining_set == "0"){

             remaining_id = Convert.ToInt16(id_remaining_set);
        }

       
        string cc = contact_no.Length.ToString();
        if (cc != "10")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Valid Mobile No ..!!');", true);
            return;
        }
        //Validation For Shoes_Size
        if ((row.FindControl("txt_shoes") as System.Web.UI.WebControls.TextBox).ReadOnly==false)
        {
            if (shoes == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Shoes size ..!!');", true);
                return;
            }
        }
       
        //Validation For Uniform_Size
        if ((row.FindControl("txt_uniform") as System.Web.UI.WebControls.TextBox).ReadOnly == false)
        {
            if (uniform == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Uniform size ..!!');", true);
                return;
            }
        }

           
        //Validation For Uniform_Set 21-12-19
        if ((row.FindControl("ddl_uniform_set") as System.Web.UI.WebControls.DropDownList).Enabled== true)
        {
            if (uniform_set == "0" && remaining_set != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Uniform set ..!!');", true);
                return;
            }
        }

        // Validation for shoes set
        if ((row.FindControl("ddl_shoes_set") as System.Web.UI.WebControls.DropDownList).Enabled == true)
        {
            if (shoes_set == "0" && shoes_remaining_set != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Shoes set ..!!');", true);
                return;
            }
        }


        // Validation for shoes set
        if ((row.FindControl("ddl_id_card_set") as System.Web.UI.WebControls.DropDownList).Enabled == true)
        {
            if (id_card_set == "0" && id_remaining_set != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter ID_Card set ..!!');", true);
                return;
            }
        }


        foreach (GridViewRow r in grd_document.Rows)
        {
            
           //string no_uniform = r.Cells[8].Text;
           // string remaning_no = r.Cells[9].Text;
           // // string emp_code = r.Cells[10].Text;
            int unifrom_set_recp = int.Parse(uniform_set);
            int remaining_set_table = int.Parse(remaining_set);


            if (unifrom_set_recp > remaining_set_table)
            { 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Correct Uniform Set');", true);
                return;
            
            
            }

        }

      //////////////////

        
        //Validation For Apron_Set
        if ((row.FindControl("txt_apron_set") as System.Web.UI.WebControls.TextBox).ReadOnly == false)
        {
            if (apron_set == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Apron set ..!!');", true);
                return;
            }
        }
        

        int result = d.operation("UPDATE pay_document_details SET size ='" + uniform + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Uniform'");
        result = d.operation("UPDATE pay_document_details SET size ='" + shoes + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Shoes'");
        result = d.operation("UPDATE pay_document_details SET size ='" + apron + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Apron'");
        
        //for uniform,shoes,id 21-12-19



        result = d.operation("UPDATE pay_document_details SET No_of_set ='" + uniform_set + "', remaining_no_set = '" + remaining_uniform + "',uniform_no_flag='" + uniform_set + "'  WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Uniform'");
   
        result = d.operation("UPDATE pay_document_details SET No_of_set ='" + shoes_set + "', remaining_no_set = '" + remaining_shoes + "'  WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Shoes'");
        
        result = d.operation("UPDATE pay_document_details SET No_of_set ='" + id_card_set + "', remaining_no_set = '" + remaining_id + "'  WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='ID_Card'");
        
        
        result = d.operation("UPDATE pay_document_details SET No_of_set ='" + apron_set + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND CLIENT_CODE = '" + ddl_client.SelectedValue + "' and unit_code = '" + ddlunitselect.SelectedValue + "' and emp_code = '" + emp_id + "' and document_type='Apron'");
        result = d.operation("UPDATE pay_employee_master SET EMP_MOBILE_NO ='" + contact_no + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND  emp_code = '" + emp_id + "'");
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Document Details Updated Succsefully ..!!');", true);

            docgv_fill();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Details Not Updated  ..!!');", true);
            docgv_fill();
        }
    }
    protected void gv_checklist_uniform_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[5].Visible = false;


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl = "";
            if (dr["original_photo"].ToString() != "")
            {

                imageUrl = "~/EMP_Images/" + dr["original_photo"];
                (e.Row.FindControl("original_photo") as Image).ImageUrl = imageUrl;

            }
        }

    }

    protected void gv_appro_emp_legal_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[4].Visible = false;
    }

    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        employee_status("4");
        checklist_gridview("4");
    }
    protected void btn_id_resend_Click(object sender, EventArgs e)
    {
        try
        {d6.con.Open();

        string resend_id = d.getsinglestring("select distinct `emp_code` from pay_id_card_resend where comp_code= '" + Session["comp_code"].ToString() + "' and client_code= '" + ddl_client.SelectedValue + "'");

        if (resend_id=="") 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Number of dublicate id card set in employee master.. !!!');", true);
            return;
        
        }


        string inlist = null; string inlist1 = null; string emp_name = null;
        string unit_code = null; string inlist2 = null; string state_name = null; string inlist3 = null;
        foreach (GridViewRow gvrow in gv_dublicate_id.Rows)
        {

          
            string emp_code = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[5].Text;
             emp_name = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[4].Text;
             unit_code = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[6].Text;
             state_name = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[2].Text;
          
            var checkbox = gvrow.FindControl("chk_client_dublicate") as System.Web.UI.WebControls.CheckBox;
            if (checkbox.Checked == true)
            {

                inlist = inlist + "'" + emp_code + "'";
                inlist1 = inlist1 + ", " + emp_name + " ";
               
               // inlist1 = inlist1.Replace("  ", ",");

                inlist2 = inlist2 + "'" + unit_code + "'";
                inlist3 = inlist3 + "'" + state_name + "'";
               

            }

        }

        string original_id = d.getsinglestring("select emp_code from pay_dispatch_billing where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code  IN (" +inlist +") and client_code = '" + ddl_client.SelectedValue + "' and unit_code in("+inlist2+") and state_dispatch in ("+inlist3+")");

        if (original_id == "")

        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
            //return; 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('this employee Original id card not Dispatched :" + inlist1 + " ');", true);
            return;


        }
        else
        {
            multiple_id_genrated();
        }

        }
        catch (Exception ex) { throw ex; }
        finally {
            d6.con.Close(); 
        }
    }


    protected void id_card_date_update() {

        try
        {

            d5.con.Open();

            // multiple_id_genrated();

            string inlist = ""; string emp_name = null; string item_emp = null; string inlist1 = ""; string state_name = null; string unit_code_id = null; string state_name_id = null; string unit_list = null;
            //foreach (GridViewRow gvrow in gv_checklist_uniform.Rows)
            //{
            //    string emp_code = gv_checklist_uniform.Rows[gvrow.RowIndex].Cells[5].Text; 
            //}
            foreach (GridViewRow gvrow in gv_dublicate_id.Rows)
            {

                // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
                string emp_code = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[5].Text;
                emp_name = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[4].Text;
                unit_code_id = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[6].Text;
                state_name_id = gv_dublicate_id.Rows[gvrow.RowIndex].Cells[2].Text;

                var checkbox = gvrow.FindControl("chk_client_dublicate") as System.Web.UI.WebControls.CheckBox;
                if (checkbox.Checked == true)
                {

                    inlist = inlist + "'" + emp_code + "',";
                    unit_list = unit_list + "'" + unit_code_id + "'";
                    state_name = state_name + "'" + state_name_id + "'";
                    inlist1 = inlist;
                    item_emp = inlist1;

                }

            }

            if (inlist.Length > 0)
            {
                inlist = inlist.Substring(0, inlist.Length - 1);

            }
            else { inlist = "''"; }


            string id_resend = d.getsinglestring("SELECT group_concat(emp_code) FROM `pay_employee_master` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "' and `client_wise_state`= " + state_name + "  AND `client_code` = '" + ddl_client.SelectedValue + "' AND `unit_code` = " + unit_list + " AND `legal_flag` = '2'  AND (`pay_employee_master`.`employee_type` = 'Permanent' || `pay_employee_master`.`employee_type` = 'Staff')  AND `id_card_dispatch` = '1' AND `left_date` IS NULL  AND `emp_code` IN (" + inlist + ")");

            if (id_resend == "")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('this employee first id card not genrated :" + emp_name + " ');", true);
                //ddl_state_name.SelectedValue = "Select";
                return;


            }
            else if (id_resend != "")
            {

                try
                {
                    string id_second = null; string id_third = null;
                    string[] invoice_ship_add = item_emp.Split(',');
                    foreach (object obj_id in invoice_ship_add)
                    {
                        if (obj_id != "")
                        {
                            //second id card
                            id_second = d.getsinglestring("SELECT `field2` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " AND `emp_code` = " + obj_id + " ");

                            if (id_second == "")
                            {

                                int res2;
                                res2 = d.operation("update pay_id_card_resend set field2 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " AND `emp_code` = " + obj_id + "");

                                if (res2 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }
                            }



                            // third id card
                            id_third = d.getsinglestring("SELECT `field3` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " AND `emp_code` = " + obj_id + " ");

                            if (id_third == "")
                            {

                                int res3;

                                res3 = d.operation("update pay_id_card_resend set field3 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res3 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }


                            // fourth id card

                            string id_fourth = d.getsinglestring("SELECT `field4` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " AND `emp_code` = " + obj_id + " ");

                            if (id_fourth == "")
                            {

                                int res4;

                                res4 = d.operation("update pay_id_card_resend set field4 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = " + unit_list + " and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res4 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // fifth id card 

                            string id_fifth = d.getsinglestring("SELECT `field5` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_fifth == "")
                            {

                                int res5;

                                res5 = d.operation("update pay_id_card_resend set field5 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res5 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // sixth id card 


                            string id_sixth = d.getsinglestring("SELECT `field6` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_sixth == "")
                            {

                                int res6;

                                res6 = d.operation("update pay_id_card_resend set field6 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res6 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // seventh id card 

                            string id_seventh = d.getsinglestring("SELECT `field7` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_seventh == "")
                            {

                                int res7;

                                res7 = d.operation("update pay_id_card_resend set field7 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res7 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // 8 id card

                            string id_eighth = d.getsinglestring("SELECT `field8` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_eighth == "")
                            {

                                int res8;

                                res8 = d.operation("update pay_id_card_resend set field8 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res8 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // 9 id card 

                            string id_nine = d.getsinglestring("SELECT `field9` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_nine == "")
                            {

                                int res9;

                                res9 = d.operation("update pay_id_card_resend set field9 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res9 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }

                            // tenth id card

                            string id_ten = d.getsinglestring("SELECT `field10` FROM `pay_id_card_resend` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' AND `emp_code` = " + obj_id + " ");

                            if (id_ten == "")
                            {

                                int res10;

                                res10 = d.operation("update pay_id_card_resend set field10 = NOW() where  `comp_code` = '" + Session["comp_code"].ToString() + "'  AND `client_code` = '" + ddl_client.SelectedValue + "'  AND `unit_code` = '" + ddlunitselect.SelectedValue + "' and field2 is not null AND `emp_code` = " + obj_id + "  ");
                                if (res10 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Id Card Added Successfully... !!!');", true);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Id Card Added Failed... !!!');", true);
                                }

                            }


                        }
                    }
                }
                catch (Exception ex) { throw ex; }
                finally { }


            }


        }
        catch (Exception ex) { throw ex; }
        finally { d5.con.Close(); }

    
    
    }

    protected void dublicate_id()
    {

        try
        {
            MySqlDataAdapter dublicate_id_card = null;
            dublicate_id_card = new MySqlDataAdapter("SELECT `client_name`, `unit_name`,pay_id_card_resend.`emp_code`, `emp_name`, pay_id_card_resend.`state_name`, `id_no_set`,pay_id_card_resend.unit_code `from_date`, `to_date`,pay_id_card_resend.unit_code FROM `pay_id_card_resend`  INNER JOIN `pay_client_master` ON `pay_id_card_resend`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_id_card_resend`.`client_code` = `pay_client_master`.`client_code`  INNER JOIN `pay_unit_master` ON `pay_id_card_resend`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_id_card_resend`.`unit_code` = `pay_unit_master`.unit_code  INNER JOIN `pay_employee_master` ON `pay_id_card_resend`.`comp_code` = `pay_employee_master`.`comp_code` AND `pay_id_card_resend`.`emp_code` = `pay_employee_master`.`emp_code`WHERE  pay_id_card_resend.`comp_code` = '" + Session["comp_code"].ToString() + "' AND pay_id_card_resend.`client_code` = '" + ddl_client.SelectedValue + "' ", d4.con);

            d4.con.Open();


            System.Data.DataTable dt_item_id = new System.Data.DataTable();

            dublicate_id_card.Fill(dt_item_id);
            if (dt_item_id.Rows.Count > 0)
            {

                gv_dublicate_id.DataSource = dt_item_id;
                gv_dublicate_id.DataBind();


            }
        }
        catch (Exception ex) { throw ex; }
        finally { d4.con.Close(); }
    
    

    
    }


    protected void btn_dublicate_id_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        Panel_first_id.Visible = false;
        btn_id_resend.Visible = true;
       
        btn_clientwise_all_employee_id.Visible = false;
        dublicate_id();
        btn_dublicate_id.Visible = false;
    }
    protected void gv_dublicate_id_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridService, "Select$" + e.Row.RowIndex);
        }
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;

    }
}
