using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Imaging;

public partial class reliance_image_upload : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "') ORDER BY client_code", d.con);
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
              
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            ddl_client_SelectedIndexChanged(null, null);
            btn_save.Visible = false;
            btn_send_mail.Visible = false;
        }
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            //State
            ddl_state.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(STATE_NAME) from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' order by 1", d.con);
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
                ddl_state.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {

        }
    }
    protected void lnk_add_Click(object sender, EventArgs e)
    {
        /////////---------------------------------------EMP_PHOTO-------------------------------------------------------------

        //if (image_1.HasFile)
        //{
        //    string fileExt = System.IO.Path.GetExtension(image_1.FileName);
        //    if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
        //    {

        //            string fileName = Path.GetFileName(photo_upload.PostedFile.FileName);
        //            photo_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

        //            File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_1" + fileExt, true);
        //            File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
        //            d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_PHOTO = '" + txt_eecode.Text + "_1" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
        //    }

        //}
    }
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("select unit_name, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name = '" + ddl_state.SelectedValue + "' order by 1", d.con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            gv_image_upload.DataSource = ds.Tables[0];
            gv_image_upload.DataBind();
            d.con.Close();
            btn_save.Visible = true;
            btn_send_mail.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }
    }
    protected void gv_image_upload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void gv_image_upload_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_image_upload.UseAccessibleHeader = false;
            gv_image_upload.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_image_upload.Rows)
        {
            if (row != null)
            {
                for (int j = 1; j <= 5; j++)
                {
                    string cntrlname = "image_" + j.ToString();
                    FileUpload img = (FileUpload)row.FindControl(cntrlname);
                    if (img.HasFile)
                    {
                        string fileExt = System.IO.Path.GetExtension(img.FileName);
                        if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".jpeg")
                        {
                            string fileName = Path.GetFileName(img.PostedFile.FileName);
                            img.PostedFile.SaveAs(Server.MapPath("~/cleaning_images/") + fileName);
                            string strToday = DateTime.Today.ToString("ddMMyyyy");

                            //   File.Copy(Server.MapPath("~/cleaning_images/") + fileName, Server.MapPath("~/cleaning_images/") + row.Cells[1].Text + "_" + strToday + "_" + j.ToString() + fileExt, true);
                            VaryQualityLevel(Server.MapPath("~/cleaning_images/") + fileName, Server.MapPath("~/cleaning_images/") + row.Cells[1].Text + "_" + get_type(j) + "_" + strToday + "_" + j.ToString() + fileExt);
                            File.Delete(Server.MapPath("~/cleaning_images/") + fileName);

                            string count = d.getsinglestring("select id from pay_reliance_image_upload where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code='" + row.Cells[0].Text + "' and date1 = curdate()");
                            if (count.Equals(""))
                            {
                                d.operation("insert into pay_reliance_image_upload (comp_code,client_code,state_name,unit_code,image_" + j.ToString() + ",date1,last_modified_by) values ('" + Session["COMP_CODE"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + row.Cells[0].Text + "','" + row.Cells[1].Text + "_" + get_type(j) + "_" + strToday + "_" + j.ToString() + fileExt + "',now(),'" + Session["LOGIN_ID"].ToString() + "')");
                            }
                            else
                            {
                                d.operation("UPDATE pay_reliance_image_upload SET image_" + j.ToString() + " = '" + row.Cells[1].Text + "_" + get_type(j) + "_" + strToday + "_" + j.ToString() + fileExt + "',date1=now(),last_modified_by='" + Session["LOGIN_ID"].ToString() + "' where unit_code = '" + row.Cells[0].Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and date1=curdate()");
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Images uploaded successfully !!!')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
                        }
                    }
                }
            }
        }
    }
    protected void btn_send_mail_Click(object sender, EventArgs e)
    {
      string sender_name = d.getsinglestring("select emp_name from pay_employee_master where comp_code = '"+Session["COMP_CODE"].ToString()+"' and emp_code = '"+Session["LOGIN_ID"].ToString()+"'");
      string sender_mob_no = d.getsinglestring("select EMP_MOBILE_NO from pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and emp_code = '" + Session["LOGIN_ID"].ToString() + "'");
        string filename2;
        List<string> list4 = new List<string>();
        d.con.Open();
        MySqlCommand cmd1 = new MySqlCommand("select image_1,image_2,image_3,image_4,image_5 from pay_reliance_image_upload where date1=curdate() and comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ", d.con);
        MySqlDataReader dr1 = cmd1.ExecuteReader();
        while (dr1.Read())
        {
            string path3 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "cleaning_images\\");
            for (int i = 0; i <= 4; ++i)
            {
                if (!dr1.GetValue(i).ToString().Equals(""))
                {
                    filename2 = path3 + dr1.GetValue(i).ToString();
                    list4.Add(filename2);
                }
            }
        }
        dr1.Close();
        d.con.Close();
        if (list4.Count == 0) 
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please upload images first !!!')", true);
            return;
        }
        string from_emailid = "", password = "";
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("select email_id,password from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_client.SelectedValue + "' ", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                from_emailid = dr.GetValue(0).ToString();
                password = dr.GetValue(1).ToString();
            }
            dr.Close();
            d.con.Close();

            if (!(from_emailid == "") || !(password == ""))
            {
                string body = "<B><FONT COLOR=\"#17365d\"><FONT FACE=\"Verdana, serif\"><FONT SIZE=2>Dear Sir,<p>Greetings from Team IH&MS..!!!<p>We take immense pleasure in updating you about your branch Hygiene and Cleaning Pictures as received by us from our team on day to day basis from each location.<p>We are keeping a close watch on the same and hence if you have any concerns please let us know so that we can get the same streamlined on priority.<p>Assuring you of our best of services as always.<p>Thanks & Regards,<br />"+sender_name.ToUpper()+"<br />Admin and OPS<br />Mobile - "+sender_mob_no+"<br />Immediate Manager - Jayati Roy(jayatiroy@ihmsindia.com)</FONT></FONT></FONT></B>";
                //5mb per email
                long filesize = 0, total_file_size = 5 * 1024 * 1024;
                using (MailMessage mailMessage = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mailMessage.From = new MailAddress(from_emailid);
                    List<string> list1 = new List<string>();
                    string gg1 = txt_email_id.Text;
                    list1.Add(gg1);

                    foreach (string file1 in list4)
                    {
                        filesize = filesize + new FileInfo(file1).Length;
                        if (filesize < total_file_size)
                        {
                            mailMessage.Attachments.Add(new Attachment(file1));
                        }
                        else
                        {
                            foreach (string mail in list1)
                            {
                                mailMessage.To.Add(mail);
                                mailMessage.Subject = "Updates for Branch Cleaning Activities for Better Upkeep and Maintenance of Branches";
                                mailMessage.Body = body;
                                mailMessage.IsBodyHtml = true;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                                SmtpServer.EnableSsl = true;
                                SmtpServer.Send(mailMessage);
                            }
                            filesize = 0;
                            mailMessage.Attachments.Clear();
                            mailMessage.Attachments.Add(new Attachment(file1));
                        }
                    }

                    foreach (string mail in list1)
                    {
                        mailMessage.To.Add(mail);
                        mailMessage.Subject = " Updates for Branch Cleaning Activities for Better Upkeep and Maintenance of Branches";
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(from_emailid, password);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mailMessage);
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Email sent successfully !!!')", true);
        }
        catch (Exception ex)
        {
            //throw ex;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Issue in sending Email, Please retry !!!')", true);
        }
        finally
        {
            d.con.Close();

        }

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    private void VaryQualityLevel(string from_file, string to_file)
    {
        // Get a bitmap. The using statement ensures objects  
        // are automatically disposed from memory after use.  
        using (Bitmap bmp1 = new Bitmap(from_file))
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID  
            // for the Quality parameter category.  
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.  
            // An EncoderParameters object has an array of EncoderParameter  
            // objects. In this case, there is only one  
            // EncoderParameter object in the array.  
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(to_file, jpgEncoder, myEncoderParameters);

            //myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"D:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

            //// Save the bitmap as a JPG file with zero quality level compression.  
            //myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"D:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
        }
    }
    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }

    private string get_type(int j)
    {
        if (j == 1)
        {
            return "Reception";
        }
        else if (j == 2)
        {
            return "Washroom";
        }
        else if (j == 3)
        {
            return "Pantry";
        }
        else if (j == 4)
        {
            return "Common_Area1";
        }
        else
        {
            return "Common_Area2";
        }

    }
    protected void btn_report_Click(object sender, EventArgs e)
    {
        try {
            MySqlDataAdapter dscmd = new MySqlDataAdapter("select client_name, unit_name, if(image_1 is null,'NO','YES') as image_1,if(image_2 is null,'NO','YES') as image_2,if(image_3 is null,'NO','YES') as image_3,if(image_4 is null,'NO','YES') as image_4,if(image_5 is null,'NO','YES') as image_5, date_format(pay_reliance_image_upload.date1,'%d/%m/%Y') as date1 from pay_unit_master inner join pay_client_master on pay_unit_master.client_code = pay_client_master.client_code and pay_unit_master.comp_code = pay_client_master.comp_code left join pay_reliance_image_upload on pay_unit_master.unit_code = pay_reliance_image_upload.unit_code and pay_unit_master.comp_code = pay_reliance_image_upload.comp_code where pay_client_master.client_code = '"+ddl_client.SelectedValue+"' and pay_client_master.comp_code = '"+Session["COMP_CODE"].ToString()+"' order by 2", d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);
            // int days = 0;


            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                
                    Response.AddHeader("content-disposition", "attachment;filename=WORKING_IMAGES.xls");
               

                string path = Server.MapPath("~/EMP_Images");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Repeater Repeater1 = new Repeater();
                Repeater1.DataSource = ds;
                Repeater1.HeaderTemplate = new MyTemplate(ListItemType.Header, null);
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
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
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
                    lc = new LiteralControl("<table border=1><tr><th>#</th><th>CLIENT NAME</th><th>BRANCH</th><th>RECEPTION</th><th>WASHROOM</th><th>PANTRY</th><th>COMMON AREA1</th><th>COMMON AREA2</th><th>DATE</th></tr>");
                    break;
                case ListItemType.Item:
                    lc = new LiteralControl("<tr><td>" + (ctr + 1) + "</td><td>" + ds.Tables[0].Rows[ctr]["client_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["unit_name"] + "</td><td>" + ds.Tables[0].Rows[ctr]["image_1"] + "</td><td>" + ds.Tables[0].Rows[ctr]["image_2"] + "</td><td>" + ds.Tables[0].Rows[ctr]["image_3"] + "</td><td>" + ds.Tables[0].Rows[ctr]["image_4"] + "</td><td>" + ds.Tables[0].Rows[ctr]["image_5"] + "</td><td>" + ds.Tables[0].Rows[ctr]["date1"] + "</td></tr>");
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
}