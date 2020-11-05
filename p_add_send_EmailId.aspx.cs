using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class p_add_send_EmailId : System.Web.UI.Page
{
    DAL d = new DAL();
    string comp_name;
    string from_emailid;
    string password;
    string phon_no;
    string filename;
    string filename2;

    string filename1;

   

    string designation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["client_code"] != "" && Session["client_code"] != "NULL" && Session["unit_code1"] != "" && Session["unit_code1"] != "NULL")
            {
                d.con.Open();
                MySqlDataAdapter cmd_email = new MySqlDataAdapter("SELECT id,head_type,head_name,head_email_id FROM pay_branch_mail_details WHERE client_code ='" + Session["client_code"].ToString() + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and  unit_code='" + Session["unit_code1"].ToString() + "'", d.con);
                //    MySqlDataAdapter cmd_email = new MySqlDataAdapter("SELECT Field1,Field2, Field3, Field4 FROM pay_zone_master WHERE client_code ='DD'  AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and Type = 'EMAIL' ", d.con);
                DataSet ds = new DataSet();
                cmd_email.Fill(ds);
                gv_send_email.DataSource = ds;
                gv_send_email.DataBind();
                d.con.Close();
            }
        }
    }

    protected void gv_emailid_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_send_email, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;

    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
         string unit_code = Session["unit_code"].ToString();
           // string filename = "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
            //string document1_file = Convert.ToString(filename);
            upload_documents(document1_file);
    }
    protected void ddl_client_phonno()
    {
        d.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("select client_phonno from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "'and client_code='" + Session["CLIENT_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                phon_no = dr.GetValue(0).ToString();
            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void upload_documents(FileUpload document_file)
    {
       // string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\");

        string filename = "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
        if (document_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(document_file.FileName);
            if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".pdf")
            {
                string fileName = Path.GetFileName(document_file.PostedFile.FileName);
                document_file.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);

                File.Copy(Server.MapPath("~/Images/") + fileName, Server.MapPath("~/Images/") + Session["COMP_CODE"].ToString() + "_" + Session["CLIENT_CODE"].ToString() + "_" + fileExt, true);
                File.Delete(Server.MapPath("~/Images/") + fileName);

                // d.operation("insert into pay_images (comp_code,client_code,created_by,created_date) values ('" + Session["COMP_CODE"].ToString() + "','" + Session["CLIENT_CODE"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "',now())");
                d.operation("UPDATE pay_client_master SET images_document = '" + Session["client_code"].ToString() + "_1" + fileExt + "' where client_code = '" + Session["client_code"].ToString() + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'"); d.operation("UPDATE pay_client_master SET images_document = '" + Session["CLIENT_CODE"].ToString() + "_1" + fileExt + "' where client_code = '" + Session["CLIENT_CODE"].ToString() + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Upload Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG, PNG and PDF Files !!!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Document !!')", true);
        }
    }


    private void ValidateEmail(string email)
    {
       // string emailid = "";
         email = txt_cc.Text;
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        System.Text.RegularExpressions.Match match = regex.Match(email);
        if (match.Success)
        { }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Valid MailId !!')", true);
            //return emailid;
        }
    }

    protected void btn_mailsend_click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            // MySqlCommand cmd = new MySqlCommand("select DESIGNATION  from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and unit_code='" + Session["unit_code1"].ToString() + "' and client_code='" + Session["CLIENT_CODE"].ToString() + "'", d.con);
            MySqlCommand cmd = new MySqlCommand("select DESIGNATION ,COMPANY_NAME from pay_designation_count INNER JOIN pay_company_master on pay_designation_count.comp_code=pay_company_master.comp_code  where pay_designation_count.comp_code='" + Session["comp_code"].ToString() + "' and unit_code='" + Session["unit_code1"].ToString() + "' and client_code='" + Session["CLIENT_CODE"].ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                designation = (dr.GetValue(0).ToString());
                comp_name = (dr.GetValue(1).ToString());

                if (designation == "HOUSEKEEPING")
                {
                    designation = "HK";
                }
                else
                {
                    designation = "SG";
                }
            }
            dr.Close();
        }
        catch (Exception ae)
        {
        }
        finally
        {
            d.con.Close();
        }
        char[] seperator1 = { ',' };

        string abc = txt_cc.Text;
        string[] list21 = abc.Split(seperator1);

        try
        {
            List<int> list = new List<int>();
            List<string> list1 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();
            foreach (GridViewRow row in gv_send_email.Rows)
            {
                var check = row.FindControl("chk_id") as CheckBox;
                if (check.Checked)
                {
                    int gg = int.Parse(row.Cells[0].Text);
                    string gg1 = row.Cells[4].Text;
                    string gg2 = txt_cc.Text;

                    char[] seperator = { ',' };
                    string[] list2 = abc.Split(seperator);

                    foreach (string item in list2)
                    {
                        list3.Add(item);
                        //  ValidateEmail(item);
                    }
                    list.Add(gg);
                    list1.Add(gg1);

                    d.con.Open();
                    MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + Session["CLIENT_CODE"].ToString() + "'", d.con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        from_emailid = dr.GetValue(0).ToString();
                        password = dr.GetValue(1).ToString();

                    }
                    dr.Close();
                    d.con.Close();
                    ddl_client_phonno();
                    string body = "<tr><td style = \"font-family:Georgia;font-size:12pt;\">Dear Sir / Madam,</td></tr><tr><td style = \"font-family:Georgia;font-size:12pt;\">Greetings from IH&MS...!!!</td></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Attached herewith the attendance for the Month of " + Session["YEAR_MONTH"].ToString() + ".</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Please consider the excel file and get it corrected if required. Also it is compulsory to send  the scan copy of the register with in & out timing of the employees.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly send the same and also attach the scan copy of in and out attendance register and send ASAP with the Signature of Branch Head & Branch's Stamp.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration: underline;\">Note:-   Please take care with the below mention notes. As it is mandatory.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">1)Please mention in & out time by manually in attendance sheet. <br>2)Please use round stamp with full address stamp for <span style = \"font-family:Georgia;font-size:10pt;\">" + Session["client_code1"].ToString() + " </span>on  Attendance Sheet. <br> 3) Attendance is valid only by branch Manager Sign.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt; color:red;text-decoration:underline;\">Note :- Please send the attendance sheet with clear print if it is not clear i will not  mention the attendance sheet.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt;\">Kindly note if there is no stamp available at the branch  Please give us the confirmation over the mail regarding the non availability of official stamp and the HK employees total working days along with the attached attendance format.</p></tr><tr><p style = \"font-family:Georgia;font-size:12pt\">Thanks & Regards</p></tr><tr><p style = \"font-size:10pt;\">" + Session["USERNAME"].ToString() + "<br>Asst. Manager- Administration<br>'" + comp_name + "'<br><span style = \"color:red\">(An ISO 9001-2008 Certified Company)</span><br>304, 3rd Floor, Nyati Millennium,Viman Nagar, Pune-411014 <br>Tel: " + phon_no + "</p></tr>";
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mailMessage.From = new MailAddress(from_emailid);

                        if (Session["client_code"].ToString() == "HDFC")
                        {
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            //Attachment atch = new Attachment(filename1);
                            // mailMessage.Attachments.Add(atch);

                            list4.Add(filename1);

                            string path2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            string filename = path + "OT_Details_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename);

                            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            string filename2 = path3 + "HDFC_FEEDBACK_FORM" + ".xlsx";
                            list4.Add(filename2);

                        }
                        else if (Session["client_code"].ToString() == "UTKARSH")
                        {
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename1);

                            string path2 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename = path2 + "CheckList_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename);

                            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename2 = path3 + "FEED_BACK_FORM_UTKRASH" + ".pdf";
                            list4.Add(filename2);
                        }
                        else if (Session["client_code"].ToString() == "RLIC HK")
                        {
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename1);


                            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename2 = path3 + "Feedback_for_Reliance" + ".xlsx";
                            list4.Add(filename2);
                        }
                        else if (Session["client_code"].ToString() == "SUD")
                        {
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename1);


                            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename2 = path3 + "FEEDBACK" + ".xls";
                            list4.Add(filename2);
                        }
                        else if (Session["client_code"].ToString() == "BAG" || Session["client_code"].ToString() == "BG" || Session["client_code"].ToString() == "BALIC SG" || Session["client_code"].ToString() == "BALIC HK")
                        {
                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename1);


                            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            // string filename2 = path3 + "Bajaj_Feedback_Form" + ".xls";
                            filename2 = path3 + "Bajaj_Feedback_Form" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename2);
                        }
                        else
                        {

                            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Downloads\\");
                            filename1 = path + "Attendance_Copy_" + Session["unit_code"].ToString() + ".xls";
                            list4.Add(filename1);

                        }



                        foreach (string mail in list4)
                        {
                            mailMessage.Attachments.Add(new Attachment(mail));
                        }
                        // System.IO.File.Delete(filename2);



                        foreach (string mail in list1)
                        {
                            mailMessage.To.Add(mail.ToString());
                        }
                        if (txt_cc.Text != "")
                        {
                            foreach (string CCEmail in list3)
                            {
                                mailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                            }
                        }
                        mailMessage.Subject = "Attendance sheet for " + designation + " Employee for the month of " + Session["YEAR_MONTH"].ToString() + " for " + Session["unit_code"].ToString() + " ";
                        mailMessage.Body = body;

                        mailMessage.IsBodyHtml = true;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mailMessage);

                        d.operation("update pay_unit_master set flag='1' ,month_year='" + Session["year"].ToString() + "' where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + Session["client_code"] + "' and UNIT_CODE='" + Session["unit_code1"].ToString() + "'");
                        list1.Clear();
                        list3.Clear();
                        list4.Clear();


                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
            System.IO.File.Delete(filename1);
            if (Session["client_code"].ToString() == "BAG" || Session["client_code"].ToString() == "BG" || Session["client_code"].ToString() == "BALIC SG" || Session["client_code"].ToString() == "BALIC HK")
            {
                System.IO.File.Delete(filename2);
                System.IO.File.Delete(filename1);
            }
            else if (Session["client_code"].ToString() == "UTKARSH" || Session["client_code"].ToString() == "HDFC")
            {
                System.IO.File.Delete(filename);
            }
            else if (Session["client_code"].ToString() == "HDFC")
            {
                System.IO.File.Delete(filename1);
                System.IO.File.Delete(filename);
            }


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send Faill!!');", true);
        }
        finally
        {
            d.con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send Successfully.');", true);
            txt_cc.Text = "";
            foreach (GridViewRow row in gv_send_email.Rows)
            {
                var check = row.FindControl("chk_id") as CheckBox;
                if (check.Checked)
                {
                    check.Checked = false;
                }

            }
            if (Session["client_code"].ToString() == "BAG" || Session["client_code"].ToString() == "BG" || Session["client_code"].ToString() == "BALIC SG" || Session["client_code"].ToString() == "BALIC HK")
            {
                System.IO.File.Delete(filename2);
                System.IO.File.Delete(filename1);


            }
            else if (Session["client_code"].ToString() == "HDFC")
            {
                System.IO.File.Delete(filename);
            }
            else if (Session["client_code"].ToString() == "HDFC")
            {
                System.IO.File.Delete(filename1);
                System.IO.File.Delete(filename);
            }
            else
            {
                System.IO.File.Delete(filename1);
            } if (Session["client_code"].ToString() == "BAG" || Session["client_code"].ToString() == "BG" || Session["client_code"].ToString() == "BALIC SG" || Session["client_code"].ToString() == "BALIC HK")
            {
                System.IO.File.Delete(filename2);
            }
            else if (Session["client_code"].ToString() == "UTKARSH" || Session["client_code"].ToString() == "HDFC")
            {
            }

        }
    
    }
    protected void gv_send_email_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_send_email.UseAccessibleHeader = false;
            gv_send_email.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}