using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Web;

public partial class IHMS_Services : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    int rowid = 0;
    public string document_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            ViewState["request_id"] = 0;
            gridview();
            gridsuperviser();
            client_list1();
            gv_feedback_load();
            ddl_client_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT CASE WHEN `client_code` = 'BALIK HK' THEN CONCAT(`client_name`, ' HK') WHEN `client_code` = 'BALIC SG' THEN CONCAT(`client_name`, ' SG') WHEN `client_code` = 'BAG' THEN CONCAT(`client_name`, ' HK') WHEN `client_code` = 'BG' THEN CONCAT(`client_name`, ' SG') ELSE `client_name` END AS 'client_name', `client_code` FROM `pay_client_master` WHERE `comp_code` = '"+Session["COMP_CODE"].ToString()+"' AND `client_code` IN (SELECT DISTINCT (`client_code`) FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '"+Session["COMP_CODE"].ToString()+"' AND `pay_client_state_role_grade`.`emp_code` IN ("+Session["REPORTING_EMP_SERIES"].ToString()+")) ORDER BY `client_code`", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client_name.DataSource = dt_item;
                    ddl_client_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_client_name.DataValueField = dt_item.Columns[1].ToString();
                    ddl_client_name.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_client_name.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            gridviewcomplaint1();
            client_list();
        }
    }

    protected void gridview()
    {

        d.con.Open();
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("Select id,(SELECT `client_name` FROM `pay_client_master` WHERE `client_code` = (SELECT `client_code` FROM `pay_unit_master` WHERE `comp_code` = `pay_service_master`.`comp_code` AND `unit_code` = `pay_service_master`.`unit_code`)) AS 'client_code',(Select CONCAT(`STATE_NAME`, '_', `UNIT_CITY`, '_', `unit_name`) from pay_unit_master where comp_code= '" + Session["comp_code"].ToString() + "' and unit_code = pay_service_master.unit_code) as unit_code,services,location,additional_comment,date_format(date,'%d/%m/%Y') as date ,(CASE WHEN `status` = 'In Process' THEN 'New' ELSE `status` END) AS 'status' from pay_service_master where comp_code = '" + Session["comp_code"].ToString() + "' and type = 'client' order by id desc ", d.con);
        adp.Fill(ds);
        gv_ihms_service.DataSource = ds;
        gv_ihms_service.DataBind();
        ds.Clear();
        d.con.Close();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
    }
    protected void gv_ihms_service_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[3].Visible = false;
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
             e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_ihms_service, "Select$" + e.Row.RowIndex);

        }
    }
    private void send_email(string emailhtmlfile, string toaddress,int id,FileUpload up_quotation)
    {
        string body = string.Empty;
        
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

            mail.From = new MailAddress("info@celtsoft.com");
            if (up_quotation.HasFile) 
            {
                string fileName = Path.GetFileName(up_quotation.PostedFile.FileName);
                Attachment myAttachment =
                               new Attachment(up_quotation.FileContent, fileName);
                mail.Attachments.Add(myAttachment);
            }
            
            mail.To.Add(toaddress);
            mail.Subject = "Quotation For Service";
            //d.con1.Open();
            //MySqlCommand cmdmax1 = new MySqlCommand("SELECT  where pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            //MySqlDataReader drmax = cmdmax1.ExecuteReader();
            //while (drmax.Read())
            //{
                body = body.Replace("{Unitcode}", Session["UNIT_CODE"].ToString());
                body = body.Replace("{quot_date}", DateTime.Now.ToString());

                body = body.Replace("{accept_url}",d.check_url(ConfigurationManager.AppSettings["URL"]) + "service_status.aspx?A=accept&B=" + Session["UNIT_CODE"].ToString()+"&C="+id);
                body = body.Replace("{reject_url}", d.check_url(ConfigurationManager.AppSettings["URL"]) + "service_status.aspx?A=reject&B=" + Session["UNIT_CODE"].ToString()+"&C="+id);

                //mail.Body = tosubject;
            //}
            //drmax.Close();
            //d.con1.Close();

            mail.Body = body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }

        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false); }

    }
    protected void gv_ihms_service_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_ihms_service.UseAccessibleHeader = false;
            gv_ihms_service.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
   
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        //int id = Convert.ToInt32(gv_ihms_service.DataKeys[index].Values[0].ToString());
        //FileUpload up_quotation = (FileUpload)gv_ihms_service.Rows[index].FindControl("up_quotation");
        //up_quotation.Enabled = true;

        //if (up_quotation.HasFile)
        //{

        //    send_email(Server.MapPath("~/IHMS_Service.htm"), "akshay.chaudhari@celtsoft.com", id, up_quotation);

        //    string fileExt = System.IO.Path.GetExtension(up_quotation.FileName);

        //    string fileName = System.IO.Path.GetFileName(up_quotation.PostedFile.FileName);
        //    up_quotation.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

        //    System.IO.File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + Session["UNIT_CODE"].ToString() + "_1" + fileExt, true);
        //    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

        //    d.operation("Insert Into Pay_unit_notification(UNIT_CODE,File,Description,flag)Values('" + Session["UNIT_CODE"].ToString() + "','" + Session["UNIT_CODE"].ToString() + "_" + id + fileExt + "','" + txt_desc.Text + "','0')");
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File Uploaded and Saved successfully!')", true);

        //}

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string status = "";
        try
        {
            if (client_documents_upload.HasFile)
            {
                //d.operation("delete from pay_service_documents where id =" + txt_req_id.Text);
                int count = client_documents_upload.PostedFiles.Count - (client_documents_upload.PostedFiles.Count - 1);
                foreach (HttpPostedFile uploadedFile in client_documents_upload.PostedFiles)
                {
                    //int id = list[0];

                    string fileExt = System.IO.Path.GetExtension(uploadedFile.FileName);
                    if (!(fileExt == ".exe" || fileExt == ".com" || fileExt == ".dll"))
                    {
                        // d.operation("delete from pay_service_documents where id =" + id);
                        string fileName = Path.GetFileName(uploadedFile.FileName);
                        client_documents_upload.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                        File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + "IHMS_Quotation" + "_" + count + fileExt, true);
                        File.Delete(Server.MapPath("~/Images/") + fileName);

                        d.con.Open();
                        MySqlCommand cmd_status = new MySqlCommand("Select status From pay_service_master where id = '" + ViewState["request_id"].ToString() + "'", d.con);
                        MySqlDataReader dr_status = cmd_status.ExecuteReader();
                        if (dr_status.Read())
                        {
                            if (dr_status.GetValue(0).ToString().Contains("In Process"))
                            {
                                status = "Quotation Generated";
                            }
                            else if (dr_status.GetValue(0).ToString().Contains("Quotation Approved"))
                            {
                                status = "Bill Generated";
                            }
                            else if (dr_status.GetValue(0).ToString().Contains("Quotation Rejected"))
                            {
                                status = "Quotation Regenerated";
                            }
                            else { status = dr_status.GetValue(0).ToString(); }
                        }
                        dr_status.Dispose();
                        d.con.Close();

                        d.operation("Update pay_service_master set status = '" + status + "' where id='" + ViewState["request_id"].ToString() + "' ");
                        d.operation("insert into pay_service_documents (id,comp_code,unit_code,file_name,create_date,created_by,Flag) values ('" + ViewState["request_id"].ToString() + "','" + Session["COMP_CODE"].ToString() + "','" + txt_unit_code.Text + "','" + "IHMS_Quotation_" + count + fileExt + "',now(),'" + Session["LOGIN_ID"].ToString() + "','client')");
                        count++;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Data Send Succesfully !!!')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('This file extensions are not allowed !!!')", true);
                    }
                }
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please Upload Documents !!!')", true);
            }
        }
        catch (Exception ex){  }
        finally { d.con.Close(); btn_clear_Click(null, null); gridview(); }
        
    }

    protected void gv_ihms_service_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        int rowid = int.Parse((gv_ihms_service.SelectedRow.Cells[0].Text));
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("Select id,(Select CONCAT(`STATE_NAME`, '_', `UNIT_CITY`, '_', `unit_name`) from pay_unit_master where comp_code= '" + Session["comp_code"].ToString() + "' and unit_code = pay_service_master.unit_code) as unit,services,additional_comment,date_format(date,'%d/%m/%Y') as date ,(CASE WHEN `status` = 'In Process' THEN 'New' ELSE `status` END) AS 'status',(Select CLient_name From pay_client_master where client_code = (Select Client_code From Pay_unit_master where comp_code= '" + Session["COMP_CODE"].ToString() + "' and unit_code = pay_service_master.unit_code)) as client,unit_code from pay_service_master where comp_code = '" + Session["comp_code"].ToString() + "' and Id = '" + rowid + "' ", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            ViewState["request_id"] = dr.GetValue(0).ToString();
            txt_branch_name.Text = dr.GetValue(1).ToString();
            ddl_service_type1.SelectedValue = dr.GetValue(2).ToString();
            txt_comment.Text = dr.GetValue(3).ToString();
            txt_date.Text = dr.GetValue(4).ToString();
            txt_status.Text = dr.GetValue(5).ToString();
            txt_client_name.Text = dr.GetValue(6).ToString();
            txt_unit_code.Text = dr.GetValue(7).ToString();
           
        }
        dr.Dispose();
        d.con.Close();
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txt_branch_name.Text = "";
        txt_client_name.Text = "";
        txt_req_id.Text = "";
        txt_unit_code.Text = "";
        ddl_service_type1.SelectedValue = "Select";
        txt_location.Text = "";
        txt_comment.Text = "";
        txt_date.Text = "";
        txt_status.Text = "";
    }

    protected void gv_ihms_superviser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //FileUpload flUpload = e.Row.FindControl("up_quotation") as FileUpload;
        //ScriptManager.GetCurrent(this).RegisterPostBackControl(flUpload);  
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_superviser, "Select$" + e.Row.RowIndex);
            e.Row.Cells[0].Visible = false;
            gv_superviser.HeaderRow.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[0].FindControl("chk_ticket").gr
            //System.Web.UI.WebControls.CheckBox activeCheckBox = sender as System.Web.UI.WebControls.CheckBox;
            //if(e.Row.Cells[0].Contro)
            //{}
        }
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_branch_name.SelectedValue != "Select")
        {
            ddl_branch_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "'  and UNIT_CODE  in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_name.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND `pay_unit_master`.`branch_status` = 0 ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_branch_name.DataSource = dt_item;
                    ddl_branch_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_branch_name.DataValueField = dt_item.Columns[1].ToString();
                    ddl_branch_name.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                // ddlunitselect.Items.Insert(0, "Select");
                ddl_branch_name.Items.Insert(0, "ALL");
                ddl_branch_name.SelectedIndex = 0;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        //State
        if (ddl_state.SelectedValue != "Select")
        {
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client_name.SelectedValue + "' and state_name in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND client_code='" + ddl_client_name.SelectedValue + "')", d.con);
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
                ddl_state.Items.Insert(0, "Select state");
                //ddl_state.SelectedIndex = 0;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            // ddl_branch_name.Items.Clear();
            ddl_state.Items.Clear();

        }

    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_vendor_name.SelectedValue != "Select")
        {
            ddl_vendor_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select vend_name,vend_id from pay_vendor_master  where txtbillstate = (select DISTINCT(state_name) from pay_unit_master where unit_code='" + ddl_branch_name.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "') and txtbillcity = (select DISTINCT(unit_city) from pay_unit_master where unit_code='" + ddl_branch_name.SelectedValue + "' and comp_code='" + Session["COMP_CODE"].ToString() + "') ", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_vendor_name.DataSource = dt_item;
                    ddl_vendor_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_vendor_name.DataValueField = dt_item.Columns[1].ToString();
                    ddl_vendor_name.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                // ddlunitselect.Items.Insert(0, "Select");
                ddl_vendor_name.Items.Insert(0, "Select Vendor Name");
                ddl_vendor_name.SelectedIndex = 0;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }

    }

    protected void superviserclient_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        rowid = int.Parse(gv_superviser.SelectedRow.Cells[0].Text);
        d.con1.Open();
        MySqlCommand cmd = new MySqlCommand("select comp_code,client_code,unit_code,state_name,services,vendor_type,vendor_code,product_specification,type_make,model_no,costing_amount,id,flag from pay_service_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and id='" + rowid + "'", d.con1);
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            ddl_client_name.SelectedValue = dr.GetValue(1).ToString();
            ddl_client_SelectedIndexChanged(null, null);
            ddl_state.SelectedValue = dr.GetValue(3).ToString();
            ddl_state_SelectedIndexChanged(null, null);
            ddl_branch_name.SelectedValue = dr.GetValue(2).ToString();

            ddl_service_type.SelectedValue = dr.GetValue(4).ToString();
            ddl_vendor_type.SelectedValue = dr.GetValue(5).ToString();
            ddl_branch_SelectedIndexChanged(null, null);
            try
            {
                ddl_vendor_name.SelectedValue = dr.GetValue(6).ToString();
            }
            catch(Exception ex)
            {}
            txt_product_specification.Text = dr.GetValue(7).ToString();
            txt_type_make.Text = dr.GetValue(8).ToString();
            txt_model_no.Text = dr.GetValue(9).ToString();
            txt_costing_amount.Text = dr.GetValue(10).ToString();
            lbl_id.Text = dr.GetValue(11).ToString();

            if (dr.GetValue(12).ToString().Equals("0"))
            {
                btn_save_details.Visible = true;
                btn_save_draft.Visible = true;
            }
            else
            {
                btn_save_details.Visible = false;
                btn_save_draft.Visible = false;
            }

        }

        dr.Close();
        d.con1.Close();
       

    }
    public void gridsuperviser()
    {

        d.con.Open();
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("select id,client_code as 'CLIENT',(select UNIT_NAME from pay_unit_master where pay_unit_master.unit_code = pay_service_master.unit_code and  pay_unit_master.comp_code = pay_service_master.comp_code ) as 'UNIT',state_name as 'STATE',services as 'SERVICE',vendor_type as 'VENDOR TYPE',(select vend_name from pay_vendor_master where vend_id= vendor_code) as 'VENDOR',product_specification AS 'SPECIFICATION',type_make AS 'TYPE/MAKE',model_no AS 'MODEL NO',costing_amount AS 'AMOUNT' from pay_service_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and type = 'supervisor' order by date desc ", d.con);
        adp.Fill(ds);
        gv_superviser.DataSource = ds.Tables[0];
        gv_superviser.DataBind();
        d.con.Close();
        

    }

    protected void btnclose_Click_superviser(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    public void text_clear()
    {
        ddl_client_name.SelectedIndex = 0;
        ddl_state.SelectedIndex = 0;
        ddl_branch_name.SelectedIndex = 0;
        ddl_service_type.SelectedValue = "Select";
        ddl_vendor_type.SelectedValue = "Select";
        ddl_vendor_name.SelectedIndex = 0;
        txt_product_specification.Text = "";
        txt_type_make.Text = "";
        txt_model_no.Text = "";
        txt_costing_amount.Text = "";


    }

    protected void gv_superviser_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_superviser.UseAccessibleHeader = false;
             gv_superviser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void btn_save_details_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        int result = 0;
        try
        {

            result = d.operation("UPDATE pay_service_master SET comp_code='" + Session["comp_code"].ToString() + "',client_code='" + ddl_client_name.SelectedValue + "',state_name='" + ddl_state.SelectedValue + "',unit_code='" + ddl_branch_name.SelectedValue + "',services='" + ddl_service_type.SelectedValue + "',vendor_type='" + ddl_vendor_type.SelectedValue + "',vendor_code='" + ddl_vendor_name.SelectedValue + "',product_specification='" + txt_product_specification.Text + "',type_make='" + txt_type_make.Text + "',model_no='" + txt_model_no.Text + "',costing_amount='" + txt_costing_amount.Text + "',flag='1' where comp_code='" + Session["COMP_CODE"].ToString() + "' and id='" + lbl_id.Text + "' ");

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
                text_clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
                text_clear();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            btn_save_details.Visible = false;
            btn_save_draft.Visible = false;
        }
    }
    protected void btn_save_draft_Click(object sender, EventArgs e)
    {
        int result = 0;
        try
        {
            result = d.operation("insert into pay_service_master (comp_code,client_code,unit_code,state_name,services,vendor_type,vendor_code,product_specification,type_make,model_no,costing_amount,date,flag,type) VALUES('" + Session["COMP_CODE"].ToString() + "','" + ddl_client_name.SelectedValue + "','" + ddl_branch_name.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_service_type.SelectedValue + "','" + ddl_vendor_type.SelectedValue + "','" + ddl_vendor_name.SelectedValue + "','" + txt_product_specification.Text + "','" + txt_type_make.Text + "','" + txt_model_no.Text + "','" + txt_costing_amount.Text + "',now(),'0','supervisor')");

            if (document_upload.HasFile)
            {
                //d.operation("delete from pay_service_documents where id =" + txt_req_id.Text);
                int count = document_upload.PostedFiles.Count - (document_upload.PostedFiles.Count - 1);
                foreach (HttpPostedFile uploadedFile in document_upload.PostedFiles)
                {
                    //int id = list[0];

                    string fileExt = System.IO.Path.GetExtension(uploadedFile.FileName);
                    if (!(fileExt == ".exe" || fileExt == ".com" || fileExt == ".dll"))
                    {
                        // d.operation("delete from pay_service_documents where id =" + id);
                        string fileName = Path.GetFileName(uploadedFile.FileName);
                        document_upload.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                        File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + "Quotation" + "_" + count + fileExt, true);
                        File.Delete(Server.MapPath("~/Images/") + fileName);

                        // d.operation("Update pay_service_master set status = 'In-Progress' where id = " + txt_req_id.Text);
                        d.operation("insert into pay_service_documents (comp_code,unit_code,file_name,create_date,created_by,flag) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_branch_name.SelectedValue + "','" + "Quotation_" + count + fileExt + "',now(),'" + Session["LOGIN_ID"].ToString() + "','supervisor')");
                        count++;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('This file extensions are not allowed !!!')", true);
                    }
                }
            }

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
                text_clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
                text_clear();
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            gridsuperviser();
        }
    }
    protected void btn_clear_super_Click(object sender, EventArgs e)
    {
        text_clear();
    }


    //MD Change start

    protected void client_list1()
    {

        try
        {
            d.con.Open();

            ddl_client_name1.Items.Clear();
            MySqlDataAdapter cmd = new MySqlDataAdapter("Select distinct(pay_unit_feedback.client_code),client_name  from pay_client_master INNER JOIN pay_unit_feedback ON pay_client_master.client_code = pay_unit_feedback.client_code where pay_unit_feedback.comp_code='" + Session["COMP_CODE"].ToString() + "' AND `pay_unit_feedback`.`client_code` in(SELECT `client_code` FROM `pay_client_state_role_grade` WHERE `COMP_CODE` = '"+Session["COMP_CODE"].ToString()+"' AND `pay_client_state_role_grade`.`emp_code` IN ("+Session["REPORTING_EMP_SERIES"].ToString()+")) ORDER BY pay_unit_feedback.client_code", d.con);
            System.Data.DataTable dt = new System.Data.DataTable();
            cmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {


                ddl_client_name1.DataSource = dt;
                ddl_client_name1.DataTextField = dt.Columns[1].ToString();
                ddl_client_name1.DataValueField = dt.Columns[0].ToString();
                ddl_client_name1.DataBind();
                dt.Dispose();
                d.con.Close();
            }
            dt.Dispose();

            ddl_client_name1.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddl_client_name1.SelectedValue != "Select")
        {
            //State
            gv_feedback_load();
            ddl_zone_name.Items.Clear();
            ddl_zone_user.Items.Clear();
            ddl_region_user.Items.Clear();
            ddl_branch_user.Items.Clear();
            // ddl_state_name.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(`ZONE`) from pay_zone_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND client_code='" + ddl_client_name1.SelectedValue + "' and (Zone!='' || zone is not null)", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_zone_name.DataSource = dt_item;
                    ddl_zone_name.DataTextField = dt_item.Columns[0].ToString();
                    ddl_zone_name.DataValueField = dt_item.Columns[0].ToString();
                    ddl_zone_name.DataBind();
                    d.con.Close();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_zone_name.Items.Insert(0, "Select");
                ddl_zone_name.SelectedValue.Insert(0,"Select");

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

            try
            {
                d.con.Open();
                ddl_branch_user.Items.Clear();
                System.Data.DataTable dt_item1 = new System.Data.DataTable();
                MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(`pay_unit_master`.`unit_code`), convert(CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) , char(250)) AS 'UNIT_NAME'FROM`pay_unit_master`INNER JOIN `pay_unit_feedback` ON `pay_unit_feedback`.`COMP_CODE` = `pay_unit_master`.`COMP_CODE` and `pay_unit_feedback`.`unit_code` = `pay_unit_master`.`unit_code`  WHERE  `pay_unit_feedback`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_unit_feedback`.`client_code` = '" + ddl_client_name1.SelectedValue + "' ", d.con);
                MySqlDataAdapter cmd_item1 = new MySqlDataAdapter(cmd_1);

                cmd_item1.Fill(dt_item1);
                if (dt_item1.Rows.Count > 0)
                {
                    ddl_branch_user.DataSource = dt_item1;
                    ddl_branch_user.DataTextField = dt_item1.Columns[1].ToString();
                    ddl_branch_user.DataValueField = dt_item1.Columns[0].ToString();
                    ddl_branch_user.DataBind();
                    d.con.Close();
                }
                dt_item1.Dispose();
                d.con.Close();
                ddl_branch_user.Items.Insert(0, "Select");


            }

            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
    }

    protected void ddl_zone_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        //zone user
        try
        {
            gv_feedback_load();
            ddl_zone_user.Items.Clear();
            d.con.Open();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(`pay_user_master`.`login_id`), `pay_user_master`.`USER_NAME`FROM`pay_unit_feedback`INNER JOIN `pay_user_master` ON `pay_unit_feedback`.`COMP_CODE` = `pay_user_master`.`COMP_CODE` AND `pay_unit_feedback`.`CLIENT_CODE` = `pay_user_master`.`CLIENT_CODE` AND `pay_unit_feedback`.`zone_login` = `pay_user_master`.`login_id`  WHERE  `pay_user_master`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_user_master`.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND `pay_user_master`.`client_zone` = '" + ddl_zone_name.SelectedValue + "' and pay_user_master.unit_code is null", d.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter(cmd_1);

            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_zone_user.DataSource = dt_item;
                ddl_zone_user.DataTextField = dt_item.Columns[1].ToString();
                ddl_zone_user.DataValueField = dt_item.Columns[0].ToString();
                ddl_zone_user.DataBind();
                d.con.Close();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_zone_user.Items.Insert(0, "Select");
        }

        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }

        //Region user
        try
        {
            ddl_region_user.Items.Clear();
            d.con.Open();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(`pay_user_master`.`login_id`), `pay_user_master`.`USER_NAME`FROM`pay_unit_feedback`INNER JOIN `pay_user_master`ON`pay_unit_feedback`.`COMP_CODE` = `pay_user_master`.`COMP_CODE`AND `pay_unit_feedback`.`CLIENT_CODE` = `pay_user_master`.`CLIENT_CODE`AND `pay_unit_feedback`.`REGION_LOGIN` = `pay_user_master`.`login_id`   WHERE  `pay_user_master`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_user_master`.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND `pay_user_master`.`client_zone` = '" + ddl_zone_name.SelectedValue + "' AND pay_user_master.unit_code is null", d.con);
            MySqlDataAdapter cmd_item = new MySqlDataAdapter(cmd_1);

            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_region_user.DataSource = dt_item;
                ddl_region_user.DataTextField = dt_item.Columns[1].ToString();
                ddl_region_user.DataValueField = dt_item.Columns[0].ToString();
                ddl_region_user.DataBind();
                d.con.Close();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_region_user.Items.Insert(0, "Select");


        }

        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        ////branch user
        //try
        //{
        //    d.con.Open();
        //    ddl_branch_user.Items.Clear();
        //    System.Data.DataTable dt_item = new System.Data.DataTable();
        //    MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT(`pay_unit_master`.`unit_code`), CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'UNIT_NAME'FROM`pay_unit_master`INNER JOIN `pay_unit_feedback` ON `pay_unit_feedback`.`COMP_CODE` = `pay_unit_master`.`COMP_CODE` AND `pay_unit_feedback`.`client_code` = `pay_unit_master`.`client_code` AND `pay_unit_feedback`.`unit_code` = `pay_unit_master`.`unit_code`  WHERE  `pay_unit_feedback`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_unit_feedback`.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND `pay_unit_master`.`txt_zone` = '" + ddl_zone_name.SelectedValue + "'", d.con);
        //    MySqlDataAdapter cmd_item = new MySqlDataAdapter(cmd_1);

        //    cmd_item.Fill(dt_item);
        //    if (dt_item.Rows.Count > 0)
        //    {
        //        ddl_branch_user.DataSource = dt_item;
        //        ddl_branch_user.DataTextField = dt_item.Columns[1].ToString();
        //        ddl_branch_user.DataValueField = dt_item.Columns[0].ToString();
        //        ddl_branch_user.DataBind();
        //        d.con.Close();
        //    }
        //    dt_item.Dispose();
        //    d.con.Close();
        //    ddl_branch_user.Items.Insert(0, "Select");


        //}

        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //}
    }




    protected void gv_feedback_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_feedback.UseAccessibleHeader = false;
            gv_feedback.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_feedback_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       
        for (int i = 1; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
                
            }

        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_feedback, "Select$" + e.Row.RowIndex);
        }
    }

    protected void ddl_region_user_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            gv_feedback.DataSource = null;
            gv_feedback.DataBind();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT  `LOGIN_ID` AS 'USER ID',pay_unit_feedback.id as 'ID',  `USER_NAME` AS 'USER NAME',  `FEEDBACK`,  `REASON` AS 'SUGGESTION',  DATE_FORMAT(`Feed_Date`, '%d - %M - %Y') AS 'FEEDBACK DATE',  `document` AS 'ATTACHMENT FILE'  FROM  `pay_user_master`  INNER JOIN `pay_unit_feedback` ON `pay_user_master`.`COMP_CODE` = `pay_unit_feedback`.`COMP_CODE` AND `pay_user_master`.`LOGIN_ID` = `pay_unit_feedback`.`Region_login`  WHERE  `pay_unit_feedback`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_feedback.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND `pay_unit_feedback`.`Region_login` = '" + ddl_region_user.SelectedValue + "'  ", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet DS1 = new DataSet();
            cad1.Fill(DS1);
            gv_feedback.DataSource = DS1;
            gv_feedback.DataBind();
            cad1.Dispose();
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
    protected void ddl_branch_user_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            gv_feedback.DataSource = null;
            gv_feedback.DataBind();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT  CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'USER NAME', pay_unit_feedback.id as 'ID' , `pay_unit_feedback`.`unit_code` AS 'USER ID',  `FEEDBACK`,  `REASON` AS 'SUGGESTION',  DATE_FORMAT(`Feed_Date`, '%d - %M - %Y') AS 'FEEDBACK DATE',  document AS 'ATTACHMENT FILE'  FROM  `pay_unit_master`  INNER JOIN `pay_unit_feedback` ON `pay_unit_master`.`COMP_CODE` = `pay_unit_feedback`.`COMP_CODE` AND `pay_unit_master`.`unit_code` = `pay_unit_feedback`.`unit_code`  WHERE  `pay_unit_feedback`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_feedback.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND  pay_unit_feedback.unit_code='" + ddl_branch_user.SelectedValue + "'  ORDER BY `pay_unit_master`.`UNIT_CODE`", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet DS1 = new DataSet();
            cad1.Fill(DS1);
            gv_feedback.DataSource = DS1;
            gv_feedback.DataBind();
            cad1.Dispose();
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
    protected void ddl_zone_user_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            d.con.Open();
            gv_feedback.DataSource = null;
            gv_feedback.DataBind();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT  `LOGIN_ID` AS 'USER ID',pay_unit_feedback.id as 'ID',  `USER_NAME` AS 'USER NAME',  `FEEDBACK`,  `REASON` AS 'SUGGESTION',  DATE_FORMAT(`Feed_Date`, '%d - %M - %Y') AS 'FEEDBACK DATE',  `document` AS 'ATTACHMENT FILE'  FROM  `pay_user_master`  INNER JOIN `pay_unit_feedback` ON `pay_user_master`.`COMP_CODE` = `pay_unit_feedback`.`COMP_CODE` AND `pay_user_master`.`LOGIN_ID` = `pay_unit_feedback`.`Zone_login`  WHERE  `pay_unit_feedback`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND pay_unit_feedback.`client_code` = '" + ddl_client_name1.SelectedValue + "' AND `pay_unit_feedback`.`Zone_login` = '" + ddl_zone_user.SelectedValue + "'  ", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet DS1 = new DataSet();
            cad1.Fill(DS1);
            gv_feedback.DataSource = DS1;
            gv_feedback.DataBind();
            cad1.Dispose();
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
    protected void gv_feedback_load()
    {

        try
        {
            string where = "";
            d.con.Open();
            gv_feedback.DataSource = null;
            gv_feedback.DataBind();
            if (ddl_client_name1.SelectedValue != "Select") {  where = "and `pay_unit_feedback`.`client_code`='"+ddl_client_name1.SelectedValue+"'";
            if (ddl_zone_name.SelectedValue!= "") { where = where + "and client_zone='" + ddl_zone_name.SelectedValue + "'"; }  
            }

            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT`LOGIN_ID` AS 'USER ID',pay_unit_feedback.id,`USER_NAME` AS 'USER NAME',`FEEDBACK`,`REASON` AS 'SUGGESTION',DATE_FORMAT(`Feed_Date`, '%d - %M - %Y') AS 'FEEDBACK DATE',`document` AS 'ATTACHMENT FILE'FROM`pay_user_master`INNER JOIN `pay_unit_feedback` ON `pay_user_master`.`COMP_CODE` = `pay_unit_feedback`.`COMP_CODE` AND `pay_user_master`.`LOGIN_ID` = `pay_unit_feedback`.`unit_code` OR `pay_user_master`.`LOGIN_ID` = `pay_unit_feedback`.`Region_login` OR `pay_user_master`.`LOGIN_ID` = `pay_unit_feedback`.`zone_login` WHERE`pay_unit_feedback`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' " + where + " order by pay_unit_feedback.id desc ", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet DS1 = new DataSet();
            cad1.Fill(DS1);
            gv_feedback.DataSource = DS1;
            gv_feedback.DataBind();
            cad1.Dispose();
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
   

    protected void downloadfile(string filename)
    {

        try
        {


            string path2 = Server.MapPath("~\\EMP_Images\\" + filename);

            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path2));
            Response.TransmitFile("~\\EMP_Images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();
        }
        catch (Exception ex) {  }


    }

    protected void lnkDownload_Command(object sender, CommandEventArgs e)
    {

        string id = e.CommandArgument + "";
        if (id != "")
        {   
            downloadfile(id);
        }
       
   else {
            ScriptManager.RegisterStartupScript(this, this.GetType(),"alert", "alert('Atachment File Cannot Be Uploaded !!!')", true);
        }
    }


    protected void gv_feedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try {

            int item = (int)gv_feedback.DataKeys[e.RowIndex].Value;

          int  re =  d.operation("delete from pay_unit_feedback where id="+item+" ");
         
        }
        catch (Exception ex) { throw ex; }
        finally {
            gv_feedback_load();
        }
    }

    protected void lnkaddtravelplan_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Show();

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("IHMS_Services.aspx");
    }
    protected void gridviewcomplaint1()
    {

        d.con.Open();
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,complaint_name,priority,date,resole_date,( CASE WHEN `status` = 'In Process' THEN  'New' else status END) as 'status',Remark,pay_unit_master.state_name,pay_client_master.client_name,pay_unit_master.unit_name,pay_unit_master.ZONE from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code  where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "'  group by ID  order by unit_login_complaint_details.Id desc  ", d.con);
        // MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,client_name AS 'client_code',unit_login_complaint_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_code,`complaint_name`,`priority`,date,`status`,`Remark` from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "'  order by unit_login_complaint_details.Id  ", d.con);
        adp.Fill(ds);
        unitcomplaintGridView.DataSource = ds;
        unitcomplaintGridView.DataBind();
        ds.Clear();
        d.con.Close();
        Panel11.Visible = true;
    }
    protected void gridviewcomplaint()
    {

        d.con.Open();
        DataSet ds = new DataSet();
        MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,complaint_name,priority,date,resole_date,( CASE WHEN `status` = 'In Process' THEN  'New' else status END) as 'status',Remark,pay_unit_master.state_name,pay_client_master.client_name,pay_unit_master.unit_name,pay_unit_master.ZONE from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code   where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "' and unit_login_complaint_details.client_code='" + ddlunitclient1.SelectedValue + "' and unit_login_complaint_details.state='" + ddl_gv_statewise.SelectedValue + "'  group by ID order by unit_login_complaint_details.Id desc  ", d.con);
        // MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,client_name AS 'client_code',unit_login_complaint_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_code,`complaint_name`,`priority`,date,`status`,`Remark` from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "'  order by unit_login_complaint_details.Id  ", d.con);
        adp.Fill(ds);
        unitcomplaintGridView.DataSource = ds;
        unitcomplaintGridView.DataBind();
        ds.Clear();
        d.con.Close();
        Panel11.Visible = true;
    }

    protected void lnkbtn_edititemcomplaince_click(object sender, EventArgs e)
    {
        txt_commentbox.Visible = true;
        if (txt_commentbox.Text != "")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            int request_id = (int)unitcomplaintGridView.DataKeys[gvrow.RowIndex].Value;

            d.operation("Update unit_login_complaint_details set status = 'Resolve',comment='" + txt_commentbox.Text + "',resole_date=now() where id = '" + request_id + "'");
            gridviewcomplaint1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Enter Comment');", true);
        }
        txt_commentbox.Text = "";
        //txt_commentbox.Visible = true;
        //if (txt_commentbox.Text != "")
        //{
        //    GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        //    int request_id = (int)unitcomplaintGridView.DataKeys[gvrow.RowIndex].Value;

        //    d.operation("Update unit_login_complaint_details set status = 'Resolve',comment='" + txt_commentbox.Text + "',resole_date=now() where id = '" + request_id + "'");
        //    gridviewcomplaint();
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' First Enter Comment');", true);
        //}
        //txt_commentbox.Text = "";
    }
    protected void unitcomplaintGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
           
            string item = e.Row.Cells[0].Text + "-" + e.Row.Cells[1].Text;

        }
        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (dr["status"].ToString() == "New")
            {
                LinkButton lb1 = e.Row.FindControl("lnkbtn_edititemcomplaince") as LinkButton;
                lb1.Visible = true;

            }
            else
            {
                LinkButton lb1 = e.Row.FindControl("lnkbtn_edititemcomplaince") as LinkButton;
                lb1.Visible = false;
            }

        }

        //try
        //{
        //    ((LinkButton)e.Row.Cells[12].FindControl("lnkbtn_edititemcomplaince")).Visible = false;

        //    if (e.Row.Cells[9].Text.Equals("New"))
        //    {
        //        ((LinkButton)e.Row.Cells[12].FindControl("lnkbtn_edititemcomplaince")).Visible = true;
        //    }
        //}
        //catch { }
    }

    protected void unitcomplaintGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            unitcomplaintGridView.UseAccessibleHeader = false;
            unitcomplaintGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    public void client_list()
    {
        d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT unit_login_complaint_details.client_code, client_NAME FROM unit_login_complaint_details INNER JOIN pay_client_state_role_grade ON unit_login_complaint_details.COMP_CODE = pay_client_state_role_grade.COMP_CODE and unit_login_complaint_details.client_code = pay_client_state_role_grade.client_code inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code WHERE unit_login_complaint_details.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND pay_client_state_role_grade.emp_code IN (" + Session["REPORTING_EMP_SERIES"] + ")", d.con1);
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

    protected void ddlunitclient1_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        unitcomplaintGridView.Visible = true;
        try
        {
            
            DataSet ds = new DataSet();
           // MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,client_name AS 'client_code',unit_login_complaint_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_code,`complaint_name`,`priority`,date,`status`,`Remark` from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "' and unit_login_complaint_details.client_code='" + ddlunitclient1.SelectedValue + "'   order by unit_login_complaint_details.Id  ", d.con); 
            MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,pay_client_master.client_name,pay_client_master.state,pay_unit_master.unit_name,pay_zone_master.region,complaint_name,priority,date,resole_date,(CASE WHEN `status` = 'In Process' THEN 'New' ELSE `status` END) AS 'status',Remark from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code   INNER JOIN pay_zone_master ON unit_login_complaint_details.CLIENT_CODE = pay_zone_master.CLIENT_CODE AND unit_login_complaint_details.comp_code = pay_zone_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "' and unit_login_complaint_details.client_code='" + ddlunitclient1.SelectedValue + "'   order by unit_login_complaint_details.Id  ", d.con); 
            adp.Fill(ds);
            Panel11.Visible = true;
            unitcomplaintGridView.DataSource = ds.Tables[0];
            unitcomplaintGridView.DataBind();
            unitcomplaintGridView.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_statewise.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct State FROM unit_login_complaint_details where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddlunitclient1.SelectedValue + "'  ", d1.con);
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
    }

    protected void ddlsatewises_SelectedIndexChanged(object sender, EventArgs e)
    {
        unitcomplaintGridView.Visible = true;
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,pay_client_master.client_name,unit_login_complaint_details.state, pay_zone_master.region,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,`complaint_name`,`priority`,date,(CASE WHEN `status` = 'In Process' THEN 'New' ELSE `status` END) AS 'status',`Remark`,resole_date from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code  INNER JOIN pay_zone_master ON unit_login_complaint_details.client_code = pay_zone_master.client_code AND unit_login_complaint_details.comp_code = pay_zone_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "' and unit_login_complaint_details.client_code='" + ddlunitclient1.SelectedValue + "' and unit_login_complaint_details.state='" + ddl_gv_statewise.SelectedValue + "'   order by unit_login_complaint_details.Id  ", d.con); 
            adp.Fill(ds);
            unitcomplaintGridView.DataSource = ds.Tables[0];
            unitcomplaintGridView.DataBind();
            Panel11.Visible = true;
            unitcomplaintGridView.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }

        ddl_gv_branchwise.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_login_complaint_details.unit_code from unit_login_complaint_details inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code  where unit_login_complaint_details.comp_code='" + Session["comp_code"] + "' and unit_login_complaint_details.client_code = '" + ddlunitclient1.SelectedValue + "' and unit_login_complaint_details.state = '" + ddl_gv_statewise.SelectedValue + "' AND  unit_login_complaint_details.UNIT_CODE in(SELECT DISTINCT (`pay_client_state_role_grade`.`unit_code`) FROM `pay_client_state_role_grade` INNER JOIN `unit_login_complaint_details` ON `unit_login_complaint_details`.`comp_code` = `pay_client_state_role_grade`.`comp_code` WHERE `pay_client_state_role_grade`.`COMP_CODE` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`client_code` = '" + ddlunitclient1.SelectedValue + "' AND `pay_client_state_role_grade`.`state_name` = '" + ddl_gv_statewise.SelectedValue + "' AND (`pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") OR `pay_client_state_role_grade`.`emp_code` = '" + Session["LOGIN_ID"].ToString() + "')) and branch_status = 0  ORDER BY unit_login_complaint_details.UNIT_CODE", d.con);
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
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select unit_login_complaint_details.Id,pay_client_master.client_name AS 'client_name',unit_login_complaint_details.state,pay_zone_master.region,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,`complaint_name`,`priority`,date,(CASE WHEN `status` = 'In Process' THEN 'New' ELSE `status` END) AS 'status',`Remark`,Resole_date from unit_login_complaint_details inner join pay_client_master on unit_login_complaint_details.client_code=pay_client_master.client_code and unit_login_complaint_details.comp_code=pay_client_master.comp_code inner join pay_unit_master on unit_login_complaint_details.unit_code=pay_unit_master.unit_code and unit_login_complaint_details.comp_code=pay_unit_master.comp_code  INNER JOIN pay_zone_master ON unit_login_complaint_details.client_code = pay_zone_master.client_code AND unit_login_complaint_details.comp_code = pay_zone_master.comp_code where unit_login_complaint_details.comp_code='" + Session["comp_code"].ToString() + "' and unit_login_complaint_details.client_code='" + ddlunitclient1.SelectedValue + "' and unit_login_complaint_details.state='" + ddl_gv_statewise.SelectedValue + "' and unit_login_complaint_details.unit_code='" + ddl_gv_branchwise.SelectedValue + "'   order by unit_login_complaint_details.Id  ", d.con); 
            adp.Fill(ds);
            unitcomplaintGridView.DataSource = ds.Tables[0];
            unitcomplaintGridView.DataBind();
            Panel11.Visible = true;
            unitcomplaintGridView.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    protected void linkaddcatagory_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
}
