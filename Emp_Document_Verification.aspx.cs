using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emp_Document_Verification : System.Web.UI.Page
{

    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        docu_reject.Visible = false;
        try
        {
            d.con.Open();
            MySqlDataAdapter cmd = new MySqlDataAdapter("SELECT pay_document_verification.Id, pay_document_verification.emp_code, pay_document_verification.emp_name, pay_client_master.client_name  AS 'Client_Name', pay_unit_master.unit_name AS 'Unit_Name', pay_employee_master.LOCATION_CITY AS 'Working_city', CASE pay_document_verification.image_name WHEN 'police_verification_form' THEN 'Police Verification form' WHEN 'adhar_card_upload' THEN 'Adhar Card' WHEN 'Pan_card_upload' THEN 'Pan Card'   WHEN 'joining_form' THEN 'Joining form' WHEN 'form_11' THEN 'Form 11'  WHEN 'form_2' THEN 'Form 2' WHEN 'bank_passbook' THEN 'Bank passbook' when 'passprt_size_photo' then 'Passport size photo' WHEN 'Employee_photo_upload'THEN 'Employee Id card' ELSE '' END AS 'image_name', pay_document_verification.image_path, pay_document_verification.comments,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' else '' end ) as 'Status' FROM pay_document_verification inner join pay_employee_master on pay_employee_master.emp_code = pay_document_verification.emp_code and  pay_employee_master.comp_code = pay_document_verification.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and  pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and  pay_employee_master.comp_code = pay_client_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' AND branch_status = 0 and pay_document_verification.image_name !='adhar_card_scanning' and client_active_close='0' ORDER BY pay_document_verification.id DESC", d.con);
            DataSet ds = new DataSet();
            cmd.Fill(ds);


            gv_emp_d_varification.DataSource = ds.Tables[0];
            gv_emp_d_varification.DataBind();
            d.con.Close();
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            d.con.Close();
        }
        if (!IsPostBack)
        {
            Panel_adhar.Visible = false;
        }
    }
    protected void ddl_adhardetails_Click(object sender, EventArgs e)
    {
        if (ddl_adhar_detail.SelectedValue == "Adhar")
        {
            d.con.Open();
            MySqlDataAdapter dr = new MySqlDataAdapter("select Id,emp_code,emp_name,field1,field2,field3,field4,field5,field6,field7,field8 from pay_document_verification where comp_code='" + Session["comp_code"].ToString() + "' and image_name='adhar_card_scanning' ", d.con);
            DataSet dt = new DataSet();
            dr.Fill(dt);
            gv_adnarcardscanning.DataSource = dt.Tables[0];
            gv_adnarcardscanning.DataBind();
            dr.Dispose();
            d.con.Close();
            Panel_adhar.Visible = true;

        }
        else
        {
            Panel_adhar.Visible = false;
        }

    }
    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if ((e.Row.Cells[10].Text == "Reject") || (e.Row.Cells[10].Text == "Approved"))
            {
                e.Row.Cells[11].Text = "";

            }
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl = "";
            if (dr["image_path"].ToString() != "")
            {
                imageUrl = "~/Temp_images/" + dr["image_path"];
                (e.Row.FindControl("Camera_image") as Image).ImageUrl = imageUrl;

            }

        }
    }

    protected void gv_emp_d_varification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String newpath = "";
        try
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            int request_id = (int)gv_emp_d_varification.DataKeys[gvrow.RowIndex].Value;


            //int index = gv_emp_d_varification.Rows[e.NewEditIndex].RowIndex;
            //int request_id = int.Parse(gv_emp_d_varification.DataKeys[index].Values[0].ToString());

            MySqlCommand cmd = new MySqlCommand("select emp_code,emp_name,image_name,image_path,reject from pay_document_verification where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + request_id + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string emp_code = dr.GetValue(0).ToString();
                string imagename = dr.GetValue(2).ToString();
                string path = dr.GetValue(3).ToString();
                int reject_id = Int32.Parse(dr.GetValue(4).ToString());

                if (reject_id == 0)
                {

                    if (imagename == "police_verification_form")
                    {
                       // MySqlCommand cmd1 = new MySqlCommand("select POLICE_VERIFICATION_DOC from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        MySqlCommand cmd1 = new MySqlCommand("select original_policy_document from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_policeverification_doc = dr.GetValue(0).ToString();
                            if (emp_policeverification_doc == "" || emp_policeverification_doc != "")
                            {

                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set original_policy_document='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(), reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }

                                //  System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);

                                d.con.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Police Verification Form  !!!')", false);
                                d.con.Close();
                            }
                        }
                    }

                    if (imagename == "adhar_card_upload")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_adharcard = dr.GetValue(0).ToString();
                            if (emp_adharcard == "" || emp_adharcard != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set original_adhar_card='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }
                    if (imagename == "Pan_card_upload")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select emp_adhar_pan from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_pancard = dr.GetValue(0).ToString();
                            if (emp_pancard == "" || emp_pancard != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";

                                int res = d.operation("update pay_images_master set emp_adhar_pan='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                                d.con.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Pan Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                    }
                }
                else if (reject_id == 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record  already Approve  !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already rejected  !!!')", true);

                }
            }
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Open();
            // MySqlDataAdapter cmd2 = new MySqlDataAdapter("select Id,emp_code,emp_name, case image_name when 'police_verification_form' then 'Police Verification form' when 'adhar_card_upload' then 'Adhar Card' when 'Pan_card_upload' then 'Pan Card' else 'Document upload' end as image_name,comments from pay_document_verification ", d.con);
            MySqlDataAdapter cmd2 = new MySqlDataAdapter("SELECT pay_document_verification.Id, pay_document_verification.emp_code, pay_document_verification.emp_name, pay_client_master.client_name  AS 'Client_Name', pay_unit_master.unit_name AS 'Unit_Name', pay_employee_master.LOCATION_CITY AS 'Working_city', CASE pay_document_verification.image_name WHEN 'police_verification_form' THEN 'Police Verification form' WHEN 'adhar_card_upload' THEN 'Adhar Card' WHEN 'Pan_card_upload' THEN 'Pan Card'  WHEN 'joining_form' THEN 'Joining form' WHEN 'form_11' THEN 'Form 11'  WHEN 'form_2' THEN 'Form 2' WHEN 'bank_passbook' THEN 'Bank passbook' when 'passprt_size_photo' then 'Passport size photo' ELSE 'Document upload' END AS 'image_name', pay_document_verification.image_path, pay_document_verification.comments FROM pay_document_verification inner join pay_employee_master on pay_employee_master.emp_code = pay_document_verification.emp_code and  pay_employee_master.comp_code = pay_document_verification.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and  pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and  pay_employee_master.comp_code = pay_client_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' ORDER BY pay_document_verification.id DESC", d.con);
            DataSet ds = new DataSet();
            cmd2.Fill(ds);
            gv_emp_d_varification.DataSource = ds.Tables[0];
            gv_emp_d_varification.DataBind();
        }
    }
    protected void gv_emp_d_varification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // ModalPopupExtender1.Show();

            // int index = gv_emp_d_varification.Rows[e.NewEditIndex].RowIndex;
            //int request_id = int.Parse(gv_emp_d_varification.DataKeys[index].Values[0].ToString());
            int request_id = int.Parse(gv_emp_d_varification.DataKeys[e.RowIndex].Values[0].ToString());

            MySqlCommand cmd = new MySqlCommand("select emp_code,emp_name,image_name,image_path from pay_document_verification where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + request_id + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string emp_code = dr.GetValue(0).ToString();
                string imagename = dr.GetValue(2).ToString();
                Session["EMP_CODE"] = emp_code;
                Session["request_id"] = request_id;
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Open();
            //MySqlDataAdapter cmd3 = new MySqlDataAdapter("select Id,emp_code,emp_name, case image_name when 'police_verification_form' then 'Police Verification form' when 'adhar_card_upload' then 'Adhar Card' when 'Pan_card_upload' then 'Pan Card' else 'Document upload' end as image_name ,comments,image_path from pay_document_verification ", d.con);
            MySqlDataAdapter cmd3 = new MySqlDataAdapter("SELECT pay_document_verification.Id, pay_document_verification.emp_code, pay_document_verification.emp_name, pay_client_master.client_name  AS 'Client_Name', pay_unit_master.unit_name AS 'Unit_Name', pay_employee_master.LOCATION_CITY AS 'Working_city', CASE pay_document_verification.image_name WHEN 'police_verification_form' THEN 'Police Verification form' WHEN 'adhar_card_upload' THEN 'Adhar Card' WHEN 'Pan_card_upload' THEN 'Pan Card'   WHEN 'joining_form' THEN 'Joining form' WHEN 'form_11' THEN 'Form 11'  WHEN 'form_2' THEN 'Form 2' WHEN 'bank_passbook' THEN 'Bank passbook' when 'passprt_size_photo' then 'Passport size photo' ELSE 'Document upload' END AS 'image_name', pay_document_verification.image_path, pay_document_verification.comments FROM pay_document_verification inner join pay_employee_master on pay_employee_master.emp_code = pay_document_verification.emp_code and  pay_employee_master.comp_code = pay_document_verification.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and  pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and  pay_employee_master.comp_code = pay_client_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' ORDER BY pay_document_verification.id DESC", d.con);
            DataSet ds = new DataSet();
            cmd3.Fill(ds);

            gv_emp_d_varification.DataSource = ds.Tables[0];
            gv_emp_d_varification.DataBind();
        }
    }
    protected void gv_emp_adhar_RowEditing(object sender, EventArgs e)
    {

    }
    protected void gv_emp_adhar_RowDeleting(object sender, EventArgs e)
    {

    }
    protected void gv_emp_d_varification_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_emp_d_varification.UseAccessibleHeader = false;
            gv_emp_d_varification.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    public void btn_close_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        int item = (int)gv_emp_d_varification.DataKeys[gvrow.RowIndex].Value;

        try
        {

            MySqlCommand cmd = new MySqlCommand("SELECT Id, comments from pay_document_verification  where comp_code='" + Session["comp_code"].ToString() + "' and  Id='" + item + "'  ", d.con);
            d.con.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                text_doc_id.Text = dr.GetValue(0).ToString();
                text_doc_comment.Text = dr.GetValue(1).ToString();

            }

        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();



        }
        docu_reject.Visible = true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string path = "";
        String emp_code = "";
        String imagename = "";
        int reject_id = 0;
        String newpath = "";
        string FileChk = "";
        try
        {

            MySqlCommand cmd = new MySqlCommand("select emp_code,emp_name,image_name,image_path,reject from pay_document_verification where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + text_doc_id.Text + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                emp_code = dr.GetValue(0).ToString();
                imagename = dr.GetValue(2).ToString();
                path = dr.GetValue(3).ToString();
                reject_id = Int32.Parse(dr.GetValue(4).ToString());
            }
            d1.con.Close();
            FileChk = Server.MapPath("~/Temp_images/") + path;
            //System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path); 
            if (reject_id == 0)
            {

                //if (File.Exists(FileChk))
                if (FileChk != "")
                {
                    //then file is exist
                    string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                    String newpath1233 = path.Replace(".png", "");
                    // String newpath = path.Remove(path.Length - 3);
                    newpath = newpath1233 + "_" + temp1 + ".png";
                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);

                    int res = d.operation("update pay_document_verification set comments='" + text_doc_comment.Text + "',cur_date=now() ,  image_path='" + newpath + "',reject='1', android_flag='1' where Id='" + text_doc_id.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'");
                    if (res > 0)
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Successfully.');", true);
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("alert('Record Reject Successfully');");
                        Response.Write("document.location.href='Emp_Document_Verification.aspx';");
                        Response.Write("</script>");
                        System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Reject Faill!!.');", true);
                    }
                }
                else { }
            }
            else if (reject_id == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already Approve  !!!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already rejected !!.');", true);
            }
            update_notification();

            string temp = d.getsinglestring("select EMP_EMAIL_ID from pay_employee_master where emp_code=(select  emp_code from pay_document_verification where  id='" + text_doc_id.Text + "')");
            if (temp != "")
            {
                // send_email();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee master page add employee email id..... !!.');", true);
            }


            //txt_comment.Text = "";
        }
        catch (Exception eq) { }
        finally
        {
            text_doc_comment.Text = "";
            // Response.Redirect("Emp_Document_Verification.aspx");
        }
    }
    private void update_notification()
    {
        try
        {
            d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Document Upload Is Rejected By " + Session["USERNAME"].ToString() + "','0','" + Session["emp_code"].ToString() + "','EmployeeMaster.aspx' ");


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

    //protected void send_email()
    //{
    //    MySqlCommand cmd = new MySqlCommand("select EMP_EMAIL_ID  from pay_employee_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + Session["emp_code"].ToString() + "'", d.con);
    //    d.con.Open();
    //    MySqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.Read())
    //    {
    //        string emp_mailid = dr.GetValue(0).ToString();




    //        string body = "Hello Your Document Is Rejected";
    //        using (MailMessage mailMessage = new MailMessage())
    //        {
    //            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");
    //            mailMessage.From = new MailAddress("info@celtsoft.com");

    //            mailMessage.Subject = "Documen Related";
    //            mailMessage.Body = body;
    //            mailMessage.IsBodyHtml = true;

    //            mailMessage.To.Add(emp_mailid);

    //            SmtpServer.Port = 587;
    //            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
    //            SmtpServer.EnableSsl = false;
    //            SmtpServer.Send(mailMessage);
    //            d.con.Close();
    //        }
    //        d.con.Close();
    //    }
    //}

    //protected void lnkbtn_edititem_Click(object sender, EventArgs e)
    //{
    //    GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
    //    int item = (int)gv_emp_d_varification.DataKeys[gvrow.RowIndex].Value;

    //    try
    //    {


    //        int res = d.operation("update pay_document_verification set comments='Approved Document' where  Id='" + item + "'");

    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {


    //    }
    //}
    protected void lnkbtn_edititem_Click(object sender, EventArgs e)
    {

        String newpath = "";
        try
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;
            int request_id = (int)gv_emp_d_varification.DataKeys[gvrow.RowIndex].Value;


            //int index = gv_emp_d_varification.Rows[e.NewEditIndex].RowIndex;
            //int request_id = int.Parse(gv_emp_d_varification.DataKeys[index].Values[0].ToString());

            MySqlCommand cmd = new MySqlCommand("select emp_code,emp_name,image_name,image_path,reject from pay_document_verification where comp_code='" + Session["COMP_CODE"].ToString() + "' and Id='" + request_id + "'", d1.con);
            d1.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string emp_code = dr.GetValue(0).ToString();
                string imagename = dr.GetValue(2).ToString();
                string path = dr.GetValue(3).ToString();
                int reject_id = Int32.Parse(dr.GetValue(4).ToString());

                if (reject_id == 0)
                {

                    if (imagename == "police_verification_form")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_policy_document from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_policeverification_doc = dr.GetValue(0).ToString();
                            if (emp_policeverification_doc == "" || emp_policeverification_doc != "")
                            {

                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set original_policy_document='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(), reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }

                                //  System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);

                                d.con.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Police Verification Form  !!!')", false);
                                d.con.Close();
                            }
                        }
                    }

                    if (imagename == "adhar_card_upload")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_adharcard = dr.GetValue(0).ToString();
                            if (emp_adharcard == "" || emp_adharcard != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set original_adhar_card='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }
                    if (imagename == "Pan_card_upload")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select emp_adhar_pan from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_pancard = dr.GetValue(0).ToString();
                            if (emp_pancard == "" || emp_pancard != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";

                                int res = d.operation("update pay_images_master set emp_adhar_pan='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                                d.con.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Pan Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                    }

                //for Employee photo  Approved.....


                    if (imagename == "Employee_photo_upload" || imagename == "passprt_size_photo")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select emp_id_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_pancard = dr.GetValue(0).ToString();
                            if (emp_pancard == "" || emp_pancard != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";

                                int res = d.operation("update pay_images_master set emp_id_card='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + path + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                                d.con.Close();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Pan Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                    }

                    // joining form
                    if (imagename == "joining_form")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_joininfForm = dr.GetValue(0).ToString();
                            if (emp_joininfForm == "" || emp_joininfForm != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set EMP_JOINING_FORM='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }

                    // Form 11 
                    if (imagename == "form_11")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_joininfForm = dr.GetValue(0).ToString();
                            if (emp_joininfForm == "" || emp_joininfForm != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set FORMNO_11='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }
                    // Form 2
                    if (imagename == "form_2")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_joininfForm = dr.GetValue(0).ToString();
                            if (emp_joininfForm == "" || emp_joininfForm != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set FORMNO_2='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }

                    // Bank passbook
                    if (imagename == "bank_passbook")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_joininfForm = dr.GetValue(0).ToString();
                            if (emp_joininfForm == "" || emp_joininfForm != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set bank_passbook='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }

                    // Bank passbook
                    if (imagename == "bank_passbook")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select original_adhar_card from pay_images_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con);
                        d.con.Open();
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            string emp_joininfForm = dr.GetValue(0).ToString();
                            if (emp_joininfForm == "" || emp_joininfForm != "")
                            {
                                string temp1 = d.getsinglestring("select coalesce(MAX(id), 0)+1 as id from pay_document_verification where emp_code='" + emp_code + "' and image_name='" + imagename + "'");
                                String newpath1233 = path.Replace(".png", "");
                                // String newpath = path.Remove(path.Length - 3);
                                newpath = newpath1233 + "_" + temp1 + ".png";


                                int res = d.operation("update pay_images_master set bank_passbook='" + path + "' where COMP_CODE='" + Session["comp_code"].ToString() + "' and emp_code='" + emp_code + "'");
                                if (res > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Approve Images Successfully !!!')", true);
                                    int res1 = d.operation("update pay_document_verification set comments='Approved Document',cur_date=now(),reject='2',android_flag='1', image_path='" + newpath + "' where Id='" + request_id + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'");
                                    System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/EMP_Images/") + path);
                                    System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                                    System.IO.File.Delete(Server.MapPath("~/Temp_images/") + path);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Images Approve Fail !!!')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Delete Previous Upload Document Of Adhar Card  !!!')", false);
                                d.con.Close();
                            }
                        }
                        d.con.Close();
                    }

                }
                else if (reject_id == 2)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record  already Approve  !!!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record already rejected  !!!')", true);

                }
            }
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Open();
            // MySqlDataAdapter cmd2 = new MySqlDataAdapter("select Id,emp_code,emp_name, case image_name when 'police_verification_form' then 'Police Verification form' when 'adhar_card_upload' then 'Adhar Card' when 'Pan_card_upload' then 'Pan Card' else 'Document upload' end as image_name,comments from pay_document_verification ", d.con);
            MySqlDataAdapter cmd2 = new MySqlDataAdapter("SELECT pay_document_verification.Id, pay_document_verification.emp_code, pay_document_verification.emp_name, pay_client_master.client_name  AS 'Client_Name', pay_unit_master.unit_name AS 'Unit_Name', pay_employee_master.LOCATION_CITY AS 'Working_city', CASE pay_document_verification.image_name WHEN 'police_verification_form' THEN 'Police Verification form' WHEN 'adhar_card_upload' THEN 'Adhar Card' WHEN 'Pan_card_upload' THEN 'Pan Card'   WHEN 'joining_form' THEN 'Joining form' WHEN 'form_11' THEN 'Form 11'  WHEN 'form_2' THEN 'Form 2' WHEN 'bank_passbook' THEN 'Bank passbook' when 'passprt_size_photo' then 'Passport size photo' ELSE 'Document upload' END AS 'image_name', pay_document_verification.image_path, pay_document_verification.comments,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' else '' end ) as 'Status' FROM pay_document_verification inner join pay_employee_master on pay_employee_master.emp_code = pay_document_verification.emp_code and  pay_employee_master.comp_code = pay_document_verification.comp_code inner join pay_unit_master on pay_employee_master.unit_code = pay_unit_master.unit_code and  pay_employee_master.comp_code = pay_unit_master.comp_code inner join pay_client_master on pay_client_master.client_code = pay_unit_master.client_code and  pay_employee_master.comp_code = pay_client_master.comp_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_document_verification.image_name !='adhar_card_scanning' ORDER BY pay_document_verification.id DESC", d.con);
            DataSet ds = new DataSet();
            cmd2.Fill(ds);
            gv_emp_d_varification.DataSource = ds.Tables[0];
            gv_emp_d_varification.DataBind();
        }
       
    }
}
