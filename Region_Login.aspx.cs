 using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Linq;


public partial class Region_Login : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    public string file_id="";
    protected string client_name = "";
    protected string emp_image = "";
    string newunit = "";
    DAL d2 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "" || Session["LOGIN_ID"] == null || Session["LOGIN_ID"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString())== "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString())  == "R")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Unit Master", Session["COMP_CODE"].ToString()) == "C")
        {

        }
        //  txt_unitcode.ReadOnly = true;

        if (!IsPostBack)
        {
           // txt_usernam1.Text = Session["LOGIN_ID"].ToString();
            txt_usernam1.Text = Session["USERNAME"].ToString();
            //txt_usernam1.Text = Session["USERNAME"].ToString().Substring(0, Session["USERNAME"].ToString().IndexOf(" ")).ToUpper();
            txt_state.ReadOnly = true;
            txtunitcity.ReadOnly = true;
            txtunitaddress1.ReadOnly = true;
            txtunitaddress2.ReadOnly = true;
            //  txt_gst_no.ReadOnly = true;
            txt_zone1.ReadOnly = true;
            txt_zone.ReadOnly = true;

            txt_operationname.ReadOnly = true;
            txt_financename.ReadOnly = true;
            txt_locationname.ReadOnly = true;
            txt_othername.ReadOnly = true;
            txt_omobileno.ReadOnly = true;
            txt_fmobileno.ReadOnly = true;
            txt_lmobileno.ReadOnly = true;
            txt_othermobno.ReadOnly = true;
            txt_oemailid.ReadOnly = true;
            txt_femailid.ReadOnly = true;
            txt_lemailid.ReadOnly = true;
            txt_otheremailid.ReadOnly = true;
            txt_itemhead.ReadOnly = true;
            txt_itmmbl.ReadOnly = true;
            txt_itmemail.ReadOnly = true;

            feedback_notification();

            //client_name = d.getsinglestring("select client_name from pay_client_master inner join pay_unit_master on pay_unit_master.client_code = pay_client_master.client_code where pay_unit_master.unit_code = '" + Session["UNIT_CODE"].ToString() + "' and pay_client_master.comp_code='" + Session["COMP_CODE"].ToString() + "'");


            ////  feedback_notification();
            ddl_branch();
        }
    }
    //MD change
    protected void ddl_branch()
    { 
          ddlunitselect.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            string zone = Session["ZONE_NAME"].ToString();
            string  regin=Session["REGION_NAME"].ToString();
        //deleting 45 days old records
            d.operation("delete from pay_geolocation_address where cur_date < now() - interval 45 day");

            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND ZONE='" + Session["REGION_NAME"].ToString() + "' AND branch_status = 0 ", d1.con);
            d1.con.Open();
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
                d1.con.Close();
              //  ddlunitselect.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(0, "Select");
                ddlunitselect.Items.Insert(1, "ALL");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }

        }
    
    //State List
    //protected void ddlunitselect_OnSelectedIndexChanged(object sender, EventArgs e) {

    //    ddl_state.Items.Clear();
    //    System.Data.DataTable dt_item = new System.Data.DataTable();
    //    MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("SELECT distinct STATE_NAME FROM pay_state_master Where STATE_NAME in(select STATE_NAME from pay_unit_master where  COMP_CODE='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND UNIT_CODE='" + ddlunitselect.SelectedValue + "') ORDER BY STATE_NAME", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataAdapter.Fill(dt_item);
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ddl_state.DataSource = dt_item;
    //            ddl_state.DataTextField = dt_item.Columns[0].ToString();
    //            ddl_state.DataValueField = dt_item.Columns[0].ToString();
    //            ddl_state.DataBind();
    //        }
    //        dt_item.Dispose();
    //        d.con.Close();
    //        //ddl_branch_increment.Items.Insert(0, "Select");
    //        //ddlunitselect.Items.Insert(0, "ALL");
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //}
    protected void feedback_notification()
    {
        //MySqlCommand cmd = new MySqlCommand("select Feed_Date,unit_code from pay_unit_feedback where comp_code='" + Session["COMP_CODE"].ToString() + "' ", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();
        //if (dr.Read())
        //{
        //    string feedbackdate = dr.GetValue(0).ToString();
        //    //  DateTime.ParseExact(feedbackdate, "dd/MM/yyyy", null);
        //    string unitcode = dr.GetValue(1).ToString();
        //    string date = feedbackdate.Substring(3, 2);
        //    DateTime localDate = DateTime.Now;
        //    string localDate1 = Convert.ToString(localDate);
        //    // DateTime.ParseExact(localDate1, "dd/MM/yyyy", null);
        //    newunit = unitcode;
        //    string currentdate = Convert.ToString(localDate1);
        //    string currentdate1 = currentdate.Substring(3, 2);
        //    int date1 = Convert.ToInt32(currentdate.Substring(0, 2));
        //    if (date != currentdate1)
        //    {
        //        if (date1 > 27)
        //        {

        //            d1.operation("Insert into pay_notification_master (not_read,emp_code,notification,page_name) values ('0','" + unitcode + "','Please Submit Your Feedback ' ,'Unit_Login.aspx')");
        //        }
        //    }



        //}

        //dr.Close();
        //d.con.Close();
    }
    int CountDay(int month, int year, int counter)
    {
        int NoOfSunday = 0;
        var firstDay = new DateTime(year, month, 1);

        var day29 = firstDay.AddDays(28);
        var day30 = firstDay.AddDays(29);
        var day31 = firstDay.AddDays(30);

        if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
        || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
        || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
        {
            NoOfSunday = 5;
        }
        else
        {
            NoOfSunday = 4;
        }

        int NumOfDay = DateTime.DaysInMonth(year, month);

        if (counter == 1)
        {//calendar days
            return NumOfDay;
        }
        else
        { //working days
            return NumOfDay - NoOfSunday;
        }
    }
    //MD change
    public void checkrecords(string unit_code)
    {
          d.con.Open();
        try
        {
            MySqlCommand cmd1 = new MySqlCommand("Select UNIT_CODE From pay_unit_master where comp_code ='" + Session["COMP_CODE"].ToString() + "' and UNIT_CODE IN (" + unit_code + ") ", d.con);
            MySqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                //btn_add.Visible = false;
                //btn_delete.Visible = true;
                //btn_edit.Visible = true;

                MySqlCommand cmd_emp = new MySqlCommand("SELECT SR_NO, UNIT_NAME,pay_designation_count.DESIGNATION,COUNT,Hours,unit_start_date,unit_end_date from pay_designation_count INNER JOIN `pay_unit_master` ON `pay_designation_count`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_designation_count`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_designation_count.UNIT_CODE IN (" + unit_code + ") AND pay_designation_count.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY SR_NO ", d1.con);
                d1.con.Open();
                try
                {
                    MySqlDataReader dr1 = cmd_emp.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Load(dr1);
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["CurrentTable"] = dt;
                            gv_itemslist.DataSource = dt;
                            gv_itemslist.DataBind();
                            d1.con.Close();
                            dt.Dispose();
                            Panel3.Visible = true;
                            //Panel4.Visible = true;
                            gv_itemslist.Visible = true;
                        }

                    }
                    dr1.Close();
                    d1.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con.Close();
                }

                d1.con1.Open();
                try
                {
                    //  cmd2;

                    MySqlCommand cmd2 = new MySqlCommand("SELECT COMP_CODE, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_EmailId,FinanceHead_Mobileno,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,adminhead_name, adminhead_mobile, adminhead_email,Designation,txt_zone,comments FROM pay_unit_master WHERE  UNIT_CODE IN (" + unit_code + ") AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and (approval='' || approval is null)", d1.con1);
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        int split = dr2.GetValue(2).ToString().LastIndexOf("-");

                        //txt_unitcode.Text = dr2.GetValue(1).ToString();
                        //txt_unitname.Text = dr2.GetValue(2).ToString().Substring(split + 1);
                        txtunitaddress1.Text = dr2.GetValue(3).ToString();
                        txtunitaddress2.Text = dr2.GetValue(4).ToString();

                        txt_state.Text = dr2.GetValue(6).ToString();
                        // get_city_list(null, null);
                        txtunitcity.Text = dr2.GetValue(5).ToString();
                        // 741741.Text = dr2.GetValue(7).ToString();

                        //txt_lattitude.Text = dr2.GetValue(8).ToString();
                        //txt_longitude.Text = dr2.GetValue(9).ToString();
                        //txt_area.Text = dr2.GetValue(10).ToString();
                        // ddlunitclient.SelectedValue = dr2.GetValue(12).ToString();
                        // file_txt.Text = dr2.GetValue(13).ToString();

                        //if (dr2.GetValue(14).ToString() != "")
                        //{
                        //    txtemailid.Text = dr2.GetValue(14).ToString();
                        //}
                        //else { txtemailid.Text = ""; }

                        txt_operationname.Text = dr2.GetValue(15).ToString();
                        txt_omobileno.Text = dr2.GetValue(16).ToString();
                        txt_oemailid.Text = dr2.GetValue(17).ToString();

                        txt_financename.Text = dr2.GetValue(18).ToString();
                        txt_fmobileno.Text = dr2.GetValue(20).ToString();
                        txt_femailid.Text = dr2.GetValue(19).ToString();

                        txt_locationname.Text = dr2.GetValue(21).ToString();
                        txt_lmobileno.Text = dr2.GetValue(22).ToString();
                        txt_lemailid.Text = dr2.GetValue(23).ToString();

                        txt_othername.Text = dr2.GetValue(24).ToString();
                        txt_othermobno.Text = dr2.GetValue(25).ToString();
                        txt_otheremailid.Text = dr2.GetValue(26).ToString();

                        txt_itemhead.Text = dr2.GetValue(27).ToString();
                        txt_itmmbl.Text = dr2.GetValue(28).ToString();
                        txt_itmemail.Text = dr2.GetValue(29).ToString();
                        //  gesignation_fill();

                        if (!dr2.GetValue(31).ToString().Equals(""))
                        {
                            txt_zone1.Text = dr2.GetValue(31).ToString();
                            //txt_zone1_region(null, null);
                        }
                        // string tet = reason_updt(dr2.GetValue(29).ToString(), 1);
                        if (!dr2.GetValue(11).ToString().Equals(""))
                        {
                            txt_zone.Text = dr2.GetValue(11).ToString();
                        }
                        //btn_add.Visible = false;
                        // txt_unitcode.ReadOnly = true;
                    }
                    d1.con1.Close();
                    dr2.Dispose();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con1.Close();
                }
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
    }
    //protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    //{
    //    int rowID = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
    //        if (dt.Rows.Count >= 1)
    //        {
    //            if (rowID < dt.Rows.Count)
    //            {
    //                dt.Rows.Remove(dt.Rows[rowID]);
    //            }
    //        }
    //        ViewState["CurrentTable"] = dt;
    //        gv_itemslist.DataSource = dt;
    //        gv_itemslist.DataBind();
    //    }

    //}
    //protected void designstion_details1(object sender, EventArgs e)
    //{
    //    //gesignation_fill();
    //}
    //protected void get_empcount_details(object sender, EventArgs e)
    //{
    //    d.con.Open();
    //    MySqlCommand cmd = new MySqlCommand("SELECT distinct GRADE_CODE,GRADE_DESC FROM pay_grade_master where COMP_CODE ='" + Session["COMP_CODE"].ToString() + "' ", d.con);
    //    try
    //    {
    //        MySqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            string ehead1 = dr.GetValue(0).ToString();
    //        }
    //        int n = 10;
    //        TextBox[] textBoxes = new TextBox[n];
    //        Label[] labels = new Label[n];

    //        for (int i = 0; i < n; i++)
    //        {
    //            textBoxes[i] = new TextBox();
    //            // Here you can modify the value of the textbox which is at textBoxes[i]

    //            labels[i] = new Label();
    //            // Here you can modify the value of the label which is at labels[i]
    //        }

    //        // This adds the controls to the form (you will need to specify thier co-ordinates etc. first)
    //        for (int i = 0; i < n; i++)
    //        {
    //            this.Controls.Add(textBoxes[i]);
    //            this.Controls.Add(labels[i]);
    //        }

    //    }
    //    catch { }
    //    finally { }

    //}
    //protected void checklistcox_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int n = 1;
    //    TextBox[] textBoxes = new TextBox[n];
    //    // Label[] labels = new Label[n];

    //    for (int i = 0; i < n; i++)
    //    {
    //        textBoxes[i] = new TextBox();
    //    }

    //    for (int i = 0; i < n; i++)
    //    {
    //        this.Controls.Add(textBoxes[i]);
    //        //  this.Controls.Add(labels[i]);
    //    }

    //}

    //MD change
    public void employee_list(string unit_code)
    {
        try
        {
            d1.con.Open();
            ddl_employee.Items.Clear();

            MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct(EMP_CODE), EMP_NAME  FROM pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and UNIT_CODE IN ("+unit_code+") and LEFT_DATE IS NULL order by EMP_NAME", d1.con);
            //if (ddlunitselect.SelectedValue=="ALL") {
            //    MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct(EMP_CODE), EMP_NAME  FROM pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' "+ unit_code +" and LEFT_DATE IS NULL order by EMP_NAME", d1.con);
            //}
            
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddl_employee.DataSource = ds1.Tables[0];
                ddl_employee.DataValueField = "EMP_CODE";
                ddl_employee.DataTextField = "EMP_NAME";
                ddl_employee.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ddl_employee.Items.Insert(0, new ListItem("Select", "Select"));
            d1.con.Close();
        }
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void UnitGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        string unit_code = "";


        if (ddlunitselect.SelectedValue == "ALL")
        {

            unit_code = "select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND ZONE='" + Session["REGION_NAME"].ToString() + "' AND branch_status = 0  ";
        }
        else { Session["UNIT_CODE"] = ddlunitselect.SelectedValue ; }

        text_clear();
        gv_unit_attendance.DataSource = null;
        gv_unit_attendance.DataBind();
        grd_work_image.DataSource = null;
        grd_work_image.DataBind();
        if (ddlunitselect.SelectedValue == "ALL")
        {
            load_fields(unit_code, 1);


        }
        else
        {

            load_fields("'"+Session["UNIT_CODE"].ToString() +"'", 1);
        }

        companyGridView.DataSource = null;
        companyGridView.DataBind();
        employee_list1();//vikas17-12

        if (ddlunitselect.SelectedValue =="ALL") 
        {
            txt_state.Text = "";
            txtunitcity.Text = "";
            txtunitaddress1.Text = "";
            txtunitaddress2.Text = "";
            txt_zone1.Text = "";
            txt_zone.Text = "";

        
        }

    }
    private void load_fields(string unitcode, int counter)
    {



        MySqlCommand cmd1 = new MySqlCommand("SELECT SR_NO, UNIT_NAME,pay_designation_count.DESIGNATION,COUNT,Hours,unit_start_date,unit_end_date from pay_designation_count  INNER JOIN `pay_unit_master` ON `pay_designation_count`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_designation_count`.`unit_code` = `pay_unit_master`.`unit_code` WHERE pay_designation_count.UNIT_CODE IN (" + unitcode + ") AND pay_designation_count.COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ORDER BY SR_NO ", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr1);
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt;
                    gv_itemslist.DataSource = dt;
                    gv_itemslist.DataBind();
                    Panel3.Visible = true;
                    //Panel4.Visible = true;
                    gv_itemslist.Visible = true;
                }

            }
            dr1.Close();
            d1.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        if (counter == 1)
        {
            //reason_panel.Visible = true;
            //btn_add.Visible = false;
            //btn_delete.Visible = true;
            //btn_edit.Visible = true;
        }

        MySqlCommand cmd2;
        if (counter == 1)
        {
            cmd2 = new MySqlCommand("SELECT COMP_CODE, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_EmailId,FinanceHead_Mobileno,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments FROM pay_unit_master WHERE unit_CODE IN (" + unitcode + ")  AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con);
        }
        else
        {
            cmd2 = new MySqlCommand("SELECT COMP_CODE, UNIT_CODE,Concat(STATE_NAME,'-',UNIT_ADD1,'-',UNIT_NAME) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY, STATE_NAME,unit_gst_no,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_EmailId,FinanceHead_Mobileno,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,comments FROM pay_unit_master WHERE unit_CODE IN (" + unitcode + ") AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and SUBSTRING(approval,1,6) = '" + Session["LOGIN_ID"].ToString() + "'", d.con);
        }
        d.con.Open();
        try
        {
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                int split = dr2.GetValue(2).ToString().LastIndexOf("-");

                //txt_unitcode.Text = dr2.GetValue(1).ToString();
                //txt_unitname.Text = dr2.GetValue(2).ToString().Substring(split + 1);
                txtunitaddress1.Text = dr2.GetValue(3).ToString();
                txtunitaddress2.Text = dr2.GetValue(4).ToString();

                // ddl_branch();
                txt_state.Text = dr2.GetValue(6).ToString();
               // ddl_state.DataTextField = dr2.GetValue(6).ToString();
                //get_city_list(null, null);
                txtunitcity.Text = dr2.GetValue(5).ToString();
                //txt_gst_no.Text = dr2.GetValue(7).ToString();

                //txt_lattitude.Text = dr2.GetValue(8).ToString();
                //txt_longitude.Text = dr2.GetValue(9).ToString();
                //txt_area.Text = dr2.GetValue(10).ToString();
                //ddlunitclient.SelectedValue = dr2.GetValue(12).ToString();
                //file_txt.Text = dr2.GetValue(13).ToString();

                //if (dr2.GetValue(14).ToString() != "")
                //{
                //    txtemailid.Text = dr2.GetValue(14).ToString();
                //}
                //else { txtemailid.Text = ""; }

                txt_operationname.Text = dr2.GetValue(15).ToString();
                txt_omobileno.Text = dr2.GetValue(16).ToString();
                txt_oemailid.Text = dr2.GetValue(17).ToString();

                txt_financename.Text = dr2.GetValue(18).ToString();
                txt_fmobileno.Text = dr2.GetValue(19).ToString();
                txt_femailid.Text = dr2.GetValue(20).ToString();

                txt_locationname.Text = dr2.GetValue(21).ToString();
                txt_lmobileno.Text = dr2.GetValue(22).ToString();
                txt_lemailid.Text = dr2.GetValue(23).ToString();

                txt_othername.Text = dr2.GetValue(24).ToString();
                txt_othermobno.Text = dr2.GetValue(25).ToString();
                txt_otheremailid.Text = dr2.GetValue(26).ToString();

                //gesignation_fill();
                if (!dr2.GetValue(28).ToString().Equals(""))
                {
                    txt_zone1.Text = dr2.GetValue(28).ToString();
                  //  txt_zone1_region(null, null);
                }
                //string tet = reason_updt(dr2.GetValue(29).ToString(), 1);
                if (!dr2.GetValue(11).ToString().Equals(""))
                {
                    txt_zone.Text = dr2.GetValue(11).ToString();
                }
                //btn_add.Visible = false;
                // txt_unitcode.ReadOnly = true;
                //MD change

                string unit_code = "";
                if (ddlunitselect.SelectedValue == "ALL")
                {

                    unit_code = "select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND ZONE='" + Session["REGION_NAME"].ToString() + "' AND branch_status = 0  ";
                }
                else { Session["UNIT_CODE"] = ddlunitselect.SelectedValue;
                unit_code = "'" + ddlunitselect.SelectedValue + "'";
                }

                employee_list(unit_code);
               

            }
            dr2.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();

            string unit_code = "";
            if (ddlunitselect.SelectedValue == "ALL")
            {

                unit_code = "select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND ZONE='" + Session["REGION_NAME"].ToString() + "' AND branch_status = 0  ";
            }
            else
            {
                Session["UNIT_CODE"] = ddlunitselect.SelectedValue;
                unit_code = "'" + ddlunitselect.SelectedValue + "'";
            }



            checkrecords(unit_code);
        }
        //string login_user = d.getsinglestring("select Login_id from pay_user_master where UNIT_FLAG = '1' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and unit_code = '" + unitcode + "'");
        //if (login_user != "")
        //{
        //    txt_unit_login_id.Text = login_user;
        //}
    }
    //public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}
    public void text_clear()
    {
        //txt_unitname.Text = "";
        txtunitaddress1.Text = "";
        txtunitaddress2.Text = "";
        txtunitcity.Text = "";
        //txtemailid.Text = "";
        txt_zone1.Text = "";
        txt_zone.Text = "";
        //file_txt.Text = "";
        txt_operationname.Text = "";
        txt_othermobno.Text = "";
        txt_otheremailid.Text = "";
        txt_financename.Text = "";
        txt_fmobileno.Text = "";
        txt_femailid.Text = "";
        txt_locationname.Text = "";
        txt_lmobileno.Text = "";
        txt_lemailid.Text = "";
        txt_othername.Text = "";
        txt_othermobno.Text = "";
        txt_otheremailid.Text = "";

        //txt_reason_updation.Text = "";
        gv_itemslist.Visible = false;
        // ddlunitclient.SelectedIndex = 0;
        //txt_unitcode.Text = "";
        txt_state.Text = "";
        //txt_lattitude.Text = "";
        //txt_longitude.Text = "";
        //txt_area.Text = "";
        //txt_gst_no.Text = "";
        txt_operationname.Text = "";
        txt_operationname.Text = "";
        txt_financename.Text = "";
        txt_locationname.Text = "";
        txt_othername.Text = "";
        txt_omobileno.Text = "";
        txt_fmobileno.Text = "";
        txt_lmobileno.Text = "";
        txt_othermobno.Text = "";
        txt_oemailid.Text = "";
        txt_femailid.Text = "";
        txt_lemailid.Text = "";
        txt_otheremailid.Text = "";
        Image4.ImageUrl  = "~/Images/placeholder.png";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
        image15.ImageUrl = "~/Images/Biodata.png";
        txt_work_img_from.Text = "";
        txt_work_img_to.Text = "";
        txt_month_year.Text = "";
        txt_itemhead.Text = "";
        txt_itmmbl.Text = "";
        txt_itmemail.Text = "";
    }
    public static string GetSha256FromString(string strData)
    {
        var message = System.Text.Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int item = (int)GridView1.DataKeys[e.RowIndex].Value;
        //d.operation("delete from pay_unit_master WHERE id=" + item);
        //loadclientgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //load_fields(GridView1.Rows[e.NewEditIndex].Cells[2].Text, 2);
        //btn_add.Visible = false; btn_edit.Visible = false; btn_delete.Visible = false; btn_approval.Visible = true;
        //load_reporting_grdv();
    }
    //protected string get_start_date()
    //{
    //    string unit_code = "";
    //    if (ddlunitselect.SelectedValue == "ALL")
    //    {

    //        unit_code = "Select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "' AND txt_zone = '" + Session["ZONE_NAME"].ToString() + "' and zone = '"+Session["REGION_NAME"].ToString()+"'  AND branch_status = 0 ";
    //    }
    //    else
    //    {
    //        unit_code = ddlunitselect.SelectedValue;
    //        unit_code = "'" + Session["unit_code"].ToString() + "'";
    //    }

    //    //comp_code='" + Session["comp_code"].ToString() + "' AND client_code='" + Session["CLIENT_CODE"].ToString() + "'
    //    return d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "' AND pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' AND unit_code IN ("+unit_code+") AND month = '" + txt_month_year.Text.Substring(0, 2) + "' AND year ='" + txt_month_year.Text.Substring(3) + "' limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  =  '" + Session["CLIENT_CODE"].ToString() + "' AND  pay_unit_master.comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  unit_code IN("+unit_code+") limit 1))");
    //}
    protected string get_start_date()
    {
        return d1.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_billing_master_history.billing_client_code = '" + Session["client_code"].ToString() + "' AND month = '" + txt_month_year.Text.Substring(0, 2) + "' and year = '" + txt_month_year.Text.Substring(3) + "' and  pay_billing_master_history.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1),(SELECT start_date_common FROM pay_billing_master INNER JOIN pay_unit_master ON pay_billing_master.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code = '" + Session["client_code"].ToString()+ "' AND pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1))");
    }
   


    protected void btn_show_Click(object sender, EventArgs e)
    {
        string start_date_common = get_start_date();
        hidtab.Value = "6";
        d1.con1.Open();
        try
        {
            string where = " and pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "'";
            if (!ddlunitselect.SelectedValue.Equals("ALL"))
            {
                where = " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'";
            }

            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("select pay_unit_master.state_name as 'STATE',pay_unit_master.unit_name, pay_unit_master.unit_city as 'CITY',pay_unit_master.client_code as 'CLIENT CODE', pay_employee_master.emp_name AS 'EMPLOYEE NAME', pay_grade_master.grade_desc AS 'DESIGNATION',  " + d.get_calendar_days(int.Parse(start_date_common), txt_month_year.Text, 5, 1) + "  pay_attendance_muster.`tot_days_present` AS 'PRESENT DAYS',`pay_attendance_muster`.`tot_days_absent` AS 'ABSENT',pay_attendance_muster.`tot_working_days` AS 'TOTAL DAYS'from pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE where pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "' " + where + " and txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND ZONE = '" + Session["REGION_NAME"].ToString() + "' and pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0", d1.con1);
            //MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("select pay_unit_master.state_name as 'STATE', pay_unit_master.unit_city as 'CITY', pay_unit_master.client_branch_code AS 'CLIENT BRANCH CODE',pay_unit_master.client_code as 'CLIENT CODE', pay_employee_master.emp_name AS 'EMPLOYEE NAME', pay_grade_master.grade_desc AS 'DESIGNATION', case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster.tot_days_present AS 'PRESENT DAYS',pay_attendance_muster.tot_days_absent AS 'ABSENT',pay_attendance_muster.tot_working_days AS 'TOTAL DAYS'from pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE where pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "' and pay_attendance_muster.unit_code = '" + Session["UNIT_CODE"].ToString() + "' and pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0,2) + "' and pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0", d1.con1);
            //MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter("select pay_unit_master.state_name as 'STATE', pay_unit_master.unit_city as 'CITY', pay_unit_master.client_branch_code AS 'CLIENT BRANCH CODE',pay_unit_master.client_code as 'CLIENT CODE', pay_employee_master.emp_name AS 'EMPLOYEE NAME', pay_grade_master.grade_desc AS 'DESIGNATION', case when DAY01 = '0' then 'A' else DAY01 end as DAY01, case when DAY02 = '0' then 'A' else DAY02 end as DAY02, case when DAY03 = '0' then 'A' else DAY03 end as DAY03, case when DAY04 = '0' then 'A' else DAY04 end as DAY04, case when DAY05 = '0' then 'A' else DAY05 end as DAY05, case when DAY06 = '0' then 'A' else DAY06 end as DAY06, case when DAY07 = '0' then 'A' else DAY07 end as DAY07, case when DAY08 = '0' then 'A' else DAY08 end as DAY08, case when DAY09 = '0' then 'A' else DAY09 end as DAY09, case when DAY10 = '0' then 'A' else DAY10 end as DAY10, case when DAY11 = '0' then 'A' else DAY11 end as DAY11, case when DAY12 = '0' then 'A' else DAY12 end as DAY12, case when DAY13 = '0' then 'A' else DAY13 end as DAY13, case when DAY14 = '0' then 'A' else DAY14 end as DAY14, case when DAY15 = '0' then 'A' else DAY15 end as DAY15, case when DAY16 = '0' then 'A' else DAY16 end as DAY16, case when DAY17 = '0' then 'A' else DAY17 end as DAY17, case when DAY18 = '0' then 'A' else DAY18 end as DAY18, case when DAY19 = '0' then 'A' else DAY19 end as DAY19, case when DAY20 = '0' then 'A' else DAY20 end as DAY20, case when DAY21 = '0' then 'A' else DAY21 end as DAY21, case when DAY22 = '0' then 'A' else DAY22 end as DAY22, case when DAY23 = '0' then 'A' else DAY23 end as DAY23, case when DAY24 = '0' then 'A' else DAY24 end as DAY24, case when DAY25 = '0' then 'A' else DAY25 end as DAY25, case when DAY26 = '0' then 'A' else DAY26 end as DAY26, case when DAY27 = '0' then 'A' else DAY27 end as DAY27, case when DAY28 = '0' then 'A' else DAY28 end as DAY28, case when DAY29 = '0' then 'A' else DAY29 end as DAY29, case when DAY30 = '0' then 'A' else DAY30 end as DAY30, case when DAY31 = '0' then 'A' else DAY31 end as DAY31, pay_attendance_muster.tot_days_present AS 'PRESENT DAYS',pay_attendance_muster.tot_days_absent AS 'ABSENT',pay_attendance_muster.tot_working_days AS 'TOTAL DAYS'from pay_employee_master INNER JOIN pay_attendance_muster ON pay_attendance_muster.emp_code = pay_employee_master.emp_code and pay_attendance_muster.comp_code = pay_employee_master.comp_code INNER JOIN pay_unit_master ON pay_attendance_muster.unit_code = pay_unit_master.unit_code and pay_attendance_muster.comp_code = pay_unit_master.comp_code inner join pay_grade_master on pay_unit_master.comp_code=pay_grade_master.comp_code and pay_employee_master.GRADE_CODE = pay_grade_master.GRADE_CODE where pay_attendance_muster.comp_code = '" + Session["comp_code"].ToString() + "' " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' and pay_attendance_muster.month = '" + txt_month_year.Text.Substring(0, 2) + "' and pay_attendance_muster.Year = '" + txt_month_year.Text.Substring(3) + "' and pay_attendance_muster.tot_days_present > 0", d1.con1);
            DataSet DS = new DataSet();
            MySqlDataAdapter.Fill(DS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                gv_unit_attendance.DataSource = DS;
                gv_unit_attendance.DataBind();
                d1.con1.Close();
                DS.Dispose();
                gv_unit_attendance.Visible = true;
            }
            else
            {
               // grd_work_image.DataSource = null;
               // grd_work_image.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance Not Found For This Branch');", true);
            }


           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }
    }
    protected void btn_document_show_Click(object sender, EventArgs e)
    {
        hidtab.Value = "7";
        try
        {
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("Select original_photo,original_adhar_card,original_policy_document,original_address_proof From pay_images_master where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + ddl_employee.SelectedValue + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.GetValue(0).ToString() != "")
                {

                    Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();
                }
                else
                {
                    Image4.ImageUrl = "~/Images/placeholder.png";
                }

                if (dr.GetValue(1).ToString() != "")
                {

                    Image2.ImageUrl = "~/EMP_Images/" + dr.GetValue(1).ToString();
                }
                else
                {
                    Image2.ImageUrl = "~/Images/passbook.jpg";
                }
                if (dr.GetValue(2).ToString() != "")
                {
                    Image14.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
                }
                else
                {
                    Image14.ImageUrl = "~/Images/certificate.jpg";
                }

                if (dr.GetValue(3).ToString() != "")
                {
                    image15.ImageUrl = "~/EMP_Images/" + dr.GetValue(3).ToString();
                }
                else
                {
                    image15.ImageUrl = "~/Images/Biodata.png";
                }
                d.con.Close();
            }
            dr.Close();

        }
        catch (Exception ex) { }
        finally { d.con.Close(); }

    }
    protected void image_click(object sender, ImageClickEventArgs e)
    {
        //System.Web.UI.WebControls.Image button = sender as System.Web.UI.WebControls.Image;
        //Response.Redirect(button.ImageUrl);
    }
    protected void btn_submit(object sender, EventArgs args)
    {
        hidtab.Value = "8";
        try
        {
            d.con.Open();
            int result = 0;
            string str = "";

           // string feed_date = DateTime.Now.ToString("dd/MM/yyyy");
            if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
            {
                str = "Poor";
            }
            else if (RadioButton4.Checked == true || RadioButton5.Checked == true)
            {
                str = "Average";
            }
            else if (RadioButton6.Checked == true || RadioButton7.Checked == true)
            {
                str = "Good";
            }
            else if (RadioButton8.Checked == true || RadioButton9.Checked == true)
            {
                str = "Best";
            }
            else if (RadioButton10.Checked == true)
            {
                str = "Excellent";
            }



            result = d.operation("INSERT INTO PAY_UNIT_FEEDBACK(REGION_LOGIN,COMP_CODE,FEEDBACK,REASON,Feed_Date,client_code) VALUES ('" + Session["LOGIN_ID"].ToString() + "','" + Session["COMP_CODE"].ToString() + "','" + str + "','" + txtfeed.Text + "',now(),'"+Session["CLIENT_CODE"].ToString()+"')");

           // string file_code = d.getsinglestring("select count(id) from pay_unit_feedback where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  Region_login='" + Session["LOGIN_ID"].ToString() + "'");
            string file_code = d.getsinglestring("select MAX(id) from pay_unit_feedback where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  Region_login='" + Session["LOGIN_ID"].ToString() + "'");
            if (result > 0)
            {
                string user_id = Session["LOGIN_ID"].ToString();

                if (FileUpload1.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                    if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
                    {
                        if (user_id != "")
                        {
                            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                            File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + user_id + "_" + file_code+ fileExt, true);
                            File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                            d.operation("UPDATE PAY_UNIT_FEEDBACK SET Feed_Date = curdate(), document = '" + user_id + "_" + file_code + fileExt + "' where Region_login = '" + user_id + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "' and id='" + file_code + "'");
                        }
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
                    }

                }



                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Feedback Submitted Successfully');", true);
                txtfeed.Text = "";
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                RadioButton4.Checked = false;
                RadioButton5.Checked = false;
                RadioButton6.Checked = false;
                RadioButton7.Checked = false;
                RadioButton8.Checked = false;
                RadioButton9.Checked = false;
                RadioButton10.Checked = false;
                d.con.Close();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Feedback Not Submitted !!');", true);
                d.con.Close();

            }
        }

        catch (Exception e)
        { }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["comp_code"] = null;
        Session["UNIT_CODE"] = null;
        Session["UNIT_NAME"] = null;
        Response.Redirect("Login_Page.aspx");
    }
    protected void gv_itemslist_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemslist.UseAccessibleHeader = false;
            gv_itemslist.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void gv_unit_attendance_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_unit_attendance.UseAccessibleHeader = false;
            gv_unit_attendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void Upload_File(object sender, EventArgs e)
    { }
    protected void btn_send_request_click(object sender, EventArgs e)
    {

        int res = 0;
        res = d.operation("insert into pay_service_master(comp_code,unit_code,date,services,priority,additional_comment,status,type)VALUES('" + Session["comp_code"].ToString() + "','" + Session["unit_code"].ToString() + "',now(),'" + ddl_asset_type.SelectedValue + "','" + department_asset.SelectedValue + "','" + txt_asset_description.Text + "','New','client')");


        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Send Successfully!!!')", true);
            fill_servicesgv();
            text_clear1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Send Faill!!!')", true);
        }

    }
    //protected void fill_forwardto()
    //{

    //    try
    //    {
    //        d1.con.Open();
    //        MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct(EMP_CODE),EMP_NAME FROM pay_employee_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and unit_code='"+Session["unit_code"].ToString()+"'  order by EMP_NAME", d1.con);
    //        MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
    //        DataSet ds1 = new DataSet();
    //        cad1.Fill(ds1);
    //        if (ds1.Tables[0].Rows.Count > 0)
    //        {
    //            txt_forward_to.DataSource = ds1.Tables[0];
    //            txt_forward_to.DataValueField = "EMP_CODE";
    //            txt_forward_to.DataTextField = "EMP_NAME";
    //            txt_forward_to.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        txt_forward_to.Items.Insert(0, new ListItem("Select", "Select"));
    //        d1.con.Close();
    //    }
    //}
    protected void fill_servicesgv()
    {
        d.con.Open();
        DataSet ds = new DataSet();
        // MySqlDataAdapter dr = new MySqlDataAdapter("select Id,services,pay_service_master.location,priority,pay_service_master.status,additional_comment,EMP_NAME as forword_to,documents from  pay_service_master inner join pay_employee_master on pay_service_master.comp_code=pay_employee_master.comp_code and  pay_service_master.forword_to=pay_employee_master.emp_code   where pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' order by Id  ", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter("SELECT pay_service_master.Id, services, priority, pay_service_master.status, additional_comment, date, concat('~/Images/',file_name) as Value FROM pay_service_master INNER JOIN pay_service_documents ON pay_service_master.comp_code = pay_service_documents.comp_code AND pay_service_master.unit_code = pay_service_documents.unit_code WHERE pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' order by pay_service_master.Id desc", d.con);
        dr.Fill(ds);
        SearchGridView.DataSource = ds.Tables[0];
        SearchGridView.DataBind();
        d.con.Close();
        SearchGridView.Visible = true;
        Panel6.Visible = true;
    }
    protected void SearchGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = SearchGridView.SelectedRow.Cells[0].Text;
        d.con.Open();
        try
        {
            MySqlCommand cmd = new MySqlCommand("select Id,services,priority,additional_comment from  pay_service_master inner join pay_employee_master on pay_service_master.comp_code=pay_employee_master.comp_code and  pay_service_master.forword_to=pay_employee_master.emp_code   where pay_service_master.comp_code='" + Session["comp_code"].ToString() + "' and pay_service_master.unit_code='" + Session["unit_code"].ToString() + "' and Id='" + id + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ddl_asset_type.SelectedValue = dr.GetValue(1).ToString();
                department_asset.SelectedValue = dr.GetValue(2).ToString();
                txt_asset_description.Text = dr.GetValue(3).ToString();
            }
            dr.Close();
            d.con.Close();
            load_grdview();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            // e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.SearchGridView, "Select$" + e.Row.RowIndex);
            string item = e.Row.Cells[0].Text + "-" + e.Row.Cells[1].Text;
            foreach (Button button in e.Row.Cells[6].Controls.OfType<Button>())
            {
                if (button.CommandName == "Edit")
                {
                    button.Attributes["onclick"] = "if(!confirm('Do you want to Approved " + item + "?')){ return false; };";
                }
                if (button.CommandName == "Delete")
                {
                    button.Attributes["onclick"] = "if(!confirm('Do you want to Reject " + item + "?')){ return false; };";
                }
            }

        }
        e.Row.Cells[0].Visible = false;
    }
    protected void text_clear1()
    {
        ddl_asset_type.SelectedValue = "0";
        //   txt_location.Text = "";
        department_asset.SelectedValue = "";
        txt_asset_description.Text = "";
        //  txt_forward_to.SelectedValue = "Select";
    }
    protected void grd_company_files_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item = (int)grd_company_files.DataKeys[e.RowIndex].Value;
        string temp = d.getsinglestring("SELECT file_name FROM pay_images WHERE id=" + item);
        if (temp != "")
        {
            File.Delete(Server.MapPath("~/Images/") + temp);
        }
        d.operation("delete from pay_images WHERE id=" + item);
        // load_grdview(ViewState["compcode"].ToString());
    }
    protected void grd_company_files_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    private void load_grdview()
    {
        string t_id = SearchGridView.SelectedRow.Cells[1].Text;
        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT id,concat('~/Images/',file_name) as Value,created_by,date_format(create_date,'%d/%m/%Y') as create_date FROM pay_service_documents where unit_code = '" + Session["unit_code"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "' and Id='" + t_id + "' ", d.con1);
        DataSet DS1 = new DataSet();
        MySqlDataAdapter1.Fill(DS1);
        grd_company_files.DataSource = DS1;
        grd_company_files.DataBind();
        grd_company_files.Visible = true;

    }

    protected void GradeGridView_RowDataBound1(object sender, GridViewRowEventArgs e)
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
            string imageUrl, imageUrl2 = "";
            if (dr["Attendances_intime_images"].ToString() != "")
            {
                imageUrl = "~/attendance_images/" + dr["Attendances_intime_images"];
                (e.Row.FindControl("Camera_Image1") as Image).ImageUrl = imageUrl;

            }
            if (dr["Attendances_outtime_images"].ToString() != "")
            {

                imageUrl2 = "~/attendance_images/" + dr["Attendances_outtime_images"];
                (e.Row.FindControl("Camera_Image2") as Image).ImageUrl = imageUrl2;

            }
            if (dr["Camera_intime_images"].ToString() != "")
            {
                imageUrl = "~/attendance_images/" + dr["Camera_intime_images"];
                (e.Row.FindControl("Camera_Image1") as Image).ImageUrl = imageUrl;
            }
            if (dr["Camera_outtime_images"].ToString() != "")
            {
                imageUrl2 = "~/attendance_images/" + dr["Camera_outtime_images"];
                (e.Row.FindControl("Camera_Image2") as Image).ImageUrl = imageUrl2;
            }
        }
    }
    //protected void GradeGridView_PreRender(object sender, EventArgs e)
    //{
    ////    try
    ////    {
    ////        GradeGridView.UseAccessibleHeader = false;
    ////        GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
    ////    }
    ////    catch { }//vinod dont apply catch
    ////}
    protected void Button3_Click(object sender, EventArgs e)
    {
        hidtab.Value = "0";
        d.con.Open();
        try
        {
            string zone = Session["ZONE_NAME"].ToString();
            string regin = Session["REGION_NAME"].ToString();

            string where = " and pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "'";
            if (!ddlunitselect.SelectedValue.Equals("ALL"))
            {
                where = " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'";
            }
           // dscmd = new MySqlDataAdapter("Select comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,attendances_intime,attendances_outtime,camera_intime,camera_outtime,Camera_intime_images,Camera_outtime_images,Attendances_intime_images,Attendances_outtime_images from pay_android_attendance_logs where UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND comp_code = '" + Session["comp_code"].ToString() + "' and date(date_time) between str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + "','%d/%m/%Y') order by date_time desc limit 200", d.con);
            MySqlDataAdapter dscmd = new MySqlDataAdapter("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_NAME, EMP_CODE, UNIT_LATITUDE, UNIT_LONGTUTDE, EMP_LATITUDE, EMP_LONGTUTDE, DISTANCES, case when ADDRESS like '%IOException%' then 'NA' else ADDRESS END as ADDRESS, upper(EMP_NAME) as emp_name, Date_Time, attendances_intime, attendances_outtime, camera_intime, camera_outtime, Camera_intime_images, Camera_outtime_images, Attendances_intime_images, Attendances_outtime_images FROM pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.comp_CODE = pay_unit_master.comp_code and pay_android_attendance_logs.unit_CODE = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' AND cast(`date_time` as date) BETWEEN str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + "','%d/%m/%Y') ORDER BY date_time DESC LIMIT 200", d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GradeGridView.DataSource = ds;
                GradeGridView.DataBind();
                GradeGridView.Visible = true;
                d.con.Close();
            }
            else
            {
                GradeGridView.DataSource = null;
                GradeGridView.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found For This Branch');", true);
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_work_image_Click(object sender, EventArgs e)
    {
        d.con.Open();
        hidtab.Value = "3";
        try
        {
            grd_work_image.DataSource = null;
            grd_work_image.DataBind();

            string where = " and pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "'";
            if (!ddlunitselect.SelectedValue.Equals("ALL"))
            {
                where = " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'";
            }

            //MySqlDataAdapter dscmd = new MySqlDataAdapter("SELECT pay_android_working_image.comp_code, pay_android_working_image.UNIT_CODE, pay_android_working_image.EMP_CODE, datecurrent, image_name , pay_employee_master.EMP_NAME AS 'EMP_NAME' FROM pay_android_working_image inner join pay_employee_master on pay_android_working_image.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_android_working_image.comp_code = pay_employee_master.comp_code and pay_android_working_image.EMP_CODE = pay_employee_master.EMP_CODE WHERE pay_android_working_image.UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND pay_android_working_image.comp_code = '" + Session["comp_code"].ToString() + "' AND date(datecurrent) BETWEEN STR_TO_DATE('" + txt_work_img_from.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_work_img_to.Text + "','%d/%m/%Y') ORDER BY datecurrent DESC LIMIT 200", d.con);
            MySqlDataAdapter dscmd = new MySqlDataAdapter("SELECT pay_android_working_image.comp_code, pay_unit_master.UNIT_NAME, pay_android_working_image.EMP_CODE, datecurrent, image_name, pay_employee_master.EMP_NAME FROM pay_android_working_image INNER JOIN pay_unit_master ON pay_android_working_image.comp_code = pay_unit_master.comp_code AND pay_android_working_image.unit_code = pay_unit_master.unit_code INNER JOIN pay_employee_master ON pay_unit_master.UNIT_CODE = pay_employee_master.UNIT_CODE AND pay_unit_master.comp_code = pay_employee_master.comp_code AND pay_android_working_image.EMP_CODE = pay_employee_master.EMP_CODE WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' AND pay_unit_master.branch_status = 0 AND datecurrent BETWEEN STR_TO_DATE('" + txt_work_img_from.Text + "','%d/%m/%Y') AND STR_TO_DATE('" + txt_work_img_to.Text + "','%d/%m/%Y') ORDER BY datecurrent DESC LIMIT 200", d.con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_work_image.DataSource = ds;
                grd_work_image.DataBind();
                grd_work_image.Visible = true;
                d.con.Close();
            }
            else
            {
                grd_work_image.DataSource = null;
                grd_work_image.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found For This Branch');", true);
            }
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
   
    protected void grd_work_image_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }
    protected void SearchGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string status = "";
        int index = SearchGridView.Rows[e.NewEditIndex].RowIndex;
        int request_id = int.Parse(SearchGridView.DataKeys[index].Values[0].ToString());
        d.con.Open();
        MySqlCommand cmd_status = new MySqlCommand("Select status From pay_service_master where id = '" + request_id + "'", d.con);
        MySqlDataReader dr_status = cmd_status.ExecuteReader();
        if (dr_status.Read())
        {
            if (dr_status.GetValue(0).ToString() == "Quotation Generate")
            {
                status = "Quotation Approved";
            }
            else if (dr_status.GetValue(0).ToString() == "Bill Generated")
            {
                status = "Completed";
            }
            else { status = dr_status.GetValue(0).ToString(); }
        }
        dr_status.Dispose();
        d.con.Close();
        d.operation("Update pay_service_master set status = '" + status + "' where id = '" + request_id + "'");
        fill_servicesgv();
    }
    protected void SearchGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int request_id = int.Parse(SearchGridView.DataKeys[e.RowIndex].Values[0].ToString());
        Session["Ticket_ID"] = request_id;
        ModalPopupExtender1.Show();
        //string status = "";
        //int request_id =int.Parse(SearchGridView.DataKeys[e.RowIndex].Values[0].ToString());

        //d.con.Open();
        //MySqlCommand cmd_status = new MySqlCommand("Select status From pay_service_master where id = '" + request_id + "'", d.con);
        //MySqlDataReader dr_status = cmd_status.ExecuteReader();
        //if (dr_status.Read())
        //{
        //    if (dr_status.GetValue(0).ToString() == "Quotation Generate")
        //    {
        //        status = "Quotation Rejected";
        //    }
        //    else if (dr_status.GetValue(0).ToString() == "Bill Generated")
        //    {
        //        status = "Completed";
        //    }
        //    else { status = dr_status.GetValue(0).ToString(); }
        //}
        //dr_status.Dispose();
        //d.con.Close();
        //d.operation("Update pay_service_master set status = '" + status + "' where id = '" + request_id + "'");
        fill_servicesgv();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        fill_servicesgv();
    }
    protected void lnk_add_category_Click(object sender, EventArgs e)
    {
        if (ddl_asset_type.SelectedValue != "Select")
        {
            Session["SERVICE"] = ddl_asset_type.SelectedValue;
            ModalPopupExtender2.Show();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Service type !!!');", true);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        fill_servicesgv();
    }
    protected void ddl_asset_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "7";

        // Category
        ddl_category.Items.Clear();
        System.Data.DataTable dt_service = new System.Data.DataTable();
        MySqlDataAdapter cmd_service = new MySqlDataAdapter("Select distinct(category) from  pay_service_category where service = '" + ddl_asset_type.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            cmd_service.Fill(dt_service);
            if (dt_service.Rows.Count > 0)
            {
                ddl_category.DataSource = dt_service;
                ddl_category.DataTextField = dt_service.Columns[0].ToString();
                ddl_category.DataValueField = dt_service.Columns[0].ToString();
                ddl_category.DataBind();
            }
            cmd_service.Dispose();
            dt_service.Dispose();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void btn_logout_click(Object sender, EventArgs e)
    {
        Session["comp_code"] = null;
        Session["COMP_NAME"] = null;
        Session["UNIT_CODE"] = null;
        Session["UNIT_NAME"] = null;
        Session["USERNAME"] = null;
        Session["CLIENT_CODE"] = null;
        Session["ZONE_NAME"] = null;
        Session["REGION_NAME"] = null;

        Response.Redirect("Login_Page.aspx");
    }

    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            string imageUrl, imageUrl2 = "";
            if (dr["image_name"].ToString() != "")
            {
                imageUrl = "~/attendance_images/" + dr["image_name"];
                (e.Row.FindControl("image_name") as Image).ImageUrl = imageUrl;

            }

        }
    }
    //vikas 17-12
    protected void grid_viwe()
    {
        try
        {

            companyGridView.Visible = true;
            d.con1.Open();

            // MySqlDataAdapter adp_grid = new MySqlDataAdapter("select id, (select emp_name from pay_employee_master where emp_code = pay_site_audit.emp_code) as emp_code,grade_name,concat('Guards present On Right Place ? :- ',que_1_ans) as que_1_ans,que_1_path,concat( 'Is Ac Working ? :- ',que_2_ans) as que_2_ans,que_2_path,concat('Is Guards strength correct ? :- ',que_3_ans) as que_3_ans,que_3_path,concat('Is CCTV Working Properly ? :- ',que_4_ans) as que_4_ans,que_4_path,concat('Is Traning Needed On Site ? :- ',que_5_ans) as que_5_ans,que_5_path,location,comment,remark from pay_site_audit where comp_code='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT  id,(SELECT emp_name FROM pay_employee_master WHERE emp_code = pay_site_audit.emp_code) AS 'emp_code',grade_name,(case when grade_name='HOUSEKEEPING'	then  CONCAT('Meet to concern person ? :- ', que_1_ans) when grade_name='SECURITY GUARD'	then 	 CONCAT('Meet to concern person ? :- ', que_1_ans) when grade_name='R&M'	then 	CONCAT('Meet to concern person ? :- ', que_1_ans) else CONCAT('Meet to concern person ? :- ', que_1_ans) end)AS 'que_1_ans',que_1_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Meet to housekeeping employee ? :- ', que_2_ans) when grade_name='SECURITY GUARD'	then  CONCAT('Meet to security guard employee ? :- ', que_2_ans) when grade_name='R&M'	then 	CONCAT('Check wiring  ? :- ', que_2_ans) else CONCAT('Meet to housekeeping employee ? :- ', que_2_ans)	end )AS 'que_2_ans',que_2_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check dressup & grooming? :- ', que_3_ans)  when grade_name='SECURITY GUARD' then  CONCAT('Check dressup & grooming ? :- ', que_3_ans) when grade_name='R&M'	then 	CONCAT('Check electrical / electronics items? :- ', que_3_ans)else CONCAT('Check dressup & grooming? :- ', que_3_ans) 	end )AS 'que_3_ans',que_3_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check I-card & shooes ? :- ', que_4_ans) when grade_name='SECURITY GUARD' then  CONCAT('Check I-card & shooes ? :- ', que_4_ans)  when grade_name='R&M'	then 	CONCAT('Check furniture ? :- ', que_4_ans)else CONCAT('Check I-card & shooes ? :- ', que_4_ans)	end )AS 'que_4_ans',que_4_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check office cleaning  ? :- ', que_5_ans) when grade_name='SECURITY GUARD' then  CONCAT('Guard maintain register proparly ? :- ', que_5_ans)  when grade_name='R&M'	then 	CONCAT('Check plumbing work? :- ', que_5_ans) else CONCAT('Check office cleaning  ? :- ', que_5_ans)	end )AS 'que_5_ans',que_5_path, (case when grade_name='HOUSEKEEPING'	then CONCAT('Check washroom cleaning ? :- ', que_6_ans)  when grade_name='SECURITY GUARD' then  CONCAT('Guard is present on place  ? :- ', que_6_ans)  when grade_name='R&M'	then 	CONCAT('Is there any query or concerns ? :- ', que_6_ans) else CONCAT('Check washroom cleaning ? :- ', que_6_ans)	end )AS 'que_6_ans',que_6_path, location,comment,remark,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' else '' end ) as 'Status' FROM pay_site_audit  where comp_code='" + Session["COMP_CODE"].ToString() + "' ORDER BY id DESC ", d.con);
            DataSet ds = new DataSet();
            adp_grid.Fill(ds);
            companyGridView.DataSource = ds;
            companyGridView.DataBind();
            d.con1.Close();



            // }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void companyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl, imageUrl2, imageUrl3, imageUrl4, imageUrl5, imageUrl6 = "";
            if (dr["que_1_path"].ToString() != "")
            {

                imageUrl = "~/site_audit/" + dr["que_1_path"];
                (e.Row.FindControl("que_1_path") as Image).ImageUrl = imageUrl;

            }

            if (dr["que_2_path"].ToString() != "")
            {
                imageUrl2 = "~/site_audit/" + dr["que_2_path"];
                (e.Row.FindControl("que_2_path") as Image).ImageUrl = imageUrl2;

            }
            if (dr["que_3_path"].ToString() != "")
            {
                imageUrl3 = "~/site_audit/" + dr["que_3_path"];
                (e.Row.FindControl("que_3_path") as Image).ImageUrl = imageUrl3;

            }
            if (dr["que_4_path"].ToString() != "")
            {
                imageUrl4 = "~/site_audit/" + dr["que_4_path"];
                (e.Row.FindControl("que_4_path") as Image).ImageUrl = imageUrl4;

            }
            if (dr["que_5_path"].ToString() != "")
            {
                imageUrl5 = "~/site_audit/" + dr["que_5_path"];
                (e.Row.FindControl("que_5_path") as Image).ImageUrl = imageUrl5;

            }
            if (dr["que_6_path"].ToString() != "")
            {
                imageUrl6 = "~/site_audit/" + dr["que_6_path"];
                (e.Row.FindControl("que_6_path") as Image).ImageUrl = imageUrl6;

            }





        }

    }
    protected void btn_show_Click1(object sender, EventArgs e)
    {
        hidtab.Value = "10";
        try
        {

           
            System.Data.DataTable dt_item = new System.Data.DataTable();

            MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT  id,(SELECT emp_name FROM pay_employee_master WHERE emp_code = pay_site_audit.emp_code) AS 'emp_code',grade_name,(case when grade_name='HOUSEKEEPING'	then  CONCAT('Meet to concern person ? :- ', que_1_ans) when grade_name='SECURITY GUARD'	then 	 CONCAT('Meet to concern person ? :- ', que_1_ans) when grade_name='R&M'	then 	CONCAT('Meet to concern person ? :- ', que_1_ans) else CONCAT('Meet to concern person ? :- ', que_1_ans) end)AS 'que_1_ans',que_1_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Meet to housekeeping employee ? :- ', que_2_ans) when grade_name='SECURITY GUARD'	then  CONCAT('Meet to security guard employee ? :- ', que_2_ans) when grade_name='R&M'	then 	CONCAT('Check wiring  ? :- ', que_2_ans) else CONCAT('Meet to housekeeping employee ? :- ', que_2_ans)	end )AS 'que_2_ans',que_2_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check dressup & grooming? :- ', que_3_ans)  when grade_name='SECURITY GUARD' then  CONCAT('Check dressup & grooming ? :- ', que_3_ans) when grade_name='R&M'	then 	CONCAT('Check electrical / electronics items? :- ', que_3_ans)else CONCAT('Check dressup & grooming? :- ', que_3_ans) 	end )AS 'que_3_ans',que_3_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check I-card & shooes ? :- ', que_4_ans) when grade_name='SECURITY GUARD' then  CONCAT('Check I-card & shooes ? :- ', que_4_ans)  when grade_name='R&M'	then 	CONCAT('Check furniture ? :- ', que_4_ans)else CONCAT('Check I-card & shooes ? :- ', que_4_ans)	end )AS 'que_4_ans',que_4_path,(case when grade_name='HOUSEKEEPING'	then CONCAT('Check office cleaning  ? :- ', que_5_ans) when grade_name='SECURITY GUARD' then  CONCAT('Guard maintain register proparly ? :- ', que_5_ans)  when grade_name='R&M'	then 	CONCAT('Check plumbing work? :- ', que_5_ans) else CONCAT('Check office cleaning  ? :- ', que_5_ans)	end )AS 'que_5_ans',que_5_path, (case when grade_name='HOUSEKEEPING'	then CONCAT('Check washroom cleaning ? :- ', que_6_ans)  when grade_name='SECURITY GUARD' then  CONCAT('Guard is present on place  ? :- ', que_6_ans)  when grade_name='R&M'	then 	CONCAT('Is there any query or concerns ? :- ', que_6_ans) else CONCAT('Check washroom cleaning ? :- ', que_6_ans)	end )AS 'que_6_ans',que_6_path, location,comment,remark,(case reject when 0 then 'Pending' when 1 then 'Reject' when 2 then 'Approved' else '' end ) as 'Status' FROM pay_site_audit where emp_code='" + dd1_super.SelectedValue + "' and unit_code='" + ddlunitselect.SelectedValue+ "' order by id desc", d.con);

            cmd_item.Fill(dt_item);

            if (dt_item.Rows.Count > 0)
            {

                companyGridView.DataSource = dt_item;

                companyGridView.DataBind();
            }
            else
            {
                companyGridView.DataSource = null;

                //ddl_client.DataValueField = dt_item.Columns[0].ToString(); vvv
                companyGridView.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records Found');", true);
            }
            dt_item.Dispose();
            d.con.Close();
            //ddl_client.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
        }


    }
  


    protected void employee_list1()
    {
        try
        {
            d1.con.Open();
            dd1_super.Items.Clear();


            MySqlCommand cmd_1 = new MySqlCommand("SELECT distinct(EMP_CODE), field_officer_name  FROM pay_op_management where comp_code = '" + Session["COMP_CODE"].ToString() + "' and UNIT_CODE ='" + ddlunitselect.SelectedValue + "' order by field_officer_name", d1.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                dd1_super.DataSource = ds1.Tables[0];
                dd1_super.DataValueField = "EMP_CODE";
                dd1_super.DataTextField = "field_officer_name";
                dd1_super.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dd1_super.Items.Insert(0, new ListItem("Select", "Select"));
            d1.con.Close();
        }
    }
    protected void grd_work_image_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_work_image.UseAccessibleHeader = false;
            grd_work_image.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void companyGridView_PreRender1(object sender, EventArgs e)
    {
        try
        {
            companyGridView.UseAccessibleHeader = false;
            companyGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }


    protected void GradeGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            GradeGridView.UseAccessibleHeader = false;
            GradeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }


    protected void btn_add_Click(object sender, EventArgs e)
    {
        hidtab.Value = "7";
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        hidtab.Value = "8";
    }

    protected void tab_attendaces_details_click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

            string where = " and pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "'";
            if (!ddlunitselect.SelectedValue.Equals("ALL"))
            {
                where = " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'";
            }
            // dscmd = new MySqlDataAdapter("Select comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,attendances_intime,attendances_outtime,camera_intime,camera_outtime,Camera_intime_images,Camera_outtime_images,Attendances_intime_images,Attendances_outtime_images from pay_android_attendance_logs where UNIT_CODE = '" + ddlunitselect.SelectedValue + "' AND comp_code = '" + Session["comp_code"].ToString() + "' and date(date_time) between str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + "','%d/%m/%Y') order by date_time desc limit 200", d.con);
            //MySqlDataAdapter dscmd = new MySqlDataAdapter("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE, EMP_CODE, UNIT_LATITUDE, UNIT_LONGTUTDE, EMP_LATITUDE, EMP_LONGTUTDE, DISTANCES, case when ADDRESS like '%IOException%' then 'NA' else ADDRESS END as ADDRESS, upper(EMP_NAME) as emp_name, Date_Time, attendances_intime, attendances_outtime, camera_intime, camera_outtime, Camera_intime_images, Camera_outtime_images, Attendances_intime_images, Attendances_outtime_images FROM pay_android_attendance_logs inner join pay_unit_master on pay_android_attendance_logs.comp_CODE = pay_unit_master.comp_code and pay_android_attendance_logs.unit_CODE = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' AND date_time BETWEEN str_to_date('" + txt_satrtdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_enddate.Text + "','%d/%m/%Y') ORDER BY date_time DESC LIMIT 200", d.con);
            

            //MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT unit_name as unit_code,emp_name,grade_desc,shifttime,CASE punctuality WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'punctuality',CASE uniforms WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'uniforms',  CASE cap WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'cap',  CASE shoes WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shoes',  CASE belt WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'belt',  CASE id_card WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'id_card',  CASE shaving WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shaving',  CASE hairs WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'hairs',  CASE nails WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'nails', CASE briefing WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'briefing',  intime_imgpath,  outtime_imgpath,remarks,location_add FROM  pay_tab_employee_attendances_details INNER JOIN pay_unit_master ON pay_tab_employee_attendances_details.client_code = pay_unit_master.client_code AND pay_tab_employee_attendances_details.unit_code = pay_unit_master.unit_code WHERE pay_tab_employee_attendances_details.comp_code='" + Session["comp_code"].ToString() + "'  AND pay_tab_employee_attendances_details.unit_code = '" + ddlunitselect.SelectedValue + "'  and date(currdate) between ", d.con);
            MySqlDataAdapter grd_client = new MySqlDataAdapter("SELECT unit_name AS 'unit_code', upper(emp_name) as emp_name, grade_desc, shifttime, CASE punctuality WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'punctuality', CASE uniforms WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'uniforms', CASE cap WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'cap', CASE shoes WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shoes', CASE belt WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'belt', CASE id_card WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'id_card', CASE shaving WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'shaving', CASE hairs WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'hairs', CASE nails WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'nails', CASE briefing WHEN 'false' THEN 'No' WHEN 'true' THEN 'Yes' END AS 'briefing', intime_imgpath, outtime_imgpath, remarks, location_add FROM pay_tab_employee_attendances_details INNER JOIN pay_unit_master ON pay_tab_employee_attendances_details.client_code = pay_unit_master.client_code AND pay_tab_employee_attendances_details.unit_code = pay_unit_master.unit_code WHERE pay_unit_master.comp_code = '" + Session["comp_code"].ToString() + "' AND currdate BETWEEN str_to_date('" + txt_fromdate.Text + "','%d/%m/%Y') and str_to_date('" + txt_todate.Text + "','%d/%m/%Y') " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' AND pay_unit_master.branch_status = 0", d.con);
            DataTable dt_client = new DataTable();
            grd_client.Fill(dt_client);
            if (dt_client.Rows.Count > 0)
            {

                gv_emp_attendance.DataSource = dt_client;
                gv_emp_attendance.DataBind();

            }
            else
            {
                gv_emp_attendance.DataSource = null;
                gv_emp_attendance.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not Found For This Branch');", true);
            }


        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con.Close();
            txt_fromdate.Text = "";
            txt_todate.Text = "";
        }
    }
    protected void gv_emp_attendance_RowDataBound(object sender, GridViewRowEventArgs e)
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
            string imageUrl, imageUrl2 = "";
            if (dr["intime_imgpath"].ToString() != "")
            {

                imageUrl = "~/tab_attendance_images/" + dr["intime_imgpath"];
                (e.Row.FindControl("intime_imgpath") as Image).ImageUrl = imageUrl;

            }

            if (dr["outtime_imgpath"].ToString() != "")
            {
                imageUrl2 = "~/tab_attendance_images/" + dr["outtime_imgpath"];
                (e.Row.FindControl("outtime_imgpath") as Image).ImageUrl = imageUrl2;

            }
        }

    }
    protected void gv_emp_attendance_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_emp_attendance.UseAccessibleHeader = false;
            gv_emp_attendance.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void btn_show_tracking_region_Click(object sender, EventArgs e)
    {
        hidtab.Value = "2";
        d.con.Open();
            try {

                string where = " and pay_unit_master.client_code = '" + Session["CLIENT_CODE"].ToString() + "'";
                if (!ddlunitselect.SelectedValue.Equals("ALL"))
                {
                    where = " and pay_unit_master.unit_code = '" + ddlunitselect.SelectedValue + "'";
                }
                MySqlDataAdapter dscmd_emp;
                //dscmd_emp = new MySqlDataAdapter("SELECT pay_geolocation_address.id,pay_geolocation_address.unit_code,pay_geolocation_address.client_code,( SELECT CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END ) AS 'emp_code', cur_address, cur_latitude, cur_longtitude, cur_date FROM pay_geolocation_address INNER JOIN pay_employee_master ON pay_geolocation_address.EMP_CODE = pay_employee_master.emp_code  WHERE pay_geolocation_address.comp_code = '" + Session["comp_code"].ToString() + "' and pay_geolocation_address.unit_code='" + ddlunitselect.SelectedValue + "' and pay_geolocation_address.cur_date BETWEEN   STR_TO_DATE('" + txt_emp_from.Text + "', '%d/%m/%Y') and  STR_TO_DATE('" + txt_emp_to.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s')    ORDER BY pay_geolocation_address.cur_date DESC LIMIT 200", d.con);
                dscmd_emp = new MySqlDataAdapter("SELECT pay_geolocation_address.id, `pay_unit_master`.`unit_name`, pay_geolocation_address.client_code, CASE pay_employee_master.Employee_type WHEN 'Reliever' THEN CONCAT(pay_employee_master.emp_name, '-', 'Reliever') ELSE pay_employee_master.emp_name END AS 'emp_code', case when cur_address like '%IOException%' then 'NA' else cur_address END as cur_address, cur_latitude, cur_longtitude, cur_date FROM pay_geolocation_address inner join pay_unit_master on pay_geolocation_address.comp_CODE = pay_unit_master.comp_code and pay_geolocation_address.unit_CODE = pay_unit_master.unit_code INNER JOIN pay_employee_master ON pay_geolocation_address.EMP_CODE = pay_employee_master.emp_code and pay_unit_master.comp_CODE = pay_employee_master.comp_code and pay_unit_master.unit_CODE = pay_employee_master.unit_code WHERE pay_geolocation_address.cur_date BETWEEN STR_TO_DATE('" + txt_emp_from.Text + "', '%d/%m/%Y') and  STR_TO_DATE('" + txt_emp_to.Text + " 23:59:59', '%d/%m/%Y %H:%i:%s') and pay_unit_master.comp_code='" + Session["comp_code"].ToString() + "' " + where + " AND pay_unit_master.txt_zone = '" + Session["ZONE_NAME"].ToString() + "' AND pay_unit_master.ZONE='" + Session["REGION_NAME"].ToString() + "' AND pay_unit_master.branch_status = 0 ORDER BY pay_geolocation_address.cur_date DESC LIMIT 200", d.con);
                DataSet ds1 = new DataSet();
                dscmd_emp.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    GridView_employee_tracking_region.DataSource = ds1;
                    GridView_employee_tracking_region.DataBind();
                    GridView_employee_tracking_region.Visible = true;
                    //GradeGridView.Visible = false;
                   // grd_work_image.Visible = false;
                }
                else
                {
                    GridView_employee_tracking_region.DataSource = null;
                    GridView_employee_tracking_region.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records');", true);
                }
  
            
            }
            catch (Exception ex) { throw ex; }
            finally {d.con.Close(); }
        
        

    }
    protected void GridView_employee_tracking_region_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView_employee_tracking_region, "Select$" + e.Row.RowIndex);

        }

        e.Row.Cells[1].Visible = false;
    }

    protected void GridView_employee_tracking_region_PreRender(object sender, EventArgs e)
    {
        try
        {
            GridView_employee_tracking_region.UseAccessibleHeader = false;
            GridView_employee_tracking_region.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void GridView_employee_tracking_region_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id_no = GridView_employee_tracking_region.SelectedRow.Cells[1].Text;

        Session["UNIT_NO"] = ddlunitselect.SelectedValue.ToString();
        Session["MAP_ID"] = id_no;

        Session["MAP_ADDRESS"] = GridView_employee_tracking_region.SelectedRow.Cells[6].Text;
        Session["MAP_LONGITUDE"] = GridView_employee_tracking_region.SelectedRow.Cells[4].Text;
        Session["MAP_LATTITUDE"] = GridView_employee_tracking_region.SelectedRow.Cells[3].Text;
        Session["MAP_AREA"] = "100";


        Response.Redirect("location_map.aspx");





    }
}

