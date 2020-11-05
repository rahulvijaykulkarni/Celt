using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;


public partial class leagal_approval : System.Web.UI.Page
{
    DAL d = new DAL();
    public string rem_emp_count = "0", appro_emp_count = "0", appro_emp_legal = "0", reject_emp_legal = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
visibility();
           
            ViewState["rem_emp_count"] = 0;
            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            rem_emp_count = ViewState["rem_emp_count"].ToString();
            appro_emp_count = ViewState["appro_emp_count"].ToString();
            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
            reject_emp_legal = ViewState["reject_emp_legal"].ToString();

            panel_link.Visible = false;
            panel_data.Visible = false;
            client_list();

            
        }

        rem_emp_count = ViewState["rem_emp_count"].ToString();
        appro_emp_count = ViewState["appro_emp_count"].ToString();
        appro_emp_legal = ViewState["appro_emp_legal"].ToString();
        reject_emp_legal = ViewState["reject_emp_legal"].ToString();

     }

    public void client_list()
    {
        d.con1.Close();
        ddl_emp_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT pay_client_master.client_code, client_NAME FROM pay_client_master INNER JOIN pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE and pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ") and client_active_close = '0'", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client.DataSource = dt_item;
                ddl_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_client.DataBind();


            }
            dt_item.Dispose();
            d.con1.Close();
            ddl_client.Items.Insert(0, "Select");
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
          //  employee_status_all();

        }

    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_emp_name.Items.Clear();
        if (ddl_client.SelectedValue != "Select")
        {

            //State
            ddl_state.Items.Clear();
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_client_state_role_grade where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' and EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") ", d.con);

            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
                ddl_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_unitcode.Items.Clear();
                employee_status(); 
               // panel_link.Visible = false;
                panel_data.Visible = false;
            }
        }

      
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_emp_name.Items.Clear();
        ddl_unitcode.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) as UNIT_NAME, unit_code,flag from pay_unit_master where state_name = '" + ddl_state.SelectedValue + "' and comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' AND UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE in (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client.SelectedValue + "') AND branch_status = 0  ORDER BY 1", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
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
            //vikas
            ddl_unitcode.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
         
            d.con.Close();
            employee_status();
          //  panel_link.Visible = true;
            panel_data.Visible = false;
          
        }
    }
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e) 
    {
        ddl_emp_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct emp_code,emp_name from pay_employee_master where comp_code='"+Session["COMP_CODE"].ToString()+"' and unit_code='"+ddl_unitcode.SelectedValue+"' and employee_type='Permanent' and left_date is null and legal_flag !=  0 order by emp_name", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_emp_name.DataSource = dt_item;
                ddl_emp_name.DataTextField = dt_item.Columns[1].ToString();
                ddl_emp_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_emp_name.DataBind();
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Branch Employee Can Not Approved By Admin!!.');", true);
            }
            dt_item.Dispose();
            d.con.Close();
            //vikas
            ddl_emp_name.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
           
            panel_data.Visible = false;
        }
    }
    protected void ddl_emp_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (ddl_emp_name.SelectedValue == "Select")
            {
                panel_data.Visible = false;
                return;
            }
            d.con.Open();
            MySqlCommand cmd_2 = new MySqlCommand("select P_TAX_NUMBER,PF_BANK_NAME,original_bank_account_no,PF_IFSC_CODE,EMP_MOBILE_NO,emp_name,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d-%m-%Y'),date_format(JOINING_DATE,'%d-%m-%Y'),(case when gender = 'M' then 'Male'  when gender = 'F' then 'Female' else '' end) as gender,PAN_NUMBER,ESIC_NUMBER,PF_NUMBER,GRADE_DESC,reject_reason from pay_employee_master inner join pay_grade_master on pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE AND  pay_employee_master.comp_code= pay_grade_master.comp_code where  pay_employee_master.comp_code='" + Session["COMP_CODE"].ToString() + "' and pay_employee_master.emp_code ='" + ddl_emp_name.SelectedValue + "'", d.con);
           // MySqlCommand cmd_2 = new MySqlCommand("select P_TAX_NUMBER,PF_BANK_NAME,original_bank_account_no,PF_IFSC_CODE,EMP_MOBILE_NO,emp_name,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d-%m-%Y'),date_format(JOINING_DATE,'%d-%m-%Y'),gender,PAN_NUMBER,ESIC_NUMBER,PF_NUMBER,GRADE_CODE from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code ='" + ddl_emp_name.SelectedValue + "'", d.con);
            MySqlDataReader dr_2 = cmd_2.ExecuteReader();
            if (dr_2.Read())
            {
                txt_adhar_no.Text = dr_2.GetValue(0).ToString();
                txt_bank_name.Text = dr_2.GetValue(1).ToString();
                txt_acc_no.Text = dr_2.GetValue(2).ToString();
                txt_ifsc_no.Text = dr_2.GetValue(3).ToString();
                txt_mb_no.Text = dr_2.GetValue(4).ToString();

                txt_emp_name.Text = dr_2.GetValue(5).ToString();
                txt_father_name.Text = dr_2.GetValue(6).ToString();
                txt_dob.Text = dr_2.GetValue(7).ToString();
                txt_doj.Text = dr_2.GetValue(8).ToString();
                txt_gender.Text = dr_2.GetValue(9).ToString();

                txt_uan_no.Text = dr_2.GetValue(10).ToString();
                txt_esic_no.Text = dr_2.GetValue(11).ToString();
                txt_pf_no.Text = dr_2.GetValue(12).ToString();
                txt_designation.Text = dr_2.GetValue(13).ToString();
                txt_reject.Text = dr_2.GetValue(14).ToString();
                dr_2.Close();
                d.con.Close();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally {
            link_originalphoto.Visible = false;
            link_adharphoto.Visible = false;
            link_joining.Visible = false;
            link_signature.Visible = false;
            link_addr_proof.Visible = false;
            link_police.Visible = false;
            link_pan_card.Visible = false;
            d.con.Close();
            string aprov = d.getsinglestring("select legal_flag from pay_employee_master where emp_code='" + ddl_emp_name.SelectedValue+ "'");
            if (aprov=="2")
            {
                //btn_approve.Visible = false;
            }
        }



        try
        {
            d.con.Open();
            MySqlCommand cmd_2 = new MySqlCommand("select original_photo,original_adhar_card,bank_passbook,emp_signature,original_address_proof,original_policy_document,EMP_ADHAR_PAN from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code ='" + ddl_emp_name.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd_2.ExecuteReader();
          
            // Profile Photo(Passport Size) :
            while (dr.Read())
            {
                if (dr.GetValue(0).ToString() != "")
                {
                    ImageButton1.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();

                    var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_originalphoto.Visible = true;
                        ImageButton1.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else {
                    ImageButton1.ImageUrl = "~/Images/placeholder.png";
                }
                //  Adhar Photo :

                if (dr.GetValue(1).ToString() != "")
                {
                    ImageButton2.ImageUrl = "~/EMP_Images/" + dr.GetValue(1).ToString();
                    var result = dr.GetValue(1).ToString().Substring(dr.GetValue(1).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_adharphoto.Visible = true;
                        ImageButton2.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton2.ImageUrl = "~/Images/placeholder.png";
                }

                //Joining Kit :

                if (dr.GetValue(2).ToString() != "")
                {
                    ImageButton3.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
                    var result = dr.GetValue(2).ToString().Substring(dr.GetValue(2).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_joining.Visible = true;
                        ImageButton3.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton3.ImageUrl = "~/Images/placeholder.png";
                }

                //Employee signature :

                if (dr.GetValue(3).ToString() != "")
                {
                    ImageButton4.ImageUrl = "~/EMP_Images/" + dr.GetValue(3).ToString();
                    var result = dr.GetValue(3).ToString().Substring(dr.GetValue(3).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_signature.Visible = true;
                        ImageButton4.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton4.ImageUrl = "~/Images/placeholder.png";
                }

                //Address Proof Photo :

                if (dr.GetValue(4).ToString() != "")
                {
                    ImageButton5.ImageUrl = "~/EMP_Images/" + dr.GetValue(4).ToString();
                    var result = dr.GetValue(4).ToString().Substring(dr.GetValue(4).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_addr_proof.Visible = true;
                        ImageButton5.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton5.ImageUrl = "~/Images/placeholder.png";
                }

                //Police Varification :

                if (dr.GetValue(5).ToString() != "")
                {
                    ImageButton6.ImageUrl = "~/EMP_Images/" + dr.GetValue(5).ToString();
                    var result = dr.GetValue(5).ToString().Substring(dr.GetValue(5).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_police.Visible = true;
                        ImageButton6.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton6.ImageUrl = "~/Images/placeholder.png";
                }

                //PAN CARD :

                if (dr.GetValue(6).ToString() != "")
                {
                    ImageButton7.ImageUrl = "~/EMP_Images/" + dr.GetValue(6).ToString();
                    var result = dr.GetValue(6).ToString().Substring(dr.GetValue(6).ToString().Length - 4);
                    if (result.ToUpper() == ".PDF")
                    {
                        link_pan_card.Visible = true;
                        ImageButton7.ImageUrl = "~/Images/pdf_format.jpg";
                    }
                }
                else
                {
                    ImageButton7.ImageUrl = "~/Images/placeholder.png";
                }




               
            }
            d.con.Close();
        }
        catch (Exception ex)
        {
            throw  ex;
        }
        finally {
            d.con.Close();
            panel_data.Visible = true;
        }

        //approve button

     //   if (d.getsinglestring("select distinct legal_flag from pay_employee_master where comp_code='C01' and employee_type='Permanent'  and emp_code='" + ddl_emp_name.SelectedValue + "'").Equals("2") ) { btn_approve.Visible = false; btn_reject.Visible = false; } else { btn_approve.Visible = true; btn_reject.Visible = true; }
    }

    protected void download_document1(object sender, CommandEventArgs e)
    {
      
        string image_id = ddl_emp_name.SelectedValue + e.CommandArgument + ".pdf";

        downloadfile(image_id);
    }


    protected void downloadfile(string filename)
    {

        try
        {


            string path2 = Server.MapPath("~\\EMP_Images\\" + filename);

            bool code = File.Exists(path2);
            if (code == true)
            {
                Response.Clear();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path2));

                Response.TransmitFile("~\\EMP_Images\\" + filename);
                Response.WriteFile(path2);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                Response.Close();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Can not Found!!.');", true);
            }




        }
        catch (Exception ex) { throw ex; }


    }

    private void visibility() {

        txt_adhar_no.ReadOnly = true;
        txt_bank_name.ReadOnly = true;
        txt_acc_no.ReadOnly = true;
        txt_ifsc_no.ReadOnly = true;
        txt_mb_no.ReadOnly = true;

        txt_emp_name.ReadOnly = true;
        txt_father_name.ReadOnly = true;
        txt_dob.ReadOnly = true;
        txt_doj.ReadOnly = true;
        txt_gender.ReadOnly = true;

        txt_uan_no.ReadOnly = true;
        txt_esic_no.ReadOnly = true;
        txt_pf_no.ReadOnly = true;
        txt_designation.ReadOnly = true;
    }

    protected void employee_status()
    {

        try
        {
            ViewState["rem_emp_count"] = 0;
            ViewState["appro_emp_count"] = 0;
            ViewState["appro_emp_legal"] = 0;
            ViewState["reject_emp_legal"] = 0;
            panel_link.Visible = true;

            gv_rem_emp_count.DataSource = null;
            gv_rem_emp_count.DataBind();

           System.Data.DataTable dt_item = new System.Data.DataTable();
            //policy
           dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(),ddl_client.SelectedValue, ddl_state.SelectedValue, 0);
            rem_emp_count = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["rem_emp_count"] = dt_item.Rows.Count.ToString();
                rem_emp_count = ViewState["rem_emp_count"].ToString();
                gv_rem_emp_count.DataSource = dt_item;
                gv_rem_emp_count.DataBind();
               
            }
            //approve by admin
            gv_appro_emp_count.DataSource = null;
            gv_appro_emp_count.DataBind();
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_state.SelectedValue, 1);
            appro_emp_count = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_count"] = dt_item.Rows.Count.ToString();
                appro_emp_count = ViewState["appro_emp_count"].ToString();

                gv_appro_emp_count.DataSource = dt_item;
                gv_appro_emp_count.DataBind();
               
            }
            dt_item.Dispose();

            //approve by legal

            gv_appro_emp_legal.DataSource = null;
            gv_appro_emp_legal.DataBind();
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_state.SelectedValue, 2);

            appro_emp_legal = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["appro_emp_legal"] = dt_item.Rows.Count.ToString();
                appro_emp_legal = ViewState["appro_emp_legal"].ToString();

                gv_appro_emp_legal.DataSource = dt_item;
                gv_appro_emp_legal.DataBind();
               
            }
            dt_item.Dispose();

            gv_reject_emp_legal.DataSource = null;
            gv_reject_emp_legal.DataBind();

            //reject by legal
            dt_item = d.chk_legal_document(Session["COMP_CODE"].ToString(), ddl_client.SelectedValue, ddl_state.SelectedValue, 3);
            reject_emp_legal = "0";
            if (dt_item.Rows.Count > 0)
            {
                ViewState["reject_emp_legal"] = dt_item.Rows.Count.ToString();
                reject_emp_legal = ViewState["reject_emp_legal"].ToString();

                gv_reject_emp_legal.DataSource = dt_item;
                gv_reject_emp_legal.DataBind();
               
            }
            dt_item.Dispose();

           

            rem_emp_count = ViewState["rem_emp_count"].ToString();
            appro_emp_count = ViewState["appro_emp_count"].ToString();
            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
            reject_emp_legal = ViewState["reject_emp_legal"].ToString();
        }
        catch (Exception ex) { throw ex; }
        finally { }

    }
    protected void gridService_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (ddl_emp_name.SelectedValue == "Select") { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Can Not Selected ..!!');", true); return; }

            d.operation("update pay_employee_master set legal_flag = 2 ,ap_status='Approve By Leagal',reject_reason='Approve By Leagal' where comp_code='" + Session["Comp_code"].ToString() + "' AND emp_code='" + ddl_emp_name.SelectedValue + "'");
            panel_data.Visible = false;
   			//copy  file to anather folder
            copy_file();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Details Approve Succsefully ..!!');", true);

            employee_status();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { 
        
        }
    }
   
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        if (ddl_emp_name.SelectedValue == "Select") { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Can Not Selected ..!!');", true); return; }

        d.operation("update pay_employee_master set legal_flag = 3 ,ap_status='Reject By Leagal',reject_reason='"+txt_reject.Text+"' where comp_code='" + Session["Comp_code"].ToString() + "' AND emp_code='" + ddl_emp_name.SelectedValue + "'");
        panel_data.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Details Rejected Succsefully ..!!');", true);
        employee_status();
        txt_reject.Text = "";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }
    protected void image_click(object sender, ImageClickEventArgs e)
    {
        System.Web.UI.WebControls.Image button = sender as System.Web.UI.WebControls.Image;
        Response.Redirect(button.ImageUrl);
        
    }
    protected void btn_exp_excel_Click(object sender, EventArgs e)
    {
        try
        {

            d.con.Open();


            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT client_name, client_wise_state AS 'state_name', unit_name AS 'branch_name', pay_employee_master.emp_name, EMP_FATHER_NAME, DATE_FORMAT(BIRTH_DATE, '%d-%m-%Y') AS 'dob', DATE_FORMAT(JOINING_DATE, '%d-%m-%Y') AS 'doj', P_TAX_NUMBER AS 'adhar_no', PF_BANK_NAME AS 'bank_name', original_bank_account_no AS 'account_no', PF_IFSC_CODE AS 'bank_ifsc_code', PAN_NUMBER AS 'van_no', ESIC_NUMBER AS 'esic_no', PF_NUMBER AS 'pf_no', IF(original_photo != '', 'YES', 'NO') AS 'original_photo', IF(original_adhar_card != '', 'YES', 'NO') AS 'original_adhar_card', IF(bank_passbook != '', 'YES', 'NO') AS 'bank_passbook', IF(emp_signature != '', 'YES', 'NO') AS 'joining_kit', IF(original_address_proof != '', 'YES', 'NO') AS 'original_address_proof', IF(original_policy_document != '', 'YES', 'NO') AS 'original_policy_document' FROM pay_employee_master INNER JOIN pay_unit_master ON pay_employee_master.COMP_CODE = pay_unit_master.comp_code AND pay_employee_master.client_wise_state = pay_unit_master.state_name AND pay_employee_master.unit_code = pay_unit_master.unit_code INNER JOIN pay_client_master ON pay_client_master.comp_code = pay_employee_master.comp_code AND pay_client_master.client_code = pay_employee_master.client_code INNER JOIN pay_images_master ON pay_images_master.comp_code = pay_employee_master.comp_code AND pay_images_master.emp_code = pay_employee_master.emp_code WHERE pay_employee_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_employee_master.client_code = '" + ddl_client.SelectedValue + "' AND left_date IS NULL AND (legal_flag =1 || legal_flag =2) AND employee_type='Permanent' ORDER BY client_name,client_wise_state, unit_name, emp_name", d.con1);

           

            DataSet ds = new DataSet();
            adp2.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename="+ddl_client.SelectedItem.Text+"_Employee_Details.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate1(ListItemType.Header, ds);
                Repeater1.ItemTemplate = new MyTemplate1(ListItemType.Item, ds);
                Repeater1.FooterTemplate = new MyTemplate1(ListItemType.Footer, null);
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

    public class MyTemplate1 : ITemplate
    {

        ListItemType type;
        LiteralControl lc;
        DataSet ds;
        static int ctr;
        public MyTemplate1(ListItemType type, DataSet ds)
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
                    lc = new LiteralControl("<TABLE BORDER=1><TR><TH>CLIENT NAME</TH><TH>STATE NAME</TH><TH>BRANCH NAME</TH><TH>EMP NAME</TH><TH>EMP FATHER NAME</TH><TH>DATE OF BIRTH </TH><TH>DATE OF JOINING</TH><TH>ADHAR NUMBER</TH><TH>BANK NAME</TH><TH>ACCOUNT NUMBER</TH><TH>BANK IFSC CODE</TH><TH>UAN NUMBER </TH><TH>ESIC NUMBER</TH><TH>PF NUMBER</TH><TH>ORIGINAL_PHOTO</TH><TH>ORIGINAL_ADHAR_CARD</TH><TH>BANK_PASSBOOK</TH><TH>JOINING_KIT</TH><TH>ORIGINAL_ADDRESS_PROOF</TH><TH>ORIGINAL_POLICY_DOCUMENT<TH></TR>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + ds.Tables[0].Rows[ctr]["client_name"] + " </td><td>" + ds.Tables[0].Rows[ctr]["state_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["branch_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["emp_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["EMP_FATHER_NAME"] + "</td><td>" + ds.Tables[0].Rows[ctr]["dob"] + "</td><td>" + ds.Tables[0].Rows[ctr]["doj"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["adhar_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bank_name"] + " </td><td>'" + ds.Tables[0].Rows[ctr]["account_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["bank_ifsc_code"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["van_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["esic_no"] + "</td><td>'" + ds.Tables[0].Rows[ctr]["pf_no"] + "</td><td>" + ds.Tables[0].Rows[ctr]["original_photo"] + "</td><td>" + ds.Tables[0].Rows[ctr]["original_adhar_card"] + "</td><td>" + ds.Tables[0].Rows[ctr]["bank_passbook"] + "</td><td>" + ds.Tables[0].Rows[ctr]["joining_kit"] + "</td><td>" + ds.Tables[0].Rows[ctr]["original_address_proof"] + "</td><td>" + ds.Tables[0].Rows[ctr]["original_policy_document"] + "</td></tr>");
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
    //copy file one folder to anather folder
    public void copy_file()
    {
        try
        {
         
            d.con.Open();
            MySqlCommand cmd_2 = new MySqlCommand("select original_photo,original_adhar_card,bank_passbook,emp_signature,original_address_proof,original_policy_document from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code ='" + ddl_emp_name.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd_2.ExecuteReader();

            string[] extention = new string[] {".png",".jpg",".pdf",".jpeg"}; 
            while (dr.Read())
            {
                for (int i = 0; i < 6; i++)
                {
                    if (dr.GetValue(i).ToString() != "")
                    {
                        string fileName = dr.GetValue(i).ToString();
                        string sourcePath = Server.MapPath(@"~/EMP_Images/");
                        string targetPath = Server.MapPath(@"~/LEAGAL_APPROVAL_IMG/");

                        
                        string FileNameOnly = fileName.Substring(0, fileName.LastIndexOf("."));

                        string delete_file = System.IO.Path.Combine(targetPath, FileNameOnly);
                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(targetPath, fileName);

                        // To copy a folder's contents to a new location:
                        // Create a new target folder, if necessary.
                        for(int j=0;j<extention.Length;j++)
                        {
                         if (File.Exists(delete_file+extention[j]))
                            {
                            File.Delete(delete_file + extention[j]);
                            }
                        }
                        
                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        if (File.Exists(sourceFile))
                        {
                            System.IO.File.Copy(sourceFile, destFile, true);
                        }
                    }
                }
               
            }
            d.con.Close();
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally {
            d.con.Close();
        }
    }

    //protected void employee_status_all()
    //{

    //    try
    //    {
    //        ViewState["rem_emp_count"] = 0;
    //        ViewState["appro_emp_count"] = 0;
    //        ViewState["appro_emp_legal"] = 0;
    //        ViewState["reject_emp_legal"] = 0;
    //        panel_link.Visible = true;
    //        gv_rem_emp_count.DataSource = null;
    //        gv_rem_emp_count.DataBind();
    //        System.Data.DataTable dt_item = new System.Data.DataTable();
    //        dt_item = d.chk_legal_document_all(Session["COMP_CODE"].ToString(), 0);
    //        rem_emp_count = "0";
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ViewState["rem_emp_count"] = dt_item.Rows.Count.ToString();
    //            rem_emp_count = ViewState["rem_emp_count"].ToString();
    //            gv_rem_emp_count.DataSource = dt_item;
    //            gv_rem_emp_count.DataBind();
    //        }
    //        gv_appro_emp_count.DataSource = null;
    //        gv_appro_emp_count.DataBind();
    //        dt_item = d.chk_legal_document_all(Session["COMP_CODE"].ToString(), 1);
    //        appro_emp_count = "0";
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ViewState["appro_emp_count"] = dt_item.Rows.Count.ToString();
    //            appro_emp_count = ViewState["appro_emp_count"].ToString();
    //            gv_appro_emp_count.DataSource = dt_item;
    //            gv_appro_emp_count.DataBind();
    //        }
    //        dt_item.Dispose();
    //        gv_appro_emp_legal.DataSource = null;
    //        gv_appro_emp_legal.DataBind();
    //        dt_item = d.chk_legal_document_all(Session["COMP_CODE"].ToString(), 2);
    //        appro_emp_legal = "0";
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ViewState["appro_emp_legal"] = dt_item.Rows.Count.ToString();
    //            appro_emp_legal = ViewState["appro_emp_legal"].ToString();
    //            gv_appro_emp_legal.DataSource = dt_item;
    //            gv_appro_emp_legal.DataBind();
    //        }
    //        dt_item.Dispose();
    //        gv_reject_emp_legal.DataSource = null;
    //        gv_reject_emp_legal.DataBind();
    //        dt_item = d.chk_legal_document_all(Session["COMP_CODE"].ToString(), 3);
    //        reject_emp_legal = "0";
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ViewState["reject_emp_legal"] = dt_item.Rows.Count.ToString();
    //            reject_emp_legal = ViewState["reject_emp_legal"].ToString();
    //            gv_reject_emp_legal.DataSource = dt_item;
    //            gv_reject_emp_legal.DataBind();
    //        }
    //        dt_item.Dispose();
    //        rem_emp_count = ViewState["rem_emp_count"].ToString();
    //        appro_emp_count = ViewState["appro_emp_count"].ToString();
    //        appro_emp_legal = ViewState["appro_emp_legal"].ToString();
    //        reject_emp_legal = ViewState["reject_emp_legal"].ToString();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { }
    //}
}
