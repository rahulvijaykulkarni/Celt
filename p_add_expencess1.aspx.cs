using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Web;

public partial class Default2 : System.Web.UI.Page
{
    DAL d = new DAL();
    int counter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
        if (!IsPostBack)
        {
            ViewState["save"] = "0";
            if (!Session["EXPENSE_ID"].ToString().Equals(""))
            {
                load_gridview(Session["EXPENSE_ID"].ToString());
            }
            else
            {
                gvexpenessadd.DataSource = null;
                gvexpenessadd.DataBind();
               // getexpenseid(Session["EXPENSE_ID"].ToString());
            }
            btn_submit.Visible = false;
            rate.Visible = false;
            type.Visible = false;
            load_city();
        }
       

    }
    private void getexpenseid(string expense_id)
    {
        if (expense_id.Equals(""))
        {
            try
            {
                d.con1.Open();
                MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTR(expenses_id, 3, 5) AS unsigned))+1 FROM  apply_travel_plan", d.con1);
                MySqlDataReader drmax = cmdmax.ExecuteReader();
                if (!drmax.HasRows)
                {
                }
                else if (drmax.Read())
                {
                    string str = drmax.GetValue(0).ToString();
                    if (str == "")
                    {
                        Session["EXPENSE_ID"] = "EP01";
                    }
                    else
                    {
                        int max_compcode = int.Parse(drmax.GetValue(0).ToString());
                        if (max_compcode < 10)
                        {
                            Session["EXPENSE_ID"] = "EP0" + max_compcode;

                        }
                        else if (max_compcode > 9 && max_compcode < 100)
                        {
                            Session["EXPENSE_ID"] = "EP" + max_compcode;

                        }
                        else
                        {
                        }
                    }
                    drmax.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { d.con1.Close(); }
        }


    }
    private void update_notification()
    {
        try
        {
            if (ViewState["save"].ToString().Equals("1"))
            {
                d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Expense Updated By Employee" + Session["USERNAME"].ToString() + "','0',(select reporting_to from pay_employee_master b where b.emp_code = a.EMP_CODE),'app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1");
            }
            else
            { d.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select 'Travel Expense Added By Employee" + Session["USERNAME"].ToString() + "','0',(select reporting_to from pay_employee_master b where b.emp_code = a.EMP_CODE),'app_rej_travelplan.aspx' from apply_travel_plan a where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "' limit 1"); }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlparticular.SelectedValue == "Accomodation")
            {
                string hotel_id = "" + ddl_travelmode.SelectedValue + "";

                string from_amt = d.getsinglestring("select (" + hotel_id + "_from) from pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"] + "') ");
                string to_amt = d.getsinglestring("select (" + hotel_id + "_to) from pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"] + "') ");

                //int from_amount = Int32.Parse(from_amt);
                //int to_amount = Int32.Parse(to_amt);
                //int txt = Int32.Parse(txt_amount.Text);

                double from_amount = Convert.ToDouble("" + from_amt + "");
                double to_amount = Convert.ToDouble("" + to_amt + "");
                double txt = Convert.ToDouble("" + txt_amount.Text + "");

                if (from_amount > txt)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Hotel Accomodation Limit is greater than " + from_amount + "!!!')", true);
                    return;
                }
                else if (to_amount < txt)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Hotel Accomodation Limit is Less than " + to_amount + " !!!')", true);
                    return;
                }

            }
           
            string text_box = "";
            if (ddl_type.SelectedValue != "Select")
            {
                if (ddlparticular.SelectedValue == "Travel")
                {
                    if (ddl_travelmode.SelectedValue != "Owned Vehicle")
                    {

                        text_box = "" + ddl_type.SelectedValue + "";
                        string db_amount = d.getsinglestring("select " + text_box + " from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");

                        double val1 = Convert.ToDouble("" + txt_amount.Text + "");
                        double val2 = Convert.ToDouble("" + db_amount + "");

                        if (val1 > val2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the limit " + val2 + "')", true);
                            return;
                        }
                    }
                }
            }

            string db_limit = "";
            string txt1 = "" + txt_amount.Text + "";
            double val = 0;
            double value = 0;
            if (ddl_city.SelectedValue == "1")
            {
                db_limit = d.getsinglestring("select txt_per_day_limit from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");
            }
            else
            {
                db_limit = d.getsinglestring("select Textbox_outside from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')");
            }
            string city_type = d.getsinglestring("select SUM(Amount) as 'Amount' FROM pay_add_expenses WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "' and date=STR_TO_DATE('" + txt_date.Text + "' ,'%d/%m/%Y')");
            val = Convert.ToDouble("" + db_limit + "");
           
            double val4 = Convert.ToDouble("" + txt1 + "");
            if (city_type == "")
            {
                double tot_amt = value + val4;

                if (tot_amt > val)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the per day limit " + db_limit + "')", true);
                    return;
                }
                //else if (tol_amt>val)
                //{
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the per day limit " + db_limit + "')", true);
                //    return;
                
                //}

            }
            else
            {
                value = Convert.ToDouble("" + city_type + "");
                if (val < value)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You can not exeed beyond the per day  limit " + db_limit + "')", true);
                    return;
                }
            }

            if (ViewState["save"].ToString().Equals("1"))
            {
                btnupdate_Click(null, null);
            }
            else
            {
               // if (Upload_File(0))
                //{
                    ViewState["ID"] = "1";
                    if (Session["EXPENSE_ID"].ToString().Equals(""))
                    {
                        getexpenseid(Session["EXPENSE_ID"].ToString());
                        d.con1.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT date_format(Date,'%d/%m/%Y') as 'Date' FROM pay_add_expenses  WHERE emp_code= '" + Session["LOGIN_ID"].ToString() + "' and expenses_id != '" + Session["EXPENSE_ID"].ToString() + "' and date='" + txt_date.Text + "' ", d.con1);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The Travel Plan already exist from " + dr.GetValue(0).ToString() + " Select another date.');", true);
                            return;
                        }
                        if (Upload_File(0))
                        {
                            d.operation("INSERT INTO apply_travel_plan(comp_code,unit_code,emp_code,travel_mode,exception_case,from_designation,to_designation,adv_amount,expense_status,modified_by,curreny_id,expenses_id,type,travel_type,city_type) VALUES('" + Session["COMP_CODE"].ToString() + "','" + Session["UNIT_CODE"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','NA','NA','NA','NA',0,'NA',(select emp_name from pay_employee_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "'), 'NA','" + Session["EXPENSE_ID"].ToString() + "','" + ddl_type.SelectedValue + "','"+ddl_travelmode.SelectedValue+"','"+ddl_city.SelectedValue+"')");
                        }
                    }
                    else
                    {
                        Upload_File(0);
                    }
                   
                    
                    
            }
            //if (photo_upload.HasFile)
            //{
            //    string fileExt = "";
            //    string bill_upload1 = "";
             
            //    fileExt = System.IO.Path.GetExtension(photo_upload.FileName);
            //    bill_upload1 = Path.GetFileName(photo_upload.PostedFile.FileName);

            //    string fname = null;
            //    if (fileExt.ToUpper() == ".JPG" || fileExt.ToUpper() == ".PNG" || fileExt.ToUpper() == ".PDF" || fileExt.ToUpper() == ".JPEG" || fileExt.ToUpper() == ".ZIP")
            //    {
            //        string fileName = bill_upload1;
            //        fname = d.getsinglestring("select Expness_Image from pay_add_expenses where  Expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
            //        photo_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);
            //        File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + fname, true);
            //        File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
            //    }
            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Expenses Saved Successfully');", true);
            text_clear();
            //ddl_type.Items.Clear();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            load_gridview(Session["EXPENSE_ID"].ToString());
        }
        // update_notification();
        btn_submit.Visible = true;
    }

    private void load_gridview(string expenses_Id_edit)
    {
        get_travel_plan();
        double a = 0, c = 0, b = 0, e=0 ;
        try
        {

            MySqlCommand cmd1 = new MySqlCommand("select id,Expenses_id,COMP_CODE,Emp_code,date_format(Date,'%d/%m/%Y') as Date,case when city_type=1 then 'Inside City' when city_type=2 then 'Outside City' end as 'city_type',Merchant,travel_type,type,Amount,description,status,comments,concat('~/EMP_Images/',Expness_Image) as 'Value',travel_amount,app_amount,particular from pay_add_expenses where Expenses_id='" + expenses_Id_edit + "' ", d.con);
            d.con.Open();
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Load(dr1);
                foreach (DataRow row in dt.Rows)
                {
                    a = double.Parse(row["Amount"].ToString());
                    c = c + a;
                    a = double.Parse(row["travel_amount"].ToString());
                    b = b + a;
                    a = double.Parse(row["app_amount"].ToString());
                    e = e + a;
                    if (!row["status"].ToString().Equals("Draft"))
                    {
                        btn_add.Visible = false;
                        btn_submit.Visible = false;
                        counter = 1;
                    }
                    else
                    {
                        btn_add.Visible = true;
                        btn_submit.Visible = true;
                    }

                }
                gvexpenessadd.DataSource = dt;
                gvexpenessadd.DataBind();
                gvexpenessadd.Visible = true;

                gvexpenessadd.FooterRow.Cells[8].Text = "Total:";
                gvexpenessadd.FooterRow.Cells[9].Text = b.ToString();
                gvexpenessadd.FooterRow.Cells[9].Visible = true;
                gvexpenessadd.FooterRow.Cells[10].Text = c.ToString();
                gvexpenessadd.FooterRow.Cells[10].Visible = true;
                gvexpenessadd.FooterRow.Cells[11].Text = e.ToString();
                gvexpenessadd.FooterRow.Cells[11].Visible = true;
            }
            else
            {
                gvexpenessadd.DataSource = null;
                gvexpenessadd.DataBind();
            }

            dr1.Close();
            ViewState["travel"] = "0";
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
        gvexpenessadd.Visible = true;
        //tn_submit.Visible = true;
    }

    protected void expenses_data_edit(object sender, EventArgs e)
    {
        try
        {
            string expenses_Id_edit = gvexpenessadd.SelectedRow.Cells[3].Text;
            d.con1.Open();

            MySqlCommand cmd1 = new MySqlCommand("select id,Expenses_id,date_format(Date,'%d/%m/%Y') as Date,city_type,Merchant,Amount,description,Expness_Image,travel_amount,particular,travel_type,type from pay_add_expenses where id=" + expenses_Id_edit, d.con1);
            MySqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                txt_date.Text = dr.GetValue(2).ToString();
                ddl_city.SelectedValue = dr.GetValue(3).ToString();

                txt_merchant.Text = dr.GetValue(4).ToString();
                txt_amount.Text = dr.GetValue(5).ToString();
                txt_description.Text = dr.GetValue(6).ToString();

                if (!DBNull.Value.Equals(dr.GetValue(7)))
                {

                    Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(7).ToString();
                    Image4.Visible = true;
                   
                }
                else
                {
                    
                    Image4.Visible = false;
                }


                ViewState["ID"] = dr.GetValue(0).ToString();
                ViewState["expense_id"] = dr.GetValue(1).ToString();
                ViewState["save"] = "1";
                ViewState["travel"] = dr.GetValue(8).ToString();
                ddlparticular.SelectedValue = dr.GetValue(9).ToString();
                ddl_travelmode.SelectedItem.Text = dr.GetValue(10).ToString();
                ddl_type.SelectedItem.Text = dr.GetValue(11).ToString();
                //ddl_travelmode.SelectedValue = dr.GetValue(10).ToString();
                //ddl_type.SelectedValue = dr.GetValue(11).ToString();
            }
            dr.Close();
            cmd1.Dispose();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con1.Close();
            btn_submit.Visible = true;
        }

    }
    protected void gv_paymentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (photo_upload.HasFile)
            {
                if (Upload_File(int.Parse(ViewState["ID"].ToString())))
                {
                    int result = d.operation("UPDATE pay_add_expenses SET Date=str_to_date('" + txt_date.Text + "','%d/%m/%Y'),Merchant='" + txt_merchant.Text + "',Amount ='" + txt_amount.Text + "',Description='" + txt_description.Text + "' ,particular='" + ddlparticular.SelectedValue + "' WHERE id = " + int.Parse(ViewState["ID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Updated Successfully!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Updation Failed!!');", true);
                    }
                }
            }
                else
                { 
                    int result = d.operation("UPDATE pay_add_expenses SET Date=str_to_date('" + txt_date.Text + "','%d/%m/%Y'),Merchant='" + txt_merchant.Text + "',Amount ='" + txt_amount.Text + "',Description='" + txt_description.Text + "' ,particular='"+ddlparticular.SelectedValue+"' WHERE id = " + int.Parse(ViewState["ID"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Updated Successfully!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Updation Failed!!');", true);
                    }
                }
                text_clear();
               // update_notification();
                load_gridview(Session["EXPENSE_ID"].ToString());
            }
        
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
        }

        //  d.SendHtmlexpenseEmail(Server.MapPath("~/travelexpense.htm"), Session["EXPENSE_ID"].ToString(), 0, "");
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        d.operation("delete from pay_add_expenses where id = " + int.Parse(grdrow.Cells[3].Text));
        load_gridview(Session["EXPENSE_ID"].ToString());
    }
    protected void txt_amount_Click(object sender, EventArgs e)
    {
       // if (ViewState["travel"].ToString().Equals("0"))
       // {
            //amountvalidation();
       // }
    }
    protected void amountvalidation()
    {
        try
        {
            

            double total_expenses_amt = 0;
            double per_day_amt = 0;
            double final = 0;
            MySqlCommand cmd1 = new MySqlCommand("select case when  IsNull(sum(Amount)) then 0 else sum(Amount) END from pay_add_expenses where  Date = STR_TO_DATE('" + txt_date.Text + "','%d/%m/%Y') and emp_code='" + Session["LOGIN_ID"] + "' and travel_amount=0", d.con);

            d.con.Open();
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                total_expenses_amt = Convert.ToDouble(dr1[0].ToString());
                double amt = Convert.ToDouble(txt_amount.Text);
                final = total_expenses_amt + amt;

            }
            dr1.Close();
            MySqlCommand cmd2 = new MySqlCommand("select case when  IsNull(txt_per_day_limit) then 0 else  txt_per_day_limit END from  pay_travel_policy_master,pay_travel_emp_policy where pay_travel_policy_master.ID=pay_travel_emp_policy.policy_id and Emp_code = '" + Session["LOGIN_ID"] + "'", d.con);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                string day_amt = dr2[0].ToString();
                if (day_amt.Equals(null) || day_amt.Equals("")) { day_amt = "0"; }
                per_day_amt = Convert.ToDouble(day_amt);
            }

            if (final > per_day_amt)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Claim Amount Less Than Per Day Limit Amount is '+'" + per_day_amt + "'+'  ');", true);
                //txt_amount.Text = "0";
                //txt_amount.Focus();
                return;
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
             finally
        {
            load_gridview(Session["EXPENSE_ID"].ToString());
        }
        // update_notification();
        btn_submit.Visible = true;
    }
    protected void datevalidation()
    {
        try
        {
            int total_days;
            MySqlCommand cmd = new MySqlCommand("select txt_claim_max_days from  pay_travel_policy_master,pay_travel_emp_policy where pay_travel_policy_master.ID=pay_travel_emp_policy.policy_id and Emp_code = '" + Session["LOGIN_ID"] + "'", d.con1);

            d.con1.Open();
            MySqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                string claim_days = dr1[0].ToString();
                if (claim_days.Equals(null) || claim_days.Equals("")) { claim_days = "1"; }
                total_days = Convert.ToInt32(claim_days);
                DateTime dateTime1, dateTime2;
                if (DateTime.TryParseExact(txt_date.Text, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime1))
                {
                    dateTime2 = DateTime.Now;
                    TimeSpan difference = dateTime1 - dateTime2;
                    int Days1 = Convert.ToInt32(difference.TotalDays);

                    if (Days1 > total_days)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Add Date Is Expired ');", true);
                        txt_amount.Text = "0";
                        txt_amount.Focus();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
        }

    }
    protected void txt_date_Click(object sender, EventArgs e)
    {
        datevalidation();
    }

    protected bool Upload_File(int id)
    {
    
        // getexpenseid(Session["EXPENSE_ID"].ToString());
        try
        {
            if (photo_upload.HasFile)
            {

                string fileExt = System.IO.Path.GetExtension(photo_upload.FileName);
                if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png")
                {
                    if (photo_upload.FileBytes.Length < 5100000)
                    {
                        string fileName = Path.GetFileName(photo_upload.PostedFile.FileName);
                        photo_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);
                        if (id.Equals(0))
                        {
                            d.con.Open();
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO pay_add_expenses(Expenses_id,COMP_CODE,Emp_code,Date,Merchant,Amount,Description,status,NowDate,particular,type,travel_type,city_type) VALUES('" + Session["EXPENSE_ID"].ToString() + "','" + Session["COMP_CODE"].ToString() + "','" + Session["LOGIN_ID"] + "',str_to_date('" + txt_date.Text + "','%d/%m/%Y'),'" + txt_merchant.Text + "','" + txt_amount.Text + "','" + txt_description.Text + "','Draft',now(),'" + ddlparticular.SelectedValue + "','" + ddl_type.SelectedValue + "','"+ddl_travelmode.SelectedValue+"','"+ddl_city.SelectedValue+"');select max(id) from pay_add_expenses", d.con);
                            try
                            {
                                id = (int)cmd.ExecuteScalar();
                            }
                            catch
                            {
                                throw;
                            }
                            finally
                            {
                                d.con.Close();
                                cmd.Dispose();
                            }
                        }
                        File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + Session["EXPENSE_ID"].ToString() + "_" + id + fileExt, true);
                        File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                        d.operation("UPDATE pay_add_expenses SET  Expness_Image = '" + Session["EXPENSE_ID"] + "_" + id + fileExt + "' where ID = " + id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File size more than 5 MBs. Please select less size !!!')", true);
                        return false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select only JPG and PNG Images !!!')", true);
                    return false;
                }

            }
            else {
                d.operation("INSERT INTO pay_add_expenses(Expenses_id,COMP_CODE,Emp_code,Date,Merchant,Amount,Description,status,NowDate,particular,type,travel_type,city_type) VALUES('" + Session["EXPENSE_ID"].ToString() + "','" + Session["COMP_CODE"].ToString() + "','" + Session["LOGIN_ID"] + "',str_to_date('" + txt_date.Text + "','%d/%m/%Y'),'" + txt_merchant.Text + "','" + txt_amount.Text + "','" + txt_description.Text + "','Draft',now(),'" + ddlparticular.SelectedValue + "','" + ddl_type.SelectedValue + "','"+ddl_travelmode.SelectedValue+"','"+ddl_city.SelectedValue+"')");
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            d.con.Close();
            load_gridview(Session["EXPENSE_ID"].ToString());
        }


        //GridViewRow grdrow = (GridViewRow)((FileUpload)sender).NamingContainer;
        //FileUpload lbl_particular = (FileUpload)grdrow.Cells[9].FindControl("file_upload1");
        //if (lbl_particular.HasFile)
        //{
        //    string fileExt1 = System.IO.Path.GetExtension(lbl_particular.FileName);
        //    if (fileExt1 == ".jpg" || fileExt1 == ".png")
        //    {
        //        string fileName = Path.GetFileName(photo_upload.PostedFile.FileName);
        //        photo_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

        //        File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + Session["EXPENSE_ID"] + "_1" + fileExt1, true);
        //        File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

        //        //int length = photo_upload.PostedFile.ContentLength;
        //        //byte[] pic = new byte[length];
        //        //photo_upload.PostedFile.InputStream.Read(pic, 0, length);

        //        d.con1.Open();
        //        //  MySqlCommand cmd1 = new MySqlCommand("UPDATE pay_add_expenses  Expness_Image = '" + Session["EXPENSE_ID"] + "_3" + fileExt + "' where Expenses_id = '" + Session["EXPENSE_ID"] + "'", d.con1);

        //        MySqlCommand cmd1 = new MySqlCommand("UPDATE pay_add_expenses SET  Expness_Image = '" + Session["EXPENSE_ID"] + "_1" + fileExt1 + "' where Expenses_id = '" + Session["EXPENSE_ID"] + "' ", d.con1);

        //        int count = cmd1.ExecuteNonQuery();
        //        d.con1.Close();
        //    }


        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
        //    }


        //}
    }
    protected void image_click(object sender, ImageClickEventArgs e)
    {
        // Session["FROM_INVOICE"] = txt_eecode.Text;
        Response.Redirect("Employee_Images.aspx");

    }
    public void text_clear()
    {
        txt_date.Text = "";
        txt_merchant.Text = "";
        txt_rate.Text = "";
        //ddl_category.SelectedValue = "0";
        txt_amount.Text = "0";
        txt_description.Text = "";
        ddlparticular.SelectedValue = "Select";
        ddl_type.SelectedValue = "Select";
        ddl_city.SelectedValue = "Select";
        ddl_travelmode.SelectedValue = "Select";


    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
       
        try
        {
            load_gridview(Session["EXPENSE_ID"].ToString());
            btn_add.Visible = false;
            btn_submit.Visible = false;
           update_notification();
            string i = Session["iddata"].ToString();
            if (i == "1")
            {
                d.SendHtmlexpenseEmail1(Server.MapPath("~/travelexpense.htm"), Session["EXPENSE_ID"].ToString(), 0, "", Session["USERNAME"].ToString());
               // Session["iddata"] = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Send successfully');", true);
            }
            else
            {
                d.SendHtmlexpenseEmail(Server.MapPath("~/travelexpense.htm"), Session["EXPENSE_ID"].ToString(), 0, "", Session["USERNAME"].ToString());
            }
            d.operation("UPDATE pay_add_expenses set status = 'Submitted' where expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
        }
        catch (Exception ex)
        { }//please dont add exception
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Claim Submitted Successfully');", true);

    }
    private void get_travel_plan()
    {
        Int64 str_id = 0;
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select count(1) from pay_add_expenses where Expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'", d.con);
        try
        {
            str_id = (Int64)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            cmd.Dispose();
        }

        if (str_id.Equals(0))
        {
           // also insert of new travel plan in pay_add_expenses table
            d.operation("insert into pay_add_expenses (Expenses_id, COMP_CODE, Emp_code, Date, travel_type, Amount, description, status, NowDate, travel_amount,type,city_type) select expenses_id, comp_code, emp_code, now(), travel_mode, adv_amount, concat('Travel from ',from_designation,' to ', to_designation), 'Draft', now(), adv_amount,type,city_type from apply_travel_plan where Expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
           // d.operation("insert into pay_add_expenses (Expenses_id, COMP_CODE, Emp_code, Date, Merchant, Amount, description, status, NowDate, travel_amount,particular,city_type) select expenses_id, comp_code, emp_code, now(), travel_mode, adv_amount, concat('Travel from ',from_designation,' to ', to_designation), 'Draft', now(), adv_amount,'"+ddlparticular.SelectedValue+"','"+ddl_city.SelectedValue+"' from apply_travel_plan where Expenses_id = '" + Session["EXPENSE_ID"].ToString() + "'");
        }
    }

    protected void txt_comment_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((Control)sender).NamingContainer as GridViewRow;
        if (row != null)
        {
            TextBox txt_comment = (TextBox)row.FindControl("txt_comment");
            if (string.IsNullOrEmpty(txt_comment.Text))
            {
                txt_comment.Text = "0";
            }
            Label lbl_amt = (Label)row.FindControl("lbl_amount");

            if (lbl_amt.Text.Equals("0"))
            {
                try
                {
                    double total_expenses_amt = 0;
                    double per_day_amt = 0;
                    double final = 0;
                    MySqlCommand cmd1 = new MySqlCommand("select case when  IsNull(sum(Amount)) then 0 else sum(Amount) END from pay_add_expenses where  Date = STR_TO_DATE('" + txt_date.Text + "','%d/%m/%Y') and emp_code='" + Session["LOGIN_ID"] + "' and travel_amount=0 and id not in ( " + int.Parse(gvexpenessadd.DataKeys[row.RowIndex].Values[0].ToString()) + ")", d.con);

                    d.con.Open();
                    MySqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.Read())
                    {
                        string expense_am = dr1[0].ToString();
                        if (expense_am.Equals(null) || expense_am.Equals("")) { expense_am = "0"; }
                        total_expenses_amt = Convert.ToDouble(expense_am);
                        double amt = Convert.ToDouble(txt_comment.Text);
                        final = total_expenses_amt + amt;

                    }
                    dr1.Close();
                    MySqlCommand cmd2 = new MySqlCommand("select case when  IsNull(txt_per_day_limit) then 0 else  txt_per_day_limit END from  pay_travel_policy_master,pay_travel_emp_policy where pay_travel_policy_master.ID=pay_travel_emp_policy.policy_id and Emp_code = '" + Session["LOGIN_ID"] + "'", d.con);
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        per_day_amt = Convert.ToDouble(dr2[0].ToString());
                    }

                    if (final > per_day_amt)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Claim Amount Less Than Per Day Limit Amount is '+'" + per_day_amt + "'+'  ');", true);
                        txt_comment.Text = "0";
                        txt_comment.Focus();
                    }
                    else
                    {
                        d.operation("update pay_add_expenses set Amount = '" + txt_comment.Text.ToString().Trim() + "' where ID = " + int.Parse(gvexpenessadd.DataKeys[row.RowIndex].Values[0].ToString()));
                        load_gridview(Session["EXPENSE_ID"].ToString());
                    }

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
            else
            {
                d.operation("update pay_add_expenses set Amount = '" + txt_comment.Text.ToString().Trim() + "' where ID = " + int.Parse(gvexpenessadd.DataKeys[row.RowIndex].Values[0].ToString()));
                load_gridview(Session["EXPENSE_ID"].ToString());
            }
        }
    }
    protected void ddlparticular_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con1.Open();
        load_fields();
       
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            MySqlCommand cmd = null;
            
            if (ddl_type.SelectedItem.Text == "AC")
            {
                cmd = new MySqlCommand("SELECT AC FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "NonAc")
            {
                cmd = new MySqlCommand("SELECT NonAc FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "CityBus")
            {
                cmd = new MySqlCommand("SELECT CityBus FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND chkbus= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "Breakfast")
            {
                cmd = new MySqlCommand("SELECT breakfast FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND CheckBox_breakfast= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "Lunch")
            {
                cmd = new MySqlCommand("SELECT lunch FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND CheckBox_lunch= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "dinner")
            {
                cmd = new MySqlCommand("SELECT dinner FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND CheckBox_dinner= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "Inside_city")
            {
                cmd = new MySqlCommand("SELECT dinner FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND CheckBox_inside= 1", d.con);
            }
            else if (ddl_type.SelectedValue == "Outside_city")
            {
                cmd = new MySqlCommand("SELECT dinner FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "') AND CheckBox_outside= 1", d.con);
            }
            if (cmd != null)
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_amount.Text = dr.GetValue(0).ToString();
                }
                dr.Close();
                cmd.Dispose();
            }
            else
            {
                txt_amount.Text = "";
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            //txt_amount.ReadOnly = true;
            d.con.Close();
        }
    }
    protected void ddl_travelmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_type.Items.Clear();
        txt_amount.ReadOnly = false;
        txt_rate.Text = "";
        txt_amount.Text = "";
        rate.Visible = false;
        type.Visible = false;
            try
            {
                d.con.Open();
                if (ddl_travelmode.SelectedValue == "Bus")
                {
                    type.Visible = true;
                    MySqlCommand cmd_1 = new MySqlCommand("SELECT CheckBox_ac,CheckBox_nonac,CheckBox_citybus FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkbus='1'", d.con);
                    MySqlDataReader cad1 = cmd_1.ExecuteReader();
                    while (cad1.Read())
                    {
                        int i = 0;
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type.Items.Insert(i++, new ListItem("AC", "AC"));
                        }
                        if (cad1.GetValue(1).ToString().Equals("1"))
                        {
                            ddl_type.Items.Insert(i++, new ListItem("NonAc", "NonAc"));
                        }
                        if (cad1.GetValue(2).ToString().Equals("1"))
                        {
                            ddl_type.Items.Insert(i++, new ListItem("CityBus", "CityBus"));
                        }
                    }
                }
                
               
                else if (ddl_travelmode.SelectedValue == "Owned Vehicle")
                {
                    type.Visible = true;
                    rate.Visible = true;
                    MySqlCommand cmd_1 = new MySqlCommand("SELECT  CheckBox_car,CheckBox_bike FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkownedvehicle='1'", d.con);
                    MySqlDataReader cad1 = cmd_1.ExecuteReader();
                    while (cad1.Read())
                    {
                        int i = 0;
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type.Items.Insert(i++, new ListItem("Car", "Car"));
                            rate.Visible = true;
                        }
                        if (cad1.GetValue(0).ToString().Equals("1"))
                        {
                            ddl_type.Items.Insert(i++, new ListItem("Bike", "Bike"));
                            rate.Visible = true;
                        }
                       
                    }
                }
                ddl_type.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ddl_type.Items.Insert(0, new ListItem("Select"));
                d.con.Close();
            }
        
    }
    protected void load_fields()
    {
        d.con.Open();
        ddl_travelmode.Items.Clear();
        //ddl_type.Items.Clear();
      
        try
        {
          
            if (ddlparticular.SelectedValue == "Travel")
            {
                travel_mode.Visible = true;
                type.Visible = true;
                MySqlCommand cmd_1 = new MySqlCommand("select chkair,chkbus,chktraint1,chktraint2,chktraint3,chkcabtaxi,chkauto,chkownedvehicle from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con);
                MySqlDataReader cad1 = cmd_1.ExecuteReader();
                if (!cad1.HasRows)
                {
                    btn_submit.Visible = false;
                    btn_add.Visible = false;
                }
                while (cad1.Read())
                {
                    int i = 0;

                    if (cad1.GetValue(0).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Air", "Air"));
                    }
                    if (cad1.GetValue(1).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Bus", "Bus"));
                    }
                    if (cad1.GetValue(2).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Train AC Tier1", "Train AC Tier1"));
                    }
                    if (cad1.GetValue(3).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Train AC Tier2", "Train AC Tier2"));
                    }
                    if (cad1.GetValue(4).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Train AC Tier3", "Train AC Tier3"));
                    }
                    if (cad1.GetValue(5).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Cab/Taxi", "Cab/Taxi"));
                    }
                    if (cad1.GetValue(6).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Auto", "Auto"));
                    }
                    if (cad1.GetValue(7).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i, new ListItem("Owned Vehicle", "Owned Vehicle"));
                    }


                }
            }
            else if (ddlparticular.SelectedValue == "Food")
            {
                travel_mode.Visible = true;
                //type.Visible = true;
                MySqlCommand cmd_1 = new MySqlCommand("SELECT  CheckBox_breakfast,CheckBox_lunch,CheckBox_dinner FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkbus='1'", d.con);
                MySqlDataReader cad1 = cmd_1.ExecuteReader();
                while (cad1.Read())
                {
                    int i = 0;
                    if (cad1.GetValue(0).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Breakfast", "Breakfast"));
                    }
                    if (cad1.GetValue(1).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("Lunch", "Lunch"));
                    }
                    if (cad1.GetValue(2).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("dinner", "dinner"));
                    }
                }
            }
            else if (ddlparticular.SelectedValue == "Accomodation")
            {
                travel_mode.Visible = true;
                //type.Visible = true;
                MySqlCommand cmd_1 = new MySqlCommand("SELECT  hotel_budget,hotel_standard,hotel_twostar,hotel_threestar,hotel_fivestar FROM pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')  and chkbus='1'", d.con);
                MySqlDataReader cad1 = cmd_1.ExecuteReader();
                while (cad1.Read())
                {
                    int i = 0;
                    if (cad1.GetValue(0).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("hotel_budget", "hotel_budget"));
                    }
                    if (cad1.GetValue(1).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("hotel_standard", "hotel_standard"));
                    }
                    if (cad1.GetValue(2).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("hotel_twostar", "hotel_twostar"));
                    }
                    if (cad1.GetValue(3).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("hotel_threestar", "hotel_threestar"));
                    }
                    if (cad1.GetValue(4).ToString().Equals("1"))
                    {
                        ddl_travelmode.Items.Insert(i++, new ListItem("hotel_fivestar", "hotel_fivestar"));
                    }
                }
            }
            
            ddl_travelmode.DataBind();
           

        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            ddl_type.Items.Insert(0, new ListItem("Select"));
            ddl_travelmode.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    }
    protected void lnk_download_Click(object sender, EventArgs e)
    {
        //string filePath = (sender as LinkButton).CommandArgument;
        //Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        //Response.WriteFile(filePath);
        //Response.End();

      

       // string  expenses_id = "'" + Session["EXPENSE_ID"].ToString() + "'";
       // GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
       //ViewState["id"] = grdrow.Cells[3].Text;
       
       // if (expenses_id != "")
       // {
       //     string filename = d.getsinglestring("select Expness_Image from pay_add_expenses where  Expenses_id = " + expenses_id + " and id=" + ViewState["id"] + "");
       //     if (filename!="")
       //     {
       //         download_attachment(filename);
       //     }
       // }
       // else
       // {
       //     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attachment File Cannot Be Uploaded !!!')", true);
       // }

    }
    protected void download_attachment(string filename)
    {
        try
        {
             var result = filename.Substring(filename.Length - 4);
            if (result.Contains("jpeg"))
            {
                result = ".jpeg";
            }
            d.con.Open();
            string path2 = Server.MapPath("~\\EMP_Images\\" + filename);
            Response.Clear();
            Response.ContentType = "Application/pdf/jpg/jpeg/png/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile("~\\EMP_Images\\" + filename);
            Response.WriteFile(path2);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
            Response.Close();

        }
        catch (Exception ex) { }
    }

    protected void txt_rate_TextChanged(object sender, EventArgs e)
    {
         try
        {
            d.con1.Open();
            MySqlCommand cmd = null;
            string rate = "" + txt_rate.Text + "";
            if (ddl_type.SelectedItem.Text == "Car")
            {
                cmd = new MySqlCommand("SELECT ((Car)*("+rate+")) FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con1);
            }
            else if (ddl_type.SelectedItem.Text == "Bike")
            {
                cmd = new MySqlCommand("SELECT ((Bike)*(" + rate + ")) FROM pay_travel_policy_master WHERE id = (SELECT policy_id FROM pay_travel_emp_policy WHERE emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con1);
            }
            if (cmd != null)
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_amount.Text = dr.GetValue(0).ToString();
                }
                dr.Close();
                cmd.Dispose();
                txt_amount.ReadOnly = true;
            }
            else
            {
                txt_amount.Text = "";
            }
        }
         catch (Exception ex) { throw ex; }
         finally
         {
            
             d.con1.Close();
         }
    }
    protected void load_city()
    {
        d.con.Open();
        ddl_city.Items.Clear();
        // emp_category.Items.Insert(0, new ListItem("Select"));
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("select  CheckBox_inside,CheckBox_outside from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + Session["LOGIN_ID"].ToString() + "')", d.con);
            MySqlDataReader cad1 = cmd_1.ExecuteReader();
            while (cad1.Read())
            {
                int i = 0;
                if (cad1.GetValue(0).ToString().Equals("1"))
                {
                    ddl_city.Items.Insert(i++, new ListItem("Inside City", "1"));
                }
                if (cad1.GetValue(1).ToString().Equals("1"))
                {
                    ddl_city.Items.Insert(i++, new ListItem("Outside City", "2"));
                }
            }
            ddl_city.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            ddl_city.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
    }
    protected void gvexpenessadd_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (dr["Value"].ToString() == "")
            {
                //LinkButton lb1 = e.Row.FindControl("unit_name") as LinkButton;
                //lb1.Visible = false;
                e.Row.Cells[16].Text = ""; 

            }
        }
        try
        {
            if (counter.Equals(1))
            {
                if (!e.Row.Cells[8].Text.Equals("Draft"))
                {
                    ((TextBox)e.Row.Cells[7].FindControl("txt_comment")).Enabled = false;
                }
            }
        }
        catch { } //dont add exception handling


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvexpenessadd, "Select$" + e.Row.RowIndex);
        //}
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
    }
    protected void lnk_download_Command(object sender, CommandEventArgs e)
    {

        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
        
    }
}