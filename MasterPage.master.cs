using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MasterPage : System.Web.UI.MasterPage
{
    string todaydate = DateTime.Now.ToString("dd/MM/yyyy");
    protected string notification_count = "";
    protected string emp_image = "";
    DAL d1 = new DAL();
    DAL d2 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        { }
        lblDate.Text = todaydate.ToString();
        lblUsername.Text = Session["USERNAME"].ToString();
        // lblcompanyname.Text = Session["COMP_NAME"].ToString();

        d1.con1.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Date_format(now(),'%b'),year(now())", d1.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Session["CURRENT_MONTH"] = dr.GetValue(0).ToString().Trim();
                Session["CURRENT_YEAR"] = dr.GetValue(1).ToString().Trim();
                //lblcurrentperiod.Text = "Current Payroll Period: " + dr.GetValue(0).ToString() + "," + dr.GetValue(1).ToString();
                //Session["CURRENT_PERIOD"] = lblcurrentperiod.Text;
            }
            dr.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }
        if ((Session["CHANGE_PASS"].ToString()) != "0")
        {

            MySqlCommand cmd = new MySqlCommand("select LOGIN_ID,USER_PASSWORD,flag,admin_login from pay_user_master where LOGIN_ID = '" + Session["LOGIN_ID"] + "' ", d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["admin_login"].ToString() == "1")
                {
                    DAL d = new DAL();
                    MySqlCommand menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus inner join pay_role_master on menus.Id = pay_role_master.menu_id  where menus.Id IN (2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,18,19,140,145,26,28) group by menus.id  ", d.con1);
                 //   MySqlCommand menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus inner join pay_role_master on menus.Id = pay_role_master.menu_id where pay_role_master.role_name = '" + Session["ROLE"].ToString() + "' and pay_role_master.permissions != 'I' and menus.Id  in (2,3,4,5,6,7,8,9,10,11,12)  AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
                    d.con1.Open();
                    try
                    {
                        MySqlDataAdapter mda = new MySqlDataAdapter(menu);
                        DataSet ds = new DataSet();
                        mda.Fill(ds);
                        mda.Dispose();

                        ds.DataSetName = "menus";
                        ds.Tables[0].TableName = "Menu";
                        DataRelation relation = new DataRelation("ParentChild",
                                ds.Tables["Menu"].Columns["Id"],
                                ds.Tables["Menu"].Columns["parent"],
                                true);

                        relation.Nested = true;
                        ds.Relations.Add(relation);
                        xmlDataSource.EnableCaching = false;
                        xmlDataSource.Data = ds.GetXml();
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        d.con1.Close();
                    }


                }

                else
                {

                    DAL d = new DAL();
                    //MySqlCommand menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus inner join pay_role_master on menus.Id = pay_role_master.menu_id where  ( menus.Id != '0')group by menus.id ", d.con1);
                   MySqlCommand menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus inner join pay_role_master on menus.Id = pay_role_master.menu_id where pay_role_master.role_name = '" + Session["ROLE"].ToString() + "' and pay_role_master.permissions != 'I' and menus.Id!='0' AND comp_code='" + Session["comp_code"].ToString() + "' and (click_evt is not null || parent is null || description is null)", d.con1);
                    d1.con1.Open();
                    try
                    {
                        MySqlDataAdapter mda = new MySqlDataAdapter(menu);
                        DataSet ds = new DataSet();
                        mda.Fill(ds);
                        mda.Dispose();

                        ds.DataSetName = "menus";
                        ds.Tables[0].TableName = "Menu";
                        DataRelation relation = new DataRelation("ParentChild",
                                ds.Tables["Menu"].Columns["Id"],
                                ds.Tables["Menu"].Columns["parent"],
                                true);

                        relation.Nested = true;
                        ds.Relations.Add(relation);
                        xmlDataSource.EnableCaching = false;
                        xmlDataSource.Data = ds.GetXml();
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        d1.con1.Close();
                    }

                }
            }

            d1.con1.Close();


            //notifications
            d1.con1.Open();
            try
            {
                if (Session["UNIT_CODE"] is string) { Session["UNIT_CODE"] = ""; }
                DataSet ds1 = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter("select notification, page_name from pay_notification_master where emp_code = '" + Session["LOGIN_ID"].ToString() + "' order by id desc", d1.con1);
                adp.Fill(ds1);
                grd_notification.DataSource = ds1.Tables[0];
                grd_notification.DataBind();
            }
            catch
            {
                throw;
            }
            finally
            {
                d1.con1.Close();
            }

            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("select count(1) from pay_notification_master where not_read = '0' and emp_code = '" + Session["LOGIN_ID"].ToString() + "'", d1.con1);
            try
            {
                this.notification_count = (string)cmd1.ExecuteScalar().ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                d1.con1.Close();
                cmd1.Dispose();
            }
            try
            {
                MySqlCommand cmd2 = new MySqlCommand("select EMP_PHOTO,comp_logo from pay_images_master inner join pay_company_master on pay_images_master.comp_code=pay_company_master.comp_code where emp_code = '" + Session["LOGIN_ID"].ToString() + "'", d2.con1);
                d2.con1.Open();
                MySqlDataReader dr_photo = cmd2.ExecuteReader();
                if (dr_photo.HasRows)
                {
                    dr_photo.Read();
                    if (dr_photo.GetValue(0).ToString() != null && dr_photo.GetValue(0).ToString() != "")
                    {
                        this.emp_image = (string)dr_photo.GetValue(0).ToString();
                        // emp_photo.Src = "~/EMP_IMAGES/" + emp_image;
                       Img1.Src = "~/EMP_Images/" + emp_image;
                        Img2.Src = "~/EMP_Images/" + emp_image;
                        img_home.ImageUrl = "~/Images/" + dr_photo.GetValue(1).ToString();
                        //Img1.ResolveUrl("~/EMP_Images/" + emp_image);
                    }

                    cmd2.Dispose();
                }
                dr_photo.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                d2.con1.Close();

            }
        }
        else
        {
            DAL d = new DAL();
            MySqlCommand menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus Where Id='0'", d.con1);
            MySqlCommand cmd = new MySqlCommand("select LOGIN_ID,USER_PASSWORD,flag,admin_login from pay_user_master where LOGIN_ID = '" + Session["LOGIN_ID"] + "' ", d1.con1);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["admin_login"].ToString() == "1")
                {
                    menu = new MySqlCommand("Select menus.Id,Menu_name,click_evt,onclick_Button,parent from menus inner join pay_role_master on menus.Id = pay_role_master.menu_id  where menus.Id IN (2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,18,19,140,145,26,28) group by menus.id  ", d.con1);
                }
            }
            d1.con1.Open();
            try
            {
                MySqlDataAdapter mda = new MySqlDataAdapter(menu);
                DataSet ds = new DataSet();
                mda.Fill(ds);
                mda.Dispose();

                ds.DataSetName = "menus";
                ds.Tables[0].TableName = "Menu";
                DataRelation relation = new DataRelation("ParentChild",
                        ds.Tables["Menu"].Columns["Id"],
                        ds.Tables["Menu"].Columns["parent"],
                        true);

                relation.Nested = true;
                ds.Relations.Add(relation);
                xmlDataSource.EnableCaching = false;
                xmlDataSource.Data = ds.GetXml();
                notification_li.Visible = false;
                notification_li2.Visible = false;
            }
            catch
            {
                throw;
            }
            finally
            {
                d1.con1.Close();
            }
            //xmlDataSource.Data = null;
        }
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session["comp_code"] = null;
        Session["COMP_NAME"] = null;
        Session["UNIT_CODE"] = null;
        Session["UNIT_NAME"] = null;
        Session["USERNAME"] = null;
        Response.Redirect("Login_Page.aspx");
    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_pfstatement.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton2_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Esicstatement.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton3_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Pfchallan.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton4_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Esicchallan.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton5_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Salarystatement.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton6_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Payslip.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton7_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Unitmaster.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton8_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_EmployeeInfo.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton9_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_EmployeeInfopfesic.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton10_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Employeerate.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton11_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Salesregister.rpt";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void LinkButton12_Click(object sender, EventArgs e)
    //{
    //    Session["REPORT_TYPE"] = "Monthly";
    //    Response.Redirect("Reports.aspx");
    //}

    //protected void LinkButton13_Click(object sender, EventArgs e)
    //{
    //    Session["REPORT_TYPE"] = "Yearly";
    //    Response.Redirect("Reports.aspx");
    //}
    //protected void lbtcustomerledger_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Payslip.rpt";//customer ledger
    //    Response.Redirect("CustomerReport.aspx");
    //}
    //protected void lbtvendorledger_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Payslip.rpt";//vendor ledger
    //    Response.Redirect("VendorReport.aspx");
    //}
    //protected void lbtbankbook_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Payslip.rpt";//bank book
    //    Response.Redirect("BankBookReport.aspx");
    //}
    //protected void lbtinvoice_Click(object sender, EventArgs e)
    //{
    //    Session["ReportPath"] = "Rpt_Mon_Payslip.rpt";//invoice
    //    Response.Redirect("InvoiceReport.aspx");
    //}
    //protected void LinkButton3_Click1(object sender, EventArgs e)
    //{
    //    Response.Redirect("ExcelUnitwiseSalary.aspx");
    //}
    //protected void lbtexcelunitwisesalary_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel Output of Unit wise Salary";
    //    Response.Redirect("AllExcelOut.aspx");
    //}

    //protected void lbltunitwisededuction_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unit wise Deduction";
    //    Response.Redirect("AllExcelOut.aspx");
    //}
    //protected void lbltunitwisebonus_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unitwise Bonus";
    //    Response.Redirect("AllExcelOut.aspx");
    //}
    //protected void lbltunitwiselww_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unitwise LWW";
    //    Response.Redirect("AllExcelOut.aspx");
    //}

    //protected void lbtunitwisenetsalary_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unit wise net Salary";
    //    Response.Redirect("AllExcelOut.aspx");
    //}
    //protected void lbltexcelunitwiseOT_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unit wise OT";
    //    Response.Redirect("AllExcelOut.aspx");
    //}
    //protected void lbtnnewjoin_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "New Joining Employee's List";
    //    Response.Redirect("AllExcelOut.aspx");
    //}



    //protected void lbtexcelunitwiseOTESIC_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unit wise OT and ESIC";
    //    Response.Redirect("AllExcelOut.aspx");
    //}


    //protected void lbtexcelunitwiseOTEXTRA_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Unit wise Extra Days";
    //    Response.Redirect("AllExcelOut.aspx");
    //}


    //protected void lbtpfonline_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "PF ONLINE FORMAT";
    //    Response.Redirect("AllExcelOut.aspx");
    //}


    //protected void lbtempinfo_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Employee Basic Information";
    //    Response.Redirect("AllExcelOut.aspx");
    //}


    //protected void lbtunitwisenetsalarysumm_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Unit wise Summary";
    //    Response.Redirect("AllExcelOut.aspx");
    //}

    //protected void lbtunitwiseempnegative_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Excel output of Employee Negative Salary Unit wise";
    //    Response.Redirect("AllExcelOut.aspx");
    //}

    //protected void lnkbtn_calc_Click(object sender, EventArgs e)
    //{
    //    Process.Start("calc");
    //}
    //protected void lbtsalesregister_Click(object sender, EventArgs e)
    //{
    //    Session["AC_REPORT_NO"] = "5";
    //    Response.Redirect("fromTodatereport.aspx");
    //}
    //protected void lbtsalessummary_Click(object sender, EventArgs e)
    //{
    //    Session["AC_REPORT_NO"] = "6";
    //    Response.Redirect("fromTodatereport.aspx");
    //}

    //protected void lbtsalesoutstanding_Click(object sender, EventArgs e)
    //{
    //    Session["AC_REPORT_NO"] = "7";
    //    Response.Redirect("fromTodatereport.aspx");
    //}

    ////----------------
    //protected void lbtnformA_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "FORM A";
    //    Response.Redirect("Exceloutput2.aspx");
    //}

    //protected void lbtpfesicdata_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Form 6(32) & EPF Declaration";
    //    Response.Redirect("Exceloutput2.aspx");
    //}

    //protected void lbtprofitloss_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Unit Profit & Loss";
    //    Response.Redirect("Exceloutput2.aspx");
    //}

    //protected void lbtnformB_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "FORM B";
    //    Response.Redirect("Exceloutput2.aspx");
    //}

    //protected void lbtnform1_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "FORM I";
    //    Response.Redirect("Exceloutput2.aspx");

    //}

    //protected void lbtnattreg_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Muster Roll cum Wages Register";
    //    Response.Redirect("Exceloutput2.aspx");

    //}


    //protected void lbtnzerosal_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Employee Salary without rating";
    //    Response.Redirect("Exceloutput2.aspx");

    //}


    //protected void lbtitemledger_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Item Ledger";
    //    Response.Redirect("Excelout3.aspx");
    //}

    //protected void lbtitemtransaction_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Inventory transaction";
    //    Response.Redirect("Excelout3.aspx");
    //}

    //protected void lbtitemsummary_Click(object sender, EventArgs e)
    //{
    //    Session["AllExcelOut"] = "Inventory Summary";
    //    Response.Redirect("Excelout3.aspx");
    //}
    //protected void lbtbackupbtn_Click(object sender, EventArgs e)
    //{
    //    MySqlConnection sqlcon = new MySqlConnection();
    //    //Mentioned Connection string make sure that user id and password sufficient previlages
    //    sqlcon.ConnectionString = @"Data Source=ADMIN;Initial Catalog=SANPAY;Integrated Security=True";

    //    //Enter destination directory where backup file stored
    //    string destdir = "D:\\SANPAY_BACKUP";

    //    //Check that directory already there otherwise create
    //    if (!System.IO.Directory.Exists(destdir))
    //    {
    //        System.IO.Directory.CreateDirectory("D:\\SANPAY_BACKUP");
    //    }
    //    try
    //    {
    //        //Open connection
    //        sqlcon.Open();
    //        //query to take backup database
    //        MySqlCommand cmd = new MySqlCommand();
    //        cmd = new MySqlCommand("backup database SANPAY to disk='" + destdir + "\\backup" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", sqlcon);
    //        cmd.ExecuteNonQuery();
    //        //Close connection
    //        sqlcon.Close();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sanpay backup created successfully at D:\\Sanpay_Backup...');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error During backup database...');" + ex.ToString(), true);

    //    }
    //}

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem selectedItem = Menu1.SelectedItem;
        string current_menu = Menu1.SelectedItem.Text;

    }


    protected void lbtassignsupervisor_Click(object sender, EventArgs e)
    {

    }
    protected void grd_notification_SelectedIndexChanged(object sender, EventArgs e)
    {
        d1.operation("update pay_notification_master set not_read = '1' where emp_code = '" + Session["LOGIN_ID"].ToString() + "'");
        Response.Redirect(grd_notification.SelectedRow.Cells[1].Text);
    }
    protected void grd_notification_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(this.grd_notification, "Select$" + e.Row.RowIndex);
            e.Row.Cells[1].Visible = false;
        }

    }
    protected void img_home_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}
