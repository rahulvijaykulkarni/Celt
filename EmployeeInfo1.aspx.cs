using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


public partial class EmployeeInfo1 : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    EmployeeBAL ebl2 = new EmployeeBAL();
    string a = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) == "R")
        {
           
           // btnhelp.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) == "U")
        {
           
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) == "C")
        {
           
           // btnhelp.Visible = false;
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Employee Master", Session["COMP_CODE"].ToString()) != "D")
        {
        }
           
        d.con1.Open();
        try
        {
            MySqlCommand cmdheads = new MySqlCommand("SELECT * FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader drheads = cmdheads.ExecuteReader();
            if (drheads.Read())
            {
                lblhead1.Text = drheads.GetValue(21).ToString();//lbledithead1.Text =drheads.GetValue(24).ToString();
                lblhead2.Text = drheads.GetValue(22).ToString(); //lbledithead2.Text =drheads.GetValue(25).ToString();
                lblhead3.Text = drheads.GetValue(23).ToString(); // lbledithead3.Text = drheads.GetValue(26).ToString();
                lblhead4.Text = drheads.GetValue(24).ToString();//  lbledithead4.Text =drheads.GetValue(27).ToString();
                lblhead5.Text = drheads.GetValue(25).ToString();//lbledithead5.Text =drheads.GetValue(28).ToString();
                lblhead6.Text = drheads.GetValue(26).ToString();//lbledithead6.Text = drheads.GetValue(29).ToString();
                lblhead7.Text = drheads.GetValue(27).ToString();// lbledithead7.Text =drheads.GetValue(30).ToString();
                lblhead8.Text = drheads.GetValue(28).ToString();// lbledithead8.Text =drheads.GetValue(31).ToString();
                lblhead9.Text = drheads.GetValue(29).ToString();// lbledithead9.Text =drheads.GetValue(32).ToString();
                lblhead10.Text = drheads.GetValue(30).ToString();//lbledithead10.Text =drheads.GetValue(33).ToString();
                lblhead11.Text = drheads.GetValue(31).ToString();// lbledithead11.Text = drheads.GetValue(34).ToString();
                lblhead12.Text = drheads.GetValue(32).ToString();// lbledithead12.Text =drheads.GetValue(35).ToString();
                lblhead13.Text = drheads.GetValue(33).ToString();//  lbledithead13.Text =drheads.GetValue(36).ToString();
                lblhead14.Text = drheads.GetValue(34).ToString();// lbledithead14.Text = drheads.GetValue(37).ToString();
                lblhead15.Text = drheads.GetValue(35).ToString();//  lbledithead15.Text =drheads.GetValue(38).ToString();
                lbllsehead1.Text = drheads.GetValue(36).ToString();// lbleditlsehead1.Text =drheads.GetValue(39).ToString();
                lbllsehead2.Text = drheads.GetValue(37).ToString();//  lbleditlsehead2.Text =drheads.GetValue(40).ToString();
                lbllsehead3.Text = drheads.GetValue(38).ToString();// lbleditlsehead3.Text =drheads.GetValue(41).ToString();
                lbllsehead4.Text = drheads.GetValue(39).ToString();//lbleditlsehead4.Text = drheads.GetValue(42).ToString();
                lbllsehead5.Text = drheads.GetValue(40).ToString();// lbleditlsehead5.Text =drheads.GetValue(43).ToString();
                //dloan=drheads.GetValue(44).ToString();  //dloan=drheads.GetValue(44).ToString();
                //lbldhead1.Text = drheads.GetValue(41).ToString(); //  lbleditdhead1.Text =drheads.GetValue(45).ToString();
                lbldhead2.Text = drheads.GetValue(41).ToString();// lbleditdhead2.Text = drheads.GetValue(46).ToString();
                lbldhead3.Text = drheads.GetValue(42).ToString();//   lbleditdhead3.Text = drheads.GetValue(47).ToString();
                lbldhead4.Text = drheads.GetValue(43).ToString();//   lbleditdhead4.Text = drheads.GetValue(48).ToString();
                lbldhead5.Text = drheads.GetValue(44).ToString();//  lbleditdhead5.Text =drheads.GetValue(49).ToString();
                lbldhead6.Text = drheads.GetValue(45).ToString();// lbleditdhead6.Text =drheads.GetValue(50).ToString();
                lbldhead7.Text = drheads.GetValue(46).ToString();// lbleditdhead7.Text = drheads.GetValue(51).ToString();
                lbldhead8.Text = drheads.GetValue(47).ToString();// lbleditdhead8.Text = drheads.GetValue(52).ToString();
                lbldhead9.Text = drheads.GetValue(48).ToString();//lbleditdhead9.Text =drheads.GetValue(53).ToString();
                lbldhead10.Text = drheads.GetValue(49).ToString();// lbleditdhead10.Text = drheads.GetValue(54).ToString();           
            }
            if (lblhead1.Text == "") { txtlhead1.Visible = false; }
            if (lblhead2.Text == "") { txtlhead2.Visible = false; }
            if (lblhead3.Text == "") { txtlhead3.Visible = false; }
            if (lblhead4.Text == "") { txtlhead4.Visible = false; }
            if (lblhead5.Text == "") { txtlhead5.Visible = false; }
            if (lblhead6.Text == "") { txtlhead6.Visible = false; }
            if (lblhead7.Text == "") { txtlhead7.Visible = false; }
            if (lblhead8.Text == "") { txtlhead8.Visible = false; }
            if (lblhead9.Text == "") { txtlhead9.Visible = false; }
            if (lblhead10.Text == "") { txtlhead10.Visible = false; }
            if (lblhead11.Text == "") { txtlhead11.Visible = false; }
            if (lblhead12.Text == "") { txtlhead12.Visible = false; }
            if (lblhead13.Text == "") { txtlhead13.Visible = false; }
            if (lblhead14.Text == "") { txtlhead14.Visible = false; }
            if (lblhead15.Text == "") { txtlhead15.Visible = false; }
            if (lbllsehead1.Text == "") { txtlsehead1.Visible = false; }
            if (lbllsehead2.Text == "") { txtlsehead2.Visible = false; }
            if (lbllsehead3.Text == "") { txtlsehead3.Visible = false; }
            if (lbllsehead4.Text == "") { txtlsehead4.Visible = false; }
            if (lbllsehead5.Text == "") { txtlsehead5.Visible = false; }
            //if (lbldhead1.Text == "") { txtdhead1.Visible = false; }
            if (lbldhead2.Text == "") { txtdhead2.Visible = false; }
            if (lbldhead3.Text == "") { txtdhead3.Visible = false; }
            if (lbldhead4.Text == "") { txtdhead4.Visible = false; }
            if (lbldhead5.Text == "") { txtdhead5.Visible = false; }
            if (lbldhead6.Text == "") { txtdhead6.Visible = false; }
            if (lbldhead7.Text == "") { txtdhead7.Visible = false; }
            if (lbldhead8.Text == "") { txtdhead8.Visible = false; }
            if (lbldhead9.Text == "") { txtdhead9.Visible = false; }
            if (lbldhead10.Text == "") { txtdhead10.Visible = false; }
            //---------------------------------------------        
         //   Panel8.Visible = true;
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }

        if (!IsPostBack)
        {
            
           
           
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT role_name FROM pay_role_master where comp_code='"+Session["comp_code"].ToString()+"'", d.con1);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "role_name";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--Select Role--", ""));
            //btnnew_Click();



            ddl_grade.Items.Clear();
            MySqlCommand cmd1 = new MySqlCommand("SELECT GRADE_CODE, GRADE_DESC FROM pay_grade_master  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
            d.con.Open();
            try
            {
                int i = 0;
                MySqlDataReader dr_item = cmd1.ExecuteReader();
                while (dr_item.Read())
                {
                    ddl_grade.Items.Insert(i++, new ListItem( dr_item[0].ToString()));
                }
                dr_item.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                ddl_grade.Items.Insert(0, new ListItem("Select", "Select"));
            }

            ddl_permstate.Items.Clear();
            ddl_state.Items.Clear();
            ddl_location.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    ddl_permstate.Items.Add(dr_item1[0].ToString());
                    ddl_state.Items.Add(dr_item1[0].ToString());
                    ddl_location.Items.Add(dr_item1[0].ToString());
                }
                dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }

            // txt_particular.Items.Insert(0, "Select");
            ddl_permstate.Items.Insert(0, new ListItem( "Select"));
            ddl_state.Items.Insert(0, new ListItem("Select"));
            txt_presentcity.Items.Insert(0, new ListItem("Select"));
            ddl_location.Items.Insert(0, new ListItem("Select"));
            ddl_location_city.Items.Insert(0, new ListItem("Select"));
            // txt_permanantcity.Items.Insert(0, new ListItem("Select"));
            ddl_dept.Items.Insert(0, new ListItem("Select", "Select"));
            
            if(Session["EMP_CODE"].ToString() != "")
            {
                getEmployee(Session["EMP_CODE"].ToString());
                Session["EMP_CODE"] = "";
            }
            else
            {
               // btnnew_Click();
            }
            clien_namelist();
            newpanel.Visible = true;
            ddl_unitcode.Enabled = false;
        }
       
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

      //  btnhelp_Click(sender, e);

      //  clien_namelist();
        //newpanel.Visible = false;
       // ddl_employmentstatus_SelectedIndexChanged(null, null);
        // if (SearchGridView.SelectedRow.Cells.Count >0)
        //{
        //    btnupdate.Visible = true;
        //    btndelete.Visible = true;

        //    btn_add.Visible = false;
        //}
        
        //set_data();
        getEmployee(Session["EMP_CODE"].ToString());
        newpanel.Visible = true;
    }

    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_unitcode.Items.Clear();
        ////string unitcode = ddl_clientname.Text;
        ////MySqlCommand cmd1 = new MySqlCommand("SELECT  concat(UNIT_CODE, '-', UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE comp_code='" + Session["comp_code"].ToString() + "' and Client_code='"+ddl_clientname.SelectedValue+ "'", d.con);
        ////d.con.Open();
        ////try
        ////{
        ////    int i = 0;
        ////    MySqlDataReader dr_item = cmd1.ExecuteReader();
        ////    while (dr_item.Read())
        ////    {
        ////        ddl_unitcode.Items.Insert(i++, new ListItem(dr_item[0].ToString()));
        ////    }
        ////    dr_item.Close();
        ////}
        ////catch (Exception ex) { throw ex; }
        ////finally
        ////{
        ////    d.con.Close();
        ////    ddl_unitcode.Items.Insert(0, new ListItem("Select", "Select"));
        ////}

        MySqlCommand cmd = new MySqlCommand("SELECT UNIT_CODE,concat(UNIT_NAME,'-',state_name,'-',unit_city) as UNIT_NAME, concat(UNIT_CODE, '-', UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE comp_code='" + Session["comp_code"].ToString() + "' and Client_code=(select client_code from pay_client_master where client_name='" + ddl_clientname.SelectedItem.Text + "')", d.con);
        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        ddl_unitcode.DataSource = ds.Tables[0];
        ddl_unitcode.DataValueField = "UNIT_CODE";
        ddl_unitcode.DataTextField = "CUNIT";
        ddl_unitcode.DataBind();
        ddl_unitcode.Items.Insert(0, new ListItem("Select", "Select"));
        //btnnew_Click();
        newpanel.Visible = true;
       

    }
    protected void clien_namelist()
    {
        ddl_clientname.Items.Clear();
        MySqlCommand cmd1 = new MySqlCommand("SELECT CLIENT_NAME from  pay_client_master  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
        d.con.Open();
        try
        {
            int i = 0;
            MySqlDataReader dr_item = cmd1.ExecuteReader();
            while (dr_item.Read())
            {
                ddl_clientname.Items.Insert(i++, new ListItem(dr_item[0].ToString()));
            }
            dr_item.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            ddl_clientname.Items.Insert(0, new ListItem("Select", "Select"));
        }

    
    }
    protected void btn_add_employee_Click(object sender, EventArgs e)
    {
        d.reset(this);
      //  btnhelp_Click(sender, e);
        newpanel.Visible = true;
       
     //  btnhelp_Click(sender, e);
       
    }
   

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (txt_pfemployeepercentage.Text == "")
        {
            txt_pfemployeepercentage.Text = "0";
        }
      //  else
      //  {
      //  btnhelp.Visible = true;
        int result = 0;
        //int rateresult = 0;
       // set_data();
        try
        {            
            d.con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT EMP_CODE+'-'+UNIT_NAME FROM pay_employee_master,pay_unit_master WHERE pay_employee_master.comp_code=pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE=pay_unit_master.UNIT_CODE AND (pay_employee_master.EMP_NAME='" + txt_eename.Text + "' OR pay_employee_master.EMP_CODE='" + txt_eecode.Text + "') AND pay_employee_master.comp_code='" + Session["comp_code"].ToString().ToString() + "'", d.con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string oldemp = dr.GetValue(0).ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This employee name is already exist old code '+'"+ oldemp +"'+' , try another name.');", true);
            }
            d.con.Close();

            if (txt_employeeaccountnumber.Text != "")
            {
                d.con.Open();
                MySqlCommand cmd_1 = new MySqlCommand("SELECT BANK_EMP_AC_CODE,EMP_NAME,EMP_CODE FROM pay_employee_master WHERE BANK_EMP_AC_CODE='" + txt_employeeaccountnumber.Text + "' and comp_code='"+Session["comp_code"].ToString()+"'", d.con);
                MySqlDataReader dr_1 = cmd_1.ExecuteReader();
                if (dr_1.Read())
                {
                    string emp_name = dr_1.GetValue(1).ToString();
                    string emp_id = dr_1.GetValue(2).ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This bank account number is already exist for employee '+'" + emp_id + "'+'-'+'" + emp_name + "'+' , try another account number.');", true);



                    d.con.Close();
                }
                else
                {
                    int result1 = 0;
                    string newdate = Convert.ToString(System.DateTime.Now);
                    string enteruserid = Session["USERID"].ToString();
                    string entrydatestmp = Session["system_curr_date"].ToString();
                    string emp_name = Session["USERNAME"].ToString();

                    //result = ebl2.EmployeeInsert(Session["comp_code"].ToString(), txt_eecode.Text, txt_eename.Text, txt_eefatharname.Text, txt_birthdate.Text, txt_joiningdate.Text, txt_confirmationdate.Text, txt_leftdate.Text, ddl_gender.Text, txt_maritalstaus.Text, txt_highestqualification.Text, ddl_religion.SelectedItem.Text, ddl_bloodgroup.Text, float.Parse((txt_weight.Text)), float.Parse((txt_height.Text)), txt_hobbies.Text, txt_presentaddress.Text, txt_presentcity.Text, ddl_state.SelectedItem.Text, txt_presentpincode.Text, txt_permanantaddress.Text, txt_permanantcity.Text, ddl_permstate.SelectedItem.Text, txt_permanantpincode.Text, txt_mobilenumber.Text, txt_residencecontactnumber.Text, ddl_employmentstatus.SelectedItem.Text, ddl_location.SelectedItem.Text, float.Parse((txt_basicpay.Text)), txt_panno.Text, ddl_pfdeductionflag.SelectedItem.Text, txt_pfnumber.Text, ddl_esicdeductionflag.SelectedItem.Text, txt_esicnumber.Text, ddl_ptaxdeductionflag.SelectedItem.Text, txt_ptaxnumber.Text, txt_employeeaccountnumber.Text, ddl_bankcode.Text, ddl_grade.Text, ddl_unitcode.Text, ddl_dept.Text, float.Parse(txtlhead1.Text), float.Parse(txtlhead2.Text), float.Parse(txtlhead3.Text), float.Parse(txtlhead4.Text), float.Parse(txtlhead5.Text), float.Parse(txtlhead6.Text), float.Parse(txtlhead7.Text), float.Parse(txtlhead8.Text), float.Parse(txtlhead9.Text), float.Parse(txtlhead10.Text), float.Parse(txtlhead11.Text), float.Parse(txtlhead12.Text), float.Parse(txtlhead13.Text), float.Parse(txtlhead14.Text), float.Parse(txtlhead15.Text), float.Parse(txtlsehead1.Text), float.Parse(txtlsehead2.Text), float.Parse(txtlsehead3.Text), float.Parse(txtlsehead4.Text), float.Parse(txtlsehead5.Text), float.Parse(txtdhead1.Text), float.Parse(txtdhead2.Text), float.Parse(txtdhead3.Text), float.Parse(txtdhead4.Text), float.Parse(txtdhead5.Text), float.Parse(txtdhead6.Text), float.Parse(txtdhead7.Text), float.Parse(txtdhead8.Text), float.Parse(txtdhead9.Text), float.Parse(txtdhead10.Text), txtreasonforleft.Text, ddlpfregisteremp.Text, float.Parse(txtetotal0.Text), ddl_relation.Text, enteruserid, entrydatestmp, txt_pfbankname.Text, txt_pfifsccode.Text, txt_pfnomineename.Text, txt_pfnomineerelation.Text, txt_pfbdate.Text, txt_pan_new_num.Text, txt_advance_payment.Text, txtrefname1.Text, txtref1mob.Text, txtrefname2.Text, txtref2mob.Text, txt_Nationality.Text, txt_Identitymark.Text, ddl_Mother_Tongue.SelectedItem.Text, txt_Passport_No.Text, ddl_Visa_Country.SelectedItem.Text, txt_Driving_License_No.Text, txt_Mise.Text, txt_Place_Of_Birth.Text, txt_Language_Known.Text, txt_Area_Of_Expertise.Text, txt_Passport_Validity_Date.Text, txt_Visa_Validity_Date.Text, txt_Details_Of_Handicap.Text, txt_qualification_1.Text, txt_year_of_passing_1.Text, txt_qualification_2.Text, txt_year_of_passing_2.Text, txt_qualification_3.Text, txt_year_of_passing_3.Text, txt_qualification_4.Text, txt_year_of_passing_4.Text, txt_qualification_5.Text, txt_year_of_passing_5.Text, txt_key_skill_1.Text, txt_experience_in_months_1.Text, txt_key_skill_2.Text, txt_experience_in_months_2.Text, txt_key_skill_3.Text, txt_experience_in_months_3.Text, txt_key_skill_4.Text, txt_experience_in_months_4.Text, txt_key_skill_5.Text, txt_experience_in_months_5.Text, ddl_reporting_to.SelectedValue, txt_loandate.Text, txt_attendanceid.Text, ddl_intime.SelectedValue, txt_pfemployeepercentage.Text);// ddl_unitcode.SelectedItem.Text);         

                    result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,PF_SHEET,EARN_TOTAL,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,pfemployeepercentage,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,jobtype,MEDICLAIM_NO,ACCI_NO,Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,ICARD_ISSU_DATE,UNIFORM_ISSU_DATE,ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,CTC_PER_MONTH) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y'), '" + ddl_gender.Text + "', '" + ddl_unitcode.Text + "', '" + ddl_grade.Text + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + txt_maritalstaus.Text + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + txt_highestqualification.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "', '" + ddl_employmentstatus.SelectedItem.Text + "', '" + ddl_location.SelectedItem.Text + "', '" + float.Parse((txt_basicpay.Text)) + "', '" + txt_panno.Text + "', '" + ddl_pfdeductionflag.SelectedItem.Text + "', '" + txt_pfnumber.Text + "', '" + ddl_esicdeductionflag.SelectedItem.Text + "', '" + txt_esicnumber.Text + "', '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + ddl_dept.Text + "', '" + float.Parse(txtlhead1.Text) + "', '" + float.Parse(txtlhead2.Text) + "', '" + float.Parse(txtlhead3.Text) + "', '" + float.Parse(txtlhead4.Text) + "', '" + float.Parse(txtlhead5.Text) + "', '" + float.Parse(txtlhead6.Text) + "', '" + float.Parse(txtlhead7.Text) + "', '" + float.Parse(txtlhead8.Text) + "', '" + float.Parse(txtlhead9.Text) + "', '" + float.Parse(txtlhead10.Text) + "', '" + float.Parse(txtlhead11.Text) + "', '" + float.Parse(txtlhead12.Text) + "', '" + float.Parse(txtlhead13.Text) + "', '" + float.Parse(txtlhead14.Text) + "', '" + float.Parse(txtlhead15.Text) + "', '" + float.Parse(txtlsehead1.Text) + "', '" + float.Parse(txtlsehead2.Text) + "', '" + float.Parse(txtlsehead3.Text) + "', '" + float.Parse(txtlsehead4.Text) + "', '" + float.Parse(txtlsehead5.Text) + "', '" + float.Parse(txtdhead1.Text) + "', '" + float.Parse(txtdhead2.Text) + "', '" + float.Parse(txtdhead3.Text) + "', '" + float.Parse(txtdhead4.Text) + "', '" + float.Parse(txtdhead5.Text) + "', '" + float.Parse(txtdhead6.Text) + "', '" + float.Parse(txtdhead7.Text) + "', '" + float.Parse(txtdhead8.Text) + "', '" + float.Parse(txtdhead9.Text) + "', '" + float.Parse(txtdhead10.Text) + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "', '" + float.Parse(txtetotal0.Text) + "', '" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'), '" + txt_pan_new_num.Text + "', '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.Text + "', '" + ddl_intime.SelectedValue + "', '" + txt_pfemployeepercentage.Text + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "','" + ddl_Jobtype.SelectedValue.ToString() + "','" + txt_mediclaim.Text + "','" + txt_acci.Text + "','" + emp_name + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.SelectedValue + "','" + txt_bankholder.Text + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "', str_to_date('" + txt_icardissudate.Text + "','%d/%m/%Y'), str_to_date('" + txt_uniformissudate.Text + "','%d/%m/%Y'),'" + generateihmscode() + "','" + ddl_infitcode.SelectedValue + "','" + txt_emailid1.Text + "','" + txt_emailid2.Text + "','" + ddl_clientname.SelectedValue + "','" + txt_amount .Text+ "')");

                  //  result1 = d.operation("INSERT INTO pay_employee_master_log(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,PF_SHEET,EARN_TOTAL,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,pfemployeepercentage,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,jobtype,MEDICLAIM_NO,ACCI_NO,Login_Person,LastModifyDate,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y'), '" + ddl_gender.Text + "', '" + ddl_unitcode.Text + "', '" + ddl_grade.Text + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "','" + txt_maritalstaus.Text + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + txt_highestqualification.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "', '" + ddl_employmentstatus.SelectedItem.Text + "', '" + ddl_location.SelectedItem.Text + "', '" + float.Parse((txt_basicpay.Text)) + "', '" + txt_panno.Text + "', '" + ddl_pfdeductionflag.SelectedItem.Text + "', '" + txt_pfnumber.Text + "', '" + ddl_esicdeductionflag.SelectedItem.Text + "', '" + txt_esicnumber.Text + "', '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + ddl_dept.Text + "', '" + float.Parse(txtlhead1.Text) + "', '" + float.Parse(txtlhead2.Text) + "', '" + float.Parse(txtlhead3.Text) + "', '" + float.Parse(txtlhead4.Text) + "', '" + float.Parse(txtlhead5.Text) + "', '" + float.Parse(txtlhead6.Text) + "', '" + float.Parse(txtlhead7.Text) + "', '" + float.Parse(txtlhead8.Text) + "', '" + float.Parse(txtlhead9.Text) + "', '" + float.Parse(txtlhead10.Text) + "', '" + float.Parse(txtlhead11.Text) + "', '" + float.Parse(txtlhead12.Text) + "', '" + float.Parse(txtlhead13.Text) + "', '" + float.Parse(txtlhead14.Text) + "', '" + float.Parse(txtlhead15.Text) + "', '" + float.Parse(txtlsehead1.Text) + "', '" + float.Parse(txtlsehead2.Text) + "', '" + float.Parse(txtlsehead3.Text) + "', '" + float.Parse(txtlsehead4.Text) + "', '" + float.Parse(txtlsehead5.Text) + "', '" + float.Parse(txtdhead1.Text) + "', '" + float.Parse(txtdhead2.Text) + "', '" + float.Parse(txtdhead3.Text) + "', '" + float.Parse(txtdhead4.Text) + "', '" + float.Parse(txtdhead5.Text) + "', '" + float.Parse(txtdhead6.Text) + "', '" + float.Parse(txtdhead7.Text) + "', '" + float.Parse(txtdhead8.Text) + "', '" + float.Parse(txtdhead9.Text) + "', '" + float.Parse(txtdhead10.Text) + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "', '" + float.Parse(txtetotal0.Text) + "', '" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y'), '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "',str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'), '" + txt_pan_new_num.Text + "', '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.Text + "', '" + ddl_intime.SelectedValue + "', '" + txt_pfemployeepercentage.Text + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "','" + ddl_Jobtype.SelectedValue.ToString() + "','" + txt_mediclaim.Text + "','" + txt_acci.Text + "','" + emp_name + "','" + newdate + "','" + txt_kra.Text + "','" + ddl_location_city.SelectedValue + "','" + txt_bankholder.Text + "','" + txt_policestationname.Text + "','" + txt_fmobile1.Text + "','" + txt_fmobile2.Text + "','" + txt_fmobile3.Text + "','" + txt_fmobile4.Text + "','" + txt_fmobile5.Text + "','" + txt_fmobile6.Text + "','" + txt_fmobile7.Text + "')");
                    if (result > 0 )
                    {
                        //add_values();
                        send_email(Server.MapPath("~/User_Details.htm"),txt_email.Text);
                        employee_leave();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully !!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);
                        
                    }
                    if (txt_uniformissudate.Text != "")
                    {
                        DateTime now = Convert.ToDateTime(DateTime.Now);
                        DateTime FromYear = Convert.ToDateTime(txt_uniformissudate.Text);
                        DateTime ToYear = Convert.ToDateTime(now);
                        TimeSpan objTimeSpan = ToYear - FromYear;
                        double datediff = Convert.ToDouble(objTimeSpan.TotalDays);
                        if (datediff > 334)
                        {
                            result = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0',(select emp_code from pay_employee_master where emp_name='" + txt_eecode.Text + "'),'Message from " + Session["USERNAME"].ToString() + " - Uniform Expire','EmployeeMaster.aspx')");

                        }

                    }

                  //  result = d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification,page_name) values ('0',(select emp_code from pay_employee_master where emp_name='" + reproting_to_name + "'),'Message from " + Session["USERNAME"].ToString() + " - Leave Approval','Leave_form_management.aspx')");
                }
            }
            else
            {
                string enteruserid = Session["USERID"].ToString();
                string entrydatestmp = Session["system_curr_date"].ToString();

                //result = ebl2.EmployeeInsert(Session["comp_code"].ToString(), txt_eecode.Text, txt_eename.Text, txt_eefatharname.Text, txt_birthdate.Text, txt_joiningdate.Text, txt_confirmationdate.Text, txt_leftdate.Text, ddl_gender.Text, txt_maritalstaus.Text, txt_highestqualification.Text, ddl_religion.SelectedItem.Text, ddl_bloodgroup.Text, float.Parse((txt_weight.Text)), float.Parse((txt_height.Text)), txt_hobbies.Text, txt_presentaddress.Text, txt_presentcity.Text, ddl_state.SelectedItem.Text, txt_presentpincode.Text, txt_permanantaddress.Text, txt_permanantcity.Text, ddl_permstate.SelectedItem.Text, txt_permanantpincode.Text, txt_mobilenumber.Text, txt_residencecontactnumber.Text, ddl_employmentstatus.SelectedItem.Text, ddl_location.SelectedItem.Text, float.Parse((txt_basicpay.Text)), txt_panno.Text, ddl_pfdeductionflag.SelectedItem.Text, txt_pfnumber.Text, ddl_esicdeductionflag.SelectedItem.Text, txt_esicnumber.Text, ddl_ptaxdeductionflag.SelectedItem.Text, txt_ptaxnumber.Text, txt_employeeaccountnumber.Text, ddl_bankcode.Text, ddl_grade.Text, ddl_unitcode.Text, ddl_dept.Text, float.Parse(txtlhead1.Text), float.Parse(txtlhead2.Text), float.Parse(txtlhead3.Text), float.Parse(txtlhead4.Text), float.Parse(txtlhead5.Text), float.Parse(txtlhead6.Text), float.Parse(txtlhead7.Text), float.Parse(txtlhead8.Text), float.Parse(txtlhead9.Text), float.Parse(txtlhead10.Text), float.Parse(txtlhead11.Text), float.Parse(txtlhead12.Text), float.Parse(txtlhead13.Text), float.Parse(txtlhead14.Text), float.Parse(txtlhead15.Text), float.Parse(txtlsehead1.Text), float.Parse(txtlsehead2.Text), float.Parse(txtlsehead3.Text), float.Parse(txtlsehead4.Text), float.Parse(txtlsehead5.Text), float.Parse(txtdhead1.Text), float.Parse(txtdhead2.Text), float.Parse(txtdhead3.Text), float.Parse(txtdhead4.Text), float.Parse(txtdhead5.Text), float.Parse(txtdhead6.Text), float.Parse(txtdhead7.Text), float.Parse(txtdhead8.Text), float.Parse(txtdhead9.Text), float.Parse(txtdhead10.Text), txtreasonforleft.Text, ddlpfregisteremp.Text, float.Parse(txtetotal0.Text), ddl_relation.Text, enteruserid, entrydatestmp, txt_pfbankname.Text, txt_pfifsccode.Text, txt_pfnomineename.Text, txt_pfnomineerelation.Text, txt_pfbdate.Text, txt_pan_new_num.Text, txt_advance_payment.Text, txtrefname1.Text, txtref1mob.Text, txtrefname2.Text, txtref2mob.Text, txt_Nationality.Text, txt_Identitymark.Text, ddl_Mother_Tongue.SelectedItem.Text, txt_Passport_No.Text, ddl_Visa_Country.SelectedItem.Text, txt_Driving_License_No.Text, txt_Mise.Text, txt_Place_Of_Birth.Text, txt_Language_Known.Text, txt_Area_Of_Expertise.Text, txt_Passport_Validity_Date.Text, txt_Visa_Validity_Date.Text, txt_Details_Of_Handicap.Text, txt_qualification_1.Text, txt_year_of_passing_1.Text, txt_qualification_2.Text, txt_year_of_passing_2.Text, txt_qualification_3.Text, txt_year_of_passing_3.Text, txt_qualification_4.Text, txt_year_of_passing_4.Text, txt_qualification_5.Text, txt_year_of_passing_5.Text, txt_key_skill_1.Text, txt_experience_in_months_1.Text, txt_key_skill_2.Text, txt_experience_in_months_2.Text, txt_key_skill_3.Text, txt_experience_in_months_3.Text, txt_key_skill_4.Text, txt_experience_in_months_4.Text, txt_key_skill_5.Text, txt_experience_in_months_5.Text, ddl_reporting_to.SelectedValue, txt_loandate.Text, txt_attendanceid.Text, ddl_intime.SelectedValue, txt_pfemployeepercentage.Text);// ddl_unitcode.SelectedItem.Text);         

                result = d.operation("INSERT INTO pay_employee_master(comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,BIRTH_DATE,JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,PF_SHEET,EARN_TOTAL,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,pfemployeepercentage,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,jobtype,MEDICLAIM_NO,ACCI_NO,KRA,LOCATION_CITY,CLIENT_CODE) VALUES ('" + Session["comp_code"].ToString() + "', '" + txt_eecode.Text + "', '" + txt_eename.Text + "', '" + txt_eefatharname.Text + "', str_to_date('" + txt_birthdate.Text + "','%d/%m/%Y') ,str_to_date( '" + txt_joiningdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_confirmationdate.Text + "','%d/%m/%Y'), str_to_date('" + txt_leftdate.Text + "','%d/%m/%Y'), '" + ddl_gender.Text + "', '" + ddl_unitcode.Text + "', '" + ddl_grade.Text + "', '" + ddl_bankcode.Text + "', '" + txt_employeeaccountnumber.Text + "', '" + txt_presentaddress.Text + "', '" + txt_presentcity.SelectedValue + "', '" + ddl_state.SelectedValue + "', '" + txt_presentpincode.Text + "', '" + txt_permanantaddress.Text + "', '" + txt_permanantcity.SelectedValue + "', '" + ddl_permstate.SelectedValue + "', '" + txt_permanantpincode.Text + "', '" + txt_mobilenumber.Text + "', '" + txt_residencecontactnumber.Text + "', '" + txt_email.Text + "', '" + txt_maritalstaus.Text + "', '" + ddl_bloodgroup.Text + "', '" + txt_hobbies.Text + "', '" + txt_highestqualification.Text + "', '" + float.Parse((txt_weight.Text)) + "', '" + ddl_religion.SelectedItem.Text + "', '" + float.Parse((txt_height.Text)) + "', '" + ddl_employmentstatus.SelectedItem.Text + "', '" + ddl_location.SelectedItem.Text + "', '" + float.Parse((txt_basicpay.Text)) + "', '" + txt_panno.Text + "', '" + ddl_pfdeductionflag.SelectedItem.Text + "', '" + txt_pfnumber.Text + "', '" + ddl_esicdeductionflag.SelectedItem.Text + "', '" + txt_esicnumber.Text + "', '" + ddl_ptaxdeductionflag.SelectedItem.Text + "', '" + txt_ptaxnumber.Text + "', '" + ddl_dept.Text + "', '" + float.Parse(txtlhead1.Text) + "', '" + float.Parse(txtlhead2.Text) + "', '" + float.Parse(txtlhead3.Text) + "', '" + float.Parse(txtlhead4.Text) + "', '" + float.Parse(txtlhead5.Text) + "', '" + float.Parse(txtlhead6.Text) + "', '" + float.Parse(txtlhead7.Text) + "', '" + float.Parse(txtlhead8.Text) + "', '" + float.Parse(txtlhead9.Text) + "', '" + float.Parse(txtlhead10.Text) + "', '" + float.Parse(txtlhead11.Text) + "', '" + float.Parse(txtlhead12.Text) + "', '" + float.Parse(txtlhead13.Text) + "', '" + float.Parse(txtlhead14.Text) + "', '" + float.Parse(txtlhead15.Text) + "', '" + float.Parse(txtlsehead1.Text) + "', '" + float.Parse(txtlsehead2.Text) + "', '" + float.Parse(txtlsehead3.Text) + "', '" + float.Parse(txtlsehead4.Text) + "', '" + float.Parse(txtlsehead5.Text) + "', '" + float.Parse(txtdhead1.Text) + "', '" + float.Parse(txtdhead2.Text) + "', '" + float.Parse(txtdhead3.Text) + "', '" + float.Parse(txtdhead4.Text) + "', '" + float.Parse(txtdhead5.Text) + "', '" + float.Parse(txtdhead6.Text) + "', '" + float.Parse(txtdhead7.Text) + "', '" + float.Parse(txtdhead8.Text) + "', '" + float.Parse(txtdhead9.Text) + "', '" + float.Parse(txtdhead10.Text) + "', '" + txtreasonforleft.Text + "', '" + ddlpfregisteremp.Text + "', '" + float.Parse(txtetotal0.Text) + "', '" + ddl_relation.SelectedItem.Text + "', '" + enteruserid + "', str_to_date('" + entrydatestmp + "','%d/%m/%Y') , '" + txt_pfbankname.Text + "', '" + txt_pfifsccode.Text + "', '" + txt_pfnomineename.Text + "', '" + txt_pfnomineerelation.Text + "', str_to_date('" + txt_pfbdate.Text + "','%d/%m/%Y'), '" + txt_pan_new_num.Text + "', '" + txt_advance_payment.Text + "', '" + txtrefname1.Text + "', '" + txtref1mob.Text + "', '" + txtrefname2.Text + "', '" + txtref2mob.Text + "', '" + txt_Nationality.Text + "', '" + txt_Identitymark.Text + "', '" + ddl_Mother_Tongue.Text + "', '" + txt_Passport_No.Text + "', '" + ddl_Visa_Country.SelectedItem.Text + "', '" + txt_Driving_License_No.Text + "', '" + txt_Mise.Text + "', '" + txt_Place_Of_Birth.Text + "', '" + txt_Language_Known.Text + "', '" + txt_Area_Of_Expertise.Text + "', '" + txt_Passport_Validity_Date.Text + "', '" + txt_Visa_Validity_Date.Text + "', '" + txt_Details_Of_Handicap.Text + "', '" + txt_qualification_1.Text + "', '" + txt_year_of_passing_1.Text + "', '" + txt_qualification_2.Text + "', '" + txt_year_of_passing_2.Text + "', '" + txt_qualification_3.Text + "', '" + txt_year_of_passing_3.Text + "', '" + txt_qualification_4.Text + "', '" + txt_year_of_passing_4.Text + "', '" + txt_qualification_5.Text + "', '" + txt_year_of_passing_5.Text + "', '" + txt_key_skill_1.Text + "', '" + txt_experience_in_months_1.Text + "', '" + txt_key_skill_2.Text + "', '" + txt_experience_in_months_2.Text + "', '" + txt_key_skill_3.Text + "', '" + txt_experience_in_months_3.Text + "', '" + txt_key_skill_4.Text + "', '" + txt_experience_in_months_4.Text + "', '" + txt_key_skill_5.Text + "', '" + txt_experience_in_months_5.Text + "', '" + ddl_reporting_to.SelectedValue + "', '" + txt_loandate.Text + "', '" + txt_attendanceid.Text + "', '" + ddl_intime.SelectedValue + "', '" + txt_pfemployeepercentage.Text + "','" + txt_name1.Text + "','" + txt_relation1.Text + "','" + txt_dob1.Text + "','" + txt_pan1.Text + "','" + txt_adhaar1.Text + "','" + txt_name2.Text + "','" + txt_relation2.Text + "','" + txt_dob2.Text + "','" + txt_pan2.Text + "','" + txt_adhaar2.Text + "','" + txt_name3.Text + "','" + txt_relation3.Text + "','" + txt_dob3.Text + "','" + txt_pan3.Text + "','" + txt_adhaar3.Text + "','" + txt_name4.Text + "','" + txt_relation4.Text + "','" + txt_dob4.Text + "','" + txt_pan4.Text + "','" + txt_adhaar4.Text + "','" + txt_name5.Text + "','" + txt_relation5.Text + "','" + txt_dob5.Text + "','" + txt_pan5.Text + "','" + txt_adhaar5.Text + "','" + txt_name6.Text + "','" + txt_relation6.Text + "','" + txt_dob6.Text + "','" + txt_pan6.Text + "','" + txt_adhaar6.Text + "','" + txt_name7.Text + "','" + txt_relation7.Text + "','" + txt_dob7.Text + "','" + txt_pan7.Text + "','" + txt_adhaar7.Text + "','" + Numberchild.Text + "','" + ddl_Jobtype.SelectedValue.ToString() + "','" + txt_mediclaim.Text + "','" + txt_acci.Text + "','" + txt_kra.Text + "','" + ddl_location_city.SelectedValue + "','"+ddl_clientname.SelectedValue+"')");
                if (result > 0)
                {
                    //add_values();
                    send_email(Server.MapPath("~/User_Details.htm"), txt_email.Text);
                    employee_leave();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);
                   
                }
            }
        
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            txt_eecode.Text = "";
           
            newpanel.Visible = false;

        }
    }

    public void employee_leave()
    {
        int result = 0;
        result = d.operation("INSERT INTO pay_leave_emp_balance(comp_code,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) select '" + Session["comp_code"].ToString() + "', '" + ddl_unitcode.SelectedValue.ToString().Substring(0, 4) + "','" + txt_eecode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "',now(),leave_name,abbreviation,gender,max_no_of_leave,max_no_of_leave from pay_leave_master where comp_code='" + Session["comp_code"].ToString() + "' and gender in ('" + ddl_gender.SelectedValue + "','B') ");
        d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,ROLE,flag,create_user, create_date, password_changed_date,first_login,comp_code) VALUES('" + txt_eecode.Text + "','" + txt_eename.Text + "','" + GetSha256FromString(txt_birthdate.Text) + "','" + DropDownList1.SelectedItem.Text + "','A','" + Session["USERID"].ToString() + "',now(),now(),'0','" + Session["comp_code"].ToString() + "')");


    }
     protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
   
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblunitname.Text = ddl_unitcode.Text;
        newpanel.Visible = true;
    }       
    
    protected void ddl_bankcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblbankname.Text = ddl_bankcode.Text.ToString();
    }
    
   


    public int getEmployee(string EmployeeCode)
    {
        int returnEmployee = 0;
        d.con1.Open();
        try
        {
            string l_EMP_CODE = EmployeeCode.ToString();

            // MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),LEFT_DATE,GENDER,UNIT_CODE,DESIGNATION_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,ADHARNO,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,AUTOATTENDANCE_CODE,PF_SHEET,EARN_TOTAL,FATHER_RELATION,DATE_STMP,ENTER_USER_ID,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,KYC_CONFIRM,REPORTING_TO,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,emistartdate,attendance_id,in_time,pfemployeepercentage FROM pay_employee_master WHERE EMP_CODE='" + l_EMP_CODE + "'", d.con1);

            MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,PF_SHEET,EARN_TOTAL,FATHER_RELATION,ENTER_USER_ID,DATE_STMP,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,date_format(PF_NOMINEE_BDATE,'%d/%m/%Y'),EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,pfemployeepercentage,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,jobtype,MEDICLAIM_NO,ACCI_NO,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7,date_format(ICARD_ISSU_DATE,'%d/%m/%Y'),date_format(UNIFORM_ISSU_DATE,'%d/%m/%Y'),ihmscode,NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,CTC_PER_MONTH FROM pay_employee_master WHERE EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                returnEmployee = 1;
                txt_eecode.Text = dr.GetValue(1).ToString();
                txt_eename.Text = dr.GetValue(2).ToString();
                txt_eefatharname.Text = dr.GetValue(3).ToString();
                string bdate = dr.GetValue(4).ToString();
                if (bdate == "")
                {
                    txt_birthdate.Text = dr.GetValue(4).ToString();
                }
                else
                {
                    txt_birthdate.Text = bdate.ToString();
                }

                string joindate = dr.GetValue(5).ToString();
                if (joindate == "")
                {
                    txt_joiningdate.Text = dr.GetValue(5).ToString();
                }
                else
                {
                    // txt_joiningdate.Text = DateTime.Parse(joindate).ToString("dd/MM/yy");
                    txt_joiningdate.Text = joindate.ToString();
                }

                string confrmdate = dr.GetValue(6).ToString();
                if (confrmdate == "")
                {
                    txt_confirmationdate.Text = dr.GetValue(6).ToString();
                }
                else
                {
                    txt_confirmationdate.Text = confrmdate.ToString();
                }

                string lftdate = dr.GetValue(7).ToString();
                if (lftdate == "")
                {
                    txt_leftdate.Text = dr.GetValue(7).ToString();
                }
                else
                {
                    txt_leftdate.Text = lftdate.ToString();
                }
                ddl_gender.Text = dr.GetValue(8).ToString();
             //   ddl_unitcode.SelectedValue = dr.GetValue(9).ToString();
             


                ddl_grade.SelectedValue = dr.GetValue(10).ToString();
                ddl_grade_SelectedIndexChanged(null,null);
                ddl_bankcode.Text = dr.GetValue(11).ToString();
                txt_employeeaccountnumber.Text = dr.GetValue(12).ToString();
                txt_presentaddress.Text = dr.GetValue(13).ToString();

                ddl_state.SelectedValue = dr.GetValue(15).ToString();
                get_city_list(null, null);
                txt_presentcity.SelectedValue = dr.GetValue(14).ToString();

                txt_presentpincode.Text = dr.GetValue(16).ToString();
                txt_permanantaddress.Text = dr.GetValue(17).ToString();

                ddl_permstate.SelectedValue = dr.GetValue(19).ToString();
                get_city_list_shipping(null, null);
                txt_permanantcity.SelectedValue = dr.GetValue(18).ToString();

                txt_permanantpincode.Text = dr.GetValue(20).ToString();
                txt_mobilenumber.Text = dr.GetValue(21).ToString();
                txt_residencecontactnumber.Text = dr.GetValue(22).ToString();
                txt_email.Text = dr.GetValue(23).ToString();
                txt_maritalstaus.Text = dr.GetValue(24).ToString();
                ddl_bloodgroup.Text = dr.GetValue(25).ToString();
                txt_hobbies.Text = dr.GetValue(26).ToString();
                txt_highestqualification.Text = dr.GetValue(27).ToString();
                txt_weight.Text = dr.GetValue(28).ToString();
                ddl_religion.Text = dr.GetValue(29).ToString();
                txt_height.Text = dr.GetValue(30).ToString();

                /////-------------------------------------------------------------

                ddl_employmentstatus.Text = dr.GetValue(31).ToString();

                ddl_location.SelectedValue = dr.GetValue(32).ToString();
                ddl_location_SelectedIndexChanged(null, null);
                ddl_location_city.SelectedValue = dr.GetValue(167).ToString();

                txt_basicpay.Text = dr.GetValue(33).ToString();
                txt_panno.Text = dr.GetValue(34).ToString();
                ddl_pfdeductionflag.Text = dr.GetValue(35).ToString();
                txt_pfnumber.Text = dr.GetValue(36).ToString();

                ddl_esicdeductionflag.Text = dr.GetValue(37).ToString();
                txt_esicnumber.Text = dr.GetValue(38).ToString();
                ddl_ptaxdeductionflag.SelectedItem.Text = dr.GetValue(39).ToString();
                txt_ptaxnumber.Text = dr.GetValue(40).ToString();//adhaar no;
                ddl_dept.SelectedItem.Text = dr.GetValue(41).ToString();

                /////----------------------------------------------------------

                txtlhead1.Text = dr.GetValue(42).ToString();
                txtlhead2.Text = dr.GetValue(43).ToString();
                txtlhead3.Text = dr.GetValue(44).ToString();
                txtlhead4.Text = dr.GetValue(45).ToString();
                txtlhead5.Text = dr.GetValue(46).ToString();
                txtlhead6.Text = dr.GetValue(47).ToString();
                txtlhead7.Text = dr.GetValue(48).ToString();
                txtlhead8.Text = dr.GetValue(49).ToString();
                txtlhead9.Text = dr.GetValue(50).ToString();
                txtlhead10.Text = dr.GetValue(51).ToString();
                txtlhead11.Text = dr.GetValue(52).ToString();
                txtlhead12.Text = dr.GetValue(53).ToString();
                txtlhead13.Text = dr.GetValue(54).ToString();
                txtlhead14.Text = dr.GetValue(55).ToString();
                txtlhead15.Text = dr.GetValue(56).ToString();
                txtlsehead1.Text = dr.GetValue(57).ToString();
                txtlsehead2.Text = dr.GetValue(58).ToString();
                txtlsehead3.Text = dr.GetValue(59).ToString();
                txtlsehead4.Text = dr.GetValue(60).ToString();
                txtlsehead5.Text = dr.GetValue(61).ToString();
                txtdhead1.Text = dr.GetValue(62).ToString();
                txtdhead2.Text = dr.GetValue(63).ToString();
                txtdhead3.Text = dr.GetValue(64).ToString();
                txtdhead4.Text = dr.GetValue(65).ToString();
                txtdhead5.Text = dr.GetValue(66).ToString();
                txtdhead6.Text = dr.GetValue(67).ToString();
                txtdhead7.Text = dr.GetValue(68).ToString();
                txtdhead8.Text = dr.GetValue(69).ToString();
                txtdhead9.Text = dr.GetValue(70).ToString();
                txtdhead10.Text = dr.GetValue(71).ToString();

                txtreasonforleft.Text = dr.GetValue(72).ToString();
                ddlpfregisteremp.Text = dr.GetValue(73).ToString();
                txtetotal0.Text = dr.GetValue(74).ToString();
                ddl_relation.SelectedItem.Text = dr.GetValue(75).ToString();
                // txt_eecode.Text = dr.GetValue(76).ToString();

                txt_pfbankname.Text = dr.GetValue(78).ToString();
                txt_pfifsccode.Text = dr.GetValue(79).ToString();
                txt_pfnomineename.Text = dr.GetValue(80).ToString();
                txt_pfnomineerelation.Text = dr.GetValue(81).ToString();

                string pfbdate = dr.GetValue(82).ToString();
                if (pfbdate == "")
                {
                    txt_pfbdate.Text = dr.GetValue(82).ToString();
                }
                else
                {

                    txt_pfbdate.Text = pfbdate.ToString();
                }

                txt_pan_new_num.Text = dr.GetValue(83).ToString();
                txt_advance_payment.Text = dr.GetValue(84).ToString();

                txtrefname1.Text = dr.GetValue(85).ToString();
                txtref1mob.Text = dr.GetValue(86).ToString();
                txtrefname2.Text = dr.GetValue(87).ToString();
                txtref2mob.Text = dr.GetValue(88).ToString();

                txt_Nationality.Text = dr.GetValue(89).ToString();
                txt_Identitymark.Text = dr.GetValue(90).ToString();
                ddl_Mother_Tongue.Text = dr.GetValue(91).ToString();
                txt_Passport_No.Text = dr.GetValue(92).ToString();
                ddl_Visa_Country.SelectedItem.Text = dr.GetValue(93).ToString();
                txt_Driving_License_No.Text = dr.GetValue(94).ToString();
                txt_Mise.Text = dr.GetValue(95).ToString();
                txt_Place_Of_Birth.Text = dr.GetValue(96).ToString();
                txt_Language_Known.Text = dr.GetValue(97).ToString();
                txt_Area_Of_Expertise.Text = dr.GetValue(98).ToString();
                txt_Passport_Validity_Date.Text = dr.GetValue(99).ToString();
                txt_Visa_Validity_Date.Text = dr.GetValue(100).ToString();
                txt_Details_Of_Handicap.Text = dr.GetValue(101).ToString();


                txt_qualification_1.Text = dr.GetValue(102).ToString();
                txt_year_of_passing_1.Text = dr.GetValue(103).ToString();
                txt_qualification_2.Text = dr.GetValue(104).ToString();
                txt_year_of_passing_2.Text = dr.GetValue(105).ToString();
                txt_qualification_3.Text = dr.GetValue(106).ToString();
                txt_year_of_passing_3.Text = dr.GetValue(107).ToString();
                txt_qualification_4.Text = dr.GetValue(108).ToString();
                txt_year_of_passing_4.Text = dr.GetValue(109).ToString();
                txt_qualification_5.Text = dr.GetValue(110).ToString();
                txt_year_of_passing_5.Text = dr.GetValue(111).ToString();
                txt_key_skill_1.Text = dr.GetValue(112).ToString();
                txt_experience_in_months_1.Text = dr.GetValue(113).ToString();
                txt_key_skill_2.Text = dr.GetValue(114).ToString();
                txt_experience_in_months_2.Text = dr.GetValue(115).ToString();
                txt_key_skill_3.Text = dr.GetValue(116).ToString();
                txt_experience_in_months_3.Text = dr.GetValue(117).ToString();
                txt_key_skill_4.Text = dr.GetValue(118).ToString();
                txt_experience_in_months_4.Text = dr.GetValue(119).ToString();
                txt_key_skill_5.Text = dr.GetValue(120).ToString();
                txt_experience_in_months_5.Text = dr.GetValue(121).ToString();

                ddl_grade_SelectedIndexChanged(null, null);
                if (ddl_reporting_to.Items.Count > 0)
                {
                    ddl_reporting_to.SelectedValue = dr.GetValue(122).ToString();
                }
              //  else
                //{
                //    ddl_reporting_to.SelectedValue = dr.GetValue(122).ToString();
                //}
                txt_loandate.Text = dr.GetValue(123).ToString();
                txt_attendanceid.Text = dr.GetValue(124).ToString();
                ddl_intime.SelectedValue = dr.GetValue(125).ToString();
                txt_pfemployeepercentage.Text = dr.GetValue(126).ToString();
                //---Family details -----
                txt_name1.Text = dr.GetValue(127).ToString();
                txt_relation1.Text = dr.GetValue(128).ToString();
                txt_dob1.Text = dr.GetValue(129).ToString();
                txt_pan1.Text = dr.GetValue(130).ToString();
                txt_adhaar1.Text = dr.GetValue(131).ToString();
                txt_name2.Text = dr.GetValue(132).ToString();
                txt_relation2.Text = dr.GetValue(133).ToString();
                txt_dob2.Text = dr.GetValue(134).ToString();
                txt_pan2.Text = dr.GetValue(135).ToString();
                txt_adhaar2.Text = dr.GetValue(136).ToString();
                txt_name3.Text = dr.GetValue(137).ToString();
                txt_relation3.Text = dr.GetValue(138).ToString();
                txt_dob3.Text = dr.GetValue(139).ToString();
                txt_pan3.Text = dr.GetValue(140).ToString();
                txt_adhaar3.Text = dr.GetValue(141).ToString();
                txt_name4.Text = dr.GetValue(142).ToString();
                txt_relation4.Text = dr.GetValue(143).ToString();
                txt_dob4.Text = dr.GetValue(144).ToString();
                txt_pan4.Text = dr.GetValue(145).ToString();
                txt_adhaar4.Text = dr.GetValue(146).ToString();
                txt_name5.Text = dr.GetValue(147).ToString();
                txt_relation5.Text = dr.GetValue(148).ToString();
                txt_dob5.Text = dr.GetValue(149).ToString();
                txt_pan5.Text = dr.GetValue(150).ToString();
                txt_adhaar5.Text = dr.GetValue(151).ToString();
                txt_name6.Text = dr.GetValue(152).ToString();
                txt_relation6.Text = dr.GetValue(153).ToString();
                txt_dob6.Text = dr.GetValue(154).ToString();
                txt_pan6.Text = dr.GetValue(155).ToString();
                txt_adhaar6.Text = dr.GetValue(156).ToString();
                txt_name7.Text = dr.GetValue(157).ToString();
                txt_relation7.Text = dr.GetValue(158).ToString();
                txt_dob7.Text = dr.GetValue(159).ToString();
                txt_pan7.Text = dr.GetValue(160).ToString();
                txt_adhaar7.Text = dr.GetValue(161).ToString();
                Numberchild.Text = dr.GetValue(162).ToString();
                ddl_Jobtype.SelectedValue = dr[163].ToString();

                txt_mediclaim.Text = dr.GetValue(164).ToString();
                txt_acci.Text = dr.GetValue(165).ToString();
                txt_kra.Text = dr.GetValue(166).ToString();

                txt_bankholder.Text = dr.GetValue(168).ToString();
                txt_policestationname.Text = dr.GetValue(169).ToString();
                txt_fmobile1.Text = dr.GetValue(170).ToString();
                txt_fmobile2.Text = dr.GetValue(171).ToString();
                txt_fmobile3.Text = dr.GetValue(172).ToString();
                txt_fmobile4.Text = dr.GetValue(173).ToString();
                txt_fmobile5.Text = dr.GetValue(174).ToString();
                txt_fmobile6.Text = dr.GetValue(175).ToString();
                txt_fmobile7.Text = dr.GetValue(176).ToString();

                string icarddate = dr.GetValue(177).ToString();
                if (icarddate == "")
                {
                    txt_icardissudate.Text = dr.GetValue(177).ToString();
                }
                else
                {
                    txt_icardissudate.Text = icarddate.ToString();
                }

                string uniformdate = dr.GetValue(178).ToString();
                if (uniformdate == "")
                {
                    txt_uniformissudate.Text = dr.GetValue(178).ToString();
                }
                else
                {
                    txt_uniformissudate.Text = uniformdate.ToString();
                }
                txt_ihmscode.Text = dr.GetValue(179).ToString();
                ddl_infitcode.SelectedValue = dr.GetValue(180).ToString();
                txt_emailid1.Text = dr.GetValue(181).ToString();
                txt_emailid2.Text = dr.GetValue(182).ToString();
                txt_amount.Text = dr.GetValue(184).ToString();

                ddl_clientname.SelectedItem.Text = dr.GetValue(183).ToString();
                ddl_clientname_SelectedIndexChanged(null, null);
                if (ddl_unitcode.Items.Count > 0)
                {
                    ddl_unitcode.Text = dr.GetValue(9).ToString();
                }
              

            }
            dr.Close();
            cmd.Dispose();

            //Role of user
            cmd = new MySqlCommand("select role from pay_user_master where login_id ='" + l_EMP_CODE + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                DropDownList1.SelectedValue = dr.GetValue(0).ToString();
            }

            dr.Close();
            cmd.Dispose();

            MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_PHOTO,EMP_ADHAR_PAN,EMP_BANK_STATEMENT,EMP_BIODATA,EMP_PASSPORT,EMP_DRIVING_LISCENCE,EMP_10TH_MARKSHEET,EMP_12TH_MARKSHEET,EMP_DIPLOMA_CERTIFICATE,EMP_DEGREE_CERTIFICATE,EMP_POST_GRADUATION_CERTIFICATE,EMP_EDUCATION_CERTIFICATE,POLICE_VERIFICATION_DOC,FormNo_2,FormNo_11 FROM pay_images_master WHERE EMP_CODE='" + l_EMP_CODE + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                //////--------------------------------------EMP_PHOTO-----------------------------------------------------

                if (!DBNull.Value.Equals(dr.GetValue(0)))
                {

                    Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();
                }
                else
                {
                    Image4.ImageUrl = null;
                }

                //////--------------------------------EMP_ADHAR_PAN----------------------------------------------------------

                if (!DBNull.Value.Equals(dr.GetValue(1)))
                {

                    Image1.ImageUrl = "~/EMP_Images/" + dr.GetValue(1).ToString();
                }
                else
                {
                    Image1.ImageUrl = null;
                }
                //////----------------------------------EMP_BANK_STATEMENT---------------------------------------------------

                if (!DBNull.Value.Equals(dr.GetValue(2)))
                {

                    Image2.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
                }
                else
                {
                    Image2.ImageUrl = null;
                }

                //////-----------------------------EMP_BIODATA--------------------------------------------------------

                if (!DBNull.Value.Equals(dr.GetValue(3)))
                {
                    Image3.ImageUrl = "~/EMP_Images/" + dr.GetValue(3).ToString();
                }
                else
                {
                    Image3.ImageUrl = null;
                }

                //////------------------------------EMP_PASSPORT-------------------------------------------------------

                if (!DBNull.Value.Equals(dr.GetValue(4)))
                {
                    Image5.ImageUrl = "~/EMP_Images/" + dr.GetValue(4).ToString();
                }
                else
                {
                    Image5.ImageUrl = null;
                }
                //////------------------------------------EMP_DRIVING_LISCENCE-------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(5)))
                {
                    Image6.ImageUrl = "~/EMP_Images/" + dr.GetValue(5).ToString();
                }
                else
                {
                    Image6.ImageUrl = null;
                }
                //////------------------------------------EMP_10TH_MARKSHEET-------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(6)))
                {
                    Image7.ImageUrl = "~/EMP_Images/" + dr.GetValue(6).ToString();
                }
                else
                {
                    Image7.ImageUrl = null;
                }
                //////------------------------------------EMP_12TH_MARKSHEET-------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(7)))
                {
                    Image8.ImageUrl = "~/EMP_Images/" + dr.GetValue(7).ToString();
                }
                else
                {
                    Image8.ImageUrl = null;
                }
                //////-------------------------------------EMP_DIPLOMA_CERTIFICATE------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(8)))
                {
                    Image9.ImageUrl = "~/EMP_Images/" + dr.GetValue(8).ToString();
                }
                else
                {
                    Image9.ImageUrl = null;
                }
                //////-------------------------------------EMP_DEGREE_CERTIFICATE------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(9)))
                {
                    Image10.ImageUrl = "~/EMP_Images/" + dr.GetValue(9).ToString();
                }
                else
                {
                    Image10.ImageUrl = null;
                }
                //////-------------------------------------EMP_POST_GRADUATION_CERTIFICATE------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(10)))
                {
                    Image11.ImageUrl = "~/EMP_Images/" + dr.GetValue(10).ToString();
                }
                else
                {
                    Image11.ImageUrl = null;
                }
                //////-------------------------------------EMP_EDUCATION_CERTIFICATE------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(11)))
                {
                    Image12.ImageUrl = "~/EMP_Images/" + dr.GetValue(11).ToString();
                }
                else
                {
                    Image12.ImageUrl = null;
                }
                //////-------------------------------------------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(12)))
                {
                    Image14.ImageUrl = "~/EMP_Images/" + dr.GetValue(12).ToString();
                }
                else
                {
                    Image14.ImageUrl = null;
                }
                //////-------------------------------------------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(13)))
                {
                    Image15.ImageUrl = "~/EMP_Images/" + dr.GetValue(13).ToString();
                }
                else
                {
                    Image15.ImageUrl = null;
                }

                //////-------------------------------------------------------------------------------------
                if (!DBNull.Value.Equals(dr.GetValue(14)))
                {
                    Image16.ImageUrl = "~/EMP_Images/" + dr.GetValue(14).ToString();
                }
                else
                {
                    Image16.ImageUrl = null;
                }


            }
            dr.Close();
            cmd1.Dispose();
        }
        catch (Exception ex)
        {  throw ex;
           returnEmployee = 0; 
        }
        finally
        {
            d.con1.Close();
            ddl_grade_SelectedIndexChanged(null, null);
           
        }
        
        return returnEmployee;
    }

    protected void ddl_grade_SelectedIndexChanged(object sender, EventArgs e)
    {
        d1.con1.Open();
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select emp_code, emp_name, grade_code from pay_employee_master where grade_code in (select reporting_to from pay_grade_master where grade_code = '" + ddl_grade.SelectedValue + "')  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
            adp.Fill(ds);
            ddl_reporting_to.DataSource = ds.Tables[0];
            ddl_reporting_to.DataBind();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
            newpanel.Visible = true;

            MySqlCommand cmd = new MySqlCommand("Select EMP_CODE from pay_employee_master where comp_code='" + Session["comp_code"] + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string abc = dr[0].ToString();
                string xyz = Convert.ToString(txt_eecode.Text);
                if (abc == xyz)
                {
                   


                    break;

                }
                else
                {

                  
                }
            }

            dr.Close();
            d.con.Close();

        }
    }
    public void text_Clear()
    {
        txt_eename.Text = "";
        txt_eefatharname.Text = "";
        txt_pfnumber.Text = "A";
        txt_esicnumber.Text = "A";
        txtreasonforleft.Text = "";
       // txtautoattendancecode.Text = "";

        ddl_relation.SelectedIndex = 0;
        txt_birthdate.Text = "";
        ddl_gender.SelectedIndex = 0;
      
        ddl_employmentstatus.SelectedIndex = 0;
     //   ddl_unitcode.SelectedIndex = 0;
        ddl_dept.SelectedIndex = 0;
        ddl_grade.SelectedIndex = 0;
        ddl_grade_SelectedIndexChanged(null, null);
        txt_presentaddress.Text = "";
        txt_presentcity.SelectedIndex = 0;
        ddl_state.SelectedIndex = 0;
        txt_presentpincode.Text = "";
        txtrefname1.Text = "";
        txtrefname2.Text = "";

        txt_permanantaddress.Text = "";
        txt_permanantcity.SelectedIndex = 0;
        ddl_permstate.SelectedIndex = 0;
        txt_permanantpincode.Text = "";
        txtref1mob.Text = "";
        txtref2mob.Text = "";
        txt_mobilenumber.Text = "";
        txt_residencecontactnumber.Text = "";
        txt_confirmationdate.Text = "";
        txt_joiningdate.Text = "";
        txt_advance_payment.Text = "";
        txt_pan_new_num.Text = "";
        ddl_location.SelectedIndex = 0;
        ddl_location_city.SelectedIndex = 0;
        txt_panno.Text = "";
        ddl_pfdeductionflag.SelectedIndex = 1;
        ddl_esicdeductionflag.SelectedIndex = 1;
        ddl_ptaxdeductionflag.SelectedIndex = 1;
        txt_ptaxnumber.Text = "";
        txt_leftdate.Text = "";
        txtreasonforleft.Text = "";
        ddlpfregisteremp.SelectedIndex = 1;
        txt_pfbankname.Text = "";
        txt_pfnomineename.Text = "";
        txt_pfifsccode.Text = "";
        txt_pfnomineerelation.Text = "";
        txt_employeeaccountnumber.Text = "";
        txt_pfbdate.Text = "";
        //ddl_bankcode.SelectedIndex =;
        txt_maritalstaus.Text = "";
        ddl_bloodgroup.SelectedIndex = 0;
        txt_hobbies.Text = "";
        txt_highestqualification.Text = "";
        txt_weight.Text = "0";
        ddl_religion.SelectedIndex = 0;
        txt_icardissudate.Text = "";
        txt_uniformissudate.Text = "";
        txt_height.Text = "0";
        if (ddl_reporting_to.SelectedIndex == -1)
        {
            //ddl_reporting_to.SelectedIndex = 0;
        }
        else
        {
            ddl_reporting_to.SelectedIndex = 0;
        }
        //txt_clopeningbalance.Text = "";
        //txt_plopningbalance.Text = "";
        //txt_slopeningbalance.Text = "";
        //txt_clallocated.Text = "";
        //txt_slallocated.Text = "";
        //txt_plallocated.Text = "";
        //txt_cltaken.Text = "";
        //txt_sltaken.Text = "";
        //txt_pltaken.Text = "";
        //txt_clbalance.Text = "";
        //txt_slbalance.Text = "";
        //txt_plbalance.Text = "";
        txtlhead1.Text = "0";
        txtlhead2.Text = "0";
        txtlhead3.Text = "0";
        txtlhead4.Text = "0";
        txtlhead5.Text = "0";
        txtlhead6.Text = "0";
        txtlhead7.Text = "0";
        txtlhead8.Text = "0";
        txtlhead9.Text = "0";
        txtlhead10.Text = "0";
        txtlhead11.Text = "0";
        txtlhead12.Text = "0";
        txtlhead13.Text = "0";
        txtlhead14.Text = "0";
        txtlhead15.Text = "0";
        txt_basicpay.Text = "0";
        txtetotal0.Text = "0";
        chk_updaterating.Checked = false;
        txtlsehead1.Text = "0";
        txtlsehead2.Text = "0";
        txtlsehead3.Text = "0";
        txtlsehead4.Text = "0";
        txtlsehead5.Text = "0";
        txtdhead1.Text = "0";
        txtdhead2.Text = "0";
        txtdhead3.Text = "0";
        txtdhead4.Text = "0";
        txtdhead5.Text = "0";
        txtdhead6.Text = "0";
        txtdhead7.Text = "0";
        txtdhead8.Text = "0";
        txtdhead9.Text = "0";
        txtdhead10.Text = "0";
        txtsearchempid.Text = "";
        txt_email.Text = "";

        txt_mediclaim.Text = "0";
        txt_acci.Text = "0";

        txt_Nationality.Text = "";
        txt_Identitymark.Text = "";
        ddl_Mother_Tongue.Text = "";
        txt_Passport_No.Text = "";
        ddl_Visa_Country.SelectedIndex = 0;
        txt_Driving_License_No.Text = "";
        txt_Mise.Text = "";
        txt_Place_Of_Birth.Text = "";
        txt_Language_Known.Text = "";
        txt_Area_Of_Expertise.Text = "";
        txt_Passport_Validity_Date.Text = "";
        txt_Visa_Validity_Date.Text = "";
        txt_Details_Of_Handicap.Text = "";
        txt_qualification_1.Text= "";
       txt_year_of_passing_1.Text= "";
       txt_qualification_2.Text= "";
       txt_year_of_passing_2.Text= "";
       txt_qualification_3.Text= "";
       txt_year_of_passing_3.Text= "";
       txt_qualification_4.Text= "";
       txt_year_of_passing_4.Text= ""; 
       txt_qualification_5.Text= ""; 
       txt_year_of_passing_5.Text= "";
        txt_key_skill_1.Text= "";
        txt_experience_in_months_1.Text= "";
        txt_key_skill_2.Text= "";
        txt_experience_in_months_2.Text= "";
        txt_key_skill_3.Text= "";
        txt_experience_in_months_3.Text= "";
        txt_key_skill_4.Text= "";
        txt_experience_in_months_4.Text= "";
        txt_key_skill_5.Text= "";
        txt_experience_in_months_5.Text = "";
        //--family Details ---
        txt_name1.Text = "";
        txt_relation1.Text = "Father";
        txt_dob1.Text = "";
        txt_pan1.Text = "";
        txt_adhaar1.Text = "";
        txt_name2.Text = "";
        txt_relation2.Text = "Mother";
        txt_dob2.Text = "";
        txt_pan2.Text = "";
        txt_adhaar2.Text = "";
        txt_name3.Text = "";
        txt_relation3.Text = "Wife";
        txt_dob3.Text = "";
        txt_pan3.Text = "";
        txt_adhaar3.Text = "";
        txt_name4.Text = "";
        txt_relation4.Text = "Child";
        txt_dob4.Text = "";
        txt_pan4.Text = "";
        txt_adhaar4.Text = "";
        txt_name5.Text = "";
        txt_relation5.Text = "Child";
        txt_dob5.Text = "";
        txt_pan5.Text = "";
        txt_adhaar5.Text = "";
        txt_name6.Text = "";
        txt_relation6.Text = "Child";
        txt_dob6.Text = "";
        txt_pan6.Text = "";
        txt_adhaar6.Text = "";
        txt_name7.Text = "";
        txt_relation7.Text = "Child";
        txt_dob7.Text = "";
        txt_pan7.Text = "";
        txt_adhaar7.Text = "";
        Numberchild.Text = "0";
        ddl_Jobtype.SelectedIndex = 0;

        Image4.ImageUrl = "~/Images/placeholder.png";
        Image1.ImageUrl = "~/Images/pan.jpg";
        Image10.ImageUrl = "~/Images/certificate.jpg";
        Image11.ImageUrl = "~/Images/certificate.jpg";
        Image12.ImageUrl = "~/Images/certificate.jpg";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image3.ImageUrl = "~/Images/Biodata.png";
        Image5.ImageUrl = "~/Images/Passport.jpg";
        Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        Image7.ImageUrl = "~/Images/marksheet.jpg";
        Image8.ImageUrl = "~/Images/marksheet.jpg";
        Image9.ImageUrl = "~/Images/certificate.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
        txt_attendanceid.Text = "";
    }

    public void photo_image_clear()
    {

        Image4.ImageUrl = "~/Images/placeholder.png";
        Image1.ImageUrl = "~/Images/pan.jpg";
        Image10.ImageUrl = "~/Images/certificate.jpg";
        Image11.ImageUrl = "~/Images/certificate.jpg";
        Image12.ImageUrl = "~/Images/certificate.jpg";
        Image2.ImageUrl = "~/Images/passbook.jpg";
        Image3.ImageUrl = "~/Images/Biodata.png";
        Image5.ImageUrl = "~/Images/Passport.jpg";
        Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        Image7.ImageUrl = "~/Images/marksheet.jpg";
        Image8.ImageUrl = "~/Images/marksheet.jpg";
        Image9.ImageUrl = "~/Images/certificate.jpg";
        Image14.ImageUrl = "~/Images/certificate.jpg";
    }

    
    
  

  

  
   


  
    protected void get_city_list_shipping(object sender, EventArgs e)
    {

        txt_permanantcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_permstate.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            txt_permanantcity.Items.Insert(0, new ListItem("Select","Select"));
            newpanel.Visible = true;
        }
    }
    protected void get_city_list(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
        txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_presentcity.Items.Add(dr_item1[0].ToString());
                txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

            txt_presentcity.Items.Insert(0, new ListItem("Select", "Select"));
            txt_permanantcity.Items.Insert(0,new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }

    }
    protected void calculate1(object sender, EventArgs e)
    {   

        d.con1.Open();
        try
        {
            double amount = Convert.ToDouble(txt_amount.Text);
            double total = amount / 12;

            MySqlCommand cmdheads = new MySqlCommand("SELECT * FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader drheads = cmdheads.ExecuteReader();
            if (drheads.Read())
            {
                txtlhead1.Text = drheads.GetValue(68).ToString();//lbledithead1.Text =drheads.GetValue(24).ToString();
                txtlhead2.Text = drheads.GetValue(69).ToString(); //lbledithead2.Text =drheads.GetValue(25).ToString();
                txtlhead3.Text = drheads.GetValue(70).ToString(); // lbledithead3.Text = drheads.GetValue(26).ToString();
                txtlhead4.Text = drheads.GetValue(71).ToString();//  lbledithead4.Text =drheads.GetValue(27).ToString();
                txtlhead5.Text = drheads.GetValue(72).ToString();//lbledithead5.Text =drheads.GetValue(28).ToString();
                txtlhead6.Text = drheads.GetValue(73).ToString();//lbledithead6.Text = drheads.GetValue(29).ToString();
                txtlhead7.Text = drheads.GetValue(74).ToString();// lbledithead7.Text =drheads.GetValue(30).ToString();
                txtlhead8.Text = drheads.GetValue(59).ToString();// lbledithead8.Text =drheads.GetValue(31).ToString();
                txtlhead9.Text = drheads.GetValue(60).ToString();// lbledithead9.Text =drheads.GetValue(32).ToString();
                txtlhead10.Text = drheads.GetValue(61).ToString();//lbledithead10.Text =drheads.GetValue(33).ToString();
                txtlhead11.Text = drheads.GetValue(62).ToString();// lbledithead11.Text = drheads.GetValue(34).ToString();
                txtlhead12.Text = drheads.GetValue(63).ToString();// lbledithead12.Text =drheads.GetValue(35).ToString();
                txtlhead13.Text = drheads.GetValue(64).ToString();//  lbledithead13.Text =drheads.GetValue(36).ToString();
                txtlhead14.Text = drheads.GetValue(65).ToString();// lbledithead14.Text = drheads.GetValue(37).ToString();
                txtlhead15.Text = drheads.GetValue(66).ToString();//  lbledithead15.Text =drheads.GetValue(38).ToString();                   
            }
           
            double head1 = (total * Convert.ToDouble(txtlhead1.Text)) / 100;
            txtlhead1.Text = Math.Round(head1, 2).ToString();

            double head2 = (head1 * Convert.ToDouble(txtlhead2.Text)) / 100;
            txtlhead2.Text = Math.Round(head2, 2).ToString();

            double head3 = (head1 * Convert.ToDouble(txtlhead3.Text)) / 100;
            txtlhead3.Text = Math.Round(head3, 2).ToString();
            double head4 = (head1 * Convert.ToDouble(txtlhead4.Text)) / 100;
            txtlhead4.Text = Math.Round(head4, 2).ToString();
            //double head5 = (head1 * Convert.ToDouble(txtlhead5.Text)) / 100;
            //txtlhead5.Text = Math.Round(head5, 2).ToString();
            double head6 = (head1 * Convert.ToDouble(txtlhead6.Text)) / 100;
            txtlhead6.Text = Math.Round(head6, 2).ToString();
            double head7 = (head1 * Convert.ToDouble(txtlhead7.Text)) / 100;
            txtlhead7.Text = Math.Round(head7, 2).ToString();
            double head8 = (head1 * Convert.ToDouble(txtlhead8.Text)) / 100;
            txtlhead8.Text = Math.Round(head8, 2).ToString();
            double head9 = (head1 * Convert.ToDouble(txtlhead9.Text)) / 100;
            txtlhead9.Text = Math.Round(head9, 2).ToString();
            double head10 = (head1 * Convert.ToDouble(txtlhead10.Text)) / 100;
            txtlhead10.Text = Math.Round(head10, 2).ToString();
            double head11 = (head1 * Convert.ToDouble(txtlhead11.Text)) / 100;
            txtlhead11.Text = Math.Round(head11, 2).ToString();
            double head12 = (head1 * Convert.ToDouble(txtlhead12.Text)) / 100;
            txtlhead12.Text = Math.Round(head12, 2).ToString();
            double head13 = (head1 * Convert.ToDouble(txtlhead13.Text)) / 100;
            txtlhead13.Text = Math.Round(head13, 2).ToString();
            double head14 = (head1 * Convert.ToDouble(txtlhead14.Text)) / 100;
            txtlhead14.Text = Math.Round(head14, 2).ToString();
          

            double totalfinal = Math.Round((Convert.ToDouble(txtlhead1.Text) + Convert.ToDouble(txtlhead2.Text) + Convert.ToDouble(txtlhead3.Text) + Convert.ToDouble(txtlhead4.Text) + Convert.ToDouble(txtlhead5.Text) + Convert.ToDouble(txtlhead6.Text) + Convert.ToDouble(txtlhead7.Text) + Convert.ToDouble(txtlhead8.Text) + Convert.ToDouble(txtlhead9.Text) + Convert.ToDouble(txtlhead10.Text) + Convert.ToDouble(txtlhead11.Text) + Convert.ToDouble(txtlhead12.Text) + Convert.ToDouble(txtlhead13.Text) + Convert.ToDouble(txtlhead14.Text) ), 2);

            double head5 = (total - totalfinal);

            txtlhead5.Text = Math.Round(head5, 2).ToString();
            double totalfinal1 = Math.Round((Convert.ToDouble(txtlhead1.Text) + Convert.ToDouble(txtlhead2.Text) + Convert.ToDouble(txtlhead3.Text) + Convert.ToDouble(txtlhead4.Text) + Convert.ToDouble(txtlhead5.Text) + Convert.ToDouble(txtlhead6.Text) + Convert.ToDouble(txtlhead7.Text) + Convert.ToDouble(txtlhead8.Text) + Convert.ToDouble(txtlhead9.Text) + Convert.ToDouble(txtlhead10.Text) + Convert.ToDouble(txtlhead11.Text) + Convert.ToDouble(txtlhead12.Text) + Convert.ToDouble(txtlhead13.Text) + Convert.ToDouble(txtlhead14.Text) + Convert.ToDouble(txtlhead15.Text)), 2);
            txtetotal0.Text = Convert.ToString(totalfinal1);

        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    
    }

    protected void calculate(object sender, EventArgs e)
    {
        calculate_salary();
        newpanel.Visible = true;

    }

    public void calculate_salary() 
    {
        System.Data.DataTable dt_heads = new System.Data.DataTable();
        dt_heads.Columns.Add("head_name", typeof(string));
        int head_count = 0;
        double totalfinal1 = 0;
        d.con.Open();
        MySqlCommand cmd_heads = new MySqlCommand("SELECT E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15 from pay_company_master Where comp_code ='" + Session["comp_code"].ToString() + "'", d.con);
        MySqlDataReader dr_heads = cmd_heads.ExecuteReader();
        if(dr_heads.Read())
        {
            for (int i = 0; i < 15; i++)
            {
                if (dr_heads[i].ToString() != "")
                {
                    DataRow dr_headName = dt_heads.NewRow();
                    dr_headName["head_name"] = dr_heads[i].ToString().ToUpper();
                    dt_heads.Rows.Add(dr_headName);
                    head_count = head_count + 1;
                }
            }
        }
        dr_heads.Close();
        d.con.Close();
        MySqlCommand cmd_headformula = new MySqlCommand("SELECT E_HEAD01_per,E_HEAD02_per,E_HEAD03_per, E_HEAD04_per,E_HEAD05_per,E_HEAD06_per,E_HEAD07_per, E_HEAD1per,E_HEAD2per,E_HEAD3per,E_HEAD4per, E_HEAD5per, E_HEAD6per,E_HEAD7per,E_HEAD8per from pay_company_master Where comp_code ='" + Session["comp_code"].ToString() + "'", d.con);
        d.con.Open();
        MySqlDataReader dr_headsformula = cmd_headformula.ExecuteReader();
        if(dr_headsformula.Read())
        {
            double ctc_amount = Convert.ToDouble(txt_amount.Text);
            double ctc_monthly = ctc_amount / 12;

            double basic_amt = Math.Round((ctc_monthly * Convert.ToDouble(dr_headsformula[0].ToString())) / 100, 2);
            txtlhead1.Text = Math.Round(basic_amt).ToString();
            string current_head_formula = "";
            string heads_textbox = "txtlhead";
            
            for (int i = 1; i < head_count; i++)
            {
                string EvalResult = "";
                int j = 0;
                if (dr_headsformula[i].ToString() != "")
                {
                    if (Convert.ToString(dr_headsformula[i]).Contains("%"))
                    {
                        current_head_formula = dr_headsformula[i].ToString().Replace("%", "*0.01").ToUpper();
                    }
                    else if (Convert.ToString(dr_headsformula[i]).Contains("MONTHLY INCOME"))
                    {
                        current_head_formula = dr_headsformula[i].ToString().Replace("MONTHLY INCOME", ctc_monthly.ToString()).ToUpper();
                    }
                    else
                    {
                        current_head_formula = dr_headsformula[i].ToString().ToUpper();
                    }
                    while (j < i)
                    {
                        //----- Converting Heads into head amount --------
                        if (current_head_formula.Contains(dt_heads.Rows[j]["head_name"].ToString()))
                        {
                            int controlid = j + 1;
                            string headcontrolId = heads_textbox + controlid.ToString();
                            System.Web.UI.WebControls.TextBox txtheadamt = UpdatePanel3.FindControl(headcontrolId) as System.Web.UI.WebControls.TextBox;
                            string jhead_amt = txtheadamt.Text;
                            current_head_formula = current_head_formula.Replace(dt_heads.Rows[j]["head_name"].ToString(), txtheadamt.Text.ToString());
                        }
                        j++;
                    }
                    System.Data.DataTable dtEvalExpression = new System.Data.DataTable();
                    var v = dtEvalExpression.Compute(current_head_formula, "");
                    EvalResult = v.ToString();
                    //double current_head_amt = Convert.ToDouble(current_head_formula);
                    //double current_head_amt = Math.current_head_formula);
                    string curr_head = heads_textbox + (i + 1).ToString();
                    System.Web.UI.WebControls.TextBox Currtxtheadamt = UpdatePanel3.FindControl(curr_head) as System.Web.UI.WebControls.TextBox;
                    Currtxtheadamt.Text = EvalResult;
                }
            }
        }
        for (int i = 1; i <= head_count; i++)
        {
            string heads_textbox = "txtlhead";
            string headcontrolId = heads_textbox + i.ToString();
            System.Web.UI.WebControls.TextBox txtheadamt = UpdatePanel3.FindControl(headcontrolId) as System.Web.UI.WebControls.TextBox;
            string jhead_amt = txtheadamt.Text;
            totalfinal1 = totalfinal1 + Math.Round(Convert.ToDouble(jhead_amt.ToString()), 2);
            txtetotal0.Text = Convert.ToString(totalfinal1);
        }
        
        dr_heads.Close();
        d.con.Close();

    }
      private void send_email(string emailhtmlfile, string toaddress)
    {
        string body = string.Empty;
        string body1 = string.Empty;
        using (StreamReader reader = new StreamReader(emailhtmlfile))
        {
            body = reader.ReadToEnd();
        }
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.celtsoft.com");

            mail.From = new MailAddress("info@celtsoft.com");
            mail.To.Add(toaddress);
           // mail.CC.Add(tocc);
            mail.Subject = "User Details";
            d.con1.Open();
            MySqlCommand cmdmax1 = new MySqlCommand("SELECT EMP_NAME,EMP_CODE as 'Login ID' ,Date_Format( BIRTH_DATE,'%d/%m/%Y') as 'Password'  FROM pay_employee_master WHERE EMP_CODE = '" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            MySqlDataReader drmax = cmdmax1.ExecuteReader();
            while (drmax.Read())
            {
                body = body.Replace("{Name}", drmax.GetValue(0).ToString());
                body = body.Replace("{Login_Id}", drmax.GetValue(1).ToString());
                body = body.Replace("{Password}", drmax.GetValue(2).ToString());
                //mail.Body = tosubject;
            }
            drmax.Close();
            d.con1.Close();

            mail.Body = body;
            mail.IsBodyHtml = true;


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@celtsoft.com", "First@123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }

        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false); }

    }

    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    protected void ddl_location_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_location_city.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_location.SelectedValue + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location_city.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            ddl_location_city.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }
    }

    protected void get_client_details(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
        txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.SelectedItem.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_presentcity.Items.Add(dr_item1[0].ToString());
               
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

           
           
            newpanel.Visible = true;
        }

    }

    protected void Erning_Issu_Details(object sender, EventArgs e)
    {
        if (txt_highestqualification.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Check Internet Connection !!!')", false);
        }
       
    }

    private string generateihmscode()
    { 
    //IHMS/CLient Name/Gradecode/statecodeautoincrementnumber
        //examle IHMS/KM/HK/MH18

        string ihmscode = "IH&MS";
        //Client code
        ihmscode = ihmscode + "/" + getsingledata("select client_code from pay_unit_master where unit_code = '" + ddl_unitcode.SelectedValue + "'");
        //grade code
        ihmscode = ihmscode + "/" + ddl_grade.SelectedValue;
        //state code
        ihmscode = ihmscode + "/" + getsingledata("select state_code from pay_state_master where state_name = '" + ddl_location.SelectedValue + "'");

        return ihmscode + int.Parse(txt_eecode.Text.Substring(1, txt_eecode.Text.Length-1)); ;

    }

    private string getsingledata(string sql)
    {
        MySqlCommand cmd = new MySqlCommand(sql, d.con);
        try
        {
            d.con.Open();
            return (string)cmd.ExecuteScalar();
        }
        catch
        { }
        finally
        {
            d.con.Close();
            d.con.Dispose();
            cmd.Dispose();
        }
        return "";
    }
    protected void ddl_employmentstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_employmentstatus.SelectedItem.Text == "Daily")
        {
            txt_amount.Text = "0";
            txt_amount.Visible = false;
            lbl_ctc.Visible = false;
            d1.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT  pay_unit_grade_rate_master.E_HEAD01, pay_unit_grade_rate_master.E_HEAD02, pay_unit_grade_rate_master.E_HEAD03, pay_unit_grade_rate_master.E_HEAD04, pay_unit_grade_rate_master.E_HEAD05, pay_unit_grade_rate_master.E_HEAD06, pay_unit_grade_rate_master.E_HEAD07, pay_unit_grade_rate_master.E_HEAD08, pay_unit_grade_rate_master.E_HEAD09, pay_unit_grade_rate_master.E_HEAD10, pay_unit_grade_rate_master.E_HEAD11, pay_unit_grade_rate_master.E_HEAD12, pay_unit_grade_rate_master.E_HEAD13, pay_unit_grade_rate_master.E_HEAD14, pay_unit_grade_rate_master.E_HEAD15, pay_unit_grade_rate_master.TOTAL,pay_unit_grade_rate_master.comp_code, pay_unit_grade_rate_master.UNIT_CODE, pay_unit_grade_rate_master.GRADE_CODE  FROM pay_unit_grade_rate_master, pay_unit_master WHERE pay_unit_grade_rate_master.comp_code = pay_unit_master.comp_code AND pay_unit_grade_rate_master.UNIT_CODE = pay_unit_master.UNIT_CODE AND pay_unit_grade_rate_master.comp_code ='" + Session["comp_code"].ToString() + "' AND pay_unit_grade_rate_master.UNIT_CODE ='" + Session["UNIT_CODE"].ToString() + "' AND pay_unit_grade_rate_master.GRADE_CODE = '" + ddl_grade.SelectedValue + "'  ORDER BY pay_unit_grade_rate_master.UNIT_CODE,pay_unit_grade_rate_master.GRADE_CODE", d1.con1);
            MySqlDataReader dr_status = cmd1.ExecuteReader();
            if (dr_status.Read())
            {
                txtlhead1.Text = dr_status.GetValue(0).ToString();
                txtlhead2.Text = dr_status.GetValue(1).ToString();
                txtlhead3.Text = dr_status.GetValue(2).ToString();
                txtlhead4.Text = dr_status.GetValue(3).ToString();
                txtlhead5.Text = dr_status.GetValue(4).ToString();
                txtlhead6.Text = dr_status.GetValue(5).ToString();
                txtlhead7.Text = dr_status.GetValue(6).ToString();
                txtlhead8.Text = dr_status.GetValue(7).ToString();
                txtlhead9.Text = dr_status.GetValue(8).ToString();
                txtlhead10.Text = dr_status.GetValue(9).ToString();
                txtlhead11.Text = dr_status.GetValue(10).ToString();
                txtlhead12.Text = dr_status.GetValue(11).ToString();
                txtlhead13.Text = dr_status.GetValue(12).ToString();
                txtlhead14.Text = dr_status.GetValue(13).ToString();
                txtlhead15.Text = dr_status.GetValue(14).ToString();
                txtetotal0.Text = dr_status.GetValue(15).ToString();
            }
            dr_status.Close();
            d1.con1.Close();
        }
        else {

            txt_amount.Visible = true;
            lbl_ctc.Visible = true;
            txtlhead1.Text = "0";
            txtlhead2.Text = "0";
            txtlhead3.Text = "0";
            txtlhead4.Text = "0";
            txtlhead5.Text = "0";
            txtlhead6.Text = "0";
            txtlhead7.Text = "0";
            txtlhead8.Text = "0";
            txtlhead9.Text = "0";
            txtlhead10.Text = "0";
            txtlhead11.Text = "0";
            txtlhead12.Text = "0";
            txtlhead13.Text = "0";
            txtlhead14.Text = "0";
            txtlhead15.Text = "0";
            txt_basicpay.Text = "0";
            txtetotal0.Text = "0";
        }
        newpanel.Visible = true;
    }
}





      

